Imports LISC
'Imports DBORA
Imports System.IO
Imports System.Data
Imports Oracle.DataAccess.Client
Imports Oracle.DataAccess.Types


Public Class FGO99

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim frm As New FGC99

        frm.ShowDialog()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim frm As New FGR99

        frm.ShowDialog()
    End Sub

    '20210222 jhs 테스트를 위해 추가
    'Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
    '    Dim db_info As New DBORA.

    '    Dim teststr1 As String = db_info.GetBCPrtToView("11232102480")

    '    Dim teststr As DataTable = db_info.BarcordScan_patinfo("20120102H102480")

    '    Dim NUM As Integer = teststr.Rows.Count

    'End Sub
    '------------------------------------------


End Class