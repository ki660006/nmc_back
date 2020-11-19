'>>> 외래 채혈
Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_C.C01
Imports LOGIN

Imports System.Windows.Forms
Imports System.Drawing

Public Class FGC21
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGC01.vb, Class : FGC01" & vbTab

    Private msFormNm As String = "FGC21"
    Private msIoGbn As String = "O"
    Private msDeptOrWard As String = ""
    Private mbOcsCall As Boolean = False

    Private LoginPopWin As New LoginPopWin

    Public Sub sbDisplay_PatList(Optional ByVal rsRegNo As String = "")
        Dim sFn As String = "sbDisplay_PatList"

        Try
            sbClear_Form()

            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim cpi As STU_PatInfo
            Dim sSpcFlgS As String = ""
            Dim sSpcFlgE As String = ""

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
                dt = PISAPP.DPIS01.OcsLink.Ord.fnGet_Coll_PatList("O", Ctrl.Get_Code(cboDeptCd), dtpDateS.Text, dtpDateE.Text, sSpcFlgS, sSpcFlgE)

                dr = dt.Select("iogbn <> 'I'", "patinfo")
            Else
                dt = PISAPP.DPIS01.OcsLink.Ord.fnGet_Coll_PatList_RegNo(rsRegNo, dtpDateS.Text, dtpDateE.Text, sSpcFlgS, sSpcFlgE)

                If Ctrl.Get_Code(cboDeptCd) = "" Then
                    dr = dt.Select("iogbn <> 'I'", "patinfo")
                Else
                    dr = dt.Select("iogbn <> 'I' AND deptcd = '" + msDeptOrWard + "'", "patinfo")
                End If
            End If

            dt = Fn.ChangeToDataTable(dr)

            Me.lblPatCount.Text = ">> 대상환자 건수 : " + dt.Rows.Count.ToString + " 건"

            If dt.Rows.Count < 1 Then
                If rsRegNo <> "" Then
                    axCollList.sbDisplay_NoOrder(rsRegNo, dtpDateS.Text, dtpDateE.Text)
                End If
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

    Private Sub sbDisplay_Dept()
        Dim sFn As String = "sbDisplay_Dept"

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList()

            Me.cboDeptCd.Items.Clear()
            Me.cboDeptCd.Items.Add("[ ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDeptCd.Items.Add("[" + dt.Rows(ix).Item("deptcd").ToString + "] " + dt.Rows(ix).Item("deptnm").ToString)

                If msDeptOrWard = dt.Rows(ix).Item("deptcd").ToString.Trim Then Me.cboDeptCd.SelectedIndex = ix + 1

            Next

            If cboDeptCd.Items.Count > 0 And msDeptOrWard = "" Then cboDeptCd.SelectedIndex = 0

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

    Public Sub New(ByVal rsIoGbn As String, ByVal rsDptOrWard As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        msIoGbn = rsIoGbn
        msDeptOrWard = rsDptOrWard

    End Sub

    Public Sub New(ByVal rsUseId As String, ByVal rsUsrNm As String, ByVal rsDeptCd As String, ByVal rsRegNo As String, ByVal r_DbCn As OleDb.OleDbConnection, ByRef rsErrMsg As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        msIoGbn = "O"
        msDeptOrWard = rsDeptCd
        mbOcsCall = True

        Try

            'If SF_COLL.fnAutoUpDate("C01", Application.StartupPath, Application.StartupPath + "\C01.DLL") Then
            '    Me.Close()
            '    Return
            'End If

            If r_DbCn Is Nothing Then
                LISAPP.APP_DB.DBfn.fnGet_DbConnect()
                rsErrMsg = "OK"
            Else
                rsErrMsg = LISAPP.APP_DB.DBfn.fnDb_Setting(r_DbCn)
            End If

            If rsErrMsg = "OK" Then
                Dim al_Args As New ArrayList

                al_Args.Add("OUT")
                al_Args.Add("C")
                al_Args.Add(rsUseId)
                al_Args.Add(rsUsrNm)
                al_Args.Add(rsDeptCd)
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

    Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.F2
                If btnReg.Enabled Then btnReg_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGC01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DS_FormDesige.sbInti(Me)

        Me.chkAutoTk.Visible = CType(IIf(PRG_CONST.BUTTON_COLL_TAKEYN = "1", True, False), Boolean)
        Me.chkAutoTk.Checked = CType(IIf(PRG_CONST.BUTTON_COLL_TAKEYN = "1", True, False), Boolean)

        Me.dtpDateS.Value = DateAdd(DateInterval.Year, -1, Now)
        Me.dtpDateE.Value = Now
        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

        Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        sbDisplay_Dept()

        btnClear_Click(Nothing, Nothing)

        Me.axCollList.SearchMode = False
        Me.axCollList.CollUsrId = USER_INFO.USRID
        Me.axCollList.Form = Me

    End Sub

    Private Sub rdoAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAll.Click, rdoColl.Click, rdoNoColl.Click
        Select Case CType(sender, Windows.Forms.RadioButton).Name.ToUpper
            Case "RDONOCOLL"
                btnPrint_BC.Visible = False
                btnPrint_Doc.Visible = False

                btnReg.Visible = True
                btnPrint_Label.Visible = True

                axCollList.SearchMode = False

            Case Else
                btnPrint_BC.Visible = True
                btnPrint_Doc.Visible = True

                btnReg.Visible = False
                btnPrint_Label.Visible = False

                axCollList.SearchMode = True
        End Select

        sbClear_Form()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        txtRegNo.Text = ""
        txtPatNm.Text = ""
        txtIDL.Text = "" : txtIDR.Text = ""

        sbClear_Form()

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

        Me.axCollList.Clear()
        Me.axCollList.DisplayOrder(sRegNo, "O", Ctrl.Get_Code(cboDeptCd), dtpDateS.Text, dtpDateE.Text, sSpcFlgS, sSpcFlgE, chkHopeDay.Checked)

        Dim r_cpi As STU_PatInfo = axCollList.PatInfo
        Me.axPatInfo.DisplayPatInfo(r_cpi)

    End Sub

    Private Sub FGC01_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        axCollList.Clear()

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        sbDisplay_PatList()

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
            Dim bToColl As Boolean = False

            If btnReg.Text.StartsWith("채혈") Then bToColl = True

            al_Return = Me.axCollList.CollectSelOrder(al_NoSunab, Me.Name, Me.axPatInfo.RegNo, "O", Ctrl.Get_Code(cboDeptCd), dtpDateS.Text, dtpDateE.Text, bToColl, Me.chkAutoTk.Checked, CType(IIf(lblBarPrinter.Text.Replace("사용안함", "") = "", False, True), Boolean))

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

                    Me.axCollList.Clear()

                    'btnQuery_Click(Nothing, Nothing)
                Else
                    Me.axCollBcNos.Clear()
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

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
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.GotFocus, txtPatNm.GotFocus, txtIDL.GotFocus, txtIDR.GotFocus
        With CType(sender, Windows.Forms.TextBox)
            .SelectionStart = 0
            .SelectAll()
        End With
    End Sub

    Private Sub TextBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRegNo.Click, txtPatNm.Click, txtIDL.Click, txtIDR.Click

        CType(sender, System.Windows.Forms.TextBox).SelectAll()

    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown
        Dim sFn As String = "Handles txtRegNo.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            If IsNumeric(Me.txtRegNo.Text.Substring(0, 1)) Then
                Me.txtRegNo.Text = Me.txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                Me.txtRegNo.Text = Me.txtRegNo.Text.Substring(0, 1) + Me.txtRegNo.Text.Substring(PRG_CONST.Len_RegNo - 1).PadLeft(9, "0"c)
            End If

            sbDisplay_PatList(Me.txtRegNo.Text)

            Me.txtRegNo.Text = ""
            Me.txtRegNo.Focus()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtPatNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatNm.KeyDown, txtIDL.KeyDown, txtIDR.KeyDown
        Dim sFn As String = "Handles txtPatNm.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_byNm(Me.txtPatNm.Text, Me.txtIDL.Text, Me.txtIDR.Text)

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
            Fn.ExclamationErrMsg(Err, Me.Text)
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
            Fn.ExclamationErrMsg(Err, Me.Text)

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
        Dim sFn As String = "Handles btnPrintSet.Click"

        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC(msFormNm)

        Try
            objFrm.ShowDialog()
            lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub btnPrint_BC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_BC.Click
        Dim sFn As String = "Handles btnPrint_BC.Click"

        If lblBarPrinter.Text.Replace("사용안함", "") = "" Then Return

        Try
            Me.Cursor = Cursors.WaitCursor

            Me.axCollList.PatInfo = Me.axPatInfo.PatInfo

            Dim al_Return As New ArrayList

            al_Return = Me.axCollList.LebelPrint()

            If Not al_Return Is Nothing Then
                '수납 안된 환자 
                Me.axCollBcNos.PrintBarcode_pis(al_Return, msFormNm, Me.lblBarPrinter.Text, False)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

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
            Fn.ExclamationErrMsg(Err, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Doc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Doc.Click

        axCollList.Print_Document()

    End Sub

    Private Sub btnQuery_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery_Unfit.Click
        Dim sFn As String = "Handles btnQuery_Unfit.Click"

        Dim frmChild As Windows.Forms.Form
        frmChild = New S01.FGS07("O", USER_INFO.N_WARDorDEPT)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()

    End Sub


    Private Sub txtIDR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtIDR.KeyDown, txtIDR.KeyDown
        Dim sFn As String = "Handles txtPatNm.KeyDown"
        If e.KeyCode <> Keys.Enter Or Me.txtIDL.Text.Length + Me.txtIDR.Text.Length = 0 Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_ByIDNO(Me.txtIDL.Text, Me.txtIDR.Text)

            objHelp.FormText = "환자정보"
            objHelp.OnRowReturnYN = True

            objHelp.AddField("bunho", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("suname", "환자명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sex", "성별", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sujumin", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("wardroom", "병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(CType(sender, Windows.Forms.TextBox))

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - CType(sender, Windows.Forms.TextBox).Left, pntFrmXY.Y + pntCtlXY.Y + CType(sender, Windows.Forms.TextBox).Height + 80, dt)

            If aryList.Count > 0 Then
                sbDisplay_PatList(aryList.Item(0).ToString.Split("|"c)(0))
            End If

            CType(sender, Windows.Forms.TextBox).Text = ""
            CType(sender, Windows.Forms.TextBox).Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)
        End Try
    End Sub
End Class