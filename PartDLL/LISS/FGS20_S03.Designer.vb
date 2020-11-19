<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS20_S03
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
        Me.components = New System.ComponentModel.Container()
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS20_S03))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.CButton1 = New CButtonLib.CButton()
        Me.btnOk = New CButtonLib.CButton()
        Me.lblRstDT = New System.Windows.Forms.Label()
        Me.cboGroup = New System.Windows.Forms.ComboBox()
        Me.cboRef = New System.Windows.Forms.ComboBox()
        Me.txtRef = New System.Windows.Forms.TextBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnClear = New CButtonLib.CButton()
        Me.SuspendLayout()
        '
        'CButton1
        '
        Me.CButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton1.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.CButton1.ColorFillBlend = CBlendItems1
        Me.CButton1.ColorFillSolid = System.Drawing.Color.White
        Me.CButton1.Corners.All = CType(6, Short)
        Me.CButton1.Corners.LowerLeft = CType(6, Short)
        Me.CButton1.Corners.LowerRight = CType(6, Short)
        Me.CButton1.Corners.UpperLeft = CType(6, Short)
        Me.CButton1.Corners.UpperRight = CType(6, Short)
        Me.CButton1.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.CButton1.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.CButton1.FocalPoints.CenterPtX = 0.4859813!
        Me.CButton1.FocalPoints.CenterPtY = 0.16!
        Me.CButton1.FocalPoints.FocusPtX = 0.0!
        Me.CButton1.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton1.FocusPtTracker = DesignerRectTracker2
        Me.CButton1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.CButton1.ForeColor = System.Drawing.Color.White
        Me.CButton1.Image = Nothing
        Me.CButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton1.ImageIndex = 0
        Me.CButton1.ImageSize = New System.Drawing.Size(16, 16)
        Me.CButton1.Location = New System.Drawing.Point(310, 76)
        Me.CButton1.Name = "CButton1"
        Me.CButton1.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.CButton1.SideImage = Nothing
        Me.CButton1.SideImageSize = New System.Drawing.Size(48, 48)
        Me.CButton1.Size = New System.Drawing.Size(100, 25)
        Me.CButton1.TabIndex = 216
        Me.CButton1.Text = "취 소"
        Me.CButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.CButton1.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOk.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnOk.ColorFillBlend = CBlendItems2
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
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOk.FocusPtTracker = DesignerRectTracker4
        Me.btnOk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Image = Nothing
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOk.ImageIndex = 0
        Me.btnOk.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnOk.Location = New System.Drawing.Point(209, 76)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnOk.SideImage = Nothing
        Me.btnOk.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnOk.Size = New System.Drawing.Size(100, 25)
        Me.btnOk.TabIndex = 215
        Me.btnOk.Text = "선 택"
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnOk.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblRstDT
        '
        Me.lblRstDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstDT.ForeColor = System.Drawing.Color.White
        Me.lblRstDT.Location = New System.Drawing.Point(3, 4)
        Me.lblRstDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRstDT.Name = "lblRstDT"
        Me.lblRstDT.Size = New System.Drawing.Size(212, 21)
        Me.lblRstDT.TabIndex = 214
        Me.lblRstDT.Text = "감염병 병원체"
        Me.lblRstDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboGroup
        '
        Me.cboGroup.FormattingEnabled = True
        Me.cboGroup.Items.AddRange(New Object() {"선택하세요", "1급", "2급", "3급"})
        Me.cboGroup.Location = New System.Drawing.Point(3, 28)
        Me.cboGroup.Name = "cboGroup"
        Me.cboGroup.Size = New System.Drawing.Size(121, 20)
        Me.cboGroup.TabIndex = 217
        '
        'cboRef
        '
        Me.cboRef.FormattingEnabled = True
        Me.cboRef.Location = New System.Drawing.Point(126, 28)
        Me.cboRef.Name = "cboRef"
        Me.cboRef.Size = New System.Drawing.Size(284, 20)
        Me.cboRef.TabIndex = 218
        '
        'txtRef
        '
        Me.txtRef.Location = New System.Drawing.Point(3, 52)
        Me.txtRef.Name = "txtRef"
        Me.txtRef.Size = New System.Drawing.Size(343, 21)
        Me.txtRef.TabIndex = 219
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(346, 51)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(64, 23)
        Me.btnAdd.TabIndex = 220
        Me.btnAdd.Text = "추가"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.Color.White
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0.5!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(3, 76)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 221
        Me.btnClear.Text = "CLEAR"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGS20_S03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(418, 106)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtRef)
        Me.Controls.Add(Me.cboRef)
        Me.Controls.Add(Me.cboGroup)
        Me.Controls.Add(Me.CButton1)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.lblRstDT)
        Me.Name = "FGS20_S03"
        Me.Text = "감염병 병원체"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CButton1 As CButtonLib.CButton
    Friend WithEvents btnOk As CButtonLib.CButton
    Friend WithEvents lblRstDT As System.Windows.Forms.Label
    Friend WithEvents cboGroup As System.Windows.Forms.ComboBox
    Friend WithEvents cboRef As System.Windows.Forms.ComboBox
    Friend WithEvents txtRef As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnClear As CButtonLib.CButton
End Class
