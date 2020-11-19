<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FPOPUPCOMMENT
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblRegno = New System.Windows.Forms.Label
        Me.cboCmtcd = New System.Windows.Forms.ComboBox
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnDel = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 21)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "특이사항"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegno
        '
        Me.lblRegno.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRegno.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegno.ForeColor = System.Drawing.Color.White
        Me.lblRegno.Location = New System.Drawing.Point(6, 3)
        Me.lblRegno.Name = "lblRegno"
        Me.lblRegno.Size = New System.Drawing.Size(79, 21)
        Me.lblRegno.TabIndex = 35
        Me.lblRegno.Text = "등록번호"
        Me.lblRegno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCmtcd
        '
        Me.cboCmtcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCmtcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboCmtcd.Items.AddRange(New Object() {"[S] Single", "[P] Parent Of Sub.", "[C] Child Of Sub.", "[B] Battery", "[G] Group"})
        Me.cboCmtcd.Location = New System.Drawing.Point(86, 25)
        Me.cboCmtcd.Margin = New System.Windows.Forms.Padding(0)
        Me.cboCmtcd.Name = "cboCmtcd"
        Me.cboCmtcd.Size = New System.Drawing.Size(186, 20)
        Me.cboCmtcd.TabIndex = 34
        Me.cboCmtcd.Tag = "TCDGBN_01"
        '
        'txtRegNo
        '
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegNo.Location = New System.Drawing.Point(86, 3)
        Me.txtRegNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegNo.MaxLength = 10
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(127, 21)
        Me.txtRegNo.TabIndex = 33
        Me.txtRegNo.Tag = "TORDCD"
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(6, 48)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(267, 186)
        Me.txtComment.TabIndex = 37
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnDel)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 238)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(279, 35)
        Me.Panel1.TabIndex = 38
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDel.Location = New System.Drawing.Point(116, 3)
        Me.btnDel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(79, 26)
        Me.btnDel.TabIndex = 28
        Me.btnDel.Text = "삭제"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSave.Location = New System.Drawing.Point(37, 3)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(0)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(79, 26)
        Me.btnSave.TabIndex = 27
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.Location = New System.Drawing.Point(195, 3)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(79, 26)
        Me.btnExit.TabIndex = 26
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'FPOPUPCOMMENT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(279, 273)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblRegno)
        Me.Controls.Add(Me.cboCmtcd)
        Me.Controls.Add(Me.txtRegNo)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FPOPUPCOMMENT"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "FPOPUPCOMMENT"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblRegno As System.Windows.Forms.Label
    Friend WithEvents cboCmtcd As System.Windows.Forms.ComboBox
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
