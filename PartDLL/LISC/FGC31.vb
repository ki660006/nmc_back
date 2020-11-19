'>>> 외래 채혈
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LOGIN

Public Class FGC31
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGC31.vb, Class : FGC31" & vbTab
    Private Const msXMLDir As String = "\XML"
    Private msAutoChecked As String = Application.StartupPath + msXMLDir & "\FGC01_AUTOCHECKED.XML"
    Private msFormNm As String = "FGC31"
    Private msIoGbn As String = "O"
    Private msDeptOrWard As String = ""
    Private msRegNo As String = ""
    Private mbCall As Boolean = False
    Private mbOcsCall As Boolean = False
    Private LoginPopWin As New LoginPopWin

    Public Sub sbDisplay_PatList(Optional ByVal rsRegNo As String = "")
        Dim sFn As String = "sbDisplay_PatList"

        Try
            sbClear_Form()

            Dim stu As New STU_COLLINFO

            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim cpi As New STU_PatInfo

            If rdoColl.Checked Then
                stu.SPCFLG1 = "1"
                stu.SPCFLG2 = "4"
            ElseIf rdoNoColl.Checked Then
                stu.SPCFLG1 = "0"
                stu.SPCFLG2 = "0"
            Else
                stu.SPCFLG1 = "0"
                stu.SPCFLG2 = "4"
            End If

            stu.REGNO = rsRegNo
            stu.IOGBN = "O"
            stu.ORDDT1 = Me.dtpDateS.Text.Replace("-", "")
            stu.ORDDT2 = Me.dtpDateE.Text.Replace("-", "")

            If Me.cboDeptCd.Text.IndexOf("|"c) >= 0 Then
                stu.DEPTCD = Me.cboDeptCd.Text.Split("|"c)(1)
            Else
                stu.DEPTCD = ""
            End If

            stu.WARDCD = ""
            stu.PARTGBN = Ctrl.Get_Code(Me.cboPartGbn)

            Dim dt As DataTable = (New WEBSERVER.CGWEB_C).fnGet_PatList(stu)

            If dt.Rows.Count < 1 Then
                If rsRegNo <> "" Then
                    axCollList.sbDisplay_NoOrder(rsRegNo, dtpDateS.Text, dtpDateE.Text)
                End If
                Return
            End If

            With spdList
                .ReDraw = False

                Dim sRegNo As String = ""

                For ix As Integer = 0 To dt.Rows.Count - 1

                    If sRegNo <> dt.Rows(ix).Item("regno").ToString Then

                        cpi = OCSAPP.OcsLink.Ord.fnSet_PatInfo(dt.Rows(ix), dtSysDate)

                        .MaxRows += 1
                        .Row = .MaxRows

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
                    End If

                    sRegNo = dt.Rows(ix).Item("regno").ToString
                Next

                .ReDraw = True
                Me.lblPatCount.Text = ">> 대상환자 건수 : " + .MaxRows.ToString + " 건"

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
            'Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList()
            Dim dt As DataTable = (New WEBSERVER.CGWEB_C).fnGet_DeptList()

            Me.cboDeptCd.Items.Clear()
            Me.cboDeptCd.Items.Add("전체" + Space(200) + "|")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboDeptCd.Items.Add(dt.Rows(ix).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(ix).Item("deptcd").ToString)

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
        Me.btnHidden_list_Click(Nothing, Nothing)

    End Sub

    Public Sub New(ByVal rsIoGbn As String, ByVal rsDptOrWard As String, ByVal rsRegNo As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        msIoGbn = rsIoGbn
        msDeptOrWard = rsDptOrWard
        msRegNo = rsRegNo

        mbCall = True

    End Sub


    Private Sub FGC01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Me.chkHopeDay.Checked Then
            COMMON.CommXML.setOneElementXML(msXMLDir, msAutoChecked, "CHECKEDINFO", "1^" + Me.cboCheckedDay.Text)
        Else
            COMMON.CommXML.setOneElementXML(msXMLDir, msAutoChecked, "CHECKEDINFO", "0^" + Me.cboCheckedDay.Text)
        End If

        If mbOcsCall = False Then MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGC01_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If mbCall Then Windows.Forms.Application.Exit()
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

        If mbCall Then
            Me.dtpDateS.Value = DateAdd(DateInterval.Day, -3, Now)
        Else
            Me.dtpDateS.Value = DateAdd(DateInterval.Year, -1, Now)
        End If

        Me.dtpDateE.Value = Now
        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

        Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        sbDisplay_Dept()

        btnClear_Click(Nothing, Nothing)

        Me.axCollList.CallForm = AxAckCollector.enumCollectCallForm.CollectOut
        Me.axCollList.SearchMode = False
        Me.axCollList.CollUsrId = USER_INFO.USRID
        Me.axCollList.Form = Me
        Me.axCollList.Clear()
        Me.axPatInfo.Form = Me

        '<<<20180821 응급 프린트 배포전 잠시 막아둠 
        'Me.axCollList.spdOrdList.Col = Me.axCollList.spdOrdList.GetColFromID("erprtyn") : Me.axCollList.spdOrdList.ColHidden = False

        Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msAutoChecked, "CHECKEDINFO")
        If sTmp <> "" Then
            If sTmp.Split("^"c)(0) = "1" Then
                Me.chkHopeDay.Checked = True
                Me.cboCheckedDay.Text = sTmp.Split("^"c)(1)
            End If
        End If

        Me.cboPartGbn.SelectedIndex = 0

        If msRegNo <> "" Then
            Me.txtRegNo.Text = msRegNo
            Me.txtRegNo_KeyDown(txtRegNo, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
        End If

    End Sub

    Private Sub rdoAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAll.Click, rdoColl.Click, rdoNoColl.Click
        Select Case CType(sender, Windows.Forms.RadioButton).Name.ToUpper
            Case "RDONOCOLL"
                btnPrint_BC.Visible = False
                btnPrint_Doc.Visible = False

                btnReg.Visible = True
                btnPrint_Label.Visible = True

                axCollList.SearchMode = False
                btnOrdSum.Enabled = True
                'btnCancel_coll.Enabled = False

            Case Else
                btnPrint_BC.Visible = True
                btnPrint_Doc.Visible = True

                btnReg.Visible = False
                btnPrint_Label.Visible = False

                axCollList.SearchMode = True
                btnOrdSum.Enabled = False
                'btnCancel_coll.Enabled = True
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


        Dim stu As New STU_COLLINFO

        With spdList
            .Row = e.row
            .Col = .GetColFromID("regno") : stu.REGNO = .Text
        End With

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

        stu.ORDDT1 = Me.dtpDateS.Text.Replace("-", "")
        stu.ORDDT2 = Me.dtpDateE.Text.Replace("-", "")
        stu.IOGBN = "O"
        stu.DEPTCD = Ctrl.Get_Code(Me.cboPartGbn)

        Me.axCollList.Clear()
        Me.axPatInfo.Clear()

        Me.axCollList.DisplayOrder(stu, Me.chkHopeDay.Checked)

       
        Dim r_cpi As STU_PatInfo = axCollList.PatInfo
        Me.axPatInfo.DisplayPatInfo(r_cpi)
        Me.txtRegNo.Text = stu.REGNO

    End Sub



    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        If Me.btnHidden_list.Text = "리스트   보  기" Then Return

        If DateDiff(DateInterval.Day, Me.dtpDateS.Value, Me.dtpDateE.Value) > 7 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "날짜 구간이 7일이 넘었습니다.!!" + vbCrLf + "날짜 구간을 재설정 하세요.!!")
            Return
        End If

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

            Me.axCollList.PatInfo = Me.axPatInfo.PatInfo

            Dim al_NoSunab As ArrayList = Nothing
            Dim al_Return As New ArrayList
            Dim bToColl As Boolean = False
            Dim sDeptCd As String = ""

            If Me.cboDeptCd.Text.IndexOf("|"c) >= 0 Then sDeptCd = Me.cboDeptCd.Text.Split("|"c)(1)

            If btnReg.Text.StartsWith("채혈") Then bToColl = True

            al_Return = Me.axCollList.CollectSelOrder_Web(al_NoSunab, Me.Name, Me.axPatInfo.RegNo, "O", sDeptCd, Ctrl.Get_Code(Me.cboPartGbn), _
                                                          Me.dtpDateS.Text, Me.dtpDateE.Text, bToColl, _
                                                          Me.chkAutoTk.Checked, CType(IIf(lblBarPrinter.Text.Replace("사용안함", "") = "", False, True), Boolean))

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

                    If al_bcno_poctyn.Count > 0 Then btnReg_Rst_Click(Nothing, Nothing, al_bcno_poctyn)


                    Me.axCollList.Clear()
                    Me.txtRegNo_KeyDown(txtRegNo, New System.Windows.Forms.KeyEventArgs(Keys.Enter))

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
            If bRet Then
                spdList_ClickEvent(Me.spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, spdList.ActiveRow))
                'If bRet Then btnQuery_Click(Nothing, Nothing)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnReg_Rst_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal r_al_bcno As ArrayList = Nothing) Handles btnReg_rst.Click
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

        If e.KeyCode <> Keys.Enter Or txtRegNo.Text = "" Then Return

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

    Private Sub txtPatNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatNm.KeyDown, txtIDL.KeyDown, txtIDR.KeyDown
        Dim sFn As String = "Handles txtPatNm.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If CType(sender, Windows.Forms.TextBox).Text = "" Then Return

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point
            Dim objHelp As New CDHELP.FGCDHELP01

            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_byNm(Me.txtPatNm.Text, Me.txtIDL.Text, Me.txtIDR.Text)
            'Dim dt As DataTable = (New WEBSERVER.CGWEB_C).fnGet_PatInfo_ByNm(Me.txtPatNm.Text, Me.txtIDL.Text, Me.txtIDR.Text)

            objHelp.FormText = "환자정보"
            objHelp.MaxRows = 10
            objHelp.OnRowReturnYN = True

            objHelp.AddField("regno", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("patnm", "환자명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sex", "성별", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("idno", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("wardroom", "병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtPatNm)

            Dim alList As ArrayList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - txtPatNm.Left, pntFrmXY.Y + pntCtlXY.Y + txtPatNm.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtRegNo.Text = alList.Item(0).ToString.Split("|"c)(0)

                sbDisplay_PatList(alList.Item(0).ToString.Split("|"c)(0))

                CType(sender, Windows.Forms.TextBox).Text = ""

                If CType(sender, Windows.Forms.TextBox).Name.StartsWith("txtID") Then
                    Me.txtIDL.Text = ""
                    Me.txtIDR.Text = ""
                End If

                Me.txtRegNo.Focus()
            Else
                CType(sender, Windows.Forms.TextBox).Focus()
            End If

            If CType(sender, Windows.Forms.TextBox).Name = "txtIDL" Then txtIDR.Focus()

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
            Me.lblDptWard.Visible = True

            pnlBottom.Tag = "F"
        Else
            axCollList.sbDisplay_Spread_HiddenYn(True)
            Me.lblDptWard.Visible = False

            pnlBottom.Tag = Nothing

        End If
    End Sub

    Private Sub btnPrint_Set_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Set.Click
        Dim sFn As String = "Handles btnPrintSet.Click"

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

    Private Sub btnPrint_BC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_BC.Click
        Dim sFn As String = "Handles btnPrint_BC.Click"

        If Me.lblBarPrinter.Text.Replace("사용안함", "") = "" Then Return

        Try
            Me.Cursor = Cursors.WaitCursor

            'Me.axCollList.PatInfo = Me.axPatInfo.PatInfo

            'Dim al_Return As New ArrayList

            'al_Return = Me.axCollList.LebelPrint()

            'If Not al_Return Is Nothing Then
            '    '수납 안된 환자 
            '    Me.axCollBcNos.PrintBarcode(al_Return, msFormNm, Me.lblBarPrinter.Text, False)
            'End If

            Me.axCollList.Print_barcode(msFormNm)

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

            Dim objFrm As New FGC01_S02

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

    Private Sub btnPrint_Doc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Doc.Click

        axCollList.Print_Document()

    End Sub

    Private Sub btnQuery_Unfit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery_Unfit.Click
        Dim sFn As String = "Handles btnQuery_Unfit.Click"

        Dim frmChild As Windows.Forms.Form
        frmChild = New FGC31_S03("O", USER_INFO.N_WARDorDEPT)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()

    End Sub


    Private Sub btnHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory.Click
        Dim sFn As String = "Handles btnResult.ButtonClick"

        Dim frmChild As Windows.Forms.Form
        frmChild = New FGC31_S02(Me.axPatInfo.RegNo, Me.dtpDateS.Text, Me.dtpDateE.Text)

        Me.AddOwnedForm(frmChild)
        frmChild.WindowState = FormWindowState.Normal
        frmChild.Activate()
        frmChild.Show()
    End Sub

    Private Sub chkHopeDay_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHopeDay.CheckedChanged

        Me.axCollList.AllCheckMode = Not Me.chkHopeDay.Checked

        If Me.chkHopeDay.Checked Then
            Me.cboCheckedDay.Visible = True
            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msAutoChecked, "CHECKEDINFO")
            If sTmp <> "" Then
                Me.cboCheckedDay.Text = sTmp.Split("^"c)(1)
                If Me.cboCheckedDay.Text = "" Then Me.cboCheckedDay.Text = "0"
            End If
        Else
            Me.cboCheckedDay.Visible = False
            COMMON.CommXML.setOneElementXML(msXMLDir, msAutoChecked, "CHECKEDINFO", "0^" + Me.cboCheckedDay.Text)
        End If

    End Sub

    Private Sub cboCheckedDay_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCheckedDay.TextChanged
        If IsNumeric(Me.cboCheckedDay.Text) Then Me.axCollList.AutoCheckDay = Convert.ToInt16(Me.cboCheckedDay.Text)

        If Me.chkHopeDay.Checked Then
            COMMON.CommXML.setOneElementXML(msXMLDir, msAutoChecked, "CHECKEDINFO", "1^" + Me.cboCheckedDay.Text)
        End If
    End Sub

    Private Sub axCollList_ChangedRow(ByVal cpi As COMMON.SVar.STU_PatInfo) Handles axCollList.ChangedRow
        Me.axPatInfo.DisplayPatInfo(cpi)
    End Sub

    Private Sub axCollList_MsgPopup(ByVal rs_Msg As String) Handles axCollList.MsgPopup
        If rs_Msg <> "" Then CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, rs_Msg)
    End Sub

    Private Sub btnReg_liscmt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_liscmt.Click
        Dim sFn As String = "btnCollNo.ButtonClick"

        Try
            Me.axCollList.PatInfo = Me.axPatInfo.PatInfo

            Me.axCollList.CollUsrId = USER_INFO.USRID

            If MsgBox(Me.btnReg_liscmt.Text + "을 하시겠습니까?", MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Return

            Dim bReturn As Boolean = Me.axCollList.CommentSelOrder()

            If bReturn Then
                txtRegNo_KeyDown(txtRegNo, New System.Windows.Forms.KeyEventArgs(Keys.Enter))

                'btnQuery_Click(Nothing, Nothing)
            Else
                MsgBox(Me.btnReg_liscmt.Text + " 작업에 실패하였습니다!!", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub btnHidden_list_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidden_list.Click

        Me.txtRegNo.Text = ""
        Me.spdList.MaxRows = 0

        If Me.btnHidden_list.Text = "리스트   숨  김" Then
            Me.grpList.Visible = False
            Me.axCollList.Left = Me.grpList.Left
            Me.axCollList.Width += Me.grpList.Width + 10

            Me.axCollBcNos.Left = Me.grpList.Left - 2
            Me.axCollBcNos.Width += Me.grpList.Width + 10

            Me.lblOrderInfo.Left = Me.grpList.Left + 1
            Me.cboPartGbn.Left = Me.lblOrderInfo.Left + Me.lblOrderInfo.Width + 4

            Me.btnHidden_list.Text = "리스트   보  기"
            Me.btnQuery.Enabled = False

            Me.dtpDateS.Value = DateAdd(DateInterval.Year, -1, Now)
            Me.dtpDateE.Value = Now

        Else
            Me.grpList.Visible = True
            Me.axCollList.Left = 281
            Me.axCollList.Width -= Me.grpList.Width + 10

            Me.axCollBcNos.Left = 279
            Me.axCollBcNos.Width -= Me.grpList.Width + 10

            Me.lblOrderInfo.Left = 282
            Me.cboPartGbn.Left = 397

            Me.btnHidden_list.Text = "리스트   숨  김"
            Me.btnQuery.Enabled = True

            Me.dtpDateS.Value = DateAdd(DateInterval.Day, -3, Now)
            Me.dtpDateE.Value = Now

        End If

    End Sub

    Private Sub txtIDL_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtIDL.KeyUp
        If Me.txtIDL.Text.Length = Me.txtIDL.MaxLength And Me.txtIDL.SelectionStart = Me.txtIDL.MaxLength Then Me.txtIDR.Focus()
    End Sub

    Private Sub cboDeptCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDeptCd.SelectedIndexChanged

        Try
            Me.lblDptWard.Text = Me.cboDeptCd.Text.Split("|"c)(1)
        Catch ex As Exception

        End Try

    End Sub


    Private Sub axCollList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles axCollList.Load

    End Sub
End Class