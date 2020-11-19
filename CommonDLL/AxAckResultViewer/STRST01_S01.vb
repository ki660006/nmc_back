Imports System.Drawing

Public Class STRST01_S01

    Private Sub sbResult_View(ByVal rsFielNm As String)
        Dim sFn As String = "Sub sbResult_View"

        Try

            If IO.File.Exists(rsFielNm) = False Then Exit Sub

            Dim bmpBuf As Bitmap = New Bitmap(rsFielNm)
            Me.picIMG.Image = CType(bmpBuf, Image)

        Catch ex As Exception

        End Try

    End Sub

    Public Function Display_Result(ByVal rsFileName As String) As String
        Dim sFn As String = "Function Display_Result"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbResult_View(rsFileName)

        Catch ex As Exception

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function


    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class