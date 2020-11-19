<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPISC01_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPISC01_S01))
        Me.lblLabel = New System.Windows.Forms.Label
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.chkAutoTk = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLabel
        '
        Me.lblLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblLabel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblLabel.ForeColor = System.Drawing.Color.White
        Me.lblLabel.Location = New System.Drawing.Point(9, 9)
        Me.lblLabel.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLabel.Name = "lblLabel"
        Me.lblLabel.Size = New System.Drawing.Size(68, 21)
        Me.lblLabel.TabIndex = 31
        Me.lblLabel.Text = "검체번호"
        Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBcNo
        '
        Me.txtBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNo.Location = New System.Drawing.Point(78, 9)
        Me.txtBcNo.MaxLength = 15
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(147, 21)
        Me.txtBcNo.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 234)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(681, 32)
        Me.Panel1.TabIndex = 189
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(593, 2)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 25)
        Me.btnExit.TabIndex = 189
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.Location = New System.Drawing.Point(9, 34)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(662, 195)
        Me.spdList.TabIndex = 190
        '
        'chkAutoTk
        '
        Me.chkAutoTk.AutoSize = True
        Me.chkAutoTk.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkAutoTk.Location = New System.Drawing.Point(233, 13)
        Me.chkAutoTk.Name = "chkAutoTk"
        Me.chkAutoTk.Size = New System.Drawing.Size(68, 15)
        Me.chkAutoTk.TabIndex = 191
        Me.chkAutoTk.Text = "자동접수"
        Me.chkAutoTk.UseVisualStyleBackColor = True
        Me.chkAutoTk.Visible = False
        '
        'FGC01_S01
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(681, 266)
        Me.Controls.Add(Me.chkAutoTk)
        Me.Controls.Add(Me.spdList)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtBcNo)
        Me.Controls.Add(Me.lblLabel)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGC01_S01"
        Me.Text = "채혈일시 등록"
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLabel As System.Windows.Forms.Label
    Protected Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkAutoTk As System.Windows.Forms.CheckBox
End Class
