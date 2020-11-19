'>>> CrossMatching 등록(가출고)

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB07
    Private mobjDAF As New LISAPP.APP_F_COMCD
    Private miChkCnt As Integer = 0

    Private Sub FGB07_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Focus()
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB07_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.F7
                btnExecute_Click(Nothing, Nothing)
            Case Keys.F8
                btnExeCancel_Click(Nothing, Nothing)
            Case Keys.F9
                btnCrossApply_Click(Nothing, Nothing)
            Case Keys.F10
                btnCrossSave_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub FGB07_NEW_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        Me.txtRegno.MaxLength = PRG_CONST.Len_RegNo()

        Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

        ' 화면 오픈시 초기화
        spdOrderList.MaxRows = 0
        spdPastTns.MaxRows = 0
        spdTnsOrd.MaxRows = 0
        spdOutList.MaxRows = 0
        spdPreList.MaxRows = 0
        spdBldList.MaxRows = 0

        dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).AddDays(-1)
        dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_FormDesige.sbInti(Me)

        Me.txtBldno.Text = ""
        Me.txtBldNoBef.Text = ""

        sb_SetComboDt()
    End Sub

    Public Sub sb_SetComboDt(Optional ByVal rsUsDt As String = "", Optional ByVal rsUeDt As String = "")
        ' 콤보 데이터 생성
        Try

            Dim dt As DataTable = mobjDAF.GetComCdInfo("")

            Me.cboComCd.Items.Clear()
            Me.cboComCd.Items.Add("[ALL] 전체")
            If dt.Rows.Count > 0 Then
                With Me.cboComCd
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add(dt.Rows(ix).Item("COMNMD"))
                    Next
                End With
            Else
                Exit Sub
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnPatPop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatPop.Click
        ' 환자 팝업 호출
        Dim objHelp As New CDHELP.FGCDHELP99
        Dim lal_Header As New ArrayList
        Dim lal_Arg As New ArrayList
        Dim li_RtnCnt As Integer = 2
        Dim lal_Rtn As New ArrayList
        Dim ls_Regno As String = Me.txtRegno.Text

        Try
            lal_Header.Add("환자번호")
            lal_Header.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LIS)
            lal_Arg.Add(" "c)
            lal_Arg.Add(" "c)

            lal_Rtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", lal_Arg, lal_Header, li_RtnCnt, "")

            If lal_Rtn.Count > 0 Then
                Me.txtRegno.Text = lal_Rtn(0).ToString
                Me.txtPatNm.Text = lal_Rtn(1).ToString

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub txtRegno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegno.Click
        Me.txtRegno.SelectAll()
    End Sub

    Private Sub txtRegno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtRegno.Text = "" Then Return

        Try
            Dim objHelp As New CDHELP.FGCDHELP99
            Dim sRegno As String = ""
            Dim sOrdDt As String = ""
            Dim sTnsnum As String = ""
            Dim alHeader As New ArrayList
            Dim alArg As New ArrayList
            Dim iRtnCnt As Integer = 2
            Dim alRtn As New ArrayList

            ' 등록번호 입력시 이벤트
            sRegno = Me.txtRegno.Text

            If IsNumeric(sRegno) Then
                If sRegno.Length() < PRG_CONST.Len_RegNo Then
                    sRegno = sRegno.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                End If
            Else
                If sRegno.Length() < 8 Then
                    sRegno = sRegno.Substring(0, 1) + sRegno.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If
            End If

            Me.txtRegno.Text = sRegno

            alHeader.Add("환자번호")
            alHeader.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LIS)
            alArg.Add(sRegno)
            alArg.Add(sRegno)

            alRtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", alArg, alHeader, iRtnCnt, sRegno)

            If alRtn.Count > 0 Then
                Me.txtRegno.Text = alRtn(0).ToString
                Me.txtPatNm.Text = alRtn(1).ToString
            End If

            btnSearch_Click(Nothing, Nothing)

            Me.txtRegno.Text = "" : Me.txtPatNm.Text = ""
            If Me.spdOrderList.MaxRows < 1 Then Return

            With Me.spdOrderList
                .Row = 1
                .Col = .GetColFromID("order_date") : sOrdDt = .Text
                .Col = .GetColFromID("tnsjubsuno") : sTnsnum = .Text

            End With

            ' 환자정보 디스플레이
            Me.AxTnsPatinfo1.sb_setPatinfo(sRegno, sOrdDt, sTnsnum)
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sComcd As String = ""
            Dim sGbn As String = ""

            Me.spdOrderList.MaxRows = 0
            Me.spdPastTns.MaxRows = 0
            Me.spdTnsOrd.MaxRows = 0
            Me.spdOutList.MaxRows = 0
            Me.spdPreList.MaxRows = 0
            Me.spdBldList.MaxRows = 0

            Me.txtBcOrder.Text = ""
            Me.txtBcKeep.Text = ""
            Me.AxTnsPatinfo1.sb_ClearLbl()

            sComcd = Ctrl.Get_Code(Me.cboComCd)

            Me.txtBcOrder.Text = ""
            Me.txtBcKeep.Text = ""

            If Me.rdoAll.Checked = True Then
                sGbn = "0"c
            ElseIf Me.rdoUnCom.Checked = True Then
                sGbn = "1"c
            ElseIf Me.rdoComplete.Checked = True Then
                sGbn = "2"c
            End If

            ' 조회
            Dim dt As DataTable = CGDA_BT.fn_PreOrderList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), txtRegno.Text, sComcd, sGbn)

            sb_DisplayDataList(dt)

            Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            Cursor.Current = System.Windows.Forms.Cursors.Default
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub txtOrderNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOrderNum.Click
        Me.txtRegno.SelectAll()
    End Sub

    Private Sub txtKeepNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKeepNum.Click
        Me.txtRegno.SelectAll()
    End Sub

    Private Sub sb_DisplayDataList(ByVal r_dt As DataTable)

        Try

            With Me.spdOrderList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False

                For i As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("state") : .Text = r_dt.Rows(i).Item("state").ToString.Trim
                    .Col = .GetColFromID("spcstate") : .Text = r_dt.Rows(i).Item("spcstate").ToString.Trim
                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(i).Item("tnsjubsuno").ToString.Trim
                    .Col = .GetColFromID("vtnsjubsuno") : .Text = r_dt.Rows(i).Item("vtnsjubsuno").ToString.Trim
                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(i).Item("comnm").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(i).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(i).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(i).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("reqqnt") : .Text = r_dt.Rows(i).Item("reqqnt").ToString.Trim
                    .Col = .GetColFromID("bcno_order") : .Text = r_dt.Rows(i).Item("bcno_order").ToString.Trim
                    .Col = .GetColFromID("bcno_keep") : .Text = r_dt.Rows(i).Item("bcno_keep").ToString.Trim
                    .Col = .GetColFromID("order_date") : .Text = r_dt.Rows(i).Item("order_date").ToString.Trim
                    .Col = .GetColFromID("abo") : .Text = r_dt.Rows(i).Item("abo").ToString.Trim
                    .Col = .GetColFromID("rh") : .Text = r_dt.Rows(i).Item("rh").ToString.Trim
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(i).Item("comcd").ToString.Trim
                    .Col = .GetColFromID("spccd") : .Text = r_dt.Rows(i).Item("spccd").ToString.Trim
                    .Col = .GetColFromID("ir") : .Text = r_dt.Rows(i).Item("ir").ToString.Trim
                    .Col = .GetColFromID("filter") : .Text = r_dt.Rows(i).Item("filter").ToString.Trim
                    .Col = .GetColFromID("iogbn") : .Text = r_dt.Rows(i).Item("iogbn").ToString.Trim
                    .Col = .GetColFromID("owngbn") : .Text = r_dt.Rows(i).Item("owngbn").ToString.Trim
                    .Col = .GetColFromID("eryn") : .Text = r_dt.Rows(i).Item("eryn").ToString.Trim
                    .Col = .GetColFromID("tnsgbn") : .Text = r_dt.Rows(i).Item("tnsgbn").ToString.Trim
                    '.Col = .GetColFromID("comordcd") : .Text = r_dt.Rows(i).Item("comordcd").ToString.Trim
                Next

                sb_SetStBarSearchCnt(r_dt.Rows.Count)

                Me.txtOrderNum.Focus()
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdOrderList.ReDraw = True

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub FGB07_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtOrderNum.Focus()
    End Sub

    Private Sub spdOrderList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdOrderList.ClickEvent
        If Me.spdOrderList.MaxRows < 1 Then Return

        miChkCnt = 0
        sb_DisPlaySubData(e.row)
    End Sub

    Private Sub sb_DisPlaySubData(ByVal riRow As Integer)
        Try
            Dim dt As DataTable
            Dim sRegno As String = ""
            Dim sAbo As String = ""
            Dim sRh As String = ""
            Dim sTnsnum As String = ""
            Dim sOrdDt As String = ""
            Dim sComcd As String = ""
            Dim sSpcCd As String = ""
            Dim sfilter As String

            If Me.spdOrderList.MaxRows < 1 Then Return

            Me.spdPastTns.MaxRows = 0
            Me.spdTnsOrd.MaxRows = 0
            Me.spdOutList.MaxRows = 0
            Me.spdPreList.MaxRows = 0
            Me.spdBldList.MaxRows = 0

            Me.txtBcOrder.Text = ""
            Me.txtBcKeep.Text = ""

            Me.chkQnt.Checked = False
            Me.chkAbo.Checked = False
            Me.lblNoCross.Visible = False
            Me.btnBldchg.Enabled = True
            Me.chkAbo.Enabled = True

            With Me.spdOrderList
                .Row = riRow
                .Col = .GetColFromID("regno") : sRegno = .Text
                .Col = .GetColFromID("abo") : sAbo = .Text
                .Col = .GetColFromID("rh") : sRh = .Text
                .Col = .GetColFromID("tnsjubsuno") : sTnsnum = .Text
                .Col = .GetColFromID("order_date") : sOrdDt = .Text
                .Col = .GetColFromID("comcd") : sComcd = .Text
                .Col = .GetColFromID("spccd") : sSpcCd = .Text
                .Col = .GetColFromID("filter") : sfilter = .Text
                .Col = .GetColFromID("bcno_order") : txtBcOrder.Text = .Text
                .Col = .GetColFromID("bcno_keep") : txtBcKeep.Text = .Text
                .Col = .GetColFromID("tnsgbn")
                If .Text = "E" Then
                    Me.lblNoCross.Text = "교차미필(CrossMatching 없이 바로 출고)"
                    Me.lblNoCross.Visible = True
                    Me.btnBldchg.Enabled = False
                    Me.chkAbo.Enabled = False
                ElseIf .Text = "P" Then
                    Me.lblNoCross.Text = "준비(혈액준비 오더)"
                    Me.lblNoCross.Visible = True
                    Me.btnBldchg.Enabled = False
                    Me.chkAbo.Enabled = False
                End If
            End With

            If Me.txtBcOrder.Text.Length > 1 Then
                Me.txtBcOrder.ReadOnly = True
            Else
                Me.txtBcOrder.ReadOnly = False
            End If

            If Me.txtBcKeep.Text.Length > 1 Then
                Me.txtBcKeep.ReadOnly = True
            Else
                Me.txtBcKeep.ReadOnly = False
            End If

            ' 환자 정보 조회
            Me.AxTnsPatinfo1.sb_setPatinfo(sRegno, sOrdDt, sTnsnum)

            ' 과거수혈내역조회
            dt = CGDA_BT.fn_GetPastTnsList(sRegno, (New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
            sb_DisplayPastList(dt)

            ' 수혈의뢰내역 조회
            dt = CGDA_BT.fn_GetTnsCnt(sTnsnum)
            sb_DisPlayTnsCnt(dt)

            ' 혈액은행 보유혈액 조회
            dt = CGDA_BT.fn_GetStoreBldList(sAbo, sRh, sComcd.Trim, sSpcCd)
            sb_DisPlayBldList(dt)

            ' 가출고 목록 조회
            dt = CGDA_BT.fn_GetPreList(sTnsnum, sSpcCd)
            sb_DisPlayPreList(dt)

            ' 출고 목록 조회
            dt = CGDA_BT.fn_GetOutList(sTnsnum, sSpcCd, "") 'sfilter)
            sb_DisPlayOutList(dt)

            Dim sBcOrder As String
            Dim sBcKeep As String

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("bcno_order") : sBcOrder = .Text
                .Col = .GetColFromID("bcno_keep") : sBcKeep = .Text
            End With

            If sBcOrder.Length() + sBcKeep.Length() < 1 Then
                Me.txtBcOrder.Focus()
            Else
                Me.txtBldno.Focus()
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        
    End Sub

    ' 과거수혈내역조회
    Private Sub sb_DisplayPastList(ByVal r_dt As DataTable)

        Try
            With spdPastTns
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(ix).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("tnsgbn") : .Text = r_dt.Rows(ix).Item("tnsgbn").ToString
                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(ix).Item("comnm").ToString
                    .Col = .GetColFromID("reqqnt") : .Text = r_dt.Rows(ix).Item("reqqnt").ToString
                    .Col = .GetColFromID("outqnt") : .Text = r_dt.Rows(ix).Item("outqnt").ToString
                    .Col = .GetColFromID("rtnqnt") : .Text = r_dt.Rows(ix).Item("rtnqnt").ToString
                    .Col = .GetColFromID("abnqnt") : .Text = r_dt.Rows(ix).Item("abnqnt").ToString
                    .Col = .GetColFromID("cancelqnt") : .Text = r_dt.Rows(ix).Item("cancelqnt").ToString
                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdPastTns.ReDraw = True
        End Try
    End Sub

    ' 수혈접수의뢰 수량 조회
    Private Sub sb_DisPlayTnsCnt(ByVal r_dt As DataTable)

        Try
            With Me.spdTnsOrd
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return
                .ReDraw = False
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows : .set_RowHeight(.MaxRows, 12)

                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(ix).Item("comnm").ToString
                    .Col = .GetColFromID("filter") : .Text = r_dt.Rows(ix).Item("filter").ToString
                    .Col = .GetColFromID("ir") : .Text = r_dt.Rows(ix).Item("ir").ToString
                    .Col = .GetColFromID("reqqnt") : .Text = r_dt.Rows(ix).Item("reqqnt").ToString
                    .Col = .GetColFromID("befoutqnt") : .Text = r_dt.Rows(ix).Item("befoutqnt").ToString
                    .Col = .GetColFromID("outqnt") : .Text = r_dt.Rows(ix).Item("outqnt").ToString
                    .Col = .GetColFromID("rtnqnt") : .Text = r_dt.Rows(ix).Item("rtnqnt").ToString
                    .Col = .GetColFromID("abnqnt") : .Text = r_dt.Rows(ix).Item("abnqnt").ToString
                    .Col = .GetColFromID("cancelqnt") : .Text = r_dt.Rows(ix).Item("cancelqnt").ToString
                    .ForeColor = Color.Red
                Next

                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .FontBold = True
                .BlockMode = False
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdTnsOrd.ReDraw = True
        End Try
    End Sub

    ' 보유혈액 조회
    Private Sub sb_DisPlayBldList(ByVal rDt As DataTable)
        Dim lc_Color As Color

        Try
            With Me.spdBldList
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then
                    spdSum.Row = 1
                    spdSum.Col = spdSum.GetColFromID("bldsum") : spdSum.Text = ""
                    Return
                End If

                .ReDraw = False
                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("bldno") : .Text = rDt.Rows(i).Item("bldno").ToString
                    .Col = .GetColFromID("vbldno") : .Text = rDt.Rows(i).Item("vbldno").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = rDt.Rows(i).Item("comcd").ToString
                    .Col = .GetColFromID("comnmd") : .Text = rDt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("abo") : .Text = rDt.Rows(i).Item("abo").ToString

                    lc_Color = fnGet_BloodColor(rDt.Rows(i).Item("abo").ToString)

                    .Col = .GetColFromID("rh") : .Text = rDt.Rows(i).Item("rh").ToString

                    .Col = .GetColFromID("aborh") : .Text = rDt.Rows(i).Item("aborh").ToString
                    .ForeColor = lc_Color

                    .Col = .GetColFromID("dondt") : .Text = rDt.Rows(i).Item("dondt").ToString
                    .Col = .GetColFromID("indt") : .Text = rDt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("availdt") : .Text = rDt.Rows(i).Item("availdt").ToString
                    .Col = .GetColFromID("sortkey") : .Text = rDt.Rows(i).Item("sortkey").ToString
                    .Col = .GetColFromID("crosslevel") : .Text = rDt.Rows(i).Item("crosslevel").ToString
                    .Col = .GetColFromID("cmt") : .Text = rDt.Rows(i).Item("cmt").ToString
                    .Col = .GetColFromID("comordcd") : .Text = rDt.Rows(i).Item("comordcd").ToString
                Next
            End With

            With Me.spdSum
                .Row = 1
                .Col = spdSum.GetColFromID("bldsum") : .Text = spdBldList.MaxRows.ToString
                .FontBold = True
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdBldList.ReDraw = True
        End Try
    End Sub

    ' 가출고 목록 작성
    Private Sub sb_DisPlayPreList(ByVal r_dt As DataTable)

        Dim sState As String = ""
        Dim iCrosslevel As Integer

        Try
            With Me.spdPreList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For i As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    sState = r_dt.Rows(i).Item("state").ToString

                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(i).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("vstate") : .Text = r_dt.Rows(i).Item("vstate").ToString : If .Text = "접" Then .Col = .GetColFromID("chk") : .Text = "1"
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(i).Item("vbldno").ToString
                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(i).Item("comnm").ToString
                    .Col = .GetColFromID("type") : .Text = r_dt.Rows(i).Item("type").ToString

                    Dim sAbo As String = r_dt.Rows(i).Item("type").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(sAbo)

                    iCrosslevel = CInt(r_dt.Rows(i).Item("crosslevel").ToString)

                    If sState = "3"c Then
                        .Col = .GetColFromID("rst1") : .Text = r_dt.Rows(i).Item("rst1").ToString
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Col = .GetColFromID("rst2") : .Text = r_dt.Rows(i).Item("rst2").ToString
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Col = .GetColFromID("rst3") : .Text = r_dt.Rows(i).Item("rst3").ToString
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Col = .GetColFromID("rst4") : .Text = r_dt.Rows(i).Item("rst4").ToString
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        .Col = .GetColFromID("cmrmk") : .Text = r_dt.Rows(i).Item("cmrmk").ToString
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                    Else
                        For j As Integer = 1 To 4
                            If j <= iCrosslevel Then
                                .Col = .GetColFromID("rst" + j.ToString) : .Text = r_dt.Rows(i).Item("rst" + j.ToString).ToString : If .Text = "" Then .Text = "-"
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeComboBox
                                .TypeComboBoxEditable = True
                                .TypeComboBoxList = "-" + Chr(9) + "+" + Chr(9) + "++" + Chr(9) + "+++" + Chr(9) + "++++" + Chr(9)
                            Else
                                .Col = .GetColFromID("rst" + j.ToString) : .Text = r_dt.Rows(i).Item("rst" + j.ToString).ToString : If .Text = "" Then .Text = "-"
                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            End If

                        Next

                        .Col = .GetColFromID("cmrmk") : .Text = r_dt.Rows(i).Item("cmrmk").ToString
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypeEdit
                    End If

                    .Col = .GetColFromID("befoutdt") : .Text = r_dt.Rows(i).Item("befoutdt").ToString
                    .Col = .GetColFromID("inspector") : .Text = r_dt.Rows(i).Item("inspector").ToString
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("dondt") : .Text = r_dt.Rows(i).Item("dondt").ToString
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(i).Item("availdt").ToString
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(i).Item("comcd").ToString
                    .Col = .GetColFromID("owngbn") : .Text = r_dt.Rows(i).Item("owngbn").ToString
                    .Col = .GetColFromID("iogbn") : .Text = r_dt.Rows(i).Item("iogbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(i).Item("fkocs").ToString
                    .Col = .GetColFromID("bldno") : .Text = r_dt.Rows(i).Item("bldno").ToString
                    .Col = .GetColFromID("comnmp") : .Text = r_dt.Rows(i).Item("comnmp").ToString
                    .Col = .GetColFromID("comcdchk") : .Text = r_dt.Rows(i).Item("comcdchk").ToString
                    .Col = .GetColFromID("state") : .Text = r_dt.Rows(i).Item("state").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = r_dt.Rows(i).Item("comcd_out").ToString
                    .Col = .GetColFromID("cmt") : .Text = r_dt.Rows(i).Item("cmt").ToString
                    .Col = .GetColFromID("comordcd") : .Text = r_dt.Rows(i).Item("comordcd").ToString

                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdPreList.ReDraw = True
        End Try
    End Sub

    ' 출고 목록 작성
    Private Sub sb_DisPlayOutList(ByVal rDt As DataTable)

        Dim ls_state As String
        Dim lc_Color As Color = Color.Black
        Dim ls_Abo As String

        Try
            With Me.spdOutList
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then Return

                .ReDraw = False
                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    ls_state = rDt.Rows(i).Item("state").ToString
                    If ls_state = "5"c Then
                        lc_Color = Color.Blue
                    ElseIf ls_state = "6"c Then
                        lc_Color = Color.Red
                    Else
                        lc_Color = Color.Black
                    End If

                    .Col = .GetColFromID("vstate") : .Text = rDt.Rows(i).Item("vstate").ToString
                    .ForeColor = lc_Color
                    .Col = .GetColFromID("vbldno") : .Text = rDt.Rows(i).Item("vbldno").ToString
                    .ForeColor = lc_Color
                    .Col = .GetColFromID("comnm") : .Text = rDt.Rows(i).Item("comnm").ToString
                    .ForeColor = lc_Color
                    .Col = .GetColFromID("type") : .Text = rDt.Rows(i).Item("type").ToString

                    ls_Abo = rDt.Rows(i).Item("type").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(ls_Abo)

                    .Col = .GetColFromID("rst1") : .Text = rDt.Rows(i).Item("rst1").ToString
                    .Col = .GetColFromID("rst2") : .Text = rDt.Rows(i).Item("rst2").ToString
                    .Col = .GetColFromID("rst3") : .Text = rDt.Rows(i).Item("rst3").ToString
                    .Col = .GetColFromID("rst4") : .Text = rDt.Rows(i).Item("rst4").ToString
                    .Col = .GetColFromID("cmrmk") : .Text = rDt.Rows(i).Item("cmrmk").ToString
                    .Col = .GetColFromID("befoutdt") : .Text = rDt.Rows(i).Item("befoutdt").ToString
                    .Col = .GetColFromID("inspector") : .Text = rDt.Rows(i).Item("inspector").ToString
                    .Col = .GetColFromID("indt") : .Text = rDt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("dondt") : .Text = rDt.Rows(i).Item("dondt").ToString
                    .Col = .GetColFromID("availdt") : .Text = rDt.Rows(i).Item("availdt").ToString
                    .Col = .GetColFromID("outdt") : .Text = rDt.Rows(i).Item("outdt").ToString
                    .Col = .GetColFromID("outid") : .Text = rDt.Rows(i).Item("outid").ToString
                    .Col = .GetColFromID("recnm") : .Text = rDt.Rows(i).Item("recnm").ToString
                    .Col = .GetColFromID("cmt") : .Text = rDt.Rows(i).Item("cmt").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = rDt.Rows(i).Item("comcd_out").ToString
                    .Col = .GetColFromID("comordcd") : .Text = rDt.Rows(i).Item("comordcd").ToString

                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdOutList.ReDraw = True
        End Try
    End Sub


    ' 타용량 포함 여부
    Private Sub chkQnt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkQnt.CheckedChanged


        Try
            Dim dt As DataTable
            Dim ls_Regno As String
            Dim ls_Abo As String
            Dim ls_Rh As String
            Dim ls_Comcd As String
            Dim ls_Spccd As String
            Dim ls_Gubun As String
            Dim ls_Change As String

            spdBldList.MaxRows = 0

            With spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("regno") : ls_Regno = .Text
                .Col = .GetColFromID("abo") : ls_Abo = .Text
                .Col = .GetColFromID("rh") : ls_Rh = .Text
                .Col = .GetColFromID("comcd") : ls_Comcd = .Text
                .Col = .GetColFromID("spccd") : ls_Spccd = .Text
            End With

            If chkQnt.Checked = True Then
                ls_Gubun = "1"
            Else
                ls_Gubun = ""
            End If

            If chkAbo.Checked = True Then
                ls_Change = "1"
            Else
                ls_Change = ""
            End If

            ' 혈액은행 보유혈액 조회
            dt = CGDA_BT.fn_GetStoreBldList(ls_Abo, ls_Rh, ls_Comcd, ls_Spccd, ls_Gubun, ls_Change)
            sb_DisPlayBldList(dt)

            miChkCnt = 0
            txtBldno.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub rdoAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAll.Click
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub rdoUnCom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoUnCom.Click
        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub rdoComplete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoComplete.Click
        btnSearch_Click(Nothing, Nothing)
    End Sub

    '스프레드 체크박스 마우스기능 차단
    Private Sub spdBldList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdBldList.ButtonClicked
        Dim ls_chk As String
        Dim ls_Bldno As String = Me.txtBldno.Text
        Dim ls_CBldno As String

        With Me.spdBldList
            .Row = e.row
            .Col = .GetColFromID("bldno") : ls_CBldno = .Text
            .Col = .GetColFromID("chk") : ls_chk = .Text

            If ls_chk = "1"c Then
                If ls_Bldno = ls_CBldno Then
                    Return
                Else
                    .Text = ""
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "혈액번호를 마우스로 선택 할 수 없습니다.")
                End If
            End If
        End With

        Me.Focus()
        Me.txtBldno.Focus()
        Me.txtBldno.SelectAll()
    End Sub

    ' 혈액번호 입력 이벤트
    Private Sub ntxtBldno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldno.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If Me.spdOrderList.MaxRows < 1 Then Return

        Try
            Dim ls_Bldno As String
            Dim li_FRow As Integer
            Dim ls_Chk As String
            Dim li_ReqQnt As Integer
            Dim li_PreQnt As Integer = spdPreList.MaxRows
            Dim li_outQnt As Integer = spdOutList.MaxRows
            Dim li_RtnQnt As Integer = 0
            Dim li_CanQnt As Integer = 0
            Dim ls_Abo As String
            Dim ls_Rh As String
            Dim ls_aborh As String
            Dim lb_Continue As Boolean

            Dim sMsg As String = Me.AxTnsPatinfo1.Ab_Screen

            If sMsg <> "" Then CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, sMsg)

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("reqqnt") : li_ReqQnt = CInt(.Text)
                .Col = .GetColFromID("abo") : ls_Abo = .Text
                .Col = .GetColFromID("rh") : ls_Rh = .Text

                ls_aborh = ls_Abo + ls_Rh
            End With

            With Me.spdTnsOrd
                .Row = 1

                .Col = .GetColFromID("rtnqnt") : li_RtnQnt = CInt(.Text)
                .Col = .GetColFromID("abnqnt") : li_RtnQnt += CInt(.Text)
                .Col = .GetColFromID("cancelqnt") : li_CanQnt = CInt(.Text)
            End With

            ls_Bldno = Me.txtBldno.Text

            If ls_Bldno.Length() < 10 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "올바른 혈액번호가 아닙니다.")
                Me.txtBldno.Focus()
                Me.txtBldno.SelectAll()
            Else
                li_FRow = Fn.SpdColSearch(spdBldList, ls_Bldno, spdBldList.GetColFromID("bldno"))
                ' 혈액번호를 체크 및 체크된 혈액을 상단으로 정렬(체크된 순서대로정렬)
                If li_FRow <> 0 Then
                    With spdBldList
                        .Row = li_FRow
                        .Col = .GetColFromID("chk") : ls_Chk = .Text

                        .Col = .GetColFromID("aborh")
                        If .Text <> ls_aborh Then
                            lb_Continue = fn_PopConfirm(Me, "I"c, "혈액형이 일치하지 않습니다. 선택 하시겠습니까?")

                            If lb_Continue = False Then
                                Return
                            End If

                        End If

                        .Col = .GetColFromID("chk")

                        If ls_Chk = "" Then ls_Chk = "0"c

                        If ls_Chk = "0" Then

                            If li_ReqQnt - li_CanQnt <= miChkCnt + li_PreQnt + li_outQnt + li_RtnQnt Then
                                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰수량보다 많은 혈액을 선택 할 수 없습니다.")
                                txtBldno.Focus()
                                txtBldno.SelectAll()
                                Return
                            End If

                            miChkCnt += 1
                            .Text = "1"c
                            .Col = .GetColFromID("sortkey") : .Text = miChkCnt.ToString
                        Else
                            miChkCnt -= 1
                            .Text = "0"c
                            .Col = .GetColFromID("sortkey") : .Text = "9999999"
                        End If

                        ' 다중 Sort를 위한 설정
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = 1 : .Row2 = .MaxRows
                        .BlockMode = True
                        .SortBy = FPSpreadADO.SortByConstants.SortByRow
                        .set_SortKey(1, .GetColFromID("sortkey"))
                        .set_SortKey(2, .GetColFromID("comcd_out"))
                        .set_SortKey(3, .GetColFromID("availdt"))
                        .set_SortKey(4, .GetColFromID("bldno"))
                        .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                        .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                        .set_SortKeyOrder(3, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                        .set_SortKeyOrder(4, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                        .Action = FPSpreadADO.ActionConstants.ActionSort
                        .BlockMode = False

                        .Col = 1 : .Row = 1
                        .Action = FPSpreadADO.ActionConstants.ActionGotoCell

                        Me.txtBldno.Text = ""
                        Me.txtBldno.Focus()

                    End With
                ElseIf li_FRow = 0 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "일치하는 혈액번호를 찾을 수 없습니다.")
                    Me.txtBldno.Focus()
                    Me.txtBldno.SelectAll()
                End If

            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub ntxtBldno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBldno.Click, txtBldNoBef.Click
        CType(sender, TextBox).SelectAll()
        CType(sender, TextBox).SelectionStart = 0
    End Sub

    '' CrossMatching 적용 버튼이벤트
    'Private Sub btnCrossApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrossApply.Click
    '    If Me.spdOrderList.MaxRows < 1 Then Return
    '    If Me.spdBldList.MaxRows < 1 Then Return

    '    Try
    '        ' 의뢰검체 혹은 보관검체가 없다면 작업진행 불가
    '        Dim sBcOrder As String = ""
    '        Dim sBcKeep As String = ""

    '        With Me.spdOrderList
    '            .Row = .ActiveRow
    '            .Col = .GetColFromID("bcno_order") : sBcOrder = .Text
    '            .Col = .GetColFromID("bcno_keep") : sBcKeep = .Text
    '        End With

    '        If sBcOrder.Length() + sBcKeep.Length() < 1 Then
    '            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰검체 혹은 보관검체가 입력되지 않아 작업을 진행 할 수 없습니다.")
    '            Return
    '        End If

    '        If miChkCnt < 1 Then
    '            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택된 혈액이 없습니다.")
    '            txtBldno.Focus()
    '            txtBldno.SelectAll()
    '            Return
    '        End If

    '        Dim sChk As String = ""
    '        Dim alRstList As New ArrayList
    '        Dim iArow As Integer = 0
    '        Dim sComcd_out As String = ""
    '        Dim sComnm_out As String = ""
    '        Dim sType As String = ""
    '        Dim sIndt As String = ""
    '        Dim sDondt As String = ""
    '        Dim sAvaildt As String = ""
    '        Dim sComcd As String = ""
    '        Dim sBldno As String = ""
    '        Dim sTnsNum As String = ""
    '        Dim sCrossLevel As String = "4"c
    '        Dim sComOrdCd As String = ""
    '        Dim sIrra As String = ""
    '        Dim sFilter As String = ""
    '        Dim sEryn As String = ""
    '        Dim sOwngbn As String = ""
    '        Dim sIogbn As String = ""
    '        Dim sComnm As String = ""

    '        With Me.spdOrderList
    '            .Row = .ActiveRow
    '            .Col = .GetColFromID("comcd") : sComcd = .Text
    '            .Col = .GetColFromID("comnm") : sComnm = .Text
    '            .Col = .GetColFromID("tnsjubsuno") : sTnsNum = .Text
    '            .Col = .GetColFromID("ir") : sIrra = .Text
    '            .Col = .GetColFromID("filter") : sFilter = .Text
    '            .Col = .GetColFromID("eryn") : sEryn = .Text
    '            .Col = .GetColFromID("owngbn") : sOwngbn = .Text
    '            .Col = .GetColFromID("iogbn") : sIogbn = .Text
    '        End With

    '        With Me.spdBldList
    '            .ReDraw = False

    '            For iRow As Integer = .MaxRows To 1 Step -1
    '                .Row = iRow
    '                .Col = .GetColFromID("chk") : sChk = .Text

    '                If sChk = "" Then sChk = "0"c

    '                If sChk = "0"c Then Continue For

    '                .Col = .GetColFromID("comnmd") : sComnm_out = .Text
    '                .Col = .GetColFromID("aborh") : sType = .Text
    '                .Col = .GetColFromID("indt") : sIndt = .Text
    '                .Col = .GetColFromID("dondt") : sDondt = .Text
    '                .Col = .GetColFromID("availdt") : sAvaildt = .Text
    '                .Col = .GetColFromID("comcd_out") : sComcd_out = .Text
    '                .Col = .GetColFromID("bldno") : sBldno = .Text
    '                .Col = .GetColFromID("crosslevel") : sCrossLevel = .Text
    '                .Col = .GetColFromID("ordcomcd") : sComOrdCd = .Text

    '                Dim clsTestInfo As New STU_TnsJubsu

    '                clsTestInfo.TESTGBN = "0"c
    '                clsTestInfo.TESTID = USER_INFO.USRID
    '                clsTestInfo.EMER = sEryn
    '                clsTestInfo.IR = sIrra
    '                clsTestInfo.FILTER = sFilter
    '                clsTestInfo.COMCD = sComcd
    '                clsTestInfo.COMNM = sComnm
    '                clsTestInfo.IOGBN = sIogbn
    '                clsTestInfo.OWNGBN = sOwngbn
    '                clsTestInfo.RST1 = ""
    '                clsTestInfo.RST2 = ""
    '                clsTestInfo.RST3 = ""
    '                clsTestInfo.RST4 = ""
    '                clsTestInfo.CMRMK = ""
    '                clsTestInfo.TEMP01 = "I"c

    '                alRstList.Add(clsTestInfo)

    '                If sChk = "1" Then
    '                    With spdPreList
    '                        .ReDraw = False
    '                        .MaxRows += 1
    '                        iArow = .MaxRows

    '                        .Row = iArow
    '                        .Col = .GetColFromID("chk") : .Text = "1"c
    '                        .Col = .GetColFromID("vstate") : .Text = "접"c
    '                        .Col = .GetColFromID("vbldno") : .Text = Fn.BLDNO_View(sBldno)
    '                        .Col = .GetColFromID("comnm") : .Text = sComnm_out
    '                        .Col = .GetColFromID("type") : .Text = sType

    '                        For ix As Integer = 1 To 4
    '                            If ix <= CInt(sCrossLevel) Then
    '                                .Col = .GetColFromID("rst" + ix.ToString) : .Text = "-"c
    '                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeComboBox
    '                                .TypeComboBoxEditable = True
    '                                .TypeComboBoxList = "-" + Chr(9) + "+" + Chr(9) + "++" + Chr(9) + "+++" + Chr(9) + "++++" + Chr(9)
    '                            Else
    '                                .Col = .GetColFromID("rst" + ix.ToString) : .Text = ""
    '                                .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
    '                            End If
    '                        Next

    '                        .Col = .GetColFromID("indt") : .Text = sIndt
    '                        .Col = .GetColFromID("dondt") : .Text = sDondt
    '                        .Col = .GetColFromID("availdt") : .Text = sAvaildt
    '                        .Col = .GetColFromID("comcd") : .Text = sComcd
    '                        .Col = .GetColFromID("bldno") : .Text = sBldno
    '                        .Col = .GetColFromID("tnsjubsuno") : .Text = sTnsNum
    '                        .Col = .GetColFromID("comcd_out") : .Text = sComcd_out
    '                        If sComcd_out = sComcd Then
    '                            .Col = .GetColFromID("comcdchk") : .Text = "1"c
    '                        Else
    '                            .Col = .GetColFromID("comcdchk") : .Text = "0"c
    '                        End If

    '                        .Col = .GetColFromID("state") : .Text = "1"c
    '                        .Col = .GetColFromID("comordcd") : .Text = sComOrdCd


    '                    End With

    '                    .DeleteRows(iRow, 1)

    '                End If
    '            Next

    '            If alRstList.Count < 1 Then
    '                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 적용할 항목이 없습니다.")
    '                Return
    '            End If

    '            Dim bContinue As Boolean = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "CrossMatching 적용 하시겠습니까?")

    '            If bContinue = False Then Return

    '            If (New BefOut).fnExe_CrossSave(alRstList) Then
    '                sb_SubSearchCB()
    '            Else
    '                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 적용 처리중 오류가 발생 하였습니다.")
    '            End If

    '            Me.spdPreList.ReDraw = True
    '            Me.spdBldList.ReDraw = True
    '        End With

    '        btnCrossSave_Click(Nothing, Nothing)
    '    Catch ex As Exception
    '        fn_PopMsg(Me, "E"c, ex.Message)
    '    End Try

    'End Sub

    ' CrossMatching 적용 버튼이벤트
    Private Sub btnCrossApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrossApply.Click
        If Me.spdOrderList.MaxRows < 1 Then Return
        If Me.spdBldList.MaxRows < 1 Then Return

        Try
            ' 의뢰검체 혹은 보관검체가 없다면 작업진행 불가
            Dim sBcOrder As String = ""
            Dim sBcKeep As String = ""

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("bcno_order") : sBcOrder = .Text
                .Col = .GetColFromID("bcno_keep") : sBcKeep = .Text
            End With

            If sBcOrder.Length() + sBcKeep.Length() < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰검체 혹은 보관검체가 입력되지 않아 작업을 진행 할 수 없습니다.")
                Return
            End If

            If miChkCnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택된 혈액이 없습니다.")
                txtBldno.Focus()
                txtBldno.SelectAll()
                Return
            End If

            Dim sChk As String = ""
            Dim alRstList As New ArrayList
            Dim iArow As Integer = 0
            Dim sComcd_out As String = ""
            Dim sComnm_out As String = ""
            Dim sType As String = ""
            Dim sIndt As String = ""
            Dim sDondt As String = ""
            Dim sAvaildt As String = ""
            Dim sComcd As String = ""
            Dim sBldno As String = ""
            Dim sTnsNum As String = ""
            Dim sCrossLevel As String = "4"c
            Dim sComOrdCd As String = ""
            Dim sIrra As String = ""
            Dim sFilter As String = ""
            Dim sEryn As String = ""
            Dim sOwngbn As String = ""
            Dim sIogbn As String = ""
            Dim sComnm As String = ""

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("comcd") : sComcd = .Text
                .Col = .GetColFromID("comnm") : sComnm = .Text
                .Col = .GetColFromID("tnsjubsuno") : sTnsNum = .Text
                .Col = .GetColFromID("ir") : sIrra = .Text
                .Col = .GetColFromID("filter") : sFilter = .Text
                .Col = .GetColFromID("eryn") : sEryn = .Text
                .Col = .GetColFromID("owngbn") : sOwngbn = .Text
                .Col = .GetColFromID("iogbn") : sIogbn = .Text
            End With

            With Me.spdBldList
                .ReDraw = False

                For iRow As Integer = .MaxRows To 1 Step -1
                    .Row = iRow
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "" Then sChk = "0"c

                    If sChk = "0"c Then Continue For

                    '.Col = .GetColFromID("comnmd") : sComnm = .Text
                    .Col = .GetColFromID("aborh") : sType = .Text
                    .Col = .GetColFromID("indt") : sIndt = .Text
                    .Col = .GetColFromID("dondt") : sDondt = .Text
                    .Col = .GetColFromID("availdt") : sAvaildt = .Text
                    .Col = .GetColFromID("comcd_out") : sComcd_out = .Text
                    .Col = .GetColFromID("bldno") : sBldno = .Text
                    .Col = .GetColFromID("crosslevel") : sCrossLevel = .Text
                    .Col = .GetColFromID("comordcd") : sComOrdCd = .Text

                    Dim clsTestInfo As New STU_TnsJubsu

                    clsTestInfo.BLDNO = sBldno
                    clsTestInfo.COMCD_OUT = sComcd_out
                    clsTestInfo.TNSJUBSUNO = sTnsNum
                    clsTestInfo.TESTGBN = "0"c
                    clsTestInfo.TESTID = USER_INFO.USRID
                    clsTestInfo.EMER = sEryn
                    clsTestInfo.IR = sIrra
                    clsTestInfo.FILTER = sFilter
                    clsTestInfo.COMCD = sComcd
                    clsTestInfo.COMNM = sComnm
                    clsTestInfo.IOGBN = sIogbn
                    clsTestInfo.OWNGBN = sOwngbn
                    clsTestInfo.RST1 = ""
                    clsTestInfo.RST2 = ""
                    clsTestInfo.RST3 = ""
                    clsTestInfo.RST4 = ""
                    clsTestInfo.CMRMK = ""

                    alRstList.Add(clsTestInfo)

                Next

                If alRstList.Count < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 적용할 항목이 없습니다.")
                    Return
                End If

                Dim bContinue As Boolean = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "CrossMatching 적용 하시겠습니까?")

                If bContinue = False Then Return

                If (New BefOut).fnExe_CrossApply(alRstList) Then
                    sb_SubSearchCB()
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 적용 처리중 오류가 발생 하였습니다.")
                End If

                Me.spdPreList.ReDraw = True
                Me.spdBldList.ReDraw = True
            End With

            btnCrossSave_Click(Nothing, Nothing)


        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub


    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdOrderList.MaxRows = 0
        Me.spdPastTns.MaxRows = 0
        Me.spdTnsOrd.MaxRows = 0
        Me.spdOutList.MaxRows = 0
        Me.spdPreList.MaxRows = 0
        Me.spdBldList.MaxRows = 0

        Me.txtBcOrder.Text = ""
        Me.txtBcKeep.Text = ""
        Me.AxTnsPatinfo1.sb_ClearLbl()
    End Sub

    ' CrossMatching 취소
    Private Sub btnCrossCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCrossCancel.Click
        Dim ls_Chk As String
        Dim ls_State As String
        Dim li_BfCnt As Integer = 0
        Dim ls_TnsNum As String
        Dim ls_Bldno As String
        Dim ls_Comcd As String
        Dim ls_Comcd_out As String
        Dim ls_Owngbn As String
        Dim ls_Iogbn As String
        Dim ls_Fkocs As String
        Dim ls_ComcdChk As String
        Dim lal_arg As New ArrayList
        Dim lb_Continue As Boolean
        Dim lb_ok As Boolean

        With Me.spdPreList
            For i As Integer = .MaxRows To 1 Step -1
                .Row = i
                .Col = .GetColFromID("chk") : ls_Chk = .Text

                If ls_Chk = "" Then ls_Chk = "0"c

                If ls_Chk = "0"c Then Continue For

                .Col = .GetColFromID("state") : ls_State = .Text
                .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                .Col = .GetColFromID("bldno") : ls_Bldno = .Text
                .Col = .GetColFromID("comcd") : ls_Comcd = .Text
                .Col = .GetColFromID("owngbn") : ls_Owngbn = .Text
                .Col = .GetColFromID("iogbn") : ls_Iogbn = .Text
                .Col = .GetColFromID("fkocs") : ls_Fkocs = .Text
                .Col = .GetColFromID("comcdchk") : ls_ComcdChk = .Text
                .Col = .GetColFromID("comcd_out") : ls_Comcd_out = .Text

                If ls_State = "1"c Then
                    li_BfCnt += 1
                    ' 접수 상태의 경우 transaction 발생이 없으므로 단순 row삭제 처리
                    .DeleteRows(i, 1)
                ElseIf ls_State = "2"c Then
                    li_BfCnt += 1

                    Dim lcls_crsCnl As New STU_TnsJubsu

                    lcls_crsCnl.TNSJUBSUNO = ls_TnsNum
                    lcls_crsCnl.BLDNO = ls_Bldno
                    lcls_crsCnl.COMCD = ls_Comcd
                    lcls_crsCnl.COMCD_OUT = ls_Comcd_out
                    lcls_crsCnl.OWNGBN = ls_Owngbn
                    lcls_crsCnl.IOGBN = ls_Iogbn
                    lcls_crsCnl.FKOCS = ls_Fkocs
                    lcls_crsCnl.TEMP01 = ls_ComcdChk

                    lal_arg.Add(lcls_crsCnl)
                End If
            Next
        End With

        If li_BfCnt < 1 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 취소할 항목을 선택후 작업 하시기 바랍니다.")
            Return
        End If

        lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "CrossMatching 취소 처리 하시겠습니까?")

        If lb_Continue = False Then Return

        If lal_arg.Count() > 0 Then
            lb_ok = (New BefOut).fnexe_CrossCancel(lal_arg)
        Else
            lb_ok = True
        End If

        If lb_ok = True Then
            'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "CrossMatching 취소 되었습니다.")
            sb_SubSearchCB()
        Else
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 취소중 오류가 발생 하였습니다.")
            sb_SubSearchCB()
        End If

    End Sub

    ' 혈액보유리스트, 가출고리스트 조회
    Private Sub sb_SubSearchCB()

        Dim dt As DataTable
        Dim ls_Regno As String
        Dim ls_Abo As String
        Dim ls_Rh As String
        Dim ls_Comcd As String
        Dim ls_Spccd As String
        Dim ls_Gubun As String
        Dim ls_TnsNum As String

        miChkCnt = 0
        Me.spdBldList.MaxRows = 0

        With Me.spdOrderList
            .Row = .ActiveRow
            .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
            .Col = .GetColFromID("regno") : ls_Regno = .Text
            .Col = .GetColFromID("abo") : ls_Abo = .Text
            .Col = .GetColFromID("rh") : ls_Rh = .Text
            .Col = .GetColFromID("comcd") : ls_Comcd = .Text
            .Col = .GetColFromID("spccd") : ls_Spccd = .Text
        End With

        If chkQnt.Checked = True Then
            ls_Gubun = "1"
        Else
            ls_Gubun = ""
        End If

        chkAbo.Checked = False

        ' 수혈의뢰내역 조회
        dt = CGDA_BT.fn_GetTnsCnt(ls_TnsNum)
        sb_DisPlayTnsCnt(dt)

        ' 가출고 목록 재조회
        dt = CGDA_BT.fn_GetPreList(ls_TnsNum, ls_Spccd)
        sb_DisPlayPreList(dt)

        ' 혈액은행 보유혈액 조회
        dt = CGDA_BT.fn_GetStoreBldList(ls_Abo, ls_Rh, ls_Comcd, ls_Spccd, ls_Gubun)
        sb_DisPlayBldList(dt)
    End Sub

    ' 크로스매치 검사 저장
    Private Sub btnCrossSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCrossSave.Click

        Try
            Dim iCnt As Integer = 0
            Dim sState As String = ""
            Dim alRstList As New ArrayList
            Dim sIrra As String = ""
            Dim sFilter As String = ""
            Dim sComcd As String = ""
            Dim sComnm As String = ""
            Dim sEryn As String = ""
            Dim sOwngbn As String = ""
            Dim sIogbn As String = ""
            Dim bContinue As Boolean
            Dim bOk As Boolean

            With spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("ir") : sIrra = .Text
                .Col = .GetColFromID("filter") : sFilter = .Text
                .Col = .GetColFromID("comcd") : sComcd = .Text
                .Col = .GetColFromID("comnm") : sComnm = .Text
                .Col = .GetColFromID("eryn") : sEryn = .Text
                .Col = .GetColFromID("owngbn") : sOwngbn = .Text
                .Col = .GetColFromID("iogbn") : sIogbn = .Text
            End With

            With spdPreList
                If .MaxRows < 1 Then Return

                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("state") : sState = .Text

                    Dim lcls_cross As New STU_TnsJubsu

                    If sState = "1"c Or sState = "2"c Then
                        iCnt += 1

                        .Col = .GetColFromID("bldno") : lcls_cross.BLDNO = .Text
                        .Col = .GetColFromID("comcd_out") : lcls_cross.COMCD_OUT = .Text
                        .Col = .GetColFromID("tnsjubsuno") : lcls_cross.TNSJUBSUNO = .Text
                        lcls_cross.TESTGBN = "2"c
                        lcls_cross.TESTID = USER_INFO.USRID
                        .Col = .GetColFromID("rst1") : lcls_cross.RST1 = .Text
                        .Col = .GetColFromID("rst2") : lcls_cross.RST2 = .Text
                        .Col = .GetColFromID("rst3") : lcls_cross.RST3 = .Text
                        .Col = .GetColFromID("rst4") : lcls_cross.RST4 = .Text
                        .Col = .GetColFromID("cmrmk") : lcls_cross.CMRMK = .Text
                        lcls_cross.EMER = sEryn
                        lcls_cross.IR = sIrra
                        lcls_cross.FILTER = sFilter
                        lcls_cross.COMCD = sComcd
                        lcls_cross.COMNM = sComnm
                        lcls_cross.IOGBN = sIogbn
                        lcls_cross.OWNGBN = sOwngbn

                        If sState = "1"c Then
                            lcls_cross.TEMP01 = "I"c
                        Else
                            lcls_cross.TEMP01 = "U"c
                        End If

                        alRstList.Add(lcls_cross)

                    End If
                Next
            End With

            If iCnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 결과저장할 항목이 없습니다.")
                Return
            End If

            bContinue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "CrossMatching 결과저장 하시겠습니까?")

            If bContinue = False Then Return

            bOk = (New BefOut).fnExe_CrossSave(alRstList)

            If bOk = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "저장 되었습니다.")
                sb_SubSearchCB()
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "CrossMatching 결과저장 처리중 오류가 발생 하였습니다.")
                sb_SubSearchCB()
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        
    End Sub

    ' 텍스트 변경시 처리
    Private Sub spdPreList_Change(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ChangeEvent) Handles spdPreList.Change
        With Me.spdPreList
            .Row = e.row

            If e.col <> .GetColFromID("chk") Then
                .Col = .GetColFromID("chk") : .Text = "1"c
            End If

        End With
    End Sub

    ' 가출고 등록
    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        Try
            Dim iCnt As Integer = 0
            Dim sState As String = ""
            Dim alBefInfo As New ArrayList
            Dim sAbo As String = ""
            Dim sRh As String = ""
            Dim sFilter As String = ""
            Dim sComcd As String = ""
            Dim sComnm As String = ""
            Dim sEryn As String = ""
            Dim sOwngbn As String = ""
            Dim sIogbn As String = ""
            Dim bContinue As Boolean = False
            Dim bOk As Boolean = False
            Dim sRegno As String = ""
            Dim sOrdDt As String = ""
            Dim sSpccd As String = ""
            Dim sChk As String = ""
            Dim sTestId As String = ""

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("filter") : sFilter = .Text
                .Col = .GetColFromID("comcd") : sComcd = .Text
                .Col = .GetColFromID("comnm") : sComnm = .Text
                .Col = .GetColFromID("eryn") : sEryn = .Text
                .Col = .GetColFromID("owngbn") : sOwngbn = .Text
                .Col = .GetColFromID("iogbn") : sIogbn = .Text
                .Col = .GetColFromID("regno") : sRegno = .Text
                .Col = .GetColFromID("order_date") : sOrdDt = .Text
                .Col = .GetColFromID("spccd") : sSpccd = .Text

                .Col = .GetColFromID("abo") : sAbo = .Text
                .Col = .GetColFromID("rh") : sRh = .Text
            End With

            With Me.spdPreList
                If .MaxRows < 1 Then Return

                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : sChk = .Text
                    .Col = .GetColFromID("inspector") : sTestId = .Text

                    If sChk = "" Then sChk = "0"c

                    If sChk = "0"c Then Continue For

                    .Col = .GetColFromID("state") : sState = .Text

                    Dim lcls_PreOut As New STU_TnsJubsu

                    If sState = "2"c And sTestId <> "" Then
                        iCnt += 1

                        .Col = .GetColFromID("bldno") : lcls_PreOut.BLDNO = .Text
                        .Col = .GetColFromID("tnsjubsuno") : lcls_PreOut.TNSJUBSUNO = .Text
                        .Col = .GetColFromID("comcd_out") : lcls_PreOut.COMCD_OUT = .Text
                        .Col = .GetColFromID("fkocs") : lcls_PreOut.FKOCS = .Text
                        .Col = .GetColFromID("comordcd") : lcls_PreOut.COMORDCD = .Text

                        lcls_PreOut.COMCD = sComcd
                        lcls_PreOut.IOGBN = sIogbn
                        lcls_PreOut.OWNGBN = sOwngbn
                        lcls_PreOut.REGNO = sRegno
                        lcls_PreOut.ORDDATE = sOrdDt
                        lcls_PreOut.SPCCD = sSpccd

                        lcls_PreOut.ABO = sAbo
                        lcls_PreOut.RH = sRh

                        alBefInfo.Add(lcls_PreOut)

                    End If
                Next
            End With

            If iCnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "가출고 등록 할 항목이 없습니다.")
                Return
            End If

            bContinue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "가출고 등록 하시겠습니까?")

            If bContinue = False Then Return

            bOk = (New BefOut).fnExe_BefOut(alBefInfo, "E"c)

            If bOk = True Then
                If Me.lblBarPrinter.Text.Replace("사용안함", "").Trim <> "" Then
                    With (New LISAPP.APP_BT.DB_BloodPrint)
                        .PrintDo(Me.Name, alBefInfo, True, False, 1) ' 출고 스티커 출력
                    End With
                End If

                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "가출고 등록 되었습니다.")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "가출고 등록중 오류가 발생 하였습니다.")
            End If

            sb_SubSearchCB()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 가출고 취소
    Private Sub btnExeCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExeCancel.Click

        Try
            Dim li_Cnt As Integer = 0
            Dim ls_State As String
            Dim lal_Arg As New ArrayList
            Dim ls_ir As String
            Dim ls_filter As String
            Dim ls_comcd As String
            Dim ls_comnm As String
            Dim ls_eryn As String
            Dim ls_owngbn As String
            Dim ls_iogbn As String
            Dim lb_Continue As Boolean
            Dim lb_ok As Boolean
            Dim ls_regno As String
            Dim ls_orddate As String
            Dim ls_spccd As String
            Dim ls_chk As String
            Dim ls_comordcd As String = ""

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("ir") : ls_ir = .Text
                .Col = .GetColFromID("filter") : ls_filter = .Text
                .Col = .GetColFromID("comcd") : ls_comcd = .Text
                .Col = .GetColFromID("comnm") : ls_comnm = .Text
                .Col = .GetColFromID("eryn") : ls_eryn = .Text
                .Col = .GetColFromID("owngbn") : ls_owngbn = .Text
                .Col = .GetColFromID("iogbn") : ls_iogbn = .Text
                .Col = .GetColFromID("regno") : ls_regno = .Text
                .Col = .GetColFromID("order_date") : ls_orddate = .Text
                .Col = .GetColFromID("spccd") : ls_spccd = .Text
                '.Col = .GetColFromID("comordcd") : ls_comordcd = .Text

            End With

            With spdPreList
                If .MaxRows < 1 Then Return

                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : ls_chk = .Text

                    If ls_chk = "" Then ls_chk = "0"c

                    If ls_chk = "0"c Then Continue For
                    .Col = .GetColFromID("state") : ls_State = .Text

                    Dim lcls_PreOut As New STU_TnsJubsu

                    If ls_State = "3"c Then
                        li_Cnt += 1

                        .Col = .GetColFromID("bldno") : lcls_PreOut.BLDNO = .Text
                        .Col = .GetColFromID("tnsjubsuno") : lcls_PreOut.TNSJUBSUNO = .Text
                        .Col = .GetColFromID("comcd_out") : lcls_PreOut.COMCD_OUT = .Text
                        .Col = .GetColFromID("comordcd") : lcls_PreOut.COMORDCD = .Text
                        .Col = .GetColFromID("fkocs") : lcls_PreOut.FKOCS = .Text

                        lcls_PreOut.COMCD = ls_comcd
                        lcls_PreOut.IOGBN = ls_iogbn
                        lcls_PreOut.OWNGBN = ls_owngbn
                        lcls_PreOut.REGNO = ls_regno
                        lcls_PreOut.ORDDATE = ls_orddate
                        lcls_PreOut.SPCCD = ls_spccd

                        lal_Arg.Add(lcls_PreOut)

                    End If
                Next
            End With

            If li_Cnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "가출고 취소 할 항목이 없습니다.")
                Return
            End If

            lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "가출고 취소 하시겠습니까?")

            If lb_Continue = False Then Return

            lb_ok = (New BefOut).fnExe_BefOut(lal_Arg, "C"c)

            If lb_ok = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "가출고 취소 되었습니다.")
                sb_SubSearchCB()
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "가출고 취소중 오류가 발생 하였습니다.")
                sb_SubSearchCB()
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
       
    End Sub

    Private Sub txtBcOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcOrder.Click
        Me.txtBcOrder.SelectAll()
    End Sub

    Private Sub txtBcKeep_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcKeep.Click
        Me.txtBcKeep.SelectAll()
    End Sub

    Private Sub txtOrderNum_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtOrderNum.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim ls_Bcno As String
            Dim li_Txtlen As Integer
            Dim dt As DataTable
            Dim lal_BcnoChk As New ArrayList

            ls_Bcno = txtOrderNum.Text

            If ls_Bcno.Length() < 1 Then
                txtRegno.Text = ""
                txtPatNm.Text = ""
                Return
            End If


            li_Txtlen = ls_Bcno.Length()

            If li_Txtlen = 18 Then
                ls_Bcno = ls_Bcno.Replace("-"c, "")

            ElseIf li_Txtlen = 15 Then
                ls_Bcno = ls_Bcno.Trim()
            ElseIf li_Txtlen = 11 Then
                ls_Bcno = CGDA_BT.fn_GetBcno(ls_Bcno)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "잘못된 검체 번호 입니다.")
                txtOrderNum.Focus()
                txtOrderNum.SelectAll()
                Return
            End If

            ' 해당 검체 번호가 기존에 보관검체로 등록되었는지 여부 체크
            dt = CGDA_BT.fn_ChkKeepSpcNo(ls_Bcno.Trim)

            If dt.Rows.Count > 0 Then
                Dim li_Cnt As Integer

                li_Cnt = CInt(dt.Rows(0).Item("cnt").ToString)

                If li_Cnt > 0 Then
                    fn_PopMsg(Me, "I"c, "이미 보관검체로 저장된 검체번호 입니다. 보관검체 선택 하시기 바랍니다.")
                    Return
                End If

            End If

            ' 검체의 사용 가능 여부 체크 3일이 지난검체는 사용 할 수 없다.
            dt = CGDA_BT.fn_GetBcnoAbleChk(ls_Bcno.Trim)

            If dt.Rows.Count < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 검체로 조회된 내역이 없습니다.")
                txtOrderNum.Focus()
                txtOrderNum.SelectAll()
                Return
            End If

            lal_BcnoChk = fn_GetSelectItem(dt, 3)

            Dim ls_Regno As String
            Dim ls_Colldt As String
            Dim ls_Availyn As String

            ls_Regno = lal_BcnoChk(0).ToString
            ls_Colldt = lal_BcnoChk(1).ToString
            ls_Availyn = lal_BcnoChk(2).ToString

            If ls_Availyn = "false" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "채혈 후 3일이 지난검체는 사용 할 수 없습니다.")
                txtOrderNum.Focus()
                txtOrderNum.SelectAll()
                Return
            End If

            txtRegno.Text = ls_Regno
            btnSearch_Click(Nothing, Nothing)

            If spdOrderList.MaxRows < 1 Then Return

            Dim ls_Bcno_Order As String
            Dim ls_Bcno_Keep As String
            Dim ls_TnsNum As String
            Dim li_BcnoChk As Integer
            Dim ls_Today As String = (New LISAPP.APP_DB.ServerDateTime).GetDate("")
            Dim ldt As DataTable
            Dim lal_Rtn As New ArrayList
            Dim ls_Abo As String
            Dim ls_Rh As String
            Dim lb_ok As Boolean

            With spdOrderList
                If .MaxRows < 1 Then
                    Return
                ElseIf .MaxRows = 1 Then
                    .Row = 1
                    .Col = .GetColFromID("bcno_order") : ls_Bcno_Order = .Text
                    .Col = .GetColFromID("bcno_keep") : ls_Bcno_Keep = .Text
                    .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                    .Col = .GetColFromID("abo") : ls_Abo = .Text
                    .Col = .GetColFromID("rh") : ls_Rh = .Text

                    li_BcnoChk = ls_Bcno_Order.Length() + ls_Bcno_Keep.Length()

                    ' 조회된 자료가 한 건일 경우 의뢰검체 혹은 보관검체가 등록되지 않았다면 등록한다. 
                    If li_BcnoChk < 1 Then
                        ldt = CGDA_BT.fn_GetKeepNo(ls_Today.Trim)
                        'lal_Rtn = fn_GetSelectItem(ldt, 1)
                        'li_num = CInt(lal_Rtn(0).ToString)

                        '' 보관검체번호 생성
                        'ls_Keepnum = ls_Today + (li_num).ToString.PadLeft(3, "0"c)
                        ' 보관검체 번호 = 의뢰검체번호로

                        lb_ok = (New TnsReg).fn_UpdBcnoBlood(ls_TnsNum, ls_Bcno, ls_Bcno, ls_Regno, ls_Colldt, ls_Abo, ls_Rh)

                        If lb_ok = True Then
                            .Row = 1
                            .Col = .GetColFromID("bcno_order") : .Text = ls_Bcno
                            .Col = .GetColFromID("bcno_keep") : .Text = ls_Bcno
                            txtBcOrder.Text = ls_Bcno
                            txtBcOrder.ReadOnly = True
                            txtBcKeep.Text = ls_Bcno
                            txtBcKeep.ReadOnly = True
                        Else
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰, 보관검체 등록중 오류가 발생 하였습니다.")
                            txtOrderNum.Focus()
                            txtOrderNum.SelectAll()
                        End If
                    End If

                    sb_DisPlaySubData(1)
                    txtRegno.Text = ""
                ElseIf .MaxRows > 1 Then
                    sb_DisPlaySubData(1)
                    txtRegno.Text = ""

                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰검체 or 보관검체를 등록 할 수혈접수 목록을 선택하시기 바랍니다.")

                    .Row = 1
                    .Col = .GetColFromID("bcno_order") : ls_Bcno_Order = .Text
                    .Col = .GetColFromID("bcno_keep") : ls_Bcno_Keep = .Text

                    li_BcnoChk = ls_Bcno_Order.Length() + ls_Bcno_Keep.Length()

                    If li_BcnoChk < 1 Then
                        txtBcOrder.Focus()
                        txtBcOrder.SelectAll()
                    Else
                        txtBldno.Focus()
                    End If

                End If
            End With

            txtOrderNum.Text = ""

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try
    End Sub

    ' 보관, 의뢰 검체 제거
    Private Sub sb_DelBcnoBlood(ByVal rsGbn As String)
        Dim lb_Continue As Boolean
        Dim lb_ok As Boolean
        Dim ls_TnsNum As String

        With Me.spdOrderList
            .Row = .ActiveRow
            .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text


            If rsGbn = "ORDER" Then
                lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "의뢰 검체를 제거 하시겠습니까?")
            ElseIf rsGbn = "KEEP" Then
                lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "보관검체를 제거 하시겠습니까?")
            End If

            lb_ok = (New TnsReg).fn_DelBcnoBlood(ls_TnsNum, rsGbn)

            If lb_ok = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 되었습니다.")
                .Row = .ActiveRow

                If rsGbn = "ORDER" Then
                    .Col = .GetColFromID("bcno_order") : .Text = ""
                    Me.txtBcOrder.Text = ""
                    Me.txtBcOrder.ReadOnly = False
                    Me.txtBcOrder.Focus()
                ElseIf rsGbn = "KEEP" Then
                    .Col = .GetColFromID("bcno_keep") : .Text = ""
                    Me.txtBcKeep.Text = ""
                    Me.txtBcKeep.ReadOnly = False
                    Me.txtBcKeep.Focus()
                End If

            Else
                If rsGbn = "ORDER" Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰 검체 제거중 오류가 발생 하였습니다.")
                ElseIf rsGbn = "KEEP" Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관검체 제거중 오류가 발생 하였습니다.")
                End If

                Me.txtOrderNum.Focus()
            End If
        End With
    End Sub

    Private Sub btnBcnoDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBcnoDel.Click
        If Me.spdOrderList.MaxRows < 1 Then Return

        If Me.txtBcOrder.Text.Length < 1 Then Return

        Dim li_OCnt As Integer = spdOutList.MaxRows
        Dim li_PCnt As Integer = spdPreList.MaxRows

        If li_OCnt + li_PCnt > 0 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "혈액이 등록되지 않은 항목의 의뢰검체만 제거 할 수 있습니다.")
            Return
        End If

        sb_DelBcnoBlood("ORDER")
    End Sub

    Private Sub btnKeepDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKeepDel.Click
        If spdOrderList.MaxRows < 1 Then Return

        If txtBcKeep.Text.Length < 1 Then Return

        Dim li_OCnt As Integer = spdOutList.MaxRows
        Dim li_PCnt As Integer = spdPreList.MaxRows

        If li_OCnt + li_PCnt > 0 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "혈액이 등록되지 않은 항목의 보관검체만 제거 할 수 있습니다.")
            Return
        End If

        sb_DelBcnoBlood("KEEP")
    End Sub

    Private Sub txtBcOrder_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcOrder.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtBcOrder.Text.Length < 1 Then Return

        Try
            Dim ls_Bcno As String
            Dim lal_BcnoChk As New ArrayList
            Dim dt As DataTable

            Dim li_Keep As Integer
            Dim li_Order As Integer
            Dim ls_ORegno As String
            Dim ls_ORegnm As String

            With spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("regno") : ls_ORegno = .Text
                .Col = .GetColFromID("patnm") : ls_ORegnm = .Text
            End With

            li_Keep = txtBcKeep.Text.Length()
            li_Order = txtBcOrder.Text.Length()
            ls_Bcno = txtBcOrder.Text

            If li_Keep > 0 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관검체가 등록된 항목에 의뢰검체를 입력 할 수 없습니다.")
                Return
            End If

            If li_Order = 18 Then
                ls_Bcno = ls_Bcno.Replace("-"c, "")

            ElseIf li_Order = 15 Then
                ls_Bcno = ls_Bcno.Trim()
            ElseIf li_Order = 11 Then
                ls_Bcno = CGDA_BT.fn_GetBcno(ls_Bcno)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "잘못된 검체 번호 입니다.")
                txtBcOrder.Focus()
                txtBcOrder.SelectAll()
                Return
            End If

            ' 검체의 사용 가능 여부 체크 3일이 지난검체는 사용 할 수 없다.
            dt = CGDA_BT.fn_GetBcnoAbleChk(ls_Bcno)

            If dt.Rows.Count < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 검체로 조회된 내역이 없습니다.")
                txtBcOrder.Focus()
                txtBcOrder.SelectAll()
                Return
            End If

            lal_BcnoChk = fn_GetSelectItem(dt, 3)

            Dim ls_Regno As String
            Dim ls_Colldt As String
            Dim ls_Availyn As String

            ls_Regno = lal_BcnoChk(0).ToString
            ls_Colldt = lal_BcnoChk(1).ToString
            ls_Availyn = lal_BcnoChk(2).ToString

            If ls_ORegno <> ls_Regno Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ls_Bcno + " 는 " + ls_ORegnm + " 환자의 검체번호가 아닙니다.")
                Return
            End If

            If ls_Availyn = "false" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "채혈 후 3일이 지난검체는 사용 할 수 없습니다.")
                txtOrderNum.Focus()
                txtOrderNum.SelectAll()
                Return
            End If

            ' 해당 검체 번호가 기존에 보관검체로 등록되었는지 여부 체크
            dt = CGDA_BT.fn_ChkKeepSpcNo(ls_Bcno)

            If dt.Rows.Count > 0 Then
                Dim li_Cnt As Integer

                li_Cnt = CInt(dt.Rows(0).Item("cnt").ToString)

                If li_Cnt > 0 Then
                    fn_PopMsg(Me, "I"c, "이미 보관검체로 저장된 검체번호 입니다. 보관검체 선택 하시기 바랍니다.")
                    Return
                End If

            End If

            ' 검체의 사용 가능 여부 체크 3일이 지난검체는 사용 할 수 없다.
            dt = CGDA_BT.fn_GetBcnoAbleChk(ls_Bcno)

            If dt.Rows.Count < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 검체로 조회된 내역이 없습니다.")
                txtOrderNum.Focus()
                txtOrderNum.SelectAll()
                Return
            End If

            Dim lb_continue As Boolean

            lb_continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, ls_Bcno + "을(를) 의뢰검체로 입력 하시겠습니까?")

            If lb_continue <> True Then
                txtBcOrder.Text = ""
                txtBcOrder.Focus()
                txtBcOrder.SelectAll()
                Return
            End If

            Dim ls_TnsNum As String
            Dim lal_Rtn As New ArrayList
            Dim ls_Abo As String
            Dim ls_Rh As String
            Dim lb_ok As Boolean
            Dim ls_Today As String = (New LISAPP.APP_DB.ServerDateTime).GetDate("")

            With spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                .Col = .GetColFromID("abo") : ls_Abo = .Text
                .Col = .GetColFromID("rh") : ls_Rh = .Text

                lb_ok = (New TnsReg).fn_UpdBcnoBlood(ls_TnsNum, ls_Bcno, ls_Bcno, ls_Regno, ls_Colldt, ls_Abo, ls_Rh)

                If lb_ok = True Then
                    .Row = .ActiveRow
                    .Col = .GetColFromID("bcno_order") : .Text = ls_Bcno
                    .Col = .GetColFromID("bcno_keep") : .Text = ls_Bcno
                    txtBcOrder.Text = ls_Bcno
                    txtBcOrder.ReadOnly = True
                    txtBcKeep.Text = ls_Bcno
                    txtBcKeep.ReadOnly = True
                    'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰, 보관검체가 적용 되었습니다.")
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰, 보관검체 등록중 오류가 발생 하였습니다.")
                    txtOrderNum.Focus()
                    txtOrderNum.SelectAll()
                End If
            End With

            Me.txtBldno.Focus()
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
       
    End Sub

    Private Sub txtKeepNum_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeepNum.KeyDown, txtBcKeep.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtRegno.Text = "" Then Return

        Try
            Dim ls_Bcno As String
            Dim dt As DataTable
            Dim lal_BcnoChk As New ArrayList

            ls_Bcno = txtBcKeep.Text

            If ls_Bcno.Length() < 1 Then
                txtRegno.Text = ""
                txtPatNm.Text = ""
                Return
            End If

            If e.KeyCode = Keys.Enter Then
                Dim li_Order As Integer
                Dim ls_ORegno As String
                Dim ls_ORegnm As String

                With spdOrderList
                    .Row = .ActiveRow
                    .Col = .GetColFromID("regno") : ls_ORegno = .Text
                    .Col = .GetColFromID("patnm") : ls_ORegnm = .Text
                End With

                li_Order = txtBcKeep.Text.Length()
                ls_Bcno = txtBcKeep.Text

                If li_Order = 18 Then
                    ls_Bcno = ls_Bcno.Replace("-"c, "")

                ElseIf li_Order = 15 Then
                    ls_Bcno = ls_Bcno.Trim()
                ElseIf li_Order = 11 Then
                    ls_Bcno = CGDA_BT.fn_GetBcno(ls_Bcno)
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "잘못된 검체 번호 입니다.")
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                    Return
                End If

                ' 검체의 사용 가능 여부 체크 3일이 지난검체는 사용 할 수 없다.
                dt = CGDA_BT.fn_GetBcnoAbleChk(ls_Bcno.Trim)

                If dt.Rows.Count < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 검체로 조회된 내역이 없습니다.")
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                    Return
                End If

                lal_BcnoChk = fn_GetSelectItem(dt, 3)

                Dim ls_Regno_chk As String
                Dim ls_Colldt As String
                Dim ls_Availyn As String

                ls_Regno_chk = lal_BcnoChk(0).ToString.Trim
                ls_Colldt = lal_BcnoChk(1).ToString.Trim
                ls_Availyn = lal_BcnoChk(2).ToString.Trim

                If ls_ORegno <> ls_Regno_chk Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ls_Bcno + " 는 " + ls_ORegnm + " 환자의 검체번호가 아닙니다.")
                    Return
                End If

                If ls_Availyn = "false" Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "채혈 후 3일이 지난검체는 사용 할 수 없습니다.")
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                    Return
                End If

                ' 검체의 사용 가능 여부 체크 3일이 지난검체는 사용 할 수 없다.
                dt = CGDA_BT.fn_GetBcnoAbleChk(ls_Bcno)

                If dt.Rows.Count < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 검체로 조회된 내역이 없습니다.")
                    txtOrderNum.Focus()
                    txtOrderNum.SelectAll()
                    Return
                End If
            End If

            ' 보관검체 번호로 등록번호 정보 조회
            dt = CGDA_BT.fn_GetKeepInfo(ls_Bcno.Trim)

            If dt.Rows.Count < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 검체로 조회된 내역이 없습니다.")
                txtKeepNum.Focus()
                txtKeepNum.SelectAll()
                Return
            End If

            lal_BcnoChk = fn_GetSelectItem(dt, 1)

            Dim ls_Regno As String

            ls_Regno = lal_BcnoChk(0).ToString

            Me.txtRegno.Text = ls_Regno
            btnSearch_Click(Nothing, Nothing)

            Dim ls_Bcno_Order As String
            Dim ls_Bcno_Keep As String
            Dim ls_TnsNum As String
            Dim li_BcnoChk As Integer
            Dim lb_ok As Boolean

            With Me.spdOrderList
                If .MaxRows < 1 Then
                    Return
                ElseIf .MaxRows = 1 Then
                    .Row = 1
                    .Col = .GetColFromID("bcno_order") : ls_Bcno_Order = .Text
                    .Col = .GetColFromID("bcno_keep") : ls_Bcno_Keep = .Text
                    .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text

                    li_BcnoChk = ls_Bcno_Order.Length() + ls_Bcno_Keep.Length()

                    ' 조회된 자료가 한 건일 경우 의뢰검체 혹은 보관검체가 등록되지 않았다면 등록한다. 
                    If li_BcnoChk < 1 Then
                        lb_ok = (New TnsReg).fn_UpdKeepNo(ls_TnsNum, ls_Bcno, "KEEP")

                        If lb_ok = True Then
                            .Row = 1
                            .Col = .GetColFromID("bcno_keep") : .Text = ls_Bcno
                            txtBcKeep.Text = ls_Bcno
                            txtBcKeep.ReadOnly = True
                        Else
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰, 보관검체 등록중 오류가 발생 하였습니다.")
                            txtBcKeep.Focus()
                            txtBcKeep.SelectAll()
                        End If
                    End If

                    sb_DisPlaySubData(1)
                    txtRegno.Text = ""
                ElseIf .MaxRows > 1 Then
                    sb_DisPlaySubData(1)
                    txtRegno.Text = ""

                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰검체 or 보관검체를 등록 할 수혈접수 목록을 선택하시기 바랍니다.")

                    .Row = 1
                    .Col = .GetColFromID("bcno_order") : ls_Bcno_Order = .Text
                    .Col = .GetColFromID("bcno_keep") : ls_Bcno_Keep = .Text

                    li_BcnoChk = ls_Bcno_Order.Length() + ls_Bcno_Keep.Length()

                    If li_BcnoChk < 1 Then
                        Me.txtBcKeep.Focus()
                        Me.txtBcKeep.SelectAll()
                    Else
                        Me.txtBldno.Focus()
                    End If

                End If
            End With

            Me.txtKeepNum.Text = ""
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        
    End Sub

    Private Sub txtBcKeep_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcKeep.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim ls_Bcno As String
            Dim lal_BcnoChk As New ArrayList
            Dim dt As DataTable

            If spdOrderList.MaxRows < 1 Then Return

            If txtBcKeep.Text.Length < 1 Then Return
            Dim li_Keep As Integer
            Dim li_Order As Integer
            Dim ls_TnsNum As String
            Dim ls_ORegno As String
            Dim ls_ORegnm As String
            Dim lb_ok As Boolean

            With spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                .Col = .GetColFromID("regno") : ls_ORegno = .Text
                .Col = .GetColFromID("patnm") : ls_ORegnm = .Text


                li_Keep = txtBcKeep.Text.Length()
                li_Order = txtBcOrder.Text.Length()
                ls_Bcno = txtBcKeep.Text

                If li_Order > 0 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰검체가 등록된 항목에 보관검체를 입력 할 수 없습니다.")
                    txtBcKeep.Text = ""
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                    Return
                End If


                ' 보관검체 번호로 등록번호 정보 조회
                dt = CGDA_BT.fn_GetKeepInfo(ls_Bcno)

                If dt.Rows.Count < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 검체로 조회된 내역이 없습니다.")
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                    Return
                End If

                lal_BcnoChk = fn_GetSelectItem(dt, 1)

                Dim ls_Regno As String

                ls_Regno = lal_BcnoChk(0).ToString

                If ls_ORegno <> ls_Regno Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ls_Bcno + " 는 " + ls_ORegnm + " 환자의 검체번호가 아닙니다.")
                    Return
                End If

                Dim lb_continue As Boolean

                lb_continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, ls_Bcno + "을(를) 보관검체로 입력 하시겠습니까?")

                If lb_continue <> True Then
                    txtBcKeep.Text = ""
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                    Return
                End If

                lb_ok = (New TnsReg).fn_UpdKeepNo(ls_TnsNum, ls_Bcno, "KEEP")

                If lb_ok = True Then
                    .Row = .ActiveRow
                    .Col = .GetColFromID("bcno_keep") : .Text = ls_Bcno
                    txtBcKeep.Text = ls_Bcno
                    txtBcKeep.ReadOnly = True
                    'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 되었습니다.")
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관검체 등록중 오류가 발생 하였습니다.")
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                End If

            End With

            Me.txtBldno.Focus()

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 보관검체선택 버튼 클릭
    Private Sub btnKeep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeep.Click
        If spdOrderList.MaxRows < 1 Then Return

        Dim ls_TnsNum As String
        Dim ls_regno As String
        Dim ls_Patnm As String
        Dim ls_keep As String

        With spdOrderList
            .Row = .ActiveRow
            .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
            .Col = .GetColFromID("regno") : ls_regno = .Text
            .Col = .GetColFromID("patnm") : ls_Patnm = .Text
            .Col = .GetColFromID("bcno_keep") : ls_keep = .Text
        End With

        If ls_keep.Length() > 0 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관검체가 등록되있는 환자 입니다.")
            txtOrderNum.Focus()
            Return
        End If

        ' 보관검체선택 팝업 호출
        Dim objHelp As New CDHELP.FGCDHELP99
        Dim lal_Header As New ArrayList
        Dim lal_Arg As New ArrayList
        Dim li_RtnCnt As Integer = 3
        Dim lal_Rtn As New ArrayList
        Dim ls_keepno As String
        Dim lb_ok As Boolean

        Try
            lal_Header.Add("보관검체번호")
            lal_Header.Add("채혈일시")
            lal_Header.Add("사용가능일시")
            lal_Header.Add("등록번호")

            lal_Arg.Add(ls_regno)

            lal_Rtn = objHelp.fn_DisplayPop(Me, "보관검체선택 ", "fn_PopKeepBcno", lal_Arg, lal_Header, li_RtnCnt, ls_regno, "N"c, 3, "Y"c)

            If lal_Rtn.Count > 0 Then
                ls_keepno = lal_Rtn(0).ToString

                Dim lb_continue As Boolean

                lb_continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, ls_keepno + "을(를) 보관검체로 입력 하시겠습니까?")

                If lb_continue <> True Then
                    txtBcKeep.Text = ""
                    txtBcKeep.Focus()
                    txtBcKeep.SelectAll()
                    Return
                End If

                lb_ok = (New TnsReg).fn_UpdKeepNo(ls_TnsNum, ls_keepno, "KEEP")

                With spdOrderList
                    If lb_ok = True Then
                        .Row = .ActiveRow
                        .Col = .GetColFromID("bcno_keep") : .Text = ls_keepno
                        txtBcKeep.Text = ls_keepno
                        txtBcKeep.ReadOnly = True
                        'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "정상적으로 처리 되었습니다.")
                        txtBldno.Focus()
                    Else
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관검체 등록중 오류가 발생 하였습니다.")
                        txtBcKeep.Focus()
                        txtBcKeep.SelectAll()
                    End If
                End With

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    ' 혈액형 변경 등록
    Private Sub chkAbo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAbo.CheckedChanged


        Try
            Dim dt As DataTable
            Dim ls_Regno As String
            Dim ls_Abo As String
            Dim ls_Rh As String
            Dim ls_Comcd As String
            Dim ls_Spccd As String
            Dim ls_Gubun As String
            Dim ls_Change As String

            spdBldList.MaxRows = 0

            With spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("regno") : ls_Regno = .Text
                .Col = .GetColFromID("abo") : ls_Abo = .Text
                .Col = .GetColFromID("rh") : ls_Rh = .Text
                .Col = .GetColFromID("comcd") : ls_Comcd = .Text
                .Col = .GetColFromID("spccd") : ls_Spccd = .Text
            End With

            If chkQnt.Checked = True Then
                ls_Gubun = "1"c
            Else
                ls_Gubun = ""
            End If

            If chkAbo.Checked = True Then
                ls_Change = "1"
            Else
                ls_Change = ""
            End If

            If ls_Change = "1"c Then
                fn_PopMsg(Me, "I"c, "체크시 다른 혈액형을 크로스매칭 검사 할 수 있습니다.")
            End If

            ' 혈액은행 보유혈액 조회
            dt = CGDA_BT.fn_GetStoreBldList(ls_Abo, ls_Rh, ls_Comcd, ls_Spccd, ls_Gubun, ls_Change)
            sb_DisPlayBldList(dt)

            miChkCnt = 0
            txtBldno.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnMOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMOut.Click
        Dim frm As Windows.Forms.Form
        Dim ls_Regno As String

        If spdOrderList.MaxRows < 1 Then Return

        frm = Ctrl.CheckFormObject(Me, Me.btnMOut.Text)

        With spdOrderList
            .Row = .ActiveRow
            .Col = .GetColFromID("regno") : ls_Regno = .Text
        End With

        If frm Is Nothing Then frm = New LISB.FGB09(ls_Regno, dtpDate0.Value, dtpDate1.Value)

        frm.MdiParent = Me.MdiParent
        frm.WindowState = Windows.Forms.FormWindowState.Maximized
        frm.Text = Me.btnMOut.Text
        frm.Activate()
        frm.Show()

        With CType(frm, LISB.FGB09)
            .mbCalled = True
            .sb_RegnoCalled(ls_Regno, dtpDate0.Value, dtpDate1.Value)
        End With

        MdiTabControl.sbTabPageAdd(frm)
    End Sub

    Private Sub btnRePrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRePrint.Click
        If Me.spdPreList.MaxRows < 1 Then Return

        Dim alPrtData As New ArrayList

        With Me.spdPreList
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("tnsjubsuno") : Dim rsTnsNo As String = .Text.Trim.Replace("-", "")
                .Col = .GetColFromID("bldno") : Dim sBldNo As String = .Text.Trim.Replace("-", "")
                .Col = .GetColFromID("chk") : Dim sChk As String = .Text.Trim

                If sChk = "1" Then
                    Dim bldl As New STU_TnsJubsu

                    bldl.TNSJUBSUNO = rsTnsNo
                    bldl.BLDNO = sBldNo

                    alPrtData.Add(bldl)
                End If
            Next

        End With

        If Me.lblBarPrinter.Text.Trim.Replace("사용안함", "") <> "" And alPrtData.Count > 0 Then
            With (New LISAPP.APP_BT.DB_BloodPrint)
                .PrintDo(Me.Name, alPrtData, True, False, 1) ' 출고 스티커 출력
            End With
        End If
    End Sub

    Private Sub btnPrint_Set_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Set.Click
        Dim sFn As String = "Handles btnPrintSet.Click"

        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC(Me.Name)

        Try
            objFrm.ShowDialog()
            Me.lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub txtBldNoBef_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles txtBldNoBef.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If Me.spdOrderList.MaxRows < 1 Then Return

        Try


            With Me.spdPreList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("bldno") : Dim sBldNo As String = .Text.Replace("-", "")

                    If Me.txtBldNoBef.Text = sBldNo Then
                        .Col = .GetColFromID("chk") : .Text = "1"
                        Exit For
                    End If
                Next
            End With

            Me.txtBldNoBef.Text = ""
            Me.txtBldNoBef.Focus()
            Me.txtBldNoBef.SelectAll()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub

    Private Sub btnBldchg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBldchg.Click

        Try
            Dim sTnsno As String = ""
            Dim frm As New Form

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("tnsjubsuno") : sTnsno = .Text.Replace("-", "")
            End With

            frm = New LISB.FGB07_S02(sTnsno)
            frm.ShowDialog()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub
End Class