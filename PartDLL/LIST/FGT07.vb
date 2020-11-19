'>> TAT 관리
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports common.commlogin.login
Imports LISAPP.APP_T

Public Class FGT07
    Inherits System.Windows.Forms.Form
    Private m_tooltip As New Windows.Forms.ToolTip

    Private Sub sbDisplay_Statistics()
        Try
            Dim sIoGbn As String = ""
            Dim sState As String = ""
            Dim sWardCd As String = ""
            Dim sDeptCd As String = ""
            Dim sTestCds As String = ""
            Dim sRstGbn As String = ""

            If rdoIOI.Checked Then
                sIoGbn = "I"
            ElseIf rdoIOO.Checked Then
                sIoGbn = "O"
            ElseIf rdoIOC.Checked Then
                sIoGbn = "C"
            End If

            If rdoEmerN.Checked Then
                sState = "N"
            ElseIf rdoEmerY.Checked Then
                sState = "Y"
            End If

            '<< 보고구분
            If rdoRstMw.Checked Then
                sRstGbn = "M" '중간보고
            ElseIf rdoRstFn.Checked Then
                sRstGbn = "F" '최종보고
            End If

            sbDisplay_Hour()

            If Me.rdoWardS.Checked Then sWardCd = Me.cboWard.Text.Split("|"c)(1)
            If Me.rdoDeptS.Checked Then sDeptCd = Me.cboDept.Text.Split("|"c)(1)


            sTestCds = Ctrl.Get_Code_Tag(Me.lblTnmd)

            If sTestCds.Length > 0 Then
                sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"
            End If


            'Dim dt As DataTable = (New SrhFn).fnGet_TatTest_Statistics(Me.dtpTkDt1.Text.Replace("-", "") + Me.cboHour1.Text, Me.dtpTkDt2.Text.Replace("-", "") + Me.cboHour2.Text, _
            '                                                          sIoGbn, sDeptCd, sWardCd, Ctrl.Get_Code(Me.cboSlip), sTestCds, _
            '                                                         Ctrl.Get_Code(Me.cboSpcCd).Trim, sState, Me.chkVerity.Checked, Me.chkPDCA.Checked, Me.chkIoGbn_noC.Checked)

            Dim dt As DataTable = (New SrhFn).fnGet_TatTest_Statistics_new(Me.dtpTkDt1.Text.Replace("-", "") + Me.cboHour1.Text, Me.dtpTkDt2.Text.Replace("-", "") + Me.cboHour2.Text, _
                                                                       sIoGbn, sDeptCd, sWardCd, Ctrl.Get_Code(Me.cboSlip), sTestCds, _
                                                                       Ctrl.Get_Code(Me.cboSpcCd).Trim, sState, Me.chkVerity.Checked, Me.chkPDCA.Checked, Me.chkIoGbn_noC.Checked, sRstGbn)

            If dt.Rows.Count < 1 Then Return

            spdStatistics.ReDraw = False
            Dim lTm_tot As Long = 0, lTCnt_tot As Long = 0
            Dim lTm_tot_o As Long = 0, lTCnt_tot_o As Long = 0
            Dim iMin As Integer = 50000, iMax As Integer = 0

            With spdStatistics
                Dim sTkTm As String = ""
                Dim lTm As Long = 0, lTCnt As Long = 0
                Dim iRow_tktm As Integer = 0

                For ix1 As Integer = 0 To dt.Rows.Count - 1
                    Dim iRow As Integer = 0

                    For ix2 As Integer = 1 To .MaxRows
                        .Row = ix2
                        .Col = .GetColFromID("hour") : Dim sTkDt1 As String = .Text.Substring(0, 5) : Dim stkdt2 As String = .Text.Substring(8).Trim

                        If sTkDt1 <= dt.Rows(ix1).Item("tk_tm").ToString And dt.Rows(ix1).Item("tk_tm").ToString <= stkdt2 Then
                            iRow = ix2
                            Exit For
                        End If
                    Next

                    If sTkTm <> "" And sTkTm <> dt.Rows(ix1).Item("tk_tm").ToString.Substring(0, 2) Then
                        .Row = iRow_tktm
                        If lTCnt > 0 Then .Col = .GetColFromID("ave") : .Text = Format(lTm / lTCnt, "#0").ToString

                        lTm = 0 : lTCnt = 0
                    End If

                    Dim sRst_tm As String = Convert.ToInt32(Int(dt.Rows(ix1).Item("rst_tm").ToString)).ToString '수정
                    Dim sRst_tm2 As String = Convert.ToDouble(dt.Rows(ix1).Item("rst_tm").ToString).ToString()

                    lTm += Convert.ToInt32(sRst_tm) * Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)
                    lTCnt += Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)

                    lTm_tot += Convert.ToInt32(sRst_tm) * Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)
                    lTCnt_tot += Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)

                    If Val(sRst_tm) > iMax Then iMax = Convert.ToInt32(Val(sRst_tm))
                    If Val(sRst_tm) < iMin Then iMin = Convert.ToInt32(Val(sRst_tm))

                    .Row = iRow
                    .Col = .GetColFromID("cnt") : .Text = (Convert.ToInt32(IIf(.Text = "", "0", .Text)) + Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)).ToString
                    .Col = .GetColFromID("min") : If .Text = "" Or Val(.Text) > Val(sRst_tm) Then .Text = sRst_tm
                    .Col = .GetColFromID("max") : If .Text = "" Or Val(.Text) < Val(sRst_tm) Then .Text = sRst_tm

                    If Val(sRst_tm2) > Val(txtTat.Text) Then
                        .Col = .GetColFromID("over") : .Text = (Convert.ToInt32(IIf(.Text = "", "0", .Text)) + Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)).ToString
                    End If

                    iRow_tktm = iRow
                    sTkTm = dt.Rows(ix1).Item("tk_tm").ToString.Substring(0, 2)
                Next

                If sTkTm <> "" Then
                    .Row = iRow_tktm
                    If lTCnt > 0 Then .Col = .GetColFromID("ave") : .Text = Format(lTm / lTCnt, "#0").ToString

                    lTm = 0 : lTCnt = 0
                End If

                '-- TAT 충족율(%)
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("cnt") : Dim lCnt As Long = Convert.ToInt32(Val(.Text))
                    .Col = .GetColFromID("over") : Dim iOver As Integer = Convert.ToInt32(Val(.Text))
                    If lCnt > 0 Then
                        .Col = .GetColFromID("per") : .Text = Format(((lCnt - iOver) / lCnt) * 100, "#0.0")
                    End If
                Next
            End With
            ' Me.txtTclsCd.Text
            '-- 기간대비
            'dt = (New SrhFn).fnGet_TatTest_Statistics(Me.dtpOTkDt1.Text.Replace("-", "") + Me.cboHour1.Text, Me.dtpOTkDt2.Text.Replace("-", "") + Me.cboHour2.Text, _
            '                                          sIoGbn, sDeptCd, sWardCd, Ctrl.Get_Code(Me.cboSlip), sTestCds, _
            '                                          Ctrl.Get_Code(Me.cboSpcCd).Trim, sState, Me.chkVerity.Checked, Me.chkPDCA.Checked, Me.chkIoGbn_noC.Checked)

            dt = (New SrhFn).fnGet_TatTest_Statistics_new(Me.dtpOTkDt1.Text.Replace("-", "") + Me.cboHour1.Text, Me.dtpOTkDt2.Text.Replace("-", "") + Me.cboHour2.Text, _
                                                      sIoGbn, sDeptCd, sWardCd, Ctrl.Get_Code(Me.cboSlip), sTestCds, _
                                                      Ctrl.Get_Code(Me.cboSpcCd).Trim, sState, Me.chkVerity.Checked, Me.chkPDCA.Checked, Me.chkIoGbn_noC.Checked, sRstGbn)

            With spdStatistics
                Dim sTkTm As String = ""
                Dim lTm As Long = 0, lTCnt As Long = 0
                Dim iRow_tktm As Integer = 0

                For ix1 As Integer = 0 To dt.Rows.Count - 1
                    Dim iRow As Integer = 0

                    For ix2 As Integer = 1 To .MaxRows
                        .Row = ix2
                        .Col = .GetColFromID("hour") : Dim sTkDt1 As String = .Text.Substring(0, 5) : Dim stkdt2 As String = .Text.Substring(8).Trim

                        If sTkDt1 <= dt.Rows(ix1).Item("tk_tm").ToString And dt.Rows(ix1).Item("tk_tm").ToString <= stkdt2 Then
                            iRow = ix2
                            Exit For
                        End If
                    Next

                    If sTkTm <> "" And sTkTm <> dt.Rows(ix1).Item("tk_tm").ToString.Substring(0, 2) Then
                        .Row = iRow_tktm

                        If lTCnt > 0 Then .Col = .GetColFromID("ave_o") : .Text = Format(lTm / lTCnt, "#0").ToString

                        lTm = 0 : lTCnt = 0
                    End If

                    Dim sRst_tm As String = Convert.ToInt32(Int(dt.Rows(ix1).Item("rst_tm").ToString)).ToString '수정
                    Dim sRst_tm2 As String = Convert.ToDouble(dt.Rows(ix1).Item("rst_tm").ToString).ToString()

                    lTm += Convert.ToInt32(sRst_tm) * Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)
                    lTCnt += Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)

                    lTm_tot_o += Convert.ToInt32(sRst_tm) * Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)
                    lTCnt_tot_o += Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)

                    .Row = iRow
                    .Col = .GetColFromID("cnt_o") : .Text = (Convert.ToInt32(IIf(.Text = "", "0", .Text)) + Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)).ToString

                    If Val(sRst_tm2) > Val(txtTat.Text) Then
                        .Col = .GetColFromID("over_o") : .Text = (Convert.ToInt32(IIf(.Text = "", "0", .Text)) + Convert.ToInt32(dt.Rows(ix1).Item("cnt").ToString)).ToString
                    End If

                    iRow_tktm = iRow
                    sTkTm = dt.Rows(ix1).Item("tk_tm").ToString.Substring(0, 2)
                Next

                If sTkTm <> "" Then
                    .Row = iRow_tktm

                    If lTCnt > 0 Then .Col = .GetColFromID("ave_o") : .Text = Format(lTm / lTCnt, "#0").ToString

                    lTm = 0 : lTCnt = 0
                End If

                '-- TAT 충족율(%)
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("cnt_o") : Dim lCnt As Long = Convert.ToInt32(Val(.Text))
                    .Col = .GetColFromID("over_o") : Dim iOver As Integer = Convert.ToInt32(Val(.Text))
                    If lCnt > 0 Then
                        .Col = .GetColFromID("per_o") : .Text = Format(((lCnt - iOver) / lCnt) * 100, "#0.0")
                    End If
                Next


                .MaxRows += 1

                .Row = .MaxRows
                .Col = .GetColFromID("hour") : .Text = "ToTal"

                For iCol As Integer = .GetColFromID("hour") + 1 To .MaxCols

                    If iCol <> .GetColFromID("ave") Or iCol <> .GetColFromID("ave_o") Then
                        Dim lTmp As Long = 0

                        For iRow As Integer = 1 To .MaxRows - 1
                            .Col = iCol : .Row = iRow : lTmp += Convert.ToInt32(Val(.Text))
                        Next

                        .Row = .MaxRows
                        .Col = iCol : .Text = lTmp.ToString
                    End If
                Next

                '-- 평균
                .Row = .MaxRows
                If lTCnt_tot > 0 Then .Col = .GetColFromID("ave") : .Text = Format(lTm_tot / lTCnt_tot, "#0").ToString
                If lTCnt_tot_o > 0 Then .Col = .GetColFromID("ave_o") : .Text = Format(lTm_tot_o / lTCnt_tot_o, "#0").ToString

                '-- 최소/최대
                .Row = .MaxRows
                .Col = .GetColFromID("min") : .Text = iMin.ToString
                .Col = .GetColFromID("max") : .Text = iMax.ToString

                '-- TAT 충족율(%)
                .Row = .MaxRows
                .Col = .GetColFromID("cnt") : Dim lCnt_t As Long = Convert.ToInt32(IIf(.Text = "", "0", .Text))
                .Col = .GetColFromID("over") : Dim iOver_t As Integer = Convert.ToInt32(IIf(.Text = "", "0", .Text))

                If lCnt_t > 0 Then .Col = .GetColFromID("per") : .Text = Format(((lCnt_t - iOver_t) / lCnt_t) * 100, "#0.0")

                .Col = .GetColFromID("cnt_o") : lCnt_t = Convert.ToInt32(IIf(.Text = "", "0", .Text))
                .Col = .GetColFromID("over_o") : iOver_t = Convert.ToInt32(IIf(.Text = "", "0", .Text))
                If lCnt_t > 0 Then .Col = .GetColFromID("per_o") : .Text = Format(((lCnt_t - iOver_t) / lCnt_t) * 100, "#0.0")

            End With

            spdStatistics.ReDraw = True

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbPrint_Data()

        Try
            Dim arlPrint As New ArrayList

            With spdStatistics
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("hour") : Dim sHour As String = .Text
                    .Col = .GetColFromID("cnt") : Dim sCnt As String = .Text
                    .Col = .GetColFromID("ave") : Dim sAve As String = .Text
                    .Col = .GetColFromID("min") : Dim sMin As String = .Text
                    .Col = .GetColFromID("max") : Dim sMax As String = .Text
                    .Col = .GetColFromID("over") : Dim sOver As String = .Text
                    .Col = .GetColFromID("per") : Dim sPer As String = .Text
                    .Col = .GetColFromID("cnt_o") : Dim sCnt_o As String = .Text
                    .Col = .GetColFromID("ave_o") : Dim sAve_o As String = .Text
                    .Col = .GetColFromID("per_o") : Dim sPer_o As String = .Text


                    Dim objPat As New FGT07_PRTINFO

                    With objPat
                        .sHour = sHour
                        .sCnt = sCnt
                        .sAve = sAve
                        .sMin = smin
                        .sMax = smax
                        .sOver = sOver
                        .sPer = sPer
                        .sCnt_o = sCnt_o
                        .sAve_o = sAve_o
                        .sPer_o = sPer_o
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGT07_PRINT
                'If lblTnmd2.Text = "" Then
                '    prt.msTitle = "Total"
                'Else
                '    prt.msTitle = lblTnmd2.Text
                'End If

                '<<<20160630

                Dim sPrtTnm As String = ""

                If Me.lblTnmd.Text.IndexOf(",") > 0 Then

                    Dim sTnm() As String = Me.lblTnmd.Text.Split(","c)
                    sPrtTnm = sTnm(0).ToString + " 외 " + (sTnm.Length - 1).ToString + "건"
                    prt.msTitle = sPrtTnm
                Else
                    If Me.lblTnmd.Text = "" Then
                        sPrtTnm = "Total"
                        prt.msTitle = "Total"
                    Else
                        sPrtTnm = Me.lblTnmd.Text
                        prt.msTitle = Me.lblTnmd.Text
                    End If


                End If

                prt.msTnm = "◆ 검사항목: " + sPrtTnm

                If rdoIOA.Checked Then
                    prt.msIoGbn = "◆ 환자구분: 전체"
                ElseIf rdoIOI.Checked Then
                    If rdoWardA.Checked Then
                        prt.msIoGbn = "◆ 환자구분: 입원"
                    Else
                        prt.msIoGbn = "◆ 병    동: " + Me.cboWard.Text.Split("|"c)(0)
                    End If
                Else
                    If rdoDeptA.Checked Then
                        If rdoIOC.Checked Then
                            prt.msIoGbn = "◆ 환자구분: 수탁"
                        Else
                            prt.msIoGbn = "◆ 환자구분: 외래"
                        End If
                    Else
                        prt.msIoGbn = "◆ 진 료 과: " + Me.cboDept.Text.Split("|"c)(0)
                    End If
                End If

                If rdoEmerA.Checked Then
                    prt.msEmarYN = "◆ 검사구분: 전체"
                ElseIf rdoEmerN.Checked Then
                    prt.msEmarYN = "◆ 검사구분: 일반"
                ElseIf rdoEmerY.Checked Then
                    prt.msEmarYN = "◆ 검사구분: 응급"
                End If

                If cboSlip.Text = "" Then
                    prt.msDeptGbn = "◆ 부서구분: 전체"
                Else
                    prt.msDeptGbn = "◆ 부서구분: " + cboSlip.Text.Split("]"c)(1)
                End If

                prt.msTAT_hour = "◆ 목표 TAT: " + txtTat.Text + "분"
                prt.msDate_cur = "◆ 접수일자: " + dtpTkDt1.Text + " ~ " + dtpTkDt2.Text
                prt.msDate_old = "◆ 기간대비: " + dtpOTkDt1.Text + " ~ " + dtpOTkDt2.Text

                prt.maPrtData = arlPrint

                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_dept()
        Try
            Dim dt As DataTable

            dt = OCSAPP.OcsLink.SData.fnGet_DeptList

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboDept.Items.Add(dt.Rows(i).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(i).Item("deptcd").ToString)
                Next

                If Me.cboDept.Items.Count > 0 Then Me.cboDept.SelectedIndex = 0

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDiplay_slip()

        Try
            Dim dt As DataTable

            dt = LISAPP.COMM.cdfn.fnGet_Slip_List()

            Me.cboSlip.Items.Clear()
            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboSlip.Items.Add("[" & dt.Rows(i).Item("slipcd").ToString & "]" & " " & dt.Rows(i).Item("slipnmd").ToString)
                Next
            End If

            If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDiplay_spc()

        Try
            Dim sPartCd As String = ""
            Dim sSlipCd As String = ""

            If Ctrl.Get_Code(cboSlip).Trim <> "" Then
                sPartCd = Ctrl.Get_Code(cboSlip).Trim.Substring(0, 1)
                sSlipCd = Ctrl.Get_Code(cboSlip).Trim.Substring(1, 1)
            End If

            Dim dt As DataTable

            dt = LISAPP.COMM.cdfn.fnGet_Spc_List("", sPartCd, sSlipCd, "", "", Me.txtTclsCd.Text, "")

            Me.cboSpcCd.Items.Clear()
            Me.cboSpcCd.Items.Add("[ ] 전체")
            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboSpcCd.Items.Add("[" & dt.Rows(i).Item(0).ToString & "]" & " " & dt.Rows(i).Item(1).ToString)
                Next
            End If

            If Me.cboSpcCd.Items.Count > 0 Then Me.cboSpcCd.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_ward()

        Try
            Dim dt As DataTable

            dt = OCSAPP.OcsLink.SData.fnGet_WardList

            Me.cboWard.Items.Clear()

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboWard.Items.Add(dt.Rows(i).Item("wardnm").ToString + Space(200) + "|" + dt.Rows(i).Item("wardno").ToString)
                Next

                If Me.cboWard.Items.Count > 0 Then Me.cboWard.SelectedIndex = 0
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Hour()

        Me.spdStatistics.MaxRows = 0

        With spdStatistics
            Dim sHH1 As String = cboHour1.Text
            Dim sHH2 As String = cboHour2.Text.Substring(0, 2)

            Dim iRow As Integer = 0

            For ix As Integer = Convert.ToInt16(sHH1) To Convert.ToInt16(sHH2)
                Dim sTmp As String = ix.ToString.PadLeft(2, "0"c) + ":00 ~ " + _
                                     ix.ToString.PadLeft(2, "0"c) + ":59"

                .MaxRows += 1
                .Row = .MaxRows
                .Col = .GetColFromID("hour") : .Text = sTmp
            Next
        End With

    End Sub

    Private Sub sbInitialize()

        Try
            Dim sCurSysDate As String = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")

            Me.dtpTkDt1.Value = CDate(sCurSysDate) : Me.dtpTkDt2.Value = CDate(sCurSysDate)
            Me.dtpOTkDt1.Value = DateAdd(DateInterval.Day, -1, CDate(sCurSysDate))
            Me.dtpOTkDt2.Value = DateAdd(DateInterval.Day, -1, CDate(sCurSysDate))

            Me.cboHour1.SelectedIndex = 0
            Me.cboHour2.SelectedIndex = cboHour2.Items.Count - 1

            Me.rdoIOA.Checked = True

            Me.rdoDeptA.Checked = True
            Me.pnlDept.Enabled = False
            Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            Me.rdoWardA.Checked = True
            Me.pnlWard.Enabled = False
            Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False

            Me.rdoEmerA.Checked = True

            sbDiplay_slip()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        With spdStatistics
            .ReDraw = False

            .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

            .MaxRows = .MaxRows + 1
            .InsertRows(1, 1)

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = 0 : sBuf = .Text
                .Col = i : .Row = 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = sBuf
            Next

            If .ExportToExcel("statistics.xls", "Statistics", "") Then
                Process.Start("statistics.xls")
            End If

            .DeleteRows(1, 1)
            .MaxRows -= 1

            .ReDraw = True
        End With
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdStatistics.MaxRows = 0
        Me.lblTnmd.Text = ""
        Me.lblTnmd2.Text = ""
        Me.lblTnmd.Tag = "" ' 2019-07-03 JJH 화면정리시 검사항목 tag 제거

    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
            Me.Cursor = Cursors.WaitCursor

            sbDisplay_Statistics()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub rdoIO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIOA.CheckedChanged, rdoIOO.CheckedChanged, rdoIOI.CheckedChanged, rdoIOC.CheckedChanged

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoIOA.Checked Then
                '전체
                Me.rdoDeptA.Checked = True
                Me.rdoWardA.Checked = True

                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = False

                Me.cboDept.Enabled = False
                Me.cboWard.Enabled = False

            ElseIf Me.rdoIOO.Checked Or Me.rdoIOC.Checked Then
                '외래
                Me.rdoWardA.Checked = True

                Me.pnlDept.Enabled = True
                Me.pnlWard.Enabled = False

            ElseIf Me.rdoIOI.Checked Then
                '입원
                Me.rdoDeptA.Checked = True

                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoDept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDeptA.CheckedChanged, rdoDeptS.CheckedChanged

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDeptA.Checked Then
                '전체
                Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            ElseIf Me.rdoDeptS.Checked Then
                '선택
                If Not Me.cboDept.Items.Count > 0 Then
                    sbDisplay_dept()
                End If

                If Me.cboDept.Items.Count = 0 Then Return

                Me.cboDept.SelectedIndex = 0 : Me.cboDept.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub


    Private Sub rdoWard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoWardA.CheckedChanged, rdoWardS.CheckedChanged

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoWardA.Checked Then
                '전체
                Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False

            ElseIf Me.rdoWardS.Checked Then
                '선택
                If Not Me.cboWard.Items.Count > 0 Then
                    sbDisplay_ward()
                End If

                If Me.cboWard.Items.Count = 0 Then Return

                Me.cboWard.SelectedIndex = 0 : Me.cboWard.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGT07_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGT07_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_ButtonClick(Nothing, Nothing)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        sbInitialize()

    End Sub

    Private Sub txtTclsCd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTclsCd.Click
        Me.txtTclsCd.SelectAll()
    End Sub

    Private Sub txtTclsCd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTclsCd.GotFocus

        Me.txtTclsCd.SelectionStart = 0
        Me.txtTclsCd.SelectAll()

    End Sub

    Private Sub txtTclsCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTclsCd.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        Me.txtTclsCd.Text = UCase(Me.txtTclsCd.Text)
        btnCdHelp_test_Click(Nothing, Nothing)

    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click

        'Try
        '    Dim pntCtlXY As New Point
        '    Dim pntFrmXY As New Point

        '    Dim objHelp As New CDHELP.FGCDHELP01
        '    Dim aryList As New ArrayList

        '    Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list(Ctrl.Get_Code(cboSlip.Text), "", "", "", Ctrl.Get_Code(cboSpcCd))
        '    Dim dr As DataRow()

        '    dr = dt.Select("tcdgbn IN ('S', 'P', 'C')", "")
        '    dt = Fn.ChangeToDataTable(dr)

        '    objHelp.FormText = "검사정보"

        '    objHelp.TableNm = ""
        '    objHelp.Where = ""

        '    objHelp.GroupBy = ""
        '    objHelp.OrderBy = ""
        '    objHelp.MaxRows = 15
        '    objHelp.Distinct = True
        '    objHelp.OnRowReturnYN = True

        '    objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
        '    objHelp.AddField("testcd", "코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

        '    pntFrmXY = Fn.CtrlLocationXY(Me)
        '    pntCtlXY = Fn.CtrlLocationXY(btnCdHelp_test)

        '    aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_test.Height + 80, dt)

        '    If aryList.Count > 0 Then
        '        Me.txtTclsCd.Text = aryList.Item(0).ToString.Split("|"c)(1)
        '        Me.lblTnmd.Text = aryList.Item(0).ToString.Split("|"c)(0)
        '    Else
        '        Me.txtTclsCd.Text = "" : lblTnmd.Text = ""
        '    End If

        '    sbDiplay_spc()

        'Catch ex As Exception
        '    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        'End Try

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)
            Dim sTestCds As String = ""

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList



            '   If txtSelTest.Tag Is Nothing Then txtSelTest.Tag = ""
            '   If txtSelTest.Tag.ToString <> "" Then sTestCds = "'" + txtSelTest.Tag.ToString.Replace(",", "','") + "'"

            '  Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list(Ctrl.Get_Code(Me.cboSlip), "")
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list(Ctrl.Get_Code(cboSlip.Text), "", "", "", Ctrl.Get_Code(cboSpcCd))
            Dim a_dr As DataRow() = dt.Select("tcdgbn IN ('S', 'P', 'C')", "")

            dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"
            objHelp.TableNm = ""
            objHelp.Where = ""
            objHelp.GroupBy = ""
            objHelp.OrderBy = ""
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = sTestCds

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("tcdgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sortl", "순서", , , , True)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnCdHelp_test)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If aryList.Count > 0 Then
                Me.lblTnmd.Text = "" : Me.lblTnmd.Tag = ""

                For ix As Integer = 0 To aryList.Count - 1
                    If ix > 0 Then
                        Me.lblTnmd.Tag = Me.lblTnmd.Tag.ToString + ","
                        Me.lblTnmd.Text += ","
                    End If

                    Me.lblTnmd.Tag = Me.lblTnmd.Tag.ToString + aryList.Item(ix).ToString.Split("|"c)(1)
                    Me.lblTnmd.Text += aryList.Item(ix).ToString.Split("|"c)(0)
                Next
            End If

            m_tooltip.RemoveAll()
            DP_Common.setToolTip(Me.CreateGraphics, Me.txtTclsCd, Me.txtTclsCd.Text, m_tooltip)

            sbDiplay_spc()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub cboTSect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged

        Call sbDiplay_spc()

    End Sub

    Private Sub FGT07_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnPrint_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        sbPrint_Data()

    End Sub

    Private Sub txtTclsCd_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTclsCd.LostFocus
        txtTclsCd.Text = UCase(txtTclsCd.Text)
    End Sub

End Class

Public Class FGT07_PRTINFO
    Public sHour As String = ""
    Public sCnt As String = ""
    Public sAve As String = ""
    Public sMin As String = ""
    Public sMax As String = ""
    Public sOver As String = ""
    Public sPer As String = ""
    Public sCnt_o As String = ""
    Public sAve_o As String = ""
    Public sPer_o As String = ""
End Class


Public Class FGT07_PRINT
    Private Const msFile As String = "File : FGT07.vb, Class : T01" & vbTab

    Private miPageNo As Integer = 0
    Private miRow_Cur As Integer = 0

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 10

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msTitle As String
    Public msDeptGbn As String
    Public msIoGbn As String
    Public msEmarYN As String
    Public msTAT_hour As String
    Public msSpcNm As String
    Public msDate_cur As String
    Public msDate_old As String
    Public msTnm As String

    Public maPrtData As ArrayList
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd hh:mm")
    Public msTitle_sub_right_1 As String = ""

    Public Sub sbPrint_Preview()
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Sub

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = True
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle


                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport
                prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miRow_Cur = 1
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngTop As Single = 0, sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 10
        msgLeft = 5
        msgTop = 30

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = CSng(fnt_Body.GetHeight(e.Graphics) * 1.3)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = 0 To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngTop = fnPrtTitle(e)
                sngPosY = sngTop
            End If

            If intIdx = maPrtData.Count - 1 Then
                fnt_Body = New Font("굴림체", 10, FontStyle.Bold)
                sngPrtH += sngPrtH / 2
            End If

            If intIdx <> 0 Then sngPosY += sngPrtH

            '-- 시간대
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sHour, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 검사건수
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sCnt, fnt_Body, Drawing.Brushes.Black, rect, sf_r)
            '-- 평균
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sAve, fnt_Body, Drawing.Brushes.Black, rect, sf_r)
            '-- 최소
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sMin, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 최대
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sMax, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 초과
            rect = New Drawing.RectangleF(msgPosX(5), sngPosY, msgPosX(6) - msgPosX(5), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sOver, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- tat 충족율
            rect = New Drawing.RectangleF(msgPosX(6), sngPosY, msgPosX(7) - msgPosX(6), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sPer, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 기간 건수
            rect = New Drawing.RectangleF(msgPosX(7), sngPosY, msgPosX(8) - msgPosX(7), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sCnt_o, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 기간 평균
            rect = New Drawing.RectangleF(msgPosX(8), sngPosY, msgPosX(9) - msgPosX(8), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sAve_o, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 기간 충족율
            rect = New Drawing.RectangleF(msgPosX(9), sngPosY, msgPosX(10) - msgPosX(9), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT07_PRTINFO).sPer_o, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            miRow_Cur += 1

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH, msgWidth, sngPosY + sngPrtH)

            If msgHeight - sngPrtH * 5 < sngPosY + sngPrtH Then Exit For

        Next

        '-- 세로
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngTop, msgPosX(ix), sngPosY + sngPrtH)
        Next


        miPageNo += 1



        If miRow_Cur < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0.0

        Dim sngPosX(0 To 10) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 120
        For ix As Integer = 2 To 10
            sngPosX(ix) = sngPosX(ix - 1) + 110
        Next

        msgWidth = sngPosX(10)
        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = CSng(fnt_Title.GetHeight(e.Graphics) * (3 / 2))

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString("진단검사의학과 TAT관리(" + msTitle + ")", fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = CSng(fnt_Head.GetHeight(e.Graphics) * 1.5)

        '-- 출력정보
        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length, sngPosY, msgWidth - 8 * msTitle_sub_right_1.Length, sngPrt), sf_l)
        End If

        sngPosY -= sngPrt  '부서구분 위로 올림

        '-- 부서구분
        e.Graphics.DrawString(msDeptGbn, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 검사항목(20160628 추가 )
        e.Graphics.DrawString(msTnm, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        sngPosY += sngPrt

        '-- 환자구분
        e.Graphics.DrawString(msIoGbn, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 응급여부
        e.Graphics.DrawString(msEmarYN, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 목표 TAT
        e.Graphics.DrawString(msTAT_hour, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        sngPosY += sngPrt

        '-- 접수일자
        e.Graphics.DrawString(msDate_cur, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 기간대비
        e.Graphics.DrawString(msDate_old, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sngPosY, msgWidth - 8 * (msTitle_Time.Length + 6), sngPrt), sf_l)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("시 간 대", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt * 2), sf_c)
        e.Graphics.DrawString("검사건수", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY, sngPosX(2) - sngPosX(1), sngPrt * 2), sf_c)
        e.Graphics.DrawString("평균소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, sngPosX(3) - sngPosX(2), sngPrt * 2), sf_c)
        e.Graphics.DrawString("최소소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY, sngPosX(4) - sngPosX(3), sngPrt * 2), sf_c)
        e.Graphics.DrawString("최대소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, sngPosX(5) - sngPosX(4), sngPrt * 2), sf_c)
        e.Graphics.DrawString("초과건수", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY, sngPosX(6) - sngPosX(5), sngPrt * 2), sf_c)
        e.Graphics.DrawString("TAT충족율(%)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(6), sngPosY, sngPosX(7) - sngPosX(6), sngPrt * 2), sf_c)
        e.Graphics.DrawString("기간대비 검사건수", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(7), sngPosY, sngPosX(8) - sngPosX(7), sngPrt * 2), sf_c)
        e.Graphics.DrawString("기간대비평균소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(8), sngPosY, sngPosX(9) - sngPosX(8), sngPrt * 2), sf_c)
        e.Graphics.DrawString("기간대비TAT충족율(%)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(9), sngPosY, sngPosX(10) - sngPosX(9), sngPrt * 2), sf_c)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 2, msgWidth, sngPosY + sngPrt * 2)

        For ix As Integer = 0 To sngPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, sngPosX(ix), sngPosY - sngPrt / 2, sngPosX(ix), sngPosY + sngPrt * 2)
        Next

        msgPosX = sngPosX

        Return sngPosY + sngPrt * 2

    End Function

End Class