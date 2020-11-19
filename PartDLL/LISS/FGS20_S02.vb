Public Class FGS20_S02
    Private msRetVal As String = ""

    Private Sub chk99_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk99.CheckStateChanged

        If Me.chk99.Checked Then
            Me.txtEtc.Focus()
        End If

    End Sub

    Public Function fnDisplayResult() As String

        Me.ShowDialog()

        Return msRetVal

    End Function

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Dim sRetVal As String = ""

        If Me.chk01.Checked Then
            sRetVal += "01"
        End If

        If Me.chk02.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "02"
        End If

        If Me.chk03.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "03"
        End If

        If Me.chk04.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "04"
        End If

        If Me.chk05.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "05"
        End If

        If Me.chk99.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "99"
            sRetVal = sRetVal + ":" + Me.txtEtc.Text
        End If

        msRetVal = sRetVal


        Me.Close()

    End Sub

End Class