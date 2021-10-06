<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPST_PBS_NMC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_PBS_NMC))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRbc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtOther_r = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNrc = New System.Windows.Forms.TextBox()
        Me.txtOther_w = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtWbc = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOther_p = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPlt = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtOpinion = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnHelp_r = New System.Windows.Forms.Button()
        Me.btnHelp_or = New System.Windows.Forms.Button()
        Me.btnHelp_w = New System.Windows.Forms.Button()
        Me.btnHelp_ow = New System.Windows.Forms.Button()
        Me.btnHelp_p = New System.Windows.Forms.Button()
        Me.btnHelp_op = New System.Windows.Forms.Button()
        Me.btnHelp_opin = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(521, 543)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(93, 36)
        Me.btnClose.TabIndex = 83
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(422, 543)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 36)
        Me.btnSave.TabIndex = 82
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 84
        Me.Label1.Text = "RED BLOOD CELLS"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(29, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 12)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "Size an Stainability"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(29, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "RBC :"
        '
        'txtRbc
        '
        Me.txtRbc.Location = New System.Drawing.Point(93, 56)
        Me.txtRbc.Name = "txtRbc"
        Me.txtRbc.Size = New System.Drawing.Size(475, 21)
        Me.txtRbc.TabIndex = 87
        Me.txtRbc.Tag = "LH31104"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(29, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 88
        Me.Label4.Text = "Others:"
        '
        'txtOther_r
        '
        Me.txtOther_r.Location = New System.Drawing.Point(93, 79)
        Me.txtOther_r.Name = "txtOther_r"
        Me.txtOther_r.Size = New System.Drawing.Size(475, 21)
        Me.txtOther_r.TabIndex = 89
        Me.txtOther_r.Tag = "LH31101"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(263, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(303, 13)
        Me.Label5.TabIndex = 90
        Me.Label5.Text = "Nucleated red cells :        /100 WBC"
        '
        'txtNrc
        '
        Me.txtNrc.Location = New System.Drawing.Point(443, 102)
        Me.txtNrc.Name = "txtNrc"
        Me.txtNrc.Size = New System.Drawing.Size(49, 21)
        Me.txtNrc.TabIndex = 91
        Me.txtNrc.Tag = "LH31102"
        '
        'txtOther_w
        '
        Me.txtOther_w.Location = New System.Drawing.Point(93, 171)
        Me.txtOther_w.Name = "txtOther_w"
        Me.txtOther_w.Size = New System.Drawing.Size(475, 21)
        Me.txtOther_w.TabIndex = 97
        Me.txtOther_w.Tag = "LH31103"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(29, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(63, 13)
        Me.Label6.TabIndex = 96
        Me.Label6.Text = "Others:"
        '
        'txtWbc
        '
        Me.txtWbc.Location = New System.Drawing.Point(93, 148)
        Me.txtWbc.Name = "txtWbc"
        Me.txtWbc.Size = New System.Drawing.Size(475, 21)
        Me.txtWbc.TabIndex = 95
        Me.txtWbc.Tag = "LH31105"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(29, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 13)
        Me.Label7.TabIndex = 94
        Me.Label7.Text = "WBC :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(12, 129)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(143, 13)
        Me.Label9.TabIndex = 92
        Me.Label9.Text = "WHiTE BLOOD CELLS"
        '
        'txtOther_p
        '
        Me.txtOther_p.Location = New System.Drawing.Point(93, 252)
        Me.txtOther_p.Name = "txtOther_p"
        Me.txtOther_p.Size = New System.Drawing.Size(475, 21)
        Me.txtOther_p.TabIndex = 102
        Me.txtOther_p.Tag = "LH31107"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(29, 260)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 101
        Me.Label8.Text = "Others:"
        '
        'txtPlt
        '
        Me.txtPlt.Location = New System.Drawing.Point(93, 229)
        Me.txtPlt.Name = "txtPlt"
        Me.txtPlt.Size = New System.Drawing.Size(475, 21)
        Me.txtPlt.TabIndex = 100
        Me.txtPlt.Tag = "LH31106"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(29, 236)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 13)
        Me.Label10.TabIndex = 99
        Me.Label10.Text = "PLT :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(12, 210)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 13)
        Me.Label11.TabIndex = 98
        Me.Label11.Text = "PLATELETS"
        '
        'txtOpinion
        '
        Me.txtOpinion.Location = New System.Drawing.Point(93, 309)
        Me.txtOpinion.Multiline = True
        Me.txtOpinion.Name = "txtOpinion"
        Me.txtOpinion.Size = New System.Drawing.Size(501, 216)
        Me.txtOpinion.TabIndex = 105
        Me.txtOpinion.Tag = "LH31108"
        Me.txtOpinion.Text = " "
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(16, 313)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 34)
        Me.Label13.TabIndex = 104
        Me.Label13.Text = "(Blood    Protozoa)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(12, 294)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(63, 13)
        Me.Label14.TabIndex = 103
        Me.Label14.Text = "OPINION"
        '
        'btnHelp_r
        '
        Me.btnHelp_r.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_r.Image = CType(resources.GetObject("btnHelp_r.Image"), System.Drawing.Image)
        Me.btnHelp_r.Location = New System.Drawing.Point(569, 56)
        Me.btnHelp_r.Name = "btnHelp_r"
        Me.btnHelp_r.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_r.TabIndex = 106
        Me.btnHelp_r.UseVisualStyleBackColor = True
        '
        'btnHelp_or
        '
        Me.btnHelp_or.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_or.Image = CType(resources.GetObject("btnHelp_or.Image"), System.Drawing.Image)
        Me.btnHelp_or.Location = New System.Drawing.Point(569, 79)
        Me.btnHelp_or.Name = "btnHelp_or"
        Me.btnHelp_or.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_or.TabIndex = 107
        Me.btnHelp_or.UseVisualStyleBackColor = True
        '
        'btnHelp_w
        '
        Me.btnHelp_w.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_w.Image = CType(resources.GetObject("btnHelp_w.Image"), System.Drawing.Image)
        Me.btnHelp_w.Location = New System.Drawing.Point(569, 148)
        Me.btnHelp_w.Name = "btnHelp_w"
        Me.btnHelp_w.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_w.TabIndex = 108
        Me.btnHelp_w.UseVisualStyleBackColor = True
        '
        'btnHelp_ow
        '
        Me.btnHelp_ow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_ow.Image = CType(resources.GetObject("btnHelp_ow.Image"), System.Drawing.Image)
        Me.btnHelp_ow.Location = New System.Drawing.Point(569, 171)
        Me.btnHelp_ow.Name = "btnHelp_ow"
        Me.btnHelp_ow.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_ow.TabIndex = 109
        Me.btnHelp_ow.UseVisualStyleBackColor = True
        '
        'btnHelp_p
        '
        Me.btnHelp_p.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_p.Image = CType(resources.GetObject("btnHelp_p.Image"), System.Drawing.Image)
        Me.btnHelp_p.Location = New System.Drawing.Point(569, 229)
        Me.btnHelp_p.Name = "btnHelp_p"
        Me.btnHelp_p.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_p.TabIndex = 110
        Me.btnHelp_p.UseVisualStyleBackColor = True
        '
        'btnHelp_op
        '
        Me.btnHelp_op.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_op.Image = CType(resources.GetObject("btnHelp_op.Image"), System.Drawing.Image)
        Me.btnHelp_op.Location = New System.Drawing.Point(569, 252)
        Me.btnHelp_op.Name = "btnHelp_op"
        Me.btnHelp_op.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_op.TabIndex = 111
        Me.btnHelp_op.UseVisualStyleBackColor = True
        '
        'btnHelp_opin
        '
        Me.btnHelp_opin.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_opin.Image = CType(resources.GetObject("btnHelp_opin.Image"), System.Drawing.Image)
        Me.btnHelp_opin.Location = New System.Drawing.Point(93, 287)
        Me.btnHelp_opin.Name = "btnHelp_opin"
        Me.btnHelp_opin.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_opin.TabIndex = 112
        Me.btnHelp_opin.UseVisualStyleBackColor = True
        '
        'FGPOPUPST_PBS_NMC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(627, 591)
        Me.Controls.Add(Me.btnHelp_opin)
        Me.Controls.Add(Me.btnHelp_op)
        Me.Controls.Add(Me.btnHelp_p)
        Me.Controls.Add(Me.btnHelp_ow)
        Me.Controls.Add(Me.btnHelp_w)
        Me.Controls.Add(Me.btnHelp_or)
        Me.Controls.Add(Me.btnHelp_r)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtOpinion)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtOther_p)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtPlt)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtOther_w)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtWbc)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtNrc)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtOther_r)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtRbc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGPOPUPST_PBS_NMC"
        Me.Text = "특수검사 모듈 (PBS)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRbc As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOther_r As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNrc As System.Windows.Forms.TextBox
    Friend WithEvents txtOther_w As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtWbc As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtOther_p As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPlt As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtOpinion As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnHelp_r As System.Windows.Forms.Button
    Friend WithEvents btnHelp_or As System.Windows.Forms.Button
    Friend WithEvents btnHelp_w As System.Windows.Forms.Button
    Friend WithEvents btnHelp_ow As System.Windows.Forms.Button
    Friend WithEvents btnHelp_p As System.Windows.Forms.Button
    Friend WithEvents btnHelp_op As System.Windows.Forms.Button
    Friend WithEvents btnHelp_opin As System.Windows.Forms.Button
End Class
