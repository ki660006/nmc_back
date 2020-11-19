<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxCalcResult
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxCalcResult))
        Me.btnCalcRst = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnCalcRst
        '
        Me.btnCalcRst.Dock = System.Windows.Forms.DockStyle.Fill
        Me.btnCalcRst.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCalcRst.ForeColor = System.Drawing.Color.Crimson
        Me.btnCalcRst.Image = CType(resources.GetObject("btnCalcRst.Image"), System.Drawing.Image)
        Me.btnCalcRst.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCalcRst.Location = New System.Drawing.Point(0, 0)
        Me.btnCalcRst.Name = "btnCalcRst"
        Me.btnCalcRst.Size = New System.Drawing.Size(104, 26)
        Me.btnCalcRst.TabIndex = 0
        Me.btnCalcRst.Text = "      계산식 결과"
        Me.btnCalcRst.UseVisualStyleBackColor = True
        '
        'AxCalcResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnCalcRst)
        Me.Name = "AxCalcResult"
        Me.Size = New System.Drawing.Size(104, 26)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCalcRst As System.Windows.Forms.Button

End Class
