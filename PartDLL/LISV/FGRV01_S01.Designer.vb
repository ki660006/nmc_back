<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGRV01_S01
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
        Me.grpNo = New System.Windows.Forms.GroupBox
        Me.lblNo = New System.Windows.Forms.Label
        Me.txtUsrId = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCfnCont = New System.Windows.Forms.TextBox
        Me.txtCont = New System.Windows.Forms.TextBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.lblGbn = New System.Windows.Forms.Label
        Me.lblRegDt = New System.Windows.Forms.Label
        Me.lblRegNm = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblBcNo = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblOrdDt = New System.Windows.Forms.Label
        Me.lblDoctorNm = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblDptWard = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.grpNo.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpNo
        '
        Me.grpNo.Controls.Add(Me.lblDptWard)
        Me.grpNo.Controls.Add(Me.Label11)
        Me.grpNo.Controls.Add(Me.lblDoctorNm)
        Me.grpNo.Controls.Add(Me.Label9)
        Me.grpNo.Controls.Add(Me.lblOrdDt)
        Me.grpNo.Controls.Add(Me.Label4)
        Me.grpNo.Controls.Add(Me.lblBcNo)
        Me.grpNo.Controls.Add(Me.Label6)
        Me.grpNo.Controls.Add(Me.lblRegNm)
        Me.grpNo.Controls.Add(Me.Label3)
        Me.grpNo.Controls.Add(Me.lblRegDt)
        Me.grpNo.Controls.Add(Me.lblGbn)
        Me.grpNo.Controls.Add(Me.txtCont)
        Me.grpNo.Location = New System.Drawing.Point(2, 38)
        Me.grpNo.Name = "grpNo"
        Me.grpNo.Size = New System.Drawing.Size(577, 326)
        Me.grpNo.TabIndex = 2
        Me.grpNo.TabStop = False
        '
        'lblNo
        '
        Me.lblNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblNo.ForeColor = System.Drawing.Color.White
        Me.lblNo.Location = New System.Drawing.Point(10, 11)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.Size = New System.Drawing.Size(78, 22)
        Me.lblNo.TabIndex = 0
        Me.lblNo.Text = "확인자"
        Me.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUsrId
        '
        Me.txtUsrId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtUsrId.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtUsrId.Location = New System.Drawing.Point(89, 11)
        Me.txtUsrId.MaxLength = 10
        Me.txtUsrId.Name = "txtUsrId"
        Me.txtUsrId.Size = New System.Drawing.Size(58, 21)
        Me.txtUsrId.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(153, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 22)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "확인내용"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCfnCont
        '
        Me.txtCfnCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCfnCont.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCfnCont.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCfnCont.Location = New System.Drawing.Point(232, 11)
        Me.txtCfnCont.MaxLength = 0
        Me.txtCfnCont.Name = "txtCfnCont"
        Me.txtCfnCont.Size = New System.Drawing.Size(267, 21)
        Me.txtCfnCont.TabIndex = 3
        '
        'txtCont
        '
        Me.txtCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCont.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCont.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCont.Location = New System.Drawing.Point(5, 60)
        Me.txtCont.MaxLength = 8
        Me.txtCont.Multiline = True
        Me.txtCont.Name = "txtCont"
        Me.txtCont.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCont.Size = New System.Drawing.Size(566, 260)
        Me.txtCont.TabIndex = 12
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Location = New System.Drawing.Point(503, 5)
        Me.btnOk.Margin = New System.Windows.Forms.Padding(1)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(76, 32)
        Me.btnOk.TabIndex = 9
        Me.btnOk.Text = "확인"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'lblGbn
        '
        Me.lblGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGbn.ForeColor = System.Drawing.Color.Black
        Me.lblGbn.Location = New System.Drawing.Point(5, 13)
        Me.lblGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblGbn.Name = "lblGbn"
        Me.lblGbn.Size = New System.Drawing.Size(83, 21)
        Me.lblGbn.TabIndex = 165
        Me.lblGbn.Text = "통보일시"
        Me.lblGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegDt
        '
        Me.lblRegDt.BackColor = System.Drawing.Color.White
        Me.lblRegDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRegDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegDt.Location = New System.Drawing.Point(89, 13)
        Me.lblRegDt.Name = "lblRegDt"
        Me.lblRegDt.Size = New System.Drawing.Size(117, 21)
        Me.lblRegDt.TabIndex = 166
        Me.lblRegDt.Text = "0000-00-00 00:00"
        Me.lblRegDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNm
        '
        Me.lblRegNm.BackColor = System.Drawing.Color.White
        Me.lblRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegNm.Location = New System.Drawing.Point(292, 13)
        Me.lblRegNm.Name = "lblRegNm"
        Me.lblRegNm.Size = New System.Drawing.Size(69, 21)
        Me.lblRegNm.TabIndex = 168
        Me.lblRegNm.Text = "0000-00-00 00:00"
        Me.lblRegNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(208, 13)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 21)
        Me.Label3.TabIndex = 167
        Me.Label3.Text = "통 보 자"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(363, 13)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 21)
        Me.Label6.TabIndex = 171
        Me.Label6.Text = "검체번호"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcNo
        '
        Me.lblBcNo.BackColor = System.Drawing.Color.White
        Me.lblBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBcNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBcNo.Location = New System.Drawing.Point(447, 13)
        Me.lblBcNo.Name = "lblBcNo"
        Me.lblBcNo.Size = New System.Drawing.Size(125, 21)
        Me.lblBcNo.TabIndex = 172
        Me.lblBcNo.Text = "00000000-00-0000-0"
        Me.lblBcNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(5, 36)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 21)
        Me.Label4.TabIndex = 173
        Me.Label4.Text = "처방일시"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.White
        Me.lblOrdDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOrdDt.Location = New System.Drawing.Point(89, 36)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(117, 21)
        Me.lblOrdDt.TabIndex = 174
        Me.lblOrdDt.Text = "0000-00-00 00:00"
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDoctorNm
        '
        Me.lblDoctorNm.BackColor = System.Drawing.Color.White
        Me.lblDoctorNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDoctorNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctorNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoctorNm.Location = New System.Drawing.Point(292, 36)
        Me.lblDoctorNm.Name = "lblDoctorNm"
        Me.lblDoctorNm.Size = New System.Drawing.Size(69, 21)
        Me.lblDoctorNm.TabIndex = 176
        Me.lblDoctorNm.Text = "0000-00-00 00:00"
        Me.lblDoctorNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(208, 36)
        Me.Label9.Margin = New System.Windows.Forms.Padding(1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 21)
        Me.Label9.TabIndex = 175
        Me.Label9.Text = "처 방 의"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDptWard
        '
        Me.lblDptWard.BackColor = System.Drawing.Color.White
        Me.lblDptWard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDptWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDptWard.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDptWard.Location = New System.Drawing.Point(447, 36)
        Me.lblDptWard.Name = "lblDptWard"
        Me.lblDptWard.Size = New System.Drawing.Size(125, 21)
        Me.lblDptWard.TabIndex = 178
        Me.lblDptWard.Text = "00000000-00-0000-0"
        Me.lblDptWard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(363, 36)
        Me.Label11.Margin = New System.Windows.Forms.Padding(1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 21)
        Me.Label11.TabIndex = 177
        Me.Label11.Text = "진료과/병동"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGRV01_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 367)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCfnCont)
        Me.Controls.Add(Me.lblNo)
        Me.Controls.Add(Me.txtUsrId)
        Me.Controls.Add(Me.grpNo)
        Me.Name = "FGRV01_S01"
        Me.Text = "특이결과 전달사항"
        Me.grpNo.ResumeLayout(False)
        Me.grpNo.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpNo As System.Windows.Forms.GroupBox
    Friend WithEvents lblNo As System.Windows.Forms.Label
    Friend WithEvents txtUsrId As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCfnCont As System.Windows.Forms.TextBox
    Friend WithEvents txtCont As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents lblGbn As System.Windows.Forms.Label
    Friend WithEvents lblRegNm As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblRegDt As System.Windows.Forms.Label
    Friend WithEvents lblDptWard As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblDoctorNm As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblBcNo As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
