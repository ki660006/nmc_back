'/*****************************************************************************************/
'/*                                                                                       */
'/* Project Name : Laboratory Information System()                                        */
'/*                                                                                       */
'/*                                                                                       */
'/* FileName     : FGB29.vb                                                               */
'/* PartName     : 혈액은행- 진료과별 출고/폐기 현황                                      */
'/* Description  :                                                                        */
'/* Design       : 2006-10-31  유은자                                                     */
'/* Coded        : 2006-10-31  유은자                                                     */
'/* Modified     :                                                                        */
'/*                                                                                       */
'/*                                                                                       */
'/*                                                                                       */
'/*****************************************************************************************/

Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN

Imports LISAPP.APP_BT

Public Class FGB20
    Inherits System.Windows.Forms.Form

    Private mbLoad As Boolean = False

    Friend WithEvents pnlButton As System.Windows.Forms.Panel
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlSearchGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoDept As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDr As System.Windows.Forms.RadioButton
    Friend WithEvents lblSGbn As System.Windows.Forms.Label

    Private Sub sbDisplay_DptWard(ByVal r_dt As DataTable)
        Try
            Dim sDptWard As String = ""
            Dim dt_dept As DataTable = CGDA_BT.fnGet_Dept_List()
            Dim dt_ward As DataTable = CGDA_BT.fnGet_Ward_List()

            With Me.spdData
                .ReDraw = False
                .MaxCols = 4 + r_dt.Rows.Count * 2
                For ix As Integer = 0 To r_dt.Rows.Count - 1

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                    .Col = (ix * 2) + 5 : .ColID = r_dt.Rows(ix).Item("comcd").ToString.Trim : .Text = r_dt.Rows(ix).Item("comnmd").ToString.Trim : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                    .Col = (ix * 2) + 5 : .Text = "출고" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .Col = (ix * 2) + 6 : .Text = "폐기" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    .AddCellSpan((ix * 2) + 5, 0, 2, 1)
                    .AddCellSpan(3, 0, 2, 1)
                Next

                Dim dt As DataTable = CGDA_BT.fnGet_OutAbn_DeptWithBld(Me.dtpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(" ", ""), "")

                If dt.Rows.Count < 0 Then Return

                For ix As Integer = 0 To dt.Rows.Count - 1
                    If sDptWard <> dt.Rows(ix).Item("dptward").ToString Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        If dt.Rows(ix).Item("dptward").ToString.Substring(1) = "--" Then
                            .Col = 1 : .Text = "자체폐기"
                        Else
                            Dim dr As DataRow()
                            '<<<20180124 육하정 선생님 병동도 진료과 로 표기 요청해서 수정함
                            'If dt.Rows(ix).Item("dptward").ToString.StartsWith("I") Then
                            '    dr = dt_ward.Select("wardno = '" + dt.Rows(ix).Item("dptward").ToString.Substring(1) + "'")

                            '    If dr.Length < 1 Then
                            '        .Col = 1 : .Text = dt.Rows(ix).Item("dptward").ToString
                            '    Else
                            '        .Col = 1 : .Text = dr(0).Item("wardnmd").ToString
                            '    End If
                            'Else
                            '    dr = dt_dept.Select("deptcd = '" + dt.Rows(ix).Item("dptward").ToString.Substring(1) + "'")
                            '    If dr.Length < 1 Then
                            '        .Col = 1 : .Text = dt.Rows(ix).Item("dptward").ToString
                            '    Else
                            '        .Col = 1 : .Text = dr(0).Item("deptnmd").ToString
                            '    End If
                            'End If
                            '>>>20180124

                            dr = dt_dept.Select("deptcd = '" + dt.Rows(ix).Item("dptward").ToString.Substring(1) + "'")
                            If dr.Length < 1 Then
                                .Col = 1 : .Text = dt.Rows(ix).Item("dptward").ToString
                            Else
                                .Col = 1 : .Text = dr(0).Item("deptnmd").ToString
                            End If


                        End If
                    End If
                    sDptWard = dt.Rows(ix).Item("dptward").ToString

                    Dim iCol As Integer = .GetColFromID(dt.Rows(ix).Item("comcd").ToString.Trim)

                    If iCol > 0 Then
                        If dt.Rows(ix).Item("gbn").ToString.Trim = "R" Then
                            .Col = iCol + 1 : .Text = dt.Rows(ix).Item("cnt").ToString
                        Else
                            .Col = iCol + 0 : .Text = dt.Rows(ix).Item("cnt").ToString
                        End If
                    End If
                Next

                '-- 가로 합계
                For iRow As Integer = 1 To .MaxRows
                    Dim lgCnt_out As Long = 0
                    Dim lgCnt_abn As Long = 0

                    .Row = iRow
                    For iCol As Integer = 5 To .MaxCols Step 2
                        .Col = iCol + 0 : lgCnt_out += CType(IIf(.Text = "", 0, .Text), Integer)
                        .Col = iCol + 1 : lgCnt_abn += CType(IIf(.Text = "", 0, .Text), Integer)
                    Next
                    .Col = 3 : .Text = lgCnt_out.ToString
                    .Col = 4 : .Text = lgCnt_abn.ToString
                Next

                '-- 세로 합계
                .MaxRows += 1
                .Row = .MaxRows : .Col = 0 : .Text = "합  계" : .Col = 1 : .Text = "합  계"
                For iCol As Integer = 3 To .MaxCols
                    Dim lgCnt As Long = 0

                    .Col = iCol
                    For iRow As Integer = 1 To .MaxRows - 1
                        .Row = iRow : lgCnt += CType(IIf(.Text = "", 0, .Text), Integer)
                    Next
                    .Row = .MaxRows : .Col = iCol : .Text = lgCnt.ToString
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdData.ReDraw = True
        End Try

    End Sub

    Private Sub sbDisplay_Dr(ByVal r_dt As DataTable)
        Try
            Dim dt_dept As DataTable = CGDA_BT.fnGet_Dept_List()
            Dim dt_ward As DataTable = CGDA_BT.fnGet_Ward_List()

            Dim sDptWard As String = ""
            Dim sDrCd As String = ""


            With Me.spdData
                .ReDraw = False
                .MaxCols = 4 + r_dt.Rows.Count * 2
                For ix As Integer = 0 To r_dt.Rows.Count - 1

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                    .Col = (ix * 2) + 5 : .ColID = r_dt.Rows(ix).Item("comcd").ToString.Trim : .Text = r_dt.Rows(ix).Item("comnmd").ToString.Trim : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter

                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                    .Col = (ix * 2) + 5 : .Text = "출고" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .Col = (ix * 2) + 6 : .Text = "폐기" : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter


                    .AddCellSpan((ix * 2) + 5, 0, 2, 1)
                    .AddCellSpan(3, 0, 2, 1)
                    .AddCellSpan(1, 0, 2, 2)
                Next

                Dim dt As DataTable = CGDA_BT.fnGet_OutAbn_DeptDrWithBld(Me.dtpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(" ", ""), "")

                If dt.Rows.Count < 0 Then Return

                Dim iStartDept As Integer = 0

                For ix As Integer = 0 To dt.Rows.Count - 1

                    If sDrCd <> dt.Rows(ix).Item("dptward").ToString + dt.Rows(ix).Item("drcd").ToString Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = 2 : .Text = dt.Rows(ix).Item("drnm").ToString
                    End If
                    sDrCd = dt.Rows(ix).Item("dptward").ToString + dt.Rows(ix).Item("drcd").ToString

                    If sDptWard <> dt.Rows(ix).Item("dptward").ToString Then
                        If dt.Rows(ix).Item("dptward").ToString.Substring(1) = "--" Then
                            .Col = 1 : .Text = "자체폐기"
                        Else
                            Dim dr As DataRow()

                            '<<<20180124 진료과로 표기 요청 
                            'If dt.Rows(ix).Item("dptward").ToString.StartsWith("I") Then
                            '    dr = dt_ward.Select("wardno = '" + dt.Rows(ix).Item("dptward").ToString.Substring(1) + "'")
                            '    If dr.Length < 1 Then
                            '        .Col = 1 : .Text = dt.Rows(ix).Item("dptward").ToString.Substring(1)
                            '    Else
                            '        .Col = 1 : .Text = dr(0).Item("wardnmd").ToString
                            '    End If
                            'Else
                            '    dr = dt_dept.Select("deptcd = '" + dt.Rows(ix).Item("dptward").ToString.Substring(1) + "'")
                            '    If dr.Length < 1 Then
                            '        .Col = 1 : .Text = dt.Rows(ix).Item("dptward").ToString.Substring(1)
                            '    Else
                            '        .Col = 1 : .Text = dr(0).Item("deptnmd").ToString
                            '    End If
                            'End If
                            '>>>

                            dr = dt_dept.Select("deptcd = '" + dt.Rows(ix).Item("dptward").ToString.Substring(1) + "'")
                            If dr.Length < 1 Then
                                .Col = 1 : .Text = dt.Rows(ix).Item("dptward").ToString.Substring(1)
                            Else
                                .Col = 1 : .Text = dr(0).Item("deptnmd").ToString
                            End If

                        End If

                        'If iStartDept > 0 Then .AddCellSpan(1, 1, iStartDept, .MaxRows - iStartDept - 1) '<20141021 스프레드 오류로 막음
                        iStartDept = .MaxRows
                    End If
                    sDptWard = dt.Rows(ix).Item("dptward").ToString


                    Dim iCol As Integer = .GetColFromID(dt.Rows(ix).Item("comcd").ToString.Trim)

                    If iCol > 0 Then
                        If dt.Rows(ix).Item("gbn").ToString.Trim = "R" Then
                            .Col = iCol + 1 : .Text = dt.Rows(ix).Item("cnt").ToString
                        Else
                            .Col = iCol + 0 : .Text = dt.Rows(ix).Item("cnt").ToString
                        End If
                    End If
                Next

                '.AddCellSpan(1, 1, iStartDept, .MaxRows - iStartDept - 1)

                '-- 가로 합계
                For iRow As Integer = 1 To .MaxRows
                    Dim lgCnt_out As Long = 0
                    Dim lgCnt_abn As Long = 0

                    .Row = iRow
                    For iCol As Integer = 5 To .MaxCols Step 2
                        .Col = iCol + 0 : lgCnt_out += CType(IIf(.Text = "", 0, .Text), Integer)
                        .Col = iCol + 1 : lgCnt_abn += CType(IIf(.Text = "", 0, .Text), Integer)
                    Next
                    .Col = 3 : .Text = lgCnt_out.ToString
                    .Col = 4 : .Text = lgCnt_abn.ToString
                Next

                '-- 세로 합계
                .MaxRows += 1
                .Row = .MaxRows : .Col = 0 : .Text = "합  계" : .Col = 1 : .Text = "합  계"
                For iCol As Integer = 3 To .MaxCols
                    Dim lgCnt As Long = 0

                    .Col = iCol
                    For iRow As Integer = 1 To .MaxRows - 1
                        .Row = iRow : lgCnt += CType(IIf(.Text = "", 0, .Text), Integer)
                    Next
                    .Row = .MaxRows : .Col = iCol : .Text = lgCnt.ToString
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdData.ReDraw = True
        End Try

    End Sub

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
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents spdData As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB20))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.spdData = New AxFPSpreadADO.AxfpSpread
        Me.pnlButton = New System.Windows.Forms.Panel
        Me.btnExcel = New CButtonLib.CButton
        Me.btnQuery = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.pnlSearchGbn = New System.Windows.Forms.Panel
        Me.rdoDept = New System.Windows.Forms.RadioButton
        Me.rdoDr = New System.Windows.Forms.RadioButton
        Me.lblSGbn = New System.Windows.Forms.Label
        Me.Panel3.SuspendLayout()
        CType(Me.spdData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlButton.SuspendLayout()
        Me.pnlSearchGbn.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.spdData)
        Me.Panel3.Location = New System.Drawing.Point(5, 39)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1005, 559)
        Me.Panel3.TabIndex = 169
        '
        'spdData
        '
        Me.spdData.DataSource = Nothing
        Me.spdData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdData.Location = New System.Drawing.Point(0, 0)
        Me.spdData.Name = "spdData"
        Me.spdData.OcxState = CType(resources.GetObject("spdData.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdData.Size = New System.Drawing.Size(1001, 555)
        Me.spdData.TabIndex = 0
        '
        'pnlButton
        '
        Me.pnlButton.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlButton.Controls.Add(Me.btnExcel)
        Me.pnlButton.Controls.Add(Me.btnQuery)
        Me.pnlButton.Controls.Add(Me.btnClear)
        Me.pnlButton.Controls.Add(Me.btnExit)
        Me.pnlButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlButton.Location = New System.Drawing.Point(0, 603)
        Me.pnlButton.Name = "pnlButton"
        Me.pnlButton.Size = New System.Drawing.Size(1016, 34)
        Me.pnlButton.TabIndex = 174
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems1
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker2
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(609, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(96, 25)
        Me.btnExcel.TabIndex = 188
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems2
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.5!
        Me.btnQuery.FocalPoints.CenterPtY = 0.0!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker4
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(706, 4)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(100, 25)
        Me.btnQuery.TabIndex = 187
        Me.btnQuery.Text = "조회(F6)"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0.0!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(807, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 186
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
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
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(908, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(97, 25)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(2, 28)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1013, 9)
        Me.Label2.TabIndex = 215
        Me.Label2.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(204, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 12)
        Me.Label5.TabIndex = 219
        Me.Label5.Text = "~"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(9, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 21)
        Me.Label4.TabIndex = 218
        Me.Label4.Text = "조회일자"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateS
        '
        Me.dtpDateS.CalendarFont = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateS.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateS.Location = New System.Drawing.Point(90, 6)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(110, 21)
        Me.dtpDateS.TabIndex = 216
        '
        'dtpDateE
        '
        Me.dtpDateE.CalendarFont = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateE.Location = New System.Drawing.Point(222, 7)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(110, 21)
        Me.dtpDateE.TabIndex = 217
        '
        'pnlSearchGbn
        '
        Me.pnlSearchGbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchGbn.Controls.Add(Me.rdoDept)
        Me.pnlSearchGbn.Controls.Add(Me.rdoDr)
        Me.pnlSearchGbn.ForeColor = System.Drawing.Color.DarkGreen
        Me.pnlSearchGbn.Location = New System.Drawing.Point(435, 7)
        Me.pnlSearchGbn.Name = "pnlSearchGbn"
        Me.pnlSearchGbn.Size = New System.Drawing.Size(175, 21)
        Me.pnlSearchGbn.TabIndex = 247
        '
        'rdoDept
        '
        Me.rdoDept.Checked = True
        Me.rdoDept.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoDept.Location = New System.Drawing.Point(3, 2)
        Me.rdoDept.Name = "rdoDept"
        Me.rdoDept.Size = New System.Drawing.Size(74, 18)
        Me.rdoDept.TabIndex = 5
        Me.rdoDept.TabStop = True
        Me.rdoDept.Tag = "1"
        Me.rdoDept.Text = "진료과별"
        Me.rdoDept.UseCompatibleTextRendering = True
        '
        'rdoDr
        '
        Me.rdoDr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDr.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoDr.Location = New System.Drawing.Point(83, 2)
        Me.rdoDr.Name = "rdoDr"
        Me.rdoDr.Size = New System.Drawing.Size(83, 18)
        Me.rdoDr.TabIndex = 6
        Me.rdoDr.Tag = "1"
        Me.rdoDr.Text = "의뢰의사별"
        Me.rdoDr.UseCompatibleTextRendering = True
        '
        'lblSGbn
        '
        Me.lblSGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSGbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSGbn.ForeColor = System.Drawing.Color.White
        Me.lblSGbn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSGbn.Location = New System.Drawing.Point(354, 7)
        Me.lblSGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSGbn.Name = "lblSGbn"
        Me.lblSGbn.Size = New System.Drawing.Size(80, 21)
        Me.lblSGbn.TabIndex = 246
        Me.lblSGbn.Text = "조회구분"
        Me.lblSGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGB20
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 637)
        Me.Controls.Add(Me.pnlSearchGbn)
        Me.Controls.Add(Me.lblSGbn)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpDateS)
        Me.Controls.Add(Me.dtpDateE)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlButton)
        Me.Controls.Add(Me.Panel3)
        Me.Name = "FGB20"
        Me.Text = "진료과별 출고 및 폐기 현황 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlButton.ResumeLayout(False)
        Me.pnlSearchGbn.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Class COM_MST
        Public sName As String
        Public sCode As String
        Public iCnt_O As Int32
        Public iCnt_R As Int32
        Public Sub New()
            MyBase.new()
        End Sub
    End Class

    Private Class DPTWARD_MST
        Public sDptWard As String = ""
        Public alList As New ArrayList

        Public Sub New()
            MyBase.new()
        End Sub

    End Class

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Me.Close()

    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        sbClear_Form()

        Try
            ' column - 성분제제
            Dim dt As DataTable = CGDA_BT.fnGet_OutAbn_Com(Me.dtpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(" ", ""))

            If dt.Rows.Count < 1 Then
                MsgBox("조회된 데이터가 없습니다. 조건을 확인하세요", MsgBoxStyle.Information, Me.Text)
                Return
            End If

            If Me.rdoDept.Checked Then
                sbDisplay_DptWard(dt)
            Else
                sbDisplay_Dr(dt)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub


    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbClear_Form()
    End Sub

    Private Sub sbClear_Form()
        With Me.spdData
            .MaxRows = 0
            .AddCellSpan(1, 1, 1, 2)
            .AddCellSpan(2, 2, 1, 2)
        End With
    End Sub

    ' 혈액 반납/폐기 건수 조회 Excel 출력하기!!
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        With Me.spdData
            .ReDraw = False

            .MaxRows += 4

            .InsertRows(1, 2)

            .Col = 4
            .Row = 1
            .Text = "혈액 출고/폐기 진료과별 현황"
            .FontBold = True
            .FontSize = 15
            .ForeColor = System.Drawing.Color.Red

            Dim sColHeaders As String = ""

            .InsertRows(3, 2)

            .Col = 1 : .Col2 = .MaxCols
            .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0 : .Row2 = FPSpreadADO.CoordConstants.SpreadHeader + 1
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 3 : .Row2 = 4
            .Clip = sColHeaders

            If Me.spdData.ExportToExcel("c:\혈액출고폐기진료과별현황" + ".xls", "출고폐기", "") Then
                Process.Start("c:\혈액출고폐기진료과별현황" + ".xls")
            End If

            .DeleteRows(1, 4)

            .MaxRows -= 4

            .ReDraw = True
        End With
    End Sub

    Private Sub FGB20_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        If mbLoad = False Then mbLoad = True

    End Sub

    Private Sub FGB20_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB20_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB20_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        'Me.dtpDateS.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
        sbClear_Form()

        Me.dtpDateS.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
        Me.dtpDateE.Value = CDate(Format(Me.dtpDateS.Value, "yyyy-MM-dd").ToString + " 23:59:59")

    End Sub

    Private Sub rdoDept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDept.CheckedChanged, rdoDr.CheckedChanged
        If mbLoad = False Then Return

        sbClear_Form()
        With Me.spdData
            If Me.rdoDept.Checked Then
                .Col = .GetColFromID("drcd") : .ColHidden = True
            Else
                .Col = .GetColFromID("drcd") : .ColHidden = False
            End If
        End With
    End Sub
End Class
