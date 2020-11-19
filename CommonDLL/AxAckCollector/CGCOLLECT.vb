Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Public Class CGPRT_DOCUMENT
    Private Const msFile As String = "File : CGPRTPATINF.vb, Class : CGPRTPATINF" & vbTab

    Public cpi As STU_PatInfo

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 10

    Public Sub sbPrint_Preview(ByVal rsPrtGbn As String)
        Dim sFn As String = "Sub sbPrint_Preview()"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                Select Case rsPrtGbn
                    Case "1"
                        prtR.DocumentName = "ACK_수혈의뢰서"
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_tns
                    Case "2"
                        prtR.DocumentName = "ACK_유전자동의서"
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_tns
                    Case "3"
                        prtR.DocumentName = "ACK_수혈의뢰서"
                        AddHandler prtR.PrintPage, AddressOf sbPrintPage_tns

                End Select
            End If
            AddHandler prtR.BeginPrint, AddressOf sbPrintData
            AddHandler prtR.EndPrint, AddressOf sbReport

            prtRView.Document = prtR
            prtRView.ShowDialog()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Public Sub sbPrint(ByVal rsPrtGbn As String)
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            'prtBPress = prtDialog.ShowDialog

            Select Case rsPrtGbn
                Case "1"
                    prtR.DocumentName = "수혈의뢰서"
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage_tns
                Case "2"
                    prtR.DocumentName = "유전자동의서"
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage_geno
                Case "3"
                    prtR.DocumentName = "PB 의뢰서"
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage_pb
            End Select
            AddHandler prtR.BeginPrint, AddressOf sbPrintData
            AddHandler prtR.EndPrint, AddressOf sbReport
            prtR.Print()

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Public Overridable Sub sbPrintPage_tns(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
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

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 10
        msgLeft = 5
        msgTop = 30

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToInt16(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF
        Dim sTmp As String = ""

        sngPosY += msgTop
        rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * 0, msgWidth, sngPrtH)
        e.Graphics.DrawString("수혈의뢰서", New Font("굴림체", 15, FontStyle.Bold), Drawing.Brushes.Black, rect, sf_c)

        '-- 등록번호
        sngPosY += sngPrtH * 2
        sTmp = "등록번호: " + cpi.REGNO
        rect = New Drawing.RectangleF(msgLeft, sngPosY, 100, sngPrtH)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 성명
        sngPosY += sngPrtH * 2
        sTmp = "환 자 명: " + cpi.PATNM
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 2)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 진료과/병동
        sngPosY += sngPrtH * 2
        sTmp = "진료과/병동: " + cpi.DEPTCD + "/" + cpi.WARD
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 3)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 성별/나이
        sngPosY += sngPrtH * 2
        sTmp = "성별/나이: " + cpi.SEX + "/" + cpi.AGE
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 4)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)


        '-- 라인
        'e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)


        e.HasMorePages = False

    End Sub

    Public Overridable Sub sbPrintPage_geno(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
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

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 10
        msgLeft = 5
        msgTop = 30

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToInt16(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF
        Dim sTmp As String = ""

        sngPosY += msgTop
        rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * 0, msgWidth, sngPrtH)
        e.Graphics.DrawString("유전자동의서", New Font("굴림체", 15, FontStyle.Bold), Drawing.Brushes.Black, rect, sf_c)

        '-- 등록번호
        sngPosY += sngPrtH * 2
        sTmp = "등록번호: " + cpi.REGNO
        rect = New Drawing.RectangleF(msgLeft, sngPosY, 100, sngPrtH)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 성명
        sngPosY += sngPrtH * 2
        sTmp = "환 자 명: " + cpi.PATNM
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 2)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 진료과/병동
        sngPosY += sngPrtH * 2
        sTmp = "진료과/병동: " + cpi.DEPTCD + "/" + cpi.WARD
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 3)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 성별/나이
        sngPosY += sngPrtH * 2
        sTmp = "성별/나이: " + cpi.SEX + "/" + cpi.AGE
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 4)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)


        '-- 라인
        'e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)


        e.HasMorePages = False

    End Sub

    Public Overridable Sub sbPrintPage_pb(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
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

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 10
        msgLeft = 5
        msgTop = 30

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToInt16(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF
        Dim sTmp As String = ""

        sngPosY += msgTop
        rect = New Drawing.RectangleF(msgLeft, sngPosY + sngPrtH * 0, msgWidth, sngPrtH)
        e.Graphics.DrawString("PB 의뢰서", New Font("굴림체", 15, FontStyle.Bold), Drawing.Brushes.Black, rect, sf_c)

        '-- 등록번호
        sngPosY += sngPrtH * 2
        sTmp = "등록번호: " + cpi.REGNO
        rect = New Drawing.RectangleF(msgLeft, sngPosY, 100, sngPrtH)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 성명
        sngPosY += sngPrtH * 2
        sTmp = "환 자 명: " + cpi.PATNM
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 2)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 진료과/병동
        sngPosY += sngPrtH * 2
        sTmp = "진료과/병동: " + cpi.DEPTCD + "/" + cpi.WARD
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 3)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

        '-- 성별/나이
        sngPosY += sngPrtH * 2
        sTmp = "성별/나이: " + cpi.SEX + "/" + cpi.AGE
        rect = New Drawing.RectangleF(msgLeft, sngPosY, msgWidth, sngPrtH * 4)
        e.Graphics.DrawString(sTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)


        '-- 라인
        'e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)


        e.HasMorePages = False

    End Sub
End Class

Public Class CGPRT_COLLLIST
    Private Const msFile As String = "File : FGS00.vb, Class : S01" & vbTab

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
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

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
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
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

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        Dim sngTmp As Single = 0
        For ix As Integer = 0 To CType(maPrtData.Item(0), CGPRT_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), CGPRT_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        'If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.2)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            For ix As Integer = 1 To miTitleCnt - 1
                rect = New Drawing.RectangleF(msgPosX(ix), sngPosY, msgPosX(ix + 1) - msgPosX(ix), sngPrtH)
                Dim strTmp As String = CType(maPrtData.Item(intIdx), CGPRT_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            Next

            If CType(maPrtData.Item(intIdx), CGPRT_PATINFO).CmtCont <> "" Then
                Dim sBuf() As String = CType(maPrtData.Item(intIdx), CGPRT_PATINFO).CmtCont.Split("^"c)(0).Replace(vbCrLf, "|").Split("|"c)

                For ix As Integer = 0 To sBuf.Length - 1
                    rect = New Drawing.RectangleF(msgPosX(msgPosX.Length - 2), sngPosY, msgPosX(msgPosX.Length - 1) - msgPosX(msgPosX.Length - 2), sngPrtH)
                    e.Graphics.DrawString(sBuf(ix), fnt_Body, Drawing.Brushes.Black, rect, sf_l)

                    sngPosY += sngPrtH
                    If msgHeight < sngPosY + sngPrtH * 3 Then miCIdx += 1 : Exit For
                Next
            End If

            'sngPosY += sngPrtH
            If msgHeight < sngPosY + sngPrtH * 2 Then miCIdx += 1 : Exit For

            If (intIdx + 1) Mod 5 = 0 Then e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)

            miCIdx += 1
        Next


        miPageNo += 1

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2 - sngPrtH / 2, msgWidth, msgHeight - sngPrtH * 2 - sngPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

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
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1
        Dim sngTmp As Single = 0

        miTitleCnt = CType(maPrtData.Item(0), CGPRT_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40
        For ix As Integer = 1 To miTitleCnt - 1

            If ix > 1 Then
                If sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), CGPRT_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2)) > msgWidth Then Exit For
            End If

            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), CGPRT_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))

        Next

        If CType(maPrtData.Item(0), CGPRT_PATINFO).CmtCont <> "" Then
            ReDim Preserve sngPosX(miTitleCnt + 1)

            sngPosX(miTitleCnt + 1) = sngPosX(miTitleCnt) + Convert.ToSingle(CType(maPrtData.Item(0), CGPRT_PATINFO).CmtCont.Split("^"c)(2))
        End If

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = fnt_Title.GetHeight(e.Graphics)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 1.5)

        If msTitle_sub_center <> "" Then
            e.Graphics.DrawString(msTitle_sub_center, fnt_Title_sub, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_c)
            sngPosY += sngPrt
        End If

        If msTitle_sub_left_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_left_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_r)
        End If

        If (msTitle_sub_left_1 + msTitle_sub_right_1).Length > 0 Then
            sngPosY += sngPrt
        End If

        If msTitle_sub_left_2 <> "" Then
            e.Graphics.DrawString(msTitle_sub_left_2, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)
        End If

        'If msTitle_sub_right_2 <> "" Then
        '    e.Graphics.DrawString(msTitle_sub_right_2, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_r)
        'End If

        'If (msTitle_sub_left_2 + msTitle_sub_right_2).Length > 0 Then
        '    sngPosY += sngPrt
        'End If

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt '+ sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt), sf_l)

        For ix As Integer = 1 To miTitleCnt - 1  ' sngPosX.Length - 2

            Dim strTmp As String = CType(maPrtData.Item(0), CGPRT_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(ix), sngPosY + sngPrt * 0, sngPosX(ix + 1) - sngPosX(ix), sngPrt), sf_l)
        Next

        If CType(maPrtData.Item(0), CGPRT_PATINFO).CmtCont <> "" Then
            Dim strTmp As String = CType(maPrtData.Item(0), CGPRT_PATINFO).CmtCont.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(sngPosX.Length - 2), sngPosY + sngPrt * 0, sngPosX(sngPosX.Length - 1) - sngPosX(sngPosX.Length - 2), sngPrt), sf_l)
        End If

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt, msgWidth, sngPosY + sngPrt)

        msgPosX = sngPosX
        Return sngPosY + sngPrt

    End Function

End Class

Public Class CGPRT_PATINFO
    Public alItem As New ArrayList
    Public CmtCont As String = ""
End Class


Public Enum enumCollectCallForm
    CollectAll = 0
    CollectIn = 1
    CollectOut = 2
    CollectCust = 3
End Enum

Public Enum enumCollectMode
    CollectAll = 0
    CollectNo = 1
    CollectYes = 2
End Enum

Public Enum enumDeptMode
    DeptCommon = 0
    DeptSpec01 = 1
    DeptSpec02 = 2
End Enum

Public Enum enumInOutMode
    InPatient = 0
    OutPatient = 1
    CustPatient = 2
End Enum

Public Enum enumOrderMode
    OrderAll = 0
    OrderRegular = 1
    OrderAdd = 2
End Enum

Public Enum enumSearchMode
    Coll = 0
    Search = 1
End Enum

Public Enum enumWorkMode
    Collect = 0
    Search = 1
End Enum

Public Class OrdList_SearchOption
    Public WardCd As String = ""
    Public RegNo As String = ""
    Public DayB As String = ""
    Public DayE As String = ""
    Public CollMode As enumCollectMode
    Public IOMode As enumInOutMode
    Public OrdMode As enumOrderMode
    Public DeptMode As enumDeptMode
    Public WorkMode As enumWorkMode
    '< add yjlee 2009-05-20  부천순천향은 Team 
    Public RoomCd As String = ""
End Class