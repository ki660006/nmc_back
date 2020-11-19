<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR08_S05
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR08_S05))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.pnlbottom = New System.Windows.Forms.Panel()
        Me.btnCancel = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.pnlCenter = New System.Windows.Forms.Panel()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlbottom.SuspendLayout()
        Me.pnlCenter.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(931, 351)
        Me.spdList.TabIndex = 132
        '
        'pnlbottom
        '
        Me.pnlbottom.Controls.Add(Me.btnCancel)
        Me.pnlbottom.Controls.Add(Me.btnExcel)
        Me.pnlbottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbottom.Location = New System.Drawing.Point(0, 358)
        Me.pnlbottom.Name = "pnlbottom"
        Me.pnlbottom.Size = New System.Drawing.Size(934, 28)
        Me.pnlbottom.TabIndex = 165
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnCancel.ColorFillBlend = CBlendItems1
        Me.btnCancel.ColorFillSolid = System.Drawing.Color.White
        Me.btnCancel.Corners.All = CType(6, Short)
        Me.btnCancel.Corners.LowerLeft = CType(6, Short)
        Me.btnCancel.Corners.LowerRight = CType(6, Short)
        Me.btnCancel.Corners.UpperLeft = CType(6, Short)
        Me.btnCancel.Corners.UpperRight = CType(6, Short)
        Me.btnCancel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnCancel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnCancel.FocalPoints.CenterPtX = 0.4678899!
        Me.btnCancel.FocalPoints.CenterPtY = 0.5652174!
        Me.btnCancel.FocalPoints.FocusPtX = 0.0!
        Me.btnCancel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.FocusPtTracker = DesignerRectTracker2
        Me.btnCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Image = Nothing
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnCancel.Location = New System.Drawing.Point(821, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnCancel.SideImage = Nothing
        Me.btnCancel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnCancel.Size = New System.Drawing.Size(109, 23)
        Me.btnCancel.TabIndex = 197
        Me.btnCancel.Text = "결과이력 삭제"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnCancel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems2
        Me.btnExcel.ColorFillSolid = System.Drawing.Color.White
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.4678899!
        Me.btnExcel.FocalPoints.CenterPtY = 0.3913043!
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
        Me.btnExcel.Location = New System.Drawing.Point(711, 3)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(109, 23)
        Me.btnExcel.TabIndex = 196
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'pnlCenter
        '
        Me.pnlCenter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCenter.Controls.Add(Me.spdList)
        Me.pnlCenter.Location = New System.Drawing.Point(0, 1)
        Me.pnlCenter.Name = "pnlCenter"
        Me.pnlCenter.Size = New System.Drawing.Size(931, 351)
        Me.pnlCenter.TabIndex = 166
        '
        'FGR08_S05
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(934, 386)
        Me.Controls.Add(Me.pnlCenter)
        Me.Controls.Add(Me.pnlbottom)
        Me.Name = "FGR08_S05"
        Me.Text = "신종코로나바이러스 I/F 전송 과거력"
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlbottom.ResumeLayout(False)
        Me.pnlCenter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlbottom As System.Windows.Forms.Panel
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents pnlCenter As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As CButtonLib.CButton
End Class
