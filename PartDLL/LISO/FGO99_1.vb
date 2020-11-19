Imports DA01

Public Class FGO99

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        Dim dt As DataTable
        Dim strDateS As String = ""
        Dim strDateE As String = ""
        Dim sPart As String = ""

        strDateS = Format(dtpTkDtS.Value, "yyyy-MM-dd")
        strDateE = Format(DateAdd(DateInterval.Day, 1, dtpTkDtE.Value), "yyyy-MM-dd")

        If Me.cboPart.Text <> "" Then
            sPart = Me.cboPart.Text
        End If

        dt = DA_R.fnGetBcnoList(strDateS, strDateE, sPart)

        If dt.Rows.Count > 0 Then

            Me.lbTotal.Text = "총 ( 0 / " + Me.spdList.MaxRows.ToString + " 건 )"
            With Me.spdList
                .MaxRows = 0
                If Me.chklimit.Checked Then
                    If Me.txtlimit.Text <> "" Then
                        .MaxRows = CInt(Me.txtlimit.Text)
                    Else
                        .MaxRows = dt.Rows.Count
                    End If
                Else
                    .MaxRows = dt.Rows.Count
                End If
                '.MaxRows = 0
                '.MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                Next
            End With
        Else
            Me.spdList.MaxRows = 0
        End If
    End Sub

    Private Sub FGO99_Load(sender As Object, e As EventArgs) Handles Me.Load
        dtpTkDtS.Value = Now
        dtpTkDtE.Value = Now
    End Sub


    Private Sub btnRst_Emr_Click(sender As Object, e As EventArgs) Handles btnExcute.Click
        Try
            If Me.spdList.MaxRows > 0 Then

                With Me.spdList

                    Dim sBcno As String = ""

                    For ix As Integer = 0 To .MaxRows - 1
                        .Row = ix + 1
                        .Col = .GetColFromID("bcno")
                        sBcno = .Text
                        Dim obj As New DA01.DA_RST_SAVE_R("E")

                        Dim bRstflg As Boolean = obj.fnEdit_EXE_EMR_RSTFLG(sBcno)

                        Dim dt_RstList As DataTable = obj.fnGet_Result_For_EMR(sBcno)

                        If sBcno.Substring(8, 1) = "M" Then
                            Dim dt_AnitInfo As DataTable = Nothing
                            Dim dt_AntiRstList As DataTable = Nothing

                            Dim bReturn As Boolean = False

                            If sBcno.Substring(8, 1) = "M" Then
                                dt_AnitInfo = (New DA01.DA_SF_RegRst_M).fnGetAntiInfo_For_EMR(sBcno)
                            End If

                            Dim dt_EmrOrder As DataTable = obj.fnGet_EMR_OrderYn(sBcno)
                            Dim sEMR As String = ""

                            If dt_EmrOrder.Rows.Count > 0 Then
                                sEMR = "E"
                            Else
                                sEMR = "O"
                            End If

                            '2-1) 배양정보가 있을경우 배양결과 조회
                            If dt_AnitInfo.Rows.Count > 0 Then

                                Dim sTclscd As String = ""
                                Dim sspccd As String = ""

                                For i As Integer = 0 To dt_AnitInfo.Rows.Count - 1

                                    sTclscd = dt_AnitInfo.Rows(i).Item("tclscd").ToString
                                    sspccd = dt_AnitInfo.Rows(i).Item("spccd").ToString

                                    dt_AntiRstList = (New DA01.DA_SF_RegRst_M).fnGetAntiRslt_For_EMR(sBcno, sTclscd, sspccd)


                                    If dt_RstList.Rows.Count > 0 And dt_AntiRstList.Rows.Count > 0 Then
                                        bReturn = obj.fnEdit_EXE_EMR_RST_M(dt_RstList, dt_AntiRstList, dt_EmrOrder, sEMR)
                                    Else
                                        bReturn = True
                                        Debug.WriteLine("항균제결과 조회 안됨")
                                    End If

                                Next

                                If bReturn Then
                                    .Col = .GetColFromID("emryn")
                                    .Text = "Y"
                                    .BackColor = Color.Green
                                Else
                                    .Col = .GetColFromID("emryn")
                                    .Text = "N"
                                    .BackColor = Color.Coral
                                End If
                            Else
                                '배양정보가 없을 경우
                                If dt_RstList.Rows.Count > 0 Then
                                    bReturn = obj.fnEdit_EXE_EMR_RST_M(dt_RstList, dt_AntiRstList, dt_EmrOrder, sEMR)
                                Else
                                    bReturn = True
                                End If

                                If bReturn Then
                                    .Col = .GetColFromID("emryn")
                                    .Text = "Y"
                                    .BackColor = Color.Green
                                Else
                                    .Col = .GetColFromID("emryn")
                                    .Text = "N"
                                    .BackColor = Color.Coral
                                End If

                            End If
                        Else
                            If dt_RstList.Rows.Count > 0 Then
                                Dim dt_EmrOrder As DataTable = obj.fnGet_EMR_OrderYn(sBcno)

                                Dim bReturn As Boolean = False

                                If dt_EmrOrder.Rows.Count > 0 Then
                                    'EMR오더일 경우 상태업데이트 까지
                                    bReturn = obj.fnEdit_EXE_EMR_RST(dt_RstList, dt_EmrOrder, "E")
                                Else
                                    'OCS일 경우 안한다 
                                    bReturn = obj.fnEdit_EXE_EMR_RST(dt_RstList, dt_EmrOrder, "O")
                                End If

                                ' MsgBox(bReturn.ToString)
                                If bReturn Then
                                    .Col = .GetColFromID("emryn")
                                    .Text = "Y"
                                    .BackColor = Color.Green
                                Else
                                    .Col = .GetColFromID("emryn")
                                    .Text = "N"
                                    .BackColor = Color.Coral
                                End If
                            End If
                        End If
                        Me.lbTotal.Text = "총 ( " + (ix + 1).ToString + " / " + Me.spdList.MaxRows.ToString + " 건 )"
                    Next

                End With

            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub
End Class