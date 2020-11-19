Imports COMMON.CommFN

Public Class FGF02
    Inherits System.Windows.Forms.Form

    Public msDate As String
    Public msTIme As String
    Friend WithEvents btnCancel As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents lblUSDayTime As System.Windows.Forms.Label
    Public msAction As String

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

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
    Friend WithEvents dtpUETime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpUEDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGF02))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.dtpUETime = New System.Windows.Forms.DateTimePicker
        Me.dtpUEDate = New System.Windows.Forms.DateTimePicker
        Me.lblMsg = New System.Windows.Forms.Label
        Me.btnCancel = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.lblUSDayTime = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'dtpUETime
        '
        Me.dtpUETime.CustomFormat = "HH:mm:ss"
        Me.dtpUETime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpUETime.Location = New System.Drawing.Point(224, 93)
        Me.dtpUETime.Name = "dtpUETime"
        Me.dtpUETime.Size = New System.Drawing.Size(54, 21)
        Me.dtpUETime.TabIndex = 62
        Me.dtpUETime.TabStop = False
        Me.dtpUETime.Value = New Date(2003, 11, 4, 0, 0, 0, 0)
        '
        'dtpUEDate
        '
        Me.dtpUEDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpUEDate.Location = New System.Drawing.Point(136, 93)
        Me.dtpUEDate.Name = "dtpUEDate"
        Me.dtpUEDate.Size = New System.Drawing.Size(88, 21)
        Me.dtpUEDate.TabIndex = 63
        '
        'lblMsg
        '
        Me.lblMsg.Location = New System.Drawing.Point(16, 8)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(288, 72)
        Me.lblMsg.TabIndex = 64
        Me.lblMsg.Text = "Label1"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnCancel.ColorFillBlend = CBlendItems1
        Me.btnCancel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnCancel.Corners.All = CType(6, Short)
        Me.btnCancel.Corners.LowerLeft = CType(6, Short)
        Me.btnCancel.Corners.LowerRight = CType(6, Short)
        Me.btnCancel.Corners.UpperLeft = CType(6, Short)
        Me.btnCancel.Corners.UpperRight = CType(6, Short)
        Me.btnCancel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnCancel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnCancel.FocalPoints.CenterPtX = 0.5!
        Me.btnCancel.FocalPoints.CenterPtY = 0.0!
        Me.btnCancel.FocalPoints.FocusPtX = 0.0!
        Me.btnCancel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.FocusPtTracker = DesignerRectTracker2
        Me.btnCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Image = Nothing
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnCancel.Location = New System.Drawing.Point(207, 129)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnCancel.SideImage = Nothing
        Me.btnCancel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnCancel.Size = New System.Drawing.Size(97, 25)
        Me.btnCancel.TabIndex = 202
        Me.btnCancel.Text = "취   소"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnCancel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems2
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.4639175!
        Me.btnReg.FocalPoints.CenterPtY = 0.16!
        Me.btnReg.FocalPoints.FocusPtX = 0.02061856!
        Me.btnReg.FocalPoints.FocusPtY = 0.16!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker4
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(109, 129)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(97, 25)
        Me.btnReg.TabIndex = 201
        Me.btnReg.Text = "확   인"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblUSDayTime
        '
        Me.lblUSDayTime.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblUSDayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblUSDayTime.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUSDayTime.ForeColor = System.Drawing.Color.White
        Me.lblUSDayTime.Location = New System.Drawing.Point(42, 93)
        Me.lblUSDayTime.Name = "lblUSDayTime"
        Me.lblUSDayTime.Size = New System.Drawing.Size(92, 21)
        Me.lblUSDayTime.TabIndex = 203
        Me.lblUSDayTime.Text = "종료일시"
        Me.lblUSDayTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FGF02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(320, 166)
        Me.Controls.Add(Me.lblUSDayTime)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnReg)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.dtpUEDate)
        Me.Controls.Add(Me.dtpUETime)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGF02"
        Me.Text = "종료일시 설정"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FGF02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpUEDate.Value = CDate(Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") + " 00:00:00")
        dtpUETime.Value = CDate(Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") + " 00:00:00")
        msAction = "NO"
    End Sub

    Private Sub btnReg_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)

        msDate = Format$(dtpUEDate.Value, "yyyy-MM-dd")
        msTIme = Format$(dtpUETime.Value, "HH:mm:ss")
        msAction = "YES"
        Me.Close()

    End Sub

    Private Sub btnCancel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs)

        msAction = "NO"
        Me.Close()

    End Sub

    Public Property UEDate() As String
        Get
            Return msDate.ToString
        End Get
        Set(ByVal Value As String)

        End Set
    End Property

    Public Property UETime() As String
        Get
            Return msTIme.ToString
        End Get
        Set(ByVal Value As String)

        End Set
    End Property

    Public Property LABEL() As String
        Get

        End Get
        Set(ByVal Value As String)
            lblMsg.Text = Value
        End Set
    End Property

    Public Property ACTION() As String
        Get
            Return msAction
        End Get
        Set(ByVal Value As String)

        End Set
    End Property
End Class
