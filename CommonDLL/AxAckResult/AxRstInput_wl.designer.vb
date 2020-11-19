<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxRstInput_wl
    Inherits System.Windows.Forms.UserControl

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxRstInput_wl))
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
        Me.spdResult = New AxFPSpreadADO.AxfpSpread
        Me.cmuLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuSpRst = New System.Windows.Forms.ToolStripMenuItem
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstCode = New System.Windows.Forms.ListBox
        Me.axCalcRst = New AxAckCalcResult.AxCalcResult
        Me.chkSelect = New System.Windows.Forms.CheckBox
        Me.pnlCode = New System.Windows.Forms.Panel
        Me.txtOrgRst = New System.Windows.Forms.TextBox
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.btnReg_Abn = New CButtonLib.CButton
        Me.btnReg_UnFit = New CButtonLib.CButton
        Me.btnQryFNModify = New CButtonLib.CButton
        Me.btnHistory = New CButtonLib.CButton
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.btnReg_tat = New CButtonLib.CButton
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblCfm = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblReg = New System.Windows.Forms.Label
        Me.lblSampleStatus = New System.Windows.Forms.Label
        Me.lblMW = New System.Windows.Forms.Label
        Me.lblFN = New System.Windows.Forms.Label
        Me.lstEx = New System.Windows.Forms.ListBox
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmuLink.SuspendLayout()
        Me.pnlCode.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdResult
        '
        Me.spdResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdResult.ContextMenuStrip = Me.cmuLink
        Me.spdResult.DataSource = Nothing
        Me.spdResult.Location = New System.Drawing.Point(0, 22)
        Me.spdResult.Name = "spdResult"
        Me.spdResult.OcxState = CType(resources.GetObject("spdResult.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdResult.Size = New System.Drawing.Size(941, 407)
        Me.spdResult.TabIndex = 0
        '
        'cmuLink
        '
        Me.cmuLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSpRst})
        Me.cmuLink.Name = "cmuRstList"
        Me.cmuLink.Size = New System.Drawing.Size(153, 26)
        Me.cmuLink.Text = "상황에 맞는 메뉴"
        '
        'mnuSpRst
        '
        Me.mnuSpRst.Name = "mnuSpRst"
        Me.mnuSpRst.Size = New System.Drawing.Size(152, 22)
        Me.mnuSpRst.Text = "특수결과 입력"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(76, 469)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 26)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = " △ 검사"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(145, 469)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 26)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = " ○ Review"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label2.Location = New System.Drawing.Point(219, 469)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 26)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = " ◆ 완료"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(0, 469)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 26)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "결과범례"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstCode
        '
        Me.lstCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstCode.ItemHeight = 12
        Me.lstCode.Location = New System.Drawing.Point(3, 0)
        Me.lstCode.Name = "lstCode"
        Me.lstCode.Size = New System.Drawing.Size(298, 460)
        Me.lstCode.TabIndex = 167
        Me.lstCode.Visible = False
        '
        'axCalcRst
        '
        Me.axCalcRst.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axCalcRst.Location = New System.Drawing.Point(333, 0)
        Me.axCalcRst.Name = "axCalcRst"
        Me.axCalcRst.Size = New System.Drawing.Size(113, 22)
        Me.axCalcRst.TabIndex = 166
        Me.axCalcRst.Visible = False
        '
        'chkSelect
        '
        Me.chkSelect.AutoSize = True
        Me.chkSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkSelect.Location = New System.Drawing.Point(30, 27)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(15, 14)
        Me.chkSelect.TabIndex = 168
        Me.chkSelect.UseVisualStyleBackColor = False
        '
        'pnlCode
        '
        Me.pnlCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCode.Controls.Add(Me.lstCode)
        Me.pnlCode.Location = New System.Drawing.Point(642, 22)
        Me.pnlCode.Name = "pnlCode"
        Me.pnlCode.Size = New System.Drawing.Size(301, 469)
        Me.pnlCode.TabIndex = 169
        Me.pnlCode.Visible = False
        '
        'txtOrgRst
        '
        Me.txtOrgRst.Location = New System.Drawing.Point(124, 1)
        Me.txtOrgRst.Name = "txtOrgRst"
        Me.txtOrgRst.Size = New System.Drawing.Size(36, 21)
        Me.txtOrgRst.TabIndex = 172
        Me.txtOrgRst.Visible = False
        '
        'txtTestCd
        '
        Me.txtTestCd.Location = New System.Drawing.Point(166, 1)
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(33, 21)
        Me.txtTestCd.TabIndex = 174
        Me.txtTestCd.Visible = False
        '
        'btnReg_Abn
        '
        Me.btnReg_Abn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg_Abn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReg_Abn.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_Abn.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnReg_Abn.ColorFillBlend = CBlendItems1
        Me.btnReg_Abn.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg_Abn.Corners.All = CType(6, Short)
        Me.btnReg_Abn.Corners.LowerLeft = CType(6, Short)
        Me.btnReg_Abn.Corners.LowerRight = CType(6, Short)
        Me.btnReg_Abn.Corners.UpperLeft = CType(6, Short)
        Me.btnReg_Abn.Corners.UpperRight = CType(6, Short)
        Me.btnReg_Abn.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg_Abn.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg_Abn.FocalPoints.CenterPtX = 0.9714286!
        Me.btnReg_Abn.FocalPoints.CenterPtY = 0.1363636!
        Me.btnReg_Abn.FocalPoints.FocusPtX = 0.0!
        Me.btnReg_Abn.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_Abn.FocusPtTracker = DesignerRectTracker2
        Me.btnReg_Abn.Image = Nothing
        Me.btnReg_Abn.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_Abn.ImageIndex = 0
        Me.btnReg_Abn.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg_Abn.Location = New System.Drawing.Point(841, 0)
        Me.btnReg_Abn.Name = "btnReg_Abn"
        Me.btnReg_Abn.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg_Abn.SideImage = Nothing
        Me.btnReg_Abn.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg_Abn.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg_Abn.Size = New System.Drawing.Size(99, 22)
        Me.btnReg_Abn.TabIndex = 179
        Me.btnReg_Abn.Text = "특이결과 등록"
        Me.btnReg_Abn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_Abn.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg_Abn.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg_UnFit
        '
        Me.btnReg_UnFit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg_UnFit.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReg_UnFit.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_UnFit.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems2.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnReg_UnFit.ColorFillBlend = CBlendItems2
        Me.btnReg_UnFit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg_UnFit.Corners.All = CType(6, Short)
        Me.btnReg_UnFit.Corners.LowerLeft = CType(6, Short)
        Me.btnReg_UnFit.Corners.LowerRight = CType(6, Short)
        Me.btnReg_UnFit.Corners.UpperLeft = CType(6, Short)
        Me.btnReg_UnFit.Corners.UpperRight = CType(6, Short)
        Me.btnReg_UnFit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg_UnFit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg_UnFit.FocalPoints.CenterPtX = 1.0!
        Me.btnReg_UnFit.FocalPoints.CenterPtY = 0.0!
        Me.btnReg_UnFit.FocalPoints.FocusPtX = 0.0!
        Me.btnReg_UnFit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_UnFit.FocusPtTracker = DesignerRectTracker4
        Me.btnReg_UnFit.Image = Nothing
        Me.btnReg_UnFit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_UnFit.ImageIndex = 0
        Me.btnReg_UnFit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg_UnFit.Location = New System.Drawing.Point(726, 0)
        Me.btnReg_UnFit.Name = "btnReg_UnFit"
        Me.btnReg_UnFit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg_UnFit.SideImage = Nothing
        Me.btnReg_UnFit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg_UnFit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg_UnFit.Size = New System.Drawing.Size(115, 22)
        Me.btnReg_UnFit.TabIndex = 180
        Me.btnReg_UnFit.Text = "부적합 검체 등록"
        Me.btnReg_UnFit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_UnFit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg_UnFit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQryFNModify
        '
        Me.btnQryFNModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQryFNModify.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQryFNModify.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQryFNModify.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems3.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQryFNModify.ColorFillBlend = CBlendItems3
        Me.btnQryFNModify.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQryFNModify.Corners.All = CType(6, Short)
        Me.btnQryFNModify.Corners.LowerLeft = CType(6, Short)
        Me.btnQryFNModify.Corners.LowerRight = CType(6, Short)
        Me.btnQryFNModify.Corners.UpperLeft = CType(6, Short)
        Me.btnQryFNModify.Corners.UpperRight = CType(6, Short)
        Me.btnQryFNModify.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQryFNModify.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQryFNModify.FocalPoints.CenterPtX = 1.0!
        Me.btnQryFNModify.FocalPoints.CenterPtY = 0.0!
        Me.btnQryFNModify.FocalPoints.FocusPtX = 0.0!
        Me.btnQryFNModify.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQryFNModify.FocusPtTracker = DesignerRectTracker6
        Me.btnQryFNModify.Image = Nothing
        Me.btnQryFNModify.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQryFNModify.ImageIndex = 0
        Me.btnQryFNModify.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQryFNModify.Location = New System.Drawing.Point(538, 0)
        Me.btnQryFNModify.Name = "btnQryFNModify"
        Me.btnQryFNModify.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQryFNModify.SideImage = Nothing
        Me.btnQryFNModify.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQryFNModify.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQryFNModify.Size = New System.Drawing.Size(91, 22)
        Me.btnQryFNModify.TabIndex = 181
        Me.btnQryFNModify.Text = "수정사유조회"
        Me.btnQryFNModify.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQryFNModify.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQryFNModify.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnHistory.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHistory.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems4.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnHistory.ColorFillBlend = CBlendItems4
        Me.btnHistory.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnHistory.Corners.All = CType(6, Short)
        Me.btnHistory.Corners.LowerLeft = CType(6, Short)
        Me.btnHistory.Corners.LowerRight = CType(6, Short)
        Me.btnHistory.Corners.UpperLeft = CType(6, Short)
        Me.btnHistory.Corners.UpperRight = CType(6, Short)
        Me.btnHistory.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnHistory.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnHistory.FocalPoints.CenterPtX = 1.0!
        Me.btnHistory.FocalPoints.CenterPtY = 0.0!
        Me.btnHistory.FocalPoints.FocusPtX = 0.0!
        Me.btnHistory.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHistory.FocusPtTracker = DesignerRectTracker8
        Me.btnHistory.Image = Nothing
        Me.btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnHistory.ImageIndex = 0
        Me.btnHistory.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnHistory.Location = New System.Drawing.Point(447, 0)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnHistory.SideImage = Nothing
        Me.btnHistory.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnHistory.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnHistory.Size = New System.Drawing.Size(91, 22)
        Me.btnHistory.TabIndex = 182
        Me.btnHistory.Text = "누적결과조회"
        Me.btnHistory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnHistory.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnHistory.Visible = False
        '
        'txtBcNo
        '
        Me.txtBcNo.Location = New System.Drawing.Point(205, 0)
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(122, 21)
        Me.txtBcNo.TabIndex = 189
        Me.txtBcNo.Visible = False
        '
        'btnReg_tat
        '
        Me.btnReg_tat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg_tat.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnReg_tat.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_tat.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems5.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnReg_tat.ColorFillBlend = CBlendItems5
        Me.btnReg_tat.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg_tat.Corners.All = CType(6, Short)
        Me.btnReg_tat.Corners.LowerLeft = CType(6, Short)
        Me.btnReg_tat.Corners.LowerRight = CType(6, Short)
        Me.btnReg_tat.Corners.UpperLeft = CType(6, Short)
        Me.btnReg_tat.Corners.UpperRight = CType(6, Short)
        Me.btnReg_tat.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg_tat.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg_tat.FocalPoints.CenterPtX = 1.0!
        Me.btnReg_tat.FocalPoints.CenterPtY = 0.0!
        Me.btnReg_tat.FocalPoints.FocusPtX = 0.0!
        Me.btnReg_tat.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = True
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_tat.FocusPtTracker = DesignerRectTracker10
        Me.btnReg_tat.Image = Nothing
        Me.btnReg_tat.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_tat.ImageIndex = 0
        Me.btnReg_tat.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg_tat.Location = New System.Drawing.Point(629, 0)
        Me.btnReg_tat.Name = "btnReg_tat"
        Me.btnReg_tat.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg_tat.SideImage = Nothing
        Me.btnReg_tat.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg_tat.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg_tat.Size = New System.Drawing.Size(97, 22)
        Me.btnReg_tat.TabIndex = 191
        Me.btnReg_tat.Text = "TAT 사유 등록"
        Me.btnReg_tat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_tat.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg_tat.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(758, 432)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 35)
        Me.Label5.TabIndex = 201
        Me.Label5.Text = "결과 확인의"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCfm
        '
        Me.lblCfm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCfm.BackColor = System.Drawing.Color.White
        Me.lblCfm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCfm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCfm.Location = New System.Drawing.Point(836, 431)
        Me.lblCfm.Name = "lblCfm"
        Me.lblCfm.Size = New System.Drawing.Size(104, 35)
        Me.lblCfm.TabIndex = 200
        Me.lblCfm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(575, 433)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 35)
        Me.Label11.TabIndex = 199
        Me.Label11.Text = "최종 보고자"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(391, 433)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 35)
        Me.Label10.TabIndex = 198
        Me.Label10.Text = "중간 보고자"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(209, 433)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 35)
        Me.Label8.TabIndex = 197
        Me.Label8.Text = "결과 입력자"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(0, 433)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 35)
        Me.Label7.TabIndex = 196
        Me.Label7.Text = "결과상태"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReg
        '
        Me.lblReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReg.BackColor = System.Drawing.Color.White
        Me.lblReg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblReg.Location = New System.Drawing.Point(285, 433)
        Me.lblReg.Name = "lblReg"
        Me.lblReg.Size = New System.Drawing.Size(104, 35)
        Me.lblReg.TabIndex = 194
        Me.lblReg.Text = "0000-00-00 00:00"
        Me.lblReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSampleStatus
        '
        Me.lblSampleStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSampleStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.lblSampleStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSampleStatus.ForeColor = System.Drawing.Color.Black
        Me.lblSampleStatus.Location = New System.Drawing.Point(76, 433)
        Me.lblSampleStatus.Name = "lblSampleStatus"
        Me.lblSampleStatus.Size = New System.Drawing.Size(133, 35)
        Me.lblSampleStatus.TabIndex = 193
        Me.lblSampleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMW
        '
        Me.lblMW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMW.BackColor = System.Drawing.Color.White
        Me.lblMW.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMW.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMW.Location = New System.Drawing.Point(467, 433)
        Me.lblMW.Name = "lblMW"
        Me.lblMW.Size = New System.Drawing.Size(104, 35)
        Me.lblMW.TabIndex = 192
        Me.lblMW.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFN
        '
        Me.lblFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFN.BackColor = System.Drawing.Color.White
        Me.lblFN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFN.Location = New System.Drawing.Point(651, 432)
        Me.lblFN.Name = "lblFN"
        Me.lblFN.Size = New System.Drawing.Size(104, 35)
        Me.lblFN.TabIndex = 195
        Me.lblFN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstEx
        '
        Me.lstEx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstEx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstEx.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lstEx.ItemHeight = 12
        Me.lstEx.Location = New System.Drawing.Point(289, 469)
        Me.lstEx.Name = "lstEx"
        Me.lstEx.Size = New System.Drawing.Size(652, 26)
        Me.lstEx.TabIndex = 202
        '
        'AxRstInput_wl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ContextMenuStrip = Me.cmuLink
        Me.Controls.Add(Me.pnlCode)
        Me.Controls.Add(Me.lstEx)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblCfm)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblReg)
        Me.Controls.Add(Me.lblSampleStatus)
        Me.Controls.Add(Me.lblMW)
        Me.Controls.Add(Me.lblFN)
        Me.Controls.Add(Me.btnReg_tat)
        Me.Controls.Add(Me.txtBcNo)
        Me.Controls.Add(Me.btnHistory)
        Me.Controls.Add(Me.btnReg_UnFit)
        Me.Controls.Add(Me.txtTestCd)
        Me.Controls.Add(Me.txtOrgRst)
        Me.Controls.Add(Me.chkSelect)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.axCalcRst)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnQryFNModify)
        Me.Controls.Add(Me.btnReg_Abn)
        Me.Controls.Add(Me.spdResult)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "AxRstInput_wl"
        Me.Size = New System.Drawing.Size(941, 495)
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmuLink.ResumeLayout(False)
        Me.pnlCode.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents spdResult As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents axCalcRst As AxAckCalcResult.AxCalcResult
    Friend WithEvents lstCode As System.Windows.Forms.ListBox
    Friend WithEvents chkSelect As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCode As System.Windows.Forms.Panel
    Friend WithEvents txtOrgRst As System.Windows.Forms.TextBox
    Friend WithEvents cmuLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuSpRst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents btnReg_Abn As CButtonLib.CButton
    Friend WithEvents btnReg_UnFit As CButtonLib.CButton
    Friend WithEvents btnQryFNModify As CButtonLib.CButton
    Friend WithEvents btnHistory As CButtonLib.CButton
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents btnReg_tat As CButtonLib.CButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblCfm As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblReg As System.Windows.Forms.Label
    Friend WithEvents lblSampleStatus As System.Windows.Forms.Label
    Friend WithEvents lblMW As System.Windows.Forms.Label
    Friend WithEvents lblFN As System.Windows.Forms.Label
    Friend WithEvents lstEx As System.Windows.Forms.ListBox

End Class
