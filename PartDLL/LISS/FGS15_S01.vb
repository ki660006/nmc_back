Imports System.Windows.Forms

Public Class FGS15_S01
    Private Const msFile As String = "File : B01.vb, Class : FGB12_S01" & vbTab

    Private mbSave As Boolean = False
    Private msSelLists As String = ""

    Private Sub sbDisplay_init()

        With spdList
            .MaxRows = 8

            .Row = 1
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "검체번호"
            .Col = .GetColFromID("width") : .Text = "140"
            .Col = .GetColFromID("field") : .Text = "bcno"

            .Row = 2
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "작업번호"
            .Col = .GetColFromID("width") : .Text = "140"
            .Col = .GetColFromID("field") : .Text = "workno"

            .Row = 3
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "등록번호"
            .Col = .GetColFromID("width") : .Text = "80"
            .Col = .GetColFromID("field") : .Text = "regno"

            .Row = 4
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "성별/나이"
            .Col = .GetColFromID("width") : .Text = "60"
            .Col = .GetColFromID("field") : .Text = "sexage"

            .Row = 5
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "의뢰의사"
            .Col = .GetColFromID("width") : .Text = "60"
            .Col = .GetColFromID("field") : .Text = "doctornm"


            .Row = 6
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "진료과/병동"
            .Col = .GetColFromID("width") : .Text = "120"
            .Col = .GetColFromID("field") : .Text = "dept"

            .Row = 7
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "검체명"
            .Col = .GetColFromID("width") : .Text = "150"
            .Col = .GetColFromID("field") : .Text = "spcnmd"

            .Row = 8
            .Col = .GetColFromID("chk") : .Text = "1"
            .Col = .GetColFromID("title") : .Text = "소견"
            .Col = .GetColFromID("width") : .Text = "100"
            .Col = .GetColFromID("field") : .Text = "doctorrmk"

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

        If sngTotal > 900 Then
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

    Private Sub FGB12_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If

    End Sub
End Class