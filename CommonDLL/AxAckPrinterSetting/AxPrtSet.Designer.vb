<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxPrtSet
    Inherits System.Windows.Forms.UserControl

    'UserControl1은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Me.pnlPrtSet = New System.Windows.Forms.Panel
        Me.cboPrinters = New System.Windows.Forms.ComboBox
        Me.lblTitle = New System.Windows.Forms.Label
        Me.pnlPrtSet.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlPrtSet
        '
        Me.pnlPrtSet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPrtSet.Controls.Add(Me.cboPrinters)
        Me.pnlPrtSet.Controls.Add(Me.lblTitle)
        Me.pnlPrtSet.Location = New System.Drawing.Point(2, 2)
        Me.pnlPrtSet.Name = "pnlPrtSet"
        Me.pnlPrtSet.Size = New System.Drawing.Size(267, 26)
        Me.pnlPrtSet.TabIndex = 8
        '
        'cboPrinters
        '
        Me.cboPrinters.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrinters.BackColor = System.Drawing.SystemColors.Window
        Me.cboPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrinters.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cboPrinters.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPrinters.FormattingEnabled = True
        Me.cboPrinters.Items.AddRange(New Object() {"디버그", "빌드"})
        Me.cboPrinters.Location = New System.Drawing.Point(83, 2)
        Me.cboPrinters.Margin = New System.Windows.Forms.Padding(0)
        Me.cboPrinters.MaxDropDownItems = 10
        Me.cboPrinters.Name = "cboPrinters"
        Me.cboPrinters.Size = New System.Drawing.Size(182, 21)
        Me.cboPrinters.TabIndex = 104
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.DarkSlateBlue
        Me.lblTitle.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(2, 2)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(80, 22)
        Me.lblTitle.TabIndex = 101
        Me.lblTitle.Text = "바코드프린터"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTitle.UseCompatibleTextRendering = True
        '
        'AxPrtSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.pnlPrtSet)
        Me.Name = "AxPrtSet"
        Me.Size = New System.Drawing.Size(271, 30)
        Me.pnlPrtSet.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlPrtSet As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents cboPrinters As System.Windows.Forms.ComboBox

End Class
