Imports System.Windows.Forms

Public Class FGB14_S01
    Private Const msFile As String = "File : LISB.vb, Class : FGB14_S01" & vbTab

    Private mbSave As Boolean = False
    Private msSelLists As String = ""

    Private Sub sbDisplay_init()

        With spdList
            .MaxRows = 19

            .Row = 1
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "성별/나이"
            .Col = .GetColFromID("width") : .Text = "50"
            .Col = .GetColFromID("field") : .Text = "sexage"

            .Row = 2
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "처방일시"
            .Col = .GetColFromID("width") : .Text = "80"
            .Col = .GetColFromID("field") : .Text = "orddt"

            .Row = 3
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "의뢰의사"
            .Col = .GetColFromID("width") : .Text = "60"
            .Col = .GetColFromID("field") : .Text = "doctor"

            .Row = 4
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "진료과/병동"
            .Col = .GetColFromID("width") : .Text = "120"
            .Col = .GetColFromID("field") : .Text = "dept"

            .Row = 5
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "구분"
            .Col = .GetColFromID("width") : .Text = "40"
            .Col = .GetColFromID("field") : .Text = "tnsgbn"

            .Row = 6
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "수혈의뢰번호"
            .Col = .GetColFromID("width") : .Text = "120"
            .Col = .GetColFromID("field") : .Text = "tnsjubsuno"

            .Row = 7
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "수혈자 혈액형"
            .Col = .GetColFromID("width") : .Text = "50"
            .Col = .GetColFromID("field") : .Text = "aborh"

            .Row = 8
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "혈액 혈액형"
            .Col = .GetColFromID("width") : .Text = "50"
            .Col = .GetColFromID("field") : .Text = "aborhBld"

            .Row = 9
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "접수일시"
            .Col = .GetColFromID("width") : .Text = "80"
            .Col = .GetColFromID("field") : .Text = "jubsudt"

            .Row = 10
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "검사일시"
            .Col = .GetColFromID("width") : .Text = "160"
            .Col = .GetColFromID("field") : .Text = "testdt"

            .Row = 11
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "검사자"
            .Col = .GetColFromID("width") : .Text = "60"
            .Col = .GetColFromID("field") : .Text = "testnm"

            .Row = 12
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "검사결과"
            .Col = .GetColFromID("width") : .Text = "80"
            .Col = .GetColFromID("field") : .Text = "rst1"

            .Row = 13
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "가출고일시"
            .Col = .GetColFromID("width") : .Text = "160"
            .Col = .GetColFromID("field") : .Text = "befoutdt"

            .Row = 14
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "가출고자"
            .Col = .GetColFromID("width") : .Text = "60"
            .Col = .GetColFromID("field") : .Text = "befoutid"

            .Row = 15
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "출고일시"
            .Col = .GetColFromID("width") : .Text = "160"
            .Col = .GetColFromID("field") : .Text = "outdt"

            .Row = 16
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "출고자"
            .Col = .GetColFromID("width") : .Text = "60"
            .Col = .GetColFromID("field") : .Text = "outid"

            .Row = 17
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "수령자"
            .Col = .GetColFromID("width") : .Text = "110"
            .Col = .GetColFromID("field") : .Text = "recnm"

            .Row = 18
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "반납/페기 중단일시"
            .Col = .GetColFromID("width") : .Text = "160"
            .Col = .GetColFromID("field") : .Text = "rtndt"

            .Row = 19
            .Col = .GetColFromID("chk") : .Text = ""
            .Col = .GetColFromID("title") : .Text = "반납/페기 중단자"
            .Col = .GetColFromID("width") : .Text = "160"
            .Col = .GetColFromID("field") : .Text = "rtnnm"

        End With
    End Sub

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form) As String
        Dim sFn As String = "Function Display_Result"


        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplay_init()

            Me.Cursor = Windows.Forms.Cursors.Default

            Me.ShowDialog(r_frm)

            If mbSave Then
                Return msSelLists
            End If
        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try

    End Function

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim sngTotal As Single = 0

        With spdList
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Row = intRow
                .Col = .GetColFromID("chk") : Dim strChk As String = .Text
                .Col = .GetColFromID("title") : Dim strTitle As String = .Text
                .Col = .GetColFromID("field") : Dim strField As String = .Text
                .Col = .GetColFromID("width") : Dim strWidth As String = .Text

                If strChk = "1" Then
                    sngTotal += Convert.ToSingle(strWidth)

                    msSelLists += strTitle + "^" + strField + "^" + strWidth + "^" + "|"
                End If
            Next
        End With

        If sngTotal > 1154 - 440 Then
            MsgBox("출력범위가 넘어 갔습니다.  선택항목을 줄여 주세요.", MsgBoxStyle.Information)
        Else
            mbSave = True
            Me.Close()
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        msSelLists = ""
        Me.Close()
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        If e.col = spdList.GetColFromID("chk") Then Return

        With spdList
            .Row = e.row
            .Col = .GetColFromID("chk") : .Text = IIf(.Text = "1", "", "1").ToString
        End With

    End Sub

    Private Sub FGB14_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If

    End Sub
End Class