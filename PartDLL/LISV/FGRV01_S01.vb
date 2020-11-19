Imports System.Net

Public Class FGRV01_S01
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGRV01_S01.vb, Class : FGRV01_S01" & vbTab
    Private m_dt_abn As New DataTable


    Public Sub sbDisplay_Result()
        Dim sFn As String = "Function Display_Result"

        Try

            Me.txtCont.Text = ""
            For ix As Integer = 0 To m_dt_abn.Rows.Count - 1

                If ix = 0 Then
                    Me.lblRegDt.Text = m_dt_abn.Rows(ix).Item("regdt").ToString.Substring(0, 16)
                    Me.lblRegDt.Tag = m_dt_abn.Rows(ix).Item("regdt").ToString.Trim
                    Me.lblRegNm.Text = m_dt_abn.Rows(ix).Item("regnm").ToString.Trim
                    Me.lblBcNo.Text = m_dt_abn.Rows(ix).Item("bcno").ToString.Trim
                    Me.lblOrdDt.Text = m_dt_abn.Rows(ix).Item("orddt").ToString.Trim
                    Me.lblDoctorNm.Text = m_dt_abn.Rows(ix).Item("doctornm").ToString.Trim
                    Me.lblDptWard.Text = m_dt_abn.Rows(ix).Item("dptward").ToString.Trim
                End If

                If m_dt_abn.Rows.Count > 1 Then
                    Me.txtCont.Text += "통보일시: " + m_dt_abn.Rows(ix).Item("regdt").ToString.Substring(0, 16) + Space(10) + "통보자: " + m_dt_abn.Rows(ix).Item("regnm").ToString.Trim + vbCrLf
                    Me.txtCont.Text += "내    용: " + vbCrLf
                    Me.txtCont.Text += m_dt_abn.Rows(ix).Item("cmtcont").ToString.Trim + vbCrLf
                Else
                    Me.txtCont.Text += m_dt_abn.Rows(ix).Item("cmtcont").ToString.Trim
                End If

            Next

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)


        End Try
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal r_dt As DataTable)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        m_dt_abn = r_dt

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Try
            Dim sRegNo As String = m_dt_abn.Rows(0).Item("regno").ToString

            If LISAPP.APP_R.AbnFn.fnExe_Abnormal_Cfm(sRegNo, Me.txtUsrId.Text, Me.txtCfnCont.Text) Then
                Me.Close()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGRV01_S01_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        sbDisplay_Result()

    End Sub
End Class