Imports System.Drawing

Public Class FGMODIFY
    Dim m_al_List As ArrayList

    Public Sub Display_Data(ByVal roForm As Windows.Forms.Form, ByVal rsBcNo As String)

        Dim dt As New DataTable
        Dim aryList As New ArrayList

        Try
            dt = LISAPP.COMM.RstFn.fnGet_FN_Modify_Cmt(rsBcNo)
            sbDisplay_ResultView(dt)

            Me.ShowDialog(roForm)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
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

                    For intIx1 As Integer = 1 To r_dt.Columns.Count
                        Dim intCol As Integer = 0

                        intCol = .GetColFromID(r_dt.Columns(intIx1 - 1).ColumnName.ToLower())

                        If intCol > 0 Then
                            .Row = introw
                            .Col = intCol
                            .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString()
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
End Class