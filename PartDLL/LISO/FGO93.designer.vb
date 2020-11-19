<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGO93
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
        Me.txtCd = New System.Windows.Forms.TextBox
        Me.lblCd = New System.Windows.Forms.Label
        Me.txtNm = New System.Windows.Forms.TextBox
        Me.lblNm = New System.Windows.Forms.Label
        Me.txtUseDt = New System.Windows.Forms.TextBox
        Me.lblUseDt = New System.Windows.Forms.Label
        Me.lblUseDtA = New System.Windows.Forms.Label
        Me.lblArrow = New System.Windows.Forms.Label
        Me.mtbUseDtA = New System.Windows.Forms.MaskedTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnDelCd = New System.Windows.Forms.Button
        Me.btnEditUseDt = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtCd
        '
        Me.txtCd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCd.Location = New System.Drawing.Point(75, 9)
        Me.txtCd.Name = "txtCd"
        Me.txtCd.ReadOnly = True
        Me.txtCd.Size = New System.Drawing.Size(100, 21)
        Me.txtCd.TabIndex = 4
        Me.txtCd.TabStop = False
        Me.txtCd.Tag = ""
        '
        'lblCd
        '
        Me.lblCd.BackColor = System.Drawing.Color.Navy
        Me.lblCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCd.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblCd.Location = New System.Drawing.Point(10, 9)
        Me.lblCd.Name = "lblCd"
        Me.lblCd.Size = New System.Drawing.Size(63, 20)
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
        Me.txtNm.Location = New System.Drawing.Point(75, 38)
        Me.txtNm.Name = "txtNm"
        Me.txtNm.ReadOnly = True
        Me.txtNm.Size = New System.Drawing.Size(397, 21)
        Me.txtNm.TabIndex = 6
        Me.txtNm.TabStop = False
        Me.txtNm.Tag = ""
        '
        'lblNm
        '
        Me.lblNm.BackColor = System.Drawing.Color.Navy
        Me.lblNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblNm.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblNm.Location = New System.Drawing.Point(10, 38)
        Me.lblNm.Name = "lblNm"
        Me.lblNm.Size = New System.Drawing.Size(63, 20)
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
        Me.txtUseDt.Location = New System.Drawing.Point(75, 83)
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
        Me.lblUseDt.BackColor = System.Drawing.Color.Navy
        Me.lblUseDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUseDt.ForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblUseDt.Location = New System.Drawing.Point(10, 83)
        Me.lblUseDt.Name = "lblUseDt"
        Me.lblUseDt.Size = New System.Drawing.Size(63, 20)
        Me.lblUseDt.TabIndex = 7
        Me.lblUseDt.Tag = ""
        Me.lblUseDt.Text = "사용일시"
        Me.lblUseDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUseDtA
        '
        Me.lblUseDtA.BackColor = System.Drawing.Color.SlateBlue
        Me.lblUseDtA.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUseDtA.ForeColor = System.Drawing.Color.White
        Me.lblUseDtA.Location = New System.Drawing.Point(286, 84)
        Me.lblUseDtA.Name = "lblUseDtA"
        Me.lblUseDtA.Size = New System.Drawing.Size(63, 20)
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
        'mtbUseDtA
        '
        Me.mtbUseDtA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.mtbUseDtA.Location = New System.Drawing.Point(351, 84)
        Me.mtbUseDtA.Mask = "0000-00-00 00:00:00"
        Me.mtbUseDtA.Name = "mtbUseDtA"
        Me.mtbUseDtA.Size = New System.Drawing.Size(121, 21)
        Me.mtbUseDtA.TabIndex = 12
        Me.mtbUseDtA.Text = "20000101123456"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(10, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "변경 전"
        Me.Label1.UseCompatibleTextRendering = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(286, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "변경 후"
        Me.Label2.UseCompatibleTextRendering = True
        '
        'btnDelCd
        '
        Me.btnDelCd.Location = New System.Drawing.Point(12, 121)
        Me.btnDelCd.Name = "btnDelCd"
        Me.btnDelCd.Size = New System.Drawing.Size(103, 31)
        Me.btnDelCd.TabIndex = 15
        Me.btnDelCd.Text = "코드 삭제"
        Me.btnDelCd.UseVisualStyleBackColor = True
        '
        'btnEditUseDt
        '
        Me.btnEditUseDt.Location = New System.Drawing.Point(288, 121)
        Me.btnEditUseDt.Name = "btnEditUseDt"
        Me.btnEditUseDt.Size = New System.Drawing.Size(103, 31)
        Me.btnEditUseDt.TabIndex = 16
        Me.btnEditUseDt.Text = "사용일시 수정"
        Me.btnEditUseDt.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(397, 121)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(76, 31)
        Me.btnClose.TabIndex = 17
        Me.btnClose.Text = "닫기"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'FGO93
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(484, 164)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnEditUseDt)
        Me.Controls.Add(Me.btnDelCd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.mtbUseDtA)
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
        Me.Name = "FGO93"
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
    Friend WithEvents mtbUseDtA As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDelCd As System.Windows.Forms.Button
    Friend WithEvents btnEditUseDt As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
