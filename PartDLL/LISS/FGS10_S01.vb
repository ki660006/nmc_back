Imports System.Net
Imports COMMON.CommFN

Public Class FGS10_S01
    Inherits System.Windows.Forms.Form

    Private m_frm As Windows.Forms.Form
    Private m_alCmfInfo As New ArrayList

    Private Sub sbDisplay_CfmCont()

        Dim dt As DataTable

        Try
            Dim sTmp As String = ""

            dt = LISAPP.COMM.cdfn.fnGet_cmtcont_etc("G", False)

            Me.cboCfmcont.Items.Clear()
            Me.cboCfmcont.Items.Add("")
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboCfmcont.Items.Add(dt.Rows(ix).Item("cmtcont").ToString.Trim)
            Next


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub cboCfmcont_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCfmcont.SelectedIndexChanged

        Me.txtCfmCont.Text = Ctrl.Get_Name(Me.cboCfmcont)

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsCfmId As String, ByVal r_alCfmInfo As ArrayList)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        sbDisplay_CfmCont()

        m_alCmfInfo = r_alCfmInfo
        Me.txtUsrId.Text = rsCfmId
        Me.txtUsrId_KeyDown(Me.txtUsrId, New System.Windows.Forms.KeyEventArgs(Windows.Forms.Keys.Enter))

    End Sub

    Private Sub txtDrId_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsrId.Click
        Me.txtUsrId.SelectAll()
        Me.txtUsrId.SelectionStart = 0
    End Sub

    Private Sub txtUsrId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUsrId.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        If Me.txtUsrId.Text = "" Then Me.txtUsrNm.Text = "" : Return

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DoctorList("", Me.txtUsrId.Text)

            If dt.Rows.Count < 1 Then
                Me.txtUsrNm.Text = ""
                Return
            Else
                Me.txtUsrNm.Text = dt.Rows(0).Item("doctornm").ToString
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Try

            If LISAPP.APP_R.AbnFn.fnExe_Abnormal_Cfm(m_alCmfInfo, Me.txtUsrId.Text, Me.txtCfmCont.Text) Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 했습니다.!!")
                Me.Close()
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub FGS10_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.btnExit_Click(Nothing, Nothing)
        End If
    End Sub
End Class