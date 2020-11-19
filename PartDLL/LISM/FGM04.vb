'>>> No growth 결과저장 및 보고

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports common.commlogin.login
Imports COMMON.CommConst
Imports COMMON.SVar

Public Class FGM04
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGM04.vb, Class : FGM04" & vbTab
    Private Const msXMLDir As String = "\XML"
    Private msTestFile As String = Application.StartupPath & msXMLDir & "\FGM04_TEST.XML"
    Private msWkGrpFile As String = Application.StartupPath & msXMLDir & "\FGM04_WKGRP.XML"
    Private msTgrpFile As String = Application.StartupPath & msXMLDir & "\FGM04_TGRP.XML"
    Private msQryFile As String = Application.StartupPath & msXMLDir & "\FGM04_Qry.XML"

    Private Const mc_iSklCd_ChgRst As Integer = 1        '결과 수정기능
    Private Const mc_iSklCd_RptA As Integer = 2          'Alert 보고기능
    Private Const mc_iSklCd_RptP As Integer = 3          'Panic 보고기능
    Private Const mc_iSklCd_RptD As Integer = 4          'Delta 보고기능
    Private Const mc_iSklCd_RptC As Integer = 5          'Critical 보고기능
    Private Const mc_iSklCd_ChgFn As Integer = 6         '최종보고 수정기능

    Private Const mc_iRptCd_ReqSub As Integer = 10       '결과입력 필수 Child Of Sub. 미입력
    Private Const mc_iRptCd_Parent As Integer = 11       'Parent Of Sub. 미발견
    Private Const mc_iRptCd_Mw As Integer = 20           '이미 중간보고
    Private Const mc_iRptCd_Fn As Integer = 30           '이미 최종보고

    Private msFLD As String = Convert.ToChar(124)
    Private msSEP As String = "，"

    Private msBcNo_Err_Ng As String = ""
    Private msBcNo_Err_Wk As String = ""

    Private miProcessing As Integer = 0
    Friend WithEvents lblTnmd As System.Windows.Forms.Label
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTItem As System.Windows.Forms.Label
    Friend WithEvents cmuDelete As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDelete As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkSel As System.Windows.Forms.CheckBox
    Friend WithEvents cboQryGbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboTGrp As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnFN As CButtonLib.CButton
    Friend WithEvents btnMW As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnHelp_Test As System.Windows.Forms.Button
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblWk As System.Windows.Forms.Label
    Friend WithEvents txtWkNoE As System.Windows.Forms.TextBox
    Friend WithEvents txtWkNoS As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker

    Private Sub sbDisplay_PatInfo(ByVal r_dt As DataTable, Optional ByVal rbAddMode As Boolean = False)
        With spdList

            If rbAddMode = False Then .MaxRows = 0

            If r_dt.Rows.Count < 1 Then
                MsgBox("조회 조건에 해당 내역이 없습니다!!")
                Return
            End If

            .ReDraw = False

            For ix As Integer = 0 To r_dt.Rows.Count - 1
                'If fnFind_Enable_NgCd(r_dt.Rows(ix).Item("bcno").ToString().Trim, r_dt.Rows(ix).Item("tkdt").ToString().Trim) = False Then
                '    GoTo [continue]
                'End If

                Dim bFind As Boolean = False

                For ix2 As Integer = 0 To .MaxRows
                    .Row = ix2
                    .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

                    If r_dt.Rows(ix).Item("bcno").ToString.Trim = sBcNo And r_dt.Rows(ix).Item("testcd").ToString.Trim = sTestCd Then
                        bFind = True
                        Exit For
                    End If
                Next

                If bFind Then GoTo [continue]

                .MaxRows += 1

                Dim iRow As Integer = .MaxRows

                For ix2 As Integer = 0 To r_dt.Rows(ix).Table.Columns.Count - 1
                    Dim iCol As Integer = .GetColFromID(r_dt.Rows(ix).Table.Columns(ix2).ColumnName.ToLower)

                    If iCol > 0 Then
                        If r_dt.Rows(ix).Table.Columns(ix2).ColumnName.ToLower = "rstflg" Then
                            .Row = iRow
                            .Col = iCol
                            .CellTag = r_dt.Rows(ix).Item("rstflg").ToString()

                            Select Case r_dt.Rows(ix).Item("rstflg").ToString()
                                Case "3"
                                    .Text = FixedVariable.gsRstFlagF
                                    .ForeColor = FixedVariable.g_color_FN
                                Case "2"
                                    .Text = FixedVariable.gsRstFlagM
                                Case "1"
                                    .Text = FixedVariable.gsRstFlagR
                            End Select

                        Else
                            .Row = iRow
                            .Col = iCol : .Text = r_dt.Rows(ix).Item(ix2).ToString().Trim
                        End If
                    End If
                Next

[continue]:
            Next
        End With

    End Sub
    Private Sub sbDisplay_Date_Setting()

        If cboQryGbn.Text = "검사그룹" Then
            Me.lblTitleDt.Text = "접수일자"

            Me.dtpDateE.Visible = True

            Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"

            Me.txtWkNoS.Visible = False : txtWkNoE.Visible = False
            Me.cboTGrp.Visible = True : Me.cboWkGrp.Visible = False

        Else
            Me.lblTitleDt.Text = "작업일자"

            Me.dtpDateE.Visible = False

            Me.txtWkNoS.Visible = True : Me.txtWkNoE.Visible = True
            Me.lblWk.Visible = True

            Me.cboTGrp.Visible = False : Me.cboWkGrp.Visible = True

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
        End If
    End Sub

    Private Sub sbDisplay_WkGrp()

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_WKGrp_List(PRG_CONST.PART_MicroBio)

        cboWkGrp.Items.Clear()

        For ix As Integer = 0 To dt.Rows.Count - 1
            cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString)
        Next

        cboWkGrp.SelectedIndex = 0
    End Sub


    Private Sub sbDisplay_TGrp() ''' 검사그룹조회 

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TGrp_List(, True)

        Me.cboTGrp.Items.Clear()
        Me.cboTGrp.Items.Add("[  ] 전체")

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboTGrp.Items.Add("[" + dt.Rows(ix).Item("tgrpcd").ToString + "] " + dt.Rows(ix).Item("tgrpnmd").ToString)
        Next
        Me.cboTGrp.SelectedIndex = 0

    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

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
    Friend WithEvents GroupBox22 As System.Windows.Forms.GroupBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents cboNgCd As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents lblTitleDt As System.Windows.Forms.Label
    Friend WithEvents cboWkGrp As System.Windows.Forms.ComboBox
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkRstReg As System.Windows.Forms.CheckBox
    Friend WithEvents chkRstNull As System.Windows.Forms.CheckBox
    Friend WithEvents lblNgCd As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGM04))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.cmuDelete = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDelete = New System.Windows.Forms.ToolStripMenuItem
        Me.chkSel = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkRstReg = New System.Windows.Forms.CheckBox
        Me.chkRstNull = New System.Windows.Forms.CheckBox
        Me.GroupBox22 = New System.Windows.Forms.GroupBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.lblWk = New System.Windows.Forms.Label
        Me.txtWkNoE = New System.Windows.Forms.TextBox
        Me.txtWkNoS = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.btnHelp_Test = New System.Windows.Forms.Button
        Me.cboTGrp = New System.Windows.Forms.ComboBox
        Me.cboQryGbn = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblTnmd = New System.Windows.Forms.Label
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.lblTItem = New System.Windows.Forms.Label
        Me.cboWkGrp = New System.Windows.Forms.ComboBox
        Me.lblTitleDt = New System.Windows.Forms.Label
        Me.lblNgCd = New System.Windows.Forms.Label
        Me.cboNgCd = New System.Windows.Forms.ComboBox
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnQuery = New CButtonLib.CButton
        Me.btnFN = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.btnMW = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.pnlTop.SuspendLayout()
        Me.cmuDelete.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox22.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTop.ContextMenuStrip = Me.cmuDelete
        Me.pnlTop.Controls.Add(Me.chkSel)
        Me.pnlTop.Controls.Add(Me.GroupBox2)
        Me.pnlTop.Controls.Add(Me.GroupBox22)
        Me.pnlTop.Controls.Add(Me.spdList)
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(998, 595)
        Me.pnlTop.TabIndex = 0
        '
        'cmuDelete
        '
        Me.cmuDelete.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDelete})
        Me.cmuDelete.Name = "cmuRstList"
        Me.cmuDelete.Size = New System.Drawing.Size(169, 26)
        Me.cmuDelete.Text = "상황에 맞는 메뉴"
        '
        'mnuDelete
        '
        Me.mnuDelete.CheckOnClick = True
        Me.mnuDelete.Name = "mnuDelete"
        Me.mnuDelete.Size = New System.Drawing.Size(168, 22)
        Me.mnuDelete.Text = "선택 리스트 삭제"
        '
        'chkSel
        '
        Me.chkSel.AutoSize = True
        Me.chkSel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkSel.Location = New System.Drawing.Point(42, 91)
        Me.chkSel.Name = "chkSel"
        Me.chkSel.Size = New System.Drawing.Size(15, 14)
        Me.chkSel.TabIndex = 154
        Me.chkSel.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Panel3)
        Me.GroupBox2.Location = New System.Drawing.Point(873, -5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(119, 81)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.Controls.Add(Me.chkRstReg)
        Me.Panel3.Controls.Add(Me.chkRstNull)
        Me.Panel3.ForeColor = System.Drawing.Color.Lavender
        Me.Panel3.Location = New System.Drawing.Point(8, 12)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(104, 63)
        Me.Panel3.TabIndex = 0
        '
        'chkRstReg
        '
        Me.chkRstReg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkRstReg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkRstReg.ForeColor = System.Drawing.Color.Black
        Me.chkRstReg.Location = New System.Drawing.Point(7, 35)
        Me.chkRstReg.Name = "chkRstReg"
        Me.chkRstReg.Size = New System.Drawing.Size(95, 18)
        Me.chkRstReg.TabIndex = 1
        Me.chkRstReg.Text = "입력(예비)"
        Me.chkRstReg.UseVisualStyleBackColor = False
        '
        'chkRstNull
        '
        Me.chkRstNull.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkRstNull.ForeColor = System.Drawing.Color.Black
        Me.chkRstNull.Location = New System.Drawing.Point(7, 11)
        Me.chkRstNull.Name = "chkRstNull"
        Me.chkRstNull.Size = New System.Drawing.Size(95, 18)
        Me.chkRstNull.TabIndex = 0
        Me.chkRstNull.Text = "미입력(접수)"
        '
        'GroupBox22
        '
        Me.GroupBox22.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox22.Controls.Add(Me.lblSearch)
        Me.GroupBox22.Controls.Add(Me.txtBcNo)
        Me.GroupBox22.Controls.Add(Me.dtpDateE)
        Me.GroupBox22.Controls.Add(Me.lblWk)
        Me.GroupBox22.Controls.Add(Me.txtWkNoE)
        Me.GroupBox22.Controls.Add(Me.txtWkNoS)
        Me.GroupBox22.Controls.Add(Me.Label1)
        Me.GroupBox22.Controls.Add(Me.dtpDateS)
        Me.GroupBox22.Controls.Add(Me.btnHelp_Test)
        Me.GroupBox22.Controls.Add(Me.cboTGrp)
        Me.GroupBox22.Controls.Add(Me.cboQryGbn)
        Me.GroupBox22.Controls.Add(Me.Label7)
        Me.GroupBox22.Controls.Add(Me.lblTnmd)
        Me.GroupBox22.Controls.Add(Me.txtTestCd)
        Me.GroupBox22.Controls.Add(Me.lblTItem)
        Me.GroupBox22.Controls.Add(Me.cboWkGrp)
        Me.GroupBox22.Controls.Add(Me.lblTitleDt)
        Me.GroupBox22.Controls.Add(Me.lblNgCd)
        Me.GroupBox22.Controls.Add(Me.cboNgCd)
        Me.GroupBox22.Location = New System.Drawing.Point(4, -5)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Size = New System.Drawing.Size(864, 82)
        Me.GroupBox22.TabIndex = 1
        Me.GroupBox22.TabStop = False
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(364, 34)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(72, 21)
        Me.lblSearch.TabIndex = 17
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBcNo
        '
        Me.txtBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcNo.Location = New System.Drawing.Point(437, 34)
        Me.txtBcNo.MaxLength = 18
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(124, 21)
        Me.txtBcNo.TabIndex = 0
        Me.txtBcNo.Text = "20050705-M0-1234-0"
        '
        'dtpDateE
        '
        Me.dtpDateE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateE.Location = New System.Drawing.Point(199, 34)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(102, 21)
        Me.dtpDateE.TabIndex = 184
        Me.dtpDateE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'lblWk
        '
        Me.lblWk.AutoSize = True
        Me.lblWk.Location = New System.Drawing.Point(219, 38)
        Me.lblWk.Name = "lblWk"
        Me.lblWk.Size = New System.Drawing.Size(11, 12)
        Me.lblWk.TabIndex = 188
        Me.lblWk.Text = "~"
        Me.lblWk.Visible = False
        '
        'txtWkNoE
        '
        Me.txtWkNoE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkNoE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkNoE.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoE.Location = New System.Drawing.Point(233, 34)
        Me.txtWkNoE.MaxLength = 4
        Me.txtWkNoE.Name = "txtWkNoE"
        Me.txtWkNoE.Size = New System.Drawing.Size(33, 21)
        Me.txtWkNoE.TabIndex = 187
        Me.txtWkNoE.Text = "9999"
        '
        'txtWkNoS
        '
        Me.txtWkNoS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkNoS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkNoS.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoS.Location = New System.Drawing.Point(183, 34)
        Me.txtWkNoS.MaxLength = 4
        Me.txtWkNoS.Name = "txtWkNoS"
        Me.txtWkNoS.Size = New System.Drawing.Size(33, 21)
        Me.txtWkNoS.TabIndex = 186
        Me.txtWkNoS.Text = "0000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(186, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 185
        Me.Label1.Text = "~"
        '
        'dtpDateS
        '
        Me.dtpDateS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateS.Location = New System.Drawing.Point(79, 34)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(102, 21)
        Me.dtpDateS.TabIndex = 183
        Me.dtpDateS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'btnHelp_Test
        '
        Me.btnHelp_Test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_Test.Image = CType(resources.GetObject("btnHelp_Test.Image"), System.Drawing.Image)
        Me.btnHelp_Test.Location = New System.Drawing.Point(491, 12)
        Me.btnHelp_Test.Name = "btnHelp_Test"
        Me.btnHelp_Test.Size = New System.Drawing.Size(21, 21)
        Me.btnHelp_Test.TabIndex = 182
        Me.btnHelp_Test.UseVisualStyleBackColor = True
        '
        'cboTGrp
        '
        Me.cboTGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTGrp.Location = New System.Drawing.Point(163, 12)
        Me.cboTGrp.Margin = New System.Windows.Forms.Padding(1)
        Me.cboTGrp.Name = "cboTGrp"
        Me.cboTGrp.Size = New System.Drawing.Size(187, 20)
        Me.cboTGrp.TabIndex = 134
        '
        'cboQryGbn
        '
        Me.cboQryGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQryGbn.FormattingEnabled = True
        Me.cboQryGbn.Items.AddRange(New Object() {"검사그룹", "작업그룹"})
        Me.cboQryGbn.Location = New System.Drawing.Point(79, 12)
        Me.cboQryGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.cboQryGbn.Name = "cboQryGbn"
        Me.cboQryGbn.Size = New System.Drawing.Size(82, 20)
        Me.cboQryGbn.TabIndex = 133
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(6, 12)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 20)
        Me.Label7.TabIndex = 132
        Me.Label7.Text = "구    분"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTnmd
        '
        Me.lblTnmd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTnmd.BackColor = System.Drawing.Color.White
        Me.lblTnmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTnmd.ForeColor = System.Drawing.Color.Black
        Me.lblTnmd.Location = New System.Drawing.Point(513, 12)
        Me.lblTnmd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTnmd.Name = "lblTnmd"
        Me.lblTnmd.Size = New System.Drawing.Size(345, 21)
        Me.lblTnmd.TabIndex = 128
        Me.lblTnmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.Location = New System.Drawing.Point(437, 12)
        Me.txtTestCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTestCd.MaxLength = 7
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(54, 21)
        Me.txtTestCd.TabIndex = 127
        '
        'lblTItem
        '
        Me.lblTItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTItem.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTItem.ForeColor = System.Drawing.Color.White
        Me.lblTItem.Location = New System.Drawing.Point(364, 12)
        Me.lblTItem.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTItem.Name = "lblTItem"
        Me.lblTItem.Size = New System.Drawing.Size(72, 21)
        Me.lblTItem.TabIndex = 126
        Me.lblTItem.Text = "검사코드"
        Me.lblTItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboWkGrp
        '
        Me.cboWkGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWkGrp.Location = New System.Drawing.Point(163, 12)
        Me.cboWkGrp.Margin = New System.Windows.Forms.Padding(1)
        Me.cboWkGrp.Name = "cboWkGrp"
        Me.cboWkGrp.Size = New System.Drawing.Size(187, 20)
        Me.cboWkGrp.TabIndex = 2
        Me.cboWkGrp.Visible = False
        '
        'lblTitleDt
        '
        Me.lblTitleDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTitleDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTitleDt.ForeColor = System.Drawing.Color.White
        Me.lblTitleDt.Location = New System.Drawing.Point(6, 34)
        Me.lblTitleDt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTitleDt.Name = "lblTitleDt"
        Me.lblTitleDt.Size = New System.Drawing.Size(72, 21)
        Me.lblTitleDt.TabIndex = 14
        Me.lblTitleDt.Text = "접수일자"
        Me.lblTitleDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNgCd
        '
        Me.lblNgCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblNgCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblNgCd.ForeColor = System.Drawing.Color.White
        Me.lblNgCd.Location = New System.Drawing.Point(6, 57)
        Me.lblNgCd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblNgCd.Name = "lblNgCd"
        Me.lblNgCd.Size = New System.Drawing.Size(115, 21)
        Me.lblNgCd.TabIndex = 125
        Me.lblNgCd.Text = "No growth 코드"
        Me.lblNgCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboNgCd
        '
        Me.cboNgCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboNgCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboNgCd.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboNgCd.Location = New System.Drawing.Point(123, 57)
        Me.cboNgCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboNgCd.Name = "cboNgCd"
        Me.cboNgCd.Size = New System.Drawing.Size(735, 21)
        Me.cboNgCd.TabIndex = 0
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(5, 81)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(988, 505)
        Me.spdList.TabIndex = 4
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.btnFN)
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Controls.Add(Me.btnMW)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 597)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(998, 32)
        Me.pnlBottom.TabIndex = 1
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
        Me.btnQuery.Location = New System.Drawing.Point(423, 3)
        Me.btnQuery.Margin = New System.Windows.Forms.Padding(0)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(96, 25)
        Me.btnQuery.TabIndex = 199
        Me.btnQuery.Text = "조회(F5)"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnFN
        '
        Me.btnFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnFN.ColorFillBlend = CBlendItems2
        Me.btnFN.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnFN.Corners.All = CType(6, Short)
        Me.btnFN.Corners.LowerLeft = CType(6, Short)
        Me.btnFN.Corners.LowerRight = CType(6, Short)
        Me.btnFN.Corners.UpperLeft = CType(6, Short)
        Me.btnFN.Corners.UpperRight = CType(6, Short)
        Me.btnFN.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnFN.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnFN.FocalPoints.CenterPtX = 0.4516129!
        Me.btnFN.FocalPoints.CenterPtY = 0.04!
        Me.btnFN.FocalPoints.FocusPtX = 0.0!
        Me.btnFN.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.FocusPtTracker = DesignerRectTracker4
        Me.btnFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnFN.ForeColor = System.Drawing.Color.White
        Me.btnFN.Image = Nothing
        Me.btnFN.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFN.ImageIndex = 0
        Me.btnFN.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnFN.Location = New System.Drawing.Point(520, 3)
        Me.btnFN.Name = "btnFN"
        Me.btnFN.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnFN.SideImage = Nothing
        Me.btnFN.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFN.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnFN.Size = New System.Drawing.Size(93, 25)
        Me.btnFN.TabIndex = 194
        Me.btnFN.Text = "결과검증(F12)"
        Me.btnFN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFN.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnFN.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnReg.Location = New System.Drawing.Point(708, 3)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(93, 25)
        Me.btnReg.TabIndex = 192
        Me.btnReg.Text = "결과저장(F9)"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnMW
        '
        Me.btnMW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnMW.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnMW.ColorFillBlend = CBlendItems4
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
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnMW.FocusPtTracker = DesignerRectTracker8
        Me.btnMW.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnMW.ForeColor = System.Drawing.Color.White
        Me.btnMW.Image = Nothing
        Me.btnMW.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnMW.ImageIndex = 0
        Me.btnMW.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnMW.Location = New System.Drawing.Point(614, 3)
        Me.btnMW.Name = "btnMW"
        Me.btnMW.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnMW.SideImage = Nothing
        Me.btnMW.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMW.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnMW.Size = New System.Drawing.Size(93, 25)
        Me.btnMW.TabIndex = 193
        Me.btnMW.Text = "중간보고(F11)"
        Me.btnMW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnMW.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnMW.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems5
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker10
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(802, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(93, 25)
        Me.btnClear.TabIndex = 191
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems6
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
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker12
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(896, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(93, 25)
        Me.btnExit.TabIndex = 190
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGM04
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(998, 629)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FGM04"
        Me.Text = "No growth 결과저장 및 보고"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.cmuDelete.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox22.ResumeLayout(False)
        Me.GroupBox22.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbDisplay_BcNo(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_BcNo"

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            Dim sOpt As String = ""
            sOpt += IIf(Me.chkRstNull.Checked, "1", "0").ToString()
            sOpt += IIf(Me.chkRstReg.Checked, "1", "0").ToString()

            If sOpt = "00" Then
                MsgBox(Me.chkRstNull.Text + ", " + Me.chkRstReg.Text + " 중 하나이상을 체크하십시요!!")
                Return
            End If

            Dim sNgCd As String = Ctrl.Get_Code(Me.cboNgCd)

            If sNgCd = "" Then
                MsgBox(Me.lblNgCd.Text + " 을(를) 선택하십시요!!")
                Return
            End If

            Dim dt As DataTable

            dt = LISAPP.APP_M.CommFn.fnGet_NgList_BcNo(rsBcNo, sOpt)

            sbDisplay_PatInfo(dt, True)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Function fnReg_Validation(ByVal riRegStep As Integer) As Boolean
        Dim sFn As String = "fnReg_Validation"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            If spd.MaxRows < 1 Then Return False

            Dim sMsg As String = ""
            sMsg += "선택한 " + Me.lblNgCd.Text + " : " + Me.cboNgCd.SelectedItem.ToString() + " (으)로" + vbCrLf + vbCrLf

            If riRegStep = 1 Then
                sMsg += "일괄적으로 결과저장합니다. 계속하시겠습니까?"
            ElseIf riRegStep = 2 Then
                sMsg += "일괄적으로 중간보고합니다. 계속하시겠습니까?"
            ElseIf riRegStep = 3 Then
                sMsg += "일괄적으로 최종보고합니다. 계속하시겠습니까?"
            End If

            If MsgBox(sMsg, MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Return False
            End If

            Dim sSkill As String = ""
            Dim iExist As Integer = 0

            '이미 결과저장된 리스트 체크
            iExist = spd.SearchCol(spd.GetColFromID("rstflag"), 0, spd.MaxRows, FixedVariable.gsRstFlagR, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

            If iExist > 0 Then
                If MsgBox("이미 결과저장된 검체가 존재합니다. 결과를 변경하시겠습니까?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Return False
                End If

                '결과 수정기능 체크
                If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst, sSkill) = False Then
                    MsgBox(sSkill + "의 권한이 없습니다. 확인하여 주십시요!!")
                    Return False
                End If
            End If

            '이미 중간보고된 리스트 체크
            iExist = spd.SearchCol(spd.GetColFromID("rstflag"), 0, spd.MaxRows, FixedVariable.gsRstFlagM, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

            If iExist > 0 Then
                If riRegStep = 1 Then
                    MsgBox("이미 중간보고된 검체가 존재하므로 결과저장할 수 없습니다. 확인하여 주십시요!!")
                    Return False
                Else
                    If MsgBox("이미 중간보고된 검체가 존재합니다. 결과를 변경하시겠습니까?", MsgBoxStyle.Information Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        Return False
                    End If

                    '결과 수정기능 체크
                    If USER_SKILL.Authority("R01", mc_iSklCd_ChgRst, sSkill) = False Then
                        MsgBox(sSkill + "의 권한이 없습니다. 확인하여 주십시요!!")
                        Return False
                    End If
                End If
            End If

            Return True

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return False

        End Try
    End Function

    Private Sub sbDisplay_Clear()
        Dim sFn As String = "sbDisplay_Clear"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            'sbDisplay_NogrowthCd()
            Me.spdList.MaxRows = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            miProcessing = 1

            '미입력(접수)
            Me.chkRstNull.Checked = True

            '작업번호별
            Dim sDate As String = Format(New LISAPP.APP_DB.ServerDateTime().GetDateTime, "yyyy-MM-dd").ToString

            Me.dtpDateS.Value = CDate(sDate.Substring(0, 8) + "01 00:00:00")
            Me.dtpDateE.Value = CDate(sDate + " 23:59:59")
            Me.txtWkNoE.Text = ""
            Me.txtWkNoS.Text = ""

            '검체번호별
            Me.txtBcNo.Text = ""
            Me.cboWkGrp.Items.Clear()
            Me.cboTGrp.Items.Clear()
            Me.spdList.MaxRows = 0

            sbDisplay_TGrp()        '-- 검사그룹
            sbDisplay_WkGrp()     '-- 작업그룹

            Dim sTgrp As String = "", sWkGrp As String = "", sJob As String = "", sTestCds As String = ""

            sTgrp = COMMON.CommXML.getOneElementXML(msXMLDir, msTgrpFile, "TGRP")
            sWkGrp = COMMON.CommXML.getOneElementXML(msXMLDir, msWkGrpFile, "WKGRP")
            sJob = COMMON.CommXML.getOneElementXML(msXMLDir, msWkGrpFile, "JOB")
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

            If sJob = "" Or Val(sJob) > cboQryGbn.Items.Count Then
                cboQryGbn.SelectedIndex = 0
            Else
                cboQryGbn.SelectedIndex = Convert.ToInt16(sJob)
            End If

            If sTestCds <> "" Then
                Me.lblTnmd.Text = sTestCds.Split("^"c)(1).Replace("|", ",")
                Me.txtTestCd.Text = sTestCds
            End If


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_NogrowthCd()
        Dim sFn As String = "sbDisplay_NogrowthCd"

        Try
            Dim sNgCd_Pre As String = Ctrl.Get_Item(Me.cboNgCd)

            Me.cboNgCd.Items.Clear()

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TestRst_list(Me.txtTestCd.Text)
            Dim dr As DataRow() = dt.Select("NVL(rstlvl, 'N') = 'N'", "")

            If dr.Length < 1 Then Return

            For ix As Integer = 0 To dr.Length - 1
                Me.cboNgCd.Items.Add(dr(ix).Item("rstcont").ToString())
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbReg_Rst_NgCd(ByVal riRegStep As Integer)
        Dim sFn As String = "sbReg_Rst_NgCd"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            If fnReg_Validation(riRegStep) = False Then Return

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim ri As STU_RstInfo

            With spd
                Dim al_succ As New ArrayList

                For i As Integer = 1 To .MaxRows

                    Dim al_ri As New ArrayList

                    Dim sChk As String = Ctrl.Get_Code(spd, "chk", i)
                    Dim sBcNo As String = Ctrl.Get_Code(spd, "bcno", i).Replace("-", "")
                    Dim sTestCD As String = Ctrl.Get_Code(spd, "testcd", i)
                    Dim sSpcCd As String = Ctrl.Get_Code(spd, "spccd", i)
                    Dim sBacCd As String = Ctrl.Get_Code(Me.cboNgCd)

                    If sChk = "1" Then
                        '결과정보 구성
                        ri = New STU_RstInfo
                        ri.TestCd = sTestCD.Substring(0, 5) + "03"
                        ri.OrgRst = Me.cboNgCd.Text
                        ri.RstCmt = ""
                        ri.EqFlag = ""

                        al_ri.Add(ri)
                        ri = Nothing

                        '샘플정보 구성
                        Dim si As New STU_SampleInfo
                        si.RegStep = riRegStep.ToString()
                        si.BCNo = sBcNo
                        si.EqCd = ""
                        si.UsrID = USER_INFO.USRID
                        si.UsrIP = USER_INFO.LOCALIP
                        si.IntSeqNo = ""
                        si.Rack = ""
                        si.Pos = ""
                        si.EqBCNo = ""
                        si.SenderID = Me.Name

                        Dim regrst As New LISAPP.APP_M.RegFn

                        regrst.al_Bac = Nothing
                        regrst.al_Anti = Nothing
                        regrst.al_Cmt = Nothing

                        Dim iReturn As Integer = regrst.RegServer(al_ri, si, Nothing)

                        If iReturn > 0 Then
                            al_succ.Add(sBcNo)
                        End If
                    End If
                Next

                '등록 후 화면 처리
                sbReg_Rst_NgCd_Clear(al_succ)
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbReg_Rst_NgCd_Clear(ByVal r_al As ArrayList)
        Dim sFn As String = "sbReg_Rst_NgCd_Clear"

        Try
            If r_al Is Nothing Then Return
            If r_al.Count < 1 Then Return

            For i As Integer = 1 To r_al.Count
                With Me.spdList
                    Dim iRow As Integer = .SearchCol(.GetColFromID("bcno"), 0, .MaxRows, r_al(i - 1).ToString(), FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    .DeleteRows(iRow, 1)
                    .MaxRows -= 1
                End With
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub FGM04_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_ButtonClick(Nothing, Nothing)
            Case Keys.F5
                btnQuery_Click(Nothing, Nothing)
            Case Keys.F9
                btnRegRstR_ButtonClick(btnReg, Nothing)
            Case Keys.F11
                btnRegRstR_ButtonClick(btnMW, Nothing)
            Case Keys.F12
                btnRegRstF_ButtonClick(btnFN, Nothing)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select
    End Sub

    '<------- Control Event ------->

    Private Sub FGM04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sFn As String = "FGM06_Load"

        Try
            sbDisplayInit()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Clear()
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnRegRstF_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFN.Click
        If STU_AUTHORITY.FNReg <> "1" Then
            MsgBox("결과검증 권한이 없습니다.!!  확인하세요.")
            Return
        End If

        sbReg_Rst_NgCd(3)
    End Sub

    Private Sub btnRegRstM_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMW.Click
        sbReg_Rst_NgCd(2)
    End Sub

    Private Sub btnRegRstR_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        sbReg_Rst_NgCd(1)
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim sFn As String = "Handles btnQuery.Click"

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            Dim sOpt As String = ""
            sOpt += IIf(Me.chkRstNull.Checked, "1", "0").ToString()
            sOpt += IIf(Me.chkRstReg.Checked, "1", "0").ToString()

            If sOpt = "00" Then
                MsgBox(Me.chkRstNull.Text + ", " + Me.chkRstReg.Text + " 중 하나이상을 체크하십시요!!")
                Return
            End If

            Dim sNgCd As String = Ctrl.Get_Code(Me.cboNgCd)

            If sNgCd = "" Then
                MsgBox(Me.lblNgCd.Text + " 을(를) 선택하십시요!!")
                Return
            End If

            Dim sWkGrpCd As String = Ctrl.Get_Code(Me.cboWkGrp)
            Dim sTGrpCd As String = Ctrl.Get_Code(Me.cboTGrp)
            Dim sWkNoS As String = ""
            Dim sWkNoE As String = ""

            If cboQryGbn.Text = "작업그룹" And sWkGrpCd.Length < 2 Then
                MsgBox("작업그룹 코드가 없습니다. 확인하여 주십시요!!")
                Return
            End If

            If Me.txtWkNoS.Text = "" Then
                sWkNoS = "0000"
            Else
                If IsNumeric(Me.txtWkNoS.Text) Then
                    sWkNoS = Me.txtWkNoS.Text.PadLeft(4, "0"c)
                Else
                    MsgBox("작업번호에 숫자를 입력하여 주십시요!!")
                    Return
                End If
            End If

            If Me.txtWkNoE.Text = "" Then
                sWkNoE = "9999"
            Else
                If IsNumeric(Me.txtWkNoE.Text) Then
                    sWkNoE = Me.txtWkNoE.Text.PadLeft(4, "0"c)
                Else
                    MsgBox("작업번호에 숫자를 입력하여 주십시요!!")
                    Return
                End If
            End If

            Dim sWkYmd As String = Me.dtpDateS.Text.Replace("-", "").PadRight(8, "0"c)
            Dim sWkDayS As String = Me.dtpDateS.Text.Replace("-", "")

            Dim dt As DataTable

            If cboQryGbn.Text = "작업그룹" Then
                dt = LISAPP.APP_M.CommFn.fnGet_NgList_WGrp(sWkYmd, sWkGrpCd, sWkNoS, sWkNoE, sOpt, txtTestCd.Text)
            Else
                dt = LISAPP.APP_M.CommFn.fnGet_NgList_TGrp(sTGrpCd, dtpDateS.Text.Replace("-", "").Replace(" ", ""), dtpDateE.Text.Replace("-", "").Replace(" ", ""), Me.txtTestCd.Text, sOpt)
            End If

            sbDisplay_PatInfo(dt)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            msBcNo_Err_Wk = ""

            spd.ReDraw = True
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        With spdList
            If e.col > .GetColFromID("chk") And e.col <= .GetColFromID("rstdt") Then
                .Row = e.row
                .Col = .GetColFromID("chk") : .Text = IIf(.Text = "1", "", "1").ToString
            End If
        End With

    End Sub

    Private Sub spd_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdList.Resize
        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

            With spd
                .ReDraw = False
                .Hide()
                .Show()
                .ReDraw = True
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdList.DblClick
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Dim sBcNo As String = Ctrl.Get_Code(spd, "cbcno", e.row)

        If MsgBox("검체번호 " + sBcNo + "을(를) 삭제하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Information) = MsgBoxResult.No Then Return

        spd.DeleteRows(e.row, 1)
        spd.MaxRows -= 1
    End Sub

    Private Sub txtNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.Click
        Me.txtBcNo.SelectAll()
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged
        If cboWkGrp.SelectedIndex < 0 Then Exit Sub

        Dim strWkNoGbn As String = cboWkGrp.Text.Split("|"c)(1)

        txtTestCd.Text = "" : lblTnmd.Text = ""

        Select Case strWkNoGbn
            Case "2"
                dtpDateS.CustomFormat = "yyyy-MM"
            Case "3"
                dtpDateS.CustomFormat = "yyyy"
            Case Else
                dtpDateS.CustomFormat = "yyyy-MM-dd"
        End Select

    End Sub

    Private Sub btnHelpTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_Test.Click
        Dim sFn As String = "Handles btnCdHelp_test.Click"
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""

            If Me.cboQryGbn.Text = "검사그룹" Then
                sTGrpCd = Ctrl.Get_Code(Me.cboTGrp)
            Else
                sWGrpCd = Ctrl.Get_Code(Me.cboWkGrp)
            End If

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnHelp_Test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list("", sTGrpCd, sWGrpCd, , "")
            Dim sSql As String = "((tcdgbn = 'P' AND titleyn = '1') OR titleyn = '0') AND mbttype = '2'"
            If Me.txtTestCd.Text <> "" Then sSql += " AND (testcd = '" + Me.txtTestCd.Text + "' OR tnmd LIKE '" + Me.txtTestCd.Text + "%')"
            Dim a_dr As DataRow() = dt.Select(sSql, "")

            dt = Fn.ChangeToDataTable(a_dr)
            objHelp.FormText = "검사목록"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmp", "출력명", 0, , , True)
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("titleyn", "titleyn", 0, , , True)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnHelp_Test.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnHelp_Test.Height + 80, dt)

            If alList.Count > 0 Then

                Me.txtTestCd.Text = alList.Item(0).ToString.Split("|"c)(2)
                Me.lblTnmd.Text = alList.Item(0).ToString.Split("|"c)(0)

                sbDisplay_NogrowthCd()
            Else
                Me.txtTestCd.Text = ""
                Me.lblTnmd.Text = ""
                Me.cboNgCd.Items.Clear()
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtSpcCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Me.lblTnmd.Text = ""
        Me.btnHelpTest_Click(Nothing, Nothing)
    End Sub

    Private Sub mnuDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDelete.Click
        With spdList
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1

                    intRow -= 1
                    If intRow < 0 Then Exit For
                End If

            Next
        End With
    End Sub

    Private Sub chkSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSel.Click

        With spdList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : .Text = IIf(chkSel.Checked, "1", "").ToString
            Next
        End With

    End Sub

    Private Sub cboQryGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQryGbn.SelectedIndexChanged

        sbDisplay_Date_Setting()

        COMMON.CommXML.setOneElementXML(msXMLDir, msQryFile, "JOB", cboQryGbn.SelectedIndex.ToString)

    End Sub

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged

        Me.txtTestCd.Text = "" : Me.lblTnmd.Text = ""

    End Sub

    Private Sub FGJ_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown
        If e.KeyCode <> Keys.Enter Or Me.txtBcNo.Text = "" Then Return

        Dim bFind As Boolean = False

        If Len(Me.txtBcNo.Text) = 11 Or Len(Me.txtBcNo.Text) = 12 Then
            Me.txtBcNo.Text = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(Me.txtBcNo.Text)
        End If

        If Me.txtBcNo.Text.Length = 14 Then Me.txtBcNo.Text += "0"

        sbDisplay_BcNo(Me.txtBcNo.Text)
        Me.txtBcNo.Text = ""

    End Sub
End Class