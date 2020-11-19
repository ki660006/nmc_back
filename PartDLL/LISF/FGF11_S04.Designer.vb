<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGF11_S04
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGF11_S04))
        Me.spdDTest = New AxFPSpreadADO.AxfpSpread()
        Me.lblRstType = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTestcd = New System.Windows.Forms.TextBox()
        Me.TxtTnmd = New System.Windows.Forms.TextBox()
        Me.TxtSpccd = New System.Windows.Forms.TextBox()
        Me.Txtspcnmd = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnMaxRow10 = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnMaxRowAdd = New System.Windows.Forms.Button()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.pnlCenter = New System.Windows.Forms.Panel()
        CType(Me.spdDTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlCenter.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdDTest
        '
        Me.spdDTest.DataSource = Nothing
        Me.spdDTest.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdDTest.Location = New System.Drawing.Point(0, 0)
        Me.spdDTest.Name = "spdDTest"
        Me.spdDTest.OcxState = CType(resources.GetObject("spdDTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDTest.Size = New System.Drawing.Size(512, 315)
        Me.spdDTest.TabIndex = 0
        '
        'lblRstType
        '
        Me.lblRstType.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstType.ForeColor = System.Drawing.Color.White
        Me.lblRstType.Location = New System.Drawing.Point(5, 23)
        Me.lblRstType.Name = "lblRstType"
        Me.lblRstType.Size = New System.Drawing.Size(88, 21)
        Me.lblRstType.TabIndex = 1
        Me.lblRstType.Text = "검사코드"
        Me.lblRstType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(181, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 21)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "검사명"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(5, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 21)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "검체코드"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(181, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 21)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "검체명"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestcd
        '
        Me.txtTestcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestcd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestcd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestcd.Location = New System.Drawing.Point(93, 23)
        Me.txtTestcd.MaxLength = 20
        Me.txtTestcd.Name = "txtTestcd"
        Me.txtTestcd.Size = New System.Drawing.Size(83, 21)
        Me.txtTestcd.TabIndex = 29
        Me.txtTestcd.Tag = "DELTAL"
        Me.txtTestcd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtTnmd
        '
        Me.TxtTnmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTnmd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtTnmd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtTnmd.Location = New System.Drawing.Point(269, 23)
        Me.TxtTnmd.MaxLength = 20
        Me.TxtTnmd.Name = "TxtTnmd"
        Me.TxtTnmd.Size = New System.Drawing.Size(237, 21)
        Me.TxtTnmd.TabIndex = 30
        Me.TxtTnmd.Tag = "DELTAL"
        Me.TxtTnmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtSpccd
        '
        Me.TxtSpccd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSpccd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSpccd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtSpccd.Location = New System.Drawing.Point(93, 47)
        Me.TxtSpccd.MaxLength = 20
        Me.TxtSpccd.Name = "TxtSpccd"
        Me.TxtSpccd.Size = New System.Drawing.Size(83, 21)
        Me.TxtSpccd.TabIndex = 31
        Me.TxtSpccd.Tag = "DELTAL"
        Me.TxtSpccd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Txtspcnmd
        '
        Me.Txtspcnmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Txtspcnmd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.Txtspcnmd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.Txtspcnmd.Location = New System.Drawing.Point(269, 47)
        Me.Txtspcnmd.MaxLength = 20
        Me.Txtspcnmd.Name = "Txtspcnmd"
        Me.Txtspcnmd.Size = New System.Drawing.Size(237, 21)
        Me.Txtspcnmd.TabIndex = 32
        Me.Txtspcnmd.Tag = "DELTAL"
        Me.Txtspcnmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(381, 12)
        Me.Label5.TabIndex = 231
        Me.Label5.Text = "※ 본 화면은 검사의뢰지침 화면에 보이는 세부 검사항목 설정 입니다."
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnMaxRow10)
        Me.pnlBottom.Controls.Add(Me.btnSave)
        Me.pnlBottom.Controls.Add(Me.btnMaxRowAdd)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 399)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(512, 33)
        Me.pnlBottom.TabIndex = 232
        '
        'btnMaxRow10
        '
        Me.btnMaxRow10.Location = New System.Drawing.Point(76, 6)
        Me.btnMaxRow10.Name = "btnMaxRow10"
        Me.btnMaxRow10.Size = New System.Drawing.Size(37, 24)
        Me.btnMaxRow10.TabIndex = 117
        Me.btnMaxRow10.Text = "+ 10"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(410, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 24)
        Me.btnSave.TabIndex = 116
        Me.btnSave.Text = "저장"
        '
        'btnMaxRowAdd
        '
        Me.btnMaxRowAdd.Location = New System.Drawing.Point(3, 6)
        Me.btnMaxRowAdd.Name = "btnMaxRowAdd"
        Me.btnMaxRowAdd.Size = New System.Drawing.Size(71, 24)
        Me.btnMaxRowAdd.TabIndex = 115
        Me.btnMaxRowAdd.Text = "줄 추가"
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Txtspcnmd)
        Me.pnlTop.Controls.Add(Me.TxtSpccd)
        Me.pnlTop.Controls.Add(Me.TxtTnmd)
        Me.pnlTop.Controls.Add(Me.txtTestcd)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.lblRstType)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(512, 84)
        Me.pnlTop.TabIndex = 233
        '
        'pnlCenter
        '
        Me.pnlCenter.Controls.Add(Me.spdDTest)
        Me.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCenter.Location = New System.Drawing.Point(0, 84)
        Me.pnlCenter.Name = "pnlCenter"
        Me.pnlCenter.Size = New System.Drawing.Size(512, 315)
        Me.pnlCenter.TabIndex = 234
        '
        'FGF11_S04
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(512, 432)
        Me.Controls.Add(Me.pnlCenter)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Name = "FGF11_S04"
        Me.Text = "검사의뢰지침 세부검사"
        CType(Me.spdDTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.pnlCenter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdDTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblRstType As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTestcd As System.Windows.Forms.TextBox
    Friend WithEvents TxtTnmd As System.Windows.Forms.TextBox
    Friend WithEvents TxtSpccd As System.Windows.Forms.TextBox
    Friend WithEvents Txtspcnmd As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlCenter As System.Windows.Forms.Panel
    Friend WithEvents btnMaxRowAdd As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnMaxRow10 As System.Windows.Forms.Button
End Class
