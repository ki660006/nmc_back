<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR08_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR08_S01))
        Me.grbOrdInfo = New System.Windows.Forms.GroupBox()
        Me.spdOrdDt = New AxFPSpreadADO.AxfpSpread()
        Me.spdOrdInfo = New AxFPSpreadADO.AxfpSpread()
        Me.grbSujinInfo = New System.Windows.Forms.GroupBox()
        Me.lblSujinCount = New System.Windows.Forms.Label()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.grbPatInfo = New System.Windows.Forms.GroupBox()
        Me.lblTel = New System.Windows.Forms.Label()
        Me.lblSexAge = New System.Windows.Forms.Label()
        Me.lblAddr = New System.Windows.Forms.Label()
        Me.lblIdNo = New System.Windows.Forms.Label()
        Me.lblRegNo = New System.Windows.Forms.Label()
        Me.txtAddr2 = New System.Windows.Forms.TextBox()
        Me.txtSexAge = New System.Windows.Forms.TextBox()
        Me.txtAddr1 = New System.Windows.Forms.TextBox()
        Me.txtPatNm = New System.Windows.Forms.TextBox()
        Me.txtRegNo = New System.Windows.Forms.TextBox()
        Me.txtTel = New System.Windows.Forms.TextBox()
        Me.txtWardRoom = New System.Windows.Forms.TextBox()
        Me.txtIdNo = New System.Windows.Forms.TextBox()
        Me.tbcAllInfo = New System.Windows.Forms.TabControl()
        Me.SujinInfo = New System.Windows.Forms.TabPage()
        Me.PastOpInfo = New System.Windows.Forms.TabPage()
        Me.grbOpInfo = New System.Windows.Forms.GroupBox()
        Me.lblOpcount = New System.Windows.Forms.Label()
        Me.spdOpInfo = New AxFPSpreadADO.AxfpSpread()
        Me.PastTnsInfo = New System.Windows.Forms.TabPage()
        Me.grbTnsInfo = New System.Windows.Forms.GroupBox()
        Me.lblTnscount = New System.Windows.Forms.Label()
        Me.spdTnsInfo = New AxFPSpreadADO.AxfpSpread()
        Me.grbOrdInfo.SuspendLayout()
        CType(Me.spdOrdDt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdOrdInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbSujinInfo.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbPatInfo.SuspendLayout()
        Me.tbcAllInfo.SuspendLayout()
        Me.SujinInfo.SuspendLayout()
        Me.PastOpInfo.SuspendLayout()
        Me.grbOpInfo.SuspendLayout()
        CType(Me.spdOpInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PastTnsInfo.SuspendLayout()
        Me.grbTnsInfo.SuspendLayout()
        CType(Me.spdTnsInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grbOrdInfo
        '
        Me.grbOrdInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbOrdInfo.Controls.Add(Me.spdOrdDt)
        Me.grbOrdInfo.Controls.Add(Me.spdOrdInfo)
        Me.grbOrdInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grbOrdInfo.ForeColor = System.Drawing.Color.Purple
        Me.grbOrdInfo.Location = New System.Drawing.Point(8, 287)
        Me.grbOrdInfo.Name = "grbOrdInfo"
        Me.grbOrdInfo.Size = New System.Drawing.Size(1053, 236)
        Me.grbOrdInfo.TabIndex = 5
        Me.grbOrdInfo.TabStop = False
        Me.grbOrdInfo.Text = "수진기간 중 처방이력"
        '
        'spdOrdDt
        '
        Me.spdOrdDt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdOrdDt.DataSource = Nothing
        Me.spdOrdDt.Location = New System.Drawing.Point(18, 20)
        Me.spdOrdDt.Name = "spdOrdDt"
        Me.spdOrdDt.OcxState = CType(resources.GetObject("spdOrdDt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrdDt.Size = New System.Drawing.Size(150, 210)
        Me.spdOrdDt.TabIndex = 1
        '
        'spdOrdInfo
        '
        Me.spdOrdInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdOrdInfo.DataSource = Nothing
        Me.spdOrdInfo.Location = New System.Drawing.Point(174, 20)
        Me.spdOrdInfo.Name = "spdOrdInfo"
        Me.spdOrdInfo.OcxState = CType(resources.GetObject("spdOrdInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrdInfo.Size = New System.Drawing.Size(863, 210)
        Me.spdOrdInfo.TabIndex = 0
        '
        'grbSujinInfo
        '
        Me.grbSujinInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbSujinInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grbSujinInfo.Controls.Add(Me.lblSujinCount)
        Me.grbSujinInfo.Controls.Add(Me.spdList)
        Me.grbSujinInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grbSujinInfo.ForeColor = System.Drawing.Color.Purple
        Me.grbSujinInfo.Location = New System.Drawing.Point(8, 9)
        Me.grbSujinInfo.Name = "grbSujinInfo"
        Me.grbSujinInfo.Size = New System.Drawing.Size(1053, 272)
        Me.grbSujinInfo.TabIndex = 4
        Me.grbSujinInfo.TabStop = False
        Me.grbSujinInfo.Text = "수진이력"
        '
        'lblSujinCount
        '
        Me.lblSujinCount.AutoSize = True
        Me.lblSujinCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSujinCount.Location = New System.Drawing.Point(16, 258)
        Me.lblSujinCount.Name = "lblSujinCount"
        Me.lblSujinCount.Size = New System.Drawing.Size(31, 12)
        Me.lblSujinCount.TabIndex = 11
        Me.lblSujinCount.Text = "건수"
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(18, 21)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1019, 232)
        Me.spdList.TabIndex = 10
        '
        'grbPatInfo
        '
        Me.grbPatInfo.Controls.Add(Me.lblTel)
        Me.grbPatInfo.Controls.Add(Me.lblSexAge)
        Me.grbPatInfo.Controls.Add(Me.lblAddr)
        Me.grbPatInfo.Controls.Add(Me.lblIdNo)
        Me.grbPatInfo.Controls.Add(Me.lblRegNo)
        Me.grbPatInfo.Controls.Add(Me.txtAddr2)
        Me.grbPatInfo.Controls.Add(Me.txtSexAge)
        Me.grbPatInfo.Controls.Add(Me.txtAddr1)
        Me.grbPatInfo.Controls.Add(Me.txtPatNm)
        Me.grbPatInfo.Controls.Add(Me.txtRegNo)
        Me.grbPatInfo.Controls.Add(Me.txtTel)
        Me.grbPatInfo.Controls.Add(Me.txtWardRoom)
        Me.grbPatInfo.Controls.Add(Me.txtIdNo)
        Me.grbPatInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grbPatInfo.ForeColor = System.Drawing.Color.Purple
        Me.grbPatInfo.Location = New System.Drawing.Point(12, 9)
        Me.grbPatInfo.Name = "grbPatInfo"
        Me.grbPatInfo.Size = New System.Drawing.Size(1076, 91)
        Me.grbPatInfo.TabIndex = 6
        Me.grbPatInfo.TabStop = False
        Me.grbPatInfo.Text = "신상정보"
        '
        'lblTel
        '
        Me.lblTel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTel.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTel.ForeColor = System.Drawing.Color.Black
        Me.lblTel.Location = New System.Drawing.Point(422, 40)
        Me.lblTel.Name = "lblTel"
        Me.lblTel.Size = New System.Drawing.Size(81, 21)
        Me.lblTel.TabIndex = 213
        Me.lblTel.Text = "연락처"
        Me.lblTel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSexAge
        '
        Me.lblSexAge.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSexAge.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSexAge.ForeColor = System.Drawing.Color.Black
        Me.lblSexAge.Location = New System.Drawing.Point(216, 40)
        Me.lblSexAge.Name = "lblSexAge"
        Me.lblSexAge.Size = New System.Drawing.Size(79, 21)
        Me.lblSexAge.TabIndex = 212
        Me.lblSexAge.Text = "Sex/Age"
        Me.lblSexAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAddr
        '
        Me.lblAddr.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblAddr.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAddr.ForeColor = System.Drawing.Color.Black
        Me.lblAddr.Location = New System.Drawing.Point(10, 62)
        Me.lblAddr.Name = "lblAddr"
        Me.lblAddr.Size = New System.Drawing.Size(79, 21)
        Me.lblAddr.TabIndex = 211
        Me.lblAddr.Text = "주소"
        Me.lblAddr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdNo
        '
        Me.lblIdNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblIdNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdNo.ForeColor = System.Drawing.Color.Black
        Me.lblIdNo.Location = New System.Drawing.Point(10, 40)
        Me.lblIdNo.Name = "lblIdNo"
        Me.lblIdNo.Size = New System.Drawing.Size(79, 21)
        Me.lblIdNo.TabIndex = 210
        Me.lblIdNo.Text = "주민등록번호"
        Me.lblIdNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRegNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.Color.White
        Me.lblRegNo.Location = New System.Drawing.Point(10, 18)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(79, 21)
        Me.lblRegNo.TabIndex = 209
        Me.lblRegNo.Text = "등록번호"
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtAddr2
        '
        Me.txtAddr2.BackColor = System.Drawing.Color.White
        Me.txtAddr2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAddr2.Location = New System.Drawing.Point(399, 62)
        Me.txtAddr2.Name = "txtAddr2"
        Me.txtAddr2.ReadOnly = True
        Me.txtAddr2.Size = New System.Drawing.Size(217, 21)
        Me.txtAddr2.TabIndex = 22
        Me.txtAddr2.Text = "710101-1234567"
        '
        'txtSexAge
        '
        Me.txtSexAge.BackColor = System.Drawing.Color.White
        Me.txtSexAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSexAge.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSexAge.Location = New System.Drawing.Point(296, 40)
        Me.txtSexAge.Name = "txtSexAge"
        Me.txtSexAge.ReadOnly = True
        Me.txtSexAge.Size = New System.Drawing.Size(102, 21)
        Me.txtSexAge.TabIndex = 12
        Me.txtSexAge.Text = "710101-1234567"
        Me.txtSexAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAddr1
        '
        Me.txtAddr1.BackColor = System.Drawing.Color.White
        Me.txtAddr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAddr1.Location = New System.Drawing.Point(90, 62)
        Me.txtAddr1.Name = "txtAddr1"
        Me.txtAddr1.ReadOnly = True
        Me.txtAddr1.Size = New System.Drawing.Size(308, 21)
        Me.txtAddr1.TabIndex = 20
        Me.txtAddr1.Text = "710101-1234567"
        '
        'txtPatNm
        '
        Me.txtPatNm.BackColor = System.Drawing.Color.White
        Me.txtPatNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPatNm.Location = New System.Drawing.Point(193, 18)
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.ReadOnly = True
        Me.txtPatNm.Size = New System.Drawing.Size(102, 21)
        Me.txtPatNm.TabIndex = 11
        Me.txtPatNm.Text = "710101-1234567"
        Me.txtPatNm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRegNo
        '
        Me.txtRegNo.BackColor = System.Drawing.Color.White
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNo.Location = New System.Drawing.Point(90, 18)
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.ReadOnly = True
        Me.txtRegNo.Size = New System.Drawing.Size(102, 21)
        Me.txtRegNo.TabIndex = 10
        Me.txtRegNo.Text = "710101-1234567"
        Me.txtRegNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTel
        '
        Me.txtTel.BackColor = System.Drawing.Color.White
        Me.txtTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTel.Location = New System.Drawing.Point(505, 40)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.ReadOnly = True
        Me.txtTel.Size = New System.Drawing.Size(111, 21)
        Me.txtTel.TabIndex = 16
        Me.txtTel.Text = "710101-1234567"
        '
        'txtWardRoom
        '
        Me.txtWardRoom.BackColor = System.Drawing.Color.White
        Me.txtWardRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWardRoom.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWardRoom.Location = New System.Drawing.Point(296, 18)
        Me.txtWardRoom.Name = "txtWardRoom"
        Me.txtWardRoom.ReadOnly = True
        Me.txtWardRoom.Size = New System.Drawing.Size(102, 21)
        Me.txtWardRoom.TabIndex = 13
        Me.txtWardRoom.Text = "710101-1234567"
        Me.txtWardRoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtIdNo
        '
        Me.txtIdNo.BackColor = System.Drawing.Color.White
        Me.txtIdNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtIdNo.Location = New System.Drawing.Point(90, 40)
        Me.txtIdNo.Name = "txtIdNo"
        Me.txtIdNo.ReadOnly = True
        Me.txtIdNo.Size = New System.Drawing.Size(102, 21)
        Me.txtIdNo.TabIndex = 14
        Me.txtIdNo.Text = "710101-1234567"
        Me.txtIdNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tbcAllInfo
        '
        Me.tbcAllInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbcAllInfo.Controls.Add(Me.SujinInfo)
        Me.tbcAllInfo.Controls.Add(Me.PastOpInfo)
        Me.tbcAllInfo.Controls.Add(Me.PastTnsInfo)
        Me.tbcAllInfo.Location = New System.Drawing.Point(12, 104)
        Me.tbcAllInfo.Name = "tbcAllInfo"
        Me.tbcAllInfo.SelectedIndex = 0
        Me.tbcAllInfo.Size = New System.Drawing.Size(1076, 555)
        Me.tbcAllInfo.TabIndex = 7
        '
        'SujinInfo
        '
        Me.SujinInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.SujinInfo.Controls.Add(Me.grbOrdInfo)
        Me.SujinInfo.Controls.Add(Me.grbSujinInfo)
        Me.SujinInfo.Location = New System.Drawing.Point(4, 22)
        Me.SujinInfo.Name = "SujinInfo"
        Me.SujinInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.SujinInfo.Size = New System.Drawing.Size(1068, 529)
        Me.SujinInfo.TabIndex = 0
        Me.SujinInfo.Text = "수진이력"
        '
        'PastOpInfo
        '
        Me.PastOpInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PastOpInfo.Controls.Add(Me.grbOpInfo)
        Me.PastOpInfo.Location = New System.Drawing.Point(4, 22)
        Me.PastOpInfo.Name = "PastOpInfo"
        Me.PastOpInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.PastOpInfo.Size = New System.Drawing.Size(1068, 529)
        Me.PastOpInfo.TabIndex = 1
        Me.PastOpInfo.Text = "수술이력"
        '
        'grbOpInfo
        '
        Me.grbOpInfo.Controls.Add(Me.lblOpcount)
        Me.grbOpInfo.Controls.Add(Me.spdOpInfo)
        Me.grbOpInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grbOpInfo.ForeColor = System.Drawing.Color.Purple
        Me.grbOpInfo.Location = New System.Drawing.Point(8, 9)
        Me.grbOpInfo.Name = "grbOpInfo"
        Me.grbOpInfo.Size = New System.Drawing.Size(1053, 492)
        Me.grbOpInfo.TabIndex = 0
        Me.grbOpInfo.TabStop = False
        Me.grbOpInfo.Text = "수술이력"
        '
        'lblOpcount
        '
        Me.lblOpcount.AutoSize = True
        Me.lblOpcount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblOpcount.Location = New System.Drawing.Point(16, 445)
        Me.lblOpcount.Name = "lblOpcount"
        Me.lblOpcount.Size = New System.Drawing.Size(31, 12)
        Me.lblOpcount.TabIndex = 1
        Me.lblOpcount.Text = "건수"
        '
        'spdOpInfo
        '
        Me.spdOpInfo.DataSource = Nothing
        Me.spdOpInfo.Location = New System.Drawing.Point(18, 21)
        Me.spdOpInfo.Name = "spdOpInfo"
        Me.spdOpInfo.OcxState = CType(resources.GetObject("spdOpInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOpInfo.Size = New System.Drawing.Size(1013, 419)
        Me.spdOpInfo.TabIndex = 0
        '
        'PastTnsInfo
        '
        Me.PastTnsInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PastTnsInfo.Controls.Add(Me.grbTnsInfo)
        Me.PastTnsInfo.Location = New System.Drawing.Point(4, 22)
        Me.PastTnsInfo.Name = "PastTnsInfo"
        Me.PastTnsInfo.Size = New System.Drawing.Size(1068, 529)
        Me.PastTnsInfo.TabIndex = 2
        Me.PastTnsInfo.Text = "수혈이력"
        '
        'grbTnsInfo
        '
        Me.grbTnsInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grbTnsInfo.Controls.Add(Me.lblTnscount)
        Me.grbTnsInfo.Controls.Add(Me.spdTnsInfo)
        Me.grbTnsInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grbTnsInfo.ForeColor = System.Drawing.Color.Purple
        Me.grbTnsInfo.Location = New System.Drawing.Point(8, 9)
        Me.grbTnsInfo.Name = "grbTnsInfo"
        Me.grbTnsInfo.Size = New System.Drawing.Size(1045, 497)
        Me.grbTnsInfo.TabIndex = 0
        Me.grbTnsInfo.TabStop = False
        Me.grbTnsInfo.Text = "수혈이력"
        '
        'lblTnscount
        '
        Me.lblTnscount.AutoSize = True
        Me.lblTnscount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTnscount.Location = New System.Drawing.Point(16, 445)
        Me.lblTnscount.Name = "lblTnscount"
        Me.lblTnscount.Size = New System.Drawing.Size(31, 12)
        Me.lblTnscount.TabIndex = 1
        Me.lblTnscount.Text = "건수"
        '
        'spdTnsInfo
        '
        Me.spdTnsInfo.DataSource = Nothing
        Me.spdTnsInfo.Location = New System.Drawing.Point(18, 21)
        Me.spdTnsInfo.Name = "spdTnsInfo"
        Me.spdTnsInfo.OcxState = CType(resources.GetObject("spdTnsInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTnsInfo.Size = New System.Drawing.Size(1013, 419)
        Me.spdTnsInfo.TabIndex = 0
        '
        'FGR08_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1101, 677)
        Me.Controls.Add(Me.tbcAllInfo)
        Me.Controls.Add(Me.grbPatInfo)
        Me.Name = "FGR08_S01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "환자정보조회"
        Me.grbOrdInfo.ResumeLayout(False)
        CType(Me.spdOrdDt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdOrdInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbSujinInfo.ResumeLayout(False)
        Me.grbSujinInfo.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbPatInfo.ResumeLayout(False)
        Me.grbPatInfo.PerformLayout()
        Me.tbcAllInfo.ResumeLayout(False)
        Me.SujinInfo.ResumeLayout(False)
        Me.PastOpInfo.ResumeLayout(False)
        Me.grbOpInfo.ResumeLayout(False)
        Me.grbOpInfo.PerformLayout()
        CType(Me.spdOpInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PastTnsInfo.ResumeLayout(False)
        Me.grbTnsInfo.ResumeLayout(False)
        Me.grbTnsInfo.PerformLayout()
        CType(Me.spdTnsInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbOrdInfo As System.Windows.Forms.GroupBox
    Friend WithEvents grbSujinInfo As System.Windows.Forms.GroupBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Public WithEvents spdOrdInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblSujinCount As System.Windows.Forms.Label
    Public WithEvents spdOrdDt As AxFPSpreadADO.AxfpSpread
    Friend WithEvents grbPatInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtAddr2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSexAge As System.Windows.Forms.TextBox
    Friend WithEvents txtAddr1 As System.Windows.Forms.TextBox
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents txtWardRoom As System.Windows.Forms.TextBox
    Friend WithEvents txtIdNo As System.Windows.Forms.TextBox
    Friend WithEvents tbcAllInfo As System.Windows.Forms.TabControl
    Friend WithEvents SujinInfo As System.Windows.Forms.TabPage
    Friend WithEvents PastOpInfo As System.Windows.Forms.TabPage
    Friend WithEvents PastTnsInfo As System.Windows.Forms.TabPage
    Friend WithEvents grbTnsInfo As System.Windows.Forms.GroupBox
    Friend WithEvents spdTnsInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents grbOpInfo As System.Windows.Forms.GroupBox
    Friend WithEvents spdOpInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents lblIdNo As System.Windows.Forms.Label
    Friend WithEvents lblSexAge As System.Windows.Forms.Label
    Friend WithEvents lblAddr As System.Windows.Forms.Label
    Friend WithEvents lblTel As System.Windows.Forms.Label
    Friend WithEvents lblOpcount As System.Windows.Forms.Label
    Friend WithEvents lblTnscount As System.Windows.Forms.Label
End Class
