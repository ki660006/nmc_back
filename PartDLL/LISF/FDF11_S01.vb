Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.DataAccess.C01

Public Class FDF11_S01
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF11_S01.vb, Class : FDF11_S01" + vbTab
    Private mobjDAF As New LISAPP.APP_F_CMT

    Private Sub sbDisplay_slip()
        Dim sFn As String = "sbDisplay_slip"
        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()
            If dt.Rows.Count < 1 Then Return

            cboSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            cboSlip.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
        End Try

    End Sub

    Private Sub sbDisplay_cmt(ByVal rsSlipCd As String)
        Dim sFn As String = "sbDisplay_test"
        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_cmtcont_slip(rsSlipCd)
            spdTest.MaxRows = 0
            If dt.Rows.Count < 1 Then Return

            With spdTest
                .ReDraw = True
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("cmtcd") : .Text = dt.Rows(ix).Item("cmtcd").ToString
                    .Col = .GetColFromID("cmtcont") : .Text = dt.Rows(ix).Item("cmtcont").ToString
                    .Col = .GetColFromID("slipnmd") : .Text = dt.Rows(ix).Item("slipnmd").ToString
                Next

                .ReDraw = False
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            spdTest.ReDraw = False
        End Try
    End Sub

    Private Sub FDF11_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        spdTest.MaxRows = 0

        sbDisplay_slip()
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click

        With spdTest
            Dim iRow As Integer = .ActiveRow

            If iRow < 2 Then Return

            .Row = iRow
            .Col = .GetColFromID("cmtcd") : Dim sCmtcd As String = .Text
            .Col = .GetColFromID("cmtcont") : Dim sCmtCont As String = .Text
            .Col = .GetColFromID("slipnmd") : Dim sSlipNmd As String = .Text

            .Row = iRow - 1
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("cmtcd") : .Text = sCmtcd
            .Col = .GetColFromID("cmtcont") : .Text = sCmtCont
            .Col = .GetColFromID("slipnmd") : .Text = sSlipNmd

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow - 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

        With spdTest
            Dim iRow As Integer = .ActiveRow

            If iRow = .MaxRows Then Return

            .Row = iRow
            .Col = .GetColFromID("cmtcd") : Dim sCmtcd As String = .Text
            .Col = .GetColFromID("cmtcont") : Dim sCmtCont As String = .Text
            .Col = .GetColFromID("slipnmd") : Dim sSlipNmd As String = .Text

            .Row = iRow + 2
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("cmtcd") : .Text = sCmtcd
            .Col = .GetColFromID("cmtcont") : .Text = sCmtCont
            .Col = .GetColFromID("slipnmd") : .Text = sSlipNmd

            .Row = iRow
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sFn As String = "sbDisplay_slip"

        Try

            With spdTest
                For ix = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("cmtcd") : Dim sCode As String = .Text
                    .Col = .GetColFromID("cmtcont") : Dim sName As String = .Text

                    If mobjDAF.TransTestInfo_Dispseql(sCode, ix.ToString, USER_INFO.USRID) = False Then
                        MsgBox("소견코드 [" + sCode + "] 저장시 오류가 발생했습니다.!!")
                        Return
                    End If
                Next
            End With

            MsgBox("정상적으로 저장했습니다.!!")

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbDisplay_cmt(Ctrl.Get_Code(cboSlip))
    End Sub

End Class