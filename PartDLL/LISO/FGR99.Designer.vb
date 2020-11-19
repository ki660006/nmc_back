<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR99
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR99))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtBcprtno = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.chkLogMode = New System.Windows.Forms.CheckBox()
        Me.txtSamplinfo = New System.Windows.Forms.TextBox()
        Me.txtTest = New System.Windows.Forms.TextBox()
        Me.txtRst1 = New System.Windows.Forms.TextBox()
        Me.txtRst2 = New System.Windows.Forms.TextBox()
        Me.chkRerun = New System.Windows.Forms.CheckBox()
        Me.chkPoct = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "인터페이스 결과테스트"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(41, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "바코드번호"
        '
        'txtBcprtno
        '
        Me.txtBcprtno.Location = New System.Drawing.Point(112, 33)
        Me.txtBcprtno.Name = "txtBcprtno"
        Me.txtBcprtno.Size = New System.Drawing.Size(147, 21)
        Me.txtBcprtno.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(38, 69)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(632, 189)
        Me.Panel1.TabIndex = 4
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(632, 189)
        Me.spdList.TabIndex = 0
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(584, 23)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(86, 40)
        Me.btnSend.TabIndex = 5
        Me.btnSend.Text = "전송"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(277, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "FLD"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(277, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "FS"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(277, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "ETX"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(313, 1)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(40, 21)
        Me.TextBox1.TabIndex = 9
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(313, 22)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(40, 21)
        Me.TextBox2.TabIndex = 10
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(313, 42)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(40, 21)
        Me.TextBox3.TabIndex = 11
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButton2)
        Me.GroupBox1.Controls.Add(Me.RadioButton1)
        Me.GroupBox1.Location = New System.Drawing.Point(359, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(93, 62)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "rstflg"
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(9, 37)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(71, 16)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "최종보고"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(9, 17)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(71, 16)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.Text = "중간보고"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'chkLogMode
        '
        Me.chkLogMode.AutoSize = True
        Me.chkLogMode.Location = New System.Drawing.Point(47, 287)
        Me.chkLogMode.Name = "chkLogMode"
        Me.chkLogMode.Size = New System.Drawing.Size(45, 16)
        Me.chkLogMode.TabIndex = 13
        Me.chkLogMode.Text = "Log"
        Me.chkLogMode.UseVisualStyleBackColor = True
        '
        'txtSamplinfo
        '
        Me.txtSamplinfo.Location = New System.Drawing.Point(178, 263)
        Me.txtSamplinfo.Multiline = True
        Me.txtSamplinfo.Name = "txtSamplinfo"
        Me.txtSamplinfo.Size = New System.Drawing.Size(482, 83)
        Me.txtSamplinfo.TabIndex = 14
        '
        'txtTest
        '
        Me.txtTest.Location = New System.Drawing.Point(178, 352)
        Me.txtTest.Multiline = True
        Me.txtTest.Name = "txtTest"
        Me.txtTest.Size = New System.Drawing.Size(482, 35)
        Me.txtTest.TabIndex = 15
        '
        'txtRst1
        '
        Me.txtRst1.Location = New System.Drawing.Point(178, 393)
        Me.txtRst1.Multiline = True
        Me.txtRst1.Name = "txtRst1"
        Me.txtRst1.Size = New System.Drawing.Size(482, 21)
        Me.txtRst1.TabIndex = 16
        '
        'txtRst2
        '
        Me.txtRst2.Location = New System.Drawing.Point(178, 420)
        Me.txtRst2.Multiline = True
        Me.txtRst2.Name = "txtRst2"
        Me.txtRst2.Size = New System.Drawing.Size(482, 21)
        Me.txtRst2.TabIndex = 17
        '
        'chkRerun
        '
        Me.chkRerun.AutoSize = True
        Me.chkRerun.Location = New System.Drawing.Point(478, 31)
        Me.chkRerun.Name = "chkRerun"
        Me.chkRerun.Size = New System.Drawing.Size(48, 16)
        Me.chkRerun.TabIndex = 18
        Me.chkRerun.Text = "재검"
        Me.chkRerun.UseVisualStyleBackColor = True
        '
        'chkPoct
        '
        Me.chkPoct.AutoSize = True
        Me.chkPoct.Location = New System.Drawing.Point(478, 8)
        Me.chkPoct.Name = "chkPoct"
        Me.chkPoct.Size = New System.Drawing.Size(58, 16)
        Me.chkPoct.TabIndex = 19
        Me.chkPoct.Text = "POCT"
        Me.chkPoct.UseVisualStyleBackColor = True
        '
        'FGR99
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(682, 507)
        Me.Controls.Add(Me.chkPoct)
        Me.Controls.Add(Me.chkRerun)
        Me.Controls.Add(Me.txtRst2)
        Me.Controls.Add(Me.txtRst1)
        Me.Controls.Add(Me.txtTest)
        Me.Controls.Add(Me.txtSamplinfo)
        Me.Controls.Add(Me.chkLogMode)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtBcprtno)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FGR99"
        Me.Text = "FGR99"
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtBcprtno As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents chkLogMode As System.Windows.Forms.CheckBox
    Friend WithEvents txtSamplinfo As System.Windows.Forms.TextBox
    Friend WithEvents txtTest As System.Windows.Forms.TextBox
    Friend WithEvents txtRst1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRst2 As System.Windows.Forms.TextBox
    Friend WithEvents chkRerun As System.Windows.Forms.CheckBox
    Friend WithEvents chkPoct As System.Windows.Forms.CheckBox
End Class
