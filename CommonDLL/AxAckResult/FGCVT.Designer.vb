<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCVT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCVT))
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.AxfpSpread1 = New AxFPSpreadADO.AxfpSpread
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblTclsCd = New System.Windows.Forms.Label
        Me.lblTnmd = New System.Windows.Forms.Label
        Me.lblRst = New System.Windows.Forms.Label
        Me.lblCvtForm = New System.Windows.Forms.Label
        Me.lblMemo = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        CType(Me.AxfpSpread1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 20)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "검사명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(12, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 20)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "결과값"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(12, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 20)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "관련검사"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AxfpSpread1
        '
        Me.AxfpSpread1.Location = New System.Drawing.Point(12, 73)
        Me.AxfpSpread1.Name = "AxfpSpread1"
        Me.AxfpSpread1.OcxState = CType(resources.GetObject("AxfpSpread1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxfpSpread1.Size = New System.Drawing.Size(412, 211)
        Me.AxfpSpread1.TabIndex = 26
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(12, 292)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 20)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "계산식"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTclsCd
        '
        Me.lblTclsCd.BackColor = System.Drawing.Color.White
        Me.lblTclsCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTclsCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTclsCd.Location = New System.Drawing.Point(99, 9)
        Me.lblTclsCd.Name = "lblTclsCd"
        Me.lblTclsCd.Size = New System.Drawing.Size(72, 20)
        Me.lblTclsCd.TabIndex = 28
        Me.lblTclsCd.Text = "Label5"
        Me.lblTclsCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTnmd
        '
        Me.lblTnmd.BackColor = System.Drawing.Color.White
        Me.lblTnmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTnmd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTnmd.Location = New System.Drawing.Point(172, 9)
        Me.lblTnmd.Name = "lblTnmd"
        Me.lblTnmd.Size = New System.Drawing.Size(251, 20)
        Me.lblTnmd.TabIndex = 29
        Me.lblTnmd.Text = "Label6"
        Me.lblTnmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRst
        '
        Me.lblRst.BackColor = System.Drawing.Color.White
        Me.lblRst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRst.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRst.Location = New System.Drawing.Point(99, 30)
        Me.lblRst.Name = "lblRst"
        Me.lblRst.Size = New System.Drawing.Size(324, 20)
        Me.lblRst.TabIndex = 30
        Me.lblRst.Text = "Label6"
        Me.lblRst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCvtForm
        '
        Me.lblCvtForm.BackColor = System.Drawing.Color.White
        Me.lblCvtForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCvtForm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCvtForm.Location = New System.Drawing.Point(99, 292)
        Me.lblCvtForm.Name = "lblCvtForm"
        Me.lblCvtForm.Size = New System.Drawing.Size(325, 20)
        Me.lblCvtForm.TabIndex = 31
        Me.lblCvtForm.Text = "Label6"
        Me.lblCvtForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMemo
        '
        Me.lblMemo.AutoSize = True
        Me.lblMemo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblMemo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMemo.Location = New System.Drawing.Point(15, 320)
        Me.lblMemo.Name = "lblMemo"
        Me.lblMemo.Size = New System.Drawing.Size(41, 12)
        Me.lblMemo.TabIndex = 32
        Me.lblMemo.Text = "Label6"
        Me.lblMemo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.Location = New System.Drawing.Point(352, 353)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(77, 26)
        Me.btnExit.TabIndex = 33
        Me.btnExit.Text = "닫기 (Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'FGCVT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(435, 391)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblMemo)
        Me.Controls.Add(Me.lblCvtForm)
        Me.Controls.Add(Me.lblRst)
        Me.Controls.Add(Me.lblTnmd)
        Me.Controls.Add(Me.lblTclsCd)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.AxfpSpread1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "FGCVT"
        Me.Text = "결과값 자동변화 계산식"
        CType(Me.AxfpSpread1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AxfpSpread1 As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTclsCd As System.Windows.Forms.Label
    Friend WithEvents lblTnmd As System.Windows.Forms.Label
    Friend WithEvents lblRst As System.Windows.Forms.Label
    Friend WithEvents lblCvtForm As System.Windows.Forms.Label
    Friend WithEvents lblMemo As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
