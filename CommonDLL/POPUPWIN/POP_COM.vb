Public Class POP_COM
    Private Const msFile As String = "File : POP_COM.vb, Class : POP_COM" & vbTab

    Public Sub sbPOPUP_UrineTATOverList()

        Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_Urine_TATOverList()

        If dt.Rows.Count < 1 Then Return

        Dim POP As New POPUP_TAT_OVER(dt)
        POP.ShowDialog()

    End Sub

End Class
