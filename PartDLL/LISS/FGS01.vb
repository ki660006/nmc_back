'>>> 이상자 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst

Imports LISAPP.APP_S.RstSrh

Public Class FGS01
    Inherits System.Windows.Forms.Form

    Private mbMicroBioYn As Boolean = False
    Private mbLoaded As Boolean = False

    Private msSEP As String = Convert.ToChar(1)
    Private msSEP_Display As String = ", "

    Private Const msXmlDir As String = "\XML"
    Private msTestFile As String = Application.StartupPath & msXmlDir & "\FGS01_TEST.XML"
    Private msWkGrpFile As String = Application.StartupPath & msXmlDir & "\FGS01_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath & msXmlDir & "\FGS01_TGRP.XML"
    Private msSlipFile As String = Application.StartupPath & msXmlDir & "\FGS01_SLIP.XML"
    Private msSpcFile As String = Application.StartupPath & msXmlDir & "\FGS01_SPC.XML"
    Private msQryFile As String = Application.StartupPath & msXmlDir & "\FGS10_Qry.XML"

    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents axItemSave As AxAckItemSave.ITEMSAVE
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents cboSpcCd As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents lblWk As System.Windows.Forms.Label
    Friend WithEvents txtSelTest As System.Windows.Forms.TextBox
    Friend WithEvents txtWkNoE As System.Windows.Forms.TextBox
    Friend WithEvents cboQrygbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtWkNoS As System.Windows.Forms.TextBox
    Friend WithEvents btnClear_test As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTitleDt As System.Windows.Forms.Label
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboWkGrp As System.Windows.Forms.ComboBox
    Friend WithEvents cboTGrp As System.Windows.Forms.ComboBox
    Friend WithEvents pnlAbOpt As System.Windows.Forms.Panel
    Friend WithEvents rdoAnd As System.Windows.Forms.RadioButton
    Friend WithEvents chkC As System.Windows.Forms.CheckBox
    Friend WithEvents chkP As System.Windows.Forms.CheckBox
    Friend WithEvents rdoOr As System.Windows.Forms.RadioButton
    Friend WithEvents chkD As System.Windows.Forms.CheckBox
    Friend WithEvents chkA As System.Windows.Forms.CheckBox
    Friend WithEvents lblJudg As System.Windows.Forms.Label
    Friend WithEvents chkFN As System.Windows.Forms.CheckBox
    Friend WithEvents chkSort_pat As System.Windows.Forms.CheckBox
    Friend WithEvents cboPart As System.Windows.Forms.ComboBox
    Friend WithEvents chkcvr As System.Windows.Forms.CheckBox

    Private m_tooltip As New Windows.Forms.ToolTip

    Private Sub sbDisp_Init()
        Try
            With spdList
                .MaxRows = 0
            End With

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
            Me.dtpDateE.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")

            sbDisplay_slip()    '-- 검사분야 
            sbDisplay_tgrp()    '-- 검사그룹

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = "", sTestCds As String = "", sSpc As String = ""

            sTgrp = COMMON.CommXML.getOneElementXML(msXmlDir, msTgrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXmlDir, msWkGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXmlDir, msQryFile, "JOB")
            sTestCds = COMMON.CommXML.getOneElementXML(msXmlDir, msTestFile, "TEST")
            sSpc = COMMON.CommXML.getOneElementXML(msXmlDir, msSpcFile, "SPC")

            If Me.cboTGrp.Items.Count > 0 Then
                If sTgrp = "" Or Val(sTgrp) > Me.cboTGrp.Items.Count Then
                    cboTGrp.SelectedIndex = 0
                Else
                    cboTGrp.SelectedIndex = Convert.ToInt16(sTgrp)
                End If
            End If

            If Me.cboWkGrp.Items.Count > 0 Then
                If sWkGrp = "" Or Val(sWkGrp) > Me.cboWkGrp.Items.Count Then
                    Me.cboWkGrp.SelectedIndex = 0
                Else
                    Me.cboWkGrp.SelectedIndex = Convert.ToInt16(sWkGrp)
                End If
            End If

            If Me.cboSpcCd.Items.Count > 0 Then
                If sSpc = "" Or Val(sSpc) > Me.cboSpcCd.Items.Count Then
                    Me.cboSpcCd.SelectedIndex = 0
                Else
                    Me.cboSpcCd.SelectedIndex = Convert.ToInt16(sSpc)
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

            sbDisplay_Spc()

            Me.dtpDateS.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_slip()
        Dim dt As DataTable

        Try
            Dim sTmp As String = ""

            dt = LISAPP.COMM.CdFn.fnGet_Slip_List(, , , mbMicroBioYn)

            Me.cboSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            sTmp = COMMON.CommXML.getOneElementXML(msXmlDir, msSlipFile, "SLIP")

            If sTmp = "" Or Val(sTmp) > cboSlip.Items.Count Then
                Me.cboSlip.SelectedIndex = 0
            Else
                Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            End If

        Catch ex As Exception
            CDHELP .FGCDHELPFN .fn_PopMsg (Me,  "E"c, ex.Message )
        End Try
    End Sub

    Private Sub sbDisplay_tgrp()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List(Not mbMicroBioYn, mbMicroBioYn)

            Me.cboTGrp.Items.Clear()
            Me.cboTGrp.Items.Add("[  ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboTGrp.Items.Add("[" + dt.Rows(ix).Item("tgrpcd").ToString + "] " + dt.Rows(ix).Item("tgrpnmd").ToString)
            Next
            If Me.cboTGrp.Items.Count > 0 Then Me.cboTGrp.SelectedIndex = 0
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        
    End Sub

    Private Sub sbDisplay_wkgrp()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_WKGrp_List(Ctrl.Get_Code(cboSlip))

            Me.cboWkGrp.Items.Clear()

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString)
            Next

            If Me.cboWkGrp.Items.Count > 0 Then cboWkGrp.SelectedIndex = 0
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        
    End Sub

    Private Sub sbDisplay_Spc()

        Try
            Dim sPartCd As String = ""
            Dim sSlipCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sWGrpCd As String = ""

            If Me.cboQrygbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(cboTGrp)
            Else
                If Ctrl.Get_Code(cboSlip) <> "" Then
                    sPartCd = Ctrl.Get_Code(cboSlip).Substring(0, 1)
                    sSlipCd = Ctrl.Get_Code(cboSlip).Substring(1, 1)
                End If
                If Me.cboQrygbn.Text = "검사그룹" Then sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            End If

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Spc_List("", sPartCd, sSlipCd, sTGrpCd, sWGrpCd, "", "")

            Me.cboSpcCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSpcCd.Items.Add("[" + dt.Rows(ix).Item("spccd").ToString.Trim + "] " + dt.Rows(ix).Item("spcnmd").ToString.Trim)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Date_Setting()

        If Me.cboQrygbn.Text = "검사그룹" Then
            Me.lblTitleDt.Text = "결과일자"

            Me.dtpDateE.Visible = True
            Me.lblDate.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            Me.txtWkNoS.Visible = False : Me.txtWkNoE.Visible = False
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False

        ElseIf Me.cboWkGrp.Text <> "" Then
            Me.lblTitleDt.Text = "작업일자"

            Me.dtpDateE.Visible = False
            Me.lblDate.Visible = False

            Me.txtWkNoS.Visible = True : Me.txtWkNoE.Visible = True
            Me.lblWk.Visible = True

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True

            Dim sWkNoGbn As String = Me.cboWkGrp.Text.Split("|"c)(1)

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
        End If
    End Sub

    Private Sub sbDisplay_init_spd()
        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            With spd
                .Col = .GetColFromID("wkno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .Col = .GetColFromID("bcno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("regno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("patnm")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("sexage")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("doctornm")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("deptward")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("tkdt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("spcnmd")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .MaxRows = 0
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "작업번호" : .WIDTH = "120" : .FIELD = "wkno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검체번호" : .WIDTH = "140" : .FIELD = "bcno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "등록번호" : .WIDTH = "95" : .FIELD = "regno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "성명" : .WIDTH = "80" : .FIELD = "patnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "성별/나이" : .WIDTH = "70" : .FIELD = "sexage"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "의뢰의사" : .WIDTH = "60" : .FIELD = "doctornm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "진료과/병동" : .WIDTH = "120" : .FIELD = "deptward"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "접수일시" : .WIDTH = "120" : .FIELD = "tkdt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검체명" : .WIDTH = "80" : .FIELD = "spcnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검사명" : .WIDTH = "120" : .FIELD = "tnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "결과" : .WIDTH = "100" : .FIELD = "viewrst"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "결과일시" : .WIDTH = "120" : .FIELD = "rstdt"
        End With
        alItems.Add(stu_item)

        Return alItems

    End Function

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList

            With spdList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    Dim strBuf() As String = rsTitle_Item.Split("|"c)
                    Dim arlItem As New ArrayList

                    For intIdx As Integer = 0 To strBuf.Length - 1

                        If strBuf(intIdx) = "" Then Exit For

                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                        If intCol > 0 Then

                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                            .Row = intRow
                            .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                        End If
                    Next

                    Dim objPat As New FGS00_PATINFO

                    With objPat
                        .alItem = arlItem
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT

                prt.msTitle = "이상자리스트"
                prt.maPrtData = arlPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Clear()
        Me.spdList.MaxRows = 0

    End Sub

    Private Sub sbDisplay_Detail(ByVal ra_dr As DataRow())

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                .MaxRows = 0
                If ra_dr.Length = 0 Then Return

                .ReDraw = False

                'Data 표시
                Dim iRow As Integer = 0

                '일단 MaxRows 확보
                .MaxRows = ra_dr.Length

                For i As Integer = 1 To ra_dr.Length
                    iRow = i

                    For j As Integer = 1 To ra_dr(i - 1).Table.Columns.Count
                        Dim iCol As Integer = .GetColFromID(ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower)

                        If iCol > 0 Then
                            .SetText(iCol, iRow, ra_dr(i - 1).Item(j - 1).ToString().Trim)

                            If ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower = "panicmark" Then
                                If ra_dr(i - 1).Item(j - 1).ToString() = "P" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkP.BackColor
                                    .ForeColor = Me.chkP.ForeColor
                                End If

                            ElseIf ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower = "deltamark" Then
                                If ra_dr(i - 1).Item(j - 1).ToString() = "D" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkD.BackColor
                                    .ForeColor = Me.chkD.ForeColor
                                End If

                            ElseIf ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower = "criticalmark" Then
                                If ra_dr(i - 1).Item(j - 1).ToString() = "C" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkC.BackColor
                                    .ForeColor = Me.chkC.ForeColor
                                End If

                            ElseIf ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower = "alertmark" Then
                                If ra_dr(i - 1).Item(j - 1).ToString() = "A" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkA.BackColor
                                    .ForeColor = Me.chkA.ForeColor
                                End If

                            ElseIf ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower = "rstflg" Then
                                .Col = iCol
                                .Row = iRow

                                Select Case ra_dr(i - 1).Item(j - 1).ToString().Trim
                                    Case "3"
                                        .Text = FixedVariable.gsRstFlagF
                                        .ForeColor = FixedVariable.g_color_FN
                                    Case "2"
                                        .Text = FixedVariable.gsRstFlagM
                                    Case "1"
                                        .Text = FixedVariable.gsRstFlagR
                                End Select

                            ElseIf ra_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower = "cvryn" Then '<< JJH CVR등록 여부 표시
                                If ra_dr(i - 1).Item(j - 1).ToString() = "Y" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Color.LightPink
                                    .ForeColor = Color.Black
                                End If
                            End If
                        End If
                    Next
                Next

                '.set_ColWidth(.GetColFromID("workno"), .get_MaxTextColWidth(.GetColFromID("workno")))
                '.set_ColWidth(.GetColFromID("bcno"), .get_MaxTextColWidth(.GetColFromID("bcno")))
                '.set_ColWidth(.GetColFromID("bfbcno"), .get_MaxTextColWidth(.GetColFromID("bfbcno")))
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplay_List_tgrp()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            Dim sSlipCd As String = ""
            Dim sTgrpCd As String = Ctrl.Get_Code(Me.cboTGrp)
            Dim sTestCds As String = ""
            If Me.txtSelTest.Text <> "" Then sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")

            If sTgrpCd = "" Then sSlipCd = Ctrl.Get_Code(Me.cboSlip)

            Dim dt As DataTable = fnGet_AbnormalList_Tgrp(sSlipCd, sTgrpCd, Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), sTestCds, Me.chkFN.Checked, mbMicroBioYn, Me.chkcvr.Checked)

            Dim sAbFilter As String = fnFind_Abnormal_Flag()

            Dim a_dr As DataRow()
            If Me.chkSort_pat.Checked Then
                a_dr = dt.Select(sAbFilter, "patnm, regno, workno, tkdt, bcno, sort1, sort2, testspc")
            Else
                a_dr = dt.Select(sAbFilter, "workno, tkdt, bcno, sort1, sort2, testspc")
            End If

            sbDisplay_Detail(a_dr)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_List_wkno()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try

            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)
            Dim sWkgrpCd As String = Ctrl.Get_Code(Me.cboWkGrp)
            Dim sWkYmd As String = "", sWkNoS As String = "", sWkNoE As String = ""
            Dim sTestCds As String = ""
            If Me.txtSelTest.Text <> "" Then sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")

            If sWkgrpCd = "" Then
                MsgBox("작업그룹을 선택하여 주십시요!!")
                Return
            End If

            sWkYmd = Me.dtpDateS.Text.Replace("-", "").PadRight(8, "0"c)
            sWkNoS = Me.txtWkNoS.Text.PadLeft(4, "0"c)
            sWkNoE = IIf(Me.txtWkNoE.Text = "", "9999", Me.txtWkNoE.Text).ToString.PadLeft(4, "0"c)

            If Not IsNumeric(sWkNoS) Or Not IsNumeric(sWkNoE) Then
                MsgBox("작업번호에 숫자를 입력하여 주십시요!!")
                Return
            End If

            Dim dt As DataTable = fnGet_AbnormalList_WGrp(sWkYmd, sWkgrpCd, sWkNoS, sWkNoE, sTestCds, Me.chkFN.Checked, mbMicroBioYn, Me.chkcvr.Checked)

            Dim sAbFilter As String = fnFind_Abnormal_Flag()

            Dim a_dr As DataRow()

            If Me.chkSort_pat.Checked Then
                a_dr = dt.Select(sAbFilter, "patnm, regno, workno, tkdt, bcno, sort1, sort2, testspc")
            Else
                a_dr = dt.Select(sAbFilter, "wkno, tkdt, bcno, sort1, sort2, testspc")
            End If

            sbDisplay_Detail(a_dr)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Function fnFind_Abnormal_Flag() As String

        Try
            Dim sReturn As String = ""

            Dim sAndOr As String = ""

            If Me.rdoOr.Checked Then
                sAndOr = " or "
            Else
                sAndOr = " and "
            End If

            If Me.chkP.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "panicmark = 'P'"
            End If

            If Me.chkD.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "deltamark = 'D'"
            End If

            If Me.chkC.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "criticalmark = 'C'"
            End If

            If Me.chkA.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "alertmark = 'A'"
            End If

            Return sReturn

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            Return ""
        End Try
    End Function


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
    End Sub

    Public Sub New(ByVal rbMicroBioYn As Boolean)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        mbMicroBioYn = rbMicroBioYn

        If mbMicroBioYn Then
            msTestFile = Application.StartupPath & msXmlDir & "\FGS01_M_TEST.XML"
            msWkGrpFile = Application.StartupPath & msXmlDir & "\FGS01_M_WKGRP.XML"
            msTgrpFile = Application.StartupPath & msXmlDir & "\FGS01_M_TGRP.XML"
            msSlipFile = Application.StartupPath & msXmlDir & "\FGS01_M_SLIP.XML"
            msSpcFile = Application.StartupPath & msXmlDir & "\FGS01_M_SPC.XML"
            msQryFile = Application.StartupPath & msXmlDir & "\FGS01_M_Qry.XML"

            Me.Text = Me.Text + "(미생물)"
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS01))
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnQuery = New CButtonLib.CButton()
        Me.btnPrint = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cboPart = New System.Windows.Forms.ComboBox()
        Me.chkSort_pat = New System.Windows.Forms.CheckBox()
        Me.chkFN = New System.Windows.Forms.CheckBox()
        Me.pnlAbOpt = New System.Windows.Forms.Panel()
        Me.rdoAnd = New System.Windows.Forms.RadioButton()
        Me.chkC = New System.Windows.Forms.CheckBox()
        Me.chkP = New System.Windows.Forms.CheckBox()
        Me.rdoOr = New System.Windows.Forms.RadioButton()
        Me.chkD = New System.Windows.Forms.CheckBox()
        Me.chkA = New System.Windows.Forms.CheckBox()
        Me.lblJudg = New System.Windows.Forms.Label()
        Me.cboSpcCd = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.lblTest = New System.Windows.Forms.Label()
        Me.lblWk = New System.Windows.Forms.Label()
        Me.txtSelTest = New System.Windows.Forms.TextBox()
        Me.txtWkNoE = New System.Windows.Forms.TextBox()
        Me.cboQrygbn = New System.Windows.Forms.ComboBox()
        Me.txtWkNoS = New System.Windows.Forms.TextBox()
        Me.btnClear_test = New System.Windows.Forms.Button()
        Me.btnCdHelp_test = New System.Windows.Forms.Button()
        Me.cboSlip = New System.Windows.Forms.ComboBox()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTitleDt = New System.Windows.Forms.Label()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.cboWkGrp = New System.Windows.Forms.ComboBox()
        Me.cboTGrp = New System.Windows.Forms.ComboBox()
        Me.axItemSave = New AxAckItemSave.ITEMSAVE()
        Me.chkcvr = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.pnlAbOpt.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(4, 132)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1063, 462)
        Me.Panel1.TabIndex = 53
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1059, 458)
        Me.spdList.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.btnQuery)
        Me.Panel2.Controls.Add(Me.btnPrint)
        Me.Panel2.Controls.Add(Me.btnExcel)
        Me.Panel2.Controls.Add(Me.btnClear)
        Me.Panel2.Controls.Add(Me.btnExit)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 597)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1071, 32)
        Me.Panel2.TabIndex = 125
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
        Me.btnQuery.Location = New System.Drawing.Point(567, 3)
        Me.btnQuery.Margin = New System.Windows.Forms.Padding(0)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(96, 25)
        Me.btnQuery.TabIndex = 197
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems2
        Me.btnPrint.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint.Corners.All = CType(6, Short)
        Me.btnPrint.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint.Corners.LowerRight = CType(6, Short)
        Me.btnPrint.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint.Corners.UpperRight = CType(6, Short)
        Me.btnPrint.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint.FocalPoints.CenterPtX = 0.5!
        Me.btnPrint.FocalPoints.CenterPtY = 0.0!
        Me.btnPrint.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker4
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(665, 3)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(96, 25)
        Me.btnPrint.TabIndex = 196
        Me.btnPrint.Text = "인쇄"
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems3
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker6
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(763, 3)
        Me.btnExcel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(100, 25)
        Me.btnExcel.TabIndex = 195
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems4
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
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker8
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(865, 3)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 194
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems5
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker10
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(967, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(97, 25)
        Me.btnExit.TabIndex = 193
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.chkcvr)
        Me.GroupBox6.Controls.Add(Me.cboPart)
        Me.GroupBox6.Controls.Add(Me.chkSort_pat)
        Me.GroupBox6.Controls.Add(Me.chkFN)
        Me.GroupBox6.Controls.Add(Me.pnlAbOpt)
        Me.GroupBox6.Controls.Add(Me.lblJudg)
        Me.GroupBox6.Controls.Add(Me.cboSpcCd)
        Me.GroupBox6.Controls.Add(Me.Label12)
        Me.GroupBox6.Controls.Add(Me.dtpDateE)
        Me.GroupBox6.Controls.Add(Me.lblTest)
        Me.GroupBox6.Controls.Add(Me.lblWk)
        Me.GroupBox6.Controls.Add(Me.txtSelTest)
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
        Me.GroupBox6.Location = New System.Drawing.Point(218, -3)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox6.Size = New System.Drawing.Size(849, 131)
        Me.GroupBox6.TabIndex = 136
        Me.GroupBox6.TabStop = False
        '
        'cboPart
        '
        Me.cboPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPart.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboPart.Location = New System.Drawing.Point(86, 13)
        Me.cboPart.Name = "cboPart"
        Me.cboPart.Size = New System.Drawing.Size(76, 20)
        Me.cboPart.TabIndex = 202
        '
        'chkSort_pat
        '
        Me.chkSort_pat.Location = New System.Drawing.Point(603, 59)
        Me.chkSort_pat.Name = "chkSort_pat"
        Me.chkSort_pat.Size = New System.Drawing.Size(121, 18)
        Me.chkSort_pat.TabIndex = 201
        Me.chkSort_pat.Text = "환자단위로 정렬"
        '
        'chkFN
        '
        Me.chkFN.Location = New System.Drawing.Point(480, 60)
        Me.chkFN.Name = "chkFN"
        Me.chkFN.Size = New System.Drawing.Size(115, 18)
        Me.chkFN.TabIndex = 200
        Me.chkFN.Text = "최종보고만 조회"
        '
        'pnlAbOpt
        '
        Me.pnlAbOpt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlAbOpt.Controls.Add(Me.rdoAnd)
        Me.pnlAbOpt.Controls.Add(Me.chkC)
        Me.pnlAbOpt.Controls.Add(Me.chkP)
        Me.pnlAbOpt.Controls.Add(Me.rdoOr)
        Me.pnlAbOpt.Controls.Add(Me.chkD)
        Me.pnlAbOpt.Controls.Add(Me.chkA)
        Me.pnlAbOpt.ForeColor = System.Drawing.Color.DarkBlue
        Me.pnlAbOpt.Location = New System.Drawing.Point(86, 57)
        Me.pnlAbOpt.Name = "pnlAbOpt"
        Me.pnlAbOpt.Size = New System.Drawing.Size(388, 21)
        Me.pnlAbOpt.TabIndex = 199
        '
        'rdoAnd
        '
        Me.rdoAnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoAnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAnd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoAnd.ForeColor = System.Drawing.Color.Black
        Me.rdoAnd.Location = New System.Drawing.Point(335, 2)
        Me.rdoAnd.Name = "rdoAnd"
        Me.rdoAnd.Size = New System.Drawing.Size(44, 17)
        Me.rdoAnd.TabIndex = 11
        Me.rdoAnd.Text = "And"
        Me.rdoAnd.UseVisualStyleBackColor = False
        '
        'chkC
        '
        Me.chkC.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkC.Checked = True
        Me.chkC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkC.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkC.ForeColor = System.Drawing.Color.Black
        Me.chkC.Location = New System.Drawing.Point(131, 2)
        Me.chkC.Name = "chkC"
        Me.chkC.Size = New System.Drawing.Size(77, 18)
        Me.chkC.TabIndex = 8
        Me.chkC.Text = "Critical"
        Me.chkC.UseVisualStyleBackColor = False
        '
        'chkP
        '
        Me.chkP.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkP.Checked = True
        Me.chkP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkP.ForeColor = System.Drawing.Color.Black
        Me.chkP.Location = New System.Drawing.Point(5, 2)
        Me.chkP.Name = "chkP"
        Me.chkP.Size = New System.Drawing.Size(58, 18)
        Me.chkP.TabIndex = 6
        Me.chkP.Text = "Panic"
        Me.chkP.UseVisualStyleBackColor = False
        '
        'rdoOr
        '
        Me.rdoOr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoOr.Checked = True
        Me.rdoOr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOr.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoOr.ForeColor = System.Drawing.Color.Black
        Me.rdoOr.Location = New System.Drawing.Point(295, 2)
        Me.rdoOr.Name = "rdoOr"
        Me.rdoOr.Size = New System.Drawing.Size(36, 17)
        Me.rdoOr.TabIndex = 10
        Me.rdoOr.TabStop = True
        Me.rdoOr.Text = "Or"
        Me.rdoOr.UseVisualStyleBackColor = False
        '
        'chkD
        '
        Me.chkD.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkD.Checked = True
        Me.chkD.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkD.ForeColor = System.Drawing.Color.Black
        Me.chkD.Location = New System.Drawing.Point(69, 2)
        Me.chkD.Name = "chkD"
        Me.chkD.Size = New System.Drawing.Size(58, 18)
        Me.chkD.TabIndex = 7
        Me.chkD.Text = "Delta"
        Me.chkD.UseVisualStyleBackColor = False
        '
        'chkA
        '
        Me.chkA.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkA.Checked = True
        Me.chkA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkA.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkA.ForeColor = System.Drawing.Color.Black
        Me.chkA.Location = New System.Drawing.Point(215, 2)
        Me.chkA.Name = "chkA"
        Me.chkA.Size = New System.Drawing.Size(62, 18)
        Me.chkA.TabIndex = 9
        Me.chkA.Text = "Alert"
        Me.chkA.UseVisualStyleBackColor = False
        '
        'lblJudg
        '
        Me.lblJudg.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblJudg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJudg.ForeColor = System.Drawing.Color.White
        Me.lblJudg.Location = New System.Drawing.Point(5, 57)
        Me.lblJudg.Name = "lblJudg"
        Me.lblJudg.Size = New System.Drawing.Size(80, 21)
        Me.lblJudg.TabIndex = 198
        Me.lblJudg.Text = "이상자구분"
        Me.lblJudg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboSpcCd
        '
        Me.cboSpcCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpcCd.FormattingEnabled = True
        Me.cboSpcCd.Location = New System.Drawing.Point(409, 35)
        Me.cboSpcCd.Name = "cboSpcCd"
        Me.cboSpcCd.Size = New System.Drawing.Size(315, 20)
        Me.cboSpcCd.TabIndex = 197
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(328, 35)
        Me.Label12.Margin = New System.Windows.Forms.Padding(1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 21)
        Me.Label12.TabIndex = 196
        Me.Label12.Text = "검체코드"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'lblTest
        '
        Me.lblTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest.ForeColor = System.Drawing.Color.Black
        Me.lblTest.Location = New System.Drawing.Point(5, 80)
        Me.lblTest.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(80, 21)
        Me.lblTest.TabIndex = 195
        Me.lblTest.Text = "검사항목"
        Me.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'txtSelTest
        '
        Me.txtSelTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelTest.BackColor = System.Drawing.Color.Thistle
        Me.txtSelTest.ForeColor = System.Drawing.Color.Brown
        Me.txtSelTest.Location = New System.Drawing.Point(86, 80)
        Me.txtSelTest.Multiline = True
        Me.txtSelTest.Name = "txtSelTest"
        Me.txtSelTest.ReadOnly = True
        Me.txtSelTest.Size = New System.Drawing.Size(759, 43)
        Me.txtSelTest.TabIndex = 194
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
        'cboQrygbn
        '
        Me.cboQrygbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQrygbn.Items.AddRange(New Object() {"검사그룹", "작업그룹"})
        Me.cboQrygbn.Location = New System.Drawing.Point(329, 13)
        Me.cboQrygbn.Margin = New System.Windows.Forms.Padding(0)
        Me.cboQrygbn.Name = "cboQrygbn"
        Me.cboQrygbn.Size = New System.Drawing.Size(80, 20)
        Me.cboQrygbn.TabIndex = 193
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
        'btnClear_test
        '
        Me.btnClear_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_test.Location = New System.Drawing.Point(32, 102)
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
        Me.btnCdHelp_test.Location = New System.Drawing.Point(5, 102)
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
        Me.cboSlip.Location = New System.Drawing.Point(163, 13)
        Me.cboSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(145, 20)
        Me.cboSlip.TabIndex = 90
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
        Me.Label4.Text = "부서/분야"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.lblTitleDt.Text = "결과일자"
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
        Me.cboWkGrp.Location = New System.Drawing.Point(410, 13)
        Me.cboWkGrp.Margin = New System.Windows.Forms.Padding(1)
        Me.cboWkGrp.Name = "cboWkGrp"
        Me.cboWkGrp.Size = New System.Drawing.Size(178, 20)
        Me.cboWkGrp.TabIndex = 88
        '
        'cboTGrp
        '
        Me.cboTGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTGrp.Location = New System.Drawing.Point(409, 13)
        Me.cboTGrp.Margin = New System.Windows.Forms.Padding(1)
        Me.cboTGrp.Name = "cboTGrp"
        Me.cboTGrp.Size = New System.Drawing.Size(179, 20)
        Me.cboTGrp.TabIndex = 157
        '
        'axItemSave
        '
        Me.axItemSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axItemSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axItemSave.Location = New System.Drawing.Point(6, 6)
        Me.axItemSave.Margin = New System.Windows.Forms.Padding(1)
        Me.axItemSave.Name = "axItemSave"
        Me.axItemSave.Size = New System.Drawing.Size(212, 125)
        Me.axItemSave.TabIndex = 135
        '
        'chkcvr
        '
        Me.chkcvr.Location = New System.Drawing.Point(731, 58)
        Me.chkcvr.Name = "chkcvr"
        Me.chkcvr.Size = New System.Drawing.Size(109, 18)
        Me.chkcvr.TabIndex = 203
        Me.chkcvr.Text = "CVR등록만 조회"
        '
        'FGS01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1071, 629)
        Me.Controls.Add(Me.axItemSave)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS01"
        Me.Text = "이상자 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.pnlAbOpt.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FGS01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        sbDisp_Init()

        Me.chkSort_pat.Checked = CType(IIf(PRG_CONST.S01_CHECKED = "", False, True), Boolean)

        Me.axItemSave.FORMID = Me.Name
        Me.axItemSave.USRID = USER_INFO.USRID
        Me.axItemSave.ITEMGBN = ""
        Me.axItemSave.SPCGBN = "NONE"
        Me.axItemSave.MicroBioYn = mbMicroBioYn
        Me.axItemSave.AllPartYn = False
        Me.axItemSave.sbDisplay_ItemList()

        '<<< 20170124 기본로드시에는 분야로 선택하도록 
        Me.cboPart.SelectedIndex = 1
        sbDisplay_slip()
        '>>>

    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        Try
            With spdList
                .ReDraw = False

                .MaxRows = .MaxRows + 1
                .InsertRows(1, 1)

                For i As Integer = 1 To .MaxCols
                    .Col = i : .Row = 0 : sBuf = .Text
                    .Col = i : .Row = 1 : .Text = sBuf
                Next

                If .ExportToExcel("이상자리스트.xls", "이상자리스트", "") Then
                    Process.Start("이상자리스트.xls")
                End If

                .DeleteRows(1, 1)
                .MaxRows -= 1

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Clear()
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Try
            If (Me.chkA.Checked Or Me.chkC.Checked Or Me.chkD.Checked Or Me.chkP.Checked) = False Then
                MsgBox("이상자구분을 선택하여 주십시요!!", MsgBoxStyle.Information, Me.Text)
                Me.chkP.Focus()
                Return
            End If

            If Me.cboQrygbn.Text = "작업그룹" Then
                sbDisplay_List_wkno()
            Else
                sbDisplay_List_tgrp()
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub axItemSave_ListDblClick(ByVal rsItemCds As String, ByVal rsItemNms As String) Handles axItemSave.ListDblClick
        If rsItemCds <> "" Then
            Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
            Me.txtSelTest.Text = rsItemNms.Replace("|", ",")
        End If
    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
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

            COMMON.CommXML.setOneElementXML(msXmlDir, msTestFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnClear_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_test.Click
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""
    End Sub

    Private Sub cboQrygbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQrygbn.SelectedIndexChanged
        sbDisplay_Date_Setting()
        COMMON.CommXML.setOneElementXML(msXmlDir, msQryFile, "JOB", cboQrygbn.SelectedIndex.ToString)
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        Me.spdList.MaxRows = 0

        sbDisplay_wkgrp()
        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        COMMON.CommXML.setOneElementXML(msXmlDir, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub cboSpcCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSpcCd.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXmlDir, msSpcFile, "SPC", cboSpcCd.SelectedIndex.ToString)
    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        sbDisplay_Spc()
        COMMON.CommXML.setOneElementXML(msXmlDir, msTgrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

        Me.spdList.MaxRows = 0

        sbDisplay_Date_Setting()
        sbDisplay_Spc()

        If cboWkGrp.SelectedIndex >= 0 Then
            COMMON.CommXML.setOneElementXML(msXmlDir, msWkGrpFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)
        End If
    End Sub


    Private Sub cboPart_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPart.SelectedIndexChanged

        If Me.cboPart.SelectedIndex = 0 Then
            sbDisplay_part()
        Else
            sbDisplay_slip()
        End If

        'COMMON.CommXML.setOneElementXML(msXML, msPartFile, "PART", cboPart.SelectedIndex.ToString)

        If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0
    End Sub

    Private Sub sbDisplay_part()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List()

            Me.cboSlip.Items.Clear()
            Me.cboSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

            'Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msPartFile, "PART")

            'If sTmp = "" Or Val(sTmp) > cboSlip.Items.Count Then
            Me.cboSlip.SelectedIndex = 0
            'Else
            '    Me.cboSlip.SelectedIndex = Convert.ToInt16(sTmp)
            'End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

End Class

