Public Class FGMSGDELAY

    Private Const miTerm As Integer = 30
    Private m_timer As Threading.Timer
    Private miTimer As Integer = 0
    Private mbCancel As Boolean = False

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form) As String
        Dim sFn As String = "Function Display_Result"

        Try

            Me.ShowDialog()

            Dim sReturn As String = "OK"

            If mbCancel Then
                sReturn = ""
            End If

            Return sReturn

        Catch ex As Exception
            Return ""
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function

    Private Sub sbTimerProc()
        If miTerm - miTimer < 1 Then
            If Not m_timer Is Nothing Then
                m_timer.Change(Threading.Timeout.Infinite, Threading.Timeout.Infinite)
                m_timer.Dispose()
            End If

            Me.Close()

            Return
        End If

        Me.btnClose.Text = (miTerm - miTimer).ToString & "초 후에 자동 로그아웃 됩니다." + vbCrLf + "로그아웃을 취소 하려면 여기를 누르세요."
        Me.btnClose.Refresh()

        miTimer += 1
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        sbTimerProc()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        mbCancel = True
        miTimer = miTerm

        sbTimerProc()
    End Sub

    Private Sub MSGDELAY_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        sbTimerProc()
    End Sub
End Class