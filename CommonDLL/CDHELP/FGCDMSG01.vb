' >> 공통 메세지 팝업

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13

Public Class FGCDMSG01
    Private msGbn As String
    Private mbRtn As Boolean = False

    ' 메세지 팝업 호출
    Public Sub sb_DisplayMsg(ByVal rofrm As Windows.Forms.Form, ByVal rsGubun As String, ByVal rsMsgTxt As String)
        msGbn = "MSG"

        Me.Text = rofrm.Text

        Select Case rsGubun
            Case "I"c
                picMsg.Image = imgMsg.Images.Item(0)
            Case "C"c
                picMsg.Image = imgMsg.Images.Item(1)
            Case "E"c
                picMsg.Image = imgMsg.Images.Item(2)
            Case Else

        End Select

        txtMsg.Text = rsMsgTxt

        btnCok.Visible = False
        btnCancel.Visible = False

        Me.ShowDialog(rofrm)

    End Sub

    Public Function fn_DisplayConfirm(ByVal rofrm As Windows.Forms.Form, ByVal rsGubun As String, ByVal rsMsgTxt As String) As Boolean
        msGbn = "CONFIRM"

        Me.Text = rofrm.Text

        Select Case rsGubun
            Case "I"c
                picMsg.Image = imgMsg.Images.Item(0)
            Case "C"c
                picMsg.Image = imgMsg.Images.Item(1)
            Case "E"c
                picMsg.Image = imgMsg.Images.Item(2)
            Case Else

        End Select

        txtMsg.Text = rsMsgTxt

        btnMok.Visible = False

        Me.ShowDialog(rofrm)

        Return mbRtn
    End Function

    Private Sub FGCDMSG01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If msGbn = "MSG" Then
            btnMok.Focus()
        Else
            btnCok.Focus()
        End If
    End Sub

    Private Sub btnMok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMok.Click
        Me.Close()
    End Sub

    Private Sub picMsg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picMsg.Click

    End Sub

    Private Sub btnCok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCok.Click
        mbRtn = True
        Me.Close()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        mbRtn = False
        Me.Close()
    End Sub
End Class