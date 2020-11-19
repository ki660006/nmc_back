<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGO97
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
        Me.txtRegno = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtRegno
        '
        Me.txtRegno.Location = New System.Drawing.Point(95, 35)
        Me.txtRegno.Name = "txtRegno"
        Me.txtRegno.Size = New System.Drawing.Size(132, 21)
        Me.txtRegno.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "mts0002_emr  update"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.Navy
        Me.lblSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSearch.Location = New System.Drawing.Point(12, 35)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(3)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(79, 22)
        Me.lblSearch.TabIndex = 4
        Me.lblSearch.Tag = "등록번호"
        Me.lblSearch.Text = "등록번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGO97
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 545)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtRegno)
        Me.Name = "FGO97"
        Me.Text = "환자정보 깨진것 update"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRegno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Protected Friend WithEvents lblSearch As System.Windows.Forms.Label
End Class
