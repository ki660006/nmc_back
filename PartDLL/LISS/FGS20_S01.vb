Public Class FGS20_S01

    Private msRetVal As String = ""

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk01.CheckedChanged

    End Sub

    Private Sub chk99_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk99.CheckStateChanged

        If Me.chk99.Checked Then
            Me.txtEtc.Focus()
        End If

    End Sub

    Public Function fnDisplayResult(Optional ByVal rsText As String = "") As String

        If rsText <> "" Then
            Me.chk99.Checked = True
            Me.txtEtc.Text = rsText
        End If

        Me.ShowDialog()

        Return msRetVal

    End Function

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Dim sRetVal As String = ""

        If Me.chk01.Checked Then
            sRetVal += "11"
        End If

        If Me.chk02.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "12"
        End If

        If Me.chk03.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "13"
        End If

        If Me.chk04.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "14"
        End If

        If Me.chk05.Checked Then
            If sRetVal.Trim <> "" Then
                sRetVal += ","
            End If
            sRetVal += "15"
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class