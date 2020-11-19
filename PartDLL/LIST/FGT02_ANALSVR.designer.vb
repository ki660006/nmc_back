<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGT02_ANALSVR
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGT02_ANALSVR))
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.lblAnalDay = New System.Windows.Forms.Label
        Me.lblProgTot = New System.Windows.Forms.Label
        Me.pgbAnalysisTot = New System.Windows.Forms.ProgressBar
        Me.lblAnalTCd = New System.Windows.Forms.Label
        Me.lblProgDay = New System.Windows.Forms.Label
        Me.pgbAnalysisDay = New System.Windows.Forms.ProgressBar
        Me.pnlLeft = New System.Windows.Forms.Panel
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnAnalysis = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lblHypn = New System.Windows.Forms.Label
        Me.dtpDayE = New System.Windows.Forms.DateTimePicker
        Me.dtpDayB = New System.Windows.Forms.DateTimePicker
        Me.lblDay = New System.Windows.Forms.Label
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.pnlBottom.SuspendLayout()
        Me.pnlLeft.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.lblAnalDay)
        Me.pnlBottom.Controls.Add(Me.lblProgTot)
        Me.pnlBottom.Controls.Add(Me.pgbAnalysisTot)
        Me.pnlBottom.Controls.Add(Me.lblAnalTCd)
        Me.pnlBottom.Controls.Add(Me.lblProgDay)
        Me.pnlBottom.Controls.Add(Me.pgbAnalysisDay)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 487)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(438, 44)
        Me.pnlBottom.TabIndex = 1
        '
        'lblAnalDay
        '
        Me.lblAnalDay.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblAnalDay.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnalDay.Location = New System.Drawing.Point(84, 21)
        Me.lblAnalDay.Name = "lblAnalDay"
        Me.lblAnalDay.Size = New System.Drawing.Size(64, 11)
        Me.lblAnalDay.TabIndex = 24
        Me.lblAnalDay.Text = "20070101"
        Me.lblAnalDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAnalDay.UseCompatibleTextRendering = True
        '
        'lblProgTot
        '
        Me.lblProgTot.AutoSize = True
        Me.lblProgTot.Location = New System.Drawing.Point(8, 20)
        Me.lblProgTot.Name = "lblProgTot"
        Me.lblProgTot.Size = New System.Drawing.Size(77, 12)
        Me.lblProgTot.TabIndex = 3
        Me.lblProgTot.Text = "전체분석상황"
        '
        'pgbAnalysisTot
        '
        Me.pgbAnalysisTot.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pgbAnalysisTot.Location = New System.Drawing.Point(150, 18)
        Me.pgbAnalysisTot.Name = "pgbAnalysisTot"
        Me.pgbAnalysisTot.Size = New System.Drawing.Size(285, 14)
        Me.pgbAnalysisTot.TabIndex = 0
        '
        'lblAnalTCd
        '
        Me.lblAnalTCd.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblAnalTCd.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnalTCd.Location = New System.Drawing.Point(84, 7)
        Me.lblAnalTCd.Name = "lblAnalTCd"
        Me.lblAnalTCd.Size = New System.Drawing.Size(64, 16)
        Me.lblAnalTCd.TabIndex = 23
        Me.lblAnalTCd.Text = "LVB0101"
        Me.lblAnalTCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAnalTCd.UseCompatibleTextRendering = True
        Me.lblAnalTCd.Visible = False
        '
        'lblProgDay
        '
        Me.lblProgDay.AutoSize = True
        Me.lblProgDay.Location = New System.Drawing.Point(8, 9)
        Me.lblProgDay.Name = "lblProgDay"
        Me.lblProgDay.Size = New System.Drawing.Size(77, 12)
        Me.lblProgDay.TabIndex = 2
        Me.lblProgDay.Text = "일별분석상황"
        Me.lblProgDay.Visible = False
        '
        'pgbAnalysisDay
        '
        Me.pgbAnalysisDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pgbAnalysisDay.Location = New System.Drawing.Point(150, 6)
        Me.pgbAnalysisDay.Name = "pgbAnalysisDay"
        Me.pgbAnalysisDay.Size = New System.Drawing.Size(285, 18)
        Me.pgbAnalysisDay.TabIndex = 1
        Me.pgbAnalysisDay.Visible = False
        '
        'pnlLeft
        '
        Me.pnlLeft.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlLeft.Controls.Add(Me.btnClose)
        Me.pnlLeft.Controls.Add(Me.btnAnalysis)
        Me.pnlLeft.Controls.Add(Me.btnSearch)
        Me.pnlLeft.Controls.Add(Me.lblHypn)
        Me.pnlLeft.Controls.Add(Me.dtpDayE)
        Me.pnlLeft.Controls.Add(Me.dtpDayB)
        Me.pnlLeft.Controls.Add(Me.lblDay)
        Me.pnlLeft.Controls.Add(Me.spdList)
        Me.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(438, 487)
        Me.pnlLeft.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClose.Location = New System.Drawing.Point(326, 444)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 38)
        Me.btnClose.TabIndex = 22
        Me.btnClose.Text = "닫기(Esc)"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnAnalysis
        '
        Me.btnAnalysis.Location = New System.Drawing.Point(220, 444)
        Me.btnAnalysis.Name = "btnAnalysis"
        Me.btnAnalysis.Size = New System.Drawing.Size(100, 38)
        Me.btnAnalysis.TabIndex = 21
        Me.btnAnalysis.Text = "(재)분석 시작"
        Me.btnAnalysis.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(338, 8)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(88, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.Text = "분석여부조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblHypn
        '
        Me.lblHypn.AutoSize = True
        Me.lblHypn.Location = New System.Drawing.Point(228, 13)
        Me.lblHypn.Name = "lblHypn"
        Me.lblHypn.Size = New System.Drawing.Size(14, 12)
        Me.lblHypn.TabIndex = 5
        Me.lblHypn.Text = "~"
        '
        'dtpDayE
        '
        Me.dtpDayE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayE.Location = New System.Drawing.Point(244, 9)
        Me.dtpDayE.Name = "dtpDayE"
        Me.dtpDayE.Size = New System.Drawing.Size(89, 21)
        Me.dtpDayE.TabIndex = 3
        '
        'dtpDayB
        '
        Me.dtpDayB.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayB.Location = New System.Drawing.Point(136, 9)
        Me.dtpDayB.Name = "dtpDayB"
        Me.dtpDayB.Size = New System.Drawing.Size(89, 21)
        Me.dtpDayB.TabIndex = 2
        '
        'lblDay
        '
        Me.lblDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDay.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDay.ForeColor = System.Drawing.Color.White
        Me.lblDay.Location = New System.Drawing.Point(12, 9)
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(123, 21)
        Me.lblDay.TabIndex = 1
        Me.lblDay.Text = "통계일자"
        Me.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(12, 34)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(413, 402)
        Me.spdList.TabIndex = 0
        '
        'FGT02_ANALSVR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(438, 531)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.pnlBottom)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FGT02_ANALSVR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "검사통계 분석 및 재분석"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeft.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents dtpDayB As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDay As System.Windows.Forms.Label
    Friend WithEvents dtpDayE As System.Windows.Forms.DateTimePicker
    Friend WithEvents pgbAnalysisTot As System.Windows.Forms.ProgressBar
    Friend WithEvents lblHypn As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnAnalysis As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents pgbAnalysisDay As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgDay As System.Windows.Forms.Label
    Friend WithEvents lblProgTot As System.Windows.Forms.Label
    Friend WithEvents lblAnalTCd As System.Windows.Forms.Label
    Friend WithEvents lblAnalDay As System.Windows.Forms.Label
End Class
