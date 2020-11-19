Imports System.Drawing

Imports COMMON.CommFN

Public Class FGRST_REF
    Dim m_al_List As ArrayList
    Private moForm As Windows.Forms.Form

    Public Sub Display_Data(ByVal roForm As Windows.Forms.Form, ByVal rsBcNo As String, ByVal rsTclsCd As String, ByVal rsSpcCd As String)

        Dim dt As New DataTable
        Dim aryList As New ArrayList

        Try
            '2018-06-29 yjh lr010m, lm010m 테이블 모두 조회할 수 있도록 수정
            'dt = LISAPP.APP_R.RstFn.fnGet_Result_Ref(rsBcNo, rsTclsCd, rsSpcCd)
            dt = LISAPP.APP_R.RstFn.fnGet_Result_Ref_All(rsBcNo, rsTclsCd, rsSpcCd)
            sbDisplay_ResultView(dt)

            sbSTDisplay()

            Me.ShowDialog(roForm)



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub
    Private Sub sbSTDisplay()

        Dim sBcNo As String = ""
        Dim sTnmd As String = ""
        Dim sTestCd As String = ""
        Dim sSpcCd As String = ""
        Dim sTCdGbn As String = ""

        Try
            With Me.spdHistory
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("orgrst")

                    If .Text.Trim = "{null}" Then
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                        .Col = .GetColFromID("tnmd") : sTnmd = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : sTestCd = .Text.Replace("-", "")

                        Dim strst As New AxAckResultViewer.STRST01

                        strst.SpecialTestName = sTnmd
                        strst.BcNo = sBcNo
                        strst.TestCd = sTestCd

                        strst.ShowDialog(moForm)
                    End If

                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_ResultView(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub sbDisplay_ResultView(DataTable)"

        Try
            With Me.spdHistory

                .MaxRows = 0

                '.ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For introw As Integer = 1 To r_dt.Rows.Count
                    .Row = introw
                    .Col = .GetColFromID("chk") : .Text = ""

                    For intIx1 As Integer = 1 To r_dt.Columns.Count
                        Dim intCol As Integer = 0

                        intCol = .GetColFromID(r_dt.Columns(intIx1 - 1).ColumnName.ToLower())

                        If intCol > 0 Then
                            .Row = introw
                            .Col = intCol
                            Select Case intCol
                                Case .GetColFromID("judgmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString() = "L" Then
                                        .BackColor = Color.FromArgb(221, 240, 255)
                                        .ForeColor = Color.FromArgb(0, 0, 255)
                                    ElseIf r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString() = "H" Then
                                        .BackColor = Color.FromArgb(255, 230, 231)
                                        .ForeColor = Color.FromArgb(255, 0, 0)
                                    End If

                                Case .GetColFromID("panicmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString() = "P" Then
                                        .BackColor = Color.FromArgb(150, 150, 255)
                                        .ForeColor = Color.FromArgb(255, 255, 255)
                                    End If

                                Case .GetColFromID("deltamark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString() = "D" Then
                                        .BackColor = Color.FromArgb(150, 255, 150)
                                        .ForeColor = Color.FromArgb(0, 128, 64)
                                    End If

                                Case .GetColFromID("criticalmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString() = "C" Then
                                        .BackColor = Color.FromArgb(255, 150, 255)
                                        .ForeColor = Color.FromArgb(255, 255, 255)
                                    End If

                                Case .GetColFromID("alertmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString() = "A" Then
                                        .BackColor = Color.FromArgb(255, 255, 150)
                                        .ForeColor = Color.FromArgb(0, 0, 0)
                                    End If

                                Case .GetColFromID("tnmd")
                                    If r_dt.Rows(introw - 1).Item("tcdgbn").ToString() = "C" Then
                                        If r_dt.Rows(introw - 1).Item("tclscd").ToString = "" Then
                                            .Text = "... " + r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                        Else
                                            .Text = "  ... " + r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                        End If
                                    ElseIf r_dt.Rows(introw - 1).Item("tclscd").ToString = "" Or _
                                           r_dt.Rows(introw - 1).Item("tcdgbn").ToString = "R" Or r_dt.Rows(introw - 1).Item("tcdgbn").ToString = "B" Then
                                        .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                    Else
                                        .Text = "  " + r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
                                    End If

                                Case Else
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()

                            End Select
                        End If
                    Next
                Next
            End With

        Catch ex As Exception
            'sbLog_Exception(sFn + " : " + ex.Message)

        Finally
            'Me.spdHistory.ReDraw = True
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGRST_REF_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

    Private Sub spdHistory_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdHistory.DblClick
        '  sbSTDisplay()
        Dim sBcNo As String = ""
        Dim sTnmd As String = ""
        Dim sTestCd As String = ""
        Dim sSpcCd As String = ""
        Dim sTCdGbn As String = ""

        Try
            With Me.spdHistory
                If (e.col = .GetColFromID("orgrst") Or e.col = .GetColFromID("tnmd")) And e.row > 0 Then
                    .Row = e.row
                    .Col = .GetColFromID("tcdgbn") : sTCdGbn = .Text

                    .Row = e.row
                    .Col = .GetColFromID("orgrst")
                    If .Text.Trim = "{null}" Then
                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")
                        .Col = .GetColFromID("tnmd") : sTnmd = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : sTestCd = .Text.Replace("-", "")

                        Dim strst As New AxAckResultViewer.STRST01

                        strst.SpecialTestName = sTnmd
                        strst.BcNo = sBcNo
                        strst.TestCd = sTestCd

                        strst.ShowDialog(moForm)

                    End If
                End If
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub FGRST_REF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        moForm = Me
    End Sub
End Class