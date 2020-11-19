'>> 신종코로나 I/F 결과값 이력관리
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommFN
Imports COMMON.CommConst
Imports SYSIF01
Imports LISAPP

Public Class FGR08_S05
    Private Const msFile As String = "File : FGR08_04.vb, Class : FGR08_04" & vbTab
    Private msEmrPrintName As String = ""
    Private msRegno As String = ""

    Public Sub Display_Data(ByVal sRegno As String)

        Try

            msRegno = sRegno

            Dim obj As New LISAPP.APP_R.RstFn
            Dim dt As DataTable = obj.fnGet_nCov_IFResult(sRegno)

            fnDisplay_Result(dt)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub fnDisplay_Result(ByVal dt As DataTable)

        Try

            With spdList

                .MaxRows = dt.Rows.Count
                If dt.Rows.Count <= 0 Then Return

                For ix As Integer = 0 To dt.Rows.Count - 1

                    .Row = ix + 1
                    .Col = .GetColFromID("chk") : .Text = "0"
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(ix).Item("spcnmd").ToString
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("rstgbn") : .Text = dt.Rows(ix).Item("gbn").ToString
                    .Col = .GetColFromID("rst") : .Text = dt.Rows(ix).Item("rst").ToString
                    .Col = .GetColFromID("rstdt") : .Text = dt.Rows(ix).Item("rstdt").ToString
                    .Col = .GetColFromID("rstflg") : .Text = dt.Rows(ix).Item("flag").ToString
                    .Col = .GetColFromID("prtno") : .Text = dt.Rows(ix).Item("prtno").ToString
                    .Col = .GetColFromID("gbn") : .Text = dt.Rows(ix).Item("rstgbn").ToString
                    .Col = .GetColFromID("seq") : .Text = dt.Rows(ix).Item("seq").ToString
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                    .Col = .GetColFromID("dtgbn") : .Text = dt.Rows(ix).Item("dtgbn").ToString

                Next

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sFn As String = "btnUpload_click()"

        Try

            With spdList

                If spdList.MaxRows < 1 Then MsgBox("조회된 자료가 없습니다.", MsgBoxStyle.Information, Me.Text) : Return

                .ReDraw = False
                .Col = .GetColFromID("chk") : .ColHidden = True

                .Row = 1
                .MaxRows += 1
                .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                For intCol As Integer = 1 To .MaxCols
                    .Row = 0 : .Col = intCol : Dim strTmp As String = .Text
                    .Row = 1 : .Col = intCol : .Text = strTmp
                Next


                If spdList.ExportToExcel("Ncov_his.xls", "Ncov history", "") Then
                    Process.Start("Ncov_his.xls")
                End If

                .Col = .GetColFromID("chk") : .ColHidden = False

                .Row = 1
                .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                .MaxRows -= 1

                .SetActiveCell(.GetColFromID("chk"), .Row)

                .ReDraw = True

            End With

        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub FGR08_S04_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        Me.Close()

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            Dim arList As New ArrayList

            With spdList

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : Dim chk = .Text

                    Dim NCOV_C As New NCOV_Cancel

                    If chk = "1" Then

                        .Col = .GetColFromID("rstflg") : Dim Flag = .Text

                        If Flag = "Y" Then
                            
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "최종보고 된 결과는 삭제할 수 없습니다.")
                            .Col = .GetColFromID("chk") : .Text = "0"
                            Return
                        End If

                        .Col = .GetColFromID("bcno") : NCOV_C.sBcno = .Text
                        .Col = .GetColFromID("testcd") : NCOV_C.sTestcd = .Text
                        .Col = .GetColFromID("gbn") : NCOV_C.sGbn = .Text
                        .Col = .GetColFromID("seq") : NCOV_C.sSeq = .Text
                        .Col = .GetColFromID("dtgbn") : NCOV_C.sDtgbn = .Text

                        arList.Add(NCOV_C)

                    End If

                Next
            End With

            If arList.Count > 0 Then
                If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "삭제 하시겠습니까?") Then

                    With (New LISAPP.APP_R.AxRstFn)
                        Dim Msg As String = .fnCancel_NCOV(arList, msRegno)
                        If Msg = "" Then
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "삭제되었습니다.")
                        Else
                            MsgBox(Msg)
                        End If

                    End With

                    Display_Data(msRegno)

                End If
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택된 항목이 없습니다.")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

End Class