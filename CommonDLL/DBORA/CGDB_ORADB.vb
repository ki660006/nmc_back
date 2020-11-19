Imports System.IO
Imports Oracle.DataAccess.Client

Imports COMMON.CommFN

Public Class ORADB
    Private Const msFile As String = "File : CGDB_ORADB.vb, Class : ORADB" & vbTab
    Private Shared m_dbCn As OracleConnection

    ' 변경 Parameter없이 Connection
    Public Function DbConnection() As OracleConnection
        Dim stuCStr As COMMON.CommDb.STU_CONNSTR
        Static iCnt As Integer = 0

        Try
            If IsNothing(m_dbCn) Then m_dbCn = New OracleConnection

            ' 연결이 끊겼을때 다시 연결한다.
            If m_dbCn.State = ConnectionState.Closed Then
                m_dbCn = New OracleConnection

                stuCStr = (New COMMON.CommDb.Info).GetConnStr

                If stuCStr.USERID <> "" Then

                    Dim sCnStr As String = ""

                    sCnStr += ";Data Source=" + stuCStr.DATASOURCE
                    sCnStr += ";User ID=" + stuCStr.USERID
                    sCnStr += ";Password=" + stuCStr.PASSWORD

                    m_dbCn.ConnectionString = sCnStr
                Else
                    ' UserID, Password 사용안함 ( ex:MDB 화일 ) 
                    m_dbCn.ConnectionString = "Provider=" + stuCStr.PROVIDER + ";Data Source=" + stuCStr.DATASOURCE
                End If

                m_dbCn.Open()

                '# MJOCS1 서버 다운 시 MJOCS2로 연결하는 로직 변경
                Dim objSvrInfo As New COMMON.CommDb.Info

                objSvrInfo.SetConnStr(stuCStr)
                objSvrInfo = Nothing
            End If

            iCnt = 0
            Return m_dbCn

        Catch ex As Exception
            iCnt += 1

            MsgBox(ex.Message)

            If iCnt <= 3 Then
                '> mod freety 2005/01/10
                stuCStr = (New COMMON.CommDb.Info).GetConnStr

                With stuCStr
                    .USEDP = "2"
                    .PROVIDER = stuCStr.PROVIDER       'SQLOLEDB, MSDAORA
                    .CATEGORY = stuCStr.CATEGORY

                    If .DATASOURCE.IndexOf("10.95.21.141") >= 0 Then         '-- 운영1기
                        .DATASOURCE.Replace("10.95.21.141", "10.95.21.142")         '-- 운영2기

                        If Not MdiMain.Frm Is Nothing Then
                            MdiMain.Frm.Text.Replace("PROD_EMRDB1", "PROD_EMRDB2")  '-- MJOCS1, MJOCS2
                        End If
                        '> add freety 2005/01/10
                    Else
                        .DATASOURCE.Replace("10.95.21.201", "10.95.21.142").Replace("10.95.21.141", "10.95.21.142")         '-- 운영2기

                        If Not MdiMain.Frm Is Nothing Then
                            MdiMain.Frm.Text.Replace("PROD_EMRDB2", "PROD_EMRDB1")  '-- MJOCS1, MJOCS2
                        End If
                    End If

                    .USERID = "lisif"
                    .PASSWORD = "lisif"
                End With

                If (New COMMON.CommDb.Info).SetConnStr(stuCStr) = True Then
                    Fn.log("**** ReConnection ****")
                    DbConnection()
                End If

            Else
                iCnt = 0
                Throw (New Exception(ex.Message, ex))
            End If
        End Try

    End Function

    Public Function DbConnection_GCRL() As OracleConnection
        Dim stuCStr As COMMON.CommDb.STU_CONNSTR
        Static iCnt As Integer = 0
        Dim Dbcn_GCRL As New OracleConnection

        Try
            'If IsNothing(m_dbCn) Then m_dbCn = New OracleConnection

            Dim sCnStr As String = ""

            sCnStr += ";Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = store.gcrl.co.kr)(PORT = 1526))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = orcl))) "
            sCnStr += ";User ID=IUSER_NMC"
            sCnStr += ";Password=IUSER_NMC"

            Dbcn_GCRL.ConnectionString = sCnStr
            Dbcn_GCRL.Open()

            Return Dbcn_GCRL

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function

    '< add freety 2006/?/?
    Public Function DbConnection(ByVal rsProvider As String, ByVal rsDatasource As String, _
                                 ByVal rsUsername As String, ByVal rsPassword As String, ByVal rsCategory As String) As OracleConnection
        Try

            Dim dbCn As New OracleConnection

            ' 연결이 끊겼을때 다시 연결한다.
            If dbCn.State = ConnectionState.Closed Then
                'dbCn.ConnectionString = "Provider=" + rsProvider + _
                dbCn.ConnectionString = ";Data Source=" + rsDatasource + _
                                        ";User ID=" + rsUsername + _
                                        ";Password=" + rsPassword
                '";OLEDB.NET=true"

                dbCn.Open()
            End If

            Return dbCn

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Function

    Public Function DbConnection(ByVal rs_db_CnStr As String) As OracleConnection
        Try
            Dim dbCn As New OracleConnection

            dbCn.ConnectionString = rs_db_CnStr

            dbCn.Open()

            Return dbCn

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Function

    '-- Close
    Public Shared Sub DbClose()
        Try
            If Not m_dbCn.State.Equals(ConnectionState.Closed) Then
                m_dbCn.Close()
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub


    '-- Text구분형의 Return Table ( Select ) 
    Public Function DbExecuteQuery(ByVal rsSql As String, ByVal r_dbCn As OracleConnection, Optional ByVal r_dbTrans As OracleTransaction = Nothing) As DataTable

        Dim dbCmd As New OracleCommand

        Try
            With dbCmd
                .Connection = r_dbCn
                .CommandText = rsSql
                .CommandType = CommandType.Text

                If r_dbTrans IsNot Nothing Then .Transaction = r_dbTrans

            End With

            Dim dbDa As New OracleDataAdapter(dbCmd)
            Dim dt As New DataTable

            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
        End Try

    End Function

    '-- Text구분형의 Return Table ( Select ) 
    Public Function DbExecuteQuery(ByVal rsSql As String, ByVal r_al_List As ArrayList, ByVal rbText As Boolean, _
                                   ByVal r_dbCn As OracleConnection, Optional ByVal r_dbTrans As OracleTransaction = Nothing) As DataTable
        Dim dbCmd As New OracleCommand

        Try
            With dbCmd
                .Connection = r_dbCn

                If r_dbTrans IsNot Nothing Then .Transaction = r_dbTrans

                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                .CommandText = rsSql

                For i As Integer = 1 To r_al_List.Count
                    Dim OleDbParam As OracleParameter = CType(r_al_List(i - 1), OracleParameter)

                    If OleDbParam.Direction = ParameterDirection.Input Or _
                        OleDbParam.Direction = ParameterDirection.InputOutput Then

                        .Parameters.Add(OleDbParam)
                    End If
                Next


                If rbText = False And rsSql.ToLower.IndexOf(".pkg_") >= 0 Then
                    .Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor, Nothing, ParameterDirection.Output, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ""))
                End If
            End With

            Dim dbDa As New OracleDataAdapter(dbCmd)
            Dim dt As New DataTable

            dbDa.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
        End Try

    End Function


    '-- Text구분형의 Return Table ( Select ) 
    Public Function DbExecuteQuery(ByVal rsSql As String, ByRef r_o_stu As DbParrameter, ByVal rbText As Boolean, _
                                   ByVal r_dbCn As OracleConnection, Optional ByVal r_dbTrans As OracleTransaction = Nothing) As DataTable

        Dim dbCmd As New OracleCommand

        Try
            Dim dt As New DataTable

            With dbCmd
                .Connection = r_dbCn
                .CommandText = rsSql
                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                For ix As Integer = 0 To r_o_stu.ItemCnt

                    If r_o_stu.Item(ix).Size < 0 Then
                        .Parameters.Add(New OracleParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    Else
                        .Parameters.Add(New OracleParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType, r_o_stu.Item(ix).Size))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    End If

                    If r_o_stu.Item(ix).Direct = ParameterDirection.Input Or r_o_stu.Item(ix).Direct = ParameterDirection.InputOutput Then

                        .Parameters(r_o_stu.Item(ix).Name).Value = r_o_stu.Item(ix).Value
                    End If

                Next
                If rbText = False And rsSql.ToLower.IndexOf(".pkg_") >= 0 Then
                    .Parameters.Add(New OracleParameter("io_cursor", OracleDbType.RefCursor, Nothing, ParameterDirection.Output, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ""))
                End If
            End With

            Dim objDAdapter As New OracleDataAdapter(dbCmd)
            objDAdapter.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
        End Try

    End Function

    '-- Stored Procedure (Return Row ) Excute에 사용
    Public Function DbExecute(ByVal rdbSql As String, ByVal rbText As Boolean, ByVal r_dbCn As OracleConnection, Optional ByVal r_dbTran As OracleTransaction = Nothing) As Integer
        Dim dbCmd As New OracleCommand
        Dim iRet As Integer = 0

        Try

            With dbCmd
                .Connection = r_dbCn
                .CommandText = rdbSql

                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                If r_dbTran IsNot Nothing Then .Transaction = r_dbTran
                iRet = .ExecuteNonQuery
            End With

            Return iRet

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            dbCmd.Dispose() : dbCmd = Nothing
        End Try

    End Function

    '-- Stored Procedure (Return ArrayLis ) Excute에 사용
    Public Function DbExecute(ByVal rsSql As String, ByRef r_al_List As ArrayList, ByVal rbText As Boolean, _
                              ByVal r_dbCn As OracleConnection, Optional ByVal r_dbTran As OracleTransaction = Nothing) As Integer

        Dim dbCmd As New OracleCommand
        Dim iRet As Integer = 0

        Try
            With dbCmd
                .Connection = r_dbCn

                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                .CommandText = rsSql

                For ix As Integer = 1 To r_al_List.Count
                    Dim DbParam As OracleParameter = CType(r_al_List(ix - 1), OracleParameter)

                    If DbParam.Direction = ParameterDirection.Input Or DbParam.Direction = ParameterDirection.InputOutput Then
                        .Parameters.Add(DbParam)
                    End If
                Next

                If r_dbTran IsNot Nothing Then .Transaction = r_dbTran

                iRet = .ExecuteNonQuery()

                For ix As Integer = 1 To r_al_List.Count
                    If Not CType(r_al_List(ix - 1), OracleParameter).Direction = ParameterDirection.Input Then
                        CType(r_al_List(ix - 1), OracleParameter).Value = .Parameters(ix - 1).Value
                    End If
                Next

            End With

            Return iRet

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            dbCmd.Dispose() : dbCmd = Nothing

        End Try
    End Function

    '-- Stored Procedure (Return Parrameter ) Excute에 사용
    Public Sub DbExecute(ByVal rsSql As String, ByRef r_o_stu As DbParrameter, ByVal rbText As Boolean, _
                         ByVal r_dbCn As OracleConnection, Optional ByVal r_dbTran As OracleTransaction = Nothing)

        Dim dbCmd As New OracleCommand
        Dim iRet As Integer = 0

        Try

            With dbCmd
                .Connection = r_dbCn
                .CommandText = rsSql
                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                For ix As Integer = 0 To r_o_stu.ItemCnt

                    If r_o_stu.Item(ix).Size < 0 Then
                        .Parameters.Add(New OracleParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    Else
                        .Parameters.Add(New OracleParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType, r_o_stu.Item(ix).Size))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    End If

                    If r_o_stu.Item(ix).Direct = ParameterDirection.Input Or _
                       r_o_stu.Item(ix).Direct = ParameterDirection.InputOutput Then

                        .Parameters(r_o_stu.Item(ix).Name).Value = r_o_stu.Item(ix).Value
                    End If

                Next

                If r_dbTran IsNot Nothing Then .Transaction = r_dbTran

                iRet = .ExecuteNonQuery

                For ix = 0 To r_o_stu.ItemCnt
                    If Not r_o_stu.Item(ix).Direct.Equals(ParameterDirection.Input) Then
                        r_o_stu.itemValue(ix) = .Parameters(ix).Value.ToString
                    End If
                Next
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        Finally
            dbCmd.Dispose() : dbCmd = Nothing
        End Try

    End Sub

End Class
