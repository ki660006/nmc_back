<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPST_PBS_S01
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_PBS_S01))
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.spdFlag = New AxFPSpreadADO.AxfpSpread
        Me.cmuList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAdd = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuDel = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.spdFlag, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmuList.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(674, 513)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(93, 34)
        Me.btnClose.TabIndex = 85
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(575, 513)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 34)
        Me.btnSave.TabIndex = 84
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'spdFlag
        '
        Me.spdFlag.ContextMenuStrip = Me.cmuList
        Me.spdFlag.DataSource = Nothing
        Me.spdFlag.Location = New System.Drawing.Point(12, 12)
        Me.spdFlag.Name = "spdFlag"
        Me.spdFlag.OcxState = CType(resources.GetObject("spdFlag.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdFlag.Size = New System.Drawing.Size(755, 492)
        Me.spdFlag.TabIndex = 86
        '
        'cmuList
        '
        Me.cmuList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAdd, Me.mnuDel})
        Me.cmuList.Name = "cmuRstList"
        Me.cmuList.Size = New System.Drawing.Size(130, 48)
        Me.cmuList.Text = "상황에 맞는 메뉴"
        '
        'mnuAdd
        '
        Me.mnuAdd.Name = "mnuAdd"
        Me.mnuAdd.Size = New System.Drawing.Size(129, 22)
        Me.mnuAdd.Text = "Row 추가"
        '
        'mnuDel
        '
        Me.mnuDel.Name = "mnuDel"
        Me.mnuDel.Size = New System.Drawing.Size(129, 22)
        Me.mnuDel.Text = "Row 삭제"
        '
        'FGPOPUPST_PBS_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(779, 557)
        Me.Controls.Add(Me.spdFlag)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.KeyPreview = True
        Me.Name = "FGPOPUPST_PBS_S01"
        Me.Text = "Flag값에 따른 Comment 설정 "
        CType(Me.spdFlag, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmuList.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents spdFlag As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cmuList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAdd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDel As System.Windows.Forms.ToolStripMenuItem
End Class
