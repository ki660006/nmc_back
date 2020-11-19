'>> TAT 시간대 통계
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports common.commlogin.login
Imports LISAPP.APP_T

Public Class FGT03
    Inherits System.Windows.Forms.Form

    Private miSelectKey As Integer = 0
    Private miMaxDiffDay As Integer = 60
    Private miMaxDiffMonth As Integer = 12

    Private mbQuery As Boolean = False
    Private mbEscape As Boolean = False

    Private Sub sbInitialize()
        Dim sFn As String = "Private Sub sbInitialize()"

        Try
            miSelectKey = 1
            Dim sCurSysDate As String = ""

            sCurSysDate = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")

            Me.dtpDate0.Value = CType(sCurSysDate + " 00:00", Date)
            Me.dtpDate1.Value = CType(sCurSysDate + " 23:59", Date)

            Me.cboSlip.SelectedIndex = 0

            Me.rdoDeptA.Checked = True
            Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False
            '------------------------------
            Me.rdoIOA.Checked = True
            Me.rdoWardA.Checked = True
            Me.pnlWard.Enabled = False
            Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False
            '------------------------------

            Me.rdoTestA.Checked = True
            Me.spdTest.MaxRows = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    ' 파트명 가져오기
    Private Sub sbDisplay_slip()

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_Slip_List()

            If dt.Rows.Count > 0 Then
                Me.cboSlip.Items.Clear()

                For intCnt As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(intCnt)
                        Dim sTmp As String = "[" + .Item("slipcd").ToString + "] " + .Item("slipnmd").ToString
                        Me.cboSlip.Items.Add(sTmp)
                    End With
                Next
            End If
            dt.Dispose()

            Me.cboSlip.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbGetDeptInfo()
        Try
            Dim dt As DataTable= OCSAPP.OcsLink.SData.fnGet_DeptList

            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Me.cboDept.Items.Add(dt.Rows(ix).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(ix).Item("deptcd").ToString)
                Next
            End If

            If Me.cboDept.Items.Count > 0 Then Me.cboDept.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbGetWardInfo()

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList

            Me.cboWard.Items.Clear()

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Me.cboWard.Items.Add(dt.Rows(ix).Item("wardnm").ToString + Space(200) + "|" + dt.Rows(ix).Item("wardno").ToString)
                Next
            End If

            If Me.cboWard.Items.Count > 0 Then Me.cboWard.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_test()

        Try
            Dim iCol As Integer = 0

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list(Ctrl.Get_Code(Me.cboSlip), "", "")

            Me.spdTest.MaxRows = 0

            If dt Is Nothing Then Return
            Dim dr As DataRow()

            dr = dt.Select("tcdgbn IN ('S', 'P', 'B')", "")
            dt = Fn.ChangeToDataTable(dr)

            If dt.Rows.Count > 0 Then
                With spdTest
                    .ReDraw = False
                    .MaxRows = dt.Rows.Count

                    For ix1 As Integer = 0 To dt.Rows.Count - 1

                        For ix2 As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(ix2).ColumnName.ToLower)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = ix1 + 1
                                .Text = dt.Rows(ix1).Item(ix2).ToString
                            End If
                        Next
                    Next

                    .ReDraw = True
                End With
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Function fnGet_DateDiff(ByVal rsDate1 As String, ByVal rsDate2 As String) As String

        Dim dtpDate1 As Date
        Dim dtpDate2 As Date
        Dim lngHH As Long = 0

        dtpDate1 = CDate(rsDate1)
        dtpDate2 = CDate(rsDate2)

        lngHH = DateDiff(DateInterval.Minute, dtpDate1, dtpDate2)

        fnGet_DateDiff = Convert.ToString(lngHH)

    End Function

    Private Sub fntQuery0()

        Dim dt As New DataTable
        Dim sKey As String = ""

        Try

            DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 데이타 조회중... -> 데이타량에 따라 다소 시간이 걸리므로 잠시만 기다려 주십시오.")

            Dim sRstFlg As String = "3"
            Dim sEmerYN As String = ""
            Dim sPartSlip As String = ""
            Dim sDeptCd As String = ""
            Dim sWardNo As String = ""
            Dim sIOgbn As String = ""
            Dim alTests As New ArrayList

            If Me.cboWard.Text.IndexOf("|"c) >= 0 Then sWardNo = Me.cboWard.Text.Split("|"c)(1)

            '< 20121012 통계시 소견있는것만 조회하는 기능 

            Dim sVerity As String = ""

            ' 0 : 사유전체조회 , 1:사유제외조회 , 2 : 사유건수만 조회
            If Me.chkVerOnly.Checked = False And Me.chkVerity.Checked = False Then
                sVerity = "0"
            ElseIf Me.chkVerOnly.Checked = False And Me.chkVerity.Checked = True Then
                sVerity = "1"
            ElseIf Me.chkVerOnly.Checked = True And Me.chkVerity.Checked = False Then
                sVerity = "2"
            End If



            If Me.rdoRstFlgM.Checked Then sRstFlg = "2"

            If Me.rdoIOO.Checked Then
                sIOgbn = "O"
            ElseIf Me.rdoIOI.Checked Then
                sIOgbn = "I"
            ElseIf Me.rdoIOC.Checked Then
                sIOgbn = "C"
            End If

            If Me.cboSlip.SelectedIndex >= 0 Then sPartSlip = Ctrl.Get_Code(Me.cboSlip)
            If Me.cboDept.SelectedIndex >= 0 Then sDeptCd = Me.cboDept.Text.Split("|"c)(1)

            If Me.rdoEmerA.Checked Then
                sEmerYN = ""
            ElseIf Me.rdoEmerY.Checked Then
                sEmerYN = "Y"
            ElseIf Me.rdoEmerN.Checked Then
                sEmerYN = "N"
            ElseIf Me.rdoEmerB.Checked Then
                sEmerYN = "B"
            End If

            If Me.rdoTestS.Checked Then
                With spdTest
                    For iRow As Integer = 1 To spdTest.MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("chk")
                        If .Text = "1" Then
                            .Col = .GetColFromID("testcd") : Dim sExmCd As String = .Text
                            alTests.Add(sExmCd)
                        End If
                    Next
                End With
            End If


            '< 20121012 통계조회에 사유건수만 조회기능 추가 
            'dt = (New SrhFn).fnGet_TatTime_Statistics(IIf(Me.rdoBaseTst.Checked, "", "O").ToString(), sRstFlg, Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), _
            '                                          sPartSlip, sDeptCd, sWardNo, sIOgbn, sEmerYN, alTests, Me.chkVerity.Checked, Me.chkPDCA.Checked)
            dt = (New SrhFn).fnGet_TatTime_Statistics(IIf(Me.rdoBaseTst.Checked, "", "O").ToString(), sRstFlg, Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), _
                                                      sPartSlip, sDeptCd, sWardNo, sIOgbn, sEmerYN, alTests, sVerity, Me.chkPDCA.Checked)
            '>

            '< add freety 2007/01/23 : 정렬기준 접수일시와 검체번호로 분리
            Dim alExm As New ArrayList

            Dim lgCnt_cur(Me.spdList.MaxCols - Me.spdList.GetColFromID("TAT 율(%)") - 1) As Long
            Dim lgCnt_tot0 As Long  '-- 유효건수
            Dim lgCnt_tot1 As Long  '-- 전체건수
            Dim lgSum_tat As Long   '-- 시간합계

            Dim dbCnt_tot(Me.spdList.MaxCols - Me.spdList.GetColFromID("TAT 율(%)") - 1) As Double
            Dim dbCnt_tot0 As Double      '-- 유효건수
            Dim dbCnt_tot1 As Double      '-- 전체건수
            Dim dbSum_tat As Double       '-- 시간합계

            Dim sSortBy As String = "dispseql, testcd, spccd"
            Dim a_dr() As DataRow

            a_dr = dt.Select("", sSortBy)
            dt = Fn.ChangeToDataTable(a_dr)
            '>

            If dt.Rows.Count < 1 Then Return

            mbQuery = True

            With Me.spdList
                .MaxRows = 0

                For ix As Integer = 0 To dt.Rows.Count - 1

                    Application.DoEvents()

                    ' 중간 취소
                    If mbEscape = True Then Exit For
                    DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 시간대별 통계 표시중... [" + (ix + 1).ToString & "/" + dt.Rows.Count.ToString & "] ->  표시 취소는 Esc Key를 눌러 주십시오.")


                    sKey = a_dr(ix).Item("testcd").ToString.PadRight(8) + a_dr(ix).Item("spccd").ToString()

                    If alExm.Contains(sKey) = False Then

                        If .MaxRows > 0 Then
                            For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                                Dim iCol As Integer = .GetColFromID("TAT 율(%)")

                                If Me.rdoCntGbn1.Checked Then
                                    .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2) / lgCnt_tot1 * 100, "#,###0"))
                                Else
                                    .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2), "#,####"))
                                End If
                            Next

                            .SetText(.GetColFromID("유효건수"), .MaxRows, Format(lgCnt_tot0, "#,####"))
                            .SetText(.GetColFromID("전체건수"), .MaxRows, Format(lgCnt_tot1, "#,####"))
                            .SetText(.GetColFromID("TAT 율(%)"), .MaxRows, Format((lgCnt_tot0 / lgCnt_tot1) * 100, "0.00"))


                            If lgCnt_tot0 > 0 Then
                                lgSum_tat = Convert.ToInt64(lgSum_tat / lgCnt_tot0)
                                Dim lgHH As Long = Convert.ToInt64(lgSum_tat / 60)
                                Dim lgMM As Long = lgSum_tat Mod 60

                                If lgHH * 60 + lgMM > lgSum_tat Then lgHH -= 1

                                .SetText(.GetColFromID("TAT"), .MaxRows, Format(lgHH, "00") + ":" + Format(lgMM, "00"))
                                '여기
                            Else

                            End If
                        End If

                        .MaxRows += 1
                        alExm.Add(sKey)

                        Dim iRow As Integer = .MaxRows

                        .Row = .MaxRows
                        .Col = .GetColFromID("검사코드") : .Text = sKey
                        .Col = .GetColFromID("검사명") : .Text = a_dr(ix).Item("tnmd").ToString
                        .Col = .GetColFromID("검체명") : .Text = a_dr(ix).Item("spcnmd").ToString

                        Dim sTatF As String = a_dr(ix).Item("tmi").ToString

                        If sTatF <> "" Then
                            Dim iHours As Double = 0
                            Dim dMin As Double = 0.0
                            Dim dSec As Double = 0.0

                            'iHours = CInt(CInt(sTatF) / 60)
                            iHours = CDbl(sTatF) / 60 '2019-05-08 소수점 결과를 Cint할 경우 반올림 되므로 버림으로 처리

                            iHours = Math.Floor(iHours)

                            dMin = CDbl(sTatF) Mod 60


                            sTatF = CStr(iHours) + "h" + CStr(dMin) + "m"
                        End If

                        .Col = .GetColFromID("TAT기준") : .Text = sTatF


                        If a_dr(ix).Item("tmi").ToString <> "" Then

                            '.Col = .GetColFromID("TAT 기준") : .Text = a_dr(ix).Item("tmi").ToString

                            Dim iThh As Integer = 0
                            Dim iTmm As Integer = 0
                            Dim sTmp As String = ""
                            Dim iPos As Integer = 0

                            iThh = Convert.ToInt16(Val(a_dr(ix).Item("tmi").ToString) / 60)
                            iTmm = Convert.ToInt16(Val(a_dr(ix).Item("tmi").ToString) Mod 60)

                            If iThh * 60 + iTmm > Val(a_dr(ix).Item("tmi").ToString) Then iThh -= 1

                            sTmp = Format(iThh, "00").ToString + ":" + Format(iTmm, "00").ToString

                            For iCol As Integer = .GetColFromID("TAT 율(%)") + 1 To .MaxCols - 1
                                .Row = 0
                                .Col = iCol

                                If sTmp >= .Text Then
                                    iPos = iCol
                                Else
                                    Exit For
                                End If
                            Next

                            If iPos > 0 Then
                                .Row = .MaxRows
                                .Col = iPos + 1
                                .BackColor = lblColor.BackColor
                                .ForeColor = lblColor.ForeColor
                            End If
                        End If

                        For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                            lgCnt_cur(ix2) = 0
                        Next

                        lgCnt_tot0 = 0
                        lgCnt_tot1 = 0
                        lgSum_tat = 0
                    End If

                    Dim sTAT_M As String = a_dr(ix).Item("tat_mi").ToString
                    Dim sTAT_S As String = a_dr(ix).Item("tat_ss").ToString

                    If sTAT_M.IndexOf(".") = 0 Then
                        sTAT_M = "0"
                    ElseIf sTAT_M.IndexOf(".") > 0 Then
                        sTAT_M = sTAT_M.Substring(0, sTAT_M.IndexOf("."))
                    End If
                    '<<<20170609 초로계산시 예로 15:59 초는 15분 에 걸려서 수정 
                    'Dim iTatMM As Integer = (Convert.ToInt32(Me.txtTat.Text)) * CType(IIf(Me.cboTat.Text = "분", 60, IIf(Me.cboTat.Text = "시간", 60 * 60, 24 * 60 * 60)), Integer)
                    Dim iTatMM As Integer = (Convert.ToInt32(Me.txtTat.Text)) * CType(IIf(Me.cboTat.Text = "분", 60, IIf(Me.cboTat.Text = "시간", 60 * 60, 24 * 60 * 60)), Integer)
                    '>>>

                    '<<<20170711 목표TAT 시간 + 59초 까지는 허용 가능하게 
                    iTatMM = iTatMM + 59
                    '>>>20170711

                    Dim iTatTerm As Integer = Convert.ToInt32(Me.txtTerm.Text) * CType(IIf(Me.cboTerm.Text = "초", 1, IIf(Me.cboTerm.Text = "분", 60, 60 * 60)), Integer)

                    For ix2 As Integer = 1 To lgCnt_cur.Length - 1
                        If Val(sTAT_S) > (ix2 - 1) * iTatTerm And Val(sTAT_S) <= ix2 * iTatTerm Then '구간안에 카운트 조건 중복안되도록 수정 YJY
                            lgCnt_cur(ix2 - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                            dbCnt_tot(ix2 - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)


                            '-- 시간합계
                            lgSum_tat += Convert.ToInt64(sTAT_M) * Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                            dbSum_tat += Val(sTAT_M) * Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)

                            ''-- 유효건수
                            'lgCnt_tot0 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                            'dbCnt_tot0 += Val(a_dr(ix).Item("totcnt").ToString)

                            ''-- 전체건수
                            'lgCnt_tot1 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                            'dbCnt_tot1 += Val(a_dr(ix).Item("totcnt").ToString)

                        End If
                    Next

                    ''-- 유효건수
                    'lgCnt_tot0 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                    'dbCnt_tot0 += Val(a_dr(ix).Item("totcnt").ToString)

                    ''-- 전체건수
                    'lgCnt_tot1 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                    'dbCnt_tot1 += Val(a_dr(ix).Item("totcnt").ToString)

                    ''-- 기타건수
                    'If Val(sTAT_S) > iTatMM Then
                    '    lgCnt_cur(lgCnt_cur.Length - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                    '    dbCnt_tot(lgCnt_cur.Length - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)

                    '    '-- 전체건수
                    '    lgCnt_tot1 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                    '    dbCnt_tot1 += Val(a_dr(ix).Item("totcnt").ToString)
                    'End If

                    '<<<20170522 TAT 통계 전체건수,초과건수가 맞지 않아서 수정 진행 
                    '-- 기타건수
                    '<<<20170609 초로계산시 예로 15:59 초는 15분 에 걸려서 수정 
                    'If Val(sTAT_S) > iTatMM Then
                    If Val(sTAT_S) >= iTatMM Then
                        '>>>20170609 
                        lgCnt_cur(lgCnt_cur.Length - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                        dbCnt_tot(lgCnt_cur.Length - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)

                        '-- 전체건수
                        lgCnt_tot1 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                        dbCnt_tot1 += Val(a_dr(ix).Item("totcnt").ToString)
                    Else
                        '-- 유효건수
                        lgCnt_tot0 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                        dbCnt_tot0 += Val(a_dr(ix).Item("totcnt").ToString)

                        '-- 전체건수
                        lgCnt_tot1 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                        dbCnt_tot1 += Val(a_dr(ix).Item("totcnt").ToString)
                    End If
                    '>>>20170522


                Next

                If .MaxRows > 0 Then
                    For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                        Dim iCol As Integer = .GetColFromID("TAT 율(%)")

                        If Me.rdoCntGbn1.Checked Then
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2) / lgCnt_tot1 * 100, "#,###0"))
                        Else
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2), "#,####"))
                        End If
                    Next

                    .SetText(.GetColFromID("유효건수"), .MaxRows, Format(lgCnt_tot0, "#,####"))
                    .SetText(.GetColFromID("전체건수"), .MaxRows, Format(lgCnt_tot1, "#,####"))
                    .SetText(.GetColFromID("TAT 율(%)"), .MaxRows, Format((lgCnt_tot0 / lgCnt_tot1) * 100, "0.00"))

                    Dim lgHH As Long = 0
                    Dim lgMM As Long = 0

                    If lgCnt_tot0 > 0 Then
                        lgSum_tat = Convert.ToInt64(lgSum_tat / lgCnt_tot0)
                        lgHH = Convert.ToInt64(lgSum_tat / 60)
                        lgMM = lgSum_tat Mod 60

                        If lgHH * 60 + lgMM > lgSum_tat Then lgHH -= 1

                        .SetText(.GetColFromID("TAT"), .MaxRows, Format(lgHH, "00") + ":" + Format(lgMM, "00"))
                        '여기

                    End If

                    .Row = 1
                    .MaxRows += 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                    .SetText(.GetColFromID("검사명"), 1, "Totoal")

                    For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                        Dim iCol As Integer = .GetColFromID("TAT 율(%)")

                        If Me.rdoCntGbn1.Checked Then
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), 1, Format(dbCnt_tot(ix2) / dbCnt_tot1 * 100, "#,###0"))
                        Else
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), 1, Format(dbCnt_tot(ix2), "#,####"))
                        End If
                    Next

                    .SetText(.GetColFromID("유효건수"), 1, Format(dbCnt_tot0, "#,####"))
                    .SetText(.GetColFromID("전체건수"), 1, Format(dbCnt_tot1, "#,####"))
                    .SetText(.GetColFromID("TAT 율(%)"), 1, Format((dbCnt_tot0 / dbCnt_tot1) * 100, "0.00"))

                    If dbCnt_tot0 > 0 Then
                        dbSum_tat = dbSum_tat / dbCnt_tot0
                        lgHH = Convert.ToInt64(dbSum_tat / 60)
                        lgMM = Convert.ToInt64(dbSum_tat Mod 60)

                        .SetText(.GetColFromID("TAT"), 1, Format(lgHH, "00") + ":" + Format(lgMM, "00"))
                        '여기

                    End If

                End If

            End With


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            DS_StatusBar.setTextStatusBar("")
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            If mbEscape = True Then
                MsgBox("리스트 표시를 중단 했습니다.", MsgBoxStyle.Information, Me.Text)
            End If

            mbQuery = False
            mbEscape = False
        End Try

    End Sub

    '누적 건수 및 누적 퍼센트
    Private Sub fntQuery1()

        Dim dt As New DataTable
        Dim sKey As String = ""

        Try

            DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 데이타 조회중... -> 데이타량에 따라 다소 시간이 걸리므로 잠시만 기다려 주십시오.")

            Dim sRstFlg As String = "3"
            Dim sEmerYN As String = ""
            Dim sPartSlip As String = ""
            Dim sDeptCd As String = ""
            Dim sWardNo As String = Me.cboWard.Text.Split("|"c)(1)
            Dim sIOgbn As String = ""
            Dim alTests As New ArrayList

            '< 20121012 통계시 소견있는것만 조회하는 기능 

            Dim sVerity As String = ""

            ' 0 : 사유전체조회 , 1:사유제외조회 , 2 : 사유건수만 조회
            If Me.chkVerOnly.Checked = False And Me.chkVerity.Checked = False Then
                sVerity = "0"
            ElseIf Me.chkVerOnly.Checked = False And Me.chkVerity.Checked = True Then
                sVerity = "1"
            ElseIf Me.chkVerOnly.Checked = True And Me.chkVerity.Checked = False Then
                sVerity = "2"
            End If

            '> 20121012 통계시 소견있는것만 조회하는 기능 

            If Me.rdoRstFlgM.Checked Then sRstFlg = "2"

            If Me.rdoIOO.Checked Then
                sIOgbn = "O"
            ElseIf Me.rdoIOI.Checked Then
                sIOgbn = "I"
            ElseIf Me.rdoIOC.Checked Then
                sIOgbn = "C"
            End If

            If Me.cboSlip.SelectedIndex >= 0 Then sPartSlip = Ctrl.Get_Code(Me.cboSlip)
            If Me.cboDept.SelectedIndex >= 0 Then sDeptCd = Me.cboDept.Text.Split("|"c)(1)

            If Me.rdoEmerA.Checked Then
                sEmerYN = ""
            ElseIf Me.rdoEmerY.Checked Then
                sEmerYN = "Y"
            ElseIf Me.rdoEmerN.Checked Then
                sEmerYN = "N"
            ElseIf Me.rdoEmerB.Checked Then
                sEmerYN = "B"
            End If

            If Me.rdoTestS.Checked Then
                With spdTest
                    For iRow As Integer = 1 To spdTest.MaxRows
                        .Row = iRow
                        .Col = .GetColFromID("chk")
                        If .Text = "1" Then
                            .Col = .GetColFromID("testcd") : Dim sExmCd As String = .Text
                            alTests.Add(sExmCd)
                        End If
                    Next
                End With
            End If

            '< 20121012 통계조회에 사유건수만 조회기능 추가 
            'dt = (New SrhFn).fnGet_TatTime_Statistics(IIf(Me.rdoBaseTst.Checked, "", "O").ToString(), sRstFlg, Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), _
            '                                          sPartSlip, sDeptCd, sWardNo, sIOgbn, sEmerYN, alTests, Me.chkVerity.Checked, Me.chkPDCA.Checked)
            dt = (New SrhFn).fnGet_TatTime_Statistics(IIf(Me.rdoBaseTst.Checked, "", "O").ToString(), sRstFlg, Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), _
                                                      sPartSlip, sDeptCd, sWardNo, sIOgbn, sEmerYN, alTests, sVerity, Me.chkPDCA.Checked)
            '>

            '< add freety 2007/01/23 : 정렬기준 접수일시와 검체번호로 분리
            Dim alExm As New ArrayList

            Dim lgCnt_cur(Me.spdList.MaxCols - Me.spdList.GetColFromID("TAT 율(%)")) As Long
            Dim lgCnt_tot0 As Long  '-- 유효건수
            Dim lgCnt_tot1 As Long  '-- 전체건수
            Dim lgSum_tat As Long  '-- 시간합계

            Dim dbCnt_tot(Me.spdList.MaxCols - Me.spdList.GetColFromID("TAT 율(%)")) As Double
            Dim dbCnt_tot0 As Double      '-- 유효건수
            Dim dbCnt_tot1 As Double      '-- 전체건수
            Dim dbSum_tat As Double        '-- 시간합계

            Dim sSortBy As String = "dispseql, testcd, spccd"
            Dim a_dr() As DataRow

            a_dr = dt.Select("", sSortBy)
            dt = Fn.ChangeToDataTable(a_dr)
            '>

            If dt.Rows.Count < 1 Then Return

            mbQuery = True

            With Me.spdList
                .MaxRows = 0

                For ix As Integer = 0 To dt.Rows.Count - 1

                    Application.DoEvents()

                    ' 중간 취소
                    If mbEscape = True Then Exit For
                    DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 시간대별 통계 표시중... [" + (ix + 1).ToString & "/" + dt.Rows.Count.ToString & "] ->  표시 취소는 Esc Key를 눌러 주십시오.")


                    sKey = a_dr(ix).Item("testcd").ToString.PadRight(8) + a_dr(ix).Item("spccd").ToString()

                    If alExm.Contains(sKey) = False Then

                        If .MaxRows > 0 Then
                            For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                                Dim iCol As Integer = .GetColFromID("TAT 율(%)")

                                If Me.rdoCntGbn3.Checked Then
                                    .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2) / lgCnt_tot1 * 100, "#,###0"))
                                Else
                                    .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2), "#,####"))
                                End If
                            Next

                            .SetText(.GetColFromID("유효건수"), .MaxRows, Format(lgCnt_tot0, "#,####"))
                            .SetText(.GetColFromID("전체건수"), .MaxRows, Format(lgCnt_tot1, "#,####"))
                            .SetText(.GetColFromID("TAT 율(%)"), .MaxRows, Format((lgCnt_tot0 / lgCnt_tot1) * 100, "0.00"))

                            If lgCnt_tot0 > 0 Then
                                lgSum_tat = Convert.ToInt64(lgSum_tat / lgCnt_tot0)
                                Dim lgHH As Long = Convert.ToInt64(lgSum_tat / 60)
                                Dim lgMM As Long = lgSum_tat Mod 60

                                .SetText(.GetColFromID("TAT"), .MaxRows, Format(lgHH, "00") + ":" + Format(lgMM, "00"))
                            Else

                            End If
                        End If

                        .MaxRows += 1
                        alExm.Add(sKey)

                        Dim iRow As Integer = .MaxRows

                        .Row = .MaxRows
                        .Col = .GetColFromID("검사코드") : .Text = sKey
                        .Col = .GetColFromID("검사명") : .Text = a_dr(ix).Item("tnmd").ToString
                        .Col = .GetColFromID("검체명") : .Text = a_dr(ix).Item("spcnmd").ToString

                        If a_dr(ix).Item("tmi").ToString <> "" Then

                            Dim iThh As Integer = 0
                            Dim iTmm As Integer = 0
                            Dim sTmp As String = ""
                            Dim iPos As Integer = 0

                            iThh = Convert.ToInt16(Val(a_dr(ix).Item("tmi").ToString) / 60)
                            iTmm = Convert.ToInt16(Val(a_dr(ix).Item("tmi").ToString) Mod 60)

                            sTmp = Format(iThh, "00").ToString + ":" + Format(iTmm, "00").ToString

                            For iCol As Integer = .GetColFromID("TAT 율(%)") + 1 To .MaxCols - 1
                                .Row = 0
                                .Col = iCol

                                If sTmp >= .Text Then
                                    iPos = iCol
                                Else
                                    Exit For
                                End If
                            Next

                            If iPos > 0 Then
                                .Row = .MaxRows
                                .Col = iPos + 1
                                .BackColor = lblColor.BackColor
                                .ForeColor = lblColor.ForeColor
                            End If
                        End If

                        For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                            lgCnt_cur(ix2) = 0
                        Next

                        lgCnt_tot0 = 0
                        lgCnt_tot1 = 0
                        lgSum_tat = 0
                    End If

                    Dim sTAT_M As String = a_dr(ix).Item("tat_mi").ToString
                    Dim sTAT_S As String = a_dr(ix).Item("tat_ss").ToString

                    If sTAT_M.IndexOf(".") = 0 Then
                        sTAT_M = "0"
                    ElseIf sTAT_M.IndexOf(".") > 0 Then
                        sTAT_M = sTAT_M.Substring(0, sTAT_M.IndexOf("."))
                    End If

                    Dim iTatMM As Integer = Convert.ToInt32(Me.txtTat.Text) * CType(IIf(Me.cboTat.Text = "분", 60, IIf(Me.cboTat.Text = "시간", 60 * 60, 24 * 60 * 60)), Integer)
                    Dim iTatTerm As Integer = Convert.ToInt32(Me.txtTerm.Text) * CType(IIf(Me.cboTerm.Text = "초", 1, IIf(Me.cboTerm.Text = "분", 60, 60 * 60)), Integer)

                    For ix2 As Integer = 0 To lgCnt_cur.Length - 2
                        If Val(sTAT_S) <= (ix2 + 1) * iTatTerm Then
                            lgCnt_cur(ix2) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                            dbCnt_tot(ix2) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                        End If
                    Next

                    lgCnt_cur(lgCnt_cur.Length - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                    dbCnt_tot(lgCnt_cur.Length - 1) += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)

                    If Val(sTAT_S) <= iTatMM Then
                        lgCnt_tot0 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                        lgSum_tat += Convert.ToInt64(sTAT_M) * Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)

                        dbCnt_tot0 += Val(a_dr(ix).Item("totcnt").ToString)
                        dbSum_tat += Val(sTAT_M) * Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                    End If

                    lgCnt_tot1 += Convert.ToInt64(a_dr(ix).Item("totcnt").ToString)
                    dbCnt_tot1 += Val(a_dr(ix).Item("totcnt").ToString)

                Next

                If .MaxRows > 0 Then
                    For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                        Dim iCol As Integer = .GetColFromID("TAT 율(%)")

                        If Me.rdoCntGbn3.Checked Then
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2) / lgCnt_tot1 * 100, "#,###0"))
                        Else
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), .MaxRows, Format(lgCnt_cur(ix2), "#,####"))
                        End If
                    Next

                    .SetText(.GetColFromID("유효건수"), .MaxRows, Format(lgCnt_tot0, "#,####"))
                    .SetText(.GetColFromID("전체건수"), .MaxRows, Format(lgCnt_tot1, "#,####"))
                    .SetText(.GetColFromID("TAT 율(%)"), .MaxRows, Format((lgCnt_tot0 / lgCnt_tot1) * 100, "0.00"))

                    Dim lgHH As Long = 0
                    Dim lgMM As Long = 0

                    If lgCnt_tot0 > 0 Then
                        lgSum_tat = Convert.ToInt64(lgSum_tat / lgCnt_tot0)
                        lgHH = Convert.ToInt64(lgSum_tat / 60)
                        lgMM = lgSum_tat Mod 60

                        .SetText(.GetColFromID("TAT"), .MaxRows, Format(lgHH, "00") + ":" + Format(lgMM, "00"))
                    End If

                    .Row = 1
                    .MaxRows += 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                    .SetText(.GetColFromID("검사명"), 1, "Totoal")

                    For ix2 As Integer = 0 To lgCnt_cur.Length - 1
                        Dim iCol As Integer = .GetColFromID("TAT 율(%)")

                        If Me.rdoCntGbn3.Checked Then
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), 1, Format(dbCnt_tot(ix2) / dbCnt_tot1 * 100, "#,###0"))
                        Else
                            .SetText(1 + ix2 + .GetColFromID("TAT 율(%)"), 1, Format(dbCnt_tot(ix2), "#,####"))
                        End If
                    Next

                    .SetText(.GetColFromID("유효건수"), 1, Format(dbCnt_tot0, "#,####"))
                    .SetText(.GetColFromID("전체건수"), 1, Format(dbCnt_tot1, "#,####"))
                    .SetText(.GetColFromID("TAT 율(%)"), 1, Format((dbCnt_tot0 / dbCnt_tot1) * 100, "0.00"))

                    If dbCnt_tot0 > 0 Then
                        dbSum_tat = dbSum_tat / dbCnt_tot0
                        lgHH = Convert.ToInt64(dbSum_tat / 60)
                        lgMM = Convert.ToInt64(dbSum_tat Mod 60)

                        .SetText(.GetColFromID("TAT"), 1, Format(lgHH, "00") + ":" + Format(lgMM, "00"))
                    End If

                End If

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            DS_StatusBar.setTextStatusBar("")
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

            If mbEscape = True Then
                MsgBox("리스트 표시를 중단 했습니다.", MsgBoxStyle.Information, Me.Text)
            End If

            mbQuery = False
            mbEscape = False
        End Try

    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "
    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.
        sbDisplay_slip()
        sbInitialize()

        Me.cboTat.SelectedIndex = 1 : Me.cboTerm.SelectedIndex = 1
        sbSpread_Init()

    End Sub

    Private Sub sbSpread_Init()

        With spdList
            .MaxRows = 0
            .MaxCols = 8

            .SetText(1, 0, "검사코드")
            .SetText(2, 0, "검사명")
            .SetText(3, 0, "검체명")
            .SetText(4, 0, "TAT기준")
            .SetText(5, 0, "TAT")
            .SetText(6, 0, "유효건수")
            .SetText(7, 0, "전체건수")
            .SetText(8, 0, "TAT 율(%)")

            .set_ColWidth(1, 10)
            .set_ColWidth(2, 16)
            .set_ColWidth(3, 10)
            .set_ColWidth(4, 6)
            .set_ColWidth(5, 6)
            .set_ColWidth(6, 8)
            .set_ColWidth(7, 8)
            .set_ColWidth(8, 8)

            Dim iTatMM As Integer = Convert.ToInt32(Me.txtTat.Text) * CType(IIf(Me.cboTat.Text = "분", 60, IIf(Me.cboTat.Text = "시간", 60 * 60, 24 * 60 * 60)), Integer)
            Dim iTatTerm As Integer = Convert.ToInt32(Me.txtTerm.Text) * CType(IIf(Me.cboTerm.Text = "초", 1, IIf(Me.cboTerm.Text = "분", 60, 60 * 60)), Integer)

            Dim dteTime As Date = CDate("00:00:00 AM")

            For ix As Integer = 1 To iTatMM Step iTatTerm

                .MaxCols += 1

                dteTime = DateAdd(DateInterval.Second, iTatTerm, dteTime)
                Dim sTmp As String = ""

                If Me.cboTerm.Text = "초" Then
                    sTmp = Format(dteTime, "mm:ss")
                ElseIf Me.cboTerm.Text = "분" Then
                    sTmp = Format(dteTime, "HH:mm")
                ElseIf Me.cboTerm.Text = "시간" Then
                    If dteTime >= DateAdd(DateInterval.Day, 1, CDate("00:00:00 AM")) Then
                        sTmp = Format(DateAdd(DateInterval.Day, -1, dteTime), "dd HH:mm")
                    Else

                        sTmp = Format(dteTime, "HH:mm")
                    End If
                End If

                .SetText(.MaxCols, 0, sTmp)
                .set_ColWidth(.MaxCols, 8)
            Next

            .MaxCols += 1
            .SetText(.MaxCols, 0, "기타")
            .set_ColWidth(.MaxCols, 8)

            .ColsFrozen = 6

        End With

        Fn.SpdSetColName(spdList)

    End Sub

    Private Sub rdoDept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDeptA.CheckedChanged, rdoDeptS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            '전체
            If Me.rdoDeptA.Checked And CType(sender, RadioButton).Name.ToUpper = "RDODEPTA" Then
                Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False
            ElseIf Me.rdoDeptS.Checked And CType(sender, RadioButton).Name.ToUpper = "RDODEPTS" Then
                If Not Me.cboDept.Items.Count > 0 Then
                    sbGetDeptInfo()
                End If

                If Me.cboDept.Items.Count = 0 Then Return

                Me.cboDept.SelectedIndex = 0 : Me.cboDept.Enabled = True
            End If

        Catch ex As Exception
            CDHELP .FGCDHELPFN .fn_PopMsg (Me, "E"c, ex.Message )

        End Try
    End Sub

    Private Sub rdoIO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIOA.CheckedChanged, rdoIOO.CheckedChanged, rdoIOI.CheckedChanged, rdoIOC.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            Me.rdoWardA.Checked = True

            '전체 또는 외래
            If Me.rdoIOA.Checked Or Me.rdoIOO.Checked Or Me.rdoIOC.Checked Then
                Me.pnlWard.Enabled = False

            ElseIf Me.rdoIOI.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOIOI" Then
                Me.pnlWard.Enabled = True
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoWard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoWardA.CheckedChanged, rdoWardS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            '전체
            If Me.rdoWardA.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOWARDA" Then
                Me.cboWard.SelectedIndex = -1 : cboWard.Enabled = False

            ElseIf Me.rdoWardS.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOWARDS" Then
                If Not Me.cboWard.Items.Count > 0 Then
                    sbGetWardInfo()
                End If

                If Me.cboWard.Items.Count = 0 Then Return

                Me.cboWard.SelectedIndex = 0 : Me.cboWard.Enabled = True
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTestA.CheckedChanged, rdoTestS.CheckedChanged
        Dim sFn As String = "rdoTest_CheckedChanged"

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            '전체
            If Me.rdoTestA.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOTESTA" Then
                spdTest.MaxRows = 0

            ElseIf Me.rdoTestS.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOTESTS" Then
                sbDisplay_test()
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            sbSpread_Init()

            If Me.rdoCntGbn0.Checked = True Or rdoCntGbn1.Checked = True Then
                If mbQuery = False Then fntQuery0()
            Else
                If mbQuery = False Then fntQuery1()
            End If

            If Me.spdList.MaxRows = 0 Then CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "해당 데이타가 없습니다.")

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
        
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cboPart_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged

        If Me.rdoTestA.Checked = False Then rdoTestA.Checked = True
        Me.spdTest.MaxRows = 0

    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sBuf As String = ""

        With spdList
            .ReDraw = False

            If .MaxRows = 0 Then Exit Sub

            .MaxRows = .MaxRows + 1
            .InsertRows(1, 1)

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = 0 : sBuf = .Text
                .Col = i : .Row = 1 : .Text = sBuf
            Next

            If .ExportToExcel("tat_statistics.xls", "TAT", "") Then
                Process.Start("tat_statistics.xls")
            End If

            .DeleteRows(1, 1)
            .MaxRows -= 1

            .ReDraw = True
        End With

    End Sub

#End Region

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdList.MaxRows = 0
    End Sub

    Private Sub FGT03_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGT03_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)

        End Select
    End Sub

   
    Private Sub chkVerity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVerity.Click
        If chkVerity.Checked = True Then
            chkVerOnly.Checked = False
        End If
    End Sub

    Private Sub chkVerOnly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVerOnly.Click
        If chkVerOnly.Checked = True Then
            chkVerity.Checked = False
        End If
    End Sub
End Class