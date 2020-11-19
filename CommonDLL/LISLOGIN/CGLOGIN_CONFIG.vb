Imports System.Net
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommFN

Namespace CONFIG
    Public Class PRGINFO
        Private Const msFile As String = "File : RISAPP_LOGIN.vb, Class : RISAPP.CONFIG.RPGINFO" + vbTab

        Public Shared Sub sbGet_PrgInfo()
            Dim sFn As String = "Function sbGet_PrgInfo(String) As DataTable"
            Try
                Dim sSql As String = ""

                sSql += "SELECT sklcd, skldesc, sklflg"
                sSql += "  FROM lf094m"
                sSql += " WHERE sklgrp = '000'"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql)

                If dt.Rows.Count < 0 Then Return
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(ix).Item("sklcd").ToString
                        Case "1" : PROGRAM.PRGINFO.BCPRTFLG = dt.Rows(ix).Item("sklflg").ToString
                        Case "2" : PROGRAM.PRGINFO.AUTOTKFLG = dt.Rows(ix).Item("sklflg").ToString
                        Case "3" : PROGRAM.PRGINFO.PASSFLG = dt.Rows(ix).Item("sklflg").ToString
                        Case "4" : PROGRAM.PRGINFO.TK2JUBSUFLG = dt.Rows(ix).Item("sklflg").ToString
                        Case "5" : PROGRAM.PRGINFO.RSTMWFLG = dt.Rows(ix).Item("sklflg").ToString
                        Case "6" : PROGRAM.PRGINFO.RSTTNSFLG = dt.Rows(ix).Item("sklflg").ToString
                    End Select
                Next

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try

        End Sub
    End Class

    Public Class MENU
        Private Const msFile As String = "File : CGLOGIN_CONFIG.vb, Class : LOGIN.CONFIG.MENU" + vbTab

        Public Shared Function fnGet_MenuInfo(ByVal rsUsrID As String) As DataTable

            Dim sFn As String = "Public Shared Function fnGet_MenuInfo(String) As DataTable"

            Dim dt As New DataTable
            Dim sSql As String = ""

            Try
                sSql = ""
                sSql += "SELECT a.usrid, a.mnuidnew, a.isparent, a.mnulvl, a.mnuid, b.mnunm"
                sSql += "  FROM lf091m a, lf092m b"
                sSql += " WHERE a.mnuid = b.mnuid"
                sSql += "   AND a.usrid = :usrid"
                sSql += "   AND b.mnugbn in ('1', '9')"
                sSql += " ORDER BY SUBSTR(b.mnuid, 1, 2), b.vieworder, b.mnulvl, a.mnuidnew"

                Dim al As New ArrayList
                al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try

        End Function

        Public Shared Function fnGet_HotListInfo(ByVal rsUsrId As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_HotListInfo(String) As DataTable"

            Dim dt As New DataTable
            Dim sSql As String = ""

            Try
                sSql = ""
                sSql += "SELECT b.mnuid, b.mnunm, a.dispseq, a.icongbn"
                sSql += "  FROM lf095m a, lf092m b"
                sSql += " WHERE a.usrid = :usrid"
                sSql += "   AND a.mnuid = b.mnuid"
                sSql += "   AND b.mnugbn in ('1', '9')"
                sSql += " ORDER BY dispseq"

                Dim al As New ArrayList
                al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrId.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrId))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try
        End Function
    End Class

    Public Class AuthorityUpdate

        Public Shared Sub SetAuthority()

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

    Public Class FN
        Private Const msFile As String = "File : LOGIN.vb, Class : LOGIN.CONFIG.FN" & vbTab

        ' 사용자 로그인 정보 Query or 로그인 정보 설정
        Public Shared Function fnGetUsrInfo(ByVal rsUsrID As String) As Boolean
            Dim sFn As String = "Public Shared Function fnGetUsrInfo(String) As Boolean"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList

            Try
                If rsUsrID.Length = 1 Then Return False

                Dim objIpEntry As IPHostEntry = Dns.GetHostByName(Dns.GetHostName())
                Dim objIpAdrees As IPAddress() = objIpEntry.AddressList

                Dim sChageDay_pw As String = ""

                sSql = ""
                sSql += "SELECT clsval FROM lf000m WHERE clsgbn = '01' AND clscd = '005'"

                DbCommand()
                dt = DbExecuteQuery(sSql)

                If dt.Rows.Count > 0 Then sChageDay_pw = dt.Rows(0).Item("clsval").ToString

                sSql = ""
                sSql += "SELECT f9.usrid, f9.usrnm,f9.usrlvl, f9.medino, f9.other, f9.drspyn, f9.delflg,"
                sSql += "       fo.fldval pw_old,"
                If sChageDay_pw = "" Or sChageDay_pw = "0" Then
                    sSql += "       f9.usrpwd"
                Else
                    sSql += "       CASE WHEN TO_CHAR(SYSDATE - " + sChageDay_pw + ", 'yyyymmddhh24miss') < NVL(fo.regdt, fn_ack_sysdate) THEN f9.usrpwd ELSE '' END usrpwd"
                End If
                sSql += "  FROM lf090m f9 LEFT OUTER JOIN"
                sSql += "       (SELECT a.usrid, a.fldval, b.regdt"
                sSql += "          FROM lf097m a, (SELECT MAX(regdt) regdt FROM lf097m WHERE usrid = :usrid AND fldgbn = '2') b"
                sSql += "         WHERE a.usrid   = :usrid"
                sSql += "           AND a.fldgbn  = '2'"
                sSql += "           AND a.regdt   = b.regdt"
                sSql += "       ) fo ON (f9.usrid = fo.usrid)"
                sSql += " WHERE f9.usrid = :usrid"

                alParm.Clear()
                alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))
                alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))
                alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrID))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    With USER_INFO
                        .USRID = dt.Rows(0).Item("usrid").ToString.Trim()
                        .USRNM = dt.Rows(0).Item("usrnm").ToString.Trim()
                        .USRPW = dt.Rows(0).Item("usrpwd").ToString.Trim()
                        .USRLVL = dt.Rows(0).Item("usrlvl").ToString.Trim()
                        .OTHER = dt.Rows(0).Item("other").ToString.Trim()
                        .DRSPYN = dt.Rows(0).Item("drspyn").ToString.Trim()
                        .MEDINO = dt.Rows(0).Item("medino").ToString.Trim()
                        .DELFLG = dt.Rows(0).Item("delflg").ToString.Trim()
                        .USRPW_OLD = dt.Rows(0).Item("pw_old").ToString.Trim()
                        .LOCALIP = objIpAdrees(0).ToString
                    End With

                    COMMON.CommLogin.LOGIN.USER_SKILL.Clear()
                    PRG_CONST.Clear()

                    sbGetUsrSkill(USER_INFO.USRID)  ' 사용자별 사용가능 기능설정
                    sbGetUsrSkill_Master()          ' 기능마스터 로드
                    sbGet_DataTableInfo()

                    Return True

                Else
                    USER_INFO.Clear()
                    Return False

                End If

            Catch ex As Exception
                USER_INFO.Clear()
                Throw (New Exception(ex.Message, ex))
            End Try

        End Function

        ' 사용자 비밀번호 설정
        Public Shared Function fnExe_NewUsrPWD(ByVal rsUsrID As String, ByVal rsUsrPW As String) As Boolean
            Dim sFn As String = "fnExe_NewUsrPWD(ByVal asUsrID As String, ByVal asUsrPW As String) As Boolean"
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim dt As New DataTable
            Dim al As New ArrayList

            Dim dbCn As OracleConnection = DBORA.DbProvider.GetDbConnection()
            Dim dbTrans As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New OracleCommand

            Try

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTrans
                    .CommandType = CommandType.Text
                End With

                sSql = ""
                sSql += "UPDATE lf097m SET"
                sSql += "       fldval = :fldval,"
                sSql += "       regid  = :regid,"
                sSql += "       regdt  = fn_ack_sysdate"
                sSql += " WHERE usrid  = :usrid"
                sSql += "   AND fldgbn = '2'"

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("fldval", OracleDbType.Varchar2).Value = (New HashMD5).Encrypt(rsUsrID, rsUsrPW)
                dbCmd.Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrID
                dbCmd.Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrID

                dbCmd.CommandText = sSql
                iRet += dbCmd.ExecuteNonQuery()

                If iRet = 0 Then
                    sSql = ""
                    sSql += "INSERT INTO lf097m(  USRID,  FLDGBN,  FLDVAL,  REGID, REGDT )"
                    sSql += "            VALUES( :usrid, :fldgbn, :fldval, :regid, fn_ack_sysdate) "

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrID
                    dbCmd.Parameters.Add("fldgbn", OracleDbType.Varchar2).Value = "2"
                    dbCmd.Parameters.Add("fldval", OracleDbType.Varchar2).Value = (New HashMD5).Encrypt(rsUsrID, rsUsrPW)
                    dbCmd.Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrID

                    dbCmd.CommandText = sSql
                    iRet += dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        dbTrans.Rollback()
                        Return False
                    End If
                End If

                sSql = ""
                sSql += "UPDATE lf090m SET usrpwd = :usrpwd"
                sSql += " WHERE usrid  = :usrid"
                sSql += "   AND NVL(delflg, '0') = '0'"

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("usrpwd", OracleDbType.Varchar2).Value = (New HashMD5).Encrypt(rsUsrID, rsUsrPW)
                dbCmd.Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrID

                dbCmd.CommandText = sSql
                iRet += dbCmd.ExecuteNonQuery()

                If iRet > 0 Then
                    dbTrans.Commit()
                    Return True
                Else
                    dbTrans.Rollback()
                    Return False
                End If

            Catch ex As Exception
                dbTrans.Rollback()
                Return False
            Finally
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        ' 사용자별 사용가능 기능 Query 
        Private Shared Sub sbGetUsrSkill(ByVal rsUsrid As String)
            Dim sFn As String = "Private Shared Sub GetUsrSkill(ByVal asUsrid As String)"
            Dim dt As New DataTable
            Dim sSql As String = ""
            Dim al As New ArrayList

            Try
                sSql += ""
                sSql += "SELECT a.sklgrp, a.sklcd, b.skldesc"
                sSql += "  FROM lf093m a, lf094m b"
                sSql += " WHERE a.usrid  = :usrid"
                sSql += "   AND a.sklgrp = b.sklgrp"
                sSql += "   AND a.sklcd  = b.sklcd"

                al.Clear()
                al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, rsUsrid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsrid))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                USER_SKILL.SetAuthority = dt

            End Try

        End Sub

        ' 사용자별 사용가능 기능 Query 
        Private Shared Sub sbGetUsrSkill_Master()
            Dim sFn As String = "Private Shared Sub sbGetUsrSkill_Master()"
            Dim dt As New DataTable
            Dim sSql As String = ""

            Try
                sSql = ""
                sSql += "SELECT sklgrp, sklcd, skldesc"
                sSql += "  FROM lf094m"

                DbCommand()
                dt = DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                USER_SKILL.Authority_MST = dt
            End Try
        End Sub

        '-- Const 정의(검체분류, 검사부서)
        Public Shared Sub sbGet_DataTableInfo()
            Dim sFn As String = "Private Shared Sub sbGet_bcclsinfo()"
            Dim dt As New DataTable

            Try
                Dim sSql As String = ""

                sSql += ""
                sSql += "SELECT clsgbn clsitem, clscd, clsval"
                sSql += "  FROM lf000m"
                sSql += " UNION "
                sSql += "SELECT 'A' clsitem, bcclscd clscd, bcclsgbn clsval"
                sSql += "  FROM lf010m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND bcclsgbn <> '0'"
                sSql += " UNION "
                sSql += "SELECT 'A' clsitem, bcclscd clscd, bcclsgbn clsval"
                sSql += "  FROM rf010m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND bcclsgbn <> '0'"
                sSql += " UNION "
                sSql += "SELECT 'B' clsitem, partcd clscd, partgbn clsval"
                sSql += "  FROM lf020m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND partgbn <> '0'"
                sSql += " UNION "
                sSql += "SELECT 'B' clsitem, partcd clscd, partgbn clsval"
                sSql += "  FROM rf020m"
                sSql += " WHERE usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND partgbn <> '0'"
                sSql += " UNION "
                sSql += "SELECT 'C' clsitem, partcd || slipcd clscd, '' clsval"
                sSql += "  FROM lf060m"
                sSql += " WHERE NVL(exlabyn, '0') = '1'"
                sSql += "   AND usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"

                DbCommand()
                dt = DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                COMMON.CommLogin.LOGIN.PRG_CONST.Set_DataTable = dt
            End Try

        End Sub

        Public Shared Function fnExe_NurseUser(ByVal ra_UsrInfo As ArrayList) As Boolean
            Dim sFn As String = ""
            Dim sUsrLvl As String = ""
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim al As New ArrayList

            Try

                '< add freety 2006/02/01 : 자동로그인 오류가 일어나지 않도록 방지
                Select Case ra_UsrInfo(0).ToString
                    Case "WARD"
                        '병동간호사
                        sUsrLvl = "N"

                    Case "OUT"
                        '외래간호사
                        sUsrLvl = "R"

                    Case "PAT"
                        '진료지원간호사
                        sUsrLvl = "E"
                    Case "LIS"
                        sUsrLvl = "1"
                End Select

                sSql = ""
                sSql += "SELECT usrid"
                sSql += "  FROM lf090m"
                sSql += " WHERE usrid = :usrid"
                'sSql += "   AND usrlvl IN ('1', 'N', 'R', 'E')"
                '>
                al.Clear()
                al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, ra_UsrInfo(2).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_UsrInfo(2).ToString.Trim))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                Dim sOther As String = ""
                If ra_UsrInfo.Count > 4 Then sOther = ra_UsrInfo(4).ToString.Trim

                sSql = ""
                If dt.Rows.Count > 0 Then
                    '< add freety 2006/02/01 : 자동로그인 오류가 일어나지 않도록 방지
                    sSql = ""
                    sSql += "UPDATE lf090m"
                    sSql += "   SET usrnm = :usrnm,"
                    sSql += "       other = :other"
                    sSql += " WHERE USRID = :usrid"
                    '>
                    al.Clear()
                    al.Add(New OracleParameter("usrnm", OracleDbType.Varchar2, ra_UsrInfo(3).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_UsrInfo(3).ToString.Trim))
                    al.Add(New OracleParameter("other", OracleDbType.Varchar2, sOther.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sOther))
                    al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, ra_UsrInfo(2).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_UsrInfo(2).ToString.Trim))

                Else
                    ' USERID가 존재하지 않으면 Insert
                    sSql = ""
                    sSql += "INSERT INTO lf090m("
                    sSql += "             usrid, regdt, regid, usrnm, usrlvl, other, delflg)"
                    sSql += "    VALUES( :usrid, fn_ack_sysdate, 'ACK', :usrnm, :usrlvl, :other, '0')"

                    al.Clear()
                    al.Add(New OracleParameter("usrid", OracleDbType.Varchar2, ra_UsrInfo(2).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_UsrInfo(2).ToString.Trim))
                    al.Add(New OracleParameter("usrnm", OracleDbType.Varchar2, ra_UsrInfo(3).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, ra_UsrInfo(3).ToString.Trim))
                    al.Add(New OracleParameter("usrlvl", OracleDbType.Varchar2, sUsrLvl.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sUsrLvl))
                    al.Add(New OracleParameter("other", OracleDbType.Varchar2, sOther.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sOther))
                End If

                DbCommand()
                DbExecute(sSql, al, True)

                dt.Dispose()
                dt = Nothing

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

End Namespace