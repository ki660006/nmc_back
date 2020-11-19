<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCSMLOGIN01
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtUsrId = New System.Windows.Forms.TextBox
        Me.txtCsmPw = New System.Windows.Forms.TextBox
        Me.txtCsmDn = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtUsr_csm = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "사용자 식별명"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "전자서명비밀번호"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(102, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "사용자 인증서 DN"
        '
        'txtUsrId
        '
        Me.txtUsrId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrId.Location = New System.Drawing.Point(122, 17)
        Me.txtUsrId.Name = "txtUsrId"
        Me.txtUsrId.Size = New System.Drawing.Size(200, 21)
        Me.txtUsrId.TabIndex = 0
        Me.txtUsrId.Text = "1234"
        '
        'txtCsmPw
        '
        Me.txtCsmPw.BackColor = System.Drawing.Color.White
        Me.txtCsmPw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCsmPw.Location = New System.Drawing.Point(122, 44)
        Me.txtCsmPw.Name = "txtCsmPw"
        Me.txtCsmPw.ReadOnly = True
        Me.txtCsmPw.Size = New System.Drawing.Size(200, 21)
        Me.txtCsmPw.TabIndex = 1
        Me.txtCsmPw.TabStop = False
        '
        'txtCsmDn
        '
        Me.txtCsmDn.BackColor = System.Drawing.Color.White
        Me.txtCsmDn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCsmDn.Location = New System.Drawing.Point(122, 71)
        Me.txtCsmDn.Name = "txtCsmDn"
        Me.txtCsmDn.Size = New System.Drawing.Size(200, 21)
        Me.txtCsmDn.TabIndex = 2
        Me.txtCsmDn.UseSystemPasswordChar = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(125, 126)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(100, 24)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "확인"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(225, 126)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 24)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "취소(Esc)"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtUsr_csm
        '
        Me.txtUsr_csm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsr_csm.Location = New System.Drawing.Point(122, 98)
        Me.txtUsr_csm.Name = "txtUsr_csm"
        Me.txtUsr_csm.Size = New System.Drawing.Size(200, 21)
        Me.txtUsr_csm.TabIndex = 3
        Me.txtUsr_csm.TabStop = False
        Me.txtUsr_csm.Visible = False
        '
        'FGCSMLOGIN01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 167)
        Me.Controls.Add(Me.txtUsr_csm)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtCsmDn)
        Me.Controls.Add(Me.txtCsmPw)
        Me.Controls.Add(Me.txtUsrId)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Name = "FGCSMLOGIN01"
        Me.Text = "인증서 확인"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtUsrId As System.Windows.Forms.TextBox
    Friend WithEvents txtCsmPw As System.Windows.Forms.TextBox
    Friend WithEvents txtCsmDn As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtUsr_csm As System.Windows.Forms.TextBox

End Class
