<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCDHELP01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCDHELP01))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkSel = New System.Windows.Forms.CheckBox
        Me.spdCdList = New AxFPSpreadADO.AxfpSpread
        Me.lblFieldNm = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnEsc = New System.Windows.Forms.Button
        Me.txtCd = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.chkSel)
        Me.Panel1.Controls.Add(Me.spdCdList)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(920, 337)
        Me.Panel1.TabIndex = 0
        '
        'chkSel
        '
        Me.chkSel.AutoSize = True
        Me.chkSel.Location = New System.Drawing.Point(38, 12)
        Me.chkSel.Name = "chkSel"
        Me.chkSel.Size = New System.Drawing.Size(15, 14)
        Me.chkSel.TabIndex = 1
        Me.chkSel.UseVisualStyleBackColor = True
        Me.chkSel.Visible = False
        '
        'spdCdList
        '
        Me.spdCdList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdCdList.DataSource = Nothing
        Me.spdCdList.Location = New System.Drawing.Point(3, 3)
        Me.spdCdList.Name = "spdCdList"
        Me.spdCdList.OcxState = CType(resources.GetObject("spdCdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCdList.Size = New System.Drawing.Size(895, 333)
        Me.spdCdList.TabIndex = 0
        '
        'lblFieldNm
        '
        Me.lblFieldNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFieldNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblFieldNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFieldNm.ForeColor = System.Drawing.Color.White
        Me.lblFieldNm.Location = New System.Drawing.Point(6, 343)
        Me.lblFieldNm.Name = "lblFieldNm"
        Me.lblFieldNm.Size = New System.Drawing.Size(79, 21)
        Me.lblFieldNm.TabIndex = 1
        Me.lblFieldNm.Text = "검색어"
        Me.lblFieldNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOK.Location = New System.Drawing.Point(814, 343)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(53, 25)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "조회"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnEsc
        '
        Me.btnEsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEsc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnEsc.Location = New System.Drawing.Point(867, 343)
        Me.btnEsc.Name = "btnEsc"
        Me.btnEsc.Size = New System.Drawing.Size(53, 25)
        Me.btnEsc.TabIndex = 4
        Me.btnEsc.Text = "Esc"
        Me.btnEsc.UseVisualStyleBackColor = True
        '
        'txtCd
        '
        Me.txtCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCd.Location = New System.Drawing.Point(86, 343)
        Me.txtCd.Name = "txtCd"
        Me.txtCd.Size = New System.Drawing.Size(722, 21)
        Me.txtCd.TabIndex = 0
        '
        'FGCDHELP01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(923, 373)
        Me.Controls.Add(Me.txtCd)
        Me.Controls.Add(Me.btnEsc)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblFieldNm)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "FGCDHELP01"
        Me.Text = "FGCDHELP02"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblFieldNm As System.Windows.Forms.Label
    Friend WithEvents spdCdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnEsc As System.Windows.Forms.Button
    Friend WithEvents txtCd As System.Windows.Forms.TextBox
    Friend WithEvents chkSel As System.Windows.Forms.CheckBox
End Class
