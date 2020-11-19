
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst

Public Class FGF21

    Private mo_DAF As New LISAPP.APP_F_TEST
    Private msSpcmode As Integer = 0 '0 : 조회 , 1: 신규
    Private msFile As String = "FGF21"
    Private Const mcDevFrmBaseWidth As Integer = 990
    Private Const mcDevFrmBaseHeight As Integer = 656

    Private Sub btnRegS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegS.Click

        Me.txtHospiSpccd.Text = ""
        Me.txtSpcnm.Text = ""
        Me.txtSpdList.Text = ""
        msSpcmode = 1
        Me.btnSaveS.Text = "등록"
        Me.btnCloseS.Enabled = False
        Me.btnSearchS.Enabled = False

    End Sub
    '병원체별 검사항목
    Public Sub sbSearchRefTest()
        Try
            Dim dt As DataTable = mo_DAF.fnGetHospiRefTestList

            With Me.spdTestRefcd
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("refcd") : .Text = dt.Rows(ix).Item("refcd").ToString
                    .Col = .GetColFromID("refnm") : .Text = dt.Rows(ix).Item("refnm").ToString
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                Next

            End With


            Me.btnClose.Enabled = True
            Me.btnSearch.Enabled = True
            Me.btnSave.Text = "저장"
            msSpcmode = 0

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '병원체
    Public Sub sbSearchRef()
        Try
            Dim dt As DataTable = mo_DAF.fnGetHospiRefList

            With Me.spd
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("refcd") : .Text = dt.Rows(ix).Item("refcd").ToString
                    .Col = .GetColFromID("refnm") : .Text = dt.Rows(ix).Item("refnm").ToString
                    .Col = .GetColFromID("refnmd") : .Text = dt.Rows(ix).Item("refnmd").ToString
                    .Col = .GetColFromID("seq") : .Text = dt.Rows(ix).Item("seq").ToString
                    .Col = .GetColFromID("groupcd") : .Text = dt.Rows(ix).Item("groupcd").ToString

                Next

            End With


            Me.btnClose.Enabled = True
            Me.btnSearch.Enabled = True
            Me.btnSave.Text = "저장"
            msSpcmode = 0

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    ' 균별 병원체 코드 
    Public Sub sbSearchBaccdRefcd()
        Try
            Dim dt As DataTable = mo_DAF.fnGetBacRefcdList

            With Me.spdBaccd

                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("refcd") : .Text = dt.Rows(ix).Item("refcd").ToString
                    .Col = .GetColFromID("refnm") : .Text = dt.Rows(ix).Item("refnm").ToString
                    .Col = .GetColFromID("baccd") : .Text = dt.Rows(ix).Item("baccd").ToString
                    .Col = .GetColFromID("bacnm") : .Text = dt.Rows(ix).Item("bacnm").ToString

                Next

            End With

            Me.btnSaveB.Text = "저장"
            msSpcmode = 0
            Me.btnCloseB.Enabled = True
            Me.btnSearchB.Enabled = True
            Me.spdSpcList.MaxRows = 0

            sbSpdspcClick(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    '검사항목별 병원체 코드 
    Public Sub sbSearchTestRefcd()
        Try
            Dim dt As DataTable = mo_DAF.fnGetTestRefcdList

            With Me.spdTestRef

                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("refcd") : .Text = dt.Rows(ix).Item("refcd").ToString
                    .Col = .GetColFromID("refnm") : .Text = dt.Rows(ix).Item("refnm").ToString
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                    .Col = .GetColFromID("testnm") : .Text = dt.Rows(ix).Item("tnm").ToString

                Next

            End With

            Me.btnSaveS.Text = "저장"
            msSpcmode = 0
            Me.btnCloseS.Enabled = True
            Me.btnSearchS.Enabled = True
            Me.spdSpcList.MaxRows = 0

            sbSpdspcClick(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    '검체
    Public Sub sbSearchSpc()
        Try
            Dim dt As DataTable = mo_DAF.fnGetHospiSpcList

            With Me.spdSpc
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("hspccd") : .Text = dt.Rows(ix).Item("refcd").ToString
                    .Col = .GetColFromID("spcnm") : .Text = dt.Rows(ix).Item("refnm").ToString
                    .Col = .GetColFromID("spclist") : .Text = dt.Rows(ix).Item("spccd").ToString

                Next

            End With

            Me.btnSaveS.Text = "저장"
            msSpcmode = 0
            Me.btnCloseS.Enabled = True
            Me.btnSearchS.Enabled = True
            Me.spdSpcList.MaxRows = 0

            sbSpdspcClick(0)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    '검사방법
    Public Sub sbSearchTest()
        Try
            Dim dt As DataTable = mo_DAF.fnGetHospiRstExList

            With Me.spdRstEx
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("rstcd") : .Text = dt.Rows(ix).Item("rstcd").ToString
                    .Col = .GetColFromID("rstex") : .Text = dt.Rows(ix).Item("rstex").ToString

                Next

            End With

            Me.btnSaveR.Text = "저장"
            msSpcmode = 0
            Me.btnCloseR.Enabled = True
            Me.btnSearchR.Enabled = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveS.Click

        'msSpcmode = 1:신규 , 0:수정 
        Dim bRet As Boolean = mo_DAF.fnRegSpcList(msSpcmode.ToString, Me.txtHospiSpccd.Text.Trim, Me.txtSpcnm.Text, Me.txtSpdList.Text.Trim)

        If bRet Then
            MsgBox("등록 되었습니다.")
        Else
            MsgBox("등록 실패")
        End If

        sbSearchSpc()
        Me.btnSaveS.Text = "저장"
    End Sub

    Private Sub btnSearchS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchS.Click
        sbSearchSpc()
    End Sub

    Private Sub TabHospi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabRstEx.Click

    End Sub

    Private Sub spdSpc_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdSpc.ClickEvent
        With Me.spdSpc

            If .MaxRows > 1 Then
                .Row = e.row
                .Col = .GetColFromID("hspccd") : Me.txtHospiSpccd.Text = .Text
                .Col = .GetColFromID("spcnm") : Me.txtSpcnm.Text = .Text
                .Col = .GetColFromID("spclist") : Me.txtSpdList.Text = .Text
            End If
            
        End With

        If Me.txtSpdList.Text <> "" Then
            If Me.txtSpdList.Text.IndexOf(",") > 0 Then
                Dim sSpcList As String() = Me.txtSpdList.Text.Split(","c)

                With Me.spdSpcList
                    .MaxRows = sSpcList.Length
                    For ix As Integer = 0 To sSpcList.Length - 1
                        .Row = ix + 1
                        .Col = .GetColFromID("spccd") : .Text = sSpcList(ix)
                        .Col = .GetColFromID("spcnm") : .Text = mo_DAF.fnGetSpcnm(sSpcList(ix))

                    Next
                End With
            Else

                With Me.spdSpcList
                    .MaxRows = 1
                    .Row = 1
                    .Col = .GetColFromID("spccd") : .Text = Me.txtSpdList.Text
                    .Col = .GetColFromID("spcnm") : .Text = mo_DAF.fnGetSpcnm(Me.txtSpdList.Text)

                End With
            End If
        Else
            With Me.spdSpcList
                .MaxRows = 0
            End With
        End If
    End Sub
    Private Sub sbSpdspcClick(ByVal riRow As Integer)

        With Me.spdSpc
            .Row = riRow
            .Col = .GetColFromID("hspccd") : Me.txtHospiSpccd.Text = .Text
            .Col = .GetColFromID("spcnm") : Me.txtSpcnm.Text = .Text
            .Col = .GetColFromID("spclist") : Me.txtSpdList.Text = .Text
        End With

        If Me.txtSpdList.Text <> "" Then
            If Me.txtSpdList.Text.IndexOf(",") > 0 Then
                Dim sSpcList As String() = Me.txtSpdList.Text.Split(","c)

                With Me.spdSpcList
                    .MaxRows = sSpcList.Length
                    For ix As Integer = 0 To sSpcList.Length - 1
                        .Row = ix + 1
                        .Col = .GetColFromID("spccd") : .Text = sSpcList(ix)
                        .Col = .GetColFromID("spcnm") : .Text = mo_DAF.fnGetSpcnm(sSpcList(ix))

                    Next
                End With
            End If
        Else
            With Me.spdSpcList
                .MaxRows = 0
            End With
        End If
    End Sub

    Private Sub FGF21_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated


    End Sub

    Private Sub FGF21_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DS_FormDesige.sbInti(Me)

        sbSearchRef() '병원체 코드
        sbSearchSpc() '검체정보
        sbSearchTest() '검사방법
        sbDisplay_Slip() '검사항목별 병원체코드에서 슬립구분 
        sbSearchTestRefcd() '검사항목별 병원체코드 리스트
        sbSearchBaccdRefcd() '균코드별 병원체 코드 리스트 

    End Sub

    Private Sub btnCdHelp_spc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp_spc.Click
        Dim sFn As String = "Handles btnCdHelp_spc.Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", "", "", "", "", "", "")

            objHelp.FormText = "검체정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            'If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("spcnmd", "검체명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnCdHelp_spc)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_spc.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_spc.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sSpcCds As String = "", sSpcNmds As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sSpccd As String = aryList.Item(ix).ToString.Split("|"c)(1)
                    Dim sSpcnmd As String = aryList.Item(ix).ToString.Split("|"c)(0)

                    If ix > 0 Then
                        sSpcCds += "|" : sSpcNmds += "|"
                    End If

                    sSpcCds += sSpccd : sSpcNmds += sSpcnmd
                Next

                Me.txtSpdList.Text = sSpcCds.Replace("|", ",")
                Me.txtSpdList.Tag = sSpcNmds
            Else
                Me.txtSpdList.Text = ""
                Me.txtSpdList.Tag = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub
    

    Public Sub New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        sbSearchRef()

    End Sub

    Private Sub spd_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spd.ClickEvent

        With Me.spd
            .Row = e.row
            .Col = .GetColFromID("refcd") : Me.txtRefcd.Text = .Text
            .Col = .GetColFromID("refnm") : Me.txtRefnm.Text = .Text
            .Col = .GetColFromID("refnmd") : Me.txtRefnmd.Text = .Text
            .Col = .GetColFromID("seq") : Me.txtSeq.Text = .Text

            .Col = .GetColFromID("groupcd")
            If .Text <> "" Then
                Me.cboGroup.SelectedIndex = CInt(.Text) - 1
            End If

        End With


    End Sub

    Private Sub brnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brnReg.Click

        Me.txtRefcd.Text = ""
        Me.txtRefnm.Text = ""
        Me.txtRefnmd.Text = ""
        Me.txtSeq.Text = ""
        Me.cboGroup.SelectedIndex = 0

        Me.btnClose.Enabled = False
        Me.btnSearch.Enabled = False
        Me.btnSave.Text = "등록"
        msSpcmode = 1


    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim sRefcd As String = Me.txtRefcd.Text
        Dim sRefnm As String = Me.txtRefnm.Text
        Dim sRefnmd As String = Me.txtRefnmd.Text
        Dim sSeq As String = Me.txtSeq.Text
        Dim sGroupCd As String = CType(cboGroup.SelectedItem, String).Substring(1, CType(cboGroup.SelectedItem, String).IndexOf("]") - 1)

        Dim iRet As Boolean = mo_DAF.fnRegList(msSpcmode.ToString, sRefcd, sRefnm, sRefnmd, sSeq, sGroupCd)

        If iRet Then
            MsgBox("등록 되었습니다.")
        Else
            MsgBox("등록 실패")
        End If

        sbSearchRef()

    End Sub

    Private Sub spdTest_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdRstEx.ClickEvent
        With Me.spdRstEx
            .Row = e.row
            .Col = .GetColFromID("rstcd") : Me.txtRstcd2.Text = .Text.Substring(1)
            .Col = .GetColFromID("rstex") : Me.txtRstEx.Text = .Text
        End With
    End Sub

    Private Sub btnSearchT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchR.Click
        sbSearchTest()
    End Sub

    Private Sub btnSaveT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveR.Click

        'msSpcmode = 1:신규 , 0:수정 
        Dim bRet As Boolean = mo_DAF.fnRegRstExList(msSpcmode.ToString, Me.txtRstcd1.Text.Trim + Me.txtRstcd2.Text.Trim, Me.txtRstEx.Text)

        If bRet Then
            MsgBox("등록 되었습니다.")
        Else
            MsgBox("등록 실패")
        End If

        sbSearchTest()
        Me.btnSaveR.Text = "저장"
    End Sub

    Private Sub btnRegT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegR.Click

        Me.txtRstEx.Text = ""
        Me.txtRstcd2.Text = ""
        msSpcmode = 1
        Me.btnSaveR.Text = "등록"
        Me.btnCloseR.Enabled = False
        Me.btnSearchR.Enabled = False

    End Sub

    Private Sub btnCloseR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseR.Click
        Me.Close()
    End Sub

    Private Sub btnCloseS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseS.Click
        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRegT_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegT.Click
        Me.txtTestCd.Text = ""
        Me.cboSlipcd.SelectedIndex = 0
        msSpcmode = 1
        Me.btnSaveT.Text = "등록"
        Me.btnCloseT.Enabled = False
        Me.btnSearChT.Enabled = False
    End Sub

    Private Sub btnSaveT_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveT.Click

        Dim sRefcd As String = Me.txtRefcdT.Text
        Dim sRefnm As String = Me.txtRefnmT.Text

        Dim sTestcd As String = Me.txtTestCd.Text


        Dim iRet As Boolean = mo_DAF.fnRegTestRefList("0", sRefcd, sRefnm, sTestcd)

        If iRet Then
            MsgBox("등록 되었습니다.")
        Else
            MsgBox("등록 실패")
        End If

        sbSearchTestRefcd()

    End Sub
    Private Sub sbDisplay_Slip()

        Dim sFn As String = "Sub sbDisplay_Slip()"

        Try

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_PartSlip_List()

            Me.cboSlipcd.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlipcd.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            Me.cboSlipcd.SelectedIndex = 0

            Me.cboSlipcd2.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlipcd2.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            Me.cboSlipcd2.SelectedIndex = 0


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnHelpTestCd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelpTestCd.Click
        Dim sFn As String = "btnHelpTestCd_Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_ref_list(Ctrl.Get_Code(Me.cboSlipcd), "", "") ' SlipCd , testcd , filter

            objHelp.FormText = "검사코드정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            'If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnCdHelp_spc)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_spc.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_spc.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sTestCds As String = "", sTestnms As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sTestCd As String = aryList.Item(ix).ToString.Split("|"c)(0)
                    Dim sTestnm As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sTestCd += "|" : sTestnms += "|"
                    End If

                    sTestCds += sTestCd : sTestnms += sTestnm
                Next

                Me.txtTestCd.Text = sTestCds
                Me.txtTestnm.Text = sTestnms
            Else
                Me.txtTestCd.Text = ""
                Me.txtTestnm.Text = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub btnHelpRefcdT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelpRefcdT.Click
        Dim sFn As String = "btnHelpRefcdT_Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim dt As DataTable = mo_DAF.fnGetHospiRefList

            objHelp.FormText = "병원체코드정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            'If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("refcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("refnm", "병원체명", 100, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("groupcd", "그룹", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnCdHelp_spc)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_spc.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_spc.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sRefCds As String = "", sRefnms As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sRefCd As String = aryList.Item(ix).ToString.Split("|"c)(0)
                    Dim sRefnm As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sRefCd += "|" : sRefnms += "|"
                    End If

                    sRefCds += sRefCd : sRefnms += sRefnm
                Next

                Me.txtRefcdT.Text = sRefCds
                Me.txtRefnmT.Text = sRefnms
            Else
                Me.txtRefcdT.Text = ""
                Me.txtRefnmT.Text = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub btnSearChT_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearChT.Click
        sbSearchTestRefcd()
    End Sub

    Private Sub spdTestRef_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdTestRef.ClickEvent

        With Me.spdTestRef
            If .MaxRows > 0 Then
                .Row = e.row
                .Col = .GetColFromID("testcd") : Me.txtTestCd.Text = .Text
                .Col = .GetColFromID("testnm") : Me.txtTestnm.Text = .Text
                .Col = .GetColFromID("refcd") : Me.txtRefcdT.Text = .Text
                .Col = .GetColFromID("refnm") : Me.txtRefnmT.Text = .Text
            End If

        End With
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Try

            If MsgBox(Me.txtRefcd.Text + " 코드 삭제 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                Dim bRet As Boolean = mo_DAF.fnDelRefCd(Me.txtRefcd.Text, "H")

                If bRet Then
                    MsgBox("삭제되었습니다.")
                    sbSearchBaccdRefcd()
                    Me.txtRefcd.Text = ""
                    Me.txtRefnm.Text = ""
                    Me.txtRefnmd.Text = ""
                    Me.txtSeq.Text = ""
                    Me.cboGroup.SelectedIndex = 0
                Else
                    MsgBox("코드 삭제 실패 ")
                End If
            End If

        Catch ex As Exception
            MsgBox("코드 삭제 실패 ")
        End Try

    End Sub

    Private Sub btnDelS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelS.Click
        Try

            If MsgBox(Me.txtRefcd.Text + " 코드 삭제 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                Dim bRet As Boolean = mo_DAF.fnDelRefCd(Me.txtRefcd.Text, "S")

                If bRet Then
                    MsgBox("삭제되었습니다.")
                    sbSearchSpc()
                    Me.txtHospiSpccd.Text = ""
                    Me.txtSpcnm.Text = ""
                    Me.txtSpdList.Text = ""
                Else
                    MsgBox("코드 삭제 실패 ")
                End If
            End If

        Catch ex As Exception
            MsgBox("코드 삭제 실패 ")
        End Try
    End Sub

    Private Sub btnDelR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelR.Click
        Try

            If MsgBox(Me.txtRefcd.Text + " 코드 삭제 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                Dim bRet As Boolean = mo_DAF.fnDelRefCd(Me.txtRstcd1.Text + Me.txtRstcd2.Text, "R")

                If bRet Then
                    MsgBox("삭제되었습니다.")
                    sbSearchTest()
                    Me.txtRstcd2.Text = ""
                    Me.txtRstEx.Text = ""
                Else
                    MsgBox("코드 삭제 실패 ")
                End If
            End If

        Catch ex As Exception
            MsgBox("코드 삭제 실패 ")
        End Try
    End Sub

    Private Sub btnDelT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelT.Click
        Try

            If MsgBox(Me.txtTestCd.Text + " 코드 삭제 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                Dim bRet As Boolean = mo_DAF.fnDelRefCd(Me.txtRefcdT.Text, "T")

                If bRet Then
                    MsgBox("삭제되었습니다.")
                    sbSearchTestRefcd()
                    Me.txtTestnm.Text = ""
                    Me.txtTestCd.Text = ""
                    Me.txtRefcdT.Text = ""
                    Me.txtRefnmT.Text = ""
                Else
                    MsgBox("코드 삭제 실패 ")
                End If
            End If

        Catch ex As Exception
            MsgBox("코드 삭제 실패 ")
        End Try
    End Sub

    Private Sub spdBaccd_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdBaccd.ClickEvent
        With Me.spdBaccd
            If .MaxRows > 0 Then
                .Row = e.row
                .Col = .GetColFromID("baccd") : Me.txtBaccd.Text = .Text
                .Col = .GetColFromID("bacnm") : Me.txtBacnm.Text = .Text
                .Col = .GetColFromID("refcd") : Me.txtRefcdB.Text = .Text
                .Col = .GetColFromID("refnm") : Me.txtRefnmB.Text = .Text

            End If
        End With
    End Sub

    Private Sub btnRegB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegB.Click

        Me.txtBaccd.Text = ""
        Me.txtBacnm.Text = ""
        Me.txtRefcdB.Text = ""
        Me.txtRefnmB.Text = ""
        msSpcmode = 1
        Me.btnSaveB.Text = "등록"
        Me.btnCloseB.Enabled = False
        Me.btnSearchB.Enabled = False

    End Sub

    Private Sub btnCloseB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseB.Click
        Me.Close()
    End Sub

    Private Sub btnSaveB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveB.Click

        Dim sRefcd As String = Me.txtRefcdB.Text

        Dim sBaccd As String = Me.txtBaccd.Text

        Dim iRet As Boolean = mo_DAF.fnRegBacRefList(sRefcd, sBaccd)

        If iRet Then
            MsgBox("등록 되었습니다.")
        Else
            MsgBox("등록 실패")
        End If

        sbSearchBaccdRefcd()
    End Sub

    Private Sub btnSearchB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchB.Click
        sbSearchBaccdRefcd()
    End Sub

    Private Sub btnHelpBaccd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelpBaccd.Click
        Dim sFn As String = "btnHelpBaccd_Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim dt As DataTable = mo_DAF.fnGetBaccdList

            objHelp.FormText = "균코드정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            'If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("baccd", "균코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("bacnm", "균명", 100, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnCdHelp_spc)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_spc.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_spc.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sBacCds As String = "", sBacnms As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sBacCd As String = aryList.Item(ix).ToString.Split("|"c)(0)
                    Dim sBacnm As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sBacCd += "|" : sBacnms += "|"
                    End If

                    sBacCds += sBacCd : sBacnms += sBacnm
                Next

                Me.txtBaccd.Text = sBacCds
                Me.txtBacnm.Text = sBacnms
            Else
                Me.txtBaccd.Text = ""
                Me.txtBacnm.Text = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub btnHelpRefcdB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelpRefcdB.Click
        Dim sFn As String = "btnHelpBaccd_Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim dt As DataTable = mo_DAF.fnGetHospiRefList

            objHelp.FormText = "병원체코드정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            'If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("refcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("refnm", "병원체명", 100, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("groupcd", "그룹", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnCdHelp_spc)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_spc.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_spc.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sRefCds As String = "", sRefnms As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sRefCd As String = aryList.Item(ix).ToString.Split("|"c)(0)
                    Dim sRefnm As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sRefCd += "|" : sRefnms += "|"
                    End If

                    sRefCds += sRefCd : sRefnms += sRefnm
                Next

                Me.txtRefcdB.Text = sRefCds
                Me.txtRefnmB.Text = sRefnms
            Else
                Me.txtRefcdB.Text = ""
                Me.txtRefnmB.Text = ""
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelB.Click
        Try

            If MsgBox(Me.txtBaccd.Text + " 코드 삭제 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                Dim bRet As Boolean = mo_DAF.fnDelRefCd(Me.txtRefcdB.Text, "B")

                If bRet Then
                    MsgBox("삭제되었습니다.")
                    sbSearchBaccdRefcd()
                    Me.txtRefcdB.Text = ""
                    Me.txtRefnmB.Text = ""
                    Me.txtBaccd.Text = ""
                    Me.txtBacnm.Text = ""
                Else
                    MsgBox("코드 삭제 실패 ")
                End If
            End If

        Catch ex As Exception
            MsgBox("코드 삭제 실패 ")
        End Try
    End Sub

   
    Private Sub btnCloseG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseG.Click
        Me.Close()
    End Sub
    '검사항목별 검사방법
    Private Sub btnSaveG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveG.Click
        'msSpcmode = 1:신규 , 0:수정 
        Dim sGbn As String = ""

        Dim bRet As Boolean = mo_DAF.fnRegRefTestList("0", Me.txtRefcdG.Text.Trim + Me.txtTestlist.Text.Trim, Me.txtRstEx.Text)

    End Sub

    Private Sub btnDelRefTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelRefTest.Click

        Dim bRet As Boolean = mo_DAF.fnRegRefTestList("1", Me.txtRefcdG.Text.Trim + Me.txtTestlist.Text.Trim, Me.txtRstEx.Text)

    End Sub

    Private Sub btnSearchG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchG.Click
        sbSearchRefTest()
        'sbSearchRef()
    End Sub

    Private Sub spdTestRefcd_ClickEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdTestRefcd.ClickEvent



        With Me.spdTestRefcd
            .Row = e.row
            .Col = .GetColFromID("refcd") : Me.txtRefcdG.Text = .Text
            .Col = .GetColFromID("refnm") : Me.txtRefnmG.Text = .Text
            .Col = .GetColFromID("testcd") : Me.txtTestlist.Text = .Text
        End With

        With Me.spdTestTest
            .MaxRows = 0

            If Me.txtTestlist.Text.IndexOf("|") > -1 Then
                Dim sTestlist() As String = Me.txtTestlist.Text.Split("|"c)
                .MaxRows = sTestlist.Length

                For ix As Integer = 0 To .MaxRows - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("testcd") : .Text = sTestlist(ix)

                    Dim sTestnm As String = mo_DAF.fnGetTestnm(sTestlist(ix))

                    .Col = .GetColFromID("testnm") : .Text = sTestnm
                Next

            End If

        End With

    End Sub

    
    Private Sub btnhelpTest2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnhelpTest2.Click
        Dim sFn As String = "btnHelpTestCd_Click"
        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_test_ref_list(Ctrl.Get_Code(Me.cboSlipcd2), "", "") ' SlipCd , testcd , filter

            objHelp.FormText = "검사코드정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True

            'If Me.txtSelSpc.Text <> "" Then objHelp.KeyCodes = Me.txtSelSpc.Tag.ToString.Split("^"c)(0) + "|"

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("testcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(Me.btnCdHelp_spc)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - Me.btnCdHelp_spc.Left, pntFrmXY.Y + pntCtlXY.Y + Me.btnCdHelp_spc.Height + 80, dt)

            If aryList.Count > 0 Then

                Dim sTestCds As String = "", sTestnms As String = ""

                For ix As Integer = 0 To aryList.Count - 1
                    Dim sTestCd As String = aryList.Item(ix).ToString.Split("|"c)(0)
                    Dim sTestnm As String = aryList.Item(ix).ToString.Split("|"c)(1)

                    If ix > 0 Then
                        sTestCd += "|" : sTestnms += "|"
                    End If

                    sTestCds += sTestCd : sTestnms += sTestnm
                Next

                Me.txtTestcdG.Text = sTestCds
                Me.txtTestnmG.Text = sTestnms
            Else
                Me.txtTestcdG.Text = ""
                Me.txtTestnmG.Text = ""
            End If

            sbRefcdTestCdAdd(Me.txtTestcdG.Text)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub sbRefcdTestCdAdd(ByVal rsTestcd As String)
        Try
            With Me.spdTestTest
                .MaxRows += 1
                .Row = .MaxRows
                .Col = .GetColFromID("testcd") : .Text = Me.txtTestcdG.Text
                .Col = .GetColFromID("testnm") : .Text = Me.txtTestnmG.Text

            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class