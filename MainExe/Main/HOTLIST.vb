Imports COMMON.CommFN
Imports common.commlogin.login

Public Class HOTLIST
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : HOTLIST.vb, Class : HOTLIST" + vbTab

    Private mobjDAF As New LISAPP.APP_F_USR_HOT

    Friend WithEvents spdMenu As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnSave As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents spdHotList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents label56 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents imgMenu As System.Windows.Forms.ImageList
    Friend WithEvents spdIcon As AxFPSpreadADO.AxfpSpread
    Friend WithEvents picTmp As System.Windows.Forms.PictureBox
    Friend WithEvents btnDown As System.Windows.Forms.Button

    Private Function fnCollectItemTable_95(ByVal rsRegDt As String) As LISAPP.ItemTableCollection
        Dim sFn As String = "Private Function fnCollectItemTable_95(ByVal asRegDT As String) As DA01.ItemTableCollection"

        Try
            Dim it95 As New LISAPP.ItemTableCollection
            Dim iCol As Integer = 0

            For ix As Integer = 1 To spdHotList.MaxRows
                Dim sMnuId As String = "", sIconGbn As String = ""
                With spdHotList
                    .Row = ix
                    .Col = .GetColFromID("mnuid") : sMnuId = .Text
                    .Col = .GetColFromID("icongbn") : sIconGbn = .Text
                End With

                With it95
                    .SetItemTable("USRID", 1, ix, USER_INFO.USRID)
                    .SetItemTable("MNUID", 2, ix, sMnuId)
                    .SetItemTable("DISPSEQ", 3, ix, (ix + 1).ToString)
                    .SetItemTable("ICONGBN", 4, ix, sIconGbn)
                    .SetItemTable("REGDT", 5, ix, rsRegDt)
                    .SetItemTable("REGID", 6, ix, USER_INFO.USRID)
                End With
            Next

            Return it95
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function fnGetSystemDT() As String
        Dim sFn As String = "Private Function fnGetSystemDT() As String"

        Try
            Dim DTable As DataTable

            DTable = mobjDAF.GetNewRegDT

            If DTable.Rows.Count > 0 Then
                fnGetSystemDT = DTable.Rows(0).Item(0).ToString
            Else
                MsgBox("시스템의 날짜를 초기화하지 못했습니다. 관리자에게 문의하시기 바랍니다!!", MsgBoxStyle.Information)
                fnGetSystemDT = Format(Now, "yyyy-MM-dd HH:mm:ss")

                Exit Function
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            fnGetSystemDT = Format(Now, "yyyy-MM-dd HH:mm:ss")
        End Try
    End Function

    Public Function fnReg() As Boolean
        Dim sFn As String = "Public Function fnRegSpc() As Boolean"

        Try
            Dim it95 As New LISAPP.ItemTableCollection
            Dim sRegDt As String = fnGetSystemDT()

            If spdHotList.MaxRows > 10 Then
                MsgBox("즐겨찾기 메뉴를 10개 이하로 설정하세요.!!")
                Return False
            End If

            it95 = fnCollectItemTable_95(sRegDt)

            If mobjDAF.TransUsrInfo(it95, USER_INFO.USRID) Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return False
        End Try
    End Function

    Private Sub sbDisplayCdDetail_Mnu(ByVal rsUsrId As String)
        Dim sFn As String = ""

        Try
            Dim dt As DataTable
            Dim iCol As Integer = 0, iParent As Integer = 0

            dt = mobjDAF.fnGet_UsrMenuInfo(rsUsrId)

            If dt.Rows.Count < 1 Then Return

            With spdMenu
                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For ix1 As Integer = 0 To dt.Rows.Count - 1
                    For ix2 As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(ix2).ColumnName.ToLower)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = ix1 + 1
                            .Text = Replace(dt.Rows(ix1).Item(ix2).ToString, "-", " ")
                        End If

                        '색상 처리
                        If dt.Columns(ix2).ColumnName.ToLower = "mnulvl" Then
                            iParent = CType(IIf(dt.Rows(ix1).Item(ix2).ToString = "0", 1, 0), Integer)
                        End If
                    Next

                    If iParent = 1 Then
                        .Col = .GetColFromID("mnunm") : .Row = ix1 + 1 : .BackColor = Drawing.Color.LavenderBlush
                        .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = ""
                    Else
                        .Col = .GetColFromID("mnunm") : .Row = ix1 + 1 : .BackColor = Drawing.Color.White
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayCdDetail_HotList(ByVal rsUsrId As String)
        Dim sFn As String = ""

        Try
            Dim DTable As DataTable
            Dim iCol As Integer = 0, iParent As Integer = 0

            DTable = mobjDAF.fnGet_UsrHotListInfo(rsUsrId)

            '스프레드 초기화
            'sbInitialize_spdSkill()

            If DTable.Rows.Count > 0 Then
                With spdHotList
                    .ReDraw = False

                    .MaxRows = DTable.Rows.Count

                    For i As Integer = 0 To DTable.Rows.Count - 1
                        For j As Integer = 0 To DTable.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(DTable.Columns(j).ColumnName.ToLower)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = DTable.Rows(i).Item(j).ToString

                                If iCol = .GetColFromID("icongbn") Then
                                    If Val(DTable.Rows(i).Item(j).ToString) > 0 Then
                                        Me.picTmp.Image = imgMenu.Images(CInt(DTable.Rows(i).Item(j).ToString) - 1)

                                        .Row = i + 1
                                        .Col = .GetColFromID("icon_pic") : .TypePictPicture = Me.picTmp.Image
                                    End If

                                End If
                            End If
                        Next

                        If iParent = 1 Then
                            .Col = .GetColFromID("mnunm") : .Row = i + 1 : .BackColor = Drawing.Color.LavenderBlush
                        Else
                            .Col = .GetColFromID("mnunm") : .Row = i + 1 : .BackColor = Drawing.Color.White
                        End If
                    Next

                    .ReDraw = True
                End With
            Else
                Return
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            spdMenu.MaxRows = 0
            spdHotList.MaxRows = 0

            With spdHotList
                .MaxRows = 0
                .Col = .GetColFromID("mnuid") : .ColHidden = True
                .Col = .GetColFromID("icongbn") : .ColHidden = True
            End With

            sbDisplayCdDetail_Mnu(USER_INFO.USRID)
            sbDisplayCdDetail_HotList(USER_INFO.USRID)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        'sbInitialize()
    End Sub

    Public Sub New(ByVal ro_imgList As System.Windows.Forms.ImageList)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        imgMenu = ro_imgList

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        'sbInitialize()
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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HOTLIST))
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.spdMenu = New AxFPSpreadADO.AxfpSpread
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnSave = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnDel = New System.Windows.Forms.Button
        Me.spdHotList = New AxFPSpreadADO.AxfpSpread
        Me.label56 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnUp = New System.Windows.Forms.Button
        Me.btnDown = New System.Windows.Forms.Button
        Me.imgMenu = New System.Windows.Forms.ImageList(Me.components)
        Me.spdIcon = New AxFPSpreadADO.AxfpSpread
        Me.picTmp = New System.Windows.Forms.PictureBox
        CType(Me.spdMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.spdHotList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTmp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spdMenu
        '
        Me.spdMenu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdMenu.DataSource = Nothing
        Me.spdMenu.Location = New System.Drawing.Point(12, 37)
        Me.spdMenu.Margin = New System.Windows.Forms.Padding(1)
        Me.spdMenu.Name = "spdMenu"
        Me.spdMenu.OcxState = CType(resources.GetObject("spdMenu.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdMenu.Size = New System.Drawing.Size(274, 365)
        Me.spdMenu.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 461)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(777, 30)
        Me.Panel1.TabIndex = 7
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSave.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnSave.ColorFillBlend = CBlendItems3
        Me.btnSave.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSave.Corners.All = CType(6, Short)
        Me.btnSave.Corners.LowerLeft = CType(6, Short)
        Me.btnSave.Corners.LowerRight = CType(6, Short)
        Me.btnSave.Corners.UpperLeft = CType(6, Short)
        Me.btnSave.Corners.UpperRight = CType(6, Short)
        Me.btnSave.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSave.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSave.FocalPoints.CenterPtX = 0.5!
        Me.btnSave.FocalPoints.CenterPtY = 0.0!
        Me.btnSave.FocalPoints.FocusPtX = 0.0!
        Me.btnSave.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSave.FocusPtTracker = DesignerRectTracker6
        Me.btnSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Image = Nothing
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.ImageIndex = 0
        Me.btnSave.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSave.Location = New System.Drawing.Point(588, 2)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSave.SideImage = Nothing
        Me.btnSave.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSave.Size = New System.Drawing.Size(86, 25)
        Me.btnSave.TabIndex = 209
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSave.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems1
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker2
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(675, 3)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(86, 25)
        Me.btnExit.TabIndex = 208
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(307, 113)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(78, 26)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = ">>"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(307, 145)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(78, 26)
        Me.btnDel.TabIndex = 9
        Me.btnDel.Text = "<<"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'spdHotList
        '
        Me.spdHotList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdHotList.DataSource = Nothing
        Me.spdHotList.Location = New System.Drawing.Point(405, 37)
        Me.spdHotList.Margin = New System.Windows.Forms.Padding(1)
        Me.spdHotList.Name = "spdHotList"
        Me.spdHotList.OcxState = CType(resources.GetObject("spdHotList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdHotList.Size = New System.Drawing.Size(355, 365)
        Me.spdHotList.TabIndex = 10
        '
        'label56
        '
        Me.label56.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.label56.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.label56.ForeColor = System.Drawing.Color.Black
        Me.label56.Location = New System.Drawing.Point(12, 14)
        Me.label56.Margin = New System.Windows.Forms.Padding(1)
        Me.label56.Name = "label56"
        Me.label56.Size = New System.Drawing.Size(274, 21)
        Me.label56.TabIndex = 24
        Me.label56.Text = " 사용자 메뉴"
        Me.label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(405, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(295, 21)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = " 즐겨찾기 메뉴"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(704, 15)
        Me.btnUp.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(28, 21)
        Me.btnUp.TabIndex = 32
        Me.btnUp.Text = "▲"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(732, 15)
        Me.btnDown.Margin = New System.Windows.Forms.Padding(1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(28, 21)
        Me.btnDown.TabIndex = 33
        Me.btnDown.Text = "▼"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'imgMenu
        '
        Me.imgMenu.ImageStream = CType(resources.GetObject("imgMenu.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMenu.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMenu.Images.SetKeyName(0, "검사실인증.gif")
        Me.imgMenu.Images.SetKeyName(1, "검체.gif")
        Me.imgMenu.Images.SetKeyName(2, "결과.gif")
        Me.imgMenu.Images.SetKeyName(3, "마스터.gif")
        Me.imgMenu.Images.SetKeyName(4, "물품.gif")
        Me.imgMenu.Images.SetKeyName(5, "조회 복사.gif")
        Me.imgMenu.Images.SetKeyName(6, "채혈2.gif")
        Me.imgMenu.Images.SetKeyName(7, "혈액은행.gif")
        Me.imgMenu.Images.SetKeyName(8, "icon0.jpg")
        Me.imgMenu.Images.SetKeyName(9, "icon1.jpg")
        Me.imgMenu.Images.SetKeyName(10, "icon2.jpg")
        Me.imgMenu.Images.SetKeyName(11, "icon3.jpg")
        Me.imgMenu.Images.SetKeyName(12, "icon4.jpg")
        Me.imgMenu.Images.SetKeyName(13, "icon5.jpg")
        Me.imgMenu.Images.SetKeyName(14, "icon6.jpg")
        '
        'spdIcon
        '
        Me.spdIcon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdIcon.DataSource = Nothing
        Me.spdIcon.Location = New System.Drawing.Point(0, 416)
        Me.spdIcon.Name = "spdIcon"
        Me.spdIcon.OcxState = CType(resources.GetObject("spdIcon.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdIcon.Size = New System.Drawing.Size(778, 47)
        Me.spdIcon.TabIndex = 34
        '
        'picTmp
        '
        Me.picTmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picTmp.Location = New System.Drawing.Point(307, 375)
        Me.picTmp.Name = "picTmp"
        Me.picTmp.Size = New System.Drawing.Size(28, 27)
        Me.picTmp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTmp.TabIndex = 36
        Me.picTmp.TabStop = False
        Me.picTmp.Visible = False
        '
        'HOTLIST
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(777, 491)
        Me.Controls.Add(Me.picTmp)
        Me.Controls.Add(Me.spdIcon)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.label56)
        Me.Controls.Add(Me.spdHotList)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.spdMenu)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "HOTLIST"
        Me.Text = "즐겨찾기 편집"
        CType(Me.spdMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdHotList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdIcon, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTmp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FDF00_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                btnSave_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If fnReg() Then Me.Close()

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        With spdMenu
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("mnuid") : Dim sMnuId As String = .Text
                .Col = .GetColFromID("mnunm") : Dim sMnuNm As String = .Text

                If sChk = "1" Then

                    Dim bFind As Boolean = False

                    For ix2 As Integer = 1 To spdHotList.MaxRows
                        spdHotList.RowsFrozen = ix2
                        spdHotList.Col = .GetColFromID("mnuid") : Dim sFind As String = spdHotList.Text

                        If sFind = sMnuId Then
                            bFind = True
                            Exit For
                        End If
                    Next

                    If bFind = False Then
                        spdHotList.MaxRows += 1
                        spdHotList.Row = spdHotList.MaxRows
                        spdHotList.Col = .GetColFromID("mnuid") : spdHotList.Text = sMnuId.Trim
                        spdHotList.Col = .GetColFromID("mnunm") : spdHotList.Text = sMnuNm.Trim
                    End If
                End If
            Next

            .Col = .GetColFromID("chk") : .Col2 = .GetColFromID("chk")
            .Row = 1 : .Row2 = .MaxRows
            .BlockMode = True
            .Action = FPSpreadADO.ActionConstants.ActionClearText
            .BlockMode = False
        End With

    End Sub

    Private Sub btnDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDel.Click

        With spdHotList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text

                If sChk = "1" Then
                    .Row = ix
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1

                    ix -= 1
                End If

                If ix < 0 Then Exit For
            Next
        End With

    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click

        With spdHotList
            Dim iRow As Integer = .ActiveRow

            If iRow < 2 Then Return

            .Row = iRow
            .Col = .GetColFromID("mnuid") : Dim sMnuId As String = .Text
            .Col = .GetColFromID("mnunm") : Dim sMnunm As String = .Text
            .Col = .GetColFromID("icon_pic") : Me.picTmp.Image = .TypePictPicture
            .Col = .GetColFromID("icongbn") : Dim sIconGbn As String = .Text

            .Row = iRow - 1
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("mnuid") : .Text = sMnuId
            .Col = .GetColFromID("mnunm") : .Text = sMnunm
            .Col = .GetColFromID("icon_pic") : .TypePictPicture = Me.picTmp.Image
            .Col = .GetColFromID("icongbn") : .Text = sIconGbn

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow - 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

        With spdHotList
            Dim iRow As Integer = .ActiveRow

            If iRow = .MaxRows Then Return

            .Row = iRow
            .Col = .GetColFromID("mnuid") : Dim sMnuId As String = .Text
            .Col = .GetColFromID("mnunm") : Dim sMnunm As String = .Text
            .Col = .GetColFromID("icon_pic") : Me.picTmp.Image = .TypePictPicture
            .Col = .GetColFromID("icongbn") : Dim sIconGbn As String = .Text

            .Row = iRow + 2
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("mnuid") : .Text = sMnuId
            .Col = .GetColFromID("mnunm") : .Text = sMnunm
            .Col = .GetColFromID("icon_pic") : .TypePictPicture = Me.picTmp.Image
            .Col = .GetColFromID("icongbn") : .Text = sIconGbn

            .Row = iRow
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub HOTLIST_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
        sbInitialize()

        With spdIcon
            .MaxCols = 0
            .MaxCols = imgMenu.Images.Count
            For ix As Integer = 0 To imgMenu.Images.Count - 1
                Me.picTmp.Image = imgMenu.Images(ix)
                .Row = 1 : .Col = ix + 1 : .TypePictPicture = Me.picTmp.Image
            Next
        End With
    End Sub

    Private Sub spdIcon_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdIcon.DblClick

        If e.col < 1 Or e.row < 1 Then Return

        With spdIcon
            .Row = 1
            .Col = e.col : Me.picTmp.Image = .TypePictPicture
        End With

        With spdHotList
            .Row = .ActiveRow
            .Col = .GetColFromID("icon_pic") : .TypePictPicture = Me.picTmp.Image
            .Col = .GetColFromID("icongbn") : .Text = e.col.ToString
        End With

    End Sub
End Class
