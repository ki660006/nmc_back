<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPST_CYTOSPIN
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_CYTOSPIN))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnHelp_cmt = New System.Windows.Forms.Button()
        Me.btnHelp_con = New System.Windows.Forms.Button()
        Me.spdRst = New AxFPSpreadADO.AxfpSpread()
        Me.txtCmt = New System.Windows.Forms.TextBox()
        Me.lblCmt = New System.Windows.Forms.Label()
        Me.lblCon = New System.Windows.Forms.Label()
        Me.txtCon = New System.Windows.Forms.TextBox()
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(521, 476)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(93, 36)
        Me.btnClose.TabIndex = 83
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(422, 476)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 36)
        Me.btnSave.TabIndex = 82
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnHelp_cmt
        '
        Me.btnHelp_cmt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_cmt.Image = CType(resources.GetObject("btnHelp_cmt.Image"), System.Drawing.Image)
        Me.btnHelp_cmt.Location = New System.Drawing.Point(569, 351)
        Me.btnHelp_cmt.Name = "btnHelp_cmt"
        Me.btnHelp_cmt.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_cmt.TabIndex = 110
        Me.btnHelp_cmt.UseVisualStyleBackColor = True
        '
        'btnHelp_con
        '
        Me.btnHelp_con.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_con.Image = CType(resources.GetObject("btnHelp_con.Image"), System.Drawing.Image)
        Me.btnHelp_con.Location = New System.Drawing.Point(569, 404)
        Me.btnHelp_con.Name = "btnHelp_con"
        Me.btnHelp_con.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_con.TabIndex = 111
        Me.btnHelp_con.UseVisualStyleBackColor = True
        '
        'spdRst
        '
        Me.spdRst.DataSource = Nothing
        Me.spdRst.Location = New System.Drawing.Point(23, 45)
        Me.spdRst.Name = "spdRst"
        Me.spdRst.OcxState = CType(resources.GetObject("spdRst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRst.Size = New System.Drawing.Size(569, 292)
        Me.spdRst.TabIndex = 112
        '
        'txtCmt
        '
        Me.txtCmt.Location = New System.Drawing.Point(93, 351)
        Me.txtCmt.Name = "txtCmt"
        Me.txtCmt.Size = New System.Drawing.Size(475, 21)
        Me.txtCmt.TabIndex = 114
        Me.txtCmt.Tag = "L177814"
        '
        'lblCmt
        '
        Me.lblCmt.AutoSize = True
        Me.lblCmt.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmt.ForeColor = System.Drawing.Color.Blue
        Me.lblCmt.Location = New System.Drawing.Point(20, 353)
        Me.lblCmt.Name = "lblCmt"
        Me.lblCmt.Size = New System.Drawing.Size(71, 13)
        Me.lblCmt.TabIndex = 113
        Me.lblCmt.Text = "Comment:"
        '
        'lblCon
        '
        Me.lblCon.AutoSize = True
        Me.lblCon.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCon.ForeColor = System.Drawing.Color.Blue
        Me.lblCon.Location = New System.Drawing.Point(20, 389)
        Me.lblCon.Name = "lblCon"
        Me.lblCon.Size = New System.Drawing.Size(95, 13)
        Me.lblCon.TabIndex = 115
        Me.lblCon.Text = "Conclusion:"
        '
        'txtCon
        '
        Me.txtCon.Location = New System.Drawing.Point(93, 405)
        Me.txtCon.Name = "txtCon"
        Me.txtCon.Size = New System.Drawing.Size(475, 21)
        Me.txtCon.TabIndex = 116
        Me.txtCon.Tag = "L177815"
        '
        'FGPOPUPST_CYTOSPIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(627, 524)
        Me.Controls.Add(Me.txtCon)
        Me.Controls.Add(Me.lblCon)
        Me.Controls.Add(Me.txtCmt)
        Me.Controls.Add(Me.lblCmt)
        Me.Controls.Add(Me.spdRst)
        Me.Controls.Add(Me.btnHelp_con)
        Me.Controls.Add(Me.btnHelp_cmt)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGPOPUPST_CYTOSPIN"
        Me.Text = "특수검사 모듈 (CYTOSPIN)"
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnHelp_cmt As System.Windows.Forms.Button
    Friend WithEvents btnHelp_con As System.Windows.Forms.Button
    Friend WithEvents spdRst As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtCmt As System.Windows.Forms.TextBox
    Friend WithEvents lblCmt As System.Windows.Forms.Label
    Friend WithEvents lblCon As System.Windows.Forms.Label
    Friend WithEvents txtCon As System.Windows.Forms.TextBox
End Class
