<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGM01_S01
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
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGM01_S01))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDrid = New System.Windows.Forms.TextBox
        Me.lblid = New System.Windows.Forms.Label
        Me.btnReg = New CButtonLib.CButton
        Me.chb_MIC = New System.Windows.Forms.CheckBox
        Me.chb_Disk = New System.Windows.Forms.CheckBox
        Me.btncnl = New CButtonLib.CButton
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "처방의사 ID를 입력해주세요 "
        '
        'txtDrid
        '
        Me.txtDrid.Location = New System.Drawing.Point(72, 64)
        Me.txtDrid.Name = "txtDrid"
        Me.txtDrid.Size = New System.Drawing.Size(100, 21)
        Me.txtDrid.TabIndex = 1
        '
        'lblid
        '
        Me.lblid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblid.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblid.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblid.ForeColor = System.Drawing.Color.White
        Me.lblid.Location = New System.Drawing.Point(12, 64)
        Me.lblid.Name = "lblid"
        Me.lblid.Size = New System.Drawing.Size(54, 21)
        Me.lblid.TabIndex = 204
        Me.lblid.Text = "ID"
        Me.lblid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems1
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
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker2
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(11, 94)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(80, 25)
        Me.btnReg.TabIndex = 205
        Me.btnReg.Text = "처  방"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chb_MIC
        '
        Me.chb_MIC.AutoSize = True
        Me.chb_MIC.Checked = True
        Me.chb_MIC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chb_MIC.Location = New System.Drawing.Point(13, 42)
        Me.chb_MIC.Name = "chb_MIC"
        Me.chb_MIC.Size = New System.Drawing.Size(47, 16)
        Me.chb_MIC.TabIndex = 206
        Me.chb_MIC.Text = "MIC"
        Me.chb_MIC.UseVisualStyleBackColor = True
        '
        'chb_Disk
        '
        Me.chb_Disk.AutoSize = True
        Me.chb_Disk.Location = New System.Drawing.Point(72, 42)
        Me.chb_Disk.Name = "chb_Disk"
        Me.chb_Disk.Size = New System.Drawing.Size(48, 16)
        Me.chb_Disk.TabIndex = 207
        Me.chb_Disk.Text = "Disk"
        Me.chb_Disk.UseVisualStyleBackColor = True
        '
        'btncnl
        '
        Me.btncnl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btncnl.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btncnl.ColorFillBlend = CBlendItems2
        Me.btncnl.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btncnl.Corners.All = CType(6, Short)
        Me.btncnl.Corners.LowerLeft = CType(6, Short)
        Me.btncnl.Corners.LowerRight = CType(6, Short)
        Me.btncnl.Corners.UpperLeft = CType(6, Short)
        Me.btncnl.Corners.UpperRight = CType(6, Short)
        Me.btncnl.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btncnl.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btncnl.FocalPoints.CenterPtX = 0.5!
        Me.btncnl.FocalPoints.CenterPtY = 0.0!
        Me.btncnl.FocalPoints.FocusPtX = 0.0!
        Me.btncnl.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btncnl.FocusPtTracker = DesignerRectTracker4
        Me.btncnl.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btncnl.ForeColor = System.Drawing.Color.White
        Me.btncnl.Image = Nothing
        Me.btncnl.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btncnl.ImageIndex = 0
        Me.btncnl.ImageSize = New System.Drawing.Size(16, 16)
        Me.btncnl.Location = New System.Drawing.Point(97, 94)
        Me.btncnl.Name = "btncnl"
        Me.btncnl.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btncnl.SideImage = Nothing
        Me.btncnl.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btncnl.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btncnl.Size = New System.Drawing.Size(70, 25)
        Me.btncnl.TabIndex = 208
        Me.btncnl.Text = "취  소"
        Me.btncnl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btncnl.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btncnl.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGM01_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(188, 149)
        Me.ControlBox = False
        Me.Controls.Add(Me.btncnl)
        Me.Controls.Add(Me.chb_Disk)
        Me.Controls.Add(Me.chb_MIC)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.lblid)
        Me.Controls.Add(Me.txtDrid)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FGM01_S01"
        Me.Text = "FGM01_S01"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDrid As System.Windows.Forms.TextBox
    Friend WithEvents lblid As System.Windows.Forms.Label
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents chb_MIC As System.Windows.Forms.CheckBox
    Friend WithEvents chb_Disk As System.Windows.Forms.CheckBox
    Friend WithEvents btncnl As CButtonLib.CButton
End Class
