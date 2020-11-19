<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGSMSSEND
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGSMSSEND))
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtSmsCont = New System.Windows.Forms.TextBox
        Me.spdDrInfo = New AxFPSpreadADO.AxfpSpread
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboDept = New System.Windows.Forms.ComboBox
        Me.lblLineQry = New System.Windows.Forms.Label
        Me.lblPatCount = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnSend = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.txtUsrNm = New System.Windows.Forms.TextBox
        Me.txtTelno = New System.Windows.Forms.TextBox
        Me.txtBcno = New System.Windows.Forms.TextBox
        Me.txtLisseq = New System.Windows.Forms.TextBox
        Me.GroupBox2.SuspendLayout()
        CType(Me.spdDrInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(9, -3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 22)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "전송내용"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSmsCont)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(286, 134)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        '
        'txtSmsCont
        '
        Me.txtSmsCont.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSmsCont.Location = New System.Drawing.Point(6, 22)
        Me.txtSmsCont.Multiline = True
        Me.txtSmsCont.Name = "txtSmsCont"
        Me.txtSmsCont.Size = New System.Drawing.Size(276, 107)
        Me.txtSmsCont.TabIndex = 11
        '
        'spdDrInfo
        '
        Me.spdDrInfo.DataSource = Nothing
        Me.spdDrInfo.Location = New System.Drawing.Point(12, 209)
        Me.spdDrInfo.Name = "spdDrInfo"
        Me.spdDrInfo.OcxState = CType(resources.GetObject("spdDrInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDrInfo.Size = New System.Drawing.Size(284, 151)
        Me.spdDrInfo.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(12, 185)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 22)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "진료과"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label3.Visible = False
        '
        'cboDept
        '
        Me.cboDept.FormattingEnabled = True
        Me.cboDept.Location = New System.Drawing.Point(88, 186)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.Size = New System.Drawing.Size(210, 20)
        Me.cboDept.TabIndex = 14
        Me.cboDept.Visible = False
        '
        'lblLineQry
        '
        Me.lblLineQry.ForeColor = System.Drawing.Color.Gray
        Me.lblLineQry.Location = New System.Drawing.Point(4, 154)
        Me.lblLineQry.Name = "lblLineQry"
        Me.lblLineQry.Size = New System.Drawing.Size(311, 10)
        Me.lblLineQry.TabIndex = 70
        Me.lblLineQry.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'lblPatCount
        '
        Me.lblPatCount.AutoSize = True
        Me.lblPatCount.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatCount.Location = New System.Drawing.Point(13, 167)
        Me.lblPatCount.Name = "lblPatCount"
        Me.lblPatCount.Size = New System.Drawing.Size(65, 12)
        Me.lblPatCount.TabIndex = 71
        Me.lblPatCount.Text = ">> 수신자"
        Me.lblPatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(13, 426)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 73
        Me.Label2.Text = ">> 발신자"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Gray
        Me.Label4.Location = New System.Drawing.Point(4, 414)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(311, 10)
        Me.Label4.TabIndex = 72
        Me.Label4.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnSend)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 468)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(309, 33)
        Me.Panel1.TabIndex = 74
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Location = New System.Drawing.Point(120, 3)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(89, 25)
        Me.btnSend.TabIndex = 19
        Me.btnSend.Text = "전송"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(211, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(89, 25)
        Me.btnExit.TabIndex = 18
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'txtUsrNm
        '
        Me.txtUsrNm.Location = New System.Drawing.Point(13, 442)
        Me.txtUsrNm.Name = "txtUsrNm"
        Me.txtUsrNm.Size = New System.Drawing.Size(92, 21)
        Me.txtUsrNm.TabIndex = 75
        Me.txtUsrNm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTelno
        '
        Me.txtTelno.Location = New System.Drawing.Point(108, 442)
        Me.txtTelno.Name = "txtTelno"
        Me.txtTelno.Size = New System.Drawing.Size(192, 21)
        Me.txtTelno.TabIndex = 76
        Me.txtTelno.Text = "02-2260-7356"
        Me.txtTelno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBcno
        '
        Me.txtBcno.Location = New System.Drawing.Point(108, 392)
        Me.txtBcno.Name = "txtBcno"
        Me.txtBcno.Size = New System.Drawing.Size(190, 21)
        Me.txtBcno.TabIndex = 77
        '
        'txtLisseq
        '
        Me.txtLisseq.Location = New System.Drawing.Point(14, 392)
        Me.txtLisseq.Name = "txtLisseq"
        Me.txtLisseq.Size = New System.Drawing.Size(77, 21)
        Me.txtLisseq.TabIndex = 78
        '
        'FGSMSSEND
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(309, 501)
        Me.Controls.Add(Me.txtLisseq)
        Me.Controls.Add(Me.txtBcno)
        Me.Controls.Add(Me.txtTelno)
        Me.Controls.Add(Me.txtUsrNm)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblPatCount)
        Me.Controls.Add(Me.lblLineQry)
        Me.Controls.Add(Me.cboDept)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.spdDrInfo)
        Me.Controls.Add(Me.GroupBox2)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGSMSSEND"
        Me.Text = "SMS 전송"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.spdDrInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSmsCont As System.Windows.Forms.TextBox
    Friend WithEvents spdDrInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboDept As System.Windows.Forms.ComboBox
    Friend WithEvents lblLineQry As System.Windows.Forms.Label
    Friend WithEvents lblPatCount As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents txtUsrNm As System.Windows.Forms.TextBox
    Friend WithEvents txtTelno As System.Windows.Forms.TextBox
    Friend WithEvents txtBcno As System.Windows.Forms.TextBox
    Friend WithEvents txtLisseq As System.Windows.Forms.TextBox
End Class
