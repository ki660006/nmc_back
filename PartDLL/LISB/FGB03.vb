Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst
Imports LISAPP.APP_BD
Imports LISAPP.APP_BD.OcsFn

Public Class FGB03
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGB02.vb, Class : FGB02" & vbTab
    Private mbActived As Boolean = False

    Private Sub sbClear_Form(ByVal rbAll As Boolean)

        If rbAll Then
            Me.spdList.MaxRows = 0
        End If

        Me.axPatInfo.sb_ClearLbl()
        Me.spdRst.MaxRows = 0
        Me.spdDonerList.MaxRows = 0

        Me.lblJudg.Text = "" : Me.lblDisCont.Text = "" : Me.lblJudgDt.Text = "" : Me.lblJudgId.Text = ""
        Me.txtBldNo.Mask = "##-##-######"
        Me.txtBldNo.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
        Me.txtBldNo.Text = PRG_CONST.HOSPITAL_DONER_NO + Format(Now, "yyyy").ToString.Substring(2, 2) : Me.txtBldNo.ReadOnly = False
        Me.rdoQnt0.Checked = True : Me.rdoBag0.Checked = True
        Me.chkPassGbn.Checked = False : Me.txtDonCmt.Text = ""

    End Sub

    Private Sub sbDisplay_Test()
        Try
            Dim dt As DataTable = fnGet_Doner_Test("2", "")
            If dt Is Nothing Then Return

            With Me.spdRst
                .ReDraw = False
                .MaxRows = 0
                .MaxCols = .GetColFromID("bcno")

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .MaxCols += 1
                    .Row = 0
                    .Col = .MaxCols : .ColID = dt.Rows(ix).Item("testcd").ToString : .Text = dt.Rows(ix).Item("tnmd").ToString
                Next
                .ReDraw = True
            End With
        Catch ex As Exception

        End Try


    End Sub
    Private Sub sbDisplay_PatList(Optional ByVal rsRegNo As String = "")
        Dim sFn As String = "sbDisplay_PatList"

        Try
            sbClear_Form(True)

            Dim sJubsuFlg As String = "2"

            If Me.rdoDoner.Checked Then sJubsuFlg = "3"

            Dim dt As DataTable = fnGet_Doner_List(Me.dtpDateS.Text, Me.dtpDateE.Text, sJubsuFlg, rsRegNo)

            If dt.Rows.Count < 1 Then Return

            With Me.spdList
                .ReDraw = False
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1

                    .Row = ix + 1
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("sex") : .Text = dt.Rows(ix).Item("sex").ToString
                    .Col = .GetColFromID("age") : .Text = dt.Rows(ix).Item("age").ToString
                    .Col = .GetColFromID("donseq") : .Text = dt.Rows(ix).Item("donseq").ToString
                    .Col = .GetColFromID("donjubsuno") : .Text = dt.Rows(ix).Item("donjubsuno").ToString
                    .Col = .GetColFromID("jubsudt") : .Text = dt.Rows(ix).Item("jubsudt").ToString
                    .Col = .GetColFromID("tnsregno") : .Text = dt.Rows(ix).Item("tnsregno").ToString
                    .Col = .GetColFromID("dongbn") : .Text = dt.Rows(ix).Item("dongbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = dt.Rows(ix).Item("fkocs").ToString
                    .Col = .GetColFromID("owngbn") : .Text = dt.Rows(ix).Item("owngbn").ToString
                    .Col = .GetColFromID("dongbnv")

                    Select Case dt.Rows(ix).Item("dongbn").ToString()
                        Case "1" : .Text = "일반"
                        Case "2" : .Text = "지정"
                        Case "3" : .Text = "성분"
                        Case "4" : .Text = "자가"
                    End Select
                Next

                .ReDraw = True
            End With

            spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Rst(ByVal rsRegNo As String, ByVal rsDonGbn As String, ByVal rsJubsuDt As String)
        Dim sFn As String = "sbDisplay_Rst"

        Try
            Dim dt_t As DataTable = fnGet_Doner_Test(rsDonGbn, rsJubsuDt)
            If dt_t Is Nothing Then Return

            Dim dt As DataTable = fnGet_Doner_Rst(rsRegNo, rsDonGbn)
            If dt Is Nothing Then Return

            Dim sBcNo As String = ""

            With Me.spdRst
                .ReDraw = False
                .MaxRows = 0
                .MaxCols = .GetColFromID("bcno")

                For ix As Integer = 0 To dt_t.Rows.Count - 1
                    .MaxCols += 1
                    .Row = 0
                    .Col = .MaxCols : .ColID = dt_t.Rows(ix).Item("testcd").ToString : .Text = dt_t.Rows(ix).Item("tnmd").ToString
                Next

                If dt.Rows.Count < 1 Then Return

                For ix As Integer = 0 To dt.Rows.Count - 1

                    If sBcNo <> dt.Rows(ix).Item("bcno").ToString Then
                        .MaxRows += 1
                    End If

                    .Row = .MaxRows
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                    .Col = .GetColFromID("rstflg")
                    Select Case dt.Rows(ix).Item("rstflg").ToString
                        Case "0" : .Text = "접"
                        Case "1" : .Text = "검"
                        Case "2" : .Text = "완"
                    End Select

                    Dim iCol As Integer = .GetColFromID(dt.Rows(ix).Item("testcd").ToString)
                    If iCol >= 0 Then
                        .Col = iCol : .Text = dt.Rows(ix).Item("viewrst").ToString

                        .ForeColor = Color.Black
                        .BackColor = Color.White

                        If dt.Rows(ix).Item("hlmark").ToString = "L" Then
                            .ForeColor = Color.Blue
                        ElseIf dt.Rows(ix).Item("hlmark").ToString = "H" Then
                            .ForeColor = Color.Red
                        End If

                        If dt.Rows(ix).Item("panicmark").ToString() = "P" Then
                            .BackColor = FixedVariable.g_color_PM_Bg
                        End If

                        If dt.Rows(ix).Item("deltamark").ToString() = "D" Then
                            .BackColor = FixedVariable.g_color_DM_Bg
                        End If

                    End If

                    sBcNo = dt.Rows(ix).Item("bcno").ToString
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Doner_History(ByVal rsRegNo As String, ByVal rsDonJubSuNo As String)
        Dim sFn As String = "sbDisplay_Doner_History"

        Try
            Dim dt As DataTable = fnGet_Doner_Info(rsRegNo, "H", rsDonJubSuNo)
            If dt Is Nothing Then Return

            Dim sBcNo As String = ""

            With Me.spdDonerList
                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1

                    .Row = ix + 1
                    .Col = .GetColFromID("jubsudt") : .Text = dt.Rows(ix).Item("jubsudt").ToString
                    .Col = .GetColFromID("dondt") : .Text = dt.Rows(ix).Item("dondt").ToString
                    .Col = .GetColFromID("bldno") : .Text = dt.Rows(ix).Item("bldno").ToString
                    .Col = .GetColFromID("bldqnt") : .Text = dt.Rows(ix).Item("bldqnt").ToString
                    .Col = .GetColFromID("donbag") : .Text = dt.Rows(ix).Item("donbag").ToString
                    .Col = .GetColFromID("tnsregno") : .Text = dt.Rows(ix).Item("tnsregno").ToString

                    .Col = .GetColFromID("dongbn")
                    Select Case dt.Rows(ix).Item("dongbn").ToString
                        Case "1" : .Text = "일반"
                        Case "2" : .Text = "지정"
                        Case "3" : .Text = "성분"
                        Case "4" : .Text = "자가"
                    End Select
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub

    Private Sub FGB03_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        mbActived = True

    End Sub

    Private Sub FGB03_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Try
            Select Case e.KeyCode
                Case Keys.F2
                    If Me.btnReg.Visible = False Then Return
                    btnReg_Click(Nothing, Nothing)
                Case Keys.F6
                    btnQuery_Click(Nothing, Nothing)
                Case Keys.F4
                    btnClear_Click(Nothing, Nothing)
                Case Keys.Escape
                    btnExit_Click(Nothing, Nothing)
            End Select

        Catch ex As Exception

        End Try
    End Sub


    Private Sub FGB03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Me.dtpDateS.Value = DateAdd(DateInterval.Day, -7, Now)
            Me.dtpDateE.Value = Now
            Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

            sbClear_Form(True)
            sbDisplay_Test()

            Me.WindowState = FormWindowState.Maximized

        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnCanecl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanecl.Click

        Try
            If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "헌혈 시행 하시겠습니까?") = False Then Return

            Dim stu_jubsu As New STU_DONER

            With Me.spdList
                .Row = .ActiveRow
                .Col = .GetColFromID("dongbn") : stu_jubsu.DonGbn = .Text
                .Col = .GetColFromID("donjubsuno") : stu_jubsu.DonJusbuNo = .Text.Replace("-", "")
                .Col = .GetColFromID("fkocs") : stu_jubsu.FkOcs = .Text
                .Col = .GetColFromID("owngbn") : stu_jubsu.OwnGbn = .Text
            End With

            stu_jubsu.BldQnt = IIf(Me.rdoQnt0.Checked, "0", "1").ToString
            stu_jubsu.DonBag = IIf(Me.rdoBag0.Checked, "0", IIf(Me.rdoBag1.Checked, "1", "2")).ToString
            stu_jubsu.ABO = Me.axPatInfo.AboRh.Replace("-", "").Replace("+", "")
            stu_jubsu.Rh = Me.axPatInfo.AboRh.Replace("A", "").Replace("B", "").Replace("O", "")
            stu_jubsu.PassGbn = IIf(Me.chkPassGbn.Checked, "0", "1").ToString
            stu_jubsu.DonCmt = Me.txtDonCmt.Text

            If (New LISAPP.APP_BD.RegFn).fnExe_Don_BldNo_Cancel(stu_jubsu) Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 시행 취소 했습니다.!!")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "시행 취소 중에 오류가 발생 했습니다.!!")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        sbClear_Form(True)

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Set_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Set.Click
        Dim sFn As String = "Handles btnPrintSet.Click"

        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC(Me.Name)

        Try
            objFrm.ShowDialog()
            Me.lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            sbDisplay_PatList()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click

        Try
            If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "헌혈 시행 하시겠습니까?") = False Then Return

            Dim stu_jubsu As New STU_DONER

            With Me.spdList
                .Row = .ActiveRow
                .Col = .GetColFromID("dongbn") : stu_jubsu.DonGbn = .Text
                .Col = .GetColFromID("donjubsuno") : stu_jubsu.DonJusbuNo = .Text.Replace("-", "")
                .Col = .GetColFromID("fkocs") : stu_jubsu.FkOcs = .Text
                .Col = .GetColFromID("owngbn") : stu_jubsu.OwnGbn = .Text
            End With

            stu_jubsu.BldQnt = IIf(Me.rdoQnt0.Checked, "0", "1").ToString
            stu_jubsu.DonBag = IIf(Me.rdoBag0.Checked, "0", IIf(Me.rdoBag1.Checked, "1", "2")).ToString
            stu_jubsu.ABO = Me.axPatInfo.AboRh.Replace("-", "").Replace("+", "")
            stu_jubsu.Rh = Me.axPatInfo.AboRh.Replace("A", "").Replace("B", "").Replace("O", "")
            stu_jubsu.PassGbn = IIf(Me.chkPassGbn.Checked, "0", "1").ToString
            stu_jubsu.DonCmt = Me.txtDonCmt.Text

            If (New LISAPP.APP_BD.RegFn).fnExe_Don_Bldno(stu_jubsu) Then

                '-- 혈액번호 라베 출력 추가

                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 했습니다.!!")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "처리 중에 오류가 발생 했습니다.!!")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub rdoDoner_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDoner.CheckedChanged, rdoNoDoner.CheckedChanged

        If mbActived = False Then Return

        btnClear_Click(Nothing, Nothing)

        If Me.rdoDoner.Checked Then
            Me.btnCanecl.Visible = True
            Me.btnReg.Visible = False
        Else
            Me.btnCanecl.Visible = False
            Me.btnReg.Visible = True
        End If

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        Try
            Me.Cursor = Cursors.WaitCursor

            sbClear_Form(False)

            With Me.spdList

                .Row = e.row
                .Col = .GetColFromID("regno") : Dim sRegNo As String = .Text
                .Col = .GetColFromID("donGbn") : Dim sDonGbn As String = .Text
                .Col = .GetColFromID("jubsudt") : Dim sJubsudt As String = .Text.Replace("-", "").Replace(" ", "").Replace(":", "")
                .Col = .GetColFromID("donjubsuno") : Dim sDonjubsuno As String = .Text.Replace("-", "")
                .Col = .GetColFromID("orddt") : Dim sOrddt As String = .Text.Replace("-", "").Replace(" ", "").Replace(":", "")
                .Col = .GetColFromID("fkocs") : Dim sFkocs As String = .Text.Replace("-", "").Replace(" ", "").Replace(":", "")

                Me.axPatInfo.sb_setPatinfo(sRegNo, sFkocs)

                sbDisplay_Rst(sRegNo, sDonGbn, sJubsudt)
                sbDisplay_Doner_History(sRegNo, sDonjubsuno)

                Dim dt As DataTable = fnGet_Doner_Info(sRegNo, "", sDonjubsuno)

                If dt Is Nothing Then Return

                If dt.Rows.Count > 0 Then
                    Me.lblJudg.Text = dt.Rows(0).Item("judg").ToString
                    Me.lblDisCont.Text = dt.Rows(0).Item("discont").ToString
                    Me.lblJudgDt.Text = dt.Rows(0).Item("judgdt").ToString
                    Me.lblJudgId.Text = dt.Rows(0).Item("judgnm").ToString

                    If dt.Rows(0).Item("bldno").ToString = "" Then
                    Else
                        Me.txtBldNo.Text = dt.Rows(0).Item("bldno").ToString : Me.txtBldNo.ReadOnly = True
                    End If

                    If dt.Rows(0).Item("bldqnt").ToString = "0" Then
                        Me.rdoQnt0.Checked = True
                    ElseIf dt.Rows(0).Item("bldqnt").ToString = "1" Then

                        Me.rdoQnt1.Checked = True
                    End If

                    If dt.Rows(0).Item("donbag").ToString = "0" Then
                        Me.rdoBag0.Checked = True
                    ElseIf dt.Rows(0).Item("donbag").ToString = "1" Then
                        Me.rdoBag1.Checked = True
                    ElseIf dt.Rows(0).Item("donbag").ToString = "2" Then
                        Me.rdoBag2.Checked = True
                    End If

                    Me.chkPassGbn.Checked = CType(IIf(dt.Rows(0).Item("passgbn").ToString = "1", True, False), Boolean)
                    Me.txtDonCmt.Text = dt.Rows(0).Item("doncmt").ToString
                End If

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub txtBldNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBldNo.Click
        Me.txtBldNo.SelectionStart = 6
    End Sub

End Class