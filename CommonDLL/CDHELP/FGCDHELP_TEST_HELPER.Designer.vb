<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCDHELP_TEST_HELPER
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCDHELP_TEST_HELPER))
        Me.spdCdList = New AxFPSpreadADO.AxfpSpread()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkSel = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnEsc = New System.Windows.Forms.Button()
        Me.txtCd = New System.Windows.Forms.TextBox()
        Me.lblFieldNm = New System.Windows.Forms.Label()
        Me.cboPartSlip = New System.Windows.Forms.ComboBox()
        Me.cboQryGbn = New System.Windows.Forms.ComboBox()
        Me.Label39 = New System.Windows.Forms.Label()
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdCdList
        '
        Me.spdCdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdCdList.DataSource = Nothing
        Me.spdCdList.Location = New System.Drawing.Point(3, 3)
        Me.spdCdList.Name = "spdCdList"
        Me.spdCdList.OcxState = CType(resources.GetObject("spdCdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCdList.Size = New System.Drawing.Size(687, 503)
        Me.spdCdList.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkSel)
        Me.Panel1.Controls.Add(Me.spdCdList)
        Me.Panel1.Location = New System.Drawing.Point(2, 34)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(693, 509)
        Me.Panel1.TabIndex = 18
        '
        'chkSel
        '
        Me.chkSel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkSel.AutoSize = True
        Me.chkSel.Location = New System.Drawing.Point(42, 11)
        Me.chkSel.Name = "chkSel"
        Me.chkSel.Size = New System.Drawing.Size(15, 14)
        Me.chkSel.TabIndex = 1
        Me.chkSel.UseVisualStyleBackColor = True
        Me.chkSel.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnOK)
        Me.Panel2.Controls.Add(Me.btnEsc)
        Me.Panel2.Controls.Add(Me.txtCd)
        Me.Panel2.Controls.Add(Me.lblFieldNm)
        Me.Panel2.Controls.Add(Me.cboPartSlip)
        Me.Panel2.Controls.Add(Me.cboQryGbn)
        Me.Panel2.Controls.Add(Me.Label39)
        Me.Panel2.Location = New System.Drawing.Point(2, 2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(693, 30)
        Me.Panel2.TabIndex = 19
        '
        'btnOK
        '
        Me.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOK.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOK.Location = New System.Drawing.Point(584, 2)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(53, 25)
        Me.btnOK.TabIndex = 17
        Me.btnOK.Text = "조회"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnEsc
        '
        Me.btnEsc.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnEsc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnEsc.Location = New System.Drawing.Point(637, 2)
        Me.btnEsc.Name = "btnEsc"
        Me.btnEsc.Size = New System.Drawing.Size(53, 25)
        Me.btnEsc.TabIndex = 18
        Me.btnEsc.Text = "Esc"
        Me.btnEsc.UseVisualStyleBackColor = True
        '
        'txtCd
        '
        Me.txtCd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtCd.Location = New System.Drawing.Point(365, 5)
        Me.txtCd.Name = "txtCd"
        Me.txtCd.Size = New System.Drawing.Size(219, 21)
        Me.txtCd.TabIndex = 15
        '
        'lblFieldNm
        '
        Me.lblFieldNm.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblFieldNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblFieldNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFieldNm.ForeColor = System.Drawing.Color.White
        Me.lblFieldNm.Location = New System.Drawing.Point(285, 5)
        Me.lblFieldNm.Name = "lblFieldNm"
        Me.lblFieldNm.Size = New System.Drawing.Size(79, 21)
        Me.lblFieldNm.TabIndex = 16
        Me.lblFieldNm.Text = "검색어"
        Me.lblFieldNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboPartSlip
        '
        Me.cboPartSlip.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.FormattingEnabled = True
        Me.cboPartSlip.Location = New System.Drawing.Point(130, 4)
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(134, 20)
        Me.cboPartSlip.TabIndex = 14
        '
        'cboQryGbn
        '
        Me.cboQryGbn.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cboQryGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQryGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboQryGbn.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboQryGbn.Location = New System.Drawing.Point(76, 4)
        Me.cboQryGbn.Name = "cboQryGbn"
        Me.cboQryGbn.Size = New System.Drawing.Size(52, 20)
        Me.cboQryGbn.TabIndex = 12
        '
        'Label39
        '
        Me.Label39.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label39.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.White
        Me.Label39.Location = New System.Drawing.Point(3, 3)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(72, 21)
        Me.Label39.TabIndex = 13
        Me.Label39.Text = "검사분야"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGCDHELP_TEST_HELPER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(698, 546)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "FGCDHELP_TEST_HELPER"
        Me.Text = "FGCDHELP_TEST_HELPER"
        CType(Me.spdCdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdCdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkSel As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnEsc As System.Windows.Forms.Button
    Friend WithEvents txtCd As System.Windows.Forms.TextBox
    Friend WithEvents lblFieldNm As System.Windows.Forms.Label
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents cboQryGbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
End Class
