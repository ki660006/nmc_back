<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB06_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB06_S01))
        Me.cboTnsGbn = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.cboComCd = New System.Windows.Forms.ComboBox()
        Me.lblComcd = New System.Windows.Forms.Label()
        Me.txtPatNm = New System.Windows.Forms.TextBox()
        Me.btnPatPop = New System.Windows.Forms.Button()
        Me.txtRegno = New System.Windows.Forms.TextBox()
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboTnsGbn
        '
        Me.cboTnsGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTnsGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTnsGbn.FormattingEnabled = True
        Me.cboTnsGbn.Location = New System.Drawing.Point(371, 25)
        Me.cboTnsGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.cboTnsGbn.MaxDropDownItems = 20
        Me.cboTnsGbn.Name = "cboTnsGbn"
        Me.cboTnsGbn.Size = New System.Drawing.Size(204, 20)
        Me.cboTnsGbn.TabIndex = 198
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(290, 25)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 21)
        Me.Label6.TabIndex = 197
        Me.Label6.Text = "수혈구분"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label99.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label99.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label99.ForeColor = System.Drawing.Color.White
        Me.Label99.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label99.Location = New System.Drawing.Point(3, 25)
        Me.Label99.Margin = New System.Windows.Forms.Padding(1)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(80, 21)
        Me.Label99.TabIndex = 193
        Me.Label99.Text = "등록번호"
        Me.Label99.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboComCd
        '
        Me.cboComCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboComCd.FormattingEnabled = True
        Me.cboComCd.Location = New System.Drawing.Point(371, 3)
        Me.cboComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboComCd.MaxDropDownItems = 20
        Me.cboComCd.Name = "cboComCd"
        Me.cboComCd.Size = New System.Drawing.Size(204, 20)
        Me.cboComCd.TabIndex = 188
        '
        'lblComcd
        '
        Me.lblComcd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComcd.ForeColor = System.Drawing.Color.White
        Me.lblComcd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComcd.Location = New System.Drawing.Point(290, 3)
        Me.lblComcd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComcd.Name = "lblComcd"
        Me.lblComcd.Size = New System.Drawing.Size(80, 21)
        Me.lblComcd.TabIndex = 194
        Me.lblComcd.Text = "성분제제"
        Me.lblComcd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPatNm
        '
        Me.txtPatNm.BackColor = System.Drawing.SystemColors.Window
        Me.txtPatNm.Location = New System.Drawing.Point(180, 25)
        Me.txtPatNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtPatNm.MaxLength = 50
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.ReadOnly = True
        Me.txtPatNm.Size = New System.Drawing.Size(108, 21)
        Me.txtPatNm.TabIndex = 196
        '
        'btnPatPop
        '
        Me.btnPatPop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPatPop.Image = CType(resources.GetObject("btnPatPop.Image"), System.Drawing.Image)
        Me.btnPatPop.Location = New System.Drawing.Point(158, 25)
        Me.btnPatPop.Name = "btnPatPop"
        Me.btnPatPop.Size = New System.Drawing.Size(21, 21)
        Me.btnPatPop.TabIndex = 195
        Me.btnPatPop.UseVisualStyleBackColor = True
        '
        'txtRegno
        '
        Me.txtRegno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegno.Location = New System.Drawing.Point(84, 25)
        Me.txtRegno.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegno.MaxLength = 8
        Me.txtRegno.Name = "txtRegno"
        Me.txtRegno.Size = New System.Drawing.Size(73, 21)
        Me.txtRegno.TabIndex = 187
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(200, 3)
        Me.dtpDate1.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate1.TabIndex = 190
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.White
        Me.lblDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDate.Location = New System.Drawing.Point(3, 3)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(80, 21)
        Me.lblDate.TabIndex = 191
        Me.lblDate.Text = "처방일자"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(179, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 12)
        Me.Label4.TabIndex = 192
        Me.Label4.Text = "~"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(84, 3)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate0.TabIndex = 189
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(5, 50)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1264, 618)
        Me.Panel1.TabIndex = 199
        '
        'spdList
        '
        'Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1264, 618)
        Me.spdList.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(578, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(108, 44)
        Me.btnSearch.TabIndex = 200
        Me.btnSearch.Text = "조 회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Location = New System.Drawing.Point(1161, 3)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(108, 44)
        Me.btnExcel.TabIndex = 201
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'FGB06_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1272, 670)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cboTnsGbn)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label99)
        Me.Controls.Add(Me.cboComCd)
        Me.Controls.Add(Me.lblComcd)
        Me.Controls.Add(Me.txtPatNm)
        Me.Controls.Add(Me.btnPatPop)
        Me.Controls.Add(Me.txtRegno)
        Me.Controls.Add(Me.dtpDate1)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpDate0)
        Me.Name = "FGB06_S01"
        Me.Text = "수혈 처방 조회"
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cboTnsGbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents cboComCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblComcd As System.Windows.Forms.Label
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Friend WithEvents btnPatPop As System.Windows.Forms.Button
    Friend WithEvents txtRegno As System.Windows.Forms.TextBox
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
End Class
