<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR09
    'Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR09))
        Me.pnlOptUser = New System.Windows.Forms.Panel
        Me.rdoUserOt = New System.Windows.Forms.RadioButton
        Me.rdoUserMe = New System.Windows.Forms.RadioButton
        Me.btnTkCc = New System.Windows.Forms.Button
        Me.btnAdd_Test = New System.Windows.Forms.Button
        Me.pnlBottom.SuspendLayout()
        Me.grpInput.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.spdSpTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlOptUser.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnAdd_Test)
        Me.pnlBottom.Controls.Add(Me.btnTkCc)
        Me.pnlBottom.Controls.SetChildIndex(Me.btnTkCc, 0)
        Me.pnlBottom.Controls.SetChildIndex(Me.btnAdd_Test, 0)
        '
        'grpInput
        '
        Me.grpInput.Size = New System.Drawing.Size(298, 161)
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(3, 177)
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.Images.SetKeyName(0, "")
        '
        'spdSpTest
        '
        Me.spdSpTest.OcxState = CType(resources.GetObject("spdSpTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSpTest.Size = New System.Drawing.Size(287, 57)
        '
        'spdList
        '
        Me.spdList.Location = New System.Drawing.Point(3, 220)
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        '
        'pnlOptUser
        '
        Me.pnlOptUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlOptUser.Controls.Add(Me.rdoUserOt)
        Me.pnlOptUser.Controls.Add(Me.rdoUserMe)
        Me.pnlOptUser.Location = New System.Drawing.Point(3, 160)
        Me.pnlOptUser.Name = "pnlOptUser"
        Me.pnlOptUser.Size = New System.Drawing.Size(298, 22)
        Me.pnlOptUser.TabIndex = 185
        '
        'rdoUserOt
        '
        Me.rdoUserOt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoUserOt.ForeColor = System.Drawing.Color.Black
        Me.rdoUserOt.Location = New System.Drawing.Point(161, 3)
        Me.rdoUserOt.Name = "rdoUserOt"
        Me.rdoUserOt.Size = New System.Drawing.Size(121, 18)
        Me.rdoUserOt.TabIndex = 1
        Me.rdoUserOt.Text = "전문의(타인) 접수"
        '
        'rdoUserMe
        '
        Me.rdoUserMe.Checked = True
        Me.rdoUserMe.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoUserMe.ForeColor = System.Drawing.Color.Black
        Me.rdoUserMe.Location = New System.Drawing.Point(22, 3)
        Me.rdoUserMe.Name = "rdoUserMe"
        Me.rdoUserMe.Size = New System.Drawing.Size(121, 18)
        Me.rdoUserMe.TabIndex = 0
        Me.rdoUserMe.TabStop = True
        Me.rdoUserMe.Text = "전문의(본인) 접수"
        '
        'btnTkCc
        '
        Me.btnTkCc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTkCc.BackColor = System.Drawing.Color.AliceBlue
        Me.btnTkCc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnTkCc.Location = New System.Drawing.Point(0, 4)
        Me.btnTkCc.Name = "btnTkCc"
        Me.btnTkCc.Size = New System.Drawing.Size(112, 25)
        Me.btnTkCc.TabIndex = 218
        Me.btnTkCc.Tag = "종합검증 대상자 접수/취소"
        Me.btnTkCc.Text = "→ 대상자 접수"
        Me.btnTkCc.UseVisualStyleBackColor = False
        '
        'btnAdd_Test
        '
        Me.btnAdd_Test.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAdd_Test.BackColor = System.Drawing.Color.AliceBlue
        Me.btnAdd_Test.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd_Test.Location = New System.Drawing.Point(112, 4)
        Me.btnAdd_Test.Name = "btnAdd_Test"
        Me.btnAdd_Test.Size = New System.Drawing.Size(92, 25)
        Me.btnAdd_Test.TabIndex = 219
        Me.btnAdd_Test.Tag = "종합검증 대상자 접수/취소"
        Me.btnAdd_Test.Text = "→ 소견 추가"
        Me.btnAdd_Test.UseVisualStyleBackColor = False
        '
        'FGR09
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.ClientSize = New System.Drawing.Size(1031, 667)
        Me.Controls.Add(Me.pnlOptUser)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "FGR09"
        Me.Controls.SetChildIndex(Me.spdList, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.Controls.SetChildIndex(Me.pnlBottom, 0)
        Me.Controls.SetChildIndex(Me.grpInput, 0)
        Me.Controls.SetChildIndex(Me.pnlOptUser, 0)
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.grpInput.ResumeLayout(False)
        Me.grpInput.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.spdSpTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlOptUser.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlOptUser As System.Windows.Forms.Panel
    Friend WithEvents rdoUserOt As System.Windows.Forms.RadioButton
    Friend WithEvents rdoUserMe As System.Windows.Forms.RadioButton
    Friend WithEvents btnTkCc As System.Windows.Forms.Button
    Friend WithEvents btnAdd_Test As System.Windows.Forms.Button

End Class
