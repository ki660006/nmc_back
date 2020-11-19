<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS20_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS20_S01))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.lblRstDT = New System.Windows.Forms.Label()
        Me.chk01 = New System.Windows.Forms.CheckBox()
        Me.chk99 = New System.Windows.Forms.CheckBox()
        Me.chk05 = New System.Windows.Forms.CheckBox()
        Me.chk04 = New System.Windows.Forms.CheckBox()
        Me.chk03 = New System.Windows.Forms.CheckBox()
        Me.chk02 = New System.Windows.Forms.CheckBox()
        Me.txtEtc = New System.Windows.Forms.TextBox()
        Me.btnOk = New CButtonLib.CButton()
        Me.btnClose = New CButtonLib.CButton()
        Me.SuspendLayout()
        '
        'lblRstDT
        '
        Me.lblRstDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstDT.ForeColor = System.Drawing.Color.White
        Me.lblRstDT.Location = New System.Drawing.Point(7, 5)
        Me.lblRstDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRstDT.Name = "lblRstDT"
        Me.lblRstDT.Size = New System.Drawing.Size(132, 21)
        Me.lblRstDT.TabIndex = 10
        Me.lblRstDT.Text = "검체유형 선택"
        Me.lblRstDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chk01
        '
        Me.chk01.AutoSize = True
        Me.chk01.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk01.Location = New System.Drawing.Point(12, 36)
        Me.chk01.Name = "chk01"
        Me.chk01.Size = New System.Drawing.Size(79, 16)
        Me.chk01.TabIndex = 11
        Me.chk01.Text = "11 : 혈액"
        Me.chk01.UseVisualStyleBackColor = True
        '
        'chk99
        '
        Me.chk99.AutoSize = True
        Me.chk99.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk99.Location = New System.Drawing.Point(12, 143)
        Me.chk99.Name = "chk99"
        Me.chk99.Size = New System.Drawing.Size(79, 16)
        Me.chk99.TabIndex = 12
        Me.chk99.Text = "99 : 기타"
        Me.chk99.UseVisualStyleBackColor = True
        '
        'chk05
        '
        Me.chk05.AutoSize = True
        Me.chk05.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk05.Location = New System.Drawing.Point(12, 121)
        Me.chk05.Name = "chk05"
        Me.chk05.Size = New System.Drawing.Size(79, 16)
        Me.chk05.TabIndex = 13
        Me.chk05.Text = "15 : 가래"
        Me.chk05.UseVisualStyleBackColor = True
        '
        'chk04
        '
        Me.chk04.AutoSize = True
        Me.chk04.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk04.Location = New System.Drawing.Point(12, 99)
        Me.chk04.Name = "chk04"
        Me.chk04.Size = New System.Drawing.Size(105, 16)
        Me.chk04.TabIndex = 14
        Me.chk04.Text = "14 : 뇌척수액"
        Me.chk04.UseVisualStyleBackColor = True
        '
        'chk03
        '
        Me.chk03.AutoSize = True
        Me.chk03.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk03.Location = New System.Drawing.Point(12, 78)
        Me.chk03.Name = "chk03"
        Me.chk03.Size = New System.Drawing.Size(105, 16)
        Me.chk03.TabIndex = 15
        Me.chk03.Text = "13 : 인두도말"
        Me.chk03.UseVisualStyleBackColor = True
        '
        'chk02
        '
        Me.chk02.AutoSize = True
        Me.chk02.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chk02.Location = New System.Drawing.Point(12, 57)
        Me.chk02.Name = "chk02"
        Me.chk02.Size = New System.Drawing.Size(79, 16)
        Me.chk02.TabIndex = 16
        Me.chk02.Text = "12 : 대변"
        Me.chk02.UseVisualStyleBackColor = True
        '
        'txtEtc
        '
        Me.txtEtc.Location = New System.Drawing.Point(11, 162)
        Me.txtEtc.Name = "txtEtc"
        Me.txtEtc.Size = New System.Drawing.Size(238, 21)
        Me.txtEtc.TabIndex = 17
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOk.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnOk.ColorFillBlend = CBlendItems1
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
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOk.FocusPtTracker = DesignerRectTracker2
        Me.btnOk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOk.ForeColor = System.Drawing.Color.White
        Me.btnOk.Image = Nothing
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOk.ImageIndex = 0
        Me.btnOk.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnOk.Location = New System.Drawing.Point(48, 189)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnOk.SideImage = Nothing
        Me.btnOk.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnOk.Size = New System.Drawing.Size(100, 25)
        Me.btnOk.TabIndex = 202
        Me.btnOk.Text = "선 택"
        Me.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnOk.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = True
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
        Me.btnClose.Location = New System.Drawing.Point(149, 189)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClose.SideImage = Nothing
        Me.btnClose.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClose.Size = New System.Drawing.Size(100, 25)
        Me.btnClose.TabIndex = 203
        Me.btnClose.Text = "취 소"
        Me.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClose.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGS20_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(253, 221)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtEtc)
        Me.Controls.Add(Me.chk02)
        Me.Controls.Add(Me.chk03)
        Me.Controls.Add(Me.chk04)
        Me.Controls.Add(Me.chk05)
        Me.Controls.Add(Me.chk99)
        Me.Controls.Add(Me.chk01)
        Me.Controls.Add(Me.lblRstDT)
        Me.Name = "FGS20_S01"
        Me.Text = "검체유형"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblRstDT As System.Windows.Forms.Label
    Friend WithEvents chk01 As System.Windows.Forms.CheckBox
    Friend WithEvents chk99 As System.Windows.Forms.CheckBox
    Friend WithEvents chk05 As System.Windows.Forms.CheckBox
    Friend WithEvents chk04 As System.Windows.Forms.CheckBox
    Friend WithEvents chk03 As System.Windows.Forms.CheckBox
    Friend WithEvents chk02 As System.Windows.Forms.CheckBox
    Friend WithEvents txtEtc As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As CButtonLib.CButton
    Friend WithEvents btnClose As CButtonLib.CButton
End Class
