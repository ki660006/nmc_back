'>> 결과조회
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Namespace APP_V
    Public Class CommFn
        Private Const msFile As String = "File : CGLISAPP_V.vb, Class : LISAPP.APP_V.CommFn" + vbTab


        Public Shared Function fnGet_OrderDate_Max(ByVal rsRegNo As String) As String
            Dim sFn As String = "Function fnGet_OrderDate_Max"

            Try
                If rsRegNo = "" Then Return ""

                Dim sSql As String = ""
                Dim dt As DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT MAX(a.orddt) orddt"
                sSql += "  FROM ("
                sSql += "        SELECT fn_ack_date_str(max(orddt), 'yyyy-mm-dd') orddt"
                sSql += "          FROM lj010m"
                sSql += "         WHERE regno = :regno"
                sSql += "           AND NVL(spcflg, '0') IN ('1', '2', '3', '4')"
                sSql += "         UNION "
                sSql += "        SELECT fn_ack_date_str(max(orddt), 'yyyy-mm-dd') orddt"
                sSql += "          FROM rj010m"
                sSql += "         WHERE regno = :regno"
                sSql += "           AND NVL(spcflg, '0') IN ('1', '2', '3', '4')"
                sSql += "       ) a"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows(0).Item(0).ToString <> "" Then Return dt.Rows(0).Item(0).ToString

                sSql = ""
                sSql += "SELECT TO_CHAR(MAX(a.orddate), 'yyyy-mm-dd') orddt"
                sSql += "  FROM mdresult a, lf100m b"
                sSql += " WHERE a.patno  = :regno"
                sSql += "   AND a.slipcd = b.tordslip"


                al.Clear()
                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows(0).Item(0).ToString = "" Then
                    Return Format(Now, "yyyy-MM-dd").ToString
                Else
                    Return dt.Rows(0).Item(0).ToString
                End If


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_LinkStRstView(ByVal rsBcno As String, ByVal rsTliscd As String) As String
            Dim sFn As String = "Function fnGet_OrderDate_Max"

            Try
                If rsBcno = "" Then Return ""

                Dim sSql As String = ""
                Dim dt As DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT b.bcno, b.testcd"
                sSql += "  FROM lr010m a, lrs10m b, lf060m f"
                sSql += " WHERE a.bcno = :bcno"
                sSql += "   AND a.bcno = b.bcno"
                sSql += "   AND a.testcd = b.testcd"
                sSql += "   AND a.testcd = f.testcd"
                sSql += "   AND a.tkdt  >= f.usdt"
                sSql += "   AND a.tkdt  <  f.uedt"
                sSql += "   AND f.tliscd = :tliscd"
                sSql += "   AND a.rstflg IN ('2', '3')"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))
                al.Add(New OracleParameter("tliscd", OracleDbType.Varchar2, rsTliscd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTliscd))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item(0).ToString + dt.Rows(0).Item(1).ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_RstDate_Max(ByVal rsRegNo As String) As String
            Dim sFn As String = "Function fnGet_RstDate_Max"

            Try
                If rsRegNo = "" Then Return ""

                Dim sSql As String = ""
                Dim dt As DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT MAX(a.rstdt) rstdt"
                sSql += "  FROM ("
                sSql += "        SELECT fn_ack_date_str(max(rstdt), 'yyyy-mm-dd') rstdt"
                sSql += "          FROM lj011m"
                sSql += "         WHERE regno = :regno"
                sSql += "           AND NVL(spcflg, '0') IN ('1', '2', '3', '4')"
                sSql += "         UNION "
                sSql += "        SELECT fn_ack_date_str(max(rstdt), 'yyyy-mm-dd') rstdt"
                sSql += "          FROM rj011m"
                sSql += "         WHERE regno = :regno"
                sSql += "           AND NVL(spcflg, '0') IN ('1', '2', '3', '4')"
                sSql += "       ) a"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If dt.Rows(0).Item(0).ToString = "" Then
                    Return Format(Now, "yyyy-MM-dd").ToString
                Else
                    Return dt.Rows(0).Item(0).ToString
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_List_RegNo_Order(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String, ByRef r_dt_bcno As DataTable, ByVal rbTempRstState As Boolean) As DataTable
            Dim sFn As String = "Function fnGet_List_RegNo_Order"

            Try
                Dim sSql As String = ""
                Dim dt As DataTable
                Dim al As New ArrayList

                sSql = "pkg_ack_qry.pkg_get_list_regno_order_1"

                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_orddts", rsDayS))
                al.Add(New OracleParameter("rs_orddte", rsDayE))
                al.Add(New OracleParameter("rs_viwflg", IIf(rbTempRstState, "Y", "N").ToString))

                DbCommand()

                dt = DbExecuteQuery(sSql, al, False)
                If r_dt_bcno Is Nothing Then r_dt_bcno = New DataTable

                sSql = "pkg_ack_qry.pkg_get_list_regno_order_2"

                al.Clear()
                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_orddts", rsDayS))
                al.Add(New OracleParameter("rs_orddte", rsDayE))

                DbCommand()
                r_dt_bcno = DbExecuteQuery(sSql, al, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_List_RegNo_Result_WithSlipName(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String, ByVal rbTempRstState As Boolean) As DataTable
            Dim sFn As String = "Function Get_List_RegNo_Result_WithSlipName"

            Try

                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = "pkg_ack_qry.pkg_get_list_regno_slip"

                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_rstdts", rsDayS))
                al.Add(New OracleParameter("rs_rstdte", rsDayE))
                al.Add(New OracleParameter("rs_viwflg", IIf(rbTempRstState, "Y", "N").ToString))

                DbCommand()
                dt = DbExecuteQuery(sSql, al, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_List_RegNo_ResultOnly(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String, ByVal rbTempRstState As Boolean) As DataTable
            Dim sFn As String = "Function Get_List_RegNo_ResultOnly"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                sSql = "pkg_ack_qry.pkg_get_list_regno_result"

                al.Add(New OracleParameter("rs_regno", rsRegNo))
                al.Add(New OracleParameter("rs_orddts", rsDayS))
                al.Add(New OracleParameter("rs_orddte", rsDayE))
                al.Add(New OracleParameter("rs_viwflg", IIf(rbTempRstState, "Y", "N").ToString))

                DbCommand()
                dt = DbExecuteQuery(sSql, al, False)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Public Shared Function Get_List_RegNo_Order_Doner(ByVal rsRegNo As String, _
                                                          ByVal rsDayS As String, ByVal rsDayE As String, _
                                                          ByRef r_dt_bcno As DataTable) As DataTable
            Dim sFn As String = "Function Get_List_RegNo_Order_Doner"

            Try
                Dim sSql As String = ""
                Dim dt As DataTable
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       b.fkocs, j.regno, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, t.tordslip, o.tordslipnm,"
                sSql += "       j.deptcd, fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm, fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN MIN(NVL(c.spcflg, '0')) < '4' THEN 'N' ELSE '' END tkflg,"
                sSql += "       CASE WHEN MAX(NVL(c.spcflg, '0')) IN ('1', '2', '3') THEN '채혈'"
                sSql += " 	         WHEN MAX(NVL(c.rstflg, '0') = '0' THEN '접수',"
                sSql += "            WHEN MIN(NVL(c.rstflg, '0') = '0' THEN '최종보고'"
                sSql += "            WHEN MIN(NVL(c.rstflg, '0') = '예비보고'"
                sSql += "            ELSE '임시결과'"
                sSql += "       END state,"
                sSql += "       '' tkdt, '' fndt "
                sSql += "       NVL(o.dispseq, 999) sort1"
                sSql += "  FROM lj011m c, lj010m j, lf060m t, lf100m o, lb010m b"
                sSql += " WHERE b.regno    = :regno"
                sSql += "   AND j.bcno     = c.bcno"
                sSql += "   and j.bcno     = b.bcno"
                sSql += "   and c.tclscd   = t.testcd"
                sSql += "   AND c.spccd    = t.spccd"
                sSql += "   AND c.colldt  >= t.usdt"
                sSql += "   AND c.colldt  <  t.uedt"
                sSql += "   and t.tordslip = o.tordslip"
                sSql += "   AND c.colldt  >= o.usdt"
                sSql += "   AND c.colldt  <  o.uedt"
                sSql += "   and NVL(c.spcflg, '0') > '0'"
                sSql += "   and NVL(j.spcflg ,'0') > '0'"
                sSql += "   and t.bcclscd = '" + PRG_CONST.BCCLS_BloodBank + "'"
                sSql += "   and b.orddt between :dates AND :datee || '235959'" + vbCrLf
                sSql += " GROUP BY j.regno, j.orddt, o.dispseq, t.tordslip, o.tordlipnm, j.deptcd, j.iogbn, j.doctorcd, b.fkocs"
                sSql += " ORDER BY orddt DESC"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If r_dt_bcno Is Nothing Then r_dt_bcno = New DataTable

                al.Clear()

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       j.regno, fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, t.tordslip,"
                sSql += "       fn_dept_name(j.iogbn, j.deptcd) deptnm, j.deptcd,"
                sSql += "       fn_doctor_name(j.doctorcd) doctornm, c.bcno, , c.fkocs,"
                sSql += "       c.iogbn, t.sectcd, CASE WHEN t.bcclscd  = '" + PRG_CONST.BCCLS_BloodBank + "' THEN 1 ELSE 2 END sortt"
                sSql += "  FROM lj010m j, lj011m c, lf060m t"
                sSql += " WHERE j.regno   = :regno"
                sSql += "   AND j.orddt   BETWEEN :dates AND :datee || '235959'"
                sSql += "   AND j.bcno    = c.bcno"
                sSql += "   AND c.tclscd  = t.testcd"
                sSql += "   AND c.spccd   = t.spccd"
                sSql += "   AND c.colldt >= t.usdt"
                sSql += "   AND c.colldt <  t.uedt"
                sSql += "   AND NVL(j.spcflg ,'0') > '0'"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))


                DbCommand()
                r_dt_bcno = DbExecuteQuery(sSql, al)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_Rst_Total(ByVal rsBcNo As String, ByRef r_dt_micro As DataTable, ByRef r_dt_cmt As DataTable, _
                                               Optional ByVal rsPTclsCd As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Rst_Total"
            Dim sSql As String = ""
            Try

                Dim dt As DataTable
                Dim al As New ArrayList

                If rsBcNo.Length = 10 Then
                    sSql = "pkg_ack_qry.pkg_get_rst_total_old"
                ElseIf PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql = "pkg_ack_qry.pkg_get_rst_total_m"
                ElseIf PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql = "pkg_ack_qry.pkg_get_rst_total_r"
                Else
                    sSql = "pkg_ack_qry.pkg_get_rst_total"
                End If

                al.Add(New OracleParameter("rs_bcno", rsBcNo))
                al.Add(New OracleParameter("rs_tclscd", IIf(rsPTclsCd = "", "ALL", rsPTclsCd).ToString))
                al.Add(New OracleParameter("rs_qryid", USER_INFO.USRID))
                al.Add(New OracleParameter("rs_qryip", USER_INFO.LOCALIP))

                DbCommand()
                dt = DbExecuteQuery(sSql, al, False)

                If r_dt_micro Is Nothing Then r_dt_micro = New DataTable

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) And dt.Select("mbttype > '1'").Length > 0 Then
                    al.Clear()

                    sSql = "pkg_ack_qry.pkg_get_rst_micro"

                    al.Add(New OracleParameter("rs_bcno", rsBcNo))

                    DbCommand()
                    r_dt_micro = DbExecuteQuery(sSql, al, False)
                End If

                If r_dt_cmt Is Nothing Then r_dt_cmt = New DataTable

                If dt.Rows.Count > 0 Then
                    al.Clear()

                    sSql = "pkg_ack_qry.pkg_get_rst_comment"

                    al.Add(New OracleParameter("rs_bcno", rsBcNo))

                    DbCommand()
                    r_dt_cmt = DbExecuteQuery(sSql, al, False)
                Else
                    al.Clear()

                    sSql = "pkg_ack_qry.pkg_get_rst_spcinfo"

                    al.Add(New OracleParameter("rs_bcno", rsBcNo))
                    al.Add(New OracleParameter("rs_msgnotk", FixedVariable.gsMsg_NoTk))

                    DbCommand()
                    dt = DbExecuteQuery(sSql, al, False)


                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_Rst_Special(ByVal rsBcNo As String, ByVal rsTestCd As String) As DataTable
            Dim sFn As String = "Function fnGet_Rst_Special(String, String) As DataTable"
            Try

                Dim dt As New DataTable
                Dim sSql As String = ""
                Dim al As New ArrayList

                sSql = ""
                sSql += "SELECT s.rstrtf"
                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM lm010m r, lrs10m s"
                ElseIf PRG_CONST.BCCLS_RIS.Contains(rsBcNo.Substring(8, 2)) Then
                    sSql += "  FROM rr010m r, lrs10m s"
                Else
                    sSql += "  FROM lr010m r, lrs10m s"
                End If
                sSql += " WHERE s.bcno    = :bcno"
                sSql += "   AND s.testcd  = :testcd"
                sSql += "   AND r.bcno    = s.bcno"
                sSql += "   AND r.testcd  = s.testcd"
                sSql += "   AND r.rstflg IN ('2', '3')"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCd))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 배양결과 존재여부
        Public Shared Function fnGet_MicroRst(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = ""

            Try
                Dim sSql As String = ""
                Dim al As New ArrayList
                Dim dt As New DataTable

                sSql += "SELECT f.mbttype"
                sSql += "  FROM lm010m a, lf060m f"
                sSql += " WHERE a.bcno    = :bcno"
                sSql += "   AND a.testcd  = f.testcd"
                sSql += "   AND a.spccd   = f.spccd"
                sSql += "   AND a.tkdt   >= f.usdt"
                sSql += "   AND a.tkdt   <  f.uedt"
                sSql += "   AND f.mbttype = '2'"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                dt = DbExecuteQuery(sSql, al)

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) And dt.Select("mbttype > '1'").Length > 0 Then
                    al.Clear()

                    sSql = "pkg_ack_qry.pkg_get_rst_bacwithanti"

                    al.Clear()
                    al.Add(New OracleParameter("rs_bcno", rsBcNo))

                    DbCommand()
                    Return DbExecuteQuery(sSql, al, False)
                Else
                    Return New DataTable
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 특이결과 조회
        Public Shared Function fnGet_Abnormal_RegNo(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Function getDelSPcPatInfo(String) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(r.regid) regnm,"
                sSql += "       r.partcd, r.slipcd, r.regno, r.regid,"
                sSql += "       fn_ack_get_bcno_full(r.bcno) bcno,"
                sSql += "       r.cmtcont,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt, fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END dptward, 'L' partgbn"
                sSql += "  FROM lr050m r, lj010m j"
                sSql += " WHERE j.regno    = :regno"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.cfmdt IS NULL"
                sSql += " UNION "
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(r.regdt, 'yyyy-mm-dd hh24:mi:ss') regdt, fn_ack_get_usr_name(r.regid) regnm,"
                sSql += "       r.partcd, r.slipcd, r.regno, r.regid,"
                sSql += "       fn_ack_get_bcno_full(r.bcno) bcno,"
                sSql += "       r.cmtcont,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd') orddt, fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END dptward, 'R' partgbn"
                sSql += "  FROM rr050m r, rj010m j"
                sSql += " WHERE j.regno    = :regno"
                sSql += "   AND j.bcno     = r.bcno"
                sSql += "   AND r.cfmdt IS NULL"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        '-- 검체정보 조회(누적)
        Public Shared Function fnGet_SpcInfo(ByVal rsBcNo As String) As DataTable
            Dim sFn As String = "Function fnGet_SpcInfo(string)"

            Try
                Dim sSql As String = ""
                Dim dt As New DataTable
                Dim al As New ArrayList

                Dim rsTableNm As String = "lr010m"

                If PRG_CONST.BCCLS_MicorBio.Contains(rsBcNo.Substring(8, 2)) Then rsTableNm = "lm010m"

                sSql = ""
                sSql += "SELECT j.bcno, j.regno, j.patnm,"
                sSql += "       fn_ack_get_dept_abbr(j.iogbn, j.deptcd) deptnm, j.deptcd,"
                'sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm, j.sex || '/' ||"
                sSql += "       CASE WHEN j.dage <= 31  THEN TO_CHAR(j.dage) || 'd'"
                sSql += "            WHEN j.dage >  365 THEN TO_CHAR(j.age) ELSE TO_CHAR(TRUNC(j.dage/30)) || 'm' END sexage,"
                sSql += "       fn_ack_get_pat_info(j.regno, '', '') patinfo, fn_ack_date_str(j.entdt, 'yyyy-mm-dd') entday,"
                sSql += "       j.wardno || '/' || j.roomno wardroom,"
                sSql += "       CASE WHEN NVL(f6.fixrptyn, '0') = '1' THEN f6.fixrptusr ELSE '' END fixrptusr,"
                sSql += "       fn_ack_get_usr_name(f68.doctorid1) labdrnm,"
                sSql += "       fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       fn_ack_get_usr_name(CASE WHEN r.rstflg = '3' THEN r.fnid WHEN r.rstflg = '2' THEN r.mwid WHEN r.rstflg = '1' THEN r.regid ELSE '' END) rstusr,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt, fn_ack_get_usr_name(r.tkid) tkusr,"
                sSql += "       fn_ack_date_str(j11.colldt, 'yyyy-mm-dd hh24:mi') colldt, fn_ack_get_usr_name(j11.collid) collusr,"
                sSql += "       fn_ack_date_str(j.orddt, 'yyyy-mm-dd hh24:mi') orddt, j13.diagnm, j11.doctorrmk,"
                sSql += "       NULL remark2, fn_ack_get_infection(j.regno) infinfo"
                sSql += "  FROM " + rsTableNm + " r, lf060m f6,"
                sSql += "       lj011m j11, lf100M f68,"
                sSql += "       lj010m j,"
                sSql += "       lj013m j13"
                sSql += " WHERE j.bcno      = :bcno"
                sSql += "   AND r.testcd    = f6.testcd"
                sSql += "   AND r.spccd     = f6.spccd"
                sSql += "   AND f6.usdt    <= r.tkdt"
                sSql += "   AND f6.uedt    >  r.tkdt"
                sSql += "   AND j.bcno      = r.bcno"
                sSql += "   AND j.bcno      = j11.bcno"
                sSql += "   AND f6.tordslip = f68.tordslip"
                sSql += "   AND f68.usdt   <= r.tkdt"
                sSql += "   AND f68.uedt   >  r.tkdt"
                sSql += "   AND j.bcno      = j13.bcno (+)"
                sSql += "   AND CASE WHEN f6.tcdgbn || NVL(f6.reqsub, '0') = 'C0' THEN CASE WHEN NVL(r.viewrst, ' ') = ' ' THEN 'N' ELSE 'Y' END ELSE 'Y' END = 'Y'"
                sSql += "   AND CASE WHEN f6.tcdgbn || NVL(f6.rptyn, '0') IN ('C0', 'P0', 'S0') THEN 'N' ELSE 'Y' END = 'Y'"

                al.Add(New OracleParameter("bcno", OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 환자정보 조회(누적)
        Public Shared Function Get_SpcInfoByRegNo(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Function Get_SpcInfo(string)"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT '' bcno, :regno regno, fn_ack_get_pat_info(:regno, :usrid, :usrip) patinfo, '' sexage,"
                sSql += "       '' deptnm, '' deptcd, '' doctornm, '' entday, '' wardroom, '' fixrptusr, '' labdrnm,"
                sSql += "       '' rstdt, '' rstusr, '' tkdt, '' tkusr, '' colldt, '' collusr,"
                sSql += "       '' orddt , '' diagnm, '' doctorrmk , '' remark2,"
                sSql += "       fn_ack_get_infection(:regno) infinfo"
                sSql += "  FROM DUAL"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("usrid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                alParm.Add(New OracleParameter("usrip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 누적결과에서 처방SLIP 가져오기
        Public Shared Function fnGet_Result_rv_slip(ByVal rsRegNo As String, ByVal rsQryGbn As String, Optional ByVal rsDayS As String = "", Optional ByVal rsDayE As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Result_rv_slip(String, String, string) As DataTable"
            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rsQryGbn = "O" Then
                    sSql += "SELECT f10.tordslipnm slipnm, f10.tordslip slipcd, NVL(f10.dispseq, 999) dispseq"
                    sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf100m f10"
                    sSql += " WHERE j.regno     = :regno"
                    sSql += "   AND j.orddt    >= :dates || '000000'"
                    sSql += "   AND j.orddt    <= :datee || '235959'"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND NVL(j1.spcflg, '0') >= '4'"
                    sSql += "   AND j.bcno      = j1.bcno"
                    sSql += "   AND j1.tclscd   = f6.testcd"
                    sSql += "   AND j1.spccd    = f6.spccd"
                    sSql += "   AND j.bcprtdt  >= f6.usdt"
                    sSql += "   AND j.bcprtdt  <  f6.uedt"
                    sSql += "   AND f6.tordslip = f10.tordslip"
                    sSql += "   AND j.bcprtdt  >= f10.usdt"
                    sSql += "   AND j.bcprtdt  <  f10.uedt"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE.Replace("-", "")))

                    '-- 핵의학
                    sSql += " UNION "
                    sSql += "SELECT f10.tordslipnm slipnm, f10.tordslip slipcd, NVL(f10.dispseq, 999) dispseq"
                    sSql += "  FROM rj010m j, rj011m j1, rf060m f6, lf100m f10"
                    sSql += " WHERE j.regno     = :regno"
                    sSql += "   AND j.orddt    >= :dates || '000000'"
                    sSql += "   AND j.orddt    <= :datee || '235959'"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND NVL(j1.spcflg, '0') >= '4'"
                    sSql += "   AND j.bcno      = j1.bcno"
                    sSql += "   AND j1.tclscd   = f6.testcd"
                    sSql += "   AND j1.spccd    = f6.spccd"
                    sSql += "   AND j.bcprtdt  >= f6.usdt"
                    sSql += "   AND j.bcprtdt  <  f6.uedt"
                    sSql += "   AND f6.tordslip = f10.tordslip"
                    sSql += "   AND j.bcprtdt  >= f10.usdt"
                    sSql += "   AND j.bcprtdt  <  f10.uedt"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE.Replace("-", "")))

                    '-- 과거결과
                    sSql += " UNION "
                    sSql += "SELECT f10.tordslipnm slipnm, f10.tordslip slipcd, NVL(f10.dispseq, 999) dispseq"
                    sSql += "  FROM vw_ack_ocs_ord_info j,"
                    sSql += "       (SELECT testcd, spccd, usdt, uedt, tordslip, tordcd FROM lf060m"
                    sSql += "         UNION "
                    sSql += "        SELECT testcd, spccd, usdt, uedt, tordslip, tordcd FROM rf060m"
                    sSql += "       ) f6, lf100m f10"
                    sSql += " WHERE j.instcd      = '" + PRG_CONST.SITECD + "'"
                    sSql += "   AND j.prcpclscd  IN ('B2', 'B6')"
                    sSql += "   AND j.prcphistcd  = 'O'"
                    sSql += "   AND j.patno       = :regno"
                    sSql += "   AND j.orddate    >= :dates"
                    sSql += "   AND j.orddate    <= :datee"
                    sSql += "   AND j.ordcd       = f6.tordcd"
                    sSql += "   AND j.spccd       = f6.spccd"
                    sSql += "   AND f6.usdt      <= fn_ack_sysdate"
                    sSql += "   AND f6.uedt      >  fn_ack_sysdate"
                    sSql += "   AND f6.tordslip   = f10.tordslip"
                    sSql += "   AND f10.usdt     <= fn_ack_sysdate"
                    sSql += "   AND f10.uedt     >  fn_ack_sysdate"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE.Replace("-", "")))

                Else

                    sSql += "SELECT f2.slipnm, f2.partcd || f2.slipcd slipcd, NVL(f2.dispseq, 999) dispseq"
                    sSql += "  FROM lj010m j, lj011m j1, lf060m f6, lf021m f2,"
                    sSql += "       (SELECT bcno, tclscd FROM lr010m"
                    sSql += "         WHERE regno  = :regno"
                    sSql += "           AND tkdt  >= :dates || '000000'"
                    sSql += "           AND tkdt  <= :datee || '235959'"
                    sSql += "         UNION"
                    sSql += "        SELECT bcno, tclscd FROM lm010m"
                    sSql += "         WHERE regno  = :regno"
                    sSql += "           AND tkdt  >= :dates || '000000'"
                    sSql += "           AND tkdt  <= :datee || '235959'"
                    sSql += "       ) r"
                    sSql += " WHERE j.regno     = :regno"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND NVL(j1.spcflg, '0') >= '4'"
                    sSql += "   AND j.bcno     = j1.bcno"
                    sSql += "   AND j1.tclscd  = f6.testcd"
                    sSql += "   AND j1.spccd   = f6.spccd"
                    sSql += "   AND j.bcprtdt >= f6.usdt"
                    sSql += "   AND j.bcprtdt <  f6.uedt"
                    sSql += "   AND f6.partcd  = f2.partcd"
                    sSql += "   AND f6.slipcd  = f2.slipcd"
                    sSql += "   AND j.bcprtdt >= f2.usdt"
                    sSql += "   AND j.bcprtdt <  f2.uedt"
                    sSql += "   AND j1.bcno    = r.bcno"
                    sSql += "   AND j1.tclscd  = r.tclscd"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE.Replace("-", "")))
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE.Replace("-", "")))
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                    '-- 핵의학
                    sSql += " UNION "
                    sSql += "SELECT f2.slipnm, f2.partcd || f2.slipcd slipcd, NVL(f2.dispseq, 999) dispseq"
                    sSql += "  FROM rj010m j, rj011m j1, rf060m f6, rf021m f2,"
                    sSql += "       (SELECT bcno, tclscd FROM rr010m"
                    sSql += "         WHERE regno  = :regno"
                    sSql += "           AND tkdt  >= :dates || '000000'"
                    sSql += "           AND tkdt  <= :datee || '235959'"
                    sSql += "       ) r"
                    sSql += " WHERE j.regno    = :regno"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND NVL(j1.spcflg, '0') >= '4'"
                    sSql += "   AND j.bcno     = j1.bcno"
                    sSql += "   AND j1.tclscd  = f6.testcd"
                    sSql += "   AND j1.spccd   = f6.spccd"
                    sSql += "   AND j.bcprtdt >= f6.usdt"
                    sSql += "   AND j.bcprtdt <  f6.uedt"
                    sSql += "   AND f6.partcd  = f2.partcd"
                    sSql += "   AND f6.slipcd  = f2.slipcd"
                    sSql += "   AND j.bcprtdt >= f2.usdt"
                    sSql += "   AND j.bcprtdt <  f2.uedt"
                    sSql += "   AND j1.bcno    = r.bcno"
                    sSql += "   AND j1.tclscd  = r.tclscd"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE.Replace("-", "")))
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    '-- 과거결과
                    sSql += " UNION "
                    sSql += "SELECT f2.slipnm, f2.partcd || f2.slipcd slipcd, NVL(f2.dispseq, 999) dispseq"
                    sSql += "  FROM lisif.vw_ack_ocs_ord_info j, lf021m f2,"
                    sSql += "       (SELECT testcd, spccd, usdt, uedt, tordslip, tordcd, partcd, slipcd FROM lf060m"
                    sSql += "         UNION "
                    sSql += "        SELECT testcd, spccd, usdt, uedt, tordslip, tordcd, partcd, slipcd FROM rf060m"
                    sSql += "       ) f6"
                    sSql += " WHERE j.orddate  >= :dates"
                    sSql += "   AND j.orddate  <= :datee"
                    sSql += "   AND j.patno      = :regno"
                    sSql += "   AND j.ordcd     = f6.tordcd"
                    sSql += "   AND j.spccd     = f6.spccd"
                    sSql += "   AND f6.usdt    <= fn_ack_sysdate"
                    sSql += "   AND f6.uedt    >  fn_ack_sysdate"
                    sSql += "   AND f6.partcd   = f2.partcd"
                    sSql += "   AND f6.slipcd   = f2.slipcd"
                    sSql += "   AND f2.usdt    <= fn_ack_sysdate"
                    sSql += "   AND f2.uedt    >  fn_ack_sysdate"

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE.Replace("-", "")))
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                End If

                sSql += " ORDER BY dispseq, slipcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 해당 처방슬립의 오더 가져오기
        Public Shared Function fnGet_History_slip_test_rv(ByVal rsRegNo As String, ByVal rsSlipCd As String, ByVal rsQryGbn As String, _
                                                           Optional ByVal rsDayS As String = "", Optional ByVal rsDayE As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_History_slip_test_rv(String, String, String, String, String) As DataTable"
            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                If rsQryGbn = "O" Then
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.testcd, f.tnmd, f2.dispseq sort1, NVL(f.dispseql, 999) sort2, f.tordslip slipcd"
                    sSql += "  FROM lj010m j, lj011m j1, lf060m f, lf100m f2"
                    sSql += " WHERE j.regno    = :regno"
                    sSql += "   AND j.orddt   >= :dates"
                    sSql += "   AND j.orddt   <= :datee || '23595'"
                    sSql += "   AND j.bcno     = j1.bcno"
                    sSql += "   AND j1.tclscd  = f.testcd"
                    sSql += "   AND j1.spccd   = f.spccd"
                    sSql += "   AND j.bcprtdt >= f.usdt"
                    sSql += "   AND j.bcprtdt <  f.uedt"
                    sSql += "   AND f.tordslip = f2.tordslip"
                    sSql += "   AND j.bcprtdt >= f2.usdt"
                    sSql += "   AND j.bcprtdt <  f2.uedt"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND NVL(j1.spcflg, '0') >= '4'"
                    sSql += "   AND f.tordslip = :tordslip"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                    alParm.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))

                    '-- 핵의학
                    sSql += " UNION "
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.testcd, f.tnmd, f2.dispseq sort1, NVL(f.dispseql, 999) sort2, f.tordslip slipcd"
                    sSql += "  FROM rj010m j, rj011m j1, rf060m f, rf100m f2"
                    sSql += " WHERE j.regno    = :regno"
                    sSql += "   AND j.orddt   >= :dates"
                    sSql += "   AND j.orddt   <= :datee || '235959'"
                    sSql += "   AND j.bcno     = j1.bcno"
                    sSql += "   AND j1.tclscd  = f.testcd"
                    sSql += "   AND j1.spccd   = f.spccd"
                    sSql += "   AND j.bcprtdt >= f.usdt"
                    sSql += "   AND j.bcprtdt <  f.uedt"
                    sSql += "   AND f.tordslip = f2.tordslip"
                    sSql += "   AND j.bcprtdt >= f2.usdt"
                    sSql += "   AND j.bcprtdt <  f2.uedt"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND NVL(j1.spcflg, '0') >= '4'"
                    sSql += "   AND f.tordslip = :tordslip"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                    alParm.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))

                    '-- 과거결과
                    sSql += " UNION "
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.testcd, f.tnmd, f2.dispseq sort1, NVL(f.dispseql, 999) sort2, f.tordslip slipcd"
                    sSql += "  FROM vw_ack_ocs_ord_info j, rf060m f, rf100m f2"
                    sSql += " WHERE j.instcd      = '" + PRG_CONST.SITECD + "'"
                    sSql += "   AND j.prcpclscd  IN ('B2', 'B6')"
                    sSql += "   AND j.prcphistcd  = 'O'"
                    sSql += "   AND j.patno       = :regno"
                    sSql += "   AND j.orddate    >= :dates"
                    sSql += "   AND j.orddate    <= :datee"
                    sSql += "   AND j.ordcd       = f.tordcd"
                    sSql += "   AND j.spccd       = f.spccd"
                    sSql += "   AND f.usdt       <= fn_ack_sysdate"
                    sSql += "   AND f.uedt       >  fn_ack_sysdate"
                    sSql += "   AND f.tordslip    = f2.tordslip"
                    sSql += "   AND f2.usdt      <= fn_ack_sysdate"
                    sSql += "   AND f2.uedt      >  fn_ack_sysdate"
                    sSql += "   AND f.tordslip    = :tordslip"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                    alParm.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))

                Else
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.testcd, f.tnmd, f2.dispseq sort1, NVL(f.dispseql, 999) sort2, f.partcd || f.slipcd slipcd"
                    sSql += "  FROM lj010m j, lf060m f, lf021m f2,"
                    sSql += "       (SELECT bcno, tclscd, spccd FROM lr010m"
                    sSql += "         WHERE regno  = :regno"
                    sSql += "           AND tkdt  >= :dates"
                    sSql += "           AND tkdt  <= :datee || '235959'"
                    sSql += "         UNION "
                    sSql += "        SELECT bcno, tclscd, spccd FROM lm010m"
                    sSql += "         WHERE regno  = :regno"
                    sSql += "           AND tkdt  >= :dates"
                    sSql += "           AND tkdt  <= :datee || '235959'"
                    sSql += "       ) j1"
                    sSql += " WHERE j.bcno     = j1.bcno"
                    sSql += "   AND j1.tclscd  = f.testcd"
                    sSql += "   AND j1.spccd   = f.spccd"
                    sSql += "   AND j.bcprtdt >= f.usdt"
                    sSql += "   AND j.bcprtdt <  f.uedt"
                    sSql += "   AND f.partcd   = f2.partcd"
                    sSql += "   AND f.slipcd   = f2.slipcd"
                    sSql += "   AND j.bcprtdt >= f2.usdt"
                    sSql += "   AND j.bcprtdt <  f2.uedt"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))

                    '-- 핵의학
                    sSql += " UNION "
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.testcd, f.tnmd, f2.dispseq sort1, NVL(f.dispseql, 999) sort2, f.partcd || f.slipcd slipcd"
                    sSql += "  FROM rj010m j, rm010m j1, rf060m f, lf021m f2"
                    sSql += " WHERE j1.regno    = :regno"
                    sSql += "   AND j1.tkdt    >= :dates"
                    sSql += "   AND j1.tkdt    <= :datee || '235959'"
                    sSql += "   AND j.bcno     = j1.bcno"
                    sSql += "   AND j1.tclscd  = f.testcd"
                    sSql += "   AND j1.spccd   = f.spccd"
                    sSql += "   AND j.bcprtdt >= f.usdt"
                    sSql += "   AND j.bcprtdt <  f.uedt"
                    sSql += "   AND f.partcd   = f2.partcd"
                    sSql += "   AND f.slipcd   = f2.slipcd"
                    sSql += "   AND j.bcprtdt >= f2.usdt"
                    sSql += "   AND j.bcprtdt <  f2.uedt"
                    sSql += "   AND NVL(j.spcflg, '0')  >= '4'"
                    sSql += "   AND f.partcd = :partcd"
                    sSql += "   AND f.slipcd = :slipcd"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))

                    '-- 과거결과
                    sSql += " UNION "
                    sSql += "SELECT DISTINCT"
                    sSql += "       f.testcd, f.tnmd, f2.dispseq sort1, NVL(f.dispseql, 999) sort2, f.tordslip slipcd"
                    sSql += "  FROM vw_ack_ocs_ord_info j, mdresult j1, rf060m f, rf021m f2"
                    sSql += " WHERE j.instcd        = '" + PRG_CONST.SITECD + "'"
                    sSql += "   AND j.prcpclscd    IN ('B2', 'B6')"
                    sSql += "   AND j.prcphistcd    = 'O'"
                    sSql += "   AND j1.patno         = :regno"
                    sSql += "   AND j1.rsltdate     >= :dates"
                    sSql += "   AND j1.rsltdate     <= :datee"
                    sSql += "   AND j.patno          = j1.patno"
                    sSql += "   AND j.orddate        = TO_CHAR(j1.orddate, 'YYYYMMDD')"
                    sSql += "   AND j.ordseqno       = j1.ordseqno"
                    sSql += "   AHD j.ioflag         = j1.ioflag"
                    sSql += "   AND j1.examcode      = f.tordcd"
                    sSql += "   AND j.spccd          = f.spccd"
                    sSql += "   AND f.usdt          <= fn_ack_sysdate"
                    sSql += "   AND f.uedt          >  fn_ack_sysdate"
                    sSql += "   AND f.partcd         = f2.partcd"
                    sSql += "   AND f.slipcd         = f2.slipcd"
                    sSql += "   AND f2.usdt         <= fn_ack_sysdate"
                    sSql += "   AND f2.uedt         >  fn_ack_sysdate"
                    sSql += "   AND f.partcd         = :partcd"
                    sSql += "   AND f.slipcd         = :slipcd"

                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                    alParm.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(0, 1)))
                    alParm.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd.Substring(1, 1)))
                End If


                sSql += " ORDER BY sort1, slipcd, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 누적결과 검사항목 가져오기
        Public Shared Function fnGet_history_test_rv(ByVal rsQryGbn As String, ByVal rsRegNo As String, _
                                                     ByVal rsSlipCd As String, ByVal rsTestCds As String, _
                                                     ByVal rsDayS As String, ByVal rsDayE As String, ByRef r_dt_Anti As DataTable) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_history_test_rv(String, String, String, String, DataTable) As DataTable"

            Try

                Dim sSql As String = ""
                Dim al As New ArrayList

                rsDayS = rsDayS.Replace("-", "")
                rsDayE = rsDayE.Replace("-", "")

                sSql = ""
                sSql += "SELECT DISTINCT f.antinmd,  m.anticd"
                sSql += "  FROM lm013m m,"
                sSql += "       (SELECT DISTINCT j.bcno"
                sSql += "          FROM (SELECT testcd, spccd"
                sSql += "                  FROM lf060m"
                sSql += "                 WHERE tcdgbn IN ('P', 'S', 'C')"
                sSql += "                   AND mbttype > '1'"
                If rsTestCds <> "" Then
                    If rsTestCds.IndexOf(",") >= 0 Then
                        sSql += "                   AND SUBSTR(testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                    Else
                        sSql += "                   AND (testcd = :testcd OR testcd IN (SELECT testcd FROM lf062m WHERE tclscd = :testcd))"
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                    End If '201
                ElseIf rsSlipCd <> "" Then
                    sSql += "                   AND " + IIf(rsQryGbn = "O", "tordslip", "partcd || slipcd").ToString + " = :slip"
                    al.Add(New OracleParameter("slip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If
                sSql += "               ) f, lj010m j, lm010m r"
                sSql += "         WHERE j.regno =  :regno"
                If rsQryGbn = "O" Then
                    sSql += "           AND j.orddt >= :dates"
                    sSql += "           AND j.orddt <= :datee || '235959'"
                Else
                    sSql += "           AND r.tkdt >= :dates"
                    sSql += "           AND r.tkdt <= :datee || '235959'"
                End If
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "       ) a, lf230m f"
                sSql += " WHERE m.bcno   = a.bcno"
                sSql += "   AND m.anticd = f.anticd"
                sSql += " GROUP BY m.anticd, f.antinmd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                DbCommand()
                r_dt_Anti = DbExecuteQuery(sSql, al)

                al.Clear()

                sSql = ""
                sSql += "SELECT b.testcd, b.spccd, b.tnmd, b.tnmp, b.rstunit unit,"
                sSql += "       fn_ack_get_test_reftxt(b.refgbn, b.sex, b.reflms, b.reflm, b.refhms, b.refhm, b.reflfs, b.reflf, b.refhfs, b.refhf, b.reflt) reftxt,"
                sSql += "       f3.spcnmd,"
                If rsQryGbn = "O" Then
                    sSql += "       f.tordslip slipcd, f.tordslipnm slipnm,"
                Else
                    sSql += "       f.partcd || f.slipcd slipcd, f.slipnmd slipnm,"
                End If
                sSql += "       NVL(f.dispseq, 999) sort1, NVL(b.dispseq, 999) sort2"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT t.* ,"
                sSql += "               f61.ageymd, f61.sage, f61.sages, f61.sagec, f61.eage, f61.eages, f61.eagec,"
                sSql += "               f61.reflm, f61.reflms, f61.refhm, f61.refhms, f61.reflf, f61.reflfs, f61.refhf, f61.refhfs, f61.reflt, f61.sex"
                sSql += "          FROM (SELECT r.testcd, r.spccd FROM lj010m j, lr010m r"
                sSql += "                 WHERE j.regno = :regno"

                If rsQryGbn = "O" Then
                    sSql += "                   AND j.orddt >= :dates"
                    sSql += "                   AND j.orddt <= :datee || '235959'"
                Else
                    sSql += "                   AND r.tkdt  >= :dates"
                    sSql += "                   AND r.tkdt  <= :datee || '235959'"
                End If
                sSql += "                   AND j.bcno = r.bcno"
                sSql += "                 UNION"
                sSql += "                SELECT r.testcd, r.spccd FROM lj010m j, lm010m r"
                sSql += "                 WHERE j.regno = :regno"

                If rsQryGbn = "O" Then
                    sSql += "                   AND j.orddt >= :dates"
                    sSql += "                   AND j.orddt <= :datee || '235959'"
                Else
                    sSql += "                   AND r.tkdt  >= :dates"
                    sSql += "                   AND r.tkdt  <= :datee || '235959'"
                End If
                sSql += "                   AND j.bcno = r.bcno"
                sSql += "               ) r,"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "               (SELECT testcd, spccd, CASE WHEN tcdgbn = 'C' THEN '...' || tnmd ELSE tnmd END tnmd, tnmp, dispseql dispseq, refgbn, rstunit,"
                sSql += "                       ctgbn, partcd, slipcd, tordslip, usdt, uedt"
                sSql += "                  FROM lf060m"
                sSql += "                 WHERE tcdgbn IN ('P', 'S', 'C')"
                sSql += "                   AND NVL(rptyn, '1') = '1'"

                If rsTestCds <> "" Then
                    If rsTestCds.IndexOf(",") >= 0 Then
                        sSql += "                   AND SUBSTR(testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                    Else
                        sSql += "                   AND (SUBSTR(testcd, 1, 5) = :testcd OR testcd IN (SELECT testcd FROM lf062m WHERE tclscd = :testcd))"
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                    End If
                ElseIf rsSlipCd <> "" Then
                    sSql += "                   AND " + IIf(rsQryGbn = "O", "tordslip", "partcd || slipcd").ToString + " = :slip"
                    al.Add(New OracleParameter("slip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If
                sSql += "               ) t LEFT OUTER JOIN"
                sSql += "               (SELECT j.sex, j.dage, f.*"
                sSql += "                  FROM (SELECT sex, MAX(dage) dage"
                sSql += "                          FROM lj010m"
                sSql += "                         WHERE regno = :regno"
                sSql += "                         GROUP BY regno, sex"
                sSql += "                       ) j,"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                sSql += "                       lf061m f"
                sSql += "                 WHERE ROUND(f.sagec * 365) + f.sages * 0.1 <= j.dage"
                sSql += "                   AND j.dage <= ROUND(f.eagec * 365) - f.eages * 0.1"
                sSql += "               ) f61 ON (t.testcd = f61.testcd AND t.spccd = f61.spccd AND t.usdt = f61.usdt)"
                sSql += "         WHERE r.testcd = t.testcd"
                sSql += "           AND r.spccd  = t.spccd"
                sSql += "           AND t.usdt  <= fn_ack_sysdate"
                sSql += "           AND t.uedt  >  fn_ack_sysdate"
                sSql += "       ) b, lf030m f3,"

                If rsQryGbn = "O" Then
                    sSql += "       lf100m f"
                    sSql += " WHERE b.tordslip = f.tordslip"
                Else
                    sSql += "       lf021m f"
                    sSql += " WHERE b.partcd = f.partcd"
                    sSql += "   AND b.slipcd = f.slipcd"
                End If

                sSql += "   AND f.usdt  <= fn_ack_sysdate"
                sSql += "   AND f.uedt  >  fn_ack_sysdate"
                sSql += "   AND b.spccd  = f3.spccd"
                sSql += "   AND f3.usdt <= fn_ack_sysdate"
                sSql += "   AND f3.uedt >  fn_ack_sysdate"

                '-- 핵의학
                sSql += " UNION "
                sSql += "SELECT b.testcd, b.spccd, b.tnmd, b.tnmp, b.rstunit unit,"
                sSql += "       fn_ack_get_test_reftxt(b.refgbn, b.sex, b.reflms, b.reflm, b.refhms, b.refhm, b.reflfs, b.reflf, b.refhfs, b.refhf, b.reflt) reftxt,"
                sSql += "       f3.spcnmd,"
                If rsQryGbn = "O" Then
                    sSql += "       f.tordslip slipcd, f.tordslipnm slipnm,"
                Else
                    sSql += "       f.partcd || f.slipcd slipcd, f.slipnmd slipnm,"
                End If
                sSql += "       NVL(f.dispseq, 999) sort1, NVL(b.dispseq, 999) sort2"
                sSql += "  FROM ("
                sSql += "        SELECT DISTINCT t.* ,"
                sSql += "               f61.ageymd, f61.sage, f61.sages, f61.sagec, f61.eage, f61.eages, f61.eagec,"
                sSql += "               f61.reflm, f61.reflms, f61.refhm, f61.refhms, f61.reflf, f61.reflfs, f61.refhf, f61.refhfs, f61.reflt, f61.sex"
                sSql += "          FROM (SELECT r.testcd, r.spccd FROM rj010m j, rr010m r"
                sSql += "                 WHERE j.regno = :regno"

                If rsQryGbn = "O" Then
                    sSql += "                   AND j.orddt >= :dates"
                    sSql += "                   AND j.orddt <= :datee || '23595'"
                Else
                    sSql += "                   AND r.tkdt  >= :dates"
                    sSql += "                   AND r.tkdt  <= :datee || '235959'"
                End If
                sSql += "                   AND j.bcno = r.bcno"
                sSql += "               ) r,"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "               (SELECT testcd, spccd, CASE WHEN tcdgbn = 'C' THEN '...' || tnmd ELSE tnmd END tnmd, tnmp, dispseql dispseq, refgbn, rstunit,"
                sSql += "                       ctgbn, partcd, slipcd, tordslip, usdt, uedt"
                sSql += "                  FROM rf060m"
                sSql += "                 WHERE tcdgbn IN ('P', 'S', 'C')"
                sSql += "                   AND NVL(rptyn, '1') = '1'"

                If rsTestCds <> "" Then
                    If rsTestCds.IndexOf(",") >= 0 Then
                        sSql += "                   AND SUBSTR(testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                    Else
                        sSql += "                   AND (SUBSTR(testcd, 1, 5) = :testcd OR testcd IN (SELECT testcd FROM lf062m WHERE tclscd = :testcd))"
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                    End If
                ElseIf rsSlipCd <> "" Then
                    sSql += "                   AND " + IIf(rsQryGbn = "O", "tordslip", "partcd || slipcd").ToString + " = :slip"
                    al.Add(New OracleParameter("slip", OracleDbType.Varchar2, rsSlipCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSlipCd))
                End If
                sSql += "               ) t LEFT OUTER JOIN"
                sSql += "               (SELECT j.sex, j.dage, f.*"
                sSql += "                  FROM (SELECT sex, MAX(dage) dage"
                sSql += "                          FROM rj010m"
                sSql += "                         WHERE regno = :regno"
                sSql += "                         GROUP BY regno, sex"
                sSql += "                       ) j,"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                sSql += "                       rf061m f"
                sSql += "                 WHERE ROUND(f.sagec * 365) + f.sages * 0.1 <= j.dage"
                sSql += "                   AND j.dage <= ROUND(f.eagec * 365) - f.eages * 0.1"
                sSql += "               ) f61 ON (t.testcd = f61.testcd AND t.spccd = f61.spccd AND t.usdt = f61.usdt)"
                sSql += "         WHERE r.testcd = t.testcd"
                sSql += "           AND r.spccd  = t.spccd"
                sSql += "           AND t.usdt  <= fn_ack_sysdate"
                sSql += "           AND t.uedt  >  fn_ack_sysdate"
                sSql += "       ) b, lf030m f3,"

                If rsQryGbn = "O" Then
                    sSql += "       lf100m f"
                    sSql += " WHERE b.tordslip = f.tordslip"
                Else
                    sSql += "       rf021m f"
                    sSql += " WHERE b.partcd = f.partcd"
                    sSql += "   AND b.slipcd = f.slipcd"
                End If

                sSql += "   AND f.usdt  <= fn_ack_sysdate"
                sSql += "   AND f.uedt  >  fn_ack_sysdate"
                sSql += "   AND b.spccd  = f3.spccd"
                sSql += "   AND f3.usdt <= fn_ack_sysdate"
                sSql += "   AND f3.uedt >  fn_ack_sysdate"


                sSql += " ORDER BY sort1, slipcd, spccd, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 누적결과 검사항목 가져오기(처방슬립별)
        Public Shared Function fnGet_history_test_rv_tordslip(ByVal rsRegNo As String, ByVal rsTOrdSlip As String, ByVal rsTestCds As String, _
                                                     ByVal rsDayS As String, ByVal rsDayE As String, ByRef r_dt_Anti As DataTable) As DataTable
            Dim sFn As String = "fnGet_history_test_rv_tordslip(String...., DataTable) As DataTable"

            Try

                Dim sSql As String = ""
                Dim al As New ArrayList

                rsDayS = rsDayS.Replace("-", "")
                rsDayE = rsDayE.Replace("-", "")

                sSql = ""
                sSql += "SELECT DISTINCT f.antinmd,  m.anticd"
                sSql += "  FROM lm013m m,"
                sSql += "       (SELECT DISTINCT j.bcno"
                sSql += "          FROM (SELECT testcd, spccd"
                sSql += "                  FROM lf060m"
                sSql += "                 WHERE tcdgbn IN ('P', 'S', 'C')"
                sSql += "                   AND mbttype > '1'"

                If rsTestCds <> "" Then
                    If rsTestCds.IndexOf(",") >= 0 Then
                        sSql += "                   AND SUBSTR(testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                    Else
                        sSql += "                   AND (testcd = :testcd OR testcd IN (SELECT testcd FROM lf062m WHERE tclscd = :testcd))"
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                    End If '201
                ElseIf rsTOrdSlip <> "" Then
                    sSql += "                   AND tordslip = :ordslip"
                    al.Add(New OracleParameter("ordslip", OracleDbType.Varchar2, rsTOrdSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdSlip))
                End If
                sSql += "               ) f, lj010m j, lm010m r"
                sSql += "         WHERE j.regno =  :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '235959'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "       ) a, lf230m f"
                sSql += " WHERE m.bcno   = a.bcno"
                sSql += "   AND m.anticd = f.anticd"
                sSql += " GROUP BY m.anticd, f.antinmd"
                sSql += " ORDER BY f.antinmd" '<<<20150805 미생물 누적결과 항생제명 표시 알파벳순으로 표시요청 

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                DbCommand()
                r_dt_Anti = DbExecuteQuery(sSql, al)

                al.Clear()

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       f6.testcd, f6.spccd, f6.tnmd, f6.tnmp, f6.rstunit unit,"
                sSql += "       f3.spcnmd, f6.tordslip slipcd, f10.tordslipnm slipnm,"
                sSql += "       fn_ack_get_test_reftxt(f6.refgbn, b.sex, b.reflms, b.reflm, b.refhms, b.refhm, b.reflfs, b.reflf, b.refhfs, b.refhf, b.reflt) reftxt,"
                sSql += "       NVL(f10.dispseq, 999) sort1, NVL(f6.dispseql, 999) sort2"
                sSql += "  FROM ("
                sSql += "        SELECT r.regno, r.testcd, r.spccd, MAX(r.tkdt) tkdt FROM lj010m j, lr010m r"
                sSql += "         WHERE j.regno  = :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '235959'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "         GROUP BY r.regno, r.testcd, r.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "         UNION"
                sSql += "        SELECT r.regno, r.testcd, r.spccd, MAX(r.tkdt) tkdt FROM lj010m j, lm010m r"
                sSql += "         WHERE j.regno  = :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '235959'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "         GROUP BY r.regno, r.testcd, r.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                '--과거결과
                sSql += "         UNION "
                sSql += "        SELECT r.patno regno, f6.testcd, f6.spccd, fn_ack_sysdate tkdt"
                sSql += "          FROM vw_ack_ocs_ord_info j, mdresult r, lf060m f6, lf030m f3"
                sSql += "         WHERE j.patno       = :regno"
                sSql += "           AND j.orddate    >= :dates"
                sSql += "           AND j.orddate    <= :datee"
                sSql += "           AND j.instcd      = '" + PRG_CONST.SITECD + "'"
                sSql += "           AND j.prcpclscd  IN ('B2', 'B6')"
                sSql += "           AND j.prcphistcd  = 'O'"
                sSql += "           AND j.patno       = r.patno"
                sSql += "           AND j.orddate     = TO_CHAR(r.orddate, 'YYYYMMDD')"
                sSql += "           AND j.ordseqno    = r.ordseqno"
                sSql += "           AND j.ioflag      = r.ioflag"
                sSql += "           AND r.examcode    = f6.tordcd"
                sSql += "           AND j.spccd       = f6.spccd"
                sSql += "           AND f6.usdt      <= fn_ack_sysdate"
                sSql += "           AND f6.uedt      >  fn_ack_sysdate"
                sSql += "           AND f6.spccd      = f3.spccd"
                sSql += "           AND f3.usdt      <= fn_ack_sysdate"
                sSql += "           AND f3.uedt      >  fn_ack_sysdate"
                sSql += "         GROUP BY r.patno, f6.testcd, f6.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                '-- 과거결과

                sSql += "       ) r,"
                sSql += "       lf030m f3, lf100m f10, lf060m f6,"
                sSql += "       (SELECT j.regno, j.sex, j.dage, f.*"
                sSql += "          FROM (SELECT regno, sex, dage FROM lj010m j,"
                sSql += "                       (SELECT MAX(bcprtdt) bcprtdt FROM lj010m"
                sSql += "                         WHERE regno  = :regno"
                sSql += "                           AND orddt >= :dates"
                sSql += "                           AND orddt <= :datee || '235959'"
                sSql += "                       ) jj"
                sSql += "                 WHERE j.regno   = :regno"
                sSql += "                   AND j.orddt  >= :dates"
                sSql += "                   AND j.orddt  <= :datee || '235959'"
                sSql += "                   AND j.bcprtdt = jj.bcprtdt"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "                 UNION "
                sSql += "                SELECT j.patno regno, p.sex, MAX(TRUNC(TO_DATE(j.orddate, 'YYYYMMDD') - TO_DATE(p.birtdate, 'YYYYMMDD'))) dage"
                sSql += "                  FROM vw_ack_ocs_ord_info j, vw_ack_ocs_pat_info p"
                sSql += "                 WHERE j.instcd       = '" + PRG_CONST.SITECD + "'"
                sSql += "                   AND j.prcpclscd   IN ('B2', 'B6')"
                sSql += "                   AND j.prcphistcd   = 'O'"
                sSql += "                   AND j.patno        = :regno"
                sSql += "                   AND j.orddate     >= :dates"
                sSql += "                   AND j.orddate     <= :datee"
                sSql += "                   AND j.patno        = p.patno"
                sSql += "                   AND p.instcd       = '" + PRG_CONST.SITECD + "'"
                sSql += "                 GROUP BY j.patno, p.sex"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "               ) j, lf061m f"
                sSql += "         WHERE ROUND(f.sagec * 365) + f.sages * 0.1 <= j.dage"
                sSql += "           AND j.dage <= ROUND(f.eagec * 365) - f.eages * 0.1"
                sSql += "       ) b"
                sSql += " WHERE f6.testcd   = r.testcd"
                sSql += "   AND f6.spccd    = r.spccd"
                sSql += "   AND f6.usdt    <= r.tkdt"
                sSql += "   AND f6.uedt    >  r.tkdt"
                sSql += "   AND f3.spccd    = r.spccd"
                sSql += "   AND f3.usdt    <= r.tkdt"
                sSql += "   AND f3.uedt    >  r.tkdt"
                sSql += "   AND f6.tordslip = f10.tordslip"
                sSql += "   AND f10.usdt   <= r.tkdt"
                sSql += "   AND f10.uedt   >  r.tkdt"
                sSql += "   AND NOT (f6.tcdgbn = 'C' AND NVL(f6.mbttype, '0') IN ('1', '2'))"
                sSql += "   AND f6.testcd   = b.testcd (+)"
                sSql += "   AND f6.spccd    = b.spccd (+)"
                sSql += "   AND f6.usdt     = b.usdt (+)"

                If rsTOrdSlip <> "" Then
                    sSql += "    AND f10.tordslip = :tordslip"
                    al.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsTOrdSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdSlip))
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND SUBSTR(r.testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                End If

                sSql += " UNION "
                sSql += "SELECT DISTINCT"
                sSql += "       f6.testcd, f6.spccd, f6.tnmd, f6.tnmp, f6.rstunit unit,"
                sSql += "       f3.spcnmd, f6.tordslip slipcd, f10.tordslipnm slipnm,"
                sSql += "       fn_ack_get_test_reftxt(f6.refgbn, b.sex, b.reflms, b.reflm, b.refhms, b.refhm, b.reflfs, b.reflf, b.refhfs, b.refhf, b.reflt) reftxt,"
                sSql += "       NVL(f10.dispseq, 999) sort1, NVL(f6.dispseql, 999) sort2"
                sSql += "  FROM ("
                sSql += "        SELECT r.regno, r.testcd, r.spccd, MAX(r.tkdt) tkdt"
                sSql += "          FROM rj010m j, rr010m r"
                sSql += "         WHERE j.regno  = :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '23595'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "         GROUP BY r.regno, r.testcd, r.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                '--과거결과
                sSql += "         UNION "
                sSql += "        SELECT r.patno regno, f6.testcd, f6.spccd, fn_ack_sysdate tkdt"
                sSql += "          FROM vw_ack_ocs_ord_info j, mdresult r, rf060m f6, lf030m f3"
                sSql += "         WHERE j.patno       = :regno"
                sSql += "           AND j.orddate    >= :dates"
                sSql += "           AND j.orddate    <= :datee"
                sSql += "           AND j.patno       = r.patno"
                sSql += "           AND j.orddate     = TO_CHAR(r.orddate, 'YYYYMMDD')"
                sSql += "           AND j.ordseqno    = r.ordseqno"
                sSql += "           AND j.ioflag      = r.ioflag"
                sSql += "           AND j.instcd      = '" + PRG_CONST.SITECD + "'"
                sSql += "           AND j.prcpclscd  IN ('B2', 'B6')"
                sSql += "           AND j.prcphistcd  = 'O'"
                sSql += "           AND r.examcode    = f6.tordcd"
                sSql += "           AND j.spccd       = f6.spccd"
                sSql += "           AND f6.usdt      <= fn_ack_sysdate"
                sSql += "           AND f6.uedt      >  fn_ack_sysdate"
                sSql += "           AND f6.spccd      = f3.spccd"
                sSql += "           AND f3.usdt      <= fn_ack_sysdate"
                sSql += "           AND f3.uedt      >  fn_ack_sysdate"
                sSql += "         GROUP BY r.patno, f6.testcd, f6.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                '-- 과거결과

                sSql += "       ) r,"
                sSql += "       lf030m f3, lf100m f10, rf060m f6,"
                'sSql += "       LEFT OUTER JOIN"
                sSql += "       (SELECT j.regno, j.sex, j.dage, f.*"
                sSql += "          FROM (SELECT regno, sex, dage FROM lj010m j,"
                sSql += "                       (SELECT MAX(bcprtdt) bcprtdt FROM rj010m"
                sSql += "                         WHERE regno  = :regno"
                sSql += "                           AND orddt >= :dates"
                sSql += "                           AND orddt <= :datee || '235959'"
                sSql += "                       ) jj"
                sSql += "                 WHERE j.regno   = :regno"
                sSql += "                   AND j.orddt  >= :dates"
                sSql += "                   AND j.orddt  <= :datee || '235959'"
                sSql += "                   AND j.bcprtdt = jj.bcprtdt"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "                 UNION "
                sSql += "                SELECT j.patno regno, p.sex, MAX(TRUNC(TO_DATE(j.orddate, 'YYYYMMDD') - TO_DATE(p.birtdate, 'YYYYMMDD'))) dage"
                sSql += "                  FROM vw_ack_ocs_ord_info j, vw_ack_ocs_pat_info p"
                sSql += "                 WHERE j.patno      = :regno"
                sSql += "                   AND j.orddate   >= :dates"
                sSql += "                   AND j.orddate   <= :datee"
                sSql += "                   AND j.instcd     = '" + PRG_CONST.SITECD + "'"
                sSql += "                   AND j.prcpclscd IN ('B2', 'B6')"
                sSql += "                   AND j.prcphistcd = 'O'"
                sSql += "                   AND p.instcd     = '" + PRG_CONST.SITECD + "'"
                sSql += "                   AND j.patno      = p.patno"
                sSql += "                 GROUP BY j.patno, p.sex"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "               ) j, rf061m f"
                sSql += "         WHERE ROUND(f.sagec * 365) + f.sages * 0.1 <= j.dage"
                sSql += "           AND j.dage <= ROUND(f.eagec * 365) - f.eages * 0.1"
                'sSql += "       ) b ON (f6.testcd = b.testcd AND f6.spccd = b.spccd AND f6.usdt = b.usdt)"
                sSql += "       ) b"
                sSql += " WHERE f6.testcd   = r.testcd"
                sSql += "   AND f6.spccd    = r.spccd"
                sSql += "   AND f6.usdt    <= r.tkdt"
                sSql += "   AND f6.uedt    >  r.tkdt"
                sSql += "   AND f3.spccd    = r.spccd"
                sSql += "   AND f3.usdt    <= r.tkdt"
                sSql += "   AND f3.uedt    >  r.tkdt"
                sSql += "   AND f6.tordslip = f10.tordslip"
                sSql += "   AND f10.usdt   <= r.tkdt"
                sSql += "   AND f10.uedt   >  r.tkdt"
                sSql += "   AND f6.testcd   = b.testcd (+)"
                sSql += "   AND f6.spccd    = b.spccd (+)"
                sSql += "   AND f6.usdt     = b.usdt (+)"

                If rsTOrdSlip <> "" Then
                    sSql += "    AND f10.tordslip = :tordslip"
                    al.Add(New OracleParameter("tordslip", OracleDbType.Varchar2, rsTOrdSlip.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdSlip))
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND SUBSTR(r.testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                End If
                sSql += " ORDER BY sort1, slipcd, spccd, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 누적결과 검사항목 가져오기(부서/분야)
        Public Shared Function fnGet_history_test_rv_partslip(ByVal rsRegNo As String, ByVal rsTOrdSlip As String, ByVal rsTestCds As String, _
                                                       ByVal rsDayS As String, ByVal rsDayE As String, ByRef r_dt_Anti As DataTable) As DataTable
            Dim sFn As String = "fnGet_history_test_rv_partslip(String...., DataTable) As DataTable"

            Try

                Dim sSql As String = ""
                Dim al As New ArrayList

                rsDayS = rsDayS.Replace("-", "")
                rsDayE = rsDayE.Replace("-", "")

                If rsTOrdSlip = "M2" Or rsTOrdSlip = "L42" Then
                    If rsDayS <> "" Then rsDayS = Format(DateAdd(DateInterval.Year, -3, Now), "yyyy-MM-dd").ToString
                    '20191230 NBM 핵의학과에서 누적결과 조회 
                    'Else
                    '    If rsDateS <> "" Then rsDateS = Format(DateAdd(DateInterval.Year, -1, Now), "yyyy-MM-dd").ToString '"1990-01-01" '<<<20180529 누적결과 조회 최대 1년으로 축소
                End If


                sSql = ""
                sSql += "SELECT DISTINCT f.antinmd,  m.anticd"
                sSql += "  FROM lm013m m,"
                sSql += "       (SELECT DISTINCT j.bcno"
                sSql += "          FROM (SELECT testcd, spccd"
                sSql += "                  FROM lf060m"
                sSql += "                 WHERE tcdgbn IN ('P', 'S', 'C')"
                sSql += "                   AND mbttype > '1'"

                If rsTestCds <> "" Then
                    If rsTestCds.IndexOf(",") >= 0 Then
                        sSql += "                   AND SUBSTR(testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                    Else
                        sSql += "                   AND (testcd = :testcd OR testcd IN (SELECT testcd FROM lf062m WHERE tclscd = :testcd))"
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                        al.Add(New OracleParameter("testcd", OracleDbType.Varchar2, rsTestCds.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTestCds))
                    End If '201
                ElseIf rsTOrdSlip <> "" Then
                    sSql += "                   AND partcd = :partcd"
                    sSql += "                   AND slipcd = :slipcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdSlip.Substring(1, 1)))
                End If
                sSql += "               ) f, lj010m j, lm010m r"
                sSql += "         WHERE j.regno =  :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '234949'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "           AND r.testcd = f.testcd"
                sSql += "           AND r.spccd  = f.spccd"
                sSql += "       ) a, lf230m f"
                sSql += " WHERE m.bcno   = a.bcno"
                sSql += "   AND m.anticd = f.anticd"
                sSql += " GROUP BY m.anticd, f.antinmd"
                sSql += " ORDER BY f.antinmd"    '<<<20150806 누적결과 팝업 에서 항생제명 알파벳순으로 표시요청 

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                DbCommand()
                r_dt_Anti = DbExecuteQuery(sSql, al)

                al.Clear()

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       f6.testcd, f6.spccd, f6.tnmd, f6.tnmp, f6.rstunit unit,"
                sSql += "       f3.spcnmd, f6.partcd || f6.slipcd slipcd, f21.slipnmd slipnm,"
                sSql += "       fn_ack_get_test_reftxt(f6.refgbn, b.sex, b.reflms, b.reflm, b.refhms, b.refhm, b.reflfs, b.reflf, b.refhfs, b.refhf, b.reflt) reftxt,"
                sSql += "       NVL(f21.dispseq, 999) sort1, NVL(f6.dispseql, 999) sort2"
                sSql += "  FROM ("
                sSql += "        SELECT r.regno, r.testcd, r.spccd, MAX(r.tkdt) tkdt FROM lj010m j, lr010m r"
                sSql += "         WHERE j.regno  = :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '235959'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "         GROUP BY r.regno, r.testcd, r.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "         UNION"
                sSql += "        SELECT r.regno, r.testcd, r.spccd, MAX(r.tkdt) tkdt FROM lj010m j, lm010m r"
                sSql += "         WHERE j.regno  = :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '235959'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "         GROUP BY r.regno, r.testcd, r.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "         UNION "
                sSql += "        SELECT r.regno, r.testcd, r.spccd, MAX(r.tkdt) tkdt FROM rj010m j, rr010m r"
                sSql += "         WHERE j.regno  = :regno"
                sSql += "           AND j.orddt >= :dates"
                sSql += "           AND j.orddt <= :datee || '235959'"
                sSql += "           AND j.bcno   = r.bcno"
                sSql += "         GROUP BY r.regno, r.testcd, r.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                '--과거결과
                sSql += "         UNION "
                sSql += "        SELECT r.patno regno, f6.testcd, f6.spccd, fn_ack_sysdate tkdt"
                sSql += "          FROM vw_ack_ocs_ord_info j, mdresult r, lf060m f6, lf030m f3"
                sSql += "         WHERE j.instcd      = '" + PRG_CONST.SITECD + "'"
                sSql += "           AND j.prcpclscd  IN ('B2', 'B6')"
                sSql += "           AND j.prcphistcd  = 'O'"
                sSql += "           AND j.patno       = :regno"
                sSql += "           AND j.execdate   >= :dates"
                sSql += "           AND j.execdate   <= :datee"
                sSql += "           AND j.patno       = r.patno"
                sSql += "           AND j.orddate     = TO_CHAR(r.orddate, 'YYYYMMDD')"
                sSql += "           AND j.ordseqno    = r.ordseqno"
                sSql += "           AND j.ioflag      = r.ioflag"
                sSql += "           AND r.examcode    = f6.tordcd"
                sSql += "           AND j.spccd       = f6.spccd"
                sSql += "           AND f6.usdt      <= fn_ack_sysdate"
                sSql += "           AND f6.uedt      >  fn_ack_sysdate"
                sSql += "           AND f6.spccd      = f3.spccd"
                sSql += "           AND f3.usdt      <= fn_ack_sysdate"
                sSql += "           AND f3.uedt      >  fn_ack_sysdate"
                sSql += "         GROUP BY r.patno, f6.testcd, f6.spccd"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))
                '-- 과거결과

                sSql += "       ) r,"
                sSql += "       lf030m f3, vw_ack_tot_partslip_info f21, vw_ack_tot_test_info f6,"
                sSql += "       (SELECT j.regno, j.sex, j.dage, f.*"
                sSql += "          FROM (SELECT regno, sex, dage FROM lj010m j,"
                sSql += "                       (SELECT MAX(bcprtdt) bcprtdt FROM lj010m"
                sSql += "                         WHERE regno  = :regno"
                sSql += "                           AND orddt >= :dates"
                sSql += "                           AND orddt <= :datee || '235959'"
                sSql += "                       ) jj"
                sSql += "                 WHERE j.regno   = :regno"
                sSql += "                   AND j.orddt  >= :dates"
                sSql += "                   AND j.orddt  <= :datee || '235959'"
                sSql += "                   AND j.bcprtdt = jj.bcprtdt"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))

                sSql += "                 UNION "
                sSql += "                SELECT j.patno regno, p.sex, MAX(TRUNC(TO_DATE(j.orddate, 'YYYYMMDD') - TO_DATE(p.birtdate, 'YYYYMMDD'))) dage"
                sSql += "                  FROM vw_ack_ocs_ord_info j, vw_ack_ocs_pat_info p"
                sSql += "                 WHERE j.patno    = :regno"
                sSql += "                   AND j.orddate >= :dates"
                sSql += "                   AND j.orddate <= :datee"
                sSql += "                   AND j.patno    = p.patno"
                sSql += "                   AND p.instcd   = '" + PRG_CONST.SITECD + "'"
                sSql += "                 GROUP BY j.patno, p.sex"

                al.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                al.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDayS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayS))
                al.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDayE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDayE))


                sSql += "               ) j, lf061m f"
                sSql += "         WHERE ROUND(f.sagec * 365) + f.sages * 0.1 <= j.dage"
                sSql += "           AND j.dage <= ROUND(f.eagec * 365) - f.eages * 0.1"
                sSql += "       ) b"
                sSql += " WHERE f6.testcd   = r.testcd"
                sSql += "   AND f6.spccd    = r.spccd"
                sSql += "   AND f6.usdt    <= r.tkdt"
                sSql += "   AND f6.uedt    >  r.tkdt"
                sSql += "   AND f3.spccd    = r.spccd"
                sSql += "   AND f3.usdt    <= r.tkdt"
                sSql += "   AND f3.uedt    >  r.tkdt"
                sSql += "   AND f6.partcd = f21.partcd"
                sSql += "   AND f6.slipcd = f21.slipcd"
                sSql += "   AND f21.usdt   <= r.tkdt"
                sSql += "   AND f21.uedt   >  r.tkdt"
                sSql += "   AND NOT (f6.tcdgbn = 'C' AND NVL(f6.mbttype, '0') IN ('1', '2'))"
                sSql += "   AND f6.testcd   = b.testcd (+)"
                sSql += "   AND f6.spccd    = b.spccd (+)"
                sSql += "   AND f6.usdt     = b.usdt (+)"

                If rsTOrdSlip <> "" Then
                    sSql += "    AND f6.partcd = :partcd"
                    sSql += "    AND f6.slipcd = :slipcd"
                    al.Add(New OracleParameter("partcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdSlip.Substring(0, 1)))
                    al.Add(New OracleParameter("slipcd", OracleDbType.Varchar2, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTOrdSlip.Substring(1, 1)))
                End If

                If rsTestCds <> "" Then
                    sSql += "   AND SUBSTR(r.testcd, 1, 5) IN ('" + rsTestCds.Replace(",", "','") + "')"
                End If

                sSql += " ORDER BY sort1, slipcd, spccd, sort2, testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        ' 누적결과 가져오기(특수보고서포함)
        Public Shared Function fnGet_history_rst_rv(ByVal rsQryGbn As String, ByVal rsRegNo As String, ByVal rsTestCds As String, _
                                                    ByVal rsDateS As String, ByVal rsDateE As String, ByRef r_dt_Micro As DataTable, Optional ByVal rsSlipcd As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fnGet_history_rst_rv(String, String, String, String, String, DataTable) As DataTable"
            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList
                ' If rsDateS <> "" Then rsDateS = Format(DateAdd(DateInterval.Year, -1, Now), "yyyy-MM-dd").ToString '"1990-01-01" '<<<20180529 누적결과 조회 최대 1년으로 축소

                '20191010 nbm
                If rsSlipcd = "M2" Or rsSlipcd = "L42" Then
                    If rsDateS <> "" Then rsDateS = Format(DateAdd(DateInterval.Year, -3, Now), "yyyy-MM-dd").ToString
                    '20191230 NBM 핵의학과에서 누적결과 조회 
                    'Else
                    '    If rsDateS <> "" Then rsDateS = Format(DateAdd(DateInterval.Year, -1, Now), "yyyy-MM-dd").ToString '"1990-01-01" '<<<20180529 누적결과 조회 최대 1년으로 축소
                End If


                If rsDateE <> "" Then rsDateE = Format(Now, "yyyy-MM-dd").ToString

                sSql = ""
                sSql += "SELECT r.bcno, r.testcd, rb.ranking, ra.baccd, ra.anticd, ra.antirst || '/' || ra.decrst antirst,"
                sSql += "       SUBSTR(r.tkdt, 1, 12) tkdt, f6.tnmd, f21.bacnmd , r.rerunflg "
                sSql += "  FROM lj010m j, lm010m r, lf060m f6,"
                sSql += "       lm012m rb, lm013m ra, lf210m f21"
                sSql += " WHERE j.regno   = :regno"
                sSql += "   AND j.spcflg  = '4'"
                If rsDateS = "1990-01-01" Then
                    sSql += "   AND r.regno = j.regno "
                Else
                    If rsQryGbn = "O" Then
                        sSql += "   AND j.orddt >= :dates || '000000'"
                        sSql += "   AND j.orddt <= :datee || '235959'"
                    Else
                        sSql += "   AND r.tkdt  >= :dates || '000000'"
                        sSql += "   AND r.tkdt  <= :datee || '235959'"
                    End If
                End If
                sSql += "   AND j.bcno      = r.bcno"
                sSql += "   AND r.testcd    = f6.testcd"
                sSql += "   AND r.spccd     = f6.spccd"
                sSql += "   AND f6.mbttype  = '2'"
                sSql += "   AND f6.usdt    <= r.tkdt"
                sSql += "   AND f6.uedt    >  r.tkdt"
                sSql += "   AND r.bcno      = rb.bcno"
                sSql += "   AND r.testcd    = rb.testcd"
                sSql += "   AND rb.bcno     = ra.bcno"
                sSql += "   AND rb.testcd   = ra.testcd"
                sSql += "   AND rb.baccd    = ra.baccd"
                sSql += "   AND rb.bacseq   = ra.bacseq"
                sSql += "   AND rb.baccd    = f21.baccd"
                sSql += "   AND f21.usdt   <= r.tkdt"
                sSql += "   AND f21.uedt   >  r.tkdt"

                If rsTestCds <> "" Then
                    sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','") + "')"
                End If

                alParm.Clear()
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                If rsDateS <> "1990-01-01" Then
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE.Replace("-", "")))
                End If

                ' sSql += " ORDER BY tkdt, ranking, baccd DESC"
                sSql += " ORDER BY bcno desc ,tkdt, ranking, baccd DESC"
                DbCommand()
                r_dt_Micro = DbExecuteQuery(sSql, alParm)

                alParm.Clear()

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       r.bcno, fn_ack_date_str(MIN(r.tkdt),'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, r.spccd,"
                sSql += "       r.rstflg, r.viewrst, r.hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, fn_ack_get_usr_name(r.fnid) fnnm,"
                sSql += "       CASE WHEN r.rstflg IN ('2', '3') AND r.orgrst = '{null}' THEN 'S' ELSE ' ' END srpt, f.titleyn , r.rerunflg"
                sSql += "  FROM lj010m j, lr010m r, lf060m f"
                sSql += " WHERE j.regno  = :regno"
                sSql += "   AND j.spcflg = '4'"
                If rsDateS = "1990-01-01" Then
                    sSql += "   AND r.regno = j.regno "
                Else
                    If rsQryGbn = "O" Then
                        sSql += "   AND j.orddt >= :dates || '000000'"
                        sSql += "   AND j.orddt <= :datee || '235959'"
                    Else
                        sSql += "   AND r.tkdt  >= :dates || '000000'"
                        sSql += "   AND r.tkdt  <= :datee || '235959'"
                    End If
                End If
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','").ToString + "')"
                sSql += "   AND (f.tcdgbn <> 'C' OR NVL(r.viewrst, '[null]') <> '[null]')"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                If rsDateS <> "1990-01-01" Then
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE.Replace("-", "")))
                End If
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd "
                sSql += "   AND f.usdt  <= r.tkdt"
                sSql += "   AND f.uedt  >= r.tkdt"
                sSql += "   AND NVL(f.rptyn, '1') = '1'"
                sSql += " GROUP BY r.bcno, r.testcd, r.spccd, r.viewrst, r.hlmark, r.panicmark, r.deltamark, r.criticalmark,"
                sSql += "          r.alertmark, r.fndt, r.orgrst, r.rstflg, r.fnid, f.titleyn , r.rerunflg"
                sSql += " UNION "
                sSql += "SELECT DISTINCT"
                sSql += "       r.bcno, fn_ack_date_str(MIN(r.tkdt),'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, r.spccd,"
                sSql += "       r.rstflg, fn_ack_get_ocs_rst_micro_v(r.bcno, r.testcd, f.tcdgbn, f.mbttype, r.viewrst) viewrst,"
                sSql += "       CASE WHEN f.mbttype = '2' THEN '' ELSE r.hlmark END hlmark,"
                sSql += "       r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, fn_ack_get_usr_name(r.fnid) fnnm,"
                sSql += "       CASE WHEN r.rstflg IN ('2', '3') AND r.orgrst = '{null}' THEN 'S' ELSE ' ' END srpt, f.titleyn , r.rerunflg"
                sSql += "  FROM lj010m j, lm010m r, lf060m f"
                sSql += " WHERE j.regno  = :regno"
                sSql += "   AND j.spcflg = '4'"
                If rsDateS = "1990-01-01" Then
                    sSql += "   AND r.regno = j.regno "
                Else
                    If rsQryGbn = "O" Then
                        sSql += "   AND j.orddt >= :dates || '000000'"
                        sSql += "   AND j.orddt <= :datee || '235959'"
                    Else
                        sSql += "   AND r.tkdt  >= :dates || '000000'"
                        sSql += "   AND r.tkdt  <= :datee || '235959'"
                    End If
                End If
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','").ToString + "')"
                sSql += "   AND (f.tcdgbn <> 'C' OR NVL(r.viewrst, '[null]') <> '[null]')"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                If rsDateS <> "1990-01-01" Then
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE.Replace("-", "")))
                End If
                sSql += "   AND r.testcd  = f.testcd"
                sSql += "   AND r.spccd   = f.spccd "
                sSql += "   AND f.usdt   <= r.tkdt"
                sSql += "   AND f.uedt   >= r.tkdt"
                sSql += "   AND NVL(f.rptyn, '1') = '1'"
                sSql += " GROUP BY r.bcno, r.testcd, r.spccd, r.viewrst, r.hlmark, r.panicmark, r.deltamark, r.criticalmark,"
                sSql += "          r.alertmark, r.fndt, r.orgrst, r.rstflg, r.fnid, f.titleyn, f.tcdgbn, f.mbttype , r.rerunflg"

                '-- 핵의학
                sSql += " UNION "
                sSql += "SELECT DISTINCT"
                sSql += "       r.bcno, fn_ack_date_str(MIN(r.tkdt),'yyyy-mm-dd hh24:mi:ss') tkdt, r.testcd, r.spccd,"
                sSql += "       r.rstflg, r.viewrst, r.hlmark, r.panicmark, r.deltamark, r.criticalmark, r.alertmark,"
                sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt, fn_ack_get_usr_name(r.fnid) fnnm,"
                sSql += "       CASE WHEN r.rstflg IN ('2', '3') AND r.orgrst = '{null}' THEN 'S' ELSE ' ' END srpt, f.titleyn , r.rerunflg"
                sSql += "  FROM rj010m j, rr010m r, rf060m f"
                sSql += " WHERE j.regno  = :regno"
                sSql += "   AND j.spcflg = '4'"
                If rsDateS = "1990-01-01" Then
                    sSql += "   AND r.regno = j.regno "
                Else
                    If rsQryGbn = "O" Then
                        sSql += "   AND j.orddt >= :dates || '000000'"
                        sSql += "   AND j.orddt <= :datee || '235959'"
                    Else
                        sSql += "   AND r.tkdt  >= :dates || '000000'"
                        sSql += "   AND r.tkdt  <= :datee || '235959'"
                    End If
                End If
                sSql += "   AND j.bcno    = r.bcno"
                sSql += "   AND r.testcd IN ('" + rsTestCds.Replace(",", "','").ToString + "')"
                sSql += "   AND (f.tcdgbn <> 'C' OR NVL(r.viewrst, '[null]') <> '[null]')"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                If rsDateS <> "1990-01-01" Then
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS.Replace("-", "")))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE.Replace("-", "")))
                End If
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccd "
                sSql += "   AND f.usdt  <= r.tkdt"
                sSql += "   AND f.uedt  >= r.tkdt"
                sSql += "   AND NVL(f.rptyn, '1') = '1'"
                sSql += " GROUP BY r.bcno, r.testcd, r.spccd, r.viewrst, r.hlmark, r.panicmark, r.deltamark, r.criticalmark,"
                sSql += "          r.alertmark, r.fndt, r.orgrst, r.rstflg, r.fnid, f.titleyn , r.rerunflg"

                '-- 과거결과
                sSql += " UNION "
                sSql += "SELECT r.spcno bcno, TO_CHAR(MIN(r.execdate),'yyyy-mm-dd hh24:mi') tkdt, f.testcd, f.spccd,"
                sSql += "       '3' rstflg, CASE WHEN NVL(r.rslt1, ' ') = ' ' THEN TO_CHAR(r.rslt2) ELSE r.rslt1 END viewrst,"
                sSql += "       '' hlmark, '' panicmark, '' deltamark, '' criticalmark, '' alertmark,"
                sSql += "       TO_CHAR(r.rsltdate, 'yyyy-mm-dd hh24:mi') fndt,"
                sSql += "       (SELECT usrnm FROM vw_ack_ocs_user_info WHERE usrid = r.reptdr AND ROWNUM = 1) fnnm,"
                sSql += "       '' srpt, f.titleyn , '' rerunflg"
                sSql += "  FROM vw_ack_ocs_ord_info j, mdresult r, lf060m f"
                sSql += " WHERE j.instcd        = '" + PRG_CONST.SITECD + "'"
                sSql += "   AND j.prcpclscd    IN ('B2', 'B6')"
                sSql += "   AND j.prcphistcd    = 'O'"
                sSql += "   AND j.patno         = :regno"
                If rsQryGbn = "O" Then
                    sSql += "   AND j.orddate  >= :dates"
                    sSql += "   AND j.orddate  <= :datee"
                Else
                    sSql += "   AND j.execdate >= :dates"
                    sSql += "   AND j.execdate <= :datee"
                End If
                sSql += "   AND j.patno    = r.patno"
                sSql += "   AND j.orddate  = TO_CHAR(r.orddate, 'YYYYMMDD')"
                sSql += "   AND j.ordseqno = r.ordseqno"
                sSql += "   AND j.ioflag   = r.ioflag"
                sSql += "   AND f.testcd IN ('" + rsTestCds.Replace(",", "','").ToString + "')"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS.Replace("-", "")))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Replace("-", "").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE.Replace("-", "")))

                sSql += "   AND r.examcode = f.tordcd"
                sSql += "   AND j.spccd    = f.spccd"
                sSql += "   AND f.usdt    <= fn_ack_sysdate"
                sSql += "   AND f.uedt    >= fn_ack_sysdate"
                sSql += "   AND NVL(f.rptyn, '1') = '1'"
                sSql += "   AND LENGTH(r.spcno) = 10"
                sSql += " GROUP BY r.spcno, f.testcd, f.spccd, CASE WHEN NVL(r.rslt1, ' ') = ' ' THEN TO_CHAR(r.rslt2) ELSE r.rslt1 END, r.rsltdate, r.reptdr, f.titleyn"
                ' sSql += " ORDER BY tkdt DESC, bcno, testcd"
                sSql += " ORDER BY bcno desc, tkdt , testcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function




    End Class

    Public Class Reg
        Private Const msFile As String = "File : CGLISAPP_V.vb, Class : LISAPP.APP_V.CommFn" + vbTab

    End Class

End Namespace

