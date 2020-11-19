Imports System.Drawing

Public Class FGHISTORY
    Private mbSave As Boolean = False

    Dim m_al_List As ArrayList

    Public Function Display_Data(ByVal roForm As Windows.Forms.Form, ByVal rsBcNo As String) As ArrayList

        Dim dt As New DataTable
        Dim alList As New ArrayList

        Try
            axPatInfo.BcNo = rsBcNo
            axPatInfo.fnDisplay_Data()

            dt = LISAPP.APP_R.RstFn.fnGet_ResultHistory(rsBcNo)
            sbDisplay_ResultView(dt)

            Me.ShowDialog(roForm)

            If mbSave Then
                'With Me.spdHistory
                '    For intRow As Integer = 1 To .MaxRows
                '        .Row = intRow
                '        .Col = .GetColFromID("chk")
                '        If .Text = "1" Then
                '            Dim objRst As New RST_INFO

                '            .Col = .GetColFromID("tclscd") : objRst.msTclsCd = .Text
                '            .Col = .GetColFromID("orgrst") : objRst.msOrgRst = .Text

                '            aryList.Add(objRst)
                '        End If
                '    Next
                'End With

                alList = m_al_List
            End If

            Return alList

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Return New ArrayList
        End Try

    End Function

    Private Sub sbDisplay_ResultView(ByVal r_dt As DataTable)
        Dim sFn As String = "Protected Sub sbDisplay_ResultView(DataTable)"

        Try
            With Me.spdHistory

                .MaxRows = 0

                '.ReDraw = False
                .MaxRows = r_dt.Rows.Count

                For introw As Integer = 1 To r_dt.Rows.Count
                    .Row = introw
                    .Col = .GetColFromID("chk") : .Text = ""

                    For intIx1 As Integer = 1 To r_dt.Columns.Count
                        Dim intCol As Integer = 0

                        intCol = .GetColFromID(r_dt.Columns(intIx1 - 1).ColumnName.ToLower())

                        If intCol > 0 Then
                            .Row = introw
                            .Col = intCol
                            Select Case intCol
                                Case .GetColFromID("hlgmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim = "L" Then
                                        .BackColor = Color.FromArgb(221, 240, 255)
                                        .ForeColor = Color.FromArgb(0, 0, 255)
                                    ElseIf r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim = "H" Then
                                        .BackColor = Color.FromArgb(255, 230, 231)
                                        .ForeColor = Color.FromArgb(255, 0, 0)
                                    End If

                                Case .GetColFromID("panicmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim = "P" Then
                                        .BackColor = Color.FromArgb(150, 150, 255)
                                        .ForeColor = Color.FromArgb(255, 255, 255)
                                    End If

                                Case .GetColFromID("deltamark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim = "D" Then
                                        .BackColor = Color.FromArgb(150, 255, 150)
                                        .ForeColor = Color.FromArgb(0, 128, 64)
                                    End If

                                Case .GetColFromID("criticalmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim = "C" Then
                                        .BackColor = Color.FromArgb(255, 150, 255)
                                        .ForeColor = Color.FromArgb(255, 255, 255)
                                    End If

                                Case .GetColFromID("alertmark")
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                    If r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim = "A" Then
                                        .BackColor = Color.FromArgb(255, 255, 150)
                                        .ForeColor = Color.FromArgb(0, 0, 0)
                                    End If

                                Case .GetColFromID("tnmd")
                                    If r_dt.Rows(introw - 1).Item("tcdgbn").ToString().Trim = "C" Then
                                        If r_dt.Rows(introw - 1).Item("tclscd").ToString.Trim = "" Then
                                            .Text = "... " + r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                        Else
                                            .Text = "  ... " + r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                        End If
                                    ElseIf r_dt.Rows(introw - 1).Item("tclscd").ToString.Trim = "" Or _
                                           r_dt.Rows(introw - 1).Item("tcdgbn").ToString.Trim = "R" Or r_dt.Rows(introw - 1).Item("tcdgbn").ToString.Trim = "B" Then
                                        .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                    Else
                                        .Text = "  " + r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim
                                    End If

                                Case Else
                                    .Text = r_dt.Rows(introw - 1).Item(intIx1 - 1).ToString().Trim

                            End Select
                        End If
                    Next
                Next
            End With

        Catch ex As Exception
            'sbLog_Exception(sFn + " : " + ex.Message)

        Finally
            'Me.spdHistory.ReDraw = True
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        mbSave = False
        Me.Close()

    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click

        mbSave = True

        m_al_List = New ArrayList

        With Me.spdHistory
            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    Dim objRst As New RST_INFO

                    .Col = .GetColFromID("testcd") : objRst.TestCd = .Text
                    .Col = .GetColFromID("orgrst") : objRst.OrgRst = .Text

                    m_al_List.Add(objRst)
                End If
            Next
        End With

        Me.Close()

    End Sub

    Private Sub spdHistory_BlockSelected(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_BlockSelectedEvent) Handles spdHistory.BlockSelected

        With spdHistory
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("chk") : .Text = IIf(.Text = "1", "", "1").ToString
            Next
        End With

    End Sub

End Class

