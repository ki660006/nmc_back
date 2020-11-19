'-- 처방의사별 검사건수
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports LISAPP.APP_T

Public Class FGT08
    Inherits System.Windows.Forms.Form

    Private miSelectKey As Integer = 0
    Private miMaxDiffDay As Integer = 31
    Private miMaxDiffMonth As Integer = 24
    Private miMaxDiffYear As Integer = 2

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdStatistics As AxFPSpreadADO.AxfpSpread
    Friend WithEvents split1 As System.Windows.Forms.Splitter
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button
    Friend WithEvents lblTnmd As System.Windows.Forms.Label
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cboTOrdSlip As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboOps As System.Windows.Forms.ComboBox
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents cboSpcCd As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As CButtonLib.CButton

    Private Sub sbDisplay_tordslip()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_OrdSlip_List()

            Me.cboTOrdSlip.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            Me.cboTOrdSlip.Items.Add("[ ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboTOrdSlip.Items.Add("[" + dt.Rows(ix).Item("tordslip").ToString + "] " + dt.Rows(ix).Item("tordslipnm").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_partslip()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List()

            Me.cboPartSlip.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            Me.cboPartSlip.Items.Add("[ ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )
        End Try
    End Sub

    Private Sub sbDisplay_spccd(ByVal rsTestCd As String)

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", "", "", "", "", rsTestCd, "")

            Me.cboSpcCd.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            Me.cboSpcCd.Items.Add("[ ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSpcCd.Items.Add("[" + dt.Rows(ix).Item("spccd").ToString + "] " + dt.Rows(ix).Item("spcnmd").ToString)
            Next

            If Me.cboSpcCd.Items.Count > 1 Then Me.cboSpcCd.SelectedIndex = 1

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )
        End Try
    End Sub

    Private Sub sbPrint_Data()

        Try
            Dim alPrint As New ArrayList

            With spdStatistics
                For iRow As Integer = 0 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("drcd") : Dim sDrcd As String = .Text
                    .Col = .GetColFromID("drnm") : Dim sDrNm As String = .Text

                    Dim sCount(0 To .MaxCols - 3) As String

                    For ix As Integer = .GetColFromID("drnm") + 1 To .MaxCols
                        .Col = ix : sCount(ix - .GetColFromID("drnm") - 1) = .Text
                    Next

                    Dim objPat As New FGT08_PRTINFO

                    With objPat
                        .DrCd = sDrcd
                        .DrNm = sDrNm
                        .COUNT = sCount
                    End With

                    alPrint.Add(objPat)
                Next
            End With

            If alPrint.Count > 0 Then
                Dim prt As New FGT08_PRINT

                prt.msTitle = Me.lblTnmd.Text + " 검사건수(처방의사별)"
                prt.msTitle_sub_left = "조회기간: " + Me.dtpDateS.Text + " ~ " + Me.dtpDateE.Text
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                prt.maPrtData = alPrint
                prt.sbPrint()

            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )

        End Try
    End Sub

    Private Sub sbDisplay_ST_Day(ByVal rsUsrIds As String)
        Try
            With Me.spdStatistics
                .ReDraw = False
                .MaxRows = 0

                For intIdx As Integer = 0 To Convert.ToInt32(DateDiff(DateInterval.Day, dtpDateS.Value, dtpDateE.Value))

                    Dim strDate As String = Format(DateAdd(DateInterval.Day, intIdx, dtpDateS.Value), "yyyy-MM-dd")

                    .MaxRows += 3
                    .Row = .MaxRows - 2
                    .Col = .GetColFromID("date") : .Text = strDate

                    .Row = .MaxRows - 2 : .Col = .GetColFromID("iogbn") : .Text = "전체"
                    .Row = .MaxRows - 1 : .Col = .GetColFromID("iogbn") : .Text = "외래"
                    .Row = .MaxRows - 0 : .Col = .GetColFromID("iogbn") : .Text = "입원"

                    .AddCellSpan(.GetColFromID("date"), .MaxRows - 2, .GetColFromID("date"), 3)

                    Dim dt As DataTable = (New SrhFn).fnGet_Coll_Statistics("D", strDate, rsUsrIds)

                    If dt.Rows.Count > 0 Then

                        For i As Integer = 0 To dt.Rows.Count - 1
                            For j As Integer = 0 To dt.Columns.Count - 1
                                Dim iCol As Integer = 0
                                iCol = 0
                                iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                                If iCol > 0 And .GetColFromID("iogbn") < iCol Then
                                    .Col = iCol

                                    If dt.Rows(i).Item("iogbn").ToString = "I" Then
                                        .Row = .MaxRows - 0
                                    Else
                                        .Row = .MaxRows - 1
                                    End If

                                    .Text = dt.Rows(i).Item(j).ToString
                                End If
                            Next
                        Next

                        For intCol As Integer = .GetColFromID("iogbn") + 1 To .MaxCols
                            Dim intTot As Long = 0
                            For intRow As Integer = .MaxRows - 1 To .MaxRows - 0
                                .Row = intRow
                                .Col = intCol : intTot += Convert.ToInt32(IIf(.Text = "", "0", .Text.Replace(",", "")).ToString)
                            Next

                            .Row = .MaxRows - 2
                            .Col = intCol : .Text = intTot.ToString
                        Next
                    End If
                Next

                If .MaxRows > 4 Then
                    .MaxRows += 3

                    .Row = .MaxRows - 2
                    .Col = .GetColFromID("date") : .Text = "전  체"

                    .Row = .MaxRows - 2 : .Col = .GetColFromID("iogbn") : .Text = "전체"
                    .Row = .MaxRows - 1 : .Col = .GetColFromID("iogbn") : .Text = "외래"
                    .Row = .MaxRows - 0 : .Col = .GetColFromID("iogbn") : .Text = "입원"

                    .AddCellSpan(.GetColFromID("date"), .MaxRows - 2, .GetColFromID("date"), 3)

                    For intCol As Integer = .GetColFromID("iogbn") + 1 To .MaxCols
                        Dim lngTot_I As Long = 0, lngTot_O As Long = 0, lngTot_A As Long = 0

                        For intRow As Integer = 1 To .MaxRows Step 3
                            .Row = intRow

                            .Row = intRow + 0 : .Col = intCol : lngTot_A += Convert.ToInt64(IIf(.Text = "", "0", .Text))
                            .Row = intRow + 1 : .Col = intCol : lngTot_O += Convert.ToInt64(IIf(.Text = "", "0", .Text))
                            .Row = intRow + 2 : .Col = intCol : lngTot_I += Convert.ToInt64(IIf(.Text = "", "0", .Text))
                        Next

                        .Row = .MaxRows - 2 : .Col = intCol : .Text = lngTot_A.ToString
                        .Row = .MaxRows - 1 : .Col = intCol : .Text = lngTot_O.ToString
                        .Row = .MaxRows - 0 : .Col = intCol : .Text = lngTot_I.ToString
                    Next


                End If
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_ST_Month(ByVal rsUsrIds As String)
        Try
            With Me.spdStatistics
                .ReDraw = False
                .MaxRows = 0

                For intIdx As Integer = 0 To Convert.ToInt32(DateDiff(DateInterval.Month, CDate(dtpDateS.Text + "-01"), CDate(dtpDateE.Text + "-01")))

                    Dim strDate As String = Format(DateAdd(DateInterval.Month, intIdx, CDate(dtpDateS.Text + "-01")), "yyyy-MM")

                    .MaxRows += 3
                    .Row = .MaxRows - 2
                    .Col = .GetColFromID("date") : .Text = strDate

                    .Row = .MaxRows - 2 : .Col = .GetColFromID("iogbn") : .Text = "전체"
                    .Row = .MaxRows - 1 : .Col = .GetColFromID("iogbn") : .Text = "외래"
                    .Row = .MaxRows - 0 : .Col = .GetColFromID("iogbn") : .Text = "입원"

                    .AddCellSpan(.GetColFromID("date"), .MaxRows - 2, .GetColFromID("date"), 3)

                    Dim dt As DataTable = (New SrhFn).fnGet_Coll_Statistics("M", strDate, rsUsrIds)

                    If dt.Rows.Count > 0 Then
                        .Row = 1 : .Col = .GetColFromID("date") : .Text = dtpDateS.Text

                        For i As Integer = 0 To dt.Rows.Count - 1
                            For j As Integer = 0 To dt.Columns.Count - 1
                                Dim iCol As Integer = 0
                                iCol = 0
                                iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                                If iCol > 0 And .GetColFromID("iogbn") < iCol Then
                                    .Col = iCol

                                    If dt.Rows(i).Item("iogbn").ToString = "I" Then
                                        .Row = .MaxRows - 0
                                    Else
                                        .Row = .MaxRows - 1
                                    End If

                                    .Text = dt.Rows(i).Item(j).ToString
                                End If
                            Next
                        Next

                        For intCol As Integer = .GetColFromID("iogbn") + 1 To .MaxCols
                            Dim intTot As Long = 0
                            For intRow As Integer = .MaxRows - 1 To .MaxRows - 0
                                .Row = intRow
                                .Col = intCol : intTot += Convert.ToInt32(IIf(.Text = "", "0", .Text.Replace(",", "")).ToString)
                            Next

                            .Row = .MaxRows - 2
                            .Col = intCol : .Text = intTot.ToString
                        Next
                    End If
                Next

                If .MaxRows > 4 Then
                    .MaxRows += 3

                    .Row = .MaxRows - 2
                    .Col = .GetColFromID("date") : .Text = "전  체"

                    .Row = .MaxRows - 2 : .Col = .GetColFromID("iogbn") : .Text = "전체"
                    .Row = .MaxRows - 1 : .Col = .GetColFromID("iogbn") : .Text = "외래"
                    .Row = .MaxRows - 0 : .Col = .GetColFromID("iogbn") : .Text = "입원"

                    .AddCellSpan(.GetColFromID("date"), .MaxRows - 2, .GetColFromID("date"), 3)

                    For intCol As Integer = .GetColFromID("iogbn") + 1 To .MaxCols
                        Dim lngTot_I As Long = 0, lngTot_O As Long = 0, lngTot_A As Long = 0

                        For intRow As Integer = 1 To .MaxRows Step 3
                            .Row = intRow

                            .Row = intRow + 0 : .Col = intCol : lngTot_A += Convert.ToInt64(IIf(.Text = "", "0", .Text))
                            .Row = intRow + 1 : .Col = intCol : lngTot_O += Convert.ToInt64(IIf(.Text = "", "0", .Text))
                            .Row = intRow + 2 : .Col = intCol : lngTot_I += Convert.ToInt64(IIf(.Text = "", "0", .Text))
                        Next

                        .Row = .MaxRows - 2 : .Col = intCol : .Text = lngTot_A.ToString
                        .Row = .MaxRows - 1 : .Col = intCol : .Text = lngTot_O.ToString
                        .Row = .MaxRows - 0 : .Col = intCol : .Text = lngTot_I.ToString
                    Next


                End If
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_ST_Year(ByVal rsUsrIds As String)

    End Sub

    Private Function fnDisplayStatistics() As Boolean

        Dim bReturn As Boolean = False

        Try
            Dim sStType As String = "", sDMYGbn As String = "", sDT1 As String = "", sDT2 As String = "", sADNGbn As String = ""
            Dim a_sDMY As String() = Nothing
            Dim iDMYDiff As Integer = 0, iSum As Integer = 0, iCnt As Integer = 0


            If Me.dtpDateS.Value > Me.dtpDateE.Value Then
                MsgBox("날짜구간 설정이 잘못되었습니다. 시작을 끝보다 작거나 같게 설정하십시요!!")

                Return False
            End If

            '> 일별/월별/연별 구분
            If Me.rdoDay.Checked Then
                '일별
                sDT1 = Me.dtpDateS.Value.ToString("yyyy-MM-dd")
                sDT2 = Me.dtpDateE.Value.ToString("yyyy-MM-dd")

                sDMYGbn = "D"

                iDMYDiff = CInt(DateDiff(DateInterval.Day, CDate(sDT1), CDate(sDT2)))

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Day, i - 1, CDate(sDT1)).ToShortDateString
                Next


                If a_sDMY.Length > miMaxDiffDay Then
                    MsgBox("일별로는 " + miMaxDiffDay.ToString + "개의 구간 까지만 검사통계를 조회할 수 있습니다. 날짜구간 또는 시간대를 다시 설정하십시요!!")

                    Return False
                End If

            ElseIf Me.rdoMonth.Checked Then
                '월별
                sDT1 = Me.dtpDateS.Value.ToString("yyyy-MM")
                sDT2 = Me.dtpDateE.Value.ToString("yyyy-MM")

                sDMYGbn = "M"

                iDMYDiff = CInt(DateDiff(DateInterval.Month, CDate(sDT1), CDate(sDT2)))

                If iDMYDiff > miMaxDiffMonth Then
                    MsgBox("월별로는 " + miMaxDiffMonth.ToString + "개월 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Month, i - 1, CDate(sDT1)).ToString("yyyy-MM")
                Next

            ElseIf Me.rdoYear.Checked Then
                '연별
                sDT1 = Me.dtpDateS.Value.ToString("yyyy")
                sDT2 = Me.dtpDateE.Value.ToString("yyyy")

                sDMYGbn = "Y"

                iDMYDiff = CInt(DateDiff(DateInterval.Year, CDate(sDT1 + "-01"), CDate(sDT2 + "-12")))

                If iDMYDiff > miMaxDiffMonth Then
                    MsgBox("연별로는 " + miMaxDiffYear.ToString + "년 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Year, i - 1, CDate(sDT1 + "-01")).ToString("yyyy")
                Next

            End If

            sbInitialize_spdStatistics(a_sDMY)

            Dim dt As DataTable
            Dim iCol As Integer = 0

            dt = (New SrhFn).fnGet_Test_Statistics_dr(sDMYGbn, a_sDMY, Me.dtpDateS.Text, Me.dtpDateE.Text, Me.txtTestCd.Text, Ctrl.Get_Code(Me.cboSpcCd))

            If dt.Rows.Count > 0 Then
                With Me.spdStatistics
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString
                            End If
                        Next
                    Next

                    .MaxRows = .MaxRows + 1

                    .Col = .GetColFromID("drnm") : .Row = .MaxRows : .Text = "합 계"

                    For i As Integer = 0 To a_sDMY.Length
                        iSum = 0

                        For j As Integer = 1 To .MaxRows - 1
                            .Col = 3 + i : .Row = j : iCnt = CType(Val(.Text), Integer)
                            iSum += iCnt
                        Next

                        .Col = 3 + i : .Row = .MaxRows : .Text = CType(iSum, String)
                    Next

                    .ReDraw = True
                End With
            Else
                spdStatistics.MaxRows = 0
            End If


            Return True
        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )

            Return False
        End Try
    End Function

    Private Sub sbInitialize()

        Try
            miSelectKey = 1
            Dim sCurSysDate As String = ""

            Me.rdoDay.Checked = True

            sCurSysDate = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")
            Me.dtpDateS.CustomFormat = "yyyy-MM-dd" : Me.dtpDateS.Value = CType(sCurSysDate & " 00:00:00", Date)
            Me.dtpDateE.CustomFormat = "yyyy-MM-dd" : Me.dtpDateE.Value = CType(sCurSysDate & " 00:00:00", Date)

            sbDisplay_tordslip()
            sbDisplay_partslip()


        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbInitialize_spdStatistics(ByVal ra_sDMY As String())

        Try
            With Me.spdStatistics
                .ReDraw = False

                '코드, 의사명, Total
                .MaxCols = ra_sDMY.Length + 3


                For ix As Integer = 0 To ra_sDMY.Length - 1
                    .Col = 4 + ix : .Row = 0 : .Text = ra_sDMY(ix) : .ColID = "c" + (ix + 1).ToString : .set_ColWidth(.GetColFromID("c" + (ix + 1).ToString), 9)
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )

        Finally
            miSelectKey = 0

        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        sbInitialize()
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
    Friend WithEvents tclStatistics As System.Windows.Forms.TabControl
    Friend WithEvents tpgVar As System.Windows.Forms.TabPage
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDay As System.Windows.Forms.RadioButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rdoYear As System.Windows.Forms.RadioButton

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGT08))
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
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.tclStatistics = New System.Windows.Forms.TabControl
        Me.tpgVar = New System.Windows.Forms.TabPage
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.spdStatistics = New AxFPSpreadADO.AxfpSpread
        Me.split1 = New System.Windows.Forms.Splitter
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboTOrdSlip = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboOps = New System.Windows.Forms.ComboBox
        Me.txtFilter = New System.Windows.Forms.TextBox
        Me.btnCdHelp_test = New System.Windows.Forms.Button
        Me.lblTnmd = New System.Windows.Forms.Label
        Me.cboSpcCd = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboPartSlip = New System.Windows.Forms.ComboBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoYear = New System.Windows.Forms.RadioButton
        Me.rdoMonth = New System.Windows.Forms.RadioButton
        Me.rdoDay = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExcel = New CButtonLib.CButton
        Me.btnSearch = New CButtonLib.CButton
        Me.btnPrint = New CButtonLib.CButton
        Me.tclStatistics.SuspendLayout()
        Me.tpgVar.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tclStatistics
        '
        Me.tclStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tclStatistics.Controls.Add(Me.tpgVar)
        Me.tclStatistics.Location = New System.Drawing.Point(0, 0)
        Me.tclStatistics.Name = "tclStatistics"
        Me.tclStatistics.SelectedIndex = 0
        Me.tclStatistics.Size = New System.Drawing.Size(1016, 660)
        Me.tclStatistics.TabIndex = 0
        '
        'tpgVar
        '
        Me.tpgVar.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgVar.Controls.Add(Me.Panel4)
        Me.tpgVar.Controls.Add(Me.split1)
        Me.tpgVar.Controls.Add(Me.Panel3)
        Me.tpgVar.Location = New System.Drawing.Point(4, 21)
        Me.tpgVar.Name = "tpgVar"
        Me.tpgVar.Size = New System.Drawing.Size(1008, 635)
        Me.tpgVar.TabIndex = 0
        Me.tpgVar.Text = "조회조건설정"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(316, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(692, 635)
        Me.Panel4.TabIndex = 128
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.spdStatistics)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(692, 645)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'spdStatistics
        '
        Me.spdStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdStatistics.DataSource = Nothing
        Me.spdStatistics.Location = New System.Drawing.Point(3, 10)
        Me.spdStatistics.Name = "spdStatistics"
        Me.spdStatistics.OcxState = CType(resources.GetObject("spdStatistics.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStatistics.Size = New System.Drawing.Size(692, 634)
        Me.spdStatistics.TabIndex = 0
        '
        'split1
        '
        Me.split1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.split1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.split1.Location = New System.Drawing.Point(311, 0)
        Me.split1.MinSize = 224
        Me.split1.Name = "split1"
        Me.split1.Size = New System.Drawing.Size(5, 635)
        Me.split1.TabIndex = 127
        Me.split1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.cboTOrdSlip)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.cboOps)
        Me.Panel3.Controls.Add(Me.txtFilter)
        Me.Panel3.Controls.Add(Me.btnCdHelp_test)
        Me.Panel3.Controls.Add(Me.lblTnmd)
        Me.Panel3.Controls.Add(Me.cboSpcCd)
        Me.Panel3.Controls.Add(Me.Label11)
        Me.Panel3.Controls.Add(Me.txtTestCd)
        Me.Panel3.Controls.Add(Me.Label10)
        Me.Panel3.Controls.Add(Me.cboPartSlip)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.dtpDateE)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.Panel1)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.dtpDateS)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(311, 635)
        Me.Panel3.TabIndex = 24
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(3, 94)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 20)
        Me.Label2.TabIndex = 213
        Me.Label2.Text = "검 사 명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTOrdSlip
        '
        Me.cboTOrdSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTOrdSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTOrdSlip.Location = New System.Drawing.Point(96, 50)
        Me.cboTOrdSlip.Margin = New System.Windows.Forms.Padding(0)
        Me.cboTOrdSlip.Name = "cboTOrdSlip"
        Me.cboTOrdSlip.Size = New System.Drawing.Size(211, 20)
        Me.cboTOrdSlip.TabIndex = 212
        Me.cboTOrdSlip.Tag = "TCDGBN_01"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(3, 50)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 20)
        Me.Label1.TabIndex = 211
        Me.Label1.Text = "처방슬립"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboOps
        '
        Me.cboOps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOps.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboOps.FormattingEnabled = True
        Me.cboOps.Items.AddRange(New Object() {"", "LIKE *", "* LIKE *", "* LIKE"})
        Me.cboOps.Location = New System.Drawing.Point(96, 94)
        Me.cboOps.Name = "cboOps"
        Me.cboOps.Size = New System.Drawing.Size(83, 20)
        Me.cboOps.TabIndex = 210
        '
        'txtFilter
        '
        Me.txtFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtFilter.Location = New System.Drawing.Point(180, 94)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(127, 21)
        Me.txtFilter.TabIndex = 208
        '
        'btnCdHelp_test
        '
        Me.btnCdHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_test.Image = CType(resources.GetObject("btnCdHelp_test.Image"), System.Drawing.Image)
        Me.btnCdHelp_test.Location = New System.Drawing.Point(281, 116)
        Me.btnCdHelp_test.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_test.Name = "btnCdHelp_test"
        Me.btnCdHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_test.TabIndex = 190
        Me.btnCdHelp_test.UseVisualStyleBackColor = True
        '
        'lblTnmd
        '
        Me.lblTnmd.BackColor = System.Drawing.Color.Thistle
        Me.lblTnmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTnmd.Location = New System.Drawing.Point(149, 116)
        Me.lblTnmd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTnmd.Name = "lblTnmd"
        Me.lblTnmd.Size = New System.Drawing.Size(131, 21)
        Me.lblTnmd.TabIndex = 189
        '
        'cboSpcCd
        '
        Me.cboSpcCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSpcCd.Location = New System.Drawing.Point(96, 139)
        Me.cboSpcCd.Margin = New System.Windows.Forms.Padding(0)
        Me.cboSpcCd.Name = "cboSpcCd"
        Me.cboSpcCd.Size = New System.Drawing.Size(211, 20)
        Me.cboSpcCd.TabIndex = 188
        Me.cboSpcCd.Tag = "TCDGBN_01"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(3, 139)
        Me.Label11.Margin = New System.Windows.Forms.Padding(0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(92, 20)
        Me.Label11.TabIndex = 187
        Me.Label11.Text = "검체코드"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTestCd
        '
        Me.txtTestCd.Location = New System.Drawing.Point(96, 116)
        Me.txtTestCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTestCd.MaxLength = 7
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(52, 21)
        Me.txtTestCd.TabIndex = 186
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(3, 116)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 21)
        Me.Label10.TabIndex = 185
        Me.Label10.Text = "검사항목"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboPartSlip
        '
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPartSlip.Location = New System.Drawing.Point(96, 72)
        Me.cboPartSlip.Margin = New System.Windows.Forms.Padding(0)
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(211, 20)
        Me.cboPartSlip.TabIndex = 184
        Me.cboPartSlip.Tag = "TCDGBN_01"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Black
        Me.Label18.Location = New System.Drawing.Point(3, 72)
        Me.Label18.Margin = New System.Windows.Forms.Padding(0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(92, 20)
        Me.Label18.TabIndex = 183
        Me.Label18.Text = "검사분야"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(195, 29)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 16)
        Me.Label5.TabIndex = 130
        Me.Label5.Text = "~"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateE
        '
        Me.dtpDateE.CustomFormat = "yyyy-MM-dd"
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateE.Location = New System.Drawing.Point(215, 27)
        Me.dtpDateE.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(92, 21)
        Me.dtpDateE.TabIndex = 129
        Me.dtpDateE.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, 4)
        Me.Label3.Margin = New System.Windows.Forms.Padding(0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 21)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "일별/월별구분"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.rdoYear)
        Me.Panel1.Controls.Add(Me.rdoMonth)
        Me.Panel1.Controls.Add(Me.rdoDay)
        Me.Panel1.Location = New System.Drawing.Point(96, 4)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(109, 21)
        Me.Panel1.TabIndex = 25
        '
        'rdoYear
        '
        Me.rdoYear.BackColor = System.Drawing.Color.Beige
        Me.rdoYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoYear.Location = New System.Drawing.Point(112, 1)
        Me.rdoYear.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoYear.Name = "rdoYear"
        Me.rdoYear.Size = New System.Drawing.Size(48, 18)
        Me.rdoYear.TabIndex = 13
        Me.rdoYear.Text = "연별"
        Me.rdoYear.UseVisualStyleBackColor = False
        Me.rdoYear.Visible = False
        '
        'rdoMonth
        '
        Me.rdoMonth.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoMonth.Location = New System.Drawing.Point(60, 1)
        Me.rdoMonth.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoMonth.Name = "rdoMonth"
        Me.rdoMonth.Size = New System.Drawing.Size(48, 18)
        Me.rdoMonth.TabIndex = 12
        Me.rdoMonth.Text = "월별"
        Me.rdoMonth.UseVisualStyleBackColor = False
        '
        'rdoDay
        '
        Me.rdoDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoDay.Checked = True
        Me.rdoDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDay.Location = New System.Drawing.Point(4, 1)
        Me.rdoDay.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoDay.Name = "rdoDay"
        Me.rdoDay.Size = New System.Drawing.Size(48, 18)
        Me.rdoDay.TabIndex = 11
        Me.rdoDay.TabStop = True
        Me.rdoDay.Text = "일별"
        Me.rdoDay.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(3, 27)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 21)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "접수일자"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateS
        '
        Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateS.Location = New System.Drawing.Point(96, 27)
        Me.dtpDateS.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(92, 21)
        Me.dtpDateS.TabIndex = 28
        Me.dtpDateS.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
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
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(905, 661)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(107, 25)
        Me.btnExit.TabIndex = 207
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4859813!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(797, 661)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 206
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExcel.Location = New System.Drawing.Point(689, 661)
        Me.btnExcel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(107, 25)
        Me.btnExcel.TabIndex = 205
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems4
        Me.btnSearch.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSearch.Corners.All = CType(6, Short)
        Me.btnSearch.Corners.LowerLeft = CType(6, Short)
        Me.btnSearch.Corners.LowerRight = CType(6, Short)
        Me.btnSearch.Corners.UpperLeft = CType(6, Short)
        Me.btnSearch.Corners.UpperRight = CType(6, Short)
        Me.btnSearch.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSearch.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSearch.FocalPoints.CenterPtX = 0.4859813!
        Me.btnSearch.FocalPoints.CenterPtY = 0.16!
        Me.btnSearch.FocalPoints.FocusPtX = 0.0!
        Me.btnSearch.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker8
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(580, 661)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSearch.SideImage = Nothing
        Me.btnSearch.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSearch.Size = New System.Drawing.Size(107, 25)
        Me.btnSearch.TabIndex = 204
        Me.btnSearch.Text = "통계조회"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSearch.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems5
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker10
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(470, 662)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(107, 25)
        Me.btnPrint.TabIndex = 208
        Me.btnPrint.Text = "출  력(F5)"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGT08
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 690)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnExcel)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.tclStatistics)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGT08"
        Me.Text = "처방의사별 검사통계"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tclStatistics.ResumeLayout(False)
        Me.tpgVar.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FGT08_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        With spdStatistics
            .ReDraw = False

            .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

            .MaxRows = .MaxRows + 1
            .InsertRows(1, 1)

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = 0 : sBuf = .Text
                .Col = i : .Row = 1 : .Text = sBuf
            Next

            If .ExportToExcel("statistics.xls", "Statistics", "") Then
                Process.Start("statistics.xls")
            End If

            .DeleteRows(1, 1)
            .MaxRows -= 1

            .ReDraw = True
        End With
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.ClickButtonArea

        Try
            Me.Close()

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )

        End Try
    End Sub

    Private Sub btnSearch_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            If Me.txtTestCd.Text = "" Then
                MsgBox("검사코드가 입력되지 않았습니다.!!", MsgBoxStyle.OkOnly, Me.Text)
                Return
            End If

            fnDisplayStatistics()

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )
        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub rdoDayMonthYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDay.CheckedChanged, rdoMonth.CheckedChanged, rdoYear.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDay.Checked Then
                Me.dtpDateS.CustomFormat = "yyyy-MM-dd"
                Me.dtpDateE.CustomFormat = "yyyy-MM-dd"

            ElseIf Me.rdoMonth.Checked Then
                Me.dtpDateS.CustomFormat = "yyyy-MM"
                Me.dtpDateE.CustomFormat = "yyyy-MM"

            ElseIf Me.rdoYear.Checked Then
                '연별 체크 시
                Me.dtpDateS.CustomFormat = "yyyy"
                Me.dtpDateE.CustomFormat = "yyyy"
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )
        End Try
    End Sub

    Private Sub FGT06_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)

        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdStatistics.MaxRows = 0
    End Sub

    Private Sub dtpDateS_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDateS.LostFocus

        If Me.dtpDateE.Value < Me.dtpDateS.Value Then Me.dtpDateE.Value = Me.dtpDateS.Value

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        sbPrint_Data()
    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Try
            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim sFilter As String = ""

            If Me.txtFilter.Text <> "" Then
                Select Case Me.cboOps.Text
                    Case "LIKE *" : sFilter = "tnmd LIKE '" + Me.txtFilter.Text + "%'"
                    Case "* LIKE *" : sFilter = "tnmd LIKE '%" + Me.txtFilter.Text + "%'"
                    Case "* LIKE" : sFilter = "tnmd LIKE '%" + Me.txtFilter.Text + "'"
                End Select

                If sFilter <> "" Then Me.txtTestCd.Text = "" : Me.lblTnmd.Text = ""

            ElseIf Me.txtTestCd.Text <> "" Then
                sFilter = "testcd LIKE '" + Me.txtTestCd.Text + "%'"
            End If

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list(Ctrl.Get_Code(Me.cboPartSlip), "", "", "", "", "", Ctrl.Get_Code(Me.cboTOrdSlip), sFilter)
            Dim a_dr As DataRow() = dt.Select("tcdgbn IN ('S', 'P', 'B', 'G') AND ordhide = '0'", "")

            dt = Fn.ChangeToDataTable(a_dr)
            objHelp.FormText = "검사목록"

            objHelp.MaxRows = 15
            objHelp.OnRowReturnYN = True

            objHelp.AddField("tnmd", "검사명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtTestCd.Text = alList.Item(0).ToString.Split("|"c)(1)
                Me.lblTnmd.Text = alList.Item(0).ToString.Split("|"c)(0)

                sbDisplay_spccd(Me.txtTestCd.Text)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )
        End Try
    End Sub

    Private Sub txtTestCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtTestCd.KeyDown, txtFilter.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        If CType(sender, System.Windows.Forms.TextBox).Text = "" Then
            If CType(sender, System.Windows.Forms.TextBox).Name.ToUpper = "TXTESTCD" Then Me.lblTnmd.Text = "" : Return
            Return
        End If

        btnCdHelp_test_Click(Nothing, Nothing)

    End Sub
End Class

Public Class FGT08_PRTINFO
    Public DrNm As String = ""
    Public DrCd As String = ""
    Public COUNT() As String
End Class


Public Class FGT08_PRINT
    Private Const msFile As String = "File : FGT06.vb, Class : T01" & vbTab

    Private miPageNo As Integer = 0
    Private miRow_Cur As Integer = 0
    Private miCol_Cur As Integer = 0

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 10

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msTitle As String = ""
    Public msTitle_sub_left As String = ""

    Public maPrtData As ArrayList
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd hh:mm")
    Public msTitle_sub_right_1 As String = ""

    Private prtR As New PrintDocument


    Public Sub sbPrint_Preview()
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Sub

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(boolean)"

        'Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = True
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miRow_Cur = 1
        miCol_Cur = 0
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim sgTop As Single = 0, sgPosY As Single = 0
        Dim sgPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 35
        msgLeft = 5
        msgTop = 30

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sgPrtH = CSng(fnt_Body.GetHeight(e.Graphics) * 1.3)

        Dim rect As New Drawing.RectangleF
        Dim iLine As Integer = 0

        For ix1 As Integer = miRow_Cur To maPrtData.Count - 1
            If sgPosY = 0 Then
                sgTop = fnPrtTitle(e)
                sgPosY = sgTop
            Else
                sgPosY += sgPrtH
            End If

            '-- 코드
            rect = New Drawing.RectangleF(msgPosX(0), sgPosY, msgPosX(1) - msgPosX(0), sgPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix1), FGT08_PRTINFO).DrCd, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 의사명
            rect = New Drawing.RectangleF(msgPosX(1), sgPosY, msgPosX(2) - msgPosX(1), sgPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix1), FGT08_PRTINFO).DrNm, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 건수
            Dim iCol As Integer = miCol_Cur - 2
            For ix2 As Integer = 2 To msgPosX.Length - 2
                If CType(maPrtData(0), FGT08_PRTINFO).COUNT.Length - 1 < iCol + ix2 Then Exit For

                rect = New Drawing.RectangleF(msgPosX(ix2), sgPosY, msgPosX(ix2 + 1) - msgPosX(ix2), sgPrtH)
                e.Graphics.DrawString(CType(maPrtData.Item(ix1), FGT08_PRTINFO).COUNT(iCol + ix2) + " ", fnt_Body, Drawing.Brushes.Black, rect, sf_r)
            Next

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY + sgPrtH, msgWidth, sgPosY + sgPrtH)

            iLine += 1

            If msgHeight - sgPrtH < sgPosY + sgPrtH Then
                Exit For
            End If

        Next

        '-- 세로
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sgTop, msgPosX(ix), sgPosY + sgPrtH)
        Next

        miPageNo += 1
        miCol_Cur += msgPosX.Length - 3

        If CType(maPrtData.Item(0), FGT08_PRTINFO).COUNT.Length <= miCol_Cur Then
            miRow_Cur += iLine
        End If

        If miRow_Cur < maPrtData.Count Or miCol_Cur < CType(maPrtData.Item(0), FGT08_PRTINFO).COUNT.Length Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sgPrtH As Single = 0
        Dim sgPosY As Single = 0.0

        Dim sgPosX(0 To 2) As Single

        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(0) + 100
        sgPosX(2) = sgPosX(1) + 60

        For ix As Integer = 0 To CType(maPrtData(0), FGT08_PRTINFO).COUNT.Length - 1
            ReDim Preserve sgPosX(ix + 3)

            sgPosX(ix + 3) = sgPosX(ix + 2) + 63

            If sgPosX(ix + 3) + 63 > msgWidth Then Exit For
        Next
        msgWidth = sgPosX(sgPosX.Length - 1)
        msgPosX = sgPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sgPrtH = CSng(fnt_Title.GetHeight(e.Graphics) * (3 / 2))

        Dim rect As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sgPrtH)

        '-- 출력정보
        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            rect = New Drawing.RectangleF(msgLeft, sgPosY + 65, msgWidth, sgPrtH)
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, rect, sf_l)
        End If

        '-- 타이틀
        rect = New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sgPrtH)
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rect, sf_c)

        sgPosY = CSng(msgTop + sgPrtH * 2)
        sgPrtH = CSng(fnt_Head.GetHeight(e.Graphics))

        '-- sub title
        rect = New Drawing.RectangleF(msgLeft, sgPosY, msgWidth, sgPrtH)
        e.Graphics.DrawString(msTitle_sub_left, fnt_Head, Drawing.Brushes.Black, rect, sf_l)

        '-- 출력일시
        rect = New Drawing.RectangleF(msgLeft, sgPosY - 15, msgWidth, sgPrtH)
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, rect, sf_l)
        sgPosY += sgPrtH + sgPrtH / 2

        rect = New Drawing.RectangleF(sgPosX(0), sgPosY, sgPosX(1) - sgPosX(0), sgPrtH * 2)
        e.Graphics.DrawString("코드", fnt_Head, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(1), sgPosY, sgPosX(2) - sgPosX(1), sgPrtH * 2)
        e.Graphics.DrawString("의사명", fnt_Head, Drawing.Brushes.Black, rect, sf_c)

        For ix As Integer = 2 To sgPosX.Length - 2

            'If ix = sgPosX.Length - 2 Then MsgBox("A")

            If CType(maPrtData(0), FGT08_PRTINFO).COUNT.Length - 1 < miCol_Cur + (ix - 2) Then Exit For

            Dim sTmp As String = CType(maPrtData(0), FGT08_PRTINFO).COUNT(miCol_Cur + (ix - 2))
            If sTmp Is Nothing Then Exit For
            If sTmp.Length = 10 Then sTmp = sTmp.Substring(2)

            rect = New Drawing.RectangleF(sgPosX(ix), sgPosY, sgPosX(ix + 1) - sgPosX(ix), sgPrtH * 2)
            e.Graphics.DrawString(sTmp, fnt_Head, Drawing.Brushes.Black, rect, sf_c)

        Next

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY - sgPrtH / 2, msgWidth, sgPosY - sgPrtH / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY + sgPrtH * 2, msgWidth, sgPosY + sgPrtH * 2)

        For ix As Integer = 0 To sgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(ix), sgPosY - sgPrtH / 2, sgPosX(ix), sgPosY + sgPrtH * 2)
        Next

        rect = New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth, sgPrtH)
        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Head, Drawing.Brushes.Black, rect, sf_r)


        Return sgPosY + sgPrtH * 2

    End Function

End Class