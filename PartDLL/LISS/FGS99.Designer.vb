<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS99
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS99))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdExcel = New AxFPSpreadADO.AxfpSpread
        Me.lblUSDayTime = New System.Windows.Forms.Label
        Me.btnExcelOpen = New System.Windows.Forms.Button
        Me.txtPath = New System.Windows.Forms.TextBox
        Me.ofdExLab = New System.Windows.Forms.OpenFileDialog
        Me.fbdPath = New System.Windows.Forms.FolderBrowserDialog
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnMatch = New System.Windows.Forms.Button
        Me.btnExcel = New System.Windows.Forms.Button
        Me.Panel1.SuspendLayout()
        CType(Me.spdExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.spdExcel)
        Me.Panel1.Location = New System.Drawing.Point(1, 44)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(815, 576)
        Me.Panel1.TabIndex = 0
        '
        'spdExcel
        '
        Me.spdExcel.DataSource = Nothing
        Me.spdExcel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdExcel.Location = New System.Drawing.Point(0, 0)
        Me.spdExcel.Name = "spdExcel"
        Me.spdExcel.OcxState = CType(resources.GetObject("spdExcel.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdExcel.Size = New System.Drawing.Size(815, 576)
        Me.spdExcel.TabIndex = 0
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(10, 13)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(99, 21)
        Me.lblUSDayTime.TabIndex = 3
        Me.lblUSDayTime.Text = "EXCEL 파일내용"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnExcelOpen
        '
        Me.btnExcelOpen.Location = New System.Drawing.Point(115, 11)
        Me.btnExcelOpen.Name = "btnExcelOpen"
        Me.btnExcelOpen.Size = New System.Drawing.Size(116, 23)
        Me.btnExcelOpen.TabIndex = 5
        Me.btnExcelOpen.Text = "execl 파일 열기"
        Me.btnExcelOpen.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(237, 11)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(400, 21)
        Me.txtPath.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnExcel)
        Me.GroupBox1.Controls.Add(Me.btnMatch)
        Me.GroupBox1.Controls.Add(Me.lblUSDayTime)
        Me.GroupBox1.Controls.Add(Me.txtPath)
        Me.GroupBox1.Controls.Add(Me.btnExcelOpen)
        Me.GroupBox1.Location = New System.Drawing.Point(2, -3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(814, 43)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'btnMatch
        '
        Me.btnMatch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMatch.Location = New System.Drawing.Point(647, 10)
        Me.btnMatch.Name = "btnMatch"
        Me.btnMatch.Size = New System.Drawing.Size(75, 23)
        Me.btnMatch.TabIndex = 7
        Me.btnMatch.Text = "MATCH"
        Me.btnMatch.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Location = New System.Drawing.Point(728, 11)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(75, 23)
        Me.btnExcel.TabIndex = 8
        Me.btnExcel.Text = "Excel출력"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'FGS99
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(817, 632)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FGS99"
        Me.Text = "FGS99"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdExcel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Friend WithEvents spdExcel As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnExcelOpen As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents ofdExLab As System.Windows.Forms.OpenFileDialog
    Friend WithEvents fbdPath As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnMatch As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
End Class
