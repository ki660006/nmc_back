Imports System.Windows.Forms
Imports COMMON.CommFN
Imports COMMON.CommLogin.STU_CONST
Imports COMMON.CommLogin.LOGIN
Imports LISAPP.APP_BT
Imports CDHELP.FGCDHELPFN

Public Class FGB29
    Public Shared PRG_CONST As New COMMON.CommLogin.STU_CONST

    Private mbQuery As Boolean = False
    Private mbEscape As Boolean = False

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If mbQuery = False Then Me.Close()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            If mbQuery = False Then sbFormClear(0)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub
    Private Sub sbFormClear(ByVal riPhase As Integer)
        Try
            If InStr("0", riPhase.ToString, CompareMethod.Text) > 0 Then
                Me.spdList.MaxRows = 0
                Me.txtPatnm.Text = ""
                Me.txtRegNo.Text = ""
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGB29_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F4 Then ' 화면정리
            btnClear_Click(Nothing, Nothing)
        ElseIf e.KeyCode = Keys.Escape Then ' 종료
            If mbQuery = False Then Me.Close()
        End If
    End Sub

    Private Sub FGB29_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        sbDisp_Init()
    End Sub

    Public Sub sbDisp_Init()
        Me.spdList.MaxRows = 0
        Me.dtpDate0.CustomFormat = "yyyy-MM-dd"
        Me.dtpDate1.CustomFormat = "yyyy-MM-dd"

        Me.dtpDate0.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 00:00:00")
        Me.dtpDate1.Value = CDate(Format(Now, "yyyy-MM-dd").ToString + " 23:59:59")
    End Sub

    Private Sub btnQuery_Click(sender As Object, e As EventArgs) Handles btnQuery.Click
        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Me.spdList.MaxRows = 0

            If mbQuery = False Then sbQuery()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Sub
    ' overTime계산
    Private Function fn_chk_bldTAT(ByVal rsVaryn As String, ByVal rsComcd As String, ByVal rsTnsgbn As String, ByVal rsIogbn As String, ByVal rsBldAboType As String,
                                   ByVal rsPatAboType As String, ByVal rsTAT_mi As String) As Boolean
        Try
            Dim chk_YN As Boolean = False
            Dim chk_Over_Tat As Integer = 0

            If PRG_CONST.RBC_YN(rsComcd) <> "" Then ' NOTE : RBC 일 때
                If rsIogbn = "O" Then ' NOTE : 외래
                    chk_YN = True
                    chk_Over_Tat = 60
                Else                  ' NOTE : 병동
                    If rsTnsgbn = "응급" Then
                        chk_YN = True
                        chk_Over_Tat = 30
                    Else
                        If rsVaryn = "Y" Then ' NOTE : 이형 수혈 일 경우
                            chk_Over_Tat = 5
                        Else                  ' NOTE : 이형 수혈 아닐 경우
                            chk_Over_Tat = 30
                        End If
                    End If
                End If

            Else ' NOTE : RBC 성분제재 아닐 때 

                Dim chk_Other As String = PRG_CONST.OTHER_COM_YN(rsComcd)
                Select Case chk_Other
                    Case "FFP"
                        chk_YN = True
                        chk_Over_Tat = 60
                    Case "PLT", "CRYO"       ' TODO : 기준 필요 아직 미정
                    Case "IRRA"
                        chk_YN = True
                        chk_Over_Tat = 240
                End Select
            End If

            If chk_YN Then
                Return If(CInt(rsTAT_mi) > chk_Over_Tat, True, False)
            Else
                Return False
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Function

    Private Sub sbQuery()
        Dim sRegno As String = ""       '등록번호
        Dim sPatnm As String = ""       '환자성명
        Dim sSexage As String = ""      '성별/나이
        Dim sDeptnm As String = ""      '부서명
        Dim sDoctornm As String = ""    '의뢰의사
        Dim sWs As String = ""          '병동/병실
        Dim sVaryn As String = ""       '이형수혈구분
        Dim sIogbn As String = ""       '외래구분
        Dim sPataborh As String = ""    '환자abo
        Dim sTnsjubsuno As String = ""  '접수번호
        Dim sBldno As String = ""       '혈액번호
        Dim sBldaborh As String = ""    '혈액abo
        Dim sComcd_out As String = ""   '출고성분제재
        Dim sComnm As String = ""       '성분제재명
        Dim sTnsgbn As String = ""      '처방구분
        Dim sState As String = ""       '실시구분
        Dim sOrddt As String = ""       '처방일시
        Dim sCrosstkdt As String = ""   '보관검체에 대한 접수일시
        Dim sFstrgstdt As String = ""   '혈액불출요청일시
        Dim sBefoutdt As String = ""    '가출고일시
        Dim sOutdt As String = ""       '출고일시
        Dim sB1 As String = ""          '접수/불출요청일시 중 큰것 ~ 가출고 시간 차
        Dim sB2 As String = ""          '가출고 ~ 출고 시간 차
        Dim sBtat1 As String = ""       '접수/불출요청일시 중 큰것 ~ 가출고 시간 차
        Dim sBtat2 As String = ""       '접수/불출요청일시 중 큰것 ~ 출고 시간 차
        Dim sBtat1_mi As String = ""    '접수/불출요청일시 중 큰것 ~ 가출고 시간 차_type_minute
        Dim sBtat2_mi As String = ""    '접수/불출요청일시 중 큰것 ~ 출고 시간 차_type_minute

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 데이타 조회중... -> 데이타량에 따라 다소 시간이 걸리므로 잠시만 기다려 주십시오.")

            '    Dim sEmerYN As String = ""

            '    If chkEmer.Checked And chkNotEmer.Checked Then
            '        sEmerYN = ""
            '    ElseIf chkEmer.Checked Then
            '        sEmerYN = "Y"
            '    ElseIf chkNotEmer.Checked Then
            '        sEmerYN = "N"
            '    End If

            Dim dt As DataTable = CGDA_BT.fnGet_BloodTat(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""))

            '    Dim sSelect As String = ""

            '    If Me.rdoBaseTkDt.Checked Then
            '        sSortBy = "tkdt, bcno, sort_slip, sort_test, testcd"
            '    Else
            '        sSortBy = "sort_slip, slipcd, tkdt, sort_test, testcd"
            '    End If

            '    a_dr = dt.Select(sSelect, sSortBy)

            '    dt = Fn.ChangeToDataTable(a_dr)

            '    If dt.Rows.Count > 0 Then
            '        mbQuery = True
            '        pnlMainBtn.Enabled = False

            '        Dim bldFlag As Boolean = False

            If dt.Rows.Count > 0 Then
                With spdList
                    .ReDraw = False
                    .MaxRows = dt.Rows.Count
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                        Application.DoEvents()

                        ' 중간 취소
                        If mbEscape = True Then Exit For
                        DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 리스트 표시중... [" & (ix + 1).ToString & "/" & dt.Rows.Count.ToString & "] ->  표시 취소는 Esc Key를 눌러 주십시오.")

                        .Row = ix + 1
                        sRegno = dt.Rows(ix).Item("regno").ToString.Trim
                        sPatnm = dt.Rows(ix).Item("patnm").ToString.Trim
                        sSexage = dt.Rows(ix).Item("sexage").ToString.Trim
                        sDeptnm = dt.Rows(ix).Item("deptnm").ToString.Trim
                        sDoctornm = dt.Rows(ix).Item("doctornm").ToString.Trim
                        sWs = dt.Rows(ix).Item("ws").ToString.Trim
                        sVaryn = dt.Rows(ix).Item("varyn").ToString.Trim
                        sIogbn = dt.Rows(ix).Item("iogbn").ToString.Trim
                        sPataborh = dt.Rows(ix).Item("pataborh").ToString.Trim
                        sTnsjubsuno = dt.Rows(ix).Item("tnsjubsuno").ToString.Trim
                        sBldno = dt.Rows(ix).Item("bldno").ToString.Trim
                        sBldaborh = dt.Rows(ix).Item("bldaborh").ToString.Trim
                        sComcd_out = dt.Rows(ix).Item("comcd_out").ToString.Trim
                        sComnm = dt.Rows(ix).Item("comnm").ToString.Trim
                        sTnsgbn = dt.Rows(ix).Item("tnsgbn").ToString.Trim
                        sState = dt.Rows(ix).Item("state").ToString.Trim
                        sOrddt = dt.Rows(ix).Item("orddt").ToString.Trim
                        sCrosstkdt = dt.Rows(ix).Item("crosstkdt").ToString.Trim
                        sFstrgstdt = dt.Rows(ix).Item("fstrgstdt").ToString.Trim
                        sBefoutdt = dt.Rows(ix).Item("befoutdt").ToString.Trim
                        sOutdt = dt.Rows(ix).Item("outdt").ToString.Trim
                        sB1 = dt.Rows(ix).Item("b1").ToString.Trim
                        sB2 = dt.Rows(ix).Item("b2").ToString.Trim
                        sBtat1 = dt.Rows(ix).Item("btat1").ToString.Trim
                        sBtat2 = dt.Rows(ix).Item("btat2").ToString.Trim
                        sBtat1_mi = dt.Rows(ix).Item("btat1_mi").ToString.Trim
                        sBtat2_mi = dt.Rows(ix).Item("btat2_mi").ToString.Trim

                        .Col = .GetColFromID("regno") : .Text = sRegno
                        .Col = .GetColFromID("patnm") : .Text = sPatnm
                        .Col = .GetColFromID("sexage") : .Text = sSexage
                        .Col = .GetColFromID("deptnm") : .Text = sDeptnm
                        .Col = .GetColFromID("doctornm") : .Text = sDoctornm
                        .Col = .GetColFromID("ws") : .Text = sWs
                        .Col = .GetColFromID("varyn") : .Text = sVaryn
                        .Col = .GetColFromID("pataborh") : .Text = sPataborh
                        .Col = .GetColFromID("tnsjubsuno") : .Text = sTnsjubsuno
                        .Col = .GetColFromID("bldno") : .Text = sBldno
                        .Col = .GetColFromID("bldaborh") : .Text = sBldaborh
                        .Col = .GetColFromID("comcd_out") : .Text = sComcd_out
                        .Col = .GetColFromID("comnm") : .Text = sComnm
                        .Col = .GetColFromID("tnsgbn") : .Text = sTnsgbn
                        .Col = .GetColFromID("state") : .Text = sState
                        .Col = .GetColFromID("orddt") : .Text = sOrddt
                        .Col = .GetColFromID("crosstkdt") : .Text = sCrosstkdt
                        .Col = .GetColFromID("fstrgstdt") : .Text = sFstrgstdt
                        .Col = .GetColFromID("befoutdt") : .Text = sBefoutdt
                        .Col = .GetColFromID("outdt") : .Text = sOutdt
                        .Col = .GetColFromID("b1") : .Text = sB1
                        .Col = .GetColFromID("b2") : .Text = sB2
                        .Col = .GetColFromID("btat1") : .Text = sBtat1 : If fn_chk_bldTAT(sVaryn, sComcd_out, sTnsgbn, sIogbn, sPataborh, sBldaborh, sBtat1_mi) Then .BackColor = System.Drawing.Color.Red : .FontBold = True
                        .Col = .GetColFromID("btat2") : .Text = sBtat2 : If fn_chk_bldTAT(sVaryn, sComcd_out, sTnsgbn, sIogbn, sPataborh, sBldaborh, sBtat2_mi) Then .BackColor = System.Drawing.Color.Red : .FontBold = True

                    Next
                End With
            Else
                Me.spdList.MaxRows = 0
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 데이타가 없습니다.")
            End If





        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            DS_StatusBar.setTextStatusBar("")
            Cursor.Current = System.Windows.Forms.Cursors.Default
            If mbEscape = True Then
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "리스트 표시를 중단 했습니다.")
            End If
            mbQuery = False
            mbEscape = False
            pnlMainBtn.Enabled = True
        End Try

    End Sub

End Class