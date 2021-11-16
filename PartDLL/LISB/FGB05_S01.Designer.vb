<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB05_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB05_S01))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkall = New System.Windows.Forms.CheckBox
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBldNm = New System.Windows.Forms.TextBox
        Me.txtBldQnt = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblComNmd = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkall)
        Me.GroupBox1.Controls.Add(Me.spdList)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(838, 416)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'chkall
        '
        Me.chkall.AutoSize = True
        Me.chkall.Location = New System.Drawing.Point(43, 23)
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(15, 14)
        Me.chkall.TabIndex = 1
        Me.chkall.UseVisualStyleBackColor = True
        '
        'spdList
        '
        'Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(6, 10)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(826, 400)
        Me.spdList.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(684, 489)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(79, 44)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "입고"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(769, 489)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(79, 44)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 21)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "혈액번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBldNm
        '
        Me.txtBldNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldNm.Location = New System.Drawing.Point(97, 9)
        Me.txtBldNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtBldNm.MaxLength = 10
        Me.txtBldNm.Name = "txtBldNm"
        Me.txtBldNm.Size = New System.Drawing.Size(86, 21)
        Me.txtBldNm.TabIndex = 0
        '
        'txtBldQnt
        '
        Me.txtBldQnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldQnt.Location = New System.Drawing.Point(97, 32)
        Me.txtBldQnt.Margin = New System.Windows.Forms.Padding(1)
        Me.txtBldQnt.MaxLength = 10
        Me.txtBldQnt.Name = "txtBldQnt"
        Me.txtBldQnt.Size = New System.Drawing.Size(86, 21)
        Me.txtBldQnt.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(12, 32)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 21)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "성분제제"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblComNmd
        '
        Me.lblComNmd.BackColor = System.Drawing.Color.LightGray
        Me.lblComNmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComNmd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComNmd.ForeColor = System.Drawing.Color.Black
        Me.lblComNmd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComNmd.Location = New System.Drawing.Point(184, 32)
        Me.lblComNmd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComNmd.Name = "lblComNmd"
        Me.lblComNmd.Size = New System.Drawing.Size(201, 21)
        Me.lblComNmd.TabIndex = 54
        Me.lblComNmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FGB05_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(860, 545)
        Me.Controls.Add(Me.lblComNmd)
        Me.Controls.Add(Me.txtBldQnt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBldNm)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGB05_S01"
        Me.Text = "CSV 입고"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBldNm As System.Windows.Forms.TextBox
    Friend WithEvents txtBldQnt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblComNmd As System.Windows.Forms.Label
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkall As System.Windows.Forms.CheckBox
End Class
