<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ITEMSAVE
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
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ITEMSAVE))
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoAll = New System.Windows.Forms.RadioButton
        Me.rdoMe = New System.Windows.Forms.RadioButton
        Me.bntSave = New CButtonLib.CButton
        Me.btnDel = New CButtonLib.CButton
        Me.lstItem = New System.Windows.Forms.ListBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.rdoAll)
        Me.Panel1.Controls.Add(Me.rdoMe)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(107, 21)
        Me.Panel1.TabIndex = 0
        '
        'rdoAll
        '
        Me.rdoAll.AutoSize = True
        Me.rdoAll.Location = New System.Drawing.Point(56, 2)
        Me.rdoAll.Name = "rdoAll"
        Me.rdoAll.Size = New System.Drawing.Size(47, 16)
        Me.rdoAll.TabIndex = 2
        Me.rdoAll.TabStop = True
        Me.rdoAll.Text = "전체"
        Me.rdoAll.UseVisualStyleBackColor = True
        '
        'rdoMe
        '
        Me.rdoMe.AutoSize = True
        Me.rdoMe.Checked = True
        Me.rdoMe.Location = New System.Drawing.Point(3, 2)
        Me.rdoMe.Name = "rdoMe"
        Me.rdoMe.Size = New System.Drawing.Size(47, 16)
        Me.rdoMe.TabIndex = 1
        Me.rdoMe.TabStop = True
        Me.rdoMe.Text = "본인"
        Me.rdoMe.UseVisualStyleBackColor = True
        '
        'bntSave
        '
        Me.bntSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bntSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.bntSave.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.bntSave.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems3.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.bntSave.ColorFillBlend = CBlendItems3
        Me.bntSave.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.bntSave.Corners.All = CType(6, Short)
        Me.bntSave.Corners.LowerLeft = CType(6, Short)
        Me.bntSave.Corners.LowerRight = CType(6, Short)
        Me.bntSave.Corners.UpperLeft = CType(6, Short)
        Me.bntSave.Corners.UpperRight = CType(6, Short)
        Me.bntSave.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.bntSave.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.bntSave.FocalPoints.CenterPtX = 0.5106383!
        Me.bntSave.FocalPoints.CenterPtY = 0.4090909!
        Me.bntSave.FocalPoints.FocusPtX = 0.0!
        Me.bntSave.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.bntSave.FocusPtTracker = DesignerRectTracker6
        Me.bntSave.Image = Nothing
        Me.bntSave.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.bntSave.ImageIndex = 0
        Me.bntSave.ImageSize = New System.Drawing.Size(16, 16)
        Me.bntSave.Location = New System.Drawing.Point(108, 0)
        Me.bntSave.Margin = New System.Windows.Forms.Padding(1)
        Me.bntSave.Name = "bntSave"
        Me.bntSave.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.bntSave.SideImage = Nothing
        Me.bntSave.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bntSave.SideImageSize = New System.Drawing.Size(48, 48)
        Me.bntSave.Size = New System.Drawing.Size(48, 22)
        Me.bntSave.TabIndex = 33
        Me.bntSave.Text = "설정"
        Me.bntSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.bntSave.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.bntSave.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDel.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnDel.ColorFillBlend = CBlendItems1
        Me.btnDel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnDel.Corners.All = CType(6, Short)
        Me.btnDel.Corners.LowerLeft = CType(6, Short)
        Me.btnDel.Corners.LowerRight = CType(6, Short)
        Me.btnDel.Corners.UpperLeft = CType(6, Short)
        Me.btnDel.Corners.UpperRight = CType(6, Short)
        Me.btnDel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnDel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnDel.FocalPoints.CenterPtX = 1.0!
        Me.btnDel.FocalPoints.CenterPtY = 0.0!
        Me.btnDel.FocalPoints.FocusPtX = 0.0!
        Me.btnDel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDel.FocusPtTracker = DesignerRectTracker2
        Me.btnDel.Image = Nothing
        Me.btnDel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDel.ImageIndex = 0
        Me.btnDel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnDel.Location = New System.Drawing.Point(157, 0)
        Me.btnDel.Margin = New System.Windows.Forms.Padding(1)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnDel.SideImage = Nothing
        Me.btnDel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnDel.Size = New System.Drawing.Size(48, 22)
        Me.btnDel.TabIndex = 34
        Me.btnDel.Text = "삭제"
        Me.btnDel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnDel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lstItem
        '
        Me.lstItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstItem.FormattingEnabled = True
        Me.lstItem.ItemHeight = 12
        Me.lstItem.Location = New System.Drawing.Point(0, 23)
        Me.lstItem.Name = "lstItem"
        Me.lstItem.Size = New System.Drawing.Size(205, 64)
        Me.lstItem.TabIndex = 35
        '
        'ITEMSAVE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Controls.Add(Me.lstItem)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.bntSave)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "ITEMSAVE"
        Me.Size = New System.Drawing.Size(205, 89)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdoMe As System.Windows.Forms.RadioButton
    Friend WithEvents bntSave As CButtonLib.CButton
    Friend WithEvents btnDel As CButtonLib.CButton
    Friend WithEvents lstItem As System.Windows.Forms.ListBox

End Class
