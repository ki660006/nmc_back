Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login

Imports LISAPP.APP_DB

Public Class FGO04
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGO04.vb, Class : O01" & vbTab
    Private moDB As New LISAPP.LISAPP_O_CUST_ORD

    ' 폼 초기설정
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"
        Dim CommFN As New Fn
        Dim ServerDT As New ServerDateTime

        Try
            Me.Tag = "Load"
            ' 서버날짜로 설정
            dtpDateS.Value = CDate(ServerDT.GetDate("-").Substring(0, 8) + "01")
            dtpDateE.Value = DateAdd(DateInterval.Day, -1, DateAdd(DateInterval.Month, 1, dtpDateS.Value))

            spdList.MaxRows = 0
            spdPatList.MaxRows = 0

            sbDisplay_Cust()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub sbDisplay_Cust()
        Dim sFn As String = "Private Sub sbDisplay_Cust()"

        Try
            Dim dt As DataTable = moDB.fnGet_CustList()

            If dt.Rows.Count < 1 Then Return

            cboCustCd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboCustCd.Items.Add(dt.Rows(ix).Item("cust").ToString)
            Next

            If cboCustCd.Items.Count > 0 Then cboCustCd.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub sbDisplay_Data()
        Dim sFn As String = "Private Sub sbDisplay_Cust()"

        Try
            spdList.MaxRows = 0 : spdPatList.MaxRows = 0

            Dim dt As DataTable = moDB.fnGet_Cust_List(Ctrl.Get_Code(cboCustCd), dtpDateS.Text.Replace("-", ""), dtpDateE.Text.Replace("-", ""))

            If dt.Rows.Count < 1 Then Return

            Dim lngTCnt As Long = 0, lngCost As Long = 0

            With spdList
                .MaxRows = dt.Rows.Count + 1

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("sugacd") : .Text = dt.Rows(ix).Item("sugacd").ToString
                    .Col = .GetColFromID("tnmp") : .Text = dt.Rows(ix).Item("tnmp").ToString
                    .Col = .GetColFromID("tcnt") : .Text = dt.Rows(ix).Item("tcnt").ToString
                    .Col = .GetColFromID("danga") : .Text = Format(dt.Rows(ix).Item("danga"), "#,##0").ToString
                    .Col = .GetColFromID("cost_t") : .Text = Format(dt.Rows(ix).Item("cost_t"), "#,##0").ToString

                    lngTCnt += Convert.ToInt32(dt.Rows(ix).Item("tcnt").ToString)
                    lngCost += Convert.ToInt32(dt.Rows(ix).Item("cost_t").ToString)
                Next

                .Row = .MaxRows
                .Col = .GetColFromID("tnmp") : .Text = "합계 " : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .Col = .GetColFromID("tcnt") : .Text = Format(lngTCnt, "#,##0")
                .Col = .GetColFromID("cost_t") : .Text = Format(lngCost, "#,##0")

                .Col = -1 : .Col2 = -1
                .Row = .MaxRows - 1 : .Row2 = .MaxRows - 1
                .BlockMode = True
                .CellBorderType = 8
                .CellBorderStyle = FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid
                .Action = FPSpreadADO.ActionConstants.ActionSetCellBorder
                .BlockMode = False

                .Col = -1 : .Col2 = -1
                .Row = .MaxRows : .Row2 = .MaxRows
                .BlockMode = True
                .CellBorderType = 8
                .CellBorderStyle = FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid
                .Action = FPSpreadADO.ActionConstants.ActionSetCellBorder
                .BlockMode = False

            End With

            dt = moDB.fnGet_Cust_PatList(Ctrl.Get_Code(cboCustCd), dtpDateS.Text.Replace("-", ""), dtpDateE.Text.Replace("-", ""))
            If dt.Rows.Count < 1 Then Return

            Dim strDate As String = "", strRegNo As String = ""

            lngCost = 0 : lngTCnt = 0

            With spdPatList
                .MaxRows = dt.Rows.Count + 1
                lngTCnt = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1

                    If strDate <> dt.Rows(ix).Item("orddt").ToString Then
                        .Row = ix + 1
                        .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString

                        If ix > 0 Then
                            .Col = -1 : .Col2 = -1
                            .Row = ix : .Row2 = ix
                            .BlockMode = True
                            .CellBorderType = 8
                            .CellBorderStyle = FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid
                            .Action = FPSpreadADO.ActionConstants.ActionSetCellBorder
                            .BlockMode = False
                        End If
                        strRegNo = ""
                    End If

                    If strRegNo <> dt.Rows(ix).Item("regno").ToString Then
                        .Row = ix + 1
                        .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString

                        If ix > 0 Then
                            .Col = .GetColFromID("patnm") : .Col2 = .MaxCols
                            .Row = ix : .Row2 = ix
                            .BlockMode = True
                            .CellBorderType = 8
                            .CellBorderStyle = FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid
                            .Action = FPSpreadADO.ActionConstants.ActionSetCellBorder
                            .BlockMode = False
                        End If
                    End If


                    .Row = ix + 1
                    strDate = dt.Rows(ix).Item("orddt").ToString
                    strRegNo = dt.Rows(ix).Item("regno").ToString

                    .Col = .GetColFromID("sugacd") : .Text = dt.Rows(ix).Item("sugacd").ToString
                    .Col = .GetColFromID("tnmp") : .Text = dt.Rows(ix).Item("tnmp").ToString
                    .Col = .GetColFromID("danga") : .Text = Format(dt.Rows(ix).Item("danga"), "#,##0")
                    .Col = .GetColFromID("etc") : .Text = dt.Rows(ix).Item("bcno").ToString

                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString

                    lngCost += Convert.ToInt32(dt.Rows(ix).Item("danga").ToString)
                Next

                .Col = -1 : .Col2 = -1
                .Row = .MaxRows - 1 : .Row2 = .MaxRows - 1
                .BlockMode = True
                .CellBorderType = 8
                .CellBorderStyle = FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid
                .Action = FPSpreadADO.ActionConstants.ActionSetCellBorder
                .BlockMode = False

                .Col = -1 : .Col2 = -1
                .Row = .MaxRows : .Row2 = .MaxRows
                .BlockMode = True
                .CellBorderType = 8
                .CellBorderStyle = FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid
                .Action = FPSpreadADO.ActionConstants.ActionSetCellBorder
                .BlockMode = False

                .Row = .MaxRows
                .Col = .GetColFromID("tnmp") : .Text = "합계 " : .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight
                .Col = .GetColFromID("danga") : .Text = Format(lngCost, "#,##0")
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)
        End Try

    End Sub

    Private Sub sbPrint_Data_1()

        Dim arlPrt As New ArrayList

        With spdList
            For ix As Integer = 1 To .MaxRows

                .Row = ix
                .Col = .GetColFromID("sugacd") : Dim sSugaCd As String = .Text
                .Col = .GetColFromID("tnmp") : Dim sExmNm As String = .Text
                .Col = .GetColFromID("tcnt") : Dim sExmCnt As String = .Text
                .Col = .GetColFromID("danga") : Dim sExmCost As String = .Text
                .Col = .GetColFromID("cost_t") : Dim sCost As String = .Text
                .Col = .GetColFromID("etc") : Dim sEtc As String = .Text

                Dim objPat As New FGO04_PRTINFO
                With objPat
                    .sSugaCd = sSugaCd
                    .sExmNm = sExmNm
                    .sExmCnt = sExmCnt
                    .sExmCost = sExmCost
                    .sCoust = sCost
                    .sEtc = sEtc

                End With

                arlPrt.Add(objPat)
            Next
        End With


        If arlPrt.Count > 0 Then
            Dim prt As New FGO04_PRINT
            prt.msTitle = "수탁검사 - 종목현황"
            prt.msTitle_Date = dtpDateS.Text + " ~ " + dtpDateE.Text + " " + cboCustCd.Text.Substring(cboCustCd.Text.IndexOf("]") + 1)
            prt.maPrtData = arlPrt

            'If chkPreview.Checked Then
            '    prt.sbPrint_Preview()
            'Else
            prt.sbPrint("1")
            'End If
        End If

    End Sub

    Private Sub sbPrint_Data_2()

        Dim arlPrt As New ArrayList

        With spdPatList

            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("orddt") : Dim sOrdDt As String = .Text
                .Col = .GetColFromID("patnm") : Dim sPatnm As String = .Text
                .Col = .GetColFromID("sugacd") : Dim sSugaCd As String = .Text
                .Col = .GetColFromID("tnmp") : Dim sExmNm As String = .Text
                .Col = .GetColFromID("danga") : Dim sExmCost As String = .Text
                .Col = .GetColFromID("etc") : Dim sEtc As String = .Text

                Dim objPat As New FGO04_PRTINFO
                With objPat
                    .sOrdDt = sOrdDt
                    .sPatNm = sPatnm
                    .sSugaCd = sSugaCd
                    .sExmNm = sExmNm
                    .sExmCost = sExmCost
                    .sEtc = sEtc

                    If sOrdDt <> "" Or sExmNm.Replace(" ", "") = "합계" Then .bLineYN_1 = True
                    If sPatnm <> "" Or sExmNm.Replace(" ", "") = "합계" Then .bLineYN_2 = True
                End With

                arlPrt.Add(objPat)
            Next
        End With

        If arlPrt.Count > 0 Then
            Dim prt As New FGO04_PRINT
            prt.msTitle = "수탁검사 - 거래명세서"
            prt.msTitle_Date = dtpDateS.Text + " ~ " + dtpDateE.Text + " " + cboCustCd.Text.Substring(cboCustCd.Text.IndexOf("]") + 1)
            prt.maPrtData = arlPrt

            'If chkPreview.Checked Then
            '    prt.sbPrint_Preview()
            'Else
            prt.sbPrint("2")
            'End If
        End If


    End Sub

    Private Sub FGO04_FontChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.FontChanged
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGO04_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGO04_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        Me.WindowState = FormWindowState.Maximized
        sbFormInitialize()

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        spdList.MaxRows = 0
        spdPatList.MaxRows = 0

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        sbDisplay_Data()
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        Select Case Me.tbcJob.SelectedTab.Text
            Case "종목현황"
                sbPrint_Data_1()
            Case Else
                sbPrint_Data_2()

        End Select

        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Dim sTime As String = Format(Now, "yyMMddHHmm")

        Select Case Me.tbcJob.SelectedTab.Text
            Case "종목현황"
                If spdList.ExportToExcel("c:\수탁_종목현황_" & sTime & ".xls", "종목현황", "") Then
                    Process.Start("c:\수탁_종목현황_" & sTime & ".xls")
                End If
            Case Else

                If spdPatList.ExportToExcel("c:\수탁_거래명세서_" & sTime & ".xls", "거래명세서", "") Then
                    Process.Start("c:\수탁_거래명세서_" & sTime & ".xls")
                End If
        End Select

    End Sub
End Class

Public Class FGO04_PRTINFO
    Public sOrdDt As String = ""
    Public sPatNm As String = ""
    Public sSugaCd As String = ""
    Public sExmNm As String = ""
    Public sExmCost As String = ""
    Public sEtc As String = ""
    Public sExmCnt As String = ""
    Public sCoust As String = ""

    Public bLineYN_1 As Boolean = False
    Public bLineYN_2 As Boolean = False

End Class


Public Class FGO04_PRINT
    Private Const msFile As String = "File : FGO04.vb, Class : O01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 10

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msgExmWidth As Single
    Public msTitle As String
    Public maPrtData As ArrayList
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

    Public Sub sbPrint_Preview()
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage_1
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

    Public Sub sbPrint(ByVal rsPrtGbn As String)
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle

                If rsPrtGbn = "1" Then
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage_1
                Else
                    AddHandler prtR.PrintPage, AddressOf sbPrintPage_2
                End If
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

    Public Overridable Sub sbPrintPage_1(ByVal sender As Object, ByVal e As PrintPageEventArgs)

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

        msgWidth = e.PageBounds.Width - 50
        msgHeight = e.PageBounds.Bottom - 30
        msgLeft = 5
        msgTop = 30

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToInt16(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF

        Dim intLine As Integer = 0

        If miCIdx = 0 Then miPageNo = 0

        For ix As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle_1(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * intLine, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((ix + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 종목코드
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * intLine, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sSugaCd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 종목명
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * intLine, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sExmNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 수량
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * intLine, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sExmCnt, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 단가
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * intLine, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sExmCost, fnt_Body, Drawing.Brushes.Black, rect, sf_r)
            '-- 금액
            rect = New Drawing.RectangleF(msgPosX(5), sngPosY + sngPrtH * intLine, msgPosX(6) - msgPosX(5), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sCoust, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 비고
            rect = New Drawing.RectangleF(msgPosX(6), sngPosY + sngPrtH * intLine, msgPosX(7) - msgPosX(6), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sEtc, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            intLine += 1
            miCIdx = ix + 1

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)

            If msgHeight - sngPrtH * 2 < sngPosY + sngPrtH * (intLine + 2) Then Exit For
        Next

        '-- 세로라인
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngPosY - sngPrtH / 2, msgPosX(ix), sngPosY + sngPrtH * intLine)
        Next

        miPageNo += 1

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList.Replace(" 진단검사의학과", ""), fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_l)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)
        e.Graphics.DrawString("출력일: " + msTitle_Time, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)

        If miCIdx < maPrtData.Count - 1 Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Sub sbPrintPage_2(ByVal sender As Object, ByVal e As PrintPageEventArgs)

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

        msgWidth = e.PageBounds.Width - 50
        msgHeight = e.PageBounds.Bottom - 30
        msgLeft = 5
        msgTop = 30

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToInt16(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF

        Dim intLine As Integer = 0

        If miCIdx = 0 Then miPageNo = 0
        Dim lngAmt_Tot As Long = 0

        For ix As Integer = miCIdx To maPrtData.Count - 2
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle_2(e)
                intLine = 0
            End If

            lngAmt_Tot += Convert.ToInt64(Val(CType(maPrtData.Item(ix), FGO04_PRTINFO).sExmCost.Replace(",", "")))

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * intLine, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString((ix + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 접수일
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * intLine, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sOrdDt, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
            '-- 환자명
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY + sngPrtH * intLine, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 수가코드
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY + sngPrtH * intLine, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sSugaCd, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 종목명
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * intLine, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sExmNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)
            '-- 금액
            rect = New Drawing.RectangleF(msgPosX(5), sngPosY + sngPrtH * intLine, msgPosX(6) - msgPosX(5), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sExmCost, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 비고
            rect = New Drawing.RectangleF(msgPosX(6), sngPosY + sngPrtH * intLine, msgPosX(7) - msgPosX(6), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(ix), FGO04_PRTINFO).sEtc, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            If CType(maPrtData.Item(ix), FGO04_PRTINFO).bLineYN_1 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(0), sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)
            ElseIf CType(maPrtData.Item(ix), FGO04_PRTINFO).bLineYN_2 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(2), sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)
            Else
                e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(3), sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)
            End If

            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(0), sngPosY + sngPrtH * intLine, msgPosX(1), sngPosY + sngPrtH * intLine)

            intLine += 1
            miCIdx = ix + 1

            If msgHeight - sngPrtH * 2 < sngPosY + sngPrtH * (intLine + 2) Then Exit For
        Next

        '-- Page Total
        rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * intLine, msgPosX(5) - msgPosX(4), sngPrtH)
        e.Graphics.DrawString("Page 합계", fnt_Body, Drawing.Brushes.Black, rect, sf_r)
        '-- 금액
        rect = New Drawing.RectangleF(msgPosX(5), sngPosY + sngPrtH * intLine, msgPosX(6) - msgPosX(5), sngPrtH)
        e.Graphics.DrawString(Format(lngAmt_Tot, "#,##0").ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(0), sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)

        intLine += 1

        If miCIdx + 1 = maPrtData.Count Then
            '-- 종목명
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * intLine, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(maPrtData.Count - 1), FGO04_PRTINFO).sExmNm, fnt_Body, Drawing.Brushes.Black, rect, sf_r)
            '-- 금액
            rect = New Drawing.RectangleF(msgPosX(5), sngPosY + sngPrtH * intLine, msgPosX(6) - msgPosX(5), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(maPrtData.Count - 1), FGO04_PRTINFO).sExmCost, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(0), sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)
            intLine += 1
        End If

        '-- 세로라인
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngPosY, msgPosX(ix), sngPosY + sngPrtH * intLine)
        Next

        ''-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)

        miPageNo += 1

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList.Replace(" 진단검사의학과", ""), fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_l)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)
        e.Graphics.DrawString("출력일: " + msTitle_Time, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)

        If miCIdx < maPrtData.Count - 1 Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle_1(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1

        Dim sngPosX(0 To 7) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40
        sngPosX(2) = sngPosX(1) + 80
        sngPosX(3) = sngPosX(2) + 200
        sngPosX(4) = sngPosX(3) + 60
        sngPosX(5) = sngPosX(4) + 80
        sngPosX(6) = sngPosX(5) + 100
        sngPosX(sngPosX.Length - 1) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = Convert.ToInt16(fnt_Title.GetHeight(e.Graphics) * 1.5)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_c)

        sngPosY += sngPrt * 2 + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt), sf_c)

        e.Graphics.DrawString("종목코드", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY, sngPosX(2) - sngPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("종  목  명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, sngPosX(3) - sngPosX(2), sngPrt), sf_c)
        e.Graphics.DrawString("수량", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY, sngPosX(4) - sngPosX(3), sngPrt), sf_c)
        e.Graphics.DrawString("단 가", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, sngPosX(5) - sngPosX(4), sngPrt), sf_c)
        e.Graphics.DrawString("금  액", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY, sngPosX(6) - sngPosX(5), sngPrt), sf_c)
        e.Graphics.DrawString("비  고", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(6), sngPosY, sngPosX(7) - sngPosX(6), sngPrt), sf_c)

        '-- 가로
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt, msgWidth, sngPosY + sngPrt)

        '-- 세로
        For ix As Integer = 0 To sngPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, sngPosX(ix), sngPosY - sngPrt / 2, sngPosX(ix), sngPosY + sngPrt)
        Next
        Return sngPosY + sngPrt '+ sngPrt / 2

    End Function

    Public Overridable Function fnPrtTitle_2(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1

        Dim sngPosX(0 To 7) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40
        sngPosX(2) = sngPosX(1) + 100
        sngPosX(3) = sngPosX(2) + 100
        sngPosX(4) = sngPosX(3) + 80
        sngPosX(5) = sngPosX(4) + 200
        sngPosX(6) = sngPosX(5) + 100
        sngPosX(sngPosX.Length - 1) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = Convert.ToInt16(fnt_Title.GetHeight(e.Graphics) * 1.5)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_c)

        sngPosY += sngPrt * 2 + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt), sf_c)

        e.Graphics.DrawString("접수일자", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY, sngPosX(2) - sngPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("환자명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, sngPosX(3) - sngPosX(2), sngPrt), sf_c)
        e.Graphics.DrawString("보험코드", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY, sngPosX(4) - sngPosX(3), sngPrt), sf_c)
        e.Graphics.DrawString("종   목   명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, sngPosX(5) - sngPosX(4), sngPrt), sf_c)
        e.Graphics.DrawString("금  액", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY, sngPosX(6) - sngPosX(5), sngPrt), sf_c)
        e.Graphics.DrawString("비  고", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(6), sngPosY, sngPosX(7) - sngPosX(6), sngPrt), sf_c)

        '-- 가로
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt, msgWidth, sngPosY + sngPrt)

        '-- 세로
        For ix As Integer = 0 To sngPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, sngPosX(ix), sngPosY - sngPrt / 2, sngPosX(ix), sngPosY + sngPrt)
        Next
        Return sngPosY + sngPrt '+ sngPrt / 2

    End Function

End Class