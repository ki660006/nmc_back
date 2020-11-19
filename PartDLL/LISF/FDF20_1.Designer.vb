<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF20_1
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
        Me.rtbPrint = New System.Windows.Forms.RichTextBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.rtbSt = New AxAckRichTextBox.AxAckRichTextBox
        Me.SuspendLayout()
        '
        'rtbPrint
        '
        Me.rtbPrint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtbPrint.Location = New System.Drawing.Point(12, 6)
        Me.rtbPrint.Name = "rtbPrint"
        Me.rtbPrint.Size = New System.Drawing.Size(737, 957)
        Me.rtbPrint.TabIndex = 183
        Me.rtbPrint.Text = ""
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(12, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(96, 26)
        Me.btnPrint.TabIndex = 184
        Me.btnPrint.Text = "보고서 출력"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'rtbSt
        '
        Me.rtbSt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rtbSt.Location = New System.Drawing.Point(12, 6)
        Me.rtbSt.Name = "rtbSt"
        Me.rtbSt.Size = New System.Drawing.Size(686, 957)
        Me.rtbSt.TabIndex = 12
        Me.rtbSt.Visible = False
        '
        'FDF20_1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(773, 975)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.rtbPrint)
        Me.Controls.Add(Me.rtbSt)
        Me.Name = "FDF20_1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FDF20_1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbSt As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents rtbPrint As System.Windows.Forms.RichTextBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
End Class
