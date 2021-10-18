'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_M.vb                                                              */
'/* PartName     : 미생물 - 검체별/작업번호별 결과저장 및 보고                            */
'/* Description  : 미생물의 Data Query구문관련 Class                                      */
'/* Design       : 2003-07-10 Ju Jin Ho                                                   */
'/* Coded        :                                                                        */
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

' 미생물 데이터 접근 클래스
Namespace APP_M
    Public Class CommFn
        Private Const msFile As String = "File : CGLISAPP.vb, Class : LISAPP.APP_M.CommFn" + vbTab

        '-- 부서/분야별 검사대상자 조회
        Public Shared Function fnGet_SpcList_tk(ByVal rsPartSlip As String, ByVal rsTGrpCd As String, _
                                                  ByVal rsTkdtS As String, ByVal rsTkdtE As String, ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_SpcList_tgrp(string, string, string, string, string) as datatable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, j.patnm,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_full(r.workno) workno,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn," '20130910 정선영 추가, remark 표시하기 위해서
                sSql += "       CASE WHEN j.rstflg = '2' THEN 'Y' ELSE 'N' END rstflg"
                sSql += "  FROM lj010m j,"
                sSql += "       (SELECT r.bcno, MIN(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno, MIN(r.tkdt) tkdt,"
                sSql += "               MIN(NVL(r.rstflg, '0')) || MAX(NVL(r.rstflg, '0')) rstflg_t"
                sSql += "          FROM lm010m r, lf060m f"
                sSql += "         WHERE r.tkdt >= :dates"
                sSql += "           AND r.tkdt <= :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkdtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkdtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkdtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkdtE))

                If rsTGrpCd <> "" Then
                    sSql += "           AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    sSql += "           AND f.tcdgbn <> 'B'" '20140711 정선영 추가.
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                Else
                    sSql += "           AND f.partcd = :partcd"
                    sSql += "           AND f.slipcd = :slipcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += "           AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "           AND r.tkdt  >= f.usdt"
                sSql += "           AND r.tkdt  <  f.uedt"
                'sSql += "           AND NVL(f.titleyn, '0') = '0'"
                'sSql += "           AND (CASE WHEN f.tcdgbn = 'C' THEN NVL(f.reqsub, '0') ELSE '1' END = '1' OR NVL(r.orgrst, ' ') <> ' ')"
                'sSql += "           AND (CASE WHEN f.tcdgbn = 'C' THEN NVL(f.reqsub, '0') ELSE '1' END = '1' OR NVL(r.orgrst, ' ') <> ' ')"
                '<20140711 정선영 수정
                sSql += "                      AND ( (f.tcdgbn IN ('B', 'P') AND nvl(f.titleyn, '0') = '0') OR"
                sSql += "                            (CASE WHEN f.tcdgbn = 'C' THEN nvl(f.reqsub, '0') ELSE '1' END = '1' OR nvl(r.orgrst, ' ') <> ' ')"
                sSql += "                          )"
                '>
                sSql += "         GROUP BY bcno"
                sSql += "       ) r"
                sSql += " WHERE j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"

                Select Case rsRstFlg
                    Case "0"
                        sSql += "   AND r.rstflg_t = '00'"
                    Case "1"
                        sSql += "   AND r.rstflg_t >= '01' AND r.rstflg_t <= '13'"
                    Case "2"
                        sSql += "   AND r.rstflg_t >= '20' AND r.rstflg_t <= '23'"
                    Case "3"
                        sSql += "   AND r.rstflg_t >= '3'"
                    Case "4"    '<20141209 미최종보고 수정 
                        sSql += "   AND r.rstflg_t >= '00' AND r.rstflg_t <= '23'"
                End Select

                'sSql += " ORDER BY tkdt, workno, bcno"
                sSql += " ORDER BY  workno, tkdt, bcno"  '<<<20150806 작업번호 우선순위로 수정 

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '<<<20150806 결과일시로 조회 되게 추가 
        Public Shared Function fnGet_SpcList_tk(ByVal rsPartSlip As String, ByVal rsTGrpCd As String, _
                                          ByVal rsTkdtS As String, ByVal rsTkdtE As String, ByVal rsRstFlg As String, ByVal rsMode As Integer) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_SpcList_tgrp(string, string, string, string, string) as datatable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       j.regno, j.patnm," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(r.workno) workno," + vbCrLf
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt," + vbCrLf
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn," '20130910 정선영 추가, remark 표시하기 위해서+vbCrLf 
                sSql += "       CASE WHEN j.rstflg = '2' THEN 'Y' ELSE 'N' END rstflg" + vbCrLf

                '<< JJH 과거 Field, MTB, NTM 결과에 따른 색표시 fn_get_afbculture_color
                ' rsTGrpCd = LC, TC    --> MTB, NTM (주황색, 하늘색)
                ' rsTGrpCd = TS        --> Field    (노란색)
                sSql += "       , FN_GET_AFBCULTURE_COLOR(j.bcno, '" + rsTGrpCd + "') color " + vbCrLf
                sSql += "  FROM lj010m j," + vbCrLf
                sSql += "       (SELECT r.bcno, MIN(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno, MIN(r.tkdt) tkdt," + vbCrLf
                sSql += "               MIN(NVL(r.rstflg, '0')) || MAX(NVL(r.rstflg, '0')) rstflg_t" + vbCrLf
                sSql += "          FROM lm010m r, lf060m f" + vbCrLf

                If rsMode = 0 Then
                    sSql += "         WHERE r.tkdt >= :dates" + vbCrLf
                    sSql += "           AND r.tkdt <= :datee || '235959'" + vbCrLf
                Else
                    sSql += "         WHERE r.fndt >= :dates" + vbCrLf
                    sSql += "           AND r.fndt <= :datee || '235959'" + vbCrLf
                End If


                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkdtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkdtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkdtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkdtE))

                If rsTGrpCd <> "" Then
                    sSql += "           AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)" + vbCrLf
                    sSql += "           AND f.tcdgbn <> 'B'" + vbCrLf '20140711 정선영 추가.
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                Else
                    sSql += "           AND f.partcd = :partcd" + vbCrLf
                    sSql += "           AND f.slipcd = :slipcd" + vbCrLf
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                sSql += "           AND NVL(r.wkymd, ' ') <> ' '" + vbCrLf
                sSql += "           AND r.testcd = f.testcd" + vbCrLf
                sSql += "           AND r.spccd  = f.spccd" + vbCrLf
                sSql += "           AND r.tkdt  >= f.usdt" + vbCrLf
                sSql += "           AND r.tkdt  <  f.uedt" + vbCrLf
                'sSql += "           AND NVL(f.titleyn, '0') = '0'"
                'sSql += "           AND (CASE WHEN f.tcdgbn = 'C' THEN NVL(f.reqsub, '0') ELSE '1' END = '1' OR NVL(r.orgrst, ' ') <> ' ')"
                'sSql += "           AND (CASE WHEN f.tcdgbn = 'C' THEN NVL(f.reqsub, '0') ELSE '1' END = '1' OR NVL(r.orgrst, ' ') <> ' ')"
                '<20140711 정선영 수정
                sSql += "                      AND ( (f.tcdgbn IN ('B', 'P') AND nvl(f.titleyn, '0') = '0') OR" + vbCrLf
                sSql += "                            (CASE WHEN f.tcdgbn = 'C' THEN nvl(f.reqsub, '0') ELSE '1' END = '1' OR nvl(r.orgrst, ' ') <> ' ')" + vbCrLf
                sSql += "                          )" + vbCrLf
                '>
                sSql += "         GROUP BY bcno" + vbCrLf
                sSql += "       ) r" + vbCrLf
                sSql += " WHERE j.bcno   = r.bcno" + vbCrLf
                sSql += "   AND j.spcflg = '4'" + vbCrLf

                Select Case rsRstFlg
                    Case "0"
                        sSql += "   AND r.rstflg_t = '00'" + vbCrLf
                    Case "1"
                        sSql += "   AND r.rstflg_t >= '01' AND r.rstflg_t <= '13'" + vbCrLf
                    Case "2"
                        sSql += "   AND r.rstflg_t >= '20' AND r.rstflg_t <= '23'" + vbCrLf
                    Case "3"
                        sSql += "   AND r.rstflg_t >= '3'" + vbCrLf
                    Case "4"    '<20141209 미최종보고 수정 
                        sSql += "   AND r.rstflg_t >= '00' AND r.rstflg_t <= '23'" + vbCrLf
                End Select

                'sSql += " ORDER BY tkdt, workno, bcno"
                sSql += " ORDER BY  workno, tkdt, bcno" + vbCrLf  '<<<20150806 작업번호 우선순위로 수정 

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        '-- 작업번호별 검사대상자 조회
        Public Shared Function fnGet_SpcList_wgrp(ByVal rsWkYmd As String, ByVal rsWGrpCd As String, _
                                                  ByVal rsWkNoS As String, ByVal rsWkNoE As String, ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_SpcList_wkgrp(string, string, string, string, string) as datatable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       j.regno, j.patnm," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(r.workno) workno," + vbCrLf
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt," + vbCrLf
                sSql += "       CASE WHEN (SELECT count(*) FROM lj011m WHERE bcno = j.bcno AND NVL(doctorrmk, ' ') <> ' ') > 0 THEN 'Y' ELSE 'N' END rmkyn," + vbCrLf '20130910 정선영 추가, remark 표시하기 위해서
                sSql += "       CASE WHEN j.rstflg = '2' THEN 'Y' ELSE 'N' END rstflg" + vbCrLf

                sSql += "  FROM lj010m j," + vbCrLf
                sSql += "       (SELECT r.bcno, MIN(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno, MIN(r.tkdt) tkdt," + vbCrLf
                sSql += "               MIN(NVL(r.rstflg, '0')) || MAX(NVL(r.rstflg, '0')) rstflg_t" + vbCrLf
                sSql += "          FROM lm010m r, lf060m f" + vbCrLf
                sSql += "         WHERE r.wkymd   = :wkymd" + vbCrLf
                sSql += "           AND r.wkgrpcd = :wgrpcd" + vbCrLf
                sSql += "           AND r.wkno   >= :wknos" + vbCrLf
                sSql += "           AND r.wkno   <= :wknoe" + vbCrLf

                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                sSql += "           AND r.testcd = f.testcd" + vbCrLf
                sSql += "           AND r.spccd  = f.spccd" + vbCrLf
                sSql += "           AND r.tkdt  >= f.usdt" + vbCrLf
                sSql += "           AND r.tkdt  <  f.uedt" + vbCrLf
                sSql += "           AND NVL(f.titleyn, '0') = '0'" + vbCrLf
                sSql += "           AND (CASE WHEN f.tcdgbn = 'C' THEN NVL(f.reqsub, '0') ELSE '1' END = '1' OR NVL(r.orgrst, ' ') <> ' ')" + vbCrLf
                sSql += "         GROUP BY bcno" + vbCrLf
                sSql += "       ) r" + vbCrLf
                sSql += " WHERE j.bcno   = r.bcno" + vbCrLf
                sSql += "   AND j.spcflg = '4'" + vbCrLf

                Select Case rsRstFlg
                    Case "0"
                        sSql += "   AND r.rstflg_t = '00'" + vbCrLf
                    Case "1"
                        sSql += "   AND r.rstflg_t >= '01' AND r.rstflg_t <= '13'" + vbCrLf
                    Case "2"
                        sSql += "   AND r.rstflg_t >= '20' AND r.rstflg_t <= '23'" + vbCrLf
                    Case "3"
                        sSql += "   AND r.rstflg_t >= '3'" + vbCrLf
                    Case "4"    '<20141209 미최종보고 조회 수정 
                        sSql += "   AND r.rstflg_t >= '00' AND r.rstflg_t <= '23'" + vbCrLf
                End Select

                sSql += " ORDER BY workno, tkdt, bcno" + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- W/L별 검사대상자 조회
        Public Shared Function fnGet_SpcList_wl(ByVal rsWLUid As String, ByVal rsWLYmd As String, ByVal rsWLTItle As String, ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_SpcList_wl(string, string, string, string) as datatable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, j.patnm, "
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_full(r.workno) workno,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       CASE WHEN j.rstflg = '2' THEN 'Y' ELSE 'N' END rstflg,"

                sSql += "  FROM lj010m j,"
                sSql += "       (SELECT r.bcno, w.wlseq workno, MIN(r.tkdt) tkdt,"
                sSql += "               MIN(NVL(r.rstflg, '0')) || MAX(NVL(r.rstflg, '0')) rstflg_t"
                sSql += "          FROM lrw11m w, lm010m r, lf060m f"
                sSql += "         WHERE w.wluid   = :wluid"
                sSql += "           AND w.wlymd   = :wlymd"
                sSql += "           AND w.wltitle = :wltitle"

                alParm.Add(New OracleParameter("wluid", OracleDbType.Varchar2, rsWLUid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLUid))
                alParm.Add(New OracleParameter("wlymd", OracleDbType.Varchar2, rsWLYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLYmd))
                alParm.Add(New OracleParameter("wltitle", OracleDbType.Varchar2, rsWLTItle.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWLTItle))

                sSql += "           AND w.bcno   = r.bcno"
                sSql += "           AND w.testcd = r.testcd"
                sSql += "           AND NVL(r.wkymd, ' ') <> ' '"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "           AND r.tkdt  >= f.usdt"
                sSql += "           AND r.tkdt  <  f.uedt"
                sSql += "           AND NVL(f.titleyn, '0') = '0'"
                sSql += "           AND (CASE WHEN f.tcdgbn = 'C' THEN NVL(f.reqsub, '0') ELSE '1' END = '1' OR NVL(r.orgrst, ' ') <> ' ')"

                sSql += "         GROUP BY r.bcno, w.wlseq"
                sSql += "       ) r"
                sSql += " WHERE j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"

                Select Case rsRstFlg
                    Case "0"
                        sSql += "   AND r.rstflg_t = '00'"
                    Case "1"
                        sSql += "   AND r.rstflg_t >= '01' AND r.rstflg_t <= '13'"
                    Case "2"
                        sSql += "   AND r.rstflg_t >= '20' AND r.rstflg_t <= '23'"
                    Case "3"
                        sSql += "   AND r.rstflg_t >= '3'"
                    Case "4"    '<20141209 미최종보고 플래그 추가 
                        sSql += "   AND r.rstflg_t >= '00' AND r.rstflg_t <= '23'"

                End Select

                sSql += " ORDER BY workno, tkdt, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 등록번호별 검사대상자 조회
        Public Shared Function fnGet_SpcList_RegNo(ByVal rbMicro As Boolean, ByVal rsSlipCd As String, ByVal rsTgrpCd As String, ByVal rsWkGrpCd As String, ByVal rsRegNo As String) As DataTable
            Dim sFn As String = ""

            Try
                Dim sTableNm As String = "lr010m"
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rbMicro Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm,"
                If rsWkGrpCd <> "" Then
                    sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno)) workno,"
                Else
                    sSql += "       '' workno,"
                End If
                sSql += "       fn_ack_date_str(MAX(r.tkdt), 'yyyy-mm-dd hh24:mi:ss') tkdt, CASE WHEN j.rstflg = '2' THEN 'Y' ELSE 'N' END rstflg"
                sSql += "  FROM lj010m j, " + sTableNm + " r"
                sSql += " WHERE j.regno  = :regno"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.spcflg = '4'"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                If rsTgrpCd <> "" Then

                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTgrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTgrpCd))

                ElseIf rsSlipCd <> "" Then

                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= r.tkdt AND uedt > r.tkdt)"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))

                End If

                If rsWkGrpCd <> "" Then
                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf066m WHERE wkgrpcd = :wgrpcd)"
                    alParm.Add(New OracleParameter("wkgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                End If

                sSql += " GROUP BY j.bcno, j.regno, j.patnm, j.rstflg" + IIf(rsWkGrpCd <> "", ", r.wkymd, r.wkgrpcd, r.wkno", "").ToString '<20141020 수정
                sSql += " ORDER BY tkdt desc, workno DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        '-- 검사항목별 검사대상자 조회
        Public Shared Function fnGet_SpcList_Test(ByVal rsTestCds As String, ByVal rsWkYmd As String, ByVal rsWGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, ByVal rsRstNullReg As String, ByVal rsTkDtB As String, ByVal rsTkDtE As String, _
                                                  Optional ByVal rsBcno As String = "", Optional ByVal rsDoubleTest As String = "", _
                                                  Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_SpcList_Test(String, String, String, String, String, String, (String)) As DataTable"


            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = ""
                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno," + vbCrLf
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt , " + vbCrLf '20150921 처방일시 추가 
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, f3.spcnmd," + vbCrLf
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage," + vbCrLf
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || '/' || FN_ACK_GET_WARD_ABBR(j.wardno)" + vbCrLf
                sSql += "            ELSE FN_ACK_GET_DEPT_ABBR (j.iogbn, j.deptcd)" + vbCrLf
                sSql += "       END deptcd," + vbCrLf
                sSql += "       NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd, f6.tcdgbn, f6.titleyn, f6.plgbn," + vbCrLf
                sSql += "       r.orgrst, r.rstflg, r.mwid, fn_ack_date_str(r.tkdt, 'yyyymmddhh24miss') tkdt, j.wardno ||'/' || j.roomno wardroom," + vbCrLf

                '20201216 jhs 검사항목별 결과저장 및 보고도 색 변경되게 하기 위한 쿼리 추가
                sSql += "       CASE WHEN (SELECT   COUNT ( * ) FROM(lj011m) WHERE(bcno = j.bcno)" + vbCrLf
                sSql += "       AND NVL (doctorrmk, ' ') <> ' ') > 0 THEN 'Y'" + vbCrLf
                sSql += "       ELSE 'N' End rmkyn," + vbCrLf
                sSql += "       CASE WHEN j.rstflg = '2' THEN 'Y' ELSE 'N' END rstflagyn," + vbCrLf
                sSql += "       FN_GET_AFBCULTURE_COLOR (j.bcno, 'TS') colorTS," + vbCrLf
                sSql += "       FN_GET_AFBCULTURE_COLOR (j.bcno, 'LC') colorLC," + vbCrLf
                sSql += "       FN_GET_AFBCULTURE_COLOR (j.bcno, 'TC') colorTC" + vbCrLf
                '----------------------------------------------------------------------------------

                sSql += "  FROM lj010m j, lm010m r, lf060m f6, lf030m f3" + vbCrLf
                If rsBcno <> "" Then
                    sSql += " WHERE j.bcno = :bcno" + vbCrLf
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                ElseIf rsWkYmd <> "" Then
                    sSql += " WHERE r.wkymd   = :wkymd" + vbCrLf
                    sSql += "   AND r.wkgrpcd = :wgrpcd" + vbCrLf
                    sSql += "   AND r.wkno   >= :wknos" + vbCrLf
                    sSql += "   AND r.wkno   <= :wknoe" + vbCrLf

                    alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                    alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                    alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))
                Else
                    sSql += " WHERE r.tkdt >= :dates || '0000'" + vbCrLf
                    sSql += "   AND r.tkdt <= :datee || '5959'" + vbCrLf

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtB.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtB))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))
                End If

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd" + vbCrLf
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))

                End If
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '" + vbCrLf
                sSql += "   AND r.testcd = f6.testcd" + vbCrLf
                sSql += "   AND r.spccd  = f6.spccd" + vbCrLf
                sSql += "   AND r.tkdt  >= f6.usdt" + vbCrLf
                sSql += "   AND r.tkdt  <  f6.uedt" + vbCrLf
                sSql += "   AND j.bcno   = r.bcno" + vbCrLf
                sSql += "   AND j.spccd  = f3.spccd" + vbCrLf
                sSql += "   AND r.tkdt  >= f3.usdt" + vbCrLf
                sSql += "   AND r.tkdt  <  f3.uedt" + vbCrLf
                sSql += "   AND j.spcflg = '4'" + vbCrLf

                Select Case rsRstNullReg
                    Case "000"

                    Case "001"
                        sSql += "   AND NVL(r.rstflg, '0') = '3'" + vbCrLf
                    Case "010"
                        sSql += "   AND NVL(r.rstflg, '0') > '0' AND NVL(r.rstflg, '0') < '3'" + vbCrLf
                    Case "011"
                        sSql += "   AND NVL(r.rstflg, '0') > '0'" + vbCrLf
                    Case "100"
                        sSql += "   AND NVL(r.rstflg, '0') = '0'" + vbCrLf
                    Case "101"
                        sSql += "   AND (NVL(r.rstflg, '0') = '0' OR NVL(r.rstflg, '0') = '3')" + vbCrLf
                    Case "110"
                        sSql += "   AND (NVL(r.rstflg, '0') = '0' OR NVL(r.rstflg, '0') < '3')" + vbCrLf
                    Case "111"

                End Select

                If rsTestCds <> "" Then sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')" + vbCrLf

                sSql += " ORDER BY workno" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- No growth List 조회 ( 검체번호 기준 )
        Public Shared Function fnGet_NgList_BcNo(ByVal rsBcNo As String, ByVal rsOpt As String) As DataTable
            Dim sFn As String = "fnGet_NgList_BcNo"

            Try
                Dim sSql As String = ""

                sSql += "SELECT j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || NVL(j.roomno, '') ELSE j.deptcd END deptinfo,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi:ss') rstdt, r.rstflg,"
                sSql += "       f3.spcnmd, f6.tnmd, r.testcd, r.spccd"
                sSql += "  FROM lj010m j, lf030m f3, lf060m f6, lm010m r"
                sSql += " WHERE j.bcno   = :bcno"
                sSql += "   AND j.bcno   = r.bcno "
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.tkdt   >= f6.usdt"
                sSql += "   AND r.tkdt   <  f6.uedt"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"
                sSql += "   AND f6.spccd = f3.spccd"
                sSql += "   AND NVL(f6.mbttype, '0') = '2'"
                sSql += "   AND f6.tcdgbn IN ('P', 'S')"

                If rsOpt = "10" Then
                    sSql += "   AND NVL(r.rstflg, ' ') = ' '"
                ElseIf rsOpt = "11" Then
                    sSql += "   AND (NVL(r.rstflg, ' ') = ' ' OR r.rstflg <> '3')"
                ElseIf rsOpt = "01" Then
                    sSql += "   AND r.rstflg <> '3'"
                End If

                sSql += " ORDER BY f6.dispseqL, f6.testcd  "

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- No growth List 조회 ( by 작업그룹, 작업번호S, 작업번호E, 작업일자S, 작업일자E, 완Or미완 )
        Public Shared Function fnGet_NgList_WGrp(ByVal rsWkYmd As String, ByVal rsWGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                     ByVal rsOpt As String, Optional ByVal rsSpcCd As String = "") As DataTable
            Dim sFn As String = "fnGet_NgList_WkGrp"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT j1.regno, j.patnm, j1.sex || '/' || j1.age sexage,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j1.bcno) bcno,"
                sSql += "       fn_ack_get_dr_name(j1.doctorcd) doctornm,"
                sSql += "       CASE WHEN j1.iogbn = 'I' THEN j1.wardno || '/' || j1.roomno ELSE j1.deptcd END deptinfo,"
                sSql += "       fn_ack_date_str(r.tkdt,  'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi:ss') rstdt, r.rstflg,"
                sSql += "       f3.spcnmd, f6.tnmd, r.testcd, r.spccd "
                sSql += "  FROM lj010m j1, lf030m f3, lf060m f6, lm010m r"
                sSql += " WHERE r.wkymd   = :wkymd"
                sSql += "   AND r.wkgrpcd = :wgrpcd"
                sSql += "   AND r.wkno   >= :wknos"
                sSql += "   AND r.wkno   <= :wknoe"
                sSql += "   AND j1.bcno   = r.bcno"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.tkdt   >= f6.usdt"
                sSql += "   AND r.tkdt   <  f6.uedt"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"
                sSql += "   AND f6.spccd  = f3.spccd"
                sSql += "   AND NVL(f6.mbttype, '0') = '2'"
                sSql += "   AND f6.tcdgbn IN ('P', 'S')"

                al.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                al.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                al.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                If rsSpcCd <> "" Then
                    sSql += "   AND r.spccd = :spccd"
                    al.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsOpt = "10" Then
                    sSql += "   AND NVL(r.rstflg, ' ') = ' '"
                ElseIf rsOpt = "11" Then
                    sSql += "    and (NVL(r.rstflg, ' ') = ' ' OR r.rstflg <> '3')"
                ElseIf rsOpt = "01" Then
                    sSql += "   AND r.rstflg <> '3'"
                End If

                sSql += " ORDER BY r.wkymd, r.wkgrpcd, r.wkno, r.bcno, f6.dispseql, r.testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- No growth List 조회 ( by 검사그룹)
        Public Shared Function fnGet_NgList_TGrp(ByVal rsTGrpCd As String, ByVal rsTkDts As String, ByVal rsTkDtE As String, _
                                                    ByVal rsTestCd As String, ByVal rsOpt As String) As DataTable
            Dim sFn As String = "fnGet_NgList_TGrp"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT j1.regno, j1.patnm, j1.sex || '/' || j1.age sexage,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j1.bcno) bcno,"
                sSql += "       fn_ack_get_dr_name(j1.doctorcd) doctornm,"
                sSql += "       CASE WHEN j1.iogbn = 'I' THEN j1.wardno || '/' || NVL(j1.roomno, '') ELSE j1.deptcd END deptinfo,"
                sSql += "       fn_ack_date_str(r.tkdt,  'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi:ss') rstdt, r.rstflg,"
                sSql += "       f3.spcnmd, f6.tnmd, r.testcd, r.spccd "
                sSql += "  FROM lj010m j1, lf030m f3, lf060m f6, lm010m r"
                sSql += " WHERE r.tkdt >= :dates"
                sSql += "   AND r.tkdt <= :datee || '5959'"
                sSql += "   AND NVL(r.wkymd, ' ') <> ' '"

                al.Add(New OracleParameter("datss", OracleDbType.Varchar2, rsTkDts.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDts))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

                If rsTGrpCd <> "" Then
                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                End If

                If rsTestCd <> "" Then
                    sSql += "   and r.testcd = :testcd"
                    al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                End If

                sSql += "    AND j1.bcno  = r.bcno"
                sSql += "    AND r.testcd = f6.testcd"
                sSql += "    AND r.tkdt  >= f6.usdt"
                sSql += "    AND r.tkdt  <  f6.uedt"
                sSql += "    AND r.spccd  = f3.spccd"
                sSql += "    AND r.tkdt  >= f3.usdt"
                sSql += "    AND r.tkdt  <  f3.uedt"
                sSql += "    AND f6.spccd = f3.spccd"
                sSql += "    AND NVL(f6.mbttype, '0') = '2'"
                sSql += "    AND f6.tcdgbn IN ('P', 'S')"

                If rsOpt = "10" Then
                    sSql += "   AND NVL(r.rstflg, ' ') = ' '"
                ElseIf rsOpt = "11" Then
                    sSql += "    and (NVL(r.rstflg, ' ') = ' ' OR r.rstflg <> '3')"
                ElseIf rsOpt = "01" Then
                    sSql += "   AND r.rstflg <> '3'"
                End If

                sSql += " ORDER BY r.tkdt, r.wkymd, r.wkgrpcd, r.wkno, r.bcno, f6.dispseqL, r.testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- Growth List ( 처방일시 또는 보고일시 기준 )
        Public Shared Function fnGet_Rst_Growth(ByVal rsDayS As String, ByVal rsDayE As String, ByVal rsOpt As String) As DataTable
            Dim sFn As String = "Function fnGet_Rst_Growth"

            Try
                Dim sSql As String = ""

                If rsOpt = "F" Then
                    sSql += "SELECT /*+ RULE */"
                    sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                    sSql += "       j.bcno, r.testcd, r.spccd,"
                    sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                    sSql += "       r.ranking, r.baccd, r.bacseq, r.testmtd, fa.dispseq, fa.antinmd, r.anticd,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptnm, FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno wardroom,"
                    sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptinfo,"
                    sSql += "       case when NVL(j.entdt, '-') = '-' then '--' else fn_ack_date_str(j.entdt, 'yyyy-mm-dd') end entdt,"
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                    sSql += "       f3.spcnmd, fb.bacnmd, fb.bacgencd bacgen, r.decrst, r.antirst,"
                    sSql += "       j.regno || ', ' || r.bcno || ',' || r.testcd || ',' || r.spccd || ',' || r.baccd || ',' || r.bacseq || ',' || r.testmtd sortkey"
                    sSql += "  FROM lj010m j, lf210m fb, lf230m fa, lf030m f3,"
                    sSql += "       (SELECT b.bcno, b.testcd, b.spccd, b.baccd, b.bacseq, b.ranking, c.testmtd,"
                    sSql += "               c.anticd, c.decrst, c.antirst, a.tkdt, a.rstdt"
                    sSql += "          FROM lm010m a, lm012m b, lm013m c"
                    sSql += "         WHERE a.rstdt >= :dates"
                    sSql += "           AND a.rstdt <= :datee || '235959'"
                    sSql += "           AND a.bcno = b.bcno AND a.testcd = b.testcd"
                    sSql += "           AND b.bcno = c.bcno(+) AND b.testcd = c.testcd(+) AND b.baccd = c.baccd(+) AND b.bacseq = c.bacseq(+)"
                    sSql += "           AND a.rstflg = '3'"
                    sSql += "       ) r"
                    sSql += " WHERE j.bcno       = r.bcno"
                    sSql += "   AND j.spccd      = f3.spccd"
                    sSql += "   AND r.tkdt      >= f3.usdt"
                    sSql += "   AND r.tkdt      <  f3.uedt"
                    sSql += "   AND r.baccd      = fb.baccd"
                    sSql += "   AND r.tkdt      >= fb.usdt"
                    sSql += "   AND r.tkdt      <  fb.uedt"
                    sSql += "   AND r.anticd     = fa.anticd"
                    sSql += "   AND r.tkdt      >= fa.usdt"
                    sSql += "   AND r.tkdt      <  fa.uedt"
                    sSql += "   AND fb.bacgencd <> :bgencd"
                Else
                    sSql += "SELECT /*+ RULE */"
                    sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                    sSql += "       j.bcno, r.testcd, r.spccd,"
                    sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                    sSql += "       rb.ranking, rb.baccd, rb.bacseq, rb.testmtd, fa.dispseq, fa.antinmd, ra.anticd,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, fn_ack_get_dept_name(j.iogbn,j.deptcd) deptnm, fn_ack_get_ward_name(j.wardno) || '/' || j.roomno wardroom,"
                    sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptinfo,"
                    sSql += "       case when NVL(j.entdt, '-') = '-' then '--' else fn_ack_date_str(j.entdt, 'yyyy-mm-dd') end entdt,"
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                    sSql += "       f3.spcnmd, fb.bacnmd, fb.bacgencd bacgen, ra.decrst, ra.antirst,"
                    sSql += "       j.regno || ', ' || j.bcno || ',' || r.testcd || ',' || r.spccd || ',' || rb.baccd || ',' || rb.bacseq || ',' || rb.testmtd sortkey"
                    sSql += "  FROM lm010m r, lj010m j,  lm012m rb, lm013m ra, lf210m fb, lf230m fa, lf030m f3"
                    sSql += " WHERE r.tkdt     >= :dates"
                    sSql += "   AND r.tkdt     <= :datee || '235959'" '<<<20170410 의뢰일시 접수일시로 변경 
                    'sSql += " WHERE j.orddt     >= :dates"
                    'sSql += "   AND j.orddt     <= :datee || '235959'"
                    sSql += "   AND r.bcno       = j.bcno"
                    sSql += "   AND r.bcno       = rb.bcno"
                    sSql += "   AND r.testcd     = rb.testcd"
                    sSql += "   AND r.rstflg     = '3'"
                    sSql += "   AND rb.bcno      = ra.bcno(+)"
                    sSql += "   AND rb.testcd    = ra.testcd(+)"
                    sSql += "   AND rb.baccd     = ra.baccd(+)"
                    sSql += "   AND rb.bacseq    = ra.bacseq(+)"
                    sSql += "   AND r.spccd      = f3.spccd"
                    sSql += "   AND r.tkdt      >= f3.usdt"
                    sSql += "   AND r.tkdt      <  f3.uedt"
                    sSql += "   AND rb.baccd     = fb.baccd"
                    sSql += "   AND r.tkdt      >= fb.usdt"
                    sSql += "   AND r.tkdt      <  fb.uedt"
                    sSql += "   AND ra.anticd    = fa.anticd"
                    sSql += "   AND r.tkdt      >= fa.usdt"
                    sSql += "   AND r.tkdt      <  fa.uedt"
                    sSql += "   AND fb.bacgencd <> :bgencd"
                End If

                sSql += " ORDER BY sortkey"

                Dim al As New ArrayList

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                al.Add(New OracleParameter("bgencd", OracleDbType.Varchar2, FixedVariable.gsBacGenCd_Nogrowth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, FixedVariable.gsBacGenCd_Nogrowth))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- 균 결과내역 ( 검체번호 기준 )
        Public Shared Function fnGet_Rst_Bac(ByVal rsBcNo As String, ByVal rsTestCds As String) As DataTable
            Dim sFn As String = "Function fnGet_Rst_Bac"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT bcno, testcd, spccd, baccd, bacseq, ranking, testmtd, bacnmd, bacgencd, incrst, baccmt, status," + vbCrLf
                sSql += "       baccd oldbaccd, ranking oldranking, incrst oldincrst, baccmt oldbaccmt" + vbCrLf
                '20210817 jhs 균색 변경으로 인해 추가 
                sSql += "         , a.BACCOLOR " + vbCrLf
                '---------------------------------------------
                sSql += "  FROM (" + vbCrLf
                sSql += "        SELECT rm.bcno, rm.testcd, rm.spccd, rb.baccd, rb.bacseq, rb.testmtd, fb.bacnmd, fb.bacgencd, rb.incrst, rb.baccmt, rb.ranking, 'S' status" + vbCrLf
                '20210817 jhs 균색 변경으로 인해 추가 
                sSql += "         , FB.BACCOLOR " + vbCrLf
                '---------------------------------------------
                sSql += "          FROM lm010m rm, lm012m rb, lf210m fb" + vbCrLf
                sSql += "         WHERE rm.bcno   = :bcno" + vbCrLf
                sSql += "           AND rm.bcno   = rb.bcno AND rm.testcd = rb.testcd and rm.spccd = rb.spccd" + vbCrLf
                sSql += "           AND rb.baccd  = fb.baccd" + vbCrLf
                sSql += "           AND rm.tkdt  >= fb.usdt and rm.tkdt < fb.uedt" + vbCrLf

                If rsTestCds <> "" Then
                    sSql += "           AND TRIM(rm.testcd) || TRIM(rm.spccd) IN ('" + rsTestCds.Replace(",", "','") + "')" + vbCrLf
                End If

                sSql += "       ) a" + vbCrLf
                sSql += " ORDER BY a.ranking, a.bacseq, a.baccd, a.testcd" + vbCrLf

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- 해당 검체에 균결과 History 존재여부
        Public Shared Function fnFind_Micro_Bak(ByVal rsBcNo As String) As Boolean
            Dim sFn As String = "Function fnFind_Micro_Bak"

            Try
                Dim sSql As String = ""

                sSql = "SELECT COUNT(bcno) FROM lm012h WHERE bcno = :bcno"

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Dim bReturn As Boolean = False

                If dt.Rows.Count > 0 Then
                    If IsNumeric(dt.Rows(0).Item(0)) Then
                        If Convert.ToInt32(dt.Rows(0).Item(0)) > 0 Then
                            bReturn = True
                        End If
                    End If
                End If

                Return bReturn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 항균제 결과내역 ( 검체번호 기준 )
        Public Shared Function fnGet_Rst_Anti(ByVal rsBcNo As String, ByVal rsTestCds As String) As DataTable
            Dim sFn As String = "Function fnGet_Rst_Anti"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT ra.bacnmd, ra.incrst, ra.bcno, ra.testcd, ra.spccd,ra. baccd, ra.bacseq, ra.bacgencd,"
                sSql += "       ra.testmtd, ra.anticd, ra.antinmd, ra.antirst, ra.decrst, ra.rptyn, fg.refr, fg.refs refs, 'S' status"
                sSql += "  FROM (SELECT fb.bacnmd, rb.incrst, rb.bcno, rb.testcd, rb.spccd, rb.baccd, rb.bacseq, fb.bacgencd,"
                sSql += "               ra.testmtd, ra.anticd, fa.antinmd, ra.antirst, ra.decrst, ra.rptyn, fa.dispseq, rm.tkdt"
                sSql += " 		   FROM lm010m rm, lm012m rb, lm013m ra, lf210m fb, lf230m fa"
                sSql += " 		  WHERE rm.bcno   = :bcno"
                sSql += " 		    AND rm.bcno   = rb.bcno AND rm.testcd = rb.testcd AND rm.spccd = rb.spccd"
                sSql += " 		    AND rb.baccd  = fb.baccd"
                sSql += " 		    AND rm.tkdt  >= fb.usdt AND rm.tkdt < fb.uedt"
                sSql += " 		    AND rb.bcno   = ra.bcno AND rb.testcd = ra.testcd AND rb.spccd = ra.spccd AND rb.baccd = ra.baccd AND rb.bacseq = ra.bacseq"
                sSql += " 		    AND ra.anticd = fa.anticd"
                sSql += " 		    AND rm.tkdt  >= fa.usdt AND rm.tkdt < fa.uedt"

                If rsTestCds <> "" Then
                    sSql += " 		    AND TRIM(rm.testcd) || TRIM(rm.spccd) IN ('" + rsTestCds.Replace(",", "','") + "')"
                End If

                sSql += "       ) ra LEFT OUTER JOIN"
                sSql += "       lf240m fg ON (ra.bacgencd = fg.bacgencd AND ra.anticd = fg.anticd AND ra.testmtd = fg.testmtd)"
                sSql += " ORDER BY ra.bacseq, ra.baccd, ra.testcd, ra.testmtd, ra.dispseq, ra.antinmd"

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 균결과 History
        Public Shared Function fnGet_Micro_Bac_Rst_History(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "fnGet_Micro_Bac_Rst_History"

            Try
                Dim sSql As String = ""

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(b.moddt, 'yyyy-mm-dd hh24:mi:ss') deldt, fn_ack_get_bcno_full(a.bcno) bcno, a.testcd,"
                sSql += "       f60.tnmd, b.bacseq, b.baccd, f21.bacnmd, b.incrst, b.testmtd,"
                sSql += "       fn_ack_date_str(b.rstdt, 'yyyy-mm-dd hh24:mi:ss') rstdt,"
                sSql += "       fn_ack_get_usr_name(CASE WHEN a.rstflg = '3' THEN a.fnid WHEN a.rstflg = '2' THEN a.mwid WHEN a.rstflg = '1' THEN a.regid END) rstnm, b.rstflg,"
                sSql += "       NVL(f60.dispseql, 999) sortl"
                sSql += "  FROM lm010m a, lm012h b, lf060m f60, lf210m f21"
                sSql += " WHERE a.bcno   = :bcno"
                sSql += "   AND a.bcno   = b.bcno"
                sSql += "   AND a.testcd = b.testcd"
                sSql += "   AND a.testcd = f60.testcd"
                sSql += "   AND a.spccd  = f60.spccd"
                sSql += "   AND a.tkdt  >= f60.usdt"
                sSql += "   AND a.tkdt  <  f60.uedt"
                sSql += "   AND b.baccd  = f21.baccd"
                sSql += "   AND a.tkdt  >= f21.usdt"
                sSql += "   AND a.tkdt  <  f21.uedt"
                sSql += " ORDER BY bcno, deldt, sortl, b.bacseq"

                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 항생제결과 History
        Public Shared Function fnGet_Micro_Anti_Rst_History(ByVal rsBcNo As String, ByVal rsTestCd As String, ByVal rsModDt As String) As DataTable
            Dim sFn As String = "fnGet_Micro_Anti_Rst_History"

            Try
                Dim sSql As String = ""

                sSql += "SELECT ra.bacseq, fa.antinmd, ra.antirst, ra.decrst, ra.anticd, NVL(fa.dispseq, 999) sorta"
                sSql += "  FROM ("
                sSql += "        SELECT ra.anticd, ra.antirst, ra.decrst, rm.tkdt, ra.bacseq"
                sSql += "          FROM lm010m rm, lm013h ra"
                sSql += "         WHERE rm.bcno   = :bcno"
                sSql += "           and rm.testcd = :testcd"
                sSql += "           and rm.bcno   = ra.bcno"
                sSql += "           and rm.testcd = ra.testcd"
                sSql += "           and rm.spccd  = ra.spccd"
                sSql += "           and ra.moddt  = :moddt"
                sSql += "       ) ra LEFT OUTER JOIN "
                sSql += "       lf230m fa ON (ra.anticd = fa.anticd AND ra.tkdt  >= fa.usdt AND ra.tkdt  <  fa.UEDT)"
                sSql += " ORDER BY sorta, ra.bacseq"


                Dim al As New ArrayList

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))
                al.Add(New OracleParameter("moddt", OracleDbType.Varchar2, rsModDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsModDt))

                DbCommand()
                Return DbExecuteQuery(sSql, al)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 균코드 조회
        Public Shared Function fnGet_BacCd() As DataTable
            Dim sFn As String = "fnGet_BacCd"

            Try
                Dim sSql As String = ""

                sSql += "SELECT baccd, bacnmd, bacgencd, usdt, uedt"
                sSql += "  FROM lf210m"
                sSql += " ORDER BY baccd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 균속별항균제코드 조회 ( by 검체번호의 접수일시 )
        Public Shared Function fnGet_AntiCd(ByVal rsTkDt As String) As DataTable
            Dim sFn As String = "fnGet_AntiCd"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT a.bacgencd, a.anticd, b.antinmd, a.testmtd, '' usdt, '' uedt, a.dispseq sort2, a.refr, a.refs, '1' rptyn"
                sSql += "  FROM lf240m a, lf230m b"
                sSql += " WHERE a.anticd = b.anticd"
                sSql += "   AND b.usdt  <= :usdt"
                sSql += "   AND b.uedt  >  :usdt"


                Dim al As New ArrayList

                al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsTkDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDt))
                al.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsTkDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDt))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '# 균속코드 조회
        Public Shared Function fnGet_BacGenCd() As DataTable
            Dim sFn As String = "fnGet_BacGenCd()"

            Try
                Dim sSql As String = ""

                sSql += "SELECT bacgencd, bacgennmd, dispseq FROM lf220m"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '# 균속코드(항균제 속한) 조회
        Public Shared Function fnGet_AntiBacGenCds() As DataTable
            Dim sFn As String = "fnGet_AntiBacGenCds()"

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT bacgencd FROM lf240m GROUP BY bacgencd"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        '# 증식정도코드 조회 ( New Table(LF211M) )
        Public Shared Function fnGet_BacIncCd(ByVal rsTestCd As String, ByVal rsSpcCd As String) As DataTable
            Dim sFn As String = "fnGet_BacIncCd()"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT incrstcd, incrstnm, testcd, spccd"
                sSql += "  FROM lf211m"
                sSql += " WHERE testcd = :testcd"

                alParm.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                If rsSpcCd <> "" Then
                    sSql += "   AND spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                sSql += " ORDER BY incrstcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


    End Class

    Public Class RegFn
        Private Const msFile As String = "File : CGLISAPP.vb, Class : LISAPP.APP_M.RegFn" + vbTab

        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Private m_dt_rst As DataTable

        Private m_al_ParentCd As ArrayList

        '균
        Public al_Bac As ArrayList
        '항균제
        Public al_Anti As ArrayList
        '소견
        Public al_Cmt As ArrayList

        Public Function fnGetBacInfo_IF(ByVal r_bi As ResultInfo_Bac) As Boolean
            Dim sFn As String = "Public Function fnGetBacInfo_IF(ResultInfo_Bac) As Boolean"

            Try
                Dim sSql As String = ""
                Dim alParam As New ArrayList

                Dim dt As New DataTable

                sSql = ""
                sSql += " select baccd, bacgencd, '1' sort1"
                sSql += "  from lf210m"
                sSql += " where bacifcd = :baccd"
                sSql += "   and usdt <= fn_ack_sysdate and uedt > fn_ack_sysdate"
                sSql += " union "
                sSql += " select baccd, bacgencd, '2' sort1"
                sSql += "  from lf210m"
                sSql += " where baccd = :baccd"
                sSql += "   and usdt <= fn_ack_sysdate and uedt > fn_ack_sysdate"
                sSql += " order by sort1"

                alParam.Add(New OracleParameter("baccd", OracleDbType.Varchar2, r_bi.BacCd.Length, ParameterDirection.Input, True, Nothing, Nothing, Nothing, DataRowVersion.Current, r_bi.BacCd))
                alParam.Add(New OracleParameter("baccd", OracleDbType.Varchar2, r_bi.BacCd.Length, ParameterDirection.Input, True, Nothing, Nothing, Nothing, DataRowVersion.Current, r_bi.BacCd))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParam)
                If dt.Rows.Count > 0 Then
                    r_bi.BacCd = dt.Rows(0).Item("baccd").ToString
                    r_bi.BacGenCd = dt.Rows(0).Item("bacgencd").ToString

                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Public Function fnGetBacInfo_IF_PrePos(ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal r_bi As ResultInfo_Bac) As Boolean
            Dim sFn As String = "Public Function fnGetBacInfo_IF_PrePos(STU_SampleInfo,  ResultInfo_Bac) As Boolean"

            Try
                Dim sSql As String = ""
                Dim alParam As New ArrayList

                Dim dt As New DataTable

                sSql = ""
                sSql += " select b.bacgencd, '1'"
                sSql += "   from lm013m a"
                sSql += "          inner join lf210m b"
                sSql += "             on a.baccd = b.baccd and b.usdt <= a.editdt and b.uedt > a.editdt"
                sSql += "  where a.bcno = :bcno"

                alParam.Add(New OracleParameter("bcno", OracleDbType.Varchar2, r_sampinfo_Buf.BCNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_sampinfo_Buf.BCNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParam)

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("bacgencd").ToString <> "--" Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGetBacInfo_IF_SpcCd(ByVal r_sampinfo_Buf As STU_SampleInfo) As String
            Dim sFn As String = "Public Function fnGetBacInfo_IF_SpcCd(STU_SampleInfo) As String"

            Try
                Dim sSql As String = ""
                Dim alParam As New ArrayList

                Dim dt As New DataTable

                sSql += "select a.spccd"
                sSql += "   from lj010m a"
                sSql += "  where a.bcno = :bcno"

                alParam.Add(New OracleParameter("bcno", OracleDbType.Varchar2, r_sampinfo_Buf.BCNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_sampinfo_Buf.BCNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParam)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("spccd").ToString
                Else
                    Return ""
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGetAntiInfo_IF(ByVal r_ai As ResultInfo_Anti) As Boolean
            Dim sFn As String = "Public Function fnGetAntiInfo_IF(ByVal r_ai As ResultInfo_Anti) As Boolean"

            Try
                Dim sSql As String = ""
                Dim alParam As New ArrayList

                Dim dt As New DataTable

                sSql = ""
                sSql += " select anticd"
                sSql += "  from lf230m"
                sSql += " where upper(antiifcd) = upper(:anticd)"
                sSql += "   and usdt <= fn_ack_sysdate and uedt > fn_ack_sysdate"
                sSql += " union "
                sSql += " select anticd"
                sSql += "  from lf230m"
                sSql += " where upper(anticd) = upper(:anticd)"
                sSql += "   and usdt <= fn_ack_sysdate and uedt > fn_ack_sysdate"

                '<2007-10-30 kmc
                alParam.Add(New OracleParameter("anticd", OracleDbType.Varchar2, r_ai.AntiCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_ai.AntiCd))
                alParam.Add(New OracleParameter("anticd", OracleDbType.Varchar2, r_ai.AntiCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_ai.AntiCd))
                '>

                DbCommand()
                dt = DbExecuteQuery(sSql, alParam)
                If dt.Rows.Count > 0 Then
                    r_ai.AntiCd = dt.Rows(0).Item("anticd").ToString

                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function RegServer(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList) As Integer
            Dim sFn As String = "Function RegServer"

            Try
                Dim iRegOK_Sum As Integer = 0
                Dim rstinfo_Buf As STU_RstInfo

                If r_al_EditSuc Is Nothing Then r_al_EditSuc = New ArrayList

                '0) Cn, Transaction 생성
                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim al_Cmt As New ArrayList
                Dim iPos As Integer

                '-- 2007/11/05 ssh
                al_Cmt.Clear()

                '1) 결과개수만큼 등록
                For i As Integer = 1 To r_al_RstInfo.Count
                    rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                    '-- Cmt 관련
                    iPos = InStr(rstinfo_Buf.EqFlag, "/")

                    If iPos > 0 Then
                        al_Cmt.Add(rstinfo_Buf.EqFlag)
                        rstinfo_Buf.EqFlag = ""
                    End If

                    If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                        iRegOK_Sum += 1
                        r_al_EditSuc.Add(rstinfo_Buf.TestCd)
                    End If
                Next

                If r_sampinfo_Buf.EqCd <> "" Then
                    '< add yooej 2009/08/05 
                    '1-1) 계산식 관련항목 등록
                    Dim objDaRegRst As New APP_R.RegFn

                    Try
                        Dim al_RstInfo_Calc As ArrayList = objDaRegRst.fnCalcRstInfo(r_sampinfo_Buf, r_al_RstInfo)

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
                        APP_R.LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 계산식 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                    End Try
                    '>

                    '-- 2008/11/28 YEJ Add
                    '-- 1-2) 결과값 자동 등록
                    Try
                        Dim al_RstInfo_Cvt As ArrayList = objDaRegRst.fnCvtRstInfo(r_sampinfo_Buf, r_al_EditSuc, m_dbTran, m_dbCn)

                        If Not al_RstInfo_Cvt Is Nothing Then
                            If al_RstInfo_Cvt.Count > 0 Then
                                For i As Integer = 1 To al_RstInfo_Cvt.Count
                                    rstinfo_Buf = CType(al_RstInfo_Cvt(i - 1), STU_RstInfo)

                                    If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                                        iRegOK_Sum += 1

                                        r_al_EditSuc.Add(rstinfo_Buf.TestCd)
                                    End If
                                Next
                            End If
                        End If
                    Catch ex As Exception
                        APP_R.LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 결과값 자동변환 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                    End Try
                End If

                '2) Sub 항목 에 대한 상태 재조정(Parent 및 Child)
                fnEdit_LR_Parent(r_sampinfo_Buf)

                '3) Battery
                fnEdit_LR_Battery(r_sampinfo_Buf)

                '4) Update LJ011M  ''' 업데이트할 칼럼 없음 
                If r_al_EditSuc.Count > 0 Then
                    fnEdit_LJ011(r_sampinfo_Buf)
                End If

                '5) Update LJ010M
                If r_al_EditSuc.Count > 0 Then
                    fnEdit_LJ010(r_sampinfo_Buf)
                End If

                '6) Update LM013M - 균
                iRegOK_Sum += fnEdit_BAC(r_sampinfo_Buf)

                '7) Update LM014M - 항균제
                fnEdit_ANTI(r_sampinfo_Buf)

                '8) Upate LR040M(검사분류별 소견)
                If r_sampinfo_Buf.EqCd = "" Then
                    Call fnEdit_lr040M(r_sampinfo_Buf)
                Else
                    Call fnEdit_LR040M_EQ(r_sampinfo_Buf)
                End If

                '9) 감염정보  
                If fnEdit_OCS_INF(r_sampinfo_Buf) Then
                Else
                    m_dbTran.Rollback()
                    Return 0
                End If

                '10) OCS 결과등록    정은 잠깐막음... ocs테이블 생기면 수정해야함 
                If fnEdit_OCS(r_sampinfo_Buf, r_al_EditSuc.Count) Then
                    m_dbTran.Commit()
                    Return iRegOK_Sum + r_al_EditSuc.Count
                Else
                    m_dbTran.Rollback()
                    Return 0
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

        Public Function RegServer(ByVal r_al_RstInfo As ArrayList, ByVal r_sampinfo_Buf As STU_SampleInfo, ByRef r_al_EditSuc As ArrayList, ByVal rbSpecialTest As Boolean) As Integer
            Dim sFn As String = "Function RegServer"

            Try

                Dim iRegOK_Sum As Integer = 0
                Dim rstinfo_Buf As STU_RstInfo

                If r_al_EditSuc Is Nothing Then r_al_EditSuc = New ArrayList

                '0) Cn, Transaction 생성
                m_dbCn = GetDbConnection()
                m_dbTran = m_dbCn.BeginTransaction()

                COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

                Dim al_Cmt As New ArrayList
                Dim iPos As Integer

                '-- 2007/11/05 ssh
                al_Cmt.Clear()

                '1) 결과개수만큼 등록
                For i As Integer = 1 To r_al_RstInfo.Count
                    rstinfo_Buf = CType(r_al_RstInfo(i - 1), STU_RstInfo)

                    '-- Cmt 관련
                    iPos = InStr(rstinfo_Buf.EqFlag, "/")

                    If iPos > 0 Then
                        al_Cmt.Add(rstinfo_Buf.EqFlag)
                        rstinfo_Buf.EqFlag = ""
                    End If

                    If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                        iRegOK_Sum += 1
                        r_al_EditSuc.Add(rstinfo_Buf.TestCd)
                    End If
                Next

                If r_sampinfo_Buf.EqCd <> "" Then
                    '< add yooej 2009/08/05 
                    '1-1) 계산식 관련항목 등록
                    Dim objDaRegRst As New APP_R.RegFn

                    Try
                        Dim al_RstInfo_Calc As ArrayList = objDaRegRst.fnCalcRstInfo(r_sampinfo_Buf, r_al_RstInfo)

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
                        APP_R.LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 계산식 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                    End Try
                    '>

                    '-- 2008/11/28 YEJ Add
                    '-- 1-2) 결과값 자동 등록
                    Try
                        Dim al_RstInfo_Cvt As ArrayList = objDaRegRst.fnCvtRstInfo(r_sampinfo_Buf, r_al_EditSuc, m_dbTran, m_dbCn)

                        If Not al_RstInfo_Cvt Is Nothing Then
                            If al_RstInfo_Cvt.Count > 0 Then
                                For i As Integer = 1 To al_RstInfo_Cvt.Count
                                    rstinfo_Buf = CType(al_RstInfo_Cvt(i - 1), STU_RstInfo)

                                    If fnRegServer(rstinfo_Buf, r_sampinfo_Buf) Then
                                        iRegOK_Sum += 1

                                        r_al_EditSuc.Add(rstinfo_Buf.TestCd)
                                    End If
                                Next
                            End If
                        End If
                    Catch ex As Exception
                        APP_R.LogFn.Log(r_sampinfo_Buf.SenderID, "RegServer 결과값 자동변환 오류 - " + r_sampinfo_Buf.EqBCNo + " : " + r_sampinfo_Buf.BCNo)
                    End Try
                End If

                '2) Sub 항목 에 대한 상태 재조정(Parent 및 Child)
                m_al_ParentCd = New ArrayList

                For i As Integer = 1 To r_al_EditSuc.Count
                    For j As Integer = 1 To m_dt_rst.Rows.Count
                        If m_dt_rst.Rows(j - 1).Item("tclscd").ToString().IndexOf(r_al_EditSuc(i - 1).ToString().Substring(0, 5)) >= 0 _
                                And m_dt_rst.Rows(j - 1).Item("tcdgbn").ToString() = "P" Then
                            If m_al_ParentCd.Contains(m_dt_rst.Rows(j - 1).Item("tclscd")) = False Then
                                m_al_ParentCd.Add(m_dt_rst.Rows(j - 1).Item("tclscd"))
                            End If
                        End If
                    Next
                Next


                If m_al_ParentCd.Count > 0 Then
                    fnEdit_LR_Parent(r_sampinfo_Buf)
                End If

                '3) Update LJ011M
                If r_al_EditSuc.Count > 0 Then
                    fnEdit_LJ011(r_sampinfo_Buf)
                End If

                '4) Update LJ010M
                If r_al_EditSuc.Count > 0 Then
                    fnEdit_LJ010(r_sampinfo_Buf)
                End If

                '5) LRS10M
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

                    '6) LRG10M
                    'sbEdit_LRG10M(rstinfo_Buf, r_sampinfo_Buf)
                End If

                '7) Update LR020M - 소견
                fnEdit_CMT(r_sampinfo_Buf)
                If al_Cmt.Count > 0 Then
                    fnEdit_CMT(r_sampinfo_Buf, al_Cmt)
                End If

                '8) 감염정보
                If fnEdit_OCS_INF(r_sampinfo_Buf) Then
                Else
                    m_dbTran.Rollback()
                    Return 0
                End If

                '9) OCS 결과등록
                If fnEdit_OCS(r_sampinfo_Buf, r_al_EditSuc.Count) Then
                    m_dbTran.Commit()
                    Return iRegOK_Sum
                Else
                    m_dbTran.Rollback()
                    Return 0
                End If

                '10) 종합검증 처방발생 '선생님컨펌전까지 막기:20140423
                If rstinfo_Buf.RegStep = "3" Then
                    If rstinfo_Buf.TestCd = "LV101" Then
                        Call sbGv_hit(r_sampinfo_Buf)
                    End If
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

        Private Sub sbGv_hit(ByVal r_sampinfo As STU_SampleInfo)
            Dim sFn As String = "sbGv_hit"

            Dim dt As DataTable = fnGv_Tk_Rows(r_sampinfo.BCNo.ToString)
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
                '    MsgBox("Column ID 오류발생")

                '    Return
                'End If

                Dim stu As New COMMON.SVar.STU_GVINFO

                stu.REGNO = dt.Rows(0).Item("regno").ToString  ' 환자번호
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

            Try
                'If Not rsBcNo.Length.Equals(11) Then Return ""

                sSql = "SELECT regno, entdt FROM lj010m WHERE bcno = '" + rsBcNo + "'"

                DbCommand()
                dt = DbExecuteQuery(sSql)


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

        Private Function fnEdit_ANTI(ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = ""

            Try
                If al_Bac Is Nothing Or al_Anti Is Nothing Then Return 0
                If al_Anti.Count < 1 Then Return 0

                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim iEditedRow As Integer = 0
                Dim sTestCds As String = ""

                For intIdx As Integer = 0 To al_Bac.Count - 1
                    If sTestCds.IndexOf(CType(al_Bac(intIdx), ResultInfo_Bac).TestCd) < 0 Then
                        sTestCds += CType(al_Bac(intIdx), ResultInfo_Bac).TestCd + ","
                    End If
                Next
                sTestCds = "'" + sTestCds.Substring(0, sTestCds.Length - 1).Replace(",", "', '") + "'"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                If r_sampinfo_Buf.EqCd <> "" Then
                    For ix As Integer = 0 To al_Bac.Count - 1
                        sSql = ""
                        sSql += "INSERT INTO lm013h "
                        sSql += "SELECT :moddt, :modid, :modip, a.*   FROM lm013m a"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND testcd IN (" + sTestCds + ")"
                        sSql += "   AND bacseq = '" + CType(al_Bac.Item(ix), ResultInfo_Bac).BacSeq + "'"

                        With dbCmd
                            .CommandText = sSql
                            .Parameters.Clear()
                            .Parameters.Add("moddt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString
                            .Parameters.Add("modid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("modip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                        End With
                        iEditedRow = dbCmd.ExecuteNonQuery()
                    Next
                Else
                    sSql = ""
                    sSql += "INSERT INTO lm013h "
                    sSql += "SELECT :moddt, :modid, :modip, a.*   FROM lm013m a"
                    sSql += " WHERE bcno    = :bcno"
                    sSql += "   AND testcd IN (" + sTestCds + ")"

                    With dbCmd
                        .CommandText = sSql
                        .Parameters.Clear()
                        .Parameters.Add("moddt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString
                        .Parameters.Add("modid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("modip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                    End With
                    iEditedRow = dbCmd.ExecuteNonQuery()
                End If

                If r_sampinfo_Buf.EqCd <> "" Then
                    For ix As Integer = 0 To al_Bac.Count - 1
                        sSql = ""
                        sSql += "DELETE lm013m"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND testcd IN (" + sTestCds + ")"
                        sSql += "   AND bacseq = '" + CType(al_Bac.Item(ix), ResultInfo_Bac).BacSeq + "'"

                        With dbCmd
                            .CommandText = sSql
                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                        End With
                        iEditedRow = dbCmd.ExecuteNonQuery()
                    Next
                Else
                    sSql = ""
                    sSql += "DELETE lm013m"
                    sSql += " WHERE bcno    = :bcno"
                    sSql += "   AND testcd IN (" + sTestCds + ")"

                    With dbCmd
                        .CommandText = sSql
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    End With

                    iEditedRow = dbCmd.ExecuteNonQuery()

                End If

                'Insert --> LM014M
                If al_Anti Is Nothing Then Return 0

                iEditedRow = 0

                For i As Integer = 1 To al_Anti.Count
                    sSql = ""
                    sSql += "INSERT INTO lm013m"
                    sSql += "          (  bcno,    testcd,  spccd,          baccd,  bacseq,  testmtd,  anticd,  regno,  decrst,  antirst, anticmt, eqcd,  rptyn,"
                    sSql += "             editid,  editip,  editdt )"
                    sSql += "    VALUES( :bcno,   :testcd, :spccd,         :baccd, :bacseq, :testmtd, :anticd, :regno, :decrst, :antirst, null,    null, :rptyn,"
                    sSql += "            :editid, :editip, fn_ack_sysdate)"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).TestCd
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).SpcCd
                        .Parameters.Add("baccd", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).BacCd
                        .Parameters.Add("bacseq", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).BacSeq
                        .Parameters.Add("testmtd", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).TestMtd
                        .Parameters.Add("anticd", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).AntiCd
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("regno").ToString
                        .Parameters.Add("decrst", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).DecRst
                        .Parameters.Add("antirst", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).AntiRst
                        .Parameters.Add("rptyn", OracleDbType.Varchar2).Value = CType(al_Anti(i - 1), ResultInfo_Anti).RptYn
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    End With

                    iEditedRow += dbCmd.ExecuteNonQuery()
                Next

                Return iEditedRow
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_BAC(ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_BAC(STU_SampleInfo) As Integer"

            Try
                If al_Bac Is Nothing Then Return 0
                If al_Bac.Count < 1 Then Return 0

                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim iEditedRow As Integer = 0
                Dim sTestCds As String = ""

                For intIdx As Integer = 0 To al_Bac.Count - 1
                    If sTestCds.IndexOf(CType(al_Bac(intIdx), ResultInfo_Bac).TestCd) < 0 Then
                        sTestCds += CType(al_Bac(intIdx), ResultInfo_Bac).TestCd + ","
                    End If
                Next

                sTestCds = "'" + sTestCds.Substring(0, sTestCds.Length - 1).Replace(",", "', '") + "'"

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                If r_sampinfo_Buf.EqCd <> "" Then
                    For ix As Integer = 0 To al_Bac.Count - 1
                        sSql = ""
                        sSql += "INSERT INTO lm012h "
                        sSql += "SELECT :moddt, :modid, :modip, a.*  FROM lm012m a"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND testcd IN (" + sTestCds + ")"
                        sSql += "   AND bacseq  = '" + CType(al_Bac(ix), ResultInfo_Bac).BacSeq + "'"

                        With dbCmd
                            .CommandText = sSql
                            .Parameters.Clear()
                            .Parameters.Add("moddt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString
                            .Parameters.Add("modid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("modip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        End With

                        iEditedRow = dbCmd.ExecuteNonQuery()
                    Next
                Else
                    sSql = ""
                    sSql += "INSERT INTO lm012h "
                    sSql += "SELECT :moddt, :modid, :modip, a.*  FROM lm012m a"
                    sSql += " WHERE bcno    = :bcno"
                    sSql += "   AND testcd IN (" + sTestCds + ")"

                    With dbCmd
                        .CommandText = sSql
                        .Parameters.Clear()
                        .Parameters.Add("moddt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString
                        .Parameters.Add("modid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("modip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    End With

                    iEditedRow = dbCmd.ExecuteNonQuery()

                End If

                If r_sampinfo_Buf.EqCd <> "" Then
                    For ix As Integer = 0 To al_Bac.Count - 1
                        sSql = ""
                        sSql += "DELETE lm012m"
                        sSql += " WHERE bcno    = :bcno"
                        sSql += "   AND testcd IN (" + sTestCds + ")"
                        sSql += "   AND bacseq  = '" + CType(al_Bac(ix), ResultInfo_Bac).BacSeq + "'"

                        With dbCmd
                            .CommandText = sSql
                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        End With

                        iEditedRow = dbCmd.ExecuteNonQuery()
                    Next
                Else

                    sSql = ""
                    sSql += "DELETE lm012m"
                    sSql += " WHERE bcno    = :bcno"
                    sSql += "   AND testcd IN (" + sTestCds + ")"

                    With dbCmd
                        .CommandText = sSql
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    End With

                    iEditedRow = dbCmd.ExecuteNonQuery()
                End If

                'Insert --> LM012M
                If al_Bac Is Nothing Then Return 0

                iEditedRow = 0

                For i As Integer = 1 To al_Bac.Count
                    sSql = ""
                    sSql += "INSERT INTO lm012m"
                    sSql += "          (  bcno,    testcd,  spccd,         baccd,  bacseq,  incrst,  testmtd,  regno,  baccmt,  ranking, eqcd,  rstflg,  rstdt,"
                    sSql += "             editid,  editip,  editdt)"
                    sSql += "    VALUES( :bcno,   :testcd, :spccd,        :baccd, :bacseq, :incrst, :testmtd, :regno, :baccmt, :ranking, null, :rstflg, :rstdt,"
                    sSql += "            :editid, :editip, fn_ack_sysdate)"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("testcd", OracleDbType.Varchar2).Value = CType(al_Bac(i - 1), ResultInfo_Bac).TestCd
                        .Parameters.Add("spccd", OracleDbType.Varchar2).Value = CType(al_Bac(i - 1), ResultInfo_Bac).SpcCd
                        .Parameters.Add("baccd", OracleDbType.Varchar2).Value = CType(al_Bac(i - 1), ResultInfo_Bac).BacCd
                        .Parameters.Add("bacseq", OracleDbType.Int32).Value = CType(al_Bac(i - 1), ResultInfo_Bac).BacSeq
                        .Parameters.Add("incrst", OracleDbType.Varchar2).Value = CType(al_Bac(i - 1), ResultInfo_Bac).IncRst
                        .Parameters.Add("testmtd", OracleDbType.Varchar2).Value = CType(al_Bac(i - 1), ResultInfo_Bac).TestMtd
                        .Parameters.Add("regno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("regno")
                        .Parameters.Add("baccmt", OracleDbType.Varchar2).Value = CType(al_Bac(i - 1), ResultInfo_Bac).BacCmt

                        If r_sampinfo_Buf.EqCd = "" Then
                            .Parameters.Add("ranking", OracleDbType.Int32).Value = CType(al_Bac(i - 1), ResultInfo_Bac).Ranking
                        Else
                            .Parameters.Add("ranking", OracleDbType.Int32).Value = CType(al_Bac(i - 1), ResultInfo_Bac).BacSeq
                        End If

                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_sampinfo_Buf.RegStep.Substring(0, 1)
                        .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(0).Item("curdt").ToString
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    End With

                    iEditedRow += dbCmd.ExecuteNonQuery()
                Next

                Return iEditedRow
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
                sSql += "  WHERE bcno   = :bcno"
                sSql += "    AND testcd = :testcd"

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
                sSql += "             bcno,  testcd,  rstflg,  rsttxt,  rstrtf, rstdt,           rstid,  editid,  editip, editdt )"
                sSql += "    VALUES( :bcno, :testcd, :rstflg, :rsttxt, :rstrtf, fn_ack_sysdate, :rstid, :editid, :editip, fn_ack_sysdate )"

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = r_rstinfo_Buf.TestCd
                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_sampinfo_Buf.RegStep.Substring(0, 1)
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
                    sSql += "    VALUES( :bcno, :testcd,      1, :filenm, :filelen, :filebin )"

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
                    sSql += "            VALUES( :bcno, :testcd, :rstno, :filenm, :filelen, :filebin)"

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

        Private Function fnEdit_CMT(ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_CMT(STU_SampleInfo) As Integer"

            Try
                '추가/수정/삭제 내용 없을 경우는 Nothing
                If al_Cmt Is Nothing Then Return 0

                Dim dbCmd As New OracleCommand

                Dim sSql As String = ""

                Dim iEditedRow As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                sSql = ""
                sSql += "INSERT INTO lr020h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, r.*"
                sSql += "  FROM lr020m r"
                sSql += " WHERE bcno = :bcno"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("modid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("modip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                iEditedRow = dbCmd.ExecuteNonQuery()

                'Delete --> LR020M
                sSql = ""
                sSql += "DELETE FROM lr020m"
                sSql += " WHERE bcno = :bcno"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                End With

                iEditedRow = dbCmd.ExecuteNonQuery()

                'Insert --> LR020M
                If al_Cmt Is Nothing Then Return 0

                iEditedRow = 0

                For i As Integer = 1 To al_Cmt.Count
                    sSql = ""
                    sSql += "INSERT INFO lr020m"
                    sSql += "          (  bcno,  rstseq,  cmt,  regid, regdt,           editid,  editip, editdt)"
                    sSql += "    VALUES( :bcno, :rstseq, :cmt, :regid, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate)"

                    dbCmd.CommandText = sSql

                    With dbCmd
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = CType(al_Cmt(i - 1), ResultInfo_Cmt).BcNo
                        .Parameters.Add("rstseq", OracleDbType.Varchar2).Value = CType(al_Cmt(i - 1), ResultInfo_Cmt).RstSeq
                        .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(al_Cmt(i - 1), ResultInfo_Cmt).Cmt
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    End With

                    iEditedRow += dbCmd.ExecuteNonQuery()
                Next

                Return iEditedRow
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 2007.11.05 ssh 수정
        Private Function fnEdit_CMT(ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal alCmt As ArrayList) As Integer
            Dim sFn As String = "Private Function fnEdit_CMT(STU_SampleInfo, ArrayList) As Integer"

            Try
                '추가/수정/삭제 내용 없을 경우는 Nothing
                ''If al_Cmt Is Nothing Then Return 0
                If alCmt Is Nothing Then Return 0

                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim iEditedRow As Integer = 0

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                Dim iPos As Integer
                Dim sComment As String = ""
                Dim sRstNo As String = ""

                For i As Integer = 0 To alCmt.Count - 1
                    iPos = InStr(Convert.ToString(alCmt(i)), "/")

                    If iPos <= 0 Then
                        GoTo NEXT_CMT
                    End If

                    sComment = Split(Convert.ToString(alCmt(i)), "/")(1)

                    sSql = ""
                    sSql += "SELECT cmt"
                    sSql += "  FROM lr020m"
                    sSql += " WHERE bcno = '" + r_sampinfo_Buf.BCNo + "'"
                    sSql += "   AND cmt  = '" + sComment + "'"

                    dbCmd.CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    dt.Reset()
                    dbDa.Fill(dt)

                    If dt.Rows.Count > 1 Then Return 1

                    sSql = ""
                    sSql += "SELECT MAX(rstseq) + 1 "
                    sSql += "  FROM lr020m "
                    sSql += " WHERE bcno = '" + r_sampinfo_Buf.BCNo + "'"

                    dbCmd.CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    dt.Reset()
                    dbDa.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        sRstNo = dt.Rows(0).Item(0).ToString()
                    End If

                    If sRstNo.Trim() = "" Then sRstNo = "1"

                    sSql = ""
                    sSql += "INSERT INTO lr020m("
                    sSql += "                   bcno, rstseq, cmt, regid, regdt, editid, editip, editdt)"
                    sSql += "            values('" + r_sampinfo_Buf.BCNo + "', '" + sRstNo + "', '" + sComment + "', '" + r_sampinfo_Buf.UsrID & "',"
                    sSql += "                   fn_ack_sysdate, '" + r_sampinfo_Buf.UsrID + "', '" + r_sampinfo_Buf.UsrIP + "', fn_ack_sysdate )"

                    dbCmd.CommandText = sSql
                    iEditedRow += dbCmd.ExecuteNonQuery()

NEXT_CMT:
                Next

                Return iEditedRow
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_lr040M(ByVal r_sampinfo_Buf As STU_SampleInfo) As String
            Dim sFn As String = "Public Function fnEdit_LR040M(object) As String"

            If al_Cmt Is Nothing Then Return ""
            If al_Cmt.Count < 1 Then Return ""

            Try
                Dim sSql As String = ""
                Dim dbCmd As New OracleCommand

                Dim alSlipCd As New ArrayList

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                For ix As Integer = 0 To al_Cmt.Count - 1
                    If alSlipCd.Contains(CType(al_Cmt(ix), STU_CvtCmtInfo).SlipCd) Then
                    Else
                        alSlipCd.Add(CType(al_Cmt(ix), STU_CvtCmtInfo).SlipCd)

                        sSql = ""
                        sSql += "DELETE lr040m"
                        sSql += " WHERE bcno   = :bcno"
                        sSql += "   AND partcd = :partcd"
                        sSql += "   AND slipcd = :slipcd"

                        With dbCmd
                            .CommandText = sSql
                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(al_Cmt(ix), STU_CvtCmtInfo).SlipCd.Substring(0, 1)
                            .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(al_Cmt(ix), STU_CvtCmtInfo).SlipCd.Substring(1, 1)
                            .ExecuteNonQuery()
                        End With
                    End If

                    sSql = ""
                    sSql += "INSERT INTO lr040m"
                    sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt, regdt,           regid,  editid,  editip, editdt )"
                    sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, fn_ack_sysdate, :regid, :editid, :editip, fn_ack_sysdate)"

                    With dbCmd
                        .CommandText = sSql
                        .Parameters.Clear()
                        .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                        .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(al_Cmt(ix), STU_CvtCmtInfo).SlipCd.Substring(0, 1)
                        .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(al_Cmt(ix), STU_CvtCmtInfo).SlipCd.Substring(1, 1)
                        .Parameters.Add("rstseq", OracleDbType.Int32).Value = ix + 1
                        .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(al_Cmt(ix), STU_CvtCmtInfo).CmtCont
                        .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                        .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                        If .ExecuteNonQuery() < 1 Then Return "Error"
                    End With

                Next
                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        '-- 
        Public Function fnEdit_LR040M_EQ(ByVal r_sampinfo_Buf As STU_SampleInfo) As String

            Dim sFn As String = "Public Function fnEdit_LR040M(object) As String"

            Try
                Dim sSql As String = ""
                Dim sTableNm As String = "lr010m"
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim alCmtVal As New ArrayList
                Dim alRstInfo As New ArrayList

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran

                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT r.testcd, r.orgrst, r.viewrst, r.hlmark, r.eqflag"
                sSql += "  FROM " + sTableNm + " r"
                sSql += " WHERE r.bcno               = :bcno"
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
                        sSql += "          (  bcno,  partcd,  slipcd,  rstseq,  cmt, regdt,           regid,  editid,  editip, editdt )"
                        sSql += "    values( :bcno, :partcd, :slipcd, :rstseq, :cmt, fn_ack_sysdate, :regid, :editid, :editip, fn_ack_sysdate)"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("partcd", OracleDbType.Varchar2).Value = CType(alCmtVal(ix), STU_CvtCmtInfo).SlipCd.Substring(0, 1)
                            .Parameters.Add("slipcd", OracleDbType.Varchar2).Value = CType(alCmtVal(ix), STU_CvtCmtInfo).SlipCd.Substring(1, 1)
                            .Parameters.Add("rstseq", OracleDbType.Int32).Value = ix + 1
                            .Parameters.Add("cmt", OracleDbType.Varchar2).Value = CType(alCmtVal(ix), STU_CvtCmtInfo).CmtCont
                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP
                        End With

                        If dbCmd.ExecuteNonQuery() < 1 Then Return "Error"
                    End If
                Next

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


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
                sSql += "UPDATE lj010m SET rstflg = :rstflg, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
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

                Dim sTable As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then sTable = "lm010m"

                sSql = ""
                sSql += "SELECT r.tclscd, r.spccd, MIN(NVL(r.rstflg, '0')) minrstflg, MAX(NVL(r.rstflg, '0')) maxrstflg, MAX(NVL(r.rstdt, ' ')) rstdt"
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
                    If dt.Rows(ix - 1).Item("minrstflg").ToString() = dt.Rows(ix - 1).Item("maxrstflg").ToString() Then
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString() = "0" And dt.Rows(ix - 1).Item("maxrstflg").ToString() = "3" Then
                        sRstFlg = "2"
                        '2018-07-02 yjh 조건 추가 AFB 고체, 액체 배지 관련해서 수정
                    ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString() = "1" And dt.Rows(ix - 1).Item("maxrstflg").ToString() = "3" Then
                        sRstFlg = "2"
                    ElseIf dt.Rows(ix - 1).Item("minrstflg").ToString() = "0" And dt.Rows(ix - 1).Item("maxrstflg").ToString() <= "2" Then
                        sRstFlg = dt.Rows(ix - 1).Item("maxrstflg").ToString()
                    Else
                        sRstFlg = dt.Rows(ix - 1).Item("minrstflg").ToString()
                    End If

                    sSql = ""

                    Select Case sRstFlg
                        Case "0"
                            sSql += "UPDATE lj011m SET rstflg = NULL, rstdt = NULL, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"
                            sSql += "   AND spccd  = :spccd"
                            sSql += "   AND spcflg = '4'"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()
                                .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("tclscd").ToString()
                                .Parameters.Add("spccd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("spccd").ToString()
                            End With

                        Case Else
                            sSql += "UPDATE lj011m SET rstflg = :rstflg, rstdt = :rstdt, editid = :editid, editip = :editip, editdt = fn_ack_sysdate"
                            sSql += " WHERE bcno   = :bcno"
                            sSql += "   AND tclscd = :tclscd"
                            sSql += "   AND spccd  = :spccd"
                            sSql += "   AND spcflg = '4'"

                            dbCmd.CommandText = sSql

                            With dbCmd
                                .Parameters.Clear()
                                .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                                .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("rstdt").ToString()
                                .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                                .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("tclscd").ToString()
                                .Parameters.Add("spccd", OracleDbType.Varchar2).Value = dt.Rows(ix - 1).Item("spccd").ToString()
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
                    '''If r_rstinfo_Buf.TestCd = m_dt_rst.Rows(i - 1).Item("tclscd").ToString() Then
                    If r_rstinfo_Buf.TestCd = m_dt_rst.Rows(i - 1).Item("testcd").ToString().Trim Then
                        iR = i - 1

                        Exit For
                    End If
                Next

                If r_rstinfo_Buf.EqFlag Is Nothing Then r_rstinfo_Buf.EqFlag = ""

                If iR = -1 Then Return 0
                '3) ViewRst
                Dim sViewRst As String = fnEdit_LR_ViewRst(iR, r_rstinfo_Buf.OrgRst)

                If r_rstinfo_Buf.ChageRst <> "" Then sViewRst = r_rstinfo_Buf.ChageRst

                '4) Delta Mark
                Dim sDM As String = fnEdit_LR_DM(iR, r_rstinfo_Buf.OrgRst, sViewRst)

                '5) Panic Mark
                Dim sPM As String = fnEdit_LR_PM(iR, r_rstinfo_Buf.OrgRst, r_rstinfo_Buf.TestCd)

                '6) Critical Mark
                Dim sCM As String = fnEdit_LR_CM(iR, r_rstinfo_Buf.OrgRst, r_rstinfo_Buf.CriticalMark)

                '7) Alert Mark
                Dim sAM As String = fnEdit_LR_AM(iR, r_rstinfo_Buf.OrgRst, sViewRst, r_rstinfo_Buf.EqFlag, sPM, sDM, r_rstinfo_Buf.AlertMark)

                '2) 번을 이쪽으로 옮김...
                If fnEdit_LR_ViolateNum(iR, r_rstinfo_Buf.OrgRst) Then sAM = "E"

                '8) L/H
                Dim sLH As String = fnEdit_LR_LH(iR, r_rstinfo_Buf.OrgRst)

                '9) N/P
                Dim sNP As String = fnEdit_LR_NP(iR, r_rstinfo_Buf.TestCd, r_rstinfo_Buf.OrgRst)

                If sNP <> "" Then sLH = sNP

                With r_rstinfo_Buf
                    .ViewRst = sViewRst
                    .DeltaMark = sDM
                    .PanicMark = sPM
                    .CriticalMark = sCM
                    .AlertMark = sAM
                    .HlMark = sLH

                    '.RegStep = r_sampinfo_Buf.RegStep
                    If r_sampinfo_Buf.RegStep = "2" Then
                        If sDM <> "" Or sAM <> "" Then
                            .RegStep = "1"
                        ElseIf sPM <> "" Or sCM <> "" Then
                            .RegStep = "2"
                        Else
                            .RegStep = "3"
                        End If
                    ElseIf r_sampinfo_Buf.RegStep = "22" Then
                        .RegStep = "2"
                    Else
                        .RegStep = r_sampinfo_Buf.RegStep
                    End If

                    If .RstCmt Is Nothing Then .RstCmt = ""
                End With

                '9) Update Or Insert 해당 Item
                '''Return fnEdit_LR_Item(iR, r_rstinfo_Buf, r_sampinfo_Buf)
                Return fnEdit_LR_Item_new(iR, r_rstinfo_Buf, r_sampinfo_Buf)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Function fnEdit_LR_AM(ByVal riR As Integer, ByVal rsOrgRst As String, ByVal rsViewRst As String, ByVal rsEqFlag As String,
                                        ByVal rsPanicMark As String, ByVal rsDeltaMark As String, Optional ByVal rsAlertMark As String = "") As String
            Dim sFn As String = "Private Function fnEdit_LR_AM(Integer, String, String, tring, String, String) As String"

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
                    Case "7"    '-- 문자값 비고
                        Return rsAlertMark
                End Select

                '-- Alert Rule 사용
                If sAlertGbn = "5" Or sAlertGbn = "A" Or sAlertGbn = "B" Or sAlertGbn = "C" Then
                    Dim iCnt As Integer = 0, iAlert As Integer = 0

                    If m_dt_rst.Rows(riR).Item("a_sex").ToString().Trim <> "" Then
                        iCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_sex").ToString().Trim = m_dt_rst.Rows(riR).Item("sex").ToString().Trim Then iAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_deptcd").ToString().Trim <> "" Then
                        iCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_deptcd").ToString().Trim.IndexOf(m_dt_rst.Rows(riR).Item("deptcd").ToString().Trim + ",") >= 0 Then iAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_orgrst").ToString().Trim <> "" Then
                        iCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_orgrst").ToString().Trim.IndexOf(rsOrgRst + ",") >= 0 Then iAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_viewrst").ToString().Trim <> "" Then
                        iCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_viewrst").ToString().Trim.IndexOf(rsViewRst + ",") >= 0 Then iAlert += 1
                    End If

                    If rsPanicMark <> "" Then
                        iCnt += 1
                        iAlert += 1
                    End If

                    If rsDeltaMark <> "" Then
                        iCnt += 1
                        iAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_eqflag").ToString().Trim <> "" And rsEqFlag <> "" Then
                        iCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_eqflag").ToString().Trim.IndexOf("^") >= 0 Then
                            Dim strBuf() As String = m_dt_rst.Rows(riR).Item("a_eqflag").ToString().Split("^"c)

                            If strBuf(1) = "" Then
                                If strBuf(0) = "" Then
                                    iAlert += 1
                                Else
                                    strBuf(0) += ","
                                    If strBuf(0).IndexOf(rsEqFlag + ",") >= 0 Then iAlert += 1
                                End If
                            Else
                                If strBuf(0) = "" Then
                                    strBuf(1) += ","
                                    If strBuf(1).IndexOf(m_dt_rst.Rows(riR).Item("testcd").ToString().Trim + ",") >= 0 Then iAlert += 1
                                Else
                                    strBuf(0) += "," : strBuf(1) += ","
                                    If strBuf(0).IndexOf(rsEqFlag + ",") >= 0 And strBuf(1).IndexOf(m_dt_rst.Rows(riR).Item("testcd").ToString().Trim + ",") >= 0 Then iAlert += 1
                                End If
                            End If
                        Else
                            If m_dt_rst.Rows(riR).Item("a_eqflag").ToString().IndexOf(rsEqFlag + ",") >= 0 Then iAlert += 1
                        End If
                    End If

                    If m_dt_rst.Rows(riR).Item("a_spccd").ToString().Trim <> "" Then
                        iCnt += 1
                        If m_dt_rst.Rows(riR).Item("a_spccd").ToString().IndexOf(m_dt_rst.Rows(riR).Item("spccd").ToString().Trim + ",") >= 0 Then iAlert += 1
                    End If

                    If m_dt_rst.Rows(riR).Item("a_baccd").ToString.Trim <> "" Then
                        iCnt += 1
                        For ix As Integer = 0 To al_Bac.Count - 1
                            If m_dt_rst.Rows(riR).Item("testcd").ToString = CType(al_Bac(ix), ResultInfo_Bac).TestCd And
                               m_dt_rst.Rows(riR).Item("a_baccd").ToString.IndexOf(CType(al_Bac(ix), ResultInfo_Bac).BacCd + ",") >= 0 Then iAlert += 1
                        Next
                    End If

                    If m_dt_rst.Rows(riR).Item("a_anticalc").ToString.Trim <> "" Then
                        iCnt += 1

                        If al_Anti.Count > 0 Then

                            Dim sCalcForm As String = m_dt_rst.Rows(riR).Item("a_anticalc").ToString.Trim

                            For ix As Integer = 0 To al_Anti.Count - 1
                                sCalcForm = sCalcForm.ToUpper.Replace("#B", "'" + CType(al_Anti(ix), ResultInfo_Anti).BacCd.ToUpper + "'")
                                sCalcForm = sCalcForm.ToUpper.Replace("[" + CType(al_Anti(ix), ResultInfo_Anti).AntiCd.ToUpper + "]", "'" + CType(al_Anti(ix), ResultInfo_Anti).DecRst + "'")
                            Next

                            sCalcForm = sCalcForm.Replace("$$", "AND").Replace("||", "OR").Replace("[", "'").Replace("]", "'")

                            If LISAPP.COMM.RstFn.fnGet_Calc_DBQuery(sCalcForm, m_dbCn, m_dbTran) = "1" Then iAlert += 1

                        End If
                    End If

                    If iCnt > 0 And iAlert > 0 Then Return "A"

                End If
                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_ViolateNum(ByVal riR As Integer, ByVal rsOrgRst As String) As Boolean
            Dim sRstType As String = m_dt_rst.Rows(riR).Item("rsttype").ToString()

            'RstType : 0 --> 문자 + 숫자 혼합, 1 --> 숫자만 허용
            If sRstType = "1" Then
                If IsNumeric(rsOrgRst) = False Then
                    Return True
                End If
            End If

            Return False
        End Function

        Private Function fnEdit_LR_CM(ByVal riR As Integer, ByVal rsOrgRst As String, Optional ByVal rsCriticalmark As String = "") As String
            Dim sFn As String = "Private Function fnEdit_LR_CM(Integer, String) As String"

            Try
                Dim sCriticalGbn As String = m_dt_rst.Rows(riR).Item("criticalgbn").ToString().Trim

                If sCriticalGbn Is Nothing Then Return ""

                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")


                Dim sCriticalL As String = m_dt_rst.Rows(riR).Item("criticall").ToString().Trim
                Dim sCriticalH As String = m_dt_rst.Rows(riR).Item("criticalh").ToString().Trim
                Dim sRefL As String = m_dt_rst.Rows(riR).Item("refl").ToString().Trim
                Dim sRefH As String = m_dt_rst.Rows(riR).Item("refh").ToString().Trim
                Dim strTclscd As String = m_dt_rst.Rows(riR).Item("testcd").ToString().Trim


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
                        ''Critical 문자값 판단 추가(검사마스터에서 Critical 구분 [7] 문자결과(결과코드 설정) 선택, 기초마스터 결과코드에 Critical 설정한 경우 )
                        'Dim sTxtCritical As String = ""
                        'sTxtCritical = LISAPP.COMM.RstFn.fnGet_GraedValue_C(strTclscd, rsOrgRst)

                        'If strTclscd = "LM205" Then 'xpert pcr 검사가 Critical이라도 해당 환자의 1주일전 pcr검사 이력이 Deteted(Critical)일 경우 Normal결과로 판단
                        '    Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_AFB_Comment(m_dt_rst.Rows(riR).Item("bcno").ToString().Trim)

                        '    If dt.Rows.Count > 0 Then
                        '        Exit Function
                        '    ElseIf dt.Rows.Count <= 0 Then
                        '        Return sTxtCritical
                        '        ' If sTxtCritical = "C" Then msXpertC = True Else msXpertC = False
                        '    End If
                        'Else
                        '    '임시막음
                        '    Return sTxtCritical
                        'End If
                        Return rsCriticalmark
                End Select

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

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
                sSql += "   AND spccd   = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "'"
                sSql += "   and rstcont = :rstcont"

                dbCmd.CommandText = sSql
                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTclsCd
                    .SelectCommand.Parameters.Add("rstcont", OracleDbType.Varchar2).Value = rsRstVal
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then sValue = dt.Rows(0).Item(0).ToString()

                Return sValue

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

       

        Private Function fnGet_RstLvlValue(ByVal rsTtestCd As String, ByVal rsRstVal As String) As String
            Dim sFn As String = "Private Function fnGet_RstLvlValue(String, String) As String"

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
                sSql += "SELECT rstlvl FROM lf083m"
                sSql += " WHERE testcd  = :testcd"
                sSql += "   AND spccd   = '" + "".PadRight(PRG_CONST.Len_SpcCd, "0"c) + "'"
                sSql += "   and rstcont = :rstcont"

                dbCmd.CommandText = sSql
                dbDa = New OracleDataAdapter(dbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                    .SelectCommand.Parameters.Add("testcd", OracleDbType.Varchar2).Value = rsTtestCd
                    .SelectCommand.Parameters.Add("rstcont", OracleDbType.Varchar2).Value = rsRstVal
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then sValue = dt.Rows(0).Item(0).ToString()
                If sValue = "" Then sValue = "N"

                Return sValue
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        Private Function fnEdit_LR_DM(ByVal riR As Integer, ByVal rsOrgRst As String, ByVal asViewRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_DM(Integer, String, String) As String"

            Try
                Dim sDeltaGbn As String = m_dt_rst.Rows(riR).Item("deltagbn").ToString().Trim

                If sDeltaGbn Is Nothing Then Return ""
                If sDeltaGbn = "" Then Return ""

                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                Dim sDeltaL As String = m_dt_rst.Rows(riR).Item("deltal").ToString().Trim
                Dim sDeltaH As String = m_dt_rst.Rows(riR).Item("deltah").ToString().Trim
                Dim sDeltaDay As String = m_dt_rst.Rows(riR).Item("deltaday").ToString().Trim

                '결과테이블의 이전결과, 조회해온 이전결과
                Dim sBFOrgRst As String = m_dt_rst.Rows(riR).Item("bforgrst_b").ToString().Trim
                Dim sBFFnDt As String = m_dt_rst.Rows(riR).Item("bffndt_b").ToString().Trim
                Dim sCurDt As String = m_dt_rst.Rows(riR).Item("curdt").ToString().Trim

                '이전결과가 없거나 숫자가 아닐 경우
                If sBFOrgRst.Trim = "" Then Return ""

                sBFOrgRst = sBFOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

                If sBFFnDt = "" Then sBFFnDt = sCurDt

                If sBFFnDt.Length = 8 Then
                    sBFFnDt = sBFFnDt.Insert(4, "-").Insert(7, "-") + " 00:00:00"
                Else
                    sBFFnDt = sBFFnDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")
                End If
                sCurDt = sCurDt.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":")

                Select Case sDeltaGbn
                    Case "1", "2", "3", "4"
                        If IsNumeric(rsOrgRst) = False Then Return ""
                        If IsNumeric(sBFOrgRst) = False Then Return ""
                End Select

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
                        Dim sGrade As String = "", sGrade_Old As String = ""
                        Dim sTestCd As String = m_dt_rst.Rows(riR).Item("tclscd").ToString().Trim

                        sGrade = fnGet_GraedValue(sTestCd, rsOrgRst)
                        sGrade_Old = fnGet_GraedValue(sTestCd, sBFOrgRst)
                        If sGrade <> "" And sGrade_Old <> "" Then
                            If IsNumeric(sDeltaH) And Math.Abs(Val(sGrade) - Val(sGrade_Old)) >= Math.Abs(Val(sDeltaH)) Then
                                Return "D"
                            End If
                        End If

                End Select

                Return ""
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_Item_new(ByVal riR As Integer, ByVal r_rstinfo_Buf As STU_RstInfo, ByVal r_sampinfo_Buf As STU_SampleInfo) As Integer
            Dim sFn As String = "Private Function fnEdit_LR_Item_new(Integer, STU_RstInfo, STU_SampleInfo) As Integer"

            Try
                'Application에서 Call 하므로 RegStep은 그대로 사용됨

                Dim sNewRstNo As String = ""
                Dim sSql As String = ""

                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text

                dbCmd.CommandText = "SELECT sq_lm011m.nextval FROM DUAL"

                With dbCmd
                    .Parameters.Clear()
                    .ExecuteNonQuery()
                End With


                'Backup
                sSql = ""
                sSql += "INSERT INTO lm011m("
                sSql += "       bcno, testcd, spccd, orgrst, viewrst, rstcmt, deltamark, panicmark, criticalmark, alertmark, hlmark, "
                sSql += "       regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg, rstdt, bfbcno, bffndt, eqcd, eqflag, sysdt,"
                sSql += "       editdt, editid, editip, seq) "
                sSql += "SELECT bcno, testcd, spccd, orgrst, viewrst, rstcmt, deltamark, panicmark, criticalmark, alertmark, hlmark, "
                sSql += "       regid, regdt, mwid, mwdt, fnid, fndt, cfmnm, cfmsign, cfmyn, rstflg, rerunflg, rstdt, bfbcno, bffndt, eqcd, eqflag, :sysdt,"
                sSql += "       editdt, editid, editip, sq_lm011m.nextval"
                sSql += "  FROM lm010m"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"

                If r_sampinfo_Buf.EqCd = "" Then
                    sSql += "   AND (NVL(regid, ' ') <> ' ' OR NVL(mwid, ' ') <>  ' ' OR NVL(fnid, ' ') <> ' ')"
                    sSql += "   AND (orgrst <> '" + r_rstinfo_Buf.OrgRst + "' OR viewrst <> '" + r_rstinfo_Buf.ViewRst + "')"
                Else
                    sSql += "   AND NVL(rstflg, '0') <> '3'"
                    sSql += "   AND NVL(hlmark, ' ') <> 'P'"
                End If
                sSql += "   AND NVL(orgrst,  ' ') <> ' '"
                sSql += "   AND NVL(viewrst, ' ') <> ' '"

                dbCmd.CommandText = sSql

                With dbCmd
                    .Parameters.Clear()

                    .Parameters.Add("sysdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt")
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString()
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString()

                End With

                Dim intRet As Integer = dbCmd.ExecuteNonQuery()
                '''End If ''' rstno 

                'Update
                sSql = ""
                sSql += "UPDATE lm010m SET"
                sSql += "       orgrst       = :orgrst,"
                sSql += "       viewrst      = :viewrst,"
                sSql += "       deltamark    = :deltamark,"
                sSql += "       panicmark    = :panicmark,"
                sSql += "       criticalmark = :criticalmark,"
                sSql += "       alertmark    = :alertmark,"
                sSql += "       hlmark       = :hlmark,"
                If r_sampinfo_Buf.EqCd <> "" Then
                    sSql += "       regid        = CASE WHEN NVL(regid, ' ') = ' ' THEN :regid ELSE regid END,"
                    sSql += "       regdt        = CASE WHEN NVL(regdt, ' ') = ' ' THEN :regdt ELSE regdt END,"
                    sSql += "       wkymd        = NVL(wkymd, TO_CHAR(SYSDATE, 'yyyymmdd')),"
                    sSql += "       wkdt         = NVL(wkdt,  TO_CHAR(SYSDATE, 'yyyymmddhh24miss')),"
                Else
                    sSql += "       regid        = :regid,"
                    sSql += "       regdt        = :regdt,"
                End If
                sSql += "       mwid         = :mwid,"
                sSql += "       mwdt         = :mwdt,"
                sSql += "       fnid         = :fnid,"
                sSql += "       fndt         = :fndt,"
                sSql += "       cfmnm        = :cfmnm,"
                sSql += "       cfmsign      = :cfmsign,"
                sSql += "       rstflg       = :rstflg,"
                sSql += "       rstdt        = :rstdt,"
                sSql += "       rstcmt       = :rstcmt,"
                If r_sampinfo_Buf.EqCd <> "" Then
                    sSql += "       eqcd         = :eqcd,"
                    sSql += "       eqflag       = :eqflag,"
                    sSql += "       cfmyn        = 'N',"
                Else
                    sSql += "       cfmyn        = 'Y',"
                End If
                sSql += "       fregdt       = CASE WHEN NVL(fregdt, ' ') =  ' ' THEN :fregdt ELSE fregdt END,"
                sSql += "       editdt       = fn_ack_sysdate,"
                sSql += "       editid       = :editid,"
                sSql += "       editip       = :editip"
                sSql += " WHERE bcno   = :bcno"
                sSql += "   AND testcd = :testcd"
                If r_sampinfo_Buf.EqCd <> "" Then
                    sSql += "   AND NVL(rstflg, '0') <> '3'"
                    sSql += "   AND NVL(hlmark, ' ') <> 'P'"
                End If

                With dbCmd
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
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString
                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = DBNull.Value

                        Case "2"
                            If m_dt_rst.Rows(riR).Item("regid").ToString() = "" Then
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString
                            Else
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regid").ToString()
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regdt").ToString
                            End If

                            .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString
                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = DBNull.Value
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = DBNull.Value

                        Case "3"
                            If m_dt_rst.Rows(riR).Item("regid").ToString() = "" Then
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt")
                            Else
                                .Parameters.Add("regid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regid").ToString()
                                .Parameters.Add("regdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("regdt").ToString
                            End If

                            If m_dt_rst.Rows(riR).Item("mwid").ToString() = "" Then
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt")
                            Else
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("mwid").ToString()
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("mwdt").ToString
                            End If

                            .Parameters.Add("fnid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("fndt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt")

                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("cfmnm_f").ToString
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("cfmsign").ToString

                    End Select

                    .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RegStep
                    .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString

                    .Parameters.Add("rstcmt", OracleDbType.Varchar2).Value = r_rstinfo_Buf.RstCmt
                    If r_sampinfo_Buf.EqCd <> "" Then
                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                        .Parameters.Add("eqflag", OracleDbType.Varchar2).Value = r_rstinfo_Buf.EqFlag
                    End If

                    .Parameters.Add("fregdt", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("curdt").ToString

                    .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                    .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("bcno").ToString()
                    .Parameters.Add("testcd", OracleDbType.Varchar2).Value = m_dt_rst.Rows(riR).Item("testcd").ToString()

                    Return .ExecuteNonQuery()


              



                End With
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnEdit_LR_LH(ByVal riR As Integer, ByVal rsOrgRst As String) As String
            Dim sFn As String = "Private Function fnEdit_LR_LH(ByVal riR As Integer, ByVal rsOrgRst As String) As String"

            Try
                Dim sRefGbn As String = m_dt_rst.Rows(riR).Item("refgbn").ToString().Trim

                If sRefGbn Is Nothing Then Return ""

                rsOrgRst = rsOrgRst.Replace(">", "").Replace("<", "").Replace("=", "")

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

        Private Function fnEdit_LR_NP(ByVal riR As Integer, ByVal rsTestCd As String, ByVal rsOrgRst As String) As String
            Dim sMbtType As String = m_dt_rst.Rows(riR).Item("mbttype").ToString().Trim

            If Not (sMbtType = "2" Or sMbtType = "3") Then Return ""

            If rsOrgRst = FixedVariable.gsRst_Nogrowth Then
                Return "N"
            ElseIf rsOrgRst = FixedVariable.gsRst_Growth Then
                Return "P"
            Else
                Return fnGet_RstLvlValue(rsTestCd, rsOrgRst)
            End If

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
                sSql += "       MAX(NVL(r.rstflg, '0')) maxrstflg, MIN(NVL(r.rstflg, '0')) rstflg, MAX(NVL(r.rstdt, '')) rstdt, MAX(NVL(r.eqcd, '')) eqcd,"
                sSql += "       MAX(NVL(hlmark, '')) hlmark, SUBSTR(r.testcd, 1, 5) testcd, r.spccd"
                sSql += "  FROM " + sTable + " r, lf060m f"
                sSql += " WHERE r.bcno   = :bcno"
                sSql += "   AND (NVL(r.orgrst, ' ') <> ' ' OR (f.tcdgbn = 'C' AND NVL(f.reqsub, '0') = '1') OR (f.tcdgbn = 'P' AND f.titleyn = '0'))"
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   AND r.tkdt  <  f.uedt"
                sSql += "   AND ((f.tcdgbn = 'P' AND NVL(f.mbttype, '0') NOT IN ('1', '2')) OR f.tcdgbn = 'C')"
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
                    Dim sPN As String = dt_p.Rows(ix).Item("hlmark").ToString
                    Dim sEqCd As String = dt_p.Rows(ix).Item("eqcd").ToString



                    Dim a_dr As DataRow() = m_dt_rst.Select("testcd = '" + dt_p.Rows(ix).Item("testcd").ToString + "'", "")



                    If sRstFlg = "3" Then
                        sSql = ""
                        sSql += "UPDATE " + sTable + " SET"
                        If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                            sSql += "       eqcd    = :eqcd,"

                            If sTable = "lr010m" Then
                                sSql += "       eqseqno = :eqseqno,"
                                sSql += "       eqrack  = :eqrack,"
                                sSql += "       eqpos   = :eqpos,"
                            End If
                        End If

                        If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                            sSql += "       hlmark   = NVL(hlmark, :hlmark),"
                        End If

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
                        sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR"
                        sSql += "        (testcd, spccd, '1') IN "
                        sSql += "        (SELECT f.testcd, f.spccd, f.titleyn FROM lf060m f, " + sTable + " r"
                        sSql += "          WHERE r.bcno   = :bcno"
                        sSql += "            AND r.testcd LIKE :testcd || '%'"
                        sSql += "            AND r.testcd = f.testcd"
                        sSql += "            AND r.spccd  = f.spccd"
                        sSql += "            AND f.usdt  <= r.tkdt"
                        sSql += "            AND f.uedt  >  r.tkdt"
                        sSql += "            AND tcdgbn   = 'P'"
                        sSql += "        ) OR "
                        sSql += "        (testcd, spccd, '1') IN "
                        sSql += "        (SELECT f.testcd, f.spccd, f.mbttype FROM lf060m f, " + sTable + " r"
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

                                If sTable = "lr010m" Then
                                    .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                    .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                    .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                End If
                            End If

                            If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                                .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = sPN
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
                                    If sTable = "lr010m" Then
                                        sSql += "       eqseqno = :eqseqno,"
                                        sSql += "       eqrack  = :eqrack,"
                                        sSql += "       eqpos   = :eqpos,"

                                    End If
                                End If

                                If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                                    sSql += "       hlmark   = NVL(hlmark, :hlmark),"
                                End If

                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NULL,          mwdt  = NULL,"
                                sSql += "       fnid   = NULL,          fndt  = NULL,"
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
                                sSql += "            AND f.uedt  <  r.tkdt"
                                sSql += "            AND tcdgbn   = 'P'"
                                sSql += "        )"
                                sSql += "       )"


                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                        If sTable = "lr010m" Then
                                            .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                            .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                            .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                        End If
                                    End If

                                    If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                                        .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = sPN
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
                                    If sTable = "lr010m" Then
                                        sSql += "       eqseqno = :eqseqno,"
                                        sSql += "       eqrack  = :eqrack,"
                                        sSql += "       eqpos   = :eqpos,"
                                    End If
                                End If

                                If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                                    sSql += "       hlmark   = NVL(hlmark, :hlmark),"
                                End If

                                sSql += "       rstflg = :rstflg,"
                                sSql += "       rstdt  = :rstdt,"
                                sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt  = NVL(mwdt,  :mwdt),"
                                sSql += "       fnid   = NULL,          fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND testcd LIKE :testcd || '%'"
                                sSql += "   AND (NVL(orgrst, ' ') <> ' ' OR "
                                sSql += "        (testcd, spccd, '1') IN "
                                sSql += "        (SELECT f.testcd, f.spccd, NVL(f.titleyn, '0') FROM lf060m f, " + sTable + " r"
                                sSql += "          WHERE r.bcno   = :bcno"
                                sSql += "            AND r.testcd LIKE :testcd || '%'"
                                sSql += "            AND r.testcd = f.testcd"
                                sSql += "            AND r.spccd  = f.spccd"
                                sSql += "            AND f.usdt  <= r.tkdt"
                                sSql += "            AND f.uedt  <  r.tkdt"
                                sSql += "            AND tcdgbn   = 'P'"
                                sSql += "        )"
                                sSql += "       )"

                                dbCmd.CommandText = sSql

                                With dbCmd
                                    .Parameters.Clear()
                                    If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                        .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                        If sTable = "lr010m" Then
                                            .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                            .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                            .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                        End If
                                    End If

                                    If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                                        .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = sPN
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
                                    sRstFlg = "1"

                                    sSql = ""
                                    sSql += "UPDATE " + sTable + " SET"
                                    If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                        sSql += "       eqcd    = :eqcd,"
                                        If sTable = "lr010m" Then
                                            sSql += "       eqseqno = :eqseqno,"
                                            sSql += "       eqrack  = :eqrack,"
                                            sSql += "       eqpos   = :eqpos,"

                                        End If
                                    End If

                                    If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                                        sSql += "       hlmark   = NVL(hlmark, :hlmark),"
                                    End If

                                    sSql += "       rstflg = :rstflg,"
                                    sSql += "       rstdt  = :rstdt,"
                                    sSql += "       regid  = NVL(regid, :regid), regdt = NVL(regdt, :regdt),"
                                    sSql += "       mwid   = NULL,               mwdt  = NULL,"
                                    sSql += "       fnid   = NULL,               fndt  = NULL,"
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
                                    sSql += "            AND f.uedt  <  r.tkdt"
                                    sSql += "            AND tcdgbn   = 'P'"
                                    sSql += "        )"
                                    sSql += "       )"


                                    dbCmd.CommandText = sSql

                                    With dbCmd
                                        .Parameters.Clear()
                                        If r_sampinfo_Buf.EqCd <> "" And sEqCd = r_sampinfo_Buf.EqCd Then
                                            .Parameters.Add("eqcd", OracleDbType.Varchar2).Value = r_sampinfo_Buf.EqCd
                                            If sTable = "lr010m" Then
                                                .Parameters.Add("eqseqno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.IntSeqNo
                                                .Parameters.Add("eqrack", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Rack
                                                .Parameters.Add("eqpos", OracleDbType.Varchar2).Value = r_sampinfo_Buf.Pos
                                            End If
                                        End If

                                        If a_dr(0).Item("mbttype").ToString = "1" Or a_dr(0).Item("mbttype").ToString = "2" Then
                                            .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = sPN
                                        End If

                                        .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = "1"
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
                                End If

                        End Select

                    End If

                    If Not sSql = "" Then
                        Dim iRet As Integer = dbCmd.ExecuteNonQuery()
                    End If

                    If dt_p.Rows(ix).Item("testcd").ToString.Length = 5 And a_dr(0).Item("mbttype").ToString = "2" And sPN <> "" Then
                        sSql = ""
                        sSql += "UPDATE " + sTable + " SET"
                        sSql += "       orgrst  = CASE WHEN NVL(hlmark, ' ') = 'P' THEN orgrst  ELSE :orgrst END,"
                        sSql += "       viewrst = CASE WHEN NVL(hlmark, ' ') = 'P' THEN viewrst ELSE :viewrst END,"
                        sSql += "       hlmark  = :hlmark,"
                        sSql += "       rstflg  = :rstflg,"
                        sSql += "       rstdt   = :rstdt,"
                        sSql += "       regid   = NVL(regid, :regid), regdt   = NVL(regdt, :regdt),"
                        If sRstFlg = "2" Then
                            sSql += "       mwid    = NVL(mwid,  :mwid), mwdt    = NVL(mwdt,  :mwdt),"
                        ElseIf sRstFlg = "3" Then
                            sSql += "       mwid    = NVL(mwid,  :mwid), mwdt    = NVL(mwdt,  :mwdt),"
                            sSql += "       fnid    = NVL(fnid,  :fnid), fndt    = :fndt,"
                            sSql += "       cfmnm   = :cfmnm,            cfmsign = :cfmsign, cfmyn = CASE WHEN cfmyn = 'Y' THEN cfmyn ELSE 'N' END,"
                        End If

                        sSql += "       editdt  = fn_ack_sysdate,"
                        sSql += "       editid  = :editid,"
                        sSql += "       editip  = :editip"
                        sSql += " WHERE bcno   = :bcno"
                        sSql += "   AND testcd = :testcd"

                        dbCmd.CommandText = sSql

                        With dbCmd
                            .Parameters.Clear()

                            If sPN = "P" Then
                                .Parameters.Add("orgrst", OracleDbType.Varchar2).Value = FixedVariable.gsRst_Growth
                                .Parameters.Add("viewrst", OracleDbType.Varchar2).Value = FixedVariable.gsRst_Growth
                            Else
                                .Parameters.Add("orgrst", OracleDbType.Varchar2).Value = FixedVariable.gsRst_Nogrowth
                                .Parameters.Add("viewrst", OracleDbType.Varchar2).Value = FixedVariable.gsRst_Nogrowth
                            End If

                            .Parameters.Add("hlmark", OracleDbType.Varchar2).Value = sPN
                            .Parameters.Add("rstflg", OracleDbType.Varchar2).Value = sRstFlg
                            .Parameters.Add("rstdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                            .Parameters.Add("regid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("regdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()

                            If sRstFlg = "2" Then
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                            ElseIf sRstFlg = "3" Then
                                .Parameters.Add("mwid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("mwdt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                .Parameters.Add("fnid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                                .Parameters.Add("fndt", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("rstdt").ToString()
                                .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmnm_f").ToString
                                .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmsign").ToString
                            End If

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("testcd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("testcd").ToString()

                            .ExecuteNonQuery()
                        End With
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
                sSql += " WHERE r.bcno     = :bcno"
                sSql += "   AND r.tclscd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND r.tkdt    >= f.usdt"
                sSql += "   AND r.tkdt    <  f.uedt"
                sSql += "   AND r.tclscd   = f62.tclscd"
                sSql += "   AND r.spccd    = f62.tspccd"
                sSql += "   AND r.testcd   = f62.testcd"
                sSql += "   AND r.spccd    = f62.spccd"
                sSql += "   AND f62.grprstyn = '1'"
                sSql += "   AND f.tcdgbn   = 'B'"
                sSql += "   AND f.grprstyn = '1'"
                sSql += "   AND r.testcd  <> r.tclscd"
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

                    If sRstFlg = "0" And sRstFlg_max = "2" Then sRstFlg = "2" '-- 2013/08/30 YEJ 추가

                    If sRstFlg = "3" Then
                        Dim a_dr As DataRow() = m_dt_rst.Select("tclscd = '" + dt_p.Rows(ix).Item("tclscd").ToString + "' AND rstdt = '" + dt_p.Rows(ix).Item("rstdt").ToString + "'", "")

                        sSql += ""
                        sSql += "UPDATE " + sTable + ""
                        sSql += "   SET rstflg = :rstflg,"
                        sSql += "       rstdt  = :rstdt,"
                        sSql += "       regid  = NVL(regid, :regid), regdt   = NVL(regdt, :regdt),"
                        sSql += "       mwid   = NVL(mwid,  :mwid),  mwdt    = NVL(mwdt,  :mwdt),"
                        sSql += "       fnid   = NVL(fnid,  :fnid),  fndt    = :fndt,"
                        sSql += "       cfmnn  = :cfmnm,             cfmsign = :cfmsign,  cfmyn = :cfmyn,"
                        sSql += "       editdt = fn_ack_sysdate,"
                        sSql += "       editid = :editid,"
                        sSql += "       editip = :editip"
                        sSql += " WHERE bcno   = :bcno"
                        sSql += "   AND tclscd = :tclscd"
                        sSql += "   AND NVL(orgrst, ' ') <> ' '"
                        sSql += "   AND rstflg <> '3'"
                        sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                        sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m FROM grprstyn = '1'))"

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
                            .Parameters.Add("cfmnm", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmnm_f").ToString
                            .Parameters.Add("cfmsign", OracleDbType.Varchar2).Value = a_dr(0).Item("cfmsign").ToString

                            If r_sampinfo_Buf.EqCd = "" Then
                                .Parameters.Add("cfmyn", OracleDbType.Varchar2).Value = "Y"
                            Else
                                .Parameters.Add("cfmyn", OracleDbType.Varchar2).Value = "N"
                            End If

                            .Parameters.Add("editid", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrID
                            .Parameters.Add("editip", OracleDbType.Varchar2).Value = r_sampinfo_Buf.UsrIP

                            .Parameters.Add("bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                            .Parameters.Add("tclscd", OracleDbType.Varchar2).Value = dt_p.Rows(ix).Item("tclscd").ToString()
                        End With
                    Else
                        sSql = ""
                        Select Case sRstFlg
                            Case "0"
                                If sRstFlg_max = "3" Then
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
                                    sSql += " WHERE bcno   = :bcno"
                                    sSql += "   AND tclscd = :tclscd"
                                    sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                    sSql += "   AND rstflg = '3'"
                                    sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                    sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m FROM grprstyn = '1'))"

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
                                sSql += "       regid  = NVL(r.regid, :regid), regdt = NVL(regdt, :regdt),"
                                sSql += "       mwid   = NULL,                 mwdt  = NULL,"
                                sSql += "       fnid   = NULL,                 fndt  = NULL,"
                                sSql += "       editdt = fn_ack_sysdate,"
                                sSql += "       editid = :editid,"
                                sSql += "       editip = :editip"
                                sSql += " WHERE bcno   = :bcno"
                                sSql += "   AND tclscd = :tclscd"
                                sSql += "   AND NVL(orgrst, ' ') <> ' '"
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m FROM grprstyn = '1'))"

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
                                sSql += "UPDATE " + sTable + ""
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
                                sSql += "   AND (tclscd, spccd, SUBSTR(testcd, 1, 5)) IN"
                                sSql += "       (SELECT tclscd, tspccd, testcd FROM lf062m FROM grprstyn = '1'))"

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

                        If Not sSql = "" Then
                            Dim iRet As Integer = dbCmd.ExecuteNonQuery()
                        End If
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
                            If Val(strGrade) < Val(sPanicL) Then
                                Return "P"
                            End If
                        End If

                    Case "5"
                        strGrade = fnGet_GraedValue(rsTclsCd, rsOrgRst)

                        If strGrade <> "" Then
                            If Val(strGrade) > Val(sPanicH) Then
                                Return "P"
                            End If
                        End If

                    Case "6"
                        strGrade = fnGet_GraedValue(rsTclsCd, rsOrgRst)

                        If strGrade <> "" Then
                            If Val(strGrade) < Val(sPanicL) Then
                                Return "P"
                            End If

                            If Val(strGrade) > Val(sPanicH) Then
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
                        sViewRst = sConvRst

                        Select Case m_dt_rst.Rows(riR).Item("cutopt").ToString()
                            Case "1"    '올림
                                '반올림- 원값 
                                '    7 - 6.9999 =  0.0001   --> 7
                                '    7 - 7      =  0        --> 7
                                '    7 - 7.0001 = -0.0001   --> 8
                                If Val(sConvRst) - Val(rsOrgRst) < 0 Then
                                    sViewRst = CStr(Val(sConvRst) + (10 ^ -Val(sLLen)))
                                End If

                            Case "2"    '반올림
                                sViewRst = sConvRst

                            Case "3"    '내림
                                '반올림- 원값 
                                '    7 - 6.9999 =  0.0001   --> 6
                                '    7 - 7      =  0        --> 7
                                '    7 - 7.0001 = -0.0001   --> 7
                                If Val(sConvRst) - Val(rsOrgRst) > 0 Then
                                    sViewRst = Format(CStr(Val(sConvRst) - (10 ^ -Val(sLLen))), sFmt)
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
                Dim sALimitGbn As String = m_dt_rst.Rows(riR).Item("alimitgbn").ToString()

                If sALimitGbn Is Nothing Then Return rsNewOrgRst

                Dim iAL As Integer = 0, iAH As Integer = 0

                Try
                    If rsOldOrgRst.Substring(0, 2) = ">=" Or rsOldOrgRst.Substring(0, 2) = "<=" Then

                        rsOldOrgRst = rsOldOrgRst.Substring(2).Trim

                    ElseIf rsOldOrgRst.Substring(0, 1) = ">" Or rsOldOrgRst.Substring(0, 1) = "<" Then

                        rsOldOrgRst = rsOldOrgRst.Substring(1).Trim

                    End If
                Catch ex As Exception

                End Try

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
                        If Val(rsOldOrgRst) < Val(sALimitL) Then
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
                        If Val(rsOldOrgRst) > Val(sALimitH) Then
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
                Dim sRefH As String = "", sRefL As String = ""
                Dim sULT1 As String = "", sULT2 As String = "", sULT3 As String = ""
                Dim sMark As String = ""
                'JudgType : 0 --> 미사용, 1 --> L/H, 212222 --> 사용자정의 2단계, 312322332 --> 사용자정의 3단계
                Dim sJudgType As String = m_dt_rst.Rows(riR).Item("judgtype").ToString()

                If sJudgType Is Nothing Then Return rsNewOrgRst

                Try
                    If rsOldOrgRst.Substring(0, 2) = ">=" Or rsOldOrgRst.Substring(0, 2) = "<=" Then

                        rsOldOrgRst = rsOldOrgRst.Substring(2).Trim
                        sMark = rsOldOrgRst.Substring(0, 2).Trim
                    ElseIf rsOldOrgRst.Substring(0, 1) = ">" Or rsOldOrgRst.Substring(0, 1) = "<" Then

                        rsOldOrgRst = rsOldOrgRst.Substring(1).Trim
                        sMark = rsOldOrgRst.Substring(0, 1).Trim

                    End If
                Catch ex As Exception

                End Try

                sULT1 = m_dt_rst.Rows(riR).Item("ujudglt1").ToString().Trim
                sULT2 = m_dt_rst.Rows(riR).Item("ujudglt2").ToString().Trim
                sULT3 = m_dt_rst.Rows(riR).Item("ujudglt3").ToString().Trim

                '몫 : 2 --> 사용자정의 2단계, 3 --> 사용자정의 3단계
                Select Case Len(sJudgType) \ 3
                    Case 2
                        '상한값과 결과값 비교
                        sRefH = m_dt_rst.Rows(riR).Item("refh").ToString().Trim

                        If IsNumeric(sRefH) Then
                            '0 --> 등호 포함 , 1 --> 부등호
                            If m_dt_rst.Rows(riR).Item("refhs").ToString().Trim = "0" Then
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

        Private Function fnEdit_OCS_INF(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean
            Dim sFn As String = "Private Function fnEdit_OCS_INF(ByVal r_sampinfo_Buf As STU_SampleInfo) As Boolean"

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Dim sErrVal As String = ""

            Try
                Dim sSql As String = "pro_ack_exe_ocs_rst_inf"
                If PRG_CONST.BCCLS_MicorBio.Contains(r_sampinfo_Buf.BCNo.Substring(8, 2)) Then
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
                    .Parameters("rs_retval").Value = sErrVal

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

        Private Function fnEdit_OCS(ByVal r_sampinfo_Buf As STU_SampleInfo, ByVal r_iAntiCnt As Integer) As Boolean
            Dim sFn As String = ""

            Dim dbCmd As New OracleCommand
            Dim dbDa As New OracleDataAdapter
            Dim dt As New DataTable

            Dim sErrVal As String = ""

            Try
                '-- OCS에 결과 올리기
                Dim sSql As String = "pro_ack_exe_ocs_rst"
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
                    .Parameters("rs_errmsg").Value = sErrVal

                    .ExecuteNonQuery()

                    sErrVal = .Parameters(3).Value.ToString
                End With

                If sErrVal.StartsWith("00") Then
                    'Return True
                Else
                    Throw (New Exception(sErrVal.Substring(2)))
                End If

                If r_iAntiCnt > 0 Then
                    sSql = "pro_ack_exe_ocs_rst_m_anti"

                    With dbCmd
                        .Connection = m_dbCn
                        .Transaction = m_dbTran
                        .CommandType = CommandType.StoredProcedure
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo

                        .Parameters.Add("rs_errmsg", OracleDbType.Varchar2, 4000)
                        .Parameters("rs_errmsg").Direction = ParameterDirection.InputOutput
                        .Parameters("rs_errmsg").Value = sErrVal

                        .ExecuteNonQuery()

                        sErrVal = .Parameters(1).Value.ToString
                    End With

                    If sErrVal.StartsWith("00") Then
                        'Return True
                    Else
                        Throw (New Exception(sErrVal.Substring(2)))
                    End If
                End If

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "pro_ack_exe_ocs_rstflg"

                    .Parameters.Clear()
                    .Parameters.Add("rs_bcno", OracleDbType.Varchar2).Value = r_sampinfo_Buf.BCNo
                    .Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
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
                    Throw (New Exception(sErrVal.Substring(2)))
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

                '2) Update LM010M, Insert LM011M
                Dim iEditRow As Integer = fnEdit_LR(r_rstinfo_Buf, r_sampinfo_Buf)

                If iEditRow = 0 Then Return False

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Private Sub sbGetRstInfo(ByVal rsBCNo As String)
            Dim sFn As String = ""

            Try

                If m_dt_rst Is Nothing Then
                    m_dt_rst = New DataTable
                Else
                    If m_dt_rst.Rows(0).Item("bcno").ToString().Trim = rsBCNo Then
                        Return
                    End If
                End If

                '20210202 jhs 속도개선을 위해 환경배양은 나눔
                'Dim sSql As String = "pkg_ack_rst.pkg_get_resultinfo_m"
                Dim sSql As String = ""

                If rsBCNo.Substring(8, 2) = "M6" Then
                    sSql = "pkg_ack_rst.pkg_get_resultinfo_m_m6" '환경배양 속도개선을 위해 프로시저 생성 기존 
                Else
                    sSql = "pkg_ack_rst.pkg_get_resultinfo_m"
                End If
                '-------------------------

                Dim dbCmd As New OracleCommand
                Dim objDAdapter As OracleDataAdapter

                dbCmd.Connection = m_dbCn
                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = sSql

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
End Namespace