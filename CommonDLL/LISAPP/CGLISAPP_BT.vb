'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : NEW LIS Laboratory Information System()                                */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : CGDA_BT.vb                                                             */
'/* PartName     : 수혈의뢰                                                               */
'/* Description  : 수혈의뢰 Class                                                         */
'/* Design       : 2010-08-26 Lee Hyung Taek                                              */
'/* Coded        :                                                                        */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/
Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.CommPrint
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommLogin

Imports Oracle.DataAccess.Client

Namespace APP_BT

#Region " 혈액 레코드용지(스티커) 출력 "
    Public Class DB_BloodPrint
        Private Const msFile As String = "File : CGDA_BK.vb, Class : DB_BloodPrint" & vbTab

        Public Function fnGet_Blood_Label_Info(ByVal rsTnsNo As String, ByVal rsBldNos As String) As DataTable
            Dim sFn As String = "fnGet_Blood_Label_Info"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       b4.regno, b4.patnm, b4.sex || '/' || b4.age sexage,"
                sSql += "       FN_ACK_GET_DEPT_ABBR(b4.iogbn, b4.deptcd) || CASE WHEN b4.iogbn = 'I' THEN '/' || FN_ACK_GET_WARD_ABBR(b4.wardno) ELSE '' END deptward,"
                sSql += "       b43.abo || b43.rh pat_aborh, b2.abo || b2.rh bld_aborh, f.comnmp,"
                sSql += "       fn_ack_get_bldno_full(b3.bldno) bldno,"
                sSql += "       b3.rst1, b3.rst2, b3.rst3, b3.rst4, " '< 20121217 수정
                sSql += "       fn_ack_date_str(b3.testdt, 'yyyy-mm-dd hh24:mi') testdt, fn_ack_get_usr_name(b3.testid) testnm,"
                sSql += "       fn_ack_date_str(b3.befoutdt, 'yyyy-mm-dd hh24:mi') befoutdt, fn_ack_get_usr_name(b3.befoutid) befoutnm,"
                sSql += "       fn_ack_date_str(b3.outdt, 'yyyy-mm-dd hh24:mi') outdt, fn_ack_get_usr_name(b3.outid) outnm,"
                sSql += "       fn_ack_date_str(b3.outdt, 'yyyy-mm-dd hh24:mi') recdt, recnm,"
                sSql += "       fn_ack_get_prtlabel_bldnoaddyn(b3.comcd_out) bldno_add,"
                sSql += "       /*fn_get_hb_tk(b4.regno, b4.jubsudt)*/ '' hb_rst,"
                sSql += "       CASE WHEN b43.filter = '1'THEN 'Y' ELSE '' END filter,"
                sSql += "       CASE WHEN b43.ir = '1' THEN 'Y' ELSE '' END ir,"
                sSql += "       fn_ack_date_str(b4.orddt, 'yyyy-mm-dd hh24:mi') orddt,"
                sSql += "       fn_ack_get_dr_name(b4.doctorcd) drnm,"
                sSql += "       b43.comnm comnm_ord,"
                sSql += "       b42.reqqnt, "
                sSql += "       b43.seq,"
                sSql += "       b4.tnsgbn"
                sSql += "  FROM lb040m b4, lb042m b42, lb043m b43, lb020m b2, lf120m f,"
                sSql += "       (SELECT bldno, testid, testdt, befoutid, befoutdt, outid, outdt, recnm, rst1, rst2, rst3, rst4, comcd_out, tnsjubsuno"
                sSql += "          FROM lb030m"
                sSql += "         WHERE bldno     IN ('" + rsBldNos.Replace(",", "','").ToString + "')"
                sSql += "           AND tnsjubsuno = :tnsno"
                sSql += "         UNION "
                sSql += "        SELECT bldno, testid, testdt, befoutid, befoutdt, outid, outdt, recnm, rst1, rst2, rst3, rst4, comcd_out, tnsjubsuno"
                sSql += "          FROM lb031m"
                sSql += "         WHERE bldno     IN ('" + rsBldNos.Replace(",", "','").ToString + "')"
                sSql += "           AND tnsjubsuno = :tnsno"
                sSql += "       ) b3"
                sSql += " WHERE b4.tnsjubsuno  = :tnsno"
                sSql += "   AND b4.tnsjubsuno  = b42.tnsjubsuno"
                sSql += "   AND b4.tnsjubsuno  = b43.tnsjubsuno"
                sSql += "   AND b43.bldno      = b3.bldno"
                sSql += "   AND b43.comcd_out  = b3.comcd_out"
                sSql += "   AND b43.tnsjubsuno = b3.tnsjubsuno"
                sSql += "   AND b3.bldno       = b2.bldno"
                sSql += "   AND b3.comcd_out   = b2.comcd"
                sSql += "   AND b3.comcd_out   = f.comcd"
                sSql += "   AND b4.jubsudt    >= f.usdt"
                sSql += "   AND b4.jubsudt    <  f.uedt"
                sSql += "  ORDER BY seq, /*bldno,*/ testdt"

                alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + sFn, ex))
            End Try
        End Function

        Public Sub PrintDo(ByVal rsFrmName As String, ByVal ra_BldInfo As ArrayList, ByVal rbLabel As Boolean, ByVal rbSummary As Boolean, ByVal riCopy As Integer)
            Dim sFn As String = "Public Shared Sub PrintDo(ByVal asBloodList As ArrayList, ByVal asCOMM_STATE As String)"

            Try
                Dim sTnsNo As String = ""
                Dim sBldNos As String = ""
                Dim alBldLabel As New ArrayList

                For ix As Integer = 0 To ra_BldInfo.Count - 1
                    sTnsNo = CType(ra_BldInfo(ix), STU_TnsJubsu).TNSJUBSUNO

                    If ix > 0 Then sBldNos += ","
                    sBldNos += CType(ra_BldInfo(ix), STU_TnsJubsu).BLDNO
                Next

                Dim dt = fnGet_Blood_Label_Info(sTnsNo, sBldNos)
                If dt.Rows.Count > 0 Then

                    Dim bldinfo As New STU_BLDLABEL

                    If rbLabel Then

                        For iCnt As Integer = 1 To riCopy
                            For ix As Integer = 0 To dt.Rows.Count - 1

                                bldinfo = New STU_BLDLABEL
                                With bldinfo
                                    .REGNO = dt.Rows(ix).Item("regno").ToString.Trim
                                    .PATNM = dt.Rows(ix).Item("patnm").ToString.Trim
                                    .SEXAGE = dt.Rows(ix).Item("sexage").ToString.Trim
                                    .DEPTWARD = dt.Rows(ix).Item("deptward").ToString.Trim
                                    .PAT_ABORH = dt.Rows(ix).Item("pat_aborh").ToString.Trim
                                    .BLD_ABORH = dt.Rows(ix).Item("bld_aborh").ToString.Trim
                                    .COMNM = dt.Rows(ix).Item("comnmp").ToString.Trim
                                    .BLDNO.Add(dt.Rows(ix).Item("bldno").ToString.Trim)

                                    If dt.Rows(ix).Item("rst1").ToString.Trim = "-" Then
                                        .XMATCH1 = "적합"
                                    ElseIf dt.Rows(ix).Item("rst1").ToString.Trim = "" Then
                                        .XMATCH1 = ""
                                    Else
                                        .XMATCH1 = "부적합"
                                    End If

                                    If dt.Rows(ix).Item("rst2").ToString.Trim = "-" Then
                                        .XMATCH2 = "적합"
                                    ElseIf dt.Rows(ix).Item("rst2").ToString.Trim = "" Then
                                        .XMATCH2 = ""
                                    Else
                                        .XMATCH2 = "부적합"
                                    End If

                                    '< 20121217 3차적합판정

                                    If dt.Rows(ix).Item("rst3").ToString.Trim = "-" Then
                                        .XMATCH3 = "적합"
                                    ElseIf dt.Rows(ix).Item("rst3").ToString.Trim = "" Then
                                        .XMATCH3 = ""
                                    Else
                                        .XMATCH3 = "부적합"
                                    End If

                                    If dt.Rows(ix).Item("rst4").ToString.Trim = "-" Then
                                        .XMATCH4 = "적합"
                                    ElseIf dt.Rows(ix).Item("rst4").ToString.Trim = "" Then
                                        .XMATCH4 = ""
                                    Else
                                        .XMATCH4 = "부적합"
                                    End If

                                    .TESTDT = dt.Rows(ix).Item("testdt").ToString.Trim
                                    .TESTNM = dt.Rows(ix).Item("testnm").ToString.Trim
                                    .BEFOUTDT = dt.Rows(ix).Item("befoutdt").ToString.Trim
                                    .BEFOUTNM = dt.Rows(ix).Item("befoutnm").ToString.Trim
                                    .OUTDT = dt.Rows(ix).Item("outdt").ToString.Trim
                                    .OUTNM = dt.Rows(ix).Item("outnm").ToString.Trim
                                    .RECDT = dt.Rows(ix).Item("recdt").ToString.Trim
                                    .RECNM = dt.Rows(ix).Item("recnm").ToString.Trim
                                    .Hb_RST = dt.Rows(ix).Item("hb_rst").ToString.Trim

                                    .FITER = dt.Rows(ix).Item("filter").ToString.Trim
                                    .IR = dt.Rows(ix).Item("filter").ToString.Trim

                                    '20210719 jhs 혈액은행 응급 구분 추가
                                    If dt.Rows(ix).Item("tnsgbn").ToString.Trim = "3" Then
                                        .EMER = "Y"
                                    Else
                                        .EMER = "N"
                                    End If
                                    '-------------------------------------------
                                End With

                                If iCnt = 0 Then
                                    alBldLabel.Add(bldinfo)
                                ElseIf dt.Rows(ix).Item("bldno_add").ToString = "N" Then
                                    alBldLabel.Add(bldinfo)
                                End If
                            Next
                        Next
                    End If


                    If rbSummary Then
                        For ix As Integer = 0 To dt.Rows.Count - 1

                            If (ix + 1) Mod 4 = 1 Then

                                If ix > 0 Then
                                    alBldLabel.Add(bldinfo)
                                    If dt.Rows(ix).Item("bldno_add").ToString = "Y" Then alBldLabel.Add(bldinfo)
                                End If

                                bldinfo = New STU_BLDLABEL
                            End If

                            With bldinfo
                                .REGNO = dt.Rows(ix).Item("regno").ToString.Trim
                                .PATNM = dt.Rows(ix).Item("patnm").ToString.Trim
                                .SEXAGE = dt.Rows(ix).Item("sexage").ToString.Trim
                                .DEPTWARD = dt.Rows(ix).Item("deptward").ToString.Trim
                                .PAT_ABORH = dt.Rows(ix).Item("pat_aborh").ToString.Trim
                                .BLD_ABORH = dt.Rows(ix).Item("bld_aborh").ToString.Trim
                                .COMNM = dt.Rows(ix).Item("comnmp").ToString.Trim
                                If dt.Rows(ix).Item("rst1").ToString.Trim = "-" Then
                                    .XMATCH1 = "적합"
                                ElseIf dt.Rows(ix).Item("rst1").ToString.Trim = "" Then
                                    .XMATCH1 = ""
                                Else
                                    .XMATCH1 = "부적합"
                                End If

                                If dt.Rows(ix).Item("rst2").ToString.Trim = "-" Then
                                    .XMATCH2 = "적합"
                                ElseIf dt.Rows(ix).Item("rst2").ToString.Trim = "" Then
                                    .XMATCH2 = ""
                                Else
                                    .XMATCH2 = "부적합"
                                End If

                                '< 20121217 3차적합판정

                                If dt.Rows(ix).Item("rst3").ToString.Trim = "-" Then
                                    .XMATCH3 = "적합"
                                ElseIf dt.Rows(ix).Item("rst3").ToString.Trim = "" Then
                                    .XMATCH3 = ""
                                Else
                                    .XMATCH3 = "부적합"
                                End If

                                If dt.Rows(ix).Item("rst4").ToString.Trim = "-" Then
                                    .XMATCH4 = "적합"
                                ElseIf dt.Rows(ix).Item("rst4").ToString.Trim = "" Then
                                    .XMATCH4 = ""
                                Else
                                    .XMATCH4 = "부적합"
                                End If

                                .TESTDT = dt.Rows(ix).Item("testdt").ToString.Trim
                                .TESTNM = dt.Rows(ix).Item("testnm").ToString.Trim
                                .BEFOUTDT = dt.Rows(ix).Item("befoutdt").ToString.Trim
                                .BEFOUTNM = dt.Rows(ix).Item("befoutnm").ToString.Trim
                                .OUTDT = dt.Rows(ix).Item("outdt").ToString.Trim
                                .OUTNM = dt.Rows(ix).Item("outnm").ToString.Trim
                                .RECDT = dt.Rows(ix).Item("recdt").ToString.Trim
                                .RECNM = dt.Rows(ix).Item("recnm").ToString.Trim
                                .Hb_RST = dt.Rows(ix).Item("hb_rst").ToString.Trim

                                .BLDNO.Add(dt.Rows(ix).Item("bldno").ToString.Trim)

                                '20210719 jhs 혈액은행 응급 구분 추가
                                If dt.Rows(ix).Item("tnsgbn").ToString.Trim = "3" Then
                                    .EMER = "Y"
                                Else
                                    .EMER = "N"
                                End If
                                '-------------------------------------------

                            End With
                        Next

                        If dt.Rows.Count > 0 Then

                            alBldLabel.Add(bldinfo)
                            If dt.Rows(0).Item("bldno_add").ToString = "Y" Then alBldLabel.Add(bldinfo)

                        End If
                    End If
                End If

                alBldLabel.TrimToSize()

                If alBldLabel.Count > 0 Then

                    Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(rsFrmName)
                    objBCPrt.PrintDo_Blood(alBldLabel, riCopy)

                End If


            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Throw (New Exception(ex.Message, ex))

            End Try

        End Sub

    End Class

#End Region

    '-- 혈액은행 공통함수
    Public Class SqlFn
        ' 혈액이력 입력
        Public Function fnGet_InsLB020HSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb020h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.* "
            sSql += "  FROM lb020m a"
            sSql += " WHERE bldno = :bldno"
            sSql += "   AND comcd = :comcd"

            Return sSql
        End Function



        ' 혈액상태 변경
        Public Function fnGet_UpdLB020MStateSql() As String
            Dim sSql As String = ""

            sSql += "UPDATE lb020m"
            sSql += "   SET state   = :state,"
            sSql += "       statedt = fn_ack_sysdate,"
            sSql += "       editid  = :editid,"
            sSql += "       editip  = :editip,"
            sSql += "       editdt  = fn_ack_sysdate"
            sSql += " WHERE bldno   = :bldno"
            sSql += "   AND comcd   = :comcd"

            Return sSql
        End Function

        ' 혈액이력입력 2
        Public Function fnGet_InsLB020H_SetStateSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb020h( "
            sSql += "       moddt, modid, modip, bldno, comcd, indt, inplace, dongbn,"
            sSql += "       abo, rh, donqnt, dondt, availdt, state, statedt, cmt, inid,"
            sSql += "       inlastid, regno, usedgbn, ir, filter, pedgbn, regid, regip,"
            sSql += "       regdt, editid, editip, editdt"
            sSql += "     )"
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.bldno, a.comcd, a.indt, a.inplace, a.dongbn,"
            sSql += "       a.abo, a.rh, a.donqnt, a.dondt, a.availdt, ?, a.statedt, a.cmt, a.inid,"
            sSql += "       a.inlastid, a.regno, a.usedgbn, a.ir, a.filter, a.pedgbn, a.regid, a.regip,"
            sSql += "       a.regdt, a.editid, a.editip, a.editdt "
            sSql += "  FROM lb020m a"
            sSql += " WHERE bldno = :bldno"
            sSql += "   AND comcd = :comcd"

            Return sSql

        End Function

        ' 혈액 출고 히스토리 입력
        Public Function fnGet_InsLB030HSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb030h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.* "
            sSql += "  FROM lb030m a"
            sSql += " WHERE a.bldno      = :bldno"
            sSql += "   AND a.comcd_out  = :comcd_out"
            sSql += "   AND a.tnsjubsuno = :tnsno"

            Return sSql
        End Function

        ' 혈액출고 테이블 입력
        Public Function fnGet_InsLB030MSql(Optional ByVal rsNCross As String = "") As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb030m"
            sSql += "          ( bldno, comcd_out, tnsjubsuno, testgbn, testid, testdt, rst1,  rst2,  rst3,  rst4,"
            sSql += "            cmrmk, emergency, ir,         filter,  comcd,  comnm,  regid, regip, regdt, editid, editip, editdt"


            If rsNCross.Length() > 0 Then
                sSql += ", outid, outdt, recid, recnm"
            End If

            sSql += "          )"
            sSql += "    VALUES( :bldno, :comcd_out, :tnsno, :testgbn, :testid, :testdt,"


            If rsNCross.Length() > 0 Then
                sSql += "NVL(:rst1, '-') , NVL(:rst2, '-'), NVL(:rst3, '-') , NVL(:rst4, '-'), NVL(:cmrmk, '응급출고'),"
            Else
                sSql += ":rst1, :rst2, :rst3, :rst4, :cmrmk,"
            End If

            sSql += ":eryn, :ir, :filter, :comcd, :comnm, :regid, :regip, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate"

            If rsNCross.Length() > 0 Then
                sSql += ",:outid, :outdt, :recid, :recnm"
            End If

            sSql += "          )"

            Return sSql
        End Function

        ' 혈액출고 업데이트
        Public Function fnGet_UpdLB030MSql(ByVal rsGbn As String, ByVal rsSubGbn As String) As String
            Dim sSql As String = ""

            sSql += "UPDATE lb030m                                         "

            If rsGbn = "P"c Then

                If rsSubGbn = "E"c Then
                    sSql += "   SET befoutid = :befoutid,"
                    sSql += "       befoutdt = :befoutdt,"
                Else
                    sSql += "   SET befoutid = NULL,"
                    sSql += "       befoutdt = NULL,"
                End If

            ElseIf rsGbn = "O"c Then

                If rsSubGbn = "E"c Then
                    sSql += "   SET outid    = :outid,"
                    sSql += "       outdt    = :outdt,"
                    sSql += "       recid    = :recid,"
                    sSql += "       recnm    = :recnm,"
                    sSql += "       keepgbn  = '0',"
                Else
                    sSql += "   SET outid    = NULL,"
                    sSql += "       outdt    = NULL,"
                    sSql += "       recid    = NULL,"
                    sSql += "       recnm    = NULL,"
                    sSql += "       keepgbn  = '0',"
                    sSql += "       keepid   = NULL,"
                    sSql += "       keeptm   = NULL,"
                End If
            ElseIf rsGbn = "C"c Then
                sSql += "   SET rst1     = :rst1,"
                sSql += "       rst2     = :rst2,"
                sSql += "       rst3     = :rst3,"
                sSql += "       rst4     = :rst4,"
                sSql += "       cmrmk    = :cmrmk,"
                sSql += "       testid   = :testid,"
                sSql += "       testdt   = :testdt,"
            ElseIf rsGbn = "K"c Then
                sSql += "   SET keepgbn    = :keepgbn,"
                sSql += "       keepid     = :keepid,"
                sSql += "       keeptm     = :keeptm,"

                If rsSubGbn = "2"c Then
                    sSql += "       outid    = :outid,"
                    sSql += "       outdt    = :outdt,"
                    sSql += "       recid    = :recid,"
                    sSql += "       recnm    = :recnm,"
                End If

            End If

            sSql += "       editid   = :editid,"
            sSql += "       editip   = :editip,"
            sSql += "       editdt   = fn_ack_sysdate"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcd_out"
            sSql += "   AND tnsjubsuno = :tnsno"

            Return sSql
        End Function


        ' 혈액출고 데이터 삭제
        Public Function fnGet_DelLB030MSql() As String
            Dim sSql As String = ""

            sSql += "DELETE lb030m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcd_out"
            sSql += "   and tnsjubsuno = :tnsno "

            Return sSql
        End Function

        ' 혈액반납/폐기 취소 입력
        Public Function fnGet_InsLB030MRtnCancelSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb030m"
            sSql += "          ( bldno,  comcd_out, tnsjubsuno, testgbn, testid,  testdt, rst1,   rst2,      rst3,  rst4,"
            sSql += "            cmrmk,  befoutid,  befoutdt,   outid,   outdt,   recid,  recnm,  emergency, ir,    filter,"
            sSql += "            comcd,  comnm,     filter_in,  ir_in,   keepgbn, keepid, keeptm, regid,     regip, regdt,"
            sSql += "            editid, editip,    editdt"
            sSql += "          )"
            sSql += "SELECT bldno,   comcd_out, tnsjubsuno, testgbn, testid, testdt, rst1,  rst2,      rst3,   rst4,"
            sSql += "       cmrmk,   befoutid,  befoutdt,   outid,   outdt,  recid,  recnm, emergency, ir,     filter,"
            sSql += "       comcd,   comnm,     filter_in,  ir_in,   '0',    '',     '',    :regid,    :regip, fn_ack_sysdate,"
            sSql += "       :editid, :edtip,    fn_ack_sysdate"
            sSql += "  FROM lb031m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcd_out"
            sSql += "   AND tnsjubsuno = :tnsno"

            Return sSql
        End Function

        ' 혈액반납/폐기 입력
        Public Function fnGet_InsLB031MSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb031m"
            sSql += "          ( bldno,    comcd_out, rtndt,     tnsjubsuno, testgbn,   testid, testdt, rst1,   rst2,   rst3,"
            sSql += "            rst4,     cmrmk,     befoutid,  befoutdt,   outid,     outdt,  recid,  recnm,  rtnid,  rtnreqid,"
            sSql += "            rtnreqnm, rtnrsncd,  rtnrsncmt, rtnflg,     emergency, ir,     filter, comcd,  comnm,  filter_in,"
            sSql += "            ir_in,    keepgbn,   keepid,    keeptm,     regid,     regip,  regdt,  editid, editip, editdt"
            sSql += "          ) "
            sSql += "SELECT bldno,     comcd_out, :rtndt,     tnsjubsuno, testgbn,   testid, testdt,         rst1,    rst2,    rst3,"
            sSql += "       rst4,      cmrmk,     befoutid,   befoutdt,   outid,     outdt,  recid,          recnm,   :rtnid,  :rtnreqid,"
            sSql += "       :rtnreqnm, :rtnrsncd, :rtnrsncmt, :rtnflg,    emergency, ir,     filter,         comcd,   comnm,   filter_in,"
            sSql += "       ir_in,     :keepgbn,  keepid,     keeptm,     :regid,    :regip, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate"
            sSql += "  FROM lb030m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcd_out"
            sSql += "   AND tnsjubsuno = :tnsno"

            Return sSql
        End Function

        ' 혈액 반납/폐기 히스토리 입력
        Public Function fnGet_InsLB031HSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb031h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
            sSql += "  FROM lb031m a"
            sSql += " WHERE a.bldno      = :bldno"
            sSql += "   AND a.comcd_out  = :comcd_out"
            sSql += "   and a.tnsjubsuno = :tnsno"

            Return sSql
        End Function


        Public Function fnGet_DelLB031mSql() As String
            Dim sSql As String = ""

            sSql += "DELETE lb031m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcd_out"
            sSql += "   and tnsjubsuno = :tnsno"

            Return sSql
        End Function

        Public Function fnGet_InsLB031MSelfSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb031m("
            sSql += "            bldno,    comcd_out, rtndt,     tnsjubsuno, testgbn,   testid, testdt, rst1,   rst2,   rst3,"
            sSql += "            rst4,     cmrmk,     befoutid,  befoutdt,   outid,     outdt,  recid,  recnm,  rtnid,  rtnreqid,"
            sSql += "            rtnreqnm, rtnrsncd,  rtnrsncmt, rtnflg,     emergency, ir,     filter, comcd,  comnm,  filter_in,"
            sSql += "            ir_in,    keepgbn,   keepid,    keeptm,     regid,     regip,  regdt,  editid, editip, editdt"
            sSql += "          )"
            sSql += "    VALUES( :bldno,    :comcd_out, :rtndt,     '',       '',     '',     '',             '',      '',      '',"
            sSql += "            '',        '',         '',         '',       '',     '',     '',             '',      :rtnid,  :rtnreqid,"
            sSql += "            :rtnreqnm, :rtnrsncd,  :rtnrsncmt, :rtnflg,  '',     '',     '',             :comcd,  '',      '',"
            sSql += "            '',        :keepgbn,   :keepid,    '',       :regid, :regip, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate"
            sSql += "          )"

            Return sSql
        End Function


        ' 수혈의뢰수량 변경
        Public Function fnGet_UpdLB042MStateSql(ByVal rsJobGbn As String) As String
            Dim sSql As String = ""

            Select Case rsJobGbn
                Case "가출고"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET befoutqnt  = befoutqnt + 1,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"

                Case "출고"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET befoutqnt  = CASE WHEN befoutqnt <> 0 THEN befoutqnt - 1 END,"
                    sSql += "       outqnt     = outqnt + 1,"
                    sSql += "       state      = CASE WHEN reqqnt - (outqnt + 1) - rtnqnt - abnqnt - cancelqnt = 0 THEN '1' ELSE '0' END,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"

                Case "응급출고" '<<<20180511 응급출고 추가 
                    sSql += "UPDATE lb042m"
                    sSql += "   SET befoutqnt  = CASE WHEN befoutqnt <> 0 THEN befoutqnt - 1 END,"
                    sSql += "       outqnt     = outqnt + 1,"
                    sSql += "       state      = CASE WHEN reqqnt - (outqnt + 1) - rtnqnt - abnqnt - cancelqnt = 0 THEN '1' ELSE '0' END,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"


                Case "반납"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET outqnt     = outqnt - 1,"
                    sSql += "       rtnqnt     = rtnqnt + 1,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"

                Case "폐기"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET outqnt     = outqnt - 1,"
                    sSql += "       abnqnt     = abnqnt + 1,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"

                Case "가출고취소"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET befoutqnt = befoutqnt - 1,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"

                Case "출고취소"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET befoutqnt  = CASE WHEN befoutqnt <> 0 THEN befoutqnt + 1 END,"
                    sSql += "       outqnt     = outqnt - 1,"
                    sSql += "       state      = CASE WHEN reqqnt - (outqnt - 1) - rtnqnt - abnqnt - cancelqnt = 0 THEN '1' ELSE '0' END,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"

                Case "반납취소"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET outqnt     = outqnt + 1,"
                    sSql += "       rtnqnt     = rtnqnt - 1,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"

                Case "폐기취소"
                    sSql += "UPDATE lb042m"
                    sSql += "   SET outqnt     = outqnt + 1,"
                    sSql += "       abnqnt     = abnqnt - 1,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    'sSql += "   AND comcd      = :comcd"

            End Select

            Return sSql
        End Function

        ' 수혈의뢰세부 히스토리 입력
        Public Function fnGet_InsLB043HSql() As String
            Dim sSql As String = ""

            sSql += "INSERT INTO lb043h "
            sSql += "SELECT fn_ack_sysdate, :modid, :modip, a.*"
            sSql += "  FROM lb043m a"
            sSql += " WHERE tnsjubsuno = :tnsno"
            sSql += "   AND comcd      = :comcd"
            sSql += "   AND iogbn      = :iogbn"
            sSql += "   AND fkocs      = :fkocs"

            Return sSql
        End Function

        ' 수혈의뢰세부데이터 상태변경
        Public Function fnGet_UpdLB043MStateSql() As String
            Dim sSql As String = ""

            sSql += "UPDATE lb043m"
            sSql += "   SET state    = :state,"
            sSql += "       abo      = CASE WHEN NVL(abo, ' ') = ' ' THEN :abo ELSE abo END,"
            sSql += "       rh       = CASE WHEN NVL(rh , ' ') = ' ' THEN :rh ELSE rh END,"
            sSql += "       ocsapply = :ocscost,"
            sSql += "       editid   = :editid,"
            sSql += "       editip   = :editip,"
            sSql += "       editdt   = fn_ack_sysdate"
            sSql += " WHERE tnsjubsuno = :tnsno"
            sSql += "   AND comcd      = :comcd"
            sSql += "   AND iogbn      = :Iogbn"
            sSql += "   AND fkocs      = :fkocs"

            Return sSql
        End Function

        ' 수혈의뢰세부데이터 혈액번호 업데이트 및 성분제제 변경 크로스 매칭 검사결과저장시
        Public Function fnGet_UpdLB043MBCSSql() As String
            Dim sSql As String = ""

            sSql += "UPDATE lb043m"
            sSql += "   SET state        = :state,"
            sSql += "       comcd_out    = :comcd_out,"
            sSql += "       bldno        = :bldno,"
            sSql += "       editid       = :editid,"
            sSql += "       editip       = :editip,"
            sSql += "       editdt       = fn_ack_sysdate"
            sSql += " WHERE tnsjubsuno   = :tnsno"
            sSql += "   AND comcd        = :comcd"
            sSql += "   AND iogbn        = :iogbn"
            sSql += "   AND fkocs        = (SELECT MIN(fkocs)"
            sSql += "                         FROM lb043m"
            sSql += "                        WHERE tnsjubsuno = :tnsno"
            sSql += "                          AND comcd      = :comcd"
            sSql += "                          AND iogbn      = :iogbn"
            sSql += "                          AND state      = '1'"
            sSql += "                      )"

            Return sSql
        End Function

        ' 의뢰검체, 보관검체 업데이트
        Public Function fnGet_UpdBcnoBlood(ByVal rsGbn As String) As String
            Dim sSql As String = ""

            sSql += "UPDATE lb040m               "

            If rsGbn = "UPD" Then
                sSql += "   SET bcno_order = :bcno_o"
                sSql += "     , bcno_keep  = :bcno_k"
            ElseIf rsGbn = "ORDER" Then
                sSql += "   SET bcno_order = :bcno"
            ElseIf rsGbn = "KEEP" Then
                sSql += "   SET bcno_keep = :bcno"
            End If

            sSql += "     , editid     = :editid"
            sSql += "     , editip     = :editip"
            sSql += "     , editdt     = fn_ack_sysdate "
            sSql += " WHERE tnsjubsuno = :tnsno"

            Return sSql
        End Function
        '20171123 전재휘 추가.
        Public Function fn_INSERT_SLBOUTT() As String
            Dim sSql As String = ""

            sSql += "           INSERT INTO SLXBOUTT "
            sSql += "  ("
            sSql += "  IODATE,     BLOODNO,        BLDCODE, BLDTYPE, BLOODYN,  IRRADYN,  FILTYN,  IOTYPE1, IOTYPE2, IOFROM, "
            sSql += "  IOTO,       IOQTY,          RETCODE, SRDATE,  PATNO,    ORDSEQNO, ORDDATE, RECNAME, USERID,  PROCDATE,"
            sSql += "  INSPUSERID, EXECPRCPUNIQNO, IOFLAG"
            sSql += "  )"
            sSql += "  VALUES( TO_DATE(:outdt, 'yyyymmdd'), :bldno, :comordcd, :bldtype, 'B', :ir, :filter, '2', :iogbn, 'LAB', :dptward,"
            sSql += "  1,     NULL, :meddate, :regno, :ordseq, TO_DATE(:orddt, 'yyyymmdd'), :recnm, :prcid, :prcdt, :testid,"
            sSql += "  :ordseq, :ioflag"
            sSql += "  )"


            Return sSql
        End Function

        ' 보관검체정보 입력
        Public Function fnGet_InsLB080M() As String
            Dim sSql As String = ""

            sSql += "insert                                         "
            sSql += "  into lb080m                                  "
            sSql += "     ( KEEPSPCNO                               "
            sSql += "     , REGNO                                   "
            sSql += "     , USTM                                    "
            sSql += "     , UETM                                    "
            sSql += "     , BLOODTYP                                "
            sSql += "     , ABO                                     "
            sSql += "     , RH                                      "
            sSql += "     , KEEPSPCBCNO                             "
            sSql += "     , REGID                                   "
            sSql += "     , REGIP                                   "
            sSql += "     , REGDT )                               "
            sSql += "values                                         "
            sSql += "     ( :keepspcno                                       "
            sSql += "     , :regno                                       "
            sSql += "     , :ustm     "
            sSql += "     , :uetm "
            sSql += "     , :bloodtyp                                      "
            sSql += "     , :abo                                       "
            sSql += "     , :rh                                       "
            sSql += "     , :keepbcno                                      "
            sSql += "     , '" + USER_INFO.USRID + "'               "
            sSql += "     , '" + USER_INFO.LOCALIP + "'             "
            sSql += "     , fn_ack_sysdate )                            "
            Return sSql
        End Function
    End Class

    '-- 혈액입고
    Public Class BldIn
        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.BldIn" + vbTab

        '-- 혈액원 혈액형코드로 혈액형 리턴
        Public Shared Function fnGet_BldCdToBType(ByVal rsBldCd As String) As DataTable
            Dim sFn As String = "fnGeg_BldcdToBtype(String) As DataTable"
            Dim sSql As String = ""

            Try
                sSql += "SELECT infofld1, infofld2"
                sSql += "  FROM lf122m"
                sSql += " WHERE infogbn = '1'"
                sSql += "   AND infocd  = :infocd"

                Dim alParm As New ArrayList

                alParm.Add(New OracleParameter("infocd", rsBldCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 입고 가능 성분제제 
        Public Shared Function fnGet_BldCdToComcd(ByVal rsBldCd As String) As DataTable
            Dim sFn As String = "fnGet_BldCdToComcd(String) as DataTable"
            Dim sSql As String = ""

            Try
                sSql += "SELECT comcd, donqnt, comnmd"
                sSql += "  FROM lf120M"
                sSql += " WHERE bldcd = :bldcd"
                sSql += "   AND usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                sSql += "   AND NVL(pscomcd, ' ')  = ' '"

                Dim alParm As New ArrayList

                alParm.Add(New OracleParameter("bldcd", rsBldCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 혈액입고 리스트 조회
        Public Shared Function fnGge_Bldno_List(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsComcd As String) As DataTable
            Dim sFn As String = "Function fnGge_Bldno_List(String, String) As DataTable"

            Try
                Dim sSql As String = ""

                '-- DONGBN = 0 외의 것( 일반,지정,성분 )은 헌혈
                sSql += "SELECT DISTINCT"
                sSql += "       FN_ACK_DATE_STR(a.indt, 'yyyy-mm-dd hh24:mi') indt,"
                sSql += "       FN_ACK_GET_BLDNO_FULL(a.bldno) bldno, a.inplace, a.abo, a.rh, a.donqnt,"
                sSql += "       FN_ACK_DATE_STR(a.dondt, 'yyyy-mm-dd') dondt, a.cmt,"
                sSql += "       FN_ACK_DATE_STR(a.availdt, 'yyyy-mm-dd') availdt,"
                sSql += "       a.comcd, b.comnmd, b.availmi,"
                sSql += "       CASE WHEN a.dongbn = '0' THEN '혈액원' WHEN a.dongbn = '1' THEN '헌혈'"
                sSql += "            WHEN a.dongbn = '2' THEN '지정'   WHEN a.dongbn = '3' THEN '성분'"
                sSql += "            WHEN a.dongbn = '4' THEN '자가'"
                sSql += "            ELSE ''"
                sSql += "       END  de_dongbn,"
                sSql += "       CASE WHEN a.state = '0' THEN CASE WHEN NVL(a.usedgbn,' ') = ' ' THEN '초입고' ELSE '재입고' END"
                sSql += "            WHEN a.state = '1' THEN '접수'"
                sSql += "            WHEN a.state = '2' THEN '검사중'"
                sSql += "            WHEN a.state = '3' THEN '가출고' "
                sSql += "            WHEN a.state = '4' THEN '출고'"
                sSql += "            WHEN a.state = '5' THEN '반납/교환'"
                sSql += "            WHEN a.state = '6' THEN '폐기'"
                sSql += "            WHEN a.state = '7' THEN '검사취소'"
                sSql += "            WHEN a.state = '8' THEN '가출고취소'"
                sSql += "            WHEN a.state = '9' THEN '출고취소'"
                sSql += "       END de_state,"
                sSql += "       CASE WHEN a.inplace = '0' THEN '" + PRG_CONST.HOSPITAL_NAME + " 혈액은행' ELSE '' END de_inplace,"
                sSql += "       a.abo || a.rh abo_rh, regno, FN_ACK_GET_USR_NAME(a.inid) usrnm"
                sSql += "  FROM lb020m a, lf120m b"
                sSql += " WHERE a.comcd = b.comcd"
                sSql += "   AND a.indt >= :dates || '0000'"
                sSql += "   AND a.indt <= :datee || '5959'"
                If rsComcd.Trim() <> "ALL" Then
                    sSql += "   AND a.comcd = :comcd "
                End If
                sSql += "   AND b.usdt <= a.indt"
                sSql += "   AND B.uedt >  a.indt"
                sSql += " ORDER BY a.abo, a.rh, a.comcd, dondt DESC"

                Dim alParm As New ArrayList
                alParm.Add(New OracleParameter("dates", rsDateS))
                alParm.Add(New OracleParameter("datee", rsDateE))
                If rsComcd.Trim() <> "ALL" Then
                    alParm.Add(New OracleParameter("comcd", rsComcd))
                End If
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        '-- 성분제제 리스트 조회
        Public Shared Function fnGet_Comcd_List(Optional ByVal rsQnt As String = "", Optional ByVal rsUsDt As String = "", _
                                                Optional ByVal r_al_CdList As ArrayList = Nothing) As DataTable

            Dim sFn As String = "fnGet_Comcd_List([String], [String], [ArrayList]) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                Dim sComCds As String = ""

                If r_al_CdList Is Nothing Then
                Else    ' 새로 뿌려주는 경우
                    For ix As Integer = 0 To r_al_CdList.Count - 1
                        If ix > 0 Then sComCds += ","
                        sComCds += r_al_CdList.Item(ix).ToString()
                    Next
                End If

                sSql += "SELECT comnmd, availmi, comcd, dispseql, bldcd" '20130821 정선영 수정
                sSql += "  FROM lf120m"
                sSql += " WHERE (donqnt IS NULL OR donqnt = :donqnt)"
                sSql += "   AND NVL(pscomcd, ' ') = ' '"

                alParm.Add(New OracleParameter("donqnt", rsQnt))

                If rsUsDt = "" Then
                    sSql += "   AND usdt    <= fn_ack_sysdate"
                    sSql += "   AND uedt    >  fn_ack_sysdate"
                Else
                    sSql += "   AND usdt    <= :usdt"
                    sSql += "   AND uedt    >  :usdt"

                    alParm.Add(New OracleParameter("usdt", rsUsDt))
                    alParm.Add(New OracleParameter("usdt", rsUsDt))

                End If

                sSql += "   AND NVL(ftcd, ' ') = ' '"   '-- 필터는 입고 하지 않는다.

                If sComCds <> "" Then
                    sSql += "   AND comcd NOT IN ('" + sComCds.Replace(",", "','") + "')"
                End If

                sSql += " GROUP BY comnmd, availmi, comcd, dispseql, bldcd" '20130821 정선영 수정
                sSql += " ORDER BY dispseql"

                DbCommand(False)
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 혈액정보
        Public Shared Function fnGet_BldNo_Info(ByVal rsBldNo As String, ByVal rsComCd As String) As DataTable
            Dim sFn As String = "fnGet_BldNo_Info(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       FN_ACK_DATE_STR(a.indt, 'yyyy-mm-dd') as indt,"
                sSql += "       FN_ACK_GET_BLDNO_FULL(a.bldno) bldno,  a.dongbn, a.inplace, a.abo, a.rh, a.donqnt,"
                sSql += "       FN_ACK_DATE_STR(a.dondt, 'yyyy-mm-dd') as dondt, a.cmt,"
                sSql += "       FN_ACK_DATE_STR(a.availdt, 'yyyy-mm-dd') as availdt,"
                sSql += "       a.comcd, a.state, b.comnmd, b.availmi,"
                sSql += "       CASE WHEN a.dongbn = '0' THEN '혈액원' ELSE '헌혈' END de_dongbn,"
                sSql += "       CASE WHEN a.state = '0' THEN CASE WHEN NVL(a.usedgbn,' ') = ' ' THEN '초입고' ELSE '재입고'END"
                sSql += "            WHEN a.state = '1' THEN '접수'"
                sSql += "            WHEN a.state = '2' THEN '검사중'"
                sSql += "            WHEN a.state = '3' THEN '가출고' "
                sSql += "            WHEN a.state = '4' THEN '출고'"
                sSql += "            WHEN a.state = '5' THEN '반납/교환'"
                sSql += "            WHEN a.state = '6' THEN '폐기'"
                sSql += "            WHEN a.state = '7' THEN '검사취소'"
                sSql += "            WHEN a.state = '8' THEN '가출고취소'"
                sSql += "            WHEN a.state = '9' THEN '출고취소'"
                sSql += "       END de_state,"
                sSql += "       CASE WHEN a.inplace = '0' THEN '" + PRG_CONST.HOSPITAL_NAME + " 혈액은행'"
                sSql += "            ELSE ''"
                sSql += "       END de_inplace,"
                sSql += "       a.abo || a.rh abo_rh, regno, FN_ACK_GET_USR_NAME(a.inid) usrnm"
                sSql += "  FROM lb020m a, lf120m b"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = b.comcd"
                sSql += "   AND a.indt >= b.usdt"
                sSql += "   AND a.indt <  b.uedt"

                alParm.Add(New OracleParameter("bldno", rsBldNo))

                If rsComCd <> "" Then
                    sSql += "   AND a.comcd = :comcd"
                    alParm.Add(New OracleParameter("comcd", rsComCd))

                End If

                sSql += " ORDER BY a.abo, a.rh, a.comcd, dondt DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 환자이름 리턴
        Public Shared Function fnGet_PatName(ByVal rsRegNo As String) As String

            Dim sFn As String = "Sub fnGet_PatName(string) As string"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = "SELECT dbo.fn_get_pat_name(:regno) patnm"

                alParm.Add(New OracleParameter("regno", rsRegNo))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("patnm").ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 혈액입고
        Public Shared Function fnExe_BldIn(ByRef r_o_bld As STU_BldInfo) As Boolean
            Dim sFn As String = "fnExe_BldIn(object) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sSrvTm As String = ""

                sSql += "SELECT FN_ACK_DATE_STR(fn_ack_sysdate, 'hh24:mi:ss') srvtime FROM DUAL"

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    r_o_bld.InDt += " " + dt.Rows(0).Item("srvtime").ToString()
                Else
                    r_o_bld.InDt += " " + Format(Now, "HH:mm:ss").ToString
                End If

                sSql = ""
                sSql += "INSERT INTO lb020m"
                sSql += "          ( bldno,  comcd,          indt,  inplace,  dongbn,  abo,    rh,     donqnt,         dondt,   availdt,"
                sSql += "            state,  statedt,        cmt,   regno,    inid,    regid,  regip,  regdt,          editid,  editip,   editdt)"
                sSql += "    values( :bldno, :comcd,         :indt, :inplace, :dongbn, :abo,   :rh,    :donqnt,        :dondt,  :availdt,"
                sSql += "            :state, fn_ack_sysdate, :cmt,  :regno,   :inid,   :regid, :regip, fn_ack_sysdate, :editid, :editip,  fn_ack_sysdate)"


                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bldno",  OracleDbType.Varchar2).Value = r_o_bld.BldNo
                    .Parameters.Add("comcd",  OracleDbType.Varchar2).Value = r_o_bld.ComCd
                    .Parameters.Add("indt",  OracleDbType.Varchar2).Value = r_o_bld.InDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                    .Parameters.Add("inplace",  OracleDbType.Varchar2).Value = r_o_bld.InPlace
                    .Parameters.Add("dongbn",  OracleDbType.Varchar2).Value = r_o_bld.DonGbn
                    .Parameters.Add("abo",  OracleDbType.Varchar2).Value = r_o_bld.Abo
                    .Parameters.Add("rh",  OracleDbType.Varchar2).Value = r_o_bld.Rh
                    .Parameters.Add("donqnt",  OracleDbType.Varchar2).Value = r_o_bld.DonQnt
                    .Parameters.Add("dondt",  OracleDbType.Varchar2).Value = r_o_bld.DonDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                    .Parameters.Add("availdt",  OracleDbType.Varchar2).Value = r_o_bld.AvailDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                    .Parameters.Add("state",  OracleDbType.Varchar2).Value = "0"
                    .Parameters.Add("cmt",  OracleDbType.Varchar2).Value = r_o_bld.Cmt
                    .Parameters.Add("regno",  OracleDbType.Varchar2).Value = r_o_bld.RegNo
                    .Parameters.Add("inid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("regip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    iRet = .ExecuteNonQuery()

                End With

                If iRet > 0 Then
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
                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        '-- 혈액입고
        Public Shared Function fnExe_BldIn(ByRef r_al_bldInfo As ArrayList) As Boolean
            Dim sFn As String = "fnExe_BldIn(object) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New OracleCommand
                Dim dbDa As OracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sSrvDt As String = ""

                sSql += "SELECT FN_ACK_DATE_STR(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') srvdt FROM DUAL"

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(dbCmd)

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count > 0 Then
                    sSrvDt = dt.Rows(0).Item("srvdt").ToString()
                Else
                    sSrvDt = Format(Now, "yyyy-MM-dd HH:mm:ss").ToString
                End If

                For ix As Integer = 0 To r_al_bldInfo.Count - 1
                    If CType(r_al_bldInfo(ix), STU_BldInfo).InDt = "" Then
                        CType(r_al_bldInfo(ix), STU_BldInfo).InDt = sSrvDt
                        'Else
                        '    CType(r_al_bldInfo(ix), STU_BldInfo).InDt += sSrvDt.Substring(10)
                    End If

                    Dim stuBldIn As STU_BldInfo = CType(r_al_bldInfo(ix), STU_BldInfo)

                    sSql = ""
                    sSql += "INSERT INTO lb020m"
                    sSql += "          ( bldno,  comcd,          indt,  inplace,  dongbn,  abo,    rh,     donqnt,         dondt,   availdt,"
                    sSql += "            state,  statedt,        cmt,   regno,    inid,    regid,  regip,  regdt,          editid,  editip,   editdt)"
                    sSql += "    values( :bldno, :comcd,         :indt, :inplace, :dongbn, :abo,   :rh,    :donqnt,        :dondt,  :availdt,"
                    sSql += "            :state, fn_ack_sysdate, :cmt,  :regno,   :inid,   :regid, :regip, fn_ack_sysdate, :editid, :editip,  fn_ack_sysdate)"


                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bldno",  OracleDbType.Varchar2).Value = stuBldIn.BldNo
                        .Parameters.Add("comcd",  OracleDbType.Varchar2).Value = stuBldIn.ComCd
                        .Parameters.Add("indt",  OracleDbType.Varchar2).Value = stuBldIn.InDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                        .Parameters.Add("inplace",  OracleDbType.Varchar2).Value = stuBldIn.InPlace
                        .Parameters.Add("dongbn",  OracleDbType.Varchar2).Value = stuBldIn.DonGbn
                        .Parameters.Add("abo",  OracleDbType.Varchar2).Value = stuBldIn.Abo
                        .Parameters.Add("rh",  OracleDbType.Varchar2).Value = stuBldIn.Rh
                        .Parameters.Add("donqnt",  OracleDbType.Varchar2).Value = stuBldIn.DonQnt
                        .Parameters.Add("dondt",  OracleDbType.Varchar2).Value = stuBldIn.DonDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                        .Parameters.Add("availdt",  OracleDbType.Varchar2).Value = stuBldIn.AvailDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                        .Parameters.Add("state",  OracleDbType.Varchar2).Value = "0"
                        .Parameters.Add("cmt",  OracleDbType.Varchar2).Value = stuBldIn.Cmt
                        .Parameters.Add("regno",  OracleDbType.Varchar2).Value = stuBldIn.RegNo
                        .Parameters.Add("inid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("regip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        .Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        .Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        iRet += .ExecuteNonQuery()

                    End With
                Next

                If iRet > 0 Then
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
                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        '-- 혈액입고 삭제
        Public Shared Function fnExe_BldIn_Del(ByVal rsBldNo As String, ByVal rsComCd As String) As Boolean      ' 기존 입고된 성분제제 삭제하기
            Dim sFn As String = "Function fnExe_BldIn_Del() As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New OracleCommand

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                ' back up 테이블에 history 남기기!!
                sSql = ""
                sSql += "INSERT INTO lb020h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, lb2.* FROM lb020m lb2"
                sSql += " WHERE lb2.bldno = :bldno"
                sSql += "   AND lb2.comcd = :comcd"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bldno",  OracleDbType.Varchar2).Value = rsBldNo
                    .Parameters.Add("comcd",  OracleDbType.Varchar2).Value = rsComCd

                    iRet = .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "DELETE lb020m"
                sSql += " WHERE bldno = :bldno"
                sSql += "   AND comcd = :comcd"
                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bldno",  OracleDbType.Varchar2).Value = rsBldNo
                    .Parameters.Add("comcd",  OracleDbType.Varchar2).Value = rsComCd

                    iRet = .ExecuteNonQuery()
                End With

                If iRet > 0 Then
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
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        '-- 초입고 상태로...
        Public Shared Function fnExe_BldIn_Change(ByVal rsBldNo As String, ByVal rsComCd As String) As Boolean      ' 재입고된 성분제제 초입고로 변경하기
            Dim sFn As String = "Function fnExe_BldIn_Change() As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTran As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim dbCmd As New OracleCommand

                ' back up 테이블에 history 남기기!!
                sSql = ""
                sSql += "INSERT INTO lb020h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, lb2.* FROM lb020m lb2"
                sSql += " WHERE lb2.bldno = :bldno"
                sSql += "   AND lb2.comcd = :comcd"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTran
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    .Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bldno",  OracleDbType.Varchar2).Value = rsBldNo
                    .Parameters.Add("comcd",  OracleDbType.Varchar2).Value = rsComCd

                    iRet = .ExecuteNonQuery()

                End With

                sSql = ""
                sSql += "UPDATE lb020m SET usedgbn = NULL"
                sSql += " WHERE bldno = :bldno"
                sSql += "   AND comcd = :comcd"
                With dbCmd
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bldno",  OracleDbType.Varchar2).Value = rsBldNo
                    .Parameters.Add("comcd",  OracleDbType.Varchar2).Value = rsComCd

                    iRet = .ExecuteNonQuery()

                End With

                If iRet > 0 Then
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
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

    End Class

    '-- 수혈의뢰 접수
    Public Class JubSu
        Inherits SqlFn

        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.JubSu" + vbTab
        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Public Sub New()
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Private Function fnGet_Sysdate() As String
            Dim sFn As String = "Private Function fnGet_Sysdate() As String"
            Dim DbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT fn_ack_sysdate  FROM DUAL"

                DbCmd.Connection = m_dbCn
                DbCmd.Transaction = m_dbTran
                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(DbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                Else
                    Return dt.Rows(0).Item(0).ToString
                End If
            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmmss").ToString

            End Try

        End Function



        Private Function fnGet_TnsNum(ByVal rsJusuDt As String) As String
            ' 수혈의뢰접수 번호 생성
            Dim sFn As String = "Public Shared Function fnGet_TnsNum() As String"
            Dim DbCmd As New OracleCommand

            Try
                Dim iTnsNo As Integer = 0

                With DbCmd
                    DbCmd.Connection = m_DbCn
                    DbCmd.Transaction = m_dbTran
                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "pro_ack_exe_seqno_tns"

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("rs_seqymd",  OracleDbType.Varchar2, rsJusuDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsJusuDt))

                    .Parameters.Add("rn_seqno", OracleDbType.Int32)
                    .Parameters("rn_seqno").Direction = ParameterDirection.InputOutput
                    .Parameters("rn_seqno").Value = -1

                    .ExecuteNonQuery()

                    iTnsNo = CType(.Parameters(1).Value.ToString.ToString, Integer)
                End With

                If iTnsNo > 0 Then
                    Return iTnsNo.ToString.PadLeft(4, "0"c)
                Else
                    Return ""
                End If
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        Public Function fn_RegTnsJubsuData(ByVal ralArg As ArrayList) As Boolean
            ' 수혈의뢰 접수 처리
            Dim sFn As String = "Public Function fn_RegTnsJubsu(ByVal ral As ArrayList) As Boolean"
            Dim DbCmd As New OracleCommand

            Dim sSrvDate As String = fnGet_Sysdate()
            Dim sTnsNo As String = ""

            Dim sSql As String = ""
            Dim iRet As Integer

            With DbCmd
                .Connection = m_dbCn
                .Transaction = m_dbTran
            End With

            Try
                Dim iQtyCnt As Integer = 0
                ' 수혈의뢰 접수 번호 생성
                sSrvDate = fnGet_Sysdate()
                sTnsNo = fnGet_TnsNum(sSrvDate.Substring(0, 8))

                If sTnsNo = "" Then
                    m_dbTran.Rollback()
                    Return False
                End If

                sTnsNo = sSrvDate.Substring(0, 8) + "T"c + sTnsNo.PadLeft(4, "0"c)
                iQtyCnt = 0

                For ix As Integer = 0 To ralArg.Count - 1

                    sSql = "PRO_ACK_EXE_OCS_TNS"

                    DbCmd.Transaction = m_dbTran
                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).REGNO
                    DbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).OWNGBN
                    DbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).FKOCS
                    DbCmd.Parameters.Add("rs_acptdt",  OracleDbType.Varchar2).Value = sSrvDate
                    DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("ri_retval", OracleDbType.Int32)
                    DbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    DbCmd.Parameters("ri_retval").Value = -1

                    DbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    DbCmd.Parameters("rs_retval").Size = 2000
                    DbCmd.Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    DbCmd.Parameters("rs_retval").Value = -1

                    DbCmd.ExecuteNonQuery()

                    iRet = CType(DbCmd.Parameters(6).Value.ToString.ToString, Integer)

                    Dim sRet As String = DbCmd.Parameters(7).Value.ToString

                    If iRet < 1 Then
                        Throw (New Exception("처방정보 입력시 오류가 발생했습니다.!!"))
                    End If

                    ' 수혈의뢰세부내역

                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "PRO_ACK_EXE_TNS_LB043M"

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).REGNO
                    DbCmd.Parameters.Add("rs_orddt",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).ORDDATE.Substring(0, 8)

                    If CType(ralArg(0), STU_TnsJubsu).OWNGBN = "L" Then
                        DbCmd.Parameters.Add("rs_ordno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).FKOCS
                        DbCmd.Parameters.Add("rs_ioflag",  OracleDbType.Varchar2).Value = ""
                    Else
                        DbCmd.Parameters.Add("rs_ordno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).FKOCS.Split("/"c)(3)
                        DbCmd.Parameters.Add("rs_ioflag",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).FKOCS.Split("/"c)(0)
                    End If

                    DbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).OWNGBN
                    DbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = sTnsNo
                    DbCmd.Parameters.Add("rn_seq", OracleDbType.Int32).Value = ix
                    DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    DbCmd.Parameters("rs_retval").Size = 2000
                    DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    DbCmd.Parameters("rs_retval").Value = ""

                    DbCmd.ExecuteNonQuery()

                    'Dim sMsgErr As String = .Parameters(12).Value.TOSTRING

                    If DbCmd.Parameters(9).Value.ToString = "00" Then
                    Else
                        Throw (New Exception(DbCmd.Parameters(9).Value.ToString.Substring(3)))
                    End If

                Next

                DbCmd.CommandType = CommandType.StoredProcedure
                DbCmd.CommandText = "PRO_ACK_EXE_TNS_LB040M"

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).REGNO
                DbCmd.Parameters.Add("rs_orddt",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).ORDDATE.Substring(0, 8)

                If CType(ralArg(0), STU_TnsJubsu).OWNGBN = "L" Then
                    DbCmd.Parameters.Add("rs_ordno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).FKOCS
                    DbCmd.Parameters.Add("rs_ioflag",  OracleDbType.Varchar2).Value = ""
                Else
                    DbCmd.Parameters.Add("rs_ordno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).FKOCS.Split("/"c)(3)
                    DbCmd.Parameters.Add("rs_ioflag",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).FKOCS.Split("/"c)(0)
                End If

                DbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).OWNGBN
                DbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = sTnsNo
                DbCmd.Parameters.Add("rs_jubsudt",  OracleDbType.Varchar2).Value = sSrvDate
                DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                DbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                DbCmd.Parameters("rs_retval").Size = 2000
                DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                DbCmd.Parameters("rs_retval").Value = ""

                DbCmd.ExecuteNonQuery()

                'Dim sMsgErr As String = .Parameters(12).Value.TOString

                If DbCmd.Parameters(9).Value.ToString = "00" Then
                Else
                    Throw (New Exception(DbCmd.Parameters(9).Value.ToString.Substring(3)))
                End If

                ' 수혈의뢰내역(LB042M)
                DbCmd.CommandType = CommandType.StoredProcedure
                DbCmd.CommandText = "PRO_ACK_EXE_TNS_LB042M"

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).REGNO
                DbCmd.Parameters.Add("rs_orddt",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).ORDDATE.Substring(0, 8)

                If CType(ralArg(0), STU_TnsJubsu).OWNGBN = "L" Then
                    DbCmd.Parameters.Add("rs_ordno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).FKOCS
                    DbCmd.Parameters.Add("rs_ioflag",  OracleDbType.Varchar2).Value = ""
                Else
                    DbCmd.Parameters.Add("rs_ordno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).FKOCS.Split("/"c)(3)
                    DbCmd.Parameters.Add("rs_ioflag",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).FKOCS.Split("/"c)(0)
                End If

                DbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).OWNGBN
                DbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = sTnsNo
                DbCmd.Parameters.Add("rn_reqqnt", OracleDbType.Int32).Value = ralArg.Count
                DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                DbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                DbCmd.Parameters("rs_retval").Size = 2000
                DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                DbCmd.Parameters("rs_retval").Value = ""

                DbCmd.ExecuteNonQuery()

                'Dim sMsgErr As String = .Parameters(12).Value.TOString

                If DbCmd.Parameters(9).Value.ToString = "00" Then
                Else
                    Throw (New Exception(DbCmd.Parameters(9).Value.ToString.Substring(3)))
                End If

                sSql = ""
                sSql += "UPDATE lb043m SET state = state"
                sSql += " WHERE fkocs  IN (SELECT b.fkocs FROM lb040m a, lb043m b"
                sSql += "                   WHERE b.fkocs            = :fkocs"
                sSql += "                     AND a.tnsjubsuno       = b.tnsjubsuno"
                sSql += "                     AND b.tnsjubsuno      <> :tnsno"
                sSql += "                     AND NVL(b.state, '0') <> '0'"
                sSql += "                 )"
                sSql += "   AND tnsjubsuno <> :tnsno"

                DbCmd.Transaction = m_dbTran
                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("fkocs",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).FKOCS
                DbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = sTnsNo

                iRet = DbCmd.ExecuteNonQuery()
                If iRet > 0 Then
                    Throw (New Exception("이미 접수된 자료입니다.!! @" + msFile + sFn))
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

        Public Function fn_CntTnsJubsuData(ByVal ralArg As ArrayList) As Boolean
            '수혈 접수 취소
            Dim sFn As String = "Public Function fn_CntTnsJubsuData(ByVal ral As ArrayList) As Boolean"
            Dim dbCmd As New OracleCommand
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            With dbCmd
                .Connection = m_dbCn
                .Transaction = m_dbTran
            End With

            Try
                Dim sSvrdate As String = fnGet_Sysdate()

                For ix As Integer = 0 To ralArg.Count - 1

                    ' 처방 상태값 변경
                    dbCmd.Transaction = m_dbTran
                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_ocs_tns_cancel"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).REGNO
                    dbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("ri_retval", OracleDbType.Int32)
                    dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    dbCmd.Parameters("ri_retval").Value = -1

                    dbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    dbCmd.Parameters("rs_retval").Size = 2000
                    dbCmd.Parameters("rs_retval").Direction = ParameterDirection.InputOutput
                    dbCmd.Parameters("rs_retval").Value = -1

                    dbCmd.ExecuteNonQuery()

                    Dim sRet As String = dbCmd.Parameters(6).Value.ToString
                    iRet = CType(dbCmd.Parameters(5).Value.ToString.ToString, Integer)

                    If iRet < 1 Then
                        Throw (New Exception("처방 자료 수정시 오류가 발생했습니다.!!"))
                    End If

                    ' 수혈의뢰 세부 히스토리 입력
                    sSql = ""
                    sSql = fnGet_InsLB043HSql()

                    dbCmd.Transaction = m_dbTran
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).COMCD
                    dbCmd.Parameters.Add("iogbn",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).IOGBN
                    dbCmd.Parameters.Add("fkocs",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        Throw (New Exception("수혈상세내역(LB043M) 취소시 오류가 발생했습니다.!!"))
                    End If

                    ' 수혈의뢰 세부 상태값 업데이트
                    sSql = ""
                    sSql = fnGet_UpdLB043MStateSql()

                    dbCmd.Transaction = m_dbTran
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("state",  OracleDbType.Varchar2).Value = "0"c
                    dbCmd.Parameters.Add("abo",  OracleDbType.Varchar2).Value = ""
                    dbCmd.Parameters.Add("rh",  OracleDbType.Varchar2).Value = ""
                    dbCmd.Parameters.Add("ocscost",  OracleDbType.Varchar2).Value = ""
                    dbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).COMCD
                    dbCmd.Parameters.Add("iogbn",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).IOGBN
                    dbCmd.Parameters.Add("fkocs",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        Throw (New Exception("수혈상세내역(LB043M) 취소시 오류가 발생했습니다.!!"))
                    End If

                    ' 수혈의뢰수량 업데이트
                    sSql = ""
                    sSql += "UPDATE lb042m"
                    sSql += "   SET cancelqnt  = cancelqnt + 1,"
                    If CType(ralArg(ix), STU_TnsJubsu).STATE = "3"c Then
                        sSql += "       befoutqnt = befoutqnt - 1,"
                        sSql += "       state      = CASE WHEN reqqnt = (befoutqnt - 1) + outqnt + rtnqnt + abnqnt + cancelqnt + 1 THEN '1' ELSE '0' END,"
                    Else
                        sSql += "       state      = CASE WHEN reqqnt = befoutqnt + outqnt + rtnqnt + abnqnt + cancelqnt + 1 THEN '1' ELSE '0' END,"
                    End If

                    sSql += "       delflg     = CASE WHEN reqqnt = cancelqnt + 1 THEN '1' ELSE '0' END,"
                    sSql += "       editid     = :editid,"
                    sSql += "       editip     = :editip,"
                    sSql += "       editdt     = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    'sSql += "   AND comcd      = :comcd"

                    dbCmd.Transaction = m_dbTran
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).TNSJUBSUNO
                    'dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).COMCD

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        Throw (New Exception("수혈의뢰내역(LB042M) 취소시 오류가 발생했습니다.!!"))
                    End If

                    ' 가출고인 경우 혈액의 상태 변경이 필요
                    If CType(ralArg(ix), STU_TnsJubsu).STATE >= "2"c Then
                        ' lb031m insert
                        sSql = ""
                        sSql = fnGet_InsLB031MSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("rtndt",  OracleDbType.Varchar2).Value = sSvrdate
                        dbCmd.Parameters.Add("rtnid",  OracleDbType.Varchar2).Value = ""
                        dbCmd.Parameters.Add("rtnreqid",  OracleDbType.Varchar2).Value = ""
                        dbCmd.Parameters.Add("rtnreqnm",  OracleDbType.Varchar2).Value = ""
                        dbCmd.Parameters.Add("rtnrsncd",  OracleDbType.Varchar2).Value = ""
                        dbCmd.Parameters.Add("rtnrsncmt",  OracleDbType.Varchar2).Value = ""

                        dbCmd.Parameters.Add("rtnflg  ",  OracleDbType.Varchar2).Value = "0"c

                        If CType(ralArg(ix), STU_TnsJubsu).STATE = "2" Then
                            dbCmd.Parameters.Add("keepgbn",  OracleDbType.Varchar2).Value = "1"c
                        Else
                            dbCmd.Parameters.Add("keepgbn",  OracleDbType.Varchar2).Value = "2"c
                        End If

                        dbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("regip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd_out",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            Throw (New Exception("수혈출고내역(LB031M) 취소시 오류가 발생했습니다.!!"))
                        End If

                        ' lb030m delete  
                        sSql = fnGet_DelLB030MSql()
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            Throw (New Exception("수혈출고내역(LB031M) 취소시 오류가 발생했습니다.!!"))
                        End If

                        ' 혈액 히스토리 정보 추가 ( 가출고정보 ) 
                        sSql = ""
                        sSql = fnGet_InsLB020HSql()

                        dbCmd.Transaction = m_dbTran
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            Throw (New Exception("혈액입고(LB020M) 처리시 오류가 발생했습니다.!!"))
                        End If

                        ' 혈액 입고 상태로 변경
                        sSql = ""
                        sSql = fnGet_UpdLB020MStateSql()

                        dbCmd.Transaction = m_dbTran
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("state",  OracleDbType.Varchar2).Value = "0"c
                        dbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(ralArg(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            Throw (New Exception("혈액입고(LB020M) 처리시 오류가 발생했습니다.!!"))
                        End If
                    End If
                Next

                ' 수혈의뢰정보 마스터(lb040m)
                sSql = ""
                sSql += "UPDATE lb040m SET"
                sSql += "       delflg   = (SELECT delflg FROM lb042m WHERE tnsjubsuno = :tnsno),"
                sSql += "       editid   = :editid,"
                sSql += "       editip   = :editip,"
                sSql += "       editdt   = fn_ack_sysdate"
                sSql += " WHERE tnsjubsuno = :tnsno"

                dbCmd.Transaction = m_dbTran
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).TNSJUBSUNO
                dbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(ralArg(0), STU_TnsJubsu).TNSJUBSUNO

                iRet = dbCmd.ExecuteNonQuery()

                If iRet = 0 Then
                    Throw (New Exception("수혈접수내역(LB040M) 취소시 오류가 발생했습니다.!!"))
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

    End Class

    '-- 가출고
    Public Class BefOut
        Inherits SqlFn

        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.Bef" + vbTab
        Private m_DbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_dbTran = m_DbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

        End Sub

        Private Function fnGet_Sysdate() As String
            Dim sFn As String = "Private Function fnGet_Sysdate() As String"
            Dim DbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT fn_ack_sysdate FROM DUAL"

                DbCmd.Connection = m_DbCn
                DbCmd.Transaction = m_dbTran
                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(DbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                Else
                    Return dt.Rows(0).Item(0).ToString
                End If
            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmmss").ToString

            End Try

        End Function

        ' 크로스매칭 적용
        Public Function fnExe_CrossApply(ByVal r_al_rst As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnExe_CrossSave( ArrayList) As Boolean"

            Dim DbCmd As New OracleCommand

            Try

                With DbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                For ix As Integer = 0 To r_al_rst.Count - 1

                    sSql = "PRO_ACK_EXE_TNS_CROSS_APPLY"

                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).BLDNO
                    DbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).TNSJUBSUNO
                    DbCmd.Parameters.Add("rs_comcd",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD
                    DbCmd.Parameters.Add("rs_comnm",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMNM
                    DbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).OWNGBN
                    DbCmd.Parameters.Add("rs_eryn",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).EMER
                    DbCmd.Parameters.Add("rs_ir",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).IR
                    DbCmd.Parameters.Add("rs_filter",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).FILTER
                    DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    DbCmd.Parameters("rs_retval").Size = 2000
                    DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    DbCmd.Parameters("rs_retval").Value = ""

                    DbCmd.ExecuteNonQuery()

                    'Dim sMsgErr As String = .Parameters(12).Value.TOString

                    If DbCmd.Parameters(11).Value.ToString <> "00" Then
                        m_dbTran.Rollback()
                        Throw (New Exception(DbCmd.Parameters(11).Value.ToString.Substring(2)))
                    End If
                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        ' 크로스매칭 결과저장
        Public Function fnExe_CrossSave(ByVal r_al_rst As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnExe_CrossSave( ArrayList) As Boolean"

            Dim DbCmd As New OracleCommand

            Try

                With DbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sTestdt As String = fnGet_Sysdate()

                For ix As Integer = 0 To r_al_rst.Count - 1

                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "PRO_ACK_EXE_TNS_CROSS_SAVE"

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).BLDNO
                    DbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).TNSJUBSUNO
                    DbCmd.Parameters.Add("rs_comcd",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD
                    DbCmd.Parameters.Add("rs_comnm",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMNM
                    DbCmd.Parameters.Add("rs_rst1",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).RST1
                    DbCmd.Parameters.Add("rs_rst2",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).RST2
                    DbCmd.Parameters.Add("rs_rst3",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).RST3
                    DbCmd.Parameters.Add("rs_rst4",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).RST4
                    DbCmd.Parameters.Add("rs_cmrmk",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).CMRMK
                    DbCmd.Parameters.Add("rs_eryn",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).EMER
                    DbCmd.Parameters.Add("rs_ir",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).IR
                    DbCmd.Parameters.Add("rs_filter",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).FILTER
                    DbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    DbCmd.Parameters("rs_retval").Size = 2000
                    DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    DbCmd.Parameters("rs_retval").Value = ""

                    DbCmd.ExecuteNonQuery()

                    'Dim sMsgErr As String =.Parameters(12).Value.TOString

                    If DbCmd.Parameters(16).Value.ToString <> "00" Then
                        m_dbTran.Rollback()
                        Throw (New Exception(DbCmd.Parameters(16).Value.ToString.Substring(2)))
                    End If

                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        ' 크로스매칭 취소 
        Public Function fnExe_CrossCancel(ByVal r_al_rst As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnExe_CrossCancel(ArrayList) As Boolean"
            Dim dbCmd As New OracleCommand
            Try

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With


                For ix As Integer = 0 To r_al_rst.Count - 1
                    ' lb043h insert
                    sSql = fnGet_InsLB043HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD
                    dbCmd.Parameters.Add("iogbn",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).IOGBN
                    dbCmd.Parameters.Add("fkocs", OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb043m update -> state : '1', comcd_out : comcd, bldno : ''  
                    sSql = fnGet_UpdLB043MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("state",  OracleDbType.Varchar2).Value = "1"c
                    dbCmd.Parameters.Add("abo",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).ABO
                    dbCmd.Parameters.Add("rh",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).RH
                    dbCmd.Parameters.Add("ocscost",  OracleDbType.Varchar2).Value = "0"
                    dbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD
                    dbCmd.Parameters.Add("iogbn",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).IOGBN
                    dbCmd.Parameters.Add("fkocs", OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb030h insert
                    sSql = fnGet_InsLB030HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).TNSJUBSUNO


                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb030m delete
                    sSql = fnGet_DelLB030MSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).TNSJUBSUNO

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb020h insert state : Cross Matching 취소
                    sSql = fnGet_InsLB020HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD_OUT

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb020m update state : '1', statedt : sysdate
                    sSql = ""
                    sSql = fnGet_UpdLB020MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("state",  OracleDbType.Varchar2).Value = "0"c
                    dbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_rst(ix), STU_TnsJubsu).COMCD_OUT

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
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
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        ' 가출고등록 or 취소
        Public Function fnExe_BefOut(ByVal r_al_BefInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_BefOut(ArrayList) As Boolean"
            Dim dbCmd As New OracleCommand

            With dbCmd
                .Connection = m_DbCn
                .Transaction = m_dbTran
            End With

            Try
                For ix As Integer = 0 To r_al_BefInfo.Count - 1

                    dbCmd.CommandType = CommandType.StoredProcedure

                    If rsGbn = "E"c Then
                        dbCmd.CommandText = "PRO_ACK_EXE_TNS_BEFOUT"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).BLDNO
                        dbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("rs_comcd",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).COMCD
                        dbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).OWNGBN
                        dbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                        dbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).REGNO
                        dbCmd.Parameters.Add("rs_abo",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).ABO
                        dbCmd.Parameters.Add("rs_rh",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).RH
                        dbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP


                        dbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                        dbCmd.Parameters("rs_retval").Size = 2000
                        dbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                        dbCmd.Parameters("rs_retval").Value = ""

                        dbCmd.ExecuteNonQuery()

                        'Dim sMsgErr As String = .Parameters(12).Value.TOSTRING

                        If dbCmd.Parameters(11).Value.ToString <> "00" Then
                            m_dbTran.Rollback()
                            Throw (New Exception(dbCmd.Parameters(11).Value.ToString.Substring(2)))
                        End If
                    Else
                        dbCmd.CommandText = "PRO_ACK_EXE_TNS_BEFOUT_CANCEL"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).BLDNO
                        dbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("rs_comcd",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).COMCD
                        dbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).OWNGBN
                        dbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                        dbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(r_al_BefInfo(ix), STU_TnsJubsu).REGNO
                        dbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP


                        dbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                        dbCmd.Parameters("rs_retval").Size = 2000
                        dbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                        dbCmd.Parameters("rs_retval").Value = ""

                        dbCmd.ExecuteNonQuery()

                        'Dim sMsgErr As String = .Parameters(12).Value.TOString

                        If dbCmd.Parameters(9).Value.ToString <> "00" Then
                            m_dbTran.Rollback()
                            Throw (New Exception(dbCmd.Parameters(9).Value.ToString.Substring(2)))
                        End If
                    End If
                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function


    End Class

    '-- 출고
    Public Class Out
        Inherits SqlFn
        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.Out" + vbTab

        Private m_DbCn As New OracleConnection
        Private m_dbTran As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_dbTran = m_DbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Private Function fnGet_Sysdate() As String
            Dim sFn As String = "Private Function fnGet_Sysdate() As String"
            Dim DbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT fn_ack_sysdate FROM DUAL"

                DbCmd.Connection = m_DbCn
                DbCmd.Transaction = m_dbTran
                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(DbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                Else
                    Return dt.Rows(0).Item(0).ToString
                End If
            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmmss").ToString

            End Try

        End Function

        ' 출고등록 or 취소
        Public Function fnExe_Out(ByVal r_al_OutInfo As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnExe_Out(ArrayList, String) As Boolean"
            Dim dbCmd As New OracleCommand


            Try
                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                For ix As Integer = 0 To r_al_OutInfo.Count - 1
                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_tns_out"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                    dbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("rs_comcd",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                    dbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).REGNO
                    dbCmd.Parameters.Add("rs_recid",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).RECID
                    dbCmd.Parameters.Add("rs_recnm",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).RECNM
                    dbCmd.Parameters.Add("rs_abo",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).ABO
                    dbCmd.Parameters.Add("rs_rh",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).RH

                    dbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    dbCmd.Parameters("rs_retval").Size = 2000
                    dbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    dbCmd.Parameters("rs_retval").Value = ""

                    dbCmd.ExecuteNonQuery()

                    'Dim sMsgErr As String = .Parameters(12).Value.TOSTRING

                    If dbCmd.Parameters(13).Value.ToString <> "00" Then
                        'm_dbTran.Rollback()
                        Throw (New Exception(dbCmd.Parameters(13).Value.ToString.Substring(2)))
                    End If
                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try
        End Function


        Public Function fnExe_Out_NotCross(ByVal r_al_OutInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Out_NotCross(ArrayList, String) As Boolean"
            Dim DbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer

                Dim sOutdt As String = fnGet_Sysdate()
                Dim sFkocs As String = ""
                Dim alParm As ArrayList

                With dbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                End With


                For ix As Integer = 0 To r_al_OutInfo.Count - 1



                    If rsGbn = "E" Then


                        ' lb030m insert
                        sSql = fnGet_InsLB030MSql("NoCross")

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd_out", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("testgbn", OracleDbType.Varchar2).Value = "3"c
                        DbCmd.Parameters.Add("testid", OracleDbType.Varchar2).Value = ""
                        DbCmd.Parameters.Add("testdt", OracleDbType.Varchar2).Value = ""
                        DbCmd.Parameters.Add("rst1", OracleDbType.Varchar2).Value = ""
                        DbCmd.Parameters.Add("rst2", OracleDbType.Varchar2).Value = ""
                        DbCmd.Parameters.Add("rst3", OracleDbType.Varchar2).Value = ""
                        DbCmd.Parameters.Add("rst4", OracleDbType.Varchar2).Value = ""
                        DbCmd.Parameters.Add("cmrmk", OracleDbType.Varchar2).Value = ""
                        DbCmd.Parameters.Add("eryn", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).EMER
                        DbCmd.Parameters.Add("ir", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).IR
                        DbCmd.Parameters.Add("filter", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).FILTER
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                        DbCmd.Parameters.Add("comnm", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMNM
                        'ssql
                        DbCmd.Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        'DbCmd.Parameters.Add("regdt", OracleDbType.Varchar2).Value = sOutdt

                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("outid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("outdt", OracleDbType.Varchar2).Value = sOutdt
                        DbCmd.Parameters.Add("recid", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).RECID
                        DbCmd.Parameters.Add("recnm", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).RECNM


                        ' iRet = DbCmd.ExecuteNonQuery()

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If



                        ' lb043 update (bldno, comcd)
                        sSql = ""
                        sSql = fnGet_UpdLB043MBCSSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("state", OracleDbType.Varchar2).Value = "4"c
                        DbCmd.Parameters.Add("comcd_out", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("bldno ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("tnsno ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("comcd ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                        DbCmd.Parameters.Add("iogbn ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).IOGBN
                        ' DbCmd.Parameters.Add("comcd ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        ' lb020h insert
                        sSql = ""
                        sSql = fnGet_InsLB020HSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        ' lb020m update (state, statedt)
                        sSql = ""
                        sSql = fnGet_UpdLB020MStateSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql


                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("state", OracleDbType.Varchar2).Value = "4"c
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        ' lb042m 수량 및 완료 여부 업데이트 
                        sSql = ""
                        sSql = fnGet_UpdLB042MStateSql("응급출고") '<<<20180511 응급출고는 RBC320 만한다고함. 용량변경시 접수테이블은 400으로 남아있기때문에 LB507로 픽스

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("tnsno ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        'DbCmd.Parameters.Add("comcd ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        'Dim OleDbDA As OleDb.OleDbDataAdapter
                        Dim dbDa As OracleDataAdapter
                        Dim dt As New DataTable

                        sSql = ""
                        sSql += "SELECT fkocs"
                        sSql += "  FROM lb043m"
                        sSql += " WHERE tnsjubsuno = :tnsno"
                        sSql += "   AND comcd      = :comcd"
                        sSql += "   AND bldno      = :bldno"

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        dbDa = New OracleDataAdapter(DbCmd)



                        With dbDa
                            .SelectCommand.Parameters.Clear()
                            .SelectCommand.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                            .SelectCommand.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                            .SelectCommand.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        End With

                        dt.Reset()
                        dbDa.Fill(dt)

                        If dt.Rows.Count < 1 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        sFkocs = dt.Rows(0).Item("fkocs").ToString

                        DbCmd.CommandType = CommandType.StoredProcedure
                        DbCmd.CommandText = "pro_ack_exe_io_error_bcno"
                        'PRO_ACK_EXE_IO_ERROR_BCNO(rs_tnsno, rs_retval);

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO.Replace("-", "")
                        DbCmd.Parameters.Add("rs_retval", OracleDbType.Varchar2)
                        DbCmd.Parameters("rs_retval").Size = 2000
                        DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                        DbCmd.Parameters("rs_retval").Value = ""

                        If DbCmd.Parameters(1).Value.ToString <> "" Then
                            m_dbTran.Rollback()
                            Throw (New Exception(DbCmd.Parameters(8).Value.ToString.Substring(3) + " @" + msFile + sFn))
                        End If


                        ' PRO_ACK_EXE_OCS_TNS_BLD( 'O', rs_tnsno, rs_bldno, rs_comcd_out, rs_usrid, rs_ip, i_retval, rs_retval);

                        If CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN = "O" Then
                          

                            sSql = ""
                            sSql += "                    SELECT SUBSTR(C.OUTDT, 1, 8) OUTDT, F.COMORDCD, B.ABO||B.RH as bldtype , C.IR, C.FILTER, " + vbCrLf
                            sSql += "    CASE WHEN SUBSTR(B.FKOCS, 1, 1) = 'I' THEN '2' ELSE '3' END," + vbCrLf
                            sSql += "   CASE WHEN SUBSTR(B.FKOCS, 1, 1) = 'I' THEN A.WARDNO ELSE A.DEPTCD END as dptward," + vbCrLf
                            sSql += "    B.REGNO, B.OCS_KEY, SUBSTR(B.ORDDT, 1, 8) ORDDT, " + vbCrLf
                            sSql += "    C.RECNM, C.OUTID,   TO_DATE(C.OUTDT, 'YYYYMMDDHH24MISS') as prcdt, C.BEFOUTID, O.ADMDATE," + vbCrLf
                            sSql += "                                O.ORDTEXT, SUBSTR(B.FKOCS, 1, 1) AS ioflag" + vbCrLf
                            'sSql += "    INTO s_outdt,  s_comordcd, s_bldtype, s_ir,     s_filter,  s_iogbn,  s_dptward,  s_regno, n_ordseq, s_orddt,  " + vbCrLf
                            'sSql += "    s_recnm, s_prcid, n_prcdt, s_testid, d_meddate, s_ordrem, s_ioflag" + vbCrLf
                            sSql += "    FROM LB040M A, LB043M B, LB030M C, VW_ACK_OCS_ORD_INFO O, LF120M F" + vbCrLf
                            sSql += "    WHERE(A.TNSJUBSUNO = B.TNSJUBSUNO)" + vbCrLf
                            sSql += "    AND B.TNSJUBSUNO           = C.TNSJUBSUNO" + vbCrLf
                            sSql += "    AND B.BLDNO                = C.BLDNO" + vbCrLf
                            sSql += "    AND B.COMCD_OUT            = C.COMCD_OUT" + vbCrLf
                            sSql += "    AND O.INSTCD               = '031'" + vbCrLf
                            sSql += "    AND B.REGNO                = O.PATNO" + vbCrLf
                            sSql += "    AND SUBSTR(B.ORDDT, 1, 8)  = O.ORDDATE" + vbCrLf
                            sSql += "    AND B.OCS_KEY              = O.ORDSEQNO" + vbCrLf
                            sSql += "    AND SUBSTR(B.FKOCS, 1, 1)  = O.IOFLAG" + vbCrLf
                            sSql += "    AND O.PRCPHISTCD           = 'O'" + vbCrLf
                            sSql += "    AND O.EXECPRCPHISTCD       = 'O'" + vbCrLf
                            sSql += "    AND O.PRCPCLSCD            = 'B4'" + vbCrLf
                            sSql += "    AND B.COMCD                = F.COMCD" + vbCrLf
                            sSql += "    AND B.SPCCD                = F.SPCCD           " + vbCrLf
                            sSql += "    AND A.JUBSUDT             >= F.USDT" + vbCrLf
                            sSql += "    AND A.JUBSUDT             <  F.UEDT" + vbCrLf
                            sSql += "    AND C.BLDNO                = :bldno" + vbCrLf
                            sSql += "    AND C.COMCD_OUT            = :comcd_out" + vbCrLf
                            sSql += "    AND C.TNSJUBSUNO           = :tnsno    " + vbCrLf


                            DbCmd.CommandType = CommandType.Text
                            DbCmd.CommandText = sSql

                            dbDa = New OracleDataAdapter(DbCmd)



                            With dbDa
                                .SelectCommand.Parameters.Clear()
                                .SelectCommand.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                                .SelectCommand.Parameters.Add("comcd_out", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                                .SelectCommand.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                            End With 'bldtype

                            dt.Reset()
                            dbDa.Fill(dt)

                            If dt.Rows.Count < 1 Then
                                m_dbTran.Rollback()
                                Return False
                            End If


                            sSql = ""
                            sSql = fn_INSERT_SLBOUTT()

                            DbCmd.CommandType = CommandType.Text
                            DbCmd.CommandText = sSql

                            DbCmd.Parameters.Clear()
                            DbCmd.Parameters.Add("outdt", OracleDbType.Varchar2).Value = dt.Rows(0).Item("outdt").ToString
                            DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                            DbCmd.Parameters.Add("comordcd ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("comordcd").ToString
                            DbCmd.Parameters.Add("bldtype ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("bldtype").ToString
                            DbCmd.Parameters.Add("ir ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("ir").ToString
                            DbCmd.Parameters.Add("filter ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("filter").ToString
                            DbCmd.Parameters.Add("iogbn ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("ioflag").ToString
                            DbCmd.Parameters.Add("dptward ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("dptward").ToString
                            DbCmd.Parameters.Add("meddate ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("ADMDATE").ToString
                            DbCmd.Parameters.Add("regno ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("regno").ToString
                            DbCmd.Parameters.Add("ordseq ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("ocs_key").ToString
                            DbCmd.Parameters.Add("orddt", OracleDbType.Varchar2).Value = dt.Rows(0).Item("orddt").ToString
                            DbCmd.Parameters.Add("recnm ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("recnm").ToString
                            DbCmd.Parameters.Add("prcid ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("outid").ToString
                            Dim sPrcdt As String = dt.Rows(0).Item("prcdt").ToString
                            DbCmd.Parameters.Add("prcdt ", OracleDbType.Varchar2).Value = sPrcdt.Substring(0, 10)
                            DbCmd.Parameters.Add("testid ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("befoutid").ToString
                            DbCmd.Parameters.Add("ordseq ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("ocs_key").ToString
                            DbCmd.Parameters.Add("ioflag ", OracleDbType.Varchar2).Value = dt.Rows(0).Item("ioflag").ToString
                            iRet = DbCmd.ExecuteNonQuery()

                            If iRet = 0 Then
                                m_dbTran.Rollback()
                                Return False
                            End If

                            'fn_INSERT_SLBOUTT

                          

                            'If DbCmd.Parameters(7).Value.ToString <> "" Then
                            '    m_dbTran.Rollback()
                            '    Throw (New Exception(DbCmd.Parameters(8).Value.ToString.Substring(3) + " @" + msFile + sFn))
                            'End If


                        End If

                        DbCmd.CommandType = CommandType.StoredProcedure
                        ' DbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg"
                        DbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg_E"



                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("regno ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).REGNO

                        DbCmd.Parameters.Add("rstflg", OracleDbType.Varchar2).Value = "3"c

                        DbCmd.Parameters.Add("bldno ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("owngbn", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN
                        DbCmd.Parameters.Add("fkocs ", OracleDbType.Varchar2).Value = sFkocs
                        DbCmd.Parameters.Add("date  ", OracleDbType.Varchar2).Value = sOutdt
                        DbCmd.Parameters.Add("usrid ", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("ip    ", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        'DbCmd.Parameters.Add("retval", OracleDbType.Decimal)
                        'DbCmd.Parameters("retval").Direction = ParameterDirection.InputOutput
                        'DbCmd.Parameters("retval").Value = -1
                        DbCmd.Parameters.Add("rs_retval", OracleDbType.Varchar2)
                        DbCmd.Parameters("rs_retval").Size = 2000
                        DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                        DbCmd.Parameters("rs_retval").Value = ""

                        DbCmd.ExecuteNonQuery()


                        If DbCmd.Parameters(8).Value.ToString <> "00" Then
                            m_dbTran.Rollback()
                            Throw (New Exception(DbCmd.Parameters(8).Value.ToString.Substring(3) + " @" + msFile + sFn))
                        End If

                        'iRet = CType(DbCmd.Parameters(8).Value, Integer)

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If



                    Else ' 응급출고 취소.

                        'lb043h insert
                        sSql = fnGet_InsLB043HSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                        DbCmd.Parameters.Add("iogbn", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).IOGBN
                        DbCmd.Parameters.Add("fkocs", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        ' lb043 update (state)
                        sSql = fnGet_UpdLB043MStateSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("state", OracleDbType.Varchar2).Value = "1"c
                        DbCmd.Parameters.Add("abo", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).ABO
                        DbCmd.Parameters.Add("rh", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).RH
                        DbCmd.Parameters.Add("ocscost", OracleDbType.Varchar2).Value = "0"
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                        DbCmd.Parameters.Add("iogbn", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).IOGBN
                        DbCmd.Parameters.Add("fkocs", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If



                        ' lb020H INSERT
                        sSql = ""
                        sSql = fnGet_InsLB020HSql()

                        DbCmd.Transaction = m_dbTran
                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If


                        ' lb020M UPDATE
                        sSql = fnGet_UpdLB020MStateSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("state", OracleDbType.Varchar2).Value = "0"c
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT


                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        ' lb030h insert
                        sSql = fnGet_InsLB030HSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd_out", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO


                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If
                      

                        ' lb030m delete
                        sSql = ""
                        sSql = fnGet_DelLB030MSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd_out", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        sSql = fnGet_UpdLB042MStateSql("출고취소")

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql
                      

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("comcd", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD



                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If


                    

                        If CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN = "O" Then
                            '-- OCS (처리)
                            DbCmd.CommandType = CommandType.StoredProcedure
                            DbCmd.CommandText = "pro_ack_exe_ocs_tns_bld2"

                            DbCmd.Parameters.Clear()

                            DbCmd.Parameters.Add("rs_jobgbn", OracleDbType.Varchar2).Value = "CO"
                            DbCmd.Parameters.Add("rs_tnsno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                            DbCmd.Parameters.Add("rs_bldno", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                            DbCmd.Parameters.Add("rs_comcd_out", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                            DbCmd.Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                            DbCmd.Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            DbCmd.Parameters.Add("ri_retval", OracleDbType.Int32)
                            DbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                            DbCmd.Parameters("ri_retval").Value = -1

                            DbCmd.Parameters.Add("rs_retval", OracleDbType.Varchar2)
                            DbCmd.Parameters("rs_retval").Size = 2000
                            DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                            DbCmd.Parameters("rs_retval").Value = ""


                            DbCmd.ExecuteNonQuery()

                            iRet = CType(DbCmd.Parameters(6).Value.ToString, Integer)
                            'CType(DbCmd.Parameters(7).Value.ToString, string)
                            '혈액번호 : 4444444444
                            '처방코드:    LBP2G2G
                            '입원     : 2
                            '등록번호 : 10141555                  
                            '처방일자 : 20170908
                            '처방키   : 539991579


                            If iRet < 1 Then
                                m_dbTran.Rollback()
                                Return False
                            End If
                        End If
                        '  End If

                        DbCmd.CommandType = CommandType.StoredProcedure
                        DbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg_E"


                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("regno ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).REGNO

                        DbCmd.Parameters.Add("rstflg", OracleDbType.Varchar2).Value = "0"c

                        DbCmd.Parameters.Add("bldno ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("owngbn", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN
                        DbCmd.Parameters.Add("fkocs ", OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                        DbCmd.Parameters.Add("date  ", OracleDbType.Varchar2).Value = sOutdt
                        DbCmd.Parameters.Add("usrid ", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("ip    ", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        
                        DbCmd.Parameters.Add("rs_retval", OracleDbType.Varchar2)
                        DbCmd.Parameters("rs_retval").Size = 3000
                        DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                        DbCmd.Parameters("rs_retval").Value = ""

                        DbCmd.ExecuteNonQuery()


                        If DbCmd.Parameters(8).Value.ToString <> "00" Then
                            m_dbTran.Rollback()
                            Throw (New Exception(DbCmd.Parameters(8).Value.ToString.Substring(3) + " @" + msFile + sFn))
                        End If


                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                    End If

                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        Public Function fnExe_Out_Cancel(ByVal r_al_OutInfo As ArrayList, ByVal rsOutGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Out_Cancel(ArrayList, String) As Boolean"
            Dim dbCmd As New OracleCommand


            Try
                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                For ix As Integer = 0 To r_al_OutInfo.Count - 1
                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_tns_out_cancel"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_outgbn",  OracleDbType.Varchar2).Value = rsOutGbn
                    dbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                    dbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("rs_comcd",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD
                    dbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).REGNO

                    dbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    dbCmd.Parameters("rs_retval").Size = 2000
                    dbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    dbCmd.Parameters("rs_retval").Value = ""

                    dbCmd.ExecuteNonQuery()

                    Dim sMsgErr As String = CType(dbCmd.Parameters(10).Value, String)

                    If dbCmd.Parameters(10).Value.ToString <> "00" Then
                        Throw (New Exception(dbCmd.Parameters(10).Value.ToString.Substring(2)))
                    End If
                Next

                m_dbTran.Commit()
                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try
        End Function


        ' 출고등록 or 취소
        Public Function fnExe_out_ocs(ByVal r_al_OutInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_out_ocs(ArrayList, String) As Boolean"
            Dim dbCmd As New OracleCommand


            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim sOutdt As String = fnGet_Sysdate()

                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                For ix As Integer = 0 To r_al_OutInfo.Count - 1

                    If CType(r_al_OutInfo(ix), STU_TnsJubsu).OWNGBN = "O" Then
                        '-- OCS (처리)
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.CommandText = "pro_ack_exe_ocs_tns_bld"

                        dbCmd.Parameters.Clear()

                        If rsGbn = "E"c Then        '-- 출고
                            dbCmd.Parameters.Add("rs_jobgbn",  OracleDbType.Varchar2).Value = "O"c
                        ElseIf rsGbn = "C"c Then    '-- 출고 취소
                            dbCmd.Parameters.Add("rs_jobgbn",  OracleDbType.Varchar2).Value = "CO"
                        End If

                        dbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).BLDNO
                        dbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_OutInfo(ix), STU_TnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        dbCmd.Parameters.Add("ri_retval", OracleDbType.Int32)
                        dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                        dbCmd.Parameters("ri_retval").Value = -1

                        dbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                        dbCmd.Parameters("rs_retval").Size = 2000
                        dbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                        dbCmd.Parameters("rs_retval").Value = ""

                        dbCmd.ExecuteNonQuery()

                        iRet += CType(dbCmd.Parameters(6).Value.ToString.ToString, Integer)


                    End If

                Next

                If iRet < 1 Then
                    m_dbTran.Rollback()
                    Return False
                Else
                    m_dbTran.Commit()
                    Return True
                End If

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try
        End Function

    End Class

    '-- 반납/폐기/자체폐기
    Public Class Rtn
        Inherits SqlFn

        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.Rtn" + vbTab

        Private m_DbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_dbTran = m_DbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Private Function fnGet_Sysdate() As String
            Dim sFn As String = "Private Function fnGet_Sysdate() As String"
            Dim DbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT fn_ack_sysdate FROM DUAL"

                DbCmd.Connection = m_DbCn
                DbCmd.Transaction = m_dbTran
                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(DbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                Else
                    Return dt.Rows(0).Item(0).ToString
                End If
            Catch ex As Exception
                Return Format(Now, "yyyyMMddHHmmss").ToString

            End Try

        End Function

        ' 혈액 반납/폐기 작업
        Public Function fnExe_Rtn(ByVal r_al_RtnInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Rtn(ArrayList, String) As Boolean"

            Dim dbCmd As New OracleCommand

            Try
                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                For ix As Integer = 0 To r_al_RtnInfo.Count - 1
                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_tns_rtnabn"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_rtnflg",  OracleDbType.Varchar2).Value = IIf(rsGbn = "R", "1", "2").ToString
                    dbCmd.Parameters.Add("rs_costyn",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TEMP01
                    dbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                    dbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("rs_comcd",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD
                    dbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).REGNO
                    dbCmd.Parameters.Add("rs_reqid",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNREQID
                    dbCmd.Parameters.Add("rs_reqnm",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNREQNM
                    dbCmd.Parameters.Add("rs_rsncd",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNrsncd
                    dbCmd.Parameters.Add("rs_rsncmt",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNrsncmt

                    dbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    dbCmd.Parameters("rs_retval").Size = 2000
                    dbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    dbCmd.Parameters("rs_retval").Value = ""

                    dbCmd.ExecuteNonQuery()

                    Dim sMsgErr As String = CType(dbCmd.Parameters(15).Value, String)

                    If dbCmd.Parameters(15).Value.ToString <> "00" Then
                        Throw (New Exception(dbCmd.Parameters(15).Value.ToString.Substring(2)))
                    End If


                    '+++++++++++++++++++++++++++++++++++++++++++++
                    '++ 폐기인 경우 
                    If rsGbn = "A" Then
                        '+++++++++++++++++++++++++++++++++++++++++++
                        '++ prcpMgrComn.setBlodRtnAbnPrcpIF(rtnVO)
                        '++++++++++++++++++++++++++++++++++++++++++++
                    End If
                    ' 
                Next

                m_dbTran.Commit()

                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        ' 혈액 반납/폐기 취소 작업
        Public Function fnExe_Rtn_Cancel(ByVal r_al_RtnInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Rtn_Cancel(ArrayList, String) As Boolean"
            Dim DbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim sRtndt As String = fnGet_Sysdate()

                With DbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                For ix As Integer = 0 To r_al_RtnInfo.Count - 1

                    'lb043h insert
                    sSql = fnGet_InsLB043HSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                    DbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD
                    DbCmd.Parameters.Add("iogbn",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).IOGBN
                    DbCmd.Parameters.Add("fkocs", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb043 update (state)
                    sSql = fnGet_UpdLB043MStateSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("state",  OracleDbType.Varchar2).Value = "4"c
                    DbCmd.Parameters.Add("abo",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).ABO
                    DbCmd.Parameters.Add("rh",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RH
                    DbCmd.Parameters.Add("ocscost",  OracleDbType.Varchar2).Value = "0"
                    DbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                    DbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD
                    DbCmd.Parameters.Add("iogbn",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).IOGBN
                    DbCmd.Parameters.Add("fkocs", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb030m insert
                    sSql = fnGet_InsLB030MRtnCancelSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("regid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("regip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb031h insert
                    sSql = fnGet_InsLB031HSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    ' lb031m delete
                    sSql = ""
                    sSql = fnGet_DelLB031mSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("tnsno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If


                    ' 필터가 아닐경우에만 혈액테이블 업데이트 처리
                    If CType(r_al_RtnInfo(ix), STU_TnsJubsu).FILTER <> "1"c Then

                        ' lb020h insert
                        sSql = fnGet_InsLB020HSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("modid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("modip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        ' lb020m update (state, statedt)
                        sSql = fnGet_UpdLB020MStateSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()

                        DbCmd.Parameters.Add("state ",  OracleDbType.Varchar2).Value = "4"c
                        DbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno ",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd ",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_dbTran.Rollback()
                            Return False
                        End If

                        If CType(r_al_RtnInfo(ix), STU_TnsJubsu).OWNGBN = "O" Then
                            '-- OCS (처리)
                            DbCmd.CommandType = CommandType.StoredProcedure
                            DbCmd.CommandText = "pro_ack_exe_ocs_tns_bld"

                            DbCmd.Parameters.Clear()

                            DbCmd.Parameters.Add("rs_jobgbn",  OracleDbType.Varchar2).Value = "C" + rsGbn
                            DbCmd.Parameters.Add("rs_tnsno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                            DbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                            DbCmd.Parameters.Add("rs_comcd_out",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT
                            DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                            DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                            DbCmd.Parameters.Add("ri_retval", OracleDbType.Int32)
                            DbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                            DbCmd.Parameters("ri_retval").Value = -1

                            DbCmd.ExecuteNonQuery()

                            iRet = CType(DbCmd.Parameters(6).Value.ToString, Integer)

                            If iRet < 1 Then
                                m_dbTran.Rollback()
                                Return False
                            End If
                        End If
                    End If

                    ' lb042m 수량 및 완료 여부 업데이트 

                    sSql = fnGet_UpdLB042MStateSql(IIf(rsGbn = "R", "반납취소", "폐기취소").ToString)

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("editid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("tnsno ",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).TNSJUBSUNO
                    ' DbCmd.Parameters.Add("comcd ",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_dbTran.Rollback()
                        Return False
                    End If

                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg"

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("rs_regno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).REGNO

                    If rsGbn = "R" Then
                        DbCmd.Parameters.Add("rs_rstflg",  OracleDbType.Varchar2).Value = "C4"
                    Else
                        DbCmd.Parameters.Add("rs_rstflg",  OracleDbType.Varchar2).Value = "C5"
                    End If

                    DbCmd.Parameters.Add("rs_bldno",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                    DbCmd.Parameters.Add("rs_owngbn",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).OWNGBN
                    DbCmd.Parameters.Add("rs_fkocs",  OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).FKOCS.Split("-"c)(0)
                    DbCmd.Parameters.Add("rs_acptdt",  OracleDbType.Varchar2).Value = sRtndt
                    DbCmd.Parameters.Add("rs_usrid",  OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_ip",  OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("rs_retval",  OracleDbType.Varchar2)
                    DbCmd.Parameters("rs_retval").Size = 2000
                    DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    DbCmd.Parameters("rs_retval").Value = ""

                    DbCmd.ExecuteNonQuery()

                    If DbCmd.Parameters(8).Value.ToString <> "00" Then
                        m_dbTran.Rollback()
                        Throw (New Exception(DbCmd.Parameters(8).Value.ToString.Substring(3) + " @" + msFile + sFn))
                    End If
                Next

                m_dbTran.Commit()

                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        ' 혈액 자체폐기/교환
        Public Function fnExe_SelfAbn(ByVal r_al_RtnInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_SelfAbn(ArrayList, String) As Boolean"
            Dim DbCmd As New OracleCommand

            Try
                Dim sRtndt As String = fnGet_Sysdate()
                Dim rs_gbn As String = ""
                With DbCmd
                    .Connection = m_DbCn
                    .Transaction = m_dbTran
                End With

                If rsGbn = "A" Then
                    rs_gbn = "5"
                Else
                    rs_gbn = "6"
                End If

                For ix As Integer = 0 To r_al_RtnInfo.Count - 1
                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "pro_ack_exe_tns_sefabn"


                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("rs_bldno", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).BLDNO
                    DbCmd.Parameters.Add("rs_comcd_out", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("rs_reqid", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNREQID
                    DbCmd.Parameters.Add("rs_reqnm", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNREQNM
                    DbCmd.Parameters.Add("rs_rsncd", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNRSNCD
                    DbCmd.Parameters.Add("rs_rsncmt", OracleDbType.Varchar2).Value = CType(r_al_RtnInfo(ix), STU_TnsJubsu).RTNRSNCMT
                    DbCmd.Parameters.Add("rs_gbn", OracleDbType.Varchar2).Value = rs_gbn

                    DbCmd.Parameters.Add("rs_usrid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_ip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("rs_retval", OracleDbType.Varchar2)
                    DbCmd.Parameters("rs_retval").Size = 2000
                    DbCmd.Parameters("rs_retval").Direction = ParameterDirection.Output
                    DbCmd.Parameters("rs_retval").Value = ""

                    DbCmd.ExecuteNonQuery()

                    Dim sMsgErr As String = DbCmd.Parameters(9).Value.ToString

                    If DbCmd.Parameters(9).Value.ToString <> "00" Then
                        Throw (New Exception(DbCmd.Parameters(9).Value.ToString.Substring(2)))
                    End If
                Next

                m_dbTran.Commit()

                Return True

            Catch ex As Exception
                m_dbTran.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_dbTran.Dispose() : m_dbTran = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

    End Class


    '-- ABO, Rh 2차 결과등록
    Public Class RegAboRh
        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.RegAboRh" + vbTab
        Private m_dbCn As OracleConnection
        Private m_dbTran As OracleTransaction

        Public Sub New()
            m_dbCn = GetDbConnection()
            m_dbTran = m_dbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Public Function fnExe_Reg_Rst(ByVal raData As ArrayList, ByVal rsUsrId As String) As String

            Dim sFn As String = "Public Function fnExe_Reg_Rst(ArrayList, String) As string"
            Dim DbCmd As New OracleCommand

            Try
                With DbCmd
                    .Connection = m_dbCn
                    .Transaction = m_dbTran
                End With

                For ix As Integer = 0 To raData.Count - 1
                    Dim sBcNo As String = raData.Item(ix).ToString.Split("|"c)(0)
                    Dim sTestCd As String = raData.Item(ix).ToString.Split("|"c)(1)
                    Dim sRst As String = raData.Item(ix).ToString.Split("|"c)(2)

                    Dim sSql As String = ""
                    Dim iRet As Integer = 0

                    sSql += "UPDATE lb070m SET rstval = :rstval, regdt = fn_ack_sysdate, regid = :regid"
                    sSql += " WHERE bcno   = :bcno"
                    sSql += "   AND testcd = :testcd"

                    With DbCmd
                        .CommandText = sSql
                        .CommandType = CommandType.Text

                        .Parameters.Clear()
                        .Parameters.Add("rstval",  OracleDbType.Varchar2).Value = sRst
                        .Parameters.Add("regid",  OracleDbType.Varchar2).Value = rsUsrId
                        .Parameters.Add("bcno",  OracleDbType.Varchar2).Value = sBcNo
                        .Parameters.Add("testcd",  OracleDbType.Varchar2).Value = sTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    If iRet = 0 Then
                        sSql = ""
                        sSql += "INSERT INTO lb070m (bcno, testcd, rstval, regdt, regid)"
                        sSql += "    VALUES( :bcno, :testcd, :rstval, fn_ack_sysdate, :regid)"

                        With DbCmd
                            .CommandText = sSql
                            .CommandType = CommandType.Text

                            .Parameters.Clear()
                            .Parameters.Add("bcno",  OracleDbType.Varchar2).Value = sBcNo
                            .Parameters.Add("testcd",  OracleDbType.Varchar2).Value = sTestCd
                            .Parameters.Add("rstval",  OracleDbType.Varchar2).Value = sRst
                            .Parameters.Add("regid",  OracleDbType.Varchar2).Value = rsUsrId

                            iRet = .ExecuteNonQuery()
                        End With

                    End If
                Next

                m_dbTran.Commit()
                Return ""

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

    End Class

    Public Class CGDA_BT
        Private Const msFile As String = "File : CGDA_BT.vb, Class : B01" & vbTab


#Region " X-Matching 현황 조회"
        Public Shared Function fnGet_Ward_List() As DataTable

            Dim sFn As String = "Function fnGet_Ward_List(String, String) As DataTable"


            Try
                DbCommand()
                Return DbExecuteQuery("SELECT * FROM VW_ACK_OCS_WARD_INFO")

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_Dept_List() As DataTable

            Dim sFn As String = "Function fnGet_Dept_List(String, String) As DataTable"

            Try
                DbCommand()
                Return DbExecuteQuery("SELECT * FROM VW_ACK_OCS_DEPT_INFO")

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_XMatch_Com(ByVal rsDateS As String, ByVal rsDateE As String) As DataTable

            Dim sFn As String = "Function fnGet_XMatch_Com(String, String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                ' X-Matching
                sSql += "SELECT f.comcd comcd, f.comnmd, f.dispseql"
                sSql += "  FROM lb031m b3, lb043m b4, lf120m f"
                sSql += " WHERE b3.testdt    >= :testdts"
                sSql += "   AND b3.testdt    <= :testdte"
                sSql += "   AND b3.tnsjubsuno = b4.tnsjubsuno"
                sSql += "   AND b4.owngbn    <> 'H'"
                sSql += "   AND b3.comcd_out  = f.comcd"
                sSql += "   AND b3.testdt    >= f.usdt"
                sSql += "   AND b3.testdt    <  f.uedt"
                sSql += " UNION "
                sSql += "SELECT f.comcd comcd, f.comnmd, f.dispseql"
                sSql += "  FROM lb030m b3, lb043m b4, lf120m f"
                sSql += " WHERE b3.testdt    >= :testdts"
                sSql += "   AND b3.testdt    <= :testdte"
                sSql += "   AND b3.tnsjubsuno = b4.tnsjubsuno"
                sSql += "   AND b4.owngbn    <> 'H'"
                sSql += "   AND b3.comcd_out  = f.comcd"
                sSql += "   AND b3.testdt    >= f.usdt"
                sSql += "   AND b3.testdt    <  f.uedt"
                sSql += " ORDER BY dispseql, comcd"

                alParm.Add(New OracleParameter("testdts",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("testdte",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

                alParm.Add(New OracleParameter("testdts",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("testdte",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_XMatch_DeptWithBld(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsDayGbn As String) As DataTable
            Dim sFn As String = "Function fnGet_XMatch_DeptWithBld( String, String) As DataTable "

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT a.dptward"
            sSql += "     , a.comcd, SUM(a.cnt) cnt"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb031m b3,"
            sSql += "               lb040m b4"
            sSql += "         WHERE b3.testdt >= :testdts"
            sSql += "           AND b3.testdt <= :testdte"
            sSql += "           AND b3.tnsjubsuno = b4.tnsjubsuno"
            sSql += "           AND b4.owngbn <> 'H'"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "         UNION "
            sSql += "        SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb030m b3,"
            sSql += "               lb040m b4"
            sSql += "         WHERE b3.testdt >= :testdts"
            sSql += "           AND b3.testdt <= :testdte"
            sSql += "           AND b3.tnsjubsuno = b4.tnsjubsuno"
            sSql += "           AND b4.owngbn <> 'H'"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "       ) a"
            sSql += " GROUP BY a.dptward, a.comcd"
            sSql += " ORDER BY dptward, comcd"

            alParm.Add(New OracleParameter("testdts",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("testdte",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            alParm.Add(New OracleParameter("testdts",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("testdte",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
#End Region


#Region " 진료과별 출고/폐기 현황 조회"
        Public Shared Function fnGet_OutAbn_Com(ByVal rsDateS As String, ByVal rsDateE As String) As DataTable

            Dim sFn As String = "Function fnGet_OutAbn_com(String, String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                ' 폐기 -> 자체폐기를 제외한 갯수를 의미한다!!! (자체폐기인 경우는 lb040m 에 데이터가 존재하지 않음)
                sSql += "SELECT f.comcd comcd, f.comnmd, f.dispseql"
                sSql += "  FROM lb031m b3, lf120m f"
                sSql += " WHERE b3.rtndt      >= :dates"
                sSql += "   AND b3.rtndt      <= :datee"
                sSql += "   AND b3.rtndt      >= :opendt"
                sSql += "   AND b3.rtnflg     = '2'"     ' 1: 반납, 2: 폐기
                sSql += "   AND b3.comcd_out  = f.comcd"
                sSql += "   AND b3.rtndt     >= f.usdt"
                sSql += "   AND b3.rtndt     <  f.uedt"
                sSql += " UNION "
                sSql += "SELECT f.comcd comcd, f.comnmd, f.dispseql"
                sSql += "  FROM lb030m b3, lb040m b4, lf120m f"
                sSql += " WHERE b3.outdt      >= :dates"
                sSql += "   AND b3.outdt      <= :datee"
                sSql += "   AND b3.tnsjubsuno = b4.tnsjubsuno"
                sSql += "   AND b4.owngbn    <> 'H'"
                sSql += "   AND b3.comcd_out  = f.comcd"
                sSql += "   AND b4.jubsudt   >= f.usdt"
                sSql += "   AND b4.jubsudt   <  f.uedt"
                sSql += " UNION "
                sSql += "SELECT f.comcd comcd, f.comnmd, f.dispseql"
                sSql += "  FROM lb031m b3, lb040m b4, lf120m f"
                sSql += " WHERE b3.outdt      >= :dates"
                sSql += "   AND b3.outdt      <= :datee"
                sSql += "   AND b3.tnsjubsuno = b4.tnsjubsuno"
                sSql += "   AND b4.owngbn    <> 'H'"
                sSql += "   AND b3.comcd_out  = f.comcd"
                sSql += "   AND b4.jubsudt   >= f.usdt"
                sSql += "   AND b4.jubsudt   <  f.uedt"
                sSql += " ORDER BY dispseql, comcd"

                alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

                alParm.Add(New OracleParameter("opendt",  OracleDbType.Varchar2, PRG_CONST.OPEN_DATE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.OPEN_DATE))

                alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))
                alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_OutAbn_DeptWithBld(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsDayGbn As String) As DataTable
            Dim sFn As String = "Function fnGet_BldInfo( String, String) As DataTable "

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT a.dptward, a.comcd, 'R' gbn, a.cnt"
            'sSql += " SELECT a.dptward, sum(a.cnt) as cnt"
            'sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb031m b3"
            sSql += "               LEFT OUTER JOIN"
            sSql += "                    lb040m b4 ON (b3.tnsjubsuno = b4.tnsjubsuno)"
            sSql += "         WHERE b3.rtndt >= :dates"
            sSql += "           AND b3.rtndt <= :datee"
            sSql += "           AND b3.rtndt >= :opendt"
            sSql += "           AND b3.rtnflg = '2'"      ' 1 : 반납, 2 : 폐기
            'sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "       ) a"
            'sSql += "  GROUP BY a.dptward " 
            'sSql += " GROUP BY a.dptward, a.comcd"
            sSql += " UNION "
            sSql += "SELECT a.dptward, a.comcd, 'O' gbn, a.cnt"
            'sSql += "SELECT a.dptward, sum(a.cnt) as cnt"
            'sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM (SELECT tnsjubsuno, bldno, comcd_out, rtndt"
            sSql += "                  FROM lb031m b3"
            sSql += "                 WHERE outdt >= :dates"
            sSql += "                   AND outdt <= :datee"
            sSql += "                 UNION ALL"
            sSql += "                SELECT tnsjubsuno, bldno, comcd_out, NULL rtndt"
            sSql += "                  FROM lb030m b3"
            sSql += "                 WHERE outdt >= :dates"
            sSql += "                   AND outdt <= :datee"
            sSql += "               ) b3,"
            sSql += "               lb040m b4"
            sSql += "         WHERE b3.tnsjubsuno = b4.tnsjubsuno"
            'sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "       ) a"
            sSql += " GROUP BY a.dptward, a.comcd, a.cnt"
            'sSql += "  GROUP BY a.dptward" ' 2019-02-11 JJH
            sSql += " ORDER BY dptward, comcd"
            'sSql += " ORDER BY dptward "

            alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            alParm.Add(New OracleParameter("opendt",  OracleDbType.Varchar2, PRG_CONST.OPEN_DATE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.OPEN_DATE))

            alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))
            alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_OutAbn_DeptDrWithBld(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsDayGbn As String) As DataTable
            Dim sFn As String = "Function fnGet_OutAbn_DeptDrWithBld( String, String) As DataTable "

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT a.dptward, a.drcd, fn_ack_get_dr_name(a.drcd) drnm, a.comcd, 'R' gbn, a.cnt"
            'sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "               b4.doctorcd drcd,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb031m b3"
            sSql += "               LEFT OUTER JOIN"
            sSql += "                    lb040m b4 ON (b3.tnsjubsuno = b4.tnsjubsuno)"
            sSql += "         WHERE b3.rtndt >= :dates"
            sSql += "           AND b3.rtndt <= :datee"
            sSql += "           AND b3.rtndt >= :opendt"
            sSql += "           AND b3.rtnflg = '2'"      ' 1 : 반납, 2 : 폐기
            'sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END, b4.doctorcd, b3.comcd_out"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END, b4.doctorcd, b3.comcd_out"
            sSql += "       ) a"
            'sSql += " GROUP BY a.dptward, a.comcd"
            sSql += " UNION "
            sSql += "SELECT a.dptward, a.drcd, fn_ack_get_dr_name(a.drcd) drnm, a.comcd, 'O' gbn, a.cnt"
            'sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END dptward,"
            sSql += "               b4.doctorcd drcd,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM (SELECT tnsjubsuno, bldno, comcd_out, rtndt"
            sSql += "                  FROM lb031m b3"
            sSql += "                 WHERE outdt >= :dates"
            sSql += "                   AND outdt <= :datee"
            sSql += "                 UNION ALL"
            sSql += "                SELECT tnsjubsuno, bldno, comcd_out, NULL rtndt"
            sSql += "                  FROM lb030m b3"
            sSql += "                 WHERE outdt >= :dates"
            sSql += "                   AND outdt <= :datee"
            sSql += "               ) b3,"
            sSql += "               lb040m b4"
            sSql += "         WHERE b3.tnsjubsuno = b4.tnsjubsuno"
            'sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || b4.wardno ELSE 'O' || NVL(b4.deptcd, '--') END, b4.doctorcd, b3.comcd_out"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN 'I' || NVL(b4.deptcd, '--') ELSE 'O' || NVL(b4.deptcd, '--') END, b4.doctorcd, b3.comcd_out"
            sSql += "       ) a"
            'sSql += " GROUP BY a.dptward, a.comcd, a.cnt"
            sSql += " ORDER BY dptward, drcd, comcd"

            alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            alParm.Add(New OracleParameter("opendt",  OracleDbType.Varchar2, PRG_CONST.OPEN_DATE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.OPEN_DATE))

            alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))
            alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

#Region " 혈액형2차"

        '-- 혈액형 결과대장
        Public Shared Function fnGet_ABOandRh_List(ByVal rsDateS As String, ByVal rsDateE As String) As DataTable
            Dim sFn As String = "fnGet_ABOandRh_List"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN FN_ACK_GET_WARD_ABBR(j.wardno) || '/' || j.roomno ELSE FN_ACK_GET_DEPT_ABBR(j.iogbn, j.deptcd) END dept,"
                sSql += "       j1.doctorrmk, j3.diagnm,"
                sSql += "       f3.spcnmd, f3.spcnmp,"
                sSql += "       r.testcd, r.viewrst, fn_ack_get_usr_name(r.regid) rstnm, fn_ack_date_str(r.rstdt, 'yyyy-mm-dd hh24:mi') rstdt"
                sSql += "  FROM lr010m r, lj011m j1, lf140m f, lf030m f3,"
                sSql += "       lj010m j, lj013m j3"
                sSql += " WHERE r.tkdt    >= :dates"
                sSql += "   AND r.tkdt    <= :datee || '5959'"
                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j.bcno     = j3.bcno (+)"
                sSql += "   AND j1.bcno    = r.bcno"
                sSql += "   AND j1.tclscd  = r.tclscd"
                sSql += "   AND j.owngbn  <> 'H'"
                sSql += "   AND SUBSTR(r.testcd, 1, 5) = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND f.bbgbn IN ('1', '2')"
                sSql += "   AND r.spccd    = f3.spccd"
                sSql += "   AND r.tkdt    >= f3.usdt"
                sSql += "   AND r.tkdt    <  f3.uedt"
                sSql += "   AND r.rstflg  IN ('2', '3')"
                sSql += " ORDER BY tkdt, bcno"

                alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_ABOandRh_Result_WGrp(ByVal rsBcNo As String, ByVal rsWkYmd As String, ByVal rsWkGrpCd As String, ByVal rsWkNoS As String, ByVal rsWkNoE As String, ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "fnGet_ABOandRH_Result_WGrp"

            Try
                Dim al As New ArrayList
                Dim sSql As String = ""

                al.Clear()

                sSql = ""
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno, "
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END dept,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       r.testcd, r.viewrst, j1.doctorrmk, j3.diagnm,"
                sSql += "       b.testcd, b.rstval, f3.spcnmd, f3.spcnmp,"
                sSql += "       fn_ack_get_usr_name(b.regid) rstnm,"
                sSql += "       fn_ack_date_str(b.regdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       fn_ack_get_usr_name(r.fnid) fnnm,"
                sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt"
                sSql += "  FROM lj010m j, lj011m j1, lf140m f, lf030m f3,"
                sSql += "       lr010m r, lj013m j3, lb070m"
                sSql += " WHERE j.bcno     = j1..bcno"
                sSql += "   AND j.bcno     = j3.bcno (+)"
                sSql += "   AND j1.bcno    = r.bcno"
                sSql += "   AND j1.tclscd  = r.tclscd"
                sSql += "   AND j.owngbn  <> 'H'"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND f.bbgbn   IN ('1', '2')"
                sSql += "   AND r.spccd    = f3.spccd"
                sSql += "   AND r.tkdt    >= f3.usdt"
                sSql += "   AND r.tkdt    <  f3.uedt"
                sSql += "   AND r.bcno     = b.bcno (+)"
                sSql += "   AND r.testcd   = b.testcd (+)"
                sSql += "   AND r.rstflg   = '3'"

                If rsRstFlg = "1" Then
                    sSql += "   AND b.testcd IS NULL"
                ElseIf rsRstFlg = "2" Then
                    sSql += "   AND b.testcd IS NOT NULL"
                End If

                If rsBcNo <> "" Then
                    sSql += "   AND j.bcno = :bcno"
                    al.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                Else
                    sSql += "   AND r.wkymd   = :wkymd"
                    sSql += "   and r.wkgrpcd = :wkgrp"
                    sSql += "   AND r.wkno   >= :wknos"
                    sSql += "   AND r.wkno   <= :wknoe"

                    al.Add(New OracleParameter("wkymd",  OracleDbType.Varchar2, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    al.Add(New OracleParameter("wkgrp",  OracleDbType.Varchar2, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                    al.Add(New OracleParameter("wknos",  OracleDbType.Varchar2, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                    al.Add(New OracleParameter("wknoe",  OracleDbType.Varchar2, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))
                End If

                sSql += " ORDER BY workno, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, al)


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_ABOandRH_Result_TGrp(ByVal rsTGrpCd As String, ByVal rsTkDtS As String, ByVal rsTkDtE As String, _
                                                          ByVal rsRstFlg As String) As DataTable
            Dim sFn As String = "fnGet_ABOandRH_Result_TGrp"

            Try
                Dim al As New ArrayList
                Dim sSql As String = ""

                al.Clear()

                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bcno_full(r.wkymd || NVL(r.wkgrpcd, '') || NVL(r.wkno, '')) workno,"
                sSql += "       fn_ack_get_bcno_full(j.bcno) bcno, j.regno, j.patnm, j.sex || '/' || j.age sexage,"
                sSql += "       fn_ack_get_dr_name(j.doctorcd) doctornm,"
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END dept,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi:ss') tkdt,"
                sSql += "       r.testcd, r.viewrst, j1.doctorrmk, j3.diagnm, b.testcd, b.rstval, f3.spcnmd, f3.spcnmp,"
                sSql += "       fn_ack_get_usr_name(b.regid) rstnm,"
                sSql += "       fn_ack_date_str(b.regdt, 'yyyy-mm-dd hh24:mi') rstdt,"
                sSql += "       fn_ack_get_usr_name(r.fnid) fnnm,"
                sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt"
                sSql += "  FROM lj010m j, lj011m j1, lf140m f, lf030m f3,"
                sSql += "       lr010m r LEFT OUTER JOIN"
                sSql += "       lj013m j3 ON (r.bcno = j3.bcno)"
                sSql += "       LEFT OUTER JOIN "
                sSql += "            lb070m b ON (r.bcno = b.bcno AND r.testcd = b.testcd)"
                sSql += " WHERE r.tkdt >= :dates"
                sSql += "   AND r.tkdt <= :datee || '5959'"

                al.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

                sSql += "   AND j.bcno     = j1.bcno"
                sSql += "   AND j1.bcno    = r.bcno"
                sSql += "   AND j1.tclscd  = r.tclscd"
                sSql += "   AND j1.owngbn <> 'H'"
                sSql += "   AND r.testcd   = f.testcd"
                sSql += "   AND r.spccd    = f.spccd"
                sSql += "   AND r.tkdt    >= f.usdt"
                sSql += "   AND r.tkdt    <  f.uedt"
                sSql += "   AND f.bbgbn   IN ('1', '2')"
                sSql += "   AND r.spccd    = f3.spccd"
                sSql += "   AND r.tkdt    >= f3.usdt"
                sSql += "   AND r.tkdt    <  f3.uedt"
                sSql += "   AND r.rstflg   = '3'"

                If rsRstFlg = "1" Then
                    sSql += "   AND b.testcd IS NULL"
                ElseIf rsRstFlg = "2" Then
                    sSql += "   AND b.testcd IS NOT NULL"
                End If


                If rsTGrpCd <> "" Then
                    sSql += "   AND (SUBSTR(r.testcd, 1, 5), r.spccd) IN (SELECT SUBSTR(testcd, 1, 5), spccd FROM lf065m WHERE tgrpcd = :tgrpcd)"
                    al.Add(New OracleParameter("tgrpcd", rsTGrpCd))
                End If

                sSql += " ORDER BY tkdt, workno, bcno"

                DbCommand()
                Return DbExecuteQuery(sSql, al)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' ABO/Rh 검사코드 가져오기
        Public Shared Sub sbGet_ABOandRH_Code(ByRef rsAboCd As String, ByRef rsRhCd As String)
            Dim sFn As String = "Public Shared Sub sbGet_ABOandRH_Code(String, String)"

            rsAboCd = "" : rsRhCd = ""

            Try
                Dim sqlDoc As String = ""
                Dim arlParm As New ArrayList

                sqlDoc += "SELECT testcd, spccd, bbgbn"
                sqlDoc += "  FROM lf140m"
                sqlDoc += " WHERE bbgbn IN ('1', '2')"


                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sqlDoc, arlParm)

                If dt.Rows.Count < 1 Then Return

                For ix As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(ix).Item("bbgbn").ToString = "1" Then
                        rsAboCd = dt.Rows(ix).Item("testcd").ToString.Trim
                    ElseIf dt.Rows(ix).Item("bbgbn").ToString = "2" Then
                        rsRhCd = dt.Rows(ix).Item("testcd").ToString.Trim
                    End If
                Next

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Sub

#End Region

#Region " 수혈의뢰 접수 "
        Public Shared Function fn_TransfusionSelectN(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            '수혈의뢰미접수리스트
            Dim sFn As String = "fn_TransfusionSelectN(String, String, String, String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList


            Try
                sSql += "pkg_ack_tns.pkg_get_tns_order_n"

                alParm.Add(New OracleParameter("rs_orddt1",  OracleDbType.Varchar2, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_orddt2",  OracleDbType.Varchar2, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))


                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm, False)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                If rsRegno <> "" Then sWhare = "bunho = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                ' < 2019-02-25 JJH 응급 구분
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim rsGbn As String = ""

                    If dt.Rows(i).Item("eryn").ToString = "○" Then rsGbn = "er"
                    If dt.Rows(i).Item("irryn").ToString = "○" Then rsGbn = "irr"
                    If dt.Rows(i).Item("ftyn").ToString = "○" Then rsGbn = "ft"

                    If rsGbn <> "" Then
                        dt.Rows(i).Item("treesortkey") = dt.Rows(i).Item("treesortkey").ToString + rsGbn
                    End If
                Next
                ' >


                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fn_TransfusionSelectJ(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            '수혈의뢰접수 데이터 트리레벨 1
            Dim sFn As String = "Public Shared Function fn_TransfusionSelectJ(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim test As String = ""

            Try
                sSql += "pkg_ack_tns.pkg_get_tns_order_j"

                alParm.Add(New OracleParameter("rs_jubsudt1",  OracleDbType.Varchar2, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_jubsudt2",  OracleDbType.Varchar2, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))


                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm, False)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                If rsRegno <> "" Then sWhare = "bunho = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt


            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fn_TransfusionSelectT(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            '수혈의뢰접수 데이터 트리레벨 2
            Dim sFn As String = "Public Shared Function fn_TransfusionSelectT(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim test As String = ""

            Try

                sSql += "pkg_ack_tns.pkg_get_tns_order_t"

                alParm.Add(New OracleParameter("rs_jubsudt1",  OracleDbType.Varchar2, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_jubsudt2",  OracleDbType.Varchar2, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm, False)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                If rsRegno <> "" Then sWhare = "bunho = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fn_TransfusionSelectNT(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            '수혈의뢰미접수 데이터 트리레벨 2
            Dim sFn As String = "Public Shared Function fn_TransfusionSelectT(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try


                sSql += "pkg_ack_tns.pkg_get_tns_order_nt"

                alParm.Add(New OracleParameter("rs_orddt1",  OracleDbType.Varchar2, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_orddt2",  OracleDbType.Varchar2, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm, False)

                Dim dr As DataRow()
                Dim sWhare As String = ""

                If rsRegno <> "" Then sWhare = "bunho = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comcd = '" + rsComcd + "'"
                If rsTnsGbn <> "" Then sWhare += IIf(sWhare = "", "", " AND ").ToString + "comgbn = '" + rsTnsGbn + "'"

                ' < 2019-02-25 JJH 응급 구분
                For i As Integer = 0 To dt.Rows.Count - 1

                    Dim rsGbn As String = ""

                    If dt.Rows(i).Item("eryn").ToString = "○" Then rsGbn = "er" '응급
                    If dt.Rows(i).Item("irryn").ToString = "○" Then rsGbn = "irr" 'IR
                    If dt.Rows(i).Item("ftyn").ToString = "○" Then rsGbn = "ft" '필터

                    If rsGbn <> "" Then
                        dt.Rows(i).Item("treesortkey") = dt.Rows(i).Item("treesortkey").ToString + rsGbn
                    End If
                Next
                ' >


                If sWhare <> "" Then
                    dr = dt.Select(sWhare, "")
                    dt = Fn.ChangeToDataTable(dr)
                End If

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fn_GetLatelyTestList(ByVal rsRegno As String) As DataTable
            ' 혈액은행 최근검사결과조회
            Dim sFn As String = "Public Shared Function fn_GetLatelyTestList(ByVal rsRegno As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                'sSql += "SELECT DISTINCT"
                'sSql += "       r.testcd, r.spccd, f.tnmd,"
                ''sSql += "       fn_ack_get_viewrst_regno(r.regno, r.testcd, r.fndt) viewrst,"
                'sSql += "       (SELECT viewrst FROM lr010m "
                'sSql += "         WHERE regno  = r.regno"
                'sSql += "           AND testcd = r.testcd"
                'sSql += "           AND rstdt  = r.fndt"
                ''sSql += "           AND rstflg = '3'"
                'sSql += "           AND rstflg in ('2','3')" ' 2019-02-08 JJH 중간보고 결과도 반영
                'sSql += "           AND ROWNUM = 1"
                'sSql += "       ) viewrst,"
                'sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                'sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt,"
                'sSql += "       r.regno,"
                'sSql += "       r.dispseq,"
                'sSql += "       r.bbgbn,"
                'sSql += "       TO_CHAR(MONTHS_BETWEEN(SYSDATE, TO_DATE(r.fndt, 'yyyymmddhh24miss'))) months_between"
                'sSql += "  FROM lf060m f,"
                'sSql += "       (SELECT r.regno, r.testcd, r.spccd, b.dispseq, b.bbgbn,"
                'sSql += "               MAX(r.tkdt)  as tkdt, MAX(r.rstdt) as fndt"
                'sSql += "          FROM lr010m r, lf140m b, lj010m j"
                'sSql += "         WHERE j.regno   = :regno"

                'alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                'sSql += "           AND j.spcflg  = '4'"
                'sSql += "           AND j.bcno    = r.bcno"
                ''sSql += "           AND r.rstflg  = '3'"
                'sSql += "           AND r.rstflg  in ('2','3')" ' 2019-02-08 JJH 중간보고 결과도 반영
                'sSql += "           AND r.testcd  = b.testcd"
                'sSql += "           AND r.spccd   = b.spccd"
                'sSql += "           AND b.trstgbn = '1'"
                'sSql += "         GROUP BY r.regno, r.testcd, r.spccd, b.dispseq, b.bbgbn "
                'sSql += "       ) r"
                'sSql += " WHERE r.testcd = f.testcd"
                'sSql += "   AND r.spccd  =  f.spccd"
                'sSql += "   AND r.tkdt   >= f.usdt"
                'sSql += "   AND r.tkdt   <  f.uedt"
                'sSql += " ORDER BY dispseq "

                '2019-12-13 JJH 검사결과가 없어도 검사명은 보이도록
                sSql = ""
                sSql += " SELECT DISTINCT                                                                                    " + vbCrLf
                sSql += "        a.testcd, a.spccd, a.tnmd,                                                                  " + vbCrLf
                sSql += "        (SELECT nvl(viewrst, '검사중')                                                              " + vbCrLf
                sSql += "           FROM lr010m                                                                              " + vbCrLf
                sSql += "          WHERE regno = b.regno                                                                     " + vbCrLf
                sSql += "            AND testcd = b.testcd                                                                   " + vbCrLf
                sSql += "            AND tkdt  = (select max(tkdt) from lr010m where bcno = b.lastbcno and regno = b.regno and testcd = b.testcd and spccd = b.spccd )                                                                     " + vbCrLf
                sSql += "            AND rstflg IN ('2', '3')                                                                " + vbCrLf
                sSql += "            AND ROWNUM = 1 ) viewrst,                                                               " + vbCrLf
                'sSql += "         fn_ack_date_str(b.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,                                        " + vbCrLf
                sSql += "         fn_ack_date_str((select max(tkdt) from lr010m where bcno = b.lastbcno and regno = b.regno and testcd = b.testcd and spccd = b.spccd ), 'yyyy-mm-dd hh24:mi') tkdt,                                        " + vbCrLf
                'sSql += "         fn_ack_date_str(b.fndt, 'yyyy-mm-dd hh24:mi') fndt,                                        " + vbCrLf
                sSql += "         fn_ack_date_str((select max(rstdt) from lr010m where bcno = b.lastbcno and regno = b.regno and testcd = b.testcd and spccd = b.spccd ), 'yyyy-mm-dd hh24:mi') fndt,                                        " + vbCrLf
                sSql += "         b.regno, a.dispseq, a.bbgbn,                                                               " + vbCrLf
                sSql += "         TO_CHAR(MONTHS_BETWEEN(SYSDATE, TO_DATE((select max(rstdt) from lr010m where bcno = b.lastbcno and regno = b.regno and testcd = b.testcd and spccd = b.spccd ), 'yyyymmddhh24miss'))) months_between       " + vbCrLf
                sSql += "    FROM (SELECT f6.tnmd, f4.testcd, f4.spccd, f4.bbgbn,  f4.dispseq                                " + vbCrLf
                sSql += "            FROM LF140M f4, LF060M f6                                                               " + vbCrLf
                '20210721 jhs 하드코딩 되어있는 부분 수정 - 기초마스터 혈액은행 관련 검사 표시순서가 적혀져 있는 것만 최근검사 항목으로 조회 
                'sSql += "           WHERE F4.TESTCD IN ('LH103','LH109','LH21103','LH212','LB110','LB112','LB151','LB142') " + vbCrLf
                'sSql += "           WHERE F4.TESTCD IN ('LH103','LH109','LH21103','LH212','LB110','LB112','LB151','LB142','LG126') " + vbCrLf
                sSql += "           WHERE F4.TESTCD IN (selecT testcd from lf140m where replace(dispseq,' ', '') <> ' ')     " + vbCrLf
                '----------------------------------------------------------------------------------
                sSql += "             AND F4.TESTCD = F6.TESTCD                                                              " + vbCrLf
                sSql += "             AND F4.SPCCD  = F6.SPCCD                                                               " + vbCrLf
                sSql += "             AND F6.UEDT  >= FN_ACK_SYSDATE()                                                       " + vbCrLf
                sSql += "         ) A                                                                                        " + vbCrLf
                sSql += "    LEFT JOIN                                                                                       " + vbCrLf
                sSql += "         (SELECT r.regno, r.testcd, r.spccd,                                                        " + vbCrLf
                sSql += "                        max(r.bcno) lastbcno                                                           " + vbCrLf
                'sSql += "                 MAX(r.tkdt) as tkdt, MAX(r.rstdt) as fndt                                          " + vbCrLf
                sSql += "            FROM LR010M r, lj010m j, lf140m f                                                       " + vbCrLf
                sSql += "           WHERE j.regno = :regno                                                                   " + vbCrLf

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                sSql += "             AND j.spcflg = '4'                                                                     " + vbCrLf
                sSql += "             AND r.testcd = f.testcd                                                                " + vbCrLf
                sSql += "             AND r.spccd  = f.spccd                                                                 " + vbCrLf
                sSql += "             AND r.bcno   = j.bcno                                                                  " + vbCrLf
                'sSql += "             AND r.rstflg IN ('2', '3')                                                             " + vbCrLf
                sSql += "        GROUP BY r.regno, r.testcd, r.spccd                                                         " + vbCrLf
                sSql += "         ) B ON a.testcd = b.testcd AND a.spccd = b.spccd                                           " + vbCrLf
                sSql += "    ORDER BY a.dispseq                                                                              " + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_ABORh(ByVal rsRegno As String) As DataTable
            ' 혈액은행 혈액형 결과
            Dim sFn As String = "Public Shared Function fnGet_ABORh(String, String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT abo || rh aborh FROM lr070m WHERE regno = :regno"

                alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_AntibodyTest_Rst(ByVal rsRegno As String) As DataTable
            ' 혈액은행 안티바디스크린검사 결과
            Dim sFn As String = "Public Shared Function fnGet_AntibodyTest_Rst(ByVal rsRegno As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Try
                sSql = ""
                sSql += " SELECT NVL(r.viewrst,'ⓣ') rst, fn_ack_date_str (r.rstdt, 'yyyy-MM-dd hh24:mi') rstdt   "
                sSql += "   FROM lr010m r"
                sSql += "   JOIN lf140m f"
                sSql += "     ON r.testcd = f.testcd"
                sSql += "    AND r.spccd = f.spccd"
                sSql += "    AND f.bbgbn = '6'"
                sSql += "  WHERE r.regno = :regno"
                sSql += " ORDER BY rstdt DESC"

                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fnGet_TnsJubsuState(ByVal rsTnsNo As String, ByVal rsComCd As String, ByVal rsState As String, ByVal rsFkOcs As String) As Boolean
            '수혈의뢰미접수리스트
            Dim sFn As String = "fnGet_TnsJubsuState(String, String, String, String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT tnsjubsuno, bldno, comcd_out, state"
                sSql += "  FROM lb043m"
                sSql += " WHERE tnsjubsuno = :tnsno"
                sSql += "   AND comcd      = :comcd"

                alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                alParm.Add(New OracleParameter("comcd",  OracleDbType.Varchar2, rsComCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComCd))

                If rsFkOcs.IndexOf("-") >= 0 Then
                    sSql += "   AND fkocs   = :fkocs"
                    sSql += "   AND seq     = :seq"

                    alParm.Add(New OracleParameter("fkocs",  OracleDbType.Varchar2, rsFkOcs.Split("-"c)(0).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs.Split("-"c)(0)))
                    alParm.Add(New OracleParameter("seq", OracleDbType.NChar, rsFkOcs.Split("-"c)(1).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs.Split("-"c)(1)))

                Else
                    sSql += "   AND fkocs   = :fkocs"

                    alParm.Add(New OracleParameter("fkocs",  OracleDbType.Varchar2, rsFkOcs.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs))
                End If

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item("state").ToString = rsState Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

        Public Shared Function fn_GetPatInfo(ByVal rsRegno As String) As DataTable
            ' 환자명 리턴
            Dim sFn As String = "Public Shared Function fn_GetPatnm(ByVal rsRegno As String) As DataTable"
            Dim sSql As String = ""
            Dim aryList As New ArrayList

            Try
                sSql += "SELECT fn_ack_get_pat_info(:regno, '', '') as patinfo FROM DUAL"

                aryList.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                Return DbExecuteQuery(sSql, aryList)
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_GetBcnoNormal(ByVal rsBcno As String) As DataTable
            ' 검체번호리턴
            Dim sFn As String = "Public Shared Function fn_GetPatnm(ByVal rsRegno As String) As DataTable"
            Dim sSql As String = ""
            Dim aryList As New ArrayList

            Try
                sSql += "SELECT fn_ack_get_bcno_normal(:bcno) as bcno FROM DUAL"

                aryList.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                fn_GetBcnoNormal = DbExecuteQuery(sSql, aryList)
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_ChkKeepSpcNo(ByVal rsBcno As String) As DataTable
            ' 보관검체 등록여부 체크
            Dim sFn As String = "Public Shared Function fn_ChkKeepSpcNo(ByVal rsBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim aryList As New ArrayList

            Try
                sSql += "SELECT COUNT(regno) as cnt "
                sSql += "  FROM lb080m"
                sSql += " WHERE keepspcno = :bcno"

                aryList.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                fn_ChkKeepSpcNo = DbExecuteQuery(sSql, aryList)
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_GetKeepNo(ByVal rsDate As String) As DataTable
            ' 보관검체 번호 리턴
            Dim sFn As String = "Public Shared Function fn_GetKeepNo(ByVal rsDate As String) As DataTable"
            Dim sSql As String = ""
            Dim aryList As New ArrayList

            Try
                sSql += "SELECT NVL(MAX(SUBSTR(keepspcno, 0, 11)), 0)  as seq"
                sSql += "  FROM lb080m"
                sSql += " WHERE keepspcno LIKE :bcno || '%'"

                aryList.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                DbCommand()
                fn_GetKeepNo = DbExecuteQuery(sSql, aryList)
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_GetBcno(ByVal rsBcno As String) As String
            Dim sFn As String = "Public Shared Function fn_GetBcno(ByVal rsBcno As String) As String"

            Dim dt As DataTable
            Dim sSql As String
            Dim alList As New ArrayList

            dt = fn_GetBcnoNormal(rsBcno)

            alList = fn_GetSelectItem(dt, 1)

            sSql = alList(0).ToString

            Return sSql

        End Function

        Public Shared Function fn_GetBcnoAbleChk(ByVal rsBcno As String) As DataTable
            ' 검체의 사용여부 체크 및 등록번호, 채혈일시 리턴
            Dim sFn As String = "Public Shared Function fn_GetBcnoAbleChk(ByVal rsBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim aryList As New ArrayList

            Try
                sSql += "SELECT a.regno,"
                sSql += "       fn_ack_date_str(b.colldt, 'yyyy-MM-dd hh24:mi:ss') as colldt,"
                sSql += "       CASE WHEN SYSDATE - TO_DATE(b.colldt, 'yyyyMMddhh24miss') > 3"
                sSql += "            THEN 'false'"
                sSql += "            ELSE 'true'"
                sSql += "       END  availyn, a.spcflg" ' 2019-02-11 JJH 상태값 추가
                sSql += "  FROM lj010m a,"
                sSql += "       lj011m b,"
                sSql += "       lf010m f"
                sSql += " WHERE a.bcno      = :bcno"
                sSql += "   AND a.bcno      = b.bcno"
                sSql += "   AND a.bcclscd   = f.bcclscd"
                sSql += "   AND a.bcprtdt  >= f.usdt"
                sSql += "   AND a.bcprtdt  <  f.uedt"
                sSql += "   AND f.bcclsgbn IN ('1', '2', '3', '8')"
                sSql += "   AND ROWNUM = 1"

                aryList.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

                DbCommand()
                Return DbExecuteQuery(sSql, aryList)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_GetKeepInfo(ByVal rsKeepBcno As String) As DataTable
            ' 보관검체 번호로 등록번호 조회
            Dim sFn As String = "Public Shared Function fn_GetKeepInfo(ByVal rsKeepBcno As String) As DataTable"
            Dim sSql As String = ""
            Dim aryList As New ArrayList

            Try
                sSql += "SELECT regno,"
                sSql += "       fn_ack_date_str(ustm, 'yyyy-MM-dd hh24:mi:ss') as ustm"
                sSql += "  FROM lb080m"
                sSql += " WHERE keepspcno = :bcno"

                aryList.Add(New OracleParameter("bcno",  OracleDbType.Varchar2, rsKeepBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsKeepBcno))

                DbCommand()
                fn_GetKeepInfo = DbExecuteQuery(sSql, aryList)
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function


#Region "가출고 쿼리 취소"
        Public Shared Function Select_TnsJubsu(ByVal rsHour As String, ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsRef As String) As DataTable
            Dim sFn As String = "Function Select_TnsJubsu(String, String, String, String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Dim dbHour As Double = CType(rsHour, Double) / 24
            Dim dtDiff As Date = Date.FromOADate(dbHour)    ' FromOADate - 지정된 OLE 자동화 날짜에 해당하는 DateTime을 반환

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       a.tnsjubsuno, fn_ack_date_str(a.jubsudt, 'yyyy-mm-dd hh24:mi') jubsudt,"
                sSql += "       a.regno, a.patnm, a.sex || '/' || a.age sexage,"
                sSql += "       fn_ack_get_dr_name(a.doctorcd) doctornm, FN_ACK_GET_DEPT_ABBR(a.iogbn, a.deptcd) deptcd, FN_ACK_GET_WARD_ABBR(a.wardno) wardno,"
                sSql += "       fn_ack_date_str(a.opdt, 'yyyy-mm-dd') opdt, b.ir, b.filter,"
                sSql += "       b.reqqnt, b.befoutqnt, b.outqnt, b.abnqnt, b.rtnqnt, b.cancelqnt, b.doctorrmk,"
                sSql += "       b.comcd, d.comnmd"
                sSql += "  FROM lb040m a, lb042m b, lf120m d"
                sSql += " WHERE a.tnsjubsuno = b.tnsjubsuno"
                sSql += "   AND b.comcd      = d.comcd"
                sSql += "   AND a.jubsudt   >= d.usdt"
                sSql += "   AND a.jubsudt   <  d.uedt"
                sSql += "   AND b.befoutqnt >  0"       ' 출고 미완료인 경우!! - > 가출고인것만 
                'sSql += "   AND b.outqnt + b.rtnqnt + b.abnqnt = 0" '-- 국립의료원인 경우 접수단위로 취소
                sSql += "   AND b.delflg     = '0'"     ' 0: 조회, 1: 삭제
                sSql += "   AND a.jubsudt   >= :dates "
                sSql += "   AND a.jubsudt   <= :datee || '235959'"

                If rsRef = "0" Then ' 수혈의뢰 접수후 출고미완료로 72시간 경과된 order
                    sSql += "   AND SYSDATE - TO_DATE(a.jubsudt, 'yyyymmddhh24miss') >= " + dbHour.ToString
                ElseIf rsRef = "1" Then    '출고 미완료로 수술예정일이 지나버린 order 
                    sSql += "   AND a.opdt < fn_ack_sysdate"
                End If

                sSql += " ORDER BY jubsudt ASC"

                alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function Select_BefOutInfo(ByVal rsTnsNo As String, ByVal rsComCd As String) As DataTable
            Dim sFn As String = "Function Select_BefOutInfo(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT"
                sSql += "       b2.dongbn, b2.abo, b2.rh, b2.abo || b2.rh aborh, "
                sSql += "       fn_ack_date_str(b2.dondt, 'yyyy-mm-dd hh24') dondt,"
                sSql += "       fn_ack_date_str(b2.indt, 'yyyy-mm-dd hh24:ss') indt,"
                sSql += "       fn_ack_date_str(b2.availdt, 'yyyy-mm-dd hh24:ss') availdt,"
                sSql += "       fn_ack_date_str(b3.befoutdt, 'yyyy-mm-dd hh24:ss') befoutdt,"
                sSql += "       fn_ack_get_usr_name(b3.testid) testnm, b3.comcd, b3.comcd_out,"
                sSql += "       fn_ack_get_bldno_full(b3.bldno) bldno,"
                sSql += "       b3.tnsjubsuno, b43.comnm, b3.befoutdt,"
                sSql += "       b43.owngbn, b43.iogbn, b43.spccd, b43.state statecd, "
                sSql += "       b43.fkocs || '-' || TO_CHAR(b43.seq) fkocs, b4.orddt, b4.regno"
                sSql += "  FROM lb040m b4, lb042m b42, lb043m b43, lb020m b2, lb030M b3"
                sSql += " WHERE b4.tnsjubsuno  = :tnsno"
                sSql += "   AND b4.tnsjubsuno  = b42.tnsjubsuno"
                sSql += "   AND b4.tnsjubsuno  = b43.tnsjubsuno"
                'sSql += "   AND b43.comcd_out   = :comcd"
                sSql += "   AND b43.tnsjubsuno = b3.tnsjubsuno"
                sSql += "   AND b43.comcd_out  = b3.comcd_out"
                sSql += "   AND b43.bldno      = b3.bldno"
                sSql += "   AND b3.bldno       = b2.bldno"
                sSql += "   AND b3.comcd_out   = b2.comcd"
                sSql += "   AND b43.state      = '3'"      '-- 국립의료원인 경우 접수 단위로 취소 가능
                sSql += " ORDER BY b3.befoutdt DESC"

                alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                'alParm.Add(New OracleParameter("comcd",  OracleDbType.Varchar2, rsComCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

#Region " 수혈 공통 업데이트 쿼리 "

        ' 과거수혈내역 조회
        Public Shared Function fn_GetPastTnsList(ByVal rsRegno As String, Optional ByVal rsDate As String = "", Optional ByVal rsTnsnum As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fn_GetPastTnsList(ByVal rsRegno As String, Optional ByVal rsdate As String = "", Optional ByVal rstnsnum As String = "") As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT fn_ack_get_tnsjubsuno_full(a.tnsjubsuno)  as tnsjubsuno  "
                sSql += "     , CASE WHEN a.tnsgbn = '1' THEN '준비'                 "
                sSql += "            WHEN a.tnsgbn = '2' THEN '수혈'                 "
                sSql += "            WHEN a.tnsgbn = '3' THEN '응급'                 "
                sSql += "            WHEN a.tnsgbn = '4' THEN 'Irra'                 "
                sSql += "       END                                   as tnsgbn      "
                sSql += "     , b.comnmd                              as comnm       "
                sSql += "     , a.reqqnt                                             "
                sSql += "     , a.outqnt                                             "
                sSql += "     , a.rtnqnt                                             "
                sSql += "     , a.abnqnt                                             "
                sSql += "     , a.cancelqnt                                          "
                sSql += "  FROM (SELECT a.tnsjubsuno                                 "
                sSql += "             , a.tnsgbn                                     "
                sSql += "             , a.regno                                      "
                sSql += "             , a.orddt                                      "
                sSql += "             , a.delflg                                     "
                sSql += "             , b.comcd                                      "
                sSql += "             , b.spccd                                      "
                sSql += "             , b.reqqnt                                     "
                sSql += "             , b.outqnt                                     "
                sSql += "             , b.rtnqnt                                     "
                sSql += "             , b.abnqnt                                     "
                sSql += "             , b.cancelqnt                                  "
                sSql += "             , a.jubsudt                                    "
                sSql += "          FROM lb040m a, /* 수혈접수정보 */                 "
                sSql += "               lb042m b  /* 수혈의뢰정보 */                 "
                sSql += "         WHERE a.tnsjubsuno =  b.tnsjubsuno                 "
                sSql += "           AND a.regno      =  :regno                            "

                alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                If rstnsnum <> "" Then
                    sSql += "           AND a.tnsjubsuno <> :tnsno "
                    alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rstnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstnsnum))
                End If

                If rsDate <> "" Then
                    sSql += "       AND a.jubsudt <= :jubsudt || '235959' ) a "
                    alParm.Add(New OracleParameter("jubsudt",  OracleDbType.Varchar2, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-"c, "")))
                End If

                sSql += "     , lf120m b /* 성분제제 */                              "
                sSql += " WHERE a.comcd                 = b.comcd                    "
                sSql += "   AND a.spccd                 = b.spccd                    "
                sSql += "   AND a.delflg                <> 'D'                       "
                sSql += "   AND NVL(b.ftcd, 'N') = 'N'                        "
                sSql += "ORDER BY tnsjubsuno DESC                                    "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 과거수혈내역 조회
        Public Shared Function fn_GetPastTnsDetailList(ByVal rsTnsNo As String, ByVal rsRegNo As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetPastTnsDetailList(String, string) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_tnsjubsuno_full(a.tnsjubsuno)  as tnsjubsuno"
                sSql += "     , CASE WHEN a.tnsgbn = '1' THEN '준비'                 "
                sSql += "            WHEN a.tnsgbn = '2' THEN '수혈'                 "
                sSql += "            WHEN a.tnsgbn = '3' THEN '응급'                 "
                sSql += "            WHEN a.tnsgbn = '4' THEN 'Irra'                 "
                sSql += "       END                                         as tnsgbn"
                sSql += "     , CASE WHEN b.state = '1' THEN '접수'                 "
                sSql += "            WHEN b.state = '2' THEN '검사'                 "
                sSql += "            WHEN b.state = '3' THEN '가출고'               "
                sSql += "            WHEN b.state = '4' THEN '출고'                 "
                sSql += "            WHEN b.state = '5' THEN '반납'                 "
                sSql += "            WHEN b.state = '6' THEN '폐기'                 "
                sSql += "       END                                         as state"
                sSql += "     , b.abo || b.rh                               as aborh_p"
                sSql += "     , NVL(d.comnmd, b.comnm)                      as comnmd"
                sSql += "     , fn_ack_get_bldno_full(b.bldno)              as bldno"
                sSql += "     , c.abo || c.rh                               as aborh_b"
                sSql += "     , fn_ack_date_str(NVL(e.testdt, f.testdt), 'YYYY-MM-DD HH24:MI') as testdt"
                sSql += "     , fn_ack_get_usr_name(NVL(e.testid, f.testid))   AS testnm"
                sSql += "     , fn_ack_date_str(NVL(e.befoutdt, f.befoutdt), 'YYYY-MM-DD HH24:MI') as befoutdt"
                sSql += "     , fn_ack_get_usr_name(NVL(e.befoutid, f.befoutid))   AS befoutnm"
                sSql += "     , fn_ack_date_str(NVL(e.outdt, f.outdt), 'YYYY-MM-DD HH24:MI') as outdt"
                sSql += "     , fn_ack_get_usr_name(NVL(e.outid, f.outid))   AS outnm"
                sSql += "     , NVL(e.recnm, f.recnm)                       AS recnm"
                sSql += "     , fn_ack_date_str(f.rtndt, 'YYYY-MM-DD HH24:MI') AS rtndt"
                sSql += "     , fn_ack_get_usr_name(f.rtnid)                AS rtnnm"
                sSql += "     , f.rtnreqnm                                  AS rtnreqnm"
                sSql += "  FROM lb040m a, lb043m b, lb020m c, lf120m d, lb030m e, lb031m f"
                sSql += " WHERE a.tnsjubsuno = b.tnsjubsuno"
                sSql += "   AND b.bldno      = c.bldno (+)"
                sSql += "   AND b.comcd_out  = c.comcd (+)"
                sSql += "   AND b.comcd_out  = d.comcd (+)"
                sSql += "   AND SUBSTR(b.tnsjubsuno, 1, 8)            >= d.usdt (+)"
                sSql += "   AND SUBSTR(b.tnsjubsuno, 1, 8) || '23595' <  d.uedt (+)"
                sSql += "   AND b.tnsjubsuno = e.tnsjubsuno (+)"
                sSql += "   AND b.bldno      = e.bldno (+)"
                sSql += "   AND b.comcd_out  = e.comcd_out (+)"
                sSql += "   AND b.tnsjubsuno = f.tnsjubsuno (+)"
                sSql += "   AND b.bldno      = f.bldno (+)"
                sSql += "   AND b.comcd_out  = f.comcd_out (+)"
                sSql += "   AND NVL(b.state, '0') <> '0'"

                If rsTnsNo = "" Then
                    sSql += "   AND a.regno = ?"
                    alParm.Add(New OleDb.OleDbParameter("regno", rsRegNo))
                Else
                    sSql += "   AND a.tnsjubsuno = ?"
                    alParm.Add(New OleDb.OleDbParameter("tnsno", rsTnsNo))
                End If

                sSql += " ORDER BY tnsjubsuno DESC, outdt DESC                                    "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 보관검체내역 조회
        Public Shared Function fn_GetKeepSpcList(ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetKeepSpcList(ByVal rsRegno As String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT bcno, tkdt, keepplace, spcnmd, min(abo) abo, min(rh) rh, min(crossm) crossm, min(irr) irr, min(abscreen) abscreen, testcd"
                sSql += "  FROM ("
                sSql += "        SELECT fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "               fn_ack_date_str(j1.tkdt, 'yyyy-MM-dd hh24:mi') tkdt,"
                sSql += "               r.testcd, r.spccd, f3.spcnmd, f14.bbgbn,"
                sSql += "               CASE WHEN f14.bbgbn = '1' THEN NVL(r.viewrst, 'ⓣ')"
                sSql += "                    WHEN f14.bbgbn = '3' THEN NVL(r.viewrst, 'ⓣ')"
                sSql += "                    ELSE 'x'"
                sSql += "               END abo,"
                sSql += "               CASE WHEN f14.bbgbn = '2' THEN NVL(r.viewrst, 'ⓣ')"
                sSql += "                    ELSE 'x'"
                sSql += "               END rh,"
                sSql += "               CASE WHEN NVL(k.bcno, ' ') <> ' ' THEN 'ⓣ'"
                sSql += "                    ELSE 'x'"
                sSql += "               END crossm,"
                sSql += "               CASE WHEN f14.bbgbn = '9' THEN 'ⓣ'"
                sSql += "                    ELSE 'x'"
                sSql += "               END irr,"
                sSql += "               CASE WHEN f14.bbgbn = '6' THEN NVL(r.viewrst, 'ⓣ')"
                sSql += "                    ELSE 'x'"
                sSql += "               END abscreen,"
                sSql += "               k.rackid || '/' || k.numrow || '/' || k.numcol keepplace"
                sSql += "          FROM lj010m j, lf030m f3,"
                sSql += "               (SELECT testcd, spccd, dispseq, bbgbn"
                sSql += "                  FROM lf140m"
                sSql += "                 WHERE bbgbn in ('1', '2', '3', '6', '7', '9')"
                sSql += "               ) f14,"
                sSql += "               lj011m j1, lr010m r, lk010m k"
                sSql += "         WHERE j.regno   =  :regno"

                alParm.Add(New OracleParameter("regno",  OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                sSql += "           AND j.spcflg  >  '3'"
                sSql += "           AND j.bcno     = j1.bcno "
                sSql += "           AND j1.colldt >  fn_ack_get_date(SYSDATE - 3)"
                sSql += "           AND j1.bcno    = r.bcno"
                sSql += "           AND j1.tclscd  = r.tclscd"
                sSql += "           AND j.spccd    = f3.spccd"
                sSql += "           AND f3.usdt   <= j1.colldt"
                sSql += "           AND f3.uedt   >  j1.colldt"
                sSql += "           AND r.testcd   = f14.testcd"
                sSql += "           AND r.spccd    = f14.spccd"
                sSql += "           AND r.bcno     = k.bcno (+)"
                sSql += "       ) a"
                sSql += " GROUP BY bcno, tkdt, keepplace, spcnmd, testcd"
                sSql += " ORDER BY tkdt DESC"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

#Region " CrossMatching 등록(가출고) "

        '-- 용량변경 쿼리
        Public Shared Function fnGet_BldQntChg_List(ByVal rsTnsNo As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fn_PreOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try


                alParm.Add(New OracleParameter("rs_tnsno", rsTnsNo))
                alParm.Add(New OracleParameter("rs_usrid", USER_INFO.USRID))

                DbCommand()
                Return DbExecuteQuery("pkg_ack_tns.pkg_get_tns_chg_list", alParm, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '20211115 jhs 적혈구제제 프로시저 추가
        ' 적혈구제제 관리 데이터 가져오기
        Public Shared Function fnGet_trans_mgt(ByVal rsStDt As String, ByVal rsEnDt As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fnGet_trans_mg(ByVal rsTnsNo As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try

                alParm.Add(New OracleParameter("rs_stdt", rsStDt))
                alParm.Add(New OracleParameter("rs_endt", rsEnDt))

                DbCommand()
                Return DbExecuteQuery("pkg_ack_tns.pkg_get_trans_mgt", alParm, False)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_trans_mgt_new(ByVal rs_date1 As String, ByVal rs_date2 As String, ByVal rs_sort As String, ByVal rs_sorting As String, ByVal rs_regno As String) As DataTable
            Dim sFn As String = "Public Shared Function fnGet_trans_mgt_new(ByVal rs_date1 As String, ByVal rs_date2 As String) As DataTable"

            Try

                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += " SELECT  '' chk,  bc1.tnsjubsuno, bc1.regdt , FN_ACK_DATE_STR(TO_CHAR(vw_ord.FSTRGSTDT, 'yyyymmddhh24miss'), 'yyyy-mm-dd hh24:mi:ss') FSTRGSTDT , 	" + vbCrLf
                sSql += " 	      B4.regno        , b4.patnm  , B4.SEX || '/' || b4.age sexage, FN_ACK_GET_DEPT_NAME(b4.iogbn, b4.deptcd) deptnm, 							" + vbCrLf
                sSql += " 	      r7.abo || r7.rh aborh, b43.bldno,  FN_ACK_DATE_STR(b3.outdt, 'yyyy-mm-dd hh24:mi:ss')  outdt, 											" + vbCrLf
                sSql += " 	       case when b43.state = '0' then '취소'                                                                                                      " + vbCrLf
                sSql += " 	              when b43.state = '1' then '접수'                                                                                                    " + vbCrLf
                sSql += " 	              when b43.state = '2' then '검사중'                                                                                                   " + vbCrLf
                sSql += " 	              when b43.state = '3' then '가출고'                                                                                                   " + vbCrLf
                sSql += " 	              when b43.state = '4' then '출고'                                                                                                    " + vbCrLf
                sSql += " 	              when b43.state = '5' then '반납'                                                                                                    " + vbCrLf
                sSql += " 	              when b43.state = '6' then '폐기'                                                                                                    " + vbCrLf
                sSql += " 	              end as state,                                                                                                                             " + vbCrLf
                sSql += " 	      FN_ACK_GET_USR_NAME(bc1.dlmcaller) dlmcaller, bc1.cmtcont, bc1.cmcaller,                                                                  " + vbCrLf
                sSql += " 	      fn_ack_trans_mgt(B4.REGNo,  B4.jubsudt,'1','1') bfviewrst,                                                                                    " + vbCrLf
                sSql += " 	      FN_ACK_DATE_STR(fn_ack_trans_mgt(B4.REGNo,  B4.jubsudt,'1','2') ,'yyyy-mm-dd hh24:mi:ss') bffndt,                                             " + vbCrLf
                sSql += " 	      fn_ack_trans_mgt(B4.REGNo,  B4.jubsudt,'2','1') afviewrst,                                                                                " + vbCrLf
                sSql += " 	      FN_ACK_DATE_STR(fn_ack_trans_mgt(B4.REGNo,  B3.outdt,'2','2')  ,'yyyy-mm-dd hh24:mi:ss') affndt,                                              " + vbCrLf
                sSql += " 	      bc1.hgyn , bc1.cbcyn, bc1.allyn , bc1.ecptyn, bc1.seq                                                                                         " + vbCrLf
                sSql += " 	 , ROW_NUMBER() over(PARTITION BY BC1.TNSJUBSUNO ORDER BY bc1.TNSJUBSUNO, bc1.seq, b3.outdt) AS row_num                                         " + vbCrLf
                sSql += "     from LBC10m bc1                                                                                                                                   " + vbCrLf
                sSql += "     inner join lb040m b4                                                                                                                          " + vbCrLf
                sSql += "        on bc1.tnsjubsuno = b4.tnsjubsuno                                                                                                          " + vbCrLf
                sSql += "     inner join lb043m b43                                                                                                                         " + vbCrLf
                sSql += "        on bc1.tnsjubsuno = b43.tnsjubsuno                                                                                                         " + vbCrLf
                sSql += "     inner join lb030m b3                                                                                                                          " + vbCrLf 'left join lb030m b3 left조인걸면 혈액불출증에 모든 혈액번호 취소된것까지  다 나옴 하지만 출고 되었는지 안되었는지 알고 싶은 쿼리기 때문에 굳이 취소 된것 안보여도 될 듯 
                sSql += "        on bc1.tnsjubsuno = b3.tnsjubsuno  and   B43.BLDNO = b3.bldno " + vbCrLf
                sSql += "     inner join (selecT o.patno, O.orddate, o.ordseqno, C.FSTRGSTDT " + vbCrLf
                sSql += "                       from VW_ACK_OCS_ORD_INFO o LEFT OUTER JOIN EMR.MNRMDEEX c                                                                        " + vbCrLf
                sSql += "                                                             ON c.prcpdd = o.orddate                                                                  " + vbCrLf
                sSql += "                                                           AND c.instcd = '031'                                                                       " + vbCrLf
                sSql += "                                                           AND c.execprcpuniqno = o.ordseqno                                                         " + vbCrLf
                sSql += "                                                           AND c.prcpno = o.prcpno  " + vbCrLf
                sSql += "                         WHERE o.instcd = '031'           " + vbCrLf
                sSql += "                          aND o.prcpclscd = 'B4'         " + vbCrLf
                sSql += "                          AND o.hscttempprcpflag = 'N'   " + vbCrLf
                sSql += "                          AND o.execprcphistcd = 'O'     " + vbCrLf
                sSql += "                          AND nvl(o.discyn, 'N') = 'N') vw_ord" + vbCrLf
                sSql += "                  on b43.regno = vw_ord.patno  " + vbCrLf
                sSql += "                and substr(b43.fkocs, 12,8) = vw_ord.orddate " + vbCrLf
                sSql += "                and substr(b43.fkocs, 21,9) = vw_ord.ordseqno " + vbCrLf
                sSql += "     inner join lr070m r7" + vbCrLf
                sSql += "        on b4.regno = r7.regno" + vbCrLf
                sSql += "     where  b4.jubsudt  >= :date1 || '0000'" + vbCrLf
                sSql += " 	    and  b4.jubsudt  <= :date2 || '5959'" + vbCrLf
                sSql += "       and  b4.regno     = nvl(:regno, b4.regno) " + vbCrLf

                Dim sort As String = ""
                If rs_sort = "T" Then
                    sort = "tnsjubsuno " + IIf(rs_sorting = "D", "desc", "").ToString() + ", regno, seq, row_num"
                Else
                    sort = "regno " + IIf(rs_sorting = "D", "desc", "").ToString() + ", tnsjubsuno, seq, row_num"
                End If

                sSql += "     order by " + sort

                alParm.Add(New OracleParameter("date1", rs_date1))
                alParm.Add(New OracleParameter("date2", rs_date2))
                alParm.Add(New OracleParameter("regno", rs_regno))

                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                MsgBox(msFile & sFn & ex.Message)
                Return New DataTable
            End Try

        End Function
        '20220124 jhs 혈액 TAT  쿼리 
        ' 적혈구제제 관리 데이터 가져오기
        Public Shared Function fnGet_BloodTat(ByVal rsStDt As String, ByVal rsEnDt As String, ByVal rsBranchComcd As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fnGet_trans_mg(ByVal rsTnsNo As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList


            Try
                sSql = " "
                sSql += " selecT b4.tnsjubsuno, b4.regno, b4.patnm, b4.sex || '/' || b4.age sexage                                                  " + vbCrLf
                sSql += " , nvl(bc2.gwa, fn_ack_get_dept_name(b4.iogbn, b4.deptcd)) deptnm                                  " + vbCrLf '이형수혈에서 데이터 변경 없으면 그냥 접수한 진료과로 표시
                sSql += " , b4.iogbn ,  bc2.varyn                                                                           " + vbCrLf ' 이형수혈 여부
                sSql += " , b3.comcd_out, b43.spccd                                                                         " + vbCrLf
                sSql += " , FN_ACK_GET_WARD_ABBR(b4.wardno) || '/' || b4.roomno ws                                          " + vbCrLf
                sSql += " , fn_ack_get_dr_name(b4.doctorcd) doctornm                                                        " + vbCrLf
                sSql += " , case when b4.tnsgbn = '1' then '준비'                                                           " + vbCrLf
                sSql += "        when b4.tnsgbn = '2' then '수혈'                                                           " + vbCrLf
                sSql += "        when b4.tnsgbn = '3' then '응급'                                                           " + vbCrLf
                sSql += "        when b4.tnsgbn = '4' then 'Irra'                                                           " + vbCrLf
                sSql += "        else '' end tnsgbn                                                                         " + vbCrLf
                sSql += " , case when b43.state = '1' then '접수'                                                           " + vbCrLf
                sSql += "        when b43.state = '2' then '검사중'                                                         " + vbCrLf
                sSql += "        when b43.state = '3' then '가출고'                                                         " + vbCrLf
                sSql += "        when b43.state = '4' then '출고'                                                           " + vbCrLf
                sSql += "        when b43.state = '5' then '반납'                                                           " + vbCrLf
                sSql += "        when b43.state = '6' then '폐기'                                                           " + vbCrLf
                sSql += "        else '' end state                                                                          " + vbCrLf
                sSql += " ,  fn_ack_get_bldno_full(b43.bldno) bldno                                                         " + vbCrLf
                sSql += " , b43.comnm, b43.abo || b43.rh  bldaborh                                                          " + vbCrLf
                sSql += " ,  r7.abo || r7.rh pataborh                                                                       " + vbCrLf
                sSql += " ,  fn_ack_date_str(b4.orddt, 'yyyy-mm-dd hh24:mi') orddt                                          " + vbCrLf
                sSql += " ,  fn_ack_date_str(nvl(TO_CHAR(vw_ord.FSTRGSTDT, 'yyyymmddhh24miss'), b4.orddt), 'yyyy-mm-dd hh24:mi') FSTRGSTDT                      " + vbCrLf
                sSql += " ,  fn_ack_date_str_bld_gt(b4.regno,nvl(b3.outdt, to_char(sysdate, 'yyyymmddhh24miss'))) crosstkdt                                     " + vbCrLf
                sSql += " ,  fn_ack_date_str(b4.jubsudt, 'yyyy-mm-dd hh24:mi') jubsudt                                                                          " + vbCrLf
                sSql += " ,  fn_ack_date_str(b3.befoutdt, 'yyyy-mm-dd hh24:mi') befoutdt                                                                        " + vbCrLf
                sSql += " ,  fn_ack_date_str(b3.outdt, 'yyyy-mm-dd hh24:mi') outdt                                                                              " + vbCrLf
                sSql += " ,  fn_ack_date_diff( fn_ack_date_str_gt(nvl(TO_CHAR(vw_ord.FSTRGSTDT, 'yyyymmddhh24miss'),'0'), fn_ack_date_str_bld_gt(b4.regno , nvl(b3.outdt, to_char(sysdate, 'yyyymmddhh24miss') ) )) , b3.befoutdt, '1')  b1             " + vbCrLf
                sSql += " ,  fn_ack_date_diff(b3.befoutdt, b3.outdt, '1') b2                                                                                                                                                                            " + vbCrLf
                sSql += " ,  fn_ack_date_diff( fn_ack_date_str_gt(nvl(TO_CHAR(vw_ord.FSTRGSTDT, 'yyyymmddhh24miss'),'0'), fn_ack_date_str_bld_gt(b4.regno , nvl(b3.outdt, to_char(sysdate, 'yyyymmddhh24miss') ) )) , b3.befoutdt, '1')  btat1          " + vbCrLf
                sSql += " ,  fn_ack_date_diff( fn_ack_date_str_gt(nvl(TO_CHAR(vw_ord.FSTRGSTDT, 'yyyymmddhh24miss'),'0'), fn_ack_date_str_bld_gt(b4.regno , nvl(b3.outdt, to_char(sysdate, 'yyyymmddhh24miss') ) )) , b3.outdt   , '1')  btat2          " + vbCrLf
                sSql += " ,  fn_ack_date_diff( fn_ack_date_str_gt(nvl(TO_CHAR(vw_ord.FSTRGSTDT, 'yyyymmddhh24miss'),'0'), fn_ack_date_str_bld_gt(b4.regno , nvl(b3.outdt, to_char(sysdate, 'yyyymmddhh24miss') ) )) , b3.befoutdt, '3')  btat1_mi       " + vbCrLf
                sSql += " ,  fn_ack_date_diff( fn_ack_date_str_gt(nvl(TO_CHAR(vw_ord.FSTRGSTDT, 'yyyymmddhh24miss'),'0'), fn_ack_date_str_bld_gt(b4.regno , nvl(b3.outdt, to_char(sysdate, 'yyyymmddhh24miss') ) )) , b3.outdt   , '3')  btat2_mi       " + vbCrLf

                'jjh 검사자, 가출고자 추가
                sSql += " ,  fn_ack_get_usr_name(B3.TESTID) TESTID                                                          " + vbCrLf
                sSql += " ,  fn_ack_get_usr_name(B3.BEFOUTID) BEFOUTID                                                      " + vbCrLf
                sSql += " ,  fn_ack_get_usr_name(B3.OUTID) OUTID                                                            " + vbCrLf
                sSql += " ,  RECNM "

                sSql += "  from lb040m b4                                                                                " + vbCrLf
                sSql += " inner join lb043m b43                                                                          " + vbCrLf
                sSql += "    on b4.tnsjubsuno = b43.tnsjubsuno                                                           " + vbCrLf
                sSql += " inner join lb030m b3                                                                           " + vbCrLf
                sSql += "    on b4.tnsjubsuno = B3.TNSJUBSUNO and b43.bldno = b3.bldno                                   " + vbCrLf
                sSql += " inner join (selecT o.patno, O.orddate, o.ordseqno, C.FSTRGSTDT                                 " + vbCrLf
                sSql += "               from VW_ACK_OCS_ORD_INFO o LEFT OUTER JOIN EMR.MNRMDEEX c                        " + vbCrLf
                sSql += "                                   ON c.prcpdd = o.orddate                                      " + vbCrLf
                sSql += "                                   And c.instcd = '031'                                         " + vbCrLf
                'sSql += "                                   /*AND c.prcphistno = o.prcphistno*/                          " + vbCrLf
                sSql += "                                   And c.execprcpuniqno = o.ordseqno                            " + vbCrLf
                sSql += "                                   AND c.prcpno = o.prcpno                                      " + vbCrLf
                sSql += "               WHERE o.instcd = '031'                                                           " + vbCrLf
                'sSql += "               --AND o.hopedate >= '20211107'                                                   " + vbCrLf
                'sSql += "               --And o.hopedate <= '20211108'                                                   " + vbCrLf
                sSql += "               AND o.prcpclscd = 'B4'                                                           " + vbCrLf
                sSql += "               And o.hscttempprcpflag = 'N'                                                     " + vbCrLf
                sSql += "               AND o.execprcphistcd = 'O'                                                       " + vbCrLf
                sSql += "               And nvl(o.discyn, 'N') = 'N') vw_ord                                             " + vbCrLf
                sSql += "    on b43.regno = vw_ord.patno                                                                 " + vbCrLf
                sSql += "   and substr(b43.fkocs, 12,8) = vw_ord.orddate                                                 " + vbCrLf
                sSql += "   and substr(b43.fkocs, 21,9) = vw_ord.ordseqno                                                " + vbCrLf
                sSql += " inner join lr070m r7                                                                           " + vbCrLf
                sSql += "    on r7.regno = b4.regno                                                                      " + vbCrLf
                'sSql += " inner join lr010m r1                                                                           " + vbCrLf
                'sSql += "    on b4.bcno_keep = r1.bcno                                                                   " + vbCrLf
                'sSql += "   And r1.testcd In ('LB141', 'LB142')                                                          " + vbCrLf

                If rsBranchComcd <> "" Then
                    sSql += " inner join lf000m f0                                                                       " + vbCrLf
                    sSql += "      on f0.clsgbn = 'B14' and f0.CLSVAL = :brcom and f0.CLSCD = b43.COMCD                 " + vbCrLf
                    alParm.Add(New OracleParameter("brcom", rsBranchComcd))
                End If

                sSql += "  left join lbc20m bc2                                                                          " + vbCrLf '이형수혈 체크 테이블 
                sSql += "   on B43.TNSJUBSUNO = bc2.tnsjubsuno and b43.bldno = bc2.bldno                                 " + vbCrLf
                sSql += " where b4.jubsudt >= :dates                                                                     " + vbCrLf
                sSql += "   And b4.jubsudt <= :datee || '235995'                                                         " + vbCrLf
                sSql += " order by b4.tnsjubsuno , b3.bldno                                                              " + vbCrLf


                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '---------------------------------------------------------

        Public Shared Function fnGet_trans_mgt_total(ByVal rsStDt As String, ByVal rsEnDt As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fnGet_trans_mgt_total(ByVal rsTnsNo As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = " "
                sSql += " select sum(hgyn) hgyn,                                               " + vbCrLf
                sSql += "        sum(cbcyn) cbcyn,                                             " + vbCrLf
                sSql += "        sum(allyn) allyn,                                             " + vbCrLf
                sSql += "        sum(ecptyn) ecptyn                                            " + vbCrLf
                sSql += "   from (selecT case when bc1.hgyn   = 'Y' then 1 else 0 end hgyn,    " + vbCrLf
                sSql += "                case when bc1.cbcyn  = 'Y' then 1 else 0 end cbcyn,   " + vbCrLf
                sSql += "                case when bc1.allyn  = 'Y' then 1 else 0 end allyn,   " + vbCrLf
                sSql += "                case when bc1.ecptyn = 'Y' then 1 else 0 end ecptyn   " + vbCrLf
                sSql += "          from lbc10m  bc1                                            " + vbCrLf
                sSql += "         inner Join lb040m b4                                         " + vbCrLf
                sSql += "            on bc1.tnsjubsuno = b4.tnsjubsuno                         " + vbCrLf
                sSql += "         where 1=1                                                    " + vbCrLf
                sSql += "           and b4.jubsudt  >= :datas                                  " + vbCrLf
                sSql += "           and b4.jubsudt  <= :datee || '235959')                     " + vbCrLf


                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_trans_mgt_total_new(ByVal rsStDt As String, ByVal rsEnDt As String, ByVal rsDMYDiff As String(), Optional ByVal rsRegno As String = "") As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fnGet_trans_mgt_total(ByVal rsTnsNo As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "select                                                 " + vbCrLf
                sSql += "       flag, sum(cnt) as total,                        " + vbCrLf

                For ix As Integer = 0 To rsDMYDiff.Length - 1
                    sSql += " sum(decode(regdate, '" + rsDMYDiff(ix).Replace("-", "").ToString + "', cnt, 0)) as C" + rsDMYDiff(ix).Replace("-", "").ToString() + ", " + vbCrLf
                Next

                sSql += "       seq                                                                 " + vbCrLf

                sSql += "  from ( select 'Hg>10 g/dL' as flag, 1 seq,                               " + vbCrLf
                sSql += "                substr(regdt, 0, 8) regdate,                               " + vbCrLf
                sSql += "                sum(case when hgyn = 'Y' then 1 else 0 end) cnt            " + vbCrLf
                sSql += "           from lbc10m                                                     " + vbCrLf
                sSql += "          where regdt between :dates and :datee || '235959'                " + vbCrLf
                sSql += "            and regno = nvl(:regno, regno)                                 " + vbCrLf
                sSql += "          group by substr(regdt, 0, 8)                                     " + vbCrLf

                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))
                alParm.Add(New OracleParameter("regno", rsRegno))

                sSql += "         union all                                                         " + vbCrLf

                sSql += "         select 'CBC F/U' as flag, 2 seq,                                  " + vbCrLf
                sSql += "                substr(regdt, 0, 8) regdate,                               " + vbCrLf
                sSql += "                sum(case when cbcyn = 'Y' then 1 else 0 end) cnt           " + vbCrLf
                sSql += "           from lbc10m                                                     " + vbCrLf
                sSql += "          where regdt between :dates and :datee || '235959'                " + vbCrLf
                sSql += "            and regno = nvl(:regno, regno)                                 " + vbCrLf
                sSql += "          group by substr(regdt, 0, 8)                                     " + vbCrLf

                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))
                alParm.Add(New OracleParameter("regno", rsRegno))

                sSql += "         union all                                                         " + vbCrLf

                sSql += "         select '모두요청' as flag, 3 seq,                                 " + vbCrLf
                sSql += "                substr(regdt, 0, 8) regdate,                               " + vbCrLf
                sSql += "                sum(case when allyn = 'Y' then 1 else 0 end) cnt           " + vbCrLf
                sSql += "           from lbc10m                                                     " + vbCrLf
                sSql += "          where regdt between :dates and :datee || '235959'                " + vbCrLf
                sSql += "            and regno = nvl(:regno, regno)                                 " + vbCrLf
                sSql += "          group by substr(regdt, 0, 8)                                     " + vbCrLf

                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))
                alParm.Add(New OracleParameter("regno", rsRegno))

                sSql += "         union all                                                         " + vbCrLf

                sSql += "         select '제외 대상' as flag, 4 seq,                                " + vbCrLf
                sSql += "                substr(regdt, 0, 8) regdate,                               " + vbCrLf
                sSql += "                sum(case when ecptyn = 'Y' then 1 else 0 end) cnt          " + vbCrLf
                sSql += "           from lbc10m                                                     " + vbCrLf
                sSql += "          where regdt between :dates and :datee || '235959'                " + vbCrLf
                sSql += "            and regno = nvl(:regno, regno)                                 " + vbCrLf
                sSql += "          group by substr(regdt, 0, 8)                                     " + vbCrLf

                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))
                alParm.Add(New OracleParameter("regno", rsRegno))

                sSql += "        )                                                                  " + vbCrLf
                sSql += " group by flag, seq                                                        " + vbCrLf
                sSql += " order by seq                                                              "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '--------------------------------------------

        '20211203 jhs 혈액tat 입력 
        ' 혈액tat 입력 데이터 가져오기
        Public Shared Function fnGet_BloodTat_Input(ByVal rsStDt As String, ByVal rsEnDt As String, ByVal rsRegno As String, ByVal rsTnsjubsuno As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fnGet_trans_mg(ByVal rsTnsNo As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql = " "
                sSql += "selecT b4.tnsjubsuno, fn_ack_date_str(b4.jubsudt, 'yyyy-MM-dd hh24:mi:ss') jubsudt  " + vbCrLf
                sSql += "     , b4.regno ,b4.patnm , b4.sex || '/' || b4.age sexage                          " + vbCrLf
                sSql += "     , fn_ack_get_dr_name(b4.doctorcd) drnm                                         " + vbCrLf
                sSql += "     , FN_ACK_GET_ROOM_NAME(b4.wardno,  b4.roomno) roomno                           " + vbCrLf
                sSql += "     , fn_ack_get_dept_name(b4.iogbn, b4.deptcd)  deptnm                            " + vbCrLf
                sSql += "     ,  CASE WHEN b4.tnsgbn = '1' THEN 'P'                                          " + vbCrLf
                sSql += "             WHEN b4.tnsgbn = '2' THEN 'T'                                          " + vbCrLf
                sSql += "             WHEN b4.tnsgbn = '3' THEN 'E'                                          " + vbCrLf
                sSql += "             WHEN b4.tnsgbn = '4' THEN 'I' end   tnsgbn                             " + vbCrLf
                sSql += "      , b42.comcd , f12.comnmd                                                      " + vbCrLf
                sSql += "      , nvl(bc2.gwa, FN_ACK_GET_ROOM_NAME(b4.wardno, b4.roomno)) seletedRoomno      " + vbCrLf
                sSql += "      ,  case when nvl(bc2.varyn,'') = 'Y'  then '이형수혈' else '' end vartnsgbn   " + vbCrLf '추후 이형수혈 
                sSql += "      , b42.befoutqnt , b42.reqqnt , b42.outqnt , b42.rtnqnt                        " + vbCrLf
                sSql += "      , b42.abnqnt , r7.abo  ||  r7.rh aborh                                        " + vbCrLf
                sSql += "      , fn_ack_get_bldno_full(b3.bldno) bldno                                       " + vbCrLf
                sSql += "   from lb040m b4                                                                   " + vbCrLf
                sSql += "  inner join lb042m b42                                                             " + vbCrLf
                sSql += "     on b4.tnsjubsuno = b42.tnsjubsuno                                              " + vbCrLf
                sSql += "  inner Join lb030m b3                                                              " + vbCrLf
                sSql += "     on b4.tnsjubsuno = b3.tnsjubsuno                                               " + vbCrLf
                sSql += "  inner join lf120m f12                                                             " + vbCrLf
                sSql += "     on b42.comcd = f12.comcd                                                       " + vbCrLf
                sSql += "  inner join lr070m r7                                                              " + vbCrLf
                sSql += "     on b4.regno = r7.regno                                                         " + vbCrLf
                sSql += "   left outer join lbc20m bc2                                                       " + vbCrLf
                sSql += "     on b4.tnsjubsuno = bc2.tnsjubsuno  and b3.bldno = bc2.bldno                    " + vbCrLf
                sSql += "  where b4.jubsudt BETWEEN :datas AND :datee || '235959'                            " + vbCrLf

                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))

                If rsRegno <> "" Then
                    sSql += "       AND b4.regno      = :regno                                               " + vbCrLf
                    alParm.Add(New OracleParameter("regno", rsRegno))
                End If

                If rsTnsjubsuno <> "" Then
                    sSql += "       AND b4.tnsjubsuno = :tnsjubsuno                                          " + vbCrLf
                    alParm.Add(New OracleParameter("tnsjubsuno", rsTnsjubsuno))
                End If

                sSql += " ORDER BY tnsjubsuno                                                                " + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '--------------------------------------------

        '20211203 jhs 혈액tat 입력 
        ' 혈액tat 입력 병동/병실 과 가져오기
        Public Shared Function fnGet_BloodTat_Input_Gwa(Optional ByVal rsGwa As String = "") As DataTable
            Dim sFn As String = "Public Function fnGet_CollTK_Cancel_ContInfo(String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT clsgbn, clsval"
                sSql += "  FROM lf000m"
                sSql += " WHERE clsgbn ='BDT'"
                If rsGwa <> "" Then
                    sSql += " and clsval  = :gwa"
                    alParm.Add(New OracleParameter("gwa", rsGwa))
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '20211203 jhs 혈액tat 입력 
        ' 혈액tat 입력 병동/병실 과 가져오기
        Public Shared Function fnGet_BloodTat_Input_SuHyul(Optional ByVal rsGwa As String = "") As DataTable
            Dim sFn As String = "Public Function fnGet_CollTK_Cancel_ContInfo(String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT clsgbn, clsval"
                sSql += "  FROM lf000m"
                sSql += " WHERE clsgbn ='SH'"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        Public Shared Function fnGet_BloodTat_Input_tns(ByVal rsTnsjubsuno As String, ByVal rsRegno As String, ByVal rsBldno As String) As DataTable
            Dim sFn As String = "Public Function fnGet_CollTK_Cancel_ContInfo(String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += " "
                sSql += " Select tnsjubsuno from lbc20m    " + vbCrLf
                sSql += "  where tnsjubsuno = :tnsjubsuno  " + vbCrLf
                sSql += "    And regno      = :regno       " + vbCrLf
                sSql += "    And bldno      = :bldno       " + vbCrLf

                alParm.Add(New OracleParameter("tnsjubsuno", OracleDbType.Varchar2, rsTnsjubsuno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsjubsuno))
                alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '--------------------------------------------

        '20211203 jhs 혈액tat 조회
        ' 혈액tat 입력 데이터 가져오기
        Public Shared Function fnGet_BloodTat(ByVal rsStDt As String, ByVal rsEnDt As String, ByVal rsRegno As String, ByVal rsTnsjubsuno As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fnGet_trans_mg(ByVal rsTnsNo As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "selecT b4.tnsjubsuno, fn_ack_date_str(b4.jubsudt, 'yyyy-MM-dd hh24:mi:ss') jubsudt, b4.regno ,b4.patnm , b4.sex || '/' || b4.age sexage " + vbCrLf
                sSql += "     , fn_ack_get_dr_name(b4.doctorcd) drnm " + vbCrLf
                'sSql += "     , FN_ACK_GET_ROOM_NAME(b4.wardno,  b4.roomno) roomno " + vbCrLf
                sSql += "     , fn_ack_get_dept_name(b4.iogbn, b4.deptcd)  deptnm " + vbCrLf
                sSql += "     ,  CASE WHEN b4.tnsgbn = '1' THEN 'P' " + vbCrLf
                sSql += "             WHEN b4.tnsgbn = '2' THEN 'T' " + vbCrLf
                sSql += "             WHEN b4.tnsgbn = '3' THEN 'E' " + vbCrLf
                sSql += "             WHEN b4.tnsgbn = '4' THEN 'I' end   tnsgbn " + vbCrLf
                sSql += "      , b42.comcd , f12.comnmd " + vbCrLf
                sSql += "      , nvl(bc2.gwa, FN_ACK_GET_ROOM_NAME(b4.wardno, b4.roomno)) roomno " + vbCrLf
                sSql += "      ,  case when nvl(bc2.varyn,'') = 'Y'  then '이형수혈' else '' end vartnsgbn" + vbCrLf '추후 이형수혈 
                sSql += "      , b42.befoutqnt , b42.reqqnt , b42.outqnt , b42.rtnqnt , b42.abnqnt , r7.abo  ||  r7.rh aborh " + vbCrLf
                sSql += "   from lb040m b4 " + vbCrLf
                sSql += "  inner join lb042m b42 " + vbCrLf
                sSql += "     on b4.tnsjubsuno = b42.tnsjubsuno " + vbCrLf
                sSql += "  inner join lf120m f12 " + vbCrLf
                sSql += "     on b42.comcd = f12.comcd  " + vbCrLf
                sSql += "  inner join lr070m r7  " + vbCrLf
                sSql += "     on b4.regno = r7.regno  " + vbCrLf
                sSql += "   left outer join lbc20m bc2  " + vbCrLf
                sSql += "     on b4.tnsjubsuno = bc2.tnsjubsuno " + vbCrLf
                sSql += "  where b4.jubsudt BETWEEN :datas AND :datee || '235959'  " + vbCrLf

                alParm.Add(New OracleParameter("dates", rsStDt))
                alParm.Add(New OracleParameter("datee", rsEnDt))

                If rsRegno <> "" Then
                    sSql += "       AND b4.regno      = :regno" + vbCrLf
                    alParm.Add(New OracleParameter("regno", rsRegno))
                End If

                If rsTnsjubsuno <> "" Then
                    sSql += "       AND b4.tnsjubsuno = :tnsjubsuno" + vbCrLf
                    alParm.Add(New OracleParameter("tnsjubsuno", rsTnsjubsuno))
                End If

                sSql += " ORDER BY tnsjubsuno        " + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        '--------------------------------------------


        Public Shared Function fn_PreOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsGbn As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fn_PreOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT CASE WHEN a.state = '1' THEN '완' ELSE '미' END state," + vbCrLf
                sSql += "       CASE WHEN a.spcstate > 0 THEN 'Y' ELSE 'N' END spcstate,                                                          " + vbCrLf
                sSql += "       a.tnsjubsuno," + vbCrLf
                sSql += "       fn_ack_get_tnsjubsuno_full(a.tnsjubsuno) vtnsjubsuno," + vbCrLf
                sSql += "       a.comcd," + vbCrLf
                sSql += "       a.comnm," + vbCrLf
                sSql += "       a.regno," + vbCrLf
                sSql += "       a.patnm," + vbCrLf
                sSql += "       a.sexage," + vbCrLf
                sSql += "       fn_ack_get_bcno_full(a.bcno_order) bcno_order," + vbCrLf
                sSql += "       a.bcno_keep," + vbCrLf
                sSql += "       a.reqqnt," + vbCrLf
                sSql += "       a.outqnt," + vbCrLf
                sSql += "       a.orddt order_date," + vbCrLf
                sSql += "       r.abo," + vbCrLf
                sSql += "       r.rh," + vbCrLf
                sSql += "       a.spccd," + vbCrLf
                sSql += "       a.ir," + vbCrLf
                sSql += "       a.filter," + vbCrLf
                sSql += "       a.iogbn," + vbCrLf
                sSql += "       a.owngbn," + vbCrLf
                sSql += "       a.eryn," + vbCrLf
                sSql += "       CASE WHEN a.tnsgbn = '1' THEN 'P' WHEN a.tnsgbn = '2' THEN 'T'" + vbCrLf
                sSql += "            WHEN a.tnsgbn = '3' THEN 'E' WHEN a.tnsgbn = '4' THEN 'I'" + vbCrLf
                sSql += "       END tnsgbn," + vbCrLf
                sSql += "       a.state" + vbCrLf
                sSql += "  FROM (SELECT DISTINCT " + vbCrLf
                sSql += "               a.tnsjubsuno," + vbCrLf
                sSql += "               b.comcd," + vbCrLf
                sSql += "               b.comnm," + vbCrLf
                sSql += "               a.regno," + vbCrLf
                sSql += "               a.patnm," + vbCrLf
                sSql += "               a.sex || '/' || a.age sexage," + vbCrLf
                sSql += "               a.bcno_order," + vbCrLf
                sSql += "               a.bcno_keep," + vbCrLf
                sSql += "               NVL(LENGTH(a.bcno_order), 0) + NVL(LENGTH(a.bcno_keep), 0) spcstate," + vbCrLf
                sSql += "               NVL(b.reqqnt, 0) reqqnt," + vbCrLf
                sSql += "               NVL(b.befoutqnt, 0) + NVL(b.outqnt, 0) + NVL(b.rtnqnt, 0) + " + vbCrLf
                sSql += "               NVL(b.abnqnt, 0) + NVL(b.cancelqnt, 0) outqnt," + vbCrLf
                sSql += "               a.orddt," + vbCrLf
                sSql += "               b.spccd," + vbCrLf
                sSql += "               b.ir," + vbCrLf
                sSql += "               b.filter," + vbCrLf
                sSql += "               a.iogbn," + vbCrLf
                sSql += "               a.owngbn," + vbCrLf
                sSql += "               a.eryn," + vbCrLf
                sSql += "               a.tnsgbn," + vbCrLf
                sSql += "               b.state," + vbCrLf
                sSql += "               a.jubsudt" + vbCrLf
                sSql += "          FROM lb040m a, lb042m b" + vbCrLf
                sSql += "         WHERE a.jubsudt BETWEEN :dates AND :datee || '235959'" + vbCrLf
                sSql += "           AND NVL(a.delflg, '0') <> '1'" + vbCrLf

                alParm.Add(New OracleParameter("dates", rsFdate))
                alParm.Add(New OracleParameter("datee", rsTdate))

                If rsRegno <> "" Then
                    sSql += "       AND a.regno      = :regno" + vbCrLf
                    alParm.Add(New OracleParameter("regno", rsRegno))
                End If

                If rsComcd <> "" And rsComcd <> "ALL" Then
                    sSql += "       AND b.comcd = :comcd" + vbCrLf
                    alParm.Add(New OracleParameter("comcd", rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno     = b.tnsjubsuno" + vbCrLf

                'sSql += "           AND NVL(b.filter, 0) = '0'"
                sSql += "       ) a, lr070m r" + vbCrLf
                sSql += " WHERE a.regno = r.regno (+)" + vbCrLf
                If rsGbn = "0"c Then

                ElseIf rsGbn = "1"c Then
                    sSql += "   AND state = '0'" + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "   AND state = '1'" + vbCrLf
                End If

                sSql += " ORDER BY tnsjubsuno                                                             " + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_GetTnsCnt(ByVal rsTnsnum As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetTnsCnt(ByVal rsTnsnum As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT comnm, " + vbCrLf
                sSql += "       CASE filter WHEN '1' THEN '○' ELSE '' END filter," + vbCrLf
                sSql += "       CASE ir  WHEN '1' THEN '○' ELSE '' END ir," + vbCrLf
                sSql += "       NVL(reqqnt, 0)    reqqnt," + vbCrLf
                sSql += "       NVL(befoutqnt, 0) befoutqnt," + vbCrLf
                sSql += "       NVL(outqnt, 0)    outqnt," + vbCrLf
                sSql += "       NVL(rtnqnt, 0)    rtnqnt," + vbCrLf
                sSql += "       NVL(abnqnt, 0)    abnqnt," + vbCrLf
                sSql += "       NVL(cancelqnt, 0) cancelqnt" + vbCrLf
                sSql += "  FROM lb042m" + vbCrLf
                sSql += " WHERE tnsjubsuno = :tnsno" + vbCrLf

                alParm.Add(New OracleParameter("tnsno", OracleDbType.Varchar2, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsnum))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 혈액은행 보유혈액 조회
        'Public Shared Function fn_GetStoreBldList(ByVal rsAbo As String, ByVal rsRh As String, _
        '                                          ByVal rsComcd As String, ByVal rsSpccd As String, _
        '                                          Optional ByVal rsEqual As String = "", Optional ByVal rsChg As String = "", _
        '                                          Optional ByVal rsTnsJubsuNo As String = "") As DataTable
        '    Dim sFn As String = "Public Shared Function fn_GetStoreBldList(String, String, String, String, [String], [String], [String]) As DataTable"

        '    Try
        '        Dim sSql As String = ""
        '        Dim alParm As New ArrayList

        '        sSql += "SELECT a.bldno,"
        '        sSql += "       fn_ack_get_bldno_full(a.bldno)  vbldno,"
        '        sSql += "       a.comcd,"
        '        sSql += "       b.comnmd,"
        '        sSql += "       b.comordcd,"
        '        sSql += "       a.abo || a.rh aborh,"
        '        sSql += "       a.abo,"
        '        sSql += "       a.rh,"
        '        sSql += "       fn_ack_date_str(a.dondt, 'yyyy-mm-dd hh24:mi')   dondt,"
        '        sSql += "       fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi')    indt,"
        '        sSql += "       fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') availdt,"
        '        sSql += "       999 sortkey,"
        '        sSql += "       b.crosslevel crosslevel,"
        '        sSql += "       a.cmt"
        '        sSql += "  FROM lb020m a, lf120m b"
        '        sSql += " WHERE a.availdt >  fn_ack_sysdate"
        '        sSql += "   AND a.state   =  '0'"
        '        sSql += "   AND a.comcd   =  NVL(b.pscomcd, b.comcd)"
        '        sSql += "   AND b.comcd   =  :comcd"
        '        sSql += "   AND b.spccd   =  :spccd"

        '        alParm.Add(New OracleParameter("comcd",  OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
        '        alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

        '        sSql += "   AND a.indt    >= b.usdt"
        '        sSql += "   AND a.indt    <  b.uedt"

        '        If rsChg = "" Then
        '            sSql += "   AND a.abo     =  :abo"
        '            sSql += "   AND a.rh      =  :rh"

        '            alParm.Add(New OracleParameter("abo",  OracleDbType.Varchar2, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
        '            alParm.Add(New OracleParameter("rh",  OracleDbType.Varchar2, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))
        '        End If

        '        If rsTnsJubsuNo <> "" Then
        '            sSql += "   AND a.bldno NOT IN (SELECT bldno FROM lb043m WHERE tnsjubsuno = :tnsno)"
        '            alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rsTnsJubsuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsJubsuNo))
        '        End If

        '        If rsEqual <> "" Then
        '            sSql += " UNION ALL "
        '            sSql += "SELECT a.bldno,"
        '            sSql += "       fn_ack_get_bldno_full(a.bldno) vbldno,"
        '            sSql += "       a.comcd,"
        '            sSql += "       b.comnmd,"
        '            sSql += "       a.abo || a.rh aborh,"
        '            sSql += "       a.abo,"
        '            sSql += "       a.rh,"
        '            sSql += "       fn_ack_date_str(a.dondt, 'yyyy-mm-dd hh24:mi')   dondt,"
        '            sSql += "       fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi')    indt,"
        '            sSql += "       fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') availdt,"
        '            sSql += "       9 sortkey,"
        '            sSql += "       b.crosslevel crosslevel,"
        '            sSql += "       b.cmt"
        '            sSql += "  FROM lb020m a, lf120m b"
        '            sSql += " WHERE a.availdt > fn_ack_sysdate "

        '            If rsChg = "" Then
        '                sSql += "   AND a.abo     = :abo"
        '                sSql += "   AND a.rh      = :rh"

        '                alParm.Add(New OracleParameter("abo",  OracleDbType.Varchar2, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
        '                alParm.Add(New OracleParameter("rh",  OracleDbType.Varchar2, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))
        '            End If


        '            sSql += "   AND a.state   = '0'"
        '            sSql += "   AND a.comcd   = NVL(b.gordcd, b.comcd)"
        '            sSql += "   AND b.comcd   = :comcd"
        '            sSql += "   AND b.spccd   = :spccd"
        '            sSql += "   AND a.indt   >= b.usdt"
        '            sSql += "   AND a.indt   <  b.uedt"

        '            alParm.Add(New OracleParameter("comcd",  OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd.Trim))
        '            alParm.Add(New OracleParameter("spccd",  OracleDbType.Varchar2, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd.Trim))

        '            If rsTnsJubsuNo <> "" Then
        '                sSql += "   AND a.bldno NOT IN (SELECT bldno FROM lb043m WHERE tnsjubsuno = :tnsno)"
        '                alParm.Add(New OracleParameter("tnsno",  OracleDbType.Varchar2, rsTnsJubsuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsJubsuNo))
        '            End If

        '        End If

        '        sSql += " ORDER BY availdt, bldno, comcd"

        '        DbCommand()
        '        Return DbExecuteQuery(sSql, alParm)

        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        '    End Try

        'End Function



        Public Shared Function fn_GetStoreBldList(ByVal rsTnsJubsuNo As String, ByVal rsAbo As String, ByVal rsRh As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetStoreBldList(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT " + vbCrLf
                sSql += "       B2.BLDNO," + vbCrLf
                sSql += "       FN_ACK_GET_BLDNO_FULL(B2.BLDNO)  AS VBLDNO," + vbCrLf
                sSql += "       B2.COMCD                         AS COMCD_OUT," + vbCrLf
                sSql += "       (SELECT COMNMD " + vbCrLf
                sSql += "          FROM LF120M" + vbCrLf
                sSql += "         WHERE COMCD  = B2.COMCD" + vbCrLf
                sSql += "           AND USDT  <= B2.INDT" + vbCrLf
                sSql += "           AND UEDT  >  B2.INDT" + vbCrLf
                sSql += "       )                                AS COMNMD," + vbCrLf
                sSql += "       F4.COMORDCD," + vbCrLf
                sSql += "       B2.ABO || B2.RH                  AS ABORH," + vbCrLf
                sSql += "       B2.ABO," + vbCrLf
                sSql += "       B2.RH," + vbCrLf
                sSql += "       FN_ACK_DATE_STR(B2.DONDT,   'YYYY-MM-DD HH24:MI')   AS DONDT," + vbCrLf
                sSql += "       FN_ACK_DATE_STR(B2.INDT,    'YYYY-MM-DD HH24:MI')   AS INDT," + vbCrLf
                sSql += "       FN_ACK_DATE_STR(B2.AVAILDT, 'YYYY-MM-DD HH24:MI')   AS AVAILDT," + vbCrLf
                sSql += "       999                                                 AS SORTKEY," + vbCrLf
                sSql += "       F4.CROSSLEVEL                                       AS CROSSLEVEL," + vbCrLf
                sSql += "       B2.CMT," + vbCrLf
                sSql += "       B4.COMCD                        AS COMCD," + vbCrLf
                sSql += "       B4.COMNM                        AS COMNM" + vbCrLf
                sSql += "  FROM LB043M B4, LB020M B2, LF120M F4" + vbCrLf
                sSql += " WHERE B4.tnsjubsuno  = :tnsno" + vbCrLf
                sSql += "   AND B4.COMCD       = F4.COMCD" + vbCrLf
                sSql += "   AND B4.SPCCD       = F4.SPCCD" + vbCrLf
                sSql += "   AND B2.AVAILDT    >  fn_ack_sysdate" + vbCrLf
                sSql += "   AND B2.STATE        = '0'" + vbCrLf
                sSql += "   AND B2.COMCD       = NVL(F4.PSCOMCD, F4.COMCD)" + vbCrLf
                sSql += "   AND B2.indt       >= F4.USDT" + vbCrLf
                sSql += "   AND B2.indt       <  F4.UEDT" + vbCrLf
                sSql += "   AND B4.STATE       = '1'" + vbCrLf

                alParm.Add(New OracleParameter("tnsno", rsTnsJubsuNo))

                If rsAbo.Length + rsRh.Length > 0 Then
                    sSql += "   AND B2.abo     =  :abo" + vbCrLf
                    sSql += "   AND B2.rh      =  :rh" + vbCrLf

                    alParm.Add(New OracleParameter("abo", rsAbo))
                    alParm.Add(New OracleParameter("rh", rsRh))
                End If

                sSql += " ORDER BY availdt, bldno, comcd" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function
        Public Shared Function fn_GetStoreBldList2(ByVal rsTnsJubsuNo As String, ByVal rsAbo As String, ByVal rsRh As String, ByVal rsErChk As Boolean) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetStoreBldList(String, String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT DISTINCT " + vbCrLf
                sSql += "       B2.BLDNO," + vbCrLf
                sSql += "       FN_ACK_GET_BLDNO_FULL(B2.BLDNO)  AS VBLDNO," + vbCrLf
                sSql += "       B2.COMCD                         AS COMCD_OUT," + vbCrLf
                sSql += "       (SELECT COMNMD " + vbCrLf
                sSql += "          FROM LF120M" + vbCrLf
                sSql += "         WHERE COMCD  = B2.COMCD" + vbCrLf
                sSql += "           AND USDT  <= B2.INDT" + vbCrLf
                sSql += "           AND UEDT  >  B2.INDT" + vbCrLf
                sSql += "       )                                AS COMNMD," + vbCrLf
                sSql += "       F4.COMORDCD," + vbCrLf
                sSql += "       B2.ABO || B2.RH                  AS ABORH," + vbCrLf
                sSql += "       B2.ABO," + vbCrLf
                sSql += "       B2.RH," + vbCrLf
                sSql += "       FN_ACK_DATE_STR(B2.DONDT,   'YYYY-MM-DD HH24:MI')   AS DONDT," + vbCrLf
                sSql += "       FN_ACK_DATE_STR(B2.INDT,    'YYYY-MM-DD HH24:MI')   AS INDT," + vbCrLf
                sSql += "       FN_ACK_DATE_STR(B2.AVAILDT, 'YYYY-MM-DD HH24:MI')   AS AVAILDT," + vbCrLf
                sSql += "       999                                                 AS SORTKEY," + vbCrLf
                sSql += "       F4.CROSSLEVEL                                       AS CROSSLEVEL," + vbCrLf
                sSql += "       B2.CMT," + vbCrLf
                sSql += "       B4.COMCD                        AS COMCD," + vbCrLf
                sSql += "       B4.COMNM                        AS COMNM," + vbCrLf
                sSql += "       (selecT tnsgbn from lb040m where tnsjubsuno = B4.TNSJUBSUNO) tnsgbn " + vbCrLf
                sSql += "  FROM LB043M B4, LB020M B2, LF120M F4" + vbCrLf
                sSql += " WHERE B4.tnsjubsuno  = :tnsno" + vbCrLf

                If rsErChk = False Then

                    sSql += "   AND B4.COMCD       = F4.COMCD" + vbCrLf

                Else
                    sSql += "   AND B4.COMCD       = F4.COMCD" + vbCrLf
                    sSql += "        AND B4.COMCD IN(" + vbCrLf
                    'sSql += "   "+vbCrLf 
                    sSql += "   'LB507' , 'LB506' " + vbCrLf  '<<<20180511 교차미필시 RBC320 추가
                    sSql += "   )" + vbCrLf

                End If

                sSql += "   AND B4.SPCCD       = F4.SPCCD" + vbCrLf
                sSql += "   AND B2.AVAILDT    >  fn_ack_sysdate" + vbCrLf
                sSql += "   AND B2.STATE        = '0'" + vbCrLf
                sSql += "   AND B2.COMCD       = NVL(F4.PSCOMCD, F4.COMCD)" + vbCrLf
                sSql += "   AND B2.indt       >= F4.USDT" + vbCrLf
                sSql += "   AND B2.indt       <  F4.UEDT" + vbCrLf
                sSql += "   AND B4.STATE       = '1'" + vbCrLf

                alParm.Add(New OracleParameter("tnsno", rsTnsJubsuNo.Replace("-", "")))


                If rsErChk = False Then

                    If rsAbo.Length + rsRh.Length > 0 Then
                        sSql += "   AND B2.abo     =  :abo" + vbCrLf
                        sSql += "   AND B2.rh      =  :rh" + vbCrLf

                        alParm.Add(New OracleParameter("abo", rsAbo))
                        alParm.Add(New OracleParameter("rh", rsRh))

                    End If
                Else

                    If rsAbo.Length + rsRh.Length > 0 Then

                        sSql += "   AND (  B2.abo     =  :abo" + vbCrLf
                        sSql += "   AND B2.rh      =  :rh OR B2.abo = 'O' ) " + vbCrLf

                        alParm.Add(New OracleParameter("abo", rsAbo))
                        alParm.Add(New OracleParameter("rh", rsRh))
                    Else

                        sSql += "   AND B2.abo     =  'O'" + vbCrLf
                        sSql += "   AND B2.rh      IN('+','-')" + vbCrLf

                    End If

                End If



                sSql += " ORDER BY availdt, bldno, comcd" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function


        ' 가출고 목록 조회
        Public Shared Function fn_GetPreList(ByVal rsTnsnum As String, ByVal rsSpccd As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetPreList(ByVal rsTnsnum As String, ByVal rsSpccd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "select a.tnsjubsuno                                                   "
                sSql += "     , CASE a.state WHEN '1' THEN '접'                                "
                sSql += "                    WHEN '2' THEN '결'                                "
                sSql += "                    WHEN '3' THEN '가'                                "
                sSql += "       END                                           as vstate        "
                sSql += "     , fn_ack_get_bldno_full(a.bldno)                as vbldno    "
                sSql += "     , d.comnmd                                      as comnmd        "
                sSql += "     , c.abo || c.rh                                 as type          "
                sSql += "     , b.rst1                                                         "
                sSql += "     , b.rst2                                                         "
                sSql += "     , b.rst3                                                         "
                sSql += "     , b.rst4                                                         "
                sSql += "     , b.cmrmk                                                        "
                sSql += "     , fn_ack_date_str(b.befoutdt, 'yyyy-MM-dd hh24:mi') as befoutdt  "
                sSql += "     , fn_ack_get_usr_name(b.testid)                     as inspector "
                sSql += "     , fn_ack_date_str(c.indt, 'yyyy-MM-dd hh24:mi')     as indt      "
                sSql += "     , fn_ack_date_str(c.dondt, 'yyyy-MM-dd hh24:mi')    as dondt     "
                sSql += "     , fn_ack_date_str(c.availdt, 'yyyy-MM-dd hh24:mi')  as availdt   "
                sSql += "     , a.comcd_out                                       as comcd_out     "
                sSql += "     , a.owngbn                                                       "
                sSql += "     , a.iogbn                                                        "
                sSql += "     , a.fkocs || '-' || TO_CHAR(a.seq)                  as fkocs     "
                sSql += "     , a.bldno                                                        "
                sSql += "     , d.comnmp                                                       "
                sSql += "     , CASE a.comcd_out WHEN a.comcd THEN '0'                         "
                sSql += "                        ELSE '1'                                      "
                sSql += "       END                                           as comcdchk  "
                sSql += "     , a.state                                                       "
                sSql += "     , a.comcd                                       as comcd        "
                sSql += "     , a.comnm                                       as comnm        "
                sSql += "     , d.comordcd"
                sSql += "     , d.crosslevel                                                   "
                sSql += "     , c.cmt "
                sSql += "  from lb043m a, lb030m b, lb020m c, lf120m d"
                sSql += " where a.tnsjubsuno = :tnsno                                               "

                alParm.Add(New OracleParameter("tnsno", OracleDbType.Varchar2, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsnum))

                sSql += "   and a.state      in ('1', '2', '3')                                "
                sSql += "   and a.bldno      = b.bldno                                         "
                sSql += "   and a.comcd_out  = b.comcd_out                                     "
                sSql += "   and a.tnsjubsuno = b.tnsjubsuno                                    "
                sSql += "   and a.bldno      = c.bldno                                         "
                sSql += "   and a.comcd_out  = c.comcd                                         "
                sSql += "   and a.comcd_out  = d.comcd                                         "
                sSql += "   and d.spccd      = :spccd                                               "

                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        ' 출고 목록 조회
        Public Shared Function fn_GetOutList(ByVal rsTnsnum As String, ByVal rsSpccd As String, ByVal rsFilter As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetPreList(ByVal rsTnsnum As String, ByVal rsSpccd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "select DISTINCT a.tnsjubsuno                                                   " + vbCrLf
                sSql += "     , CASE a.state WHEN '4' THEN '출'                                " + vbCrLf
                sSql += "                    WHEN '5' THEN '반'                                " + vbCrLf
                sSql += "                    WHEN '6' THEN '폐'                                " + vbCrLf
                sSql += "       END                                              as vstate     " + vbCrLf
                sSql += "     , d.comnmd              as comnmd                                " + vbCrLf
                sSql += "     , a.comcd               as comcd                                 " + vbCrLf
                sSql += "     , a.comnm               as comnm                                 " + vbCrLf
                sSql += "     , a.comcd_out           as comcd_out                             " + vbCrLf
                sSql += "     , d.comordcd            " + vbCrLf
                sSql += "     , a.owngbn            " + vbCrLf

                If rsFilter <> "1"c Then
                    sSql += "     , fn_ack_get_bldno_full(a.bldno)                       as vbldno     " + vbCrLf
                    sSql += "     , c.abo || c.rh                                    as type                                   " + vbCrLf
                    sSql += "     , b.rst1                                                         " + vbCrLf
                    sSql += "     , b.rst2                                                         " + vbCrLf
                    sSql += "     , b.rst3                                                         " + vbCrLf
                    sSql += "     , b.rst4                                                         " + vbCrLf
                    sSql += "     , b.cmrmk                                                        " + vbCrLf
                    sSql += "     , fn_ack_date_str(b.befoutdt, 'yyyy-MM-dd hh24:mi')    as befoutdt       " + vbCrLf
                    sSql += "     , fn_ack_date_str(b.outdt, 'yyyy-MM-dd hh24:mi')       as outdt          " + vbCrLf
                    sSql += "     , fn_ack_get_usr_name(b.testid)                        as inspector      " + vbCrLf
                    sSql += "     , fn_ack_date_str(c.indt, 'yyyy-MM-dd hh24:mi')        as indt           " + vbCrLf
                    sSql += "     , fn_ack_date_str(c.dondt, 'yyyy-MM-dd hh24:mi')       as dondt          " + vbCrLf
                    sSql += "     , fn_ack_date_str(c.availdt, 'yyyy-MM-dd hh24:mi')     as availdt        " + vbCrLf
                    sSql += "     , fn_ack_get_usr_name(b.outid)                         as outid          " + vbCrLf
                    sSql += "     , b.recnm                                                                " + vbCrLf
                    sSql += "     , CASE b.keepgbn WHEN '0' THEN '출'                                      " + vbCrLf
                    sSql += "                      WHEN '1' THEN '보'                                      " + vbCrLf
                    sSql += "                      WHEN '2' THEN '재'                                      " + vbCrLf
                    sSql += "                      WHEN '3' THEN '반'                                      " + vbCrLf
                    sSql += "                      WHEN '4' THEN '폐'                                      " + vbCrLf
                    sSql += "       END                                              as vkeepgbn           " + vbCrLf
                    sSql += "     , b.keepgbn                                                              " + vbCrLf
                    sSql += "     , CASE b.keepgbn WHEN '1' THEN 0                                       " + vbCrLf
                    sSql += "                      ELSE TRUNC(SYSDATE - TO_DATE(b.outdt, 'yyyymmddhh24miss')) * 24 * 60" + vbCrLf
                    sSql += "       END                                              as elapsdm            " + vbCrLf

                Else
                    sSql += "     , 'FILTER' as vbldno                                " + vbCrLf
                    sSql += "     , ''           as type                                  " + vbCrLf
                    sSql += "     , '' as rst1                                                         " + vbCrLf
                    sSql += "     , '' as rst2                                                         " + vbCrLf
                    sSql += "     , '' as rst3                                                         " + vbCrLf
                    sSql += "     , '' as rst4                                                         " + vbCrLf
                    sSql += "     , '' as cmrmk                                                        " + vbCrLf
                    sSql += "     , ''    as befoutdt       " + vbCrLf
                    sSql += "     , ''       as outdt          " + vbCrLf
                    sSql += "     , ''                    as inspector      " + vbCrLf
                    sSql += "     , ''        as indt           " + vbCrLf
                    sSql += "     , ''      as dondt          " + vbCrLf
                    sSql += "     , ''    as availdt        " + vbCrLf
                    sSql += "     , ''                      as outid          " + vbCrLf
                    sSql += "     , ''      as recnm                                                  " + vbCrLf
                    sSql += "     , '출'      as vkeepgbn       " + vbCrLf
                    sSql += "     , '9'           keepgbn          " + vbCrLf
                    sSql += "     , '' as elapsdm               " + vbCrLf
                End If

                sSql += "     , a.state                                                        " + vbCrLf
                sSql += "     , a.bldno                                                        " + vbCrLf
                sSql += "     , d.comnmp                                                       " + vbCrLf
                sSql += "     , a.fkocs || '-' || TO_CHAR(seq) as fkocs                        " + vbCrLf
                sSql += "     , c.cmt " + vbCrLf
                sSql += "  from lb043m a                                                       " + vbCrLf

                If rsFilter <> "1"c Then
                    sSql += "     , lb030m b                                                       " + vbCrLf
                End If

                sSql += "     , lb020m c                                                       " + vbCrLf
                sSql += "     , lf120m d                                                       " + vbCrLf
                sSql += " where a.tnsjubsuno = :tnsno                                               " + vbCrLf

                alParm.Add(New OracleParameter("tnsno", OracleDbType.Varchar2, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsnum))

                'sSql += "   and a.state      in ('4', '5', '6')                                "
                sSql += "   and a.state      = '4'                               " + vbCrLf

                If rsFilter <> "1"c Then
                    sSql += "   and a.bldno      = b.bldno                                         " + vbCrLf
                    sSql += "   and a.comcd_out  = b.comcd_out                                     " + vbCrLf
                    sSql += "   and a.tnsjubsuno = b.tnsjubsuno                                    " + vbCrLf
                End If

                sSql += "   and a.bldno      = c.bldno                                         " + vbCrLf
                sSql += "   and a.comcd_out  = c.comcd                                         " + vbCrLf
                sSql += "   and a.comcd_out  = d.comcd                                         " + vbCrLf
                sSql += "   and d.spccd      = :spccd                                               " + vbCrLf

                alParm.Add(New OracleParameter("spccd", OracleDbType.Varchar2, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액출고 "

        Public Shared Function fnGet_RBC_List() As List(Of String)
            ' 수혈 출고 대기 리스트
            Dim sFn As String = "Public Shared Function fnGet_RBC_List() As List(Of String)"
            Dim sSql As String = ""
            Dim dt As New DataTable

            Try
                sSql += "SELECT clsval FROM LF000M                " + vbCrLf
                sSql += " WHERE clsgbn = 'RBCL'                   "

                DbCommand()
                dt = DbExecuteQuery(sSql)

                Dim rturList As New List(Of String)
                For ix As Integer = 0 To dt.Rows.Count - 1
                    rturList.Add(dt.Rows(ix).Item("clsval").ToString())
                Next

                Return rturList

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_OutOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsBldno As String, ByVal rsGbn As String) As DataTable
            ' 수혈 출고 대기 리스트
            Dim sFn As String = "Public Shared Function fn_OutOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT fn_ack_get_tnsjubsuno_full(a.tnsjubsuno)  tnsjubsuno                " + vbCrLf
                sSql += "     , CASE WHEN a.reqqnt >  NVL(a.outqnt, 0) THEN '미'                            " + vbCrLf
                sSql += "            WHEN a.reqqnt <= NVL(a.outqnt, 0) THEN '완'                           " + vbCrLf
                sSql += "       END as vstate                                                              " + vbCrLf
                sSql += "     , a.comcd                                                                    " + vbCrLf
                sSql += "     , a.comnm as comnmd                                                          " + vbCrLf
                sSql += "     , a.reqqnt                                                                   " + vbCrLf
                sSql += "     , a.jubsudt                                                                  " + vbCrLf
                sSql += "     , a.befoutqnt                                                                " + vbCrLf
                sSql += "     , a.outqnt                                                                   " + vbCrLf
                sSql += "     , a.regno                                                                    " + vbCrLf
                sSql += "     , a.patnm                                                                    " + vbCrLf
                sSql += "     , r.abo                                     " + vbCrLf
                sSql += "     , r.rh                                      " + vbCrLf
                sSql += "     , a.spccd                                                                    " + vbCrLf
                sSql += "     , a.iogbn                                                                    " + vbCrLf
                sSql += "     , a.owngbn                                                                   " + vbCrLf
                sSql += "     , fn_ack_date_str(a.orddt, 'yyyy-mm-dd') order_date                                                               " + vbCrLf
                sSql += "     , a.tnsgbn                                                                   " + vbCrLf
                sSql += "     , a.filter                                                                   " + vbCrLf
                sSql += "     , a.comordcd                                                                 " + vbCrLf
                sSql += "     , a.state" + vbCrLf
                sSql += "  FROM " + vbCrLf

                If rsBldno.Length() > 0 Then
                    sSql += "       (SELECT tnsjubsuno, COUNT(tnsjubsuno) cnt" + vbCrLf
                    sSql += "          FROM lb043m" + vbCrLf
                    sSql += "         WHERE bldno = :bldno" + vbCrLf

                    alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

                    sSql += "        GROUP BY tnsjubsuno" + vbCrLf
                    sSql += "       ) b" + vbCrLf
                    sSql += "       INNER JOIN" + vbCrLf
                End If

                sSql += "       (SELECT a.tnsjubsuno                                                       " + vbCrLf
                sSql += "             , b.comcd                                                            " + vbCrLf
                sSql += "             , b.comnm                                                            " + vbCrLf
                sSql += "             , b.spccd                                                            " + vbCrLf
                sSql += "             , a.tnsgbn                                                           " + vbCrLf
                sSql += "             , NVL(b.reqqnt, 0)    as reqqnt                                      " + vbCrLf
                sSql += "             , NVL(b.befoutqnt, 0) as befoutqnt                                   " + vbCrLf
                sSql += "             , NVL(b.outqnt, 0) + NVL(b.rtnqnt, 0) +                              " + vbCrLf
                sSql += "               NVL(b.abnqnt, 0) /*+ NVL(b.cancelqnt, 0)*/ as outqnt                   " + vbCrLf
                sSql += "             , a.regno                                                            " + vbCrLf
                sSql += "             , a.patnm" + vbCrLf
                sSql += "             , fn_ack_date_str(a.jubsudt, 'yyyy-MM-dd hh24:mi') as jubsudt        " + vbCrLf
                sSql += "             , a.iogbn                                                            " + vbCrLf
                sSql += "             , a.owngbn                                                           " + vbCrLf
                sSql += "             , b.filter                                                           " + vbCrLf
                sSql += "             , c.comordcd                                                         " + vbCrLf
                sSql += "             , a.orddt                " + vbCrLf
                sSql += "             , b.state" + vbCrLf
                sSql += "          FROM lb040m a                                                           " + vbCrLf
                sSql += "             , lb042m b                                                           " + vbCrLf
                sSql += "             , lf120m c                                                           " + vbCrLf
                sSql += "         WHERE a.jubsudt BETWEEN :dates" + vbCrLf
                sSql += "                             AND :datee || '235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

                If rsRegno <> "" Then
                    sSql += "       AND a.regno      = :regno " + vbCrLf
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                If rsComcd <> "" And rsComcd <> "ALL" Then
                    sSql += "       AND b.comcd = :comcd " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If


                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                       " + vbCrLf
                sSql += "           AND b.comcd      = c.comcd                                            " + vbCrLf
                sSql += "           AND b.spccd      = c.spccd " + vbCrLf
                sSql += "           AND NVL(a.delflg, '0') = '0'" + vbCrLf
                sSql += "     ) a      " + vbCrLf
                If rsBldno.Length() > 0 Then
                    sSql += " ON (a.tnsjubsuno = b.tnsjubsuno) " + vbCrLf
                End If

                sSql += "     LEFT OUTER JOIN" + vbCrLf
                sSql += "         lr070m r ON (a.regno = r.regno)" + vbCrLf

                If rsGbn = "0"c Then

                ElseIf rsGbn = "1"c Then
                    sSql += " WHERE a.state = '0'" + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += " WHERE a.state = '1'" + vbCrLf
                End If

                sSql += " ORDER BY tnsjubsuno                                                               "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_StOrderList(ByVal rsTnsNum As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fn_POrderList(ByVal rsFdate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.tnsjubsuno                                           " + vbCrLf
                sSql += "     , fn_ack_get_bldno_full(a.bldno)                     as vbldno    " + vbCrLf
                sSql += "     , e.comnmd                                               " + vbCrLf
                sSql += "     , d.abo || d.rh                                  as type      " + vbCrLf
                sSql += "     , fn_ack_date_str(c.befoutdt, 'yyyy-MM-dd hh24:mi')  as befoutdt  " + vbCrLf
                sSql += "     , fn_ack_get_usr_name(c.testid)                      as testid    " + vbCrLf
                sSql += "     , fn_ack_date_str(d.indt, 'yyyy-MM-dd hh24:mi')      as indt      " + vbCrLf
                sSql += "     , fn_ack_date_str(d.dondt, 'yyyy-MM-dd hh24:mi')     as dondt     " + vbCrLf
                sSql += "     , fn_ack_date_str(d.availdt, 'yyyy-MM-dd hh24:mi')   as availdt   " + vbCrLf
                sSql += "     , c.comcd_out                                        as comcd_out     " + vbCrLf
                sSql += "     , a.comcd" + vbCrLf
                sSql += "     , a.owngbn                                               " + vbCrLf
                sSql += "     , a.iogbn                                                " + vbCrLf
                sSql += "     , a.fkocs || '-' || TO_CHAR(seq) as fkocs                " + vbCrLf
                sSql += "     , a.bldno                                        as bldno     " + vbCrLf
                sSql += "     , b.filter                                               " + vbCrLf
                sSql += "     , e.comordcd                                     as comordcd    " + vbCrLf
                sSql += "     , '9999999'                                      as sortkey   " + vbCrLf
                sSql += "     , d.cmt " + vbCrLf
                sSql += "  FROM lb040m b1" + vbCrLf
                sSql += "     , lb043m a " + vbCrLf
                sSql += "     , lb042m b                                               " + vbCrLf
                sSql += "     , lb030m c                                               " + vbCrLf
                sSql += "     , lb020m d                                               " + vbCrLf
                sSql += "     , lf120m e                                               " + vbCrLf
                sSql += " WHERE b1.tnsjubsuno = :tnsno                                       " + vbCrLf
                sSql += "   AND b1.tnsjubsuno = b.tnsjubsuno                            " + vbCrLf
                sSql += "   AND b1.tnsjubsuno = a.tnsjubsuno" + vbCrLf

                alParm.Add(New OracleParameter("tnsno", OracleDbType.Varchar2, rsTnsNum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNum))

                sSql += "   AND a.state      = '3'                                     " + vbCrLf
                sSql += "   AND a.bldno      = c.bldno                                 " + vbCrLf
                sSql += "   AND a.comcd_out  = c.comcd_out                             " + vbCrLf
                sSql += "   AND a.tnsjubsuno = c.tnsjubsuno                            " + vbCrLf
                sSql += "   AND a.bldno      = d.bldno                                 " + vbCrLf
                sSql += "   AND a.comcd_out  = d.comcd                                 " + vbCrLf
                sSql += "   AND a.comcd_out  = e.comcd                                 " + vbCrLf
                sSql += "   AND b.spccd      = e.spccd                                 " + vbCrLf
                sSql += "   AND b1.jubsudt  >= e.usdt" + vbCrLf
                sSql += "   AND b1.jubsudt  <  e.uedt" + vbCrLf
                sSql += " ORDER BY vbldno" + vbCrLf


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function Bld_Bfout_Chk(ByVal rsBldno As String, ByVal rsRegno As String) As String
            ' 가출고 환자 체크
            Dim sFn As String = "Public Shared Function Bld_Bfout_Chk(ByVal rsBldno As String, ByVal rsRegno As String) As String"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim dt As New DataTable

            Try
                sSql += " SELECT CASE WHEN COUNT(*) > 0 THEN 'Y' ELSE '' END YN "
                sSql += "   FROM LB030M A, LB043M B "
                sSql += "  WHERE A.BLDNO = B.BLDNO "
                sSql += "    AND A.TNSJUBSUNO = B.TNSJUBSUNO "
                sSql += "    AND A.BLDNO = :BLDNO "
                sSql += "    AND B.REGNO = :REGNO "

                alParm.Add(New OracleParameter("BLDNO", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("REGNO", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                DbCommand()
                dt = DbExecuteQuery(sSql, alParm)

                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0).Item("YN").ToString
                Else
                    Return ""
                End If

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

#End Region

#Region " 혈액반납/폐기 "
        Public Shared Function fn_RtnOrderList(ByVal rsRtnFlg As String, ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsBldno As String) As DataTable
            ' 반납 / 폐기 대상 리스트
            Dim sFn As String = "Public Shared Function fn_OutOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"

            Try

                Dim sSql As String = "pkg_ack_tns.pkg_get_tns_req_list"
                Dim sWhere As String = "rtnreqflg = '" + rsRtnFlg + "'"

                Dim oParm As New DBORA.DbParrameter

                With oParm
                    .AddItem("rs_orddt1", OracleDbType.Varchar2, ParameterDirection.Input, rsFdate)
                    .AddItem("rs_orddt2", OracleDbType.Varchar2, ParameterDirection.Input, rsTdate)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery(sSql, oParm, False)

                If rsRegno <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "regno = '" + rsRegno + "'"
                If rsComcd <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "comcd_out = '" + rsComcd + "'"
                If rsBldno <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "bldno = '" + rsBldno + "'"

                Dim a_dr As DataRow()
                a_dr = dt.Select(sWhere, "tnsjubsuno")
                dt = Fn.ChangeToDataTable(a_dr)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_RtnList(ByVal rsRtnFlg As String, ByVal rsTnsNo As String) As DataTable
            ' 반납 리스트
            Dim sFn As String = "Public Shared Function fn_RtnList(ByVal rsTnsNum As String, ByVal rsFilter As String) As DataTable"

            Try
                Dim sSql As String = "pkg_ack_tns.pkg_get_tns_rtn_list"
                Dim sWhere As String = "rtnflg = '" + rsRtnFlg + "'"
                Dim oParm As New DBORA.DbParrameter

                With oParm
                    .AddItem("rs_tnsno", OracleDbType.Varchar2, ParameterDirection.Input, rsTnsNo)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery(sSql, oParm, False)

                Dim a_dr As DataRow()
                a_dr = dt.Select(sWhere, "bldno")
                dt = Fn.ChangeToDataTable(a_dr)

                Return dt

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_RtnOutList(ByVal rsRtnFlg As String, ByVal rsTnsNo As String, ByVal rsRegNo As String) As DataTable
            ' 출고 리스트
            Dim sFn As String = "Public Shared Function fn_RtnOutList(ByVal rsTnsNum As String) As DataTable"

            Try
                Dim sSql As String = "pkg_ack_tns.pkg_get_tns_out_list"
                Dim sWhere As String = "(rtnreqflg = '" + rsRtnFlg + "' OR owngbn = 'L')"
                Dim oParm As New DBORA.DbParrameter

                With oParm
                    .AddItem("rs_tnsno", OracleDbType.Varchar2, ParameterDirection.Input, rsTnsNo)
                    .AddItem("rs_regno", OracleDbType.Varchar2, ParameterDirection.Input, rsRegNo)
                End With

                DbCommand(False)
                Dim dt As DataTable = DbExecuteQuery(sSql, oParm, False)

                Dim a_dr As DataRow()
                a_dr = dt.Select(sWhere, "bldno")
                dt = Fn.ChangeToDataTable(a_dr)

                Return dt

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액 재고량 조회 "
        Public Shared Function fn_StoredList(ByVal rsDate As String) As DataTable
            ' 재고량 리스트
            Dim sFn As String = "Public Shared Function fn_StoredList(ByVal rsDate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.comcd" + vbCrLf
                sSql += "     , a.comnmd                                                                    " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'A+'  THEN b.availqty END) as a1                     " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'B+'  THEN b.availqty END) as b1                     " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'O+'  THEN b.availqty END) as o1                     " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'AB+' THEN b.availqty END) as ab1                    " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'A-'  THEN b.availqty END) as a2                     " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'B-'  THEN b.availqty END) as b2                     " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'O-'  THEN b.availqty END) as o2                     " + vbCrLf
                sSql += "     , MAX(CASE WHEN b.type = 'AB-' THEN b.availqty END) as ab2                    " + vbCrLf
                sSql += "     , SUM(b.availqty)                                   as availqty               " + vbCrLf
                '20210317 jhs 유효일시가 10일, 13일 이하로 남은 것이 있는 경우 Y 조회
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'A+'  ,a.comcd)    as a110                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'B+'  ,a.comcd)    as b110                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'O+'  ,a.comcd)    as o110                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'AB+' ,a.comcd)    as ab110               " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'A-'  ,a.comcd)    as a210                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'B-'  ,a.comcd)    as b210                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'O-'  ,a.comcd)    as o210                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,10, 'AB-' ,a.comcd)    as ab210               " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'A+'  ,a.comcd)    as a113                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'B+'  ,a.comcd)    as b113                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'O+'  ,a.comcd)    as o113                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'AB+' ,a.comcd)    as ab113               " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'A-'  ,a.comcd)    as a213                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'B-'  ,a.comcd)    as b213                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'O-'  ,a.comcd)    as o213                " + vbCrLf
                sSql += "     , fn_ack_bld_availdtcount(:indt,13, 'AB-' ,a.comcd)    as ab213               " + vbCrLf
                '------------------------------------------------------------------------------------------------------
                sSql += "  FROM (SELECT a.comcd                                                             " + vbCrLf
                sSql += "             , b.comnm                                                             " + vbCrLf
                sSql += "             , b.comnmd                                                            " + vbCrLf
                sSql += "          FROM lb020m a,                                                           " + vbCrLf
                sSql += "               lf120m b                                                            " + vbCrLf
                sSql += "         WHERE a.comcd    = b.comcd" + vbCrLf
                sSql += "           AND a.availdt >= :indt" + vbCrLf
                sSql += "           AND a.indt    <= :indt || '235959'" + vbCrLf
                sSql += "           AND a.indt    >= b.usdt" + vbCrLf
                sSql += "           AND a.indt    <  b.uedt" + vbCrLf
                sSql += "           AND CASE WHEN NVL(a.editdt, fn_ack_sysdate) > :indt || '235959'" + vbCrLf

                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "                    THEN '0'                                                       " + vbCrLf
                sSql += "                    ELSE a.state                                                   " + vbCrLf
                sSql += "               END               = '0'                                             " + vbCrLf
                sSql += "         GROUP BY a.comcd, b.comnm, b.comnmd                                       " + vbCrLf
                sSql += "       ) a LEFT OUTER JOIN                                                         " + vbCrLf
                sSql += "       (SELECT a.comcd                                                             " + vbCrLf
                sSql += "             , a.abo || a.rh    as type                                             " + vbCrLf
                sSql += "             , COUNT(a.bldno) as availqty                                          " + vbCrLf
                sSql += "          FROM lb020m a,                                                           " + vbCrLf
                sSql += "               (SELECT DISTINCT comcd                                              " + vbCrLf
                sSql += "                     , comnm                                                       " + vbCrLf
                sSql += "                  FROM lf120m                                                      " + vbCrLf
                sSql += "               ) b                                                  " + vbCrLf
                sSql += "         WHERE a.comcd = b.comcd                                                   " + vbCrLf
                sSql += "           AND a.availdt >= :indt" + vbCrLf
                sSql += "           AND a.indt    <= :indt || '235959'" + vbCrLf
                sSql += "           AND CASE WHEN NVL(a.editdt, fn_ack_sysdate) > :indt || '235959'" + vbCrLf

                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "                    THEN '0'                                                       " + vbCrLf
                sSql += "                    ELSE a.state                                                   " + vbCrLf
                sSql += "               END               = '0'                                             " + vbCrLf
                sSql += "        GROUP BY a.comcd, b.comnm, a.abo || a.rh                                    " + vbCrLf
                sSql += "      ) b ON (a.comcd = b.comcd)                                                   " + vbCrLf
                sSql += "GROUP BY a.comcd, a.comnmd                                                         " + vbCrLf
                sSql += "ORDER BY a.comcd                                                                   " + vbCrLf

                DbCommand()

                fn_StoredList = DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_StdDetailList(ByVal rsDate As String, ByVal rsComcd As String, Optional ByVal rsAbo As String = "", Optional ByVal rsRh As String = "") As DataTable
            ' 재고 세부 리스트
            Dim sFn As String = "Public Shared Function fn_StdDetailList(ByVal rsComcd As String, Optional ByVal rsAbo As String = "", Optional ByVal rsRh As String = "") As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT b.comnmd" + vbCrLf
                sSql += "     , a.abo || a.rh                                as aborh" + vbCrLf
                sSql += "     , fn_ack_get_bldno_full(a.bldno)                   as bldno" + vbCrLf
                sSql += "     , fn_ack_date_str(a.dondt, 'yyyy-MM-dd hh24:mi')   as dondt" + vbCrLf
                sSql += "     , fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') as availdt" + vbCrLf
                sSql += "     , fn_ack_date_str(a.indt, 'yyyy-mm-dd hh24:mi')    as indt" + vbCrLf
                sSql += "     , fn_ack_get_usr_name(a.inid)                      as inid" + vbCrLf
                sSql += "  FROM lb020m a," + vbCrLf
                sSql += "       lf120m b" + vbCrLf
                sSql += " WHERE a.comcd    = b.comcd" + vbCrLf
                sSql += "   AND a.indt    >= b.usdt" + vbCrLf
                sSql += "   AND a.indt    <  b.uedt" + vbCrLf
                sSql += "   AND a.availdt >= :indt" + vbCrLf
                sSql += "   AND a.indt    <= :indt || '235959'" + vbCrLf
                sSql += "   AND CASE WHEN NVL(a.editdt, fn_ack_sysdate) > :indt || '235959'" + vbCrLf

                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "            THEN '0'" + vbCrLf
                sSql += "            ELSE a.state" + vbCrLf
                sSql += "       END               = '0'" + vbCrLf
                sSql += "   AND a.comcd = :comcd" + vbCrLf

                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                If rsAbo.Length() > 0 Then
                    sSql += "   AND a.abo   = :abo" + vbCrLf
                    sSql += "   AND a.rh    = :rh" + vbCrLf

                    alParm.Add(New OracleParameter("abo", OracleDbType.Varchar2, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                    alParm.Add(New OracleParameter("rh", OracleDbType.Varchar2, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))
                End If


                sSql += " ORDER BY a.dondt, a.availdt, a.indt, b.comcd, a.bldno" + vbCrLf

                DbCommand()

                fn_StdDetailList = DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액이력조회 "
        Public Shared Function fn_GetBldInfo(ByVal rsBldno As String, Optional ByVal rsComcd As String = "") As DataTable
            ' 혈액정보
            Dim sFn As String = "Public Shared Function fn_GetBldInfo(String, [String]) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.bldno                                                    "
                sSql += "     , fn_ack_get_bldno_full(a.bldno) as vbldno                   "
                sSql += "     , a.donqnt                                                   "
                sSql += "     , fn_ack_get_usr_name(a.inid) as inid                        "
                sSql += "     , CASE WHEN a.state = '0' THEN '미출고' WHEN a.state = '1' THEN '접수'"
                sSql += "            WHEN a.state = '2' THEN '검사중' WHEN a.state = '3' THEN '가출고'"
                sSql += "            WHEN a.state = '4' THEN '출고'   "
                'sSql += "           WHEN a.state = '5' THEN '반납'    WHEN a.state = '6' THEN '폐기'                        "
                sSql += "            WHEN a.state = '5' THEN nvl2(b.regno,'반납' ,'자체폐기')                        " '<20150921 반납폐기 잘못나오는 부분 수정 
                sSql += "            WHEN a.state = '6' THEN nvl2(b.regno,'폐기','교환')                        "
                sSql += "       END state                                                  "
                sSql += "     , fn_ack_date_str(a.dondt, 'yyyy-mm-dd hh24:mi') dondt       "
                sSql += "     , a.abo                                                      "
                sSql += "     , a.rh                                                       "
                sSql += "     , a.abo || a.rh aborh                                         "
                sSql += "     , b.regno                                                    "
                sSql += "     , b.orddt                                                    "
                sSql += "  FROM lb020m a LEFT OUTER JOIN                                   "
                sSql += "       (                                                          "
                sSql += "        SELECT x.bldno                                            "
                sSql += "             , x.comcd_out                                        "
                sSql += "             , y.regno                                            "
                sSql += "             , fn_ack_date_str(y.orddt, 'yyyy-MM-dd') as orddt    "
                sSql += "          FROM lb030m x                                           "
                sSql += "             , lb040m y                                           "
                sSql += "         WHERE x.tnsjubsuno = y.tnsjubsuno                        "
                sSql += "         UNION                                                    "
                sSql += "        SELECT x.bldno                        "
                sSql += "             , x.comcd_out                                        "
                sSql += "             , y.regno                                            "
                sSql += "             , fn_ack_date_str(y.orddt, 'yyyy-MM-dd') as orddt    "
                sSql += "          FROM lb031m x LEFT OUTER JOIN                           "
                sSql += "               lb040m y ON (x.tnsjubsuno = y.tnsjubsuno)          "
                sSql += "       ) b ON (a.bldno = b.bldno AND a.comcd = b.comcd_out)       "
                sSql += " WHERE a.bldno = :bldno                                                "
                sSql += "   AND ROWNUM  = 1"

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

                If rsComcd.Length() > 1 Then
                    sSql += "   AND a.comcd = :comcd                                            "

                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_GetComcdList(ByVal rsBldno As String) As DataTable
            ' 혈액번호별 성분제제정보
            Dim sFn As String = "Public Shared Function fn_GetComcdList(ByVal rsBldno As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bldno_full(a.bldno) as vbldno                    "
                sSql += "     , a.comcd                                                     "
                sSql += "     , b.comnmd              as comnmd                             "
                sSql += "     , fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi') as indt       "
                sSql += "     , fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') as availdt "
                sSql += "  FROM lb020m a                                            "
                sSql += "     , lf120m b                                            "
                sSql += " WHERE a.bldno = :bldno                                         "

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

                sSql += "   AND a.comcd = b.comcd                                   "
                sSql += "ORDER BY comcd                                             "

                DbCommand()

                fn_GetComcdList = DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_GetBldHisList(ByVal rsBldno As String, ByVal rsComcd As String) As DataTable
            ' 혈액번호별 이력조회
            Dim sFn As String = "Public Shared Function fn_GetBldHisList(ByVal rsBldno As String, ByVal rsComcd As string) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql = "" + vbCrLf
                '-- 입고 
                sSql += "SELECT CASE WHEN NVL(editid, ' ') = ' ' THEN '초입고' ELSE '입고' END state," + vbCrLf
                sSql += "       fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi:ss') workdt," + vbCrLf
                sSql += "       fn_ack_get_usr_name(a.inid)  worknm," + vbCrLf
                sSql += "       '' recid, '' recnm, '' tnsgbn, '' regno, '' patnm," + vbCrLf
                sSql += "       '' vtnsjubsuno, a.abo, a.rh, '' rtnrsncmt," + vbCrLf
                sSql += "       '0' sort_key" + vbCrLf
                sSql += "  FROM lb020m a" + vbCrLf
                sSql += " WHERE a.bldno = :bldno" + vbCrLf
                sSql += "   AND a.comcd = :comcd" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 검사중
                sSql += " UNION " + vbCrLf
                sSql += "SELECT '검사' state," + vbCrLf
                sSql += "        fn_ack_date_str(b.testdt, 'yyyy-MM-dd hh24:mi:ss') workdt," + vbCrLf
                sSql += "        fn_ack_get_usr_name(b.testid) woknm," + vbCrLf
                sSql += "        '' recid, '' recnm," + vbCrLf
                sSql += "        CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'" + vbCrLf
                sSql += "             WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'" + vbCrLf
                sSql += "        END tnsgbn," + vbCrLf
                sSql += "        b.regno, b.patnm, " + vbCrLf
                sSql += "        fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno,      " + vbCrLf
                sSql += "        a.abo, a.rh, '' rtnrsncmt, " + vbCrLf
                sSql += "        b.sort_key" + vbCrLf
                sSql += "   FROM lb020m a," + vbCrLf
                sSql += "        (SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.testdt || '1' sort_key" + vbCrLf
                sSql += "           FROM lb030m x, lb040m y" + vbCrLf
                sSql += "          WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "            AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "            AND NVL(x.testid, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          UNION" + vbCrLf
                sSql += "         SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno,  x.testdt || '1' sort_key" + vbCrLf
                sSql += "           FROM lb031m x, lb040m y" + vbCrLf
                sSql += "          WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "            AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "            AND NVL(x.testid, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          UNION" + vbCrLf
                sSql += "         SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.testdt || '1' sort_key" + vbCrLf
                sSql += "           FROM lb030h x, lb040m y" + vbCrLf
                sSql += "          WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "            AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "            AND NVL(x.testid, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          UNION" + vbCrLf
                sSql += "         SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.testdt || '1' sort_key                                                                        " + vbCrLf
                sSql += "           FROM lb031h x, lb040m y" + vbCrLf
                sSql += "          WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "            AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "            AND NVL(x.testid, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b" + vbCrLf
                sSql += " WHERE a.bldno = :bldno" + vbCrLf
                sSql += "   AND a.comcd = :comcd" + vbCrLf
                sSql += "   AND a.bldno = b.bldno" + vbCrLf
                sSql += "   AND a.comcd = b.comcd_out" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 가출고
                sSql += " UNION " + vbCrLf
                sSql += "SELECT '가출고' state," + vbCrLf
                sSql += "       fn_ack_date_str(b.befoutdt, 'yyyy-MM-dd hh24:mi:ss') workdt," + vbCrLf
                sSql += "       fn_ack_get_usr_name(b.befoutid) worknm," + vbCrLf
                sSql += "       '' recid, '' recnm," + vbCrLf
                sSql += "       CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'" + vbCrLf
                sSql += "            WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'" + vbCrLf
                sSql += "       END tnsgbn," + vbCrLf
                sSql += "       b.regno, b.patnm," + vbCrLf
                sSql += "       fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno," + vbCrLf
                sSql += "       a.abo, a.rh, '' rtnrsncmt," + vbCrLf
                sSql += "       b.sort_key" + vbCrLf
                sSql += "  FROM lb020m a," + vbCrLf
                sSql += "       (SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.befoutdt || '2' sort_key" + vbCrLf
                sSql += "          FROM lb030m x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION" + vbCrLf
                sSql += "        SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.befoutdt || '2' sort_key" + vbCrLf
                sSql += "          FROM lb031m x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION" + vbCrLf
                sSql += "        SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.befoutdt || '2' sort_key" + vbCrLf
                sSql += "          FROM lb030h x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION" + vbCrLf
                sSql += "        SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.befoutdt || '2' sort_key" + vbCrLf
                sSql += "          FROM lb031h x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b" + vbCrLf
                sSql += " WHERE a.bldno = :bldno" + vbCrLf
                sSql += "   AND a.comcd = :comcd" + vbCrLf
                sSql += "   AND a.bldno = b.bldno" + vbCrLf
                sSql += "   AND a.comcd = b.comcd_out" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += " UNION " + vbCrLf
                sSql += "SELECT '출고' state," + vbCrLf
                sSql += "       fn_ack_date_str(b.outdt, 'yyyy-MM-dd hh24:mi:ss') workdt," + vbCrLf
                sSql += "       fn_ack_get_usr_name(b.outid) worknm," + vbCrLf
                sSql += "       b.recid recid, b.recnm recnm," + vbCrLf
                sSql += "       CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'" + vbCrLf
                sSql += "            WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'" + vbCrLf
                sSql += "       END tnsgbn," + vbCrLf
                sSql += "       b.regno, b.patnm," + vbCrLf
                sSql += "       fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno," + vbCrLf
                sSql += "       a.abo, a.rh, '' rtnrsncmt," + vbCrLf
                sSql += "       b.sort_key" + vbCrLf
                sSql += "  FROM lb020m a," + vbCrLf
                sSql += "       (SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key" + vbCrLf
                sSql += "          FROM lb030m x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno " + vbCrLf
                sSql += "           AND NVL(x.outdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION" + vbCrLf
                sSql += "        SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key" + vbCrLf
                sSql += "          FROM lb031m x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.outdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION" + vbCrLf
                sSql += "        SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key" + vbCrLf
                sSql += "          FROM lb030h x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.outdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION" + vbCrLf
                sSql += "        SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key" + vbCrLf
                sSql += "          FROM lb031h x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.outdt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b" + vbCrLf
                sSql += " WHERE a.bldno = :bldno" + vbCrLf
                sSql += "   AND a.comcd = :comcd" + vbCrLf
                sSql += "   AND a.bldno = b.bldno" + vbCrLf
                sSql += "   AND a.comcd = b.comcd_out" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 반납/폐기                                                             
                sSql += " UNION " + vbCrLf
                sSql += "SELECT CASE WHEN b.rtnflg = '1' THEN '반납' " + vbCrLf
                sSql += "            WHEN b.rtnflg = '2' THEN '폐기'" + vbCrLf
                sSql += "            ELSE '접수취소'" + vbCrLf
                sSql += "       END state," + vbCrLf
                sSql += "       fn_ack_date_str(b.rtndt, 'yyyy-MM-dd hh24:mi:ss') workdt," + vbCrLf
                sSql += "       fn_ack_get_usr_name(b.rtnid) worknm," + vbCrLf
                sSql += "       '' recid, '' recnm," + vbCrLf
                sSql += "       CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'" + vbCrLf
                sSql += "            WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'" + vbCrLf
                sSql += "       END as tnsgbn,      " + vbCrLf
                sSql += "       b.regno, b.patnm," + vbCrLf
                sSql += "       fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno," + vbCrLf
                sSql += "       a.abo, a.rh, b.rtnrsncmt," + vbCrLf
                sSql += "       b.sort_key" + vbCrLf
                sSql += "  FROM lb020m a," + vbCrLf
                sSql += "       (SELECT x.bldno, x.comcd_out, x.rtndt, x.rtnid, x.rtnflg, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.rtndt || '4' sort_key, x.rtnrsncmt, x.keepgbn" + vbCrLf
                sSql += "          FROM lb031m x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.rtndt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION" + vbCrLf
                sSql += "        SELECT x.bldno, x.comcd_out, x.rtndt, x.rtnid, x.rtnflg, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.rtndt || '4' sort_key, x.rtnrsncmt, x.keepgbn" + vbCrLf
                sSql += "          FROM lb031h x, lb040m y" + vbCrLf
                sSql += "         WHERE x.bldno      = :bldno" + vbCrLf
                sSql += "           AND x.comcd_out  = :comcd" + vbCrLf
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno" + vbCrLf
                sSql += "           AND NVL(x.rtndt, ' ') <> ' '" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b" + vbCrLf
                sSql += " WHERE a.bldno = :bldno" + vbCrLf
                sSql += "   AND a.comcd = :comcd" + vbCrLf
                sSql += "   AND a.bldno = b.bldno" + vbCrLf
                sSql += "   AND a.comcd = b.comcd_out" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 자체폐기/교환
                sSql += " UNION " + vbCrLf
                sSql += "SELECT CASE WHEN b.keepgbn = '5' THEN '자체폐기' ELSE '교환' END state," + vbCrLf
                sSql += "       fn_ack_date_str(b.rtndt, 'yyyy-MM-dd hh24:mi:ss') workdt," + vbCrLf
                sSql += "       fn_ack_get_usr_name(b.rtnid) worknm," + vbCrLf '<20150921 교환자 이름 오류 수정 
                sSql += "       '' recid, '' recnm, '' tnsgbn, '' regno, '' patnm, '' vtnsjubsuno," + vbCrLf
                sSql += "       a.abo, a.rh, '' rtnrsncmt," + vbCrLf
                sSql += "       '9' sort_key" + vbCrLf
                sSql += "  FROM lb020m a, lb031m b" + vbCrLf
                sSql += " WHERE a.bldno = :bldno" + vbCrLf
                sSql += "   AND a.comcd = :comcd" + vbCrLf
                sSql += "  AND a.bldno = b.bldno" + vbCrLf
                sSql += "  AND a.comcd = b.comcd_out" + vbCrLf
                sSql += "  AND NVL(b.tnsjubsuno, ' ') = ' '" + vbCrLf
                sSql += " ORDER BY sort_key DESC, workdt DESC" + vbCrLf

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 수혈의뢰서 조회 "
        Public Shared Function fn_TnsSearchList(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String, ByVal rsTnsGbn As String,
                                                Optional ByVal rsRegno As String = "", Optional ByVal rsState As String = "",
                                                Optional ByVal rsDept As String = "", Optional ByVal rsWard As String = "", Optional ByVal rsIoGbn As String = "",
                                                Optional ByVal rsRstDay As String = "", Optional ByVal rsRst_Hb As String = "", Optional ByVal rsRst_Plt1 As String = "",
                                                Optional ByVal rsRst_Plt2 As String = "", Optional ByVal rsBranchComCd As Boolean = False) As DataTable
            ' 재고 세부 리스트
            Dim sFn As String = "Public Shared Function fn_TnsSearchList(String, String, String, String, [String]..) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.regno, a.patnm, a.sex || '/' || a.age sexage," + vbCrLf
                sSql += "       fn_ack_date_str(a.orddt, 'yyyy-MM-dd') orddt," + vbCrLf
                sSql += "       fn_ack_get_dr_name(a.doctorcd) docnm," + vbCrLf
                sSql += "       fn_ack_get_dept_abbr(a.iogbn, a.deptcd) deptnm," + vbCrLf
                sSql += "       fn_ack_get_ward_abbr(a.wardno) || '/' || a.roomno wdsr," + vbCrLf
                sSql += "       CASE WHEN a.tnsgbn = '1' THEN '준비'      WHEN a.tnsgbn = '2' THEN '수혈'" + vbCrLf
                sSql += "            WHEN a.tnsgbn = '3' THEN '교차미필'  WHEN a.tnsgbn = '4' THEN 'Irra'" + vbCrLf
                sSql += "       END comgbn," + vbCrLf
                sSql += "       fn_ack_get_tnsjubsuno_full(a.tnsjubsuno) vtnsjubsuno," + vbCrLf
                sSql += "       c.comcd_out, f.comnmd," + vbCrLf
                sSql += "       (SELECT abo || rh FROM lr070m WHERE regno = a.regno) aborh," + vbCrLf
                sSql += "       NVL(b.reqqnt, 0)    reqqnt," + vbCrLf
                sSql += "       NVL(b.befoutqnt, 0) befoutqnt," + vbCrLf
                sSql += "       NVL(b.outqnt, 0)    outqnt," + vbCrLf
                sSql += "       NVL(b.rtnqnt, 0)    rtnqnt," + vbCrLf
                sSql += "       NVL(b.abnqnt, 0)    abnqnt," + vbCrLf
                sSql += "       NVL(b.cancelqnt, 0) cancelqnt," + vbCrLf
                sSql += "       g.abo || g.rh aborhBld," + vbCrLf
                sSql += "       fn_ack_get_bldno_full(c.bldno) vbldno," + vbCrLf
                sSql += "       CASE WHEN c.state = '0' THEN '취소'   WHEN c.state = '1' THEN '접수' WHEN c.state = '2' THEN '검사중'" + vbCrLf
                sSql += "            WHEN c.state = '3' THEN '가출고' WHEN c.state = '4' THEN '출고' WHEN c.state = '5' THEN '반납'" + vbCrLf
                sSql += "            WHEN c.state = '6' THEN '폐기'" + vbCrLf
                sSql += "       END state," + vbCrLf
                sSql += "       fn_ack_date_str(a.jubsudt, 'yyyy-MM-dd hh24:mi') jubsudt," + vbCrLf
                sSql += "       NVL(d.rst1, e.rst1) rst1," + vbCrLf
                sSql += "       NVL(d.rst2, e.rst2) rst2," + vbCrLf
                sSql += "       NVL(d.rst3, e.rst3) rst3," + vbCrLf
                sSql += "       NVL(d.rst4, e.rst4) rst4," + vbCrLf
                sSql += "       fn_ack_date_str(NVL(d.testdt,   e.testdt),   'yyyy-MM-dd hh24:mi') testdt,   fn_ack_get_usr_name(NVL(d.testid,   e.testid))   testid," + vbCrLf
                sSql += "       fn_ack_date_str(NVL(d.befoutdt, e.befoutdt), 'yyyy-MM-dd hh24:mi') befoutdt, fn_ack_get_usr_name(NVL(d.befoutid, e.befoutid)) befoutid," + vbCrLf
                sSql += "       fn_ack_date_str(NVL(d.outdt,    e.outdt),    'yyyy-MM-dd hh24:mi') outdt,    fn_ack_get_usr_name(NVL(d.outid,    e.outid))    outid," + vbCrLf
                sSql += "       NVL(d.recnm, e.recnm) recnm," + vbCrLf
                sSql += "       fn_ack_date_str(e.rtndt, 'yyyy-MM-dd hh24:mi') rtndt, fn_ack_get_usr_name(e.rtnid) rtnid" + vbCrLf

                If rsRstDay <> "" And rsRst_Plt1.Length + rsRst_Plt2.Length > 0 Then
                    sSql += ", r.orgrst" + vbCrLf
                End If

                sSql += "  FROM lb040m a" + vbCrLf
                If rsRstDay <> "" And rsRst_Plt1.Length + rsRst_Plt2.Length > 0 Then
                    sSql += "       INNER JOIN" + vbCrLf
                    sSql += "             lr010m r ON (a.regno = r.regno AND r.rstflg = '3' AND r.tkdt >= fn_ack_get_date(TO_DATE(a.jubsudt, 'yyyymmddhh24miss') - 1) AND r.tkdt <= a.jubsudt)" + vbCrLf
                    sSql += "       INNER JOIN" + vbCrLf
                    sSql += "             lf140m f14 ON (r.testcd = f14.testcd AND r.spccd = f14.spccd AND f14.bbgbn = 'B')" + vbCrLf
                End If
                sSql += "       INNER JOIN" + vbCrLf
                sSql += "             lb042m b ON (a.tnsjubsuno = b.tnsjubsuno)" + vbCrLf
                sSql += "       INNER JOIN" + vbCrLf
                sSql += "             lb043m c ON (a.tnsjubsuno = c.tnsjubsuno)" + vbCrLf
                sSql += "       INNER JOIN" + vbCrLf
                sSql += "             lf120m f ON (c.comcd = f.comcd AND c.spccd = f.spccd AND f.usdt <= a.jubsudt AND f.uedt > a.jubsudt)" + vbCrLf
                sSql += "       LEFT OUTER JOIN" + vbCrLf
                sSql += "             lb030m d ON (d.tnsjubsuno = c.tnsjubsuno AND d.comcd = c.comcd AND d.bldno = c.bldno)" + vbCrLf
                sSql += "       LEFT OUTER JOIN" + vbCrLf
                sSql += "             lb031m e ON (e.tnsjubsuno = c.tnsjubsuno AND e.comcd = c.comcd AND e.bldno = c.bldno)                                                     " + vbCrLf
                sSql += "       LEFT OUTER JOIN" + vbCrLf
                sSql += "             lb020m g ON (g.comcd = c.comcd_out AND g.bldno = c.bldno)" + vbCrLf
                sSql += " WHERE a.jubsudt BETWEEN :dates ||'000000'" + vbCrLf
                sSql += "                     AND :datee || '235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsRegno.Length() > 0 Then
                    sSql += "   AND a.regno = :regno" + vbCrLf
                    alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                If rsComcd <> "ALL" Then
                    'sSql += "   AND c.comcd = :comcd"
                    'alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    '20210106 jhs 성분제재 묶음으로 조회
                    If rsBranchComCd Then
                        sSql += "   AND c.comcd in (" + rsComcd + ")" + vbCrLf
                        'alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    Else
                        sSql += "   AND c.comcd = :comcd" + vbCrLf
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                    '------------------------------------------------------------------------------------------------
                End If

                If rsIoGbn <> "" Then
                    '20210419 jhs 응급추가, 입원시 dsc 낮병동 같이 조회
                    If rsIoGbn = "I" Then
                        sSql += "   AND a.iogbn in ('I','D')" + vbCrLf
                    ElseIf rsIoGbn = "O" Then
                        sSql += "   AND a.iogbn = 'O'" + vbCrLf
                    ElseIf rsIoGbn = "E" Then
                        sSql += "   AND a.iogbn = 'E'" + vbCrLf
                    End If
                    'sSql += "   AND a.iogbn = :iogbn" + vbCrLf
                    'alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))
                    '------------------------------------------------------

                    If rsWard <> "ALL" Then
                        sSql += "   AND a.wardno = :wardcd" + vbCrLf
                        alParm.Add(New OracleParameter("wardcd", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If

                    If rsDept <> "ALL" Then
                        sSql += "   AND a.deptcd = :deptcd" + vbCrLf
                        alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                    End If
                End If

                If rsTnsGbn <> "ALL" Then
                    sSql += "   AND a.tnsgbn = :tnsgbn" + vbCrLf
                    alParm.Add(New OracleParameter("tnsgbn", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsGbn))
                End If

                If rsState.Length() > 0 Then
                    sSql += "   AND c.state in (" + rsState + ")" + vbCrLf
                End If

                If rsRstDay <> "" And rsRst_Hb.Length > 0 Then
                    Dim sWhere1 As String = ""

                    If rsRst_Hb.Length > 0 Then
                        sWhere1 += "a.regno IN (SELECT r.regno FROM lr010m r, lf140m f" + vbCrLf
                        sWhere1 += "             WHERE r.tkdt  >= fn_ack_get_date(TO_DATE(a.jubsudt, 'yyyymmddhh24miss') - 1)" + vbCrLf
                        sWhere1 += "               AND r.tkdt  <= a.jubsudt" + vbCrLf
                        sWhere1 += "               AND r.rstflg  = '3'" + vbCrLf
                        sWhere1 += "               AND r.viewrst =  '" + rsRst_Hb + "'" + vbCrLf
                        sWhere1 += "               AND r.testcd  = f.testcd" + vbCrLf
                        sWhere1 += "               AND r.spccd   = f.spccd" + vbCrLf
                        sWhere1 += "               AND f.bbgbn   = 'A'" + vbCrLf
                        sWhere1 += "           )"
                    End If

                    sSql += "   AND " + sWhere1
                End If


                sSql += " ORDER BY a.tnsjubsuno DESC, c.comcd_out, a.regno, b.state" + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '20210106 jhs 성분제재 묶음으로 조회
        Public Shared Function fn_get_BranchComCd_List(ByVal rsUsDt As String, ByVal rsComcd As String) As String
            Dim sFn As String = "Public Shared Function fn_TnsSearchList(String, String, String, String, [String]..) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += " "
                sSql += "  select a.clscd from lf000m a "
                sSql += " inner join lf120m b"
                sSql += " on  a.clscd = b.comcd"

                If rsUsDt <> "" Then
                    sSql += "   AND b.usdt <= :usdt"
                    sSql += "   AND b.uedt >  :usdt"

                    alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                Else
                    sSql += "   AND b.usdt <= fn_ack_sysdate"
                    sSql += "   AND b.uedt >  fn_ack_sysdate"

                End If

                sSql += " where a.clsgbn = 'B14' "
                sSql += " and a.clsval = :comcd "

                alParm.Add(New OracleParameter("usdt", OracleDbType.Varchar2, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                DbCommand()
                Dim dt As DataTable = DbExecuteQuery(sSql, alParm)
                Dim rtComcds As String = ""

                For i As Integer = 0 To dt.Rows.Count - 1
                    If i = 0 Then
                        rtComcds = "'" + dt.Rows(i).Item("clscd").ToString.Trim + "'"
                    Else
                        rtComcds = rtComcds + ",'" + dt.Rows(i).Item("clscd").ToString.Trim + "'"
                    End If
                Next

                Return rtComcds

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
        '-------------------------------------------------------------

        Public Shared Function fn_outComcdList(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String,
                                               ByVal rsDept As String, ByVal rsWard As String, ByVal rsIoGbn As String,
                                               Optional ByVal rsRstDay As String = "",
                                               Optional ByVal rsRst_Hb As String = "", Optional ByVal rsRst_Plt1 As String = "", Optional ByVal rsRst_Plt2 As String = "",
                                               Optional ByVal rsABORh As String = "", Optional ByVal rsBranchComCd As Boolean = False) As DataTable
            ' 성분제제 & 혈액형별 리스트
            Dim sFn As String = "Public Shared Function fn_outComcdList(ByVal rsfDate As String, ByVal rstDate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "select c.comcd,"
                sSql += "       f.comnmd,"
                sSql += "       SUM(CASE WHEN c.abo = 'A'  THEN CASE WHEN c.rh = '+' THEN 1 ELSE 0 END ELSE 0 END) as a1,"
                sSql += "       SUM(CASE WHEN c.abo = 'A'  THEN CASE WHEN c.rh = '-' THEN 1 ELSE 0 END ELSE 0 END) as a2,"
                sSql += "       SUM(CASE WHEN c.abo = 'B'  THEN CASE WHEN c.rh = '+' THEN 1 ELSE 0 END ELSE 0 END) as b1,"
                sSql += "       SUM(CASE WHEN c.abo = 'B'  THEN CASE WHEN c.rh = '-' THEN 1 ELSE 0 END ELSE 0 END) as b2,"
                sSql += "       SUM(CASE WHEN c.abo = 'O'  THEN CASE WHEN c.rh = '+' THEN 1 ELSE 0 END ELSE 0 END) as o1,"
                sSql += "       SUM(CASE WHEN c.abo = 'O'  THEN CASE WHEN c.rh = '-' THEN 1 ELSE 0 END ELSE 0 END) as o2,"
                sSql += "       SUM(CASE WHEN c.abo = 'AB' THEN CASE WHEN c.rh = '+' THEN 1 ELSE 0 END ELSE 0 END) as ab1,"
                sSql += "       SUM(CASE WHEN c.abo = 'AB' THEN CASE WHEN c.rh = '-' THEN 1 ELSE 0 END ELSE 0 END) as ab2,"
                sSql += "       SUM(CASE WHEN NVL(c.abo || c.rh, ' ') = ' ' THEN 0 ELSE 1 END) as allcnt"
                sSql += "  FROM lb040m a"
                sSql += "       INNER JOIN"
                sSql += "             lb042m b ON (a.tnsjubsuno = b.tnsjubsuno)"
                sSql += "       INNER JOIN"
                sSql += "             lb043m c ON (b.tnsjubsuno = c.tnsjubsuno)"
                sSql += "       INNER JOIN"
                sSql += "             lf120m f ON (c.comcd = f.comcd AND c.spccd = f.spccd AND f.usdt <= a.jubsudt AND f.uedt > a.jubsudt)"
                sSql += "       LEFT  OUTER JOIN"
                sSql += "             lb030m d ON (d.tnsjubsuno = c.tnsjubsuno AND d.comcd = c.comcd AND d.bldno = c.bldno)"
                sSql += "       LEFT  OUTER JOIN"
                sSql += "             lb031m e ON (e.tnsjubsuno = c.tnsjubsuno AND e.comcd = c.comcd AND e.bldno = c.bldno)                                                     "
                sSql += "       LEFT  OUTER JOIN"
                sSql += "             lb020m g ON (g.comcd = c.comcd_out AND g.bldno = c.bldno)"
                sSql += " WHERE a.jubsudt BETWEEN :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsComcd <> "ALL" Then
                    If rsBranchComCd Then
                        sSql += "   AND c.comcd_out in (" + rsComcd + ")"
                        'alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    Else
                        sSql += "   AND c.comcd_out = :comcd"
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                End If

                If rsIoGbn <> "" Then
                    sSql += "   AND a.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleDbType.Varchar2, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))

                    If rsDept <> "ALL" Then
                        sSql += "   AND a.deptcd = :deptcd"
                        alParm.Add(New OracleParameter("deptcd", OracleDbType.Varchar2, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                    End If

                    If rsWard <> "ALL" Then
                        sSql += "   AND a.wardno = :wardcd"
                        alParm.Add(New OracleParameter("wardcd", OracleDbType.Varchar2, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If
                End If

                If rsABORh <> "" Then
                    sSql += "   AND c.abo || c.rh = :aborh"
                    alParm.Add(New OracleParameter("aborh", OracleDbType.Varchar2, rsABORh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsABORh))
                End If

                If rsRstDay <> "" And rsRst_Hb.Length + rsRst_Plt1.Length + rsRst_Plt2.Length > 0 Then
                    Dim sWhere1 As String = ""
                    Dim sWhere2 As String = ""

                    If rsRst_Hb.Length > 0 Then
                        sWhere1 += "a.regno IN (SELECT r.regno FROM lr010m r, lf140m f"
                        sWhere1 += "             WHERE r.tkdt   >= fn_ack_get_date(TO_DATE(a.jubsudt, 'yyyymmddhh24miss') - 1)"
                        sWhere1 += "               AND r.tkdt   <= a.jubsudt"
                        sWhere1 += "               AND r.rstflg  = '3'"
                        sWhere1 += "               AND r.viewrst =  '" + rsRst_Hb + "'"
                        sWhere1 += "               AND r.testcd  = f.testcd"
                        sWhere1 += "               AND r.spccd   = f.spccd"
                        sWhere1 += "               AND f.bbgbn   = 'A'"
                        sWhere1 += "           )"
                    End If

                    If rsRst_Plt1.Length + rsRst_Plt2.Length > 0 Then
                        sWhere2 += "a.regno IN (SELECT r.regno"
                        sWhere2 += "              FROM (SELECT r.regno, fn_get_convert_real(r.orgrst) orgrst"
                        sWhere2 += "                      FROM lr010m r, lf140m f"
                        sWhere2 += "                     WHERE r.tkdt   >= fn_ack_get_date(TO_DATE(a.jubsudt, 'yyyymmddhh24miss') - 1)"
                        sWhere2 += "                       AND r.tkdt   <= a.jubsudt"
                        sWhere2 += "                       AND r.rstflg  = '3'"
                        sWhere2 += "                       AND ISNUMERIC(r.orgrst) = 1"
                        sWhere2 += "                       AND r.testcd  = f.testcd"
                        sWhere2 += "                       AND r.spccd   = f.spccd"
                        sWhere2 += "                       AND f.bbgbn   = 'B'"
                        sWhere2 += "                   ) r"

                        If rsRst_Plt1 <> "" And rsRst_Plt2 <> "" Then
                            sWhere2 += "             WHERE r.orgrst >=  " + rsRst_Plt1 + ""
                            sWhere2 += "               AND r.orgrst <=  " + rsRst_Plt2 + ""
                        Else
                            sWhere2 += "             WHERE r.orgrst = '" + rsRst_Plt1 + rsRst_Plt2 + "'"
                        End If
                        sWhere2 += "           )"

                    End If

                    If sWhere1 <> "" And sWhere2 <> "" Then
                        sSql += "   AND ((" + sWhere1 + ") OR (" + sWhere2 + ")) "
                    ElseIf sWhere1 <> "" Then
                        sSql += "   AND " + sWhere1
                    ElseIf sWhere2 <> "" Then
                        sSql += "   AND " + sWhere2
                    End If

                End If

                sSql += " GROUP BY c.comcd, f.comnmd"
                sSql += " ORDER BY c.comcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액입고 "
        Public Shared Function fnGet_BldProv(ByVal rsBldNm As String, Optional ByVal rsBldCd As String = "") As DataTable
            Dim sFn As String = "fnGet_BldProv(String, [String]) As DataTable"
            Dim sSql As String = ""

            Dim alParm As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       a.abo || a.rh abo_rh, a.dongbn,"
                sSql += "       fn_ack_date_str(a.availdt, 'yyyyy-mm-dd hh24:mi') availdt, a.donqnt, a.comcd,"
                sSql += "       fn_ack_date_str(a.indt,    'yyyyy-mm-dd hh24:mi') indt,"
                sSql += "       fn_ack_date_str(a.dondt,   'yyyyy-mm-dd hh24:mi') dondt,"
                sSql += "       DECODE(a.inplace, '0', '" + PRG_CONST.HOSPITAL_NAME + " 혈액은행') de_inplace,"
                sSql += "       b.comnmd, a.filter"
                sSql += "  FROM lb020m a, lf120m b"
                sSql += " WHERE a.comcd = b.comcd"
                sSql += "   and a.bldno = :bldno"

                alParm.Add(New OracleParameter("bldno", OracleDbType.Varchar2, rsBldNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldNm))

                If rsBldCd <> "" Then
                    sSql += "   AND b.bldcd = :bldcd"
                    alParm.Add(New OracleParameter("bldcd", OracleDbType.Varchar2, rsBldCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldCd))
                End If

                sSql += "  AND a.dondt >= b.usdt"
                sSql += "  AND a.dondt <  b.uedt"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 혈액원의 혈액코드와 매칭되는 병원에 성분제제코드 가져오기!!
        Public Shared Function fnGet_ComCd(ByVal rsBldCd As String) As DataTable
            Dim sFn As String = "Function fnGet_ComCd(String) As DataTable"

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "select distinct COMCD, AVAILMI, COMNMD, DONQNT, fn_ack_get_bldtype_plat_yn(COMCD) PLATYN" '20130821 정선영 수정
                sSql += "  from LF120M"
                sSql += " where BLDCD = :bldcd"
                sSql += "   and USDT <= fn_ack_sysdate"
                sSql += "   and UEDT >  fn_ack_sysdate"

                alParm.Add(New OracleParameter("bldcd", OracleDbType.Varchar2, rsBldCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

#Region " 혈액입출고현황 "
        Public Shared Function fn_InOutBldList(ByVal rsfDate As String, ByVal rstDate As String) As DataTable
            ' 성분제제 & 혈액형별 리스트
            Dim sFn As String = "Public Shared Function fn_InOutBldList(ByVal rsfDate As String, ByVal rstDate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT f.comcd, f.comnm,                                                        "
                sSql += "       SUM(CASE WHEN a.bldtype = 'A+'  THEN NVL(a.bldqty, 0) ELSE 0 END) as aip,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'B+'  THEN NVL(a.bldqty, 0) ELSE 0 END) as bip,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'O+'  THEN NVL(a.bldqty, 0) ELSE 0 END) as oip,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'AB+' THEN NVL(a.bldqty, 0) ELSE 0 END) as abip,  "
                sSql += "       SUM(CASE WHEN a.bldtype = 'A-'  THEN NVL(a.bldqty, 0) ELSE 0 END) as aim,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'B-'  THEN NVL(a.bldqty, 0) ELSE 0 END) as bim,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'O-'  THEN NVL(a.bldqty, 0) ELSE 0 END) as oim,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'AB-' THEN NVL(a.bldqty, 0) ELSE 0 END) as abim,  "
                sSql += "       SUM(CASE WHEN b.bldtype = 'A+'  THEN NVL(b.bldqty, 0) ELSE 0 END) as aop,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'B+'  THEN NVL(b.bldqty, 0) ELSE 0 END) as bop,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'O+'  THEN NVL(b.bldqty, 0) ELSE 0 END) as oop,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'AB+' THEN NVL(b.bldqty, 0) ELSE 0 END) as abop,  "
                sSql += "       SUM(CASE WHEN b.bldtype = 'A-'  THEN NVL(b.bldqty, 0) ELSE 0 END) as aom,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'B-'  THEN NVL(b.bldqty, 0) ELSE 0 END) as bom,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'O-'  THEN NVL(b.bldqty, 0) ELSE 0 END) as oom,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'AB-' THEN NVL(b.bldqty, 0) ELSE 0 END) as abom,  "
                sSql += "       SUM(NVL(a.bldqty, 0))                                          as sumiq, "
                sSql += "       SUM(NVL(b.bldqty, 0))                                          as sumoq "
                sSql += "  FROM ("
                sSql += "        SELECT a.comcd, a.abo || a.rh bldtype, MAX(f.comnmd) comnm "
                sSql += "          FROM lb020m a, lf120m f"
                sSql += "         WHERE a.indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND a.comcd = f.comcd"
                sSql += "         GROUP BY a.comcd, a.abo || a.rh"
                sSql += "         UNION "
                sSql += "        SELECT a.comcd, a.abo || a.rh bldtype, MAX(f.comnmd) comnm "
                'sSql += "          FROM lb020m a, lf120m f"
                'sSql += "         WHERE a.statedt BETWEEN :dates AND :datee || '235959'"
                'sSql += "           AND a.state   IN ('4', '6')"
                'sSql += "           AND a.comcd   = f.comcd"
                '<20140521 통계오류 추가
                sSql += "            FROM   (select b.comcd ,b.abo,b.rh "
                sSql += "                     from (SELECT   bldno, comcd_out comcd "
                sSql += "                             FROM   lb030m "
                sSql += "                            WHERE   outdt BETWEEN :dates AND :datee || '235959' "
                sSql += "                           UNION "
                sSql += "                           SELECT   b.bldno, b.comcd_out comcd "
                sSql += "                             FROM   lb043m a, lb031m b "
                sSql += "                            WHERE   b.outdt BETWEEN :dates AND :datee || '235959' "
                sSql += "                              AND b.rtnflg = '2' "
                sSql += "                              AND a.bldno = b.bldno "
                sSql += "                              AND a.comcd_out = b.comcd_out  "
                sSql += "                              AND a.tnsjubsuno = b.tnsjubsuno)a inner join lb020m b"
                sSql += "                                                              on b.bldno = a.bldno "
                sSql += "                                                             and b.comcd = a.comcd"
                sSql += "                                                             and b.state in ('4','6')) a, lf120m f "
                sSql += "           WHERE a.comcd   = f.comcd"

                sSql += "         GROUP BY a.comcd, a.abo || a.rh"
                sSql += "       ) f"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "       LEFT OUTER JOIN"
                sSql += "            ("
                sSql += "             SELECT comcd, abo || rh bldtype, count(*) bldqty FROM lb020m"
                sSql += "              WHERE indt BETWEEN :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY comcd, abo || rh"
                sSql += "            ) a  ON f.comcd = a.comcd AND f.bldtype = a.bldtype                                                                        "
                sSql += "       LEFT OUTER JOIN                                                               "
                sSql += "            ("
                sSql += "             SELECT b.comcd, b.abo || b.rh bldtype, count(*) bldqty"
                sSql += "               FROM (SELECT bldno, comcd_out FROM lb030m"
                sSql += "                      WHERE outdt  BETWEEN :dates AND :datee || '235959'"
                sSql += "                      UNION "
                sSql += "                     SELECT b.bldno, b.comcd_out"
                sSql += "                       FROM lb043m a, lb031m b"
                sSql += "                      WHERE b.outdt  BETWEEN :dates AND :datee || '235959'"
                sSql += "                        AND b.rtnflg     = '2'"
                sSql += "                        AND a.bldno      = b.bldno"
                sSql += "                        AND a.comcd_out  = b.comcd_out"
                sSql += "                        AND a.tnsjubsuno = b.tnsjubsuno"
                sSql += "                    ) a, lb020m b"
                sSql += "              WHERE a.comcd_out = b.comcd"
                sSql += "                AND a.bldno     = b.bldno"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY b.comcd, b.abo || b.rh"
                sSql += "            ) b ON f.comcd = b.comcd AND f.bldtype = b.bldtype"
                sSql += " WHERE NVL(a.bldqty, 0) + NVL(b.bldqty, 0) > 0"
                sSql += " GROUP BY f.comcd, f.comnm"
                sSql += " UNION ALL "
                sSql += "SELECT 'ZZZZZ' comcd, '  계 :' comnm,"
                sSql += "       SUM(CASE WHEN a.bldtype = 'A+'  THEN NVL(a.bldqty, 0) ELSE 0 END) as aip,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'B+'  THEN NVL(a.bldqty, 0) ELSE 0 END) as bip,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'O+'  THEN NVL(a.bldqty, 0) ELSE 0 END) as oip,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'AB+' THEN NVL(a.bldqty, 0) ELSE 0 END) as abip,  "
                sSql += "       SUM(CASE WHEN a.bldtype = 'A-'  THEN NVL(a.bldqty, 0) ELSE 0 END) as aim,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'B-'  THEN NVL(a.bldqty, 0) ELSE 0 END) as bim,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'O-'  THEN NVL(a.bldqty, 0) ELSE 0 END) as oim,   "
                sSql += "       SUM(CASE WHEN a.bldtype = 'AB-' THEN NVL(a.bldqty, 0) ELSE 0 END) as abim,  "
                sSql += "       SUM(CASE WHEN b.bldtype = 'A+'  THEN NVL(b.bldqty, 0) ELSE 0 END) as aop,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'B+'  THEN NVL(b.bldqty, 0) ELSE 0 END) as bop,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'O+'  THEN NVL(b.bldqty, 0) ELSE 0 END) as oop,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'AB+' THEN NVL(b.bldqty, 0) ELSE 0 END) as abop,  "
                sSql += "       SUM(CASE WHEN b.bldtype = 'A-'  THEN NVL(b.bldqty, 0) ELSE 0 END) as aom,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'B-'  THEN NVL(b.bldqty, 0) ELSE 0 END) as bom,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'O-'  THEN NVL(b.bldqty, 0) ELSE 0 END) as oom,   "
                sSql += "       SUM(CASE WHEN b.bldtype = 'AB-' THEN NVL(b.bldqty, 0) ELSE 0 END) as abom,  "
                sSql += "       SUM(NVL(a.bldqty, 0))                                          as sumiq, "
                sSql += "       SUM(NVL(b.bldqty, 0))                                          as sumoq "
                sSql += "  FROM (SELECT a.comcd, a.abo || a.rh bldtype, MAX(f.comnmd) comnm "
                sSql += "          FROM lb020m a, lf120m f"
                sSql += "         WHERE a.indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND a.comcd = f.comcd"
                sSql += "         GROUP BY a.comcd, a.abo || a.rh"
                sSql += "         UNION "
                sSql += "        SELECT a.comcd, a.abo || a.rh bldtype, MAX(f.comnmd) comnm "

                'sSql += "          FROM lb020m a, lf120m f"
                'sSql += "         WHERE a.statedt BETWEEN  :dates AND :datee || '235959'"
                'sSql += "           AND a.state   IN ('4', '6')"
                'sSql += "           AND a.comcd   = f.comcd"
                '<20140521 통계오류 추가
                sSql += "            FROM   (select b.comcd ,b.abo,b.rh "
                sSql += "                     from (SELECT   bldno, comcd_out comcd "
                sSql += "                             FROM   lb030m "
                sSql += "                            WHERE   outdt BETWEEN :dates AND :datee || '235959' "
                sSql += "                           UNION "
                sSql += "                           SELECT   b.bldno, b.comcd_out comcd "
                sSql += "                             FROM   lb043m a, lb031m b "
                sSql += "                            WHERE   b.outdt BETWEEN :dates AND :datee || '235959' "
                sSql += "                              AND b.rtnflg = '2' "
                sSql += "                              AND a.bldno = b.bldno "
                sSql += "                              AND a.comcd_out = b.comcd_out  "
                sSql += "                              AND a.tnsjubsuno = b.tnsjubsuno)a inner join lb020m b"
                sSql += "                                                              on b.bldno = a.bldno "
                sSql += "                                                             and b.comcd = a.comcd"
                sSql += "                                                             and b.state in ('4','6')) a, lf120m f "
                sSql += "           WHERE a.comcd   = f.comcd"

                sSql += "         GROUP BY a.comcd, a.abo || a.rh"
                sSql += "       ) f"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "       LEFT OUTER JOIN"
                sSql += "            (SELECT comcd, abo || rh bldtype, count(*) bldqty FROM lb020m"
                sSql += "              WHERE indt BETWEEN  :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY comcd, abo || rh"
                sSql += "            ) a  ON f.comcd = a.comcd AND f.bldtype = a.bldtype"
                sSql += "       LEFT OUTER JOIN  "
                sSql += "            ("
                sSql += "             SELECT b.comcd, b.abo || b.rh bldtype, count(*) bldqty"
                sSql += "               FROM (SELECT bldno, comcd_out FROM lb030m"
                sSql += "                      WHERE outdt  BETWEEN :dates AND :datee || '235959'"
                sSql += "                      UNION "
                sSql += "                     SELECT b.bldno, b.comcd_out"
                sSql += "                       FROM lb043m a, lb031m b"
                sSql += "                      WHERE b.outdt  BETWEEN :dates AND :datee || '235959'"
                sSql += "                        AND b.rtnflg     = '2'"
                sSql += "                        AND a.bldno      = b.bldno"
                sSql += "                        AND a.comcd_out  = b.comcd_out"
                sSql += "                        AND a.tnsjubsuno = b.tnsjubsuno"
                sSql += "                    ) a, lb020m b"
                sSql += "              WHERE a.comcd_out = b.comcd"
                sSql += "                AND a.bldno     = b.bldno"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY b.comcd, b.abo || b.rh"
                sSql += "            ) b ON f.comcd = b.comcd AND f.bldtype = b.bldtype"
                sSql += " WHERE NVL(a.bldqty, 0) + NVL(b.bldqty, 0) > 0"
                sSql += " ORDER BY comcd"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_InOutDetail1(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String) As DataTable
            ' 성분제제 & 혈액형별 리스트
            Dim sFn As String = "Public Shared Function fn_InOutDetail1(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "select '1' as sortorder                                                   "
                sSql += "     , '+' as tree1                                                       "
                sSql += "     , ''  as tree2                                                       "
                sSql += "     , '1' as tlevel                                                      "
                sSql += "     , '+' as tree_filter                                                "
                sSql += "     , '+' as tree_filter2                                                "
                sSql += "     , 'TREE_SUB' as subcode                                              "
                sSql += "     , ''         as subcode2                                             "
                sSql += "     , fn_ack_date_str('" + rsfDate + "', 'yyyy-mm-dd') || '~' ||           "
                sSql += "       fn_ack_date_str('" + rstDate + "', 'yyyy-mm-dd')    as period      "
                sSql += "     , CASE WHEN NVL(a.pinqty, 0) - NVL(b.poutqty, 0) >= 0          "
                sSql += "            THEN NVL(a.pinqty, 0) - NVL(b.poutqty, 0)               "
                sSql += "            ELSE 0                                                        "
                sSql += "       END  as fwdqty                                                     "
                sSql += "     , NVL(c.inqty, 0)                       as inqty                  "
                sSql += "     , NVL(d.outqty, 0)                      as outqty                 "
                sSql += "     , 0                                     as foutqty                   "
                sSql += "     , CASE WHEN NVL(a.pinqty, 0) - NVL(b.poutqty, 0) >= 0          "
                sSql += "            THEN NVL(a.pinqty, 0) - NVL(b.poutqty, 0)               "
                sSql += "            ELSE 0                                                        "
                sSql += "       END +                                                              "
                sSql += "       NVL(c.inqty, 0) - NVL(d.outqty, 0) as remainqty              "
                sSql += "  FROM (SELECT COUNT(bldno) as pinqty    /* 기간전 입고량 */              "
                sSql += "          FROM lb020m                                                     "
                sSql += "         WHERE indt  < :dates || '000000'                                        "
                sSql += "           AND comcd = :comcd                                                  "
                sSql += "       ) a,                                             "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       (SELECT COUNT(bldno) as poutqty    /* 기간전 출고량 */             "
                sSql += "          FROM lb020m                                                     "
                sSql += "         WHERE statedt < :datee || '000000'                                     "
                sSql += "           AND comcd   =  :comcd                                                      "

                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "           AND state in ('6', '4')                                        "
                sSql += "       ) b,                                                               "
                sSql += "       (SELECT COUNT(bldno) as inqty    /* 기간중 입고량 */               "
                sSql += "          from lb020m                                                     "
                sSql += "         WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd = :comcd                                                  "
                sSql += "       ) c,                                                               "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       (SELECT COUNT(bldno) as outqty   /* 기간중 출고량 */               "
                sSql += "          FROM lb020m                                                     "
                sSql += "         WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd   = :comcd                                                  "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "           AND state IN ('6', '4')                                        "
                sSql += "       ) d    "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_InOutDetail2(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String) As DataTable
            ' 성분제제 & 혈액형별 리스트
            Dim sFn As String = "Public Shared Function fn_InOutDetail2(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT '2'                     as sortorder                                "
                sSql += "     , ''                      as tree1                                    "
                sSql += "     , '+'                     as tree2                                    "
                sSql += "     , '2'                     as tlevel                                   "
                sSql += "     , '+' as tree_filter                                                  "
                sSql += "     , '+' as tree_filter2                                                 "
                sSql += "     , 'TREE_SUB' as subcode                                               "
                sSql += "     , a.inmonth               as subcode2                                 "
                sSql += "     , a.inmonth               as period                                   "
                sSql += "     , null                    as fwdqty                                   "
                sSql += "     , NVL(b.inqty, 0)      as inqty                                    "
                sSql += "     , NVL(c.outqty, 0)     as outqty                                   "
                sSql += "     , 0                       as foutqty                                  "
                sSql += "     , NVL(b.inqty, 0) - NVL(c.outqty, 0) +                          "
                sSql += "       NVL((SELECT COUNT(bldno)                                         "
                sSql += "                FROM lb020m                                                "
                sSql += "               WHERE fn_ack_date_str(indt, 'YYYY-MM') < a.inmonth          "
                sSql += "                 AND comcd = :comcd), 0)                                        "

                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       - NVL((SELECT COUNT(bldno)                                       "
                sSql += "                   FROM lb020m                                             "
                sSql += "                  WHERE fn_ack_date_str(statedt, 'YYYY-MM') < a.inmonth    "
                sSql += "                    AND comcd = :comcd                                         "

                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "                 AND state in ('6', '4')), 0) as remainqty                 "
                sSql += "  FROM (SELECT fn_ack_date_str(indt, 'YYYY-MM') as inmonth                 "
                sSql += "          FROM lb020m                                                      "
                sSql += "         WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd = :comcd"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM')                          "
                sSql += "        UNION                                                              "
                sSql += "       SELECT fn_ack_date_str(statedt, 'YYYY-MM') as inmonth               "
                sSql += "         FROM lb020m                                                       "
                sSql += "        WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd   = :comcd                                                    "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          AND state in ('6', '4')                                          "
                sSql += "       GROUP BY fn_ack_date_str(statedt, 'YYYY-MM')                        "
                sSql += "       ) a LEFT OUTER JOIN                                                                "
                sSql += "       (SELECT fn_ack_date_str(indt, 'YYYY-MM') as inmonth                 "
                sSql += "             , COUNT(bldno)             as inqty                           "
                sSql += "          FROM lb020m                                                      "
                sSql += "         WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd = :comcd"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM')                          "
                sSql += "       ) b ON a.inmonth = b.inmonth                                        "
                sSql += "       LEFT OUTER JOIN                                                     "
                sSql += "       (SELECT fn_ack_date_str(statedt, 'YYYY-MM') as inmonth              "
                sSql += "             , COUNT(bldno)             as outqty                          "
                sSql += "          FROM lb020m                                                      "
                sSql += "         WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd   = :comcd                                                   "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "           AND state in ('6', '4')                                         "
                sSql += "        GROUP BY fn_ack_date_str(statedt, 'YYYY-MM')                       "
                sSql += "       ) c ON a.inmonth = c.inmonth                                        "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_InOutDetail3(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String) As DataTable
            ' 성분제제 & 혈액형별 리스트
            Dim sFn As String = "Public Shared Function fn_InOutDetail3(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT '2'                     as sortorder                              "
                sSql += "     , ''                      as tree1                                  "
                sSql += "     , ''                      as tree2                                  "
                sSql += "     , '3'                     as tlevel                                 "
                sSql += "     , '' as tree_filter                                                 "
                sSql += "     , '' as tree_filter2                                                "
                sSql += "     , 'TREE_SUB' as subcode                                             "
                sSql += "     , SUBSTR(a.inday, 1, 7)  as subcode2                             "
                sSql += "     , a.inday                   as period                               "
                sSql += "     , NULL                      as fwdqty                               "
                sSql += "     , NVL(b.inqty, 0)        as inqty                                "
                sSql += "     , NVL(c.outqty, 0)       as outqty                               "
                sSql += "     , 0                        as foutqty                               "
                sSql += "     , NVL(b.inqty, 0) - NVL(c.outqty, 0)                          "
                sSql += "       + NVL((SELECT COUNT(bldno)                                     "
                sSql += "                   FROM lb020m                                           "
                sSql += "                  WHERE indt < a.inday                                   "
                sSql += "                    AND comcd = :comcd), 0)                             "
                sSql += "       - NVL((SELECT COUNT(bldno)                                     "
                sSql += "                   FROM lb020m                                           "
                sSql += "                  WHERE statedt < a.inday                                "
                sSql += "                    AND comcd = :comcd                                  "
                sSql += "                    AND state in ('6', '4')), 0) as remainqty            "
                sSql += "  FROM (SELECT fn_ack_date_str(indt, 'YYYY-MM-DD') as inday              "
                sSql += "          FROM lb020m                                                    "
                sSql += "         WHERE indt BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd = :comcd"

                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM-DD')                     "
                sSql += "        UNION                                                            "
                sSql += "       SELECT fn_ack_date_str(statedt, 'YYYY-MM-DD') as inday            "
                sSql += "         FROM lb020m                                                     "
                sSql += "        WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd   = :comcd"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          AND state in ('6', '4')                                        "
                sSql += "        GROUP BY fn_ack_date_str(statedt, 'YYYY-MM-DD')                  "
                sSql += "      ) a LEFT OUTER JOIN                                                "
                sSql += "      (SELECT fn_ack_date_str(indt, 'YYYY-MM-DD') as inday               "
                sSql += "            , COUNT(bldno)             as inqty                          "
                sSql += "         FROM lb020m                                                     "
                sSql += "        WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd = :comcd                                                  "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM-DD')"
                sSql += "      ) b ON a.inday = b.inday                                           "
                sSql += "      LEFT OUTER JOIN                                                    "
                sSql += "      (SELECT fn_ack_date_str(statedt, 'YYYY-MM-DD') as inday            "
                sSql += "            , COUNT(bldno)             as outqty                         "
                sSql += "         FROM lb020m                                                     "
                sSql += "        WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd = :comcd                                                  "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "           and state in ('6', '4')                                       "
                sSql += "        GROUP BY fn_ack_date_str(statedt, 'YYYY-MM-DD')                  "
                sSql += "       ) c ON a.inday = c.inday                                          "

                DbCommand()

                fn_InOutDetail3 = DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액 입고/출고 월별 현황 조회 "
        Public Shared Function fn_GetLastday(ByVal rsDate As String, Optional ByVal rsFormat As String = "") As DataTable
            ' 월의 마지막 날짜 불러오기
            Dim sFn As String = "Public Shared Function fn_GetLastday(ByVal rsDate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                If rsFormat <> "" Then
                    sSql += "SELECT TO_CHAR(LAST_DAY(TO_DATE(:date1,'yyyyMMdd')), 'yyyy-MM-dd') as lastday "
                Else
                    sSql += "SELECT TO_CHAR(LAST_DAY(TO_DATE(:date1,'yyyyMMdd')), 'yyyyMMdd') as lastday"
                End If

                sSql += "  FROM DUAL"

                alParm.Add(New OracleParameter("date1", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_InouTBldListM(ByVal rsMonth As String, ByVal rsInOutGbn As String) As DataTable
            ' 월별 입고 현황 조회
            Dim sFn As String = "Public Shared Function fn_InBldListM(ByVal rsDate As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                Dim sDateS As String = rsMonth + "01"
                Dim sDateE As String = Format(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate(rsMonth.Substring(0, 4) + "-" + rsMonth.Substring(4, 2) + "-01"))), "yyyyMMdd").ToString

                sSql += "SELECT a.aborh                                                                         "
                sSql += "     , a.comcd                                                                         "
                sSql += "     , a.comnmd                                                                        "
                sSql += "     , a.sortorder                                                                     "
                sSql += "     , CASE WHEN a.donqnt = '0' THEN '400ml' WHEN a.donqnt = '1' THEN '320ml' ELSE '미정' END as donqnt"
                sSql += "     , SUM(CASE WHEN a.day = '01' THEN 1 ELSE 0 END) as d1                             "
                sSql += "     , SUM(CASE WHEN a.day = '02' THEN 1 ELSE 0 END) as d2                             "
                sSql += "     , SUM(CASE WHEN a.day = '03' THEN 1 ELSE 0 END) as d3                             "
                sSql += "     , SUM(CASE WHEN a.day = '04' THEN 1 ELSE 0 END) as d4                             "
                sSql += "     , SUM(CASE WHEN a.day = '05' THEN 1 ELSE 0 END) as d5                             "
                sSql += "     , SUM(CASE WHEN a.day = '06' THEN 1 ELSE 0 END) as d6                             "
                sSql += "     , SUM(CASE WHEN a.day = '07' THEN 1 ELSE 0 END) as d7                             "
                sSql += "     , SUM(CASE WHEN a.day = '08' THEN 1 ELSE 0 END) as d8                             "
                sSql += "     , SUM(CASE WHEN a.day = '09' THEN 1 ELSE 0 END) as d9                             "
                sSql += "     , SUM(CASE WHEN a.day = '10' THEN 1 ELSE 0 END) as d10                            "
                sSql += "     , SUM(CASE WHEN a.day = '11' THEN 1 ELSE 0 END) as d11                            "
                sSql += "     , SUM(CASE WHEN a.day = '12' THEN 1 ELSE 0 END) as d12                            "
                sSql += "     , SUM(CASE WHEN a.day = '13' THEN 1 ELSE 0 END) as d13                            "
                sSql += "     , SUM(CASE WHEN a.day = '14' THEN 1 ELSE 0 END) as d14                            "
                sSql += "     , SUM(CASE WHEN a.day = '15' THEN 1 ELSE 0 END) as d15                            "
                sSql += "     , SUM(CASE WHEN a.day = '16' THEN 1 ELSE 0 END) as d16                            "
                sSql += "     , SUM(CASE WHEN a.day = '17' THEN 1 ELSE 0 END) as d17                            "
                sSql += "     , SUM(CASE WHEN a.day = '18' THEN 1 ELSE 0 END) as d18                            "
                sSql += "     , SUM(CASE WHEN a.day = '19' THEN 1 ELSE 0 END) as d19                            "
                sSql += "     , SUM(CASE WHEN a.day = '20' THEN 1 ELSE 0 END) as d20                            "
                sSql += "     , SUM(CASE WHEN a.day = '21' THEN 1 ELSE 0 END) as d21                            "
                sSql += "     , SUM(CASE WHEN a.day = '22' THEN 1 ELSE 0 END) as d22                            "
                sSql += "     , SUM(CASE WHEN a.day = '23' THEN 1 ELSE 0 END) as d23                            "
                sSql += "     , SUM(CASE WHEN a.day = '24' THEN 1 ELSE 0 END) as d24                            "
                sSql += "     , SUM(CASE WHEN a.day = '25' THEN 1 ELSE 0 END) as d25                            "
                sSql += "     , SUM(CASE WHEN a.day = '26' THEN 1 ELSE 0 END) as d26                            "
                sSql += "     , SUM(CASE WHEN a.day = '27' THEN 1 ELSE 0 END) as d27                            "
                sSql += "     , SUM(CASE WHEN a.day = '28' THEN 1 ELSE 0 END) as d28                            "
                sSql += "     , SUM(CASE WHEN a.day = '29' THEN 1 ELSE 0 END) as d29                            "
                sSql += "     , SUM(CASE WHEN a.day = '30' THEN 1 ELSE 0 END) as d30                            "
                sSql += "     , SUM(CASE WHEN a.day = '31' THEN 1 ELSE 0 END) as d31                            "
                sSql += "     , COUNT(a.day)                   as sumcnt                                        "
                sSql += "  FROM (SELECT a.abo || a.rh                                             as aborh       "
                sSql += "             , CASE WHEN a.abo || a.rh = 'A+'  THEN 1                                   "
                sSql += "                    WHEN a.abo || a.rh = 'A-'  THEN 2                                   "
                sSql += "                    WHEN a.abo || a.rh = 'B+'  THEN 3                                   "
                sSql += "                    WHEN a.abo || a.rh = 'B-'  THEN 4                                   "
                sSql += "                    WHEN a.abo || a.rh = 'O+'  THEN 5                                   "
                sSql += "                    WHEN a.abo || a.rh = 'O-'  THEN 6                                   "
                sSql += "                    WHEN a.abo || a.rh = 'AB+' THEN 7                                   "
                sSql += "                    ELSE 8"
                sSql += "               END                                                     as sortorder    "
                sSql += "             , b.comcd                                                                 "
                sSql += "             , b.comnmd                                                                "
                sSql += "             , a.donqnt                                                                "

                If rsInOutGbn = "I" Then
                    sSql += "             , SUBSTR(a.indt, 7, 2)                                 as day      "
                Else
                    sSql += "             , SUBSTR(a.statedt, 7, 2)                              as day      "
                End If
                sSql += "          FROM lb020m a,                                                               "
                sSql += "               lf120m b                                                                "
                sSql += "         WHERE a.comcd    = b.comcd                                                    "
                sSql += "           AND a.statedt >= b.usdt                                                     "
                sSql += "           AND a.statedt <  b.uedt                                                     "
                If rsInOutGbn = "I" Then
                    sSql += "           AND a.indt BETWEEN :dates  AND :datee || '235959'"
                Else
                    sSql += "           AND a.statedt BETWEEN :dates  AND :datee || '235959'"
                    sSql += "           AND a.state   IN ('3', '4', '6')                                        "
                End If

                sSql += "       ) a"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                sSql += " GROUP BY a.sortorder, a.aborh, a.sortorder, a.comcd, a.comnmd, a.donqnt              "
                sSql += " UNION ALL                                                                            "
                sSql += "SELECT ''                                                                             "
                sSql += "     , ''     as comcd                                                                "
                sSql += "     , '계'                                                                           "
                sSql += "     , 9                                                                              "
                sSql += "     , ''                                                                             "
                sSql += "     , SUM(CASE WHEN a.day = '01' THEN 1 ELSE 0 END) as d1                            "
                sSql += "     , SUM(CASE WHEN a.day = '02' THEN 1 ELSE 0 END) as d2                            "
                sSql += "     , SUM(CASE WHEN a.day = '03' THEN 1 ELSE 0 END) as d3                            "
                sSql += "     , SUM(CASE WHEN a.day = '04' THEN 1 ELSE 0 END) as d4                            "
                sSql += "     , SUM(CASE WHEN a.day = '05' THEN 1 ELSE 0 END) as d5                            "
                sSql += "     , SUM(CASE WHEN a.day = '06' THEN 1 ELSE 0 END) as d6                            "
                sSql += "     , SUM(CASE WHEN a.day = '07' THEN 1 ELSE 0 END) as d7                            "
                sSql += "     , SUM(CASE WHEN a.day = '08' THEN 1 ELSE 0 END) as d8                            "
                sSql += "     , SUM(CASE WHEN a.day = '09' THEN 1 ELSE 0 END) as d9                            "
                sSql += "     , SUM(CASE WHEN a.day = '10' THEN 1 ELSE 0 END) as d10                           "
                sSql += "     , SUM(CASE WHEN a.day = '11' THEN 1 ELSE 0 END) as d11                           "
                sSql += "     , SUM(CASE WHEN a.day = '12' THEN 1 ELSE 0 END) as d12                           "
                sSql += "     , SUM(CASE WHEN a.day = '13' THEN 1 ELSE 0 END) as d13                           "
                sSql += "     , SUM(CASE WHEN a.day = '14' THEN 1 ELSE 0 END) as d14                           "
                sSql += "     , SUM(CASE WHEN a.day = '15' THEN 1 ELSE 0 END) as d15                           "
                sSql += "     , SUM(CASE WHEN a.day = '16' THEN 1 ELSE 0 END) as d16                           "
                sSql += "     , SUM(CASE WHEN a.day = '17' THEN 1 ELSE 0 END) as d17                           "
                sSql += "     , SUM(CASE WHEN a.day = '18' THEN 1 ELSE 0 END) as d18                           "
                sSql += "     , SUM(CASE WHEN a.day = '19' THEN 1 ELSE 0 END) as d19                           "
                sSql += "     , SUM(CASE WHEN a.day = '20' THEN 1 ELSE 0 END) as d20                           "
                sSql += "     , SUM(CASE WHEN a.day = '21' THEN 1 ELSE 0 END) as d21                           "
                sSql += "     , SUM(CASE WHEN a.day = '22' THEN 1 ELSE 0 END) as d22                           "
                sSql += "     , SUM(CASE WHEN a.day = '23' THEN 1 ELSE 0 END) as d23                           "
                sSql += "     , SUM(CASE WHEN a.day = '24' THEN 1 ELSE 0 END) as d24                           "
                sSql += "     , SUM(CASE WHEN a.day = '25' THEN 1 ELSE 0 END) as d25                           "
                sSql += "     , SUM(CASE WHEN a.day = '26' THEN 1 ELSE 0 END) as d26                           "
                sSql += "     , SUM(CASE WHEN a.day = '27' THEN 1 ELSE 0 END) as d27                           "
                sSql += "     , SUM(CASE WHEN a.day = '28' THEN 1 ELSE 0 END) as d28                           "
                sSql += "     , SUM(CASE WHEN a.day = '29' THEN 1 ELSE 0 END) as d29                           "
                sSql += "     , SUM(CASE WHEN a.day = '30' THEN 1 ELSE 0 END) as d30                           "
                sSql += "     , SUM(CASE WHEN a.day = '31' THEN 1 ELSE 0 END) as d31                           "
                sSql += "     , COUNT(a.day)                                  as sumcnt                        "
                sSql += "  FROM ("
                If rsInOutGbn = "I" Then
                    sSql += "        SELECT SUBSTR(a.indt, 7, 2)           as day                           "
                Else
                    sSql += "        SELECT SUBSTR(a.statedt, 7, 2)        as day                           "
                End If
                sSql += "          FROM lb020m a, lf120m b                                                               "
                sSql += "         WHERE a.comcd = b.comcd                                                      "
                sSql += "           AND a.statedt >= b.usdt                                                      "
                sSql += "           AND a.statedt <  b.uedt                                                      "
                If rsInOutGbn = "I" Then
                    sSql += "           AND a.indt BETWEEN :dates  AND :datee || '235959'"
                Else
                    sSql += "           AND a.statedt BETWEEN :dates  AND :datee || '235959'"
                    sSql += "           AND a.state   IN ('3', '4', '6')                                        "
                End If
                sSql += "       ) a  "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                sSql += " ORDER BY sortorder, comcd                                                              "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_OutBldListM(ByVal rsMonth As String) As DataTable
            ' 월별 출고 현황 조회
            Dim sFn As String = "Public Shared Function fn_OutBldListM(ByVal rsDate As String, ByVal rsIOGbn As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                Dim sDateS As String = rsMonth + "01"
                Dim sDateE As String = Format(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate(rsMonth.Substring(0, 4) + "-" + rsMonth.Substring(4, 2) + "-01"))), "yyyyMMdd").ToString

                sSql += "SELECT a.aborh                                                                         "
                sSql += "     , a.comcd                                                                         "
                sSql += "     , a.comnmd                                                                        "
                sSql += "     , a.sortorder                                                                     "
                sSql += "     , CASE WHEN a.donqnt = '0' THEN '400ml' WHEN a.donqnt = '1' THEN '320ml' ELSE '미정' END as donqnt"
                sSql += "     , SUM(CASE WHEN a.day = '01' THEN 1 ELSE 0 END) as d1                             "
                sSql += "     , SUM(CASE WHEN a.day = '02' THEN 1 ELSE 0 END) as d2                             "
                sSql += "     , SUM(CASE WHEN a.day = '03' THEN 1 ELSE 0 END) as d3                             "
                sSql += "     , SUM(CASE WHEN a.day = '04' THEN 1 ELSE 0 END) as d4                             "
                sSql += "     , SUM(CASE WHEN a.day = '05' THEN 1 ELSE 0 END) as d5                             "
                sSql += "     , SUM(CASE WHEN a.day = '06' THEN 1 ELSE 0 END) as d6                             "
                sSql += "     , SUM(CASE WHEN a.day = '07' THEN 1 ELSE 0 END) as d7                             "
                sSql += "     , SUM(CASE WHEN a.day = '08' THEN 1 ELSE 0 END) as d8                             "
                sSql += "     , SUM(CASE WHEN a.day = '09' THEN 1 ELSE 0 END) as d9                             "
                sSql += "     , SUM(CASE WHEN a.day = '10' THEN 1 ELSE 0 END) as d10                            "
                sSql += "     , SUM(CASE WHEN a.day = '11' THEN 1 ELSE 0 END) as d11                            "
                sSql += "     , SUM(CASE WHEN a.day = '12' THEN 1 ELSE 0 END) as d12                            "
                sSql += "     , SUM(CASE WHEN a.day = '13' THEN 1 ELSE 0 END) as d13                            "
                sSql += "     , SUM(CASE WHEN a.day = '14' THEN 1 ELSE 0 END) as d14                            "
                sSql += "     , SUM(CASE WHEN a.day = '15' THEN 1 ELSE 0 END) as d15                            "
                sSql += "     , SUM(CASE WHEN a.day = '16' THEN 1 ELSE 0 END) as d16                            "
                sSql += "     , SUM(CASE WHEN a.day = '17' THEN 1 ELSE 0 END) as d17                            "
                sSql += "     , SUM(CASE WHEN a.day = '18' THEN 1 ELSE 0 END) as d18                            "
                sSql += "     , SUM(CASE WHEN a.day = '19' THEN 1 ELSE 0 END) as d19                            "
                sSql += "     , SUM(CASE WHEN a.day = '20' THEN 1 ELSE 0 END) as d20                            "
                sSql += "     , SUM(CASE WHEN a.day = '21' THEN 1 ELSE 0 END) as d21                            "
                sSql += "     , SUM(CASE WHEN a.day = '22' THEN 1 ELSE 0 END) as d22                            "
                sSql += "     , SUM(CASE WHEN a.day = '23' THEN 1 ELSE 0 END) as d23                            "
                sSql += "     , SUM(CASE WHEN a.day = '24' THEN 1 ELSE 0 END) as d24                            "
                sSql += "     , SUM(CASE WHEN a.day = '25' THEN 1 ELSE 0 END) as d25                            "
                sSql += "     , SUM(CASE WHEN a.day = '26' THEN 1 ELSE 0 END) as d26                            "
                sSql += "     , SUM(CASE WHEN a.day = '27' THEN 1 ELSE 0 END) as d27                            "
                sSql += "     , SUM(CASE WHEN a.day = '28' THEN 1 ELSE 0 END) as d28                            "
                sSql += "     , SUM(CASE WHEN a.day = '29' THEN 1 ELSE 0 END) as d29                            "
                sSql += "     , SUM(CASE WHEN a.day = '30' THEN 1 ELSE 0 END) as d30                            "
                sSql += "     , SUM(CASE WHEN a.day = '31' THEN 1 ELSE 0 END) as d31                            "
                sSql += "     , COUNT(a.day)                   as sumcnt                                        "
                sSql += "  FROM (SELECT a.abo || a.rh                                             as aborh       "
                sSql += "             , CASE WHEN a.abo || a.rh = 'A+'  THEN 1                                   "
                sSql += "                    WHEN a.abo || a.rh = 'A-'  THEN 2                                   "
                sSql += "                    WHEN a.abo || a.rh = 'B+'  THEN 3                                   "
                sSql += "                    WHEN a.abo || a.rh = 'B-'  THEN 4                                   "
                sSql += "                    WHEN a.abo || a.rh = 'O+'  THEN 5                                   "
                sSql += "                    WHEN a.abo || a.rh = 'O-'  THEN 6                                   "
                sSql += "                    WHEN a.abo || a.rh = 'AB+' THEN 7                                   "
                sSql += "                    ELSE 8"
                sSql += "               END                                                     as sortorder    "
                sSql += "             , b.comcd                                                                 "
                sSql += "             , b.comnmd                                                                "
                sSql += "             , a.donqnt                                                                "
                sSql += "             , SUBSTR(c.outdt, 7, 2)                              as day      "
                sSql += "          FROM lb020m a, lf120m b,"
                sSql += "               (SELECT comcd_out, bldno, outdt FROM lb030m"
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'"
                sSql += "                 UNION "
                sSql += "                SELECT comcd_out, bldno, outdt FROM lb031m"
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'"
                sSql += "               ) c"
                sSql += "         WHERE a.comcd    = b.comcd                                                    "
                sSql += "           AND a.indt    >= b.usdt                                                     "
                sSql += "           AND a.indt    <  b.uedt                                                     "
                sSql += "           AND a.bldno    = c.bldno"
                sSql += "           AND a.comcd    = c.comcd_out"
                sSql += "       ) a                                          "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                sSql += " GROUP BY a.sortorder, a.aborh, a.sortorder, a.comcd, a.comnmd, a.donqnt              "
                sSql += " UNION ALL                                                                            "
                sSql += "SELECT ''                                                                             "
                sSql += "     , ''     as comcd                                                                "
                sSql += "     , '계'                                                                           "
                sSql += "     , 9                                                                              "
                sSql += "     , ''                                                                             "
                sSql += "     , SUM(CASE WHEN a.day = '01' THEN 1 ELSE 0 END) as d1                            "
                sSql += "     , SUM(CASE WHEN a.day = '02' THEN 1 ELSE 0 END) as d2                            "
                sSql += "     , SUM(CASE WHEN a.day = '03' THEN 1 ELSE 0 END) as d3                            "
                sSql += "     , SUM(CASE WHEN a.day = '04' THEN 1 ELSE 0 END) as d4                            "
                sSql += "     , SUM(CASE WHEN a.day = '05' THEN 1 ELSE 0 END) as d5                            "
                sSql += "     , SUM(CASE WHEN a.day = '06' THEN 1 ELSE 0 END) as d6                            "
                sSql += "     , SUM(CASE WHEN a.day = '07' THEN 1 ELSE 0 END) as d7                            "
                sSql += "     , SUM(CASE WHEN a.day = '08' THEN 1 ELSE 0 END) as d8                            "
                sSql += "     , SUM(CASE WHEN a.day = '09' THEN 1 ELSE 0 END) as d9                            "
                sSql += "     , SUM(CASE WHEN a.day = '10' THEN 1 ELSE 0 END) as d10                           "
                sSql += "     , SUM(CASE WHEN a.day = '11' THEN 1 ELSE 0 END) as d11                           "
                sSql += "     , SUM(CASE WHEN a.day = '12' THEN 1 ELSE 0 END) as d12                           "
                sSql += "     , SUM(CASE WHEN a.day = '13' THEN 1 ELSE 0 END) as d13                           "
                sSql += "     , SUM(CASE WHEN a.day = '14' THEN 1 ELSE 0 END) as d14                           "
                sSql += "     , SUM(CASE WHEN a.day = '15' THEN 1 ELSE 0 END) as d15                           "
                sSql += "     , SUM(CASE WHEN a.day = '16' THEN 1 ELSE 0 END) as d16                           "
                sSql += "     , SUM(CASE WHEN a.day = '17' THEN 1 ELSE 0 END) as d17                           "
                sSql += "     , SUM(CASE WHEN a.day = '18' THEN 1 ELSE 0 END) as d18                           "
                sSql += "     , SUM(CASE WHEN a.day = '19' THEN 1 ELSE 0 END) as d19                           "
                sSql += "     , SUM(CASE WHEN a.day = '20' THEN 1 ELSE 0 END) as d20                           "
                sSql += "     , SUM(CASE WHEN a.day = '21' THEN 1 ELSE 0 END) as d21                           "
                sSql += "     , SUM(CASE WHEN a.day = '22' THEN 1 ELSE 0 END) as d22                           "
                sSql += "     , SUM(CASE WHEN a.day = '23' THEN 1 ELSE 0 END) as d23                           "
                sSql += "     , SUM(CASE WHEN a.day = '24' THEN 1 ELSE 0 END) as d24                           "
                sSql += "     , SUM(CASE WHEN a.day = '25' THEN 1 ELSE 0 END) as d25                           "
                sSql += "     , SUM(CASE WHEN a.day = '26' THEN 1 ELSE 0 END) as d26                           "
                sSql += "     , SUM(CASE WHEN a.day = '27' THEN 1 ELSE 0 END) as d27                           "
                sSql += "     , SUM(CASE WHEN a.day = '28' THEN 1 ELSE 0 END) as d28                           "
                sSql += "     , SUM(CASE WHEN a.day = '29' THEN 1 ELSE 0 END) as d29                           "
                sSql += "     , SUM(CASE WHEN a.day = '30' THEN 1 ELSE 0 END) as d30                           "
                sSql += "     , SUM(CASE WHEN a.day = '31' THEN 1 ELSE 0 END) as d31                           "
                sSql += "     , COUNT(a.day)                                  as sumcnt                        "
                sSql += "  FROM (SELECT SUBSTR(c.outdt, 7, 2)           as day                           "
                sSql += "          FROM lb020m a, lf120m b,                                                               "
                sSql += "               (SELECT comcd_out, bldno, outdt FROM lb030m"
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'"
                sSql += "                 UNION "
                sSql += "                SELECT comcd_out, bldno, outdt FROM lb031m"
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'"
                sSql += "               ) c"
                sSql += "         WHERE a.comcd    = b.comcd                                                      "
                sSql += "           AND a.indt    >= b.usdt                                                      "
                sSql += "           AND a.indt    <  b.uedt  "
                sSql += "           AND a.bldno    = c.bldno"
                sSql += "           AND a.comcd    = c.comcd_out"
                sSql += "       ) a"

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                sSql += " ORDER BY sortorder, comcd                                                              "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_InOutDtailM(ByVal rsMonth As String, ByVal rsGbn As String, ByVal rsComcd As String, ByVal rsAbo As String, ByVal rsRh As String) As DataTable
            ' 월의 마지막 날짜 불러오기
            Dim sFn As String = "Public Shared Function fn_InOutDtailM(ByVal rsDate As String, ByVal rsGbn As String, ByVal rsComcd As String, ByVal rsAbo As String, ByVal rsrh As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Dim sDateS As String = rsMonth + "01"
            Dim sDateE As String = Format(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate(rsMonth.Substring(0, 4) + "-" + rsMonth.Substring(4, 2) + "-01"))), "yyyyMMdd").ToString

            Try
                If rsGbn = "I"c Then
                    sSql += "SELECT '입고'                                       as gbn                   "
                    sSql += "     , b.comnmd                                                                  "
                    sSql += "     , a.abo || a.rh                                as aborh                 "
                    sSql += "     , fn_ack_get_bldno_full(a.bldno)                   as vbldno                "
                    sSql += "     , fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi')    as indt                  "
                    sSql += "     , fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') as availdt               "
                    sSql += "     , ''                                           as testdt                "
                    sSql += "     , ''                                           as outdt                 "
                    sSql += "     , ''                                           as rtndt                 "
                    sSql += "  FROM lb020m a                                                                  "
                    sSql += "     , lf120m b                                                                  "
                    sSql += " WHERE a.indt BETWEEN :dates AND :datee || '235959'                                                    "

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                    sSql += "   AND a.comcd    = b.comcd                                                     "
                    sSql += "   AND a.statedt >= b.usdt                                                      "
                    sSql += "   AND a.statedt <  b.uedt                                                      "
                    sSql += "   AND a.comcd    = :comcd                                                           "
                    sSql += "   AND a.abo      = :abo                                                           "
                    sSql += "   AND a.rh       = :rh                                                           "

                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    alParm.Add(New OracleParameter("abo", OracleDbType.Varchar2, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                    alParm.Add(New OracleParameter("rh", OracleDbType.Varchar2, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))

                    sSql += "ORDER BY a.statedt                                                              "
                Else
                    sSql += "SELECT CASE WHEN a.state = '2' THEN '검사중' WHEN a.state = '3' THEN '가출고'   "
                    sSql += "            WHEN a.state = '4' THEN '출고'   WHEN a.state = '6' THEN '폐기'     "
                    sSql += "            ELSE a.state                                                        "
                    sSql += "       END                                              as gbn                  "
                    sSql += "     , d.comnmd                                                                 "
                    sSql += "     , a.abo || a.rh                                    as aborh                "
                    sSql += "     , fn_ack_get_bldno_full(a.bldno)                   as vbldno               "
                    sSql += "     , fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi')    as indt                 "
                    sSql += "     , fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') as availdt              "
                    sSql += "     , fn_ack_date_str(b.testdt, 'yyyy-MM-dd hh24:mi')  as testdt               "
                    sSql += "     , fn_ack_date_str(b.outdt, 'yyyy-MM-dd hh24:mi')   as outdt                "
                    sSql += "     , fn_ack_date_str(c.rtndt, 'yyyy-MM-dd hh24:mi')   as rtndt                "
                    sSql += "  FROM lb020m a INNER JOIN                                                      "
                    sSql += "       lf120m d ON (a.comcd = d.comcd)                                          "
                    sSql += "       LEFT OUTER JOIN                                                          "
                    sSql += "            lb030m b ON (a.bldno = b.bldno AND a.comcd = b.comcd_out)           "
                    sSql += "       LEFT OUTER JOIN                                                          "
                    sSql += "            lb031m c ON (a.bldno = c.bldno AND a.comcd = c.comcd_out)           "
                    sSql += " WHERE a.statedt BETWEEN :dates AND :datee || '235959'                                                "

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                    sSql += "   AND a.state   IN ('3', '4', '6')                                             "
                    sSql += "   AND a.statedt >= d.usdt                                                      "
                    sSql += "   AND a.statedt <  d.uedt                                                      "
                    sSql += "   AND a.comcd    = :comcd                                                           "
                    sSql += "   AND a.abo      = :abo                                                           "
                    sSql += "   AND a.rh       = :rh                                                           "

                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    alParm.Add(New OracleParameter("abo", OracleDbType.Varchar2, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                    alParm.Add(New OracleParameter("rh", OracleDbType.Varchar2, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))

                    sSql += "ORDER BY a.statedt                                                              "
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액 반납/폐기 리스트 조회 "
        Public Shared Function fn_rtnSearchList(ByVal rsfDate As String, ByVal rstDate As String, Optional ByVal rsGbn As String = "") As DataTable
            ' 혈액 반납/폐기 리스트
            Dim sFn As String = "Public Shared Function fn_rtnList(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT CASE WHEN a.keepgbn = '3' THEN '반납'"
                sSql += "            WHEN a.keepgbn = '4' THEN '폐기' "
                sSql += "            WHEN a.keepgbn = '5' THEN '자체폐기'"
                sSql += "            WHEN a.keepgbn = '6' THEN '교환'"
                sSql += "       END                                          as gbn      "
                sSql += "     , fn_ack_date_str(a.rtndt, 'YYYY-MM-DD')           as rtndt    "
                sSql += "     , b.regno                                                      "
                sSql += "     , b.patnm                                                      "
                sSql += "     , b.sex || '/' || b.age                        as sexage   "
                sSql += "     , fn_ack_date_str(b.orddt, 'YYYY-MM-DD')           as orddt    "
                sSql += "     , fn_ack_get_ward_abbr(b.wardno)               as wardno                                                    "
                sSql += "     , fn_ack_get_dept_abbr(b.iogbn, b.deptcd)          as deptnm   "
                sSql += "     , fn_ack_get_dr_name(b.doctorcd)               as doctornm "
                sSql += "     , c.abo || c.rh                                as aborh    "
                sSql += "     , d.comnmd                                                     "
                sSql += "     , fn_ack_get_bldno_full(a.bldno)                   as vbldno   "
                sSql += "     , a.rtnrsncd                                                   "
                sSql += "     , a.rtnrsncmt                                                  "
                sSql += "  FROM lb020m c,                                                    "
                sSql += "       lf120m d,                                                    "
                sSql += "       lb031m a,"
                sSql += "       lb040m b"
                sSql += " WHERE a.rtndt BETWEEN :dates  AND :datee || '235959'                "
                sSql += "   AND a.bldno      = c.bldno                                       "
                sSql += "   AND a.comcd_out  = c.comcd                                       "
                sSql += "   AND a.comcd_out  = d.comcd                                       "
                sSql += "   AND c.indt      >= d.usdt                                        "
                sSql += "   AND c.indt      <  d.uedt    "
                sSql += "   AND a.tnsjubsuno = b.tnsjubsuno (+)                               "

                alParm.Add(New OracleParameter("dates", rsfDate))
                alParm.Add(New OracleParameter("datee", rstDate))

                If rsGbn <> "" Then
                    sSql += "   AND a.keepgbn IN ( " + rsGbn + " )                          "
                ElseIf rsGbn = "" Then
                    sSql += "   AND a.keepgbn IN ('3','4','5','6' )                          "
                End If

                sSql += "ORDER BY a.keepgbn, a.rtndt, b.regno                              "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_rtnqnt(ByVal rsfDate As String, ByVal rstDate As String, Optional ByVal rsGbn As String = "") As DataTable
            ' 혈액 반납/폐기 량
            Dim sFn As String = "Public Shared Function fn_rtnList(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT SUM(CASE WHEN keepgbn = '3' THEN 1 ELSE 0 END) as rtncnt      "
                sSql += "     , SUM(CASE WHEN keepgbn = '4' THEN 1 ELSE 0 END) as discnt      "
                sSql += "     , SUM(CASE WHEN keepgbn = '5' THEN 1 ELSE 0 END) as selfcnt     "
                sSql += "     , SUM(CASE WHEN keepgbn = '6' THEN 1 ELSE 0 END) as exccnt      "
                sSql += "  FROM lb031m                                                        "
                sSql += " WHERE rtndt BETWEEN :dates AND :datee || '235959'                                         "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsGbn <> "" Then
                    sSql += "   AND keepgbn IN ( " + rsGbn + " )                              "
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액 반납/폐기 건수 조회 "
        Public Shared Function fn_RtnCntSearchDay(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable
            ' 혈액 반납/폐기 건수 조회
            Dim sFn As String = "Public Shared Function fn_RtnCntSearchDay(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT b.comnmd                                                                         "
                sSql += "     , (SELECT COUNT(comcd) as qty                                                      "
                sSql += "          FROM lb020m                                                                   "
                sSql += "         WHERE statedt BETWEEN :dates AND :datee || '235959'                                                  "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "           AND state in ('4', '6')                                                      "
                sSql += "           AND comcd = a.comcd_out) as suma                                             "
                sSql += "     , SUM(a.qty) as sumt                                                               "
                sSql += "     , SUM(CASE WHEN a.days = '01' THEN a.qty ELSE 0 END) as d1                         "
                sSql += "     , SUM(CASE WHEN a.days = '02' THEN a.qty ELSE 0 END) as d2                         "
                sSql += "     , SUM(CASE WHEN a.days = '03' THEN a.qty ELSE 0 END) as d3                         "
                sSql += "     , SUM(CASE WHEN a.days = '04' THEN a.qty ELSE 0 END) as d4                         "
                sSql += "     , SUM(CASE WHEN a.days = '05' THEN a.qty ELSE 0 END) as d5                         "
                sSql += "     , SUM(CASE WHEN a.days = '06' THEN a.qty ELSE 0 END) as d6                         "
                sSql += "     , SUM(CASE WHEN a.days = '07' THEN a.qty ELSE 0 END) as d7                         "
                sSql += "     , SUM(CASE WHEN a.days = '08' THEN a.qty ELSE 0 END) as d8                         "
                sSql += "     , SUM(CASE WHEN a.days = '09' THEN a.qty ELSE 0 END) as d9                         "
                sSql += "     , SUM(CASE WHEN a.days = '10' THEN a.qty ELSE 0 END) as d10                        "
                sSql += "     , SUM(CASE WHEN a.days = '11' THEN a.qty ELSE 0 END) as d11                        "
                sSql += "     , SUM(CASE WHEN a.days = '12' THEN a.qty ELSE 0 END) as d12                        "
                sSql += "     , SUM(CASE WHEN a.days = '13' THEN a.qty ELSE 0 END) as d13                        "
                sSql += "     , SUM(CASE WHEN a.days = '14' THEN a.qty ELSE 0 END) as d14                        "
                sSql += "     , SUM(CASE WHEN a.days = '15' THEN a.qty ELSE 0 END) as d15                        "
                sSql += "     , SUM(CASE WHEN a.days = '16' THEN a.qty ELSE 0 END) as d16                        "
                sSql += "     , SUM(CASE WHEN a.days = '17' THEN a.qty ELSE 0 END) as d17                        "
                sSql += "     , SUM(CASE WHEN a.days = '18' THEN a.qty ELSE 0 END) as d18                        "
                sSql += "     , SUM(CASE WHEN a.days = '19' THEN a.qty ELSE 0 END) as d19                        "
                sSql += "     , SUM(CASE WHEN a.days = '20' THEN a.qty ELSE 0 END) as d20                        "
                sSql += "     , SUM(CASE WHEN a.days = '21' THEN a.qty ELSE 0 END) as d21                        "
                sSql += "     , SUM(CASE WHEN a.days = '22' THEN a.qty ELSE 0 END) as d22                        "
                sSql += "     , SUM(CASE WHEN a.days = '23' THEN a.qty ELSE 0 END) as d23                        "
                sSql += "     , SUM(CASE WHEN a.days = '24' THEN a.qty ELSE 0 END) as d24                        "
                sSql += "     , SUM(CASE WHEN a.days = '25' THEN a.qty ELSE 0 END) as d25                        "
                sSql += "     , SUM(CASE WHEN a.days = '26' THEN a.qty ELSE 0 END) as d26                        "
                sSql += "     , SUM(CASE WHEN a.days = '27' THEN a.qty ELSE 0 END) as d27                        "
                sSql += "     , SUM(CASE WHEN a.days = '28' THEN a.qty ELSE 0 END) as d28                        "
                sSql += "     , SUM(CASE WHEN a.days = '29' THEN a.qty ELSE 0 END) as d29                        "
                sSql += "     , SUM(CASE WHEN a.days = '30' THEN a.qty ELSE 0 END) as d30                        "
                sSql += "     , SUM(CASE WHEN a.days = '31' THEN a.qty ELSE 0 END) as d31                        "
                sSql += "  FROM (SELECT comcd_out                                                          "
                sSql += "             , fn_ack_date_str(rtndt, 'DD') as days                                     "
                sSql += "             , COUNT(comcd) as qty                                                      "
                sSql += "          FROM lb031m                                                                   "
                sSql += "         WHERE rtndt BETWEEN :dates AND :datee || '235959'                                                    "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsGbn = "R"c Then
                    sSql += "           AND keepgbn = '3'                                                        "
                Else
                    sSql += "           AND keepgbn in ('4', '5')                                                "
                End If

                sSql += "           AND regdt in (SELECT MAX(regdt)                                              "
                sSql += "                           FROM lb031m                                                  "
                sSql += "                          WHERE rtndt BETWEEN :dates AND :datee || '235959'                                         "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsGbn = "R"c Then
                    sSql += "                            AND keepgbn = '3'                                       "
                Else
                    sSql += "                            AND keepgbn IN ('4', '5')                               "
                End If

                sSql += "                         GROUP BY bldno, comcd_out)                                     "
                sSql += "        GROUP BY comcd_out, fn_ack_date_str(rtndt, 'DD') ) a                                "
                sSql += "     , lf120m b                                                                         "
                sSql += " WHERE a.comcd_out = b.comcd                                                            "
                sSql += "   AND b.usdt <= :dates                                                                      "
                sSql += "   AND b.uedt >  :datee || '235959'                                                                      "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += " GROUP BY a.comcd_out, b.comnmd                                                         "
                sSql += " ORDER BY a.comcd_out                                                                   "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_RtnCntSearchMonth(ByVal rsfMonth As String, ByVal rstMonth As String, ByVal rsGbn As String) As DataTable
            ' 혈액 반납/폐기 건수 조회
            Dim sFn As String = "Public Shared Function fn_RtnCntSearchDay(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                Dim sTMonth As String = Format(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate(rstMonth.Insert(4, "-") + "-01"))), "yyyyMMdd").ToString

                sSql += "SELECT b.comnmd                                                                                          "
                sSql += "     , (SELECT COUNT(comcd) as qty                                                                       "
                sSql += "          FROM lb020m                                                                                    "
                sSql += "         WHERE statedt BETWEEN :dates AND :datee || '235959'                                                                 "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

                sSql += "           AND state in ('4', '6')                                                                       "
                sSql += "           AND comcd = a.comcd_out ) as suma                                                             "
                sSql += "     , SUM(a.qty) as sumt                                                                                "
                sSql += "     , SUM(CASE WHEN a.months = '01' THEN a.qty ELSE 0 END) as m1                                        "
                sSql += "     , SUM(CASE WHEN a.months = '02' THEN a.qty ELSE 0 END) as m2                                        "
                sSql += "     , SUM(CASE WHEN a.months = '03' THEN a.qty ELSE 0 END) as m3                                        "
                sSql += "     , SUM(CASE WHEN a.months = '04' THEN a.qty ELSE 0 END) as m4                                        "
                sSql += "     , SUM(CASE WHEN a.months = '05' THEN a.qty ELSE 0 END) as m5                                        "
                sSql += "     , SUM(CASE WHEN a.months = '06' THEN a.qty ELSE 0 END) as m6                                        "
                sSql += "     , SUM(CASE WHEN a.months = '07' THEN a.qty ELSE 0 END) as m7                                        "
                sSql += "     , SUM(CASE WHEN a.months = '08' THEN a.qty ELSE 0 END) as m8                                        "
                sSql += "     , SUM(CASE WHEN a.months = '09' THEN a.qty ELSE 0 END) as m9                                        "
                sSql += "     , SUM(CASE WHEN a.months = '10' THEN a.qty ELSE 0 END) as m10                                       "
                sSql += "     , SUM(CASE WHEN a.months = '11' THEN a.qty ELSE 0 END) as m11                                       "
                sSql += "     , SUM(CASE WHEN a.months = '12' THEN a.qty ELSE 0 END) as m12                                       "
                sSql += "  FROM (SELECT comcd_out                                                                                 "
                sSql += "             , fn_ack_date_str(rtndt, 'MM') as months                                                    "
                sSql += "             , COUNT(comcd) as qty                                                                       "
                sSql += "          FROM lb031m                                                                                    "
                sSql += "         WHERE rtndt BETWEEN :dates AND :datee || '235959'                                                                     "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

                If rsGbn = "R"c Then
                    sSql += "           AND keepgbn = '3'                                                                         "
                Else
                    sSql += "           AND keepgbn in ('4', '5')                                                                 "
                End If

                sSql += "           AND regdt IN (SELECT MAX(regdt)                                                               "
                sSql += "                             FROM lb031m                                                                 "
                sSql += "                            WHERE rtndt BETWEEN :dates AND :datee || '235959'                                                        "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

                If rsGbn = "R"c Then
                    sSql += "                              AND keepgbn = '3'                                                      "
                Else
                    sSql += "                              AND keepgbn in ('4', '5')                                              "
                End If

                sSql += "                           GROUP BY bldno, comcd)                                                        "
                sSql += "        GROUP BY comcd_out, fn_ack_date_str(rtndt, 'MM') ) a                                                 "
                sSql += "     , lf120m b                                                                                          "
                sSql += " WHERE a.comcd_out = b.comcd                                                                             "

                sSql += "   AND b.usdt <= :dates                                                                      "
                sSql += "   AND b.uedt >  :datee || '235959'                                                                      "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

                sSql += " GROUP BY a.comcd_out, b.comnmd                                                                           "
                sSql += " ORDER BY a.comcd_out                                                                                     "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        'Public Shared Function fn_RtnCntSearchYear(ByVal rsYear As String, ByVal rsGbn As String) As DataTable
        '    ' 혈액 반납/폐기 건수 조회
        '    Dim sFn As String = "Public Shared Function fn_RtnCntSearchDay(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable"
        '    Dim sSql As String = ""
        '    Dim alParm As New ArrayList

        '    Try
        '        sSql += "SELECT b.comnmd                                                                                                "
        '        sSql += "     , (SELECT COUNT(comcd) as qty                                                                             "
        '        sSql += "          FROM lb020m                                                                                          "
        '        sSql += "         WHERE statedt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                                              "

        '        alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
        '        alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

        '        sSql += "           AND state in ('4', '6')                                                                             "
        '        sSql += "           AND comcd = a.comcd_out) as suma                                                                       "
        '        sSql += "     , SUM(a.qty) as sumt                                                                                      "
        '        sSql += "     , SUM(CASE WHEN a.years = :year - 1 THEN a.qty ELSE 0 END) as y1                                         "
        '        sSql += "     , SUM(CASE WHEN a.years = :year THEN a.qty ELSE 0 END) as y2                                                                 "

        '        alParm.Add(New OracleParameter("year",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
        '        alParm.Add(New OracleParameter("year",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

        '        sSql += "  FROM (SELECT comcd_out                                                                                       "
        '        sSql += "             , fn_ack_date_str(rtndt, 'YYYY') as years                                                         "
        '        sSql += "             , COUNT(comcd) as qty                                                                             "
        '        sSql += "          FROM lb031m                                                                                          "
        '        sSql += "         WHERE rtndt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                                    "

        '        alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
        '        alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

        '        If rsGbn = "R"c Then
        '            sSql += "           AND keepgbn = '3'                                                                               "
        '        Else
        '            sSql += "           AND keepgbn in ('4', '5')                                                                       "
        '        End If

        '        sSql += "           AND regdt IN (SELECT MAX(regdt)                                                                     "
        '        sSql += "                             FROM lb031m                                                                       "
        '        sSql += "                            WHERE rtndt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                        "

        '        alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
        '        alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

        '        If rsGbn = "R"c Then
        '            sSql += "                              AND keepgbn = '3'                                                            "
        '        Else
        '            sSql += "                              AND keepgbn in ('4', '5')                                                    "
        '        End If

        '        sSql += "                           GROUP BY bldno, comcd)                                                              "
        '        sSql += "        GROUP BY comcd_out, fn_ack_date_str(rtndt, 'YYYY') ) a                                                 "
        '        sSql += "     , lf120m b                                                                                                "
        '        sSql += " WHERE a.comcd_out = b.comcd                                                                                       "
        '        sSql += "   AND b.usdt <= :dates || '0101000000'                                                                      "
        '        sSql += "   AND b.uedt >  :datee || '1231235959'                                                                      "

        '        alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
        '        alParm.Add(New OracleParameter("datee",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

        '        sSql += " GROUP BY a.comcd_out, b.comnmd                                                                                     "
        '        sSql += " ORDER BY a.comcd_out                                                                                               "

        '        DbCommand()
        '        Return DbExecuteQuery(sSql, alParm)

        '    Catch ex As Exception
        '        Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        '    End Try
        'End Function
        '<<<
        Public Shared Function fn_RtnCntSearchYear(ByVal rsYear As String, ByVal rsGbn As String) As DataTable
            ' 혈액 반납/폐기 건수 조회
            Dim sFn As String = "Public Shared Function fn_RtnCntSearchDay(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT b.comnmd                                                                                                "
                sSql += "     , (SELECT COUNT(comcd) as qty                                                                             "
                sSql += "          FROM lb020m                                                                                          "
                sSql += "         WHERE statedt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                                              "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "           AND state in ('4', '6')                                                                             "
                sSql += "           AND comcd = a.comcd_out) as suma                                                                       "
                sSql += "     , SUM(a.qty) as sumt                                                                                      "
                sSql += "     , SUM(CASE WHEN a.years = :year THEN a.qty ELSE 0 END) as y1                                         "
                'sSql += "     , SUM(CASE WHEN a.years = :year THEN a.qty ELSE 0 END) as y2                                                                 "

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                'alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT comcd_out                                                                                       "
                sSql += "             , fn_ack_date_str(rtndt, 'YYYY') as years                                                         "
                sSql += "             , COUNT(comcd) as qty                                                                             "
                sSql += "          FROM lb031m                                                                                          "
                sSql += "         WHERE rtndt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                                    "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsGbn = "R"c Then
                    sSql += "           AND keepgbn = '3'                                                                               "
                Else
                    sSql += "           AND keepgbn in ('4', '5')                                                                       "
                End If

                sSql += "           AND regdt IN (SELECT MAX(regdt)                                                                     "
                sSql += "                             FROM lb031m                                                                       "
                sSql += "                            WHERE rtndt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                        "


                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsGbn = "R"c Then
                    sSql += "                              AND keepgbn = '3'                                                            "
                Else
                    sSql += "                              AND keepgbn in ('4', '5')                                                    "
                End If

                sSql += "                           GROUP BY bldno, comcd)                                                              "
                sSql += "        GROUP BY comcd_out, fn_ack_date_str(rtndt, 'YYYY') ) a                                                 "
                sSql += "     , lf120m b                                                                                                "
                sSql += " WHERE a.comcd_out = b.comcd                                                                                       "
                sSql += "   AND b.usdt <= :dates || '0101000000'                                                                      "
                sSql += "   AND b.uedt >  :datee || '1231235959'                                                                      "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += " GROUP BY a.comcd_out, b.comnmd                                                                                     "
                sSql += " ORDER BY a.comcd_out                                                                                               "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

#End Region

#Region " CrossMating 결과저장 "
        Public Shared Function fn_OutCrossList(ByVal rsfDate As String, ByVal rstDate As String, Optional ByVal rsGbn As String = "", Optional ByVal rsRegno As String = "", Optional ByVal rsComcd As String = "") As DataTable
            ' 혈액 반납/폐기 량
            Dim sFn As String = "Public Shared Function fn_OutCrossList(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsGbn As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                If rsGbn = "N"c Then
                    sSql += "SELECT fn_ack_get_tnsjubsuno_full(c.tnsjubsuno)          as vtnsjubsuno "
                    sSql += "     , CASE WHEN c.tnsgbn = '1' THEN '준비'                             "
                    sSql += "            WHEN c.tnsgbn = '2' THEN '수혈'                             "
                    sSql += "            WHEN c.tnsgbn = '3' THEN '교차미필'                         "
                    sSql += "            WHEN c.tnsgbn = '4' THEN 'Irra.'                            "
                    sSql += "       END                                           as tnsgbn      "
                    sSql += "     , c.regno                                                          "
                    sSql += "     , c.patnm                                                          "
                    sSql += "     , c.sex || '/' || c.age                         as sexage      "
                    sSql += "     , fn_ack_get_bldno_full(a.bldno)                    as vbldno      "
                    sSql += "     , e.comnmd                                                         "
                    sSql += "     , f.abo || f.rh                                 as aborh       "
                    sSql += "     , a.rst1                                                           "
                    sSql += "     , a.rst2                                                           "
                    sSql += "     , a.rst3                                                           "
                    sSql += "     , a.rst4                                                           "
                    sSql += "     , a.cmrmk                                                          "
                    sSql += "     , fn_ack_get_usr_name(a.testid)                     as testid      "
                    sSql += "     , fn_ack_date_str(a.testdt, 'yyyy-MM-dd hh24:mi')   as testdt      "
                    sSql += "     , ''                                            as testid2     "
                    sSql += "     , ''                                            as testdt2     "
                    sSql += "     , fn_ack_date_str(a.befoutdt, 'yyyy-MM-dd hh24:mi') as befoutdt    "
                    sSql += "     , fn_ack_get_usr_name(a.outid)                      as outid       "
                    sSql += "     , fn_ack_date_str(a.outdt, 'yyyy-MM-dd hh24:mi')    as outdt       "
                    sSql += "     , a.recnm                                                          "
                    sSql += "     , a.bldno                                                          "
                    sSql += "     , a.comcd_out                                                      "
                    sSql += "     , a.tnsjubsuno                                                     "
                    sSql += "     , fn_ack_date_str(c.orddt, 'yyyy-MM-dd')            as order_date  "
                    sSql += "     , e.crosslevel                                                     "
                    sSql += "     , e.comcd                                                          "
                    sSql += "  FROM lb030m a                                                         "
                    sSql += "     , lb043m b                                                         "
                    sSql += "     , lb040m c                                                         "
                    sSql += "     , lf120m e                                                         "
                    sSql += "     , lb020m f                                                         "
                    sSql += " WHERE a.outdt BETWEEN :dates AND :datee || '235959'                    "

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                    If rsComcd <> "" And rsComcd <> "ALL" Then
                        sSql += "   AND a.comcd_out = :comcd                                                  "
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If

                    sSql += "   AND a.tnsjubsuno = b.tnsjubsuno                                      "
                    sSql += "   AND a.comcd      = b.comcd                                           "
                    sSql += "   AND a.bldno      = b.bldno                                           "
                    sSql += "   AND b.tnsjubsuno = c.tnsjubsuno                                      "
                    sSql += "   AND c.tnsgbn  = '3'                                                  "

                    If rsRegno <> "" Then
                        sSql += "   and c.regno  = :regno                                                     "
                        alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                    End If

                    sSql += "   AND a.comcd_out  = e.comcd                                           "
                    sSql += "   AND b.spccd      = e.spccd                                           "
                    sSql += "   AND a.bldno      = f.bldno                                           "
                    sSql += "   AND a.comcd_out  = f.comcd                                           "
                    sSql += "   AND (a.bldno || a.comcd_out) NOT IN                                   "
                    sSql += "       (SELECT bldno || comcd_out FROM lb032m                            "
                    sSql += "         WHERE outdt BETWEEN :dates AND :datee || '235959'                                 "
                    sSql += "       )                                                                "

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                    sSql += " ORDER BY tnsjubsuno, vbldno, comcd                                     "

                Else
                    sSql += "SELECT fn_ack_get_tnsjubsuno_full(c.tnsjubsuno)          as vtnsjubsuno "
                    sSql += "     , CASE WHEN c.tnsgbn = '1' THEN '준비'                             "
                    sSql += "            WHEN c.tnsgbn = '2' THEN '수혈'                             "
                    sSql += "            WHEN c.tnsgbn = '3' THEN '교차미필'                         "
                    sSql += "            WHEN c.tnsgbn = '4' THEN 'Irra.'                            "
                    sSql += "       END                                           as tnsgbn      "
                    sSql += "     , c.regno                                                          "
                    sSql += "     , c.patnm                                                          "
                    sSql += "     , c.sex || '/' || c.age                         as sexage      "
                    sSql += "     , fn_ack_get_bldno_full(a.bldno)                    as vbldno      "
                    sSql += "     , e.comnmd                                                         "
                    sSql += "     , f.abo || f.rh                                 as aborh       "
                    sSql += "     , d.rst1                                                           "
                    sSql += "     , d.rst2                                                           "
                    sSql += "     , d.rst3                                                           "
                    sSql += "     , d.rst4                                                           "
                    sSql += "     , d.cmrmk                                                          "
                    sSql += "     , fn_ack_get_usr_name(a.testid)                     as testid      "
                    sSql += "     , fn_ack_date_str(a.testdt, 'yyyy-MM-dd hh24:mi')   as testdt      "
                    sSql += "     , ''                                            as testid2     "
                    sSql += "     , ''                                            as testdt2     "
                    sSql += "     , fn_ack_date_str(a.befoutdt, 'yyyy-MM-dd hh24:mi') as befoutdt    "
                    sSql += "     , fn_ack_get_usr_name(a.outid)                      as outid       "
                    sSql += "     , fn_ack_date_str(a.outdt, 'yyyy-MM-dd hh24:mi')    as outdt       "
                    sSql += "     , a.recnm                                                          "
                    sSql += "     , a.bldno                                                          "
                    sSql += "     , a.comcd_out                                                      "
                    sSql += "     , a.tnsjubsuno                                                     "
                    sSql += "     , fn_ack_date_str(c.orddt, 'yyyy-MM-dd')            as order_date  "
                    sSql += "     , e.crosslevel                                                     "
                    sSql += "  FROM lb030m a                                                         "
                    sSql += "     , lb043m b                                                         "
                    sSql += "     , lb040m c                                                         "
                    sSql += "     , lb032m d                                                         "
                    sSql += "     , lf120m e                                                         "
                    sSql += "     , lb020m f                                                         "
                    sSql += " WHERE a.outdt BETWEEN :dates AND :datee || '235959'                    "

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                    If rsComcd <> "" And rsComcd <> "ALL" Then
                        sSql += "   AND a.comcd_out = :comcd                                                 "
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If

                    sSql += "   AND a.tnsjubsuno = b.tnsjubsuno                                      "
                    sSql += "   AND a.comcd      = b.comcd                                           "
                    sSql += "   AND a.bldno      = b.bldno                                           "
                    sSql += "   AND b.tnsjubsuno = c.tnsjubsuno                                      "
                    sSql += "   AND c.tnsgbn     = '3'                                               "

                    If rsRegno <> "" Then
                        sSql += "   and c.regno  = :regno                                                     "
                        alParm.Add(New OracleParameter("regno", OracleDbType.Varchar2, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                    End If
                    sSql += "   AND a.bldno      = d.bldno                                           "
                    sSql += "   AND a.comcd_out  = d.comcd_out                                       "
                    sSql += "   AND a.comcd_out  = e.comcd                                           "
                    sSql += "   AND b.spccd      = e.spccd                                           "
                    sSql += "   AND a.bldno      = f.bldno                                           "
                    sSql += "   AND a.comcd_out  = f.comcd                                           "
                    sSql += " ORDER BY c.tnsjubsuno, a.bldno, e.comcd                                "
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function



#End Region

#Region " 혈액반납폐기율 "
        Public Shared Function fn_percentOfRtnBloodTypeY(ByVal rsGbn As String, ByVal rsYear As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_percentOfRtnBloodTypeY(ByVal rsGbn As String, ByVal rsDate As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT b.joincd                                                                 "
                sSql += "       , (SELECt clscd FROM LF000M where  clsval = dept.deptnmd  ) as deptsortgbn   " + vbCrLf '20211007 jhs 정렬 추가

                If rsGroup = "1"c Then
                    'sSql += " , fn_ack_get_dept_abbr(b.iogbn, b.joincd)                     as gbnnm            "
                    sSql += "  , dept.deptnmd as gbnnm"
                Else
                    sSql += " , b.comnmd                                                as gbnnm            "
                End If

                sSql += "     , b.rtnrsncd                                              as rsncd         "
                sSql += "     , b.rtnrsncmt                                             as rsnnm         "
                sSql += "     , NVL(a.qty, 0)                                           as sumall        "
                sSql += "     , CASE WHEN SUM(NVL(a.qty, 0)) = 0 THEN 0"
                sSql += "            ELSE ROUND((SUM(NVL(b.qty, 0)) * 1.0 / SUM(a.qty)) * 100, 2)"
                sSql += "       END                                                     as per "
                sSql += "     , '1'                                                     as sortgbn       "
                sSql += "     , '2'                                                     as subgbn        "
                'sSql += "     , SUM(CASE WHEN b.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END)  as year1 "
                sSql += "     , SUM(CASE WHEN b.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                as year2 "

                'alParm.Add(New OracleParameter("year",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT '1'            as joingbn                                        "
                sSql += "             , COUNT(a.bldno) as qty                                            "
                sSql += "          FROM (                                                         "
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                 "
                sSql += "                  FROM lb030m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                     "


                'alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                         "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                            "
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                 "
                sSql += "                  FROM lb031m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                               "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND rtnflg IN ( '2')                                      "
                sSql += "               ) a                                                                "
                sSql += "             , lb040m b                                                         "
                sSql += "             , lb043m c                                                         "
                sSql += "             , lb020m d                                                         "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                         "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959' "

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                 "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND b.tnsjubsuno = c.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "
                sSql += "           AND a.bldno      = d.bldno                                           "
                sSql += "           AND a.comcd_out  = d.comcd                                           "
                'sSql += "           AND C.STATE IN ('4','6')                                             "
                '  sSql += "           AND C.STATE IN ('5','6')                                             "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                           "
                    sSql += "       AND c.spccd      = e.spccd                                           "
                    sSql += "       AND b.jubsudt   >= e.usdt                                            "
                    sSql += "       AND b.jubsudt   <  e.uedt                                            "
                End If

                sSql += "       ) a LEFT OUTER JOIN                                                      "
                sSql += "       (SELECT '1'                      as joingbn                              "
                sSql += "             , fn_ack_date_str(a.outdt, 'yyyy') as years                        "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                 "
                    sSql += "         , b.iogbn                                                          "
                Else
                    sSql += "         , a.comcd_out            as joincd                                 "
                    sSql += "         , e.comnmd                                                         "
                End If

                sSql += "             , a.rtnrsncd                                                       "
                sSql += "             , a.rtnrsncmt                                                      "
                sSql += "             , COUNT(a.bldno)           as qty                                  "
                sSql += "          FROM lb031m a                                                         "
                sSql += "             , lb040m b                                                         "
                sSql += "             , lb043m c                                                         "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                          "

                'alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                      "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "

                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                                  "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND a.comcd_out  = e.comcd                                              "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                    sSql += "       AND b.jubsudt   >= e.usdt                                            "
                    sSql += "       AND b.jubsudt   <  e.uedt                                            "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), b.deptcd, b.iogbn, a.rtnrsncd, a.rtnrsncmt"
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), a.comcd_out, e.comnmd, a.rtnrsncd, a.rtnrsncmt"
                End If

                sSql += "       ) b ON (a.joingbn  = b.joingbn)"
                'sSql += "       INNER JOIN lf170m c ON (b.rtnrsncd = c.cmtcd)                              "

                'If rsGbn = "1"c Then
                '    sSql += " WHERE c.cmtgbn   = '0'                                                        "
                'Else
                '    sSql += " WHERE c.cmtgbn   = '1'                                                        "
                'End If

                If rsGroup = "1"c Then
                    sSql += " RIGHT OUTER JOIN vw_ack_ocs_dept_info dept "
                    sSql += "               ON dept.deptcd = b.joincd     "
                    '20210104 jhs 진료과 항목 'IMG','IMC','IME','IMR','IMN','IMH','IMI','NU','NP','GS','OS','NS','TS' ,'PS','OG','OT','OL','DM','UR','FM','EM','BB' 만 표기 되도록 수정
                    sSql += "   where dept.deptnmd in (SELECt clsval FROM LF000M where clsgbn = 'B23') "
                    '-------------------------------------------------------------------------------------
                    sSql += "GROUP BY b.joincd, b.iogbn, b.rtnrsncd, b.rtnrsncmt, a.qty   , dept.deptnmd                           "
                Else
                    'sSql += "GROUP BY b.joincd, b.rtnrsncd, c.cmtcont, b.comnmd, a.qty                             "
                    sSql += "GROUP BY b.joincd, b.rtnrsncd,b.rtnrsncmt, b.comnmd, a.qty                             "

                End If

                sSql += "UNION ALL                                                                       "
                sSql += "SELECT b.joincd                                                as joincd        "
                sSql += "     , (SELECt clscd FROM LF000M where  clsval = dept.deptnmd  ) as deptsortgbn    " + vbCrLf '20211007 jhs 정렬 추가
                sSql += "     , '        '                                              as gbnnm         "
                sSql += "     , 'aaaaa'                                                 as rsncd         "
                sSql += "     , '[합 계]'                                               as rsnnm         "
                sSql += "     , NVL(a.qty, 0)                                           as sumall        "
                sSql += "     , CASE WHEN SUM(NVL(a.qty, 0)) = 0 THEN 0                                  "
                sSql += "            ELSE ROUND((SUM(NVL(b.qty, 0)) * 1.0 / a.qty) * 100, 2)"
                sSql += "       END                                                     as per           "
                sSql += "     , '2'                                                     as sortgbn       "
                sSql += "     , '1'                                                     as subgbn        "
                'sSql += "     , SUM(CASE WHEN b.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END) as year1"
                sSql += "     , SUM(CASE WHEN b.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                   as year2"

                ' alParm.Add(New OracleParameter("year",  OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT '1'            as joingbn                                        "
                sSql += "             , COUNT(a.bldno) as qty                                            "
                sSql += "          FROM (                                                         "
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                 "
                sSql += "                  FROM lb030m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                 "

                'alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                      "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                            "
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                 "
                sSql += "                  FROM lb031m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                  "

                'alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                If rsGbn = "1"c Then
                    sSql += "       AND rtnflg     = '1'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND rtnflg     = '2'                                                  "
                End If

                ' sSql += "                   AND rtnflg IN ( '2')                                      "
                sSql += "               ) a                                                                "
                sSql += "             , lb040m b                                                         "
                sSql += "             , lb043m c                                                         "
                sSql += "             , lb020m d                                                         "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                         "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'      "

                'alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND b.tnsjubsuno = c.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "
                sSql += "           AND a.bldno      = d.bldno                                           "
                sSql += "           AND a.comcd_out  = d.comcd                                           "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                           "
                    sSql += "       AND c.spccd      = e.spccd                                           "
                    sSql += "       AND b.jubsudt   >= e.usdt                                            "
                    sSql += "       AND b.jubsudt   <  e.uedt                                            "
                End If

                sSql += "       ) a LEFT OUTER JOIN                                                      "
                sSql += "       (SELECT '1'                      as joingbn                              "
                sSql += "             , fn_ack_date_str(a.outdt, 'yyyy') as years                        "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                 "
                    sSql += "         , b.iogbn                                                          "
                Else
                    sSql += "         , a.comcd_out            as joincd                                 "
                    sSql += "         , e.comnmd                                                         "
                End If

                sSql += "             , a.rtnrsncd                                                       "
                sSql += "             , COUNT(a.bldno)           as qty                                  "
                sSql += "          FROM lb031m a                                                         "
                sSql += "             , lb040m b                                                         "
                sSql += "             , lb043m c                                                         "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                            "

                'alParm.Add(New OracleParameter("dates",  OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                      "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "
                'sSql += "           AND C.STATE IN ('4','6')                                             "
                '  sSql += "           AND C.STATE IN ('5','6')                                             "
                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                                  "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND a.comcd_out  = e.comcd                                              "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                    sSql += "       AND b.jubsudt   >= e.usdt                                               "
                    sSql += "       AND b.jubsudt   <  e.uedt                                               "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), b.deptcd, b.iogbn, a.rtnrsncd"
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), a.comcd_out, e.comnmd, a.rtnrsncd"
                End If

                sSql += "       ) b ON (a.joingbn  = b.joingbn)"
                'sSql += "       INNER JOIN lf170m c ON (b.rtnrsncd = c.cmtcd)                              "

                'If rsGbn = "1"c Then
                '    'sSql += " WHERE c.cmtgbn   = '0'                                                        "
                '    sSql += " WHERE c.cmtgbn   = '1'                                                        "
                'Else
                '    'sSql += " WHERE c.cmtgbn   = '1'                                                        "
                '    sSql += " WHERE c.cmtgbn   = '0'                                                        "
                'End If

                If rsGroup = "1"c Then
                    sSql += " RIGHT OUTER JOIN vw_ack_ocs_dept_info dept "
                    sSql += "               ON dept.deptcd = b.joincd     "
                    sSql += "   where dept.deptnmd in (SELECt clsval FROM LF000M where clsgbn = 'B23') " + vbCrLf
                    sSql += " GROUP BY b.joincd, b.iogbn, a.qty,   dept.deptnmd                   "
                Else
                    sSql += " GROUP BY b.joincd, b.comnmd, a.qty                        "
                End If

                'sSql += " ORDER BY deptsortgbn, joincd, sortgbn   , subgbn                                               "

                If rsGroup = "1"c Then
                    sSql += " ORDER BY  deptsortgbn, joincd,   sortgbn , subgbn                     " + vbCrLf
                Else
                    sSql += " ORDER BY joincd, sortgbn   , subgbn                                    " + vbCrLf
                End If


                DbCommand()

                fn_percentOfRtnBloodTypeY = DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_percentOfRtnBloodTypeM(ByVal rsGbn As String, ByVal rsYear As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_percentOfRtnBloodY(ByVal rsGbn As String, ByVal rsDate As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT b.joincd                                                                   " + vbCrLf
                sSql += "       , (SELECt clscd FROM LF000M where  clsval = dept.deptnmd  )    as deptsortgbn   " + vbCrLf '20211007 jhs 정렬 추가

                If rsGroup = "1"c Then
                    'sSql += " , fn_ack_get_dept_abbr(b.iogbn, b.joincd)                 as gbnnm           "+ vbCrLf 
                    sSql += " , dept.deptnmd as gbnnm" + vbCrLf
                Else
                    sSql += " , b.comnmd                                                as gbnnm           " + vbCrLf
                End If

                sSql += "     , b.rtnrsncd                                              as rsncd           " + vbCrLf
                'sSql += "     , c.cmtcont                                               as rsnnm           "+ vbCrLf 
                sSql += "     , b.rtnrsncmt                                             as rsnnm           " + vbCrLf
                sSql += "     , NVL(a.qty, 0)                                           as sumall          " + vbCrLf
                sSql += "     , CASE WHEN a.qty = 0 THEN 0                                                 " + vbCrLf
                sSql += "            ELSE ROUND((SUM(NVL(b.qty, 0)) * 1.0 / a.qty) * 100, 2)       " + vbCrLf
                sSql += "       END                                                             as per     " + vbCrLf
                sSql += "     , '1'                                                             as sortgbn " + vbCrLf
                sSql += "     , '2'                                                             as subgbn  " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '01' THEN NVL(b.qty, 0) ELSE 0 END) as m1      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '02' THEN NVL(b.qty, 0) ELSE 0 END) as m2      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '03' THEN NVL(b.qty, 0) ELSE 0 END) as m3      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '04' THEN NVL(b.qty, 0) ELSE 0 END) as m4      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '05' THEN NVL(b.qty, 0) ELSE 0 END) as m5      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '06' THEN NVL(b.qty, 0) ELSE 0 END) as m6      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '07' THEN NVL(b.qty, 0) ELSE 0 END) as m7      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '08' THEN NVL(b.qty, 0) ELSE 0 END) as m8      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '09' THEN NVL(b.qty, 0) ELSE 0 END) as m9      " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '10' THEN NVL(b.qty, 0) ELSE 0 END) as m10     " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '11' THEN NVL(b.qty, 0) ELSE 0 END) as m11     " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '12' THEN NVL(b.qty, 0) ELSE 0 END) as m12     " + vbCrLf
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy')                        as years   " + vbCrLf
                sSql += "             , COUNT(a.bldno)                                          as qty     " + vbCrLf
                sSql += "          FROM (                                                                  " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                 " + vbCrLf
                sSql += "                  FROM lb030m                                                     " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                                      " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                         " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                            " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                 " + vbCrLf
                sSql += "                  FROM lb031m                                                     " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'      " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                If rsGbn = "1"c Then
                    sSql += "       AND rtnflg     = '1'                                                  " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND rtnflg     = '2'                                                  " + vbCrLf
                End If

                'sSql += "                   AND rtnflg IN ( '2')                                       "
                sSql += "               ) a                                                                " + vbCrLf
                sSql += "             , lb040m b                                                           " + vbCrLf
                sSql += "             , lb043m c                                                           " + vbCrLf
                sSql += "             , lb020m d                                                           " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                    " + vbCrLf
                End If

                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                          " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                              " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                              " + vbCrLf
                'sSql += "           AND C.STATE IN ('4','6')                                             "
                'sSql += "           AND C.STATE IN ('5','6')                                             "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                              " + vbCrLf
                    sSql += "       AND b.jubsudt   >= e.usdt                                               " + vbCrLf
                    sSql += "       AND b.jubsudt   <  e.uedt                                               " + vbCrLf
                End If

                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy')                                  " + vbCrLf
                sSql += "       ) a LEFT OUTER JOIN                                                         " + vbCrLf
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years, fn_ack_date_str(a.outdt, 'MM') as months" + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    " + vbCrLf
                    sSql += "         , b.iogbn                                                             " + vbCrLf
                Else
                    sSql += "         , a.comcd_out            as joincd                                    " + vbCrLf
                    sSql += "         , e.comnmd                                                            " + vbCrLf
                End If

                sSql += "             , a.rtnrsncd                                                          " + vbCrLf
                sSql += "             , a.rtnrsncmt                                                         " + vbCrLf
                sSql += "             , COUNT(a.bldno)         as qty                                       " + vbCrLf
                sSql += "          FROM lb031m a                                                            " + vbCrLf
                sSql += "             , lb040m b                                                            " + vbCrLf
                sSql += "             , lb043m c                                                            " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            " + vbCrLf
                End If

                sSql += "         WHERE a.outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                                      " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                          " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf

                'If rsGbn = "1"c Then
                '    sSql += "       AND a.rtnflg     = '1'                                                  "
                'ElseIf rsGbn = "2"c Then
                '    sSql += "       AND a.rtnflg     = '2'                                                  "
                'End If

                If rsGbn = "1"c Then
                    sSql += "       AND a.keepgbn     = '3'                                                  " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.keepgbn     = '4'                                                  " + vbCrLf
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND a.comcd_out  = e.comcd                                              " + vbCrLf
                    sSql += "       AND b.jubsudt   >= e.usdt                                               " + vbCrLf
                    sSql += "       AND b.jubsudt   <  e.uedt                                               " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                              " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn, a.rtnrsncd, a.rtnrsncmt    " + vbCrLf
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd, a.rtnrsncd, a.rtnrsncmt" + vbCrLf
                End If

                sSql += "       ) b ON (a.years = b.years)" + vbCrLf
                'sSql += "       INNER JOIN"
                'sSql += "             lf170m c ON (b.rtnrsncd = c.cmtcd)                                    "
                'If rsGbn = "1"c Then
                '    sSql += " WHERE c.cmtgbn   = '0'                                                        "
                'Else
                '    sSql += " WHERE c.cmtgbn   = '1'                                                        "
                'End If

                If rsGroup = "1"c Then
                    sSql += " RIGHT OUTER JOIN vw_ack_ocs_dept_info dept" + vbCrLf
                    sSql += "               ON dept.deptcd = b.joincd      " + vbCrLf
                    '20210104 jhs 진료과 항목 'IMG','IMC','IME','IMR','IMN','IMH','IMI','NU','NP','GS','OS','NS','TS' ,'PS','OG','OT','OL','DM','UR','FM','EM','BB' 만 표기 되도록 수정
                    sSql += "   where dept.deptnmd in (SELECt clsval FROM LF000M where clsgbn = 'B23') " + vbCrLf
                    '-------------------------------------------------------------------------------------
                    sSql += "GROUP BY b.joincd, b.iogbn, b.rtnrsncd, b.rtnrsncmt, a.qty    ,dept.deptnmd                   " + vbCrLf
                Else
                    sSql += "GROUP BY b.joincd, b.rtnrsncd, b.rtnrsncmt, b.comnmd, a.qty                      " + vbCrLf
                End If

                sSql += "UNION ALL                                                                          " + vbCrLf
                sSql += "SELECT b.joincd                                                as joincd           " + vbCrLf
                sSql += "     , (SELECt clscd FROM LF000M where  clsval = dept.deptnmd  ) as deptsortgbn    " + vbCrLf '20211007 jhs 정렬 추가
                sSql += "     , '        '                                              as gbnnm            " + vbCrLf
                sSql += "     , 'aaaaa'                                                 as rsncd            " + vbCrLf
                sSql += "     , '[합 계]'                                               as rsnnm            " + vbCrLf
                sSql += "     , NVL(a.qty, 0)                                           as sumall           " + vbCrLf
                ' sSql += "     , ROUND((SUM(NVL(b.qty, 0)) * 1.0 / NVL(a.qty, 0)) * 100, 2) as per              "
                sSql += "      , ROUND (SUM (NVL (b.qty, 0)) * 1.0 / SUM (DECODE(a.qty, 0, NULL, a.qty)) * 100, 2) as per" + vbCrLf
                sSql += "     , '2'                                                     as sortgbn          " + vbCrLf
                sSql += "     , '1'                                                     as subgbn           " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '01' THEN NVL(b.qty, 0) ELSE 0 END)           as m1               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '02' THEN NVL(b.qty, 0) ELSE 0 END)           as m2               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '03' THEN NVL(b.qty, 0) ELSE 0 END)           as m3               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '04' THEN NVL(b.qty, 0) ELSE 0 END)           as m4               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '05' THEN NVL(b.qty, 0) ELSE 0 END)           as m5               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '06' THEN NVL(b.qty, 0) ELSE 0 END)           as m6               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '07' THEN NVL(b.qty, 0) ELSE 0 END)           as m7               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '08' THEN NVL(b.qty, 0) ELSE 0 END)           as m8               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '09' THEN NVL(b.qty, 0) ELSE 0 END)           as m9               " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '10' THEN NVL(b.qty, 0) ELSE 0 END)           as m10              " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '11' THEN NVL(b.qty, 0) ELSE 0 END)           as m11              " + vbCrLf
                sSql += "     , SUM(CASE WHEN b.months = '12' THEN NVL(b.qty, 0) ELSE 0 END)           as m12              " + vbCrLf
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                   " + vbCrLf
                sSql += "             , COUNT(a.bldno)         as qty                                               " + vbCrLf
                sSql += "          FROM (" + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt          " + vbCrLf
                sSql += "                  FROM lb030m                                              " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                          " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                     " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt          " + vbCrLf
                sSql += "                  FROM lb031m                                              " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                         " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                If rsGbn = "1"c Then
                    sSql += "       AND rtnflg     = '1'                                                  " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND rtnflg     = '2'                                                  " + vbCrLf
                End If

                'sSql += "                   AND rtnflg IN ( '2')                               "
                sSql += "               ) a                                                                         " + vbCrLf
                sSql += "             , lb040m b                                                                    " + vbCrLf
                sSql += "             , lb043m c                                                                    " + vbCrLf
                sSql += "             , lb020m d                                                                    " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                    " + vbCrLf
                End If

                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                          " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                              " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                              " + vbCrLf
                'sSql += "           AND C.STATE IN ('4','6')                                             "
                'sSql += "           AND C.STATE IN ('5','6')                                             "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                              " + vbCrLf
                    sSql += "       AND b.jubsudt   <  e.uedt                                               " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                              " + vbCrLf
                End If

                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy')                                  " + vbCrLf
                sSql += "       ) a LEFT OUTER JOIN                                                         " + vbCrLf
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years, fn_ack_date_str(a.outdt, 'MM') as months" + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    " + vbCrLf
                    sSql += "         , b.iogbn                                                             " + vbCrLf
                Else
                    sSql += "         , a.comcd_out            as joincd                                    " + vbCrLf
                    sSql += "         , e.comnmd                                                            " + vbCrLf
                End If

                sSql += "             , ''                     as rtnrsncd                                  " + vbCrLf
                sSql += "             , COUNT(a.bldno)         as qty                                       " + vbCrLf
                sSql += "          FROM lb031m a                                                            " + vbCrLf
                sSql += "             , lb040m b                                                            " + vbCrLf
                sSql += "             , lb043m c                                                            " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            " + vbCrLf
                End If

                sSql += "         WHERE a.outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                                            " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                          " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf

                'If rsGbn = "1"c Then
                '    sSql += "       AND a.rtnflg     = '1'                                                  "
                'ElseIf rsGbn = "2"c Then
                '    sSql += "       AND a.rtnflg     = '2'                                                  "
                'End If

                If rsGbn = "1"c Then
                    sSql += "       AND a.keepgbn     = '3'                                                  " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.keepgbn     = '4'                                                  " + vbCrLf
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND a.comcd_out  = e.comcd                                              " + vbCrLf
                    sSql += "       AND b.jubsudt   >= e.usdt                                               " + vbCrLf
                    sSql += "       AND b.jubsudt   <  e.uedt                                               " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                              " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn" + vbCrLf
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd" + vbCrLf
                End If

                sSql += "       ) b ON (a.years   = b.years)" + vbCrLf

                If rsGroup = "1"c Then
                    sSql += " RIGHT OUTER JOIN vw_ack_ocs_dept_info dept" + vbCrLf
                    sSql += "               ON dept.deptcd = b.joincd      " + vbCrLf
                    sSql += "   where dept.deptnmd in (SELECt clsval FROM LF000M where clsgbn = 'B23') " + vbCrLf
                    sSql += " GROUP BY b.joincd, b.iogbn, a.qty   , dept.deptnmd                                                " + vbCrLf
                Else
                    sSql += " GROUP BY b.joincd, b.comnmd, a.qty                                           " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += " ORDER BY  deptsortgbn, joincd,   sortgbn , subgbn                     " + vbCrLf
                Else
                    sSql += " GROUP BY   b.joincd, b.comnmd, a.qty                                   " + vbCrLf
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_percentOfRtnBloodY(ByVal rsGbn As String, ByVal rsYear As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_percentOfRtnBloodY(ByVal rsGbn As String, ByVal rsDate As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.joincd                                                                                       " + vbCrLf
                If rsGroup = "1"c Then
                    sSql += "       , (SELECt clscd FROM LF000M where  clsval = dept.deptnmd  ) as deptsortgbn   " + vbCrLf '20211007 jhs 정렬 추가
                Else
                    sSql += "       , '' as deptsortgbn   " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    '  sSql += " , fn_ack_get_dept_abbr(a.iogbn, a.joincd)                 as gbnnm                               "
                    sSql += "   , dept.deptnmd as gbnnm" + vbCrLf
                    'sSql += "   , dept.deptnmd ||  ' (' || a.iogbn  ||  ')'  as gbnnm" + vbCrLf
                Else
                    sSql += " , a.comnmd                                                as gbnnm                               " + vbCrLf
                End If

                sSql += "     , '2'                                                                               as sortgbn " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)     as ayear1       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)                       as ayear2       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END)     as year1        " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                       as year2        " + vbCrLf
                sSql += "     , ROUND(SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /              " + vbCrLf
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1" + vbCrLf
                sSql += "                 ELSE SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)" + vbCrLf
                sSql += "            END * 100, 2)                                                       as pyear1       " + vbCrLf
                sSql += "     , ROUND(SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                                " + vbCrLf
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1             " + vbCrLf
                sSql += "                 ELSE SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)" + vbCrLf
                sSql += "            END * 100, 2)                                                  as pyear2       " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                             " + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                                       " + vbCrLf
                    sSql += "         , b.iogbn                                                                                " + vbCrLf
                Else
                    sSql += "         , a.comcd_out            as joincd                                                       " + vbCrLf
                    sSql += "         , e.comnmd                                                                               " + vbCrLf
                End If

                sSql += "             , COUNT(a.bldno)           as qty                                                        " + vbCrLf
                sSql += "          FROM (" + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb030m                                                      " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb031m                                                      " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                  " + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND rtnflg IN ( '2')                                       " + vbCrLf
                sSql += "               ) a                                                                                    " + vbCrLf
                sSql += "             , lb040m b                                                                               " + vbCrLf
                sSql += "             , lb043m c                                                                               " + vbCrLf
                sSql += "             , lb020m d                                                                               " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                               " + vbCrLf
                End If

                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                                            " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                                             " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                                                 " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                                                 " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                                                 " + vbCrLf
                sSql += "           AND C.STATE IN ('4','6')                                             "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                                                 " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                                                 " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), b.deptcd, b.iogbn                              " + vbCrLf
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), a.comcd_out, e.comnmd                          " + vbCrLf
                End If
                sSql += "       ) a LEFT OUTER JOIN" + vbCrLf
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                              " + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                                       " + vbCrLf
                    sSql += "         , b.iogbn                                                                                " + vbCrLf
                Else
                    sSql += "         , a.comcd_out            as joincd                                                       " + vbCrLf
                    sSql += "         , e.comnmd                                                                               " + vbCrLf
                End If

                sSql += "             , COUNT(a.bldno)           as qty                                                        " + vbCrLf
                sSql += "          FROM lb031m a                                                                               " + vbCrLf
                sSql += "             , lb040m b                                                                               " + vbCrLf
                sSql += "             , lb043m c                                                                               " + vbCrLf
                sSql += "             , lb020m d                                                                               " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                               " + vbCrLf
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                                 " + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                                       " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                                            " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                                            " + vbCrLf
                sSql += "           AND a.comcd      = c.comcd                                                                 " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                                                 " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                                                 " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                                                 " + vbCrLf

                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                                                     " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                                                     " + vbCrLf
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                                                 " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                                                 " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), b.deptcd, b.iogbn                              " + vbCrLf
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), a.comcd_out, e.comnmd                          " + vbCrLf
                End If
                sSql += "       ) b ON a.joincd = b.joincd AND a.years  = b.years                                              " + vbCrLf

                If rsGroup = "1"c Then
                    sSql += " RIGHT OUTER JOIN vw_ack_ocs_dept_info dept" + vbCrLf
                    sSql += "               ON dept.deptcd = a.joincd " + vbCrLf
                    '20210104 jhs 진료과 항목 'IMG','IMC','IME','IMR','IMN','IMH','IMI','NU','NP','GS','OS','NS','TS' ,'PS','OG','OT','OL','DM','UR','FM','EM','BB' 만 표기 되도록 수정
                    sSql += "   where dept.deptnmd in (SELECt clsval FROM LF000M where clsgbn = 'B23') " + vbCrLf
                    '-------------------------------------------------------------------------------------
                    'sSql += " GROUP BY a.joincd, a.iogbn  , dept.deptnmd                                                                      " + vbCrLf
                    sSql += " GROUP BY a.joincd  , dept.deptnmd                                                                      " + vbCrLf
                    'sSql += " GROUP BY a.joincd , dept.deptnmd                                                                      " + vbCrLf   'test용 검사받고 바꿀지 생각
                Else
                    sSql += " GROUP BY a.joincd, a.comnmd                                                                       " + vbCrLf
                End If

                sSql += "UNION ALL                                                                                             " + vbCrLf
                sSql += "SELECT '11'                                                                                as joincd  " + vbCrLf
                sSql += "     , ' '                                                                                 as deptsortgbn " + vbCrLf '20211007 jhs 정렬 추가
                sSql += "     , '총합계 :'                                                                          as gbnnm   " + vbCrLf
                sSql += "     , '1'                                                                                 as sortgbn " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)       as ayear1  " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)                         as ayear2  " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END)       as year1   " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                         as year2   " + vbCrLf
                sSql += "     , ROUND(SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /     " + vbCrLf
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1" + vbCrLf
                sSql += "                 ELSE SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)" + vbCrLf
                sSql += "            END * 100, 2)                                                    as pyear1  " + vbCrLf
                sSql += "     , ROUND(SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                       " + vbCrLf
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1          " + vbCrLf
                sSql += "                 ELSE SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)" + vbCrLf
                sSql += "            END * 100, 2)                                                    as pyear2  " + vbCrLf

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                                      " + vbCrLf
                sSql += "             , COUNT(a.bldno)           as qty                                                        " + vbCrLf
                sSql += "          FROM (" + vbCrLf
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb030m                                                      " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                   " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb031m                                                      " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                   " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND rtnflg IN ( '2')                                       " + vbCrLf
                sSql += "               ) a                                                                               " + vbCrLf
                sSql += "             , lb040m b                                                                               " + vbCrLf
                sSql += "             , lb043m c                                                                               " + vbCrLf
                sSql += "             , lb020m d                                                                               " + vbCrLf
                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                                            " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                                                 " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                                                 " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                                                 " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                                                 " + vbCrLf
                sSql += "           AND C.STATE IN ('4','6')       " + vbCrLf
                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy')" + vbCrLf

                '20211013 jhs 자체폐기 내용 추가
                If rsGbn = "2"c Then
                    sSql += "          union all                                                                            " + vbCrLf
                    sSql += "         Select fn_ack_date_str(a.rtndt, 'yyyy')                                               " + vbCrLf
                    sSql += "         , COUNT(a.bldno)         as qty                                                       " + vbCrLf
                    sSql += "         from( SELECT bldno, comcd_out, comcd, rtndt                                           " + vbCrLf
                    sSql += "                 From lb031m                                                                   " + vbCrLf
                    sSql += "                Where rtndt  BETWEEN :dates || '0101000000' AND :datee || '1231235959'         " + vbCrLf

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                    sSql += "                And keepgbn = '5'                                                                " + vbCrLf
                    If rsComcd <> "ALL" Then
                        sSql += "                   AND comcd_out  = :comcd                                                   " + vbCrLf
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                    sSql += "                                    )   a                                                        " + vbCrLf
                    sSql += "        GROUP BY fn_ack_date_str(a.rtndt, 'yyyy')                                                " + vbCrLf
                End If

                sSql += "       ) a LEFT OUTER JOIN                                                  " + vbCrLf
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                              " + vbCrLf
                sSql += "             , COUNT(a.bldno)           as qty                                                        " + vbCrLf
                sSql += "          FROM lb031m a                                                                               " + vbCrLf
                sSql += "             , lb040m b                                                                               " + vbCrLf
                sSql += "             , lb043m c                                                                               " + vbCrLf
                sSql += "             , lb020m d                                                                               " + vbCrLf

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                                            " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                                            " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                                             " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                                                 " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                                                 " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                                                 " + vbCrLf


                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                                                     " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                                                     " + vbCrLf
                End If

                If rsComcd <> "ALL" Then
                    sSql += "                   AND a.comcd_out  = :comcd                                                   " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "         GROUP BY fn_ack_date_str(a.outdt, 'yyyy')                                                    " + vbCrLf
                sSql += "       ) b ON a.years = b.years                                                                       " + vbCrLf

                '20211013 jhs 자체폐기 내용 추가
                If rsGbn = "2"c Then
                    sSql += "       union all                                                                                                 " + vbCrLf
                    sSql += "       Select'12'                                                         as joincd                               " + vbCrLf
                    sSql += "       , ' '                                                              as deptsortgbn                         " + vbCrLf
                    sSql += "       , '자체폐기 :'                                                     as gbnnm                               " + vbCrLf
                    sSql += "       , '3'                                                                               as sortgbn          " + vbCrLf
                    sSql += "       , SUM(CASE WHEN a.years = TO_NUMBER(:Year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)       as ayear1             " + vbCrLf
                    sSql += "       , SUM(CASE WHEN a.years = :Year THEN NVL(a.qty, 0) ELSE 0 END)                      as ayear2          " + vbCrLf
                    sSql += "       , SUM(CASE WHEN a.years = TO_NUMBER(:Year) - 1 THEN NVL(0, 0) ELSE 0 END)           as year1                " + vbCrLf
                    sSql += "       , SUM(CASE WHEN a.years = :Year THEN NVL(0, 0) ELSE 0 END)                          as year2               " + vbCrLf
                    sSql += "       , ROUND(SUM(CASE WHEN a.years = TO_NUMBER(:Year) - 1 THEN NVL(0, 0) ELSE 0 END) * 1.0 /                   " + vbCrLf
                    sSql += "               Case WHEN SUM(CASE WHEN a.years = TO_NUMBER(:Year) - 1 THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1  " + vbCrLf
                    sSql += "               Else SUM(Case When a.years = TO_NUMBER(:Year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)                  " + vbCrLf
                    sSql += "                End * 100, 2)                                                              As pyear1                       " + vbCrLf
                    sSql += "       , ROUND(SUM(CASE WHEN a.years = :Year THEN NVL(0, 0) ELSE 0 END) * 1.0 /                                  " + vbCrLf
                    sSql += "               Case WHEN SUM(CASE WHEN a.years = :Year THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1                 " + vbCrLf
                    sSql += "               Else SUM(Case When a.years = :Year THEN NVL(a.qty, 0) ELSE 0 END)                                 " + vbCrLf
                    sSql += "                End * 100, 2)                                                              As pyear2                       " + vbCrLf
                    sSql += "       from(                                                                                                     " + vbCrLf
                    sSql += "            Select fn_ack_date_str(rtndt, 'yyyy') as years   ,  count(bldno) qty  from lb031m                    " + vbCrLf
                    sSql += "             where keepgbn = '5'                                                                                 " + vbCrLf
                    sSql += "               And rtndt  BETWEEN :dates || '0101000000' AND :datee || '1231235959'                              " + vbCrLf
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                    If rsComcd <> "ALL" Then
                        sSql += "                   AND comcd_out  = :comcd                                                   " + vbCrLf
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                    sSql += "             group by fn_ack_date_str(rtndt, 'yyyy')                                                             " + vbCrLf
                    sSql += "       ) a                                                                                                       " + vbCrLf
                End If
                '--------------------------------------------------------------------

                sSql += "ORDER BY sortgbn , deptsortgbn , joincd                                                                              " + vbCrLf

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_percentOfRtnBloodM(ByVal rsGbn As String, ByVal rsYear As String, ByVal rsYear1 As String, ByVal rsGroup As String, ByVal rsComcd As String, ByVal a_date As String()) As DataTable
            Dim sFn As String = "Public Shared Function fn_percentOfRtnBloodM(ByVal rsGbn As String, ByVal rsDate As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList
            Dim MM As String = rsYear.Substring(4, 2)
            Dim MM1 As String = rsYear1.Substring(4, 2)

            Try
                sSql += "SELECT a.joincd                                                                    " + vbCrLf
                If rsGroup = "1"c Then
                    sSql += "       , (SELECt clscd FROM LF000M where  clsval = dept.deptnmd  ) as deptsortgbn   " + vbCrLf
                Else
                    sSql += "       , '' as deptsortgbn   " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    'sSql += " , fn_ack_get_dept_abbr(a.iogbn, a.joincd)                 as gbnnm            "+vbcrlf
                    'sSql += ",( SELECT deptnmd FROM vw_ack_ocs_dept_info WHERE deptcd = a.joincd AND rownum = 1) AS gbnnm"+vbcrlf
                    sSql += " , dept.deptnmd as gbnnm " + vbCrLf
                Else
                    sSql += " , a.comnmd                                                as gbnnm            " + vbCrLf
                End If

                sSql += "     , SUM(NVL(a.qty, 0))                                         as sumall        " + vbCrLf
                sSql += "     , SUM(NVL(b.qty, 0))                                         as cnt           " + vbCrLf
                ' sSql += "     , ROUND(SUM(NVL(b.qty, 0)) * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)    as per           "+vbcrlf
                sSql += "     , ROUND (SUM (NVL (b.qty, 0)) * 1.0 / SUM (DECODE(a.qty, 0, NULL, a.qty)) * 100, 2) as per" + vbCrLf
                sSql += "     , '2'                                                        as sortgbn  ,     " + vbCrLf
                '<-- 2019-03-27 JJH 지정한 날짜구간에 맞게 추가
                For i As Integer = 1 To a_date.Length

                    sSql += "       SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(a.qty, 0) ELSE 0 END) as am" + i.ToString
                    sSql += "       ,SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(b.qty, 0) ELSE 0 END) as pm" + i.ToString
                    sSql += "     , ROUND(SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                        "
                    sSql += "            CASE WHEN SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                    sSql += "                 ELSE SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                    sSql += "            END * 100, 2)                                                             as per" + i.ToString

                    If i = a_date.Length Then
                        sSql += ""
                    Else
                        sSql += ","
                    End If
                Next
                '-->

                'sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'MM') as months                                                    "
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyyMM') as months                                                    " + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    " + vbCrLf
                    sSql += "         , b.iogbn                                                             " + vbCrLf
                Else
                    sSql += "         , a.comcd_out            as joincd                                    " + vbCrLf
                    sSql += "         , e.comnmd                                                            " + vbCrLf
                End If

                sSql += "             , COUNT(a.bldno)         as qty                                       " + vbCrLf
                sSql += "          FROM (                                                                   " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb030m                                                      " + vbCrLf
                'sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'"
                sSql += "                 WHERE outdt BETWEEN :year || '01000000' AND :years || '31235959'" + vbCrLf '' 2019-02-08 JJH 월별조회시 해당 월만 나오도록

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb031m                                                      " + vbCrLf
                'sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'"
                sSql += "                 WHERE outdt BETWEEN :year || '01000000' AND :years || '31235959'" + vbCrLf '' 2019-02-08 JJH 월별조회시 해당 월만 나오도록

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND rtnflg IN ( '2')                                       " '< 반납은 총출고에서 세지 않는다
                sSql += "               ) a                                                                 " + vbCrLf
                sSql += "             , lb040m b                                                            " + vbCrLf
                sSql += "             , lb043m c                                                            " + vbCrLf
                sSql += "             , lb020m d                                                            " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            " + vbCrLf
                End If

                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                          " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                              " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                              " + vbCrLf
                sSql += "           AND C.STATE IN ('4','6')                                             " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                              " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    'sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn             "
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyyMM'), b.deptcd, b.iogbn             " + vbCrLf
                Else
                    'sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd         "
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyyMM'), a.comcd_out, e.comnmd         " + vbCrLf
                End If

                sSql += "       ) a LEFT OUTER JOIN" + vbCrLf
                'sSql += "       (SELECT fn_ack_date_str(a.outdt, 'MM') as months                            "
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyyMM') as months                            " + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    " + vbCrLf
                    sSql += "         , b.iogbn                                                             " + vbCrLf
                Else
                    sSql += "         , a.comcd_out            as joincd                                    " + vbCrLf
                    sSql += "         , e.comnmd                                                            " + vbCrLf
                End If

                sSql += "             , COUNT(a.bldno)         as qty                                       " + vbCrLf
                sSql += "          FROM lb031m a                                                            " + vbCrLf
                sSql += "             , lb040m b                                                            " + vbCrLf
                sSql += "             , lb043m c                                                            " + vbCrLf
                sSql += "             , lb020m d                                                            " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            " + vbCrLf
                End If

                'sSql += "         WHERE a.outdt BETWEEN :year || '0101000000' AND :year || '1231235959'"
                sSql += "         WHERE a.outdt BETWEEN :year || '01000000' AND :years || '31235959'" + vbCrLf '' 2019-02-08 JJH 월별조회시 해당 월만 나오도록

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd      = c.comcd                                              " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                              " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                              " + vbCrLf

                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                                  " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                                  " + vbCrLf
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                              " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    'sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn             "
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyyMM'), b.deptcd, b.iogbn             " + vbCrLf
                Else
                    'sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd         "
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyyMM'), a.comcd_out, e.comnmd         " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += "    ) b ON a.joincd = b.joincd AND a.months = b.months AND a.iogbn = b.iogbn   " + vbCrLf
                Else
                    sSql += "    ) b On a.joincd = b.joincd AND a.months = b.months AND a.comnmd = b.comnmd " + vbCrLf
                End If
                'sSql += "        ) b ON a.joincd = b.joincd AND a.months = b.months  and a.iogbn = b.iogbn                       "

                If rsGroup = "1"c Then
                    'sSql += "GROUP BY a.joincd, a.iogbn                                                     "
                    sSql += " RIGHT OUTER JOIN vw_ack_ocs_dept_info dept" + vbCrLf
                    sSql += "               ON dept.deptcd = a.joincd " + vbCrLf
                    '20210104 jhs 진료과 항목 'IMG','IMC','IME','IMR','IMN','IMH','IMI','NU','NP','GS','OS','NS','TS' ,'PS','OG','OT','OL','DM','UR','FM','EM','BB' 만 표기 되도록 수정
                    sSql += "   where dept.deptnmd in (SELECt clsval FROM LF000M where clsgbn = 'B23') " + vbCrLf
                    '-------------------------------------------------------------------------------------
                    sSql += " GROUP BY a.joincd  , dept.deptnmd                                                   " + vbCrLf

                Else
                    sSql += "GROUP BY a.joincd, a.comnmd                                                    " + vbCrLf
                End If

                sSql += "UNION ALL                                                                          " + vbCrLf
                sSql += "SELECT '11'                                                       as joincd        " + vbCrLf
                sSql += "     , ' '                                                        as deptsortgbn   " + vbCrLf '20211007 jhs 정렬 추가
                sSql += "     , '총합계 :'                                                 as gbnnm         " + vbCrLf
                sSql += "     , SUM(NVL(a.qty, 0))                                         as sumall        " + vbCrLf
                sSql += "     , SUM(NVL(b.qty, 0))                                         as cnt           " + vbCrLf
                sSql += "     , ROUND(SUM(NVL(b.qty, 0))  * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)    as per           " + vbCrLf
                sSql += "     , '1'                                                        as sortgbn  ,     " + vbCrLf

                '<-- 2019-03-27 지정한 날짜구간에 맞게 추가
                For i As Integer = 1 To a_date.Length

                    sSql += "       SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(a.qty, 0) ELSE 0 END) as am" + i.ToString
                    sSql += "       ,SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(b.qty, 0) ELSE 0 END) as pm" + i.ToString
                    sSql += "     , ROUND(SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                        " + vbCrLf
                    sSql += "            CASE WHEN SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           " + vbCrLf
                    sSql += "                 ELSE SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(a.qty, 0) ELSE 0 END)                      " + vbCrLf
                    sSql += "            END * 100, 2)                                                             as per" + i.ToString

                    If i = a_date.Length Then
                        sSql += ""
                    Else
                        sSql += ","
                    End If
                Next

                'sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'MM') as months                                               "
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyyMM') as months                                               " + vbCrLf
                sSql += "             , COUNT(a.bldno)         as qty                                       " + vbCrLf
                sSql += "          FROM (                                                                   " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb030m                                                      " + vbCrLf
                'sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'"
                sSql += "                 WHERE outdt BETWEEN :year || '01000000' AND :years || '31235959'" + vbCrLf '' 2019-02-08 JJH 월별조회시 해당 월만 나오도록

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt                  " + vbCrLf
                sSql += "                  FROM lb031m                                                      " + vbCrLf
                'sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'"
                sSql += "                 WHERE outdt BETWEEN :year || '01000000' AND :years || '31235959'" + vbCrLf '' 2019-02-08 JJH 월별조회시 해당 월만 나오도록

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND rtnflg  IN ( '2')                                       " + vbCrLf '<반납은 총출고에서 세지 않는다
                sSql += "               ) a                                                                 " + vbCrLf
                sSql += "             , lb040m b                                                            " + vbCrLf
                sSql += "             , lb043m c                                                            " + vbCrLf
                sSql += "             , lb020m d                                                            " + vbCrLf
                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                          " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                              " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                              " + vbCrLf
                sSql += "           AND C.STATE IN ('4','6')                                                " + vbCrLf
                'sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM')                                    "+vbcrlf
                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyyMM')                                 " + vbCrLf

                '20211013 jhs 자체폐기 총합계에 추가 되도록 수정
                If rsGbn = "2"c Then
                    sSql += "          union all                                                                    " + vbCrLf
                    sSql += "          Select  fn_ack_date_str(a.rtndt, 'yyyyMM')                                   " + vbCrLf
                    sSql += "                  , COUNT(a.bldno)         as qty                                      " + vbCrLf
                    sSql += "            from( SELECT bldno, comcd_out, comcd, rtndt                                " + vbCrLf
                    sSql += "                    From lb031m                                                        " + vbCrLf
                    sSql += "                   Where rtndt  BETWEEN :year || '01000000' AND :years || '31235959' " + vbCrLf

                    alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                    alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))

                    sSql += "                     And keepgbn = '5'  " '20211111 jhs 성분제제 조건 추가
                    If rsComcd <> "ALL" Then
                        sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                    '---------------------------
                    sSql += "                                                   )   a                                          " + vbCrLf
                    sSql += "          GROUP BY fn_ack_date_str(a.rtndt, 'yyyyMM')                                  " + vbCrLf
                End If
                '-----------------------------------------------------------------------------

                sSql += "       ) a LEFT OUTER JOIN                                                         " + vbCrLf
                'sSql += "       (SELECT fn_ack_date_str(a.outdt, 'MM') as months                            "+vbcrlf
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyyMM') as months                            " + vbCrLf
                sSql += "             , COUNT(a.bldno)         as qty                                       " + vbCrLf
                sSql += "          FROM lb031m a                                                            " + vbCrLf
                sSql += "             , lb040m b                                                            " + vbCrLf
                sSql += "             , lb043m c                                                            " + vbCrLf
                sSql += "             , lb020m d                                                            " + vbCrLf
                'sSql += "         WHERE a.outdt BETWEEN :year || '0101000000' AND :year || '1231235959'"
                sSql += "         WHERE a.outdt BETWEEN :year || '01000000' AND :years || '31235959'" + vbCrLf '' 2019-02-08 JJH 월별조회시 해당 월만 나오도록

                alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                          " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                              " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                              " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                              " + vbCrLf

                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                                  " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                                  " + vbCrLf
                End If

                'sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM')                                    "
                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyyMM')                                    " + vbCrLf
                sSql += "       ) b ON a.months = b.months                                                " + vbCrLf

                If rsGbn = "2"c Then
                    sSql += "  union all       " + vbCrLf
                    sSql += "  Select '12'                                                        as joincd         " + vbCrLf
                    sSql += "  , ' '                                                              as deptsortgbn    " + vbCrLf
                    sSql += "  , '자체폐기 :'                                                     as gbnnm          " + vbCrLf
                    sSql += "  , SUM(NVL(a.qty, 0))                                                   as sumall         " + vbCrLf ' 자체폐기 여서 총출고는 0
                    sSql += "  , SUM(NVL(0, 0))                                               as cnt            " + vbCrLf
                    sSql += "  , ROUND(SUM(NVL(0, 0))  * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)       as per            " + vbCrLf ' 총출고 0이여서 퍼센트도 0
                    sSql += "  , '3'                                                              as sortgbn ,       " + vbCrLf

                    For i As Integer = 1 To a_date.Length

                        sSql += "      SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(a.qty, 0) ELSE 0 END) as am" + i.ToString + vbCrLf
                        sSql += "     , SUM(Case When a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(0, 0) ELSE 0 END) as pm" + i.ToString + vbCrLf
                        sSql += "     , ROUND(SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(0, 0) ELSE 0 END) * 1.0 /                       " + vbCrLf
                        sSql += "            CASE WHEN SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(0, 0) ELSE 0 END) = 0 THEN 1           " + vbCrLf
                        sSql += "                 ELSE SUM(CASE WHEN a.months = '" + a_date(i - 1).Replace("-", "").Replace(" ", "") + "' THEN NVL(0, 0) ELSE 0 END)                      " + vbCrLf
                        sSql += "            END * 100, 2)                                                             as per" + i.ToString + vbCrLf

                        If i = a_date.Length Then
                            sSql += ""
                        Else
                            sSql += ","
                        End If
                    Next

                    sSql += "  from(      " + vbCrLf
                    sSql += "       Select fn_ack_date_str(rtndt, 'yyyyMM') as months ,  count(bldno) qty  from lb031m       " + vbCrLf
                    sSql += "        where keepgbn = '5'      " + vbCrLf
                    sSql += "          And rtndt  BETWEEN :year || '01000000' AND :years || '31235959'      " + vbCrLf
                    alParm.Add(New OracleParameter("year", OracleDbType.Varchar2, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                    alParm.Add(New OracleParameter("years", OracleDbType.Varchar2, rsYear1.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear1))
                    '20211111 jhs 성분제제 조건 추가
                    If rsComcd <> "ALL" Then
                        sSql += "                   AND comcd_out  = :comcd                                                    " + vbCrLf
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                    '---------------------------
                    sSql += "  group by fn_ack_date_str(rtndt, 'yyyyMM')      " + vbCrLf
                    sSql += "  ) a                       " + vbCrLf
                End If

                'sSql += " ORDER BY sortgbn                                                          "+vbcrlf
                '20211007 jhs 국립에서 정한순서대로 정렬
                'sSql += " ORDER BY sortgbn, joincd                                                          " + vbCrLf
                sSql += " ORDER BY sortgbn, deptsortgbn , joincd                                                          " + vbCrLf
                '-----------------------------------

                DbCommand()

                fn_percentOfRtnBloodM = DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_percentOfRtnBlood(ByVal rsGbn As String, ByVal rsDate As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable
            Dim sFn As String = "fn_percentOfRtnBlood(ByVal rsGbn As String, ByVal rsDate As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                Dim sLastDay As String = Format(DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, CDate(rsDate.Substring(0, 6).Insert(4, "-") + "-01"))), "yyyyMMdd").ToString

                sSql += "SELECT a.joincd                                                            " + vbCrLf
                If rsGroup = "1"c Then
                    sSql += "       , (SELECt clscd FROM LF000M where  clsval = dept.deptnmd  ) as deptsortgbn   " + vbCrLf '20211007 jhs 정렬 추가
                Else
                    sSql += "       , '' as deptsortgbn   " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    'sSql += " , fn_ack_get_dept_name(a.iogbn, a.joincd)                                    as gbnnm    "+vbcrlf
                    'sSql += " , fn_ack_get_dept_code(a.iogbn, a.joincd)                                    as gbnnm    "  +vbcrlf'<<<20180822 부서를 영문으로 표기 요청 
                    ' sSql += " , ( SELECT DEPTNMD FROM VW_ACK_OCS_DEPT_INFO WHERE DEPTCD = a.joincd AND ROWNUM = 1 )  as gbnnm  "+vbcrlf '<<<20180827 
                    sSql += " ,  dept.deptnmd as gbnnm" + vbCrLf
                Else
                    sSql += " , a.comnmd                                                                   as gbnnm    " + vbCrLf
                End If

                sSql += "     , SUM(NVL(a.qty, 0))                                                      as sumall   " + vbCrLf
                sSql += "     , SUM(NVL(b.qty, 0))                                                      as cnt      " + vbCrLf
                ' sSql += "     , ROUND(SUM(NVL(b.qty, 0)) * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)           as per      "+vbcrlf
                sSql += "     , ROUND (SUM (NVL (b.qty, 0)) * 1.0 / SUM (DECODE(a.qty, 0, NULL, a.qty)) * 100, 2) as per" + vbCrLf
                sSql += "     , '2'                                                                     as sortgbn  " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '01' THEN NVL(b.qty, 0) ELSE 0 END)              as d1       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '02' THEN NVL(b.qty, 0) ELSE 0 END)              as d2       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '03' THEN NVL(b.qty, 0) ELSE 0 END)              as d3       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '04' THEN NVL(b.qty, 0) ELSE 0 END)              as d4       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '05' THEN NVL(b.qty, 0) ELSE 0 END)              as d5       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '06' THEN NVL(b.qty, 0) ELSE 0 END)              as d6       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '07' THEN NVL(b.qty, 0) ELSE 0 END)              as d7       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '08' THEN NVL(b.qty, 0) ELSE 0 END)              as d8       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '09' THEN NVL(b.qty, 0) ELSE 0 END)              as d9       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '10' THEN NVL(b.qty, 0) ELSE 0 END)              as d10      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '11' THEN NVL(b.qty, 0) ELSE 0 END)              as d11      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '12' THEN NVL(b.qty, 0) ELSE 0 END)              as d12      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '13' THEN NVL(b.qty, 0) ELSE 0 END)              as d13      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '14' THEN NVL(b.qty, 0) ELSE 0 END)              as d14      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '15' THEN NVL(b.qty, 0) ELSE 0 END)              as d15      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '16' THEN NVL(b.qty, 0) ELSE 0 END)              as d16      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '17' THEN NVL(b.qty, 0) ELSE 0 END)              as d17      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '18' THEN NVL(b.qty, 0) ELSE 0 END)              as d18      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '19' THEN NVL(b.qty, 0) ELSE 0 END)              as d19      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '20' THEN NVL(b.qty, 0) ELSE 0 END)              as d20      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '21' THEN NVL(b.qty, 0) ELSE 0 END)              as d21      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '22' THEN NVL(b.qty, 0) ELSE 0 END)              as d22      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '23' THEN NVL(b.qty, 0) ELSE 0 END)              as d23      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '24' THEN NVL(b.qty, 0) ELSE 0 END)              as d24      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '25' THEN NVL(b.qty, 0) ELSE 0 END)              as d25      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '26' THEN NVL(b.qty, 0) ELSE 0 END)              as d26      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '27' THEN NVL(b.qty, 0) ELSE 0 END)              as d27      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '28' THEN NVL(b.qty, 0) ELSE 0 END)              as d28      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '29' THEN NVL(b.qty, 0) ELSE 0 END)              as d29      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '30' THEN NVL(b.qty, 0) ELSE 0 END)              as d30      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '31' THEN NVL(b.qty, 0) ELSE 0 END)              as d31      " + vbCrLf
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'DD') as days                              " + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                            " + vbCrLf
                    sSql += "         , b.iogbn                                                     " + vbCrLf
                Else
                    sSql += "         , c.comcd_out            as joincd                            " + vbCrLf
                    sSql += "         , e.comnmd                                                    " + vbCrLf
                End If

                sSql += "             , COUNT(a.bldno)         as qty                               " + vbCrLf
                sSql += "          FROM (" + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt          " + vbCrLf
                sSql += "                  FROM lb030m                                              " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                     " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt          " + vbCrLf
                sSql += "                  FROM lb031m                                              " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'" + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                 " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND rtnflg IN ( '2')                                " + vbCrLf
                sSql += "               ) a                                                         " + vbCrLf
                sSql += "             , lb040m b                                                    " + vbCrLf
                sSql += "             , lb043m c                                                    " + vbCrLf
                sSql += "             , lb020m d                                                    " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                    " + vbCrLf
                End If

                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                 " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                  " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                      " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                      " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                      " + vbCrLf
                sSql += "           AND C.STATE IN ('4','6')                                             " + vbCrLf

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                      " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                      " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), b.deptcd, b.iogbn     " + vbCrLf
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), c.comcd_out, e.comnmd " + vbCrLf
                End If
                sSql += "       ) a LEFT OUTER JOIN                                                 " + vbCrLf
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'DD') as days                      " + vbCrLf

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                            " + vbCrLf
                    sSql += "         , b.iogbn                                                     " + vbCrLf
                Else
                    sSql += "         , a.comcd_out            as joincd                            " + vbCrLf
                    sSql += "         , e.comnmd                                                    " + vbCrLf
                End If

                sSql += "             , COUNT(a.bldno)         as qty                               " + vbCrLf
                sSql += "          FROM lb031m a                                                    " + vbCrLf
                sSql += "             , lb040m b                                                    " + vbCrLf
                sSql += "             , lb043m c                                                    " + vbCrLf
                sSql += "             , lb020m d                                                    " + vbCrLf


                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                    " + vbCrLf
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates AND :datee || '235959'                                  " + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                            " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                 " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                 " + vbCrLf
                sSql += "           AND a.comcd      = c.comcd                                      " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                      " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                      " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                      " + vbCrLf

                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                          " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                          " + vbCrLf
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                      " + vbCrLf
                    sSql += "       AND c.spccd      = e.spccd                                      " + vbCrLf
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), b.deptcd, b.iogbn     " + vbCrLf
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), a.comcd_out, e.comnmd " + vbCrLf
                End If
                sSql += "       ) b ON a.joincd = b.joincd AND a.days   = b.days                    " + vbCrLf

                If rsGroup = "1"c Then
                    'sSql += " GROUP BY a.joincd, a.iogbn                                             "
                    sSql += " RIGHT OUTER JOIN VW_ACK_OCS_DEPT_INFO dept" + vbCrLf '2019-04-22 진료과별일 경우 진료과별로 건수가 0이라도 모두 표시되도록 수정 요청
                    sSql += "               ON dept.deptcd = a.joincd" + vbCrLf
                    '20210104 jhs 진료과 항목 'IMG','IMC','IME','IMR','IMN','IMH','IMI','NU','NP','GS','OS','NS','TS' ,'PS','OG','OT','OL','DM','UR','FM','EM','BB' 만 표기 되도록 수정
                    sSql += "   where dept.deptnmd in (SELECt clsval FROM LF000M where clsgbn = 'B23') " + vbCrLf
                    '-------------------------------------------------------------------------------------
                    sSql += " GROUP BY a.joincd , dept.deptnmd                                      " + vbCrLf
                Else
                    sSql += " GROUP BY a.joincd, a.comnmd                                            " + vbCrLf
                End If

                sSql += "UNION ALL                                                                  " + vbCrLf
                sSql += "SELECT '11'                                                                   as joincd   " + vbCrLf
                sSql += "     , ' '                                                                    as deptsortgbn " + vbCrLf '20211007 jhs 정렬 추가
                sSql += "     , '총합계 :'                                                             as gbnnm    " + vbCrLf
                sSql += "     , SUM(NVL(a.qty, 0))                                                     as sumall   " + vbCrLf
                sSql += "     , SUM(NVL(b.qty, 0))                                                     as cnt      " + vbCrLf
                sSql += "     , ROUND(SUM(NVL(b.qty, 0)) * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)          as per      " + vbCrLf
                sSql += "     , '1'                                                                    as sortgbn  " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '01' THEN NVL(b.qty, 0) ELSE 0 END)             as d1       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '02' THEN NVL(b.qty, 0) ELSE 0 END)             as d2       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '03' THEN NVL(b.qty, 0) ELSE 0 END)             as d3       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '04' THEN NVL(b.qty, 0) ELSE 0 END)             as d4       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '05' THEN NVL(b.qty, 0) ELSE 0 END)             as d5       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '06' THEN NVL(b.qty, 0) ELSE 0 END)             as d6       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '07' THEN NVL(b.qty, 0) ELSE 0 END)             as d7       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '08' THEN NVL(b.qty, 0) ELSE 0 END)             as d8       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '09' THEN NVL(b.qty, 0) ELSE 0 END)             as d9       " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '10' THEN NVL(b.qty, 0) ELSE 0 END)             as d10      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '11' THEN NVL(b.qty, 0) ELSE 0 END)             as d11      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '12' THEN NVL(b.qty, 0) ELSE 0 END)             as d12      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '13' THEN NVL(b.qty, 0) ELSE 0 END)             as d13      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '14' THEN NVL(b.qty, 0) ELSE 0 END)             as d14      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '15' THEN NVL(b.qty, 0) ELSE 0 END)             as d15      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '16' THEN NVL(b.qty, 0) ELSE 0 END)             as d16      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '17' THEN NVL(b.qty, 0) ELSE 0 END)             as d17      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '18' THEN NVL(b.qty, 0) ELSE 0 END)             as d18      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '19' THEN NVL(b.qty, 0) ELSE 0 END)             as d19      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '20' THEN NVL(b.qty, 0) ELSE 0 END)             as d20      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '21' THEN NVL(b.qty, 0) ELSE 0 END)             as d21      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '22' THEN NVL(b.qty, 0) ELSE 0 END)             as d22      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '23' THEN NVL(b.qty, 0) ELSE 0 END)             as d23      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '24' THEN NVL(b.qty, 0) ELSE 0 END)             as d24      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '25' THEN NVL(b.qty, 0) ELSE 0 END)             as d25      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '26' THEN NVL(b.qty, 0) ELSE 0 END)             as d26      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '27' THEN NVL(b.qty, 0) ELSE 0 END)             as d27      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '28' THEN NVL(b.qty, 0) ELSE 0 END)             as d28      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '29' THEN NVL(b.qty, 0) ELSE 0 END)             as d29      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '30' THEN NVL(b.qty, 0) ELSE 0 END)             as d30      " + vbCrLf
                sSql += "     , SUM(CASE WHEN a.days = '31' THEN NVL(b.qty, 0) ELSE 0 END)             as d31      " + vbCrLf
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'DD') as days                              " + vbCrLf
                sSql += "             , COUNT(a.bldno)         as qty                               " + vbCrLf
                '20211111 jhs 추가
                'If rsGbn = "2" Then
                'sSql += "             , b.deptcd as joincd                                          " + vbCrLf
                ' End If
                '-----------------------
                sSql += "          FROM (SELECT bldno, comcd_out, comcd, outdt                      " + vbCrLf
                sSql += "                  FROM lb030m                                              " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'                            " + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                     " + vbCrLf
                sSql += "                SELECT bldno, comcd_out, comcd, outdt          " + vbCrLf
                sSql += "                  FROM lb031m                                              " + vbCrLf
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'             " + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                               " + vbCrLf
                    alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND rtnflg IN ('2')                               " + vbCrLf
                sSql += "               ) a                                                         " + vbCrLf
                sSql += "             , lb040m b                                                    " + vbCrLf
                sSql += "             , lb043m c                                                    " + vbCrLf
                sSql += "             , lb020m d                                                    " + vbCrLf
                sSql += "         WHERE b.tnsjubsuno = c.tnsjubsuno                                 " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                      " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                  " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                      " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                      " + vbCrLf
                sSql += "           AND c.state      in ( '4', '6')                                 " + vbCrLf
                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD')  , b.deptcd                " + vbCrLf

                '20211013 jhs 총합계 자체폐기 내용 추가
                If rsGbn = "2" Then

                    sSql += "        union all                                                          " + vbCrLf
                    sSql += "        Select fn_ack_date_str(a.rtndt, 'DD') as days                      " + vbCrLf
                    sSql += "               , COUNT(a.bldno)         as qty                             " + vbCrLf
                    ' sSql += "               , '9999999999' as joincd                                    " + vbCrLf
                    sSql += "        from ( SELECT bldno, comcd_out, comcd, rtndt                       " + vbCrLf
                    sSql += "                 From lb031m                                               " + vbCrLf
                    sSql += "                Where rtndt BETWEEN :dates And : datee || '235959'         " + vbCrLf

                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                    sSql += "                  And keepgbn = '5'                                         " + vbCrLf
                    '20211111 jhs 성분제제 조건 항목 추가
                    If rsComcd <> "ALL" Then
                        sSql += "                   AND comcd_out  = :comcd                               " + vbCrLf
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                    '-----------------------------
                    sSql += "                                    )   a                                 " + vbCrLf
                    sSql += "                GROUP BY fn_ack_date_str(a.rtndt, 'DD')                    " + vbCrLf
                End If
                '-----------------------------------------------------------------------------------------

                sSql += "       ) a LEFT OUTER JOIN                                                 " + vbCrLf
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'DD') as days                      " + vbCrLf
                sSql += "             , COUNT(a.bldno)         as qty                               " + vbCrLf
                sSql += "          FROM lb031m a                                                    " + vbCrLf
                sSql += "             , lb040m b                                                    " + vbCrLf
                'sSql += "             , lb043m_temp c                                                    "+vbcrlf
                sSql += "             , lb043m c                                                    " + vbCrLf
                sSql += "             , lb020m d                                                    " + vbCrLf
                sSql += "         WHERE a.outdt BETWEEN :dates AND :datee || '235959'                           " + vbCrLf

                alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                 " + vbCrLf
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                 " + vbCrLf
                sSql += "           AND a.comcd_out  = c.comcd_out                                  " + vbCrLf
                sSql += "           AND a.bldno      = c.bldno                                      " + vbCrLf
                sSql += "           AND a.bldno      = d.bldno                                      " + vbCrLf
                sSql += "           AND a.comcd_out  = d.comcd                                      " + vbCrLf

                If rsGbn = "1"c Then
                    sSql += "       AND a.rtnflg     = '1'                                          " + vbCrLf
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND a.rtnflg     = '2'                                          " + vbCrLf
                End If
                sSql += "         GROUP BY fn_ack_date_str(a.outdt, 'DD')                           " + vbCrLf
                sSql += "       ) b ON a.days   = b.days                                            " + vbCrLf

                '20211012 jhs 자체폐기 추가
                If rsGbn = "2" Then

                    sSql += " union all " + vbCrLf
                    sSql += " Select '12'                                               as joincd   " + vbCrLf
                    sSql += " , ' '                                                          as deptsortgbn " + vbCrLf
                    sSql += " , '자체폐기 :'                                              as gbnnm    " + vbCrLf
                    sSql += " , SUM(NVL(a.qty, 0))                                 as sumall   " + vbCrLf
                    sSql += " , SUM(NVL(a.qty, 0))                                 as cnt      " + vbCrLf
                    sSql += " ,   0             as per      " + vbCrLf
                    sSql += " ,   '3'           as sortgbn  " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '01' THEN NVL(a.qty, 0) ELSE 0 END)          as d1       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '02' THEN NVL(a.qty, 0) ELSE 0 END)          as d2       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '03' THEN NVL(a.qty, 0) ELSE 0 END)          as d3       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '04' THEN NVL(a.qty, 0) ELSE 0 END)          as d4       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '05' THEN NVL(a.qty, 0) ELSE 0 END)          as d5       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '06' THEN NVL(a.qty, 0) ELSE 0 END)          as d6       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '07' THEN NVL(a.qty, 0) ELSE 0 END)          as d7       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '08' THEN NVL(a.qty, 0) ELSE 0 END)          as d8       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '09' THEN NVL(a.qty, 0) ELSE 0 END)          as d9       " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '10' THEN NVL(a.qty, 0) ELSE 0 END)          as d10      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '11' THEN NVL(a.qty, 0) ELSE 0 END)          as d11      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '12' THEN NVL(a.qty, 0) ELSE 0 END)          as d12      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '13' THEN NVL(a.qty, 0) ELSE 0 END)          as d13      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '14' THEN NVL(a.qty, 0) ELSE 0 END)          as d14      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '15' THEN NVL(a.qty, 0) ELSE 0 END)          as d15      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '16' THEN NVL(a.qty, 0) ELSE 0 END)          as d16      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '17' THEN NVL(a.qty, 0) ELSE 0 END)          as d17      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '18' THEN NVL(a.qty, 0) ELSE 0 END)          as d18      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '19' THEN NVL(a.qty, 0) ELSE 0 END)          as d19      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '20' THEN NVL(a.qty, 0) ELSE 0 END)          as d20      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '21' THEN NVL(a.qty, 0) ELSE 0 END)          as d21      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '22' THEN NVL(a.qty, 0) ELSE 0 END)          as d22      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '23' THEN NVL(a.qty, 0) ELSE 0 END)          as d23      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '24' THEN NVL(a.qty, 0) ELSE 0 END)          as d24      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '25' THEN NVL(a.qty, 0) ELSE 0 END)          as d25      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '26' THEN NVL(a.qty, 0) ELSE 0 END)          as d26      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '27' THEN NVL(a.qty, 0) ELSE 0 END)          as d27      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '28' THEN NVL(a.qty, 0) ELSE 0 END)          as d28      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '29' THEN NVL(a.qty, 0) ELSE 0 END)          as d29      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '30' THEN NVL(a.qty, 0) ELSE 0 END)          as d30      " + vbCrLf
                    sSql += " ,  SUM(CASE WHEN a.days = '31' THEN NVL(a.qty, 0) ELSE 0 END)          as d31     " + vbCrLf
                    sSql += "  from(" + vbCrLf
                    sSql += "      selecT fn_ack_date_str(rtndt, 'DD') as days ,  count(bldno) qty  from lb031m " + vbCrLf
                    sSql += "       where keepgbn = '5'" + vbCrLf
                    sSql += "         And rtndt BETWEEN :dates AND :datee || '235959'" + vbCrLf
                    alParm.Add(New OracleParameter("dates", OracleDbType.Varchar2, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                    alParm.Add(New OracleParameter("datee", OracleDbType.Varchar2, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))
                    '20211111 jhs 성분제제 조건 항목 추가
                    If rsComcd <> "ALL" Then
                        sSql += "                   AND comcd_out  = :comcd                               " + vbCrLf
                        alParm.Add(New OracleParameter("comcd", OracleDbType.Varchar2, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If
                    '-----------------------------
                    sSql += "       group by fn_ack_date_str(rtndt, 'DD') " + vbCrLf
                    sSql += "      ) a                 " + vbCrLf
                End If
                '-------------------------------------------------------------------------
                '20211007 jhs 요청한 내용으로 정렬되도록 변경
                'sSql += " ORDER BY sortgbn  , joincd                                                  " + vbCrLf
                sSql += " ORDER BY sortgbn  , deptsortgbn , joincd                                                  " + vbCrLf
                '-----------------------------------

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액자체폐기 "
        Public Shared Function fn_OverAvailyBloodList() As DataTable
            ' 유효기간이 지난 혈액
            Dim sFn As String = "Public Shared Function fn_OverAvailyBloodList() As DataTable"
            Dim sSql As String = ""

            Try
                sSql += "Select fn_ack_get_bldno_full(a.bldno)                  As vbldno  "
                sSql += "     , b.comnmd                                        As comnmd  "
                sSql += "     , '미출고'                                        as state   "
                sSql += "     , a.abo || a.rh                                   as aborh   "
                sSql += "     , fn_ack_date_str(a.indt, 'YYYY-MM-DD HH24:MI:SS')    as indt    "
                sSql += "     , fn_ack_date_str(a.dondt, 'YYYY-MM-DD HH24:MI:SS')   as dondt   "
                sSql += "     , fn_ack_date_str(a.availdt, 'YYYY-MM-DD HH24:MI:SS') as availdt "
                sSql += "     , a.bldno                                                "
                sSql += "     , a.comcd                                                "
                sSql += "  FROM lb020m a,                                              "
                sSql += "       lf120m b                                               "
                sSql += " WHERE a.comcd    = b.comcd                                   "
                sSql += "   AND a.availdt  < fn_ack_sysdate                            "
                sSql += "   AND a.state    = '0'                                       "
                sSql += "ORDER BY a.bldno, a.comcd                                     "

                DbCommand()
                Return DbExecuteQuery(sSql)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_AbnBloodSearch(ByVal rsBldno As String) As DataTable
            '혈액조회
            Dim sFn As String = "Public Shared Function fn_AbnBloodSearch(ByVal rsBldno As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_get_bldno_full(a.bldno)                      as vbldno  "
                sSql += "     , b.comnmd                                        as comnmd  "
                sSql += "     , CASE WHEN a.state = '0' THEN '미출고' WHEN a.state = '1' THEN '접수'"
                sSql += "            WHEN a.state = '2' THEN '검사중' WHEN a.state = '3' THEN '가출고'"
                sSql += "            WHEN a.state = '4' THEN '반납'   WHEN a.state = '6' THEN '폐기'"
                sSql += "       END as state"
                sSql += "     , a.abo || a.rh                                   as aborh   "
                sSql += "     , fn_ack_date_str(a.indt, 'yyyy-mm-dd hh24:mi:ss')    as indt    "
                sSql += "     , fn_ack_date_str(a.dondt, 'yyyy-mm-dd hh24:mi:ss')   as dondt   "
                sSql += "     , fn_ack_date_str(a.availdt, 'yyyy-mm-dd hh24:mi:ss') as availdt "
                sSql += "     , a.bldno                                                "
                sSql += "     , a.comcd                                                "
                sSql += "     , CASE WHEN SYSDATE - TO_DATE(a.availdt, 'yyyymmddhh24miss') > 0 THEN '1'"
                sSql += "            ELSE '0'                                          "
                sSql += "       END                                             as chkgbn  "
                sSql += "  FROM lb020m a,                                              "
                sSql += "       lf120m b                                               "
                sSql += " WHERE a.comcd    = b.comcd                                   "
                sSql += "   AND NVL(a.state, ' ') IN (' ', '0')                       "
                sSql += "   AND a.bldno    = :bldno                                         "

                alParm.Add(New OracleParameter("bldno", rsBldno))

                DbCommand()

                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

    End Class

    Public Class TnsReg
        Inherits SqlFn

        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.KeepReg" + vbTab
        Private m_DbCn As OracleConnection
        Private m_DbTrans As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_DbTrans = m_DbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        '202111125 jhs 적혈구제제 수정
        '적혈구제제 수정 함수
        Public Function fn_trans_mgt_upd(ByVal rsArTrans As TnsTranList) As Boolean
            Dim sFn As String = "Public Shared Function fn_trans_mgt_upd(ByVal rsTnsNo As String) As Boolean"
            Dim DbCmd As New OracleCommand
            Dim lb_rtnValue As Boolean = False
            Dim sSql As String = ""
            Dim intRet As Integer

            Dim sTnsjubsuno As String = ""
            Dim sRegno As String = ""
            Dim sHgyn As String = ""
            Dim sAllyn As String = ""
            Dim sCbcyn As String = ""
            Dim sEcpyn As String = ""
            Dim sSeq As String = ""
            Dim sUpdid As String = ""
            Dim sCmcaller As String = ""

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try

                With rsArTrans
                    sTnsjubsuno = .TNSJUBSUNO
                    sRegno = .REGNO
                    sHgyn = .HGYN
                    sAllyn = .ALLYN
                    sCbcyn = .CBCYN
                    sEcpyn = .ECPYN
                    sCmcaller = .CMCALLER
                    sSeq = .SEQ
                End With

                sSql += "update lbc10m set                  " + vbCrLf
                sSql += "         hgyn = :hgyn,             " + vbCrLf
                sSql += "        cbcyn = :cbcyn,            " + vbCrLf
                sSql += "        allyn = :allyn,            " + vbCrLf
                sSql += "       ecptyn = :ecptyn,           " + vbCrLf
                sSql += "        upddt = fn_ack_sysdate(),  " + vbCrLf
                sSql += "        updid = :updid  ,           " + vbCrLf
                sSql += "     cmcaller = :cmcaller          " + vbCrLf
                sSql += "where tnsjubsuno = :tnsjubsuno     " + vbCrLf
                sSql += "  and regno      = :regno          " + vbCrLf
                sSql += "  And seq        = :seq            " + vbCrLf

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("hgyn", OracleDbType.Varchar2).Value = sHgyn
                DbCmd.Parameters.Add("cbcyn", OracleDbType.Varchar2).Value = sCbcyn
                DbCmd.Parameters.Add("allyn", OracleDbType.Varchar2).Value = sAllyn
                DbCmd.Parameters.Add("ecptyn", OracleDbType.Varchar2).Value = sEcpyn
                DbCmd.Parameters.Add("updid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("cmcaller", OracleDbType.Varchar2).Value = sCmcaller
                DbCmd.Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = sTnsjubsuno
                DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = sRegno
                DbCmd.Parameters.Add("seq", OracleDbType.Varchar2).Value = sSeq

                intRet = DbCmd.ExecuteNonQuery()

                If intRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()

                lb_rtnValue = True

                Return lb_rtnValue

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function
        '------------------------------------
        '202111125 jhs 적혈구제제 수정
        '적혈구제제 삭제 함수
        Public Function fn_trans_mgt_del(ByVal rsArTrans As TnsTranList) As Boolean
            Dim sFn As String = "Public Shared Function fnGet_trans_mgt_upd(ByVal rsTnsNo As String) As Boolean"
            Dim DbCmd As New OracleCommand
            Dim lb_rtnValue As Boolean = False
            Dim sSql As String = ""
            Dim intRet As Integer

            Dim sTnsjubsuno As String = ""
            Dim sRegno As String = ""
            Dim sSeq As String = ""

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try

                With rsArTrans
                    sTnsjubsuno = .TNSJUBSUNO
                    sRegno = .REGNO
                    sSeq = .SEQ
                End With

                sSql += "delete lbc10m                   " + vbCrLf
                sSql += "where tnsjubsuno = :tnsjubsuno     " + vbCrLf
                sSql += "  and regno      = :regno          " + vbCrLf
                sSql += "  And seq        = :seq            " + vbCrLf

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = sTnsjubsuno
                DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = sRegno
                DbCmd.Parameters.Add("seq", OracleDbType.Varchar2).Value = sSeq

                intRet = DbCmd.ExecuteNonQuery()

                If intRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()

                lb_rtnValue = True

                Return lb_rtnValue

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function
        '------------------------------------
        '202111125 jhs 적혈구제제 수정
        '혈액 TAT 병실/이형수혈 입력 
        Public Function fn_BldTat_Input_Upd(ByVal rsBldTatInput As BldTatInput) As Boolean
            Dim sFn As String = "Public Shared Function fn_BldTat_Input_Upd(ByVal rsTnsNo As String) As Boolean"
            Dim DbCmd As New OracleCommand
            Dim sSql As String = ""
            Dim intRet As Integer

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try

                sSql = ""
                sSql += "delete lbc20m                  " + vbCrLf
                sSql += "where tnsjubsuno = :tnsjubsuno " + vbCrLf
                sSql += "  and regno = :regno           " + vbCrLf
                sSql += "  and bldno = :bldno           " + vbCrLf

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = rsBldTatInput.TNSJUBSUNO
                DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = rsBldTatInput.REGNO
                DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = rsBldTatInput.BLDNO

                intRet = DbCmd.ExecuteNonQuery()

                sSql = ""
                sSql += "insert into lbc20m ( tnsjubsuno, regno, regdt           ,  regid,  gwa,  varyn,  bldno)" + vbCrLf
                sSql += "            values (:tnsjubsuno,:regno, fn_ack_sysdate(), :regid, :gwa, :varyn, :bldno) " + vbCrLf

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = rsBldTatInput.TNSJUBSUNO
                DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = rsBldTatInput.REGNO
                DbCmd.Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("gwa", OracleDbType.Varchar2).Value = rsBldTatInput.GWA
                DbCmd.Parameters.Add("varyn", OracleDbType.Varchar2).Value = rsBldTatInput.VARYN
                DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = rsBldTatInput.BLDNO

                intRet = DbCmd.ExecuteNonQuery()

                If intRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()

                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function

        '2022.06.22 JJH 다건 입력
        Public Function fn_BldTat_Input_Upd(ByVal rsBldTatInputList As List(Of BldTatInput)) As Boolean
            Dim sFn As String = "Public Shared Function fn_BldTat_Input_Upd(ByVal rsTnsNo As String) As Boolean"
            Dim DbCmd As New OracleCommand
            Dim sSql As String = ""
            Dim intRet As Integer

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try

                For Each rsBldTatInput As BldTatInput In rsBldTatInputList

                    sSql = ""
                    sSql += "delete lbc20m                  " + vbCrLf
                    sSql += "where tnsjubsuno = :tnsjubsuno " + vbCrLf
                    sSql += "  and regno = :regno           " + vbCrLf
                    sSql += "  and bldno = :bldno           " + vbCrLf

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = rsBldTatInput.TNSJUBSUNO
                    DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = rsBldTatInput.REGNO
                    DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = rsBldTatInput.BLDNO

                    intRet = DbCmd.ExecuteNonQuery()

                    sSql = ""
                    sSql += "insert into lbc20m ( tnsjubsuno, regno, regdt           ,  regid,  gwa,  varyn,  bldno)" + vbCrLf
                    sSql += "            values (:tnsjubsuno,:regno, fn_ack_sysdate(), :regid, :gwa, :varyn, :bldno) " + vbCrLf

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = rsBldTatInput.TNSJUBSUNO
                    DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = rsBldTatInput.REGNO
                    DbCmd.Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("gwa", OracleDbType.Varchar2).Value = rsBldTatInput.GWA
                    DbCmd.Parameters.Add("varyn", OracleDbType.Varchar2).Value = rsBldTatInput.VARYN
                    DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = rsBldTatInput.BLDNO

                    intRet = DbCmd.ExecuteNonQuery()

                    If intRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If
                Next

                m_DbTrans.Commit()

                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function
        '혈액 TAT 병실/이형수혈 입력 
        Public Function fn_BldTat_Input_Del(ByVal rsBldTatInput As BldTatInput) As Boolean
            Dim sFn As String = "Public Shared Function fn_BldTat_Input_Del(ByVal rsTnsNo As String) As Boolean"
            Dim dt As DataTable
            Dim alParm As New ArrayList
            Dim DbCmd As New OracleCommand
            Dim lb_rtnValue As Boolean = False
            Dim sSql As String = ""
            Dim intRet As Integer

            Dim sTnsjubsuno As String = ""
            Dim sRegno As String = ""
            Dim sGwa As String = ""
            Dim sBldno As String = ""
            Dim sVarYN As String = ""

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                With rsBldTatInput
                    sTnsjubsuno = .TNSJUBSUNO
                    sRegno = .REGNO
                    sGwa = .GWA
                    sBldno = .BLDNO
                    sVarYN = .VARYN
                End With

                sSql = ""
                sSql += "delete lbc20m " + vbCrLf
                sSql += " where tnsjubsuno = :tnsjubsuno " + vbCrLf
                sSql += "   and regno      = :regno " + vbCrLf
                sSql += "   and bldno      = :bldno " + vbCrLf

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("tnsjubsuno", OracleDbType.Varchar2).Value = sTnsjubsuno
                DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = sRegno
                DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = sBldno

                intRet = DbCmd.ExecuteNonQuery()

                If intRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()
                lb_rtnValue = True
                Return lb_rtnValue
            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally
                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing
                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try
        End Function
        '------------------------------------

        Private Function fnGet_Sysdate() As String
            Dim sFn As String = "Private Function fnGet_Sysdate() As String"
            Dim DbCmd As New OracleCommand
            Dim dbDa As OracleDataAdapter
            Dim dt As New DataTable

            Try
                Dim sSql As String = ""

                sSql = ""
                sSql += "SELECT fn_ack_sysdate FROM DUAL"

                DbCmd.Connection = m_DbCn
                DbCmd.Transaction = m_DbTrans
                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                dbDa = New OracleDataAdapter(DbCmd)

                With dbDa
                    .SelectCommand.Parameters.Clear()
                End With

                dt.Reset()
                dbDa.Fill(dt)

                If dt.Rows.Count < 1 Then
                    Return Format(Now, "yyyyMMddHHmmss").ToString
                Else
                    Return dt.Rows(0).Item(0).ToString
                End If
            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Return Format(Now, "yyyyMMddHHmmss").ToString

            End Try

        End Function

        ' 보관검체 입력
        Public Function fn_UpdKeepNo(ByVal rsTnsnum As String, ByVal rsKeepNo As String, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fn_UpdKeepNo(ByVal rsTnsnum As String, ByVal rsGbn As String) As Boolean"

            Dim DbCmd As New OracleCommand
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                sSql = ""
                sSql = fnGet_UpdBcnoBlood(rsGbn)

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("bcno", OracleDbType.Varchar2).Value = rsKeepNo
                DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = rsTnsnum

                iRet = DbCmd.ExecuteNonQuery()

                If iRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If
                m_DbTrans.Commit()

                Return True

            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        ' 의뢰검체, 보관검체 등록 
        Public Function fn_UpdBcnoBlood(ByVal rsTnsNum As String, ByVal rsOrderNum As String, ByVal rsKeepNum As String, ByVal rsRegno As String, ByVal rsColldt As String, ByVal rsAbo As String, ByVal rsRh As String) As Boolean
            Dim sFn As String = "Public Function fn_CrossCancel(ByVal ral_arg As ArrayList) As Boolean"
            Dim DbCmd As New OracleCommand
            Dim lb_rtnValue As Boolean = False
            Dim sSql As String = ""
            Dim intRet As Integer
            Dim sUeDt As String = rsColldt

            If IsDate(sUeDt) Then
                sUeDt = Format(DateAdd(DateInterval.Day, 3, CDate(sUeDt)), "yyyyMMddHHmmss").ToString
            End If

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                sSql = ""
                sSql = fnGet_UpdBcnoBlood("UPD")

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("bcno_o", OracleDbType.Varchar2).Value = rsOrderNum
                DbCmd.Parameters.Add("bcno_k", OracleDbType.Varchar2).Value = rsKeepNum
                DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = rsTnsNum

                intRet = DbCmd.ExecuteNonQuery()

                If intRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                sSql = ""
                sSql = fnGet_InsLB080M()

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("keepspcno", OracleDbType.Varchar2).Value = rsKeepNum
                DbCmd.Parameters.Add("regno", OracleDbType.Varchar2).Value = rsRegno
                DbCmd.Parameters.Add("ustm", OracleDbType.Varchar2).Value = rsColldt.Replace("-"c, "").Replace(":"c, "").Replace(" ", "")
                DbCmd.Parameters.Add("uetm", OracleDbType.Varchar2).Value = sUeDt.Replace("-"c, "").Replace(":"c, "").Replace(" ", "")
                DbCmd.Parameters.Add("bloodtyp", OracleDbType.Varchar2).Value = rsAbo + rsRh
                DbCmd.Parameters.Add("abo", OracleDbType.Varchar2).Value = rsAbo
                DbCmd.Parameters.Add("rh", OracleDbType.Varchar2).Value = rsRh
                DbCmd.Parameters.Add("keepbcno", OracleDbType.Varchar2).Value = rsOrderNum

                intRet = DbCmd.ExecuteNonQuery()

                If intRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()
                lb_rtnValue = True

                Return lb_rtnValue
            Catch ex As Exception
                m_DbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        ' 보관검체, 의뢰검체 제거
        Public Function fn_DelBcnoBlood(ByVal rsTnsnum As String, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fn_DelBcnoBlood(ByVal rsTnsnum As String, ByVal rsGbn As String) As Boolean"
            Dim DbCmd As New OracleCommand
            Dim lb_rtnValue As Boolean = False
            Dim sSql As String = ""
            Dim intRet As Integer

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                sSql = ""
                sSql = fnGet_UpdBcnoBlood(rsGbn)

                DbCmd.CommandType = CommandType.Text
                DbCmd.CommandText = sSql

                DbCmd.Parameters.Clear()
                DbCmd.Parameters.Add("bcno", OracleDbType.Varchar2).Value = ""
                DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = rsTnsnum

                intRet = DbCmd.ExecuteNonQuery()

                If intRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If
                m_DbTrans.Commit()
                lb_rtnValue = True

                Return lb_rtnValue
            Catch ex As Exception
                m_DbTrans.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message, ex))
                Return False
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        ' 혈액보관
        Public Function fn_KeepExe(ByVal ralArg As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fn_KeepExe(ByVal ralArg As ArrayList) As Boolean"
            Dim DbCmd As New OracleCommand
            Dim lb_rtnValue As Boolean = False
            Dim sSql As String = ""
            Dim intRet As Integer

            Dim ls_tnsNum As String
            Dim ls_comcd As String
            Dim ls_bldno As String
            Dim ls_keepdt As String = fnGet_Sysdate()
            Dim ls_recid As String
            Dim ls_recnm As String

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                For i As Integer = 0 To ralArg.Count - 1
                    With CType(ralArg(i), STU_TnsJubsu)
                        ls_tnsNum = .TNSJUBSUNO
                        ls_comcd = .COMCD
                        ls_bldno = .BLDNO
                        ls_recid = .RECID
                        ls_recnm = .RECNM
                    End With

                    ' lb030h insert
                    sSql = ""
                    sSql = fnGet_InsLB030HSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("modid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("modip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = ls_bldno
                    DbCmd.Parameters.Add("comcdout", OracleDbType.Varchar2).Value = ls_comcd
                    DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = ls_tnsNum

                    intRet = DbCmd.ExecuteNonQuery()

                    If intRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb030m update - keepgbn
                    sSql = fnGet_UpdLB030MSql("K"c, rsGbn)
                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()

                    DbCmd.Parameters.Add("keepgbn", OracleDbType.Varchar2).Value = rsGbn
                    DbCmd.Parameters.Add("keepid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("keeptm", OracleDbType.Varchar2).Value = ls_keepdt

                    If rsGbn = "2"c Then
                        DbCmd.Parameters.Add("outid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("outdt", OracleDbType.Varchar2).Value = ls_keepdt
                        DbCmd.Parameters.Add("recid", OracleDbType.Varchar2).Value = ls_recid
                        DbCmd.Parameters.Add("rednm", OracleDbType.Varchar2).Value = ls_recnm
                    End If

                    DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = ls_bldno
                    DbCmd.Parameters.Add("comcdout", OracleDbType.Varchar2).Value = ls_comcd
                    DbCmd.Parameters.Add("tnsno", OracleDbType.Varchar2).Value = ls_tnsNum

                    intRet = DbCmd.ExecuteNonQuery()

                    If intRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If
                Next

                m_DbTrans.Commit()
                lb_rtnValue = True

                Return lb_rtnValue
            Catch ex As Exception
                m_DbTrans.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function


        ' 크로스매칭 결과저장
        Public Function fn_CrossSaveSecond(ByVal ralArg As ArrayList) As Boolean
            Dim sFn As String = "Public Function fn_CrossSaveSecond(ByVal ral_arg As ArrayList) As Boolean"
            Dim DbCmd As New OracleCommand
            'Dim dbDa As oracleDataAdapter
            Dim lb_rtnValue As Boolean = False
            Dim sSql As String = ""
            Dim intRet As Integer

            Dim ls_bldno As String
            Dim ls_comcd As String
            Dim ls_tnsNum As String
            Dim ls_testgbn As String
            Dim ls_testdt As String = fnGet_Sysdate()
            Dim ls_rst1 As String
            Dim ls_rst2 As String
            Dim ls_rst3 As String
            Dim ls_rst4 As String
            Dim ls_cmrmk As String

            With DbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                For i As Integer = 0 To ralArg.Count - 1
                    With CType(ralArg(i), STU_TnsJubsu)
                        ls_bldno = .BLDNO
                        ls_comcd = .COMCD
                        ls_tnsNum = .TNSJUBSUNO
                        ls_testgbn = .TESTGBN
                        ls_rst1 = .RST1
                        ls_rst2 = .RST2
                        ls_rst3 = .RST3
                        ls_rst4 = .RST4
                        ls_cmrmk = .CMRMK
                    End With


                    sSql = ""
                    sSql += "UPDATE lb032m"
                    sSql += "   SET testid2 = :testid2,"
                    sSql += "       testdt2 = fn_ack_sysdate,"
                    sSql += "       rst1    = :rst1,"
                    sSql += "       rst2    = :rst2,"
                    sSql += "       rst3    = :rst3,"
                    sSql += "       rst4    = :ret4,"
                    sSql += "       cmrmk   = :cmrmk,"
                    sSql += "       editdt  = fn_ack_sysdate,"
                    sSql += "       editid  = :editid,"
                    sSql += "       editip  = :editip"
                    sSql += " WHERE bldno     = :bldno"
                    sSql += "   AND comcd_out = :comcdout"

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("testid2", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rst1", OracleDbType.Varchar2).Value = ls_rst1
                    DbCmd.Parameters.Add("rst2", OracleDbType.Varchar2).Value = ls_rst2
                    DbCmd.Parameters.Add("rst3", OracleDbType.Varchar2).Value = ls_rst3
                    DbCmd.Parameters.Add("rst4", OracleDbType.Varchar2).Value = ls_rst4
                    DbCmd.Parameters.Add("cmrmk", OracleDbType.Varchar2).Value = ls_cmrmk
                    DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = ls_bldno
                    DbCmd.Parameters.Add("comcdout", OracleDbType.Varchar2).Value = ls_comcd

                    intRet = DbCmd.ExecuteNonQuery()

                    If intRet = 0 Then
                        ' lb032m merge into
                        sSql = ""
                        sSql += "INSERT INTO lb032m(                                                         "
                        sSql += "                   bldno, comcd_out, tnsjubsuno, testid1, testdt1, testid2, testdt2, rst1, rst2, rst3,"
                        sSql += "                   rst4, cmrmk, befoutid, befoutdt, outid, outdt, recid, recnm, emergency, ir,"
                        sSql += "                   filter, comcd, comnm, regdt, regid, regip, editdt, editid, editip"
                        sSql += "                  ) "
                        sSql += "SELECT bldno, comcd_out, tnsjubsuno, testid, testdt, :testid2, fn_ack_sysdate, :rst1, :rst2, :rst3,"
                        sSql += "       :rst4, :cmrmk, befoutid, befoutdt, outid, outdt, recid, recnm, emergency, ir,"
                        sSql += "       filter, comcd, comnm, fn_ack_sysdate, :regid, :regip, fn_ack_sysdate, :editid, :editip"
                        sSql += "  FROM lb030m                                              "
                        sSql += " WHERE bldno     = :bldno"
                        sSql += "   AND comcd_out = :comcdout"

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("testid2", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("rst1", OracleDbType.Varchar2).Value = ls_rst1
                        DbCmd.Parameters.Add("rst2", OracleDbType.Varchar2).Value = ls_rst2
                        DbCmd.Parameters.Add("rst3", OracleDbType.Varchar2).Value = ls_rst3
                        DbCmd.Parameters.Add("rst4", OracleDbType.Varchar2).Value = ls_rst4
                        DbCmd.Parameters.Add("cmrmk", OracleDbType.Varchar2).Value = ls_cmrmk
                        DbCmd.Parameters.Add("regid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("regip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("editid", OracleDbType.Varchar2).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleDbType.Varchar2).Value = USER_INFO.LOCALIP

                        DbCmd.Parameters.Add("bldno", OracleDbType.Varchar2).Value = ls_bldno
                        DbCmd.Parameters.Add("comcdoout", OracleDbType.Varchar2).Value = ls_comcd

                        intRet = DbCmd.ExecuteNonQuery()
                    End If

                    If intRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                Next

                m_DbTrans.Commit()

                lb_rtnValue = True

                Return lb_rtnValue
            Catch ex As Exception
                m_DbTrans.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                m_DbTrans.Dispose() : m_DbTrans = Nothing
                If m_DbCn.State = ConnectionState.Open Then m_DbCn.Close()
                m_DbCn.Dispose() : m_DbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function
    End Class

    '-- 질병관리 본부 입/출고 조회
    Public Class BldInOut

        'lhj
        Public Shared Function Select_BloodP() As DataTable '(ByVal dtpDateS As String, ByVal dtpDateE As String) As DataTable

            Dim sFn As String = "Function Select_BloodP"
            Dim alParm As New ArrayList
            Dim sSql As String = ""

            sSql = "select CLSDESC from lf000m where clsgbn = 'B01' "

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)


        End Function

        '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목.
        Public Shared Function Select_BloodP2(ByVal rsCbo As String) As DataTable '(ByVal dtpDateS As String, ByVal dtpDateE As String) As DataTable

            Dim sFn As String = "Function Select_BloodP"
            Dim alParm As New ArrayList
            Dim sSql As String = ""

            sSql += "select clsval from lf000m where clsgbn = 'B01' " + vbCrLf
            sSql += " AND CLSDESC = '" + rsCbo + "'"

            DbCommand()
            Return DbExecuteQuery(sSql, alParm)


        End Function

        Public Shared Function fBldIn_Search(ByVal r_dte_dates As String, ByVal r_dte_datee As String, ByVal sComcd As String, ByVal sComcd2 As String) As DataTable
            Dim sFn As String = "Public Function fBldIn_Search(String, String) As DataTable"

            Try
                'Dim sDates As String = Fn.ToDateInsStr(r_dte_dates)
                'Dim sDatee As String = Fn.ToDateInsStr(r_dte_datee)
                Dim objDTable As New DataTable

                Dim sSql As String = ""
                sSql &= "SELECT DISTINCT    SUBSTR (a.bldno, 0, 2) || '-' || SUBSTR (a.bldno, 3, 2) || '-' || SUBSTR (a.bldno, 5, 6) bldno," + vbCrLf
                sSql &= "                b.dspccd2, b.comnmp," + vbCrLf
                sSql &= "                DECODE (a.state, '0', DECODE(NVL(a.usedgbn,''),'', 'Y', 'R'), 'Y') ingbn," + vbCrLf
                sSql &= "                FN_ACK_DATE_STR (a.indt, 'YYYY-MM-DD') AS indtymd," + vbCrLf
                sSql &= "                FN_ACK_DATE_STR (a.indt, 'hh24:mi') AS indthm," + vbCrLf
                sSql &= "                FN_ACK_DATE_STR (a.dondt, 'YYYY-MM-DD') AS dondt, c.ID," + vbCrLf
                sSql &= "                c.cdval2 AS abotype, fn_ack_get_usr_name (a.inid) AS innm" + vbCrLf
                sSql &= "           FROM lb020m a, lf120m b, lf911m c" + vbCrLf
                sSql &= "          WHERE a.comcd = b.comcd" + vbCrLf
                sSql &= "            AND a.indt >= :dates" + vbCrLf
                sSql &= "            AND a.indt <  :datee" + vbCrLf
                sSql &= "            AND c.cdgrpid = 'B0001'" + vbCrLf
                'sSql &= "            AND b.dspccd2 IN ('01','04','51','54')" + vbCrLf
                sSql &= " and b.dspccd2 in ( '01','04','06','10','51','54','56','60','76','83') " + vbCrLf
                sSql &= "            AND RTRIM (a.abo) || RTRIM (a.rh) = RTRIM (c.cdnm)" + vbCrLf

                Dim alParm As New ArrayList
                alParm.Add(New OracleParameter("dates", r_dte_dates + "000000"))
                alParm.Add(New OracleParameter("datee", r_dte_datee + "235959"))

                '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. 
                If sComcd <> "" Then
                    ' sSql &= "And b.comcd in ('" + sComcd + "','" + sComcd2 + "' )"
                    sSql &= "And b.comcd in (" + sComcd + ")"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

                'DbCommand()
                'DbExecuteText(sSql, objDTable)
                'Dim dt As DataTable = DbExecute(sSql, al)

                'Return objDTable

            Catch ex As Exception
                'Fn.log(sFile & sFn, Err)
                'MsgBox(sFile & sFn & vbCrLf & ex.Message)
            End Try
        End Function

        Public Shared Function fBldOut_Search(ByVal r_dte_dates As String, ByVal r_dte_datee As String, ByVal sComcd As String, ByVal sComcd2 As String) As DataTable
            Dim sFn As String = "Public Function fBldIn_Search(String, String) As DataTable"

            Try
                Dim objDTable As New DataTable

                'Dim sSql As String = ""
                'sSql &= "SELECT SUBSTR(a.bldno,0,2) || '-' || SUBSTR(a.bldno,3,2) || '-' || SUBSTR(a.bldno,5,6) AS bldno," + vbCrLf
                'sSql &= "       c.dspccd2," + vbCrLf
                'sSql &= "       c.comnmp," + vbCrLf
                'sSql &= "       '1' AS bldflg," + vbCrLf
                'sSql &= "       FN_ACK_DATE_STR(a.outdt, 'YYYY-MM-DD') AS outdtymd," + vbCrLf
                'sSql &= "       FN_ACK_DATE_STR(a.outdt, 'HH24:MI') AS outdthm," + vbCrLf
                'sSql &= "       d.id," + vbCrLf
                'sSql &= "       d.cdval2 AS abotype," + vbCrLf
                'sSql &= "       fn_ack_get_usr_name(a.outid) AS outnm" + vbCrLf
                'sSql &= "  FROM lb030m a, lb020m b, lf120m c, lf911m d" + vbCrLf
                'sSql &= " WHERE a.outdt >= :dates" + vbCrLf
                'sSql &= "   AND a.outdt <  :datee" + vbCrLf
                'sSql &= "   AND a.bldno  = b.bldno" + vbCrLf
                'sSql &= "   AND a.comcd  = b.comcd" + vbCrLf
                'sSql &= "   AND b.comcd  = c.comcd" + vbCrLf
                'sSql &= "   AND RTRIM(b.abo) || RTRIM(b.rh) = RTRIM(d.cdnm)" + vbCrLf
                'sSql &= "   AND d.cdgrpid = 'B0001'" + vbCrLf
                'sSql &= "   AND c.dspccd2 IN ('01','04','51','54')" + vbCrLf

                ''<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. 
                'If sComcd <> "" And sComcd2 <> "" Then
                '    sSql &= "And b.comcd in ('" + sComcd + "','" + sComcd2 + "' )"
                'ElseIf sComcd <> "" And sComcd2 = "" Then
                '    sSql &= "And b.comcd = '" + sComcd + "'"

                'End If

                'sSql &= " UNION" + vbCrLf
                'sSql &= "SELECT SUBSTR(a.bldno,0,2) || '-' || SUBSTR(a.bldno,3,2) || '-' || SUBSTR(a.bldno,5,6) AS bldno," + vbCrLf
                'sSql &= "       c.dspccd2," + vbCrLf
                'sSql &= "       c.comnmp," + vbCrLf
                'sSql &= "       '2' AS bldflg," + vbCrLf
                'sSql &= "       FN_ACK_DATE_STR(a.outdt, 'YYYY-MM-DD') AS outdtymd," + vbCrLf
                'sSql &= "       FN_ACK_DATE_STR(a.outdt, 'HH24:MI') AS outdthm," + vbCrLf
                'sSql &= "       d.id," + vbCrLf
                'sSql &= "       d.cdval2 AS abotype," + vbCrLf
                'sSql &= "       fn_ack_get_usr_name(a.outid) AS outnm" + vbCrLf
                'sSql &= "  FROM lb031m a, lb020m b, lf120m c, lf911m d" + vbCrLf
                'sSql &= " WHERE a.outdt >= :dates" + vbCrLf
                'sSql &= "   AND a.outdt <  :datee" + vbCrLf
                'sSql &= "   AND a.rtnflg = '2'" + vbCrLf
                'sSql &= "   AND a.bldno  = b.bldno" + vbCrLf
                'sSql &= "   AND a.comcd  = b.comcd" + vbCrLf
                'sSql &= "   AND b.comcd  = c.comcd" + vbCrLf
                'sSql &= "   AND RTRIM(b.abo) || RTRIM(b.rh) = RTRIM(d.cdnm)" + vbCrLf
                'sSql &= "   AND d.cdgrpid = 'B0001'" + vbCrLf
                'sSql &= "   AND c.dspccd2 IN ('01','04','51','54')" + vbCrLf

                'Dim alParm As New ArrayList
                'alParm.Add(New OracleParameter("dates", r_dte_dates + "000000"))
                'alParm.Add(New OracleParameter("datee", r_dte_datee + "235959"))

                ''<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. 
                'If sComcd <> "" And sComcd2 <> "" Then
                '    sSql &= "And b.comcd in ('" + sComcd + "','" + sComcd2 + "' )"
                'ElseIf sComcd <> "" And sComcd2 = "" Then
                '    sSql &= "And b.comcd = '" + sComcd + "'"

                'End If

                Dim sSql As String = ""
                sSql &= "SELECT SUBSTR(a.bldno,0,2) || '-' || SUBSTR(a.bldno,3,2) || '-' || SUBSTR(a.bldno,5,6) AS bldno," + vbCrLf
                sSql &= "       c.dspccd2," + vbCrLf
                sSql &= "       c.comnmp," + vbCrLf
                sSql &= "       '1' AS bldflg," + vbCrLf
                sSql &= "       FN_ACK_DATE_STR(a.outdt, 'YYYY-MM-DD') AS outdtymd," + vbCrLf
                sSql &= "       FN_ACK_DATE_STR(a.outdt, 'HH24:MI') AS outdthm," + vbCrLf
                sSql &= "       d.id," + vbCrLf
                sSql &= "       d.cdval2 AS abotype," + vbCrLf
                sSql &= "       fn_ack_get_usr_name(a.outid) AS outnm , " + vbCrLf
                sSql &= "              case when e.sex = 'M' THEN 'M'   " + vbCrLf
                sSql &= " WHEN E.SEX = 'F' THEN 'W'  END AS SEX1  " + vbCrLf
                sSql &= ",'999' as info  , substr(g.birtdate,0,4) birth , " + vbCrLf
                '20220216 jhs 기존 표시 내용 정리
                sSql &= " case when e.deptcd = '2010200000' THEN '010'    " + vbCrLf ' 일반내과
                sSql &= "      when e.deptcd = '2010300000' THEN '011'    " + vbCrLf ' 소화기내과
                sSql &= "      when e.deptcd = '2010400000' THEN '012'    " + vbCrLf ' 순환기내과
                sSql &= "      when e.deptcd = '2010600000' THEN '013'    " + vbCrLf ' 호흡기내과
                sSql &= "      when e.deptcd = '2010500000' THEN '014'    " + vbCrLf ' 내분비내과
                sSql &= "      when e.deptcd = '2010700000' THEN '015'    " + vbCrLf ' 신장내과
                sSql &= "      when e.deptcd = '2010900000' THEN '016'    " + vbCrLf ' 혈액종양내과
                sSql &= "      when e.deptcd = '2011000000' THEN '017'    " + vbCrLf ' 감염내과 
                sSql &= "      when e.deptcd = '2011100000' THEN '017'    " + vbCrLf ' 류마티스내과
                sSql &= "      when e.deptcd = '2011300000' THEN '018'    " + vbCrLf ' 알레르기내과
                sSql &= "      when e.deptcd = '2040000000' THEN '020'    " + vbCrLf ' 일반외과
                sSql &= "      when e.deptcd = '2050000000' THEN '026'    " + vbCrLf ' 정형외과
                sSql &= "      when e.deptcd = '2060000000' THEN '027'    " + vbCrLf ' 신경외과
                '--when e.deptcd = 'BC' THEN '025'
                '--when e.deptcd = 'OS' THEN '026'
                '--when e.deptcd = 'NS' THEN '027'
                sSql &= "      when e.deptcd = '2070000000' THEN '028'    " + vbCrLf ' 흉부외과
                sSql &= "      when e.deptcd = '2080000000' THEN '029'    " + vbCrLf ' 성형외과
                sSql &= "      when e.deptcd = '2020000000' THEN '030'    " + vbCrLf ' 신경과
                sSql &= "      when e.deptcd = '2030000000' THEN '031'    " + vbCrLf ' 정신건강의학과
                sSql &= "      when e.deptcd = '2090000000' THEN '032'    " + vbCrLf ' 마취통증의학과
                sSql &= "      when e.deptcd = '2100000000' THEN '033'    " + vbCrLf ' 산부인과
                sSql &= "      when e.deptcd = '2110000000' THEN '034'    " + vbCrLf ' 소아청소년과
                sSql &= "      when e.deptcd = '2120000000' THEN '035'    " + vbCrLf ' 안과
                sSql &= "      when e.deptcd = '2130000000' THEN '036'    " + vbCrLf ' 이비인후과
                sSql &= "      when e.deptcd = '2140000000' THEN '037'    " + vbCrLf ' 피부과
                sSql &= "      when e.deptcd = '2150000000' THEN '038'    " + vbCrLf ' 비뇨기과
                sSql &= "      when e.deptcd = '2160000000' THEN '039'    " + vbCrLf ' 영상의학과
                sSql &= "      when e.deptcd = '2170000000' THEN '040'    " + vbCrLf ' 방사선종양학과
                sSql &= "      when e.deptcd = '2210000000' THEN '041'    " + vbCrLf ' 병리과
                sSql &= "      when e.deptcd = '2200000000' THEN '042'    " + vbCrLf ' 진단검사의학과
                sSql &= "      when e.deptcd = '2220000000' THEN '043'    " + vbCrLf ' 재활의학과
                sSql &= "      when e.deptcd = '2230000000' THEN '045'    " + vbCrLf ' 가정의학과
                sSql &= "      when e.deptcd = '2180000000' THEN '046'    " + vbCrLf ' 핵의학과
                sSql &= "      when e.deptcd = '2280000000' THEN '048'    " + vbCrLf ' 응급의학과
                sSql &= "      when e.deptcd = '2240000000' THEN '060'    " + vbCrLf ' 일반치과
                sSql &= "      else '099' END AS DEPTNO,                  " + vbCrLf ' 기타
                '---------------------------------------------------------
                sSql &= " case when h.abo||h.rh = 'O+'  THEN '1' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'A+'  THEN '2' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'B+'  THEN '3' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'AB+' THEN '4' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'O-'  THEN '5' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'A-'  THEN '6' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'B-'  THEN '7' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'AB-' THEN '8' END ABO  " + vbCrLf

                '<2022.04.05 JJH BMS 폐기사유코드 추가
                sSql += " , fn_ack_get_usr_name(a.outid) as BMS_RTN_CD " + vbCrLf

                sSql &= "  FROM lb030m a, lb020m b, lf120m c, lf911m d , lb040m e , lb040m f , lb043m h , VW_ACK_OCS_PAT_INFO g" + vbCrLf
                sSql &= " WHERE a.outdt >= :dates" + vbCrLf
                sSql &= "   AND a.outdt <  :datee" + vbCrLf
                sSql &= "   AND a.bldno  = b.bldno" + vbCrLf
                'sSql &= "   AND a.comcd  = b.comcd" + vbCrLf
                sSql &= "   AND a.comcd_out  = b.comcd" + vbCrLf ' 2019-04-04 JJH comcd -> comcd_out (실제출고된 성분코드)
                'sSql &= "   AND b.comcd  = c.comcd" + vbCrLf
                sSql &= "   AND a.comcd  = c.comcd" + vbCrLf ' 2019-04-25 JJH 실제출고된 성분제제 명 -> 출고된 성분제제 명
                sSql &= "   AND e.tnsjubsuno  = a.tnsjubsuno " + vbCrLf
                sSql &= "   AND f.tnsjubsuno = a.tnsjubsuno" + vbCrLf
                sSql &= "   aND e.regno = g.patno  " + vbCrLf
                sSql &= "   and g.sex = e.sex " + vbCrLf
                sSql &= "   and h.BLDNo = a.bldno " + vbCrLf
                sSql &= "   AND RTRIM(b.abo) || RTRIM(b.rh) = RTRIM(d.cdnm)" + vbCrLf
                sSql &= "   AND d.cdgrpid = 'B0001'" + vbCrLf
                ' sSql &= "   AND c.dspccd2 IN ('01','04','51','54')" + vbCrLf
                sSql &= " and c.dspccd2 in ( '01','04','06','10','51','54','56','60','76','83') " + vbCrLf

                '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. 
                If sComcd <> "" Then
                    sSql &= "And b.comcd in (" + sComcd + ")"
                End If

                sSql &= " UNION" + vbCrLf
                sSql &= "SELECT SUBSTR(a.bldno,0,2) || '-' || SUBSTR(a.bldno,3,2) || '-' || SUBSTR(a.bldno,5,6) AS bldno," + vbCrLf
                sSql &= "       c.dspccd2," + vbCrLf
                sSql &= "       c.comnmp," + vbCrLf
                '20210419 jhs 폐기는 2로 출력 폐기는 폐기 일시로 표시
                'sSql &= "       '1' AS bldflg," + vbCrLf
                sSql &= "       '2' AS bldflg," + vbCrLf
                'sSql &= "       FN_ACK_DATE_STR(a.outdt, 'YYYY-MM-DD') AS outdtymd," + vbCrLf
                'sSql &= "       FN_ACK_DATE_STR(a.outdt, 'HH24:MI') AS outdthm," + vbCrLf
                sSql &= "       FN_ACK_DATE_STR(a.rtndt, 'YYYY-MM-DD') AS outdtymd," + vbCrLf
                sSql &= "       FN_ACK_DATE_STR(a.rtndt, 'HH24:MI') AS outdthm," + vbCrLf
                '------------------------------------
                sSql &= "       d.id," + vbCrLf
                sSql &= "       d.cdval2 AS abotype," + vbCrLf
                sSql &= "       fn_ack_get_usr_name(a.outid) AS outnm ," + vbCrLf
                sSql &= "              case when e.sex = 'M' THEN 'M'   " + vbCrLf
                sSql &= " WHEN E.SEX = 'F' THEN 'W'  END AS SEX1  " + vbCrLf
                sSql &= ",'999' as info  , substr(g.birtdate,0,4) birth , " + vbCrLf
                '20220216 jhs 기존 표시 내용 정리
                sSql &= " case when e.deptcd = '2010200000' THEN '010'    " + vbCrLf ' 일반내과
                sSql &= "      when e.deptcd = '2010300000' THEN '011'    " + vbCrLf ' 소화기내과
                sSql &= "      when e.deptcd = '2010400000' THEN '012'    " + vbCrLf ' 순환기내과
                sSql &= "      when e.deptcd = '2010600000' THEN '013'    " + vbCrLf ' 호흡기내과
                sSql &= "      when e.deptcd = '2010500000' THEN '014'    " + vbCrLf ' 내분비내과
                sSql &= "      when e.deptcd = '2010700000' THEN '015'    " + vbCrLf ' 신장내과
                sSql &= "      when e.deptcd = '2010900000' THEN '016'    " + vbCrLf ' 혈액종양내과
                sSql &= "      when e.deptcd = '2011000000' THEN '017'    " + vbCrLf ' 감염내과 
                sSql &= "      when e.deptcd = '2011100000' THEN '017'    " + vbCrLf ' 류마티스내과
                sSql &= "      when e.deptcd = '2011300000' THEN '018'    " + vbCrLf ' 알레르기내과
                sSql &= "      when e.deptcd = '2040000000' THEN '020'    " + vbCrLf ' 일반외과
                sSql &= "      when e.deptcd = '2050000000' THEN '026'    " + vbCrLf ' 정형외과
                sSql &= "      when e.deptcd = '2060000000' THEN '027'    " + vbCrLf ' 신경외과
                '--when e.deptcd = 'BC' THEN '025'
                '--when e.deptcd = 'OS' THEN '026'
                '--when e.deptcd = 'NS' THEN '027'
                sSql &= "      when e.deptcd = '2070000000' THEN '028'    " + vbCrLf ' 흉부외과
                sSql &= "      when e.deptcd = '2080000000' THEN '029'    " + vbCrLf ' 성형외과
                sSql &= "      when e.deptcd = '2020000000' THEN '030'    " + vbCrLf ' 신경과
                sSql &= "      when e.deptcd = '2030000000' THEN '031'    " + vbCrLf ' 정신건강의학과
                sSql &= "      when e.deptcd = '2090000000' THEN '032'    " + vbCrLf ' 마취통증의학과
                sSql &= "      when e.deptcd = '2100000000' THEN '033'    " + vbCrLf ' 산부인과
                sSql &= "      when e.deptcd = '2110000000' THEN '034'    " + vbCrLf ' 소아청소년과
                sSql &= "      when e.deptcd = '2120000000' THEN '035'    " + vbCrLf ' 안과
                sSql &= "      when e.deptcd = '2130000000' THEN '036'    " + vbCrLf ' 이비인후과
                sSql &= "      when e.deptcd = '2140000000' THEN '037'    " + vbCrLf ' 피부과
                sSql &= "      when e.deptcd = '2150000000' THEN '038'    " + vbCrLf ' 비뇨기과
                sSql &= "      when e.deptcd = '2160000000' THEN '039'    " + vbCrLf ' 영상의학과
                sSql &= "      when e.deptcd = '2170000000' THEN '040'    " + vbCrLf ' 방사선종양학과
                sSql &= "      when e.deptcd = '2210000000' THEN '041'    " + vbCrLf ' 병리과
                sSql &= "      when e.deptcd = '2200000000' THEN '042'    " + vbCrLf ' 진단검사의학과
                sSql &= "      when e.deptcd = '2220000000' THEN '043'    " + vbCrLf ' 재활의학과
                sSql &= "      when e.deptcd = '2230000000' THEN '045'    " + vbCrLf ' 가정의학과
                sSql &= "      when e.deptcd = '2180000000' THEN '046'    " + vbCrLf ' 핵의학과
                sSql &= "      when e.deptcd = '2280000000' THEN '048'    " + vbCrLf ' 응급의학과
                sSql &= "      when e.deptcd = '2240000000' THEN '060'    " + vbCrLf ' 일반치과
                sSql &= "      else '099' END AS DEPTNO,                  " + vbCrLf ' 기타
                '---------------------------------------------------------
                sSql &= " case when h.abo||h.rh = 'O+'  THEN '1' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'A+'  THEN '2' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'B+'  THEN '3' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'AB+' THEN '4'" + vbCrLf
                sSql &= "      when h.abo||h.rh = 'O-'  THEN '5' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'A-'  THEN '6' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'B-'  THEN '7' " + vbCrLf
                sSql &= "      when h.abo||h.rh = 'AB-' THEN '8' END ABO  " + vbCrLf

                '<2022.04.05 JJH BMS 폐기사유코드 추가
                sSql += ", CASE a.RTNRSNCD WHEN 'P00001' THEN '11'	" + vbCrLf
                sSql += "                  WHEN 'P00002' THEN '11'	" + vbCrLf
                sSql += "                  WHEN 'P00003' THEN '11'	" + vbCrLf
                sSql += "                  WHEN 'P00004' THEN '11'	" + vbCrLf
                sSql += "                  WHEN 'P00005' THEN '13'	" + vbCrLf
                sSql += "                  WHEN 'P00006' THEN '14'	" + vbCrLf
                sSql += "                  WHEN 'P00007' THEN '12'	" + vbCrLf
                sSql += "                  WHEN 'P00008' THEN '11'	" + vbCrLf
                sSql += "                  WHEN 'P00009' THEN '11'	" + vbCrLf
                sSql += "                  WHEN 'H00001' THEN '19'	" + vbCrLf
                sSql += "                  WHEN 'H00002' THEN '19'	" + vbCrLf
                sSql += "                  WHEN 'H00003' THEN '18'	" + vbCrLf
                sSql += "                  WHEN 'H00004' THEN '17'	" + vbCrLf
                sSql += "                  ELSE ''					" + vbCrLf
                sSql += "   END BMS_RTN_CD							" + vbCrLf

                sSql &= " FROM lb031m a, lb020m b, lf120m c, lf911m d , lb040m e , lb040m f , lb043m h,  VW_ACK_OCS_PAT_INFO g " + vbCrLf
                sSql &= " WHERE a.outdt >= :dates" + vbCrLf
                sSql &= "   AND a.outdt <  :datee" + vbCrLf
                sSql &= "   AND a.rtnflg = '2'" + vbCrLf
                sSql &= "   AND a.bldno  = b.bldno" + vbCrLf
                'sSql &= "   AND a.comcd  = b.comcd" + vbCrLf
                sSql &= "   AND a.comcd_out  = b.comcd" + vbCrLf ' 2019-04-04 JJH comcd -> comcd_out (실제출고된 성분코드)
                'sSql &= "   AND b.comcd  = c.comcd" + vbCrLf
                sSql &= "   AND a.comcd  = c.comcd" + vbCrLf ' 2019-04-25 JJH 실제출고된 성분제제 명 -> 출고된 성분제제 명
                sSql &= "   AND e.tnsjubsuno  = a.tnsjubsuno " + vbCrLf
                sSql &= "   AND f.tnsjubsuno = a.tnsjubsuno" + vbCrLf
                sSql &= "   aND e.regno = g.patno  " + vbCrLf
                sSql &= "   and g.sex = e.sex " + vbCrLf
                sSql &= "   and h.BLDNo = a.bldno " + vbCrLf
                sSql &= "   AND RTRIM(b.abo) || RTRIM(b.rh) = RTRIM(d.cdnm)" + vbCrLf
                sSql &= "   AND d.cdgrpid = 'B0001'" + vbCrLf
                'sSql &= "   AND c.dspccd2 IN ('01','04','51','54')" + vbCrLf
                sSql &= "   and c.dspccd2 in ( '01','04','06','10','51','54','56','60','76','83') " + vbCrLf

                Dim alParm As New ArrayList
                alParm.Add(New OracleParameter("dates", r_dte_dates + "000000"))
                alParm.Add(New OracleParameter("datee", r_dte_datee + "235959"))

                '<<< 20151112 lhj 질병 관리 본부. 혈액 제제 종류 확대 항목. 
                If sComcd <> "" Then
                    sSql &= "And b.comcd in (" + sComcd + ")"
                End If

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                MsgBox(sFn & vbCrLf & ex.Message)
                Return New DataTable
            End Try
        End Function
    End Class

#Region " 수혈 구조체 선언 "
    'Public Class STU_TnsJubsu
    '    Public REGNO As String = ""         ' 등록번호
    '    Public PATNM As String = ""         ' 환자명
    '    Public SEX As String = ""           ' 성별
    '    Public AGE As String = ""           ' 나이
    '    Public ORDDATE As String = ""       ' 처방일자
    '    Public DEPTCD As String = ""        ' 진료과
    '    Public DRCD As String = ""          ' 진료의
    '    Public WARDCD As String = ""        ' 병동
    '    Public ROOMNO As String = ""        ' 병실
    '    Public COMCD As String = ""         ' 성분제제코드
    '    Public COMNM As String = ""         ' 성분제제코드
    '    Public COMORDCD As String = ""      ' 원처방코드
    '    Public SPCCD As String = ""         ' 검체코드
    '    Public OWNGBN As String = ""        ' 처방소유구분
    '    Public TNSJUBSUNO As String = ""    ' 수혈의뢰접수번호
    '    Public FKOCS As String = ""         ' 외래처방키
    '    Public SEQ As String = ""           ' 순번
    '    Public BLDNO As String = ""         ' 혈액번호
    '    Public IOGBN As String = ""         ' 입외구분
    '    Public BCNO As String = ""          ' 검체번호
    '    Public STATE As String = ""         ' 상태
    '    Public FILTER As String = ""        ' 필터
    '    Public WORKID As String = ""        ' 
    '    Public RST1 As String = ""          ' 크로스결과4
    '    Public RST2 As String = ""          ' 크로스결과4
    '    Public RST3 As String = ""          ' 크로스결과4
    '    Public RST4 As String = ""          ' 크로스결과4
    '    Public CMRMK As String = ""         ' 리마크
    '    Public TESTGBN As String = ""       ' 검사구분
    '    Public TESTID As String = ""        ' 검사자
    '    Public BEFOUTID As String = ""      ' 가출고아이디
    '    Public OUTID As String = ""         ' 출고자아이디
    '    Public RECID As String = ""         ' 수령자아이디
    '    Public RECNM As String = ""         ' 수령자명
    '    Public RTNREQID As String = ""      ' 반납/폐기 의뢰자
    '    Public RTNREQNM As String = ""      ' 반납/폐기 의뢰자명
    '    Public RTNRSNCD As String = ""      ' 반납사유코드
    '    Public RTNRSNCMT As String = ""     ' 반납사유
    '    Public EMER As String = ""          ' 응급
    '    Public IR As String = ""            ' 이라데이션
    '    Public COMCD_OUT As String = ""     ' 출고용 성분제제
    '    Public EDITIP As String = ""        ' 수정자IP
    '    Public TEMP01 As String = ""        ' 여유1
    '    Public TEMP02 As String = ""        ' 여유2
    '    Public TEMP03 As String = ""        ' 여유3

    '    Public ABO As String = ""           ' ABO 혈액형
    '    Public RH As String = ""            ' Rh 혈액형

    '    Public RTNDT As String = ""   ' 반납/폐기일시
    'End Class

    '-- 혈액입고
    Public Class STU_BldInfo
        Public Bldno_Full As String = ""    '-- 혈액번호 ( ex -> 20-03-000123 )
        Public BldNo As String = ""         '-- 혈액번호 ( ex -> 2003000050 )
        Public ComCd As String = ""         '-- 성분제제코드
        Public InDt As String = ""          '-- 입고일자
        Public InPlace As String = ""       '-- 입고장소
        Public DonGbn As String = ""        '-- 구분(0:혈액원)
        Public Abo As String = ""           '-- ABO
        Public Rh As String = ""            '-- Rh
        Public DonQnt As String = ""        '-- 혈액용량 (= 채혈량)
        Public DonDt As String = ""         '-- 헌혈일시 (= 채혈일)
        Public AvailDt As String = ""       '-- 유효일시
        Public Cmt As String = ""           '-- Comment
        Public RegNo As String = ""         '-- 등록번호
        Public UsedGbn As String = ""       '-- 사용여부
        Public Ir As String = ""
        Public Filter As String = ""
        Public PedGbn As String = ""
        Public Seq As String = ""           '-- number - 별 의미 없음
        Public ProvDay As String = ""       '-- 공급일
        Public ProvTime As String = ""      '-- 공급시간
        Public BldName As String = ""       '-- 혈액명 ex) 적혈구 농축액
        Public BldCd As String = ""         '-- 혈액코드 (성분제제 코드)
        Public ComNmd As String = ""        '-- 성분제제명 
        Public Price As String = ""         '-- 단가 - 별 의미 없음
        Public OutID As String = ""         '-- 출고인

        Public Sub New()
            MyBase.New()
        End Sub
    End Class

    Public Class csAboRh
        Public msBldCd As String = ""      ' 혈액원 code
        Public msAbo As String = ""      ' Abo
        Public msRh As String = ""       ' Rh
        Public msComCd As String = ""    ' 성분제제코드
        Public msBldQnt As String = ""   ' 용량 

        Public Sub New()
            MyBase.New()
        End Sub
    End Class

    Public Class TnsTranList
        Public TNSJUBSUNO As String = ""
        Public REGNO As String = ""
        Public HGYN As String = ""
        Public ALLYN As String = ""
        Public CBCYN As String = ""
        Public ECPYN As String = ""
        Public SEQ As String = ""
        Public CMCALLER As String = ""
    End Class

    Public Class BldTatInput
        Public TNSJUBSUNO As String = ""
        Public REGNO As String = ""
        Public GWA As String = ""
        Public BLDNO As String = ""
        Public VARYN As String = ""
    End Class
#End Region


End Namespace
