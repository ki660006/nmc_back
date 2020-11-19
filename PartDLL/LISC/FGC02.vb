'>>> 병동 채혈
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LOGIN

Public Class FGC02
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGC02.vb, Class : FGC02" & vbTab

    Private msFormNm As String = "FGC02"
    Private msIoGbn As String = "I"
    Private msDeptOrWard As String = ""
    Private msRegNo As String = ""

    Private mbOcsCall As Boolean = False
    Private mbCall As Boolean = False

    Private LoginPopWin As New LoginPopWin

    Private Sub sbDisplay_PatList(Optional ByVal rsRegNo As String = "")
        Dim sFn As String = "sbDisplay_PatList"

        Try
            sbClear_Form()

            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim cpi As STU_PatInfo
            Dim sSpcFlgS As String = ""
            Dim sSpcFlgE As String = ""
            Dim sIoGbn As String = "I"

            If rdoIoGbnO.Checked Then sIoGbn = "O"

            If rdoColl.Checked Then
                sSpcFlgS = "1"
                sSpcFlgE = "4"
            ElseIf rdoNoColl.Checked Then
                sSpcFlgS = "0"
                sSpcFlgE = "0"
            Else
                sSpcFlgS = "0"
                sSpcFlgE = "4"
            End If

            Dim dt As DataTable = New DataTable
            Dim dr As DataRow()

            If rsRegNo = "" Then
                dt = OCSAPP.OcsLink.Ord.fnGet_Coll_PatList(sIoGbn, Ctrl.Get_Code(cboDptOrWard), dtpDateS.Text, dtpDateE.Text, sSpcFlgS, sSpcFlgE)

                If sIoGbn = "I" Then
                    dr = dt.Select("iogbn = 'I'", "patinfo")
                Else
                    dr = dt.Select("iogbn <> 'I'", "patinfo")
                End If
            Else
                dt = OCSAPP.OcsLink.Ord.fnGet_Coll_PatList_RegNo(rsRegNo, dtpDateS.Text, dtpDateE.Text, sSpcFlgS, sSpcFlgE)

                If sIoGbn = "I" Then
                    dr = dt.Select("iogbn = 'I' AND wardno = '" + Ctrl.Get_Code(cboDptOrWard) + "'", "patinfo")
                Else
                    If Ctrl.Get_Code(cboDptOrWard) = "" Then
                        dr = dt.Select("iogbn <> 'I'", "patinfo")
                    Else
                        dr = dt.Select("iogbn <> 'I' AND deptcd = '" + msDeptOrWard + "'", "patinfo")
                    End If
                End If
            End If

            dt = Fn.ChangeToDataTable(dr)

            lblPatCount.Text = ">> 대상환자 건수 : " + dt.Rows.Count.ToString + " 건"

            If dt.Rows.Count < 1 Then
                If rsRegNo <> "" Then axCollList.sbDisplay_NoOrder(rsRegNo, dtpDateS.Text, dtpDateE.Text)
                Return
            End If

            With spdList
                .ReDraw = False
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1
                    cpi = OCSAPP.OcsLink.Ord.fnSet_PatInfo(dt.Rows(ix), dtSysDate)

                    .Row = ix + 1
                    .Col = .GetColFromID("regno") : .Text = cpi.REGNO
                    .Col = .GetColFromID("patnm") : .Text = cpi.PATNM
                    .Col = .GetColFromID("sex") : .Text = cpi.SEX
                    .Col = .GetColFromID("age") : .Text = cpi.AGE

                    If dt.Rows(ix).Item("iogbn").ToString = "I" Then
                        .Col = .GetColFromID("etc") : .Text = cpi.WARD + "/" + cpi.ROOMNO
                    Else
                        .Col = .GetColFromID("etc") : .Text = cpi.DEPTCD
                    End If

                    'hidden col
                    .Col = .GetColFromID("iogbn") : .Text = dt.Rows(ix).Item("iogbn").ToString
                    .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("deptcd").ToString
                    .Col = .GetColFromID("wardno") : .Text = dt.Rows(ix).Item("wardno").ToString
                Next

                .ReDraw = True
            End With

            spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Ward()
        Dim sFn As String = "sbDisplay_Ward"

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList()

            Me.cboDptOrWard.Items.Clear()
            'cboDptOrWard.Items.Add("[ ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDptOrWard.Items.Add("[" + dt.Rows(ix).Item("wardno").ToString + "] " + dt.Rows(ix).Item("wardnm").ToString)
                If msDeptOrWard <> "" Then
                    If dt.Rows(ix).Item("wardno").ToString.Trim = msDeptOrWard Then
                        Me.cboDptOrWard.SelectedIndex = ix
                    End If
                End If
            Next

            If cboDptOrWard.Items.Count > 0 And msDeptOrWard = "" Then cboDptOrWard.SelectedIndex = 0

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

                If msDeptOrWard <> "" Then
                    If dt.Rows(ix).Item("deptcd").ToString.Trim = msDeptOrWard Then
                        Me.cboDptOrWard.SelectedIndex = ix
                    End If
                End If
            Next

            If Me.cboDptOrWard.Items.Count > 0 And msDeptOrWard = "" Then cboDptOrWard.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)

        End Try
    End Sub

    Private Sub sbClear_Form()

        Me.lblPatCount.Text = ">> 대상환자 건수 :"
        Me.axPatInfo.Clear()
        Me.axCollList.Clear()
        Me.spdList.MaxRows = 0

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        Me.WindowState = Windows.Forms.FormWindowState.Maximized


    End Sub

    Public Sub New(ByVal rsIoGbn As String, ByVal rsDptOrWard As String, ByVal rsRegNo As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        msIoGbn = rsIoGbn
        msDeptOrWard = rsDptOrWard
        msRegNo = rsRegNo
        mbCall = True

    End Sub

    Public Sub New(ByVal rsUseId As String, ByVal rsUsrNm As String, ByVal rsWardNo As String, ByVal rsRegNo As String, ByVal r_DbCn As OleDb.OleDbConnection, ByRef rsErrMsg As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        msIoGbn = "I"
        msDeptOrWard = rsWardNo
        msRegNo = rsRegNo

        mbCall = True
        mbOcsCall = True

        Try
            If r_DbCn Is Nothing Then
                LISAPP.APP_DB.DbFn.fnGet_DbConnect()
                rsErrMsg = "OK"
            Else
                rsErrMsg = LISAPP.APP_DB.DbFn.fnDb_Setting(r_DbCn)
            End If

            If rsErrMsg = "OK" Then
                Dim al_Args As New ArrayList

                al_Args.Add("WARD")
                al_Args.Add("C")
                al_Args.Add(rsUseId)
                al_Args.Add(rsUsrNm)
                al_Args.Add(rsWardNo)
                al_Args.Add(rsRegNo)

                If LoginPopWin.LogInDo(al_Args) Then

                Else
                    rsErrMsg = "OCS와 연동 오류로 화면을 표시할수 없습니다."
                    Me.Close()
                End If

            Else
                Me.Close()
            End If
        Catch ex As Exception
            rsErrMsg = ex.Message
            Me.Close()
        End Try

    End Sub

    Private Sub FGC01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If mbOcsCall = False Then MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGC02_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If mbCall Then Windows.Forms.Application.Exit()
    End Sub

    Private Sub FGC02_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If btnReg.Enabled Then btnReg_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGC020010_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DS_FormDesige.sbInti(Me)

        Me.btnReg.Text = PRG_CONST.BUTTON_COLL_WARD + "(F2)"

        Me.axCollList.SearchMode = False
        Me.axCollList.CollUsrId = USER_INFO.USRID
        Me.axCollList.Form = Me

        Me.dtpDateS.Value = DateAdd(DateInterval.Day, -1, Now)
        Me.dtpDateE.Value = Now

        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

        Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        If msIoGbn = "I" Then
            rdoIoGbnI.Checked = True
            sbDisplay_Ward()
        Else
            rdoIoGbnO.Checked = True
        End If

        btnClear_Click(Nothing, Nothing)

        If msRegNo <> "" Then
            Me.txtRegNo.Text = msRegNo
            txtRegNo_KeyDown(txtRegNo, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
        End If

    End Sub

    Private Sub rdoAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAll.Click, rdoColl.Click, rdoNoColl.Click
        Select Case CType(sender, Windows.Forms.RadioButton).Name.ToUpper
            Case "RDONOCOLL"

                If lblOrder.Tag Is Nothing Then
                    btnPrint_BC.Visible = False
                    btnPrint_Doc.Visible = False

                    btnReg.Visible = True
                    btnPrint_Label.Visible = True
                Else
                    btnPrint_BC.Visible = False
                    btnPrint_Doc.Visible = False

                    btnReg.Visible = True
                    btnPrint_Label.Visible = True
                End If

                axCollList.SearchMode = False
                btnOrdSum.Enabled = True
                'btnCancel_coll.Enabled = False

            Case Else
                If lblOrder.Tag Is Nothing Then
                    btnPrint_BC.Visible = True
                    btnPrint_Doc.Visible = True

                    btnReg.Visible = False
                    btnPrint_Label.Visible = False
                Else
                    btnPrint_BC.Visible = True
                    btnPrint_Doc.Visible = True

                    btnReg.Visible = False
                    btnPrint_Label.Visible = False
                End If

                axCollList.SearchMode = True
                btnOrdSum.Enabled = False
                'btnCancel_coll.Enabled = True
        End Select

        sbClear_Form()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Me.txtRegNo.Text = ""
        Me.txtPatNm.Text = ""

        sbClear_Form()

    End Sub

    Private Sub rdoIoGbnA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoIoGbnI.Click, rdoIoGbnO.Click

        If rdoIoGbnI.Checked Then
            Me.lblDptOrWard.Text = "병    동"
            sbDisplay_Ward()
        Else
            Me.lblDptOrWard.Text = "진 료 과"
            sbDisplay_Dept()
        End If

        If msDeptOrWard <> "" Then
            For ix As Integer = 0 To cboDptOrWard.Items.Count - 1
                If Me.cboDptOrWard.Items(ix).ToString.StartsWith("[" + msDeptOrWard + "]") Then
                    Me.cboDptOrWard.SelectedIndex = ix
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        If e.row < 1 Then Return

        Dim sRegNo As String = ""
        Dim sSpcFlgS As String = ""
        Dim sSpcFlgE As String = ""

        With spdList
            .Row = e.row
            .Col = .GetColFromID("regno") : sRegNo = .Text
        End With

        If rdoAll.Checked Then
            sSpcFlgS = "0"
            sSpcFlgE = "4"
        ElseIf rdoColl.Checked Then
            sSpcFlgS = "1"
            sSpcFlgE = "4"
        Else
            sSpcFlgS = "0"
            sSpcFlgE = "0"
        End If

        axCollList.Clear()
        axCollList.DisplayOrder(sRegNo, IIf(rdoIoGbnI.Checked, "I", "O").ToString, Ctrl.Get_Code(cboDptOrWard), dtpDateS.Text, dtpDateE.Text, sSpcFlgS, sSpcFlgE, False)

        Dim r_cpi As STU_PatInfo = axCollList.PatInfo
        axPatInfo.DisplayPatInfo(r_cpi)

    End Sub

    Private Sub FGC02010_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        axCollList.Clear()

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        sbDisplay_PatList()

    End Sub

    Private Sub btnOrdSum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOrdSum.Click

        If rdoNoColl.Checked = False Then Return

        Me.axCollList.MergeOrder()
        'axCollList.DisplayOrder_bcsum(axPatInfo.RegNo, IIf(rdoIoGbnI.Checked, "I", "O").ToString, Ctrl.Get_Code(cboDptOrWard), dtpDateS.Text, dtpDateE.Text)

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

            If rdoIoGbnO.Checked Then sIoGbn = "O"


            al_Return = Me.axCollList.CollectSelOrder(al_NoSunab, Me.Name, Me.axPatInfo.RegNo, sIoGbn, Ctrl.Get_Code(cboDptOrWard), dtpDateS.Text, dtpDateE.Text, bToColl, False, CType(IIf(lblBarPrinter.Text.Replace("사용안함", "") = "", False, True), Boolean))

            'If al_NoSunab IsNot Nothing Then
            '    '수납 안된 환자 
            '    Me.axCollBcNos.PrintBarcode_NotSuNab(al_NoSunab, Me.Name)

            '    btnQuery_Click(Nothing, Nothing)
            'End If

            Dim al_bcno_poctyn As New ArrayList

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

                        If listcollData.Item(0).POCTYN = "1" Then
                            al_bcno_poctyn.Add(listcollData.Item(0).BCNO)
                        End If
                    Next

                    Me.axCollBcNos.txtBcNos.Text = sBcNos.Trim()

                    'If al_bcno_poctyn.Count > 0 Then btnReg_rst_Click(Nothing, Nothing, al_bcno_poctyn)

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
            txtRegNo.Text = txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            sbDisplay_PatList(txtRegNo.Text)

            txtRegNo.Text = ""
            txtRegNo.Focus()

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

            txtPatNm.Text = ""
            txtPatNm.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnPrintLabel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Label.Click
        Dim sFn As String = "Handles btnPrint_Label.Click"

        Try
            Me.Cursor = Cursors.WaitCursor

            Me.axCollList.PatInfo = Me.axPatInfo.PatInfo

            Dim al_Return As New ArrayList

            al_Return = Me.axCollList.LebelPrint()

            If Not al_Return Is Nothing Then
                '수납 안된 환자 
                Me.axCollBcNos.PrintBarcode(al_Return, msFormNm, Me.lblBarPrinter.Text, True)
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

        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC(msFormNm)

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

        Dim frm As New FGC01_S01

        frm.ShowDialog()

    End Sub

    Private Sub btnReg_Pat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_Pat.Click
        Dim sFn As String = "Handles btnReg_Pat.ButtonClick"

        Try
            If Me.axPatInfo.RegNo.ToString() = "" Then
                MsgBox("환자 조회 후 사용 하시기 바랍니다.!", MsgBoxStyle.Information, btnReg_Pat.Text)
                Return
            End If

            Dim objFrm As New POPUPWIN.FPOPUPCOMMENT

            '< CmtGbn 2 : 환자 특이사항(진상환자) 
            '         3 : 미채혈 사유 
            With objFrm
                .Init()
                .RegNo = Me.axPatInfo.RegNo
                .CmtGbn = 2
                .IOGBN = "I"
                .Title = "환자 특이사항 등록"

                .sbLoad()
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnPrintDoc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Doc.Click

        axCollList.Print_Document()

    End Sub

    Private Sub btnQuery_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery_Unfit.Click
        Dim sFn As String = "Handles btnQuery_Unfit.Click"

        Dim frmChild As Windows.Forms.Form
        frmChild = New LISS.FGS07("O", USER_INFO.N_WARDorDEPT)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()
    End Sub

    Private Sub lblOrder_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblOrder.DoubleClick

        If lblOrder.Tag Is Nothing Then
            btnClear.Visible = True
            btnExit.Visible = True
            btnQuery_Unfit.Visible = True
            btnReg_Pat.Visible = True

            If rdoNoColl.Checked Then
                btnPrint_Label.Visible = True
                btnReg.Visible = True
            Else
                btnPrint_BC.Visible = True
                btnPrint_Doc.Visible = True
            End If

            btnClear.Visible = False
            btnExit.Visible = False
            btnQuery_Unfit.Visible = False
            btnReg_Pat.Visible = False
            btnPrint_Label.Visible = False
            btnReg.Visible = False
            btnPrint_BC.Visible = False
            btnPrint_Doc.Visible = False

            lblOrder.Tag = "imagebutton"
        Else
            btnClear.Visible = True
            btnExit.Visible = True
            btnQuery_Unfit.Visible = True
            btnReg_Pat.Visible = True

            If rdoNoColl.Checked Then
                btnPrint_Label.Visible = True
                btnReg.Visible = True
            Else
                btnPrint_BC.Visible = True
                btnPrint_Doc.Visible = True
            End If

            btnClear.Visible = False
            btnExit.Visible = False
            btnQuery_Unfit.Visible = False
            btnReg_Pat.Visible = False
            btnPrint_Label.Visible = False
            btnReg.Visible = False
            btnPrint_BC.Visible = False
            btnPrint_Doc.Visible = False

            lblOrder.Tag = Nothing
        End If
    End Sub

    Private Sub btnHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Dim sFn As String = "Handles btnResult.ButtonClick"

        Dim frmChild As Windows.Forms.Form
        frmChild = New LISS.FGS06(Me.axPatInfo.RegNo, Me.dtpDateS.Text, Me.dtpDateE.Text)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()
    End Sub

    Private Sub btnReg_rst_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal r_al_bcno As ArrayList = Nothing) Handles btnReg_rst.Click
        Dim sFn As String = "Handles btnResult.btnReg_rst"

        Dim frmChild As Windows.Forms.Form

        If r_al_bcno Is Nothing Then
            If Me.axPatInfo.Ward = "" Then
                frmChild = New LISR.FGR05("O", Me.axPatInfo.DeptCd, Me.axPatInfo.RegNo, Me.axPatInfo.OrdDt.Substring(0, 10))
            Else
                frmChild = New LISR.FGR05("I", Me.axPatInfo.Ward, Me.axPatInfo.RegNo, Me.axPatInfo.OrdDt.Substring(0, 10))
            End If
        Else
            frmChild = New LISR.FGR05(IIf(Me.axPatInfo.Ward <> "", "I", "O").ToString, r_al_bcno)
        End If

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()

    End Sub
End Class