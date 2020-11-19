Imports COMMON.CommFN
Imports common.commlogin.login

Public Class FPOPUPCOMMENT
    Private Const mcFile As String = "File : POPUPWIN.vb, Class : FPOPUPCOMMENT" + vbTab

    Private msTitle As String
    Private msRegNo As String
    Private msIOGbn As String

    Private miCommentGbn As Integer

    Public WriteOnly Property Title() As String
        Set(ByVal Value As String)
            msTitle = Value
        End Set
    End Property

    Public WriteOnly Property RegNo() As String
        Set(ByVal Value As String)
            msRegNo = Value
        End Set
    End Property

    Public WriteOnly Property CmtGbn() As Integer
        Set(ByVal Value As Integer)
            miCommentGbn = Value
        End Set
    End Property

    Public WriteOnly Property IOGBN() As String
        Set(ByVal Value As String)
            msIOGbn = Value
        End Set
    End Property

    Public Sub Init()
        Dim sFn As String = "Init"

        Try
            msTitle = ""
            msRegNo = ""
            miCommentGbn = 0

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub sbLoad()
        Dim sFn As String = ""

        sbDisplay()

        Me.Show()
    End Sub

    Public Sub sbDisplay()
        Dim sFn As String = "sbDisplay"

        Try
            '< REGNO
            If msRegNo = "" Then Exit Sub
            Me.txtRegNo.Text = msRegNo
            '> 

            '< TITLE
            Me.Text = msTitle
            '>

            '< Comment Code  
            Dim dt As New DataTable
            Dim objCollTkCd As New LISAPP.APP_F_COLLTKCD

            dt = objCollTkCd.fnGet_CollTK_Cancel_ContInfo(miCommentGbn.ToString())

            cboCmtcd.Items.Clear()

            If dt.Rows.Count > 0 Then
                For iCnt As Integer = 0 To dt.Rows.Count - 1
                    Dim sBuf As String = ""

                    'CMTCD , CMTCONT
                    '"[ " & dt.Rows(iCnt).Item("CMTCD").ToString() & " ]" & 
                    sBuf = dt.Rows(iCnt).Item("CMTCONT").ToString()

                    cboCmtcd.Items.Add(sBuf)
                Next

                cboCmtcd.SelectedIndex = 0
            End If
            '>  

            Dim sCmt As String = LISAPP.APP_C.Collfn.fnGet_Comment_pat(msIOGbn, txtRegNo.Text)
            If sCmt <> "" Then
                Me.cboCmtcd.SelectedIndex = -1
                Me.txtComment.Text = sCmt
            End If

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim sFn As String = "Handles btnExit.ButtonClick"

        Try
            Me.Close()
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub cboCmtcd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCmtcd.SelectedIndexChanged
        Dim sFn As String = "cboCmtcd_SelectedIndexChanged"

        Try
            If cboCmtcd.SelectedIndex > -1 Then
                Dim sCmt As String = Me.cboCmtcd.Text

                Me.txtComment.Text = sCmt

                Me.txtComment.Focus()
            Else
                MsgBox("코드가 존재 하지 않습니다.! " & _
                           "관리자에게 문의 하시기 바랍니다!!", _
                           MsgBoxStyle.Information, msTitle & "코드 미존재")
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sFn As String = "Handles btnSave.ButtonClick"

        Try
            Dim iReturn As Integer
            Dim objSpcComment As New LISAPP.APP_C.SpCmtReg

            iReturn = objSpcComment.Reg_SpecalComment(Me.txtRegNo.Text.Trim(), Me.txtComment.Text.Trim(), msIOGbn, _
                                                      miCommentGbn, USER_INFO.USRID)

            MsgBox("등록되었습니다.", MsgBoxStyle.Information, msTitle)

            Me.Close()

        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            MsgBox(mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub FPOPUPCOMMENT_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.txtComment.Focus()
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Dim sFn As String = "Handles btnDel.ButtonClick"

        Dim iReturn As Integer
        Dim objSpcComment As New LISAPP.APP_C.SpCmtReg

        iReturn = objSpcComment.Reg_SpecalComment(Me.txtRegNo.Text.Trim(), Me.txtComment.Text.Trim(), msIOGbn, _
                                                      miCommentGbn, USER_INFO.USRID, True)

        MsgBox("삭제되었습니다.", MsgBoxStyle.Information, msTitle)

        Me.Close()

    End Sub
 
    Private Sub FPOPUPCOMMENT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.Escape
                btnExit_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.F2
                btnSave_Click(Nothing, Nothing)
        End Select

    End Sub
End Class