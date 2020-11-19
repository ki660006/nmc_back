Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports CDHELP.FGCDHELPFN
Imports OCSAPP.OcsLink
Imports LISAPP.APP_BT



Public Class FGB06_S01


    Public MsRegNo As String


    Public Sub New(ByVal rsRegNo As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        MsRegNo = rsRegNo
        txtRegno.Text = MsRegNo

        Me.FGB06_S01vb_Load(Nothing, Nothing)

        txtRegno_KeyDown(Nothing, Nothing)
    End Sub
    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim ls_Comcd As String
            Dim ls_TnsGbn As String

            ls_Comcd = Ctrl.Get_Code(cboComCd)
            ls_TnsGbn = Ctrl.Get_Code(cboTnsGbn)


            Dim dt As DataTable = SData.fnGet_TnsOrdList(Me.dtpDate0.Value.ToString.Substring(0, 10).Replace("-", ""), Me.dtpDate1.Value.ToString.Substring(0, 10).Replace("-", ""), Me.txtRegno.Text, ls_Comcd, ls_TnsGbn)

            sbDisplayList(dt)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub sbDisplayList(ByVal rsDt As DataTable)
        Try

            Dim dtSysDate As Date = Fn.GetServerDateTime()

            With Me.spdList
                .MaxRows = rsDt.Rows.Count
                If rsDt.Rows.Count > 0 Then

                    For ix As Integer = 0 To rsDt.Rows.Count - 1
                        .Row = ix + 1
                        Dim sPatInfo() As String = rsDt.Rows(ix).Item("patinfo").ToString.Split("|"c)

                        Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                        Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                        .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                        .Col = .GetColFromID("sexage") : .Text = sPatInfo(1).Trim + "/" + iAge.ToString

                        .Col = .GetColFromID("orddt") : .Text = rsDt.Rows(ix).Item("orddt").ToString
                        .Col = .GetColFromID("regno") : .Text = rsDt.Rows(ix).Item("regno").ToString

                        .Col = .GetColFromID("deptnm") : .Text = rsDt.Rows(ix).Item("deptnm").ToString
                        .Col = .GetColFromID("doctor") : .Text = rsDt.Rows(ix).Item("doctor").ToString
                        .Col = .GetColFromID("wardroom") : .Text = rsDt.Rows(ix).Item("wardroom").ToString
                        .Col = .GetColFromID("gbn") : .Text = rsDt.Rows(ix).Item("gbn").ToString
                        .Col = .GetColFromID("comcdnm") : .Text = rsDt.Rows(ix).Item("comnmd").ToString
                        .Col = .GetColFromID("qty") : .Text = rsDt.Rows(ix).Item("qty").ToString
                        .Col = .GetColFromID("outqty") : .Text = rsDt.Rows(ix).Item("outqty").ToString
                        .Col = .GetColFromID("state") : .Text = rsDt.Rows(ix).Item("state").ToString
                        .Col = .GetColFromID("er") : .Text = rsDt.Rows(ix).Item("er").ToString
                        .Col = .GetColFromID("remark") : .Text = rsDt.Rows(ix).Item("remark").ToString
                        .Col = .GetColFromID("opstat") : .Text = rsDt.Rows(ix).Item("opstat").ToString
                        .Col = .GetColFromID("aborh") : .Text = rsDt.Rows(ix).Item("aborh").ToString
                        '.Col = .GetColFromID("tnsstrdt") : .Text = rsDt.Rows(ix).Item("TNSSTRDDTM").ToString '<< JJH 혈액불출요청시간

                        If rsDt.Rows(ix).Item("PREPPRCPFLAG").ToString = "Y" Then
                            .BlockMode = True
                            .Row = ix + 1 : .Row2 = ix + 1
                            .Col = 1 : .Col2 = .MaxCols
                            .BackColor = Color.AntiqueWhite
                            .BlockMode = False
                        Else
                            .BlockMode = True
                            .Row = ix + 1 : .Row2 = ix + 1
                            .Col = 1 : .Col2 = .MaxCols
                            .BackColor = Color.White
                            .BlockMode = False
                        End If

                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGB06_S01vb_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MsRegNo <> "" Then
            Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).AddDays(-2)
        Else
            Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-")).AddDays(-1)
        End If

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

    Private Sub txtRegno_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown

        If MsRegNo <> "" Then

        ElseIf e.KeyCode <> Keys.Enter And MsRegNo = "" Then
            Return
        End If

        'If e.KeyCode <> Keys.Enter And MsRegNo = "" Then Return

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

            Me.txtRegno.SelectAll()
            Me.txtRegno.Focus()



        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, ex.Message)
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
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sFc As String = "Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click"

        Try

            Dim sExcelFile As String = Format(Now, "yyyy-MM-dd") + "_수혈처방조회" + ".xls"

            With spdList

                .ReDraw = False

                .MaxRows += 1
                .InsertRows(1, 1)

                Dim sColHeaders As String = ""

                .Col = 1 : .Col2 = .MaxCols
                .Row = 0 : .Row2 = 0
                sColHeaders = .Clip

                .Col = 1 : .Col2 = .MaxCols
                .Row = 1 : .Row2 = 1
                .Clip = sColHeaders

                .ExportToExcel(sExcelFile, "수혈처방조회", "")

                Process.Start(sExcelFile)

                .DeleteRows(1, 1)
                .MaxRows -= 1

                .ReDraw = True
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class