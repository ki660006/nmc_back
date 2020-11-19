Imports System
Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN

Public Class FGR06
    Private Const msFile As String = "File : FGR06.vb, Class : FGR06" + vbTab

    Private Const msXMLDir As String = "\XML"
    Private msTestFile As String = Application.StartupPath + msXMLDir + "\FGR04_TEST.XML"
    Private msWkGrpFile As String = Application.StartupPath + msXMLDir + "\FGR04_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath + msXMLDir + "\FGR04_TGRP.XML"
    Private msSlipFile As String = Application.StartupPath + msXMLDir + "\FGR04_SLIP.XML"
    Private msQryFile As String = Application.StartupPath + msXMLDir + "\FGR04_Qry.XML"
    Private msTermFile As String = Application.StartupPath + msXMLDir + "\FGR04_Term.XML"

    Private Sub sbDisp_Init()
        Dim sFn As String = "Sub sbDisp_Init()"

        Try
            STU_AUTHORITY.usrid = USER_INFO.USRID

            Me.axResult.Form = Me
            Me.axResult.ColHiddenYn = True
            Me.axResult.sbDisplay_Init("ALL")

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
            Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")

            sbDisplay_Slip()    '-- 검사분야 
            sbDisplay_TGrp()    '-- 검사그룹

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = "", sTerm As String = "", sTestCds As String = ""

            sTgrp = COMMON.CommXML.getOneElementXML(msXMLDir, msTgrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXMLDir, msWkGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXMLDir, msQryFile, "JOB")
            sTerm = COMMON.CommXML.getOneElementXML(msXMLDir, msTermFile, "TERM")
            sTestCds = COMMON.CommXML.getOneElementXML(msXMLDir, msTestFile, "TEST")

            If Me.cboTGrp.Items.Count > 0 Then
                If sTgrp = "" Or Val(sTgrp) > Me.cboTGrp.Items.Count Then
                    Me.cboTGrp.SelectedIndex = 0
                Else
                    Me.cboTGrp.SelectedIndex = Convert.ToInt16(sTgrp)
                End If
            End If

            If cboWkGrp.Items.Count > 0 Then
                If sWkGrp = "" Or Val(sWkGrp) > Me.cboWkGrp.Items.Count Then
                    Me.cboWkGrp.SelectedIndex = 0
                Else
                    Me.cboWkGrp.SelectedIndex = Convert.ToInt16(sWkGrp)
                End If
            End If

            If sJob = "" Or Val(sJob) > Me.cboQrygbn.Items.Count Then
                Me.cboQrygbn.SelectedIndex = 0
            Else
                Me.cboQrygbn.SelectedIndex = Convert.ToInt16(sJob)
            End If

            If sTestCds <> "" Then
                Me.txtSelTest.Text = sTestCds.Split("^"c)(1).Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds
            End If

            Me.dtpDateS.Focus()

            sbDisplay_Spc()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Date_Setting()

        If Me.cboQrygbn.Text = "검사그룹" Then
            Me.lblTitleDt.Text = "접수일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            Me.txtWkNoS.Visible = False : Me.txtWkNoE.Visible = False
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False : Me.cboWL.Visible = False
            Me.lblTSpc.Visible = True : Me.cboSpcCd.Visible = True

        ElseIf Me.cboQrygbn.Text = "작업그룹" Then

            Me.lblTitleDt.Text = "작업일자"

            Me.dtpDateE.Visible = False
            Me.lblDate.Visible = False

            Me.txtWkNoS.Visible = True : Me.txtWkNoE.Visible = True
            Me.lblWk.Visible = True

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True : Me.cboWL.Visible = False
            Me.lblTSpc.Visible = True : Me.cboSpcCd.Visible = True

            If Me.cboWkGrp.Text = "" Then Return

            Dim sWkNoGbn As String = cboWkGrp.Text.Split("|"c)(1)

            Select Case sWkNoGbn
                Case "1"
                    Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
                Case "2"
                    Me.dtpDateS.CustomFormat = "yyyy-MM"
                Case "3"
                    Me.dtpDateS.CustomFormat = "yyyy"
                Case Else
                    Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            End Select

        Else
            Me.lblTitleDt.Text = "W/L 일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            Me.txtWkNoS.Visible = False : Me.txtWkNoE.Visible = False
            Me.lblWk.Visible = False

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = False : Me.cboWL.Visible = True

            Me.lblTSpc.Visible = False : Me.cboSpcCd.Visible = False

            sbDisplay_wl()
        End If
    End Sub

    Private Sub sbDisplay_Slip()

        Dim sFn As String = "Sub sbDisplay_Slip()"
        Dim dt As DataTable

        Try
            Dim sTmp As String = ""

            dt = LISAPP.COMM.CdFn.fnGet_Slip_List(Me.dtpDateS.Text, , False)

            Me.cboSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            sTmp = COMMON.CommXML.getOneElementXML(msXMLDir, msSlipFile, "SLIP")

            If sTmp = "" Or Val(sTmp) > Me.cboSlip.Items.Count Then
                Me.cboSlip.SelectedIndex = 0
            Else
                Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
        End Try
    End Sub

    Private Sub sbDisplay_WkGrp()

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_WKGrp_List(Ctrl.Get_Code(cboSlip))

        Me.cboWkGrp.Items.Clear()

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString)
        Next

        If Me.cboWkGrp.Items.Count > 0 Then Me.cboWkGrp.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_TGrp() ''' 검사그룹조회 

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List(False, False, False)

        Me.cboTGrp.Items.Clear()
        Me.cboTGrp.Items.Add("[  ] 전체")

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboTGrp.Items.Add("[" + dt.Rows(ix).Item("tgrpcd").ToString + "] " + dt.Rows(ix).Item("tgrpnmd").ToString)
        Next
        Me.cboTGrp.SelectedIndex = 0

    End Sub

    Private Sub sbDisplay_Spc()
        Dim sFn As String = "Sub sbDisplay_Spc()"

        Try
            Dim sPartCd As String = ""
            Dim sSlipCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sWGrpCd As String = ""

            If cboQrygbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(cboTGrp)
            Else
                If Ctrl.Get_Code(cboSlip) <> "" Then
                    sPartCd = Ctrl.Get_Code(cboSlip).Substring(0, 1)
                    sSlipCd = Ctrl.Get_Code(cboSlip).Substring(0, 1)
                End If
                If cboQrygbn.Text = "검사그룹" Then sWGrpCd = Ctrl.Get_Code(cboWkGrp)
            End If

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Spc_List("", sPartCd, sSlipCd, sTGrpCd, sWGrpCd, "", "")

            Me.cboSpcCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSpcCd.Items.Add("[" + dt.Rows(ix).Item("spccd").ToString.Trim + "] " + dt.Rows(ix).Item("spcnmd").ToString.Trim)
            Next


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_wl()

        Dim sFn As String = "Sub sbDisplay_wl()"

        Try

            Dim dt As DataTable = LISAPP.APP_WL.Qry.fnGet_wl_title(Ctrl.Get_Code(Me.cboSlip), "--", Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateS.Text.Replace("-", ""), "")

            Me.cboWL.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sTmp As String = ""
                sTmp += dt.Rows(ix).Item("wltitle").ToString.Trim + "(" + dt.Rows(ix).Item("wlymd").ToString.Trim + ")" + Space(200) + "|"
                sTmp += dt.Rows(ix).Item("wlymd").ToString.Trim + "|"
                sTmp += dt.Rows(ix).Item("wluid").ToString.Trim + "|"
                sTmp += dt.Rows(ix).Item("wltype").ToString.Trim + "|"

                Me.cboWL.Items.Add(sTmp)
            Next

            If Me.cboWL.Items.Count > 0 Then Me.cboWL.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Private Sub FGR06_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        COMMON.CommXML.setOneElementXML(msXMLDir, msTestFile, "TEST", Me.txtSelTest.Tag.ToString)
        MdiTabControl.sbTabPageMove(Me)

    End Sub

    Private Sub FGR06_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFn As String = "Sub FGR04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load"

        sbDisp_Init()

        Me.axItemSave.FORMID = "R"
        Me.axItemSave.USRID = USER_INFO.USRID
        Me.axItemSave.ITEMGBN = ""
        Me.axItemSave.SPCGBN = "NONE"
        Me.axItemSave.BloodBankYn = False
        Me.axItemSave.AllPartYn = False
        Me.axItemSave.sbDisplay_ItemList()

        Me.cboQrygbn.SelectedIndex = 2
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub FGR04_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnQuery_Click(Nothing, Nothing)
            Case Keys.F9
                If Me.btnReg.Enabled = True Then btnFN_Click(Me.btnReg, New System.EventArgs)
            Case Keys.F11
                If Me.btnMW.Enabled = True Then btnFN_Click(Me.btnMW, New System.EventArgs)
            Case Keys.F12
                If Me.btnFN.Enabled = True Then btnFN_Click(Me.btnFN, New System.EventArgs)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Dim sFn As String = "Handles btnCdHelp_test.Click"
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sPartSlip As String = ""

            If Me.cboQrygbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
                If sTGrpCd = "" Then sPartSlip = Ctrl.Get_Code(Me.cboSlip)
            Else
                sPartSlip = Ctrl.Get_Code(Me.cboSlip)
                sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            End If

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list(sPartSlip, sTGrpCd, sWGrpCd, , Ctrl.Get_Code(cboSpcCd))
            Dim a_dr As DataRow() = dt.Select("((tcdgbn = 'P' AND titleyn = '1') OR titleyn = '0')", "")

            dt = Fn.ChangeToDataTable(a_dr)
            objHelp.FormText = "검사목록"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            If Me.txtSelTest.Text <> "" Then objHelp.KeyCodes = Me.txtSelTest.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmp", "출력명", 0, , , True)
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("tcdgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("titleyn", "titleyn", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sTestCds As String = "", sTestNmds As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sTestCd As String = aryList.Item(ix).ToString.Split("|"c)(2)
                    Dim sTnmd As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sTestCds += "|" : sTestNmds += "|"
                    End If

                    sTestCds += sTestCd : sTestNmds += sTnmd
                Next

                Me.txtSelTest.Text = sTestNmds.Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds + "^" + sTestNmds
            Else
                Me.txtSelTest.Text = ""
                Me.txtSelTest.Tag = ""
            End If

            COMMON.CommXML.setOneElementXML(msXMLDir, msTestFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.axResult.sbDisplay_Init("ALL")
    End Sub

    Private Sub btnClear_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_test.Click
        Me.txtSelTest.Tag = "" : Me.txtSelTest.Text = ""
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnFN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFN.Click, btnMW.Click, btnReg.Click

        Try
            Dim sRetMsg As String = Me.axResult.fnReg(CType(sender, CButtonLib.CButton).Tag.ToString)

            If sRetMsg <> "" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, sRetMsg)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub


    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal rsBcNo As String = "") Handles btnQuery.Click

        Try
            Dim sRstNull As String = CStr(IIf(Me.chkRstNull.Checked, "1", "0"))
            Dim sRstReg As String = CStr(IIf(Me.chkRstReg.Checked, "1", "0"))
            Dim sRstFn As String = IIf(Me.chkRstFn.Checked, "1", "0").ToString()
            Dim sTestCdS As String = ""

            If Me.txtSelTest.Text <> "" Then sTestCdS = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")

            If Me.cboQrygbn.Text.StartsWith("W/L") And Me.cboWL.Items.Count > 0 Then
                Dim sWLUid As String = Me.cboWL.Text.Split("|"c)(2).Trim
                Dim sWLYmd As String = Me.cboWL.Text.Split("|"c)(1).Trim
                Dim sWLTitle As String = Me.cboWL.Text.Split("|"c)(0).Replace("(" + sWLYmd + ")", "").Trim

                Me.axResult.sbDisplay_Data_wl(sWLUid, sWLYmd, sWLTitle, sRstNull + sRstReg + sRstFn)

            ElseIf Me.cboQrygbn.Text.StartsWith("작업번호") Then
                Dim sWkYmd As String = Me.dtpDateS.Text.Replace("-", "").PadRight(8, "0"c)
                Dim sWkNos As String = Me.txtWkNoS.Text.PadLeft(4, "0"c)
                Dim sWkNoe As String = Me.txtWkNoE.Text.PadLeft(4, "0"c)

                Me.axResult.sbDisplay_Data_wgrp(sWkYmd, Ctrl.Get_Code(Me.cboWkGrp), sWkNos, sWkNoe, sTestCdS, sRstNull + sRstReg + sRstFn)

            ElseIf Me.cboQrygbn.Text.StartsWith("검사그룹") Then
                Dim sTkDts As String = Me.dtpDateS.Text.Replace("-", "").Replace(" ", "").Replace(":", "")
                Dim sTkDte As String = Me.dtpDateE.Text.Replace("-", "").Replace(" ", "").Replace(":", "")

                Me.axResult.sbDisplay_Data_tgrp(Ctrl.Get_Code(Me.cboTGrp), Stkdts, stkdte, sTestCdS, sRstNull + sRstReg + sRstFn)
            End If

            If Me.chkRstFn.Checked Then
                Me.btnFN.Enabled = False
                Me.btnMW.Enabled = False
                Me.btnReg.Enabled = False
            Else
                Me.btnFN.Enabled = True
                Me.btnMW.Enabled = True
                Me.btnReg.Enabled = True
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub axItemSave_ListDblClick(ByVal rsItemCds As String, ByVal rsItemNms As String) Handles axItemSave.ListDblClick
        Try
            If rsItemCds <> "" Then
                Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
                Me.txtSelTest.Text = rsItemNms.Replace("|", ",")
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtBcNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.GotFocus, txtWkNoS.GotFocus, txtWkNoE.GotFocus
        With CType(sender, Windows.Forms.TextBox)
            .SelectionStart = 0
            .SelectAll()
        End With
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return
        If Me.txtBcNo.Text.Length < 1 Then Return

        Dim dt As New DataTable
        Dim bFind As Boolean = False
        Dim sBcNo As String = ""

        sBcNo = Trim(Me.txtBcNo.Text)

        If Len(sBcNo) = 11 Or Len(sBcNo) = 12 Then
            sBcNo = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(sBcNo.Substring(0, 11))
        End If

        If sBcNo.Length = 14 Then sBcNo += "0"

        Me.txtBcNo.Text = sBcNo

        If bFind Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "이미 리스트에 있는 검체입니다.!!")
            Me.txtBcNo.Text = ""
        Else
            btnQuery_Click(Nothing, Nothing, sBcNo)
            Me.txtBcNo.Text = ""
        End If

    End Sub

    Private Sub cboQrygbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQrygbn.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Date_Setting()

        COMMON.CommXML.setOneElementXML(msXMLDir, msQryFile, "JOB", cboQrygbn.SelectedIndex.ToString)

    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged

        Me.axResult.sbDisplay_Init("ALL")

        sbDisplay_WkGrp()
        sbDisplay_Date_Setting()
        sbDisplay_Spc()
        sbDisplay_wl()

        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)

    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Spc()
        COMMON.CommXML.setOneElementXML(msXMLDir, msTgrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)

    End Sub

    Private Sub cboWkGrp_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedValueChanged

        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""
        Me.axResult.sbDisplay_Init("ALL")

        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        If cboWkGrp.SelectedIndex >= 0 Then
            COMMON.CommXML.setOneElementXML(msXMLDir, msWkGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
        End If

    End Sub

    Private Sub cboWL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWL.SelectedIndexChanged
        Try
            Dim sWLUid As String = Me.cboWL.Text.Split("|"c)(2)
            Dim sWLYmd As String = Me.cboWL.Text.Split("|"c)(1)
            Dim sWLTitle As String = Me.cboWL.Text.Split("|"c)(0).Replace("(" + sWLYmd + ")", "").Trim

            Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_Test_wl(sWLUid, sWLYmd, sWLTitle)

            If dt.Rows.Count < 1 Then Return

            Dim sTestCds As String = "", sTestNmds As String = ""
            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sTestCd As String = dt.Rows(ix).Item("testcd").ToString.Trim
                Dim sTnmd As String = dt.Rows(ix).Item("tnmd").ToString.Trim

                If ix > 0 Then
                    sTestCds += "|" : sTestNmds += "|"
                End If

                sTestCds += sTestCd : sTestNmds += sTnmd
            Next

            Me.txtSelTest.Text = sTestNmds.Replace("|", ",")
            Me.txtSelTest.Tag = sTestCds + "^" + sTestNmds

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub dtpDateS_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDateS.CloseUp, dtpDateE.CloseUp
        If Me.cboQrygbn.Text.StartsWith("W/L") Then sbDisplay_Date_Setting()
    End Sub

    Private Sub dtpDateS_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles dtpDateS.Validating, dtpDateE.Validating
        If Me.cboQrygbn.Text.StartsWith("W/L") Then sbDisplay_Date_Setting()

    End Sub
End Class