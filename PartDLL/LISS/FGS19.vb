'>>> 재검통계
Imports System.Windows.Forms
Imports COMMON.CommFN
Imports COMMON.SVar
Imports LISAPP.APP_S.RstSrh
Imports COMMON.CommLogin.LOGIN
Imports System.Drawing

Public Class FGS19
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


    Private Sub sbDisplay_Init()

        Me.spdList.MaxRows = 0
        txtSelTest.Text = ""

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

    Private Sub sbDisplay_Data(ByVal rsBcno As String)
        Try
            'Me.spdList.MaxRows = 0

            Dim sOrdDtS As String = "", sOrdDtE As String = "", sIoGbn As String = ""
            Dim sDW_Items As String = ""
            Dim sDW_Gbn As String = ""
            Dim introw As Integer = 0

            'If txtSelTest.Text = "" Then
            '    MsgBox("검사항목을 선택하세요.")
            '    Return
            'End If

            sOrdDtS = Me.dtpDate0.Text.Replace("-", "")

            Dim sTestCds As String = ""

            If Me.txtSelTest.Text <> "" Then
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")
                ' sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"
            End If
            sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"

            Dim dt As DataTable = fn_get_HosRst2(sOrdDtS, sTestCds, Ctrl.Get_Code(Me.cboPartSlip), rsBcno)

            If dt.Rows.Count < 1 Then
                MsgBox("조회된 항목이 없습니다.")
                Return
            End If


            With Me.spdList
                .ReDraw = False
                .MaxRows += 1
                intRow = .MaxRows
                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = introw

                    .Col = .GetColFromID("hospinm") : .Text = dt.Rows(ix).Item("hospinm").ToString
                    .Col = .GetColFromID("hospital") : .Text = dt.Rows(ix).Item("hospital").ToString
                    .Col = .GetColFromID("usrnm") : .Text = dt.Rows(ix).Item("usrnm").ToString
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("sex") : .Text = dt.Rows(ix).Item("sex").ToString
                    .Col = .GetColFromID("birth") : .Text = dt.Rows(ix).Item("birth").ToString
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("deptcd").ToString
                    .Col = .GetColFromID("spcetc") : .Text = dt.Rows(ix).Item("spcetc").ToString
                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(ix).Item("spcnmd").ToString
                    .Col = .GetColFromID("etc") : .Text = dt.Rows(ix).Item("etc").ToString
                    .Col = .GetColFromID("etc2") : .Text = dt.Rows(ix).Item("etc2").ToString
                    .Col = .GetColFromID("hospicd") : .Text = dt.Rows(ix).Item("hospicd").ToString
                    .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString
                    .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString
                    '.Col = .GetColFromID("sysdt") : .Text = dt.Rows(ix).Item("sysdt").ToString
                    .Col = .GetColFromID("hospinm2") : .Text = dt.Rows(ix).Item("hospinm2").ToString
                    .Col = .GetColFromID("usrnm2") : .Text = dt.Rows(ix).Item("usrnm2").ToString
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString


                Next
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Data()

        Try
            Me.spdList.MaxRows = 0

            Dim sOrdDtS As String = "", sOrdDtE As String = "", sIoGbn As String = ""
            Dim sDW_Items As String = ""
            Dim sDW_Gbn As String = ""

            If txtSelTest.Text = "" Then
                MsgBox("검사항목을 선택하세요.")
                Return
            End If

            sOrdDtS = Me.dtpDate0.Text.Replace("-", "")

            Dim sTestCds As String = ""

            If Me.txtSelTest.Text <> "" Then
                sTestCds = Me.txtSelTest.Tag.ToString.Split("^"c)(0).Replace("|", ",")
                ' sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"
            End If
            sTestCds = "'" + sTestCds.Replace(",", "','").Trim + "'"

            Dim dt As DataTable = fn_get_HosRst2(sOrdDtS, sTestCds, Ctrl.Get_Code(Me.cboPartSlip), "")
            If dt.Rows.Count < 1 Then Return

            With Me.spdList
                .ReDraw = False
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1

                    .Col = .GetColFromID("hospinm") : .Text = dt.Rows(ix).Item("hospinm").ToString
                    .Col = .GetColFromID("hospital") : .Text = dt.Rows(ix).Item("hospital").ToString
                    .Col = .GetColFromID("usrnm") : .Text = dt.Rows(ix).Item("usrnm").ToString
                    .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                    .Col = .GetColFromID("sex") : .Text = dt.Rows(ix).Item("sex").ToString
                    .Col = .GetColFromID("birth") : .Text = dt.Rows(ix).Item("birth").ToString
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("deptcd").ToString
                    .Col = .GetColFromID("spcetc") : .Text = dt.Rows(ix).Item("spcetc").ToString
                    .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(ix).Item("spcnmd").ToString
                    .Col = .GetColFromID("etc") : .Text = dt.Rows(ix).Item("etc").ToString
                    .Col = .GetColFromID("etc2") : .Text = dt.Rows(ix).Item("etc2").ToString
                    .Col = .GetColFromID("hospicd") : .Text = dt.Rows(ix).Item("hospicd").ToString
                    .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString
                    .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString
                    '<<<20170324 신고일 삭제요청 으로 삭제함 
                    '.Col = .GetColFromID("sysdt") : .Text = dt.Rows(ix).Item("sysdt").ToString
                    .Col = .GetColFromID("hospinm2") : .Text = dt.Rows(ix).Item("hospinm2").ToString
                    .Col = .GetColFromID("usrnm2") : .Text = dt.Rows(ix).Item("usrnm2").ToString
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString

                Next
                .ReDraw = True
            End With

          

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
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
        '  Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")) 'CDate(dtpDate1.Value.AddDays(-1))

        sbDisplay_Init()


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


    Private Sub btnFilterN_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        sbFilterOff()
    End Sub


    Private Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXMLDir, msSlipFile, "SLIP", cboPartSlip.SelectedIndex.ToString)
        txtSelTest.Text = ""
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


    Private Sub btnCdHelp_test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click
        Try

            Dim sWGrpCd As String = ""
            Dim sTGrpCd As String = ""
            Dim sPartSlip As String = ""

            sPartSlip = Ctrl.Get_Code(Me.cboPartSlip)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(btnCdHelp_test)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_list(sPartSlip, sTGrpCd, sWGrpCd, , "")
            Dim a_dr As DataRow() = dt.Select("(tcdgbn = 'P'OR titleyn = '0')", "sort1, sort2, testcd")

            dt = Fn.ChangeToDataTable(a_dr)
            objHelp.FormText = "검사목록"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            If Me.txtSelTest.Text <> "" Then objHelp.KeyCodes = Me.txtSelTest.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnmp", "출력명", 0, , , True)
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("tcdgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("titleyn", "titleyn", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sTestCds As String = "", sTestNmds As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sTestCd As String = aryList.Item(ix).ToString.Split("|"c)(2)
                    Dim sTnmd As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sTestCds += "|" : sTestNmds += "|"
                    End If

                    sTestCds += sTestCd : sTestNmds += sTnmd
                Next

                Me.txtSelTest.Text = sTestNmds.Replace("|", ",")
                Me.txtSelTest.Tag = sTestCds + "^" + sTestNmds
            Else
                Me.txtSelTest.Text = ""
                Me.txtSelTest.Tag = ""
            End If


            Me.spdList.MaxRows = 0
            ' sbDisplay_Test()

            ' COMMON.CommXML.setOneElementXML(msXMLDir, msTESTFile, "TEST", Me.txtSelTest.Tag.ToString)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try

            Dim sBuf As String = ""

            With Me.spdList


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

                    '만약 해당 컬림이 Hidden이라면 해당 컬럼의 내용을 삭제한다.
                    .Row = 0
                    If .ColHidden.Equals(True) Then
                        '.DeleteCols(i, 1)
                        For i2 As Integer = 1 To .MaxRows
                            .Row = i2
                            .Text = ""
                        Next
                    End If

                Next

                If .ExportToExcel("병원체_검사결과_신고.xls", "병원체_검사결과_신고", "") Then
                    Process.Start("병원체_검사결과_신고.xls")
                End If

                .DeleteRows(1, 2)
                .MaxRows -= 2
                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown

        Dim dt As New DataTable
        Dim bFind As Boolean = False
        Dim sBcNo As String = ""

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        If Trim(txtBcNo.Text).Length = 0 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "검체번호를 입력해주세요.!!")
        Else
            sBcNo = Trim(txtBcNo.Text).Replace("-", "")

            If Len(sBcNo) = 11 Or Len(sBcNo) = 12 Then
                sBcNo = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(sBcNo.Substring(0, 11))
            End If

            If sBcNo.Length = 14 Then sBcNo += "0"

            Me.txtBcNo.Text = sBcNo

            With Me.spdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("bcno")
                    If .Text = sBcNo Then
                        MessageBox.Show("이미 리스트에 있는 검체입니다.!!")
                        Me.txtBcNo.Text = ""
                        Return
                    End If

                Next
            End With
            

            sbDisplay_Data(Me.txtBcNo.Text)
            

        End If
        Me.txtBcNo.Clear()
        Me.txtBcNo.Focus()
    End Sub
End Class