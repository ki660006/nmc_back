<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB18
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB18))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.btnSearch = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.Label98 = New System.Windows.Forms.Label
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.pnlStoreList = New System.Windows.Forms.Panel
        Me.spdStored = New AxFPSpreadADO.AxfpSpread
        Me.pnlStdDetail = New System.Windows.Forms.Panel
        Me.spdStdDetail = New AxFPSpreadADO.AxfpSpread
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStoreList.SuspendLayout()
        CType(Me.spdStored, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStdDetail.SuspendLayout()
        CType(Me.spdStdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems1
        Me.btnSearch.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSearch.Corners.All = CType(6, Short)
        Me.btnSearch.Corners.LowerLeft = CType(6, Short)
        Me.btnSearch.Corners.LowerRight = CType(6, Short)
        Me.btnSearch.Corners.UpperLeft = CType(6, Short)
        Me.btnSearch.Corners.UpperRight = CType(6, Short)
        Me.btnSearch.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSearch.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSearch.FocalPoints.CenterPtX = 0.4672897!
        Me.btnSearch.FocalPoints.CenterPtY = 0.16!
        Me.btnSearch.FocalPoints.FocusPtX = 0.0!
        Me.btnSearch.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker2
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(947, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSearch.SideImage = Nothing
        Me.btnSearch.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSearch.Size = New System.Drawing.Size(107, 25)
        Me.btnSearch.TabIndex = 186
        Me.btnSearch.Text = "조   회(F6)"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSearch.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExit.FocalPoints.CenterPtX = 0.4387755!
        Me.btnExit.FocalPoints.CenterPtY = 0.56!
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
        Me.btnExit.Location = New System.Drawing.Point(1163, 3)
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
        Me.btnClear.Location = New System.Drawing.Point(1055, 3)
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
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label98.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label98.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.White
        Me.Label98.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label98.Location = New System.Drawing.Point(5, 5)
        Me.Label98.Margin = New System.Windows.Forms.Padding(1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(80, 21)
        Me.Label98.TabIndex = 210
        Me.Label98.Text = "재고일자"
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(86, 5)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate0.TabIndex = 209
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(4, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1272, 9)
        Me.Label1.TabIndex = 211
        Me.Label1.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 35)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 19)
        Me.PictureBox1.TabIndex = 234
        Me.PictureBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(24, 38)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 12)
        Me.Label5.TabIndex = 233
        Me.Label5.Text = "현재고"
        '
        'pnlStoreList
        '
        Me.pnlStoreList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlStoreList.Controls.Add(Me.spdStored)
        Me.pnlStoreList.Location = New System.Drawing.Point(3, 54)
        Me.pnlStoreList.Name = "pnlStoreList"
        Me.pnlStoreList.Size = New System.Drawing.Size(549, 781)
        Me.pnlStoreList.TabIndex = 235
        '
        'spdStored
        '
        Me.spdStored.DataSource = Nothing
        Me.spdStored.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdStored.Location = New System.Drawing.Point(0, 0)
        Me.spdStored.Margin = New System.Windows.Forms.Padding(1)
        Me.spdStored.Name = "spdStored"
        Me.spdStored.OcxState = CType(resources.GetObject("spdStored.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStored.Size = New System.Drawing.Size(549, 781)
        Me.spdStored.TabIndex = 0
        '
        'pnlStdDetail
        '
        Me.pnlStdDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlStdDetail.Controls.Add(Me.spdStdDetail)
        Me.pnlStdDetail.Location = New System.Drawing.Point(558, 53)
        Me.pnlStdDetail.Margin = New System.Windows.Forms.Padding(1)
        Me.pnlStdDetail.Name = "pnlStdDetail"
        Me.pnlStdDetail.Size = New System.Drawing.Size(709, 782)
        Me.pnlStdDetail.TabIndex = 236
        '
        'spdStdDetail
        '
        Me.spdStdDetail.DataSource = Nothing
        Me.spdStdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdStdDetail.Location = New System.Drawing.Point(0, 0)
        Me.spdStdDetail.Name = "spdStdDetail"
        Me.spdStdDetail.OcxState = CType(resources.GetObject("spdStdDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStdDetail.Size = New System.Drawing.Size(709, 782)
        Me.spdStdDetail.TabIndex = 0
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(558, 34)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 19)
        Me.PictureBox2.TabIndex = 238
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(579, 37)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 12)
        Me.Label2.TabIndex = 237
        Me.Label2.Text = "현재고 상세내역"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(295, 37)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(261, 12)
        Me.Label3.TabIndex = 239
        Me.Label3.Text = "※ 현재고 수량 선택시 상세내역이 조회 됩니다."
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 843)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1272, 32)
        Me.Panel1.TabIndex = 240
        '
        'FGB18
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1272, 875)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlStdDetail)
        Me.Controls.Add(Me.Label98)
        Me.Controls.Add(Me.dtpDate0)
        Me.Controls.Add(Me.pnlStoreList)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "FGB18"
        Me.Text = "혈액 재고량 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStoreList.ResumeLayout(False)
        CType(Me.spdStored, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStdDetail.ResumeLayout(False)
        CType(Me.spdStdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlStoreList As System.Windows.Forms.Panel
    Friend WithEvents spdStored As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlStdDetail As System.Windows.Forms.Panel
    Friend WithEvents spdStdDetail As AxFPSpreadADO.AxfpSpread
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
