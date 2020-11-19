' 혈액 재고량 조회

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB18
    Private mobjDAF As New LISAPP.APP_F_COMCD

    Private Sub FGB18_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB18_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB18_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdStored)
        DS_SpreadDesige.sbInti(spdStdDetail)

        Me.spdStored.MaxRows = 0
        Me.spdStdDetail.MaxRows = 0
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sFn As String = "CButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CButton1.Click"
        Dim dt As New DataTable

        Me.spdStored.MaxRows = 0
        Me.spdStdDetail.MaxRows = 0

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회
            dt = CGDA_BT.fn_StoredList(Format(dtpDate0.Value, "yyyyMMdd"))

            sb_DisplayDataList(dt)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdStored.MaxRows = 0
        Me.spdStdDetail.MaxRows = 0
    End Sub

    Private Sub sb_DisplayDataList(ByVal r_dt As DataTable)

        Try
            With Me.spdStored
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("comcd").ToString
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString
                    .Col = .GetColFromID("a1") : .Text = r_dt.Rows(ix).Item("a1").ToString
                    .Col = .GetColFromID("b1") : .Text = r_dt.Rows(ix).Item("b1").ToString
                    .Col = .GetColFromID("o1") : .Text = r_dt.Rows(ix).Item("o1").ToString
                    .Col = .GetColFromID("ab1") : .Text = r_dt.Rows(ix).Item("ab1").ToString
                    .Col = .GetColFromID("a2") : .Text = r_dt.Rows(ix).Item("a2").ToString
                    .Col = .GetColFromID("b2") : .Text = r_dt.Rows(ix).Item("b2").ToString
                    .Col = .GetColFromID("o2") : .Text = r_dt.Rows(ix).Item("o2").ToString
                    .Col = .GetColFromID("ab2") : .Text = r_dt.Rows(ix).Item("ab2").ToString
                    .Col = .GetColFromID("availqty") : .Text = r_dt.Rows(ix).Item("availqty").ToString
                Next

                sb_SetStBarSearchCnt(r_dt.Rows.Count)

            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdStored.ReDraw = True

        End Try
    End Sub

    Private Sub spdStored_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdStored.ClickEvent
        If Me.spdStored.MaxRows < 1 Then Return

        sb_DisPlaySubData(e.row, e.col)
    End Sub

    Private Sub sb_DisPlaySubData(ByVal riRow As Integer, ByVal riCol As Integer)
        Try
            Dim dt As New DataTable
            Dim sComcd As String = ""

            If Me.spdStored.MaxRows < 1 Then Return

            Me.spdStdDetail.MaxRows = 0

            With Me.spdStored
                .Row = riRow
                .Col = .GetColFromID("comcd") : sComcd = .Text
            End With

            Select Case riCol
                Case 1
                    Return
                Case 2
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "A"c, "+"c)
                Case 3
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "B"c, "+"c)
                Case 4
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "O"c, "+"c)
                Case 5
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "AB", "+"c)
                Case 6
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "A"c, "-"c)
                Case 7
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "B"c, "-"c)
                Case 8
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "O"c, "-"c)
                Case 9
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd, "AB", "-"c)
                Case 10
                    dt = CGDA_BT.fn_StdDetailList(Format(dtpDate0.Value, "yyyyMMdd"), sComcd)
            End Select

            If dt.Rows.Count() < 1 Then Return

            sb_DisplaySubList(dt)
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub

    Private Sub sb_DisplaySubList(ByVal r_dt As DataTable)

        Dim sAbo As String = ""

        Try
            With Me.spdStdDetail
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False

                For i As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("aborh") : .Text = r_dt.Rows(i).Item("aborh").ToString

                    sAbo = r_dt.Rows(i).Item("aborh").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(sAbo)

                    .Col = .GetColFromID("bldno") : .Text = r_dt.Rows(i).Item("bldno").ToString
                    .Col = .GetColFromID("dondt") : .Text = r_dt.Rows(i).Item("dondt").ToString
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(i).Item("availdt").ToString
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("inid") : .Text = r_dt.Rows(i).Item("inid").ToString
                Next

                sb_SetStBarSearchCnt(r_dt.Rows.Count)

            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdStdDetail.ReDraw = True

        End Try
    End Sub
End Class