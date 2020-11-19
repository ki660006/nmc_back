<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB10_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB10_S01))
        Me.Label98 = New System.Windows.Forms.Label
        Me.txtWorkGbn = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtReqNm = New System.Windows.Forms.TextBox
        Me.txtReqid = New System.Windows.Forms.TextBox
        Me.cboResn = New System.Windows.Forms.ComboBox
        Me.txtCmt = New System.Windows.Forms.TextBox
        Me.chkCost = New System.Windows.Forms.CheckBox
        Me.grbBottom = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnExe = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnPop = New System.Windows.Forms.Button
        Me.grbBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label98.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label98.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.White
        Me.Label98.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label98.Location = New System.Drawing.Point(3, 4)
        Me.Label98.Margin = New System.Windows.Forms.Padding(1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(130, 21)
        Me.Label98.TabIndex = 102
        Me.Label98.Text = "작  업    구  분"
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWorkGbn
        '
        Me.txtWorkGbn.Enabled = False
        Me.txtWorkGbn.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWorkGbn.ForeColor = System.Drawing.Color.Red
        Me.txtWorkGbn.Location = New System.Drawing.Point(134, 4)
        Me.txtWorkGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.txtWorkGbn.MaxLength = 8
        Me.txtWorkGbn.Name = "txtWorkGbn"
        Me.txtWorkGbn.Size = New System.Drawing.Size(204, 21)
        Me.txtWorkGbn.TabIndex = 104
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(3, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 21)
        Me.Label1.TabIndex = 105
        Me.Label1.Text = "반납/폐기 의뢰자"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(3, 48)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(130, 21)
        Me.Label2.TabIndex = 106
        Me.Label2.Text = "반납/폐기   사유"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtReqNm
        '
        Me.txtReqNm.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqNm.Location = New System.Drawing.Point(222, 26)
        Me.txtReqNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtReqNm.MaxLength = 50
        Me.txtReqNm.Name = "txtReqNm"
        Me.txtReqNm.ReadOnly = True
        Me.txtReqNm.Size = New System.Drawing.Size(116, 21)
        Me.txtReqNm.TabIndex = 190
        '
        'txtReqid
        '
        Me.txtReqid.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtReqid.Location = New System.Drawing.Point(134, 26)
        Me.txtReqid.Margin = New System.Windows.Forms.Padding(1)
        Me.txtReqid.MaxLength = 8
        Me.txtReqid.Name = "txtReqid"
        Me.txtReqid.Size = New System.Drawing.Size(65, 21)
        Me.txtReqid.TabIndex = 189
        '
        'cboResn
        '
        Me.cboResn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboResn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboResn.FormattingEnabled = True
        Me.cboResn.Location = New System.Drawing.Point(134, 48)
        Me.cboResn.Margin = New System.Windows.Forms.Padding(1)
        Me.cboResn.MaxDropDownItems = 20
        Me.cboResn.Name = "cboResn"
        Me.cboResn.Size = New System.Drawing.Size(297, 20)
        Me.cboResn.TabIndex = 192
        '
        'txtCmt
        '
        Me.txtCmt.Enabled = False
        Me.txtCmt.Location = New System.Drawing.Point(134, 70)
        Me.txtCmt.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCmt.MaxLength = 2000
        Me.txtCmt.Multiline = True
        Me.txtCmt.Name = "txtCmt"
        Me.txtCmt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCmt.Size = New System.Drawing.Size(297, 107)
        Me.txtCmt.TabIndex = 193
        '
        'chkCost
        '
        Me.chkCost.AutoSize = True
        Me.chkCost.Enabled = False
        Me.chkCost.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkCost.Location = New System.Drawing.Point(362, 31)
        Me.chkCost.Name = "chkCost"
        Me.chkCost.Size = New System.Drawing.Size(76, 16)
        Me.chkCost.TabIndex = 219
        Me.chkCost.Text = "환불적용"
        Me.chkCost.UseVisualStyleBackColor = True
        '
        'grbBottom
        '
        Me.grbBottom.Controls.Add(Me.btnExit)
        Me.grbBottom.Controls.Add(Me.btnExe)
        Me.grbBottom.Location = New System.Drawing.Point(3, 173)
        Me.grbBottom.Name = "grbBottom"
        Me.grbBottom.Size = New System.Drawing.Size(432, 41)
        Me.grbBottom.TabIndex = 220
        Me.grbBottom.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(335, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 23)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "취 소(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnExe
        '
        Me.btnExe.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExe.Location = New System.Drawing.Point(242, 13)
        Me.btnExe.Name = "btnExe"
        Me.btnExe.Size = New System.Drawing.Size(94, 23)
        Me.btnExe.TabIndex = 105
        Me.btnExe.Text = "실 행(F4)"
        Me.btnExe.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(3, 70)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(130, 21)
        Me.Label3.TabIndex = 221
        Me.Label3.Text = "C O M M E N T"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnPop
        '
        Me.btnPop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPop.Image = CType(resources.GetObject("btnPop.Image"), System.Drawing.Image)
        Me.btnPop.Location = New System.Drawing.Point(200, 26)
        Me.btnPop.Name = "btnPop"
        Me.btnPop.Size = New System.Drawing.Size(21, 21)
        Me.btnPop.TabIndex = 222
        Me.btnPop.UseVisualStyleBackColor = True
        '
        'FGB10_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(437, 216)
        Me.Controls.Add(Me.btnPop)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkCost)
        Me.Controls.Add(Me.txtCmt)
        Me.Controls.Add(Me.cboResn)
        Me.Controls.Add(Me.txtReqNm)
        Me.Controls.Add(Me.txtReqid)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtWorkGbn)
        Me.Controls.Add(Me.Label98)
        Me.Controls.Add(Me.grbBottom)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.KeyPreview = True
        Me.Name = "FGB10_S01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "혈액반납/폐기"
        Me.grbBottom.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents txtWorkGbn As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtReqNm As System.Windows.Forms.TextBox
    Friend WithEvents txtReqid As System.Windows.Forms.TextBox
    Friend WithEvents cboResn As System.Windows.Forms.ComboBox
    Friend WithEvents txtCmt As System.Windows.Forms.TextBox
    Friend WithEvents chkCost As System.Windows.Forms.CheckBox
    Friend WithEvents grbBottom As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnExe As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnPop As System.Windows.Forms.Button
End Class
