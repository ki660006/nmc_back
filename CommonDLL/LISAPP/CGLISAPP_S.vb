'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : 관동대명지병원 Laboratory Information System(KMC_LIS)                  */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_S.vb                                                              */
'/* PartName     : 조회관리                                                               */
'/* Description  : 조회관리의 Data Query구문관련 Class                                    */
'/* Design       : 2003-10-06 Jin Hwa Ji                                                  */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.Drawing
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Namespace APP_S

#Region " 환자정보 : STU_PatInfo_S"
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

#Region "결과관련"
    Public Class RstSrh
        Private Const msFile As String = "File : CGLISAPP_S.vb, Class : LISAPP.APP_S.RstSrh" + vbTab

        '-- 이상자조회(작업그룹)
        Public Shared Function fnGet_AbnormalList_WGrp(ByVal rsWkYmd As String, ByVal rsWkgrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                       ByVal rsTestCds As String, ByVal rbFnYn As Boolean, ByVal rbMicroBioYn As Boolean, ByVal rCvrChk As Boolean) As DataTable
            Dim sFn As String = "fnGet_AbnormalList_Wkno"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"
                If rbMicroBioYn Then sTableNm = "lm010m"

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
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2 , "

                sSql += "       CASE WHEN cvr.barcodeno is not null THEN 'Y' ELSE 'N' END cvryn, "

                sSql += "       nvl2(r.criticalmark , rc.cmtcont,'') cmtcont "
                'sSql += "  FROM " + sTableNm + " r, lj010m j, lf030m f3, lf060m f6 , lr050m rc "

                sSql += "  FROM " + sTableNm + " r, lj010m j, lf030m f3, lf060m f6 , lr050m rc, lis_cvr_info cvr "

                sSql += " WHERE r.wkymd   = :wkymd"
                sSql += "   AND r.wkgrpcd = :wgrpcd"
                sSql += "   AND r.wkno   <= :wknos"
                sSql += "   AND r.wkno   >= :wknoe"

                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkgrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkgrpCd))
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
                sSql += "   AND r.bcno = rc.bcno (+)  " '<<<20180612 특이결과 등록 내용 추가 

                If rCvrChk Then
                    sSql += "   AND r.bcno = cvr.barcodeno "
                    sSql += "   AND r.testcd = cvr.dtltestcd "
                Else
                    sSql += "   AND r.bcno = cvr.barcodeno(+) "
                    sSql += "   AND r.testcd = cvr.dtltestcd(+)"
                End If


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 이상자조회(검사그룹)
        Public Shared Function fnGet_AbnormalList_Tgrp(ByVal rsSlipCd As String, ByVal rsTgrpCd As String, _
                                                       ByVal rsRstDtS As String, ByVal rsRstDtE As String, _
                                                       ByVal rsTestCds As String, ByVal rbFnYn As Boolean, ByVal rbMicroBioYn As Boolean, ByVal rCvrChk As Boolean) As DataTable
            Dim sFn As String = "fnGet_AbnormalList_Tgrp"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If rbMicroBioYn Then sTableNm = "lm010m"

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
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2 , "
                sSql += "       CASE WHEN cvr.barcodeno is not null THEN 'Y' ELSE 'N' END cvryn," '<< jjh CVR등록 y/n
                sSql += "       nvl2(r.criticalmark , rc.cmtcont,'') cmtcont "
                'sSql += "  FROM " + sTableNm + " r, lj010m j, lf030m f3, lf060m f6 , lr050m rc"

                sSql += "  FROM " + sTableNm + " r, lj010m j, lf030m f3, lf060m f6 , lr050m rc, lis_cvr_info cvr"

                sSql += " WHERE r.rstdt >= :dates"
                sSql += "   AND r.rstdt <= :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','").ToString + "')"
                ElseIf rsTgrpCd <> "" Then
                    sSql += "   AND (SUBSTR(r.testcd, 1, 5), r.spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"

                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTgrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTgrpCd))
                ElseIf rsSlipCd <> "" Then

                    If rsSlipCd.Length = 1 Then '<<<20170124 부서조회 추가 
                        sSql += "   AND f6.partcd = :partcd"

                        alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))

                    Else
                        sSql += "   AND f6.partcd = :partcd"
                        sSql += "   AND f6.slipcd = :slipcd"

                        alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                        alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                    End If

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
                sSql += "   AND r.bcno = rc.bcno (+)  " '<<<20180612 특이결과 등록 내용 추가 

                '<< jjh cvr등록여부
                If rCvrChk Then
                    sSql += "   AND r.bcno = cvr.barcodeno "
                    sSql += "   AND r.testcd = cvr.dtltestcd "
                Else
                    sSql += "   AND r.bcno = cvr.barcodeno(+) "
                    sSql += "   AND r.testcd = cvr.dtltestcd(+) "
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- 이상자조회(특수검사)
        Public Shared Function fnGet_Search_Rstval_SP(ByVal rsRstDtS As String, ByVal rsRstDtE As String, ByVal rbFn As Boolean, _
                                                        ByVal rsQryGbn As String, ByVal rbMicroBio As Boolean, ByVal rbSpcYn As Boolean) As DataTable

            Dim sFn As String = "fnGet_Search_Rstval_SP"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"
                If rbMicroBio Then sTableNm = "lm010m"

                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                '<20130704 정선영 수정
                'sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END deptinfo,"
                sSql += "       FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE '' END deptinfo,"
                '>
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       f3.spcnmd, f6.tnmd, NVL(f6.dispseql, 999) sort2, r.testcd, r.spccd,"
                sSql += "       '{null}' orgrst, r.viewrst, r.rstflg, fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       r.bforgrst, '' bfviewrst, fn_ack_get_bcno_full(r.bfbcno) bfbcno, fn_ack_date_str(r.bffndt, 'yyyy-mm-dd hh24:mi') bffndt,"
                sSql += "       r.hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       '' reftxt, '' panictxt, '' deltagbn, '' deltatxt, '' criticaltxt, '' alerttxt"
                sSql += "  FROM lj010m j, lr010m r, lrs10m rs, lf030m f3, lf060m f6"
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
                    If rbSpcYn Then
                        sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd || r.spccd").Replace("#ORGRST", "rs.rsttxt") + ")"
                    Else
                        sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd").Replace("#ORGRST", "rs.rsttxt") + ")"
                    End If
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

        Public Shared Function fnGet_Jubsu_Test(ByVal rsRstDtS As String, ByVal rsRstDtE As String, ByVal rsgbn As String) As DataTable
            Dim sFn As String = "fnGet_Search_rst_count"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Try

                If rsgbn = "1" Then
                    sSql = ""
                    sSql += "                select fn_ack_get_ward_name(x.wardno) warnm"
                    sSql += "  from"
                    sSql += "("
                    sSql += "select distinct a.regno , b.wardno, to_char(to_date(a.tkdt , 'yyyy-mm-dd hh24:mi:ss') , 'yyyy-mm-dd') tkd, to_char(to_date(a.tkdt , 'yyyy-mm-dd hh24:mi:ss') , 'hh24') tkh"
                    sSql += "  from lj011m a"
                    sSql += "  JOIN lj010m b "
                    sSql += "    ON a.bcno = b.bcno "
                    sSql += "   AND a.spccd = b.spccd"
                    sSql += " where tkdt between '20181201000000' and '20181231235959'"
                    sSql += "   and a.spcflg = '4'"
                    sSql += "   and a.spccd in ('S53' , 'S01')"
                    sSql += " ) x"
                    sSql += " group by x.wardno"
                    sSql += " order by x.wardno"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alParm)
                Else
                    sSql = ""
                    sSql += "                select count(x.regno) cnt, x.tkd , x.tkh , fn_ack_get_ward_name(x.wardno) warnm"
                    sSql += "  from"
                    sSql += "("
                    sSql += "select distinct a.regno , b.wardno, to_char(to_date(a.tkdt , 'yyyy-mm-dd hh24:mi:ss') , 'yyyy-mm') tkd, to_char(to_date(a.tkdt , 'yyyy-mm-dd hh24:mi:ss') , 'hh24') tkh"
                    sSql += "  from lj011m a"
                    sSql += "  JOIN lj010m b "
                    sSql += "    ON a.bcno = b.bcno "
                    sSql += "   AND a.spccd = b.spccd"
                    sSql += " where tkdt between '20181001000000' and '20181031235959'"
                    sSql += "   and a.spcflg = '4'"
                    sSql += "   and a.spccd in ('S53' , 'S01')"
                    sSql += " ) x"
                    sSql += " group by x.tkd , x.tkh ,x.wardno"
                    sSql += " order by x.wardno, x.tkd , x.tkh "

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                    DbCommand()
                    Return DbExecuteQuery(sSql, alParm)

                End If
               
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_Search_rst_count(ByVal rsRstDtS As String, ByVal rsRstDtE As String, ByVal rsQryGbn As String, ByVal rbMicroBio As Boolean, _
                                                       ByVal rbSpcYnn As Boolean, ByVal rsRstGbn As String) As DataTable
            Dim sFn As String = "fnGet_Search_rst_count"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim sTableNm As String = "lr010m"
            If rbMicroBio Then sTableNm = "lm010m"

            Dim days As Long = DateDiff(DateInterval.Day, CDate(rsRstDtS), CDate(rsRstDtE))

            Try
                sSql = ""
                sSql += "   SELECT '','검사코드' ," + vbCrLf
                For i As Integer = 0 To CInt(days)
                    If i = days Then
                        sSql += "'" + CStr(CDate(rsRstDtS).AddDays(i)) + "' as day" + (i + 1).ToString + vbCrLf
                    Else
                        sSql += "'" + CStr(CDate(rsRstDtS).AddDays(i)) + "' as day" + (i + 1).ToString + "," + vbCrLf
                    End If
                Next
                sSql += "     FROM DUAL" + vbCrLf
                sSql += "   UNION ALL" + vbCrLf
                'sSql += "      SELECT CASE WHEN r.testcd = 'LG104' " + vbCrLf
                'sSql += "                  THEN 'M. tuberculosis : DETECTED'" + vbCrLf
                'sSql += "                  WHEN r.testcd = 'LU141'" + vbCrLf
                'sSql += "                  THEN 'Negative' END as tnmd,r.testcd," + vbCrLf
                sSql += "       SELECT f.tnmd , r.testcd , " + vbCrLf
                For i As Integer = 0 To CInt(days)
                    If i = days Then
                        sSql += "             TO_CHAR(SUM(DECODE(TO_CHAR(TO_DATE(r.rstdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') , '" + CStr(CDate(rsRstDtS).AddDays(i)) + "' , 1 , 0))) rstcnt" + (i + 1).ToString + vbCrLf
                    Else
                        sSql += "             TO_CHAR(SUM(DECODE(TO_CHAR(TO_DATE(r.rstdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') , '" + CStr(CDate(rsRstDtS).AddDays(i)) + "' , 1 , 0))) rstcnt" + (i + 1).ToString + "," + vbCrLf
                    End If
                Next
                sSql += "        FROM " + sTableNm + " r" + vbCrLf
                sSql += "        JOIN lf060m f " + vbCrLf
                sSql += "          ON r.testcd = f.testcd " + vbCrLf
                sSql += "         AND r.spccd = f.spccd " + vbCrLf
                sSql += "         AND f.usdt <= r.tkdt " + vbCrLf
                sSql += "         AND f.uedt > r.tkdt " + vbCrLf
                sSql += "       WHERE r.rstdt  >= :dates" + vbCrLf
                sSql += "         AND r.rstdt  <= :datee || '235959'" + vbCrLf
                If rsQryGbn <> "" Then
                    If rbSpcYnn Then
                        If rsRstGbn = "O" Then
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd || r.spccd").Replace("#ORGRST", "r.orgrst") + ")" + vbCrLf
                        Else
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd || r.spccd").Replace("#ORGRST", "r.viewrst") + ")" + vbCrLf
                        End If
                    Else
                        If rsRstGbn = "O" Then
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd").Replace("#ORGRST", "r.orgrst") + ")" + vbCrLf
                        Else
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd").Replace("#ORGRST", "r.viewrst") + ")" + vbCrLf
                        End If
                    End If
                End If
                sSql += "          AND r.rstflg = '3'" + vbCrLf
                sSql += "      GROUP BY r.testcd , f.tnmd                  " + vbCrLf
                sSql += "   UNION ALL    " + vbCrLf
                sSql += "      SELECT 'Total' as tot, ''," + vbCrLf
                For i As Integer = 0 To CInt(days)
                    If i = days Then
                        sSql += "             TO_CHAR(SUM(DECODE(TO_CHAR(TO_DATE(r.rstdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') , '" + CStr(CDate(rsRstDtS).AddDays(i)) + "' , 1 , 0))) rstcnt" + (i + 1).ToString + vbCrLf
                    Else
                        sSql += "             TO_CHAR(SUM(DECODE(TO_CHAR(TO_DATE(r.rstdt, 'yyyy-mm-dd hh24:mi:ss'),'yyyy-mm-dd') , '" + CStr(CDate(rsRstDtS).AddDays(i)) + "' , 1 , 0))) rstcnt" + (i + 1).ToString + "," + vbCrLf
                    End If
                Next
                sSql += "        FROM " + sTableNm + " r" + vbCrLf
                sSql += "       WHERE r.rstdt  >= :dates" + vbCrLf
                sSql += "         AND r.rstdt  <= :datee || '235959'" + vbCrLf
                If rsQryGbn <> "" Then
                    If rbSpcYnn Then
                        If rsRstGbn = "O" Then
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd || r.spccd").Replace("#ORGRST", "r.orgrst") + ")" + vbCrLf
                        Else
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd || r.spccd").Replace("#ORGRST", "r.viewrst") + ")" + vbCrLf
                        End If
                    Else
                        If rsRstGbn = "O" Then
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd").Replace("#ORGRST", "r.orgrst") + ")" + vbCrLf
                        Else
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd").Replace("#ORGRST", "r.viewrst") + ")" + vbCrLf
                        End If
                    End If
                End If
                sSql += "         AND r.rstflg = '3'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE.Replace("-", "")))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE.Replace("-", "")))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 특정결과 값조회
        Public Shared Function fnGet_Search_Rstval(ByVal rsRstDtS As String, ByVal rsRstDtE As String, ByVal rbFn As Boolean, _
                                                   ByVal rsQryGbn As String, ByVal rsRstGbn As String, _
                                                   ByVal rsOpt As String, ByVal rsRefL As String, ByVal rsRefH As String, _
                                                   ByVal rsPanic As String, ByVal rsDelta As String, _
                                                   ByVal rsCritical As String, ByVal rsAlert As String, ByVal rbMicroBio As Boolean, _
                                                   ByVal rbSpcYnn As Boolean) As DataTable
            Dim sFn As String = "fnGet_Search_Rstval"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"
                If rbMicroBio Then sTableNm = "lm010m"

                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                '<20130704 정선영 수정
                'sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || '/' || FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE j.deptcd || '/' END deptinfo,"
                sSql += "        FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno)  || '/' || j.roomno ELSE '' END deptinfo,"
                '>
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
                sSql += "       END alerttxt,"
                sSql += "       fn_ack_get_dr_name(o.chadr) gendr"
                sSql += "  FROM lf030m f3, lf060m f6, lj010m j, lj011m j1, vw_ack_ocs_ord_info o,"
                sSql += "       " + sTableNm + " r,"
                sSql += "       (SELECT DISTINCT"
                sSql += "               r.bcno, f61.*"
                sSql += "          FROM lj010m j, " + sTableNm + " r, lf060m f6, lf061m f61"
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
                sSql += " WHERE j.bcno    = j1.bcno"
                sSql += "   AND j1.regno  = o.patno"
                sSql += "   AND SUBSTR(j1.orgorddt, 1, 8) = o.orddate"
                sSql += "   AND j1.ocs_key = o.ordseqno"
                sSql += "   AND o.prcpclscd  = 'B2'"
                sSql += "   AND o.prcphistcd = 'O'"
                sSql += "   AND j1.bcno      = r.bcno"
                sSql += "   AND j1.tclscd    = r.tclscd"
                sSql += "   AND r.testcd     = f6.testcd"
                sSql += "   AND r.spccd      = f6.spccd"
                sSql += "   AND r.tkdt      >= f6.usdt"
                sSql += "   AND r.tkdt      <  f6.uedt"
                sSql += "   AND r.spccd      = f3.spccd"
                sSql += "   AND r.tkdt      >= f3.usdt"
                sSql += "   AND r.tkdt      <  f3.uedt"
                sSql += "   AND j.spcflg     = '4'"
                sSql += "   AND NVL(r.rstflg, ' ') <> ' '"
                sSql += "   AND NVL(r.orgrst, ' ') <> ' '"
                sSql += "   AND r.bcno       = re.bcno (+)"
                sSql += "   AND r.testcd     = re.testcd (+)"
                sSql += "   AND r.spccd = re.spccd(+)"

                Dim sTmp As String = ""

                If rsOpt.ToLower = "or" And rsPanic.Length + rsDelta.Length + rsCritical.Length + rsAlert.Length > 1 Then
                    sTmp = ""
                    If rsRefL <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.hlmark = 'L'"
                    If rsRefH <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.hlmark = 'H'"
                    If rsPanic <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.panicmark = 'P'"
                    If rsDelta <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.deltamark = 'D'"
                    If rsCritical <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.criticalmark = 'C'"
                    If rsAlert <> "" Then sTmp += IIf(sTmp = "", "", " OR ").ToString + "r.alertmark IN ('E', 'A')"

                    sSql += "   AND (" + sTmp + ")"
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
                    If rbSpcYnn Then
                        If rsRstGbn = "O" Then
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd || r.spccd").Replace("#ORGRST", "r.orgrst") + ")"
                        Else
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd || r.spccd").Replace("#ORGRST", "r.viewrst") + ")"
                        End If
                    Else
                        If rsRstGbn = "O" Then
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd").Replace("#ORGRST", "r.orgrst") + ")"
                        Else
                            sSql += "   AND (" + rsQryGbn.Replace("#TEST", "r.testcd").Replace("#ORGRST", "r.viewrst") + ")"
                        End If
                    End If
                End If

                sSql += "   AND r.rstdt >= :dates"
                sSql += "   AND r.rstdt <= :datee || '235959'"
                sSql += " ORDER BY bcno, testcd"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

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
            Dim sFn As String = "Function fnGet_Abnormal_ActionInfo(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(r5.regdt, 'yyyy-mm-dd hh24:mi') regdt,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       r4.RECVRNM ,fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptward,"
                sSql += "       f3.spcnmd, fn_ack_get_usr_name(r5.regid) regnm, r5.regid,"
                sSql += "       r5.cmtcont || CASE WHEN NVL(r5.cfmcont, ' ') = ' ' THEN '' ELSE CHR(13) || CHR(10) || '[조치내용]' || CHR(13) || CHR(10) || r5.cfmcont END cmtcont,"
                sSql += "       fn_ack_get_dr_name(r5.cfmid) cfmnm, fn_ack_date_str(r5.cfmdt, 'yyyy-mm-dd hh24:mi') cfmdt , fn_ack_get_dr_name(j.doctorcd) doctornm "
                sSql += "  FROM lr050m r5, lj010m j, lf030m f3 , lr054m r4"
                '  sSql += "        ,lr050m rc "
                sSql += " WHERE r5.regdt  >= :dates"
                sSql += "   AND r5.regdt  <= :datee || '235959'"
                sSql += "   AND r5.bcno    = j.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"
                sSql += "   AND r5.bcno = r4.bcno (+)"
                sSql += "   AND r5.lisseq = r4.lisseq (+)"



                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then  '<<<20170124 부서별 조회 조건 추가 
                    If rsPartSlip.Length = 1 Then
                        sSql += "   AND r5.partcd = :partcd"
                        alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                    Else
                        sSql += "   AND r5.partcd = :partcd"
                        sSql += "   AND r5.slipcd = :partcd"
                        alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                        alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If

                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsRegNo <> "" Then
                    sSql += "   AND j.patnm LIKE :partnm || '%'"
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
                sSql += "  FROM lr053m r5, lj010m j, lf030m f3"
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
                    alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDeptCd))
                End If

                If rsWardno <> "" Then
                    sSql += "   AND j.wardno = :wardno"
                    alParm.Add(New OracleParameter("wardno", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWardno))
                End If

                If rsCmtCont <> "" Then
                    sSql += "   AND r5.cmtcont LIKE '%' || :cmtcont || '%'"
                    alParm.Add(New OracleParameter("cmtcont", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmtCont))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 결과대장(작업그룹)
        Public Shared Function fnGet_RstList_WGrp(ByVal rsWkYmd As String, ByVal rsWkGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, _
                                                  ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, ByVal rbMiroBioYn As Boolean) As DataTable
            Dim sFn As String = "fnGet_RstList_WGrp"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"

                If rbMiroBioYn Then sTableNm = "lm010m"

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
                'sSql += "       fn_ack_get_slip_comment(j.bcno, f6.partcd, f6.slipcd, r.tkdt) slipcmt,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(a, ',' || a.cmt)).extract('//text()'), 2)"
                sSql += "          FROM lr040m a"
                sSql += "         WHERE bcno = j.bcno"
                sSql += "           AND partcd = f6.partcd"
                sSql += "           AND slipcd = f6.slipcd"
                sSql += "       ) slipcmt,"
                sSql += "       r.viewrst, r.rstcmt, hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.spccd) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt" '2018-11-30 처방일자(orddt) 추가

                '20210201 jhs 보고자 보고일시 수정 추가 
                 sSql += "      , fn_ack_get_usr_name(r.mwid) mwid "
                sSql += "      , fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi') mwdt"
                sSql += "      , fn_ack_get_usr_name(r.fnid) fnid "
                sSql += "      , fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt"
                '--------------------------------------------------------------------
                sSql += "  FROM lj010m j, " + sTableNm + " r, lf060m f6, lf030m f3"
                sSql += " WHERE r.wkymd   = :wkymd"
                sSql += "   AND r.wkgrpcd = :wgrpcd"
                sSql += "   AND r.wkno   >= :wknos"
                sSql += "   AND r.wkno   <= :wknoe"
                sSql += "   AND j.bcno    = r.BCNO"
                sSql += "   AND r.spccd   = f3.spccd"
                sSql += "   AND r.tkdt   >= f3.usdt"
                sSql += "   AND r.tkdt   <  f3.uedt"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.spccd   = f6.spccd"
                sSql += "   AND r.tkdt   >= f6.usdt"
                sSql += "   AND r.tkdt   <  f6.uedt"
                sSql += "   AND ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))"
                If rsRstFlg.Equals("MF") Then
                    sSql += "AND r.MWDT < r.FNDT "
                    sSql += "AND r.rstflg = '3' "
                Else
                    sSql += "   AND r.rstflg IN ('" + rsRstFlg.Replace(",", "','").ToString + "')"
                End If


                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                If rsTestCds <> "" Then sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd"

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
        Public Shared Function fnGet_RstList_TGrp(ByVal rsPartSlip As String, ByVal rsTGrpCd As String, ByVal rsRstDtS As String, ByVal rsRstDtE As String, _
                                                  ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, ByVal rbMicroBioYn As Boolean) As DataTable
            Dim sFn As String = "fnGet_RstList_TGrp"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010m"
                If rbMicroBioYn Then sTableNm = "lm010m"

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
                sSql += "          FROM lr040m a"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.partcd = f6.partcd"
                sSql += "           AND a.slipcd = f6.slipcd"
                sSql += "       ) slipcmt,"
                sSql += "       r.viewrst, r.rstcmt, hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                'sSql += "       fn_ack_get_slip_dispseq(f6.partcd, f6.spccd, r.tkdt) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f6.partcd AND slipcd = f6.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                sSql += "       f6.dispseql sort2, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt" '2018-11-30 처방일자(orddt) 추가
                '20210201 jhs 보고자 보고일시 수정 추가 
                sSql += "      , fn_ack_get_usr_name(r.mwid) mwid "
                sSql += "      , fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi') mwdt"
                sSql += "      , fn_ack_get_usr_name(r.fnid) fnid "
                sSql += "      , fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt"
                '--------------------------------------------------
                sSql += "  FROM lj010m j, " + sTableNm + " r, lf060m f6, lf030m f3"
                sSql += " WHERE r.tkdt >= :dates"             ' 2018-11-07 rstdt -> tkdt
                sSql += "   AND r.tkdt <= :datee || '235959'" ' 2018-11-07 rstdt -> tkdt
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND r.testcd = f6.testcd"
                sSql += "   AND r.spccd  = f6.spccd"
                sSql += "   AND r.tkdt  >= f6.usdt"
                sSql += "   AND r.tkdt  <  f6.uedt"
                sSql += "   AND r.spccd  = f3.spccd"
                sSql += "   AND r.tkdt  >= f3.usdt"
                sSql += "   AND r.tkdt  <  f3.uedt"
                sSql += "   AND ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))"

                If rsRstFlg.Equals("MF") Then
                    sSql += "AND r.MWDT < r.FNDT "
                    sSql += "AND r.rstflg = '3' "
                Else
                    sSql += "   AND r.rstflg IN ('" + rsRstFlg.Replace(",", "','") + "')"
                End If

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsRstDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsRstDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRstDtE))

                If rsSpcCd <> "" Then
                    sSql += "   AND j.spccd = :spccd"
                    alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsSpcCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpcCd))
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"

                ElseIf rsTGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(r.testcd, 1, 5), r.spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
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
        '<<<병원체코드 콤보 가져오는 함수
        Public Shared Function fnRefList(ByVal rsGrp As String, Optional ByVal rsRefcd As String = "") As DataTable
            Try

                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "select refcd, refnm "
                sSql += " from lf510m "
                sSql += "where groupcd = :grpcd "
                al.Add(New OracleParameter("grpcd", OracleDbType.Varchar2, rsGrp.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsGrp))

                If rsRefcd <> "" Then

                    sSql += "and refcd = :refcd "
                    al.Add(New OracleParameter("refcd", OracleDbType.Varchar2, rsRefcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRefcd))

                End If

                '20200225 jhs 병원체 군에서 급으로 변경으로 인한 조건절 추가
                sSql += "and useyn = 'Y' "
                '-----------------------------------

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End Function

        '<<<병원체 URL 정보 인서트 하기위해서 조회 
        Public Shared Function fnGetURLseqinfo(ByVal rsbcno As String) As DataTable
            Try

                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "select seq "
                sSql += " from lr080m "
                sSql += "where bcno = :bcno "
                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsbcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsbcno))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try


        End Function


        Public Shared Function fnIns_LR080M(ByVal rsBcno As String, ByVal rsSeq As String, ByVal rsSuccessYn As String, ByVal rsSendmsg As String, ByVal rsReturnmsg As String) As Boolean
            Dim sFn As String = "Private Function fnIns_LR080M(ByVal rsBcno As String, ByVal rsSeq As String, ByVal rsSuccessYn As String, ByVal rsSendmsg As String, ByVal rsReturnmsg As String) As Boolean"

            Try
                Dim dbCmd As New OracleCommand
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim intRanking As Integer = 0

                Dim dbCn As OracleConnection
                Dim dbTran As OracleTransaction


                dbCn = GetDbConnection()
                dbTran = dbCn.BeginTransaction()

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTran
                dbCmd.CommandType = CommandType.Text


                sSql = ""
                sSql += "INSERT INTO lr080m( bcno, seq, successyn, sendmsg, returnmsg , regdt ) "
                sSql += "            Values(:bcno,:seq,:successyn,:sendmsg,:returnmsg , sysdate ) "

                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsBcno
                    .Parameters.Add("seq", OracleDbType.Varchar2).Value = rsSeq
                    .Parameters.Add("successyn", OracleDbType.Varchar2).Value = rsSuccessYn
                    .Parameters.Add("sendmsg", OracleDbType.Varchar2).Value = rsSendmsg
                    .Parameters.Add("returnmsg", OracleDbType.Varchar2).Value = rsReturnmsg

                    .ExecuteNonQuery()
                End With

                dbTran.Commit()

                Return True

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function
        '-- 검체맵핑
        Public Shared Function fnGetRefSpccd() As DataTable

            Dim sFn As String = "fnGet_ReTest_List"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT refcd , spccd "
                sSql += "  FROM lf510m "
                sSql += "        WHERE GBN = 'S' "
                
                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fn_get_refcd_for_bcno(ByVal rsBcno As String) As DataTable
            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT distinct refcd ,groupcd " + vbCrLf
                sSql += "  FROM lf510m f , lr010m r " + vbCrLf
                sSql += " WHERE r.bcno = '" + rsBcno + "'" + vbCrLf
                sSql += "   AND r.TESTCD = f.testcd" + vbCrLf
                sSql += " union  " + vbCrLf
                sSql += "SELECT distinct refcd ,groupcd " + vbCrLf
                sSql += "  FROM lf510m f , lm010m r " + vbCrLf
                sSql += " WHERE r.bcno = '" + rsBcno + "'" + vbCrLf
                sSql += "   AND r.TESTCD = f.testcd" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception

            End Try
        End Function

        Public Shared Function fn_get_HosRst2(ByVal rsDateS As String, ByVal rsTestcds As String, ByVal rsPartSlip As String, ByVal rsBcno As String) As DataTable
            Dim sFn As String = "fn_get_HosRst"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "" + vbCrLf
                sSql += " Select distinct '11101318' as hospinm  " + vbCrLf
                sSql += "        , '국립중앙의료원'  as hospital " + vbCrLf
                sSql += "        , fn_ack_get_ocs_mediid_bcno(j10.bcno) as usrnm " + vbCrLf
                sSql += "        , J10.patnm AS patnm" + vbCrLf
                sSql += "        , case when J10.sex = 'M' THEN '남'" + vbCrLf
                sSql += "               when j10.sex = 'F' THEN '여' end as sex " + vbCrLf
                sSql += "        , PAT.birtdate as birth" + vbCrLf
                sSql += "        , J10.regno  " + vbCrLf
                sSql += "        , fn_ack_get_dept_code(J10.iogbn, J10.deptcd)  || '/' || fn_ack_get_ward_abbr(J10.wardno) deptcd " + vbCrLf
                sSql += "        , '' as spcetc " + vbCrLf
                sSql += "        , F30.SPCNMD " + vbCrLf
                sSql += "        , '' as etc  " + vbCrLf
                sSql += "        , '' as etc2 " + vbCrLf

                If rsBcno <> "" Then
                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) = True Then
                        sSql += "    ,(select substr(xmlagg(xmlelement(a,',' ||m12.baccd) order by m12.bacseq).extract('//text()'),2)  from lm012m m12 where m12.bcno = r10.bcno ) as hospicd" + vbCrLf
                    Else
                        sSql += "    , r10.viewrst as hospicd  " + vbCrLf
                    End If
                Else
                    If rsPartSlip.Substring(0, 1) = "M" Then
                        sSql += "    ,(select substr(xmlagg(xmlelement(a,',' ||m12.baccd) order by m12.bacseq).extract('//text()'),2)  from lm012m m12 where m12.bcno = r10.bcno ) as hospicd" + vbCrLf
                    Else
                        sSql += "    , r10.viewrst as hospicd  " + vbCrLf
                    End If
                End If

                sSql += "        , substr(r10.tkdt , 0,8) as tkdt " + vbCrLf  'REPLACE(to_char(sysdate , 'YYYY-MM-DD'),'-','') as sysdt ," + vbCrLf
                sSql += "        , substr(r10.fndt , 0,8) as fndt" + vbCrLf
                sSql += "        , substr(r10.fndt , 0,8) as sysdt " + vbCrLf
                sSql += "        , '11101318' as hospinm2 " + vbCrLf
                sSql += "        , FN_ACK_GET_USR_NAME('" + USER_INFO.USRID + "') as usrnm2, r10.bcno" + vbCrLf

                sSql += " FROM ( select r.bcno  ,r.testcd , r.viewrst,  min(r.tkdt) tkdt  ,max( r.fndt)fndt  " + vbCrLf


                If rsBcno <> "" Then
                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) = True Then
                        sSql += "  from lm010m r   " + vbCrLf
                    Else
                        sSql += "  from lr010m r   " + vbCrLf
                    End If
                Else
                    If rsPartSlip.Substring(0, 1) = "M" Then
                        sSql += "  from lm010m r   " + vbCrLf
                    Else
                        sSql += "  from lr010m r   " + vbCrLf
                    End If


                End If

                If rsBcno <> "" Then
                    sSql += " where r.bcno = :bcno " + vbCrLf
                    al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                Else
                    sSql += "  where r.fndt >= '" + rsDateS + "' || '000000'" + vbCrLf
                    sSql += "    and r.fndt <= '" + rsDateS + "'|| '235959'" + vbCrLf
                End If

                If rsPartSlip <> "" And rsBcno = "" Then
                    sSql += "   AND r.partcd = :partcd"
                    sSql += "   AND r.slipcd = :slipcd"

                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsTestcds <> "" And rsBcno = "" Then
                    sSql += " and r.testcd in(" + rsTestcds + ")" + vbCrLf
                End If

                sSql += "  and r.fndt is not null  " + vbCrLf
                sSql += "  group by r.bcno , r.testcd , r.viewrst ) r10 " + vbCrLf
                sSql += "       INNER JOIN LJ010M J10 ON R10.BCNO = J10.BCNO AND J10.RSTFLG >= '1'" + vbCrLf
                sSql += "       INNER JOIN VW_ACK_OCS_PAT_INFO PAT ON PAT.INSTCD = '031' AND J10.REGNO  =  PAT.PATNO " + vbCrLf
                sSql += "       INNER JOIN lf030m f30 ON j10.spccd = f30.spccd AND r10.tkdt >= f30.usdt AND r10.tkdt <= f30.uedt " + vbCrLf
                sSql += " order by fndt , regno" + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        Public Shared Function fn_get_Groupcd(ByVal rsRefcd As String) As String
            Dim sFn As String = "fn_get_HosRst"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList
                Dim sRtn As String = ""

                sSql += "SELECT groupcd" + vbCrLf
                sSql += "  FROM lf510m" + vbCrLf
                sSql += " WHERE refcd = '" + rsRefcd + "'" + vbCrLf

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    sRtn = dt.Rows(0).Item("groupcd").ToString
                End If

                Return sRtn

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '<<<병원체 등록된 내용 조회 
        Public Shared Function fn_get_HosRst_Reginfo(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "fn_get_HosRst_Reginfo"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList
                '<<<주의사항 신고로 조회 되나 데이터가 없는것은 직접신고가 아닌검체일수 있음(동일환자 동일 검사의 과거 신고검체는 신고로 표시하기 떄문)

                sSql += " SELECT rptusr " + vbCrLf
                sSql += "        , returnmsg , sendmsg " + vbCrLf
                sSql += " FROM LR080m " + vbCrLf
                sSql += " WHERE bcno = '" + rsBcno + "'" + vbCrLf
                sSql += "   AND successyn = 'Y' " + vbCrLf

                'al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()

                Dim dt As DataTable = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '<<<병원체검사 조회 
        Public Shared Function fn_get_HosRst(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsTestcds As String, ByVal rsPartSlip As String, ByVal rsBcno As String) As DataTable
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

                '<<<20170613 병원체코드 받아오는 함수 추가 
                If rsBcno <> "" Then
                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) = True Then
                        sSql += "    ,(SELECT substr(xmlagg(xmlelement(a,',' ||fn_ack_get_refcd(m12.baccd)) order by m12.bacseq).extract('//text()'),2)  from lm012m m12 where m12.bcno = r10.bcno ) as refcd" + vbCrLf
                    Else
                        sSql += "    , '' as refcd  " + vbCrLf
                    End If
                Else
                    If rsPartSlip.Substring(0, 1) = "M" Then
                        sSql += "    ,(SELECT substr(xmlagg(xmlelement(a,',' ||fn_ack_get_refcd(m12.baccd)) order by m12.bacseq).extract('//text()'),2)  from lm012m m12 where m12.bcno = r10.bcno ) as refcd" + vbCrLf
                    Else
                        sSql += "    , '' as refcd  " + vbCrLf
                    End If
                End If

                sSql += "        , substr(r10.tkdt , 0,8) as tkdt " + vbCrLf  'REPLACE(to_char(sysdate , 'YYYY-MM-DD'),'-','') as sysdt ," + vbCrLf
                sSql += "        , SUBSTR(r10.fndt , 0,8) as fndt " + vbCrLf
                sSql += "        , FN_ACK_GET_USR_NAME(r10.fnid) as fnnm " + vbCrLf
                sSql += "        , '11101318' as hospinm2 " + vbCrLf
                sSql += "        , FN_ACK_GET_USR_NAME('" + USER_INFO.USRID + "') as rptnm " + vbCrLf
                sSql += "        , FN_ACK_GET_BCNO_RST(r10.bcno) as orgrsts " + vbCrLf
                sSql += "        , r10.bcno , FN_ACK_GET_KCDC_STATE(r10.bcno) as state " + vbCrLf
                sSql += "        , fn_ack_get_kcdc_regdt_state(r10.bcno) as decla " + vbCrLf
                sSql += "        , fn_ack_get_bcno_bac_rst (r10.bcno) as bacrst, fn_ack_get_bcno_Anti_rst(r10.bcno) as antirst  "
                sSql += "        , FN_ACK_GET_KCDC_ERRMSG(r10.bcno) as errmsg " + vbCrLf
                '20210903 jhs 병원체 신고한 내역 있는지 확인하는 것 추가
                sSql += "        , (selecT distinct bcno from lr080m where bcno = r10.bcno)  as r80bcno " + vbCrLf
                '---------------------------------------------------------------
                sSql += " FROM ( SELECT  r.bcno  ,  min(r.tkdt) tkdt  , max(r.fnid) fnid , max(r.fndt) fndt " + vbCrLf

                If rsBcno <> "" Then
                    If PRG_CONST.BCCLS_MicorBio.Contains(rsBcno.Substring(8, 2)) = True Then
                        sSql += "  FROM lm010m r   " + vbCrLf
                    Else
                        sSql += "  FROM lr010m r   " + vbCrLf
                    End If
                Else
                    If rsPartSlip.Substring(0, 1) = "M" Then
                        sSql += "  FROM lm010m r   " + vbCrLf
                    Else
                        sSql += "  FROM lr010m r   " + vbCrLf
                    End If


                End If

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
                sSql += "       INNER JOIN LJ010M J10 ON R10.BCNO = J10.BCNO AND J10.RSTFLG >= '1'" + vbCrLf
                sSql += "       INNER JOIN VW_ACK_OCS_PAT_INFO PAT ON PAT.INSTCD = '031' AND J10.REGNO  =  PAT.PATNO " + vbCrLf
                sSql += "       INNER JOIN lf030m f30 ON j10.spccd = f30.spccd AND r10.tkdt >= f30.usdt AND r10.tkdt <= f30.uedt " + vbCrLf
                sSql += " order by fndt , regno" + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, al)

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
                sSql += "       fn_ack_get_bcno_full(a.bcno) vbcno,"
                'sSql += "       fn_ack_get_slip_dispseq(a.partcd, a.slipcd, fn_ack_sysdate) sort1,"
                sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = a.partcd AND slipcd = a.slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate) sort1,"
                sSql += "       a.dispseql sort2"
                sSql += "       , fn_ack_get_bfrst(a.bcno, a.testcd) bfviewrst" 'JJH 이전결과 추가
                sSql += "  FROM (SELECT r.*, f6.tcdgbn, f6.tnmd tnms,"
                sSql += "               fn_ack_date_str(j1.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "               j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "               FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd, FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno wardroom, fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "               CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo, "
                sSql += "               fn_ack_get_bcno_prt(r.bcno) prtbcno, f6.dispseql"
                sSql += "          FROM (SELECT bcno, regno, orgrst orgrst, viewrst,"
                sSql += "                       testcd, spccd, tclscd, fn_ack_date_str(tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "                       fn_ack_get_usr_name(tkid) tkid, fn_ack_date_str(rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "                       fn_ack_get_usr_name(CASE WHEN rstflg = '3' THEN fnid WHEN rstflg = '2' THEN mwid ELSE regid END) regid, partcd, slipcd"
                sSql += "                  FROM lr010m"
                sSql += "                 WHERE NVL(rerunflg, '0') > '0'"
                sSql += "                   AND rstdt >= :dates"
                sSql += "                   AND rstdt <= :datee || '235959'"
                sSql += "              ) r,"
                sSql += "              lf060m f6, lj011m j1, lj010m j"
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
                sSql += "          AND f6.usdt <= REPLACE(REPLACE(REPLACE(r.tkdt, '-', ''), ':', ''), ' ', '') "
                sSql += "          AND f6.uedt >  REPLACE(REPLACE(REPLACE(r.tkdt, '-', ''), ':', ''), ' ', '')"
                sSql += "       ) a LEFT OUTER JOIN"
                sSql += "       (SELECT bcno, testcd, spccd, viewrst"
                sSql += " , RANK() OVER (PARTITION BY bcno, testcd ORDER BY sysdt) rstno FROM lr011m"
                sSql += "       ) b ON (a.bcno = b.bcno AND a.testcd = b.testcd)"
                sSql += " WHERE (a.tcdgbn IN ('B', 'S', 'P') OR (a.tcdgbn = 'C' AND NVL(a.viewrst, ' ') <> ' '))"
                sSql += " ORDER BY a.rstdt, a.orddt, a.bcno, sort1, sort2"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 재검 통계
        Public Shared Function fnGet_ReTest_Statistics(ByVal ra_sDMY As String(), ByVal rsDateS As String, ByVal rsDateE As String, _
                                                       Optional ByVal rsPartSlip As String = "") As DataTable

            Dim sFn As String = "fnGet_ReTestList(String(),String,String,[String])"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT re.testcd, tnmd, re.rstdt , COUNT (re.testcd) recnt ,r.cnt totcnt "
                sSql += "  FROM lj010m j,"
                sSql += "       (SELECT bcno, testcd, spccd, fn_ack_date_str(rstdt, 'yyyy-mm-dd') rstdt, partcd, slipcd"
                sSql += "         FROM lr010m"
                sSql += "        WHERE NVL(rerunflg, ' ') <> ' '"
                sSql += "          AND rstdt >= :dates"
                sSql += "          AND rstdt <= :datee || '235959'"
                sSql += "       ) re,"
                sSql += "       (SELECT fn_ack_date_str(rstdt, 'yyyy-mm-dd') rstdt, testcd, count(*) cnt"
                sSql += "          FROM lr010m"
                sSql += "         WHERE rstdt >= :dates"
                sSql += "           AND rstdt <= :datee || '235959'"
                sSql += "         GROUP BY fn_ack_date_str(rstdt, 'yyyy-mm-dd'), testcd"
                sSql += "       ) r,"
                sSql += "       lf060m f"
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

        '-- 최종보고 수정 리스트
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
                sSql += "       r.viewrst, fn_ack_get_usr_name(r.fnid) fnid, fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, r52.cmtcont"
                sSql += "  FROM lj010m j, lr010m r, lf060m F6, lr011m r1, lr052m r52"
                sSql += " WHERE r.rstdt  >= :dates"
                sSql += "   AND r.rstdt  <= :datee || '235959'"
                sSql += "   AND r1.rstflg = '3'"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.bcno    = r1.bcno"
                sSql += "   AND r.testcd  = r1.testcd"
                sSql += "   AND r.spccd   = r1.spccd"
                sSql += "   AND r1.bcno   = r52.bcno"
                'sSql += "   AND r1.sysdt  = r.fndt"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.spccd   = f6.spccd"
                sSql += "   AND (f6.usdt <= r.tkdt AND f6.uedt > r.tkdt)"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then
                    sSql += "   AND r.partcd = :partcd"
                    sSql += "   AND r.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))

                End If

                sSql += " UNION "
                sSql += "SELECT r.regno, j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN '입원' ELSE '외래' END iogbn,FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd,"
                sSql += "       r.testcd, f6.tnmd, r.spccd, fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       r1.viewrst prerst, fn_ack_get_usr_name(r1.fnid) prefnid, fn_ack_date_str(r1.fndt,'yyyy-mm-dd hh24:mi') prefndt,"
                sSql += "       r.viewrst, fn_ack_get_usr_name(r.fndt) fnid, fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, r52.cmtcont"
                sSql += "  FROM lj010m j  , lm010m r, lf060m F6, lm011m r1, lr052m r52"
                sSql += " WHERE r.rstdt   >= :dates"
                sSql += "   AND r.rstdt   <= :datee || '235959'"
                sSql += "   AND r1.rstflg  = '3'"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.bcno     = r1.bcno"
                sSql += "   AND r.testcd   = r1.testcd"
                sSql += "   AND r.spccd    = r1.spccd"
                sSql += "   AND r1.bcno    = r52.bcno"
                'sSql += "   AND r1.sysdt  = r.fndt"
                sSql += "   AND r.testcd   = f6.testcd"
                sSql += "   AND r.spccd    = f6.spccd"
                sSql += "   AND (f6.usdt  <= r.tkdt AND f6.uedt > r.tkdt)"

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
        '--중간보고 조회 리스트(2018.10.08 KMJ)
        Public Shared Function fnGet_MWDATA_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsPartSlip As String) As DataTable
            Dim sFn As String = "fnGet_MWDATA_List" '-- 함수명/오류위치를 알려주는 param

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT r.regno, j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN '입원' ELSE '외래' END iogbn, FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd,"
                sSql += "       r.testcd, f6.tnmd, r.spccd, fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       r1.viewrst prerst, fn_ack_get_usr_name(r1.fnid) prefnid, fn_ack_date_str(r1.fndt,'yyyy-mm-dd hh24:mi') prefndt,"
                sSql += "       r.viewrst, fn_ack_get_usr_name(r.fnid) fnid, fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, r52.cmtcont"
                sSql += "  FROM lj010m j, lr010m r, lf060m F6, lr011m r1, lr052m r52"
                sSql += " WHERE r.rstdt  >= :dates"
                sSql += "   AND r.rstdt  <= :datee || '235959'"
                sSql += "   AND r1.rstflg = '3'"
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.bcno    = r1.bcno"
                sSql += "   AND r.testcd  = r1.testcd"
                sSql += "   AND r.spccd   = r1.spccd"
                sSql += "   AND r1.bcno   = r52.bcno"
                sSql += "   AND r.testcd  = f6.testcd"
                sSql += "   AND r.spccd   = f6.spccd"
                sSql += "   AND (f6.usdt <= r.tkdt AND f6.uedt > r.tkdt)"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then
                    sSql += "   AND r.partcd = :partcd"
                    sSql += "   AND r.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))

                End If

                sSql += " UNION "
                sSql += "SELECT r.regno, j.patnm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN '입원' ELSE '외래' END iogbn,FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd,"
                sSql += "       r.testcd, f6.tnmd, r.spccd, fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno,"
                sSql += "       r1.viewrst prerst, fn_ack_get_usr_name(r1.fnid) prefnid, fn_ack_date_str(r1.fndt,'yyyy-mm-dd hh24:mi') prefndt,"
                sSql += "       r.viewrst, fn_ack_get_usr_name(r.fndt) fnid, fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, r52.cmtcont"
                sSql += "  FROM lj010m j  , lm010m r, lf060m F6, lm011m r1, lr052m r52"
                sSql += " WHERE r.rstdt   >= :dates"
                sSql += "   AND r.rstdt   <= :datee || '235959'"
                sSql += "   AND r1.rstflg  = '3'"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.bcno     = r1.bcno"
                sSql += "   AND r.testcd   = r1.testcd"
                sSql += "   AND r.spccd    = r1.spccd"
                sSql += "   AND r1.bcno    = r52.bcno"
                sSql += "   AND r.testcd   = f6.testcd"
                sSql += "   AND r.spccd    = f6.spccd"
                sSql += "   AND (f6.usdt  <= r.tkdt AND f6.uedt > r.tkdt)"

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
        Private Const msFile As String = "File : CGLISAPP_S.vb, Class : LISAPP.APP_S.TatFn" + vbTab


        Public Shared Function fnGet_Tat_List(ByVal rsTestcd As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsQryGbn As String, _
                                              ByVal rbOverTime As Boolean, Optional ByVal rsSlipCd As String = "", _
                                              Optional ByVal rsEmerYN As String = "", Optional ByVal rsRegNo As String = "", Optional ByVal rschkTATCont As Boolean = False, _
                                              Optional ByVal rsIncludeChild As Boolean = False) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_TatList"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                If rsQryGbn = "" Then
                    '결과단위 TAT

                    sSql += "SELECT f6.partcd, r.bcno," + vbCrLf
                    sSql += "       f6.testcd, j.regno, j.statgbn, j.iogbn, FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd, j.wardno," + vbCrLf
                    sSql += "       j.patnm, j.sex || '/' || j.age sa," + vbCrLf
                    sSql += "       fn_ack_get_dept_name(j.iogbn, j.deptcd) deptnm," + vbCrLf
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ws," + vbCrLf
                    sSql += "       f6.tnmd, f6.spccd, f3.spcnmd," + vbCrLf
                    sSql += "       fn_ack_date_str(j1.orgorddt, 'yyyy-mm-dd hh24:mi') orddt," + vbCrLf
                    sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi') colldt," + vbCrLf
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt," + vbCrLf
                    sSql += "       fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi') mwdt," + vbCrLf
                    sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt," + vbCrLf
                    sSql += "       fn_ack_date_diff(j1.orgorddt, j1.colldt, '1') t1," + vbCrLf
                    sSql += "       fn_ack_date_diff(j1.colldt, r.tkdt, '1') t2," + vbCrLf
                    sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.mwdt, '1') tat1," + vbCrLf
                    sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.fndt, '1') tat3," + vbCrLf
                    sSql += "       fn_ack_date_diff(r.mwdt , r.fndt , '1') tat2," + vbCrLf
                    sSql += "       fn_ack_date_diff(j1.orgorddt , r.fndt , '1') tot," + vbCrLf
                    '<<< 20170511 TAT에서 소수점이 계산된 TAT는 오버타임으로 계산되서 소수점 버림 
                    sSql += "       trunc(fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.mwdt, '3')) tat1_mi," + vbCrLf
                    sSql += "       trunc(fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.fndt, '3')) tat2_mi," + vbCrLf
                    '>>> 20170511 TAT에서 소수점이 계산된 TAT는 오버타임으로 계산되서 소수점 버림 
                    '20210202 JHS 휴일 tat 계산
                    sSql += "       fn_ack_date_diff_excep_holi(NVL(r.wkdt, r.tkdt), r.fndt, r.testcd, r.spccd, '1') tat1_mi_exp_holi," + vbCrLf
                    '----------------------------------------------------
                    sSql += "       CASE WHEN j.statgbn IN ('Y',  'E') THEN NVL(f6.perrptmi, f6.prptmi) ELSE f6.prptmi END prptmi," + vbCrLf
                    sSql += "       CASE WHEN j.statgbn IN ('Y',  'E') THEN NVL(f6.ferrptmi, f6.frptmi) ELSE f6.frptmi END frptmi," + vbCrLf
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyymmdd') tkdt_m, r.workno, f6.partcd || f6.slipcd slipcd, f2.dispseq sort_slip," + vbCrLf
                    sSql += "       '[' || NVL(r51.cmtcd, '') || '] ' || r51.cmtcont cmtcont, f2.dispseq sort_slip, f6.dispseql sort_test," + vbCrLf
                    sSql += "       fn_ack_get_usr_name(r.rstid) rstnm" + vbCrLf
                    sSql += "  FROM lf060m f6," + vbCrLf
                    sSql += "       lj010m j, lj011m j1," + vbCrLf
                    sSql += "       lf030m f3, lf021m f2," + vbCrLf
                    sSql += "       (" + vbCrLf
                    sSql += "        SELECT bcno, tclscd, testcd, spccd, tkdt, wkdt, mwdt, fndt, NVL(fnid, mwid) rstid, wkymd || NVL(wkgrpcd, '') || NVL(wkno, '') workno" + vbCrLf
                    sSql += "          FROM lr010m" + vbCrLf
                    sSql += "         WHERE tkdt >= :dates" + vbCrLf
                    sSql += "           AND tkdt <= :datee || '235959'" + vbCrLf
                    sSql += "           AND (NVL(mwdt, ' ') <> ' ' OR NVL(fndt, ' ') <> ' ')" + vbCrLf
                    sSql += "         UNION ALL" + vbCrLf
                    sSql += "        SELECT bcno, tclscd, testcd, spccd, tkdt, wkdt, mwdt, fndt, NVL(fnid, mwid) rstid, wkymd || NVL(wkgrpcd, '') || NVL(wkno, '') workno" + vbCrLf
                    sSql += "          FROM lm010m" + vbCrLf
                    sSql += "         WHERE tkdt >= :dates" + vbCrLf
                    sSql += "           AND tkdt <= :datee || '235959'" + vbCrLf
                    sSql += "           AND (NVL(mwdt, ' ') <> ' ' OR NVL(fndt, ' ') <> ' ')" + vbCrLf
                    sSql += "       ) r," + vbCrLf
                    sSql += "       lr051m r51" + vbCrLf
                    sSql += " WHERE f6.testcd  = r.testcd" + vbCrLf
                    sSql += "   AND f6.spccd   = r.spccd" + vbCrLf
                    sSql += "   AND f6.usdt   <= r.tkdt" + vbCrLf
                    sSql += "   AND f6.uedt   >  r.tkdt" + vbCrLf
                    sSql += "   AND f6.spccd   = f3.spccd" + vbCrLf
                    sSql += "   AND f3.usdt   <= r.tkdt" + vbCrLf
                    sSql += "   AND f3.uedt   >  r.tkdt" + vbCrLf
                    sSql += "   AND f6.partcd = f2.partcd" + vbCrLf
                    sSql += "   AND f6.slipcd = f2.slipcd" + vbCrLf
                    sSql += "   AND f2.usdt  <= r.tkdt" + vbCrLf
                    sSql += "   AND f2.uedt  >  r.tkdt" + vbCrLf
                    '20210616 jhs 차일드 코드 포함 test 
                    If rsIncludeChild Then
                        sSql += "   AND ((f6.tcdgbn = 'B' AND NVL(f6.titleyn, '0') = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))" + vbCrLf
                    Else
                        sSql += "   AND ((f6.tcdgbn = 'B' AND NVL(f6.titleyn, '0') = '0') OR f6.tcdgbn IN ('S', 'P'))" + vbCrLf
                    End If
                    '------------------------------------------
                    sSql += "   AND NVL(f6.tatyn, '0') = '1'" + vbCrLf
                    If rsTestcd <> "" Then
                        sSql += " AND f6.testcd||f6.spccd in (" + rsTestcd.Replace(" ", "") + ") " + vbCrLf
                    End If
                    sSql += "   AND j.bcno    = j1.bcno" + vbCrLf
                    sSql += "   AND j1.bcno   = r.bcno" + vbCrLf
                    sSql += "   AND j1.tclscd = r.tclscd" + vbCrLf
                    sSql += "   AND r.bcno    = r51.bcno (+)" + vbCrLf
                    sSql += "   AND r.testcd  = r51.testcd (+)" + vbCrLf
                Else
                    '처방단위 TAT
                    sSql = ""
                    sSql += "SELECT f6.partcd, r.bcno," + vbCrLf
                    sSql += "       f6.testcd, j.regno, j.statgbn, j.iogbn, j.deptcd, j.wardno," + vbCrLf
                    sSql += "       j.patnm, j.sex || '/' || j.age sa," + vbCrLf
                    sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm," + vbCrLf
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.wardno || '/' || j.roomno ws," + vbCrLf
                    sSql += "       f6.tnmd, f6.spccd, f3.spcnmd," + vbCrLf
                    sSql += "       fn_ack_date_str(r.orgorddt, 'yyyy-mm-dd hh24:mi') orddt," + vbCrLf
                    sSql += "       fn_ack_date_str(r.colldt, 'yyyy-mm-dd hh24:mi') colldt," + vbCrLf
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt," + vbCrLf
                    sSql += "       fn_ack_date_str(r.mwdt, 'yyyy-mm-dd hh24:mi') mwdt," + vbCrLf
                    sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt," + vbCrLf
                    sSql += "       fn_ack_date_diff(r.orgorddt, r.colldt, '1') t1," + vbCrLf
                    sSql += "       fn_ack_date_diff(r.colldt, r.tkdt, '1') t2," + vbCrLf
                    sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.mwdt, '1') tat1," + vbCrLf
                    sSql += "       fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.fndt, '1') tat3," + vbCrLf
                    sSql += "       fn_ack_date_diff(r.mwdt , r.fndt , '1') tat2," + vbCrLf
                    sSql += "       fn_ack_date_diff(r.orgorddt , r.fndt , '1') tot," + vbCrLf
                    '<<< 20170511 TAT에서 소수점이 계산된 TAT는 오버타임으로 계산되서 소수점 버림 
                    sSql += "       trunc(fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.mwdt, '3')) tat1_mi," + vbCrLf
                    sSql += "       trunc(fn_ack_date_diff(NVL(r.wkdt, r.tkdt), r.fndt, '3')) tat2_mi," + vbCrLf
                    '>>> 20170511
                    '20210202 JHS 휴일 tat 계산
                    sSql += "       fn_ack_date_diff_excep_holi(NVL(r.wkdt, r.tkdt), r.fndt, '1') tat1_mi_exp_holi," + vbCrLf
                    '----------------------------------------------------
                    sSql += "       CASE WHEN j.statgbn IN ('Y',  'E') THEN NVL(f6.perrptmi, f6.prptmi) ELSE f6.prptmi END prptmi," + vbCrLf
                    sSql += "       CASE WHEN j.statgbn IN ('Y',  'E') THEN NVL(f6.ferrptmi, f6.frptmi) ELSE f6.frptmi END frptmi," + vbCrLf
                    sSql += "       fn_ack_date_str(r.tkdt, 'yyyymmdd') tkdt_m, r.workno, f6.partcd || f6.slipcd slipcd," + vbCrLf
                    sSql += "       '[' || r51.cmtcd || '] ' || r51.cmtcont cmtcont, f2.dispseq sort_slip, f6.dispseql sort_test," + vbCrLf
                    sSql += "       fn_ack_get_usr_name(r.rstid) rstnm" + vbCrLf
                    sSql += "  FROM lf060m f6," + vbCrLf
                    sSql += "       lj010m j," + vbCrLf
                    sSql += "       lf030m f3, lf021m f2," + vbCrLf
                    sSql += "       (" + vbCrLf
                    sSql += "        SELECT j0.bcno, j1.tclscd, j1.spccd, MIN(j1.orgorddt) orgorddt, MAX(j1.colldt) colldt, MIN(r.tkdt) tkdt, MIN(wkdt) wkdt, MAX(r.mwdt) mwdt, MAX(r.fndt) fndt, MAX(NVL(r.fnid, r.mwid)) rstid, '' workno" + vbCrLf
                    sSql += "          FROM lj010m j0, lj011m j1, lr010m r" + vbCrLf
                    sSql += "         WHERE r.tkdt   >= :dates" + vbCrLf
                    sSql += "           AND r.tkdt   <= :datee || '235959'" + vbCrLf
                    sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')" + vbCrLf
                    sSql += "           AND j1.bcno   = r.bcno" + vbCrLf
                    sSql += "           AND j1.tclscd = r.tclscd" + vbCrLf
                    sSql += "           AND j0.bcno   = j1.bcno" + vbCrLf
                    sSql += "         GROUP BY j0.bcno, j1.tclscd, j1.spccd" + vbCrLf
                    sSql += "         UNION ALL" + vbCrLf
                    sSql += "        SELECT j0.bcno, j1.tclscd, j1.spccd, MIN(j1.orgorddt) orgorddt, MAX(j1.colldt) colldt, MIN(r.tkdt) tkdt, MIN(wkdt) wkdt, MAX(r.mwdt) mwdt, MAX(r.fndt) fndt, MAX(NVL(r.fnid, r.mwid)) rstid, '' workno" + vbCrLf
                    sSql += "          FROM lj010m j0, lj011m j1, lM010m r" + vbCrLf
                    sSql += "         WHERE r.tkdt   >= :dates" + vbCrLf
                    sSql += "           AND r.tkdt   <= :datee || '235959'" + vbCrLf
                    sSql += "           AND (NVL(r.mwdt, ' ') <> ' ' OR NVL(r.fndt, ' ') <> ' ')" + vbCrLf
                    sSql += "           AND j1.bcno   = r.bcno" + vbCrLf
                    sSql += "           AND j1.tclscd = r.tclscd" + vbCrLf
                    sSql += "           AND j0.bcno   = j1.bcno" + vbCrLf
                    sSql += "         GROUP BY j0.bcno, j1.tclscd, j1.spccd" + vbCrLf
                    sSql += "       ) r," + vbCrLf
                    sSql += "       lr051m r51" + vbCrLf
                    sSql += " WHERE f6.testcd = r.tclscd" + vbCrLf
                    sSql += "   AND f6.spccd  = r.spccd" + vbCrLf
                    sSql += "   AND f6.usdt  <= r.tkdt" + vbCrLf
                    sSql += "   AND f6.uedt  >  r.tkdt" + vbCrLf
                    sSql += "   AND f6.spccd  = f3.spccd" + vbCrLf
                    sSql += "   AND f3.usdt  <= r.tkdt " + vbCrLf
                    sSql += "   AND f3.uedt  >  r.tkdt" + vbCrLf
                    sSql += "   AND f6.partcd = f2.partcd" + vbCrLf
                    sSql += "   AND f6.slipcd = f2.slipcd" + vbCrLf
                    sSql += "   AND f2.usdt  <= r.tkdt" + vbCrLf
                    sSql += "   AND f2.uedt  >  r.tkdt" + vbCrLf
                    If rsIncludeChild Then
                        sSql += "   AND ((f6.tcdgbn = 'B' AND NVL(f6.titleyn, '0') = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))" + vbCrLf
                    Else
                        sSql += "   AND ((f6.tcdgbn = 'B' AND NVL(f6.titleyn, '0') = '0') OR f6.tcdgbn IN ('S', 'P'))" + vbCrLf
                    End If
                    '------------------------------------------
                    sSql += "   AND NVL(f6.tatyn, '0') = '1'" + vbCrLf
                    If rsTestcd <> "" Then
                        sSql += " AND f6.testcd||f6.spccd in (" + rsTestcd.Replace(" ", "") + ") " + vbCrLf
                    End If
                    sSql += "   AND j.bcno      = r.bcno" + vbCrLf
                    sSql += "   AND r.bcno      = r51.bcno (+) " + vbCrLf
                    sSql += "   AND r.tclscd    = r51.testcd (+)" + vbCrLf
                End If

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegNo <> "" Then
                    sSql += "   AND j.regno = :regno" + vbCrLf
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsEmerYN = "Y" Then
                    sSql += "   AND NVL(j.statgbn, ' ') <> ' '" + vbCrLf
                ElseIf rsEmerYN = "N" Then
                    sSql += "   AND NVL(j.statgbn, ' ') = ' '" + vbCrLf
                End If
                '<<< 20170511 TAT에서 소수점이 계산된 TAT는 오버타임으로 계산되서 소수점 버림 
                If rbOverTime Then
                    sSql += "   AND (trunc(fn_ack_date_diff(j1.tkdt, r.mwdt, '3')) > CASE WHEN j.statgbn IN ('Y',  'E') THEN NVL(f6.perrptmi, f6.prptmi) ELSE f6.prptmi END OR" + vbCrLf
                    sSql += "        trunc(fn_ack_date_diff(j1.tkdt, r.fndt, '3')) > CASE WHEN j.statgbn IN ('Y',  'E') THEN NVL(f6.ferrptmi, f6.frptmi) ELSE f6.frptmi END" + vbCrLf
                    sSql += "       )"
                End If

                If rsSlipCd <> "" Then
                    sSql += "   AND f6.partcd = :partcd" + vbCrLf
                    sSql += "   AND f6.slipcd = :slipcd" + vbCrLf
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If

                If rschkTATCont Then
                    sSql += "   AND r51.cmtcont is not null" + vbCrLf
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        '-- 사유별 통계
        Public Shared Function fnGet_TatCont_St(ByVal rsDateS As String, ByVal rsDateE As String, _
                                                Optional ByVal rsPartSlip As String = "") As DataTable

            Dim sFn As String = "fnGet_TatCont_St(String(),String,String,[String])"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT nvl(r.cmtcd, '기타소견') as cmtcd, fn_ack_date_str(j.tkdt, 'yyyy-mm-dd') regdt, count(*) cnt, MAX(r.cmtcont) cmtcont"
                sSql += "  FROM lr010m j, lr051m r,"        '미생물은 빼고
                sSql += "       (SELECT testcd FROM lf060m"
                sSql += "         WHERE NVL(tatyn, '0') = '1'"
                sSql += "           AND ((tcdgbn = 'B' AND NVL(titleyn, '0') = '0') OR tcdgbn IN ('S', 'P'))"

                If rsPartSlip <> "" Then

                    sSql += "           AND partcd = :partcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                    If rsPartSlip.Length = 2 Then
                        sSql += "           AND slipcd = :slipcd"
                        alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If

                    sSql += "             AND usdt  <= fn_ack_sysdate"
                    sSql += "             AND uedt  >  fn_ack_sysdate"
                End If
                sSql += "         GROUP BY testcd"
                sSql += "       ) f"
                sSql += " WHERE j.tkdt  >= :dates || '000000'"
                sSql += "   AND j.tkdt  <= :datee || '235959'"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.testcd = r.testcd"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                sSql += "   AND r.testcd = f.testcd"
                sSql += " GROUP BY nvl(r.cmtcd, '기타소견'), fn_ack_date_str (j.tkdt, 'yyyy-mm-dd')"
                sSql += " ORDER BY nvl(r.cmtcd, '기타소견')"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 사유별 통계 add 20130212 ymg
        '<20130718 정선영 수정, 일반, 응급 tat 구분해서 적용
        Public Shared Function fnGet_TatCont_St2(ByVal rsDateS As String, ByVal rsDateE As String, _
                                                ByVal rsEmerYN As String, ByVal rbOverTime As Boolean, Optional ByVal rsPartSlip As String = "") As DataTable

            Dim sFn As String = "fnGet_TatCont_St2(String(),String,String,[String])"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT nvl(r.cmtcd, '기타소견') as cmtcd, fn_ack_date_str(j.tkdt, 'yyyy-mm-dd') regdt, count(*) cnt, MAX(r.cmtcont) cmtcont"
                sSql += "  FROM lr010m j, lr051m r, lj010m j1,"        '미생물은 빼고
                sSql += "       (SELECT testcd ,spccd, prptmi, frptmi, perrptmi, ferrptmi FROM lf060m"
                sSql += "         WHERE NVL(tatyn, '0') = '1'"
                sSql += "           AND ((tcdgbn = 'B' AND NVL(titleyn, '0') = '0') OR tcdgbn IN ('S', 'P'))"

                If rsPartSlip <> "" Then

                    sSql += "           AND partcd = :partcd"
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                    If rsPartSlip.Length = 2 Then
                        sSql += "           AND slipcd = :slipcd"
                        alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If

                    sSql += "             AND usdt  <= fn_ack_sysdate"
                    sSql += "             AND uedt  >  fn_ack_sysdate"
                End If
                sSql += "         GROUP BY testcd, spccd, prptmi, frptmi, perrptmi, ferrptmi "
                sSql += "       ) f"
                sSql += " WHERE j.tkdt  >= :dates || '000000'"
                sSql += "   AND j.tkdt  <= :datee || '235959'"
                sSql += "   AND j.bcno   = r.bcno"
                sSql += "   AND j.bcno   = j1.bcno"
                sSql += "   AND j.spccd  = f.spccd"
                sSql += "   AND j.spccd  = j1.spccd"
                sSql += "   AND j.testcd = r.testcd"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                sSql += "   AND r.testcd = f.testcd"

                If rsEmerYN = "Y" Then
                    sSql += "   AND NVL(j1.statgbn, ' ') <> ' '"
                ElseIf rsEmerYN = "N" Then
                    sSql += "   AND NVL(j1.statgbn, ' ') = ' '"
                End If

                If rbOverTime Then
                    'sSql += "   AND (fn_ack_date_diff(j.tkdt, j.mwdt, '3') > f.prptmi OR fn_ack_date_diff(j.tkdt, j.fndt, '3') > f.frptmi)"
                    '<20130718 정선영 수정, 일반/응급 tat 구분 
                    sSql += "   AND (TRUNC(fn_ack_date_diff(j.tkdt, j.mwdt, '3')) > CASE WHEN j1.statgbn IN ('Y',  'E') THEN NVL(f.perrptmi, f.prptmi) ELSE f.prptmi END OR"
                    sSql += "        TRUNC(fn_ack_date_diff(j.tkdt, j.fndt, '3')) > CASE WHEN j1.statgbn IN ('Y',  'E') THEN NVL(f.ferrptmi, f.frptmi) ELSE f.frptmi END"
                    sSql += "       )"
                End If

                sSql += " GROUP BY nvl(r.cmtcd, '기타소견'), fn_ack_date_str (j.tkdt, 'yyyy-mm-dd')"
                sSql += " ORDER BY nvl(r.cmtcd, '기타소견')"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Sub New()

        End Sub
    End Class
#End Region

#Region "채혈/접수 관련"
    Public Class CollTkFn
        Private Const msFile As String = "File : CGRISAPP_S, Class : RISAPP.APP_S.CollTkFn" + vbTab

        '-- 채혈/졉수 대장
        Public Shared Function fnGet_CollTk_List(ByVal rsQryGbn As String, _
                                                 ByVal rsDateS As String, ByVal rsDateE As String, _
                                                 ByVal rsSlipCd As String, ByVal rsWGrpCd As String, ByVal rsTGrpCd As String, _
                                                 ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, _
                                                 ByVal rsRegNo As String, ByVal rsPatNm As String, _
                                                 ByVal rsIOGbn As String, ByVal rsWard As String, ByVal rsDeptCd As String, _
                                                 ByVal rbNoTk2 As Boolean) As DataTable
            Dim sFn As String = "fnGet_CollTk_List"

            Try

                Dim al As New ArrayList
                Dim sSql As String = ""

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi:ss') orddt, j.iogbn,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END dept,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM lj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       f3.spcnmd,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "          FROM lj011m a, lf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                '20200401 jhs 정렬 맞추기 위해 tkdt 의 초 추가
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi:ss') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(j1.passdt, 'yyyy-mm-dd hh24:mi:ss') passdt, j1.passid passnm,"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then
                    sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, fn_ack_get_usr_name(NVL(rr.tkid, rm.tkid)) tknm, NULL workno"
                Else
                    sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,  NULL tknm, NULL workno"
                End If
                '-----------------------------------------------------
                sSql += "  FROM lj011m j1, lf030m f3, lj010m j"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then sSql += ", lr010m rr, lm010m rm"

                sSql += " WHERE j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"
                sSql += "   AND NVL(j1.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "')"

                If rsIOGbn <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"

                    al.Add(New OracleParameter("iogbn", rsIOGbn))
                End If

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
                ElseIf rsQryGbn = "1" Then   '20161020 허용석 채혈, 검체전달(접수 이전 상태인 것만) 조회 기능 추가
                    sSql += "   AND j1.colldt >= :dates"
                    sSql += "   AND j1.colldt <= :datee || '235959'"
                    sSql += "   AND j1.spcflg IN ('2', '3')"
                    sSql += "   AND j.spcflg  IN ('2', '3')"

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
                    sSql += "   AND j1.bcno   = rr.bcno (+)"
                    sSql += "   AND j1.tclscd = rr.tclscd (+)"
                    sSql += "   AND j1.bcno   = rm.bcno (+)"
                    sSql += "   AND j1.tclscd = rm.tclscd (+)"

                    If rsTestCds <> "" Then
                        sSql += "   AND (j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "        rr.testcd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "        rm.testcd IN ('" + rsTestCds.Replace(",", "','") + "') "
                        sSql += "       )"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) "
                        sSql += "       )"

                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))

                    ElseIf rsSlipCd <> "" Then

                        If rsSlipCd.Length = 1 Then
                            sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) "
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd))
                            al.Add(New OracleParameter("partcd", rsSlipCd))
                            al.Add(New OracleParameter("partcd", rsSlipCd))
                        Else
                            sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) "
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        End If
                End If


                    If rsWGrpCd <> "" Then
                        sSql += "           AND (rr.wkgrpcd = :wgrpcd OR rm.wkgrpcd = :wgrpcd)"

                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                    End If

                    If rsRstFlg <> "" Then sSql += "   AND (NVL(rr.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "') OR NVL(rm.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "'))"

                    If rsQryGbn = "5" Then
                        If rbNoTk2 Then sSql += "           AND (NVL(rr.wkymd, ' ') <> ' ' OR NVL(rm.wkymd, ' ') <> ' ')"
                    Else
                        If rbNoTk2 Then sSql += "           AND (NVL(rr.wkymd, ' ') = ' '  OR NVL(rm.wkymd, ' ') = ' ')"
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If

                Else
                    If rsTestCds <> "" Then
                        sSql += "   AND j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "')"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                    ElseIf rsSlipCd <> "" Then

                        If rsSlipCd.Length = 1 Then
                            sSql += "   AND (j1.tclscd, j1.spccd) IN "
                            sSql += "       (SELECT testcd, spccd"
                            sSql += "          FROM lf060m"
                            sSql += "         WHERE partcd  = :partcd"
                            sSql += "           AND usdt   <= j.bcprtdt"
                            sSql += "           AND uedt   >  j.bcprtdt"
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd))
                        Else
                            sSql += "   AND (j1.tclscd, j1.spccd) IN "
                            sSql += "       (SELECT testcd, spccd"
                            sSql += "          FROM lf060m"
                            sSql += "         WHERE partcd  = :partcd"
                            sSql += "           AND slipcd  = :slipcd"
                            sSql += "           AND usdt   <= j.bcprtdt"
                            sSql += "           AND uedt   >  j.bcprtdt"
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        End If
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If
                End If

                Select Case rsQryGbn
                    Case "1" : sSql += " ORDER BY colldt, bcno"
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

        '-- 채혈/졉수 대장 바코드 추가
        Public Shared Function fnGet_CollTk_List2(ByVal rsQryGbn As String, _
                                                 ByVal rsDateS As String, ByVal rsDateE As String, _
                                                 ByVal rsSlipCd As String, ByVal rsWGrpCd As String, ByVal rsTGrpCd As String, _
                                                 ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, _
                                                 ByVal rsRegNo As String, ByVal rsPatNm As String, _
                                                 ByVal rsIOGbn As String, ByVal rsWard As String, ByVal rsDeptCd As String, _
                                                 ByVal rbNoTk2 As Boolean) As DataTable
            Dim sFn As String = "fnGet_CollTk_List"

            Try

                Dim al As New ArrayList
                Dim sSql As String = ""

                sSql += "SELECT DISTINCT"
                sSql += "       '*'||fn_ack_get_bcno_prt(j.bcno)||'*' prtimg, "
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtno, "
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi:ss') orddt, j.iogbn,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END dept,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)"
                sSql += "          FROM lj011m ff"
                sSql += "         WHERE bcno    = j.bcno"
                sSql += "           AND spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '"
                sSql += "       ) doctorrmk,"
                sSql += "       f3.spcnmd,"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "          FROM lj011m a, lf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) testnms,"
                '20200401 jhs 정렬 맞추기 위해 tkdt 의 초 추가
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-mm-dd hh24:mi:ss') colldt, fn_ack_get_usr_name(j1.collid) collnm,"
                sSql += "       fn_ack_date_str(j1.passdt, 'yyyy-mm-dd hh24:mi:ss') passdt, j1.passid passnm,"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then
                    sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt, fn_ack_get_usr_name(NVL(rr.tkid, rm.tkid)) tknm, NULL workno"
                Else
                    sSql += "       fn_ack_date_str(j1.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,  NULL tknm, NULL workno"
                End If
                '-----------------------------------------------------
                sSql += "  FROM lj011m j1, lf030m f3, lj010m j"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then sSql += ", lr010m rr, lm010m rm"

                sSql += " WHERE j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"
                sSql += "   AND NVL(j1.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "')"

                If rsIOGbn <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"

                    al.Add(New OracleParameter("iogbn", rsIOGbn))
                End If

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
                ElseIf rsQryGbn = "1" Then   '20161020 허용석 채혈, 검체전달(접수 이전 상태인 것만) 조회 기능 추가
                    sSql += "   AND j1.colldt >= :dates"
                    sSql += "   AND j1.colldt <= :datee || '235959'"
                    sSql += "   AND j1.spcflg IN ('2', '3')"
                    sSql += "   AND j.spcflg  IN ('2', '3')"

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
                    sSql += "   AND j1.bcno   = rr.bcno (+)"
                    sSql += "   AND j1.tclscd = rr.tclscd (+)"
                    sSql += "   AND j1.bcno   = rm.bcno (+)"
                    sSql += "   AND j1.tclscd = rm.tclscd (+)"

                    If rsTestCds <> "" Then
                        sSql += "   AND (j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "        rr.testcd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "        rm.testcd IN ('" + rsTestCds.Replace(",", "','") + "') "
                        sSql += "       )"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) "
                        sSql += "       )"

                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))

                    ElseIf rsSlipCd <> "" Then

                        If rsSlipCd.Length = 1 Then
                            sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) "
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd))
                            al.Add(New OracleParameter("partcd", rsSlipCd))
                            al.Add(New OracleParameter("partcd", rsSlipCd))
                        Else
                            sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                            sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) "
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        End If
                    End If


                    If rsWGrpCd <> "" Then
                        sSql += "           AND (rr.wkgrpcd = :wgrpcd OR rm.wkgrpcd = :wgrpcd)"

                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                    End If

                    If rsRstFlg <> "" Then sSql += "   AND (NVL(rr.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "') OR NVL(rm.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "'))"

                    If rsQryGbn = "5" Then
                        If rbNoTk2 Then sSql += "           AND (NVL(rr.wkymd, ' ') <> ' ' OR NVL(rm.wkymd, ' ') <> ' ')"
                    Else
                        If rbNoTk2 Then sSql += "           AND (NVL(rr.wkymd, ' ') = ' '  OR NVL(rm.wkymd, ' ') = ' ')"
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If

                Else
                    If rsTestCds <> "" Then
                        sSql += "   AND j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "')"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                    ElseIf rsSlipCd <> "" Then

                        If rsSlipCd.Length = 1 Then
                            sSql += "   AND (j1.tclscd, j1.spccd) IN "
                            sSql += "       (SELECT testcd, spccd"
                            sSql += "          FROM lf060m"
                            sSql += "         WHERE partcd  = :partcd"
                            sSql += "           AND usdt   <= j.bcprtdt"
                            sSql += "           AND uedt   >  j.bcprtdt"
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd))
                        Else
                            sSql += "   AND (j1.tclscd, j1.spccd) IN "
                            sSql += "       (SELECT testcd, spccd"
                            sSql += "          FROM lf060m"
                            sSql += "         WHERE partcd  = :partcd"
                            sSql += "           AND slipcd  = :slipcd"
                            sSql += "           AND usdt   <= j.bcprtdt"
                            sSql += "           AND uedt   >  j.bcprtdt"
                            sSql += "       )"

                            al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                            al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        End If
                    End If

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If
                End If

                Select Case rsQryGbn
                    Case "1" : sSql += " ORDER BY colldt, bcno"
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

        '-- 채혈/졉수 건수
        Public Shared Function fnGet_CollTk_Statistics(ByVal rsQryGbn As String, _
                                                       ByVal rsDateS As String, ByVal rsDateE As String, _
                                                       ByVal rsSlipCd As String, ByVal rsWGrpCd As String, ByVal rsTGrpCd As String, _
                                                       ByVal rsSpcCd As String, ByVal rsTestCds As String, ByVal rsRstFlg As String, _
                                                       ByVal rsRegNo As String, ByVal rsPatNm As String, _
                                                       ByVal rsIOGbn As String, ByVal rsWard As String, ByVal rsDeptCd As String) As DataTable

            Dim sFn As String = "fnGet_CollTk_Statistics"

            Try

                Dim al As New ArrayList
                Dim sSql As String = ""

                sSql += "SELECT /*+ INDEX(J PK_LJ010M) */"
                sSql += "       j.bcno"
                sSql += "  FROM lj010m j, lj011m j1, lf030m f3"

                If rsQryGbn = "4" Or rsQryGbn = "5" Then sSql += ", lr010m rr, lm010m rm"

                sSql += " WHERE j.bcno     = j1.bcno"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND j.bcprtdt >= f3.usdt"
                sSql += "   AND j.bcprtdt <  f3.uedt"
                sSql += "   AND NVL(j1.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "')"

                If rsIOGbn <> "" Then
                    sSql += "   AND j.iogbn = :iogbn"

                    al.Add(New OracleParameter("iogbn", rsIOGbn))
                End If

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
                    sSql += "   AND j1.bcno   = rr.bcno (+)"
                    sSql += "   AND j1.tclscd = rr.tclscd (+)"
                    sSql += "   AND j1.bcno   = rm.bcno (+)"
                    sSql += "   AND j1.tclscd = rm.tclscd (+)"

                    If rsTestCds <> "" Then
                        sSql += "   AND (j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "        rr.testcd IN ('" + rsTestCds.Replace(",", "','") + "') OR "
                        sSql += "        rm.testcd IN ('" + rsTestCds.Replace(",", "','") + "') "
                        sSql += "       )"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) OR"
                        sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd) "
                        sSql += "       )"

                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))

                    ElseIf rsSlipCd <> "" Then
                        sSql += "   AND ((j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                        sSql += "        (rr.testcd, rr.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) OR"
                        sSql += "        (rm.testcd, rm.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) "
                        sSql += "       )"

                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                        al.Add(New OracleParameter("partcd", rsSlipCd.Substring(0, 1)))
                        al.Add(New OracleParameter("slipcd", rsSlipCd.Substring(1, 1)))
                    End If


                    If rsWGrpCd <> "" Then
                        sSql += "           AND (rr.wkgrpcd = :wgrpcd OR rm.wkgrpcd = :wgrpcd)"

                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                        al.Add(New OracleParameter("wgrpcd", rsWGrpCd))
                    End If

                    If rsRstFlg <> "" Then sSql += "   AND (NVL(rr.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "') OR NVL(rm.rstflg, '0') IN ('" + rsRstFlg.Replace(",", "','") + "'))"

                    If rsSpcCd <> "" Then
                        sSql += "    AND j.spccd = :spccd"
                        al.Add(New OracleParameter("spccd", rsSpcCd))
                    End If

                Else
                    If rsTestCds <> "" Then
                        sSql += "   AND j1.tclscd IN ('" + rsTestCds.Replace(",", "','") + "')"

                    ElseIf rsTGrpCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                        al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                    ElseIf rsSlipCd <> "" Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN "
                        sSql += "       (SELECT testcd, spccd"
                        sSql += "          FROM lf060m"
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
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 채혈리스트
        Public Shared Function fnGet_Collect_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIOGbn As String, _
                                                  ByVal rbDetailGbn As Boolean, ByVal rsDeptWards As String, ByVal rsCollIds As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Collect_List(String, String, String, Boolean, String, String) As DataTable"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                rsDateS = rsDateS.Replace("-", "")
                rsDateE = rsDateE.Replace("-", "")

                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       j1.colldt, j.regno, j.patnm, j.sex || '/' || j.age sexage," + vbCrLf
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) deptcd," + vbCrLf
                sSql += "       j.iogbn, CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo," + vbCrLf
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE '' END wardroom," + vbCrLf
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j.bcno," + vbCrLf
                sSql += "       j1.colldt_sort, j1.tubenmd, fn_ack_get_usr_name(j1.collid) collnm, j1.tubecd, j1.collid" + vbCrLf
                sSql += "  FROM lj010m j," + vbCrLf
                sSql += "       (" + vbCrLf
                sSql += "        SELECT fn_ack_date_str(j.colldt, 'yyyy-mm-dd hh24:mi') colldt, j.bcno, fn_ack_date_str(j.colldt, 'yyyy-mm-dd') colldt_sort," + vbCrLf
                sSql += "               f4.tubenmd, f4.tubecd, j.collid" + vbCrLf
                sSql += "          FROM lj011m j, lf060m f6, lf040m f4" + vbCrLf
                'sSql += "         WHERE j.colldt >= :dates"
                sSql += "         WHERE j.colldt >= '" + rsDateS + "' || '000000'" + vbCrLf '2019-11-04 JJH 날짜조건 수정 배포전
                sSql += "           AND j.colldt <= '" + rsDateE + "' || '235959'" + vbCrLf

                'alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                'alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                sSql += "           AND j.tclscd   = f6.testcd" + vbCrLf
                sSql += " 		    AND j.spccd    = f6.spccd" + vbCrLf
                sSql += " 		    AND f6.usdt   <= j.colldt" + vbCrLf
                sSql += " 		    AND f6.uedt   >  j.colldt" + vbCrLf
                sSql += " 		    AND f6.tubecd  = f4.tubecd" + vbCrLf
                sSql += " 		    AND f4.usdt   <= j.colldt" + vbCrLf
                sSql += " 		    AND f4.uedt   >  j.colldt" + vbCrLf
                sSql += "           AND f4.tubecd > '00'" + vbCrLf
                If rsCollIds <> "" Then
                    sSql += "           AND j.collid IN ('" + rsCollIds.Replace(",", "','") + "')" + vbCrLf
                End If
                sSql += "         GROUP BY j.colldt, j.bcno, f4.tubenmd, f4.tubecd, j.collid" + vbCrLf
                sSql += "       ) j1" + vbCrLf
                sSql += " WHERE j.bcno = j1.bcno" + vbCrLf

                '>
                If rsIOGbn = "O" Then
                    ' 외래
                    sSql += "   AND j.iogbn <> 'I'" + vbCrLf
                    'alParm.Add(New oracleParameter("iogbn", rsIOGbn))

                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            ' 제외
                            sSql += "   AND j.deptcd NOT IN ('" + rsDeptWards.Replace(",", "','") + "') " + vbCrLf
                        Else
                            ' 포함
                            sSql += "   AND j.deptcd IN ('" + rsDeptWards.Replace(",", "','") + "') " + vbCrLf
                        End If
                    End If

                ElseIf rsIOGbn = "I" Then
                    ' 입원
                    sSql += "   AND j.iogbn = :iogbn" + vbCrLf
                    alParm.Add(New oracleParameter("iogbn", rsIOGbn))

                    If rsDeptWards <> "" Then
                        If rbDetailGbn Then
                            ' 제외
                            sSql += "   AND j.wardno NOT IN ('" + rsDeptWards.Replace(",", "','") + "') " + vbCrLf
                        Else
                            ' 포함
                            sSql += "   AND j.wardno IN ('" + rsDeptWards.Replace(",", "','") + "') " + vbCrLf
                        End If
                    End If
                End If
                sSql += " ORDER BY colldt, regno, bcno" + vbCrLf

                DbCommand()
                'Return DbExecuteQuery(sSql, alParm)
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        '-- 채혈/접수 취소 리스트
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
                sSql += "       CASE WHEN SUBSTR(j.bcno, 9, 1) = 'M' THEN (SELECT fn_ack_date_str(MAX (tkdt), 'YYYY-MM-DD HH24:MI') || '^' || fn_ack_get_usr_name(MAX(tkid)) tkinfo FROM lm010h WHERE bcno = j.bcno)"
                sSql += "             ELSE (SELECT fn_ack_date_str(MAX (tkdt), 'YYYY-MM-DD HH24:MI') || '^' || fn_ack_get_usr_name(MAX(tkid)) tkinfo FROM lr010h WHERE bcno = j.bcno)"
                sSql += "       END tkinfo,"
                sSql += "       j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.deptcd,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END deptinfo,"
                sSql += "       j.wardno || '/' || j.roomno wardroom, j.iogbn,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       j3.cancelcmt, j3.cancelcd, j3.cancelgbn,"
                sSql += "       fn_ack_get_usr_name(j3.cancelid) cancelnm,"
                'sSql += "                 fn_ack_get_test_name_list(j.bcno)"
                sSql += "       (SELECT listagg(b.tnmd,',') within group (order by b.dispseql)"
                sSql += "          FROM lj011m a, lf060m b"
                sSql += "         WHERE a.bcno   = j.bcno"
                sSql += "           AND a.tclscd = b.testcd  AND a.spccd = b.spccd"
                sSql += "           AND b.usdt  <= j.bcprtdt AND b.uedt > j.bcprtdt"
                sSql += "       ) tnm"
                sSql += "  FROM lj030m j3, lj010m j, lj011h j1"
                sSql += " WHERE j3.canceldt  >= :dates"
                sSql += "   AND j3.canceldt  <= :datee || '235959'"
                sSql += "   AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"
                sSql += "   AND j3.bcno       = j.bcno"
                sSql += "   AND j.bcno        = j1.bcno"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsPartSlip <> "" Then

                    If rsPartSlip.Length = 1 Then
                        sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"

                        alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    Else
                        sSql += "   AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd AND usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate)"

                        alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                        alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If
                    
                End If

                If rsIoGbn <> "" Then
                    If rsIoGbn = "O" Then
                        sSql += "   AND j.iogbn <> 'I'"
                    Else
                        sSql += "   AND j.iogbn = 'I'"
                    End If

                    If rsDeptWards <> "" Then
                        If rsIoGbn = "I" Then
                            sSql += "   AND j.wardno " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        Else
                            sSql += "   AND j.deptcd " + IIf(rbDetailGbn, " NOT ", "").ToString + "IN ('" + rsDeptWards.Replace(",", "','") + "')"
                        End If
                    End If
                End If

                sSql += " ORDER BY deldt, orddt, regno, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 채혈/접수 취소 건수
        Public Shared Function fnGet_CollTk_Cancel_Statistics(ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, ByVal rsIOGBN As String, _
                                                              ByVal rsCancelGbn As String, ByVal rbDetailGbn As Boolean, ByVal rsDeptWards As String, _
                                                              Optional ByVal rsSlipCd As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_CollTk_Cancel_List(String, String, String, String, boolean, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                '<<<20170123 사유코드통계 사유내용을 마스터로 가져오게 수정함
                sSql += "SELECT canceldt, cancelcd, cancelcmt, COUNT(bcno) cnt"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT"
                sSql += "               MAX(f4.cmtcont) cancelcmt, j3.cancelcd, fn_ack_date_str(j3.canceldt, 'yyyy-mm') canceldt, j3.canceldt || j3.bcno bcno"
                sSql += "          FROM lj030m j3, lj010m j, lj011m j1 , lf410m f4 "
                sSql += "         WHERE j3.bcno  = j.bcno"
                sSql += "           AND j.bcno   = j1.bcno"
                sSql += "           AND j3.canceldt >= :dates"
                sSql += "           AND j3.canceldt <= :datee || '235959'"
                sSql += "           AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"
                sSql += "           AND j3.cancelcd = f4.cmtgbn||f4.CMTCD "
                '>>>

                'sSql += "SELECT canceldt, cancelcd, cancelcmt, COUNT(bcno) cnt"
                'sSql += "  FROM ("
                'sSql += "        SELECT DISTINCT"
                'sSql += "               MAX(j3.cancelcmt) cancelcmt, j3.cancelcd, fn_ack_date_str(j3.canceldt, 'yyyy-mm') canceldt, j3.canceldt || j3.bcno bcno"
                'sSql += "          FROM lj030m j3, lj010m j, lj011m j1"
                'sSql += "         WHERE j3.bcno  = j.bcno"
                'sSql += "           AND j.bcno   = j1.bcno"
                'sSql += "           AND j3.canceldt >= :dates"
                'sSql += "           AND j3.canceldt <= :datee || '235959'"
                'sSql += "           AND j3.cancelgbn IN ('" + rsCancelGbn.Replace(",", "','") + "')"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsOrdDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsOrdDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtE))

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
                    If rsSlipCd.Length = 1 Then
                        sSql += "           AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd = :slipcd)"
                        alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                    Else
                        sSql += "           AND (j1.tclscd, j1.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE usdt <= fn_ack_sysdate AND uedt > fn_ack_sysdate AND partcd || slipcd = :slipcd)"
                        alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                    End If
                    
                End If

                sSql += "         GROUP BY j3.cancelcd, fn_ack_date_str(j3.canceldt, 'yyyy-mm'), j3.canceldt || j3.bcno"
                sSql += "       ) a"

                sSql += " GROUP BY cancelcmt, cancelcd, canceldt"
                sSql += " ORDER by cancelcd "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- Reject 결과값 조회
        Public Shared Function fnGet_Reject_Rstval(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_Reject_Rstval(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then sTableNm = "lm010"

                If PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.tnmd, r.testcd, r.orgrst, r.viewrst, r.rstcmt, r.rstflg,"
                    sSql += "       r.hlmark, r.panicmark, r.deltamark, r.alertmark, r.criticalmark, f.tcdgbn, r.tclscd,"
                    sSql += "       fn_ack_get_test_reftxt(f.refgbn, j.sex, re.reflms, re.reflm, re.refhms, re.refhm, re.reflfs, re.reflf, re.refhfs, re.refhf, re.reflt) reftxt,"
                    sSql += "       fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(r.regid) regnm,"
                    sSql += "       fn_ack_date_str(r.mwdt,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(r.mwid)  mwnm,"
                    sSql += "       fn_ack_date_str(r.fndt,  'yyyy-mm-dd hh24:mi:ss') fndt,  fn_ack_get_usr_name(r.fnid)  fnnm,"
                    sSql += "       fn_ack_date_str(r.moddt, 'yyyy-mm-dd hh24:mi') canceldt, fn_ack_get_usr_name(r.modid) cancelnm,"
                    sSql += "       f.partcd, f.slipcd,"
                    'sSql += "       fn_ack_get_slip_dispseq(f.partcd, f.slipcd, r.tkdt) sort1,"
                    sSql += "       (SELECT dispseq FROM rf021m WHERE partcd = f.partcd AND slipcd = f.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                    'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                    sSql += "       (SELECT dispseql FROM rf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                    sSql += "       NVL(f.dispseql, 999) sort3,"
                    sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                    sSql += "       j.sex || '/' || j.age sexage,"
                    sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                    sSql += "       j.deptcd || CASE WHEN NVL(j.wardno, ' ') = ' ' THEN '' ELSE '/' || j.wardno END deptward,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) drnm"
                    sSql += "  FROM rj010m j, rf060m f, rr010h r,"
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
                    sSql += " ORDER BY canceldt, sort1, f.partcd, f.slipcd, sort2, r.tclscd, sort3, testcd"
                Else
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.tnmd, r.testcd, r.orgrst, r.viewrst, r.rstcmt, r.rstflg,"
                    sSql += "       r.hlmark, r.panicmark, r.deltamark, r.alertmark, r.criticalmark, f.tcdgbn, r.tclscd,"
                    sSql += "       fn_ack_get_test_reftxt(f.refgbn, j.sex, re.reflms, re.reflm, re.refhms, re.refhm, re.reflfs, re.reflf, re.refhfs, re.refhf, re.reflt) reftxt,"
                    sSql += "       fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(r.regid) regnm,"
                    sSql += "       fn_ack_date_str(r.mwdt,  'yyyy-mm-dd hh24:mi:ss') mwdt,  fn_ack_get_usr_name(r.mwid)  mwnm,"
                    sSql += "       fn_ack_date_str(r.fndt,  'yyyy-mm-dd hh24:mi:ss') fndt,  fn_ack_get_usr_name(r.fnid)  fnnm,"
                    sSql += "       fn_ack_date_str(r.moddt, 'yyyy-mm-dd hh24:mi') canceldt, fn_ack_get_usr_name(r.modid) cancelnm,"
                    sSql += "       f.partcd, f.slipcd,"
                    'sSql += "       fn_ack_get_slip_dispseq(f.partcd, f.slipcd, r.tkdt) sort1,"
                    sSql += "       (SELECT dispseq FROM lf021m WHERE partcd = f.partcd AND slipcd = f.slipcd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort1,"
                    'sSql += "       fn_ack_get_test_dispseql(r.tclscd, r.spccd, r.tkdt) sort2,"
                    sSql += "       (SELECT dispseql FROM lf060m WHERE testcd = r.tclscd AND spccd = r.spccd AND usdt <= j.bcprtdt AND uedt > j.bcprtdt) sort2,"
                    sSql += "       NVL(f.dispseql, 999) sort3,"
                    sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                    sSql += "       j.sex ||'/'||j.age sexage,"
                    sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                    sSql += "       j.deptcd || CASE WHEN NVL(j.wardno, ' ') = ' ' THEN '' ELSE '/' || j.wardno END deptward,"
                    sSql += "       fn_ack_get_dr_name(j.doctorcd) drnm"
                    sSql += "  FROM lj010m j, lf060m f," + sTableNm + "h r,"
                    sSql += "       (SELECT DISTINCT f61.*"
                    sSql += "          FROM lj010m j, " + sTableNm + "m r,"
                    sSql += "               lf060m f6, lf061m f61"
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
                    sSql += " ORDER BY canceldt, sort1, f.partcd, f.slipcd, sort2, r.tclscd, sort3, testcd"
                End If

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

#Region " 환자/검체 현황 조회"
    Public Class PatHisFn
        Private Const msFile As String = "File : CGRISAPP_S, Class : RISAPP.APP_S.PatHisFn" + vbTab

        ' 미접수 조회
        Public Shared Function fnGet_NotTk_PatList(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIoGbn As String, _
                                                   Optional ByVal rsWard As String = "", _
                                                   Optional ByVal rsPartSlip As String = "", Optional ByVal rsTGrpCd As String = "", _
                                                   Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_NotTk_PatList(string, string, string, [string], [string], [string], [string]) As DataTable"

            Dim sSql As String = ""
            Dim al As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       j.iogbn, fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       j.deptcd, fn_ack_get_dr_name(j.doctorcd) doctornm, j.wardno || '/' || j.roomno wardroom, j.bedno, j.entdt, j.resdt,"
                sSql += "       fn_ack_date_str(NVL(j1.colldt, j.bcprtdt), 'yyyy-mm-dd') colldt,"
                sSql += "       NULL tkdt, j.spcflg, j.owngbn"
                sSql += "  FROM lj010m j, lj011m j1, lf060m f"
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
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
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
                    sSql += "   AND (f.testcd, f.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip.Length = 1 Then
                    sSql += "  AND f.partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, rsPartSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip))
                ElseIf rsPartSlip.Length = 2 Then
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
            Dim sFn As String = "Public Shared Function fnGet_NotRst_PatList(String, String, string, [string], [string], [string], [string]) As DataTable"

            Dim sSql As String = ""
            Dim al As New ArrayList

            Try

                sSql += "SELECT DISTINCT"
                sSql += "       j.iogbn, fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       j.deptcd, fn_ack_get_dr_name(j.doctorcd) doctornm, "
                sSql += "       j.wardno || '/' || j.roomno wardroom, j.bedno, j.entdt, j.resdt,"
                sSql += "       NULL colldt, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd') tkdt, j.spcflg, j.owngbn"
                sSql += "  FROM lj010m j,"
                sSql += "       (SELECT r.bcno, r.tkdt FROM lr010m r, lf060m f"
                sSql += "         WHERE r.tkdt   >= :dates"
                sSql += "           AND r.tkdt   <= :datee || '235959'"
                sSql += "           AND NVL(r.rstflg, '0') <= '2'"
                sSql += "           AND f.titleyn = '0'"
                sSql += "           AND r.testcd  = f.testcd"
                sSql += "           AND r.spccd   = f.spccd"
                sSql += "           AND r.tkdt   >= f.usdt"
                sSql += "           AND r.tkdt   <  f.uedt"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegNo <> "" Then
                    sSql += "   AND r.regno = :regno"
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "           AND (f.testcd, f.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "           AND f.partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                    If rsPartSlip.Length > 1 Then
                        sSql += "           AND f.slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If
                End If

                If rsWkGrpCd <> "" Then
                    sSql += "           AND r.wkgrpcd = :wgrpcd"
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                End If

                sSql += "         UNION "
                sSql += "        SELECT r.bcno, r.tkdt"
                sSql += "          FROM lm010m r, lf060m f"
                sSql += "         WHERE r.tkdt   >= :dates"
                sSql += "           AND r.tkdt   <= :datee || '235959'"
                sSql += "           AND NVL(r.rstflg, '0') <= '2'"
                sSql += "           AND f.titleyn = '0'"
                sSql += "           AND r.spccd   = f.spccd"
                sSql += "           AND r.tkdt   >= f.usdt"
                sSql += "           AND r.tkdt   <  f.uedt"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegNo <> "" Then
                    sSql += "   AND r.regno = :regno"
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "           AND (f.testcd, f.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "           AND f.partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                    If rsPartSlip.Length > 1 Then
                        sSql += "           AND f.slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If
                End If

                If rsWkGrpCd <> "" Then
                    sSql += "           AND r.wkgrpcd  = :wgrpcd"
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                End If

                sSql += "       ) r"
                sSql += " WHERE j.bcno = r.bcno"
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

        ' 접수이상
        Public Shared Function fnGet_Tk_PatList(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsIoGbn As String, _
                                            Optional ByVal rsWard As String = "", _
                                            Optional ByVal rsPartSlip As String = "", Optional ByVal rsWkGrpCd As String = "", Optional ByVal rsTGrpCd As String = "", _
                                            Optional ByVal rsRegNo As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Tk_PatList(String, String, string, [string], [string], [string], [string]) As DataTable"

            Dim sSql As String = ""
            Dim al As New ArrayList

            Try

                sSql += "SELECT DISTINCT"
                sSql += "       j.iogbn, fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt,"
                sSql += "       j.regno, fn_ack_get_pat_info(j.regno, '', '') patinfo,"
                sSql += "       j.deptcd, fn_ack_get_dr_name(j.doctorcd) doctornm, "
                sSql += "       j.wardno || '/' || j.roomno wardroom, j.bedno, j.entdt, j.resdt,"
                sSql += "       NULL colldt, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd') tkdt, j.spcflg, j.owngbn"
                sSql += "  FROM lj010m j,"
                sSql += "       (SELECT r.bcno, r.tkdt FROM lr010m r, lf060m f"
                sSql += "         WHERE r.tkdt >= :dates"
                sSql += "           AND r.tkdt <= :datee || '235959'"
                sSql += "           AND NVL(r.rstflg, '0') <= '3'"
                sSql += "           AND f.titleyn = '0'"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "           AND r.tkdt  >= f.usdt"
                sSql += "           AND r.tkdt  <  f.uedt"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegNo <> "" Then
                    sSql += "   AND r.regno = :regno"
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "           AND (f.testcd, f.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "           AND f.partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                    If rsPartSlip.Length > 1 Then
                        sSql += "           AND f.slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If
                End If

                If rsWkGrpCd <> "" Then
                    sSql += "           AND r.wkgrpcd = :wgrpcd"
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                End If

                sSql += "         UNION "
                sSql += "        SELECT r.bcno, r.tkdt"
                sSql += "          FROM lm010m r, lf060m f"
                sSql += "         WHERE r.tkdt   >= :dates"
                sSql += "           AND r.tkdt   <= :datee || '235959'"
                sSql += "           AND NVL(r.rstflg, '0') <= '3'"
                sSql += "           AND f.titleyn = '0'"
                sSql += "           AND r.spccd   = f.spccd"
                sSql += "           AND r.tkdt   >= f.usdt"
                sSql += "           AND r.tkdt   <  f.uedt"

                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                If rsRegNo <> "" Then
                    sSql += "   AND r.regno = :regno"
                    al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                End If

                If rsTGrpCd <> "" Then
                    sSql += "           AND (f.testcd, f.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "           AND f.partcd = :partcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))

                    If rsPartSlip.Length > 1 Then
                        sSql += "           AND f.slipcd = :slipcd"
                        al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                    End If
                End If

                If rsWkGrpCd <> "" Then
                    sSql += "           AND r.wkgrpcd  = :wgrpcd"
                    al.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                End If

                sSql += "       ) r"
                sSql += " WHERE j.bcno = r.bcno"
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

        '-- 환자상세(채혈)
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
                sSql += "       f6.partcd || f6.slipcd partslip, f6.bcclscd, j.statgbn, j.iogbn, j1.fkocs, f6.tubecd,"
                sSql += "       j.owngbn, f6.testcd, f6.tordcd,"
                sSql += "       '' append_yn, NVL(f6.exlabyn, '0') exlabyn,"
                sSql += "       NVL(f6.seqtyn, '0') seqtyn, f6.seqtmi,"
                sSql += "       CASE WHEN f6.dbltseq = '2' THEN '0' ELSE f6.dbltseq END dbltseq_sort,"
                sSql += "       RPAD(f6.testcd, 7, ' ') || f6.spccd testspc,"
                sSql += "       '' wkgrpcd"
                sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf030m f3"
                sSql += " WHERE j.regno   = :regno"
                sSql += "   AND j.iogbn   = :iogbn"
                sSql += "   AND j.owngbn  = :owngbn"
                sSql += "   AND j.bcno    = j1.bcno"
                sSql += "   AND (j.bcprtdt BETWEEN :colldt AND :colldt || '235959' OR j1.colldt BETWEEN :colldt AND :colldt || '235959')"
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
                al.Add(New OracleParameter("colldt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("colldt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("colldt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("colldt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

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

        '-- 환자상세(접수)
        Public Shared Function fnGet_Tk_TestList(ByVal rsDate As String, ByVal rsRegNo As String, _
                                                     ByVal rsIoGbn As String, ByVal rsOwnGbn As String, _
                                                     Optional ByVal rsBcNo As String = "") As DataTable
            Dim sFn As String = "Function FGS04_Get_TestList_tk"

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
                sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf030m f3,"
                sSql += "       ("
                sSql += "        SELECT bcno, tclscd, spccd, tkdt, wkymd, wkgrpcd, wkno, MIN(NVL(rstflg, '0')) minrstflg, MAX(NVL(rstflg, '0')) maxrstflg,"
                sSql += "               MAX(NVL(rstdt, '19000101')) rstdt"
                sSql += "          FROM lr010m "
                sSql += "         WHERE regno = :regno"
                sSql += "           AND tkdt >= :tkdt"
                sSql += "           AND tkdt <= :tkdt || '235959'"
                sSql += "         GROUP BY bcno, tclscd, spccd, tkdt, wkymd, wkgrpcd, wkno"
                sSql += "         UNION "
                sSql += "        SELECT bcno, tclscd, spccd, tkdt, wkymd, wkgrpcd, wkno, MIN(NVL(rstflg, '0')) minrstflg, MAX(NVL(rstflg, '0')) maxrstflg,"
                sSql += "               MAX(NVL(rstdt, '19000101')) rstdt"
                sSql += "          FROM lm010m "
                sSql += "         WHERE regno = :regno"
                sSql += "           AND tkdt >= :tkdt"
                sSql += "           AND tkdt <= :tkdt || '235959'"
                sSql += "         GROUP BY bcno, tclscd, spccd, tkdt, wkymd, wkgrpcd, wkno"
                sSql += "       ) r"
                sSql += " WHERE j.regno    = :regno"
                sSql += "   AND j.iogbn    = :iogbn"
                sSql += "   AND j.owngbn   = :owngbn"
                sSql += "   AND NVL(j.spcflg, '0') = '4'"
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
                al.Add(New OracleParameter("tkdt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("tkdt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("tkdt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                al.Add(New OracleParameter("tkdt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

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

        ' 해당검체 접수 유/무 조회
        Public Shared Function fnGet_BcNo_TkYn(ByVal rsBcno As String) As DataTable
            Dim sFn As String = "Public Function FGS04_BcNoSearch(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT fn_ack_get_bcno_full(bcno) bcno, spcflg"
                sSql += "  FROM lj010m"
                sSql += " WHERE bcno    = :bcno"
                sSql += "   AND NVL(spcflg, '0') = '0'"

                alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '처방 진검 비교 
        Public Shared Function fnGet_EmrvsLis_state(ByVal rsPid As String, ByVal rsOrddt As String, ByVal rsTordcd As String, ByVal rsDept As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_EmrvsLis_state(ByVal rsPid As String, ByVal rsOrddt As String, ByVal rsTordcd As String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += " select oprc.pid, oprc.prcpdd,oprc.prcphistcd,'O' iogbn " + vbCrLf
                sSql += "        ,(select cdnm  from com.zbcmcode a WHERE CDGRUPID = 'M0011' and cdid = oprc.PRCPSTATCD) PRCPSTATCD" + vbCrLf
                sSql += "        ,(select cdnm  from com.zbcmcode a WHERE CDGRUPID = 'M0011' and cdid = exop.EXECRCPTSTATCD) EXECRCPTSTATCD" + vbCrLf
                sSql += "        ,exop.execprcpuniqno " + vbCrLf
                sSql += "        , case j11.spcflg when '1' then '바코드출력' " + vbCrLf
                sSql += "                          when '2' then '채혈'" + vbCrLf
                sSql += "                          when '3' then '전달'" + vbCrLf
                sSql += "                          when '4' then '접수'" + vbCrLf
                sSql += "                          else 'X' end spcflg " + vbCrLf
                sSql += "        , case j11.rstflg when '0' then '미결과'" + vbCrLf
                sSql += "                          when '1' then '검사중'" + vbCrLf
                sSql += "                          when '2' then '중간보고'" + vbCrLf
                sSql += "                          when '3' then '최종보고'" + vbCrLf
                sSql += "                          else 'X' end rstflg  " + vbCrLf
                sSql += "  from emr.mmohoprc oprc ,emr.mmodexop exop , lj011m j11" + vbCrLf
                sSql += " where oprc.instcd = '031'" + vbCrLf
                sSql += "   and oprc.prcpdd = :orddt" + vbCrLf
                sSql += "   and oprc.pid = :regno " + vbCrLf
                sSql += "   and oprc.prcpcd = :tordcd " + vbCrLf
                sSql += "   and oprc.prcphistcd = 'O'" + vbCrLf
                sSql += "   and oprc.ORDDEPTCD = FN_ACK_GET_DEPT_CODE2('O',:dept)"
                sSql += "   and exop.instcd = oprc.instcd " + vbCrLf
                sSql += "   and exop.prcpno = oprc.prcpno " + vbCrLf
                sSql += "   and exop.pid = oprc.pid " + vbCrLf
                sSql += "   and exop.prcpdd = oprc.prcpdd " + vbCrLf
                sSql += "   and j11.regno (+) = exop.pid" + vbCrLf
                sSql += "   and j11.orddt  (+) = exop.prcpdd" + vbCrLf
                sSql += "   and j11.ocs_key (+)  = exop.execprcpuniqno " + vbCrLf
                sSql += "union" + vbCrLf
                sSql += "select iprc.pid, iprc.prcpdd,iprc.prcphistcd,'I' iogbn" + vbCrLf
                sSql += ",(select cdnm  from com.zbcmcode a WHERE CDGRUPID = 'M0011' and cdid = iprc.PRCPSTATCD) PRCPSTATCD" + vbCrLf
                sSql += ",(select cdnm  from com.zbcmcode a WHERE CDGRUPID = 'M0011' and cdid = exip.EXECRCPTSTATCD) EXECRCPTSTATCD" + vbCrLf
                sSql += ", exip.execprcpuniqno" + vbCrLf
                sSql += ", case j11.spcflg when '1' then '바코드출력'" + vbCrLf
                sSql += "         when '2' then '채혈'" + vbCrLf
                sSql += "when '3' then '전달'" + vbCrLf
                sSql += " when '4' then '접수' " + vbCrLf
                sSql += "else 'X' end spcflg " + vbCrLf
                sSql += ", case j11.rstflg when '0' then '미결과'" + vbCrLf
                sSql += " when '1' then '검사중'" + vbCrLf
                sSql += " when '2' then '중간보고'" + vbCrLf
                sSql += " when '3' then '최종보고'" + vbCrLf
                sSql += " else 'X' end rstflg  " + vbCrLf
                sSql += "from emr.mmohiprc iprc ,emr.mmodexip exip , lj011m j11" + vbCrLf
                sSql += "where iprc.instcd = '031'" + vbCrLf
                sSql += "and iprc.prcpdd = :orddt " + vbCrLf
                sSql += "and iprc.pid =:regno" + vbCrLf
                sSql += "and iprc.prcpcd = :tordcd" + vbCrLf
                sSql += "and iprc.prcphistcd = 'O'" + vbCrLf
                sSql += "and iprc.ORDDEPTCD = FN_ACK_GET_DEPT_CODE2('I',:dept)"
                sSql += "and exip.instcd = iprc.instcd" + vbCrLf
                sSql += "and exip.prcpno = iprc.prcpno" + vbCrLf
                sSql += "and exip.pid = iprc.pid" + vbCrLf
                sSql += "and exip.prcpdd = iprc.prcpdd" + vbCrLf
                sSql += "and j11.regno (+) = exip.pid" + vbCrLf
                sSql += "and j11.orddt  (+) = exip.prcpdd" + vbCrLf
                sSql += "and j11.ocs_key (+)  = exip.execprcpuniqno" + vbCrLf

                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrddt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrddt))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsPid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPid))
                alParm.Add(New OracleParameter("tordcd", OracleDbType.Varchar2, rsTordcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTordcd))
                alParm.Add(New OracleParameter("dept", OracleDbType.Varchar2, rsTordcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrddt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrddt))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsPid.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPid))
                alParm.Add(New OracleParameter("tordcd", OracleDbType.Varchar2, rsTordcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTordcd))
                alParm.Add(New OracleParameter("dept", OracleDbType.Varchar2, rsTordcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))

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
                sSql += "  FROM lj010m j"

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

        Public Shared Function fnGet_WorkList_WGrp(ByVal rsWkYmd As String, ByVal rsWGrpCd As String, ByVal rsWKNoS As String, ByVal rsWkNoE As String,
                                                   ByVal rsSpcCds As String, ByVal rsTestCds As String, ByVal rsRstFlg As String,
                                                   ByVal rsBcNo As String, ByVal rbMbtType As Boolean,
                                                   Optional ByVal rbMicroBioYn As Boolean = False,
                                                   Optional ByVal rbSpcAdd As Boolean = True,
                                                   Optional ByVal rsWardCds As String = "",
                                                   Optional ByVal rsWardExcept As Boolean = False) As DataTable
            Dim sFn As String = "fnGet_WorkList_WGrp"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010M"
                If rbMicroBioYn Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm," + vbCrLf
                sSql += "       j.sex || '/' || j.age sexage," + vbCrLf
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno," + vbCrLf
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm," + vbCrLf
                sSql += "       FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno) ELSE '' END deptinfo," + vbCrLf
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt," + vbCrLf
                sSql += "       j.orddt," + vbCrLf '20140128 정선영 추가, 처방일(의뢰일자) 추가
                sSql += "       r.testcd, f6.tnmd, f6.tnmp, f6.dispseql, fn_ack_get_pat_befviewrst(r.bcno, r.testcd, r.spccd) bfviewrst," + vbCrLf
                'sSql += "       fn_ack_get_dr_remark(j.bcno) doctorrmk,"+vbcrlf
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)" + vbCrLf
                sSql += "          FROM lj011m ff" + vbCrLf
                sSql += "         WHERE bcno    = j.bcno" + vbCrLf
                sSql += "           AND spcflg IN ('1', '2', '3', '4')" + vbCrLf
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '" + vbCrLf
                sSql += "       ) doctorrmk," + vbCrLf
                sSql += "       j3.diagnm, NULL wlseq, r.spccd, j.deptcd " + vbCrLf
                sSql += "  FROM " + sTableNm + " r, lf060m f6, lf030m f3, lj010m j, lj013m j3" + vbCrLf
                sSql += " WHERE r.wkymd   = :wkymd" + vbCrLf
                sSql += "   AND r.wkgrpcd = :wgrpcd" + vbCrLf
                sSql += "   AND r.wkno   >= :wknos" + vbCrLf
                sSql += "   AND r.wkno   <= :wknoe" + vbCrLf

                alParm.Add(New OracleParameter("wkymd", OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                alParm.Add(New OracleParameter("wgrpcd", OracleDbType.Varchar2, rsWGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWGrpCd))
                alParm.Add(New OracleParameter("wknos", OracleDbType.Varchar2, rsWKNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWKNoS))
                alParm.Add(New OracleParameter("wknoe", OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))

                If rsSpcCds <> "" Then
                    sSql += "   AND j.spccd " + IIf(rbSpcAdd, " IN ", " NOT IN ").ToString + "('" + rsSpcCds.Replace(",", "','") + "')" + vbCrLf
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')" + vbCrLf
                End If

                If rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno" + vbCrLf
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                sSql += "   AND j.bcno   = r.bcno" + vbCrLf
                sSql += "   AND r.testcd = f6.testcd" + vbCrLf
                sSql += "   AND r.spccd  = f6.spccd" + vbCrLf
                sSql += "   AND f6.usdt <= r.tkdt" + vbCrLf
                sSql += "   AND f6.uedt >  r.tkdt" + vbCrLf
                sSql += "   AND r.spccd  = f3.spccd" + vbCrLf
                sSql += "   AND f3.usdt <= r.tkdt" + vbCrLf
                sSql += "   AND f3.uedt >  r.tkdt" + vbCrLf
                '20210810 jhs 병동조건 추가
                If rsWardCds <> "" Then
                    If rsWardExcept Then
                        sSql += "   And j.wardno not IN ('" + rsWardCds.Replace(",", "','") + "')" + vbCrLf
                    Else
                        sSql += "   And j.wardno IN ('" + rsWardCds.Replace(",", "','") + "')" + vbCrLf
                    End If

                End If
                '-------------------------------------
                sSql += "   AND ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))" + vbCrLf
                sSql += "   AND j.bcno   = j3.bcno (+)" + vbCrLf
                Dim sWhere As String = ""

                If rsRstFlg.Substring(0, 1) = "1" Then sWhere = "NVL(r.rstflg, '0') = '0'"
                If rsRstFlg.Substring(1, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '1'"
                If rsRstFlg.Substring(2, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '2'"
                If rsRstFlg.Substring(3, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '3'"

                sSql += "   AND (" + sWhere + ")"

                If rbMbtType Then sSql += "   AND NVL(f6.mbttype, '0') IN ('2', '3')"

                sSql += " ORDER BY workno, tkdt, f6.dispseql" '20130731 정선영 수정, 출력순서, 20130808 재수정

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try

        End Function

        Public Shared Function fnGet_WorkList_TGrp(ByVal rsPartSlip As String, ByVal rsTGrpCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String,
                                                         ByVal rsSpcCds As String, ByVal rsTestCds As String, ByVal rsRstFlg As String,
                                                         ByVal rsBcNo As String, ByVal rbMbtType As Boolean,
                                                         Optional ByVal rbMicroBioYn As Boolean = False,
                                                         Optional ByVal rbSpcSelect As Boolean = True,
                                                         Optional ByVal rsWardCds As String = "",
                                                         Optional ByVal rsWardExcept As Boolean = False) As DataTable
            Dim sFn As String = "fnGet_WorkList_TGrp"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                Dim sTableNm As String = "lr010M"
                If rbMicroBioYn Then sTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT DISTINCT" + vbCrLf
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm," + vbCrLf
                sSql += "       j.sex || '/'|| j.age sexage," + vbCrLf
                sSql += "       fn_ack_get_bcno_prt(j.bcno) prtbcno," + vbCrLf
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm," + vbCrLf
                sSql += "       FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) || CASE WHEN j.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(j.wardno)  ELSE '' END deptinfo," + vbCrLf
                sSql += "       f3.spcnmp, f3.spcnmd, fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt," + vbCrLf
                sSql += "       j.orddt," + vbCrLf '20140128 정선영 추가, 처방일(의뢰일자) 추가
                sSql += "       r.testcd, f6.tnmd, f6.tnmp, fn_ack_get_pat_befviewrst(r.bcno, r.testcd, r.spccd) bfviewrst," + vbCrLf
                sSql += "       (SELECT SUBSTR(xmlagg(xmlelement(ff, ',' || ff.doctorrmk)).extract('//text()'), 2)" + vbCrLf
                sSql += "          FROM lj011m ff" + vbCrLf
                sSql += "         WHERE bcno    = j.bcno" + vbCrLf
                sSql += "           AND spcflg IN ('1', '2', '3', '4')" + vbCrLf
                sSql += "           AND NVL(doctorrmk, ' ') <> ' '" + vbCrLf
                sSql += "       ) doctorrmk," + vbCrLf
                sSql += "       j3.diagnm, NULL wlseq, r.spccd, j.deptcd " + vbCrLf
                sSql += "  FROM " + sTableNm + " r, lf060m f6, lf030m f3, lj010m j, lj013m j3" + vbCrLf
                sSql += " WHERE r.tkdt >= :dates" + vbCrLf
                sSql += "   AND r.tkdt <= :datee || '5959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

                If rsSpcCds <> "" Then
                    sSql += "   AND j.spccd " + IIf(rbSpcSelect, " IN ", " NOT IN ").ToString + "('" + rsSpcCds.Replace(",", "','") + "')" + vbCrLf
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')" + vbCrLf
                ElseIf rsTGrpCd <> "" Then
                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf065m WHERE tgrpcd = :tgrpcd)" + vbCrLf

                    alParm.Add(New OracleParameter("tgrpcd", OracleDbType.Varchar2, rsTGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTGrpCd))
                ElseIf rsPartSlip <> "" Then
                    sSql += "   AND (r.testcd, r.spccd) IN (SELECT testcd, spccd FROM lf060m WHERE partcd = :partcd AND slipcd = :slipcd) " + vbCrLf
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPartSlip.Substring(1, 1)))
                End If

                If rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno" + vbCrLf
                    alParm.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                End If

                sSql += "   AND NVL(r.wkymd, ' ') <> ' '" + vbCrLf
                sSql += "   AND j.bcno   = r.bcno" + vbCrLf
                sSql += "   AND r.testcd = f6.testcd" + vbCrLf
                sSql += "   AND r.spccd  = f6.spccd" + vbCrLf
                sSql += "   AND f6.usdt <= r.tkdt" + vbCrLf
                sSql += "   AND f6.uedt >  r.tkdt" + vbCrLf
                sSql += "   AND r.spccd  = f3.spccd" + vbCrLf
                sSql += "   AND f3.usdt <= r.tkdt" + vbCrLf
                sSql += "   AND f3.uedt >  r.tkdt" + vbCrLf
                '20210810 jhs 병동조건 추가
                If rsWardCds <> "" Then
                    If rsWardExcept Then
                        sSql += "   And j.wardno not IN ('" + rsWardCds.Replace(",", "','") + "')" + vbCrLf
                    Else
                        sSql += "   And j.wardno IN ('" + rsWardCds.Replace(",", "','") + "')" + vbCrLf
                    End If
                End If
                '-------------------------------------
                sSql += " And ((f6.tcdgbn = 'B' AND f6.titleyn = '0') OR f6.tcdgbn IN ('S', 'P', 'C'))" + vbCrLf
                sSql += "   AND j.bcno  = j3.bcno (+)" + vbCrLf

                Dim sWhere As String = ""

                If rsRstFlg.Substring(0, 1) = "1" Then sWhere = "NVL(r.rstflg, '0') = '0'"
                If rsRstFlg.Substring(1, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '1'"
                If rsRstFlg.Substring(2, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '2'"
                If rsRstFlg.Substring(3, 1) = "1" Then sWhere += IIf(sWhere = "", "", " OR ").ToString + "NVL(r.rstflg, '0') = '3'"

                sSql += "   AND (" + sWhere + ")"

                If rbMbtType Then sSql += "   AND NVL(f6.mbttype, '0') IN ('2', '3')" + vbCrLf

                If rbMicroBioYn Then
                    sSql += " ORDER BY workno, tkdt, bcno" + vbCrLf
                Else
                    sSql += " ORDER BY tkdt, bcno" + vbCrLf
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message, ex))
            End Try
        End Function

        Public Shared Function fnGet_WorkList_OrdDate(ByVal rsRegNo As String, ByVal rsBcno As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_WGrp"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT orddt"
                sSql += "  FROM lj010m"
                sSql += " WHERE bcno  = :bcno"
                sSql += "   AND RTRIM(regno) = :regno"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Public Shared Function fnGet_WorkList_PbBm(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_PbBm"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "  SELECT DISTINCT"
                sSql += "	      a.patno, TO_CHAR(a.rgtdate,'yyyy-mm-dd'), TO_CHAR(a.orddate,'yyyy-mm-dd'), "
                sSql += "	      a.meddept, FN_ACK_GET_DEPT_NAME(t.iogbn, t.deptcd) deptnm, a.meddr, "
                sSql += "	      FN_ACK_GET_USR_NAME(a.meddr), a.wardno, a.roomno, "
                sSql += "	      TO_CHAR(a.pbsdate,'yyyy-mm-dd') as pbsdate, TO_CHAR(a.bmsdate,'yyyy-mm-dd') as bmsdate, a.slideno, "
                sSql += "	      a.pbr, a.bmr, a.diagname, "
                sSql += "	      a.diagcode, a.gbio, a.itemidx, "
                sSql += "	      a.fevertxt, a.weighttxt, a.abdosize, "
                sSql += "	      a.lymphosize, a.lymphosite, a.hepatosize, "
                sSql += "	      a.splenosize, a.drug, a.duration, "
                sSql += "	      TO_CHAR(a.labdate,'yyyy-mm-dd') as labdate, a.rbc, a.mcv, "
                sSql += "	      a.wbc, a.plt, a.fe_tibc, "
                sSql += "	      a.hb, a.mch, a.diffcount, "
                sSql += "	      a.feffitin, a.hct, a.mchc, "
                sSql += "	      a.proalb, a.other, a.reti, "
                sSql += "	      a.rdw, a.irf, c.orddt, "
                sSql += "	      a.flag, t.sex, t.age, t.dage "
                sSql += "    FROM lr010m r, lj011m c, mdpbmsheet a, lj010m t "
                sSql += "   WHERE c.bcno    = :bcno"
                sSql += "     AND r.bcno    = c.bcno "
                sSql += "     AND r.bcno    = t.bcno "
                sSql += "     AND r.tclscd  = c.tclscd "
                sSql += "     AND c.regno   = a.patno "
                sSql += "     AND TO_DATE(SUBSTR(c.orgorddt, 1, 8), 'YYYYMMDD')  = TRUNC (a.orddate) "
                sSql += "     AND a.flag    IS NULL "

                al.Add(New OracleParameter("bcno", rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function
        Public Shared Function fnGet_WorkList_BFtest_diag(ByVal rsBcNo As String, ByVal rsPatno As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_BFtest"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                '--------------------------------------
                '환자 진단명 가져오는 기준
                '1. 환자번호에 해당하는 것
                '2. main(주)진단명인지 여부 
                '3. 해당 검체번호의 진료과에 해당하는 것
                '----------------------------------------
                sSql += " selecT vw.patno, vw.meddept, fn_ack_date_str(vw.rgsttm, 'yyyy-mm-dd hh24:mi:ss') rgsttm, vw.diagnm_eng, vw.diagnm_han, vw.maindiag" + vbCrLf
                sSql += "   from VW_ACK_OCS_PAT_DIAG_INFO vw " + vbCrLf
                sSql += "  where vw.patno = :patno" + vbCrLf
                sSql += "    And vw.maindiag = 'Y'" + vbCrLf
                sSql += "    And vw.meddept = (selecT deptcd from lj010m where bcno = :bcno )" + vbCrLf
                sSql += "  order by rgsttm desc" + vbCrLf


                al.Add(New OracleParameter("patno", rsPatno))
                al.Add(New OracleParameter("bcno", rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function
        '20210414 jhs 체액검사정보 가져오기
        Public Shared Function fnGet_WorkList_BFtest(ByVal rsBcNo As String, Optional ByVal rsTestCd As String = "", Optional ByVal rsChk As Boolean = True, Optional ByVal rsChkOrddt As Boolean = False) As DataTable
            Dim sFn As String = "fnGet_WorkList_BFtest"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "  selecT r.bcno, r.testcd,f6.tnmd,substr(r.fndt,1,4) || '-'||substr(r.fndt,5,2) || '-'||substr(r.fndt,7,2) fndt ,r.spccd, f3.spcnm , r.viewrst , j13.diagnm, j13.diagnm_eng , " + vbCrLf
                sSql += "    j.patnm, j.sex,j.age, j1.fkocs , j1.orddt, j1.regno, substr(r.tkdt,1,4) || '-'||substr(r.tkdt,5,2) || '-'||substr(r.tkdt,7,2) tkdt" + vbCrLf
                'sSql += "    ,'' --진단명 일시없음" + vbCrLf
                sSql += "    ,f6.rstunit" + vbCrLf
                sSql += "   from lr010m r" + vbCrLf
                sSql += "   inner join lj010m j " + vbCrLf
                sSql += "       on  r.bcno = j.bcno" + vbCrLf
                sSql += "   inner join lj011m j1" + vbCrLf
                sSql += "       on r.bcno = j1.bcno " + vbCrLf
                sSql += "   inner join lf060m f6" + vbCrLf
                sSql += "       on r.testcd = f6.testcd" + vbCrLf
                sSql += "       and r.spccd = f6.spccd" + vbCrLf
                sSql += "       and r.tkdt >= f6.usdt" + vbCrLf
                sSql += "       and r.tkdt <= f6.uedt" + vbCrLf
                sSql += "   inner join lf030m f3" + vbCrLf
                sSql += "       on r.spccd = f3.spccd" + vbCrLf
                sSql += "       and r.tkdt >= f3.usdt" + vbCrLf
                sSql += "       and r.tkdt <= f3.uedt" + vbCrLf
                sSql += "   inner join lj013m j13" + vbCrLf
                sSql += "       on r.bcno = j13.bcno " + vbCrLf

                If rsChk Then ' 최근 검사결과에서 
                    sSql += "   where j.regno  = (selecT j1.regno from lj011m j1 where bcno =  :bcno)" + vbCrLf
                    'sSql += "     and j1.orddt = (selecT j1.orddt from lj011m j1 where bcno =  :bcno)" + vbCrLf
                    al.Add(New OracleParameter("bcno", rsBcNo))
                    'al.Add(New OracleParameter("bcno", rsBcNo))
                    If rsChkOrddt Then
                        sSql += " and j1.orddt <> (selecT j1.orddt from lj011m j1 where bcno =  :bcno)" + vbCrLf
                        al.Add(New OracleParameter("bcno", rsBcNo))
                    End If
                Else
                    sSql += "   where j.bcno  = :bcno" + vbCrLf
                    al.Add(New OracleParameter("bcno", rsBcNo))
                End If

                If rsTestCd <> "" Then ' 검사코드 조건 조회 시
                    sSql += "     and r.testcd = :testcd"
                    al.Add(New OracleParameter("testcd", rsTestCd))
                End If

                sSql += "   order by r.bcno desc"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function
        '최근 등록번호에 ㄴ
        Public Shared Function fnGet_WorkList_BFtest_spc(ByVal rsRegno As String, ByVal rsTestCd As String, ByVal rsSpccd As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_BFtest"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += " selecT r.bcno, r.testcd, f6.tnmd, r.viewrst" + vbCrLf
                sSql += "       ,substr(r.tkdt, 1, 4) || '-'||substr(r.tkdt,5,2) || '-'||substr(r.tkdt,7,2) tkdt " + vbCrLf
                sSql += "       ,f6.rstunit" + vbCrLf
                sSql += "  from lr010m r" + vbCrLf
                sSql += " inner join lf060m f6" + vbCrLf
                sSql += "    On r.testcd = f6.testcd" + vbCrLf
                sSql += "   and r.tkdt >= f6.usdt" + vbCrLf
                sSql += "   and r.tkdt <= f6.uedt " + vbCrLf
                sSql += " where r.regno  ='" + rsRegno + "'" + vbCrLf
                sSql += "   and f6.testcd ='" + rsTestCd + "'" + vbCrLf
                sSql += "   and f6.spccd = '" + rsSpccd + "'" + vbCrLf
                sSql += "   order by r.bcno desc" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function



        Public Shared Function fnGet_WorkList_BFtest_rr(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_BFtest_rr"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "" + vbCrLf
                sSql += "  selecT x.bcno ,x.testcd ,x.spccd ,x.tnm , x.viewrst , x.rstunit, x.tkdt" + vbCrLf
                sSql += "    from (  select r1.bcno, r1.testcd, r1.spccd, f6.tnm, r1.viewrst, f6.rstunit, substr(r1.tkdt, 1, 4) || '-'||substr(r1.tkdt,5,2) || '-'||substr(r1.tkdt,7,2) tkdt " + vbCrLf
                sSql += "              from rr010m r1" + vbCrLf
                sSql += "             inner join rf060m f6" + vbCrLf
                sSql += "                on r1.testcd = f6.testcd" + vbCrLf
                sSql += "               and r1.spccd = f6.spccd      " + vbCrLf
                sSql += "               and r1.tkdt >= f6.usdt" + vbCrLf
                sSql += "               and r1.tkdt <= f6.uedt" + vbCrLf
                sSql += "             where r1.regno = (selecT j1.regno from lj011m j1 where bcno =  :bcno)" + vbCrLf
                al.Add(New OracleParameter("bcno", rsBcNo))
                sSql += "               and r1.testcd in (" + rsTestCd + ")" + vbCrLf
                sSql += "             order by r1.bcno desc" + vbCrLf
                sSql += "          ) x" + vbCrLf
                sSql += "     where rownum = 1 " + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function
        '------------------------------------------------------------------------------------

        Public Shared Function fnGet_WorkList_cs(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "fnGet_WorkList_cs"

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       FN_ACK_DATE_STR(b.meddate, 'YYYY-MM-DD') || '|' || b.diagnm_eng AS diaginfo,"
                sSql += "       b.meddate"
                sSql += "  FROM lj010m j, vw_ack_ocs_pat_diag_info b"
                sSql += " WHERE j.bcno    = :bcno"
                sSql += "   AND b.instcd  = '" + PRG_CONST.SITECD + "'"
                sSql += "   AND j.regno   = b.patno"
                sSql += "   AND SUBSTR(j.bcno , 1, 8) >= b.meddate"
                sSql += "   AND ROWNUM    = 1"
                sSql += " ORDER BY b.meddate"

                al.Add(New OracleParameter("bcno", rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Public Shared Function fnget_worklist_pastrst(ByVal rsBcno As String) As DataTable
            Dim sfn As String = "fnget_worklist_pastrst"

            Try
                Dim ssql As String = ""
                Dim al As New ArrayList

                ssql = ""
                ssql += "SELECT b.regno, b.bcno, b.testcd, b.spccd, b.viewrst, SUBSTR(b.rstdt, 1, 8) bffndt"
                ssql += " FROM (SELECT r.regno, r.testcd, r.spccd, max(r.tkdt) tkdt"
                ssql += "         FROM lr010m r, lj010m j"
                ssql += "        WHERE r.regno = j.regno"
                ssql += "          and j.bcno  = :bcno"
                ssql += "          and r.testcd IN('LH102','LH105','LH101','LH109','LC124','LC125','LH103','LH106','LH121','L1299','LH104','LH107','LC118','LC119','LH123','LH108','LH124','LC124','LC125','LH378') "
                ssql += "          group by r.regno, r.testcd, r.spccd) a, lr010m b "
                ssql += "WHERE a.regno = b.regno"
                ssql += "  AND a.testcd = b.testcd"
                ssql += "  AND a.spccd = b.spccd"
                ssql += "  AND b.tkdt  = a.tkdt"
                ssql += "  AND b.rstflg IN ('2', '3')"
                ssql += "  AND b.testcd IN('LH102','LH105','LH101','LH109','LC124','LC125','LH103','LH106','LH121','L1299','LH104','LH107','LC118','LC119','LH123','LH108','LH124','LC124','LC125','LH378') "
                ssql += "GROUP BY b.regno, b.bcno, b.testcd, b.spccd, b.viewrst, SUBSTR(b.rstdt, 1, 8)"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(ssql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sfn, ex))

            End Try
        End Function

        Public Shared Function fnget_worklist_pastrst_lym(ByVal rsBcno As String) As DataTable
            Dim sfn As String = "fnget_worklist_pastrst"

            Try
                Dim ssql As String = ""
                Dim al As New ArrayList


                'ssql += "SELECT b.regno, b.bcno, b.testcd, b.spccd, b.viewrst, SUBSTR(b.rstdt, 1, 8) bffndt"
                'ssql += " FROM (SELECT r.regno, r.testcd, r.spccd, max(r.tkdt) tkdt"
                'ssql += "         FROM lr010m r, lj010m j"
                'ssql += "        WHERE r.regno = j.regno"
                'ssql += "          and j.bcno  = :bcno"
                'ssql += "          and r.testcd IN('LH101','LH12103')"
                'ssql += "          group by r.regno, r.testcd, r.spccd) a, lr010m b "
                'ssql += "WHERE a.regno = b.regno"
                'ssql += "  AND a.testcd = b.testcd"
                'ssql += "  AND a.spccd = b.spccd"
                'ssql += "  AND b.tkdt  = a.tkdt"
                'ssql += "  AND b.rstflg IN ('2', '3')"
                'ssql += "  AND b.testcd IN('LH101','LH12103')"
                'ssql += "GROUP BY b.regno, b.bcno, b.testcd, b.spccd, b.viewrst, SUBSTR(b.rstdt, 1, 8)"
                '<20140915 wbc결과 당일처방것 가져오게 수정 
                ssql += "SELECT b.regno, b.bcno, b.testcd, b.spccd, b.viewrst, SUBSTR (b.rstdt, 1, 8) fndt"
                ssql += "  FROM   (SELECT   w.bcno, w.spccd, w.regno     "
                ssql += "            FROM   (SELECT   regno, orddt"
                ssql += "                      FROM   lj010m"
                ssql += "                     WHERE   bcno = :bcno "
                ssql += "                   ) l, lj010m w"
                ssql += "           WHERE       l.regno = w.regno"
                ssql += "             AND substr(l.orddt,1,8) = substr(w.orddt,1,8)"
                ssql += "             AND w.bcclscd = 'H1') a,lr010m b"
                ssql += " WHERE a.bcno = b.bcno "
                ssql += "   AND a.regno = b.regno"
                ssql += "   AND a.spccd = b.spccd"
                ssql += "   AND b.testcd IN ('LH101', 'LH12103')"
                ssql += "   AND b.rstflg IN ('2', '3')"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, Replace(rsBcno, "-", "")))

                DbCommand()
                Return DbExecuteQuery(ssql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sfn, ex))

            End Try
        End Function

    End Class

#End Region

End Namespace