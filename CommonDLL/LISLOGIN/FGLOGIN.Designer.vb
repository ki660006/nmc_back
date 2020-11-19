<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGLOGIN_NMC
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGLOGIN_NMC))
        Me.lblLoginMsg = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnLogin = New System.Windows.Forms.Button
        Me.txtUsrPW = New System.Windows.Forms.TextBox
        Me.txtUsrID = New System.Windows.Forms.TextBox
        Me.chkSaveID = New System.Windows.Forms.CheckBox
        Me.btnFrmMinimize = New System.Windows.Forms.Button
        Me.imlTitle = New System.Windows.Forms.ImageList(Me.components)
        Me.picTitle = New System.Windows.Forms.PictureBox
        Me.lblServer = New System.Windows.Forms.Label
        Me.cboChange_srv = New System.Windows.Forms.ComboBox
        Me.lblwarning = New System.Windows.Forms.Label
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLoginMsg
        '
        Me.lblLoginMsg.AutoSize = True
        Me.lblLoginMsg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblLoginMsg.ForeColor = System.Drawing.Color.Navy
        Me.lblLoginMsg.Location = New System.Drawing.Point(239, 49)
        Me.lblLoginMsg.Name = "lblLoginMsg"
        Me.lblLoginMsg.Size = New System.Drawing.Size(245, 12)
        Me.lblLoginMsg.TabIndex = 13
        Me.lblLoginMsg.Text = "※ 이 프로그램은 사용중이며, 잠겨있습니다."
        Me.lblLoginMsg.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(414, 207)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(61, 21)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "CANCLE"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(414, 183)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(61, 21)
        Me.btnLogin.TabIndex = 2
        Me.btnLogin.Text = "LOGIN"
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'txtUsrPW
        '
        Me.txtUsrPW.Location = New System.Drawing.Point(307, 207)
        Me.txtUsrPW.Name = "txtUsrPW"
        Me.txtUsrPW.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtUsrPW.Size = New System.Drawing.Size(104, 21)
        Me.txtUsrPW.TabIndex = 1
        '
        'txtUsrID
        '
        Me.txtUsrID.Location = New System.Drawing.Point(307, 183)
        Me.txtUsrID.Name = "txtUsrID"
        Me.txtUsrID.Size = New System.Drawing.Size(104, 21)
        Me.txtUsrID.TabIndex = 0
        '
        'chkSaveID
        '
        Me.chkSaveID.BackColor = System.Drawing.Color.Transparent
        Me.chkSaveID.Checked = True
        Me.chkSaveID.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSaveID.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkSaveID.ForeColor = System.Drawing.Color.Snow
        Me.chkSaveID.Location = New System.Drawing.Point(249, 164)
        Me.chkSaveID.Name = "chkSaveID"
        Me.chkSaveID.Size = New System.Drawing.Size(95, 20)
        Me.chkSaveID.TabIndex = 8
        Me.chkSaveID.TabStop = False
        Me.chkSaveID.Text = "아이디 저장"
        Me.chkSaveID.UseVisualStyleBackColor = False
        '
        'btnFrmMinimize
        '
        Me.btnFrmMinimize.Location = New System.Drawing.Point(465, -1)
        Me.btnFrmMinimize.Name = "btnFrmMinimize"
        Me.btnFrmMinimize.Size = New System.Drawing.Size(30, 24)
        Me.btnFrmMinimize.TabIndex = 19
        Me.btnFrmMinimize.Text = "▼"
        Me.btnFrmMinimize.UseVisualStyleBackColor = True
        Me.btnFrmMinimize.Visible = False
        '
        'imlTitle
        '
        Me.imlTitle.ImageStream = CType(resources.GetObject("imlTitle.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlTitle.TransparentColor = System.Drawing.Color.Transparent
        Me.imlTitle.Images.SetKeyName(0, "")
        Me.imlTitle.Images.SetKeyName(1, "")
        '
        'picTitle
        '
        Me.picTitle.BackColor = System.Drawing.Color.White
        Me.picTitle.Image = CType(resources.GetObject("picTitle.Image"), System.Drawing.Image)
        Me.picTitle.Location = New System.Drawing.Point(1, -1)
        Me.picTitle.Name = "picTitle"
        Me.picTitle.Size = New System.Drawing.Size(73, 35)
        Me.picTitle.TabIndex = 20
        Me.picTitle.TabStop = False
        Me.picTitle.Visible = False
        '
        'lblServer
        '
        Me.lblServer.BackColor = System.Drawing.Color.LavenderBlush
        Me.lblServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblServer.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblServer.ForeColor = System.Drawing.Color.Crimson
        Me.lblServer.Location = New System.Drawing.Point(343, 160)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(132, 20)
        Me.lblServer.TabIndex = 21
        Me.lblServer.Text = "KMCLIS(LIS테스트 서버)"
        Me.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboChange_srv
        '
        Me.cboChange_srv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChange_srv.FormattingEnabled = True
        Me.cboChange_srv.Items.AddRange(New Object() {"", "[1] 운영서버(1)", "[2] 운영서버(2)", "[3] 개발서버", "[4] 교육서버", "[9] 회사서버"})
        Me.cboChange_srv.Location = New System.Drawing.Point(1, 208)
        Me.cboChange_srv.Name = "cboChange_srv"
        Me.cboChange_srv.Size = New System.Drawing.Size(195, 20)
        Me.cboChange_srv.TabIndex = 22
        Me.cboChange_srv.Visible = False
        '
        'lblwarning
        '
        Me.lblwarning.AutoSize = True
        Me.lblwarning.BackColor = System.Drawing.Color.Transparent
        Me.lblwarning.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblwarning.ForeColor = System.Drawing.Color.Red
        Me.lblwarning.Location = New System.Drawing.Point(1, 231)
        Me.lblwarning.Name = "lblwarning"
        Me.lblwarning.Size = New System.Drawing.Size(491, 11)
        Me.lblwarning.TabIndex = 23
        Me.lblwarning.Text = "※경고:부당한 방법으로 고객정보를 유출, 삭제, 변경하면 관계 법령에 따라 처벌 받을 수 있습니다"
        '
        'FGLOGIN_NMC
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(494, 244)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblwarning)
        Me.Controls.Add(Me.cboChange_srv)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.picTitle)
        Me.Controls.Add(Me.btnFrmMinimize)
        Me.Controls.Add(Me.lblLoginMsg)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.txtUsrPW)
        Me.Controls.Add(Me.txtUsrID)
        Me.Controls.Add(Me.chkSaveID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGLOGIN_NMC"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FGLOGIN"
        CType(Me.picTitle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLoginMsg As System.Windows.Forms.Label
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Private WithEvents btnLogin As System.Windows.Forms.Button
    Private WithEvents txtUsrPW As System.Windows.Forms.TextBox
    Private WithEvents txtUsrID As System.Windows.Forms.TextBox
    Friend WithEvents chkSaveID As System.Windows.Forms.CheckBox
    Friend WithEvents btnFrmMinimize As System.Windows.Forms.Button
    Friend WithEvents imlTitle As System.Windows.Forms.ImageList
    Friend WithEvents picTitle As System.Windows.Forms.PictureBox
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents cboChange_srv As System.Windows.Forms.ComboBox
    Friend WithEvents lblwarning As System.Windows.Forms.Label
End Class
