<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS10_S01
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
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUsrId = New System.Windows.Forms.TextBox
        Me.txtUsrNm = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboCfmcont = New System.Windows.Forms.ComboBox
        Me.txtCfmCont = New System.Windows.Forms.TextBox
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnOk)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 223)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(464, 34)
        Me.pnlBottom.TabIndex = 127
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(375, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(81, 23)
        Me.btnExit.TabIndex = 187
        Me.btnExit.Text = "취 소(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(295, 4)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(81, 23)
        Me.btnOk.TabIndex = 186
        Me.btnOk.Text = "확 인"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(10, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 21)
        Me.Label2.TabIndex = 202
        Me.Label2.Text = "확인자"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUsrId
        '
        Me.txtUsrId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrId.Location = New System.Drawing.Point(80, 9)
        Me.txtUsrId.Margin = New System.Windows.Forms.Padding(0)
        Me.txtUsrId.MaxLength = 10
        Me.txtUsrId.Name = "txtUsrId"
        Me.txtUsrId.Size = New System.Drawing.Size(78, 21)
        Me.txtUsrId.TabIndex = 203
        '
        'txtUsrNm
        '
        Me.txtUsrNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtUsrNm.Location = New System.Drawing.Point(159, 9)
        Me.txtUsrNm.Name = "txtUsrNm"
        Me.txtUsrNm.ReadOnly = True
        Me.txtUsrNm.Size = New System.Drawing.Size(118, 21)
        Me.txtUsrNm.TabIndex = 204
        Me.txtUsrNm.TabStop = False
        Me.txtUsrNm.Tag = "TNMD"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(10, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 21)
        Me.Label1.TabIndex = 205
        Me.Label1.Text = "조치사항"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCfmcont
        '
        Me.cboCfmcont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCfmcont.FormattingEnabled = True
        Me.cboCfmcont.Location = New System.Drawing.Point(80, 32)
        Me.cboCfmcont.Name = "cboCfmcont"
        Me.cboCfmcont.Size = New System.Drawing.Size(372, 20)
        Me.cboCfmcont.TabIndex = 206
        '
        'txtCfmCont
        '
        Me.txtCfmCont.Location = New System.Drawing.Point(10, 55)
        Me.txtCfmCont.Multiline = True
        Me.txtCfmCont.Name = "txtCfmCont"
        Me.txtCfmCont.Size = New System.Drawing.Size(442, 156)
        Me.txtCfmCont.TabIndex = 207
        '
        'FGS10_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(464, 257)
        Me.Controls.Add(Me.txtCfmCont)
        Me.Controls.Add(Me.cboCfmcont)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtUsrNm)
        Me.Controls.Add(Me.txtUsrId)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGS10_S01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "조치내용 입력"
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUsrId As System.Windows.Forms.TextBox
    Friend WithEvents txtUsrNm As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboCfmcont As System.Windows.Forms.ComboBox
    Friend WithEvents txtCfmCont As System.Windows.Forms.TextBox
End Class
