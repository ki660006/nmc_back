Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class FGLOGIN_S01
    Private Const sFile As String = "File : FGLOGIN01_S01.vb, Class : LOGIN01" & vbTab

    Public msUID As String = ""
    Public msUPW As String = ""

    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"

        Try
            ' 화면 정리
            Me.Tag = "Load"

            Me.txtUsrPw1.Text = ""
            Me.txtUsrPw2.Text = ""

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Function fnValidation() As Boolean
        Dim sFn As String = "Private Function fnValidation() As Boolean"

        fnValidation = False
        Try

            If Trim(Me.txtUsrPw2.Text) = "" Then
                MsgBox("비밀번호 확인을 입력해 주십시오", MsgBoxStyle.Information, Me.Text)
                txtUsrPw2.Focus()
                Exit Function
            End If

            If Me.txtUsrPw1.Text <> Me.txtUsrPw2.Text Then
                MsgBox("비밀번호가 일치하지 않습니다. 확인해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Me.txtUsrPw2.Focus()
                Exit Function
            End If

            If (New HashMD5).Encrypt(USER_INFO.USRID, Me.txtUsrPw2.Text) = USER_INFO.USRPW_OLD Then
                MsgBox("이전 비밀번호와 같습니다.!!" + vbCrLf + "이전 비밀번호와 다른 비밀번호로 입력해 주세요.!!.", MsgBoxStyle.Information, Me.Text)
                Me.txtUsrPw2.Focus()
                Exit Function
            End If

            fnValidation = True

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        msUPW = ""
        Me.Close()

    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If fnValidation() = False Then Exit Sub

        msUPW = ""
        If LOGIN.CONFIG.FN.fnExe_NewUsrPWD(msUID, txtUsrPw2.Text) = True Then
            ' 비밀번호 정상적으로 등록
            msUPW = txtUsrPw2.Text
        End If

        Me.Close()

    End Sub

    Private Sub FGLOGIN_S01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If CType(Me.Tag, String) = "Load" Then

            If msUPW = "" Then
                Me.txtUsrPw1.Enabled = True
                Me.txtUsrPw1.Focus()
            Else
                Me.txtUsrPw1.Text = msUPW
                Me.txtUsrPw2.Focus()
            End If

            Me.Tag = ""
        End If

    End Sub

    Private Sub FGLOGIN_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            btnCancel_Click(Nothing, Nothing)
        End If

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        sbFormInitialize()
    End Sub

    Private Sub txtUsrPw2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsrPw2.GotFocus, txtUsrPw1.GotFocus
        CType(sender, Windows.Forms.TextBox).SelectAll()
    End Sub

    Private Sub txtUsrPw1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsrPw1.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Me.txtUsrPw2.Focus()

    End Sub

    Private Sub txtUsrPw2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsrPw2.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        btnOk_Click(Nothing, Nothing)
    End Sub

End Class