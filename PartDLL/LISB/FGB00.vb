Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class FGB00

    Private mbSave As Boolean = False
    Private msSelLists As String = ""

    Private Sub sbDisplay_init(ByVal ra_Item As ArrayList)

        With spdList
            .MaxRows = ra_Item.Count

            For ix As Integer = 0 To ra_Item.Count - 1
                .Row = ix + 1
                .Col = .GetColFromID("chk") : .Text = CType(ra_Item(ix), STU_PrtItemInfo).CHECK
                .Col = .GetColFromID("title") : .Text = CType(ra_Item(ix), STU_PrtItemInfo).TITLE
                .Col = .GetColFromID("width") : .Text = CType(ra_Item(ix), STU_PrtItemInfo).WIDTH
                .Col = .GetColFromID("field") : .Text = CType(ra_Item(ix), STU_PrtItemInfo).FIELD
            Next
        End With
    End Sub

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal ra_Item As ArrayList) As String
        Dim sFn As String = "Function Display_Result"


        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            sbDisplay_init(ra_Item)

            Me.Cursor = Windows.Forms.Cursors.Default

            Me.ShowDialog(r_frm)

            If mbSave Then
                Return msSelLists
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            Return ""
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try

    End Function

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        Dim sgTotal As Single = 0

        With spdList
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Row = intRow
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                .Col = .GetColFromID("title") : Dim sTitle As String = .Text
                .Col = .GetColFromID("field") : Dim sfield As String = .Text
                .Col = .GetColFromID("width") : Dim sWidth As String = .Text

                If sChk = "1" Then
                    sgTotal += Convert.ToSingle(sWidth)

                    msSelLists += sTitle + "^" + sField + "^" + sWidth + "^" + "|"
                End If
            Next
        End With

        'If sngTotal > 1100 Then
        '    MsgBox("출력범위가 넘어 갔습니다.  선택항목을 줄여 주세요.", MsgBoxStyle.Information)
        'Else
        mbSave = True
        Me.Close()
        'End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        msSelLists = ""
        Me.Close()
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        If e.col = spdList.GetColFromID("chk") Then Return

        With Me.spdList
            .Row = e.row
            .Col = .GetColFromID("chk") : .Text = IIf(.Text = "1", "", "1").ToString
        End With

    End Sub

    Private Sub FGS00_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

Public Class FGB00_PATINFO
    Public alItem As New ArrayList
    Public CmtCont As String = ""
End Class

Public Class FGB00_PRINT
    Private Const msFile As String = "File : FGB00.vb, Class : LISB.FGB00_PRINT" + vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public mbLandscape As Boolean = False
    Public msgExmWidth As Single = 0
    Public msTitle As String = ""
    Public msTitle_sub_center As String = ""
    Public msTitle_sub_left_1 As String = ""
    Public msTitle_sub_left_2 As String = ""
    Public msTitle_sub_right_1 As String = ""
    Public msTitle_sub_right_2 As String = ""

    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

    Public maPrtData As ArrayList
    Public msJobGbn As String = ""
    Public miTitleCnt As Integer = 0

    Public Sub sbPrint_Preview()
        Dim sFn As String = "Sub sbPrint_Preview()"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = mbLandscape
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + sFn, ex))

        End Try
    End Sub

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = mbLandscape

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog


            If prtBPress = DialogResult.OK Then


                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport
                prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miCIdx = 0
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sgPosY As Single = 0
        Dim sgPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        Dim sngTmp As Single = 0
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGB00_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGB00_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        'If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sgPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.2)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sgPosY = 0 Then
                sgPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sgPosY, msgPosX(1) - msgPosX(0), sgPrtH)
            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            For ix As Integer = 1 To miTitleCnt - 1
                rect = New Drawing.RectangleF(msgPosX(ix), sgPosY, msgPosX(ix + 1) - msgPosX(ix), sgPrtH)
                Dim strTmp As String = CType(maPrtData.Item(intIdx), FGB00_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            If CType(maPrtData.Item(intIdx), FGB00_PATINFO).CmtCont <> "" Then
                Dim sBuf() As String = CType(maPrtData.Item(intIdx), FGB00_PATINFO).CmtCont.Split("^"c)(0).Replace(vbCrLf, "|").Split("|"c)

                For ix As Integer = 0 To sBuf.Length - 1
                    rect = New Drawing.RectangleF(msgPosX(msgPosX.Length - 2), sgPosY, msgPosX(msgPosX.Length - 1) - msgPosX(msgPosX.Length - 2), sgPrtH)
                    e.Graphics.DrawString(sBuf(ix), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                    sgPosY += sgPrtH
                    If msgHeight < sgPosY + sgPrtH * 3 Then miCIdx += 1 : Exit For
                Next
            End If

            miCIdx += 1

            sgPosY += sgPrtH
            If msgHeight < sgPosY + sgPrtH * 3 Then miCIdx += 1 : Exit For

            If (intIdx + 1) Mod 5 = 0 Then e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY, msgWidth, sgPosY)

        Next


        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sgPrtH * 2 - sgPrtH / 2, msgWidth, msgHeight - sgPrtH * 2 - sgPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth - msgLeft - 25, sgPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth - msgLeft - 25, sgPrtH), sf_c)

        If miCIdx < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Title_sub As New Font("굴림체", 12, FontStyle.Bold)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sgPrt As Single = 0
        Dim sgPosY As Single = 0
        Dim iCnt As Integer = 1

        miTitleCnt = CType(maPrtData.Item(0), FGB00_PATINFO).alItem.Count + 1

        Dim sgPosX(0 To 1) As Single

        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(0) + 40
        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sgPosX(ix + 1)

            sgPosX(ix + 1) = sgPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGB00_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))

        Next

        If CType(maPrtData.Item(0), FGB00_PATINFO).CmtCont <> "" Then
            ReDim Preserve sgPosX(miTitleCnt + 1)

            sgPosX(miTitleCnt + 1) = sgPosX(miTitleCnt) + Convert.ToSingle(CType(maPrtData.Item(0), FGB00_PATINFO).CmtCont.Split("^"c)(2))

        End If

        msgPosX = sgPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sgPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sgPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sgPosY = msgTop + sgPrt * 2
        sgPrt = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 1.5)

        If msTitle_sub_center <> "" Then
            e.Graphics.DrawString(msTitle_sub_center, fnt_Title_sub, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sgPosY, msgWidth - sgPosX(0), sgPrt), sf_c)
            sgPosY += sgPrt
        End If

        If msTitle_sub_left_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_left_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sgPosY, msgWidth - sgPosX(0), sgPrt), sf_l)
        End If

        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length, sgPosY, msgWidth - 8 * msTitle_sub_right_1.Length, sgPrt), sf_l)
        End If

        If (msTitle_sub_left_1 + msTitle_sub_right_1).Length > 0 Then
            sgPosY += sgPrt
        End If

        If msTitle_sub_left_2 <> "" Then
            e.Graphics.DrawString(msTitle_sub_left_2, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sgPosY, msgWidth - sgPosX(0), sgPrt), sf_l)
        End If

        'If msTitle_sub_right_2 <> "" Then
        '    e.Graphics.DrawString(msTitle_sub_right_2, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sgPosY, msgWidth - sgPosX(0), sgPrt), sf_r)
        'End If

        'If (msTitle_sub_left_2 + msTitle_sub_right_2).Length > 0 Then
        '    sgPosY += sgPrt
        'End If

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sgPosY - 20, msgWidth - 8 * (msTitle_Time.Length + 6), sgPrt), sf_l)

        sgPosY += sgPrt '+ sgPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sgPosY, sgPosX(1) - sgPosX(0), sgPrt), sf_l)

        For ix As Integer = 1 To miTitleCnt - 1  ' sgPosX.Length - 2

            Dim strTmp As String = CType(maPrtData.Item(0), FGB00_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(ix), sgPosY + sgPrt * 0, sgPosX(ix + 1) - sgPosX(ix), sgPrt), sf_l)
        Next

        If CType(maPrtData.Item(0), FGB00_PATINFO).CmtCont <> "" Then
            Dim strTmp As String = CType(maPrtData.Item(0), FGB00_PATINFO).CmtCont.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(sgPosX.Length - 2), sgPosY + sgPrt * 0, sgPosX(sgPosX.Length - 1) - sgPosX(sgPosX.Length - 2), sgPrt), sf_l)
        End If

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY, msgWidth, sgPosY)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY + sgPrt, msgWidth, sgPosY + sgPrt)

        msgPosX = sgPosX
        Return sgPosY + sgPrt

    End Function

End Class