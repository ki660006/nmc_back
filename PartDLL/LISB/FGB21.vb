' CrossMating 결과 수정

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT
Public Class FGB21
    Private mobjDAF As New LISAPP.APP_F_COMCD
    Private Const mcFile As String = "File : B01.dll, Class : FGB21" + vbTab
    Private msRegno As String = ""
    Private msOrddt As String = ""

    Private Sub FGB21_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB21_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.F7
                btnExecute_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB21_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        Me.txtRegno.MaxLength = PRG_CONST.Len_RegNo

        ' 화면 오픈시 초기화
        spdList.MaxRows = 0

        dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).AddDays(-1)
        dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdList)

        sb_SetComboDt()
    End Sub

    Public Sub sb_SetComboDt(Optional ByVal asUSDT As String = "", Optional ByVal asUEDT As String = "")
        Dim sFn As String = "sb_SetComboDt"
        ' 콤보 데이터 생성
        Try
            Dim DTable As DataTable

            If asUSDT = "" Then asUSDT = "20000101"
            If asUEDT = "" Then asUEDT = "30000101"


            DTable = mobjDAF.GetComCdInfo(asUSDT)

            cboComCd.Items.Clear()
            cboComCd.Items.Add("[ALL] 전체")
            If DTable.Rows.Count > 0 Then
                With cboComCd
                    For i As Integer = 0 To DTable.Rows.Count - 1
                        .Items.Add(DTable.Rows(i).Item("COMNMD"))
                    Next
                End With
            Else
                Exit Sub
            End If
        Catch ex As Exception
            Fn.log(mcFile + sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, mcFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnPatPop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatPop.Click
        ' 환자 팝업 호출
        Dim sFn As String = "Private Sub btnPatPop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatPop.Click"
        Dim objHelp As New CDHELP.FGCDHELP99
        Dim lal_Header As New ArrayList
        Dim lal_Arg As New ArrayList
        Dim li_RtnCnt As Integer = 2
        Dim lal_Rtn As New ArrayList
        Dim ls_Regno As String = txtRegno.Text

        Try
            lal_Header.Add("환자번호")
            lal_Header.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LIS)
            lal_Arg.Add(" "c)
            lal_Arg.Add(" "c)

            lal_Rtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", lal_Arg, lal_Header, li_RtnCnt, "")

            If lal_Rtn.Count > 0 Then
                txtRegno.Text = lal_Rtn(0).ToString
                txtPatNm.Text = lal_Rtn(1).ToString

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            End If
        Catch ex As Exception
            Fn.log(mcFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    Private Sub txtRegno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegno.Click
        txtRegno.SelectAll()
    End Sub

    Private Sub txtRegno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown
        Dim objHelp As New CDHELP.FGCDHELP99
        Dim ls_Regno As String = ""
        Dim ls_OrderDate As String = ""
        Dim ls_TnsNum As String = ""

        Dim la_getValue As New ArrayList
        Dim lal_Header As New ArrayList
        Dim lal_Arg As New ArrayList
        Dim li_RtnCnt As Integer = 2
        Dim lal_Rtn As New ArrayList

        ' 등록번호 입력시 이벤트
        ls_Regno = txtRegno.Text

        If ls_Regno.Length() < 1 Then
            txtPatNm.Text = ""
            Return
        End If

        If e.KeyCode = Keys.Enter Then

            If IsNumeric(ls_Regno) Then
                If ls_Regno.Length() < 8 Then
                    ls_Regno = ls_Regno.PadLeft(8, "0"c)
                End If
            Else
                If ls_Regno.Length() < 8 Then
                    ls_Regno = ls_Regno.Substring(0, 1) + ls_Regno.Substring(1).PadLeft(7, "0"c)
                End If
            End If

            txtRegno.Text = ls_Regno

            lal_Header.Add("환자번호")
            lal_Header.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LIS)
            lal_Arg.Add(ls_Regno)
            lal_Arg.Add(ls_Regno)


            lal_Rtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", lal_Arg, lal_Header, li_RtnCnt, ls_Regno)

            If lal_Rtn.Count > 0 Then
                txtRegno.Text = lal_Rtn(0).ToString
                txtPatNm.Text = lal_Rtn(1).ToString

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            End If

            btnSearch_Click(Nothing, Nothing)

            If spdList.MaxRows < 1 Then Return

            With spdList
                .Row = 1
                .Col = .GetColFromID("order_date") : ls_OrderDate = .Text
                .Col = .GetColFromID("vtnsjubsuno") : ls_TnsNum = .Text.Replace("-", "")
            End With

            ' 환자정보 디스플레이
            AxTnsPatinfo1.sb_setPatinfo(ls_Regno, ls_OrderDate, ls_TnsNum)
        Else
            Return
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sFn As String = "CButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CButton1.Click"
        Dim dt As New DataTable
        Dim ls_Gbn As String = ""
        Dim ls_Comcd As String

        spdList.MaxRows = 0

        If rdoUnCom.Checked = True Then
            ls_Gbn = "N"c
        ElseIf rdoComplete.Checked = True Then
            ls_Gbn = "R"c
        End If

        ls_Comcd = Ctrl.Get_Code(cboComCd)

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회

            dt = CGDA_BT.fn_OutCrossList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), ls_Gbn, txtRegno.Text, ls_Comcd)

            sb_DisplayDataList(dt)

        Catch ex As Exception
            Fn.log(mcFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        spdList.MaxRows = 0
    End Sub

    Private Sub sb_DisplayDataList(ByVal rDt As DataTable)
        Dim sFn As String = "Private Sub sb_DisplayDataList(ByVal rDt As DataTable)"

        Dim lc_Color As Color
        Dim ls_Abo As String
        Dim li_crosslevel As Integer

        Try
            With spdList
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then
                    sb_SetStBarSearchCnt(0)
                    Return
                End If

                .ReDraw = False

                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("vtnsjubsuno") : .Text = rDt.Rows(i).Item("vtnsjubsuno").ToString
                    .Col = .GetColFromID("tnsgbn") : .Text = rDt.Rows(i).Item("tnsgbn").ToString
                    .Col = .GetColFromID("regno") : .Text = rDt.Rows(i).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = rDt.Rows(i).Item("patnm").ToString
                    .Col = .GetColFromID("sexage") : .Text = rDt.Rows(i).Item("sexage").ToString
                    .Col = .GetColFromID("patnm") : .Text = rDt.Rows(i).Item("patnm").ToString
                    .Col = .GetColFromID("sexage") : .Text = rDt.Rows(i).Item("sexage").ToString
                    .Col = .GetColFromID("vbldno") : .Text = rDt.Rows(i).Item("vbldno").ToString
                    .Col = .GetColFromID("comnmd") : .Text = rDt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("aborh") : .Text = rDt.Rows(i).Item("aborh").ToString
                    li_crosslevel = CInt(rDt.Rows(i).Item("crosslevel").ToString)

                    ls_Abo = rDt.Rows(i).Item("aborh").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(ls_Abo)

                    For j As Integer = 1 To 4
                        If j <= li_crosslevel Then
                            .Col = .GetColFromID("rst" + j.ToString) : .Text = rDt.Rows(i).Item("rst" + j.ToString).ToString
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeComboBox
                            .TypeComboBoxEditable = True
                            .TypeComboBoxList = "-" + Chr(9) + "+" + Chr(9) + "++" + Chr(9) + "+++" + Chr(9) + "++++" + Chr(9)
                        Else
                            .Col = .GetColFromID("rst" + j.ToString) : .Text = ""
                            .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        End If

                    Next

                    .Col = .GetColFromID("cmrmk") : .Text = rDt.Rows(i).Item("cmrmk").ToString
                    .BackColor = Drawing.Color.FromArgb(234, 249, 255)

                    .Col = .GetColFromID("testid") : .Text = rDt.Rows(i).Item("testid").ToString
                    .Col = .GetColFromID("testdt") : .Text = rDt.Rows(i).Item("testdt").ToString
                    .Col = .GetColFromID("testid2") : .Text = rDt.Rows(i).Item("testid2").ToString
                    .Col = .GetColFromID("testdt2") : .Text = rDt.Rows(i).Item("testdt2").ToString
                    .Col = .GetColFromID("befoutdt") : .Text = rDt.Rows(i).Item("befoutdt").ToString
                    .Col = .GetColFromID("outid") : .Text = rDt.Rows(i).Item("outid").ToString
                    .Col = .GetColFromID("outdt") : .Text = rDt.Rows(i).Item("outdt").ToString
                    .Col = .GetColFromID("recnm") : .Text = rDt.Rows(i).Item("recnm").ToString
                    .Col = .GetColFromID("bldno") : .Text = rDt.Rows(i).Item("bldno").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = rDt.Rows(i).Item("comcd_out").ToString
                    .Col = .GetColFromID("tnsjubsuno") : .Text = rDt.Rows(i).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("order_date") : .Text = rDt.Rows(i).Item("order_date").ToString

                Next

                sb_SetStBarSearchCnt(rDt.Rows.Count)

            End With
        Catch ex As Exception
            spdList.ReDraw = True
            Fn.log(mcFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)
        Finally
            spdList.ReDraw = True

        End Try
    End Sub

    Private Sub spdList_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdList.Change
        If spdList.MaxRows < 1 Then Return

        With spdList
            .Row = e.row
            .Col = .GetColFromID("chk") : .Text = "1"c
        End With
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If spdList.MaxRows < 1 Then Return

        Dim ls_Regno As String = ""
        Dim ls_Order_date As String = ""
        Dim ls_TnsNum As String = ""

        With spdList
            .Row = e.row
            .Col = .GetColFromID("regno") : ls_Regno = .Text
            .Col = .GetColFromID("order_date") : ls_Order_date = .Text
            .Col = .GetColFromID("vtnsjubsuno") : ls_TnsNum = .Text.Replace("-", "")
        End With

        If ls_Regno + ls_Order_date <> msRegno + msOrddt Then
            ' 환자정보 디스플레이
            AxTnsPatinfo1.sb_setPatinfo(ls_Regno, ls_Order_date, ls_TnsNum)
        End If

        msRegno = ls_Regno
        msOrddt = ls_Order_date

    End Sub


    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        Dim li_Cnt As Integer = 0
        Dim ls_Chk As String
        Dim lal_Arg As New ArrayList
        Dim lb_Continue As Boolean
        Dim lb_ok As Boolean

        With spdList
            If .MaxRows < 1 Then Return

            For i As Integer = 1 To .MaxRows
                .Row = i
                .Col = .GetColFromID("chk") : ls_Chk = .Text

                If ls_Chk <> "1" Then Continue For

                Dim lcls_cross As New STU_TnsJubsu

                li_Cnt += 1

                .Col = .GetColFromID("bldno") : lcls_cross.BLDNO = .Text
                .Col = .GetColFromID("comcd_out") : lcls_cross.COMCD = .Text
                .Col = .GetColFromID("tnsjubsuno") : lcls_cross.TNSJUBSUNO = .Text
                lcls_cross.TESTID = USER_INFO.USRID
                .Col = .GetColFromID("rst1") : lcls_cross.RST1 = .Text
                .Col = .GetColFromID("rst2") : lcls_cross.RST2 = .Text
                .Col = .GetColFromID("rst3") : lcls_cross.RST3 = .Text
                .Col = .GetColFromID("rst4") : lcls_cross.RST4 = .Text
                .Col = .GetColFromID("cmrmk") : lcls_cross.CMRMK = .Text

                lal_Arg.Add(lcls_cross)
            Next
        End With

        If li_Cnt < 1 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 결과저장할 항목이 없습니다.")
            Return
        End If

        lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "CrossMatching 결과저장 하시겠습니까?")

        If lb_Continue = False Then Return

        Try
            lb_ok = (New TnsReg).fn_CrossSaveSecond(lal_Arg)

            If lb_ok = True Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "저장 되었습니다.")
                btnSearch_Click(Nothing, Nothing)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "CrossMatching 결과저장 처리중 오류가 발생 하였습니다.")
                btnSearch_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub rdoUnCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoUnCom.Click
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub rdoComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoComplete.Click
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub btnTExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTExcel.Click
        Dim xlsApp As Excel.Application
        Dim xlsBook As Excel.Workbook
        Dim xlsSheet As Excel.Worksheet

        Dim intRow As Integer, intCol As Integer, intCnt As Integer
        Dim intExcell As Integer, strExcell As String = ""
        Dim strDta() As String

        xlsApp = New Excel.Application
        xlsApp.Visible = True
        xlsBook = xlsApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet)

        xlsSheet = CType(xlsBook.Worksheets(1), Excel.Worksheet)

        For intRow = 0 To spdList.MaxRows
            With spdList
                intCnt = 0
                intExcell = 64 : strExcell = ""
                For intCol = 2 To spdList.MaxCols
                    .Row = intRow + 1
                    .Col = intCol
                    If Not .ColHidden Then
                        intCnt = intCnt + 1
                        ReDim Preserve strDta(intCnt)

                        .Row = intRow
                        .Col = intCol
                        strDta(intCnt) = .Text
                    End If
                Next
            End With

            intExcell = 64 : strExcell = ""
            For intCol = 1 To intCnt
                intExcell = intExcell + 1
                If intExcell = 91 Then
                    intExcell = 65
                    If strExcell = "" Then
                        strExcell = "A"
                    Else
                        strExcell = Chr(Asc(strExcell) + 1)
                    End If
                End If
                If intRow = 0 Then
                    xlsSheet.Range(strExcell & Chr(intExcell) & "1").ColumnWidth = COMMON.CommFN.Fn.LengthH(strDta(intCol))
                End If
                xlsSheet.Range(strExcell & Chr(intExcell) & CStr(intRow + 4) & ":" & strExcell & Chr(intExcell) & CStr(intRow + 4)).Value2 = "'" & strDta(intCol)
            Next
        Next
        xlsSheet.Range("A1:" & strExcell & Chr(intExcell) & "1").Merge()
        xlsSheet.Range("A1:" & strExcell & Chr(intExcell) & "1").Value2 = "응급수혈 Cross Matching 결과대장"
        xlsSheet.Range("A1:" & strExcell & Chr(intExcell) & "1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        xlsSheet.Range("A1:" & strExcell & Chr(intExcell) & "1").Font.Size = 16
        xlsSheet.Range("A1:" & strExcell & Chr(intExcell) & "1").Font.Underline = True
        xlsSheet.Range("A1").RowHeight = 20

        xlsApp.Visible = True
    End Sub

End Class