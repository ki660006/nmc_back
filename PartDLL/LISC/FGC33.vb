'>>> 모닝 채혈
Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_C
Imports LOGIN

Imports System.Windows.Forms
Imports System.Drawing

Public Class FGC33
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGC33.vb, Class : FGC33" + vbTab

    Private msFormNm As String = "FGC33"
    Private msIoGbn As String = "I"
    Private msDeptOrWard As String '= ""
    Private mbOcsCall As Boolean = False

    Private LoginPopWin As New LoginPopWin


    Private Sub sbDisplay_Ward()
        Dim sFn As String = "sbDisplay_Ward"

        Try
            'Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList()
            Dim dt As DataTable = (New WEBSERVER.CGWEB_C).fnGet_WardList()

            Me.cboDptOrWard.Items.Clear()
            Me.cboDptOrWard.Items.Add("전체" + Space(200) + "|")

            For ix As Integer = 0 To dt.Rows.Count - 1
                cboDptOrWard.Items.Add(dt.Rows(ix).Item("wardnm").ToString + Space(200) + "|" + dt.Rows(ix).Item("deptcd").ToString)
            Next

            If cboDptOrWard.Items.Count > 0 Then cboDptOrWard.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbDisplay_Dept()
        Dim sFn As String = "sbDisplay_Dept"

        Try
            'Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList()
            Dim dt As DataTable = (New WEBSERVER.CGWEB_C).fnGet_DeptList()

            cboDptOrWard.Items.Clear()
            cboDptOrWard.Items.Add("전체" + Space(200) + "|")

            For ix As Integer = 0 To dt.Rows.Count - 1
                cboDptOrWard.Items.Add(dt.Rows(ix).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(ix).Item("deptcd").ToString)
            Next

            If cboDptOrWard.Items.Count > 0 Then cboDptOrWard.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbClear_Form()

        Me.lblPatCount.Text = ">> 대상환자 건수 :"
        Me.axPatInfo.Clear()
        Me.axCollList.Clear()

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        Me.WindowState = Windows.Forms.FormWindowState.Maximized


    End Sub

    Public Sub New(ByVal rsIoGbn As String, ByVal rsDptOrWard As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        msIoGbn = rsIoGbn
        msDeptOrWard = rsDptOrWard

    End Sub

    Private Sub FGC01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If mbOcsCall = False Then MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGC33_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If btnReg.Enabled Then btnReg_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGC33_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If USER_INFO.N_IOGBN = "PAT" Or USER_INFO.N_IOGBN = "WARD" Then
            msIoGbn = "I"
        Else
            msIoGbn = "O"
        End If
        msDeptOrWard = USER_INFO.N_WARDorDEPT

        DS_FormDesige.sbInti(Me)

        Me.btnReg.Text = PRG_CONST.BUTTON_COLL_BATCH + "(F2)"

        Me.axCollList.CollBatch = True
        Me.axCollList.SearchMode = False
        Me.axCollList.CollUsrId = USER_INFO.USRID
        Me.axCollList.Form = Me

        Me.axPatInfo.Form = Me

        Me.dtpDateS.Value = Now 'DateAdd(DateInterval.Day, -1, Now)
        Me.dtpDateE.Value = Now

        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

        Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        btnClear_Click(Nothing, Nothing)

    End Sub

    Private Sub rdoAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAll.Click, rdoColl.Click, rdoNoColl.Click
        Select Case CType(sender, Windows.Forms.RadioButton).Name.ToUpper
            Case "RDONOCOLL"

                If lblOrder.Tag Is Nothing Then
                    btnPrint_BC.Visible = False
                    'btnPrint_Doc.Visible = False

                    btnReg.Visible = True
                    'btnPrint_Label.Visible = True
                Else
                    btnPrint_BC.Visible = False
                    'btnPrint_Doc.Visible = False

                    btnReg.Visible = True
                    'btnPrint_Label.Visible = True
                End If

                axCollList.SearchMode = False
                btnOrdSum.Enabled = True
                'btnCancel_coll.Enabled = False

            Case Else
                If lblOrder.Tag Is Nothing Then
                    btnPrint_BC.Visible = True
                    'btnPrint_Doc.Visible = True

                    btnReg.Visible = False
                    'btnPrint_Label.Visible = False
                Else
                    btnPrint_BC.Visible = True
                    'btnPrint_Doc.Visible = True

                    btnReg.Visible = False
                    'btnPrint_Label.Visible = False
                End If

                axCollList.SearchMode = True
                Me.btnOrdSum.Enabled = False
                'btnCancel_coll.Enabled = True
        End Select

        sbClear_Form()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Me.txtRegNo.Text = ""
        Me.txtPatNm.Text = ""

        sbClear_Form()

    End Sub


    Private Sub FGC33010_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Me.axCollList.Clear()

        If msIoGbn = "I" Then
            Me.rdoIoGbnI.Checked = True
            sbDisplay_Ward()

            If msDeptOrWard <> "" Then
                For ix As Integer = 0 To cboDptOrWard.Items.Count - 1
                    Dim sBuf() As String = Me.cboDptOrWard.Items(ix).ToString.Split("|"c)

                    If sBuf(1) = USER_INFO.N_WARDorDEPT Then
                        Me.cboDptOrWard.SelectedIndex = ix
                        Exit For
                    End If
                Next
            End If

        Else
            Me.rdoIoGbnO.Checked = True
        End If

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Dim stu As New STU_COLLINFO

        If Me.rdoIoGbnO.Checked Then
            stu.IOGBN = "O"
            stu.DEPTCD = Me.cboDptOrWard.Text.Split("|"c)(1)
            stu.WARDCD = ""
        Else
            stu.IOGBN = "I"
            stu.DEPTCD = ""
            stu.WARDCD = Me.cboDptOrWard.Text.Split("|"c)(1)
        End If

        If rdoAll.Checked Then
            stu.SPCFLG1 = "0"
            stu.SPCFLG2 = "4"
        ElseIf rdoColl.Checked Then
            stu.SPCFLG1 = "1"
            stu.SPCFLG2 = "4"
        Else
            stu.SPCFLG1 = "0"
            stu.SPCFLG2 = "0"
        End If

        stu.REGNO = ""
        stu.ORDDT1 = Me.dtpDateS.Text.Replace("-", "")
        stu.ORDDT2 = Me.dtpDateE.Text.Replace("-", "")
        stu.PARTGBN = Ctrl.Get_Code(Me.cboPartGbn)

        Me.axCollList.Clear()
        Me.axCollList.DisplayOrder(stu, Me.chkHopeDay.Checked)
        btnOrdSum_Click(Nothing, Nothing)

        Dim r_cpi As STU_PatInfo = axCollList.PatInfo
        Me.axPatInfo.DisplayPatInfo(r_cpi)

    End Sub

    Private Sub btnOrdSum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrdSum.Click

        If Me.rdoNoColl.Checked = False Then Return

        Me.axCollList.MergeOrder()

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Handles btnCollOne.ButtonClick"

        Try
            Me.Cursor = Cursors.WaitCursor

            ''< 장시간 미사용후 이전 조회내용으로 채혈을 하는 경우의 에러 방지
            'If fnFind_ChgPatInfo() Then
            '    If MsgBox("환자정보의 변경사항을 감지하였습니다. 이를 무시하고 계속 진행하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return
            'End If
            ''>

            'If Me.axCollList.FindEnabledMerge Then
            '    If MsgBox("처방일시는 다르나 동일 검체바코드로 가능한 검사가 존재합니다. 이것을 확인하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '        If MsgBox("아니오를 선택하였으므로 처방일시가 다른 검사를 각각 채혈합니다. 계속하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return

            '    Else
            '        Me.axCollList.MergeOrder()
            '        Return
            '    End If
            'End If

            Me.axCollList.PatInfo = Me.axPatInfo.PatInfo

            Dim al_NoSunab As ArrayList = Nothing
            Dim al_Return As New ArrayList
            Dim sIoGbn As String = "I"
            Dim bToColl As Boolean = False

            If btnReg.Text.StartsWith("채혈") Then bToColl = True



            al_Return = Me.axCollList.CollectSelOrder_Batch(Me.Name, "I", Me.cboDptOrWard.Text.Split("|"c)(1), bToColl, False, CType(IIf(lblBarPrinter.Text.Replace("사용안함", "") = "", False, True), Boolean))

            'If al_NoSunab IsNot Nothing Then
            '    '수납 안된 환자 
            '    Me.axCollBcNos.PrintBarcode_NotSuNab(al_NoSunab, Me.Name)

            '    btnQuery_Click(Nothing, Nothing)
            'End If

            If al_Return Is Nothing Then
                Me.axCollBcNos.Clear()
            Else
                If al_Return.Count > 0 Then
                    Me.axCollBcNos.UseEndocrine = True
                    Me.axCollBcNos.lblBcNOsCnt.Text = al_Return.Count.ToString + "장"
                    Dim sBcNos As String = ""

                    For iCnt As Integer = 0 To al_Return.Count - 1
                        Dim listcollData As List(Of STU_CollectInfo) = CType(al_Return(iCnt), List(Of STU_CollectInfo))

                        If sBcNos.Length > 0 Then sBcNos += ", "
                        sBcNos += listcollData.Item(0).BCNO
                    Next

                    Me.axCollBcNos.txtBcNos.Text = sBcNos.Trim()

                    btnQuery_Click(Nothing, Nothing)
                Else
                    Me.axCollBcNos.Clear()
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.txtRegNo.Focus()
            Me.txtRegNo.SelectAll()

            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnCancel_coll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel_coll.Click
        Dim sFn As String = "Handles btnCancel_coll.ButtonClick"

        Try

            Dim bRet As Boolean = axCollList.CollectSelCancel(Me)
            If bRet Then btnQuery_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnQuery_rst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery_rst.Click
        Dim sFn As String = "Handles btnQuery_rst.ButtonClick"

        Dim frm As Windows.Forms.Form

        frm = New LISV.FGRV01(axPatInfo.RegNo, "", "")
        frm.Activate()
        frm.Show()

    End Sub

    Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.GotFocus, txtPatNm.GotFocus
        With CType(sender, Windows.Forms.TextBox)
            .SelectionStart = 0
            .SelectAll()
        End With
    End Sub

    Private Sub TextBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRegNo.Click, txtPatNm.Click

        CType(sender, System.Windows.Forms.TextBox).SelectAll()

    End Sub


    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown
        Dim sFn As String = "Handles txtRegNo.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try

            Me.txtRegNo.Text = txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)

            Dim stu As New STU_COLLINFO

            If Me.rdoIoGbnO.Checked Then
                stu.IOGBN = "O"
                stu.DEPTCD = Me.cboDptOrWard.Text.Split("|"c)(1)
                stu.WARDCD = ""
            Else
                stu.IOGBN = "I"
                stu.DEPTCD = ""
                stu.WARDCD = Me.cboDptOrWard.Text.Split("|"c)(1)
            End If

            If rdoAll.Checked Then
                stu.SPCFLG1 = "0"
                stu.SPCFLG2 = "4"
            ElseIf rdoColl.Checked Then
                stu.SPCFLG1 = "1"
                stu.SPCFLG2 = "4"
            Else
                stu.SPCFLG1 = "0"
                stu.SPCFLG2 = "0"
            End If

            stu.REGNO = Me.txtRegNo.Text
            stu.ORDDT1 = Me.dtpDateS.Text.Replace("-", "")
            stu.ORDDT2 = Me.dtpDateE.Text.Replace("-", "")
            stu.PARTGBN = Ctrl.Get_Code(Me.cboPartGbn)

            Me.axCollList.Clear()
            Me.axCollList.DisplayOrder(stu, Me.chkHopeDay.Checked)

            Me.txtRegNo.Text = ""
            Me.txtRegNo.Focus()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPatNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatNm.KeyDown
        Dim sFn As String = "Handles txtPatNm.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_byNm(Me.txtPatNm.Text)
            'Dim dt As DataTable = (New WEBSERVER.CGWEB_C).fnGet_PatInfo_ByNm(Me.txtPatNm.Text, "", "")

            objHelp.FormText = "환자정보"
            objHelp.OnRowReturnYN = True

            objHelp.AddField("bunho", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("suname", "환자명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sex", "성별", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sujumin", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("wardroom", "병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtPatNm)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - txtPatNm.Left, pntFrmXY.Y + pntCtlXY.Y + txtPatNm.Height + 80, dt)

            If alList.Count > 0 Then

                Dim stu As New STU_COLLINFO

                If Me.rdoIoGbnO.Checked Then
                    stu.IOGBN = "O"
                    stu.DEPTCD = Me.cboDptOrWard.Text.Split("|"c)(1)
                    stu.WARDCD = ""
                Else
                    stu.IOGBN = "I"
                    stu.DEPTCD = ""
                    stu.WARDCD = Me.cboDptOrWard.Text.Split("|"c)(1)
                End If

                If rdoAll.Checked Then
                    stu.SPCFLG1 = "0"
                    stu.SPCFLG2 = "4"
                ElseIf rdoColl.Checked Then
                    stu.SPCFLG1 = "1"
                    stu.SPCFLG2 = "4"
                Else
                    stu.SPCFLG1 = "0"
                    stu.SPCFLG2 = "0"
                End If

                stu.REGNO = alList.Item(0).ToString.Split("|"c)(0)
                stu.ORDDT1 = Me.dtpDateS.Text.Replace("-", "")
                stu.ORDDT2 = Me.dtpDateE.Text.Replace("-", "")
                stu.IOGBN = "O"
                stu.DEPTCD = Ctrl.Get_Code(Me.cboPartGbn)

                Me.axCollList.Clear()
                Me.axCollList.DisplayOrder(stu, Me.chkHopeDay.Checked)
            End If

            Me.txtPatNm.Text = ""
            Me.txtPatNm.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub pnlBottom_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlBottom.DoubleClick

        If pnlBottom.Tag Is Nothing Then
            axCollList.sbDisplay_Spread_HiddenYn(False)

            pnlBottom.Tag = "F"
        Else
            axCollList.sbDisplay_Spread_HiddenYn(True)

            pnlBottom.Tag = Nothing

        End If
    End Sub

    Private Sub btnPrint_Set_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Set.Click
        Dim sFn As String = "Handles btnPrint_Set.Click("

        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC(msFormNm, Me.chkBarInit.Checked)

        Try
            objFrm.ShowDialog()
            lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnPrintBC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_BC.Click
        Dim sFn As String = "Handles btnPrint_Label.Click"

        If lblBarPrinter.Text.Replace("사용안함", "") = "" Then Return

        Try
            Me.Cursor = Cursors.WaitCursor

            Me.axCollList.PatInfo = Me.axPatInfo.PatInfo

            Dim al_Return As New ArrayList

            al_Return = Me.axCollList.LebelPrint()

            If Not al_Return Is Nothing Then
                '수납 안된 환자 
                Me.axCollBcNos.PrintBarcode(al_Return, msFormNm, Me.lblBarPrinter.Text, False)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.txtRegNo.Focus()
            Me.txtRegNo.SelectAll()

            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnReg_Coll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_Coll.Click
        Dim sFn As String = "Handles btnReg_Coll.ButtonClick"

        Dim frm As New FGC01_S01(CType(IIf(PRG_CONST.BUTTON_COLL_TAKEYN_COLDT = "1", True, False), Boolean))

        frm.ShowDialog()

    End Sub

    Private Sub lblOrder_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrder.DoubleClick

        If lblOrder.Tag Is Nothing Then
            btnClear.Visible = True
            btnExit.Visible = True

            If rdoNoColl.Checked Then
                'btnPrint_Label.Visible = True
                btnReg.Visible = True
            Else
                btnPrint_BC.Visible = True
                'btnPrint_Doc.Visible = True
            End If

            btnClear.Visible = False
            btnExit.Visible = False
            'btnPrint_Label.Visible = False
            btnReg.Visible = False
            btnPrint_BC.Visible = False
            'btnPrint_Doc.Visible = False

            lblOrder.Tag = "imagebutton"
        Else
            btnClear.Visible = True
            btnExit.Visible = True

            If rdoNoColl.Checked Then
                'btnPrint_Label.Visible = True
                btnReg.Visible = True
            Else
                btnPrint_BC.Visible = True
                'btnPrint_Doc.Visible = True
            End If

            btnClear.Visible = False
            btnExit.Visible = False
            'btnPrint_Label.Visible = False
            btnReg.Visible = False
            btnPrint_BC.Visible = False
            'btnPrint_Doc.Visible = False

            lblOrder.Tag = Nothing
        End If
    End Sub


    Private Sub btnPrint_pat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_pat.Click

        Me.axCollList.Print_CollList(True)

    End Sub

    Private Sub rdoIoGbn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoIoGbnI.CheckedChanged, rdoIoGbnO.CheckedChanged

        If rdoIoGbnI.Checked Then
            Me.lblDptOrWard.Text = "병    동"
            sbDisplay_Ward()
        Else
            Me.lblDptOrWard.Text = "진 료 과"
            sbDisplay_Dept()
        End If

        If msDeptOrWard <> "" Then
            For ix As Integer = 0 To cboDptOrWard.Items.Count - 1
                Dim sBuf() As String = Me.cboDptOrWard.Items(ix).ToString.Split("|"c)
                If sBuf(1) = USER_INFO.N_WARDorDEPT Then
                    Me.cboDptOrWard.SelectedIndex = ix
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub axCollList_ChangedRow(ByVal cpi As COMMON.SVar.STU_PatInfo) Handles axCollList.ChangedRow
        Me.axPatInfo.DisplayPatInfo(cpi)
    End Sub
End Class