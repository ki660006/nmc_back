<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxCollBcNos
    Inherits System.Windows.Forms.UserControl

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Me.lblBcNOsCnt = New System.Windows.Forms.Label
        Me.txtBcNos = New System.Windows.Forms.TextBox
        Me.lblBcNosT = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblBcNOsCnt
        '
        Me.lblBcNOsCnt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.lblBcNOsCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBcNOsCnt.Location = New System.Drawing.Point(128, 2)
        Me.lblBcNOsCnt.Name = "lblBcNOsCnt"
        Me.lblBcNOsCnt.Size = New System.Drawing.Size(36, 22)
        Me.lblBcNOsCnt.TabIndex = 4
        Me.lblBcNOsCnt.Text = "3장"
        Me.lblBcNOsCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBcNOsCnt.UseCompatibleTextRendering = True
        '
        'txtBcNos
        '
        Me.txtBcNos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBcNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNos.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcNos.Location = New System.Drawing.Point(163, 2)
        Me.txtBcNos.Multiline = True
        Me.txtBcNos.Name = "txtBcNos"
        Me.txtBcNos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtBcNos.Size = New System.Drawing.Size(398, 22)
        Me.txtBcNos.TabIndex = 5
        '
        'lblBcNosT
        '
        Me.lblBcNosT.BackColor = System.Drawing.Color.Khaki
        Me.lblBcNosT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcNosT.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblBcNosT.Location = New System.Drawing.Point(2, 2)
        Me.lblBcNosT.Name = "lblBcNosT"
        Me.lblBcNosT.Size = New System.Drawing.Size(126, 22)
        Me.lblBcNosT.TabIndex = 3
        Me.lblBcNosT.Text = "최근 바코드 출력내역"
        Me.lblBcNosT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblBcNosT.UseCompatibleTextRendering = True
        '
        'AxCollBcNos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblBcNOsCnt)
        Me.Controls.Add(Me.txtBcNos)
        Me.Controls.Add(Me.lblBcNosT)
        Me.Name = "AxCollBcNos"
        Me.Size = New System.Drawing.Size(563, 26)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblBcNosT As System.Windows.Forms.Label
    Public WithEvents lblBcNOsCnt As System.Windows.Forms.Label
    Public WithEvents txtBcNos As System.Windows.Forms.TextBox

End Class
