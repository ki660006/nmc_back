'>>> 결과값 조회

Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommConst
Imports COMMON.CommLogin.LOGIN

Imports LISAPP.APP_S.RstSrh

Public Class FGS09
    Inherits System.Windows.Forms.Form

    Private Const msXmlDir As String = "\XML"
    Private msFile_CalForm As String = Windows.Forms.Application.StartupPath & msXmlDir & "\FGS09_CALFORM.XML"

    Private mbLoaded As Boolean = False
    Private mbMicroBioYn As Boolean = False

    Friend WithEvents chkCtTest As System.Windows.Forms.CheckBox
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnCalForm As System.Windows.Forms.Button
    Friend WithEvents txtCalForm As System.Windows.Forms.TextBox
    Friend WithEvents chkH As System.Windows.Forms.CheckBox
    Friend WithEvents chkL As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSpc As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoViewRst As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOrgRst As System.Windows.Forms.RadioButton
    Friend WithEvents ChkMicro As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear_calc As System.Windows.Forms.Button

    Public Overridable Sub DisplayInit()

        Try
            DisplayInit_dtpWkDay()
            DisplayInit_spdList()
            Display_Last()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Public Overridable Sub DisplayInit_dtpWkDay()

        Try
            Dim dtNow As Date = New LISAPP.APP_DB.ServerDateTime().GetDateTime
            Me.dtpWkDayE.Value = dtNow
            Me.dtpWkDayS.Value = dtNow

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Public Overridable Sub DisplayInit_spdList()

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            With spd
                .Col = .GetColFromID("workno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .Col = .GetColFromID("bcno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("regno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("patnm")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("sexage")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("doctornm")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("deptinfo")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("tkdt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("spcnmd")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .MaxRows = 0
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Public Overridable Sub Display_Clear()

        Try
            DisplayInit_spdList()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Public Overridable Sub Display_Last()

        Try
            'Last Test
            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXmlDir, msFile_CalForm, "CALCFORM")

            sTmp = sTmp.Replace("&gt;", ">").Replace("&lt;", "<")
            If sTmp <> "" Then
                Me.txtCalForm.Text = sTmp.Split("|"c)(1)
                Me.txtCalForm.Tag = sTmp.Split("|"c)(0)
            Else
                Me.txtCalForm.Text = ""
                Me.txtCalForm.Tag = ""
            End If

        Catch ex As Exception
           CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Public Overridable Sub Display_List()

        Try

            DisplayInit_spdList()
            sbDisplay_List_Test()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_List_view(ByVal r_dt As DataTable)

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With spd
                .MaxRows = 0

                .ReDraw = False

                'Data 표시
                Dim iRow As Integer = 0

                '일단 MaxRows 확보
                .MaxRows = r_dt.Rows.Count

                For ix As Integer = 1 To r_dt.Rows.Count
                    iRow = ix

                    For j As Integer = 1 To r_dt.Rows(ix - 1).Table.Columns.Count
                        Dim iCol As Integer = .GetColFromID(r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower)

                        If iCol > 0 Then
                            .SetText(iCol, iRow, r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim)


                            If r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower = "judgmark" Then
                                If r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim = "L" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .ForeColor = Color.Blue
                                ElseIf r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim = "H" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .ForeColor = Color.Red
                                End If

                            ElseIf r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower = "panicmark" Then
                                If r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim = "P" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkP.BackColor
                                    .ForeColor = Me.chkP.ForeColor
                                End If

                            ElseIf r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower = "deltamark" Then
                                If r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim = "D" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkD.BackColor
                                    .ForeColor = Me.chkD.ForeColor
                                End If

                            ElseIf r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower = "criticalmark" Then
                                If r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim = "C" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkC.BackColor
                                    .ForeColor = Me.chkC.ForeColor
                                End If

                            ElseIf r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower = "alertmark" Then
                                If r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim = "A" Then
                                    .Col = iCol
                                    .Row = iRow
                                    .BackColor = Me.chkA.BackColor
                                    .ForeColor = Me.chkA.ForeColor
                                End If
                            ElseIf r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower = "doctornm" Then
                                .Row = iRow
                                .Col = iCol : .Text = OCSAPP.OcsLink.Ord.fnGet_GenDr_Name(r_dt.Rows(ix - 1).Item("bcno").ToString.Replace("-", ""), r_dt.Rows(ix - 1).Item("regno").ToString)
                                If .Text.Replace("/", "") = "" Then .Text = r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim

                            ElseIf r_dt.Rows(ix - 1).Table.Columns(j - 1).ColumnName.ToLower = "rstflag" Then
                                .Col = iCol
                                .Row = iRow

                                Select Case r_dt.Rows(ix - 1).Item(j - 1).ToString().Trim
                                    Case "3"
                                        .Text = FixedVariable.gsRstFlagF
                                        .ForeColor = FixedVariable.g_color_FN
                                    Case "2"
                                        .Text = FixedVariable.gsRstFlagM
                                    Case "1"
                                        .Text = FixedVariable.gsRstFlagR
                                End Select

                            End If
                        End If
                    Next
                Next

                .set_ColWidth(.GetColFromID("workno"), .get_MaxTextColWidth(.GetColFromID("workno")))
                .set_ColWidth(.GetColFromID("bcno"), .get_MaxTextColWidth(.GetColFromID("bcno")) + 1) '2010-11-24 레포트 출력시 짤리는 현상때문에 +1해줌 by LJM
                .set_ColWidth(.GetColFromID("bfbcno"), .get_MaxTextColWidth(.GetColFromID("bfbcno")))
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub


    Public Overridable Sub sbDisplay_List_Test()

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            Dim sOpt As String = ""
            Dim sRefL As String = "", sRefH As String = ""
            Dim sPanic As String = "", sDelta As String = "", sCritical As String = "", sAlert As String = ""
            Dim sTests As String = ""

            If rdoOr.Checked Then sOpt = "or"
            If chkL.Checked Then sRefL = "L"
            If chkH.Checked Then sRefH = "H"
            If chkP.Checked Then sPanic = "P"
            If chkD.Checked Then sDelta = "D"
            If chkC.Checked Then sCritical = "C"
            If chkA.Checked Then sAlert = "A"
            '--------20171030 전재휘 추가
            If ChkMicro.Checked Then
                mbMicroBioYn = True
            Else
                mbMicroBioYn = False
            End If

            '-----------------


            If Me.txtCalForm.Text = "" Then Me.txtCalForm.Tag = ""

            '검사계의 전체 내용을 가져와 Filtering
            Dim dt As New DataTable

            If Me.chkCtTest.Checked Then
                dt = fnGet_Search_Rstval_SP(Me.dtpWkDayS.Text.Replace("-", ""), Me.dtpWkDayE.Text.Replace("-", ""), Me.chkFN.Checked, Me.txtCalForm.Tag.ToString, mbMicroBioYn, Me.chkSpc.Checked)
            Else
                dt = fnGet_Search_Rstval(Me.dtpWkDayS.Text.Replace("-", ""), Me.dtpWkDayE.Text.Replace("-", ""), Me.chkFN.Checked, Me.txtCalForm.Tag.ToString, _
                                         IIf(Me.rdoOrgRst.Checked, "O", "V").ToString, sOpt, sRefL, sRefH, sPanic, sDelta, sCritical, sAlert, mbMicroBioYn, Me.chkSpc.Checked)
            End If

            sbDisplay_List_view(dt)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Public Overridable Function Find_Abnormal_Flag() As String

        Try
            Dim sReturn As String = ""

            Dim sAndOr As String = ""

            If Me.rdoOr.Checked Then
                sAndOr = " or "
            Else
                sAndOr = " and "
            End If

            If Me.chkP.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "panicmark = 'P'"
            End If

            If Me.chkD.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "deltamark = 'D'"
            End If

            If Me.chkC.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "criticalmark = 'C'"
            End If

            If Me.chkA.Checked Then
                If sReturn.Length > 0 Then sReturn += sAndOr

                sReturn += "alertmark = 'A'"
            End If

            Return sReturn

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            Return ""
        End Try
    End Function


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
    End Sub

    Public Sub New(ByVal rbMicroBioYn As Boolean)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        mbMicroBioYn = rbMicroBioYn
        If mbMicroBioYn Then
            msFile_CalForm = Windows.Forms.Application.StartupPath & msXmlDir & "\FGS09_M_CALFORM.XML"
        End If

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
    Friend WithEvents pnlMid As System.Windows.Forms.Panel
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents grpTop1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblRstDT As System.Windows.Forms.Label
    Friend WithEvents lblDat As System.Windows.Forms.Label
    Friend WithEvents dtpWkDayE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpWkDayS As System.Windows.Forms.DateTimePicker
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkC As System.Windows.Forms.CheckBox
    Friend WithEvents chkD As System.Windows.Forms.CheckBox
    Friend WithEvents chkP As System.Windows.Forms.CheckBox
    Friend WithEvents chkA As System.Windows.Forms.CheckBox
    Friend WithEvents pnlAbOpt As System.Windows.Forms.Panel
    Friend WithEvents lblJudg As System.Windows.Forms.Label
    Friend WithEvents rdoOr As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAnd As System.Windows.Forms.RadioButton
    Friend WithEvents chkFN As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS09))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.pnlMid = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnPrint = New CButtonLib.CButton()
        Me.btnQuery = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.grpTop1 = New System.Windows.Forms.GroupBox()
        Me.ChkMicro = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoViewRst = New System.Windows.Forms.RadioButton()
        Me.rdoOrgRst = New System.Windows.Forms.RadioButton()
        Me.chkSpc = New System.Windows.Forms.CheckBox()
        Me.btnClear_calc = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCalForm = New System.Windows.Forms.Button()
        Me.txtCalForm = New System.Windows.Forms.TextBox()
        Me.chkCtTest = New System.Windows.Forms.CheckBox()
        Me.chkFN = New System.Windows.Forms.CheckBox()
        Me.pnlAbOpt = New System.Windows.Forms.Panel()
        Me.chkH = New System.Windows.Forms.CheckBox()
        Me.chkL = New System.Windows.Forms.CheckBox()
        Me.rdoAnd = New System.Windows.Forms.RadioButton()
        Me.chkC = New System.Windows.Forms.CheckBox()
        Me.chkP = New System.Windows.Forms.CheckBox()
        Me.rdoOr = New System.Windows.Forms.RadioButton()
        Me.chkD = New System.Windows.Forms.CheckBox()
        Me.chkA = New System.Windows.Forms.CheckBox()
        Me.lblJudg = New System.Windows.Forms.Label()
        Me.lblRstDT = New System.Windows.Forms.Label()
        Me.lblDat = New System.Windows.Forms.Label()
        Me.dtpWkDayE = New System.Windows.Forms.DateTimePicker()
        Me.dtpWkDayS = New System.Windows.Forms.DateTimePicker()
        Me.pnlMid.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.grpTop1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlAbOpt.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlMid
        '
        Me.pnlMid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMid.Controls.Add(Me.spdList)
        Me.pnlMid.Location = New System.Drawing.Point(4, 96)
        Me.pnlMid.Name = "pnlMid"
        Me.pnlMid.Size = New System.Drawing.Size(1008, 490)
        Me.pnlMid.TabIndex = 53
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1004, 486)
        Me.spdList.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBottom.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlBottom.Controls.Add(Me.btnPrint)
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Location = New System.Drawing.Point(0, 592)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1015, 34)
        Me.pnlBottom.TabIndex = 125
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems1
        Me.btnPrint.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint.Corners.All = CType(6, Short)
        Me.btnPrint.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint.Corners.LowerRight = CType(6, Short)
        Me.btnPrint.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint.Corners.UpperRight = CType(6, Short)
        Me.btnPrint.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint.FocalPoints.CenterPtX = 0.4859813!
        Me.btnPrint.FocalPoints.CenterPtY = 0.16!
        Me.btnPrint.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker2
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(608, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(100, 25)
        Me.btnPrint.TabIndex = 205
        Me.btnPrint.Text = "출  력"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems2
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.4859813!
        Me.btnQuery.FocalPoints.CenterPtY = 0.16!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker4
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(507, 5)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(100, 25)
        Me.btnQuery.TabIndex = 201
        Me.btnQuery.Text = "조  회"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems3
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
        Me.btnExcel.FocalPoints.FocusPtX = 0.03738318!
        Me.btnExcel.FocalPoints.FocusPtY = 0.04!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker6
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(709, 5)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(100, 25)
        Me.btnExcel.TabIndex = 202
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems4
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.57!
        Me.btnClear.FocalPoints.CenterPtY = 0.4!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker8
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(810, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 203
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems5
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker10
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(911, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(100, 25)
        Me.btnExit.TabIndex = 204
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'grpTop1
        '
        Me.grpTop1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTop1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTop1.Controls.Add(Me.ChkMicro)
        Me.grpTop1.Controls.Add(Me.Panel1)
        Me.grpTop1.Controls.Add(Me.chkSpc)
        Me.grpTop1.Controls.Add(Me.btnClear_calc)
        Me.grpTop1.Controls.Add(Me.Label1)
        Me.grpTop1.Controls.Add(Me.btnCalForm)
        Me.grpTop1.Controls.Add(Me.txtCalForm)
        Me.grpTop1.Controls.Add(Me.chkCtTest)
        Me.grpTop1.Controls.Add(Me.chkFN)
        Me.grpTop1.Controls.Add(Me.pnlAbOpt)
        Me.grpTop1.Controls.Add(Me.lblJudg)
        Me.grpTop1.Controls.Add(Me.lblRstDT)
        Me.grpTop1.Controls.Add(Me.lblDat)
        Me.grpTop1.Controls.Add(Me.dtpWkDayE)
        Me.grpTop1.Controls.Add(Me.dtpWkDayS)
        Me.grpTop1.Location = New System.Drawing.Point(4, 0)
        Me.grpTop1.Name = "grpTop1"
        Me.grpTop1.Size = New System.Drawing.Size(1008, 90)
        Me.grpTop1.TabIndex = 154
        Me.grpTop1.TabStop = False
        '
        'ChkMicro
        '
        Me.ChkMicro.Location = New System.Drawing.Point(481, 14)
        Me.ChkMicro.Margin = New System.Windows.Forms.Padding(1)
        Me.ChkMicro.Name = "ChkMicro"
        Me.ChkMicro.Size = New System.Drawing.Size(85, 18)
        Me.ChkMicro.TabIndex = 208
        Me.ChkMicro.Text = "미생물"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.rdoViewRst)
        Me.Panel1.Controls.Add(Me.rdoOrgRst)
        Me.Panel1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Panel1.Location = New System.Drawing.Point(752, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(168, 21)
        Me.Panel1.TabIndex = 207
        '
        'rdoViewRst
        '
        Me.rdoViewRst.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoViewRst.Checked = True
        Me.rdoViewRst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoViewRst.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoViewRst.ForeColor = System.Drawing.Color.Black
        Me.rdoViewRst.Location = New System.Drawing.Point(87, 2)
        Me.rdoViewRst.Name = "rdoViewRst"
        Me.rdoViewRst.Size = New System.Drawing.Size(79, 17)
        Me.rdoViewRst.TabIndex = 11
        Me.rdoViewRst.TabStop = True
        Me.rdoViewRst.Text = "보고결과"
        Me.rdoViewRst.UseVisualStyleBackColor = False
        '
        'rdoOrgRst
        '
        Me.rdoOrgRst.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoOrgRst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOrgRst.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoOrgRst.ForeColor = System.Drawing.Color.Black
        Me.rdoOrgRst.Location = New System.Drawing.Point(6, 2)
        Me.rdoOrgRst.Name = "rdoOrgRst"
        Me.rdoOrgRst.Size = New System.Drawing.Size(77, 18)
        Me.rdoOrgRst.TabIndex = 10
        Me.rdoOrgRst.Text = "실제결과"
        Me.rdoOrgRst.UseVisualStyleBackColor = False
        '
        'chkSpc
        '
        Me.chkSpc.AutoSize = True
        Me.chkSpc.Location = New System.Drawing.Point(925, 42)
        Me.chkSpc.Name = "chkSpc"
        Me.chkSpc.Size = New System.Drawing.Size(72, 16)
        Me.chkSpc.TabIndex = 206
        Me.chkSpc.Text = "검체포함"
        Me.chkSpc.UseVisualStyleBackColor = True
        '
        'btnClear_calc
        '
        Me.btnClear_calc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear_calc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_calc.Location = New System.Drawing.Point(947, 61)
        Me.btnClear_calc.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_calc.Name = "btnClear_calc"
        Me.btnClear_calc.Size = New System.Drawing.Size(50, 21)
        Me.btnClear_calc.TabIndex = 205
        Me.btnClear_calc.Text = "Clear"
        Me.btnClear_calc.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(8, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 21)
        Me.Label1.TabIndex = 204
        Me.Label1.Text = "결과값"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCalForm
        '
        Me.btnCalForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCalForm.Image = CType(resources.GetObject("btnCalForm.Image"), System.Drawing.Image)
        Me.btnCalForm.Location = New System.Drawing.Point(85, 61)
        Me.btnCalForm.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCalForm.Name = "btnCalForm"
        Me.btnCalForm.Size = New System.Drawing.Size(26, 21)
        Me.btnCalForm.TabIndex = 203
        Me.btnCalForm.UseVisualStyleBackColor = True
        '
        'txtCalForm
        '
        Me.txtCalForm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCalForm.BackColor = System.Drawing.Color.Thistle
        Me.txtCalForm.ForeColor = System.Drawing.Color.Brown
        Me.txtCalForm.Location = New System.Drawing.Point(112, 61)
        Me.txtCalForm.Multiline = True
        Me.txtCalForm.Name = "txtCalForm"
        Me.txtCalForm.ReadOnly = True
        Me.txtCalForm.Size = New System.Drawing.Size(835, 21)
        Me.txtCalForm.TabIndex = 202
        '
        'chkCtTest
        '
        Me.chkCtTest.Location = New System.Drawing.Point(397, 15)
        Me.chkCtTest.Margin = New System.Windows.Forms.Padding(1)
        Me.chkCtTest.Name = "chkCtTest"
        Me.chkCtTest.Size = New System.Drawing.Size(85, 18)
        Me.chkCtTest.TabIndex = 171
        Me.chkCtTest.Text = "특수보고서"
        '
        'chkFN
        '
        Me.chkFN.Location = New System.Drawing.Point(279, 15)
        Me.chkFN.Name = "chkFN"
        Me.chkFN.Size = New System.Drawing.Size(115, 18)
        Me.chkFN.TabIndex = 2
        Me.chkFN.Text = "최종보고만 조회"
        '
        'pnlAbOpt
        '
        Me.pnlAbOpt.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlAbOpt.Controls.Add(Me.chkH)
        Me.pnlAbOpt.Controls.Add(Me.chkL)
        Me.pnlAbOpt.Controls.Add(Me.rdoAnd)
        Me.pnlAbOpt.Controls.Add(Me.chkC)
        Me.pnlAbOpt.Controls.Add(Me.chkP)
        Me.pnlAbOpt.Controls.Add(Me.rdoOr)
        Me.pnlAbOpt.Controls.Add(Me.chkD)
        Me.pnlAbOpt.Controls.Add(Me.chkA)
        Me.pnlAbOpt.ForeColor = System.Drawing.Color.DarkBlue
        Me.pnlAbOpt.Location = New System.Drawing.Point(85, 37)
        Me.pnlAbOpt.Name = "pnlAbOpt"
        Me.pnlAbOpt.Size = New System.Drawing.Size(464, 21)
        Me.pnlAbOpt.TabIndex = 164
        '
        'chkH
        '
        Me.chkH.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkH.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkH.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkH.ForeColor = System.Drawing.Color.Black
        Me.chkH.Location = New System.Drawing.Point(41, 2)
        Me.chkH.Name = "chkH"
        Me.chkH.Size = New System.Drawing.Size(33, 18)
        Me.chkH.TabIndex = 13
        Me.chkH.Text = "H"
        Me.chkH.UseVisualStyleBackColor = False
        '
        'chkL
        '
        Me.chkL.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkL.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkL.ForeColor = System.Drawing.Color.Black
        Me.chkL.Location = New System.Drawing.Point(4, 2)
        Me.chkL.Name = "chkL"
        Me.chkL.Size = New System.Drawing.Size(33, 18)
        Me.chkL.TabIndex = 12
        Me.chkL.Text = "L"
        Me.chkL.UseVisualStyleBackColor = False
        '
        'rdoAnd
        '
        Me.rdoAnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoAnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAnd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoAnd.ForeColor = System.Drawing.Color.Black
        Me.rdoAnd.Location = New System.Drawing.Point(410, 2)
        Me.rdoAnd.Name = "rdoAnd"
        Me.rdoAnd.Size = New System.Drawing.Size(44, 17)
        Me.rdoAnd.TabIndex = 11
        Me.rdoAnd.Text = "And"
        Me.rdoAnd.UseVisualStyleBackColor = False
        '
        'chkC
        '
        Me.chkC.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkC.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkC.ForeColor = System.Drawing.Color.Black
        Me.chkC.Location = New System.Drawing.Point(206, 2)
        Me.chkC.Name = "chkC"
        Me.chkC.Size = New System.Drawing.Size(77, 18)
        Me.chkC.TabIndex = 8
        Me.chkC.Text = "Critical"
        Me.chkC.UseVisualStyleBackColor = False
        '
        'chkP
        '
        Me.chkP.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkP.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkP.ForeColor = System.Drawing.Color.Black
        Me.chkP.Location = New System.Drawing.Point(80, 2)
        Me.chkP.Name = "chkP"
        Me.chkP.Size = New System.Drawing.Size(58, 18)
        Me.chkP.TabIndex = 6
        Me.chkP.Text = "Panic"
        Me.chkP.UseVisualStyleBackColor = False
        '
        'rdoOr
        '
        Me.rdoOr.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoOr.Checked = True
        Me.rdoOr.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOr.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoOr.ForeColor = System.Drawing.Color.Black
        Me.rdoOr.Location = New System.Drawing.Point(370, 2)
        Me.rdoOr.Name = "rdoOr"
        Me.rdoOr.Size = New System.Drawing.Size(36, 17)
        Me.rdoOr.TabIndex = 10
        Me.rdoOr.TabStop = True
        Me.rdoOr.Text = "Or"
        Me.rdoOr.UseVisualStyleBackColor = False
        '
        'chkD
        '
        Me.chkD.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkD.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkD.ForeColor = System.Drawing.Color.Black
        Me.chkD.Location = New System.Drawing.Point(144, 2)
        Me.chkD.Name = "chkD"
        Me.chkD.Size = New System.Drawing.Size(58, 18)
        Me.chkD.TabIndex = 7
        Me.chkD.Text = "Delta"
        Me.chkD.UseVisualStyleBackColor = False
        '
        'chkA
        '
        Me.chkA.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkA.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkA.ForeColor = System.Drawing.Color.Black
        Me.chkA.Location = New System.Drawing.Point(290, 2)
        Me.chkA.Name = "chkA"
        Me.chkA.Size = New System.Drawing.Size(62, 18)
        Me.chkA.TabIndex = 9
        Me.chkA.Text = "Alert"
        Me.chkA.UseVisualStyleBackColor = False
        '
        'lblJudg
        '
        Me.lblJudg.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblJudg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJudg.ForeColor = System.Drawing.Color.White
        Me.lblJudg.Location = New System.Drawing.Point(8, 37)
        Me.lblJudg.Name = "lblJudg"
        Me.lblJudg.Size = New System.Drawing.Size(76, 21)
        Me.lblJudg.TabIndex = 163
        Me.lblJudg.Text = "이상자구분"
        Me.lblJudg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRstDT
        '
        Me.lblRstDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstDT.ForeColor = System.Drawing.Color.White
        Me.lblRstDT.Location = New System.Drawing.Point(8, 14)
        Me.lblRstDT.Name = "lblRstDT"
        Me.lblRstDT.Size = New System.Drawing.Size(76, 21)
        Me.lblRstDT.TabIndex = 12
        Me.lblRstDT.Text = "보고일자"
        Me.lblRstDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDat
        '
        Me.lblDat.AutoSize = True
        Me.lblDat.BackColor = System.Drawing.Color.Transparent
        Me.lblDat.Location = New System.Drawing.Point(175, 19)
        Me.lblDat.Name = "lblDat"
        Me.lblDat.Size = New System.Drawing.Size(11, 12)
        Me.lblDat.TabIndex = 2
        Me.lblDat.Text = "~"
        Me.lblDat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpWkDayE
        '
        Me.dtpWkDayE.CustomFormat = "yyyy-MM"
        Me.dtpWkDayE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpWkDayE.Location = New System.Drawing.Point(188, 14)
        Me.dtpWkDayE.Name = "dtpWkDayE"
        Me.dtpWkDayE.Size = New System.Drawing.Size(88, 21)
        Me.dtpWkDayE.TabIndex = 1
        '
        'dtpWkDayS
        '
        Me.dtpWkDayS.CustomFormat = "yyyy-MM"
        Me.dtpWkDayS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpWkDayS.Location = New System.Drawing.Point(85, 14)
        Me.dtpWkDayS.Name = "dtpWkDayS"
        Me.dtpWkDayS.Size = New System.Drawing.Size(88, 21)
        Me.dtpWkDayS.TabIndex = 0
        '
        'FGS09
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 629)
        Me.Controls.Add(Me.grpTop1)
        Me.Controls.Add(Me.pnlMid)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS09"
        Me.Text = "이상자 분석(결과값 조회)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlMid.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.grpTop1.ResumeLayout(False)
        Me.grpTop1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlAbOpt.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FGS09_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS09_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            DisplayInit()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            mbLoaded = True

        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        With Me.spdList
            If .MaxRows < 1 Then Return

            .ReDraw = False

            .MaxRows += 1
            .InsertRows(1, 1)

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 1 : .Row2 = 1
            .Clip = sColHeaders

            If .ExportToExcel("AbnormalList_" + Now.ToShortDateString() + ".xls", "AbnormalList", "") Then
                Process.Start("AbnormalList_" + Now.ToShortDateString() + ".xls")
            End If

            .DeleteRows(1, 1)
            .MaxRows -= 1

            .ReDraw = True
        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Display_Clear()
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Display_List()

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        With spdList
            .Row = e.row
            .Col = .GetColFromID("orgrst")
            If .Text <> "{null}" Then Return

            .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
            .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

            Dim strst As New AxAckResultViewer.STRST01

            strst.SpecialTestName = sTnmd
            strst.BcNo = sBcNo
            strst.TestCd = sTestCd

            strst.Left = CType(Me.ParentForm.Left + (Me.ParentForm.Width - strst.Width) / 2, Integer)
            strst.Top = Me.ParentForm.Top + Ctrl.menuHeight

            strst.ShowDialog(Me)

        End With
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        With spdList
            For ix As Integer = 1 To .MaxCols

                .Row = 0 : .Col = ix
                If .ColHidden = False Then
                    stu_item = New STU_PrtItemInfo

                    If .ColID = "cbcno" Or .ColID = "regno" Or .ColID = "patnm" Or .ColID = "sexage" Or .ColID = "doctornm" Or _
                       .ColID = "deptnm" Or .ColID = "wardroom" Or .ColID = "spcnmd" Or .ColID = "tnmd" Or .ColID = "judgmark" Or _
                       .ColID = "panicmark" Or .ColID = "deltamark" Or .ColID = "criticalmark" Or .ColID = "alertmark" Or .ColID = "rstflag" Or _
                       .ColID = "orgrst" Or .ColID = "viewrst" Then
                        stu_item.CHECK = "1"
                    Else
                        stu_item.CHECK = "0"
                    End If
                    stu_item.TITLE = .Text
                    stu_item.FIELD = .ColID
                    If .ColID = "tatcont" Then
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10 + 50).ToString
                    Else
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString
                    End If
                    alItems.Add(stu_item)
                End If
            Next

        End With

        Return alItems

    End Function

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList

            With spdList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    Dim strBuf() As String = rsTitle_Item.Split("|"c)
                    Dim arlItem As New ArrayList

                    For intIdx As Integer = 0 To strBuf.Length - 1

                        If strBuf(intIdx) = "" Then Exit For

                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                        If intCol > 0 Then

                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                            .Row = intRow
                            .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                        End If
                    Next

                    Dim objPat As New FGS00_PATINFO

                    With objPat
                        .alItem = arlItem
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT

                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = "이상자조회(결과값 조회)"
                prt.maPrtData = arlPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnCalForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalForm.Click
        Dim frm As New FGS09_S01

        Dim sRetVale As String = frm.Display_Result(mbMicroBioYn, Me.chkCtTest.Checked, Me.chkSpc.Checked)

        If sRetVale.IndexOf("|") > 0 Then
            Me.txtCalForm.Tag = sRetVale.Split("|"c)(0)
            Me.txtCalForm.Text = sRetVale.Split("|"c)(1)
        End If

        COMMON.CommXML.setOneElementXML(msXmlDir, msFile_CalForm, "CALCFORM", Me.txtCalForm.Tag.ToString() + "|" + Me.txtCalForm.Text + "|" + IIf(Me.chkSpc.Checked, "1", "0").ToString)

    End Sub

    Private Sub btnClear_calc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_calc.Click
        Me.txtCalForm.Text = ""
        Me.txtCalForm.Tag = ""
        COMMON.CommXML.setOneElementXML(msXmlDir, msFile_CalForm, "CALCFORM", "")
    End Sub

    Private Sub chkSpc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSpc.CheckedChanged
        btnClear_calc_Click(Nothing, Nothing)
    End Sub
End Class

