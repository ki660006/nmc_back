<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS20_S05
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS20_S05))
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.lblRstDT = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.spdBacgen = New AxFPSpreadADO.AxfpSpread
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnClose = New CButtonLib.CButton
        Me.btnOk = New CButtonLib.CButton
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnExcute = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.spdAnti = New AxFPSpreadADO.AxfpSpread
        Me.txtFilter = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdBacgen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.spdAnti, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblRstDT
        '
        Me.lblRstDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstDT.ForeColor = System.Drawing.Color.White
        Me.lblRstDT.Location = New System.Drawing.Point(6, 3)
        Me.lblRstDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRstDT.Name = "lblRstDT"
        Me.lblRstDT.Size = New System.Drawing.Size(132, 21)
        Me.lblRstDT.TabIndex = 11
        Me.lblRstDT.Text = "항균제 필터 설정"
        Me.lblRstDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.spdBacgen)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(379, 382)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "균속"
        '
        'spdBacgen
        '
        Me.spdBacgen.DataSource = Nothing
        Me.spdBacgen.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdBacgen.Location = New System.Drawing.Point(3, 17)
        Me.spdBacgen.Name = "spdBacgen"
        Me.spdBacgen.OcxState = CType(resources.GetObject("spdBacgen.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdBacgen.Size = New System.Drawing.Size(373, 362)
        Me.spdBacgen.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 489)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(878, 41)
        Me.Panel1.TabIndex = 14
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClose.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClose.ColorFillBlend = CBlendItems2
        Me.btnClose.ColorFillSolid = System.Drawing.Color.White
        Me.btnClose.Corners.All = CType(6, Short)
        Me.btnClose.Corners.LowerLeft = CType(6, Short)
        Me.btnClose.Corners.LowerRight = CType(6, Short)
        Me.btnClose.Corners.UpperLeft = CType(6, Short)
        Me.btnClose.Corners.UpperRight = CType(6, Short)
        Me.btnClose.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClose.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClose.FocalPoints.CenterPtX = 0.4859813!
        Me.btnClose.FocalPoints.CenterPtY = 0.16!
        Me.btnClose.FocalPoints.FocusPtX = 0.0!
        Me.btnClose.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClose.FocusPtTracker = DesignerRectTracker4
        Me.btnClose.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Image = Nothing
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.ImageIndex = 0
        Me.btnClose.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClose.Location = New System.Drawing.Point(776, 14)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClose.SideImage = Nothing
        Me.btnClose.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClose.Size = New System.Drawing.Size(100, 25)
        Me.btnClose.TabIndex = 218
        Me.btnClose.Text = "취 소"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClose.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOk.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnOk.ColorFillBlend = CBlendItems3
        Me.btnOk.ColorFillSolid = System.Drawing.Color.White
        Me.btnOk.Corners.All = CType(6, Short)
        Me.btnOk.Corners.LowerLeft = CType(6, Short)
        Me.btnOk.Corners.LowerRight = CType(6, Short)
        Me.btnOk.Corners.UpperLeft = CType(6, Short)
        Me.btnOk.Corners.UpperRight = CType(6, Short)
        Me.btnOk.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnOk.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnOk.FocalPoints.CenterPtX = 0.4859813!
        Me.btnOk.FocalPoints.CenterPtY = 0.16!
        Me.btnOk.FocalPoints.FocusPtX = 0.0!
        Me.btnOk.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOk.FocusPtTracker = DesignerRectTracker6
        Me.btnOk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Image = Nothing
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOk.ImageIndex = 0
        Me.btnOk.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnOk.Location = New System.Drawing.Point(675, 14)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnOk.SideImage = Nothing
        Me.btnOk.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOk.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnOk.Size = New System.Drawing.Size(100, 25)
        Me.btnOk.TabIndex = 217
        Me.btnOk.Text = "선 택"
        Me.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnOk.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(9, 411)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(106, 21)
        Me.Label12.TabIndex = 29
        Me.Label12.Text = "필터 값 "
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExcute
        '
        Me.btnExcute.Location = New System.Drawing.Point(730, 408)
        Me.btnExcute.Name = "btnExcute"
        Me.btnExcute.Size = New System.Drawing.Size(145, 24)
        Me.btnExcute.TabIndex = 30
        Me.btnExcute.Text = "▼ 적 용 "
        Me.btnExcute.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.spdAnti)
        Me.GroupBox2.Location = New System.Drawing.Point(388, 28)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(487, 379)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "배양균속별 항균제 "
        '
        'spdAnti
        '
        Me.spdAnti.DataSource = Nothing
        Me.spdAnti.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdAnti.Location = New System.Drawing.Point(3, 17)
        Me.spdAnti.Name = "spdAnti"
        Me.spdAnti.OcxState = CType(resources.GetObject("spdAnti.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdAnti.Size = New System.Drawing.Size(481, 359)
        Me.spdAnti.TabIndex = 0
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(8, 435)
        Me.txtFilter.Multiline = True
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(867, 52)
        Me.txtFilter.TabIndex = 34
        '
        'FGS20_S05
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(878, 530)
        Me.Controls.Add(Me.txtFilter)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnExcute)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblRstDT)
        Me.Name = "FGS20_S05"
        Me.Text = "FGS20_S05"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdBacgen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.spdAnti, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblRstDT As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdBacgen As AxFPSpreadADO.AxfpSpread

    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As CButtonLib.CButton
    Friend WithEvents btnOk As CButtonLib.CButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnExcute As System.Windows.Forms.Button

    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents spdAnti As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
End Class
