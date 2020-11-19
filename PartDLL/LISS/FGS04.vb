'>>> 환자/검체현황 조회
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login

Imports LISAPP.APP_S
Imports LISAPP.APP_S.PatHisFn
Imports LISAPP.APP_C

Public Class FGS04
    Inherits System.Windows.Forms.Form

    Private Const msXML As String = "\XML"
    'Private msSlipFile As String = Application.StartupPath + msXML + "\FGS04_SLIP.XML"
    'Private msWGrPFIle As String = Application.StartupPath + msXML + "\FGS04_WGRP.XML"
    'Private msTGrpFile As String = Application.StartupPath + msXML + "\FGS04_TGRP.XML"

    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents lblWard As System.Windows.Forms.Label
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblIOGbn As System.Windows.Forms.Label
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDateTitle As System.Windows.Forms.Label
    Friend WithEvents lblDay As System.Windows.Forms.Label
    Friend WithEvents cboTGrp As System.Windows.Forms.ComboBox
    Friend WithEvents lblTGrp As System.Windows.Forms.Label
    Friend WithEvents cboWkGrp As System.Windows.Forms.ComboBox
    Friend WithEvents lblWkGrp As System.Windows.Forms.Label
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblSlip As System.Windows.Forms.Label
    Friend WithEvents cboIOGbn As System.Windows.Forms.ComboBox
    Friend WithEvents cmuList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRstView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColl As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents cboQryGbn As System.Windows.Forms.ComboBox
    Friend WithEvents cboSpcFlg As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As CButtonLib.CButton

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "처방일시" : .WIDTH = "120" : .FIELD = "orddt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "진료과/병동" : .WIDTH = "60" : .FIELD = "deptward"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "의뢰의사" : .WIDTH = "60" : .FIELD = "doctornm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "상태" : .WIDTH = "75" : .FIELD = "statgbn"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검사명" : .WIDTH = "150" : .FIELD = "tnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검체명" : .WIDTH = "80" : .FIELD = "spcnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "검체번호" : .WIDTH = "140" : .FIELD = "bcno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "작업번호" : .WIDTH = "140" : .FIELD = "workno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "채혈일시" : .WIDTH = "120" : .FIELD = "colldt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "접수일시" : .WIDTH = "120" : .FIELD = "tkdt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "보고일시" : .WIDTH = "120" : .FIELD = "rstdt"
        End With
        alItems.Add(stu_item)

        Return alItems

    End Function

    Private Sub sbFormLoadedChk(ByVal r_frm As Windows.Forms.Form, ByVal rsFrmText As String)

        Try

            If Me.IsMdiContainer Then
                r_frm.MdiParent = Me
            Else
                r_frm.MdiParent = Me.MdiParent
            End If

            r_frm.Text = rsFrmText

            r_frm.Activate()
            r_frm.Show()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Slip()


        Try

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

            Me.cboSlip.Items.Clear()
            Me.cboSlip.Items.Add("[  ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0

            'Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msSlipFile, "SLIP")
            'If sTmp = "" Then sTmp = "0"

            'If Me.cboSlip.Items.Count > 0 And Me.cboSlip.Items.Count > CInt(sTmp) Then
            '    Me.cboSlip.SelectedIndex = CInt(IIf(sTmp = "", "0", sTmp))
            'End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_part()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List()

            Me.cboSlip.Items.Clear()
            Me.cboSlip.Items.Add("[  ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub


    Private Sub sbDisplay_WkGrp()

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_WKGrp_List(Ctrl.Get_Code(Me.cboSlip))

            Me.cboWkGrp.Items.Clear()
            Me.cboWkGrp.Items.Add("[  ] 전체")
            For intIdx As Integer = 0 To dt.Rows.Count - 1
                cboWkGrp.Items.Add("[" + dt.Rows(intIdx).Item("wkgrpcd").ToString + "] " + dt.Rows(intIdx).Item("wkgrpnmd").ToString)
            Next

            If Me.cboWkGrp.Items.Count > 0 Then Me.cboWkGrp.SelectedIndex = 0

            'Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msWGrPFIle, "WKGRP")
            'If sTmp = "" Then sTmp = "0"

            'If Me.cboWkGrp.Items.Count > 0 And Me.cboWkGrp.Items.Count > CInt(sTmp) Then
            '    Me.cboWkGrp.SelectedIndex = CInt(IIf(sTmp = "", "0", sTmp))
            'End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_TGrp()

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TGrp_List()

            Me.cboTGrp.Items.Clear()
            Me.cboTGrp.Items.Add("[  ] 전체")

            For intIdx As Integer = 0 To dt.Rows.Count - 1
                Me.cboTGrp.Items.Add("[" + dt.Rows(intIdx).Item("tgrpcd").ToString + "] " + dt.Rows(intIdx).Item("tgrpnmd").ToString)
            Next

            If Me.cboTGrp.Items.Count > 0 Then Me.cboTGrp.SelectedIndex = 0

            'Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msTGrpFile, "TGRP")
            'If sTmp = "" Then sTmp = "0"

            'If Me.cboTGrp.Items.Count > 0 And Me.cboTGrp.Items.Count > CInt(sTmp) Then
            '    Me.cboTGrp.SelectedIndex = CInt(IIf(sTmp = "", "0", sTmp))
            'End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()

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
    Friend WithEvents pnlTop2 As System.Windows.Forms.Panel
    Friend WithEvents spdPatInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents spdOrder As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlTop1 As System.Windows.Forms.Panel
    Friend WithEvents grpTop2 As System.Windows.Forms.GroupBox
    Friend WithEvents grpTop1 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlList As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents grpTop3 As System.Windows.Forms.GroupBox
    Friend WithEvents PnlBottom2 As System.Windows.Forms.Panel
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents txtBCNO As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents tbcDirectQry As System.Windows.Forms.TabControl
    Friend WithEvents tbpDirectQry0 As System.Windows.Forms.TabPage
    Friend WithEvents tbpDirectQry1 As System.Windows.Forms.TabPage
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents lblBcnoSearch As System.Windows.Forms.Label
    Friend WithEvents lblGbnQ As System.Windows.Forms.Label
    Friend WithEvents grpTop As System.Windows.Forms.GroupBox
    Friend WithEvents grpBottom As System.Windows.Forms.GroupBox
    Friend WithEvents lblGbn_IOR As System.Windows.Forms.Label
    Friend WithEvents pnlGbnO As System.Windows.Forms.Panel
    Friend WithEvents picGbnO As System.Windows.Forms.PictureBox
    Friend WithEvents lblGbnO As System.Windows.Forms.Label
    Friend WithEvents pnlR As System.Windows.Forms.Panel
    Friend WithEvents picReser As System.Windows.Forms.PictureBox
    Friend WithEvents lblReser As System.Windows.Forms.Label
    Friend WithEvents pnlGbnI As System.Windows.Forms.Panel
    Friend WithEvents picGbnI As System.Windows.Forms.PictureBox
    Friend WithEvents lblGbnI As System.Windows.Forms.Label
    Friend WithEvents imglBcGbn As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS04))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlTop2 = New System.Windows.Forms.Panel
        Me.spdPatInfo = New AxFPSpreadADO.AxfpSpread
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.cmuList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRst = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuRstView = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuColl = New System.Windows.Forms.ToolStripMenuItem
        Me.spdOrder = New AxFPSpreadADO.AxfpSpread
        Me.pnlTop1 = New System.Windows.Forms.Panel
        Me.tbcDirectQry = New System.Windows.Forms.TabControl
        Me.tbpDirectQry0 = New System.Windows.Forms.TabPage
        Me.grpTop2 = New System.Windows.Forms.GroupBox
        Me.txtBCNO = New System.Windows.Forms.TextBox
        Me.lblBcnoSearch = New System.Windows.Forms.Label
        Me.tbpDirectQry1 = New System.Windows.Forms.TabPage
        Me.grpTop1 = New System.Windows.Forms.GroupBox
        Me.btnToggle = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.pnlList = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.grpTop3 = New System.Windows.Forms.GroupBox
        Me.cboSpcFlg = New System.Windows.Forms.ComboBox
        Me.cboQryGbn = New System.Windows.Forms.ComboBox
        Me.cboIOGbn = New System.Windows.Forms.ComboBox
        Me.cboTGrp = New System.Windows.Forms.ComboBox
        Me.lblTGrp = New System.Windows.Forms.Label
        Me.cboWkGrp = New System.Windows.Forms.ComboBox
        Me.lblWkGrp = New System.Windows.Forms.Label
        Me.cboSlip = New System.Windows.Forms.ComboBox
        Me.lblSlip = New System.Windows.Forms.Label
        Me.cboWard = New System.Windows.Forms.ComboBox
        Me.lblWard = New System.Windows.Forms.Label
        Me.btnQuery = New System.Windows.Forms.Button
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.lblIOGbn = New System.Windows.Forms.Label
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.lblDateTitle = New System.Windows.Forms.Label
        Me.lblGbnQ = New System.Windows.Forms.Label
        Me.lblDay = New System.Windows.Forms.Label
        Me.PnlBottom2 = New System.Windows.Forms.Panel
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnPrint = New CButtonLib.CButton
        Me.grpBottom = New System.Windows.Forms.GroupBox
        Me.pnlGbnI = New System.Windows.Forms.Panel
        Me.picGbnI = New System.Windows.Forms.PictureBox
        Me.lblGbnI = New System.Windows.Forms.Label
        Me.pnlR = New System.Windows.Forms.Panel
        Me.picReser = New System.Windows.Forms.PictureBox
        Me.lblReser = New System.Windows.Forms.Label
        Me.pnlGbnO = New System.Windows.Forms.Panel
        Me.picGbnO = New System.Windows.Forms.PictureBox
        Me.lblGbnO = New System.Windows.Forms.Label
        Me.lblGbn_IOR = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.grpTop = New System.Windows.Forms.GroupBox
        Me.imglBcGbn = New System.Windows.Forms.ImageList(Me.components)
        Me.pnlTop2.SuspendLayout()
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.cmuList.SuspendLayout()
        CType(Me.spdOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop1.SuspendLayout()
        Me.tbcDirectQry.SuspendLayout()
        Me.tbpDirectQry0.SuspendLayout()
        Me.grpTop2.SuspendLayout()
        Me.tbpDirectQry1.SuspendLayout()
        Me.grpTop1.SuspendLayout()
        Me.pnlList.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTop3.SuspendLayout()
        Me.PnlBottom2.SuspendLayout()
        Me.grpBottom.SuspendLayout()
        Me.pnlGbnI.SuspendLayout()
        CType(Me.picGbnI, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlR.SuspendLayout()
        CType(Me.picReser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlGbnO.SuspendLayout()
        CType(Me.picGbnO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTop.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlTop2
        '
        Me.pnlTop2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop2.Controls.Add(Me.spdPatInfo)
        Me.pnlTop2.Location = New System.Drawing.Point(4, 12)
        Me.pnlTop2.Name = "pnlTop2"
        Me.pnlTop2.Size = New System.Drawing.Size(621, 52)
        Me.pnlTop2.TabIndex = 0
        '
        'spdPatInfo
        '
        Me.spdPatInfo.DataSource = Nothing
        Me.spdPatInfo.Location = New System.Drawing.Point(0, 0)
        Me.spdPatInfo.Name = "spdPatInfo"
        Me.spdPatInfo.OcxState = CType(resources.GetObject("spdPatInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPatInfo.Size = New System.Drawing.Size(617, 48)
        Me.spdPatInfo.TabIndex = 0
        Me.spdPatInfo.TabStop = False
        '
        'pnlTop
        '
        Me.pnlTop.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.ContextMenuStrip = Me.cmuList
        Me.pnlTop.Controls.Add(Me.spdOrder)
        Me.pnlTop.Location = New System.Drawing.Point(284, 69)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(728, 527)
        Me.pnlTop.TabIndex = 3
        '
        'cmuList
        '
        Me.cmuList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRst, Me.mnuRstView, Me.mnuColl})
        Me.cmuList.Name = "cmuRstList"
        Me.cmuList.Size = New System.Drawing.Size(195, 70)
        Me.cmuList.Text = "상황에 맞는 메뉴"
        '
        'mnuRst
        '
        Me.mnuRst.Name = "mnuRst"
        Me.mnuRst.Size = New System.Drawing.Size(194, 22)
        Me.mnuRst.Text = "검체별 결과등록"
        '
        'mnuRstView
        '
        Me.mnuRstView.Name = "mnuRstView"
        Me.mnuRstView.Size = New System.Drawing.Size(194, 22)
        Me.mnuRstView.Text = "결과조회(일일보고서)"
        '
        'mnuColl
        '
        Me.mnuColl.Name = "mnuColl"
        Me.mnuColl.Size = New System.Drawing.Size(194, 22)
        Me.mnuColl.Text = "외래채혈"
        Me.mnuColl.Visible = False
        '
        'spdOrder
        '
        Me.spdOrder.ContextMenuStrip = Me.cmuList
        Me.spdOrder.DataSource = Nothing
        Me.spdOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdOrder.Location = New System.Drawing.Point(0, 0)
        Me.spdOrder.Name = "spdOrder"
        Me.spdOrder.OcxState = CType(resources.GetObject("spdOrder.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrder.Size = New System.Drawing.Size(724, 523)
        Me.spdOrder.TabIndex = 0
        '
        'pnlTop1
        '
        Me.pnlTop1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlTop1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop1.Controls.Add(Me.tbcDirectQry)
        Me.pnlTop1.Location = New System.Drawing.Point(4, 4)
        Me.pnlTop1.Name = "pnlTop1"
        Me.pnlTop1.Size = New System.Drawing.Size(276, 64)
        Me.pnlTop1.TabIndex = 0
        '
        'tbcDirectQry
        '
        Me.tbcDirectQry.Controls.Add(Me.tbpDirectQry0)
        Me.tbcDirectQry.Controls.Add(Me.tbpDirectQry1)
        Me.tbcDirectQry.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcDirectQry.HotTrack = True
        Me.tbcDirectQry.ItemSize = New System.Drawing.Size(60, 18)
        Me.tbcDirectQry.Location = New System.Drawing.Point(0, 0)
        Me.tbcDirectQry.Name = "tbcDirectQry"
        Me.tbcDirectQry.SelectedIndex = 0
        Me.tbcDirectQry.Size = New System.Drawing.Size(272, 60)
        Me.tbcDirectQry.TabIndex = 0
        '
        'tbpDirectQry0
        '
        Me.tbpDirectQry0.Controls.Add(Me.grpTop2)
        Me.tbpDirectQry0.Location = New System.Drawing.Point(4, 22)
        Me.tbpDirectQry0.Name = "tbpDirectQry0"
        Me.tbpDirectQry0.Size = New System.Drawing.Size(264, 34)
        Me.tbpDirectQry0.TabIndex = 1
        Me.tbpDirectQry0.Text = "검체번호 조회"
        '
        'grpTop2
        '
        Me.grpTop2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTop2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTop2.Controls.Add(Me.txtBCNO)
        Me.grpTop2.Controls.Add(Me.lblBcnoSearch)
        Me.grpTop2.Location = New System.Drawing.Point(0, -4)
        Me.grpTop2.Name = "grpTop2"
        Me.grpTop2.Size = New System.Drawing.Size(264, 38)
        Me.grpTop2.TabIndex = 0
        Me.grpTop2.TabStop = False
        '
        'txtBCNO
        '
        Me.txtBCNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBCNO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBCNO.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBCNO.Location = New System.Drawing.Point(80, 11)
        Me.txtBCNO.MaxLength = 15
        Me.txtBCNO.Name = "txtBCNO"
        Me.txtBCNO.Size = New System.Drawing.Size(179, 22)
        Me.txtBCNO.TabIndex = 0
        '
        'lblBcnoSearch
        '
        Me.lblBcnoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblBcnoSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblBcnoSearch.ForeColor = System.Drawing.Color.White
        Me.lblBcnoSearch.Location = New System.Drawing.Point(4, 11)
        Me.lblBcnoSearch.Name = "lblBcnoSearch"
        Me.lblBcnoSearch.Size = New System.Drawing.Size(75, 22)
        Me.lblBcnoSearch.TabIndex = 1
        Me.lblBcnoSearch.Text = "검체번호"
        Me.lblBcnoSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbpDirectQry1
        '
        Me.tbpDirectQry1.Controls.Add(Me.grpTop1)
        Me.tbpDirectQry1.Location = New System.Drawing.Point(4, 22)
        Me.tbpDirectQry1.Name = "tbpDirectQry1"
        Me.tbpDirectQry1.Size = New System.Drawing.Size(264, 34)
        Me.tbpDirectQry1.TabIndex = 0
        Me.tbpDirectQry1.Text = "등록번호 조회"
        Me.tbpDirectQry1.Visible = False
        '
        'grpTop1
        '
        Me.grpTop1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTop1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTop1.Controls.Add(Me.btnToggle)
        Me.grpTop1.Controls.Add(Me.txtSearch)
        Me.grpTop1.Controls.Add(Me.lblSearch)
        Me.grpTop1.Location = New System.Drawing.Point(0, -4)
        Me.grpTop1.Name = "grpTop1"
        Me.grpTop1.Size = New System.Drawing.Size(264, 38)
        Me.grpTop1.TabIndex = 0
        Me.grpTop1.TabStop = False
        '
        'btnToggle
        '
        Me.btnToggle.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(216, 11)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(44, 22)
        Me.btnToggle.TabIndex = 3
        Me.btnToggle.Text = "<->"
        Me.btnToggle.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Font = New System.Drawing.Font("굴림", 9.0!)
        Me.txtSearch.Location = New System.Drawing.Point(77, 12)
        Me.txtSearch.MaxLength = 8
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(134, 21)
        Me.txtSearch.TabIndex = 2
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSearch.Location = New System.Drawing.Point(4, 12)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(72, 21)
        Me.lblSearch.TabIndex = 6
        Me.lblSearch.Text = "등록번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlList
        '
        Me.pnlList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlList.BackColor = System.Drawing.SystemColors.Control
        Me.pnlList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlList.Controls.Add(Me.spdList)
        Me.pnlList.Location = New System.Drawing.Point(4, 152)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Size = New System.Drawing.Size(268, 376)
        Me.pnlList.TabIndex = 0
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(264, 372)
        Me.spdList.TabIndex = 0
        '
        'grpTop3
        '
        Me.grpTop3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpTop3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTop3.Controls.Add(Me.cboSpcFlg)
        Me.grpTop3.Controls.Add(Me.cboQryGbn)
        Me.grpTop3.Controls.Add(Me.cboIOGbn)
        Me.grpTop3.Controls.Add(Me.cboTGrp)
        Me.grpTop3.Controls.Add(Me.lblTGrp)
        Me.grpTop3.Controls.Add(Me.cboWkGrp)
        Me.grpTop3.Controls.Add(Me.lblWkGrp)
        Me.grpTop3.Controls.Add(Me.cboSlip)
        Me.grpTop3.Controls.Add(Me.lblSlip)
        Me.grpTop3.Controls.Add(Me.cboWard)
        Me.grpTop3.Controls.Add(Me.lblWard)
        Me.grpTop3.Controls.Add(Me.btnQuery)
        Me.grpTop3.Controls.Add(Me.dtpDateE)
        Me.grpTop3.Controls.Add(Me.lblIOGbn)
        Me.grpTop3.Controls.Add(Me.dtpDateS)
        Me.grpTop3.Controls.Add(Me.lblDateTitle)
        Me.grpTop3.Controls.Add(Me.lblGbnQ)
        Me.grpTop3.Controls.Add(Me.pnlList)
        Me.grpTop3.Controls.Add(Me.lblDay)
        Me.grpTop3.Location = New System.Drawing.Point(4, 64)
        Me.grpTop3.Name = "grpTop3"
        Me.grpTop3.Size = New System.Drawing.Size(276, 532)
        Me.grpTop3.TabIndex = 1
        Me.grpTop3.TabStop = False
        '
        'cboSpcFlg
        '
        Me.cboSpcFlg.AutoCompleteCustomSource.AddRange(New String() {"[0] 미채혈", "[1] 미접수", "[2] 미결과", "[3] 접수이상"})
        Me.cboSpcFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpcFlg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSpcFlg.Location = New System.Drawing.Point(77, 12)
        Me.cboSpcFlg.Name = "cboSpcFlg"
        Me.cboSpcFlg.Size = New System.Drawing.Size(194, 20)
        Me.cboSpcFlg.TabIndex = 121
        '
        'cboQryGbn
        '
        Me.cboQryGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQryGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboQryGbn.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboQryGbn.Location = New System.Drawing.Point(77, 78)
        Me.cboQryGbn.Name = "cboQryGbn"
        Me.cboQryGbn.Size = New System.Drawing.Size(54, 20)
        Me.cboQryGbn.TabIndex = 120
        '
        'cboIOGbn
        '
        Me.cboIOGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIOGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboIOGbn.Items.AddRange(New Object() {"전체", "외래", "입원"})
        Me.cboIOGbn.Location = New System.Drawing.Point(77, 57)
        Me.cboIOGbn.Name = "cboIOGbn"
        Me.cboIOGbn.Size = New System.Drawing.Size(55, 20)
        Me.cboIOGbn.TabIndex = 119
        '
        'cboTGrp
        '
        Me.cboTGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTGrp.Location = New System.Drawing.Point(77, 98)
        Me.cboTGrp.Name = "cboTGrp"
        Me.cboTGrp.Size = New System.Drawing.Size(146, 20)
        Me.cboTGrp.TabIndex = 118
        '
        'lblTGrp
        '
        Me.lblTGrp.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTGrp.ForeColor = System.Drawing.Color.Black
        Me.lblTGrp.Location = New System.Drawing.Point(5, 99)
        Me.lblTGrp.Name = "lblTGrp"
        Me.lblTGrp.Size = New System.Drawing.Size(71, 20)
        Me.lblTGrp.TabIndex = 117
        Me.lblTGrp.Text = "검사그룹"
        Me.lblTGrp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboWkGrp
        '
        Me.cboWkGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWkGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWkGrp.Location = New System.Drawing.Point(77, 120)
        Me.cboWkGrp.Name = "cboWkGrp"
        Me.cboWkGrp.Size = New System.Drawing.Size(146, 20)
        Me.cboWkGrp.TabIndex = 116
        Me.cboWkGrp.Visible = False
        '
        'lblWkGrp
        '
        Me.lblWkGrp.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblWkGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWkGrp.ForeColor = System.Drawing.Color.Black
        Me.lblWkGrp.Location = New System.Drawing.Point(5, 120)
        Me.lblWkGrp.Name = "lblWkGrp"
        Me.lblWkGrp.Size = New System.Drawing.Size(71, 20)
        Me.lblWkGrp.TabIndex = 115
        Me.lblWkGrp.Text = "작업그룹"
        Me.lblWkGrp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblWkGrp.Visible = False
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSlip.Location = New System.Drawing.Point(132, 78)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(138, 20)
        Me.cboSlip.TabIndex = 114
        '
        'lblSlip
        '
        Me.lblSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlip.ForeColor = System.Drawing.Color.Black
        Me.lblSlip.Location = New System.Drawing.Point(5, 78)
        Me.lblSlip.Name = "lblSlip"
        Me.lblSlip.Size = New System.Drawing.Size(71, 20)
        Me.lblSlip.TabIndex = 113
        Me.lblSlip.Text = "검사분야"
        Me.lblSlip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWard.Location = New System.Drawing.Point(200, 57)
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(71, 20)
        Me.cboWard.TabIndex = 104
        Me.cboWard.Visible = False
        '
        'lblWard
        '
        Me.lblWard.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblWard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWard.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblWard.ForeColor = System.Drawing.Color.Black
        Me.lblWard.Location = New System.Drawing.Point(133, 57)
        Me.lblWard.Name = "lblWard"
        Me.lblWard.Size = New System.Drawing.Size(66, 20)
        Me.lblWard.TabIndex = 111
        Me.lblWard.Text = "병동"
        Me.lblWard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblWard.Visible = False
        '
        'btnQuery
        '
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnQuery.Location = New System.Drawing.Point(226, 99)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(44, 42)
        Me.btnQuery.TabIndex = 108
        Me.btnQuery.Text = "조  회"
        '
        'dtpDateE
        '
        Me.dtpDateE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(183, 35)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateE.TabIndex = 105
        '
        'lblIOGbn
        '
        Me.lblIOGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblIOGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIOGbn.ForeColor = System.Drawing.Color.Black
        Me.lblIOGbn.Location = New System.Drawing.Point(5, 57)
        Me.lblIOGbn.Name = "lblIOGbn"
        Me.lblIOGbn.Size = New System.Drawing.Size(71, 20)
        Me.lblIOGbn.TabIndex = 110
        Me.lblIOGbn.Text = "입외구분"
        Me.lblIOGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateS
        '
        Me.dtpDateS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(77, 35)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateS.TabIndex = 103
        '
        'lblDateTitle
        '
        Me.lblDateTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDateTitle.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDateTitle.ForeColor = System.Drawing.Color.White
        Me.lblDateTitle.Location = New System.Drawing.Point(5, 35)
        Me.lblDateTitle.Name = "lblDateTitle"
        Me.lblDateTitle.Size = New System.Drawing.Size(71, 21)
        Me.lblDateTitle.TabIndex = 109
        Me.lblDateTitle.Text = "처방일자"
        Me.lblDateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGbnQ
        '
        Me.lblGbnQ.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblGbnQ.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGbnQ.ForeColor = System.Drawing.Color.White
        Me.lblGbnQ.Location = New System.Drawing.Point(5, 12)
        Me.lblGbnQ.Name = "lblGbnQ"
        Me.lblGbnQ.Size = New System.Drawing.Size(71, 22)
        Me.lblGbnQ.TabIndex = 102
        Me.lblGbnQ.Text = "조회구분"
        Me.lblGbnQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDay
        '
        Me.lblDay.AutoSize = True
        Me.lblDay.Location = New System.Drawing.Point(170, 40)
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(11, 12)
        Me.lblDay.TabIndex = 106
        Me.lblDay.Text = "~"
        Me.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlBottom2
        '
        Me.PnlBottom2.Controls.Add(Me.btnClear)
        Me.PnlBottom2.Controls.Add(Me.btnExit)
        Me.PnlBottom2.Controls.Add(Me.btnPrint)
        Me.PnlBottom2.Controls.Add(Me.grpBottom)
        Me.PnlBottom2.Controls.Add(Me.lblUserNm)
        Me.PnlBottom2.Controls.Add(Me.lblUserId)
        Me.PnlBottom2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PnlBottom2.Location = New System.Drawing.Point(0, 595)
        Me.PnlBottom2.Name = "PnlBottom2"
        Me.PnlBottom2.Size = New System.Drawing.Size(1016, 34)
        Me.PnlBottom2.TabIndex = 4
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems1
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
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker2
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(819, 4)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(96, 25)
        Me.btnClear.TabIndex = 204
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems2
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
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker4
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(916, 4)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(96, 25)
        Me.btnExit.TabIndex = 203
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems3
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
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker6
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(722, 4)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(96, 25)
        Me.btnPrint.TabIndex = 202
        Me.btnPrint.Text = "출  력(F5)"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'grpBottom
        '
        Me.grpBottom.Controls.Add(Me.pnlGbnI)
        Me.grpBottom.Controls.Add(Me.pnlR)
        Me.grpBottom.Controls.Add(Me.pnlGbnO)
        Me.grpBottom.Controls.Add(Me.lblGbn_IOR)
        Me.grpBottom.Location = New System.Drawing.Point(4, -4)
        Me.grpBottom.Name = "grpBottom"
        Me.grpBottom.Size = New System.Drawing.Size(276, 32)
        Me.grpBottom.TabIndex = 11
        Me.grpBottom.TabStop = False
        '
        'pnlGbnI
        '
        Me.pnlGbnI.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlGbnI.Controls.Add(Me.picGbnI)
        Me.pnlGbnI.Controls.Add(Me.lblGbnI)
        Me.pnlGbnI.ForeColor = System.Drawing.Color.Navy
        Me.pnlGbnI.Location = New System.Drawing.Point(208, 9)
        Me.pnlGbnI.Name = "pnlGbnI"
        Me.pnlGbnI.Size = New System.Drawing.Size(63, 20)
        Me.pnlGbnI.TabIndex = 3
        '
        'picGbnI
        '
        Me.picGbnI.BackColor = System.Drawing.Color.White
        Me.picGbnI.Image = CType(resources.GetObject("picGbnI.Image"), System.Drawing.Image)
        Me.picGbnI.Location = New System.Drawing.Point(4, 3)
        Me.picGbnI.Name = "picGbnI"
        Me.picGbnI.Size = New System.Drawing.Size(12, 14)
        Me.picGbnI.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picGbnI.TabIndex = 0
        Me.picGbnI.TabStop = False
        '
        'lblGbnI
        '
        Me.lblGbnI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGbnI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGbnI.Location = New System.Drawing.Point(0, 0)
        Me.lblGbnI.Name = "lblGbnI"
        Me.lblGbnI.Size = New System.Drawing.Size(63, 20)
        Me.lblGbnI.TabIndex = 3
        Me.lblGbnI.Text = "　　입원"
        Me.lblGbnI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlR
        '
        Me.pnlR.BackColor = System.Drawing.Color.Honeydew
        Me.pnlR.Controls.Add(Me.picReser)
        Me.pnlR.Controls.Add(Me.lblReser)
        Me.pnlR.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.pnlR.Location = New System.Drawing.Point(144, 9)
        Me.pnlR.Name = "pnlR"
        Me.pnlR.Size = New System.Drawing.Size(63, 20)
        Me.pnlR.TabIndex = 2
        '
        'picReser
        '
        Me.picReser.BackColor = System.Drawing.Color.White
        Me.picReser.Image = CType(resources.GetObject("picReser.Image"), System.Drawing.Image)
        Me.picReser.Location = New System.Drawing.Point(4, 3)
        Me.picReser.Name = "picReser"
        Me.picReser.Size = New System.Drawing.Size(12, 14)
        Me.picReser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picReser.TabIndex = 0
        Me.picReser.TabStop = False
        '
        'lblReser
        '
        Me.lblReser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblReser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblReser.Location = New System.Drawing.Point(0, 0)
        Me.lblReser.Name = "lblReser"
        Me.lblReser.Size = New System.Drawing.Size(63, 20)
        Me.lblReser.TabIndex = 3
        Me.lblReser.Text = "　　예약"
        Me.lblReser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlGbnO
        '
        Me.pnlGbnO.BackColor = System.Drawing.Color.LavenderBlush
        Me.pnlGbnO.Controls.Add(Me.picGbnO)
        Me.pnlGbnO.Controls.Add(Me.lblGbnO)
        Me.pnlGbnO.ForeColor = System.Drawing.Color.Brown
        Me.pnlGbnO.Location = New System.Drawing.Point(80, 9)
        Me.pnlGbnO.Name = "pnlGbnO"
        Me.pnlGbnO.Size = New System.Drawing.Size(63, 20)
        Me.pnlGbnO.TabIndex = 1
        '
        'picGbnO
        '
        Me.picGbnO.BackColor = System.Drawing.Color.White
        Me.picGbnO.Image = CType(resources.GetObject("picGbnO.Image"), System.Drawing.Image)
        Me.picGbnO.Location = New System.Drawing.Point(4, 3)
        Me.picGbnO.Name = "picGbnO"
        Me.picGbnO.Size = New System.Drawing.Size(12, 14)
        Me.picGbnO.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picGbnO.TabIndex = 0
        Me.picGbnO.TabStop = False
        '
        'lblGbnO
        '
        Me.lblGbnO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGbnO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblGbnO.Location = New System.Drawing.Point(0, 0)
        Me.lblGbnO.Name = "lblGbnO"
        Me.lblGbnO.Size = New System.Drawing.Size(63, 20)
        Me.lblGbnO.TabIndex = 3
        Me.lblGbnO.Text = "　　외래"
        Me.lblGbnO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGbn_IOR
        '
        Me.lblGbn_IOR.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblGbn_IOR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGbn_IOR.ForeColor = System.Drawing.Color.White
        Me.lblGbn_IOR.Location = New System.Drawing.Point(3, 9)
        Me.lblGbn_IOR.Name = "lblGbn_IOR"
        Me.lblGbn_IOR.Size = New System.Drawing.Size(77, 20)
        Me.lblGbn_IOR.TabIndex = 0
        Me.lblGbn_IOR.Text = "범   례"
        Me.lblGbn_IOR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(356, 8)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 4
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(284, 8)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 3
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'grpTop
        '
        Me.grpTop.Controls.Add(Me.pnlTop2)
        Me.grpTop.Location = New System.Drawing.Point(284, -2)
        Me.grpTop.Name = "grpTop"
        Me.grpTop.Size = New System.Drawing.Size(631, 70)
        Me.grpTop.TabIndex = 2
        Me.grpTop.TabStop = False
        '
        'imglBcGbn
        '
        Me.imglBcGbn.ImageStream = CType(resources.GetObject("imglBcGbn.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imglBcGbn.TransparentColor = System.Drawing.Color.Transparent
        Me.imglBcGbn.Images.SetKeyName(0, "")
        Me.imglBcGbn.Images.SetKeyName(1, "")
        '
        'FGS04
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 629)
        Me.Controls.Add(Me.grpTop)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlTop1)
        Me.Controls.Add(Me.grpTop3)
        Me.Controls.Add(Me.PnlBottom2)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS04"
        Me.Text = "환자/검체현황 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlTop2.ResumeLayout(False)
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.cmuList.ResumeLayout(False)
        CType(Me.spdOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop1.ResumeLayout(False)
        Me.tbcDirectQry.ResumeLayout(False)
        Me.tbpDirectQry0.ResumeLayout(False)
        Me.grpTop2.ResumeLayout(False)
        Me.grpTop2.PerformLayout()
        Me.tbpDirectQry1.ResumeLayout(False)
        Me.grpTop1.ResumeLayout(False)
        Me.grpTop1.PerformLayout()
        Me.pnlList.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTop3.ResumeLayout(False)
        Me.grpTop3.PerformLayout()
        Me.PnlBottom2.ResumeLayout(False)
        Me.grpBottom.ResumeLayout(False)
        Me.pnlGbnI.ResumeLayout(False)
        Me.pnlGbnI.PerformLayout()
        CType(Me.picGbnI, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlR.ResumeLayout(False)
        Me.pnlR.PerformLayout()
        CType(Me.picReser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlGbnO.ResumeLayout(False)
        Me.pnlGbnO.PerformLayout()
        CType(Me.picGbnO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTop.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Spread 보기기/숨김 "
    Private Sub Form_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick

        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        sbSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If
    End Sub
#End Region

#Region " 메인 버튼 처리 "

    Private Sub FGS04_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    ' Function Key정의
    Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F2
                If tbcDirectQry.SelectedIndex <> 0 Then tbcDirectQry.SelectedIndex = 1
                If lblSearch.Text <> "등록번호" Then
                    btnToggle_Click(Nothing, Nothing)
                End If
                txtSearch.Focus()

            Case Keys.F3
                If tbcDirectQry.SelectedIndex <> 0 Then tbcDirectQry.SelectedIndex = 1
                If lblSearch.Text = "등록번호" Then
                    btnToggle_Click(Nothing, Nothing)
                End If
                txtSearch.Focus()

            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Try
            sbFormClear(0)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

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

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList

            With spdOrder
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

                            If strField = "colldt" And strVal <> "" Then strVal = strVal.Substring(2)
                            If strField = "tkdt" And strVal <> "" Then strVal = strVal.Substring(2)
                            If strField = "rstdt" And strVal <> "" Then strVal = strVal.Substring(2)

                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                        Else
                            intCol = spdPatInfo.GetColFromID(strBuf(intIdx).Split("^"c)(1))

                            If intCol > 0 Then

                                Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                                Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                                Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                                spdPatInfo.Row = 1
                                spdPatInfo.Col = spdPatInfo.GetColFromID(strField) : Dim strVal As String = spdPatInfo.Text

                                arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                            End If
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
                Dim sRegNo As String = "", sPatNm As String = "", sSexAge As String = ""

                With spdPatInfo
                    .Row = 1
                    .Col = .GetColFromID("regno") : sRegNo = .Text
                    .Col = .GetColFromID("patnm") : sPatNm = .Text
                    .Col = .GetColFromID("sexage") : sSexAge = .Text
                End With

                Dim prt As New FGS00_PRINT
                prt.mbLandscape = False  '-- false : 세로, true : 가로
                prt.msTitle = "환자/검체 현황 조회"
                prt.msTitle_sub_left_2 = "등록번호: " + sRegNo + Space(10) + "성  명: " + sPatNm + "(" + sSexAge + ")"
                prt.maPrtData = arlPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region

#Region " Form내부 함수 "
    ' Form초기화
    Private Sub sbFormInitialize()

        Try
            Me.txtSearch.MaxLength = PRG_CONST.Len_RegNo

            Me.spdList.MaxRows = 0
            Me.spdList.Tag = ""

            Me.cboSpcFlg.Items.Clear()
            Me.cboSpcFlg.Items.Add("[0] 미채혈")
            Me.cboSpcFlg.Items.Add("[1] 미접수")
            Me.cboSpcFlg.Items.Add("[2] 미결과")
            Me.cboSpcFlg.Items.Add("[3] 접수이상")
            If Me.cboSpcFlg.Items.Count > 0 Then Me.cboSpcFlg.SelectedIndex = 0

            '-- 서버날짜로 설정
            Me.dtpDateE.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
            Me.dtpDateS.Value = Me.dtpDateE.Value

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            Me.txtBCNO.Text = ""
            Me.txtSearch.Text = ""

            sbDisplay_Slip()
            sbDisplay_WkGrp()
            sbDisplay_TGrp()

            sbSpreadColHidden(True)
            sbFormClear(0)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub

    ' 화면정리
    Private Sub sbFormClear(ByVal aiPhase As Integer)

        Try
            If InStr("0", aiPhase.ToString, CompareMethod.Text) > 0 Then
                Me.txtBCNO.Text = ""
                Me.txtSearch.Text = ""
            End If

            If InStr("01", aiPhase.ToString, CompareMethod.Text) > 0 Then
                Me.spdList.MaxRows = 0
            End If

            If InStr("012", aiPhase.ToString, CompareMethod.Text) > 0 Then
                Me.spdPatInfo.ClearRange(1, 1, Me.spdPatInfo.MaxCols, 2, True)
                Me.spdOrder.MaxRows = 0
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub

    ' 칼럼 Hidden 유무
    Private Sub sbSpreadColHidden(ByVal abFlag As Boolean)

        Try
            With spdList
                If Ctrl.Get_Code(Me.cboSpcFlg) = "0" Then
                    .Col = .GetColFromID("orddt") : .ColHidden = False
                    .Col = .GetColFromID("tkdt") : .ColHidden = abFlag
                ElseIf Ctrl.Get_Code(Me.cboSpcFlg) = "1" Then
                    .Col = .GetColFromID("orddt") : .ColHidden = False
                    .Col = .GetColFromID("tkdt") : .ColHidden = abFlag
                ElseIf Ctrl.Get_Code(Me.cboSpcFlg) >= "2" Then
                    .Col = .GetColFromID("orddt") : .ColHidden = abFlag
                    .Col = .GetColFromID("tkdt") : .ColHidden = False
                End If

                .Col = .GetColFromID("sexage") : .ColHidden = abFlag
                .Col = .GetColFromID("idno") : .ColHidden = abFlag
                .Col = .GetColFromID("iogbn") : .ColHidden = abFlag
                .Col = .GetColFromID("wardroom") : .ColHidden = abFlag
                .Col = .GetColFromID("resdt") : .ColHidden = abFlag
                .Col = .GetColFromID("spcflg") : .ColHidden = abFlag
                .Col = .GetColFromID("owngbn") : .ColHidden = abFlag
            End With

            With spdOrder
                .Col = .GetColFromID("rstflg") : .ColHidden = abFlag
                .Col = .GetColFromID("orddt") : .ColHidden = abFlag
                .Col = .GetColFromID("testcd") : .ColHidden = abFlag
                .Col = .GetColFromID("spccd") : .ColHidden = abFlag
                .Col = .GetColFromID("fkocs") : .ColHidden = abFlag
                .Col = .GetColFromID("owngbn") : .ColHidden = abFlag
                .Col = .GetColFromID("tordcd") : .ColHidden = abFlag
                .Col = .GetColFromID("spcflg") : .ColHidden = abFlag
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 환자리스트 보기
    Private Sub sbDiplay_patlist(ByVal r_dt As DataTable)

        Try
            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim alKey As New ArrayList

            With spdList
                .MaxRows = 0
                For ix As Integer = 0 To r_dt.Rows.Count - 1

                    If alKey.Contains(r_dt.Rows(ix).Item("regno").ToString + r_dt.Rows(ix).Item("orddt").ToString + r_dt.Rows(ix).Item("iogbn").ToString) Then
                    Else
                        .MaxRows += 1

                        .Row = .MaxRows
                        .Col = .GetColFromID("iogbn_vw")
                        If r_dt.Rows(ix).Item("resdt").ToString <> "" And r_dt.Rows(ix).Item("iogbn").ToString = "O" Then
                            .TypePictPicture = GetImgList.getPatGbn(enumImgPatGbn.예약)
                        Else
                            If r_dt.Rows(ix).Item("iogbn").ToString = "I" Then
                                .TypePictPicture = GetImgList.getPatGbn(enumImgPatGbn.입원)
                            Else
                                .TypePictPicture = GetImgList.getPatGbn(enumImgPatGbn.외래)
                            End If
                        End If

                        Dim sBuf() As String = r_dt.Rows(ix).Item("patinfo").ToString.Split("|"c)
                        '< 나이계산
                        Dim dtBirthDay As Date = CDate(sBuf(2).Trim)
                        Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                        If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                        '>

                        .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(ix).Item("orddt").ToString
                        .Col = .GetColFromID("colldt") : .Text = r_dt.Rows(ix).Item("colldt").ToString
                        .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString
                        .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString
                        .Col = .GetColFromID("patnm") : .Text = sBuf(0)
                        .Col = .GetColFromID("sexage") : .Text = sBuf(1) + "/" + iAge.ToString

                        .Col = .GetColFromID("idno") : .Text = sBuf(3)
                        .Col = .GetColFromID("iogbn") : .Text = r_dt.Rows(ix).Item("iogbn").ToString
                        .Col = .GetColFromID("wardroom") : .Text = r_dt.Rows(ix).Item("wardroom").ToString
                        .Col = .GetColFromID("resdt") : .Text = r_dt.Rows(ix).Item("resdt").ToString
                        .Col = .GetColFromID("spcflg") : .Text = r_dt.Rows(ix).Item("spcflg").ToString
                        .Col = .GetColFromID("owngbn") : .Text = r_dt.Rows(ix).Item("owngbn").ToString


                        alKey.Add(r_dt.Rows(ix).Item("regno").ToString + r_dt.Rows(ix).Item("orddt").ToString + r_dt.Rows(ix).Item("iogbn").ToString)
                    End If

                Next
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 환자정보 보기
    Private Sub sbDisplay_patinfo(ByVal ro_PatInfo As STU_PatInfo_S)

        Try
            With spdPatInfo
                .Row = 2
                .Col = .GetColFromID("orddt") : .Text = ro_PatInfo.ORDDT.Substring(0, 10) ' 처방일자
                .Col = .GetColFromID("regno") : .Text = ro_PatInfo.REGNO ' 등록번호
                .Col = .GetColFromID("patnm") : .Text = ro_PatInfo.PATNM ' 성명
                .Col = .GetColFromID("sexage") : .Text = ro_PatInfo.SexAge ' Sex/Age
                .Col = .GetColFromID("idno") : .Text = ro_PatInfo.IDNO ' 주민등록번호
                .Col = .GetColFromID("deptcd") : .Text = ro_PatInfo.DEPT ' 진료과
                .Col = .GetColFromID("wardroom") : .Text = ro_PatInfo.WardRoom ' 병동/병실
                .Col = .GetColFromID("resdt") : .Text = ro_PatInfo.RESDT ' 예약일자
                .Col = .GetColFromID("iogbn") : .Text = ro_PatInfo.IOGBN
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 검사항목 보기
    Private Sub sbDisplay_test(ByVal r_dt As DataTable)
        Dim intGrpNo As Integer
        Dim SpdBackColor As System.Drawing.Color

        Dim RowData As STU_TestItemInfo
        Dim OldRowData As New STU_TestItemInfo

        Dim blnNewBCNO As Boolean = False   ' 새로운 검체번호 발생 여부
        Dim blnNewPLURAL As Boolean = False ' 복수구분 증가여부 

        Dim strOldOrdDate As String = ""
        Dim strOldDeptNm As String = ""
        Dim strOldDoctor As String = ""
        Dim strOldSpcNm As String = ""

        Try
            If r_dt.Rows.Count = 0 Then Return

            With spdOrder
                .ReDraw = False
                .MaxRows = 0
                .MaxRows = r_dt.Rows.Count
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    RowData = New STU_TestItemInfo
                    With r_dt.Rows(ix)
                        RowData.ORDDT = .Item("orddt").ToString.Trim                    '처방일자
                        RowData.DEPTCD = .Item("deptcd").ToString.Trim                  '과코드
                        RowData.DEPTNM = .Item("deptward").ToString.Trim                '진료과/병동
                        RowData.DOCTORCD = .Item("doctorcd").ToString.Trim              '의뢰의사코드
                        RowData.DOCTORNM = .Item("doctornm").ToString.Trim              '의뢰의사명

                        RowData.TESTCD = .Item("testcd").ToString.Trim                  '검사코드
                        RowData.TNMD = .Item("tnmd").ToString.Trim                      '검사명
                        RowData.SPCCD = .Item("spccd").ToString.Trim                    '검체코드
                        RowData.SPCNMD = .Item("spcnmd").ToString.Trim                  '검체명
                        RowData.STATGBN = .Item("statgbn").ToString.Trim              '응급 구분

                        RowData.BCCLSCD = .Item("bcclscd").ToString.Trim               '계코드 & 검사계 코드
                        RowData.TUBECD = .Item("TUBECD").ToString.Trim                  '검체용기구분
                        RowData.EXLABYN = .Item("EXLABYN").ToString.Trim                '위탁기관 유/무
                        RowData.SEQTYN = .Item("SEQTYN").ToString.Trim                  '연속검사
                        RowData.SEQTMI = .Item("SEQTMI").ToString.Trim                  '연속시간

                        RowData.IOGBN = .Item("iogbn").ToString.Trim             '외래/입원 구분
                        RowData.FKOCS = .Item("FKOCS").ToString.Trim                    'OCSKEY 
                        RowData.OWNGBN = .Item("OWNGBN").ToString.Trim                  'OCS처방 or LIS처방
                        RowData.ORDTCLSCD = .Item("tordcd").ToString.Trim         '처방항목코드
                        RowData.APPEND_YN = .Item("APPEND_YN").ToString.Trim            '추가여부

                        'RowData.HOPEDT = .Item("hopedt").ToString.Trim                  '예약일시
                        RowData.BCNO = .Item("bcno").ToString.Trim                      '검체번호
                        RowData.WORKNO = .Item("workno").ToString.Trim                  '작업번호
                        RowData.SPCFLG = .Item("spcflg").ToString.Trim                '검체상태
                        RowData.RSTFLG = .Item("rstflg").ToString                     '결과상태
                    End With

                    ' 새로운 바코드 구분
                    blnNewBCNO = (New CollReg).fnNewBCNO_Judge(RowData, OldRowData)

                    If blnNewBCNO = True Then
                        intGrpNo += 1
                        .Row = ix + 1
                        .Col = 0 : .Text = intGrpNo.ToString
                        .Col = .GetColFromID("chkbc")
                        If RowData.SPCFLG = "" Then
                            .TypePictPicture = imglBcGbn.Images(0)
                        ElseIf RowData.SPCFLG = "1" Or RowData.SPCFLG = "2" Or RowData.SPCFLG = "3" Or RowData.SPCFLG = "4" Then
                            .TypePictPicture = imglBcGbn.Images(1)
                        Else

                        End If

                        If intGrpNo Mod 2 = 1 Then
                            SpdBackColor = System.Drawing.Color.White
                        Else
                            SpdBackColor = System.Drawing.Color.FromArgb(255, 251, 244)
                        End If

                        .Row = ix + 1
                        .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(ix).Item("orddt").ToString
                        .Col = .GetColFromID("colldt") : .Text = r_dt.Rows(ix).Item("colldt").ToString : .ForeColor = JobGbn.FrColor(enumJobGbn.채혈)
                        .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString : .ForeColor = JobGbn.FrColor(enumJobGbn.접수)
                        .Col = .GetColFromID("bcno") : .Text = Fn.BCNO_View(r_dt.Rows(ix).Item("bcno").ToString, True)
                        .Col = .GetColFromID("workno") : .Text = Fn.WKNO_View(r_dt.Rows(ix).Item("workno").ToString)

                        strOldOrdDate = ""
                        strOldDeptNm = ""
                        strOldDoctor = ""
                        strOldSpcNm = ""

                        ' Line 그리기
                        If ix > 0 Then Fn.DrawBorderLineTop(spdOrder, ix + 1)
                    End If

                    '배경색 설정
                    .Row = ix + 1
                    .Col = -1 : .BackColor = SpdBackColor

                    If strOldOrdDate <> RowData.ORDDT Then
                        .Col = .GetColFromID("ordtm") : .Text = CType(r_dt.Rows(ix).Item("orddt").ToString, Date).ToString("HH:mm")
                        If RowData.APPEND_YN = "Y" Then .ForeColor = System.Drawing.Color.Blue
                        strOldOrdDate = RowData.ORDDT

                        .Col = .GetColFromID("deptward") : .Text = r_dt.Rows(ix).Item("deptward").ToString
                        strOldDeptNm = RowData.DEPTNM
                    Else
                        If strOldDeptNm <> RowData.DEPTNM Then
                            .Col = .GetColFromID("deptward") : .Text = r_dt.Rows(ix).Item("deptward").ToString
                            strOldDeptNm = RowData.DEPTNM
                        End If
                    End If

                    If strOldDoctor <> RowData.DOCTORCD Then
                        .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString
                        strOldDoctor = RowData.DOCTORCD
                    End If

                    .Row = ix + 1
                    If RowData.SPCFLG <= "0" Then
                        .Col = .GetColFromID("statgbn") : .Text = "미채혈"
                        .ForeColor = JobGbn.FrColor(enumJobGbn.미채혈)
                        .BackColor = JobGbn.BkColor(enumJobGbn.미채혈, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    ElseIf RowData.SPCFLG = "1" Then
                        .Col = .GetColFromID("statgbn") : .Text = "바코드"
                        .ForeColor = JobGbn.FrColor(enumJobGbn.바코드)
                        .BackColor = JobGbn.BkColor(enumJobGbn.바코드, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    ElseIf RowData.SPCFLG = "2" Then
                        .Col = .GetColFromID("statgbn") : .Text = "채혈"
                        .ForeColor = JobGbn.FrColor(enumJobGbn.채혈)
                        .BackColor = JobGbn.BkColor(enumJobGbn.채혈, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    ElseIf RowData.SPCFLG = "4" And RowData.RSTFLG <= "0" Then
                        .Col = .GetColFromID("statgbn") : .Text = "접수"
                        .ForeColor = JobGbn.FrColor(enumJobGbn.접수)
                        .BackColor = JobGbn.BkColor(enumJobGbn.접수, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    ElseIf RowData.SPCFLG = "4" And RowData.RSTFLG = "1" Then
                        .Col = .GetColFromID("statgbn") : .Text = "미보고"
                        .ForeColor = JobGbn.FrColor(enumJobGbn.접수)
                        .BackColor = JobGbn.BkColor(enumJobGbn.접수, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    ElseIf RowData.SPCFLG = "4" And RowData.RSTFLG = "2" Then
                        .Col = .GetColFromID("statgbn") : .Text = "중간보고"
                        .ForeColor = JobGbn.FrColor(enumJobGbn.보고)
                        .BackColor = JobGbn.BkColor(enumJobGbn.보고, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    ElseIf RowData.SPCFLG = "4" And RowData.RSTFLG = "3" Then
                        .Col = .GetColFromID("statgbn") : .Text = "최종보고"
                        .ForeColor = JobGbn.FrColor(enumJobGbn.보고)
                        .BackColor = JobGbn.BkColor(enumJobGbn.보고, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    End If

                    .Col = .GetColFromID("rstflg") : .Text = RowData.RSTFLG
                    .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix).Item("tnmd").ToString

                    If strOldSpcNm <> RowData.SPCNMD Then
                        .Col = .GetColFromID("spcnmd") : .Text = RowData.SPCNMD
                        strOldSpcNm = RowData.SPCNMD
                    End If

                    .Row = ix + 1
                    .Col = .GetColFromID("colldt")
                    .BackColor = JobGbn.BkColor(enumJobGbn.채혈, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))

                    .Col = .GetColFromID("tkdt")
                    .BackColor = JobGbn.BkColor(enumJobGbn.접수, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))

                    .Col = .GetColFromID("rstdt")
                    .ForeColor = JobGbn.FrColor(enumJobGbn.보고)
                    .BackColor = JobGbn.BkColor(enumJobGbn.보고, CType(IIf(intGrpNo Mod 2 = 1, False, True), Boolean))
                    .Text = r_dt.Rows(ix).Item("rstdt").ToString


                    .Col = .GetColFromID("testcd") : .Text = RowData.TESTCD
                    .Col = .GetColFromID("spccd") : .Text = RowData.SPCCD
                    .Col = .GetColFromID("fkocs") : .Text = RowData.FKOCS
                    .Col = .GetColFromID("owngbn") : .Text = RowData.OWNGBN
                    .Col = .GetColFromID("tordcd") : .Text = RowData.ORDTCLSCD
                    .Col = .GetColFromID("spcflg") : .Text = RowData.SPCFLG
                Next

                .ReDraw = True

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            SpdBackColor = Nothing

        End Try

    End Sub

#End Region

#Region " Control Event 처리 "

    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn
        CommFn.SearchToggle(lblSearch, btnToggle, enumToggle.RegnoToName, Me.txtSearch)
        Me.txtSearch.Focus()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtSearch.Text = "" Then Return

        Try
            Dim sRegNo As String = ""

            If Me.lblSearch.Text = "등록번호" Then
                If IsNumeric(Me.txtSearch.Text.Substring(0, 1)) Then
                    Me.txtSearch.Text = Me.txtSearch.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                Else
                    Me.txtSearch.Text = Me.txtSearch.Text.Substring(0, 1) + Me.txtSearch.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If

                sRegNo = Me.txtSearch.Text
            Else

                Dim dt As New DataTable
                Dim objHelp As New CDHELP.FGCDHELP01
                Dim alList As New ArrayList

                objHelp.FormText = "환자정보"

                dt = OCSAPP.OcsLink.Pat.fnGet_Patinfo("", Me.txtSearch.Text)

                objHelp.MaxRows = 15
                objHelp.Distinct = True
                objHelp.OnRowReturnYN = True

                objHelp.AddField("bunho", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("suname", "성명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("sex", "성별", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("idno", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

                Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
                Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtSearch)

                alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtSearch.Height + 80, dt)

                If alList.Count > 0 Then
                    sRegNo = alList.Item(0).ToString.Split("|"c)(0)
                Else
                    sbFormClear(1)
                    Me.txtSearch.Text = ""
                    Return
                End If
            End If

            btnQuery0_Click(Nothing, Nothing, sRegNo)
            If Me.spdList.MaxRows > 0 Then spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, 1))
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub txtBCNO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBCNO.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtBCNO.Text = "" Then Return

        Try
            '검체번호 선택시 처리내용
            If Me.txtBCNO.Text.Length.Equals(11) Then
                ' 바코드에서 직접 입력시

                ' 바코드번호(검체번호)를 표시형 검체번호로 변경
                Dim objCommDBFN As New LISAPP.APP_DB.DbFn
                Me.txtBCNO.Text = objCommDBFN.GetBCPrtToView(Me.txtBCNO.Text.Trim)

            ElseIf Me.txtBCNO.Text.Length.Equals(14) Then
                ' 복수구분 없이 입력
            ElseIf Me.txtBCNO.Text.Length.Equals(15) Then
                ' 복수구분 입력시
            Else
                MsgBox("잘못된 검체번호 입니다.", MsgBoxStyle.Information, Me.Text)
                txtBCNO.Focus()
                Exit Sub
            End If

            Dim dt As DataTable = fnGet_PatInfo_bcno(Me.txtBCNO.Text)

            If dt.Rows.Count > 0 Then
                Dim objPatInfo As New STU_PatInfo_S
                Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

                objPatInfo.ORDDT = dt.Rows(0).Item("orddt").ToString.Trim
                objPatInfo.REGNO = dt.Rows(0).Item("regno").ToString.Trim
                objPatInfo.PATNM = sPatInfo(0).Trim
                objPatInfo.SexAge = dt.Rows(0).Item("sexage").ToString.Trim
                objPatInfo.IDNO = sPatInfo(3).Trim
                objPatInfo.WardRoom = dt.Rows(0).Item("wardroom").ToString.Trim
                objPatInfo.RESDT = dt.Rows(0).Item("resdt").ToString.Trim
                objPatInfo.IOGBN = dt.Rows(0).Item("iogbn").ToString.Trim
                objPatInfo.OWNGBN = dt.Rows(0).Item("owngbn").ToString.Trim

                '환자신상 보기
                sbFormClear(2)
                sbDisplay_patinfo(objPatInfo)

                ' 검사항목 보기
                dt = OCSAPP.OcsLink.SData.fnGet_Ord_TestList_FGS04(objPatInfo.ORDDT.Replace("-", ""), objPatInfo.REGNO, objPatInfo.IOGBN, objPatInfo.OWNGBN, Me.txtBCNO.Text)


                If Me.txtBCNO.Text <> "" Then
                    Dim dr As DataRow()

                    dr = dt.Select("bcno = '" + Me.txtBCNO.Text.Replace("-", "") + "'")

                    dt = Fn.ChangeToDataTable(dr)
                End If
                sbDisplay_test(dt)
            Else
                dt = fnGet_BcNo_TkYn(Me.txtBCNO.Text)

                If dt.Rows.Count > 0 Then
                    MsgBox("취소된 검체번호 입니다.", MsgBoxStyle.Information, Me.Text)
                Else
                    MsgBox("해당하는 검체번호가 없습니다.", MsgBoxStyle.Information, Me.Text)
                End If

                Me.txtBCNO.Text = ""

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub


    Private Sub rdoQryGbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cboIOGbn_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIOGbn.SelectedIndexChanged

        Try
            If Me.cboIOGbn.Text = "입원" Then
                Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList()
                If dt.Rows.Count > 0 Then
                    Me.cboWard.Items.Clear()
                    Me.cboWard.Items.Add("전체" + Space(200) + "|")
                    For intCnt As Integer = 0 To dt.Rows.Count - 1
                        With dt.Rows(intCnt)
                            Me.cboWard.Items.Add(.Item("wardnm").ToString + Space(200) + "|" + .Item("wardno").ToString)
                        End With
                    Next
                End If
                dt.Dispose()
                dt = Nothing
                cboWard.SelectedIndex = 0

                lblWard.Visible = True : cboWard.Visible = True
            Else
                lblWard.Visible = False : cboWard.Visible = False
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 조회
    Private Sub btnQuery0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs, Optional ByVal rsRegNo As String = "") Handles btnQuery.Click

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Me.btnQuery.Enabled = False

            Dim dt As New DataTable
            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)
            Dim sWGrpCd As String = Ctrl.Get_Code(Me.cboWkGrp)
            Dim sTGrpCd As String = Ctrl.Get_Code(Me.cboTGrp)
            Dim sDeptCd As String = ""
            Dim sWardNo As String = ""

            If Me.cboWard.Text.IndexOf("|"c) >= 0 Then sWardNo = Me.cboWard.Text.Split("|"c)(0)

            If sTGrpCd <> "" Then sSlipCd = ""

            If Ctrl.Get_Code(Me.cboSpcFlg) = "0" Then
                dt = OCSAPP.OcsLink.SData.fnGet_NotColl_FGS04(Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), Me.cboIOGbn.Text, sWardNo, sSlipCd, sTGrpCd, rsRegNo)

            ElseIf Ctrl.Get_Code(Me.cboSpcFlg) = "1" Then
                dt = fnGet_NotTk_PatList(Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), Me.cboIOGbn.Text, sWardNo, sSlipCd, sTGrpCd, rsRegNo)

            ElseIf Ctrl.Get_Code(Me.cboSpcFlg) = "2" Then
                dt = fnGet_NotRst_PatList(Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), Me.cboIOGbn.Text, sWardNo, sSlipCd, sWGrpCd, sTGrpCd, rsRegNo)
            ElseIf Ctrl.Get_Code(Me.cboSpcFlg) = "3" Then
                dt = fnGet_Tk_PatList(Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), Me.cboIOGbn.Text, sWardNo, sSlipCd, sWGrpCd, sTGrpCd, rsRegNo)
            End If

            sbFormClear(1)
            If dt.Rows.Count > 0 Then
                sbDiplay_patlist(dt)
            Else
                MsgBox("조회된 데이타가 없습니다.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.btnQuery.Enabled = True

            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub spdList_MouseDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdList.MouseDownEvent
        Dim objSpread As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)
        objSpread.Tag = "1"
    End Sub

    Private Sub spdList_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdList.LeaveCell
        Dim objSpread As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

        If objSpread.Tag.ToString = "1" Then Exit Sub
        spdList_ClickEvent(objSpread, New AxFPSpreadADO._DSpreadEvents_ClickEvent(e.newCol, e.newRow))
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        Try
            If e.row < 1 Then Exit Sub

            Dim objPatInfo As New STU_PatInfo_S
            Dim sCollDt As String = "", sTkDt As String = ""

            ' 환자 기본신상 Set
            With spdList
                .Row = e.row
                .Col = .GetColFromID("orddt") : objPatInfo.ORDDT = .Text.ToString
                .Col = .GetColFromID("regno") : objPatInfo.REGNO = .Text.ToString
                .Col = .GetColFromID("patnm") : objPatInfo.PATNM = .Text.ToString
                .Col = .GetColFromID("sexage") : objPatInfo.SexAge = .Text.ToString
                .Col = .GetColFromID("idno") : objPatInfo.IDNO = .Text.ToString
                .Col = .GetColFromID("deptcd") : objPatInfo.DEPT = .Text.ToString
                .Col = .GetColFromID("wardroom") : objPatInfo.WardRoom = .Text.ToString
                .Col = .GetColFromID("resdt") : objPatInfo.RESDT = .Text.ToString
                .Col = .GetColFromID("iogbn") : objPatInfo.IOGBN = .Text.ToString
                .Col = .GetColFromID("owngbn") : objPatInfo.OWNGBN = .Text.ToString

                .Col = .GetColFromID("colldt") : sCollDt = .Text.ToString
                .Col = .GetColFromID("tkdt") : sTkDt = .Text.ToString

            End With

            sbFormClear(2)
            '환자신상 보기
            sbDisplay_patinfo(objPatInfo)

            ' 검사항목 보기
            Dim dt As New DataTable

            If Ctrl.Get_Code(Me.cboSpcFlg) = "0" Or Me.txtSearch.Text <> "" Then
                dt = OCSAPP.OcsLink.SData.fnGet_Ord_TestList_FGS04(objPatInfo.ORDDT.Replace("-", ""), objPatInfo.REGNO, objPatInfo.IOGBN, objPatInfo.OWNGBN)
            ElseIf Ctrl.Get_Code(Me.cboSpcFlg) = "1" Then
                dt = fnGet_Coll_TestList(sCollDt.Replace("-", ""), objPatInfo.REGNO, objPatInfo.IOGBN, objPatInfo.OWNGBN)
            Else
                dt = fnGet_Tk_TestList(sTkDt.Replace("-", ""), objPatInfo.REGNO, objPatInfo.IOGBN, objPatInfo.OWNGBN)
            End If

            Dim sFilter As String = ""

            If Me.cboSlip.SelectedIndex > 0 Then
                sFilter += IIf(sFilter = "", "", " AND ").ToString + "partslip LIKE '" + Ctrl.Get_Code(cboSlip) + "%'"
            End If

            If cboWkGrp.SelectedIndex > 0 And cboWkGrp.Visible Then
                sFilter += IIf(sFilter = "", "", " AND ").ToString + "wkgrpcd = '" + Ctrl.Get_Code(cboWkGrp) + "'"
            End If

            Dim dr As DataRow()
            dr = dt.Select(sFilter, "orddt, bcno, bcclscd, spccd, exlabyn, tubecd, seqtmi, testcd")
            dt = Fn.ChangeToDataTable(dr)

            sbDisplay_test(dt)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            spdList.Tag = ""

        End Try

    End Sub

    Private Sub tbcDirectQry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcDirectQry.Click
        If Me.tbcDirectQry.SelectedIndex = 0 Then
            Me.txtSearch.Text = ""
            Me.txtBCNO.Focus()
        Else
            Me.txtBCNO.Text = ""
            Me.txtSearch.Focus()
        End If
    End Sub

    Private Sub spdTItemList_TextTipFetch(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles spdOrder.TextTipFetch
        Dim objSpd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)
        Dim sText As String = ""

        e.showTip = False
        If e.row < 1 Then Exit Sub

        With objSpd
            .SetTextTipAppearance("굴림체", 9, False, False, &HDFFFFF&, &H800000)
            e.multiLine = FPSpreadADO.TextTipFetchMultilineConstants.TextTipFetchMultilineMultiple

            Select Case e.col
                Case .GetColFromID("BC구분")
                    .Row = e.row : .Col = e.col
                    If .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then
                        .Row = e.row

                        .Col = .GetColFromID("검체번호")
                        If .Text <> "" Then sText = vbCrLf & " 검체번호: " & .Text.ToString.ToString & " "
                        .Col = .GetColFromID("작업번호")
                        If .Text <> "" Then sText += vbCrLf & " 작업번호: " & .Text.ToString.ToString & " "

                        If sText <> "" Then
                            e.tipText = sText + vbCrLf
                            e.showTip = True
                        End If
                    End If

            End Select

        End With
    End Sub

#End Region

    Private Sub cboTGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTGrp.SelectedIndexChanged
        'COMMON.CommXML.setOneElementXML(msXML, msTGrpFile, "TGRP", cboTGrp.SelectedIndex.ToString)
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbDisplay_TGrp()
        sbDisplay_WkGrp()
        'COMMON.CommXML.setOneElementXML(msXML, msSlipFile, "SLIP", cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged
        'COMMON.CommXML.setOneElementXML(msXML, msWGrPFIle, "WKGRP", cboWkGrp.SelectedIndex.ToString)
    End Sub

    Private Sub mnuRstView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRstView.Click

        Dim objForm As Windows.Forms.Form
        Dim strRegNo As String = "", strOrdDt As String = ""

        With spdPatInfo
            .Row = 1
            .Col = .GetColFromID("regno") : strRegNo = .Text
            .Col = .GetColFromID("orddt") : strOrdDt = .Text
        End With

        objForm = Ctrl.CheckFormObject(Me, mnuRstView.Text)

        '-- 결과조회(일일보고서)
        If objForm Is Nothing Then objForm = New LISV.FGRV01(True)

        sbFormLoadedChk(objForm, mnuRstView.Text)

        With CType(objForm, LISV.FGRV01)
            .Display_Result(strRegNo, strOrdDt.Replace("-", ""), "")
        End With

    End Sub

    Private Sub mnuRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRst.Click
        Dim objForm As Windows.Forms.Form

        Dim strBcNo As String = ""

        With spdOrder
            .Row = .ActiveRow
            .Col = .GetColFromID("검체번호") : strBcNo = .Text.Replace("-", "")
        End With

        If strBcNo = "" Then Return

        If strBcNo.Substring(8, 1) = PRG_CONST.BCCLS_MicorBio.Item(0).ToString.Substring(0, 1) Then
            objForm = Ctrl.CheckFormObject(Me, "검체별 결과저장 및 보고 (M)")
            If objForm Is Nothing Then objForm = New LISM.FGM01()
            sbFormLoadedChk(objForm, "검체별 결과저장 및 보고 (M)")

            CType(objForm, LISM.FGM01).sbDisplay_Data(strBcNo)

        ElseIf strBcNo.Substring(8, 1) = PRG_CONST.BCCLS_BloodBank.Substring(0, 1) Then

            objForm = Ctrl.CheckFormObject(Me, "검체별 결과저장 및 보고(T)")
            If objForm Is Nothing Then objForm = New LISR.FGR02()
            objForm.Text = "검체별 결과저장 및 보고(T)"

            sbFormLoadedChk(objForm, "검체별 결과저장 및 보고(T)")

            With CType(objForm, LISR.FGR02)
                .msTitle = "검체별 결과저장 및 보고(T)"
                .mbBloodBankYN = True
                .sbDisplay_Data(strBcNo)
            End With
        Else
            objForm = Ctrl.CheckFormObject(Me, "검체별 결과저장 및 보고")
            If objForm Is Nothing Then objForm = New LISR.FGR02()

            sbFormLoadedChk(objForm, "검체별 결과저장 및 보고")

            With CType(objForm, LISR.FGR02)
                .sbDisplay_Data(strBcNo)
            End With
        End If


    End Sub

    Private Sub mnuColl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuColl.Click
        'Dim objForm As Windows.Forms.Form
        'Dim strRegNo As String = ""

        'objForm = Ctrl.CheckFormObject(Me, mnuColl.Text)

        'With spdPatInfo
        '    .Row = 1
        '    .Col = .GetColFromID("regno") : strRegNo = .Text
        'End With

        'If objForm Is Nothing Then objForm = New C01.FGC01()

        'sbFormLoadedChk(objForm, mnuColl.Text)

        'CType(objForm, C01.FGC01).sbDisplay_PatList(strRegNo)

    End Sub

    Private Sub cboQryGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQryGbn.SelectedIndexChanged
        If Me.cboQryGbn.SelectedIndex = 0 Then
            sbDisplay_part()
        Else
            sbDisplay_Slip()
        End If

    End Sub

    Private Sub cboSpcFlg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSpcFlg.SelectedIndexChanged

        Try
            sbFormClear(1)

            If Ctrl.Get_Code(Me.cboSpcFlg) = "0" Then
                ' 미채혈 조회
                lblDateTitle.Text = "처방일자"
                With spdList
                    .Col = .GetColFromID("orddt") : .ColHidden = False
                    .Col = .GetColFromID("colldt") : .ColHidden = True
                    .Col = .GetColFromID("tkdt") : .ColHidden = True
                End With

                lblWkGrp.Visible = False : cboWkGrp.Visible = False : If cboWkGrp.Items.Count > 0 Then cboWkGrp.SelectedIndex = 0

            ElseIf Ctrl.Get_Code(Me.cboSpcFlg) = "1" Then
                ' 미접수 조회
                lblDateTitle.Text = "채혈일자"
                With spdList
                    .Col = .GetColFromID("orddt") : .ColHidden = True
                    .Col = .GetColFromID("colldt") : .ColHidden = False
                    .Col = .GetColFromID("tkdt") : .ColHidden = True
                End With

                lblWkGrp.Visible = False : cboWkGrp.Visible = False : If cboWkGrp.Items.Count > 0 Then cboWkGrp.SelectedIndex = 0
            Else
                ' 미보고 조회
                lblDateTitle.Text = "접수일자"
                With spdList
                    .Col = .GetColFromID("orddt") : .ColHidden = True
                    .Col = .GetColFromID("colldt") : .ColHidden = True
                    .Col = .GetColFromID("tkdt") : .ColHidden = False
                End With
                lblWkGrp.Visible = True : cboWkGrp.Visible = True
            End If

            Me.cboIOGbn.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub
End Class
