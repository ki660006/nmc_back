'>>> 수혈 의뢰 접수

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_BT

Public Class FGB06
    Private mobjDAF As New LISAPP.APP_F_COMCD
    Public mdt_BldSubData As DataTable
    Public mbAutoQuery As Boolean
    Public mbUnConfirmAlarm As Boolean
    Private mAlramWaveFile As String = "\Wave\TNS_Alarm.wav"
    Private mAlComfirmkey As New ArrayList

    Private Class clsSelRow
        Public SRow As Integer
        Public ERow As Integer
    End Class

    Private Sub FGB06_NEW_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB06_NEW_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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

    Private Sub FGB06_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        Me.txtRegno.MaxLength = PRG_CONST.Len_RegNo

        ' 화면 오픈시 초기화
        Me.spdWorkList.MaxRows = 0
        Me.spdKeepList.MaxRows = 0
        Me.spdPastTns.MaxRows = 0

        Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).AddDays(-1)
        Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

        ' 스프레드 헤더 색상 및 로우선택 색상 설정
        DS_FormDesige.sbInti(Me)

        sb_SetComboDt()

    End Sub

    Public Sub sb_SetComboDt(Optional ByVal rsUsDt As String = "", Optional ByVal rsUeDt As String = "")
        Dim sFn As String = "sb_SetComboDt"
        ' 콤보 데이터 생성
        Try

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Com_List("", "")

            Me.cboComCd.Items.Clear()
            Me.cboComCd.Items.Add("[     ] 전체")
            If dt.Rows.Count > 0 Then
                With Me.cboComCd
                    For i As Integer = 0 To dt.Rows.Count - 1
                        .Items.Add("[" + dt.Rows(i).Item("comcd").ToString.Trim + "] " + dt.Rows(i).Item("comnmd").ToString.Trim)
                    Next
                End With
            End If

            Me.cboTnsGbn.Items.Clear()
            Me.cboTnsGbn.Items.Add("[ ] 전체")
            Me.cboTnsGbn.Items.Add("[1] 준비(Prep.)")
            Me.cboTnsGbn.Items.Add("[2] 수혈(Tranf.)")
            Me.cboTnsGbn.Items.Add("[3] 교차미필(Emer.)")
            Me.cboTnsGbn.Items.Add("[4] Irradiation.)")

            Me.cboTnsGbn.Text = "[ ] 전체"

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Me.Close()

    End Sub

    Private Sub FGB06_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.cboFilter.SelectedIndex = 0
        Me.txtRegno.Focus()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dt As New DataTable
        Dim ls_Comcd As String
        Dim ls_TnsGbn As String

        spdWorkList.MaxRows = 0
        spdPastTns.MaxRows = 0
        spdKeepList.MaxRows = 0

        'With spdWorkList
        '    .Col = .GetColFromID("order_date") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("hope_date") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("bunho") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("patnm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("sexage") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("deptnm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("doctornm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("comgbn") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        'End With

        ls_Comcd = Ctrl.Get_Code(cboComCd)
        ls_TnsGbn = Ctrl.Get_Code(cboTnsGbn)

        Try
            If rdoNoJub.Checked Then
                '미접수 조회
                dt = CGDA_BT.fn_TransfusionSelectN(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), Me.txtRegno.Text, ls_Comcd, ls_TnsGbn)

                ' 미접수 데이터 트리 하위 데이터 테이블
                mdt_BldSubData = CGDA_BT.fn_TransfusionSelectNT(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), Me.txtRegno.Text, ls_Comcd, ls_TnsGbn)

                sb_DisplayDataList("N", dt)

            ElseIf rdoJubsu.Checked Then
                Dim dr() As DataRow
                Dim sSql As String = ""

                Select Case Ctrl.Get_Code(Me.cboFilter)
                    Case "0" : sSql = "qrygbn = '0'"
                    Case "1" : sSql = "qrygbn = '1'"
                End Select

                '접수 조회
                ' 접수 데이터 트리 상위 데이터 테이블
                dt = CGDA_BT.fn_TransfusionSelectJ(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), Me.txtRegno.Text, ls_Comcd, ls_TnsGbn)
                ' 접수 데이터 트리 하위 데이터 테이블
                mdt_BldSubData = CGDA_BT.fn_TransfusionSelectT(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), Me.txtRegno.Text, ls_Comcd, ls_TnsGbn)

                If sSql <> "" Then
                    dr = dt.Select(sSql, "tnsjubsuno, treelv")
                    dt = Fn.ChangeToDataTable(dr)

                    dr = mdt_BldSubData.Select(sSql, "tnsjubsuno, treelv")
                    mdt_BldSubData = Fn.ChangeToDataTable(dr)
                End If

                sb_DisplayDataList("Y", dt)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sb_DisplayDataList(ByVal rsGbn As String, ByVal rdt As DataTable)
        Dim sFn As String = "sb_DisplayDataList(ByVal rsGbn As String, ByVal rdt As DataTable)"
        Dim dtSysDate As Date = Fn.GetServerDateTime()

        If rsGbn = "N" Then
            '미접수 리스트
            Dim ls_ckey As String
            Try
                With Me.spdWorkList
                    .ReDraw = False
                    For i As Integer = 0 To rdt.Rows.Count - 1
                        .MaxRows += 1
                        .Row = .MaxRows

                        .Col = .GetColFromID("treelv") : .Text = rdt.Rows(i).Item("treelv").ToString.Trim

                        .Col = .GetColFromID("treechk") : .Text = "+"c
                        .Col = .GetColFromID("subchk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture

                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)
                        .TypePictCenter = True

                        ls_ckey = rdt.Rows(i).Item("bunho").ToString.Trim + rdt.Rows(i).Item("order_date").ToString.Trim + rdt.Rows(i).Item("comcd").ToString.Trim

                        ' 확인한 접수 내역 처리 start
                        Dim li_chkcnt As Integer = 0

                        For k As Integer = 0 To mAlComfirmkey.Count - 1
                            If mAlComfirmkey(k).ToString = ls_ckey Then
                                li_chkcnt += 1
                            End If
                        Next

                        If li_chkcnt > 0 Then
                            .Col = .GetColFromID("confirm") : .Text = "○"c
                        End If
                        ' 확인한 접수 내역 처리 end

                        Dim sPatInfo() As String = rdt.Rows(i).Item("patinfo").ToString.Split("|"c)
                        '< 나이계산
                        Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                        Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                        If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                        '>
                       
                        .Col = .GetColFromID("order_date") : .Text = rdt.Rows(i).Item("order_date").ToString.Trim
                        .Col = .GetColFromID("hope_date") : .Text = rdt.Rows(i).Item("hope_date").ToString.Trim
                        .Col = .GetColFromID("bunho") : .Text = rdt.Rows(i).Item("bunho").ToString.Trim
                        .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                        .Col = .GetColFromID("sexage") : .Text = sPatInfo(1).Trim + "/" + iAge.ToString
                        .Col = .GetColFromID("deptnm") : .Text = rdt.Rows(i).Item("deptnm").ToString.Trim
                        .Col = .GetColFromID("doctornm") : .Text = rdt.Rows(i).Item("doctornm").ToString.Trim
                        .Col = .GetColFromID("wardroom") : .Text = rdt.Rows(i).Item("wardroom").ToString.Trim
                        .Col = .GetColFromID("comgbn") : .Text = rdt.Rows(i).Item("comgbn").ToString.Trim
                        .Col = .GetColFromID("comcd") : .Text = rdt.Rows(i).Item("comcd").ToString.Trim
                        .Col = .GetColFromID("spccd") : .Text = rdt.Rows(i).Item("spccd").ToString.Trim
                        .Col = .GetColFromID("comnmd") : .Text = rdt.Rows(i).Item("comnmd").ToString.Trim
                        .Col = .GetColFromID("owngbn") : .Text = rdt.Rows(i).Item("owngbn").ToString.Trim
                        .Col = .GetColFromID("iogbn") : .Text = rdt.Rows(i).Item("iogbn").ToString.Trim
                        .Col = .GetColFromID("qty") : .Text = rdt.Rows(i).Item("qty").ToString.Trim
                        .Col = .GetColFromID("state") : .Text = rdt.Rows(i).Item("state").ToString.Trim
                        .Col = .GetColFromID("diagnm") : .Text = rdt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("sunabyn") : .Text = rdt.Rows(i).Item("sunabyn").ToString.Trim

                        If rdt.Rows(i).Item("sunabyn").ToString.Trim = "Y" Then
                            .ForeColor = Color.Red
                        Else
                            .ForeColor = Color.Black
                        End If

                        .Col = .GetColFromID("diagnm") : .Text = rdt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("eryn") : .Text = rdt.Rows(i).Item("eryn").ToString.Trim
                        .Col = .GetColFromID("irryn") : .Text = rdt.Rows(i).Item("irryn").ToString.Trim
                        .Col = .GetColFromID("ftyn") : .Text = rdt.Rows(i).Item("ftyn").ToString.Trim
                        .Col = .GetColFromID("rmk") : .Text = rdt.Rows(i).Item("rmk").ToString.Trim
                        .Col = .GetColFromID("comordcd") : .Text = rdt.Rows(i).Item("comordcd").ToString.Trim
                        .Col = .GetColFromID("comcd_out") : .Text = rdt.Rows(i).Item("comcd_out").ToString.Trim
                        .Col = .GetColFromID("treesortkey") : .Text = rdt.Rows(i).Item("treesortkey").ToString.Trim


                        .Col = .GetColFromID("fkocs") : .Text = rdt.Rows(i).Item("fkocs").ToString.Trim

                        Select Case rdt.Rows(i).Item("comgbn").ToString.Trim
                            Case "실시"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .BackColor = Color.DarkGreen
                                .ForeColor = Color.White
                                .BlockMode = False
                            Case "수혈"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .ForeColor = Color.Blue
                                .BlockMode = False
                            Case "X-M 수혈"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .BackColor = Color.Blue
                                .ForeColor = Color.White
                                .BlockMode = False
                            Case "응급"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .BackColor = Color.Red
                                .ForeColor = Color.White
                                .BlockMode = False
                        End Select
                    Next
                End With

                sb_SetStBarSearchCnt(rdt.Rows.Count)

            Catch ex As Exception
                Me.spdWorkList.ReDraw = True
                fn_PopMsg(Me, "E"c, ex.Message)
                Return
            Finally
                Me.spdWorkList.ReDraw = True
            End Try
        Else
            '접수 리스트
            Try
                With Me.spdWorkList
                    .ReDraw = False
                    For i As Integer = 0 To rdt.Rows.Count - 1
                        .MaxRows += 1
                        .Row = .MaxRows

                        .Col = .GetColFromID("treelv") : .Text = rdt.Rows(i).Item("treelv").ToString.Trim

                        .Col = .GetColFromID("treechk") : .Text = "+"c
                        .Col = .GetColFromID("subchk")
                        .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture

                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)
                        .TypePictCenter = True

               
                        .Col = .GetColFromID("tnsjubsuno") : .Text = rdt.Rows(i).Item("tnsjubsuno").ToString.Trim
                        .Col = .GetColFromID("rmk") : .Text = rdt.Rows(i).Item("rmk").ToString.Trim
                        .Col = .GetColFromID("order_date") : .Text = rdt.Rows(i).Item("order_date").ToString.Trim

                        .Col = .GetColFromID("hope_date") : .Text = rdt.Rows(i).Item("hope_date").ToString.Trim

                        .Col = .GetColFromID("bunho") : .Text = rdt.Rows(i).Item("bunho").ToString.Trim
                        .Col = .GetColFromID("patnm") : .Text = rdt.Rows(i).Item("patnm").ToString.Trim
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("sexage") : .Text = rdt.Rows(i).Item("sexage").ToString.Trim

                        .Col = .GetColFromID("deptnm") : .Text = rdt.Rows(i).Item("deptnm").ToString.Trim

                        .Col = .GetColFromID("doctornm") : .Text = rdt.Rows(i).Item("doctornm").ToString.Trim
                        .Col = .GetColFromID("wardroom") : .Text = rdt.Rows(i).Item("wardroom").ToString.Trim

                        .Col = .GetColFromID("comgbn") : .Text = rdt.Rows(i).Item("comgbn").ToString.Trim

                        .Col = .GetColFromID("comcd") : .Text = rdt.Rows(i).Item("comcd").ToString.Trim
                        .Col = .GetColFromID("spccd") : .Text = rdt.Rows(i).Item("spccd").ToString.Trim
                        .Col = .GetColFromID("comnmd") : .Text = rdt.Rows(i).Item("comnmd").ToString.Trim
                        .Col = .GetColFromID("owngbn") : .Text = rdt.Rows(i).Item("owngbn").ToString.Trim
                        .Col = .GetColFromID("iogbn") : .Text = rdt.Rows(i).Item("iogbn").ToString.Trim
                        .Col = .GetColFromID("qty") : .Text = rdt.Rows(i).Item("qty").ToString.Trim
                        .Col = .GetColFromID("state") : .Text = rdt.Rows(i).Item("state").ToString.Trim
                        .Col = .GetColFromID("diagnm") : .Text = rdt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("sunabyn") : .Text = rdt.Rows(i).Item("sunabyn").ToString.Trim
                        .Col = .GetColFromID("diagnm") : .Text = rdt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("eryn") : .Text = rdt.Rows(i).Item("eryn").ToString.Trim
                        .Col = .GetColFromID("irryn") : .Text = rdt.Rows(i).Item("irryn").ToString.Trim
                        .Col = .GetColFromID("ftyn") : .Text = rdt.Rows(i).Item("ftyn").ToString.Trim
                        .Col = .GetColFromID("fkocs") : .Text = rdt.Rows(i).Item("fkocs").ToString.Trim
                        .Col = .GetColFromID("statecd") : .Text = rdt.Rows(i).Item("statecd").ToString.Trim
                        .Col = .GetColFromID("bldno") : .Text = rdt.Rows(i).Item("bldno").ToString.Trim
                        .Col = .GetColFromID("comordcd") : .Text = rdt.Rows(i).Item("comordcd").ToString.Trim
                        .Col = .GetColFromID("comcd_out") : .Text = rdt.Rows(i).Item("comcd_out").ToString.Trim

                        Select Case rdt.Rows(i).Item("comgbn").ToString.Trim
                            Case "실시"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .BackColor = Color.DarkGreen
                                .ForeColor = Color.White
                                .BlockMode = False
                            Case "수혈"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .ForeColor = Color.Blue
                                .BlockMode = False
                            Case "X-M 수혈"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .BackColor = Color.Blue
                                .ForeColor = Color.White
                                .BlockMode = False
                            Case "응급"
                                .BlockMode = True
                                .Row = .Row
                                .Row2 = .Row
                                .Col = .GetColFromID("comgbn")
                                .Col2 = .GetColFromID("comgbn")
                                .BackColor = Color.Red
                                .ForeColor = Color.White
                                .BlockMode = False
                        End Select

                        If rdt.Rows(i).Item("overtime").ToString = "1" Then
                            .Col = .GetColFromID("state")
                            .BackColor = Color.Red
                            .ForeColor = Color.White
                        End If

                        If rdt.Rows(i).Item("state").ToString.Trim = "취소" Then
                            .Row = .MaxRows
                            .Col = .GetColFromID("treelv") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            .Col = .GetColFromID("treechk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                            .Row = .MaxRows : .Row2 = .MaxRows
                            .Col = 1 : .Col2 = .MaxCols
                            .BlockMode = True

                            .BackColor = Color.FromArgb(240, 240, 240)
                            .ForeColor = Color.FromArgb(235, 0, 120)

                            .FontStrikethru = True
                            .BlockMode = False
                        End If
                    Next
                End With

                sb_SetStBarSearchCnt(rdt.Rows.Count)

            Catch ex As Exception
                fn_PopMsg(Me, "E"c, ex.Message)
            Finally
                Me.spdWorkList.ReDraw = True
            End Try
        End If
    End Sub

    Private Sub spdWorkList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdWorkList.ButtonClicked
        Dim ls_Regno As String
        Dim ls_OrderDate As String
        Dim ls_TreeChk As String
        Dim ls_TreeLv As String
        Dim ls_TnsNum As String
        Dim ls_chk As String
        'Dim test As String = ""


        ' 체크 버튼 선택시 전체 선택 혹은 전체 해제
        With spdWorkList
            .Row = e.row
            .Col = .GetColFromID("bunho") : ls_Regno = .Text
            .Col = .GetColFromID("order_date") : ls_OrderDate = .Text

            If e.col = .GetColFromID("chk") Then
                .Row = e.row
                .Col = .GetColFromID("chk") : ls_chk = .Value
                .Col = .GetColFromID("treechk") : ls_TreeChk = .Text
                .Col = .GetColFromID("treelv") : ls_TreeLv = .Text

                ' 접수일경우 수혈의뢰 접수 번호 미접수시 sortkey
                If rdoJubsu.Checked = True Then
                    .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                Else
                    .Col = .GetColFromID("treesortkey") : ls_TnsNum = .Text
                End If

                '.Col = .GetColFromID("eryn") : If .Text.Trim <> "" Then test = "er"
                '.Col = .GetColFromID("irryn") : If .Text.Trim <> "" Then test = "irr"
                '.Col = .GetColFromID("ftyn") : If .Text.Trim <> "" Then test = "ft"

                If ls_TreeChk = "+"c And ls_TreeLv = "1"c Then
                    ' 트리가 펼쳐지지 않은경우 트리 생성 후 체크
                    If ls_chk = "1"c Then
                        .Col = .GetColFromID("treechk") : .Text = "-"c
                        .Col = .GetColFromID("subchk")

                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Minus)
                        .TypePictCenter = True

                        If rdoJubsu.Checked = True Then
                            sb_AddTreeChildData(mdt_BldSubData, ls_TnsNum, True)
                        Else
                            sb_AddTreeChildDataN(mdt_BldSubData, ls_TnsNum, True) ', test)
                        End If

                    End If
                ElseIf ls_TreeChk = "-"c And ls_TreeLv = "1"c Then
                    ' 트리가 펼쳐진 경우 체크 해제 혹은 체크
                    .Col = .GetColFromID("subchk") : .TypePictCenter = True
                    sb_chkTreeChildData(ls_TnsNum, ls_chk) ', test)
                End If
            End If
        End With
    End Sub

    Private Sub spdWorkList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdWorkList.ClickEvent

        'If e.col = spdWorkList.GetColFromID("treechk") Or e.col = spdWorkList.GetColFromID("subchk") Then Return

        If e.row = 0 Then Return 'JJH 컬럼 클릭시 return

        Try
            Dim ls_TreeChk As String = ""
            Dim ls_TreeLv As String = ""
            Dim ls_TnsNum As String = ""
            Dim ls_chk As String = ""
            Dim ls_Regno As String = ""
            Dim ls_OrderDate As String = ""
            Dim ls_Comcd As String = ""
            Dim ls_ckey As String = ""
            Dim li_chkKey As Integer
            Dim dt As DataTable
            Dim ls_date As String = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")
            'Dim test As String = ""

            With spdWorkList
                .ReDraw = False

                .Row = e.row
                .Col = .GetColFromID("bunho") : ls_Regno = .Text
                .Col = .GetColFromID("order_date") : ls_OrderDate = .Text
                .Col = .GetColFromID("chk") : ls_chk = .Text
                .Col = .GetColFromID("comcd") : ls_Comcd = .Text

                ls_ckey = ls_Regno + ls_OrderDate + ls_Comcd
                ls_OrderDate = ls_OrderDate.Replace("-"c, "").Substring(0, 8)

                ' 트리 기능 구현
                If e.col = .GetColFromID("subchk") Then
                    .Row = e.row
                    .Col = .GetColFromID("treechk") : ls_TreeChk = .Text
                    .Col = .GetColFromID("treelv") : ls_TreeLv = .Text

                    If rdoJubsu.Checked Then
                        .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                    Else
                        .Col = .GetColFromID("treesortkey") : ls_TnsNum = .Text
                    End If


                    If ls_TreeChk = "+"c And ls_TreeLv = "1"c Then
                        .Col = .GetColFromID("treechk") : .Text = "-"c
                        .Col = .GetColFromID("subchk")
                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Minus)
                        .TypePictCenter = True

                        '.Col = .GetColFromID("eryn") : If .Text.Trim <> "" Then test = "er"
                        '.Col = .GetColFromID("irryn") : If .Text.Trim <> "" Then test = "irr"
                        '.Col = .GetColFromID("ftyn") : If .Text.Trim <> "" Then test = "ft"

                        If rdoJubsu.Checked = True Then
                            ' 동일한 수혈의뢰 접수 번호 아이템 리스트 추가
                            If ls_chk = "1"c Then
                                sb_AddTreeChildData(mdt_BldSubData, ls_TnsNum, True)
                            Else
                                sb_AddTreeChildData(mdt_BldSubData, ls_TnsNum, False)
                            End If
                        Else
                            ' 동일한 처방 아이템 리스트 추가
                            If ls_chk = "1"c Then
                                sb_AddTreeChildDataN(mdt_BldSubData, ls_TnsNum, True)
                            Else
                                sb_AddTreeChildDataN(mdt_BldSubData, ls_TnsNum, False) ', test)
                            End If
                        End If

                    ElseIf ls_TreeChk = "-"c And ls_TreeLv = "1"c Then
                        .Col = .GetColFromID("treechk") : .Text = "+"c
                        .Col = .GetColFromID("subchk")
                        .TypePictPicture = GetImgList.getPlusMinus(enumPlusMinus.Plus)
                        .TypePictCenter = True

                        '.Col = .GetColFromID("order_date") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
                        '.Col = .GetColFromID("hope_date") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
                        '.Col = .GetColFromID("bunho") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
                        '.Col = .GetColFromID("patnm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
                        '.Col = .GetColFromID("sexage") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
                        '.Col = .GetColFromID("deptnm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
                        '.Col = .GetColFromID("doctornm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
                        '.Col = .GetColFromID("comgbn") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone

                        ' 트리 접기
                        sb_DeleteTreeChildData(ls_TnsNum)
                    End If

                    .ReDraw = True

                ElseIf e.col = .GetColFromID("confirm") Then
                    .Col = .GetColFromID("confirm") : .Text = "○"

                    If mAlComfirmkey.Count < 1 Then
                        mAlComfirmkey.Add(ls_ckey)
                    Else
                        li_chkKey = 0

                        For i As Integer = 0 To mAlComfirmkey.Count - 1
                            If mAlComfirmkey(i).ToString <> ls_ckey Then
                                li_chkKey += 1
                            End If
                        Next

                        If li_chkKey > 0 Then
                            mAlComfirmkey.Add(ls_ckey)
                        End If
                    End If

                End If
            End With

            ' 환자 정보 조회
            axPatInfo.sb_setPatinfo(ls_Regno, ls_OrderDate, IIf(rdoJubsu.Checked, ls_TnsNum, "").ToString)

            spdPastTns.MaxRows = 0
            ' 과거수혈내역조회
            dt = CGDA_BT.fn_GetPastTnsList(ls_Regno, ls_date)
            sb_DisplayPastList(dt)

            spdKeepList.MaxRows = 0
            ' 보관검체정보조회
            dt = CGDA_BT.fn_GetKeepSpcList(ls_Regno)
            sb_DisplayKeepSpcList(dt)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 보관검체정보조회
    Private Sub sb_DisplayKeepSpcList(ByVal r_dt As DataTable)
        If r_Dt.Rows.Count < 1 Then Return

        Try
            With Me.spdKeepList
                .MaxRows = 0
                .ReDraw = False
                For ix As Integer = 0 To r_Dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows
                    Dim Testcd$ = r_dt.Rows(ix).Item("testcd").ToString

                    .Col = .GetColFromID("keepplace") : .Text = r_Dt.Rows(ix).Item("keepplace").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_Dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("tkdt") : .Text = r_dt.Rows(ix).Item("tkdt").ToString.Trim '2019-04-22 요청사항으로 채혈일시에서 접수일시로 보이게 수정
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim

                    '< JJH abo
                    .Col = .GetColFromID("abo") : .Text = r_dt.Rows(ix).Item("abo").ToString.Trim
                    If Testcd = "LB110" Or Testcd = "LB111" Or Testcd = "LB112" Then
                        .BackColor = Color.LightSalmon
                    End If
                    ' >

                    '< JJH rh
                    .Col = .GetColFromID("rh") : .Text = r_dt.Rows(ix).Item("rh").ToString.Trim
                    If Testcd = "LB110" Or Testcd = "LB111" Or Testcd = "LB112" Then
                        .BackColor = Color.LightSalmon
                    End If
                    ' >

                    '< JJH cross
                    .Col = .GetColFromID("crossm") : .Text = r_dt.Rows(ix).Item("crossm").ToString.Trim
                    If Testcd = "LB142" Then
                        .BackColor = Color.LightBlue
                    End If
                    ' >

                    .Col = .GetColFromID("irr") : .Text = r_dt.Rows(ix).Item("irr").ToString.Trim

                    '< JJH abscreen
                    .Col = .GetColFromID("abscreen") : If .Col > 0 Then .Text = r_dt.Rows(ix).Item("abscreen").ToString.Trim
                    If Testcd = "LB151" Then
                        .BackColor = Color.LightGreen
                    End If
                    ' >

                Next
            End With
        Catch ex As Exception
            Me.spdKeepList.ReDraw = True
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdKeepList.ReDraw = True
        End Try
    End Sub

    ' 과거수혈내역조회
    Private Sub sb_DisplayPastList(ByVal r_dt As DataTable)
        Dim sFn As String = "Private Sub sb_DisplayPastList(ByVal rDt As DataTable)"
        If r_dt.Rows.Count < 1 Then Return

        Try
            With Me.spdPastTns
                .MaxRows = 0
                .ReDraw = False
                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("tnsjubsuno") : .Text = r_dt.Rows(ix).Item("tnsjubsuno").ToString.Trim
                    .Col = .GetColFromID("tnsgbn") : .Text = r_dt.Rows(ix).Item("tnsgbn").ToString.Trim
                    .Col = .GetColFromID("comnm") : .Text = r_dt.Rows(ix).Item("comnm").ToString.Trim
                    .Col = .GetColFromID("reqqnt") : .Text = r_dt.Rows(ix).Item("reqqnt").ToString.Trim
                    .Col = .GetColFromID("outqnt") : .Text = r_dt.Rows(ix).Item("outqnt").ToString.Trim
                    .Col = .GetColFromID("rtnqnt") : .Text = r_dt.Rows(ix).Item("rtnqnt").ToString.Trim
                    .Col = .GetColFromID("abnqnt") : .Text = r_dt.Rows(ix).Item("abnqnt").ToString.Trim
                    .Col = .GetColFromID("cancelqnt") : .Text = r_dt.Rows(ix).Item("cancelqnt").ToString.Trim
                Next
            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdPastTns.ReDraw = True
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdWorkList.MaxRows = 0
        Me.spdPastTns.MaxRows = 0
        Me.spdKeepList.MaxRows = 0
        Me.axPatInfo.sb_ClearLbl()

        'With spdWorkList
        '    .Col = .GetColFromID("order_date") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("hope_date") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("bunho") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("patnm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("sexage") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("deptnm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("doctornm") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        '    .Col = .GetColFromID("comgbn") : .ColMerge = FPSpreadADO.MergeConstants.MergeNone
        'End With

    End Sub

    Private Sub txtRegno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegno.Click
        Me.txtRegno.SelectAll()
    End Sub

    Private Sub txtRegno_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim ls_Regno As String = ""
            Dim ls_OrderDate As String = ""
            Dim ls_TnsNum As String = ""

            Dim dt As DataTable
            Dim la_getValue As New ArrayList
            ' 등록번호 입력시 이벤트
            ls_Regno = txtRegno.Text

            If ls_Regno.Length() < 1 Then
                txtPatNm.Text = ""
                Return
            End If

            If IsNumeric(ls_Regno) Then
                If ls_Regno.Length() < PRG_CONST.Len_RegNo Then
                    ls_Regno = ls_Regno.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                End If
            Else
                If ls_Regno.Length() < PRG_CONST.Len_RegNo Then
                    ls_Regno = ls_Regno.Substring(0, 1) + ls_Regno.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If
            End If

            txtRegno.Text = ls_Regno

            dt = CGDA_BT.fn_GetPatInfo(ls_Regno)

            la_getValue = fn_GetSelectItem(dt, 1)

            txtPatNm.Text = la_getValue(0).ToString

            btnSearch_Click(Nothing, Nothing)

            Me.txtRegno.Text = "" : txtPatNm.Text = ""
            If Me.spdWorkList.MaxRows < 1 Then Return

            With Me.spdWorkList
                .Row = 1
                .Col = .GetColFromID("order_date") : ls_OrderDate = .Text
                .Col = .GetColFromID("order_date") : ls_OrderDate = .Text

                If Me.rdoJubsu.Checked Then
                    .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text.Replace("-", "")
                End If

            End With

            ' 환자정보 디스플레이
            axPatInfo.sb_setPatinfo(ls_Regno, ls_OrderDate, ls_TnsNum)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
        End Try


    End Sub


    Private Sub rdoNoJub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoNoJub.Click
        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Me.cboFilter.Visible = False
            Me.lblDate.Text = "처방일자"
            With Me.spdWorkList
                .MaxRows = 0
                .Col = .GetColFromID("bldno") : .ColHidden = True
                .Col = .GetColFromID("tnsjubsuno") : .ColHidden = True
            End With
            Me.btnExecute.Text = "접   수(F7)"
            Me.btnSearch_Click(Nothing, Nothing)
        Catch ex As Exception
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
        
    End Sub

    Private Sub rdoJubsu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoJubsu.Click
        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Me.cboFilter.Visible = True
            Me.lblDate.Text = "접수일자"

            With Me.spdWorkList
                .MaxRows = 0
                .Col = .GetColFromID("bldno") : .ColHidden = False : .set_ColWidth(.GetColFromID("bldno"), 11)
                .Col = .GetColFromID("tnsjubsuno") : .ColHidden = False : .set_ColWidth(.GetColFromID("tnsjubsuno"), 14)
            End With

            Me.btnExecute.Text = "취   소(F7)"
            Me.btnSearch_Click(Nothing, Nothing)
        Catch ex As Exception
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
        
    End Sub

    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click
        Dim sFn As String = "Handles btnExecute.Click"

        Dim bAutoSearch As Boolean = False

        Try
            '접수, 취소 처리
            Dim al_TnsInfo As New ArrayList

            Dim bError As Boolean = False
            Dim sTreesortkey As String = ""

            If tmrReq.Enabled = True Then
                '자동 조회중인 경우
                tmrReq.Enabled = False      ' 접수중 자동조회 일시중지
                tmrAlarm.Enabled = False    ' 접수중 자동알림 일시중지

                bAutoSearch = True
            End If

            If rdoNoJub.Checked = True Then
                If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "접수 처리 하시겠습니까?") = False Then Return

                ' 접수 처리
                With spdWorkList
                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("subchk") : Dim schk As String = .Text
                        .Col = .GetColFromID("iogbn") : Dim sIoGbn As String = .Text
                        .Col = .GetColFromID("sunabyn") : Dim ssunab As String = .Text

                        Dim stuJubsu As New STU_TnsJubsu

                        If schk = "1" Then
                            '-- 병원 정책으로 미수납도 처리 가능
                            'If Not (ls_IoGbn = "I"c Or ls_IoGbn = "E"c) Then
                            '    If ls_sunab <> "Y"c Then
                            '        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "외래 환자의 경우 수납 후 접수 가능 합니다.")
                            '        Return
                            '    End If
                            'End If
                            ''수납 chk end

                            ' 접수를 위한 데이터 생성 
                            .Col = .GetColFromID("bunho") : stuJubsu.REGNO = .Text
                            .Col = .GetColFromID("patnm") : stuJubsu.PATNM = .Text
                            .Col = .GetColFromID("sexage") : stuJubsu.SEX = .Text.Substring(0, 1)
                            .Col = .GetColFromID("sexage") : stuJubsu.AGE = .Text.Substring(2)
                            .Col = .GetColFromID("order_date") : stuJubsu.ORDDATE = .Text.Replace("-", "").Substring(0, 8)
                            .Col = .GetColFromID("comcd") : stuJubsu.COMCD = .Text
                            .Col = .GetColFromID("comordcd") : stuJubsu.COMORDCD = .Text
                            .Col = .GetColFromID("comcd_out") : stuJubsu.COMCD_OUT = .Text
                            .Col = .GetColFromID("spccd") : stuJubsu.SPCCD = .Text
                            .Col = .GetColFromID("owngbn") : stuJubsu.OWNGBN = .Text
                            .Col = .GetColFromID("iogbn") : stuJubsu.IOGBN = .Text
                            .Col = .GetColFromID("fkocs") : stuJubsu.FKOCS = .Text
                            .Col = .GetColFromID("treesortkey") : stuJubsu.TEMP01 = .Text
                            .Col = .GetColFromID("treesortkey") : stuJubsu.DEPTCD = .Text.Split("|"c)(2)
                            .Col = .GetColFromID("treesortkey") : stuJubsu.DRCD = .Text.Split("|"c)(3)
                            .Col = .GetColFromID("treesortkey") : stuJubsu.WARDCD = .Text.Split("|"c)(4)
                            .Col = .GetColFromID("treesortkey") : stuJubsu.ROOMNO = .Text.Split("|"c)(5)

                        End If

                        If al_TnsInfo.Count > 0 And sTreesortkey <> stuJubsu.TEMP01 Then

                            If (New JubSu).fn_RegTnsJubsuData(al_TnsInfo) = False Then bError = True

                            al_TnsInfo.Clear()
                        End If

                        If stuJubsu.FKOCS <> "0" And stuJubsu.FKOCS <> "" Then al_TnsInfo.Add(stuJubsu)

                        sTreesortkey = stuJubsu.TEMP01
                    Next

                    If al_TnsInfo.Count > 0 Then
                        If (New JubSu).fn_RegTnsJubsuData(al_TnsInfo) = False Then bError = True
                    End If

                    If bError Then CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "접수 처리중 오류가 발생 하였습니다.")
                End With

            ElseIf rdoJubsu.Checked = True Then
                If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "취소 처리 하시겠습니까?") = False Then Return

                ' 취소 처리
                Dim iBefOutCnt As Integer = 0
                Dim iOutCnt As Integer = 0
                Dim iRtnCnt As Integer = 0

                With spdWorkList
                    For i As Integer = 0 To .MaxRows
                        .Row = i
                        .Col = .GetColFromID("subchk") : Dim sChk As String = .Text

                        Dim stuJubsu As New STU_TnsJubsu

                        If sChk = "1" Then
                            .Col = .GetColFromID("statecd") : Dim sState As String = .Text

                            If sState = "3"c Then                        ' 가출고 자료 체크
                                iBefOutCnt += 1
                            ElseIf sState = "4"c Then                    ' 출고 체크
                                iOutCnt += 1
                            ElseIf sState = "5"c Or sState = "6"c Then  ' 반납/폐기 체크
                                iRtnCnt += 1
                            End If

                            .Col = .GetColFromID("tnsjubsuno") : stuJubsu.TNSJUBSUNO = .Text.Replace("-", "")
                            .Col = .GetColFromID("comcd") : stuJubsu.COMCD = .Text
                            .Col = .GetColFromID("comordcd") : stuJubsu.COMORDCD = .Text
                            .Col = .GetColFromID("comcd_out") : stuJubsu.COMCD_OUT = .Text
                            .Col = .GetColFromID("owngbn") : stuJubsu.OWNGBN = .Text
                            .Col = .GetColFromID("iogbn") : stuJubsu.IOGBN = .Text
                            .Col = .GetColFromID("fkocs") : stuJubsu.FKOCS = .Text
                            .Col = .GetColFromID("bldno") : stuJubsu.BLDNO = .Text.Replace("-", "")
                            .Col = .GetColFromID("statecd") : stuJubsu.STATE = .Text
                            .Col = .GetColFromID("bunho") : stuJubsu.REGNO = .Text
                            .Col = .GetColFromID("order_date") : stuJubsu.ORDDATE = .Text.Replace("-", "").Substring(0, 8)
                            .Col = .GetColFromID("spccd") : stuJubsu.SPCCD = .Text

                            If CGDA_BT.fnGet_TnsJubsuState(stuJubsu.TNSJUBSUNO, stuJubsu.COMCD, stuJubsu.STATE, stuJubsu.FKOCS) = False Then
                                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "데이타가 변경 되었습니다.  다시 조회한 후에 처리 해 주세요.!!")
                                Return
                            End If

                            If al_TnsInfo.Count > 0 And sTreesortkey <> stuJubsu.TNSJUBSUNO Then

                                If iOutCnt > 0 Then
                                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "취소선택 자료중 반납/폐기된 자료가 선택되었습니다.")
                                    Return
                                End If

                                If iRtnCnt > 0 Then
                                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고된 항목을 선택시 취소 작업을 할 수 없습니다.")
                                    Return
                                End If

                                If iBefOutCnt > 0 Then
                                    If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "가출고된 항목이 포함되어있습니다. 취소작업을 진행 하시겠습니까?") = False Then Return
                                End If

                                If (New JubSu).fn_CntTnsJubsuData(al_TnsInfo) = False Then bError = True

                                al_TnsInfo.Clear()
                                iBefOutCnt = 0
                                iOutCnt = 0
                                iRtnCnt = 0
                            End If

                            If stuJubsu.FKOCS <> "0" Then al_TnsInfo.Add(stuJubsu)
                        End If

                        sTreesortkey = stuJubsu.TNSJUBSUNO

                    Next

                    If al_TnsInfo.Count > 0 Then
                        If iOutCnt > 0 Then
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "취소선택 자료중 반납/폐기된 자료가 선택되었습니다.")
                            Return
                        End If

                        If iRtnCnt > 0 Then
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고된 항목을 선택시 취소 작업을 할 수 없습니다.")
                            Return
                        End If

                        If iBefOutCnt > 0 Then
                            If CDHELP.FGCDHELPFN.fn_PopConfirm(Me, "I"c, "가출고된 항목이 포함되어있습니다. 취소작업을 진행 하시겠습니까?") = False Then Return
                        End If

                        If (New JubSu).fn_CntTnsJubsuData(al_TnsInfo) = False Then bError = True

                    End If

                    If bError Then CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "접수 취소 처리중 오류가 발생 하였습니다.")
                End With
            End If

            btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            If bAutoSearch Then
                '자동 조회중인 경우
                tmrReq.Enabled = True      ' 접수중 자동조회 일시중지
                tmrAlarm.Enabled = True    ' 접수중 자동알림 일시중지
            End If
        End Try
    End Sub

    Private Sub sb_AddTreeChildDataN(ByVal r_dt As DataTable, ByVal rsSortkey As String, ByVal rbChk As Boolean) ', Optional ByVal gbn As String = "")
        ' 트리의 하위 데이터를 펼쳐주는 역할
        Dim ls_Sortkey As String = ""
        Dim dtSysDate As Date = Fn.GetServerDateTime()
        'Dim test As String = ""


        Try

            Dim a_dr As DataRow() = r_dt.Select("treesortkey= '" + rsSortkey + "'", "")


            Dim dt As DataTable = Fn.ChangeToDataTable(a_dr)


            With Me.spdWorkList
                .ReDraw = False
                For i As Integer = 0 To dt.Rows.Count - 1
                    ls_Sortkey = dt.Rows(i).Item("treesortkey").ToString.Trim

                    'If dt.Rows(i).Item("eryn").ToString.Trim <> "" Then test = "er"
                    'If dt.Rows(i).Item("irryn").ToString.Trim <> "" Then test = "irr"
                    'If dt.Rows(i).Item("ftyn").ToString.Trim <> "" Then test = "ft"


                    'If ls_Sortkey = rsSortkey And test = gbn Then
                    If ls_Sortkey = rsSortkey Then

                        Dim sPatInfo() As String = dt.Rows(i).Item("patinfo").ToString.Split("|"c)
                        '< 나이계산
                        Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                        Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                        If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                        '>

                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("treelv") : .Text = dt.Rows(i).Item("treelv").ToString.Trim
                        .Col = .GetColFromID("treechk") : .Text = "-"c
                        .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                        If rbChk = True Then
                            .Col = .GetColFromID("subchk") : .Text = "1"c
                        End If

                        'If test <> "" Then
                        '.Col = .GetColFromID("treesortkey") : .Text = dt.Rows(i).Item("treesortkey").ToString.Trim + test
                        'Else
                        .Col = .GetColFromID("treesortkey") : .Text = dt.Rows(i).Item("treesortkey").ToString.Trim
                        'End If

                       
                        .Col = .GetColFromID("rmk") : .Text = dt.Rows(i).Item("rmk").ToString.Trim
                        .Col = .GetColFromID("order_date") : .Text = dt.Rows(i).Item("order_date").ToString.Trim : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("hope_date") : .Text = dt.Rows(i).Item("hope_date").ToString.Trim : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("bunho") : .Text = dt.Rows(i).Item("bunho").ToString.Trim : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("sexage") : .Text = sPatInfo(1).Trim + "/" + iAge.ToString : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("deptnm") : .Text = dt.Rows(i).Item("deptnm").ToString.Trim : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("doctornm") : .Text = dt.Rows(i).Item("doctornm").ToString.Trim : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                        .Col = .GetColFromID("comgbn") : .Text = dt.Rows(i).Item("comgbn").ToString.Trim : .ForeColor = Color.White
                        '.ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                        .Col = .GetColFromID("comcd") : .Text = dt.Rows(i).Item("comcd").ToString.Trim
                        .Col = .GetColFromID("spccd") : .Text = dt.Rows(i).Item("spccd").ToString.Trim
                        .Col = .GetColFromID("comnmd") : .Text = dt.Rows(i).Item("comnmd").ToString.Trim
                        .Col = .GetColFromID("owngbn") : .Text = dt.Rows(i).Item("owngbn").ToString.Trim
                        .Col = .GetColFromID("iogbn") : .Text = dt.Rows(i).Item("iogbn").ToString.Trim
                        .Col = .GetColFromID("qty") : .Text = dt.Rows(i).Item("qty").ToString.Trim
                        .Col = .GetColFromID("state") : .Text = dt.Rows(i).Item("state").ToString.Trim
                        .Col = .GetColFromID("diagnm") : .Text = dt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("sunabyn") : .Text = dt.Rows(i).Item("sunabyn").ToString.Trim
                        .Col = .GetColFromID("diagnm") : .Text = dt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("eryn") : .Text = dt.Rows(i).Item("eryn").ToString.Trim
                        .Col = .GetColFromID("irryn") : .Text = dt.Rows(i).Item("irryn").ToString.Trim
                        .Col = .GetColFromID("ftyn") : .Text = dt.Rows(i).Item("ftyn").ToString.Trim
                        .Col = .GetColFromID("fkocs") : .Text = dt.Rows(i).Item("fkocs").ToString.Trim
                        .Col = .GetColFromID("comordcd") : .Text = dt.Rows(i).Item("comordcd").ToString.Trim
                        .Col = .GetColFromID("comcd_out") : .Text = dt.Rows(i).Item("comcd_out").ToString.Trim
                    End If
                Next

                ' 다중 Sort를 위한 설정
                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .SortBy = FPSpreadADO.SortByConstants.SortByRow
                .set_SortKey(1, .GetColFromID("treesortkey"))
                .set_SortKey(2, .GetColFromID("treelv"))
                .set_SortKey(3, .GetColFromID("fkocs"))
                .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(3, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .Action = FPSpreadADO.ActionConstants.ActionSort
                .BlockMode = False

            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdWorkList.ReDraw = True
        End Try
    End Sub


    Private Sub sb_AddTreeChildData(ByVal r_dt As DataTable, ByVal rsTnsNum As String, ByVal rbChk As Boolean)
        ' 트리의 하위 데이터를 펼쳐주는 역할
        Dim ls_TnsNum As String
        Dim ls_state As String

        Try
            Dim a_dr As DataRow() = r_dt.Select("tnsjubsuno= '" + rsTnsNum + "'", "")
            Dim dt As DataTable = Fn.ChangeToDataTable(a_dr)

            With Me.spdWorkList
                .ReDraw = False
                For i As Integer = 0 To dt.Rows.Count - 1
                    ls_TnsNum = dt.Rows(i).Item("tnsjubsuno").ToString.Trim

                    If ls_TnsNum = rsTnsNum Then
                        .MaxRows += 1
                        .Row = .MaxRows
                        .Col = .GetColFromID("treelv") : .Text = dt.Rows(i).Item("treelv").ToString.Trim
                        .Col = .GetColFromID("treechk") : .Text = "-"c
                        .Col = .GetColFromID("chk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        ls_state = dt.Rows(i).Item("statecd").ToString.Trim

                        If ls_state = "0"c Or ls_state = "1"c Or ls_state = "2"c Or ls_state = "3"c Then
                            If rbChk = True Then
                                .Col = .GetColFromID("subchk") : .Text = "1"c
                            End If
                        Else
                            .Col = .GetColFromID("subchk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                        End If

                        .Col = .GetColFromID("tnsjubsuno") : .Text = dt.Rows(i).Item("tnsjubsuno").ToString.Trim : .ForeColor = Color.White
                        .Col = .GetColFromID("rmk") : .Text = dt.Rows(i).Item("rmk").ToString.Trim
                        .Col = .GetColFromID("order_date") : .Text = dt.Rows(i).Item("order_date").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("hope_date") : .Text = dt.Rows(i).Item("hope_date").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("bunho") : .Text = dt.Rows(i).Item("bunho").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("patnm") : .Text = dt.Rows(i).Item("patnm").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("sexage") : .Text = dt.Rows(i).Item("sexage").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("deptnm") : .Text = dt.Rows(i).Item("deptnm").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("doctornm") : .Text = dt.Rows(i).Item("doctornm").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("comgbn") : .Text = dt.Rows(i).Item("comgbn").ToString.Trim : .ForeColor = Color.White

                        .Col = .GetColFromID("comcd") : .Text = dt.Rows(i).Item("comcd").ToString.Trim
                        .Col = .GetColFromID("spccd") : .Text = dt.Rows(i).Item("spccd").ToString.Trim
                        .Col = .GetColFromID("comnmd") : .Text = dt.Rows(i).Item("comnmd").ToString.Trim
                        .Col = .GetColFromID("owngbn") : .Text = dt.Rows(i).Item("owngbn").ToString.Trim
                        .Col = .GetColFromID("iogbn") : .Text = dt.Rows(i).Item("iogbn").ToString.Trim
                        .Col = .GetColFromID("qty") : .Text = dt.Rows(i).Item("qty").ToString.Trim
                        .Col = .GetColFromID("state") : .Text = dt.Rows(i).Item("state").ToString.Trim
                        .Col = .GetColFromID("diagnm") : .Text = dt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("sunabyn") : .Text = dt.Rows(i).Item("sunabyn").ToString.Trim
                        .Col = .GetColFromID("diagnm") : .Text = dt.Rows(i).Item("diagnm").ToString.Trim
                        .Col = .GetColFromID("eryn") : .Text = dt.Rows(i).Item("eryn").ToString.Trim
                        .Col = .GetColFromID("irryn") : .Text = dt.Rows(i).Item("irryn").ToString.Trim
                        .Col = .GetColFromID("ftyn") : .Text = dt.Rows(i).Item("ftyn").ToString.Trim
                        .Col = .GetColFromID("fkocs") : .Text = dt.Rows(i).Item("fkocs").ToString.Trim
                        .Col = .GetColFromID("statecd") : .Text = dt.Rows(i).Item("statecd").ToString.Trim
                        .Col = .GetColFromID("bldno") : .Text = dt.Rows(i).Item("bldno").ToString.Trim
                        .Col = .GetColFromID("comordcd") : .Text = dt.Rows(i).Item("comordcd").ToString.Trim
                        .Col = .GetColFromID("comcd_out") : .Text = dt.Rows(i).Item("comcd_out").ToString.Trim

                        If dt.Rows(i).Item("state").ToString.Trim = "취소" Then
                            .Row = .MaxRows
                            .Col = .GetColFromID("treelv") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                            .Col = .GetColFromID("treechk") : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText

                            .Row = .MaxRows : .Row2 = .MaxRows
                            .Col = 1 : .Col2 = .MaxCols
                            .BlockMode = True

                            .BackColor = Color.FromArgb(240, 240, 240)
                            .ForeColor = Color.FromArgb(235, 0, 120)

                            .FontStrikethru = True
                            .BlockMode = False
                        End If
                    End If
                Next

                ' 다중 Sort를 위한 설정
                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .SortBy = FPSpreadADO.SortByConstants.SortByRow
                .set_SortKey(1, .GetColFromID("tnsjubsuno"))
                .set_SortKey(2, .GetColFromID("treelv"))
                .set_SortKey(3, .GetColFromID("fkocs"))
                .set_SortKeyOrder(1, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(2, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .set_SortKeyOrder(3, FPSpreadADO.SortKeyOrderConstants.SortKeyOrderAscending)
                .Action = FPSpreadADO.ActionConstants.ActionSort
                .BlockMode = False

            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdWorkList.ReDraw = True
        End Try
    End Sub

    Private Sub sb_DeleteTreeChildData(ByVal rsTnsNum As String)
        ' 트리의 하위 자료를 삭제 하는 역할
        Dim ls_TnsNum As String
        Dim ls_TreeLv As String

        Try
            With Me.spdWorkList
                .ReDraw = False
                For i As Integer = .MaxRows To 0 Step -1
                    .Row = i

                    ' 접수일 경우 수혈의뢰 접수 번호 미접수 일경우 같은 처방 묶음으로 처리
                    If rdoJubsu.Checked = True Then
                        .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text
                    Else
                        .Col = .GetColFromID("treesortkey") : ls_TnsNum = .Text
                    End If

                    .Col = .GetColFromID("treelv") : ls_TreeLv = .Text

                    ' 수혈접수번호가 같지 않거나 트리 레벨이 1이 아닐경우에 스킵
                    If ls_TnsNum <> rsTnsNum Or ls_TreeLv = "1"c Then
                        Continue For
                    End If

                    .DeleteRows(i, 1)
                    .MaxRows += -1

                Next

                .ReDraw = True

            End With

        Catch ex As Exception
            fn_PopMsg (Me, "E"c, ex.Message )
        Finally
            Me.spdWorkList.ReDraw = True
        End Try
    End Sub

    Private Sub sb_chkTreeChildData(ByVal rsTnsNum As String, ByVal rsChk As String) ', ByVal gbn As String)
        ' 트리의 하위 자료를 체크혹은 체크 해제 역할
        Dim ls_TnsNum As String
        Dim ls_TreeLv As String
        Dim ls_state As String
        'Dim test As String = ""

        Try
            With Me.spdWorkList
                .ReDraw = False
                For i As Integer = .MaxRows To 0 Step -1
                    .Row = i
                    If rdoJubsu.Checked = True Then
                        .Col = .GetColFromID("tnsjubsuno") : ls_TnsNum = .Text.Replace("-", "")
                    Else
                        .Col = .GetColFromID("treesortkey") : ls_TnsNum = .Text
                    End If

                    .Col = .GetColFromID("treelv") : ls_TreeLv = .Text
                    .Col = .GetColFromID("statecd") : ls_state = .Text

                    '.Col = .GetColFromID("eryn") : If .Text.Trim <> "" Then test = "er"
                    '.Col = .GetColFromID("irryn") : If .Text.Trim <> "" Then test = "irr"
                    '.Col = .GetColFromID("ftyn") : If .Text.Trim <> "" Then test = "ft"


                    ' 수혈접수번호가 같지 않거나 트리 레벨이 1인 경우에 스킵
                    'If (test <> gbn And ls_TnsNum <> rsTnsNum) Or ls_TreeLv = "1"c Then
                    If ls_TnsNum <> rsTnsNum Or ls_TreeLv = "1"c Then
                        Continue For
                    End If

                    If rdoJubsu.Checked = True Then
                        ' 접수 또는 가출고 상태인 경우에만 체크 되도록
                        If ls_state = "1"c Or ls_state = "2"c Or ls_state = "3"c Then
                            .Col = .GetColFromID("subchk") : .Text = rsChk
                        End If
                    Else
                        .Col = .GetColFromID("subchk") : .Text = rsChk
                    End If

                Next

                .ReDraw = True

            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdWorkList.ReDraw = True
        End Try
    End Sub

    Private Sub lblAutoQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblAutoQuery.Click

        Dim objButton As Windows.Forms.Label = CType(sender, Windows.Forms.Label)
        Dim strTag As String = CType(objButton.Tag, String)

        Try
            If mbAutoQuery = False Then
                ' 자동조회 On 설정
                With lblAutoQuery
                    .Text = "자동조회 ON"
                    .BackColor = System.Drawing.Color.FromArgb(179, 232, 147)
                    .ForeColor = System.Drawing.Color.FromArgb(0, 64, 0)
                End With
                mbAutoQuery = True

                ' 미확인 알림 On 설정
                With lblUnConfirmAlarm
                    mbUnConfirmAlarm = False
                    .Enabled = True
                    lblUnConfirmAlarm_Click(lblUnConfirmAlarm, Nothing)
                End With

                ' 자동조회초 조회
                If IsNumeric(txtAutoSearchSec.Text) Then
                    tmrReq.Interval = CInt(txtAutoSearchSec.Text) * 1000
                End If

                ' 자동조회 타이머 동작
                tmrReq.Enabled = True

                txtAutoSearchSec.Enabled = True
                'fnFormClear(0)

            Else
                ' 자동조회 Off 설정
                With lblAutoQuery
                    .Text = "자동조회 OFF"
                    .BackColor = System.Drawing.SystemColors.Control
                    .ForeColor = System.Drawing.SystemColors.ControlText
                End With
                mbAutoQuery = False

                ' 미확인 알림 Off 설정
                With lblUnConfirmAlarm
                    mbUnConfirmAlarm = True
                    .Enabled = False
                    lblUnConfirmAlarm_Click(lblUnConfirmAlarm, Nothing)
                End With

                ' 자동조회 타이머 동작
                tmrReq.Enabled = False


                txtAutoSearchSec.Enabled = False
            End If

            ' 미접수 선택
            rdoNoJub.Checked = True
            btnSearch_Click(Nothing, Nothing)

            ' 자동조회는 처음에 조회
            If mbAutoQuery = True Then btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            fn_PopMsg (Me, "E"c, ex.Message )

        End Try
    End Sub

    Private Sub lblUnConfirmAlarm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblUnConfirmAlarm.Click
        Dim objButton As Windows.Forms.Label = CType(sender, Windows.Forms.Label)
        Dim strTag As String = CType(objButton.Tag, String)
        Dim intTmp As Integer = 0

        Try
            If mbUnConfirmAlarm = False Then
                With lblUnConfirmAlarm
                    .Text = "미확인 알림 ON"
                    .BackColor = System.Drawing.Color.FromArgb(211, 193, 240)
                    .ForeColor = System.Drawing.Color.FromArgb(64, 64, 64)
                End With
                mbUnConfirmAlarm = True

                ' 자동조회초 조회
                If IsNumeric(txtUnCfmAlarmSec.Text) = True Then
                    tmrAlarm.Interval = CInt(txtUnCfmAlarmSec.Text) * 1000
                End If

                ' 미확인알림 타이머 동작
                tmrAlarm.Enabled = True

                txtUnCfmAlarmSec.Enabled = True
            Else
                With lblUnConfirmAlarm
                    .Text = "미확인 알림 OFF"
                    .BackColor = System.Drawing.SystemColors.Control
                    .ForeColor = System.Drawing.SystemColors.ControlText
                End With
                mbUnConfirmAlarm = False

                ' 미확인알림 타이머 중지
                tmrAlarm.Enabled = False

                txtUnCfmAlarmSec.Enabled = False
            End If

        Catch ex As Exception
            fn_PopMsg (Me, "E"c, ex.Message )

        End Try
    End Sub

    ' 자동조회 타이머
    Private Sub tmrReq_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrReq.Tick

        Try
            'tmrReq.Enabled = False
            Debug.WriteLine("R  :" & Now.ToLongTimeString)

            Application.DoEvents()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            fn_PopMsg (Me, "E"c, ex.Message )

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub tmrAlarm_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrAlarm.Tick
        Dim objWave As New PlayWave
        Dim strWaveDir As String = Application.StartupPath & mAlramWaveFile

        Dim blnNoneChk As Boolean = False

        Try
            tmrAlarm.Enabled = False

            If spdWorkList.MaxRows > 0 Then
                ' 확인안된 수혈의뢰 체크
                With spdWorkList
                    For intRow As Integer = 1 To .MaxRows

                        .Row = intRow
                        .Col = .GetColFromID("subchk")

                        Dim sSubchk As String = CType(CType(.CellType, ContentAlignment), String)

                        If sSubchk = "9" Then
                            .Col = .GetColFromID("confirm")
                            If .Text.Trim = "" Then
                                blnNoneChk = True
                            End If
                        End If


                        'Debug.WriteLine(intRow)
                        'Dim objRow As clsSelRow = fnGetSelRow(intRow)

                        '.Row = objRow.SRow : .Col = .GetColFromID("confirm")
                        'If .Text.Trim = "" Then
                        '    blnNoneChk = True

                        'End If

                        'intRow = objRow.ERow
                    Next
                End With

                ' 확인안된 수혈의뢰가 있으면 알람울림
                If blnNoneChk = True Then
                    Debug.WriteLine("  A:" & Now.ToLongTimeString)
                    If Dir(strWaveDir) <> "" Then
                        Debug.WriteLine(strWaveDir)
                        objWave.Play(strWaveDir)
                    End If
                End If
            End If

        Catch ex As Exception
            fn_PopMsg (Me, "E"c, ex.Message )

        Finally
            tmrAlarm.Enabled = True

        End Try
    End Sub

    ' 선택항목의 수혈의뢰처방 처음, 마지막Row 찾기
    Private Function fnGetSelRow(ByVal aiRow As Integer) As clsSelRow
        Dim objRow As New clsSelRow

        Dim sGrpNo As String = ""
        Dim iRow As Integer = 0

        fnGetSelRow = objRow

        Try
            With spdWorkList
                .Row = aiRow : .Col = .GetColFromID("GRPNO")
                sGrpNo = .Text

                ' 초기 설정
                objRow.SRow = 1 ' 시작Row
                objRow.ERow = .MaxRows  ' 마지막Row

                ' 선택Row의 수혈의뢰처방 처음항목 Row찾기
                For intRow = aiRow To 1 Step -1
                    .Row = intRow : .Col = .GetColFromID("GRPNO")
                    If .Text <> sGrpNo Then
                        objRow.SRow = intRow + 1
                        Exit For
                    End If
                Next

                ' 선택Row의 수혈의뢰처방 마지막항목 Row찾기
                For intRow = aiRow To .MaxRows
                    .Row = intRow : .Col = .GetColFromID("GRPNO")
                    If .Text <> sGrpNo Then
                        objRow.ERow = intRow - 1
                        Exit For
                    End If
                Next

                fnGetSelRow = objRow
            End With

        Catch ex As Exception
            fn_PopMsg (Me, "E"c, ex.Message )

        End Try

    End Function

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
            fn_PopMsg (Me, "E"c, ex.Message )
        End Try

    End Sub

    Private Sub btnOrdbld_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrdbld.Click
        'Dim obj As New FGB06_S01
        Dim obj As New FGB06_S02
        obj.Show()
    End Sub

End Class
