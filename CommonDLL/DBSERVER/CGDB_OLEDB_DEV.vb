Imports System.IO
Imports System.Data
Imports COMMON.CommFN

Public Class DbOLE
    Private Const msFile As String = "File : CGDP_OleDb.vb, Class : DPOleDb" & vbTab

    Private Shared msDbConnStr As String = ""
    Private Shared m_DbCn As OleDb.OleDbConnection
    Private Shared m_DbCmd As OleDb.OleDbCommand
    Private Shared m_DbTran As OleDb.OleDbTransaction

    Protected Shared p_DbCn As OleDb.OleDbConnection
    Protected Shared p_DbCmd As OleDb.OleDbCommand
    Protected Shared p_DbTran As OleDb.OleDbTransaction

    ' 변경 Parameter없이 Connection
    Public Shared Sub OleDbConnection()
        Dim stuCStr As COMMON.CommDb.STU_CONNSTR
        Static iCnt As Integer = 0

        Try
            If IsNothing(m_DbCn) Then m_DbCn = New OleDb.OleDbConnection

            ' 연결이 끊겼을때 다시 연결한다.
            If m_DbCn.State = ConnectionState.Closed Then
                stuCStr = (New COMMON.CommDb.Info).GetConnStr

                If stuCStr.USERID <> "" Then

                    If stuCStr.DATASOURCE.StartsWith("10.0.0.10") And iCnt = 0 Then          '-- 운영1기 이면 운영2기로 바꿈
                        stuCStr.DATASOURCE = "10.0.0.10\EGMAINKFDEV"                                     '-- 운여2기
                        If Not MdiMain.Frm Is Nothing Then
                            MdiMain.Frm.Text.Replace("10.0.0.10\EGMAINKFDEV", "10.0.0.10\EGMAINKFDEV")          '-- 운영1, 운영2
                        End If

                    End If

                    m_DbCn.ConnectionString = "Provider=" + stuCStr.PROVIDER + _
                                              ";Data Source=" + stuCStr.DATASOURCE + _
                                              ";User ID=" + stuCStr.USERID + _
                                              ";Password=" + stuCStr.PASSWORD + _
                                               ";Initial Catalog=" + stuCStr.CATEGORY + ";OLEDB.NET=true"

                Else
                    ' UserID, Password 사용안함 ( ex:MDB 화일 ) 
                    m_DbCn.ConnectionString = "Provider=" + stuCStr.PROVIDER + ";Data Source=" + stuCStr.DATASOURCE
                End If

                m_DbCn.Open()

                '# MJOCS1 서버 다운 시 MJOCS2로 연결하는 로직 변경
                Dim objSvrInfo As New COMMON.CommDb.Info

                objSvrInfo.SetConnStr(stuCStr)
                objSvrInfo = Nothing
            End If

            iCnt = 0

        Catch ex As Exception
            iCnt += 1

            If iCnt <= 3 Then
                '> mod freety 2005/01/10
                stuCStr = (New COMMON.CommDb.Info).GetConnStr

                With stuCStr
                    .USEDP = "2"
                    .PROVIDER = stuCStr.PROVIDER       'SQLOLEDB, MSDAORA
                    .CATEGORY = stuCStr.CATEGORY

                    If .DATASOURCE = "10.0.0.10\EGMAINKFDEV" Then     '-- 운영1기
                        .DATASOURCE = "10.0.0.10\EGMAINKFDEV"         '-- 운영2기

                        If Not MdiMain.Frm Is Nothing Then
                            MdiMain.Frm.Text.Replace("10.0.0.10\EGMAINKFDEV", "10.0.0.10\EGMAINKFDEV")  '-- MJOCS1, MJOCS2
                        End If
                        '> add freety 2005/01/10
                    Else
                        .DATASOURCE = "10.0.0.10\EGMAINKFDEV"              '-- MJOCS1

                        If Not MdiMain.Frm Is Nothing Then
                            MdiMain.Frm.Text.Replace("10.0.0.10\EGMAINKFDEV", "10.0.0.10\EGMAINKFDEV")  '-- MJOCS2, MJOCS1
                        End If
                    End If

                    .USERID = "sa"
                    .PASSWORD = "egmainkf#$D"
                End With

                If (New COMMON.CommDb.Info).SetConnStr(stuCStr) = True Then
                    Fn.log("**** ReConnection ****")
                    OleDbConnection()
                End If

            Else
                iCnt = 0
                Throw (New Exception(ex.Message, ex))

            End If
        End Try

    End Sub

    '< add freety 2006/?/?
    Public Shared Function OleDbConnection(ByVal riMode As Integer, ByVal rsProvider As String, ByVal rsDatasource As String, _
                                              ByVal rsUsername As String, ByVal rsPassword As String, ByVal rsCategory As String) As OleDb.OleDbConnection
        Try
            If IsNothing(m_DbCn) Then m_DbCn = New OleDb.OleDbConnection

            ' 연결이 끊겼을때 다시 연결한다.
            If m_DbCn.State = ConnectionState.Closed Then
                m_DbCn.ConnectionString = "Provider=" + rsProvider + _
                                          ";Data Source=" + rsDatasource + _
                                          ";User ID=" + rsUsername + _
                                          ";Password=" + rsPassword + _
                                          ";OLEDB.NET=true"

                m_DbCn.Open()
            End If

            Return m_DbCn

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Function


    Public Shared Function GetDbConnection() As OleDb.OleDbConnection
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            OleDbConnection()

            Return m_DbCn

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

            Return Nothing
        End Try
    End Function

    '-- OCS 프로그램에서 DbConnect이 넣어 올 경우
    Public Shared Function SetDbConnection(ByVal r_DbCn As OleDb.OleDbConnection) As String

        Try
            If IsNothing(m_DbCn) Then m_DbCn = New OleDb.OleDbConnection

            m_DbCn = r_DbCn

            Return "OK"

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    '-- Close
    Public Shared Sub OleDbClose()
        Try
            If Not m_DbCn.State.Equals(ConnectionState.Closed) Then
                m_DbCn.Close()
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub

    Public Shared Sub OleDbCommand(Optional ByVal rbTransaction As Boolean = False)
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            OleDbConnection()

            If rbTransaction = True Then
                m_DbCmd = New OleDb.OleDbCommand("", m_DbCn)
                m_DbTran = m_DbCn.BeginTransaction()
            Else
                m_DbCmd = New OleDb.OleDbCommand
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Public Shared Sub OleDbCommand(ByVal r_o_DbTran As Object)
        Try
            OleDbConnection()

            p_DbCn = m_DbCn
            p_DbCmd = New OleDb.OleDbCommand()

            p_DbCmd.Connection = p_DbCn

            p_DbTran = CType(r_o_DbTran, OleDb.OleDbTransaction)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Public Shared Sub OleDbCommit()
        Try
            If Not IsNothing(m_DbTran) Then m_DbTran.Commit()

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Public Shared Sub OleDbRollback()
        Try
            If Not IsNothing(m_DbTran) Then m_DbTran.Rollback()

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    '-- Text구분형의 Return Table ( Select ) 
    Public Shared Function OleDbExecuteQuery(ByVal rdbSql As String) As DataTable

        Try
            With m_DbCmd
                .Connection = m_DbCn
                .CommandTimeout = 600000

                .CommandText = rdbSql

                '-- Transaction 처리인경우 
                If Not IsNothing(m_DbTran) Then .Transaction = m_DbTran

                .CommandType = CommandType.Text
            End With

            Dim dbDA As New OleDb.OleDbDataAdapter(m_DbCmd)
            Dim dt As New DataTable

            dbDA.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return New DataTable

        Finally
            m_DbCmd.Dispose()

        End Try
    End Function

    '-- Text구분형의 Return Table ( Select ) 
    Public Shared Function OleDbExecuteQuery(ByVal rdbSql As String, ByVal raList As ArrayList, ByVal rbText As Boolean) As DataTable
        Try
            With m_DbCmd
                .Connection = m_DbCn
                .CommandTimeout = 600000

                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                .CommandText = rdbSql

                For i As Integer = 1 To raList.Count
                    Dim OleDbParam As OleDb.OleDbParameter = CType(raList(i - 1), OleDb.OleDbParameter)

                    If OleDbParam.Direction = ParameterDirection.Input Or _
                        OleDbParam.Direction = ParameterDirection.InputOutput Then

                        .Parameters.Add(OleDbParam)
                    End If
                Next

                '-- Transaction 처리인경우 
                If Not IsNothing(m_DbTran) Then .Transaction = m_DbTran
            End With

            Dim OleDbDA As New OleDb.OleDbDataAdapter(m_DbCmd)
            Dim dt As New DataTable

            OleDbDA.Fill(dt)

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

            Return New DataTable
        Finally
            m_DbCmd.Dispose()
        End Try

    End Function




    '-- Text구분형의 Return Table ( Select ) 
    Public Shared Function OleDbExecuteQuery(ByVal rdbSql As String, ByRef r_o_stu As DbParrameter, ByVal rbText As Boolean) As DataTable

        Try
            Dim dt As New DataTable

            With m_DbCmd
                .Connection = m_DbCn
                .CommandTimeout = 600000
                .CommandText = rdbSql
                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                For ix As Integer = 0 To r_o_stu.ItemCnt

                    If r_o_stu.Item(ix).Size < 0 Then
                        .Parameters.Add(New OleDb.OleDbParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    Else
                        .Parameters.Add(New OleDb.OleDbParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType, r_o_stu.Item(ix).Size))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    End If

                    If r_o_stu.Item(ix).Direct = ParameterDirection.Input Or r_o_stu.Item(ix).Direct = ParameterDirection.InputOutput Then

                        .Parameters(r_o_stu.Item(ix).Name).Value = r_o_stu.Item(ix).Value
                    End If

                Next

                '-- Transaction 처리인경우 
                If Not IsNothing(m_DbTran) Then .Transaction = m_DbTran

            End With

            Dim objDAdapter As New OleDb.OleDbDataAdapter(m_DbCmd)
            objDAdapter.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return New DataTable
        Finally
            m_DbCmd.Dispose()
        End Try
    End Function

    '-- Text구분형의 Return Table ( Select ) 
    Public Shared Function OleDbExecuteQuery(ByVal rsSql As String, ByVal r_dbCn As OleDb.OleDbConnection) As DataTable
        Dim dbCmd As New OleDb.OleDbCommand
        Dim intRow As Integer = 0

        Try
            With dbCmd
                .Connection = r_dbCn
                .CommandTimeout = 600000
                .CommandText = rsSql
                .CommandType = CommandType.Text
            End With

            Dim dbDa As New OleDb.OleDbDataAdapter(dbCmd)
            Dim dt As New DataTable

            dbDa.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return New DataTable
        Finally
            dbCmd.Dispose()
        End Try

    End Function

    '-- Stored Procedure (Return Row ) Excute에 사용
    Public Shared Function OleDbExecute(ByVal rdbSql As String, ByVal rbText As Boolean) As Integer

        Try
            With m_DbCmd
                .Connection = m_DbCn
                .CommandTimeout = 600000
                .CommandText = rdbSql

                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                '-- Transaction 처리인경우 
                If Not IsNothing(m_DbTran) Then .Transaction = m_DbTran

                Return .ExecuteNonQuery
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return 0
        Finally
            m_DbCmd.Dispose()
        End Try

    End Function

    '-- Stored Procedure (Return ArrayLis ) Excute에 사용
    Public Shared Function OleDbExecute(ByVal rsSql As String, ByRef r_al_List As ArrayList, ByVal rbText As Boolean) As Integer
        Try
            With m_DbCmd
                .Connection = m_DbCn
                .CommandTimeout = 600000

                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                .CommandText = rsSql

                For ix As Integer = 1 To r_al_List.Count
                    Dim OleDbParam As OleDb.OleDbParameter = CType(r_al_List(ix - 1), OleDb.OleDbParameter)

                    If OleDbParam.Direction = ParameterDirection.Input Or OleDbParam.Direction = ParameterDirection.InputOutput Then
                        .Parameters.Add(OleDbParam)
                    End If
                Next

                '-- Transaction 처리인경우 
                If Not IsNothing(m_DbTran) Then .Transaction = m_DbTran

                Dim iRetRow As Integer = .ExecuteNonQuery()

                For ix As Integer = 1 To r_al_List.Count
                    If Not CType(r_al_List(ix - 1), OleDb.OleDbParameter).Direction = ParameterDirection.Input Then
                        CType(r_al_List(ix - 1), OleDb.OleDbParameter).Value = .Parameters(ix - 1).Value
                    End If
                Next

                Return iRetRow
            End With

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return 0
        Finally
            m_DbCmd.Dispose()

        End Try
    End Function

    '-- Stored Procedure (Return Parrameter ) Excute에 사용
    Public Shared Sub OleDbExecute(ByVal rsSql As String, ByRef r_o_stu As DbParrameter, ByVal rbText As Boolean)

        Try
            Dim iRet As Integer = 0

            With m_DbCmd
                .Connection = m_DbCn
                .CommandTimeout = 600000
                .CommandText = rsSql
                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                For ix As Integer = 0 To r_o_stu.ItemCnt

                    If r_o_stu.Item(ix).Size < 0 Then
                        .Parameters.Add(New OleDb.OleDbParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    Else
                        .Parameters.Add(New OleDb.OleDbParameter(r_o_stu.Item(ix).Name, r_o_stu.Item(ix).DBType, r_o_stu.Item(ix).Size))
                        .Parameters(r_o_stu.Item(ix).Name).Direction = r_o_stu.Item(ix).Direct
                    End If

                    If r_o_stu.Item(ix).Direct = ParameterDirection.Input Or _
                       r_o_stu.Item(ix).Direct = ParameterDirection.InputOutput Then

                        .Parameters(r_o_stu.Item(ix).Name).Value = r_o_stu.Item(ix).Value
                    End If

                Next

                '-- Transaction 처리인경우 
                If Not IsNothing(m_DbTran) Then .Transaction = m_DbTran

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
            m_DbCmd.Dispose()
        End Try

    End Sub

    '-- Stored Procedure (Return Row) Excute에 사용
    Public Shared Function OleDbExecute(ByVal rsSql As String, ByVal r_dbCn As OleDb.OleDbConnection, ByVal r_dbTran As OleDb.OleDbTransaction, ByVal rbText As Boolean) As Integer
        Dim dbCmd As New OleDb.OleDbCommand

        Try

            With dbCmd
                .Connection = r_dbCn
                .CommandTimeout = 600000
                .CommandText = rsSql

                If rbText Then
                    .CommandType = CommandType.Text
                Else
                    .CommandType = CommandType.StoredProcedure
                End If

                '-- Transaction 처리인경우 
                If Not IsNothing(r_dbTran) Then .Transaction = r_dbTran

                Return .ExecuteNonQuery
            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
            Return 0
        Finally
            dbCmd.Dispose()
        End Try

    End Function


End Class
