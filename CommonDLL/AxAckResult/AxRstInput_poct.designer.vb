<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxRstInput_poct
    Inherits System.Windows.Forms.UserControl

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxRstInput_poct))
        Me.spdResult = New AxFPSpreadADO.AxfpSpread
        Me.cmuLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuSpRst = New System.Windows.Forms.ToolStripMenuItem
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstEx = New System.Windows.Forms.ListBox
        Me.chkSelect = New System.Windows.Forms.CheckBox
        Me.txtOrgRst = New System.Windows.Forms.TextBox
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.lblcasegbn = New System.Windows.Forms.Label
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblReg = New System.Windows.Forms.Label
        Me.lblFN = New System.Windows.Forms.Label
        Me.pnlCode = New System.Windows.Forms.Panel
        Me.lstCode = New System.Windows.Forms.ListBox
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmuLink.SuspendLayout()
        Me.pnlCode.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdResult
        '
        Me.spdResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdResult.ContextMenuStrip = Me.cmuLink
        Me.spdResult.DataSource = Nothing
        Me.spdResult.Location = New System.Drawing.Point(0, 0)
        Me.spdResult.Name = "spdResult"
        Me.spdResult.OcxState = CType(resources.GetObject("spdResult.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdResult.Size = New System.Drawing.Size(941, 455)
        Me.spdResult.TabIndex = 0
        '
        'cmuLink
        '
        Me.cmuLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSpRst})
        Me.cmuLink.Name = "cmuRstList"
        Me.cmuLink.Size = New System.Drawing.Size(151, 26)
        Me.cmuLink.Text = "상황에 맞는 메뉴"
        '
        'mnuSpRst
        '
        Me.mnuSpRst.Name = "mnuSpRst"
        Me.mnuSpRst.Size = New System.Drawing.Size(150, 22)
        Me.mnuSpRst.Text = "특수결과 입력"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.White
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(435, 457)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 38)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = " △ 검사"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(504, 457)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 38)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = " ○ Review"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.White
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label2.Location = New System.Drawing.Point(578, 457)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 38)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = " ◆ 완료"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(376, 457)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 38)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "결과범례"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lstEx
        '
        Me.lstEx.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstEx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstEx.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lstEx.ItemHeight = 12
        Me.lstEx.Location = New System.Drawing.Point(647, 457)
        Me.lstEx.Name = "lstEx"
        Me.lstEx.Size = New System.Drawing.Size(294, 38)
        Me.lstEx.TabIndex = 54
        '
        'chkSelect
        '
        Me.chkSelect.AutoSize = True
        Me.chkSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkSelect.Location = New System.Drawing.Point(29, 5)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(15, 14)
        Me.chkSelect.TabIndex = 168
        Me.chkSelect.UseVisualStyleBackColor = False
        '
        'txtOrgRst
        '
        Me.txtOrgRst.Location = New System.Drawing.Point(154, 1)
        Me.txtOrgRst.Name = "txtOrgRst"
        Me.txtOrgRst.Size = New System.Drawing.Size(36, 21)
        Me.txtOrgRst.TabIndex = 172
        Me.txtOrgRst.Visible = False
        '
        'txtTestCd
        '
        Me.txtTestCd.Location = New System.Drawing.Point(196, 1)
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(33, 21)
        Me.txtTestCd.TabIndex = 174
        Me.txtTestCd.Visible = False
        '
        'lblcasegbn
        '
        Me.lblcasegbn.Location = New System.Drawing.Point(638, 305)
        Me.lblcasegbn.Name = "lblcasegbn"
        Me.lblcasegbn.Size = New System.Drawing.Size(98, 20)
        Me.lblcasegbn.TabIndex = 187
        Me.lblcasegbn.Text = "casegbn"
        Me.lblcasegbn.Visible = False
        '
        'txtBcNo
        '
        Me.txtBcNo.Location = New System.Drawing.Point(235, 0)
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(33, 21)
        Me.txtBcNo.TabIndex = 189
        Me.txtBcNo.Visible = False
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(188, 457)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 38)
        Me.Label11.TabIndex = 195
        Me.Label11.Text = "최종 보고자"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(0, 457)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 38)
        Me.Label8.TabIndex = 194
        Me.Label8.Text = "결과 입력자"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblReg
        '
        Me.lblReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblReg.BackColor = System.Drawing.Color.White
        Me.lblReg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblReg.Location = New System.Drawing.Point(75, 457)
        Me.lblReg.Name = "lblReg"
        Me.lblReg.Size = New System.Drawing.Size(114, 38)
        Me.lblReg.TabIndex = 191
        Me.lblReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblFN
        '
        Me.lblFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblFN.BackColor = System.Drawing.Color.White
        Me.lblFN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFN.Location = New System.Drawing.Point(263, 457)
        Me.lblFN.Name = "lblFN"
        Me.lblFN.Size = New System.Drawing.Size(114, 38)
        Me.lblFN.TabIndex = 192
        Me.lblFN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlCode
        '
        Me.pnlCode.Controls.Add(Me.lstCode)
        Me.pnlCode.Location = New System.Drawing.Point(3, 364)
        Me.pnlCode.Name = "pnlCode"
        Me.pnlCode.Size = New System.Drawing.Size(615, 90)
        Me.pnlCode.TabIndex = 196
        Me.pnlCode.Visible = False
        '
        'lstCode
        '
        Me.lstCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstCode.ItemHeight = 12
        Me.lstCode.Location = New System.Drawing.Point(3, 0)
        Me.lstCode.Name = "lstCode"
        Me.lstCode.Size = New System.Drawing.Size(612, 88)
        Me.lstCode.TabIndex = 167
        Me.lstCode.Visible = False
        '
        'AxRstInput_poct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ContextMenuStrip = Me.cmuLink
        Me.Controls.Add(Me.pnlCode)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblReg)
        Me.Controls.Add(Me.lblFN)
        Me.Controls.Add(Me.txtBcNo)
        Me.Controls.Add(Me.lblcasegbn)
        Me.Controls.Add(Me.txtTestCd)
        Me.Controls.Add(Me.txtOrgRst)
        Me.Controls.Add(Me.chkSelect)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstEx)
        Me.Controls.Add(Me.spdResult)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "AxRstInput_poct"
        Me.Size = New System.Drawing.Size(941, 495)
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmuLink.ResumeLayout(False)
        Me.pnlCode.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents spdResult As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstEx As System.Windows.Forms.ListBox
    Friend WithEvents chkSelect As System.Windows.Forms.CheckBox
    Friend WithEvents txtOrgRst As System.Windows.Forms.TextBox
    Friend WithEvents cmuLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuSpRst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblcasegbn As System.Windows.Forms.Label
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblReg As System.Windows.Forms.Label
    Friend WithEvents lblFN As System.Windows.Forms.Label
    Friend WithEvents pnlCode As System.Windows.Forms.Panel
    Friend WithEvents lstCode As System.Windows.Forms.ListBox

End Class
