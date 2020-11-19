Imports System.Drawing
Imports System.Net
Imports System.Windows.Forms
Imports System.IO
Imports AxAckResultViewer
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst

Public Class FGRV11
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGRV11.vb, Class : FGRV11 & vbTab"

    Private msRegNo As String = ""
    Private msSearchDayS As String = ""
    Private msSearchDayE As String = ""
    Private mbViewReportOnly As Boolean = False


    '< add yjlee  
    Public mbCalled As Boolean = False
    Private mbMicro As Boolean = False

    Private msXML As String = "\XML"
    Private msTermFile As String = Application.StartupPath + msXML + "\FGRV11_TERM.XML"

    Private m_al_GrpData As ArrayList

    Friend WithEvents txtDebug As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkDPoint As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboTerm As System.Windows.Forms.ComboBox
    Friend WithEvents chkDDatagrid As System.Windows.Forms.CheckBox
    Friend WithEvents chkLabel As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlMicro As System.Windows.Forms.Panel
    Friend WithEvents spdMicro As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpDayE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDayS As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearchOR As System.Windows.Forms.Button
    Friend WithEvents btnSearchR As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents axItemSave As AxAckItemSave.ITEMSAVE
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnClear_test As System.Windows.Forms.Button
    Friend WithEvents txtSelTest As System.Windows.Forms.TextBox
    Friend WithEvents axPatInfo As AxAckPatientInfo.AxSpcInfo
    Friend WithEvents rstChart1 As AxAckResultViewer.RSTCHART03
    Friend WithEvents lblDate As System.Windows.Forms.Label

    Public Sub sbBtnClick(ByVal rsFormGbn As String)

        If rsFormGbn = "R" Then
            Me.btnSearchR_Click(btnSearchR, Nothing)
        ElseIf rsFormGbn = "O" Then
            Me.btnSearchR_Click(btnSearchOR, Nothing)
        End If

    End Sub

    Public Sub Display_List()
        Dim sFn As String = "sbDisplay_List"

        Try
            Me.btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

            Dim sw As StreamWriter
            Dim sLastFileNm As String = System.Windows.Forms.Application.StartupPath + "\emr_LastGbn.ini"

            If IO.File.Exists(sLastFileNm) Then
                sw = New StreamWriter(sLastFileNm, False, System.Text.Encoding.UTF8)

                sw.WriteLine(Me.Text)
                sw.Close()
            End If
        End Try
    End Sub

    Public Sub Display_Result(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String)

        Me.dtpDayS.Value = CDate(rsDayS)
        Me.dtpDayE.Value = CDate(rsDayE)
        Me.txtNo.Text = rsRegNo

        sbDisplay_Data(rsRegNo)
    End Sub

    Private Function fnGet_LastOrdDt(ByVal rsRegNo As String) As String
        Dim sFn As String = "fnGet_LastOrdDt"
        Try
            Return LISAPP.APP_V.CommFn.fnGet_OrderDate_Max(rsRegNo)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Format(Now, "yyyy-MM-dd").ToString
        End Try

    End Function

    Public Sub sbDisplay_Data(ByVal rsRegNo As String)
        Dim sFn As String = "sbDisplay_data"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            sbDisplay_Clear()

            If rsRegNo = "" Then Return
            If IsNumeric(rsRegNo) Then
                rsRegNo = rsRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                rsRegNo = rsRegNo.Substring(0, 1).ToUpper + rsRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            End If
            Me.txtNo.Text = rsRegNo

            '< 신상정보
            Dim dt As DataTable = LISAPP.APP_V.CommFn.Get_SpcInfoByRegNo(msRegNo)
            If Not dt Is Nothing Then sbDisplay_SpcInfo(dt)
            '> 
            Dim sQryGbn As String = "O"
            dt = LISAPP.APP_V.CommFn.fnGet_Result_rv_slip(rsRegNo, sQryGbn, dtpDayS.Text, dtpDayE.Text)
            With spdSlip
                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("slipcd") : .Text = dt.Rows(ix).Item("slipcd").ToString
                    .Col = .GetColFromID("slipnm") : .Text = dt.Rows(ix).Item("slipnm").ToString
                Next
                .ReDraw = True

                If .MaxRows > 0 Then spdSlip_ClickEvent(Me.spdSlip, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("slipnm"), 1))
            End With


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Sub

    Private Sub sbDisplay_Data_rst(ByVal rsRegNo As String, ByVal rsTestCds As String)

        Dim sFn As String = "sbDisplay_Data_rst"

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            sbDisplayinit_spdResult()
            Me.rstChart1.Clear()

            Dim dt_Anti As New DataTable
            Dim dt As New DataTable

            If Me.lblDate.Text.StartsWith("접수") Then
                dt = LISAPP.APP_V.CommFn.fnGet_history_test_rv_partslip(rsRegNo, "", rsTestCds, Me.dtpDayS.Text, Me.dtpDayE.Text, dt_Anti)
            Else
                dt = LISAPP.APP_V.CommFn.fnGet_history_test_rv_tordslip(rsRegNo, "", rsTestCds, Me.dtpDayS.Text, Me.dtpDayE.Text, dt_Anti)
            End If
            If dt.Rows.Count = 0 Then Exit Sub

            Dim sTestCds As String = ""

            Me.spdResult.ReDraw = False
            Me.spdMicro.ReDraw = False

            With Me.spdResult
                .Row = 0
                .Col = .GetColFromID("unit") : .Text = "단위"

                .MaxRows = dt.Rows.Count
                For intRow As Integer = 0 To dt.Rows.Count - 1
                    .Row = intRow + 1
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(intRow).Item("tnmd").ToString
                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(intRow).Item("spcnmd").ToString
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(intRow).Item("testcd").ToString + "/" + dt.Rows(intRow).Item("spccd").ToString
                    .Col = .GetColFromID("reftxt") : .Text = dt.Rows(intRow).Item("reftxt").ToString
                    .Col = .GetColFromID("unit") : .Text = dt.Rows(intRow).Item("unit").ToString

                    If intRow <> 0 Then sTestCds += ","
                    sTestCds += dt.Rows(intRow).Item("testcd").ToString
                Next
            End With


            '< add yjlee 2009-04-20
            If dt_Anti.Rows.Count > 0 Then

                With Me.spdMicro
                    .MaxRows = dt_Anti.Rows.Count

                    For IntRow As Integer = 0 To dt_Anti.Rows.Count - 1
                        .Row = IntRow + 1
                        .Col = .GetColFromID("antinm") : .Text = dt_Anti.Rows(IntRow).Item("antinmd").ToString
                        .Col = .GetColFromID("anticd") : .Text = dt_Anti.Rows(IntRow).Item("anticd").ToString()
                    Next
                End With
            End If
            '> add yjlee 2009-04-20 

            dt = Nothing

            If spdResult.MaxRows < 1 Then Exit Sub

            '-- 누적 결과 표시-
            Dim dt_Micro As New DataTable

            dt = LISAPP.APP_V.CommFn.fnGet_history_rst_rv(IIf(Me.lblDate.Text.StartsWith("접수"), "J", "O").ToString, rsRegNo, sTestCds, dtpDayS.Text, dtpDayE.Text, dt_Micro)

            If dt Is Nothing Then Return

            spdResult.MaxCols = spdResult.GetColFromID("unit")

            If dt.Rows.Count < 1 Then Exit Sub

            Dim sTkDt As String = ""
            Dim arlTkDt As New ArrayList
            Dim intCol As Integer = 0

            With spdResult
                intCol = .GetColFromID("unit")
                For ix As Integer = 0 To dt.Rows.Count - 1
                    sTkDt = dt.Rows(ix).Item("tkdt").ToString

                    If arlTkDt.Contains(sTkDt) = False Then
                        intCol += 1

                        .MaxCols = intCol
                        .Row = 0
                        .Col = intCol : .ColID = sTkDt : .Text = sTkDt : .set_ColWidth(.Col, 8.13)

                        arlTkDt.Add(sTkDt)
                    End If

                    Dim sBcNo As String = ""
                    Dim sTestCd As String = ""
                    Dim iRow As Integer = 0

                    sBcNo = dt.Rows(ix).Item("bcno").ToString
                    sTestCd = dt.Rows(ix).Item("testcd").ToString + "/" + dt.Rows(ix).Item("spccd").ToString

                    .Row = .SearchCol(.GetColFromID("testcd"), 0, .MaxRows, sTestCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                    .Col = .GetColFromID(sTkDt)

                    iRow = .RowsFrozen

                    If .Col > 0 And .Row > 0 Then

                        If dt.Rows(ix).Item("rstflg").ToString <= "1" And mbViewReportOnly Then
                            .BackColor = Color.White
                            .ForeColor = Color.Black
                        Else
                            If dt.Rows(ix).Item("panicmark").ToString <> "" Then
                                .BackColor = Color.FromArgb(150, 150, 255)
                                .ForeColor = Color.FromArgb(255, 255, 255)
                            Else
                                If dt.Rows(ix).Item("hlmark").ToString <> "" Then
                                    If dt.Rows(ix).Item("hlmark").ToString = "H" Then
                                        .BackColor = Color.FromArgb(255, 230, 231)
                                        .ForeColor = Color.FromArgb(255, 0, 0)
                                    ElseIf dt.Rows(ix).Item("hlmark").ToString = "L" Then
                                        .BackColor = Color.FromArgb(221, 240, 255)
                                        .ForeColor = Color.FromArgb(0, 0, 255)
                                    End If

                                Else
                                    .BackColor = Color.White
                                    .ForeColor = Color.Black
                                End If
                            End If
                        End If

                        If dt.Rows(ix).Item("srpt").ToString <> "S" Then
                            Dim sTmp As String = dt.Rows(ix).Item("viewrst").ToString
                            If sTmp = "" Then sTmp = IIf(dt.Rows(ix).Item("rstflg").ToString < "2", FixedVariable.gsMsg_NoRpt, "").ToString()
                            If IsNumeric(sTmp) And sTmp.StartsWith(".") Then sTmp = "0" + sTmp

                            .CellTag = dt.Rows(ix).Item("bcno").ToString.Trim() + "/"

                            If dt.Rows(ix).Item("rstflg").ToString <= "1" And mbViewReportOnly Then
                                .Text = FixedVariable.gsMsg_NoRpt
                                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            Else
                                .Text = sTmp + "  " + dt.Rows(ix).Item("hlmark").ToString + dt.Rows(ix).Item("panicmark").ToString
                                If IsNumeric(sTmp) Then
                                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                                Else
                                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft
                                End If
                            End If
                        Else
                            If dt.Rows(ix).Item("rstflg").ToString <= "1" And mbViewReportOnly Then
                                .CellTag = dt.Rows(ix).Item("bcno").ToString.Trim() + "/"
                                .Text = FixedVariable.gsMsg_NoRpt
                            Else
                                .CellTag = dt.Rows(ix).Item("bcno").ToString.Trim() + "/" + "S"
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
                                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                                .TypePictPicture = GetImgList.getImgOther("LEAF")
                                .TypePictStretch = False
                            End If
                        End If

                        If cboTerm.Text <> "" Then
                            .set_ColWidth(.Col, CDbl(cboTerm.Text))
                        End If
                        '> add yjlee 2009-03-25  
                    End If
                Next
            End With

            If dt_Micro.Rows.Count > 0 Then
                Dim alKeys As New ArrayList

                With spdMicro
                    intCol = .GetColFromID("anticd")
                    For intIdx As Integer = 0 To dt_Micro.Rows.Count - 1
                        Dim sBcNo As String = dt_Micro.Rows(intIdx).Item("bcno").ToString.Trim
                        Dim sTestCd As String = dt_Micro.Rows(intIdx).Item("testcd").ToString.Trim
                        Dim sRanking As String = dt_Micro.Rows(intIdx).Item("ranking").ToString.Trim

                        Dim sTnmd As String = dt_Micro.Rows(intIdx).Item("tnmd").ToString.Trim
                        Dim sBacNm As String = dt_Micro.Rows(intIdx).Item("bacnmd").ToString.Trim


                        If alKeys.Contains(sBcNo + "/" + sTestCd + "/" + sRanking) = False Then
                            intCol += 1

                            alKeys.Add(sBcNo + "/" + sTestCd + "/" + sRanking)

                            .MaxCols = intCol
                            .BlockMode = True
                            .Col = .MaxCols : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                            .BlockMode = False

                            .Row = 0
                            .Col = intCol : .Text = sBcNo

                            .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                            .Col = intCol : .Text = sBacNm ' sTnmd + ":" + sBacNm
                            .set_ColWidth(.Col, 15)

                            .ColID = sBcNo + "/" + sTestCd + "/" + sRanking
                        End If

                        Dim sAntiCd As String = dt_Micro.Rows(intIdx).Item("anticd").ToString.Trim
                        Dim iRow As Integer = 0

                        .Col = .GetColFromID(sBcNo + "/" + sTestCd + "/" + sRanking)
                        .Row = .SearchCol(.GetColFromID("anticd"), 0, .MaxRows, sAntiCd, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                        iRow = .RowsFrozen


                        .Text = dt_Micro.Rows(intIdx).Item("antirst").ToString.Trim
                    Next
                End With

                SplitContainer1.SplitterDistance = CInt(SplitContainer1.Height / 2)
                Me.pnlMicro.Visible = True
                Me.rstChart1.Visible = False
            Else
                SplitContainer1.SplitterDistance = CInt(SplitContainer1.Height * 0.8)
                Me.pnlMicro.Visible = False
                Me.rstChart1.Visible = True
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            Me.spdResult.ReDraw = True
            Me.spdMicro.ReDraw = True
        End Try

    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub


    Public Sub New(ByVal rbViewReportOnly As Boolean)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        mbViewReportOnly = rbViewReportOnly

    End Sub

    Public Sub New(ByVal rsRegNo As String, ByVal rsDayS As String, ByVal rsDayE As String, Optional ByVal rbViewReportOnly As Boolean = False)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        msRegNo = rsRegNo
        msSearchDayS = rsDayS
        msSearchDayE = rsDayE
        mbViewReportOnly = rbViewReportOnly

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
    Friend WithEvents lblNo As System.Windows.Forms.Label
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents grpNo As System.Windows.Forms.GroupBox
    Friend WithEvents spdResult As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdSlip As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdTest As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGRV11))
        Me.grpNo = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.dtpDayE = New System.Windows.Forms.DateTimePicker
        Me.dtpDayS = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.btnToggle = New System.Windows.Forms.Button
        Me.lblNo = New System.Windows.Forms.Label
        Me.txtNo = New System.Windows.Forms.TextBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdResult = New AxFPSpreadADO.AxfpSpread
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.spdSlip = New AxFPSpreadADO.AxfpSpread
        Me.spdTest = New AxFPSpreadADO.AxfpSpread
        Me.txtDebug = New System.Windows.Forms.TextBox
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.chkLabel = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.chkDDatagrid = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboTerm = New System.Windows.Forms.ComboBox
        Me.chkDPoint = New System.Windows.Forms.CheckBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.rstChart1 = New AxAckResultViewer.RSTCHART03
        Me.pnlMicro = New System.Windows.Forms.Panel
        Me.spdMicro = New AxFPSpreadADO.AxfpSpread
        Me.btnSearchOR = New System.Windows.Forms.Button
        Me.btnSearchR = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnSearch = New System.Windows.Forms.Button
        Me.axItemSave = New AxAckItemSave.ITEMSAVE
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnClear_test = New System.Windows.Forms.Button
        Me.txtSelTest = New System.Windows.Forms.TextBox
        Me.axPatInfo = New AxAckPatientInfo.AxSpcInfo
        Me.grpNo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.spdSlip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pnlMicro.SuspendLayout()
        CType(Me.spdMicro, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpNo
        '
        Me.grpNo.Controls.Add(Me.Label2)
        Me.grpNo.Controls.Add(Me.dtpDayE)
        Me.grpNo.Controls.Add(Me.dtpDayS)
        Me.grpNo.Controls.Add(Me.lblDate)
        Me.grpNo.Controls.Add(Me.btnToggle)
        Me.grpNo.Controls.Add(Me.lblNo)
        Me.grpNo.Controls.Add(Me.txtNo)
        Me.grpNo.Location = New System.Drawing.Point(4, 0)
        Me.grpNo.Name = "grpNo"
        Me.grpNo.Size = New System.Drawing.Size(283, 59)
        Me.grpNo.TabIndex = 1
        Me.grpNo.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(170, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 12)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "~"
        '
        'dtpDayE
        '
        Me.dtpDayE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayE.Location = New System.Drawing.Point(188, 34)
        Me.dtpDayE.Name = "dtpDayE"
        Me.dtpDayE.Size = New System.Drawing.Size(88, 21)
        Me.dtpDayE.TabIndex = 33
        Me.dtpDayE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'dtpDayS
        '
        Me.dtpDayS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayS.Location = New System.Drawing.Point(79, 34)
        Me.dtpDayS.Name = "dtpDayS"
        Me.dtpDayS.Size = New System.Drawing.Size(88, 21)
        Me.dtpDayS.TabIndex = 32
        Me.dtpDayS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDate.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDate.ForeColor = System.Drawing.Color.White
        Me.lblDate.Location = New System.Drawing.Point(6, 34)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(72, 22)
        Me.lblDate.TabIndex = 30
        Me.lblDate.Text = "처방일자"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(232, 12)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(44, 21)
        Me.btnToggle.TabIndex = 2
        Me.btnToggle.Text = "<->"
        '
        'lblNo
        '
        Me.lblNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNo.ForeColor = System.Drawing.Color.White
        Me.lblNo.Location = New System.Drawing.Point(6, 11)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.Size = New System.Drawing.Size(72, 22)
        Me.lblNo.TabIndex = 0
        Me.lblNo.Text = "등록번호"
        Me.lblNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNo
        '
        Me.txtNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNo.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtNo.Location = New System.Drawing.Point(79, 12)
        Me.txtNo.MaxLength = 8
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(151, 21)
        Me.txtNo.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.spdResult)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(867, 255)
        Me.Panel1.TabIndex = 14
        '
        'spdResult
        '
        Me.spdResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdResult.DataSource = Nothing
        Me.spdResult.Location = New System.Drawing.Point(1, 1)
        Me.spdResult.Name = "spdResult"
        Me.spdResult.OcxState = CType(resources.GetObject("spdResult.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdResult.Size = New System.Drawing.Size(865, 252)
        Me.spdResult.TabIndex = 7
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.spdSlip)
        Me.Panel2.Controls.Add(Me.spdTest)
        Me.Panel2.Location = New System.Drawing.Point(3, 252)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(283, 359)
        Me.Panel2.TabIndex = 16
        '
        'spdSlip
        '
        Me.spdSlip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdSlip.DataSource = Nothing
        Me.spdSlip.Location = New System.Drawing.Point(1, 0)
        Me.spdSlip.Name = "spdSlip"
        Me.spdSlip.OcxState = CType(resources.GetObject("spdSlip.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSlip.Size = New System.Drawing.Size(110, 356)
        Me.spdSlip.TabIndex = 0
        '
        'spdTest
        '
        Me.spdTest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdTest.DataSource = Nothing
        Me.spdTest.Location = New System.Drawing.Point(113, 0)
        Me.spdTest.Name = "spdTest"
        Me.spdTest.OcxState = CType(resources.GetObject("spdTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTest.Size = New System.Drawing.Size(170, 356)
        Me.spdTest.TabIndex = 1
        '
        'txtDebug
        '
        Me.txtDebug.Location = New System.Drawing.Point(1088, 17)
        Me.txtDebug.Name = "txtDebug"
        Me.txtDebug.Size = New System.Drawing.Size(166, 21)
        Me.txtDebug.TabIndex = 20
        Me.txtDebug.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.chkLabel)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.chkDDatagrid)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.cboTerm)
        Me.Panel3.Controls.Add(Me.chkDPoint)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Location = New System.Drawing.Point(1050, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(108, 112)
        Me.Panel3.TabIndex = 22
        '
        'chkLabel
        '
        Me.chkLabel.AutoSize = True
        Me.chkLabel.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkLabel.Location = New System.Drawing.Point(3, 95)
        Me.chkLabel.Name = "chkLabel"
        Me.chkLabel.Size = New System.Drawing.Size(43, 15)
        Me.chkLabel.TabIndex = 193
        Me.chkLabel.Text = "X축"
        Me.chkLabel.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(2, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(106, 22)
        Me.Label6.TabIndex = 192
        Me.Label6.Text = "그래프출력정보"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkDDatagrid
        '
        Me.chkDDatagrid.AutoSize = True
        Me.chkDDatagrid.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkDDatagrid.Location = New System.Drawing.Point(44, 77)
        Me.chkDDatagrid.Name = "chkDDatagrid"
        Me.chkDDatagrid.Size = New System.Drawing.Size(57, 15)
        Me.chkDDatagrid.TabIndex = 191
        Me.chkDDatagrid.Text = "데이터"
        Me.chkDDatagrid.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 190
        Me.Label4.Text = "결과간격"
        '
        'cboTerm
        '
        Me.cboTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTerm.Items.AddRange(New Object() {"8", "9", "10", "11", "12", "13", "14", "15"})
        Me.cboTerm.Location = New System.Drawing.Point(57, 26)
        Me.cboTerm.Margin = New System.Windows.Forms.Padding(0)
        Me.cboTerm.Name = "cboTerm"
        Me.cboTerm.Size = New System.Drawing.Size(51, 20)
        Me.cboTerm.TabIndex = 189
        '
        'chkDPoint
        '
        Me.chkDPoint.AutoSize = True
        Me.chkDPoint.Checked = True
        Me.chkDPoint.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDPoint.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkDPoint.Location = New System.Drawing.Point(3, 77)
        Me.chkDPoint.Name = "chkDPoint"
        Me.chkDPoint.Size = New System.Drawing.Size(46, 15)
        Me.chkDPoint.TabIndex = 188
        Me.chkDPoint.Text = "수치"
        Me.chkDPoint.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(2, 1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 22)
        Me.Label5.TabIndex = 186
        Me.Label5.Text = "결과화면출력정보"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(291, 117)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1MinSize = 255
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.rstChart1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlMicro)
        Me.SplitContainer1.Size = New System.Drawing.Size(867, 491)
        Me.SplitContainer1.SplitterDistance = 255
        Me.SplitContainer1.TabIndex = 24
        '
        'rstChart1
        '
        Me.rstChart1.AxisVisible = False
        Me.rstChart1.BackColor = System.Drawing.Color.White
        Me.rstChart1.DataGridVisible = False
        Me.rstChart1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rstChart1.EndDate = ""
        Me.rstChart1.ExamCd = ""
        Me.rstChart1.ExamNm = ""
        Me.rstChart1.Location = New System.Drawing.Point(0, 0)
        Me.rstChart1.Name = "rstChart1"
        Me.rstChart1.PointLabelVisible = False
        Me.rstChart1.RefTxt = ""
        Me.rstChart1.RegNo = ""
        Me.rstChart1.Size = New System.Drawing.Size(867, 232)
        Me.rstChart1.TabIndex = 19
        Me.rstChart1.Viewer = False
        '
        'pnlMicro
        '
        Me.pnlMicro.Controls.Add(Me.spdMicro)
        Me.pnlMicro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMicro.Location = New System.Drawing.Point(0, 0)
        Me.pnlMicro.Name = "pnlMicro"
        Me.pnlMicro.Size = New System.Drawing.Size(867, 232)
        Me.pnlMicro.TabIndex = 18
        Me.pnlMicro.Visible = False
        '
        'spdMicro
        '
        Me.spdMicro.DataSource = Nothing
        Me.spdMicro.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdMicro.Location = New System.Drawing.Point(0, 0)
        Me.spdMicro.Name = "spdMicro"
        Me.spdMicro.OcxState = CType(resources.GetObject("spdMicro.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdMicro.Size = New System.Drawing.Size(867, 232)
        Me.spdMicro.TabIndex = 0
        '
        'btnSearchOR
        '
        Me.btnSearchOR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearchOR.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchOR.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnSearchOR.Location = New System.Drawing.Point(147, 86)
        Me.btnSearchOR.Name = "btnSearchOR"
        Me.btnSearchOR.Size = New System.Drawing.Size(140, 24)
        Me.btnSearchOR.TabIndex = 164
        Me.btnSearchOR.Text = "결과조회(처방일자별)"
        '
        'btnSearchR
        '
        Me.btnSearchR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearchR.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearchR.ForeColor = System.Drawing.Color.MediumBlue
        Me.btnSearchR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearchR.Location = New System.Drawing.Point(4, 86)
        Me.btnSearchR.Name = "btnSearchR"
        Me.btnSearchR.Size = New System.Drawing.Size(140, 24)
        Me.btnSearchR.TabIndex = 163
        Me.btnSearchR.Text = "결과조회(일일보고서)"
        '
        'btnClear
        '
        Me.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear.Location = New System.Drawing.Point(195, 61)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(92, 24)
        Me.btnClear.TabIndex = 162
        Me.btnClear.Text = "화면정리"
        '
        'btnPrint
        '
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrint.Location = New System.Drawing.Point(100, 61)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(92, 24)
        Me.btnPrint.TabIndex = 161
        Me.btnPrint.Text = "엑셀(&E)"
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Location = New System.Drawing.Point(4, 61)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(92, 24)
        Me.btnSearch.TabIndex = 160
        Me.btnSearch.Text = "조회(&S)"
        '
        'axItemSave
        '
        Me.axItemSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axItemSave.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axItemSave.Location = New System.Drawing.Point(4, 114)
        Me.axItemSave.Margin = New System.Windows.Forms.Padding(1)
        Me.axItemSave.Name = "axItemSave"
        Me.axItemSave.Size = New System.Drawing.Size(283, 89)
        Me.axItemSave.TabIndex = 197
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 205)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 22)
        Me.Label1.TabIndex = 196
        Me.Label1.Text = "검사항목"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClear_test
        '
        Me.btnClear_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_test.Location = New System.Drawing.Point(4, 228)
        Me.btnClear_test.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_test.Name = "btnClear_test"
        Me.btnClear_test.Size = New System.Drawing.Size(72, 21)
        Me.btnClear_test.TabIndex = 194
        Me.btnClear_test.Text = "Clear"
        Me.btnClear_test.UseVisualStyleBackColor = True
        '
        'txtSelTest
        '
        Me.txtSelTest.BackColor = System.Drawing.Color.Thistle
        Me.txtSelTest.ForeColor = System.Drawing.Color.Brown
        Me.txtSelTest.Location = New System.Drawing.Point(77, 205)
        Me.txtSelTest.Multiline = True
        Me.txtSelTest.Name = "txtSelTest"
        Me.txtSelTest.ReadOnly = True
        Me.txtSelTest.Size = New System.Drawing.Size(210, 44)
        Me.txtSelTest.TabIndex = 195
        '
        'axPatInfo
        '
        Me.axPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axPatInfo.Location = New System.Drawing.Point(294, 1)
        Me.axPatInfo.Name = "axPatInfo"
        Me.axPatInfo.Size = New System.Drawing.Size(755, 116)
        Me.axPatInfo.TabIndex = 198
        '
        'FGRV11
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1158, 608)
        Me.Controls.Add(Me.axPatInfo)
        Me.Controls.Add(Me.axItemSave)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClear_test)
        Me.Controls.Add(Me.txtSelTest)
        Me.Controls.Add(Me.btnSearchOR)
        Me.Controls.Add(Me.btnSearchR)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.grpNo)
        Me.Controls.Add(Me.txtDebug)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "FGRV11"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FGRV11"
        Me.grpNo.ResumeLayout(False)
        Me.grpNo.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.spdSlip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.pnlMicro.ResumeLayout(False)
        CType(Me.spdMicro, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Function fnFind_RstDtUsr(ByVal ra_dr() As DataRow, ByRef rsTestDt As String, ByRef rsTestUsr As String, _
                                        ByRef rsFnDt As String, ByRef rsFnUsr As String) As String
        Dim sFn As String = "fnFind_RstDtUsr"

        Try
            Dim dt As DataTable

            Dim sFilter As String = ""
            Dim sSort As String = ""

            dt = Fn.ChangeToDataTable(ra_dr)

            Dim sLabDrNm As String = ra_dr(0).Item("labdrnm").ToString()

            rsFnDt = ra_dr(0).Item("rstdt").ToString()
            rsFnUsr = ra_dr(0).Item("rstusr").ToString()

            '최종보고시간 중 가장 빠른 시간 --> TestDt
            rsTestDt = ra_dr(ra_dr.Length - 1).Item("rstdt").ToString()
            rsTestUsr = ra_dr(ra_dr.Length - 1).Item("rstusr").ToString()

            ra_dr = dt.Select("fixrptusr <> ''", "rstdt desc")

            If ra_dr.Length > 0 Then
                sLabDrNm = ra_dr(0).Item("fixrptusr").ToString()
            End If

            Return sLabDrNm

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Function

    Private Function fnFind_RegNo(ByVal rsPatNm As String) As String
        Dim sFn As String = "fnFind_RegNo"

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_byNm(rsPatNm)

            objHelp.FormText = "환자정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            objHelp.AddField("regno", "등록번호", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("patnm", "성명", 12, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sex", "성별", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("idno", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtNo)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.txtNo.Left, pntFrmXY.Y + pntCtlXY.Y + txtNo.Height + 80, dt)

            If aryList.Count > 0 Then
                msRegNo = aryList.Item(0).ToString.Split("|"c)(0)
                Me.txtNo.Text = aryList.Item(0).ToString.Split("|"c)(1)
                Return msRegNo
            End If

            Return ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return ""
        End Try

    End Function

    Private Sub sbDisplay_Clear()
        Dim sFn As String = "sbDisplay_Clear"

        Try

            Me.spdSlip.MaxRows = 0
            Me.spdTest.MaxRows = 0

            Me.axPatInfo.sbInit()

            sbDisplayinit_spdResult()
            rstChart1.Clear()

            Me.txtNo.Focus()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub


    Private Sub sbDisplay_Test(ByVal rsSlipCd As String)
        Dim sFn As String = "sbDisplay_Test"

        Try
            Dim dt As DataTable = LISAPP.APP_V.CommFn.fnGet_History_slip_test_rv(Me.txtNo.Text, rsSlipCd, IIf(Me.lblDate.Text.StartsWith("처방"), "O", "J").ToString, Me.dtpDayS.Text.Replace("-", ""), Me.dtpDayE.Text.Replace("-", ""))

            With Me.spdTest
                .MaxRows = dt.Rows.Count

                If dt.Rows.Count < 1 Then Return

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.Trim
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim
                Next

                spdTest_ClickEvent(spdTest, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.GetColFromID("testcd"), 1))
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_SpcInfo(ByVal r_dt As DataTable)
        Dim sFn As String = "sbDisplay_SpcInfo"

        Try

            Dim dtSysDate As Date = Fn.GetServerDateTime()
            Dim r_si As New AxAckResultViewer.SpecimenInfo

            Dim a_dr() As DataRow = r_dt.Select("", "rstdt desc")

            If r_dt.Rows.Count = 0 Then Exit Sub

            Dim sPatInfo() As String = r_dt.Rows(0).Item("patinfo").ToString.Split("|"c)
            '< 나이계산

            Dim dtBirthDay As Date = Nothing
            Dim iAge As Integer = 0
            If sPatInfo.Length > 1 Then
                If IsDate(sPatInfo(2)) Then
                    dtBirthDay = CDate(sPatInfo(2).Trim)
                    iAge = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)
                End If
            End If

            If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
            '>
            r_si.LabDrNm = fnFind_RstDtUsr(a_dr, r_si.TestDt, r_si.TestUsr, r_si.FnDt, r_si.FnUsr)

            r_si.RegNo = r_dt.Rows(0).Item("regno").ToString.Trim
            If sPatInfo.Length > 1 Then
                r_si.PatNm = sPatInfo(0).Trim
                r_si.IdNo = sPatInfo(3).Trim
            Else
                r_si.PatNm = ""
                r_si.IdNo = ""
            End If

            r_si.WardRoom = r_dt.Rows(0).Item("wardroom").ToString.Trim
            r_si.EntDt = r_dt.Rows(0).Item("entday").ToString.Trim
            r_si.DeptNm = r_dt.Rows(0).Item("deptcd").ToString.Trim

            If r_dt.Rows(0).Item("sexage").ToString.Trim = "" And sPatInfo.Length > 1 Then
                r_si.SexAge = sPatInfo(1) + "/" + iAge.ToString
            Else
                r_si.SexAge = r_dt.Rows(0).Item("sexage").ToString.Trim
            End If

            r_si.TestUsr = r_dt.Rows(0).Item("rstusr").ToString.Trim
            r_si.DoctorNm = r_dt.Rows(0).Item("doctornm").ToString.Trim
            r_si.OrdDt = r_dt.Rows(0).Item("orddt").ToString.Trim
            r_si.CollDt = r_dt.Rows(0).Item("colldt").ToString.Trim
            r_si.CollUsr = r_dt.Rows(0).Item("collusr").ToString.Trim
            r_si.TkDt = r_dt.Rows(0).Item("tkdt").ToString.Trim
            r_si.TkUsr = r_dt.Rows(0).Item("tkusr").ToString.Trim
            r_si.DiagNm = r_dt.Rows(0).Item("diagnm").ToString.Trim
            r_si.Remark = r_dt.Rows(0).Item("doctorrmk").ToString.Trim
            r_si.TestDt = r_dt.Rows(0).Item("rstdt").ToString.Trim
            r_si.TestUsr = r_dt.Rows(0).Item("rstusr").ToString.Trim
            r_si.Remark2 = r_dt.Rows(0).Item("remark2").ToString.Trim
            r_si.InfInfo = r_dt.Rows(0).Item("infinfo").ToString.Trim

            axPatInfo.sbDisplay_SpcInfo(r_si)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try

            Me.axItemSave.FORMID = Me.Name
            Me.axItemSave.USRID = USER_INFO.USRID
            Me.axItemSave.ITEMGBN = "ALL"
            Me.axItemSave.SPCGBN = "NONE"
            Me.axItemSave.sbDisplay_ItemList()

            Me.spdSlip.MaxRows = 0
            Me.spdTest.MaxRows = 0

            Me.axPatInfo.sbInit()
            Me.rstChart1.Clear()

            sbDisplayinit_spdResult()

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXML, msTermFile, "TERM")
            If sTmp <> "" Then
                If CInt(sTmp) < cboTerm.Items.Count Then cboTerm.SelectedIndex = CInt(sTmp)
            End If

            Me.txtNo.Focus()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayinit_spdResult()
        With spdResult
            .MaxRows = 0
            .Row = 0 : .Row2 = 0
            .Col = .GetColFromID("unit") + 1 : .Col2 = .MaxCols
            .BlockMode = True
            .Text = ""
            .BlockMode = False
        End With

        '< mod yjlee 2009-04-20
        With spdMicro
            .ClearRange(1, 0, .MaxCols, .MaxRows, False)

            .MaxRows = 1
            .MaxCols = 2
        End With
        '> mod yjlee 2009-04-20
    End Sub

    Private Sub FGRV11_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim sFn As String = ""

        Try
            COMMON.CommXML.setOneElementXML(msXML, msTermFile, "TERM", cboTerm.SelectedIndex.ToString)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally

            If mbViewReportOnly = False Then MdiTabControl.sbTabPageMove(Me)
        End Try
    End Sub


    '<----- Control Event ----->
    Private Sub FGRV11_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = Windows.Forms.FormWindowState.Maximized
        Me.txtNo.MaxLength = PRG_CONST.Len_RegNo

        sbDisplayInit()

        '등록번호, 조회일자S, 조회일자E 의 조건으로 리스트 조회
        If msRegNo.Length > 0 Then
            Me.txtNo.Text = msRegNo

            If msSearchDayE = "" Then msSearchDayE = Format(Now, "yyyy-MM-dd").ToString
            If msSearchDayS = "" Then msSearchDayS = Format(DateAdd(DateInterval.Month, -3, CDate(fnGet_LastOrdDt(msRegNo))), "yyyy-MM-dd").ToString

            Me.dtpDayS.Value = CDate(msSearchDayS)
            Me.dtpDayE.Value = CDate(msSearchDayE)

            sbDisplay_Data(msRegNo)
        Else
            Me.dtpDayE.Value = Now
            Me.dtpDayS.Value = DateAdd(DateInterval.Month, -3, Now)
        End If

        Me.txtNo.Focus()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Clear()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim sFn As String = "Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click"
        Try
            Dim sTime As String = Format(Now, "yyyyMMdd")
            Dim sRegNo As String = Me.axPatInfo.RegNo
            Dim sPatnm As String = Me.axPatInfo.PatNm
            Dim sSexage As String = Me.axPatInfo.SexAge

            With spdResult
                .ReDraw = False

                .MaxRows += 5
                .InsertRows(1, 4)

                .Col = 2
                .Row = 1
                .Text = "누적결과 조회"
                .FontBold = True
                .FontSize = 15

                .Col = 2
                .Row = 3
                .Text = "등록번호 : " + sRegNo + Space(10) + "성명 : " + sPatnm + Space(10) + "성별/나이 : " + sSexage
                .FontBold = True

                Dim sColHeaders As String = ""

                .Col = 1 : .Col2 = .MaxCols
                .Row = 0 : .Row2 = 0
                sColHeaders = .Clip

                .Col = 1 : .Col2 = .MaxCols
                .Row = 4 : .Row2 = 4
                .Clip = sColHeaders


                If spdResult.ExportToExcel("c:\누적결과 조회_" & sTime & ".xls", "TransfList", "") Then
                    Process.Start("c:\누적결과 조회_" & sTime & ".xls")
                End If

                .DeleteRows(1, 4)
                .MaxRows -= 5

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If Me.txtSelTest.Text <> "" Then
            sbDisplay_Data_rst(Me.txtNo.Text, Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ","))
        Else
            sbDisplay_Data(Me.txtNo.Text)
        End If

    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        '진단검사의학과 프로그램에서는 등록번호 -> 성명 -> 검체번호 -> 등록번호 ...
        If Windows.Forms.Application.ExecutablePath.ToUpper.EndsWith(FixedVariable.gsExeFileName.ToUpper) Then
            Fn.SearchToggle(Me.lblNo, Me.btnToggle, enumToggle.Regno_Name_Bcno, Me.txtNo)
            Me.txtNo.Focus()
        Else
            Fn.SearchToggle(Me.lblNo, Me.btnToggle, enumToggle.RegnoToName, Me.txtNo)
            Me.txtNo.Focus()
        End If
    End Sub

    Private Sub txtNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNo.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Select Case Me.lblNo.Text
            Case "검체번호"
                Return

            Case "등록번호"
                '등록번호, 성명의 경우 처리
                If IsNumeric(Me.txtNo.Text.Substring(1)) Then
                    If IsNumeric(Me.txtNo.Text.Substring(0, 1)) Then
                        Me.txtNo.Text = Me.txtNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                    Else
                        Me.txtNo.Text = Me.txtNo.Text.Substring(0, 1) + Me.txtNo.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                    End If
                End If

            Case Else
                Me.txtNo.Text = fnFind_RegNo(Me.txtNo.Text)

                Do While True
                    btnToggle_Click(Nothing, Nothing)
                    If lblNo.Text = "등록번호" Then Exit Do
                Loop
        End Select

        Me.dtpDayS.Value = DateAdd(DateInterval.Month, -3, CDate(fnGet_LastOrdDt(Me.txtNo.Text)))
        Me.btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub txtNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNo.GotFocus
        txtNo.SelectAll()
    End Sub

    Private Function fnGetDecimal(ByVal rsStr As String) As String
        Dim sFn As String = "fnGetDecimal"

        Dim sBuf() As String
        Dim iCnt As Integer = 0
        Dim iTmp As Integer = 0
        Dim sReturn As String = ""

        Try
            If rsStr = "" Then Return ""

            Dim sTmp As String = rsStr
            Dim sTmpBuf As String

            If sTmp.IndexOf("<=") > -1 Then
                sBuf = sTmp.Split("<=".ToCharArray()(0))
            ElseIf sTmp.IndexOf("<") > -1 Then
                sBuf = sTmp.Split("<"c)
            ElseIf sTmp.IndexOf("~") > -1 Then
                sBuf = sTmp.Split("~"c)
            Else
                sBuf = sTmp.Split(" "c)
            End If

            If sBuf.Length > 0 Then
                For iCnt = 0 To sBuf.Length - 1
                    sTmpBuf = sBuf(iCnt).Trim()
                    If IsNumeric(sTmpBuf) Then
                        If sTmpBuf.IndexOf(".") > -1 Then
                            If iTmp < sTmpBuf.Length - sTmpBuf.IndexOf(".") - 1 Then
                                iTmp = CInt(sTmpBuf.Length - sTmpBuf.IndexOf(".") - 1)
                            End If
                        End If
                    Else
                        Return CStr(iTmp)
                    End If
                Next
            Else
                Return CStr(iTmp)
            End If

            Return CStr(iTmp)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
            Return rsStr
        End Try
    End Function

    Private Sub spdResult_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdResult.ClickEvent
        Dim sFn As String = "spdResult_ClickEvent"

        Try
            With spdResult
                .Row = e.row
                .Col = .GetColFromID("unit") + 1 'e.col

                Dim sBcNo As String = ""
                Dim sTestCd As String = ""
                Dim sTnm As String = ""
                Dim objTable As New DataTable

                If Not .CellTag = "" Then
                    If .CellTag.Split("/"c)(1) = "S" Then
                        If Not .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture Then Return

                        sBcNo = .CellTag.Split("/"c)(0) '.ColID
                        .Col = .GetColFromID("testcd")
                        sTestCd = .Text.Split("/"c)(0).Trim()
                        .Col = .GetColFromID("tnmd")
                        sTnm = .Text

                        sbDisplay_SpecialReport(sBcNo, sTestCd, sTnm)
                    End If

                    If .CellTag = "" Then
                        axPatInfo.sbInit()
                    Else
                        sBcNo = .CellTag.Split("/"c)(0)

                        objTable = LISAPP.APP_V.CommFn.fnGet_SpcInfo(sBcNo)
                        If objTable.Rows.Count > 0 Then sbDisplay_SpcInfo(objTable)

                        spdResultGraph(.GetColFromID("unit") + 1, e.row)
                    End If
                End If

                .Row = e.row
                If e.col > .GetColFromID("unit") Then
                    .Row = e.row
                    .Col = e.col : If .Text.Length > 10 Then MsgBox(.Text, MsgBoxStyle.OkOnly, "결과보기")
                End If
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    '< add yjlee 2009-03-25 
    Private Sub sbDisplay_SpecialReport(ByVal rsBcno As String, ByVal rsTestCd As String, ByVal rsTnm As String)
        Dim sFn As String = "sbDisplay_SpecialReport"

        Try
            If rsBcno = "" Then Exit Sub
            If rsTestCd = "" Then Exit Sub

            'Special Report
            Dim strst As New STRST01

            strst.SpecialTestName = rsTnm
            strst.BcNo = rsBcno
            strst.TestCd = rsTestCd

            strst.Left = CType(Me.ParentForm.Left + (Me.ParentForm.Width - strst.Width) / 2, Integer)
            strst.Top = Me.ParentForm.Top + Ctrl.menuHeight

            strst.ShowDialog(Me)

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
    '> add yjlee 2009-03-25 

    Private Sub spdResult_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdResult.GotFocus

        With spdResult
            If .ActiveCol < .GetColFromID("unit") + 1 Then
                .Col = .GetColFromID("unit") + 1
            Else
                .Col = .ActiveCol
            End If

            If .ActiveRow < 1 Then
                .Row = 1
            Else
                .Row = .ActiveRow
            End If
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell
        End With
    End Sub

    Private Sub spdResult_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdResult.KeyDownEvent

        If e.keyCode = 13 Then
            spdResult_ClickEvent(spdResult, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spdResult.ActiveCol, spdResult.ActiveRow))
        End If

    End Sub

    Private Sub btnSearchR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchOR.Click, btnSearchR.Click

        Dim frm As Windows.Forms.Form

        frm = Ctrl.CheckFormObject(Me, CType(sender, Windows.Forms.Button).Text)

        Select Case CType(sender, Windows.Forms.Button).Text
            Case "결과조회(처방일자별)"
                If frm Is Nothing Then frm = New FGRV01(False, mbViewReportOnly)

            Case "결과조회(일일보고서)"
                If frm Is Nothing Then frm = New FGRV01(True, mbViewReportOnly)

        End Select

        frm.MdiParent = Me.MdiParent
        frm.WindowState = Windows.Forms.FormWindowState.Maximized
        If mbViewReportOnly Then
            frm.Text = CType(sender, Windows.Forms.Button).Text
        Else
            frm.Text = frm.Name + "ː" + CType(sender, Windows.Forms.Button).Text
        End If
        frm.Activate()
        frm.Show()

        With CType(frm, FGRV01)
            .Display_Result(Me.txtNo.Text, Me.dtpDayS.Text, Me.dtpDayE.Text)
        End With

        If mbViewReportOnly = False Then MdiTabControl.sbTabPageAdd(frm)

    End Sub

    Private Sub txtNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNo.Click
        Dim sFn As String = "txtNo_Click"

        Try
            txtNo.Focus()
            txtNo.SelectAll()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub spdSlip_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdSlip.ClickEvent

        With spdSlip
            .Row = e.row
            .Col = .GetColFromID("slipcd") : Dim sSlipCd As String = .Text

            sbDisplay_Test(sSlipCd)
        End With

    End Sub

    Private Sub spdResultGraph(ByVal riCol As Integer, ByVal riRow As Integer)
        Dim sFn As String = ""
        Dim strTBcNo As String = ""
        Dim objTable As New DataTable

        Try
            '< 신상정보 표시 
            With spdResult
                .Col = riCol
                .Row = riRow
                If .Col > .GetColFromID("unit") Then


                    If .CellTag = "" Then
                        axPatInfo.sbInit()
                    Else
                        strTBcNo = .CellTag.Split("/"c)(0)

#If DEBUG Then
                        txtDebug.Text = strTBcNo
#End If

                        objTable = LISAPP.APP_V.CommFn.fnGet_SpcInfo(strTBcNo)
                        If objTable.Rows.Count > 0 Then sbDisplay_SpcInfo(objTable)

                    End If

                    sbDisplay_Chart(riCol, riRow)
                End If
            End With
            '>



        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Chart(ByVal riCol As Integer, ByVal riRow As Integer)
        Dim sFn As String = ""

        Try
            Dim arlData As New ArrayList
            Dim intFxCol As Integer
            Dim strRef As String
            Dim strTcd As String
            Dim strTnm As String
            Dim strEdt As String

            Dim sDecimal As String

            With spdResult
                If riCol > .GetColFromID("unit") + 1 Then
                    intFxCol = riCol
                Else
                    intFxCol = .GetColFromID("unit") + 1
                End If

                .Row = riRow
                .Col = .GetColFromID("reftxt") : strRef = .Text

                sDecimal = fnGetDecimal(strRef)

                .Row = riRow
                .Col = .GetColFromID("testcd") : strTcd = .Text
                If strTcd.IndexOf("/") > 0 Then
                    strTcd = strTcd.Substring(0, strTcd.IndexOf("/"))
                End If

                .Row = riRow
                .Col = .GetColFromID("tnmd") : strTnm = .Text

                .Row = 0
                .Col = intFxCol : strEdt = .Text.Substring(0, 10)

                For intCol As Integer = intFxCol To .MaxCols

                    Dim strValue As String = ""
                    Dim strRstDt As String = ""

                    .Row = riRow
                    .Col = intCol : strValue = .Text.Replace(" H", "").Replace(" P", "").Replace(" L", "")

                    If IsNumeric(strValue) Then
                        .Row = 0
                        .Col = intCol : strRstDt = .Text

                        Dim clsChart As New AxAckResultViewer.ChartInfo
                        With clsChart
                            .sRstDte = strRstDt
                            .sRstVal = strValue
                        End With

                        arlData.Add(clsChart)
                    End If
                Next
                arlData.TrimToSize()
            End With

            If arlData.Count > 0 Then
                rstChart1.RegNo = Me.txtNo.Text
                rstChart1.ExamCd = strTcd
                rstChart1.ExamNm = strTnm
                rstChart1.EndDate = strEdt
                rstChart1.RefTxt = strRef
                rstChart1.msDecimal = sDecimal
                rstChart1.DataGridVisible = chkDDatagrid.Checked
                rstChart1.PointLabelVisible = chkDPoint.Checked
                rstChart1.AxisVisible = chkLabel.Checked

                '< add yjlee 2009-03-24 
                m_al_GrpData = arlData
                '> add yjlee 2009-03-24 

                rstChart1.Display_Chart(arlData, strTnm)
            Else
                m_al_GrpData = Nothing
                rstChart1.Clear()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub rstchart1_ChangeDblClick()

        Dim objFrm As Windows.Forms.Form

        With rstChart1
            If .RegNo = "" Then Exit Sub
            If .ExamCd = "" Then Exit Sub
            If .ExamNm = "" Then Exit Sub
            If .EndDate = "" Then Exit Sub
            If m_al_GrpData Is Nothing Then Exit Sub

            objFrm = New LISV.FGRV11_S01(.RegNo, .ExamCd, .ExamNm, .EndDate, .RefTxt, .msDecimal, m_al_GrpData)
        End With

        objFrm.Activate()
        objFrm.Show()

    End Sub

    Private Sub FGRV11_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtNo.Focus()

        SplitContainer1.SplitterDistance = 456
        pnlMicro.Visible = False
        rstChart1.BringToFront()
        spdResult.ReDraw = True
    End Sub


    '< add yjlee 2009-03-26 
    Private Sub cboTerm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTerm.SelectedIndexChanged
        Dim sFn As String = "cboTerm_SelectedIndexChanged"

        Try
            Dim iWidth As Single

            If Not IsNumeric(cboTerm.Text) Then
                iWidth = 8.5
            Else
                iWidth = CSng(cboTerm.Text)
            End If

            With spdResult
                For iCol As Integer = .GetColFromID("unit") + 1 To .MaxCols
                    .set_ColWidth(iCol, iWidth)
                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub
    '> yjlee 2009-03-25 

    Private Sub chkDPoint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDPoint.Click
        Dim sFn As String = "chkDPoint_Click"

        Try
            rstChart1.PointLabelVisible = chkDPoint.Checked
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub chkDDatagrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDDatagrid.Click
        Dim sFn As String = "chkDDatagrid_Click"

        Try
            rstChart1.DataGridVisible = chkDDatagrid.Checked
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub chkLabel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLabel.Click
        Dim sFn As String = ""

        Try
            rstChart1.AxisVisible = chkLabel.Checked
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbToggle(ByRef aoLabel As System.Windows.Forms.Label, ByRef aoButton As System.Windows.Forms.Button, _
                         ByVal aeGbn As enumToggle, Optional ByRef aoText As System.Windows.Forms.TextBox = Nothing)
        Dim sFn As String = ""

        Dim strText As String = ""
        Dim objForeColor As System.Drawing.Color
        Dim objBackColor As System.Drawing.Color

        Dim objButton As System.Windows.Forms.Button = CType(aoButton, System.Windows.Forms.Button)
        Dim strTag As String = CType(objButton.Tag, String)

        Try
            If strTag = "1" Then
                strText = "처방일자"
                objBackColor = System.Drawing.Color.DarkSlateBlue
                objForeColor = System.Drawing.Color.FromArgb(255, 255, 192)
                objButton.Tag = ""
            Else
                strText = "접수일자"
                objBackColor = System.Drawing.Color.DarkSeaGreen
                objForeColor = System.Drawing.Color.FromArgb(255, 255, 128)
                objButton.Tag = "1"
            End If

            With aoLabel
                .Text = strText
                .BackColor = objBackColor
                .ForeColor = objForeColor
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub


    Private Sub spdTest_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdTest.ClickEvent

        With spdTest
            .Row = e.row
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text

            sbDisplay_Data_rst(Me.txtNo.Text, sTestCd)

        End With
    End Sub

    Private Sub axItemSave_ListDblClick(ByVal rsItemCds As String, ByVal rsItemNms As String) Handles axItemSave.ListDblClick
        If rsItemCds <> "" Then
            Me.txtSelTest.Tag = rsItemCds + "^" + rsItemNms
            Me.txtSelTest.Text = rsItemNms.Replace("|", ",")
        End If
    End Sub
End Class
 