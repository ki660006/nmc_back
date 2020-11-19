<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCancel_BC_PIS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCancel_BC_PIS))
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.lblLabel = New System.Windows.Forms.Label
        Me.cboCancel = New System.Windows.Forms.ComboBox
        Me.txtCmtCont = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnReg = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblRst = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblOrdFlgB = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.lblOrdFlgT = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.lblOrdFlgC = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.lblNoColl = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.lblErFlgE = New System.Windows.Forms.Label
        Me.lblErFlgB = New System.Windows.Forms.Label
        Me.lblNo = New System.Windows.Forms.Label
        Me.txtNo = New System.Windows.Forms.TextBox
        Me.btnToggle = New System.Windows.Forms.Button
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdList
        '
        Me.spdList.Location = New System.Drawing.Point(5, 33)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(467, 175)
        Me.spdList.TabIndex = 0
        '
        'lblLabel
        '
        Me.lblLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblLabel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblLabel.ForeColor = System.Drawing.Color.White
        Me.lblLabel.Location = New System.Drawing.Point(4, 211)
        Me.lblLabel.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLabel.Name = "lblLabel"
        Me.lblLabel.Size = New System.Drawing.Size(68, 21)
        Me.lblLabel.TabIndex = 31
        Me.lblLabel.Text = "취소사유"
        Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCancel
        '
        Me.cboCancel.BackColor = System.Drawing.SystemColors.Window
        Me.cboCancel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboCancel.ItemHeight = 12
        Me.cboCancel.Location = New System.Drawing.Point(74, 211)
        Me.cboCancel.Margin = New System.Windows.Forms.Padding(0)
        Me.cboCancel.Name = "cboCancel"
        Me.cboCancel.Size = New System.Drawing.Size(399, 20)
        Me.cboCancel.TabIndex = 34
        '
        'txtCmtCont
        '
        Me.txtCmtCont.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCont.Location = New System.Drawing.Point(4, 233)
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(469, 100)
        Me.txtCmtCont.TabIndex = 35
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnReg)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 365)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(479, 30)
        Me.Panel1.TabIndex = 190
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg.Location = New System.Drawing.Point(303, 2)
        Me.btnReg.Margin = New System.Windows.Forms.Padding(0)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(84, 25)
        Me.btnReg.TabIndex = 191
        Me.btnReg.Text = "채혈취소"
        Me.btnReg.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(388, 2)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(84, 25)
        Me.btnExit.TabIndex = 190
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.lblRst)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.lblOrdFlgB)
        Me.Panel5.Controls.Add(Me.Label47)
        Me.Panel5.Controls.Add(Me.lblOrdFlgT)
        Me.Panel5.Controls.Add(Me.Label45)
        Me.Panel5.Controls.Add(Me.lblOrdFlgC)
        Me.Panel5.Controls.Add(Me.Label32)
        Me.Panel5.Controls.Add(Me.lblNoColl)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Controls.Add(Me.lblErFlgE)
        Me.Panel5.Controls.Add(Me.lblErFlgB)
        Me.Panel5.Location = New System.Drawing.Point(0, 338)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(479, 27)
        Me.Panel5.TabIndex = 191
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(376, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 22)
        Me.Label2.TabIndex = 211
        Me.Label2.Text = "결과"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRst
        '
        Me.lblRst.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRst.BackColor = System.Drawing.Color.Green
        Me.lblRst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRst.ForeColor = System.Drawing.Color.White
        Me.lblRst.Location = New System.Drawing.Point(358, 5)
        Me.lblRst.Name = "lblRst"
        Me.lblRst.Size = New System.Drawing.Size(18, 16)
        Me.lblRst.TabIndex = 210
        Me.lblRst.Text = "결"
        Me.lblRst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(162, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 22)
        Me.Label1.TabIndex = 209
        Me.Label1.Text = "바코드발행"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOrdFlgB
        '
        Me.lblOrdFlgB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOrdFlgB.BackColor = System.Drawing.Color.Moccasin
        Me.lblOrdFlgB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdFlgB.ForeColor = System.Drawing.Color.Black
        Me.lblOrdFlgB.Location = New System.Drawing.Point(143, 5)
        Me.lblOrdFlgB.Name = "lblOrdFlgB"
        Me.lblOrdFlgB.Size = New System.Drawing.Size(18, 16)
        Me.lblOrdFlgB.TabIndex = 208
        Me.lblOrdFlgB.Text = "바"
        Me.lblOrdFlgB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label47
        '
        Me.Label47.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(319, 2)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(31, 22)
        Me.Label47.TabIndex = 194
        Me.Label47.Text = "접수"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOrdFlgT
        '
        Me.lblOrdFlgT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOrdFlgT.BackColor = System.Drawing.Color.SkyBlue
        Me.lblOrdFlgT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdFlgT.ForeColor = System.Drawing.Color.Black
        Me.lblOrdFlgT.Location = New System.Drawing.Point(301, 5)
        Me.lblOrdFlgT.Name = "lblOrdFlgT"
        Me.lblOrdFlgT.Size = New System.Drawing.Size(18, 16)
        Me.lblOrdFlgT.TabIndex = 193
        Me.lblOrdFlgT.Text = "접"
        Me.lblOrdFlgT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label45.ForeColor = System.Drawing.Color.Black
        Me.Label45.Location = New System.Drawing.Point(258, 2)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(31, 22)
        Me.Label45.TabIndex = 192
        Me.Label45.Text = "채혈"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOrdFlgC
        '
        Me.lblOrdFlgC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOrdFlgC.BackColor = System.Drawing.Color.Goldenrod
        Me.lblOrdFlgC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdFlgC.ForeColor = System.Drawing.Color.Black
        Me.lblOrdFlgC.Location = New System.Drawing.Point(238, 5)
        Me.lblOrdFlgC.Name = "lblOrdFlgC"
        Me.lblOrdFlgC.Size = New System.Drawing.Size(18, 16)
        Me.lblOrdFlgC.TabIndex = 191
        Me.lblOrdFlgC.Text = "채"
        Me.lblOrdFlgC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(90, 2)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(42, 22)
        Me.Label32.TabIndex = 190
        Me.Label32.Text = "미채혈"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNoColl
        '
        Me.lblNoColl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNoColl.BackColor = System.Drawing.Color.White
        Me.lblNoColl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNoColl.ForeColor = System.Drawing.Color.Black
        Me.lblNoColl.Location = New System.Drawing.Point(71, 5)
        Me.lblNoColl.Name = "lblNoColl"
        Me.lblNoColl.Size = New System.Drawing.Size(18, 16)
        Me.lblNoColl.TabIndex = 189
        Me.lblNoColl.Text = "미"
        Me.lblNoColl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(-1, -1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(66, 27)
        Me.Label26.TabIndex = 188
        Me.Label26.Text = "처방범례"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblErFlgE
        '
        Me.lblErFlgE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblErFlgE.BackColor = System.Drawing.Color.White
        Me.lblErFlgE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblErFlgE.Font = New System.Drawing.Font("돋움체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblErFlgE.ForeColor = System.Drawing.Color.Crimson
        Me.lblErFlgE.Location = New System.Drawing.Point(469, 28)
        Me.lblErFlgE.Name = "lblErFlgE"
        Me.lblErFlgE.Size = New System.Drawing.Size(56, 19)
        Me.lblErFlgE.TabIndex = 207
        Me.lblErFlgE.Text = "E 응급"
        Me.lblErFlgE.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.lblErFlgE.UseCompatibleTextRendering = True
        Me.lblErFlgE.Visible = False
        '
        'lblErFlgB
        '
        Me.lblErFlgB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblErFlgB.BackColor = System.Drawing.Color.White
        Me.lblErFlgB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblErFlgB.Font = New System.Drawing.Font("돋움체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblErFlgB.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblErFlgB.Location = New System.Drawing.Point(469, 46)
        Me.lblErFlgB.Name = "lblErFlgB"
        Me.lblErFlgB.Size = New System.Drawing.Size(56, 19)
        Me.lblErFlgB.TabIndex = 206
        Me.lblErFlgB.Text = "B 진료전"
        Me.lblErFlgB.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.lblErFlgB.UseCompatibleTextRendering = True
        Me.lblErFlgB.Visible = False
        '
        'lblNo
        '
        Me.lblNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblNo.ForeColor = System.Drawing.Color.White
        Me.lblNo.Location = New System.Drawing.Point(4, 6)
        Me.lblNo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.Size = New System.Drawing.Size(68, 21)
        Me.lblNo.TabIndex = 192
        Me.lblNo.Text = "검체번호"
        Me.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNo
        '
        Me.txtNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNo.Location = New System.Drawing.Point(74, 6)
        Me.txtNo.MaxLength = 15
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(147, 21)
        Me.txtNo.TabIndex = 0
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(222, 6)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(40, 21)
        Me.btnToggle.TabIndex = 193
        Me.btnToggle.Text = "<->"
        '
        'FGCancel_BC
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(479, 395)
        Me.Controls.Add(Me.btnToggle)
        Me.Controls.Add(Me.txtNo)
        Me.Controls.Add(Me.lblNo)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cboCancel)
        Me.Controls.Add(Me.txtCmtCont)
        Me.Controls.Add(Me.lblLabel)
        Me.Controls.Add(Me.spdList)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGCancel_BC"
        Me.Text = "채혈취소"
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblLabel As System.Windows.Forms.Label
    Friend WithEvents cboCancel As System.Windows.Forms.ComboBox
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblOrdFlgB As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents lblOrdFlgT As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents lblOrdFlgC As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents lblNoColl As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblErFlgE As System.Windows.Forms.Label
    Friend WithEvents lblErFlgB As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblRst As System.Windows.Forms.Label
    Friend WithEvents lblNo As System.Windows.Forms.Label
    Protected Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents btnToggle As System.Windows.Forms.Button
End Class
