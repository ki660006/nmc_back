'>>> 혈액출고

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT
Imports WEBSERVER.CGWEB_B

Public Class FGB09
    Private mobjDAF As New LISAPP.APP_F_COMCD
    Private miChkCnt As Integer = 0
    Private miOutChkCnt As Integer = 0
    Public mbCalled As Boolean = False

    Private Sub FGB09_NEW_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB09_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtSBldno.Focus()
    End Sub

    Private Sub FGB09_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F6
                btnSearch_Click(Nothing, Nothing)
            Case Keys.F7
                btnExecute_Click(Nothing, Nothing)
            Case Keys.F8
                btnExeCancel_Click(Nothing, Nothing)
            Case Keys.Escape
                txtRegno.Focus()
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub FGB09_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        If PRG_CONST.BLD_LABEL_SUM_YN = "N" Then
            Me.chkSummery.Checked = False
            Me.chkSummery.Visible = False
            Me.chkPrt.Left = Me.chkSummery.Left
        End If

        Me.txtRegno.MaxLength = PRG_CONST.Len_RegNo()

        ' 화면 오픈시 초기화
        Me.spdOrderList.MaxRows = 0
        Me.spdPastTns.MaxRows = 0
        Me.spdTnsOrd.MaxRows = 0
        Me.spdOutList.MaxRows = 0
        Me.spdStOutList.MaxRows = 0

        If mbCalled = False Then
            Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).AddDays(-1)
            Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
        End If

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdOrderList)
        DS_SpreadDesige.sbInti(spdPastTns)
        DS_SpreadDesige.sbInti(spdTnsOrd)
        DS_SpreadDesige.sbInti(spdOutList)
        DS_SpreadDesige.sbInti(spdStOutList)

        Me.txtBldno.Text = ""

        sbDisplay_com()

        Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

    End Sub

    Public Sub sbDisplay_com(Optional ByVal rsUsDt As String = "", Optional ByVal rsUeDt As String = "")

        Try

            Dim dt As DataTable = mobjDAF.GetComCdInfo("")

            Me.cboComCd.Items.Clear()
            Me.cboComCd.Items.Add("[ALL] 전체")
            If dt.Rows.Count > 0 Then
                With Me.cboComCd
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add(dt.Rows(i).Item("comnmd"))
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
        Dim alHeader As New ArrayList
        Dim alArg As New ArrayList
        Dim iRtnCnt As Integer = 2
        Dim alRtn As New ArrayList

        Try
            alHeader.Add("환자번호")
            alHeader.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LIS)
            alArg.Add(" "c)
            alArg.Add(" "c)

            alRtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", alArg, alHeader, iRtnCnt, "")

            If alRtn.Count > 0 Then
                Me.txtRegno.Text = alRtn(0).ToString
                Me.txtPatNm.Text = alRtn(1).ToString

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
        If Me.txtRegno.Text = "" Then
            Me.txtPatNm.Text = ""
            Return
        End If

        Try
            Dim objHelp As New CDHELP.FGCDHELP99
            Dim sRegno As String = Me.txtRegno.Text
            Dim sOrdDt As String = ""
            Dim sTnsnum As String = ""
            Dim alHeader As New ArrayList
            Dim alArg As New ArrayList
            Dim iRtnCnt As Integer = 2
            Dim alRtn As New ArrayList

            If IsNumeric(sRegno) Then
                If sRegno.Length() < PRG_CONST.Len_RegNo Then
                    sRegno = sRegno.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                End If
            Else
                If sRegno.Length() < PRG_CONST.Len_RegNo Then
                    sRegno = sRegno.Substring(0, 1) + sRegno.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If
            End If

            Me.txtRegno.Text = sRegno

            alHeader.Add("환자번호")
            alHeader.Add("환자명")

            ' 환자 검색테이블이 둘이라 두개 add (OCS, LISv
            alArg.Add(sRegno)
            alArg.Add(sRegno)

            alRtn = objHelp.fn_DisplayPop(Me, "환자조회 ", "fn_PopGetPatList", alArg, alHeader, iRtnCnt, sRegno)

            If alRtn.Count > 0 Then
                Me.txtRegno.Text = alRtn(0).ToString
                Me.txtPatNm.Text = alRtn(1).ToString
            End If

            btnSearch_Click(Nothing, Nothing)

            Me.txtRegno.Text = "" : Me.txtPatNm.Text = "" '20211125 jhs 배포 하고 변경 예정

            If Me.spdOrderList.MaxRows < 1 Then Return

            With Me.spdOrderList
                .Row = 1
                .Col = .GetColFromID("order_date") : sOrdDt = .Text
                .Col = .GetColFromID("tnsjubsuno") : sTnsnum = .Text.Trim.Replace("-", "")

            End With

            ' 환자정보 디스플레이
            Me.AxTnsPatinfo1.sb_setPatinfo(sRegno, sOrdDt, sTnsnum)


        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        Dim ls_Comcd As String
        Dim ls_Gbn As String = ""
        Dim ls_Bldno As String

        Me.spdOrderList.MaxRows = 0
        Me.spdPastTns.MaxRows = 0
        Me.spdTnsOrd.MaxRows = 0
        Me.spdOutList.MaxRows = 0
        Me.spdStOutList.MaxRows = 0
        Me.AxTnsPatinfo1.sb_ClearLbl()

        ls_Comcd = Ctrl.Get_Code(cboComCd)
        ls_Bldno = txtSBldno.Text

        miOutChkCnt = 0

        If rdoAll.Checked = True Then
            ls_Gbn = "0"c
        ElseIf rdoUnCom.Checked = True Then
            ls_Gbn = "1"c
        ElseIf rdoComplete.Checked = True Then
            ls_Gbn = "2"c
        End If

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            ' 조회
            dt = CGDA_BT.fn_OutOrderList(Format(dtpDate0.Value, "yyyyMMdd"), Format(dtpDate1.Value, "yyyyMMdd"), txtRegno.Text, ls_Comcd, ls_Bldno, ls_Gbn)

            sb_DisplayDataList(dt)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sb_DisplayDataList(ByVal rDt As DataTable)

        Try
            With Me.spdOrderList
                .MaxRows = 0
                If rDt.Rows.Count < 1 Then Return

                .ReDraw = False

                For i As Integer = 0 To rDt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("vstate") : .Text = rDt.Rows(i).Item("vstate").ToString
                    .Col = .GetColFromID("tnsjubsuno") : .Text = rDt.Rows(i).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("jubsudt") : .Text = rDt.Rows(i).Item("jubsudt").ToString
                    .Col = .GetColFromID("comnmd") : .Text = rDt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("regno") : .Text = rDt.Rows(i).Item("regno").ToString
                    .Col = .GetColFromID("patnm") : .Text = rDt.Rows(i).Item("patnm").ToString
                    .Col = .GetColFromID("reqqnt") : .Text = rDt.Rows(i).Item("reqqnt").ToString
                    .Col = .GetColFromID("befoutqnt") : .Text = rDt.Rows(i).Item("befoutqnt").ToString
                    .Col = .GetColFromID("outqnt") : .Text = rDt.Rows(i).Item("outqnt").ToString
                    .Col = .GetColFromID("order_date") : .Text = rDt.Rows(i).Item("order_date").ToString
                    .Col = .GetColFromID("abo") : .Text = rDt.Rows(i).Item("abo").ToString
                    .Col = .GetColFromID("rh") : .Text = rDt.Rows(i).Item("rh").ToString
                    .Col = .GetColFromID("comcd") : .Text = rDt.Rows(i).Item("comcd").ToString
                    .Col = .GetColFromID("spccd") : .Text = rDt.Rows(i).Item("spccd").ToString
                    .Col = .GetColFromID("iogbn") : .Text = rDt.Rows(i).Item("iogbn").ToString
                    .Col = .GetColFromID("owngbn") : .Text = rDt.Rows(i).Item("owngbn").ToString
                    .Col = .GetColFromID("filter") : .Text = rDt.Rows(i).Item("filter").ToString
                    .Col = .GetColFromID("tnsgbn") : .Text = rDt.Rows(i).Item("tnsgbn").ToString
                Next

                sb_SetStBarSearchCnt(rDt.Rows.Count)

                Me.txtSBldno.Focus()
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdOrderList.ReDraw = True

        End Try
    End Sub

    ' 화면 정리
    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdOrderList.MaxRows = 0
        Me.spdPastTns.MaxRows = 0
        Me.spdTnsOrd.MaxRows = 0
        Me.spdOutList.MaxRows = 0
        Me.spdStOutList.MaxRows = 0

        Me.txtSBldno.Text = ""
        Me.txtBldno.Text = ""

        Me.AxTnsPatinfo1.sb_ClearLbl()
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

    Private Sub spdOrderList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdOrderList.ClickEvent
        If spdOrderList.MaxRows < 1 Then Return

        miChkCnt = 0
        miOutChkCnt = 0

        Me.chkCMCO.Checked = False

        sb_DisPlaySubData(e.row)
    End Sub

    Private Sub sb_DisPlaySubData(ByVal riRow As Integer)

        Try
            Dim dt As DataTable
            Dim sRegno As String = ""
            Dim sDate As String = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")
            Dim sTnsnum As String = ""
            Dim sOrdDt As String = ""
            Dim sSpccd As String = ""
            Dim sFilter As String = ""
            Dim sTnsGbn As String = ""

            If spdOrderList.MaxRows < 1 Then Return

            Me.spdPastTns.MaxRows = 0
            Me.spdTnsOrd.MaxRows = 0
            Me.spdOutList.MaxRows = 0
            Me.spdStOutList.MaxRows = 0

            miOutChkCnt = 0

            With spdOrderList
                .Row = riRow
                .Col = .GetColFromID("regno") : sRegno = .Text.Trim
                .Col = .GetColFromID("tnsjubsuno") : sTnsnum = .Text.Trim.Replace("-", "")
                .Col = .GetColFromID("order_date") : sOrdDt = .Text.Replace("-"c, "").Trim
                .Col = .GetColFromID("spccd") : sSpccd = .Text.Trim
                .Col = .GetColFromID("filter") : sFilter = .Text.Trim
                .Col = .GetColFromID("tnsgbn") : sTnsGbn = .Text.Trim

            End With

            Me.chkCMCO.Checked = False
            Me.txtRecid.Text = ""
            Me.txtRecnm.Text = ""

            ' 환자 정보 조회
            AxTnsPatinfo1.sb_setPatinfo(sRegno, sOrdDt, sTnsnum)

            ' 과거수혈내역조회
            dt = CGDA_BT.fn_GetPastTnsList(sRegno, sDate)
            sb_DisplayPastList(dt)

            ' 수혈의뢰내역 조회
            dt = CGDA_BT.fn_GetTnsCnt(sTnsnum)
            sb_DisPlayTnsCnt(dt)

            ' 출고 목록 조회
            dt = CGDA_BT.fn_GetOutList(sTnsnum, sSpccd, "") ' sFilter)
            sb_DisPlayOutList(dt)

            ' 가출고 목록 조회
            dt = CGDA_BT.fn_StOrderList(sTnsnum)
            sb_DisplayStOutList(dt)

            If sTnsGbn = "3" And spdStOutList.MaxRows = 0 Then Me.chkCMCO.Checked = True 'tnsgbn = 3 은 응급 교차미필 체크 

            Me.txtBldno.Focus()

        Catch ex As Exception
            fn_PopMsg(Me, "E", ex.Message)
        End Try

    End Sub

    ' 가출고리스트 조회
    Private Sub sb_DisplayStOutList(ByVal r_dt As DataTable)
        If r_dt.Rows.Count < 1 Then Return

        Dim ls_Abo As String

        Try
            With Me.spdStOutList
                .MaxRows = 0
                .ReDraw = False
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(ix).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(ix).Item("vbldno").ToString
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString
                    .Col = .GetColFromID("type") : .Text = r_dt.Rows(ix).Item("type").ToString

                    ls_Abo = r_dt.Rows(ix).Item("type").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(ls_Abo)

                    .Col = .GetColFromID("befoutdt") : .Text = r_dt.Rows(ix).Item("befoutdt").ToString
                    .Col = .GetColFromID("testid") : .Text = r_dt.Rows(ix).Item("testid").ToString
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(ix).Item("indt").ToString
                    .Col = .GetColFromID("dondt") : .Text = r_dt.Rows(ix).Item("dondt").ToString
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(ix).Item("availdt").ToString
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("comcd").ToString
                    .Col = .GetColFromID("owngbn") : .Text = r_dt.Rows(ix).Item("owngbn").ToString
                    .Col = .GetColFromID("iogbn") : .Text = r_dt.Rows(ix).Item("iogbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(ix).Item("fkocs").ToString
                    .Col = .GetColFromID("bldno") : .Text = r_dt.Rows(ix).Item("bldno").ToString.Trim
                    .Col = .GetColFromID("filter") : .Text = r_dt.Rows(ix).Item("filter").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = r_dt.Rows(ix).Item("comcd_out").ToString
                    .Col = .GetColFromID("sortkey") : .Text = r_dt.Rows(ix).Item("sortkey").ToString
                    .Col = .GetColFromID("cmt") : .Text = r_dt.Rows(ix).Item("cmt").ToString
                    .Col = .GetColFromID("comordcd") : .Text = r_dt.Rows(ix).Item("comordcd").ToString
                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdPastTns.ReDraw = True
        End Try
    End Sub

    ' 과거수혈내역조회
    Private Sub sb_DisplayPastList(ByVal r_dt As DataTable)

        Try
            With Me.spdPastTns
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
                    .Row = .MaxRows

                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(ix).Item("comnm").ToString
                    .Col = .GetColFromID("filter") : .Text = r_dt.Rows(ix).Item("filter").ToString
                    .Col = .GetColFromID("ir") : .Text = r_dt.Rows(ix).Item("ir").ToString
                    .Col = .GetColFromID("reqqnt") : .Text = r_dt.Rows(ix).Item("reqqnt").ToString
                    .Col = .GetColFromID("befoutqnt") : .Text = r_dt.Rows(ix).Item("befoutqnt").ToString
                    .Col = .GetColFromID("outqnt") : .Text = r_dt.Rows(ix).Item("outqnt").ToString
                    .Col = .GetColFromID("rtnqnt") : .Text = r_dt.Rows(ix).Item("rtnqnt").ToString
                    .Col = .GetColFromID("abnqnt") : .Text = r_dt.Rows(ix).Item("abnqnt").ToString
                    .Col = .GetColFromID("cancelqnt") : .Text = r_dt.Rows(ix).Item("cancelqnt").ToString
                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdTnsOrd.ReDraw = True
        End Try
    End Sub

    ' 출고 목록 작성
    Private Sub sb_DisPlayOutList(ByVal r_dt As DataTable)

        Dim ls_state As String
        Dim lc_Color As Color = Color.Black
        Dim ls_Abo As String
        Dim ls_KeepGbn As String

        Try
            With Me.spdOutList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For i As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    ls_state = r_dt.Rows(i).Item("state").ToString
                    If ls_state = "5"c Then
                        lc_Color = Color.Blue
                    ElseIf ls_state = "6"c Then
                        lc_Color = Color.Red
                    Else
                        lc_Color = Color.Black
                    End If

                    If r_dt.Rows(i).Item("state").ToString <> "4"c Then
                        .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    End If

                    .Col = .GetColFromID("vkeepgbn") : .Text = r_dt.Rows(i).Item("vkeepgbn").ToString
                    .ForeColor = lc_Color
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(i).Item("vbldno").ToString
                    .ForeColor = lc_Color
                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(i).Item("comnm").ToString
                    .ForeColor = lc_Color
                    .Col = .GetColFromID("type") : .Text = r_dt.Rows(i).Item("type").ToString

                    ls_Abo = r_dt.Rows(i).Item("type").ToString.Replace("+"c, "").Replace("-"c, "")
                    .ForeColor = fnGet_BloodColor(ls_Abo)

                    .Col = .GetColFromID("rst1") : .Text = r_dt.Rows(i).Item("rst1").ToString
                    .Col = .GetColFromID("rst2") : .Text = r_dt.Rows(i).Item("rst2").ToString
                    .Col = .GetColFromID("rst3") : .Text = r_dt.Rows(i).Item("rst3").ToString
                    .Col = .GetColFromID("rst4") : .Text = r_dt.Rows(i).Item("rst4").ToString
                    .Col = .GetColFromID("cmrmk") : .Text = r_dt.Rows(i).Item("cmrmk").ToString
                    .Col = .GetColFromID("befoutdt") : .Text = r_dt.Rows(i).Item("befoutdt").ToString
                    .Col = .GetColFromID("outdt") : .Text = r_dt.Rows(i).Item("outdt").ToString
                    .Col = .GetColFromID("inspector") : .Text = r_dt.Rows(i).Item("inspector").ToString
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("dondt") : .Text = r_dt.Rows(i).Item("dondt").ToString
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(i).Item("availdt").ToString
                    .Col = .GetColFromID("outid") : .Text = r_dt.Rows(i).Item("outid").ToString
                    .Col = .GetColFromID("recnm") : .Text = r_dt.Rows(i).Item("recnm").ToString
                    .Col = .GetColFromID("bldno") : .Text = r_dt.Rows(i).Item("bldno").ToString
                    .Col = .GetColFromID("keepgbn") : .Text = r_dt.Rows(i).Item("keepgbn").ToString

                    ls_KeepGbn = r_dt.Rows(i).Item("keepgbn").ToString

                    .Col = .GetColFromID("comnmp") : .Text = r_dt.Rows(i).Item("comnmp").ToString
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(i).Item("comcd").ToString
                    .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(i).Item("fkocs").ToString
                    .Col = .GetColFromID("elapsdm") : .Text = r_dt.Rows(i).Item("elapsdm").ToString
                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(i).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("cmt") : .Text = r_dt.Rows(i).Item("cmt").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = r_dt.Rows(i).Item("comcd_out").ToString
                    .Col = .GetColFromID("comordcd") : .Text = r_dt.Rows(i).Item("comordcd").ToString

                    If ls_KeepGbn = "1"c Then
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = i + 1 : .Row2 = i + 1
                        .BlockMode = True

                        .BackColor = Color.CadetBlue

                        .BlockMode = False
                    ElseIf ls_KeepGbn = "2"c Then
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = i + 1 : .Row2 = i + 1
                        .BlockMode = True

                        .BackColor = Color.LawnGreen

                        .BlockMode = False
                    End If

                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdOutList.ReDraw = True
        End Try
    End Sub

    Private Sub txtSBldno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSBldno.Click
        Me.txtSBldno.SelectAll()
    End Sub

    Private Sub txtSBldno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSBldno.KeyDown
        Dim ls_Bldno As String
        Dim li_FRow As Integer

        If e.KeyCode = Keys.Enter Then

            '<<JJH 혈액번호로 조회시 등록번호 입력필수 추가 (사용자요청)
            'If Me.txtRegno.Text = "" Then
            '    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "등록번호를 먼저 입력하시기 바랍니다.")
            '    Me.txtRegno.Focus()
            '    Return
            'End If


            '20210121 jhs 등록번호 조회 하지 않았을 시 조회를 한번이라도 하고 진행하도록 수정 (사용자 요청)
            If Me.AxTnsPatinfo1.Regno() = "" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "환자 조회를 먼저 해주시기 바랍니다.")
                Me.txtRegno.Focus()
                Return
            End If
            '----------------------------------


            ls_Bldno = txtSBldno.Text

            If ls_Bldno.Length() = 10 Then
                ls_Bldno = ls_Bldno
            ElseIf ls_Bldno.Length() = 12 Then
                ls_Bldno = ls_Bldno.Replace("-"c, "")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "혈액번호를 정확히 입력하시기 바랍니다.")
                txtSBldno.Focus()
                txtSBldno.SelectAll()
                Return
            End If

            Me.txtSBldno.Text = ls_Bldno

            '// JJH 환자의 가출고혈액 체크
            If Me.AxTnsPatinfo1.Regno() <> "" Then
                If CGDA_BT.Bld_Bfout_Chk(ls_Bldno, Me.AxTnsPatinfo1.Regno()) <> "Y" Then
                    'If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "다른 환자의 혈액입니다 그래도 진행하시겠습니까?") = False Then
                    '//JJH 2020-12-21 아예 진행이 불가능하도록 수정 (사용자 요청)
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "다른 환자의 혈액입니다.")
                    Me.txtSBldno.Focus()
                    Me.txtSBldno.SelectAll()
                    Return
                    'End If
                End If
            End If

            btnSearch_Click(Nothing, Nothing)

            If spdOrderList.MaxRows < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회기간중 등록된 혈액이 없습니다.")
                Me.txtSBldno.Focus()
                Return
            Else
                sb_DisPlaySubData(1)

                With spdStOutList
                    If .MaxRows > 0 Then
                        li_FRow = Fn.SpdColSearch(spdStOutList, ls_Bldno, .GetColFromID("bldno"))

                        If li_FRow > 0 Then
                            .Row = li_FRow
                            .Col = .GetColFromID("chk") : .Text = "1"c
                            miOutChkCnt += 1
                        End If
                    End If
                End With

                Me.txtSBldno.Text = ""
                Me.txtBldno.Focus()
            End If
        End If
    End Sub

    Private Sub txtBldno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBldno.Click
        Me.txtBldno.SelectAll()
    End Sub
    '20211021 jhs 혈액불출요청서 혈액번호 확인
    Public Function chkTxtBlood() As Boolean

        Try
            Dim msg As String = ""

            If Me.txtRegno.Text = "" Then
                msg = "등록번호를 입력해주세요"
                fn_PopMsg(Me, "E"c, msg)
                Me.txtRegno.Focus()
                Me.txtRegno.SelectAll()
                Return False
            ElseIf Me.txtRegno.Text <> Me.AxTnsPatinfo1.Regno Then
                Me.txtRegno.Text = Me.txtRegno.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                msg = "입력한 등록번호와 조회한 등록번호가 상이합니다." + vbCrLf + "입력 등록번호 : " + Me.txtRegno.Text + vbCrLf + "조회한 등록번호: " + Me.AxTnsPatinfo1.Regno
                fn_PopMsg(Me, "E"c, msg)
                Me.txtRequestBlood.Focus()
                Me.txtRequestBlood.SelectAll()
                Return False
            ElseIf Me.txtRequestBlood.Text = "" Then
                Me.txtRegno.Text = Me.txtRegno.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                msg = "불출요청서 혈액번호를 작성해 주세요."
                fn_PopMsg(Me, "E"c, msg)
                Me.txtRequestBlood.Focus()
                Me.txtRequestBlood.SelectAll()
                Return False
            ElseIf Me.txtBldno.Text = "" Then
                Me.txtRegno.Text = Me.txtRegno.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                msg = "혈액번호를 입력해주세요."
                Me.txtBldno.Focus()
                Me.txtBldno.SelectAll()
                fn_PopMsg(Me, "E"c, msg)
                Return False
            ElseIf Me.txtRequestBlood.Text <> Me.txtBldno.Text Then
                Me.txtRegno.Text = Me.txtRegno.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                msg = "불출 요청서 혈액번호와 출고혈액번호가 상이합니다." + vbCrLf + "불출요청서 혈액번호 : " + Me.txtRequestBlood.Text + vbCrLf + "출고 혈액번호 : " + Me.txtBldno.Text
                fn_PopMsg(Me, "E"c, msg)
                Me.txtBldno.Focus()
                Me.txtBldno.SelectAll()
                Return False
            End If

            Return True

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Function
    '-----------------------------------------------

    Private Sub txtBldno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldno.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If Me.spdOrderList.MaxRows < 1 Then Return
        '20211021 jhs 혈액불출요청서 혈액번호 확인
        'If chkTxtBlood() = False Then Return
        '-----------------------------------------------

        Try
            Dim sBldno As String = Me.txtBldno.Text.Trim.Replace("-", "")
            Dim iFRow As Integer = 0
            Dim sChk As String = ""
            Dim iCanQnt As Integer = 0
            Dim iMaxRow As Integer = Me.spdStOutList.MaxRows

            With Me.spdTnsOrd
                .Row = 1
                .Col = .GetColFromID("cancelqnt") : iCanQnt = CInt(.Text)
            End With

            If sBldno.Length() < 10 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "올바른 혈액번호가 아닙니다.")
                Me.txtBldno.Focus()
                Me.txtBldno.SelectAll()
            Else
                iFRow = Fn.SpdColSearch(Me.spdStOutList, sBldno, spdStOutList.GetColFromID("bldno"))
                ' 혈액번호를 체크 및 체크된 혈액을 상단으로 정렬(체크된 순서대로정렬)
                If iFRow <> 0 Then
                    With Me.spdStOutList
                        .Row = iFRow
                        .Col = .GetColFromID("chk") : sChk = .Text

                        If sChk = "" Then sChk = "0"c

                        If sChk = "0" Then

                            If Me.chkCMCO.Checked = True Then
                                Dim iReqQnt As Integer = 0
                                Dim ioutQnt As Integer = spdOutList.MaxRows

                                With spdOrderList
                                    .Row = .ActiveRow
                                    .Col = .GetColFromID("reqqnt") : iReqQnt = CInt(.Text)
                                End With

                                If iReqQnt - iCanQnt <= miChkCnt + ioutQnt Then
                                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰수량보다 많은 혈액을 선택 할 수 없습니다.")
                                    Me.txtBldno.Focus()
                                    Me.txtBldno.SelectAll()
                                    Return
                                End If
                            End If

                            miChkCnt += 1
                            miOutChkCnt += 1

                            .Text = "1"c
                            .Col = .GetColFromID("sortkey") : .Text = miChkCnt.ToString
                        Else
                            miChkCnt -= 1
                            miOutChkCnt -= 1

                            .Text = "0"c
                            .Col = .GetColFromID("sortkey") : .Text = "9999999"
                        End If

                        ' 다중 Sort를 위한 설정
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = 1 : .Row2 = .MaxRows
                        .BlockMode = True
                        .SortBy = FPSpreadADO.SortByConstants.SortByRow
                        .set_SortKey(1, .GetColFromID("sortkey"))
                        .set_SortKey(2, .GetColFromID("bldno"))
                        .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                        .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                        .Action = FPSpreadADO.ActionConstants.ActionSort
                        .BlockMode = False

                        Me.txtBldno.Text = ""
                        '20211021 jhs 혈액불출요청서 혈액번호 초기화
                        Me.txtRequestBlood.Text = ""
                        '------------------------------------
                    End With

                    If iMaxRow = miOutChkCnt Then
                        Me.txtRecid.Focus()
                    Else
                        Me.txtBldno.Focus()
                    End If
                ElseIf iFRow = 0 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "일치하는 혈액번호를 찾을 수 없습니다.")
                    Me.txtBldno.Focus()
                    Me.txtBldno.SelectAll()
                End If

            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub chkCMCO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCMCO.CheckedChanged
        If spdOrderList.MaxRows < 1 Then Return

        miChkCnt = 0
        miOutChkCnt = 0

        If chkCMCO.Checked = True Then
            ' 교차미필 체크시 혈액 조회
            Dim ls_TnsGbn As String
            Dim ls_comcd As String
            Dim ls_spccd As String
            Dim ls_abo As String
            Dim ls_rh As String
            Dim ls_Filter As String
            Dim ls_TnsNum As String = ""

            With spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                .Col = .GetColFromID("tnsgbn") : ls_TnsGbn = .Text
                .Col = .GetColFromID("comcd") : ls_comcd = .Text
                .Col = .GetColFromID("spccd") : ls_spccd = .Text
                .Col = .GetColFromID("abo") : ls_abo = .Text
                .Col = .GetColFromID("rh") : ls_rh = .Text
                .Col = .GetColFromID("filter") : ls_Filter = .Text
            End With

            'If ls_Filter = "1"c Then
            '    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "필터 처방은 교차미필 출고 할 수 없습니다.")
            '    chkCMCO.Checked = False
            '    Return
            'End If

            If ls_TnsGbn <> "3"c Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "응급이 아닌 수혈처방은 교차미필 체크를 할 수 없습니다.")
                chkCMCO.Checked = False
                Return
            End If

            If spdStOutList.MaxRows > 0 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고대기 혈액이 있을경우 교차미필 체크를 할 수 없습니다.")
                chkCMCO.Checked = False
                Return
            End If

            btnBldchg.Enabled = True

            ' 혈액은행 보유혈액 조회

            Dim dt As DataTable = CGDA_BT.fn_GetStoreBldList2(ls_TnsNum, ls_abo, ls_rh, chkCMCO.Checked)
            sb_DisPlayBldList(dt)

        Else
            Me.spdStOutList.MaxRows = 0
        End If

        Me.txtBldno.Focus()
        Me.txtBldno.SelectAll()
    End Sub

    ' 보유혈액 조회
    Private Sub sb_DisPlayBldList(ByVal r_dt As DataTable)

        Try
            With Me.spdStOutList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For i As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("bldno") : .Text = r_dt.Rows(i).Item("bldno").ToString
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(i).Item("vbldno").ToString
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(i).Item("comcd").ToString
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(i).Item("comnmd").ToString
                    .Col = .GetColFromID("type") : .Text = r_dt.Rows(i).Item("aborh").ToString
                    .Col = .GetColFromID("dondt") : .Text = r_dt.Rows(i).Item("dondt").ToString
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(i).Item("indt").ToString
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(i).Item("availdt").ToString
                    .Col = .GetColFromID("sortkey") : .Text = r_dt.Rows(i).Item("sortkey").ToString
                    .Col = .GetColFromID("comordcd") : .Text = r_dt.Rows(i).Item("comordcd").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = r_dt.Rows(i).Item("comcd_out").ToString
                    .Col = .GetColFromID("cmt") : .Text = r_dt.Rows(i).Item("cmt").ToString
                    '20210719 jhs 응급수혈 구분 추가 
                    .Col = .GetColFromID("tnsGbn") : .Text = r_dt.Rows(i).Item("tnsgbn").ToString
                    '------------------------------------------
                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdStOutList.ReDraw = True
        End Try
    End Sub

    Private Sub txtRecid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRecid.Click
        Me.txtRecid.SelectAll()
    End Sub

    Private Sub txtRecid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRecid.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If Me.spdOrderList.MaxRows < 1 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회 후 수령자를 입력 하시기 바랍니다.")
            Me.txtRecid.Text = ""
            Return
        End If

        Try
            Dim objHelp As New CDHELP.FGCDHELP99
            Dim ls_Id As String
            Dim la_getValue As New ArrayList
            Dim lal_Header As New ArrayList
            Dim lal_Arg As New ArrayList
            Dim li_RtnCnt As Integer = 2
            Dim lal_Rtn As New ArrayList

            ' 등록번호 입력시 이벤트
            ls_Id = Me.txtRecid.Text

            If ls_Id.Length() < 1 Then
                txtRecnm.Text = ""
                Return
            End If


            lal_Header.Add("수령자")
            lal_Header.Add("수령자명")

            lal_Arg.Add(ls_Id)

            lal_Rtn = objHelp.fn_DisplayPop(Me, "수령자조회 ", "fn_PopGetUserList", lal_Arg, lal_Header, li_RtnCnt, ls_Id)

            If lal_Rtn.Count > 0 Then
                Me.txtRecid.Text = lal_Rtn(0).ToString
                Me.txtRecnm.Text = lal_Rtn(1).ToString
                Me.txtBldno.Focus()

                ' 구조체로 넘겨 받았을 경우 
                'With CType(lal_Rtn(0), CDHELP.clsRtnData)
                '    txtRegno.Text = .RTNDATA0
                '    txtPatNm.Text = .RTNDATA1
                'End With
            Else
                Me.txtRecid.Text = ""
                Me.txtRecnm.Text = ""
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnRecPop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRecPop.Click
        If Me.spdOrderList.MaxRows < 1 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회 후 수령자를 입력 하시기 바랍니다.")
            Return
        End If

        Dim objHelp As New CDHELP.FGCDHELP99
        Dim la_getValue As New ArrayList
        Dim lal_Header As New ArrayList
        Dim lal_Arg As New ArrayList
        Dim li_RtnCnt As Integer = 2
        Dim lal_Rtn As New ArrayList

        lal_Header.Add("수령자")
        lal_Header.Add("수령자명")

        lal_Arg.Add(" ")

        lal_Rtn = objHelp.fn_DisplayPop(Me, "수령자조회 ", "fn_PopGetUserList", lal_Arg, lal_Header, li_RtnCnt, "")

        If lal_Rtn.Count > 0 Then
            Me.txtRecid.Text = lal_Rtn(0).ToString
            Me.txtRecnm.Text = lal_Rtn(1).ToString

            ' 구조체로 넘겨 받았을 경우 
            'With CType(lal_Rtn(0), CDHELP.clsRtnData)
            '    txtRegno.Text = .RTNDATA0
            '    txtPatNm.Text = .RTNDATA1
            'End With
        Else
            Me.txtRecid.Text = ""
            Me.txtRecnm.Text = ""
        End If

    End Sub

    '스프레드 체크박스 마우스기능 차단
    Private Sub spdStOutList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdStOutList.ButtonClicked
        Dim ls_Filter As String
        Dim ls_chk As String
        Dim ls_bldno As String = txtBldno.Text
        Dim ls_sbldno As String = txtSBldno.Text.Replace("-"c, "")
        Dim ls_cbldno As String
        Dim li_MaxRow As Integer = spdStOutList.MaxRows

        With Me.spdStOutList
            .Row = e.row
            .Col = .GetColFromID("bldno") : ls_cbldno = .Text
            .Col = .GetColFromID("filter") : ls_Filter = .Text
            .Col = .GetColFromID("chk") : ls_chk = .Text

            If ls_chk = "1"c And ls_Filter <> "1"c Then
                If ls_bldno = ls_cbldno Then
                    Return
                ElseIf ls_bldno <> ls_sbldno And ls_bldno = "" Then
                    Return
                Else
                    .Text = ""
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "혈액번호를 마우스로 선택 할 수 없습니다.")
                End If
            ElseIf ls_Filter = "1"c Then
                If ls_chk = "1"c Then
                    miOutChkCnt += 1
                Else
                    miOutChkCnt -= 1
                End If

            End If
        End With

        Me.Focus()
        If miOutChkCnt = li_MaxRow Then
            Me.txtRecid.Focus()
        Else
            Me.txtBldno.Focus()
            Me.txtBldno.SelectAll()
        End If

    End Sub

    ' 혈액출고
    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        If spdOrderList.MaxRows < 1 Then Return
        '20191014 nbm 수령자 이름도 있어야 출고가능
        If Me.txtRecid.Text.Length() < 1 Or Me.txtRecnm.Text.Length() < 1 Then
            '------------------->
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "수령자를 입력 하시기 바랍니다.")
            Me.txtRecid.Focus()
            Me.txtRecid.SelectAll()
            Return
        End If

        Try
            Dim iChkCnt As Integer = 0
            Dim sTnsNum As String = ""
            Dim sChk As String = ""
            Dim sOwngbn As String = ""
            Dim sIogbn As String = ""
            Dim sRegno As String = ""
            Dim alOutList As New ArrayList
            Dim bContinue As Boolean = False
            Dim bOk As Boolean = False
            Dim sOrdDt As String = ""
            Dim sSpccd As String = ""
            Dim sFilter As String = ""
            Dim sAboType As String = ""
            Dim sRhType As String = ""
            '20210719 jhs 응급구분 추가
            Dim sTnsGbn As String = ""
            '---------------------------------

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("tnsjubsuno") : sTnsNum = .Text.Trim.Replace("-", "")
                .Col = .GetColFromID("owngbn") : sOwngbn = .Text.Trim
                .Col = .GetColFromID("iogbn") : sIogbn = .Text.Trim
                .Col = .GetColFromID("regno") : sRegno = .Text.Trim
                .Col = .GetColFromID("order_date") : sOrdDt = .Text.Trim
                .Col = .GetColFromID("spccd") : sSpccd = .Text.Trim
                .Col = .GetColFromID("filter") : sFilter = .Text.Trim
                .Col = .GetColFromID("abo") : sAboType = .Text.Trim
                .Col = .GetColFromID("rh") : sRhType = .Text.Trim
                '20210719 jhs 응급구분 추가
                .Col = .GetColFromID("tnsGbn") : sTnsGbn = .Text.Trim
                '---------------------------------
            End With

            With Me.spdStOutList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1"c Then
                        Dim stuOut As New STU_TnsJubsu
                        iChkCnt += 1

                        stuOut.TNSJUBSUNO = sTnsNum.Trim
                        stuOut.OWNGBN = sOwngbn.Trim
                        stuOut.IOGBN = sIogbn.Trim
                        stuOut.REGNO = sRegno.Trim
                        stuOut.ORDDATE = sOrdDt
                        stuOut.SPCCD = sSpccd.Trim
                        stuOut.FILTER = sFilter.Trim
                        stuOut.ABO = Me.AxTnsPatinfo1.AboRh.Replace("-", "").Replace("+", "")
                        stuOut.RH = Me.AxTnsPatinfo1.AboRh.Replace("A", "").Replace("B", "").Replace("O", "")
                        '20210719 jhs 응급구분 추가
                        If sTnsGbn = "3" Then
                            stuOut.EMER = "Y"
                        Else
                            stuOut.EMER = "N"
                        End If
                        '---------------------------------

                        .Col = .GetColFromID("bldno") : stuOut.BLDNO = .Text.Trim
                        .Col = .GetColFromID("comcd") : stuOut.COMCD = .Text.Trim
                        .Col = .GetColFromID("comcd_out") : stuOut.COMCD_OUT = .Text.Trim
                        .Col = .GetColFromID("fkocs") : stuOut.FKOCS = .Text.Trim
                        .Col = .GetColFromID("comordcd") : stuOut.COMORDCD = .Text.Trim

                        stuOut.RECID = Me.txtRecid.Text
                        stuOut.RECNM = Me.txtRecnm.Text

                        alOutList.Add(stuOut)
                    End If
                Next
            End With

            If iChkCnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고 등록 할 항목이 없습니다.")
                Return
            End If

            bContinue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "출고 등록 하시겠습니까?")

            If bContinue = False Then Return

            'bOk = (New Out).fnExe_Out(alOutList)
            'bOk = (New WEBSERVER.CGWEB_B).ExecuteDo_Out(alOutList)

            If Me.chkCMCO.Checked = False Then
                ' 일반혈액 출고
                bOk = (New WEBSERVER.CGWEB_B).ExecuteDo_Out(alOutList)
            Else
                ' 교차미필 혈액출고시
                bOk = (New LISAPP.APP_BT.Out).fnExe_Out_NotCross(alOutList, "E"c)
            End If

            If bOk = True Then

                If Me.lblBarPrinter.Text.Replace("사용안함", "") <> "" Then
                    With (New LISAPP.APP_BT.DB_BloodPrint)
                        .PrintDo(Me.Name, alOutList, True, Me.chkSummery.Checked, 2) ' 출고 스티커 출력
                    End With
                End If

                If Me.chkPrt.Checked Then
                    '-- 출고내역서
                    Dim prt As New FGB09_PRINT

                    prt.m_al_PrtData = alOutList
                    prt.sbPrint()

                End If

                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고 등록 되었습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "출고 등록중 오류가 발생 하였습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
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

            Me.spdOrderList_ClickEvent(Me.spdOrderList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, Me.spdOrderList.ActiveRow))

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub

    ' 출고취소
    Private Sub btnExeCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExeCancel.Click
        If Me.spdOrderList.MaxRows < 1 Then Return

        If USER_SKILL.Authority("B01", 7) = False Then
            MsgBox("혈액은행 [출고 취소]의 권한이 없습니다!!")
            Return
        End If

        Try
            Dim li_ChkCnt As Integer = 0
            Dim ls_TnsNum As String
            Dim ls_chk As String
            Dim ls_Owngbn As String
            Dim ls_Iogbn As String
            Dim ls_Regno As String
            Dim lal_Arg As New ArrayList
            Dim lb_Continue As Boolean
            Dim lb_ok As Boolean
            Dim ls_orddate As String
            Dim ls_spccd As String
            Dim ls_filter As String
            Dim sAboType As String = ""
            Dim sRhType As String = ""

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text.Replace("-", "")
                .Col = .GetColFromID("owngbn") : ls_Owngbn = .Text
                .Col = .GetColFromID("iogbn") : ls_Iogbn = .Text
                .Col = .GetColFromID("regno") : ls_Regno = .Text
                .Col = .GetColFromID("order_date") : ls_orddate = .Text
                .Col = .GetColFromID("spccd") : ls_spccd = .Text
                .Col = .GetColFromID("filter") : ls_filter = .Text
                .Col = .GetColFromID("abo") : sAboType = .Text
                .Col = .GetColFromID("rh") : sRhType = .Text

            End With

            With Me.spdOutList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : ls_chk = .Text

                    If ls_chk = "1"c Then
                        Dim lcls_StOut As New STU_TnsJubsu
                        li_ChkCnt += 1

                        lcls_StOut.TNSJUBSUNO = ls_TnsNum
                        lcls_StOut.OWNGBN = ls_Owngbn
                        lcls_StOut.IOGBN = ls_Iogbn
                        lcls_StOut.REGNO = ls_Regno
                        lcls_StOut.ORDDATE = ls_orddate
                        lcls_StOut.SPCCD = ls_spccd
                        lcls_StOut.FILTER = ls_filter
                        lcls_StOut.ABO = Me.AxTnsPatinfo1.AboRh.Replace("+", "").Replace("-", "")
                        lcls_StOut.RH = Me.AxTnsPatinfo1.AboRh.Replace("A", "").Replace("B", "").Replace("O", "")

                        .Col = .GetColFromID("bldno") : lcls_StOut.BLDNO = .Text
                        .Col = .GetColFromID("comcd") : lcls_StOut.COMCD = .Text
                        .Col = .GetColFromID("comcd_out") : lcls_StOut.COMCD_OUT = .Text
                        .Col = .GetColFromID("fkocs") : lcls_StOut.FKOCS = .Text
                        .Col = .GetColFromID("comordcd") : lcls_StOut.COMORDCD = .Text

                        lal_Arg.Add(lcls_StOut)
                    End If
                Next
            End With

            If li_ChkCnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고 취소 할 항목이 없습니다.")
                Return
            End If

            lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "출고 취소 하시겠습니까?")

            If lb_Continue = False Then Return
            'pro_ack_exe_tns_out_cancel'
            'lb_ok = (New Out).fnExe_Out_Cancel(lal_Arg, "C"c)
            ' lb_ok = (New WEBSERVER.CGWEB_B).ExecuteDo_Out_cancle(lal_Arg, "C"c)
            If Me.chkCMCO.Checked = False Then
                lb_ok = (New WEBSERVER.CGWEB_B).ExecuteDo_Out_cancle(lal_Arg, "C"c)
            Else

                lb_ok = (New Out).fnExe_Out_NotCross(lal_Arg, "C"c)

            End If


            If lb_ok = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고 취소 되었습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "출고 취소중 오류가 발생 하였습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub spdOutList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdOutList.ButtonClicked
        Me.Focus()

        Dim ls_chk As String
        Dim ls_elapsdm As String
        Dim lb_Continue As Boolean

        With spdOutList
            .Row = e.row
            .Col = .GetColFromID("chk") : ls_chk = .Text
            .Col = .GetColFromID("elapsdm") : ls_elapsdm = .Text

            If ls_chk = "1"c And CLng(ls_elapsdm) > CInt(PRG_CONST.BLD_OUT_TIME) Then
                lb_Continue = fn_PopConfirm(Me, "I"c, "출고 후 " + PRG_CONST.BLD_OUT_TIME + "분이 경과한 혈액입니다. 선택 하시겠습니까?")

                If lb_Continue = False Then
                    .Col = .GetColFromID("chk") : .Text = "0"c
                End If
            End If
        End With

        txtBldno.Focus()
    End Sub

    Private Sub btnKeep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeep.Click
        If Me.spdOrderList.MaxRows < 1 Then Return
        If Me.spdOutList.MaxRows < 1 Then Return

        Try
            Dim ls_filter As String
            Dim ls_TnsNum As String
            Dim ls_Comcd As String
            Dim ls_bldno As String
            Dim ls_keepgbn As String
            Dim ls_chk As String
            Dim li_ChkCnt As Integer = 0
            Dim lal_Arg As New ArrayList
            Dim lb_Continue As Boolean
            Dim lb_ok As Boolean

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("filter") : ls_filter = .Text

            End With

            If ls_filter = "1"c Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "필터는 보관 대상이 아닙니다.")
                Return
            End If

            With Me.spdOutList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : ls_chk = .Text
                    .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text.Replace("-", "")
                    .Col = .GetColFromID("comcd") : ls_Comcd = .Text
                    .Col = .GetColFromID("bldno") : ls_bldno = .Text
                    .Col = .GetColFromID("keepgbn") : ls_keepgbn = .Text

                    If ls_chk = "1"c And ls_keepgbn <> "1"c Then
                        Dim lcls_Keep As New STU_TnsJubsu
                        li_ChkCnt += 1

                        lcls_Keep.TNSJUBSUNO = ls_TnsNum
                        lcls_Keep.COMCD = ls_Comcd
                        lcls_Keep.BLDNO = ls_bldno

                        lal_Arg.Add(lcls_Keep)
                    End If
                Next
            End With

            If li_ChkCnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관작업을 할 선택 항목이 없습니다.")
                Return
            End If

            lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "보관작업 진행 하시겠습니까?")

            If lb_Continue = False Then Return

            ' 보관 및 재출고
            lb_ok = (New TnsReg).fn_KeepExe(lal_Arg, "1"c)

            If lb_ok = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관 처리 되었습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "보관 작업중 오류가 발생 하였습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnReOut_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReOut.Click
        If Me.spdOrderList.MaxRows < 1 Then Return
        If Me.spdOutList.MaxRows < 1 Then Return

        '20191014 nbm 수령자 이름도 있어야 출고가능
        If Me.txtRecid.Text.Length() < 1 Or Me.txtRecnm.Text.Length() < 1 Then
            '------------------->
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "수령자를 입력 하시기 바랍니다.")
            Me.txtRecid.Focus()
            Me.txtRecid.SelectAll()
            Return
        End If

        Try
            Dim ls_TnsNum As String
            Dim ls_Comcd As String
            Dim ls_bldno As String
            Dim ls_keepgbn As String
            Dim ls_chk As String
            Dim li_ChkCnt As Integer = 0
            Dim lal_Arg As New ArrayList
            Dim lb_Continue As Boolean
            Dim lb_ok As Boolean

            With Me.spdOutList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : ls_chk = .Text
                    .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                    .Col = .GetColFromID("comcd") : ls_Comcd = .Text
                    .Col = .GetColFromID("bldno") : ls_bldno = .Text
                    .Col = .GetColFromID("keepgbn") : ls_keepgbn = .Text

                    If ls_chk = "1"c And ls_keepgbn = "1"c Then
                        Dim lcls_Keep As New STU_TnsJubsu
                        li_ChkCnt += 1

                        lcls_Keep.TNSJUBSUNO = ls_TnsNum
                        lcls_Keep.COMCD = ls_Comcd
                        lcls_Keep.BLDNO = ls_bldno
                        lcls_Keep.RECID = txtRecid.Text
                        lcls_Keep.RECNM = txtRecnm.Text

                        lal_Arg.Add(lcls_Keep)
                    End If
                Next
            End With

            If li_ChkCnt < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "재출고할 선택 항목이 없습니다.")
                Return
            End If

            lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "재출고 하시겠습니까?")

            If lb_Continue = False Then Return

            ' 보관 및 재출고
            lb_ok = (New TnsReg).fn_KeepExe(lal_Arg, "2"c)

            If lb_ok = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "재출고 되었습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "재출고 작업중 오류가 발생 하였습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsRegno As String, ByVal rdFdate As Date, ByVal rdTdate As Date)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub sb_RegnoCalled(ByVal rsRegno As String, ByVal rdFdate As Date, ByVal rdTdate As Date)

        Me.dtpDate0.Value = rdFdate
        Me.dtpDate1.Value = rdTdate
        Me.txtRegno.Text = rsRegno

        btnSearch_Click(Nothing, Nothing)
    End Sub

    Private Sub btnPrint_Set_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint_Set.Click
        Dim sFn As String = "Handles btnPrintSet.Click"

        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC(Me.Name, Me.chkBarInit.Checked)

        Try
            objFrm.ShowDialog()
            Me.lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnRePrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRePrint.Click
        If Me.spdOutList.MaxRows < 1 Then Return

        Try
            Dim alPrtData As New ArrayList

            With Me.spdOutList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("tnsjubsuno") : Dim rsTnsNo As String = .Text.Trim.Replace("-", "")
                    .Col = .GetColFromID("vbldno") : Dim sBldNo As String = .Text.Trim.Replace("-", "")
                    .Col = .GetColFromID("chk") : Dim sChk As String = .Text.Trim

                    If sChk = "1" Then
                        Dim bldl As New STU_TnsJubsu

                        bldl.TNSJUBSUNO = rsTnsNo
                        bldl.BLDNO = sBldNo

                        alPrtData.Add(bldl)
                    End If
                Next

            End With


            If alPrtData.Count > 0 Then
                If Me.lblBarPrinter.Text <> "" Then
                    If Me.chkPrt_Label.Checked Or Me.chkSummery.Checked Then
                        With (New LISAPP.APP_BT.DB_BloodPrint)
                            .PrintDo(Me.Name, alPrtData, Me.chkPrt_Label.Checked, Me.chkSummery.Checked, 1) ' 출고 스티커 출력
                        End With
                    End If
                End If

                If Me.chkPrt.Visible And Me.chkPrt.Checked Then
                    Dim prt As New FGB09_PRINT

                    prt.m_al_PrtData = alPrtData

                    prt.sbPrint()
                End If
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub spdPastTns_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdPastTns.DblClick
        Dim sFn As String = "spdPastTns.DblClick"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor
            Dim sTnsNo As String = ""

            With Me.spdPastTns
                .Row = e.row
                .Col = .GetColFromID("tnsjubsuno") : sTnsNo = .Text
            End With

            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.spdPastTns)

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.spdPastTns)

            Dim objHelp As New CDHELP.FGCDHELP01

            Dim dt As DataTable = CGDA_BT.fn_GetPastTnsDetailList(sTnsNo.Replace("-", ""), "")

            objHelp.FormText = "수혈 상세 조회"

            objHelp.OnRowReturnYN = False
            objHelp.MaxRows = 15

            objHelp.AddField("tnsjubsuno", "수혈의뢰접수번호", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("state", "상태", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("comnmd", "성분제제명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("aborh_p", "환  자혈액형", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("bldno", "혈액번호", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("aborh_b", "혈  액혈액형", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("testdt", "검사일", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("testnm", "검사자", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("befoutdt", "가출고일", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("befoutnm", "가출고자", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("outdt", "출고일", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("outnm", "출고자", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("recnm", "수령자", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("rtndt", "반납/폐기일", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("rtnnm", "반납/폐기자", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("rtnreqnm", "반납/폐기의뢰자", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim alList As ArrayList = objHelp.Display_Result(Me, iLeft, iTop, dt)


        Catch ex As Exception


        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub


    Private Sub spdOrderList_BeforeUserSort(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BeforeUserSortEvent) Handles spdOrderList.BeforeUserSort


        ' 2019-02-15 JJH 성명컬림 클릭시 성명 정렬후 성명기준으로 성분제제 정렬 기능
        With spdOrderList
            If e.col = 8 Then  ' 클릭한 컬럼이 성명컬럼 일때
                .BlockMode = True
                .Col = -1
                .Row = -1
                .Col2 = -1
                .Row = -1
                .SortBy = 0
                .set_SortKey(1, 8)
                .set_SortKey(2, 3)
                .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .Action = FPSpreadADO.ActionConstants.ActionSort
                .BlockMode = False
            Else
                .BlockMode = True
                .Col = -1
                .Row = -1
                .SortBy = 0
                .set_SortKey(1, e.col)
                .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .Action = FPSpreadADO.ActionConstants.ActionSort
                .BlockMode = False
            End If
        End With


    End Sub

    Private Sub spdOrderList_AfterUserSort(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_AfterUserSortEvent) Handles spdOrderList.AfterUserSort

        'With spdOrderList
        '    If e.col = 7 Then '클릭한 컬럼이 성명컬럼 일때
        '        .BlockMode = True
        '        .Col = -1
        '        .Row = -1
        '        .Col2 = -1
        '        .Row = -1
        '        .SortBy = 0
        '        .set_SortKey(1, 8)
        '        .set_SortKey(2, 9)
        '        .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
        '        .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
        '        .Action = FPSpreadADO.ActionConstants.ActionSort
        '        .BlockMode = False
        '    Else
        '        .BlockMode = True
        '        .Col = -1
        '        .Row = -1
        '        .SortBy = 0
        '        .set_SortKey(1, e.col)
        '        .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
        '        .Action = FPSpreadADO.ActionConstants.ActionSort
        '        .BlockMode = False
        '    End If
        'End With


    End Sub
    '20211021 jhs 혈액불출요청서 혈액번호 확인
    Private Sub txtRequestBlood_Click(ByVal sender As Object, e As EventArgs) Handles txtRequestBlood.Click
        Me.txtRequestBlood.SelectAll()
    End Sub

    Private Sub txtRequestBlood_KeyDown(ByVal sender As Object, e As KeyEventArgs) Handles txtRequestBlood.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Dim sBldno As String = Me.txtRequestBlood.Text
        If sBldno.Length < 10 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "올바른 혈액번호가 아닙니다.")
            Return
        End If

        If Me.txtBldno.Text = "" Then
            Me.txtBldno.Focus()
            Me.txtBldno.SelectAll()
            Return
        End If

        If Me.txtBldno.Text <> "" And Me.txtRequestBlood.Text <> "" Then
            txtBldno_KeyDown(sender, e)
        End If

    End Sub
    '-------------------------------------
End Class

Public Class FGB09_PRINT
    Private Const msFile As String = "File : FGB09.vb, Class : LISB" + vbTab

    Private miPageNo As Integer = 0

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")

    Public m_al_PrtData As ArrayList

    Public Sub sbPrint_Preview()
        Dim sFn As String = "Sub sbPrint_Preview()"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = False
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_출고내역서"

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

    Public Sub sbPrint()
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = False

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog


            If prtBPress = DialogResult.OK Then


                prtR.DocumentName = "ACK_출고내역서"

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
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim sTnsNo As String = ""
        Dim sBldNos As String = ""
        For ix As Integer = 0 To m_al_PrtData.Count - 1
            sTnsNo = CType(m_al_PrtData(0), STU_TnsJubsu).TNSJUBSUNO

            If ix > 0 Then sBldNos += ","
            sBldNos += CType(m_al_PrtData(ix), STU_TnsJubsu).BLDNO
        Next

        Dim dt As DataTable = (New LISAPP.APP_BT.DB_BloodPrint).fnGet_Blood_Label_Info(sTnsNo, sBldNos)
        If dt.Rows.Count < 1 Then Return

        Dim iPage As Integer = 0
        Dim sgPosY As Single = 0
        Dim sgPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Underline)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Body_b As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_aborh_b As New Font("굴림체", 24, FontStyle.Bold)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 50

        sgPosY = msgTop
        sgPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim sgPosX(0 To 6) As Single

        sgPosX(0) = msgLeft
        sgPosX(1) = sgPosX(0) + sgPosY * 2
        sgPosX(2) = sgPosX(1) + 240
        sgPosX(3) = sgPosX(2) + 240
        sgPosX(4) = msgWidth - 5

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far


        Dim rect As New Drawing.RectangleF
        rect = New Drawing.RectangleF(sgPosX(0), sgPosY, msgWidth, sgPrtH * 2)
        e.Graphics.DrawString("혈 액 출 고 내 역 서", fnt_Title, Drawing.Brushes.Black, rect, sf_c)

        sgPosY += sgPrtH * 2 + sgPrtH / 2
        rect = New Drawing.RectangleF(msgLeft, sgPosY, msgWidth - msgLeft - 25, sgPrtH)
        e.Graphics.DrawString(msTitle_Time, fnt_Body, Drawing.Brushes.Black, rect, sf_r)

        '-- 라인
        sgPosY += sgPrtH
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY, msgWidth, sgPosY)
        'e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(1), sgPosY + sgPrtH * 4, msgWidth, sgPosY + sgPrtH * 4)

        sgPosY += sgPrtH / 2
        e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(0), sgPosY + sgPrtH * 0, sgPosX(1), sgPosY + sgPrtH * 0)
        e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(0), sgPosY + sgPrtH * 1, sgPosX(1), sgPosY + sgPrtH * 1)
        e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(0), sgPosY + sgPrtH * 4, sgPosX(1), sgPosY + sgPrtH * 4)
        e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(0), sgPosY, sgPosX(0), sgPosY + sgPrtH * 4)
        e.Graphics.DrawLine(Drawing.Pens.Black, sgPosX(1), sgPosY, sgPosX(1), sgPosY + sgPrtH * 4)

        'e.Graphics.DrawRectangle(Drawing.Pens.Black, sgPosX(0), sgPosY, sgPosX(1) - sgPosX(0), sgPrtH * 3)
        rect = New Drawing.RectangleF(sgPosX(0), sgPosY, sgPosX(1) - sgPosX(0), sgPrtH)
        e.Graphics.DrawString("환자혈액형", fnt_Body, Drawing.Brushes.Black, rect, sf_c)

        rect = New Drawing.RectangleF(sgPosX(0), sgPosY + sgPrtH, sgPosX(1) - sgPosX(0), sgPrtH * 3)
        e.Graphics.DrawString(dt.Rows(0).Item("pat_aborh").ToString, fnt_aborh_b, Drawing.Brushes.Black, rect, sf_c)

        sgPosY += sgPrtH
        rect = New Drawing.RectangleF(sgPosX(1), sgPosY, sgPosX(2) - sgPosX(1), sgPrtH)
        e.Graphics.DrawString("   등록번호 : " + dt.Rows(0).Item("regno").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)

        sgPosY += sgPrtH
        rect = New Drawing.RectangleF(sgPosX(1), sgPosY, sgPosX(2) - sgPosX(1), sgPrtH)
        e.Graphics.DrawString("   환 자 명 : " + dt.Rows(0).Item("patnm").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(2), sgPosY, sgPosX(3) - sgPosX(2), sgPrtH)
        e.Graphics.DrawString("진 료 과: " + dt.Rows(0).Item("deptward").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)

        sgPosY += sgPrtH
        rect = New Drawing.RectangleF(sgPosX(1), sgPosY, sgPosX(2) - sgPosX(1), sgPrtH)
        e.Graphics.DrawString("   성별/나이: " + dt.Rows(0).Item("sexage").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(2), sgPosY, sgPosX(3) - sgPosX(2), sgPrtH)
        e.Graphics.DrawString("주 치 의: " + dt.Rows(0).Item("drnm").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sgPosY, sgPosX(4) - sgPosX(3), sgPrtH)
        e.Graphics.DrawString("처방일시: " + dt.Rows(0).Item("orddt").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)

        sgPosY += sgPrtH + sgPrtH / 2
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY, msgWidth, sgPosY)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sgPosY + sgPrtH + sgPrtH / 2, msgWidth, sgPosY + sgPrtH + sgPrtH / 2)

        sgPosY += sgPrtH / 2
        rect = New Drawing.RectangleF(sgPosX(1), sgPosY, sgPosX(3) - sgPosX(1), sgPrtH)
        e.Graphics.DrawString("   처방명칭 : " + dt.Rows(0).Item("comnm_ord").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)

        rect = New Drawing.RectangleF(sgPosX(3), sgPosY, sgPosX(4) - sgPosX(3), sgPrtH)
        e.Graphics.DrawString("의뢰수량: " + dt.Rows(0).Item("reqqnt").ToString, fnt_Body_b, Drawing.Brushes.Black, rect, sf_l)


        miPageNo += 1

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sgPrtH * 2 - sgPrtH / 2, msgWidth, msgHeight - sgPrtH * 2 - sgPrtH / 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sgPrtH * 2, msgWidth - msgLeft - 25, sgPrtH), sf_r)

        e.HasMorePages = False

    End Sub





End Class
