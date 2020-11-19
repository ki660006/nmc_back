<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF51
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF51))
        Me.tbcBody = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.cboTestCd = New System.Windows.Forms.ComboBox
        Me.lblTestCd = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.lblGuide2 = New System.Windows.Forms.Label
        Me.lblGuide1 = New System.Windows.Forms.Label
        Me.spdItem = New AxFPSpreadADO.AxfpSpread
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tbcBody.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.spdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbcBody
        '
        Me.tbcBody.Controls.Add(Me.tbcTpg)
        Me.tbcBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcBody.Location = New System.Drawing.Point(0, 0)
        Me.tbcBody.Name = "tbcBody"
        Me.tbcBody.SelectedIndex = 0
        Me.tbcBody.Size = New System.Drawing.Size(792, 587)
        Me.tbcBody.TabIndex = 2
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtModNm)
        Me.tbcTpg.Controls.Add(Me.txtModID)
        Me.tbcTpg.Controls.Add(Me.lblModNm)
        Me.tbcTpg.Controls.Add(Me.txtModDT)
        Me.tbcTpg.Controls.Add(Me.lblModDT)
        Me.tbcTpg.Controls.Add(Me.txtRegDT)
        Me.tbcTpg.Controls.Add(Me.lblUserNm)
        Me.tbcTpg.Controls.Add(Me.lblRegDT)
        Me.tbcTpg.Controls.Add(Me.txtRegID)
        Me.tbcTpg.Controls.Add(Me.grpCd)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg.Name = "tbcTpg"
        Me.tbcTpg.Size = New System.Drawing.Size(784, 562)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "Alert Rule 설정"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(708, 530)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(66, 21)
        Me.txtRegNm.TabIndex = 147
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(296, 531)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(78, 21)
        Me.txtModNm.TabIndex = 142
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(296, 531)
        Me.txtModID.Margin = New System.Windows.Forms.Padding(1)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(78, 21)
        Me.txtModID.TabIndex = 22
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(211, 531)
        Me.lblModNm.Margin = New System.Windows.Forms.Padding(1)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 21
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(92, 530)
        Me.txtModDT.Margin = New System.Windows.Forms.Padding(1)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 20
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(8, 530)
        Me.lblModDT.Margin = New System.Windows.Forms.Padding(1)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(83, 21)
        Me.lblModDT.TabIndex = 19
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(502, 530)
        Me.txtRegDT.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 16
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(622, 530)
        Me.lblUserNm.Margin = New System.Windows.Forms.Padding(1)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(85, 21)
        Me.lblUserNm.TabIndex = 15
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(415, 530)
        Me.lblRegDT.Margin = New System.Windows.Forms.Padding(1)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(86, 21)
        Me.lblRegDT.TabIndex = 18
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(714, 530)
        Me.txtRegID.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(60, 21)
        Me.txtRegID.TabIndex = 17
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.cboTestCd)
        Me.grpCd.Controls.Add(Me.lblTestCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(7, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(769, 44)
        Me.grpCd.TabIndex = 0
        Me.grpCd.TabStop = False
        '
        'cboTestCd
        '
        Me.cboTestCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTestCd.Location = New System.Drawing.Point(102, 15)
        Me.cboTestCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboTestCd.Name = "cboTestCd"
        Me.cboTestCd.Size = New System.Drawing.Size(295, 20)
        Me.cboTestCd.TabIndex = 5
        Me.cboTestCd.Tag = "TNMD_01"
        '
        'lblTestCd
        '
        Me.lblTestCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(19, 14)
        Me.lblTestCd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(81, 21)
        Me.lblTestCd.TabIndex = 4
        Me.lblTestCd.Text = " 검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(682, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 3
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.lblGuide2)
        Me.grpCdInfo1.Controls.Add(Me.lblGuide1)
        Me.grpCdInfo1.Controls.Add(Me.spdItem)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(7, 52)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(769, 472)
        Me.grpCdInfo1.TabIndex = 1
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = " Alert Rule 정보"
        '
        'lblGuide2
        '
        Me.lblGuide2.AutoSize = True
        Me.lblGuide2.Location = New System.Drawing.Point(19, 303)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(149, 12)
        Me.lblGuide2.TabIndex = 6
        Me.lblGuide2.Text = "   의미: $$= and, ||= or"
        '
        'lblGuide1
        '
        Me.lblGuide1.AutoSize = True
        Me.lblGuide1.Location = New System.Drawing.Point(19, 286)
        Me.lblGuide1.Name = "lblGuide1"
        Me.lblGuide1.Size = New System.Drawing.Size(599, 12)
        Me.lblGuide1.TabIndex = 5
        Me.lblGuide1.Text = "※ 항균제결과 입력방법 : (#B = '균코드' $$ [항균제코드] = 'DEC 결과') || [항균제코드] = 'DEC 결과' "
        '
        'spdItem
        '
        Me.spdItem.DataSource = Nothing
        Me.spdItem.Location = New System.Drawing.Point(17, 28)
        Me.spdItem.Name = "spdItem"
        Me.spdItem.OcxState = CType(resources.GetObject("spdItem.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdItem.Size = New System.Drawing.Size(734, 245)
        Me.spdItem.TabIndex = 0
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF51
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 587)
        Me.Controls.Add(Me.tbcBody)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FDF51"
        Me.Text = "[51] Alert Rule"
        Me.tbcBody.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.spdItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbcBody As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents cboTestCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdItem As AxFPSpreadADO.AxfpSpread
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblGuide1 As System.Windows.Forms.Label
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
End Class
