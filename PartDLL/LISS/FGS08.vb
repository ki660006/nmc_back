'>>> 미채혈 사유 대장
Imports System.Windows.Forms
Imports COMMON.CommFN
Imports COMMON.SVar
Imports LISAPP.APP_S.CollTkFn

Public Class FGS08
    Private mbQuery As Boolean = False
    '< add freety 2005/04/04 : Filter Off
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

                
            Next

            If iCol = 0 Then Return



            For j As Integer = 1 To .MaxRows
                .Col = iCol
                .Row = j

                
            Next

            If bFilter Then
                .ShadowColor = System.Drawing.Color.LightSteelBlue
            End If

            .ReDraw = True
        End With
    End Sub

    Private Sub sbDisplay_Init()

        Me.spdList.MaxRows = 0
        Me.spdList.MaxRows = 0

        With Me.spdList
            .Row = 0
            .Col = 1 : .ColID = "cancelcmt"
            .Col = 2 : .ColID = "cancelcd"
            .Col = 3 : .ColID = "tot"

            For intCol As Integer = 4 To .MaxCols
                .Col = intCol : .ColID = CStr(intCol - 3).ToString.PadLeft(2, "0"c)
            Next
        End With

    End Sub

    Protected Sub sbDisplay_DataView(ByVal r_dt As DataTable)
        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            With spd
                .MaxRows = 0

                .ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For intRow As Integer = 1 To r_dt.Rows.Count

                    For intIx1 As Integer = 1 To r_dt.Columns.Count
                        Dim iCol As Integer = 0

                        iCol = .GetColFromID(r_dt.Columns(intIx1 - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Row = intRow
                            .Col = iCol

                        End If
                    Next

                Next

                .Row = 1 : .Col = 1 : Dim tmp As String = .Text
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_StatisticsView(ByVal r_dt As DataTable)

        Try
            Dim sKey As String = ""

            With Me.spdList
                .MaxRows = 0

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    Dim sCancelCd As String = r_dt.Rows(ix).Item("cancelcd").ToString
                    If sCancelCd = "" Then sCancelCd = r_dt.Rows(ix).Item("cancelcmt").ToString

                    If sKey <> sCancelCd Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("cancelcmt") : .Text = r_dt.Rows(ix).Item("cancelcmt").ToString
                        .Col = .GetColFromID("cancelcd") : .Text = r_dt.Rows(ix).Item("cancelcd").ToString
                    End If
                    sKey = sCancelCd

                    .Row = .MaxRows
                    Dim iCol As Integer = .GetColFromID(r_dt.Rows(ix).Item("canceldt").ToString.Substring(5, 2))

                    If iCol > 0 Then
                        .Col = iCol : .Text = Convert.ToString(Convert.ToInt16(IIf(.Text = "", "0", .Text).ToString) + Convert.ToInt16(r_dt.Rows(ix).Item("cnt").ToString))
                    End If
                Next

                For iRow As Integer = 1 To .MaxRows
                    Dim intTot As Integer = 0

                    For intCol As Integer = 4 To .MaxCols
                        .Row = iRow
                        .Col = intCol : intTot += Convert.ToInt16(IIf(.Text = "", "0", .Text).ToString)
                    Next

                    .Row = iRow
                    .Col = .GetColFromID("tot") : .Text = intTot.ToString
                Next

                .MaxRows += 1
                .Row = .MaxRows
                .Col = .GetColFromID("cancelcmt") : .Text = "    전체건수"

                For intCol As Integer = 3 To .MaxCols
                    Dim intTot As Integer = 0

                    For intRow As Integer = 1 To .MaxRows - 1
                        .Col = intCol
                        .Row = intRow : intTot += Convert.ToInt16(IIf(.Text = "", "0", .Text).ToString)
                    Next

                    .Row = .MaxRows
                    .Col = intCol : .Text = intTot.ToString
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.spdList.ReDraw = True
        End Try
    End Sub

    Private Sub sbDisplay_Data()

        Try
            spdList.MaxRows = 0

            Dim sIoGbn As String = ""
            Dim sCancelGbn As String = ""
            Dim sDeptWards As String = ""

            If rdoIoGbnA.Checked Then
                sIoGbn = ""
            ElseIf rdoIoGbnO.Checked Then
                sIoGbn = "O"
            ElseIf rdoIoGbnI.Checked Then
                sIoGbn = "I"
            End If

            Dim dt As DataTable = fnGet_NoColl_Cmt_List(Me.dtpDateS.Text.Replace("-", ""), Me.dtpDateE.Text.Replace("-", ""), sIoGbn)
            If dt.Rows.Count < 1 Then Return

            sbDisplay_DataView(dt)
            dt = Nothing

            'dt = fnGet_CollTk_Cancel_Statistics(Me.dtpDateS.Text.Substring(0, 5).Replace("-", "") + "0101", Me.dtpDateE.Text.Replace("-", ""), sIoGbn, sCancelGbn, Me.chkDelGbn.Checked, sDeptWards, sSlipCd)
            'sbDisplay_StatisticsView(dt)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbSetFilterColumn()
        With Me.spdList
            .Row = 0

            For j As Integer = 1 To .MaxCols
                .Col = j

                
            Next
        End With
    End Sub

    ' 출력
    Private Sub sbToExcel(ByVal aiMode As Integer)
        Dim sBuf As String = ""

        Select Case aiMode
            Case 0
                With Me.spdList
                    .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub
                    .ReDraw = False
                    .MaxRows = .MaxRows + 1
                    .InsertRows(1, 1)

                    For i As Integer = 1 To .MaxCols
                        .Col = i : .Row = 0 : sBuf = .Text
                        .Col = i : .Row = 1 : .Text = sBuf
                    Next

                    If .ExportToExcel("cancel_list.xls", "cancel list", "") Then
                        Process.Start("cancel_list.xls")
                    End If

                    .DeleteRows(1, 1)
                    .MaxRows -= 1

                    .ReDraw = True

                End With

            Case 1
                With Me.spdList
                    .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub
                    .ReDraw = False

                    .MaxRows = .MaxRows + 1
                    .InsertRows(1, 1)

                    For i As Integer = 1 To .MaxCols
                        .Col = i : .Row = 0 : sBuf = .Text
                        .Col = i : .Row = 1 : .Text = sBuf
                    Next

                    If .ExportToExcel("cancel_sum.xls", "cancel sum", "") Then
                        Process.Start("cancel_sum.xls")
                    End If

                    .DeleteRows(1, 1)
                    .MaxRows -= 1

                    .ReDraw = True

                End With

        End Select
    End Sub

    Private Sub FGS07_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
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

   

    Private Sub FGS12_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.WindowState = FormWindowState.Maximized

        '-- 서버날짜로 설정
        Me.dtpDateE.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
        Me.dtpDateS.Value = CDate(Format(dtpDateE.Value, "yyyy-MM-").ToString + "01")

        sbDisplay_Init()

        sbSetFilterColumn()


        sbDisplay_slip()
    End Sub

    Private Sub sbDisplay_slip()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List()




           


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub


    Private Sub btnQuery_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbDisplay_Data()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub mnuRst_h_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRst_h.Click

        Dim objForm As New FGS07_01
        Dim strBcNo As String = ""

        With spdList
            .Row = .ActiveRow
            .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")
        End With

        objForm.Display_Data(Me, strBcNo)

    End Sub

    Private Sub btnFilterY_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        sbFilterOn()
    End Sub

    Private Sub btnFilterN_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        sbFilterOff()
    End Sub

    Private Sub chkCollMove_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCollMove.CheckedChanged
        Me.spdList.AllowColMove = chkCollMove.Checked

    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If mbQuery Then Return

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                'a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                'sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    'Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

    '    Try
    '        Dim arlPrint As New ArrayList

    '        If Me.cboPrint.SelectedIndex = 0 Then
    '            With spdList
    '                For intRow As Integer = 1 To .MaxRows
    '                    .Row = intRow
    '                    Dim strBuf() As String = rsTitle_Item.Split("|"c)
    '                    Dim arlItem As New ArrayList

    '                    For intIdx As Integer = 0 To strBuf.Length - 1

    '                        If strBuf(intIdx) = "" Then Exit For

    '                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

    '                        If intCol > 0 Then

    '                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
    '                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
    '                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

    '                            .Row = intRow
    '                            .Col = .GetColFromID(strField) : Dim strVal As String = .Text

    '                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
    '                        End If
    '                    Next

    '                    Dim objPat As New FGS00_PATINFO

    '                    With objPat
    '                        .alItem = arlItem
    '                    End With

    '                    arlPrint.Add(objPat)
    '                Next
    '            End With
    '        Else
    '            With spdStList
    '                For intRow As Integer = 1 To .MaxRows
    '                    .Row = intRow
    '                    Dim strBuf() As String = rsTitle_Item.Split("|"c)
    '                    Dim arlItem As New ArrayList

    '                    For intIdx As Integer = 0 To strBuf.Length - 1

    '                        If strBuf(intIdx) = "" Then Exit For

    '                        Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

    '                        If intCol > 0 Then

    '                            Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
    '                            Dim strField As String = strBuf(intIdx).Split("^"c)(1)
    '                            Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

    '                            .Row = intRow
    '                            .Col = .GetColFromID(strField) : Dim strVal As String = .Text

    '                            arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
    '                        End If
    '                    Next

    '                    Dim objPat As New FGS00_PATINFO

    '                    With objPat
    '                        .alItem = arlItem
    '                    End With

    '                    arlPrint.Add(objPat)
    '                Next
    '            End With
    '        End If
    '        If arlPrint.Count > 0 Then
    '            Dim prt As New FGS00_PRINT

    '            prt.mbLandscape = True  '-- false : 세로, true : 가로
    '            prt.msTitle = "채혈/접수 취소 내역"
    '            prt.maPrtData = arlPrint

    '            prt.sbPrint_Preview()
    '        End If
    '    Catch ex As Exception
    '        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
    '    End Try
    'End Sub

    'Private Function fnGet_prt_iteminfo() As ArrayList
    '    Dim alItems As New ArrayList
    '    Dim stu_item As STU_PrtItemInfo

    '    If Me.cboPrint.SelectedIndex = 0 Then

    '        With spdList
    '            For ix As Integer = 1 To .MaxCols

    '                .Row = 0 : .Col = ix
    '                If .ColHidden = False Then
    '                    stu_item = New STU_PrtItemInfo

    '                    If .ColID = "deldt" Or .ColID = "cancelnm" Or .ColID = "regno" Or .ColID = "patnm" Or _
    '                       .ColID = "wardroom" Or .ColID = "bcno" Or .ColID = "cancelgbn" Or .ColID = "cancelcmt" Then
    '                        stu_item.CHECK = "1"
    '                    Else
    '                        stu_item.CHECK = "0"
    '                    End If
    '                    stu_item.TITLE = .Text
    '                    stu_item.FIELD = .ColID
    '                    If .ColID = "tatcont" Then
    '                        stu_item.WIDTH = (.get_ColWidth(ix) * 10 + 50).ToString
    '                    Else
    '                        stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString
    '                    End If
    '                    alItems.Add(stu_item)
    '                End If
    '            Next

    '        End With
    '    Else
    '        With spdStList
    '            For ix As Integer = 1 To .MaxCols

    '                .Row = 0 : .Col = ix
    '                If .ColHidden = False Then
    '                    stu_item = New STU_PrtItemInfo

    '                    stu_item.CHECK = "1"

    '                    stu_item.TITLE = .Text
    '                    stu_item.FIELD = .ColID
    '                    stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString

    '                    alItems.Add(stu_item)
    '                End If
    '            Next

    '        End With
    '    End If
    '    Return alItems

    'End Function

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsIoGbn As String, ByVal rsDeptWard As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        If rsIoGbn = "I" Then
            Me.rdoIoGbnI.Checked = True

            Me.rdoIoGbnA.Enabled = False
            Me.rdoIoGbnI.Enabled = False
            Me.rdoIoGbnO.Enabled = False

        ElseIf rsIoGbn = "O" Then
            Me.rdoIoGbnO.Checked = True

            Me.rdoIoGbnA.Enabled = False
            Me.rdoIoGbnI.Enabled = False
            Me.rdoIoGbnO.Enabled = False

        End If

    End Sub
End Class