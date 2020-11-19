' 혈액 반납/폐기 리스트 조회

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB15

    Private Sub FGB27_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB15_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB15_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-").Substring(0, 7) + "-01")
        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdSList)

        Me.spdSList.MaxRows = 0

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdSList.MaxRows = 0
        Me.txtRtn.Text = ""
        Me.txtAbn.Text = ""
        Me.txtSelf.Text = ""
        Me.txtExc.Text = ""
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        Dim ls_Gbn As String = ""
        Dim lal_gbn As New ArrayList

        Me.spdSList.MaxRows = 0
        Me.txtRtn.Text = ""
        Me.txtAbn.Text = ""
        Me.txtSelf.Text = ""
        Me.txtExc.Text = ""

        If Me.chkRtn.Checked = True And Me.chkAbn.Checked = True And Me.chkAbnDo.Checked = True And Me.chkChg.Checked = True Then
            ls_Gbn = ""
        Else
            If Me.chkRtn.Checked = True Then lal_gbn.Add("'3'")
            If Me.chkAbn.Checked = True Then lal_gbn.Add("'4'")
            If Me.chkAbnDo.Checked = True Then lal_gbn.Add("'5'")
            If Me.chkChg.Checked = True Then lal_gbn.Add("'6'")
            ' If Me.chkRtn.Checked = False And Me.chkAbn.Checked = False And Me.chkAbnDo.Checked = False And Me.chkChg.Checked = False Then lal_gbn.Add(" ")

            For i As Integer = 0 To lal_gbn.Count - 1
                If i = 0 Then
                    ls_Gbn = lal_gbn(i).ToString
                Else
                    ls_Gbn = ls_Gbn + ", " + lal_gbn(i).ToString
                End If
            Next
        End If

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회
            dt = CGDA_BT.fn_rtnSearchList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), ls_Gbn)

            sb_DisplayDataList(dt)

            dt = CGDA_BT.fn_rtnqnt(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), ls_Gbn)

            If dt.Rows.Count > 0 Then
                Me.txtRtn.Text = dt.Rows(0).Item("rtncnt").ToString
                Me.txtAbn.Text = dt.Rows(0).Item("discnt").ToString
                Me.txtSelf.Text = dt.Rows(0).Item("selfcnt").ToString
                Me.txtExc.Text = dt.Rows(0).Item("exccnt").ToString
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sb_DisplayDataList(ByVal r_dt As DataTable)
        Try
            With Me.spdSList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then
                    sb_SetStBarSearchCnt(0)
                    Return
                End If

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("gbn") : .Text = r_dt.Rows(ix).Item("gbn").ToString
                    .Col = .GetColFromID("rtndt") : .Text = r_dt.Rows(ix).Item("rtndt").ToString
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(ix).Item("orddt").ToString
                    .Col = .GetColFromID("wardno") : .Text = r_dt.Rows(ix).Item("wardno").ToString
                    .Col = .GetColFromID("deptnm") : .Text = r_dt.Rows(ix).Item("deptnm").ToString
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString
                    .Col = .GetColFromID("aborh") : .Text = r_dt.Rows(ix).Item("aborh").ToString
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(ix).Item("vbldno").ToString
                    .Col = .GetColFromID("rtnrsncmt") : .Text = r_dt.Rows(ix).Item("rtnrsncmt").ToString

                Next

                sb_SetStBarSearchCnt(r_dt.Rows.Count)

            End With
        Catch ex As Exception
            Me.spdSList.ReDraw = True
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdSList.ReDraw = True

        End Try
    End Sub

    Private Sub btnTExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")

        With Me.spdSList
            .ReDraw = False

            .MaxRows += 6
            .InsertRows(1, 6)

            .Row = 1
            .Col = 5
            .Text = "혈액 반납/폐기 리스트"
            .FontBold = True
            .FontSize = 20
            .ForeColor = System.Drawing.Color.Red

            ' 조회일자구간 표시해주기
            .Row = 3
            .Col = 3
            .Text = "조회구간 : " & Format(dtpDate0.Value, "yyyy-MM-dd") & " ~ " & Format(dtpDate1.Value, "yyyy-MM-dd")

            Dim sColHeaders As String = ""

            .Col = 1 : .Col2 = .MaxCols
            .Row = 0 : .Row2 = 0
            sColHeaders = .Clip

            .Col = 1 : .Col2 = .MaxCols
            .Row = 5 : .Row2 = 5
            .Clip = sColHeaders

            ' 아래쪽에 혈액 반납/폐기 수량 보여주기
            Dim iMax As Integer = .MaxRows

            .MaxRows += 5
            .InsertRows(iMax + 1, 5)

            .SetText(2, iMax + 1, "소계 : ")
            .SetText(3, iMax + 1, "반납 : " + txtRtn.Text + " 개")
            .SetText(4, iMax + 1, "폐기 : " + txtAbn.Text + " 개")
            .SetText(5, iMax + 1, "자체폐기 : " + txtSelf.Text + " 개")
            .SetText(6, iMax + 1, "혈액교환 : " + txtExc.Text + " 개")

            If .ExportToExcel("OutBloodList_" + Now.ToShortDateString() + ".xls", "Out Blood List", "") Then
                Process.Start("OutBloodList_" + Now.ToShortDateString() + ".xls")
            End If

            .DeleteRows(1, 6)
            .DeleteRows(iMax + 1, 5)

            .MaxRows -= 11

            .ReDraw = True
        End With
    End Sub

End Class