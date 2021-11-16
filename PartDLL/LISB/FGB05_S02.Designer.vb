<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB05_S02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB05_S02))
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.cboRH = New System.Windows.Forms.ComboBox
        Me.dtpIndt = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.cboBType = New System.Windows.Forms.ComboBox
        Me.dtpDonDt = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblBType = New System.Windows.Forms.Label
        Me.txtBldQnt = New System.Windows.Forms.TextBox
        Me.txtBType = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBldNm = New System.Windows.Forms.TextBox
        Me.lblComNmd = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboBldGbn = New System.Windows.Forms.ComboBox
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblPatNm = New System.Windows.Forms.Label
        Me.lblComCd = New System.Windows.Forms.Label
        Me.lblAvailDt = New System.Windows.Forms.Label
        Me.lblDonQnt = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(871, 576)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 44)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(780, 576)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(85, 44)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "입고"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cboRH
        '
        Me.cboRH.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboRH.Items.AddRange(New Object() {"+", "-"})
        Me.cboRH.Location = New System.Drawing.Point(347, 19)
        Me.cboRH.Margin = New System.Windows.Forms.Padding(1)
        Me.cboRH.Name = "cboRH"
        Me.cboRH.Size = New System.Drawing.Size(38, 21)
        Me.cboRH.TabIndex = 147
        Me.cboRH.Visible = False
        '
        'dtpIndt
        '
        Me.dtpIndt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpIndt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpIndt.Location = New System.Drawing.Point(600, 17)
        Me.dtpIndt.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpIndt.Name = "dtpIndt"
        Me.dtpIndt.Size = New System.Drawing.Size(166, 21)
        Me.dtpIndt.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(527, 45)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 21)
        Me.Label7.TabIndex = 155
        Me.Label7.Text = "헌혈일자"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboBType
        '
        Me.cboBType.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBType.Items.AddRange(New Object() {"A", "B", "O", "AB"})
        Me.cboBType.Location = New System.Drawing.Point(294, 19)
        Me.cboBType.Margin = New System.Windows.Forms.Padding(1)
        Me.cboBType.Name = "cboBType"
        Me.cboBType.Size = New System.Drawing.Size(52, 21)
        Me.cboBType.TabIndex = 146
        Me.cboBType.Visible = False
        '
        'dtpDonDt
        '
        Me.dtpDonDt.CustomFormat = "yyyy-MM-dd HH:mm"
        Me.dtpDonDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDonDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDonDt.Location = New System.Drawing.Point(600, 45)
        Me.dtpDonDt.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDonDt.Name = "dtpDonDt"
        Me.dtpDonDt.Size = New System.Drawing.Size(166, 21)
        Me.dtpDonDt.TabIndex = 4
        Me.dtpDonDt.Value = New Date(2013, 8, 8, 9, 48, 0, 0)
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Location = New System.Drawing.Point(527, 17)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 21)
        Me.Label5.TabIndex = 156
        Me.Label5.Text = "입고일자"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBType
        '
        Me.lblBType.BackColor = System.Drawing.Color.White
        Me.lblBType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBType.Font = New System.Drawing.Font("Arial Black", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBType.ForeColor = System.Drawing.Color.Crimson
        Me.lblBType.Location = New System.Drawing.Point(6, 19)
        Me.lblBType.Name = "lblBType"
        Me.lblBType.Size = New System.Drawing.Size(137, 77)
        Me.lblBType.TabIndex = 153
        Me.lblBType.Text = "AB+"
        Me.lblBType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBldQnt
        '
        Me.txtBldQnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldQnt.Location = New System.Drawing.Point(221, 73)
        Me.txtBldQnt.MaxLength = 5
        Me.txtBldQnt.Name = "txtBldQnt"
        Me.txtBldQnt.Size = New System.Drawing.Size(72, 21)
        Me.txtBldQnt.TabIndex = 2
        '
        'txtBType
        '
        Me.txtBType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBType.Location = New System.Drawing.Point(221, 19)
        Me.txtBType.Margin = New System.Windows.Forms.Padding(1)
        Me.txtBType.MaxLength = 4
        Me.txtBType.Name = "txtBType"
        Me.txtBType.Size = New System.Drawing.Size(72, 21)
        Me.txtBType.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(148, 73)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 21)
        Me.Label4.TabIndex = 150
        Me.Label4.Text = "성분제제"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(148, 19)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 21)
        Me.Label3.TabIndex = 149
        Me.Label3.Text = "혈액형"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(148, 47)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 21)
        Me.Label1.TabIndex = 144
        Me.Label1.Text = "혈액번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBldNm
        '
        Me.txtBldNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldNm.Location = New System.Drawing.Point(221, 47)
        Me.txtBldNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtBldNm.MaxLength = 10
        Me.txtBldNm.Name = "txtBldNm"
        Me.txtBldNm.Size = New System.Drawing.Size(72, 21)
        Me.txtBldNm.TabIndex = 1
        '
        'lblComNmd
        '
        Me.lblComNmd.BackColor = System.Drawing.Color.LightGray
        Me.lblComNmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComNmd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComNmd.ForeColor = System.Drawing.Color.Black
        Me.lblComNmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComNmd.Location = New System.Drawing.Point(294, 73)
        Me.lblComNmd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComNmd.Name = "lblComNmd"
        Me.lblComNmd.Size = New System.Drawing.Size(137, 21)
        Me.lblComNmd.TabIndex = 157
        Me.lblComNmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.spdList)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 94)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(953, 471)
        Me.GroupBox1.TabIndex = 158
        Me.GroupBox1.TabStop = False
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        'Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(6, 11)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(939, 454)
        Me.spdList.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(527, 73)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 21)
        Me.Label2.TabIndex = 159
        Me.Label2.Text = "구분"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboBldGbn
        '
        Me.cboBldGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBldGbn.FormattingEnabled = True
        Me.cboBldGbn.Items.AddRange(New Object() {"혈액원", "헌혈", "지정", "성분", "자가"})
        Me.cboBldGbn.Location = New System.Drawing.Point(600, 73)
        Me.cboBldGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.cboBldGbn.Name = "cboBldGbn"
        Me.cboBldGbn.Size = New System.Drawing.Size(75, 20)
        Me.cboBldGbn.TabIndex = 5
        '
        'txtRegNo
        '
        Me.txtRegNo.BackColor = System.Drawing.Color.White
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Location = New System.Drawing.Point(758, 73)
        Me.txtRegNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegNo.MaxLength = 9
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.ReadOnly = True
        Me.txtRegNo.Size = New System.Drawing.Size(83, 21)
        Me.txtRegNo.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label13.Location = New System.Drawing.Point(685, 73)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 21)
        Me.Label13.TabIndex = 162
        Me.Label13.Text = "등록번호"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatNm
        '
        Me.lblPatNm.BackColor = System.Drawing.Color.LightGray
        Me.lblPatNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPatNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatNm.ForeColor = System.Drawing.Color.Black
        Me.lblPatNm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPatNm.Location = New System.Drawing.Point(842, 73)
        Me.lblPatNm.Margin = New System.Windows.Forms.Padding(1)
        Me.lblPatNm.Name = "lblPatNm"
        Me.lblPatNm.Size = New System.Drawing.Size(80, 21)
        Me.lblPatNm.TabIndex = 163
        Me.lblPatNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComCd
        '
        Me.lblComCd.BackColor = System.Drawing.Color.LightGray
        Me.lblComCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComCd.ForeColor = System.Drawing.Color.Black
        Me.lblComCd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComCd.Location = New System.Drawing.Point(430, 19)
        Me.lblComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComCd.Name = "lblComCd"
        Me.lblComCd.Size = New System.Drawing.Size(43, 21)
        Me.lblComCd.TabIndex = 164
        Me.lblComCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComCd.Visible = False
        '
        'lblAvailDt
        '
        Me.lblAvailDt.BackColor = System.Drawing.Color.LightGray
        Me.lblAvailDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAvailDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAvailDt.ForeColor = System.Drawing.Color.Black
        Me.lblAvailDt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAvailDt.Location = New System.Drawing.Point(386, 19)
        Me.lblAvailDt.Margin = New System.Windows.Forms.Padding(1)
        Me.lblAvailDt.Name = "lblAvailDt"
        Me.lblAvailDt.Size = New System.Drawing.Size(43, 21)
        Me.lblAvailDt.TabIndex = 165
        Me.lblAvailDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAvailDt.Visible = False
        '
        'lblDonQnt
        '
        Me.lblDonQnt.BackColor = System.Drawing.Color.LightGray
        Me.lblDonQnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDonQnt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDonQnt.ForeColor = System.Drawing.Color.Black
        Me.lblDonQnt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDonQnt.Location = New System.Drawing.Point(432, 73)
        Me.lblDonQnt.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDonQnt.Name = "lblDonQnt"
        Me.lblDonQnt.Size = New System.Drawing.Size(59, 21)
        Me.lblDonQnt.TabIndex = 166
        Me.lblDonQnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FGB05_S02
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(966, 632)
        Me.Controls.Add(Me.lblDonQnt)
        Me.Controls.Add(Me.lblAvailDt)
        Me.Controls.Add(Me.lblComCd)
        Me.Controls.Add(Me.lblPatNm)
        Me.Controls.Add(Me.txtRegNo)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboBldGbn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblComNmd)
        Me.Controls.Add(Me.cboRH)
        Me.Controls.Add(Me.dtpIndt)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboBType)
        Me.Controls.Add(Me.dtpDonDt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblBType)
        Me.Controls.Add(Me.txtBldQnt)
        Me.Controls.Add(Me.txtBType)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBldNm)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGB05_S02"
        Me.Text = "일괄입고"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cboRH As System.Windows.Forms.ComboBox
    Friend WithEvents dtpIndt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboBType As System.Windows.Forms.ComboBox
    Friend WithEvents dtpDonDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblBType As System.Windows.Forms.Label
    Friend WithEvents txtBldQnt As System.Windows.Forms.TextBox
    Friend WithEvents txtBType As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBldNm As System.Windows.Forms.TextBox
    Friend WithEvents lblComNmd As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboBldGbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblPatNm As System.Windows.Forms.Label
    Friend WithEvents lblComCd As System.Windows.Forms.Label
    Friend WithEvents lblAvailDt As System.Windows.Forms.Label
    Friend WithEvents lblDonQnt As System.Windows.Forms.Label
End Class
