<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGMULTILLINERST
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnReg = New System.Windows.Forms.Button
        Me.txtOrgRst = New System.Windows.Forms.TextBox
        Me.btnHelp_rst = New System.Windows.Forms.Button
        Me.ChkAdd = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.Location = New System.Drawing.Point(336, 232)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(87, 28)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.Location = New System.Drawing.Point(247, 232)
        Me.btnReg.Margin = New System.Windows.Forms.Padding(1)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(87, 28)
        Me.btnReg.TabIndex = 1
        Me.btnReg.Text = "저장(F2)"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'txtOrgRst
        '
        Me.txtOrgRst.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOrgRst.Location = New System.Drawing.Point(2, 3)
        Me.txtOrgRst.MaxLength = 900
        Me.txtOrgRst.Multiline = True
        Me.txtOrgRst.Name = "txtOrgRst"
        Me.txtOrgRst.Size = New System.Drawing.Size(422, 226)
        Me.txtOrgRst.TabIndex = 0
        '
        'btnHelp_rst
        '
        Me.btnHelp_rst.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHelp_rst.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnHelp_rst.Location = New System.Drawing.Point(2, 233)
        Me.btnHelp_rst.Margin = New System.Windows.Forms.Padding(1)
        Me.btnHelp_rst.Name = "btnHelp_rst"
        Me.btnHelp_rst.Size = New System.Drawing.Size(87, 28)
        Me.btnHelp_rst.TabIndex = 3
        Me.btnHelp_rst.Text = "코드보기"
        Me.btnHelp_rst.UseVisualStyleBackColor = True
        '
        'ChkAdd
        '
        Me.ChkAdd.AutoSize = True
        Me.ChkAdd.Checked = True
        Me.ChkAdd.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkAdd.Location = New System.Drawing.Point(93, 239)
        Me.ChkAdd.Name = "ChkAdd"
        Me.ChkAdd.Size = New System.Drawing.Size(72, 16)
        Me.ChkAdd.TabIndex = 4
        Me.ChkAdd.Text = "결과추가"
        Me.ChkAdd.UseVisualStyleBackColor = True
        '
        'FGMULTILLINERST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(427, 266)
        Me.Controls.Add(Me.ChkAdd)
        Me.Controls.Add(Me.btnHelp_rst)
        Me.Controls.Add(Me.txtOrgRst)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnReg)
        Me.KeyPreview = True
        Me.Name = "FGMULTILLINERST"
        Me.Text = "멀티라인 결과입력"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents txtOrgRst As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp_rst As System.Windows.Forms.Button
    Friend WithEvents ChkAdd As System.Windows.Forms.CheckBox
End Class
