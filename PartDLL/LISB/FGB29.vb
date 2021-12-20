Imports System.Windows.Forms
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports CDHELP.FGCDHELPFN

Public Class FGB29
    Private mbQuery As Boolean = False
    Private mbEscape As Boolean = False

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If mbQuery = False Then Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            If mbQuery = False Then sbFormClear(0)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub
    Private Sub sbFormClear(ByVal riPhase As Integer)
        Try
            If InStr("0", riPhase.ToString, CompareMethod.Text) > 0 Then
                Me.spdList.MaxRows = 0
                Me.txtPatnm.Text = ""
                Me.txtRegNo.Text = ""
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGB29_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F4 Then ' 화면정리
            btnClear_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then ' 종료
            If mbQuery = False Then Me.Close()
        End If
    End Sub

    Private Sub FGB29_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        sbDisp_Init()
    End Sub

    Public Sub sbDisp_Init()
        Me.spdList.MaxRows = 0
        Me.dtpDate0.CustomFormat = "yyyy-MM-dd"
        Me.dtpDate1.CustomFormat = "yyyy-MM-dd"

        Me.dtpDate0.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
        Me.dtpDate1.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")
    End Sub

    Private Sub btnQuery_Click(sender As Object, e As EventArgs) Handles btnQuery.Click
        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Me.spdList.MaxRows = 0

            If mbQuery = False Then sbQuery()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Sub

    Private Sub sbQuery()

        'Dim dt2 As New DataTable
        'Dim strKey As String = ""
        'Dim strOldKey As String = ""

        'Dim intGrpNo As Integer = 0
        'Dim spdBackColor As New Drawing.Color

        'Dim sngTAT1_MI As Single = 0
        'Dim sngTAT2_MI As Single = 0
        'Dim sngPRPTMI As Single = 0
        'Dim sngFRPTMI As Single = 0
        'Dim sngTAT1_MI_exp_holi As Single = 0

        'Dim sTestCds As String = ""

        'Try
        '    Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '    DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 데이타 조회중... -> 데이타량에 따라 다소 시간이 걸리므로 잠시만 기다려 주십시오.")

        '    Dim sEmerYN As String = ""

        '    If chkEmer.Checked And chkNotEmer.Checked Then
        '        sEmerYN = ""
        '    ElseIf chkEmer.Checked Then
        '        sEmerYN = "Y"
        '    ElseIf chkNotEmer.Checked Then
        '        sEmerYN = "N"
        '    End If

        '    sTestCds = Ctrl.Get_Code_Tag(Me.txtSelTest)

        '    If sTestCds.Length > 0 Then
        '        sTestCds = "'" + sTestCds.Replace(",", "','") + "'"
        '    End If

        '    Dim dt_Cmt As New DataTable
        '    Dim objCollTkCd As New LISAPP.APP_F_COLLTKCD

        '    dt_Cmt = objCollTkCd.fnGet_CollTK_Cancel_ContInfo("C")
        '    Dim dt As DataTable = fnGet_Tat_List(sTestCds, Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""),
        '                                         IIf(Me.rdoBaseTst.Checked, "", "ORDER").ToString(),
        '                                         Me.chkOVT.Checked, Ctrl.Get_Code(Me.cboSlip), sEmerYN, Me.txtRegNo.Text, Me.chkTATCont.Checked,
        '                                         Me.chkIncludeChild.Checked)

        '    Dim sSelect As String = ""

        '    If Me.txtPatnm.Text <> "" Then sSelect = "patnm LIKE '" + Me.txtPatnm.Text.Trim() + "%'"

        '    If Me.txtRegNo.Text <> "" Then sSelect += IIf(sSelect = "", "", " AND ").ToString + " regno = '" + Me.txtRegNo.Text + "'"

        '    If Me.rdoIogbnI.Checked Then
        '        sSelect += IIf(sSelect = "", "", " AND ").ToString + "iogbn IN ('I', 'D', 'E')"
        '        If Me.txtDeptWard.Text <> "" Then sSelect += IIf(sSelect = "", "", " AND ").ToString + "wardno IN ('" + Me.txtDeptWard.Tag.ToString.Replace(",", "','").ToString + "')"
        '    ElseIf Me.rdoIogbnO.Checked Then
        '        sSelect += IIf(sSelect = "", "", " AND ").ToString + "iogbn NOT IN ('I', 'D', 'E')"
        '        If Me.txtDeptWard.Text <> "" Then sSelect += IIf(sSelect = "", "", " AND ").ToString + "deptcd IN ('" + Me.txtDeptWard.Tag.ToString.Replace(",", "','").ToString + "')"
        '    End If

        '    If Me.chkTATCont.Checked Then
        '        sSelect += IIf(sSelect = "", "", " AND ").ToString + "TRIM(cmtcont) <> '[]'"
        '    End If
        '    '< add freety 2007/01/23 : 정렬기준 접수일시와 검체번호로 분리
        '    Dim sSortBy As String = ""
        '    Dim a_dr() As DataRow

        '    If Me.rdoBaseTkDt.Checked Then
        '        sSortBy = "tkdt, bcno, sort_slip, sort_test, testcd"
        '    Else
        '        sSortBy = "sort_slip, slipcd, tkdt, sort_test, testcd"
        '    End If

        '    a_dr = dt.Select(sSelect, sSortBy)

        '    dt = Fn.ChangeToDataTable(a_dr)
        '    '>

        '    If dt.Rows.Count > 0 Then
        '        mbQuery = True
        '        pnlMainBtn.Enabled = False

        '        Dim bldFlag As Boolean = False

        '        With spdList
        '            .MaxRows = 0
        '            For ix As Integer = 0 To dt.Rows.Count - 1
        '                Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        '                Application.DoEvents()

        '                ' 중간 취소
        '                If mbEscape = True Then Exit For
        '                DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 리스트 표시중... [" & (ix + 1).ToString & "/" & dt.Rows.Count.ToString & "] ->  표시 취소는 Esc Key를 눌러 주십시오.")

        '                .MaxRows += 1 : .Row = .MaxRows
        '                strKey = dt.Rows(ix).Item("bcno").ToString.Trim
        '                If strKey <> strOldKey Then
        '                    If strOldKey <> "" Then Fn.DrawBorderLineTop(spdList, .MaxRows)
        '                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString.Trim
        '                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString.Trim
        '                    .Col = .GetColFromID("sexage") : .Text = dt.Rows(ix).Item("sa").ToString.Trim
        '                    .Col = .GetColFromID("deptnm") : .Text = dt.Rows(ix).Item("deptcd").ToString.Trim
        '                    .Col = .GetColFromID("doctornm") : .Text = dt.Rows(ix).Item("doctornm").ToString.Trim
        '                    .Col = .GetColFromID("wardroom") : .Text = dt.Rows(ix).Item("ws").ToString.Trim
        '                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(ix).Item("spcnmd").ToString.Trim
        '                    .Col = .GetColFromID("bcno") : .Text = Fn.BCNO_View(strKey, True)


        '                    intGrpNo += 1
        '                    If intGrpNo Mod 2 = 1 Then
        '                        spdBackColor = System.Drawing.Color.White
        '                    Else
        '                        spdBackColor = System.Drawing.Color.FromArgb(255, 251, 244)
        '                    End If

        '                    strOldKey = strKey
        '                End If

        '                '배경색 설정
        '                .Row = ix + 1 : .Row2 = ix + 1
        '                .Col = 1 : .Col2 = .MaxCols
        '                .BlockMode = True
        '                .BackColor = spdBackColor
        '                .BlockMode = False

        '                .Row = .MaxRows
        '                .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim

        '                .Col = .GetColFromID("statgbn")
        '                If dt.Rows(ix).Item("statgbn").ToString.Trim <> "" Then  '기존 If dt.Rows(ix).Item("statgbn").ToString.Trim <> "Y" Then
        '                    .ForeColor = System.Drawing.Color.Red : .FontBold = True
        '                    .Text = "Y"
        '                    .set_RowHeight(.Row, 12.27)
        '                Else
        '                    .Text = ""
        '                End If

        '                .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString.Trim
        '                .ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
        '                If intGrpNo Mod 2 = 1 Then
        '                    .BackColor = System.Drawing.Color.FromArgb(244, 244, 244)
        '                Else
        '                    .BackColor = System.Drawing.Color.FromArgb(238, 238, 238)
        '                End If

        '                .Col = .GetColFromID("t1") : .Text = dt.Rows(ix).Item("t1").ToString.Trim
        '                .Col = .GetColFromID("t2") : .Text = dt.Rows(ix).Item("t2").ToString.Trim

        '                .ForeColor = System.Drawing.Color.FromArgb(0, 64, 0)
        '                If intGrpNo Mod 2 = 1 Then
        '                    .BackColor = System.Drawing.Color.FromArgb(234, 255, 234)
        '                Else
        '                    .BackColor = System.Drawing.Color.FromArgb(234, 249, 228)
        '                End If

        '                .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString.Trim
        '                .ForeColor = System.Drawing.Color.FromArgb(0, 0, 94)
        '                If intGrpNo Mod 2 = 1 Then
        '                    .BackColor = System.Drawing.Color.FromArgb(234, 234, 255)
        '                Else
        '                    .BackColor = System.Drawing.Color.FromArgb(234, 228, 249)
        '                End If

        '                sngTAT1_MI = CSng(IIf(dt.Rows(ix).Item("tat1_mi").ToString.Trim = "", 0, dt.Rows(ix).Item("tat1_mi").ToString.Trim))
        '                sngTAT2_MI = CSng(IIf(dt.Rows(ix).Item("tat2_mi").ToString.Trim = "", 0, dt.Rows(ix).Item("tat2_mi").ToString.Trim))
        '                sngPRPTMI = CSng(IIf(dt.Rows(ix).Item("prptmi").ToString.Trim = "", 0, dt.Rows(ix).Item("prptmi").ToString.Trim))
        '                sngFRPTMI = CSng(IIf(dt.Rows(ix).Item("frptmi").ToString.Trim = "", 0, dt.Rows(ix).Item("frptmi").ToString.Trim))

        '                .Col = .GetColFromID("tat1") : .Text = dt.Rows(ix).Item("tat1").ToString.Trim
        '                '<<<20170511 TAT시간에 걸린것 이 소수점이 있을경우 오버타임으로 계산되는것 막기 위해 소수점은버림 추가 
        '                If Math.Truncate(sngTAT1_MI) > sngPRPTMI And sngPRPTMI > 0 Then
        '                    .ForeColor = System.Drawing.Color.Red
        '                    .BackColor = System.Drawing.Color.FromArgb(255, 202, 202)

        '                    .Col = .GetColFromID("ovt1") : .Text = (Fix(sngTAT1_MI - sngPRPTMI)).ToString
        '                End If

        '                .Col = .GetColFromID("tat3") : .Text = dt.Rows(ix).Item("tat3").ToString.Trim
        '                '<<<20170511 TAT시간에 걸린것 이 소수점이 있을경우 오버타임으로 계산되는것 막기 위해 소수점은버림 추가 
        '                If Math.Truncate(sngTAT2_MI) > sngFRPTMI And sngFRPTMI > 0 Then
        '                    .ForeColor = System.Drawing.Color.Red
        '                    .BackColor = System.Drawing.Color.FromArgb(255, 202, 202)

        '                    .Col = .GetColFromID("ovt2") : .Text = (Fix(sngTAT2_MI - sngFRPTMI)).ToString
        '                End If

        '                .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.Trim
        '                .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString.Trim
        '                .Col = .GetColFromID("rstnm") : .Text = dt.Rows(ix).Item("rstnm").ToString.Trim

        '                .Col = .GetColFromID("tat1_mi") : .Text = sngTAT1_MI.ToString
        '                .Col = .GetColFromID("tat2_mi") : .Text = sngTAT2_MI.ToString
        '                .Col = .GetColFromID("prptmi") : .Text = sngPRPTMI.ToString
        '                .Col = .GetColFromID("frptmi") : .Text = sngFRPTMI.ToString
        '                .Col = .GetColFromID("partcd") : .Text = dt.Rows(ix).Item("slipcd").ToString.Trim

        '            Next
        '        End With

        '        Debug.WriteLine(dt2.Rows.Count)

        '    Else
        '        Me.spdList.MaxRows = 0
        '        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 데이타가 없습니다.")
        '    End If

        'Catch ex As Exception
        '    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        'Finally
        '    DS_StatusBar.setTextStatusBar("")
        '    Cursor.Current = System.Windows.Forms.Cursors.Default

        '    If mbEscape = True Then
        '        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "리스트 표시를 중단 했습니다.")
        '    End If

        '    mbQuery = False
        '    mbEscape = False
        '    pnlMainBtn.Enabled = True
        'End Try

    End Sub

End Class