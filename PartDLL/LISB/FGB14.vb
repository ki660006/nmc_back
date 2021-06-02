' 수혈의뢰현황 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB14
    Private mobjDAF As New LISAPP.APP_F_COMCD

    Private Sub FGB14_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB14_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB14_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        Me.txtRegno.MaxLength = PRG_CONST.Len_RegNo()

        dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
        dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdSearchList)
        DS_SpreadDesige.sbInti(spdDetail)

        spdSearchList.MaxRows = 0
        spdDetail.MaxRows = 0

        sb_SetComboDt()

        'cboComCd.Text = "[ALL] 전체"
        'cboDept.Text = "[ALL] 전체"
        'cboWard.Text = "[ALL] 전체"

        lblDept.Visible = False
        lblWard.Visible = False
        cboDept.Visible = False
        cboWard.Visible = False
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sFn As String = "CButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CButton1.Click"

        Dim dt As New DataTable
        Dim ls_Comcd As String
        Dim ls_TnsGbn As String = "ALL"
        Dim ls_regno As String
        Dim ls_state As String = ""
        Dim lal_state As New ArrayList
        Dim ls_dept As String = ""
        Dim ls_ward As String = ""
        Dim ls_iogbn As String = ""

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            spdSearchList.MaxRows = 0
            spdDetail.MaxRows = 0

            '20210108 jhs 묶음으로 성분제재 조회 될 수 있또록 로직 추가
            If chkBldBranchSrh.Checked Then
                Dim ComBranchCd As String = Ctrl.Get_Code(cboBranchComcd)
                If ComBranchCd = "ALL" Then
                    ls_Comcd = ComBranchCd
                Else
                    ls_Comcd = CGDA_BT.fn_get_BranchComCd_List("", ComBranchCd)
                End If
            Else
                ls_Comcd = Ctrl.Get_Code(cboComCd)
            End If
            '-----------------------------------------------------

            ls_dept = Ctrl.Get_Code(cboDept)
            ls_ward = Ctrl.Get_Code(cboWard)

            If rdoAll.Checked = True Then ls_TnsGbn = "ALL"
            If rdoPre.Checked = True Then ls_TnsGbn = "1"c
            If rdoTns.Checked = True Then ls_TnsGbn = "2"c
            If rdoCross.Checked = True Then ls_TnsGbn = "3"c

            '20210419 jhs iogbn 정리 로직 추가(응급 추가, des(낮병동) 으로 구분 해야함
            If rdoI.Checked Then ls_iogbn = "I"
            If rdoO.Checked Then ls_iogbn = "O"
            If rdoE.Checked Then ls_iogbn = "E"
            '------------------------------------------------

            ls_regno = txtRegno.Text



            If chkJub.Checked = True And chkBef.Checked = True And chkOut.Checked = True And chkRtn.Checked = True And chkAbn.Checked = True And chkCan.Checked = True Then
                ls_state = ""
            Else
                If chkJub.Checked = True Then lal_state.Add("'1'")
                If chkBef.Checked = True Then lal_state.Add("'3'")
                If chkOut.Checked = True Then lal_state.Add("'4'")
                If chkRtn.Checked = True Then lal_state.Add("'5'")
                If chkAbn.Checked = True Then lal_state.Add("'6'")
                If chkCan.Checked = True Then lal_state.Add("'0'")

                For i As Integer = 0 To lal_state.Count - 1
                    If i = 0 Then
                        ls_state = lal_state(i).ToString
                    Else
                        ls_state = ls_state + ", " + lal_state(i).ToString
                    End If
                Next
            End If

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            '20210106 jhs 묶음으로 성분제재 출력
            If chkBldBranchSrh.Checked Then
                dt = CGDA_BT.fn_TnsSearchList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), ls_Comcd, ls_TnsGbn, _
                                      ls_regno, ls_state, ls_dept, ls_ward, ls_iogbn, _
                                      Me.txtRstDay.Text, Me.txtRst_Hb.Text, Me.txtRst_plt1.Text, Me.txtRst_plt2.Text, chkBldBranchSrh.Checked)
            Else
                dt = CGDA_BT.fn_TnsSearchList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), ls_Comcd, ls_TnsGbn, _
                                      ls_regno, ls_state, ls_dept, ls_ward, ls_iogbn, _
                                      Me.txtRstDay.Text, Me.txtRst_Hb.Text, Me.txtRst_plt1.Text, Me.txtRst_plt2.Text)
            End If
            ' 조회


            Dim sSql As String = ""
            If Me.txtRstDay.Text <> "" And Me.txtRst_plt1.Text.Length + Me.txtRst_plt2.Text.Length > 0 Then

                If Me.txtRst_plt1.Text.Length > 0 And Me.txtRst_plt2.Text.Length > 0 Then
                    sSql = "orgrst >= " + Me.txtRst_plt1.Text + " AND orgrst <= " + Me.txtRst_plt2.Text
                Else
                    sSql = "orgrst = '" + Me.txtRst_plt1.Text + Me.txtRst_plt2.Text + "'"
                End If

            End If

            If Me.cboAboRh.Text <> "" Then
                sSql += IIf(sSql = "", "", " AND ").ToString + "aborh = '" + Me.cboAboRh.Text + "'"
            End If

            If sSql <> "" Then
                Dim a_dr As DataRow() = dt.Select(sSql, "vtnsjubsuno DESC, comcd_out, regno, state")
                dt = Fn.ChangeToDataTable(a_dr)

            End If

            sb_DisplayDataList(dt)

            If chkBldBranchSrh.Checked Then
                dt = CGDA_BT.fn_outComcdList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), ls_Comcd, ls_dept, ls_ward, ls_iogbn, _
                                             Me.txtRstDay.Text, Me.txtRst_Hb.Text, Me.txtRst_plt1.Text, Me.txtRst_plt2.Text, Me.cboAboRh.Text, chkBldBranchSrh.Checked)
            Else
                dt = CGDA_BT.fn_outComcdList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), ls_Comcd, ls_dept, ls_ward, ls_iogbn, _
                                             Me.txtRstDay.Text, Me.txtRst_Hb.Text, Me.txtRst_plt1.Text, Me.txtRst_plt2.Text, Me.cboAboRh.Text)
            End If


            sb_DisplayComcdList(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try
    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdSearchList.MaxRows = 0
        Me.spdDetail.MaxRows = 0
    End Sub

    Private Sub sb_DisplayDataList(ByVal rDt As DataTable)

        Try
            With Me.spdSearchList
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then
                    sb_SetStBarSearchCnt(0)
                    Return
                End If


                .ReDraw = False

                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("regno") : .Text = rDt.Rows(i).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = rDt.Rows(i).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = rDt.Rows(i).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("orddt") : .Text = rDt.Rows(i).Item("orddt").ToString.Trim
                    .Col = .GetColFromID("docnm") : .Text = rDt.Rows(i).Item("docnm").ToString.Trim
                    .Col = .GetColFromID("deptnm") : .Text = rDt.Rows(i).Item("deptnm").ToString.Trim
                    .Col = .GetColFromID("wdsr") : .Text = rDt.Rows(i).Item("wdsr").ToString.Trim
                    .Col = .GetColFromID("comgbn") : .Text = rDt.Rows(i).Item("comgbn").ToString.Trim
                    .Col = .GetColFromID("vtnsjubsuno") : .Text = rDt.Rows(i).Item("vtnsjubsuno").ToString.Trim
                    .Col = .GetColFromID("comnmd") : .Text = rDt.Rows(i).Item("comnmd").ToString.Trim
                    .Col = .GetColFromID("aborh") : .Text = rDt.Rows(i).Item("aborh").ToString.Trim
                    .Col = .GetColFromID("reqqnt") : .Text = rDt.Rows(i).Item("reqqnt").ToString.Trim
                    .Col = .GetColFromID("befoutqnt") : .Text = rDt.Rows(i).Item("befoutqnt").ToString.Trim
                    .Col = .GetColFromID("outqnt") : .Text = rDt.Rows(i).Item("outqnt").ToString.Trim
                    .Col = .GetColFromID("rtnqnt") : .Text = rDt.Rows(i).Item("rtnqnt").ToString.Trim
                    .Col = .GetColFromID("abnqnt") : .Text = rDt.Rows(i).Item("abnqnt").ToString.Trim
                    .Col = .GetColFromID("cancelqnt") : .Text = rDt.Rows(i).Item("cancelqnt").ToString.Trim
                    .Col = .GetColFromID("aborhBld") : .Text = rDt.Rows(i).Item("aborhBld").ToString.Trim
                    .Col = .GetColFromID("vbldno") : .Text = rDt.Rows(i).Item("vbldno").ToString.Trim
                    .Col = .GetColFromID("state") : .Text = rDt.Rows(i).Item("state").ToString.Trim
                    .Col = .GetColFromID("jubsudt") : .Text = rDt.Rows(i).Item("jubsudt").ToString.Trim
                    .Col = .GetColFromID("rst1") : .Text = rDt.Rows(i).Item("rst1").ToString.Trim
                    .Col = .GetColFromID("rst2") : .Text = rDt.Rows(i).Item("rst2").ToString.Trim
                    .Col = .GetColFromID("rst3") : .Text = rDt.Rows(i).Item("rst3").ToString.Trim
                    .Col = .GetColFromID("rst4") : .Text = rDt.Rows(i).Item("rst4").ToString.Trim
                    .Col = .GetColFromID("testdt") : .Text = rDt.Rows(i).Item("testdt").ToString.Trim
                    .Col = .GetColFromID("testid") : .Text = rDt.Rows(i).Item("testid").ToString.Trim
                    .Col = .GetColFromID("befoutdt") : .Text = rDt.Rows(i).Item("befoutdt").ToString.Trim
                    .Col = .GetColFromID("befoutid") : .Text = rDt.Rows(i).Item("befoutid").ToString.Trim
                    .Col = .GetColFromID("outdt") : .Text = rDt.Rows(i).Item("outdt").ToString.Trim
                    .Col = .GetColFromID("outid") : .Text = rDt.Rows(i).Item("outid").ToString.Trim
                    .Col = .GetColFromID("recnm") : .Text = rDt.Rows(i).Item("recnm").ToString.Trim
                    .Col = .GetColFromID("rtndt") : .Text = rDt.Rows(i).Item("rtndt").ToString.Trim
                    .Col = .GetColFromID("rtnid") : .Text = rDt.Rows(i).Item("rtnid").ToString.Trim
                Next

                sb_SetStBarSearchCnt(rDt.Rows.Count)

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdSearchList.ReDraw = True
        End Try

    End Sub

    Private Sub sb_DisplayComcdList(ByVal rDt As DataTable)
        Try
            With Me.spdDetail
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then Return

                .ReDraw = False

                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("comnmd") : .Text = rDt.Rows(i).Item("comnmd").ToString.Trim
                    .Col = .GetColFromID("a1") : .Text = rDt.Rows(i).Item("a1").ToString.Trim
                    .Col = .GetColFromID("a2") : .Text = rDt.Rows(i).Item("a2").ToString.Trim
                    .Col = .GetColFromID("b1") : .Text = rDt.Rows(i).Item("b1").ToString.Trim
                    .Col = .GetColFromID("b2") : .Text = rDt.Rows(i).Item("b2").ToString.Trim
                    .Col = .GetColFromID("o1") : .Text = rDt.Rows(i).Item("o1").ToString.Trim
                    .Col = .GetColFromID("o2") : .Text = rDt.Rows(i).Item("o2").ToString.Trim
                    .Col = .GetColFromID("ab1") : .Text = rDt.Rows(i).Item("ab1").ToString.Trim
                    .Col = .GetColFromID("ab2") : .Text = rDt.Rows(i).Item("ab2").ToString.Trim
                    .Col = .GetColFromID("allcnt") : .Text = rDt.Rows(i).Item("allcnt").ToString.Trim
                Next

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdDetail.ReDraw = True
        End Try
    End Sub

    Public Sub sb_SetComboDt()
        ' 콤보 데이터 생성
        Try
            Dim dt As DataTable = Nothing
            ' 성분제제
            dt = mobjDAF.GetComCdInfo("")

            Me.cboComCd.Items.Clear()
            Me.cboComCd.Items.Add("[ALL] 전체")
            If dt.Rows.Count > 0 Then
                With Me.cboComCd
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add(dt.Rows(i).Item("COMNMD"))
                    Next
                End With
            End If
            Me.cboComCd.SelectedIndex = 0

            '20210105 jhs 성분제재 묶음으로 조회ui단 정리
            dt = mobjDAF.GetBranchComCdInfo("")

            Me.cboBranchComcd.Items.Clear()
            Me.cboBranchComcd.Items.Add("[ALL] 전체")
            If dt.Rows.Count > 0 Then
                With Me.cboBranchComcd
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add(dt.Rows(i).Item("COMNMD"))
                    Next
                End With
            End If
            Me.cboBranchComcd.SelectedIndex = 0
            '---------------------------------------------------

            ' 진료과
            dt = OCSAPP.OcsLink.SData.fnGet_DeptList() ' CGDA_BT .fn_GetDeptList()

            Me.cboDept.Items.Clear()
            Me.cboDept.Items.Add("[ALL] 전체")
            If dt.Rows.Count > 0 Then
                With cboDept
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add("[" + dt.Rows(i).Item("deptcd").ToString.Trim + "] " + dt.Rows(i).Item("deptnm").ToString.Trim)
                    Next
                End With
            End If
            Me.cboDept.SelectedIndex = 0

            ' 병동
            dt = OCSAPP.OcsLink.SData.fnGet_WardList()

            Me.cboWard.Items.Clear()
            Me.cboWard.Items.Add("[ALL] 전체")
            If dt.Rows.Count > 0 Then
                With Me.cboWard
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add("[" + dt.Rows(i).Item("wardno").ToString.Trim + "] " + dt.Rows(i).Item("wardnm").ToString.Trim)
                    Next
                End With
            End If
            Me.cboWard.SelectedIndex = 0

            '20210105 jhs 성분제재 묶음으로 조회ui단 정리
            If chkBldBranchSrh.Checked Then
                cboBranchComcd.Visible = True
                lblBranchComcd.Visible = True
                lblComcd.Visible = False
                cboComCd.Visible = False
            Else
                cboBranchComcd.Visible = False
                lblBranchComcd.Visible = False
                lblComcd.Visible = True
                cboComCd.Visible = True
            End If
            '----------------------------------
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnPatPop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatPop.Click
        ' 환자 팝업 호출
        Dim objHelp As New CDHELP.FGCDHELP99
        Dim lal_Header As New ArrayList
        Dim lal_Arg As New ArrayList
        Dim li_RtnCnt As Integer = 2
        Dim lal_Rtn As New ArrayList
        Dim ls_Regno As String = txtRegno.Text

        Try
            lal_Header.Add("환자번호")
            lal_Header.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LIS)
            lal_Arg.Add(" "c)
            lal_Arg.Add(" "c)


            lal_Rtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", lal_Arg, lal_Header, li_RtnCnt, "")

            If lal_Rtn.Count > 0 Then
                txtRegno.Text = lal_Rtn(0).ToString
                txtPatNm.Text = lal_Rtn(1).ToString

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub txtRegno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegno.Click
        Me.txtRegno.SelectAll()
    End Sub

    Private Sub txtRegno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtRegno.Text = "" Then
            Me.txtPatNm.Text = ""
            Return
        End If

        Try
            Dim objHelp As New CDHELP.FGCDHELP99
            Dim ls_Regno As String
            Dim la_getValue As New ArrayList
            Dim lal_Header As New ArrayList
            Dim lal_Arg As New ArrayList
            Dim li_RtnCnt As Integer = 2
            Dim lal_Rtn As New ArrayList

            ' 등록번호 입력시 이벤트
            ls_Regno = txtRegno.Text

            If IsNumeric(ls_Regno) Then
                If ls_Regno.Length() < PRG_CONST.Len_RegNo Then
                    ls_Regno = ls_Regno.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                End If
            Else
                If ls_Regno.Length() < PRG_CONST.Len_RegNo Then
                    ls_Regno = ls_Regno.Substring(0, 1) + ls_Regno.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If
            End If

            Me.txtRegno.Text = ls_Regno

            lal_Header.Add("환자번호")
            lal_Header.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LIS)
            lal_Arg.Add(ls_Regno)
            lal_Arg.Add(ls_Regno)

            lal_Rtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", lal_Arg, lal_Header, li_RtnCnt, ls_Regno)

            If lal_Rtn.Count > 0 Then
                Me.txtRegno.Text = lal_Rtn(0).ToString
                Me.txtPatNm.Text = lal_Rtn(1).ToString

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            End If

            btnSearch_Click(Nothing, Nothing)
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnTExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")

        With Me.spdSearchList
            .ReDraw = False

            .MaxRows += 4
            .InsertRows(1, 3)

            .Col = 8
            .Row = 1
            .Text = "수혈의뢰 현황조회"
            .FontBold = True
            .FontSize = 15
            .ForeColor = System.Drawing.Color.Red

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 3 : .Row2 = 3
            .Clip = sColHeaders

            .InsertRows(4, 1)

            If spdSearchList.ExportToExcel("c:\수혈의뢰현황조회_" & sTime & ".xls", "TransfList", "") Then
                Process.Start("c:\수혈의뢰현황조회_" & sTime & ".xls")
            End If

            .DeleteRows(1, 4)
            .MaxRows -= 4

            .ReDraw = True
        End With
    End Sub

    Private Sub btnBExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")
        Dim iColNm As String = ""           ' column header 의 내용

        With Me.spdDetail
            .ReDraw = False

            .MaxRows += 4
            .InsertRows(1, 3)

            .Col = 8
            .Row = 1
            .Text = "성분제제별, 혈액형별 수혈의뢰 현황"
            .FontBold = True
            .FontSize = 15
            .ForeColor = System.Drawing.Color.Red

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 3 : .Row2 = 3
            .Clip = sColHeaders

            .InsertRows(4, 1)

            If spdDetail.ExportToExcel("c:\수혈의뢰통계_" & sTime & ".xls", "TransfList", "") Then
                Process.Start("c:\수혈의뢰통계_" & sTime & ".xls")
            End If

            .DeleteRows(1, 4)
            .MaxRows -= 4

            .ReDraw = True
        End With
    End Sub

    Private Sub rdoAll2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAll2.Click
        Me.lblDept.Visible = False
        Me.lblWard.Visible = False
        Me.cboDept.Visible = False
        Me.cboWard.Visible = False

        Me.cboDept.Text = "[ALL] 전체"
        Me.cboWard.Text = "[ALL] 전체"
    End Sub

    Private Sub rdoO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoO.Click
        Me.lblDept.Visible = True
        Me.cboDept.Visible = True
        Me.lblWard.Visible = False
        Me.cboWard.Visible = False

        Me.cboDept.Text = "[ALL] 전체"
        Me.cboWard.Text = "[ALL] 전체"
    End Sub

    Private Sub rdoI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoI.Click
        Me.lblWard.Visible = True
        Me.cboWard.Visible = True
        Me.lblDept.Visible = False
        Me.cboDept.Visible = False

        Me.cboDept.Text = "[ALL] 전체"
        Me.cboWard.Text = "[ALL] 전체"
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try
            Dim invas_buf As New InvAs
            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISB.dll", "LISB.FGB14_S01")

                '.SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(0)

                a_objParam(0) = Me

                Dim strReturn As String = .InvokeMember("Display_Result", a_objParam).ToString

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbPrint_Data(ByVal rsJobGbn As String)

        Try
            Dim arlPrint As New ArrayList

            With spdSearchList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("regno") : Dim strRegNo As String = .Text
                    .Col = .GetColFromID("patnm") : Dim strPatnm As String = .Text
                    .Col = .GetColFromID("comnmd") : Dim strComNmd As String = .Text
                    .Col = .GetColFromID("vbldno") : Dim strBldNm As String = .Text

                    Dim strBuf() As String = rsJobGbn.Split("|"c)
                    Dim arlItem As New ArrayList

                    For intIdx As Integer = 0 To strBuf.Length - 1

                        If strBuf(intIdx) = "" Then Exit For

                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                        If intCol > 0 Then

                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                            Select Case strField
                                Case "deptnm"
                                    .Row = intRow
                                    .Col = .GetColFromID("deptnm") : Dim strDept As String = .Text
                                    .Col = .GetColFromID("wdsr") : Dim strWard As String = .Text

                                    If strWard = "" Then
                                        arlItem.Add(strDept + "^" + strTitle + "^" + strWidth + "^")
                                    Else
                                        arlItem.Add(strWard + "^" + strTitle + "^" + strWidth + "^")

                                    End If

                                Case "comgbn"
                                    .Row = intRow
                                    .Col = .GetColFromID("comgbn") : Dim strTnsgbn As String = .Text
                                    .Col = .GetColFromID("state") : Dim strBldState As String = .Text

                                    If strTnsgbn = "준비" Then
                                        arlItem.Add(strTnsgbn + "^" + strTitle + "^" + strWidth + "^")
                                    Else
                                        arlItem.Add(strBldState + "^" + strTitle + "^" + strWidth + "^")
                                    End If

                                Case "rst1"
                                    .Row = intRow
                                    .Col = .GetColFromID("rst1") : Dim strVal As String = .Text.PadRight(2, " "c)
                                    .Col = .GetColFromID("rst2") : strVal += .Text.PadRight(2, " "c)
                                    .Col = .GetColFromID("rst3") : strVal += .Text.PadRight(2, " "c)
                                    .Col = .GetColFromID("rst4") : strVal += .Text.PadRight(2, " "c)

                                    arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")

                                Case Else
                                    .Row = intRow
                                    .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                                    arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                            End Select
                        End If

                    Next

                    Dim objPat As New FGB14_PrtInfo

                    With objPat
                        .sRegNo = strRegNo
                        .sPatNm = strPatnm
                        .sComNmd = strComNmd
                        .sBldNm = strBldNm

                        .aItem = arlItem
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            Dim prt As New FGB14_PRINT_NEW
            prt.msTitle = "수혈의뢰 대장"
            prt.msTitle_Date = lblTitle.Text + ": " + Format(dtpDate1.Value, "yyyy-MM-dd").ToString + " ~ " + Format(dtpDate1.Value, "yyyy-MM-dd").ToString
            prt.maPrtData = arlPrint
            prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

            'If chkPreview.Checked Then
            '    prt.sbPrint_Preview(chkTclsFix.Checked)
            'Else
            prt.sbPrint()
            'End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
    '20210105 jhs 성분제재 묶음으로 조회
    Private Sub chkBldBranchSrh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBldBranchSrh.CheckedChanged
        Try
            If chkBldBranchSrh.Checked Then
                cboBranchComcd.Visible = True
                lblBranchComcd.Visible = True
                lblComcd.Visible = False
                cboComCd.Visible = False
            Else
                cboBranchComcd.Visible = False
                lblBranchComcd.Visible = False
                lblComcd.Visible = True
                cboComCd.Visible = True
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
    '-----------------------------------------------------
End Class

Public Class FGB14_PrtInfo
    Public sRegNo As String = ""
    Public sPatNm As String = ""
    Public sSexAge As String = ""
    Public sOrdDt As String = ""
    Public sDeptWard As String = ""
    Public sDoctorNm As String = ""
    Public sTnsGbn As String = ""
    Public sTnsJubSuNo As String = ""
    Public sComNmd As String = ""
    Public sPAboRh As String = ""
    Public sBAboRh As String = ""
    Public sBldNm As String = ""
    Public sJobGbn As String = ""
    Public sJubSuDt As String = ""

    Public sRst1 As String = ""
    Public sRst2 As String = ""
    Public sRst3 As String = ""
    Public sRst4 As String = ""
    Public sTestDt As String = ""
    Public sTestNm As String = ""

    Public sBefOutDt As String = ""
    Public sBefOUtNm As String = ""

    Public sOutDt As String = ""
    Public sOutNm As String = ""
    Public sRecNm As String = ""

    Public sRtnDt As String = ""
    Public sRtnNm As String = ""

    Public aItem As New ArrayList
End Class

Public Class FGB14_PRINT_NEW
    Private Const msFile As String = "File : FGB12.vb, Class : B01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    Private miTitle_ExmCnt As Integer = 0
    Private miCCol As Integer = 1

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msgExmWidth As Single
    Public msTitle As String
    Public maPrtData As ArrayList
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd hh:mm")
    Public msTitle_sub_right_1 As String = ""

    Private miItemCnt As Integer = 0

    Public Sub sbPrint_Preview(ByVal rsPrtGbn As String)
        Dim sFn As String = "Sub sbPrint_Preview(String)"

        Try
            Dim prtRView As PrintPreviewDialog
            Dim prtR As PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = True

            miItemCnt = CType(maPrtData(0), FGB14_PrtInfo).aItem.Count - 1

            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR = New PrintDocument
                prtRView = New PrintPreviewDialog

                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        End Try
    End Sub

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(String)"

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
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miCIdx = 0
        miCCol = 1
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        'Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        'Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = fnt_Body.GetHeight(e.Graphics)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * 0, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 등록번호
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 0, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            '-- 성명
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 0, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 성분제제
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 0, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sComNmd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 혈액번호
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 0, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sBldNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            Dim arlItem As ArrayList = CType(maPrtData.Item(intIdx), FGB14_PrtInfo).aItem

            For intIx2 As Integer = 0 To arlItem.Count - 1
                rect = New Drawing.RectangleF(msgPosX(5 + intIx2), sngPosY + sngPrtH * 0, msgPosX(6 + intIx2) - msgPosX(5 + intIx2), sngPrtH)
                e.Graphics.DrawString(arlItem.Item(intIx2).ToString.Split("^"c)(0), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            miCIdx += 1

            sngPosY += sngPrtH + sngPrtH / 2
            If msgHeight - sngPrtH * 6 < sngPosY Then Exit For

        Next

        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miCIdx < maPrtData.Count - 1 Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0

        Dim arlItem As ArrayList = CType(maPrtData.Item(0), FGB14_PrtInfo).aItem
        Dim sngPosX(0 To 5 + arlItem.Count) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40    '-- 번호
        sngPosX(2) = sngPosX(1) + 80    '-- 등록번호
        sngPosX(3) = sngPosX(2) + 70    '-- 성명
        sngPosX(4) = sngPosX(3) + 160   '-- 성분제제
        sngPosX(5) = sngPosX(4) + 100   '-- 혈액번호

        For intIdx As Integer = 0 To arlItem.Count - 1
            sngPosX(6 + intIdx) = sngPosX(5 + intIdx) + Convert.ToSingle(arlItem(intIdx).ToString.Split("^"c)(2))
        Next

        sngPosX(5 + arlItem.Count) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        Dim sf_t As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 출력정보
        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length - 20, sngPosY + 30, msgWidth - 8 * msTitle_sub_right_1.Length, sngPrt), sf_l)
        End If

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)
        sngPrt *= Convert.ToSingle(1.9)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY * Convert.ToSingle(0.9), msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6) - 20, sngPosY - 40, msgWidth - 8 * (msTitle_Time.Length + 6), sngPrt), sf_l)

        sngPosY += sngPrt

        fnPrtTitle = sngPosY + sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY + sngPrt * 0, sngPosX(1) - sngPosX(0), sngPrt), sf_c)

        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrt * 0, sngPosX(2) - sngPosX(1), sngPrt), sf_l)
        e.Graphics.DrawString("성명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 0, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성분제제", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 0, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("혈액번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 0, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        For intIdx As Integer = 0 To arlItem.Count - 1
            e.Graphics.DrawString(arlItem.Item(intIdx).ToString.Split("^"c)(1), fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5 + intIdx), sngPosY + sngPrt * 0, sngPosX(6 + intIdx) - sngPosX(5 + intIdx), sngPrt), sf_l)
        Next

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt, msgWidth, sngPosY + sngPrt)

    End Function


End Class

Public Class FGB14_PRINT
    Private Const msFile As String = "File : FGS14.vb, Class : S01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    Private miTitle_ExmCnt As Integer = 0
    Private miCCol As Integer = 1

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msgExmWidth As Single
    Public msTitle As String
    Public maPrtData As ArrayList
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd hh:mm")

    Private miItemCnt As Integer = 0

    Public Sub sbPrint_Preview(ByVal rsPrtGbn As String)
        Dim sFn As String = "Sub sbPrint_Preview(String)"

        Try
            Dim prtRView As PrintPreviewDialog
            Dim prtR As PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = True

            miItemCnt = CType(maPrtData(0), FGB14_PrtInfo).aItem.Count - 1

            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR = New PrintDocument
                prtRView = New PrintPreviewDialog

                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        End Try
    End Sub

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(String)"

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
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miCIdx = 0
        miCCol = 1
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        'Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        'Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = fnt_Body.GetHeight(e.Graphics)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * 0, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 등록번호
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 0, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            '-- 성명
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * 0, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 성분제제
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * 0, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sComNmd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 혈액번호
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 0, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB14_PrtInfo).sBldNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            Dim arlItem As ArrayList = CType(maPrtData.Item(intIdx), FGB14_PrtInfo).aItem

            For intIx2 As Integer = 0 To arlItem.Count - 1
                rect = New Drawing.RectangleF(msgPosX(5 + intIx2), sngPosY + sngPrtH * 0, msgPosX(6 + intIx2) - msgPosX(5 + intIx2), sngPrtH)
                e.Graphics.DrawString(arlItem.Item(intIx2).ToString.Split("^"c)(0), fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            miCIdx += 1

            sngPosY += sngPrtH + sngPrtH / 2
            If msgHeight - sngPrtH * 6 < sngPosY Then Exit For

        Next

        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miCIdx < maPrtData.Count - 1 Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim sngTmp As Single

        Dim arlItem As ArrayList = CType(maPrtData.Item(0), FGB14_PrtInfo).aItem
        Dim sngPosX(0 To 5 + arlItem.Count) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40    '-- 번호
        sngPosX(2) = sngPosX(1) + 80    '-- 등록번호
        sngPosX(3) = sngPosX(2) + 70    '-- 성명
        sngPosX(4) = sngPosX(3) + 160   '-- 성분제제
        sngPosX(5) = sngPosX(4) + 90    '-- 혈액번호

        For intIdx As Integer = 0 To arlItem.Count - 1
            sngPosX(6 + intIdx) = sngPosX(5 + intIdx) + Convert.ToSingle(arlItem(intIdx).ToString.Split("^"c)(2))
        Next

        sngPosX(5 + arlItem.Count) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        Dim sf_t As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)
        sngPrt *= Convert.ToSingle(1.9)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY * Convert.ToSingle(0.9), msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY * Convert.ToSingle(0.9), msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt

        fnPrtTitle = sngPosY + sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY + sngPrt * 0, sngPosX(1) - sngPosX(0), sngPrt), sf_c)

        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY + sngPrt * 0, sngPosX(2) - sngPosX(1), sngPrt), sf_l)
        e.Graphics.DrawString("성명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY + sngPrt * 0, sngPosX(3) - sngPosX(2), sngPrt), sf_l)
        e.Graphics.DrawString("성분제제", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY + sngPrt * 0, sngPosX(4) - sngPosX(3), sngPrt), sf_l)
        e.Graphics.DrawString("혈액번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY + sngPrt * 0, sngPosX(5) - sngPosX(4), sngPrt), sf_l)

        For intIdx As Integer = 0 To arlItem.Count - 1
            e.Graphics.DrawString(arlItem.Item(intIdx).ToString.Split("^"c)(1), fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5 + intIdx), sngPosY + sngPrt * 0, sngPosX(6 + intIdx) - sngPosX(5 + intIdx), sngPrt), sf_l)
        Next

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt, msgWidth, sngPosY + sngPrt)

    End Function


End Class