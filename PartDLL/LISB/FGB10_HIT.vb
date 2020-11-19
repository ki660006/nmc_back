'>>> 혈액반납/폐기

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB10
    Private mobjDAF As New LISAPP.APP_F_COMCD
    Private ms_rsncd_list As String = ""

    Private Sub sbDisplay_rsn(ByVal riRtnFlg As Integer)
        Try
            ms_rsncd_list = ""
            Dim dt As DataTable = CDHELP.FGCDHELPFN.fn_CmtList(riRtnFlg, "")

            If dt.Rows.Count < 1 Then Return

            ms_rsncd_list = Chr(9)

            For ix As Integer = 0 To dt.Rows.Count - 1
                ms_rsncd_list += dt.Rows(ix).Item("cmt").ToString + Chr(9)
            Next

        Catch ex As Exception

        End Try


    End Sub
    Private Sub FGB10_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FGB10_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized
        Me.txtRegno.MaxLength = PRG_CONST.Len_RegNo()

        ' 화면 오픈시 초기화
        Me.spdOrderList.MaxRows = 0
        Me.spdRtnList.MaxRows = 0
        Me.spdOutList.MaxRows = 0

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).AddDays(-1)
        Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_SpreadDesige.sbInti(spdOrderList)
        DS_SpreadDesige.sbInti(spdRtnList)
        DS_SpreadDesige.sbInti(spdOutList)

        Me.ntxtBldno.Text = ""

        sbDisplay_rsn(0)
        sb_SetComboDt()

    End Sub

    Public Sub sb_SetComboDt(Optional ByVal rsUsDt As String = "", Optional ByVal rsUeDt As String = "")
        ' 콤보 데이터 생성
        Try

            If rsUsDt = "" Then rsUsDt = "20000101"
            If rsUeDt = "" Then rsUeDt = "30000101"

            Dim dt As DataTable = mobjDAF.GetComCdInfo(rsUsDt)

            Me.cboComCd.Items.Clear()
            Me.cboComCd.Items.Add("[] 전체")
            If dt.Rows.Count > 0 Then
                With cboComCd
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add(dt.Rows(i).Item("COMNMD"))
                    Next
                End With
            Else
                Exit Sub
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGB10_NEW_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.txtSBldno.Focus()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()

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
        If Me.txtRegno.Text.Length() < 1 Then
            Me.txtPatNm.Text = ""
            Return
        End If

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
                If sRegno.Length() < 8 Then
                    sRegno = sRegno.PadLeft(8, "0"c)
                End If
            Else
                If sRegno.Length() < 8 Then
                    sRegno = sRegno.Substring(0, 1) + sRegno.Substring(1).PadLeft(7, "0"c)
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

            If Me.spdOrderList.MaxRows < 1 Then Return

            With Me.spdOrderList
                .Row = 1
                .Col = .GetColFromID("order_date") : sOrdDt = .Text
                .Col = .GetColFromID("tnsjubsuno") : sTnsnum = .Text.Trim

            End With

            ' 환자정보 디스플레이
            Me.axPatInfo.sb_setPatinfo(sRegno, sOrdDt, sTnsnum)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Dim sComcd As String = ""
            Dim sBldno As String = ""

            Me.spdOrderList.MaxRows = 0
            Me.spdRtnList.MaxRows = 0
            Me.spdOutList.MaxRows = 0
            Me.axPatInfo.sb_ClearLbl()

            sComcd = Ctrl.Get_Code(Me.cboComCd)
            sBldno = txtSBldno.Text

            ' 조회
            Dim dt As DataTable = CGDA_BT.fn_RtnOrderList(IIf(Me.rdoRtn.Checked, "1", "2").ToString, Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), Me.txtRegno.Text, sComcd, sBldno)

            sb_DisplayDataList(dt)
            Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception
            Cursor.Current = System.Windows.Forms.Cursors.Default
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sb_DisplayDataList(ByVal r_dt As DataTable)
        Try
            With Me.spdOrderList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False

                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    Dim sPatInfo() As String = r_dt.Rows(ix).Item("patinfo").ToString.Split("|"c)
                    '< 나이계산
                    Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                    Dim dtSysDate As Date = CDate(r_dt.Rows(ix).Item("jubsudt").ToString.Substring(0, 10))
                    Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                    If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                    '>

                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(ix).Item("tnsjubsuno").ToString.Trim
                    .Col = .GetColFromID("vtnsjubsuno") : .Text = r_dt.Rows(ix).Item("vtnsjubsuno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                    .Col = .GetColFromID("sexage") : .Text = sPatInfo(1).Trim + "/" + iAge.ToString
                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(ix).Item("comnm").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("deptnm") : .Text = r_dt.Rows(ix).Item("deptnm").ToString.Trim
                    .Col = .GetColFromID("wardroom") : .Text = r_dt.Rows(ix).Item("wardroom").ToString.Trim
                    .Col = .GetColFromID("jubsudt") : .Text = r_dt.Rows(ix).Item("jubsudt").ToString.Trim
                    .Col = .GetColFromID("state") : .Text = r_dt.Rows(ix).Item("state").ToString.Trim

                    If r_dt.Rows(ix).Item("state").ToString.Trim.IndexOf("의뢰") >= 0 Then
                        .ForeColor = Color.Red
                    Else
                        .ForeColor = Color.Black
                    End If

                    .Col = .GetColFromID("order_date") : .Text = r_dt.Rows(ix).Item("order_date").ToString.Trim

                    .Col = .GetColFromID("rtnreqflg") : .Text = r_dt.Rows(ix).Item("rtnreqflg").ToString.Trim

                    .Col = .GetColFromID("ir") : .Text = r_dt.Rows(ix).Item("ir").ToString
                    .Col = .GetColFromID("filter") : .Text = r_dt.Rows(ix).Item("filter").ToString
                    .Col = .GetColFromID("reqqnt") : .Text = r_dt.Rows(ix).Item("reqqnt").ToString
                    .Col = .GetColFromID("befoutqnt") : .Text = r_dt.Rows(ix).Item("befoutqnt").ToString
                    .Col = .GetColFromID("outqnt") : .Text = r_dt.Rows(ix).Item("outqnt").ToString
                    .Col = .GetColFromID("rtnqnt") : .Text = r_dt.Rows(ix).Item("rtnqnt").ToString
                    .Col = .GetColFromID("abnqnt") : .Text = r_dt.Rows(ix).Item("abnqnt").ToString
                    .Col = .GetColFromID("cancelqnt") : .Text = r_dt.Rows(ix).Item("cancelqnt").ToString
                Next

                sb_SetStBarSearchCnt(r_dt.Rows.Count)

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
        Me.spdRtnList.MaxRows = 0
        Me.spdOutList.MaxRows = 0

        Me.txtSBldno.Text = ""
        Me.ntxtBldno.Text = ""

        Me.axPatInfo.sb_ClearLbl()
    End Sub

    Private Sub rdoRtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoRtn.Click
        Me.btnExecute.Text = "반  납(F7)"
        Me.ntxtBldno.Focus()

        sbDisplay_rsn(0)

        btnClear_Click(Nothing, Nothing)

    End Sub

    Private Sub rdoAbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoAbn.Click
        Me.btnExecute.Text = "폐  기(F7)"
        Me.ntxtBldno.Focus()

        sbDisplay_rsn(1)

        btnClear_Click(Nothing, Nothing)
    End Sub

    Private Sub spdOrderList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdOrderList.ClickEvent
        If spdOrderList.MaxRows < 1 Then Return

        sb_DisPlaySubData(e.row)
    End Sub

    Private Sub sb_DisPlaySubData(ByVal riRow As Integer)
        Try
            Dim dt As New DataTable
            Dim sRegno As String = ""
            Dim sTnsno As String = ""
            Dim sOrdDt As String = ""
            Dim sFkocs_req As String = ""

            If spdOrderList.MaxRows < 1 Then Return

            spdRtnList.MaxRows = 0
            spdOutList.MaxRows = 0

            With Me.spdOrderList
                .Row = riRow
                .Col = .GetColFromID("regno") : sRegno = .Text.Trim
                .Col = .GetColFromID("tnsjubsuno") : sTnsno = .Text.Trim
                .Col = .GetColFromID("order_date") : sOrdDt = .Text.Replace("-"c, "").Trim
                .Col = .GetColFromID("fkocs_req") : sFkocs_req = .Text.Trim

            End With

            ' 환자 정보 조회
            Me.axPatInfo.sb_setPatinfo(sRegno, sOrdDt, sTnsno)

            ' 반납/폐기목록
            dt = CGDA_BT.fn_RtnList(IIf(rdoRtn.Checked, "1", "2").ToString, sTnsno)
            sb_DisplayRtnList(dt)

            ' 출고 목록
            dt = CGDA_BT.fn_RtnOutList(IIf(rdoRtn.Checked, "1", "2").ToString, sTnsno, sRegno)
            sb_DisplayRtnOutList(dt)

            Me.ntxtBldno.Focus()

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 반납/폐기목록
    Private Sub sb_DisplayRtnList(ByVal r_dt As DataTable)

        Try
            With spdRtnList
                .MaxRows = 0
                If r_Dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("gubun") : .Text = r_dt.Rows(ix).Item("gubun").ToString.Trim
                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString.Trim
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(ix).Item("vbldno").ToString.Trim
                    .Col = .GetColFromID("aborh") : .Text = r_dt.Rows(ix).Item("aborh").ToString.Trim
                    .Col = .GetColFromID("rtndt") : .Text = r_dt.Rows(ix).Item("rtndt").ToString.Trim
                    .Col = .GetColFromID("rtnnm") : .Text = r_dt.Rows(ix).Item("rtnnm").ToString.Trim
                    .Col = .GetColFromID("rtnreqnm") : .Text = r_dt.Rows(ix).Item("rtnreqnm").ToString.Trim
                    .Col = .GetColFromID("rtnrsncmt") : .Text = r_dt.Rows(ix).Item("rtnrsncmt").ToString.Trim
                    .Col = .GetColFromID("indt") : .Text = r_dt.Rows(ix).Item("indt").ToString.Trim
                    .Col = .GetColFromID("dondt") : .Text = r_dt.Rows(ix).Item("dondt").ToString.Trim
                    .Col = .GetColFromID("availdt") : .Text = r_dt.Rows(ix).Item("availdt").ToString.Trim
                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(ix).Item("tnsjubsuno").ToString.Trim
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("comcd").ToString.Trim
                    .Col = .GetColFromID("comcd_out") : .Text = r_dt.Rows(ix).Item("comcd_out").ToString.Trim
                    .Col = .GetColFromID("owngbn") : .Text = r_dt.Rows(ix).Item("owngbn").ToString.Trim
                    .Col = .GetColFromID("iogbn") : .Text = r_dt.Rows(ix).Item("iogbn").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(ix).Item("fkocs").ToString.Trim
                    .Col = .GetColFromID("chk") : .Text = ""
                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdRtnList.ReDraw = True
        End Try
    End Sub

    ' 출고 목록
    Private Sub sb_DisplayRtnOutList(ByVal r_dt As DataTable)
        Dim sKeepGbn As String = ""

        Try
            With Me.spdOutList
                .MaxRows = 0
                If r_dt.Rows.Count < 1 Then Return

                .ReDraw = False
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("comnmd") : .Text = r_dt.Rows(ix).Item("comnmd").ToString
                    .Col = .GetColFromID("outnm") : .Text = r_dt.Rows(ix).Item("outnm").ToString
                    .Col = .GetColFromID("elapsdt") : .Text = r_dt.Rows(ix).Item("elapsdt").ToString
                    .Col = .GetColFromID("vbldno") : .Text = r_dt.Rows(ix).Item("vbldno").ToString
                    .Col = .GetColFromID("aborh") : .Text = r_dt.Rows(ix).Item("aborh").ToString
                    .Col = .GetColFromID("befoutdt") : .Text = r_dt.Rows(ix).Item("befoutdt").ToString
                    .Col = .GetColFromID("testnm") : .Text = r_dt.Rows(ix).Item("testnm").ToString
                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(ix).Item("tnsjubsuno").ToString
                    .Col = .GetColFromID("comcd") : .Text = r_dt.Rows(ix).Item("comcd").ToString
                    .Col = .GetColFromID("comcd_out") : .Text = r_dt.Rows(ix).Item("comcd_out").ToString
                    .Col = .GetColFromID("comordcd") : .Text = r_dt.Rows(ix).Item("comordcd").ToString
                    .Col = .GetColFromID("owngbn") : .Text = r_dt.Rows(ix).Item("owngbn").ToString
                    .Col = .GetColFromID("iogbn") : .Text = r_dt.Rows(ix).Item("iogbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = r_dt.Rows(ix).Item("fkocs").ToString
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString
                    .Col = .GetColFromID("bldno") : .Text = r_dt.Rows(ix).Item("bldno").ToString
                    .Col = .GetColFromID("keepelapsdm") : .Text = r_dt.Rows(ix).Item("keepelapsdm").ToString
                    .Col = .GetColFromID("state") : .Text = r_dt.Rows(ix).Item("state").ToString
                    '.Col = .GetColFromID("outdt") : .Text = r_dt.Rows(ix).Item("outdt").ToString
                    .Col = .GetColFromID("outnm") : .Text = r_dt.Rows(ix).Item("outnm").ToString

                    .Col = .GetColFromID("rtnrsncd") : .TypeComboBoxList = ms_rsncd_list
                    .Text = "[" + r_dt.Rows(ix).Item("rtnrsncd").ToString + "]" + r_dt.Rows(ix).Item("rtnrsncmt").ToString.Replace("-", "")

                    .Col = .GetColFromID("rtnrsncmt") : .Text = r_dt.Rows(ix).Item("rtnrsncmt").ToString.Replace("-", "")
                    .Col = .GetColFromID("rtnreqid") : .Text = r_dt.Rows(ix).Item("rtnreqid").ToString
                    .Col = .GetColFromID("rtnreqnm") : .Text = r_dt.Rows(ix).Item("rtnreqnm").ToString

                    If r_dt.Rows(ix).Item("rtnreqid").ToString <> "" Then
                        Me.txtRtnreqid.Text = r_dt.Rows(ix).Item("rtnreqid").ToString
                        Me.txtRtnreqnm.Text = r_dt.Rows(ix).Item("rtnreqnm").ToString
                    End If

                    sKeepGbn = r_dt.Rows(ix).Item("keepgbn").ToString

                    If sKeepGbn = "1"c Then
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = ix + 1 : .Row2 = ix + 1
                        .BlockMode = True

                        .BackColor = Color.CadetBlue

                        .BlockMode = False
                    ElseIf sKeepGbn = "2"c Then
                        .Col = 1 : .Col2 = .MaxCols
                        .Row = ix + 1 : .Row2 = ix + 1
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

    ' 조회조건 혈액번호 박스
    Private Sub txtSBldno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSBldno.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim sBldno As String = ""
            Dim iFRow As Integer = 0
            Dim sKeepelapsdm As String = ""
            Dim bContinue As Boolean = False

            sBldno = Me.txtSBldno.Text

            If sBldno.Length() = 10 Then
                sBldno = sBldno
            ElseIf sBldno.Length() = 12 Then
                sBldno = sBldno.Replace("-"c, "")
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "혈액번호를 정확히 입력하시기 바랍니다.")
                Me.txtSBldno.Focus()
                Me.txtSBldno.SelectAll()
                Return
            End If

            Me.txtSBldno.Text = sBldno

            btnSearch_Click(Nothing, Nothing)

            If spdOrderList.MaxRows < 1 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회기간중 등록된 혈액이 없습니다.")
                Me.txtSBldno.Focus()
                Return
            Else
                sb_DisPlaySubData(1)

                With Me.spdOutList
                    If .MaxRows > 0 Then
                        iFRow = Fn.SpdColSearch(spdOutList, sBldno, .GetColFromID("bldno"))

                        .Col = .GetColFromID("keepelapsdm") : skeepelapsdm = .Text

                        If skeepelapsdm = "" Then skeepelapsdm = "0"c

                        If CLng(sKeepelapsdm) > CInt(PRG_CONST.BLD_OUT_TIME) Then
                            bContinue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, " 출고 후  " + PRG_CONST.BLD_OUT_TIME + "분이 경과된 혈액입니다." + vbCrLf + "선택 하시겠습니까?")

                            If bContinue = False Then
                                Me.ntxtBldno.Text = ""
                                Me.ntxtBldno.Focus()
                                Return
                            End If
                        End If

                        If iFRow > 0 Then
                            .Row = iFRow
                            .Col = .GetColFromID("chk") : .Text = "1"c
                        End If
                    End If
                End With

                Me.txtSBldno.Text = ""
                Me.ntxtBldno.Focus()
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
        
    End Sub

    Private Sub ntxtBldno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ntxtBldno.Click
        ntxtBldno.SelectAll()
    End Sub

    ' 하단 혈액번호 스캔 이벤트
    Private Sub ntxtBldno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ntxtBldno.KeyDown
        Dim ls_Bldno As String
        Dim li_FRow As Integer
        Dim ls_Chk As String
        Dim ls_keepelapsdm As String
        Dim lb_Continue As Boolean

        If e.KeyCode = Keys.Enter Then
            If spdOrderList.MaxRows < 1 Then Return

            ls_Bldno = ntxtBldno.Text

            If ls_Bldno.Length() < 10 Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "올바른 혈액번호가 아닙니다.")
                ntxtBldno.Focus()
                ntxtBldno.SelectAll()
            Else
                li_FRow = Fn.SpdColSearch(spdOutList, ls_Bldno, spdOutList.GetColFromID("bldno"))

                If li_FRow <> 0 Then
                    With spdOutList
                        .Row = li_FRow
                        .Col = .GetColFromID("chk") : ls_Chk = .Text
                        .Col = .GetColFromID("keepelapsdm") : ls_keepelapsdm = .Text

                        If ls_keepelapsdm = "" Then ls_keepelapsdm = "0"c

                        If CLng(ls_keepelapsdm) > CInt(PRG_CONST.BLD_OUT_TIME) Then
                            lb_Continue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, " 출고 후 " + PRG_CONST.BLD_OUT_TIME + "분이 경과된 혈액입니다." + vbCrLf + "선택 하시겠습니까?")

                            If lb_Continue = False Then
                                ntxtBldno.Text = ""
                                ntxtBldno.Focus()
                                Return
                            End If
                        End If

                        .Col = .GetColFromID("chk")

                        If ls_Chk = "" Then ls_Chk = "0"c

                        If ls_Chk = "0" Then
                            .Text = "1"c
                        Else
                            .Text = "0"c
                        End If

                        ntxtBldno.Text = ""
                    End With
                ElseIf li_FRow = 0 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "일치하는 혈액번호를 찾을 수 없습니다.")
                    ntxtBldno.Focus()
                    ntxtBldno.SelectAll()
                End If

            End If
        Else
            Return

        End If
    End Sub

    ' 스프레드 선택 이벤트 막음
    Private Sub spdOutList_ButtonClicked(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdOutList.ButtonClicked
        Dim ls_Filter As String
        Dim ls_chk As String
        Dim ls_bldno As String = ntxtBldno.Text
        Dim ls_sbldno As String = txtSBldno.Text.Replace("-"c, "")
        Dim ls_cbldno As String

        With spdOutList
            .Row = e.row
            .Col = .GetColFromID("bldno") : ls_cbldno = .Text
            .Col = .GetColFromID("filter") : ls_Filter = .Text
            .Col = .GetColFromID("chk") : ls_chk = .Text

            If ls_chk = "1"c And ls_Filter <> "○"c Then
                If ls_bldno = ls_cbldno Then
                    Return
                ElseIf ls_bldno <> ls_sbldno And ls_bldno = "" Then
                    Return
                Else
                    .Text = ""
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "혈액번호를 마우스로 선택 할 수 없습니다.")
                End If

            End If
        End With

        Me.Focus()
        ntxtBldno.Focus()
        ntxtBldno.SelectAll()
    End Sub

    ' 반납 / 폐기 처리
    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        If Me.spdOrderList.MaxRows < 1 Then Return
        If Me.spdOutList.MaxRows < 1 Then Return

        Try
            If Me.txtRtnreqid.Text = "" Or Me.txtRtnreqnm.Text = "" Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰자를 입력해 주세요.")
                Return
            End If

            Dim objHelp As New FGB10_S01
            Dim iChkCnt As Integer = 0
            Dim bContinue As Boolean = False
            Dim bOk As Boolean = False
            Dim sChk As String = ""
            Dim alRtnInfo As New ArrayList
            Dim alArg As New ArrayList
            Dim sMs As String = ""
            Dim iGbn As Integer = 0

            If Me.rdoRtn.Checked = True Then
                sMs = " '반납' "
                iGbn = 0
            ElseIf rdoAbn.Checked = True Then
                sMs = " '폐기' "
                iGbn = 1
            End If

            With Me.spdOutList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1"c Then
                        .Col = .GetColFromID("rtnrsncd") : Dim sRtnrsncd As String = Ctrl.Get_Code(.Text)

                        If sRtnrsncd = "" Then
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "반납/폐기 사유를 선택하세요.")
                            Return
                        End If
                        iChkCnt += 1

                    End If
                Next

                If iChkCnt < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택한 항목이 없습니다.")
                    Return
                End If

                bContinue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "선택된 혈액을" + sMs + "처리 하시겠습니까?")

                If bContinue = False Then Return

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1"c Then
                        Dim stuRtnInfo As New STU_TnsJubsu

                        .Col = .GetColFromID("tnsjubsuno") : stuRtnInfo.TNSJUBSUNO = .Text
                        .Col = .GetColFromID("comcd") : stuRtnInfo.COMCD = .Text
                        .Col = .GetColFromID("comcd_out") : stuRtnInfo.COMCD_OUT = .Text
                        .Col = .GetColFromID("comordcd") : stuRtnInfo.COMORDCD = .Text
                        .Col = .GetColFromID("iogbn") : stuRtnInfo.IOGBN = .Text
                        .Col = .GetColFromID("owngbn") : stuRtnInfo.OWNGBN = .Text
                        .Col = .GetColFromID("fkocs") : stuRtnInfo.FKOCS = .Text
                        .Col = .GetColFromID("bldno") : stuRtnInfo.BLDNO = .Text
                        .Col = .GetColFromID("regno") : stuRtnInfo.REGNO = .Text
                        .Col = .GetColFromID("rtnreqid") : stuRtnInfo.RTNREQID = .Text
                        .Col = .GetColFromID("rtnreqnm") : stuRtnInfo.RTNREQNM = .Text
                        .Col = .GetColFromID("rtnrsncd") : stuRtnInfo.RTNRSNCD = Ctrl.Get_Code(.Text)
                        .Col = .GetColFromID("rtnrsncmt") : stuRtnInfo.RTNRSNCMT = .Text

                        stuRtnInfo.TEMP01 = IIf(Me.chkCost.Checked, "1", "0").ToString

                        If stuRtnInfo.RTNREQID = "" Or stuRtnInfo.RTNREQNM = "" Then
                            stuRtnInfo.RTNREQID = Me.txtRtnreqid.Text
                            stuRtnInfo.RTNREQNM = Me.txtRtnreqnm.Text
                        End If

                        alArg.Add(stuRtnInfo)
                    End If
                Next

                If Me.rdoRtn.Checked = True Then
                    'bOk = (New Rtn).fnExe_Rtn(alArg, "R"c)
                    bOk = (New WEBSERVER.CGWEB_B).ExecuteDo_Bld_Rtn(alArg, "R"c)
                Else
                    'bOk = (New Rtn).fnExe_Rtn(alArg, "A"c)
                    bOk = (New WEBSERVER.CGWEB_B).ExecuteDo_Bld_Rtn(alArg, "A"c)
                End If

            End With

            If bOk = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 되었습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "반납/폐기 작업중 오류가 발생 하였습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub

    Private Sub btnExeCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.spdOrderList.MaxRows < 1 Then Return

        If USER_SKILL.Authority("B01", 8) = False Then
            MsgBox("혈액은행의 [반납/폐기 취소 기능]의 권한이 없습니다!!")
            Return
        End If

        Try
            Dim objHelp As New FGB10_S01
            Dim iChkCnt As Integer = 0
            Dim bContinue As Boolean = False
            Dim bOk As Boolean = False
            Dim sChk As String = ""
            Dim alArg As New ArrayList
            Dim sMs As String = ""
            Dim sOrddt As String = ""
            Dim sFilter As String = ""
            Dim sRegno As String = ""
            Dim sSpccd As String = ""
            Dim sGuBun As String = ""

            With Me.spdOrderList
                .Row = .ActiveRow
                .Col = .GetColFromID("order_date") : sOrddt = .Text
                .Col = .GetColFromID("filter") : sFilter = .Text
                .Col = .GetColFromID("regno") : sRegno = .Text
                .Col = .GetColFromID("spccd") : sSpccd = .Text
            End With

            If Me.rdoRtn.Checked = True Then
                sMs = " '반납 취소' "
            ElseIf rdoAbn.Checked = True Then
                sMs = " '폐기 취소' "
            End If

            With Me.spdRtnList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1"c Then
                        Dim lcls_Rtn As New STU_TnsJubsu

                        iChkCnt += 1

                    End If
                Next

                If iChkCnt < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "선택한 항목이 없습니다.")
                    Return
                End If

                bContinue = CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "선택된 혈액을" + sMs + "처리 하시겠습니까?")

                If bContinue = False Then Return

                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("chk") : sChk = .Text

                    If sChk = "1"c Then
                        Dim stuRtnInfo As New STU_TnsJubsu

                        .Col = .GetColFromID("tnsjubsuno") : stuRtnInfo.TNSJUBSUNO = .Text
                        .Col = .GetColFromID("comcd") : stuRtnInfo.COMCD = .Text
                        .Col = .GetColFromID("comcdo") : stuRtnInfo.COMCD_OUT = .Text
                        .Col = .GetColFromID("iogbn") : stuRtnInfo.IOGBN = .Text
                        .Col = .GetColFromID("owngbn") : stuRtnInfo.OWNGBN = .Text
                        .Col = .GetColFromID("fkocs") : stuRtnInfo.FKOCS = .Text
                        .Col = .GetColFromID("vbldno") : stuRtnInfo.BLDNO = .Text.Replace("-", "")
                        .Col = .GetColFromID("rtndt") : stuRtnInfo.RTNDT = .Text.Replace("-", "").Replace(":", "").Replace(" ", "")
                        .Col = .GetColFromID("gubun") : sGuBun = IIf(.Text = "반납", "R", "A").ToString


                        stuRtnInfo.REGNO = sRegno
                        stuRtnInfo.FILTER = sFilter
                        stuRtnInfo.ORDDATE = sOrddt
                        stuRtnInfo.SPCCD = sSpccd

                        alArg.Add(stuRtnInfo)
                    End If
                Next

                bOk = (New Rtn).fnExe_Rtn_Cancel(alArg, sGuBun)


            End With

            If bOk = True Then
                'CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 처리 되었습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "반납/폐기 취소 작업중 오류가 발생 하였습니다.")
                sb_DisPlaySubData(spdOrderList.ActiveRow)
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub spdOutList_ComboSelChange(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ComboSelChangeEvent) Handles spdOutList.ComboSelChange

        With spdOutList
            If e.col <> .GetColFromID("rtnrsncd") Then Return

            .Row = e.row
            .Col = .GetColFromID("rtnrsncd") : Dim sRtnRsnCd As String = .Text
            .Col = .GetColFromID("rtnrsncmt") : .Text = Ctrl.Get_Name(sRtnRsnCd)
        End With
    End Sub

    Private Sub txtRtnreqid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRtnreqid.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Me.txtRtnreqnm.Text = CDHELP.FGCDHELPFN.fn_RtnDoc(Me.txtRtnreqid.Text)

        If Me.txtRtnreqnm.Text.Length() < 1 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "의뢰의사 아이디가 정확하지 않습니다.")
            Me.txtRtnreqid.Focus()
            Me.txtRtnreqid.SelectAll()
        End If

    End Sub

    Private Sub btnRtnreqPop_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRtnreqPop.Click
        Dim sFn As String = "Private Sub btnPop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPatPop.Click"
        Dim objHelp As New CDHELP.FGCDHELP99
        Dim al_Header As New ArrayList
        Dim al_Arg As New ArrayList
        Dim i_RtnCnt As Integer = 3
        Dim al_Rtn As New ArrayList

        Try
            al_Header.Add("아이디")
            al_Header.Add("유저명")
            al_Header.Add("진료과")

            al_Arg.Add(" "c)
            al_Rtn = objHelp.fn_DisplayPop(Me, "혈액반납폐기 ", "fn_PopGetRtnReqList", al_Arg, al_Header, i_RtnCnt, "")

            If al_Rtn.Count > 0 Then
                Me.txtRtnreqid.Text = al_Rtn(0).ToString
                Me.txtRtnreqnm.Text = al_Rtn(1).ToString
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try
    End Sub
End Class