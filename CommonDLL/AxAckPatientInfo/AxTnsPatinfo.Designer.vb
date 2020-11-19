<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxTnsPatinfo
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxTnsPatinfo))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtRmk = New System.Windows.Forms.TextBox
        Me.pnlBldResult = New System.Windows.Forms.Panel
        Me.spdPatInfo = New AxFPSpreadADO.AxfpSpread
        Me.btnSebu = New CButtonLib.CButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblDiagNm = New System.Windows.Forms.Label
        Me.txtSRmk = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.lblRmk = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.lblPhone = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.lblOdate = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblEmer = New System.Windows.Forms.Label
        Me.lblDoctor = New System.Windows.Forms.Label
        Me.lblWd = New System.Windows.Forms.Label
        Me.lblSr = New System.Windows.Forms.Label
        Me.lblIdate = New System.Windows.Forms.Label
        Me.lblDeptCd = New System.Windows.Forms.Label
        Me.lblOrdDate = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblInfInfo = New System.Windows.Forms.Label
        Me.lblJumino = New System.Windows.Forms.Label
        Me.lblNation = New System.Windows.Forms.Label
        Me.lblAbo = New System.Windows.Forms.Label
        Me.lblWeight = New System.Windows.Forms.Label
        Me.lblHeight = New System.Windows.Forms.Label
        Me.lblSexAge = New System.Windows.Forms.Label
        Me.lblPatNm = New System.Windows.Forms.Label
        Me.lblRegno = New System.Windows.Forms.Label
        Me.cmuLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuCopy_regno = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel1.SuspendLayout()
        Me.pnlBldResult.SuspendLayout()
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmuLink.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.txtRmk)
        Me.Panel1.Controls.Add(Me.pnlBldResult)
        Me.Panel1.Controls.Add(Me.btnSebu)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lblDiagNm)
        Me.Panel1.Controls.Add(Me.txtSRmk)
        Me.Panel1.Controls.Add(Me.Label25)
        Me.Panel1.Controls.Add(Me.lblRmk)
        Me.Panel1.Controls.Add(Me.Label23)
        Me.Panel1.Controls.Add(Me.lblPhone)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.lblOdate)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblEmer)
        Me.Panel1.Controls.Add(Me.lblDoctor)
        Me.Panel1.Controls.Add(Me.lblWd)
        Me.Panel1.Controls.Add(Me.lblSr)
        Me.Panel1.Controls.Add(Me.lblIdate)
        Me.Panel1.Controls.Add(Me.lblDeptCd)
        Me.Panel1.Controls.Add(Me.lblOrdDate)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.lblInfInfo)
        Me.Panel1.Controls.Add(Me.lblJumino)
        Me.Panel1.Controls.Add(Me.lblNation)
        Me.Panel1.Controls.Add(Me.lblAbo)
        Me.Panel1.Controls.Add(Me.lblWeight)
        Me.Panel1.Controls.Add(Me.lblHeight)
        Me.Panel1.Controls.Add(Me.lblSexAge)
        Me.Panel1.Controls.Add(Me.lblPatNm)
        Me.Panel1.Controls.Add(Me.lblRegno)
        Me.Panel1.Location = New System.Drawing.Point(5, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(967, 165)
        Me.Panel1.TabIndex = 190
        '
        'txtRmk
        '
        Me.txtRmk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRmk.BackColor = System.Drawing.Color.White
        Me.txtRmk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRmk.Location = New System.Drawing.Point(819, 4)
        Me.txtRmk.Multiline = True
        Me.txtRmk.Name = "txtRmk"
        Me.txtRmk.ReadOnly = True
        Me.txtRmk.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRmk.Size = New System.Drawing.Size(147, 43)
        Me.txtRmk.TabIndex = 195
        Me.txtRmk.Text = "빠른 결과보고 바람."
        '
        'pnlBldResult
        '
        Me.pnlBldResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBldResult.Controls.Add(Me.spdPatInfo)
        Me.pnlBldResult.Location = New System.Drawing.Point(105, 92)
        Me.pnlBldResult.Name = "pnlBldResult"
        Me.pnlBldResult.Size = New System.Drawing.Size(859, 70)
        Me.pnlBldResult.TabIndex = 192
        '
        'spdPatInfo
        '
        Me.spdPatInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdPatInfo.DataSource = Nothing
        Me.spdPatInfo.Location = New System.Drawing.Point(0, 0)
        Me.spdPatInfo.Name = "spdPatInfo"
        Me.spdPatInfo.OcxState = CType(resources.GetObject("spdPatInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPatInfo.Size = New System.Drawing.Size(862, 70)
        Me.spdPatInfo.TabIndex = 0
        '
        'btnSebu
        '
        Me.btnSebu.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSebu.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnSebu.ColorFillBlend = CBlendItems1
        Me.btnSebu.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSebu.Corners.All = CType(6, Short)
        Me.btnSebu.Corners.LowerLeft = CType(6, Short)
        Me.btnSebu.Corners.LowerRight = CType(6, Short)
        Me.btnSebu.Corners.UpperLeft = CType(6, Short)
        Me.btnSebu.Corners.UpperRight = CType(6, Short)
        Me.btnSebu.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSebu.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSebu.FocalPoints.CenterPtX = 0.4848485!
        Me.btnSebu.FocalPoints.CenterPtY = 0.52!
        Me.btnSebu.FocalPoints.FocusPtX = 0.0!
        Me.btnSebu.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSebu.FocusPtTracker = DesignerRectTracker2
        Me.btnSebu.Image = Nothing
        Me.btnSebu.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSebu.ImageIndex = 0
        Me.btnSebu.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSebu.Location = New System.Drawing.Point(4, 137)
        Me.btnSebu.Name = "btnSebu"
        Me.btnSebu.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSebu.SideImage = Nothing
        Me.btnSebu.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSebu.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSebu.Size = New System.Drawing.Size(99, 25)
        Me.btnSebu.TabIndex = 194
        Me.btnSebu.Text = "상세조회"
        Me.btnSebu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSebu.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSebu.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(3, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 44)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "최      근 검사 결과"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDiagNm
        '
        Me.lblDiagNm.BackColor = System.Drawing.Color.White
        Me.lblDiagNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDiagNm.ForeColor = System.Drawing.Color.Black
        Me.lblDiagNm.Location = New System.Drawing.Point(569, 70)
        Me.lblDiagNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDiagNm.Name = "lblDiagNm"
        Me.lblDiagNm.Size = New System.Drawing.Size(180, 21)
        Me.lblDiagNm.TabIndex = 189
        Me.lblDiagNm.Text = "감염성심내막염"
        Me.lblDiagNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSRmk
        '
        Me.txtSRmk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSRmk.BackColor = System.Drawing.Color.White
        Me.txtSRmk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSRmk.Location = New System.Drawing.Point(819, 48)
        Me.txtSRmk.Multiline = True
        Me.txtSRmk.Name = "txtSRmk"
        Me.txtSRmk.ReadOnly = True
        Me.txtSRmk.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSRmk.Size = New System.Drawing.Size(147, 43)
        Me.txtSRmk.TabIndex = 188
        Me.txtSRmk.Text = "2010-10-10 EM 나응급"
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label25.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.White
        Me.Label25.Location = New System.Drawing.Point(496, 48)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 21)
        Me.Label25.TabIndex = 187
        Me.Label25.Text = "수술일자"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRmk
        '
        Me.lblRmk.BackColor = System.Drawing.Color.White
        Me.lblRmk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRmk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRmk.ForeColor = System.Drawing.Color.Black
        Me.lblRmk.Location = New System.Drawing.Point(265, 70)
        Me.lblRmk.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRmk.Name = "lblRmk"
        Me.lblRmk.Size = New System.Drawing.Size(230, 21)
        Me.lblRmk.TabIndex = 177
        Me.lblRmk.Text = "NS환자입니다. "
        Me.lblRmk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label23.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.White
        Me.Label23.Location = New System.Drawing.Point(750, 48)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(69, 43)
        Me.Label23.TabIndex = 176
        Me.Label23.Text = "특이사항"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPhone
        '
        Me.lblPhone.BackColor = System.Drawing.Color.White
        Me.lblPhone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPhone.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPhone.ForeColor = System.Drawing.Color.Black
        Me.lblPhone.Location = New System.Drawing.Point(407, 48)
        Me.lblPhone.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(88, 21)
        Me.lblPhone.TabIndex = 54
        Me.lblPhone.Text = "010-1234-5678"
        Me.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label19.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(750, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(69, 43)
        Me.Label19.TabIndex = 52
        Me.Label19.Text = "의뢰의사 Remark"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOdate
        '
        Me.lblOdate.BackColor = System.Drawing.Color.White
        Me.lblOdate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOdate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOdate.ForeColor = System.Drawing.Color.Black
        Me.lblOdate.Location = New System.Drawing.Point(569, 48)
        Me.lblOdate.Margin = New System.Windows.Forms.Padding(0)
        Me.lblOdate.Name = "lblOdate"
        Me.lblOdate.Size = New System.Drawing.Size(180, 21)
        Me.lblOdate.TabIndex = 51
        Me.lblOdate.Text = "2010-10-12"
        Me.lblOdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(496, 70)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 21)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "진단명"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEmer
        '
        Me.lblEmer.BackColor = System.Drawing.Color.White
        Me.lblEmer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmer.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblEmer.ForeColor = System.Drawing.Color.Red
        Me.lblEmer.Location = New System.Drawing.Point(407, 26)
        Me.lblEmer.Margin = New System.Windows.Forms.Padding(0)
        Me.lblEmer.Name = "lblEmer"
        Me.lblEmer.Size = New System.Drawing.Size(88, 21)
        Me.lblEmer.TabIndex = 49
        Me.lblEmer.Text = "응급"
        Me.lblEmer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDoctor
        '
        Me.lblDoctor.BackColor = System.Drawing.Color.White
        Me.lblDoctor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDoctor.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctor.ForeColor = System.Drawing.Color.Black
        Me.lblDoctor.Location = New System.Drawing.Point(691, 4)
        Me.lblDoctor.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDoctor.Name = "lblDoctor"
        Me.lblDoctor.Size = New System.Drawing.Size(58, 21)
        Me.lblDoctor.TabIndex = 48
        Me.lblDoctor.Text = "나의사"
        Me.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWd
        '
        Me.lblWd.BackColor = System.Drawing.Color.White
        Me.lblWd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWd.ForeColor = System.Drawing.Color.Black
        Me.lblWd.Location = New System.Drawing.Point(691, 26)
        Me.lblWd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWd.Name = "lblWd"
        Me.lblWd.Size = New System.Drawing.Size(58, 21)
        Me.lblWd.TabIndex = 47
        Me.lblWd.Text = "1001"
        Me.lblWd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSr
        '
        Me.lblSr.BackColor = System.Drawing.Color.White
        Me.lblSr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSr.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSr.ForeColor = System.Drawing.Color.Black
        Me.lblSr.Location = New System.Drawing.Point(647, 26)
        Me.lblSr.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSr.Name = "lblSr"
        Me.lblSr.Size = New System.Drawing.Size(43, 21)
        Me.lblSr.TabIndex = 46
        Me.lblSr.Text = "101"
        Me.lblSr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdate
        '
        Me.lblIdate.BackColor = System.Drawing.Color.White
        Me.lblIdate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIdate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdate.ForeColor = System.Drawing.Color.Black
        Me.lblIdate.Location = New System.Drawing.Point(569, 26)
        Me.lblIdate.Margin = New System.Windows.Forms.Padding(0)
        Me.lblIdate.Name = "lblIdate"
        Me.lblIdate.Size = New System.Drawing.Size(77, 21)
        Me.lblIdate.TabIndex = 45
        Me.lblIdate.Text = "2010-09-15"
        Me.lblIdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDeptCd
        '
        Me.lblDeptCd.BackColor = System.Drawing.Color.White
        Me.lblDeptCd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeptCd.ForeColor = System.Drawing.Color.Black
        Me.lblDeptCd.Location = New System.Drawing.Point(647, 4)
        Me.lblDeptCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDeptCd.Name = "lblDeptCd"
        Me.lblDeptCd.Size = New System.Drawing.Size(43, 21)
        Me.lblDeptCd.TabIndex = 44
        Me.lblDeptCd.Text = "CP"
        Me.lblDeptCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdDate
        '
        Me.lblOrdDate.BackColor = System.Drawing.Color.White
        Me.lblOrdDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDate.ForeColor = System.Drawing.Color.Black
        Me.lblOrdDate.Location = New System.Drawing.Point(569, 4)
        Me.lblOrdDate.Margin = New System.Windows.Forms.Padding(0)
        Me.lblOrdDate.Name = "lblOrdDate"
        Me.lblOrdDate.Size = New System.Drawing.Size(77, 21)
        Me.lblOrdDate.TabIndex = 43
        Me.lblOrdDate.Text = "2010-10-10"
        Me.lblOrdDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label17.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(496, 26)
        Me.Label17.Margin = New System.Windows.Forms.Padding(0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(72, 21)
        Me.Label17.TabIndex = 42
        Me.Label17.Text = "재원정보"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(496, 4)
        Me.Label18.Margin = New System.Windows.Forms.Padding(0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(72, 21)
        Me.Label18.TabIndex = 41
        Me.Label18.Text = "처방일자"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInfInfo
        '
        Me.lblInfInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblInfInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInfInfo.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblInfInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfInfo.Location = New System.Drawing.Point(407, 4)
        Me.lblInfInfo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblInfInfo.Name = "lblInfInfo"
        Me.lblInfInfo.Size = New System.Drawing.Size(88, 21)
        Me.lblInfInfo.TabIndex = 40
        Me.lblInfInfo.Text = "HIV"
        Me.lblInfInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJumino
        '
        Me.lblJumino.BackColor = System.Drawing.Color.White
        Me.lblJumino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblJumino.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJumino.ForeColor = System.Drawing.Color.Black
        Me.lblJumino.Location = New System.Drawing.Point(265, 48)
        Me.lblJumino.Margin = New System.Windows.Forms.Padding(0)
        Me.lblJumino.Name = "lblJumino"
        Me.lblJumino.Size = New System.Drawing.Size(141, 21)
        Me.lblJumino.TabIndex = 39
        Me.lblJumino.Text = "850701-2******"
        Me.lblJumino.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblNation
        '
        Me.lblNation.BackColor = System.Drawing.Color.White
        Me.lblNation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNation.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblNation.ForeColor = System.Drawing.Color.Black
        Me.lblNation.Location = New System.Drawing.Point(334, 4)
        Me.lblNation.Margin = New System.Windows.Forms.Padding(0)
        Me.lblNation.Name = "lblNation"
        Me.lblNation.Size = New System.Drawing.Size(72, 21)
        Me.lblNation.TabIndex = 38
        Me.lblNation.Text = "깜둥이"
        Me.lblNation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAbo
        '
        Me.lblAbo.BackColor = System.Drawing.Color.White
        Me.lblAbo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAbo.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAbo.ForeColor = System.Drawing.Color.Red
        Me.lblAbo.Location = New System.Drawing.Point(104, 4)
        Me.lblAbo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblAbo.Name = "lblAbo"
        Me.lblAbo.Size = New System.Drawing.Size(161, 87)
        Me.lblAbo.TabIndex = 37
        Me.lblAbo.Text = "AB+"
        Me.lblAbo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWeight
        '
        Me.lblWeight.BackColor = System.Drawing.Color.White
        Me.lblWeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWeight.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWeight.ForeColor = System.Drawing.Color.Black
        Me.lblWeight.Location = New System.Drawing.Point(334, 26)
        Me.lblWeight.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(72, 21)
        Me.lblWeight.TabIndex = 36
        Me.lblWeight.Text = "59.8"
        Me.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHeight
        '
        Me.lblHeight.BackColor = System.Drawing.Color.White
        Me.lblHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHeight.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblHeight.ForeColor = System.Drawing.Color.Black
        Me.lblHeight.Location = New System.Drawing.Point(265, 26)
        Me.lblHeight.Margin = New System.Windows.Forms.Padding(0)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Size = New System.Drawing.Size(68, 21)
        Me.lblHeight.TabIndex = 35
        Me.lblHeight.Text = "177.8"
        Me.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSexAge
        '
        Me.lblSexAge.BackColor = System.Drawing.Color.White
        Me.lblSexAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSexAge.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSexAge.ForeColor = System.Drawing.Color.Black
        Me.lblSexAge.Location = New System.Drawing.Point(265, 4)
        Me.lblSexAge.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSexAge.Name = "lblSexAge"
        Me.lblSexAge.Size = New System.Drawing.Size(68, 21)
        Me.lblSexAge.TabIndex = 34
        Me.lblSexAge.Text = "M/100"
        Me.lblSexAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatNm
        '
        Me.lblPatNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblPatNm.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatNm.ForeColor = System.Drawing.Color.White
        Me.lblPatNm.Location = New System.Drawing.Point(3, 48)
        Me.lblPatNm.Name = "lblPatNm"
        Me.lblPatNm.Size = New System.Drawing.Size(100, 43)
        Me.lblPatNm.TabIndex = 33
        Me.lblPatNm.Text = "나환자아들"
        Me.lblPatNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegno
        '
        Me.lblRegno.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblRegno.ContextMenuStrip = Me.cmuLink
        Me.lblRegno.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegno.ForeColor = System.Drawing.Color.White
        Me.lblRegno.Location = New System.Drawing.Point(3, 4)
        Me.lblRegno.Name = "lblRegno"
        Me.lblRegno.Size = New System.Drawing.Size(100, 43)
        Me.lblRegno.TabIndex = 32
        Me.lblRegno.Text = "012345678"
        Me.lblRegno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmuLink
        '
        Me.cmuLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCopy_regno})
        Me.cmuLink.Name = "cmuRstList"
        Me.cmuLink.Size = New System.Drawing.Size(153, 26)
        Me.cmuLink.Text = "상황에 맞는 메뉴"
        '
        'mnuCopy_regno
        '
        Me.mnuCopy_regno.Name = "mnuCopy_regno"
        Me.mnuCopy_regno.Size = New System.Drawing.Size(152, 22)
        Me.mnuCopy_regno.Text = "등록번호 복사"
        '
        'AxTnsPatinfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Controls.Add(Me.Panel1)
        Me.Name = "AxTnsPatinfo"
        Me.Size = New System.Drawing.Size(976, 173)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlBldResult.ResumeLayout(False)
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmuLink.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNm As System.Windows.Forms.Label
    Friend WithEvents txtSRmk As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents lblRmk As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblPhone As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblOdate As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblEmer As System.Windows.Forms.Label
    Friend WithEvents lblDoctor As System.Windows.Forms.Label
    Friend WithEvents lblWd As System.Windows.Forms.Label
    Friend WithEvents lblSr As System.Windows.Forms.Label
    Friend WithEvents lblIdate As System.Windows.Forms.Label
    Friend WithEvents lblDeptCd As System.Windows.Forms.Label
    Friend WithEvents lblOrdDate As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblInfInfo As System.Windows.Forms.Label
    Friend WithEvents lblJumino As System.Windows.Forms.Label
    Friend WithEvents lblNation As System.Windows.Forms.Label
    Friend WithEvents lblAbo As System.Windows.Forms.Label
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents lblSexAge As System.Windows.Forms.Label
    Friend WithEvents lblPatNm As System.Windows.Forms.Label
    Friend WithEvents lblRegno As System.Windows.Forms.Label
    Friend WithEvents btnSebu As CButtonLib.CButton
    Friend WithEvents pnlBldResult As System.Windows.Forms.Panel
    Friend WithEvents spdPatInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtRmk As System.Windows.Forms.TextBox
    Friend WithEvents cmuLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuCopy_regno As System.Windows.Forms.ToolStripMenuItem

End Class
