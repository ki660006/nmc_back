﻿'*****************************************************************************************/
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
Imports Oracle.DataAccess.Types

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst


Namespace APP_R

    '-- 결과등록 공통
    Public Class RstFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.CommFn" + vbTab

        '-- 인증 검사항목 여부 채크
        Public Function fnGet_CSM_TEST_YES(ByVal rsBcNo As String, ByVal rsTestCd As String) As Boolean
            Dim sFn As String = "Function fnGet_CSM_TEST_YES(String) As Boolean"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT NVL(f.signrptyn, '0') signrptyn"
                sSql += "  FROM lr010m r, lf060m f"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND r.testcd = :testcd"
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   AND r.tkdt  <  f.uedt"

                alParm.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))


                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count < 1 Then Return False

                If dt.Rows(0).Item("signrptyn").ToString = "1" Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' JJH  I/F 신종코로나 CT 결과 이력
        Public Function fnGet_nCov_IFResult(ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Function fnGet_nCov_IFResult(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += " SELECT CASE WHEN B.RSTFLG = '3' THEN 'Y' ELSE 'N' END FLAG, "
                sSql += "        A.BCNO, A.TESTCD, C.SPCNMD, J.PATNM, A.SPCCD, A.REGNO, A.RST, TO_CHAR(TO_DATE(A.RSTDT, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') RSTDT, A.RSTID, "
                sSql += "        A.GBN AS RSTGBN, fn_ack_get_bcno_prt(A.BCNO) prtno, '999' SEQ, 'M' DTGBN,"
                sSql += "        cASE WHEN A.GBN = 'E' THEN 'E gene' "
                sSql += "             WHEN A.GBN = 'R' THEN 'RdRP gene' "
                sSql += "             WHEN A.GBN = 'I' THEN 'Internal control' "
                sSql += "        ELSE '' END GBN, "
                sSql += "        cASE WHEN A.GBN = 'I' THEN 1 "
                sSql += "             WHEN A.GBN = 'E' THEN 2 "
                sSql += "             WHEN A.GBN = 'R' THEN 3 "
                sSql += "        ELSE 999 END SEQNO "
                sSql += "   FROM LRS18M A, LJ010M J, LR010M B, LF030M C"
                sSql += "  WHERE A.REGNO  = :REGNO "
                sSql += "    AND A.BCNO   = J.BCNO"
                sSql += "    AND A.BCNO   = B.BCNO"
                sSql += "    AND A.TESTCD = B.TESTCD"
                sSql += "    AND A.SPCCD  = B.SPCCD"
                sSql += "    AND B.SPCCD  = C.SPCCD"
                sSql += "    AND B.TKDT  >= C.USDT"
                sSql += "    AND B.TKDT  <= C.UEDT"
                sSql += " UNION "
                sSql += " SELECT 'N' FLAG, A.BCNO, A.TESTCD, C.SPCNMD, J.PATNM, A.SPCCD, A.REGNO, A.RST, TO_CHAR(TO_DATE(A.RSTDT, 'YYYY-MM-DD HH24:MI:SS'), 'YYYY-MM-DD HH24:MI:SS') RSTDT, A.RSTID, "
                sSql += "        A.GBN AS RSTGBN, fn_ack_get_bcno_prt(A.BCNO) prtno,  A.SEQ, 'H' DTGBN, "
                sSql += "        cASE WHEN A.GBN = 'E' THEN 'E gene' "
                sSql += "             WHEN A.GBN = 'R' THEN 'RdRP gene' "
                sSql += "             WHEN A.GBN = 'I' THEN 'Internal control' "
                sSql += "        ELSE '' END GBN, "
                sSql += "        cASE WHEN A.GBN = 'I' THEN 1 "
                sSql += "             WHEN A.GBN = 'E' THEN 2 "
                sSql += "             WHEN A.GBN = 'R' THEN 3 "
                sSql += "        ELSE 999 END SEQNO "
                sSql += "   FROM LRS18H A, LJ010M J, LR010M B, LF030M C"
                sSql += "  WHERE A.REGNO  = :REGNO "
                sSql += "    AND A.BCNO   = J.BCNO"
                sSql += "    AND A.BCNO   = B.BCNO"
                sSql += "    AND A.TESTCD = B.TESTCD"
                sSql += "    AND A.SPCCD  = B.SPCCD"
                sSql += "    AND B.SPCCD  = C.SPCCD"
                sSql += "    AND B.TKDT  >= C.USDT"
                sSql += "    AND B.TKDT  <= C.UEDT"
                sSql += "  ORDER BY BCNO, FLAG, RSTDT, SEQNO "

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
                Return New DataTable
            End Try
        End Function

        Public Function fnGet_TEST() As DataTable
            Dim sFn As String = "Function fnGet_nCov_IFResult(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += " select regno, patnm, bcno, fn_ack_get_bcno_prt(BCNO) prtno from lj010m"
                sSql += " where bcno in ( "
                sSql += "'20200701C100040', "
                sSql += " '20200701C100050', "
                sSql += " '20200701C100060' ) "

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
                Return New DataTable
            End Try
        End Function

        '-- 검사항목에 정보
        Public Function fnGet_rstInfo_test(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
            Dim sFn As String = "Function fnGet_rstInfo_test(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim sTableNm As String = "lr010m"
                Dim alParm As New ArrayList

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(wkymd || NVL(wkgrpcd, '') || NVL(wkno, '')) workno, "
                sSql += "       fn_ack_date_str(rstdt, 'yyyy-mm-dd hh24:mi') rstd"
                sSql += "  FROM " + sTableNm
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"

                alParm.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

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
                sSql += "  ,b.partcd || b.slipcd partslip "
                sSql += "  FROM lj010m a, lr010m b"
                sSql += " WHERE a.regno  = :regno"
                sSql += "   AND a.bcno   = b.bcno"
                sSql += "   AND a.spcflg = '4'"

                alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                If rsPartSlip.Length = 1 Then
                    sSql += "  AND b.partcd = :partcd"
                    alParm.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                ElseIf rsPartSlip.Length = 2 Then
                    sSql += "  AND b.partcd || b.slipcd = :slipcd"
                    alParm.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))

                End If

                If rsTkDtS <> "" Then
                    sSql += "   AND b.tkdt >= :dates || '000000'"
                    sSql += "   AND b.tkdt <= :datee || '235959'"

                    alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE.Replace("-", "")))
                End If

                If rbBankYn Then
                    sSql += "   AND b.partcd = :partbnk"
                    alParm.Add(New OracleParameter("partbnk",  OracleDbType.Varchar2, PRG_CONST.PART_BloodBank.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.PART_BloodBank))
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
                sSql += "       CASE WHEN j.iogbn = 'I' THEN fn_ack_get_ward_name(j.wardno) || '/' || j.roomno ELSE fn_ack_get_dept_name('O',j.deptcd) END deptcd," '20131107 진료과숫자표시 변경
                sSql += "       j.statgbn,"
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM lj010m j, lr010m r, lf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM DUAL) s"
                sSql += " WHERE r.tkdt   >= :dates || '000000'"
                sSql += "   AND r.tkdt   <= :datee || '235959'"
                sSql += "   AND j.bcno    = r.bcno"

                alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsTkDts.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDts))
                alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsTkDte.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDte))

                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += "   AND f.partcd  = :partcd"

                alParm.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                If rsPartSlip.Length > 1 Then
                    sSql += "   AND f.slipcd = :slipcd"
                    alParm.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
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
        Public Shared Function fnGet_SpcList_TK(ByVal rsPartSlip As String, ByVal rsTkDts As String, ByVal rsTkDte As String, ByVal rsEr As String, ByVal rsMode As Integer) As DataTable
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
                sSql += "       CASE WHEN j.iogbn = 'I' THEN fn_ack_get_ward_name(j.wardno) || '/' || j.roomno ELSE fn_ack_get_dept_name('O',j.deptcd) END deptcd," '20131107 진료과숫자표시 변경
                sSql += "       j.statgbn ,"
                sSql += "       CASE WHEN j15.bcno is not null THEN 'Y' ELSE '' END eryn, " 'JJH 자체응급 추가
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       CASE WHEN j.statgbn = 'E' "
                sSql += "       THEN fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.perrptmi, NVL (ferrptmi, '')))" ' 2018-11-27 응급환자 tat시간 가져오기 추가
                sSql += "       ELSE fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, '')))" ' 2018-11-27 응급환자 tat시간 가져오기 추가
                sSql += "       END tat, "
                sSql += "       CASE WHEN j.statgbn = 'E' "
                sSql += "            THEN MIN(NVL(f.perrptmi, NVL (ferrptmi, ''))  - f.ALARMTER)"
                sSql += "            ELSE MIN(NVL(f.prptmi, NVL (frptmi, ''))  - f.ALARMT)   "
                sSql += "             END ALARMTIME, "
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "       ,j14.cancelyn, j14.brainyn"
                sSql += "  FROM lj010m j, lr010m r, lf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM DUAL) s"
                sSql += "       ,lj014m j14 "

                '<< JJH 자체응급
                sSql += "       ,lj015m j15 "

                If rsMode = 0 Then    '<<<20150806 결과시간으로 조회되도록 수정 
                    sSql += " WHERE r.tkdt   >= :dates || '000000'"
                    sSql += "   AND r.tkdt   <= :datee || '235959'"
                Else
                    sSql += " WHERE r.fndt   >= :dates || '000000'"
                    sSql += "   AND r.fndt   <= :datee || '235959'"
                End If

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

                sSql += "    AND j.bcno = j14.bcno(+)"
                sSql += "    AND j.bcno = j15.bcno(+)"

                If rsEr <> "" Then
                    'sSql += "   AND NVL(j.statgbn, '0') <> '0' "

                    '<< JJH 응급조회시 자체응급 포함
                    sSql += "   AND (NVL(j.statgbn, '0') <> '0' or NVL(j15.bcno, 'N') <> 'N')"
                End If


                sSql += " GROUP BY r.tkdt, j.bcno, j.regno, j.patnm, j.iogbn, j.wardno, j.roomno, j.deptcd, f.partcd, f.slipcd, j.statgbn, j.rstflg, j14.brainyn, j14.cancelyn, j15.bcno"

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
                sSql += "       CASE WHEN j15.bcno is not null THEN 'Y' ELSE '' END eryn, " '<< JJH 자체응급
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM lj010m j, lr010m r, lf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM   DUAL) s"

                sSql += "      , lj015m j15 "

                If rsWkYmdE <> "" Then
                    sSql += " WHERE r.wkymd   BETWEEN :wkymds AND :wkymde"
                    sSql += "   AND r.wkgrpcd = :wgprcd"

                    alParm.Add(New OracleParameter("wkymds",  OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("wkymde",  OracleDbType.Varchar2, rsWkYmdE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmdE))
                    alParm.Add(New OracleParameter("wgrpcd",  OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                Else
                    sSql += " WHERE r.wkymd   = :wkymd"
                    sSql += "   AND r.wkgrpcd = :wgrpcd"
                    sSql += "   AND r.wkno    BETWEEN :wknos AND :wknoe"

                    alParm.Add(New OracleParameter("wkymd",  OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("wgrpcd",  OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                    alParm.Add(New OracleParameter("wknos",  OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                    alParm.Add(New OracleParameter("wknoe",  OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))
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

                sSql += "   AND j.bcno = j15.bcno(+) "

                If rsEr <> "" Then
                    'sSql += "   AND NVL(j.statgbn, '0') <> '0'"
                    sSql += "   AND (NVL(j.statgbn, '0') <> '0' or NVL(j15.bcno, 'N') <> 'N')"
                End If


                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                sSql += " GROUP BY r.tkdt, j.bcno, j.regno, j.patnm, j.iogbn, j.wardno, j.roomno, j.deptcd, j.statgbn, j.rstflg, j15.bcno"
                sSql += "          ,f.partcd, f.slipcd, r.wkymd, r.wkgrpcd, r.wkno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 검사그룹 검사리스트 조회 
        Public Shared Function fnGet_SpcList_TGrp(ByVal rsTGrpCds As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                                  ByVal rsEr As String, Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "fnGet_SpcList_TGrp(String, ..., string) As DataTable("")"
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
                sSql += "       /*fn_ack_get_bcno_full (r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, ''))*/ '' workno,"
                sSql += "       j.regno,"
                sSql += "       j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptcd,"
                sSql += "       j.statgbn,"
                sSql += "       CASE WHEN j15.bcno is not null THEN 'Y' ELSE '' END eryn, " '<< JJH 자체응급
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       '' partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM lj010m j, lr010m r, lf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM   DUAL) s"

                sSql += "       , lj015m j15 "

                sSql += " WHERE r.tkdt   >= :dates || '000000'"
                sSql += "   AND r.tkdt   <= :datee || '235959'"
                sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd IN ('" + rsTGrpCds.Replace(",", "','") + "'))"
                sSql += "   AND j.bcno    = r.bcno"

                alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

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

                '<< JJH 자체응급
                sSql += "   AND j.bcno = j15.bcno(+) "
                If rsEr <> "" Then
                    'sSql += "   AND NVL(j.statgbn, '0') <> '0'"
                    sSql += "   AND (NVL(j.statgbn, '0') <> '0' or NVL(j15.bcno, 'N') <> 'N') "
                End If


                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                sSql += " GROUP BY r.tkdt, j.bcno, j.regno, j.patnm, j.iogbn, j.wardno, j.roomno, j.deptcd, j.statgbn, j.rstflg, j15.bcno" ', r.wkymd, r.wkgrpcd, r.wkno"

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
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptcd,"
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       MIN(NVL(r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                'sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       fn_ack_date_diff(MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (f.frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM lj010m j, lr010m r, lf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM DUAL) s"
                sSql += " WHERE r.eqcd   = :eqcd"
                sSql += "   AND r.rstdt >= :dates || '000000'"
                sSql += "   AND r.rstdt <= :datee || '235959'"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"
                sSql += "   AND j.bcno   = r.bcno"

                alParm.Add(New OracleParameter("eqcd", OracleDbType.Varchar2, rsEqCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsEqCd))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDt.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDt.Replace("-", "")))

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
        '20210513 jhs QC장비 코드 가져오기 
        '-- 장비별 검사리스트 조회
        Public Shared Function fnGet_SpcList_Qceqcd(ByVal rsEqCd As String) As DataTable
            Dim sFn As String = "SectionListSelect(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim strWhere As String = ""

                sSql += " "
                sSql += "select distinct f7.qceqcd" + vbCrLf
                sSql += "  from lf070m f7" + vbCrLf
                sSql += " where f7.eqcd = '" + rsEqCd + "'" + vbCrLf
                sSql += "   and f7.delflg = '0'" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '----------------------------------------------

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
                '<20130710 정선영 수정
                sSql += "       FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno) ELSE '' END deptcd,"
                'sSql += "       CASE WHEN j.iogbn = 'I' THEN j.deptcd || '/' || j.wardno ELSE j.deptcd END deptcd,"
                '>
                sSql += "       NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd, f6.tcdgbn, f6.titleyn, f6.plgbn,"
                sSql += "       r.orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno || '/' || j.roomno wardroom"
                sSql += "  FROM lj010m j, lr010m r, lf060m f6, lf030m f3"

                If rsBcno <> "" Then
                    sSql += " WHERE j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                ElseIf rsWkYmd <> "" Then
                    sSql += " WHERE r.wkymd   = :wkymd"
                    sSql += "   AND r.wkgrpcd = :wgrpcd"
                    sSql += "   AND r.wkno   >= :wknos"
                    sSql += "   AND r.wkno   <= :wknoe"

                    alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
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
        '-- 검사항목별 검사리스트 조회 (보고일)
        Public Shared Function fnGet_SpcList_Test2(ByVal rsTestCds As String, ByVal rsWkYmd As String, ByVal rsWkGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, ByVal rsRstNullReg As String, ByVal rsTkDtB As String, ByVal rsTkDtE As String, _
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
                '<20130710 정선영 수정
                sSql += "       FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno) ELSE '' END deptcd,"
                'sSql += "       CASE WHEN j.iogbn = 'I' THEN j.deptcd || '/' || j.wardno ELSE j.deptcd END deptcd,"
                '>
                sSql += "       NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd, f6.tcdgbn, f6.titleyn, f6.plgbn,"
                sSql += "       r.orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno || '/' || j.roomno wardroom"
                sSql += "  FROM lj010m j, lr010m r, lf060m f6, lf030m f3"

                If rsBcno <> "" Then
                    sSql += " WHERE j.bcno = :bcno"
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                ElseIf rsWkYmd <> "" Then
                    sSql += " WHERE r.wkymd   = :wkymd"
                    sSql += "   AND r.wkgrpcd = :wgrpcd"
                    sSql += "   AND r.wkno   >= :wknos"
                    sSql += "   AND r.wkno   <= :wknoe"

                    alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                    alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                    alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))
                Else
                    sSql += " WHERE r.fndt >= :dates || '0000'"
                    sSql += "   AND r.fndt <= :datee || '5959'"

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
                sSql += "  FROM lrw11m w, lr010m r, lf060m f6, lf021m f2"
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
                sSql += "       CASE WHEN j15.bcno is not null THEN 'Y' ELSE '' END eryn, "
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn,"
                sSql += "       NVL(j.rstflg, '0') rstflg,"
                sSql += "       f.partcd || f.slipcd partslip,"
                sSql += "       MIN (NVL (r.rstflg, '0')) || MAX (NVL (r.rstflg, '0')) rstflg_t,"
                sSql += "       fn_ack_date_diff (MIN (r.wkdt), MIN(NVL(r.rstdt, s.sysdt)), '3') || '^' || MIN (NVL (f.prptmi, NVL (frptmi, ''))) tat,"
                sSql += "       MAX(NVL(r.hlmark, ' ')) hl, MAX(NVL(r.panicmark, ' ')) pm, MAX(NVL(r.deltamark, ' ')) dm,"
                sSql += "       MAX(NVL(r.alertmark, ' ')) am, MAX(NVL(r.criticalmark, ' ')) cm,"
                sSql += "       MAX(NVL(r.eqflag, ' ')) eqflag, MAX(NVL(r.rerunflg, '0')) rerun"
                sSql += "  FROM lrw11m w, lj010m j, lr010m r, lf060m f,"
                sSql += "       (SELECT TO_CHAR (SYSDATE, 'yyyymmddhh24miss') sysdt FROM   DUAL) s"

                sSql += "       , lj015m j15 "

                sSql += " WHERE w.wluid   = :wluid"
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

                sSql += "   AND j.bcno = j15.bcno(+) "

                If rsEr <> "" Then
                    'sSql += "   AND NVL(j.statgbn, '0') <> '0'"
                    sSql += "   AND (NVL(j.statgbn, '0') <> '0' or NVL(j15.bcno, 'N') <> 'N')"
                End If


                If rsRegNo = "" Then

                    If rsReRun = "RERUN" Then
                        sSql += "   AND NVL(r.rerunflg, '0') = '1'"
                    ElseIf rsReRun = "NOTRERUN" Then
                        sSql += "   AND j.bcno NOT IN (SELECT w.bcno FROM lrw11m w, lr010m"
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
                        sSql += "   AND j.bcno NOT IN (SELECT w.bcno FROM lrw11m w, lr010m"
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
                        sSql += "   AND j.bcno NOT IN (SELECT w.bcno FROM lrw11m w, lr010m"
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

                sSql += " GROUP BY w.wlseq, j.regno, j.patnm, j.bcno, r.tkdt, j.iogbn, j.wardno, j.roomno, j.deptcd, j.statgbn, j15.bcno"
                sSql += "       ,f.partcd, f.slipcd, j.rstflg"

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
                '<20130710 정선영 수정
                sSql += "       FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno) ELSE '' END deptcd,"
                'sSql += "       CASE WHEN j.iogbn = 'I' THEN j.deptcd || '/' || j.wardno ELSE j.deptcd END deptcd,"
                '>
                sSql += "       NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd, f6.tcdgbn, f6.titleyn, f6.plgbn,"
                sSql += "       f6.partcd || f6.slipcd partslip,"
                sSql += "       r.orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno || '/' || j.roomno wardroom"
                sSql += "  FROM lrw11m w, lj010m j, lr010m r,"
                sSql += "       lf060m f6, lf030m f3"
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

                ' sSql += "SELECT j.regno, j.deptcd, j.doctorcd, fn_ack_get_usr_telno(:usrid) telno"
                sSql += "SELECT j.regno, j.deptcd, "
                sSql += "       nvl(fn_ack_get_ocs_gendrid_bcno('" + rsBcNo + "'), j.doctorcd) doctorcd, fn_ack_get_usr_telno(:usrid) telno"
                sSql += "  FROM lj010m j"
                sSql += " WHERE j.bcno = :bcno"

                alParm.Add(New OracleParameter("usrid", USER_INFO.USRID))
                alParm.Add(New OracleParameter("bcno", rsBcNo))

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
                sSql += "   FROM lf180m"

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
                sSql += "SELECT r.testcd, r.spccd, r.rstflg, NVL(j.rstflg, '0') rstflg_j,"
                sSql += "       fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, r.regid, fn_ack_get_usr_name(regid) regnm,"
                sSql += "  	    fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi:ss')  mwdt,  r.mwid,  fn_ack_get_usr_name(mwid)  mwnm,"
                sSql += " 	    fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi:ss')  fndt,  r.fnid,  fn_ack_get_usr_name(fnid)  fnnm,"
                sSql += "       fn_ack_get_usr_name(r.cfmnm) cfmnm"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m r, lj010m j"
                Else
                    sSql += "  FROM lr010m r, lj010m j"
                End If
                sSql += " WHERE j.bcno LIKE :bcno || '%'"
                sSql += "   AND j.bcno = r.bcno"


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
                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql = "pkg_ack_qry.pkg_get_rst_test_m"
                    ElseIf PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql = "pkg_ack_qry.pkg_get_rst_test_r"
                    Else
                        sSql = "pkg_ack_qry.pkg_get_rst_test"
                    End If
                Else
                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql = "pkg_ack_qry.pkg_get_rst_testspc_m"
                    ElseIf PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                        sSql = "pkg_ack_qry.pkg_get_rst_testspc_r"
                    Else
                        sSql = "pkg_ack_qry.pkg_get_rst_testspc"
                    End If
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
                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010"

                sSql += "SELECT DISTINCT"
                sSql += "       c.tnmd, a.testcd, a.orgrst, a.viewrst, a.rstcmt, a.rstflg,"
                sSql += "       a.hlmark, a.panicmark, a.deltamark, a.alertmark, a.criticalmark, c.tcdgbn, a.tclscd,"
                sSql += "       fn_ack_get_test_reftxt(c.refgbn, b.sex, d.reflms, d.reflm, d.refhms, d.refhm, d.reflfs, d.reflf, d.refhfs, d.refhf, d.reflt) reftxt,"
                sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(a.regid) regnm,"
                sSql += "       fn_ack_date_str(a.MWDT,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(a.mwid)  mwnm,"
                sSql += "       fn_ack_date_str(a.fndt, 'yyyy-mm-dd hh24:mi:ss')  fndt,  fn_ack_get_usr_name(a.fnid)  fnnm,"
                sSql += "       fn_ack_get_bcno_full(a.bcno) bcno, a.rstdt,"
                'sSql += "       fn_ack_get_slip_dispseq(c.partcd, c.slipcd, a.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = c.partcd AND slipcd = c.slipcd AND usdt <= b.bcprtdt AND uedt > b.bcprtdt) sort1,"
                sSql += "       NVL(c.dispseqL, 999) sort2"
                sSql += "  FROM lj010m b, lf060m c,"
                sSql += "       ("
                sSql += "        SELECT regno, testcd, spccd, MAX(rstdt) rstdt"
                sSql += "          FROM lr010m"
                sSql += "         WHERE rstflg IN ('2', '3')"
                sSql += "           AND bcno   <> :bcno"
                sSql += "           AND regno   = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
                sSql += "           AND tkdt   <= (SELECT tkdt  FROM lr010m WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (testcd, spccd) IN (SELECT reftestcd, refspccd FROM lf063m WHERE testcd = :testcd AND spccd = :spccd)"
                sSql += "         GROUP BY regno, testcd, spccd"
                sSql += "       ) r,"
                sSql += "       " + sTableNm + " a,"
                sSql += "       (SELECT DISTINCT f61.*"
                sSql += "          FROM lf060M f6, lf061M f61, lj010m j, " + sTableNm + " r"
                sSql += "         WHERE j.regno    = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
                sSql += "           AND j.bcno     = r.bcno"
                sSql += "           AND r.tkdt    <= (SELECT tkdt FROM lr010m WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (f6.testcd, f6.spccd) IN (SELECT reftestcd, refspccd FROM lf063m WHERE testcd = :testcd and spccd = :spccd)"
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
                sSql += " WHERE a.regno  = r.regno"
                sSql += "   AND a.testcd = r.testcd"
                sSql += "   AND a.spccd  = r.spccd"
                sSql += "   AND a.rstdt  = r.rstdt"
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
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '2018-06-29 yjh lr010m, lm010m 테이블 모두 조회할 수 있도록 수정
        Public Shared Function fnGet_Result_Ref_All(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_Ref(String, String, string) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql += "SELECT DISTINCT"
                sSql += "       c.tnmd, a.testcd, a.orgrst, a.viewrst, a.rstcmt, a.rstflg,"
                sSql += "       a.hlmark, a.panicmark, a.deltamark, a.alertmark, a.criticalmark, c.tcdgbn, a.tclscd,"
                sSql += "       fn_ack_get_test_reftxt(c.refgbn, b.sex, d.reflms, d.reflm, d.refhms, d.refhm, d.reflfs, d.reflf, d.refhfs, d.refhf, d.reflt) reftxt,"
                sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(a.regid) regnm,"
                sSql += "       fn_ack_date_str(a.MWDT,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(a.mwid)  mwnm,"
                sSql += "       fn_ack_date_str(a.fndt, 'yyyy-mm-dd hh24:mi:ss')  fndt,  fn_ack_get_usr_name(a.fnid)  fnnm,"
                sSql += "       fn_ack_get_bcno_full(a.bcno) bcno, a.rstdt,"
                'sSql += "       fn_ack_get_slip_dispseq(c.partcd, c.slipcd, a.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = c.partcd AND slipcd = c.slipcd AND usdt <= b.bcprtdt AND uedt > b.bcprtdt) sort1,"
                sSql += "       NVL(c.dispseqL, 999) sort2"
                sSql += "  FROM lj010m b, lf060m c,"
                sSql += "       ("
                sSql += "        SELECT regno, testcd, spccd, MAX(rstdt) rstdt"
                sSql += "          FROM lr010m"
                sSql += "         WHERE rstflg IN ('2', '3')"
                sSql += "           AND bcno   <> :bcno"
                sSql += "           AND regno   = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
                sSql += "           AND tkdt   <= (SELECT tkdt  FROM " + sTableNm + " WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (testcd, spccd) IN (SELECT reftestcd, refspccd FROM lf063m WHERE testcd = :testcd AND spccd = :spccd)"
                sSql += "         GROUP BY regno, testcd, spccd"
                sSql += "       ) r,"
                sSql += "       lr010m a,"
                sSql += "       (SELECT DISTINCT f61.*"
                sSql += "          FROM lf060M f6, lf061M f61, lj010m j, lr010m r"
                sSql += "         WHERE j.regno    = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
                sSql += "           AND j.bcno     = r.bcno"
                sSql += "           AND r.tkdt    <= (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (f6.testcd, f6.spccd) IN (SELECT reftestcd, refspccd FROM lf063m WHERE testcd = :testcd and spccd = :spccd)"
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
                sSql += " WHERE a.regno  = r.regno"
                sSql += "   AND a.testcd = r.testcd"
                sSql += "   AND a.spccd  = r.spccd"
                sSql += "   AND a.rstdt  = r.rstdt"
                sSql += "   AND a.bcno   = b.bcno"
                sSql += "   AND a.tkdt  >= c.usdt"
                sSql += "   AND a.tkdt  <  c.uedt"
                sSql += "   AND a.testcd = c.testcd"
                sSql += "   AND a.spccd  = c.spccd"
                sSql += "   AND a.testcd = d.testcd (+)"
                sSql += "   AND a.spccd  = d.spccd (+)"
                'sSql += " ORDER BY rstdt DESC, sort1, sort2, testcd"

                sSql += " union all "

                sSql += "SELECT DISTINCT"
                sSql += "       c.tnmd, a.testcd, a.orgrst, a.viewrst, a.rstcmt, a.rstflg,"
                sSql += "       a.hlmark, a.panicmark, a.deltamark, a.alertmark, a.criticalmark, c.tcdgbn, a.tclscd,"
                sSql += "       fn_ack_get_test_reftxt(c.refgbn, b.sex, d.reflms, d.reflm, d.refhms, d.refhm, d.reflfs, d.reflf, d.refhfs, d.refhf, d.reflt) reftxt,"
                sSql += "       fn_ack_date_str(a.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(a.regid) regnm,"
                sSql += "       fn_ack_date_str(a.MWDT,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(a.mwid)  mwnm,"
                sSql += "       fn_ack_date_str(a.fndt, 'yyyy-mm-dd hh24:mi:ss')  fndt,  fn_ack_get_usr_name(a.fnid)  fnnm,"
                sSql += "       fn_ack_get_bcno_full(a.bcno) bcno, a.rstdt,"
                'sSql += "       fn_ack_get_slip_dispseq(c.partcd, c.slipcd, a.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = c.partcd AND slipcd = c.slipcd AND usdt <= b.bcprtdt AND uedt > b.bcprtdt) sort1,"
                sSql += "       NVL(c.dispseqL, 999) sort2"
                sSql += "  FROM lj010m b, lf060m c,"
                sSql += "       ("
                sSql += "        SELECT regno, testcd, spccd, MAX(rstdt) rstdt"
                sSql += "          FROM lm010m"
                sSql += "         WHERE rstflg IN ('2', '3')"
                sSql += "           AND bcno   <> :bcno"
                sSql += "           AND regno   = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
                sSql += "           AND tkdt   <= (SELECT tkdt  FROM " + sTableNm + " WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (testcd, spccd) IN (SELECT reftestcd, refspccd FROM lf063m WHERE testcd = :testcd AND spccd = :spccd)"
                sSql += "         GROUP BY regno, testcd, spccd"
                sSql += "       ) r,"
                sSql += "       lm010m a,"
                sSql += "       (SELECT DISTINCT f61.*"
                sSql += "          FROM lf060M f6, lf061M f61, lj010m j, lm010m r"
                sSql += "         WHERE j.regno    = (SELECT regno FROM lj010m WHERE bcno = :bcno)"
                sSql += "           AND j.bcno     = r.bcno"
                sSql += "           AND r.tkdt    <= (SELECT tkdt FROM " + sTableNm + " WHERE bcno = :bcno AND ROWNUM = 1)"
                sSql += "           AND (f6.testcd, f6.spccd) IN (SELECT reftestcd, refspccd FROM lf063m WHERE testcd = :testcd and spccd = :spccd)"
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
                sSql += " WHERE a.regno  = r.regno"
                sSql += "   AND a.testcd = r.testcd"
                sSql += "   AND a.spccd  = r.spccd"
                sSql += "   AND a.rstdt  = r.rstdt"
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
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

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
                Dim sTableNm As String = "lr"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm"

                sSql += "SELECT DISTINCT"
                sSql += "       r.regno, f6.tnmd, r1.testcd, r1.orgrst, r1.viewrst, r1.rstcmt, r1.rstflg,"
                sSql += "       r1.hlmark, r1.panicmark, r1.deltamark, r1.alertmark, r1.criticalmark, f6.tcdgbn, r.tclscd,"
                sSql += "       fn_ack_get_test_reftxt(f6.refgbn, j.sex, re.reflms, re.reflm, re.refhms, re.refhm, re.reflfs, re.reflf, re.refhfs, re.refhf, re.reflt) reftxt,"
                sSql += "       fn_ack_date_str(r1.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(r1.regid) regnm,"
                sSql += "       fn_ack_date_str(r1.mwdt,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(r1.mwid)  mwnm,"
                sSql += "       fn_ack_date_str(r1.fndt,  'yyyy-mm-dd hh24:mi:ss') fndt,  fn_ack_get_usr_name(r1.fnid)  fnnm,"
                sSql += "       fn_ack_date_str(r1.sysdt, 'yyyy-mm-dd hh24:mi:ss') sysdt,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                sSql += "       (SELECT dispseql FROM lf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                sSql += "       NVL(f6.dispseql, 999) sort3"
                sSql += "  FROM " + sTableNm + "011m r1, lj010m j, lf060m f6,"
                sSql += "       " + sTableNm + "010m r LEFT OUTER JOIN"
                sSql += "       (SELECT DISTINCT f61.*"
                sSql += "          FROM lf060m f6, lf061m f61,"
                sSql += "               lj010m j, " + sTableNm + "010M r"
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
                sSql += "       ) re ON (r.testcd = re.testcd AND r.spccd  = re.spccd)"
                sSql += " WHERE j.bcno   = :bcno"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND r.bcno   = r1.bcno"
                sSql += "   AND r.testcd = r1.testcd"
                sSql += "   AND r.spccd  = r1.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += " ORDER BY sysdt, sort1, sort2, sort3, testcd"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '<< JJH CVR 등록 목록 조회
        Public Shared Function fnGet_CVRList(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_ResultHistory(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList


                sSql += " SELECT examnm as tnmd, dtltestcd as testcd, rst, usernm as regnm, to_char(fstrgstdt,'yyyy-mm-dd hh24:mi:ss') regdt, execprcpuniqno as fkocs, rstunit "
                sSql += "   FROM LIS_CVR_INFO "
                sSql += "  WHERE BARCODENO = :BCNO"
                sSql += "  ORDER BY FSTRGSTDT "

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
                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql += ""
                sSql += "SELECT j.bcno, j.regno, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       fn_ack_get_dept_name(j.iogbn, j.deptcd) deptnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN fn_ack_get_ward_abbr(j.wardno) || '/' || j.roomno ELSE '' END wardroom,"
                sSql += "       j.spcflg,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tknm,"
                sSql += "       f.tcdgbn, r.tclscd, r.testcd, r.spccd, f3.spcnmd,"
                sSql += "       f.tnmd,  r.viewrst, r.rstflg,"
                sSql += "       r.hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       CASE WHEN criticalmark = 'C' THEN '1' ELSE '' END chk,"
                sSql += "       f.partcd || f.slipcd partslip, "
                'sSql += "       fn_ack_get_slip_dispseq(f.partcd, f.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f.partcd AND slipcd = f.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                sSql += "       (SELECT dispseql FROM lf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                sSql += "       f.dispseql sort3"
                sSql += "  FROM lj010m j, lj011m j1, " + sTableNm + " r,"
                sSql += "       lf060m f, lf030m f3"
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

                alParm.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                If rsPartSlip <> "" Then
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd",  OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd",  OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += " ORDER BY sort1, sort2, sort3"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        Public Shared Function fnGet_LIS_SMS_SEQ() As String
            Dim sFn As String = "Public Shared Function fnGet_LIS_SMS_SEQ() As String"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sSeq As String = "0"

                sSql += " select max(lisseq) + 1 as lisseq from lr054m " + vbCrLf

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    sSeq = dt.Rows(0).Item("lisseq").ToString()
                End If

                Return sSeq

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        '-- 특이결과 확인자 설정(대장)
        Public Shared Function fnExe_Abnormal_Cfm(ByVal ra_CfmInfo As ArrayList, ByVal rsCfmId As String, ByVal rsCfmCont As String) As Boolean
            Dim sFn As String = "Function fnExe_Abnormal_Reg(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                For ix As Integer = 0 To ra_CfmInfo.Count - 1
                    sSql = ""
                    sSql += "UPDATE lr050m SET"
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
                        .Parameters.Add("cfmid",  OracleDbType.Varchar2).Value = rsCfmId
                        .Parameters.Add("cfmcont",  OracleDbType.Varchar2).Value = rsCfmCont
                        .Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("regdt",  OracleDbType.Varchar2).Value = ra_CfmInfo.Item(ix).ToString.Split("|"c)(0)
                        .Parameters.Add("regid",  OracleDbType.Varchar2).Value = ra_CfmInfo.Item(ix).ToString.Split("|"c)(1)

                        iRet += .ExecuteNonQuery()

                    End With
                Next

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

        '-- 특이결과 확인자 설정(결과조회)
        Public Shared Function fnExe_Abnormal_Cfm(ByVal rsRegNo As String, ByVal rsCfmId As String, ByVal rsCfmCont As String) As Boolean
            Dim sFn As String = "Function fnExe_Abnormal_Reg(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                sSql = ""
                sSql += "UPDATE lr050m SET"
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
                    .Parameters.Add("cfmid",  OracleDbType.Varchar2).Value = rsCfmId
                    .Parameters.Add("cfmcont",  OracleDbType.Varchar2).Value = rsCfmCont
                    .Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("regno",  OracleDbType.Varchar2).Value = rsRegNo

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
                                                  ByVal rsRegNo As String, Optional ByVal rsLisSeq As String = "0") As Boolean
            Dim sFn As String = "Function fnExe_Special_Reg(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                sSql = ""
                sSql += "INSERT INTO lr050m("
                sSql += "            regdt, regid, regip, partcd, slipcd, bcno, cmtcont, cmtcd, regno, editid, editip, editdt , lisseq"
                sSql += "          )"
                sSql += "    VALUES( fn_ack_sysdate, :regid, :regip, :partcd, :slipcd, :bcno, :cmtcont, :cmtcd, :regno, :editid, :editip, fn_ack_sysdate, :lisseq )"

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
                    .Parameters.Add("lisseq", OracleDbType.Varchar2).Value = rsLisSeq

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

    End Class

    '-- 부적합검체 관련
    Public Class UnifitFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.UnifitFn" + vbTab

        '< 20121211 검체상태조회하는 함수 

        Public Shared Function fnGet_SpcInfo(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "Function fnGet_ResultHistory(String) As DataTable"

            Try
                Dim sSql As String = ""
                sSql = ""
                sSql += "select j.bcno,j.spcflg,j.rstflg, nvl(r.regdt,'0') wrYn"
                sSql += "  from lj011m j left outer join lr053m r "
                sSql += "    on r.bcno = j.bcno "
                sSql += " WHERE j.bcno = :bcno"

                DbCommand()

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

    End Class

    '-- TAT 사유
    Public Class TatFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.UnifitFn" + vbTab

        Public Shared Function fnGet_TatInfo_bcno(ByVal rsBcno As String, ByVal rsPartSlip As String) As DataTable
            Dim sFn As String = "Function fnGet_Abnormal_RstInfo(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) Then sTableNm = "lm010m"

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
                sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), NVL(r.mwdt, fn_ack_sysdate), '1') tat1,"
                sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), NVL(r.fndt, fn_ack_sysdate), '1') tat2,"
                'sSql += "       fn_ack_get_slip_dispseq(f.partcd, f.slipcd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f.partcd AND slipcd = f.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                sSql += "       (SELECT dispseql FROM lf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                sSql += "       f.dispseql sort3,"
                sSql += "       r5.cmtcont"
                sSql += "  FROM lj010m j "
                sSql += "       INNER JOIN lj011m j1 ON (j.bcno = j1.bcno)"
                sSql += "       INNER JOIN " + sTableNm + " r ON (j1.bcno = r.bcno AND j1.tclscd = r.tclscd)"
                sSql += "       INNER JOIN lf060m f ON (r.testcd = f.testcd AND r.spccd = f.spccd AND r.tkdt >= f.usdt AND r.tkdt < f.uedt)"
                sSql += "       INNER JOIN lf030m f3 ON (r.spccd = f3.spccd AND r.tkdt >= f3.usdt AND r.tkdt < f3.uedt)"
                sSql += "       LEFT OUTER JOIN lr051m r5 ON (j1.bcno = r5.bcno AND j1.tclscd = r5.testcd)"
                sSql += " WHERE j.bcno    = :bcno"
                sSql += "   AND NVL(f.tatyn, '0') = '1'"

                alParm.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

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
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sBuf() As String = rsTestCds.Split("|"c)
            If sBuf.Length < 1 Then Return True

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                For ix As Integer = 0 To sBuf.Length - 1



                    If sBuf(ix) <> "" Then
                        sSql = ""
                        sSql += "UPDATE lr051m SET"
                        sSql += "       regdt   = fn_ack_sysdate,"
                        sSql += "       regid   = :regid,"
                        sSql += "       regip   = :regip,"
                        sSql += "       cmtcd   = :cmtcd,"
                        sSql += "       cmtcont = :cmtcont"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND testcd  = :testcd"


                        With dbCmd
                            .Connection = dbCn
                            .Transaction = dbTran
                            .CommandType = CommandType.Text

                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("regip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                            .Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd
                            .Parameters.Add("cmtcont",  OracleDbType.Varchar2).Value = rsCmtCont
                            .Parameters.Add("bcno",  OracleDbType.Varchar2).Value = rsBcno
                            .Parameters.Add("testcd",  OracleDbType.Varchar2).Value = sBuf(ix)

                            iRet = .ExecuteNonQuery()
                        End With

                        If iRet < 1 Then
                            sSql = ""
                            sSql += "INSERT INTO lr051m("
                            sSql += "            regdt,           regid,  regip,  bcno,  testcd,  cmtcd,  cmtcont )"
                            sSql += "    VALUES( fn_ack_sysdate, :regid, :regip, :bcno, :testcd, :cmtcd, :cmtcont)"

                            With dbCmd
                                .Connection = dbCn
                                .Transaction = dbTran
                                .CommandType = CommandType.Text

                                .CommandText = sSql

                                .Parameters.Clear()
                                .Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                                .Parameters.Add("regip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                                .Parameters.Add("bcno",  OracleDbType.Varchar2).Value = rsBcno
                                .Parameters.Add("testcd",  OracleDbType.Varchar2).Value = sBuf(ix)
                                .Parameters.Add("cmtcd",  OracleDbType.Varchar2).Value = rsCmtCd
                                .Parameters.Add("cmtcont",  OracleDbType.Varchar2).Value = rsCmtCont

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
                dbTran.Dispose() : dbTran = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        '<20130212 ymg TAT 소견 삭제기능 추가 
        Public Shared Function fnExe_Tat_CmtDel(ByVal rsBcno As String, ByVal rsTestCds As String, ByVal rsCmtCd As String, ByVal rsCmtCont As String) As Boolean
            Dim sFn As String = "Function fnExe_Tat_CmtDel(String, ...) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Dim sBuf() As String = rsTestCds.Split("|"c)
            If sBuf.Length < 1 Then Return True

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                For ix As Integer = 0 To sBuf.Length - 1

                    If sBuf(ix) <> "" Then

                        sSql = ""
                        sSql += "DELETE FROM LR051M"
                        sSql += " WHERE bcno   = :bcno"
                        sSql += "   AND testcd = :testcd"

                        With dbCmd
                            .Connection = dbCn
                            .Transaction = dbTran
                            .CommandType = CommandType.Text
                            .CommandText = sSql
                            .Parameters.Clear()
                            .Parameters.Add("bcno",  OracleDbType.Varchar2).Value = rsBcno
                            .Parameters.Add("testcd",  OracleDbType.Varchar2).Value = sBuf(ix)

                            iRet = .ExecuteNonQuery()
                        End With

                    End If

                Next

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

    End Class

    '-- poct 결과 등록 관련..
    Public Class PoctFn
        Private Const msFile As String = "File : CGLISAPP_R.vb, Class : LISAPP.APP_R.UnifitFn" + vbTab

        Public Shared Function fnGet_Result_fkocs(ByVal rsOwnGbn As String, ByVal rsFkOcs As String, ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Function fnGet_Result_regno"

            Try
                Dim sSql As String = ""

                If rsOwnGbn = "L" Then
                    sSql = "pkg_ack_rst.pkg_get_result_fkocs_l"
                Else
                    sSql = "pkg_ack_rst.pkg_get_result_fkocs_o"
                End If

                Dim oParm As New DBORA.DbParrameter

                With oParm
                    .AddItem("rs_fkocs",  OracleDbType.Varchar2, ParameterDirection.Input, rsFkOcs)
                    .AddItem("rs_regno",  OracleDbType.Varchar2, ParameterDirection.Input, rsRegNo)
                End With

                DbCommand(False)

                Dim dt As DataTable = DbExecuteQuery(sSql, oParm, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_poct_rstinfo(ByVal rsTestCd As String) As DataTable
            Dim sFn As String = "Function fnGet_poct_rstinfo(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT b.testcd, b.keypad, b.rstcont, b.grade, b.rstcdseq"
                sSql += "  FROM lf083m b, lf060m f"
                sSql += " WHERE b.testcd  = :testcd"
                sSql += "   AND b.testcd  = f.testcd"
                sSql += "   AND (b.spccd  = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "' OR b.spccd = f.spccd)"
                sSql += "   AND f.usdt   <= fn_ack_sysdate"
                sSql += "   AND f.uedt   >  fn_ack_sysdate"

                sSql += " ORDER BY testcd, LENGTH(keypad), keypad"

                alParm.Add(New OracleParameter("testcd",  OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
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
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT fn_ack_date_str(regdt, 'yyyy-mm-dd hh24:mi') regdt, fn_ack_get_usr_name(regid) regnm, bfregno, regno"
                sSql += "  FROM lrc10m"
                sSql += " WHERE regdt >= :dates "
                sSql += "   AND regdt <= :datee || '235959'"

                al.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegno <> "" Then
                    sSql += "   AND bfregno <= :regno "
                    al.Add(New OracleParameter("regdno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
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
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT regno, patnm, sex, age"
                sSql += "  FROM lj010m"
                sSql += " WHERE regno = :regno"

                al.Clear()
                al.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

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
                sSql += "          FROM lr010m"
                sSql += "         WHERE regno = :regno"
                sSql += "         UNION ALL"
                sSql += "        SELECT regno, bcno, tclscd, testcd, spccd, viewrst, hlmark, panicmark, deltamark, rstflg,"
                sSql += "               tkdt, tkid, regdt, regid, mwdt, mwid, fndt, fnid"
                sSql += "          FROM lm010m"
                sSql += "         WHERE regno = :regno"
                sSql += "       ) r, lj010m j, lf060m f6, lf030m f3"
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

                sSql += " UNION "
                sSql += "SELECT a.orddt, a.tnsjubsuno bcno, f3.spcnmd, f.comnmd tnmd, '' viewrst, null fndt, b.comcd testcd, '' rstflg, '' chk, 'B' jobgbn"
                sSql += "  FROM lb040m a, lb042m b, lf120m f, lf030m f3"
                sSql += " WHERE a.regno      = :regno"
                sSql += "   AND a.orddt     >= :dates"
                sSql += "   AND a.orddt     <= :datee || '235959'"
                sSql += "   AND a.delflg     = '0'"
                sSql += "   AND a.tnsjubsuno = b.tnsjubsuno"
                sSql += "   AND b.comcd      = f.comcd"
                sSql += "   AND a.jubsudt   >= f.usdt"
                sSql += "   AND a.jubsudt   <  f.uedt"
                sSql += "   AND b.spccd      = f3.spccd"
                sSql += "   AND a.jubsudt   >= f3.usdt"
                sSql += "   AND a.jubsudt   <  f3.uedt"
                sSql += " ORDER BY jobgbn, bcno desc, testcd"

                al.Clear()
                al.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                al.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                al.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                al.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                al.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

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

                    If rsTableNM(i).ToLower = "slxboutt" Then
                        sSql = ""
                        sSql += "UPDATE " + rsTableNM(i)
                        sSql += "   SET patno = '" + rsRegNo_chg + "'"
                        sSql += " WHERE (patno, orddate, execprcpuniqno, ioflag) IN"
                        sSql += "       (SELECT '" + rsRegNo + "', SUBSTR(orddt, 1, 8), ocs_key, SUBSTR(fkocs, 1, 1)"
                        sSql += "          FROM lb043m"
                        sSql += "         WHERE tnsjubsuno IN ('" + rsBcNos.Replace(",", "','") + "')"
                        sSql += "       )"
                    ElseIf rsTableNM(i).ToLower = "mdresult" Then
                        sSql = ""
                        sSql += "UPDATE " + rsTableNM(i)
                        sSql += "   SET patno = '" + rsRegNo_chg + "'"
                        sSql += " WHERE (patno, orddate, execprcpuniqno, ioflag) IN"
                        sSql += "       (SELECT '" + rsRegNo + "', SUBSTR(orgorddt, 1, 8), ocs_key, SUBSTR(fkocs, 1, 1)"
                        sSql += "          FROM lj011m"
                        sSql += "         WHERE bcno IN ('" + rsBcNos.Replace(",", "','") + "')"
                        sSql += "       )"
                    ElseIf rsTableNM(i).ToLower = "lb040m" Then
                        sSql = ""
                        sSql += "UPDATE " + rsTableNM(i)
                        sSql += "   SET regno = '" + rsRegNo_chg + "', patnm = '" + rsPatNm + "'"
                        sSql += " WHERE tnsjubsuno IN ('" + rsBcNos.Replace(",", "','") + "')"

                    ElseIf rsTableNM(i).ToLower = "lb043m" Or rsTableNM(i).ToLower = "lb043h" Then
                        sSql = ""
                        sSql += "UPDATE " + rsTableNM(i)
                        sSql += "   SET regno = '" + rsRegNo_chg + "', fkocs = CASE WHEN owngbn = 'L' THEN fkocs ELSE SUBSTR(fkocs, 1, 2) || '" + rsRegNo_chg + "' || SUBSTR(fkocs, 11) END"
                        sSql += " WHERE tnsjubsuno IN ('" + rsBcNos.Replace(",", "','") + "')"
                    Else
                        sSql = ""
                        sSql += "UPDATE " + rsTableNM(i)
                        sSql += "   SET regno = '" + rsRegNo_chg + "'"

                        If rsTableNM(i).ToLower = "lj010m" Or rsTableNM(i).ToLower = "lj010h" Then
                            sSql += ", patnm = '" + rsPatNm + "'"
                        ElseIf rsTableNM(i).ToLower = "lj011m" Or rsTableNM(i).ToLower = "lj011h" Then
                            sSql += ", fkocs = CASE WHEN owngbn = 'L' THEN fkocs ELSE SUBSTR(fkocs, 1, 2) || '" + rsRegNo_chg + "' || SUBSTR(fkocs, 11) END"
                        End If

                        sSql += " WHERE bcno IN ('" + rsBcNos.Replace(",", "','") + "')"

                    End If

                    al_Sql.Add(sSql)
                Next

                sSql = ""
                sSql += "INSERT INTO lrc10m (regdt, regid, bfregno, regno, ordsdt, ordedt, editid, editip, editdt)"
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

        Private m_dbCn As oracleConnection
        Private m_dbTran As oracleTransaction

        Private m_dt_rst As DataTable
        Private m_al_ParentCd As ArrayList
        Private m_s_CfmNm As String = ""
        Private m_s_CfmSign As String = ""

        Public Sub New()
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Public Sub New(ByVal r_dbCn As oracleConnection, ByVal r_dbTran As oracleTransaction)
            m_dbCn = r_dbCn
            m_dbTran = r_dbTran
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub
       
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



        Private Function fnImgFile_Get(ByVal rsFileNm As String) As Byte()
            Dim sFn As String = "Public Function fnImgFile_Get(string) As Byte()"

            Try
                Dim fs As IO.FileStream = New IO.FileStream(rsFileNm, IO.FileMode.Open, IO.FileAccess.Read)
                Dim br As IO.BinaryReader = New IO.BinaryReader(fs)

                Dim a_btReturn() As Byte = br.ReadBytes(CType(fs.Length, Integer))

                br.Close()
                fs.Close()

                Return a_btReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnReg_IMAGE(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal r_al_File As ArrayList) As Boolean

            Dim sFn As String = "Public Function fnReg_IMAGE(string) As String"
            Dim dbCmd As New OracleCommand

            Dim sErrVal As String = ""

            Try

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text
                End With

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                sSql = ""
                sSql += "DELETE lrs13m"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"


                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd

                    .ExecuteNonQuery()
                End With

                For ix As Integer = 0 To r_al_File.Count - 1

                    Dim btFile As Byte() = fnImgFile_Get(r_al_File(ix).ToString)


                    sSql = ""
                    sSql += "INSERT INTO lrs13m (  bcno,  testcd,   rstno,  filenm, filelen )"
                    sSql += "            VALUES ( :bcno,  :testcd, :rstno, :filenm, :filelen )"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2, rsBcNo.Length).Value = rsBcNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2, rsTestCd.Length).Value = rsTestCd
                        .Parameters.Add("rstno", OracleDbType.Int32, (ix + 1).ToString.Length).Value = (ix + 1).ToString
                        .Parameters.Add("filenm", OracleDbType.Varchar2, r_al_File(ix).ToString.Length).Value = r_al_File(ix).ToString
                        .Parameters.Add("filelen", OracleDbType.Int64).Value = btFile.Length

                        .ExecuteNonQuery()
                    End With


                    sSql = ""
                    sSql += "UPDATE lrs13m SET filebin = :filebin"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND rstno  = :rstno"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("filebin", OracleDbType.Blob, btFile.Length).Value = btFile
                        .Parameters.Add("bcno", OracleDbType.Varchar2, rsBcNo.Length).Value = rsBcNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2, rsTestCd.Length).Value = rsTestCd
                        .Parameters.Add("rstno", OracleDbType.Int32, 2).Value = (ix + 1).ToString

                        iRet += .ExecuteNonQuery()
                    End With
                Next


                If iRet = r_al_File.Count Then
                    For ix As Integer = 0 To r_al_File.Count - 1
                        If IO.File.Exists("C:\ACK\LIS\" + r_al_File(ix).ToString) Then
                            IO.File.Delete("C:\ACK\LIS\" + r_al_File(ix).ToString)
                        End If
                    Next

                    m_dbTran.Commit()
                    Return True
                Else
                    m_dbTran.Rollback()
                    Return False
                End If

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

        Public Function fnGet_ImgFile_List(ByVal dateS As String, ByVal dateE As String, ByVal gbnTk As Boolean) As DataTable
            Dim sFn As String = "fnGet_ImgFile_List(ByVal dateS As String, ByVal dateE As String, ByVal gbn As String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList


                sSql += " SELECT DISTINCT " + vbCrLf
                sSql += "        fn_ack_get_bcno_full(j.bcno) bcno, f.tnmd, j.regno, " + vbCrLf
                sSql += "        j.patnm patnm, r.testcd, " + vbCrLf
                sSql += "        NVL2(s13.bcno, 'O', 'X') imgchk " + vbCrLf
                sSql += "   FROM lf060m f, lj010m j, lrs13m s13, " + vbCrLf
                sSql += "        lf310m f3, " + vbCrLf
                sSql += "        (SELECT bcno, tkdt, testcd, spccd, wkymd, fnid " + vbCrLf
                sSql += "           FROM lr010m " + vbCrLf

                If gbnTk Then
                    sSql += "     WHERE tkdt >= :dates " + vbCrLf
                    sSql += "       AND tkdt <= :datee || '235959' " + vbCrLf
                Else
                    sSql += "     WHERE rstdt >= :dates " + vbCrLf
                    sSql += "       AND rstdt <= :datee || '235959' " + vbCrLf
                End If

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, dateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, dateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dateE))

                sSql += "          AND rstflg = '3' " + vbCrLf
                sSql += "         UNION " + vbCrLf
                sSql += "         SELECT bcno, tkdt, testcd, spccd, wkymd, fnid " + vbCrLf
                sSql += "           FROM lm010m " + vbCrLf

                If gbnTk Then
                    sSql += "     WHERE tkdt >= :dates " + vbCrLf
                    sSql += "       AND tkdt <= :datee || '235959' " + vbCrLf
                Else
                    sSql += "     WHERE rstdt >= :dates " + vbCrLf
                    sSql += "       AND rstdt <= :datee || '235959' " + vbCrLf
                End If

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, dateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, dateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dateE))

                sSql += "          AND rstflg = '3') r" + vbCrLf
                sSql += "           , lrs10m s10 " + vbCrLf
                sSql += "     WHERE j.bcno = r.bcno " + vbCrLf
                sSql += "       AND f.testcd = r.testcd " + vbCrLf
                sSql += "       AND f.spccd = r.spccd " + vbCrLf
                sSql += "       AND f.usdt  <= r.tkdt " + vbCrLf
                sSql += "       AND f.uedt  >= r.tkdt " + vbCrLf
                sSql += "       AND f.testcd = f3.testcd " + vbCrLf
                sSql += "       AND r.bcno = s10.bcno " + vbCrLf
                sSql += "       AND f.tcdgbn in ('P', 'S') " + vbCrLf
                sSql += "       AND NVL(f.ctgbn, '0') = '1' " + vbCrLf
                sSql += "       AND NVL(f.signrptyn, '0') = '1' " + vbCrLf
                sSql += "       AND j.spcflg = '4' " + vbCrLf
                sSql += "       AND NVL(r.wkymd, ' ') <> ' ' " + vbCrLf
                sSql += "       AND j.bcno = s13.bcno(+) " + vbCrLf
                sSql += "  ORDER BY bcno " + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try

        End Function

        Public Function fnGet_File_Image_count(ByVal rsBcno As String, ByVal rsTestcd As String) As DataTable

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Try

                Dim sSql As String = ""

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text
                End With

                sSql = ""
                sSql += "SELECT BCNO,FILENM, RSTNO"
                sSql += "  FROM lrs13m"
                sSql += " WHERE bcno = :bcno"
                sSql += "   and testcd = :testcd"
                sSql += " order by rstno"

                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestcd
                End With

                dt.Reset()
                dbDa.Fill(dt)

                m_dbTran.Commit()

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))


            Finally

            End Try
        End Function


        Public Function fnGet_File_Image(ByVal rsBcno As String, ByVal rsRstno As String, ByVal rbCn As Boolean, ByVal rsTestcd As String) As Byte()

            Dim dbCmd As New OracleCommand

            Try

                Dim sSql As String = ""

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text
                End With

                sSql = ""
                sSql += "SELECT FILELEN, FILEBIN"
                sSql += "  FROM lrs13m"
                sSql += " WHERE bcno = :bcno"
                sSql += "   AND rstno = :rstno"
                sSql += "   AND testcd = :testcd"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .Parameters.Add("rstno", OracleDbType.Varchar2).Value = rsRstno
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestcd

                End With

                Dim a_btReturn() As Byte


                Dim dbDr As OracleDataReader = dbCmd.ExecuteReader(CommandBehavior.SequentialAccess)

                Do While dbDr.Read()

                    Dim iStartIndex As Integer = 0
                    Dim lngReturn As Long = 0

                    Dim iBufferSize As Integer = 0

                    iBufferSize = Convert.ToInt32(dbDr.GetValue(0).ToString)

                    Dim a_btBuffer(iBufferSize - 1) As Byte
                    ReDim a_btBuffer(iBufferSize - 1)

                    iStartIndex = 0
                    lngReturn = dbDr.GetBytes(1, iStartIndex, a_btBuffer, 0, iBufferSize)

                    Do While lngReturn = iBufferSize
                        fnCopyToBytes(a_btBuffer, a_btReturn)


                        ReDim a_btBuffer(iBufferSize - 1)

                        iStartIndex += iBufferSize
                        lngReturn = dbDr.GetBytes(1, iStartIndex, a_btReturn, 0, iBufferSize)
                    Loop
                Loop

                dbDr.Close()
                Return a_btReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))


            Finally
                If rbCn = False Then
                    dbCmd.Dispose() : dbCmd = Nothing
                    If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                    m_dbCn.Dispose() : m_dbCn = Nothing
                End If

            End Try
        End Function
        Private Shared Function fnCopyToBytes(ByVal r_a_btFrom As Byte(), ByRef r_a_btTo As Byte()) As Boolean

            Try
                Dim iIndexDest As Integer = 0
                Dim iLength As Integer = 0

                If r_a_btTo Is Nothing Then
                    iIndexDest = 0
                Else
                    iIndexDest = r_a_btTo.Length
                End If

                iLength = r_a_btFrom.Length

                ReDim Preserve r_a_btTo(iIndexDest + iLength - 1)

                Array.ConstrainedCopy(r_a_btFrom, 0, r_a_btTo, iIndexDest, iLength)
            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))

            End Try

        End Function

        Public Function fnRegURL(ByVal rsBcno As String, ByVal rsSeq As String, ByVal rsSuccessYn As String, ByVal rsSendMsg As String, ByVal rsRtnMsg As String) As Boolean

            fnIns_LR080M(rsBcno, rsSuccessYn, rsSendMsg, rsRtnMsg, "")

        End Function

        Public Function fnReg_OCS(ByVal rsBcNo As String) As Boolean

            Dim sFn As String = "Public Function fnReg_OCS(string) As String"
            Dim dbCmd As New OracleCommand

            Dim sErrVal As String = ""

            Try

                fnEdit_LJ011M(rsBcNo)

                fnEdit_LJ010M(rsBcNo)

                '-- 감염정보
                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_ocs_rst_inf"

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
                Else
                    Return False
                End If

                '-- OCS에 결과 올리기
                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure

                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                        .CommandText = "pro_ack_exe_ocs_rst_m"
                    Else
                        .CommandText = "pro_ack_exe_ocs_rst"
                    End If

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

                If sErrVal.StartsWith("00") Then
                Else
                    m_dbTran.Rollback()
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
                    m_dbTran.Commit()
                    Return True
                Else
                    m_dbTran.Rollback()
                    Return False
                End If

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

        Public Function fnReg_AboRh(ByVal rsDate As String) As Boolean

            Dim sFn As String = "Public Function fnReg_OCS(string) As String"
            Dim sErrVal As String = ""

            Try

                Dim dt As New DataTable
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql = ""
                sSql += "SELECT a.patno, a.rslt1 abo, b.rslt1 rh, a.rstdt"
                sSql += "  FROM ("
                sSql += "        SELECT patno, rslt1, TO_CHAR(rsltdate, 'yyyymmddhh24miss') rstdt"
                sSql += "          FROM mdresult"
                sSql += "         WHERE rsltdate >= TO_DATE(:rstdt||'000000', 'yyyymmddhh24miss')"
                sSql += "           AND rsltdate <= TO_DATE(:rstdt||'235959', 'yyyymmddhh24miss')"
                sSql += "           AND examcode IN (SELECT A.tordcd FROM lf060m a, lf140m b WHERE a.testcd = b.testcd AND a.spccd = b.spccd AND b.bbgbn = '1')"
                sSql += "       ) a,"
                sSql += "       ("
                sSql += "        SELECT patno, rslt1, TO_CHAR(rsltdate, 'yyyymmddhh24miss') rstdt"
                sSql += "          FROM mdresult"
                sSql += "         WHERE rsltdate >= TO_DATE(:rstdt||'000000', 'yyyymmddhh24miss')"
                sSql += "           AND rsltdate <= TO_DATE(:rstdt||'235959', 'yyyymmddhh24miss')"
                sSql += "           AND examcode IN (SELECT A.tordcd FROM lf060m a, lf140m b WHERE a.testcd = b.testcd AND a.spccd = b.spccd AND b.bbgbn = '2')"
                sSql += "       ) b"
                sSql += " WHERE a.patno = b.patno"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsDate
                    .SelectCommand.Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsDate
                    .SelectCommand.Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsDate
                    .SelectCommand.Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsDate
                End With

                dt.Reset()
                dbDa.Fill(dt)

                For ix2 As Integer = 0 To dt.Rows.Count - 1
                    sSql = ""
                    sSql += "SELECT * FROM lr070m WHERE regno = :regno"

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    With dbDa
                        .SelectCommand.Parameters.Clear()
                        .SelectCommand.Parameters.Add("regno", OracleDbType.Varchar2).Value = dt.Rows(ix2).Item("patno").ToString
                    End With

                    Dim dt_s As New DataTable

                    dt_s.Reset()
                    dbDa.Fill(dt_s)

                    If dt_s.Rows.Count = 0 Then

                        With dbCmd
                            sSql = ""
                            sSql += "INSERT INTO lr070m(  regno,  abo,  rh,  rstdt,  editid,  editip, editdt )"
                            sSql += "            VALUES( :regno, :abo, :rh, :rstdt, :editid, :editip, fn_ack_sysdate)"

                            .CommandType = CommandType.Text
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("regno", OracleDbType.Varchar2).Value = dt.Rows(ix2).Item("patno").ToString
                            .Parameters.Add("abo", OracleDbType.Varchar2).Value = dt.Rows(ix2).Item("abo").ToString
                            .Parameters.Add("rh", OracleDbType.Varchar2).Value = dt.Rows(ix2).Item("rh").ToString
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt.Rows(ix2).Item("rstdt").ToString

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            .ExecuteNonQuery()

                        End With
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

        Public Function fnReg_err_m() As Boolean

            Dim sFn As String = "Public Function fnReg_err_m) As String"
            Dim sErrVal As String = ""

            Try

                Dim dt As New DataTable
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql = ""
                sSql += "SELECT b.bcno, SUBSTR(b.testcd, 1, 5) testcd, MAX(NVL(b.fndt, ' ')) fndt, MAX(NVL(b.fnid, ' ')) fnid"
                sSql += "  FROM lm010m a, lm010m b"
                sSql += " WHERE a.rstflg  = '3'"
                sSql += "   AND a.orgrst IS NOT NULL"
                sSql += "   AND a.rstdt  IS NULL"
                sSql += "   AND a.bcno    = b.bcno"
                sSql += "   AND a.testcd  = SUBSTR(b.testcd, 1, 5)"
                sSql += "   AND LENGTH(a.testcd) = 5"
                sSql += " GROUP BY b.bcno, SUBSTR(b.testcd, 1, 5)"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                End With

                dt.Reset()
                dbDa.Fill(dt)

                For ix As Integer = 0 To dt.Rows.Count - 1
                    sSql = ""
                    sSql += "UPDATE lm010m SET"
                    sSql += "       rstdt   = :rstdt,"
                    sSql += "       regid   = NVL(regid, :regid), regdt   = NVL(regdt, :regdt),"
                    sSql += "       mwid    = NVL(mwid,  :mwid),  mwdt    = NVL(mwdt,  :mwdt),"
                    sSql += "       fnid    = NVL(fnid,  :fnid),  fndt    = NVL(fndt,  :fndt),"
                    sSql += "       cfmnm   = '정보경',      cfmyn = CASE WHEN cfmyn = 'Y' THEN cfmyn ELSE 'N' END"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()

                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fndt").ToString()

                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fnid").ToString()
                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fndt").ToString()
                        .Parameters.Add("mwid", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fnid").ToString()
                        .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fndt").ToString()
                        .Parameters.Add("fnid", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fnid").ToString()
                        .Parameters.Add("fndt", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fndt").ToString()


                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("bcno").ToString()
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("testcd").ToString()

                        iRet = .ExecuteNonQuery()
                    End With


                    fnEdit_LJ011M(dt.Rows(ix).Item("bcno").ToString())

                    ''-- OCS에 결과 올리기
                    'With dbCmd
                    '    .Connection = m_dbCn
                    '    .Transaction = m_dbTran
                    '    .CommandType = CommandType.StoredProcedure

                    '    If PRG_CONST.BCCLS_MicorBio.Contains(dt.Rows(ix).Item("bcno").ToString()) Then
                    '        .CommandText = "pro_ack_exe_ocs_rst_m"
                    '    Else
                    '        .CommandText = "pro_ack_exe_ocs_rst"
                    '    End If

                    '    .Parameters.Clear()
                    '    .Parameters.Add("rs_bcno",  OracleDbType.Varchar2).Value = dt.Rows(ix).Item("bcno").ToString()
                    '    .Parameters.Add("rs_editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    '    .Parameters.Add("rs_editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    '    .Parameters.Add("rs_errmsg",  OracleDbType.Varchar2, 4000)
                    '    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    '    .Parameters("rs_errmsg").Value = sErrVal

                    '    .ExecuteNonQuery()

                    '    sErrVal = .Parameters(3).Value.ToString
                    'End With

                    'If sErrVal.StartsWith("00") Then
                    'Else
                    '    m_dbTran.Rollback()
                    '    Return False
                    'End If

                    '-- OCS에 결과 올리기
                    With dbCmd
                        .Connection = m_dbCn
                        .Transaction = m_dbTran
                        .CommandType = CommandType.StoredProcedure

                        .CommandText = "pro_ack_exe_ocs_rstflg"

                        .Parameters.Clear()
                        .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("bcno").ToString()
                        .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = dt.Rows(ix).Item("fnid").ToString()
                        .Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                        .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                        .Parameters("rs_errmsg").Value = sErrVal

                        .ExecuteNonQuery()

                        sErrVal = .Parameters(3).Value.ToString
                    End With

                    If sErrVal.StartsWith("00") Or sErrVal.IndexOf("no data") > 0 Then
                    Else
                        m_dbTran.Rollback()
                        Return False
                    End If

                    If ix = 0 Then Exit For
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

        Public Function fnReg_Change_CollAndTkAndRst_date(ByVal rsBcNo As String, ByVal rsRstDate As String) As Boolean

            Dim sFn As String = "Public Function fnReg_OCS(string) As String"
            Dim dbCmd As New OracleCommand

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Try

                sSql = ""
                sSql += " UPDATE lj011m SET colldt = :rstdt, tkdt = :rstdt, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                sSql += "  WHERE bcno = :bcno"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With

                If iRet = 0 Then
                    m_dbTran.Rollback()
                    Return False
                End If

                sSql = ""
                sSql += " UPDATE lr010m SET tkdt = :rstdt, "
                sSql += "        regdt  = DECODE(NVL(regid, ' '),  ' ',  NULL, :rstdt),"
                sSql += "        mwdt   = DECODE(NVL(mwid, ' '),   ' ',  NULL, :rstdt),"
                sSql += "        fndt   = DECODE(NVL(fnid, ' '),   ' ',  NULL, :rstdt),"
                sSql += "        rstdt  = DECODE(NVL(rstflg, '0'), '0',  NULL, :rstdt),"
                sSql += "        editdt = fn_ack_sysdate,"
                sSql += "        editid = :editid,"
                sSql += "        editip = :editip"
                sSql += "  where bcno = :bcno"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsRstDate

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

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
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        Private Function fnEdit_LR_Rerun(ByVal rsSrvDt As String, ByVal roRstInfo As ArrayList, ByRef roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR_Rerun( String, String, ArrayList, ByRef ArrayList) As Boolean"
            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                roBcNos.Clear()
                For intIdx As Integer = 0 To roRstInfo.Count - 1

                    If roBcNos.Contains(CType(roRstInfo.Item(intIdx), RERUN_INFO).msBcNo) = False Then
                        roBcNos.Add(CType(roRstInfo.Item(intIdx), RERUN_INFO).msBcNo)
                    End If

                    sSql = ""
                    sSql += "INSERT INTO lr011m"
                    sSql += "       ("
                    sSql += "        bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "        bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, rstflg, rerunflg, tclscd,"
                    sSql += "        eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, sysdt, editdt, editid, editip, seq"
                    sSql += "       ) "
                    sSql += "SELECT bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "       bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, rstflg, rerunflg, tclscd,"
                    sSql += "       eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, fn_ack_sysdate, editdt, editid, editip, sq_lr011m.nextval"
                    sSql += "  FROM lr010m"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND (NVL(regid, ' ') <> ' ' OR NVL(mwid, ' ') <> ' ' OR NVL(fnid, ' ') <> ' ')"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(intIdx), RERUN_INFO).msBcNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(intIdx), RERUN_INFO).msTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    sSql = ""
                    sSql += "UPDATE lr010m"
                    sSql += "   SET rstflg = '0', rstdt = :rstdt, orgrst = NULL, viewrst = NULL, eqflag = NULL,"
                    sSql += "       regdt  = :rstdt, regid = :regid,"
                    sSql += "       mwdt   = NULL, mwid = NULL,"
                    sSql += "       fndt   = NULL, fnid = NULL, rerunflg = '1',"
                    sSql += "       editdt = fn_ack_sysdate,"
                    sSql += "       editid = :editid,"
                    sSql += "       editip = :editip"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND NVL(cfmsign, ' ') = ' '"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsSrvDt
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = rsSrvDt
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID

                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(intIdx), RERUN_INFO).msBcNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(intIdx), RERUN_INFO).msTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    If iRet = 0 Then Return False
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        Public Function fnReg_rerun(ByVal roRstInfo As ArrayList, ByVal roCmtInfo As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnReg_rerun(String, ArrayList, ArrayList) As Boolean"
            Dim dbCmd As New OracleCommand

            Try
                Dim intRet As Integer = 0
                Dim sSql As String = ""

                Dim alBcNos As New ArrayList
                Dim sSrvDt As String = fnGet_Server_DateTime()

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                If fnEdit_LR_Rerun(sSrvDt, roRstInfo, alBcNos) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                '<< 재검 코로나 ct값 delete 
                fnDel_LRS18M(alBcNos.Item(0).ToString)

                If fnEdit_LR040M(roCmtInfo) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                If fnEdit_LJ_Clear(alBcNos, sSrvDt) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                If fnEdit_lnc_Clear(alBcNos) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                For ix As Integer = 0 To alBcNos.Count - 1
                    If fnEdit_EXE_OCS_RST(alBcNos.Item(ix).ToString) = False Then
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

        Public Function RegNcov(ByVal rsPrtno As String, ByVal rsTestGbn As String, ByVal Orgrst As String, ByVal rsUsrid As String, ByVal rsRstflg As String) As Integer
            Dim sFn As String = "Private Function RegNcov(string, string, string) As Integer"

            Try
                Fn.log(rsPrtno + "|" + rsTestGbn + "|" + Orgrst + "|" + rsUsrid)

                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable


                sSql = ""
                sSql += " SELECT A.BCNO, A.TESTCD, A.SPCCD, A.REGNO, B.GBN, B.RST   "
                sSql += "   FROM LR010M A                                           "
                sSql += "   LEFT OUTER JOIN LRS18M B                                "
                sSql += "                ON A.BCNO   = B.BCNO                       "
                sSql += "               AND A.TESTCD = B.TESTCD                     "
                sSql += "               AND A.SPCCD  = B.SPCCD                      "
                sSql += "               AND B.GBN    = :gbn                         "
                sSql += "  WHERE A.BCNO = FN_ACK_GET_BCNO_NORMAL(:prtno)            "

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    .SelectCommand.Parameters.Add("prtno", OracleDbType.Varchar2).Value = rsPrtno
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count <= 0 Then Return 0

                Dim iRet As Integer = 0

                Dim Gbn As String = dt.Rows(0).Item("gbn").ToString
                Dim Rst As String = dt.Rows(0).Item("rst").ToString
                Dim Bcno As String = dt.Rows(0).Item("bcno").ToString

                '<< 결과가 존재할때
                If Gbn <> "" Then

                    sSql = ""
                    sSql += "SELECT MAX(NVL(SEQ, 0)) + 1 AS SEQ "
                    sSql += "  FROM LRS18H "
                    sSql += " WHERE BCNO = :BCNO "
                    sSql += "   AND GBN  = :GBN "

                    dbCmd.CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    With dbDa
                        .SelectCommand.Parameters.Clear()
                        .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .SelectCommand.Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    End With

                    dt.Reset()
                    dbDa.Fill(dt)

                    Dim SEQ As String = dt.Rows(0).Item("seq").ToString

                    sSql = ""
                    sSql += " INSERT INTO LRS18H "
                    sSql += "             SELECT A.BCNO, A.TESTCD, A.SPCCD, A.REGNO, A.RST, A.RSTDT, A.RSTID, A.REGDT, A.REGID, A.GBN , :SEQ , FN_ACK_SYSDATE(), :USRID , A.RSTFLG "
                    sSql += "               FROM LRS18M A"
                    sSql += "              WHERE A.BCNO = :BCNO "
                    sSql += "                AND A.GBN  = :GBN "

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("seq", OracleDbType.Varchar2).Value = SEQ
                        .Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    End With

                    iRet += dbCmd.ExecuteNonQuery()


                    Fn.log("U")

                    sSql = ""
                    sSql += " UPDATE LRS18M "
                    sSql += "    SET RST    = :RST, "
                    sSql += "        RSTDT  = FN_ACK_SYSDATE() ,"
                    sSql += "        RSTID  = :RSTID ,"
                    sSql += "        RSTFLG = :RSTFLG "
                    sSql += "  WHERE BCNO   = :BCNO "
                    sSql += "    AND GBN    = :GBN"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("rst", OracleDbType.Varchar2).Value = Orgrst
                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = rsRstflg
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    End With

                    iRet += dbCmd.ExecuteNonQuery()

                Else '<< 결과가 존재하지않을때

                    sSql = ""
                    sSql += " INSERT INTO LRS18M  "
                    sSql += "             ( BCNO,  TESTCD,  SPCCD,  REGNO,  RST,      RSTDT,        RSTID,       REGDT,         REGID,   GBN,   RSTFLG )  "
                    sSql += "      VALUES (:BCNO, :TESTCD, :SPCCD, :REGNO, :RST, fn_ack_sysdate(), :RSTID,  fn_ack_sysdate(),  :REGID,  :GBN,  :RSTFLG )  "

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt.Rows(0).Item("testcd").ToString
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = dt.Rows(0).Item("spccd").ToString
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = dt.Rows(0).Item("regno").ToString
                        .Parameters.Add("rst", OracleDbType.Varchar2).Value = Orgrst
                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = rsRstflg
                    End With

                    iRet += dbCmd.ExecuteNonQuery()

                End If

                m_dbTran.Commit()

                Return 1
            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing
            End Try

        End Function

        Public Function fnExe_CVR(ByVal rsBcno As String, ByVal rsRegno As String, ByVal CVR_Buf As ArrayList, ByVal Usrid As String, ByVal Usrnm As String) As String

            Dim sFn As String = "Public Function fnEdit_LR040M(object) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim dt2 As New DataTable
                Dim iRet As Integer = 0


                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To CVR_Buf.Count - 1
                    Dim Cvr As LIS_CVR_INFO = CType(CVR_Buf(ix), LIS_CVR_INFO)

                    sSql = ""
                    sSql += " DELETE LIS_CVR_INFO "
                    sSql += "  WHERE BARCODENO = :BCNO"
                    sSql += "    AND EXECPRCPUNIQNO = :FKOCS "
                    sSql += "    AND DTLTESTCD = :TESTCD "

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    With dbCmd

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                        .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = Cvr.Fkocs
                        .Parameters.Add("dtltestcd", OracleDbType.Varchar2).Value = Cvr.Testcd

                        .ExecuteNonQuery()

                    End With

                    sSql = ""
                    sSql += " INSERT INTO LIS_CVR_INFO "
                    sSql += "             ( PRCPDD,  EXECPRCPUNIQNO,  PID,  BARCODENO,  EXAMNM,  USERNM,  DTLTESTCD,  RST,  RSTDT,  RSTID,  FSTRGSTRID,  LASTUPDTRID,  RSTUNIT ) VALUES "
                    sSql += "             (:PRCPDD, :EXECPRCPUNIQNO, :PID, :BARCODENO, :EXAMNM, :USERNM, :DTLTESTCD, :RST, :RSTDT, :RSTID, :FSTRGSTRID, :LASTUPDTRID, :RSTUNIT )   "
                    'sSql += "             ( PRCPDD,  EXECPRCPUNIQNO,  PID,  BARCODENO,  EXAMNM,  USERNM,  DTLTESTCD,  RST,  RSTDT,  RSTID,  FSTRGSTRID,  LASTUPDTRID ) VALUES "
                    'sSql += "             (:PRCPDD, :EXECPRCPUNIQNO, :PID, :BARCODENO, :EXAMNM, :USERNM, :DTLTESTCD, :RST, :RSTDT, :RSTID, :FSTRGSTRID, :LASTUPDTRID )   "
                    '                      처방일자      처방키    등록번호  검체번호   검사명   등록자    검사코드  결과  보고일자 보고자   등록자ID      등록자ID    결과단위
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    With dbCmd

                        .Parameters.Clear()
                        .Parameters.Add("PRCPDD", OracleDbType.Varchar2).Value = Cvr.Orddt
                        .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = Cvr.Fkocs
                        .Parameters.Add("pid", OracleDbType.Varchar2).Value = rsRegno
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                        .Parameters.Add("tnmd", OracleDbType.Varchar2).Value = Cvr.Tnmd
                        .Parameters.Add("usernm", OracleDbType.Varchar2).Value = Usrnm
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = Cvr.Testcd
                        .Parameters.Add("rst", OracleDbType.Varchar2).Value = Cvr.Rst
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = Cvr.Rstdt
                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = Cvr.Rstid
                        .Parameters.Add("fstid", OracleDbType.Varchar2).Value = Usrid
                        .Parameters.Add("lastid", OracleDbType.Varchar2).Value = Usrid
                        .Parameters.Add("rstunit", OracleDbType.Varchar2).Value = Cvr.RstUnit

                        iRet += .ExecuteNonQuery()

                    End With

                Next

                m_dbTran.Commit()

                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                MsgBox(ex.Message)
                Return "오류"
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing
            End Try

        End Function

        Public Function fnCancel_CVR(ByVal rsBcno As String, ByVal CVR_Buf As ArrayList) As String

            Dim sFn As String = "Public Function fnCancel_CVR(String, ArrayList) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim dt2 As New DataTable
                Dim iRet As Integer = 0


                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To CVR_Buf.Count - 1

                    Dim Cvr As LIS_CVR_INFO = CType(CVR_Buf(ix), LIS_CVR_INFO)

                    sSql = ""
                    sSql += " DELETE LIS_CVR_INFO "
                    sSql += "  WHERE BARCODENO = :BCNO"
                    sSql += "    AND EXECPRCPUNIQNO = :FKOCS "
                    sSql += "    AND DTLTESTCD = :TESTCD "

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    With dbCmd

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                        .Parameters.Add("fkocs", OracleDbType.Varchar2).Value = Cvr.Fkocs
                        .Parameters.Add("dtltestcd", OracleDbType.Varchar2).Value = Cvr.Testcd

                        .ExecuteNonQuery()

                    End With

                Next

                m_dbTran.Commit()

                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                Return "오류"
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing
            End Try

        End Function

        Public Function fnCancel_NCOV(ByVal Cancel_Arry As ArrayList, ByVal sRegno As String) As String

            Dim sFn As String = "Public Function fnCancel_NCOV(ArrayList, String) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To Cancel_Arry.Count - 1

                    Dim Ncov As NCOV_Cancel = CType(Cancel_Arry(ix), NCOV_Cancel)
                    Dim DtGbn As String = Ncov.sDtgbn

                    If DtGbn = "H" Then
                        sSql = ""
                        sSql += " DELETE LRS18H "
                        sSql += "  WHERE BCNO   = :BCNO"
                        sSql += "    AND TESTCD = :TESTCD "
                        sSql += "    AND REGNO  = :REGNO "
                        sSql += "    AND GBN    = :GBN "
                        sSql += "    AND SEQ    = :SEQ "
                    Else
                        sSql = ""
                        sSql += " DELETE LRS18M "
                        sSql += "  WHERE BCNO   = :BCNO"
                        sSql += "    AND TESTCD = :TESTCD "
                        sSql += "    AND REGNO  = :REGNO "
                        sSql += "    AND GBN    = :GBN "
                    End If



                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    With dbCmd

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = Ncov.sBcno
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = Ncov.sTestcd
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = sRegno
                        .Parameters.Add("gbn", OracleDbType.Varchar2).Value = Ncov.sGbn

                        If DtGbn = "H" Then
                            .Parameters.Add("seq", OracleDbType.Varchar2).Value = Ncov.sSeq
                        End If


                        .ExecuteNonQuery()

                    End With

                Next

                m_dbTran.Commit()

                Return ""

            Catch ex As Exception
                m_dbTran.Rollback()
                Return "오류" + ex.Message
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_dbCn.State = ConnectionState.Open Then m_dbCn.Close()
                m_dbCn.Dispose() : m_dbCn = Nothing
            End Try

        End Function



        Private Function fnEdit_RST_Clear(ByVal roRstInfo As ArrayList, ByRef roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_RST_Clear(ArrayList, ArrayList) As Boolean"
            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""
                Dim sTableNm As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                roBcNos.Clear()

                For ix As Integer = 0 To roRstInfo.Count - 1

                    If PRG_CONST.BCCLS_MicorBio.Contains(CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO.Substring(8, 2)) Then
                        sTableNm = "lm010"
                    Else
                        sTableNm = "lr010"
                    End If

                    If roBcNos.Contains(CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO) = False Then
                        roBcNos.Add(CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO)
                    End If

                    sSql = ""
                    sSql += "INSERT INTO " + sTableNm + "h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.*"
                    sSql += "  FROM " + sTableNm + "m r"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()

                    End With

                    sSql = ""
                    sSql += "UPDATE " + sTableNm + "m"
                    sSql += " SET orgrst = NULL, viewrst = NULL, rstcmt = NULL, rerunflg = NULL,"
                    sSql += "       regid = NULL, regdt = NULL, mwid = NULL, mwdt = NULL, fnid = NULL, fndt = NULL,"
                    sSql += "       rstflg = NULL, rstdt = NULL,"
                    sSql += "       hlmark = NULL, panicmark = NULL, deltamark = NULL, criticalmark = NULL, alertmark = NULL,"
                    sSql += "       bfbcno = '', bffndt = NULL, bforgrst = NULL, bfviewrst = NULL, eqflag = NULL,"
                    sSql += "       editdt = fn_ack_sysdate,"
                    sSql += "       editid = :editid,"
                    sSql += "       editip = :editip"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND NVL(cfmsign, ' ') = ' '"

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

                    '-- 장비 데이타 삭제
                    sSql = ""
                    sSql += "DELETE lnc10m WHERE bcno = :bcno"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO

                        .ExecuteNonQuery()
                    End With

                    '-- 장비 데이타 삭제
                    sSql = ""
                    sSql += "DELETE lnc20m WHERE bcno = :bcno"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO

                        .ExecuteNonQuery()
                    End With

                    '-- 특수보고서 삭제
                    sSql = ""
                    sSql += "INSERT INTO lrs10h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, bcno, testcd, rstflg, rsttxt, rstdt, rstid, migymd, editdt, editid, editip"
                    sSql += "  FROM lrs10m"
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
                    sSql += "DELETE lrs10m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    sSql = ""
                    sSql += "DELETE lrs11m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    sSql = ""
                    sSql += "DELETE lrs12m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    sSql = ""
                    sSql += "DELETE lrs13m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    '<2020-02-11 jjh 특수보고서 결과
                    sSql = ""
                    sSql += "DELETE lrs17m WHERE bcno = :bcno AND testcd = :testcd"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                        iRet = .ExecuteNonQuery()
                    End With
                    '>


                    If PRG_CONST.BCCLS_MicorBio.Contains(CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO.Substring(8, 2)) Then
                        sSql = ""
                        sSql += "DELETE lm012m WHERE bcno = :bcno AND testcd = :testcd"

                        With dbCmd
                            .CommandType = CommandType.Text
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                            iRet = .ExecuteNonQuery()
                        End With

                        sSql = ""
                        sSql += "DELETE lm013m WHERE bcno = :bcno AND testcd = :testcd"

                        With dbCmd
                            .CommandType = CommandType.Text
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mBCNO
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo.Item(ix), ResultInfo_Test).mTestCd

                            iRet = .ExecuteNonQuery()
                        End With
                    End If
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_lnc_Clear(ByVal roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_RST_Clear(ArrayList, ArrayList) As Boolean"
            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To roBcNos.Count - 1

                    '-- 장비 데이타 삭제
                    sSql = ""
                    sSql += "DELETE lnc10m WHERE bcno = :bcno"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roBcNos.Item(ix).ToString

                        .ExecuteNonQuery()
                    End With

                    '-- 장비 데이타 삭제
                    sSql = ""
                    sSql += "DELETE lnc20m WHERE bcno = :bcno"

                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = roBcNos.Item(ix).ToString

                        .ExecuteNonQuery()
                    End With

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

                    If fnEdit_LR_Battery(roBcNos.Item(intIdx).ToString, "", rsSrvDt) = False Then
                        Return False
                    End If

                    If fnEdit_LJ011M(roBcNos.Item(intIdx).ToString) < 1 Then
                        Return False
                    End If

                    If fnEdit_LJ010M(roBcNos.Item(intIdx).ToString) < 1 Then
                        Return False
                    End If


                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR020M_Clear(ByVal roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR020M_Clear(ArrayList) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                Dim intRet As Integer = 0
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To roBcNos.Count - 1

                    sSql = "DELETE lr020m WHERE bcno = :bcno"
                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno1", OracleDbType.Varchar2).Value = roBcNos.Item(ix).ToString

                        intRet = .ExecuteNonQuery()
                    End With

                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR040M_Clear(ByVal roBcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR040M_Clear(ArrayList) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim sSql As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To roBcNos.Count - 1

                    sSql = "DELETE lr040m WHERE bcno = :bcno"
                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno1", OracleDbType.Varchar2).Value = roBcNos.Item(ix).ToString

                        iRet = .ExecuteNonQuery()
                    End With
                Next

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_FGO90(ByVal rsDateS As String, ByVal rsDateE As String) As DataTable
            Dim sFn As String = "Private Function RegNcov(string, string, string) As Integer"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += " SELECT A.BCNO, D.REGNO, D.PATNM, fn_ack_get_bcno_prt(a.bcno) prtno,  C.TNMD, C.TESTCD, C.SPCCD, "
                sSql += "        MAX(CASE WHEN B.GBN = 'E' THEN B.RST END) E,"
                sSql += "        MAX(CASE WHEN B.GBN = 'R' THEN B.RST END) R "
                sSql += "   FROM LJ011M A"
                sSql += "        LEFT OUTER JOIN LRS18M B ON A.BCNO = B.BCNO AND A.TCLSCD = B.TESTCD AND A.SPCCD = B.SPCCD"
                sSql += "        , LF060M C, LJ010M D"
                sSql += "  WHERE A.TKDT BETWEEN :dateS || '000000' AND :dateE || '235959'"
                sSql += "    AND A.TCLSCD = C.TESTCD"
                sSql += "    AND A.SPCCD = C.SPCCD"
                'sSql += "    AND A.TCLSCD IN ('LG119','LG120','LG121','LG122','LG123')"
                sSql += "    AND A.TCLSCD IN ('LG117', 'LG118')"
                sSql += "    AND A.TKDT >= C.USDT"
                sSql += "    AND A.TKDT <= C.UEDT"
                sSql += "    AND A.BCNO  = D.BCNO "
                sSql += "  GROUP BY A.BCNO, D.REGNO, D.PATNM, C.TNMD, C.TESTCD, C.SPCCD, A.TKDT"
                sSql += "  ORDER BY A.TKDT"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Function fnRsg_RstClear(ByVal rsUsrId As String, ByVal roRstInfo As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnRsg_RstClear(String, ArrayList) As Boolean"

            Try
                Dim dbCmd As New OracleCommand

                Dim iRet As Integer = 0
                Dim alBcNos As New ArrayList
                Dim sSrvDt As String = fnGet_Server_DateTime()

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

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

                If fnEdit_LR040M_Clear(alBcNos) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                'If fnEdit_LR020M_Clear(alBcNos) = False Then
                '    m_dbTran.Rollback()
                '    Return False
                'End If

                If fnEdit_lnc_Clear(alBcNos) = False Then
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

        ' 결과등록 
        Public Function fnReg(ByVal rsUsrId As String, ByVal roRstInfo As ArrayList, Optional ByVal roCmtInfo As ArrayList = Nothing, _
                                    Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "") As Boolean
            Dim sFn As String = "Public Function fnReg(String, ArrayList, [ArrayList], [String], [String]) As Boolean"

            Try
                m_s_CfmNm = rsCfmNm
                m_s_CfmSign = rsCfmSign

                Dim blnRet As Boolean = False
                Dim alBcNos As New ArrayList

                If fnEdit_LR010M(roRstInfo, rsUsrId, alBcNos) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                If roCmtInfo Is Nothing Then
                Else

                    ''' part slip별 소견일때 
                    If fnEdit_LR040M(roCmtInfo) = False Then  ''' 검체 part slip별 소견 
                        m_dbTran.Rollback()
                        Return False
                    End If

                End If

                For ix As Integer = 0 To alBcNos.Count - 1
                    ' ocs연동 저장  잠시 막음 수정해야함 정은 
                    If fnEdit_EXE_OCS_RST(alBcNos.Item(ix).ToString.Split("|"c)(0)) = False Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    'If (New RegFn()).fnEdit_Covid_SMS(alBcNos.Item(ix).ToString.Split("|"c)(0), m_dbCn, m_dbTran) = False Then
                    '    m_dbTran.Rollback()
                    '    Return False
                    'End If
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

        ' 현장검사 결과등록
        Public Function fnReg(ByVal rsRegNo As String, ByVal rsFkOcs As String, ByVal rsUsrId As String, ByVal r_al_RstInfo As ArrayList, _
                                    Optional ByVal rsCfmNm As String = "", Optional ByVal rsCfmSign As String = "") As Boolean

            Dim sFn As String = "Public Function fnReg( String, String, String, ArrayList, [String], [String]) As Boolean"

            m_s_CfmNm = rsCfmNm
            m_s_CfmSign = rsCfmSign

            Try
                Dim sBcNo As String = ""
                Dim sRet As String = ""
                Dim alBcNos As New ArrayList

                Dim dbCmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                With dbCmd
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_collect_take"

                    .Parameters.Clear()


                    .Parameters.Add(New OracleParameter("rs_regno", rsRegNo))
                    .Parameters.Add(New OracleParameter("rs_fkocs", rsFkOcs))
                    .Parameters.Add(New OracleParameter("rs_usrid", USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("rs_ip", USER_INFO.LOCALIP))

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = ""

                    .ExecuteNonQuery()

                    sRet = .Parameters(4).Value.ToString
                End With

                If sRet.StartsWith("00") Then sBcNo = sRet.Substring(2)

                For ix As Integer = 0 To r_al_RstInfo.Count - 1
                    If CType(r_al_RstInfo(ix), ResultInfo_Test).mBCNO = "" Then
                        CType(r_al_RstInfo(ix), ResultInfo_Test).mBCNO = sBcNo
                    End If
                Next

                If fnEdit_LR010M(r_al_RstInfo, rsUsrId, alBcNos) = False Then
                    m_dbTran.Rollback()
                    Return False
                End If

                For ix As Integer = 0 To alBcNos.Count - 1
                    If fnEdit_EXE_OCS_RST(alBcNos.Item(ix).ToString.Split("|"c)(0)) = False Then
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

                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                        .CommandText = "pro_ack_exe_ocs_rst_m"
                    Else
                        .CommandText = "pro_ack_exe_ocs_rst"
                    End If

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
                    Throw (New Exception(sErrVal.Substring(2)))
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
                    Throw (New Exception(sErrVal.Substring(2)))
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
                    .CommandText = "pro_ack_exe_ocs_rst_inf"

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


        Private Function fnEdit_LR010M(ByVal roRstInfo As ArrayList, ByVal rsUsrId As String, ByRef r_al_BcNos As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR010M(ArrayList, String) As Boolean"
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
                    sSql += "INSERT INTO lr011m"
                    sSql += "       ("
                    sSql += "        bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "        bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg, tclscd,"
                    sSql += "        eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, sysdt, editdt, editid, editip, seq"
                    sSql += "       ) "
                    sSql += "SELECT bcno, testcd, spccd, orgrst, viewrst, deltamark, panicmark, criticalmark, alertmark, hlmark,"
                    sSql += "       bfbcno, bffndt, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg, tclscd,"
                    sSql += "       eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, :sysdt, editdt, editid, editip, sq_lr011m.nextval"
                    sSql += "  FROM lr010m"
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

                        .Parameters.Add("sysdt", OracleDbType.Varchar2).Value = sSrvDt
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mBCNO
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mTestCd

                        .ExecuteNonQuery()
                    End With

                    Dim sRstFlg As String = CType(roRstInfo(ix), ResultInfo_Test).mRstFlg

                    'Update
                    sSql = ""
                    sSql += "UPDATE lr010m"
                    sSql += "   SET orgrst       = :orgrst,"
                    sSql += "       viewrst      = :viewrst,"
                    sSql += "       deltamark    = :deltamark,"
                    sSql += "       panicmark    = :panicmark,"
                    sSql += "       criticalmark = :criticalmark,"
                    sSql += "       alertmark    = :alertmark,"
                    sSql += "       hlmark       = :hlmark,"

                    Select Case sRstFlg
                        Case "1"
                            sSql += "       regid = :regid,"
                            sSql += "       regdt = :regdt,"
                        Case "2"
                            sSql += "       regid = NVL(regid, :regid),"
                            sSql += "       regdt = NVL(regdt, :regdt),"
                            sSql += "       mwid  = :mwid,"
                            sSql += "       mwdt  = :mwdt,"
                        Case "3"
                            sSql += "       regid   = NVL(regid, :regid),"
                            sSql += "       regdt   = NVL(regdt, :regdt),"
                            sSql += "       mwid    = NVL(mwid,  :mwid),"
                            sSql += "       mwdt    = NVL(mwdt,  :mwdt),"
                            sSql += "       fnid    = :fnid,"
                            sSql += "       fndt    = :fndt,"
                            sSql += "       cfmnm   = :cfmnm,"
                            'sSql += "       cfmsign = :cfmsign,"
                            'sSql += "       cfmyn   = 'Y',"
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
                    sSql += "       fregdt = CASE WHEN NVL(fregdt, ' ') = ' ' THEN :fregdt ELSE fregdt END,"
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
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = sSrvDt
                            Case "2"
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = sSrvDt
                            Case "3"
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("fnid", OracleDbType.Varchar2).Value = rsUsrId
                                .Parameters.Add("fndt", OracleDbType.Varchar2).Value = sSrvDt
                                .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mCfmNm.Trim
                                '.Parameters.Add("cfmsign",  OracleDbType.Varchar2).Value = CType(roRstInfo(ix), ResultInfo_Test).mCfmSign

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

                        .Parameters.Add("fregdt", OracleDbType.Varchar2).Value = sSrvDt

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

                        If fnEdit_LJ011M(alBcNos.Item(ix).ToString.Split("|"c)(0)) < 1 Then
                            Return False
                        End If

                        If fnEdit_LJ010M(alBcNos.Item(ix).ToString.Split("|"c)(0)) < 1 Then
                            Return False
                        End If

                        '감염정보 잠시 막음  수정해야함  정은 
                        If fnEdit_EXE_OCS_RST_INF(alBcNos.Item(ix).ToString.Split("|"c)(0)) = False Then
                            Return False
                        End If

                    Next
                End If

                r_al_BcNos = alBcNos

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

                Dim sTable As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, SUBSTR(r.testcd, 1, 5) testcd, r.spccd,"
                sSql += "       CASE WHEN NVL(f.fixrptusr, ' ') <> ' ' THEN f.fixrptusr"
                sSql += "            ELSE fn_ack_get_usr_name(f68.doctorid1)"
                sSql += "       END cfmnm, cfmsign"
                sSql += "  FROM " + sTable + " r, lf060m f, lf100m f68"
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
                    Dim sRstFlg_Max As String = dt_p.Rows(ix).Item("maxrstflg").ToString

                    If sRstFlg = "3" Then
                        sSql = ""
                        sSql += "UPDATE " + sTable + " SET"
                        sSql += "       rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :regid), regdt   = NVL(regdt, :regdt),"
                        sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt    = NVL(mwdt,  :mwdt),"
                        sSql += "       fnid   = NVL(fnid,  :fnid),  fndt    = :fndt,"
                        sSql += "       cfmnm  = :cfmnm,             cfmsign = :cfmsign, cfmyn = CASE WHEN cfmyn = 'Y' THEN cfmyn ELSE 'N' END,"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno   = :bcno"
                        sSql += "   AND testcd LIKE :testcd || '%'"
                        sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                        sSql += "        (testcd, spccd, '1') IN "
                        sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
                        sSql += "          WHERE r.bcno   = :bcno"
                        sSql += "            AND r.testcd LIKE :testcd || '%'"
                        sSql += "            AND r.testcd = f.testcd"
                        sSql += "            AND r.spccd  = f.spccd"
                        sSql += "            AND f.usdt  <= r.tkdt"
                        sSql += "            AND f.uedt  >  r.tkdt"
                        sSql += "            AND tcdgbn   = 'P'"
                        sSql += "        )"
                        sSql += "       )"
                        sSql += "   AND NVL(rstflg, ' ') <> '3'"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmnm").ToString().Trim
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmsign").ToString

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                        End With
                    Else
                        sSql = ""
                        Select Case sRstFlg
                            Case "1"
                                sSql = ""
                                sSql += "UPDATE " + sTable + " SET"
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
                                sSql += "        (testcd, spccd, '1') IN "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
                                sSql += "          WHERE r.bcno   = :bcno"
                                sSql += "            AND r.testcd LIKE :testcd || '%'"
                                sSql += "            AND r.testcd = f.testcd"
                                sSql += "            AND r.spccd  = f.spccd"
                                sSql += "            AND f.usdt  <= r.tkdt"
                                sSql += "            AND f.uedt  >  r.tkdt"
                                sSql += "            AND f.tcdgbn   = 'P'"
                                sSql += "        )"
                                sSql += "       )"


                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                End With

                            Case "2"
                                sSql = ""
                                sSql += "UPDATE " + sTable + " SET"
                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND testcd LIKE :testcd ||'%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '1') IN "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
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
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()
                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                                End With
                            Case "0"
                                If sRstFlg_Max = "3" Then
                                    sSql = ""
                                    sSql += "UPDATE " + sTable + " SET"
                                    sSql += "       rstflg = :rstflg,"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                    sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                    sSql += "       fnid   = NULL,               fndt  = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno   = :bcno"
                                    sSql += "   AND testcd LIKE :testcd ||'%'"
                                    sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                    sSql += "        (testcd, spccd, '1') IN "
                                    sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
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
                                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = "2"
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

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

                Dim sTable As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, r.tclscd, r.spccd,"
                sSql += "       CASE WHEN NVL(f.fixrptusr, ' ') <> ' ' THEN f.fixrptusr"
                sSql += "            ELSE fn_ack_get_usr_name(f68.doctorid1)"
                sSql += "       END cfmnm, '' cfmsign"
                sSql += "  FROM " + sTable + " r, lf060m f, lf100m f68, lf062m f62"
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
                    Dim sRstFlg_Max As String = dt_p.Rows(ix).Item("maxrstflg").ToString


                    If sRstFlg = "3" Then

                        sSql = ""
                        sSql += "UPDATE " + sTable + ""
                        sSql += "   SET rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :regid), regdt   = NVL(regdt, :regdt),"
                        sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt    = NVL(mwdt,  :mwdt),"
                        sSql += "       fnid   = NVL(fnid,  :fnid),  fndt    = NVL(fndt,  :fndt),"
                        sSql += "       cfmnm  = :cfmnm,             cfmsign = :cfmsign, cfmyn = 'Y',"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno      = :bcno"
                        sSql += "   AND tclscd    = :tclscd"
                        sSql += "   AND rstflg   <> '3'"
                        sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                        sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = '1')"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmnm").ToString().Trim
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("cfmsign").ToString()

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                            .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                        End With
                    Else
                        Select Case sRstFlg
                            Case "0"
                                If sRstFlg_Max = "3" Then
                                    sSql = ""
                                    sSql += "UPDATE " + sTable + ""
                                    sSql += "   SET rstflg = '1',"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                    sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                    sSql += "       fnid   = NULL,               fndt  = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno       = :bcno"
                                    sSql += "   AND tclscd     = :tclscd"
                                    sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR NVL(rstflg, ' ') <> ' ')"
                                    sSql += "   AND rstflg    IN ('2', '3')"
                                    sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                                    sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = '1')"

                                    dbCmd.CommandText = sSql

                                    With dbCmd
                                        .Parameters.Clear()
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                                        .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                    End With
                                End If

                            Case "1"
                                sSql = ""
                                sSql += "UPDATE " + sTable + ""
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NULL,               mwdt  = NULL,"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno       = :bcno"
                                sSql += "   AND tclscd     = :tclscd"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR NVL(rstflg, ' ') <> ' ')"
                                sSql += "   AND rstflg    <> '1'"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = '1')"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                End With

                            Case "2"
                                sSql = ""
                                sSql += "UPDATE " + sTable + ""
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno       = :bcno"
                                sSql += "   AND tclscd     = :tclscd"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR NVL(rstflg, ' ') <> ' ')"
                                sSql += "   AND rstflg    <> '2'"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN "
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = '1')"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("mwid", OracleDbType.Varchar2).Value = rsUsrId
                                    .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcNo
                                    .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
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

        ' 삭제  fnEdit_LJ011M_new로 새로생성함 
        Private Function fnEdit_LJ011M(ByVal rsBcNo As String) As Integer
            Dim sFn As String = "Private Function fnEdit_LJ011M(String) As Integer"
            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sTable As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += "SELECT r.tclscd, r.spccd, MIN(NVL(r.rstflg, '0')) minrstflg, MAX(NVL(r.rstflg, '0')) maxrstflg, MAX(r.rstdt) rstdt"
                sSql += "  FROM " + sTable + " r, lf060m f"
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
                            sSql += "UPDATE lj011m SET rstflg = NULL, rstdt = NULL, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
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
                            sSql += "UPDATE lj011m SET rstflg = :rstflg, rstdt = :rstdt, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
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


        Private Function fnEdit_LJ010M(ByVal rsBcNo As String) As Integer
            Dim sFn As String = "Private Function fnEdit_LJ010M(String) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT MIN(NVL(j.rstflg, '0')) minrstflg, MAX(NVL(j.rstflg, '0')) maxrstflg"
                sSql += "  FROM lj011m j"
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
                sSql += "UPDATE lj010m SET rstflg = :rstflg, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
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

        '<< 코로나 검사 Ct 결과값 delete (재검)
        Private Function fnDel_LRS18M(ByVal rsBcNo As String) As Integer
            Dim sFn As String = "Private Function fnEdit_LJ010M(String) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT BCNO "
                sSql += "  FROM LRS18M       "
                sSql += " WHERE BCNO = :BCNO "

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

                If dt.Rows.Count <= 0 Then Return 0

                Dim iRet As Integer = 0

                sSql = ""
                sSql += " DELETE LRS18M       "
                sSql += "  WHERE BCNO = :BCNO "

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("bcno ", OracleDbType.Varchar2).Value = rsBcNo

                    iRet = .ExecuteNonQuery()
                End With

                Return 1
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Public Function fnIns_LR080M(ByVal rsBcno As String, ByVal rsSuccessYn As String, ByVal rsSendmsg As String, ByVal rsReturnmsg As String, ByVal rsRtnVal As String _
                                       , Optional ByVal rsSpc As String = "", Optional ByVal rsSpcEtc As String = "", Optional ByVal rsTest As String = "", Optional ByVal rsTestEtc As String = "", Optional ByVal rsRefcd As String = "", Optional ByVal rsRptusr As String = "") As Boolean
            Dim sFn As String = "Private Function fnIns_LR080M(ByVal rsBcno As String, ByVal rsSeq As String, ByVal rsSuccessYn As String, ByVal rsSendmsg As String, ByVal rsReturnmsg As String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand
                Dim dt_p As New DataTable

                Dim dbDa As OracleDataAdapter

                Dim sSql As String = ""
                Dim intRanking As Integer = 0

                Dim iSeq As Integer = 0

                sSql += "select nvl(max(seq),'0') seq "
                sSql += " from lr080m "
                sSql += "where bcno = :bcno "

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                End With

                dt_p.Reset()
                dbDa.Fill(dt_p)

                If dt_p.Rows.Count < 1 Then
                    iSeq = 1
                Else
                    iSeq = CInt(dt_p.Rows(0).Item("seq").ToString) + 1
                End If

                sSql = ""
                sSql += "INSERT INTO lr080m( bcno, seq, successyn, sendmsg, returnmsg , regdt , errmsg , spc ,spcetc , test, testetc , refcd , rptusr ) "
                sSql += "            Values(:bcno,:seq,:successyn,:sendmsg,:returnmsg , sysdate , :errmsg , :spc ,:spcetc , :test, :testetc , :refcd , :rptusr) "

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .Parameters.Add("seq", OracleDbType.Int64).Value = iSeq
                    .Parameters.Add("successyn", OracleDbType.Varchar2).Value = rsSuccessYn
                    .Parameters.Add("sendmsg", OracleDbType.Varchar2).Value = rsSendmsg
                    .Parameters.Add("returnmsg", OracleDbType.Varchar2).Value = rsReturnmsg
                    .Parameters.Add("errmsg", OracleDbType.Varchar2).Value = rsRtnVal

                    .Parameters.Add("spc", OracleDbType.Varchar2).Value = rsSpc
                    .Parameters.Add("spcetc", OracleDbType.Varchar2).Value = rsSpcEtc
                    .Parameters.Add("test", OracleDbType.Varchar2).Value = rsTest
                    .Parameters.Add("testetc", OracleDbType.Varchar2).Value = rsTestEtc
                    .Parameters.Add("refcd", OracleDbType.Varchar2).Value = rsRefcd

                    .Parameters.Add("rptusr", OracleDbType.Varchar2).Value = rsRptusr

                    .ExecuteNonQuery()
                End With

                m_dbTran.Commit()

                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Private Function fnEdit_LR020M(ByVal roCmt As ArrayList, ByVal rsUsrId As String) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR020M(ArrayList, String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim strSrvDt As String = fnGet_Server_DateTime()
                Dim sSql As String = ""
                Dim intRanking As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                For ix As Integer = 0 To roCmt.Count - 1
                    If ix = 0 Then

                        sSql = ""
                        sSql += "INSERT INTO lr020h("
                        sSql += "       bcno, rstseq, moddt,           modid, cmt, regid, regdt ) "
                        sSql += "SELECT bcno, rstseq, fn_ack_sysdate, :modid, cmt, regid, regdt"
                        sSql += "  FROM lr020m"
                        sSql += " WHERE bcno = :bcno"

                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("modid", OracleDbType.Varchar2).Value = rsUsrId
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).BcNo

                            .ExecuteNonQuery()
                        End With

                        sSql = ""
                        sSql += "DELETE lr020m WHERE bcno = :bcno"

                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).BcNo
                            .ExecuteNonQuery()
                        End With
                    End If

                    sSql = ""
                    sSql += "INSERT INTO lr020m(  bcno,  rstseq,  cmt,  regid, regdt,           editid,  editip, editddt )"
                    sSql += "            VALUES( :bcno, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

                    With dbCmd
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).BcNo
                        .Parameters.Add("rstseq", OracleDbType.Varchar2).Value = (ix + 1).ToString
                        .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).Cmt
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrId
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

        Private Function fnEdit_LR040M(ByVal roCmt As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_LR040M(ArrayList, String) As Boolean"

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
                        sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.* FROM lr040m r WHERE bcno = :bcno AND partcd = :partcd AND slipcd = :slipcd"
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
                        sSql += "DELETE lr040m WHERE bcno = :bcno AND partcd = :partcd AND slipcd = :slipcd"

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
                        sSql += "INSERT INTO lr040m"
                        sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt, regdt,           regid,  editid,  editip, editdt )"
                        sSql += "   VALUES ( :bcno, :partcd, :slipcd, :rstseq, :cmt, fn_ack_sysdate, :regid, :editid, :editip, fn_ack_sysdate )"


                        With dbCmd
                            .CommandText = sSql

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).BcNo
                            .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(0, 1)
                            .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(roCmt(ix), ResultInfo_Cmt).PartSlip.Substring(1, 1)
                            .Parameters.Add("rstseq", OracleDbType.Varchar2).Value = (ix + 1).ToString
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

        '20210315 jhs log남기기 위한 정보 테이블 가져오기 
        Public Function fnget_LR010M_log(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Private Function fnget_LR010M(String) As DataTable"
            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sTable As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += " selecT r.bcno, r.testcd, r.spccd, f6.tnmd,r.partcd,r.slipcd,r.viewrst,"
                sSql += "       r.mwid, r.mwdt, fnid,fndt"
                sSql += "  from lr010m r"
                sSql += " inner join lf060m f6"
                sSql += "    on r.testcd = f6.testcd"
                sSql += " where r.bcno = :bcno"
                sSql += "   and f6.usdt <= fn_ack_sysdate"
                sSql += "   and f6.uedt > fn_ack_sysdate"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql


                Dim alParm As New ArrayList
                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '-----------------------------------------------------------------------
        '20210304 jhs 검사자간 공유사항 코멘트 인서트 추가
        Public Function fnReg_shareCmt(ByVal roShareCmtInfo As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnReg_shareCmt(ArrayList) As Boolean"

            Try

                If roShareCmtInfo Is Nothing Then
                Else
                    ''' part slip별 소견일때 
                    If fnEdit_LRC40M(roShareCmtInfo) = False Then  ''' 검체 part slip별 소견 
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
        Private Function fnEdit_LRC40M(ByVal roShareCmt As ArrayList) As Boolean
            Dim sFn As String = "Private Function fnEdit_LRC40M(ArrayList, String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim alSlipCd As New ArrayList
                Dim bAddFlg As Boolean = False
                Dim bdelFlg As Boolean = False

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text


                For ix As Integer = 0 To roShareCmt.Count - 1
                    '--수정 , 입력 로직

                    If CType(roShareCmt(ix), ResultInfo_ShareCmt).SaveFlg = "1" Then

                        If bAddFlg = False Then
                            bAddFlg = True

                            '--히스토리 입력
                            sSql = ""
                            sSql += " INSERT INTO lrc40h "
                            'sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.* FROM lr040m r WHERE bcno = :bcno AND partcd = :partcd AND slipcd = :slipcd"
                            sSql += " select fn_ack_sysdate, :modid, :modip , rstseq, cmt, regdt, regid, regip, regno  from lrc40m where regno = :regno"
                            With dbCmd
                                .CommandText = sSql
                                .Parameters.Clear()
                                .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                                .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(roShareCmt(ix), ResultInfo_ShareCmt).Regno
                                .ExecuteNonQuery()
                            End With

                            '--기존내용 삭제
                            sSql = ""
                            sSql += "DELETE lrc40m WHERE regno = :regno"

                            With dbCmd
                                .CommandText = sSql
                                .Parameters.Clear()
                                .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(roShareCmt(ix), ResultInfo_ShareCmt).Regno
                                .ExecuteNonQuery()
                            End With
                        End If


                        '--내용 삽입
                        If CType(roShareCmt(ix), ResultInfo_ShareCmt).Cmt <> vbCrLf Or CType(roShareCmt(ix), ResultInfo_ShareCmt).Cmt <> "" Or bAddFlg Then
                            sSql = ""
                            sSql += "INSERT INTO lrc40m"
                            sSql += "          (  regno,  rstseq,  cmt, regdt,           regid,  regip)" + vbCrLf
                            sSql += "   VALUES ( :regno, :rstseq, :cmt, fn_ack_sysdate, :regid, :regip)" + vbCrLf

                            With dbCmd
                                .CommandText = sSql
                                .Parameters.Clear()
                                .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(roShareCmt(ix), ResultInfo_ShareCmt).Regno
                                .Parameters.Add("rstseq", OracleDbType.Varchar2).Value = (ix + 1).ToString
                                .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(roShareCmt(ix), ResultInfo_ShareCmt).Cmt
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                .Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                                .ExecuteNonQuery()
                            End With
                        End If

                    ElseIf CType(roShareCmt(ix), ResultInfo_ShareCmt).SaveFlg = "2" Then

                        '--삭제 로직
                        If bdelFlg = False Then
                            bdelFlg = True

                            '--히스토리 입력
                            sSql = ""
                            sSql += " INSERT INTO lrc40h "
                            'sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.* FROM lr040m r WHERE bcno = :bcno AND partcd = :partcd AND slipcd = :slipcd"
                            sSql += " select fn_ack_sysdate, :modid, :modip , rstseq, cmt, regdt, regid, regip, regno  from lrc40m where regno = :regno"
                            With dbCmd
                                .CommandText = sSql
                                .Parameters.Clear()
                                .Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                                .Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                                .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(roShareCmt(ix), ResultInfo_ShareCmt).Regno
                                .ExecuteNonQuery()
                            End With

                            sSql = ""
                            sSql += "DELETE lrc40m WHERE regno = :regno"

                            With dbCmd
                                .CommandText = sSql
                                .Parameters.Clear()
                                .Parameters.Add("regno", OracleDbType.Varchar2).Value = CType(roShareCmt(ix), ResultInfo_ShareCmt).Regno
                                .ExecuteNonQuery()
                            End With
                        End If
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

                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()
                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
            Else
                miUseCase = 1
            End If
        End Sub

        Public Sub New(ByVal r_dbCn As OracleConnection, ByVal r_dbTran As OracleTransaction)
            m_dbCn = r_dbCn
            m_dbTran = r_dbTran

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
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
                sSql += "SELECT grade FROM lf083m"
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

        Public Function RegServer(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Function RegServer"

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

                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then
                    Dim iRegOK_M As Integer = RegServerM(r_al_RstInfo, r_sampinfo_Buf, r_al_EditSuc)

                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_M
                End If
                '-- 2007-10.16 YOOEJ END

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
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i - 1), STU_RstInfo_cvt))
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

                '4) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) Upate LR040M(검사분류별 소견)
                Call fnEdit_LR040M(r_sampinfo_Buf) '-- 자동소견

                '7) 위탁검사 소견 자동 등록 
                If rstinfo_Buf.RegStep = "3" Then
                    Call fnEdit_LR040M_OUT(r_sampinfo_Buf) '-- 자동소견
                End If


                '-- 2009-09-15 YEJ (감염정보)
                If fnEdit_OCS(r_sampinfo_Buf) Then
                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_Sum
                Else
                    Return 0
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function RegServer_RegRst(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Function RegServer"

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

                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then
                    Dim iRegOK_M As Integer = RegServerM(r_al_RstInfo, r_sampinfo_Buf, r_al_EditSuc)

                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_M
                End If
                '-- 2007-10.16 YOOEJ END

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
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i - 1), STU_RstInfo_cvt))
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

                '4) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) Upate LR040M(검사분류별 소견)
                Call fnEdit_LR040M(r_sampinfo_Buf) '-- 자동소견

                '7) 위탁검사 소견 자동 등록 
                If rstinfo_Buf.RegStep = "3" Then
                    Call fnEdit_LR040M_OUT(r_sampinfo_Buf) '-- 자동소견
                End If


                '-- 2009-09-15 YEJ (감염정보)
                If fnEdit_OCS_RegRst(r_sampinfo_Buf) Then
                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_Sum
                Else
                    Return 0
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function RegServer_xpert(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Function RegServer"

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
                '> mod freety 2005/03/18f

                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then
                    Dim iRegOK_M As Integer = RegServerM(r_al_RstInfo, r_sampinfo_Buf, r_al_EditSuc)

                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_M
                End If
                '-- 2007-10.16 YOOEJ END

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
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i - 1), STU_RstInfo_cvt))
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

                '4) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) Upate LR040M(검사분류별 소견)
                Call fnEdit_LR040M_xpert(r_sampinfo_Buf) '-- 자동소견

                '7) 위탁검사 소견 자동 등록 
                'If rstinfo_Buf.RegStep = "3" Then
                '    Call fnEdit_LR040M_OUT(r_sampinfo_Buf) '-- 자동소견
                'End If

                '-- 2009-09-15 YEJ (감염정보)
                If fnEdit_OCS(r_sampinfo_Buf) Then
                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_Sum
                Else
                    Return 0
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '20220221 jhs IGRA 소견 자동 입력
        Public Function RegServer_igra(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Function RegServer"

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
                '> mod freety 2005/03/18f

                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then
                    Dim iRegOK_M As Integer = RegServerM(r_al_RstInfo, r_sampinfo_Buf, r_al_EditSuc)

                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_M
                End If
                '-- 2007-10.16 YOOEJ END

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
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i - 1), STU_RstInfo_cvt))
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

                '4) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) Upate LR040M(검사분류별 소견)
                Call fnEdit_LR040M_igra(r_sampinfo_Buf) '-- 자동소견

                '7) 위탁검사 소견 자동 등록 
                'If rstinfo_Buf.RegStep = "3" Then
                '    Call fnEdit_LR040M_OUT(r_sampinfo_Buf) '-- 자동소견
                'End If

                '8) 2022.03.22 JJH 코로나 SMS 문자전송
                'Call fnEdit_Covid_SMS(r_sampinfo_Buf.BCNo, m_dbCn, m_dbTran)

                '-- 2009-09-15 YEJ (감염정보)
                If fnEdit_OCS(r_sampinfo_Buf) Then
                    'Log 남기기
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                    Return iRegOK_Sum
                Else
                    Return 0
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '-----------------------------------------------


        Public Function Reg_NCOV_RstUpd(ByVal rsBcno As String, ByVal rsTestcd As String) As Boolean
            Dim sFn As String = "Public Function Reg_NCOV_RstUpd(ByVal rsBcno As String, ByVal rsTestcd As String) As Boolean"

            Try

                Dim dbCn_n As OracleConnection
                Dim dbTran_n As OracleTransaction

                dbCn_n = GetDbConnection()
                dbTran_n = dbCn_n.BeginTransaction()


                '<< JJH LJ011M, LR010M 보고시간 update
                If Reg_Rstdt_Upd(rsBcno, rsTestcd, dbCn_n, dbTran_n) = False Then
                    Return False
                End If






                Return True

            Catch ex As Exception
                Fn.log(sFn + "||" + ex.Message)
                Return False
            End Try
        End Function

        Public Function Reg_Rstdt_Upd(ByVal rsBcno As String, ByVal rsTestcd As String, ByVal dbCn As OracleConnection, ByVal dbTran As OracleTransaction) As Boolean

            Try
                Dim dbCmd As New OracleCommand
                Dim sSql As String = ""
                Dim Nowdate As String = Now.ToString("yyyymmddhhmiss")

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTran


                sSql = ""
                sSql += " UPDATE LR010M "
                sSql += "    SET FNDT   = :FNDT, "
                sSql += "        RSTDT  = :RSTDT "
                sSql += "  WHERE BCNO   = :BCNO "
                sSql += "    AND TESTCD = :TESTCD "
                sSql += "    AND RSTFLG = '3' "

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("fndt", OracleDbType.Varchar2).Value = Nowdate
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = Nowdate
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestcd
                End With

                dbCmd.ExecuteNonQuery()

                sSql = ""
                sSql += " UPDATE LJ011M "
                sSql += "    SET RSTDT  = :RSTDT "
                sSql += "  WHERE BCNO   = :BCNO "
                sSql += "    AND TCLSCD = :TCLSCD "
                sSql += "    AND RSTFLG = '3' "

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = Nowdate
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = rsTestcd
                End With

                dbCmd.ExecuteNonQuery()


                Return True
            Catch ex As Exception
                Return False
            End Try

        End Function


        Public Function fnGet_CalcRstInfo_BcNo(ByVal rsBcNo As String, Optional ByVal rbAuto As Boolean = False, Optional ByVal r_objLisdbTran As Object = Nothing) As DataTable
            Dim sFn As String = "Public Shared Function Get_CalcRstInfo_BcNo(String, (Boolean), (Object)) As DataTable"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT b.* FROM lr010m a,"
                sSql += "       (SELECT 1, c.calform, r.bcno, r.testcd ctestcd, r.testcd, f.tnmd, r.orgrst, r.rstflg,"
                sSql += "               c.param0 || '/' || NVL(c.param1, '')"
                sSql += "               || '/' || NVL(c.param2, '') || '/' || NVL(c.param3, '')"
                sSql += "               || '/' || NVL(c.param4, '') || '/' || NVL(c.param5, '')"
                sSql += "               || '/' || NVL(c.param6, '') || '/' || NVL(c.param7, '')"
                sSql += "               || '/' || NVL(c.param8, '') || '/' || NVL(c.param9, '') calitems,"
                sSql += "               f.dispseql sortpkey, 0 sortskey, c.caldays , c.calrange"
                sSql += "          FROM lr010m r, lf069m c, lf060m f"
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
                sSql += "                       WHEN TRIM(c.param0) THEN 'A'"
                sSql += "                       WHEN TRIM(c.param1) THEN 'B'"
                sSql += "                       WHEN TRIM(c.param2) THEN 'C'"
                sSql += "                       WHEN TRIM(c.param3) THEN 'D'"
                sSql += "                       WHEN TRIM(c.param4) THEN 'E'"
                sSql += "                       WHEN TRIM(c.param5) THEN 'F'"
                sSql += "                       WHEN TRIM(c.param6) THEN 'G'"
                sSql += "                       WHEN TRIM(c.param7) THEN 'H'"
                sSql += "                       WHEN TRIM(c.param8) THEN 'I'"
                sSql += "                       WHEN TRIM(c.param9) THEN 'J'"
                sSql += "                       ELSE '-'"
                sSql += "                  END calform,"
                sSql += "               r.bcno, c.testcd ctclscd, r.testcd, f.tnmd, r.orgrst, r.rstflg,"
                sSql += "               '' calitems,"
                sSql += "               f.dispseql sortpkey,"
                sSql += "               CASE RPAD(r.testcd, 7, ' ') || r.spccd"
                sSql += "                    WHEN TRIM(c.param0) THEN 10"
                sSql += "                    WHEN TRIM(c.param1) THEN 11"
                sSql += "                    WHEN TRIM(c.param2) THEN 12"
                sSql += "                    WHEN TRIM(c.param3) THEN 13"
                sSql += "                    WHEN TRIM(c.param4) THEN 14"
                sSql += "                    WHEN TRIM(c.param5) THEN 15"
                sSql += "                    WHEN TRIM(c.param6) THEN 16"
                sSql += "                    WHEN TRIM(c.param7) THEN 17"
                sSql += "                    WHEN TRIM(c.param8) THEN 18"
                sSql += "                    WHEN TRIM(c.param9) THEN 19"
                sSql += "                    ELSE 20"
                sSql += "               END sortskey, c.caldays , c.calrange"
                sSql += "          FROM lr010m r, lf069m c, lf060m f"
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
                Dim dt As DataTable = fnGet_CalcRstInfo_BcNo(rsBcNo, True, m_dbTran)

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

        Private Function fnGet_CalcState_BcNo(ByVal rsBcNo As String, Optional ByVal rbAuto As Boolean = False, Optional ByVal r_objLisdbTran As Object = Nothing) As DataTable
            Dim sFn As String = "fnGet_CalcState_BcNo"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT r.bcno, MIN(NVL(r.rstflg, '0')) minrstflg, NVL(c.calview, 'A') calview"
                sSql += "  FROM lr010m r, lf069m c"
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
        '<<<20180424 자동소견
        Private Function fnGet_ACmt_BcNo(ByVal rsBcNo As String, Optional ByVal r_objLisdbTran As Object = Nothing) As DataTable
            Dim sFn As String = "fnGet_CalcState_BcNo"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT r.bcno, MIN(NVL(r.rstflg, '0')) minrstflg, NVL(c.calview, 'A') calview"
                sSql += "  FROM lr010m r, lf086m c"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND r.testcd = c.testcd"
                sSql += "   AND r.spccd  = c.spccd"
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
                Dim dt As DataTable = fnGet_CalcState_BcNo(rsBcNo, True, m_dbTran)
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

        Public Function fnCalcRstInfo(ByVal r_SampInfo As STU_SampleInfo, ByVal r_al_RstInfo As ArrayList) As ArrayList
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

        Public Function fnGet_BcNo_Testcds(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Public Function fnGet_BcNo_Testcds(ByVal rsBcNo As String) As DataTable"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim sTestCds As String = ""

                sSql = ""
                sSql += "SELECT r.bcno, r.testcd"
                sSql += "  FROM lr010m r"
                sSql += " WHERE r.bcno     = :bcno"

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

        Public Function fnGet_CvtRst_State_BcNo(ByVal rsBcNo As String, Optional ByVal r_al_TestInfo As ArrayList = Nothing) As DataTable
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
                sSql += "  FROM lr010m r, lf084m c, lf083m d"
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
                sSql += "       f.tnmd, r.orgrst, r.viewrst, r.hlmark, MIN(NVL(r.rstflg, '0')) rstflg"
                sSql += "  FROM lr010m r, lf085m c, lf060m f"
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
                sSql += "          f.tnmd, r.orgrst, r.viewrst, r.hlmark"

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
                sSql += "       f.tnmd, r.orgrst, r.viewrst, r.hlmark, MIN(NVL(r.rstflg, '0')) rstflg,"
                sSql += "  FROM lr010m r, lj010m j, lf085m c, lf060m f"
                sSql += " WHERE (j.regno, j.orddt) = (SELECT regno, orddt FROM lj010m WHERE bcno = :bcno)"
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
                sSql += "          f.tnmd, r.orgrst, r.viewrst, r.hlmark"


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
                    objCvt.CTestCd = dt.Rows(intIdx).Item("ctestcd").ToString.Trim
                    objCvt.OrgRst = IIf(dt.Rows(intIdx).Item("orgrst").ToString.Trim = "", rsOrgRst, dt.Rows(intIdx).Item("orgrst").ToString.Trim).ToString
                    objCvt.ViewRst = dt.Rows(intIdx).Item("viewrst").ToString.Trim
                    objCvt.HlMark = dt.Rows(intIdx).Item("hlmark").ToString.Trim
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

        Public Function fnCvtRstInfo(ByVal r_SampInfo As STU_SampleInfo, ByVal r_al_TestInfo As ArrayList, _
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

                        Dim ri As STU_RstInfo_cvt = New STU_RstInfo_cvt

                        ri.TestCd = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).TestCd
                        ri.OrgRst = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).OrgRst
                        ri.ViewRst = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).ViewRst
                        ri.RstCmt = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).RstCmt
                        ri.HlMark = CType(arlRstInfo(intIdx), STU_RstInfo_cvt).HlMark
                        'ri.RstCmt = ri.RstCmt

                        al_RstInfo_Cvt.Add(ri)

                        ri = Nothing
                    End If
                Next

                Return al_RstInfo_Cvt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function fnCvtRstInfo(ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal r_al_TestInfo As ArrayList) As ArrayList
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

                                    If CType(alCvt(ix), STU_RstInfo_cvt).CvtFldGbn <> "C" Then
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

        Public Function Find_Calculated_Result(ByVal rsCalForm As String) As String
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

        Public Function Find_Calculated_Settings(ByVal rsTestCd As String) As DataTable
            Dim sFn As String = "Function Find_Calculated_Result"

            Dim sReturn As String = ""

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += "SELECT rsttype, rstllen, rstulen, cutopt"
                sSql += "  FROM lf060m"
                sSql += " WHERE testcd = :testcd"
                sSql += "   AND uedt   > fn_ack_sysdate"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTestCd
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

        Public Function Get_CalcRstInfo_Pat(ByVal rsBcNo As String, ByVal rsTClsCd As String, ByVal rsSpcCd As String, _
                                                   ByVal rsCalDays As String, ByVal rsCalRange As String, Optional ByVal r_objLisDbTran As Object = Nothing) As DataTable
            Dim sFn As String = "Public Shared Function Get_CalcRstInfo_Pat(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "pkg_ack_rst.pkg_get_pat_calc_rstinfo_m"
                Else
                    sSql += "pkg_ack_rst.pkg_get_pat_calc_rstinfo"
                End If

                Dim al As New ArrayList

                al.Add(New OracleParameter("rs_bcno", rsBcNo))
                al.Add(New OracleParameter("rs_testcd", rsTClsCd))
                al.Add(New OracleParameter("rs_spccd", rsSpcCd))
                al.Add(New OracleParameter("ri_caldays", Convert.ToInt16(rsCalDays)))

                DbCommand(r_objLisDbTran)

                Dim dt As DataTable = DbExecuteQuery(sSql, al, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 2008-11-28 yooeJ add
        '-- 자동 소견 등록
        Public Function fnEdit_LR020M(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_LR020M(object) As String"
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
                sSql += "  FROM lr010m"
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

                arlCmt = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(r_sampinfo_Buf.BCNo, arlRst, "", True)

                If arlCmt.Count < 1 Then Return ""

                sSql = "SELECT * FROM lr020m WHERE bcno = :bcno"

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
                    sSql += "INSERT INTO lr020m(  bcno,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt)"
                    sSql += "            VALUES( :bcno, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

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
        Public Function fnEdit_LR040M(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_LR040M(object) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sTableNm As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT r.testcd, r.orgrst, r.viewrst, r.hlmark, r.eqflag"
                sSql += "  FROM " + sTableNm + " r"
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

                alCmtVal = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(r_sampinfo_Buf.BCNo, alRstInfo, "", True, m_dbCn, m_dbTran)

                If alCmtVal.Count < 1 Then Return ""

                sSql = ""
                sSql += "SELECT bcno, partcd, slipcd, cmt"
                sSql += "  FROM lr040m"
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
                        sSql += "INSERT INTO lr040m"
                        sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt )"
                        sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

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

        Public Function fnEdit_LR040M_xpert(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_LR040M(object) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim PrealCmtVal As New ArrayList
                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList
                Dim sCriticalmark As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sTableNm As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT r.testcd, r.orgrst, r.viewrst, r.hlmark, r.eqflag, r.criticalmark"
                sSql += "  FROM " + sTableNm + " r"
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

                            sCriticalmark = dt.Rows(ix).Item("criticalmark").ToString

                        End With

                        alRstInfo.Add(objRst)
                    Next
                End If

                '<--
                If dt.Rows(0).Item("testcd").ToString = "LG104" Then
                    sbDisplay_XPertCmt(r_sampinfo_Buf.BCNo, alCmtVal, sCriticalmark, dt.Rows(0).Item("viewrst").ToString)


                    sSql = ""
                    sSql += "DELETE lr040m "
                    sSql += " WHERE bcno = :bcno"
                    sSql += "   AND partcd = :partcd"
                    sSql += "   AND slipcd = :slipcd"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(alCmtVal(0), STU_CvtCmtInfo).SlipCd.Substring(0, 1)
                        .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(alCmtVal(0), STU_CvtCmtInfo).SlipCd.Substring(1, 1)

                    End With

                    dbCmd.ExecuteNonQuery()

                End If
                '-->

                Fn.log("-----------")

                PrealCmtVal = alCmtVal
                alCmtVal = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(r_sampinfo_Buf.BCNo, alRstInfo, "", True, m_dbCn, m_dbTran)

                If alCmtVal.Count < 1 Then
                    If PrealCmtVal.Count < 1 Then
                        Return ""
                    Else
                        alCmtVal = PrealCmtVal
                    End If
                End If

                sSql = ""
                sSql += "SELECT bcno, partcd, slipcd, cmt"
                sSql += "  FROM lr040m"
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
                        sSql += "INSERT INTO lr040m"
                        sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt )"
                        sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

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
        '20220221 jhs IGRA 소견 자동 입력
        Public Function fnEdit_LR040M_igra(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_LR040M(object) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim dt_t As New DataTable
                Dim PrealCmtVal As New ArrayList
                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList
                Dim sCriticalmark As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sTableNm As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT r.testcd, r.orgrst, r.viewrst, r.hlmark, r.eqflag, r.criticalmark, r.regno"
                sSql += "  FROM " + sTableNm + " r"
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

                            sCriticalmark = dt.Rows(ix).Item("criticalmark").ToString

                        End With

                        alRstInfo.Add(objRst)
                    Next
                End If


                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql = ""
                sSql += " select CLSVAL          "
                sSql += "   from LF000M          "
                sSql += "  where CLSGBN = 'IGRA' "
                sSql += "    and CLSCD LIKE 'T%' "

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                End With

                dt_t.Reset()
                dbDa.Fill(dt_t)

                Dim Igra_ary As New List(Of String)
                For ix As Integer = 0 To dt_t.Rows.Count - 1
                    Igra_ary.Add(dt_t.Rows(ix).Item("clsval").ToString())
                Next

                Dim igra_testcd As String = dt.Rows(0).Item("testcd").ToString()
                Dim igra_regno As String = dt.Rows(0).Item("regno").ToString()
                Dim igra_cbc_cmt As String = ""

                '---- 결핵균특이항원자극 Interferon-gamma 인 경우
                If Igra_ary.Contains(igra_testcd) Then
                    sbDisplay_IgraCmt(r_sampinfo_Buf.BCNo, alCmtVal, igra_testcd, igra_regno)


                    sSql = ""
                    sSql += "DELETE lr040m "
                    sSql += " WHERE bcno = :bcno"
                    sSql += "   AND partcd = :partcd"
                    sSql += "   AND slipcd = :slipcd"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(alCmtVal(0), STU_CvtCmtInfo).SlipCd.Substring(0, 1)
                        .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(alCmtVal(0), STU_CvtCmtInfo).SlipCd.Substring(1, 1)

                    End With

                    dbCmd.ExecuteNonQuery()

                    igra_cbc_cmt = Pat_CBC_Rst(igra_testcd, igra_regno, Igra_ary)

                End If
                '-->

                Fn.log("-----------")

                PrealCmtVal = alCmtVal
                alCmtVal = LISAPP.COMM.CvtCmt.fnCvtCmtInfo(r_sampinfo_Buf.BCNo, alRstInfo, "", True, m_dbCn, m_dbTran)

                Dim Cmt_temp As New STU_CvtCmtInfo
                If alCmtVal.Count < 1 Then
                    If PrealCmtVal.Count < 1 Then
                        Return ""
                    Else
                        alCmtVal = PrealCmtVal
                        CType(alCmtVal(0), STU_CvtCmtInfo).CmtCont += vbCrLf + igra_cbc_cmt
                    End If
                Else
                    If PrealCmtVal.Count > 0 Then

                        '1. 환자이전결과 소견
                        '2. 기초마스터 자동소견
                        '3. 환자 CBC 이전결과 소견
                        With Cmt_temp
                            .BcNo = CType(alCmtVal(0), STU_CvtCmtInfo).BcNo
                            .SlipCd = CType(alCmtVal(0), STU_CvtCmtInfo).SlipCd
                            .CmtCont = CType(PrealCmtVal(0), STU_CvtCmtInfo).CmtCont + vbCrLf + vbCrLf '1
                            .CmtCont += CType(alCmtVal(0), STU_CvtCmtInfo).CmtCont + vbCrLf '2
                            .CmtCont += igra_cbc_cmt '3
                        End With

                        alCmtVal(0) = Cmt_temp

                    End If
                End If

                sSql = ""
                sSql += "SELECT bcno, partcd, slipcd, cmt"
                sSql += "  FROM lr040m"
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
                        sSql += "INSERT INTO lr040m"
                        sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt )"
                        sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

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
        '-----------------------------------------------------


        Private Sub sbDisplay_XPertCmt(ByVal rsBcno As String, ByRef rarrList As ArrayList, ByVal rsCritical As String, ByVal rsViewrst As String)
            Dim dt As DataTable : Dim sCMT As String = "" : Dim sCMT1 As String = "" : Dim sCmt2 As String = "" : Dim sCmt3 As String = "" : Dim sViewrst As String = ""
            Dim sGbn1 As String = "1.", sGbn2 As String = "2.", sGbn3 As String = "3."

            Dim objRst As New STU_CvtCmtInfo
            '환자의 최근 1주일 결핵균 결과를 가져온다
            dt = fnGet_Xpert_Comment(rsBcno)

            '환자의 이전 1주일전 결과가 있을경우
            If dt.Rows.Count > 0 Then
                '1주일 결과중 검체의 최대 사이즈를 가져오기 위하여 Data Table에 ORDER BY하여 변수에 담는다
                Dim a_dr As DataRow() = dt.Select("", "spclen desc")

                Dim sSpcLen As String = a_dr(0).Item("spclen").ToString.Trim

                '1. 검사결과가 Critical일 경우 다음과 같은 소견을 만듬
                If rsCritical = "C" Then
                    sCMT1 += sGbn1 + "Critical value를 다음과 같이 통보함" + vbCrLf
                    sCMT1 += "1) 수신자-소속:                    / 성명:" + vbCrLf
                    sCMT1 += "2) 발신자-소속: 진단검사의학과     / 성명:" + vbCrLf
                    sCMT1 += "3) 통보시간: " + vbCrLf + vbCrLf
                End If
                '1의 소견이 안들어 갈 경우 2의 소견이 1.번으로 치환
                If rsCritical <> "C" Then
                    '1번 3번 소견이 안들어갈 경우 *로 치환
                    If rsCritical <> "C" And rsViewrst.ToLower.Replace(" ", "").Contains("m.tuberculosis:detected") = False Then
                        sGbn2 = "*"
                    Else
                        sGbn2 = "1."
                    End If
                End If
                '2. 최근 1주일 검사결과가 있을 경우 다음과 같은 소견을 만듬
                sCmt2 += sGbn2 + "상기 검체 접수일로부터 최근 1주일 이내에 의뢰된 결핵균 및 리팜핀 내성 [Xpert PCR] 검사 결과 :" + vbCrLf + vbCrLf
                sCmt2 += "검사시행날짜" + Space(8 + CInt(sSpcLen)) + "검체번호" + Space(17) + "검사결과" + vbCrLf
                '1의 소견이 안들어 갈경우 3의 소견을 2소견으로 치환
                If rsCritical <> "C" Then
                    sGbn3 = "2."
                End If
                '3. 검사결과분류 소견(CT Range)을 만듬
                If rsViewrst.ToLower.Replace(" ", "").Contains("m.tuberculosis:detected") = True Then
                    sCmt3 += sGbn3 + "검사결과 분류 :" + vbCrLf + vbCrLf
                    sCmt3 += "MTB result                                  Ct range" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, HIGH             <16" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, MEDIUM           16-22" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, LOW              23-28" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, VERY LOW         >28" + vbCrLf
                End If
                '2의 최근 1주일 검사 결과 값을 소견으로 만들어준다
                For ix = 1 To dt.Rows.Count
                    '검체명 길이대로 공백 채우기 위한 계산
                    Dim sSpcLenRow As String = ""

                    sSpcLenRow = dt.Rows(ix - 1).Item("spclen").ToString

                    sSpcLenRow = CStr((8 + CInt(sSpcLen)) - CInt(sSpcLenRow))

                    If ix <> 1 Then sCmt2 += vbCrLf
                    '결과값에 엔터 값이 들어가 있을 경우 한줄 결과로 만들어주는 코드
                    If dt.Rows(ix - 1).Item("viewrst").ToString.IndexOf(Chr(13)) > 0 Then
                        '> 2019-12-17 수정해야 함.
                        Dim sViewrst1 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(0)
                        Dim sViewrst2 As String = dt.Rows(ix - 1).Item("viewrst").ToString.Split(Chr(13))(1)
                        'sViewrst = sViewrst1.Replace(Chr(10), "") + " " + sViewrst2.Replace(Chr(10), "")
                        'sViewrst = sViewrst1.Replace(Chr(10), "") + vbCrLf + sViewrst2.Replace(Chr(10), "")
                        'sViewrst = sViewrst1.Replace(Chr(10), "") + vbCrLf + Space(12) + Space(CInt(sSpcLenRow) + 2) + Space(CInt(sSpcLenRow) - 2) + Space(10) + sViewrst2.Replace(Chr(10), "")
                        sViewrst = sViewrst1.Replace(Chr(10), "") + vbCrLf + Space(62) + sViewrst2.Replace(Chr(10), "")
                    Else
                        sViewrst = dt.Rows(ix - 1).Item("viewrst").ToString
                    End If
                    '결과일시 [검체명]   검체번호 결과값 순으로 소견 만들어 줌
                    sCmt2 += dt.Rows(ix - 1).Item("fndt").ToString + Space(2) + "[" + dt.Rows(ix - 1).Item("spcnms").ToString + "]" + Space(CInt(sSpcLenRow) - 2) + dt.Rows(ix - 1).Item("bcno").ToString + Space(10) + sViewrst

                    If ix = dt.Rows.Count Then sCmt2 += vbCrLf + vbCrLf
                Next
            ElseIf dt.Rows.Count <= 0 Then '1주일 전 결과가 없을 경우
                '1. Critical 소견을 만든
                If rsCritical = "C" = True Then
                    sCMT1 += sGbn1 + "Critical value를 다음과 같이 통보함" + vbCrLf
                    sCMT1 += "1) 수신자-소속:                    / 성명:" + vbCrLf
                    sCMT1 += "2) 발신자-소속: 진단검사의학과     / 성명:" + vbCrLf
                    sCMT1 += "3) 통보시간: " + vbCrLf + vbCrLf
                End If
                '1의 소견이 안들어 갈 경우 2의 소견이 1.번으로 치환
                If rsCritical <> "C" Then
                    '1번 3번 소견이 안들어갈 경우 *로 치환
                    If rsCritical <> "C" And rsViewrst.ToLower.Replace(" ", "").Contains("m.tuberculosis:detected") = False Then
                        sGbn2 = "*"
                    Else
                        sGbn2 = "1."
                    End If
                End If
                '2. 최근 1주일 검사결과가 없을 경우 다음과 같은 소견을 만듬
                sCmt2 += sGbn2 + "상기 검체 접수일로부터 최근 1주일 이내에 의뢰된 결핵균 및 리팜핀 내성 [Xpert PCR] 검사 결과 : 검사이력 없음" + vbCrLf + vbCrLf
                '1의 소견이 안들어 갈경우 3의 소견을 2소견으로 치환
                If rsCritical <> "C" Then
                    sGbn3 = "2."
                End If
                '3. 검사결과분류 소견(CT Range)을 만듬
                If rsViewrst.ToLower.Replace(" ", "").Contains("m.tuberculosis:detected") = True Then
                    sCmt3 += sGbn3 + "검사결과 분류 :" + vbCrLf + vbCrLf
                    sCmt3 += "MTB result                                  Ct range" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, HIGH             <16" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, MEDIUM           16-22" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, LOW              23-28" + vbCrLf
                    sCmt3 += "M.tuberculosis : DETECTED, VERY LOW         >28" + vbCrLf
                End If
            End If
            '1,2,3 소견 합치기
            sCMT = sCMT1 + sCmt2 + sCmt3
            'If sCMT1 <> "" Then
            '    sCMT = sCMT1 + sCmt2
            'ElseIf sCMT1 = "" Then
            '    sCMT = sCmt2
            'End If

            Fn.log("SCMT -- " + sCMT)

            objRst.BcNo = rsBcno
            objRst.SlipCd = "G1"
            objRst.CmtCont = sCMT

            rarrList.Add(objRst)
        End Sub
        '20220221 jhs IGRA 소견 자동 입력
        Private Sub sbDisplay_IgraCmt(ByVal rsBcno As String, ByRef rarrList As ArrayList, ByVal rsTestCd As String, ByVal rsRegno As String)
            Dim dt As DataTable : Dim sCMT As String = "" : Dim sCMT1 As String = "" : Dim sCmt2 As String = "" : Dim sCmt3 As String = "" : Dim sViewrst As String = ""

            Dim objRst As New STU_CvtCmtInfo
            '환자의 최근 2년 결핵균 결과를 가져온다
            dt = fnGet_IGRA_Comment(rsBcno)

            '환자의 이전 1주일전 결과가 있을경우
            If dt.Rows.Count > 0 Then
                '1주일 결과중 검체의 최대 사이즈를 가져오기 위하여 Data Table에 ORDER BY하여 변수에 담는다
                'Dim a_dr As DataRow() = dt.Select("", "fndt desc")

                '2. 최근 2년간 검사 총회 입력
                sCMT1 += "이전검사결과:" + dt.Rows(0).Item("fndt").ToString + " ~ 금번 검사일 사이 기간 중 총 " + dt.Rows.Count.ToString + "회" + vbCrLf

                '2 의 최근 2년간 검사결과 입력
                For ix = 0 To dt.Rows.Count -1
                    sCMT1 += "[" + dt.Rows(ix).Item("fndt").ToString + " 국립중앙의료원]" + dt.Rows(ix).Item("viewrst").ToString + vbCrLf
                Next
            ElseIf dt.Rows.Count <= 0 Then '1주일 전 결과가 없을 경우
                sCMT1 += "상기 검체 접수일로부터 최근 2년 이내에 의뢰된 결핵균특이항원자극 Interferon-gamma 검사 결과 : 검사이력 없음" + vbCrLf + vbCrLf
            End If

            'sCmt2 += "" + vbCrLf
            'sCmt2 += "1. 참고치" + vbCrLf
            'sCmt2 += " (1) TB1 Antigen: 결핵단백항원에 대한 CD4+ T 림프구의 interferon-gamma 반응을 측정함." + vbCrLf
            'sCmt2 += " (2) TB2 Antigen: 결핵단백항원에 대한 CD4+ T 림프구와 CD8+ T 림프구의 interferon-gamma 반응을 측정함." + vbCrLf + vbCrLf

            'sCmt2 += "2. 판정기준" + vbCrLf
            'sCmt2 += " (1) Negative: Nil tube가 판정 대상 범위 내 값을 보이면서 TB1, TB2 Antigen 모두 0.35 IU/mL 미만인 경우" + vbCrLf
            'sCmt2 += " (2) Positive: Nil tube가 판정 대상 범위 내 값을 보이면서 TB1, TB2 Antigen 중 하나 이상 " + vbCrLf
            'sCmt2 += "     0.35 IU/mL 이상인 경우" + vbCrLf
            'sCmt2 += " (3) Indeterminate: 해당 검체의 면역반응이 판정 대상 범위 외로 나타나(과도 또는 저조), " + vbCrLf
            'sCmt2 += "     결과 판정이 불가능한 경우" + vbCrLf + vbCrLf

            'sCmt2 += "3. 검사개요" + vbCrLf
            'sCmt2 += " (1) 검사원리: 세포매개성 면역반응, 즉 M.tuberculosis 및 소수 non-tuberculous mycobacteria에 분포하는 " + vbCrLf
            'sCmt2 += "     단백 항원에 대한 interferon-gamma 반응을 검사대상자의 전혈 검체에서 ELISA 법으로 측정함." + vbCrLf
            'sCmt2 += " (2) 관련 질환과 의의: 활동성 결핵과 잠복결핵 (Active and Latent tuberculosis infection). " + vbCrLf
            'sCmt2 += "     본 검사에서 양성 결과를 보이는 경우, 의학적/진단적 평가를 후속으로 진행할 것이 권장됨. " + vbCrLf
            'sCmt2 += "     감염의 단계, 면역학적 변수, 검체 채취 시점과 취급 중 변수 등에 의해 위음성 결과 또한 가능함." + vbCrLf
            'sCmt2 += " (3) 검사의 변경: 검사 Kit가 2019년 06월 12일부터 TB1 & TB2 Antigen tube를 사용하도록 업그레이드되었음." + vbCrLf + vbCrLf

            'sCMT = sCMT1 + sCmt2
            sCMT = sCMT1

            '// YJY 결핵검사 진행 시 환자의 최근 CBC검사항목 결과 가져와 소견으로 Display.
            'sCMT += Pat_CBC_Rst(rsTestCd, rsRegno)

            Fn.log("SCMT -- " + sCMT)

            objRst.BcNo = rsBcno
            objRst.SlipCd = "I2"
            objRst.CmtCont = sCMT

            rarrList.Add(objRst)
        End Sub
        Private Function Pat_CBC_Rst(ByVal sTestcd As String, ByVal sRegno As String, ByVal sIgra_Testcd As List(Of String)) As String
            Dim sFN As String = "Public Function Pat_CBC_Rst() As DataTable"
            Dim sCmt2 As String = ""
            Try
                '< 2016-11-22 YJY 결핵검사 진행 시 환자의 최근 CBC검사항목 결과 가져와 소견으로 Display.
                If sIgra_Testcd.Contains(sTestcd) Then
                    Dim a_dt As DataTable = New DataTable
                    Dim stestisno611 As String = "", stestisno612 As String = "", stest611rdt As String = "", stest611rst As String = "", stest611rstunit As String = "",
                stest612rstunit As String = "", stest612rdt As String = "", stest612rst As String = ""

                    'If msRegNoCmt <> "" Then 'LI611, LI612 검사로 판단되면
                    a_dt = fnGet_Pat_Recent_Rst(sRegno) '환자의 최근 CBC검사 항목 가져오기

                    '불러온 이전 결과 없을 경우 "기존의뢰 없음" 표시 
                    If a_dt.Rows.Count = 0 Then
                        If sCmt2 = "" Then
                            sCmt2 += "4. 과거 일반혈액 검사결과 " '2019-07-10 JJH 3->4 수정
                            sCmt2 += vbNewLine
                            sCmt2 += "   검사항목                                   검사시행날짜      실제결과 "
                            sCmt2 += vbNewLine
                            sCmt2 += "   WBC Count (CBC)                            기존의뢰 없음"
                            sCmt2 += vbNewLine
                            sCmt2 += "   Lymphocyte Count (WBC Differential Count)  기존의뢰 없음"
                        End If
                    Else
                        '-결과 있을 경우 이전 결과 변수 담기
                        For i As Integer = 0 To a_dt.Rows.Count - 1
                            If a_dt.Rows(i).Item("testcd").ToString.Equals("LH101") Then
                                stest611rdt = a_dt.Rows(i).Item("rstdtd").ToString
                                stest611rst = a_dt.Rows(i).Item("viewrst").ToString
                                stest611rstunit = a_dt.Rows(i).Item("rstunit").ToString
                            ElseIf a_dt.Rows(i).Item("testcd").ToString.Equals("LH12103") Then
                                stest612rdt = a_dt.Rows(i).Item("rstdtd").ToString
                                stest612rst = a_dt.Rows(i).Item("viewrst").ToString
                                stest612rstunit = a_dt.Rows(i).Item("rstunit").ToString
                            End If
                        Next
                        '-
                        If stest611rdt = "" Then
                            stestisno611 = "기존의뢰 없음"
                        ElseIf stest612rdt = "" Then
                            stestisno612 = "기존의뢰 없음"
                        End If
                        '-자동 소견 양식 만들고 이전 결과 넣어 주기
                        sCmt2 += "4. 과거 일반혈액 검사결과 "  '2019-07-10 JJH 3->4 수정
                        sCmt2 += vbNewLine
                        sCmt2 += "   검사항목                                   검사시행날짜      실제결과 "
                        sCmt2 += vbNewLine
                        If stestisno611 = "" Then
                            sCmt2 += "   WBC Count (CBC)                            " + stest611rdt + Space(8) + stest611rst + Space(1) + stest611rstunit
                        Else
                            sCmt2 += "   WBC Count (CBC)                            " + stestisno611
                        End If
                        sCmt2 += vbNewLine
                        If stestisno612 = "" Then
                            sCmt2 += "   Lymphocyte Count (WBC Differential Count)  " + stest612rdt + Space(8) + stest612rst + Space(1) + stest612rstunit
                        Else
                            sCmt2 += "   Lymphocyte Count (WBC Differential Count)  " + stestisno612
                        End If
                        '-
                    End If
                End If

                Return sCmt2
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFN, ex))
            End Try

        End Function

        '-------------------------------------------------------
        Public Function fnGet_Xpert_Comment(ByVal rsBcno As String, Optional ByVal RsCriticalGbn As Boolean = False) As DataTable
            '해당 환자의 Xpert PCR 검사 최근 1주일 검사결과 가져오는 쿼리 함수
            Dim sFn As String = "Public Shared Function fnGet_Xpert_Comment(ByVal rsBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList
            'Dim OleDbCmd As New OleDb.OleDbCommand
            'Dim OleDbDA As New OleDb.OleDbDataAdapter

            Dim dbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran

            'OleDbCmd.Connection = m_OleDbCn
            'OleDbCmd.Transaction = m_OleDbTrans
            'OleDbCmd.CommandType = CommandType.Text

            Try
                sSql = ""
                sSql += "SELECT r.regno ,r.testcd , r.spccd ,  r.tkdt , r.bcno , decode(r.rstflg , '1' , TO_CHAR(TO_DATE(r.regdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') , '3' , TO_CHAR(TO_DATE(r.fndt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd')) fndt, r.viewrst"
                sSql += "       ,f3.spcnms, MAX(length(f3.spcnms)) spclen "
                sSql += "  FROM lr010m r,"
                sSql += "  ("
                sSql += "   SELECT regno , tclscd , tkdt , bcno "
                sSql += "     FROM lj011m"
                sSql += "    WHERE bcno = :bcno"
                sSql += "      AND tclscd = 'LG104'"
                sSql += "  ) x , lf030m f3"
                sSql += " WHERE r.regno = x.regno"
                sSql += "   AND r.testcd = x.tclscd"
                sSql += "   AND r.tkdt BETWEEN TO_CHAR(TO_DATE(x.tkdt , 'yyyy-mm-dd hh24:mi:ss') - 7, 'yyyymmddhh24miss') AND x.tkdt"
                '>2019-12-17 추가 rstflg 
                If RsCriticalGbn = False Then
                    'sSql += "   AND r.rstflg = '1'"
                    sSql += "   AND nvl(r.viewrst , ' ') <> ' '"
                End If
                sSql += "   AND r.bcno NOT IN (:bcno)  "
                sSql += "   AND r.spccd = f3.spccd"
                sSql += "   AND f3.usdt <= r.tkdt"
                sSql += "   AND f3.uedt > r.tkdt"
                If RsCriticalGbn = True Then
                    sSql += " And lower(Replace(r.orgrst, ' ', ''))  LIKE '%m.tuberculosis:detected%'"
                End If
                sSql += " GROUP BY  r.regno , r.testcd, r.spccd, r.tkdt, r.bcno, decode(r.rstflg , '1' , TO_CHAR(TO_DATE(r.regdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') , '3' , TO_CHAR(TO_DATE(r.fndt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd')), r.viewrst"
                sSql += "      , f3.spcnms"
                sSql += "   ORDER BY r.tkdt"

                Fn.log("sql 문 - " & sSql)

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    '.SelectCommand.Parameters.Add("@param1", OleDb.OleDbType.VarChar).Value = rsBcno
                    '.SelectCommand.Parameters.Add("@param2", OleDb.OleDbType.VarChar).Value = rsBcno
                End With

                dt.Reset()
                dbDa.Fill(dt)

                'dbCmd.CommandText = sSql
                'dbDa = New OleDb.OleDbDataAdapter(dbCmd)

                'With OleDbDA
                '    .SelectCommand.Parameters.Clear()
                '    .SelectCommand.Parameters.Add("@param1", OleDb.OleDbType.VarChar).Value = rsBcno
                '    .SelectCommand.Parameters.Add("@param2", OleDb.OleDbType.VarChar).Value = rsBcno
                'End With

                'dt.Reset()
                'OleDbDA.Fill(dt)


                fnGet_Xpert_Comment = dt
                'alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                'alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                'DbCommand()
                'Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function
        '20220222 jhs igra comment 추가
        Public Function fnGet_IGRA_Comment(ByVal rsBcno As String, Optional ByVal RsCriticalGbn As Boolean = False) As DataTable
            '해당 환자의 igra 검사 최근 2년 검사결과 가져오는 쿼리 함수
            Dim sFn As String = "Public Function fnGet_IGRA_Comment(ByVal rsBcno As String, Optional ByVal RsCriticalGbn As Boolean = False) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList

            Dim dbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran

            Try
                sSql = ""
                sSql += " SELECT r.regno ,r.testcd , r.spccd ,  r.tkdt , r.bcno                                                                                                                 " + vbCrLf
                sSql += " , decode(r.rstflg , '1' , TO_CHAR(TO_DATE(r.regdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd'), '3' , TO_CHAR(TO_DATE(r.fndt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd')) fndt " + vbCrLf
                sSql += " , replace(replace(r.viewrst, chr(10), '' ), chr(13), '')  viewrst                                                                                                     " + vbCrLf
                sSql += " FROM lr010m r,                                                                                                 " + vbCrLf
                sSql += " (SELECT regno , tclscd , tkdt , bcno                                                                           " + vbCrLf
                sSql += "    FROM lj011m                                                                                                 " + vbCrLf
                sSql += "   WHERE bcno = :bcno                                                                                           " + vbCrLf
                'sSql += "     AND tclscd in ('LI611','LI612', 'LI613')                                                                   " + vbCrLf '결핵검사 코드 3가지
                sSql += "     AND tclscd in (SELECT CLSVAL FROM LF000M WHERE CLSGBN = 'IGRA' AND CLSCD LIKE 'T%')                                                                   " + vbCrLf '결핵검사 코드 3가지
                sSql += "     ) x , lf030m f3                                                                                            " + vbCrLf
                sSql += " WHERE r.regno = x.regno                                                                                        " + vbCrLf
                'sSql += "   AND r.testcd in ('LI611','LI612', 'LI613')                                                                   " + vbCrLf
                sSql += "   AND r.testcd in (SELECT CLSVAL FROM LF000M WHERE CLSGBN = 'IGRA' AND CLSCD LIKE 'T%')                                                                   " + vbCrLf
                sSql += "   AND r.tkdt BETWEEN TO_CHAR(TO_DATE(x.tkdt , 'yyyy-mm-dd hh24:mi:ss') - 730, 'yyyymmddhh24miss') AND x.tkdt   " + vbCrLf
                sSql += "   And nvl(r.viewrst, ' ') <> ' '                                                                               " + vbCrLf
                sSql += "   AND r.bcno NOT IN (:bcno)                                                                                    " + vbCrLf
                sSql += "   And r.spccd = f3.spccd                                                                                       " + vbCrLf
                sSql += "   AND f3.usdt <= r.tkdt                                                                                        " + vbCrLf
                sSql += "   AND f3.uedt > r.tkdt                                                                                         " + vbCrLf
                sSql += " GROUP BY  r.regno , r.testcd, r.spccd, r.tkdt, r.bcno                                                          " + vbCrLf
                sSql += "    , decode(r.rstflg , '1' , TO_CHAR(TO_DATE(r.regdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd'), '3' , TO_CHAR(TO_DATE(r.fndt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd'))     " + vbCrLf
                sSql += "    , r.viewrst                                                                                                  " + vbCrLf
                sSql += "    , f3.spcnms                                                                                                  " + vbCrLf
                sSql += " ORDER BY r.tkdt                                                                                                 " + vbCrLf

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                End With

                dt.Reset()
                dbDa.Fill(dt)

                fnGet_IGRA_Comment = dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        '20220222 jhs igra comment 추가
        Public Function fnGet_Pat_Recent_Rst(ByVal rsRegNo As String) As DataTable
            '해당 환자의 igra 검사 최근 2년 검사결과 가져오는 쿼리 함수
            Dim sFn As String = "Public Shared Function fnGet_Xpert_Comment(ByVal rsBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim alParm As New ArrayList

            Dim dbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter

            dbCmd.Connection = m_dbCn
            dbCmd.Transaction = m_dbTran

            Try
                sSql += "SELECT y.* , TO_CHAR(TO_DATE(y.rstdt, 'yyyy-mm-dd hh24:miss'), 'YYYY-MM-DD') rstdtd, "
                sSql += "       f6.testcd, f6.rstunit "
                sSql += "  FROM ( "
                sSql += "        SELECT ROW_NUMBER() OVER(PARTITION BY x.testcd || x.spccd ORDER BY x.tkdt desc) num, x.* "
                sSql += "          FROM ("
                sSql += "                SELECT testcd, spccd, orgrst, viewrst, MAX(tkdt) tkdt, MAX(rstdt) rstdt "
                sSql += "                  FROM lr010m "
                sSql += "                 WHERE regno = :regno"
                sSql += "                   AND testcd IN ('LH101', 'LH12103') "
                sSql += "                   AND spccd = 'S01' "
                sSql += "                   AND rstflg IN ('2', '3') "
                sSql += "                GROUP BY testcd, spccd, orgrst, viewrst "
                sSql += "                ) x"
                sSql += "        ) y "
                sSql += "       , lf060m f6 "
                sSql += "  WHERE y.num = 1 "
                sSql += "    AND y.testcd = f6.testcd "
                sSql += "    AND y.spccd = f6.spccd "
                sSql += "    AND f6.usdt <= TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS')"
                sSql += "    AND f6.uedt > TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS')"
                sSql += "    ORDER BY f6.testcd"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function


        '-- 검사분류별 자동 소견 등록
        Public Function fnEdit_LR040M_OUT(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_LR040M(Object) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable
                Dim dt2 As New DataTable

                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                Dim sTableNm As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "Select r.testcd,  c.cmt , r.partcd , r.slipcd"
                sSql += "  FROM " + sTableNm + " r , lf086m c , lf060m f"
                sSql += " WHERE r.bcno = :bcno"
                sSql += "   AND r.testcd = c.testcd "
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd = f.spccd"
                sSql += "   AND f.usdt <= r.tkdt"
                sSql += "   AND f.uedt > r.tkdt"
                sSql += "   AND f.exlabyn = '1'"
                sSql += "   AND f.exlabcd = '006'" 'SCL위탁기관만 소견 들어가도록 추가
                sSql += "   AND rownum = 1"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then Return ""

                sSql = ""
                sSql += "SELECT nvl(max(rstseq),'0') rstseq"
                sSql += "  FROM lr040m"
                sSql += " WHERE bcno = :bcno"

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                dt2.Reset()
                dbDa.Fill(dt2)



                sSql = ""
                sSql += "INSERT INTO lr040m"
                sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt )"
                sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("partcd", OracleDbType.Varchar2).Value = dt.Rows(0).Item("partcd").ToString
                    .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = dt.Rows(0).Item("slipcd").ToString
                    .Parameters.Add("rstno", OracleDbType.Int32).Value = CInt(dt2.Rows(0).Item("rstseq").ToString) + 1
                    .Parameters.Add("cmt", OracleDbType.Varchar2).Value = dt.Rows(0).Item("cmt").ToString

                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                End With

                If dbCmd.ExecuteNonQuery() < 1 Then Return "Error"

                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '<<<20180116
        '-- 특수보고서 소견 등록
        Public Function fnEdit_LR040M_SP(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "fnEdit_LR040M_SP(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList

                Dim iRstseq As Integer = 0
                Dim sRsttxt As String = ""

                Dim sPartcd As String = ""
                Dim sSlipcd As String = ""
                Dim iCnt As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql = ""
                sSql += "select r.bcno , r.partcd , r.SLIPCD , nvl(max(c.rstseq)  , '0')   rstseq "
                sSql += "  from lr010m r  , lr040m c "
                sSql += " where r.bcno = :bcno "
                sSql += "   and r.bcno = C.BCNO (+) "
                sSql += "   and r.partcd = c.partcd (+) "
                sSql += "   and r.slipcd = c.slipcd (+) "
                sSql += " group by r.bcno , r.partcd , r.slipcd "

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
                    iRstseq = CInt(dt.Rows(0).Item("rstseq").ToString)
                    sPartcd = dt.Rows(0).Item("partcd").ToString
                    sSlipcd = dt.Rows(0).Item("slipcd").ToString
                End If

                'sSql = ""
                'sSql += "SELECT bcno, rsttxt "
                'sSql += "  FROM lrs10m"
                'sSql += " WHERE bcno = :bcno "

                'dbCmd.CommandType = CommandType.Text
                'dbCmd.CommandText = sSql

                'dbDa = New OracleDataAdapter(dbCmd)

                'With dbDa
                '    .SelectCommand.Parameters.Clear()
                '    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                'End With

                'dt.Reset()
                'dbDa.Fill(dt)

                '<<< Comment 편집 
                If r_rstinfo_Buf.TestCd = "LH311" Or r_rstinfo_Buf.TestCd = "LH371" Then

                    Dim sTextCmt As String = ""
                    Dim ipos1 As Integer = r_rstinfo_Buf.RstTXT.IndexOf("□ RED")

                    If ipos1 > -1 Then
                        Dim sTemp As String = r_rstinfo_Buf.RstTXT
                        sTextCmt = r_rstinfo_Buf.RstTXT.Substring(ipos1)
                    End If

                    Dim ipos2 As Integer = sTextCmt.IndexOf("보고일자")

                    If ipos2 > -1 Then
                        sRsttxt = "     " + sTextCmt.Substring(0, ipos2)
                    End If
                    sRsttxt = sRsttxt.Replace(Chr(10), vbCrLf)

                End If

                If dt.Rows.Count > 0 Then

                    sSql = ""
                    sSql += "DELETE lr040m"
                    sSql += " WHERE  bcno = :bcno "

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                        iCnt = .ExecuteNonQuery()

                    End With


                    sSql = ""
                    sSql += "INSERT INTO lr040m"
                    sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt )"
                    sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("partcd", OracleDbType.Varchar2).Value = sPartcd
                        .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = sSlipcd
                        .Parameters.Add("rstno", OracleDbType.Int32).Value = iRstseq + 1
                        .Parameters.Add("cmt", OracleDbType.Varchar2).Value = sRsttxt

                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    End With


                    If dbCmd.ExecuteNonQuery() < 1 Then Return "Error"

                End If

                Return ""

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '<JJH 2020-02-11 특수보고서 결과값 저장(이전결과 가져오기위해)
        Public Function fnEdit_LRS17M_BfRst(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "fnEdit_LRS17M_BfRst(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As String"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList

                Dim sRsttxt As String = ""

                Dim iCnt As Integer = 0

                Dim sRst As String = ""
                Dim sRstdt As String = ""
                Dim sRstid As String = ""
                Dim sRstflg As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql = ""
                sSql += "select a.rst, a.rstdt, a.rstid, b.rstflg"
                sSql += "  from lrs17m a, lj011m b "
                sSql += " where a.bcno   = :bcno "
                sSql += "   and a.testcd = :testcd "
                sSql += "   and a.bcno   = b.bcno "
                sSql += "   and a.testcd = b.tclscd "

                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then

                    sRst = dt.Rows(0).Item("rst").ToString
                    sRstdt = dt.Rows(0).Item("rstdt").ToString
                    sRstid = dt.Rows(0).Item("rstid").ToString
                    sRstflg = dt.Rows(0).Item("rstflg").ToString

                    sSql = ""
                    sSql += " UPDATE LRS17M "
                    sSql += "    SET RSTDT  = :RSTDT  "
                    sSql += "       ,RSTID  = :RSTID  "
                    sSql += "       ,REGDT  = :REGDT  "
                    sSql += "       ,REGID  = :REGID  "

                    If r_sampinfo_Buf.BfRst <> sRst And r_sampinfo_Buf.BfRst <> "" Then
                        sSql += "       ,RST  = :RST  "
                    End If

                    sSql += "  WHERE BCNO   = :BCNO   "
                    sSql += "    AND TESTCD = :TESTCD "

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString.Trim
                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString.Trim
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID

                        If r_sampinfo_Buf.BfRst <> sRst And r_sampinfo_Buf.BfRst <> "" Then
                            .Parameters.Add("rst", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BfRst
                        End If

                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    End With

                    If dbCmd.ExecuteNonQuery() < 1 Then Return "Error"

                Else

                    sSql = ""
                    sSql += " INSERT INTO LRS17M "
                    sSql += "             ( BCNO,  TESTCD,  SPCCD,  REGNO,  RST,  RSTDT,  RSTID,  REGDT,  REGID ) "
                    sSql += "      VALUES (:BCNO, :TESTCD, :SPCCD, :REGNO, :RST, :RSTDT, :RSTID, :REGDT, :REGID ) "
                    '                                                     결과값
                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("spccd").ToString.Trim
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("regno").ToString.Trim
                        .Parameters.Add("rst", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BfRst
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString.Trim
                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString.Trim
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    End With

                    If dbCmd.ExecuteNonQuery() < 1 Then Return "Error"
                End If


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
                sSql += " UPDATE lj011m SET colldt = :rstdt, tkdt = :rstdt, rstdt = :rstdt, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
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
                sSql += " UPDATE lr010m SET"
                sSql += "        tkdt   = :rstdt, wkdt = rstdt, wkymd = :wkymd,"
                sSql += "        regdt  = DECODE(NVL(regdt, ' '),   ' ',  NULL, :regdt),"
                sSql += "        mwdt   = DECODE(NVL(mwdt, ' '),    ' ',  NULL, :mwdt), "
                sSql += "        fndt   = DECODE(NVL(fndt, ' '),    ' ',  NULL, :fndt),"
                sSql += "        rstdt  = DECODE(NVL(rstflg, '0'), '0',   NULL, :rstflg),"
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
                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = rsRstDate
                    .Parameters.Add("fndt", OracleDbType.Varchar2).Value = rsRstDate
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

        Private Function fnEdit_LR_CSM(ByVal rsSignKey As String, ByVal rsSignText As String) As Integer
            Dim sFn As String = "'Private Function fnEdit_LR_CSM(String, String) As Integer"

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                sSql = ""
                sSql += "INSERT INTO ccesignt (  sign_key, editdate,  sign_text)"
                sSql += "              VALUES ( :sign_key, SYSDATE,  :sign_text)"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("sign_key", OracleDbType.Varchar2).Value = rsSignKey
                    .Parameters.Add("sign_text", OracleDbType.LongRaw).Value = rsSignText
                End With

                Return dbCmd.ExecuteNonQuery()

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_OCS(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean
            Dim sFn As String = "Private Function fnEdit_OCS(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean"

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Dim strErrVal As String = ""

            Try

                '-- 감염정보 등록
                Dim sSql As String = "pro_ack_exe_ocs_rst_inf"
                If r_sampinfo_Buf.BCNo.Substring(8, 1) = PRG_CONST.BCCLS_MicorBio.Item(0).ToString.Substring(0, 1) Then
                    sSql = "pro_ack_exe_ocs_rst_inf_mb"
                End If

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
                    .Parameters("rs_retval").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Or strErrVal.IndexOf("no data") > 0 Then

                Else
                    Return False
                End If

                '-- OCS에 결과 올리기
                sSql = "pro_ack_exe_ocs_rst"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then
                    sSql = "pro_ack_exe_ocs_rst_m"
                End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("rs_editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Then
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
                    .Parameters("rs_errmsg").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Then
                    Return True
                Else
                    Return False
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '[2022.03.25] JJH 코로나 SMS 전송 서비스
        Public Function fnEdit_Covid_SMS(ByVal r_bcno As String,
                                         ByRef r_dbCn As OracleConnection, ByRef r_dbTran As OracleTransaction, Optional ByVal r_ifSrv As Boolean = False) As Boolean
            '[r_ifSrv]
            '> True  = I/F 결과저장 서비스
            '> False = 그 외 (default)
            Dim sFn As String = "Public Function fnEdit_Covid_SMS(ByVal r_bcno As String, ByRef r_dbCn As OracleConnection, ByRef r_dbTran As OracleTransaction, Optional ByVal r_ifSrv As Boolean = False) As Boolean"

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Dim strErrVal As String = ""

            Try

                '1. 코로나 SMS전송 검체조회
                Dim sSql As String = ""

                sSql = ""
                sSql += " select                                            "
                sSql += "        '031' as INSTCD                            "
                sSql += "      , a.REGNO                                    "
                sSql += "      , c.ORDDD                                    "
                sSql += "      , c.CRETNO                                   "
                sSql += "      , b.IOGBN                                    "
                sSql += "   from LJ010M a, LJ011M b, EMR.MMODEXOP c         "
                sSql += "  where a.BCNO     = :BCNO                         "
                sSql += "    and a.rstflg   = '2'                           "
                sSql += "    and a.BCNO     = b.BCNO                        "
                sSql += "    and c.INSTCD   = '031'                         "
                sSql += "    and b.REGNO    = c.PID                         "
                sSql += "    and b.ORDDT    = c.PRCPDD                      "
                sSql += "    and b.OCS_KEY  = c.EXECPRCPUNIQNO              "
                sSql += "  group by a.REGNO, c.ORDDD, c.CRETNO, b.IOGBN    "

                sSql += " union                                             "

                sSql += " select                                            "
                sSql += "        '031' as INSTCD                            "
                sSql += "      , a.REGNO                                    "
                sSql += "      , c.ORDDD                                    "
                sSql += "      , c.CRETNO                                   "
                sSql += "      , b.IOGBN                                    "
                sSql += "   from LJ010M a, LJ011M b, EMR.MMODEXIP c         "
                sSql += "  where a.BCNO     = :BCNO                         "
                sSql += "    and a.rstflg   = '2'                           "
                sSql += "    and a.BCNO     = b.BCNO                        "
                sSql += "    and c.INSTCD   = '031'                         "
                sSql += "    and b.REGNO    = c.PID                         "
                sSql += "    and b.ORDDT    = c.PRCPDD                      "
                sSql += "    and b.OCS_KEY  = c.EXECPRCPUNIQNO              "
                sSql += "  group by a.REGNO, c.ORDDD, c.CRETNO, b.IOGBN    "

                dbCmd.Connection = r_dbCn
                dbCmd.Transaction = r_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_bcno
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then
                    Return True
                End If

                Dim sInstcd As String = dt.Rows(0).Item("instcd").ToString()
                Dim sPid As String = dt.Rows(0).Item("regno").ToString()
                Dim sOrddd As String = dt.Rows(0).Item("orddd").ToString()
                Dim sCretno As String = dt.Rows(0).Item("cretno").ToString()
                Dim sIogbn As String = dt.Rows(0).Item("iogbn").ToString()

                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@")
                Console.WriteLine($"instcd = {sInstcd}")
                Console.WriteLine($"regno  = {sPid}")
                Console.WriteLine($"prcpdd = {sOrddd}")
                Console.WriteLine($"cretno = {sCretno}")
                Console.WriteLine($"sIogbn = {sIogbn}")
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@")


                '2. 코로나 SMS 전송 프로시저 (전산실제공)
                sSql = "lisif.pc_emr_covidexam_smssend"

                dbCmd = New OracleCommand()
                Dim iRet As Integer = 0

                With dbCmd
                    .Connection = r_dbCn
                    .Transaction = r_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_instcd", OracleDbType.Varchar2).Value = sInstcd '기관코드 
                    .Parameters.Add("rs_pid", OracleDbType.Varchar2).Value = sPid '등록번호
                    .Parameters.Add("rs_orddd", OracleDbType.Varchar2).Value = sOrddd '진료일자
                    .Parameters.Add("rs_cretno", OracleDbType.Varchar2).Value = sCretno '원무접수번호
                    .Parameters.Add("rs_iogbn", OracleDbType.Varchar2).Value = sIogbn '입외구분
                    .Parameters.Add("rs_userid", OracleDbType.Varchar2).Value = USER_INFO.USRID '로그인사용자
                    .Parameters.Add("rs_resendyn", OracleDbType.Varchar2).Value = "N"  '재전송여부 (검사결과시는 무조건 N)

                    .Parameters.Add("out_code", OracleDbType.Int32) '7
                    .Parameters("out_code").Direction = ParameterDirection.Output
                    .Parameters.Add("out_msg", OracleDbType.Varchar2, 4000) '8
                    .Parameters("out_msg").Direction = ParameterDirection.Output

                    iRet = .ExecuteNonQuery()

                    Dim outCode As String = .Parameters(7).Value.ToString()
                    Dim outMsg As String = .Parameters(8).Value.ToString()

                    Console.WriteLine("*************************************")
                    Console.WriteLine($"outCode = {outCode}")
                    Console.WriteLine("outCode => 1 정상전송, 0 전송못함, -1 프로그램오류")
                    Console.WriteLine($"outMsg  = {outMsg}")
                    Console.WriteLine("*************************************")

                End With

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_OCS_NCOV(ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal dbCn As OracleConnection, ByVal dbTran As OracleTransaction) As Boolean
            Dim sFn As String = "Private Function fnEdit_OCS(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean"

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Dim strErrVal As String = ""

            Try

                '-- 감염정보 등록
                Dim sSql As String = "pro_ack_exe_ocs_rst_inf"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("rs_usrip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_retval").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Or strErrVal.IndexOf("no data") > 0 Then

                Else
                    Return False
                End If

                '-- OCS에 결과 올리기
                sSql = "pro_ack_exe_ocs_rst"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("rs_editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Then
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
                    .Parameters("rs_errmsg").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Then
                    Return True
                Else
                    Return False
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_OCS_RegRst(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean
            Dim sFn As String = "Private Function fnEdit_OCS(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean"

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Dim strErrVal As String = ""

            Try

                '-- 감염정보 등록
                Dim sSql As String = "pro_ack_exe_ocs_rst_inf"
                'If r_sampinfo_Buf.BCNo.Substring(8, 1) = PRG_CONST.BCCLS_MicorBio.Item(0).ToString.Substring(0, 1) Then
                '    sSql = "pro_ack_exe_ocs_rst_inf_mb"
                'End If

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
                    .Parameters("rs_retval").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Or strErrVal.IndexOf("no data") > 0 Then

                Else
                    Return False
                End If

                '-- OCS에 결과 올리기
                sSql = "pro_ack_exe_ocs_rst"
                'If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then
                '    sSql = "pro_ack_exe_ocs_rst_m"
                'End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("rs_editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                    .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                    .Parameters("rs_errmsg").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Then
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
                    .Parameters("rs_errmsg").Value = strErrVal

                    .ExecuteNonQuery()

                    strErrVal = .Parameters(3).Value.ToString
                End With

                If strErrVal.StartsWith("00") Then
                    Return True
                Else
                    Return False
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function RegServer_Multi_Image(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList, ByVal rbSpecialTest As Boolean, ByVal raRTF_ARR As ArrayList) As Integer
            Dim sFn As String = "Function RegServer"

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim rstinfo_Buf As STU_RstInfo
                Dim rstinfo_Buf2 As STU_RstInfo

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
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i - 1), STU_RstInfo_cvt))
                            Next
                        End If
                    End If
                Catch ex As Exception
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 결과값 자동변환 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                End Try

                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Battery
                fnEdit_LR_Battery(r_sampinfo_Buf)

                '4) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) LRS10M
                If rbSpecialTest Then
                    Dim iRegErr_Sum As Integer = 0

                    For i As Integer = 1 To r_al_RstInfo.Count
                        For j As Integer = 1 To raRTF_ARR.Count '2019/11 추가 이미지 갯수만큼 count
                            rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                            If rstinfo_Buf.TestCd.Length = 5 And raRTF_ARR(j - 1).ToString <> "" Then
                                If fnEdit_LRS10_MutiImage(rstinfo_Buf, r_sampinfo_Buf, raRTF_ARR(j - 1).ToString, j) Then
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
                    Next

                    If r_al_EditSuc.Count = 0 Or iRegErr_Sum > 0 Then
                        m_dbTran.Rollback()
                        Return iRegOK_Sum
                    End If

                    '6) LRG10M
                    'sbEdit_LRG10M(rstinfo_Buf, r_sampinfo_Buf)
                End If


                '7) Upate LR040M(검사분류별 소견)
                Call fnEdit_LR040M(r_sampinfo_Buf) '-- 자동소견

                '<<<20180424 위탁검사 자동소견 
                If rstinfo_Buf.RegStep = "3" Then
                    fnEdit_LR040M_OUT(r_sampinfo_Buf)
                End If


                '<<<20180116 특수검사 소견 입력
                If rstinfo_Buf.RegStep = "3" Then
                    If rstinfo_Buf.TestCd = "LH311" Or rstinfo_Buf.TestCd = "LH371" Then

                        Dim bPb As Boolean = False
                        Dim bMal As Boolean = False

                        Dim dt As DataTable = fnGet_BcNo_Testcds(r_sampinfo_Buf.BCNo)

                        For i As Integer = 0 To dt.Rows.Count - 1

                            If dt.Rows(i).Item("testcd").ToString() = "LH311" Then
                                bPb = True
                            ElseIf dt.Rows(i).Item("testcd").ToString() = "LH371" Then
                                bMal = True
                            End If

                        Next

                        If bPb And bMal Then
                            If rstinfo_Buf.TestCd = "LH311" Then
                                Call fnEdit_LR040M_SP(rstinfo_Buf, r_sampinfo_Buf) '-- 자동소견
                            End If
                        Else
                            Call fnEdit_LR040M_SP(rstinfo_Buf, r_sampinfo_Buf) '-- 자동소견
                        End If


                    End If
                End If

                'Call fnEdit_LR020M(r_sampinfo_Buf) '-- 자동소견

                '8) -- 2009-09-15 YEJ (감염정보)
                Call fnEdit_OCS(r_sampinfo_Buf)

                '9) 종합검증 처방생성 (수가 생성) '선생님컨펌전까지 막기:20140423
                If rstinfo_Buf.RegStep = "3" Then
                    If rstinfo_Buf.TestCd = "LV101" Then
                        Call sbGv_hit(r_sampinfo_Buf)
                    End If
                End If

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

        '<--JJH AJEIJFKJDIJEJKFJD
        Public Function RegServer(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList, ByVal rbSpecialTest As Boolean, Optional ByVal SpecialBfRst As Boolean = False) As Integer
            Dim sFn As String = "Function RegServer"

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim rstinfo_Buf As STU_RstInfo
                Dim rstinfo_Buf2 As STU_RstInfo

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
                                fnEdit_LR_Item_Edit_View(r_sampinfo_Buf.BCNo, CType(al_RstInfo_Cvt(i - 1), STU_RstInfo_cvt))
                            Next
                        End If
                    End If
                Catch ex As Exception
                    LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 결과값 자동변환 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                End Try

                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Battery
                fnEdit_LR_Battery(r_sampinfo_Buf)

                '4) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) LRS10M
                If rbSpecialTest Then
                    Dim iRegErr_Sum As Integer = 0

                    For i As Integer = 1 To r_al_RstInfo.Count
                        rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                        If rstinfo_Buf.TestCd.Length = 5 And rstinfo_Buf.RstRTF.ToString <> "" Then
                            If fnEdit_LRS10_MutiImage(rstinfo_Buf, r_sampinfo_Buf, rstinfo_Buf.RstRTF, 0) Then
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

                    '6) LRG10M
                    'sbEdit_LRG10M(rstinfo_Buf, r_sampinfo_Buf)
                End If


                '7) Upate LR040M(검사분류별 소견)
                Call fnEdit_LR040M(r_sampinfo_Buf) '-- 자동소견

                '<<<20180424 위탁검사 자동소견 
                If rstinfo_Buf.RegStep = "3" Then
                    fnEdit_LR040M_OUT(r_sampinfo_Buf)
                End If


                '<<<20180116 특수검사 소견 입력
                If rstinfo_Buf.RegStep = "3" Then
                    If rstinfo_Buf.TestCd = "LH311" Or rstinfo_Buf.TestCd = "LH371" Then

                        Dim bPb As Boolean = False
                        Dim bMal As Boolean = False

                        Dim dt As DataTable = fnGet_BcNo_Testcds(r_sampinfo_Buf.BCNo)

                        For i As Integer = 0 To dt.Rows.Count - 1

                            If dt.Rows(i).Item("testcd").ToString() = "LH311" Then
                                bPb = True
                            ElseIf dt.Rows(i).Item("testcd").ToString() = "LH371" Then
                                bMal = True
                            End If

                        Next

                        If bPb And bMal Then
                            If rstinfo_Buf.TestCd = "LH311" Then
                                Call fnEdit_LR040M_SP(rstinfo_Buf, r_sampinfo_Buf) '-- 자동소견
                            End If
                        Else
                            Call fnEdit_LR040M_SP(rstinfo_Buf, r_sampinfo_Buf) '-- 자동소견
                        End If


                    End If
                End If

                '-- 2020-02-11 JJH 특수보고서 결과 관리
                If SpecialBfRst Then
                    Call fnEdit_LRS17M_BfRst(rstinfo_Buf, r_sampinfo_Buf)
                End If


                'Call fnEdit_LR020M(r_sampinfo_Buf) '-- 자동소견

                '8) -- 2009-09-15 YEJ (감염정보)
                Call fnEdit_OCS(r_sampinfo_Buf)

                '9) 종합검증 처방생성 (수가 생성) '선생님컨펌전까지 막기:20140423
                If rstinfo_Buf.RegStep = "3" Then
                    If rstinfo_Buf.TestCd = "LV101" Then
                        Call sbGv_hit(r_sampinfo_Buf)
                    End If
                End If

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

        Public Function RegServer(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList, _
                                  ByVal rsPoctFlg As String, ByVal rsRstDate As String) As Integer
            Dim sFn As String = "Function RegServer"

            Dim strFkOcs As String = r_sampinfo_Buf.BCNo
            Dim strBcNo As String = ""
            Dim strRet As String = ""

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim rstinfo_Buf As STU_RstInfo

                If r_al_EditSuc Is Nothing Then r_al_EditSuc = New ArrayList

                '0) Cn, Transaction 생성
                'm_dbCn = GetDbConnection()
                'm_dbTran = m_dbCn.BeginTransaction()

                '-- 2009/11/19 yej 수정 (검체번호 길이로 구분하여 처리)
                If r_sampinfo_Buf.BCNo.Length = 11 Then strBcNo = fnGetBCPrtToView(r_sampinfo_Buf.BCNo)

                If strBcNo <> "" Then
                    strRet = fnEdit_TK(strBcNo, r_sampinfo_Buf.UsrID, r_sampinfo_Buf.UsrIP)
                Else
                    strBcNo = fnEdit_Coll_TK(r_sampinfo_Buf.BCNo, r_sampinfo_Buf.UsrID, r_sampinfo_Buf.UsrIP)
                    strRet = strBcNo
                End If

                If strRet = "" Then
                    m_dbTran.Rollback()

                    If strBcNo.Length <> 15 Then Return iRegOK_Sum
                End If

                r_sampinfo_Buf.BCNo = strBcNo

                '1) 결과개수만큼 등록
                For i As Integer = 1 To r_al_RstInfo.Count
                    rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                    If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                        iRegOK_Sum += 1

                        r_al_EditSuc.Add(rstinfo_Buf.TestCd)
                    End If
                Next

                If r_al_EditSuc.Count = 0 Then
                    m_dbTran.Rollback()
                    Return iRegOK_Sum
                End If


                '3) Parent
                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Battery
                fnEdit_LR_Battery(r_sampinfo_Buf)

                '4) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '5) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                '6) Upate LR040M(검사분류별 소견)
                Call fnEdit_LR040M(r_sampinfo_Buf) '-- 자동소견

                '-- POCT 장비결과인 경우는 장비에서 검사한 일자를 채혈,접수,결과일자로 설정
                If rsPoctFlg = "P" And rsRstDate <> "" Then
                    Call fnEdit_Change_CollAndTkAndRst_date(r_sampinfo_Buf, rsRstDate)
                End If

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

        '-- 2009/11/19 YEJ 추가
        '-- POCT (채혈만 했을 경우 사용) 때문에 추가
        Private Function fnGetBCPrtToView(ByVal rsBcNo As String) As String
            Dim sFn As String = "Function fnGetBCPrtToView(String) As String"

            Dim sSql As String = ""
            Dim dt As New DataTable

            Try
                If Not rsBcNo.Length.Equals(11) Then Return ""

                sSql = "SELECT bcno FROM lj010m WHERE bcno = fn_get_bcno_from_prtbcno('" + rsBcNo + "') AND spcflg IN ('1', '2')"

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

        '-- 2007-10-16 경희에서 추가
        Public Function RegServerM(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Public Function RegServerM( ArrayList, STU_SampleInfo, ByRef ArrayList) As Integer"

            Dim ri As STU_RstInfo
            Dim al_bac As New ArrayList
            Dim al_anti As New ArrayList

            Dim regrstM As New APP_M.RegFn

            Try

                For i As Integer = 1 To r_al_RstInfo.Count
                    ri = New STU_RstInfo

                    ri = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                    Dim sTestCd As String = ri.TestCd
                    Dim sSpcCd As String = regrstM.fnGetBacInfo_IF_SpcCd(r_sampinfo_Buf)
                    Dim sOrgRst As String = ri.OrgRst.Replace(PRG_CONST.CdSep2, Convert.ToChar(124)).Replace("¶", Convert.ToChar(3).ToString)

                    Dim sBacCd As String = ""
                    Dim sBacSeq As String = ""

                    Dim bi As ResultInfo_Bac
                    Dim ai As ResultInfo_Anti

                    If sOrgRst.IndexOf(Convert.ToChar(3)) < 0 And sOrgRst.IndexOf(Convert.ToChar(124)) >= 0 Then
                        '> <ETX>없이 <FLD>만 있는 경우 +<ETX>
                        sOrgRst += Convert.ToChar(3)
                    End If

                    If sOrgRst.IndexOf(Convert.ToChar(3)) < 0 Then
                        '> 미생물 일반결과
                        Exit For
                    End If

                    For j As Integer = 1 To sOrgRst.Split(Convert.ToChar(3)).Length
                        Dim sMicro As String = sOrgRst.Split(Convert.ToChar(3))(j - 1)
                        Dim a_sBacInfo As String()
                        Dim a_sAntiInfo As String()

                        If sMicro = "" Then Exit For

                        If sMicro.Substring(0, 1) = "O" Then
                            bi = New ResultInfo_Bac

                            bi.TestCd = sTestCd
                            bi.SpcCd = sSpcCd

                            a_sBacInfo = sMicro.Split(Convert.ToChar(124))

                            If bi.SpcCd = "" Then
                                Return 0
                            End If

                            bi.BacSeq = a_sBacInfo(1)
                            bi.BacCd = a_sBacInfo(2)
                            bi.IncRst = a_sBacInfo(3)
                            bi.TestMtd = "M"

                            If regrstM.fnGetBacInfo_IF(bi) = False Then
                                Return 0
                            End If

                            sBacCd = bi.BacCd
                            sBacSeq = bi.BacSeq

                            If regrstM.fnGetBacInfo_IF_PrePos(r_sampinfo_Buf, bi) Then
                                Return 0
                            End If

                            al_bac.Add(bi)

                        ElseIf sMicro.Substring(0, 1) = "A" Then
                            ai = New ResultInfo_Anti

                            ai.TestCd = sTestCd
                            ai.SpcCd = sSpcCd
                            ai.BacCd = sBacCd
                            ai.BacSeq = sBacSeq

                            a_sAntiInfo = sMicro.Split(Convert.ToChar(124))

                            ai.AntiCd = a_sAntiInfo(1)

                            If regrstM.fnGetAntiInfo_IF(ai) = False Then
                                Return 0
                            End If

                            ai.AntiRst = a_sAntiInfo(2)
                            ai.DecRst = a_sAntiInfo(3)

                            ai.TestMtd = "M"

                            al_anti.Add(ai)

                        End If

                        If sMicro.Substring(0, 1) = "O" Then
                            bi = Nothing

                        ElseIf sMicro.Substring(0, 1) = "A" Then
                            ai = Nothing

                        End If
                    Next

                    If al_bac.Count > 0 Then
                        If CType(al_bac(0), ResultInfo_Bac).BacGenCd = FixedVariable.gsBacGenCd_Nogrowth Then
                            ri.OrgRst = FixedVariable.gsRst_Nogrowth
                        Else
                            ri.OrgRst = FixedVariable.gsRst_Growth
                        End If
                    End If

                    ri = Nothing
                Next

                regrstM.al_Bac = al_bac
                regrstM.al_Anti = al_anti

                Dim iRegOK_M As Integer = regrstM.RegServer(r_al_RstInfo, r_sampinfo_Buf, r_al_EditSuc)

                Return iRegOK_M

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function RegServerEP(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Function RegServerEP"

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim epInfo_Buf As STU_RstInfo_ep
                Dim iR As Integer = -1

                'Log 남기기
                '< mod freety 2005/03/18
                '# 한 프로세스에 멀티장비용으로 수정
                'RegRstFn.Log("RegServer 시작 - " + r_sampinfo_Buf.BCNo)
                LogFn.Log(r_sampinfo_Buf.SenderID, "RegServerEP 시작 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                '> mod freety 2005/03/18

                '1) 결과개수만큼 등록
                For intIx1 As Integer = 1 To r_al_RstInfo.Count
                    epInfo_Buf = CType(r_al_RstInfo(intIx1 - 1), STU_RstInfo_ep)
                    If epInfo_Buf.RstGbn = "T" Then
                        Dim rsInfo_Buf As New STU_RstInfo

                        rsInfo_Buf.TestCd = epInfo_Buf.TestCd
                        rsInfo_Buf.OrgRst = "{null}" 'epInfo_Buf.Rst1
                        rsInfo_Buf.RstCmt = ""

                        If fnRegServer(rsInfo_Buf, r_sampinfo_Buf) Then
                            iRegOK_Sum += 1

                            r_al_EditSuc.Add(epInfo_Buf.TestCd)
                        End If
                    End If

                    If intIx1 = 1 Then
                        '1) Select Rst Info
                        sbGetRstInfo(r_sampinfo_Buf.BCNo)
                        If fnEdit_LRG10M(0, epInfo_Buf, r_sampinfo_Buf) = 0 Then Return 0
                        fnEdit_LRI10H(0)
                    End If

                    If fnEdit_Item_LRI10M(0, epInfo_Buf) <> 0 Then
                        iRegOK_Sum += 1
                        'r_al_EditSuc.Add(epInfo_Buf.FrNm)
                    End If
                Next

                If r_al_EditSuc.Count = 0 Then Return iRegOK_Sum

                '2) Sub 항목 에 대한 상태 재조정(Parent 및 Child)
                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '4) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                'Log 남기기
                LogFn.Log(r_sampinfo_Buf.SenderID, "RegServerEP 종료 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)

                Return iRegOK_Sum

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Function RegServerIFE(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Function RegServerEP"

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim epInfo_Buf As STU_RstInfo_ep
                Dim iR As Integer = -1

                'Log 남기기
                '< mod freety 2005/03/18
                '# 한 프로세스에 멀티장비용으로 수정
                'RegRstFn.Log("RegServer 시작 - " + r_sampinfo_Buf.BCNo)
                LogFn.Log(r_sampinfo_Buf.SenderID, "RegServerEP 시작 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                '> mod freety 2005/03/18

                '1) 결과개수만큼 등록
                For intIx1 As Integer = 1 To r_al_RstInfo.Count
                    epInfo_Buf = CType(r_al_RstInfo(intIx1 - 1), STU_RstInfo_ep)

                    If epInfo_Buf.RstGbn = "ELP" Or r_al_RstInfo.Count = 1 Then
                        '1) Select Rst Info
                        sbGetRstInfo(r_sampinfo_Buf.BCNo)
                    End If

                    If epInfo_Buf.RstGbn = "ELP" Then sbEdit_LRG20H(0, epInfo_Buf, r_sampinfo_Buf)

                    If fnEdit_LRG20M(0, epInfo_Buf, r_sampinfo_Buf) = 0 Then Return (0)

                    Dim rsInfo_Buf As New STU_RstInfo

                    rsInfo_Buf.TestCd = epInfo_Buf.TestCd
                    rsInfo_Buf.OrgRst = "{null}" 'epInfo_Buf.Rst1
                    rsInfo_Buf.RstCmt = ""

                    If fnRegServer(rsInfo_Buf, r_sampinfo_Buf) Then
                        iRegOK_Sum += 1
                        r_al_EditSuc.Add(epInfo_Buf.TestCd)
                    End If
                Next

                If r_al_EditSuc.Count = 0 Then Return iRegOK_Sum

                '2) Sub 항목 에 대한 상태 재조정(Parent 및 Child)
                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Update LJ011M
                fnEdit_LJ011(r_sampinfo_Buf)

                '4) Update LJ010M
                fnEdit_LJ010(r_sampinfo_Buf)

                Return iRegOK_Sum

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Private Function fnCDate(ByVal rsDate As String) As String
            If IsDate(rsDate) Then
                Return "to_date('" + Format(Convert.ToDateTime(rsDate), "yyyyMMddHHmmss") + "', 'yyyy-mm-dd hh24:mi:ss')"
            Else
                Return "''"
            End If
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
                sSql += "  FROM lj011m j"
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
                sSql += "UPDATE lj010m SET rstflg = :rstflg"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND spcflg = '4'"
                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstflg
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                    iRet = .ExecuteNonQuery()
                End With

                Return 1
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function
        Private Function fnGet_Bm_Mal() As Boolean

            Dim sFn As String = "Private Function fnGet_Bm_Mal() As Boolean"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                sSql = ""
                sSql += " SELECT count(*) cnt " + vbCrLf
                sSql += "   FROM lj011m " + vbCrLf
                sSql += "  WHERE bcno = :bcno " + vbCrLf
                sSql += "    AND testcd in ('LH311','LH371')" + vbCrLf
                

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

                Dim sTable As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += "SELECT r.tclscd, r.spccd, MIN(NVL(r.rstflg, '0')) minrstflg, MAX(NVL(r.rstflg, '0')) maxrstflg, MAX(r.rstdt) rstdt"
                sSql += "  FROM " + sTable + " r, lf060m f"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   AND r.tkdt  <  f.uedt"
                sSql += "   AND (f.tcdgbn IN ('S', 'P') OR (f.tcdgbn = 'B' AND NVL(f.titleyn, '0') = '0'))"
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
                    'If dt.Rows(ix - 1).Item("minrstflg").ToString() = dt.Rows(ix - 1).Item("maxrstflg").ToString() Then
                    '    sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    'ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString() = "0" And dt.Rows(ix - 1).Item("maxrstflg").ToString() <= "3" Then
                    '    sRstFlg = "1"
                    'Else
                    '    sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    'End If

                    '> 2016-11-14 윤장열 인터페이스에서 중간보고로 넘어올 떄 상태값 버그 수정
                    If dt.Rows(ix - 1).Item("minrstflg").ToString() = dt.Rows(ix - 1).Item("maxrstflg").ToString Then
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString
                    ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString = "0" And dt.Rows(ix - 1).Item("maxrstflg").ToString = "3" Then
                        sRstFlg = "2"
                    ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString = "0" And dt.Rows(ix - 1).Item("maxrstflg").ToString <= "2" Then
                        sRstFlg = dt.Rows(ix - 1).Item("maxrstflg").ToString
                    Else
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString
                    End If

                    sSql = ""

                    Select Case sRstFlg
                        Case "0"
                            sSql += "UPDATE lj011m SET rstflg = NULL, rstdt = NULL"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"
                            sSql += "   AND spcflg = '4'"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()
                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("tclscd").ToString()
                            End With

                        Case Else
                            sSql += "UPDATE lj011m SET rstflg = :rstflg, rstdt = :rstdt"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"
                            sSql += "   AND spcflg = '4'"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()
                                .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("rstdt").ToString()
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
                'Dim sCM As String = fnEdit_LR_CM(iR, r_rstinfo_Buf.OrgRst) '<<<20180803
                Dim sCM As String = fnEdit_LR_CM(iR, r_rstinfo_Buf.OrgRst, r_sampinfo_Buf.BCNo, r_rstinfo_Buf.TestCd).Trim

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
                        If r_sampinfo_Buf.EqCd <> "" Then
                            If sDM <> "" Or sAM <> "" Or sPM <> "" Or sCM <> "" Then
                                .RegStep = "1"
                            Else
                                .RegStep = "3"
                            End If
                        Else
                            If sDM <> "" Or sAM <> "" Then
                                .RegStep = "1"
                            ElseIf sPM <> "" Or sCM <> "" Or m_b_SpecialTest Then
                                .RegStep = "2"
                            Else
                                .RegStep = "3"
                            End If
                        End If
                    ElseIf r_sampinfo_Buf.RegStep = "22" Then
                        .RegStep = "2"
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

        Public Function RegNcov(ByVal rsPrtno As String, ByVal rsTestGbn As String, ByVal Orgrst As String, ByVal rsUsrid As String, ByVal rsDate As String) As Integer
            Dim sFn As String = "Private Function RegNcov(string, string, string) As Integer"

            Try
                Fn.log(rsPrtno + "|" + rsTestGbn + "|" + Orgrst + "|" + rsUsrid + "|" + rsDate)

                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable


                sSql = ""
                sSql += " SELECT fn_ack_sysdate() as dt, A.BCNO, A.TESTCD, A.SPCCD, A.REGNO, B.GBN, B.RST   "
                sSql += "   FROM LR010M A                                                                     "
                sSql += "   LEFT OUTER JOIN LRS18M B                                                          "
                sSql += "                ON A.BCNO   = B.BCNO                                                 "
                sSql += "               AND A.TESTCD = B.TESTCD                                               "
                sSql += "               AND A.SPCCD  = B.SPCCD                                                "
                sSql += "               AND B.GBN    = :gbn                                                   "
                sSql += "  WHERE A.BCNO = FN_ACK_GET_BCNO_NORMAL(:prtno)                                      "
                sSql += "    AND NVL(A.RSTFLG, '0') <> '3'                                                    "

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    .SelectCommand.Parameters.Add("prtno", OracleDbType.Varchar2).Value = rsPrtno
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count <= 0 Then Return 0

                Dim iRet As Integer = 0

                Dim Gbn As String = dt.Rows(0).Item("gbn").ToString
                Dim Rst As String = dt.Rows(0).Item("rst").ToString
                Dim Bcno As String = dt.Rows(0).Item("bcno").ToString
                Dim Nowdt As String = dt.Rows(0).Item("dt").ToString

                '<< 결과가 존재할때
                If Gbn <> "" Then

                    sSql = ""
                    sSql += "SELECT MAX(NVL(TO_NUMBER(SEQ), 0)) + 1 AS SEQ "
                    sSql += "  FROM LRS18H "
                    sSql += " WHERE BCNO = :BCNO "
                    sSql += "   AND GBN  = :GBN "

                    dbCmd.CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    With dbDa
                        .SelectCommand.Parameters.Clear()
                        .SelectCommand.Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .SelectCommand.Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    End With

                    dt.Reset()
                    dbDa.Fill(dt)

                    Dim SEQ As String = IIf(dt.Rows(0).Item("seq").ToString = "", "1", dt.Rows(0).Item("seq").ToString).ToString

                    sSql = ""
                    sSql += " INSERT INTO LRS18H "
                    sSql += "             SELECT A.*, :SEQ, fn_ack_sysdate(), :USRID "
                    sSql += "               FROM LRS18M A"
                    sSql += "              WHERE A.BCNO = :BCNO "
                    sSql += "                AND A.GBN  = :GBN "

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("seq", OracleDbType.Varchar2).Value = SEQ
                        .Parameters.Add("usrid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    End With

                    iRet += dbCmd.ExecuteNonQuery()


                    Fn.log("U")

                    sSql = ""
                    sSql += " UPDATE LRS18M "
                    sSql += "    SET RST    = :RST, "
                    sSql += "        RSTDT  = :RSTDT ,"
                    sSql += "        RSTID  = :RSTID "
                    sSql += "  WHERE BCNO   = :BCNO "
                    sSql += "    AND GBN    = :GBN"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("rst", OracleDbType.Varchar2).Value = Orgrst
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = IIf(rsDate = "", Nowdt, rsDate).ToString
                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn
                    End With

                    iRet += dbCmd.ExecuteNonQuery()

                Else '<< 결과가 존재하지않을때

                    Fn.log("I")

                    sSql = ""
                    sSql += " INSERT INTO LRS18M  "
                    sSql += "             ( BCNO,  TESTCD,  SPCCD,  REGNO,  RST,  RSTDT,  RSTID,  REGDT,  REGID,   GBN )  "
                    sSql += "      VALUES (:BCNO, :TESTCD, :SPCCD, :REGNO, :RST, :RSTDT, :RSTID, :REGDT, :REGID,  :GBN )  "

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = Bcno
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt.Rows(0).Item("testcd").ToString
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = dt.Rows(0).Item("spccd").ToString
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = dt.Rows(0).Item("regno").ToString
                        .Parameters.Add("rst", OracleDbType.Varchar2).Value = Orgrst
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = IIf(rsDate = "", Nowdt, rsDate).ToString
                        .Parameters.Add("rstid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = IIf(rsDate = "", Nowdt, rsDate).ToString
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = rsUsrid
                        .Parameters.Add("gbn", OracleDbType.Varchar2).Value = rsTestGbn

                    End With

                    iRet += dbCmd.ExecuteNonQuery()

                End If

                m_dbTran.Commit()

                Return 1
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
                Else
                    sRst = rsOrgRst
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

        'Private Function fnEdit_LR_CM(ByVal riR As Integer, ByVal rsOrgRst As String) As String
        '    Dim sFn As String = "Private Function fnEdit_LR_CM(Integer, String) As String"

        '    Try
        '        Dim sCriticalGbn As String = m_dt_rst.Rows(riR).Item("criticalgbn").ToString().Trim

        '        If sCriticalGbn Is Nothing Then Return ""

        '        rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

        '        Dim sCriticalL As String = m_dt_rst.Rows(riR).Item("criticall").ToString().Trim
        '        Dim sCriticalH As String = m_dt_rst.Rows(riR).Item("criticalh").ToString().Trim
        '        Dim sRefL As String = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
        '        Dim sRefH As String = m_dt_rst.Rows(riR).Item("refh").ToString().Trim

        '        'CriticalGbn : 0 --> 사용안함, 1 --> 하한만 사용,    2 --> 상한만 사용,    3 --> 모두 사용
        '        '                           4 --> 하한만 사용(%), 5 --> 하한만 사용(%), 6 --> 모두 사용(%)
        '        Select Case sCriticalGbn
        '            Case "1"
        '                If IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sCriticalL) Then
        '                    Return "C"
        '                End If

        '            Case "2"
        '                If IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sCriticalH) Then
        '                    Return "C"
        '                End If

        '            Case "3"
        '                If IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sCriticalL) Then
        '                    Return "C"
        '                End If

        '                If IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sCriticalH) Then
        '                    Return "C"
        '                End If

        '            Case "4"
        '                If IsNumeric(sRefL) And IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sRefL) * (1 + Val(sCriticalL) / 100) Then
        '                    Return "C"
        '                End If

        '            Case "5"
        '                If IsNumeric(sRefH) And IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sRefH) * (1 + Val(sCriticalH) / 100) Then
        '                    Return "C"
        '                End If

        '            Case "6"
        '                If IsNumeric(sRefL) And IsNumeric(sCriticalL) And Val(rsOrgRst) < Val(sRefL) * (1 + Val(sCriticalL) / 100) Then
        '                    Return "C"
        '                End If

        '                If IsNumeric(sRefH) And IsNumeric(sCriticalH) And Val(rsOrgRst) > Val(sRefH) * (1 + Val(sCriticalH) / 100) Then
        '                    Return "C"
        '                End If

        '        End Select

        '        Return ""
        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        '    End Try

        'End Function
        Private Function fnEdit_LR_CM(ByVal riR As Integer, ByVal rsOrgRst As String, Optional ByVal rsBcno As String = "", Optional ByVal rsTestcd As String = "") As String
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
                    Case "7"
                        'Critical 문자값 판단 추가(검사마스터에서 Critical 구분 [7] 문자결과(결과코드 설정) 선택, 기초마스터 결과코드에 Critical 설정한 경우 )
                        Dim sTxtCritical As String = ""

                        sTxtCritical = fnGet_GraedValue_C(rsTestcd, rsOrgRst)

                        If rsTestcd = "LG104" Then 'xpert pcr 검사가 Critical이라도 해당 환자의 1주일전 pcr검사 이력이 Deteted(Critical)일 경우 Normal결과로 판단
                            Dim dt As DataTable = fnGet_Xpert_Comment(rsBcno, True)

                            Fn.log(sFn + " - 1")
                            If dt.Rows.Count > 0 Then
                                Return ""
                            ElseIf dt.Rows.Count <= 0 Then

                                Fn.log(sFn + " - 2")
                                Return sTxtCritical
                            End If
                        Else
                            '임시막음
                            'Return sTxtCritical
                        End If

                End Select

                Return ""
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        '<<<20180803 크리티컬 보고 문자
        Private Function fnEdit_LR_CM2(ByVal riR As Integer, ByVal rsOrgRst As String, Optional ByVal rsTestcd As String = "") As String
            Dim sFn As String = "Private Function fnEdit_LR_CM(Integer, String) As String"

            Try
                Dim sCriticalGbn As String = m_dt_rst.Rows(riR).Item("criticalgbn").ToString().Trim

                If sCriticalGbn Is Nothing Then Return ""

                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                Dim sCriticalL As String = m_dt_rst.Rows(riR).Item("criticall").ToString().Trim
                Dim sCriticalH As String = m_dt_rst.Rows(riR).Item("criticalh").ToString().Trim
                Dim sRefL As String = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
                Dim sRefH As String = m_dt_rst.Rows(riR).Item("refh").ToString().Trim
                Dim sTestcd As String = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                'CriticalGbn : 0 --> 사용안함, 1 --> 하한만 사용,    2 --> 상한만 사용,    3 --> 모두 사용
                '                           4 --> 하한만 사용(%), 5 --> 하한만 사용(%), 6 --> 모두 사용(%) , 7--> 문자
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
                    Case "7"

                        Return fnGet_GraedValue_C(rsTestcd, rsOrgRst) '<<<결과코드에서 크리티컬로 설정되어 있으면 크리티컬 

                End Select

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function
        Private Function fnGet_GraedValue_C(ByVal rsTclsCd As String, ByVal rsRstVal As String) As String
            Dim sFn As String = "Private Function fnGet_GraedValue(String, String) As String"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As New OracleDataAdapter
                Dim dt As New DataTable
                Dim sSql As String = ""
                Dim sXpertRst As String = ""

                Dim sValue As String = ""

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                If rsTclsCd = "LG104" Then
                    If rsRstVal.IndexOf(Chr(13)) > 0 Then
                        sXpertRst = rsRstVal.Substring(0, rsRstVal.IndexOf(Chr(13)))
                    Else
                        sXpertRst = rsRstVal
                    End If
                End If

                sSql = ""
                sSql += "SELECT crtval FROM lf083m"
                sSql += " WHERE testcd  = :testcd"
                sSql += "   AND spccd   = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "'"

                If rsTclsCd = "LG104" Then
                    sSql += "    AND lower(replace(rstcont, ' ', '')) LIKE lower(replace('" + sXpertRst + "' , ' ', ''))||'%'"
                Else
                    sSql += "    AND rstcont = :rstcont"
                End If

                'sSql += "   and rstcont = :rstcont"

                dbCmd.CommandText = sSql
                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                    If rsTclsCd <> "LG104" Then .SelectCommand.Parameters.Add("rstcont", OracleDbType.Varchar2).Value = rsRstVal
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then sValue = dt.Rows(0).Item(0).ToString()

                Return sValue

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

                Dim sCfmSign As String = ""

                If r_rstinfo_Buf.CfmSignRst <> "" Then
                    sCfmSign += m_dt_rst.Rows(riR).Item("regno").ToString().Trim + "|"
                    sCfmSign += r_sampinfo_Buf.UsrID + "|"
                    sCfmSign += m_dt_rst.Rows(riR).Item("curdt").ToString().Trim + "|"
                    sCfmSign += "LRS10M" + "|"
                    sCfmSign += m_dt_rst.Rows(riR).Item("bcno").ToString().Trim + "|"
                    sCfmSign += m_dt_rst.Rows(riR).Item("testcd").ToString().Trim + "|"

                    If fnEdit_LR_CSM(sCfmSign, r_rstinfo_Buf.CfmSignRst) = 0 Then Return 0
                End If


                sSql = ""
                sSql += "UPDATE lr010m SET"
                sSql += "       orgrst       = :orgrst,"              '--  1)
                sSql += "       viewrst      = :viewrst,"              '--  2)    
                sSql += "       deltamark    = :deltamark,"              '--  3)
                sSql += "       panicmark    = :panicmark,"              '--  4)
                sSql += "       criticalmark = :criticalmark,"              '--  5)
                sSql += "       alertmark    = :alertmark,"              '--  6)
                sSql += "       hlmark       = :hlmark,"              '--  7)    
                sSql += "       regid        = :regid,"              '--  8)
                sSql += "       regdt        = :regdt,"              '--  9)
                sSql += "       mwid         = :mwid,"              '-- 10)
                sSql += "       mwdt         = :mwdt,"              '-- 11)
                sSql += "       fnid         = :fnid,"              '-- 12)
                sSql += "       fndt         = :fndt,"              '-- 13)
                sSql += "       cfmnm        = :cfmnm,"              '-- 14)
                'sSql += "       cfmsign      = :cfmsign,"              '-- 15)
                'sSql += "       cfmyn        = 'N',"
                sSql += "       rstflg       = :rstflg,"              '-- 16)
                sSql += "       rstdt        = :rstdt,"              '-- 17)
                sSql += "       rstcmt       = :rstcmt,"              '-- 18)
                sSql += "       bfbcno       = :bfbcno,"              '-- 19)
                sSql += "       bffndt       = :bffndt,"              '-- 20)
                sSql += "       bforgrst     = :bforgrst,"              '-- 21)
                sSql += "       bfviewrst    = :bfviwrst,"              '-- 22)
                If r_sampinfo_Buf.EqCd <> "" Then
                    sSql += "       eqcd         = :eqcd,"          '-- 23)    
                    sSql += "       eqseqno      = :eqseqno,"          '-- 24)
                    sSql += "       eqrack       = :eqrack,"          '-- 25)
                    sSql += "       eqpos        = :eqpos,"          '-- 26)
                    sSql += "       eqbcno       = :eqbcno,"          '-- 27)
                    sSql += "       eqflag       = :eqflag,"          '-- 28)
                End If
                sSql += "       fregdt = CASE WHEN  NVL(fregdt, ' ') = ' ' THEN fn_ack_sysdate ELSE fregdt END,"
                sSql += "       editdt = fn_ack_sysdate,"
                sSql += "       editid = :editid,"                    '-- 30)
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
                            '.Parameters.Add("cfmsign",  OracleDbType.Varchar2).Value = DBNull.Value
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
                            '.Parameters.Add("cfmsign",  OracleDbType.Varchar2).Value = DBNull.Value

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
                            '.Parameters.Add("cfmsign",  OracleDbType.Varchar2).Value = sCfmSign
                    End Select

                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RegStep
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString


                    If r_rstinfo_Buf.RstCmt Is Nothing Then r_rstinfo_Buf.RstCmt = ""
                    .Parameters.Add("rstcmt", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstCmt

                    '이전결과
                    .Parameters.Add("bfbcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bfbcno_b").ToString().Trim
                    .Parameters.Add("bffndt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bffndt_b").ToString.Trim
                    .Parameters.Add("bforgrst", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bforgrst_b").ToString().Trim
                    .Parameters.Add("bfviwrst", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bfviewrst_b").ToString().Trim

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
                sSql += "INSERT INTO lr011m"
                sSql += "       ("
                sSql += "        bcno, testcd, spccd, tclscd, orgrst, viewrst, rstcmt, deltamark, panicmark, criticalmark,"
                sSql += "        alertmark, hlmark, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg,"
                sSql += "        rstdt, bfbcno, bffndt, eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, sysdt, editdt, editid, editip, seq"
                sSql += "       ) "
                sSql += "SELECT bcno, testcd, spccd, tclscd, orgrst, viewrst, rstcmt, deltamark, panicmark, criticalmark,"
                sSql += "       alertmark, hlmark, regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg,"
                sSql += "       rstdt, bfbcno, bffndt, eqcd, eqseqno, eqrack, eqpos, eqbcno, eqflag, :moddt, editdt, editid, editip, sq_lr011m.nextval"
                sSql += "  FROM lr010m"
                sSql += " WHERE bcno    = :bcno"
                sSql += "   AND testcd  = :testcd"
                sSql += "   AND NVL(rstdt, ' ') <> ' '"
                sSql += "   AND (NVL(orgrst, '" + r_rstinfo_Buf.OrgRst + "') <> '" + r_rstinfo_Buf.OrgRst + "' OR NVL(viewrst, '" + r_rstinfo_Buf.ViewRst + "') <> '" + r_rstinfo_Buf.ViewRst + "')"
                sSql += "   AND NVL(orgrst,  ' ') <> ' '"
                sSql += "   AND NVL(viewrst, ' ') <> ' '"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("moddt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString.Trim

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                End With

                dbCmd.ExecuteNonQuery()

                Dim sCfmSign As String = ""

                If r_rstinfo_Buf.CfmSignRst <> "" Then
                    sCfmSign += m_dt_rst.Rows(riR).Item("regno").ToString().Trim + "|"
                    sCfmSign += r_sampinfo_Buf.UsrID + "|"
                    sCfmSign += m_dt_rst.Rows(riR).Item("curdt").ToString().Trim + "|"
                    sCfmSign += "LRS10M" + "|"
                    sCfmSign += m_dt_rst.Rows(riR).Item("bcno").ToString().Trim + "|"
                    sCfmSign += m_dt_rst.Rows(riR).Item("testcd").ToString().Trim + "|"

                    If fnEdit_LR_CSM(sCfmSign, r_rstinfo_Buf.CfmSignRst) = 0 Then Return 0
                End If

                'Update
                sSql = ""
                sSql += "UPDATE lr010m SET"
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
                'sSql += "       cfmsign      = :cfmsign,"
                'sSql += "       cfmyn        = 'N',"
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
                sSql += "       fregdt = CASE WHEN NVL(fregdt, ' ') = ' ' THEN fn_ack_sysdate ELSE fregdt END"
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
                            '.Parameters.Add("cfmsign",  OracleDbType.Varchar2).Value = DBNull.Value
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
                            '.Parameters.Add("cfmsign",  OracleDbType.Varchar2).Value = DBNull.Value
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
                            '.Parameters.Add("cfmsign",  OracleDbType.Varchar2).Value = sCfmSign
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
                sSql += "UPDATE lr010m SET"
                sSql += "       viewrst = :viewrst,"
                sSql += "       rstcmt  = :rstcmt"
                sSql += " WHERE bcno    = :bcno"
                sSql += "   AND testcd  = :testcd"

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

                Dim sTable As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, "
                sSql += "       MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, MAX(NVL(r.eqcd, '')) eqcd,"
                sSql += "       SUBSTR(r.testcd, 1, 5) testcd, r.spccd"
                sSql += "  FROM " + sTable + " r, lf060m f"
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
                    Dim sEqCd As String = dt_p.Rows(ix).Item("eqcd").ToString

                    If sRstFlg = "3" Then

                        Dim a_dr As DataRow() = m_dt_rst.Select("testcd = '" + dt_p.Rows(ix).Item("testcd").ToString + "'", "")

                        sSql = ""
                        sSql += "UPDATE " + sTable + " SET"
                        If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                            sSql += "       eqcd    = :eqcd,"
                            sSql += "       eqseqno = :eqseqno,"
                            sSql += "       eqrack  = :eqrack,"
                            sSql += "       eqpos   = :eqpos,"
                            sSql += "       eqbcno  = :eqbcno,"
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
                        sSql += "        (testcd, spccd, '1') IN "
                        sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
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
                            If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                .Parameters.Add("eqbcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqBCNo
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
                                sSql += "UPDATE " + sTable + " SET"
                                If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                    sSql += "       eqcd    = :eqcd,"
                                    sSql += "       eqseqno = :eqseqno,"
                                    sSql += "       eqrack  = :eqrack,"
                                    sSql += "       eqpos   = :eqpos,"
                                    sSql += "       eqbcno  = :eqbcno,"
                                End If

                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NULL,               mwdt = NULL,"
                                sSql += "       fnid   = NULL,               fndt = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND testcd LIKE :testcd || '%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '1') IN "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
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
                                    If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                        .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                        .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                        .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                        .Parameters.Add("eqbcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqBCNo
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
                                sSql += "UPDATE " + sTable + " SET"
                                If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                    sSql += "       eqcd    = :eqcd,"
                                    sSql += "       eqseqno = :eqseqno,"
                                    sSql += "       eqrack  = :eqrack,"
                                    sSql += "       eqpos   = :eqpos,"
                                    sSql += "       eqbcno  = :eqbcno,"
                                End If

                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                sSql += "       fnid   = NULL,  fndt = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editip,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bnco"
                                sSql += "   AND testcd LIKE :testcd || '%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '1') IN "
                                sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
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
                                    If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                        .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                        .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                        .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                        .Parameters.Add("eqbcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqBCNo
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
                                    sSql += "UPDATE " + sTable + " SET"
                                    If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                        sSql += "       eqcd    = :eqcd,"
                                        sSql += "       eqseqno = :eqseqno,"
                                        sSql += "       eqrack  = :eqrack,"
                                        sSql += "       eqpos   = :eqpos,"
                                        sSql += "       eqbcno  = :eqbcno,"
                                    End If

                                    sSql += "       rstflg = :rstflg,"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                    sSql += "       mwid  = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                    sSql += "       fnid  = NULL,               fndt = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno   = :bcno"
                                    sSql += "   AND testcd LIKE :testcd || '%'"
                                    sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                    sSql += "        (testcd, spccd, '1') IN "
                                    sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
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
                                        If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                            .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                            .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                            .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                            .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                            .Parameters.Add("eqbcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqBCNo
                                        End If
                                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = "2"
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

                Dim sTable As String = "lr010m"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(r.rstdt) rstdt, r.tclscd, r.spccd"
                sSql += "  FROM " + sTable + " r, lf060m f, lf062m f62"
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
                        Dim a_dr As DataRow() = m_dt_rst.Select("tclscd = '" + dt_p.Rows(ix).Item("tclscd").ToString + "' AND rstdt = '" + dt_p.Rows(ix).Item("rstdt").ToString + "'", "")

                        sSql = ""
                        sSql += "UPDATE " + sTable + ""
                        sSql += "   SET rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :regid), regdt   = NVL(regdt, :regdt),"
                        sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt    = NVL(mwdt,  :mwdt),"
                        sSql += "       fnid   = NVL(fnid,  :fnid),  fndt    = :fndt,"
                        sSql += "       cfmnm  = :cfmnm,             cfmsign = :cfmsign,  cfmyn = 'N',"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND tclscd  = :testcd"
                        sSql += "   AND NVL(orgrst, ' ') <> ' '"
                        sSql += "   AND rstflg <> '3'"
                        sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                        sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = 1)"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

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
                                    sSql += "UPDATE " + sTable + ""
                                    sSql += "   SET rstflg = '" + r_sampinfo_Buf.RegStep + "',"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                    sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                    sSql += "       fnid   = NULL,               fndt  = NULL,"
                                    sSql += "       editdt = fn_ack_sysdate,"
                                    sSql += "       editid = :editid,"
                                    sSql += "       editip = :editip"
                                    sSql += " WHERE bcno    = :bcno"
                                    sSql += "   AND tclscd  = :tclscd"
                                    sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                    sSql += "   AND rstflg  = '3'"
                                    sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                    sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = 1)"

                                    dbCmd.CommandText = sSql

                                    With dbCmd
                                        .Parameters.Clear()
                                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                        .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                        .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                    End With

                                End If

                            Case "1"
                                sSql = ""
                                sSql += "UPDATE " + sTable + ""
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NULL,               mwdt  = NULL,"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno    = :bcno"
                                sSql += "   AND tclscd  = :tclscd"
                                sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = 1)"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                                End With

                            Case "2"
                                sSql = ""
                                sSql += "UPDATE " + sTable + " "
                                sSql += "   SET rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                sSql += "       fnid   = NULL,               fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND tclscd = :tclscd"
                                sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m WHERE grprstyn = 1)"


                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                    .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                    .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
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
                                    sViewRst = sFlag + Format(CStr(Val(sConvRst) - (10 ^ -Val(sLLen))), sFmt)
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
                    rsOldOrgRst = rsOldOrgRst.Replace(",", "").Trim()

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
                sSql += "INSERT INTO lrs10h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, bcno, testcd, rstflg, rsttxt, rstdt, rstid, migymd, editdt, editid, editip"
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


                '0) Delete lrs10m : 나중에 필요에 의해서 lrs10m도 History 관리할 경우 이것만 Remark 처리함
                sSql = ""
                sSql += " DELETE lrs10m"
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
                sSql += "INSERT INTO lrs10m("
                sSql += "             bcno,  testcd,  rstflg, /* rstrtf,  rsttxt,*/ rstdt,           rstid,  editid,  editip, editdt )"
                sSql += "    VALUES( :bcno, :testcd, :rstflg,/* :rstrtf, :rsttxt, */ fn_ack_sysdate, :rstid, :editid, :editip, fn_ack_sysdate )"


                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_sampinfo_Buf.RegStep.Substring(0, 1)
                    '.Parameters.Add("rstrtf", OracleDbType.Clob).Value = r_rstinfo_Buf.RstRTF
                    '.Parameters.Add("rsttxt",  OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstTXT
                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    iRet += .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "UPDATE lrs10m SET rstrtf = :rstrtf"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"


                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rstrtf", OracleDbType.Clob).Value = r_rstinfo_Buf.RstRTF
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd


                    iRet += .ExecuteNonQuery()
                End With

                'If r_rstinfo_Buf.TestCd = "LH311" Then

                '    Dim sTextCmt As String = ""
                '    Dim ipos1 As Integer = r_rstinfo_Buf.RstTXT.IndexOf("□ RED")

                '    If ipos1 > -1 Then
                '        Dim sTemp As String = r_rstinfo_Buf.RstTXT
                '        sTextCmt = r_rstinfo_Buf.RstTXT.Substring(ipos1)
                '    End If

                '    Dim ipos2 As Integer = sTextCmt.IndexOf("보고일자")

                '    If ipos2 > -1 Then
                '        sTextCmt = "     " + sTextCmt.Substring(0, ipos2)
                '    End If

                '    sTextCmt = sTextCmt.Replace(Chr(10), vbCrLf)

                '    sSql = ""
                '    sSql += "UPDATE lrs10m SET rsttxt = :rsttxt"
                '    sSql += " WHERE bcno   = :bcno"
                '    sSql += "   AND testcd = :testcd"

                '    With dbCmd
                '        .CommandText = sSql

                '        .Parameters.Clear()
                '        .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = sTextCmt
                '        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                '        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd

                '        iRet += .ExecuteNonQuery()
                '    End With

                'End If

                '-- 20090907 YEJ
                If r_rstinfo_Buf.AddFileNm1 <> "" Then
                    sSql = ""
                    sSql += "DELETE lrs12m WHERE bcno = :bcno AND testcd = :testcd"

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
                    sSql += "INSERT INTO lrs12m("
                    sSql += "             bcno,  testcd,  rstno,  filenm,  filelen,  filebin )"
                    sSql += "    VALUES( :bcno, :testcd,  1,     :fielnm, :filelen, :filebin )"

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
                    sSql += "INSERT INTO lrs12m(  bcno,  testcd,  rstno,  filenm,  filelen,  filebin )"
                    sSql += "            VALUES( :bcno, :testcd,      2, :filenm, :filelen, :filebin )"

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
        Private Function fnEdit_LRS10_MutiImage(ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal rsRTF_ARR As String, ByVal rnSeq As Integer) As Boolean
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
                sSql += "INSERT INTO lrs10h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, bcno, testcd, rstflg, rsttxt, rstdt, rstid,migymd, editdt, editid, editip"
                sSql += "  FROM lrs10m"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"
                'sSql += "   AND rtfseq = :rtfseq "

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    ' .Parameters.Add("rtfseq", OracleDbType.Int32).Value = rnSeq

                    iRet = .ExecuteNonQuery()
                End With


                '0) Delete lrs10m : 나중에 필요에 의해서 lrs10m도 History 관리할 경우 이것만 Remark 처리함
                sSql = ""
                sSql += " DELETE lrs10m"
                sSql += "  WHERE bcno   = :bcno"
                sSql += "    AND testcd = :testcd"
                sSql += "    AND migymd = :migymd "

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    .Parameters.Add("migymd", OracleDbType.Varchar2).Value = rnSeq

                    iRet += .ExecuteNonQuery()
                End With

                '-- 20090907 YEJ
                sSql = ""
                sSql += "INSERT INTO lrs10m("
                sSql += "             bcno,  testcd, migymd, rstflg, /* rstrtf,  rsttxt,*/ rstdt,           rstid,  editid,  editip, editdt )"
                sSql += "    VALUES( :bcno, :testcd, :migymd, :rstflg,/* :rstrtf, :rsttxt, */ fn_ack_sysdate, :rstid, :editid, :editip, fn_ack_sysdate )"


                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    .Parameters.Add("migymd", OracleDbType.Varchar2).Value = rnSeq
                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_sampinfo_Buf.RegStep.Substring(0, 1)
                    '.Parameters.Add("rstrtf", OracleDbType.Clob).Value = r_rstinfo_Buf.RstRTF
                    '.Parameters.Add("rsttxt",  OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstTXT
                    .Parameters.Add("rstid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    iRet += .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "UPDATE lrs10m SET rstrtf = :rstrtf"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"
                sSql += "   AND migymd = :migymd "

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("rstrtf", OracleDbType.Clob).Value = rsRTF_ARR
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    .Parameters.Add("migymd", OracleDbType.Varchar2).Value = rnSeq

                    iRet += .ExecuteNonQuery()
                End With

                'If r_rstinfo_Buf.TestCd = "LH311" Then

                '    Dim sTextCmt As String = ""
                '    Dim ipos1 As Integer = r_rstinfo_Buf.RstTXT.IndexOf("□ RED")

                '    If ipos1 > -1 Then
                '        Dim sTemp As String = r_rstinfo_Buf.RstTXT
                '        sTextCmt = r_rstinfo_Buf.RstTXT.Substring(ipos1)
                '    End If

                '    Dim ipos2 As Integer = sTextCmt.IndexOf("보고일자")

                '    If ipos2 > -1 Then
                '        sTextCmt = "     " + sTextCmt.Substring(0, ipos2)
                '    End If

                '    sTextCmt = sTextCmt.Replace(Chr(10), vbCrLf)

                '    sSql = ""
                '    sSql += "UPDATE lrs10m SET rsttxt = :rsttxt"
                '    sSql += " WHERE bcno   = :bcno"
                '    sSql += "   AND testcd = :testcd"

                '    With dbCmd
                '        .CommandText = sSql

                '        .Parameters.Clear()
                '        .Parameters.Add("rsttxt", OracleDbType.Varchar2).Value = sTextCmt
                '        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                '        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd

                '        iRet += .ExecuteNonQuery()
                '    End With

                'End If

                '-- 20090907 YEJ
                If r_rstinfo_Buf.AddFileNm1 <> "" Then
                    sSql = ""
                    sSql += "DELETE lrs12m WHERE bcno = :bcno AND testcd = :testcd"

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
                    sSql += "INSERT INTO lrs12m("
                    sSql += "             bcno,  testcd,  rstno,  filenm,  filelen,  filebin )"
                    sSql += "    VALUES( :bcno, :testcd,  1,     :fielnm, :filelen, :filebin )"

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
                    sSql += "INSERT INTO lrs12m(  bcno,  testcd,  rstno,  filenm,  filelen,  filebin )"
                    sSql += "            VALUES( :bcno, :testcd,      2, :filenm, :filelen, :filebin )"

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

        Private Function fnEdit_LRG10M(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo_ep, ByVal r_sampleInfo As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LRG10M(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo_ep) As Integer"

            Try
                Dim sSql As String = ""
                Dim iRet As Integer

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO lrg10h"
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.*"
                    sSql += "  FROM lrg10m r"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND eqcd   = :eqcd"

                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrIP
                    .Parameters.Add("modip ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrIP
                    .Parameters.Add("bcno  ".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                    .Parameters.Add("eqcd  ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.EqCd

                    iRet = .ExecuteNonQuery

                    If iRet = 0 Then
                        sSql = ""
                        sSql += "INSERT INTO lrg10m("
                        sSql += "             bcno,  testcd,  eqcd,  graphdata,  editid,  editip,  editdt)"
                        sSql += "    VALUES( :bcno, :testcd, :eqcd, :graphdata, :editid, :editip,  fn_ack_sysdate)"

                        .CommandText = sSql

                        .Parameters.Clear()

                        .Parameters.Add("bcno".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                        .Parameters.Add("testcd".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                        .Parameters.Add("eqcd".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.EqCd
                        .Parameters.Add("graphdata".ToString(), OracleDbType.Varchar2).Value = r_rstinfo_Buf.Graph
                        .Parameters.Add("editid".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrID
                        .Parameters.Add("editip".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrIP

                    Else
                        sSql = ""
                        sSql += "UPDATE lrg10m"
                        sSql += "   SET graphdata = :graphdata, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                        sSql += " WHERE bcno      = :bcno"
                        sSql += "   AND testcd    = :testcd"
                        sSql += "   AND eqcd      = :eqcd"

                        .CommandText = sSql
                        .Parameters.Clear()
                        .Parameters.Add("graphdata".ToString(), OracleDbType.Varchar2).Value = r_rstinfo_Buf.Graph
                        .Parameters.Add("editid".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrID
                        .Parameters.Add("editip".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrIP
                        .Parameters.Add("bcno  ".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                        .Parameters.Add("testcd".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                        .Parameters.Add("eqcd  ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.EqCd

                    End If
                    Return .ExecuteNonQuery()
                End With
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_LRG20M(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo_ep, ByVal r_sampleInfo As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LRG20M(Integer, STU_RstInfo_ep) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO lrg20m("
                    sSql += "             bcno,  testcd,  eqcd,  rstno,  graphdata,  editid,  editip, editdt)"
                    sSql += "    VALUES( :bcno, :testcd, :eqcd, :rstno, :graphdata, :editid, :editip, fn_ack_sysdate)"

                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("bcno  ".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                    .Parameters.Add("eqcd  ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.EqCd
                    .Parameters.Add("rstno ".ToString(), OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstGbn
                    .Parameters.Add("graphdata".ToString(), OracleDbType.Varchar2).Value = r_rstinfo_Buf.Graph
                    .Parameters.Add("editid".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrID
                    .Parameters.Add("editip ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrIP

                    Return .ExecuteNonQuery()
                End With

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Private Sub sbEdit_LRG20H(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo_ep, ByVal r_sampleInfo As STU_SampleInfo)
            Dim sFn As String = "Private Sub sbEdit_LRG20H(Integer, STU_RstInfo_ep)"

            Try
                Dim sSql As String = ""
                Dim intRet As Integer = 0

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO lrg20h"
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.*"
                    sSql += "  FROM lrg20m r"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND eqcd   = :eqcd"

                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("modid ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrID
                    .Parameters.Add("modip ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.UsrIP
                    .Parameters.Add("bcno  ".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd".ToString(), OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                    .Parameters.Add("eqcd  ".ToString(), OracleDbType.Varchar2).Value = r_sampleInfo.EqCd

                    intRet = .ExecuteNonQuery

                    sSql = ""
                    sSql += "DELETE lrg20m"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"
                    sSql += "   AND eqcd   = :eqcd"

                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                    .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampleInfo.EqCd

                    .ExecuteNonQuery()
                End With
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

        Private Function fnEdit_Item_LRI10M(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo_ep) As Integer
            Dim sFn As String = "Private Function fnEdit_Item_LRI10M(Integer, STU_RstInfo_ep) As Integer"

            Try
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO lri10m("
                    sSql += "             bcno,  testcd,  spccd,  frtno,  frtnm,  frtrst,  frtconc,  frthl,  frtref,  frtgbn)"
                    sSql += "    VALUES( :bcno, :testcd, :spccd, :frtno, :frtnm, :frtrst, :frtconc, :frthl, :frtref, :frtgbn)"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim
                    .Parameters.Add("spccd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("spccd").ToString().Trim
                    .Parameters.Add("frtno", OracleDbType.Varchar2).Value = r_rstinfo_Buf.FrNo
                    .Parameters.Add("frtnm", OracleDbType.Varchar2).Value = r_rstinfo_Buf.FrNm
                    .Parameters.Add("frtrst", OracleDbType.Varchar2).Value = r_rstinfo_Buf.Rst1
                    .Parameters.Add("frtconc", OracleDbType.Varchar2).Value = r_rstinfo_Buf.Rst2
                    .Parameters.Add("frthl", OracleDbType.Varchar2).Value = r_rstinfo_Buf.JudgMark
                    .Parameters.Add("frtref", OracleDbType.Varchar2).Value = r_rstinfo_Buf.Refrmk
                    .Parameters.Add("frtgbn", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstGbn

                    Return .ExecuteNonQuery

                End With
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LRI10H(ByVal riR As Integer) As Integer
            Dim sFn As String = "Private Function fnEdit_LRI10H(Integer) As Integer"

            Try
                Dim sSql As String = ""
                Dim intRet As Integer

                Dim dbCmd As New OracleCommand

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.Text

                    sSql = ""
                    sSql += "INSERT INTO lri10 "
                    sSql += "SELECT fn_ack_sysdate, r.*"
                    sSql += "  FROM lri10m"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    .CommandText = sSql
                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim

                    intRet = .ExecuteNonQuery

                    sSql = ""
                    sSql += "DELETE lri10m"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    .CommandText = sSql
                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString().Trim
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim

                    Return .ExecuteNonQuery()
                End With
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_Coll_TK(ByVal rsFkOcs As String, ByVal rsUsrId As String, ByVal rsUsrIP As String) As String
            Dim sFn As String = "Private Function fnEdit_Coll_TK(String, String) As String"

            Dim sSql As String = ""
            Dim dbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Dim oleDbParam As New OracleParameter  'New DBORA.DbParrameter

            Try

                Dim strRegNo As String = "", strIoGbn As String = "", strOrdDt As String = "", strRetVal As String = ""

                sSql = "pkg_ack_coll.pkg_get_order_fkcos"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("rs_fkocs", OracleDbType.Varchar2).Value = rsFkOcs
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    strRegNo = dt.Rows(0).Item("bunho").ToString()
                    strIoGbn = dt.Rows(0).Item("in_out_gubun").ToString()
                    strOrdDt = dt.Rows(0).Item("orddt").ToString()
                End If

                sSql = "pro_exe_collectTotake_poct"

                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("regno", OracleDbType.Varchar2, strRegNo.Length).Value = strRegNo : .Parameters("regno").Direction = ParameterDirection.Input
                    .Parameters.Add("orddt", OracleDbType.Varchar2, strOrdDt.Length).Value = strOrdDt : .Parameters("orddt").Direction = ParameterDirection.Input
                    .Parameters.Add("iogbn", OracleDbType.Varchar2, strIoGbn.Length).Value = strIoGbn : .Parameters("iogbn").Direction = ParameterDirection.Input
                    .Parameters.Add("fkocs", OracleDbType.Int64).Value = rsFkOcs : .Parameters("fkocs").Direction = ParameterDirection.Input
                    .Parameters.Add("usrid", OracleDbType.Varchar2, rsUsrId.Length).Value = rsUsrId : .Parameters("usrid").Direction = ParameterDirection.Input
                    .Parameters.Add("retval", OracleDbType.Varchar2, 2000).Value = strRetVal : .Parameters("retval").Direction = ParameterDirection.InputOutput

                    .ExecuteNonQuery()
                End With

                strRetVal = dbCmd.Parameters("retval").Value.ToString

                If strRetVal.StartsWith("00") Then

                    Return strRetVal.Substring(2).Trim
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_TK(ByVal rsBcNo As String, ByVal rsUsrId As String, ByVal rsUsrIp As String) As String
            Dim sFn As String = "Private Function fnEdit_TK(String, String) As String"

            Dim sSql As String = ""
            Dim dbCmd As New OracleCommand
            Dim dt As New DataTable

            Try

                Dim sRetVal As String = ""

                sSql = "pro_ack_exe_take_ocs"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2, rsBcNo.Length).Value = rsBcNo : .Parameters("bcno").Direction = ParameterDirection.Input
                    .Parameters.Add("rs_wknoyn", OracleDbType.Varchar2, rsUsrId.Length).Value = "" : .Parameters("usrid").Direction = ParameterDirection.Input
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2, rsUsrId.Length).Value = rsUsrId : .Parameters("usrid").Direction = ParameterDirection.Input
                    .Parameters.Add("rs_ip", OracleDbType.Varchar2, rsUsrId.Length).Value = rsUsrIp : .Parameters("usrid").Direction = ParameterDirection.Input
                    .Parameters.Add("rs_retval", OracleDbType.Varchar2, 2000).Value = sRetVal : .Parameters("retval").Direction = ParameterDirection.InputOutput

                    .ExecuteNonQuery()
                End With

                sRetVal = dbCmd.Parameters("rs_retval").Value.ToString

                If sRetVal.StartsWith("00") Then
                    Return "OK"
                Else
                    Return sRetVal.Substring(2).Trim
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

                '2) Update LR010M, Insert LR011M
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
                dbCmd.CommandText = "pkg_ack_rst.pkg_get_resultinfo"

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

        Private Sub sbGv_hit(ByVal r_sampinfo As STU_SampleInfo)
            Dim sFn As String = "sbGv_hit"

            Dim dt As DataTable = fnGv_Tk_Rows(r_sampinfo.BCNo.ToString) '접수대상자의 정보 가져오기(종합검증대상자 접수리스트 spdlist)
            Dim al_sucs As New ArrayList
            'Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            Try
#If DEBUG Then
                Dim sDeptInf As String = fnGet_Usr_Dept_info_new("210003")
#Else
                Dim sDeptInf As String = fnGet_Usr_Dept_info_new(USER_INFO.USRID)
#End If

                'For i As Integer = 1 To al_rows.Count
                'If spd.GetColFromID("regno") * spd.GetColFromID("baseday") = 0 Then
                '    MsgBox("Column ID 오류 발생!!")

                '    Return
                'End If

                Dim stu As New COMMON.SVar.STU_GVINFO

                stu.REGNO = dt.Rows(0).Item("regno").ToString  '환자번호

                stu.ORDCD = PRG_CONST.TEST_GV_ORDCD.Split("/"c)(0)
                stu.SUGACD = PRG_CONST.TEST_GV_ORDCD.Split("/"c)(1)

                If sDeptInf.IndexOf("/") >= 0 Then
                    stu.DEPTCD_USR = sDeptInf.Split("/"c)(0)
                    stu.DEPTNM_USR = sDeptInf.Split("/"c)(1)
                Else
                    stu.DEPTCD_USR = ""
                    stu.DEPTNM_USR = ""
                End If

                stu.SPCCD = PRG_CONST.SPC_GV
                stu.STATUS = "I,G"

                Dim sRet As String = (New WEBSERVER.CGWEB_G).ExecuteDo(stu)

                If sRet.StartsWith("00") Then
                    '성공
                Else
                    '실패
                    MsgBox("종합검증 수가발생에 실패하였습니다.")
                    Return
                End If
                'Next

            Catch ex As Exception
                MsgBox("종합검증에 실패하였습니다.")
                Return
            Finally

                'For i As Integer = al_sucs.Count To 1 Step -1
                '    spd.DeleteRows(Convert.ToInt32(al_sucs(i - 1)), 1)
                '    spd.MaxRows -= 1
                'Next

                'sbDisplay_ToDo_Today()

            End Try
        End Sub

        Private Function fnGv_Tk_Rows(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGetBCPrtToView(String) As String"

            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim dbcmd As New OracleCommand
            Dim dbda As OracleDataAdapter

            Try
                'If Not rsBcNo.Length.Equals(11) Then Return ""

                sSql = "SELECT regno, entdt FROM lj010m WHERE bcno = '" + rsBcNo + "'"

                'DbCommand()
                'dt = DbExecuteQuery(sSql)

                dbcmd.Connection = m_dbCn
                dbcmd.Transaction = m_dbTran
                dbcmd.CommandType = CommandType.Text
                dbcmd.CommandText = sSql

                dbda = New OracleDataAdapter(dbcmd)

                dt.Reset()
                dbda.Fill(dt)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_Usr_Dept_info_new(ByVal rsUsrId As String) As String
            Dim sFn As String = "fnGet_Usr_Dept_info"

            Try
                Dim dt As New DataTable
                Dim dbda As OracleDataAdapter
                Dim dbcmd As New OracleCommand

                'Dim m_dbCn As OracleConnection
                'Dim m_dbTran As OracleTransaction

                Dim sSql As String = "SELECT FN_ACK_GET_USR_DEPTINFO('" + rsUsrId + "') FROM DUAL"
                Dim al As New ArrayList

                'DbCommand()

                'al.Add(New OracleParameter("usrid", rsUsrId))
                'Dim dt As DataTable = DbExecuteQuery(sSql, al)

                dbcmd.Connection = m_dbCn
                dbcmd.Transaction = m_dbTran
                dbcmd.CommandType = CommandType.Text
                dbcmd.CommandText = sSql

                dbda = New OracleDataAdapter(dbcmd)

                dt.Reset()
                dbda.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString
                Else
                    Return "/"
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

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
