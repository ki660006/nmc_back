<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGFINAL_CMT2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGFINAL_CMT2))
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnReg = New System.Windows.Forms.Button
        Me.cboCmtCd = New System.Windows.Forms.ComboBox
        Me.txtCmtCont = New System.Windows.Forms.TextBox
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblBcNo = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 20)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "내용"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.Location = New System.Drawing.Point(243, 236)
        Me.btnReg.Margin = New System.Windows.Forms.Padding(1)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(87, 28)
        Me.btnReg.TabIndex = 34
        Me.btnReg.Text = "저장(F2)"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'cboCmtCd
        '
        Me.cboCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboCmtCd.FormattingEnabled = True
        Me.cboCmtCd.Location = New System.Drawing.Point(87, 126)
        Me.cboCmtCd.Name = "cboCmtCd"
        Me.cboCmtCd.Size = New System.Drawing.Size(332, 20)
        Me.cboCmtCd.TabIndex = 35
        '
        'txtCmtCont
        '
        Me.txtCmtCont.Location = New System.Drawing.Point(12, 149)
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(407, 74)
        Me.txtCmtCont.TabIndex = 36
        '
        'spdList
        '
        Me.spdList.Location = New System.Drawing.Point(12, 31)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(407, 77)
        Me.spdList.TabIndex = 37
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(12, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 21)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "검체번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcNo
        '
        Me.lblBcNo.BackColor = System.Drawing.Color.White
        Me.lblBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBcNo.Location = New System.Drawing.Point(87, 7)
        Me.lblBcNo.Name = "lblBcNo"
        Me.lblBcNo.Size = New System.Drawing.Size(332, 21)
        Me.lblBcNo.TabIndex = 39
        Me.lblBcNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.Location = New System.Drawing.Point(332, 236)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(87, 28)
        Me.btnExit.TabIndex = 40
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'FGDCMT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(432, 276)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblBcNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.spdList)
        Me.Controls.Add(Me.txtCmtCont)
        Me.Controls.Add(Me.cboCmtCd)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGDCMT"
        Me.Text = "최종보고 수정사유 입력"
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents cboCmtCd As System.Windows.Forms.ComboBox
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblBcNo As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
