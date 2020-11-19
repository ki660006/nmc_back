Imports System.IO
Imports System.Data.OleDb
Imports COMMON.CommFN

Public Class DbProvider
    Private Const msFile As String = "File : CGDP_BASE.vb, Class : DataProvider" + vbTab

    Private Shared m_enumDP As enumDbProvider = enumDbProvider.OleDb
    Private Shared mDbProvider As enumDbProvider = enumDbProvider.OleDb

    '-- 데이타 프로바이더 종류
    Public Enum enumDbProvider
        Oracle9i = 0
        SqlServer2K = 1
        OleDb = 2
    End Enum

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
            Fn.Log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + Err.Description)
        End Try
#End If

    End Sub


    '-- DB Connection ( New )
    Public Shared Sub DbOpen()

        Try
            '-- DataProvider 설정 
            mDbProvider = CType((New COMMON.CommDb.Info).GetConnStr.USEDP, enumDbProvider)

            If mDbProvider = enumDbProvider.Oracle9i Then
                MsgBox("지원되지 않는 Db Provider 입니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then
                MsgBox("지원되지 않는 Db Provider 입니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                DbOLE.OleDbConnection()
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Public Shared Sub DbClose()
        Dim sFn As String = "Sub DbClose()"
        Try
            '-- DataProvider 설정 
            mDbProvider = CType((New COMMON.CommDb.Info).GetConnStr.USEDP, enumDbProvider)

            If mDbProvider = enumDbProvider.Oracle9i Then
            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                DbOLE.OleDbClose()
            End If

        Catch ex As Exception
            FN.Log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub


    '-- DB Command 초기정의 (Transaction사용 유/무)
    Public Shared Sub DbCommand(Optional ByVal rbTransaction As Boolean = False)
        Dim sFn As String = "Sub DbCommand(Optional ByVal abTransaction As Boolean = False)"
        Try
            '-- DataProvider 설정 
            mDbProvider = CType((New COMMON.CommDb.Info).GetConnStr.USEDP, enumDbProvider)

            If rbTransaction = True Then FN.Log("begin transaction")

            If mDbProvider = enumDbProvider.Oracle9i Then
                'ORA9I.OraCommand(abTransaction)
            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then
                ' /* 미적용 */

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                DbOLE.OleDbCommand(rbTransaction)

            End If

        Catch ex As Exception
            FN.Log(msFile + sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Public Shared Sub DbCommand(ByVal r_o_DbTran As Object)
        Dim sFn As String = "Sub DbCommand(Object)"

        Try
            m_enumDP = CType((New COMMON.CommDb.Info).GetConnStr.USEDP, enumDbProvider)

            If m_enumDP = enumDbProvider.Oracle9i Then
                MsgBox("지원되지 않는 Db Provider 입니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

            ElseIf m_enumDP = enumDbProvider.SqlServer2K Then
                MsgBox("지원되지 않는 Db Provider 입니다. 확인하여 주십시요!!", MsgBoxStyle.Exclamation)

            ElseIf m_enumDP = enumDbProvider.OleDb Then
                DbOLE.OleDbCommand(r_o_DbTran)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    '-- DB Commit
    Public Shared Sub DbCommit()
        Dim sFn As String = "Public Shared Sub DbCommit()"
        Try
            Fn.Log("commit transaction")

            If mDbProvider = enumDbProvider.Oracle9i Then

            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then
                ' /* 미적용 */

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                DbOLE.OleDbCommit()

            End If

        Catch ex As Exception
            Fn.Log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    '-- DB Rollaack 
    Public Shared Sub DbRollback()
        Dim sFn As String = "Public Shared Sub DbRollback()"
        Try
            Fn.Log("rollback transaction")

            If mDbProvider = enumDbProvider.Oracle9i Then

            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then
                ' /* 미적용 */

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                DbOLE.OleDbRollback()

            End If

        Catch ex As Exception
            Fn.Log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    '-- Select: Return Datatable
    Public Shared Function DbExecuteQuery(ByVal rsSql As String) As DataTable
        Dim sFn As String = "Public Shared Function DbExecuteQuery(String) As DataTable"
        Dim dt As New DataTable

        Try
            FN.Log(rsSql)
            Dim dteStartTime As Date = Now

            If mDbProvider = enumDbProvider.Oracle9i Then

            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                dt = DbOLE.OleDbExecuteQuery(rsSql)
            End If
            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " + RunLength.ToString + " 초")

        Catch ex As Exception
            Dim alLog As New ArrayList

            alLog.Add(msFile + sFn)
            alLog.Add(rsSql)

            FN.Log(alLog, Err)
            Throw (New Exception(ex.Message, ex))

            Return New DataTable

        Finally
            DbExecuteQuery = dt
        End Try

    End Function

    '-- For OleDb : Return DataTable - Query
    Public Shared Function DbExecuteQuery(ByVal rsSql As String, ByVal r_al As ArrayList) As DataTable
        Dim sFn As String = "Public Shared Function DbExecuteQuery(String, ArrayList) As DataTable"
        Dim dt As New DataTable

        Try
            FN.Log(rsSql)

            Dim dteStartTime As Date = Now

            dt = DbOLE.OleDbExecuteQuery(rsSql, r_al, True)

            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

        Catch ex As Exception
            Dim alLog As New ArrayList
            alLog.Add(msFile & sFn)
            alLog.Add(rsSql)

            FN.Log(alLog, Err)
            Throw (New Exception(ex.Message, ex))
        Finally
            DbExecuteQuery = dt

        End Try
    End Function

    Public Shared Function DbExecuteQuery(ByVal rsSql As String, ByVal r_dbCn As System.Data.OleDb.OleDbConnection) As DataTable
        Dim sFn As String = "Public Shared Function DbExecuteQuery(String, OleDb.OleDbConnection) As DataTable"

        Try
            Dim dt As New DataTable

            Fn.Log(rsSql)
            Dim dteStartTime As Date = Now
            If mDbProvider = enumDbProvider.Oracle9i Then

            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                dt = DbOLE.OleDbExecuteQuery(rsSql, r_dbCn)
            End If

            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

            Return dt

        Catch ex As Exception
            Dim alLog As New ArrayList
            alLog.Add(msFile & sFn)
            alLog.Add(rsSql)
            Fn.Log(alLog, Err)
            Throw (New Exception(ex.Message, ex))
            Return New DataTable
        End Try
    End Function

    Public Shared Function DbExecute(ByVal rsSql As String, ByVal r_al As ArrayList, ByVal rbText As Boolean) As Integer
        Dim sFn As String = "Public Shared Function DbExecute(String, ArrayList,Boolean) As Integer"
        Dim iRet As Integer = 0

        Try
            FN.Log(rsSql)

            Dim dteStartTime As Date = Now

            iRet = DbOLE.OleDbExecute(rsSql, r_al, rbText)
            
            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

        Catch ex As Exception
            Dim alLog As New ArrayList
            alLog.Add(msFile & sFn)
            alLog.Add(rsSql)
            Fn.log(alLog, Err)
            Throw (New Exception(ex.Message, ex))
        Finally
            DbExecute = iRet
        End Try

    End Function

    '-- Procedure사용 No Return
    Public Shared Function DbExecute(ByVal rsSql As String, ByVal rbText As Boolean) As Integer
        Dim sFn As String = "Public Shared Function DbExecute(String, Boolean) As Integer"

        Try
            Fn.log(rsSql)

            Dim iRow As Integer = 0
            Dim dteStartTime As Date = Now

            If mDbProvider = enumDbProvider.Oracle9i Then

            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then
                ' /* SqlServer용 미적용 */

            ElseIf mDbProvider = enumDbProvider.OleDb Then

                iRow = DbOLE.OleDbExecute(rsSql, rbText)
            End If

            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " + RunLength.ToString + " 초")

            Return iRow
        Catch ex As Exception
            Dim alLog As New ArrayList

            alLog.Add(msFile + sFn)
            alLog.Add(rsSql)

            Fn.log(alLog, Err)
            Throw (New Exception(ex.Message, ex))

            Return 0
        End Try
    End Function

    '-- For OleDb : Procedure사용으로 OutParameter Return
    Public Shared Sub DbExecute(ByVal rsSql As String, ByRef r_o_stu As DbParrameter, ByVal rbText As Boolean)
        Dim sFn As String = "Public Shared Sub DbExecute(String, ref DbParrameter, Boolean)"
        Try
            Fn.log(rsSql)
            Dim dStartTime As Date = Now

            DbOLE.OleDbExecute(rsSql, r_o_stu, rbText)
            
            Dim RunLength As System.TimeSpan = Now.Subtract(dStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

        Catch ex As Exception
            Dim alLog As New ArrayList
            alLog.Add(msFile & sFn)
            alLog.Add(rsSql)
            Fn.log(alLog, Err)

            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    Public Shared Function DbExecute(ByVal rsSql As String, ByVal r_dbCn As System.Data.OleDb.OleDbConnection, Optional ByVal r_DbTran As System.Data.OleDb.OleDbTransaction = Nothing, Optional ByVal rbText As Boolean = True) As Integer
        Dim sFn As String = "Public Shared Function DbExecute(String, [OleDb.OleDbConnection], [OleDb.OleDbTransaction], [rbText]) As Integer"

        Try

            Dim iRet As Integer = 0

            Fn.log(rsSql)
            Dim dteStartTime As Date = Now
            If mDbProvider = enumDbProvider.Oracle9i Then

            ElseIf mDbProvider = enumDbProvider.SqlServer2K Then

            ElseIf mDbProvider = enumDbProvider.OleDb Then
                iRet = DbOLE.OleDbExecute(rsSql, r_dbCn, r_DbTran, True)
            End If

            Dim RunLength As System.TimeSpan = Now.Subtract(dteStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

            Return iRet

        Catch ex As Exception
            Dim alLog As New ArrayList
            alLog.Add(msFile & sFn)
            alLog.Add(rsSql)
            Fn.Log(alLog, Err)
            Throw (New Exception(ex.Message, ex))

            Return 0
        End Try

    End Function

    Public Shared Function DbExecuteQuery(ByVal rsSql As String, ByVal r_al As ArrayList, ByVal rbText As Boolean) As DataTable
        Dim sFn As String = "Function DbExecuteQuery(String, ArrayList, Boolean) As Integer"

        Try
            Fn.log(rsSql)

            Dim dtStartTime As Date = Now

            Dim dt As DataTable = DbOLE.OleDbExecuteQuery(rsSql, r_al, rbText)

            Dim ts_RunLength As System.TimeSpan = Now.Subtract(dtStartTime)

            Log_Time(" => " & ts_RunLength.ToString & " 초")

            Return dt

        Catch ex As Exception
            Dim al_log As New ArrayList
            al_log.Add(msFile & sFn)
            al_log.Add(rsSql)
            Fn.log(al_log, Err)
            Throw (New Exception(ex.Message, ex))

            Return Nothing

        End Try
    End Function


    '-- For OleDb : Procedure사용으로 Cursor Return 
    Public Shared Function DbExecuteQuery(ByVal rsSql As String, ByRef r_o_stu As DbParrameter, ByVal rbText As Boolean) As DataTable
        Dim sFn As String = "Sub DbExecute(String, DbParrameter, Boolean)"

        Try
            Fn.log(rsSql)
            Dim dStartTime As Date = Now

            Dim dt As DataTable = DbOLE.OleDbExecuteQuery(rsSql, r_o_stu, rbText)

            Dim RunLength As System.TimeSpan = Now.Subtract(dStartTime)
            Log_Time(" => " & RunLength.ToString & " 초")

            Return dt

        Catch ex As Exception
            Dim alLog As New ArrayList
            alLog.Add(msFile & sFn)
            alLog.Add(rsSql)
            Fn.log(alLog, Err)
            Throw (New Exception(ex.Message, ex))

            Return New DataTable
        End Try

    End Function


    Public Shared Function GetDbConnection() As OleDbConnection
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            Return DbOLE.GetDbConnection()

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

            Return Nothing
        End Try
    End Function

    Public Shared Function GetDbConnection(ByVal riMode As Integer, ByVal rsProvider As String, ByVal rsDatasource As String, _
                                              ByVal rsUsername As String, ByVal rsPassword As String, ByVal rsCategory As String) As OleDbConnection
        Try
            ' 연결이 끊겼을때 다시 연결을위해
            Return DbOLE.OleDbConnection(riMode, rsProvider, rsDatasource, rsUsername, rsPassword, rsCategory)

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))

            Return Nothing
        End Try
    End Function

    '-- OCS 프로그램에서 DbConnect이 넣어 올 경우
    Public Shared Function SetDbConnection_Setting(ByVal r_DbCn As OleDb.OleDbConnection) As String

        Return DbOLE.SetDbConnection(r_DbCn)

    End Function


End Class

Public Class DbParrameter

    Public Structure STU_PARRAMETER
        Dim Name As String
        Dim DBType As System.Data.OleDb.OleDbType
        Dim Direct As System.Data.ParameterDirection
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

    Public Sub AddItem(ByVal asName As String, ByVal aoDbType As OleDbType, ByVal aoDirect As ParameterDirection, ByVal asValue As Object)

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

    Public Sub AddItem(ByVal asName As String, ByVal aoDbType As OleDbType, ByVal aoDirect As ParameterDirection, ByVal aiSize As Integer, ByVal asValue As Object)

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
            .Size = aiSize
            .Value = asValue
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
