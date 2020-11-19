Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class FGS00

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

        mbSave = True
        Me.Close()

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

    Private Sub FGS00_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Escape Then
            btnExit_Click(Nothing, Nothing)
        End If

    End Sub
End Class

Public Class FGS00_PATINFO
    Public alItem As New ArrayList
    Public CmtCont As String = ""
End Class

Public Class FGS00_PRINT
    Private Const msFile As String = "File : FGS00.vb, Class : S01" & vbTab

    Private miPageNo As Integer = 0
    Private miCol_Cur As Integer = 0
    Private miRow_Cur As Integer = 0
    Private miMaxCol As Integer = 0

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
    Public msFrm As String = ""
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

    Public maPrtData As ArrayList
    Public msJobGbn As String = ""
    Public miTitleCnt As Integer = 0
    Public m_o_BodyFont As Font = New Font("굴림체", 10, FontStyle.Regular)

    Public Sub sbPrint_Preview(Optional ByVal rsFrm As String = "") '<<<20170821 화면 받아오게 수정 
        Dim sFn As String = "Sub sbPrint_Preview()"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult
            msFrm = rsFrm '<<<20170821 화면 받아오게 수정 
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
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Sub

    Public Sub sbPrint(Optional ByVal rsFrm As String = "") '<<<20170821 화면 받아오게 수정 
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult
            msFrm = rsFrm '<<<20170821 화면 받아오게 수정 
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
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miCol_Cur = 0
        miRow_Cur = 0
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim iLine As Integer = 0
        Dim iPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 22, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        If m_o_BodyFont IsNot Nothing Then fnt_Body = m_o_BodyFont

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 13
        msgLeft = 5
        msgTop = 40

        Dim sgTmp As Single = 0
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGS00_PATINFO).alItem.Count - 1
            sgTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGS00_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        'If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.25) '한행의 높이

        ' msgPosX(index) = 컬럼(index)별 가로(x좌표) 위치
        ' sngPosY = ROW행단위 Y좌표 위치
        ' sngPrtH = 한 ROW 행의 높이 

        Dim rect As New Drawing.RectangleF '출력할 행의 사각형 박스 

        For iRow As Integer = miRow_Cur To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e) '타이틀 
            End If
            '1) 검체단위 행 번호를 출력
            If miCol_Cur = 0 Then
                '-- 번호를 출력
                rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
                e.Graphics.DrawString((iRow + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            End If

            Dim iPos As Integer = 0
            '2) 검체의 세부내용과 결과값까지 컬럼단위로 for문을 돌려 출력
            For iCol As Integer = CType(IIf(miCol_Cur = 0, 1, 0), Integer) To msgPosX.Length - 2 - CType(IIf(CType(maPrtData.Item(0), FGS00_PATINFO).CmtCont = "", 0, 1), Integer)
                rect = New Drawing.RectangleF(msgPosX(iCol), sngPosY, msgPosX(iCol + 1) - msgPosX(iCol), sngPrtH)
                Dim sTmp As String = CType(maPrtData.Item(iRow), FGS00_PATINFO).alItem(miCol_Cur + iPos).ToString.Split("^"c)(0)

                e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                iPos += 1
            Next

            If miCol_Cur = 0 Then
                If CType(maPrtData.Item(iRow), FGS00_PATINFO).CmtCont <> "" Then '소견이 있을때
                    Dim sBuf() As String = CType(maPrtData.Item(iRow), FGS00_PATINFO).CmtCont.Split("^"c)(0).Replace(vbCrLf, "|").Split("|"c)

                    For ix As Integer = 0 To sBuf.Length - 1 '소견수 만큼 
                        rect = New Drawing.RectangleF(msgPosX(msgPosX.Length - 2), sngPosY, msgPosX(msgPosX.Length - 1) - msgPosX(msgPosX.Length - 2), sngPrtH)
                        e.Graphics.DrawString(sBuf(ix), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                        sngPosY += sngPrtH
                        If msgHeight < sngPosY + sngPrtH * 3 Then miRow_Cur += 1 : Exit For
                    Next
                End If
            End If

            sngPosY += sngPrtH
            'If msgHeight - 70 < sngPosY + sngPrtH * 3 Then miRow_Cur += 1 : Exit For '<<<20170427 출력물 짤림 수정 
            'If msgHeight - 70 < sngPosY + sngPrtH Then miRow_Cur += 1 : Exit For '<<<20170427 출력물 짤림 수정 

            If msFrm <> "" Then
                If msFrm = "FGS10" Then
                    If msgHeight - 70 < sngPosY + sngPrtH * 3 Then miRow_Cur += 1 : Exit For '<<<20170427 출력물 짤림 수정 
                ElseIf msFrm = "FGS02" Then
                    If msgHeight - 70 < sngPosY + sngPrtH * 3 Then miRow_Cur += 1 : Exit For '<<<20170427 출력물 짤림 수정 
                End If
            Else
                If msgHeight < sngPosY + sngPrtH * 3 Then miRow_Cur += 1 : Exit For '<<<20170427 출력물 짤림 수정 
            End If

            If (iRow + 1) Mod 5 = 0 Then e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)
            iLine += 1
        Next

        miCol_Cur += miMaxCol
        If miCol_Cur >= CType(maPrtData.Item(0), FGS00_PATINFO).alItem.Count Then miRow_Cur += iLine : miPageNo += 1 : miCol_Cur = 0
        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miRow_Cur < maPrtData.Count - 1 Then
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
        Dim iCol As Integer = 0

        miTitleCnt = CType(maPrtData.Item(0), FGS00_PATINFO).alItem.Count + 1

        Dim sgPosX(0 To 1) As Single

        miMaxCol = 0
        sgPosX(0) = msgLeft

        If miCol_Cur = 0 Then sgPosX(1) = sgPosX(0) + 40 : iCol = 1

        For ix As Integer = miCol_Cur To CType(maPrtData.Item(0), FGS00_PATINFO).alItem.Count - 1

            If sgPosX(iCol) + Convert.ToSingle(CType(maPrtData.Item(0), FGS00_PATINFO).alItem(ix).ToString.Split("^"c)(2)) > msgWidth Then Exit For
            
            ReDim Preserve sgPosX(iCol + 1)

            sgPosX(iCol + 1) = sgPosX(iCol) + Convert.ToSingle(CType(maPrtData.Item(0), FGS00_PATINFO).alItem(ix).ToString.Split("^"c)(2))

            miMaxCol += 1
            iCol += 1
        Next

        If miCol_Cur = 0 Then
            If CType(maPrtData.Item(0), FGS00_PATINFO).CmtCont <> "" Then
                ReDim Preserve sgPosX(miTitleCnt + 1)

                sgPosX(miTitleCnt + 1) = sgPosX(miTitleCnt) + Convert.ToSingle(CType(maPrtData.Item(0), FGS00_PATINFO).CmtCont.Split("^"c)(2))

                miMaxCol += 1
            End If
        End If

        msgPosX = sgPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sgPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sgPrt) ' 5.0 , 40.0 , 1154.0 , 25.52..

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        'sgPosY = msgTop + sgPrt * 2
        sgPosY = msgTop + sgPrt
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
            sgPosY += sgPrt / 2
        End If

        If msTitle_sub_left_2 <> "" Then
            e.Graphics.DrawString(msTitle_sub_left_2, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sgPosY, msgWidth - sgPosX(0), sgPrt), sf_l)
        End If

        'If msTitle_sub_right_2 <> "" Then
        '    e.Graphics.DrawString(msTitle_sub_right_2, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_r)
        'End If

        'If (msTitle_sub_left_2 + msTitle_sub_right_2).Length > 0 Then
        '    sngPosY += sngPrt
        'End If

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sgPosY, msgWidth - 8 * (msTitle_Time.Length + 6), sgPrt), sf_l)

        sgPosY += sgPrt '+ sngPrt / 2

        If miCol_Cur = 0 Then e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(0), sgPosY, sgPosX(1) - sgPosX(0), sgPrt), sf_l)

        iCol = 0
        For ix As Integer = CType(IIf(miCol_Cur = 0, 1, 0), Integer) To sgPosX.Length - 2 - CType(IIf(CType(maPrtData.Item(0), FGS00_PATINFO).CmtCont = "", 0, 1), Integer)

            Dim sTmp As String = CType(maPrtData.Item(0), FGS00_PATINFO).alItem(miCol_Cur + iCol).ToString.Split("^"c)(1)

            e.Graphics.DrawString(sTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(ix), sgPosY + sgPrt * 0, sgPosX(ix + 1) - sgPosX(ix), sgPrt), sf_l)

            iCol += 1
        Next

        If CType(maPrtData.Item(0), FGS00_PATINFO).CmtCont <> "" Then
            Dim sTmp As String = CType(maPrtData.Item(0), FGS00_PATINFO).CmtCont.Split("^"c)(1)

            e.Graphics.DrawString(sTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sgPosX(sgPosX.Length - 2), sgPosY + sgPrt * 0, sgPosX(sgPosX.Length - 1) - sgPosX(sgPosX.Length - 2), sgPrt), sf_l)
        End If

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY, msgWidth, sgPosY)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY + sgPrt, msgWidth, sgPosY + sgPrt)

        msgPosX = sgPosX
        Return sgPosY + sgPrt

    End Function



End Class