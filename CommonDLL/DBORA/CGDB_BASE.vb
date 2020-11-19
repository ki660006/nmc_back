Imports System.IO
Imports Oracle.DataAccess.Client

Imports COMMON.CommFN
Imports System.Data.OleDb

Public Class DbProvider
    Private Const msFile As String = "File : CGDP_BASE.vb, Class : DbProvider" + vbTab

    Private Shared mConnectionString As String = ""
    Private Shared mOleDbConnection As OleDbConnection
    Private Shared mOleDbCommand As OleDbCommand

    Private Shared mOleDbTransaction As OleDbTransaction


    Private Shared Sub Log_Time(ByVal rsSql As String)
        Dim sFn As String = "Private Shared Sub Log_Time(String)"

#If DEBUG Then
        Try
            Dim sDir As String = Environment.CurrentDirectory + "\SqlLog"
            Dim sFile As String = sDir + "\SQL" + Format(Now, "yyyy-MM-dd") + ".txt"

            If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

            Dim sw As New StreamWriter(sFile, True, System.Text.Encoding.UTF8)

            sw.WriteLine(vbTab + rsSql.ToLower.Replace("from", vbCrLf + vbTab + "from").Replace("select", vbCrLf + vbTab + "select").Replace("where", vbCrLf & vbTab & "where"))
            sw.Close()

        Catch ex As Exception
            Throw (New Exception("[" + Err.Number.ToString + "] " + ex.Message + vbCrLf + " @" + msFile + sFn, ex))
        End Try
#End If

    End Sub
   
    '-- DB Connection Ms_Sql
    Public Shared Function GetOleDbConnection() As OleDb.OleDbConnection
        OleDbConnection()

        Return mOleDbConnection
    End Function
   

    ' 변경 Parameter없이 Connection
    Public Shared Sub OleDbConnection()
        'Dim objCnStr As COMMON.CommSvr.clsCONN_STR
        Static intCnt As Integer = 0

        Try
            If IsNothing(mOleDbConnection) Then mOleDbConnection = New OleDbConnection

            ' 연결이 끊겼을때 다시 연결한다.
            If mOleDbConnection.State = ConnectionState.Closed Then
                'objCnStr = (New COMMON.CommSvr.Info).GetConnStr

                'If objCnStr.USERID <> "" Then
                '    If objCnStr.DATASOURCE.StartsWith("192.168.100.106") And intCnt = 0 Then        '-- 운영
                '        If objCnStr.DATASOURCE = "192.168.100.106" Then                             '-- 운영
                '            If Not COMMON.CommFN.MdiMain.Frm Is Nothing Then
                '                COMMON.CommFN.MdiMain.Frm.Text.Replace("192.168.100.106", "192.168.100.106")    '-- 운영
                '            End If
                '        End If

                '        objCnStr.DATASOURCE = "192.168.100.106"                                     '-- 운영
                '    End If

                'mOleDbConnection.ConnectionString = "Provider= SQLOLEDB" + _
                '                                     ";Data Source= 211.54.17.205" + _
                '                                     ";User ID= ack" + _
                '                                     ";Password= Sml!@#$%^&" + ";OLEDB.NET=true"


                'Dim sID As String = ""
                'Dim sPW As String = ""
                'Dim sDSN As String = ""
                'Dim sProvider As String = ""

                'sID = "ack"
                'sPW = "Sml!@#$%^&"
                'sDSN = "SMLSERVER"
                'sProvider = "SQLOLEDB"

                'mOleDbConnection.ConnectionString = "Provider=" & sProvider & "DSN=" & sDSN & ";UID=" & sID & ";PWD=" & sPW & ";"

                mOleDbConnection.ConnectionString = "Provider=" + "SQLOLEDB" + _
                                                         ";Data Source=" + "211.54.17.205,1486" + _
                                                         ";User ID=" + "ack" + _
                                                         ";Password=" + "Sml!@#$%^&" + _
                                                         ";Initial Catalog=" + "OCS" + ";OLEDB.NET=true"

                'Dim sCnStr As String = ""
                'sCnStr += "Provider= " + "SQLOLEDB"
                'sCnStr += ";Data Source=" + "211.54.17.205"
                'sCnStr += ";User ID=" + "ack"
                'sCnStr += ";Password=" + "Sml!@#$%^&"

             

                ' mOleDbConnection.ConnectionString = sCnStr
                ' ";Initial Catalog=" + objCnStr.CATEGORY + ";OLEDB.NET=true"

            Else
                ' UserID, Password 사용안함 ( ex:MDB 화일 ) 
                'mOleDbConnection.ConnectionString = "Provider=" & objCnStr.PROVIDER _
                '                                                  & ";Data Source=" & objCnStr.DATASOURCE
            End If

            mOleDbConnection.Open()

            '# MJOCS1 서버 다운 시 MJOCS2로 연결하는 로직 변경
            'Dim objSvrInfo As New COMMON.CommSvr.Info
            'objSvrInfo.SetConnStr(objCnStr)
            'objSvrInfo = Nothing
            ' End If

            intCnt = 0

        Catch ex As Exception
            intCnt += 1

            'If intCnt <= 4 Then
            '    '> mod freety 2005/01/10
            '    objCnStr = (New COMMON.CommSvr.Info).GetConnStr

            '    With objCnStr
            '        .USEDP = "2"
            '        .PROVIDER = objCnStr.PROVIDER       'SQLOLEDB, MSDAORA
            '        .CATEGORY = objCnStr.CATEGORY

            '        If .DATASOURCE = "192.168.100.106" Then  '-- MJOCS1
            '            .DATASOURCE = "192.168.100.106"           '-- MJ0CS2

            '            If Not COMMON.CommFN.MdiMain.Frm Is Nothing Then
            '                COMMON.CommFN.MdiMain.Frm.Text.Replace("192.168.100.106", "192.168.100.106")  '-- MJOCS1, MJOCS2
            '            End If
            '            '> add freety 2005/01/10
            '        Else
            '            .DATASOURCE = "192.168.100.106"              '-- MJOCS1

            '            If Not COMMON.CommFN.MdiMain.Frm Is Nothing Then
            '                COMMON.CommFN.MdiMain.Frm.Text.Replace("192.168.100.106", "192.168.100.106")  '-- MJOCS2, MJOCS1
            '            End If
            '        End If

            '        .USERID = "fklis"
            '        .PASSWORD = "fklis#$R"
            '    End With

            '    If (New COMMON.CommSvr.Info).SetConnStr(objCnStr) = True Then
            '        COMMON.CommFN.Fn.log("**** ReConnection ****")
            '        OleDbConnection()
            '    End If

            'Else
            '    intCnt = 0
            '    Throw (New Exception(ex.Message, ex))

            'End If
        End Try

    End Sub


    '-- DB Connection 
    Public Shared Function GetDbConnection() As OracleConnection
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            Return (New ORADB).DbConnection()

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    '-- 녹십자 DB Connection
    Public Shared Function GetDbConnection_GCRL() As OracleConnection
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            Return (New ORADB).DbConnection_GCRL()

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function


    '-- DB Connection 
    Public Shared Function GetDbConnection(ByVal rsDbCnStr As String) As OracleConnection
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            Return (New ORADB).DbConnection(rsDbCnStr)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    '-- DB Connection 
    Public Shared Function GetDbConnection(ByVal rsProvider As String, ByVal rsDatasource As String, _
                                           ByVal rsUserId As String, ByVal rsPassword As String, ByVal rsCategory As String) As OracleConnection
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            Return (New ORADB).DbConnection(rsProvider, rsDatasource, rsUserId, rsPassword, rsCategory)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Function

    Public Shared Sub DbClose()
        Dim sFn As String = "Sub DbClose()"
        Try
            '-- DataProvider 설정 
            ORADB.DbClose()

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try
    End Sub



    Public Shared Function DbExecute(ByVal rsSql As String, ByVal r_al As ArrayList, Optional ByVal rbText As Boolean = True, _
                                     Optional ByVal r_db_Cn As OracleConnection = Nothing, _
                                     Optional ByVal r_db_trans As OracleTransaction = Nothing) As Integer
        Dim dbCn As OracleConnection = r_db_Cn
        Dim iRet As Integer = 0

        Try
            If r_db_Cn Is Nothing Then dbCn = (New ORADB).DbConnection

            COMMON.CommFN.Fn.log(rsSql)

            Dim dteStartTime As Date = Now

            iRet = (New ORADB).DbExecute(rsSql, r_al, rbText, dbCn, r_db_trans)

            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

            Return iRet

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            If r_db_Cn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then
                    dbCn.Close() : dbCn.Dispose() : dbCn = Nothing
                End If
            End If
        End Try

    End Function


    '-- For OleDb : Procedure사용으로 OutParameter Return
    Public Shared Function DbExecute(ByVal rsSql As String, Optional ByVal rbText As Boolean = True, _
                                     Optional ByVal r_db_Cn As OracleConnection = Nothing, _
                                     Optional ByVal r_db_trans As OracleTransaction = Nothing) As Integer
        Dim dbCn As OracleConnection = r_db_Cn
        Dim iRet As Integer = 0

        Try
            If r_db_Cn Is Nothing Then dbCn = (New ORADB).DbConnection

            Fn.log(rsSql)
            Dim dStartTime As Date = Now

            iRet = (New ORADB).DbExecute(rsSql, rbText, dbCn, r_db_trans)

            Dim RunLength As System.TimeSpan = Now.Subtract(dStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

            Return iRet

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            If r_db_Cn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then
                    dbCn.Close() : dbCn.Dispose() : dbCn = Nothing
                End If
            End If
        End Try
    End Function


    '-- For OleDb : Procedure사용으로 OutParameter Return
    Public Shared Sub DbExecute(ByVal rsSql As String, ByRef r_o_stu As DbParrameter, Optional ByVal rbText As Boolean = True, _
                                Optional ByVal r_db_Cn As OracleConnection = Nothing, _
                                Optional ByVal r_db_trans As OracleTransaction = Nothing)

        Dim dbCn As OracleConnection = r_db_Cn
        Dim iRet As Integer = 0

        Try
            If r_db_Cn Is Nothing Then dbCn = (New ORADB).DbConnection

            Fn.log(rsSql)
            Dim dStartTime As Date = Now

            Call (New ORADB).DbExecute(rsSql, r_o_stu, rbText, dbCn, r_db_trans)

            Dim RunLength As System.TimeSpan = Now.Subtract(dStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            If r_db_Cn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then
                    dbCn.Close() : dbCn.Dispose() : dbCn = Nothing
                End If
            End If
        End Try
    End Sub

    '-- Select: Return Datatable
    Public Shared Function DbExecuteQuery(ByVal rsSql As String, _
                                          Optional ByVal r_db_Cn As OracleConnection = Nothing, _
                                          Optional ByVal r_db_trans As OracleTransaction = Nothing) As DataTable

        Dim dbCn As OracleConnection = r_db_Cn
        Dim dt As New DataTable

        Try
            If r_db_Cn Is Nothing Then dbCn = (New ORADB).DbConnection

            Fn.log(rsSql)
            Dim dteStartTime As Date = Now

            dt = (New ORADB).DbExecuteQuery(rsSql, dbCn, r_db_trans)

            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " + RunLength.ToString + " 초")

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            If r_db_Cn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then
                    dbCn.Close() : dbCn.Dispose() : dbCn = Nothing
                End If
            End If

        End Try

    End Function

    '-- For OleDb : Return DataTable - Query
    Public Shared Function DbExecuteQuery(ByVal rsSql As String, ByVal r_al As ArrayList, _
                                          Optional ByVal rbText As Boolean = True, _
                                          Optional ByVal r_db_Cn As OracleConnection = Nothing, _
                                          Optional ByVal r_db_trans As OracleTransaction = Nothing) As DataTable

        Dim dbCn As OracleConnection = r_db_Cn
        Dim dt As New DataTable

        Try
            If r_db_Cn Is Nothing Then dbCn = (New ORADB).DbConnection

            Fn.log(rsSql)

            Dim dteStartTime As Date = Now

            dt = (New ORADB).DbExecuteQuery(rsSql, r_al, rbText, dbCn)

            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            If r_db_Cn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then
                    dbCn.Close() : dbCn.Dispose() : dbCn = Nothing
                End If
            End If

        End Try

    End Function

    '-- For OleDb : Procedure사용으로 Cursor Return 
    Public Shared Function DbExecuteQuery(ByVal rsSql As String, ByRef r_o_stu As DbParrameter, _
                                          Optional ByVal rbText As Boolean = True, _
                                          Optional ByVal r_db_Cn As OracleConnection = Nothing, _
                                          Optional ByVal r_db_trans As OracleTransaction = Nothing) As DataTable

        Dim dbCn As OracleConnection = r_db_Cn
        Dim dt As New DataTable

        Try
            If r_db_Cn Is Nothing Then dbCn = (New ORADB).DbConnection

            Fn.log(rsSql)
            Dim dStartTime As Date = Now


            dt = (New ORADB).DbExecuteQuery(rsSql, r_o_stu, rbText, dbCn, r_db_trans)

            Dim RunLength As System.TimeSpan = Now.Subtract(dStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        Finally
            If r_db_Cn Is Nothing Then
                If dbCn.State = ConnectionState.Open Then
                    dbCn.Close() : dbCn.Dispose() : dbCn = Nothing
                End If
            End If
        End Try

    End Function


    '-- DB Command 초기정의 (Transaction사용 유/무)
    Public Shared Sub DbCommand(Optional ByVal rbTransaction As Boolean = False)

    End Sub

    Public Shared Sub DbCommand(ByVal r_o_DbTran As Object)

    End Sub

End Class

Public Class DbParrameter

    Public Structure STU_PARRAMETER
        Dim Name As String
        Dim DBType As OracleDbType
        Dim Direct As ParameterDirection
        Dim Size As Integer
        Dim Value As Object
    End Structure

    Private m_stu_item() As STU_PARRAMETER
    Private m_i_ItemCount As Integer
    Private m_al_BindCount As Integer
    Private m_b_NewFlag As Boolean

    Public Sub New()
        m_i_ItemCount = 0
        ReDim m_stu_item(0)
        m_b_NewFlag = True
    End Sub

    Public Sub AddItem(ByVal asName As String, ByVal aoDbType As OracleDbType, ByVal aoDirect As ParameterDirection, ByVal asValue As Object)

        If Not m_b_NewFlag Then
            m_i_ItemCount += 1
            ReDim Preserve m_stu_item(m_i_ItemCount)
        Else
            m_b_NewFlag = False
        End If

        With m_stu_item(m_i_ItemCount)
            .Name = asName
            .DBType = aoDbType
            .Direct = aoDirect
            .Size = -1
            .Value = asValue
        End With
    End Sub

    Public Sub AddItem(ByVal rsName As String, ByVal r_o_DbType As OracleDbType, ByVal r_o_Direct As ParameterDirection, ByVal riSize As Integer, ByVal r_o_Value As Object)

        If Not m_b_NewFlag Then
            m_i_ItemCount += 1
            ReDim Preserve m_stu_item(m_i_ItemCount)
        Else
            m_b_NewFlag = False
        End If

        With m_stu_item(m_i_ItemCount)
            .Name = rsName
            .DBType = r_o_DbType
            .Direct = r_o_Direct
            .Size = riSize
            .Value = r_o_Value
        End With
    End Sub

    Public ReadOnly Property Item(ByVal Index As Integer) As STU_PARRAMETER
        Get
            Item = m_stu_item(Index)
        End Get
    End Property

    Public WriteOnly Property itemValue(ByVal index As Integer) As String
        Set(ByVal Value As String)
            m_stu_item(index).Value = Value
        End Set
    End Property

    Public ReadOnly Property ItemCnt() As Integer
        Get
            ItemCnt = m_i_ItemCount
        End Get
    End Property

    Public Property ArrayBindCount() As Integer
        Get
            ArrayBindCount = m_al_BindCount
        End Get
        Set(ByVal Value As Integer)
            m_al_BindCount = Value
        End Set
    End Property
End Class
