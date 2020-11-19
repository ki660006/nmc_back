<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGO99
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGO99))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.dtpTkDtE = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpTkDtS = New System.Windows.Forms.DateTimePicker()
        Me.btnExcute = New System.Windows.Forms.Button()
        Me.lbTotal = New System.Windows.Forms.Label()
        Me.cboPart = New System.Windows.Forms.ComboBox()
        Me.chklimit = New System.Windows.Forms.CheckBox()
        Me.txtlimit = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(12, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(383, 541)
        Me.Panel1.TabIndex = 0
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(383, 541)
        Me.spdList.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(398, 36)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(146, 64)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "조회"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(176, 13)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(14, 12)
        Me.Label32.TabIndex = 22
        Me.Label32.Text = "~"
        '
        'dtpTkDtE
        '
        Me.dtpTkDtE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpTkDtE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkDtE.Location = New System.Drawing.Point(195, 9)
        Me.dtpTkDtE.Name = "dtpTkDtE"
        Me.dtpTkDtE.Size = New System.Drawing.Size(88, 21)
        Me.dtpTkDtE.TabIndex = 20
        Me.dtpTkDtE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Navy
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(10, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 21)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "처방일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpTkDtS
        '
        Me.dtpTkDtS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpTkDtS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkDtS.Location = New System.Drawing.Point(84, 9)
        Me.dtpTkDtS.Name = "dtpTkDtS"
        Me.dtpTkDtS.Size = New System.Drawing.Size(88, 21)
        Me.dtpTkDtS.TabIndex = 19
        Me.dtpTkDtS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'btnExcute
        '
        Me.btnExcute.Location = New System.Drawing.Point(397, 513)
        Me.btnExcute.Name = "btnExcute"
        Me.btnExcute.Size = New System.Drawing.Size(146, 64)
        Me.btnExcute.TabIndex = 23
        Me.btnExcute.Text = "실행"
        Me.btnExcute.UseVisualStyleBackColor = True
        '
        'lbTotal
        '
        Me.lbTotal.AutoSize = True
        Me.lbTotal.Location = New System.Drawing.Point(308, 13)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(85, 12)
        Me.lbTotal.TabIndex = 24
        Me.lbTotal.Text = "              건수"
        '
        'cboPart
        '
        Me.cboPart.FormattingEnabled = True
        Me.cboPart.Items.AddRange(New Object() {"M1"})
        Me.cboPart.Location = New System.Drawing.Point(401, 106)
        Me.cboPart.Name = "cboPart"
        Me.cboPart.Size = New System.Drawing.Size(121, 20)
        Me.cboPart.TabIndex = 25
        '
        'chklimit
        '
        Me.chklimit.AutoSize = True
        Me.chklimit.Location = New System.Drawing.Point(401, 132)
        Me.chklimit.Name = "chklimit"
        Me.chklimit.Size = New System.Drawing.Size(88, 16)
        Me.chklimit.TabIndex = 26
        Me.chklimit.Text = "조회수 제한"
        Me.chklimit.UseVisualStyleBackColor = True
        '
        'txtlimit
        '
        Me.txtlimit.Location = New System.Drawing.Point(490, 127)
        Me.txtlimit.Name = "txtlimit"
        Me.txtlimit.Size = New System.Drawing.Size(54, 21)
        Me.txtlimit.TabIndex = 27
        '
        'FGO99
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 583)
        Me.Controls.Add(Me.txtlimit)
        Me.Controls.Add(Me.chklimit)
        Me.Controls.Add(Me.cboPart)
        Me.Controls.Add(Me.lbTotal)
        Me.Controls.Add(Me.btnExcute)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.dtpTkDtE)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.dtpTkDtS)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FGO99"
        Me.Text = "EMR결과 배치 프로그램 "
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents dtpTkDtE As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpTkDtS As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnExcute As System.Windows.Forms.Button
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents cboPart As System.Windows.Forms.ComboBox
    Friend WithEvents chklimit As System.Windows.Forms.CheckBox
    Friend WithEvents txtlimit As System.Windows.Forms.TextBox
End Class
