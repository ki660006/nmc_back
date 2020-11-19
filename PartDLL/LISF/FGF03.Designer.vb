<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGF03
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
        Me.components = New System.ComponentModel.Container
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGF03))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.txtCd = New System.Windows.Forms.TextBox
        Me.lblCd = New System.Windows.Forms.Label
        Me.txtNm = New System.Windows.Forms.TextBox
        Me.lblNm = New System.Windows.Forms.Label
        Me.txtUseDt = New System.Windows.Forms.TextBox
        Me.lblUseDt = New System.Windows.Forms.Label
        Me.lblUseDtA = New System.Windows.Forms.Label
        Me.lblArrow = New System.Windows.Forms.Label
        Me.txtUseDtA = New System.Windows.Forms.MaskedTextBox
        Me.lblBefore = New System.Windows.Forms.Label
        Me.lblAfter = New System.Windows.Forms.Label
        Me.btnDelCd = New CButtonLib.CButton
        Me.btnEditUseDt = New CButtonLib.CButton
        Me.btnClose = New CButtonLib.CButton
        Me.SuspendLayout()
        '
        'txtCd
        '
        Me.txtCd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCd.Location = New System.Drawing.Point(74, 9)
        Me.txtCd.Name = "txtCd"
        Me.txtCd.ReadOnly = True
        Me.txtCd.Size = New System.Drawing.Size(100, 21)
        Me.txtCd.TabIndex = 4
        Me.txtCd.TabStop = False
        Me.txtCd.Tag = ""
        '
        'lblCd
        '
        Me.lblCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCd.ForeColor = System.Drawing.Color.Black
        Me.lblCd.Location = New System.Drawing.Point(10, 9)
        Me.lblCd.Name = "lblCd"
        Me.lblCd.Size = New System.Drawing.Size(63, 21)
        Me.lblCd.TabIndex = 3
        Me.lblCd.Tag = ""
        Me.lblCd.Text = "코드"
        Me.lblCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNm
        '
        Me.txtNm.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtNm.Location = New System.Drawing.Point(74, 32)
        Me.txtNm.Name = "txtNm"
        Me.txtNm.ReadOnly = True
        Me.txtNm.Size = New System.Drawing.Size(397, 21)
        Me.txtNm.TabIndex = 6
        Me.txtNm.TabStop = False
        Me.txtNm.Tag = ""
        '
        'lblNm
        '
        Me.lblNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblNm.ForeColor = System.Drawing.Color.Black
        Me.lblNm.Location = New System.Drawing.Point(10, 32)
        Me.lblNm.Name = "lblNm"
        Me.lblNm.Size = New System.Drawing.Size(63, 21)
        Me.lblNm.TabIndex = 5
        Me.lblNm.Tag = ""
        Me.lblNm.Text = "명칭"
        Me.lblNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUseDt
        '
        Me.txtUseDt.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtUseDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUseDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtUseDt.Location = New System.Drawing.Point(74, 83)
        Me.txtUseDt.Name = "txtUseDt"
        Me.txtUseDt.ReadOnly = True
        Me.txtUseDt.Size = New System.Drawing.Size(121, 21)
        Me.txtUseDt.TabIndex = 8
        Me.txtUseDt.TabStop = False
        Me.txtUseDt.Tag = ""
        Me.txtUseDt.Text = "2000-01-01 00:00:00"
        '
        'lblUseDt
        '
        Me.lblUseDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUseDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUseDt.ForeColor = System.Drawing.Color.Black
        Me.lblUseDt.Location = New System.Drawing.Point(10, 83)
        Me.lblUseDt.Name = "lblUseDt"
        Me.lblUseDt.Size = New System.Drawing.Size(63, 21)
        Me.lblUseDt.TabIndex = 7
        Me.lblUseDt.Tag = ""
        Me.lblUseDt.Text = "사용일시"
        Me.lblUseDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUseDtA
        '
        Me.lblUseDtA.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUseDtA.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUseDtA.ForeColor = System.Drawing.Color.White
        Me.lblUseDtA.Location = New System.Drawing.Point(286, 84)
        Me.lblUseDtA.Name = "lblUseDtA"
        Me.lblUseDtA.Size = New System.Drawing.Size(63, 21)
        Me.lblUseDtA.TabIndex = 9
        Me.lblUseDtA.Tag = ""
        Me.lblUseDtA.Text = "사용일시"
        Me.lblUseDtA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblArrow
        '
        Me.lblArrow.AutoSize = True
        Me.lblArrow.Font = New System.Drawing.Font("굴림", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblArrow.ForeColor = System.Drawing.Color.Crimson
        Me.lblArrow.Location = New System.Drawing.Point(218, 77)
        Me.lblArrow.Name = "lblArrow"
        Me.lblArrow.Size = New System.Drawing.Size(48, 32)
        Me.lblArrow.TabIndex = 11
        Me.lblArrow.Text = "→"
        '
        'txtUseDtA
        '
        Me.txtUseDtA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUseDtA.Location = New System.Drawing.Point(350, 84)
        Me.txtUseDtA.Mask = "0000-00-00 00:00:00"
        Me.txtUseDtA.Name = "txtUseDtA"
        Me.txtUseDtA.Size = New System.Drawing.Size(121, 21)
        Me.txtUseDtA.TabIndex = 12
        Me.txtUseDtA.Text = "20000101123456"
        '
        'lblBefore
        '
        Me.lblBefore.Location = New System.Drawing.Point(10, 69)
        Me.lblBefore.Name = "lblBefore"
        Me.lblBefore.Size = New System.Drawing.Size(47, 12)
        Me.lblBefore.TabIndex = 13
        Me.lblBefore.Text = "변경 전"
        Me.lblBefore.UseCompatibleTextRendering = True
        '
        'lblAfter
        '
        Me.lblAfter.Location = New System.Drawing.Point(286, 69)
        Me.lblAfter.Name = "lblAfter"
        Me.lblAfter.Size = New System.Drawing.Size(47, 12)
        Me.lblAfter.TabIndex = 14
        Me.lblAfter.Text = "변경 후"
        Me.lblAfter.UseCompatibleTextRendering = True
        '
        'btnDelCd
        '
        Me.btnDelCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDelCd.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnDelCd.ColorFillBlend = CBlendItems1
        Me.btnDelCd.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnDelCd.Corners.All = CType(6, Short)
        Me.btnDelCd.Corners.LowerLeft = CType(6, Short)
        Me.btnDelCd.Corners.LowerRight = CType(6, Short)
        Me.btnDelCd.Corners.UpperLeft = CType(6, Short)
        Me.btnDelCd.Corners.UpperRight = CType(6, Short)
        Me.btnDelCd.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnDelCd.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnDelCd.FocalPoints.CenterPtX = 0.4639175!
        Me.btnDelCd.FocalPoints.CenterPtY = 0.16!
        Me.btnDelCd.FocalPoints.FocusPtX = 0.02061856!
        Me.btnDelCd.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDelCd.FocusPtTracker = DesignerRectTracker2
        Me.btnDelCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDelCd.ForeColor = System.Drawing.Color.White
        Me.btnDelCd.Image = Nothing
        Me.btnDelCd.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDelCd.ImageIndex = 0
        Me.btnDelCd.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnDelCd.Location = New System.Drawing.Point(12, 127)
        Me.btnDelCd.Name = "btnDelCd"
        Me.btnDelCd.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnDelCd.SideImage = Nothing
        Me.btnDelCd.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelCd.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnDelCd.Size = New System.Drawing.Size(97, 25)
        Me.btnDelCd.TabIndex = 199
        Me.btnDelCd.Text = "코드 삭제"
        Me.btnDelCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDelCd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnDelCd.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnEditUseDt
        '
        Me.btnEditUseDt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnEditUseDt.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnEditUseDt.ColorFillBlend = CBlendItems2
        Me.btnEditUseDt.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnEditUseDt.Corners.All = CType(6, Short)
        Me.btnEditUseDt.Corners.LowerLeft = CType(6, Short)
        Me.btnEditUseDt.Corners.LowerRight = CType(6, Short)
        Me.btnEditUseDt.Corners.UpperLeft = CType(6, Short)
        Me.btnEditUseDt.Corners.UpperRight = CType(6, Short)
        Me.btnEditUseDt.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnEditUseDt.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnEditUseDt.FocalPoints.CenterPtX = 0.4639175!
        Me.btnEditUseDt.FocalPoints.CenterPtY = 0.16!
        Me.btnEditUseDt.FocalPoints.FocusPtX = 0.02061856!
        Me.btnEditUseDt.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnEditUseDt.FocusPtTracker = DesignerRectTracker4
        Me.btnEditUseDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnEditUseDt.ForeColor = System.Drawing.Color.White
        Me.btnEditUseDt.Image = Nothing
        Me.btnEditUseDt.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnEditUseDt.ImageIndex = 0
        Me.btnEditUseDt.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnEditUseDt.Location = New System.Drawing.Point(276, 127)
        Me.btnEditUseDt.Name = "btnEditUseDt"
        Me.btnEditUseDt.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnEditUseDt.SideImage = Nothing
        Me.btnEditUseDt.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditUseDt.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnEditUseDt.Size = New System.Drawing.Size(97, 25)
        Me.btnEditUseDt.TabIndex = 200
        Me.btnEditUseDt.Text = "사용일시 수정"
        Me.btnEditUseDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnEditUseDt.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnEditUseDt.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClose.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClose.ColorFillBlend = CBlendItems3
        Me.btnClose.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClose.Corners.All = CType(6, Short)
        Me.btnClose.Corners.LowerLeft = CType(6, Short)
        Me.btnClose.Corners.LowerRight = CType(6, Short)
        Me.btnClose.Corners.UpperLeft = CType(6, Short)
        Me.btnClose.Corners.UpperRight = CType(6, Short)
        Me.btnClose.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClose.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClose.FocalPoints.CenterPtX = 0.4639175!
        Me.btnClose.FocalPoints.CenterPtY = 0.16!
        Me.btnClose.FocalPoints.FocusPtX = 0.02061856!
        Me.btnClose.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClose.FocusPtTracker = DesignerRectTracker6
        Me.btnClose.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClose.ForeColor = System.Drawing.Color.White
        Me.btnClose.Image = Nothing
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.ImageIndex = 0
        Me.btnClose.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClose.Location = New System.Drawing.Point(374, 127)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClose.SideImage = Nothing
        Me.btnClose.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClose.Size = New System.Drawing.Size(97, 25)
        Me.btnClose.TabIndex = 201
        Me.btnClose.Text = "닫기"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClose.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGF03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(484, 165)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnEditUseDt)
        Me.Controls.Add(Me.btnDelCd)
        Me.Controls.Add(Me.lblAfter)
        Me.Controls.Add(Me.lblBefore)
        Me.Controls.Add(Me.txtUseDtA)
        Me.Controls.Add(Me.lblArrow)
        Me.Controls.Add(Me.lblUseDtA)
        Me.Controls.Add(Me.txtUseDt)
        Me.Controls.Add(Me.lblUseDt)
        Me.Controls.Add(Me.txtNm)
        Me.Controls.Add(Me.lblNm)
        Me.Controls.Add(Me.txtCd)
        Me.Controls.Add(Me.lblCd)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGF03"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtCd As System.Windows.Forms.TextBox
    Friend WithEvents lblCd As System.Windows.Forms.Label
    Friend WithEvents txtNm As System.Windows.Forms.TextBox
    Friend WithEvents lblNm As System.Windows.Forms.Label
    Friend WithEvents txtUseDt As System.Windows.Forms.TextBox
    Friend WithEvents lblUseDt As System.Windows.Forms.Label
    Friend WithEvents lblUseDtA As System.Windows.Forms.Label
    Friend WithEvents lblArrow As System.Windows.Forms.Label
    Friend WithEvents txtUseDtA As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblBefore As System.Windows.Forms.Label
    Friend WithEvents lblAfter As System.Windows.Forms.Label
    Friend WithEvents btnDelCd As CButtonLib.CButton
    Friend WithEvents btnEditUseDt As CButtonLib.CButton
    Friend WithEvents btnClose As CButtonLib.CButton
End Class
