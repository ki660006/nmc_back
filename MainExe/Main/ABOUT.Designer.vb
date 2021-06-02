<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ABOUT
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents TableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents lblCopyright As System.Windows.Forms.Label

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ABOUT))
        Me.TableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.lblHosp = New System.Windows.Forms.LinkLabel()
        Me.lblOs = New System.Windows.Forms.Label()
        Me.lblIPAddr = New System.Windows.Forms.Label()
        Me.lblPCNm = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.picHosp = New System.Windows.Forms.PictureBox()
        Me.picAck = New System.Windows.Forms.PictureBox()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblMemory = New System.Windows.Forms.Label()
        Me.lblAckAs = New System.Windows.Forms.LinkLabel()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.AxMSComm1 = New AxMSCommLib.AxMSComm()
        Me.TableLayoutPanel.SuspendLayout()
        CType(Me.picHosp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AxMSComm1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel
        '
        Me.TableLayoutPanel.AutoSize = True
        Me.TableLayoutPanel.ColumnCount = 2
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.0!))
        Me.TableLayoutPanel.Controls.Add(Me.lblHosp, 0, 3)
        Me.TableLayoutPanel.Controls.Add(Me.lblOs, 0, 7)
        Me.TableLayoutPanel.Controls.Add(Me.lblIPAddr, 0, 6)
        Me.TableLayoutPanel.Controls.Add(Me.lblPCNm, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblProductName, 1, 0)
        Me.TableLayoutPanel.Controls.Add(Me.lblVersion, 1, 1)
        Me.TableLayoutPanel.Controls.Add(Me.lblCopyright, 1, 2)
        Me.TableLayoutPanel.Controls.Add(Me.picHosp, 0, 0)
        Me.TableLayoutPanel.Controls.Add(Me.picAck, 0, 5)
        Me.TableLayoutPanel.Controls.Add(Me.lblDescription, 1, 3)
        Me.TableLayoutPanel.Controls.Add(Me.lblMemory, 1, 8)
        Me.TableLayoutPanel.Controls.Add(Me.lblAckAs, 0, 8)
        Me.TableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel.Location = New System.Drawing.Point(10, 8)
        Me.TableLayoutPanel.Name = "TableLayoutPanel"
        Me.TableLayoutPanel.RowCount = 9
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel.Size = New System.Drawing.Size(507, 220)
        Me.TableLayoutPanel.TabIndex = 1
        '
        'lblHosp
        '
        Me.lblHosp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHosp.AutoSize = True
        Me.lblHosp.Location = New System.Drawing.Point(3, 78)
        Me.lblHosp.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lblHosp.Name = "lblHosp"
        Me.lblHosp.Size = New System.Drawing.Size(171, 12)
        Me.lblHosp.TabIndex = 8
        Me.lblHosp.TabStop = True
        Me.lblHosp.Text = "국립중앙의료원 사이트"
        Me.lblHosp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOs
        '
        Me.lblOs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblOs.Location = New System.Drawing.Point(184, 170)
        Me.lblOs.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblOs.MaximumSize = New System.Drawing.Size(0, 16)
        Me.lblOs.Name = "lblOs"
        Me.lblOs.Size = New System.Drawing.Size(320, 16)
        Me.lblOs.TabIndex = 6
        Me.lblOs.Text = "운영체제"
        Me.lblOs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIPAddr
        '
        Me.lblIPAddr.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblIPAddr.Location = New System.Drawing.Point(184, 145)
        Me.lblIPAddr.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblIPAddr.MaximumSize = New System.Drawing.Size(0, 16)
        Me.lblIPAddr.Name = "lblIPAddr"
        Me.lblIPAddr.Size = New System.Drawing.Size(320, 16)
        Me.lblIPAddr.TabIndex = 5
        Me.lblIPAddr.Text = "IP 주소"
        Me.lblIPAddr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPCNm
        '
        Me.lblPCNm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblPCNm.Location = New System.Drawing.Point(184, 120)
        Me.lblPCNm.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblPCNm.MaximumSize = New System.Drawing.Size(0, 16)
        Me.lblPCNm.Name = "lblPCNm"
        Me.lblPCNm.Size = New System.Drawing.Size(320, 16)
        Me.lblPCNm.TabIndex = 3
        Me.lblPCNm.Text = "PC 이름"
        Me.lblPCNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProductName
        '
        Me.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProductName.Location = New System.Drawing.Point(184, 0)
        Me.lblProductName.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblProductName.MaximumSize = New System.Drawing.Size(0, 16)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(320, 16)
        Me.lblProductName.TabIndex = 0
        Me.lblProductName.Text = "제품 이름"
        Me.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblVersion
        '
        Me.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblVersion.Location = New System.Drawing.Point(184, 25)
        Me.lblVersion.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblVersion.MaximumSize = New System.Drawing.Size(0, 16)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(320, 16)
        Me.lblVersion.TabIndex = 0
        Me.lblVersion.Text = "버전"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCopyright
        '
        Me.lblCopyright.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblCopyright.Location = New System.Drawing.Point(184, 50)
        Me.lblCopyright.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblCopyright.MaximumSize = New System.Drawing.Size(0, 16)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(320, 16)
        Me.lblCopyright.TabIndex = 0
        Me.lblCopyright.Text = "저작권"
        Me.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picHosp
        '
        Me.picHosp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picHosp.Image = CType(resources.GetObject("picHosp.Image"), System.Drawing.Image)
        Me.picHosp.Location = New System.Drawing.Point(3, 3)
        Me.picHosp.Name = "picHosp"
        Me.TableLayoutPanel.SetRowSpan(Me.picHosp, 3)
        Me.picHosp.Size = New System.Drawing.Size(171, 69)
        Me.picHosp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picHosp.TabIndex = 1
        Me.picHosp.TabStop = False
        '
        'picAck
        '
        Me.picAck.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picAck.Image = CType(resources.GetObject("picAck.Image"), System.Drawing.Image)
        Me.picAck.Location = New System.Drawing.Point(3, 123)
        Me.picAck.Name = "picAck"
        Me.TableLayoutPanel.SetRowSpan(Me.picAck, 3)
        Me.picAck.Size = New System.Drawing.Size(171, 69)
        Me.picAck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picAck.TabIndex = 2
        Me.picAck.TabStop = False
        '
        'lblDescription
        '
        Me.lblDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(184, 75)
        Me.lblDescription.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(320, 12)
        Me.lblDescription.TabIndex = 9
        Me.lblDescription.Text = "제품 설명"
        Me.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblMemory
        '
        Me.lblMemory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblMemory.Location = New System.Drawing.Point(184, 195)
        Me.lblMemory.Margin = New System.Windows.Forms.Padding(7, 0, 3, 0)
        Me.lblMemory.MaximumSize = New System.Drawing.Size(0, 16)
        Me.lblMemory.Name = "lblMemory"
        Me.lblMemory.Size = New System.Drawing.Size(320, 16)
        Me.lblMemory.TabIndex = 7
        Me.lblMemory.Text = "메모리"
        Me.lblMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAckAs
        '
        Me.lblAckAs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblAckAs.AutoSize = True
        Me.lblAckAs.Location = New System.Drawing.Point(3, 198)
        Me.lblAckAs.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.lblAckAs.Name = "lblAckAs"
        Me.lblAckAs.Size = New System.Drawing.Size(171, 12)
        Me.lblAckAs.TabIndex = 4
        Me.lblAckAs.TabStop = True
        Me.lblAckAs.Text = "ACK 사이트"
        Me.lblAckAs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OKButton
        '
        Me.OKButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OKButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.OKButton.Location = New System.Drawing.Point(430, 234)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(87, 26)
        Me.OKButton.TabIndex = 0
        Me.OKButton.Text = "확인(&O)"
        '
        'AxMSComm1
        '
        Me.AxMSComm1.Enabled = True
        Me.AxMSComm1.Location = New System.Drawing.Point(0, 0)
        Me.AxMSComm1.Name = "AxMSComm1"
        Me.AxMSComm1.OcxState = CType(resources.GetObject("AxMSComm1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxMSComm1.Size = New System.Drawing.Size(38, 38)
        Me.AxMSComm1.TabIndex = 2
        '
        'ABOUT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(527, 266)
        Me.Controls.Add(Me.AxMSComm1)
        Me.Controls.Add(Me.TableLayoutPanel)
        Me.Controls.Add(Me.OKButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ABOUT"
        Me.Padding = New System.Windows.Forms.Padding(10, 8, 10, 8)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "MEDI@CK 정보"
        Me.TableLayoutPanel.ResumeLayout(False)
        Me.TableLayoutPanel.PerformLayout()
        CType(Me.picHosp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AxMSComm1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picAck As System.Windows.Forms.PictureBox
    Friend WithEvents picHosp As System.Windows.Forms.PictureBox
    Friend WithEvents lblPCNm As System.Windows.Forms.Label
    Friend WithEvents lblAckAs As System.Windows.Forms.LinkLabel
    Friend WithEvents lblIPAddr As System.Windows.Forms.Label
    Friend WithEvents lblOs As System.Windows.Forms.Label
    Friend WithEvents lblMemory As System.Windows.Forms.Label
    Friend WithEvents lblHosp As System.Windows.Forms.LinkLabel
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents AxMSComm1 As AxMSCommLib.AxMSComm

End Class
