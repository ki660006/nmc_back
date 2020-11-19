<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGDRUG
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGDRUG))
        Me.spdDrug = New AxFPSpreadADO.AxfpSpread
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnQuery = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboDCom = New System.Windows.Forms.ComboBox
        CType(Me.spdDrug, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spdDrug
        '
        Me.spdDrug.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdDrug.DataSource = Nothing
        Me.spdDrug.Location = New System.Drawing.Point(4, 50)
        Me.spdDrug.Name = "spdDrug"
        Me.spdDrug.OcxState = CType(resources.GetObject("spdDrug.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDrug.Size = New System.Drawing.Size(660, 212)
        Me.spdDrug.TabIndex = 0
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.Location = New System.Drawing.Point(577, 6)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(77, 42)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "닫기 (Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnQuery
        '
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.Location = New System.Drawing.Point(494, 6)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(77, 42)
        Me.btnQuery.TabIndex = 2
        Me.btnQuery.Text = "조회"
        Me.btnQuery.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(5, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 21)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "처방일자"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(188, 32)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(14, 12)
        Me.Label32.TabIndex = 21
        Me.Label32.Text = "~"
        '
        'dtpDateE
        '
        Me.dtpDateE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(206, 26)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateE.TabIndex = 20
        Me.dtpDateE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'dtpDateS
        '
        Me.dtpDateS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(92, 26)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateS.TabIndex = 19
        Me.dtpDateS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(5, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 20)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "성분명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboDCom
        '
        Me.cboDCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDCom.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDCom.FormattingEnabled = True
        Me.cboDCom.Location = New System.Drawing.Point(92, 5)
        Me.cboDCom.Name = "cboDCom"
        Me.cboDCom.Size = New System.Drawing.Size(359, 20)
        Me.cboDCom.TabIndex = 23
        '
        'FGDRUG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(669, 266)
        Me.Controls.Add(Me.cboDCom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.dtpDateE)
        Me.Controls.Add(Me.dtpDateS)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnQuery)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.spdDrug)
        Me.KeyPreview = True
        Me.Name = "FGDRUG"
        Me.Text = "투약정보"
        CType(Me.spdDrug, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents spdDrug As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboDCom As System.Windows.Forms.ComboBox
End Class
