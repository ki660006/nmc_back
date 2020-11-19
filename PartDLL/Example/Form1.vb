Imports COMMON.SVar.Login
Imports COMMON.CommFN

Public Class Form1
    Private Sub sbDisplay_TGrp()

        Dim dt As DataTable = DA01.CommQry.DA_LF.fnGet_Slip_List()

        cboTgrpCd.Items.Clear()

        If dt.Rows.Count < 1 Then Return

        For ix As Integer = 0 To dt.Rows.Count - 1
            cboTgrpCd.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "]  " + dt.Rows(ix).Item("slipnmd").ToString)
        Next

    End Sub

    Private Sub FGR99_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        sbDisplay_TGrp()

        With axItemSave
            .FORMID = Me.Name
            .USRID = USER_INFO.USRID
            .SPCGBN = "NONE"
        End With

    End Sub

    Private Sub cboTgrpCd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTgrpCd.SelectedIndexChanged

        axItemSave.ITEMGBN = Ctrl.Get_Code(cboTgrpCd)

    End Sub
End Class