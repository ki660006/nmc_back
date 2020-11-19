Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN

Namespace APP_S

#Region " 환자정보 : clsPatInfo_S"
    Public Class STU_PatInfo_S
        Public ORDDT As String = ""
        Public REGNO As String = ""
        Public PATNM As String = ""
        Public SexAge As String = ""
        Public IDNO As String = ""
        Public WardRoom As String = ""
        Public RESDT As String = ""
        Public IOGBN As String = ""
        Public OWNGBN As String = ""
        Public DEPT As String
        Public HREGNO As String = ""

        Public Sub New()
            MyBase.New()
        End Sub
    End Class
#End Region

#Region "팝업관련"
    Public Class PopUpInfo

        Private Const msFile As String = "File : POPUP_SPCINFO.vb, Class : RISAPP.APP_S.POPUPINFO" + vbTab

        Public Shared Function fnGet_RisSpcInfoList(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "fnGet_RisSpcInfoList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "select a.deptcd "
                sSql += "       , substr(b.orddt, 1, 4) || '-' || substr(b.orddt, 5, 2) || '-' || substr(b.orddt, 7, 2) as orddd "
                sSql += "       , c.tnms "
                sSql += "       , substr(b.orddt, 1, 4) || '-' || substr(b.orddt, 5, 2) || '-' || substr(b.orddt, 7, 2) || ' 00:00:00' "
                sSql += "       as hopedd "
                sSql += "       , substr(b.orddt, 1, 4) || '-' || substr(b.orddt, 5, 2) || '-' || substr(b.orddt, 7, 2) || ' ' "
                sSql += "       || substr(b.orddt, 9, 2) || ':' || substr(b.orddt, 11, 2) || ':' || substr(b.orddt, 13, 2) as orddt "
                sSql += "       , fn_ack_get_dr_name(b.orgdoctorcd) as doctornm "
                sSql += "       , decode(a.statgbn, '', '', '응급') as ergbn "
                sSql += "       , a.bcno, d.spcnmd as spcnm, d.spccd, a.regno, a.patnm, a.age || ' / ' || a.sex as agesex "
                sSql += "       , a.wardno || decode(a.roomno, '', '', '/' || a.roomno) as wardroom, b.tclscd "
                sSql += "  from rj010m a, rj011m b, rf060m c, lf030m d "
                sSql += " where a.bcno   = :bcno"
                sSql += "   and a.bcno   = b.bcno "
                sSql += "   and b.tclscd = c.testcd "
                sSql += "   and b.spccd  = c.spccd "
                sSql += "   and b.orddt  between c.usdt and c.uedt "
                sSql += "   and b.spccd  = d.spccd "
                sSql += "   and b.orddt  between d.usdt and d.uedt "
                sSql += "   order by b.tclscd, c.dispseql "

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_RisRstInfoList(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "fnGet_RisRstInfoList"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql = ""
                sSql += "select a.*, d.rstunit, f.eqnm, d.tnms   "
                sSql += "  from (select nvl(b.spcflg, 0) + nvl(c.rstflg, 0) as bcnostat "
                sSql += "             , substr(b.colldt, 1, 4) || '-' || substr(b.colldt, 5, 2) || '-' || substr(b.colldt, 7, 2) || ' ' "
                sSql += "             || substr(b.colldt, 9, 2) || ':' || substr(b.colldt, 11, 2) || ':' || substr(b.colldt, 13, 2) as colldt "
                sSql += "             , CASE WHEN c.tkdt is null THEN '' "
                sSql += "                    ELSE substr(c.tkdt, 1, 4) || '-' || substr(c.tkdt, 5, 2) || '-' || substr(c.tkdt, 7, 2) || ' '  "
                sSql += "                      || substr(c.tkdt, 9, 2) || ':' || substr(c.tkdt, 11, 2) || ':' || substr(c.tkdt, 13, 2) END as tkdt "
                sSql += "             , CASE WHEN c.regdt is null THEN '' "
                sSql += "                    ELSE substr(c.regdt, 1, 4) || '-' || substr(c.regdt, 5, 2) || '-' || substr(c.regdt, 7, 2) || ' '  "
                sSql += "                      || substr(c.regdt, 9, 2) || ':' || substr(c.regdt, 11, 2) || ':' || substr(c.regdt, 13, 2) END as regdt "
                sSql += "             , CASE WHEN c.fndt is null THEN '' "
                sSql += "                    ELSE substr(c.fndt, 1, 4) || '-' || substr(c.fndt, 5, 2) || '-' || substr(c.fndt, 7, 2) || ' '  "
                sSql += "                      || substr(c.fndt, 9, 2) || ':' || substr(c.fndt, 11, 2) || ':' || substr(c.fndt, 13, 2) END as fndt "
                sSql += "             , c.viewrst "
                sSql += "             , fn_ack_get_usr_name(b.collid) collnm "
                sSql += "             , fn_ack_get_usr_name(c.tkid) tknm "
                sSql += "             , fn_ack_get_usr_name(c.regid) regnm "
                sSql += "             , fn_ack_get_usr_name(c.fnid) fnnm "
                sSql += "             , b.tclscd   "
                sSql += "             , nvl(c.testcd, b.tclscd) as testcd "
                sSql += "             , nvl(c.spccd , b.spccd) as spccd "
                sSql += "             , nvl(c.tkdt , b.colldt) as msdt   "
                sSql += "             , c.eqcd "
                sSql += "          from rj010m a, rj011m b, rr010m c "
                sSql += "         where a.bcno   = :bcno"
                sSql += "           and a.bcno   = b.bcno "
                sSql += "           and b.bcno   = c.bcno (+)  "
                sSql += "           and b.tclscd = c.tclscd (+) "
                sSql += "           and b.spccd = c.spccd(+)"
                sSql += "       ) a, rf060m d, lf030m e, rf070m f  "
                sSql += " where a.testcd = d.testcd "
                sSql += "   and a.spccd  = d.spccd "
                sSql += "   and a.msdt   between d.usdt and d.uedt "
                sSql += "   and d.tcdgbn <> 'B' "
                sSql += "   and a.spccd  = e.spccd  "
                sSql += "   and a.msdt between e.usdt and e.uedt "
                sSql += "   and a.eqcd = f.eqcd(+) "
                sSql += "   order by a.tclscd, d.dispseql "

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

    End Class
#End Region

#Region "결과관련"
    Public Class RstSrh
        Private Const msFile As String = "File : CGRISAPP_S.vb, Class : RISAPP.APP_S.FGRISS01" + vbTab

        '-- 이상자조회(작업번호)
        Public Shared Function fnGet_AbnormalList_Wkno(ByVal rsWkYmd As String, ByVal rsWkgrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                       ByVal rsTestCds As String, ByVal rbFnYn As Boolean) As DataTable
            Dim sFn As String = "Get_AbnormalList_Wkno"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, f3.spcnmd, f6.tnmd,"
                sSql += "       r.testcd || r.spccd testspc, r.orgrst, r.viewrst, r.rstflg, fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       r.bforgrst, r.bfviewrst, fn_ack_get_bcno_full(r.bfbcno) bfbcno, fn_ack_date_str(r.bffndt, 'yyyy-mm-dd hh24:mi') bfrstdt,"
                sSql += "       r.panicmark, r.deltamark, r.criticalmark, r.alertmark, r.hlmark,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2"
                sSql += "  FROM rr010m r, rj010m j, lf030m f3, rf060m f6"
                sSql += " WHERE r.wkymd   = :wkymd"
                sSql += "   AND r.wkgrpcd = :wkgrp"
                sSql += "   AND r.wkno   <= :wknos"
                sSql += "   AND r.wkno   >= :wknoe"

                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, rsWkgrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkgrpCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                If rbFnYn Then sSql += "   AND r.rstflg = '3'"

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"
                End If

                sSql += "   AND NVL(r.rstflg, ' ') <> ' '"
                sSql += "   AND NVL(r.orgrst, ' ') <> ' '"
                sSql += "   AND (r.panicmark = 'P' OR r.deltamark = 'D' OR r.criticalmark = 'C' OR r.alertmark in ('E', 'A'))"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND j.spcflg  = '4'"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.spccd   = f6.spccd"
                sSql += "   AND r.tkdt   >= f6.usdt"
                sSql += "   AND r.tkdt   <  f6.uedt"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '<<<병원체검사 조회 
        Public Shared Function fn_get_HosRst(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsTestcds As String, ByVal rsPartSlip As String, ByVal rsBcno As String, ByVal rsUsrid As String) As DataTable
            Dim sFn As String = "fn_get_HosRst"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "" + vbCrLf
                sSql += " SELECT distinct '11101318' as hospinm  " + vbCrLf
                sSql += "        , '국립중앙의료원'  as hospital " + vbCrLf
                sSql += "        , fn_ack_get_ocs_mediid_bcno(j10.bcno) as usrnm " + vbCrLf
                sSql += "        , J10.patnm AS patnm" + vbCrLf
                sSql += "        , case when J10.sex = 'M' THEN '남'" + vbCrLf
                sSql += "               when j10.sex = 'F' THEN '여' end as sex " + vbCrLf
                sSql += "        , PAT.birtdate as birth" + vbCrLf
                sSql += "        , J10.regno  " + vbCrLf
                sSql += "        , fn_ack_get_dept_code(J10.iogbn, J10.deptcd)  || '/' || fn_ack_get_ward_abbr(J10.wardno) deptcd " + vbCrLf
                sSql += "        , f30.spccd||'/'||f30.spcnmd as spc " + vbCrLf
                sSql += "        , '' as spcnmd " + vbCrLf
                sSql += "        , '' as etc  " + vbCrLf
                sSql += "        , '' as etc2 " + vbCrLf


                sSql += "    , '' as refcd  " + vbCrLf

                sSql += "        , substr(r10.tkdt , 0,8) as tkdt " + vbCrLf  'REPLACE(to_char(sysdate , 'YYYY-MM-DD'),'-','') as sysdt ," + vbCrLf
                sSql += "        , SUBSTR(r10.fndt , 0,8) as fndt " + vbCrLf
                sSql += "        , FN_ACK_GET_USR_NAME(r10.fnid) as fnnm " + vbCrLf
                sSql += "        , '11101318' as hospinm2 " + vbCrLf
                sSql += "        , FN_ACK_GET_USR_NAME('" + rsUsrid + "') as rptnm " + vbCrLf
                sSql += "        , FN_ACK_GET_BCNO_RST(r10.bcno) as orgrsts " + vbCrLf
                sSql += "        , r10.bcno , FN_ACK_GET_KCDC_STATE(r10.bcno) as state " + vbCrLf
                sSql += "        , fn_ack_get_kcdc_regdt_state(r10.bcno) as decla " + vbCrLf
                sSql += "        , fn_ack_get_bcno_bac_rst (r10.bcno) as bacrst, fn_ack_get_bcno_Anti_rst(r10.bcno) as antirst  "
                sSql += "        , FN_ACK_GET_KCDC_ERRMSG(r10.bcno) as errmsg " + vbCrLf
                sSql += " FROM ( SELECT  r.bcno  ,  min(r.tkdt) tkdt  , max(r.fnid) fnid , max(r.fndt) fndt " + vbCrLf

                sSql += "  FROM rr010m r   " + vbCrLf

                If rsBcno <> "" Then
                    sSql += " WHERE r.bcno = :bcno " + vbCrLf
                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                Else
                    sSql += "  WHERE r.fndt >= '" + rsDateS + "' || '000000'" + vbCrLf
                    '20170927 전재휘 수정.
                    sSql += "    AND r.fndt <= '" + rsDateE + "'|| '235959'" + vbCrLf
                End If

                If rsPartSlip <> "" And rsBcno = "" Then
                    sSql += "   AND r.partcd = :partcd" + vbCrLf
                    sSql += "   AND r.slipcd = :slipcd" + vbCrLf

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsTestcds <> "" And rsBcno = "" Then
                    sSql += "   AND r.testcd in(" + rsTestcds + ")" + vbCrLf
                End If

                sSql += "  AND r.fndt is not null  " + vbCrLf
                sSql += "  GROUP by r.bcno ) r10 " + vbCrLf
                sSql += "       INNER JOIN RJ010M J10 ON R10.BCNO = J10.BCNO AND J10.RSTFLG >= '1'" + vbCrLf
                sSql += "       INNER JOIN VW_ACK_OCS_PAT_INFO PAT ON PAT.INSTCD = '031' AND J10.REGNO  =  PAT.PATNO " + vbCrLf
                sSql += "       INNER JOIN lf030m f30 ON j10.spccd = f30.spccd AND r10.tkdt >= f30.usdt AND r10.tkdt <= f30.uedt " + vbCrLf
                sSql += " order by fndt , regno" + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 이상자조회(검사그룹)
        Public Shared Function fnGet_AbnormalList_Tgrp(ByVal rsSlipCd As String, ByVal rsTgrpCd As String, _
                                                       ByVal rsRstDtS As String, ByVal rsRstDtE As String, _
                                                       ByVal rsTestCds As String, ByVal rbFnYn As Boolean) As DataTable
            Dim sFn As String = "fnGet_AbnormalList_Tgrp"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, f3.spcnmd, f6.tnmd,"
                sSql += "       r.testcd || r.spccd testspc, r.orgrst, r.viewrst, r.rstflg, fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       r.bforgrst, r.bfviewrst, fn_ack_get_bcno_full(r.bfbcno) bfbcno, fn_ack_date_str(r.bffndt, 'yyyy-mm-dd hh24:mi') bfrstdt,"
                sSql += "       r.panicmark, r.deltamark, r.criticalmark, r.alertmark, r.hlmark,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2"
                sSql += "  FROM rr010m r, rj010m j, lf030m f3, rf060m f6"
                sSql += " WHERE r.rstdt >= :dates"
                sSql += "   AND r.rstdt <= :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','").ToString + "')"
                ElseIf rsTgrpCd <> "" Then
                    sSql += "   AND (SUBSTR(r.testcd, 1, 5), r.spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"

                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTgrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTgrpCd))
                ElseIf rsSlipCd <> "" Then
                    sSql += "   AND f6.partcd = :partcd"
                    sSql += "   AND f6.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If

                If rbFnYn Then sSql += "   AND r.rstflg = '3'"

                sSql += "   AND NVL(r.rstflg, ' ') <> ' '"
                sSql += "   AND NVL(r.orgrst, ' ') <> ' '"
                sSql += "   AND (r.panicmark = 'P' OR r.deltamark = 'D' OR r.criticalmark = 'C' OR r.alertmark in ('E', 'A'))"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt  <  f3.uedt"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- 특정결과 값조회(특수검사)
        Public Shared Function fnGet_Search_Rstval_SP(ByVal rsRstDtS As String, ByVal rsRstDtE As String, ByVal rbFn As Boolean, ByVal rsQryGbn As String) As DataTable

            Dim sFn As String = "fnGet_Search_Rstval_SP"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       f3.spcnmd, f6.tnmd, NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd,"
                sSql += "       '{null}' orgrst, r.viewrst, r.rstflg, fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       r.bforgrst, '' bfviewrst, fn_ack_get_bcno_full(r.bfbcno) bfbcno, fn_ack_date_str(r.bffndt, 'yyyy-mm-dd hh24:mi') bffndt,"
                sSql += "       r.hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       '' reftxt, '' panictxt, '' deltagbn, '' deltatxt, '' criticaltxt, '' alerttxt"
                sSql += "  FROM rj010m j, rr010m r, rrs10m rs, lf030m f3, rf060m f6"
                sSql += " WHERE j.bcno   = r.bcno"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt  <  f3.uedt"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND NVL(r.rstflg, ' ') <> ' '"
                sSql += "   AND NVL(r.orgrst, ' ') <> ' '"
                sSql += "   AND r.bcno   = rs.bcno"
                sSql += "   AND r.testcd = rs.testcd"

                If rbFn Then sSql += "   AND r.rstflg = '3'"

                If rsQryGbn <> "" Then
                    sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd + r.spccd").Replace("#ORGRST", "rs.rsttxt") + ")"
                End If

                sSql += "   AND r.rstdt >= :dates"
                sSql += "   AND r.rstdt <= :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 특정결과 값조회
        Public Shared Function fnGet_Search_Rstval(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rbFn As Boolean, _
                                                   ByVal rsQryGbn As String, ByVal rsOpt As String, ByVal rsRefL As String, ByVal rsRefH As String, _
                                                   ByVal rsPanic As String, ByVal rsDelta As String, _
                                                   ByVal rsCritical As String, ByVal rsAlert As String) As DataTable

            Dim sFn As String = "fnGet_Search_Rstval"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, f3.spcnmd, f6.tnmd, f6.dispseql,"
                sSql += "       r.testcd, r.spccd, r.orgrst, r.viewrst, r.rstflg,"
                sSql += "       fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       r.bforgrst, r.bfviewrst, fn_ack_get_bcno_full(r.bfbcno) bfbcno, fn_ack_date_str(r.bffndt, 'yyyy-mm-dd hh24:mi') bffndt,"
                sSql += "       r.hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       fn_ack_get_test_reftxt(f6.refgbn, j.sex, re.reflms, re.reflm, re.refhms, re.refhm, re.reflfs, re.reflf, re.refhfs, re.refhf, re.reflt) reftxt,"
                sSql += "       CASE WHEN f6.panicgbn = '0' THEN ''"
                sSql += "            WHEN f6.panicgbn = '1' THEN '> ' || NVL(f6.panicl, f6.panich)"
                sSql += "            WHEN f6.panicgbn = '2' THEN '< ' || NVL(f6.panich, f6.panicl)"
                sSql += "            WHEN f6.panicgbn = '3' THEN f6.panicl || ' ~ ' || f6.panich"
                sSql += "            WHEN f6.panicgbn = '4' THEN '> ' || NVL(f6.panicl, f6.panich) || '(Grade)'"
                sSql += "            WHEN f6.panicgbn = '5' THEN '< ' || NVL(f6.panich, f6.panicl) ||'(Grade)'"
                sSql += "            WHEN f6.panicgbn = '6' THEN f6.panicl || ' ~ ' || f6.panich || '(Grade)'"
                sSql += "       END panictxt,"
                sSql += "       CASE WHEN f6.deltagbn = '0' THEN ''"
                sSql += "            WHEN f6.deltagbn = '1' THEN '변화차'"
                sSql += "            WHEN f6.deltagbn = '2' THEN '변화비율'"
                sSql += "            WHEN f6.deltagbn = '3' THEN '기간당변화차'"
                sSql += "            WHEN f6.deltagbn = '4' THEN '기간당변화비율'"
                sSql += "            WHEN f6.deltagbn = '5' THEN '절대변화비율'"
                sSql += "            WHEN f6.deltagbn = '6' THEN 'Grade Delta'"
                sSql += "       END deltagbn,"
                sSql += "       CASE WHEN f6.deltagbn = '0' THEN ''"
                sSql += "            WHEN f6.deltagbn = '1' THEN '> ' || NVL(f6.deltal, deltah)"
                sSql += "            WHEN f6.deltagbn = '2' THEN '< ' || NVL(f6.deltah, f6.deltal)"
                sSql += "            WHEN f6.deltagbn = '3' THEN f6.deltal || ' ~ ' || f6.deltah"
                sSql += "            WHEN f6.deltagbn = '4' THEN '> ' || NVL(f6.deltal, f6.deltah)"
                sSql += "            WHEN f6.deltagbn = '5' THEN '< ' || NVL(f6.deltah, f6.deltal)"
                sSql += "            WHEN f6.deltagbn = '6' THEN f6.deltal || ' ~ ' || f6.deltah"
                sSql += "       END deltatxt,"
                sSql += "       CASE WHEN f6.criticalgbn = '0' THEN ''"
                sSql += "            WHEN f6.criticalgbn = '1' THEN '> ' || NVL(f6.criticall, f6.criticalh)"
                sSql += "            WHEN f6.criticalgbn = '2' THEN '< ' || NVL(f6.criticalh, f6.criticall)"
                sSql += "            WHEN f6.criticalgbn = '3' THEN f6.criticall || ' ~ ' || f6.criticalh"
                sSql += "       END criticaltxt,"
                sSql += "       CASE WHEN f6.alertgbn = '0' THEN ''"
                sSql += "            WHEN f6.alertgbn = '1' THEN '> ' || NVL(f6.alertl, f6.alerth)"
                sSql += "            WHEN f6.alertgbn = '2' THEN '< ' || NVL(f6.alerth, f6.alertl)"
                sSql += "            WHEN f6.alertgbn = '3' THEN f6.alertl || ' ~ ' || f6.alerth"
                sSql += "            WHEN f6.alertgbn = '4' THEN '= ' || NVL(f6.alertl, f6.alerth)"
                sSql += "            ELSE 'Alert Rule'"
                sSql += "       END alerttxt"
                sSql += "  FROM lf030m f3, rf060m f6, rj010m j, rr010m r,"
                sSql += "       (SELECT DISTINCT"
                sSql += "               r.bcno, f61.*"
                sSql += "          FROM rj010m j, rr010m r, rf060m f6, rf061m f61"
                sSql += "         WHERE j.bcno    = r.bcno"
                sSql += "           AND r.rstdt  >= :dates"
                sSql += "           AND r.rstdt  <= :datee || '235959'"
                sSql += "           AND r.testcd  = f6.testcd"
                sSql += "           AND r.spccd   = f6.spccd"
                sSql += "           AND r.tkdt   >= f6.usdt"
                sSql += "           AND r.tkdt   <  f6.uedt"
                sSql += "           AND f6.testcd = f61.testcd"
                sSql += "           AND f6.spccd  = f61.spccd"
                sSql += "           AND f6.usdt   = f61.usdt"
                sSql += "           AND ROUND(f61.sagec * 365) + f61.sages * 0.1 <= j.dage"
                sSql += "           AND j.dage <= ROUND(f61.eagec * 365) - f61.eages * 0.1"
                sSql += "       ) re"
                sSql += " WHERE j.bcno   = r.bcno"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt  <  f3.uedt"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND NVL(r.rstflg, ' ') <> ' '"
                sSql += "   AND NVL(r.orgrst, ' ') <> ' '"
                sSql += "   AND r.bcno   = re.bcno (+)"
                sSql += "   AND r.testcd = re.testcd (+)"
                sSql += "   AND r.spccd  = re.spccd (+)"

                Dim sTmp As String = ""

                If rsOpt.ToLower = "or" And rsPanic.Length + rsDelta.Length + rsCritical.Length + rsAlert.Length > 1 Then
                    sTmp = ""
                    If rsRefL <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.hlmark = 'L'"
                    If rsRefH <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.hlmark = 'H'"
                    If rsPanic <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.panicmark = 'P'"
                    If rsDelta <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.deltamark = 'D'"
                    If rsCritical <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.criticalmark = 'C'"
                    If rsAlert <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.alertmark IN ('E', 'A')"

                    sSql += "   AND (" & sTmp & ")"
                Else
                    If rsRefL <> "" Then sTmp += "   AND r.hlmark = 'L'"
                    If rsRefH <> "" Then sTmp += "   AND r.hlmark = 'H'"
                    If rsPanic <> "" Then sSql += "   AND r.panicmark = 'P'"
                    If rsDelta <> "" Then sSql += "   AND r.deltamark = 'D'"
                    If rsCritical <> "" Then sSql += "   AND r.criticalmark = 'C'"
                    If rsAlert <> "" Then sSql += "   AND r.alertmark IN ('E', 'A')"
                End If

                If rbFn Then sSql += "   AND r.rstflg = '3'"

                If rsQryGbn <> "" Then
                    sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd + r.spccd").Replace("#ORGRST", "r.orgrst") + ")"
                End If

                sSql += "   AND r.rstdt >= :dates"
                sSql += "   AND r.rstdt <= :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 특이결과 조회
        Public Shared Function fnGet_Abnormal_ActionInfo(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String, _
                                                         Optional ByVal rsRegNo As String = "", Optional ByVal rsPatNm As String = "", _
                                                         Optional ByVal rsDeptCd As String = "", Optional ByVal rsWardno As String = "", _
                                                         Optional ByVal rsCmtCont As String = "", _
                                                         Optional ByVal rsCfmFlg As String = "", Optional ByVal rsDrCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Abnormal_ActionInfo(String...) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(r5.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       j.bcno, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward,"
                sSql += "       f3.spcnmd, fn_ack_get_usr_name(r5.regid) regnm, r5.regid,"
                sSql += "       r5.cmtcont || CASE WHEN NVL(r5.cfmcont, ' ') = ' ' THEN '' ELSE CHR(13) || CHR(10) || '[조치내용]' || CHR(13) || CHR(10) || r5.cfmcont END cmtcont,"
                sSql += "       fn_ack_get_dr_name(r5.cfmid) cfmnm, fn_ack_date_str(r5.cfmdt, 'yyyy-mm-dd hh24:mi') cfmdt"
                sSql += "  FROM rr050m r5, rj010m j, lf030m f3"
                sSql += " WHERE r5.regdt  >= :dates"
                sSql += "   AND r5.regdt  <= :datee || '235959'"
                sSql += "   AND r5.bcno    = j.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then
                    sSql += "   AND r5.partcd = :partcd"
                    sSql += "   AND r5.slipcd = :slipcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.patnm LIKE :patnm || '%'"
                    alParm.Add(New OracleParameter("patnm", OracleDbType.Varchar2, rsPatNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPatNm))
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If

                If rsWardno <> "" Then
                    sSql += "   AND j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardno))
                End If

                If rsCmtCont <> "" Then
                    sSql += "   AND cmtcont LIKE '%' || :cmtcont || '%'"
                    alParm.Add(New OracleParameter("cmtcont", OracleDbType.Varchar2, rsCmtCont.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCont))
                End If

                If rsDrCd <> "" Then
                    sSql = sSql + "   AND j.doctorcd = :drcd"
                    alParm.Add(New OracleParameter("drcd", OracleDbType.Varchar2, rsDrCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDrCd))
                End If

                If rsCfmFlg = "NO" Then
                    sSql = sSql + "   AND (r5.cfmdt IS NULL or NVL(r5.cfmdt, ' ') = ' ')"
                    sSql = sSql + " ORDER BY cfmdt, regdt"
                ElseIf rsCfmFlg = "OK" Then
                    sSql = sSql + "   AND NVL(r5.cfmdt, ' ') <> ' '"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 부적합검체 조회
        Public Shared Function fnGet_Unfit_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String, _
                                                Optional ByVal rsRegNo As String = "", Optional ByVal rsPatNm As String = "", _
                                                Optional ByVal rsDeptCd As String = "", Optional ByVal rsWardno As String = "", _
                                                Optional ByVal rsCmtCont As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Unfit_List(...) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(r5.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       j.bcno, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptward,"
                sSql += "       cmtcont,"
                sSql += "       f3.spcnmd, fn_ack_get_usr_name(r5.regid) regnm, r5.regid"
                sSql += "  FROM rr053m r5, rj010m j, lf030m f3"
                sSql += " WHERE r5.regdt  >= :dates"
                sSql += "   AND r5.regdt  <= :datee || '235959'"
                sSql += "   AND r5.bcno    = j.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then
                    sSql += "   AND r5.partcd = :partcd"
                    sSql += "   AND r5.slipcd = :slipcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.patnm LIKE :patnm || '%'"
                    alParm.Add(New OracleParameter("patnm", OracleDbType.Varchar2, rsPatNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPatNm))
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDeptCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If

                If rsWardno <> "" Then
                    sSql += "   AND j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWardno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardno))
                End If

                If rsCmtCont <> "" Then
                    sSql += "   AND r5.cmtcont LIKE '%' || :cmtcont || '%'"
                    alParm.Add(New OracleParameter("cmtcont", OracleDbType.Varchar2, rsCmtCont.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCont))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 결과대장(작업번호) 
        Public Shared Function fnGet_RstList_WGrp(ByVal rsWkYmd As String, ByVal rsWkGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                  ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "fnGet_RstList_WGrp"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       r.testcd, f6.tnmd, f6.tnmp, f6.partcd || f6.slipcd partslip,"
                'sSql += "       fn_ack_get_slip_comment(j.bcno, f6.partcd, f6.slipcd) slipcmt,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(a, ',' || a.cmt)).extract('//text()'), 2)"
                sSql += "          FROM rr040m a"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.partcd = f6.partcd"
                sSql += "           AND a.slipcd = f6.slipcd"
                sSql += "       ) slipcmt,"
                sSql += "       r.viewrst, r.rstcmt, hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.spccd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2"
                sSql += "  FROM rj010m j, rr010m r, rf060m f6, lf030m f3"
                sSql += " WHERE r.wkymd   = :wkymd"
                sSql += "   AND r.wkgrpcd = :wkgrp"
                sSql += "   AND r.wkno   >= :wknos"
                sSql += "   AND r.wkno   <= :wknoe"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.spccd   = f6.spccd"
                sSql += "   AND r.tkdt   >= f6.usdt"
                sSql += "   AND r.tkdt   <  f6.uedt"
                sSql += "   AND ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))"
                sSql += "   AND r.rstflg IN ('" + rsRstFlg.Replace(",", "','").ToString + "')"

                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                If rsTestCds <> "" Then sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd "

                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += " ORDER BY workno, tkdt, bcno, sort1, sort2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 결과대장(검사그룹)
        Public Shared Function fnGet_RstList_TGrp(ByVal rsPartSlip As String, ByVal rsTGrpCd As String, ByVal rsDateS As String, ByVal rsDateE As String, _
                                                  ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "fnGet_RstList_TGrp"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       r.testcd, f6.tnmd, f6.tnmp, f6.partcd  ||  f6.slipcd partslip,"
                'sSql += "       fn_ack_get_slip_comment(j.bcno, f6.partcd, f6.slipcd) slipcmt,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(a, ',' || a.cmt)).extract('//text()'), 2)"
                sSql += "          FROM rr040m a"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.partcd = f6.partcd"
                sSql += "           AND a.slipcd = f6.slipcd"
                sSql += "       ) slipcmt,"
                sSql += "       r.viewrst, r.rstcmt, hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.spccd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2"
                sSql += "  FROM rj010m j, rr010m r, rf060m f6, lf030m f3"
                sSql += " WHERE r.rstdt >= :dates"
                sSql += "   AND r.rstdt <= :datee || '235959'"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt  <  f3.uedt"
                sSql += "   AND ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))"
                sSql += "   AND r.rstflg IN ('" + rsRstFlg.Replace(",", "','") + "')"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"

                ElseIf rsTGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(r.testcd, 1, 5), r.spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    alParm.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "   AND f6.partcd = :partcd"
                    sSql += "   AND f6.slipcd = :slipcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += " ORDER BY tkdt, bcno, sort1, sort2"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 재검리스트
        Public Shared Function fnGet_ReTest_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String) As DataTable

            Dim sFn As String = "fnGet_ReTest_List"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT a.*, CASE WHEN b.rstno = 1 THEN b.viewrst ELSE NULL END rst1,"
                sSql += "            CASE WHEN b.rstno = 2 THEN b.viewrst ELSE NULL END rst2,"
                sSql += "            CASE WHEN b.rstno = 3 THEN b.viewrst ELSE NULL END rst3,"
                sSql += "            CASE WHEN b.rstno = 4 THEN b.viewrst ELSE NULL END rst4,"
                sSql += "            CASE WHEN b.rstno = 5 THEN b.viewrst ELSE NULL END rst5,"
                'sSql += "       fn_ack_get_slip_dispseq(a.partcd, a.slipcd, fn_ack_sysdate) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = a.partcd AND slipcd = a.slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1,"
                sSql += "       a.dispseql sort2"
                sSql += "  FROM (SELECT r.*, f6.tcdgbn, f6.tnmd tnms,"
                sSql += "               fn_ack_date_str(j1.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "               j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "               j.deptcd, FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno wardroom, fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "               CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo, "
                sSql += "               fn_ack_get_bcno_prt(r.bcno) prtbcno, f6.dispseql"
                sSql += "          FROM (SELECT bcno, regno, orgrst orgrst, viewrst,"
                sSql += "                       testcd, spccd, tclscd, fn_ack_date_str(tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "                       fn_ack_get_usr_name(tkid) tkid, fn_ack_date_str(rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "                       fn_ack_get_usr_name(CASE WHEN rstflg = '3' THEN fnid WHEN rstflg = '2' THEN mwid ELSE regid END) regid, partcd, slipcd"
                sSql += "                  FROM rr010m"
                sSql += "                 WHERE NVL(rerunflg, '0') > '0'"
                sSql += "                   AND rstdt >= :dates"
                sSql += "                   AND rstdt <= :datee || '235959'"
                sSql += "              ) r,"
                sSql += "              rf060m f6, rj011m j1, rj010m j"
                sSql += "        WHERE j.bcno    = j1.bcno "
                sSql += "          AND j1.bcno   = r.bcno "
                sSql += "          AND j1.tclscd = r.tclscd"
                sSql += "          AND j1.spccd  = r.spccd "
                sSql += "          AND r.testcd  = f6.testcd"
                sSql += "          AND r.spccd   = f6.spccd"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then
                    sSql += "          AND r.partcd = :partcd"
                    sSql += "          AND r.slipcd = :slipcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If
                sSql += "          AND (f6.usdt <= r.tkdt AND f6.uedt > r.tkdt)"
                sSql += "       ) a LEFT OUTER JOIN"
                sSql += "       (SELECT bcno, testcd, spccd, viewrst, RANK() OVER (PARTITION BY bcno, testcd ORDER BY sysdt) rstno FROM rr011m"
                sSql += "       ) b ON (a.bcno = b.bcno AND a.testcd = b.testcd)"
                sSql += " WHERE (a.tcdgbn IN ('B', 'S', 'P') OR (a.tcdgbn = 'C' AND NVL(a.viewrst, ' ') <> ' '))"
                sSql += " ORDER BY a.rstdt, a.orddt, a.bcno, sort1, sort2"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 재검통계
        Public Shared Function fnGet_ReTest_Statistics(ByVal ra_sDMY As String(), ByVal rsDateS As String, ByVal rsDateE As String, _
                                                       Optional ByVal rsPartSlip As String = "") As DataTable

            Dim sFn As String = "fnGet_ReTest_Statistics(String(),String,String,[String])"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT re.testcd, tnmd, re.rstdt , COUNT (re.testcd) recnt, r.cnt totcnt "
                sSql += "  FROM rj010m j,"
                sSql += "       (SELECT bcno, testcd, fn_ack_date_str(rstdt, 'yyyy-mm-dd') rstdt, partcd, slipcd"
                sSql += "         FROM rr010m"
                sSql += "        WHERE NVL(rerunflg, ' ') <> ' '"
                sSql += "          AND rstdt >= :dates"
                sSql += "          AND rstdt <= :datee || '235959'"
                sSql += "       ) re,"
                sSql += "       (SELECT fn_ack_date_str(rstdt, 'yyyy-mm-dd') rstdt, testcd, count(*) cnt"
                sSql += "          FROM rr010m"
                sSql += "         WHERE rstdt >= :dates"
                sSql += "           AND rstdt <= :datee || '235959'"
                sSql += "         GROUP BY fn_ack_date_str(rstdt, 'yyyy-mm-dd'), testcd"
                sSql += "       ) r,"
                sSql += "       rf060m f"
                sSql += " WHERE j.bcno    = re.bcno"
                sSql += "   AND re.testcd = r.testcd"
                sSql += "   AND re.rstdt  = r.rstdt"
                sSql += "   AND re.testcd = f.testcd"
                sSql += "   AND re.spccd  = f.spccd"
                sSql += "   AND (f.usdt  <= j.bcprtdt AND f.uedt > j.bcprtdt)"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then
                    sSql += "   AND re.partcd = :partcd"
                    sSql += "   AND re.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += " GROUP BY re.testcd,  f.tnmd, r.cnt, re.rstdt"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 최종보고 수정 리스트 조회
        Public Shared Function fnGet_FnModify_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String) As DataTable
            Dim sFn As String = "fnGet_FnModify_List"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT r.regno, j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN '입원' ELSE '외래' END iogbn, FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd,"
                sSql += "       r.testcd, f6.tnmd, r.spccd, fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       r1.viewrst prerst, fn_ack_get_usr_name(r1.fnid) prefnid, fn_ack_date_str(r1.fndt,'yyyy-mm-dd hh24:mi') prefndt,"
                sSql += "       r.viewrst, fn_ack_get_usr_name(r.fndt) fnid, fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, r52.cmtcont"
                sSql += "  FROM rj010m j, rr010m r, rf060m F6, rr011m r1, rr052m r52"
                sSql += " WHERE r.tkdt    BETWEEN :dates AND :datee || '235959'"
                sSql += "   AND r1.rstflg = '3'"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.bcno    = r1.bcno"
                sSql += "   AND r.testcd  = r1.testcd"
                sSql += "   AND r.spccd   = r1.spccd"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.spccd   = f6.spccd"
                sSql += "   AND (f6.usdt <= r.tkdt AND f6.uedt > r.tkdt)"
                sSql += "   AND r1.bcno   = r52.bcno (+)"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then
                    sSql += "   AND r.partcd = :partcd"
                    sSql += "   AND r.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))

                End If


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

#End Region

#Region "TAT 조회"
    Public Class TatFn
        Private Const msFile As String = "File : CGRISAPP_S.vb, Class : RISAPP.APP_S.TatFn" + vbTab

        Public Shared Function fnGet_TatList(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsQryGbn As String, _
                                              ByVal rbOverTime As Boolean, ByVal rsTestcds As String, Optional ByVal rsSlipCd As String = "", Optional ByVal rsEmerYN As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_TatList"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                If rsQryGbn = "" Then
                    '결과단위 TAT

                    sSql += "SELECT f6.partcd, r.bcno,"
                    sSql += "       f6.testcd, j.regno, j.statgbn, j.iogbn, j.deptcd, j.wardno,"
                    sSql += "       j.patnm, j.sex || '/' || j.age sa,"
                    sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, fn_ack_get_ward_abbr(j.wardno) || '/' || j.roomno ws,"
                    sSql += "       f6.tnmd, f6.spccd, f3.spcnmd,"
                    sSql += "       fn_ack_date_str(j1.orgorddt, 'yyyy-mm-dd hh24:mi') orddt,"
                    sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt,"
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                    sSql += "       fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi') mwdt,"
                    sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt,"
                    sSql += "       fn_ack_date_diff(j1.orgorddt, j1.colldt, '1') t1,"
                    sSql += "       fn_ack_date_diff(j1.colldt, r.tkdt, '1') t2,"
                    sSql += "       fn_ack_date_diff(r.tkdt, r.mwdt, '1') tat1,"
                    sSql += "       fn_ack_date_diff(r.mwdt, r.fndt, '1') tat2,"
                    sSql += "       fn_ack_date_diff(r.tkdt , r.fndt , '1') tat3,"
                    sSql += "       fn_ack_date_diff(j1.orgorddt , r.fndt , '1') tot,"
                    sSql += "       fn_ack_date_diff(r.tkdt, r.mwdt, '3') tat1_mi,"
                    sSql += "       fn_ack_date_diff(r.tkdt, r.fndt, '3') tat2_mi, f6.prptmi prptmi,"
                    sSql += "       f6.frptmi, fn_ack_date_str(r.tkdt, 'yyyymmdd') tkdt_m, r.workno, f6.partcd || f6.slipcd slipcd, f2.dispseq sort_slip,"
                    sSql += "       '[' || r51.cmtcd || '] ' || r51.cmtcont cmtcont, f2.dispseq sort_slip, f6.dispseql sort_test"
                    sSql += "  FROM rf060m f6, rj010m j, rj011m j1,lf030m f3, rf021m f2,"
                    sSql += "       ("
                    sSql += "        SELECT bcno, tclscd, testcd, spccd, tkdt, mwdt, fndt, wkymd || NVL(wkgrpcd, '') || NVL(wkno, '') workno"
                    sSql += "          FROM rr010m"
                    sSql += "         WHERE tkdt >= :dates"
                    sSql += "           AND tkdt <= :datee || '235959'"
                    sSql += "           AND (NVL(mwdt, ' ') <> ' ' OR NVL(fndt, ' ') <> ' ')"
                    sSql += "       ) r LEFT OUTER JOIN"
                    sSql += "       rr051m r51 ON (r.bcno = r51.bcno AND r.testcd = r51.testcd)"
                    sSql += " WHERE f6.testcd  = r.testcd"
                    sSql += "   AND f6.spccd   = r.spccd"
                    sSql += "   AND f6.usdt   <= r.tkdt"
                    sSql += "   AND f6.uedt   >  r.tkdt"
                    sSql += "   AND f6.spccd   = f3.spccd"
                    sSql += "   AND f3.usdt   <= r.tkdt"
                    sSql += "   AND f3.uedt   >  r.tkdt"
                    sSql += "   AND f6.partcd = f2.partcd"
                    sSql += "   AND f6.slipcd = f2.slipcd"
                    sSql += "   AND f2.usdt  <= r.tkdt"
                    sSql += "   AND f2.uedt  >  r.tkdt"
                    sSql += "   AND f6.tcdgbn IN ('S', 'P')"
                    sSql += "   AND NVL(f6.tatyn, '0') = '1'"
                    sSql += "   AND j.bcno = j1.bcno"
                    sSql += "   AND j1.bcno = r.bcno"
                    sSql += "   AND j1.tclscd = r.tclscd"
                Else
                    '처방단위 TAT
                    sSql = ""
                    sSql += "SELECT f6.partcd, r.bcno,"
                    sSql += "       f6.testcd, j.regno, j.statgbn, j.iogbn, j.deptcd, j.wardno,"
                    sSql += "       j.patnm, j.sex || '/' || j.age sa, "
                    sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.wardno || '/' || j.roomno ws,"
                    sSql += "       f6.tnmd, f6.spccd, f3.spcnmd,"
                    sSql += "       fn_ack_date_str(r.orgorddt, 'yyyy-mm-dd hh24:mi') orddt,"
                    sSql += "       fn_ack_date_str(r.colldt, 'yyyy-mm-dd hh24:mi') colldt,"
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                    sSql += "       fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi') mwdt,"
                    sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt,"
                    sSql += "       fn_ack_date_diff(r.orgorddt, r.colldt, '1') t1,"
                    sSql += "       fn_ack_date_diff(r.colldt, r.tkdt, '1') t2,"
                    sSql += "       fn_ack_date_diff(r.tkdt, r.mwdt, '1') tat1,"
                    sSql += "       fn_ack_date_diff(r.mwdt, r.fndt, '1') tat2,"
                    sSql += "       fn_ack_date_diff(r.tkdt , r.fndt , '1') tat3,"
                    sSql += "       fn_ack_date_diff(r.orgorddt , r.fndt , '1') tot,"
                    sSql += "       fn_ack_date_diff(r.tkdt, r.mwdt, '3') tat1_mi,"
                    sSql += "       fn_ack_date_diff(r.tkdt, r.fndt, '3') tat2_mi, f6.prptmi prptmi,"
                    sSql += "       f6.frptmi, fn_ack_date_str(r.tkdt, 'yyyymmdd') tkdt_m, r.workno, f6.partcd || f6.slipcd slipcd,"
                    sSql += "       '[' || r51.cmtcd || '] ' || r51.cmtcont cmtcont, f2.dispseq sort_slip, f6.dispseql sort_test"
                    sSql += "  FROM rf060m f6, rj010m j, lf030m f3, rf021m f2,"
                    sSql += "       ("
                    sSql += "        SELECT j0.bcno, j1.tclscd, j1.spccd, MIN(j1.orgorddt) orgorddt, MAX(j1.colldt) colldt, MIN(r.tkdt) tkdt, MAX(r.mwdt) mwdt, MAX(r.fndt) fndt, '' workno"
                    sSql += "          FROM rj010m j0, rj011m j1, rr010m r"
                    sSql += "         WHERE r.tkdt   >= :dates"
                    sSql += "           AND r.tkdt   <= :datee || '235959'"
                    sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')"
                    sSql += "           AND j1.bcno   = r.bcno"
                    sSql += "           AND j1.tclscd = r.tclscd"
                    sSql += "           AND j0.bcno   = j1.bcno"
                    sSql += "         GROUP BY j0.bcno, j1.tclscd, j1.spccd"
                    sSql += "       ) r LEFT OUTER JOIN"
                    sSql += "       rr051m r51 ON (r.bcno = r51.bcno AND r.tclscd = r51.testcd)"
                    sSql += " WHERE f6.testcd = r.tclscd"
                    sSql += "   AND f6.spccd  = r.spccd"
                    sSql += "   AND f6.usdt  <= r.tkdt"
                    sSql += "   AND f6.uedt  >  r.tkdt"
                    sSql += "   AND f6.spccd  = f3.spccd"
                    sSql += "   AND f3.usdt  <= r.tkdt "
                    sSql += "   AND f3.uedt  >  r.tkdt"
                    sSql += "   AND f6.partcd = f2.partcd"
                    sSql += "   AND f6.slipcd = f2.slipcd"
                    sSql += "   AND f2.usdt  <= r.tkdt"
                    sSql += "   AND f2.uedt  >  r.tkdt"
                    sSql += "   AND f6.tcdgbn IN ('S', 'P')"
                    sSql += "   AND NVL(f6.tatyn, '0') = '1'"
                    sSql += "   AND j.bcno    = r.bcno"

                End If

                If rsEmerYN = "Y" Then
                    sSql += "   AND NVL(j.statgbn, ' ') <> ' '"
                ElseIf rsEmerYN = "N" Then
                    sSql += "   AND NVL(j.statgbn, ' ') = ' '"
                End If

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rbOverTime Then
                    sSql += "   AND (fn_ack_date_diff(r.tkdt, r.mwdt, '3') > f6.prptmi OR fn_ack_date_diff(r.tkdt, r.fndt, '3') > f6.frptmi)"
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND f6.partcd = :partcd"
                    sSql += "   AND f6.slipcd = :slipcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If

                If rsTestcds <> "" Then
                    sSql += "   AND TRIM(f6.testcd) || TRIM(f6.spccd) IN (" + rsTestcds.Replace(" ", "") + ")"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class
#End Region

#Region "채혈/접수 관련"
    Public Class CollTkFn
        Private Const msFile As String = "File : CGRISAPP_S, Class : RISAPP.APP_S.CollTkFn" + vbTab

        ' 채혈/접수 대장
        Public Shared Function fnGet_CollTk_List(ByVal rsQryGbn As String, _
                                                 ByVal rsDateS As String, ByVal rsDateE As String, _
                                                 ByVal rsSlipCd As String, ByVal rsWGrpCd As String, ByVal rsTGrpCd As String, _
                                                 ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, _
                                                 ByVal rsRegNo As String, ByVal rsPatNm As String, ByVal rsWard As String, ByVal rsDeptCd As String, _
                                                 ByVal rbNoTk2 As Boolean) As DataTable
            Dim sFn As String = "fnGet_CollTk_List"

            Try

                Dim al As New ArrayList
                Dim sSql As String = ""

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.iogbn,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END dept,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM rj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       f3.spcnmd,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "          FROM rj011m a, rf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(j1.passdt, 'yyyy-mm-dd hh24:mi') passdt, j1.passid passnm,"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then
                    sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tknm, NULL workno"
                Else
                    sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, NULL tknm, NULL workno"
                End If
                sSql += "  FROM rj011m j1, lf030m f3, rj010m j"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then sSql += ", rr010m r"

                sSql += " WHERE j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"
                sSql += "   AND NVL(j1.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "')"

                If rsQryGbn = "2" Then
                    sSql += "   AND j1.colldt >= :dates"
                    sSql += "   AND j1.colldt <= :datee || '235959'"
                    sSql += "   AND j1.spcflg >= '2'"
                    sSql += "   AND j.spcflg  >= '2'"

                    al.Add(New OracleParameter("dates", rsDateS))
                    al.Add(New OracleParameter("datee", rsDateE))
                ElseIf rsQryGbn = "3" Then
                    sSql += "   AND j1.passdt >= :dates"
                    sSql += "   AND j1.passdt <= :datee || '235959'"
                    sSql += "   AND j1.spcflg >= '3'"
                    sSql += "   AND j.spcflg  >= '3'"

                    al.Add(New OracleParameter("dates", rsDateS))
                    al.Add(New OracleParameter("datee", rsDateE))
                Else
                    sSql += "   AND j1.tkdt   >= :dates"
                    sSql += "   AND j1.tkdt   <= :datee || '235959'"
                    sSql += "   AND j1.spcflg  = '4'"
                    sSql += "   AND j.spcflg   = '4'"

                    al.Add(New OracleParameter("dates", rsDateS))
                    al.Add(New OracleParameter("datee", rsDateE))
                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    al.Add(New OracleParameter("regno", rsRegNo))
                End If

                If rsPatNm <> "" Then
                    sSql += "   AND j.patnm LIKE :patnm ||'%'"
                    al.Add(New OracleParameter("patnm", rsPatNm))
                End If

                If rsWard <> "" Then
                    sSql += "   AND j.wardno = :wardno"
                    al.Add(New OracleParameter("wardno", rsWard))
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    al.Add(New OracleParameter("deptcd", rsDeptCd))
                End If

                If rsQryGbn = "4" Or rsQryGbn = "5" Then
                    sSql += "   AND j1.bcno   = r.bcno (+)"
                    sSql += "   AND j1.tclscd = r.tclscd (+)"

                    If rsTestCds <> "" Then
                        sSql += "   AND (j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "         r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"
                        sSql += "       )"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (r.testcd,   r.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                        sSql += "       )"

                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        
                    ElseIf rsSlipCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                        sSql += "        (r.testcd,   r.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) "
                        sSql += "       )"

                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                    End If


                    If rsWGrpCd <> "" Then
                        sSql += "           AND r.wkgrpcd = :wgrpcd"

                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                    End If

                    If rsRstFlg <> "" Then sSql += "   AND NVL(r.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "')"

                    If rsQryGbn = "5" Then
                        If rbNoTk2 Then sSql += "           AND NVL(r.wkymd, ' ') <> ' '"
                    Else
                        If rbNoTk2 Then sSql += "           AND NVL(r.wkymd, ' ') = ' '"
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If

                Else
                    If rsTestCds <> "" Then
                        sSql += "   AND j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "')"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                    ElseIf rsSlipCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN "
                        sSql += "       (SELECT testcd, spccd"
                        sSql += "          FROM rf060m"
                        sSql += "         WHERE partcd  = :partcd"
                        sSql += "           AND slipcd  = :slipcd"
                        sSql += "           AND usdt   <= j.bcprtdt"
                        sSql += "           AND uedt   >  j.bcprtdt"
                        sSql += "       )"

                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If
                End If

                Select Case rsQryGbn
                    Case "2" : sSql += " ORDER BY colldt, bcno"
                    Case "3" : sSql += " ORDER BY passdt, bcno"
                    Case "4" : sSql += " ORDER BY tkdt, bcno"
                    Case "5" : sSql += " ORDER BY workno, tkdt, bcno"
                End Select


                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 채혈/접수 건수
        Public Shared Function fnGet_CollTk_Statistics(ByVal rsQryGbn As String, _
                                                           ByVal rsDateS As String, ByVal rsDateE As String, _
                                                           ByVal rsSlipCd As String, ByVal rsWGrpCd As String, ByVal rsTGrpCd As String, _
                                                           ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, _
                                                           ByVal rsRegNo As String, ByVal rsPatNm As String, ByVal rsWard As String, ByVal rsDeptCd As String) As DataTable

            Dim sFn As String = "fnGet_CollTk_Statistics"

            Try

                Dim al As New ArrayList
                Dim sSql As String = ""

                sSql += "SELECT DISTINCT"
                sSql += "       j.bcno"
                sSql += "  FROM rj011m j1, lf030m f3, rj010m j"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then sSql += ", rr010m r"

                sSql += " WHERE j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"
                sSql += "   AND NVL(j1.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "')"

                If rsQryGbn = "2" Then
                    sSql += "   AND j1.colldt >= :dates"
                    sSql += "   AND j1.colldt <= :datee || '235959'"
                    sSql += "   AND j1.spcflg >= '2'"
                    sSql += "   AND j.spcflg  >= '2'"

                    al.Add(New OracleParameter("dates", rsDateS))
                    al.Add(New OracleParameter("datee", rsDateE))
                ElseIf rsQryGbn = "3" Then
                    sSql += "   AND j1.passdt >= :dates"
                    sSql += "   AND j1.passdt <= :datee || '235959'"
                    sSql += "   AND j1.spcflg >= '3'"
                    sSql += "   AND j.spcflg  >= '3'"

                    al.Add(New OracleParameter("dates", rsDateS))
                    al.Add(New OracleParameter("datee", rsDateE))
                Else
                    sSql += "   AND j1.tkdt   >= :dates"
                    sSql += "   AND j1.tkdt   <= :datee || '235959'"
                    sSql += "   AND j1.spcflg  = '4'"
                    sSql += "   AND j.spcflg   = '4'"

                    al.Add(New OracleParameter("dates", rsDateS))
                    al.Add(New OracleParameter("datee", rsDateE))
                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    al.Add(New OracleParameter("regno", rsRegNo))
                End If

                If rsPatNm <> "" Then
                    sSql += "   AND j.patnm LIKE :patnm ||'%'"
                    al.Add(New OracleParameter("patnm", rsPatNm))
                End If

                If rsWard <> "" Then
                    sSql += "   AND j.wardno = :wardno"
                    al.Add(New OracleParameter("wardno", rsWard))
                End If

                If rsDeptCd <> "" Then
                    sSql += "   AND j.deptcd = :deptcd"
                    al.Add(New OracleParameter("deptcd", rsDeptCd))
                End If

                If rsQryGbn = "4" Or rsQryGbn = "5" Then
                    sSql += "   AND j1.bcno   = r.bcno (+)"
                    sSql += "   AND j1.tclscd = r.tclscd (+)"

                    If rsTestCds <> "" Then
                        sSql += "   AND (j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "         r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"
                        sSql += "       )"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (r.testcd,   r.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                        sSql += "       )"

                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))

                    ElseIf rsSlipCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                        sSql += "        (r.testcd,   r.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) "
                        sSql += "       )"

                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                    End If


                    If rsWGrpCd <> "" Then
                        sSql += "           AND r.wkgrpcd = :wgrpcd"

                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                    End If

                    If rsRstFlg <> "" Then sSql += "   AND NVL(r.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "')"

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If

                Else
                    If rsTestCds <> "" Then
                        sSql += "   AND j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "')"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                    ElseIf rsSlipCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN "
                        sSql += "       (SELECT testcd, spccd"
                        sSql += "          FROM rf060m"
                        sSql += "         WHERE partcd  = :partcd"
                        sSql += "           AND slipcd  = :slipcd"
                        sSql += "           AND usdt   <= j.bcprtdt"
                        sSql += "           AND uedt   >  j.bcprtdt"
                        sSql += "       )"

                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If
                End If

                sSql += " GROUP BY j.bcno"

                DbCommand()

                Dim dt As New DataTable

                dt = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 채혈통계
        Public Shared Function fnGet_Collect_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIOGbn As String, _
                                          ByVal rbDetailGbn As Boolean, ByVal rsDeptWards As String, ByVal rsCollIds As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Collect_List(String, String, String, Boolean, String, String) As DataTable"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsDateS = rsDateS.Replace("-", "")
                rsDateE = rsDateE.Replace("-", "")

                sSql += "SELECT DISTINCT"
                sSql += "       j1.colldt, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd,"
                sSql += "       j.iogbn, CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE '' END wardroom,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.bcno,"
                sSql += "       j1.colldt_sort, j1.tubenmd, fn_ack_get_usr_name(j1.collid) collnm, j1.tubecd, j1.collid"
                sSql += "  FROM rj010m j,"
                sSql += "       ("
                sSql += "        SELECT fn_ack_date_str(j.colldt, 'yyyy-mm-dd hh24:mi') colldt, j.bcno, fn_ack_date_str(j.colldt, 'yyyy-mm-dd') colldt_sort,"
                sSql += "               f4.tubenmd, f4.tubecd, j.collid"
                sSql += "          FROM rj011m j, rf060m f6, lf040m f4"
                sSql += "         WHERE j.colldt >= :dates"
                sSql += "           AND j.colldt <= :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                sSql += "           AND j.tclscd   = f6.testcd"
                sSql += " 		    AND j.spccd    = f6.spccd"
                sSql += " 		    AND f6.usdt   <= j.colldt"
                sSql += " 		    AND f6.uedt   >  j.colldt"
                sSql += " 		    AND f6.tubecd  = f4.tubecd"
                sSql += " 		    AND f4.usdt   <= j.colldt"
                sSql += " 		    AND f4.uedt   >  j.colldt"
                sSql += "           AND f4.tubecd > '00'"
                If rsCollIds <> "" Then
                    sSql += "           AND j.collid IN ('" + rsCollIds.Replace(",", "','") + "')"
                End If
                sSql += "         GROUP BY j.colldt, j.bcno, f4.tubenmd, f4.tubecd, j.collid"
                sSql += "       ) j1"
                sSql += " WHERE j.bcno = j1.bcno"

                '>
                If rsIOGbn = "O" Then
                    ' 외래
                    sSql += "   AND j.iogbn <> 'I'"

                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            ' 제외
                            sSql += "   AND j.deptcd NOT IN ('" + rsDeptWards.Replace(",", "','") + "') "
                        Else
                            ' 포함
                            sSql += "   AND j.deptcd IN ('" + rsDeptWards.Replace(",", "','") + "') "
                        End If
                    End If

                ElseIf rsIOGbn = "I" Then
                    ' 입원
                    sSql += "   AND j.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIOGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIOGbn))

                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            ' 제외
                            sSql += "   AND j.wardno NOT IN ('" + rsDeptWards.Replace(",", "','") + "') "
                        Else
                            ' 포함
                            sSql += "   AND j.wardno IN ('" + rsDeptWards.Replace(",", "','") + "') "
                        End If
                    End If
                End If
                sSql += " ORDER BY colldt, regno, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '--채혈/접수 취소 리스트
        Public Shared Function fnGet_CollTk_Cancel_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIoGbn As String, _
                                                        ByVal rsCancelGbn As String, ByVal rbDetailGbn As Boolean, ByVal rsDeptWards As String, _
                                                        Optional ByVal rsPartSlip As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CollTk_Cancel_List(String, String, String, String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(j3.canceldt, 'yyyy-mm-dd hh24:mi:ss') deldt,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tknm,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.deptcd,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       j.wardno || '/' || j.roomno wardroom, j.iogbn,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j3.cancelcmt, j3.cancelcd, j3.cancelgbn,"
                sSql += "       fn_ack_get_usr_name(j3.cancelid) cancelnm,"
                'sSql += "       fn_ack_get_test_name_list(j.bcno) tnm"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "          FROM rj011m a, rf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnm"
                sSql += "  FROM rj030m j3, rj010m j,"
                sSql += "       (SELECT bcno, MAX(colldt) colldt, MAX(collid) collid"
                sSql += "          FROM rj011h"
                sSql += "         WHERE bcno IN (SELECT bcno FROM rj030m WHERE canceldt >= :dates AND canceldt <= :datee || '235959')"
                sSql += "         GROUP BY bcno"
                sSql += "       ) j1,"
                sSql += "       (SELECT bcno, MAX(tkdt) tkdt, MAX(tkid) tkid FROM rr010h"
                sSql += "         WHERE bcno IN (SELECT bcno FROM lj030m WHERE canceldt >= :dates AND canceldt <= :datee || '235959')"
                sSql += "         GROUP BY bcno"
                sSql += "       ) r"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                sSql += " WHERE j3.canceldt  >= :dates"
                sSql += "   AND j3.canceldt  <= :datee || '235959'"
                sSql += "   AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"
                sSql += "   AND j3.bcno       = j.bcno"
                sSql += "   AND j.bcno        = j1.bcno"
                sSql += "   AND j1.bcno       = r.bcno (+)"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsIoGbn <> "" Then
                    If rsIoGbn = "O" Then
                        sSql += "   AND j.iogbn <> 'I'"
                    Else
                        sSql += "   AND j.iogbn = :iogbn"
                        alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
                    End If
                    If rsDeptWards <> "" Then
                        If rsIoGbn = "I" Then
                            sSql += "   AND j.wardno " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "   AND j.deptcd " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                End If

                If rsPartSlip <> "" Then
                    sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd || slipcd = :partslip)"
                    alParm.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                End If

                sSql += " ORDER BY deldt, orddt, regno, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 채혈/접수 취소 건수
        Public Shared Function fnGet_CollTk_Cancel_Statistics(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIOGBN As String, _
                                                              ByVal rsCancelGbn As String, ByVal rbDetailGbn As Boolean, ByVal rsDeptWards As String, _
                                                              Optional ByVal rsSlipCd As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CollTk_Cancel_List(String, String, String, String, boolean, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT canceldt, cancelcd, cancelcmt, COUNT(bcno) cnt"
                sSql += "  FROM ("
                sSql += "        SELECT j3.cancelcmt, j3.cancelcd, fn_ack_date_str(j3.canceldt, 'yyyy-mm') canceldt, j3.bcno"
                sSql += "          FROM rj030m j3, rj010m j"
                sSql += "         WHERE j3.bcno = j.bcno"
                sSql += "           AND j3.canceldt >= :dates"
                sSql += "           AND j3.canceldt <= :datee || '235959'"
                sSql += "           AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsIOGBN = "I" Then
                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            sSql += "           AND j.wardno NOT IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "           AND j.wardno IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                ElseIf rsIOGBN = "O" Then
                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            sSql += "           AND j.deptcd NOT IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "           AND j.deptcd IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                End If


                If rsSlipCd <> "" Then
                    sSql += "           AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd || slipcd = :partslip)"
                    alParm.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If

                sSql += "       ) a"

                sSql += " GROUP BY cancelcmt, cancelcd, canceldt"


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- Reject 리스트
        Public Shared Function fnGet_Reject_Rstval(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_Reject_Rstval(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       f.tnmd, r.testcd, r.orgrst, r.viewrst, r.rstcmt, r.rstflg,"
                sSql += "       r.hlmark, r.panicmark, r.deltamark, r.alertmark, r.criticalmark, f.tcdgbn, r.tclscd,"
                sSql += "       fn_ack_get_test_reftxt(f.refgbn, j.sex, re.reflms, re..reflm, re.refhms, re..refhm, re.reflfs, re.reflf, re.refhfs, re.refhf, re.reflt) reftxt,"
                sSql += "       fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(r.regid) regnm,"
                sSql += "       fn_ack_date_str(r.mwdt,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(r.mwid)  mwnm,"
                sSql += "       fn_ack_date_str(r.fndt,  'yyyy-mm-dd hh24:mi:ss') fndt,  fn_ack_get_usr_name(r.fnid)  fnnm,"
                sSql += "       fn_ack_date_str(r.moddt, 'yyyy-mm-dd hh24:mi') canceldt, fn_ack_get_usr_name(r.modid) cancelnm,"
                sSql += "       f.partcd, f.slipcd,"
                'sSql += "       fn_ack_get_slip_dispseq(f.partcd, f.slipcd) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f.partcd AND slipcd = f.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd) sort2,"
                sSql += "       (SELECT dispseql FROM rf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                sSql += "       NVL(f.dispseql, 999) sort3,"
                sSql += "  FROM rj010m j, rr010h r, rf060m f,"
                sSql += "       (SELECT DISTINCT f61.*"
                sSql += "          FROM rj010m j, rr010m r, rf060m f6, rf061m f61"
                sSql += "         WHERE j.bcno    = :bcno"
                sSql += "           AND j.bcno    = r.bcno"
                sSql += "           AND r.testcd  = f6.testcd"
                sSql += "           AND r.spccd   = f6.spccd"
                sSql += "           AND r.tkdt   >= f6.usdt"
                sSql += "           AND r.tkdt   <  f6.uedt"
                sSql += "           AND f6.testcd = f61.testcd"
                sSql += "           AND f6.spccd  = f61.spccd"
                sSql += "           AND f6.usdt   = f61.usdt"
                sSql += "           AND ROUND(f61.sagec * 365) + f61.sages * 0.1 <= j.dage"
                sSql += "           AND j.dage <= ROUND(f61.eagec * 365) - f61.eages * 0.1"
                sSql += "       ) re"
                sSql += " WHERE j.bcno   = :bcno"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   AND r.tkdt  <  f.uedt"
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd"
                sSql += "   AND r.testcd = re.testcd (+)"
                sSql += "   AND r.spccd  = re.spccd (+)"
                sSql += " ORDER BY canceldt, sort1, f.partcd, f.slipcd, sort2, r.tclscd, sort3, f.testcd"


                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class


#End Region

#Region " 환자/검체 현황 조회 "

    Public Class PatHisFn
        Private Const msFile As String = "File : CGRISAPP_S, Class : RISAPP.APP_S.PatHisFn" + vbTab

        ' 미접수 조회
        Public Shared Function fnGet_NotTk_PatList(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIoGbn As String, _
                                                   Optional ByVal rsWard As String = "", _
                                                   Optional ByVal rsPartSlip As String = "", Optional ByVal rsTGrpCd As String = "", _
                                                   Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_NotTk_List(string, string, string, [string], [string], [string], [string]) As DataTable"

            Dim sSql As String = ""
            Dim al As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       j.iogbn, fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       j.deptcd, fn_ack_get_dr_name(j.doctorcd) doctornm, j.wardno || '/' || j.roomno wardroom, j.bedno, j.entdt, j.resdt,"
                sSql += "       fn_ack_date_str(NVL(j1.colldt, j.bcprtdt), 'yyyy-mm-dd') colldt,"
                sSql += "       NULL tkdt, j.spcflg, j.owngbn"
                sSql += "  FROM rj010m j, rj011m j1, rf060m f"
                sSql += " WHERE (j.bcprtdt BETWEEN :dates AND :datee || '235959' OR j1.colldt BETWEEN :dates AND :datee || '235959')"
                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j1.tclscd  = f.testcd"
                sSql += "   AND j1.spccd   = f.spccd"
                sSql += "   AND j.bcprtdt >= f.usdt"
                sSql += "   AND j.bcprtdt <  f.uedt"
                sSql += "   AND NVL(j1.spcflg, '0') > '0'"
                sSql += "   AND NVL(j1.spcflg, '0') < '4'"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsIoGbn = "외래" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIoGbn = "입원" Then
                    sSql += "   AND j.iogbn = 'I'"

                    '  입원일경우 병동구분
                    If rsWard <> "" Then
                        sSql += "   AND j.wordno = :wardno"
                        al.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If
                End If

                If rsTGrpCd <> "" Then
                    sSql += "   AND (f.testcd, f.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "  AND f.partcd = :partcd"
                    sSql += "  AND f.slipcd = :slipcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If


                sSql += " ORDER BY orddt, regno"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 미보고 조회
        Public Shared Function fnGet_NotRst_PatList(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIoGbn As String, _
                                                Optional ByVal rsWard As String = "", _
                                                Optional ByVal rsPartSlip As String = "", Optional ByVal rsWkGrpCd As String = "", Optional ByVal rsTGrpCd As String = "", _
                                                Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "Public Shared Function FGS04_Query2(String, String, string, [string], [string], [string], [string]) As DataTable"

            Dim sSql As String = ""
            Dim al As New ArrayList

            Try

                sSql += "SELECT DISTINCT"
                sSql += "       j.iogbn, fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       j.deptcd, fn_ack_get_dr_name(j.doctorcd) doctornm, j.wardno || '/' || j.roomno wardroom,"
                sSql += "       j.bedno, j.entdt, j.resdt,"
                sSql += "       NULL colldt, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd') tkdt, j.spcflg, j.owngbn"
                sSql += "  FROM rj010m j,"
                sSql += "       (SELECT r.bcno, r.tkdt FROM rr010m r, rf060m f"
                sSql += "         WHERE r.tkdt >= :dates"
                sSql += "           AND r.tkdt <= :datee || '235959'"
                sSql += "           AND NVL(r.rstflg, '0') <= '2'"
                sSql += "           AND f.titleyn = '0'"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "           AND r.tkdt  >= f.usdt"
                sSql += "           AND r.tkdt  <  f.uedt"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsTGrpCd <> "" Then
                    sSql += "           AND (f.testcd, f.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "           AND f.partcd = :partcd"
                    sSql += "           AND f.slipcd = :slipccd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsWkGrpCd <> "" Then
                    sSql += "           AND r.wkgrpcd = :wkgrp"
                    al.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                End If

                sSql += "       ) r"
                sSql += " WHERE j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"


                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsIoGbn = "외래" Then
                    sSql += "   AND j.iogbn <> 'I'"
                ElseIf rsIoGbn = "입원" Then
                    sSql += "   AND j.iogbn = 'I'"
                    '  입원일경우 병동구분
                    If rsWard <> "" Then
                        sSql += "   AND j.wardno = :wardno"
                        al.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If
                End If

                sSql += " ORDER BY tkdt, regno"


                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 상세리스트(채혈)
        Public Shared Function fnGet_Coll_TestList(ByVal rsDate As String, ByVal rsRegNo As String, ByVal rsIoGbn As String, ByVal rsOwnGbn As String, _
                                                       Optional ByVal rsBcNo As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Coll_TestList"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.spcflg,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward,"
                sSql += "       j.deptcd, j.doctorcd, fn_ack_get_dr_name(j.doctorcd) doctornm, f6.tnmd tnmd,"
                sSql += "       f6.spccd, f3.spcnmd, j.bcno, '' workno, '' rstflg,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt,"
                sSql += "       NULL tkdt, NULL rstdt,"
                sSql += "       f6.partcd ||f6.slipcd partslip, f6.bcclscd, j.statgbn, j.iogbn, j1.fkocs, f6.tubecd,"
                sSql += "       j.owngbn, f6.testcd, f6.tordcd,"
                sSql += "       '' append_yn, NVL(f6.exlabyn, '0') exlabyn,"
                sSql += "       NVL(f6.seqtyn, '0') seqtyn, f6.seqtmi,"
                sSql += "       CASE WHEN f6.dbltseq = '2' THEN '0' ELSE f6.dbltseq END dbltseq_sort,"
                sSql += "       RPAD(f6.testcd, 7, ' ') || f6.spccd testspc,"
                sSql += "       '' wkgrpcd"
                sSql += "  FROM rj010m j, rj011m j1, rf060m f6, lf030m f3"
                sSql += " WHERE j.regno   = :regno"
                sSql += "   AND j.iogbn   = :iogbn"
                sSql += "   AND j.owngbn  = :owngbn"
                sSql += "   AND j.bcno    = j1.bcno"
                sSql += "   AND (j.bcprtdt BETWEEN :dates AND :datee || '235959' OR j1.colldt BETWEEN :dates AND :datee || '235959')"
                sSql += "   AND j1.tclscd  = f6.testcd"
                sSql += "   AND j1.spccd   = f6.spccd"
                sSql += "   AND f6.usdt   <= j.bcprtdt"
                sSql += "   AND f6.uedt   >  j.bcprtdt"
                sSql += "   AND f6.spccd   = f3.spccd"
                sSql += "   AND f3.usdt   <= j.bcprtdt"
                sSql += "   AND f3.uedt   >  j.bcprtdt"
                sSql += "   AND f6.tubecd > '00'"
                sSql += "   AND f6.tcdgbn IN ('G', 'P', 'B', 'S')"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
                al.Add(New OracleParameter("owngbn", OracleDbType.Varchar2, rsOwnGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOwnGbn))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                If rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, al)
                '>

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 상세리스트(접수)
        Public Shared Function fnGet_Tk_TestList(ByVal rsDate As String, ByVal rsRegNo As String, _
                                                 ByVal rsIoGbn As String, ByVal rsOwnGbn As String, _
                                                 Optional ByVal rsBcNo As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Tk_TestList"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.spcflg,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward,"
                sSql += "       j.deptcd, fn_ack_get_dr_name(j.doctorcd) doctornm, f6.tnmd tnmd,"
                sSql += "       f6.spccd, f3.spcnmd, j.bcno, r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '') workno,"
                sSql += "       CASE WHEN minrstflg = maxrstflg THEN minrstflg"
                sSql += "            WHEN minrstflg = '0' AND maxrstflg > '0' THEN '1'"
                sSql += "            ELSE minrstflg"
                sSql += "       END rstflg,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       f6.partcd || f6.slipcd partslip, f6.bcclscd, j.statgbn, j.iogbn, j1.fkocs, f6.tubecd,"
                sSql += "       j.owngbn, j.deptcd, j.doctorcd, f6.testcd, f6.tordcd,"
                sSql += "       '' append_yn, NVL(f6.exlabyn, '0') exlabyn,"
                sSql += "       NVL(f6.seqtyn, '0') seqtyn, f6.seqtmi,"
                sSql += "       CASE WHEN f6.dbltseq = '2' THEN '0' ELSE f6.dbltseq END dbltseq_sort,"
                sSql += "       RPAD(f6.testcd, 7, ' ') || f6.spccd testspcd,"
                sSql += "       r.wkgrpcd"
                sSql += "  FROM rj010m j, rj011m j1, rf060m f6, lf030m f3,"
                sSql += "       ("
                sSql += "        SELECT bcno, tclscd, spccd, tkdt, wkymd, wkgrpcd, wkno, MIN(NVL(rstflg, '0')) minrstflg, MAX(NVL(rstflg, '0')) maxrstflg,"
                sSql += "               MAX(NVL(rstdt, '19000101')) rstdt"
                sSql += "          FROM rr010m "
                sSql += "         WHERE regno = :regno"
                sSql += "           AND tkdt >= :dates"
                sSql += "           AND tkdt <= :datee || '235959'"
                sSql += "         GROUP BY bcno, tclscd, spccd, tkdt, wkymd, wkgrpcd, wkno"
                sSql += "       ) r"
                sSql += " WHERE j.regno    = :regno"
                sSql += "   AND j.iogbn    = :iogbn"
                sSql += "   AND j.owngbn   = :owngbn"
                sSql += "   AND NVL(j.rstflg, '0') = '0'"
                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j1.bcno    = r.bcno"
                sSql += "   AND j1.tclscd  = r.tclscd"
                sSql += "   AND j1.tclscd  = f6.testcd"
                sSql += "   AND j1.spccd   = f6.spccd"
                sSql += "   AND f6.usdt   <= j.bcprtdt"
                sSql += "   AND f6.uedt   >  j.bcprtdt"
                sSql += "   AND f6.spccd   = f3.spccd"
                sSql += "   AND f3.usdt   <= j.bcprtdt"
                sSql += "   AND f3.uedt   >  j.bcprtdt"
                sSql += "   AND f6.tubecd > '00'"
                sSql += "   AND f6.tcdgbn IN ('G', 'P', 'B', 'S')"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
                al.Add(New OracleParameter("owngbn", OracleDbType.Varchar2, rsOwnGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOwnGbn))

                If rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, al)
                '>

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 해당검체 접수 유/무 조회
        Public Shared Function fnGet_BcNo_TkYn(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "Public Function fnGet_BcNo_TkYn(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT fn_ack_get_bcno_full(bcno) bcno, spcflg"
                sSql += "  FROM rj010m"
                sSql += " WHERE bcno    = :bcno"
                sSql += "   AND NVL(spcflg, '0') = '0'"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Shared Function fnGet_PatInfo_bcno(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_PatInfo_bcno(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       j.sex || '/' || j.age sexage, j.iogbn, j.owngbn,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt,"
                sSql += "       fn_ack_date_str(j.resdt, 'yyyy-mm-dd') resdt,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       j.deptcd, j.wardno || '/' || j.roomno wardroom,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno"
                sSql += "  FROM rj010m j"

                If rsBcNo.Length = 14 Then
                    sSql += " WHERE j.bcno >= :bcno || '0'"
                    sSql += "   AND j.bcno <= :bcno || '9'"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                Else
                    sSql += " WHERE j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                sSql += "   AND NVL(j.spcflg, '0') > '0'"
                sSql += " ORDER BY orddt DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

#End Region

#Region "WorkList 조회"
    Public Class WkFn
        Private Const msFile As String = "File : CGRISAPP_S, Class : RISAPP.APP_S.WkFn" + vbTab

        Public Shared Function fnGet_WorkList_WGrp(ByVal rsWkYmd As String, ByVal rsWGrpCd As String, ByVal rsWKNoS As String, ByVal rsWkNoE As String, _
                                                   ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, _
                                                   ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_WGrp"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || NVL(j.roomno, '') ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, r.tkdt as tkdtsort, "
                sSql += "       r.testcd, f6.tnmd, f6.tnmp, fn_ack_get_pat_befviewrst(r.bcno, r.testcd, r.spccd) bfviewrst,"
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM rj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       j3.diagnm, NULL wlseq, r.spccd, r.viewrst"
                sSql += "  FROM rr010m r, rf060m f6 , lf030m f3 , rj010m j"
                sSql += "       LEFT OUTER JOIN"
                sSql += "            rj013m j3  ON (j.bcno = j3.bcno)"
                sSql += " WHERE r.wkymd   = :wkymd"
                sSql += "   AND r.wkgrpcd = :wkgrp"
                sSql += "   AND r.wkno   >= :wknos"
                sSql += "   AND r.wkno   <= :wknoe"

                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWKNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWKNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"
                End If

                If rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND f6.usdt <= r.tkdt"
                sSql += "   AND f6.uedt >  r.tkdt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND f3.usdt <= r.tkdt"
                sSql += "   AND f3.uedt >  r.tkdt"
                sSql += "   AND ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))"

                Dim sWhere As String = ""

                If rsRstFlg.Substring(0, 1) = "1" Then sWhere = "NVL(r.rstflg, '0') = '0'"
                If rsRstFlg.Substring(1, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '1'"
                If rsRstFlg.Substring(2, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '2'"
                If rsRstFlg.Substring(3, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '3'"

                If sWhere <> "" Then
                    sSql += "   AND (" + sWhere + ")"
                End If

                sSql += " ORDER BY workno, tkdtsort, bcno" '<20150827 작업번호 순으로 조회


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_WorkList_TGrp(ByVal rsPartSlip As String, ByVal rsTGrpCd As String, ByVal rsDateS As String, ByVal rsDateE As String, _
                                                   ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, _
                                                   ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_TGrp"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm, j.sex || '/'|| j.age sexage,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, r.tkdt as tkdtsort, "
                sSql += "       r.testcd, f6.tnmd, f6.tnmp,"
                sSql += "       fn_ack_get_pat_befviewrst(r.bcno, r.testcd, r.spccd) bfviewrst,"
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM rj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       j3.diagnm, NULL wlseq, r.spccd, r.viewrst"
                sSql += "  FROM rr010m r, rf060m f6, lf030m f3, rj010m j,"
                sSql += "       rj013m j3"
                sSql += " WHERE r.tkdt >= :dates"
                sSql += "   AND r.tkdt <= :datee || '5959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"
                ElseIf rsTGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(r.testcd, 1, 5) || r.spccd) IN (SELECT SUBSTR(testcd, 1, 5) || spccd FROM rf065m WHERE tgrpcd = :tgrpcd)"

                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM rf060m WHERE partcd = :partcd AND slipcd = :slipcd) "
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND f6.usdt <= r.tkdt"
                sSql += "   AND f6.uedt >  r.tkdt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND f3.usdt <= r.tkdt"
                sSql += "   AND f3.uedt >  r.tkdt"
                sSql += "   AND j.bcno   = j3.bcno (+)"
                sSql += "   AND ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))"

                Dim sWhere As String = ""

                If rsRstFlg.Substring(0, 1) = "1" Then sWhere = "NVL(r.rstflg, '0') = '0'"
                If rsRstFlg.Substring(1, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '1'"
                If rsRstFlg.Substring(2, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '2'"
                If rsRstFlg.Substring(3, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '3'"

                If sWhere <> "" Then
                    sSql += "   AND (" + sWhere + ")"
                End If

                sSql += " ORDER BY workno,tkdtsort, bcno" '<20150827 작업번호 순으로 조회

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

    End Class

#End Region

End Namespace
