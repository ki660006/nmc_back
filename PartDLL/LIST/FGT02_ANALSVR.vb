Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Public Class FGT02_ANALSVR

    Friend mbForceClose As Boolean = False
    Friend mbAnalyzing As Boolean = False

    Public Function fnDisplay_ResultOfAnalysis() As Integer
        Dim sFn As String = "fnDisplay_ResultOfAnalysis"

        Try
            '> �ʱ�ȭ
            With Me.spdList
                .MaxRows = 0

                .Col = .GetColFromID("styymmdd")
                .Row = 0
                .Text = Me.lblDay.Text
            End With

            Me.Cursor = Cursors.WaitCursor

            Dim dt As DataTable = (New LISAPP.APP_T.SrhFn).fnGet_Test_AnalysisInfo(Me.dtpDayB.Text.Replace("-", ""), Me.dtpDayE.Text.Replace("-", ""), "-")

            If dt Is Nothing Then Return 0

            Dim iReturn As Integer = 0

            With Me.spdList
                .ReDraw = False

                .MaxRows = Me.dtpDayE.Value.Subtract(Me.dtpDayB.Value).Days + 1

                For i As Integer = 1 To Me.dtpDayE.Value.Subtract(Me.dtpDayB.Value).Days + 1
                    .Col = .GetColFromID("styymmdd")
                    .Row = i
                    .Text = Me.dtpDayB.Value.AddDays(i - 1).ToString("yyyy-MM-dd")

                    Dim a_dr As DataRow() = dt.Select("styymmdd = '" + Me.dtpDayB.Value.AddDays(i - 1).ToString("yyyyMMdd") + "'")

                    If a_dr.Length = 0 Then Continue For

                    .SetText(.GetColFromID("regdt"), i, a_dr(0).Item("regdt").ToString)
                    .SetText(.GetColFromID("regid"), i, a_dr(0).Item("regid").ToString)
                    .SetText(.GetColFromID("regnm"), i, a_dr(0).Item("regnm").ToString)

                    iReturn += 1
                Next

                .ReDraw = True
            End With

            Return iReturn

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = Cursors.Default
            Me.spdList.ReDraw = True

        End Try
    End Function

    Public Sub Exec_Analysis()

        Try
            '> �ʱ�ȭ
            mbAnalyzing = True
            Me.btnSearch.Enabled = False
            Me.btnAnalysis.Enabled = False
            Me.btnClose.Enabled = False

            Dim al_StDay As New ArrayList

            With Me.spdList
                .Col = .GetColFromID("styymmdd")
                .Row = 0

                For i As Integer = 1 To .MaxRows
                    .Col = .GetColFromID("chk")
                    .Row = i

                    If .Text = "1" Then
                        .Col = .GetColFromID("styymmdd")
                        .Row = i
                        al_StDay.Add(.Text)
                    End If
                Next

                If al_StDay.Count = 0 Then
                    MsgBox(Me.btnAnalysis.Text + "�� ���� ���õ� ���� �����ϴ�. Ȯ���Ͽ� �ֽʽÿ�!!", MsgBoxStyle.Exclamation)

                    Return
                End If
            End With

            '> �ʱ�ȭ
            Me.pgbAnalysisTot.Maximum = al_StDay.Count
            Me.pgbAnalysisTot.Minimum = 0
            Me.pgbAnalysisTot.Value = 0

            Me.Cursor = Cursors.WaitCursor

            For i As Integer = 1 To al_StDay.Count
                Me.lblAnalDay.Text = al_StDay(i - 1).ToString.Replace("-", "")

                Dim sReturn As String = (New LISAPP.APP_T.ExecFn).fnExe_Test_Statistics(al_StDay(i - 1).ToString.Replace("-", ""))

                '> ȭ�� ǥ��
                Me.pgbAnalysisTot.Value = i
                Me.pgbAnalysisTot.Refresh()

                If sReturn = "" Then Continue For

                With Me.spdList
                    Dim iRow As Integer = .SearchCol(.GetColFromID("styymmdd"), 0, .MaxRows, al_StDay(i - 1).ToString, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRow < 1 Then Continue For

                    .SetText(.GetColFromID("chk"), iRow, "")
                    .SetText(.GetColFromID("regdt"), iRow, sReturn)
                    .SetText(.GetColFromID("regid"), iRow, USER_INFO.USRID)
                    .SetText(.GetColFromID("regnm"), iRow, USER_INFO.USRNM)
                End With
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.spdList.ReDraw = True

            mbAnalyzing = False
            Me.btnSearch.Enabled = True
            Me.btnAnalysis.Enabled = True
            Me.btnClose.Enabled = True

        End Try
    End Sub

    Private Function fnValidation() As Boolean

        Dim bReturn As Boolean = False

        Try
            Dim sStType As String = ""
            Dim bAnalysis As Boolean = False

            With Me.spdList
                For i As Integer = 1 To .MaxRows
                    .Col = .GetColFromID("chk")
                    .Row = i

                    If .Text = "1" Then
                        .Col = .GetColFromID("regdt")
                        .Row = i

                        If IsDate(.Text) Then
                            bAnalysis = True

                            Exit For
                        End If
                    End If
                Next
            End With

            If bAnalysis Then
                If MsgBox("�̹� �м��� ��¥�� ���õǾ����ϴ�. ��м��� �Ͻðڽ��ϱ�?", _
                            MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, _
                                "��м� Ȯ��") = MsgBoxResult.No Then
                    bReturn = False
                Else
                    bReturn = True
                End If
            Else
                bReturn = True
            End If

            Return bReturn

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.spdList.ReDraw = True

        End Try
    End Function

    '> Control Event
    Private Sub FGT02_ANAL_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If mbForceClose Then Return

        e.Cancel = True

        Me.Hide()
    End Sub

    Private Sub FGT02_ANALSVR_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.Escape
                btnClose_Click(Nothing, Nothing)

        End Select
    End Sub

    Private Sub FGT02_ANAL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Control.CheckForIllegalCrossThreadCalls = False

        Me.lblAnalDay.Text = ""
        Me.lblAnalTCd.Text = ""
    End Sub

    Private Sub btnAnalysis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnalysis.Click

        Try
            Dim bValidation As Boolean = fnValidation()

            If bValidation = False Then Return

            If MsgBox(Me.btnAnalysis.Text + "�� �����Ͻðڽ��ϱ�?", _
                        MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, _
                            Me.btnAnalysis.Text + " ���� Ȯ��") = MsgBoxResult.No Then Return

            Dim thread_anal As Threading.Thread = New Threading.Thread(AddressOf Exec_Analysis)

            thread_anal.Name = "analysis"
            thread_anal.IsBackground = True
            thread_anal.Start()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Hide()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        fnDisplay_ResultOfAnalysis()
    End Sub

    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.lblDay.Text = "ó������" Then
            Me.lblDay.Text = "��������"
        ElseIf Me.lblDay.Text = "��������" Then
            Me.lblDay.Text = "��������"
        Else
            Me.lblDay.Text = "ó������"
        End If

    End Sub
End Class