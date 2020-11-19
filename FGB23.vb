' 혈액반납폐기율

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB23
    Private mobjDAF As New LISAPP.APP_F_COMCD
    Private msMonth As String = ""
    Private msYear As String = ""

    Private Sub FGB23_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB23_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB23_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        ' 화면 오픈시 초기화
        Me.spdDayList.MaxRows = 0
        Me.spdMonthList.MaxRows = 0
        Me.spdYearList.MaxRows = 0
        Me.spdMonType.MaxRows = 0
        Me.spdYearType.MaxRows = 0

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(Me.spdDayList)
        DS_SpreadDesige.sbInti(Me.spdMonthList)
        DS_SpreadDesige.sbInti(Me.spdYearList)
        DS_SpreadDesige.sbInti(Me.spdMonType)
        DS_SpreadDesige.sbInti(Me.spdYearType)

        sb_SetComboDt()

        sb_SetHeader("D"c, Format(dtpDate0.Value, "yyyy-MM"))
        msMonth = Format(dtpDate0.Value, "yyyyMM")
        msYear = Format(dtpDate0.Value, "yyyy")
    End Sub

    Public Sub sb_SetComboDt(Optional ByVal rsUsDt As String = "", Optional ByVal rsUeDt As String = "")
        Dim sFn As String = "sb_SetComboDt"
        ' 콤보 데이터 생성
        Try
            Me.cboGbn.Items.Add("[1] 반납")
            Me.cboGbn.Items.Add("[2] 폐기")

            Me.cboType.Items.Add("[1] 진료과별")
            Me.cboType.Items.Add("[2] 성분제제별")

            If rsUsDt = "" Then rsUsDt = "20000101"
            If rsUeDt = "" Then rsUeDt = "30000101"

            Dim dt As DataTable = mobjDAF.GetComCdInfo("")

            Me.cboComCd.Items.Clear()
            Me.cboComCd.Items.Add("[ALL] 전체")
            If dt.Rows.Count > 0 Then
                With Me.cboComCd
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add(dt.Rows(i).Item("comnmd").ToString)
                    Next
                End With
            Else
                Exit Sub
            End If

            Me.cboGbn.SelectedIndex = 0
            Me.cboType.SelectedIndex = 0
            Me.cboComCd.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdDayList.MaxRows = 0
        Me.spdMonthList.MaxRows = 0
        Me.spdYearList.MaxRows = 0
        Me.spdMonType.MaxRows = 0
        Me.spdYearType.MaxRows = 0
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim dt As New DataTable
            Dim sGbn As String = ""
            Dim sGroup As String = ""
            Dim sComcd As String = ""
            Dim dateiff As Integer = 0
            Dim a_date As String() = Nothing


            sGbn = Ctrl.Get_Code(Me.cboGbn)
            sGroup = Ctrl.Get_Code(Me.cboType)
            sComcd = Ctrl.Get_Code(Me.cboComCd)

            Select Case tbcMain.SelectedIndex
                Case 0
                    spdDayList.MaxRows = 0

                    dt = CGDA_BT.fn_percentOfRtnBlood(sGbn, Format(dtpDate0.Value, "yyyyMM") + "01", sGroup, sComcd)
                Case 1
                    spdMonthList.MaxRows = 0
                    dateiff = CInt(DateDiff(DateInterval.Month, CDate(dtpDate0.Value), CDate(dtpDate1.Value)))
                    ReDim a_date(dateiff)

                    For i As Integer = 1 To dateiff + 1
                        a_date(i - 1) = DateAdd(DateInterval.Month, i - 1, CDate(dtpDate0.Value.ToString)).ToString("yyyy-MM")
                    Next

                    'dt = CGDA_BT.fn_percentOfRtnBloodM(sGbn, Format(dtpDate0.Value, "yyyy"), sGroup, sComcd)
                    dt = CGDA_BT.fn_percentOfRtnBloodM(sGbn, Me.dtpDate0.Value.ToString("yyyyMM"), Me.dtpDate1.Value.ToString("yyyyMM"), sGroup, sComcd, a_date)
                    'dt = CGDA_BT.fn_percentOfRtnBloodM(sGbn, Format(dtpDate0.Value, "yyyy"), sGroup, sComcd)
                Case 2
                    spdYearList.MaxRows = 0

                    dt = CGDA_BT.fn_percentOfRtnBloodY(sGbn, Format(dtpDate0.Value, "yyyy"), sGroup, sComcd)
                Case 3
                    spdMonType.MaxRows = 0

                    dt = CGDA_BT.fn_percentOfRtnBloodTypeM(sGbn, Format(dtpDate0.Value, "yyyy"), sGroup, sComcd)
                Case 4
                    spdYearType.MaxRows = 0

                    dt = CGDA_BT.fn_percentOfRtnBloodTypeY(sGbn, Format(dtpDate0.Value, "yyyy"), sGroup, sComcd)
                Case Else
                    Return
            End Select

            sb_DisplayDataList(dt)
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sb_SetHeader(ByVal rsGbn As String, ByVal rsDate As String)
        Dim sLastDay As String = ""

        If rsGbn = "D"c Then
            sLastDay = CDate(rsDate + "-01").AddMonths(1).AddDays(-1).ToString.Substring(8, 2)

            With spdDayList
                .ReDraw = False
                .MaxCols = 4
                .MaxRows = 0

                For i As Integer = 1 To CInt(sLastDay)
                    .Row = 0
                    .MaxCols += 1

                    .Col = .MaxCols : .Text = i.ToString
                    .set_ColWidth(.Col, 4)
                    .ColID = "d"c + i.ToString
                Next

                .ReDraw = True
            End With
        ElseIf rsGbn = "Y"c Then
            With spdYearList
                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                .Col = 2 : .Text = rsDate
                '.Col = 2 : .Text = (CInt(rsDate) - 1).ToString
                '.Col = 5 : .Text = rsDate
            End With

        ElseIf rsGbn = "M"c Then
            With spdMonthList
                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                .Col = 2 : .Text = rsDate + "월"
            End With
        ElseIf rsGbn = "TY" Then
            With spdYearType
                .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0
                .Col = 5 : .Text = rsDate
                '.Col = 5 : .Text = (CInt(rsDate) - 1).ToString
                '.Col = 6 : .Text = rsDate
            End With
        End If
    End Sub

    Private Sub dtpDate0_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate0.ValueChanged
        Dim sMonth As String = ""
        Dim sYear As String = ""

        Select Case tbcMain.SelectedIndex
            Case 0
                sMonth = Format(Me.dtpDate0.Value, "yyyyMM")

                If sMonth <> msMonth Then
                    sb_SetHeader("D"c, Format(Me.dtpDate0.Value, "yyyy-MM"))
                    msMonth = Format(dtpDate0.Value, "yyyyMM")
                End If
            Case 1
                sYear = Format(Me.dtpDate0.Value, "MM")

                If sYear <> msYear Then
                    sb_SetHeader("M"c, Format(dtpDate0.Value, "MM"))
                    msYear = Format(dtpDate0.Value, "MM")
                End If
            Case 2
                sYear = Format(Me.dtpDate0.Value, "yyyy")

                If sYear <> msYear Then
                    sb_SetHeader("Y"c, Format(dtpDate0.Value, "yyyy"))
                    msYear = Format(dtpDate0.Value, "yyyy")
                End If
            Case 4
                sYear = Format(dtpDate0.Value, "yyyy")

                If sYear <> msYear Then
                    sb_SetHeader("TY", Format(dtpDate0.Value, "yyyy"))
                    msYear = Format(dtpDate0.Value, "yyyy")
                    spdYearType.MaxRows = 0
                End If
            Case Else
                Return
        End Select

    End Sub

    Private Sub tbcMain_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcMain.SelectedIndexChanged
        Select Case tbcMain.SelectedIndex
            Case 0
                dtpDate1.Visible = False
                sb_SetHeader("D"c, Format(dtpDate0.Value, "yyyy-MM"))
                msMonth = Format(dtpDate0.Value, "yyyyMM")
            Case 1
                dtpDate1.Visible = True
                sb_SetHeader("M"c, Format(dtpDate0.Value, "MM"))
                msMonth = Format(dtpDate0.Value, "MM")
            Case 2
                dtpDate1.Visible = False
                sb_SetHeader("Y"c, Format(dtpDate0.Value, "yyyy"))
                msYear = Format(dtpDate0.Value, "yyyy")
            Case 4
                dtpDate1.Visible = False
                sb_SetHeader("TY", Format(dtpDate0.Value, "yyyy"))
                msYear = Format(dtpDate0.Value, "yyyy")

            Case Else
                Return
        End Select

        btnClear_Click(Nothing, Nothing)
    End Sub

    Private Sub sb_DisplayDataList(ByVal r_dt As DataTable)

        Try
            Select Case Me.tbcMain.SelectedIndex
                Case 0
                    With Me.spdDayList
                        Dim sLastDay As String = CDate(Format(Me.dtpDate0.Value, "yyyy-MM") + "-01").AddMonths(1).AddDays(-1).ToString.Substring(8, 2)

                        .MaxRows = 0
                        If r_dt.Rows.Count < 1 Then Return

                        .ReDraw = False

                        For i As Integer = 0 To r_dt.Rows.Count - 1
                            .MaxRows += 1
                            .Row = .MaxRows

                            .Col = .GetColFromID("gbnnm") : .Text = r_dt.Rows(i).Item("gbnnm").ToString
                            .Col = .GetColFromID("sumall") : .Text = r_dt.Rows(i).Item("sumall").ToString
                            .Col = .GetColFromID("cnt") : .Text = r_dt.Rows(i).Item("cnt").ToString
                            .Col = .GetColFromID("per") : .Text = r_dt.Rows(i).Item("per").ToString

                            For k As Integer = 1 To CInt(sLastDay)
                                .Col = .GetColFromID("d" + k.ToString) : .Text = r_dt.Rows(i).Item("d" + k.ToString).ToString
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                            Next
                        Next

                        sb_SetStBarSearchCnt(r_dt.Rows.Count)
                    End With
                Case 1
                    Me.spdMonthList.MaxRows = 0

                    With Me.spdMonthList
                        .MaxRows = 0
                        If r_dt.Rows.Count < 1 Then Return

                        .ReDraw = False

                        For i As Integer = 0 To r_dt.Rows.Count - 1
                            .MaxRows += 1
                            .Row = .MaxRows

                            .Col = .GetColFromID("gbnnm") : .Text = r_dt.Rows(i).Item("gbnnm").ToString
                            '.Col = .GetColFromID("sumall") : .Text = r_dt.Rows(i).Item("sumall").ToString
                            '.Col = .GetColFromID("cnt") : .Text = r_dt.Rows(i).Item("cnt").ToString
                            '.Col = .GetColFromID("per") : .Text = r_dt.Rows(i).Item("per").ToString

                            .Col = .GetColFromID("sumall") : .Text = r_dt.Rows(i).Item("am1").ToString
                            .Col = .GetColFromID("cnt") : .Text = r_dt.Rows(i).Item("m1").ToString
                            .Col = .GetColFromID("per") : .Text = r_dt.Rows(i).Item("pm1").ToString


                            'For k As Integer = 1 To 12
                            For k As Integer = 1 To 1
                                '.Col = .GetColFromID("am" + k.ToString) : .Text = r_dt.Rows(i).Item("am" + k.ToString).ToString
                                '.Col = .GetColFromID("m" + k.ToString) : .Text = r_dt.Rows(i).Item("m" + k.ToString).ToString
                                '.Col = .GetColFromID("pm" + k.ToString) : .Text = r_dt.Rows(i).Item("pm" + k.ToString).ToString

                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                            Next
                        Next

                    End With
                Case 2
                    Me.spdYearList.MaxRows = 0

                    With Me.spdYearList
                        .MaxRows = 0
                        If r_dt.Rows.Count < 1 Then Return

                        .ReDraw = False

                        For i As Integer = 0 To r_dt.Rows.Count - 1
                            .MaxRows += 1
                            .Row = .MaxRows

                            .Col = .GetColFromID("gbnnm") : .Text = r_dt.Rows(i).Item("gbnnm").ToString

                            For k As Integer = 2 To 2 '<<<20180510 해당년도만 조회 되도록 수정 
                                .Col = .GetColFromID("ayear1") : .Text = r_dt.Rows(i).Item("ayear2").ToString
                                .Col = .GetColFromID("year1") : .Text = r_dt.Rows(i).Item("year2").ToString
                                .Col = .GetColFromID("pyear1") : .Text = r_dt.Rows(i).Item("pyear2").ToString

                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                            Next
                        Next

                    End With
                Case 3
                    Me.spdMonType.MaxRows = 0

                    With Me.spdMonType
                        .MaxRows = 0
                        If r_dt.Rows.Count < 1 Then Return

                        Dim ls_SubGbn As String
                        Dim lc_Color As Color

                        .ReDraw = False

                        For i As Integer = 0 To r_dt.Rows.Count - 1
                            .MaxRows += 1
                            .Row = .MaxRows

                            ls_SubGbn = r_dt.Rows(i).Item("subgbn").ToString

                            If ls_SubGbn = "1"c Then
                                lc_Color = Color.LightSalmon
                            ElseIf ls_SubGbn = "2"c Then
                                lc_Color = Color.White
                            ElseIf ls_SubGbn = "3"c Then
                                lc_Color = Color.Silver
                            End If

                            .Col = .GetColFromID("gbnnm") : .Text = r_dt.Rows(i).Item("gbnnm").ToString
                            .BackColor = lc_Color
                            .Col = .GetColFromID("rsnnm") : .Text = r_dt.Rows(i).Item("rsnnm").ToString
                            .BackColor = lc_Color
                            .Col = .GetColFromID("sumall") : .Text = r_dt.Rows(i).Item("sumall").ToString
                            .BackColor = lc_Color
                            .Col = .GetColFromID("per") : .Text = r_dt.Rows(i).Item("per").ToString
                            .BackColor = lc_Color

                            For k As Integer = 1 To 12
                                .Col = .GetColFromID("m" + k.ToString) : .Text = r_dt.Rows(i).Item("m" + k.ToString).ToString
                                .BackColor = lc_Color

                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                            Next
                        Next

                    End With
                Case 4
                    Me.spdYearType.MaxRows = 0

                    With Me.spdYearType
                        .MaxRows = 0
                        If r_dt.Rows.Count < 1 Then Return

                        Dim ls_SubGbn As String
                        Dim lc_Color As Color

                        .ReDraw = False

                        For i As Integer = 0 To r_dt.Rows.Count - 1
                            .MaxRows += 1
                            .Row = .MaxRows

                            ls_SubGbn = r_dt.Rows(i).Item("subgbn").ToString

                            If ls_SubGbn = "1"c Then
                                lc_Color = Color.LightSalmon
                            ElseIf ls_SubGbn = "2"c Then
                                lc_Color = Color.White
                            ElseIf ls_SubGbn = "3"c Then
                                lc_Color = Color.Silver
                            End If

                            .Col = .GetColFromID("gbnnm") : .Text = r_dt.Rows(i).Item("gbnnm").ToString
                            .BackColor = lc_Color
                            .Col = .GetColFromID("rsnnm") : .Text = r_dt.Rows(i).Item("rsnnm").ToString
                            .BackColor = lc_Color
                            .Col = .GetColFromID("sumall") : .Text = r_dt.Rows(i).Item("sumall").ToString
                            .BackColor = lc_Color
                            .Col = .GetColFromID("per") : .Text = r_dt.Rows(i).Item("per").ToString
                            .BackColor = lc_Color

                            For k As Integer = 2 To 2 '<<<20180827 해당월 연도만 표시 
                                .Col = .GetColFromID("year" + k.ToString) : .Text = r_dt.Rows(i).Item("year" + k.ToString).ToString
                                .BackColor = lc_Color

                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
                            Next
                        Next

                    End With
                Case Else
                    Return
            End Select


        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdDayList.ReDraw = True

        End Try
    End Sub

    Private Sub btnTExcel_BackgroundImageLayoutChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTExcel.BackgroundImageLayoutChanged

    End Sub

    Private Sub btnTExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTExcel.Click
        Dim spdObj As AxFPSpreadADO.AxfpSpread


        Select Case Me.tbcMain.SelectedIndex
            Case 0
                spdObj = Me.spdDayList
            Case 1
                spdObj = Me.spdMonthList
            Case 2
                spdObj = Me.spdYearList
            Case 3
                spdObj = Me.spdMonType
            Case 4
                spdObj = Me.spdYearType
            Case Else
                Return
        End Select

        With spdObj
            .ReDraw = False

            .MaxRows += 2
            .InsertRows(1, 2)

            Dim sExcelFile As String = Format(dtpDate0.Value, "yyyy") + "_혈액폐기율" + ".xls"

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 1 : .Row2 = 1
            .Clip = sColHeaders

            Dim iMax As Integer = .MaxRows

            .ExportToExcel(sExcelFile, "Monthly_In_Out", "")

            Process.Start(sExcelFile)

            .DeleteRows(1, 2)
            .MaxRows -= 2

            .ReDraw = True
        End With
    End Sub

End Class