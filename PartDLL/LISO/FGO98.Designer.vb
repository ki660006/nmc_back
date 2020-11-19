<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGO98
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnToJpg = New System.Windows.Forms.Button()
        Me.rtbStRst = New AxAckRichTextBox.AxAckRichTextBox()
        Me.btnFile = New System.Windows.Forms.Button()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.btnEMR = New System.Windows.Forms.Button()
        Me.btnTestDataClear = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(78, 24)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(134, 21)
        Me.TextBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "검체번호"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "RTF-to-jpg 테스트"
        '
        'btnToJpg
        '
        Me.btnToJpg.Location = New System.Drawing.Point(231, 22)
        Me.btnToJpg.Name = "btnToJpg"
        Me.btnToJpg.Size = New System.Drawing.Size(75, 23)
        Me.btnToJpg.TabIndex = 3
        Me.btnToJpg.Text = "로컬 저장"
        Me.btnToJpg.UseVisualStyleBackColor = True
        '
        'rtbStRst
        '
        Me.rtbStRst.Location = New System.Drawing.Point(644, 9)
        Me.rtbStRst.Name = "rtbStRst"
        Me.rtbStRst.Size = New System.Drawing.Size(142, 165)
        Me.rtbStRst.TabIndex = 4
        '
        'btnFile
        '
        Me.btnFile.Location = New System.Drawing.Point(21, 91)
        Me.btnFile.Name = "btnFile"
        Me.btnFile.Size = New System.Drawing.Size(162, 46)
        Me.btnFile.TabIndex = 5
        Me.btnFile.Text = "파일존재여부테스트"
        Me.btnFile.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(21, 64)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(162, 21)
        Me.TextBox2.TabIndex = 6
        '
        'btnEMR
        '
        Me.btnEMR.Location = New System.Drawing.Point(23, 166)
        Me.btnEMR.Name = "btnEMR"
        Me.btnEMR.Size = New System.Drawing.Size(189, 87)
        Me.btnEMR.TabIndex = 7
        Me.btnEMR.Text = "MTS emr 업데이트"
        Me.btnEMR.UseVisualStyleBackColor = True
        '
        'btnTestDataClear
        '
        Me.btnTestDataClear.Location = New System.Drawing.Point(261, 166)
        Me.btnTestDataClear.Name = "btnTestDataClear"
        Me.btnTestDataClear.Size = New System.Drawing.Size(307, 87)
        Me.btnTestDataClear.TabIndex = 8
        Me.btnTestDataClear.Text = "btnTestDataClear"
        Me.btnTestDataClear.UseVisualStyleBackColor = True
        '
        'FGO98
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(798, 601)
        Me.Controls.Add(Me.btnTestDataClear)
        Me.Controls.Add(Me.btnEMR)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.btnFile)
        Me.Controls.Add(Me.rtbStRst)
        Me.Controls.Add(Me.btnToJpg)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "FGO98"
        Me.Text = "테스트폼"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnToJpg As System.Windows.Forms.Button
    Friend WithEvents rtbStRst As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents btnFile As System.Windows.Forms.Button
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents btnEMR As System.Windows.Forms.Button
    Friend WithEvents btnTestDataClear As System.Windows.Forms.Button
End Class
