'>>> 혈액형 결과대장

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login
Imports LISAPP.APP_BT
Imports LISAPP.APP_BT.CGDA_BT

Public Class FGB25
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGB25.vb, Class : B01" & vbTab

    Private msABOCode As String = ""
    Private msRhCode As String = ""

#Region " Form내부 함수 "
    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "접수일시" : .WIDTH = "140" : .FIELD = "tkdt"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검체번호" : .WIDTH = "140" : .FIELD = "bcno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "등록번호" : .WIDTH = "80" : .FIELD = "regno"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "성명" : .WIDTH = "80" : .FIELD = "patnm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "성별/나이" : .WIDTH = "70" : .FIELD = "sexage"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "의뢰의사" : .WIDTH = "60" : .FIELD = "doctornm"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "진료과/병동" : .WIDTH = "120" : .FIELD = "dept"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "검체명" : .WIDTH = "80" : .FIELD = "spcnmd"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "ABO(1차)" : .WIDTH = "60" : .FIELD = msABOCode
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "Rh(1차)" : .WIDTH = "60" : .FIELD = msRhCode
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검사자(1차)" : .WIDTH = "80" : .FIELD = "rstid1"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "결과일시(1차)" : .WIDTH = "120" : .FIELD = "rstdt1"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "ABO(2차)" : .WIDTH = "60" : .FIELD = "abo"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "Rh(2차)" : .WIDTH = "60" : .FIELD = "rh"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "1" : .TITLE = "검사자(2차)" : .WIDTH = "80" : .FIELD = "rstid2"
        End With
        alItems.Add(stu_item)

        stu_item = New STU_PrtItemInfo
        With stu_item
            .CHECK = "" : .TITLE = "결과일시(2차)" : .WIDTH = "120" : .FIELD = "rstdt2"
        End With
        alItems.Add(stu_item)


        Return alItems

    End Function

    Private Sub sbForm_Initialize()
        Dim sFn As String = "Private Sub sbForm_Initialize()"

        Try
            Dim sTmp As String = (New LISAPP.APP_DB.ServerDateTime).GetDate("-") + " 00:00:00"

            Me.dtpDateS.Value = CDate(sTmp)
            Me.dtpDateE.Value = Me.dtpDateS.Value

            sbGet_ABOandRH_Code(msABOCode, msRhCode)

            With Me.spdList
                .MaxRows = 0
                .Col = .GetColFromID("abo") - 4 : .ColID = msABOCode
                .Col = .GetColFromID("rh") - 4 : .ColID = msRhCode
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub sbDisplay_Data(Optional ByVal rsBcNo As String = "")

        Try

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim dt As New DataTable

            dt = fnGet_ABOandRh_List(Me.dtpDateS.Text.Replace("-", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(" ", ""))
            sbDisplay_Data_View(dt)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    ' 환자리스트 보기
    Protected Sub sbDisplay_Data_View(ByVal r_dt As DataTable, Optional ByVal rbAdd As Boolean = False)
        Try
            Dim sBcNo As String = ""

            With Me.spdList
                If Not rbAdd Then .MaxRows = 0
                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    ' 새로운 바코드 구분
                    If sBcNo <> r_dt.Rows(ix).Item("bcno").ToString Then
                        .MaxRows += 1
                    End If

                    .Row = .MaxRows
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("dept") : .Text = r_dt.Rows(ix).Item("dept").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("diagnm") : .Text = r_dt.Rows(ix).Item("diagnm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("docrmk") : .Text = r_dt.Rows(ix).Item("doctorrmk").ToString.Trim

                    sBcNo = r_dt.Rows(ix).Item("bcno").ToString

                    Dim iCol As Integer = .GetColFromID(r_dt.Rows(ix).Item("testcd").ToString.Substring(0, 5))
                    If iCol > 0 And r_dt.Rows(ix).Item("testcd").ToString.Length > 5 Then

                        Select Case r_dt.Rows(ix).Item("testcd").ToString.Substring(5, 2)
                            Case "01"
                                .Col = iCol + 0 : .Text = r_dt.Rows(ix).Item("viewrst").ToString
                                If r_dt.Rows(ix).Item("testcd").ToString.StartsWith(msABOCode) Then
                                    .Col = iCol + 2 : .Text = r_dt.Rows(ix).Item("rstnm").ToString
                                    .Col = iCol + 3 : .Text = r_dt.Rows(ix).Item("rstdt").ToString
                                End If

                            Case "02"
                                .Col = iCol + 4 : .Text = r_dt.Rows(ix).Item("viewrst").ToString
                                If r_dt.Rows(ix).Item("testcd").ToString.StartsWith(msABOCode) Then
                                    .Col = iCol + 6 : .Text = r_dt.Rows(ix).Item("rstnm").ToString
                                    .Col = iCol + 7 : .Text = r_dt.Rows(ix).Item("rstdt").ToString
                                End If

                        End Select
                    End If
                Next
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub


    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)
        Dim sFn As String = "Sub sbPrint_Data()"

        Try
            Dim alPrint As New ArrayList

            With Me.spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    Dim sBuf() As String = rsTitle_Item.Split("|"c)
                    Dim alItem As New ArrayList

                    For ix As Integer = 0 To sBuf.Length - 1

                        If sBuf(ix) = "" Then Exit For

                        Dim iCol As Integer = .GetColFromID(sBuf(ix).Split("^"c)(1))

                        If iCol > 0 Then

                            Dim sTitle As String = sBuf(ix).Split("^"c)(0)
                            Dim sField As String = sBuf(ix).Split("^"c)(1)
                            Dim sWidth As String = sBuf(ix).Split("^"c)(2)

                            .Row = iRow
                            .Col = .GetColFromID(sField) : Dim sVal As String = .Text

                            alItem.Add(sVal + "^" + sTitle + "^" + sWidth + "^")
                        End If
                    Next

                    Dim objPat As New FGB00_PATINFO

                    With objPat
                        .alItem = alItem
                    End With

                    alPrint.Add(objPat)
                Next
            End With

            If alPrint.Count > 0 Then
                Dim prt As New FGB00_PRINT

                prt.msTitle = "혈액형 결과대장"
                prt.msTitle_sub_left_2 = Me.lblTitleDt.Text + ": " + Me.dtpDateS.Text + " ~ " + Me.dtpDateE.Text
                prt.maPrtData = alPrint
                prt.mbLandscape = True
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

#End Region

    Private Sub FGB25_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB25_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        With spdList
            .MaxRows = 0
            .MaxCols = .GetColFromID("spcnmd")
            .MaxCols += 6
        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Private Sub FGB25_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        Me.WindowState = FormWindowState.Maximized

        sbForm_Initialize()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim sFn As String = "Handles btnPrint.Click"

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISB.dll", "LISB.FGB00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sFn As String = "Sub btnExcel_ButtonClick()"
        Dim sBuf As String = ""

        Try
            With spdList
                .ReDraw = False

                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .RowHidden = True
                Next

                .Col = .GetColFromID("chk") : .ColHidden = True

                .MaxRows = .MaxRows + 1
                .InsertRows(1, 1)

                For i As Integer = 1 To .MaxCols
                    .Col = i : .Row = 0 : sBuf = .Text
                    .Col = i : .Row = 1 : .Text = sBuf
                Next

                If .ExportToExcel("혈액형결과_" + Now.ToShortDateString() + ".xls", "Worklist", "") Then
                    Process.Start("혈액형결과_" + Now.ToShortDateString() + ".xls")
                End If


                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk")
                    If .Text <> "1" And .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox Then .RowHidden = False
                Next

                .Col = .GetColFromID("chk") : .ColHidden = False

                .DeleteRows(1, 1)
                .MaxRows -= 1

                .ReDraw = True

            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try

    End Sub

    Private Sub spdList_BlockSelected(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BlockSelectedEvent) Handles spdList.BlockSelected
        Dim sFn As String = ""

        Try
            spdList.ClearSelection()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        sbDisplay_Data()
    End Sub

End Class

Public Class FGB25_PATINFO
    Public sRegNo As String = ""
    Public sPatNm As String = ""
    Public sSexAge As String = ""
    Public sDeptWard As String = ""
    Public sDoctorNm As String = ""
    Public sDocRmk As String = ""
    Public sSpcNmd As String = ""
    Public sBcNo As String = ""
    Public sWorkNo As String = ""
    Public sDiagNm As String = ""

    Public sPrtBcNo As String = ""

    Public sAbo_1 As String = ""
    Public sRh_1 As String = ""
    Public sAbo_2 As String = ""
    Public sRh_2 As String = ""

End Class

Public Class FGB25_PRINT
    Private Const msFile As String = "File : FGB25.vb, Class : B01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msTitle As String
    Public maPrtData As ArrayList
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")
    Public miTotExmCnt As Integer = 0

    Public Sub sbPrint_Preview(ByVal rbFixed As Boolean)
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = False
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

    Public Sub sbPrint(ByVal rbFixed As Boolean)
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

        Dim fnt_BarCd As New Font("Code39(2:3)", 18, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far


        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF

        If miCIdx = 0 Then miPageNo = 0

        For intIdx As Integer = miCIdx To maPrtData.Count - 1
            If sngPosY = 0 Then
                sngPosY = fnPrtTitle(e)
            End If

            '-- 번호
            rect = New Drawing.RectangleF(msgPosX(0), sngPosY, msgPosX(1) - msgPosX(0), sngPrtH)
            Dim strWkNo As String = CType(maPrtData.Item(intIdx), FGB25_PATINFO).sWorkNo.Substring(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sWorkNo.Length - 4)

            e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 검체번호
            rect = New Drawing.RectangleF(msgPosX(1), sngPosY + sngPrtH * 0, msgPosX(2) - msgPosX(1), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sBcNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 작업번호
            rect = New Drawing.RectangleF(msgPosX(2), sngPosY, msgPosX(3) - msgPosX(2), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sWorkNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 등록번호
            rect = New Drawing.RectangleF(msgPosX(3), sngPosY, msgPosX(4) - msgPosX(3), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sRegNo, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 성명
            rect = New Drawing.RectangleF(msgPosX(4), sngPosY + sngPrtH * 0, msgPosX(5) - msgPosX(4), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sPatNm, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- 성별/나이
            rect = New Drawing.RectangleF(msgPosX(5), sngPosY, msgPosX(6) - msgPosX(5), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sSexAge, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- 진료과/병동
            rect = New Drawing.RectangleF(msgPosX(6), sngPosY, msgPosX(7) - msgPosX(6), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sDeptWard, fnt_Body, Drawing.Brushes.Black, rect, sf_l)

            '-- ABO
            rect = New Drawing.RectangleF(msgPosX(7), sngPosY, msgPosX(8) - msgPosX(7), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sAbo_2, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            '-- Rh
            rect = New Drawing.RectangleF(msgPosX(8), sngPosY, msgPosX(9) - msgPosX(8), sngPrtH)
            e.Graphics.DrawString(CType(maPrtData.Item(intIdx), FGB25_PATINFO).sRh_2, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

            sngPosY += sngPrtH                              '데이터로우간 간격 조절
            If msgHeight - sngPrtH * 4 < sngPosY Then miCIdx += 1 : Exit For ' 전체크기에서 로우의높이를 나눈것보다 크면 다음페이지

            miCIdx += 1

            If (intIdx + 1) Mod 5 = 0 Then
                e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY, msgWidth, sngPosY)
            End If

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
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1

        Dim sngPosX(0 To 9) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40
        sngPosX(2) = sngPosX(1) + 140
        sngPosX(3) = sngPosX(2) + 140
        sngPosX(4) = sngPosX(3) + 80
        sngPosX(5) = sngPosX(4) + 85
        sngPosX(6) = sngPosX(5) + 80
        sngPosX(7) = sngPosX(6) + 85
        sngPosX(8) = sngPosX(7) + 70

        sngPosX(sngPosX.Length - 1) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 1.5)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - msgLeft - 25, sngPrt), sf_r)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt), sf_c)
        e.Graphics.DrawString("검체번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(1), sngPosY, sngPosX(2) - sngPosX(1), sngPrt), sf_c)
        e.Graphics.DrawString("작업번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(2), sngPosY, sngPosX(3) - sngPosX(2), sngPrt), sf_c)
        e.Graphics.DrawString("등록번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(3), sngPosY, sngPosX(4) - sngPosX(3), sngPrt), sf_c)
        e.Graphics.DrawString("성명", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(4), sngPosY, sngPosX(5) - sngPosX(4), sngPrt), sf_c)
        e.Graphics.DrawString("성별/나이", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(5), sngPosY, sngPosX(6) - sngPosX(5), sngPrt), sf_c)
        e.Graphics.DrawString("진료과/병동", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(6), sngPosY, sngPosX(7) - sngPosX(6), sngPrt), sf_c)
        e.Graphics.DrawString("ABO", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(7), sngPosY, sngPosX(8) - sngPosX(7), sngPrt), sf_c)
        e.Graphics.DrawString("Rh", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(8), sngPosY, sngPosX(9) - sngPosX(8), sngPrt), sf_c)

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt + sngPrt / 4, msgWidth, sngPosY + sngPrt + sngPrt / 4)

        msgPosX = sngPosX

        Return sngPosY + sngPrt + sngPrt / 2

    End Function



End Class