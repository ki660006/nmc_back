<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxPatientInfo
    Inherits System.Windows.Forms.UserControl

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Me.components = New System.ComponentModel.Container()
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxPatientInfo))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.lblOpDt_Label = New System.Windows.Forms.Label()
        Me.lblOpDt = New System.Windows.Forms.Label()
        Me.lblBirthDay = New System.Windows.Forms.Label()
        Me.txtResDtail = New System.Windows.Forms.TextBox()
        Me.lblResDtail = New System.Windows.Forms.Label()
        Me.txtSpecialCmt = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnDetailPatInfo = New CButtonLib.CButton()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblGubun = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblSogae = New System.Windows.Forms.Label()
        Me.lblVip = New System.Windows.Forms.Label()
        Me.lblTel = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblDiagNm = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblEmer = New System.Windows.Forms.Label()
        Me.lblDoctorNm = New System.Windows.Forms.Label()
        Me.lblRoomNo = New System.Windows.Forms.Label()
        Me.lblWardCd = New System.Windows.Forms.Label()
        Me.lblEntDt = New System.Windows.Forms.Label()
        Me.lblDeptCd = New System.Windows.Forms.Label()
        Me.lblOrdDt = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblInfInfo = New System.Windows.Forms.Label()
        Me.lblIdNo = New System.Windows.Forms.Label()
        Me.lblInjong = New System.Windows.Forms.Label()
        Me.lblAbo = New System.Windows.Forms.Label()
        Me.lblWeight = New System.Windows.Forms.Label()
        Me.lblHeight = New System.Windows.Forms.Label()
        Me.lblSexAge = New System.Windows.Forms.Label()
        Me.lblPatNm = New System.Windows.Forms.Label()
        Me.lblRegNo = New System.Windows.Forms.Label()
        Me.cmuLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuCopy_regno = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblDiagNme = New System.Windows.Forms.Label()
        Me.gbxUniqueComment = New System.Windows.Forms.GroupBox()
        Me.btnShareCmtDel = New CButtonLib.CButton()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnShareCmtAdd = New CButtonLib.CButton()
        Me.txtShareCmtCont = New System.Windows.Forms.TextBox()
        Me.pnlInfo.SuspendLayout()
        Me.cmuLink.SuspendLayout()
        Me.gbxUniqueComment.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlInfo
        '
        Me.pnlInfo.Controls.Add(Me.lblOpDt_Label)
        Me.pnlInfo.Controls.Add(Me.lblOpDt)
        Me.pnlInfo.Controls.Add(Me.lblBirthDay)
        Me.pnlInfo.Controls.Add(Me.txtResDtail)
        Me.pnlInfo.Controls.Add(Me.lblResDtail)
        Me.pnlInfo.Controls.Add(Me.txtSpecialCmt)
        Me.pnlInfo.Controls.Add(Me.Label23)
        Me.pnlInfo.Controls.Add(Me.btnDetailPatInfo)
        Me.pnlInfo.Controls.Add(Me.Label38)
        Me.pnlInfo.Controls.Add(Me.lblGubun)
        Me.pnlInfo.Controls.Add(Me.Label21)
        Me.pnlInfo.Controls.Add(Me.Label22)
        Me.pnlInfo.Controls.Add(Me.lblSogae)
        Me.pnlInfo.Controls.Add(Me.lblVip)
        Me.pnlInfo.Controls.Add(Me.lblTel)
        Me.pnlInfo.Controls.Add(Me.txtRemark)
        Me.pnlInfo.Controls.Add(Me.Label19)
        Me.pnlInfo.Controls.Add(Me.lblDiagNm)
        Me.pnlInfo.Controls.Add(Me.Label7)
        Me.pnlInfo.Controls.Add(Me.lblEmer)
        Me.pnlInfo.Controls.Add(Me.lblDoctorNm)
        Me.pnlInfo.Controls.Add(Me.lblRoomNo)
        Me.pnlInfo.Controls.Add(Me.lblWardCd)
        Me.pnlInfo.Controls.Add(Me.lblEntDt)
        Me.pnlInfo.Controls.Add(Me.lblDeptCd)
        Me.pnlInfo.Controls.Add(Me.lblOrdDt)
        Me.pnlInfo.Controls.Add(Me.Label17)
        Me.pnlInfo.Controls.Add(Me.Label18)
        Me.pnlInfo.Controls.Add(Me.lblInfInfo)
        Me.pnlInfo.Controls.Add(Me.lblIdNo)
        Me.pnlInfo.Controls.Add(Me.lblInjong)
        Me.pnlInfo.Controls.Add(Me.lblAbo)
        Me.pnlInfo.Controls.Add(Me.lblWeight)
        Me.pnlInfo.Controls.Add(Me.lblHeight)
        Me.pnlInfo.Controls.Add(Me.lblSexAge)
        Me.pnlInfo.Controls.Add(Me.lblPatNm)
        Me.pnlInfo.Controls.Add(Me.lblRegNo)
        Me.pnlInfo.Controls.Add(Me.lblDiagNme)
        Me.pnlInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Size = New System.Drawing.Size(877, 143)
        Me.pnlInfo.TabIndex = 3
        '
        'lblOpDt_Label
        '
        Me.lblOpDt_Label.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblOpDt_Label.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOpDt_Label.ForeColor = System.Drawing.Color.White
        Me.lblOpDt_Label.Location = New System.Drawing.Point(595, 113)
        Me.lblOpDt_Label.Name = "lblOpDt_Label"
        Me.lblOpDt_Label.Size = New System.Drawing.Size(76, 26)
        Me.lblOpDt_Label.TabIndex = 189
        Me.lblOpDt_Label.Text = "수술일자"
        Me.lblOpDt_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblOpDt_Label.Visible = False
        '
        'lblOpDt
        '
        Me.lblOpDt.BackColor = System.Drawing.Color.White
        Me.lblOpDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOpDt.ForeColor = System.Drawing.Color.Black
        Me.lblOpDt.Location = New System.Drawing.Point(673, 113)
        Me.lblOpDt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblOpDt.Name = "lblOpDt"
        Me.lblOpDt.Size = New System.Drawing.Size(202, 26)
        Me.lblOpDt.TabIndex = 188
        Me.lblOpDt.Text = "2010-10-12"
        Me.lblOpDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOpDt.Visible = False
        '
        'lblBirthDay
        '
        Me.lblBirthDay.BackColor = System.Drawing.Color.White
        Me.lblBirthDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBirthDay.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBirthDay.ForeColor = System.Drawing.Color.Black
        Me.lblBirthDay.Location = New System.Drawing.Point(383, 58)
        Me.lblBirthDay.Name = "lblBirthDay"
        Me.lblBirthDay.Size = New System.Drawing.Size(99, 26)
        Me.lblBirthDay.TabIndex = 183
        Me.lblBirthDay.Tag = "전화번호"
        Me.lblBirthDay.Text = "010-1234-5678"
        Me.lblBirthDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBirthDay.Visible = False
        '
        'txtResDtail
        '
        Me.txtResDtail.BackColor = System.Drawing.Color.White
        Me.txtResDtail.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtResDtail.ForeColor = System.Drawing.Color.Black
        Me.txtResDtail.Location = New System.Drawing.Point(673, 85)
        Me.txtResDtail.Multiline = True
        Me.txtResDtail.Name = "txtResDtail"
        Me.txtResDtail.ReadOnly = True
        Me.txtResDtail.Size = New System.Drawing.Size(202, 54)
        Me.txtResDtail.TabIndex = 181
        Me.txtResDtail.Text = "2010-10-10 EM 나응급"
        '
        'lblResDtail
        '
        Me.lblResDtail.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblResDtail.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblResDtail.ForeColor = System.Drawing.Color.White
        Me.lblResDtail.Location = New System.Drawing.Point(595, 85)
        Me.lblResDtail.Name = "lblResDtail"
        Me.lblResDtail.Size = New System.Drawing.Size(76, 54)
        Me.lblResDtail.TabIndex = 178
        Me.lblResDtail.Text = "예약일자"
        Me.lblResDtail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSpecialCmt
        '
        Me.txtSpecialCmt.BackColor = System.Drawing.Color.White
        Me.txtSpecialCmt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpecialCmt.ForeColor = System.Drawing.Color.Black
        Me.txtSpecialCmt.Location = New System.Drawing.Point(104, 85)
        Me.txtSpecialCmt.Multiline = True
        Me.txtSpecialCmt.Name = "txtSpecialCmt"
        Me.txtSpecialCmt.ReadOnly = True
        Me.txtSpecialCmt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSpecialCmt.Size = New System.Drawing.Size(230, 54)
        Me.txtSpecialCmt.TabIndex = 177
        Me.txtSpecialCmt.Tag = "특이사항"
        Me.txtSpecialCmt.Text = "NS환자입니다. "
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label23.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.White
        Me.Label23.Location = New System.Drawing.Point(3, 85)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(100, 26)
        Me.Label23.TabIndex = 176
        Me.Label23.Text = "특이사항"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDetailPatInfo
        '
        Me.btnDetailPatInfo.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDetailPatInfo.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnDetailPatInfo.ColorFillBlend = CBlendItems1
        Me.btnDetailPatInfo.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnDetailPatInfo.Corners.All = CType(6, Short)
        Me.btnDetailPatInfo.Corners.LowerLeft = CType(6, Short)
        Me.btnDetailPatInfo.Corners.LowerRight = CType(6, Short)
        Me.btnDetailPatInfo.Corners.UpperLeft = CType(6, Short)
        Me.btnDetailPatInfo.Corners.UpperRight = CType(6, Short)
        Me.btnDetailPatInfo.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnDetailPatInfo.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnDetailPatInfo.FocalPoints.CenterPtX = 0.4848485!
        Me.btnDetailPatInfo.FocalPoints.CenterPtY = 0.56!
        Me.btnDetailPatInfo.FocalPoints.FocusPtX = 0.0!
        Me.btnDetailPatInfo.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDetailPatInfo.FocusPtTracker = DesignerRectTracker2
        Me.btnDetailPatInfo.Image = Nothing
        Me.btnDetailPatInfo.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDetailPatInfo.ImageIndex = 0
        Me.btnDetailPatInfo.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnDetailPatInfo.Location = New System.Drawing.Point(3, 114)
        Me.btnDetailPatInfo.Name = "btnDetailPatInfo"
        Me.btnDetailPatInfo.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnDetailPatInfo.SideImage = Nothing
        Me.btnDetailPatInfo.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnDetailPatInfo.Size = New System.Drawing.Size(99, 25)
        Me.btnDetailPatInfo.TabIndex = 175
        Me.btnDetailPatInfo.Text = "상세조회"
        Me.btnDetailPatInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnDetailPatInfo.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label38.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.White
        Me.Label38.Location = New System.Drawing.Point(595, 4)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(76, 26)
        Me.Label38.TabIndex = 172
        Me.Label38.Text = "환자유형"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGubun
        '
        Me.lblGubun.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGubun.BackColor = System.Drawing.Color.White
        Me.lblGubun.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGubun.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGubun.Location = New System.Drawing.Point(673, 4)
        Me.lblGubun.Name = "lblGubun"
        Me.lblGubun.Size = New System.Drawing.Size(201, 26)
        Me.lblGubun.TabIndex = 171
        Me.lblGubun.Text = "국민(중증)"
        Me.lblGubun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label21.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.White
        Me.Label21.Location = New System.Drawing.Point(595, 58)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(76, 26)
        Me.Label21.TabIndex = 174
        Me.Label21.Text = "VIP"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label22.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(595, 31)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(76, 26)
        Me.Label22.TabIndex = 173
        Me.Label22.Text = "직원관계"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSogae
        '
        Me.lblSogae.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSogae.BackColor = System.Drawing.Color.White
        Me.lblSogae.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSogae.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSogae.Location = New System.Drawing.Point(673, 31)
        Me.lblSogae.Name = "lblSogae"
        Me.lblSogae.Size = New System.Drawing.Size(201, 26)
        Me.lblSogae.TabIndex = 170
        Me.lblSogae.Text = "총무팀 홍길동(VIP Family)"
        Me.lblSogae.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblVip
        '
        Me.lblVip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVip.BackColor = System.Drawing.Color.White
        Me.lblVip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblVip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblVip.ForeColor = System.Drawing.Color.Black
        Me.lblVip.Location = New System.Drawing.Point(673, 58)
        Me.lblVip.Name = "lblVip"
        Me.lblVip.Size = New System.Drawing.Size(201, 26)
        Me.lblVip.TabIndex = 169
        Me.lblVip.Text = "청소팀 막내입니다!!! VIP"
        Me.lblVip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTel
        '
        Me.lblTel.BackColor = System.Drawing.Color.White
        Me.lblTel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTel.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTel.ForeColor = System.Drawing.Color.Black
        Me.lblTel.Location = New System.Drawing.Point(246, 58)
        Me.lblTel.Name = "lblTel"
        Me.lblTel.Size = New System.Drawing.Size(88, 26)
        Me.lblTel.TabIndex = 54
        Me.lblTel.Tag = "전화번호"
        Me.lblTel.Text = "010-1234-5678"
        Me.lblTel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.Color.White
        Me.txtRemark.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRemark.ForeColor = System.Drawing.Color.Black
        Me.txtRemark.Location = New System.Drawing.Point(413, 85)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ReadOnly = True
        Me.txtRemark.Size = New System.Drawing.Size(180, 54)
        Me.txtRemark.TabIndex = 53
        Me.txtRemark.Text = "빠른결과보고 바람."
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label19.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(336, 85)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(76, 54)
        Me.Label19.TabIndex = 52
        Me.Label19.Text = "의뢰의사 Remark"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDiagNm
        '
        Me.lblDiagNm.BackColor = System.Drawing.Color.White
        Me.lblDiagNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDiagNm.ForeColor = System.Drawing.Color.Black
        Me.lblDiagNm.Location = New System.Drawing.Point(413, 58)
        Me.lblDiagNm.Name = "lblDiagNm"
        Me.lblDiagNm.Size = New System.Drawing.Size(180, 26)
        Me.lblDiagNm.TabIndex = 51
        Me.lblDiagNm.Tag = "진단명"
        Me.lblDiagNm.Text = "감염성심내막염"
        Me.lblDiagNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(336, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 26)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "진 단 명"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEmer
        '
        Me.lblEmer.BackColor = System.Drawing.Color.White
        Me.lblEmer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmer.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblEmer.ForeColor = System.Drawing.Color.Red
        Me.lblEmer.Location = New System.Drawing.Point(246, 31)
        Me.lblEmer.Name = "lblEmer"
        Me.lblEmer.Size = New System.Drawing.Size(88, 26)
        Me.lblEmer.TabIndex = 49
        Me.lblEmer.Tag = "응급여부"
        Me.lblEmer.Text = "응급"
        Me.lblEmer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDoctorNm
        '
        Me.lblDoctorNm.BackColor = System.Drawing.Color.White
        Me.lblDoctorNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDoctorNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctorNm.ForeColor = System.Drawing.Color.Black
        Me.lblDoctorNm.Location = New System.Drawing.Point(535, 4)
        Me.lblDoctorNm.Name = "lblDoctorNm"
        Me.lblDoctorNm.Size = New System.Drawing.Size(58, 26)
        Me.lblDoctorNm.TabIndex = 48
        Me.lblDoctorNm.Tag = "의사"
        Me.lblDoctorNm.Text = "나의사"
        Me.lblDoctorNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRoomNo
        '
        Me.lblRoomNo.BackColor = System.Drawing.Color.White
        Me.lblRoomNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRoomNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRoomNo.ForeColor = System.Drawing.Color.Black
        Me.lblRoomNo.Location = New System.Drawing.Point(557, 31)
        Me.lblRoomNo.Name = "lblRoomNo"
        Me.lblRoomNo.Size = New System.Drawing.Size(36, 26)
        Me.lblRoomNo.TabIndex = 47
        Me.lblRoomNo.Tag = "병실"
        Me.lblRoomNo.Text = "1001"
        Me.lblRoomNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWardCd
        '
        Me.lblWardCd.BackColor = System.Drawing.Color.White
        Me.lblWardCd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWardCd.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWardCd.ForeColor = System.Drawing.Color.Black
        Me.lblWardCd.Location = New System.Drawing.Point(491, 31)
        Me.lblWardCd.Name = "lblWardCd"
        Me.lblWardCd.Size = New System.Drawing.Size(64, 26)
        Me.lblWardCd.TabIndex = 46
        Me.lblWardCd.Tag = "병동"
        Me.lblWardCd.Text = "101"
        Me.lblWardCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEntDt
        '
        Me.lblEntDt.BackColor = System.Drawing.Color.White
        Me.lblEntDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEntDt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblEntDt.ForeColor = System.Drawing.Color.Black
        Me.lblEntDt.Location = New System.Drawing.Point(413, 31)
        Me.lblEntDt.Name = "lblEntDt"
        Me.lblEntDt.Size = New System.Drawing.Size(77, 26)
        Me.lblEntDt.TabIndex = 45
        Me.lblEntDt.Tag = "입원일자"
        Me.lblEntDt.Text = "2010-09-15"
        Me.lblEntDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDeptCd
        '
        Me.lblDeptCd.BackColor = System.Drawing.Color.White
        Me.lblDeptCd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptCd.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeptCd.ForeColor = System.Drawing.Color.Black
        Me.lblDeptCd.Location = New System.Drawing.Point(491, 4)
        Me.lblDeptCd.Name = "lblDeptCd"
        Me.lblDeptCd.Size = New System.Drawing.Size(43, 26)
        Me.lblDeptCd.TabIndex = 44
        Me.lblDeptCd.Tag = "진료과"
        Me.lblDeptCd.Text = "CP"
        Me.lblDeptCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.White
        Me.lblOrdDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.Color.Black
        Me.lblOrdDt.Location = New System.Drawing.Point(413, 4)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(77, 26)
        Me.lblOrdDt.TabIndex = 43
        Me.lblOrdDt.Tag = "처방일자"
        Me.lblOrdDt.Text = "2010-10-10"
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label17.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(336, 31)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(76, 26)
        Me.Label17.TabIndex = 42
        Me.Label17.Text = "재원정보"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(336, 4)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(76, 26)
        Me.Label18.TabIndex = 41
        Me.Label18.Text = "처방일시"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInfInfo
        '
        Me.lblInfInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblInfInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInfInfo.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblInfInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfInfo.Location = New System.Drawing.Point(246, 4)
        Me.lblInfInfo.Name = "lblInfInfo"
        Me.lblInfInfo.Size = New System.Drawing.Size(88, 26)
        Me.lblInfInfo.TabIndex = 40
        Me.lblInfInfo.Tag = "감염정보"
        Me.lblInfInfo.Text = "HIV"
        Me.lblInfInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdNo
        '
        Me.lblIdNo.BackColor = System.Drawing.Color.White
        Me.lblIdNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIdNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdNo.ForeColor = System.Drawing.Color.Black
        Me.lblIdNo.Location = New System.Drawing.Point(104, 58)
        Me.lblIdNo.Name = "lblIdNo"
        Me.lblIdNo.Size = New System.Drawing.Size(141, 26)
        Me.lblIdNo.TabIndex = 39
        Me.lblIdNo.Tag = "주민등록번호"
        Me.lblIdNo.Text = "850701-2******"
        Me.lblIdNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInjong
        '
        Me.lblInjong.BackColor = System.Drawing.Color.White
        Me.lblInjong.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInjong.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblInjong.ForeColor = System.Drawing.Color.Black
        Me.lblInjong.Location = New System.Drawing.Point(104, 31)
        Me.lblInjong.Name = "lblInjong"
        Me.lblInjong.Size = New System.Drawing.Size(94, 26)
        Me.lblInjong.TabIndex = 38
        Me.lblInjong.Tag = "인종"
        Me.lblInjong.Text = "깜둥이"
        Me.lblInjong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbo
        '
        Me.lblAbo.BackColor = System.Drawing.Color.White
        Me.lblAbo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAbo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAbo.ForeColor = System.Drawing.Color.Red
        Me.lblAbo.Location = New System.Drawing.Point(199, 31)
        Me.lblAbo.Name = "lblAbo"
        Me.lblAbo.Size = New System.Drawing.Size(46, 26)
        Me.lblAbo.TabIndex = 37
        Me.lblAbo.Tag = "혈액형"
        Me.lblAbo.Text = "AB-"
        Me.lblAbo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWeight
        '
        Me.lblWeight.BackColor = System.Drawing.Color.White
        Me.lblWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeight.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWeight.ForeColor = System.Drawing.Color.Black
        Me.lblWeight.Location = New System.Drawing.Point(199, 4)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(46, 26)
        Me.lblWeight.TabIndex = 36
        Me.lblWeight.Tag = "몸무게"
        Me.lblWeight.Text = "59.8"
        Me.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHeight
        '
        Me.lblHeight.BackColor = System.Drawing.Color.White
        Me.lblHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHeight.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblHeight.ForeColor = System.Drawing.Color.Black
        Me.lblHeight.Location = New System.Drawing.Point(152, 4)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Size = New System.Drawing.Size(46, 26)
        Me.lblHeight.TabIndex = 35
        Me.lblHeight.Tag = "키"
        Me.lblHeight.Text = "177.8"
        Me.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSexAge
        '
        Me.lblSexAge.BackColor = System.Drawing.Color.White
        Me.lblSexAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSexAge.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSexAge.ForeColor = System.Drawing.Color.Black
        Me.lblSexAge.Location = New System.Drawing.Point(104, 4)
        Me.lblSexAge.Name = "lblSexAge"
        Me.lblSexAge.Size = New System.Drawing.Size(47, 26)
        Me.lblSexAge.TabIndex = 34
        Me.lblSexAge.Tag = "셩별/나이"
        Me.lblSexAge.Text = "M/100"
        Me.lblSexAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatNm
        '
        Me.lblPatNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblPatNm.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatNm.ForeColor = System.Drawing.Color.White
        Me.lblPatNm.Location = New System.Drawing.Point(3, 45)
        Me.lblPatNm.Name = "lblPatNm"
        Me.lblPatNm.Size = New System.Drawing.Size(100, 39)
        Me.lblPatNm.TabIndex = 33
        Me.lblPatNm.Text = "나환자아들"
        Me.lblPatNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblRegNo.ContextMenuStrip = Me.cmuLink
        Me.lblRegNo.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.Color.White
        Me.lblRegNo.Location = New System.Drawing.Point(3, 4)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(100, 40)
        Me.lblRegNo.TabIndex = 32
        Me.lblRegNo.Text = "0123456789"
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmuLink
        '
        Me.cmuLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCopy_regno})
        Me.cmuLink.Name = "cmuRstList"
        Me.cmuLink.Size = New System.Drawing.Size(151, 26)
        Me.cmuLink.Text = "상황에 맞는 메뉴"
        '
        'mnuCopy_regno
        '
        Me.mnuCopy_regno.Name = "mnuCopy_regno"
        Me.mnuCopy_regno.Size = New System.Drawing.Size(150, 22)
        Me.mnuCopy_regno.Text = "등록번호 복사"
        '
        'lblDiagNme
        '
        Me.lblDiagNme.BackColor = System.Drawing.Color.White
        Me.lblDiagNme.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNme.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDiagNme.ForeColor = System.Drawing.Color.Black
        Me.lblDiagNme.Location = New System.Drawing.Point(418, 81)
        Me.lblDiagNme.Name = "lblDiagNme"
        Me.lblDiagNme.Size = New System.Drawing.Size(180, 26)
        Me.lblDiagNme.TabIndex = 182
        Me.lblDiagNme.Text = "감염성심내막염"
        Me.lblDiagNme.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDiagNme.Visible = False
        '
        'gbxUniqueComment
        '
        Me.gbxUniqueComment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbxUniqueComment.Controls.Add(Me.btnShareCmtDel)
        Me.gbxUniqueComment.Controls.Add(Me.Label9)
        Me.gbxUniqueComment.Controls.Add(Me.btnShareCmtAdd)
        Me.gbxUniqueComment.Controls.Add(Me.txtShareCmtCont)
        Me.gbxUniqueComment.Location = New System.Drawing.Point(877, 0)
        Me.gbxUniqueComment.Name = "gbxUniqueComment"
        Me.gbxUniqueComment.Size = New System.Drawing.Size(136, 143)
        Me.gbxUniqueComment.TabIndex = 177
        Me.gbxUniqueComment.TabStop = False
        '
        'btnShareCmtDel
        '
        Me.btnShareCmtDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnShareCmtDel.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnShareCmtDel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems2.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnShareCmtDel.ColorFillBlend = CBlendItems2
        Me.btnShareCmtDel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnShareCmtDel.Corners.All = CType(6, Short)
        Me.btnShareCmtDel.Corners.LowerLeft = CType(6, Short)
        Me.btnShareCmtDel.Corners.LowerRight = CType(6, Short)
        Me.btnShareCmtDel.Corners.UpperLeft = CType(6, Short)
        Me.btnShareCmtDel.Corners.UpperRight = CType(6, Short)
        Me.btnShareCmtDel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnShareCmtDel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnShareCmtDel.FocalPoints.CenterPtX = 1.0!
        Me.btnShareCmtDel.FocalPoints.CenterPtY = 0.0!
        Me.btnShareCmtDel.FocalPoints.FocusPtX = 0.0!
        Me.btnShareCmtDel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnShareCmtDel.FocusPtTracker = DesignerRectTracker4
        Me.btnShareCmtDel.Image = Nothing
        Me.btnShareCmtDel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnShareCmtDel.ImageIndex = 0
        Me.btnShareCmtDel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnShareCmtDel.Location = New System.Drawing.Point(66, 20)
        Me.btnShareCmtDel.Name = "btnShareCmtDel"
        Me.btnShareCmtDel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnShareCmtDel.SideImage = Nothing
        Me.btnShareCmtDel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnShareCmtDel.Size = New System.Drawing.Size(71, 17)
        Me.btnShareCmtDel.TabIndex = 177
        Me.btnShareCmtDel.Text = "삭제"
        Me.btnShareCmtDel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnShareCmtDel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(0, 2)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 35)
        Me.Label9.TabIndex = 176
        Me.Label9.Text = "검사자간 공유사항"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnShareCmtAdd
        '
        Me.btnShareCmtAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnShareCmtAdd.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnShareCmtAdd.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems3.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnShareCmtAdd.ColorFillBlend = CBlendItems3
        Me.btnShareCmtAdd.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnShareCmtAdd.Corners.All = CType(6, Short)
        Me.btnShareCmtAdd.Corners.LowerLeft = CType(6, Short)
        Me.btnShareCmtAdd.Corners.LowerRight = CType(6, Short)
        Me.btnShareCmtAdd.Corners.UpperLeft = CType(6, Short)
        Me.btnShareCmtAdd.Corners.UpperRight = CType(6, Short)
        Me.btnShareCmtAdd.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnShareCmtAdd.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnShareCmtAdd.FocalPoints.CenterPtX = 1.0!
        Me.btnShareCmtAdd.FocalPoints.CenterPtY = 0.0!
        Me.btnShareCmtAdd.FocalPoints.FocusPtX = 0.0!
        Me.btnShareCmtAdd.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnShareCmtAdd.FocusPtTracker = DesignerRectTracker6
        Me.btnShareCmtAdd.Image = Nothing
        Me.btnShareCmtAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnShareCmtAdd.ImageIndex = 0
        Me.btnShareCmtAdd.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnShareCmtAdd.Location = New System.Drawing.Point(66, 2)
        Me.btnShareCmtAdd.Name = "btnShareCmtAdd"
        Me.btnShareCmtAdd.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnShareCmtAdd.SideImage = Nothing
        Me.btnShareCmtAdd.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnShareCmtAdd.Size = New System.Drawing.Size(69, 19)
        Me.btnShareCmtAdd.TabIndex = 176
        Me.btnShareCmtAdd.Text = "추가(수정)"
        Me.btnShareCmtAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnShareCmtAdd.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtShareCmtCont
        '
        Me.txtShareCmtCont.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtShareCmtCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShareCmtCont.Location = New System.Drawing.Point(0, 35)
        Me.txtShareCmtCont.Multiline = True
        Me.txtShareCmtCont.Name = "txtShareCmtCont"
        Me.txtShareCmtCont.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtShareCmtCont.Size = New System.Drawing.Size(136, 106)
        Me.txtShareCmtCont.TabIndex = 176
        '
        'AxPatientInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Controls.Add(Me.gbxUniqueComment)
        Me.Controls.Add(Me.pnlInfo)
        Me.Name = "AxPatientInfo"
        Me.Size = New System.Drawing.Size(1016, 143)
        Me.pnlInfo.ResumeLayout(False)
        Me.pnlInfo.PerformLayout()
        Me.cmuLink.ResumeLayout(False)
        Me.gbxUniqueComment.ResumeLayout(False)
        Me.gbxUniqueComment.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents txtResDtail As System.Windows.Forms.TextBox
    Friend WithEvents lblResDtail As System.Windows.Forms.Label
    Friend WithEvents txtSpecialCmt As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents btnDetailPatInfo As CButtonLib.CButton
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblGubun As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblSogae As System.Windows.Forms.Label
    Friend WithEvents lblVip As System.Windows.Forms.Label
    Friend WithEvents lblTel As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNm As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblEmer As System.Windows.Forms.Label
    Friend WithEvents lblDoctorNm As System.Windows.Forms.Label
    Friend WithEvents lblRoomNo As System.Windows.Forms.Label
    Friend WithEvents lblWardCd As System.Windows.Forms.Label
    Friend WithEvents lblEntDt As System.Windows.Forms.Label
    Friend WithEvents lblDeptCd As System.Windows.Forms.Label
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblInfInfo As System.Windows.Forms.Label
    Friend WithEvents lblIdNo As System.Windows.Forms.Label
    Friend WithEvents lblInjong As System.Windows.Forms.Label
    Friend WithEvents lblAbo As System.Windows.Forms.Label
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents lblSexAge As System.Windows.Forms.Label
    Friend WithEvents lblPatNm As System.Windows.Forms.Label
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents lblDiagNme As System.Windows.Forms.Label
    Friend WithEvents lblBirthDay As System.Windows.Forms.Label
    Friend WithEvents cmuLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuCopy_regno As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblOpDt_Label As System.Windows.Forms.Label
    Friend WithEvents lblOpDt As System.Windows.Forms.Label
    Friend WithEvents gbxUniqueComment As System.Windows.Forms.GroupBox
    Friend WithEvents btnShareCmtDel As CButtonLib.CButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnShareCmtAdd As CButtonLib.CButton
    Friend WithEvents txtShareCmtCont As System.Windows.Forms.TextBox

End Class
