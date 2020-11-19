Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN
Imports LISAPP.APP_BD
Imports LISAPP.APP_BD.OcsFn

Public Class FGB02
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGB02.vb, Class : FGB02" & vbTab

    Private Sub sbClear_Form(ByVal rbAll As Boolean)

        If rbAll Then Me.spdList.MaxRows = 0

        Me.axPatInfo.Clear()
        Me.spdDonerList.MaxRows = 0
        Me.axRstView.Clear()

        Me.rdoJudgN.Checked = False : Me.rdoJudgY.Checked = True
        Me.txtDisCd.Text = "" : Me.txtDisCont.Text = "" : Me.txtJudgCmt.Text = ""

        Me.txtJudgDt.Text = "" : Me.txtJudgNm.Text = ""

    End Sub

    Private Sub sbDisplay_Ward()
        Dim sFn As String = "sbDisplay_Ward"

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList()

            Me.cboDptOrWard.Items.Clear()
            cboDptOrWard.Items.Add("[ ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDptOrWard.Items.Add("[" + dt.Rows(ix).Item("wardno").ToString + "] " + dt.Rows(ix).Item("wardnm").ToString)
            Next

            If cboDptOrWard.Items.Count > 0 Then cboDptOrWard.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbDisplay_Dept()
        Dim sFn As String = "sbDisplay_Dept"

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList()

            Me.cboDptOrWard.Items.Clear()
            Me.cboDptOrWard.Items.Add("[ ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDptOrWard.Items.Add("[" + dt.Rows(ix).Item("deptcd").ToString + "] " + dt.Rows(ix).Item("deptnm").ToString)
            Next

            If Me.cboDptOrWard.Items.Count > 0 Then cboDptOrWard.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbDisplay_PatList(Optional ByVal rsRegNo As String = "")
        Dim sFn As String = "sbDisplay_PatList"

        Try
            sbClear_Form(True)

            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim sIoGbn As String = "I"
            Dim sSpcFlg As String = "0"

            If Me.rdoIoGbnO.Checked Then sIoGbn = "O"
            If Me.rdoJubsu.Checked Then sSpcFlg = "1"

            Dim dt As DataTable = fnGet_Don_Order(sIoGbn, Ctrl.Get_Code(cboDptOrWard), Me.dtpDateS.Text, Me.dtpDateE.Text, rsRegNo, sSpcFlg, "")

            If dt.Rows.Count < 1 Then Return

            With Me.spdList
                .ReDraw = False
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1

                    Dim sPatInfo() As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c)

                    '< 나이계산
                    Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                    Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                    If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                    '>

                    .Row = ix + 1
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = sPatInfo(0)
                    .Col = .GetColFromID("sex") : .Text = sPatInfo(1)
                    .Col = .GetColFromID("age") : .Text = iAge.ToString
                    .Col = .GetColFromID("sunabyn") : .Text = dt.Rows(ix).Item("sunabyn").ToString
                    .Col = .GetColFromID("dongbn") : .Text = dt.Rows(ix).Item("dongbn").ToString
                    .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString


                    'hidden col
                    .Col = .GetColFromID("iogbn") : .Text = dt.Rows(ix).Item("iogbn").ToString
                    .Col = .GetColFromID("owngbn") : .Text = dt.Rows(ix).Item("owngbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = dt.Rows(ix).Item("fkocs").ToString
                    .Col = .GetColFromID("tordcd") : .Text = dt.Rows(ix).Item("fkocs").ToString
                    .Col = .GetColFromID("donjubsuno") : .Text = dt.Rows(ix).Item("tnmd").ToString
                Next

                .ReDraw = True
            End With

            spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1), rsRegNo)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Doner_Info(ByVal rsRegNo As String)

        Try
            Dim dt As DataTable = (New LISAPP.APP_BD.DonFn).fnGet_DonerList_Regno(rsRegNo)

            Dim iCol As Integer = 0

            With Me.spdDonerList
                .MaxRows = dt.Rows.Count

                If dt.Rows.Count < 1 Then Return

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1

                    Dim sPatInfo() As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c)
                    Dim sPatInfo_don() As String = dt.Rows(ix).Item("patinfo_don").ToString.Split("|"c)

                    iCol = .GetColFromID("dondt") : If iCol > 0 Then .Text = dt.Rows(ix).Item("dondt").ToString
                    iCol = .GetColFromID("bldno") : If iCol > 0 Then .Text = dt.Rows(ix).Item("bldno").ToString
                    iCol = .GetColFromID("bldqnt") : If iCol > 0 Then .Text = dt.Rows(ix).Item("bldqnt").ToString

                    If sPatInfo(0) = "" Then
                        iCol = .GetColFromID("patnm") : If iCol > 0 Then .Text = sPatInfo(0)
                        iCol = .GetColFromID("idno") : If iCol > 0 Then .Text = sPatInfo(0)
                    Else
                        iCol = .GetColFromID("patnm") : If iCol > 0 Then .Text = sPatInfo_don(0)
                        iCol = .GetColFromID("idno") : If iCol > 0 Then .Text = sPatInfo_don(0)
                    End If

                    iCol = .GetColFromID("judgyn") : If iCol > 0 Then .Text = dt.Rows(ix).Item("judgyn").ToString
                    iCol = .GetColFromID("judgdt") : If iCol > 0 Then .Text = dt.Rows(ix).Item("judgdt").ToString
                    iCol = .GetColFromID("tnsregno") : If iCol > 0 Then .Text = dt.Rows(ix).Item("tnsregno").ToString
                    iCol = .GetColFromID("doncmt") : If iCol > 0 Then .Text = dt.Rows(ix).Item("doncmt").ToString

                Next
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_TestRst(ByVal rsRegNo As String, ByVal rsFkOcs As String)

        Try

            Me.axRstView.Clear()

            Dim dt As DataTable = (New LISAPP.APP_BD.DonFn).fnGet_Doner_RstList(rsRegNo, rsFkOcs)     ' 검체번호, 검사항목, 결과를 가져옴 (헌혈자 등록번호를 이용!!)

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                axRstView.Display_Result(dt.Rows(ix).Item("bcno").ToString, CType(IIf(ix = 0, False, True), Boolean))
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_JudgRst(ByVal rsDonNo As String)

        Try

            Dim dt As DataTable = (New LISAPP.APP_BD.DonFn).fnGet_Doner_JudgInfo(rsDonNo)     ' 검체번호, 검사항목, 결과를 가져옴 (헌혈자 등록번호를 이용!!)

            If dt.Rows.Count < 1 Then Return

            Me.rdoJudgY.Checked = True
            If dt.Rows(0).Item("judgyn").ToString = "N" Then Me.rdoJudgN.Checked = True
            Me.txtDisCd.Text = dt.Rows(0).Item("discd").ToString
            Me.txtDisCont.Text = dt.Rows(0).Item("discont").ToString
            Me.txtJudgCmt.Text = dt.Rows(0).Item("judgcmt").ToString
            Me.txtJudgDt.Text = dt.Rows(0).Item("judgdt").ToString
            Me.txtJudgNm.Text = dt.Rows(0).Item("judgnm").ToString

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try
    End Sub

    Private Sub rdoIoGbnA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoIoGbnI.Click, rdoIoGbnO.Click

        If rdoIoGbnI.Checked Then
            Me.lblDptOrWard.Text = "병    동"
            sbDisplay_Ward()
        Else
            Me.lblDptOrWard.Text = "진 료 과"
            sbDisplay_Dept()
        End If

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

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent, Optional ByVal rsRegNo As String = "") Handles spdList.ClickEvent

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim sRegNo As String = ""
            Dim sOrdDt As String = ""
            Dim sDonGbn As String = ""
            Dim sFkOcs As String = ""
            Dim sDonNo As String = ""

            sbClear_Form(False)

            With Me.spdList
                .Row = e.row
                .Col = .GetColFromID("regno") : sRegNo = .Text
                .Col = .GetColFromID("orddt") : sOrdDt = .Text.Replace("-", "")
                .Col = .GetColFromID("fkocs") : sFkOcs = .Text
                .Col = .GetColFromID("dongbn") : sDonGbn = .Text
                .Col = .GetColFromID("donjubsuno") : sDonNo = .Text
            End With

            Select Case sDonGbn
                Case "일반" : sDonGbn = "1"
                Case "성분" : sDonGbn = "2"
                Case "지정" : sDonGbn = "3"
                Case "자가" : sDonGbn = "4"
            End Select

            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim cpi As STU_PatInfo
            Dim sIoGbn As String = "I"
            Dim sSpcFlg As String = "0"

            If Me.rdoIoGbnO.Checked Then sIoGbn = "O"
            If Me.rdoJubsu.Checked Then sSpcFlg = "1"

            Dim dt As DataTable = fnGet_Don_Order(sIoGbn, Ctrl.Get_Code(cboDptOrWard), sOrdDt, sOrdDt, sRegNo, sSpcFlg, sFkOcs)
            If dt.Rows.Count < 1 Then Return
            cpi = OCSAPP.OcsLink.Ord.fnSet_PatInfo(dt.Rows(0), dtSysDate)
            Me.axPatInfo.DisplayPatInfo(cpi)

            '-- 관련 헌혈자 리스트 뿌려주기
            sbDisplay_Doner_Info(sRegNo)

            '-- 검사결과 표시
            sbDisplay_TestRst(sRegNo, sFkOcs)

            '-- 핀정결과 표시
            If sDonNo <> "" Then sbDisplay_JudgRst(sDonNo)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbClear_Form(True)
    End Sub

    Private Sub TextBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRegNo.Click, txtPatNm.Click, txtDisCd.Click

        CType(sender, System.Windows.Forms.TextBox).SelectAll()

    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown
        Dim sFn As String = "Handles txtRegNo.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            If IsNumeric(Me.txtRegNo.Text.Substring(0, 1)) Then
                Me.txtRegNo.Text = Me.txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                Me.txtRegNo.Text = Me.txtRegNo.Text.Substring(0, 1) + Me.txtRegNo.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            End If

            sbDisplay_PatList(Me.txtRegNo.Text)

            Me.txtRegNo.Focus()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPatNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatNm.KeyDown
        Dim sFn As String = "Handles txtPatNm.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_byNm(Me.txtPatNm.Text)

            objHelp.FormText = "환자정보"
            objHelp.OnRowReturnYN = True

            objHelp.AddField("bunho", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("suname", "환자명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sex", "성별", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sujumin", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("wardroom", "병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtPatNm)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - txtPatNm.Left, pntFrmXY.Y + pntCtlXY.Y + txtPatNm.Height + 80, dt)

            If aryList.Count > 0 Then
                sbDisplay_PatList(aryList.Item(0).ToString.Split("|"c)(0))
            End If

            Me.txtPatNm.Text = ""
            Me.txtPatNm.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGB02_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB02_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If btnReg.Enabled Then btnReg_Click(Nothing, Nothing)
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGB02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.WindowState = FormWindowState.Maximized

        Me.dtpDateS.Value = Now 'DateAdd(DateInterval.Day, -1, Now)
        Me.dtpDateE.Value = Now

        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

        btnClear_Click(Nothing, Nothing)

    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click

        Try
            If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "접수 및 판정 처리 하시겠습니까?") = False Then Return

            Dim stu_jubsu As New STU_DONER

            With Me.spdList
                .Row = .ActiveRow
                .Col = .GetColFromID("regno") : stu_jubsu.RegNo = .Text
                .Col = .GetColFromID("owngbn") : stu_jubsu.OwnGbn = .Text
                .Col = .GetColFromID("fkocs") : stu_jubsu.FkOcs = .Text
                .Col = .GetColFromID("dongbn") : Dim sDonGbn As String = .Text

                Select Case sDonGbn
                    Case "일반" : stu_jubsu.DonGbn = "1"
                    Case "성분" : stu_jubsu.DonGbn = "2"
                    Case "지성" : stu_jubsu.DonGbn = "3"
                    Case "자가" : stu_jubsu.DonGbn = "4"
                End Select
            End With

            stu_jubsu.JudgYn = IIf(rdoJudgY.Checked, "Y", "N").ToString
            stu_jubsu.judgCmt = Me.txtJudgCmt.Text
            stu_jubsu.DisCd = Me.txtDisCd.Text
            stu_jubsu.DisCont = Me.txtDisCont.Text
            stu_jubsu.PassGbn = IIf(Me.chkPassGbn0.Checked, "0", "1").ToString

            If (New LISAPP.APP_BD.RegFn).fnExe_Don_Jubsu(stu_jubsu) Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 했습니다.!!")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "처리 중에 오류가 발생 했습니다.!!")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub rdoJubsu_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoJubsu.CheckedChanged, rdoNoJubsu.CheckedChanged

        If Me.rdoNoJubsu.Checked Then
            Me.btnReg.Visible = True
            Me.btnCanecl.Visible = True
        Else
            Me.btnReg.Visible = False
            Me.btnCanecl.Visible = False
        End If

    End Sub

    Private Sub btnHelp_Dis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_Dis.Click

        Dim sFn As String = "Handles btnHelp_Dis.Click"

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnHelp_Dis) + Me.btnHelp_Dis.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnHelp_Dis)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_dis_list()
            Dim sSql As String = ""

            If Me.txtDisCd.Text <> "" Then
                sSql = "discd LIKE '" + Me.txtDisCd.Text + "%'"
                Dim a_dr As DataRow() = dt.Select(sSql, "")

                dt = Fn.ChangeToDataTable(a_dr)
            End If

            objHelp.FormText = "검사정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("discd", "코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("disrsn", "내용", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then

                Me.txtDisCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.txtDisCont.Text = alList.Item(0).ToString.Split("|"c)(1)

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)


        End Try
    End Sub

    Private Sub rdoJudgN_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoJudgN.CheckedChanged, rdoJudgY.CheckedChanged

        If Me.rdoJudgY.Checked Then
            Me.txtDisCd.Text = "" : Me.txtDisCont.Text = ""
            Me.txtDisCd.ReadOnly = True : Me.txtDisCont.ReadOnly = True
            Me.btnHelp_Dis.Enabled = False
        ElseIf Me.rdoJudgN.Checked Then
            Me.txtDisCd.ReadOnly = False : Me.txtDisCont.ReadOnly = False
            Me.btnHelp_Dis.Enabled = True
        End If

    End Sub

    Private Sub txtDisCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDisCd.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtDisCd.Text = "" Then Return

        btnHelp_Dis_Click(Nothing, Nothing)

    End Sub

    Private Sub btnCanecl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCanecl.Click
        Try
            If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "접수 및 판정 처리 하시겠습니까?") = False Then Return

            Dim stu_jubsu As New STU_DONER

            With Me.spdList
                .Row = .ActiveRow
                .Col = .GetColFromID("donjubsuno") : stu_jubsu.DonJusbuNo = .Text
                .Col = .GetColFromID("regno") : stu_jubsu.RegNo = .Text
                .Col = .GetColFromID("owngbn") : stu_jubsu.OwnGbn = .Text
                .Col = .GetColFromID("fkocs") : stu_jubsu.FkOcs = .Text
                .Col = .GetColFromID("dongbn") : Dim sDonGbn As String = .Text

                Select Case sDonGbn
                    Case "일반" : stu_jubsu.DonGbn = "1"
                    Case "성분" : stu_jubsu.DonGbn = "2"
                    Case "지성" : stu_jubsu.DonGbn = "3"
                    Case "자가" : stu_jubsu.DonGbn = "4"
                End Select
            End With

            If (New LISAPP.APP_BD.RegFn).fnExe_Don_Jubsu_Cancel(stu_jubsu) Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 했습니다.!!")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "처리 중에 오류가 발생 했습니다.!!")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
End Class