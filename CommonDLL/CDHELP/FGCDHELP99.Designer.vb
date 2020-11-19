<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCDHELP99
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCDHELP99))
        Me.Label98 = New System.Windows.Forms.Label
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cboGubun = New System.Windows.Forms.ComboBox
        Me.grbBottom = New System.Windows.Forms.GroupBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnChoose = New System.Windows.Forms.Button
        Me.pnlSpread = New System.Windows.Forms.Panel
        Me.spdSearchList = New AxFPSpreadADO.AxfpSpread
        Me.pnlHeader.SuspendLayout()
        Me.grbBottom.SuspendLayout()
        Me.pnlSpread.SuspendLayout()
        CType(Me.spdSearchList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label98.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label98.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.White
        Me.Label98.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label98.Location = New System.Drawing.Point(2, 4)
        Me.Label98.Margin = New System.Windows.Forms.Padding(1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(80, 21)
        Me.Label98.TabIndex = 101
        Me.Label98.Text = "조회구분"
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.Controls.Add(Me.btnSearch)
        Me.pnlHeader.Controls.Add(Me.txtSearch)
        Me.pnlHeader.Controls.Add(Me.cboGubun)
        Me.pnlHeader.Controls.Add(Me.Label98)
        Me.pnlHeader.Location = New System.Drawing.Point(2, 2)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(399, 30)
        Me.pnlHeader.TabIndex = 102
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearch.Location = New System.Drawing.Point(302, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(94, 23)
        Me.btnSearch.TabIndex = 104
        Me.btnSearch.Text = "조 회(F6)"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(167, 5)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(1)
        Me.txtSearch.MaxLength = 8
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(134, 21)
        Me.txtSearch.TabIndex = 103
        '
        'cboGubun
        '
        Me.cboGubun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGubun.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboGubun.FormattingEnabled = True
        Me.cboGubun.Location = New System.Drawing.Point(83, 5)
        Me.cboGubun.Margin = New System.Windows.Forms.Padding(1)
        Me.cboGubun.MaxDropDownItems = 20
        Me.cboGubun.Name = "cboGubun"
        Me.cboGubun.Size = New System.Drawing.Size(82, 20)
        Me.cboGubun.TabIndex = 102
        '
        'grbBottom
        '
        Me.grbBottom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbBottom.Controls.Add(Me.btnExit)
        Me.grbBottom.Controls.Add(Me.btnChoose)
        Me.grbBottom.Location = New System.Drawing.Point(2, 372)
        Me.grbBottom.Name = "grbBottom"
        Me.grbBottom.Size = New System.Drawing.Size(396, 41)
        Me.grbBottom.TabIndex = 193
        Me.grbBottom.TabStop = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(299, 13)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(94, 23)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "종 료(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnChoose
        '
        Me.btnChoose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChoose.Location = New System.Drawing.Point(206, 13)
        Me.btnChoose.Name = "btnChoose"
        Me.btnChoose.Size = New System.Drawing.Size(94, 23)
        Me.btnChoose.TabIndex = 105
        Me.btnChoose.Text = "선 택(F4)"
        Me.btnChoose.UseVisualStyleBackColor = True
        '
        'pnlSpread
        '
        Me.pnlSpread.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSpread.Controls.Add(Me.spdSearchList)
        Me.pnlSpread.Location = New System.Drawing.Point(2, 32)
        Me.pnlSpread.Name = "pnlSpread"
        Me.pnlSpread.Size = New System.Drawing.Size(399, 347)
        Me.pnlSpread.TabIndex = 194
        '
        'spdSearchList
        '
        Me.spdSearchList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdSearchList.Location = New System.Drawing.Point(0, 0)
        Me.spdSearchList.Name = "spdSearchList"
        Me.spdSearchList.OcxState = CType(resources.GetObject("spdSearchList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSearchList.Size = New System.Drawing.Size(399, 347)
        Me.spdSearchList.TabIndex = 0
        '
        'FGCDHELP99
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(402, 416)
        Me.Controls.Add(Me.pnlSpread)
        Me.Controls.Add(Me.grbBottom)
        Me.Controls.Add(Me.pnlHeader)
        Me.KeyPreview = True
        Me.Name = "FGCDHELP99"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FGCDHELP99"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.grbBottom.ResumeLayout(False)
        Me.pnlSpread.ResumeLayout(False)
        CType(Me.spdSearchList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents cboGubun As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents grbBottom As System.Windows.Forms.GroupBox
    Friend WithEvents pnlSpread As System.Windows.Forms.Panel
    Friend WithEvents spdSearchList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnChoose As System.Windows.Forms.Button
End Class
