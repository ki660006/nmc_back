' 혈액 반납/폐기 건수 조회

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB17
    Private msMonth As String = ""
    Private msLastday As String = ""
    Private mbSkipFlg As Boolean = False

    Private Sub FGB17_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB17_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB17_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-").Substring(0, 7) + "-01")

        Dim dt As DataTable
        Dim lal_rtnValue As New ArrayList
        Dim ls_Lastday As String

        dt = CGDA_BT.fn_GetLastday((Format(Me.dtpDate0.Value, "yyyyMMdd")), "D"c)

        lal_rtnValue = fn_GetSelectItem(dt, 1)
        ls_Lastday = lal_rtnValue.Item(0).ToString

        dtpDate1.Value = CDate(ls_Lastday)

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdList)

        Me.spdList.MaxRows = 0

        sb_SetHeader("1"c)
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sFn As String = "CButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CButton1.Click"

        Dim dt As New DataTable
        Dim sGbn As String = ""

        Me.spdList.MaxRows = 0

        If Me.rdoRtn.Checked = True Then
            sGbn = "R"c
        ElseIf Me.rdoAbn.Checked = True Then
            sGbn = "A"c
        End If

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회

            If Me.rdoDay.Checked = True Then
                dt = CGDA_BT.fn_RtnCntSearchDay(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), sGbn)
            ElseIf Me.rdoMon.Checked = True Then
                dt = CGDA_BT.fn_RtnCntSearchMonth(Format(dtpDate0.Value, "yyyyMM"), Format(dtpDate1.Value, "yyyyMM"), sGbn)
            ElseIf Me.rdoYear.Checked = True Then
                dt = CGDA_BT.fn_RtnCntSearchYear(Format(dtpDate0.Value, "yyyy"), sGbn)
            End If

            sb_DisplayDataList(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        spdList.MaxRows = 0
    End Sub

    Private Sub sb_SetHeader(ByVal rsGbn As String)
        Dim li_LoopFrom As Integer = 0
        Dim li_LoopTo As Integer = 0
        Dim ls_F As String = ""
        Dim ls_T As String = ""

        If rsGbn = "1"c Then     ' 일별
            ls_F = Format(dtpDate0.Value, "yyyyMMdd").Substring(6, 2)
            ls_T = Format(dtpDate1.Value, "yyyyMMdd").Substring(6, 2)

            li_LoopFrom = CInt(ls_F)
            li_LoopTo = CInt(ls_T)

        ElseIf rsGbn = "2"c Then ' 월별
            ls_F = Format(dtpDate0.Value, "yyyyMM").Substring(4, 2)
            ls_T = Format(dtpDate1.Value, "yyyyMM").Substring(4, 2)

            li_LoopFrom = CInt(ls_F)
            li_LoopTo = CInt(ls_T)
        ElseIf rsGbn = "3"c Then ' 년별
            ls_F = Format(dtpDate0.Value, "yyyyMM").Substring(0, 4)

            'li_LoopFrom = CInt(ls_F) - 1
            li_LoopFrom = CInt(ls_F)
            li_LoopTo = CInt(ls_F)

        End If

        With spdList
            .ReDraw = False
            .MaxCols = 3
            .MaxRows = 0

            For i As Integer = li_LoopFrom To li_LoopTo
                .Row = 0
                .MaxCols += 1

                If rsGbn = "1"c Then
                    .Col = .MaxCols : .Text = i.ToString
                    .set_ColWidth(.Col, 4)
                    .ColID = "d"c + i.ToString
                ElseIf rsGbn = "2"c Then
                    .Col = .MaxCols : .Text = i.ToString + "월"c
                    .set_ColWidth(.Col, 7)
                    .ColID = "m"c + i.ToString
                ElseIf rsGbn = "3"c Then
                    .Col = .MaxCols : .Text = i.ToString + "년"c
                    .set_ColWidth(.Col, 10)

                    If i = li_LoopFrom Then
                        .ColID = "y1"
                        '<<<20180124 년별은 해당년도만 보이게 수정함 .
                        'ElseIf i = li_LoopTo Then
                        '    .ColID = "y2"
                    End If

                End If

            Next

            .ReDraw = True
        End With
    End Sub

    Private Sub dtpDate0_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate0.ValueChanged
        Try
            Dim dt As DataTable
            Dim lal_rtnValue As New ArrayList
            Dim ls_Lastday As String
            Dim ls_Fmonth As String = ""
            Dim ls_Tmonth As String = ""

            If mbSkipFlg = True Then Return

            mbSkipFlg = True

            If Me.rdoDay.Checked = True Then
                dt = CGDA_BT.fn_GetLastday(Format(dtpDate0.Value, "yyyyMMdd"), "D"c)

                lal_rtnValue = fn_GetSelectItem(dt, 1)
                ls_Lastday = lal_rtnValue.Item(0).ToString

                dtpDate1.Value = CDate(ls_Lastday)

                sb_SetHeader("1"c)

            ElseIf Me.rdoMon.Checked = True Then
                ls_Fmonth = Format(dtpDate0.Value, "yyyyMM").Substring(0, 4)
                ls_Tmonth = Format(dtpDate1.Value, "yyyyMM").Substring(0, 4)

                If ls_Fmonth <> ls_Tmonth Then
                    dtpDate1.Value = dtpDate0.Value
                End If

                sb_SetHeader("2"c)
            ElseIf Me.rdoYear.Checked = True Then
                ls_Fmonth = Format(dtpDate0.Value, "yyyyMM").Substring(0, 4)
                ls_Tmonth = Format(dtpDate1.Value, "yyyyMM").Substring(0, 4)

                If ls_Fmonth > ls_Tmonth Then
                    dtpDate1.Value = dtpDate0.Value
                End If

                sb_SetHeader("3"c)
            End If

            mbSkipFlg = False
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub dtpDate1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate1.ValueChanged
        Dim ls_Fmonth As String = ""
        Dim ls_Tmonth As String = ""

        If mbSkipFlg = True Then Return

        mbSkipFlg = True

        If Me.rdoDay.Checked = True Then
            ls_Fmonth = Format(dtpDate0.Value, "yyyyMM")
            ls_Tmonth = Format(dtpDate1.Value, "yyyyMM")

            If ls_Fmonth <> ls_Tmonth Then
                Me.dtpDate0.Value = CDate(Format(dtpDate1.Value, "yyyy-MM") + "-01")

                sb_SetHeader("1"c)
            End If

        ElseIf Me.rdoMon.Checked = True Then
            ls_Fmonth = Format(dtpDate0.Value, "yyyyMM").Substring(0, 4)
            ls_Tmonth = Format(dtpDate1.Value, "yyyyMM").Substring(0, 4)

            If ls_Fmonth <> ls_Tmonth Then
                Me.dtpDate0.Value = dtpDate1.Value
            End If

            sb_SetHeader("2"c)
        ElseIf Me.rdoYear.Checked = True Then
            ls_Fmonth = Format(dtpDate0.Value, "yyyyMM").Substring(0, 4)
            ls_Tmonth = Format(dtpDate1.Value, "yyyyMM").Substring(0, 4)

            If ls_Fmonth > ls_Tmonth Then
                Me.dtpDate0.Value = dtpDate1.Value
            End If

            sb_SetHeader("3"c)
        End If

        mbSkipFlg = False
    End Sub

    Private Sub rdoDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDay.Click

        Try
            Me.dtpDate0.Format = DateTimePickerFormat.Short
            Me.dtpDate1.Format = DateTimePickerFormat.Short
            Me.dtpDate0.CustomFormat = ""
            Me.dtpDate1.CustomFormat = ""

            Dim lal_rtnValue As New ArrayList
            Dim ls_Lastday As String

            Dim dt As DataTable = CGDA_BT.fn_GetLastday(Format(dtpDate0.Value, "yyyyMMdd"), "D"c)

            lal_rtnValue = fn_GetSelectItem(dt, 1)
            ls_Lastday = lal_rtnValue.Item(0).ToString

            dtpDate1.Value = CDate(ls_Lastday)

            dtpDate1.Visible = True
            lblBar.Visible = True

            sb_SetHeader("1"c)
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub rdoMon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoMon.Click
        Me.dtpDate0.Format = DateTimePickerFormat.Custom
        Me.dtpDate1.Format = DateTimePickerFormat.Custom
        Me.dtpDate0.CustomFormat = "yyyy-MM"
        Me.dtpDate1.CustomFormat = "yyyy-MM"

        Me.dtpDate1.Visible = True
        Me.lblBar.Visible = True

        sb_SetHeader("2"c)
    End Sub

    Private Sub rdoYear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoYear.Click
        Me.dtpDate0.Format = DateTimePickerFormat.Custom
        Me.dtpDate1.Format = DateTimePickerFormat.Custom
        Me.dtpDate0.CustomFormat = "yyyy"
        Me.dtpDate1.CustomFormat = "yyyy"

        Me.dtpDate1.Visible = False
        Me.lblBar.Visible = False

        sb_SetHeader("3"c)
    End Sub

    Private Sub sb_DisplayDataList(ByVal rDt As DataTable)

        Dim li_LoopFrom As Integer = 0
        Dim li_LoopTo As Integer = 0
        Dim ls_F As String = ""
        Dim ls_T As String = ""

        Try
            With spdList
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then
                    sb_SetStBarSearchCnt(0)
                    Return
                End If

                .ReDraw = False

                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("comnmd") : .Text = rDt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("suma") : .Text = rDt.Rows(i).Item("suma").ToString
                    .Col = .GetColFromID("sumt") : .Text = rDt.Rows(i).Item("sumt").ToString

                    If rdoDay.Checked = True Then     ' 일별
                        ls_F = Format(dtpDate0.Value, "yyyyMMdd").Substring(6, 2)
                        ls_T = Format(dtpDate1.Value, "yyyyMMdd").Substring(6, 2)

                        li_LoopFrom = CInt(ls_F)
                        li_LoopTo = CInt(ls_T)

                        For k As Integer = li_LoopFrom To li_LoopTo
                            .Col = .GetColFromID("d" + k.ToString) : .Text = rDt.Rows(i).Item("d" + k.ToString).ToString
                        Next

                    ElseIf rdoMon.Checked = True Then ' 월별
                        ls_F = Format(dtpDate0.Value, "yyyyMM").Substring(4, 2)
                        ls_T = Format(dtpDate1.Value, "yyyyMM").Substring(4, 2)

                        li_LoopFrom = CInt(ls_F)
                        li_LoopTo = CInt(ls_T)

                        For k As Integer = li_LoopFrom To li_LoopTo
                            .Col = .GetColFromID("m" + k.ToString) : .Text = rDt.Rows(i).Item("m" + k.ToString).ToString
                        Next

                    ElseIf rdoYear.Checked = True Then ' 년별

                        .Col = .GetColFromID("y1") : .Text = rDt.Rows(i).Item("y1").ToString
                        '.Col = .GetColFromID("y2") : .Text = rDt.Rows(i).Item("y2").ToString

                    End If

                Next

                .Col = 2 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True

                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter

                .BlockMode = False

                sb_SetStBarSearchCnt(rDt.Rows.Count)

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdList.ReDraw = True
        End Try

    End Sub

    Private Sub btnTExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")

        With Me.spdList
            .ReDraw = False

            .MaxRows += 5 : .InsertRows(1, 5)
            .MaxCols += 1 : .InsertCols(1, 1)

            .Row = 1
            .Col = 3

            If rdoRtn.Checked = True Then
                .Text = "혈액 반납 건수 조회"
            Else
                .Text = "혈액 폐기 건수 조회"
            End If

            .FontBold = True
            .FontSize = 20
            .ForeColor = System.Drawing.Color.Red

            ' 조회일자구간 표시해주기
            .Row = 3
            .Col = 2

            If rdoDay.Checked = True Then
                .Text = "조회구간 : " & Format(dtpDate0.Value, "yyyy-MM-dd") & " ~ " & Format(dtpDate1.Value, "yyyy-MM-dd")
            ElseIf rdoMon.Checked = True Then
                .Text = "조회구간 : " & Format(dtpDate0.Value, "yyyy-MM") & " ~ " & Format(dtpDate1.Value, "yyyy-MM")
            ElseIf rdoYear.Checked = True Then
                .Text = "조회구간 : " & Format(dtpDate0.Value, "yyyy")
            End If


            Dim sColName As String = ""                           ' column header text를 저장하는 변수
            For iCol As Integer = 1 To .MaxCols
                .Row = 0
                .Col = iCol
                sColName = .Text.Trim

                .Row = 5
                .Col = iCol
                .Text = sColName
            Next

            Dim sRowName As String = ""                           ' row header text 를 저장하는 변수
            For iRow As Integer = 1 To .MaxRows - 5
                .Col = 0
                .Row = iRow + 5
                sRowName = .Text.Trim

                .Col = 1
                .Row = iRow + 5                                   ' row 가 7인 곳에서 부터 row header 및 데이터가 표시된다.
                .Text = sRowName
            Next

            If .ExportToExcel("c:\혈액반납폐기건수_" & sTime & ".xls", "혈액반납폐기건수", "") Then
                Process.Start("c:\혈액반납폐기건수_" & sTime & ".xls")
            End If

            .DeleteRows(1, 5) : .MaxRows -= 5
            .DeleteCols(1, 1) : .MaxCols -= 1

            .ReDraw = True
        End With
    End Sub
End Class