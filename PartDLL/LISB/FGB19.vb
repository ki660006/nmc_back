' 혈액 입고/출고 월별 현황 조회

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB19
    Private msMonth As String = ""
    Private msLastday As String = ""

    Private Sub FGB19_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB19_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB19_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-").Substring(0, 7) + "-01")

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(Me.spdTopList)
        DS_SpreadDesige.sbInti(Me.spdDetail)

        Me.spdTopList.MaxRows = 0
        Me.spdDetail.MaxRows = 0

        msMonth = Format(Me.dtpDate0.Value, "yyyyMM")
        sb_SetHeader(Format(dtpDate0.Value, "yyyyMM") + "01")
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click


        Dim sGbn As String = ""

        Me.spdTopList.MaxRows = 0
        Me.spdDetail.MaxRows = 0

        If Me.rdoIn.Checked = True Then
            sGbn = "I"c
        Else
            sGbn = "O"c
        End If

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회

            Dim dt As New DataTable

            dt = CGDA_BT.fn_InouTBldListM(Format(dtpDate0.Value, "yyyyMM"), sGbn)

            sb_DisplayDataList(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdTopList.MaxRows = 0
        Me.spdDetail.MaxRows = 0
    End Sub

    Private Sub sb_DisplayDataList(ByVal r_dt As DataTable)

        Try
            With Me.spdTopList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then
                    sb_SetStBarSearchCnt(0)
                    Return
                End If

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("aborh") : .Text = r_dt.Rows(ix).Item("aborh").ToString.Trim
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("comcd").ToString.Trim
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString.Trim
                    .Col = .GetColFromID("donqnt") : .Text = r_dt.Rows(ix).Item("donqnt").ToString.Trim
                    .Col = .GetColFromID("d1") : .Text = r_dt.Rows(ix).Item("d1").ToString.Trim
                    .Col = .GetColFromID("d2") : .Text = r_dt.Rows(ix).Item("d2").ToString.Trim
                    .Col = .GetColFromID("d3") : .Text = r_dt.Rows(ix).Item("d3").ToString.Trim
                    .Col = .GetColFromID("d4") : .Text = r_dt.Rows(ix).Item("d4").ToString.Trim
                    .Col = .GetColFromID("d5") : .Text = r_dt.Rows(ix).Item("d5").ToString.Trim
                    .Col = .GetColFromID("d6") : .Text = r_dt.Rows(ix).Item("d6").ToString.Trim
                    .Col = .GetColFromID("d7") : .Text = r_dt.Rows(ix).Item("d7").ToString.Trim
                    .Col = .GetColFromID("d8") : .Text = r_dt.Rows(ix).Item("d8").ToString.Trim
                    .Col = .GetColFromID("d9") : .Text = r_dt.Rows(ix).Item("d9").ToString.Trim
                    .Col = .GetColFromID("d10") : .Text = r_dt.Rows(ix).Item("d10").ToString.Trim
                    .Col = .GetColFromID("d11") : .Text = r_dt.Rows(ix).Item("d11").ToString.Trim
                    .Col = .GetColFromID("d12") : .Text = r_dt.Rows(ix).Item("d12").ToString.Trim
                    .Col = .GetColFromID("d13") : .Text = r_dt.Rows(ix).Item("d13").ToString.Trim
                    .Col = .GetColFromID("d14") : .Text = r_dt.Rows(ix).Item("d14").ToString.Trim
                    .Col = .GetColFromID("d15") : .Text = r_dt.Rows(ix).Item("d15").ToString.Trim
                    .Col = .GetColFromID("d16") : .Text = r_dt.Rows(ix).Item("d16").ToString.Trim
                    .Col = .GetColFromID("d17") : .Text = r_dt.Rows(ix).Item("d17").ToString.Trim
                    .Col = .GetColFromID("d18") : .Text = r_dt.Rows(ix).Item("d18").ToString.Trim
                    .Col = .GetColFromID("d19") : .Text = r_dt.Rows(ix).Item("d19").ToString.Trim
                    .Col = .GetColFromID("d20") : .Text = r_dt.Rows(ix).Item("d20").ToString.Trim
                    .Col = .GetColFromID("d21") : .Text = r_dt.Rows(ix).Item("d21").ToString.Trim
                    .Col = .GetColFromID("d22") : .Text = r_dt.Rows(ix).Item("d22").ToString.Trim
                    .Col = .GetColFromID("d23") : .Text = r_dt.Rows(ix).Item("d23").ToString.Trim
                    .Col = .GetColFromID("d24") : .Text = r_dt.Rows(ix).Item("d24").ToString.Trim
                    .Col = .GetColFromID("d25") : .Text = r_dt.Rows(ix).Item("d25").ToString.Trim
                    .Col = .GetColFromID("d26") : .Text = r_dt.Rows(ix).Item("d26").ToString.Trim
                    .Col = .GetColFromID("d27") : .Text = r_dt.Rows(ix).Item("d27").ToString.Trim
                    .Col = .GetColFromID("d28") : .Text = r_dt.Rows(ix).Item("d28").ToString.Trim
                    .Col = .GetColFromID("sumcnt") : .Text = r_dt.Rows(ix).Item("sumcnt").ToString.Trim

                    If msLastday = "29" Then
                        .Col = .GetColFromID("d29") : .Text = r_dt.Rows(ix).Item("d29").ToString.Trim
                    ElseIf msLastday = "30" Then
                        .Col = .GetColFromID("d29") : .Text = r_dt.Rows(ix).Item("d29").ToString.Trim
                        .Col = .GetColFromID("d30") : .Text = r_dt.Rows(ix).Item("d30").ToString.Trim
                    ElseIf msLastday = "31" Then
                        .Col = .GetColFromID("d29") : .Text = r_dt.Rows(ix).Item("d29").ToString.Trim
                        .Col = .GetColFromID("d30") : .Text = r_dt.Rows(ix).Item("d30").ToString.Trim
                        .Col = .GetColFromID("d31") : .Text = r_dt.Rows(ix).Item("d31").ToString.Trim
                    End If

                    If ix = r_dt.Rows.Count - 1 Then
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = .MaxRows : .Row2 = .MaxRows
                        .BlockMode = True

                        .BackColor = Drawing.Color.FromArgb(165, 186, 222)

                        .BlockMode = False
                    End If

                Next

                .Col = 5 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True

                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter

                .BlockMode = False

                sb_SetStBarSearchCnt(r_dt.Rows.Count - 1)

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdTopList.ReDraw = True

        End Try
    End Sub

    Private Sub sb_SetHeader(ByVal rsDate As String)
        Try
            Dim lal_rtnValue As New ArrayList
            Dim ls_Lastday As String
            Dim li_Loop As Integer

            Dim dt As DataTable = CGDA_BT.fn_GetLastday(rsDate)

            lal_rtnValue = fn_GetSelectItem(dt, 1)
            ls_Lastday = lal_rtnValue.Item(0).ToString

            li_Loop = CInt(ls_Lastday.Substring(6, 2))
            msLastday = li_Loop.ToString

            With spdTopList
                .ReDraw = False
                .MaxCols = 4
                .MaxRows = 0

                For ix As Integer = 1 To li_Loop
                    .Row = 0
                    .MaxCols += 1
                    .Col = .MaxCols : .Text = ix.ToString
                    .set_ColWidth(.Col, 5)
                    .ColID = "d"c + ix.ToString

                Next

                .MaxCols += 1
                .Col = .MaxCols : .Text = "계"c
                .set_ColWidth(.Col, 5)
                .ColID = "sumcnt"

                .ReDraw = True
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub dtpDate0_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate0.ValueChanged
        Dim ls_month As String

        ls_month = Format(dtpDate0.Value, "yyyyMM")

        If ls_month <> msMonth Then
            sb_SetHeader(ls_month + "01")
        End If

        msMonth = ls_month
    End Sub

    Private Sub rdoIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIn.Click
        Me.lbTxt.Text = "입고 혈액 리스트"
        Me.spdTopList.MaxRows = 0
        Me.spdDetail.MaxRows = 0
    End Sub

    Private Sub rdoOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoOut.Click
        Me.lbTxt.Text = "출고 혈액 리스트"
        Me.spdTopList.MaxRows = 0
        Me.spdDetail.MaxRows = 0
    End Sub

    Private Sub spdTopList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdTopList.ClickEvent
        If Me.spdTopList.MaxRows < 1 Then Return

        Dim ls_Comcd As String
        Dim ls_Month As String
        Dim ls_AboRh As String
        Dim ls_Abo As String
        Dim ls_rh As String
        Dim ls_Gbn As String
        Dim dt As DataTable

        With Me.spdTopList
            .Row = e.row
            .Col = .GetColFromID("comcd") : ls_Comcd = .Text
            .Col = .GetColFromID("aborh") : ls_AboRh = .Text

        End With

        ls_Month = Format(dtpDate0.Value, "yyyyMM")
        ls_Abo = ls_AboRh.Replace("+"c, "").Replace("-"c, "")
        ls_rh = ls_AboRh.Replace("A"c, "").Replace("B"c, "").Replace("O"c, "")

        Me.spdDetail.MaxRows = 0

        If Me.rdoIn.Checked = True Then
            ls_Gbn = "I"c
        Else
            ls_Gbn = "O"c
        End If

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회
            dt = CGDA_BT.fn_InOutDtailM(Format(dtpDate0.Value, "yyyyMM"), ls_Gbn, ls_Comcd, ls_Abo, ls_rh)

            sb_DisplayDetail(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub sb_DisplayDetail(ByVal r_dt As DataTable)

        Try
            With spdDetail
                .MaxRows = 0

                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("gbn") : .Text = r_dt.Rows(ix).Item("gbn").ToString
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString
                    .Col = .GetColFromID("aborh") : .Text = r_dt.Rows(ix).Item("aborh").ToString
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(ix).Item("vbldno").ToString
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(ix).Item("indt").ToString
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(ix).Item("availdt").ToString
                    .Col = .GetColFromID("testdt") : .Text = r_dt.Rows(ix).Item("testdt").ToString
                    .Col = .GetColFromID("outdt") : .Text = r_dt.Rows(ix).Item("outdt").ToString
                    .Col = .GetColFromID("rtndt") : .Text = r_dt.Rows(ix).Item("rtndt").ToString

                Next

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdDetail.ReDraw = True

        End Try
    End Sub

    Private Sub btnTExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTExcel.Click
        Dim sGbn As String = ""

        If Me.rdoIn.Checked = True Then
            sGbn = "입고"
        Else
            sGbn = "출고"
        End If

        With Me.spdTopList

            .MaxRows += 1 : .InsertRows(1, 1)

            .ReDraw = False

            Dim sColName As String = ""
            For iCol As Integer = 1 To .MaxCols
                .Row = 0
                .Col = iCol
                sColName = .Text.Trim

                .Row = 1
                .Col = iCol
                .Text = sColName
            Next

            Dim sExcelFile As String = Format(dtpDate0.Value, "yyyy-MM") + "_혈액" + sGbn + ".xls"

            .ExportToExcel(sExcelFile, "Monthly_In_Out", "")

            Process.Start(sExcelFile)

            .DeleteRows(1, 1) : .MaxRows -= 1

            .ReDraw = True
        End With
    End Sub
End Class