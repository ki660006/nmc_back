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
Imports System.Data.OracleClient

Imports DBORA.DbProvider
Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.CommPrint
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommLogin

Namespace APP_BT_N


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
                sSql += "       b4.regno, j.patnm, b4.sex || '/' || b4.age sexage,"
                sSql += "       b4.deptcd || CASE WHEN b4.iogbn = 'I' THEN '/' || b4.wardno ELSE '' END deptward,"
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
                sSql += "       b42.reqqnt"
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
                sSql += "  ORDER BY bldno, outdt DESC"

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile & sFn, Err)
                Return New DataTable
                MsgBox(ex.Message)
            End Try
        End Function

        Public Sub PrintDo(ByVal rsFrmName As String, ByVal ra_BldInfo As ArrayList, ByVal rbLabel As Boolean, ByVal rbSummary As Boolean, ByVal riCopy As Integer)
            Dim sFn As String = "Public Shared Sub PrintDo(ByVal asBloodList As ArrayList, ByVal asCOMM_STATE As String)"

            Try
                Dim sTnsNo As String = ""
                Dim sBldNos As String = ""
                Dim alBldLabel As New ArrayList

                For ix As Integer = 0 To ra_BldInfo.Count - 1
                    sTnsNo = CType(ra_BldInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    If ix > 0 Then sBldNos += ","
                    sBldNos += CType(ra_BldInfo(ix), clsTnsJubsu).BLDNO
                Next

                Dim dt = fnGet_Blood_Label_Info(sTnsNo, sBldNos)
                If dt.Rows.Count > 0 Then

                    Dim bldinfo As New STU_BLDLABEL

                    If rbLabel Then
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

                            End With

                            alBldLabel.Add(bldinfo)

                            If dt.Rows(ix).Item("bldno_add").ToString = "N" Then
                                For iCnt As Integer = 1 To riCopy - 1
                                    alBldLabel.Add(bldinfo)
                                Next
                            End If

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
        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.BldSql" + vbTab

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
            sSql += "       a.abo, a.rh, a.donqnt, a.dondt, a.availdt, :state, a.statedt, a.cmt, a.inid,"
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
            sSql += "   AND a.comcd_out  = :comcdout"
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
            sSql += "    VALUES( :bldno, :comcdout, :tnsno, :testgbn, :testid, :testdt,"


            If rsNCross.Length() > 0 Then
                sSql += "NVL(:rst1, '-') , NVL(:rst2, '-'), NVL(:rst3, '-') , NVL(:rst4, '-'), NVL(:cmrmk, '응급출고'),"
            Else
                sSql += ":rst1, :rst2, :rst3, :rst4, :cmrmk,"
            End If

            sSql += ":eryn, :ir, :filter, :comcd, :comnm, :regid, :regip, fn_ack_sysdate, :editid, :editip, fn_ack_sysdate"

            If rsNCross.Length() > 0 Then
                sSql += ", :outid, :outdt, :recid, :recnm"
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
                    sSql += "   SET befoutid = :outid,"
                    sSql += "       befoutdt = :outdt,"
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
            sSql += "   AND comcd_out  = :comcdout"
            sSql += "   AND tnsjubsuno = :tnsno"

            Return sSql
        End Function

        ' 혈액출고 데이터 삭제
        Public Function fnGet_DelLB030MSql() As String
            Dim sSql As String = ""

            sSql += "DELETE lb030m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcdout"
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
            sSql += "       :editid, :editip,   fn_ack_sysdate"
            sSql += "  FROM lb031m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcdout"
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
            sSql += "       ir_in,     :keepgbn,  keepid,     keeptm,     :regid,    :regip, fn_ack_sysdate, :editid, :editdt, fn_ack_sysdate"
            sSql += "  FROM lb030m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcdout"
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
            sSql += "   AND a.comcd_out  = :comcdout"
            sSql += "   and a.tnsjubsuno = :tnsno"

            Return sSql
        End Function


        Public Function fnGet_DelLB031mSql() As String
            Dim sSql As String = ""

            sSql += "DELETE lb031m"
            sSql += " WHERE bldno      = :bldno"
            sSql += "   AND comcd_out  = :comcdout"
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
            sSql += "    VALUES( :bldno,    :comcdout,  :rtndt,     '',      '',     '',      '',             '',         '',      '',"
            sSql += "            '',        '',         '',         '',      '',     '',      '',             '',         :rtnid,  :rtnreqid,"
            sSql += "            :rtnreqnm, :rtnrsncd,  :rtnrsncmt, :rtnflg, '',     '',      '',             :fitler,    '',      '',"
            sSql += "            '',        :keepgbn,   :keepid,    :keeptm, :regid, :regip,  fn_ack_sysdate, :editid, :editdt,    fn_ack_sysdate"
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
                    sSql += "   AND comcd      = :comcd"

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
            sSql += "   AND fkocs      = :fkocs"
            sSql += "   AND seq        = :seq"

            Return sSql
        End Function

        ' 수혈의뢰세부데이터 상태변경
        Public Function fnGet_UpdLB043MStateSql() As String
            Dim sSql As String = ""

            sSql += "UPDATE lb043m"
            sSql += "   SET state    = :state,"
            sSql += "       abo      = CASE WHEN NVL(abo, ' ') = ' ' THEN :abo ELSE abo END,"
            sSql += "       rh       = CASE WHEN NVL(rh , ' ') = ' ' THEN :rh ELSE rh END,"
            sSql += "       ocsapply = :ocsapply,"
            sSql += "       editid   = :editid,"
            sSql += "       editip   = :editip,"
            sSql += "       editdt   = fn_ack_sysdate"
            sSql += " WHERE tnsjubsuno = :tnsno"
            sSql += "   AND comcd      = :comcd"
            sSql += "   AND fkocs      = :fkcos"
            sSql += "   AND seq        = :seq"

            Return sSql
        End Function

        ' 수혈의뢰세부데이터 혈액번호 업데이트 및 성분제제 변경 크로스 매칭 검사결과저장시
        Public Function fnGet_UpdLB043MBCSSql() As String
            Dim sSql As String = ""

            sSql += "UPDATE lb043m"
            sSql += "   SET state        = :state,"
            sSql += "       comcd_out    = :comcdout,"
            sSql += "       bldno        = :bldno,"
            sSql += "       editid       = :editid,"
            sSql += "       editip       = :editip,"
            sSql += "       editdt       = fn_ack_sysdate"
            sSql += " WHERE tnsjubsuno   = :tnsno"
            sSql += "   AND comcd        = :comcd"
            sSql += "   AND (fkocs, seq) = (SELECT MIN(fkocs), MIN(seq)"
            sSql += "                         FROM lb043m"
            sSql += "                        WHERE tnsjubsuno = :tnsno"
            sSql += "                          AND comcd      = :comcd"
            sSql += "                          AND state      = '1'"
            sSql += "                      )"

            Return sSql
        End Function

        '' 의뢰검체, 보관검체 업데이트
        Public Function fnGet_UpdBcnoBlood(ByVal rsGbn As String) As String
            Dim ls_rtnValue As String = ""

            ls_rtnValue += "UPDATE lb040m               "

            If rsGbn = "UPD" Then
                ls_rtnValue += "   SET bcno_order = :bcnoord"
                ls_rtnValue += "     , bcno_keep  = :bcnokeep"
            ElseIf rsGbn = "ORDER" Then
                ls_rtnValue += "   SET bcno_order = :bcnoord"
            ElseIf rsGbn = "KEEP" Then
                ls_rtnValue += "   SET bcno_keep = :bcnokeep"
            End If

            ls_rtnValue += "     , editid     = :editid"
            ls_rtnValue += "     , editip     = :editip"
            ls_rtnValue += "     , editdt     = fn_ack_sysdate "
            ls_rtnValue += " WHERE tnsjubsuno = :tnsno"

            Return ls_rtnValue
        End Function

        ' 보관검체정보 입력
        Public Function fnGet_InsLB080M() As String
            Dim ls_rtnValue As String = ""

            ls_rtnValue += "insert                                         "
            ls_rtnValue += "  into lb080m                                  "
            ls_rtnValue += "     ( KEEPSPCNO                               "
            ls_rtnValue += "     , REGNO                                   "
            ls_rtnValue += "     , USTM                                    "
            ls_rtnValue += "     , UETM                                    "
            ls_rtnValue += "     , BLOODTYP                                "
            ls_rtnValue += "     , ABO                                     "
            ls_rtnValue += "     , RH                                      "
            ls_rtnValue += "     , KEEPSPCBCNO                             "
            ls_rtnValue += "     , REGID                                   "
            ls_rtnValue += "     , REGIP                                   "
            ls_rtnValue += "     , REGDT )                               "
            ls_rtnValue += "values                                         "
            ls_rtnValue += "     ( :keepspcno                              "
            ls_rtnValue += "     , :regno                                       "
            ls_rtnValue += "     , :ustm     "
            ls_rtnValue += "     , :uetm "
            ls_rtnValue += "     , :bloodtype                                       "
            ls_rtnValue += "     , :abo                                       "
            ls_rtnValue += "     , :rh                                       "
            ls_rtnValue += "     , :keepspcbcno                                       "
            ls_rtnValue += "     , '" + USER_INFO.USRID + "'               "
            ls_rtnValue += "     , '" + USER_INFO.LOCALIP + "'             "
            ls_rtnValue += "     , fn_ack_sysdate )                            "
            Return ls_rtnValue
        End Function



    End Class

    '-- 혈액입고
    Public Class BldIn
        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.BldIn" + vbTab

        '-- 혈액원 혈액형코드로 혈액형 리턴
        Public Shared Function fnGet_BldCdToBType(ByVal rsBldCd As String) As DataTable
            Dim sFn As String = "fnGeg_BldcdToBtype(String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT infofld1, infofld2"
                sSql += "  FROM lf122m"
                sSql += " WHERE infogbn = '1'"
                sSql += "   AND infocd  = :infocd"

                alParm.Add(New OracleParameter("infocd", OracleType.VarChar, rsBldCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldCd))

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
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT comcd, donqnt, comnmd"
                sSql += "  FROM lf120M"
                sSql += " WHERE bldcd = :bldcd"
                sSql += "   AND usdt <= fn_ack_sysdate"
                sSql += "   AND uedt >  fn_ack_sysdate"
                'sSql += "   AND NVL(pscomcd, ' ')  = ' '"

                alParm.Add(New OracleParameter("bldcd", OracleType.VarChar, rsBldCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldCd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        '-- 혈액입고 리스트 조회
        Public Shared Function fnGge_Bldno_List(ByVal rsDateS As String, ByVal rsDateE As String) As DataTable
            Dim sFn As String = "Function fnGge_Bldno_List(String, String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                '-- DONGBN = 0 외의 것( 일반,지정,성분 )은 헌혈
                sSql += "SELECT DISTINCT"
                sSql += "       fn_ack_date_str(a.indt, 'yyyy-mm-dd hh24:mi') indt,"
                sSql += "       fn_ack_get_bldno_full(a.bldno) bldno, a.inplace, a.abo, a.rh, a.donqnt,"
                sSql += "       fn_ack_date_str(a.dondt, 'yyyy-mm-dd') dondt, a.cmt,"
                sSql += "       fn_ack_date_str(a.availdt, 'yyyy-mm-dd') availdt,"
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
                sSql += "       a.abo || a.rh abo_rh, regno, fn_ack_get_usr_name(a.inid) usrnm"
                sSql += "  FROM lb020m a, lf120m b"
                sSql += " WHERE a.comcd = b.comcd"
                sSql += "   AND a.indt >= :date1"
                sSql += "   AND a.indt <= :date2"
                sSql += "   AND b.usdt <= a.indt"
                sSql += "   AND B.uedt >  a.indt"
                sSql += " ORDER BY a.abo, a.rh, a.comcd, dondt DESC"

                alParm.Add(New OracleParameter("date1", OracleType.VarChar, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("date2", OracleType.VarChar, (rsDateE + "235959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "235959"))

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

                sSql += "SELECT comnmd, availmi, comcd, dispseql"
                sSql += "  FROM lf120m"
                sSql += " WHERE (donqnt IS NULL OR donqnt = :donqnt)"
                sSql += "   AND NVL(pscomcd, ' ') = ' '"

                alParm.Add(New OracleParameter("donqnt", OracleType.VarChar, rsQnt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsQnt))

                If rsUsDt = "" Then
                    sSql += "   AND usdt    <= fn_ack_sysdate"
                    sSql += "   AND uedt    >  fn_ack_sysdate"
                Else
                    sSql += "   AND usdt    <= :usdt"
                    sSql += "   AND uedt    >  :usdt"

                    alParm.Add(New OracleParameter("usdt", OracleType.VarChar, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                    alParm.Add(New OracleParameter("usdt", OracleType.VarChar, rsUsDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsUsDt))
                End If
                sSql += "   AND NVL(ftcd, ' ') = ' '"   '-- 필터는 입고 하지 않는다.

                If sComCds <> "" Then
                    sSql += "   AND comcd NOT IN ('" + sComCds.Replace(",", "','") + "')"
                End If

                sSql += " GROUP BY comnmd, availmi, comcd, dispseql"
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
                sSql += "       fn_ack_date_str(a.indt, 'yyyy-mm-dd') as indt,"
                sSql += "       fn_ack_get_bldno_full(a.bldno) bldno,  a.dongbn, a.inplace, a.abo, a.rh, a.donqnt,"
                sSql += "       fn_ack_date_str(a.dondt, 'yyyy-mm-dd') as dondt, a.cmt,"
                sSql += "       fn_ack_date_str(a.availdt, 'yyyy-mm-dd') as availdt,"
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
                sSql += "       a.abo || a.rh abo_rh, regno, fn_ack_get_usr_name(a.inid) usrnm"
                sSql += "  FROM lb020m a, lf120m b"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = b.comcd"
                sSql += "   AND a.indt >= b.usdt"
                sSql += "   AND a.indt <  b.uedt"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldNo))

                If rsComCd <> "" Then
                    sSql += "   AND a.comcd = :comcd"

                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComCd))
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

                sSql += "SELECT patnm patnm, 0 seq FROM vw_ack_ocs_pat_info WHERE patno = :regno "
                sSql += " UNION "
                sSql += "SELECT suname patnm, 1 seq FROM mts0002_lis WHERE bunho = :regno"
                sSql += " ORDER BY seq"

                alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))
                alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegNo))

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

            Dim dbCn As oracleConnection = GetDbConnection()
            Dim dbTrans As oracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New oracleCommand
                Dim dbDa As oracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sSrvTm As String = ""

                sSql += "SELECT fn_ack_date_str(fn_ack_sysdate, 'hh24:mi:ss') srvtime FROM DUAL"

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New oracleDataAdapter(dbCmd)

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
                    .Parameters.Add("bldno", OracleType.VarChar).Value = r_o_bld.BldNo
                    .Parameters.Add("comcd", OracleType.VarChar).Value = r_o_bld.ComCd
                    .Parameters.Add("indt", OracleType.VarChar).Value = r_o_bld.InDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                    .Parameters.Add("inplace", oracleType.VarChar).Value = r_o_bld.InPlace
                    .Parameters.Add("dongbn", OracleType.VarChar).Value = r_o_bld.DonGbn
                    .Parameters.Add("abo", OracleType.VarChar).Value = r_o_bld.Abo
                    .Parameters.Add("rh", OracleType.VarChar).Value = r_o_bld.Rh
                    .Parameters.Add("donqnt", OracleType.VarChar).Value = r_o_bld.DonQnt
                    .Parameters.Add("dondt", OracleType.VarChar).Value = r_o_bld.DonDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                    .Parameters.Add("availdt", oracleType.VarChar).Value = r_o_bld.AvailDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                    .Parameters.Add("state", OracleType.VarChar).Value = "0"
                    .Parameters.Add("cmt", OracleType.VarChar).Value = r_o_bld.Cmt
                    .Parameters.Add("regno", OracleType.VarChar).Value = r_o_bld.RegNo
                    .Parameters.Add("inid", OracleType.VarChar).Value = USER_INFO.USRID
                    .Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                    .Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    .Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    .Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    iRet = .ExecuteNonQuery()

                End With

                If iRet > 0 Then
                    dbTrans.Commit()
                    Return True
                Else
                    dbTrans.Rollback()
                    Return False
                End If

            Catch ex As Exception
                dbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                dbTrans.Dispose() : dbTrans = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        '-- 혈액입고
        Public Shared Function fnExe_BldIn(ByRef r_al_bldInfo As ArrayList) As Boolean
            Dim sFn As String = "fnExe_BldIn(object) As Boolean"

            Dim dbCn As OracleConnection = GetDbConnection()
            Dim dbTrans As OracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New oracleCommand
                Dim dbDa As oracleDataAdapter
                Dim dt As New DataTable

                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sSrvDt As String = ""

                sSql += "SELECT fn_ack_date_str(fn_ack_sysdate, 'yyyy-mm-dd hh24:mi:ss') srvdt FROM DUAL"

                dbCmd.Connection = dbCn
                dbCmd.Transaction = dbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbDa = New oracleDataAdapter(dbCmd)

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
                    sSql += "    values( :stae,  :comcd,         :indt, :inplace, :dongbn, :abo,   :rh,    :donqnt,        :dondt,  :availdt,"
                    sSql += "            :state, fn_ack_sysdate, :cmt,  :regno,   :inid,   :regid, :regip, fn_ack_sysdate, :editid, :editip,  fn_ack_sysdate)"


                    With dbCmd
                        .CommandType = CommandType.Text
                        .CommandText = sSql

                        .Parameters.Clear()
                        .Parameters.Add("bldno", OracleType.VarChar).Value = stuBldIn.BldNo
                        .Parameters.Add("comcd", OracleType.VarChar).Value = stuBldIn.ComCd
                        .Parameters.Add("indt", OracleType.VarChar).Value = stuBldIn.InDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                        .Parameters.Add("inplace", oracleType.VarChar).Value = stuBldIn.InPlace
                        .Parameters.Add("dongbn", OracleType.VarChar).Value = stuBldIn.DonGbn
                        .Parameters.Add("abo", OracleType.VarChar).Value = stuBldIn.Abo
                        .Parameters.Add("rh", OracleType.VarChar).Value = stuBldIn.Rh
                        .Parameters.Add("donqnt", OracleType.VarChar).Value = stuBldIn.DonQnt
                        .Parameters.Add("dondt", OracleType.VarChar).Value = stuBldIn.DonDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                        .Parameters.Add("availdt", oracleType.VarChar).Value = stuBldIn.AvailDt.Replace("-", "").Replace(" ", "").Replace(":", "")
                        .Parameters.Add("state", OracleType.VarChar).Value = "0"
                        .Parameters.Add("cmt", OracleType.VarChar).Value = stuBldIn.Cmt
                        .Parameters.Add("regno", OracleType.VarChar).Value = stuBldIn.RegNo
                        .Parameters.Add("inid", OracleType.VarChar).Value = USER_INFO.USRID
                        .Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                        .Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        .Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        .Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        iRet += .ExecuteNonQuery()

                    End With
                Next

                If iRet > 0 Then
                    dbTrans.Commit()
                    Return True
                Else
                    dbTrans.Rollback()
                    Return False
                End If

            Catch ex As Exception
                dbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                dbTrans.Dispose() : dbTrans = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""

            End Try

        End Function

        '-- 혈액입고 삭제
        Public Shared Function fnExe_BldIn_Del(ByVal rsBldNo As String, ByVal rsComCd As String) As Boolean      ' 기존 입고된 성분제제 삭제하기
            Dim sFn As String = "Function fnExe_BldIn_Del() As Boolean"

            Dim dbCn As oracleConnection = GetDbConnection()
            Dim dbTrans As oracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim dbCmd As New oracleCommand

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                ' back up 테이블에 history 남기기!!
                sSql = ""
                sSql += "INSERT INTO lb020h "
                sSql += "SELECT fn_ack_sysdate, :modid, :midip, lb2.* FROM lb020m lb2"
                sSql += " WHERE lb2.bldno = :bldno"
                sSql += "   AND lb2.comcd = :comcd"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTrans
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", oracleType.VarChar).Value = USER_INFO.USRID
                    .Parameters.Add("modip", oracleType.VarChar).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bldno", oracleType.VarChar).Value = rsBldNo
                    .Parameters.Add("comcd", oracleType.VarChar).Value = rsComCd

                    iRet = .ExecuteNonQuery()
                End With

                sSql = ""
                sSql += "DELETE lb020m"
                sSql += " WHERE bldno = :bldno"
                sSql += "   AND comcd = :comcd"
                With dbCmd
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("bldno", oracleType.VarChar).Value = rsBldNo
                    .Parameters.Add("comcd", oracleType.VarChar).Value = rsComCd

                    iRet = .ExecuteNonQuery()
                End With

                If iRet > 0 Then
                    dbTrans.Commit()
                    Return True
                Else
                    dbTrans.Rollback()
                    Return False
                End If

            Catch ex As Exception
                dbTrans.Rollback()
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                dbTrans.Dispose() : dbTrans = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

        '-- 초입고 상태로...
        Public Shared Function fnExe_BldIn_Change(ByVal rsBldNo As String, ByVal rsComCd As String) As Boolean      ' 재입고된 성분제제 초입고로 변경하기
            Dim sFn As String = "Function fnExe_BldIn_Change() As Boolean"

            Dim dbCn As oracleConnection = GetDbConnection()
            Dim dbTrans As oracleTransaction = dbCn.BeginTransaction()

            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim dbCmd As New oracleCommand

                ' back up 테이블에 history 남기기!!
                sSql = ""
                sSql += "INSERT INTO lb020h "
                sSql += "SELECT fn_ack_sysdate, :modid, :modip, lb2.* FROM lb020m lb2"
                sSql += " WHERE lb2.bldno = :bldno"
                sSql += "   AND lb2.comcd = :comcd"

                With dbCmd
                    .Connection = dbCn
                    .Transaction = dbTrans
                    .CommandType = CommandType.Text
                    .CommandText = sSql

                    .Parameters.Clear()
                    .Parameters.Add("modid", oracleType.VarChar).Value = USER_INFO.USRID
                    .Parameters.Add("modip", oracleType.VarChar).Value = USER_INFO.LOCALIP
                    .Parameters.Add("bldno", oracleType.VarChar).Value = rsBldNo
                    .Parameters.Add("comcd", oracleType.VarChar).Value = rsComCd

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
                    .Parameters.Add("bldno", oracleType.VarChar).Value = rsBldNo
                    .Parameters.Add("comcd", oracleType.VarChar).Value = rsComCd

                    iRet = .ExecuteNonQuery()

                End With

                If iRet > 0 Then
                    dbTrans.Commit()
                    Return True
                Else
                    dbTrans.Rollback()
                    Return False
                End If

            Catch ex As Exception
                dbTrans.Rollback()
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            Finally

                dbTrans.Dispose() : dbTrans = Nothing
                If dbCn.State = ConnectionState.Open Then dbCn.Close()
                dbCn.Dispose() : dbCn = Nothing

                COMMON.CommFN.MdiMain.DB_Active_YN = ""
            End Try

        End Function

    End Class

    '-- 수혈의뢰 접수
    Public Class JubSu
        Inherits SqlFn

        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.Bef" + vbTab
        Private m_DbCn As OracleConnection
        Private m_DbTrans As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_DbTrans = m_DbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Private Function fnGet_Sysdate() As String
            Dim sFn As String = "Private Function fnGet_Sysdate() As String"
            Dim DbCmd As New oracleCommand
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

        Private Function fnGet_TnsNum(ByVal rsJusuDt As String) As String
            ' 수혈의뢰접수 번호 생성
            Dim sFn As String = "Public Shared Function fnGet_TnsNum() As String"
            Dim DbCmd As New OracleCommand

            Try
                Dim iTnsNo As Integer = 0

                With DbCmd
                    DbCmd.Connection = m_DbCn
                    DbCmd.Transaction = m_DbTrans
                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "pro_ack_exe_seqno_tns"

                    .Parameters.Clear()

                    .Parameters.Add(New OracleParameter("rs_seqymd", OracleType.VarChar, rsJusuDt.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsJusuDt))

                    .Parameters.Add("rn_seqno", OracleType.Number)
                    .Parameters("rn_seqno").Direction = ParameterDirection.InputOutput
                    .Parameters("rn_seqno").Value = -1

                    .ExecuteNonQuery()

                    iTnsNo = CType(.Parameters(1).Value, Integer)
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

            Dim sSrvDate As String = fnGet_Sysdate()
            Dim sTnsNo As String = ""

            Dim dbCmd As New OracleCommand
            Dim sSql As String = ""
            Dim iRet As Integer

            With dbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                Dim iQtyCnt As Integer = 0

                ' 수혈의뢰 접수 번호 생성
                sSrvDate = fnGet_Sysdate()
                sTnsNo = fnGet_TnsNum(sSrvDate.Substring(0, 8))

                sTnsNo = sSrvDate.Substring(0, 8) + "T"c + sTnsNo.PadLeft(4, "0"c)
                iQtyCnt = 0

                ' 처방키가 새로 생성 될 경우에만 lb040m insert

                ' 수혈의뢰접수테이블
                sSql = ""
                sSql += "INSERT INTO lb040m"
                sSql += "          ( tnsjubsuno, tnsgbn, regno, patnm, sex, age, deptcd, doctorcd, wardno, roomno,"
                sSql += "            bedno, jubsuid, jubsudt, orddt, owngbn, iogbn, opdt, eryn, bcno_keep, bcno_order, delflg,"
                sSql += "            hregno, regdt, regid, regip, editdt, editid, editip"
                sSql += "          ) "

                If CType(ralArg(0), clsTnsJubsu).OWNGBN = "L" Then
                    sSql += "SELECT :tnsno,"
                    sSql += "       MAX(CASE WHEN a.emergency = 'Y' THEN '3' ELSE b.comgbn END),"
                    sSql += "       a.bunho,"
                    sSql += "       :patnm,"
                    sSql += "       :sex,"
                    sSql += "       :age,"
                    sSql += "       a.gwa,"
                    sSql += "       a.doctor,"
                    sSql += "       MAX(a.ho_dong),"
                    sSql += "       MAX(a.ho_code),"
                    sSql += "       MAX(a.ho_bed),"
                    sSql += "       :jubsuid,                        /* 접수자아이디 */ "
                    sSql += "       :jubsudt,                        /* 접수시간     */"
                    sSql += "       MAX(a.order_date || a.order_time) + '00',"
                    sSql += "       'L' owngbn,"
                    sSql += "       a.in_out_gubun,"
                    sSql += "       MAX(a.opdt),"
                    sSql += "       a.emergency,"
                    sSql += "       '',                      /* 보관검체번호 */"
                    sSql += "       '',                      /* 의뢰검체번호 */"
                    sSql += "       '0',                     /* 삭제여부 */"
                    sSql += "       '',                      /* 도너등록번호 */"
                    sSql += "       fn_ack_sysdate,          /* 등록시간 */"
                    sSql += "       :regid,                       /* 등록자 */"
                    sSql += "       :regip,                       /* 등록자 아이피 */"
                    sSql += "       fn_ack_sysdate,"
                    sSql += "       :editid,"
                    sSql += "       :editip"
                    sSql += "  FROM mts0001_lis a"
                    sSql += " WHERE a.bunho         = :regno"
                    sSql += "   AND a.order_date    = :orddt"
                    sSql += "   AND a.fkocs         = :fkcos"
                    sSql += "   AND a.dc_yn         = 'N'"
                    sSql += "   AND a.hangmog_code  = b.comordcd"
                    sSql += "   AND a.specimen_code = b.spccd"
                    sSql += "   AND b.usdt         <= fn_ack_sysdate"
                    sSql += "   AND b.uedt         >  fn_ack_sysdate"
                Else
                    sSql += "SELECT :tnsno,"
                    sSql += "       MAX(CASE WHEN NVL(a.eryn, ' ') <> ' ' THEN '3' ELSE b.comgbn END),"
                    sSql += "       a.patno,"
                    sSql += "       :patnm,"
                    sSql += "       :sex,"
                    sSql += "       :age,"
                    sSql += "       a.deptcd,"
                    sSql += "       a.orddr,"
                    sSql += "       MAX(a.wardno),"
                    sSql += "       MAX(fn_ack_get_room_bed(a.patno, TO_CHAR(a.orddate, 'yyyymmdd'))),"
                    sSql += "       '',"
                    sSql += "       :jubsuid,                        /* 접수자아이디 */ "
                    sSql += "       :jubsuip,                        /* 접수시간     */"
                    sSql += "       MAX(TO_CHAR(a.orddate, 'yyyymmdd') || TO_CHAR(a.ordtime, 'hh24miss')),"
                    sSql += "       'O' owngbn,"
                    sSql += "       a.iogbn,"
                    sSql += "       MAX(TO_CHAR(a.opexdate, 'yyyymmdd')),             /* 수술일자     */"
                    sSql += "       a.eryn,                  /* 응급여부     */"
                    sSql += "       '',                      /* 보관검체번호 */"
                    sSql += "       '',                      /* 의뢰검체번호 */"
                    sSql += "       '0',                     /* 삭제여부 */"
                    sSql += "       '',                      /* 도너등록번호 */"
                    sSql += "       fn_ack_sysdate,        /* 등록시간 */"
                    sSql += "       :regid,                       /* 등록자 */"
                    sSql += "       :regip,                      /* 등록자 아이피 */"
                    sSql += "       fn_ack_sysdate,"
                    sSql += "       :editid,"
                    sSql += "       :editip"
                    sSql += "  FROM vw_ack_ocs_ord_info a"
                    sSql += " WHERE a.patno            = :regno"
                    sSql += "   AND a.orddate          = TO_DATE(:regno, 'yyyymmdd')"
                    sSql += "   AND a.ordseqno         = :fkocs"
                    sSql += "   AND NVL(a.discyn, 'N') = 'N'"
                    sSql += "   AND a.ordcd    = b.comordcd"
                    sSql += "   AND a.spccd    = b.spccd"
                    sSql += "   AND b.usdt    <= fn_ack_sysdate"
                    sSql += "   AND b.uedt    >  fn_ack_sysdate"
                End If

                dbCmd.Transaction = m_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsNo
                dbCmd.Parameters.Add("patnm", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).PATNM
                dbCmd.Parameters.Add("sex", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).SEX
                dbCmd.Parameters.Add("age", OracleType.Number).Value = CType(ralArg(0), clsTnsJubsu).AGE
                dbCmd.Parameters.Add("jubsuid", OracleType.VarChar).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("jubsudt", OracleType.VarChar).Value = sSrvDate
                dbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                dbCmd.Parameters.Add("regno", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).REGNO
                dbCmd.Parameters.Add("orddt", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).ORDDATE

                If CType(ralArg(0), clsTnsJubsu).OWNGBN = "L" Then
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).FKOCS
                Else
                    dbCmd.Parameters.Add("fkocs", OracleType.Number).Value = CType(ralArg(0), clsTnsJubsu).FKOCS.Split("/"c)(2)
                End If

                iRet = dbCmd.ExecuteNonQuery()

                If iRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                sSql = ""
                sSql += "INSERT INTO lb042m"
                sSql += "          ( tnsjubsuno, comcd, comnm, spccd, ir, filter, reqqnt, befoutqnt, outqnt, rtnqnt,"
                sSql += "            abnqnt, cancelqnt, state, doctorrmk, delflg, pedgbn, regdt, regid, regip, editdt,"
                sSql += "            editid, editip"
                sSql += "          ) "

                If CType(ralArg(0), clsTnsJubsu).OWNGBN = "L" Then
                    sSql += "SELECT :tnsno,"
                    sSql += "       MAX(b.comcd),"
                    sSql += "       MAX(b.comnmd),"
                    sSql += "       MAX(a.specimen_code),"
                    sSql += "       CASE WHEN MAX(b.comgbn) = '4' THEN '1' ELSE '0' END,"
                    sSql += "       CASE WHEN MAX(NVL(b.ftcd, ' ')) = ' ' THEN '0' ELSE '1' END,"
                    sSql += "       :reqqnt,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       '0',"
                    sSql += "       MAX(a.remark),"
                    sSql += "       '0',"
                    sSql += "       '',"
                    sSql += "       fn_ack_sysdate,"
                    sSql += "       :regid,"
                    sSql += "       :regip,"
                    sSql += "       fn_ack_sysdate,"
                    sSql += "       :editid,"
                    sSql += "       :editip"
                    sSql += "  FROM mts0001_lis a, lf120m b"
                    sSql += " WHERE a.bunho         = :regno"
                    sSql += "   AND a.order_date    = :orddt"
                    sSql += "   AND a.fkocs         = :fkcos"
                    sSql += "   AND a.dc_yn         = 'N'"
                    sSql += "   AND a.hangmog_code  = b.comordcd"
                    sSql += "   AND a.specimen_code = b.spccd"
                    sSql += "   AND b.usdt         <= fn_ack_sysdate"
                    sSql += "   AND b.uedt         >  fn_ack_sysdate"
                    sSql += " GROUP BY bunho, order_date, gwa, doctor, in_out_gubun"
                Else
                    sSql += "SELECT :tnsno,"
                    sSql += "       MAX(b.comcd),"
                    sSql += "       MAX(b.comnmd),"
                    sSql += "       MAX(a.spccd),"
                    sSql += "       CASE WHEN MAX(NVL(a.irradyn, 'N')) = 'Y' THEN '1' ELSE '' END,"
                    sSql += "       CASE WHEN MAX(NVL(a.filtyn,  'N')) = 'Y' THEN '1' ELSE '' END,"
                    sSql += "       :reqqnt,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       0,"
                    sSql += "       '0',"
                    sSql += "       MAX(a.remark),"
                    sSql += "       '0',"
                    sSql += "       '',"
                    sSql += "       fn_ack_sysdate,"
                    sSql += "       :regid,"
                    sSql += "       :regip,"
                    sSql += "       fn_ack_sysdate,"
                    sSql += "       editid,"
                    sSql += "       editip"
                    sSql += "  FROM vw_ack_ocs_ord_info a, lf120m b"
                    sSql += " WHERE a.patno            = :regno"
                    sSql += "   AND a.orddate          = TO_DATE(:orddt, 'yyyymmdd') "
                    sSql += "   AND a.ordseqno         = :fkcos"
                    sSql += "   AND NVL(a.discyn, 'N') = 'N'"
                    sSql += "   AND a.ordcd            = b.comordcd"
                    sSql += "   AND a.spccd            = b.spccd"
                    sSql += "   AND b.usdt            <= fn_ack_sysdate"
                    sSql += "   AND b.uedt            >  fn_ack_sysdate"
                    sSql += " GROUP BY patno, orddate, deptcd, orddr"
                End If

                dbCmd.Transaction = m_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsNo
                dbCmd.Parameters.Add("regqnt", OracleType.Number).Value = ralArg.Count
                dbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                dbCmd.Parameters.Add("bunho", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).REGNO
                dbCmd.Parameters.Add("orddt", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).ORDDATE

                If CType(ralArg(0), clsTnsJubsu).OWNGBN = "L" Then
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).FKOCS.Split("|"c)(3)
                Else
                    dbCmd.Parameters.Add("fkocs", OracleType.Number).Value = CType(ralArg(0), clsTnsJubsu).FKOCS.Split("|"c)(3).Split("/"c)(2)
                End If

                iRet = dbCmd.ExecuteNonQuery()

                If iRet = 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                For ix As Integer = 0 To ralArg.Count - 1

                    ' 수혈의뢰세부내역
                    sSql = ""
                    sSql += "INSERT INTO lb043m"
                    sSql += "          ( tnsjubsuno, comcd, owngbn, iogbn, fkocs, seq, regno, bcno, comnm, spccd, abo,"
                    sSql += "            rh, bldno, state, comcd_out, ocsapply, ir, filter, pedgbn,"
                    sSql += "            orddt, ordslip, ocs_key, regdt, regid,"
                    sSql += "            regip, editdt, editid, editip"
                    sSql += "          ) "
                    If CType(ralArg(ix), clsTnsJubsu).OWNGBN = "L" Then
                        sSql += "SELECT :tnsno, "
                        sSql += "       b.comcd,"
                        sSql += "       'L' owngbn,"
                        sSql += "       a.in_out_gubun,"
                        sSql += "       a.fkocs,"
                        sSql += "       :seq,"
                        sSql += "       a.bunho,"
                        sSql += "       '',"
                        sSql += "       b.comnmd,"
                        sSql += "       a.specimen_code,"
                        sSql += "       r.abo,"
                        sSql += "       r.rh,"
                        sSql += "       '',"
                        sSql += "       '1',"
                        sSql += "       b.comcd,"
                        sSql += "       '',"
                        sSql += "       CASE WHEN b.comgbn = '4' THEN '1' ELSE '0' END,"
                        sSql += "       CASE WHEN NVL(b.ftcd, ' ') = ' ' THEN '0' ELSE '1' END,"
                        sSql += "       '0',"
                        sSql += "       order_date || order_time || '00',"
                        sSql += "       NULL,"
                        sSql += "       NULL,"
                        sSql += "       fn_ack_sysdate,"
                        sSql += "       :regid,"
                        sSql += "       :regip,"
                        sSql += "       fn_ack_sysdate,"
                        sSql += "       :editid,"
                        sSql += "       :editip"
                        sSql += "  FROM mts0001_lis a, lf120m b, lr070m r"
                        sSql += " WHERE a.bunho         = :regno"
                        sSql += "   AND a.order_date    = :orddt"
                        sSql += "   AND a.fkocs         = :fkcos"
                        sSql += "   AND a.dc_yn         = 'N'"
                        sSql += "   AND a.hangmog_code  = b.comordcd"
                        sSql += "   AND a.specimen_code = b.spccd"
                        sSql += "   AND b.usdt         <= fn_ack_sysdate"
                        sSql += "   AND b.uedt         >  fn_ack_sysdate"
                        sSql += "   AND a.bunho         = r.regno(+)"
                    Else
                        sSql += "SELECT :tnsno,"
                        sSql += "       b.comcd,"
                        sSql += "       'O' owngbn,"
                        sSql += "       a.iogbn,"
                        sSql += "       a.patno || '/' || TO_CHAR(a.orddate, 'yyyymmdd') || '/'||TO_CHAR(a.ordseqNO) fkocs,"
                        sSql += "       :seq,"
                        sSql += "       a.patno,"
                        sSql += "       '',"
                        sSql += "       b.comnmd,"
                        sSql += "       a.spccd,"
                        sSql += "       r.abo,"
                        sSql += "       r.rh,"
                        sSql += "       '',"
                        sSql += "       '1',"
                        sSql += "       b.comcd,"
                        sSql += "       '',"
                        sSql += "       CASE WHEN a.irradyn = 'Y' THEN '1' ELSE '' END,"
                        sSql += "       CASE WHEN a.filtyn  = 'Y' THEN '1' ELSE '' END,"
                        sSql += "       '0',"
                        sSql += "       TO_CHAR(a.orddate, 'yyyymmdd') || TO_CHAR(a.ordtime, 'hh24miss'), "
                        sSql += "       a.slipcd,"
                        sSql += "       a.ordseqno,"
                        sSql += "       fn_ack_sysdate,"
                        sSql += "       :regid,"
                        sSql += "       :regip,"
                        sSql += "       fn_ack_sysdate,"
                        sSql += "       :editid,"
                        sSql += "       :editip"
                        sSql += "  FROM vw_ack_ocs_ord_info a, lf120m b, lr070m r"
                        sSql += " WHERE a.patno            = :regno"
                        sSql += "   AND a.orddate          = TO_DATE(:orddt, 'yyyymmdd')"
                        sSql += "   AND a.ordseqno         = :fkcos"
                        sSql += "   AND NVL(a.discyn, 'N') = 'N'"
                        sSql += "   AND a.ordcd            = b.comordcd "
                        sSql += "   AND a.spccd            = b.spccd"
                        sSql += "   AND b.usdt            <= fn_ack_sysdate"
                        sSql += "   AND b.uedt            >  fn_ack_sysdate"
                        sSql += "   AND a.bunho            = r.regno(+)"

                    End If

                    dbCmd.Transaction = m_DbTrans
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsNo
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = (ix + 1).ToString
                    dbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("regno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).REGNO
                    dbCmd.Parameters.Add("orddt", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).ORDDATE

                    If CType(ralArg(ix), clsTnsJubsu).OWNGBN = "L" Then
                        dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).FKOCS
                    Else
                        dbCmd.Parameters.Add("fkocs", OracleType.Number).Value = CType(ralArg(ix), clsTnsJubsu).FKOCS.Split("/"c)(2)
                    End If

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If
                Next


                sSql = "pro_ack_exe_ocs_tns"

                dbCmd.Transaction = m_DbTrans
                dbCmd.CommandType = CommandType.StoredProcedure
                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).REGNO
                dbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).OWNGBN
                dbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).FKOCS
                dbCmd.Parameters.Add("rs_acptdt", OracleType.VarChar).Value = sSrvDate
                dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                dbCmd.Parameters("ri_retval").Value = -1

                dbCmd.ExecuteNonQuery()

                iRet = CType(dbCmd.Parameters(6).Value, Integer)

                If iRet < 1 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                sSql = ""
                sSql += "UPDATE lb043m SET state = state"
                sSql += " WHERE fkocs  IN (SELECT fkocs FROM lb040m a, lb043m b"
                sSql += "                   WHERE b.fkocs      = :fkcos"
                sSql += "                     AND a.tnsjubsuno = b.tnsjubsuno"
                sSql += "                     AND NVL(a.delflg, '0') = '0'"
                sSql += "                 )"
                sSql += "   AND tnsjubsuno <> :tnsno"

                dbCmd.Transaction = m_DbTrans
                dbCmd.CommandType = CommandType.Text
                dbCmd.CommandText = sSql

                dbCmd.Parameters.Clear()
                dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(ralArg(0), clsTnsJubsu).FKOCS
                dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsNo

                iRet = dbCmd.ExecuteNonQuery()
                If iRet > 0 Then
                    m_DbTrans.Rollback()
                    Return False
                End If

                m_DbTrans.Commit()

                Return True

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

        Public Function fn_CntTnsJubsuData(ByVal ralArg As ArrayList) As Boolean
            '수혈 접수 취소
            Dim sFn As String = "Public Function fn_CntTnsJubsuData(ByVal ral As ArrayList) As Boolean"
            Dim dbCmd As New OracleCommand
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            With dbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                Dim sRegNo As String = ""
                Dim sFkOcs As String = ""
                Dim sOWnGbn As String = ""
                Dim sTnsno As String = ""
                Dim sComcd As String = ""
                Dim sSvrdate As String = fnGet_Sysdate()

                For ix As Integer = 0 To ralArg.Count - 1

                    If sTnsno <> "" And sTnsno <> CType(ralArg(ix), clsTnsJubsu).TNSJUBSUNO Then
                        ' 수혈의뢰정보 마스터(lb040m)

                        sSql = ""
                        sSql += "UPDATE lb040m SET"
                        sSql += "       delflg   = (SELECT delflg FROM lb042m WHERE tnsjubsuno = :tnsno AND comcd = :comcd),"
                        sSql += "       editid   = :editid,"
                        sSql += "       editip   = :edttip,"
                        sSql += "       editdt   = fn_ack_sysdate"
                        sSql += " WHERE tnsjubsuno = :tnsno"

                        dbCmd.Transaction = m_DbTrans
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsno
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = sComcd
                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsno

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' 처방 상태값 변경
                        dbCmd.Transaction = m_DbTrans
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.CommandText = "pro_ack_exe_ocs_tns_cancel"

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = sRegNo
                        dbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = sOWnGbn
                        dbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = sFkOcs.Split("-"c)(0)
                        dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                        dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                        dbCmd.Parameters("ri_retval").Value = -1

                        dbCmd.ExecuteNonQuery()

                        iRet = CType(dbCmd.Parameters(5).Value, Integer)

                        If iRet < 1 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    End If

                    sRegNo = CType(ralArg(ix), clsTnsJubsu).REGNO
                    sFkOcs = CType(ralArg(ix), clsTnsJubsu).FKOCS
                    sOWnGbn = CType(ralArg(ix), clsTnsJubsu).OWNGBN
                    sTnsno = CType(ralArg(ix), clsTnsJubsu).TNSJUBSUNO
                    sComcd = CType(ralArg(ix), clsTnsJubsu).COMCD

                    ' 수혈의뢰 세부 히스토리 입력
                    sSql = ""
                    sSql = fnGet_InsLB043HSql()

                    dbCmd.Transaction = m_DbTrans
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.Number).Value = CType(ralArg(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' 수혈의뢰 세부 상태값 업데이트
                    sSql = ""
                    sSql = fnGet_UpdLB043MStateSql()

                    dbCmd.Transaction = m_DbTrans
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "0"c
                    dbCmd.Parameters.Add("abo", OracleType.VarChar).Value = ""
                    dbCmd.Parameters.Add("rh", OracleType.VarChar).Value = ""
                    dbCmd.Parameters.Add("ocsapply", OracleType.VarChar).Value = ""
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.Number).Value = CType(ralArg(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' 수혈의뢰수량 업데이트
                    sSql = ""
                    sSql += "UPDATE lb042m"
                    sSql += "   SET cancelqnt  = cancelqnt + 1,"
                    If CType(ralArg(ix), clsTnsJubsu).STATE = "3"c Then
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
                    sSql += "   AND comcd      = :comcd"

                    dbCmd.Transaction = m_DbTrans
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).COMCD

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' 가출고인 경우 혈액의 상태 변경이 필요
                    If CType(ralArg(ix), clsTnsJubsu).STATE >= "2"c Then
                        ' lb031m insert
                        sSql = ""
                        sSql = fnGet_InsLB031MSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("rtndt", OracleType.VarChar).Value = sSvrdate
                        dbCmd.Parameters.Add("rtnid", OracleType.VarChar).Value = ""
                        dbCmd.Parameters.Add("rtnreqid", OracleType.VarChar).Value = ""
                        dbCmd.Parameters.Add("rtnreqnm", OracleType.VarChar).Value = ""
                        dbCmd.Parameters.Add("rtnrsncd", OracleType.VarChar).Value = ""
                        dbCmd.Parameters.Add("rtnrsncmt", OracleType.VarChar).Value = ""
                        dbCmd.Parameters.Add("rtnflg", OracleType.VarChar).Value = "0"c

                        If CType(ralArg(ix), clsTnsJubsu).STATE = "2" Then
                            dbCmd.Parameters.Add("keepgbn", OracleType.VarChar).Value = "1"c
                        Else
                            dbCmd.Parameters.Add("keepgbn", OracleType.VarChar).Value = "2"c
                        End If

                        dbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).COMCD
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb030m delete  
                        sSql = fnGet_DelLB030MSql()
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).COMCD
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False


                        End If
                        ' 혈액 히스토리 정보 추가 ( 가출고정보 ) 
                        sSql = ""
                        sSql = fnGet_InsLB020HSql()

                        dbCmd.Transaction = m_DbTrans
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).COMCD

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' 혈액 입고 상태로 변경
                        sSql = ""
                        sSql = fnGet_UpdLB020MStateSql()

                        dbCmd.Transaction = m_DbTrans
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "0"c
                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(ralArg(ix), clsTnsJubsu).COMCD

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                    End If

                Next

                If sTnsno <> "" Then
                    ' 수혈의뢰정보 마스터(lb040m)
                    sSql = ""
                    sSql += "UPDATE lb040m SET"
                    sSql += "       delflg   = (SELECT delflg FROM lb042m WHERE tnsjubsuno = :tnsno AND comcd = :comcd),"
                    sSql += "       editid   = :editid,"
                    sSql += "       editip   = :editip,"
                    sSql += "       editdt   = fn_ack_sysdate"
                    sSql += " WHERE tnsjubsuno = :tnsno"

                    dbCmd.Transaction = m_DbTrans
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsno
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = sComcd
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = sTnsno

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' 처방 상태값 변경
                    dbCmd.Transaction = m_DbTrans
                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_ocs_tns_cancel"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = sRegNo
                    dbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = sOWnGbn
                    dbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = sFkOcs.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                    dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    dbCmd.Parameters("ri_retval").Value = -1

                    dbCmd.ExecuteNonQuery()

                    iRet = CType(dbCmd.Parameters(5).Value, Integer)

                    If iRet < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If
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


    End Class

    '-- 가출고
    Public Class BefOut
        Inherits SqlFn

        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.Bef" + vbTab
        Private m_DbCn As OracleConnection
        Private m_DbTrans As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_DbTrans = m_DbCn.BeginTransaction()
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

        ' 크로스매칭 결과저장
        Public Function fnExe_CrossSave(ByVal r_al_rst As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnExe_CrossSave( ArrayList) As Boolean"

            Dim DbCmd As New OracleCommand

            Try

                With DbCmd
                    .Connection = m_DbCn
                    .Transaction = m_DbTrans
                End With

                Dim sSql As String = ""
                Dim iRet As Integer = 0
                Dim sTestdt As String = fnGet_Sysdate()

                For ix As Integer = 0 To r_al_rst.Count - 1
                    If CType(r_al_rst(ix), clsTnsJubsu).TEMP01 = "I"c Then
                        ' lb030m insert
                        sSql = fnGet_InsLB030MSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("testgbn", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TESTGBN
                        DbCmd.Parameters.Add("testid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("testdt", OracleType.VarChar).Value = sTestdt
                        DbCmd.Parameters.Add("rst1", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST1
                        DbCmd.Parameters.Add("rst2", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST2
                        DbCmd.Parameters.Add("rst3", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST3
                        DbCmd.Parameters.Add("rst4", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST4
                        DbCmd.Parameters.Add("cmrmk", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).CMRMK
                        DbCmd.Parameters.Add("eryn", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).EMER
                        DbCmd.Parameters.Add("ir", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).IR
                        DbCmd.Parameters.Add("filter", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).FILTER
                        DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD
                        DbCmd.Parameters.Add("comnm", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMNM
                        DbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb043 update (bldno, comcd)
                        sSql = fnGet_UpdLB043MBCSSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("state", OracleType.VarChar).Value = "2"c
                        DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD
                        DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO
                        DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020h insert
                        sSql = fnGet_InsLB020HSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020m update (state, statedt)
                        sSql = fnGet_UpdLB020MStateSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("state", OracleType.VarChar).Value = "2"c
                        DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    Else
                        ' lb030h insert
                        sSql = fnGet_InsLB030HSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb030 update
                        sSql = fnGet_UpdLB030MSql("C"c, "")

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("rst1", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST1
                        DbCmd.Parameters.Add("rst2", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST2
                        DbCmd.Parameters.Add("rst3", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST3
                        DbCmd.Parameters.Add("rst4", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RST4
                        DbCmd.Parameters.Add("cmrmk", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).CMRMK
                        DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT
                        DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
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

        ' 크로스매칭 취소 
        Public Function fnExe_CrossCancel(ByVal r_al_rst As ArrayList) As Boolean
            Dim sFn As String = "Public Function fnExe_CrossCancel(ArrayList) As Boolean"
            Dim dbCmd As New OracleCommand
            Try

                Dim sSql As String = ""
                Dim iRet As Integer = 0

                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_DbTrans
                End With

                For ix As Integer = 0 To r_al_rst.Count - 1
                    ' lb043h insert
                    sSql = fnGet_InsLB043HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb043m update -> state : '1', comcd_out : comcd, bldno : ''  
                    sSql = fnGet_UpdLB043MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "1"c
                    dbCmd.Parameters.Add("abo", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).ABO
                    dbCmd.Parameters.Add("rh", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).RH
                    dbCmd.Parameters.Add("ocscost", OracleType.VarChar).Value = "0"
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb030h insert
                    sSql = fnGet_InsLB030HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO


                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb030m delete
                    sSql = fnGet_DelLB030MSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb020h insert state : Cross Matching 취소
                    sSql = fnGet_InsLB020HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb020m update state : '1', statedt : sysdate
                    sSql = ""
                    sSql = fnGet_UpdLB020MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "0"c
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_rst(ix), clsTnsJubsu).COMCD_OUT

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
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

        ' 가출고등록 or 취소
        Public Function fnExe_BefOut(ByVal r_al_BefInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_BefOut(ArrayList) As Boolean"
            Dim dbCmd As New OracleCommand
            Dim sSql As String = ""
            Dim iRet As Integer = 0

            Dim ls_bldno As String
            Dim ls_comcd As String
            Dim ls_tnsNum As String
            Dim ls_PreOutdt As String = fnGet_Sysdate()
            Dim ls_comcdo As String
            Dim ls_owngbn As String
            Dim ls_iogbn As String
            Dim ls_Fkocs As String
            Dim ls_regno As String
            Dim ls_orddate As String
            Dim ls_spccd As String
            Dim Is_abo As String = ""
            Dim Is_rh As String = ""

            With dbCmd
                .Connection = m_DbCn
                .Transaction = m_DbTrans
            End With

            Try
                For ix As Integer = 0 To r_al_BefInfo.Count - 1
                    With CType(r_al_BefInfo(ix), clsTnsJubsu)
                        ls_bldno = .BLDNO.Trim
                        ls_comcd = .COMCD.Trim
                        ls_comcdo = .COMCD_OUT.Trim
                        ls_tnsNum = .TNSJUBSUNO.Trim
                        ls_owngbn = .OWNGBN.Trim
                        ls_iogbn = .IOGBN.Trim
                        ls_Fkocs = .FKOCS.Trim
                        ls_regno = .REGNO.Trim
                        ls_orddate = .ORDDATE.Trim
                        ls_spccd = .SPCCD.Trim

                        Is_abo = .ABO.Trim
                        Is_rh = .RH.Trim
                    End With

                    ' lb030h insert
                    sSql = ""
                    sSql = fnGet_InsLB030HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = dbCmd.ExecuteNonQuery()
                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb030m update   befoutid : userid, befoutdt : sysdate
                    sSql = fnGet_UpdLB030MSql("P"c, rsGbn)
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()

                    If rsGbn = "E"c Then
                        dbCmd.Parameters.Add("befoutid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("befoutdt", OracleType.VarChar).Value = ls_PreOutdt
                    End If

                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    'lb043h insert
                    sSql = ""
                    sSql = fnGet_InsLB043HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb043 update (state)
                    sSql = fnGet_UpdLB043MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()

                    If rsGbn = "E"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "3"c
                    ElseIf rsGbn = "C"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "2"c
                    End If

                    dbCmd.Parameters.Add("abo", OracleType.VarChar).Value = Is_abo
                    dbCmd.Parameters.Add("rh", OracleType.VarChar).Value = Is_rh
                    dbCmd.Parameters.Add("ocsapply", OracleType.VarChar).Value = "0"
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    If rsGbn = "E"c Then
                        ' lb020h insert
                        sSql = ""
                        sSql = fnGet_InsLB020HSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    ElseIf rsGbn = "C"c Then
                        ' lb020h insert
                        sSql = ""
                        sSql = fnGet_InsLB020H_SetStateSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "8"c
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    End If

                    ' lb020m update (state, statedt)
                    sSql = ""
                    sSql = fnGet_UpdLB020MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()

                    If rsGbn = "E"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "3"c
                    ElseIf rsGbn = "C"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "2"c
                    End If

                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD_OUT

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb042m 수량 및 완료 여부 업데이트 
                    sSql = ""
                    sSql = fnGet_UpdLB042MStateSql("가출고" + IIf(rsGbn = "C", "취소", "").ToString)

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).COMCD

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If


                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).REGNO

                    If rsGbn = "E"c Then        '-- 가출고
                        dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "2"c
                    ElseIf rsGbn = "C"c Then    '-- 가출고 취소
                        dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "0"c
                    End If
                    dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = CType(r_al_BefInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_acptdt", OracleType.VarChar).Value = ls_PreOutdt
                    dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                    dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    dbCmd.Parameters("ri_retval").Value = -1

                    dbCmd.ExecuteNonQuery()

                    iRet = CType(dbCmd.Parameters(8).Value, Integer)

                    If iRet < 1 Then
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


    End Class

    '-- 출고
    Public Class Out
        Inherits SqlFn
        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.Out" + vbTab

        Private m_DbCn As OracleConnection
        Private m_DbTrans As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_DbTrans = m_DbCn.BeginTransaction()
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

        ' 출고등록 or 취소
        Public Function fnExe_Out(ByVal r_al_OutInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Out(ArrayList, String) As Boolean"
            Dim dbCmd As New OracleCommand


            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim sOutdt As String = fnGet_Sysdate()

                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_DbTrans
                End With

                For ix As Integer = 0 To r_al_OutInfo.Count - 1
                    'lb043h insert
                    sSql = fnGet_InsLB043HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("trnno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb043 update (state)
                    sSql = ""
                    sSql = fnGet_UpdLB043MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()

                    If rsGbn = "E"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "4"c
                    ElseIf rsGbn = "C"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "3"c
                    End If

                    dbCmd.Parameters.Add("abo", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).ABO
                    dbCmd.Parameters.Add("rh", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).RH
                    dbCmd.Parameters.Add("ocsapply", OracleType.VarChar).Value = "0"
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    If rsGbn = "E"c Then
                        ' lb030h insert
                        sSql = ""
                        sSql = fnGet_InsLB030HSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb030m update   outid : userid, foutdt : sysdate
                        sSql = fnGet_UpdLB030MSql("O"c, rsGbn)
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        If rsGbn = "E"c Then
                            dbCmd.Parameters.Add("outid", OracleType.VarChar).Value = USER_INFO.USRID
                            dbCmd.Parameters.Add("outdt", OracleType.VarChar).Value = sOutdt
                            dbCmd.Parameters.Add("recid", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).RECID
                            dbCmd.Parameters.Add("recnm", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).RECNM
                        End If

                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020h insert
                        sSql = fnGet_InsLB020HSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020m update (state, statedt)
                        sSql = ""
                        sSql = fnGet_UpdLB020MStateSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        If rsGbn = "E"c Then
                            dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "4"c
                        ElseIf rsGbn = "C"c Then
                            dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "3"c
                        End If

                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    ElseIf rsGbn = "C"c Then
                        ' lb030h insert
                        sSql = ""
                        sSql = fnGet_InsLB030HSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb030m update   outid : userid, foutdt : sysdate
                        sSql = fnGet_UpdLB030MSql("O"c, rsGbn)
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020h insert
                        sSql = ""
                        sSql = fnGet_InsLB020H_SetStateSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "9"c
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020m update (state, statedt)
                        sSql = ""
                        sSql = fnGet_UpdLB020MStateSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        If rsGbn = "E"c Then
                            dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "4"c
                        ElseIf rsGbn = "C"c Then
                            dbCmd.Parameters.Add("stete", OracleType.VarChar).Value = "3"c
                        End If

                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                    End If

                    If CType(r_al_OutInfo(ix), clsTnsJubsu).OWNGBN = "O" Then
                        '-- OCS (처리)
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.CommandText = "pro_ack_exe_ocs_tns_bld"

                        dbCmd.Parameters.Clear()

                        If rsGbn = "E"c Then        '-- 출고
                            dbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = "O"c
                        ElseIf rsGbn = "C"c Then    '-- 출고 취소
                            dbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = "CO"
                        End If

                        dbCmd.Parameters.Add("rs_tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("rs_comcd_out", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                        dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                        dbCmd.Parameters("ri_retval").Value = -1

                        dbCmd.ExecuteNonQuery()

                        iRet = CType(dbCmd.Parameters(6).Value, Integer)

                        If iRet < 1 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    End If


                    ' lb042m 수량 및 완료 여부 업데이트 
                    sSql = ""
                    sSql = fnGet_UpdLB042MStateSql("출고" + IIf(rsGbn = "C", "취소", "").ToString)

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).REGNO

                    If rsGbn = "E"c Then        '-- 출고
                        dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "3"c
                    ElseIf rsGbn = "C"c Then    '-- 출고 취소
                        dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "C3"
                    End If

                    dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_acptdt", OracleType.VarChar).Value = sOutdt
                    dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                    dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    dbCmd.Parameters("ri_retval").Value = -1

                    dbCmd.ExecuteNonQuery()

                    iRet = CType(dbCmd.Parameters(8).Value, Integer)

                    If iRet < 1 Then
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

        ' 교차미필 출고
        Public Function fnExe_Out_NotCross(ByVal r_al_OutInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Out_NotCross(ArrayList, String) As Boolean"
            Dim dbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer

                Dim sOutdt As String = fnGet_Sysdate()
                Dim sFkocs As String = ""

                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_DbTrans
                End With


                For ix As Integer = 0 To r_al_OutInfo.Count - 1
                    If rsGbn = "C"c Then
                        ' lb030h insert
                        sSql = ""
                        sSql = fnGet_InsLB030HSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb030m update   outid : userid, foutdt : sysdate
                        sSql = fnGet_DelLB030MSql()
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If


                    Else
                        ' lb030m insert
                        sSql = fnGet_InsLB030MSql("NoCross")
                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO         '1
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT  '2
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO    '3
                        dbCmd.Parameters.Add("testgbn", OracleType.VarChar).Value = "3"c                                               '4
                        dbCmd.Parameters.Add("testid", OracleType.VarChar).Value = ""                                                 '5
                        dbCmd.Parameters.Add("testdt", OracleType.VarChar).Value = ""                                                 '6
                        dbCmd.Parameters.Add("rst1", OracleType.VarChar).Value = ""                                                 '7
                        dbCmd.Parameters.Add("rst2", OracleType.VarChar).Value = ""                                                 '8
                        dbCmd.Parameters.Add("rst3", OracleType.VarChar).Value = ""                                                 '9
                        dbCmd.Parameters.Add("rst4", OracleType.VarChar).Value = ""                                                 '10
                        dbCmd.Parameters.Add("cmrmk", OracleType.VarChar).Value = ""                                                 '11
                        dbCmd.Parameters.Add("eryn", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).EMER          '12
                        dbCmd.Parameters.Add("ir", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).IR            '13
                        dbCmd.Parameters.Add("filter", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FILTER        '14
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD         '15
                        dbCmd.Parameters.Add("comnm", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMNM         '16

                        dbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID                                    '17
                        dbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP                                  '18
                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID                                    '19
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP                                  '20
                        dbCmd.Parameters.Add("outid", OracleType.VarChar).Value = USER_INFO.USRID                                    '21
                        dbCmd.Parameters.Add("outdt", OracleType.VarChar).Value = sOutdt                                             '22
                        dbCmd.Parameters.Add("recid", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).RECID         '23
                        dbCmd.Parameters.Add("recnm", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).RECNM         '24

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                    End If

                    ' lb020h insert
                    sSql = ""
                    sSql = fnGet_InsLB020H_SetStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "9"c
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb020m update (state, statedt)
                    sSql = ""
                    sSql = fnGet_UpdLB020MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()

                    If rsGbn = "E"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "4"c
                    ElseIf rsGbn = "C"c Then
                        dbCmd.Parameters.Add("stete", OracleType.VarChar).Value = "0"c
                    End If

                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If


                    If rsGbn = "C" Then
                        'lb043h insert
                        sSql = fnGet_InsLB043HSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("trnno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD
                        dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                        dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb043 update (state)
                        sSql = ""
                        sSql = fnGet_UpdLB043MStateSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "1"c
                        dbCmd.Parameters.Add("abo", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).ABO
                        dbCmd.Parameters.Add("rh", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).RH
                        dbCmd.Parameters.Add("ocsapply", OracleType.VarChar).Value = "0"
                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD
                        dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                        dbCmd.Parameters.Add("seq", OracleType.Number).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    Else
                        ' lb043 update (bldno, comcd)
                        sSql = ""
                        sSql = fnGet_UpdLB043MBCSSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "4"c
                        dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD
                        dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    End If



                    If CType(r_al_OutInfo(ix), clsTnsJubsu).OWNGBN = "O" Then
                        '-- OCS (처리)
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.CommandText = "pro_ack_exe_ocs_tns_bld"

                        dbCmd.Parameters.Clear()

                        If rsGbn = "E"c Then        '-- 출고
                            dbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = "O"c
                        ElseIf rsGbn = "C"c Then    '-- 출고 취소
                            dbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = "CO"
                        End If

                        dbCmd.Parameters.Add("rs_tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("rs_comcd_out", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                        dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                        dbCmd.Parameters("ri_retval").Value = -1

                        dbCmd.ExecuteNonQuery()

                        iRet = CType(dbCmd.Parameters(6).Value, Integer)

                        If iRet < 1 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If
                    End If

                    ' lb042m 수량 및 완료 여부 업데이트 
                    sSql = ""
                    sSql = fnGet_UpdLB042MStateSql("출고" + IIf(rsGbn = "C", "취소", "").ToString)

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    Dim dbDa As OracleDataAdapter
                    Dim dt As New DataTable

                    sSql = ""
                    sSql += "SELECT fkocs"
                    sSql += "  FROM lb043m"
                    sSql += " WHERE tnsjubsuno = :tnsno"
                    sSql += "   AND comcd      = :comcd"
                    sSql += "   AND bldno      = :bldno"

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbDa = New OracleDataAdapter(dbCmd)

                    With dbDa
                        .SelectCommand.Parameters.Clear()
                        .SelectCommand.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        .SelectCommand.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD
                        .SelectCommand.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                    End With

                    dt.Reset()
                    dbDa.Fill(dt)

                    If dt.Rows.Count < 1 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    sFkocs = dt.Rows(0).Item("fkocs").ToString

                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).REGNO

                    If rsGbn = "E"c Then        '-- 출고
                        dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "3"c
                    ElseIf rsGbn = "C"c Then    '-- 출고 취소
                        dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "C3"
                    End If

                    dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = sFkocs
                    dbCmd.Parameters.Add("rs_acptdt", OracleType.VarChar).Value = sOutdt
                    dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                    dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    dbCmd.Parameters("ri_retval").Value = -1

                    dbCmd.ExecuteNonQuery()

                    iRet = CType(dbCmd.Parameters(8).Value, Integer)

                    If iRet = 0 Then
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
                    .Transaction = m_DbTrans
                End With

                For ix As Integer = 0 To r_al_OutInfo.Count - 1

                    If CType(r_al_OutInfo(ix), clsTnsJubsu).OWNGBN = "O" Then
                        '-- OCS (처리)
                        dbCmd.CommandType = CommandType.StoredProcedure
                        dbCmd.CommandText = "pro_ack_exe_ocs_tns_bld"

                        dbCmd.Parameters.Clear()

                        If rsGbn = "E"c Then        '-- 출고
                            dbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = "O"c
                        ElseIf rsGbn = "C"c Then    '-- 출고 취소
                            dbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = "CO"
                        End If

                        dbCmd.Parameters.Add("rs_tnsno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).TNSJUBSUNO
                        dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("rs_comcd_out", OracleType.VarChar).Value = CType(r_al_OutInfo(ix), clsTnsJubsu).COMCD_OUT
                        dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                        dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                        dbCmd.Parameters("ri_retval").Value = -1

                        dbCmd.ExecuteNonQuery()

                        iRet += CType(dbCmd.Parameters(6).Value, Integer)


                    End If

                Next

                If iRet < 1 Then
                    m_DbTrans.Rollback()
                    Return False
                Else
                    m_DbTrans.Commit()
                    Return True
                End If

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

    End Class

    '-- 반납/폐기/자체폐기
    Public Class Rtn
        Inherits SqlFn

        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.Rtn" + vbTab

        Private m_DbCn As OracleConnection
        Private m_DbTrans As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_DbTrans = m_DbCn.BeginTransaction()
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

        ' 혈액 반납/폐기 작업
        Public Function fnExe_Rtn(ByVal r_al_RtnInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_Rtn(ArrayList, String) As Boolean"

            Dim dbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer = 0

                Dim sRtndt As String = fnGet_Sysdate()

                With dbCmd
                    .Connection = m_DbCn
                    .Transaction = m_DbTrans
                End With

                For ix As Integer = 0 To r_al_RtnInfo.Count - 1

                    'lb043h insert
                    sSql = fnGet_InsLB043HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb043 update (state)
                    sSql = fnGet_UpdLB043MStateSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    If rsGbn = "R"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "5"c
                    ElseIf rsGbn = "A"c Then
                        dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "6"c
                    End If

                    dbCmd.Parameters.Add("abo", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).ABO
                    dbCmd.Parameters.Add("rh", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RH
                    dbCmd.Parameters.Add("ocsapply", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TEMP01
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD
                    dbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("seq", OracleType.Number).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb031m insert
                    sSql = fnGet_InsLB031MSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rtndt", OracleType.VarChar).Value = sRtndt
                    dbCmd.Parameters.Add("retid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rtnreqid", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNREQID
                    dbCmd.Parameters.Add("rtnreqnm", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNREQNM
                    dbCmd.Parameters.Add("rtnrsncd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNCODE
                    dbCmd.Parameters.Add("rtnrsncmt", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNCMT

                    If rsGbn = "R"c Then
                        dbCmd.Parameters.Add("rtnflg", OracleType.VarChar).Value = "1"c
                    Else
                        dbCmd.Parameters.Add("rtnflg", OracleType.VarChar).Value = "2"c
                    End If

                    If rsGbn = "R"c Then
                        dbCmd.Parameters.Add("keepgbn", OracleType.VarChar).Value = "3"c
                    Else
                        dbCmd.Parameters.Add("keepgbn", OracleType.VarChar).Value = "4"c
                    End If

                    dbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb030h insert
                    sSql = fnGet_InsLB030HSql()

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb030m delete  
                    sSql = fnGet_DelLB030MSql()
                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If


                    ' 필터가 아닐경우에만 혈액테이블 업데이트 처리
                    If CType(r_al_RtnInfo(ix), clsTnsJubsu).FILTER <> "1"c Then

                        ' lb020h insert
                        sSql = fnGet_InsLB020HSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()
                        dbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020m update (state, statedt)
                        sSql = fnGet_UpdLB020MStateSql()

                        dbCmd.CommandType = CommandType.Text
                        dbCmd.CommandText = sSql

                        dbCmd.Parameters.Clear()

                        If rsGbn = "R"c Then
                            dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "0"c
                        ElseIf rsGbn = "A"c Then
                            dbCmd.Parameters.Add("state", OracleType.VarChar).Value = "6"c
                        End If

                        dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        dbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                        dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = dbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        If CType(r_al_RtnInfo(ix), clsTnsJubsu).OWNGBN = "O" Then
                            '-- OCS (처리)
                            dbCmd.CommandType = CommandType.StoredProcedure
                            dbCmd.CommandText = "pro_ack_exe_ocs_tns_bld"

                            dbCmd.Parameters.Clear()

                            dbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = rsGbn
                            dbCmd.Parameters.Add("rs_tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                            dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                            dbCmd.Parameters.Add("rs_comcd_out", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                            dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                            dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                            dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                            dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                            dbCmd.Parameters("ri_retval").Value = -1

                            dbCmd.ExecuteNonQuery()

                            iRet = CType(dbCmd.Parameters(6).Value, Integer)

                            If iRet < 1 Then
                                m_DbTrans.Rollback()
                                Return False
                            End If

                        End If

                    End If

                    ' lb042m 수량 및 완료 여부 업데이트 
                    sSql = ""
                    sSql = fnGet_UpdLB042MStateSql(IIf(rsGbn = "R", "반납", "폐기").ToString)

                    dbCmd.CommandType = CommandType.Text
                    dbCmd.CommandText = sSql

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    dbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    dbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD

                    iRet = dbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    dbCmd.CommandType = CommandType.StoredProcedure
                    dbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg"

                    dbCmd.Parameters.Clear()
                    dbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).REGNO

                    If rsGbn = "R"c Then        '-- 반납
                        dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "4"c
                    ElseIf rsGbn = "A"c Then    '-- 폐기
                        If CType(r_al_RtnInfo(ix), clsTnsJubsu).TEMP01 = "1" Then
                            dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "A"c
                        Else
                            dbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "5"c
                        End If
                    End If

                    dbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    dbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).OWNGBN
                    dbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    dbCmd.Parameters.Add("rs_acptdt", OracleType.VarChar).Value = sRtndt
                    dbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                    dbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    dbCmd.Parameters.Add("ri_retval", OracleType.Number)
                    dbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    dbCmd.Parameters("ri_retval").Value = -1

                    dbCmd.ExecuteNonQuery()

                    iRet = CType(dbCmd.Parameters(8).Value, Integer)

                    If iRet < 1 Then
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
                    .Transaction = m_DbTrans
                End With

                For ix As Integer = 0 To r_al_RtnInfo.Count - 1

                    'lb043h insert
                    sSql = fnGet_InsLB043HSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD
                    DbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    DbCmd.Parameters.Add("seq", OracleType.Number).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb043 update (state)
                    sSql = fnGet_UpdLB043MStateSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("state", OracleType.VarChar).Value = "4"c
                    DbCmd.Parameters.Add("abo", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).ABO
                    DbCmd.Parameters.Add("rh", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RH
                    DbCmd.Parameters.Add("ocsapply", OracleType.VarChar).Value = "0"
                    DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD
                    DbCmd.Parameters.Add("fkocs", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    DbCmd.Parameters.Add("seq", OracleType.Number).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(1)

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb030m insert
                    sSql = fnGet_InsLB030MRtnCancelSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb031h insert
                    sSql = fnGet_InsLB031HSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb031m delete
                    sSql = ""
                    sSql = fnGet_DelLB031mSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If


                    ' 필터가 아닐경우에만 혈액테이블 업데이트 처리
                    If CType(r_al_RtnInfo(ix), clsTnsJubsu).FILTER <> "1"c Then

                        ' lb020h insert
                        sSql = fnGet_InsLB020HSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()
                        DbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        ' lb020m update (state, statedt)
                        sSql = fnGet_UpdLB020MStateSql()

                        DbCmd.CommandType = CommandType.Text
                        DbCmd.CommandText = sSql

                        DbCmd.Parameters.Clear()

                        DbCmd.Parameters.Add("state", OracleType.VarChar).Value = "4"c
                        DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                        DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT

                        iRet = DbCmd.ExecuteNonQuery()

                        If iRet = 0 Then
                            m_DbTrans.Rollback()
                            Return False
                        End If

                        If CType(r_al_RtnInfo(ix), clsTnsJubsu).OWNGBN = "O" Then
                            '-- OCS (처리)
                            DbCmd.CommandType = CommandType.StoredProcedure
                            DbCmd.CommandText = "pro_ack_exe_ocs_tns_bld"

                            DbCmd.Parameters.Clear()

                            DbCmd.Parameters.Add("rs_jobgbn", OracleType.VarChar).Value = "C" + rsGbn
                            DbCmd.Parameters.Add("rs_tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                            DbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                            DbCmd.Parameters.Add("rs_comcd_out", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                            DbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                            DbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                            DbCmd.Parameters.Add("ri_retval", OracleType.Number)
                            DbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                            DbCmd.Parameters("ri_retval").Value = -1

                            DbCmd.ExecuteNonQuery()

                            iRet = CType(DbCmd.Parameters(6).Value, Integer)

                            If iRet < 1 Then
                                m_DbTrans.Rollback()
                                Return False
                            End If
                        End If
                    End If

                    ' lb042m 수량 및 완료 여부 업데이트 

                    sSql = fnGet_UpdLB042MStateSql(IIf(rsGbn = "R", "반납취소", "폐기취소").ToString)

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).TNSJUBSUNO
                    DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    DbCmd.CommandType = CommandType.StoredProcedure
                    DbCmd.CommandText = "pro_ack_exe_ocs_tns_rstflg"

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("rs_regno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).REGNO

                    If rsGbn = "R" Then
                        DbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "C4"
                    Else
                        DbCmd.Parameters.Add("rs_rstflg", OracleType.VarChar).Value = "C5"
                    End If

                    DbCmd.Parameters.Add("rs_bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    DbCmd.Parameters.Add("rs_owngbn", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).OWNGBN
                    DbCmd.Parameters.Add("rs_fkocs", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).FKOCS.Split("-"c)(0)
                    DbCmd.Parameters.Add("rs_acptdt", OracleType.VarChar).Value = sRtndt
                    DbCmd.Parameters.Add("rs_usrid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rs_ip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("ri_retval", OracleType.Number)
                    DbCmd.Parameters("ri_retval").Direction = ParameterDirection.InputOutput
                    DbCmd.Parameters("ri_retval").Value = -1

                    DbCmd.ExecuteNonQuery()

                    iRet = CType(DbCmd.Parameters(8).Value, Integer)

                    If iRet < 1 Then
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

        ' 혈액 자체폐기/교환
        Public Function fnExe_SelfAbn(ByVal r_al_RtnInfo As ArrayList, ByVal rsGbn As String) As Boolean
            Dim sFn As String = "Public Function fnExe_SelfAbn(ArrayList, String) As Boolean"
            Dim DbCmd As New OracleCommand

            Try
                Dim sSql As String = ""
                Dim iRet As Integer

                Dim sRtndt As String = fnGet_Sysdate()

                With DbCmd
                    .Connection = m_DbCn
                    .Transaction = m_DbTrans
                End With

                For ix As Integer = 0 To r_al_RtnInfo.Count - 1

                    ' lb031m insert
                    sSql = fnGet_InsLB031MSelfSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT
                    DbCmd.Parameters.Add("rtndt", OracleType.VarChar).Value = sRtndt
                    DbCmd.Parameters.Add("retid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("reqid", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNREQID
                    DbCmd.Parameters.Add("reqnm", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNREQNM
                    DbCmd.Parameters.Add("rtncd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNCODE
                    DbCmd.Parameters.Add("rtncmt", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).RTNCMT
                    DbCmd.Parameters.Add("rtnflg", OracleType.VarChar).Value = "2"c
                    DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD

                    If rsGbn = "A"c Then
                        DbCmd.Parameters.Add("keepgbn", OracleType.VarChar).Value = "5"c
                    Else
                        DbCmd.Parameters.Add("keepgbn", OracleType.VarChar).Value = "6"c
                    End If

                    DbCmd.Parameters.Add("keepid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb020h insert
                    sSql = fnGet_InsLB020HSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()
                    DbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
                        m_DbTrans.Rollback()
                        Return False
                    End If

                    ' lb020m update (state, statedt)
                    sSql = fnGet_UpdLB020MStateSql()

                    DbCmd.CommandType = CommandType.Text
                    DbCmd.CommandText = sSql

                    DbCmd.Parameters.Clear()

                    If rsGbn = "A"c Then
                        DbCmd.Parameters.Add("state", OracleType.VarChar).Value = "6"c
                    Else
                        DbCmd.Parameters.Add("state", OracleType.VarChar).Value = "5"c
                    End If

                    DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).BLDNO
                    DbCmd.Parameters.Add("comcd", OracleType.VarChar).Value = CType(r_al_RtnInfo(ix), clsTnsJubsu).COMCD_OUT

                    iRet = DbCmd.ExecuteNonQuery()

                    If iRet = 0 Then
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

    End Class

    '-- ABO, Rh 2차 결과등록
    Public Class RegAboRh
        Private Const msFile As String = "File : CGLISAPP_BT.vb, Class : APP_BT.RegAboRh" + vbTab
        Private m_DbCn As OracleConnection
        Private m_DbTrans As OracleTransaction

        Public Sub New()
            m_DbCn = GetDbConnection()
            m_DbTrans = m_DbCn.BeginTransaction()
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Sub

        Public Function fnExe_Reg_Rst(ByVal raData As ArrayList, ByVal rsUsrId As String) As String

            Dim sFn As String = "Public Function fnExe_Reg_Rst(ArrayList, String) As string"
            Dim DbCmd As New OracleCommand

            Try
                With DbCmd
                    .Connection = m_DbCn
                    .Transaction = m_DbTrans
                End With

                For ix As Integer = 0 To raData.Count - 1
                    Dim sBcNo As String = raData.Item(ix).ToString.Split("|"c)(0)
                    Dim sTestCd As String = raData.Item(ix).ToString.Split("|"c)(1)
                    Dim sRst As String = raData.Item(ix).ToString.Split("|"c)(2)

                    Dim sSql As String = ""
                    Dim iRet As Integer = 0

                    sSql += "UPDATE lb070m SET rstval = :rstval, regdt = fn_ack_sysdate, regid = :regid"
                    sSql += " WHERE bcno   = :bldno"
                    sSql += "   AND testcd = :testcd"

                    With DbCmd
                        .CommandText = sSql
                        .CommandType = CommandType.Text

                        .Parameters.Clear()
                        .Parameters.Add("rstval", OracleType.VarChar).Value = sRst
                        .Parameters.Add("regid", OracleType.VarChar).Value = rsUsrId
                        .Parameters.Add("bcno", OracleType.VarChar).Value = sBcNo
                        .Parameters.Add("testcd", OracleType.VarChar).Value = sTestCd

                        iRet = .ExecuteNonQuery()
                    End With

                    If iRet = 0 Then
                        sSql = ""
                        sSql += "INSERT INTO lb070m (bcno, testcd, rstval, regdt, regid)"
                        sSql += "    VALUES( :bldno, :testcd, :rstval, fn_ack_sysdate, :regid)"

                        With DbCmd
                            .CommandText = sSql
                            .CommandType = CommandType.Text

                            .Parameters.Clear()
                            .Parameters.Add("bcno", OracleType.VarChar).Value = sBcNo
                            .Parameters.Add("testcd", OracleType.VarChar).Value = sTestCd
                            .Parameters.Add("rstval", OracleType.VarChar).Value = sRst
                            .Parameters.Add("regid", OracleType.VarChar).Value = rsUsrId

                            iRet = .ExecuteNonQuery()
                        End With

                    End If
                Next

                m_DbTrans.Commit()
                Return ""

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
                DbCmd.Parameters.Add("bcnokeep", OracleType.VarChar).Value = rsKeepNo
                DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = rsTnsnum

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
                DbCmd.Parameters.Add("bcnoord", OracleType.VarChar).Value = rsOrderNum
                DbCmd.Parameters.Add("bcnokeep", OracleType.VarChar).Value = rsKeepNum
                DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = rsTnsNum

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
                DbCmd.Parameters.Add("keepspcno", OracleType.VarChar).Value = rsKeepNum
                DbCmd.Parameters.Add("regno", OracleType.VarChar).Value = rsRegno
                DbCmd.Parameters.Add("ustm", OracleType.VarChar).Value = rsColldt.Replace("-"c, "").Replace(":"c, "").Replace(" ", "")
                DbCmd.Parameters.Add("uetm", OracleType.VarChar).Value = sUeDt.Replace("-"c, "").Replace(":"c, "").Replace(" ", "")
                DbCmd.Parameters.Add("bloodtype", OracleType.VarChar).Value = rsAbo + rsRh
                DbCmd.Parameters.Add("abo", OracleType.VarChar).Value = rsAbo
                DbCmd.Parameters.Add("rh", OracleType.VarChar).Value = rsRh
                DbCmd.Parameters.Add("keepspcbcno", OracleType.VarChar).Value = rsOrderNum

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
                DbCmd.Parameters.Add("bcnokeep", OracleType.VarChar).Value = ""
                DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = rsTnsnum

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
                    With CType(ralArg(i), clsTnsJubsu)
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
                    DbCmd.Parameters.Add("modid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("modip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = ls_bldno
                    DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = ls_comcd
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = ls_tnsNum

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

                    DbCmd.Parameters.Add("keepgbn", OracleType.VarChar).Value = rsGbn
                    DbCmd.Parameters.Add("keepid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("keeptm", OracleType.VarChar).Value = ls_keepdt

                    If rsGbn = "2"c Then
                        DbCmd.Parameters.Add("outid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("outdt", OracleType.VarChar).Value = ls_keepdt
                        DbCmd.Parameters.Add("recid", OracleType.VarChar).Value = ls_recid
                        DbCmd.Parameters.Add("rednm", OracleType.VarChar).Value = ls_recnm
                    End If

                    DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = ls_bldno
                    DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = ls_comcd
                    DbCmd.Parameters.Add("tnsno", OracleType.VarChar).Value = ls_tnsNum

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
                    With CType(ralArg(i), clsTnsJubsu)
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
                    DbCmd.Parameters.Add("testid2", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("rst1", OracleType.VarChar).Value = ls_rst1
                    DbCmd.Parameters.Add("rst2", OracleType.VarChar).Value = ls_rst2
                    DbCmd.Parameters.Add("rst3", OracleType.VarChar).Value = ls_rst3
                    DbCmd.Parameters.Add("rst4", OracleType.VarChar).Value = ls_rst4
                    DbCmd.Parameters.Add("cmrmk", OracleType.VarChar).Value = ls_cmrmk
                    DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                    DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                    DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = ls_bldno
                    DbCmd.Parameters.Add("comcdout", OracleType.VarChar).Value = ls_comcd

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
                        DbCmd.Parameters.Add("testid2", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("rst1", OracleType.VarChar).Value = ls_rst1
                        DbCmd.Parameters.Add("rst2", OracleType.VarChar).Value = ls_rst2
                        DbCmd.Parameters.Add("rst3", OracleType.VarChar).Value = ls_rst3
                        DbCmd.Parameters.Add("rst4", OracleType.VarChar).Value = ls_rst4
                        DbCmd.Parameters.Add("cmrmk", OracleType.VarChar).Value = ls_cmrmk
                        DbCmd.Parameters.Add("regid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("regip", OracleType.VarChar).Value = USER_INFO.LOCALIP
                        DbCmd.Parameters.Add("editid", OracleType.VarChar).Value = USER_INFO.USRID
                        DbCmd.Parameters.Add("editip", OracleType.VarChar).Value = USER_INFO.LOCALIP

                        DbCmd.Parameters.Add("bldno", OracleType.VarChar).Value = ls_bldno
                        DbCmd.Parameters.Add("comcdoout", OracleType.VarChar).Value = ls_comcd

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

    Public Class CGDA_BT
        Inherits SqlFn

        Private Const msFile As String = "File : CGDA_BT.vb, Class : B01" & vbTab
  
#Region " X-Matching 현황 조회"
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

                alParm.Add(New OracleParameter("testdts", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("testdte", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

                alParm.Add(New OracleParameter("testdts", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("testdte", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

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

            sSql += "SELECT a.dptward, a.comcd, SUM(a.cnt) cnt"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END dptward,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb031m b3,"
            sSql += "               lb040m b4"
            sSql += "         WHERE b3.testdt >= :testdts"
            sSql += "           AND b3.testdt <= :testdte"
            sSql += "           AND b3.tnsjubsuno = b4.tnsjubsuno"
            sSql += "           AND b4.owngbn <> 'H'"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "         UNION "
            sSql += "        SELECT CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END dptward,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb030m b3,"
            sSql += "               lb040m b4"
            sSql += "         WHERE b3.testdt >= :testdts"
            sSql += "           AND b3.testdt <= :testdte"
            sSql += "           AND b3.tnsjubsuno = b4.tnsjubsuno"
            sSql += "           AND b4.owngbn <> 'H'"
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "       ) a"
            sSql += " GROUP BY a.dptward, a.comcd"
            sSql += " ORDER BY dptward, comcd"

            alParm.Add(New OracleParameter("testdts", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("testdte", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            alParm.Add(New OracleParameter("testdts", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("testdte", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

                alParm.Add(New OracleParameter("opendt", OracleType.VarChar, PRG_CONST.OPEN_DATE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.OPEN_DATE))

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))
                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

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
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END dptward,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb031m b3"
            sSql += "               LEFT OUTER JOIN"
            sSql += "                    lb040m b4 ON (b3.tnsjubsuno = b4.tnsjubsuno)"
            sSql += "         WHERE b3.rtndt >= :dates"
            sSql += "           AND b3.rtndt <= :datee"
            sSql += "           AND b3.rtndt >= :opendt"
            sSql += "           AND b3.rtnflg = '2'"      ' 1 : 반납, 2 : 폐기
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "       ) a"
            'sSql += " GROUP BY a.dptward, a.comcd"
            sSql += " UNION "
            sSql += "SELECT a.dptward, a.comcd, 'O' gbn, a.cnt"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END dptward,"
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
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END, b3.comcd_out"
            sSql += "       ) a"
            'sSql += " GROUP BY a.dptward, a.comcd, a.cnt"
            sSql += " ORDER BY dptward, comcd"

            alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            alParm.Add(New OracleParameter("opendt", OracleType.VarChar, PRG_CONST.OPEN_DATE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.OPEN_DATE))

            alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))
            alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            Try
                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        Public Shared Function fnGet_OutAbn_DeptDrWithBld(ByVal rsDateS As String, ByVal rsDateE As String, ByVal rsDayGbn As String) As DataTable
            Dim sFn As String = "Function fnGet_BldInfo( String, String) As DataTable "

            Dim sSql As String = ""
            Dim alParm As New ArrayList

            sSql += "SELECT a.dptward, a.drcd, fn_ack_get_dr_name(a.drcd) drnm, a.comcd, 'R' gbn, a.cnt"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END dptward,"
            sSql += "               b4.doctorcd drcd,"
            sSql += "               comcd_out comcd, COUNT(*) cnt"
            sSql += "          FROM lb031m b3"
            sSql += "               LEFT OUTER JOIN"
            sSql += "                    lb040m b4 ON (b3.tnsjubsuno = b4.tnsjubsuno)"
            sSql += "         WHERE b3.rtndt >= :dates"
            sSql += "           AND b3.rtndt <= :datee"
            sSql += "           AND b3.rtndt >= :opendt"
            sSql += "           AND b3.rtnflg = '2'"      ' 1 : 반납, 2 : 폐기
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END, b4.doctorcd, b3.comcd_out"
            sSql += "       ) a"
            'sSql += " GROUP BY a.dptward, a.comcd"
            sSql += " UNION "
            sSql += "SELECT a.dptward, a.drcd, fn_ack_get_dr_name(a.drcd) drnm, a.comcd, 'O' gbn, a.cnt"
            sSql += "  FROM (SELECT CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END dptward,"
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
            sSql += "         GROUP BY CASE WHEN b4.iogbn = 'I' THEN b4.wardno ELSE NVL(b4.deptcd, '--') END, b4.doctorcd, b3.comcd_out"
            sSql += "       ) a"
            'sSql += " GROUP BY a.dptward, a.comcd, a.cnt"
            sSql += " ORDER BY dptward, drcd, comcd"

            alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

            alParm.Add(New OracleParameter("opendt", OracleType.VarChar, PRG_CONST.OPEN_DATE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, PRG_CONST.OPEN_DATE))

            alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))
            alParm.Add(New OracleParameter("dates", OracleType.VarChar, (rsDateS + "0000").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS + "0000"))
            alParm.Add(New OracleParameter("datee", OracleType.VarChar, (rsDateE + "5959").Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE + "5959"))

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
                sSql += "       CASE WHEN j.iogbn = 'I' THEN j.wardno || '/' || j.roomno ELSE j.deptcd END dept,"
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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

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
                    al.Add(New OracleParameter("bcno", OracleType.VarChar, rsBcNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcNo))
                Else
                    sSql += "   AND r.wkymd   = :wkymd"
                    sSql += "   and r.wkgrpcd = :wkgrp"
                    sSql += "   AND r.wkno   >= :wknos"
                    sSql += "   AND r.wkno   <= :wknoe"

                    al.Add(New OracleParameter("wkymd", OracleType.VarChar, rsWkYmd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkYmd))
                    al.Add(New OracleParameter("wkgrp", OracleType.VarChar, rsWkGrpCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkGrpCd))
                    al.Add(New OracleParameter("wknos", OracleType.VarChar, rsWkNoS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoS))
                    al.Add(New OracleParameter("wknoe", OracleType.VarChar, rsWkNoE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWkNoE))
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

                al.Add(New OracleParameter("dates", OracleType.VarChar, rsTkDtS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtS))
                al.Add(New OracleParameter("datee", OracleType.VarChar, rsTkDtE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTkDtE))

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

                alParm.Add(New OracleParameter("rs_orddt1", OracleType.VarChar, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_orddt2", OracleType.VarChar, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))


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

        Public Shared Function fn_TransfusionSelectJ(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsTnsGbn As String) As DataTable
            '수혈의뢰접수 데이터 트리레벨 1
            Dim sFn As String = "Public Shared Function fn_TransfusionSelectJ(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "pkg_ack_tns.pkg_get_tns_order_j"

                alParm.Add(New OracleParameter("rs_jubsudt1", OracleType.VarChar, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_jubsudt2", OracleType.VarChar, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))


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

            Try

                sSql += "pkg_ack_tns.pkg_get_tns_order_t"

                alParm.Add(New OracleParameter("rs_jubsudt1", OracleType.VarChar, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_jubsudt2", OracleType.VarChar, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

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

                alParm.Add(New OracleParameter("rs_orddt1", OracleType.VarChar, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("rs_orddt2", OracleType.VarChar, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

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

        Public Shared Function fn_GetLatelyTestList(ByVal rsRegno As String) As DataTable
            ' 혈액은행 최근검사결과조회
            Dim sFn As String = "Public Shared Function fn_GetLatelyTestList(ByVal rsRegno As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       r.testcd, r.spccd, f.tnmd,"
                'sSql += "       fn_ack_get_viewrst_regno(r.regno, r.testcd, r.fndt) viewrst,"
                sSql += "       (SELECT viewrst FROM lr010m "
                sSql += "         WHERE regno  = r.regno"
                sSql += "           AND testcd = r.testcd"
                sSql += "           AND rstdt  = r.fndt"
                sSql += "           AND rstflg = '3'"
                sSql += "           AND ROWNUM = 1"
                sSql += "       ) viewrst,"
                sSql += "       fn_ack_date_str(r.tkdt, 'yyyy-mm-dd hh24:mi') tkdt,"
                sSql += "       fn_ack_date_str(r.fndt, 'yyyy-mm-dd hh24:mi') fndt,"
                sSql += "       r.regno,"
                sSql += "       r.dispseq,"
                sSql += "       r.bbgbn,"
                sSql += "       MONTHS_BETWEEN(SYSDATE, TO_DATE(r.fndt, 'yyyymmddhh24miss')) months_between"
                sSql += "  FROM lf060m f,"
                sSql += "       (SELECT r.regno, r.testcd, r.spccd, b.dispseq, b.bbgbn,"
                sSql += "               MAX(r.tkdt)  as tkdt, MAX(r.rstdt) as fndt"
                sSql += "          FROM lr010m r, lf140m b, lj010m j"
                sSql += "         WHERE j.regno   = :regno"

                alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                sSql += "           AND j.spcflg  = '4'"
                sSql += "           AND j.bcno    = r.bcno"
                sSql += "           AND r.rstflg  = '3'"
                sSql += "           AND r.testcd  = b.testcd"
                sSql += "           AND r.spccd   = b.spccd"
                sSql += "           AND b.trstgbn = '1'"
                sSql += "         GROUP BY r.regno, r.testcd, r.spccd, b.dispseq, b.bbgbn "
                sSql += "       ) r"
                sSql += " WHERE r.testcd = f.testcd"
                sSql += "   AND r.spccd  =  f.spccd"
                sSql += "   AND r.tkdt   >= f.usdt"
                sSql += "   AND r.tkdt   <  f.uedt"
                sSql += " ORDER BY dispseq "

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

                alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

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

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComCd))

                If rsFkOcs.IndexOf("-") >= 0 Then
                    sSql += "   AND fkocs   = :fkocs"
                    sSql += "   AND seq     = :seq"

                    alParm.Add(New OracleParameter("fkocs", OracleType.VarChar, rsFkOcs.Split("-"c)(0).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs.Split("-"c)(0)))
                    alParm.Add(New OracleParameter("seq", OracleType.NChar, rsFkOcs.Split("-"c)(1).Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs.Split("-"c)(1)))

                Else
                    sSql += "   AND fkocs   = :fkocs"

                    alParm.Add(New OracleParameter("fkocs", OracleType.VarChar, rsFkOcs.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFkOcs))
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

                aryList.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

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

                aryList.Add(New OracleParameter("bcno", OracleType.VarChar, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

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

                aryList.Add(New OracleParameter("bcno", OracleType.VarChar, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

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

                aryList.Add(New OracleParameter("bcno", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

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
                sSql += "       END  availyn"
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

                aryList.Add(New OracleParameter("@param0", OracleType.VarChar, rsBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBcno))

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

                aryList.Add(New OracleParameter("@param0", OracleType.VarChar, rsKeepBcno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsKeepBcno))

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
                sSql += "       a.regno, j.patnm, a.sex || '/' || a.age sexage,"
                sSql += "       fn_ack_get_dr_name(a.doctorcd) doctornm, a.deptcd, a.wardno,"
                sSql += "       fn_ack_date_str(a.opdt, 'yyyy-mm-dd') opdt, b.ir, b.filter,"
                sSql += "       b.reqqnt, b.befoutqnt, b.outqnt, b.abnqnt, b.rtnqnt, b.cancelqnt, b.doctorrmk,"
                sSql += "       b.comcd, c.comnmd"
                sSql += "  FROM lb040m a, lb042m b, lf120m c"
                sSql += " WHERE a.tnsjubsuno = b.tnsjubsuno"
                sSql += "   AND b.comcd      = c.comcd"
                sSql += "   AND a.jubsudt   >= c.usdt"
                sSql += "   AND a.jubsudt   <  c.uedt"
                sSql += "   AND b.befoutqnt >  0"       ' 출고 미완료인 경우!! - > 가출고인것만 
                sSql += "   AND b.outqnt + b.rtnqnt + b.abnqnt = 0" '-- 국립의료원인 경우 접수단위로 취소
                sSql += "   AND b.delflg     = '0'"     ' 0: 조회, 1: 삭제
                sSql += "   AND a.jubsudt   >= :dates "
                sSql += "   AND a.jubsudt   <= :datee || '235959'"

                If rsRef = "0" Then ' 수혈의뢰 접수후 출고미완료로 72시간 경과된 order
                    sSql += "   AND SYSDATE - TO_DATE(a.jubsudt, 'yyyymmddhh24miss') >= " + dbHour.ToString
                ElseIf rsRef = "1" Then    '출고 미완료로 수술예정일이 지나버린 order 
                    sSql += "   AND a.opdt < fn_ack_sysdate"
                End If

                sSql += " ORDER BY jubsudt ASC"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDateE))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
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
                sSql += "  FROM lb040m b4, lb043m b43, lb020m b2, lb030M b3"
                sSql += " WHERE b4.tnsjubsuno  = :tnsno"
                sSql += "   AND b4.tnsjubsuno  = b43.tnsjubsuno"
                sSql += "   AND b43.comcd      = :comcd"
                sSql += "   AND b43.tnsjubsuno = b3.tnsjubsuno"
                sSql += "   AND b43.comcd      = b3.comcd"
                sSql += "   AND b43.bldno      = b3.bldno"
                sSql += "   AND b3.bldno       = b2.bldno"
                sSql += "   AND b3.comcd       = b2.comcd"
                '  sSql += "   AND b2.state       = '3'"      '-- 국립의료원인 경우 접수 단위로 취소 가능
                sSql += " ORDER BY b3.befoutdt DESC"

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNo))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComCd))

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

                alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                If rstnsnum <> "" Then
                    sSql += "           AND a.tnsjubsuno <> :tnsno "
                    alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rstnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstnsnum))
                End If

                If rsDate <> "" Then
                    sSql += "       AND a.jubsudt <= :jubsudt || '235959' ) a "
                    alParm.Add(New OracleParameter("jubsudt", OracleType.VarChar, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate.Replace("-"c, "")))
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

        ' 보관검체내역 조회
        Public Shared Function fn_GetKeepSpcList(ByVal rsRegno As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_GetKeepSpcList(ByVal rsRegno As String) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT fn_ack_get_bcno_full(j.bcno) bcno,"
                sSql += "       fn_ack_date_str(j1.colldt, 'yyyy-MM-dd hh24:mi') colldt,"
                sSql += "       r.testcd, r.spccd, f3.spcnmd, f14.bbgbn,"
                sSql += "       CASE WHEN f14.bbgbn = '1' THEN r.viewrst"
                sSql += "            WHEN f14.bbgbn = '3' THEN r.viewrst"
                sSql += "            ELSE 'x'"
                sSql += "       END abo,"
                sSql += "       CASE WHEN f14.bbgbn = '2' THEN r.viewrst"
                sSql += "            ELSE 'x'"
                sSql += "       END rh,"
                sSql += "       CASE WHEN NVL(k.bcno, ' ') <> ' ' THEN 'ⓣ'"
                sSql += "            ELSE 'x'"
                sSql += "       END crossm,"
                sSql += "       CASE WHEN f14.bbgbn = '9' THEN 'ⓣ'"
                sSql += "            ELSE 'x'"
                sSql += "       END irr,"
                sSql += "       k.rackid || '/' || k.numrow || '/' || k.numcol keepplace"
                sSql += "  FROM lj010m j, lf030m f3,"
                sSql += "       (SELECT testcd, spccd, dispseq, bbgbn"
                sSql += "          FROM lf140m"
                sSql += "         WHERE bbgbn in ('1', '2', '3', '7', '9')"
                sSql += "       ) f14,"
                sSql += "       lj011m j1, lr010m r, lk010m k"
                sSql += " WHERE j.regno   =  :regno"

                alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))

                sSql += "   AND j.spcflg  >  '3'"
                sSql += "   AND j.bcno     = j1.bcno "
                sSql += "   AND j1.colldt >  fn_ack_get_date(SYSDATE - 3)"
                sSql += "   AND j1.bcno    = r.bcno"
                sSql += "   AND j1.tclscd  = r.tclscd"
                sSql += "   AND j.spccd    = f3.spccd"
                sSql += "   AND f3.usdt   <= j1.colldt"
                sSql += "   AND f3.uedt   >  j1.colldt"
                sSql += "   AND r.testcd   = f14.testcd"
                sSql += "   AND r.spccd    = f14.spccd"
                sSql += "   AND r.bcno     = k.bcno (+)"
                sSql += " ORDER BY colldt DESC, f14.bbgbn"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

#End Region

#Region " CrossMatching 등록(가출고) "
        Public Shared Function fn_PreOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsGbn As String) As DataTable
            ' 수혈 가출고 대기 리스트
            Dim sFn As String = "Public Shared Function fn_PreOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT CASE WHEN a.state = '1' THEN '완' ELSE '미' END state,"
                sSql += "       CASE WHEN a.spcstate > 0 THEN 'Y' ELSE 'N' END spcstate,                                                          "
                sSql += "       a.tnsjubsuno,"
                sSql += "       fn_ack_get_tnsjubsuno_full(a.tnsjubsuno) vtnsjubsuno,"
                sSql += "       a.comcd,"
                sSql += "       a.comnm,"
                sSql += "       a.regno,"
                sSql += "       j.patnm,"
                sSql += "       a.sexage,"
                sSql += "       fn_ack_get_bcno_full(a.bcno_order) bcno_order,"
                sSql += "       a.bcno_keep,"
                sSql += "       a.reqqnt,"
                sSql += "       a.outqnt,"
                sSql += "       a.orddt order_date,"
                sSql += "       r.abo,"
                sSql += "       r.rh,"
                sSql += "       a.spccd,"
                sSql += "       a.ir,"
                sSql += "       a.filter,"
                sSql += "       a.iogbn,"
                sSql += "       a.owngbn,"
                sSql += "       a.eryn,"
                sSql += "       CASE WHEN a.tnsgbn = '1' THEN 'P' WHEN a.tnsgbn = '2' THEN 'T'"
                sSql += "            WHEN a.tnsgbn = '3' THEN 'E' WHEN a.tnsgbn = '4' THEN 'I'"
                sSql += "       END tnsgbn,"
                sSql += "       a.state"
                sSql += "  FROM (SELECT a.tnsjubsuno,"
                sSql += "               b.comcd,"
                sSql += "               b.comnm,"
                sSql += "               a.regno,"
                sSql += "               a.patnm,"
                sSql += "               a.sex || '/' || a.age sexage,"
                sSql += "               a.bcno_order,"
                sSql += "               a.bcno_keep,"
                sSql += "               NVL(LENGTH(a.bcno_order), 0) + NVL(LENGTH(a.bcno_keep), 0) spcstate,"
                sSql += "               NVL(b.reqqnt, 0) reqqnt,"
                sSql += "               NVL(b.befoutqnt, 0) + NVL(b.outqnt, 0) + NVL(b.rtnqnt, 0) + "
                sSql += "               NVL(b.abnqnt, 0) + NVL(b.cancelqnt, 0) outqnt,"
                sSql += "               a.orddt,"
                sSql += "               b.spccd,"
                sSql += "               b.ir,"
                sSql += "               b.filter,"
                sSql += "               a.iogbn,"
                sSql += "               a.owngbn,"
                sSql += "               a.eryn,"
                sSql += "               a.tnsgbn,"
                sSql += "               b.state,"
                sSql += "               a.jubsudt"
                sSql += "          FROM lb040m a, lb042m b"
                sSql += "         WHERE a.jubsudt BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND NVL(a.delflg, '0') <> '1'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

                If rsRegno <> "" Then
                    sSql += "       AND a.regno      = :regno"
                    alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                If rsComcd <> "" And rsComcd <> "ALL" Then
                    sSql += "       AND b.comcd = :comcd"
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno     = b.tnsjubsuno "
                'sSql += "           AND NVL(b.filter, 0) = '0'"
                sSql += "       ) a, lr070m r"
                sSql += " WHERE a.regno = r.regno (+)"
                If rsGbn = "0"c Then

                ElseIf rsGbn = "1"c Then
                    sSql += "   AND state = '0'"
                ElseIf rsGbn = "2"c Then
                    sSql += "   AND state = '1'"
                End If

                sSql += " ORDER BY tnsjubsuno                                                             "

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
                sSql += "SELECT comnm, "
                sSql += "       CASE filter WHEN '1' THEN '○' ELSE '' END filter,"
                sSql += "       CASE ir  WHEN '1' THEN '○' ELSE '' END ir,"
                sSql += "       NVL(reqqnt, 0)    reqqnt,"
                sSql += "       NVL(befoutqnt, 0) befoutqnt,"
                sSql += "       NVL(outqnt, 0)    outqnt,"
                sSql += "       NVL(rtnqnt, 0)    rtnqnt,"
                sSql += "       NVL(abnqnt, 0)    abnqnt,"
                sSql += "       NVL(cancelqnt, 0) cancelqnt"
                sSql += "  FROM lb042m"
                sSql += " WHERE tnsjubsuno = :tnsno"

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsnum))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try

        End Function

        ' 혈액은행 보유혈액 조회
        Public Shared Function fn_GetStoreBldList(ByVal rsAbo As String, ByVal rsRh As String, _
                                                  ByVal rsComcd As String, ByVal rsSpccd As String, _
                                                  Optional ByVal rsEqual As String = "", Optional ByVal rsChg As String = "", _
                                                  Optional ByVal rsTnsJubsuNo As String = "") As DataTable
            Dim sFn As String = "Public Shared Function fn_GetStoreBldList(String, String, String, String, [String], [String], [String]) As DataTable"

            Try
                Dim sSql As String = ""
                Dim alParm As New ArrayList

                sSql += "SELECT a.bldno,"
                sSql += "       fn_ack_get_bldno_full(a.bldno)  vbldno,"
                sSql += "       a.comcd,"
                sSql += "       b.comnmd,"
                sSql += "       a.abo || a.rh aborh,"
                sSql += "       a.abo,"
                sSql += "       a.rh,"
                sSql += "       fn_ack_date_str(a.dondt, 'yyyy-mm-dd hh24:mi')   dondt,"
                sSql += "       fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi')    indt,"
                sSql += "       fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') availdt,"
                sSql += "       999 sortkey,"
                sSql += "       b.crosslevel crosslevel,"
                sSql += "       a.cmt"
                sSql += "  FROM lb020m a, lf120m b"
                sSql += " WHERE a.availdt >  fn_ack_sysdate"
                sSql += "   AND a.state   =  '0'"
                sSql += "   AND a.comcd   =  b.comcd"
                sSql += "   AND b.comcd   =  :comcd"
                sSql += "   AND b.spccd   =  :spccd"

                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                alParm.Add(New OracleParameter("spccd", OracleType.VarChar, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                sSql += "   AND a.indt    >= b.usdt"
                sSql += "   AND a.indt    <  b.uedt"

                If rsChg = "" Then
                    sSql += "   AND a.abo     =  :abo"
                    sSql += "   AND a.rh      =  :rh"

                    alParm.Add(New OracleParameter("abo", OracleType.VarChar, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                    alParm.Add(New OracleParameter("rh", OracleType.VarChar, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))
                End If

                If rsTnsJubsuNo <> "" Then
                    sSql += "   AND a.bldno NOT IN (SELECT bldno FROM lb043m WHERE tnsjubsuno = :tnsno)"
                    alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsJubsuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsJubsuNo))
                End If

                If rsEqual <> "" Then
                    sSql += " UNION ALL "
                    sSql += "SELECT a.bldno,"
                    sSql += "       fn_ack_get_bldno_full(a.bldno) vbldno,"
                    sSql += "       a.comcd,"
                    sSql += "       b.comnmd,"
                    sSql += "       a.abo || a.rh aborh,"
                    sSql += "       a.abo,"
                    sSql += "       a.rh,"
                    sSql += "       fn_ack_date_str(a.dondt, 'yyyy-mm-dd hh24:mi')   dondt,"
                    sSql += "       fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi')    indt,"
                    sSql += "       fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') availdt,"
                    sSql += "       9 sortkey,"
                    sSql += "       b.crosslevel crosslevel,"
                    sSql += "       b.cmt"
                    sSql += "  FROM lb020m a, lf120m b"
                    sSql += " WHERE a.availdt > fn_ack_sysdate "

                    If rsChg = "" Then
                        sSql += "   AND a.abo     = :abo"
                        sSql += "   AND a.rh      = :rh"

                        alParm.Add(New OracleParameter("abo", OracleType.VarChar, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                        alParm.Add(New OracleParameter("rh", OracleType.VarChar, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))
                    End If


                    sSql += "   AND a.state   = '0'"
                    sSql += "   AND a.comcd   = b.comcd"
                    sSql += "   AND b.gordcd  = :comcd"
                    sSql += "   AND b.spccd   = :spccd"
                    sSql += "   AND a.indt   >= b.usdt"
                    sSql += "   AND a.indt   <  b.uedt"

                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd.Trim))
                    alParm.Add(New OracleParameter("spccd", OracleType.VarChar, rsSpccd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd.Trim))

                    If rsTnsJubsuNo <> "" Then
                        sSql += "   AND a.bldno NOT IN (SELECT bldno FROM lb043m WHERE tnsjubsuno = :tnsno)"
                        alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsJubsuNo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsJubsuNo))
                    End If

                End If

                sSql += " ORDER BY availdt, bldno, comcd"

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
                sSql += "       END                                           as vstate    "
                sSql += "     , fn_ack_get_bldno_full(a.bldno)                    as vbldno    "
                sSql += "     , d.comnmd                                      as comnm                                 "
                sSql += "     , c.abo || c.rh                                 as type                                  "
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
                sSql += "     , a.comcd_out                                       as comcd     "
                sSql += "     , a.owngbn                                                       "
                sSql += "     , a.iogbn                                                        "
                sSql += "     , a.fkocs || '-' || TO_CHAR(a.seq)                  as fkocs     "
                sSql += "     , a.bldno                                                        "
                sSql += "     , d.comnmp                                                       "
                sSql += "     , CASE a.comcd_out WHEN a.comcd THEN '0'                         "
                sSql += "                        ELSE '1'                                      "
                sSql += "       END                                           as comcdchk  "
                sSql += "     , a.state                                                       "
                sSql += "     , a.comcd                                       as comcdo    "
                sSql += "     , d.crosslevel                                                   "
                sSql += "     , c.cmt "
                sSql += "  from lb043m a, lb030m b,"
                sSql += "       lb020m c, lf120m d"
                sSql += " where a.tnsjubsuno = :tnsno                                               "

                alParm.Add(New OracleParameter("@param0", OracleType.VarChar, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsnum))

                sSql += "   and a.state      in ('1', '2', '3')                                "
                sSql += "   and a.bldno      = b.bldno                                         "
                sSql += "   and a.comcd_out  = b.comcd_out                                     "
                sSql += "   and a.tnsjubsuno = b.tnsjubsuno                                    "
                sSql += "   and a.bldno      = c.bldno                                         "
                sSql += "   and a.comcd_out  = c.comcd                                         "
                sSql += "   and a.comcd_out  = d.comcd                                         "
                sSql += "   and d.spccd      = :spccd                                               "

                alParm.Add(New OracleParameter("spccd", OracleType.VarChar, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                fn_GetPreList = DbExecuteQuery(sSql, alParm)
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
                sSql += "select a.tnsjubsuno                                                   "
                sSql += "     , CASE a.state WHEN '4' THEN '출'                                "
                sSql += "                    WHEN '5' THEN '반'                                "
                sSql += "                    WHEN '6' THEN '폐'                                "
                sSql += "       END                                              as vstate     "
                sSql += "     , d.comnmd              as comnm                                 "
                sSql += "     , a.comcd_out           as comcd                                 "

                If rsFilter <> "1"c Then
                    sSql += "     , fn_ack_get_bldno_full(a.bldno)                       as vbldno     "
                    sSql += "     , c.abo || c.rh                                    as type                                  "
                    sSql += "     , b.rst1                                                         "
                    sSql += "     , b.rst2                                                         "
                    sSql += "     , b.rst3                                                         "
                    sSql += "     , b.rst4                                                         "
                    sSql += "     , b.cmrmk                                                        "
                    sSql += "     , fn_ack_date_str(b.befoutdt, 'yyyy-MM-dd hh24:mi')    as befoutdt       "
                    sSql += "     , fn_ack_date_str(b.outdt, 'yyyy-MM-dd hh24:mi')       as outdt          "
                    sSql += "     , fn_ack_get_usr_name(b.testid)                        as inspector      "
                    sSql += "     , fn_ack_date_str(c.indt, 'yyyy-MM-dd hh24:mi')        as indt           "
                    sSql += "     , fn_ack_date_str(c.dondt, 'yyyy-MM-dd hh24:mi')       as dondt          "
                    sSql += "     , fn_ack_date_str(c.availdt, 'yyyy-MM-dd hh24:mi')     as availdt        "
                    sSql += "     , fn_ack_get_usr_name(b.outid)                         as outid          "
                    sSql += "     , b.recnm                                                                "
                    sSql += "     , CASE b.keepgbn WHEN '0' THEN '출'                                      "
                    sSql += "                      WHEN '1' THEN '보'                                      "
                    sSql += "                      WHEN '2' THEN '재'                                      "
                    sSql += "                      WHEN '3' THEN '반'                                      "
                    sSql += "                      WHEN '4' THEN '폐'                                      "
                    sSql += "       END                                              as vkeepgbn           "
                    sSql += "     , b.keepgbn                                                              "
                    sSql += "     , CASE b.keepgbn WHEN '1' THEN 0                                       "
                    sSql += "                      ELSE TRUNC(SYSDATE - TO_DATE(b.outdt, 'yyyymmddhh24miss')) * 24 * 60"
                    sSql += "       END                                              as elapsdm            "

                Else
                    sSql += "     , 'FILTER' as vbldno                                "
                    sSql += "     , ''           as type                                  "
                    sSql += "     , '' as rst1                                                         "
                    sSql += "     , '' as rst2                                                         "
                    sSql += "     , '' as rst3                                                         "
                    sSql += "     , '' as rst4                                                         "
                    sSql += "     , '' as cmrmk                                                        "
                    sSql += "     , ''    as befoutdt       "
                    sSql += "     , ''       as outdt          "
                    sSql += "     , ''                    as inspector      "
                    sSql += "     , ''        as indt           "
                    sSql += "     , ''      as dondt          "
                    sSql += "     , ''    as availdt        "
                    sSql += "     , ''                      as outid          "
                    sSql += "     , ''      as recnm                                                  "
                    sSql += "     , '출'      as vkeepgbn       "
                    sSql += "     , '9'           keepgbn          "
                    sSql += "     , '' as elapsdm               "
                End If

                sSql += "     , a.state                                                        "
                sSql += "     , a.bldno                                                        "
                sSql += "     , d.comnmp                                                       "
                sSql += "     , a.fkocs || '-' || TO_CHAR(seq) as fkocs                        "
                sSql += "     , c.cmt "
                sSql += "  from lb043m a                                                       "

                If rsFilter <> "1"c Then
                    sSql += "     , lb030m b                                                       "
                End If

                sSql += "     , lb020m c                                                       "
                sSql += "     , lf120m d                                                       "
                sSql += " where a.tnsjubsuno = :tnsno                                               "

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsnum))

                'sSql += "   and a.state      in ('4', '5', '6')                                "
                sSql += "   and a.state      = '4'                               "

                If rsFilter <> "1"c Then
                    sSql += "   and a.bldno      = b.bldno                                         "
                    sSql += "   and a.comcd_out  = b.comcd_out                                     "
                    sSql += "   and a.tnsjubsuno = b.tnsjubsuno                                    "
                End If

                sSql += "   and a.bldno      = c.bldno                                         "
                sSql += "   and a.comcd_out  = c.comcd                                         "
                sSql += "   and a.comcd_out  = d.comcd                                         "
                sSql += "   and d.spccd      = :spccd                                               "

                alParm.Add(New OracleParameter("spccd", OracleType.VarChar, rsTnsnum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsSpccd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액출고 "
        Public Shared Function fn_OutOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsBldno As String, ByVal rsGbn As String) As DataTable
            ' 수혈 출고 대기 리스트
            Dim sFn As String = "Public Shared Function fn_OutOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT fn_ack_get_tnsjubsuno_full(a.tnsjubsuno)  tnsjubsuno                "
                sSql += "     , CASE WHEN a.reqqnt >  NVL(a.outqnt, 0) THEN '미'                            "
                sSql += "            WHEN a.reqqnt <= NVL(a.outqnt, 0) THEN '완'                           "
                sSql += "       END as vstate                                                              "
                sSql += "     , a.comcd                                                                    "
                sSql += "     , a.comnm as comnmd                                                          "
                sSql += "     , a.reqqnt                                                                   "
                sSql += "     , a.jubsudt                                                                  "
                sSql += "     , a.befoutqnt                                                                "
                sSql += "     , a.outqnt                                                                   "
                sSql += "     , a.regno                                                                    "
                sSql += "     , a.patnm                                                                    "
                sSql += "     , r.abo                                     "
                sSql += "     , r.rh                                      "
                sSql += "     , a.spccd                                                                    "
                sSql += "     , a.iogbn                                                                    "
                sSql += "     , a.owngbn                                                                   "
                sSql += "     , fn_ack_date_str(a.orddt, 'yyyy-mm-dd') order_date                                                               "
                sSql += "     , a.tnsgbn                                                                   "
                sSql += "     , a.filter                                                                   "
                sSql += "     , a.comordcd                                                                 "
                sSql += "     , a.state"
                sSql += "  FROM "

                If rsBldno.Length() > 0 Then
                    sSql += "       (SELECT tnsjubsuno, COUNT(tnsjubsuno) cnt"
                    sSql += "          FROM lb043m"
                    sSql += "         WHERE bldno = :bldno"

                    alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

                    sSql += "        GROUP BY tnsjubsuno"
                    sSql += "       ) b"
                    sSql += "       INNER JOIN"
                End If

                sSql += "       (SELECT a.tnsjubsuno                                                       "
                sSql += "             , b.comcd                                                            "
                sSql += "             , b.comnm                                                            "
                sSql += "             , b.spccd                                                            "
                sSql += "             , a.tnsgbn                                                           "
                sSql += "             , NVL(b.reqqnt, 0)    as reqqnt                                      "
                sSql += "             , NVL(b.befoutqnt, 0) as befoutqnt                                   "
                sSql += "             , NVL(b.outqnt, 0) + NVL(b.rtnqnt, 0) +                              "
                sSql += "               NVL(b.abnqnt, 0) /*+ NVL(b.cancelqnt, 0)*/ as outqnt                   "
                sSql += "             , a.regno                                                            "
                sSql += "             , a.patnm"
                sSql += "             , fn_ack_date_str(a.jubsudt, 'yyyy-MM-dd hh24:mi') as jubsudt        "
                sSql += "             , a.iogbn                                                            "
                sSql += "             , a.owngbn                                                           "
                sSql += "             , b.filter                                                           "
                sSql += "             , c.comordcd                                                         "
                sSql += "             , a.orddt                "
                sSql += "             , b.state"
                sSql += "          FROM lb040m a                                                           "
                sSql += "             , lb042m b                                                           "
                sSql += "             , lf120m c                                                           "
                sSql += "         WHERE a.jubsudt BETWEEN :dates"
                sSql += "                             AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

                If rsRegno <> "" Then
                    sSql += "       AND a.regno      = :regno "
                    alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                If rsComcd <> "" And rsComcd <> "ALL" Then
                    sSql += "       AND b.comcd = :comcd "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If


                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                       "
                sSql += "           AND b.comcd      = c.comcd                                            "
                sSql += "           AND b.spccd      = c.spccd "
                sSql += "           AND NVL(a.delflg, '0') = '0'"
                sSql += "     ) a      "
                If rsBldno.Length() > 0 Then
                    sSql += " ON (a.tnsjubsuno = b.tnsjubsuno) "
                End If

                sSql += "     LEFT OUTER JOIN"
                sSql += "         lr070m r ON (a.regno = r.regno)"

                If rsGbn = "0"c Then

                ElseIf rsGbn = "1"c Then
                    sSql += " WHERE a.state = '0'"
                ElseIf rsGbn = "2"c Then
                    sSql += " WHERE a.state = '1'"
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
                sSql += "SELECT a.tnsjubsuno                                           "
                sSql += "     , fn_ack_get_bldno_full(a.bldno)                     as vbldno    "
                sSql += "     , e.comnmd                                               "
                sSql += "     , d.abo || d.rh                                  as type      "
                sSql += "     , fn_ack_date_str(c.befoutdt, 'yyyy-MM-dd hh24:mi')  as befoutdt  "
                sSql += "     , fn_ack_get_usr_name(c.testid)                      as testid    "
                sSql += "     , fn_ack_date_str(d.indt, 'yyyy-MM-dd hh24:mi')      as indt      "
                sSql += "     , fn_ack_date_str(d.dondt, 'yyyy-MM-dd hh24:mi')     as dondt     "
                sSql += "     , fn_ack_date_str(d.availdt, 'yyyy-MM-dd hh24:mi')   as availdt   "
                sSql += "     , c.comcd_out                                    as comcd     "
                sSql += "     , a.owngbn                                               "
                sSql += "     , a.iogbn                                                "
                sSql += "     , a.fkocs || '-' || TO_CHAR(seq) as fkocs                "
                sSql += "     , a.bldno                                        as bldno     "
                sSql += "     , b.filter                                               "
                sSql += "     , e.comordcd                                     as comcdo    "
                sSql += "     , '9999999'                                      as sortkey   "
                sSql += "     , d.cmt "
                sSql += "  FROM lb040m b1"
                sSql += "     , lb043m a "
                sSql += "     , lb042m b                                               "
                sSql += "     , lb030m c                                               "
                sSql += "     , lb020m d                                               "
                sSql += "     , lf120m e                                               "
                sSql += " WHERE b1.tnsjubsuno = :tnsno                                       "
                sSql += "   AND b1.tnsjubsuno = b.tnsjubsuno                            "
                sSql += "   AND b1.tnsjubsuno = a.tnsjubsuno"

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNum))

                sSql += "   AND a.state      = '3'                                     "
                sSql += "   AND a.bldno      = c.bldno                                 "
                sSql += "   AND a.comcd_out  = c.comcd_out                             "
                sSql += "   AND a.tnsjubsuno = c.tnsjubsuno                            "
                sSql += "   AND a.bldno      = d.bldno                                 "
                sSql += "   AND a.comcd_out  = d.comcd                                 "
                sSql += "   AND a.comcd_out  = e.comcd                                 "
                sSql += "   AND b.spccd      = e.spccd                                 "
                sSql += "   AND b1.jubsudt  >= e.usdt"
                sSql += "   AND b1.jubsudt  <  e.uedt"
                sSql += " ORDER BY vbldno"


                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 혈액반납/폐기 "
        Public Shared Function fn_RtnOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String, ByVal rsBldno As String) As DataTable
            ' 반납 / 폐기 대상 리스트
            Dim sFn As String = "Public Shared Function fn_OutOrderList(ByVal rsFdate As String, ByVal rsTdate As String, ByVal rsRegno As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT DISTINCT"
                sSql += "       NVL(c.outdt, '-') as outdt                               "
                sSql += "     , fn_ack_get_tnsjubsuno_full(a.tnsjubsuno)        as vtnsjubsuno  "
                sSql += "     , a.tnsjubsuno                                                    "
                sSql += "     , a.regno                                                         "
                sSql += "     , a.patnm"
                sSql += "     , b.comnm                                                         "
                sSql += "     , a.sex || '/' || a.age       as sexage       "
                sSql += "     , fn_ack_get_dr_name(a.doctorcd)              as doctornm     "
                sSql += "     , fn_ack_get_dept_name(a.iogbn, a.deptcd)         as deptnm       "
                sSql += "     , a.wardno || '/' || a.roomno                       as sr               "
                sSql += "     , fn_ack_date_str(a.jubsudt, 'yyyy-MM-dd hh24:mi')    as jubsudt          "
                sSql += "     , CASE b.state WHEN '0' THEN '미완료' WHEN '1' THEN '완료' END as state            "
                sSql += "     , ''                                          as remark           "
                sSql += "     , fn_ack_date_str(a.orddt, 'yyyy-MM-dd')              as order_date       "
                sSql += "     , b.filter                                                        "
                sSql += "     , b.spccd                                                         "
                sSql += "  FROM lb040m a LEFT OUTER JOIN                                        "
                sSql += "       (SELECT a.tnsjubsuno                                            "
                sSql += "             , fn_ack_date_str(MAX(b.outdt), 'yyyy-MM-dd hh24:mi') as outdt    "
                sSql += "             , '' rtnflg"
                sSql += "          FROM lb043m a                                                "
                sSql += "             , lb030m b                                                "
                sSql += "         WHERE a.bldno      = b.bldno                                  "
                sSql += "           AND a.comcd      = b.comcd                                  "
                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                             "

                If rsBldno.Length() > 0 Then
                    sSql += "       AND a.bldno = :bldno                                             "
                    alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                End If
                sSql += "         GROUP BY a.tnsjubsuno"
                sSql += "         UNION "
                sSql += "        SELECT a.tnsjubsuno                                            "
                sSql += "             , fn_ack_date_str(MAX(b.outdt), 'yyyy-MM-dd hh24:mi') as outdt    "
                sSql += "             , b.rtnflg"
                sSql += "          FROM lb043m a                                                "
                sSql += "             , lb031m b                                                "
                sSql += "         WHERE a.bldno      = b.bldno                                  "
                sSql += "           AND a.comcd      = b.comcd                                  "
                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                             "

                If rsBldno.Length() > 0 Then
                    sSql += "       AND a.bldno = :bldno                                             "
                    alParm.Add(New OracleParameter("bldno", rsBldno))
                End If
                sSql += "        GROUP BY a.tnsjubsuno, b.rtnflg"

                sSql += "       ) c ON (a.tnsjubsuno = c.tnsjubsuno)                                      "
                sSql += "     , lb042m b                                                        "
                sSql += " WHERE a.jubsudt    BETWEEN :dates"
                sSql += "                        AND :datee || '235959'"
                sSql += "   AND a.delflg = '0'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsFdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsFdate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsTdate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTdate))

                If rsRegno <> "" Then
                    sSql += "       AND a.regno      = :regno "
                    alParm.Add(New OracleParameter("@param2", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                If rsComcd <> "" And rsComcd <> "ALL" Then
                    sSql += "       AND b.comcd = :comcd "
                    alParm.Add(New OracleParameter("@param3", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "   AND a.tnsjubsuno = b.tnsjubsuno                                     "

                If rsBldno.Length() > 0 Then
                    sSql += "   AND a.tnsjubsuno = c.tnsjubsuno                                 "
                Else
                    sSql += "   AND a.tnsjubsuno = c.tnsjubsuno                              "
                End If

                sSql += "ORDER BY NVL(c.outdt, '-') DESC, tnsjubsuno                            "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_RtnList(ByVal rsTnsNum As String, ByVal rsFilter As String) As DataTable
            ' 반납 리스트
            Dim sFn As String = "Public Shared Function fn_RtnList(ByVal rsTnsNum As String, ByVal rsFilter As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "select DISTINCT"
                sSql += "       CASE a.rtnflg WHEN '1' THEN '반납' WHEN '2' THEN '폐기' END as gubun   "
                sSql += "     , d.comnmd                                               "

                If rsFilter = "1"c Then
                    sSql += "     , d.comcd                                            "
                    sSql += "     , 'FILTER'                               as vbldno   "
                    sSql += "     , ''                                     as aborh    "
                Else
                    sSql += "     , b.comcd                                            "
                    sSql += "     , fn_ack_get_bldno_full(a.bldno)             as vbldno   "
                    sSql += "     , b.abo || b.rh                          as aborh    "
                End If


                sSql += "     , fn_ack_date_str(a.rtndt, 'yyyy-MM-dd hh24:mi:ss')  as rtndt    "
                sSql += "     , fn_ack_get_usr_name(a.rtnid)                       as rtnnm    "
                sSql += "     , a.rtnreqnm "
                sSql += "     , a.rtnrsncmt                                    as remark "

                If rsFilter = "1"c Then
                    sSql += "     , ''     as indt     "
                    sSql += "     , ''     as dondt    "
                    sSql += "     , ''     as availdt  "
                Else
                    sSql += "     , fn_ack_date_str(b.indt, 'yyyy-MM-dd hh24:mi')     as indt     "
                    sSql += "     , fn_ack_date_str(b.dondt, 'yyyy-MM-dd hh24:mi')    as dondt    "
                    sSql += "     , fn_ack_date_str(b.availdt, 'yyyy-MM-dd hh24:mi')  as availdt  "
                End If
                sSql += "     , a.tnsjubsuno, a.comcd_out, a.comcd, c.owngbn, c.iogbn"
                sSql += "     , c.fkocs || '-' || TO_CHAR(c.seq) fkocs               "
                sSql += "  from lb031m a                                               "

                If rsFilter = "1"c Then

                Else
                    sSql += "     , lb020m b                                           "
                End If


                sSql += "     , lb043m c                                               "
                sSql += "     , lf120m d                                               "
                sSql += " where a.tnsjubsuno = :tnsno                                       "

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNum))

                If rsFilter = "1"c Then

                Else
                    sSql += "   and a.bldno      = b.bldno                             "
                    sSql += "   and a.comcd_out  = b.comcd                             "
                End If


                sSql += "   and a.tnsjubsuno = c.tnsjubsuno                            "
                sSql += "   and a.comcd_out  = c.comcd_out                             "
                sSql += "   and a.bldno      = c.bldno"
                sSql += "   and a.comcd_out  = d.comcd                                 "
                sSql += "   and c.spccd      = d.spccd                                 "
                sSql += "   AND c.state    IN ('5', '6')"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_RtnOutList(ByVal rsTnsNum As String) As DataTable
            ' 출고 리스트
            Dim sFn As String = "Public Shared Function fn_RtnOutList(ByVal rsTnsNum As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.comnmd                                                                          "
                sSql += "     , a.ir                                                                              "
                sSql += "     , a.filter                                                                          "
                sSql += "     , a.reqqnt                                                                          "
                sSql += "     , a.befoutqnt                                                                       "
                sSql += "     , a.outqnt                                                                          "
                sSql += "     , a.rtnqnt                                                                          "
                sSql += "     , a.abnqnt                                                                          "
                sSql += "     , a.cancelqnt                                                                       "
                sSql += "     , a.outnm                                                                           "
                sSql += "     , TRUNC(DECODE(a.keepgbn, '1', '0', a.keepelapsdm), 1) as keepelapsdm               "
                sSql += "     , CASE a.keepgbn WHEN '1' THEN '00:00'                                              "
                sSql += "                      ELSE fn_ack_date_diff(a.outdt, fn_ack_sysdate, 0)                                                              "
                sSql += "       END   as elapsdt                                                                 "
                sSql += "     , fn_ack_get_bldno_full(a.bldno) as vbldno                                                   "
                sSql += "     , a.aborh                                                                           "
                sSql += "     , a.befoutdt                                                                        "
                sSql += "     , a.testnm                                                                          "
                sSql += "     , a.tnsjubsuno                                                                      "
                sSql += "     , a.comcd                                                                           "
                sSql += "     , a.comcdo                                                                          "
                sSql += "     , a.comordcd                                                                        "
                sSql += "     , a.owngbn                                                                          "
                sSql += "     , a.iogbn                                                                           "
                sSql += "     , a.fkocs                                            "
                sSql += "     , a.bldno                                                                           "
                sSql += "     , a.keepgbn                                                                         "
                sSql += "     , CASE a.state WHEN '2' THEN '검사중'                                               "
                sSql += "                    WHEN '3' THEN '가출고'                                               "
                sSql += "                    WHEN '4' THEN CASE a.keepgbn WHEN '0' THEN '출고' WHEN '1' THEN '보관' WHEN '2' THEN '재출고' END "
                sSql += "       END                                                                      as state "
                sSql += "  FROM (SELECT d.comnmd                                                                  "
                sSql += "             , CASE b.ir WHEN '1' THEN '○' ELSE '' END as ir                            "
                sSql += "             , CASE b.filter WHEN '1' THEN '○' ELSE '' END as filter                    "
                sSql += "             , NVL(b.reqqnt, 0) as reqqnt                                                "
                sSql += "             , NVL(b.befoutqnt, 0) as befoutqnt                                          "
                sSql += "             , NVL(b.outqnt, 0) as outqnt                                                "
                sSql += "             , NVL(b.rtnqnt, 0) as rtnqnt                                                "
                sSql += "             , NVL(b.abnqnt, 0) as abnqnt                                                "
                sSql += "             , NVL(b.cancelqnt, 0) as cancelqnt                                          "
                sSql += "             , fn_ack_get_usr_name(c.outid) as outnm                                        "
                sSql += "             , TRUNC((SYSDATE - TO_DATE(c.outdt, 'yyyymmddhh24miss')) * 24 * 60, 1)      as keepelapsdm                         "
                sSql += "             , TRUNC((SYSDATE - TO_DATE(c.outdt, 'yyyymmddhh24miss')) * 24, 1)           as keepelapsdt                         "
                sSql += "             , c.bldno                     as bldno                                                                   "
                sSql += "             , a.abo || a.rh as aborh                                                      "
                sSql += "             , fn_ack_date_str(c.befoutdt, 'yyyy-MM-dd hh24:mi') as befoutdt                     "
                sSql += "             , fn_ack_get_usr_name(c.testid)                as testnm                       "
                sSql += "             , a.tnsjubsuno                                                              "
                sSql += "             , a.comcd_out                               as comcd                        "
                sSql += "             , a.comcd                                   as comcdo                       "
                sSql += "             , d.comordcd                                as comordcd                     "
                sSql += "             , a.owngbn                                                                  "
                sSql += "             , a.iogbn                                                                   "
                sSql += "             , a.fkocs || '-' || TO_CHAR(a.seq) fkocs                                    "
                sSql += "             , c.keepgbn                                                                 "
                sSql += "             , a.state                                                                   "
                sSql += "             , c.outdt                                                                   "
                sSql += "          FROM lb043m a                                                                  "
                sSql += "             , lb042m b                                                                  "
                sSql += "             , lb030m c                                                                  "
                sSql += "             , lf120m d                                                                  "
                sSql += "         WHERE a.tnsjubsuno = :tnsno                                                          "

                alParm.Add(New OracleParameter("tnsno", OracleType.VarChar, rsTnsNum.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsNum))

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                               "
                sSql += "           AND a.bldno      = c.bldno                                                    "
                sSql += "           AND a.comcd_out  = c.comcd_out                                                "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                               "
                sSql += "           AND c.keepgbn    not in ('3', '4')                                            "
                sSql += "           AND a.comcd_out  = d.comcd                                                    "
                sSql += "           AND b.spccd      = d.spccd "
                sSql += "           AND a.state      = '4'"
                sSql += "       ) a"
                sSql += " ORDER BY bldno                                                                           "
                sSql += "                                                                                         "
                sSql += "                                                                                         "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

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
                sSql += "SELECT a.comcd"
                sSql += "     , a.comnmd                                                                    "
                sSql += "     , MAX(CASE WHEN b.type = 'A+'  THEN b.availqty END) as a1                     "
                sSql += "     , MAX(CASE WHEN b.type = 'B+'  THEN b.availqty END) as b1                     "
                sSql += "     , MAX(CASE WHEN b.type = 'O+'  THEN b.availqty END) as o1                     "
                sSql += "     , MAX(CASE WHEN b.type = 'AB+' THEN b.availqty END) as ab1                    "
                sSql += "     , MAX(CASE WHEN b.type = 'A-'  THEN b.availqty END) as a2                     "
                sSql += "     , MAX(CASE WHEN b.type = 'B-'  THEN b.availqty END) as b2                     "
                sSql += "     , MAX(CASE WHEN b.type = 'O-'  THEN b.availqty END) as o2                     "
                sSql += "     , MAX(CASE WHEN b.type = 'AB-' THEN b.availqty END) as ab2                    "
                sSql += "     , SUM(b.availqty)                                   as availqty                          "
                sSql += "  FROM (SELECT a.comcd                                                             "
                sSql += "             , b.comnm                                                             "
                sSql += "             , b.comnmd                                                            "
                sSql += "          FROM lb020m a,                                                           "
                sSql += "               lf120m b                                                            "
                sSql += "         WHERE a.comcd    = b.comcd"
                sSql += "           AND a.availdt >= :indt"
                sSql += "           AND a.indt    <= :indt || '235959'"
                sSql += "           AND a.indt    >= b.usdt"
                sSql += "           AND a.indt    <  b.uedt"
                sSql += "           AND CASE WHEN NVL(a.editdt, fn_ack_sysdate) > :indt || '235959'"

                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "                    THEN '0'                                                       "
                sSql += "                    ELSE a.state                                                   "
                sSql += "               END               = '0'                                             "
                sSql += "         GROUP BY a.comcd, b.comnm, b.comnmd                                       "
                sSql += "       ) a LEFT OUTER JOIN                                                         "
                sSql += "       (SELECT a.comcd                                                             "
                sSql += "             , a.abo || a.rh    as type                                             "
                sSql += "             , COUNT(a.bldno) as availqty                                          "
                sSql += "          FROM lb020m a,                                                           "
                sSql += "               (SELECT DISTINCT comcd                                              "
                sSql += "                     , comnm                                                       "
                sSql += "                  FROM lf120m                                                      "
                sSql += "               ) b                                                  "
                sSql += "         WHERE a.comcd = b.comcd                                                   "
                sSql += "           AND a.availdt >= :indt"
                sSql += "           AND a.indt    <= :indt || '235959'"
                sSql += "           AND CASE WHEN NVL(a.editdt, fn_ack_sysdate) > :indt || '235959'"

                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "                    THEN '0'                                                       "
                sSql += "                    ELSE a.state                                                   "
                sSql += "               END               = '0'                                             "
                sSql += "        GROUP BY a.comcd, b.comnm, a.abo || a.rh                                    "
                sSql += "      ) b ON (a.comcd = b.comcd)                                                   "
                sSql += "GROUP BY a.comcd, a.comnmd                                                         "
                sSql += "ORDER BY a.comcd                                                                   "

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
                sSql += "SELECT b.comnmd"
                sSql += "     , a.abo || a.rh                                as aborh"
                sSql += "     , fn_ack_get_bldno_full(a.bldno)                   as bldno"
                sSql += "     , fn_ack_date_str(a.dondt, 'yyyy-MM-dd hh24:mi')   as dondt"
                sSql += "     , fn_ack_date_str(a.availdt, 'yyyy-MM-dd hh24:mi') as availdt"
                sSql += "     , fn_ack_date_str(a.indt, 'yyyy-mm-dd hh24:mi')    as indt"
                sSql += "     , fn_ack_get_usr_name(a.inid)                      as inid"
                sSql += "  FROM lb020m a,"
                sSql += "       lf120m b"
                sSql += " WHERE a.comcd    = b.comcd"
                sSql += "   AND a.indt    >= b.usdt"
                sSql += "   AND a.indt    <  b.uedt"
                sSql += "   AND a.availdt >= :indt"
                sSql += "   AND a.indt    <= :indt || '235959'"
                sSql += "   AND CASE WHEN NVL(a.editdt, fn_ack_sysdate) > :indt || '235959'"

                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("indt", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

                sSql += "            THEN '0'"
                sSql += "            ELSE a.state"
                sSql += "       END               = '0'"
                sSql += "   AND a.comcd = :comcd"

                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                If rsAbo.Length() > 0 Then
                    sSql += "   AND a.abo   = :abo"
                    sSql += "   AND a.rh    = :rh"

                    alParm.Add(New OracleParameter("abo", OracleType.VarChar, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                    alParm.Add(New OracleParameter("rh", OracleType.VarChar, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))
                End If


                sSql += " ORDER BY a.dondt, a.availdt, a.indt, b.comcd, a.bldno"

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
                sSql += "            WHEN a.state = '4' THEN '출고'   WHEN a.state = '5' THEN '반납'"
                sSql += "            WHEN a.state = '6' THEN '폐기'                        "
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

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

                If rsComcd.Length() > 1 Then
                    sSql += "   AND a.comcd = :comcd                                            "

                    alParm.Add(New OracleParameter("@param0", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
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

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

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

                sSql = ""
                '-- 입고 
                sSql += "SELECT CASE WHEN NVL(editid, ' ') = ' ' THEN '초입고' ELSE '입고' END state,"
                sSql += "       fn_ack_date_str(a.indt, 'yyyy-MM-dd hh24:mi:ss') workdt,"
                sSql += "       fn_ack_get_usr_name(a.inid)  worknm,"
                sSql += "       '' recid, '' recnm, '' tnsgbn, '' regno, '' patnm,"
                sSql += "       '' vtnsjubsuno, a.abo, a.rh, '' rtnrsncmt,"
                sSql += "       '0' sort_key"
                sSql += "  FROM lb020m a"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = :comcd"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 검사중
                sSql += " UNION "
                sSql += "SELECT '검사' state,"
                sSql += "        fn_ack_date_str(b.testdt, 'yyyy-MM-dd hh24:mi:ss') workdt,"
                sSql += "        fn_ack_get_usr_name(b.testid) woknm,"
                sSql += "        '' recid, '' recnm,"
                sSql += "        CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'"
                sSql += "             WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'"
                sSql += "        END tnsgbn,"
                sSql += "        b.regno, b.patnm, "
                sSql += "        fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno,      "
                sSql += "        a.abo, a.rh, '' rtnrsncmt, "
                sSql += "        b.sort_key"
                sSql += "   FROM lb020m a,"
                sSql += "        (SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.testdt || '1' sort_key"
                sSql += "           FROM lb030m x, lb040m y"
                sSql += "          WHERE x.bldno      = :bldno"
                sSql += "            AND x.comcd_out  = :comcd"
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "            AND NVL(x.testid, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          UNION"
                sSql += "         SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno,  x.testdt || '1' sort_key"
                sSql += "           FROM lb031m x, lb040m y"
                sSql += "          WHERE x.bldno      = :bldno"
                sSql += "            AND x.comcd_out  = :comccd"
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "            AND NVL(x.testid, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          UNION"
                sSql += "         SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.testdt || '1' sort_key"
                sSql += "           FROM lb030h x, lb040m y"
                sSql += "          WHERE x.bldno      = :bldno"
                sSql += "            AND x.comcd_out  = :comcd"
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "            AND NVL(x.testid, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          UNION"
                sSql += "         SELECT x.bldno, x.comcd_out, x.testdt, x.testid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.testdt || '1' sort_key                                                                        "
                sSql += "           FROM lb031h x, lb040m y"
                sSql += "          WHERE x.bldno      = :bldno"
                sSql += "            AND x.comcd_out  = :comcd"
                sSql += "            AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "            AND NVL(x.testid, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = :comcd"
                sSql += "   AND a.bldno = b.bldno"
                sSql += "   AND a.comcd = b.comcd_out"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 가출고
                sSql += " UNION "
                sSql += "SELECT '가출고' state,"
                sSql += "       fn_ack_date_str(b.befoutdt, 'yyyy-MM-dd hh24:mi:ss') workdt,"
                sSql += "       fn_ack_get_usr_name(b.befoutid) worknm,"
                sSql += "       '' recid, '' recnm,"
                sSql += "       CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'"
                sSql += "            WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'"
                sSql += "       END tnsgbn,"
                sSql += "       b.regno, b.patnm,"
                sSql += "       fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno,"
                sSql += "       a.abo, a.rh, '' rtnrsncmt,"
                sSql += "       b.sort_key"
                sSql += "  FROM lb020m a,"
                sSql += "       (SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.tnsjubsuno, x.befoutdt || '2' sort_key"
                sSql += "          FROM lb030m x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION"
                sSql += "        SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.befoutdt || '2' sort_key"
                sSql += "          FROM lb031m x, lb040m y"
                sSql += "         WHERE x.bldno      = :bdno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION"
                sSql += "        SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.befoutdt || '2' sort_key"
                sSql += "          FROM lb030h x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION"
                sSql += "        SELECT x.bldno, x.comcd_out, x.befoutdt, x.befoutid, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.befoutdt || '2' sort_key"
                sSql += "          FROM lb031h x, lb040m y"
                sSql += "         WHERE x.bldno      = :bdno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.befoutdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = :comcd"
                sSql += "   AND a.bldno = b.bldno"
                sSql += "   AND a.comcd = b.comcd_out"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += " UNION "
                sSql += "SELECT '출고' state,"
                sSql += "       fn_ack_date_str(b.outdt, 'yyyy-MM-dd hh24:mi:ss') workdt,"
                sSql += "       fn_ack_get_usr_name(b.outid) worknm,"
                sSql += "       '' recid, '' recnm,"
                sSql += "       CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'"
                sSql += "            WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'"
                sSql += "       END tnsgbn,"
                sSql += "       b.regno, b.patnm,"
                sSql += "       fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno,"
                sSql += "       a.abo, a.rh, '' rtnrsncmt,"
                sSql += "       b.sort_key"
                sSql += "  FROM lb020m a,"
                sSql += "       (SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key"
                sSql += "          FROM lb030m x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno "
                sSql += "           AND NVL(x.outdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION"
                sSql += "        SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key"
                sSql += "          FROM lb031m x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.outdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION"
                sSql += "        SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key"
                sSql += "          FROM lb030h x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.outdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION"
                sSql += "        SELECT x.bldno, x.comcd_out, x.outdt, x.outid, x.recid, x.recnm, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.outdt || '3' sort_key"
                sSql += "          FROM lb031h x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.outdt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = :comcd"
                sSql += "   AND a.bldno = b.bldno"
                sSql += "   AND a.comcd = b.comcd_out"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 반납/폐기                                                             
                sSql += " UNION "
                sSql += "SELECT CASE WHEN b.rtnflg = '1' THEN '반납' "
                sSql += "            WHEN b.rtnflg = '2' THEN '폐기'"
                sSql += "            ELSE '접수취소'"
                sSql += "       END state,"
                sSql += "       fn_ack_date_str(b.rtndt, 'yyyy-MM-dd hh24:mi:ss') workdt,"
                sSql += "       fn_ack_get_usr_name(b.rtnid) worknm,"
                sSql += "       '' recid, '' recnm,"
                sSql += "       CASE WHEN b.tnsgbn = '1' THEN '준비' WHEN b.tnsgbn = '2' THEN '수혈'"
                sSql += "            WHEN b.tnsgbn = '3' THEN '응급' WHEN b.tnsgbn = '4' THEN 'Irra'"
                sSql += "       END as tnsgbn,      "
                sSql += "       b.regno, b.patnm,"
                sSql += "       fn_ack_get_tnsjubsuno_full(b.tnsjubsuno) vtnsjubsuno,"
                sSql += "       a.abo, a.rh, b.rtnrsncmt,"
                sSql += "       b.sort_key"
                sSql += "  FROM lb020m a,"
                sSql += "       (SELECT x.bldno, x.comcd_out, x.rtndt, x.rtnid, x.rtnflg, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.rtndt || '4' sort_key, x.rtnrsncmt, x.keepgbn"
                sSql += "          FROM lb031m x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.rtndt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "         UNION"
                sSql += "        SELECT x.bldno, x.comcd_out, x.rtndt, x.rtnid, x.rtnflg, y.tnsgbn, y.regno, y.patnm, y.tnsjubsuno, x.rtndt || '4' sort_key, x.rtnrsncmt, x.keepgbn"
                sSql += "          FROM lb031h x, lb040m y"
                sSql += "         WHERE x.bldno      = :bldno"
                sSql += "           AND x.comcd_out  = :comcd"
                sSql += "           AND x.tnsjubsuno = y.tnsjubsuno"
                sSql += "           AND NVL(x.rtndt, ' ') <> ' '"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       ) b"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = :comcd"
                sSql += "   AND a.bldno = b.bldno"
                sSql += "   AND a.comcd = b.comcd_out"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                '-- 자체폐기/교환
                sSql += " UNION "
                sSql += "SELECT CASE WHEN b.keepgbn = '5' THEN '자체폐기' ELSE '교환' END state,"
                sSql += "       fn_ack_date_str(b.rtndt, 'yyyy-MM-dd hh24:mi:ss') workdt,"
                sSql += "       fn_ack_get_usr_name(b.rtndt) worknm,"
                sSql += "       '' recid, '' recnm, '' tnsgbn, '' regno, '' patnm, '' vtnsjubsuno,"
                sSql += "       a.abo, a.rh, '' rtnrsncmt,"
                sSql += "       '9' sort_key"
                sSql += "  FROM lb020m a, lb031m b"
                sSql += " WHERE a.bldno = :bldno"
                sSql += "   AND a.comcd = :comcd"
                sSql += "  AND a.bldno = b.bldno"
                sSql += "  AND a.comcd = b.comcd_out"
                sSql += "  AND NVL(b.tnsjubsuno, ' ') = ' '"
                sSql += " ORDER BY sort_key DESC, workdt DESC"

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

#Region " 수혈의뢰서 조회 "
        Public Shared Function fn_TnsSearchList(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String, ByVal rsTnsGbn As String, _
                                                Optional ByVal rsRegno As String = "", Optional ByVal rsState As String = "", _
                                                Optional ByVal rsDept As String = "", Optional ByVal rsWard As String = "", Optional ByVal rsIoGbn As String = "", _
                                                Optional ByVal rsRstDay As String = "", Optional ByVal rsRst_Hb As String = "", Optional ByVal rsRst_Plt1 As String = "", Optional ByVal rsRst_Plt2 As String = "") As DataTable
            ' 재고 세부 리스트
            Dim sFn As String = "Public Shared Function fn_TnsSearchList(String, String, String, String, [String]..) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.regno, a.patnm, a.sex || '/' || a.age sexage,"
                sSql += "       fn_ack_date_str(a.orddt, 'yyyy-MM-dd') orddt,"
                sSql += "       fn_ack_get_dr_name(a.doctorcd) docnm,"
                sSql += "       fn_ack_get_dept_name(a.iogbn, a.deptcd) deptnm,"
                sSql += "       a.wardno || '/' || a.roomno wdsr,"
                sSql += "       CASE WHEN a.tnsgbn = '1' THEN '준비'      WHEN a.tnsgbn = '2' THEN '수혈'"
                sSql += "            WHEN a.tnsgbn = '3' THEN '교차미필'  WHEN a.tnsgbn = '4' THEN 'Irra'"
                sSql += "       END comgbn,"
                sSql += "       fn_ack_get_tnsjubsuno_full(a.tnsjubsuno) vtnsjubsuno,"
                sSql += "       c.comcd_out, f.comnmd,"
                sSql += "       (SELECT abo || rh FROM lr070m WHERE regno = a.regno) aborh,"
                sSql += "       NVL(b.reqqnt, 0)    reqqnt,"
                sSql += "       NVL(b.befoutqnt, 0) befoutqnt,"
                sSql += "       NVL(b.outqnt, 0)    outqnt,"
                sSql += "       NVL(b.rtnqnt, 0)    rtnqnt,"
                sSql += "       NVL(b.abnqnt, 0)    abnqnt,"
                sSql += "       NVL(b.cancelqnt, 0) cancelqnt,"
                sSql += "       g.abo || g.rh aborhBld,"
                sSql += "       fn_ack_get_bldno_full(c.bldno) vbldno,"
                sSql += "       CASE WHEN c.state = '0' THEN '취소'   WHEN c.state = '1' THEN '접수' WHEN c.state = '2' THEN '검사중'"
                sSql += "            WHEN c.state = '3' THEN '가출고' WHEN c.state = '4' THEN '출고' WHEN c.state = '5' THEN '반납'"
                sSql += "            WHEN c.state = '6' THEN '폐기'"
                sSql += "       END state,"
                sSql += "       fn_ack_date_str(a.jubsudt, 'yyyy-MM-dd hh24:mi') jubsudt,"
                sSql += "       NVL(d.rst1, e.rst1) rst1,"
                sSql += "       NVL(d.rst2, e.rst2) rst2,"
                sSql += "       NVL(d.rst3, e.rst3) rst3,"
                sSql += "       NVL(d.rst4, e.rst4) rst4,"
                sSql += "       fn_ack_date_str(NVL(d.testdt,   e.testdt),   'yyyy-MM-dd hh24:mi') testdt,   fn_ack_get_usr_name(NVL(d.testid,   e.testid))   testid,"
                sSql += "       fn_ack_date_str(NVL(d.befoutdt, e.befoutdt), 'yyyy-MM-dd hh24:mi') befoutdt, fn_ack_get_usr_name(NVL(d.befoutid, e.befoutid)) befoutid,"
                sSql += "       fn_ack_date_str(NVL(d.outdt,    e.outdt),    'yyyy-MM-dd hh24:mi') outdt,    fn_ack_get_usr_name(NVL(d.outid,    e.outid))    outid,"
                sSql += "       NVL(d.recnm, e.recnm) recnm,"
                sSql += "       fn_ack_date_str(e.rtndt, 'yyyy-MM-dd hh24:mi') rtndt, fn_ack_get_usr_name(e.rtnid) rtnid"

                If rsRstDay <> "" And rsRst_Plt1.Length + rsRst_Plt2.Length > 0 Then
                    sSql += ", r.orgrst"
                End If

                sSql += "  FROM lb040m a"
                If rsRstDay <> "" And rsRst_Plt1.Length + rsRst_Plt2.Length > 0 Then
                    sSql += "       INNER JOIN"
                    sSql += "             lr010m r ON (a.regno = r.regno AND r.rstflg = '3' AND r.tkdt >= fn_ack_get_date(TO_DATE(a.jubsudt, 'yyyymmddhh24miss') - 1) AND r.tkdt <= a.jubsudt)"
                    sSql += "       INNER JOIN"
                    sSql += "             lf140m f14 ON (r.testcd = f14.testcd AND r.spccd = f14.spccd AND f14.bbgbn = 'B')"
                End If
                sSql += "       INNER JOIN"
                sSql += "             lb042m b ON (a.tnsjubsuno = b.tnsjubsuno)"
                sSql += "       INNER JOIN"
                sSql += "             lb043m c ON (b.tnsjubsuno = c.tnsjubsuno AND b.comcd = c.comcd)"
                sSql += "       INNER JOIN"
                sSql += "             lf120m f ON (c.comcd = f.comcd AND c.spccd = f.spccd AND f.usdt <= a.jubsudt AND f.uedt > a.jubsudt)"
                sSql += "       LEFT OUTER JOIN"
                sSql += "             lb030m d ON (d.tnsjubsuno = c.tnsjubsuno AND d.comcd = c.comcd AND d.bldno = c.bldno)"
                sSql += "       LEFT OUTER JOIN"
                sSql += "             lb031m e ON (e.tnsjubsuno = c.tnsjubsuno AND e.comcd = c.comcd AND e.bldno = c.bldno)                                                     "
                sSql += "       LEFT OUTER JOIN"
                sSql += "             lb020m g ON (g.comcd = c.comcd_out AND g.bldno = c.bldno)"
                sSql += " WHERE a.jubsudt BETWEEN :dates ||'000000'"
                sSql += "                     AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsRegno.Length() > 0 Then
                    sSql += "   AND a.regno = :regno"
                    alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                End If

                If rsComcd <> "ALL" Then
                    sSql += "   AND c.comcd = :comcd"
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                If rsIoGbn <> "" Then
                    sSql += "   AND a.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleType.VarChar, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))


                    If rsWard <> "ALL" Then
                        sSql += "   AND a.wardno = :wardcd"
                        alParm.Add(New OracleParameter("wardcd", OracleType.VarChar, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If

                    If rsDept <> "ALL" Then
                        sSql += "   AND a.deptcd = :deptcd"
                        alParm.Add(New OracleParameter("deptcd", OracleType.VarChar, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                    End If
                End If

                If rsTnsGbn <> "ALL" Then
                    sSql += "   AND a.tnsgbn = :tnsgbn"
                    alParm.Add(New OracleParameter("tnsgbn", OracleType.VarChar, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsTnsGbn))
                End If

                If rsState.Length() > 0 Then
                    sSql += "   AND c.state in (" + rsState + ")"
                End If

                If rsRstDay <> "" And rsRst_Hb.Length > 0 Then
                    Dim sWhere1 As String = ""

                    If rsRst_Hb.Length > 0 Then
                        sWhere1 += "a.regno IN (SELECT r.regno FROM lr010m r, lf140m f"
                        sWhere1 += "             WHERE r.tkdt  >= fn_ack_get_date(TO_DATE(a.jubsudt, 'yyyymmddhh24miss') - 1)"
                        sWhere1 += "               AND r.tkdt  <= a.jubsudt"
                        sWhere1 += "               AND r.rstflg  = '3'"
                        sWhere1 += "               AND r.viewrst =  '" + rsRst_Hb + "'"
                        sWhere1 += "               AND r.testcd  = f.testcd"
                        sWhere1 += "               AND r.spccd   = f.spccd"
                        sWhere1 += "               AND f.bbgbn   = 'A'"
                        sWhere1 += "           )"
                    End If

                    sSql += "   AND " + sWhere1
                End If


                sSql += " ORDER BY a.tnsjubsuno DESC, c.comcd_out, a.regno, b.state"

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_outComcdList(ByVal rsfDate As String, ByVal rstDate As String, ByVal rsComcd As String, _
                                               ByVal rsDept As String, ByVal rsWard As String, ByVal rsIoGbn As String, _
                                               Optional ByVal rsRstDay As String = "", _
                                               Optional ByVal rsRst_Hb As String = "", Optional ByVal rsRst_Plt1 As String = "", Optional ByVal rsRst_Plt2 As String = "", _
                                               Optional ByVal rsABORh As String = "") As DataTable
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
                sSql += "             lb043m c ON (b.tnsjubsuno = c.tnsjubsuno AND b.comcd = c.comcd)"
                sSql += "       INNER JOIN"
                sSql += "             lf120m f ON (c.comcd = f.comcd AND c.spccd = f.spccd AND f.usdt <= a.jubsudt AND f.uedt > a.jubsudt)"
                sSql += "       LEFT  OUTER JOIN"
                sSql += "             lb030m d ON (d.tnsjubsuno = c.tnsjubsuno AND d.comcd = c.comcd AND d.bldno = c.bldno)"
                sSql += "       LEFT  OUTER JOIN"
                sSql += "             lb031m e ON (e.tnsjubsuno = c.tnsjubsuno AND e.comcd = c.comcd AND e.bldno = c.bldno)                                                     "
                sSql += "       LEFT  OUTER JOIN"
                sSql += "             lb020m g ON (g.comcd = c.comcd_out AND g.bldno = c.bldno)"
                sSql += " WHERE a.jubsudt BETWEEN :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsComcd <> "ALL" Then
                    sSql += "   AND c.comcd_out = :comcd"
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                If rsIoGbn <> "" Then
                    sSql += "   AND a.iogbn = :iogbn"
                    alParm.Add(New OracleParameter("iogbn", OracleType.VarChar, rsIoGbn.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsIoGbn))

                    If rsDept <> "ALL" Then
                        sSql += "   AND a.deptcd = :deptcd"
                        alParm.Add(New OracleParameter("deptcd", OracleType.VarChar, rsDept.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDept))
                    End If

                    If rsWard <> "ALL" Then
                        sSql += "   AND a.wardno = :wardcd"
                        alParm.Add(New OracleParameter("wardcd", OracleType.VarChar, rsWard.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsWard))
                    End If
                End If

                If rsABORh <> "" Then
                    sSql += "   AND c.abo || c.rh = :aborh"
                    alParm.Add(New OracleParameter("aborh", OracleType.VarChar, rsABORh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsABORh))
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

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldNm.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldNm))

                If rsBldCd <> "" Then
                    sSql += "   AND b.bldcd = :bldcd"
                    alParm.Add(New OracleParameter("bldcd", OracleType.VarChar, rsBldCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldCd))
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
                sSql += "select distinct COMCD, AVAILMI, COMNMD, DONQNT"
                sSql += "  from LF120M"
                sSql += " where BLDCD = :bldcd"
                sSql += "   and USDT <= fn_ack_sysdate"
                sSql += "   and UEDT >  fn_ack_sysdate"

                alParm.Add(New OracleParameter("bldcd", OracleType.VarChar, rsBldCd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldCd))

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
                sSql += "          FROM lb020m a, lf120m f"
                sSql += "         WHERE a.statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND a.state   IN ('4', '6')"
                sSql += "           AND a.comcd   = f.comcd"
                sSql += "         GROUP BY a.comcd, a.abo || a.rh"
                sSql += "       ) f"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "       LEFT OUTER JOIN"
                sSql += "            ("
                sSql += "             SELECT comcd, abo || rh bldtype, count(*) bldqty FROM lb020m"
                sSql += "              WHERE indt BETWEEN :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY comcd, abo || rh"
                sSql += "            ) a  ON f.comcd = a.comcd AND f.bldtype = a.bldtype                                                                        "
                sSql += "       LEFT OUTER JOIN                                                               "
                sSql += "            ("
                sSql += "             SELECT comcd, abo || rh bldtype, count(*) bldqty FROM lb020m"
                sSql += "              WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "                AND state IN ('4', '6')"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY comcd, abo || rh"
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
                sSql += "          FROM lb020m a, lf120m f"
                sSql += "         WHERE a.statedt BETWEEN  :dates AND :datee || '235959'"
                sSql += "           AND a.state   IN ('4', '6')"
                sSql += "           AND a.comcd   = f.comcd"
                sSql += "         GROUP BY a.comcd, a.abo || a.rh"
                sSql += "       ) f"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "       LEFT OUTER JOIN"
                sSql += "            (SELECT comcd, abo || rh bldtype, count(*) bldqty FROM lb020m"
                sSql += "              WHERE indt BETWEEN  :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY comcd, abo || rh"
                sSql += "            ) a  ON f.comcd = a.comcd AND f.bldtype = a.bldtype"
                sSql += "       LEFT OUTER JOIN  "
                sSql += "            ("
                sSql += "             SELECT comcd, abo || rh bldtype, count(*) bldqty FROM lb020m"
                sSql += "              WHERE statedt BETWEEN  :dates AND :datee || '235959'"
                sSql += "                AND state IN ('4', '6')"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                sSql += "              GROUP BY comcd, abo || rh"
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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       (SELECT COUNT(bldno) as poutqty    /* 기간전 출고량 */             "
                sSql += "          FROM lb020m                                                     "
                sSql += "         WHERE statedt < :datee || '000000                                     "
                sSql += "           AND comcd   =  :comcd                                                      "

                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "           AND state in ('6', '4')                                        "
                sSql += "       ) b,                                                               "
                sSql += "       (SELECT COUNT(bldno) as inqty    /* 기간중 입고량 */               "
                sSql += "          from lb020m                                                     "
                sSql += "         WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd = :comcd                                                  "
                sSql += "       ) c,                                                               "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       (SELECT COUNT(bldno) as outqty   /* 기간중 출고량 */               "
                sSql += "          FROM lb020m                                                     "
                sSql += "         WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd   = :comcd                                                  "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

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

                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "       - NVL((SELECT COUNT(bldno)                                       "
                sSql += "                   FROM lb020m                                             "
                sSql += "                  WHERE fn_ack_date_str(statedt, 'YYYY-MM') < a.inmonth    "
                sSql += "                    AND comcd = :comcd                                         "

                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "                 AND state in ('6', '4')), 0) as remainqty                 "
                sSql += "  FROM (SELECT fn_ack_date_str(indt, 'YYYY-MM') as inmonth                 "
                sSql += "          FROM lb020m                                                      "
                sSql += "         WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd = :comcd"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM')                          "
                sSql += "        UNION                                                              "
                sSql += "       SELECT fn_ack_date_str(statedt, 'YYYY-MM') as inmonth               "
                sSql += "         FROM lb020m                                                       "
                sSql += "        WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd   = :comcd                                                    "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          AND state in ('6', '4')                                          "
                sSql += "       GROUP BY fn_ack_date_str(statedt, 'YYYY-MM')                        "
                sSql += "       ) a LEFT OUTER JOIN                                                                "
                sSql += "       (SELECT fn_ack_date_str(indt, 'YYYY-MM') as inmonth                 "
                sSql += "             , COUNT(bldno)             as inqty                           "
                sSql += "          FROM lb020m                                                      "
                sSql += "         WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd = :comcd"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM')                          "
                sSql += "       ) b ON a.inmonth = b.inmonth                                        "
                sSql += "       LEFT OUTER JOIN                                                     "
                sSql += "       (SELECT fn_ack_date_str(statedt, 'YYYY-MM') as inmonth              "
                sSql += "             , COUNT(bldno)             as outqty                          "
                sSql += "          FROM lb020m                                                      "
                sSql += "         WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "           AND comcd   = :comcd                                                   "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

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

                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM-DD')                     "
                sSql += "        UNION                                                            "
                sSql += "       SELECT fn_ack_date_str(statedt, 'YYYY-MM-DD') as inday            "
                sSql += "         FROM lb020m                                                     "
                sSql += "        WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd   = :comcd"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "          AND state in ('6', '4')                                        "
                sSql += "        GROUP BY fn_ack_date_str(statedt, 'YYYY-MM-DD')                  "
                sSql += "      ) a LEFT OUTER JOIN                                                "
                sSql += "      (SELECT fn_ack_date_str(indt, 'YYYY-MM-DD') as inday               "
                sSql += "            , COUNT(bldno)             as inqty                          "
                sSql += "         FROM lb020m                                                     "
                sSql += "        WHERE indt  BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd = :comcd                                                  "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

                sSql += "        GROUP BY fn_ack_date_str(indt, 'YYYY-MM-DD')"
                sSql += "      ) b ON a.inday = b.inday                                           "
                sSql += "      LEFT OUTER JOIN                                                    "
                sSql += "      (SELECT fn_ack_date_str(statedt, 'YYYY-MM-DD') as inday            "
                sSql += "            , COUNT(bldno)             as outqty                         "
                sSql += "         FROM lb020m                                                     "
                sSql += "        WHERE statedt BETWEEN :dates AND :datee || '235959'"
                sSql += "          AND comcd = :comcd                                                  "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))
                alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))

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
                    sSql += "SELECT TO_CHAR(LAST_DAY(TO_DATE(:date,'yyyyMMdd')), 'yyyy-MM-dd') as lastday "
                Else
                    sSql += "SELECT TO_CHAR(LAST_DAY(TO_DATE(:date,'yyyyMMdd')), 'yyyyMMdd') as lastday"
                End If

                sSql += "  FROM DUAL"

                alParm.Add(New OracleParameter("date", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))
                alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

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

                    alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                    alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                    sSql += "   AND a.comcd    = b.comcd                                                     "
                    sSql += "   AND a.statedt >= b.usdt                                                      "
                    sSql += "   AND a.statedt <  b.uedt                                                      "
                    sSql += "   AND a.comcd    = :comcd                                                           "
                    sSql += "   AND a.abo      = :abo                                                           "
                    sSql += "   AND a.rh       = :rh                                                           "

                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    alParm.Add(New OracleParameter("abo", OracleType.VarChar, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                    alParm.Add(New OracleParameter("rh", OracleType.VarChar, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))

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

                    alParm.Add(New OracleParameter("dates", OracleType.VarChar, sDateS.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateS))
                    alParm.Add(New OracleParameter("datee", OracleType.VarChar, sDateE.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sDateE))

                    sSql += "   AND a.state   IN ('3', '4', '6')                                             "
                    sSql += "   AND a.statedt >= d.usdt                                                      "
                    sSql += "   AND a.statedt <  d.uedt                                                      "
                    sSql += "   AND a.comcd    = :comcd                                                           "
                    sSql += "   AND a.abo      = :abo                                                           "
                    sSql += "   AND a.rh       = :rh                                                           "

                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    alParm.Add(New OracleParameter("abo", OracleType.VarChar, rsAbo.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsAbo))
                    alParm.Add(New OracleParameter("rh", OracleType.VarChar, rsRh.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRh))

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
                sSql += "     , b.wardno                                                     "
                sSql += "     , fn_ack_get_dept_name(b.iogbn, b.deptcd)          as deptnm   "
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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsGbn <> "" Then
                    sSql += "   AND a.keepgbn IN ( " + rsGbn + " )                          "
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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                If rsGbn = "R"c Then
                    sSql += "           AND keepgbn = '3'                                                        "
                Else
                    sSql += "           AND keepgbn in ('4', '5')                                                "
                End If

                sSql += "           AND regdt in (SELECT MAX(regdt)                                              "
                sSql += "                           FROM lb031m                                                  "
                sSql += "                          WHERE rtndt BETWEEN :dates AND :datee || '235959'                                         "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

                If rsGbn = "R"c Then
                    sSql += "           AND keepgbn = '3'                                                                         "
                Else
                    sSql += "           AND keepgbn in ('4', '5')                                                                 "
                End If

                sSql += "           AND regdt IN (SELECT MAX(regdt)                                                               "
                sSql += "                             FROM lb031m                                                                 "
                sSql += "                            WHERE rtndt BETWEEN :dates AND :datee || '235959'                                                        "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

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
                sSql += "   AND b.uedt >  :datee || '235959                                                                      "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfMonth))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sTMonth.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sTMonth))

                sSql += " GROUP BY a.comcd_out, b.comnmd                                                                           "
                sSql += " ORDER BY a.comcd_out                                                                                     "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "           AND state in ('4', '6')                                                                             "
                sSql += "           AND comcd = a.comcd_out) as suma                                                                       "
                sSql += "     , SUM(a.qty) as sumt                                                                                      "
                sSql += "     , SUM(CASE WHEN a.years = :year - 1 THEN a.qty ELSE 0 END) as y1                                         "
                sSql += "     , SUM(CASE WHEN a.years = :year THEN a.qty ELSE 0 END) as y2                                                                 "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT comcd_out                                                                                       "
                sSql += "             , fn_ack_date_str(rtndt, 'YYYY') as years                                                         "
                sSql += "             , COUNT(comcd) as qty                                                                             "
                sSql += "          FROM lb031m                                                                                          "
                sSql += "         WHERE rtndt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                                    "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsGbn = "R"c Then
                    sSql += "           AND keepgbn = '3'                                                                               "
                Else
                    sSql += "           AND keepgbn in ('4', '5')                                                                       "
                End If

                sSql += "           AND regdt IN (SELECT MAX(regdt)                                                                     "
                sSql += "                             FROM lb031m                                                                       "
                sSql += "                            WHERE rtndt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                        "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

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

                    alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                    alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                    If rsComcd <> "" And rsComcd <> "ALL" Then
                        sSql += "   AND a.comcd_out = :comcd                                                  "
                        alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If

                    sSql += "   AND a.tnsjubsuno = b.tnsjubsuno                                      "
                    sSql += "   AND a.comcd      = b.comcd                                           "
                    sSql += "   AND a.bldno      = b.bldno                                           "
                    sSql += "   AND b.tnsjubsuno = c.tnsjubsuno                                      "
                    sSql += "   AND c.tnsgbn  = '3'                                                  "

                    If rsRegno <> "" Then
                        sSql += "   and c.regno  = :regno                                                     "
                        alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
                    End If

                    sSql += "   AND a.comcd_out  = e.comcd                                           "
                    sSql += "   AND b.spccd      = e.spccd                                           "
                    sSql += "   AND a.bldno      = f.bldno                                           "
                    sSql += "   AND a.comcd_out  = f.comcd                                           "
                    sSql += "   AND (a.bldno || a.comcd_out) NOT IN                                   "
                    sSql += "       (SELECT bldno || comcd_out FROM lb032m                            "
                    sSql += "         WHERE outdt BETWEEN :dates AND :datee || '235959'                                 "
                    sSql += "       )                                                                "

                    alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                    alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

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

                    alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsfDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsfDate))
                    alParm.Add(New OracleParameter("datee", OracleType.VarChar, rstDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rstDate))

                    If rsComcd <> "" And rsComcd <> "ALL" Then
                        sSql += "   AND a.comcd_out = :comcd                                                 "
                        alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                    End If

                    sSql += "   AND a.tnsjubsuno = b.tnsjubsuno                                      "
                    sSql += "   AND a.comcd      = b.comcd                                           "
                    sSql += "   AND a.bldno      = b.bldno                                           "
                    sSql += "   AND b.tnsjubsuno = c.tnsjubsuno                                      "
                    sSql += "   AND c.tnsgbn     = '3'                                               "

                    If rsRegno <> "" Then
                        sSql += "   and c.regno  = :regno                                                     "
                        alParm.Add(New OracleParameter("regno", OracleType.VarChar, rsRegno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsRegno))
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

                If rsGroup = "1"c Then
                    sSql += " , fn_ack_get_dept_name(b.iogbn, b.joincd)                     as gbnnm            "
                Else
                    sSql += " , b.comnmd                                                as gbnnm            "
                End If

                sSql += "     , b.rtnrsncd                                              as rsncd         "
                sSql += "     , c.cmtcont                                               as rsnnm         "
                sSql += "     , NVL(a.qty, 0)                                           as sumall        "
                sSql += "     , CASE WHEN SUM(NVL(a.qty, 0)) = 0 THEN 0"
                sSql += "            ELSE ROUND((SUM(NVL(b.qty, 0)) * 1.0 / SUM(a.qty)) * 100, 2)"
                sSql += "       END                                                     as per "
                sSql += "     , '1'                                                     as sortgbn       "
                sSql += "     , '2'                                                     as subgbn        "
                sSql += "     , SUM(CASE WHEN b.years = TO_NUMBER(year) - 1 THEN NVL(b.qty, 0) ELSE 0 END)  as year1 "
                sSql += "     , SUM(CASE WHEN b.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                as year2 "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT '1'            as joingbn                                        "
                sSql += "             , COUNT(a.bldno) as qty                                            "
                sSql += "          FROM (                                                         "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                 "
                sSql += "                  FROM lb030m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                     "


                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                         "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                            "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                 "
                sSql += "                  FROM lb031m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                               "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                                      "
                sSql += "               ) a                                                                "
                sSql += "             , lb040m b                                                         "
                sSql += "             , lb043m c                                                         "
                sSql += "             , lb020m d                                                         "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                         "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959' "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                 "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                      "
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

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                          "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                      "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "

                If rsGbn = "1"c Then
                    sSql += "       AND c.state      = '5'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND c.state      = '6'                                                  "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND a.comcd_out  = e.comcd                                              "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                    sSql += "       AND b.jubsudt   >= e.usdt                                            "
                    sSql += "       AND b.jubsudt   <  e.uedt                                            "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), b.deptcd, b.iogbn, a.rtnrsncd"
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), a.comcd_out, e.comnmd, a.rtnrsncd"
                End If

                sSql += "       ) b ON (a.joingbn  = b.joingbn)"
                sSql += "       INNER JOIN lf170m c ON (b.rtnrsncd = c.cmtcd)                              "

                If rsGbn = "1"c Then
                    sSql += " WHERE c.cmtgbn   = '0'                                                        "
                Else
                    sSql += " WHERE c.cmtgbn   = '1'                                                        "
                End If

                If rsGroup = "1"c Then
                    sSql += "GROUP BY b.joincd, b.iogbn, b.rtnrsncd, c.cmtcont, a.qty                              "
                Else
                    sSql += "GROUP BY b.joincd, b.rtnrsncd, c.cmtcont, b.comnmd, a.qty                             "
                End If

                sSql += "UNION ALL                                                                       "
                sSql += "SELECT b.joincd                                                as joincd        "
                sSql += "     , '        '                                              as gbnnm         "
                sSql += "     , 'aaaaa'                                                 as rsncd         "
                sSql += "     , '[합 계]'                                               as rsnnm         "
                sSql += "     , NVL(a.qty, 0)                                           as sumall        "
                sSql += "     , CASE WHEN SUM(NVL(a.qty, 0)) = 0 THEN 0                                  "
                sSql += "            ELSE ROUND((SUM(NVL(b.qty, 0)) * 1.0 / a.qty) * 100, 2)"
                sSql += "       END                                                     as per           "
                sSql += "     , '2'                                                     as sortgbn       "
                sSql += "     , '1'                                                     as subgbn        "
                sSql += "     , SUM(CASE WHEN b.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END) as year1"
                sSql += "     , SUM(CASE WHEN b.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                   as year2"

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT '1'            as joingbn                                        "
                sSql += "             , COUNT(a.bldno) as qty                                            "
                sSql += "          FROM (                                                         "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                 "
                sSql += "                  FROM lb030m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                 "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                      "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                            "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                 "
                sSql += "                  FROM lb031m                                                     "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                  "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                                      "
                sSql += "               ) a                                                                "
                sSql += "             , lb040m b                                                         "
                sSql += "             , lb043m c                                                         "
                sSql += "             , lb020m d                                                         "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                         "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'      "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                      "
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

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                      "
                sSql += "           AND a.bldno      = c.bldno                                           "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                      "
                sSql += "           AND a.comcd_out  = c.comcd_out                                       "

                If rsGbn = "1"c Then
                    sSql += "       AND c.state      = '5'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND c.state      = '6'                                                  "
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
                sSql += "       INNER JOIN lf170m c ON (b.rtnrsncd = c.cmtcd)                              "

                If rsGbn = "1"c Then
                    sSql += " WHERE c.cmtgbn   = '0'                                                        "
                Else
                    sSql += " WHERE c.cmtgbn   = '1'                                                        "
                End If

                If rsGroup = "1"c Then
                    sSql += " GROUP BY b.joincd, b.iogbn, a.qty                                           "
                Else
                    sSql += " GROUP BY b.joincd, b.comnmd, a.qty                        "
                End If

                sSql += " ORDER BY joincd, sortgbn, subgbn                                               "

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
                sSql += "SELECT b.joincd                                                                   "

                If rsGroup = "1"c Then
                    sSql += " , fn_ack_get_dept_name(b.iogbn, b.joincd)                 as gbnnm           "
                Else
                    sSql += " , b.comnmd                                                as gbnnm           "
                End If

                sSql += "     , b.rtnrsncd                                              as rsncd           "
                sSql += "     , c.cmtcont                                               as rsnnm           "
                sSql += "     , NVL(a.qty, 0)                                           as sumall          "
                sSql += "     , CASE WHEN a.qty = 0 THEN 0                                                 "
                sSql += "            ELSE ROUND((SUM(NVL(b.qty, 0)) * 1.0 / a.qty) * 100, 2)       "
                sSql += "       END                                                             as per     "
                sSql += "     , '1'                                                             as sortgbn "
                sSql += "     , '2'                                                             as subgbn  "
                sSql += "     , SUM(CASE WHEN b.months = '01' THEN NVL(b.qty, 0) ELSE 0 END) as m1      "
                sSql += "     , SUM(CASE WHEN b.months = '02' THEN NVL(b.qty, 0) ELSE 0 END) as m2      "
                sSql += "     , SUM(CASE WHEN b.months = '03' THEN NVL(b.qty, 0) ELSE 0 END) as m3      "
                sSql += "     , SUM(CASE WHEN b.months = '04' THEN NVL(b.qty, 0) ELSE 0 END) as m4      "
                sSql += "     , SUM(CASE WHEN b.months = '05' THEN NVL(b.qty, 0) ELSE 0 END) as m5      "
                sSql += "     , SUM(CASE WHEN b.months = '06' THEN NVL(b.qty, 0) ELSE 0 END) as m6      "
                sSql += "     , SUM(CASE WHEN b.months = '07' THEN NVL(b.qty, 0) ELSE 0 END) as m7      "
                sSql += "     , SUM(CASE WHEN b.months = '08' THEN NVL(b.qty, 0) ELSE 0 END) as m8      "
                sSql += "     , SUM(CASE WHEN b.months = '09' THEN NVL(b.qty, 0) ELSE 0 END) as m9      "
                sSql += "     , SUM(CASE WHEN b.months = '10' THEN NVL(b.qty, 0) ELSE 0 END) as m10     "
                sSql += "     , SUM(CASE WHEN b.months = '11' THEN NVL(b.qty, 0) ELSE 0 END) as m11     "
                sSql += "     , SUM(CASE WHEN b.months = '12' THEN NVL(b.qty, 0) ELSE 0 END) as m12     "
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy')                        as years   "
                sSql += "             , COUNT(a.bldno)                                          as qty     "
                sSql += "          FROM (                                                                  "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                 "
                sSql += "                  FROM lb030m                                                     "
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                                      "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                         "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                            "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                 "
                sSql += "                  FROM lb031m                                                     "
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'      "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                                      "
                sSql += "               ) a                                                                "
                sSql += "             , lb040m b                                                           "
                sSql += "             , lb043m c                                                           "
                sSql += "             , lb020m d                                                           "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                    "
                End If

                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "
                sSql += "           AND a.bldno      = d.bldno                                              "
                sSql += "           AND a.comcd_out  = d.comcd                                              "
                sSql += "           AND d.state     IN ('0', '4', '6')                                      "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                    sSql += "       AND b.jubsudt   >= e.usdt                                               "
                    sSql += "       AND b.jubsudt   <  e.uedt                                               "
                End If

                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy')                                  "
                sSql += "       ) a LEFT OUTER JOIN                                                         "
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years, fn_ack_date_str(a.outdt, 'MM') as months"

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    "
                    sSql += "         , b.iogbn                                                             "
                Else
                    sSql += "         , a.comcd_out            as joincd                                    "
                    sSql += "         , e.comnmd                                                            "
                End If

                sSql += "             , a.rtnrsncd                                                          "
                sSql += "             , COUNT(a.bldno)         as qty                                       "
                sSql += "          FROM lb031m a                                                            "
                sSql += "             , lb040m b                                                            "
                sSql += "             , lb043m c                                                            "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            "
                End If

                sSql += "         WHERE a.outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                                      "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "

                If rsGbn = "1"c Then
                    sSql += "       AND c.state      = '5'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND c.state      = '6'                                                  "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND a.comcd_out  = e.comcd                                              "
                    sSql += "       AND b.jubsudt   >= e.usdt                                               "
                    sSql += "       AND b.jubsudt   <  e.uedt                                               "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn, a.rtnrsncd    "
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd, a.rtnrsncd"
                End If

                sSql += "       ) b ON (a.years = b.years)"
                sSql += "       INNER JOIN"
                sSql += "             lf170m c ON (b.rtnrsncd = c.cmtcd)                                    "
                If rsGbn = "1"c Then
                    sSql += " WHERE c.cmtgbn   = '0'                                                        "
                Else
                    sSql += " WHERE c.cmtgbn   = '1'                                                        "
                End If

                If rsGroup = "1"c Then
                    sSql += "GROUP BY b.joincd, b.iogbn, b.rtnrsncd, c.cmtcont, a.qty                       "
                Else
                    sSql += "GROUP BY b.joincd, b.rtnrsncd, c.cmtcont, b.comnmd, a.qty                      "
                End If

                sSql += "UNION ALL                                                                          "
                sSql += "SELECT b.joincd                                                as joincd           "
                sSql += "     , '        '                                              as gbnnm            "
                sSql += "     , 'aaaaa'                                                 as rsncd            "
                sSql += "     , '[합 계]'                                               as rsnnm            "
                sSql += "     , NVL(a.qty, 0)                                           as sumall           "
                sSql += "     , ROUND((SUM(NVL(b.qty, 0)) * 1.0 / NVL(a.qty, 0)) * 100, 2) as per              "
                sSql += "     , '2'                                                     as sortgbn          "
                sSql += "     , '1'                                                     as subgbn           "
                sSql += "     , SUM(CASE WHEN b.months = '01' THEN NVL(b.qty, 0) ELSE 0 END)           as m1               "
                sSql += "     , SUM(CASE WHEN b.months = '02' THEN NVL(b.qty, 0) ELSE 0 END)           as m2               "
                sSql += "     , SUM(CASE WHEN b.months = '03' THEN NVL(b.qty, 0) ELSE 0 END)           as m3               "
                sSql += "     , SUM(CASE WHEN b.months = '04' THEN NVL(b.qty, 0) ELSE 0 END)           as m4               "
                sSql += "     , SUM(CASE WHEN b.months = '05' THEN NVL(b.qty, 0) ELSE 0 END)           as m5               "
                sSql += "     , SUM(CASE WHEN b.months = '06' THEN NVL(b.qty, 0) ELSE 0 END)           as m6               "
                sSql += "     , SUM(CASE WHEN b.months = '07' THEN NVL(b.qty, 0) ELSE 0 END)           as m7               "
                sSql += "     , SUM(CASE WHEN b.months = '08' THEN NVL(b.qty, 0) ELSE 0 END)           as m8               "
                sSql += "     , SUM(CASE WHEN b.months = '09' THEN NVL(b.qty, 0) ELSE 0 END)           as m9               "
                sSql += "     , SUM(CASE WHEN b.months = '10' THEN NVL(b.qty, 0) ELSE 0 END)           as m10              "
                sSql += "     , SUM(CASE WHEN b.months = '11' THEN NVL(b.qty, 0) ELSE 0 END)           as m11              "
                sSql += "     , SUM(CASE WHEN b.months = '12' THEN NVL(b.qty, 0) ELSE 0 END)           as m12              "
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                   "
                sSql += "             , COUNT(a.bldno)         as qty                                               "
                sSql += "          FROM ("
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt          "
                sSql += "                  FROM lb030m                                              "
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                          "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                     "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt          "
                sSql += "                  FROM lb031m                                              "
                sSql += "                 WHERE outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                         "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                               "
                sSql += "               ) a                                                                         "
                sSql += "             , lb040m b                                                                    "
                sSql += "             , lb043m c                                                                    "
                sSql += "             , lb020m d                                                                    "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                    "
                End If

                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "
                sSql += "           AND a.bldno      = d.bldno                                              "
                sSql += "           AND a.comcd_out  = d.comcd                                              "
                sSql += "           AND d.state     IN ('0', '4', '6')                                      "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                    sSql += "       AND b.jubsudt   <  e.uedt                                               "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                End If

                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy')                                  "
                sSql += "       ) a LEFT OUTER JOIN                                                         "
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years, fn_ack_date_str(a.outdt, 'MM') as months"

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    "
                    sSql += "         , b.iogbn                                                             "
                Else
                    sSql += "         , a.comcd_out            as joincd                                    "
                    sSql += "         , e.comnmd                                                            "
                End If

                sSql += "             , ''                     as rtnrsncd                                  "
                sSql += "             , COUNT(a.bldno)         as qty                                       "
                sSql += "          FROM lb031m a                                                            "
                sSql += "             , lb040m b                                                            "
                sSql += "             , lb043m c                                                            "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            "
                End If

                sSql += "         WHERE a.outdt BETWEEN :year || '0101000000' AND :year || '1231235959'                                            "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "

                If rsGbn = "1"c Then
                    sSql += "       AND c.state      = '5'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND c.state      = '6'                                                  "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND a.comcd_out  = e.comcd                                              "
                    sSql += "       AND b.jubsudt   >= e.usdt                                               "
                    sSql += "       AND b.jubsudt   <  e.uedt                                               "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn"
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd"
                End If

                sSql += "       ) b ON (a.years   = b.years)"

                If rsGroup = "1"c Then
                    sSql += " GROUP BY b.joincd, b.iogbn, a.qty                                              "
                Else
                    sSql += " GROUP BY b.joincd, b.comnmd, a.qty                                             "
                End If

                sSql += " ORDER BY joincd, sortgbn, subgbn                                                   "

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
                sSql += "SELECT a.joincd                                                                                       "

                If rsGroup = "1"c Then
                    sSql += " , fn_ack_get_dept_name(a.iogbn, a.joincd)                 as gbnnm                               "
                Else
                    sSql += " , a.comnmd                                                as gbnnm                               "
                End If

                sSql += "     , '2'                                                                               as sortgbn "
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)     as ayear1       "
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)                       as ayear2       "
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END)     as year1        "
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                       as year2        "
                sSql += "     , ROUND(SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1"
                sSql += "                 ELSE SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)"
                sSql += "            END * 100, 2)                                                       as pyear1       "
                sSql += "     , ROUND(SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                                "
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1             "
                sSql += "                 ELSE SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)"
                sSql += "            END * 100, 2)                                                  as pyear2       "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                             "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                                       "
                    sSql += "         , b.iogbn                                                                                "
                Else
                    sSql += "         , a.comcd_out            as joincd                                                       "
                    sSql += "         , e.comnmd                                                                               "
                End If

                sSql += "             , COUNT(a.bldno)           as qty                                                        "
                sSql += "          FROM ("
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb030m                                                      "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb031m                                                      "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                  "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                                       "
                sSql += "               ) a                                                                                    "
                sSql += "             , lb040m b                                                                               "
                sSql += "             , lb043m c                                                                               "
                sSql += "             , lb020m d                                                                               "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                               "
                End If

                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                                            "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                                            "
                sSql += "           AND a.comcd      = c.comcd                                                                 "
                sSql += "           AND a.bldno      = c.bldno                                                                 "
                sSql += "           AND a.bldno      = d.bldno                                                                 "
                sSql += "           AND a.comcd_out  = d.comcd                                                                 "
                sSql += "           AND d.state      in ('0', '4', '6')                                                        "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                                                 "
                    sSql += "       AND c.spccd      = e.spccd                                                                 "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), b.deptcd, b.iogbn                              "
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), a.comcd_out, e.comnmd                          "
                End If
                sSql += "       ) a LEFT OUTER JOIN"
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                              "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                                       "
                    sSql += "         , b.iogbn                                                                                "
                Else
                    sSql += "         , a.comcd_out            as joincd                                                       "
                    sSql += "         , e.comnmd                                                                               "
                End If

                sSql += "             , COUNT(a.bldno)           as qty                                                        "
                sSql += "          FROM lb031m a                                                                               "
                sSql += "             , lb040m b                                                                               "
                sSql += "             , lb043m c                                                                               "
                sSql += "             , lb020m d                                                                               "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                                               "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'                                                                 "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                                       "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                                            "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                                            "
                sSql += "           AND a.comcd      = c.comcd                                                                 "
                sSql += "           AND a.bldno      = c.bldno                                                                 "
                sSql += "           AND a.bldno      = d.bldno                                                                 "
                sSql += "           AND a.comcd_out  = d.comcd                                                                 "

                If rsGbn = "1"c Then
                    sSql += "       AND d.state      = '5'                                                                     "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND d.state      = '6'                                                                     "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                                                 "
                    sSql += "       AND c.spccd      = e.spccd                                                                 "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), b.deptcd, b.iogbn                              "
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy'), a.comcd_out, e.comnmd                          "
                End If
                sSql += "       ) b ON a.joincd = b.joincd AND a.years  = b.years                                              "

                If rsGroup = "1"c Then
                    sSql += " GROUP BY a.joincd, a.iogbn                                                                        "
                Else
                    sSql += " GROUP BY a.joincd, a.comnmd                                                                       "
                End If

                sSql += "UNION ALL                                                                                             "
                sSql += "SELECT '11'                                                                                as joincd  "
                sSql += "     , '총합계 :'                                                                          as gbnnm   "
                sSql += "     , '1'                                                                                 as sortgbn "
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)       as ayear1  "
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)                         as ayear2  "
                sSql += "     , SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END)       as year1   "
                sSql += "     , SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END)                         as year2   "
                sSql += "     , ROUND(SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /     "
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1"
                sSql += "                 ELSE SUM(CASE WHEN a.years = TO_NUMBER(:year) - 1 THEN NVL(a.qty, 0) ELSE 0 END)"
                sSql += "            END * 100, 2)                                                    as pyear1  "
                sSql += "     , ROUND(SUM(CASE WHEN a.years = :year THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                       "
                sSql += "            CASE WHEN SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1          "
                sSql += "                 ELSE SUM(CASE WHEN a.years = :year THEN NVL(a.qty, 0) ELSE 0 END)"
                sSql += "            END * 100, 2)                                                    as pyear2  "

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                                      "
                sSql += "             , COUNT(a.bldno)           as qty                                                        "
                sSql += "          FROM ("
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb030m                                                      "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                   "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb031m                                                      "
                sSql += "                 WHERE outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                   "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                                       "
                sSql += "               ) a                                                                               "
                sSql += "             , lb040m b                                                                               "
                sSql += "             , lb043m c                                                                               "
                sSql += "             , lb020m d                                                                               "
                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                                            "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                                            "
                sSql += "           AND a.comcd      = c.comcd                                                                 "
                sSql += "           AND a.bldno      = c.bldno                                                                 "
                sSql += "           AND a.bldno      = d.bldno                                                                 "
                sSql += "           AND a.comcd_out  = d.comcd                                                                 "
                sSql += "           AND d.state      in ('0', '4', '6')                                                        "
                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'yyyy')"
                sSql += "       ) a LEFT OUTER JOIN                                                  "
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'yyyy') as years                                              "
                sSql += "             , COUNT(a.bldno)           as qty                                                        "
                sSql += "          FROM lb031m a                                                                               "
                sSql += "             , lb040m b                                                                               "
                sSql += "             , lb043m c                                                                               "
                sSql += "             , lb020m d                                                                               "
                sSql += "         WHERE a.outdt BETWEEN :dates || '0101000000' AND :datee || '1231235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, (Convert.ToInt32(rsYear) - 1).ToString.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, (Convert.ToInt32(rsYear) - 1).ToString))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                                            "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                                            "
                sSql += "           AND a.comcd      = c.comcd                                                                 "
                sSql += "           AND a.bldno      = c.bldno                                                                 "
                sSql += "           AND a.bldno      = d.bldno                                                                 "
                sSql += "           AND a.comcd_out  = d.comcd                                                                 "

                If rsGbn = "1"c Then
                    sSql += "       AND d.state      = '5'                                                                     "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND d.state      = '6'                                                                     "
                End If

                sSql += "         GROUP BY fn_ack_date_str(a.outdt, 'yyyy')                                                    "
                sSql += "       ) b ON a.years = b.years                                                                       "
                sSql += "ORDER BY sortgbn, joincd                                                                              "

                DbCommand()
                Return DbExecuteQuery(sSql, alParm)
            Catch ex As Exception
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function

        Public Shared Function fn_percentOfRtnBloodM(ByVal rsGbn As String, ByVal rsYear As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable
            Dim sFn As String = "Public Shared Function fn_percentOfRtnBloodM(ByVal rsGbn As String, ByVal rsDate As String, ByVal rsGroup As String, ByVal rsComcd As String) As DataTable"
            Dim sSql As String = ""
            Dim alParm As New ArrayList

            Try
                sSql += "SELECT a.joincd                                                                    "

                If rsGroup = "1"c Then
                    sSql += " , fn_ack_get_dept_name(a.iogbn, a.joincd)                 as gbnnm            "
                Else
                    sSql += " , a.comnmd                                                as gbnnm            "
                End If

                sSql += "     , SUM(NVL(a.qty, 0))                                         as sumall        "
                sSql += "     , SUM(NVL(b.qty, 0))                                         as cnt           "
                sSql += "     , ROUND(SUM(NVL(b.qty, 0)) * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)    as per           "
                sSql += "     , '2'                                                        as sortgbn       "
                sSql += "     , SUM(CASE WHEN a.months = '01' THEN NVL(a.qty, 0) ELSE 0 END)              as am1           "
                sSql += "     , SUM(CASE WHEN a.months = '02' THEN NVL(a.qty, 0) ELSE 0 END)              as am2           "
                sSql += "     , SUM(CASE WHEN a.months = '03' THEN NVL(a.qty, 0) ELSE 0 END)              as am3           "
                sSql += "     , SUM(CASE WHEN a.months = '04' THEN NVL(a.qty, 0) ELSE 0 END)              as am4           "
                sSql += "     , SUM(CASE WHEN a.months = '05' THEN NVL(a.qty, 0) ELSE 0 END)              as am5           "
                sSql += "     , SUM(CASE WHEN a.months = '06' THEN NVL(a.qty, 0) ELSE 0 END)              as am6           "
                sSql += "     , SUM(CASE WHEN a.months = '07' THEN NVL(a.qty, 0) ELSE 0 END)              as am7           "
                sSql += "     , SUM(CASE WHEN a.months = '08' THEN NVL(a.qty, 0) ELSE 0 END)              as am8           "
                sSql += "     , SUM(CASE WHEN a.months = '09' THEN NVL(a.qty, 0) ELSE 0 END)              as am9           "
                sSql += "     , SUM(CASE WHEN a.months = '10' THEN NVL(a.qty, 0) ELSE 0 END)              as am10          "
                sSql += "     , SUM(CASE WHEN a.months = '11' THEN NVL(a.qty, 0) ELSE 0 END)              as am11          "
                sSql += "     , SUM(CASE WHEN a.months = '12' THEN NVL(a.qty, 0) ELSE 0 END)              as am12          "

                sSql += "     , SUM(CASE WHEN a.months = '01' THEN NVL(b.qty, 0) ELSE 0 END)              as m1            "
                sSql += "     , SUM(CASE WHEN a.months = '02' THEN NVL(b.qty, 0) ELSE 0 END)              as m2            "
                sSql += "     , SUM(CASE WHEN a.months = '03' THEN NVL(b.qty, 0) ELSE 0 END)              as m3            "
                sSql += "     , SUM(CASE WHEN a.months = '04' THEN NVL(b.qty, 0) ELSE 0 END)              as m4            "
                sSql += "     , SUM(CASE WHEN a.months = '05' THEN NVL(b.qty, 0) ELSE 0 END)              as m5            "
                sSql += "     , SUM(CASE WHEN a.months = '06' THEN NVL(b.qty, 0) ELSE 0 END)              as m6            "
                sSql += "     , SUM(CASE WHEN a.months = '07' THEN NVL(b.qty, 0) ELSE 0 END)              as m7            "
                sSql += "     , SUM(CASE WHEN a.months = '08' THEN NVL(b.qty, 0) ELSE 0 END)              as m8            "
                sSql += "     , SUM(CASE WHEN a.months = '09' THEN NVL(b.qty, 0) ELSE 0 END)              as m9            "
                sSql += "     , SUM(CASE WHEN a.months = '10' THEN NVL(b.qty, 0) ELSE 0 END)              as m10           "
                sSql += "     , SUM(CASE WHEN a.months = '11' THEN NVL(b.qty, 0) ELSE 0 END)              as m11           "
                sSql += "     , SUM(CASE WHEN a.months = '12' THEN NVL(b.qty, 0) ELSE 0 END)              as m12           "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '01' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                        "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '01' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '01' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm1 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '02' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '02' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '02' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm2 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '03' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '03' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '03' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm3 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '04' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '04' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '04' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm4 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '05' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '05' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '05' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm5 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '06' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '06' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '06' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                            as pm6 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '07' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '07' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '07' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm7 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '08' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '08' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '08' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm8 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '09' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '09' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '09' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm9 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '10' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '10' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '10' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm10"
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '11' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '11' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '11' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                             as pm11"
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '12' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                              "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '12' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1           "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '12' THEN NVL(a.qty, 0) ELSE 0 END)                      "
                sSql += "            END * 100, 2)                                                               as pm12"
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'MM') as months                                                    "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    "
                    sSql += "         , b.iogbn                                                             "
                Else
                    sSql += "         , a.comcd_out            as joincd                                    "
                    sSql += "         , e.comnmd                                                            "
                End If

                sSql += "             , COUNT(a.bldno)         as qty                                       "
                sSql += "          FROM (                                                                   "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb030m                                                      "
                sSql += "                 WHERE outdt BETWEEN :year || 0101000000 AND :year || '1231235959'"

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb031m                                                      "
                sSql += "                 WHERE outdt BETWEEN :year || 0101000000 AND :year || '1231235959'"

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                                       "
                sSql += "               ) a                                                                 "
                sSql += "             , lb040m b                                                            "
                sSql += "             , lb043m c                                                            "
                sSql += "             , lb020m d                                                            "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            "
                End If

                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "
                sSql += "           AND a.bldno      = d.bldno                                              "
                sSql += "           AND a.comcd_out  = d.comcd                                              "
                sSql += "           AND d.state      in ('0', '4', '6')                                     "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn             "
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd         "
                End If

                sSql += "       ) a LEFT OUTER JOIN"
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'MM') as months                            "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                                    "
                    sSql += "         , b.iogbn                                                             "
                Else
                    sSql += "         , a.comcd_out            as joincd                                    "
                    sSql += "         , e.comnmd                                                            "
                End If

                sSql += "             , COUNT(a.bldno)         as qty                                       "
                sSql += "          FROM lb031m a                                                            "
                sSql += "             , lb040m b                                                            "
                sSql += "             , lb043m c                                                            "
                sSql += "             , lb020m d                                                            "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                            "
                End If

                sSql += "         WHERE a.outdt BETWEEN :year || 0101000000 AND :year || '1231235959'"

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "
                sSql += "           AND a.bldno      = d.bldno                                              "
                sSql += "           AND a.comcd_out  = d.comcd                                              "

                If rsGbn = "1"c Then
                    sSql += "       AND d.state      = '5'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND d.state      = '6'                                                  "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                              "
                    sSql += "       AND c.spccd      = e.spccd                                              "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), b.deptcd, b.iogbn             "
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM'), a.comcd_out, e.comnmd         "
                End If

                sSql += "        ) b ON a.joincd = b.joincd AND a.months = b.months                         "

                If rsGroup = "1"c Then
                    sSql += "GROUP BY a.joincd, a.iogbn                                                     "
                Else
                    sSql += "GROUP BY a.joincd, a.comnmd                                                    "
                End If

                sSql += "UNION ALL                                                                          "
                sSql += "SELECT '11'                                                       as joincd        "
                sSql += "     , '총합계 :'                                                 as gbnnm         "
                sSql += "     , SUM(NVL(a.qty, 0))                                         as sumall        "
                sSql += "     , SUM(NVL(b.qty, 0))                                         as cnt           "
                sSql += "     , ROUND(SUM(NVL(b.qty, 0))  * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)    as per           "
                sSql += "     , '1'                                                        as sortgbn       "
                sSql += "     , SUM(CASE WHEN a.months = '01' THEN NVL(a.qty, 0) ELSE 0 END)              as am1           "
                sSql += "     , SUM(CASE WHEN a.months = '02' THEN NVL(a.qty, 0) ELSE 0 END)              as am2           "
                sSql += "     , SUM(CASE WHEN a.months = '03' THEN NVL(a.qty, 0) ELSE 0 END)              as am3           "
                sSql += "     , SUM(CASE WHEN a.months = '04' THEN NVL(a.qty, 0) ELSE 0 END)              as am4           "
                sSql += "     , SUM(CASE WHEN a.months = '05' THEN NVL(a.qty, 0) ELSE 0 END)              as am5           "
                sSql += "     , SUM(CASE WHEN a.months = '06' THEN NVL(a.qty, 0) ELSE 0 END)              as am6           "
                sSql += "     , SUM(CASE WHEN a.months = '07' THEN NVL(a.qty, 0) ELSE 0 END)              as am7           "
                sSql += "     , SUM(CASE WHEN a.months = '08' THEN NVL(a.qty, 0) ELSE 0 END)              as am8           "
                sSql += "     , SUM(CASE WHEN a.months = '09' THEN NVL(a.qty, 0) ELSE 0 END)              as am9           "
                sSql += "     , SUM(CASE WHEN a.months = '10' THEN NVL(a.qty, 0) ELSE 0 END)              as am10          "
                sSql += "     , SUM(CASE WHEN a.months = '11' THEN NVL(a.qty, 0) ELSE 0 END)              as am11          "
                sSql += "     , SUM(CASE WHEN a.months = '12' THEN NVL(a.qty, 0) ELSE 0 END)              as am12          "

                sSql += "     , SUM(CASE WHEN a.months = '01' THEN NVL(b.qty, 0) ELSE 0 END)              as m1            "
                sSql += "     , SUM(CASE WHEN a.months = '02' THEN NVL(b.qty, 0) ELSE 0 END)              as m2            "
                sSql += "     , SUM(CASE WHEN a.months = '03' THEN NVL(b.qty, 0) ELSE 0 END)              as m3            "
                sSql += "     , SUM(CASE WHEN a.months = '04' THEN NVL(b.qty, 0) ELSE 0 END)              as m4            "
                sSql += "     , SUM(CASE WHEN a.months = '05' THEN NVL(b.qty, 0) ELSE 0 END)              as m5            "
                sSql += "     , SUM(CASE WHEN a.months = '06' THEN NVL(b.qty, 0) ELSE 0 END)              as m6            "
                sSql += "     , SUM(CASE WHEN a.months = '07' THEN NVL(b.qty, 0) ELSE 0 END)              as m7            "
                sSql += "     , SUM(CASE WHEN a.months = '08' THEN NVL(b.qty, 0) ELSE 0 END)              as m8            "
                sSql += "     , SUM(CASE WHEN a.months = '09' THEN NVL(b.qty, 0) ELSE 0 END)              as m9            "
                sSql += "     , SUM(CASE WHEN a.months = '10' THEN NVL(b.qty, 0) ELSE 0 END)              as m10           "
                sSql += "     , SUM(CASE WHEN a.months = '11' THEN NVL(b.qty, 0) ELSE 0 END)              as m11           "
                sSql += "     , SUM(CASE WHEN a.months = '12' THEN NVL(b.qty, 0) ELSE 0 END)              as m12           "

                sSql += "     , ROUND(SUM(CASE WHEN a.months = '01' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '01' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '01' THEN NVL(a.qty, 0) ELSE 0 END)"
                sSql += "            END * 100, 2)                                                          as pm1 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '02' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '02' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '02' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                          as pm2 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '03' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '03' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '03' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                          as pm3 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '04' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '04' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '04' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                          as pm4 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '05' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '05' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '05' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                        as pm5 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '06' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '06' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '06' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                          as pm6 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '07' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '07' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '07' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                        as pm7 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '08' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '08' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '08' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                        as pm8 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '09' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '09' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '09' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END, 2)                                                        as pm9 "
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '10' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '10' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '10' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                        as pm10"
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '11' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '11' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '11' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                        as pm11"
                sSql += "     , ROUND(SUM(CASE WHEN a.months = '12' THEN NVL(b.qty, 0) ELSE 0 END) * 1.0 /                         "
                sSql += "            CASE WHEN SUM(CASE WHEN a.months = '12' THEN NVL(a.qty, 0) ELSE 0 END) = 0 THEN 1      "
                sSql += "                 ELSE SUM(CASE WHEN a.months = '12' THEN NVL(a.qty, 0) ELSE 0 END)                 "
                sSql += "            END * 100, 2)                                                        as pm12"
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'MM') as months                                               "
                sSql += "             , COUNT(a.bldno)         as qty                                       "
                sSql += "          FROM (                                                                   "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb030m                                                      "
                sSql += "                 WHERE outdt BETWEEN :year || 0101000000 AND :year || '1231235959'"

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                             "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt                  "
                sSql += "                  FROM lb031m                                                      "
                sSql += "                 WHERE outdt BETWEEN :year || 0101000000 AND :year || '1231235959'"

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                                    "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                                       "
                sSql += "               ) a                                                                 "
                sSql += "             , lb040m b                                                            "
                sSql += "             , lb043m c                                                            "
                sSql += "             , lb020m d                                                            "
                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "
                sSql += "           AND a.bldno      = d.bldno                                              "
                sSql += "           AND a.comcd_out  = d.comcd                                              "
                sSql += "           AND d.state      in ('0', '4', '6')                                     "
                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM')                                    "
                sSql += "       ) a LEFT OUTER JOIN                                                         "
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'MM') as months                            "
                sSql += "             , COUNT(a.bldno)         as qty                                       "
                sSql += "          FROM lb031m a                                                            "
                sSql += "             , lb040m b                                                            "
                sSql += "             , lb043m c                                                            "
                sSql += "             , lb020m d                                                            "
                sSql += "         WHERE a.outdt BETWEEN :year || 0101000000 AND :year || '1231235959'"

                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))
                alParm.Add(New OracleParameter("year", OracleType.VarChar, rsYear.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsYear))

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                         "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                         "
                sSql += "           AND a.comcd      = c.comcd                                              "
                sSql += "           AND a.bldno      = c.bldno                                              "
                sSql += "           AND a.bldno      = d.bldno                                              "
                sSql += "           AND a.comcd_out  = d.comcd                                              "

                If rsGbn = "1"c Then
                    sSql += "       AND d.state      = '5'                                                  "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND d.state      = '6'                                                  "
                End If

                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'MM')                                    "
                sSql += "       ) b ON a.months = b.months                                                  "
                sSql += " ORDER BY sortgbn, joincd                                                          "

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

                sSql += "SELECT a.joincd                                                            "

                If rsGroup = "1"c Then
                    sSql += " , fn_ack_get_dept_name(a.iogbn, a.joincd)                                    as gbnnm    "
                Else
                    sSql += " , a.comnmd                                                                   as gbnnm    "
                End If

                sSql += "     , SUM(NVL(a.qty, 0))                                                      as sumall   "
                sSql += "     , SUM(NVL(b.qty, 0))                                                      as cnt      "
                sSql += "     , ROUND(SUM(NVL(b.qty, 0)) * 1.0 / sum(NVL(a.qty, 0)) * 100, 2)           as per      "
                sSql += "     , '2'                                                                     as sortgbn  "
                sSql += "     , SUM(CASE WHEN a.days = '01' THEN NVL(b.qty, 0) ELSE 0 END)              as d1       "
                sSql += "     , SUM(CASE WHEN a.days = '02' THEN NVL(b.qty, 0) ELSE 0 END)              as d2       "
                sSql += "     , SUM(CASE WHEN a.days = '03' THEN NVL(b.qty, 0) ELSE 0 END)              as d3       "
                sSql += "     , SUM(CASE WHEN a.days = '04' THEN NVL(b.qty, 0) ELSE 0 END)              as d4       "
                sSql += "     , SUM(CASE WHEN a.days = '05' THEN NVL(b.qty, 0) ELSE 0 END)              as d5       "
                sSql += "     , SUM(CASE WHEN a.days = '06' THEN NVL(b.qty, 0) ELSE 0 END)              as d6       "
                sSql += "     , SUM(CASE WHEN a.days = '07' THEN NVL(b.qty, 0) ELSE 0 END)              as d7       "
                sSql += "     , SUM(CASE WHEN a.days = '08' THEN NVL(b.qty, 0) ELSE 0 END)              as d8       "
                sSql += "     , SUM(CASE WHEN a.days = '09' THEN NVL(b.qty, 0) ELSE 0 END)              as d9       "
                sSql += "     , SUM(CASE WHEN a.days = '10' THEN NVL(b.qty, 0) ELSE 0 END)              as d10      "
                sSql += "     , SUM(CASE WHEN a.days = '11' THEN NVL(b.qty, 0) ELSE 0 END)              as d11      "
                sSql += "     , SUM(CASE WHEN a.days = '12' THEN NVL(b.qty, 0) ELSE 0 END)              as d12      "
                sSql += "     , SUM(CASE WHEN a.days = '13' THEN NVL(b.qty, 0) ELSE 0 END)              as d13      "
                sSql += "     , SUM(CASE WHEN a.days = '14' THEN NVL(b.qty, 0) ELSE 0 END)              as d14      "
                sSql += "     , SUM(CASE WHEN a.days = '15' THEN NVL(b.qty, 0) ELSE 0 END)              as d15      "
                sSql += "     , SUM(CASE WHEN a.days = '16' THEN NVL(b.qty, 0) ELSE 0 END)              as d16      "
                sSql += "     , SUM(CASE WHEN a.days = '17' THEN NVL(b.qty, 0) ELSE 0 END)              as d17      "
                sSql += "     , SUM(CASE WHEN a.days = '18' THEN NVL(b.qty, 0) ELSE 0 END)              as d18      "
                sSql += "     , SUM(CASE WHEN a.days = '19' THEN NVL(b.qty, 0) ELSE 0 END)              as d19      "
                sSql += "     , SUM(CASE WHEN a.days = '20' THEN NVL(b.qty, 0) ELSE 0 END)              as d20      "
                sSql += "     , SUM(CASE WHEN a.days = '21' THEN NVL(b.qty, 0) ELSE 0 END)              as d21      "
                sSql += "     , SUM(CASE WHEN a.days = '22' THEN NVL(b.qty, 0) ELSE 0 END)              as d22      "
                sSql += "     , SUM(CASE WHEN a.days = '23' THEN NVL(b.qty, 0) ELSE 0 END)              as d23      "
                sSql += "     , SUM(CASE WHEN a.days = '24' THEN NVL(b.qty, 0) ELSE 0 END)              as d24      "
                sSql += "     , SUM(CASE WHEN a.days = '25' THEN NVL(b.qty, 0) ELSE 0 END)              as d25      "
                sSql += "     , SUM(CASE WHEN a.days = '26' THEN NVL(b.qty, 0) ELSE 0 END)              as d26      "
                sSql += "     , SUM(CASE WHEN a.days = '27' THEN NVL(b.qty, 0) ELSE 0 END)              as d27      "
                sSql += "     , SUM(CASE WHEN a.days = '28' THEN NVL(b.qty, 0) ELSE 0 END)              as d28      "
                sSql += "     , SUM(CASE WHEN a.days = '29' THEN NVL(b.qty, 0) ELSE 0 END)              as d29      "
                sSql += "     , SUM(CASE WHEN a.days = '30' THEN NVL(b.qty, 0) ELSE 0 END)              as d30      "
                sSql += "     , SUM(CASE WHEN a.days = '31' THEN NVL(b.qty, 0) ELSE 0 END)              as d31      "
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'DD') as days                              "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                            "
                    sSql += "         , b.iogbn                                                     "
                Else
                    sSql += "         , c.comcd_out            as joincd                            "
                    sSql += "         , e.comnmd                                                    "
                End If

                sSql += "             , COUNT(a.bldno)         as qty                               "
                sSql += "          FROM ("
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt          "
                sSql += "                  FROM lb030m                                              "
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                  "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                     "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt          "
                sSql += "                  FROM lb031m                                              "
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'"

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                 "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                               "
                sSql += "               ) a                                                         "
                sSql += "             , lb040m b                                                    "
                sSql += "             , lb043m c                                                    "
                sSql += "             , lb020m d                                                    "

                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                    "
                End If

                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                 "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                 "
                sSql += "           AND a.comcd      = c.comcd                                      "
                sSql += "           AND a.bldno      = c.bldno                                      "
                sSql += "           AND a.bldno      = d.bldno                                      "
                sSql += "           AND a.comcd_out  = d.comcd                                      "
                sSql += "           AND d.state     IN ('0', '4', '6')                              "

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                      "
                    sSql += "       AND c.spccd      = e.spccd                                      "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), b.deptcd, b.iogbn     "
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), c.comcd_out, e.comnmd "
                End If
                sSql += "       ) a LEFT OUTER JOIN                                                 "
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'DD') as days                      "

                If rsGroup = "1"c Then
                    sSql += "         , b.deptcd               as joincd                            "
                    sSql += "         , b.iogbn                                                     "
                Else
                    sSql += "         , a.comcd_out            as joincd                            "
                    sSql += "         , e.comnmd                                                    "
                End If

                sSql += "             , COUNT(a.bldno)         as qty                               "
                sSql += "          FROM lb031m a                                                    "
                sSql += "             , lb040m b                                                    "
                sSql += "             , lb043m c                                                    "
                sSql += "             , lb020m d                                                    "


                If rsGroup = "2"c Then
                    sSql += "         , lf120m e                                                    "
                End If

                sSql += "         WHERE a.outdt BETWEEN :dates AND :datee || '235959'                                  "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "       AND a.comcd_out  = :comcd                                            "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                 "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                 "
                sSql += "           AND a.comcd      = c.comcd                                      "
                sSql += "           AND a.bldno      = c.bldno                                      "
                sSql += "           AND a.bldno      = d.bldno                                      "
                sSql += "           AND a.comcd_out  = d.comcd                                      "

                If rsGbn = "1"c Then
                    sSql += "       AND d.state      = '5'                                          "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND d.state      = '6'                                          "
                End If

                If rsGroup = "2"c Then
                    sSql += "       AND d.comcd      = e.comcd                                      "
                    sSql += "       AND c.spccd      = e.spccd                                      "
                End If

                If rsGroup = "1"c Then
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), b.deptcd, b.iogbn     "
                Else
                    sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD'), a.comcd_out, e.comnmd "
                End If
                sSql += "       ) b ON a.joincd = b.joincd AND a.days   = b.days                    "

                If rsGroup = "1"c Then
                    sSql += " GROUP BY a.joincd, a.iogbn                                             "
                Else
                    sSql += " GROUP BY a.joincd, a.comnmd                                            "
                End If

                sSql += "UNION ALL                                                                  "
                sSql += "SELECT '11'                                                    as joincd   "
                sSql += "     , '총합계 :'                                              as gbnnm    "
                sSql += "     , SUM(NVL(a.qty, 0))                                   as sumall   "
                sSql += "     , SUM(NVL(b.qty, 0))                                   as cnt      "
                sSql += "     , ROUND(SUM(NVL(b.qty, 0)) * 1.0 / sum(NVL(a.qty, 0)) * 100, 2) as per      "
                sSql += "     , '1'                                                     as sortgbn  "
                sSql += "     , SUM(CASE WHEN a.days = '01' THEN NVL(b.qty, 0) ELSE 0 END)             as d1       "
                sSql += "     , SUM(CASE WHEN a.days = '02' THEN NVL(b.qty, 0) ELSE 0 END)             as d2       "
                sSql += "     , SUM(CASE WHEN a.days = '03' THEN NVL(b.qty, 0) ELSE 0 END)             as d3       "
                sSql += "     , SUM(CASE WHEN a.days = '04' THEN NVL(b.qty, 0) ELSE 0 END)             as d4       "
                sSql += "     , SUM(CASE WHEN a.days = '05' THEN NVL(b.qty, 0) ELSE 0 END)             as d5       "
                sSql += "     , SUM(CASE WHEN a.days = '06' THEN NVL(b.qty, 0) ELSE 0 END)             as d6       "
                sSql += "     , SUM(CASE WHEN a.days = '07' THEN NVL(b.qty, 0) ELSE 0 END)             as d7       "
                sSql += "     , SUM(CASE WHEN a.days = '08' THEN NVL(b.qty, 0) ELSE 0 END)             as d8       "
                sSql += "     , SUM(CASE WHEN a.days = '09' THEN NVL(b.qty, 0) ELSE 0 END)             as d9       "
                sSql += "     , SUM(CASE WHEN a.days = '10' THEN NVL(b.qty, 0) ELSE 0 END)             as d10      "
                sSql += "     , SUM(CASE WHEN a.days = '11' THEN NVL(b.qty, 0) ELSE 0 END)             as d11      "
                sSql += "     , SUM(CASE WHEN a.days = '12' THEN NVL(b.qty, 0) ELSE 0 END)             as d12      "
                sSql += "     , SUM(CASE WHEN a.days = '13' THEN NVL(b.qty, 0) ELSE 0 END)             as d13      "
                sSql += "     , SUM(CASE WHEN a.days = '14' THEN NVL(b.qty, 0) ELSE 0 END)             as d14      "
                sSql += "     , SUM(CASE WHEN a.days = '15' THEN NVL(b.qty, 0) ELSE 0 END)             as d15      "
                sSql += "     , SUM(CASE WHEN a.days = '16' THEN NVL(b.qty, 0) ELSE 0 END)             as d16      "
                sSql += "     , SUM(CASE WHEN a.days = '17' THEN NVL(b.qty, 0) ELSE 0 END)             as d17      "
                sSql += "     , SUM(CASE WHEN a.days = '18' THEN NVL(b.qty, 0) ELSE 0 END)             as d18      "
                sSql += "     , SUM(CASE WHEN a.days = '19' THEN NVL(b.qty, 0) ELSE 0 END)             as d19      "
                sSql += "     , SUM(CASE WHEN a.days = '20' THEN NVL(b.qty, 0) ELSE 0 END)             as d20      "
                sSql += "     , SUM(CASE WHEN a.days = '21' THEN NVL(b.qty, 0) ELSE 0 END)             as d21      "
                sSql += "     , SUM(CASE WHEN a.days = '22' THEN NVL(b.qty, 0) ELSE 0 END)             as d22      "
                sSql += "     , SUM(CASE WHEN a.days = '23' THEN NVL(b.qty, 0) ELSE 0 END)             as d23      "
                sSql += "     , SUM(CASE WHEN a.days = '24' THEN NVL(b.qty, 0) ELSE 0 END)             as d24      "
                sSql += "     , SUM(CASE WHEN a.days = '25' THEN NVL(b.qty, 0) ELSE 0 END)             as d25      "
                sSql += "     , SUM(CASE WHEN a.days = '26' THEN NVL(b.qty, 0) ELSE 0 END)             as d26      "
                sSql += "     , SUM(CASE WHEN a.days = '27' THEN NVL(b.qty, 0) ELSE 0 END)             as d27      "
                sSql += "     , SUM(CASE WHEN a.days = '28' THEN NVL(b.qty, 0) ELSE 0 END)             as d28      "
                sSql += "     , SUM(CASE WHEN a.days = '29' THEN NVL(b.qty, 0) ELSE 0 END)             as d29      "
                sSql += "     , SUM(CASE WHEN a.days = '30' THEN NVL(b.qty, 0) ELSE 0 END)             as d30      "
                sSql += "     , SUM(CASE WHEN a.days = '31' THEN NVL(b.qty, 0) ELSE 0 END)             as d31      "
                sSql += "  FROM (SELECT fn_ack_date_str(a.outdt, 'DD') as days                              "
                sSql += "             , COUNT(a.bldno)         as qty                               "
                sSql += "          FROM (SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt          "
                sSql += "                  FROM lb030m                                              "
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'                            "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                                "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                 UNION                                                     "
                sSql += "                SELECT bldno, comcd_out, tnsjubsuno, comcd, outdt          "
                sSql += "                  FROM lb031m                                              "
                sSql += "                 WHERE outdt BETWEEN :dates AND :datee || '235959'             "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                If rsComcd <> "ALL" Then
                    sSql += "                   AND comcd_out  = :comcd                               "
                    alParm.Add(New OracleParameter("comcd", OracleType.VarChar, rsComcd.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsComcd))
                End If

                sSql += "                   AND keepgbn IN ('3', '4')                               "
                sSql += "               ) a                                                         "
                sSql += "             , lb040m b                                                    "
                sSql += "             , lb043m c                                                    "
                sSql += "             , lb020m d                                                    "
                sSql += "         WHERE a.tnsjubsuno = b.tnsjubsuno                                 "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                 "
                sSql += "           AND a.bldno      = c.bldno                                      "
                sSql += "           AND a.comcd      = c.comcd                                      "
                sSql += "           AND a.bldno      = d.bldno                                      "
                sSql += "           AND a.comcd_out  = d.comcd                                      "
                sSql += "           AND d.state      in ('0', '4', '6')                             "
                sSql += "        GROUP BY fn_ack_date_str(a.outdt, 'DD')                            "
                sSql += "       ) a LEFT OUTER JOIN                                                 "
                sSql += "       (SELECT fn_ack_date_str(a.outdt, 'DD') as days                      "
                sSql += "             , COUNT(a.bldno)         as qty                               "
                sSql += "          FROM lb031m a                                                    "
                sSql += "             , lb040m b                                                    "
                sSql += "             , lb043m c                                                    "
                sSql += "             , lb020m d                                                    "
                sSql += "         WHERE a.outdt BETWEEN :dates AND :datee || '235959'                           "

                alParm.Add(New OracleParameter("dates", OracleType.VarChar, rsDate.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsDate))
                alParm.Add(New OracleParameter("datee", OracleType.VarChar, sLastDay.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, sLastDay))

                sSql += "           AND a.tnsjubsuno = b.tnsjubsuno                                 "
                sSql += "           AND a.tnsjubsuno = c.tnsjubsuno                                 "
                sSql += "           AND a.comcd      = c.comcd                                      "
                sSql += "           AND a.bldno      = c.bldno                                      "
                sSql += "           AND a.bldno      = d.bldno                                      "
                sSql += "           AND a.comcd_out  = d.comcd                                      "

                If rsGbn = "1"c Then
                    sSql += "       AND d.state      = '5'                                          "
                ElseIf rsGbn = "2"c Then
                    sSql += "       AND d.state      = '6'                                          "
                End If

                sSql += "         GROUP BY fn_ack_date_str(a.outdt, 'DD')                           "
                sSql += "       ) b ON a.days   = b.days                                            "
                sSql += " ORDER BY sortgbn, joincd                                                  "

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
                sSql += "SELECT fn_ack_get_bldno_full(a.bldno)                  as vbldno  "
                sSql += "     , b.comnmd                                        as comnmd  "
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

                alParm.Add(New OracleParameter("bldno", OracleType.VarChar, rsBldno.Length, ParameterDirection.Input, Nothing, Nothing, Nothing, Nothing, DataRowVersion.Current, rsBldno))

                DbCommand()

                Return DbExecuteQuery(sSql, alParm)

            Catch ex As Exception
                Fn.log(msFile + sFn, Err)
                Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
            End Try
        End Function
#End Region

    End Class

#Region " 수혈 구조체 선언 "
    Public Class clsTnsJubsu
        Public REGNO As String = ""         ' 등록번호
        Public PATNM As String = ""         ' 환자명
        Public SEX As String = ""           ' 성별
        Public AGE As String = ""           ' 나이
        Public ORDDATE As String = ""       ' 처방일자
        Public DEPTCD As String = ""        ' 진료과
        Public DRCD As String = ""          ' 진료의
        Public WARDCD As String = ""        ' 병동
        Public ROOMNO As String = ""        ' 병실
        Public COMCD As String = ""         ' 성분제제코드
        Public COMNM As String = ""         ' 성분제제코드
        Public COMORDCD As String = ""      ' 원처방코드
        Public SPCCD As String = ""         ' 검체코드
        Public OWNGBN As String = ""        ' 처방소유구분
        Public TNSJUBSUNO As String = ""    ' 수혈의뢰접수번호
        Public FKOCS As String = ""         ' 외래처방키
        Public SEQ As String = ""           ' 순번
        Public BLDNO As String = ""         ' 혈액번호
        Public IOGBN As String = ""         ' 입외구분
        Public BCNO As String = ""          ' 검체번호
        Public STATE As String = ""         ' 상태
        Public FILTER As String = ""        ' 필터
        Public WORKID As String = ""        ' 
        Public RST1 As String = ""          ' 크로스결과4
        Public RST2 As String = ""          ' 크로스결과4
        Public RST3 As String = ""          ' 크로스결과4
        Public RST4 As String = ""          ' 크로스결과4
        Public CMRMK As String = ""         ' 리마크
        Public TESTGBN As String = ""       ' 검사구분
        Public TESTID As String = ""        ' 검사자
        Public BEFOUTID As String = ""      ' 가출고아이디
        Public OUTID As String = ""         ' 출고자아이디
        Public RECID As String = ""         ' 수령자아이디
        Public RECNM As String = ""         ' 수령자명
        Public RTNREQID As String = ""      ' 반납/폐기 의뢰자
        Public RTNREQNM As String = ""      ' 반납/폐기 의뢰자명
        Public RTNCODE As String = ""       ' 반납사유코드
        Public RTNCMT As String = ""        ' 반납사유
        Public EMER As String = ""          ' 응급
        Public IR As String = ""            ' 이라데이션
        Public COMCD_OUT As String = ""     ' 출고용 성분제제
        Public EDITIP As String = ""        ' 수정자IP
        Public TEMP01 As String = ""        ' 여유1
        Public TEMP02 As String = ""        ' 여유2
        Public TEMP03 As String = ""        ' 여유3

        Public ABO As String = ""           ' ABO 혈액형
        Public RH As String = ""            ' Rh 혈액형

        Public RTNDT As String = ""   ' 반납/폐기일시
    End Class

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

#End Region


End Namespace
