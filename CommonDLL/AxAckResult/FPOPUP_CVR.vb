Imports System.Drawing

Imports COMMON.CommFN
Imports CDHELP.FGCDHELPFN

Public Class FPOPUP_CVR
    Dim m_al_List As ArrayList
    Private moForm As Windows.Forms.Form
    Private msBcno As String = ""

    Public Sub Display_Data(ByVal roForm As Windows.Forms.Form, ByVal rsBcNo As String)

        Dim dt As New DataTable
        Dim aryList As New ArrayList

        Try
            '<< 2020-06-02 JJH CVR 리스트 조회
            Display_Data(rsBcNo)
            msBcno = rsBcNo

            Me.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub Display_Data(ByVal sBcno As String)

        Try

            Dim dt As New DataTable

            dt = LISAPP.APP_R.RstFn.fnGet_CVRList(sBcno)
            sbDisplay_Data(dt)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
   
    Private Sub sbDisplay_Data(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub sbDisplay_ResultView(DataTable)"

        Try
            With Me.spdcvrList

                .MaxRows = 0

                If r_dt.Rows.Count <= 0 Then Return

                '.ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For ix As Integer = 1 To r_dt.Rows.Count
                    .Row = ix

                    .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix - 1).Item("tnmd").ToString
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix - 1).Item("testcd").ToString
                    .Col = .GetColFromID("rst") : .Text = r_dt.Rows(ix - 1).Item("rst").ToString
                    .Col = .GetColFromID("rstunit") : .Text = r_dt.Rows(ix - 1).Item("rstunit").ToString
                    .Col = .GetColFromID("regnm") : .Text = r_dt.Rows(ix - 1).Item("regnm").ToString
                    .Col = .GetColFromID("regdt") : .Text = r_dt.Rows(ix - 1).Item("regdt").ToString
                    .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(ix - 1).Item("fkocs").ToString

                Next


            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGRST_REF_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then Me.Close()
    End Sub

    Private Sub FGRST_REF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        moForm = Me
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Try
            If Me.spdcvrList.MaxRows = 0 Then CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "취소할 항목이 없습니다.") : Return

            Dim isOk As Boolean = fn_PopConfirm(Me, "I"c, "CVR 취소하시겠습니까?")
            If isOk Then

                Dim alCancel As New ArrayList

                With Me.spdcvrList
                    For ix As Integer = 1 To .MaxRows

                        .Row = ix
                        .Col = .GetColFromID("chk") : Dim chk As String = .Text

                        Dim CvrInfo As New LIS_CVR_INFO

                        If chk = "1" Then

                            .Col = .GetColFromID("fkocs") : CvrInfo.Fkocs = .Text
                            .Col = .GetColFromID("testcd") : CvrInfo.Testcd = .Text

                            alCancel.Add(CvrInfo)

                        End If
                    Next

                    If alCancel.Count > 0 Then

                        With (New LISAPP.APP_R.AxRstFn)

                            If .fnCancel_CVR(msBcno, alCancel) = "" Then
                                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "취소되었습니다.")
                            End If

                        End With
                    Else
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "선택된 항목이 없습니다.")
                    End If

                End With

            End If

            Display_Data(msBcno)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class