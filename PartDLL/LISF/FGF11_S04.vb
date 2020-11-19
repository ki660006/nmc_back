Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.CommConst
Imports CDHELP.FGCDMSG01

Public Class FGF11_S04

    Public msTestCd As String = ""
    Public msSpcCd As String = ""
    Public marrlist As New ArrayList
    Private mo_DAF As New LISAPP.APP_F_TEST
    Private mpopup As New CDHELP.FGCDMSG01

    Private miMouseX As Integer = 0
    Private miMouseY As Integer = 0

    Private Const msFile As String = "File : FGF11_S04.vb, Class : FGF11_S04" + vbTab
    Public Sub New(ByVal rsTestcd As String, ByVal rsSpccd As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        msTestCd = rsTestcd
        msSpcCd = rsSpccd

    End Sub

    Private Sub FGF11_S02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sFn As String = "Private Sub FGF11_S02_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load"
        Try
            Dim dt As DataTable = New DataTable
            Dim dt2 As DataTable = New DataTable
            Dim iCol As Integer = 0

            dt = mo_DAF.GetTestInfo_detail2_New(msTestCd, msSpcCd)

            dt2 = mo_DAF.GetTestInfo_detail3(msTestCd, msSpcCd)

            Me.txtTestcd.Text = msTestCd
            Me.TxtSpccd.Text = msSpcCd
            If dt2.Rows.Count > 0 Then
                Me.TxtTnmd.Text = dt2.Rows(0).Item("tnmd").ToString
                Me.Txtspcnmd.Text = dt2.Rows(0).Item("spcnmd").ToString
            End If

            '스프레드 초기화
            sbInitialize_spdDTest()

            If dt.Rows.Count < 1 Then Return

            With spdDTest
                .ReDraw = False

                .MaxRows = dt.Rows.Count

                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        iCol = 0
                        iCol = .GetColFromID(dt.Columns(j).ColumnName.ToLower)

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i + 1
                            .Text = dt.Rows(i).Item(j).ToString.Trim
                        End If
                    Next
                Next

                .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub sbInitialize_spdDTest()
        Dim sFn As String = "Private Sub sbInitializeControl_spdDTest()"

        Try
            With spdDTest
                .ReDraw = False : .MaxRows = 0 : .ReDraw = True
            End With
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub btnMaxRowAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaxRowAdd.Click, btnMaxRow10.Click
        '/// Spread Row 추가 (+1, +10)
        Try

            If CType(sender, Windows.Forms.Button).Name.StartsWith("btnMaxRowAdd") Then
                spdDTest.MaxRows += 1
            ElseIf CType(sender, Windows.Forms.Button).Name.StartsWith("btnMaxRow10") Then
                spdDTest.MaxRows += 10
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub spdDTest_DblClick(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdDTest.DblClick
        '/// 항목 삭제
        Try
            If e.row > 0 Then
                
                With Me.spdDTest
                    .Row = e.row
                    .Col = .GetColFromID("tnmd") : Dim tnmd As String = .Text

                    If MsgBox("검사 [" + tnmd + "]를 제거 하시겠습니까?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return

                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1
                End With
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try

            Dim DTEST_ARRAY As New ArrayList

            With spdDTest
                For i As Integer = 1 To .MaxRows

                    Dim DTEST_INFO As New TESTINFO_DTEST

                    .Row = i
                    .Col = .GetColFromID("tnmd") : Dim tnmd As String = .Text
                    .Col = .GetColFromID("testcd") : Dim testcd As String = .Text
                    .Col = .GetColFromID("spccd") : Dim spccd As String = .Text

                    If tnmd <> "" Then

                        DTEST_INFO.TESTCD = testcd
                        DTEST_INFO.SPCCD = spccd
                        DTEST_INFO.TNMD = tnmd

                        DTEST_ARRAY.Add(DTEST_INFO)

                    End If

                Next

                If mo_DAF.DTEST_INFO_SAVE(msTestCd, msSpcCd, DTEST_ARRAY) Then
                    mpopup.sb_DisplayMsg(Me, "I", "저장되었습니다.")
                End If


            End With

            FGF11_S02_Load(Nothing, Nothing)

        Catch ex As Exception
            mpopup.sb_DisplayMsg(Me, "E", ex.Message)
        End Try
    End Sub

    Private Sub spdDTest_ButtonClicked(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdDTest.ButtonClicked
        Dim sFn As String = "spdDTest_ButtonClicked"

        If e.row < 1 Then Return
        If e.col <> Me.spdDTest.GetColFromID("btntestcd") Then Return

        If Len(Me.txtTestcd.Text.Trim) < 1 Or Len(Me.TxtTnmd.Text.Trim) < 1 Then Return
        If Len(Me.TxtSpccd.Text.Trim) < 1 Or Len(Me.Txtspcnmd.Text.Trim) < 1 Then Return

        Dim iTop As Integer = miMouseY
        Dim iLeft As Integer = miMouseX

        Dim sTestcd As String = ""
        Dim sSpccd As String = ""

        With Me.spdDTest
            .Row = e.row
            .Col = .GetColFromID("testcd") : sTestcd = .Text
            .Col = .GetColFromID("spccd") : sSpccd = .Text
        End With


        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list("", "", sTestcd, sSpccd)
            'Dim a_dr As DataRow() = dt.Select("(tcdgbn IN ('P', 'B') AND titleyn = '0' OR tcdgbn IN ('S', 'C'))")
            'dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"

            objHelp.MaxRows = 15
            objHelp.OnRowReturnYN = True

            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("tnmd", "검사명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tcdgbn", "", 0, , , True)

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)

            If alList.Count > 0 Then

                With Me.spdDTest
                    .SetText(.GetColFromID("testcd"), e.row, alList.Item(0).ToString.Split("|"c)(0))
                    .SetText(.GetColFromID("spccd"), e.row, alList.Item(0).ToString.Split("|"c)(1))
                    .SetText(.GetColFromID("tnmd"), e.row, alList.Item(0).ToString.Split("|"c)(2))
                End With
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub spdDTest_MouseDownEvent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdDTest.MouseDownEvent
        miMouseX = Ctrl.FindControlLeft(Me.spdDTest) + e.x
        miMouseY = Ctrl.FindControlTop(Me.spdDTest) + e.y
    End Sub

End Class