'>>> 재검통계
Imports System.Windows.Forms
Imports COMMON.CommFN
Imports COMMON.SVar
Imports LISAPP.APP_S.RstSrh
Imports COMMON.CommLogin.LOGIN

Public Class FGS17
    Private Const msXMLDir As String = "\XML"
    Private msSlipFile As String = Application.StartupPath & msXMLDir & "\FGS17_SLIP.XML"
    Private mbQuery As Boolean = False

    Private Sub sbFilterOff()
        With Me.spdList
            .ReDraw = False

            For i As Integer = 1 To .MaxRows
                .Row = i
                If .RowHidden Then
                    .RowHidden = False
                End If
            Next

            .ShadowColor = System.Drawing.Color.FromArgb(224, 224, 224)

            .ReDraw = True
        End With

    End Sub

    '< add freety 2005/04/04 : Filter On
    Private Sub sbFilterOn()
        Dim iCol As Integer = 0
        Dim bFilter As Boolean = False

        With Me.spdList
            .ReDraw = False

            For i As Integer = 1 To .MaxCols
                .Col = i
                .Row = 0

                If .Text = Me.cboFilter.Text Then
                    iCol = i

                    Exit For
                End If
            Next

            If iCol = 0 Then Return
            If Me.cboOp.SelectedIndex < 0 Then Return
            If Me.txtFilter.Text = "" Then Return

            For j As Integer = 1 To .MaxRows
                .Col = iCol
                .Row = j

                If Me.cboOp.Text = "=" Then
                    If Not .Text = Me.txtFilter.Text Then
                        .RowHidden = True
                        bFilter = True
                    End If
                ElseIf Me.cboOp.Text.ToUpper() = "LIKE" Then
                    If Not .Text.IndexOf(Me.txtFilter.Text) >= 0 Then
                        .RowHidden = True
                        bFilter = True
                    End If
                ElseIf Me.cboOp.Text = "<>" Then
                    If .Text = Me.txtFilter.Text Then
                        .RowHidden = True
                        bFilter = True
                    End If
                End If
            Next

            If bFilter Then
                .ShadowColor = System.Drawing.Color.LightSteelBlue
            End If

            .ReDraw = True
        End With
    End Sub

    Private Sub sbDisplay_Init()

        Me.spdList.MaxRows = 0
        Me.spdStList.MaxRows = 0

        With spdStList
            .Row = 0
            .Col = 1 : .ColID = "tclscd"
            .Col = 2 : .ColID = "spccd"
            .Col = 3 : .ColID = "tnmd"
            .MaxCols = 3
        End With

    End Sub

    Protected Sub sbDisplay_DataView(ByVal r_dt As DataTable)

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList
            Dim iRow As Integer = 0
            Dim sKey As String = ""

            With spd
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = 0

                For ix1 As Integer = 1 To r_dt.Rows.Count
                    If sKey <> r_dt.Rows(ix1 - 1).Item("bcno").ToString + r_dt.Rows(ix1 - 1).Item("testcd").ToString Then
                        .MaxRows += 1
                        iRow += 1
                    End If
                    sKey = r_dt.Rows(ix1 - 1).Item("bcno").ToString + r_dt.Rows(ix1 - 1).Item("testcd").ToString

                    For ix2 As Integer = 1 To r_dt.Columns.Count
                        Dim intCol As Integer = 0

                        intCol = .GetColFromID(r_dt.Columns(ix2 - 1).ColumnName.ToLower())

                        If intCol > 0 Then
                            .Row = iRow
                            .Col = intCol

                            If .Col > -1 And r_dt.Rows(ix1 - 1).Item(ix2 - 1).ToString() <> "" Then
                                .Text = r_dt.Rows(ix1 - 1).Item(ix2 - 1).ToString()
                            End If
                        End If
                    Next
                Next
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_StatisticsView(ByVal r_dt As DataTable)

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdStList
            Dim sKey As String = ""

            With spd
                .MaxRows = 0

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    If sKey <> r_dt.Rows(ix).Item("cancelcd").ToString Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("cancelcmt") : .Text = r_dt.Rows(ix).Item("cancelcmt").ToString
                        .Col = .GetColFromID("cancelcd") : .Text = r_dt.Rows(ix).Item("cancelcd").ToString
                    End If
                    sKey = r_dt.Rows(ix).Item("cancelcd").ToString

                    .Row = .MaxRows
                    Dim iCol As Integer = .GetColFromID(r_dt.Rows(ix).Item("orddt").ToString.Substring(5, 2))

                    If iCol > 0 Then
                        .Col = iCol : .Text = Convert.ToString(Convert.ToInt16(IIf(.Text = "", "0", .Text).ToString) + Convert.ToInt16(r_dt.Rows(ix).Item("cnt").ToString))
                    End If
                Next

                For iRow As Integer = 1 To .MaxRows
                    Dim iTot As Integer = 0

                    For iCol As Integer = 4 To .MaxCols
                        .Row = iRow
                        .Col = iCol : iTot += Convert.ToInt16(IIf(.Text = "", "0", .Text).ToString)
                    Next

                    .Row = iRow
                    .Col = .GetColFromID("tot") : .Text = iTot.ToString
                Next

                .MaxRows += 1
                .Row = .MaxRows
                .Col = .GetColFromID("cancelcmt") : .Text = "    전체건수"

                For iCol As Integer = 3 To .MaxCols
                    Dim iTot As Integer = 0

                    For iRow As Integer = 1 To .MaxRows - 1
                        .Col = iCol
                        .Row = iRow : iTot += Convert.ToInt16(IIf(.Text = "", "0", .Text).ToString)
                    Next

                    .Row = .MaxRows
                    .Col = iCol : .Text = iTot.ToString
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdStList.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_Data()

        Try
            Me.spdList.MaxRows = 0
            Me.spdStList.MaxRows = 0
            Me.spdStList.MaxCols = 3

            Dim sOrdDtS As String = "", sOrdDtE As String = "", sIoGbn As String = ""
            Dim sDW_Items As String = ""
            Dim sDW_Gbn As String = ""

            sOrdDtS = Me.dtpDate0.Text.Replace("-", "")
            sOrdDtE = Me.dtpDate1.Text.Replace("-", "")

            Dim dt As DataTable = fnGet_ReTest_List(sOrdDtS, sOrdDtE, Ctrl.Get_Code(Me.cboPartSlip))

            If dt.Rows.Count < 1 Then Return
            sbDisplay_DataView(dt)

            dt = Nothing

            Dim a_sDMY As String() = Nothing
            Dim iDMYDiff As Integer

            iDMYDiff = CInt(DateDiff(DateInterval.Day, CDate(Me.dtpDate0.Value), CDate(Me.dtpDate1.Value)))

            ReDim a_sDMY(iDMYDiff)

            For i As Integer = 1 To iDMYDiff + 1
                a_sDMY(i - 1) = DateAdd(DateInterval.Day, i - 1, CDate(Me.dtpDate0.Value)).ToShortDateString
            Next

            sbInitialize_spdStatistics(a_sDMY)

            dt = fnGet_ReTest_Statistics(a_sDMY, sOrdDtS, sOrdDtE, Ctrl.Get_Code(Me.cboPartSlip))

            With Me.spdStList
                .ReDraw = False
                .MaxRows = 0

                Dim iPos As Integer = -1
                Dim iSearchRow As Integer = 0

                For iRow As Integer = 0 To dt.Rows.Count - 1
                    iPos = .SearchCol(1, 0, .MaxRows, dt.Rows(iRow).Item("testcd").ToString(), FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iPos < 0 Then
                        .MaxRows += 1
                        .Row = .MaxRows

                        .Col = .GetColFromID("testcd")
                        .Text = dt.Rows(iRow).Item("testcd").ToString()

                        .Col = .GetColFromID("tnmd")
                        .Text = dt.Rows(iRow).Item("tnmd").ToString()

                        iSearchRow = .Row
                    Else
                        iSearchRow = iPos
                    End If

                    .Row = iSearchRow
                    .Col = .GetColFromID(dt.Rows(iRow).Item("rstdt").ToString())

                    If .Col > 0 Then
                        .Text = dt.Rows(iRow).Item("totcnt").ToString()

                        .Col = .Col + 1
                        .Text = dt.Rows(iRow).Item("recnt").ToString()

                        .Col = .Col + 1
                        .Text = (CDbl(dt.Rows(iRow).Item("recnt").ToString()) / CDbl(dt.Rows(iRow).Item("totcnt").ToString()) * 100).ToString()
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbInitialize_spdStatistics(ByVal ra_sDMY As String())

        Try
            With Me.spdStList
                .ReDraw = False

                '검사코드, 검체코드, 검사명, 검체명, Total
                .MaxCols += (ra_sDMY.Length * 3)

                .Col = 4 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True
                .CellType = FPSpreadADO.CellTypeConstants.CellTypeNumber : .TypeNumberDecPlaces = 0 : .Lock = True
                .BlockMode = False

                For i As Integer = 0 To ra_sDMY.Length - 1
                    .Col = 4 + (i * 3) : .Row = 0
                    .Text = ra_sDMY(i)
                    .ColID = .Text

                    .AddCellSpan(.Col, 0, 3, 1)

                    .Col = .Col
                    .Row = .ColHeaderRows + 1
                    .Text = "검사"
                    .set_ColWidth(.Col, 5.8)
                    .Col = .Col + 1
                    .set_ColWidth(.Col, 5.8)
                    .Text = "재검"
                    .Col = .Col + 1
                    .set_ColWidth(.Col, 5.8)
                    .Text = "재검율"


                    .Col = .Col : .Col2 = .Col : .Row = -1 : .Row2 = -1
                    .BlockMode = True
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeNumber : .TypeNumberDecPlaces = 2 : .Lock = True
                    .BlockMode = False


                Next
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally

        End Try
    End Sub

    Private Sub sbSetFilterColumn()
        With Me.spdList
            .Row = 0

            For j As Integer = 1 To .MaxCols
                .Col = j

                If .ColHidden = False Then
                    Me.cboFilter.Items.Add(.Text)
                End If
            Next
        End With
    End Sub

    ' 출력
    Private Sub sbToExcel(ByVal aiMode As Integer)
        Dim sBuf As String = ""

        Select Case aiMode
            Case 0
                With Me.spdList
                    .ReDraw = False
                    .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

                    .MaxRows = .MaxRows + 2
                    .InsertRows(1, 2)

                    .Col = 1 : .Col2 = .MaxCols
                    .Row = 1 : .Row2 = 2
                    .BlockMode = True
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .BlockMode = False

                    For i As Integer = 1 To .MaxCols
                        .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0 : sBuf = .Text
                        .Col = i : .Row = 1 : .Text = sBuf

                        .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1 : sBuf = .Text
                        .Col = i : .Row = 2 : .Text = sBuf
                    Next

                    If .ExportToExcel("cancel_list.xls", "cancel list", "") Then
                        Process.Start("cancel_list.xls")
                    End If

                    .DeleteRows(1, 2)
                    .MaxRows -= 2
                    .ReDraw = True
                End With

            Case 1
                With Me.spdStList
                    .ReDraw = False
                    .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

                    .MaxRows = .MaxRows + 2
                    .InsertRows(1, 2)

                    .Col = 1 : .Col2 = .MaxCols
                    .Row = 1 : .Row2 = 2
                    .BlockMode = True
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    .BlockMode = False

                    .Col = 1 : .Col2 = .MaxCols
                    .Row = 2 : .Row2 = 2
                    .BlockMode = True
                    .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
                    .BlockMode = False

                    For i As Integer = 1 To .MaxCols
                        .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 0 : sBuf = .Text
                        .Col = i : .Row = 1 : .Text = sBuf

                        .Col = i : .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1 : sBuf = .Text
                        .Col = i : .Row = 2 : .Text = sBuf
                    Next

                    If .ExportToExcel("cancel_sum.xls", "cancel sum", "") Then
                        Process.Start("cancel_sum.xls")
                    End If

                    .DeleteRows(1, 2)
                    .MaxRows -= 2
                    .ReDraw = True

                End With

        End Select
    End Sub

    Private Sub FGS17_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape : btnExit_ButtonClick(Nothing, Nothing)
            Case Keys.F4 : btnClear_ButtonClick(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Init()
    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub FGS17_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.WindowState = FormWindowState.Maximized

        '-- 서버날짜로 설정
        Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")) 'CDate(dtpDate1.Value.AddDays(-1))

        sbDisplay_Init()

        sbSetFilterColumn()
        Me.cboPrint.SelectedIndex = 0

        sbDisplay_slip()
    End Sub

    Private Sub sbDisplay_slip()
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List(, True)

            Me.cboPartSlip.Items.Clear()
            'cboSection.Items.Add("[--] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msSlipFile, "SLIP")

            If sTmp <> "" Then
                Me.cboPartSlip.SelectedIndex = CInt(IIf(sTmp = "", 0, sTmp))
            Else
                Me.cboPartSlip.SelectedIndex = 0
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbDisplay_Data()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub btnPrint_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        sbToExcel(Me.cboPrint.SelectedIndex)

    End Sub

    Private Sub btnFilterY_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterY.Click
        sbFilterOn()
    End Sub

    Private Sub btnFilterN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterN.Click
        sbFilterOff()
    End Sub

    Private Sub chkCollMove_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCollMove.CheckedChanged
        Me.spdList.AllowColMove = Me.chkCollMove.Checked

    End Sub

    Private Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboPartSlip.SelectedIndex.ToString)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mbQuery Then Return

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

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
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList

            With spdList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    Dim strBuf() As String = rsTitle_Item.Split("|"c)
                    Dim arlItem As New ArrayList

                    For intIdx As Integer = 0 To strBuf.Length - 1

                        If strBuf(intIdx) = "" Then Exit For

                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                        If intCol > 0 Then

                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                            .Row = intRow
                            .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                        End If
                    Next

                    Dim objPat As New FGS00_PATINFO

                    With objPat
                        .alItem = arlItem
                    End With

                    arlPrint.Add(objPat)
                Next
            End With

            If arlPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT

                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = "재검 내역 조회"
                prt.maPrtData = arlPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub


    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        With spdList
            For ix As Integer = 1 To .MaxCols

                .Row = 0 : .Col = ix
                If .ColID = "rst1" Or .ColID = "rst2" Or .ColID = "rst3" Or .ColID = "rst4" Or .ColID = "rst5" Then
                    .Row = FPSpreadADO.CoordConstants.SpreadHeader + 1
                End If
                If .ColHidden = False Then
                    stu_item = New STU_PrtItemInfo

                    If .ColID = "regno" Or .ColID = "patnm" Or .ColID = "vbcno" Or .ColID = "regdt" Or .ColID = "regid" Or .ColID = "tnms" Or _
                       .ColID = "viewrst" Or .ColID = "rst1" Or .ColID = "rst2" Then
                        stu_item.CHECK = "1"
                    Else
                        stu_item.CHECK = "0"
                    End If

                    If .ColID = "rst1" Then
                        stu_item.TITLE = "재검 1차"
                    ElseIf .ColID = "rst2" Then
                        stu_item.TITLE = "재검 2차"
                    ElseIf .ColID = "rst3" Then
                        stu_item.TITLE = "재검 3차"
                    ElseIf .ColID = "rst4" Then
                        stu_item.TITLE = "재검 4차"
                    ElseIf .ColID = "rst5" Then
                        stu_item.TITLE = "재검 5차"
                    Else
                        stu_item.TITLE = .Text
                    End If

                    stu_item.FIELD = .ColID

                    If .ColID = "tatcont" Then
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10 + 50).ToString
                    Else
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString
                    End If
                    alItems.Add(stu_item)
                End If
            Next

        End With

        Return alItems

    End Function

End Class