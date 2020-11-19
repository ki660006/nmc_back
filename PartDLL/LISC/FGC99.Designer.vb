<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGC99
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
        Me.txtIdno = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtAdminCd = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPatnm = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtOrddt = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtAdminNm = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtSwCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSwCfmCd = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtRetVal = New System.Windows.Forms.TextBox
        Me.btnRun = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtIdno
        '
        Me.txtIdno.Location = New System.Drawing.Point(97, 2)
        Me.txtIdno.Name = "txtIdno"
        Me.txtIdno.Size = New System.Drawing.Size(213, 21)
        Me.txtIdno.TabIndex = 0
        Me.txtIdno.Text = "3307031006018"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.MidnightBlue
        Me.lblSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(3, 2)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(93, 21)
        Me.lblSearch.TabIndex = 18
        Me.lblSearch.Text = "주민번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 21)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "요양기관코드"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAdminCd
        '
        Me.txtAdminCd.Location = New System.Drawing.Point(97, 46)
        Me.txtAdminCd.Name = "txtAdminCd"
        Me.txtAdminCd.Size = New System.Drawing.Size(213, 21)
        Me.txtAdminCd.TabIndex = 19
        Me.txtAdminCd.Text = "11101318"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 21)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "이름"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPatnm
        '
        Me.txtPatnm.Location = New System.Drawing.Point(97, 24)
        Me.txtPatnm.Name = "txtPatnm"
        Me.txtPatnm.Size = New System.Drawing.Size(213, 21)
        Me.txtPatnm.TabIndex = 21
        Me.txtPatnm.Text = "박정언"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(3, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(93, 21)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "처방일시"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtOrddt
        '
        Me.txtOrddt.Location = New System.Drawing.Point(97, 90)
        Me.txtOrddt.Name = "txtOrddt"
        Me.txtOrddt.Size = New System.Drawing.Size(213, 21)
        Me.txtOrddt.TabIndex = 23
        Me.txtOrddt.Text = "20171220"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(3, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 21)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "요양기관명칭"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAdminNm
        '
        Me.txtAdminNm.Location = New System.Drawing.Point(97, 68)
        Me.txtAdminNm.Name = "txtAdminNm"
        Me.txtAdminNm.Size = New System.Drawing.Size(213, 21)
        Me.txtAdminNm.TabIndex = 25
        Me.txtAdminNm.Text = "국립중앙의료원"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(3, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 21)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "sw업체코드"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSwCode
        '
        Me.txtSwCode.Location = New System.Drawing.Point(97, 112)
        Me.txtSwCode.Name = "txtSwCode"
        Me.txtSwCode.Size = New System.Drawing.Size(213, 21)
        Me.txtSwCode.TabIndex = 27
        Me.txtSwCode.Text = "11101318"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(3, 134)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 21)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "sw인증코드"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSwCfmCd
        '
        Me.txtSwCfmCd.Location = New System.Drawing.Point(97, 134)
        Me.txtSwCfmCd.Name = "txtSwCfmCd"
        Me.txtSwCfmCd.Size = New System.Drawing.Size(213, 21)
        Me.txtSwCfmCd.TabIndex = 29
        Me.txtSwCfmCd.Text = "111013180000000000000000000000"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.MidnightBlue
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(3, 156)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 21)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Return값"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRetVal
        '
        Me.txtRetVal.Location = New System.Drawing.Point(3, 178)
        Me.txtRetVal.Multiline = True
        Me.txtRetVal.Name = "txtRetVal"
        Me.txtRetVal.Size = New System.Drawing.Size(393, 73)
        Me.txtRetVal.TabIndex = 32
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(3, 253)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(125, 73)
        Me.btnRun.TabIndex = 33
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(186, 253)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(125, 73)
        Me.btnClear.TabIndex = 34
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'FGC99
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(408, 365)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnRun)
        Me.Controls.Add(Me.txtRetVal)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtSwCfmCd)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtSwCode)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtAdminNm)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtOrddt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPatnm)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAdminCd)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.txtIdno)
        Me.Name = "FGC99"
        Me.Text = "FGC99"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtIdno As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtAdminCd As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPatnm As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtOrddt As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAdminNm As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSwCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSwCfmCd As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRetVal As System.Windows.Forms.TextBox
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
