<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FGCDBLD_TRAN_MGT
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    '코드 편집기에서는 수정하지 마세요.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCDBLD_TRAN_MGT))
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblAge = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblWard_SR = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblDeptNm = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblIdNo = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblSex = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblPatNm = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblTkID = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblOrdDt = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblRegNo = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblDoctor = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTkDt = New System.Windows.Forms.Label()
        Me.lblTnsjubsuno = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblDLMCaller = New System.Windows.Forms.Label()
        Me.txtCMCaller = New System.Windows.Forms.TextBox()
        Me.txtDLMCaller = New System.Windows.Forms.TextBox()
        Me.txtCmtCont = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.btnSave = New CButtonLib.CButton()
        Me.chkALL = New System.Windows.Forms.CheckBox()
        Me.chkExcept = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkCBC = New System.Windows.Forms.CheckBox()
        Me.chkHb10 = New System.Windows.Forms.CheckBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New CButtonLib.CButton()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.lblAge)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.lblWard_SR)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.lblDeptNm)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.lblIdNo)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.lblSex)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.lblPatNm)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.lblTkID)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.lblOrdDt)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.lblRegNo)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblDoctor)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblTkDt)
        Me.GroupBox3.Controls.Add(Me.lblTnsjubsuno)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(423, 180)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'lblAge
        '
        Me.lblAge.BackColor = System.Drawing.Color.White
        Me.lblAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAge.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAge.Location = New System.Drawing.Point(296, 129)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(120, 22)
        Me.lblAge.TabIndex = 31
        Me.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(210, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 22)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "나이"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWard_SR
        '
        Me.lblWard_SR.BackColor = System.Drawing.Color.White
        Me.lblWard_SR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWard_SR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWard_SR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWard_SR.Location = New System.Drawing.Point(296, 60)
        Me.lblWard_SR.Name = "lblWard_SR"
        Me.lblWard_SR.Size = New System.Drawing.Size(120, 22)
        Me.lblWard_SR.TabIndex = 29
        Me.lblWard_SR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label30.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(210, 60)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(85, 22)
        Me.Label30.TabIndex = 28
        Me.Label30.Text = "병동/병실"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDeptNm
        '
        Me.lblDeptNm.BackColor = System.Drawing.Color.White
        Me.lblDeptNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeptNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptNm.Location = New System.Drawing.Point(296, 37)
        Me.lblDeptNm.Name = "lblDeptNm"
        Me.lblDeptNm.Size = New System.Drawing.Size(120, 22)
        Me.lblDeptNm.TabIndex = 27
        Me.lblDeptNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label28.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(210, 37)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(85, 22)
        Me.Label28.TabIndex = 26
        Me.Label28.Text = "진료과"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdNo
        '
        Me.lblIdNo.BackColor = System.Drawing.Color.White
        Me.lblIdNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIdNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIdNo.Location = New System.Drawing.Point(296, 106)
        Me.lblIdNo.Name = "lblIdNo"
        Me.lblIdNo.Size = New System.Drawing.Size(120, 22)
        Me.lblIdNo.TabIndex = 25
        Me.lblIdNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label26.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(210, 106)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(85, 22)
        Me.Label26.TabIndex = 24
        Me.Label26.Text = "주민등록번호"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSex
        '
        Me.lblSex.BackColor = System.Drawing.Color.White
        Me.lblSex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSex.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSex.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSex.Location = New System.Drawing.Point(89, 129)
        Me.lblSex.Name = "lblSex"
        Me.lblSex.Size = New System.Drawing.Size(120, 22)
        Me.lblSex.TabIndex = 23
        Me.lblSex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label24.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(8, 129)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(80, 22)
        Me.Label24.TabIndex = 22
        Me.Label24.Text = "성별"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatNm
        '
        Me.lblPatNm.BackColor = System.Drawing.Color.White
        Me.lblPatNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPatNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPatNm.Location = New System.Drawing.Point(89, 106)
        Me.lblPatNm.Name = "lblPatNm"
        Me.lblPatNm.Size = New System.Drawing.Size(120, 22)
        Me.lblPatNm.TabIndex = 21
        Me.lblPatNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label22.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(8, 106)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(80, 22)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "성명"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkID
        '
        Me.lblTkID.BackColor = System.Drawing.Color.White
        Me.lblTkID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTkID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkID.Location = New System.Drawing.Point(296, 152)
        Me.lblTkID.Name = "lblTkID"
        Me.lblTkID.Size = New System.Drawing.Size(120, 22)
        Me.lblTkID.TabIndex = 19
        Me.lblTkID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(8, 152)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(80, 22)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "접수일시"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(210, 152)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(85, 22)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "접수자"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.White
        Me.lblOrdDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOrdDt.Location = New System.Drawing.Point(89, 60)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(120, 22)
        Me.lblOrdDt.TabIndex = 13
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(8, 60)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 22)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "처방일시"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.White
        Me.lblRegNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRegNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegNo.Location = New System.Drawing.Point(296, 83)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(120, 22)
        Me.lblRegNo.TabIndex = 11
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(210, 83)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 22)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "등록번호"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(4, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(415, 22)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "수혈 정보"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDoctor
        '
        Me.lblDoctor.BackColor = System.Drawing.Color.White
        Me.lblDoctor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDoctor.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoctor.Location = New System.Drawing.Point(89, 83)
        Me.lblDoctor.Name = "lblDoctor"
        Me.lblDoctor.Size = New System.Drawing.Size(120, 22)
        Me.lblDoctor.TabIndex = 8
        Me.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 22)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "의뢰의사"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkDt
        '
        Me.lblTkDt.BackColor = System.Drawing.Color.White
        Me.lblTkDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTkDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkDt.Location = New System.Drawing.Point(89, 152)
        Me.lblTkDt.Name = "lblTkDt"
        Me.lblTkDt.Size = New System.Drawing.Size(120, 22)
        Me.lblTkDt.TabIndex = 6
        Me.lblTkDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTnsjubsuno
        '
        Me.lblTnsjubsuno.BackColor = System.Drawing.Color.White
        Me.lblTnsjubsuno.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTnsjubsuno.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTnsjubsuno.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTnsjubsuno.Location = New System.Drawing.Point(89, 37)
        Me.lblTnsjubsuno.Name = "lblTnsjubsuno"
        Me.lblTnsjubsuno.Size = New System.Drawing.Size(120, 22)
        Me.lblTnsjubsuno.TabIndex = 13
        Me.lblTnsjubsuno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 22)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "수혈접수번호"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblDLMCaller)
        Me.GroupBox1.Controls.Add(Me.txtCMCaller)
        Me.GroupBox1.Controls.Add(Me.txtDLMCaller)
        Me.GroupBox1.Controls.Add(Me.txtCmtCont)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label29)
        Me.GroupBox1.Controls.Add(Me.Label32)
        Me.GroupBox1.Controls.Add(Me.Label41)
        Me.GroupBox1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 191)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(423, 269)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        '
        'lblDLMCaller
        '
        Me.lblDLMCaller.BackColor = System.Drawing.Color.Silver
        Me.lblDLMCaller.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDLMCaller.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDLMCaller.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDLMCaller.Location = New System.Drawing.Point(212, 40)
        Me.lblDLMCaller.Name = "lblDLMCaller"
        Me.lblDLMCaller.Size = New System.Drawing.Size(90, 22)
        Me.lblDLMCaller.TabIndex = 32
        Me.lblDLMCaller.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCMCaller
        '
        Me.txtCMCaller.Location = New System.Drawing.Point(89, 64)
        Me.txtCMCaller.Name = "txtCMCaller"
        Me.txtCMCaller.Size = New System.Drawing.Size(122, 21)
        Me.txtCMCaller.TabIndex = 16
        '
        'txtDLMCaller
        '
        Me.txtDLMCaller.Location = New System.Drawing.Point(89, 40)
        Me.txtDLMCaller.Name = "txtDLMCaller"
        Me.txtDLMCaller.Size = New System.Drawing.Size(122, 21)
        Me.txtDLMCaller.TabIndex = 15
        '
        'txtCmtCont
        '
        Me.txtCmtCont.Location = New System.Drawing.Point(5, 113)
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(410, 103)
        Me.txtCmtCont.TabIndex = 14
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label23.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Black
        Me.Label23.Location = New System.Drawing.Point(6, 63)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(82, 22)
        Me.Label23.TabIndex = 12
        Me.Label23.Text = "임상통화자"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label29.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.White
        Me.Label29.Location = New System.Drawing.Point(4, 12)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(415, 22)
        Me.Label29.TabIndex = 9
        Me.Label29.Text = "적혈구제제 수혈 관리 정보"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label32.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(6, 88)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(82, 22)
        Me.Label32.TabIndex = 7
        Me.Label32.Text = "통화내용"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label41.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.Black
        Me.Label41.Location = New System.Drawing.Point(6, 39)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(82, 22)
        Me.Label41.TabIndex = 12
        Me.Label41.Text = "진검통화자"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSave.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnSave.ColorFillBlend = CBlendItems2
        Me.btnSave.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSave.Corners.All = CType(6, Short)
        Me.btnSave.Corners.LowerLeft = CType(6, Short)
        Me.btnSave.Corners.LowerRight = CType(6, Short)
        Me.btnSave.Corners.UpperLeft = CType(6, Short)
        Me.btnSave.Corners.UpperRight = CType(6, Short)
        Me.btnSave.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSave.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSave.FocalPoints.CenterPtX = 0.4936709!
        Me.btnSave.FocalPoints.CenterPtY = 0.28!
        Me.btnSave.FocalPoints.FocusPtX = 0!
        Me.btnSave.FocalPoints.FocusPtY = 0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSave.FocusPtTracker = DesignerRectTracker4
        Me.btnSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Image = Nothing
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.ImageIndex = 0
        Me.btnSave.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSave.Location = New System.Drawing.Point(257, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSave.SideImage = Nothing
        Me.btnSave.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSave.Size = New System.Drawing.Size(79, 25)
        Me.btnSave.TabIndex = 35
        Me.btnSave.Text = "확인"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSave.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chkALL
        '
        Me.chkALL.AutoSize = True
        Me.chkALL.Location = New System.Drawing.Point(256, 16)
        Me.chkALL.Name = "chkALL"
        Me.chkALL.Size = New System.Drawing.Size(72, 16)
        Me.chkALL.TabIndex = 16
        Me.chkALL.Text = "모두요청"
        Me.chkALL.UseVisualStyleBackColor = True
        '
        'chkExcept
        '
        Me.chkExcept.AutoSize = True
        Me.chkExcept.Location = New System.Drawing.Point(341, 16)
        Me.chkExcept.Name = "chkExcept"
        Me.chkExcept.Size = New System.Drawing.Size(72, 16)
        Me.chkExcept.TabIndex = 17
        Me.chkExcept.Text = "제외대상"
        Me.chkExcept.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.chkExcept)
        Me.GroupBox2.Controls.Add(Me.chkCBC)
        Me.GroupBox2.Controls.Add(Me.chkALL)
        Me.GroupBox2.Controls.Add(Me.chkHb10)
        Me.GroupBox2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(10, 411)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(417, 41)
        Me.GroupBox2.TabIndex = 33
        Me.GroupBox2.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(5, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 24)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "보고상태"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkCBC
        '
        Me.chkCBC.AutoSize = True
        Me.chkCBC.Location = New System.Drawing.Point(177, 15)
        Me.chkCBC.Name = "chkCBC"
        Me.chkCBC.Size = New System.Drawing.Size(66, 16)
        Me.chkCBC.TabIndex = 15
        Me.chkCBC.Text = "CBC F/U"
        Me.chkCBC.UseVisualStyleBackColor = True
        '
        'chkHb10
        '
        Me.chkHb10.AutoSize = True
        Me.chkHb10.Location = New System.Drawing.Point(90, 16)
        Me.chkHb10.Name = "chkHb10"
        Me.chkHb10.Size = New System.Drawing.Size(78, 16)
        Me.chkHb10.TabIndex = 14
        Me.chkHb10.Text = "Hb>10g/dL"
        Me.chkHb10.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnCancel)
        Me.GroupBox4.Controls.Add(Me.btnSave)
        Me.GroupBox4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(8, 463)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(423, 41)
        Me.GroupBox4.TabIndex = 36
        Me.GroupBox4.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnCancel.ColorFillBlend = CBlendItems3
        Me.btnCancel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnCancel.Corners.All = CType(6, Short)
        Me.btnCancel.Corners.LowerLeft = CType(6, Short)
        Me.btnCancel.Corners.LowerRight = CType(6, Short)
        Me.btnCancel.Corners.UpperLeft = CType(6, Short)
        Me.btnCancel.Corners.UpperRight = CType(6, Short)
        Me.btnCancel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnCancel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnCancel.FocalPoints.CenterPtX = 0.3924051!
        Me.btnCancel.FocalPoints.CenterPtY = 0.48!
        Me.btnCancel.FocalPoints.FocusPtX = 0!
        Me.btnCancel.FocalPoints.FocusPtY = 0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.FocusPtTracker = DesignerRectTracker6
        Me.btnCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Image = Nothing
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnCancel.Location = New System.Drawing.Point(337, 12)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnCancel.SideImage = Nothing
        Me.btnCancel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnCancel.Size = New System.Drawing.Size(79, 25)
        Me.btnCancel.TabIndex = 36
        Me.btnCancel.Text = "취소"
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnCancel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGCDBLD_TRAN_MGT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 508)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Name = "FGCDBLD_TRAN_MGT"
        Me.Text = "적혈구제제 수혈관리"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents lblWard_SR As Windows.Forms.Label
    Friend WithEvents Label30 As Windows.Forms.Label
    Friend WithEvents lblDeptNm As Windows.Forms.Label
    Friend WithEvents Label28 As Windows.Forms.Label
    Friend WithEvents lblIdNo As Windows.Forms.Label
    Friend WithEvents Label26 As Windows.Forms.Label
    Friend WithEvents lblSex As Windows.Forms.Label
    Friend WithEvents Label24 As Windows.Forms.Label
    Friend WithEvents lblPatNm As Windows.Forms.Label
    Friend WithEvents Label22 As Windows.Forms.Label
    Friend WithEvents lblTkID As Windows.Forms.Label
    Friend WithEvents Label16 As Windows.Forms.Label
    Friend WithEvents Label18 As Windows.Forms.Label
    Friend WithEvents lblOrdDt As Windows.Forms.Label
    Friend WithEvents Label14 As Windows.Forms.Label
    Friend WithEvents lblRegNo As Windows.Forms.Label
    Friend WithEvents Label12 As Windows.Forms.Label
    Friend WithEvents Label9 As Windows.Forms.Label
    Friend WithEvents lblDoctor As Windows.Forms.Label
    Friend WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents lblTkDt As Windows.Forms.Label
    Friend WithEvents lblTnsjubsuno As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents Label23 As Windows.Forms.Label
    Friend WithEvents Label29 As Windows.Forms.Label
    Friend WithEvents Label32 As Windows.Forms.Label
    Friend WithEvents Label41 As Windows.Forms.Label
    Friend WithEvents txtCmtCont As Windows.Forms.TextBox
    Friend WithEvents lblAge As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents btnSave As CButtonLib.CButton
    Friend WithEvents chkALL As Windows.Forms.CheckBox
    Friend WithEvents chkExcept As Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents chkCBC As Windows.Forms.CheckBox
    Friend WithEvents chkHb10 As Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents btnCancel As CButtonLib.CButton
    Friend WithEvents txtCMCaller As Windows.Forms.TextBox
    Friend WithEvents txtDLMCaller As Windows.Forms.TextBox
    Friend WithEvents lblDLMCaller As Windows.Forms.Label
End Class
