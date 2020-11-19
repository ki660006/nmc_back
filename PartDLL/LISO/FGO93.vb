Public Class FGO93
    Private Const mc_sFile As String = "File : FGF03.vb, Class : FGF03" + vbTab

    Private Sub FGF03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.mtbUseDtA.Text = ""
    End Sub

    Private Sub FGF03_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Text = Me.btnEditUseDt.Text + " / " + Me.btnDelCd.Text
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Owner.AccessibleName = ""

        Me.Close()
    End Sub

    Private Sub btnDelCd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelCd.Click
        Dim sMsg As String = ""

        sMsg = ""
        sMsg += Me.lblCd.Text + " : " + Me.txtCd.Text + vbCrLf
        sMsg += Me.lblNm.Text + " : " + Me.txtNm.Text + vbCrLf + vbCrLf
        sMsg += "의 " + Me.btnDelCd.Text.Replace("코드", "코드를") + "하시겠습니까?" + vbCrLf + vbCrLf + vbCrLf
        sMsg += ">>> " + Me.btnDelCd.Text + "는 주의를 요하는 작업이므로 신중히 실행하시기 바랍니다!!" + vbTab + vbCrLf

        If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo, Me.btnDelCd.Text + " 확인") = MsgBoxResult.No Then Return

        Me.Owner.AccessibleName = Date.MinValue.ToString("yyyy-MM-dd HH:mm:ss")

        Me.Close()
    End Sub

    Private Sub btnEditUseDt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditUseDt.Click
        If IsDate(Me.mtbUseDtA.Text) = False Then
            MsgBox(Me.lblUseDtA.Text + " : " + Me.mtbUseDtA.Text + "은(는) 날짜 형식이 아닙니다. 확인하여 주십시요!!", MsgBoxStyle.Information)

            Return
        End If

        If CDate(Me.txtUseDt.Text).ToString("yyyy-MM-dd HH:mm:ss") = CDate(Me.mtbUseDtA.Text).ToString("yyyy-MM-dd HH:mm:ss") Then
            MsgBox(Me.lblUseDtA.Text + " : " + Me.mtbUseDtA.Text + "은(는) 변경 전과 동일합니다. 확인하여 주십시요!!", MsgBoxStyle.Information)

            Return
        End If

        Dim sMsg As String = ""

        sMsg = ""
        sMsg += Me.lblCd.Text + " : " + Me.txtCd.Text + vbCrLf
        sMsg += Me.lblNm.Text + " : " + Me.txtNm.Text + vbCrLf + vbCrLf
        sMsg += "의 " + Me.btnEditUseDt.Text.Replace("일시", "일시를") + "하시겠습니까?" + vbCrLf + vbCrLf + vbCrLf
        sMsg += ">>> " + Me.btnEditUseDt.Text + "은 주의를 요하는 작업이므로 신중히 실행하시기 바랍니다!!" + vbTab + vbCrLf

        If MsgBox(sMsg, MsgBoxStyle.Exclamation Or MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.YesNo, Me.btnEditUseDt.Text + " 확인") = MsgBoxResult.No Then Return

        Me.Owner.AccessibleName = CDate(Me.mtbUseDtA.Text).ToString("yyyy-MM-dd HH:mm:ss")

        Me.Close()
    End Sub
End Class