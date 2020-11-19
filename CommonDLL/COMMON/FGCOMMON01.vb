Public Class FGCOMMON01
    Inherits System.Windows.Forms.Form

    Public mSpdRst As AxFPSpreadADO.AxfpSpread

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        mSpdRst = spdOrdListR
    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents imlPlusMinus As System.Windows.Forms.ImageList
    Friend WithEvents imlMultiRst As System.Windows.Forms.ImageList
    Friend WithEvents imIChkBox As System.Windows.Forms.ImageList
    Friend WithEvents imlSIR As System.Windows.Forms.ImageList
    Friend WithEvents imlReportChk As System.Windows.Forms.ImageList
    Friend WithEvents imlSingleSel As System.Windows.Forms.ImageList
    Friend WithEvents spdOrdListR As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents imlPatGbn As System.Windows.Forms.ImageList
    Friend WithEvents picBullet As System.Windows.Forms.PictureBox
    Friend WithEvents picTxt As System.Windows.Forms.PictureBox
    Friend WithEvents picLeaf As System.Windows.Forms.PictureBox
    Public WithEvents spdTemp As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCOMMON01))
        Me.imlPlusMinus = New System.Windows.Forms.ImageList(Me.components)
        Me.imlMultiRst = New System.Windows.Forms.ImageList(Me.components)
        Me.imIChkBox = New System.Windows.Forms.ImageList(Me.components)
        Me.imlSIR = New System.Windows.Forms.ImageList(Me.components)
        Me.imlReportChk = New System.Windows.Forms.ImageList(Me.components)
        Me.imlSingleSel = New System.Windows.Forms.ImageList(Me.components)
        Me.spdOrdListR = New AxFPSpreadADO.AxfpSpread
        Me.Label1 = New System.Windows.Forms.Label
        Me.imlPatGbn = New System.Windows.Forms.ImageList(Me.components)
        Me.picBullet = New System.Windows.Forms.PictureBox
        Me.picTxt = New System.Windows.Forms.PictureBox
        Me.picLeaf = New System.Windows.Forms.PictureBox
        Me.spdTemp = New AxFPSpreadADO.AxfpSpread
        CType(Me.spdOrdListR, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBullet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTxt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLeaf, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdTemp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imlPlusMinus
        '
        Me.imlPlusMinus.ImageStream = CType(resources.GetObject("imlPlusMinus.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlPlusMinus.TransparentColor = System.Drawing.Color.Transparent
        Me.imlPlusMinus.Images.SetKeyName(0, "")
        Me.imlPlusMinus.Images.SetKeyName(1, "")
        '
        'imlMultiRst
        '
        Me.imlMultiRst.ImageStream = CType(resources.GetObject("imlMultiRst.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlMultiRst.TransparentColor = System.Drawing.Color.Transparent
        Me.imlMultiRst.Images.SetKeyName(0, "")
        '
        'imIChkBox
        '
        Me.imIChkBox.ImageStream = CType(resources.GetObject("imIChkBox.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imIChkBox.TransparentColor = System.Drawing.Color.Transparent
        Me.imIChkBox.Images.SetKeyName(0, "")
        Me.imIChkBox.Images.SetKeyName(1, "")
        '
        'imlSIR
        '
        Me.imlSIR.ImageStream = CType(resources.GetObject("imlSIR.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlSIR.TransparentColor = System.Drawing.Color.Transparent
        Me.imlSIR.Images.SetKeyName(0, "")
        Me.imlSIR.Images.SetKeyName(1, "")
        Me.imlSIR.Images.SetKeyName(2, "")
        '
        'imlReportChk
        '
        Me.imlReportChk.ImageStream = CType(resources.GetObject("imlReportChk.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlReportChk.TransparentColor = System.Drawing.Color.Transparent
        Me.imlReportChk.Images.SetKeyName(0, "")
        Me.imlReportChk.Images.SetKeyName(1, "")
        '
        'imlSingleSel
        '
        Me.imlSingleSel.ImageStream = CType(resources.GetObject("imlSingleSel.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlSingleSel.TransparentColor = System.Drawing.Color.Transparent
        Me.imlSingleSel.Images.SetKeyName(0, "")
        '
        'spdOrdListR
        '
        Me.spdOrdListR.Location = New System.Drawing.Point(12, 104)
        Me.spdOrdListR.Name = "spdOrdListR"
        Me.spdOrdListR.OcxState = CType(resources.GetObject("spdOrdListR.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrdListR.Size = New System.Drawing.Size(716, 324)
        Me.spdOrdListR.TabIndex = 141
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Brown
        Me.Label1.ForeColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(716, 23)
        Me.Label1.TabIndex = 142
        Me.Label1.Text = "*** 결과등록 공통 Spread ***"
        '
        'imlPatGbn
        '
        Me.imlPatGbn.ImageStream = CType(resources.GetObject("imlPatGbn.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imlPatGbn.TransparentColor = System.Drawing.Color.Transparent
        Me.imlPatGbn.Images.SetKeyName(0, "")
        Me.imlPatGbn.Images.SetKeyName(1, "")
        Me.imlPatGbn.Images.SetKeyName(2, "")
        '
        'picBullet
        '
        Me.picBullet.BackColor = System.Drawing.Color.White
        Me.picBullet.Image = CType(resources.GetObject("picBullet.Image"), System.Drawing.Image)
        Me.picBullet.Location = New System.Drawing.Point(60, 20)
        Me.picBullet.Name = "picBullet"
        Me.picBullet.Size = New System.Drawing.Size(6, 6)
        Me.picBullet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picBullet.TabIndex = 143
        Me.picBullet.TabStop = False
        '
        'picTxt
        '
        Me.picTxt.BackColor = System.Drawing.Color.White
        Me.picTxt.Image = CType(resources.GetObject("picTxt.Image"), System.Drawing.Image)
        Me.picTxt.Location = New System.Drawing.Point(80, 16)
        Me.picTxt.Name = "picTxt"
        Me.picTxt.Size = New System.Drawing.Size(16, 16)
        Me.picTxt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picTxt.TabIndex = 144
        Me.picTxt.TabStop = False
        '
        'picLeaf
        '
        Me.picLeaf.BackColor = System.Drawing.Color.White
        Me.picLeaf.Image = CType(resources.GetObject("picLeaf.Image"), System.Drawing.Image)
        Me.picLeaf.Location = New System.Drawing.Point(112, 18)
        Me.picLeaf.Name = "picLeaf"
        Me.picLeaf.Size = New System.Drawing.Size(13, 13)
        Me.picLeaf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.picLeaf.TabIndex = 145
        Me.picLeaf.TabStop = False
        '
        'spdTemp
        '
        Me.spdTemp.Location = New System.Drawing.Point(448, 12)
        Me.spdTemp.Name = "spdTemp"
        Me.spdTemp.OcxState = CType(resources.GetObject("spdTemp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTemp.Size = New System.Drawing.Size(116, 50)
        Me.spdTemp.TabIndex = 146
        '
        'FGCOMMON01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(752, 453)
        Me.Controls.Add(Me.spdTemp)
        Me.Controls.Add(Me.picLeaf)
        Me.Controls.Add(Me.picTxt)
        Me.Controls.Add(Me.picBullet)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.spdOrdListR)
        Me.Name = "FGCOMMON01"
        Me.Text = "FGCOMMON01"
        CType(Me.spdOrdListR, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBullet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTxt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLeaf, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdTemp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

End Class
