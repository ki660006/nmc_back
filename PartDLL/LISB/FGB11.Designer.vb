<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB11
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB11))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExecute = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.txtSBldno = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.pnlSearchGbn = New System.Windows.Forms.Panel
        Me.rdoAbn = New System.Windows.Forms.RadioButton
        Me.rdoChg = New System.Windows.Forms.RadioButton
        Me.lblSGbn = New System.Windows.Forms.Label
        Me.pnlList = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.chkAbo = New System.Windows.Forms.CheckBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlSearchGbn.SuspendLayout()
        Me.pnlList.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(3, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1272, 9)
        Me.Label1.TabIndex = 215
        Me.Label1.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'btnExecute
        '
        Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExecute.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExecute.ColorFillBlend = CBlendItems1
        Me.btnExecute.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExecute.Corners.All = CType(6, Short)
        Me.btnExecute.Corners.LowerLeft = CType(6, Short)
        Me.btnExecute.Corners.LowerRight = CType(6, Short)
        Me.btnExecute.Corners.UpperLeft = CType(6, Short)
        Me.btnExecute.Corners.UpperRight = CType(6, Short)
        Me.btnExecute.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExecute.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExecute.FocalPoints.CenterPtX = 0.4672897!
        Me.btnExecute.FocalPoints.CenterPtY = 0.16!
        Me.btnExecute.FocalPoints.FocusPtX = 0.0!
        Me.btnExecute.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExecute.FocusPtTracker = DesignerRectTracker2
        Me.btnExecute.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExecute.ForeColor = System.Drawing.Color.White
        Me.btnExecute.Image = Nothing
        Me.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExecute.ImageIndex = 0
        Me.btnExecute.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExecute.Location = New System.Drawing.Point(948, 3)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExecute.SideImage = Nothing
        Me.btnExecute.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExecute.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExecute.Size = New System.Drawing.Size(107, 25)
        Me.btnExecute.TabIndex = 185
        Me.btnExecute.Tag = "availdt"
        Me.btnExecute.Text = "폐  기(F7)"
        Me.btnExecute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExecute.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExecute.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExit.FocalPoints.CenterPtX = 0.4725275!
        Me.btnExit.FocalPoints.CenterPtY = 0.64!
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
        Me.btnExit.Location = New System.Drawing.Point(1164, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(98, 25)
        Me.btnExit.TabIndex = 184
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4672897!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
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
        Me.btnClear.Location = New System.Drawing.Point(1056, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 183
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtSBldno
        '
        Me.txtSBldno.Location = New System.Drawing.Point(343, 3)
        Me.txtSBldno.Margin = New System.Windows.Forms.Padding(1)
        Me.txtSBldno.MaxLength = 10
        Me.txtSBldno.Name = "txtSBldno"
        Me.txtSBldno.Size = New System.Drawing.Size(120, 21)
        Me.txtSBldno.TabIndex = 222
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(262, 3)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 21)
        Me.Label4.TabIndex = 221
        Me.Label4.Text = "혈액번호"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlSearchGbn
        '
        Me.pnlSearchGbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchGbn.Controls.Add(Me.rdoAbn)
        Me.pnlSearchGbn.Controls.Add(Me.rdoChg)
        Me.pnlSearchGbn.ForeColor = System.Drawing.Color.DarkGreen
        Me.pnlSearchGbn.Location = New System.Drawing.Point(86, 3)
        Me.pnlSearchGbn.Name = "pnlSearchGbn"
        Me.pnlSearchGbn.Size = New System.Drawing.Size(140, 22)
        Me.pnlSearchGbn.TabIndex = 220
        '
        'rdoAbn
        '
        Me.rdoAbn.Checked = True
        Me.rdoAbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAbn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoAbn.Location = New System.Drawing.Point(9, 3)
        Me.rdoAbn.Name = "rdoAbn"
        Me.rdoAbn.Size = New System.Drawing.Size(56, 18)
        Me.rdoAbn.TabIndex = 5
        Me.rdoAbn.TabStop = True
        Me.rdoAbn.Tag = "1"
        Me.rdoAbn.Text = "폐기"
        Me.rdoAbn.UseCompatibleTextRendering = True
        '
        'rdoChg
        '
        Me.rdoChg.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoChg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoChg.Location = New System.Drawing.Point(74, 3)
        Me.rdoChg.Name = "rdoChg"
        Me.rdoChg.Size = New System.Drawing.Size(56, 18)
        Me.rdoChg.TabIndex = 6
        Me.rdoChg.Tag = "1"
        Me.rdoChg.Text = "교환"
        Me.rdoChg.UseCompatibleTextRendering = True
        '
        'lblSGbn
        '
        Me.lblSGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSGbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSGbn.ForeColor = System.Drawing.Color.White
        Me.lblSGbn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSGbn.Location = New System.Drawing.Point(4, 3)
        Me.lblSGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSGbn.Name = "lblSGbn"
        Me.lblSGbn.Size = New System.Drawing.Size(80, 21)
        Me.lblSGbn.TabIndex = 219
        Me.lblSGbn.Text = "작업구분"
        Me.lblSGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlList
        '
        Me.pnlList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlList.Controls.Add(Me.spdList)
        Me.pnlList.Location = New System.Drawing.Point(5, 34)
        Me.pnlList.Margin = New System.Windows.Forms.Padding(1)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Size = New System.Drawing.Size(1263, 799)
        Me.pnlList.TabIndex = 223
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1263, 799)
        Me.spdList.TabIndex = 0
        '
        'chkAbo
        '
        Me.chkAbo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkAbo.AutoSize = True
        Me.chkAbo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkAbo.ForeColor = System.Drawing.Color.Red
        Me.chkAbo.Location = New System.Drawing.Point(1080, 5)
        Me.chkAbo.Margin = New System.Windows.Forms.Padding(1)
        Me.chkAbo.Name = "chkAbo"
        Me.chkAbo.Size = New System.Drawing.Size(188, 16)
        Me.chkAbo.TabIndex = 240
        Me.chkAbo.Text = "유효일시가 지난 혈액 조회"
        Me.chkAbo.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnExecute)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 843)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1272, 32)
        Me.Panel1.TabIndex = 241
        '
        'FGB16
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1272, 875)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkAbo)
        Me.Controls.Add(Me.pnlList)
        Me.Controls.Add(Me.txtSBldno)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.pnlSearchGbn)
        Me.Controls.Add(Me.lblSGbn)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "FGB16"
        Me.Text = "혈액자체폐기"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSearchGbn.ResumeLayout(False)
        Me.pnlList.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExecute As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents txtSBldno As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoAbn As System.Windows.Forms.RadioButton
    Friend WithEvents rdoChg As System.Windows.Forms.RadioButton
    Friend WithEvents lblSGbn As System.Windows.Forms.Label
    Friend WithEvents pnlList As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkAbo As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
