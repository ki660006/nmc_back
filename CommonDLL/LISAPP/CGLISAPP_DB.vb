Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN

Namespace APP_DB
    Public Class DbFn

        Private Const msFile As String = "File : CGLISAPP_DB.vb, Class : APP_DB.DBSql" + vbTab

        Public Shared Function fnGet_DbConnect() As OracleConnection
            Return Nothing
        End Function

        Public Function fnGet_Ward_Abbr(ByVal rsWardNo As String) As String
            Dim sFn As String = "Function GetViewToBCPrt(String) As String"

            Try

                If rsWardNo = "" Then Return ""

                Dim sSql As String
                Dim dt As New DataTable

                sSql = "SELECT FN_ACK_GET_WARD_ABBR(:wardno) FROM DUAL"

                Dim al As New ArrayList
                al.Add(New OracleParameter("wardno", rsWardNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Return ""

            End Try

        End Function

        Public Function fnGet_Dept_Abbr(ByVal rsIoGbn As String, ByVal rsDeptCd As String) As String
            Dim sFn As String = "Function GetViewToBCPrt(String) As String"

            Try

                If rsDeptCd = "" Then Return ""

                Dim sSql As String
                Dim dt As New DataTable

                sSql = "SELECT FN_ACK_GET_DEPT_ABBR(:iogbn, :deptcd) FROM DUAL"

                Dim al As New ArrayList

                al.Add(New OracleParameter("iogbn", rsIoGbn))
                al.Add(New OracleParameter("deptcd", rsDeptCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Return ""

            End Try

        End Function

        '화면출력검체번호를 바코드출력 검체번호로 변경
        Public Function GetViewToBCPrt(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function GetViewToBCPrt(String) As String"

            Try

                If Not rsBcNo.Length.Equals(15) Then Return ""

                Dim sSql As String
                Dim dt As New DataTable

                sSql = "SELECT fn_ack_get_bcno_prt(:bcno) FROM DUAL"

                Dim al As New ArrayList
                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Return ""

            End Try

        End Function

        Public Function GetViewToBCPrt(ByVal rsBcNo As String, ByVal r_dbCn As OracleConnection) As String
            Dim sFn As String = "Function GetViewToBCPrt(ByVal adtBaseDate As Date, ByVal asBCNO As String) As String"

            Try

                If Not rsBcNo.Length.Equals(15) Then Return ""

                Dim sSql As String
                Dim dt As New DataTable

                If r_dbCn Is Nothing Then
                    sSql = "SELECT fn_ack_get_bcno_prt(:bcno) FROM DUAL"

                    Dim al As New ArrayList
                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                    DbCommand()
                    dt = DbExecuteQuery(sSql, al)
                Else

                    sSql = "SELECT fn_ack_get_bcno_prt('" + rsBcNo + "') FROM DUAL"

                    dt = DbExecuteQuery(sSql, r_dbCn)
                End If

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Return ""

            End Try

        End Function

        '바코드출력검체번호를 화면출력검체번호 변경
        Public Function GetBCPrtToView(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function GetBCPrtToView(String) As String"

            Try

                If Not rsBcNo.Length.Equals(11) Then Return ""

                Dim sSql As String = ""
                Dim dt As New DataTable

                sSql = "SELECT fn_ack_get_bcno_normal(:bcno) FROM DUAL"

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Return ""

            End Try

        End Function

        '바코드출력검체번호를 화면출력검체번호 변경
        Public Function GetBCPrtToView(ByVal rsBcNo As String, ByVal r_DbCn As OracleConnection) As String
            Dim sFn As String = "Function GetBCPrtToView(ByVal asBCNO As String) As String"

            Try

                If Not rsBcNo.Length.Equals(11) Then Return ""

                Dim sSql As String = ""
                Dim dt As New DataTable

                If r_DbCn Is Nothing Then
                    sSql = "SELECT fn_ack_get_bcno_normal(:bcno) FROM DUAL"

                    Dim al As New ArrayList

                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                    DbCommand()
                    dt = DbExecuteQuery(sSql, al)
                Else

                    sSql = "SELECT fn_ack_get_bcno_normal('" + rsBcNo + "') FROM DUAL"

                    dt = DbExecuteQuery(sSql, r_DbCn)
                End If

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Return ""

            End Try

        End Function

    End Class

    Public Class DBSql
        Private Const msFile As String = "File : CGLISAPP_DB.vb, Class : APP_DB.DBSql" + vbTab

        ' SQL 구문 실행
        Public Shared Function ExcuteSql(ByVal r_al_sql As ArrayList, Optional ByVal rbMsgBox As Boolean = True, Optional ByRef riRows As Integer = 0) As Boolean
            Dim sFn As String = "Function ExcuteSql(ByVal alSqlDoc As ArrayList) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTrans As OracleTransaction = dbCn.BeginTransaction()

            With r_al_sql
                Try
                    If r_al_sql Is Nothing Then Return False


                    DbCommand(True)
                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    For ix = 0 To .Count - 1
                        Dim iRet As Integer = DbExecute(.Item(ix).ToString, True, dbCn, dbTrans)
                        If iRet > riRows Then
                            riRows = iRet
                        End If
                    Next

                    dbTrans.Commit()

                    Return True
                Catch ex As Exception
                    dbTrans.Rollback()

                    Fn.log(msFile & sFn, Err)
                    If rbMsgBox = True Then MsgBox(ex.Message)
                    Return False
                Finally
                    dbTrans.Dispose() : dbTrans = Nothing
                    dbCn.Dispose() : dbCn = Nothing

                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try
            End With

        End Function

        Public Shared Function ExcuteSql(ByVal r_dbCn As oracleConnection, ByVal r_al_sql As ArrayList, Optional ByVal rbMsgBox As Boolean = True, Optional ByRef riRows As Integer = 0) As Boolean
            Dim sFn As String = "Function ExcuteSql(ByVal alSqlDoc As ArrayList) As Boolean"

            Dim dbTran As OracleTransaction = r_dbCn.BeginTransaction()
            With r_al_sql
                Try
                    If r_al_sql Is Nothing Then Return False

                    COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                    For ix = 0 To .Count - 1
                        Dim iRet As Integer = DbExecute(.Item(ix).ToString, True, r_dbCn, dbTran)
                        If iRet > riRows Then
                            riRows = iRet
                        End If
                    Next

                    dbTran.Commit()

                    Return True

                Catch ex As Exception
                    dbTran.Rollback()

                    If rbMsgBox = True Then MsgBox(ex.Message)
                    Return False
                Finally
                    dbTran.Dispose() : dbTran = Nothing
                    COMMON.CommFN.MdiMain.DB_Active_YN = ""
                End Try
            End With
        End Function

    End Class

    Public Class LogSql
        Private Const mc_sFile As String = "File : CGDA_COMMON, Class : LogSql" & vbTab

        Private Shared mdtNow As Date = Date.MinValue

        Public Shared Sub Log_Begin(ByVal rsSql As String, ByVal r_al As ArrayList)
            Dim sFn As String = "Log_Begin(String, ArrayList)"

#If DEBUG Then
            Dim sDir As String = ""
            Dim sFile As String = ""
            Dim sw As IO.StreamWriter

            Try
                sDir = Windows.Forms.Application.StartupPath & "\SqlLog"

                If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

                sFile = sDir & "\SQL" & Format(Now, "yyyy-MM-dd") & ".txt"

                sw = New IO.StreamWriter(sFile, True, System.Text.Encoding.UTF8)

                mdtNow = Now

                sw.WriteLine(mdtNow)

                sw.WriteLine(vbTab & Replace(Replace(Replace(rsSql, "from", vbCrLf & vbTab & "from"), "select", vbCrLf & vbTab & "select"), "where", vbCrLf & vbTab & "where"))

                If r_al Is Nothing Then Return
                If r_al.Count = 0 Then Return

                For i As Integer = 1 To r_al.Count
                    Dim lisDbParam As oracleParameter = CType(r_al(i - 1), oracleParameter)

                    Dim sValue As String = "Value : " + lisDbParam.Value.ToString
                    Dim sType As String = "CacheDbType : " + lisDbParam.OracleDbType.ToString + ", DbType : " + lisDbParam.DbType.ToString
                    Dim sDirection As String = "Direction : " + lisDbParam.Direction.ToString

                    sw.WriteLine(vbTab + sValue + vbTab + sType + vbTab + sDirection)
                Next

            Catch ex As Exception
                COMMON.CommFN.Fn.log(mc_sFile & sFn, Err)
                MsgBox(mc_sFile & sFn & vbCrLf & ex.Message)

            Finally
                If sw IsNot Nothing Then
                    sw.Close()
                End If

            End Try
#End If
        End Sub

        Public Shared Sub Log_Begin(ByVal rsSql As String, ByVal r_lisdbCmd As oracleCommand)
            Dim sFn As String = "Log_Begin(String, DbCommand)"

#If DEBUG Then
            Dim sDir As String = ""
            Dim sFile As String = ""
            Dim sw As IO.StreamWriter

            Try
                sDir = Windows.Forms.Application.StartupPath & "\SqlLog"

                If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

                sFile = sDir & "\SQL" & Format(Now, "yyyy-MM-dd") & ".txt"

                sw = New IO.StreamWriter(sFile, True, System.Text.Encoding.UTF8)

                mdtNow = Now

                sw.WriteLine(mdtNow)

                sw.WriteLine(vbTab & Replace(Replace(Replace(rsSql, "from", vbCrLf & vbTab & "from"), "select", vbCrLf & vbTab & "select"), "where", vbCrLf & vbTab & "where"))

                If r_lisdbCmd Is Nothing Then Return
                If r_lisdbCmd.Parameters.Count = 0 Then Return

                For i As Integer = 1 To r_lisdbCmd.Parameters.Count
                    Dim lisDbParam As oracleParameter = r_lisdbCmd.Parameters(i - 1)

                    Dim sValue As String = "Value : " + lisDbParam.Value.ToString
                    Dim sType As String = "OracleDbType : " + lisDbParam.OracleDbType.ToString + ", DbType : " + lisDbParam.DbType.ToString
                    Dim sDirection As String = "Direction : " + lisDbParam.Direction.ToString

                    sw.WriteLine(vbTab + sValue + vbTab + sType + vbTab + sDirection)
                Next

            Catch ex As Exception
                COMMON.CommFN.Fn.log(mc_sFile & sFn, Err)
                MsgBox(mc_sFile & sFn & vbCrLf & ex.Message)

            Finally
                If sw IsNot Nothing Then
                    sw.Close()
                End If

            End Try
#End If
        End Sub

        Public Shared Sub Log_End(ByVal riRows As Integer)
            Dim sFn As String = "Log_End(Integer)"

#If DEBUG Then
            Dim sDir As String = ""
            Dim sFile As String = ""
            Dim sw As IO.StreamWriter

            Try
                sDir = Windows.Forms.Application.StartupPath & "\SqlLog"

                If IO.Directory.Exists(sDir) = False Then IO.Directory.CreateDirectory(sDir)

                sFile = sDir & "\SQL" & Format(Now, "yyyy-MM-dd") & ".txt"

                sw = New IO.StreamWriter(sFile, True, System.Text.Encoding.UTF8)

                sw.WriteLine(vbTab + " => " + riRows.ToString + "행, " + Now.Subtract(mdtNow).ToString)

            Catch ex As Exception
                COMMON.CommFN.Fn.log(mc_sFile & sFn, Err)
                MsgBox(mc_sFile & sFn & vbCrLf & ex.Message)

            Finally
                If sw IsNot Nothing Then
                    sw.Close()
                End If

            End Try
#End If
        End Sub
    End Class

    Public Class ServerDateTime
        Private Const msFile As String = "File : CGDA_COMMON.vb, Class : ServerDateTime" & vbTab

        Dim mDateTime As Date = Now

        Public Sub New()
            MyBase.New()
        End Sub

        Public ReadOnly Property GetDateTimeWithNewCn() As Date
            Get
                GetSVRDateTime()
                GetDateTimeWithNewCn = mDateTime
            End Get
        End Property

        Public ReadOnly Property GetDateTime() As Date
            Get
                GetSVRDateTime()
                GetDateTime = mDateTime
            End Get
        End Property

        Public ReadOnly Property GetDateTime24() As String
            Get
                GetSVRDateTime()
                GetDateTime24 = Format(mDateTime, "yyyy-MM-dd HH:mm:ss")
            End Get
        End Property

        Public ReadOnly Property GetDate(Optional ByVal asDelimiter As String = "") As String
            Get
                GetSVRDateTime()
                GetDate = mDateTime.Year.ToString & asDelimiter _
                        & Format(CInt(mDateTime.Month.ToString), "0#") & asDelimiter _
                        & Format(CInt(mDateTime.Day.ToString), "0#")
            End Get
        End Property

        Public ReadOnly Property GetTime24(Optional ByVal asDelimiter As String = ":") As String
            Get
                GetSVRDateTime()
                GetTime24 = Format(CInt(mDateTime.Hour.ToString), "0#") & asDelimiter _
                          & Format(CInt(mDateTime.Minute.ToString), "0#") & asDelimiter _
                          & Format(CInt(mDateTime.Second.ToString), "0#")
            End Get
        End Property

        Private Function GetSVRDateTime() As Boolean
            Dim sFn As String = "Function GetSVRDateTime() As Boolean"

            Try

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery("SELECT fn_ack_date_str(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') FROM DUAL")

                If dt.Rows.Count > 0 Then
                    mDateTime = CType(dt.Rows(0).Item(0), Date)
                End If

                Return True
            Catch ex As Exception
                COMMON.CommFN.Fn.log(msFile & sFn, Err)
                Return False
            End Try

        End Function

    End Class

    Public Class AuthorityUpdate

        Public Shared Sub setAuthority()

            STU_AUTHORITY.UsrID = USER_INFO.USRID

            Dim sDESC As String = ""
            ' 결과수정 기능 확인
            If Not USER_SKILL.Authority("R01", 1, sDESC) Then
                STU_AUTHORITY.RstUpdate = ""
            Else
                STU_AUTHORITY.RstUpdate = "1"
            End If
            ' Alert 보고 기능 확인
            If Not USER_SKILL.Authority("R01", 2, sDESC) Then
                STU_AUTHORITY.AFNReg = ""
            Else
                STU_AUTHORITY.AFNReg = "1"
            End If
            ' Panic 보고 기능 확인
            If Not USER_SKILL.Authority("R01", 3, sDESC) Then
                STU_AUTHORITY.PDFNReg = ""
            Else
                STU_AUTHORITY.PDFNReg = "1"
            End If
            ' Delta 보고 기능 확인
            If Not USER_SKILL.Authority("R01", 4, sDESC) Then
                STU_AUTHORITY.DFNReg = ""
            Else
                STU_AUTHORITY.DFNReg = "1"
            End If
            ' Critical 보고 기능 확인
            If Not USER_SKILL.Authority("R01", 5, sDESC) Then
                STU_AUTHORITY.CFNReg = ""
            Else
                STU_AUTHORITY.CFNReg = "1"
            End If
            ' 최종보고 수정
            If Not USER_SKILL.Authority("R01", 6, sDESC) Then
                STU_AUTHORITY.FNUpdate = ""
            Else
                STU_AUTHORITY.FNUpdate = "1"
            End If

            ' 결과검증 권한
            If Not USER_SKILL.Authority("R01", 9, sDESC) Then
                STU_AUTHORITY.FNReg = ""
            Else
                STU_AUTHORITY.FNReg = "1"
            End If

            ' 결과소거 권한
            If Not USER_SKILL.Authority("R01", 10, sDESC) Then
                STU_AUTHORITY.RstClear = ""
            Else
                STU_AUTHORITY.RstClear = "1"
            End If

        End Sub

    End Class

    Public Class clsSQLQueryString
        Public SQLFLAG As String = ""   '-- sql return 값 여부
        Public SQLDOC As String = ""    '-- sql문
    End Class

End Namespace


