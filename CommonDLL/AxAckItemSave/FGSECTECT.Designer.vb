<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGSECTECT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGSECTECT))
        Me.spdTestList = New AxFPSpreadADO.AxfpSpread
        Me.label56 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboSlipCd = New System.Windows.Forms.ComboBox
        Me.spdSelList = New AxFPSpreadADO.AxfpSpread
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnDown = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.btnDel = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.cboSaveNm = New System.Windows.Forms.ComboBox
        CType(Me.spdTestList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdSelList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdTestList
        '
        Me.spdTestList.Location = New System.Drawing.Point(10, 33)
        Me.spdTestList.Name = "spdTestList"
        Me.spdTestList.OcxState = CType(resources.GetObject("spdTestList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTestList.Size = New System.Drawing.Size(261, 332)
        Me.spdTestList.TabIndex = 0
        '
        'label56
        '
        Me.label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.label56.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.label56.ForeColor = System.Drawing.Color.White
        Me.label56.Location = New System.Drawing.Point(348, 10)
        Me.label56.Margin = New System.Windows.Forms.Padding(0)
        Me.label56.Name = "label56"
        Me.label56.Size = New System.Drawing.Size(79, 21)
        Me.label56.TabIndex = 36
        Me.label56.Text = "저장이름"
        Me.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 21)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "검사분류"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboSlipCd
        '
        Me.cboSlipCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlipCd.FormattingEnabled = True
        Me.cboSlipCd.Location = New System.Drawing.Point(90, 10)
        Me.cboSlipCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboSlipCd.Name = "cboSlipCd"
        Me.cboSlipCd.Size = New System.Drawing.Size(181, 20)
        Me.cboSlipCd.TabIndex = 39
        '
        'spdSelList
        '
        Me.spdSelList.Location = New System.Drawing.Point(348, 34)
        Me.spdSelList.Name = "spdSelList"
        Me.spdSelList.OcxState = CType(resources.GetObject("spdSelList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSelList.Size = New System.Drawing.Size(310, 331)
        Me.spdSelList.TabIndex = 40
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 374)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(670, 30)
        Me.Panel1.TabIndex = 41
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(482, 3)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(87, 26)
        Me.btnClear.TabIndex = 2
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(571, 3)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(87, 26)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(393, 3)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(87, 26)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(630, 10)
        Me.btnDown.Margin = New System.Windows.Forms.Padding(1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(28, 21)
        Me.btnDown.TabIndex = 43
        Me.btnDown.Text = "▼"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(600, 10)
        Me.btnUp.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(30, 21)
        Me.btnUp.TabIndex = 42
        Me.btnUp.Text = "▲"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(277, 130)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(65, 26)
        Me.btnDel.TabIndex = 45
        Me.btnDel.Text = "<<"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(277, 98)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(65, 26)
        Me.btnAdd.TabIndex = 44
        Me.btnAdd.Text = ">>"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'cboSaveNm
        '
        Me.cboSaveNm.FormattingEnabled = True
        Me.cboSaveNm.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cboSaveNm.Location = New System.Drawing.Point(428, 10)
        Me.cboSaveNm.Margin = New System.Windows.Forms.Padding(1)
        Me.cboSaveNm.Name = "cboSaveNm"
        Me.cboSaveNm.Size = New System.Drawing.Size(170, 20)
        Me.cboSaveNm.Sorted = True
        Me.cboSaveNm.TabIndex = 46
        '
        'FGSECTECT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(670, 404)
        Me.Controls.Add(Me.cboSaveNm)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.spdSelList)
        Me.Controls.Add(Me.cboSlipCd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.label56)
        Me.Controls.Add(Me.spdTestList)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGSECTECT"
        Me.Text = "검사선택"
        CType(Me.spdTestList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdSelList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdTestList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents label56 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboSlipCd As System.Windows.Forms.ComboBox
    Friend WithEvents spdSelList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents cboSaveNm As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
