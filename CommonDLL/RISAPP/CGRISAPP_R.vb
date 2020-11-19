'*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGLISAPP_R.vb                                                          */
'/* PartName     : 결과관리 - 검체별/작업번호별 결과저장 및 보고                          */
'/* Description  : 결과관리의 Data Query구문관련 Class                                    */
'/* Design       :                                                                        */
'/* Coded        : 2003-07-10 Ju Jin Ho                                                   */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst

Namespace APP_R


    '-- 결과등록 공통
    Public Class RstFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.CommFn" + vbTab

        '-- 환자등록번호로 검사리스트 조회
        Public Shared Function fnGet_SpcList_Reg(ByVal rsRegNo As String, Optional ByVal rsPartSlip As String = "", _
                                                 Optional ByVal rsTkDtS As String = "", Optional ByVal rsTkDtE As String = "", Optional ByVal rbBankYn As Boolean = False) As DataTable
            Dim sFn As String = "Function fnGet_SpcList_Reg(String, [String], [String], [String]) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       '', SUBSTR(a.bcno, 1, 14) || '-' || SUBSTR(a.bcno, 15, 1) bcno,"
                sSql += "       a.regno, a.patnm,"
                sSql += "       fn_ack_date_str(b.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt"
                sSql += "  FROM rj010m a, rr010m b"
                sSql += " WHERE a.regno  = :regno"
                sSql += "   AND a.bcno   = b.bcno"
                sSql += "   AND a.spcflg = '4'"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                If rsPartSlip <> "" Then
                    sSql += "  AND b.partcd || b.slipcd = :partslip"
                    alParm.Add(New OracleParameter("partslip", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                End If

                If rsTkDtS <> "" Then
                    sSql += "   AND b.tkdt >= :dates || '000000'"
                    sSql += "   AND b.tkdt <= :datee || '235959'"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                End If

                If rbBankYn Then
                    sSql += "   AND b.partcd = :partcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, PRG_CONST.PART_BloodBank.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.PART_BloodBank))
                End If
                sSql += " ORDER BY tkdt DESC"


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 부서/분야별 검사리스트 조회
        Public Shared Function fnGet_SpcList_TK(ByVal rsPartSlip As String, ByVal rsTkDts As String, ByVal rsTkDte As String, ByVal rsEr As String) As DataTable
            Dim sFn As String = "fnGet_SpcList_TK()"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sWhere As String = ""

                rsTkDts = rsTkDts.Replace("-", "")
                rsTkDte = rsTkDte.Replace("-", "")

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:miss') tkdt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       j.regno,"
                sSql += "       j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptcd,"
                sSql += "       j.statgbn,"
                sSql += "       CASE WHEN (SELECT count(*) FROM rj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM rj010m j, rr010m r, rf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM DUAL) s"
                sSql += " WHERE r.tkdt   >= :dates"
                sSql += "   AND r.tkdt   <= :datee || '235959'"
                sSql += "   AND j.bcno    = r.bcno"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDts.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDts))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDte.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDte))

                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND f.partcd  = :partcd"

                alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                If rsPartSlip.Length > 1 Then
                    sSql += "   AND f.slipcd = :slipcd"
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND NVL(f.titleyn, '0') = '0'"
                sSql += "   AND ("
                sSql += "        CASE WHEN f.tcdgbn = 'C' THEN NVL (f.reqsub, '0') ELSE '1' END = '1' OR  NVL (r.orgrst, ' ') <> ' '"
                sSql += "       )"

                If rsEr <> "" Then sSql += "   AND NVL(j.statgbn, '0') <> '0'"

                sSql += " GROUP BY r.tkdt, j.bcno, j.regno, j.patnm, j.iogbn, j.wardno, j.roomno, j.deptcd, f.partcd, f.slipcd, j.statgbn, j.rstflg"

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 작업그룹별 검사리스트 조회
        Public Shared Function fnGet_SpcList_WGrp(ByVal rsWkGrpCd As String, ByVal rsWkYmd As String, ByVal rsWkYmdE As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                   ByVal rsEr As String, Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_SpcList_WGrp(String, String, String, String, String, String)"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       '작업번호' qrygbn,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       j.regno,"
                sSql += "       j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptcd,"
                sSql += "       j.statgbn,"
                sSql += "       CASE WHEN (SELECT count(*) FROM rj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM rj010m j, rr010m r, rf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM   DUAL) s"

                If rsWkYmdE <> "" Then
                    sSql += " WHERE r.wkymd   BETWEEN :dates AND :datee"
                    sSql += "   AND r.wkgrpcd = :wgrpcd"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsWkYmdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmdE))
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                Else
                    sSql += " WHERE r.wkymd   = :wkymd"
                    sSql += "   AND r.wkgrpcd = :wgrpcd"
                    sSql += "   AND r.wkno    BETWEEN :wknos AND :wknoe"

                    alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                    alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                    alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))
                End If
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND NVL(f.titleyn, '0') = '0'"
                sSql += "   AND ("
                sSql += "        CASE WHEN f.tcdgbn = 'C' THEN NVL (f.reqsub, '0') ELSE '1' END = '1' OR  NVL (r.orgrst, ' ') <> ' '"
                sSql += "       )"

                If rsEr <> "" Then sSql += "   AND NVL(j.statgbn, '0') <> '0'"

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                sSql += " GROUP BY r.tkdt, j.bcno, j.regno, j.patnm, j.iogbn, j.wardno, j.roomno, j.deptcd, j.statgbn, j.rstflg,"
                sSql += "          f.partcd, f.slipcd, r.wkymd, r.wkgrpcd, r.wkno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 장비별 검사리스트 조회
        Public Shared Function fnGet_SpcList_Eq(ByVal rsEqCd As String, ByVal rsRstDt As String, _
                                                ByVal rsEr As String, Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "SectionListSelect(String, String, String,  String, String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim strWhere As String = ""

                rsRstDt = rsRstDt.Replace("-", "")

                sSql += "SELECT DISTINCT"
                sSql += "       '검사장비' qrygbn, r.eqseqno, r.eqrack, r.eqpos, '' workno, j.regno, j.patnm,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno, j.statgbn,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptcd,"
                sSql += "       CASE WHEN (SELECT count(*) FROM rj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       MIN(NVL(r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                'sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       fn_ack_date_diff(MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (f.frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM rj010m j, rr010m r, rf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM DUAL) s"
                sSql += " WHERE r.eqcd   = :eqcd"
                sSql += "   AND r.rstdt >= :rstdt"
                sSql += "   AND r.rstdt <= :rstdt || '25959'"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND j.bcno   = r.bcno"

                alParm.Add(New OracleParameter("eqcd", OracleDbType.Varchar2, rsEqCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsEqCd))
                alParm.Add(New OracleParameter("rstdt", OracleDbType.Varchar2, rsRstDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDt))
                alParm.Add(New OracleParameter("rstdt", OracleDbType.Varchar2, rsRstDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDt))

                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND NVL(f.titleyn, '0') = '0'"
                sSql += "   AND ("
                sSql += "        CASE WHEN f.tcdgbn = 'C' THEN NVL (f.reqsub, '0') ELSE '1' END = '1' OR  NVL (r.orgrst, ' ') <> ' '"
                sSql += "       )"

                If rsEr <> "" Then sSql += "   AND NVL(j.statgbn, '0') <> '0'"

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                sSql += " GROUP BY r.eqseqno, r.eqrack, r.eqpos, r.tkdt, j.bcno, j.regno, j.patnm, j.iogbn, j.wardno, j.roomno, j.deptcd, j.statgbn" ', f.partcd, f.slipcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 검사그룹 검사리스트 조회  
        Public Shared Function fnGet_SpcList_TGrp(ByVal rsTGrpCds As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                                  ByVal rsEr As String, Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "fnGet_SpcList_TGrp(String, ..., string) As DataTable"
            Try

                Dim oFn As New Fn

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim strWhere As String = ""

                rsTkDtS = rsTkDtS.Replace("-", "")
                rsTkDtE = rsTkDtE.Replace("-", "")

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       '검사그룹' qrygbn,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       '' workno,"
                sSql += "       j.regno,"
                sSql += "       j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptcd,"
                sSql += "       j.statgbn,"
                sSql += "       CASE WHEN (SELECT count(*) FROM rj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       '' partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM rj010m j, rr010m r, rf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM   DUAL) s"
                sSql += " WHERE r.tkdt   >= :dates"
                sSql += "   AND r.tkdt   <= :datee || '235959'"
                sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM rf065m WHERE tgrpcd IN ('" + rsTGrpCds.Replace(",", "','") + "'))"
                sSql += "   AND j.bcno    = r.bcno"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND NVL(f.titleyn, '0') = '0'"
                sSql += "   AND ("
                sSql += "        CASE WHEN f.tcdgbn = 'C' THEN NVL (f.reqsub, '0') ELSE '1' END = '1' OR  NVL (r.orgrst, ' ') <> ' '"
                sSql += "       )"

                If rsEr <> "" Then sSql += "   AND NVL(j.statgbn, '0') <> '0'"

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                sSql += " GROUP BY r.tkdt, j.bcno, j.regno, j.patnm, j.iogbn, j.wardno, j.roomno, j.deptcd, j.statgbn, j.rstflg"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 검사항목별 검사리스트 조회
        Public Shared Function fnGet_SpcList_Test(ByVal rsTestCds As String, ByVal rsWkYmd As String, ByVal rsWkGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, ByVal rsRstNullReg As String, ByVal rsTkDtB As String, ByVal rsTkDtE As String, _
                                                  Optional ByVal rsBcno As String = "", Optional ByVal rsDoubleTest As String = "", _
                                                  Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_SpcList_Test(String, String, String, String, String, String, (String)) As DataTable"


            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, f3.spcnmd,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || '/' || FN_ACK_GET_WARD_ABBR(j.wardno) ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptcd,"
                sSql += "       NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd, f6.tcdgbn, f6.titleyn, f6.plgbn,"
                'sSql += "       r.orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno || '/' || j.roomno wardroom"
                sSql += "       r.viewrst as orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno || '/' || j.roomno wardroom"
                sSql += "       ,trim(r.HLMARK||r.DELTAMARK||r.PANICMARK||r.CRITICALMARK||r.ALERTMARK) judgmark "
                sSql += "  FROM rj010m j, rr010m r, rf060m f6, lf030m f3"

                If rsBcno <> "" Then
                    sSql += " WHERE j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                ElseIf rsWkYmd <> "" Then
                    sSql += " WHERE r.wkymd   = :wkymd"
                    sSql += "   AND r.wkgrpcd = :wkgrp"
                    sSql += "   AND r.wkno   >= :wknos"
                    sSql += "   AND r.wkno   <= :wknoe"

                    alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("wkgrp", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                    alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                    alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))
                Else
                    sSql += " WHERE r.tkdt >= :dates || '0000'"
                    sSql += "   AND r.tkdt <= :datee || '5959'"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtB.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtB))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                End If

                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt  <  f3.uedt"
                sSql += "   AND j.spcflg = '4'"

                Select Case rsRstNullReg
                    Case "000"

                    Case "001"
                        sSql += "   AND NVL(r.rstflg, '0') = '3'"
                    Case "010"
                        sSql += "   AND NVL(r.rstflg, '0') < '3'"
                    Case "011"
                        sSql += "   AND NVL(r.rstflg, '0') > '0'"
                    Case "100"
                        sSql += "   AND NVL(r.rstflg, '0') = '0'"
                    Case "101"
                        sSql += "   AND (NVL(r.rstflg, '0') = '0' OR NVL(r.rstflg, '0') = '3')"
                    Case "110"
                        sSql += "   AND (NVL(r.rstflg, '0') = '0' OR NVL(r.rstflg, '0') < '3')"
                    Case "111"

                End Select

                If rsTestCds <> "" Then sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"

                sSql += " ORDER BY workno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 해당 W/L에 속한 검사리스트 조회
        Public Shared Function fnGet_Test_wl(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String) As DataTable
            Dim sFn As String = "Function fnGet_Test_wl(String, String, String) As DataTable"


            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       w.testcd, w.spccd, f6.tnmd, f2.dispseq sort1, f6.dispseql sort2"
                sSql += "  FROM rrw11m w, rr010m r, rf060m f6, rf021m f2"
                sSql += " WHERE w.wluid   = :wluid"
                sSql += "   AND w.wlymd   = :wlymd"
                sSql += "   AND w.wltitle = :wltitle"
                sSql += "   AND w.bcno    = r.bcno"
                sSql += "   AND w.testcd  = r.testcd"
                sSql += "   AND w.testcd  = f6.testcd"
                sSql += "   AND w.spccd   = f6.spccd"
                sSql += "   AND w.regdt  >= f6.usdt"
                sSql += "   AND w.regdt  <  f6.uedt"
                sSql += "   AND f6.partcd = f2.partcd"
                sSql += "   AND f6.slipcd = f2.slipcd"
                sSql += "   AND w.regdt  >= f2.usdt"
                sSql += "   AND w.regdt  <  f2.uedt"
                sSql += " ORDER BY sort1, sort2, testcd"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- W/L별 검사리스트 조회(담당자별)
        Public Shared Function fnGet_SpcList_WL(ByVal rsWlUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, _
                                                  ByVal rsN As String, ByVal rsHL As String, ByVal rsPDC As String, ByVal rsA As String, ByVal rsEqFlag As String, ByVal rsReRun As String, _
                                                  ByVal rsEr As String, Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "fnGet_SpcList_WL(String, String, String, String, string, string, string, string) As DataTable"
            Try

                Dim oFn As New Fn

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim strWhere As String = ""

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       'W/L' qrygbn, w.wlseq workno, j.regno, j.patnm,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptcd, j.statgbn,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       CASE WHEN (SELECT count(*) FROM rj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM rrw11m w, rj010m j, rr010m r, rf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM   DUAL) s"
                sSql += " WHERE w.wluid   = :wluid "
                sSql += "   AND w.wlymd   = :wlymd"
                sSql += "   AND w.wltitle = :wltitle"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWlUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWlUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))

                sSql += "   AND w.bcno   = j.bcno"
                sSql += "   AND w.bcno   = r.bcno"
                sSql += "   AND w.testcd = r.testcd"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND j.spcflg  = '4'"
                sSql += "   AND j.owngbn <> 'H'"
                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"

                If rsEr <> "" Then sSql += "   AND NVL(j.statgbn, '0') <> '0'"

                If rsRegNo = "" Then

                    If rsReRun = "RERUN" Then
                        sSql += "   AND NVL(r.rerunflg, '0') = '1'"
                    ElseIf rsReRun = "NOTRERUN" Then
                        sSql += "   AND j.bcno NOT IN (SELECT w.bcno FROM rrw11m w, rr010m"
                        sSql += "                       WHERE w.wluid   = :wluid"
                        sSql += "                         AND w.wlymd   = :wlymd"
                        sSql += "                         AND w.wltitle = :wltitle"
                        sSql += "                         AND w.bcno    = r.bcno"
                        sSql += "                         AND w.testcd  = r.testcd"
                        sSql += "                         AND NVL(r.rerunflg, '0') = '1'"
                        sSql += "                     )"

                        alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWlUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWlUid))
                        alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                        alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))
                    End If

                    If rsN <> "" Then
                        sSql += "   AND j.bcno NOT IN (SELECT w.bcno FROM rrw11m w, rr010m"
                        sSql += "                       WHERE w.wluid   = :wluid"
                        sSql += "                         AND w.wlymd   = :wlymd"
                        sSql += "                         AND w.wltitle = :wltitle"
                        sSql += "                         AND w.bcno    = r.bcno"
                        sSql += "                         AND w.testcd  = r.testcd"
                        sSql += "                         AND NVL(r.hlmark, ' ') <> ' '"
                        sSql += "                     )"

                        alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWlUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWlUid))
                        alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                        alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))
                    End If

                    If rsHL <> "" Then sSql += " 	 AND NVL(r.hlmark, ' ') <> ' '"
                    If rsA <> "" Then sSql += "   AND NVL(r.alertmark, ' ') <> ' '"

                    If rsN.Length + rsHL.Length + rsPDC.Length + rsA.Length + rsEqFlag.Length > 0 Then
                        sSql += "   AND NVL(r.orgrst, ' ') <> ' '"
                    End If

                    If rsHL <> "" And rsPDC = "" Then
                        If rsEqFlag <> "" Then strWhere += "   AND NVL(r.eqflag, ' ') <> ' '"
                    Else
                        Dim strTmp As String = ""

                        If rsHL <> "" And rsPDC = "" Then
                        ElseIf rsPDC <> "" Then
                            strTmp += "r.panicmark = 'P' OR r.deltamark = 'D' OR r.criticalmark = 'C'"
                        End If

                        If rsEqFlag <> "" Then strTmp += IIf(strTmp = "", "", " OR ").ToString + "NVL(r.eqflag, ' ') <> ' '"

                        If strTmp <> "" Then strWhere += "   AND (" + strTmp + ")"
                    End If
                    If strWhere <> "" Then sSql += strWhere

                    If rsHL <> "" And rsPDC = "" Then
                        sSql += "   AND j.bcno NOT IN (SELECT w.bcno FROM rrw11m w, rr010m"
                        sSql += "                       WHERE w.wluid   = :wluid"
                        sSql += "                         AND w.wlymd   = :wlymd"
                        sSql += "                         AND w.wltitle = :wltitle"
                        sSql += "                         AND w.bcno    = r.bcno"
                        sSql += "                         AND w.testcd  = r.testcd"
                        sSql += "                         AND (panicmark = 'P' OR deltamark = 'D' OR criticalmark = 'C')"
                        sSql += "                     )"

                        alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWlUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWlUid))
                        alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                        alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))
                    End If
                Else
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                sSql += " GROUP BY w.wlseq, j.regno, j.patnm, j.bcno, r.tkdt, j.iogbn = 'I', j.wardno, j.roomno, j.deptcd, j.statgbn,"
                sSql += "       f.partcd, f.slipcd, j.rstflg"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- W/L별 검사리스트 조회(검사항목별)
        Public Shared Function fnGet_SpcList_WL(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String, ByVal rsRstNullReg As String) As DataTable
            Dim sFn As String = "Function fnGet_SpcList_WL(String, String, String) As DataTable"


            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       w.wlseq workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, f3.spcnmd,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || '/' || FN_ACK_GET_WARD_ABBR(j.wardno) ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptcd,"
                sSql += "       NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd, f6.tcdgbn, f6.titleyn, f6.plgbn,"
                'sSql += "       r.orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno || '/' || j.roomno wardroom"
                sSql += "       r.viewrst as orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno || '/' || j.roomno wardroom"
                sSql += "       ,trim(r.HLMARK||r.DELTAMARK||r.PANICMARK||r.CRITICALMARK||r.ALERTMARK) judgmark "
                sSql += "  FROM rrw11m w, rj010m j, rr010m r, rf060m f6, lf030m f3"
                sSql += " WHERE w.wluid   = :wluid"
                sSql += "   AND w.wlymd   = :wlymd"
                sSql += "   AND w.wltitle = :wltitle"
                sSql += "   AND w.bcno    = j.bcno"
                sSql += "   AND w.bcno    = r.bcno"
                sSql += "   AND w.testcd  = r.testcd"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt  <  f3.uedt"
                sSql += "   AND j.spcflg = '4'"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTitle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTitle))

                Select Case rsRstNullReg
                    Case "000"

                    Case "001"
                        sSql += "   AND NVL(r.rstflg, '0') = '3'"
                    Case "010"
                        sSql += "   AND NVL(r.rstflg, '0') < '3'"
                    Case "011"
                        sSql += "   AND NVL(r.rstflg, '0') > '0'"
                    Case "100"
                        sSql += "   AND NVL(r.rstflg, '0') = '0'"
                    Case "101"
                        sSql += "   AND (NVL(r.rstflg, '0') = '0' OR NVL(r.rstflg, '0') = '3')"
                    Case "110"
                        sSql += "   AND (NVL(r.rstflg, '0') = '0' OR NVL(r.rstflg, '0') < '3')"
                    Case "111"

                End Select

                sSql += " ORDER BY workno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 처방의 전화번호 가져오기(SMS 서비스 사용)
        Public Shared Function fnGet_SMS_DrInof(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_SMS_DrInof()  As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT j.deptcd, j.doctorcd, fn_ack_get_usr_telno(:usrid) telno"
                sSql += "  FROM rj010m j"
                sSql += " WHERE j.bcno = :bcno"

                alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 혈액은행 관련검사 조회
        Public Shared Function fnGet_BBTType_List() As DataTable
            Dim sFn As String = "Function fnGet_BBTType_List()  As DataTable"
            Dim sSql As String = ""

            Try
                sSql += "SELECT testcd, spccd, bbgbn"
                sSql += "  FROM lf140m"

                DbCommand()
                Return DbExecuteQuery(sSql)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- Alert Rule 정보
        Public Shared Function fnGet_Alert_Rule() As DataTable
            Dim sFn As String = "Get_Alert_Rule"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += " SELECT testcd, sex, deptcds, orgrst, viewrst, spccds, baccds, antic, eqflag"
                sSql += "   FROM rf180m"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '# 해당 검체번호의 결과일시, 사용자아이디, 사용자명 가져오기
        Public Shared Function fnGet_RstUsrInfo(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_RstUsrInfo(String) As DataTable"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT testcd, spccd, rstflg,"
                sSql += "       fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, regid, fn_ack_get_usr_name(regid) regnm,"
                sSql += "  	    fn_ack_date_str(mwdt, 'yyyy-mm-dd hh24:mi:ss')  mwdt,  mwid,  fn_ack_get_usr_name(mwid)  mwnm,"
                sSql += " 	    fn_ack_date_str(fndt, 'yyyy-mm-dd hh24:mi:ss')  fndt,  fnid,  fn_ack_get_usr_name(fnid)  fnnm,"
                sSql += "       fn_ack_get_usr_name(cfmnm) cfmnm"
                sSql += "  FROM rr010m"
                sSql += " WHERE bcno LIKE :bcno || '%'"

                DbCommand()

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 누적결과 가져오기
        Public Shared Function fnGet_hsitory_rst_test_rv(ByVal rsRegNo As String, ByVal rsTestCd As String, ByVal rsDateS As String, ByVal rsDateE As String, _
                                                         ByVal rbResultDataMode As Boolean, ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_hsitory_rst_test_rv(string, string, string, string,string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sSpcCd As String = ""
                Dim sTestCd As String = ""

                If rsTestCd.IndexOf("/") > 0 Then
                    sTestCd = rsTestCd.Substring(0, rsTestCd.IndexOf("/"))
                    sSpcCd = rsTestCd.Substring(rsTestCd.IndexOf("/") + 1)
                Else
                    sTestCd = rsTestCd
                    sSpcCd = ""
                End If

                rsDateS = rsDateS.Replace("-", "").Replace(":", "").Replace(" ", "")
                rsDateE = rsDateE.Replace("-", "").Replace(":", "").Replace(" ", "")

                alParm.Add(New OracleParameter("rs_regno", rsRegNo))
                alParm.Add(New OracleParameter("rs_fromdt", rsDateS))
                alParm.Add(New OracleParameter("rs_todt", rsDateE))
                alParm.Add(New OracleParameter("rs_testcd", sTestCd))

                If sSpcCd = "" Then
                    sSql = "pkg_ack_qry.pkg_get_rst_test_r"
                Else
                    sSql = "pkg_ack_qry.pkg_get_rst_testspc_r"
                    alParm.Add(New OracleParameter("rs_spccd", sSpcCd))
                End If

                alParm.Add(New OracleParameter("rs_viwflg", IIf(rbResultDataMode, "Y", "N").ToString))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 관련검사 최근결과 조회
        Public Shared Function fnGet_Result_Ref(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_Ref(String, String, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       c.tnmd, a.testcd, a.orgrst, a.viewrst, a.rstcmt, a.rstflg,"
                sSql += "       a.hlmark, a.panicmark, a.deltamark, a.alertmark, a.criticalmark, c.tcdgbn, a.tclscd,"
                sSql += "       fn_ack_get_test_reftxt(c.refgbn, b.sex, d.reflms, d.reflm, d.refhms, d.refhm, d.reflfs, d.reflf, d.refhfs, d.refhf, d.reflt) reftxt,"
                sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(a.regid) regnm,"
                sSql += "       fn_ack_date_str(a.MWDT,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(a.mwid)  mwnm,"
                sSql += "       fn_ack_date_str(a.fndt, 'yyyy-mm-dd hh24:mi:ss')  fndt,  fn_ack_get_usr_name(a.fnid)  fnnm,"
                sSql += "       fn_ack_get_bcno_full(a.bcno) bcno, a.rstdt,"
                'sSql += "       fn_ack_get_slip_dispseq(c.partcd, c.slipcd, a.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = c.partcd AND slipcd = c.slipcd AND usdt <= b.bcprtdt AND uedt > b.bcprtdt) sort1,"
                sSql += "       NVL(c.dispseqL, 999) sort2"
                sSql += "  FROM rj010m b, rf060m c,"
                sSql += "       ("
                sSql += "        SELECT bcno, testcd, spccd, MAX(tkdt) tkdt"
                sSql += "          FROM rr010m"
                sSql += "         WHERE rstflg IN ('2', '3')"
                sSql += "           AND bcno   <> :bcno"
                sSql += "           AND regno   = (SELECT regno FROM rj010m WHERE bcno = :bcno)"
                sSql += "           AND tkdt   <= (SELECT tkdt FROM rr010m WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (testcd, spccd) IN (SELECT reftestcd, refspccd FROM rf063m WHERE testcd = :testcd AND spccd = :spccd)"
                sSql += "         GROUP BY bcno, testcd, spccd"
                sSql += "       ) r,"
                sSql += "       rr010m a,"
                sSql += "       (SELECT DISTINCT f61.*"
                sSql += "          FROM rf060m f6, rf061m f61, rj010m j, rr010m r"
                sSql += "         WHERE j.regno    = (SELECT regno FROM rj010m WHERE bcno = :bcno)"
                sSql += "           AND j.bcno     = r.bcno"
                sSql += "           AND r.tkdt    <= (SELECT tkdt FROM rr010m WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (f6.testcd, f6.spccd) IN (SELECT reftestcd, refspccd FROM rf063m WHERE testcd = :testcd and spccd = :spccd)"
                sSql += "           AND r.bcno    <> :bcno"
                sSql += "           AND r.testcd   = f6.testcd"
                sSql += "           AND r.spccd    = f6.spccd"
                sSql += "           AND r.tkdt    >= f6.usdt"
                sSql += "           AND r.tkdt    <  f6.uedt"
                sSql += "           AND f61.testcd = f6.testcd"
                sSql += "           AND f61.spccd  = f6.spccd"
                sSql += "           AND f61.usdt   = f6.usdt"
                sSql += "           AND ROUND(f61.sagec * 365) + f61.sages * 0.1 <= j.dage"
                sSql += "           AND j.dage <= ROUND(f61.eagec * 365) - f61.eages * 0.1"
                sSql += "       ) d"
                sSql += " WHERE a.bcno   = r.bcno"
                sSql += "   AND a.testcd = r.testcd"
                sSql += "   AND a.spccd  = r.spccd"
                sSql += "   AND a.bcno   = b.bcno"
                sSql += "   AND a.tkdt  >= c.usdt"
                sSql += "   AND a.tkdt  <  c.uedt"
                sSql += "   AND a.testcd = c.testcd"
                sSql += "   AND a.spccd  = c.spccd"
                sSql += "   AND a.testcd = d.testcd (+)"
                sSql += "   AND a.spccd  = d.spccd (+)"
                sSql += " ORDER BY rstdt DESC, sort1, sort2, testcd"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcmp", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcmp", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 결과History 조회
        Public Shared Function fnGet_ResultHistory(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_ResultHistory(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       r.regno, f6.tnmd, r1.testcd, r1.orgrst, r1.viewrst, r1.rstcmt, r1.rstflg,"
                sSql += "       r1.hlmark, r1.panicmark, r1.deltamark, r1.alertmark, r1.criticalmark, f6.tcdgbn, r.tclscd,"
                sSql += "       fn_ack_get_test_reftxt(f6.refgbn, j.sex, re.reflms, re.reflm, re.refhms, re.refhm, re.reflfs, re.reflf, re.refhfs, re.refhf, re.reflt) reftxt,"
                sSql += "       fn_ack_date_str(r1.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(r1.regid) regnm,"
                sSql += "       fn_ack_date_str(r1.mwdt,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(r1.mwid)  mwnm,"
                sSql += "       fn_ack_date_str(r1.fndt,  'yyyy-mm-dd hh24:mi:ss') fndt,  fn_ack_get_usr_name(r1.fnid)  fnnm,"
                sSql += "       fn_ack_date_str(r1.sysdt, 'yyyy-mm-dd hh24:mi:ss') sysdt,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                sSql += "       (SELECT dispseql FROM rf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                sSql += "       NVL(f6.dispseql, 999) sort3"
                sSql += "  FROM rr011m r1, rj010m j, rf060m f6,"
                sSql += "       rr010m r,"
                sSql += "       (SELECT DISTINCT f61.*"
                sSql += "          FROM rf060m f6, rf061m f61, rj010m j, rr010M r"
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
                sSql += "   AND r.bcno   = r1.bcno"
                sSql += "   AND r.testcd = r1.testcd"
                sSql += "   AND r.spccd  = r1.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.testcd = re.testcd (+)"
                sSql += "   AND r.spccd  = re.spccd (+)"
                sSql += " ORDER BY sysdt, sort1, sort2, sort3, testcd"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


    End Class

    '-- 특이결과 관련 클래스
    Public Class AbnFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.AbnFn" + vbTab

        Public Shared Function fnGet_Abnormal_RstInfo(ByVal rsBcno As String, ByVal rsPartSlip As String) As DataTable
            Dim sFn As String = "Function fnGet_Abnormal_RstInfo(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += ""
                sSql += "SELECT j.bcno, j.regno, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE '' END wardroom,"
                sSql += "       j.spcflg,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tknm,"
                sSql += "       f.tcdgbn, r.tclscd, r.testcd, r.spccd, f3.spcnmd,"
                sSql += "       f.tnmd,  r.viewrst, r.rstflg,"
                sSql += "       r.hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       CASE WHEN criticalmark = 'C' THEN '1' ELSE '' END chk,"
                sSql += "       f.partcd || f.slipcd partslip, "
                'sSql += "       fn_ack_get_slip_dispseq(f.partcd, f.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f.partcd AND slipcd = f.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                sSql += "       (SELECT dispseql FROM rf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                sSql += "       f.dispseql sort3"
                sSql += "  FROM rj010m j, rj011m j1, rr010m r, rf060m f, lf030m f3"
                sSql += " WHERE j.bcno    = :bcno"
                sSql += "   AND j.bcno    = j1.bcno"
                sSql += "   AND j1.bcno   = r.bcno"
                sSql += "   AND j1.tclscd = r.tclscd"
                sSql += "   AND r.testcd  = f.testcd "
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                If rsPartSlip <> "" Then
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += " ORDER BY sort1, sort2, sort3"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 특이결과 확인자 설정(대장)
        Public Shared Function fnExe_Abnormal_Cfm(ByVal ra_CfmInfo As ArrayList, ByVal rsCfmId As String, ByVal rsCfmCont As String) As Boolean
            Dim sFn As String = "Function fnExe_Abnormal_Reg(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New OracleCommand


            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                For ix As Integer = 0 To ra_CfmInfo.Count - 1
                    sSql = ""
                    sSql += "UPDATE rr050m SET"
                    sSql += "       cfmid   = :cfmid,"
                    sSql += "       cfmdt   = fn_ack_sysdate,"
                    sSql += "       cfmcont = :cfmcont,"
                    sSql += "       editdt  = fn_ack_sysdate,"
                    sSql += "       editid  = :editid,"
                    sSql += "       editip  = :editip"
                    sSql += " WHERE regdt   = :regdt"
                    sSql += "   AND regid   = :regid"

                    With dbCmd
                        .Connection = dbCn
                        .Transaction = dbTran
                        .CommandType = CommandType.Text

                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("cfmid", OracleDbType.Varchar2).Value = rsCfmId
                        .Parameters.Add("cfmcont", OracleDbType.Varchar2).Value = rsCfmCont
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = ra_CfmInfo.Item(ix).ToString.Split("|"c)(0)
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = ra_CfmInfo.Item(ix).ToString.Split("|"c)(1)

                        iRet += .ExecuteNonQuery()

                    End With
                Next

                dbTran.Commit()
                Return CType(IIf(iRet > 0, True, False), Boolean)

            Catch ex As Exception '
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                dbCmd.Dispose() : dbCmd = Nothing
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        '-- 특이결과 확인자 설정(결과조회)
        Public Shared Function fnExe_Abnormal_Cfm(ByVal rsRegNo As String, ByVal rsCfmId As String, ByVal rsCfmCont As String) As Boolean
            Dim sFn As String = "Function fnExe_Abnormal_Reg(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                sSql = ""
                sSql += "UPDATE rr050m SET"
                sSql += "       cfmid   = :cfmid,"
                sSql += "       cfmdt   = fn_ack_sysdate,"
                sSql += "       cfmcont = :cfmcont,"
                sSql += "       editdt  = fn_ack_sysdate,"
                sSql += "       editid  = :editid,"
                sSql += "       editip  = :editip"
                sSql += " WHERE regno   = :regno"
                sSql += "   AND NVL(cfmid, ' ') = ' '"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.Text

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("cfmid", OracleDbType.Varchar2).Value = rsCfmId
                    .Parameters.Add("cfmcont", OracleDbType.Varchar2).Value = rsCfmCont
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo

                    iRet = .ExecuteNonQuery()

                End With

                dbTran.Commit()
                Return CType(IIf(iRet > 0, True, False), Boolean)

            Catch ex As Exception '
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function


        Public Shared Function fnExe_Abnormal_Reg(ByVal rsBcno As String, ByVal rsPartSlip As String, ByVal rsTestCd As String, ByVal rsSpcCd As String, _
                                                  ByVal rsUsrid As String, ByVal rsCmtCont As String, ByVal rsCmtCd As String, _
                                                  ByVal rsRegNo As String) As Boolean
            Dim sFn As String = "Function fnExe_Special_Reg(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New OracleCommand

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                sSql = ""
                sSql += "INSERT INTO rr050m( regdt,           regid,  regip,  partcd,  slipcd,  bcno,  cmtcont,  cmtcd,  regno,  editid,  editip, editdt )"
                sSql += "            VALUES( fn_ack_sysdate, :regid, :regip, :partcd, :slipcd, :bcno, :cmtcont, :cmtcd, :regno, :editid, :editip, fn_ack_sysdate)"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.Text

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("partcd", OracleDbType.Varchar2).Value = rsPartSlip.Substring(0, 1)
                    .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = rsPartSlip.Substring(1, 1)
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = rsCmtCont
                    .Parameters.Add("cmtcd", OracleDbType.Varchar2).Value = rsCmtCd
                    .Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    iRet = .ExecuteNonQuery()

                End With

                dbTran.Commit()
                Return CType(IIf(iRet > 0, True, False), Boolean)

            Catch ex As Exception '
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                dbCmd.Dispose() : dbCmd = Nothing
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

    End Class

    '-- 부적합검체 관련
    Public Class UnifitFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.UnifitFn" + vbTab

    End Class

    '-- TAT 사유
    Public Class TatFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.UnifitFn" + vbTab

        Public Shared Function fnGet_TatInfo_bcno(ByVal rsBcno As String, ByVal rsPartSlip As String) As DataTable
            Dim sFn As String = "Function fnGet_Abnormal_RstInfo(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += ""
                sSql += "SELECT j.bcno, j.regno, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       fn_ack_get_dept_name(j.iogbn, j.deptcd) deptnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE '' END wardroom,"
                sSql += "       j.spcflg,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi')    tkdt,   fn_ack_get_usr_name(r.tkid) tknm,"
                sSql += "       fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi')    mwdt,   fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt,"
                sSql += "       f.tcdgbn, r.tclscd, r.testcd, r.spccd, f3.spcnmd,"
                sSql += "       f.tnmd,   f.prptmi, f.frptmi,"
                sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), NVL(r.mwdt, fn_ack_sysdate), '3') tat_m,"
                sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), NVL(r.fndt, fn_ack_sysdate), '3') tat_f,"
                'sSql += "       fn_ack_get_slip_dispseq(f.partcd, f.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f.partcd AND slipcd = f.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                sSql += "       (SELECT dispseql FROM rf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                sSql += "       f.dispseql sort3"
                sSql += "  FROM rj010m j, rj011m j1, rr010m r, rf060m f, lf030m f3"
                sSql += " WHERE j.bcno    = :bcno"
                sSql += "   AND j.bcno    = j1.bcno"
                sSql += "   AND j1.bcno   = r.bcno"
                sSql += "   AND j1.tclscd = r.tclscd"
                sSql += "   AND r.testcd  = f.testcd "
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                If rsPartSlip <> "" Then
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += " ORDER BY sort1, sort2, sort3"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnExe_Tat_Reg(ByVal rsBcno As String, ByVal rsTestCds As String, ByVal rsCmtCd As String, ByVal rsCmtCont As String) As Boolean
            Dim sFn As String = "Function fnExe_Tat_Reg(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            Dim dbCmd As New OracleCommand

            Dim sBuf() As String = rsTestCds.Split("|"c)
            If sBuf.Length < 1 Then Return True

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                For ix As Integer = 0 To sBuf.Length - 1

                    If sBuf(ix) <> "" Then

                        sSql = ""
                        sSql += "UPDATE rr051m SET"
                        sSql += "       regdt   = fn_ack_sysdate,"
                        sSql += "       regid   = :regid,"
                        sSql += "       regip   = :regip,"
                        sSql += "       cmtcont = :cmtcont,"
                        sSql += "       cmtcd   = :cmtcd"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND testcd  = :testcd"

                        With dbCmd
                            .Connection = dbCn
                            .Transaction = dbTran
                            .CommandType = CommandType.Text

                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                            .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = rsCmtCont
                            .Parameters.Add("cmtcd", OracleDbType.Varchar2).Value = rsCmtCd
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = sBuf(ix)

                            iRet = .ExecuteNonQuery()
                        End With

                        If iRet = 0 Then
                            With dbCmd

                                sSql = ""
                                sSql += "INSERT INTO rr051m("
                                sSql += "            regdt,           regid,  regip,  bcno,  testcd,  cmtcont,  cmtcd )"
                                sSql += "    VALUES( fn_ack_sysdate, :regid, :regip, :bcno, :testcd, :cmtcont, :cmtcd)"

                                .Connection = dbCn
                                .Transaction = dbTran
                                .CommandType = CommandType.Text

                                .CommandText = sSql

                                .Parameters.Clear()
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                .Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                                .Parameters.Add("testcd", OracleDbType.Varchar2).Value = sBuf(ix)
                                .Parameters.Add("cmtcont", OracleDbType.Varchar2).Value = rsCmtCont
                                .Parameters.Add("cmtcd", OracleDbType.Varchar2).Value = rsCmtCd

                                iRet = .ExecuteNonQuery()
                            End With
                        End If
                    End If

                Next

                dbTran.Commit()
                Return CType(IIf(iRet > 0, True, False), Boolean)

            Catch ex As Exception '
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            Finally
                dbCmd.Dispose() : dbCmd = Nothing
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

    End Class

    '-- 환자정보 바꾸기 관련
    Public Class ChangePatFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.ChangePatFn" + vbTab

        '-- 환자정보 수정 데이타 조회
        Public Shared Function fnGet_Change_PatList(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Function fnGet_Change_PatList(ByVal rsDStartDt As String, ByVal rsDEndDt As String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi') regdt, fn_ack_get_usr_name(regid) regnm, bfregno, regno"
                sSql += "  FROM rrc10m"
                sSql += " WHERE regdt >= :dates "
                sSql += "   AND regdt <= :datee || '235959'"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegno <> "" Then
                    sSql += "   AND bfregno <= :regno "
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 환자정보 리턴
        Public Shared Function fnGet_PatInfo(ByRef rsRegno As String) As DataTable
            Dim sFn As String = "Function fnGet_PatInfo(ByVal rsDStartDt As String, ByVal rsDEndDt As String, ByVal rsRegno As String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT regno, patnm, sex, age"
                sSql += "  FROM rj010m"
                sSql += " WHERE regno = :regno"

                al.Clear()
                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 접수환자 조회(환자정보 수정을 위한)
        Public Shared Function fnGet_Change_PatInfo(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Function fnGet_Change_PatInfo(String, String, String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT j.orddt, j.bcno, f3.spcnmd, f6.tnmd, r.viewrst, r.fndt, r.testcd, r.rstflg,"
                sSql += "       CASE WHEN r.rstflg = '3' THEN '1' ELSE '0' END chk, 'L' jobgbn"
                sSql += "  fROM (SELECT regno, bcno, tclscd, testcd, spccd, viewrst, hlmark, panicmark, deltamark, rstflg,"
                sSql += "               tkdt, tkid, regdt, regid, mwdt, mwid, fndt, fnid"
                sSql += "          FROM rr010m"
                sSql += "         WHERE regno = :regno"
                sSql += "       ) r, rj010m j, rf060m f6, lf030m f3"
                sSql += " WHERE r.bcno   = j.bcno"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt   < f3.uedt"
                sSql += "   AND j.orddt  >= :dates"
                sSql += "   AND j.orddt  <= :datee || '235959'"
                sSql += "   AND j.spcflg IN ('1', '2', '3', '4')"
                sSql += " ORDER BY jobgbn, bcno desc, testcd"

                al.Clear()
                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnExe_Change_Regnoe(ByVal rsRegNo As String, ByVal rsRegNo_chg As String, ByVal rsTableNM As String(), ByVal rsBcNos As String, _
                                                   ByVal rsPatNm As String, ByVal rsIdNo1 As String, ByVal rsIdNo2 As String, ByVal rsUsrID As String, ByVal rsOrddtS As String, ByVal rsOrddtE As String) As Boolean
            Dim sFn As String = "fnRegNoChange(String, string, string, string(), string, string, stirng, string) As Boolean"

            Try

                Dim sSql As String = ""
                Dim al_Sql As New ArrayList

                For i As Integer = 0 To rsTableNM.Length - 1

                    If rsTableNM(i).ToLower = "mdresult" Then
                        sSql = ""
                        sSql += "UPDATE " + rsTableNM(i)
                        sSql += "   SET patno = '" + rsRegNo_chg + "'"
                        sSql += " WHERE (patno, orddate, execprcpuniqno, ioflag) IN"
                        sSql += "       (SELECT '" + rsRegNo + "', SUBSTR(orgorddt, 1, 8), ocs_key, SUBSTR(fkocs, 1, 1)"
                        sSql += "          FROM lj011m"
                        sSql += "         WHERE bcno IN ('" + rsBcNos.Replace(",", "','") + "')"
                        sSql += "       )"
                    Else
                        sSql = ""
                        sSql += "UPDATE " + rsTableNM(i)
                        sSql += "   SET regno = '" + rsRegNo_chg + "'"

                        If rsTableNM(i).ToLower = "rj010m" Or rsTableNM(i).ToLower = "rj010h" Then
                            sSql += ", patnm = '" + rsPatNm + "'"
                        ElseIf rsTableNM(i).ToLower = "rj011m" Or rsTableNM(i).ToLower = "rj011h" Then
                            sSql += ", fkocs = CASE WHEN owngbn = 'L' THEN fkocs ELSE SUBSTR(fkocs, 1, 2) ||'" + rsRegNo_chg + "' || SUBSTR(fkocs, 11) END"
                        End If

                        sSql += " WHERE bcno IN ('" + rsBcNos.Replace(",", "','") + "')"
                    End If

                    al_Sql.Add(sSql)
                Next

                sSql = ""
                sSql += "INSERT INTO rrc10m (regdt, regid, bfregno, regno, ordsdt, ordedt, editid, editip, editdt)"
                sSql += "            VALUES (fn_ack_sysdate, '" + rsUsrID + "', '" + rsRegNo + "', '" + rsRegNo_chg + "',"
                sSql += "                    '" + rsOrddtS + "', '" + rsOrddtE + "', '" + USER_INFO.USRID + "', '" + USER_INFO.LOCALIP + "', fn_ack_sysdate"
                sSql += "                   )"

                al_Sql.Add(sSql)

                Dim bReturn As Boolean = (New APP_DB.DBSql).ExcuteSql(al_Sql)

                Return bReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


    End Class

    '-- 화면에서 결과등록
    Public Class AxRstFn
        Private Const msFile As String = "File : CGDA_R.vb, Class : DA_RegRst" & vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Private m_dt_rst As DataTable
        Private m_al_ParentCd As ArrayList
        Private m_s_CfmNm As String = ""
        Private m_s_CfmSign As String = ""

        Private Function fnGet_Server_DateTime() As String

            Dim sFn As String = "Private Function fnGet_Server_DateTime() As string"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql += "SELECT fn_ack_sysdate srvdate FROM DUAL"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("srvdate").ToString()
                Else
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                End If

            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmmss").ToString
            End Try

        End Function

        Public Function fnReg_OCS(ByVal rsBcNo As String) As Boolean
            Dim sFn As String = "Public Function fnReg_OCS(string) As String"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction
            Dim dbCmd As New OracleCommand
            Dim sErrVal As String = ""

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                '-- OCS에 결과 올리기
                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.StoredProcedure

                    .CommandText = "pro_ack_exe_ocs_rst_r"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Or sErrVal.IndexOf("no data") > 0 Then
                Else
                    dbTran.Rollback()
                    Return False
                End If

                '-- OCS에 결과 올리기
                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.StoredProcedure

                    .CommandText = "pro_ack_exe_ocs_rstflg"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Or sErrVal.IndexOf("no data") > 0 Then
                    dbTran.Commit()
                    Return True
                Else
                    dbTran.Rollback()
                    Return False
                End If

            Catch ex As Exception
                dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                dbCmd.Dispose() : dbCmd = Nothing
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Public Function fnReg_Change_CollAndTkAndRst_date(ByVal rsBcNo As String, ByVal rsRstDate As String) As Boolean

            Dim sFn As String = "Public Function fnReg_OCS(string) As String"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction
            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                sSql = ""
                sSql += " UPDATE rj011m SET colldt = :colldt, tkdt = :tkdt"
                sSql += "  WHERE bcno = :bcno"

                With dbCmd
                    .Transaction = dbTran
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("colldt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("tkdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With

                If iRet = 0 Then
                    m_dbTran.Rollback()
                    Return False
                End If

                sSql = ""
                sSql += " UPDATE rr010m SET tkdt = :tkdt,"
                sSql += "        regdt = DECODE(NVL(regdt, ' '),   ' ',  NULL, :rstdt),"
                sSql += "        mwdt  = DECODE(NVL(mwdt, ' '),    ' ',  NULL, :rstdt),"
                sSql += "        fndt  = DECODE(NVL(fndt, ' '),    ' ',  NULL, :rstdt),"
                sSql += "        rstdt = DECODE(NVL(rstdt, ' '),   ' ',  NULL, :rstdt)"
                sSql += "  where bcno = :bcno"
                'sSql += "    AND orgrst IS NOT NULL"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("tkdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With

                If iRet = 0 Then
                    m_dbTran.Rollback()
                    Return False
                End If

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                dbCmd.Dispose() : dbCmd = Nothing
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Private Function fnEdit_LR_Rerun(ByVal rsUsrId As String, ByVal rsSrvDt As String, ByVal roRstInfo As ArrayList, ByRef roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR_Rerun( String, String, ArrayList, ByRef ArrayList) As Boolean"
            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                roBcNos.Clear()
                For ix As Integer = 0 To roRstInfo.Count - 1

                    If roBcNos.Contains(CType(roRstInfo.Item(ix), RERUN_INFO).msBcNo) = False Then
                        roBcNos.Add(CType(roRstInfo.Item(ix), RERUN_INFO).msBcNo)
                    End If

                    sSql = ""
                    sSql += "INSERT INTO rr011m"
                    sSql += "       ("
                    sSql += "        bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "        bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, rstflg, rerunflg, tclscd,"
                    sSql += "        eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, sysdt, editdt, editid, editip, seq"
                    sSql += "       ) "
                    sSql += "SELECT bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "       bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, rstflg, rerunflg, tclscd,"
                    sSql += "       eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, fn_ack_sysdate, editdt, editid, editip, sq_rr011m.nextval"
                    sSql += "  FROM rr010m"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND (NVL(regid, ' ') <> ' ' OR NVL(mwid, ' ') <> ' ' OR NVL(fnid, ' ') <> ' ')"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), RERUN_INFO).msBcNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), RERUN_INFO).msTestCd
                    End With

                    sSql = ""
                    sSql += "UPDATE rr010m"
                    sSql += "   SET rstflg = '0',    rstdt = :rstdt,    orgrst = NULL, viewrst = NULL, eqflag = NULL,"
                    sSql += "       regdt  = :rstdt, regid = :regid,"
                    sSql += "       mwdt   = NULL, mwid  = NULL,"
                    sSql += "       fndt   = NULL, fnid  = NULL, cfmnm = NULL, cfmsign = NULL, rerunflg = '1'"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsSrvDt
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsSrvDt
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId

                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), RERUN_INFO).msBcNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), RERUN_INFO).msTestCd

                    End With

                    If iRet = 0 Then Return False
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        Public Function fnReRun(ByVal rsUsrId As String, ByVal roRstInfo As ArrayList, ByVal roCmtInfo As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnReRun(String, ArrayList, ArrayList) As Boolean"

            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim iRet As Integer = 0

                Dim alBcNos As New ArrayList
                Dim sSrvDt As String = fnGet_Server_DateTime()

                If fnEdit_LR_Rerun(rsUsrId, sSrvDt, roRstInfo, alBcNos) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                If fnEdit_rr040m(roCmtInfo) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                If fnEdit_LJ_Clear(alBcNos, sSrvDt) = False Then
                    m_dbTran.Rollback()
                    Return False

                End If

                For intIdx As Integer = 0 To alBcNos.Count - 1
                    If fnEdit_EXE_OCS_RST(alBcNos.Item(intIdx).ToString) = False Then
                        m_dbTran.Rollback()
                        Return False
                    End If
                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Private Function fnEdit_RST_Clear(ByVal roRstInfo As ArrayList, ByRef roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_RST_Clear(ArrayList, ArrayList) As Boolean"
            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                roBcNos.Clear()

                For ix As Integer = 0 To roRstInfo.Count - 1

                    If roBcNos.Contains(CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO) = False Then
                        roBcNos.Add(CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO)
                    End If

                    sSql = ""
                    sSql += "UPDATE rr010m"
                    sSql += "   SET orgrst = NULL, viewrst = NULL, rstcmt = NULL, rerunflg = NULL,"
                    sSql += "       regid = NULL, regdt = NULL, mwid = NULL, mwdt = NULL, fnid = NULL, fndt = NULL, cfmnm = NULL, cfmsign = NULL,"
                    sSql += "       rstflg = NULL, rstdt = NULL,"
                    sSql += "       hlmark = NULL, panicmark = NULL, deltamark = NULL, criticalmark = NULL, alertmark = NULL,"
                    sSql += "       bfbcno = '', bffndt = NULL, bforgrst = NULL, bfviewrst = NULL, eqflag = NULL,"
                    sSql += "       editdt = fn_ack_sysdate,"
                    sSql += "       editid = :editid,"
                    sSql += "       editip = :editip"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()

                    End With

                    If iRet = 0 Then Return False

                    '-- 특수보고서 삭제                    
                    sSql = ""
                    sSql += "INSERT INTO rrs10h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, bcno, testcd, rstflg, rsttxt, fedt, feid, migymd, editdt, editid, editip"
                    sSql += "  FROM rrs10m"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    With dbCmd
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    sSql = ""
                    sSql += "DELETE rrs10m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    sSql = ""
                    sSql += "DELETE rrs11m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    sSql = ""
                    sSql += "DELETE rrs12m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    'sSql = ""
                    'sSql += "DELETE rrs13m WHERE bcno = :bcno AND testcd = :testcd"

                    'With dbCmd
                    '    .CommandType = CommandType.Text
                    '    .CommandText = sSql

                    '    .Parameters.Clear()
                    '    .Parameters.Add("bcno",  OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                    '    .Parameters.Add("testcd",  OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                    '    iRet = .ExecuteNonQuery()
                    'End With

                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_LJ_Clear(ByVal roBcNos As ArrayList, ByVal rsSrvDt As String) As Boolean
            Dim sFn As String = "Private Function fnEdit_LJ_Clear(ArrayList, String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                Dim intRet As Integer = 0
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For intIdx As Integer = 0 To roBcNos.Count - 1

                    If fnEdit_LR_Parent(roBcNos.Item(intIdx).ToString, "", rsSrvDt) = False Then
                        Return False
                    End If

                    If fnEdit_rj011m(roBcNos.Item(intIdx).ToString) < 1 Then
                        Return False
                    End If

                    If fnEdit_rj010m(roBcNos.Item(intIdx).ToString) < 1 Then
                        Return False
                    End If

                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_rr020m_Clear(ByVal roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_rr020m_Clear(ArrayList) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                Dim intRet As Integer = 0
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To roBcNos.Count - 1

                    sSql = "DELETE rr020m WHERE bcno = :bcno"
                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roBcNos.Item(ix).ToString

                        intRet = .ExecuteNonQuery()
                    End With

                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_rr040m_Clear(ByVal roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_rr040m_Clear(ArrayList) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To roBcNos.Count - 1

                    sSql = "DELETE rr040m WHERE bcno = :bcno"
                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roBcNos.Item(ix).ToString

                        iRet = .ExecuteNonQuery()
                    End With
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnRstClear(ByVal rsUsrId As String, ByVal roRstInfo As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnRstClear(String, ArrayList) As Boolean"

            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim iRet As Integer = 0
                Dim alBcNos As New ArrayList
                Dim sSrvDt As String = fnGet_Server_DateTime()

                'If fnEdit_LR_BackUp(roRstInfo) = False Then
                '    m_dbTran.Rollback()
                '    Return False
                'End If

                If fnEdit_RST_Clear(roRstInfo, alBcNos) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                If fnEdit_LJ_Clear(alBcNos, sSrvDt) = False Then
                    m_dbTran.Rollback()
                    Return False

                End If

                'If fnEdit_rr020m_Clear(alBcNos) = False Then
                '    m_dbTran.Rollback()
                '    Return False
                'End If

                For intIdx As Integer = 0 To alBcNos.Count - 1
                    If fnEdit_EXE_OCS_RST(alBcNos.Item(intIdx).ToString) = False Then
                        m_dbTran.Rollback()
                        Return False
                    End If
                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        ' 결과등록 
        Public Function fnReg(ByVal rsUsrId As String, ByVal roRstInfo As ArrayList, Optional ByVal roCmtInfo As ArrayList = Nothing, _
                                    Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "") As Boolean
            Dim sFn As String = "Public Function fnReg(String, ArrayList, [ArrayList], [String], [String]) As Boolean"

            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction

            Try
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                m_s_CfmNm = rsCfmNm
                m_s_CfmSign = rsCfmSign

                If fnEdit_rr010m(roRstInfo, rsUsrId) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                If roCmtInfo Is Nothing Then
                Else
                    ''' part slip별 소견일때 
                    If fnEdit_rr040m(roCmtInfo) = False Then  ''' 검체 part slip별 소견 
                        m_dbTran.Rollback()
                        Return False
                    End If

                End If

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        Private Function fnEdit_EXE_OCS_RST(ByVal rsBcNo As String) As Boolean

            Dim sFn As String = "Public Function fnEdit_EXE_OCS_RST(string) As String"
            Dim dbCmd As New OracleCommand

            Dim sErrVal As String = ""

            Try

                '-- OCS에 결과 올리기
                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure

                    .CommandText = "pro_ack_exe_ocs_rst_r"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 1000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Or sErrVal.IndexOf("no data") > 0 Then
                    ' Return True
                Else
                    Return False
                End If

                '-- OCS에 결과 올리기
                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure

                    .CommandText = "pro_ack_exe_ocs_rstflg"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Or sErrVal.IndexOf("no data") > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_EXE_OCS_RST_INF(ByVal rsBcNo As String) As Boolean

            Dim sFn As String = "Public Function fnEdit_EXE_OCS_RST_INF(string) As String"
            Dim dbCmd As New OracleCommand

            Dim sErrVal As String = ""

            Try
                '-- 감염정보
                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_ocs_rst_inf_r"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        Private Function fnEdit_rr010m(ByVal roRstInfo As ArrayList, ByVal rsUsrId As String) As Boolean
            Dim sFn As String = "Private Function fnEdit_rr010m(ArrayList, String) As Boolean"
            ' 새로 생성 박정은 
            Try
                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim sSrvDt As String = fnGet_Server_DateTime()
                Dim sBcNo_Old As String = ""
                Dim alBcNos As New ArrayList
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To roRstInfo.Count - 1

                    If CType(roRstInfo(ix), ResultInfo_Test).mBCNO <> sBcNo_Old Then
                        sSrvDt = fnGet_Server_DateTime()
                        alBcNos.Add(CType(roRstInfo(ix), ResultInfo_Test).mBCNO + "|" + sSrvDt)
                    End If
                    sBcNo_Old = CType(roRstInfo(ix), ResultInfo_Test).mBCNO

                    Dim iRet As Integer = 0

                    'Backup
                    sSql = ""
                    sSql += "INSERT INTO rr011m"
                    sSql += "       ("
                    sSql += "        bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "        bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg, tclscd,"
                    sSql += "        eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, sysdt, editdt, editid, editip, seq"
                    sSql += "       ) "
                    sSql += "SELECT bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "       bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg, tclscd,"
                    sSql += "       eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, :rstdt, editdt, editid, editip, sq_rr011m.nextval"
                    sSql += "  FROM rr010m"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND (NVL(regid, ' ') <> ' ' OR NVL(mwid, ' ') <> ' ' OR NVL(fnid, ' ') <> ' ')"
                    sSql += "   AND (NVL(orgrst,  '" + CType(roRstInfo(ix), ResultInfo_Test).mOrgRst + "') <> '" + CType(roRstInfo(ix), ResultInfo_Test).mOrgRst + "' OR"
                    sSql += "        NVL(viewrst, '" + CType(roRstInfo(ix), ResultInfo_Test).mViewRst + "') <> '" + CType(roRstInfo(ix), ResultInfo_Test).mViewRst + "'"
                    sSql += "       )"
                    sSql += "   AND NVL(orgrst,  ' ') <> ' '"
                    sSql += "   AND NVL(viewrst, ' ') <> ' '"

                    With dbCmd
                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mTestCd

                        .ExecuteNonQuery()
                    End With

                    Dim sRstFlg As String = CType(roRstInfo(ix), ResultInfo_Test).mRstFlg

                    'Update
                    sSql = ""
                    sSql += "UPDATE rr010m"
                    sSql += "   SET orgrst       = :orgrst,"
                    sSql += "       viewrst      = :viewrst,"
                    sSql += "       deltamark    = :deltamark,"
                    sSql += "       panicmark    = :panicmark,"
                    sSql += "       criticalmark = :criticalmark,"
                    sSql += "       alertmark    = :alertmark,"
                    sSql += "       hlmark       = :hlmark,"

                    Select Case sRstFlg
                        Case "1"
                            sSql += "       regid = :rstid,"
                            sSql += "       regdt = :rstdt,"
                        Case "2"
                            sSql += "       regid = NVL(regid, :rstid),"
                            sSql += "       regdt = NVL(regdt, :rstdt),"
                            sSql += "       mwid  = :rstid,"
                            sSql += "       mwdt  = :rstdt,"
                        Case "3"
                            sSql += "       regid   = NVL(regid, :rstid),"
                            sSql += "       regdt   = NVL(regdt, :rstdt),"
                            sSql += "       mwid    = NVL(mwid,  :rstid),"
                            sSql += "       mwdt    = NVL(mwdt,  :rstdt),"
                            sSql += "       fnid    = :rstid,"
                            sSql += "       fndt    = :rstdt,"
                            sSql += "       cfmnm   = :cfmnm,"
                            sSql += "       cfmsign = :cfmsign,"
                            sSql += "       cfmyn   = 'Y',"
                    End Select

                    sSql += "       rstflg = :rstflg,"
                    sSql += "       rstdt  = :rstdt,"
                    sSql += "       rstcmt = :rstcmt,"

                    If CType(roRstInfo(ix), ResultInfo_Test).mBFBCNO <> "" Then
                        sSql += "       bfbcno    = :bfbcno,"
                        sSql += "       bffndt    = :bffndt,"
                        sSql += "       bforgrst  = :bforgrst,"
                        sSql += "       bfviewrst = :bfviewrst,"
                    End If
                    sSql += "       fregdt = CASE WHEN NVL(fregdt, ' ') = ' ' THEN :rstdt ELSE fregdt END,"
                    sSql += "       editdt = fn_ack_sysdate,"
                    sSql += "       editid = :editid,"
                    sSql += "       editip = :editip"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()

                        .Parameters.Add("orgrst", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mOrgRst
                        .Parameters.Add("viewrst", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mViewRst
                        .Parameters.Add("deltamark", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mDeltaMark
                        .Parameters.Add("panicmark", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mPanicMark
                        .Parameters.Add("criticalmark", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mCriticalMark
                        .Parameters.Add("alertmark", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mAlertMark
                        .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mHLMark

                        Select Case sRstFlg
                            Case "1"
                                .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                            Case "2"
                                .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                            Case "3"
                                .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mCfmNm.Trim
                                .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mCfmSign

                        End Select

                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mRstFlg
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt
                        .Parameters.Add("rstcmt", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mRstCmt

                        If CType(roRstInfo(ix), ResultInfo_Test).mBFBCNO <> "" Then
                            .Parameters.Add("bfbcno", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mBFBCNO
                            .Parameters.Add("bffndt", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mBFFNDT
                            .Parameters.Add("bforgrst", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mBFORGRST
                            .Parameters.Add("bfviewrst", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mBFVIEWRST
                        End If

                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = sSrvDt

                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    If iRet = 0 Then Return False
                Next

                If alBcNos.Count > 0 Then
                    For ix As Integer = 0 To alBcNos.Count - 1

                        If fnEdit_LR_Parent(alBcNos.Item(ix).ToString.Split("|"c)(0), rsUsrId, alBcNos.Item(ix).ToString.Split("|"c)(1)) = False Then
                            Return False
                        End If

                        ''' battery 정은추가 
                        If fnEdit_LR_Battery(CType(roRstInfo(0), ResultInfo_Test).mBCNO, rsUsrId, sSrvDt) = False Then
                            Return False
                        End If

                        If fnEdit_rj011m(alBcNos.Item(ix).ToString.Split("|"c)(0)) < 1 Then
                            Return False
                        End If

                        If fnEdit_rj010m(alBcNos.Item(ix).ToString.Split("|"c)(0)) < 1 Then
                            Return False
                        End If

                        '감염정보 잠시 막음  수정해야함  정은 
                        If fnEdit_EXE_OCS_RST_INF(alBcNos.Item(ix).ToString.Split("|"c)(0)) = False Then
                            Return False
                        End If

                        ' ocs연동 저장  잠시 막음 수정해야함 정은 
                        If fnEdit_EXE_OCS_RST(alBcNos.Item(ix).ToString.Split("|"c)(0)) = False Then
                            Return False
                        End If

                    Next
                End If

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function ' new 새로생성 박정은


        Private Function fnEdit_LR_Parent(ByVal rsBcNo As String, ByVal rsUsrId As String, ByVal rsDate As String) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR_Parent(String, String, String) As Boolean"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt_p As New DataTable

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, SUBSTR(r.testcd, 1, 5) testcd, r.spccd,"
                sSql += "       CASE WHEN NVL(f.fixrptusr, ' ') <> ' ' THEN f.fixrptusr"
                sSql += "            ELSE fn_ack_get_usr_name(f68.doctorid1)"
                sSql += "       END cfmnm, cfmsign"
                sSql += "  FROM rr010m r, rf060m f, lf100m f68"
                sSql += " WHERE r.bcno = :bcno"
                sSql += "   and (NVL(r.orgrst, ' ') <> ' ' OR (f.tcdgbn = 'C' AND NVL(f.reqsub, '0') = '1') OR (f.tcdgbn = 'P' AND f.titleyn = '0'))"
                sSql += "   and r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   and r.tkdt   <  f.uedt"
                sSql += "   and f.tcdgbn IN ('P', 'C')"
                sSql += "   AND f.tordslip = f68.tordslip"
                sSql += "   AND r.tkdt    >= f68.usdt"
                sSql += "   AND r.tkdt    <  f68.uedt"
                sSql += " GROUP BY SUBSTR(r.testcd, 1, 5), r.spccd,"
                sSql += "          CASE WHEN NVL(f.fixrptusr, ' ') <> ' ' THEN f.fixrptusr"
                sSql += "               ELSE fn_ack_get_usr_name(f68.doctorid1)"
                sSql += "          END, cfmsign"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dt_p.Reset()
                dbDa.Fill(dt_p)

                If dt_p.Rows.Count < 1 Then Return True

                For ix As Integer = 0 To dt_p.Rows.Count - 1
                    Dim sRstFlg As String = dt_p.Rows(ix).Item("rstflg").ToString
                    Dim sRstFlg_max As String = dt_p.Rows(ix).Item("maxrstflg").ToString

                    If sRstFlg = "3" Then


                        sSql = ""
                        sSql += "UPDATE rr010m SET"
                        sSql += "       rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :rstid), regdt   = NVL(regdt, :rstdt),"
                        sSql += "       mwid   = NVL(mwid,  :rstid), mwdt    = NVL(mwdt,  :rstdt),"
                        sSql += "       fnid   = NVL(fnid,  :rstid), fndt    = :rstdt,"
                        sSql += "       cfmnm  = :cfmnm,             cfmsign = :cfmsign, cfmyn = CASE WHEN cfmyn = 'Y' THEN cfmyn ELSE 'N' END,"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno = :bcno"
                        sSql += "   AND testcd LIKE :testcd || '%'"
                        sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                        sSql += "        (testcd, spccd, '1') = "
                        sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                        sSql += "          WHERE r.bcno   = :bcno"
                        sSql += "            AND r.testcd LIKE :testcd || '%'"
                        sSql += "            AND r.testcd = f.testcd"
                        sSql += "            AND r.spccd  = f.spccd"
                        sSql += "            AND f.usdt  <= r.tkdt"
                        sSql += "            AND f.uedt  >  r.tkdt"
                        sSql += "            AND tcdgbn   = 'P'"
                        sSql += "        )"
                        sSql += "       )"
                        sSql += "   AND rstflg <> '3'"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmnm").ToString().Trim
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmsign").ToString

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                            .Parameters.Add("tescdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                        End With
                    Else
                        sSql = ""
                        Select Case sRstFlg
                            Case "1"
                                sSql = ""
                                sSql += "UPDATE rr010m SET"
                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :rstid), regdt = NVL(regdt, :rstdt),"
                                sSql += "       mwid   = NULL,               mwdt  = NULL,"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND testcd LIKE :testcd || '%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '0') = "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                                sSql += "          WHERE r.bcno   = :bcno"
                                sSql += "            AND r.testcd LIKE :testcd || '%'"
                                sSql += "            AND r.testcd = f.testcd"
                                sSql += "            AND r.spccd  = f.spccd"
                                sSql += "            AND f.usdt  <= r.tkdt"
                                sSql += "            AND f.uedt  >  r.tkdt"
                                sSql += "            AND tcdgbn   = 'P'"
                                sSql += "        )"
                                sSql += "       )"


                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                End With

                            Case "2"
                                sSql = ""
                                sSql += "UPDATE rr010m SET"
                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :rstid), regdt = NVL(regdt, :rstdt),"
                                sSql += "       mwid   = NVL(mwid,  :rstid), mwdt  = NVL(mwdt,  :rstdt),"
                                sSql += "       fnid   = NULL,               fndt = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND testcd LIKE :testcd ||'%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '0') = "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                                sSql += "          WHERE r.bcno   = :bcno"
                                sSql += "            AND r.testcd LIKE :testcd || '%'"
                                sSql += "            AND r.testcd = f.testcd"
                                sSql += "            AND r.spccd  = f.spccd"
                                sSql += "            AND f.usdt  <= r.tkdt"
                                sSql += "            AND f.uedt  >  r.tkdt"
                                sSql += "            AND tcdgbn   = 'P'"
                                sSql += "        )"
                                sSql += "       )"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                                End With
                            Case "0"
                                If sRstFlg_max = "3" Then
                                    sSql = ""
                                    sSql += "UPDATE rr010m SET"
                                    sSql += "       rstflg = :rstflg,"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid  = NVL(regid, :rstid), regdt = NVL(regdt, :rstdt),"
                                    sSql += "       mwid   = NVL(mwid,  :rstid), mwdt  = NVL(mwdt,  :rstdt),"
                                    sSql += "       fnid   = NULL,               fndt  = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno   = :bcno"
                                    sSql += "   AND testcd LIKE :testcd ||'%'"
                                    sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                    sSql += "        (testcd, spccd, '0') = "
                                    sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                                    sSql += "          WHERE r.bcno   = :bcno"
                                    sSql += "            AND r.testcd LIKE :testcd || '%'"
                                    sSql += "            AND r.testcd = f.testcd"
                                    sSql += "            AND r.spccd  = f.spccd"
                                    sSql += "            AND f.usdt  <= r.tkdt"
                                    sSql += "            AND f.uedt  >  r.tkdt"
                                    sSql += "            AND tcdgbn   = 'P'"
                                    sSql += "        )"
                                    sSql += "       )"

                                    dbCmd.CommandText = sSql

                                    With dbCmd
                                        .Parameters.Clear()
                                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = "1"
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                                    End With
                                End If
                        End Select

                    End If
                    If Not sSql = "" Then
                        Dim iRet As Integer = dbCmd.ExecuteNonQuery()
                    End If
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_LR_Battery(ByVal rsBcNo As String, ByVal rsUsrId As String, ByVal rsDate As String) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR_Battery(String, String, String) As Boolean"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt_p As New DataTable

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, r.tclscd, r.spccd,"
                sSql += "       CASE WHEN NVL(f.fixrptusr, ' ') <> ' ' THEN f.fixrptusr"
                sSql += "            ELSE fn_ack_get_usr_name(f68.doctorid1)"
                sSql += "       END cfmnm, '' cfmsign"
                sSql += "  FROM rr010m r, rf060m f, lf100m f68, rf062m f62"
                sSql += " WHERE r.bcno       = :bcno"
                sSql += "   AND r.tclscd     = f.testcd"
                sSql += "   AND r.spccd      = f.spccd"
                sSql += "   AND r.tkdt      >= f.usdt"
                sSql += "   AND r.tkdt      <  f.uedt"
                sSql += "   AND r.tclscd     = f62.tclscd"
                sSql += "   AND r.spccd      = f62.tspccd"
                sSql += "   AND r.testcd     = f62.testcd"
                sSql += "   AND r.spccd      = f62.spccd"
                sSql += "   AND f62.grprstyn = '1'"
                sSql += "   AND f.tcdgbn     = 'B'"
                sSql += "   AND f.grprstyn   = '1'"
                sSql += "   AND f.tordslip   = f68.tordslip"
                sSql += "   AND r.tkdt      >= f68.usdt"
                sSql += "   AND r.tkdt      <  f68.uedt"
                sSql += "   AND (r.testcd <> r.tclscd OR NVL(f.titleyn, '0') = '0')"
                sSql += "   AND LENGTH(r.testcd) = 5"
                sSql += " GROUP BY r.tclscd, r.spccd,"
                sSql += "          CASE WHEN NVL(f.fixrptusr, ' ') <> ' ' THEN f.fixrptusr"
                sSql += "               ELSE fn_ack_get_usr_name(f68.doctorid1)"
                sSql += "          END"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dt_p.Reset()
                dbDa.Fill(dt_p)

                If dt_p.Rows.Count < 1 Then Return True

                For ix As Integer = 0 To dt_p.Rows.Count - 1
                    Dim sRstFlg As String = dt_p.Rows(ix).Item("rstflg").ToString
                    Dim sRstFlg_max As String = dt_p.Rows(ix).Item("maxrstflg").ToString

                    If sRstFlg = "3" Then

                        sSql = ""
                        sSql += "UPDATE rr010m"
                        sSql += "   SET rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :rstid), regdt   = NVL(regdt, :rstdt),"
                        sSql += "       mwid   = NVL(mwid,  :rstid), mwdt    = NVL(mwdt,  :rstdt),"
                        sSql += "       fnid   = NVL(fnid,  :rstid), fndt    = NVL(fndt,  :rstdt),"
                        sSql += "       cfmnm  = :cfmnm,             cfmsign = :cfmsign, cfmyn = 'Y',"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno      = :bcno"
                        sSql += "   AND tclscd    = :testcd"
                        sSql += "   AND rstflg   <> '3'"
                        sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                        sSql += "       (SELECT tclscd, tspccd, testcd FROM rf062m WHERE grprstyn = '1')"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmnm").ToString().Trim
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmsign").ToString()

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                        End With
                    Else
                        Select Case sRstFlg
                            Case "0"
                                If sRstFlg_max = "3" Then
                                    sSql = ""
                                    sSql += "UPDATE rr010m"
                                    sSql += "   SET rstflg = '1',"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid  = NVL(regid, :rstid), regdt = NVL(regdt, :rstdt),"
                                    sSql += "       mwid   = NVL(mwid,  :rstid), mwdt  = NVL(mwdt,  :rstdt),"
                                    sSql += "       fnid   = NULL,               fndt  = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno       = :bcno"
                                    sSql += "   AND tclscd     = :testcd"
                                    sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR NVL(rstflg, ' ') <> ' ')"
                                    sSql += "   AND rstflg     = '3'"
                                    sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                                    sSql += "       (SELECT tclscd, tspccd, testcd FROM rf062m WHERE grprstyn = '1')"

                                    dbCmd.CommandText = sSql

                                    With dbCmd
                                        .Parameters.Clear()
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                    End With
                                End If

                            Case "1"
                                sSql = ""
                                sSql += "UPDATE rr010m"
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :rstid), regdt = NVL(regdt, :rstdt),"
                                sSql += "       mwid   = NULL,               mwdt  = NULL,"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno       = :bcno"
                                sSql += "   AND tclscd     = :testcd"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR NVL(rstflg, ' ') <> ' ')"
                                sSql += "   AND rstflg    <> '1'"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM rf062m WHERE grprstyn = '1')"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                End With

                            Case "2"
                                sSql = ""
                                sSql += "UPDATE rr010m"
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :rstid), regdt = NVL(regdt, :rstdt),"
                                sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno       = :bcno"
                                sSql += "   AND tclscd     = :testcd"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR NVL(rstflg, ' ') <> ' ')"
                                sSql += "   AND rstflg    <> '2'"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM rf062m WHERE grprstyn = '1')"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                End With
                        End Select

                    End If

                    If Not sSql = "" Then
                        Dim iRet As Integer = dbCmd.ExecuteNonQuery()
                    End If

                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 삭제  fnEdit_rj011m_new로 새로생성함 
        Private Function fnEdit_rj011m(ByVal rsBcNo As String) As Integer
            Dim sFn As String = "Private Function fnEdit_rj011m(String) As Integer"
            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT r.tclscd, r.spccd, MIN(NVL(r.rstflg, '0')) minrstflg, MAX(NVL(r.rstflg, '0')) maxrstflg, MAX(r.rstdt) rstdt"
                sSql += "  FROM rr010m r, rf060m f"
                sSql += " WHERE r.bcno     = :bcno"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND r.tkdt    >= f.usdt"
                sSql += "   AND r.tkdt    <  f.uedt"
                sSql += "   AND (f.tcdgbn IN ('S', 'P') OR (f.tcdgbn = 'B' AND NVL(f.titleyn, '0') = '0'))"
                sSql += " GROUP BY r.tclscd, r.spccd"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then Return 0

                Dim sRstFlg As String = ""
                Dim iRet As Integer = 0

                For ix As Integer = 1 To dt.Rows.Count
                    If dt.Rows(ix - 1).Item("minrstflg").ToString() = dt.Rows(ix - 1).Item("maxrstflg").ToString() Then
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString() = "0" And dt.Rows(ix - 1).Item("maxrstflg").ToString() <= "3" Then
                        sRstFlg = "1"
                    Else
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    End If

                    sSql = ""

                    Select Case sRstFlg
                        Case "0"
                            sSql += "UPDATE rj011m SET rstflg = NULL, rstdt = NULL, editdt = fn_ack_sysdate, editid = :editid, editip = :editip"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()

                                .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("tclscd").ToString()
                            End With

                        Case Else
                            sSql += "UPDATE rj011m SET rstflg = :rstflg, rstdt = :rstdt, editdt = fn_ack_sysdate, editid = :editid, editip = :editip"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"
                            sSql += "   AND spcflg = '4'"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()
                                .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("rstdt").ToString()

                                .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("tclscd").ToString()
                            End With
                    End Select

                    If Not sSql = "" Then
                        iRet += dbCmd.ExecuteNonQuery()
                    End If
                Next

                Return 1
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_rj010m(ByVal rsBcNo As String) As Integer
            Dim sFn As String = "Private Function fnEdit_rj010m(String) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT MIN(NVL(j.rstflg, '0')) minrstflg, MAX(NVL(j.rstflg, '0')) maxrstflg"
                sSql += "  FROM rj011m j"
                sSql += " WHERE j.bcno = :bcno"
                sSql += "   AND NVL(j.spcflg, '0') NOT IN ('0', 'R')"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then Return 0

                Dim sRstflg As String = ""
                Dim iRet As Integer = 0

                If dt.Rows(0).Item("maxrstflg").ToString() = "0" Then
                    sRstflg = ""
                ElseIf dt.Rows(0).Item("minrstflg").ToString() = "3" And dt.Rows(0).Item("minrstflg").ToString() = "3" Then
                    sRstflg = "2"
                Else
                    sRstflg = "1"
                End If

                sSql = ""
                sSql += "UPDATE rj010m SET rstflg = :rstflg, editdt = fn_ack_sysdate, editid = :editid, editip = :editip"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND spcflg = '4'"
                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstflg

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With

                Return 1
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Private Function fnEdit_rr020m(ByVal roCmt As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_rr020m(ArrayList, String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim strSrvDt As String = fnGet_Server_DateTime()
                Dim sSql As String = ""
                Dim intRanking As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                For intIdx As Integer = 0 To roCmt.Count - 1
                    If intIdx = 0 Then

                        sSql = ""
                        sSql += "INSERT INTO rr020h("
                        sSql += "       bcno, rstseq, moddt,           modid,  modip,  cmt, regid, regdt ) "
                        sSql += "SELECT bcno, rstseq, fn_ack_sysdate, :modid, :modip,  cmt, regid, regdt"
                        sSql += "  FROM rr020m"
                        sSql += " WHERE bcno = :bcno"

                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(intIdx), ResultInfo_Cmt).BcNo

                            .ExecuteNonQuery()
                        End With

                        sSql = ""
                        sSql += "DELETE rr020m WHERE bcno = :bcno"

                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(intIdx), ResultInfo_Cmt).BcNo
                            .ExecuteNonQuery()
                        End With
                    End If

                    sSql = ""
                    sSql += "INSERT INTO rr020m(  bcno,  rstseq,  cmt, regdt,           regid,  editid,  editip, editdt )"
                    sSql += "            VALUES( :bcno, :rstSeq, :cmt, fn_ack_sysdate, :regid, :editid, :editip, fn_ack_sysdate)"

                    With dbCmd
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(intIdx), ResultInfo_Cmt).BcNo
                        .Parameters.Add("rstseq", OracleDbType.Varchar2).Value = (intIdx + 1).ToString
                        .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(roCmt(intIdx), ResultInfo_Cmt).Cmt
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .ExecuteNonQuery()
                    End With
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Private Function fnEdit_rr040m(ByVal roCmt As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_rr040m(ArrayList, String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim alSlipCd As New ArrayList
                Dim bAddFlg As Boolean = False

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                For ix As Integer = 0 To roCmt.Count - 1

                    If alSlipCd.Contains(CType(roCmt(ix), ResultInfo_Cmt).PartSlip) Then

                    Else
                        bAddFlg = False

                        sSql = ""
                        sSql += "INSERT INTO lr040h "
                        sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.* FROM rr040m r WHERE bcno = :bcno AND partcd = :partcd AND slipcd = :slipcd"
                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).BcNo
                            .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(0, 1)
                            .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(1, 1)

                            .ExecuteNonQuery()
                        End With


                        sSql = ""
                        sSql += "DELETE rr040m WHERE bcno = :bcno AND partcd = :partcd AND slipcd = :slipcd"

                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).BcNo
                            .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(0, 1)
                            .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(1, 1)

                            .ExecuteNonQuery()
                        End With
                    End If

                    alSlipCd.Add(CType(roCmt(ix), ResultInfo_Cmt).PartSlip)

                    If CType(roCmt(ix), ResultInfo_Cmt).Cmt <> vbCrLf Or CType(roCmt(ix), ResultInfo_Cmt).Cmt <> "" Then bAddFlg = True

                    If CType(roCmt(ix), ResultInfo_Cmt).Cmt <> vbCrLf Or CType(roCmt(ix), ResultInfo_Cmt).Cmt <> "" Or bAddFlg Then
                        sSql = ""
                        sSql += "INSERT INTO rr040m"
                        sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt, regdt,           regid,  editid,  editip, editdt )"
                        sSql += "   VALUES ( :bcno, :Partcd, :slipcd, :rstseq, :cmt, fn_ack_sysdate, :regid, :editid, :editip, fn_ack_sysdate)"


                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).BcNo
                            .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(0, 1)
                            .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(1, 1)
                            .Parameters.Add("rstSeq", OracleDbType.Varchar2).Value = (ix + 1).ToString
                            .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).Cmt
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            .ExecuteNonQuery()
                        End With
                    End If
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

    End Class

    '-- I/F에서 결과 등록
    Public Class RegFn
        '2005/08/05 freety : 전체적으로 Prestatement Query로 변경

        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.RegFn" + vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Private m_dt_rst As DataTable

        Private m_al_ParentCd As ArrayList
        Private m_b_SpecialTest As Boolean = False

        'IF -> 0, LIS -> 1
        Private miUseCase As Integer = 0
        Private mbNotUseALimit As Boolean = False

        Public Sub DbRollback()
            Try
                m_dbTran.Rollback()

            Catch ex As Exception

            End Try
        End Sub

        Public Sub New()
            miUseCase = 1
        End Sub

        Public Sub New(ByVal rbNotUseALimit As Boolean)
            miUseCase = 1

            mbNotUseALimit = rbNotUseALimit
        End Sub

        Public Sub New(ByVal riUseCase As Integer)
            If riUseCase = 0 Then
                miUseCase = 0

                'm_dbCn = GetDbConnection()
                'm_dbTran = m_dbCn.BeginTransaction()
                'COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
            Else
                miUseCase = 1
            End If
        End Sub

        Private Function fnGet_Server_DateTime() As String

            Dim sFn As String = "Private Function fnGet_Server_DateTime() As string"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql += "SELECT fn_ack_sysdate FROM DUAL"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("srvdate").ToString()
                Else
                    Return Format(Now, "yyyyMMddHHmm:s").ToString
                End If

            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmm").ToString
            End Try

        End Function

        '-- 2008-01-08 Yej Add
        Private Function fnGet_GraedValue(ByVal rsTclsCd As String, ByVal rsRstVal As String) As String
            Dim sFn As String = "Private Function fnGet_GraedValue(String, String) As String"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As New OracleDataAdapter
                Dim dt As New DataTable
                Dim sSql As String = ""

                Dim sValue As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                sSql = ""
                sSql += "SELECT grade FROM rf083m"
                sSql += " WHERE testcd  = :testcd"
                sSql += "   AND spccd   = '" + "".PadLeft(PRG_CONST.Len_SpcCd, "0"c) + "'"
                sSql += "   AND rstcont = :rstcont"

                dbCmd.CommandText = sSql
                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                    .SelectCommand.Parameters.Add("rstcont", OracleDbType.Varchar2).Value = rsRstVal
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then sValue = dt.Rows(0).Item(0).ToString().Trim

                Return sValue

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Public Function RegServer(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList, ByVal r_dbCn As OracleConnection, ByVal r_dbTran As OracleTransaction) As Integer
            Dim sFn As String = "Function RegServer"

            m_dbCn = r_dbCn
            m_dbTran = r_dbTran

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim rstinfo_Buf As STU_RstInfo

                Dim alCvtRstInfo As New ArrayList
                Dim alCvtCmtInfo As New ArrayList

                'Log 남기기
                '< mod freety 2005/03/18
                '# 한 프로세스에 멀티장비용으로 수정
                'RegRstFn.Log("RegServer 시작 - " + r_sampinfo_Buf.BCNo)
                LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 시작 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                '> mod freety 2005/03/18

                '1) 결과개수만큼 등록
                For i As Integer = 1 To r_al_RstInfo.Count
                    rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                    If rstinfo_Buf.EqFlag Is Nothing Then rstinfo_Buf.EqFlag = ""
                    If rstinfo_Buf.RstCmt Is Nothing Then rstinfo_Buf.RstCmt = ""

                    If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                        iRegOK_Sum += 1

                        r_al_EditSuc.Add(rstinfo_Buf.TestCd)

                        '-- 소견 자동등록에서 필요
                        Dim oCvtCmtInfo As New STU_CvtCmtInfo
                        With oCvtCmtInfo
                            .BcNo = r_sampinfo_Buf.BCNo
                            .TestCd = rstinfo_Buf.TestCd
                            .OrgRst = rstinfo_Buf.OrgRst
                            .ViewRst = rstinfo_Buf.ViewRst
                        End With

                        alCvtCmtInfo.Add(oCvtCmtInfo)

                        '-- 결과값 자동변경에서 필요
                        Dim oCvtRstInfo As New STU_RstInfo_cvt
                        With oCvtRstInfo
                            .BcNo = r_sampinfo_Buf.BCNo
                            .TestCd = rstinfo_Buf.TestCd
                            .OrgRst = rstinfo_Buf.OrgRst
                            .ViewRst = rstinfo_Buf.ViewRst
                        End With

                        alCvtRstInfo.Add(oCvtRstInfo)

                    End If
                Next

                If r_al_EditSuc.Count = 0 Then Return iRegOK_Sum

                '1-1) 계산식 관련항목 등록
                Try
                    Dim al_RstInfo_Calc As ArrayList = fnCalcRstInfo(r_sampinfo_Buf, r_al_RstInfo)

                    If Not al_RstInfo_Calc Is Nothing Then
                        If al_RstInfo_Calc.Count > 0 Then
                            For i As Integer = 1 To al_RstInfo_Calc.Count
                                rstinfo_Buf = CType(al_RstInfo_Calc(i - 1), STU_RstInfo)

                                If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                                    iRegOK_Sum += 1

                                    r_al_EditSuc.Add(rstinfo_Buf.TestCd)
                                End If
                            Next
                        End If
                    End If

                Catch ex As Exception
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 계산식 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                End Try
                '>

                '-- 1-2) 결과값 자동 등록
                Try
                    Dim al_RstInfo_Cvt As ArrayList = fnCvtRstInfo(r_sampinfo_Buf, r_al_EditSuc, m_dbTran, m_dbCn)

                    If Not al_RstInfo_Cvt Is Nothing Then
                        If al_RstInfo_Cvt.Count > 0 Then
                            For i As Integer = 1 To al_RstInfo_Cvt.Count
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i), STU_RstInfo_cvt))
                            Next
                        End If
                    End If
                Catch ex As Exception
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 결과값 자동변환 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                End Try

                '2) Sub 항목 에 대한 상태 재조정(Parent 및 Child)
                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Battery
                fnEdit_LR_Battery(r_sampinfo_Buf)

                '4) Update rj011m
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update rj010m
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) Upate rr040m(검사분류별 소견)
                Call fnEdit_rr040m(r_sampinfo_Buf) '-- 자동소견

                '-- 2009-09-15 YEJ (감염정보)
                If fnEdit_OCS(r_sampinfo_Buf) Then
                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_Sum
                Else
                    Return 0
                End If

            Catch ex As Exception
                LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 에러 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnGet_CalcRstInfo_BcNo(ByVal rsBcNo As String, Optional ByVal rbAuto As Boolean = False) As DataTable
            Dim sFn As String = "Public Shared Function Get_CalcRstInfo_BcNo(String, (Boolean), (Object)) As DataTable"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT b.* FROM rr010m a,"
                sSql += "       (SELECT 1, c.calform, r.bcno, r.testcd ctestcd, r.testcd, f.tnmd, r.orgrst, r.rstflg,"
                sSql += "               c.param0 || '/' || NVL(c.param1, '')"
                sSql += "               || '/' || NVL(c.param2, '') || '/' || NVL(c.param3, '')"
                sSql += "               || '/' || NVL(c.param4, '') || '/' || NVL(c.param5, '')"
                sSql += "               || '/' || NVL(c.param6, '') || '/' || NVL(c.param7, '')"
                sSql += "               || '/' || NVL(c.param8, '') || '/' || NVL(c.param9, '') calitems,"
                sSql += "               f.dispseql sortpkey, 0 sortskey, c.caldays , c.calrange"
                sSql += "          FROM rr010m r, rf069m c, rf060m f"
                sSql += "         WHERE r.bcno   = :bcno"
                sSql += "           AND r.testcd = c.testcd"
                sSql += "           AND r.spccd  = c.spccd"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "           AND r.tkdt  >= f.usdt"
                sSql += "           AND r.tkdt  <  f.uedt"

                If rbAuto Then
                    sSql += "           AND NVL(c.caltype, 'M') = 'A'"
                End If

                sSql += "        UNION ALL"
                sSql += "        SELECT 2, CASE RPAD(r.testcd, 7, ' ') || r.spccd"
                sSql += "                       WHEN c.param0 THEN 'A'"
                sSql += "                       WHEN c.param1 THEN 'B'"
                sSql += "                       WHEN c.param2 THEN 'C'"
                sSql += "                       WHEN c.param3 THEN 'D'"
                sSql += "                       WHEN c.param4 THEN 'E'"
                sSql += "                       WHEN c.param5 THEN 'F'"
                sSql += "                       WHEN c.param6 THEN 'G'"
                sSql += "                       WHEN c.param7 THEN 'H'"
                sSql += "                       WHEN c.param8 THEN 'I'"
                sSql += "                       WHEN c.param9 THEN 'J'"
                sSql += "                       ELSE '-'"
                sSql += "                  END calform,"
                sSql += "               r.bcno, c.testcd ctclscd, r.testcd, f.tnmd, r.orgrst, r.rstflg,"
                sSql += "               '' calitems,"
                sSql += "               f.dispseql sortpkey,"
                sSql += "               CASE RPAD(r.testcd, 7, ' ') || r.spccd"
                sSql += "                    WHEN c.param0 THEN 10"
                sSql += "                    WHEN c.param1 THEN 11"
                sSql += "                    WHEN c.param2 THEN 12"
                sSql += "                    WHEN c.param3 THEN 13"
                sSql += "                    WHEN c.param4 THEN 14"
                sSql += "                    WHEN c.param5 THEN 15"
                sSql += "                    WHEN c.param6 THEN 16"
                sSql += "                    WHEN c.param7 THEN 17"
                sSql += "                    WHEN c.param8 THEN 18"
                sSql += "                    WHEN c.param9 THEN 19"
                sSql += "                    ELSE 20"
                sSql += "               END sortskey, c.caldays , c.calrange"
                sSql += "          FROM rr010m r, rf069m c, rf060m f"
                sSql += "         WHERE r.bcno = :bcno"
                sSql += "           AND RPAD(r.testcd, 7, ' ') || r.spccd"
                sSql += "            IN ("
                sSql += "                TRIM(c.param0), TRIM(c.param1), TRIM(c.param2), TRIM(c.param3), TRIM(c.param4),"
                sSql += "                TRIM(c.param5), TRIM(c.param6), TRIM(c.param7), TRIM(c.param8), TRIM(c.param9)"
                sSql += "               )"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "           AND r.tkdt  >= f.usdt"
                sSql += "           AND r.tkdt  <  f.uedt"

                If rbAuto Then
                    sSql += "           AND NVL(c.caltype, 'M') = 'A'"
                End If

                sSql += "       ) b"
                sSql += " WHERE a.bcno   = :bcno"
                sSql += "   AND a.testcd = b.ctestcd"
                sSql += " ORDER BY ctestcd, 1, calform, sortpkey,  sortskey"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnCalcRstInfo_Find_STU_RstInfo_calc_BcNo(ByVal rsBcNo As String) As ArrayList
            Dim sFn As String = "fnCalcRstInfo_Find_STU_RstInfo_calc_BcNo"

            Try
                Dim dt As DataTable = fnGet_CalcRstInfo_BcNo(rsBcNo, True)

                If dt Is Nothing Then Return New ArrayList
                If dt.Rows.Count = 0 Then Return New ArrayList

                Dim al_cri As New ArrayList

                For i As Integer = 1 To dt.Rows.Count
                    Dim cri As STU_RstInfo_calc = New STU_RstInfo_calc

                    With dt.Rows(i - 1)
                        cri.CalForm = .Item("calform").ToString
                        cri.CalItems = .Item("calitems").ToString
                        cri.CTestCd = .Item("ctestcd").ToString
                        cri.TestCd = .Item("testcd").ToString
                        cri.TNmD = .Item("tnmd").ToString
                        cri.OrgRst = .Item("orgrst").ToString
                        cri.RstFlg = .Item("rstflg").ToString
                        cri.BcNo = .Item("bcno").ToString

                        cri.CalDsys = .Item("caldays").ToString
                        cri.CalRange = .Item("calrange").ToString
                    End With

                    al_cri.Add(cri)

                    cri = Nothing
                Next

                Return al_cri

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnGet_CalcState_BcNo(ByVal rsBcNo As String, Optional ByVal rbAuto As Boolean = False) As DataTable
            Dim sFn As String = "fnGet_CalcState_BcNo"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT r.bcno, MIN(NVL(r.rstflg, '0')) minrstflg, NVL(c.calview, 'A') calview"
                sSql += "  FROM rr010m r, rf069m c"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND r.testcd = c.testcd"
                sSql += "   AND r.spccd  = c.spccd"

                If rbAuto Then
                    sSql += "    and NVL(c.caltype, 'M') = 'A'"
                End If
                sSql += "  group by r.bcno, c.calview"

                Dim lisdbcmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnCalcRstInfo_Find_CalcState(ByVal rsBcNo As String) As Boolean
            Dim sFn As String = "Private Function fnCalcRstInfo_Find_CalcState(String) As Boolean"

            Try
                Dim dt As DataTable = fnGet_CalcState_BcNo(rsBcNo, True)
                Dim bExist As Boolean = False

                If dt IsNot Nothing Then
                    If dt.Rows.Count > 0 Then
                        bExist = True
                    End If
                End If

                If bExist = False Then
                    Return False
                End If

                bExist = True

                Dim bFinal As Boolean = False

                If dt.Rows(0).Item("minrstflg").ToString > "2" Then
                    bFinal = True
                End If

                If bFinal Then
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnCalcRstInfo(ByVal r_SampInfo As STU_SampleInfo, ByVal r_al_RstInfo As ArrayList) As ArrayList
            Dim sFn As String = "Public Function fnCalcRstInfo(STU_SampleInfo, ArrayList) As ArrayList"
            Try
                Dim sBcNo As String = r_SampInfo.BCNo

                Dim bFind As Boolean = fnCalcRstInfo_Find_CalcState(sBcNo)

                If bFind = False Then
                    Return Nothing
                End If

                Dim al_cri_bcno As ArrayList = fnCalcRstInfo_Find_STU_RstInfo_calc_BcNo(sBcNo)

                If al_cri_bcno.Count = 0 Then
                    Return Nothing
                End If

                Dim al_CTestCds As New ArrayList
                Dim al_CTestCds_RegStep As New ArrayList

                For i As Integer = 1 To r_al_RstInfo.Count
                    Dim sCRegStep As String = CType(r_al_RstInfo(i - 1), STU_RstInfo).RegStep

                    For j As Integer = 1 To al_cri_bcno.Count
                        Dim sCTestCd As String = CType(al_cri_bcno(j - 1), STU_RstInfo_calc).CTestCd
                        Dim sTestCd As String = CType(al_cri_bcno(j - 1), STU_RstInfo_calc).TestCd

                        If CType(r_al_RstInfo(i - 1), STU_RstInfo).TestCd = sTestCd Then
                            Dim iExist As Integer = 0

                            For a As Integer = 1 To al_CTestCds.Count
                                If al_CTestCds(a - 1).ToString = sCTestCd Then
                                    iExist = a
                                End If
                            Next

                            If iExist = 0 Then
                                al_CTestCds.Add(sCTestCd)
                                al_CTestCds_RegStep.Add(sCRegStep)
                            Else
                                If sCRegStep < al_CTestCds_RegStep(iExist - 1).ToString Then
                                    al_CTestCds_RegStep(iExist - 1) = sCRegStep
                                End If
                            End If
                        End If
                    Next
                Next

                If al_CTestCds.Count = 0 Then
                    Return Nothing
                End If

                For i As Integer = 1 To al_CTestCds.Count
                    Dim sCTestCd As String = al_CTestCds(i - 1).ToString
                    Dim sCRegStep As String = al_CTestCds_RegStep(i - 1).ToString

                    fnCalcRstInfo_Find_STU_RstInfo_calc_Pat(al_cri_bcno, sBcNo, sCTestCd, sCRegStep)

                    '> RegStep 계산식 관련검사의 가장 낮은 단계로 조정
                    If sCRegStep < al_CTestCds_RegStep(i - 1).ToString Then
                        al_CTestCds_RegStep(i - 1) = sCRegStep
                    End If
                Next

                Dim al_RstInfo_Calc As New ArrayList

                For i As Integer = 1 To al_CTestCds.Count
                    Dim iIdx As Integer = fnCalcRstInfo_Proc_Caculate(al_cri_bcno, al_CTestCds(i - 1).ToString)

                    If iIdx < 0 Then Continue For

                    Dim ri As STU_RstInfo = New STU_RstInfo

                    ri.TestCd = CType(al_cri_bcno(iIdx), STU_RstInfo_calc).TestCd
                    ri.OrgRst = CType(al_cri_bcno(iIdx), STU_RstInfo_calc).OrgRst
                    ri.RstCmt = ""
                    ri.RegStep = al_CTestCds_RegStep(i - 1).ToString

                    al_RstInfo_Calc.Add(ri)

                    ri = Nothing
                Next

                Return al_RstInfo_Calc

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnGet_CvtRst_State_BcNo(ByVal rsBcNo As String, Optional ByVal r_al_TestInfo As ArrayList = Nothing) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtRst_State_BcNo(String, (Boolean), (Object)) As DataTable"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim sTestCds As String = ""

                If Not r_al_TestInfo Is Nothing Then
                    For ix As Integer = 0 To r_al_TestInfo.Count - 1
                        sTestCds += IIf(ix > 0, ",", "").ToString + r_al_TestInfo(0).ToString
                    Next
                End If

                sSql = ""
                sSql += "SELECT r.bcno, r.testcd, r.spccd, r.orgrst, r.viewrst, r.rstcmt,"
                sSql += "       c.rstcdseq, c.cvtrange, c.cvtform, c.cvtfldgbn, d.rstcont, r.rstflg"
                sSql += "  FROM rr010m r, rf084m c, rf083m d"
                sSql += " WHERE r.bcno     = :bcno"

                If sTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + sTestCds.Replace(",", "','") + "')"
                End If

                sSql += "   AND r.testcd   = c.testcd"
                sSql += "   AND r.spccd    = c.spccd"
                sSql += "   AND c.testcd   = d.testcd"
                sSql += "   AND c.rstcdseq = d.rstcdseq"
                sSql += "   AND NVL(r.rstflg, '0') IN ('0', '1', '2', '3')"

                Dim lisdbcmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                End With

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnGet_CvtRstInfo(ByVal rsBcNo As String, Optional ByVal r_al_TestInfo As ArrayList = Nothing) As ArrayList
            Dim sFn As String = "Private Function fnCvtRstInfo_State(String) As Boolean"

            Try
                Dim dt As DataTable = fnGet_CvtRst_State_BcNo(rsBcNo, r_al_TestInfo)
                Dim alList As New ArrayList

                Dim bExist As Boolean = False

                If dt.Rows.Count < 1 Then Return New ArrayList

                Dim bFinal As Boolean = False

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(0).Item("rstflg").ToString > "2" Then
                    Else
                        Dim objCvt As New STU_RstInfo_cvt

                        objCvt.BcNo = dt.Rows(intIdx).Item("bcno").ToString.Trim
                        objCvt.TestCd = dt.Rows(intIdx).Item("testcd").ToString.Trim
                        objCvt.SpcCd = dt.Rows(intIdx).Item("spccd").ToString.Trim
                        objCvt.OrgRst = dt.Rows(intIdx).Item("orgrst").ToString.Trim
                        objCvt.ViewRst = dt.Rows(intIdx).Item("viewrst").ToString.Trim
                        objCvt.RstCmt = dt.Rows(intIdx).Item("rstcmt").ToString.Trim
                        objCvt.RstCdSeq = dt.Rows(intIdx).Item("rstcdseq").ToString.Trim
                        objCvt.CvtForm = dt.Rows(intIdx).Item("cvtform").ToString.Trim
                        objCvt.CvtFldGbn = dt.Rows(intIdx).Item("cvtfldgbn").ToString.Trim
                        objCvt.CvtRange = dt.Rows(intIdx).Item("cvtrange").ToString.Trim
                        objCvt.RstCont = dt.Rows(intIdx).Item("rstcont").ToString.Trim

                        alList.Add(objCvt)

                    End If
                Next

                Return alList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function fnGet_CvtRstInfo_BcNo(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String, Optional ByVal r_objLisdbTran As Object = Nothing) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtRstInfo_BcNo(String, (Object)) As DataTable"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim sSql As String

                sSql = ""
                sSql += "SELECT r.bcno, r.testcd, r.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "       f.tnmd, r.orgrst, r.viewrst, r.lhmark, MIN(NVL(r.rstflg, '0')) rstflg"
                sSql += "  FROM rr010m r, rf085m c, rf060m f"
                sSql += " WHERE r.bcno     = :bcno"
                sSql += "   AND c.testcd   = :testcd"
                sSql += "   AND c.spccd    = :spccd"
                sSql += "   AND c.rstcdseq = :rstcdseq"
                sSql += "   AND r.testcd   = c.ctestcd"
                sSql += "   AND r.spccd    = c.cspccd"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND r.tkdt    >= f.usdt"
                sSql += "   AND r.tkdt    <  f.uedt"
                sSql += " GROUP BY r.bcno, r.testcd, r.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "          f.tnmd, r.orgrst, r.viewrst, r.lhmark"

                Dim lisdbcmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                    .SelectCommand.Parameters.Add("spccd", OracleDbType.Varchar2).Value = rsSpcCd
                    .SelectCommand.Parameters.Add("rstcdseq", OracleDbType.Varchar2).Value = rsRstCd
                End With

                dbDa.Fill(dt)

                Return dt
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function fnGet_CvtRstInfo_RegNo(ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsSpcCd As String, ByVal rsRstCd As String, Optional ByVal r_objLisdbTran As Object = Nothing) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CvtRstInfo_RegNo(String, string, string, string, [Object]) As DataTable"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim sSql As String

                sSql = ""
                sSql += "SELECT r.bcno, r.testcd, r.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "       f.tnmd, r.orgrst, r.viewrst, r.lhmark, MIN(NVL(r.rstflg, '0')) rstflg,"
                sSql += "  FROM rr010m r, rj010m j, rf085m c, rf060m f"
                sSql += " WHERE (j.regno, j.orddt) = (SELECT regno, orddt FROM rj010m WHERE bcno = :bcno)"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.testcd   = :testcd"
                sSql += "   AND r.spccd    = :spccd"
                sSql += "   AND c.rstcdseq = :rstcdseq"
                sSql += "   AND r.testcd   = c.testcd"
                sSql += "   AND r.spccd    = c.spccd"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND r.tkdt    >= f.usdt"
                sSql += "   AND r.tkdt    <  f.uedt"
                sSql += " GROUP BY r.bcno, r.testcd, r.spccd, c.cvtparam, c.ctestcd, c.reflgbn, c.refl, c.refls, c.refhgbn, c.refh, c.refhs, c.reflt, c.reflts,"
                sSql += "          f.tnmd, r.orgrst, r.viewrst, r.lhmark"


                Dim lisdbcmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                    .SelectCommand.Parameters.Add("spccd", OracleDbType.Varchar2).Value = rsSpcCd
                    .SelectCommand.Parameters.Add("rstcdseq", OracleDbType.Varchar2).Value = rsRstCd
                End With

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnGet_CvtRstInfo_Items(ByVal rsRange As String, ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsSpcCd As String, ByVal rsRstSeq As String, ByVal rsOrgRst As String) As ArrayList
            Dim sFn As String = "Private Function fnGet_CvtRstInfo_Items(string, string, string, String) As ArrayList"

            Try
                Dim dt As DataTable = fnGet_CvtRstInfo_BcNo(rsBcNo, rsTclsCd, rsSpcCd, rsRstSeq)

                Dim alList As New ArrayList

                If dt.Rows.Count < 1 Then Return New ArrayList

                For intIdx As Integer = 0 To dt.Rows.Count - 1
                    Dim objCvt As New STU_RstInfo_cvt

                    objCvt.TestCd = dt.Rows(intIdx).Item("testcd").ToString.Trim
                    objCvt.SpcCd = dt.Rows(intIdx).Item("spccd").ToString.Trim
                    objCvt.RstFlg = dt.Rows(intIdx).Item("rstflg").ToString.Trim
                    objCvt.CvtParam = dt.Rows(intIdx).Item("cvtparam").ToString.Trim
                    objCvt.CTestCd = dt.Rows(intIdx).Item("ctescdt").ToString.Trim
                    objCvt.OrgRst = IIf(dt.Rows(intIdx).Item("orgrst").ToString.Trim = "", rsOrgRst, dt.Rows(intIdx).Item("orgrst").ToString.Trim).ToString
                    objCvt.ViewRst = dt.Rows(intIdx).Item("viewrst").ToString.Trim
                    objCvt.HlMark = dt.Rows(intIdx).Item("lhmark").ToString.Trim
                    objCvt.BcNo = dt.Rows(intIdx).Item("bcno").ToString.Trim

                    Dim strCalcL As String = ""
                    Dim strCalcH As String = ""
                    Dim strCalcC As String = ""

                    If dt.Rows(intIdx).Item("refl").ToString.Trim <> "" Then
                        Select Case dt.Rows(intIdx).Item("reflgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(intIdx).Item("refls").ToString.Trim
                                    Case "0" : strCalcL = "[ro] > " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                    Case "1" : strCalcL = "[ro] >= " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                End Select
                            Case "2"
                                Select Case dt.Rows(intIdx).Item("refls").ToString.Trim
                                    Case "0" : strCalcL = "[rv] > " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                    Case "1" : strCalcL = "[rv] >= " + dt.Rows(intIdx).Item("refl").ToString.Trim
                                End Select
                        End Select
                    End If

                    If dt.Rows(intIdx).Item("refh").ToString.Trim <> "" Then
                        Select Case dt.Rows(intIdx).Item("refhgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(intIdx).Item("refhs").ToString.Trim
                                    Case "0" : strCalcH = "[ro] < " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "1" : strCalcH = "[ro] <= " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "2"
                                        strCalcH = "[ro] = " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                        strCalcL = ""
                                End Select
                            Case "2"
                                Select Case dt.Rows(intIdx).Item("refhs").ToString.Trim
                                    Case "0" : strCalcH = "[rv] < " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "1" : strCalcH = "[rv] <= " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                    Case "2"
                                        strCalcH = "[rv] = " + dt.Rows(intIdx).Item("refh").ToString.Trim
                                        strCalcL = ""
                                End Select
                            Case "3"
                                strCalcH = "{rj} = '" + dt.Rows(intIdx).Item("refh").ToString.Trim + "'"
                                strCalcL = ""
                        End Select
                    End If

                    If dt.Rows(intIdx).Item("reflt").ToString.Trim <> "" Then
                        strCalcL = "" : strCalcH = ""
                        Select Case dt.Rows(intIdx).Item("refhgbn").ToString.Trim
                            Case "1"
                                Select Case dt.Rows(intIdx).Item("reflts").ToString.Trim
                                    Case "0" : strCalcC = "{ro} = '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "1" : strCalcC = "{ro} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%"
                                    Case "2" : strCalcC = "{ro} like '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "3" : strCalcC = "{ro} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "4" : strCalcC = "{ro} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "5" : strCalcC = "{ro} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                End Select
                            Case "2"
                                Select Case dt.Rows(intIdx).Item("reflts").ToString.Trim
                                    Case "0" : strCalcC = "{rv} = '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "1" : strCalcC = "{rv} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%"
                                    Case "2" : strCalcC = "{rv} like '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "3" : strCalcC = "{rv} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "%'"
                                    Case "4" : strCalcC = "{rv} like '%" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                    Case "5" : strCalcC = "{rv} <> '" + dt.Rows(intIdx).Item("reflt").ToString.Trim + "'"
                                End Select
                        End Select
                    End If

                    objCvt.CondiExp = ""
                    If strCalcC <> "" Then
                        objCvt.CondiExp = strCalcC
                    Else
                        If strCalcL <> "" And strCalcH <> "" Then
                            objCvt.CondiExp = "(" + strCalcL + " AND " + strCalcH + ")"
                        ElseIf strCalcL <> "" Then
                            objCvt.CondiExp = strCalcL
                        ElseIf strCalcH <> "" Then
                            objCvt.CondiExp = strCalcH
                        End If
                    End If

                    alList.Add(objCvt)
                Next

                Return alList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnCvtRstInfo(ByVal r_SampInfo As STU_SampleInfo, ByVal r_al_TestInfo As ArrayList, _
                                     ByVal r_DbTrans As OracleTransaction, _
                                     ByVal r_DbCn As OracleConnection) As ArrayList
            Dim sFn As String = "Public Function fnCvtRstInfo(STU_SampleInfo,  ArrayList, [oracleTransaction], [oracleConnection]) As ArrayList"

            ''- 장비에서 사용하는 결과값 자동변환

            If r_DbCn Is Nothing Then
                m_dbCn = r_DbCn
                m_dbTran = r_DbTrans
            End If

            Try

                Dim sBcNo As String = r_SampInfo.BCNo

                Dim arlRstInfo As ArrayList = fnCvtRstInfo(r_SampInfo, r_al_TestInfo)
                Dim al_RstInfo_Cvt As New ArrayList

                For intIdx As Integer = 0 To arlRstInfo.Count - 1

                    If CType(arlRstInfo(intIdx), STU_RstInfo_cvt).OrgRst <> "" Then

                        Dim ri As STU_RstInfo = New STU_RstInfo

                        ri.TestCd = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).TestCd
                        ri.OrgRst = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).OrgRst
                        ri.ViewRst = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).ViewRst
                        ri.RstCmt = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).RstCmt
                        ri.HlMark = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).HlMark
                        ri.RegStep = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).RstFlg
                        ri.ChageRst = ri.ViewRst

                        al_RstInfo_Cvt.Add(ri)

                        ri = Nothing
                    End If
                Next

                Return al_RstInfo_Cvt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnCvtRstInfo(ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal r_al_TestInfo As ArrayList) As ArrayList
            Dim sFn As String = "Public Function fnCvtRstInfo(STU_SampleInfo, ArrayList) As ArrayList"
            Try
                Dim alRerturn As New ArrayList

                Dim alCvt As ArrayList = fnGet_CvtRstInfo(r_sampinfo_Buf.BCNo, r_al_TestInfo)
                If alCvt.Count < 1 Then Return New ArrayList

                For ix As Integer = 0 To alCvt.Count - 1
                    Dim alCvt_Item As ArrayList = fnGet_CvtRstInfo_Items(CType(alCvt(ix), STU_RstInfo_cvt).CvtRange, r_sampinfo_Buf.BCNo, CType(alCvt(ix), STU_RstInfo_cvt).TestCd, CType(alCvt(ix), STU_RstInfo_cvt).SpcCd, CType(alCvt(ix), STU_RstInfo_cvt).RstCdSeq, CType(alCvt(ix), STU_RstInfo_cvt).OrgRst)
                    If alCvt_Item.Count > 0 Then

                        For ix1 As Integer = 0 To alCvt_Item.Count - 1
                            If CType(alCvt(ix), STU_RstInfo_cvt).TestCd = CType(alCvt_Item(ix1), STU_RstInfo_cvt).TestCd And _
                               CType(alCvt(ix), STU_RstInfo_cvt).SpcCd = CType(alCvt_Item(ix1), STU_RstInfo_cvt).SpcCd Then
                                If CType(alCvt_Item(ix1), STU_RstInfo_cvt).OrgRst <> "" Then
                                    CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("[ro]", CType(alCvt_Item(ix1), STU_RstInfo_cvt).OrgRst)
                                    CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("[rv]", CType(alCvt_Item(ix1), STU_RstInfo_cvt).ViewRst)

                                    CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{ro}", "'" + CType(alCvt_Item(ix1), STU_RstInfo_cvt).OrgRst + "'")
                                    CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{rv}", "'" + CType(alCvt_Item(ix1), STU_RstInfo_cvt).ViewRst + "'")
                                    CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp = CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp.Replace("{rj}", "'" + CType(alCvt_Item(ix1), STU_RstInfo_cvt).HlMark + "'")

                                    CType(alCvt(ix), STU_RstInfo_cvt).CvtForm = CType(alCvt(ix), STU_RstInfo_cvt).CvtForm.Replace("[" + CType(alCvt_Item(ix1), STU_RstInfo_cvt).CvtParam + "]", CType(alCvt_Item(ix1), STU_RstInfo_cvt).CondiExp)
                                End If
                            End If
                        Next

                        For ix1 = 65 To 90
                            CType(alCvt(ix), STU_RstInfo_cvt).CvtForm = CType(alCvt(ix), STU_RstInfo_cvt).CvtForm.Replace("[" + Chr(ix1) + "]", "2 = 1")
                        Next

                        CType(alCvt(ix), STU_RstInfo_cvt).CvtForm = CType(alCvt(ix), STU_RstInfo_cvt).CvtForm.Replace("$$", "AND").Replace("||", "OR")

                        Dim sSql As String = ""
                        Dim dt As New DataTable
                        Try
                            Dim dbCmd As New OracleCommand
                            Dim dbDa As OracleDataAdapter

                            sSql = "SELECT CASE WHEN " + CType(alCvt(ix), STU_RstInfo_cvt).CvtForm + " THEN '1' ELSE '0' END rst FROM DUAL"

                            dbCmd.Connection = m_dbCn
                            dbCmd.Transaction = m_dbTran
                            dbCmd.CommandType = CommandType.Text
                            dbCmd.CommandText = sSql

                            dbDa = New OracleDataAdapter(dbCmd)

                            dbDa.Fill(dt)
                            'dt = DbExecuteQuery()
                            If dt.Rows.Count > 0 Then
                                If dt.Rows(0).Item("rst").ToString = "1" Then
                                    Dim objRet As New STU_RstInfo_cvt

                                    objRet.TestCd = CType(alCvt(ix), STU_RstInfo_cvt).TestCd
                                    objRet.SpcCd = CType(alCvt(ix), STU_RstInfo_cvt).SpcCd
                                    objRet.BcNo = CType(alCvt(ix), STU_RstInfo_cvt).BcNo
                                    objRet.CvtFldGbn = CType(alCvt(ix), STU_RstInfo_cvt).CvtFldGbn
                                    objRet.RstFlg = CType(alCvt(ix), STU_RstInfo_cvt).RstFlg
                                    objRet.OrgRst = CType(alCvt(ix), STU_RstInfo_cvt).OrgRst

                                    If CType(alCvt(ix), STU_RstInfo_cvt).CvtFldGbn = "R" Then
                                        objRet.ViewRst = CType(alCvt(ix), STU_RstInfo_cvt).RstCont
                                        objRet.RstCmt = CType(alCvt(ix), STU_RstInfo_cvt).RstCmt
                                    Else
                                        objRet.ViewRst = CType(alCvt(ix), STU_RstInfo_cvt).OrgRst
                                        objRet.RstCmt = CType(alCvt(ix), STU_RstInfo_cvt).RstCont
                                    End If

                                    alRerturn.Add(objRet)
                                End If
                            End If
                        Catch ex As Exception
                        End Try

                    End If
                Next

                Return alRerturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnCalcRstInfo_Proc_Caculate(ByRef r_al_cri As ArrayList, ByVal rsCTestCd As String) As Integer
            Dim sFn As String = "fnCalcRstInfo_Proc_Caculate"

            Try
                Dim iIdx As Integer = -1

                For i As Integer = 1 To r_al_cri.Count
                    If CType(r_al_cri(i - 1), STU_RstInfo_calc).CTestCd = rsCTestCd Then
                        iIdx = i - 1

                        Exit For
                    End If
                Next

                If iIdx < 0 Then Return -1

                Dim sCalForm As String = CType(r_al_cri(iIdx), STU_RstInfo_calc).CalForm
                Dim sCalItems As String = CType(r_al_cri(iIdx), STU_RstInfo_calc).CalItems
                Dim a_sCalItemTmp As String() = sCalItems.Split(CChar("/"))
                Dim a_sCalItem As String() = Nothing

                For i As Integer = 1 To a_sCalItemTmp.Length
                    If a_sCalItemTmp(i - 1).Trim = "" Then
                        Exit For
                    End If

                    ReDim Preserve a_sCalItem(i - 1)

                    a_sCalItem(i - 1) = a_sCalItemTmp(i - 1).Trim
                Next

                If a_sCalItem Is Nothing Then Return -1
                If a_sCalItem.Length < 1 Then Return -1

                Dim iCntCalc As Integer = 0

                For i As Integer = 1 To a_sCalItem.Length
                    Dim sSymbol As String = Chr(Asc("A") + i - 1)
                    Dim sTestCd As String = a_sCalItem(i - 1).Substring(0, "LTEST99".Length).Trim
                    Dim sSpcCd As String = a_sCalItem(i - 1).Substring("LTEST99".Length).Trim

                    Dim iIdx1 As Integer = -1
                    Dim iIdx2 As Integer = -1

                    For a As Integer = iIdx + 1 To r_al_cri.Count
                        If CType(r_al_cri(a - 1), STU_RstInfo_calc).CalForm = sSymbol Then
                            iIdx1 = a - 1

                            Exit For
                        End If
                    Next

                    For a As Integer = iIdx + 1 To r_al_cri.Count
                        If CType(r_al_cri(a - 1), STU_RstInfo_calc).TestCd = sTestCd Then
                            iIdx2 = a - 1

                            Exit For
                        End If
                    Next

                    If iIdx1 <> iIdx2 Then Return -1
                    If iIdx1 <= iIdx Then Return -1

                    Dim sOrgRst As String = CType(r_al_cri(iIdx1), STU_RstInfo_calc).OrgRst

                    If IsNumeric(sOrgRst) = False Then Return -1

                    sCalForm = sCalForm.Replace(sSymbol, sOrgRst)

                    iCntCalc += 1
                Next

                If iCntCalc <> a_sCalItem.Length Then Return -1

                Dim sRstCalc As String = Find_Calculated_Result(sCalForm)

                Dim iLenDot As Integer = 0

                If sRstCalc.IndexOf(".") >= 0 Then
                    iLenDot = sRstCalc.Substring(sRstCalc.IndexOf(".") + 1).Trim.Length
                End If

                Dim dt_Settings As DataTable = Find_Calculated_Settings(rsCTestCd)

                If Not dt_Settings Is Nothing Then
                    Dim strRstLLen As String = ""
                    Dim strRstULen As String = ""
                    Dim strRstType As String = ""
                    Dim strCutOpt As String = ""

                    strRstType = dt_Settings.Rows(0).Item("rsttype").ToString()
                    strRstLLen = dt_Settings.Rows(0).Item("rstllen").ToString()
                    strRstULen = dt_Settings.Rows(0).Item("rstulen").ToString()
                    strCutOpt = dt_Settings.Rows(0).Item("cutopt").ToString()

                    If (strRstType = "0" Or strRstType = "1") And strRstLLen <> "" And sRstCalc <> "" And IsNumeric(sRstCalc) Then
                        Dim intPos As Integer
                        intPos = InStr(sRstCalc, ".")

                        If Val(strRstLLen) >= 0 Then

                            Dim strDecimal As String = "0"
                            Dim intDecimal As Integer = CInt(strRstLLen)
                            If intDecimal > 0 Then
                                strDecimal = strDecimal & "." & New String(Chr(Asc("0")), intDecimal)
                            End If

                            Select Case strCutOpt
                                Case "0", "3"   ' 0 : 반올림처리없음(입력그대로). 3 : 내림
                                    If intPos > 0 Then
                                        If Len(sRstCalc) >= intPos + intDecimal Then
                                            sRstCalc = Mid(sRstCalc, 1, intPos + intDecimal)
                                        End If
                                    End If
                                Case "1"    ' 1 : 올림
                                    If intPos > 0 Then
                                        If Len(sRstCalc) >= intPos + intDecimal Then
                                            Dim strRstTmp As String
                                            strRstTmp = Mid(sRstCalc, 1, intPos + intDecimal)
                                            If Len(sRstCalc) >= intPos + intDecimal + 1 Then
                                                If Mid(sRstCalc, intPos + intDecimal + 1, 1) > "0" Then
                                                    strRstTmp += "9"
                                                End If
                                            End If
                                            sRstCalc = strRstTmp
                                        End If
                                    End If
                                Case "2"    ' 2 : 반올림
                            End Select

                            sRstCalc = Format(Val(sRstCalc), strDecimal).ToString
                        End If
                    End If
                End If

                If IsNumeric(sRstCalc) Then
                    CType(r_al_cri(iIdx), STU_RstInfo_calc).OrgRst = sRstCalc
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function Find_Calculated_Result(ByVal rsCalForm As String) As String
            Dim sFn As String = "Function Find_Calculated_Result"

            Dim sReturn As String = ""

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += " SELECT " + rsCalForm + " FROM DUAL"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    sReturn = dt.Rows(0).Item(0).ToString
                End If

                Return sReturn

            Catch ex As Exception

                Return ""

            End Try
        End Function

        Private Function Find_Calculated_Settings(ByVal rsTclsCd As String) As DataTable
            Dim sFn As String = "Function Find_Calculated_Result"

            Dim sReturn As String = ""

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT rsttype, rstllen, rstulen, cutopt"
                sSql += "  FROM rf060m"
                sSql += " WHERE testcd = :testcd"
                sSql += "   AND uedt   > fn_ack_sysdate"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                End With

                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnCalcRstInfo_Find_STU_RstInfo_calc_Pat(ByRef r_al_cri As ArrayList, ByVal rsBcNo As String, ByVal rsCTestCd As String, ByRef rsCRegStep As String) As Boolean
            Dim sFn As String = "fnCalcRstInfo_Find_STU_RstInfo_calc_Pat"

            Try
                Dim iIdx As Integer = -1

                For i As Integer = 1 To r_al_cri.Count
                    If CType(r_al_cri(i - 1), STU_RstInfo_calc).CTestCd = rsCTestCd Then
                        iIdx = i - 1

                        Exit For
                    End If
                Next

                If iIdx < 0 Then Return False

                Dim sCalForm As String = CType(r_al_cri(iIdx), STU_RstInfo_calc).CalForm
                Dim sCalItems As String = CType(r_al_cri(iIdx), STU_RstInfo_calc).CalItems
                Dim sCalDays As String = CType(r_al_cri(iIdx), STU_RstInfo_calc).CalDsys

                Dim a_sCalItemTmp As String() = sCalItems.Split(CChar("/"))
                Dim a_sCalItem As String() = Nothing

                For i As Integer = 1 To a_sCalItemTmp.Length
                    If a_sCalItemTmp(i - 1).Trim = "" Then
                        Exit For
                    End If

                    ReDim Preserve a_sCalItem(i - 1)

                    a_sCalItem(i - 1) = a_sCalItemTmp(i - 1).Trim
                Next

                If a_sCalItem Is Nothing Then Return False
                If a_sCalItem.Length < 1 Then Return False

                Dim iCntCalc As Integer = 0

                For i As Integer = 1 To a_sCalItem.Length
                    Dim sSymbol As String = Chr(Asc("A") + i - 1)
                    Dim sTestCd As String = a_sCalItem(i - 1).Substring(0, "LTEST99".Length).Trim
                    Dim sSpcCd As String = a_sCalItem(i - 1).Substring("LTEST99".Length).Trim

                    Dim iIdx1 As Integer = -1
                    Dim iIdx2 As Integer = -1

                    For a As Integer = iIdx + 1 To r_al_cri.Count
                        If CType(r_al_cri(a - 1), STU_RstInfo_calc).CalForm = sSymbol Then
                            iIdx1 = a - 1

                            Exit For
                        End If
                    Next

                    For a As Integer = iIdx + 1 To r_al_cri.Count
                        If CType(r_al_cri(a - 1), STU_RstInfo_calc).TestCd = sTestCd Then
                            iIdx2 = a - 1

                            Exit For
                        End If
                    Next

                    If iIdx1 = iIdx2 And iIdx1 > iIdx Then Continue For

                    Dim sCalRangeB As String = ""

                    sCalRangeB = CType(r_al_cri(iIdx), STU_RstInfo_calc).CalRange
                    'If iCalRangeB = "B" Then Continue For 

                    Dim cri As New STU_RstInfo_calc

                    cri.CalForm = sSymbol
                    cri.CalItems = ""
                    cri.CTestCd = rsCTestCd
                    cri.TestCd = sTestCd

                    iIdx2 = iIdx + i
                    r_al_cri.Insert(iIdx2, cri)

                    If sCalDays = "" Then sCalDays = "9999"
                    Dim dt As DataTable = Get_CalcRstInfo_Pat(rsBcNo, sTestCd, sSpcCd, sCalDays, sCalRangeB, m_dbTran)

                    If dt.Rows.Count = 0 Then Continue For

                    With CType(r_al_cri(iIdx2), STU_RstInfo_calc)
                        .TNmD = dt.Rows(0).Item("tnmd").ToString
                        .OrgRst = dt.Rows(0).Item("orgrst").ToString
                        .RstFlg = dt.Rows(0).Item("rstflg").ToString
                        .BcNo = dt.Rows(0).Item("bcno").ToString
                    End With

                    '> RegStep 계산식 관련검사의 가장 낮은 단계로 조정
                    If dt.Rows(0).Item("rstflg").ToString < rsCRegStep Then
                        rsCRegStep = dt.Rows(0).Item("rstflg").ToString
                    End If
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '-- 계산식 End

        Private Function Get_CalcRstInfo_Pat(ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsSpcCd As String, _
                                                   ByVal rsCalDays As String, ByVal rsCalRange As String, Optional ByVal r_dbTran As Object = Nothing) As DataTable
            Dim sFn As String = "Public Shared Function Get_CalcRstInfo_Pat(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                sSql += "pkg_ack_rst.pkg_get_pat_calc_rstinfo_r"

                Dim al As New ArrayList

                al.Add(New OracleParameter("rs_bcno", rsBcNo))
                al.Add(New OracleParameter("rs_testcd", rsTClsCd))
                al.Add(New OracleParameter("rs_spccd", rsSpcCd))
                al.Add(New OracleParameter("ri_caldays", Convert.ToInt16(rsCalDays)))

                DbCommand(r_dbTran)

                Dim dt As DataTable = DbExecuteQuery(sSql, al, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 2008-11-28 yooeJ add
        '-- 자동 소견 등록
        Private Function fnEdit_rr020m(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_rr020m(object) As String"
            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim arlCmt As New ArrayList
                Dim arlRst As New ArrayList

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql += "SELECT testcd, orgrst, viewrst, hlmark, eqflag"
                sSql += "  FROM rr010m"
                sSql += " WHERE bcno     = :bcno"
                sSql += "   AND orgrst  IS NOT NULL"
                sSql += "   AND viewrst IS NOT NULL"
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        Dim objRst As New STU_CvtCmtInfo

                        With objRst
                            .BcNo = r_sampinfo_Buf.BCNo
                            .TestCd = dt.Rows(ix).Item("testcd").ToString
                            .OrgRst = dt.Rows(ix).Item("orgrst").ToString
                            .ViewRst = dt.Rows(ix).Item("viewrst").ToString
                            .HlMark = dt.Rows(ix).Item("hlmark").ToString
                            .EqFlag = dt.Rows(ix).Item("eqflag").ToString
                        End With

                        arlRst.Add(objRst)
                    Next
                End If

                arlCmt = RISAPP.COMM.CvtCmt.fnCvtCmtInfo(r_sampinfo_Buf.BCNo, arlRst, "", True)

                If arlCmt.Count < 1 Then Return ""

                sSql = "SELECT * FROM rr020m WHERE bcno = :bcno"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then Return ""

                For intIdx As Integer = 0 To arlCmt.Count - 1
                    sSql = ""
                    sSql += "INSERT INTO rr020m(  bcno,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt)"
                    sSql += "            VALUES( :bcno, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_syssdate)"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("rstseq", OracleDbType.Varchar2).Value = Convert.ToString(intIdx + 1)
                        .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(arlCmt(intIdx), STU_CvtCmtInfo).CmtCont
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    End With

                    If dbCmd.ExecuteNonQuery() < 1 Then Return "Error"
                Next

                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 검사분류별 자동 소견 등록
        Private Function fnEdit_rr040m(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_rr040m(object) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql = ""
                sSql += "SELECT r.testcd, r.orgrst, r.viewrst, r.hlmark, r.eqflag"
                sSql += "  FROM rr010m r"
                sSql += " WHERE r.bcno = :bcno"
                sSql += "   AND NVL(r.orgrst,  ' ') <> ' '"
                sSql += "   AND NVL(r.viewrst, ' ') <> ' '"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        Dim objRst As New STU_CvtCmtInfo

                        With objRst
                            .BcNo = r_sampinfo_Buf.BCNo
                            .TestCd = dt.Rows(ix).Item("testcd").ToString
                            .OrgRst = dt.Rows(ix).Item("orgrst").ToString
                            .ViewRst = dt.Rows(ix).Item("viewrst").ToString
                            .HlMark = dt.Rows(ix).Item("hlmark").ToString
                            .EqFlag = dt.Rows(ix).Item("eqflag").ToString
                        End With

                        alRstInfo.Add(objRst)
                    Next
                End If

                alCmtVal = RISAPP.COMM.CvtCmt.fnCvtCmtInfo(r_sampinfo_Buf.BCNo, alRstInfo, "", True, m_dbCn, m_dbTran)

                If alCmtVal.Count < 1 Then Return ""

                sSql = ""
                sSql += "SELECT bcno, partcd, slipcd, cmt"
                sSql += "  FROM rr040m"
                sSql += " WHERE bcno = :bcno"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                For ix As Integer = 0 To alCmtVal.Count - 1
                    Dim sWhere As String = ""
                    sWhere += "bcno = '" + r_sampinfo_Buf.BCNo + "' AND "
                    sWhere += "partcd = '" + CType(alCmtVal(ix), STU_CvtCmtInfo).SlipCd.Substring(0, 1) + "' AND "
                    sWhere += "slipcd = '" + CType(alCmtVal(ix), STU_CvtCmtInfo).SlipCd.Substring(1, 1) + "'"

                    Dim dr As DataRow() = dt.Select(sWhere)

                    If dr.Count < 1 Then
                        sSql = ""
                        sSql += "INSERT INTO rr040m"
                        sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt, regdt,           regid,  editid,  editip, editdt )"
                        sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, fn_ack_sysdate, :regid, :editid, :editip, fn_ack_sysdate)"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(alCmtVal(ix), STU_CvtCmtInfo).SlipCd.Substring(0, 1)
                            .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(alCmtVal(ix), STU_CvtCmtInfo).SlipCd.Substring(1, 1)
                            .Parameters.Add("rstno", OracleDbType.Int32).Value = ix + 1
                            .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(alCmtVal(ix), STU_CvtCmtInfo).CmtCont

                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                        End With

                        If dbCmd.ExecuteNonQuery() < 1 Then Return "Error"
                    End If
                Next

                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_Change_CollAndTkAndRst_date(ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal rsRstDate As String) As Boolean
            Dim sFn As String = "Private Function fnEdit_Change_CollAndTkAndRst_date(STU_SampleInfo, String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text
                End With

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                sSql = ""
                sSql += " UPDATE rj010m SET wkymd = :wkymd, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                sSql += "  where bcno = :bcno"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("wkymd", OracleDbType.Varchar2).Value = rsRstDate.Substring(0, 8)

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                    iRet = .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += " UPDATE rj011m SET colldt = :rstdt, tkdt = :rstdt, rstdt = :rstdt, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                sSql += "  where bcno = :bcno"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                    iRet = .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += " UPDATE rr010m SET"
                sSql += "        tkdt   = :rstdt, wkdt = :rstdt, wkymd = :wkymd,"
                sSql += "        regdt  = DECODE(NVL(regid, ' '),   ' ',  NULL, :rstdt),"
                sSql += "        mwdt   = DECODE(NVL(mwid, ' '),    ' ',  NULL, :rstdt), "
                sSql += "        fndt   = DECODE(NVL(fnid, ' '),    ' ',  NULL, :rstdt),"
                sSql += "        rstdt  = DECODE(NVL(rstflg, '0'), '0', NULL, :rstdt),"
                sSql += "        editdt = fn_ack_sysdate,"
                sSql += "        editid = :editid,"
                sSql += "        editip = :editip"
                sSql += "  where bcno   = :bcno"
                'sSql += "    AND orgrst IS NOT NULL"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("wkymd", OracleDbType.Varchar2).Value = rsRstDate.Substring(0, 8)
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                    iRet = .ExecuteNonQuery()
                End With

                Return True
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_OCS(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean
            Dim sFn As String = "Private Function fnEdit_OCS(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean"

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Dim sErrVal As String = ""

            Try

                '-- 감염정보 등록
                Dim sSql As String = "pro_ack_exe_ocs_rst_inf_r"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Or sErrVal.IndexOf("no data") > 0 Then

                Else
                    Return False
                End If

                '-- OCS에 결과 올리기
                sSql = "pro_ack_exe_ocs_rst_r"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("rs_editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Then
                    'Return True
                Else
                    Return False
                End If

                '-- OCS에 결과 올리기
                sSql = "pro_ack_exe_ocs_rstflg"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 100)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Then
                    Return True
                Else
                    Return False
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function RegServer(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList, ByVal rbSpecialTest As Boolean) As Integer
            Dim sFn As String = "Function RegServer"

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim rstinfo_Buf As STU_RstInfo

                m_b_SpecialTest = rbSpecialTest

                If r_al_EditSuc Is Nothing Then r_al_EditSuc = New ArrayList

                '0) Cn, Transaction 생성
                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                '1) 결과개수만큼 등록
                For i As Integer = 1 To r_al_RstInfo.Count
                    rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                    If rstinfo_Buf.EqFlag Is Nothing Then rstinfo_Buf.EqFlag = ""
                    If rstinfo_Buf.RstCmt Is Nothing Then rstinfo_Buf.RstCmt = ""

                    If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                        iRegOK_Sum += 1

                        r_al_EditSuc.Add(rstinfo_Buf.TestCd)

                    End If
                Next

                If r_al_EditSuc.Count = 0 Then Return iRegOK_Sum

                '1-1) 계산식 관련항목 등록
                Try
                    Dim al_RstInfo_Calc As ArrayList = fnCalcRstInfo(r_sampinfo_Buf, r_al_RstInfo)

                    If Not al_RstInfo_Calc Is Nothing Then
                        If al_RstInfo_Calc.Count > 0 Then
                            For i As Integer = 1 To al_RstInfo_Calc.Count
                                rstinfo_Buf = CType(al_RstInfo_Calc(i - 1), STU_RstInfo)

                                If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                                    iRegOK_Sum += 1

                                    r_al_EditSuc.Add(rstinfo_Buf.TestCd)
                                End If
                            Next
                        End If
                    End If

                Catch ex As Exception
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 계산식 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                End Try
                '>

                '-- 1-2) 결과값 자동 등록
                Try
                    Dim al_RstInfo_Cvt As ArrayList = fnCvtRstInfo(r_sampinfo_Buf, r_al_EditSuc)

                    If Not al_RstInfo_Cvt Is Nothing Then
                        If al_RstInfo_Cvt.Count > 0 Then
                            For i As Integer = 1 To al_RstInfo_Cvt.Count
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i), STU_RstInfo_cvt))
                            Next
                        End If
                    End If
                Catch ex As Exception
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 결과값 자동변환 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                End Try

                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Battery
                fnEdit_LR_Battery(r_sampinfo_Buf)

                '4) Update rj011m
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update rj010m
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) rrs10m
                If rbSpecialTest Then
                    Dim iRegErr_Sum As Integer = 0

                    For i As Integer = 1 To r_al_RstInfo.Count
                        rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                        If rstinfo_Buf.TestCd.Length = 5 And rstinfo_Buf.RstRTF.ToString <> "" Then
                            If fnEdit_LRS10(rstinfo_Buf, r_sampinfo_Buf) Then
                                If Not r_al_EditSuc.Contains(rstinfo_Buf.TestCd) Then
                                    iRegErr_Sum += 1
                                    iRegOK_Sum -= 1
                                End If
                            Else
                                iRegErr_Sum += 1
                                iRegOK_Sum -= 1

                                If r_al_EditSuc.Contains(rstinfo_Buf.TestCd) Then
                                    r_al_EditSuc.Remove(rstinfo_Buf.TestCd)
                                End If
                            End If
                        End If
                    Next

                    If r_al_EditSuc.Count = 0 Or iRegErr_Sum > 0 Then
                        m_dbTran.Rollback()
                        Return iRegOK_Sum
                    End If

                End If


                '7) Upate rr040m(검사분류별 소견)
                Call fnEdit_rr040m(r_sampinfo_Buf) '-- 자동소견

                'Call fnEdit_rr020m(r_sampinfo_Buf) '-- 자동소견

                '-- 2009-09-15 YEJ (감염정보)
                Call fnEdit_OCS(r_sampinfo_Buf)

                m_dbTran.Commit()
                Return iRegOK_Sum


            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        '-- POCT (채혈만 했을 경우 사용) 때문에 추가
        Private Function fnGetBCPrtToView(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function fnGetBCPrtToView(String) As String"

            Dim sSql As String = ""
            Dim dt As New DataTable

            Try
                If Not rsBcNo.Length.Equals(11) Then Return ""

                sSql = "SELECT bcno FROM rj010m WHERE bcno = fn_get_bcno_from_prtbcno('" + rsBcNo + "') AND spcflg IN ('1', '2')"

                DbCommand()
                dt = DbExecuteQuery(sSql)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnConvPrtBCNoToBCNo(ByVal rsBCNo As String) As String
            Dim sBCNo As String = ""

            '2010년에 2009년 바코드 사용하는 경우
            If Format(Now, "yyyy") < Left(Format(Now, "yyyy"), 3) & Mid(Trim(rsBCNo), 3, 1) Then
                sBCNo = CStr(CInt(Left(Format(Now, "yyyy"), 3)) - 1) & Mid(Trim(rsBCNo), 3, 9)
            Else
                sBCNo = Left(Format(Now, "yyyy"), 3) & Mid(Trim(rsBCNo), 3, 9)
            End If

            Return sBCNo
        End Function

        Private Function fnEdit_LJ010(ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LJ010(STU_SampleInfo) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT MIN(NVL(j.rstflg, '0')) minrstflg, MAX(NVL(j.rstflg, '0')) maxrstflg"
                sSql += "  FROM rj011m j"
                sSql += " WHERE j.bcno = :bcno"
                sSql += "   AND NVL(j.spcflg, '0') NOT IN ('0', 'R')"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then Return 0

                Dim sRstflg As String = ""
                Dim iRet As Integer = 0

                If dt.Rows(0).Item("maxrstflg").ToString() = "0" Then
                    sRstflg = ""
                ElseIf dt.Rows(0).Item("minrstflg").ToString() = "3" And dt.Rows(0).Item("minrstflg").ToString() = "3" Then
                    sRstflg = "2"
                Else
                    sRstflg = "1"
                End If

                sSql = ""
                sSql += "UPDATE rj010m SET rstflg = :rstflg, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND spcflg = '4'"
                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstflg

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                    iRet = .ExecuteNonQuery()
                End With

                Return 1
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LJ011(ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LJ011(STU_SampleInfo) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT r.tclscd, r.spccd, MIN(NVL(r.rstflg, '0')) minrstflg, MAX(NVL(r.rstflg, '0')) maxrstflg, MAX(r.rstdt) rstdt"
                sSql += "  FROM rr010m r, rf060m f"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND r.tclscd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   AND r.tkdt  <  f.uedt"
                sSql += "   AND (f.tcdgbn IN ('S', 'P') OR (f.tcdgbn = 'B' AND NVL(f.titleyn, '0') = '1'))"
                sSql += " GROUP BY r.tclscd, r.spccd"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then Return 0

                Dim sRstFlg As String = ""
                Dim iRet As Integer = 0

                For ix As Integer = 1 To dt.Rows.Count
                    If dt.Rows(ix - 1).Item("minrstflg").ToString() = dt.Rows(ix - 1).Item("maxrstflg").ToString() Then
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString() = "0" And dt.Rows(ix - 1).Item("maxrstflg").ToString() <= "3" Then
                        sRstFlg = "1"
                    Else
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    End If

                    sSql = ""

                    Select Case sRstFlg
                        Case "0"
                            sSql += "UPDATE rj011m SET rstflg = NULL, rstdt = NULL, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"
                            sSql += "   AND spcflg = '4'"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()
                                .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("tclscd").ToString()
                            End With

                        Case Else
                            sSql += "UPDATE rj011m SET rstflg = :rstflg, rstdt = :rstdt, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"
                            sSql += "   AND spcflg = '4'"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()
                                .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("rstdt").ToString()

                                .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("tclscd").ToString()
                            End With
                    End Select

                    If Not sSql = "" Then
                        iRet += dbCmd.ExecuteNonQuery()
                    End If
                Next

                Return 1
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LR(STU_RstInfo, STU_SampleInfo) As Integer"

            Try
                Dim iR As Integer = -1

                '1) Item 찾기
                For i As Integer = 1 To m_dt_rst.Rows.Count
                    If r_rstinfo_Buf.TestCd = m_dt_rst.Rows(i - 1).Item("testcd").ToString().Trim Then
                        iR = i - 1

                        Exit For
                    End If
                Next

                If iR = -1 Then Return 0

                If r_rstinfo_Buf.EqFlag Is Nothing Then r_rstinfo_Buf.EqFlag = ""

                '3) ViewRst
                Dim sViewRst As String = fnEdit_LR_ViewRst(iR, r_rstinfo_Buf.OrgRst)

                If miUseCase = 0 Then
                    If sViewRst = "" Then Return 0
                End If

                If r_rstinfo_Buf.ChageRst <> "" Then sViewRst = r_rstinfo_Buf.ChageRst

                '4) Delta Mark
                Dim sDM As String = fnEdit_LR_DM(iR, r_rstinfo_Buf.OrgRst, sViewRst)

                '5) Panic Mark
                Dim sPM As String = fnEdit_LR_PM(iR, r_rstinfo_Buf.OrgRst, r_rstinfo_Buf.TestCd)

                '6) Critical Mark
                Dim sCM As String = fnEdit_LR_CM(iR, r_rstinfo_Buf.OrgRst)

                '7) Alert Mark
                Dim sAM As String = fnEdit_LR_AM(iR, r_rstinfo_Buf.OrgRst, sViewRst, r_rstinfo_Buf.EqFlag, sPM, sDM)

                '2) 번을 이쪽으로 옮김...
                If fnEdit_LR_ViolateNum(iR, r_rstinfo_Buf.OrgRst) Then sAM = "E"

                '8) L/H
                '-- yej 2007.07.09 보여지는 결과로 H/L Check
                Dim sLH As String = fnEdit_LR_LH(iR, r_rstinfo_Buf.OrgRst)
                'Dim sLH As String = fnEdit_LR_LH(iR, sViewRst)

                With r_rstinfo_Buf
                    .ViewRst = sViewRst
                    .DeltaMark = sDM
                    .PanicMark = sPM
                    .CriticalMark = sCM
                    .AlertMark = sAM
                    .HlMark = sLH

                    If r_sampinfo_Buf.RegStep = "2" Then
                        If sDM <> "" Or sPM <> "" Or sCM <> "" Or sAM <> "" Or m_b_SpecialTest Then
                            .RegStep = r_sampinfo_Buf.RegStep
                        Else
                            .RegStep = "3"
                        End If
                    ElseIf r_sampinfo_Buf.RegStep = "22" Then
                        If sDM <> "" Or sPM <> "" Or sCM <> "" Or sAM <> "" Or m_b_SpecialTest Then
                            .RegStep = "1"
                        Else
                            .RegStep = "3"
                        End If
                    Else
                        .RegStep = r_sampinfo_Buf.RegStep
                    End If
                End With

                '9) Update Or Insert 해당 Item
                Return fnEdit_LR_Item(iR, r_rstinfo_Buf, r_sampinfo_Buf)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_LR_AM(ByVal riR As Integer, ByVal rsOrgRst As String, ByVal rsViewRst As String, ByVal rsEqFlag As String, _
                                       ByVal rsPanicMark As String, ByVal rsDeltaMark As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_AM(Integer, String, String, String, String, String) As String"

            Try
                Dim sAlertGbn As String = m_dt_rst.Rows(riR).Item("alertgbn").ToString().Trim

                If sAlertGbn Is Nothing Then Return ""

                Dim sMark As String = "", sRst As String = ""
                Dim sAlertL As String = m_dt_rst.Rows(riR).Item("alertl").ToString().Trim
                Dim sAlertH As String = m_dt_rst.Rows(riR).Item("alerth").ToString().Trim
                Dim sRefL As String = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
                Dim sRefH As String = m_dt_rst.Rows(riR).Item("refh").ToString().Trim

                If rsOrgRst.StartsWith("<=") Or rsOrgRst.StartsWith(">=") Then
                    sMark = rsOrgRst.Substring(0, 2).Trim
                    sRst = rsOrgRst.Substring(2).Trim
                ElseIf rsOrgRst.StartsWith("<") Or rsOrgRst.StartsWith(">") Then
                    sMark = rsOrgRst.Substring(0, 1).Trim
                    sRst = rsOrgRst.Substring(1).Trim
                End If

                'AlertGbn : 0 --> 사용안함, 1 --> 하한만 사용,    2 --> 상한만 사용,    3 --> 모두 사용
                '                           4 --> 문자값,         5 --> Alert Rule
                Select Case sAlertGbn
                    Case "1", "A"
                        If IsNumeric(sAlertL) And Val(sRst) < Val(sAlertL) Then
                            Return "A"
                        End If

                    Case "2", "B"
                        If IsNumeric(sAlertH) And Val(sRst) > Val(sAlertH) Then
                            Return "A"
                        End If

                    Case "3", "C"
                        If IsNumeric(sAlertL) And Val(sRst) < Val(sAlertL) Then
                            Return "A"
                        End If

                        If IsNumeric(sAlertH) And Val(sRst) > Val(sAlertH) Then
                            Return "A"
                        End If
                    Case "4"    '-- 문자값 비고
                        If sAlertL = "" And sAlertH = "" Then
                        Else
                            If sAlertL = "" Then sAlertL = sAlertH

                            If rsOrgRst.ToUpper = sAlertL.ToUpper Then Return "A"

                        End If
                End Select

                '-- Alert Rule 사용
                If sAlertGbn = "5" Or sAlertGbn = "A" Or sAlertGbn = "B" Or sAlertGbn = "C" Then
                    Dim intCnt As Integer = 0, intAlert As Integer = 0

                    If m_dt_rst.Rows(riR).Item("a_sex").ToString().Trim <> "" Then
                        intCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_sex").ToString().Trim = m_dt_rst.Rows(riR).Item("sex").ToString().Trim Then intAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_deptcd").ToString().Trim <> "" Then
                        intCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_deptcd").ToString().Trim.IndexOf(m_dt_rst.Rows(riR).Item("deptcd").ToString().Trim + ",") >= 0 Then intAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_orgrst").ToString().Trim <> "" Then
                        intCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_orgrst").ToString().Trim.IndexOf(rsOrgRst + ",") >= 0 Then intAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_viewrst").ToString().Trim <> "" Then
                        intCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_viewrst").ToString().Trim.IndexOf(rsViewRst + ",") >= 0 Then intAlert += 1
                    End If

                    If rsPanicMark <> "" Then
                        intCnt += 1
                        intAlert += 1
                    End If

                    If rsDeltaMark <> "" Then
                        intCnt += 1
                        intAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_eqflag").ToString().Trim <> "" And rsEqFlag <> "" Then
                        intCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_eqflag").ToString().Trim.IndexOf("^") >= 0 Then
                            Dim strBuf() As String = m_dt_rst.Rows(riR).Item("a_eqflag").ToString().Trim.Split("^"c)

                            If strBuf(1) = "" Then
                                If strBuf(0) = "" Then
                                    intAlert += 1
                                Else
                                    strBuf(0) += ","
                                    If strBuf(0).IndexOf(rsEqFlag + ",") >= 0 Then intAlert += 1
                                End If
                            Else
                                If strBuf(0) = "" Then
                                    strBuf(1) += ","
                                    If strBuf(1).IndexOf(m_dt_rst.Rows(riR).Item("testcd").ToString().Trim + ",") >= 0 Then intAlert += 1
                                Else
                                    strBuf(0) += "," : strBuf(1) += ","
                                    If strBuf(0).IndexOf(rsEqFlag + ",") >= 0 And strBuf(1).IndexOf(m_dt_rst.Rows(riR).Item("testcd").ToString().Trim + ",") >= 0 Then intAlert += 1
                                End If
                            End If
                        Else
                            If m_dt_rst.Rows(riR).Item("a_eqflag").ToString().Trim.IndexOf(rsEqFlag + ",") >= 0 Then intAlert += 1
                        End If
                    End If

                    If m_dt_rst.Rows(riR).Item("a_spccd").ToString().Trim <> "" Then
                        intCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_spccd").ToString().Trim.IndexOf(m_dt_rst.Rows(riR).Item("spccd").ToString().Trim + ",") >= 0 Then intAlert += 1
                    End If

                    If intCnt > 0 And intAlert > 0 Then Return "A"
                End If

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_ViolateNum(ByVal riR As Integer, ByVal rsOrgRst As String) As Boolean
            Dim sRstType As String = m_dt_rst.Rows(riR).Item("rsttype").ToString().Trim

            'RstType : 0 --> 문자 + 숫자 혼합, 1 --> 숫자만 허용
            If sRstType = "1" Then
                If IsNumeric(rsOrgRst) = False Then
                    Return True
                End If
            End If

            Return False
        End Function

        Private Function fnEdit_LR_CM(ByVal riR As Integer, ByVal rsOrgRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_CM(Integer, String) As String"

            Try
                Dim sCriticalGbn As String = m_dt_rst.Rows(riR).Item("criticalgbn").ToString().Trim

                If sCriticalGbn Is Nothing Then Return ""

                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                Dim sCriticalL As String = m_dt_rst.Rows(riR).Item("criticall").ToString().Trim
                Dim sCriticalH As String = m_dt_rst.Rows(riR).Item("criticalh").ToString().Trim
                Dim sRefL As String = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
                Dim sRefH As String = m_dt_rst.Rows(riR).Item("refh").ToString().Trim

                'CriticalGbn : 0 --> 사용안함, 1 --> 하한만 사용,    2 --> 상한만 사용,    3 --> 모두 사용
                '                           4 --> 하한만 사용(%), 5 --> 하한만 사용(%), 6 --> 모두 사용(%)
                Select Case sCriticalGbn
                    Case "1"
                        If IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sCriticalL) Then
                            Return "C"
                        End If

                    Case "2"
                        If IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sCriticalH) Then
                            Return "C"
                        End If

                    Case "3"
                        If IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sCriticalL) Then
                            Return "C"
                        End If

                        If IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sCriticalH) Then
                            Return "C"
                        End If

                    Case "4"
                        If IsNumeric(sRefL) And IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sRefL) * (1 + Val(sCriticalL) / 100) Then
                            Return "C"
                        End If

                    Case "5"
                        If IsNumeric(sRefH) And IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sRefH) * (1 + Val(sCriticalH) / 100) Then
                            Return "C"
                        End If

                    Case "6"
                        If IsNumeric(sRefL) And IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sRefL) * (1 + Val(sCriticalL) / 100) Then
                            Return "C"
                        End If

                        If IsNumeric(sRefH) And IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sRefH) * (1 + Val(sCriticalH) / 100) Then
                            Return "C"
                        End If

                End Select

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Private Function fnEdit_LR_DM(ByVal riR As Integer, ByVal rsOrgRst As String, ByVal asViewRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_DM(Integer, String, tring) As String"

            Try
                Dim sDeltaGbn As String = m_dt_rst.Rows(riR).Item("deltagbn").ToString().Trim

                If sDeltaGbn Is Nothing Then Return ""
                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                Dim sDeltaL As String = m_dt_rst.Rows(riR).Item("deltal").ToString().Trim
                Dim sDeltaH As String = m_dt_rst.Rows(riR).Item("deltah").ToString().Trim
                Dim sDeltaDay As String = m_dt_rst.Rows(riR).Item("deltaday").ToString().Trim

                '결과테이블의 이전결과, 조회해온 이전결과
                Dim sBFOrgRst As String = m_dt_rst.Rows(riR).Item("bforgrst_b").ToString().Trim
                Dim sBFFnDt As String = m_dt_rst.Rows(riR).Item("bffndt_b").ToString().Trim

                Dim sCurDt As String = m_dt_rst.Rows(riR).Item("curdt").ToString().Trim

                sBFOrgRst = sBFOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                '이전결과가 없거나 숫자가 아닐 경우
                If sBFOrgRst.Trim = "" Then Return ""

                Select Case sDeltaGbn
                    Case "1", "2", "3", "4"
                        If IsNumeric(rsOrgRst) = False Then Return ""
                        If IsNumeric(sBFOrgRst) = False Then Return ""
                End Select

                If sBFFnDt = "" Then sBFFnDt = sCurDt
                If sBFFnDt.Length = 8 Then
                    sBFFnDt = sBFFnDt.Insert(4, "-").Insert(7, "-") + " 00:00:00"
                Else
                    sBFFnDt = sBFFnDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")
                End If
                sCurDt = sCurDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")

                '이전결과가 DeltaDay를 초과하는 경우
                Dim lngTerm As Long = DateDiff(DateInterval.Day, CDate(sBFFnDt), CDate(sCurDt))

                If IsNumeric(sDeltaDay) Then
                    If lngTerm > Convert.ToInt64(sDeltaDay) Then
                        Return ""
                    End If
                End If

                '델타구분 : 1 --> 변화차 = 현재결과 - 이전결과,     2 --> 변화비율 = 변화차 / 이전결과 * 100,
                '           3 --> 기간당변화차 = 변화차 / 기간,     4 --> 기간당변화비율 = 변화비율 / 기간,
                '           5 --> 절대변화비율 = 변화차 / 이전결과, 6 --> Grade Delta = 현재Grade - 이전Grade
                Select Case sDeltaGbn
                    Case "1"
                        If IsNumeric(sDeltaH) And Val(rsOrgRst) - Val(sBFOrgRst) > Val(sDeltaH) Then
                            Return "D"
                        End If

                        If IsNumeric(sDeltaL) And Val(rsOrgRst) - Val(sBFOrgRst) < Val(sDeltaL) Then
                            Return "D"
                        End If

                    Case "2"
                        If Val(sBFOrgRst) = 0 Then
                            If IsNumeric(sDeltaH) Or IsNumeric(sDeltaL) Then
                                Return "D"
                            End If
                        Else
                            If IsNumeric(sDeltaH) And ((Val(rsOrgRst) - Val(sBFOrgRst)) / Val(sBFOrgRst)) * 100 > Val(sDeltaH) Then
                                Return "D"
                            End If

                            If IsNumeric(sDeltaL) And ((Val(rsOrgRst) - Val(sBFOrgRst)) / Val(sBFOrgRst)) * 100 < Val(sDeltaL) Then
                                Return "D"
                            End If
                        End If

                    Case "3"
                        '당일 이전검사와 비교시에는 0으로 나눌 수 없으므로 1로 전환
                        If lngTerm = 0 Then lngTerm = 1

                        If IsNumeric(sDeltaH) And (Val(rsOrgRst) - Val(sBFOrgRst)) / lngTerm > Val(sDeltaH) Then
                            Return "D"
                        End If

                        If IsNumeric(sDeltaL) And (Val(rsOrgRst) - Val(sBFOrgRst)) / lngTerm < Val(sDeltaL) Then
                            Return "D"
                        End If

                    Case "4"
                        '당일 이전검사와 비교시에는 0으로 나눌 수 없으므로 1로 전환
                        If lngTerm = 0 Then lngTerm = 1

                        If Val(sBFOrgRst) = 0 Then
                            If IsNumeric(sDeltaH) Or IsNumeric(sDeltaL) Then
                                Return "D"
                            End If
                        Else
                            If IsNumeric(sDeltaH) And ((Val(rsOrgRst) - Val(sBFOrgRst)) / Val(sBFOrgRst)) * 100 / lngTerm > Val(sDeltaH) Then
                                Return "D"
                            End If

                            If IsNumeric(sDeltaL) And ((Val(rsOrgRst) - Val(sBFOrgRst)) / Val(sBFOrgRst)) * 100 / lngTerm < Val(sDeltaL) Then
                                Return "D"
                            End If
                        End If

                    Case "5"
                        Dim strGrade As String = "", strGrade_Old As String = ""
                        Dim strTclsCd As String = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim

                        strGrade = fnGet_GraedValue(strTclsCd, rsOrgRst)
                        strGrade_Old = fnGet_GraedValue(strTclsCd, sBFOrgRst)
                        If strGrade <> "" And strGrade_Old <> "" Then
                            If IsNumeric(sDeltaH) And Math.Abs(Val(strGrade) - Val(strGrade_Old)) >= Math.Abs(Val(sDeltaH)) Then
                                Return "D"
                            End If
                        End If

                End Select

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        '< add freety 2006/02/07 : LIS특수검사와 같이 사용할 수 있도록 변경
        Private Function fnEdit_LR_Item(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Select Case miUseCase
                Case 0
                    '# IF의 경우
                    'D, P, C, A의 경우 RegStep 변경
                    'If Not r_rstinfo_Buf.AlertMark + r_rstinfo_Buf.CriticalMark + r_rstinfo_Buf.DeltaMark + r_rstinfo_Buf.PanicMark = "" Then
                    '    r_rstinfo_Buf.RegStep = "1"
                    'End If

                    If Not r_rstinfo_Buf.AlertMark = "" Then
                        r_rstinfo_Buf.RegStep = "1"
                    End If

                Case 1
                    '# LIS의 경우
                    '사용자가 확인한 것이므로 RegStep은 그대로 사용됨

            End Select

            If m_dt_rst.Rows(riR).Item("rstflg").ToString().Trim = "" Then
                Return fnEdit_LR_Item_Edit_Cur(riR, r_rstinfo_Buf, r_sampinfo_Buf)
            Else
                If m_dt_rst.Rows(riR).Item("rstflg").ToString().Trim = "3" Then
                    ') 이전검사에서 이미 최종보고인 경우

                    Select Case miUseCase
                        Case 0
                            ''IF : 최종보고된 결과는 Overwrite X, Backup O
                            'r_rstinfo_Buf.RegStep = "1"
                            'Return fnEdit_LR_Item_Add_Back(riR, r_rstinfo_Buf, r_sampinfo_Buf)

                        Case 1
                            'LIS : 최종보고수정에 해당하므로 RegStep 그대로, 신규결과로
                            Return fnEdit_LR_Item_Edit_New(riR, r_rstinfo_Buf, r_sampinfo_Buf)

                    End Select
                Else
                    ') 이전검사에서 결과저장 또는 중간보고인 경우

                    Select Case miUseCase
                        Case 0
                            'IF : 현검사가 최종보고 아닌 경우(결과저장 또는 중간보고)이면서 이전 RstFlag보다 낮을 경우는 이전 RstFlag로 ...
                            If Not r_rstinfo_Buf.RegStep = "3" And Val(r_rstinfo_Buf.RegStep) < Val(m_dt_rst.Rows(riR).Item("rstflg").ToString().Trim) Then
                                r_rstinfo_Buf.RegStep = m_dt_rst.Rows(riR).Item("rstflg").ToString().Trim
                            End If

                        Case 1
                            'LIS : RegStep 그대로(중간보고 -> 결과저장은 이미 App에서 불가)

                    End Select

                    Return fnEdit_LR_Item_Edit_New(riR, r_rstinfo_Buf, r_sampinfo_Buf)
                End If
            End If

        End Function

        Private Function fnEdit_LR_Item_Edit_Cur(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LR_Item_Edit_Cur(Integer, STU_RstInfo, STU_SampleInfo) As Integer'"

            Try
                Dim sSql As String = ""
                Dim sNewRstNo As String = "1"

                sSql = ""
                sSql += "UPDATE rr010m SET"
                sSql += "       orgrst       = :orgrst,"
                sSql += "       viewrst      = :viewrst,"
                sSql += "       deltamark    = :deltamark,"
                sSql += "       panicmark    = :panicmark,"
                sSql += "       criticalmark = :criticalmark,"
                sSql += "       alertmark    = :alertmark,"
                sSql += "       hlmark       = :hlmark,"
                sSql += "       regid        = :regid,"
                sSql += "       regdt        = :regdt,"
                sSql += "       mwid         = :mwid,"
                sSql += "       mwdt         = :mwdt,"
                sSql += "       fnid         = :fnid,"
                sSql += "       fndt         = :fndt,"
                sSql += "       cfmnm        = :cfmnm,"
                sSql += "       cfmsign      = :cfmsign,"
                sSql += "       cfmyn        = 'N',"
                sSql += "       rstflg       = :rstflg,"
                sSql += "       rstdt        = :rstdt,"
                sSql += "       rstcmt       = :rstcmt,"
                sSql += "       bfbcno       = :bfbcno,"
                sSql += "       bffndt       = :bffndt,"
                sSql += "       bforgrst     = :bforgrst,"
                sSql += "       bfviewrst    = :bfviewrst,"
                If r_sampinfo_Buf.EqCd <> "" Then
                    sSql += "       eqcd         = :eqcd,"
                    sSql += "       eqseqno      = :eqseqno,"
                    sSql += "       eqrack       = :eqrack,"
                    sSql += "       eqpos        = :eqpos,"
                    sSql += "       eqbcno       = :eqbcno,"
                    sSql += "       eqflag       = :eqflag,"
                End If
                sSql += "       fregdt = CASE WHEN  NVL(fregdt, ' ') = ' ' THEN fn_ack_sysdate ELSE fregdt END,"
                sSql += "       editdt = fn_ack_sysdate,"
                sSql += "       editid = :editid,"
                sSql += "       editip = :editip"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("orgrst", OracleDbType.Varchar2).Value = r_rstinfo_Buf.OrgRst
                    .Parameters.Add("viewrst", OracleDbType.Varchar2).Value = r_rstinfo_Buf.ViewRst
                    .Parameters.Add("deltamark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.DeltaMark
                    .Parameters.Add("panicmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.PanicMark
                    .Parameters.Add("criticalmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.CriticalMark
                    .Parameters.Add("alertmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.AlertMark
                    .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.HlMark

                    'REGID, REGDT, MWID, MWDT, FNID, FNDT
                    Select Case r_rstinfo_Buf.RegStep
                        Case "1"
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = DBNull.Value
                        Case "2"

                            If m_dt_rst.Rows(riR).Item("regdt").ToString.Trim = "" Then
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            Else
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regid").ToString.Trim
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regdt").ToString.Trim
                            End If

                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = DBNull.Value

                        Case "3"
                            If m_dt_rst.Rows(riR).Item("regdt").ToString.Trim = "" Then
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            Else
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regid").ToString.Trim
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regdt").ToString.Trim
                            End If


                            If m_dt_rst.Rows(riR).Item("mwdt").ToString.Trim = "" Then
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            Else
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("mwid").ToString.Trim
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("mwdt").ToString.Trim
                            End If

                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("cfmnm_f").ToString.Trim
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("cfmsign").ToString.Trim
                    End Select

                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RegStep
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString


                    If r_rstinfo_Buf.RstCmt Is Nothing Then r_rstinfo_Buf.RstCmt = ""
                    .Parameters.Add("rstcmt", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstCmt

                    '이전결과
                    .Parameters.Add("bfbcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bfbcno_b").ToString().Trim
                    .Parameters.Add("bffndt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bffndt_b").ToString.Trim
                    .Parameters.Add("bforgrst", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bforgrst_b").ToString().Trim
                    .Parameters.Add("bfviewrst", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bfviewrst_b").ToString().Trim

                    If r_sampinfo_Buf.EqCd <> "" Then
                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                        .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                        .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                        .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                        .Parameters.Add("eqbcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqBCNo
                        .Parameters.Add("eqflag", OracleDbType.Varchar2).Value = r_rstinfo_Buf.EqFlag
                    End If

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim

                    Return .ExecuteNonQuery()

                End With

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_Item_Edit_New(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "'Private Function fnEdit_LR_Item_Edit_New(Integer, STU_RstInfo, STU_SampleInfo) As Integer"

            Try
                Dim sSql As String = ""
                Dim sNewRstNo As String = ""

                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                'Backup
                sSql = ""
                sSql += "INSERT INTO rr011m"
                sSql += "       ("
                sSql += "        bcno, testcd, spccd, tclscd, orgrst, viewrst, rstcmt, deltamark, panicmark, criticalmark,"
                sSql += "        alertmark, hlmark, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg,"
                sSql += "        rstdt, bfbcno, bffndt, eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, sysdt, editdt, editid, editip, seq"
                sSql += "       ) "
                sSql += "SELECT bcno, testcd, spccd, tclscd, orgrst, viewrst, rstcmt, deltamark, panicmark, criticalmark,"
                sSql += "       alertmark, hlmark, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg,"
                sSql += "       rstdt, bfbcno, bffndt, eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, :rstdt, editdt, editid, editip, sq_rr011m.nextval"
                sSql += "  FROM rr010m"
                sSql += " WHERE bcno    = :bcno"
                sSql += "   AND testcd  = :testcd"
                sSql += "   AND NVL(rstdt, ' ') <> ' '"
                sSql += "   AND (NVL(orgrst, '" + r_rstinfo_Buf.OrgRst + "') <> '" + r_rstinfo_Buf.OrgRst + "' OR NVL(viewrst, '" + r_rstinfo_Buf.ViewRst + "') <> '" + r_rstinfo_Buf.ViewRst + "')"
                sSql += "   AND NVL(orgrst,  ' ') <> ' '"
                sSql += "   AND NVL(viewrst, ' ') <> ' '"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                End With

                dbCmd.ExecuteNonQuery()

                'Update
                sSql = ""
                sSql += "UPDATE rr010m SET"
                sSql += "       orgrst       = :orgrst,"
                sSql += "       viewrst      = :viewrst,"
                sSql += "       deltamark    = :deltamark,"
                sSql += "       panicmark    = :panicmark,"
                sSql += "       criticalmark = :criticalmark,"
                sSql += "       alertmark    = :alertmark,"
                sSql += "       hlmark       = :hlmark,"
                sSql += "       regid        = :regid,"
                sSql += "       regdt        = :regdt,"
                sSql += "       mwid         = :mwid,"
                sSql += "       mwdt         = :mwdt,"
                sSql += "       fnid         = :fnid,"
                sSql += "       fndt         = :fndt,"
                sSql += "       cfmnm        = :cfmnm,"
                sSql += "       cfmsign      = :cfmsign,"
                sSql += "       cfmyn        = 'N',"
                sSql += "       rstflg       = :rstflg,"
                sSql += "       rstdt        = :rstdt,"
                sSql += "       rstcmt       = :rstcmt,"
                sSql += "       bfbcno       = :bfbcno,"
                sSql += "       bffndt       = :bffndt,"
                sSql += "       bforgrst     = :bforgrst,"
                sSql += "       bfviewrst    = :bfviewrst,"
                If r_sampinfo_Buf.EqCd <> "" Then
                    sSql += "       eqcd         = :eqcd,"
                    sSql += "       eqseqno      = :eqseqno,"
                    sSql += "       eqrack       = :eqrack,"
                    sSql += "       eqpos        = :eqpos,"
                    sSql += "       eqbcno       = :eqbcno,"
                    sSql += "       eqflag       = :eqflag,"
                End If
                sSql += "       fregdt = CASE WHEN NVL(fregdt, ' ') = ' ' THEN fn_ack_sysdate ELSE fregdt END,"
                sSql += "       editdt = fn_ack_sysdate,"
                sSql += "       editid = :editid,"
                sSql += "       editip = :editip"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"


                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()

                    .Parameters.Add("orgrst", OracleDbType.Varchar2).Value = r_rstinfo_Buf.OrgRst
                    .Parameters.Add("viewrst", OracleDbType.Varchar2).Value = r_rstinfo_Buf.ViewRst
                    .Parameters.Add("deltamark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.DeltaMark
                    .Parameters.Add("panicmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.PanicMark
                    .Parameters.Add("criticalmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.CriticalMark
                    .Parameters.Add("alertmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.AlertMark
                    .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = r_rstinfo_Buf.HlMark

                    'REGID, REGDT, MWID, MWDT, FNID, FNDT
                    Select Case r_rstinfo_Buf.RegStep
                        Case "1"
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = DBNull.Value
                        Case "2"
                            If m_dt_rst.Rows(riR).Item("regdt").ToString.Trim = "" Then
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            Else
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regid").ToString.Trim
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regdt").ToString.Trim
                            End If

                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = DBNull.Value
                        Case "3"
                            If m_dt_rst.Rows(riR).Item("regdt").ToString.Trim = "" Then
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            Else
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regid").ToString.Trim
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regdt").ToString.Trim
                            End If

                            If m_dt_rst.Rows(riR).Item("mwdt").ToString.Trim = "" Then
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            Else
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("mwid").ToString.Trim
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("mwdt").ToString.Trim
                            End If


                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("cfmnm_f").ToString.Trim
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("cfmsign").ToString.Trim
                    End Select

                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RegStep
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim

                    If r_rstinfo_Buf.RstCmt Is Nothing Then r_rstinfo_Buf.RstCmt = ""
                    .Parameters.Add("rstcmt", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstCmt


                    .Parameters.Add("bfbcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bfbcno_b").ToString().Trim
                    .Parameters.Add("bffndt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bffndt_b").ToString.Trim
                    .Parameters.Add("bforgrst", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bforgrst_b").ToString().Trim
                    .Parameters.Add("bfviewrst", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bfviewrst_b").ToString().Trim

                    If r_sampinfo_Buf.EqCd <> "" Then
                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                        .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                        .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                        .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                        .Parameters.Add("eqbcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqBCNo
                        .Parameters.Add("eqflag", OracleDbType.Varchar2).Value = r_rstinfo_Buf.EqFlag
                    End If

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim


                End With

                Return dbCmd.ExecuteNonQuery()
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_Item_Edit_View(ByVal rsBcNo As String, ByVal r_RstInfo As STU_RstInfo_cvt) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR_Item_Edit_View(String, STU_RstInfo_cvt) As Boolean"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "UPDATE rr010m SET"
                sSql += "       viewrst      = :viewrst,"
                sSql += "       rstcmt       = :rstcmt,"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("viewrst", OracleDbType.Varchar2).Value = r_RstInfo.ViewRst
                    .Parameters.Add("rstcmt", OracleDbType.Varchar2).Value = r_RstInfo.RstCmt

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_RstInfo.TestCd

                    .ExecuteNonQuery()

                End With

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_LH(ByVal riR As Integer, ByVal rsOrgRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_LH(Integer, String) As String"

            Try
                Dim sRefGbn As String = m_dt_rst.Rows(riR).Item("refgbn").ToString().Trim

                If sRefGbn Is Nothing Then Return ""

                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                If IsNumeric(rsOrgRst) = False Then Return ""

                Dim sRefL As String = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
                Dim sRefLS As String = m_dt_rst.Rows(riR).Item("refls").ToString().Trim
                Dim sRefH As String = m_dt_rst.Rows(riR).Item("refh").ToString().Trim
                Dim sRefHS As String = m_dt_rst.Rows(riR).Item("refhs").ToString().Trim
                Dim sRefLT As String = m_dt_rst.Rows(riR).Item("reflt").ToString().Trim
                Dim sJudgType As String = m_dt_rst.Rows(riR).Item("judgtype").ToString().Trim

                '0 --> 등호 포함 , 1 --> 부등호
                If sRefHS = "0" Then
                    sRefH = (Val(sRefH) + 0.0000000001).ToString()
                Else
                    sRefH = (Val(sRefH) - 0.0000000001).ToString()
                End If

                If sRefLS = "0" Then
                    sRefL = (Val(sRefL) - 0.0000000001).ToString()
                Else
                    sRefL = (Val(sRefL) + 0.0000000001).ToString()
                End If

                'RefGbn : 0 --> 없음, 1 --> 문자, 2 --> 숫자
                'JudgType : 0 --> 미사용, 1 --> L/H, 2 --> 사용자정의 2단계, 3 --> 사용자정의 3단계
                Select Case sRefGbn
                    Case "1"
                        If sJudgType = "1" Then
                            If Not sRefLT = "" And Not rsOrgRst = sRefLT Then
                                Return "H"
                            End If
                        End If

                    Case "2"
                        If sJudgType = "1" Then
                            If IsNumeric(sRefL) And Val(rsOrgRst) < Val(sRefL) Then
                                Return "L"
                            End If

                            If IsNumeric(sRefH) And Val(rsOrgRst) > Val(sRefH) Then
                                Return "H"
                            End If
                        End If

                End Select

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_Parent(ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LR_Parent(STU_SampleInfo) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt_p As New DataTable

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, SUBSTR(r.testcd, 1, 5) testcd, r.spccd"
                sSql += "  FROM rr010m r, rf060m f"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   and (NVL(r.orgrst, ' ') <> ' ' OR (f.tcdgbn = 'C' AND NVL(f.reqsub, '0') = '1') OR (f.tcdgbn = 'P' AND f.titleyn = '0'))"
                sSql += "   and r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   and r.tkdt  <  f.uedt"
                sSql += "   and f.tcdgbn IN ('P', 'C')"
                sSql += " GROUP BY SUBSTR(r.testcd, 1, 5), r.spccd"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt_p.Reset()
                dbDa.Fill(dt_p)

                If dt_p.Rows.Count < 1 Then Return 1

                For ix As Integer = 0 To dt_p.Rows.Count - 1
                    Dim sRstFlg As String = dt_p.Rows(ix).Item("rstflg").ToString
                    Dim sRstFlg_max As String = dt_p.Rows(ix).Item("maxrstflg").ToString

                    If sRstFlg = "3" Then

                        Dim a_dr As DataRow() = m_dt_rst.Select("testcd = '" + dt_p.Rows(ix).Item("testcd").ToString + "'", "")

                        sSql = ""
                        sSql += "UPDATE rr010m SET"
                        If r_sampinfo_Buf.EqCd <> "" Then
                            sSql += "       eqcd    = :eqcd,"
                            sSql += "       eqseqno = :eqseqno,"
                            sSql += "       eqrack  = :eqrack,"
                            sSql += "       eqpos   = :eqpos,"
                        End If

                        sSql += "       cfmyn  = 'N',"
                        sSql += "       rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :regid), regdt   = NVL(regdt, :regdt),"
                        sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt    = NVL(mwdt,  :mwdt),"
                        sSql += "       fnid   = NVL(fnid,  :fnid),  fndt    = :fndt,"
                        sSql += "       cfmnm  = :cfmnm,             cfmsign = :cfmsign,"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno   = :bcno"
                        sSql += "   AND testcd LIKE :testcd || '%'"
                        sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                        sSql += "        (testcd, spccd, '1') = "
                        sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                        sSql += "          WHERE r.bcno   = :bcno"
                        sSql += "            AND r.testcd LIKE :testcd || '%'"
                        sSql += "            AND r.testcd = f.testcd"
                        sSql += "            AND r.spccd  = f.spccd"
                        sSql += "            AND f.usdt  <= r.tkdt"
                        sSql += "            AND f.uedt  >  r.tkdt"
                        sSql += "            AND tcdgbn   = 'P'"
                        sSql += "        )"
                        sSql += "       )"
                        'sSql += "   AND rstflg <> '3'"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            If r_sampinfo_Buf.EqCd <> "" Then
                                .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                            End If

                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmnm_f").ToString
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmsign").ToString

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                        End With
                    Else
                        sSql = ""
                        Select Case sRstFlg
                            Case "1"
                                sSql = ""
                                sSql += "UPDATE rr010m SET"
                                If r_sampinfo_Buf.EqCd <> "" Then
                                    sSql += "       eqcd    = :eqcd,"
                                    sSql += "       eqseqno = :eqseqno,"
                                    sSql += "       eqrack  = :eqrack,"
                                    sSql += "       eqpos   = :eqpos,"
                                End If

                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NULL,  mwdt = NULL,"
                                sSql += "       fnid   = NULL,  fndt = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND testcd LIKE :testcd || '%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '0') = "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                                sSql += "          WHERE r.bcno   = :bcno"
                                sSql += "            AND r.testcd LIKE :testcd ||'%'"
                                sSql += "            AND r.testcd = f.testcd"
                                sSql += "            AND r.spccd  = f.spccd"
                                sSql += "            AND f.usdt  <= r.tkdt"
                                sSql += "            AND f.uedt  >  r.tkdt"
                                sSql += "            AND tcdgbn   = 'P'"
                                sSql += "        )"
                                sSql += "       )"


                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    If r_sampinfo_Buf.EqCd <> "" Then
                                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                        .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                        .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                        .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos

                                    End If

                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                End With

                            Case "2"
                                sSql = ""
                                sSql += "UPDATE rr010m SET"
                                If r_sampinfo_Buf.EqCd <> "" Then
                                    sSql += "       eqcd    = :eqcd,"
                                    sSql += "       eqseqno = :eqseqno,"
                                    sSql += "       eqrack  = :eqrack,"
                                    sSql += "       eqpos   = :eqpos,"
                                End If

                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                sSql += "       fnid   = NULL,               fndt = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND testcd LIKE :testcd || '%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '0') = "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                                sSql += "          WHERE r.bcno   = :bcno"
                                sSql += "            AND r.testcd LIKE :testcd || '%'"
                                sSql += "            AND r.testcd = f.testcd"
                                sSql += "            AND r.spccd  = f.spccd"
                                sSql += "            AND f.usdt  <= r.tkdt"
                                sSql += "            AND f.uedt  >  r.tkdt"
                                sSql += "            AND tcdgbn   = 'P'"
                                sSql += "        )"
                                sSql += "       )"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    If r_sampinfo_Buf.EqCd <> "" Then
                                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                        .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                        .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                        .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos

                                    End If
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                                End With
                            Case "0"
                                If sRstFlg_max = "3" Then
                                    sSql = ""
                                    sSql += "UPDATE rr010m SET"
                                    If r_sampinfo_Buf.EqCd <> "" Then
                                        sSql += "       eqcd    = :eqcd,"
                                        sSql += "       eqseqno = :eqseqno,"
                                        sSql += "       eqrack  = :eqrack,"
                                        sSql += "       eqpos   = :eqpos,"
                                    End If

                                    sSql += "       rstflg = :rstflg,"
                                    sSql += "       rstdt  = :regdt,"
                                    sSql += "       regid  = NVL(regid, :regdt), regdt = NVL(regdt, ;regdt),"
                                    sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                    sSql += "       fnid   = NULL,               fndt  = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno   = :bcno"
                                    sSql += "   AND testcd LIKE :testcd || '%'"
                                    sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                    sSql += "        (testcd, spccd, '0') = "
                                    sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM rf060m f, rr010m r"
                                    sSql += "          WHERE r.bcno   = :bcno"
                                    sSql += "            AND r.testcd LIKE :testcd || '%'"
                                    sSql += "            AND r.testcd = f.testcd"
                                    sSql += "            AND r.spccd  = f.spccd"
                                    sSql += "            AND f.usdt  <= r.tkdt"
                                    sSql += "            AND f.uedt  >  r.tkdt"
                                    sSql += "            AND tcdgbn   = 'P'"
                                    sSql += "        )"
                                    sSql += "       )"

                                    dbCmd.CommandText = sSql

                                    With dbCmd
                                        .Parameters.Clear()
                                        If r_sampinfo_Buf.EqCd <> "" Then
                                            .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                            .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                            .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                            .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos

                                        End If
                                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = "1"
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                                    End With
                                End If

                        End Select
                    End If

                    If Not sSql = "" Then
                        Dim iRet As Integer = dbCmd.ExecuteNonQuery()
                    End If

                Next

                Return 1

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_Battery(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR_Battery(STU_SampleInfo) As Boolean"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt_p As New DataTable

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, r.tclscd, r.spccd"
                sSql += "  FROM rr010m r, rf060m f, rf062m f62"
                sSql += " WHERE r.bcno    = :bcno"
                sSql += "   AND r.tclscd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND r.tclscd  = f62.tclscd"
                sSql += "   AND r.spccd   = f62.tspccd"
                sSql += "   AND r.testcd  = f62.testcd"
                sSql += "   AND r.spccd   = f62.spccd"
                sSql += "   AND f62.grprstyn = '1'"
                sSql += "   AND f.tcdgbn     = 'B'"
                sSql += "   AND f.grprstyn   = '1'"
                sSql += "   AND (r.testcd <> r.tclscd OR NVL(f.titleyn, '0') = '0')"
                sSql += "   AND LENGTH(r.testcd) = 5"
                sSql += " GROUP BY r.tclscd, r.spccd"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt_p.Reset()
                dbDa.Fill(dt_p)

                If dt_p.Rows.Count < 1 Then Return True

                For ix As Integer = 0 To dt_p.Rows.Count - 1
                    Dim sRstFlg As String = dt_p.Rows(ix).Item("rstflg").ToString
                    Dim sRstFlg_max As String = dt_p.Rows(ix).Item("maxrstflg").ToString

                    If sRstFlg = "3" Then
                        'Dim a_dr As DataRow() = m_dt_rst.Select("tclscd = '" + dt_p.Rows(ix).Item("tclscd").ToString + "' AND rstdt = '" + dt_p.Rows(ix).Item("rstdt").ToString + "')", "")
                        Dim a_dr As DataRow() = m_dt_rst.Select("tclscd = '" + dt_p.Rows(ix).Item("tclscd").ToString + "' AND rstdt = '" + dt_p.Rows(ix).Item("rstdt").ToString + "'", "")

                        sSql = ""
                        sSql += "UPDATE rr010m"
                        sSql += "   SET rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :rstid), regdt   = NVL(regdt, :rstdt),"
                        sSql += "       mwid   = NVL(mwid,  :rstid), mwdt    = NVL(mwdt,  :rstdt),"
                        sSql += "       fnid   = NVL(fnid,  :rstid), fndt    = :rstdt,"
                        sSql += "       cfmnm  = :cfmnm,             cfmsign = :cfmsign,  cfmyn = 'N',"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND tclscd  = :testcd"
                        sSql += "   AND NVL(orgrst, ' ') <> ' '"
                        sSql += "   AND rstflg <> '3'"
                        sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                        sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m"
                        sSql += "         WHERE grprstyn = 1"
                        sSql += "       )"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                            If a_dr.Length > 0 Then
                                .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmnm_f").ToString
                                .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmsign").ToString
                            Else
                                .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = ""
                                .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = ""
                            End If

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                        End With
                    Else
                        sSql = ""
                        Select Case sRstFlg
                            Case "0"
                                If sRstFlg_max = "3" Then
                                    sSql = ""
                                    sSql += "UPDATE rr010m"
                                    sSql += "   SET rstflg = '1',"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid  = NVL(regid, :rstid), regdt   = NVL(regdt, :rstdt),"
                                    sSql += "       mwid   = NVL(mwid,  :rstid), mwdt    = NVL(mwdt,  :rstdt),"
                                    sSql += "       fnid   = NULL,          fndt  = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno    = :bcno"
                                    sSql += "   AND tclscd  = :testcd"
                                    sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                    sSql += "   AND rstflg  = '3'"
                                    sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                    sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m"
                                    sSql += "         WHERE grprstyn = 1"
                                    sSql += "       )"

                                    dbCmd.CommandText = sSql

                                    With dbCmd
                                        .Parameters.Clear()
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                    End With
                                End If

                            Case "1"
                                sSql = ""
                                sSql += "UPDATE rr010m"
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :rstid), regdt   = NVL(regdt, :rstdt),"
                                sSql += "       mwid   = NULL,          mwdt = NULL,"
                                sSql += "       fnid   = NULL,          fndt = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno    = :bcno"
                                sSql += "   AND tclscd  = :testcd"
                                sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m"
                                sSql += "         WHERE grprstyn = 1"
                                sSql += "       )"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                End With

                            Case "2"
                                sSql = ""
                                sSql += "UPDATE rr010m"
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :rstid), regdt   = NVL(regdt, :rstdt),"
                                sSql += "       mwid   = NVL(mwid,  :rstid), mwdt    = NVL(mwdt,  :rstdt),"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND tclscd = :testcd"
                                sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m"
                                sSql += "         WHERE grprstyn = 1"
                                sSql += "       )"


                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                End With
                        End Select
                    End If
                    If Not sSql = "" Then
                        Dim iRet As Integer = dbCmd.ExecuteNonQuery()
                    End If
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_PM(ByVal riR As Integer, ByVal rsOrgRst As String, ByVal rsTclsCd As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_PM(Integer, String, String) As String"

            Try
                Dim sPanicGbn As String = m_dt_rst.Rows(riR).Item("panicgbn").ToString().Trim

                If sPanicGbn Is Nothing Then Return ""
                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                Dim sPanicL As String = m_dt_rst.Rows(riR).Item("panicl").ToString().Trim
                Dim sPanicH As String = m_dt_rst.Rows(riR).Item("panich").ToString().Trim
                Dim sRefL As String = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
                Dim sRefH As String = m_dt_rst.Rows(riR).Item("refh").ToString().Trim
                Dim strGrade As String

                'PanicGbn : 0 --> 사용안함, 1 --> 하한만 사용,    2 --> 상한만 사용,    3 --> 모두 사용
                '                           4 --> 하한만 사용(Grad), 5 --> 상한만 사용(Grad), 6 --> 모두 사용(Grad)
                Select Case sPanicGbn
                    Case "1"
                        If IsNumeric(sPanicL) And Val(rsOrgRst) < Val(sPanicL) Then
                            Return "P"
                        End If

                    Case "2"
                        If IsNumeric(sPanicH) And Val(rsOrgRst) > Val(sPanicH) Then
                            Return "P"
                        End If

                    Case "3"
                        If IsNumeric(sPanicL) And Val(rsOrgRst) < Val(sPanicL) Then
                            Return "P"
                        End If

                        If IsNumeric(sPanicH) And Val(rsOrgRst) > Val(sPanicH) Then
                            Return "P"
                        End If

                    Case "4"
                        strGrade = fnGet_GraedValue(rsTclsCd, rsOrgRst)

                        If strGrade <> "" Then
                            If Val(strGrade) < Val(rsOrgRst) Then
                                Return "P"
                            End If
                        End If

                    Case "5"
                        strGrade = fnGet_GraedValue(rsTclsCd, rsOrgRst)

                        If strGrade <> "" Then
                            If Val(strGrade) > Val(rsOrgRst) Then
                                Return "P"
                            End If
                        End If

                    Case "6"
                        strGrade = fnGet_GraedValue(rsTclsCd, rsOrgRst)

                        If strGrade <> "" Then
                            If Val(strGrade) < Val(rsOrgRst) Then
                                Return "P"
                            End If

                            If Val(strGrade) > Val(rsOrgRst) Then
                                Return "P"
                            End If
                        End If
                End Select

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_ViewRst(ByVal riR As Integer, ByVal rsOrgRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_ViewRst(Integer, String) As String"

            Try
                Dim sViewRst As String = ""
                Dim sULen As String = "", sLLen As String = ""
                Dim sFmt As String = ""
                Dim sConvRst As String = ""

                'CutOpt : 1 --> 올림, 2 --> 반올림, 3 --> 내림
                'RstULen : 정수크기
                'RstLLen : 소수크기

                Dim sFlag As String = ""

                Try
                    If rsOrgRst.Substring(0, 2) = ">=" Or rsOrgRst.Substring(0, 2) = "<=" Then
                        sFlag = rsOrgRst.Substring(0, 2)
                        rsOrgRst = rsOrgRst.Substring(2).Trim
                    ElseIf rsOrgRst.Substring(0, 1) = ">" Or rsOrgRst.Substring(0, 1) = "<" Then
                        sFlag = rsOrgRst.Substring(0, 1)
                        rsOrgRst = rsOrgRst.Substring(1).Trim
                    End If
                Catch ex As Exception

                End Try

                'OrgRst가 숫자이면 CutOpt, RstULen, RstLLen 적용
                If Not IsNumeric(rsOrgRst) Then Return sFlag + rsOrgRst

                If Val(m_dt_rst.Rows(riR).Item("cutopt").ToString().Trim) > 0 Then
                    sULen = m_dt_rst.Rows(riR).Item("rstulen").ToString().Trim
                    sLLen = m_dt_rst.Rows(riR).Item("rstllen").ToString().Trim

                    If Val(sULen) > 0 Then
                        If rsOrgRst.IndexOf(".") > Val(sULen) Then
                            Return ""
                        End If
                    End If

                    If IsNumeric(sLLen) Then
                        If Val(sLLen) = 0 Then
                            sFmt = "0"
                        Else
                            sFmt = "0.".PadRight(CInt(Val(sLLen) + 2), "0"c)
                        End If

                        sConvRst = Format(Convert.ToDouble(rsOrgRst), sFmt)
                        sViewRst = sFlag + sConvRst

                        Select Case m_dt_rst.Rows(riR).Item("cutopt").ToString().Trim
                            Case "1"    '올림
                                '반올림- 원값 
                                '    7 - 6.9999 =  0.0001   --> 7
                                '    7 - 7      =  0        --> 7
                                '    7 - 7.0001 = -0.0001   --> 8
                                If Val(sConvRst) - Val(rsOrgRst) < 0 Then
                                    sViewRst = sFlag + CStr(Val(sConvRst) + (10 ^ -Val(sLLen)))
                                End If

                            Case "2"    '반올림
                                sViewRst = sFlag + sConvRst

                            Case "3"    '내림
                                '반올림- 원값 
                                '    7 - 6.9999 =  0.0001   --> 6
                                '    7 - 7      =  0        --> 7
                                '    7 - 7.0001 = -0.0001   --> 7
                                If Val(sConvRst) - Val(rsOrgRst) > 0 Then
                                    sViewRst = sFlag + CStr(Val(sConvRst) - (10 ^ -Val(sLLen)))
                                End If

                        End Select
                    Else
                        sViewRst = sFlag + rsOrgRst
                    End If
                Else
                    sViewRst = sFlag + rsOrgRst
                End If

                rsOrgRst = sFlag + rsOrgRst

                '사용자정의문자 적용
                sViewRst = fnEdit_LR_ViewRst_UJ(riR, sViewRst, rsOrgRst)

                '허용상하한치 적용
                sViewRst = fnEdit_LR_ViewRst_AL(riR, sViewRst, rsOrgRst)

                Return sViewRst
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_ViewRst_AL(ByVal riR As Integer, ByVal rsNewOrgRst As String, ByVal rsOldOrgRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_ViewRst_AL(Integer, String, String) As String"

            Try
                Dim sALimitL As String = "", sALimitH As String = ""
                Dim sALimitLS As String = "", sALimitHS As String = ""
                Dim sALimitGbn As String = m_dt_rst.Rows(riR).Item("alimitgbn").ToString().Trim
                Dim sFlag As String

                Try

                    If rsOldOrgRst.Substring(0, 2) = ">=" Or rsOldOrgRst.Substring(0, 2) = "<=" Then
                        sFlag = rsOldOrgRst.Substring(0, 2)
                        rsOldOrgRst = rsOldOrgRst.Substring(2).Trim
                    ElseIf rsOldOrgRst.Substring(0, 1) = ">" Or rsOldOrgRst.Substring(0, 1) = "<" Then
                        sFlag = rsOldOrgRst.Substring(0, 1)
                        rsOldOrgRst = rsOldOrgRst.Substring(1).Trim
                    End If
                Catch ex As Exception

                End Try
                If sALimitGbn Is Nothing Then Return rsNewOrgRst

                Dim iAL As Integer = 0, iAH As Integer = 0

                '허용치구분 : 1 --> 허용하한만 사용, 2 --> 허용상한만 사용, 3 --> 모두 사용
                Select Case sALimitGbn
                    Case "1"
                        iAL = 1 : iAH = 0

                    Case "2"
                        iAL = 0 : iAH = 1

                    Case "3"
                        iAL = 1 : iAH = 1

                End Select

                sALimitL = m_dt_rst.Rows(riR).Item("alimitl").ToString().Trim
                sALimitH = m_dt_rst.Rows(riR).Item("alimith").ToString().Trim
                sALimitLS = m_dt_rst.Rows(riR).Item("alimitls").ToString().Trim
                sALimitHS = m_dt_rst.Rows(riR).Item("alimiths").ToString().Trim

                '허용하한 적용
                If iAL = 1 Then
                    If IsNumeric(sALimitL) Then
                        If Val(rsOldOrgRst) <= Val(sALimitL) Then
                            Select Case sALimitLS
                                Case "1"
                                    rsNewOrgRst = sALimitL
                                Case "2"
                                    rsNewOrgRst = "< " + sALimitL
                                Case "3"
                                    rsNewOrgRst = sALimitL + " 이하"
                                Case "4"
                                    rsNewOrgRst = sALimitL + " 미만"
                                Case "5"
                                    rsNewOrgRst = "<= " + sALimitL
                            End Select
                        End If
                    End If
                End If

                '허용상한 적용
                If iAH = 1 Then
                    If IsNumeric(sALimitH) Then
                        If Val(rsOldOrgRst) >= Val(sALimitH) Then
                            Select Case sALimitHS
                                Case "1"
                                    rsNewOrgRst = sALimitH
                                Case "2"
                                    rsNewOrgRst = "> " + sALimitH
                                Case "3"
                                    rsNewOrgRst = sALimitH + " 이상"
                                Case "4"
                                    rsNewOrgRst = sALimitH + " 초과"
                                Case "5"
                                    rsNewOrgRst = ">= " + sALimitH
                            End Select
                        End If
                    End If
                End If

                Return rsNewOrgRst

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Private Function fnEdit_LR_ViewRst_UJ(ByVal riR As Integer, ByVal rsNewOrgRst As String, ByVal rsOldOrgRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_ViewRst_UJ(Integer, String, String) As String"

            Try
                Dim sRefH As String = "", sRefL As String = "", sRefHs As String = ""
                Dim sULT1 As String = "", sULT2 As String = "", sULT3 As String = ""

                'JudgType : 0 --> 미사용, 1 --> L/H, 212222 --> 사용자정의 2단계, 312322332 --> 사용자정의 3단계
                Dim sJudgType As String = m_dt_rst.Rows(riR).Item("judgtype").ToString().Trim
                Dim sFlag As String = ""

                Try
                    If rsOldOrgRst.Substring(0, 2) = ">=" Or rsOldOrgRst.Substring(0, 2) = "<=" Then
                        sFlag = rsOldOrgRst.Substring(0, 2)
                        rsOldOrgRst = rsOldOrgRst.Substring(2).Trim
                    ElseIf rsOldOrgRst.Substring(0, 1) = ">" Or rsOldOrgRst.Substring(0, 1) = "<" Then
                        sFlag = rsOldOrgRst.Substring(0, 1)
                        rsOldOrgRst = rsOldOrgRst.Substring(1).Trim
                    End If
                Catch ex As Exception

                End Try

                If sJudgType Is Nothing Then Return rsNewOrgRst

                sULT1 = m_dt_rst.Rows(riR).Item("ujudglt1").ToString().Trim
                sULT2 = m_dt_rst.Rows(riR).Item("ujudglt2").ToString().Trim
                sULT3 = m_dt_rst.Rows(riR).Item("ujudglt3").ToString().Trim

                '몫 : 2 --> 사용자정의 2단계, 3 --> 사용자정의 3단계
                Select Case Len(sJudgType) \ 3
                    Case 2
                        '상한값과 결과값 비교
                        sRefH = m_dt_rst.Rows(riR).Item("refh").ToString().Trim
                        sRefHs = m_dt_rst.Rows(riR).Item("refhs").ToString().Trim

                        If sRefH = "" Then
                            sRefH = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
                            sRefHs = m_dt_rst.Rows(riR).Item("refls").ToString().Trim
                        End If

                        If IsNumeric(sRefH) Then
                            '0 --> 등호 포함 , 1 --> 부등호
                            If sRefHs = "0" Then
                                sRefH = (Val(sRefH) + 0.0000000001).ToString()
                            Else
                                sRefH = (Val(sRefH) - 0.0000000001).ToString()
                            End If

                            If Val(rsOldOrgRst) > Val(sRefH) Then
                                '21222>2<
                                Select Case sJudgType.Substring(5, 1)
                                    Case "0"
                                        rsNewOrgRst = rsNewOrgRst
                                    Case "1"
                                        rsNewOrgRst = sULT2
                                    Case "2"
                                        rsNewOrgRst = sULT2 + "(" + rsNewOrgRst + ")"
                                    Case "3"
                                        rsNewOrgRst = sULT2 + " " + rsNewOrgRst
                                    Case "4"
                                        rsNewOrgRst = rsNewOrgRst + " " + sULT2
                                End Select
                            Else
                                '21>2<222
                                Select Case sJudgType.Substring(2, 1)
                                    Case "0"
                                        rsNewOrgRst = rsNewOrgRst
                                    Case "1"
                                        rsNewOrgRst = sULT1
                                    Case "2"
                                        rsNewOrgRst = sULT1 + "(" + rsNewOrgRst + ")"
                                    Case "3"
                                        rsNewOrgRst = sULT1 + " " + rsNewOrgRst
                                    Case "4"
                                        rsNewOrgRst = rsNewOrgRst + " " + sULT1
                                End Select
                            End If
                        End If

                    Case 3
                        '상한값, 하한값과 결과값 비교
                        sRefH = m_dt_rst.Rows(riR).Item("refh").ToString().Trim
                        sRefL = m_dt_rst.Rows(riR).Item("refl").ToString().Trim

                        If IsNumeric(sRefH) And IsNumeric(sRefL) Then
                            '0 --> 등호 포함 , 1 --> 부등호
                            If m_dt_rst.Rows(riR).Item("refhs").ToString().Trim = "0" Then
                                sRefH = (Val(sRefH) + 0.0000000001).ToString()
                            Else
                                sRefH = (Val(sRefH) - 0.0000000001).ToString()
                            End If

                            If m_dt_rst.Rows(riR).Item("refls").ToString().Trim = "0" Then
                                sRefL = (Val(sRefL) - 0.0000000001).ToString()
                            Else
                                sRefL = (Val(sRefL) + 0.0000000001).ToString()
                            End If

                            If Val(rsOldOrgRst) > Val(sRefH) Then
                                '31232233>2<
                                Select Case sJudgType.Substring(8, 1)
                                    Case "0"
                                        rsNewOrgRst = rsNewOrgRst
                                    Case "1"
                                        rsNewOrgRst = sULT3
                                    Case "2"
                                        rsNewOrgRst = sULT3 + "(" + rsNewOrgRst + ")"
                                    Case "3"
                                        rsNewOrgRst = sULT3 + " " + rsNewOrgRst
                                    Case "4"
                                        rsNewOrgRst = rsNewOrgRst + " " + sULT3
                                End Select
                            ElseIf Val(rsOldOrgRst) < Val(sRefL) Then
                                '31>2<322332
                                Select Case sJudgType.Substring(2, 1)
                                    Case "0"
                                        rsNewOrgRst = rsNewOrgRst
                                    Case "1"
                                        rsNewOrgRst = sULT1
                                    Case "2"
                                        rsNewOrgRst = sULT1 + "(" + rsNewOrgRst + ")"
                                    Case "3"
                                        rsNewOrgRst = sULT1 + " " + rsNewOrgRst
                                    Case "4"
                                        rsNewOrgRst = rsNewOrgRst + " " + sULT1
                                End Select
                            Else
                                '31232>2<332
                                Select Case sJudgType.Substring(5, 1)
                                    Case "0"
                                        rsNewOrgRst = rsNewOrgRst
                                    Case "1"
                                        rsNewOrgRst = sULT2
                                    Case "2"
                                        rsNewOrgRst = sULT2 + "(" + rsNewOrgRst + ")"
                                    Case "3"
                                        rsNewOrgRst = sULT2 + " " + rsNewOrgRst
                                    Case "4"
                                        rsNewOrgRst = rsNewOrgRst + " " + sULT2
                                End Select
                            End If
                        End If

                End Select

                Return rsNewOrgRst
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        'add jchyung 2008.1.2
        Private Shared Function fnDepFile_Get(ByVal rsFileNm As String) As Byte()
            Dim fs As IO.FileStream = New IO.FileStream(rsFileNm, IO.FileMode.Open, IO.FileAccess.Read)
            Dim br As IO.BinaryReader = New IO.BinaryReader(fs)

            Dim a_btReturn() As Byte = br.ReadBytes(CType(fs.Length, Integer))

            br.Close()
            fs.Close()

            Return a_btReturn
        End Function

        Private Function fnEdit_LRS10(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean
            Dim sFn As String = "Private Function fnEdit_LRS10(STU_RstInfo,  STU_SampleInfo) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text
                End With

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                '0) lrs10h
                sSql = ""
                sSql += "INSERT INTO rrs10h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, bcno, testcd, rstflg, rsttxt, rstdt, regid, migymd, editdt, editid, editip"
                sSql += "  FROM lrs10m"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd

                    iRet = .ExecuteNonQuery()
                End With


                '0) Delete rrs10m : 나중에 필요에 의해서 rrs10m도 History 관리할 경우 이것만 Remark 처리함
                sSql = ""
                sSql += " DELETE rrs10m"
                sSql += "  WHERE bcno   = :bcno"
                sSql += "    AND testcd = :testcd"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd

                    iRet += .ExecuteNonQuery()
                End With

                '-- 20090907 YEJ
                sSql = ""
                sSql += "INSERT INTO rrs10m("
                sSql += "             bcno,  testcd,  rstflg,  rsttxt,  rstrtf, rstdt,           rstid,  editid,  editip, editdt )"
                sSql += "    VALUES( :bcno, :testcd, :rstflg, :rsttxt, :rstrtf, fn_ack_sysdate, :rstid, :editid, :editip, fn_ack_sysdate )"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_sampinfo_Buf.RegStep
                    .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstTXT
                    .Parameters.Add("rstrtf", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstRTF
                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    iRet += .ExecuteNonQuery()
                End With

                '-- 20090907 YEJ
                If r_rstinfo_Buf.AddFileNm1 <> "" Then
                    sSql = ""
                    sSql += "DELETE rrs12m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd

                        iRet += .ExecuteNonQuery()
                    End With

                    Dim a_btAddFile() As Byte = fnDepFile_Get(r_rstinfo_Buf.AddFileNm1)
                    Dim sFileNm As String = r_rstinfo_Buf.AddFileNm1
                    Dim iPos As Integer = sFileNm.IndexOf("\")

                    Do While iPos >= 0
                        sFileNm = sFileNm.Substring(iPos + 1)
                        iPos = sFileNm.IndexOf("\")
                    Loop

                    '-- 20090907 YEJ
                    sSql = ""
                    sSql += "INSERT INTO rrs12m("
                    sSql += "             bcno,  testcd, rstno,  filenm,  filelen,  filebin )"
                    sSql += "    VALUES( :bcno, :testcd,     1, :fielnm, :filelen, :filebin )"

                    With dbCmd
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                        .Parameters.Add("filenm", OracleDbType.Varchar2).Value = sFileNm
                        .Parameters.Add("filelen", OracleDbType.Varchar2).Value = a_btAddFile.Length
                        .Parameters.Add("filebin", OracleDbType.LongRaw, a_btAddFile.Length).Value = a_btAddFile

                        iRet += .ExecuteNonQuery()
                    End With
                End If

                If r_rstinfo_Buf.AddFileNm2 <> "" Then
                    Dim a_btAddFile() As Byte = fnDepFile_Get(r_rstinfo_Buf.AddFileNm2)
                    Dim sFileNm As String = r_rstinfo_Buf.AddFileNm1
                    Dim iPos As Integer = sFileNm.IndexOf("\")

                    Do While iPos >= 0
                        sFileNm = sFileNm.Substring(iPos + 1)
                        iPos = sFileNm.IndexOf("\")
                    Loop

                    '-- 20090907 YEJ
                    sSql = ""
                    sSql += "INSERT INTO rrs12m(  bcno,  testcd,  rstno,  filenm,  filelen,  filebin )"
                    sSql += "            VALUES( :bcno, :testcd, :rstno, :filenm, :filelen, :filebin )"

                    With dbCmd
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                        .Parameters.Add("filenm", OracleDbType.Varchar2).Value = sFileNm
                        .Parameters.Add("filelen", OracleDbType.Varchar2).Value = a_btAddFile.Length
                        .Parameters.Add("filebin", OracleDbType.LongRaw, a_btAddFile.Length).Value = a_btAddFile

                        iRet += .ExecuteNonQuery()
                    End With
                End If

                If iRet > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnRegServer(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean
            Dim sFn As String = "Private Function fnRegServer(STU_RstInfo, STU_SampleInfo) As Boolean"
            Try
                '1) Select Rst Info
                sbGetRstInfo(r_sampinfo_Buf.BCNo)

                '2) Update rr010m, Insert rr011m
                Dim iEditRow As Integer = fnEdit_LR(r_rstinfo_Buf, r_sampinfo_Buf)


                If iEditRow = 0 Then Return False

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '< add freety 2005/07/08 : Prestatement(Binding Variable) Query
        Private Sub sbGetRstInfo(ByVal rsBCNo As String)

            Dim sFn As String = "Private Sub sbGetRstInfo(String)"

            Try
                If m_dt_rst Is Nothing Then
                    m_dt_rst = New DataTable
                Else
                    If m_dt_rst.Rows(0).Item("bcno").ToString().Trim = rsBCNo Then
                        Return
                    End If
                End If


                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = "pkg_ack_rst.pkg_get_resultinfo_r"
                'dbCmd.CommandText = "PKG_ACK_RST.PKG_GET_RESULTINFO_R"
                
                objDAdapter = New OracleDataAdapter(dbCmd)
                objDAdapter.SelectCommand.Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = rsBCNo
                objDAdapter.SelectCommand.Parameters.Add("io_cursor", OracleDbType.RefCursor).Value = ""
                objDAdapter.SelectCommand.Parameters("io_cursor").Direction = ParameterDirection.Output

                m_dt_rst.Reset()
                objDAdapter.Fill(m_dt_rst)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub


    End Class

    Public Class LogFn
        ' Error 로그
        Public Shared Sub Log(ByVal sLog As String)
            Dim sFile As String
            Dim sDir As String
            Dim sw As IO.StreamWriter
            Dim iErrLine As Integer = 0

            Try
                sDir = Windows.Forms.Application.StartupPath & "\RegLog"

                If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

                sFile = sDir & "\Reg" & Format(Now, "yyyy-MM-dd") & ".txt"

                sw = New IO.StreamWriter(sFile, True, System.Text.Encoding.UTF8)

                iErrLine = 1

                sw.WriteLine(Now())

                iErrLine = 2

                sw.WriteLine(vbTab & sLog)

                iErrLine = 3

                sw.Close()

            Catch ie As System.IO.IOException
                If iErrLine = 1 Then
                    sw.Close()
                End If

                'Recursive Call
                Log(sLog)

            Catch ex As System.Exception

            End Try
        End Sub

        ' Error 로그
        Public Shared Sub Log(ByVal sSenderID As String, ByVal sLog As String)
            Dim sFile As String
            Dim sDir As String
            Dim sw As IO.StreamWriter
            Dim iErrLine As Integer = 0

            Try
                sDir = Windows.Forms.Application.StartupPath & "\RegLog"

                If Dir(sDir, FileAttribute.Directory) = "" Then MkDir(sDir)

                sFile = sDir & "\Reg" & Format(Now, "yyyy-MM-dd") & sSenderID & ".txt"

                sw = New IO.StreamWriter(sFile, True, System.Text.Encoding.UTF8)

                iErrLine = 1

                sw.WriteLine(Now())

                iErrLine = 2

                sw.WriteLine(vbTab & sLog)

                iErrLine = 3

                sw.Close()

            Catch ie As System.IO.IOException
                If iErrLine = 1 Then
                    sw.Close()
                End If

                'Recursive Call
                Log(sSenderID, sLog)

            Catch ex As System.Exception

            End Try
        End Sub



    End Class
End Namespace
