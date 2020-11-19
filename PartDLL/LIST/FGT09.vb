'>> TAT 관리
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports common.commlogin.login
Imports LISAPP.APP_T

Public Class FGT09
    Inherits System.Windows.Forms.Form

    Private Sub sbDisplay_Statistics()

        Try

            Dim dt As DataTable
            Dim alDepts As New ArrayList
            Dim sStType As String = "T", sSame As String = "", sSpc As String = "", sTCdGbn As String = "", sIoGbn As String = "O"
            Dim sTestCd As String = ""

            With Me.spdStatistics
                .MaxRows = 0
                .MaxCols = .GetColFromID("total")
            End With

            If Me.rdoIOO.Checked Then sIoGbn = "O"
            If Me.rdoIOI.Checked Then sIoGbn = "I"
            If Me.rdoIOA.Checked Then sIoGbn = "A"

            If Me.rdoOptDT2.Checked Then sStType = "F"

            '대표검사 적용
            If Me.chkSameCd.Checked Then sSame = "Y"

            '검체코드 적용
            If Me.chkSpcCd.Checked Then sSpc = "Y"

            'Single, Parent 검사만 적용
            If Me.chkTcls_s_p.Checked And Me.chkTcls_b.Checked Then
                sTCdGbn = "'B', 'S', 'P'"
            ElseIf Me.chkTcls_s_p.Checked Then
                sTCdGbn = "'S', 'P'"
            ElseIf Me.chkTcls_b.Checked Then
                sTCdGbn = "B"
            End If

            dt = (New SrhFn).fnGet_Test_Statistics_dept(sStType, Ctrl.Get_Code(Me.cboSlip), Me.dtpDT1.Text.Replace("-", ""), sIoGbn, sSame, sSpc, sTCdGbn)

            If dt.Rows.Count > 0 Then
                With Me.spdStatistics
                    .ReDraw = False

                    For ix As Integer = 0 To dt.Rows.Count - 1
                        If alDepts.Contains(dt.Rows(ix).Item("stdeptcd").ToString) Then
                        Else
                            alDepts.Add(dt.Rows(ix).Item("stdeptcd").ToString)

                            .Row = 0
                            .MaxCols += 1
                            .Col = .MaxCols : .ColID = dt.Rows(ix).Item("stdeptcd").ToString : .Text = dt.Rows(ix).Item("deptnm").ToString
                        End If

                        If sTestCd <> dt.Rows(ix).Item("testcd").ToString + dt.Rows(ix).Item("spccd").ToString Then
                            .MaxRows += 1

                            .Row = .MaxRows
                            .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.PadRight(8, " "c) + dt.Rows(ix).Item("spccd").ToString
                            .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnm").ToString
                        End If

                        .Row = .MaxRows
                        .Col = .GetColFromID(dt.Rows(ix).Item("stdeptcd").ToString) : .Text = dt.Rows(ix).Item("cnt").ToString

                        sTestCd = dt.Rows(ix).Item("testcd").ToString + dt.Rows(ix).Item("spccd").ToString
                    Next

                    For iRow As Integer = 1 To .MaxRows
                        Dim lgCnt As Long = 0

                        For iCol As Integer = .GetColFromID("total") + 1 To .MaxCols
                            .Row = iRow
                            .Col = iCol : lgCnt += CType(IIf(.Text = "", "0", .Text), Integer)
                        Next

                        .Row = iRow : .Col = .GetColFromID("total") : .Text = lgCnt.ToString
                    Next

                    .MaxRows += 1
                    .Row = .MaxRows
                    .Col = .GetColFromID("tnmd") : .Text = "합    계"

                    For iCol As Integer = .GetColFromID("total") To .MaxCols
                        Dim lgCnt As Long = 0

                        For iRow As Integer = 1 To .MaxRows - 1
                            .Row = iRow
                            .Col = iCol : lgCnt += CType(IIf(.Text = "", "0", .Text), Integer)
                        Next

                        .Row = .MaxRows : .Col = iCol : .Text = lgCnt.ToString
                    Next

                    .ReDraw = True
                End With
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_slip()

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List

            Me.cboSlip.Items.Clear()

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboSlip.Items.Add("[" + dt.Rows(i).Item("slipcd").ToString + "]" + " " + dt.Rows(i).Item("slipnmd").ToString)
                Next
            End If

            If Me.cboSlip.Items.Count > 0 Then Me.cboSlip.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbInitialize()

        Try
            Dim sCurSysDate As String = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")

            Me.dtpDT1.Value = CDate(sCurSysDate)

            sbDisplay_slip()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        With spdStatistics
            .ReDraw = False

            .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

            .MaxRows = .MaxRows + 1
            .InsertRows(1, 1)

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = 0 : sBuf = .Text
                .Col = i : .Row = 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = sBuf
            Next

            If .ExportToExcel("statistics.xls", "Statistics", "") Then
                Process.Start("statistics.xls")
            End If

            .DeleteRows(1, 1)
            .MaxRows -= 1

            .ReDraw = True
        End With
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdStatistics.MaxRows = 0
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            sbDisplay_Statistics()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub FGT09_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGT09_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                'btnPrint_ButtonClick(Nothing, Nothing)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        sbInitialize()

    End Sub


    Private Sub FGT09_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub rdoYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoYear.CheckedChanged, rdoMonth.CheckedChanged
        If Me.rdoMonth.Checked Then
            Me.dtpDT1.CustomFormat = "yyyy"
        Else
            Me.dtpDT1.CustomFormat = "yyyy-MM"
        End If

    End Sub
End Class

Public Class FGT09_PRTINFO
    Public sHour As String = ""
    Public sCnt As String = ""
    Public sAve As String = ""
    Public sMin As String = ""
    Public sMax As String = ""
    Public sOver As String = ""
    Public sPer As String = ""
    Public sCnt_o As String = ""
    Public sAve_o As String = ""
    Public sPer_o As String = ""
End Class


Public Class FGT09_PRINT
    Private Const msFile As String = "File : FGT09.vb, Class : T01" & vbTab

    Private miPageNo As Integer = 0
    Private miRow_Cur As Integer = 0

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 10

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msTitle As String
    Public msIoGbn As String
    Public msEmarYN As String
    Public msTAT_hour As String
    Public msSpcNm As String
    Public msDate_cur As String
    Public msDate_old As String

    Public maPrtData As ArrayList
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd hh:mm")

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

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Sub

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = True
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
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miRow_Cur = 1
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngTop As Single = 0, sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

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

        sngPrtH = CSng(fnt_Body.GetHeight(e.Graphics) * 1.3)

        Dim rect As New Drawing.RectangleF

        For intIdx As Integer = 0 To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngTop = fnPrtTitle(e)
                sngPosY = sngTop
            End If

            If intIdx = maPrtData.Count - 1 Then
                fnt_Body = New Font("굴림체", 10, FontStyle.Bold)
                sngPrtH += sngPrtH / 2
            End If

            If intIdx <> 0 Then sngPosY += sngPrtH

            '-- 시간대
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sHour, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 검사건수
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sCnt, fnt_Body, Drawing.Brushes.Black, rect, sf_r)
            '-- 평균
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sAve, fnt_Body, Drawing.Brushes.Black, rect, sf_r)
            '-- 최소
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sMin, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 최대
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sMax, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 초과
            rect = New Drawing.RectangleF(msgPosX(5), sngPosY, msgPosX(6) - msgPosX(5), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sOver, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- tat 충족율
            rect = New Drawing.RectangleF(msgPosX(6), sngPosY, msgPosX(7) - msgPosX(6), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sPer, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 기간 건수
            rect = New Drawing.RectangleF(msgPosX(7), sngPosY, msgPosX(8) - msgPosX(7), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sCnt_o, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 기간 평균
            rect = New Drawing.RectangleF(msgPosX(8), sngPosY, msgPosX(9) - msgPosX(8), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sAve_o, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            '-- 기간 충족율
            rect = New Drawing.RectangleF(msgPosX(9), sngPosY, msgPosX(10) - msgPosX(9), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGT09_PRTINFO).sPer_o, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

            miRow_Cur += 1

            e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH, msgWidth, sngPosY + sngPrtH)

            If msgHeight - sngPrtH * 5 < sngPosY + sngPrtH Then Exit For

        Next

        '-- 세로
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngTop, msgPosX(ix), sngPosY + sngPrtH)
        Next


        miPageNo += 1



        If miRow_Cur < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0.0

        Dim sngPosX(0 To 10) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 120
        For ix As Integer = 2 To 10
            sngPosX(ix) = sngPosX(ix - 1) + 110
        Next

        msgWidth = sngPosX(10)
        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = CSng(fnt_Title.GetHeight(e.Graphics) * (3 / 2))

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString("진단검사의학과 TAT관리(" + msTitle + ")", fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = CSng(fnt_Head.GetHeight(e.Graphics) * 1.5)

        '-- 환자구분
        e.Graphics.DrawString(msIoGbn, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 응급여부
        e.Graphics.DrawString(msEmarYN, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 목표 TAT
        e.Graphics.DrawString(msTAT_hour, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        sngPosY += sngPrt

        '-- 접수일자
        e.Graphics.DrawString(msDate_cur, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 기간대비
        e.Graphics.DrawString(msDate_old, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("시 간 대", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt * 2), sf_c)
        e.Graphics.DrawString("검사건수", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY, sngPosX(2) - sngPosX(1), sngPrt * 2), sf_c)
        e.Graphics.DrawString("평균소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, sngPosX(3) - sngPosX(2), sngPrt * 2), sf_c)
        e.Graphics.DrawString("최소소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY, sngPosX(4) - sngPosX(3), sngPrt * 2), sf_c)
        e.Graphics.DrawString("최대소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, sngPosX(5) - sngPosX(4), sngPrt * 2), sf_c)
        e.Graphics.DrawString("초과건수", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY, sngPosX(6) - sngPosX(5), sngPrt * 2), sf_c)
        e.Graphics.DrawString("TAT충족율(%)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(6), sngPosY, sngPosX(7) - sngPosX(6), sngPrt * 2), sf_c)
        e.Graphics.DrawString("기간대비 검사건수", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(7), sngPosY, sngPosX(8) - sngPosX(7), sngPrt * 2), sf_c)
        e.Graphics.DrawString("기간대비평균소요시간(분)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(8), sngPosY, sngPosX(9) - sngPosX(8), sngPrt * 2), sf_c)
        e.Graphics.DrawString("기간대비TAT충족율(%)", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(9), sngPosY, sngPosX(10) - sngPosX(9), sngPrt * 2), sf_c)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 2, msgWidth, sngPosY + sngPrt * 2)

        For ix As Integer = 0 To sngPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, sngPosX(ix), sngPosY - sngPrt / 2, sngPosX(ix), sngPosY + sngPrt * 2)
        Next

        msgPosX = sngPosX

        Return sngPosY + sngPrt * 2

    End Function

End Class