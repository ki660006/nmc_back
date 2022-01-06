'>>> 검사항목별 결과저장 및 보고

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin
Imports common.commlogin.login

Public Class FGR04
    Inherits System.Windows.Forms.Form
    Private moForm As Windows.Forms.Form

    Private Const msFile As String = "File : FGR04.vb, Class : FGR04" & vbTab

    Private mbBloodBankYN As Boolean = False
    Public msTitle As String = ""

    Private Const msXMLDir As String = "\XML"
    Private msTestFile As String = Application.StartupPath + msXMLDir + "\FGR04_TEST.XML"
    Private msWkGrpFile As String = Application.StartupPath + msXMLDir + "\FGR04_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath + msXMLDir + "\FGR04_TGRP.XML"
    Private msSlipFile As String = Application.StartupPath + msXMLDir + "\FGR04_SLIP.XML"
    Private msQryFile As String = Application.StartupPath + msXMLDir + "\FGR04_Qry.XML"
    Private msTermFile As String = Application.StartupPath + msXMLDir + "\FGR04_Term.XML"

    Private malSpcInfo As New ArrayList ' 소견 hidden 컬럼 정보

    Private mbDebug As Boolean = False
    Private m_dt_RstCd As DataTable  ' 검사항목별 결과코드 마스터

    Private msSpreadNm As String    ' 결과코드 리스트 해당 스프레드

    Private mbLoaded As Boolean = False
    Friend WithEvents cboWkGrp As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents axResult As AxAckResult.AxRstInput
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlCode As System.Windows.Forms.Panel
    Friend WithEvents lstCode As System.Windows.Forms.ListBox
    Friend WithEvents btnAction As System.Windows.Forms.Button
    Friend WithEvents cmuAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sfdSCd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents chkSel As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnMW As CButtonLib.CButton
    Friend WithEvents btnFN As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents cboTGrp As System.Windows.Forms.ComboBox
    Friend WithEvents axItemSave As AxAckItemSave.ITEMSAVE
    Friend WithEvents btnClear_test As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button
    Friend WithEvents cboQrygbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtSelTest As System.Windows.Forms.TextBox
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents cboTerm As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboSpcCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblTSpc As System.Windows.Forms.Label
    Friend WithEvents cboWL As System.Windows.Forms.ComboBox
    Private mbActivated As Boolean = False

    Private Sub sbReg(ByVal rsRstFlg As String)

        Dim sFn As String = "Sub sbReg(String)"
        Try
            If Me.txtSelTest.Text = "" Then Return

            Dim sBuf() As String = Me.txtSelTest.Tag.ToString.Split("^"c)
            Dim aBuf As New ArrayList
            Dim sTestCds As String = ""

            For ix As Integer = 0 To sBuf(0).Split("|"c).Length - 1
                Dim sTestCd As String = sBuf(0).Split("|"c)(ix)

                If sTestCd.Length = 7 Then
                    If aBuf.Contains(sBuf(0).Split("|"c)(ix).Substring(0, 5)) = False Then
                        aBuf.Add(sBuf(0).Split("|"c)(ix).Substring(0, 5))
                    End If
                End If
                If aBuf.Contains(sBuf(0).Split("|"c)(ix)) = False Then
                    aBuf.Add(sBuf(0).Split("|"c)(ix))
                End If
            Next

            For ix As Integer = 0 To aBuf.Count - 1
                If ix <> 0 Then sTestCds += ","
                sTestCds += aBuf.Item(ix).ToString
            Next

            With spdSpcInfo
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                    Dim alRst As New ArrayList

                    If sChk = "1" Then
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                        .Col = .GetColFromID("regno") : Dim sRegNo As String = .Text
                        .Col = .GetColFromID("patnm") : Dim sPatNm As String = .Text
                        .Col = .GetColFromID("sexage") : Dim sSexAge As String = .Text
                        .Col = .GetColFromID("deptcd") : Dim sDeptCd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text

                        alRst = fnGet_Results(iRow, sBcNo)

                        If alRst.Count > 0 Then

                            Me.axResult.RegNo = sRegNo
                            Me.axResult.PatName = sPatNm
                            Me.axResult.SexAge = sSexAge
                            Me.axResult.DeptCd = sDeptCd
                            Me.axResult.TgrpCds = ""
                            Me.axResult.WKgrpCd = ""
                            Me.axResult.EqCd = ""
                            Me.axResult.TestCds = (Me.txtSelTest.Tag.ToString.Split("^"c)(0) + "|").Replace("|", sSpcCd + ",")
                            Me.axResult.BcNoAll = False
                            Me.axResult.sbDisplay_Data(sBcNo)

                            Threading.Thread.Sleep(800)

                            Dim blnRst As Boolean = axResult.fnReg(rsRstFlg, alRst)

                            axResult.sbDisplay_Init("ALL")
                        End If
                    End If
                Next
            End With

            sbClear_Form()
            btnQuery_Click(Nothing, Nothing)

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

            With Me.spdSpcInfo
                .Row = 0
                .Col = .GetColFromID("workno") : .Text = "작업번호"
            End With
        ElseIf Me.cboQrygbn.Text = "작업그룹" Then

            Me.lblTitleDt.Text = "작업일자"

            Me.dtpDateE.Visible = False
            Me.lblDate.Visible = False

            Me.txtWkNoS.Visible = True : Me.txtWkNoE.Visible = True
            Me.lblWk.Visible = True

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True : Me.cboWL.Visible = False
            Me.lblTSpc.Visible = True : Me.cboSpcCd.Visible = True

            With Me.spdSpcInfo
                .Row = 0
                .Col = .GetColFromID("workno") : .Text = "작업번호"
            End With

            If cboWkGrp.Text = "" Then Return

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

            With Me.spdSpcInfo
                .Row = 0
                .Col = .GetColFromID("workno") : .Text = "W/L NO."
            End With

            sbDisplay_wl()
        End If
    End Sub

    Private Sub sbClear_ResultInfo()

        Me.lblReg.Text = ""
        Me.lblMW.Text = ""
        Me.lblFN.Text = ""

        With Me.spdRef
            .Col = 1
            .Row = 1
            .Col2 = .MaxCols
            .Row2 = 1
            .BlockMode = True
            .BackColor = Drawing.Color.White
            .BlockMode = False
            .Col = 1
            .Row = 1
            .CellNote = ""
            .Col = 2
            .Row = 1
            .CellNote = ""
            .SetText(-1, 1, "")
        End With

        Me.lblSpcNm.Text = ""
        Me.lblEqNm.Text = ""
        Me.lblRst.Text = ""
        Me.lblBFFNDT.Text = ""
        Me.lblBFVIEWRST.Text = ""
        Me.lblRef.Text = ""
        Me.lblCABO.Text = ""
        Me.lblOABO.Text = ""
        Me.txtComment.Text = ""

    End Sub

    Private Sub sbDisplay_ResultInfo(ByVal roTInfo As AxAckResult.RST_INFO, ByVal rsSpcNmd As String)

        sbClear_ResultInfo()

        Dim strRstType$ = roTInfo.RstType
        Dim strRstULen$ = roTInfo.RstULen
        Dim strRstLLen$ = roTInfo.RstLLen
        Dim strCutOpt$ = roTInfo.CutOpt

        Dim strRefGbn$ = roTInfo.RefGbn
        Dim strJudgType$ = roTInfo.JudgType
        Dim strRefLs$ = roTInfo.RefLs
        Dim strRefL$ = roTInfo.RefL
        Dim strRefHs$ = roTInfo.RefHs
        Dim strRefH$ = roTInfo.RefH

        Dim strPanicGbn$ = roTInfo.PanicGbn
        Dim strPanicL$ = roTInfo.PanicL
        Dim strPanicH$ = roTInfo.PanicH
        Dim strSpcCd$ = roTInfo.SpcCd

        Dim strUJudglt1$ = roTInfo.UJudglt1
        Dim strUJudglt2$ = roTInfo.UJudglt2
        Dim strUJudglt3$ = roTInfo.UJudglt3

        Dim strBfFnDt$ = roTInfo.BfFnDt
        Dim strBfRst$ = roTInfo.BfOrgRst
        Dim strDeltaGbn$ = roTInfo.DeltaGbn
        Dim strDeltaL$ = roTInfo.DeltaL
        Dim strDeltaH$ = roTInfo.DeltaH
        Dim strDeltaDay$ = roTInfo.DeltaDay

        Dim strCriticalGbn$ = roTInfo.CriticalGbn
        Dim strCriticalL$ = roTInfo.CriticalL
        Dim strCriticalH$ = roTInfo.CriticalH

        Dim strAlertGbn$ = roTInfo.AlertGbn
        Dim strAlertL$ = roTInfo.AlertL
        Dim strAlertH$ = roTInfo.AlertH

        Dim strAlimitGbn$ = roTInfo.AlimitGbn
        Dim strAlimitLs$ = roTInfo.AlimitLs
        Dim strAlimitL$ = roTInfo.AlimitL
        Dim strAlimitH$ = roTInfo.AlimitH
        Dim strAlimitHs$ = roTInfo.AlimitHs

        With Me.spdRef
            spdRef.Row = 1
            spdRef.Col = 1 : spdRef.Text = roTInfo.HLMark

            If roTInfo.HLMark = "L" Then
                .BackColor = Color.FromArgb(221, 240, 255)
                .ForeColor = Color.FromArgb(0, 0, 255)
            ElseIf roTInfo.HLMark = "H" Then
                .BackColor = Color.FromArgb(255, 230, 231)
                .ForeColor = Color.FromArgb(255, 0, 0)
            End If

            spdRef.Col = 2 : spdRef.Text = roTInfo.PanicMark
            If roTInfo.PanicMark = "P" Then
                .BackColor = Color.FromArgb(150, 150, 255)
                .ForeColor = Color.FromArgb(255, 255, 255)
            End If

            spdRef.Col = 3 : spdRef.Text = roTInfo.DeltaMark
            If roTInfo.DeltaMark = "D" Then
                .BackColor = Color.FromArgb(150, 255, 150)
                .ForeColor = Color.FromArgb(0, 128, 64)
            End If

            spdRef.Col = 4 : spdRef.Text = roTInfo.CriticalMark
            If roTInfo.CriticalMark = "C" Then
                .BackColor = Color.FromArgb(255, 150, 255)
                .ForeColor = Color.FromArgb(255, 255, 255)
            End If

            spdRef.Col = 5 : spdRef.Text = roTInfo.AlertMark
            If roTInfo.AlertMark <> "" Then
                .BackColor = Color.FromArgb(255, 255, 150)
                .ForeColor = Color.FromArgb(0, 0, 0)
            End If

            spdRef.Col = 6 : spdRef.Text = roTInfo.RstFlg
        End With

        Me.lblSpcNm.Text = rsSpcNmd
        Me.lblRst.Text = roTInfo.ViewRst

        Me.lblRef.Text = roTInfo.RefTxt
        Me.lblBFVIEWRST.Text = roTInfo.BfViewRst
        Me.lblBFFNDT.Text = roTInfo.BfFnDt

        Me.lblEqNm.Text = roTInfo.EqNm

        Me.lblReg.Text = roTInfo.RegNm
        Me.lblMW.Text = roTInfo.MwNm
        lblFN.Text = roTInfo.FnNm

        Me.txtComment.Text = roTInfo.RstCmt

        Me.lblCABO.Text = roTInfo.ABO_Cur
        Me.lblOABO.Text = roTInfo.ABO_Old

    End Sub

    Private Sub sbDisplay_Test()

        If Me.txtSelTest.Text = "" Then
            spdSpcInfo.MaxCols = spdSpcInfo.GetColFromID("tposition")
            Return
        End If

        Dim sBuf() As String = Me.txtSelTest.Tag.ToString.Split("^"c)
        Dim sgColWidth As Single = 8

        If cboTerm.Text <> "" Then sgColWidth = Convert.ToSingle(cboTerm.Text)

        For ix As Integer = 0 To sBuf(0).Split("|"c).Length - 1
            With spdSpcInfo
                If ix = 0 Then
                    .MaxCols = .GetColFromID("tposition") + sBuf(0).Split("|"c).Length

                    For iCol As Integer = .GetColFromID("tposition") + 1 To .MaxCols
                        .set_ColWidth(iCol, sgColWidth)
                    Next
                End If

                .Row = 0
                .Col = .GetColFromID("tposition") + ix + 1 : .Text = sBuf(1).Split("|"c)(ix).Trim : .ColID = sBuf(0).Split("|"c)(ix).Trim
            End With
        Next

    End Sub

    Private Sub sbDisplay_PatInfo(ByVal r_dt As DataTable)

        'spdSpcInfo.MaxRows = r_dt.Rows.Count
        Dim intRow As Integer
        Dim strBcno As String = ""

        If r_dt.Rows.Count > 0 Then
            With spdSpcInfo
                For intIx1 As Integer = 0 To r_dt.Rows.Count - 1
                    If strBcno <> r_dt.Rows(intIx1).Item("bcno").ToString Then
                        .MaxRows += 1
                        intRow = .MaxRows

                        .Col = .GetColFromID("tposition") + 1 : .Col2 = .MaxCols
                        .Row = .MaxRows : .Row2 = .MaxRows
                        .BlockMode = True
                        .BackColor = Drawing.Color.LightGray
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .BlockMode = False
                    End If

                    For intIx2 As Integer = 1 To r_dt.Columns.Count
                        Dim intCol As Integer = 0

                        Select Case r_dt.Columns(intIx2 - 1).ColumnName.ToLower()
                            Case "testcd"
                                .Row = intRow
                                .Col = .GetColFromID(r_dt.Rows(intIx1).Item("testcd").ToString.Trim)
                                .Text = r_dt.Rows(intIx1).Item("orgrst").ToString.Trim

                                .BackColor = Drawing.Color.White
                                '.Lock = False

                                If r_dt.Rows(intIx1).Item("titleyn").ToString.Trim = "1" Then
                                    .Row = intRow
                                    .Col = .GetColFromID(r_dt.Rows(intIx1).Item("testcd").ToString.Trim)
                                    .BackColor = Drawing.Color.LightGray
                                    .ForeColor = Drawing.Color.LightGray

                                ElseIf r_dt.Rows(intIx1).Item("plgbn").ToString.Trim = "2" Then
                                    If r_dt.Rows(intIx1).Item("mwid").ToString.Trim <> "" And r_dt.Rows(intIx1).Item("mwid").ToString.Trim <> STU_AUTHORITY.usrid And _
                                       r_dt.Rows(intIx1).Item("orgrst").ToString.Trim <> "" And r_dt.Rows(intIx1).Item("rstflg").ToString.Trim <> "3" Then

                                        .Row = intRow
                                        .Col = .GetColFromID(r_dt.Rows(intIx1).Item("testcd").ToString.Trim)

                                        .BackColor = Color.LightPink
                                        .ForeColor = Color.LightPink
                                    Else
                                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit
                                    End If
                                Else
                                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit
                                End If


                            Case Else
                                intCol = .GetColFromID(r_dt.Columns(intIx2 - 1).ColumnName.ToLower())
                                If intCol > 0 Then
                                    .Row = intRow
                                    .Col = intCol

                                    .Text = r_dt.Rows(intIx1).Item(intIx2 - 1).ToString.Trim
                                End If
                        End Select
                    Next

                    strBcno = r_dt.Rows(intIx1).Item("bcno").ToString
                Next
            End With
        End If

    End Sub

    Private Sub sbDisplay_Slip()

        Dim sFn As String = "Sub sbDisplay_Slip()"
        Dim dt As DataTable

        Try
            Dim sTmp As String = ""

            dt = LISAPP.COMM.cdfn.fnGet_Slip_List(dtpDateS.Text, , mbBloodBankYN)

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
            'MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_WkGrp()

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_WKGrp_List(Ctrl.Get_Code(cboSlip))

        cboWkGrp.Items.Clear()

        For ix As Integer = 0 To dt.Rows.Count - 1
            cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString)
        Next

        If cboWkGrp.SelectedIndex = -1 Then
        Else
            cboWkGrp.SelectedIndex = 0
        End If

        'cboWkGrp.SelectedIndex = 0  이전 쏘스 위의 구문으로 변경 조회된 작업그룹이 없을시 오류 남

    End Sub

    Private Sub sbDisplay_TGrp() ''' 검사그룹조회 

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TGrp_List(False, False, mbBloodBankYN)

        cboTGrp.Items.Clear()
        cboTGrp.Items.Add("[  ] 전체")

        For ix As Integer = 0 To dt.Rows.Count - 1
            cboTGrp.Items.Add("[" + dt.Rows(ix).Item("tgrpcd").ToString + "] " + dt.Rows(ix).Item("tgrpnmd").ToString)
        Next
        cboTGrp.SelectedIndex = 0
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

    Private Sub sbDisplay_Result(ByVal r_dt As DataTable)

        If r_dt.Rows.Count > 0 Then
            With spdSpcInfo
                For intIdx As Integer = 0 To r_dt.Rows.Count - 1
                    Dim strBcNo As String = ""
                    Dim strTclsCd As String = ""

                    Dim intPos As Integer = 0
                    For intRow As Integer = 1 To .MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")

                        If strBcNo = r_dt.Rows(intIdx).Item("bcno").ToString Then
                            intPos = intRow
                            Exit For
                        End If
                    Next

                    If intPos > 0 Then
                        .Row = intPos
                        .Col = .GetColFromID(r_dt.Rows(intIdx).Item("tclscd").ToString.Trim)
                        .Text = r_dt.Rows(intIdx).Item("orgrst").ToString.Trim

                        .BackColor = Drawing.Color.White
                        .Lock = False

                        If r_dt.Rows(intIdx).Item("titleyn").ToString.Trim = "1" Then
                            .BackColor = Drawing.Color.LightGray
                            .ForeColor = Drawing.Color.LightGray
                            .Lock = True
                        ElseIf r_dt.Rows(intIdx).Item("plgbn").ToString.Trim = "2" Then
                            If r_dt.Rows(intIdx).Item("mwid").ToString.Trim <> "" And r_dt.Rows(intIdx).Item("mwid").ToString.Trim <> STU_AUTHORITY.usrid And _
                               r_dt.Rows(intIdx).Item("orgrst").ToString.Trim <> "" And r_dt.Rows(intIdx).Item("rstflag").ToString.Trim <> "3" Then

                                .BackColor = Color.LightPink
                                .ForeColor = Color.LightPink
                                .Lock = True
                            End If
                        End If
                    End If
                Next
            End With
        End If

    End Sub

    Private Function fnGet_Results(ByVal riRow As Integer, ByVal rsBcNo As String) As ArrayList

        If Me.txtSelTest.Text = "" Then Return New ArrayList

        Dim alRst As New ArrayList

        With spdSpcInfo
            .Row = riRow
            Dim sBuf() As String = Me.txtSelTest.Tag.ToString.Split("^"c)

            For ix As Integer = 0 To sBuf(0).Split("|"c).Length - 1
                .Col = .GetColFromID(sBuf(0).Split("|"c)(ix))
                If .Text <> .CellNote Then
                    Dim sOrgRst As String = .Text
                    Dim sViewRst As String = .Text
                    Dim sTestCd As String = sBuf(0).Split("|"c)(ix)

                    If sOrgRst <> "" Then
                        Dim objRst As New AxAckResult.RST_INFO

                        With objRst
                            .Chk = "1"
                            .IUD = "1"
                            .BcNo = rsBcNo
                            .OrgRst = sOrgRst
                            .ViewRst = sViewRst
                            .TestCd = sTestCd
                            .RstCmt = ""
                        End With
                        alRst.Add(objRst)
                    End If
                End If
            Next
        End With

        Return alRst

    End Function

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        'sbDisp_Init()
    End Sub

    Public Sub New(ByVal rbBloodBankYn As Boolean)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        'sbDisp_Init()

        mbBloodBankYN = rbBloodBankYn

        If mbBloodBankYN Then
            msTestFile = Application.StartupPath & msXMLDir & "\FGR04_B_TEST.XML"
            msWkGrpFile = Application.StartupPath & msXMLDir & "\FGR04_B_WKGRP.XML"
            msTgrpFile = Application.StartupPath & msXMLDir & "\FGR04_B_TGRP.XML"
            msSlipFile = Application.StartupPath & msXMLDir & "\FGR043_B_SLIP.XML"
            msQryFile = Application.StartupPath & msXMLDir & "\FGR04_B_Qry.XML"
            msTermFile = Application.StartupPath & msXMLDir & "\FGR04_B_Term.XML"

        End If
    End Sub


    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTitleDt As System.Windows.Forms.Label
    Friend WithEvents pnlRstInfo As System.Windows.Forms.Panel
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblWk As System.Windows.Forms.Label
    Friend WithEvents txtWkNoE As System.Windows.Forms.TextBox
    Friend WithEvents txtWkNoS As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents spdSpcInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblRegNm As System.Windows.Forms.Label
    Friend WithEvents lblReg As System.Windows.Forms.Label
    Friend WithEvents lblMW As System.Windows.Forms.Label
    Friend WithEvents lblFN As System.Windows.Forms.Label
    Friend WithEvents spdRef As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents lblEqNm As System.Windows.Forms.Label
    Friend WithEvents lblBFFNDT As System.Windows.Forms.Label
    Friend WithEvents lblBFVIEWRST As System.Windows.Forms.Label
    Friend WithEvents lblRef As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents chkRstReg As System.Windows.Forms.CheckBox
    Friend WithEvents chkRstNull As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents gbxABO As System.Windows.Forms.GroupBox
    Friend WithEvents gbxComment As System.Windows.Forms.GroupBox
    Friend WithEvents spdSpcInfoDT As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblCABO As System.Windows.Forms.Label
    Friend WithEvents lblOABO As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblRst As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkRstFn As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR04))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker13 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems7 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker14 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblRst = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblFN = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblReg = New System.Windows.Forms.Label()
        Me.lblRegNm = New System.Windows.Forms.Label()
        Me.lblMW = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblEqNm = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblBFFNDT = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblBFVIEWRST = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblRef = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSpcNm = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdRef = New AxFPSpreadADO.AxfpSpread()
        Me.gbxComment = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtBcNo = New System.Windows.Forms.TextBox()
        Me.lblWk = New System.Windows.Forms.Label()
        Me.txtWkNoE = New System.Windows.Forms.TextBox()
        Me.txtWkNoS = New System.Windows.Forms.TextBox()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblTitleDt = New System.Windows.Forms.Label()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.cboWkGrp = New System.Windows.Forms.ComboBox()
        Me.pnlRstInfo = New System.Windows.Forms.Panel()
        Me.cmuAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDelete = New System.Windows.Forms.ToolStripMenuItem()
        Me.chkSel = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.axResult = New AxAckResult.AxRstInput()
        Me.spdSpcInfo = New AxFPSpreadADO.AxfpSpread()
        Me.spdSpcInfoDT = New AxFPSpreadADO.AxfpSpread()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnAction = New System.Windows.Forms.Button()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkRstFn = New System.Windows.Forms.CheckBox()
        Me.chkRstReg = New System.Windows.Forms.CheckBox()
        Me.chkRstNull = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnQuery = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnFN = New CButtonLib.CButton()
        Me.btnMW = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.gbxABO = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblOABO = New System.Windows.Forms.Label()
        Me.lblCABO = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cboWL = New System.Windows.Forms.ComboBox()
        Me.cboSpcCd = New System.Windows.Forms.ComboBox()
        Me.lblTSpc = New System.Windows.Forms.Label()
        Me.lblTest = New System.Windows.Forms.Label()
        Me.txtSelTest = New System.Windows.Forms.TextBox()
        Me.cboQrygbn = New System.Windows.Forms.ComboBox()
        Me.btnClear_test = New System.Windows.Forms.Button()
        Me.btnCdHelp_test = New System.Windows.Forms.Button()
        Me.cboSlip = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboTGrp = New System.Windows.Forms.ComboBox()
        Me.pnlCode = New System.Windows.Forms.Panel()
        Me.lstCode = New System.Windows.Forms.ListBox()
        Me.sfdSCd = New System.Windows.Forms.SaveFileDialog()
        Me.axItemSave = New AxAckItemSave.ITEMSAVE()
        Me.cboTerm = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdRef, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbxComment.SuspendLayout()
        Me.pnlRstInfo.SuspendLayout()
        Me.cmuAction.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.spdSpcInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdSpcInfoDT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.gbxABO.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.pnlCode.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblRst)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblFN)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblReg)
        Me.GroupBox1.Controls.Add(Me.lblRegNm)
        Me.GroupBox1.Controls.Add(Me.lblMW)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.lblEqNm)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.lblBFFNDT)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblBFVIEWRST)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblRef)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblSpcNm)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Location = New System.Drawing.Point(1048, -5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(144, 451)
        Me.GroupBox1.TabIndex = 58
        Me.GroupBox1.TabStop = False
        '
        'lblRst
        '
        Me.lblRst.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRst.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblRst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRst.Location = New System.Drawing.Point(4, 166)
        Me.lblRst.Name = "lblRst"
        Me.lblRst.Size = New System.Drawing.Size(136, 20)
        Me.lblRst.TabIndex = 59
        Me.lblRst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(4, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 20)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "결과"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFN
        '
        Me.lblFN.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFN.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblFN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFN.Location = New System.Drawing.Point(4, 429)
        Me.lblFN.Name = "lblFN"
        Me.lblFN.Size = New System.Drawing.Size(136, 20)
        Me.lblFN.TabIndex = 57
        Me.lblFN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label15.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(4, 409)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(136, 20)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "최종보고자"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReg
        '
        Me.lblReg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReg.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblReg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblReg.Location = New System.Drawing.Point(4, 341)
        Me.lblReg.Name = "lblReg"
        Me.lblReg.Size = New System.Drawing.Size(136, 20)
        Me.lblReg.TabIndex = 55
        Me.lblReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNm
        '
        Me.lblRegNm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRegNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNm.ForeColor = System.Drawing.Color.White
        Me.lblRegNm.Location = New System.Drawing.Point(4, 321)
        Me.lblRegNm.Name = "lblRegNm"
        Me.lblRegNm.Size = New System.Drawing.Size(136, 20)
        Me.lblRegNm.TabIndex = 54
        Me.lblRegNm.Text = "결과입력자"
        Me.lblRegNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMW
        '
        Me.lblMW.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMW.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblMW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMW.Location = New System.Drawing.Point(4, 385)
        Me.lblMW.Name = "lblMW"
        Me.lblMW.Size = New System.Drawing.Size(136, 20)
        Me.lblMW.TabIndex = 53
        Me.lblMW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label27
        '
        Me.Label27.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label27.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label27.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.White
        Me.Label27.Location = New System.Drawing.Point(4, 365)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(136, 20)
        Me.Label27.TabIndex = 52
        Me.Label27.Text = "중간보고자"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEqNm
        '
        Me.lblEqNm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEqNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblEqNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEqNm.Location = New System.Drawing.Point(4, 297)
        Me.lblEqNm.Name = "lblEqNm"
        Me.lblEqNm.Size = New System.Drawing.Size(136, 20)
        Me.lblEqNm.TabIndex = 51
        Me.lblEqNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(4, 277)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(136, 20)
        Me.Label9.TabIndex = 50
        Me.Label9.Text = "검사장비"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBFFNDT
        '
        Me.lblBFFNDT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBFFNDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblBFFNDT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBFFNDT.Location = New System.Drawing.Point(4, 253)
        Me.lblBFFNDT.Name = "lblBFFNDT"
        Me.lblBFFNDT.Size = New System.Drawing.Size(136, 20)
        Me.lblBFFNDT.TabIndex = 49
        Me.lblBFFNDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(4, 233)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(136, 20)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "이전결과일"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBFVIEWRST
        '
        Me.lblBFVIEWRST.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBFVIEWRST.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblBFVIEWRST.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBFVIEWRST.Location = New System.Drawing.Point(4, 209)
        Me.lblBFVIEWRST.Name = "lblBFVIEWRST"
        Me.lblBFVIEWRST.Size = New System.Drawing.Size(136, 20)
        Me.lblBFVIEWRST.TabIndex = 47
        Me.lblBFVIEWRST.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(4, 189)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 20)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "이전결과"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRef
        '
        Me.lblRef.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRef.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblRef.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRef.Location = New System.Drawing.Point(4, 76)
        Me.lblRef.Name = "lblRef"
        Me.lblRef.Size = New System.Drawing.Size(136, 20)
        Me.lblRef.TabIndex = 45
        Me.lblRef.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 20)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "참고치 && 판정"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcNm
        '
        Me.lblSpcNm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSpcNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcNm.Location = New System.Drawing.Point(4, 32)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(136, 20)
        Me.lblSpcNm.TabIndex = 27
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label21.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.White
        Me.Label21.Location = New System.Drawing.Point(4, 12)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(136, 20)
        Me.Label21.TabIndex = 26
        Me.Label21.Text = "검체명"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdRef)
        Me.Panel1.Location = New System.Drawing.Point(4, 96)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(136, 48)
        Me.Panel1.TabIndex = 44
        '
        'spdRef
        '
        Me.spdRef.DataSource = Nothing
        Me.spdRef.Location = New System.Drawing.Point(0, 0)
        Me.spdRef.Name = "spdRef"
        Me.spdRef.OcxState = CType(resources.GetObject("spdRef.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRef.Size = New System.Drawing.Size(180, 50)
        Me.spdRef.TabIndex = 0
        '
        'gbxComment
        '
        Me.gbxComment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbxComment.Controls.Add(Me.Label1)
        Me.gbxComment.Controls.Add(Me.txtComment)
        Me.gbxComment.Location = New System.Drawing.Point(1048, 452)
        Me.gbxComment.Name = "gbxComment"
        Me.gbxComment.Size = New System.Drawing.Size(144, 123)
        Me.gbxComment.TabIndex = 59
        Me.gbxComment.TabStop = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Comment"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtComment
        '
        Me.txtComment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComment.Location = New System.Drawing.Point(4, 20)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(136, 99)
        Me.txtComment.TabIndex = 0
        '
        'dtpDateE
        '
        Me.dtpDateE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateE.Location = New System.Drawing.Point(206, 35)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(102, 21)
        Me.dtpDateE.TabIndex = 15
        Me.dtpDateE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label8.Location = New System.Drawing.Point(518, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(112, 21)
        Me.Label8.TabIndex = 83
        Me.Label8.Text = "검체번호"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBcNo
        '
        Me.txtBcNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNo.Location = New System.Drawing.Point(519, 80)
        Me.txtBcNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(111, 21)
        Me.txtBcNo.TabIndex = 82
        '
        'lblWk
        '
        Me.lblWk.AutoSize = True
        Me.lblWk.Location = New System.Drawing.Point(226, 39)
        Me.lblWk.Name = "lblWk"
        Me.lblWk.Size = New System.Drawing.Size(11, 12)
        Me.lblWk.TabIndex = 18
        Me.lblWk.Text = "~"
        Me.lblWk.Visible = False
        '
        'txtWkNoE
        '
        Me.txtWkNoE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkNoE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkNoE.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoE.Location = New System.Drawing.Point(240, 35)
        Me.txtWkNoE.MaxLength = 4
        Me.txtWkNoE.Name = "txtWkNoE"
        Me.txtWkNoE.Size = New System.Drawing.Size(33, 21)
        Me.txtWkNoE.TabIndex = 17
        Me.txtWkNoE.Text = "9999"
        '
        'txtWkNoS
        '
        Me.txtWkNoS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkNoS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkNoS.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoS.Location = New System.Drawing.Point(190, 35)
        Me.txtWkNoS.MaxLength = 4
        Me.txtWkNoS.Name = "txtWkNoS"
        Me.txtWkNoS.Size = New System.Drawing.Size(33, 21)
        Me.txtWkNoS.TabIndex = 16
        Me.txtWkNoS.Text = "0000"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(193, 39)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(11, 12)
        Me.lblDate.TabIndex = 16
        Me.lblDate.Text = "~"
        '
        'lblTitleDt
        '
        Me.lblTitleDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTitleDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTitleDt.ForeColor = System.Drawing.Color.White
        Me.lblTitleDt.Location = New System.Drawing.Point(5, 35)
        Me.lblTitleDt.Name = "lblTitleDt"
        Me.lblTitleDt.Size = New System.Drawing.Size(80, 21)
        Me.lblTitleDt.TabIndex = 14
        Me.lblTitleDt.Text = "접수일자"
        Me.lblTitleDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateS
        '
        Me.dtpDateS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateS.Location = New System.Drawing.Point(86, 35)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(102, 21)
        Me.dtpDateS.TabIndex = 13
        Me.dtpDateS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'cboWkGrp
        '
        Me.cboWkGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWkGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWkGrp.Location = New System.Drawing.Point(312, 13)
        Me.cboWkGrp.Margin = New System.Windows.Forms.Padding(1)
        Me.cboWkGrp.Name = "cboWkGrp"
        Me.cboWkGrp.Size = New System.Drawing.Size(136, 20)
        Me.cboWkGrp.TabIndex = 88
        '
        'pnlRstInfo
        '
        Me.pnlRstInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRstInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.pnlRstInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlRstInfo.ContextMenuStrip = Me.cmuAction
        Me.pnlRstInfo.Controls.Add(Me.chkSel)
        Me.pnlRstInfo.Controls.Add(Me.GroupBox2)
        Me.pnlRstInfo.Controls.Add(Me.spdSpcInfo)
        Me.pnlRstInfo.Location = New System.Drawing.Point(1, 105)
        Me.pnlRstInfo.Name = "pnlRstInfo"
        Me.pnlRstInfo.Size = New System.Drawing.Size(1045, 544)
        Me.pnlRstInfo.TabIndex = 51
        '
        'cmuAction
        '
        Me.cmuAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDelete})
        Me.cmuAction.Name = "cmuRstList"
        Me.cmuAction.Size = New System.Drawing.Size(151, 26)
        Me.cmuAction.Text = "상황에 맞는 메뉴"
        '
        'mnuDelete
        '
        Me.mnuDelete.Name = "mnuDelete"
        Me.mnuDelete.Size = New System.Drawing.Size(150, 22)
        Me.mnuDelete.Text = "선택항목 삭제"
        '
        'chkSel
        '
        Me.chkSel.AutoSize = True
        Me.chkSel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkSel.Location = New System.Drawing.Point(34, 7)
        Me.chkSel.Name = "chkSel"
        Me.chkSel.Size = New System.Drawing.Size(15, 14)
        Me.chkSel.TabIndex = 152
        Me.chkSel.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.axResult)
        Me.GroupBox2.Location = New System.Drawing.Point(536, 59)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(657, 479)
        Me.GroupBox2.TabIndex = 151
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "GroupBox2"
        Me.GroupBox2.Visible = False
        '
        'axResult
        '
        Me.axResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axResult.BcNoAll = False
        Me.axResult.ColHiddenYn = False
        Me.axResult.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axResult.Location = New System.Drawing.Point(10, 20)
        Me.axResult.Name = "axResult"
        Me.axResult.Size = New System.Drawing.Size(493, 459)
        Me.axResult.TabIndex = 0
        Me.axResult.UseBloodBank = False
        Me.axResult.UseDoctor = False
        '
        'spdSpcInfo
        '
        Me.spdSpcInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress
        Me.spdSpcInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdSpcInfo.ContextMenuStrip = Me.cmuAction
        Me.spdSpcInfo.DataSource = Nothing
        Me.spdSpcInfo.Location = New System.Drawing.Point(0, 0)
        Me.spdSpcInfo.Name = "spdSpcInfo"
        Me.spdSpcInfo.OcxState = CType(resources.GetObject("spdSpcInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSpcInfo.Size = New System.Drawing.Size(1041, 545)
        Me.spdSpcInfo.TabIndex = 145
        '
        'spdSpcInfoDT
        '
        Me.spdSpcInfoDT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdSpcInfoDT.DataSource = Nothing
        Me.spdSpcInfoDT.Location = New System.Drawing.Point(0, 0)
        Me.spdSpcInfoDT.Name = "spdSpcInfoDT"
        Me.spdSpcInfoDT.OcxState = CType(resources.GetObject("spdSpcInfoDT.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSpcInfoDT.Size = New System.Drawing.Size(805, 547)
        Me.spdSpcInfoDT.TabIndex = 146
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.btnAction)
        Me.GroupBox5.Controls.Add(Me.txtCode)
        Me.GroupBox5.Location = New System.Drawing.Point(852, 14)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(191, 57)
        Me.GroupBox5.TabIndex = 60
        Me.GroupBox5.TabStop = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(4, 0)
        Me.Label10.Margin = New System.Windows.Forms.Padding(1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 21)
        Me.Label10.TabIndex = 196
        Me.Label10.Text = "결과값"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAction
        '
        Me.btnAction.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAction.Location = New System.Drawing.Point(144, 24)
        Me.btnAction.Margin = New System.Windows.Forms.Padding(0)
        Me.btnAction.Name = "btnAction"
        Me.btnAction.Size = New System.Drawing.Size(45, 23)
        Me.btnAction.TabIndex = 82
        Me.btnAction.Text = "적용"
        Me.btnAction.UseVisualStyleBackColor = True
        '
        'txtCode
        '
        Me.txtCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Location = New System.Drawing.Point(5, 25)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(0)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(136, 21)
        Me.txtCode.TabIndex = 17
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.Lavender
        Me.Panel3.Controls.Add(Me.chkRstFn)
        Me.Panel3.Controls.Add(Me.chkRstReg)
        Me.Panel3.Controls.Add(Me.chkRstNull)
        Me.Panel3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Panel3.Location = New System.Drawing.Point(462, 13)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(168, 21)
        Me.Panel3.TabIndex = 143
        '
        'chkRstFn
        '
        Me.chkRstFn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkRstFn.Location = New System.Drawing.Point(118, 1)
        Me.chkRstFn.Name = "chkRstFn"
        Me.chkRstFn.Size = New System.Drawing.Size(45, 19)
        Me.chkRstFn.TabIndex = 98
        Me.chkRstFn.Text = "최종"
        '
        'chkRstReg
        '
        Me.chkRstReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkRstReg.Location = New System.Drawing.Point(69, 1)
        Me.chkRstReg.Name = "chkRstReg"
        Me.chkRstReg.Size = New System.Drawing.Size(45, 19)
        Me.chkRstReg.TabIndex = 97
        Me.chkRstReg.Text = "입력"
        '
        'chkRstNull
        '
        Me.chkRstNull.Checked = True
        Me.chkRstNull.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRstNull.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkRstNull.Location = New System.Drawing.Point(9, 1)
        Me.chkRstNull.Name = "chkRstNull"
        Me.chkRstNull.Size = New System.Drawing.Size(58, 19)
        Me.chkRstNull.TabIndex = 96
        Me.chkRstNull.Text = "미입력"
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.btnQuery)
        Me.Panel4.Controls.Add(Me.btnExcel)
        Me.Panel4.Controls.Add(Me.btnReg)
        Me.Panel4.Controls.Add(Me.btnExit)
        Me.Panel4.Controls.Add(Me.txtID)
        Me.Panel4.Controls.Add(Me.btnFN)
        Me.Panel4.Controls.Add(Me.btnMW)
        Me.Panel4.Controls.Add(Me.btnClear)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 654)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1197, 32)
        Me.Panel4.TabIndex = 148
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems1
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.5416667!
        Me.btnQuery.FocalPoints.CenterPtY = 0.16!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(516, 3)
        Me.btnQuery.Margin = New System.Windows.Forms.Padding(0)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(96, 25)
        Me.btnQuery.TabIndex = 198
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems2
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.08!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker4
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(613, 3)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(91, 25)
        Me.btnExcel.TabIndex = 195
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems3
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5!
        Me.btnReg.FocalPoints.CenterPtY = 0.0!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker6
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(899, 3)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(96, 25)
        Me.btnReg.TabIndex = 192
        Me.btnReg.Text = "결과저장(F9)"
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1094, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(93, 25)
        Me.btnExit.TabIndex = 190
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(282, 7)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(116, 21)
        Me.txtID.TabIndex = 151
        Me.txtID.Text = "ACK"
        Me.txtID.Visible = False
        '
        'btnFN
        '
        Me.btnFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnFN.ColorFillBlend = CBlendItems5
        Me.btnFN.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnFN.Corners.All = CType(6, Short)
        Me.btnFN.Corners.LowerLeft = CType(6, Short)
        Me.btnFN.Corners.LowerRight = CType(6, Short)
        Me.btnFN.Corners.UpperLeft = CType(6, Short)
        Me.btnFN.Corners.UpperRight = CType(6, Short)
        Me.btnFN.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnFN.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnFN.FocalPoints.CenterPtX = 0.5!
        Me.btnFN.FocalPoints.CenterPtY = 0.08!
        Me.btnFN.FocalPoints.FocusPtX = 0.0!
        Me.btnFN.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.FocusPtTracker = DesignerRectTracker10
        Me.btnFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnFN.ForeColor = System.Drawing.Color.White
        Me.btnFN.Image = Nothing
        Me.btnFN.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFN.ImageIndex = 0
        Me.btnFN.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnFN.Location = New System.Drawing.Point(705, 3)
        Me.btnFN.Name = "btnFN"
        Me.btnFN.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnFN.SideImage = Nothing
        Me.btnFN.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnFN.Size = New System.Drawing.Size(96, 25)
        Me.btnFN.TabIndex = 194
        Me.btnFN.Text = "결과검증(F12)"
        Me.btnFN.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnFN.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnMW
        '
        Me.btnMW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnMW.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnMW.ColorFillBlend = CBlendItems6
        Me.btnMW.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnMW.Corners.All = CType(6, Short)
        Me.btnMW.Corners.LowerLeft = CType(6, Short)
        Me.btnMW.Corners.LowerRight = CType(6, Short)
        Me.btnMW.Corners.UpperLeft = CType(6, Short)
        Me.btnMW.Corners.UpperRight = CType(6, Short)
        Me.btnMW.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnMW.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnMW.FocalPoints.CenterPtX = 0.5!
        Me.btnMW.FocalPoints.CenterPtY = 0.0!
        Me.btnMW.FocalPoints.FocusPtX = 0.0!
        Me.btnMW.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnMW.FocusPtTracker = DesignerRectTracker12
        Me.btnMW.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnMW.ForeColor = System.Drawing.Color.White
        Me.btnMW.Image = Nothing
        Me.btnMW.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnMW.ImageIndex = 0
        Me.btnMW.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnMW.Location = New System.Drawing.Point(802, 3)
        Me.btnMW.Name = "btnMW"
        Me.btnMW.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnMW.SideImage = Nothing
        Me.btnMW.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnMW.Size = New System.Drawing.Size(96, 25)
        Me.btnMW.TabIndex = 193
        Me.btnMW.Text = "결과확인(F11)"
        Me.btnMW.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnMW.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker13.IsActive = False
        DesignerRectTracker13.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker13.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker13
        CBlendItems7.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems7.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems7
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0.0!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker14.IsActive = False
        DesignerRectTracker14.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker14.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker14
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(996, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(97, 25)
        Me.btnClear.TabIndex = 191
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'gbxABO
        '
        Me.gbxABO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbxABO.Controls.Add(Me.Label22)
        Me.gbxABO.Controls.Add(Me.Label45)
        Me.gbxABO.Controls.Add(Me.Label3)
        Me.gbxABO.Controls.Add(Me.lblOABO)
        Me.gbxABO.Controls.Add(Me.lblCABO)
        Me.gbxABO.Location = New System.Drawing.Point(1048, 569)
        Me.gbxABO.Name = "gbxABO"
        Me.gbxABO.Size = New System.Drawing.Size(144, 80)
        Me.gbxABO.TabIndex = 149
        Me.gbxABO.TabStop = False
        Me.gbxABO.Visible = False
        '
        'Label22
        '
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label22.Location = New System.Drawing.Point(71, 8)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(2, 68)
        Me.Label22.TabIndex = 164
        Me.Label22.Text = "Label22"
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label45.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label45.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label45.Location = New System.Drawing.Point(76, 8)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(64, 20)
        Me.Label45.TabIndex = 163
        Me.Label45.Text = "이전"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(4, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 162
        Me.Label3.Text = "현재"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOABO
        '
        Me.lblOABO.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblOABO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOABO.Font = New System.Drawing.Font("Arial Black", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOABO.ForeColor = System.Drawing.Color.Crimson
        Me.lblOABO.Location = New System.Drawing.Point(76, 28)
        Me.lblOABO.Name = "lblOABO"
        Me.lblOABO.Size = New System.Drawing.Size(64, 48)
        Me.lblOABO.TabIndex = 161
        Me.lblOABO.Text = "A+"
        Me.lblOABO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCABO
        '
        Me.lblCABO.BackColor = System.Drawing.Color.White
        Me.lblCABO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCABO.Font = New System.Drawing.Font("Arial Black", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCABO.ForeColor = System.Drawing.Color.Crimson
        Me.lblCABO.Location = New System.Drawing.Point(4, 28)
        Me.lblCABO.Name = "lblCABO"
        Me.lblCABO.Size = New System.Drawing.Size(64, 48)
        Me.lblCABO.TabIndex = 160
        Me.lblCABO.Text = "A+"
        Me.lblCABO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cboWL)
        Me.GroupBox6.Controls.Add(Me.cboSpcCd)
        Me.GroupBox6.Controls.Add(Me.lblTSpc)
        Me.GroupBox6.Controls.Add(Me.dtpDateE)
        Me.GroupBox6.Controls.Add(Me.txtBcNo)
        Me.GroupBox6.Controls.Add(Me.lblTest)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.lblWk)
        Me.GroupBox6.Controls.Add(Me.txtSelTest)
        Me.GroupBox6.Controls.Add(Me.Panel3)
        Me.GroupBox6.Controls.Add(Me.txtWkNoE)
        Me.GroupBox6.Controls.Add(Me.cboQrygbn)
        Me.GroupBox6.Controls.Add(Me.txtWkNoS)
        Me.GroupBox6.Controls.Add(Me.btnClear_test)
        Me.GroupBox6.Controls.Add(Me.btnCdHelp_test)
        Me.GroupBox6.Controls.Add(Me.cboSlip)
        Me.GroupBox6.Controls.Add(Me.lblDate)
        Me.GroupBox6.Controls.Add(Me.Label4)
        Me.GroupBox6.Controls.Add(Me.lblTitleDt)
        Me.GroupBox6.Controls.Add(Me.dtpDateS)
        Me.GroupBox6.Controls.Add(Me.cboWkGrp)
        Me.GroupBox6.Controls.Add(Me.cboTGrp)
        Me.GroupBox6.Location = New System.Drawing.Point(216, -4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox6.Size = New System.Drawing.Size(633, 106)
        Me.GroupBox6.TabIndex = 130
        Me.GroupBox6.TabStop = False
        '
        'cboWL
        '
        Me.cboWL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWL.Location = New System.Drawing.Point(312, 36)
        Me.cboWL.Margin = New System.Windows.Forms.Padding(1)
        Me.cboWL.Name = "cboWL"
        Me.cboWL.Size = New System.Drawing.Size(176, 20)
        Me.cboWL.TabIndex = 198
        Me.cboWL.Visible = False
        '
        'cboSpcCd
        '
        Me.cboSpcCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSpcCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpcCd.FormattingEnabled = True
        Me.cboSpcCd.Location = New System.Drawing.Point(462, 35)
        Me.cboSpcCd.Name = "cboSpcCd"
        Me.cboSpcCd.Size = New System.Drawing.Size(167, 20)
        Me.cboSpcCd.TabIndex = 197
        '
        'lblTSpc
        '
        Me.lblTSpc.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTSpc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTSpc.ForeColor = System.Drawing.Color.Black
        Me.lblTSpc.Location = New System.Drawing.Point(381, 35)
        Me.lblTSpc.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTSpc.Name = "lblTSpc"
        Me.lblTSpc.Size = New System.Drawing.Size(80, 21)
        Me.lblTSpc.TabIndex = 196
        Me.lblTSpc.Text = "검체코드"
        Me.lblTSpc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTest
        '
        Me.lblTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest.ForeColor = System.Drawing.Color.Black
        Me.lblTest.Location = New System.Drawing.Point(5, 58)
        Me.lblTest.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(80, 21)
        Me.lblTest.TabIndex = 195
        Me.lblTest.Text = "검사항목"
        Me.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSelTest
        '
        Me.txtSelTest.BackColor = System.Drawing.Color.Thistle
        Me.txtSelTest.ForeColor = System.Drawing.Color.Brown
        Me.txtSelTest.Location = New System.Drawing.Point(86, 58)
        Me.txtSelTest.Multiline = True
        Me.txtSelTest.Name = "txtSelTest"
        Me.txtSelTest.ReadOnly = True
        Me.txtSelTest.Size = New System.Drawing.Size(430, 43)
        Me.txtSelTest.TabIndex = 194
        '
        'cboQrygbn
        '
        Me.cboQrygbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQrygbn.Items.AddRange(New Object() {"검사그룹", "작업그룹", "W/L"})
        Me.cboQrygbn.Location = New System.Drawing.Point(231, 13)
        Me.cboQrygbn.Margin = New System.Windows.Forms.Padding(0)
        Me.cboQrygbn.Name = "cboQrygbn"
        Me.cboQrygbn.Size = New System.Drawing.Size(80, 20)
        Me.cboQrygbn.TabIndex = 193
        '
        'btnClear_test
        '
        Me.btnClear_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_test.Location = New System.Drawing.Point(32, 80)
        Me.btnClear_test.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_test.Name = "btnClear_test"
        Me.btnClear_test.Size = New System.Drawing.Size(52, 21)
        Me.btnClear_test.TabIndex = 192
        Me.btnClear_test.Text = "clear"
        Me.btnClear_test.UseVisualStyleBackColor = True
        '
        'btnCdHelp_test
        '
        Me.btnCdHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_test.Image = CType(resources.GetObject("btnCdHelp_test.Image"), System.Drawing.Image)
        Me.btnCdHelp_test.Location = New System.Drawing.Point(5, 80)
        Me.btnCdHelp_test.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_test.Name = "btnCdHelp_test"
        Me.btnCdHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_test.TabIndex = 191
        Me.btnCdHelp_test.UseVisualStyleBackColor = True
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSlip.Location = New System.Drawing.Point(86, 13)
        Me.cboSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(132, 20)
        Me.cboSlip.TabIndex = 90
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(5, 13)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 21)
        Me.Label4.TabIndex = 89
        Me.Label4.Text = "검사분야"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTGrp
        '
        Me.cboTGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTGrp.Location = New System.Drawing.Point(312, 13)
        Me.cboTGrp.Margin = New System.Windows.Forms.Padding(1)
        Me.cboTGrp.Name = "cboTGrp"
        Me.cboTGrp.Size = New System.Drawing.Size(136, 20)
        Me.cboTGrp.TabIndex = 157
        '
        'pnlCode
        '
        Me.pnlCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlCode.Controls.Add(Me.lstCode)
        Me.pnlCode.Location = New System.Drawing.Point(1, 461)
        Me.pnlCode.Name = "pnlCode"
        Me.pnlCode.Size = New System.Drawing.Size(615, 184)
        Me.pnlCode.TabIndex = 151
        Me.pnlCode.Visible = False
        '
        'lstCode
        '
        Me.lstCode.ItemHeight = 12
        Me.lstCode.Location = New System.Drawing.Point(0, 0)
        Me.lstCode.Name = "lstCode"
        Me.lstCode.Size = New System.Drawing.Size(615, 184)
        Me.lstCode.TabIndex = 145
        Me.lstCode.Visible = False
        '
        'axItemSave
        '
        Me.axItemSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axItemSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axItemSave.Location = New System.Drawing.Point(4, 5)
        Me.axItemSave.Margin = New System.Windows.Forms.Padding(1)
        Me.axItemSave.Name = "axItemSave"
        Me.axItemSave.Size = New System.Drawing.Size(212, 101)
        Me.axItemSave.TabIndex = 0
        '
        'cboTerm
        '
        Me.cboTerm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTerm.Items.AddRange(New Object() {"5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
        Me.cboTerm.Location = New System.Drawing.Point(989, 81)
        Me.cboTerm.Margin = New System.Windows.Forms.Padding(0)
        Me.cboTerm.Name = "cboTerm"
        Me.cboTerm.Size = New System.Drawing.Size(52, 20)
        Me.cboTerm.TabIndex = 167
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(890, 81)
        Me.Label11.Margin = New System.Windows.Forms.Padding(1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(98, 21)
        Me.Label11.TabIndex = 197
        Me.Label11.Text = "검사항목 간격"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGR04
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1197, 686)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cboTerm)
        Me.Controls.Add(Me.pnlCode)
        Me.Controls.Add(Me.axItemSave)
        Me.Controls.Add(Me.gbxComment)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pnlRstInfo)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.gbxABO)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGR04"
        Me.Text = "검사항목별 결과저장 및 보고"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdRef, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbxComment.ResumeLayout(False)
        Me.gbxComment.PerformLayout()
        Me.pnlRstInfo.ResumeLayout(False)
        Me.pnlRstInfo.PerformLayout()
        Me.cmuAction.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.spdSpcInfo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdSpcInfoDT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.gbxABO.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.pnlCode.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ' 화면 정리
    Private Sub sbClear_Form()
        Dim sFn As String = "Sub sbInit_Spread()"

        Try
            spdSpcInfo.MaxRows = 0
            sbDisplay_Test()
            sbClear_ResultInfo()

            Me.axResult.sbDisplay_Init("ALL")

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisp_Init()
        Dim sFn As String = "Sub sbDisp_Init()"

        Try
            STU_AUTHORITY.usrid = USER_INFO.USRID

            axResult.UseBloodBank = mbBloodBankYN
            axResult.Form = Me
            axResult.ColHiddenYn = False

            spdRef.CellNoteIndicator = FPSpreadADO.CellNoteIndicatorConstants.CellNoteIndicatorDoNotShowAndDoNotFireEvent

            If mbBloodBankYN Then Me.Text = msTitle

            sbClear_Form()

            With spdSpcInfo
                .Col = .GetColFromID("bldgbn") : .ColHidden = True
                .Col = .GetColFromID("tposition") : .ColHidden = True
                '.Col = .GetColFromID("spcnmd") : .ColHidden = True
                .CellNoteIndicator = FPSpreadADO.CellNoteIndicatorConstants.CellNoteIndicatorDoNotShowAndDoNotFireEvent
            End With

            dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
            dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")

            sbDisplay_Slip()    '-- 검사분야 
            sbDisplay_TGrp()    '-- 검사그룹

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = "", sTerm As String = "", sTestCds As String = ""

            sTgrp = COMMON.CommXML.getOneElementXML(msXMLDir, msTgrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXMLDir, msWkGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXMLDir, msQryFile, "JOB")
            sTerm = COMMON.CommXML.getOneElementXML(msXMLDir, msTermFile, "TERM")
            sTestCds = COMMON.CommXML.getOneElementXML(msXMLDir, msTestFile, "TEST")

            If cboTGrp.Items.Count > 0 Then
                If sTgrp = "" Or Val(sTgrp) > cboTGrp.Items.Count Then
                    cboTGrp.SelectedIndex = 0
                Else
                    cboTGrp.SelectedIndex = Convert.ToInt16(sTgrp)
                End If
            End If

            If cboWkGrp.Items.Count > 0 Then
                If sWkGrp = "" Or Val(sWkGrp) > cboWkGrp.Items.Count Then
                    cboWkGrp.SelectedIndex = 0
                Else
                    cboWkGrp.SelectedIndex = Convert.ToInt16(sWkGrp)
                End If
            End If

            If sJob = "" Or Val(sJob) > cboQrygbn.Items.Count Then
                cboQrygbn.SelectedIndex = 0
            Else
                cboQrygbn.SelectedIndex = Convert.ToInt16(sJob)
            End If

            If sTestCds <> "" Then
                Me.txtSelTest.Text = sTestCds.Split("^"c)(1).Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds
            End If

            Me.cboTerm.Text = sTerm

            Me.dtpDateS.Focus()

            Me.lstCode.Hide()
            Me.pnlCode.Visible = False

            sbDisplay_Spc()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub spdSpcInfo_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdSpcInfo.DblClick

        If e.col <= spdSpcInfo.GetColFromID("tposition") And e.row > 0 Then
            If MsgBox("라인 [" + e.row.ToString + "]를 삭제 하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return

            With spdSpcInfo
                .Row = e.row
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1
            End With
        Else
            '20210105 jhs 검사항목별에도 이미지 파일 보이도록 설정
            With spdSpcInfo

                moForm = Application.OpenForms(0)
                Dim sTestcd As String
                Dim sSpccd As String
                Dim sBcno As String
                Dim sTCdGbn As String

                .Row = e.row
                .Col = e.col

                sTestcd = .ColID
                .Col = .GetColFromID("spccd") : sSpccd = .Text
                .Col = .GetColFromID("bcno") : sBcno = Replace(Replace(.Text, "-", ""), " ", "")

                'Dim sSpRstYn As String = LISAPP.COMㅣM.RstFn.fnGet_SpRst_yn(IIf(Me.txtBcNo.Text = "", sBcno, Me.txtBcNo.Text).ToString.Replace("-", ""), sTestcd.Substring(0, 5))
                'Dim sFormGbn As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_FormGbn(sTestcd.Substring(0, 5), sSpccd)

                .Row = e.row
                .Col = e.col
                If .Text.Trim = "{null}" Then
                    Dim strst As New AxAckResultViewer.STRST01
                    strst.SpecialTestName = sTestcd
                    strst.BcNo = sBcno
                    strst.TestCd = sTestcd


                    strst.Left = CType(moForm.Left + (moForm.Width - strst.Width) / 2, Integer)
                    strst.Top = moForm.Top + Ctrl.menuHeight

                    strst.ShowDialog(moForm)
                End If
            End With
            '---------------------------------------------------
        End If

    End Sub

    Private Sub spdSpcInfo_KeyDownEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdSpcInfo.KeyDownEvent
        Dim sFn As String = "Sub spdResult_KeyDownEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyDownEvent)"

        Try
            Select Case e.keyCode
                Case 37, 39, 229, 27 ' 화살표 키
                    lstCode.Items.Clear()
                    lstCode.Hide()
                    pnlCode.Visible = False

                Case 38 ' 방향 위키
                    If lstCode.Visible = True Then
                        If lstCode.SelectedIndex > -1 Then
                            If lstCode.SelectedIndex > 0 Then
                                lstCode.SelectedIndex -= 1
                            End If
                        Else
                            lstCode.SelectedIndex = lstCode.Items.Count - 1
                        End If
                        e.keyCode = 0
                    End If


                Case 40 ' 방향 아래키
                    If lstCode.Visible = True Then
                        If lstCode.SelectedIndex > -1 Then
                            If lstCode.Items.Count - 1 > lstCode.SelectedIndex Then
                                lstCode.SelectedIndex += 1
                            End If
                        Else
                            lstCode.SelectedIndex = 0
                        End If
                        e.keyCode = 0
                    End If
                Case 13

                    lstCode.Items.Clear()
                    lstCode.Hide()
                    pnlCode.Visible = False

                    With spdSpcInfo
                        For intRow As Integer = .ActiveRow + 1 To .MaxRows
                            .Row = intRow
                            .Col = .ActiveCol

                            If .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit Then
                                .Row = intRow
                                .Action = FPSpreadADO.ActionConstants.ActionActiveCell

                                spdSpcInfo_ClickEvent(spdSpcInfo, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.ActiveCol, intRow))
                                Return
                            End If
                        Next
                    End With
            End Select

        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try


    End Sub


    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs, Optional ByVal rsBcNo As String = "") Handles btnQuery.Click
        Dim sFn As String = "Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click"
        Try
            DS_ProgressBar.pvisible = True

            DS_ProgressBar.MAIN_ProgressBar.Minimum = 1
            DS_ProgressBar.MAIN_ProgressBar.Maximum = 10
            DS_ProgressBar.MAIN_ProgressBar.Value = 1
            DS_ProgressBar.MAIN_ProgressBar.Step = 1

            If rsBcNo = "" Then
                Me.btnClear_ButtonClick(btnClear, New System.EventArgs)
            End If

            Dim sWkYmd As String = "", sWkGrpCd As String = "", sWkNoS As String = "", sWkNoE As String = ""
            Dim sDateS As String = "", sDateE As String = ""

            Dim sRstNull As String = CStr(IIf(chkRstNull.Checked, "1", "0"))
            Dim sRstReg As String = CStr(IIf(chkRstReg.Checked, "1", "0"))
            Dim sRstFn As String = IIf(chkRstFn.Checked, "1", "0").ToString()

            If sRstNull + sRstReg + sRstFn = "000" Then
                MsgBox(Me.chkRstNull.Text + ", " + Me.chkRstReg.Text + ", " + Me.chkRstFn.Text + " 중 하나이상을 선택하여 주십시요!!", MsgBoxStyle.Information)
                Return
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
            '>

            If Me.txtSelTest.Text = "" Then
                MsgBox("검사항목을 선택하세요.!!")
                Return
            End If

            spdSpcInfo.ReDraw = False

            If cboQrygbn.Text = "작업그룹" Then
                sWkYmd = dtpDateS.Text.Replace("-", "").PadRight(8, "0"c)

                sWkGrpCd = Ctrl.Get_Code(cboWkGrp)
                sWkNoS = Me.txtWkNoS.Text.PadLeft(4, "0"c)
                sWkNoE = Me.txtWkNoE.Text.PadLeft(4, "0"c)

                If sWkNoS <> "" Then
                    If IsNumeric(sWkNoS) = False Then
                        MsgBox("작업번호에 숫자를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If
                Else
                    sWkNoS = "0000"
                End If

                If sWkNoE <> "" Then
                    If IsNumeric(sWkNoE) = False Then
                        MsgBox("작업번호에 숫자를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If
                Else
                    sWkNoE = "9999"
                End If
            Else
                sDateS = dtpDateS.Text.Replace("-", "").Replace(" ", "")
                sDateE = dtpDateE.Text.Replace("-", "").Replace(" ", "")
            End If


            Dim sTestCds As String = Me.txtSelTest.Tag.ToString.Split("^"c)(0)

            Dim dt As New DataTable

            If Me.cboQrygbn.Text.StartsWith("W/L") And Me.cboWL.Items.Count > 0 Then
                Dim sWLUid As String = Me.cboWL.Text.Split("|"c)(2)
                Dim sWLYmd As String = Me.cboWL.Text.Split("|"c)(1)
                Dim sWLTitle As String = Me.cboWL.Text.Split("|"c)(0).Replace("(" + sWLYmd + ")", "").Trim

                dt = LISAPP.APP_R.RstFn.fnGet_SpcList_WL(sWLUid, sWLYmd, sWLTitle, sRstNull + sRstReg + sRstFn)
            Else
                If lblTitleDt.Text = "보고일" Then
                    dt = LISAPP.APP_R.RstFn.fnGet_SpcList_Test2(sTestCds.Replace("|", ","), sWkYmd, sWkGrpCd, sWkNoS, sWkNoE, sRstNull + sRstReg + sRstFn, sDateS, sDateE, rsBcNo)
                Else
                    dt = LISAPP.APP_R.RstFn.fnGet_SpcList_Test(sTestCds.Replace("|", ","), sWkYmd, sWkGrpCd, sWkNoS, sWkNoE, sRstNull + sRstReg + sRstFn, sDateS, sDateE, rsBcNo)
                End If

            End If

            sbDisplay_PatInfo(dt)

            DS_ProgressBar.PerformStep()

            dt = Nothing

            spdSpcInfo.ReDraw = True
            sbSet_RstcdInfo(sTestCds.Replace("|", ","), sWkGrpCd)   ' 검사항목별 결과코드 조회

            DS_ProgressBar.pvisible = False

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            DS_ProgressBar.pvisible = False
        End Try

    End Sub

    ' 검사항목별 결과코드 데이터 테이블 
    Private Sub sbSet_RstcdInfo(ByVal rsTestCd As String, ByVal rsWkGrpCd As String)
        Dim sFn As String = "Sub sbSet_RstcdInfo()"
        Try
            rsTestCd = "'" + rsTestCd.Replace(",", "','") + "'"
            m_dt_RstCd = LISAPP.COMM.RstFn.fnGet_Test_RstCdList(rsTestCd, rsWkGrpCd)
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub spdSpcInfo_KeyUpEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdSpcInfo.KeyUpEvent
        Dim sFn As String = "Sub spdOrdList_KeyUpEvent(Object, AxFPSpreadADO._DSpreadEvents_KeyUpEvent) Handles spdOrdListR.KeyUpEvent"

        Dim strTclsCd As String = ""
        Dim strRst As String = ""
        Dim intPos As Integer = gbxComment.Location.Y

        Select Case e.keyCode
            Case 37, 38, 39, 40, 229 ' 화살표 키                
            Case 27     ' ESC
            Case 123    ' F12
            Case 13
            Case Else
                With Me.spdSpcInfo
                    If .ActiveCol < .GetColFromID("tposition") Then
                        Exit Sub
                    End If
                    .Row = .ActiveRow
                    .Col = .ActiveCol : strTclsCd = .ColID : strRst = .Text

                    DP_Common.sbDispaly_test_rstcd(m_dt_RstCd, Convert.ToString(strTclsCd), lstCode)  ' 검사항목별 결과코드 표시
                    DP_Common.sbFindPosition(lstCode, Convert.ToString(strRst))

                    If pnlCode.Visible = False Then
                        If lstCode.Items.Count > 0 Then
                            pnlCode.Visible = True
                            lstCode.Focus()
                        Else
                            pnlCode.Visible = False
                        End If
                    End If
                End With
        End Select
    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbClear_Form()
    End Sub

    '결과저장 버튼 클릭
    Private Sub btnReg_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        sbReg("1")
    End Sub

    Private Sub txtCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown
        Dim sFn As String = "Sub txtCode_KeyDown(Object, System.Windows.Forms.KeyEventArgs) Handles txtCode.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return

        If spdSpcInfo.ActiveCol <= spdSpcInfo.GetColFromID("tposition") Then
            MsgBox("결과값을 일괄 적용할 검사항목을 선택하세요.")
            Return
        End If

        Try
            Dim sTestCd As String = ""
            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtCode)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            With spdSpcInfo
                .Col = .ActiveCol : sTestCd = .ColID
            End With

            Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_Test_RstCdList("'" + sTestCd + "'", "")
            If Me.txtCode.Text <> "" Then
                Dim a_dr As DataRow() = dt.Select("keypad = '" + Me.txtCode.Text + "'", "")
                dt = Fn.ChangeToDataTable(a_dr)
            End If

            objHelp.FormText = "결과코드"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("keypad", "KEY", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("rstcont", "내용", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtCode.Height + 80, dt)

            If alList.Count > 0 Then
                Dim strRstCont As String = alList.Item(0).ToString.Split("|"c)(1)

                Me.txtCode.Text = strRstCont
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnMW_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMW.Click
        sbReg("2")

    End Sub

    Private Sub btnFN_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFN.Click
        sbReg("3")
    End Sub

    Private Sub spdSpcInfo_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdSpcInfo.Change
        If e.row < 1 Or e.col <= spdSpcInfo.GetColFromID("tposition") Then Exit Sub

        With spdSpcInfo
            .Row = e.row
            .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
            .Col = .GetColFromID("spcnmd") : Dim sSpcNmd As String = .Text
            .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text

            .Row = e.row : .Col = e.col : Dim sOrgRst As String = .Text
            .Row = 0 : .Col = e.col : Dim sTestCd As String = .ColID

            Dim objTInfo As New AxAckResult.RST_INFO

            Me.axResult.TestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", sSpcCd + ",")

            objTInfo = axResult.fnSet_Result_Test(sBcNo, sTestCd, sOrgRst)

            If objTInfo.IUD = "1" Then
                .Row = e.row
                .Col = .GetColFromID("chk") : .Text = "1"
            End If

            sbDisplay_ResultInfo(objTInfo, sSpcNmd)

        End With

    End Sub


    Private Sub spdSpcInfo_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdSpcInfo.ClickEvent
        If e.row < 1 Then Exit Sub

        With spdSpcInfo
            .Row = e.row
            .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
            .Col = .GetColFromID("regno") : Dim sRegNo As String = .Text
            .Col = .GetColFromID("patnm") : Dim sPatNm As String = .Text
            .Col = .GetColFromID("sexage") : Dim sSexAge As String = .Text
            .Col = .GetColFromID("spcnmd") : Dim sSpcNmd As String = .Text
            .Col = .GetColFromID("spccd") : Dim sSpccd As String = .Text

            If axResult.BCNO <> sBcNo Then
                Me.axResult.RegNo = sRegNo
                Me.axResult.PatName = sPatNm
                Me.axResult.SexAge = sSexAge
                Me.axResult.TestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", sSpccd + ",")
                Me.axResult.TgrpCds = ""
                Me.axResult.WKgrpCd = ""
                Me.axResult.EqCd = ""
                Me.axResult.BcNoAll = False
                Me.axResult.sbDisplay_Data(sBcNo)
            End If

            If e.col > .GetColFromID("tposition") Then
                .Row = e.row : .Col = e.col : Dim sOrgRst As String = .Text
                .Row = 0 : .Col = e.col : Dim sTestCd As String = .ColID

                Dim objTInfo As New AxAckResult.RST_INFO

                objTInfo = axResult.fnSet_Result_Test(sBcNo, sTestCd, sOrgRst)

                sbDisplay_ResultInfo(objTInfo, sSpcNmd)

            Else
                sbClear_ResultInfo()
            End If
        End With

    End Sub

    Private Sub spdSpcInfo_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdSpcInfo.LeaveCell

        If e.newRow < 1 Or (e.col = e.newCol And e.row = e.newRow) Then Exit Sub

        spdSpcInfo_ClickEvent(spdSpcInfo, New AxFPSpreadADO._DSpreadEvents_ClickEvent(e.newCol, e.newRow))

    End Sub

    Private Sub txtComment_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComment.Validated
        Dim sBCNO As String
        Dim sTCLSCD As String
        With spdRef
            .Col = 1
            .Row = 1
            sBCNO = .CellNote
            .Col = 2
            .Row = 1
            sTCLSCD = .CellNote
        End With

        Debug.WriteLine(txtComment.Text)
    End Sub

    Private Sub lstCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstCode.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.Escape
                lstCode.Hide()
                pnlCode.Visible = False
            Case Windows.Forms.Keys.Enter
                lstCode_DoubleClick(lstCode, New System.EventArgs())
        End Select

    End Sub

    Private Sub lstCode_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCode.DoubleClick
        With spdSpcInfo
            Dim arlRstCd() As String
            Dim strRst As String = ""

            If lstCode.SelectedIndex > -1 Then
                For i As Integer = 0 To lstCode.SelectedIndices.Count - 1
                    arlRstCd = Split(lstCode.Items(lstCode.SelectedIndices(i)).ToString(), Chr(9))
                    strRst = arlRstCd(1)
                Next

                .Row = .ActiveRow
                .Col = .ActiveCol : .Text = strRst

                .Col = .GetColFromID("bcno") : Dim strBcNo As String = .Text.Replace("-", "")
                .Col = .GetColFromID("regno") : Dim strRegNo As String = .Text
                .Col = .GetColFromID("patnm") : Dim strPatNm As String = .Text
                .Col = .GetColFromID("sexage") : Dim strSexAge As String = .Text

                .Row = 0
                .Col = .ActiveCol : Dim strTclsCd As String = .ColID

                Dim objTInfo As New AxAckResult.RST_INFO

                objTInfo = axResult.fnSet_Result_Test(strBcNo, strTclsCd, strRst)

            End If

            lstCode.Hide()
            pnlCode.Visible = False

            .Focus()
        End With
    End Sub

    Private Sub FGR04_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_ButtonClick(Nothing, Nothing)
            Case Keys.F5
                btnQuery_Click(Nothing, Nothing)

            Case Keys.F9
                If btnReg.Enabled = True Then btnReg_ButtonClick(btnReg, New System.EventArgs)
            Case Keys.F11
                If btnMW.Enabled = True Then btnMW_ButtonClick(btnMW, New System.EventArgs)
            Case Keys.F12
                If btnFN.Enabled = True Then btnFN_ButtonClick(btnFN, New System.EventArgs)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbClear_Form()

        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        If cboWkGrp.SelectedIndex >= 0 Then
            COMMON.CommXML.setOneElementXML(msXMLDir, msWkGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
        End If

    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged

        sbClear_Form()

        sbDisplay_WkGrp()
        sbDisplay_Date_Setting()
        sbDisplay_Spc()
        sbDisplay_wl()

        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)

    End Sub


    Private Sub FGR04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFn As String = "Sub FGR04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load"

        sbDisp_Init()
        sbDisplay_Test()

        Me.axItemSave.FORMID = "R"
        Me.axItemSave.USRID = USER_INFO.USRID
        Me.axItemSave.ITEMGBN = ""
        Me.axItemSave.SPCGBN = "NONE"
        Me.axItemSave.BloodBankYn = mbBloodBankYN
        Me.axItemSave.AllPartYn = False
        Me.axItemSave.sbDisplay_ItemList()

        If mbBloodBankYN Then
            spdSpcInfo.OperationMode = FPSpreadADO.OperationModeConstants.OperationModeRow
            spdSpcInfo.SelBackColor = System.Drawing.Color.FromArgb(213, 215, 255)
        End If
    End Sub

    Private Sub btnAction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAction.Click

        If txtCode.Text = "" Then Return
        If spdSpcInfo.ActiveCol <= spdSpcInfo.GetColFromID("tposition") Then
            MsgBox("결과값을 일괄 적용할 검사항목을 선택하세요.")
            Return
        End If

        For intRow As Integer = 1 To spdSpcInfo.MaxRows
            With spdSpcInfo
                .Row = intRow
                .Col = .ActiveCol
                If .BackColor = Color.White And .Text = "" Then
                    .Text = txtCode.Text
                End If
            End With
        Next

    End Sub

    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click

        For intRow As Integer = 1 To spdSpcInfo.MaxRows

            With spdSpcInfo
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1

                    intRow -= 1
                End If
            End With

            If intRow < 0 Then Exit For
        Next
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim strTime As String = Format(Now, "yyMMddhhmm").ToString

        With spdSpcInfo
            .ReDraw = False

            .Row = 1
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            For intCol As Integer = 1 To .MaxCols
                .Row = 0 : .Col = intCol : Dim strTmp As String = .Text
                .Row = 1 : .Col = intCol : .Text = strTmp

            Next
            If spdSpcInfo.ExportToExcel("c:\검사항목별_" + strTime + ".xls", "검사항목별 결과등록", "") Then
                Process.Start("c:\검사항목별_" + strTime + ".xls")
            End If

            .Row = 1
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1
            .ReDraw = True
        End With

    End Sub

    Private Sub chkSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSel.Click

        With spdSpcInfo
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk") : .Text = IIf(chkSel.Checked, "1", "").ToString
            Next
        End With

    End Sub

    Private Sub txtWkNoS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWkNoS.Click
        txtWkNoS.SelectAll()
    End Sub

    Private Sub txtWkNoE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWkNoE.Click
        txtWkNoE.SelectAll()
    End Sub


    Private Sub txtWkNoS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWkNoS.KeyDown
        Dim sFn As String = ""

        Try
            If e.KeyCode = Keys.Tab Then
                txtWkNoE.SelectAll()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtWkNoS_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtWkNoS.PreviewKeyDown

        If e.KeyCode = Keys.Tab Then
            txtWkNoE.SelectAll()
        End If
    End Sub

    Private Sub txtQuery_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown

        Dim dt As New DataTable
        Dim bFind As Boolean = False
        Dim sBcNo As String = ""

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        If Trim(txtBcNo.Text).Length = 0 Then
            'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "검체번호를 입력해주세요.!!")
        Else
            sBcNo = Trim(txtBcNo.Text)

            If Len(sBcNo) = 11 Or Len(sBcNo) = 12 Then
                sBcNo = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(sBcNo.Substring(0, 11))
            End If

            If sBcNo.Length = 14 Then sBcNo += "0"

            Me.txtBcNo.Text = sBcNo

            For ix As Integer = 1 To spdSpcInfo.MaxRows
                With spdSpcInfo
                    .Row = ix
                    .Col = .GetColFromID("bcno") : Dim sBcNo_t As String = .Text

                    If sBcNo_t = sBcNo Then
                        bFind = True
                        Exit For
                    End If
                End With
            Next

            If bFind Then
                MessageBox.Show("이미 리스트에 있는 검체입니다.!!")
                Me.txtBcNo.Text = ""
            Else
                btnQuery_Click(Nothing, Nothing, sBcNo)
                Me.txtBcNo.Text = ""
            End If
        End If

        Me.txtBcNo.Focus()

    End Sub

    Private Sub Form_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        COMMON.CommXML.setOneElementXML(msXMLDir, msTestFile, "TEST", Me.txtSelTest.Tag.ToString)
        MdiTabControl.sbTabPageMove(Me)

    End Sub

    Private Sub axItemSave_ListDblClick(ByVal rsItemCds As System.String, ByVal rsItemNms As System.String) Handles axItemSave.ListDblClick
        If rsItemCds <> "" Then
            Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
            Me.txtSelTest.Text = rsItemNms.Replace("|", ",")

            sbDisplay_Test()
        End If
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
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list(sPartSlip, sTGrpCd, sWGrpCd, , Ctrl.Get_Code(cboSpcCd))
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

            sbDisplay_Test()

            COMMON.CommXML.setOneElementXML(msXMLDir, msTestFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Spc()
        COMMON.CommXML.setOneElementXML(msXMLDir, msTgrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)

    End Sub

    Private Sub cboQrygbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQrygbn.SelectedIndexChanged

        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Date_Setting()

        COMMON.CommXML.setOneElementXML(msXMLDir, msQryFile, "JOB", cboQrygbn.SelectedIndex.ToString)

    End Sub

    Private Sub btnClear_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_test.Click
        Me.txtSelTest.Tag = "" : Me.txtSelTest.Text = ""

        spdSpcInfo.MaxRows = 0
        sbDisplay_Test()

    End Sub

    Private Sub cboTerm_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTerm.SelectedValueChanged

        COMMON.CommXML.setOneElementXML(msXMLDir, msTermFile, "TERM", cboTerm.Text)

    End Sub

    Private Sub dtpDateS_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDateS.CloseUp, dtpDateE.CloseUp

        If Me.cboQrygbn.Text = "W/L" Then sbDisplay_wl()

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

            sbDisplay_Test()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lblTitleDt_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTitleDt.DoubleClick

        Try

        

            If cboQrygbn.Text = "검사그룹" Then

                If lblTitleDt.Text = "접수일자" Then

                    lblTitleDt.Text = "보고일"

                ElseIf lblTitleDt.Text = "보고일" Then

                    lblTitleDt.Text = "접수일자"

                End If
                'lblTitleDt.Text = "보고일"

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    
End Class
