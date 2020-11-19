<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGF11_S03
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGF11_S03))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.spdCdList = New AxFPSpreadADO.AxfpSpread()
        Me.btnINSERT = New System.Windows.Forms.Button()
        Me.btnQuery = New CButtonLib.CButton()
        Me.chkOrder = New System.Windows.Forms.CheckBox()
        Me.chkNotSpc = New System.Windows.Forms.CheckBox()
        Me.chkCtGbn_q = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoSort_spc = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.rdoSort_test = New System.Windows.Forms.RadioButton()
        Me.rdoSort_lis = New System.Windows.Forms.RadioButton()
        Me.rdoSort_ocs = New System.Windows.Forms.RadioButton()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.cboOps = New System.Windows.Forms.ComboBox()
        Me.cboTordSlip_q = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboFilter = New System.Windows.Forms.ComboBox()
        Me.cboPartSlip = New System.Windows.Forms.ComboBox()
        Me.cboPSGbn = New System.Windows.Forms.ComboBox()
        Me.cboBccls_q = New System.Windows.Forms.ComboBox()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.pnlCenter = New System.Windows.Forms.Panel()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlCenter.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdCdList
        '
        Me.spdCdList.DataSource = Nothing
        Me.spdCdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdCdList.Location = New System.Drawing.Point(0, 0)
        Me.spdCdList.Name = "spdCdList"
        Me.spdCdList.OcxState = CType(resources.GetObject("spdCdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCdList.Size = New System.Drawing.Size(771, 274)
        Me.spdCdList.TabIndex = 211
        '
        'btnINSERT
        '
        Me.btnINSERT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnINSERT.Location = New System.Drawing.Point(646, 8)
        Me.btnINSERT.Name = "btnINSERT"
        Me.btnINSERT.Size = New System.Drawing.Size(113, 24)
        Me.btnINSERT.TabIndex = 212
        Me.btnINSERT.Text = "검사항목 추가"
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems1
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 1.0!
        Me.btnQuery.FocalPoints.CenterPtY = 1.0!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(489, 71)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(267, 21)
        Me.btnQuery.TabIndex = 213
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chkOrder
        '
        Me.chkOrder.AutoSize = True
        Me.chkOrder.Location = New System.Drawing.Point(642, 51)
        Me.chkOrder.Name = "chkOrder"
        Me.chkOrder.Size = New System.Drawing.Size(112, 16)
        Me.chkOrder.TabIndex = 225
        Me.chkOrder.Text = "처방가능 항목만"
        Me.chkOrder.UseVisualStyleBackColor = True
        '
        'chkNotSpc
        '
        Me.chkNotSpc.AutoSize = True
        Me.chkNotSpc.Location = New System.Drawing.Point(540, 51)
        Me.chkNotSpc.Name = "chkNotSpc"
        Me.chkNotSpc.Size = New System.Drawing.Size(100, 16)
        Me.chkNotSpc.TabIndex = 224
        Me.chkNotSpc.Text = "검체코드 제외"
        Me.chkNotSpc.UseVisualStyleBackColor = True
        '
        'chkCtGbn_q
        '
        Me.chkCtGbn_q.AutoSize = True
        Me.chkCtGbn_q.Location = New System.Drawing.Point(492, 51)
        Me.chkCtGbn_q.Name = "chkCtGbn_q"
        Me.chkCtGbn_q.Size = New System.Drawing.Size(48, 16)
        Me.chkCtGbn_q.TabIndex = 226
        Me.chkCtGbn_q.Text = "특수"
        Me.chkCtGbn_q.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.rdoSort_spc)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.rdoSort_test)
        Me.Panel1.Controls.Add(Me.rdoSort_lis)
        Me.Panel1.Controls.Add(Me.rdoSort_ocs)
        Me.Panel1.Location = New System.Drawing.Point(489, 25)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(270, 21)
        Me.Panel1.TabIndex = 227
        '
        'rdoSort_spc
        '
        Me.rdoSort_spc.AutoSize = True
        Me.rdoSort_spc.Location = New System.Drawing.Point(109, 2)
        Me.rdoSort_spc.Name = "rdoSort_spc"
        Me.rdoSort_spc.Size = New System.Drawing.Size(47, 16)
        Me.rdoSort_spc.TabIndex = 210
        Me.rdoSort_spc.Text = "검체"
        Me.rdoSort_spc.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(0, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 12)
        Me.Label10.TabIndex = 209
        Me.Label10.Text = " 표시방법"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'rdoSort_test
        '
        Me.rdoSort_test.AutoSize = True
        Me.rdoSort_test.Location = New System.Drawing.Point(58, 2)
        Me.rdoSort_test.Name = "rdoSort_test"
        Me.rdoSort_test.Size = New System.Drawing.Size(47, 16)
        Me.rdoSort_test.TabIndex = 213
        Me.rdoSort_test.Text = "검사"
        Me.rdoSort_test.UseVisualStyleBackColor = True
        '
        'rdoSort_lis
        '
        Me.rdoSort_lis.AutoSize = True
        Me.rdoSort_lis.Location = New System.Drawing.Point(160, 2)
        Me.rdoSort_lis.Name = "rdoSort_lis"
        Me.rdoSort_lis.Size = New System.Drawing.Size(41, 16)
        Me.rdoSort_lis.TabIndex = 211
        Me.rdoSort_lis.Text = "LIS"
        Me.rdoSort_lis.UseVisualStyleBackColor = True
        '
        'rdoSort_ocs
        '
        Me.rdoSort_ocs.AutoSize = True
        Me.rdoSort_ocs.Location = New System.Drawing.Point(207, 2)
        Me.rdoSort_ocs.Name = "rdoSort_ocs"
        Me.rdoSort_ocs.Size = New System.Drawing.Size(49, 16)
        Me.rdoSort_ocs.TabIndex = 212
        Me.rdoSort_ocs.Text = "OCS"
        Me.rdoSort_ocs.UseVisualStyleBackColor = True
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.cboOps)
        Me.pnlTop.Controls.Add(Me.chkOrder)
        Me.pnlTop.Controls.Add(Me.cboTordSlip_q)
        Me.pnlTop.Controls.Add(Me.Panel1)
        Me.pnlTop.Controls.Add(Me.Label8)
        Me.pnlTop.Controls.Add(Me.chkNotSpc)
        Me.pnlTop.Controls.Add(Me.cboFilter)
        Me.pnlTop.Controls.Add(Me.cboPartSlip)
        Me.pnlTop.Controls.Add(Me.chkCtGbn_q)
        Me.pnlTop.Controls.Add(Me.cboPSGbn)
        Me.pnlTop.Controls.Add(Me.btnQuery)
        Me.pnlTop.Controls.Add(Me.cboBccls_q)
        Me.pnlTop.Controls.Add(Me.txtFilter)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(771, 101)
        Me.pnlTop.TabIndex = 228
        '
        'cboOps
        '
        Me.cboOps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOps.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboOps.FormattingEnabled = True
        Me.cboOps.Items.AddRange(New Object() {"=", ">", "<", ">=", "<=", "LIKE *", "* LIKE *", "* LIKE"})
        Me.cboOps.Location = New System.Drawing.Point(126, 72)
        Me.cboOps.Name = "cboOps"
        Me.cboOps.Size = New System.Drawing.Size(83, 20)
        Me.cboOps.TabIndex = 223
        '
        'cboTordSlip_q
        '
        Me.cboTordSlip_q.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTordSlip_q.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTordSlip_q.FormattingEnabled = True
        Me.cboTordSlip_q.Location = New System.Drawing.Point(74, 28)
        Me.cboTordSlip_q.Name = "cboTordSlip_q"
        Me.cboTordSlip_q.Size = New System.Drawing.Size(406, 20)
        Me.cboTordSlip_q.TabIndex = 221
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(3, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 21)
        Me.Label8.TabIndex = 222
        Me.Label8.Text = " 처방슬립"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboFilter
        '
        Me.cboFilter.AutoCompleteCustomSource.AddRange(New String() {"검사코드", "검체코드", "처방코드", "결과코드", "검사구분", "검사명", "위탁기관명"})
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboFilter.FormattingEnabled = True
        Me.cboFilter.Items.AddRange(New Object() {"검사코드", "검체코드", "처방코드", "결과코드", "검사구분", "검사명", "위탁기관명"})
        Me.cboFilter.Location = New System.Drawing.Point(4, 72)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(120, 20)
        Me.cboFilter.TabIndex = 220
        '
        'cboPartSlip
        '
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPartSlip.FormattingEnabled = True
        Me.cboPartSlip.Location = New System.Drawing.Point(147, 50)
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(333, 20)
        Me.cboPartSlip.TabIndex = 218
        '
        'cboPSGbn
        '
        Me.cboPSGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPSGbn.FormattingEnabled = True
        Me.cboPSGbn.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboPSGbn.Location = New System.Drawing.Point(74, 50)
        Me.cboPSGbn.Name = "cboPSGbn"
        Me.cboPSGbn.Size = New System.Drawing.Size(71, 20)
        Me.cboPSGbn.TabIndex = 217
        '
        'cboBccls_q
        '
        Me.cboBccls_q.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBccls_q.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBccls_q.FormattingEnabled = True
        Me.cboBccls_q.Location = New System.Drawing.Point(74, 6)
        Me.cboBccls_q.Name = "cboBccls_q"
        Me.cboBccls_q.Size = New System.Drawing.Size(406, 20)
        Me.cboBccls_q.TabIndex = 214
        '
        'txtFilter
        '
        Me.txtFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtFilter.Location = New System.Drawing.Point(211, 70)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(270, 21)
        Me.txtFilter.TabIndex = 219
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(3, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 21)
        Me.Label7.TabIndex = 216
        Me.Label7.Text = " 부서/분야"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(3, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(70, 21)
        Me.Label6.TabIndex = 215
        Me.Label6.Text = " 검체분류"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnINSERT)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 375)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(771, 44)
        Me.pnlBottom.TabIndex = 229
        '
        'pnlCenter
        '
        Me.pnlCenter.Controls.Add(Me.spdCdList)
        Me.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCenter.Location = New System.Drawing.Point(0, 101)
        Me.pnlCenter.Name = "pnlCenter"
        Me.pnlCenter.Size = New System.Drawing.Size(771, 274)
        Me.pnlCenter.TabIndex = 230
        '
        'FGF11_S03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(771, 419)
        Me.Controls.Add(Me.pnlCenter)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Name = "FGF11_S03"
        Me.Text = "검사의뢰지침 세부검사 검사마스터"
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlCenter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdCdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnINSERT As System.Windows.Forms.Button
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents chkOrder As System.Windows.Forms.CheckBox
    Friend WithEvents chkNotSpc As System.Windows.Forms.CheckBox
    Friend WithEvents chkCtGbn_q As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoSort_spc As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents rdoSort_test As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSort_lis As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSort_ocs As System.Windows.Forms.RadioButton
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlCenter As System.Windows.Forms.Panel
    Friend WithEvents cboOps As System.Windows.Forms.ComboBox
    Friend WithEvents cboTordSlip_q As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents cboPSGbn As System.Windows.Forms.ComboBox
    Friend WithEvents cboBccls_q As System.Windows.Forms.ComboBox
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
