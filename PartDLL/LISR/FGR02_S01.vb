Public Class FGR02_S01
    Public msAction As String = ""

    Public Function Display_Result() As String

        Me.ShowDialog()

        Return msAction

    End Function

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click

        Dim sDate As String = Format$(dtpChgDate.Value, "yyyy-MM-dd").Replace("-", "")
        Dim sTIme As String = Format$(dtpChgTime.Value, "HH:mm:ss").Replace(":", "")
        msAction = sDate + sTIme
        Me.Close()

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        msAction = ""
        Me.Close()

    End Sub

    Private Sub FGR02_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                btnReg_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGR02_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        dtpChgDate.Value = CDate(Format(Now, "yyyy-MM-dd") + " 00:00:00")
        dtpChgTime.Value = CDate(Format(Now, "yyyy-MM-dd") + " 00:00:00")

    End Sub
End Class