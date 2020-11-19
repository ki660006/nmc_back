Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login

Public Class FGF11_S01
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FDF11_S01.vb, Class : FDF11_S01" + vbTab
    Private msDispSeq_Gbn As String = "L"

    Private mobjDAF As New LISAPP.APP_F_TEST

    Private Sub sbDisplay_slip()
        Dim sFn As String = "sbDisplay_slip"
        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()
            If dt.Rows.Count < 1 Then Return

            cboSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            cboSlip.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
        End Try

    End Sub

    Private Sub sbDisplay_ordslip()
        Dim sFn As String = "sbDisplay_ordslip"
        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_OrdSlip_List()
            If dt.Rows.Count < 1 Then Return

            cboSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboSlip.Items.Add("[" + dt.Rows(ix).Item("tordslip").ToString.Trim + "] " + dt.Rows(ix).Item("tordslipnm").ToString)
            Next

            cboSlip.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
        End Try

    End Sub

    Private Sub sbDisplay_test()
        Dim sFn As String = "sbDisplay_test"
        Try
            Dim sPartSlip As String = Ctrl.Get_Code(cboSlip)
            Dim sTOrdSlip As String = Ctrl.Get_Code(cboSlip)

            If msDispSeq_Gbn = "L" Then
                sTOrdSlip = ""
            Else
                sPartSlip = ""
            End If

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list(sPartSlip, "", "", , , , sTOrdSlip)

            If msDispSeq_Gbn = "L" Then
                Dim a_dr As DataRow() = dt.Select("", "sort1, sort2, testcd")
                dt = Fn.ChangeToDataTable(a_dr)
            Else
                Dim a_dr As DataRow() = dt.Select("", "sort_tslip, sort_ord, testcd")
                dt = Fn.ChangeToDataTable(a_dr)
            End If

            spdTest.MaxRows = 0
            If dt.Rows.Count < 1 Then Return

            With spdTest
                .ReDraw = True
                .MaxRows = dt.Rows.Count

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                    .Col = .GetColFromID("tcdgbn") : .Text = dt.Rows(ix).Item("tcdgbn").ToString

                    If msDispSeq_Gbn = "L" Then
                        .Col = .GetColFromID("dispseq") : .Text = dt.Rows(ix).Item("sort2").ToString
                    Else
                        .Col = .GetColFromID("dispseq") : .Text = dt.Rows(ix).Item("sort_ord").ToString
                    End If
                Next

                .ReDraw = False
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            spdTest.ReDraw = False
        End Try
    End Sub

    Private Sub FDF11_S01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

        spdTest.MaxRows = 0

        If msDispSeq_Gbn = "O" Then
            Me.lblSlip.Text = "처방슬립"
            sbDisplay_ordslip()
        Else
            sbDisplay_slip()
        End If

    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click

        With spdTest
            Dim iRow As Integer = .ActiveRow

            If iRow < 2 Then Return

            .Row = iRow
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
            .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
            .Col = .GetColFromID("tcdgbn") : Dim sTcdGbn As String = .Text

            .Row = iRow - 1
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("testcd") : .Text = sTestCd
            .Col = .GetColFromID("tnmd") : .Text = sTnmd
            .Col = .GetColFromID("tcdgbn") : .Text = sTcdGbn

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow - 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click

        With spdTest
            Dim iRow As Integer = .ActiveRow

            If iRow = .MaxRows Then Return

            .Row = iRow
            .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
            .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
            .Col = .GetColFromID("tcdgbn") : Dim sTcdGbn As String = .Text

            .Row = iRow + 2
            .MaxRows += 1
            .Action = FPSpreadADO.ActionConstants.ActionInsertRow

            .Col = .GetColFromID("testcd") : .Text = sTestCd
            .Col = .GetColFromID("tnmd") : .Text = sTnmd
            .Col = .GetColFromID("tcdgbn") : .Text = sTcdGbn

            .Row = iRow
            .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
            .MaxRows -= 1

            .Row = iRow + 1
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell

        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sFn As String = "sbDisplay_slip"

        Try

            With spdTest
                For ix = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("testcd") : Dim sTestCd As String = .Text
                    .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
                    .Col = .GetColFromID("dispseq") : Dim sDispSeq As String = .Text

                    If sDispSeq <> "" Then
                        If msDispSeq_Gbn = "L" Then

                            If mobjDAF.TransTestInfo_DispseqlL(sTestCd, sDispSeq, USER_INFO.USRID) = False Then
                                MsgBox("검사[" + sTnmd + "] 저장시 오류가 발생했습니다.!!")
                                Return
                            End If
                        Else
                            If mobjDAF.TransTestInfo_DispseqlO(sTestCd, sDispSeq, USER_INFO.USRID) = False Then
                                MsgBox("검사[" + sTnmd + "] 저장시 오류가 발생했습니다.!!")
                                Return
                            End If
                        End If
                    End If
                Next

            End With

            MsgBox("정상적으로 저장했습니다.!!")

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbDisplay_test()
    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsDispSeq_Gbn As String)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        msDispSeq_Gbn = rsDispSeq_Gbn
    End Sub

    Private Sub spdTest_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdTest.KeyDownEvent

        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        With spdTest
            If .ActiveCol <> .GetColFromID("dispseq") Then Return

            Dim iRow As Integer = .ActiveRow
            Dim iCol As Integer = .ActiveCol

            .Row = iRow : .Col = iCol : Dim sValue As String = .Text

            If IsNumeric(sValue) = False Then
                MsgBox("수치값만 입력 가능합니다.!!")
                .Row = iRow : .Col = iCol : .Text = ""
                Return
            End If

            If MsgBox("일괄 적용하시겠습니까?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "정렬순서") = MsgBoxResult.No Then Return

            Dim iSeq As Integer = Convert.ToInt16(sValue)

            For ix As Integer = iRow + 1 To .MaxRows

                iSeq += 1

                .Row = ix
                .Col = .GetColFromID("dispseq") : .Text = iSeq.ToString
            Next

        End With


    End Sub
End Class