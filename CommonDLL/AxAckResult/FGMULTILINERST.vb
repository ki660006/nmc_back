Imports System.Drawing
Imports COMMON.CommFN

Public Class FGMULTILLINERST
    Private msOrgRst As String = ""
    Private msTestCd As String = ""

    Public Function Display_Result(ByVal rsTestCd As String, ByVal rsOrgRst As String) As String

        msTestCd = rsTestCd
        sbDisplay_Data(rsOrgRst)

        Me.ShowDialog()

        Return msOrgRst

    End Function

    Private Sub sbDisplay_Data(ByVal rsOrgRst As String)

        Me.txtOrgRst.Text = rsOrgRst

        Me.Focus()

    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click

        msOrgRst = txtOrgRst.Text
        Me.Close()

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        msOrgRst = ""
        Me.Close()
    End Sub

    Private Sub FGMULTILLINERST_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Windows.Forms.Keys.F2
                btnReg_Click(Nothing, Nothing)
            Case Windows.Forms.Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnHelp_rst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_rst.Click
        Dim sFn As String = "Handles btnHelp_Cmt.Click"


        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            Dim rsOrgRst As String = ""

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TestRst_list(msTestCd)

            objHelp.FormText = "결과코드"

            objHelp.GroupBy = ""
            objHelp.OrderBy = ""
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("'' chk", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("keypad", "코드", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("rstcont", "내용", 60, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnHelp_rst)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + btnHelp_rst.Height + 80, dt)

            Dim sRstVal As String = ""

            If alList.Count > 0 Then

                For ix = 0 To alList.Count - 1
                    If ix <> 0 Then sRstVal += vbCrLf
                    sRstVal += alList.Item(ix).ToString.Split("|"c)(1)
                Next
            End If

            '< 20121009 멀티라인입력시 ADD기능 추가
            If ChkAdd.Checked Then
                If Trim(Me.txtOrgRst.Text) = "" Then '<2012
                    Me.txtOrgRst.Text = sRstVal
                Else
                    Me.txtOrgRst.Text = Me.txtOrgRst.Text + vbCrLf + sRstVal
                End If
            Else
                Me.txtOrgRst.Text = sRstVal
            End If



        Catch ex As Exception
        End Try
    End Sub


End Class