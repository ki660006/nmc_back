Imports System.Drawing
Imports System.Windows.Forms

Public Class FGC31_S04

    Public Function Display_Data(ByVal roForm As Windows.Forms.Form, ByVal rsBcNo As String) As ArrayList

        Dim dt As New DataTable
        Dim aryList As New ArrayList

        Try

            dt = LISAPP.APP_S.CollTkFn.fnGet_Reject_Rstval(rsBcNo)
            sbDisplay_ResultView(dt)

            Me.ShowDialog(roForm)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return New ArrayList
        End Try

    End Function

    Private Sub sbDisplay_ResultView(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub sbDisplay_ResultView(DataTable)"

        Try
            With Me.spdHistory

                .MaxRows = 0

                '.ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For iRow As Integer = 1 To r_dt.Rows.Count
                    If iRow = 1 Then
                        Me.spdPatInfo.Row = 1
                        Me.spdPatInfo.Col = Me.spdPatInfo.GetColFromID("regno") : Me.spdPatInfo.Text = r_dt.Rows(iRow - 1).Item("regno").ToString
                        Me.spdPatInfo.Col = Me.spdPatInfo.GetColFromID("patnm") : Me.spdPatInfo.Text = r_dt.Rows(iRow - 1).Item("patinfo").ToString.Split("|"c)(0)
                        Me.spdPatInfo.Col = Me.spdPatInfo.GetColFromID("idno") : Me.spdPatInfo.Text = r_dt.Rows(iRow - 1).Item("patinfo").ToString.Split("|"c)(3)
                        Me.spdPatInfo.Col = Me.spdPatInfo.GetColFromID("sexage") : Me.spdPatInfo.Text = r_dt.Rows(iRow - 1).Item("sexage").ToString
                        Me.spdPatInfo.Col = Me.spdPatInfo.GetColFromID("orddt") : Me.spdPatInfo.Text = r_dt.Rows(iRow - 1).Item("orddt").ToString
                        Me.spdPatInfo.Col = Me.spdPatInfo.GetColFromID("dept") : Me.spdPatInfo.Text = r_dt.Rows(iRow - 1).Item("deptward").ToString
                        Me.spdPatInfo.Col = Me.spdPatInfo.GetColFromID("doctor") : Me.spdPatInfo.Text = r_dt.Rows(iRow - 1).Item("drnm").ToString
                    End If

                    .Row = iRow
                    .Col = .GetColFromID("chk") : .Text = ""

                    For ix As Integer = 1 To r_dt.Columns.Count
                        Dim iCol As Integer = 0

                        iCol = .GetColFromID(r_dt.Columns(ix - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Row = iRow
                            .Col = iCol
                            Select Case iCol
                                Case .GetColFromID("hlmark")
                                    .Text = r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                    If r_dt.Rows(iRow - 1).Item(ix - 1).ToString() = "L" Then
                                        .BackColor = Color.FromArgb(221, 240, 255)
                                        .ForeColor = Color.FromArgb(0, 0, 255)
                                    ElseIf r_dt.Rows(iRow - 1).Item(ix - 1).ToString() = "H" Then
                                        .BackColor = Color.FromArgb(255, 230, 231)
                                        .ForeColor = Color.FromArgb(255, 0, 0)
                                    End If

                                Case .GetColFromID("panicmark")
                                    .Text = r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                    If r_dt.Rows(iRow - 1).Item(ix - 1).ToString() = "P" Then
                                        .BackColor = Color.FromArgb(150, 150, 255)
                                        .ForeColor = Color.FromArgb(255, 255, 255)
                                    End If

                                Case .GetColFromID("deltamark")
                                    .Text = r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                    If r_dt.Rows(iRow - 1).Item(ix - 1).ToString() = "D" Then
                                        .BackColor = Color.FromArgb(150, 255, 150)
                                        .ForeColor = Color.FromArgb(0, 128, 64)
                                    End If

                                Case .GetColFromID("criticalmark")
                                    .Text = r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                    If r_dt.Rows(iRow - 1).Item(ix - 1).ToString() = "C" Then
                                        .BackColor = Color.FromArgb(255, 150, 255)
                                        .ForeColor = Color.FromArgb(255, 255, 255)
                                    End If

                                Case .GetColFromID("alertmark")
                                    .Text = r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                    If r_dt.Rows(iRow - 1).Item(ix - 1).ToString() = "A" Then
                                        .BackColor = Color.FromArgb(255, 255, 150)
                                        .ForeColor = Color.FromArgb(0, 0, 0)
                                    End If

                                Case .GetColFromID("tnmd")
                                    If r_dt.Rows(iRow - 1).Item("tcdgbn").ToString() = "C" Then
                                        If r_dt.Rows(iRow - 1).Item("testcd").ToString.Trim = r_dt.Rows(iRow - 1).Item("tclscd").ToString.Trim Then
                                            .Text = "... " + r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                        Else
                                            .Text = "  ... " + r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                        End If
                                    ElseIf r_dt.Rows(iRow - 1).Item("testcd").ToString.Trim = r_dt.Rows(iRow - 1).Item("tclscd").ToString.Trim Then
                                        .Text = r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                    Else
                                        .Text = "  " + r_dt.Rows(iRow - 1).Item(ix - 1).ToString()
                                    End If

                                Case Else
                                    .Text = r_dt.Rows(iRow - 1).Item(ix - 1).ToString()

                            End Select
                        End If
                    Next
                Next
            End With

        Catch ex As Exception

        Finally
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGS07_01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)

        End Select
    End Sub
End Class


