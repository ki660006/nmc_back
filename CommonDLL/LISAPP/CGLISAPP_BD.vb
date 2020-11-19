'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : NEW LIS Laboratory Information System()                                */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_BD.vb                                                             */
'/* PartName     : 헌혈                                                                   */
'/* Description  : 헌혈 Class                                                             */
'/* Design       : 2010-08-26 Lee HYUNG TAEK                                              */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports Oracle.DataAccess.Client

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommPrint
Imports COMMON.CommLogin.LOGIN

Namespace APP_BD
    Public Class DonFn
        Private Const msFile As String = "File : CGLISAPP_BD.vb, Class : APP_BD.DonFn" & vbTab

        '-- 헌혈 정보
        Public Function fnGet_DonerList_Regno(ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "fnGet_DonerList_Regno fnGet_Doner_Regno(String) As DataTable "

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(b3.dondt, 'YYYY-MM-DD HH24:MI') dondt,"
                sSql += "       b0.dongbn, b0.donseq, b0.tnsregno,"
                sSql += "       fn_ack_get_pat_info(b0.regno, '', '') patinfo,"
                sSql += "       fn_ack_get_don_pat_info(b0.regno)     patinfo_don,"
                sSql += "       fn_ack_date_str(b2.judgdt, 'YYYY-MM-DD HH24:MI') judgdt,"
                sSql += "       CASE WHEN b2.judgyn = 'Y' THEN '적격' ELSE '부적격' END judgyn,"
                sSql += "       CASE WHEN b2.judgyn = 'Y' THEN b2.judgcmt ELSE b2.discont END judgcmt, b3.doncmt,"
                sSql += "       fn_ack_get_bldno_full(b3.bldno) bldno,"
                sSql += "       CASE WHEN b3.bldqnt = '0' THEN '400 ml' ELSE '320 ml' END donqnt"
                sSql += "  FROM lb010m b0, lb011m b1, lb012m b2, lb013m b3"
                sSql += " WHERE b0.regno   = :regno"
                sSql += "   AND b0.donjubsuno = b1.donjubsuno"
                sSql += "   AND b0.donjubsuno = b2.donjubsuno (+)"
                sSql += "   AND b0.donjubsuno = b3.donjubsuno"
                sSql += " ORDER BY dondt DESC"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try


        End Function

        '-- 헌혈 검사결과 및 판정
        Public Function fnGet_Doner_RstList(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsDonGbn As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_RstList(String) As DataTable"

            Try
                Dim sSql As String = "" '
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       j.bcno"
                sSql += "  FROM lj010m j, lj011m j1, lf140m f"
                sSql += " WHERE j.regno      = :regno"
                sSql += "   AND j.bcno       = j1.bcno"
                sSql += "   AND j1.tclscd    = f.testcd"
                sSql += "   AND j1.spccd     = f.spccd"
                If rsDonGbn = "4" Then
                    sSql += "   AND f.aordgbn    = '1'"
                Else
                    sSql += "   AND f.dordgbn    = '1'"
                End If
                sSql += "   AND j1.orgorddt >= :orddt1"
                sSql += "   AND j1.orgorddt <= :orddt2 || '235959'"
                sSql += " ORDER BY bcno"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("orddt1", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
                alParm.Add(New OracleParameter("orddt2", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        Public Function fnGet_Doner_RstList(ByVal rsRegNo As String, ByVal rsFkOcs As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_RstList(String) As DataTable"

            Try
                Dim sSql As String = "" '
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT bcno"
                sSql += "  FROM lj011m "
                sSql += " WHERE regno = :regno"
                sSql += "   AND fkocs = :fkocs"
                sSql += "   AND spcflg IN ('1', '2', '3', '4')"
                sSql += " ORDER BY bcno"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("fkocs", OracleDbType.Varchar2, rsFkOcs.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try
        End Function

        '-- 헌혈 검사결과 및 판정
        Public Function fnGet_Doner_JudgInfo(ByVal rsDonNo As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_JudgInfo(String) As DataTable"

            Try
                Dim sSql As String = "" '
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       judgyn, passgbn, judgcmt, discd, discont,"
                sSql += "       fn_ack_date_str(judgdt, 'YYYY-MM-DD HH24:MI') judgdt,"
                sSql += "       fn_ack_get_usr_name(judgid) judgnm"
                sSql += "  FROM lb012m"
                sSql += " WHERE donjubsuno = :dregno"

                alParm.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


        Public Shared Function fnGet_Doner_BldInfo(ByVal rsBldNo As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_BldInfo(String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT donregno, bldqnt, donbag, dondt, dongbn"
                sSql += "  FROM lb015m"
                sSql += " WHERE bldno = :bldno"

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        Public Shared Function fnGet_JudgRst(ByVal rsDonRegNo As String) As DataTable  ' 혈액이 판정부적격인지 알아오기!!
            Dim sFn As String = "Function fnGet_JudgRst(String) As String"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT judgrst FROM lb013m"
                sSql += " WHERE donregno = :dregno"

                alParm.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        Public Shared Function fnGet_Doner_Info(ByVal rsDonRegNo As String, ByVal rsDonGbn As String, Optional ByVal rsBBGbn As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Doner_Info(String, String, [String]) As DataTable"


            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT j.donregno, r.bcno, r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '') workno,"
                sSql += "       a.bbgbn, a.dispsq, a.testcd, a.spccd, f.tnmd, r.viewrst, r.rstflg"
                sSql += "  FROM lf140m a, lf060m f,  lr010m r,"
                sSql += "       (SELECT DISTINCT j.bcno, b.donregno FROM lj011m j, lb010m b"
                sSql += "         WHERE b.fkocs            = j.fkocs"
                sSql += "           AND NVL(j.spcflg, '0') > '0'"
                sSql += "           AND b.donregno         = :dregno"
                sSql += "       ) j"
                sSql += " WHERE j.bcno   = r.bcno"
                sSql += "   AND r.testcd = a.testcd"
                sSql += "   AND r.spccd  = a.spccd"
                sSql += "   AND r.testcd = f.testcd"
                sSql += "   AND r.spccd  = f.spccdCCD"
                sSql += "   AND r.tkdt  >= f.usdt"
                sSql += "   AND r.tkdt  <  f.uedt"

                alParm.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                If rsBBGbn = "" Then
                    sSql += "   AND a.bbgbn IN ('1', '3')"
                Else
                    sSql += "   AND a.bbgbn = :bbgbn"
                    alParm.Add(New OracleParameter("bbgbn", OracleDbType.Varchar2, rsBBGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBBGbn))
                End If

                If rsDonGbn = "0" Then
                    sSql += "   AND a.trstgbn = '1'"
                ElseIf rsDonGbn = "1" Or rsDonGbn = "2" Then
                    sSql += "   AND a.drstgbn = '1'"
                ElseIf rsDonGbn = "3" Then
                    sSql += "   AND a.arstgbn = '1'"
                End If

                sSql += " ORDER BY a.dispseq, r.bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '--
        Public Function fnGet_Doner_BldNo_Info(ByVal rsBldNo As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_BldNo_Info(String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT donregno, bldqnt, bldbag, dondt, dongbn"
                sSql += "  FROM lb015m"
                sSql += " WHERE bldno = :bldno"

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_Doner_Test_Info(ByVal rsDonRegNo As String, ByVal rsDonGbn As String, Optional ByVal rsBBGbn As String = "") As DataTable
            Dim sFn As String = "Sub fnGet_Doner_Test_Info(ByVal as_DonReg_No As String)"

            Dim sSql As String = ""
            Dim alParm As New ArrayList


            Try
                sSql += "SELECT j.donregno, r.bcno, r.workno, a.bbgbn, a.sortkey, a.testcd, a.spccd, f.tnmd, r.viewrst, r.rstflg"
                sSql += "  FROM lf140m a, lf060m f, lr010m r,"
                sSql += "       (SELECT DISTINCT j.bcno, b.donregno FROM lj011m j, lb010m b"
                sSql += "         WHERE b.donregno = :dregno"
                sSql += "           AND b.fkocs    = j.fkocs"
                sSql += "           AND j.spcflg  IN ('2', '3', '4')"
                sSql += "       ) j"
                sSql += " WHERE j.bcno   = r.bcno"
                sSql += "   and a.testcd = r.testcd"
                sSql += "   and a.spccd  = a.spccd"
                sSql += "   and r.testcd = f.testcd"
                sSql += "   and r.spccd  = f.spccd"
                sSql += "   and r.tkdt  >= f.usdt"
                sSql += "   and r.tkdt  <  f.uedt"

                alParm.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                If rsBBGbn = "" Then
                    sSql += "   and a.bbgbn IN ('1', '3')"
                Else
                    sSql += "   and a.bbgbn = :bbgbn"
                    alParm.Add(New OracleParameter("bbgbn", OracleDbType.Varchar2, rsBBGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBBGbn))
                End If

                If rsDonGbn = "0" Then
                    sSql += "   and a.trstgbn = '1'"
                ElseIf rsDonGbn = "1" Or rsDonGbn = "2" Then
                    sSql += "   and a.drstgbn = '1'"
                ElseIf rsDonGbn = "3" Then
                    sSql += "   and a.arstgbn = '1'"
                End If

                sSql += " ORDER TY a.sortkey, r.bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_AutoDonor(ByVal rsDonFlag As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsRegNo As String) As DataTable

            Dim sFn As String = "Function fnSelect_AssignDon(String, String, String, String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT /*+ ALL_ROWS */"
            sSql += "       b.donflg, b.regno, b.tnsnm patnm, SUBSTR(b.tnsjumin, 1, 6)||'-'||SUBSTR(b.tnsjumin, 7, 7) idno, tnssexage sexage,"
            sSql += "       b.owngbn, b.iogbn, b.fkocs, b.tordcd, b.donseq,"
            sSql += "       b.donregno, fn_ack_date_str(b.orddt, 'yyyy-mm-dd hh24:mi') orddt, b.doncmt liscmt,"
            sSql += "       fn_ack_date_str(b.donregdt, 'yyyy-mm-dd hh24:mi:ss') donregdt,"
            sSql += "       fn_ack_get_dept_abbr(o.gwa, o.in_out_gubun) deptnm, fn_ack_get_dr_name(o.doctor) drnm,"
            sSql += "       o.remark, CASE WHEN o.in_out_gubun = 'I' THEN o.ho_dong || '/' || o.ho_code ELSE '' END wardroom,"
            sSql += "       fn_ack_date_str(o.opdt, 'YYYY-MM-DD') opdt, r.judgrst"
            sSql += "  FROM lb010m b, lf060m f, lb013m r, mts0001_lis o"
            sSql += " WHERE b.regno    = o.bunho"
            sSql += "   AND b.iogbn    = o.in_out_gubun"
            sSql += "   AND b.fkocs    = o.fkocs"
            sSql += "   AND b.tordcd   = o.hangmog_code"
            sSql += "   AND f.tordcd   = o.hangmog_code"
            sSql += "   AND f.spccd    = o.specimen_code "
            sSql += "   AND f.usdt    <= b.donregdt"
            sSql += "   AND f.uedt    >  b.donregdt"
            sSql += "   AND b.donregno = r.donregno(+)"
            sSql += "   AND b.dongbn   = '3'"    '-- 자가헌혈

            If rsDonFlag = "2" Then
                sSql += "   AND b.donflg = '2'"
            Else
                sSql += "   AND NVL(b.donflg, '0') <> '2'"
            End If

            If rsRegNo <> "" Then
                sSql += "   AND b.regno = :regno"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            End If

            If rsDateS <> "" Then
                sSql += "   AND b.donregdt  >= :dates"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            End If

            If rsDateE <> "" Then
                sSql += "   AND b.donregdt  <= :datee || '235959'"

                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
            End If

            sSql += " UNION "
            sSql += "SELECT b.donflg, b.regno, b.tnsnm patnm, SUBSTR(b.tnsjumin, 1, 6)||'-'||SUBSTR(b.tnsjumin, 7, 7) idno, tnssexage sexage,"
            sSql += "       b.owngbn, b.iogbn, b.fkocs, b.tordcd, b.donseq,"
            sSql += "       b.donregno, fn_ack_date_str(b.orddt, 'yyyy-mm-dd hh24:mi') orddt, b.doncmt liscmt,"
            sSql += "       fn_ack_date_str(b.donregdt, 'yyyy-mm-dd hh24:mi:ss') donregdt,"
            sSql += "       fn_ack_get_dept_abbr(o.deptcd, o.iogbn) deptnm, fn_ack_get_dr_name(o.orddr) drnm,"
            sSql += "       o.remark, CASE WHEN o.iogbn = 'I' THEN o.wardno || '/' || o.roomno ELSE '' END wardroom,"
            sSql += "       fn_ack_date_str(o.opexdate, 'YYYY-MM-DD') opdt, r.judgrst"
            sSql += "  FROM lb010m b, lf060m f, lb013m r, vw_ack_ocs_ord_info o"
            sSql += " WHERE b.regno    = o.bunho"
            sSql += "   AND b.iogbn    = o.in_out_gubun"
            sSql += "   AND b.fkocs    = o.ioflag || '/' || o.patno || '/' + || o.orddate || '/' || TO_CHAR(o.ordseqno)"
            sSql += "   AND o.instcd   = '" + PRG_CONST.SITECD + "'"
            sSql += "   AND b.tordcd   = o.ordcd"
            sSql += "   AND f.tordcd   = o.ordcd"
            sSql += "   AND f.spccd    = o.spccd"
            sSql += "   AND f.usdt    <= b.donregdt"
            sSql += "   AND f.uedt    >  b.donregdt"
            sSql += "   AND b.donregno = r.donregno(+)"
            sSql += "   AND b.dongbn   = '3'"    '-- 자가헌혈

            If rsDonFlag = "2" Then
                sSql += "   AND b.donflg = '2'"
            Else
                sSql += "   AND NVL(b.donflg, '0') <> '2'"
            End If

            If rsRegNo <> "" Then
                sSql += "   AND b.regno = :regno"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            End If

            If rsDateS <> "" Then
                sSql += "   AND b.donregdt  >= :dates"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            End If

            If rsDateE <> "" Then
                sSql += "   AND b.donregdt  <= :datee || '235959'"

                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))
            End If

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        ' 자가헌혈페이지에서 이름으로 조회 (FGB04)
        Public Function fnGet_AutoDonor_PatNm(ByVal rsJubsu As String, ByVal rsPatNm As String) As DataTable
            Dim sFn As String = "Shared Function fnGet_AutoDonor_PatNm(String, String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT o.bunho regno, fn_ack_get_pat_info(o.bunho, '', '') patinfo"
            sSql += "       fn_ack_get_dept_abbr(o.gwa, o.in_out_gubun) deptnm,"
            sSql += "       f.bbttype, o.fkocs, "
            sSql += "       fn_ack_date_str(b.donregdt, 'yyyy-mm-dd hh24:mi:ss') denregdt"
            sSql += "  FROM mts0001_lis o, lb010m b, lf060m f"
            sSql += " WHERE o.bunho IN (SELECT bunho FROM mts0002_lis WHERE SUNAME = :patnm)"
            sSql += "   AND o.hangmog_code  = f.tordcd"
            sSql += "   AND o.specimen_code = f.spccd"
            sSql += "   AND NVL(b.donregdt, fn_ack_sysdate) >= f.usdt"
            sSql += "   AND NVL(b.donregdt, fn_ack_sysdate)  < f.uedt"

            If rsJubsu = "2" Then
                sSql += "   AND o.spcflg = '4'"
            Else
                sSql += "   AND NVL(o.spcflg, '0') = '0'"
            End If

            sSql += "   AND o.dc_yn= 'N'"
            sSql += "   and o.bunho = b.regno(+)"
            sSql += "   and o.fkocs = f.fkocs(+)"

            sSql += " UNION "
            sSql += "SELECT o.patno regno, fn_ack_get_pat_info(o.patno, '', '') patinfo"
            sSql += "       fn_ack_get_dept_abbr(o.deptcd, o.iogbn) deptnm,"
            sSql += "       f.bbttype, o.ioflag || '/' || o.patno || '/' || o.orddate || '/' || TO_CHAR(o.ordseqno) fkocs,"
            sSql += "       fn_ack_date_str(b.donregdt, 'yyyy-mm-dd hh24:mi:ss') denregdt"
            sSql += "  FROM vw_ack_ocs_ord_info o, lb010m b, lf060m f"
            sSql += " WHERE o.patnm IN (SELECT patno FROM vw_ack_pat_info WHERE patnm = :patnm)"
            sSql += "   AND o.ordcd  = f.tordcd"
            sSql += "   AND o.spccd  = f.spccd"
            sSql += "   AND NVL(b.donregdt, fn_ack_sysdate) >= f.usdt"
            sSql += "   AND NVL(b.donregdt, fn_ack_sysdate)  <  f.uedt"

            If rsJubsu = "2" Then
                sSql += "           AND CASE WHEN NVL(a.procstat, '0')  = '0' THEN '0'"
                sSql += "                    WHEN NVL(a.procstat, '0')  = 'B' THEN '2'"
                sSql += "                    WHEN NVL(a.procstat, '0') IN ('C', 'E') THEN '4'"
                sSql += "                    ELSE '4'"
                sSql += "               END = '4'"
            Else
                sSql += "           AND CASE WHEN NVL(a.procstat, '0')  = '0' THEN '0'"
                sSql += "                    WHEN NVL(a.procstat, '0')  = 'B' THEN '2'"
                sSql += "                    WHEN NVL(a.procstat, '0') IN ('C', 'E') THEN '4'"
                sSql += "                    ELSE '4'"
                sSql += "               END = '0'"
            End If

            sSql += "   AND NVL(o.discyn, 'N') = 'N'"
            sSql += "   AND o.patno   = b.regno(+)"
            sSql += "   AND o.ioflag || '/' || o.patno || '/' || o.orddate || '/' || TO_CHAR(o.ordseqno) = b.fkocs(+)"
            sSql += "   AND o.instcd   = '" + PRG_CONST.SITECD + "'"
            sSql += " ORDER BY fkocs DESC"

            alParm.Add(New OracleParameter("patnm", OracleDbType.Varchar2, rsPatNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPatNm))
            alParm.Add(New OracleParameter("patnm", OracleDbType.Varchar2, rsPatNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsPatNm))

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_Doner_AutoInfo(ByVal rsRegno As String, Optional ByVal rsDonRegNo As String = "", Optional ByVal rsOrdDt As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Doner_AutoInfo(String, [String = ""], [String = ""]) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT fn_ack_date_str(c.donregdt, 'YYYY-MM-DD HH24:MI') donregdt,"
                sSql += "       fn_ack_date_str(b.dondt, 'YYYY-MM-DD HH24:MI') dondt,"
                sSql += "       b.bldno, fn_ack_get_bldno_full(b.bldno) bldno_vw,"
                sSql += "       b.bldqnt, DECODE(b.bldqnt, '0', '400㎖', '1', '320㎖') bldqnt,"
                sSql += "       b.bldbag, DECODE(b.bldbag, '0', 'T/B', '1', 'D/B', '2', 'S/B') bldbag,"
                sSql += " 	    c.regno, c.donregno, b.cmt"
                sSql += "  FROM lb015m b, lb010m c"
                sSql += " WHERE b.donregno = c.donregno"
                sSql += "   AND c.regno    = :regno"
                sSql += "   AND c.dongbn   = '3'"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                If rsOrdDt <> "" Then
                    sSql += "    and c.orddt = :orddt"
                    alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
                End If

                If rsDonRegNo <> "" Then
                    sSql += "  and c.donregno = :dregno"
                    alParm.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))
                End If

                sSql += " ORDER BY donregdt"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_Doner_Bcno(ByVal rsDonRegNo As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_Bcno(String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT fn_ack_get_bcno_full(bcno) bcno"
                sSql += "  FROM lj011m"
                sSql += " WHERE (fkocs) in (SELECT fkocs FROM lb010m WHERE donregno = :dregno)"
                sSql += "   AND SUBSTR(bcno, 9, 1) = '" + PRG_CONST.BCCLS_BloodBank.Substring(0, 1) + "'"
                sSql += "   AND spcflag IN ('2', '3', '4')"

                alParm.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Function fnGet_Doner_Suryang(ByVal rsRegNo As String, ByVal rsOrdDt As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_Suryang(String, String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList


            Try
                sSql += "SELECT COUNT(*) suryang"
                sSql += "  FROM mts0001_lis"
                sSql += " WHERE bunho = :regno"
                sSql += "   AND (hangmog_code, specimen_code) IN (SELECT tordcd, spccd FROM lf060m WHERE bbttype IN ('6', 'A', 'B', 'C', 'D'))"
                sSql += "   AND order_date = :orddt"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
    End Class

    Public Class OcsFn
        Private Const msFile As String = "File : CGLISAPP_BD.vb, Class : APP_BD.OcsFn" + vbTab

        Public Shared Function fnGet_Don_Order(ByVal rsIoGbn As String, ByVal rsDptOrWard As String, _
                                               ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                                ByVal rsRegno As String, ByVal rsSpcFlg As String, ByVal rsFkOcs As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Don_Order(String, String, String, String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alPram As New ArrayList

                rsOrdDtS = rsOrdDtS.Replace("-", "")
                rsOrdDtE = rsOrdDtE.Replace("-", "")

                If rsRegno <> "" Then
                    sSql += "pkg_ack_don.pkg_get_order_regno"

                    alPram.Add(New OracleParameter("rs_regno", rsRegno))
                Else
                    sSql += "pkg_ack_don.pkg_get_order"

                    alPram.Add(New OracleParameter("rs_orddt1", OracleDbType.Varchar2, rsOrdDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtS))
                    alPram.Add(New OracleParameter("rs_orddt2", OracleDbType.Varchar2, rsOrdDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDtE))
                End If

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alPram, False)
                Dim sWhere As String = ""

                If rsIoGbn = "I" Then
                    sWhere = "iogbn = 'I'"
                    If rsDptOrWard <> "" Then sWhere += " AND wardcd = '" + rsDptOrWard + "'"
                ElseIf rsIoGbn = "O" Then
                    sWhere = "iogbn <> 'I'"
                    If rsDptOrWard <> "" Then sWhere += " AND deptcd = '" + rsDptOrWard + "'"
                End If

                If rsSpcFlg = "0" Then

                    sWhere += " AND donjubsuno = '0'"
                Else
                    sWhere += " AND donjubsuno <> '0'"
                End If


                If rsFkOcs <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "fkocs = '" + rsFkOcs + "'"

                If sWhere <> "" Then
                    Dim dr As DataRow()

                    dr = dt.Select(sWhere, "")
                    dt = Fn.ChangeToDataTable(dr)

                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Function fnGet_Doner_Ocs_Info(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsBBTType As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_Ocs_Info(String, String, String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT distinct"
            sSql += "       o.regno, fn_ack_get_pat_info(o.regno, '', ''),"
            sSql += "       fn_ack_date_str(o.orddt, 'YYYY-MM-DD') orddt, o.opdt,"
            sSql += "       fn_ack_get_dr_name(o.drcd) drnm, fn_ack_get_dept_abbr(o.deptcd, o.iogbn),"
            sSql += "       CASE WHEN iogbn = 'I' THEN o.wardcd || '/' || o.roomno ELSE '' END wardroom, o.tordcd, o.spccd"
            sSql += "  FROM (SELECT bunho regno, order_date orddt, fn_ack_date_str(opdt, 'YYYY-MM-DD') opdt,"
            sSql += "               ho_dong wardcd, ho_code roomno, doctor drcd, gwa deptcd,"
            sSql += "               hangmog_code tordcd, specimen_code spccd, in_out_gubun iogbn"
            sSql += "          FROM mts0001_lis"
            sSql += "         WHERE order_date >= :orddt1"
            sSql += "           AND order_date <= :orddt2"
            sSql += "           AND spcflg     >= '2'"
            sSql += "           AND dc_yn       = 'N'"
            sSql += "         UNION "
            sSql += "        SELECT patno regno, orddate orddt, opexdate opdt,"
            sSql += "               wardno wardcd, roomno, orddr drcd, deptcd, ordcd tordcd, spccd, iogbn"
            sSql += "          FROM vw_ack_ocs_ord_info"
            sSql += "         WHERE orddate >= :orddt1"
            sSql += "           AND orddate <= :orddt2"
            sSql += "           AND instcd   = '" + PRG_CONST.SITECD + "'"
            sSql += "           AND CASE WHEN NVL(a.procstat, '0')  = '0' THEN '0'"
            sSql += "                    WHEN NVL(a.procstat, '0')  = 'B' THEN '2'"
            sSql += "                    WHEN NVL(a.procstat, '0') IN ('C', 'E') THEN '4'"
            sSql += "                    ELSE '4'"
            sSql += "               END >= '2'"
            sSql += "           AND NVL(discyn, 'N') = 'N'"
            sSql += "       ) o, lb010m b, lf060m f"
            sSql += " WHERE o.regno   = b.regno"
            sSql += "   AND o.orddt   = b.orddt"
            sSql += "   AND o.tordcd  = f.tordcd"
            sSql += "   AND o.spccd   = f.spccd"
            sSql += "   AND o.orddt  >= f.usdt"
            sSql += "   AND o.orddt  <  f.uedt"
            sSql += "   AND b.refflg  = '0'"
            sSql += "   AND f.bbttype = :bbttype"
            sSql += " ORDER BY orddt"

            alParm.Add(New OracleParameter("orddt1", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            alParm.Add(New OracleParameter("orddt2", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

            alParm.Add(New OracleParameter("orddt1", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
            alParm.Add(New OracleParameter("orddt2", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

            alParm.Add(New OracleParameter("bbttype", OracleDbType.Varchar2, rsBBTType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBBTType))

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            End Try

        End Function


        ' 1.헌혈자 등록화면에서 수혈자 정보를 보는경우, 2.처방일자로 수혈자 조회하기
        Public Function fnGet_Doner_Ocs_PatInfo(ByVal rsRegNo As String, ByVal rsBBTType As String, Optional ByVal rsOrdDt As String = "") As DataTable
            Dim sFn As String = "Function fnGet_Doner_Ocs_PatInfo(String, String, [string]) As DataTable "

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT DISTINCT"
            sSql += "       fn_ack_date_str(o.orddt, 'YYYY-MM-DD') orddt,"
            sSql += "       o.regno, fn_ack_get_pat_info(o.regno, '', '') patinfo,"
            sSql += "       o.owngbn, o.iogbn, o.fkocs,"
            sSql += "       o.opdt opdt, o.wardcd, o.roomno,"
            sSql += "       fn_ack_get_dr_name(o.drcd) drnm, fn_ack_get_dept_abbr(o.deptcd, o.iogbn) deptnm,"
            sSql += "       o.remark, r.abo, r.rh, '' bcno,"
            sSql += "       fn_ack_date_str(r.rstdt, 'YYYY-MM-DD HH24:MI') rstdt, o.liscmt, s.suryang, o.tordcd, o.spccd"
            sSql += "  from (SELECT bunho regno, order_date orddt, fn_ack_date_str(opdt, 'YYYY-MM-DD') opdt,"
            sSql += "               ho_dong wardcd, ho_code roomno, doctor drcd, gwa deptcd,"
            sSql += "               hangmog_code tordcd, specimen_code spccd, in_out_gubun iogbn, 'L', owngbn,"
            sSql += "               fkocs, remark"
            sSql += "          FROM mts0001_lis a, lf060m b"
            sSql += "         WHERE bunho             = :regno"
            sSql += "           AND NVL(spcflg, '0') >= '0'"
            sSql += "           AND dc_yn             = 'N'"

            alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

            If rsOrdDt <> "" Then
                sSql += "           AND order_date = :orddt"
                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsOrdDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
            End If

            sSql += "         UNION "
            sSql += "        SELECT patno regno, orddate orddt, fn_ack_date_str(opexdate, 'YYYY-MM-DD') opdt,"
            sSql += "               wardno wardcd, roomno, orddr drcd, deptcd, ordcd tordcd, spccd, iogbn, 'O', owngbn,"
            sSql += "               ioflag || '/' || patno||'/'||orddate||'/'||TO_CHAR(ordseqno) fkocs"
            sSql += "          FROM vw_ack_ocs_ord_info a, lf060m b"
            sSql += "         WHERE patno    = :regno"
            sSql += "           AND instcd   = '" + PRG_CONST.SITECD + "'"
            sSql += "           AND CASE WHEN NVL(a.procstat, '0')  = '0' THEN '0'"
            sSql += "                    WHEN NVL(a.procstat, '0')  = 'B' THEN '2'"
            sSql += "                    WHEN NVL(a.procstat, '0') IN ('C', 'E') THEN '4'"
            sSql += "                    ELSE '4'"
            sSql += "               END = '0'"
            sSql += "           AND NVL(discyn, 'N') = 'N'"

            alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

            If rsOrdDt <> "" Then
                sSql += "           AND orddate = :orddt"
                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
            End If

            sSql += "       ) o, lr070m r,"


            sSql += "       (SELECT count(*) suryang, o.order_date orddt, o.bunho regno"
            sSql += "          FROM mts0001_lis a, lf060m b"
            sSql += "         WHERE o.bunho         = :regno"
            sSql += "           AND o.hangmog_code  = b.tordcd"
            sSql += "           AND o.specimen_code = b.spccd"
            sSql += "           AND o.order_date   >= b.usdt"
            sSql += "           AND o.order_date   <  b.uedt"
            sSql += "           AND b.bbttype       = :bbttype"

            alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            alParm.Add(New OracleParameter("bbttype", OracleDbType.Varchar2, rsBBTType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBBTType))

            If rsOrdDt <> "" Then
                sSql += "           and o.ORDER_DATE = :orddt"

                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
            End If
            sSql += "         GROUP BY o.order_date, o.bunho"
            sSql += "         UNION "
            sSql += "        SELECT count(*) suryang, o.orddate orddt, o.patno regno"
            sSql += "          FROM vw_ack_ocs_ord_info a, lf060m b"
            sSql += "         WHERE o.patno    = :regno"
            sSql += "           AND o.instcd   = '" + PRG_CONST.SITECD + "'"
            sSql += "           AND o.ordcd    = b.tordcd"
            sSql += "           AND o.spccd    = b.spccd"
            sSql += "           AND o.orddate >= b.usdt"
            sSql += "           AND o.orddate <  b.uedt"
            sSql += "           AND b.bbttype  = :bbttype"

            alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
            alParm.Add(New OracleParameter("bbbtype", OracleDbType.Varchar2, rsBBTType.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBBTType))

            If rsOrdDt <> "" Then
                sSql += "           and o.orddate = :orddt"

                alParm.Add(New OracleParameter("orddt", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsOrdDt))
            End If
            sSql += "         GROUP BY o.orddate, o.patno"
            sSql += "       ) s"

            sSql += " WHERE o.orddt = s.orddt"
            sSql += "   AND o.regno = r.regno(+)"
            sSql += " ORDER BY fkocs"

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            End Try

        End Function

        Public Shared Function fnGet_Doner_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsJubsuFlg As String, ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_Doner_List(String, String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alPram As New ArrayList

                rsDateS = rsDateS.Replace("-", "")
                rsDateE = rsDateE.Replace("-", "")

                sSql += "pkg_ack_don.pkg_get_doner"

                alPram.Add(New OracleParameter("rs_jubsudt1", OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS.Replace("-", "")))
                alPram.Add(New OracleParameter("rs_jubsudt2", OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE.Replace("-", "")))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alPram, False)

                Dim sWhere As String = "jubsuflg = '" + rsJubsuFlg + "'"

                If rsRegno <> "" Then
                    sWhere += " AND regno = '" + rsRegno + "'"
                End If

                Dim dr As DataRow()

                dr = dt.Select(sWhere, "jubsudt")
                dt = Fn.ChangeToDataTable(dr)


                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function


        Public Shared Function fnGet_Doner_Test(ByVal rsDonGbn As String, ByVal rsDate As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Doner_Test(String, String, [String]) As DataTable"

            Dim sSql As String = ""


            Try
                If rsDate = "" Then rsDate = Format(Now, "yyyyMMddHHmmss").ToString

                sSql += "SELECT f14.dispseq, f14.testcd, f14.spccd, f6.tnmd"
                sSql += "  FROM lf140m f14, lf060m f6"
                sSql += " WHERE f14.testcd = f6.testcd"
                sSql += "   AND f14.spccd  = f6.spccd"
                sSql += "   AND f6.usdt   <= '" + rsDate + "'"
                sSql += "   AND f6.uedt   >  '" + rsDate + "'"


                If rsDonGbn = "1" Then
                    sSql += "   AND f14.trstgbn = '1'"
                ElseIf rsDonGbn = "2" Or rsDonGbn = "3" Then
                    sSql += "   AND f14.drstgbn = '1'"
                ElseIf rsDonGbn = "4" Then
                    sSql += "   AND f14.arstgbn = '1'"
                End If

                sSql += " ORDER BY dispseq"

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_Doner_Rst(ByVal rsRegNo As String, ByVal rsDonGbn As String) As DataTable
            Dim sFn As String = "Public Function fnGet_Doner_Rst(String, String, [String]) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList


            Try
                sSql += "SELECT r.regno, fn_ack_get_bcno_full(r.bcno) bcno, r.workno, f14.bbgbn, f14.dispseq, f14.testcd, f14.spccd, f6.tnmd,"
                sSql += "       r.viewrst, r.hlmark, r.panicmark, r.deltamark, r.rstflg"
                sSql += "  FROM lf140m f14, lf060m f6,"
                sSql += "       (SELECT DISTINCT"
                sSql += "               a.regno, b.bcno, b.bcprtdt, NVL(b.rstflg, '0') rstflg, NVL(d.testcd, c.tclscd) testcd, NVL(d.spccd, c.spccd) spccd,"
                sSql += "               d.viewrst, d.hlmark, d.panicmark, d.deltamark, d.wkymd || NVL(d.wkgrpcd, '') || NVL(d.wkno, '') workno"
                sSql += "          FROM lb010m a, lj010m b, lj011m c, lr010m d"
                sSql += "         WHERE a.regno   = :regno"
                sSql += "           AND a.fkocs   = c.fkocs"
                sSql += "           AND a.owngbn  = c.owngbn"
                sSql += "           AND a.iogbn   = c.iogbn"
                sSql += "           AND c.spcflg IN ('1', '2', '3', '4')"
                sSql += "           AND b.bcno    = c.bcno"
                sSql += "           AND c.bcno    = d.bcno(+)"
                sSql += "           AND c.tclscd  = d.tclscd(+)"
                sSql += "       ) r"
                sSql += " WHERE f14.testcd = f6.testcd"
                sSql += "   AND f14.spccd  = f6.spccd"
                sSql += "   AND f14.testcd = r.testcd"
                sSql += "   AND f14.spccd  = r.spccd"
                sSql += "   AND f6.usdt <= NVL(r.bcprtdt, fn_ack_sysdate)"
                sSql += "   AND f6.uedt >  NVL(r.bcprtdt, fn_ack_sysdate)"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

                If rsDonGbn = "1" Then
                    sSql += "   AND f14.trstgbn = '1'"
                ElseIf rsDonGbn = "2" Or rsDonGbn = "3" Then
                    sSql += "   AND f14.drstgbn = '1'"
                ElseIf rsDonGbn = "4" Then
                    sSql += "   AND f14.arstgbn = '1'"
                End If

                sSql += " ORDER BY dispseq, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_Doner_Info(ByVal rsRegno As String, ByVal rsQryGbn As String, ByVal rsDonjubsuno As String) As DataTable
            Dim sFn As String = "Function fnGet_Doner_Info(String, [String = ""], [String = ""]) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT fn_ack_date_str(b0.jubsudt, 'yyyy-mm-dd hh24:mi') jubsudt,"
                sSql += "       fn_ack_date_str(b3.dondt, 'yyyy-mm-dd hh24:mi') dondt,"
                sSql += " 	    b0.regno, b0.tnsregno,"
                sSql += "       CASE WHEN b2.judgyn = 'Y' THEN '적격' ELSE '부적격' END judg,"
                sSql += "       b2.discont, fn_ack_date_str(b2.judgdt, 'yyyy-mm-dd hh24:mi') judgdt, fn_ack_get_usr_name(judgid) judgnm,"
                sSql += "       fn_ack_get_bldno_full(b3.bldno) bldno, b3.bldqnt, b3.donbag, b3.passgbn, b3.doncmt"
                sSql += "  FROM lb010M b0, lb012m b2, lb013m b3"
                sSql += " WHERE b0.regno      = :regno"
                sSql += "   AND b0.donjubsuno = b2.donjubsuno"
                sSql += "   AND b0.donjubsuno = b3.donjubsuno(+)"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                If rsQryGbn = "H" Then
                    sSql += "   AND b0.donjubsuno <> :donno"
                Else
                    sSql += "   AND b0.donjubsuno  = :donno"
                End If

                alParm.Add(New OracleParameter("donno", OracleDbType.Varchar2, rsDonjubsuno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonjubsuno))

                sSql += " ORDER BY dondt ASC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try

        End Function

    End Class

    Public Class RegFn
        Private Const msFile As String = "File : CGLISAPP_BD.vb, Class : APP_BD.ExeFn" & vbTab

        Private m_DbCn As OracleConnection
        Private m_DbTrans As OracleTransaction

        Public msBldnum As String = ""

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_DbTrans = m_DbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Private Function fnGet_Server_DateTime() As String
            Dim sFn As String = "Private Function fnGet_Server_DateTime() As string"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDA As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = "SELECT fn_ack_sysdate srvdate FROM DUAL"

                dbCmd.Connection = m_DbCn
                dbCmd.Transaction = m_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDA = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDA.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("srvdate").ToString()
                Else
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                End If

            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmmss").ToString
            End Try

        End Function

        Private Function fnGet_Don_Order(ByVal rsFkOcs As String, ByVal rsOwnGbn As String) As DataTable

            Dim sFn As String = "Private Sub fnGet_Don_Order(String)"

            Try
                Dim dt As New DataTable
                Dim dbCmd As New OracleCommand
                Dim objDAdapter As oracleDataAdapter

                dbCmd.Connection = m_DbCn
                dbCmd.Transaction = m_DbTrans
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = "pkg_ack_don.pkg_get_order_fkocs"

                objDAdapter = New OracleDataAdapter(dbCmd)
                objDAdapter.SelectCommand.Parameters.Add("rs_fkocs", OracleDbType.Varchar2).Value = rsFkOcs
                objDAdapter.SelectCommand.Parameters.Add("rs_owngbn", OracleDbType.Varchar2).Value = rsOwnGbn

                dt.Reset()
                objDAdapter.Fill(dt)

                Return dt
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Private Function fnGet_DonNo(ByVal rsJusuDt As String) As String
            ' 수혈의뢰접수 번호 생성
            Dim sFn As String = "Public Shared Function fn_getTnsNum() As String"
            Dim DbCmd As New oracleCommand

            Try
                Dim iDonNo As Integer = 0

                With DbCmd
                    DbCmd.Connection = m_DbCn
                    DbCmd.Transaction = m_DbTrans
                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "pro_ack_exe_seqno_don"

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("rs_seqymd", rsJusuDt.Substring(0, 4)))

                    .Parameters.Add("rn_seqno", OracleDbType.Int32)
                    .Parameters("rn_seqno").Direction = ParameterDirection.InputOutput
                    .Parameters("rn_seqno").Value = -1

                    .ExecuteNonQuery()

                    iDonNo = CType(.Parameters(1).Value.ToString, Integer)
                End With

                If iDonNo > 0 Then
                    Return iDonNo.ToString.PadLeft(5, "0"c)
                Else
                    Return ""
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Private Function fnGet_BldNo(ByVal rsJusuDt As String, ByVal rsHDodNo As String) As String
            ' 수혈의뢰접수 번호 생성
            Dim sFn As String = "Public Shared Function fn_getTnsNum() As String"
            Dim DbCmd As New oracleCommand

            Try
                Dim iDonNo As Integer = 0

                With DbCmd
                    DbCmd.Connection = m_DbCn
                    DbCmd.Transaction = m_DbTrans
                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "pro_ack_exe_seqno_bld"

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("rs_seqymd", rsJusuDt.Substring(0, 4)))
                    .Parameters.Add(New OracleParameter("rs_seqgbn", rsHDodNo))

                    .Parameters.Add("rn_seqno", OracleDbType.Int32)
                    .Parameters("rn_seqno").Direction = ParameterDirection.InputOutput
                    .Parameters("rn_seqno").Value = -1

                    .ExecuteNonQuery()

                    iDonNo = CType(.Parameters(2).Value.ToString, Integer)
                End With

                If iDonNo > 0 Then
                    Return iDonNo.ToString.PadLeft(6, "0"c)
                Else
                    Return ""
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

            End Try

        End Function

        Public Function fnExe_Don_Jubsu(ByVal r_stu_don As STU_DONER) As Boolean
            Dim sFn As String = "Function fnExe_Don_Jubsu(ByVal as_BldNm ).... "

            Dim dbCmd As New OracleCommand
            dbCmd.Connection = m_DbCn
            dbCmd.Transaction = m_DbTrans

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Try
                Dim dt As DataTable = fnGet_Don_Order(r_stu_don.FkOcs, r_stu_don.OwnGbn)

                If dt.Rows.Count < 1 Then
                    Throw (New Exception("접수할 데이타가 존재하지 않습니다.!!"))
                End If

                Dim dtSysDate As Date
                Dim sSrvDt As String = fnGet_Server_DateTime()
                Dim sDonNo As String = fnGet_DonNo(sSrvDt)

                dtSysDate = CDate(sSrvDt.Substring(0, 4) + "-" + sSrvDt.Substring(4, 2) + "-" + sSrvDt.Substring(6, 2))

                If sDonNo = "" Then
                    m_DbTrans.Rollback()
                    Throw (New Exception("헌혈접수번호 생성시 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                End If

                sDonNo = sSrvDt.Substring(0, 4) + "D" + sDonNo

                Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

                '< 나이계산
                Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                '>

                sSql = ""
                sSql += "INSERT INTO lb010m"
                sSql += "          ( donjubsuno, dongbn, regno, patnm, sex, age, owngbn, iogbn,"
                sSql += "            orddt, ordtm, fkocs, tordcd, spccd, deptcd, orddrcd, fmydrcd, gendrcd, wardcd,"
                sSql += "            roomno, bedno, entdt, resdt, eryn, opdt, drrmk, weight, height, ocs_key,"
                sSql += "            ordpart, tnsregno, jubsuflg, jubsudt, jubsuid, editdt, editid, editip"
                sSql += "          ) "
                sSql += "    VALUES( :donno,   :dongbn, :regno, :patnm,   :sex,     :age,           :owngbn,  :iogbn, "
                sSql += "            :orddt,   :ordtm,  :fkocs, :tordcd,  :spccd,   :deptcd,        :orddrcd, :fmydrcd, :gendrcd, :wardcd,"
                sSql += "            :roomno,  :bedno,  :entdt, :resdt,   :eryn,    :opdt,          :drrmk,   :weight,  :height,  :ocskey,"
                sSql += "            :ordpart, :tregno, '2',    :jubsudt, :jubsuid, fn_ack_sysdate, :editid,  :editip"
                sSql += "          )"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, sDonNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDonNo))                                     '-- 1
                    .Parameters.Add(New OracleParameter("dongbn", OracleDbType.Varchar2, r_stu_don.DonGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonGbn))                           '-- 2
                    .Parameters.Add(New OracleParameter("regno", OracleDbType.Varchar2, dt.Rows(0).Item("regno").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("regno").ToString))          '-- 3
                    .Parameters.Add(New OracleParameter("patnm", OracleDbType.Varchar2, sPatInfo(0).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sPatInfo(0)))                                '-- 4
                    .Parameters.Add(New OracleParameter("sex", OracleDbType.Varchar2, sPatInfo(1).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sPatInfo(1)))                                '-- 5
                    .Parameters.Add(New OracleParameter("age", OracleDbType.Varchar2, iAge.ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, iAge))                                       '-- 6   
                    .Parameters.Add(New OracleParameter("owngbn", OracleDbType.Varchar2, r_stu_don.OwnGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.OwnGbn))                           '-- 7   
                    .Parameters.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, dt.Rows(0).Item("iogbn").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("iogbn").ToString))          '-- 8

                    .Parameters.Add(New OracleParameter("orddt", OracleDbType.Varchar2, dt.Rows(0).Item("orddt").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("orddt").ToString))         '-- 9
                    .Parameters.Add(New OracleParameter("ordtm", OracleDbType.Varchar2, dt.Rows(0).Item("ordtm").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("ordtm").ToString))         '-- 10
                    .Parameters.Add(New OracleParameter("fkocs", OracleDbType.Varchar2, r_stu_don.FkOcs.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.FkOcs))                           '-- 11
                    .Parameters.Add(New OracleParameter("tordcd", OracleDbType.Varchar2, dt.Rows(0).Item("tordcd").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("tordcd").ToString))        '-- 12
                    .Parameters.Add(New OracleParameter("spccd", OracleDbType.Varchar2, dt.Rows(0).Item("spccd").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("spccd").ToString))         '-- 13
                    .Parameters.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, dt.Rows(0).Item("deptcd").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("deptcd").ToString))        '-- 14    
                    .Parameters.Add(New OracleParameter("orddrcd", OracleDbType.Varchar2, dt.Rows(0).Item("orddrcd").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("orddrcd").ToString))       '-- 15
                    .Parameters.Add(New OracleParameter("fmydrcd", OracleDbType.Varchar2, dt.Rows(0).Item("fmydrcd").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("fmydrcd").ToString))       '-- 16
                    .Parameters.Add(New OracleParameter("gendrcd", OracleDbType.Varchar2, dt.Rows(0).Item("gendrcd").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("gendrcd").ToString))       '-- 17  
                    .Parameters.Add(New OracleParameter("wardcd", OracleDbType.Varchar2, dt.Rows(0).Item("wardcd").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("wardcd").ToString))        '-- 18  

                    .Parameters.Add(New OracleParameter("roomno", OracleDbType.Varchar2, dt.Rows(0).Item("roomno").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("roomno").ToString))        '-- 19
                    .Parameters.Add(New OracleParameter("bedno", OracleDbType.Varchar2, dt.Rows(0).Item("bedno").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("bedno").ToString))         '-- 20
                    .Parameters.Add(New OracleParameter("entdt", OracleDbType.Varchar2, dt.Rows(0).Item("entdt").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("entdt").ToString))         '-- 21
                    .Parameters.Add(New OracleParameter("resdt", OracleDbType.Varchar2, dt.Rows(0).Item("resdt").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("resdt").ToString))         '-- 22
                    .Parameters.Add(New OracleParameter("eryn", OracleDbType.Varchar2, dt.Rows(0).Item("erflg").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("erflg").ToString))         '-- 23
                    .Parameters.Add(New OracleParameter("opdt", OracleDbType.Varchar2, dt.Rows(0).Item("opdt").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("opdt").ToString))          '-- 24
                    .Parameters.Add(New OracleParameter("drrmk", OracleDbType.Varchar2, dt.Rows(0).Item("drrmk").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("drrmk").ToString))         '-- 25    
                    .Parameters.Add(New OracleParameter("weight", OracleDbType.Varchar2, dt.Rows(0).Item("weight").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("weight").ToString))        '-- 26
                    .Parameters.Add(New OracleParameter("height", OracleDbType.Varchar2, dt.Rows(0).Item("height").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("height").ToString))        '-- 27

                    If dt.Rows(0).Item("ocs_key1").ToString = "" Then
                        .Parameters.Add(New OracleParameter("ocskey", OracleDbType.Int64, 1, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, 0))      '-- 28
                    Else

                        .Parameters.Add(New OracleParameter("ocskey", OracleDbType.Int64, dt.Rows(0).Item("ocs_key1").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("ocs_key1").ToString))      '-- 28
                    End If

                    .Parameters.Add(New OracleParameter("ordpart", OracleDbType.Varchar2, dt.Rows(0).Item("ordpart").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("ordpart").ToString))       '-- 29

                    .Parameters.Add(New OracleParameter("tregno", OracleDbType.Varchar2, dt.Rows(0).Item("regno").ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, dt.Rows(0).Item("regno").ToString))        '-- 30
                    .Parameters.Add(New OracleParameter("jubsudt", OracleDbType.Varchar2, sSrvDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sSrvDt))                                   '-- 31
                    .Parameters.Add(New OracleParameter("jubsuid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))                          '-- 32

                    .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))                          '-- 33
                    .Parameters.Add(New OracleParameter("endtip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))                        '-- 34

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Throw (New Exception("헌혈접수 테이블[LB010M]에서 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                    End If

                    sSql = ""
                    sSql += "INSERT INTO lb012m("
                    sSql += "            donjubsuno, judgyn, passgbn, judgcmt, discd, discont, judgdt, judgid, editdt, editid, editip)"
                    sSql += "    VALUES( :donno, :judgyn, :passgbn, :judgcmt, :discd, :discnt, :judgdt, :judgid, fn_ack_sysdate, :editid, :editip)"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, sDonNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDonNo))
                    .Parameters.Add(New OracleParameter("judgyn", OracleDbType.Varchar2, r_stu_don.JudgYn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.JudgYn))
                    .Parameters.Add(New OracleParameter("passgbn", OracleDbType.Varchar2, r_stu_don.PassGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.PassGbn))
                    .Parameters.Add(New OracleParameter("judgcmt", OracleDbType.Varchar2, r_stu_don.judgCmt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.judgCmt))
                    .Parameters.Add(New OracleParameter("discd", OracleDbType.Varchar2, r_stu_don.DisCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DisCd))
                    .Parameters.Add(New OracleParameter("discont", OracleDbType.Varchar2, r_stu_don.DisCont.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DisCont))
                    .Parameters.Add(New OracleParameter("judgdt", OracleDbType.Varchar2, sSrvDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sSrvDt))
                    .Parameters.Add(New OracleParameter("jubsuid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))

                    .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("endtip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Throw (New Exception("판정 테이블[LB012M]에서 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                    End If

                    '.CommandType = CommandType.StoredProcedure
                    '.CommandText = "pro_ack_exe_ocs_don"

                    '.Parameters.Clear()

                    '.Parameters.Add(New oracleParameter("rs_regno", r_stu_don.RegNo))
                    '.Parameters.Add(New oracleParameter("rs_owngbn", r_stu_don.OwnGbn))
                    '.Parameters.Add(New oracleParameter("rs_fkocs", r_stu_don.FkOcs))
                    '.Parameters.Add(New oracleParameter("rs_donno", sDonNo))
                    '.Parameters.Add(New oracleParameter("rs_spcflg", "2"))
                    '.Parameters.Add(New oracleParameter("rs_acptdt ", sSrvDt))
                    '.Parameters.Add(New oracleParameter("rs_usrid", USER_INFO.USRID))
                    '.Parameters.Add(New oracleParameter("rs_ip", USER_INFO.LOCALIP))

                    '.Parameters.Add("ri_retval", OracleDbType.Number)
                    '.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    '.Parameters("ri_retval").Value = -1

                    '.ExecuteNonQuery()

                    'iRet = CType(.Parameters(8).Value.ToString, Integer)
                    'If iRet < 1 Then
                    '    m_DbTrans.Rollback()
                    '    Throw (New Exception("처방연동(상태)시 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                    'End If

                    If r_stu_don.PassGbn = "1" Then
                        Dim stu_ocs As New OCSAPP.OcsLink.ChgOcsState

                        stu_ocs.RegNo = dt.Rows(0).Item("regno").ToString
                        stu_ocs.TotFkOcs = r_stu_don.FkOcs
                        stu_ocs.OwnGbn = r_stu_don.OwnGbn
                        stu_ocs.LabCmt = r_stu_don.judgCmt

                        iRet = (New OCSAPP.OcsLink.Ord).SetOrderChgLisCmt(stu_ocs, m_DbCn, m_DbTrans)
                        If iRet < 1 Then
                            m_DbTrans.Rollback()
                            Throw (New Exception("처방연동(전달)시 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                        End If
                    End If
                End With

                If iRet < 1 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()
                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

            End Try

        End Function

        Public Function fnExe_Don_Jubsu_Cancel(ByVal r_stu_don As STU_DONER) As Boolean
            Dim sFn As String = "Function fnExe_Don_Jubsu_Cancel(ByVal as_BldNm ).... "

            Dim dbCmd As New oracleCommand
            dbCmd.Connection = m_DbCn
            dbCmd.Transaction = m_DbTrans

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Try

                Dim sSrvDt As String = fnGet_Server_DateTime()

                With dbCmd

                    sSql += "INSERT INTO lb010h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb010m a"
                    sSql += " WHERE donjubsuno = :donno"
                    sSql += "   AND jubsuno    = '1'"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("moidp", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql = ""
                    sSql += "DELETE lb010m"
                    sSql += " WHERE donjubsuno = :donno"
                    sSql += "   AND jubsuno    = '1'"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql += "INSERT INTO lb011h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb011m a"
                    sSql += " WHERE donjubsuno = :donno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("momid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE lb011m WHERE donjubsuno = :donno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()


                    sSql += "INSERT INTO lb012h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb012m a"
                    sSql += " WHERE donjubsuno = :donno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("momid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql = ""
                    sSql += "DELETE lb012m WHERE donjubsuno = :donno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    '.CommandType = CommandType.StoredProcedure
                    '.CommandText = "pro_ack_exe_ocs_don"

                    '.Parameters.Clear()

                    '.Parameters.Add(New oracleParameter("rs_regno", r_stu_don.RegNo))
                    '.Parameters.Add(New oracleParameter("rs_owngbn", r_stu_don.OwnGbn))
                    '.Parameters.Add(New oracleParameter("rs_fkocs", r_stu_don.FkOcs))
                    '.Parameters.Add(New oracleParameter("rs_donno", r_stu_don.DonJusbuNo))
                    '.Parameters.Add(New oracleParameter("rs_spcflg", "D1"))
                    '.Parameters.Add(New oracleParameter("rs_acptdt", sSrvDt))
                    '.Parameters.Add(New oracleParameter("rs_usrid", USER_INFO.USRID))
                    '.Parameters.Add(New oracleParameter("rs_ip", USER_INFO.LOCALIP))

                    '.Parameters.Add("ri_retval", OracleDbType.Number)
                    '.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    '.Parameters("ri_retval").Value = -1

                    '.ExecuteNonQuery()

                    'iRet = CType(.Parameters(7).Value.TOSTRING, Integer)
                    'If iRet < 1 Then
                    '    m_DbTrans.Rollback()
                    '    Return False
                    'End If

                    If r_stu_don.PassGbn = "1" Then
                        Dim stu_ocs As New OCSAPP.OcsLink.ChgOcsState

                        stu_ocs.RegNo = r_stu_don.RegNo
                        stu_ocs.TotFkOcs = r_stu_don.FkOcs
                        stu_ocs.OwnGbn = r_stu_don.OwnGbn
                        stu_ocs.LabCmt = ""

                        iRet = (New OCSAPP.OcsLink.Ord).SetOrderChgLisCmt(stu_ocs, m_DbCn, m_DbTrans)
                        If iRet < 1 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    End If
                End With

                If iRet < 1 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()
                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing
            End Try

        End Function

        Public Function fnExe_Don_Bldno(ByVal r_stu_don As STU_DONER) As Boolean
            Dim sFn As String = "Function fnExe_Don_Bldno(ByVal as_BldNm ).... "

            Dim dbCmd As New oracleCommand
            dbCmd.Connection = m_DbCn
            dbCmd.Transaction = m_DbTrans

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Try

                Dim dtSysDate As Date
                Dim sSrvDt As String = fnGet_Server_DateTime()
                Dim sBldNo As String = fnGet_BldNo(sSrvDt, PRG_CONST.HOSPITAL_DONER_NO)

                dtSysDate = CDate(sSrvDt.Substring(0, 4) + "-" + sSrvDt.Substring(4, 2) + "-" + sSrvDt.Substring(6, 2))

                If sBldNo = "" Then
                    Throw (New Exception("혈액번호 생성시 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                End If

                r_stu_don.BldNo = PRG_CONST.HOSPITAL_DONER_NO + sSrvDt.Substring(2, 2) + sBldNo

                sSql = ""
                sSql += "UPDATE lb010m SET jubsuflg = '3', editdt = fn_ack_sysdate, editid = editid, editip = :editip"
                sSql += " WHERE donjubsuno = :donno"

                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("endtip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        Throw (New Exception("헌혈접수 테이블[LB010M]에서 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                    End If

                    sSql = ""
                    sSql += "INSERT INTO lb013m("
                    sSql += "            donjubsuno, bldno,  bldqnt,  donbag,  abo,  rh,  passgbn,  dondt,          donid,  dongbn,  doncmt,  editdt,         editid,  editip)"
                    sSql += "    VALUES( :donno,     :bldno, :bldqnt, :donbag, :abo, :rh, :passgbn, fn_ack_sysdate, :donid, :dongbn, :doncmt, fn_ack_sysdate, :editid, :editip)"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))
                    .Parameters.Add(New OracleParameter("bldno", OracleDbType.Varchar2, r_stu_don.BldNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.BldNo))
                    .Parameters.Add(New OracleParameter("bldqnt", OracleDbType.Varchar2, r_stu_don.BldQnt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.BldQnt))
                    .Parameters.Add(New OracleParameter("donbag", OracleDbType.Varchar2, r_stu_don.DonBag.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonBag))
                    .Parameters.Add(New OracleParameter("abo", OracleDbType.Varchar2, r_stu_don.ABO.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.ABO))
                    .Parameters.Add(New OracleParameter("rh", OracleDbType.Varchar2, r_stu_don.Rh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.Rh))
                    .Parameters.Add(New OracleParameter("passgbn", OracleDbType.Varchar2, r_stu_don.PassGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.PassGbn))
                    .Parameters.Add(New OracleParameter("donid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("dongbn", OracleDbType.Varchar2, r_stu_don.DonGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonGbn))
                    .Parameters.Add(New OracleParameter("doncmt", OracleDbType.Varchar2, r_stu_don.DonCmt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonCmt))

                    .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("endtip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        Throw (New Exception("헌혈 혈액정보 테이블[LB013M]에서 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                    End If

                    '.CommandType = CommandType.StoredProcedure
                    '.CommandText = "pro_ack_exe_ocs_don"

                    '.Parameters.Clear()

                    '.Parameters.Add(New oracleParameter("rs_regno", r_stu_don.RegNo))
                    '.Parameters.Add(New oracleParameter("rs_owngbn", r_stu_don.OwnGbn))
                    '.Parameters.Add(New oracleParameter("rs_fkocs", r_stu_don.FkOcs))
                    '.Parameters.Add(New oracleParameter("rs_donno", sDonNo))
                    '.Parameters.Add(New oracleParameter("rs_spcflg", "2"))
                    '.Parameters.Add(New oracleParameter("rs_acptdt", sSrvDt))
                    '.Parameters.Add(New oracleParameter("rs_usrid", USER_INFO.USRID))
                    '.Parameters.Add(New oracleParameter("rs_ip", USER_INFO.LOCALIP))

                    '.Parameters.Add("ri_retval", OracleDbType.Number)
                    '.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    '.Parameters("ri_retval").Value = -1

                    '.ExecuteNonQuery()

                    'iRet = CType(.Parameters(8).Value.TOSTRING, Integer)
                    'If iRet < 1 Then
                    '    m_DbTrans.Rollback()
                    '    Throw (New Exception("처방연동(상태)시 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                    'End If

                    If r_stu_don.PassGbn = "1" Then
                        Dim stu_ocs As New OCSAPP.OcsLink.ChgOcsState

                        stu_ocs.RegNo = r_stu_don.RegNo
                        stu_ocs.TotFkOcs = r_stu_don.FkOcs
                        stu_ocs.OwnGbn = r_stu_don.OwnGbn
                        stu_ocs.LabCmt = r_stu_don.DonCmt

                        iRet = (New OCSAPP.OcsLink.Ord).SetOrderChgLisCmt(stu_ocs, m_DbCn, m_DbTrans)
                        If iRet < 1 Then
                            Throw (New Exception("처방연동(전달)시 오류가 발생했습니다.!!" + " @" + msFile + sFn))
                        End If
                    End If
                End With

                If iRet < 1 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()
                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing
            End Try

        End Function

        Public Function fnExe_Don_BldNo_Cancel(ByVal r_stu_don As STU_DONER) As Boolean
            Dim sFn As String = "Function fnExe_Don_BldNo_Cancel(ByVal as_BldNm ).... "

            Dim dbCmd As New oracleCommand
            dbCmd.Connection = m_DbCn
            dbCmd.Transaction = m_DbTrans

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Try

                Dim sSrvDt As String = fnGet_Server_DateTime()

                With dbCmd

                    sSql += "INSERT INTO lb010h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb010m a"
                    sSql += " WHERE donjubsuno = :donno"
                    sSql += "   AND jubsuno    = '3'"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql = ""
                    sSql += "UPDATE lb010m SET jubsuflg = '2', editdt = fn_ack_sysdate, editid = :editid, editip = :editip"
                    sSql += " WHERE donjubsuno = :donno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("editip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql += "INSERT INTO lb013h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb013m a"
                    sSql += " WHERE donjubsuno = :donno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE lb013m WHERE donjubsuno = :donno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("donno", OracleDbType.Varchar2, r_stu_don.DonJusbuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, r_stu_don.DonJusbuNo))

                    iRet = .ExecuteNonQuery()
                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    '.CommandType = CommandType.StoredProcedure
                    '.CommandText = "pro_ack_exe_ocs_don"

                    '.Parameters.Clear()

                    '.Parameters.Add(New oracleParameter("rs_regno", r_stu_don.RegNo))
                    '.Parameters.Add(New oracleParameter("rs_owngbn", r_stu_don.OwnGbn))
                    '.Parameters.Add(New oracleParameter("rs_fkocs", r_stu_don.FkOcs))
                    '.Parameters.Add(New oracleParameter("rs_donno", r_stu_don.DonJusbuNo))
                    '.Parameters.Add(New oracleParameter("rs_spcflg", "D1"))
                    '.Parameters.Add(New oracleParameter("rs_acptdt", sSrvDt))
                    '.Parameters.Add(New oracleParameter("rs_usrid", USER_INFO.USRID))
                    '.Parameters.Add(New oracleParameter("rs_ip", USER_INFO.LOCALIP))

                    '.Parameters.Add("ri_retval", OracleDbType.Number)
                    '.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    '.Parameters("ri_retval").Value = -1

                    '.ExecuteNonQuery()

                    'iRet = CType(.Parameters(7).Value.TOSTRING, Integer)
                    'If iRet < 1 Then
                    '    m_DbTrans.Rollback()
                    '    Return False
                    'End If

                    If r_stu_don.PassGbn = "1" Then
                        Dim stu_ocs As New OCSAPP.OcsLink.ChgOcsState

                        stu_ocs.RegNo = r_stu_don.RegNo
                        stu_ocs.TotFkOcs = r_stu_don.FkOcs
                        stu_ocs.OwnGbn = r_stu_don.OwnGbn
                        stu_ocs.LabCmt = ""

                        iRet = (New OCSAPP.OcsLink.Ord).SetOrderChgLisCmt(stu_ocs, m_DbCn, m_DbTrans)
                        If iRet < 1 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    End If
                End With

                If iRet < 1 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()
                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing
            End Try

        End Function

        Public Function fnExe_Doner_Modify(ByVal rsBldNo As String, ByVal rsBldQnt As String, ByVal rsBldBag As String, _
                                           ByVal rsComGbn As String, ByVal rsDonGbn As String, ByVal rsCmt As String, _
                                           ByVal rsDonRegNo As String, ByVal rsOwnGbn As String, ByVal rsLisCmtYn As String, _
                                           Optional ByVal rsCmtAll As String = "") As Boolean
            Dim sFn As String = "Function fnExe_Doner_Modify(ByVal as_bldnm As String, ....) "

            Dim dbCmd As New oracleCommand
            dbCmd.Connection = m_DbCn
            dbCmd.Transaction = m_DbTrans

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Try
                With dbCmd
                    sSql = ""
                    sSql += "INSERT INTO lb015h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb015m a"
                    sSql += " WHERE bldno = :bldno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldNo))

                    iRet = .ExecuteNonQuery()

                    sSql = ""
                    sSql += "UPDATE lb015m"
                    sSql += "   SET bldqnt  = :bldqnt,"
                    sSql += "       bldbag  = :bldbag,"
                    sSql += "       comgbn  = :comgbn,"
                    sSql += "       cmt     = :cmt,"
                    sSql += "       editdt  = fn_ack_sysdate,"
                    sSql += "       editid  = :editid,"
                    sSql += "       editip  = :editip"
                    sSql += " WHERE bldno   = bldno"

                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("bldqnt", OracleDbType.Varchar2, rsBldQnt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldQnt))
                    .Parameters.Add(New OracleParameter("bldbag", OracleDbType.Varchar2, rsBldBag.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldBag))
                    .Parameters.Add(New OracleParameter("comgbn", OracleDbType.Varchar2, rsComGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComGbn))
                    .Parameters.Add(New OracleParameter("cmt", OracleDbType.Varchar2, rsCmt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsCmt))

                    .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("editip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldNo))

                    iRet = .ExecuteNonQuery()

                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql = ""
                    If Not rsDonGbn.Equals("1") Then     ' 지정(2), 성분(3), 자가(4)

                        If rsLisCmtYn = "Y" Then '체크가 된경우에만 update 한다

                            If rsOwnGbn = "O" Then
                                sSql = "UPDATE MTS0001"
                            Else
                                sSql = "UPDATE mts0001_lis"
                                If rsDonGbn.Equals("4") Then ' 자가
                                    sSql = "  SET liscmt = '" + rsCmt + "',"
                                Else
                                    sSql = "  SET liscmt = '" + rsCmtAll + "',"
                                End If
                                sSql += "      editdt = fn_ack_sysdate,"
                                sSql += "      editid = :editid,"
                                sSql += "      editip = :editip"
                                sSql += " WHERE (bunho, fkocs) IN"
                                sSql += "       (SELECT regno, fkocs FROM lb010m WHERE donregno = :dregno)"


                            End If

                            .Parameters.Clear()
                            .Parameters.Add(New OracleParameter("editid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                            .Parameters.Add(New OracleParameter("editip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                            .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                            iRet = .ExecuteNonQuery()

                            If iRet < 1 Then
                                m_DbTrans.Rollback()
                                Return False
                            End If

                        End If
                    End If

                End With

                m_DbTrans.Commit()
                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing
            End Try

        End Function

        Public Function fnExe_Doner_Del(ByVal rsDonRegNo As String, ByVal rsOwnGbn As String, ByVal rsLisCmtYn As String, ByVal rsLisCmt As String) As Boolean
            Dim sFn As String = "Function fnExe_Doner_Delete(String, String, String, String) as Boolean"

            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim dbCmd As New oracleCommand
            dbCmd.Connection = m_DbCn
            dbCmd.Transaction = m_DbTrans

            Try
                With dbCmd
                    sSql = ""
                    sSql += "INSERT INTO lb015h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip,, a.*"
                    sSql += "  FROM lb015m a"
                    sSql += " WHERE donregno = :dregno"

                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                    iRet = .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE lb015m"
                    sSql += " WHERE donregno = :dregno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                    iRet = .ExecuteNonQuery()

                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql = ""
                    sSql += "INSERT INTO lb013h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb013m a"
                    sSql += " WHERE donregno = :dregno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                    iRet = .ExecuteNonQuery()

                    sSql = ""
                    sSql += "DELETE lb013m"
                    sSql += " WHERE donregno = :dregno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                    iRet = .ExecuteNonQuery()

                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    If rsOwnGbn = "O" Then
                    Else
                        sSql = "UPDATE mts0001_lis"
                        sSql += "   SET spcflg = NULL, colldt = NULL, tkdt = NULL"

                        If rsLisCmtYn = "Y" Then '체크가 된경우에만 update 한다
                            sSql += ", LISCMT = '" + rsLisCmt + "',"
                        End If

                        sSql += " WHERE (bunho, fkocs) IN"
                        sSql += "       (SELECT regno, fkocs FROM lb10m WHERE donregno = :dregno)"
                    End If

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                    iRet = .ExecuteNonQuery()

                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sSql = ""
                    sSql += "INSERT INTO lb010h "
                    sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
                    sSql += "  FROM lb010m a"
                    sSql += " WHERE donregno = :dregno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("modid", OracleDbType.Varchar2, USER_INFO.USRID.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.USRID))
                    .Parameters.Add(New OracleParameter("modip", OracleDbType.Varchar2, USER_INFO.LOCALIP.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, USER_INFO.LOCALIP))
                    .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                    iRet = .ExecuteNonQuery()

                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If


                    sSql = ""
                    sSql += "DELETE lb010m"
                    sSql += " WHERE donregno = dregno"

                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add(New OracleParameter("dregno", OracleDbType.Varchar2, rsDonRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDonRegNo))

                    iRet = .ExecuteNonQuery()

                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If
                End With


                m_DbTrans.Commit()
                Return True

            Catch ex As Exception

                m_DbTrans.Rollback()
                Throw (New Exception(msFile + sFn + vbCrLf + ex.Message, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing
            End Try

        End Function

    End Class

#Region " 헌혈 구조체 선언 "

    Public Class STU_DONER
        Public RegNo As String = ""         ' 등록번호  (REGNO - > 병원의 고유한 KEY)
        Public PatNm As String = ""         ' 성명
        Public Sex As String = ""           ' 성별
        Public Age As String = ""           ' 나이
        Public Address1 As String = ""      ' 앞주소
        Public Address2 As String = ""      ' 뒷주소
        Public DrNm As String = ""          ' 의뢰의사
        Public DeptNm As String = ""        ' 진료과
        Public WardCd As String = ""        ' 병동 -> 추후변경
        Public ROOMNO As String = ""        ' 병실 -> 추후변경
        Public OpDtPre As String = ""       ' 수술예정일 -> 추후변경
        Public IdLeft As String = ""        ' 주민등록번호 앞자리
        Public IdRight As String = ""       ' 주민등록번호 뒷자리
        Public AboRh As String = ""         ' 혈액형 (ABO + RH)
        Public BcNo As String = ""          ' 검체번호
        Public RstDt As String = ""         ' 결과일시
        Public DonQnt As String = ""        ' 헌혈 요청 인원
        Public Remark As String = ""        ' 의뢰의사 Remark
        Public FkOcs As String = ""         ' ocs key
        Public TOrdCd As String = ""        ' 처방코드
        Public IoGbn As String = ""         ' 
        Public OwnGbn As String = ""        ' ocs key
        Public DonRegNo As String = ""      ' 헌혈자 등록번호
        Public DonSeq As String = ""        ' 헌혈회차
        Public OrdDt As String = ""         ' 처방일자  ( ex) 2003-06-05 )

        Public DonJusbuNo As String = ""    '-- 헌헐접수번호
        Public DonGbn As String = ""        '-- 헌혈구부ㄴ
        Public JudgYn As String = ""        '-- 적격여부
        Public DisCd As String = ""         '-- 부적격 사유코드
        Public DisCont As String = ""       '-- 부적격 내용
        Public PassGbn As String = ""       '-- 판정 Comment 구분
        Public judgCmt As String = ""       '-- 판정 Comment

        Public BldNo As String = ""         '-- 혈액번호
        Public BldQnt As String = ""        '-- 용량(0:400, 1:320)
        Public DonBag As String = ""        '-- 혈액백(0:T/B, 1:D/B, 2:S?B)
        Public ABO As String = ""
        Public Rh As String = ""
        Public DonCmt As String = ""

        Public Sub New()
            MyBase.New()
        End Sub
    End Class

#End Region


End Namespace
