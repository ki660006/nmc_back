<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.cboTgrpCd = New System.Windows.Forms.ComboBox
        Me.axItemSave = New AxAckItemSave.ITEMSAVE
        Me.SuspendLayout()
        '
        'cboTgrpCd
        '
        Me.cboTgrpCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTgrpCd.FormattingEnabled = True
        Me.cboTgrpCd.Location = New System.Drawing.Point(10, 12)
        Me.cboTgrpCd.Name = "cboTgrpCd"
        Me.cboTgrpCd.Size = New System.Drawing.Size(225, 20)
        Me.cboTgrpCd.TabIndex = 0
        '
        'axItemSave
        '
        Me.axItemSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axItemSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axItemSave.Location = New System.Drawing.Point(10, 36)
        Me.axItemSave.Margin = New System.Windows.Forms.Padding(1)
        Me.axItemSave.Name = "axItemSave"
        Me.axItemSave.Size = New System.Drawing.Size(441, 140)
        Me.axItemSave.TabIndex = 1
        '
        'FGR99
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 266)
        Me.Controls.Add(Me.axItemSave)
        Me.Controls.Add(Me.cboTgrpCd)
        Me.Name = "FGR99"
        Me.Text = "FGR99"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboTgrpCd As System.Windows.Forms.ComboBox
    Friend WithEvents axItemSave As AxAckItemSave.ITEMSAVE
End Class
