Imports System.Windows.Forms
Imports COMMON.CommFN
Imports COMMON.SVar
Imports LISAPP.APP_S.RstSrh
Imports COMMON.CommLogin.LOGIN
Imports System.Drawing


Public Class FGS20_S03

    Private msRetVal As String = ""

    Private Sub cboGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboGroup.SelectedIndexChanged

        Dim st As String = Me.cboGroup.SelectedIndex.ToString

        Dim dt As DataTable = fnRefList((Me.cboGroup.SelectedIndex).ToString)

        Me.cboRef.Items.Clear()

        'Return
        Me.cboRef.Items.Add("선택하세요")

        For ix As Integer = 0 To dt.Rows.Count - 1
            Me.cboRef.Items.Add("[" + dt.Rows(ix).Item("refcd").ToString + "] " + dt.Rows(ix).Item("refnm").ToString)
        Next

        Me.cboRef.SelectedIndex = 0

    End Sub

    Public Function fnDisplayResult() As String

        Me.cboGroup.SelectedIndex = 0

        Me.ShowDialog()

        Return msRetVal

    End Function

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Dim sRetVal As String = ""

        msRetVal = Me.txtRef.Text

        Me.Close()

    End Sub
   
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If Me.txtRef.Text <> "" Then
            Me.txtRef.Text = Me.txtRef.Text + " , " + Ctrl.Get_Code(Me.cboRef)
        Else
            Me.txtRef.Text = Ctrl.Get_Code(Me.cboRef)
        End If


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.txtRef.Text = ""
    End Sub

    Private Sub cboRef_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRef.SelectedIndexChanged
        If Me.txtRef.Text <> "" Then
            Me.txtRef.Text = Me.txtRef.Text + " , " + Ctrl.Get_Code(Me.cboRef)
        Else
            Me.txtRef.Text = Ctrl.Get_Code(Me.cboRef)
        End If
    End Sub

    Private Sub CButton1_ClickButtonArea(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles CButton1.ClickButtonArea
        Me.Close()
    End Sub
End Class