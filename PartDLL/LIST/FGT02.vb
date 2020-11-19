'>> 검사통계
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports LISAPP.APP_T

Public Class FGT02
    Inherits System.Windows.Forms.Form

    Private Const mi_Analysis_Or_Reanalysis As Integer = 1

    Private miSelectKey As Integer = 0
    Private miMaxDiffDay As Integer = 100
    Private miMaxDiffMonth As Integer = 24
    Private miMaxDiffYear As Integer = 2

    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents spdStatistics As AxFPSpreadADO.AxfpSpread
    Friend WithEvents split1 As System.Windows.Forms.Splitter
    Friend WithEvents chkTcls_b As System.Windows.Forms.CheckBox
    Friend WithEvents cboTGrp As System.Windows.Forms.ComboBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdoTGrpS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTGrpA As System.Windows.Forms.RadioButton
    Friend WithEvents lblTGrp As System.Windows.Forms.Label
    Friend WithEvents rdoIOC As System.Windows.Forms.RadioButton
    Friend WithEvents chkIoGbn_noC As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents CButton1 As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnAnalysis As CButtonLib.CButton

    Private m_fgt02_anal As New FGT02_ANALSVR

    Private Function fnDisplayStatistics() As Boolean

        Dim bReturn As Boolean = False

        Try
            Dim sStType As String = "", sDMYGbn As String = "", sDT1 As String = "", sDT2 As String = "", sTM1 As String = "", sTM2 As String = "", sADNGbn As String = ""
            Dim sIO As String = "", sDept As String = "", sDr As String = "", sWard As String = ""
            Dim sAbRst As String = ""
            Dim sBccls As String = "", sPart As String = "", sSlip As String = "", sTGrp As String = ""

            Dim sSame As String = "", sSpc As String = "", sMinusExLab As String = "", sTcdGbn As String = "", sTestCd As String = ""
            Dim a_sDMY As String() = Nothing
            Dim iDMYDiff As Integer = 0, iSum As Integer = 0, iCnt As Integer = 0

            '기준시간 구분
            If Me.rdoOptDT1.Checked Then
                sStType = "T"
            Else
                sStType = "F"
            End If

            If Me.dtpDT1.Value > Me.dtpDT2.Value Then
                MsgBox("날짜구간 설정이 잘못되었습니다. 시작을 끝보다 작거나 같게 설정하십시요!!")

                Return False
            End If

            '> 일별/월별/연별 구분
            If Me.rdoDay.Checked Then
                '일별
                Select Case Me.cboTimeCfg.SelectedIndex
                    Case 0  '일자
                        sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM-dd")
                        sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM-dd")
                        sTM1 = ""
                        sTM2 = ""
                    Case 1  '순차시간
                        sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM-dd")
                        sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM-dd")
                        sTM1 = ""
                        sTM2 = ""
                    Case 2  '시간대
                        sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM-dd")
                        sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM-dd")
                        sTM1 = Me.dtpTM1.Value.ToString("HH")
                        sTM2 = Me.dtpTM2.Value.ToString("HH")
                End Select

                sDMYGbn = "D"

                Select Case Me.cboTimeCfg.SelectedIndex
                    Case 0  '일자
                        iDMYDiff = CInt(DateDiff(DateInterval.Day, CDate(sDT1), CDate(sDT2)))

                        ReDim a_sDMY(iDMYDiff)

                        For i As Integer = 1 To iDMYDiff + 1
                            a_sDMY(i - 1) = DateAdd(DateInterval.Day, i - 1, CDate(sDT1)).ToShortDateString
                        Next

                    Case 1  '순차시간
                        Dim sDT1buf As String = sDT1 + " " + Me.dtpDT1.Value.ToString("HH:00")
                        Dim sDT2buf As String = sDT2 + " " + Me.dtpDT2.Value.ToString("HH:59")

                        iDMYDiff = CInt(DateDiff(DateInterval.Hour, CDate(sDT1buf), CDate(sDT2buf)))

                        ReDim a_sDMY(iDMYDiff)

                        For i As Integer = 1 To iDMYDiff + 1
                            a_sDMY(i - 1) = DateAdd(DateInterval.Hour, i - 1, CDate(sDT1buf + ":00")).ToString("yyyy-MM-dd HH")
                        Next

                    Case 2  '시간대
                        iDMYDiff = CInt(DateDiff(DateInterval.Day, CDate(sDT1), CDate(sDT2)))
                        Dim iTimeZone As Integer = CInt(DateDiff(DateInterval.Hour, CDate(sTM1 + ":00"), CDate(sTM2 + ":59")))

                        ReDim a_sDMY((iDMYDiff + 1) * (iTimeZone + 1) - 1)

                        For i As Integer = 1 To iDMYDiff + 1
                            For j As Integer = 1 To iTimeZone + 1
                                Dim sDtTmBuf As String = ""
                                sDtTmBuf += DateAdd(DateInterval.Day, i - 1, CDate(sDT1)).ToString("yyyy-MM-dd") + " "
                                sDtTmBuf += DateAdd(DateInterval.Hour, j - 1, CDate(sTM1 + ":00")).ToString("HH")

                                a_sDMY((iTimeZone + 1) * (i - 1) + j - 1) = sDtTmBuf
                            Next
                        Next

                End Select

                If a_sDMY.Length > miMaxDiffDay - 1 Then
                    MsgBox("일별로는 " + miMaxDiffDay.ToString + "개의 구간 까지만 검사통계를 조회할 수 있습니다. 날짜구간 또는 시간대를 다시 설정하십시요!!")

                    Return False
                End If

            ElseIf Me.rdoMonth.Checked Then
                '월별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy-MM")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy-MM")
                sTM1 = ""
                sTM2 = ""

                sDMYGbn = "M"

                iDMYDiff = CInt(DateDiff(DateInterval.Month, CDate(sDT1), CDate(sDT2)))

                If iDMYDiff > miMaxDiffMonth - 1 Then
                    MsgBox("월별로는 " + miMaxDiffMonth.ToString + "개월 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Month, i - 1, CDate(sDT1)).ToString("yyyy-MM")
                Next

            ElseIf Me.rdoYear.Checked Then
                '연별
                sDT1 = Me.dtpDT1.Value.ToString("yyyy")
                sDT2 = Me.dtpDT2.Value.ToString("yyyy")
                sTM1 = ""
                sTM2 = ""

                sDMYGbn = "Y"

                iDMYDiff = CInt(DateDiff(DateInterval.Year, CDate(sDT1 + "-01"), CDate(sDT2 + "-12")))

                If iDMYDiff > miMaxDiffMonth - 1 Then
                    MsgBox("연별로는 " + miMaxDiffYear.ToString + "년 까지의 검사통계를 조회할 수 있습니다. 날짜구간을 다시 설정하십시요!!")

                    Return False
                End If

                ReDim a_sDMY(iDMYDiff)

                For i As Integer = 1 To iDMYDiff + 1
                    a_sDMY(i - 1) = DateAdd(DateInterval.Year, i - 1, CDate(sDT1 + "-01")).ToString("yyyy")
                Next

            End If

            '주야설정
            If Me.cboDayNight.SelectedIndex = 1 Then
                '주간
                sADNGbn = "D"
            ElseIf Me.cboDayNight.SelectedIndex = 2 Then
                '야간
                sADNGbn = "N"
            Else
                '전체
                sADNGbn = "A"
            End If

            '외래/입원
            If Me.rdoIOO.Checked Then
                sIO = "O"
            ElseIf Me.rdoIOI.Checked Then
                sIO = "I"
            ElseIf Me.rdoIOC.Checked Then
                sIO = "C"
            End If

            '진료과
            If Me.rdoDeptS.Enabled And Me.rdoDeptS.Checked Then sDept = Me.cboDept.Text.Split("|"c)(1)

            '병동
            If Me.rdoWardS.Enabled And Me.rdoWardS.Checked Then sWard = Me.cboWard.Text.Split("|"c)(1)

            '이상자구분
            If Me.rdoAbNone.Checked Then
            ElseIf Me.rdoAbWithLH.Checked Then
                sAbRst = "1"
            ElseIf Me.rdoAbWithoutLH.Checked Then
                sAbRst = "2"
            End If

            '분야/슬립
            If Me.rdoSlipP.Checked Then
                sPart = Ctrl.Get_Code(Me.cboSlip)
            ElseIf Me.rdoSlipS.Checked Then
                sSlip = Ctrl.Get_Code(Me.cboSlip)
            End If

            '계/검사계
            If Me.rdoBcclsS.Checked Then
                sBccls = Ctrl.Get_Code(Me.cboBccls)
                'ElseIf Me.rdoSectT.Checked Then
                '   sTSect = Ctrl.Get_Code(Me.cboSect)
            End If

            ''작업그룹
            'If Me.rdoWkGrpS.Checked Then
            '    sWkGrp = Ctrl.Get_Code(Me.cboWkGrp)
            'End If

            '검사그룹
            If Me.rdoTGrpS.Checked Then
                sTGrp = Ctrl.Get_Code(Me.cboTGrp)
            End If

            '대표검사 적용
            If Me.chkSameCd.Checked Then sSame = "Y"

            '검체코드 적용
            If Me.chkSpcCd.Checked Then sSpc = "Y"

            '위탁검사 제외
            If Me.chkExLab.Checked Then sMinusExLab = "Y"

            'Single, Parent 검사만 적용
            If Me.chkTcls_s_p.Checked And Me.chkTcls_b.Checked Then
                sTcdGbn = "'B', 'S', 'P'"
            ElseIf Me.chkTcls_s_p.Checked Then
                sTcdGbn = "'S', 'P'"
            ElseIf Me.chkTcls_b.Checked Then
                sTcdGbn = "B"
            End If

            '검사
            If Me.rdoTestS.Checked Then
                With Me.spdTest
                    For i As Integer = 1 To .MaxRows
                        .Col = .GetColFromID("CHK")
                        .Row = i

                        If .Text = "1" Then
                            .Col = .GetColFromID("TESTCD")
                            .Row = i

                            sTestCd += "'" + .Text + "',"
                        End If
                    Next
                End With

                If sTestCd = "" Then
                    MsgBox("통계를 위한 검사를 선택해 주십시요!!")

                    Return False
                Else
                    sTestCd = sTestCd.Substring(0, sTestCd.Length - 1)
                End If
            End If

            sbInitialize_spdStatistics(a_sDMY, sSpc)

            Dim dt As DataTable
            Dim iCol As Integer = 0

            '2018-07-11 yjh 통계조회 시 현재 검사명이 종료된 검사코드의 이름으로 나오는 건 수정
            dt = (New SrhFn).fnGet_Test_Statistics(sStType, sDMYGbn, a_sDMY, sDT1, sDT2, sADNGbn, sTM1, sTM2, sIO, sDept, sWard, sAbRst, _
                                                   sPart, sSlip, sBccls, "", sTestCd, sSame, sSpc, sMinusExLab, sTcdGbn, sTGrp, chkIoGbn_noC.Checked)

            If dt.Rows.Count > 0 Then
                With Me.spdStatistics
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(j).ColumnName.ToUpper)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString
                            End If
                        Next
                    Next

                    .MaxRows = .MaxRows + 1

                    '.Col = 1 : .Col2 = 3 : .Row = .MaxRows : .Row2 = .MaxRows
                    '.BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .BlockMode = False
                    '.Col = 4 : .Col2 = .MaxCols : .Row = .MaxRows : .Row2 = .MaxRows
                    '.BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeNumber : .TypeNumberDecPlaces = 0 : .Lock = True : .BlockMode = False

                    .Col = 3 : .Row = .MaxRows : .Text = "합 계"

                    For i As Integer = 0 To a_sDMY.Length
                        iSum = 0

                        For j As Integer = 1 To .MaxRows - 1
                            .Col = 5 + i : .Row = j : iCnt = CType(Val(.Text), Integer)
                            iSum += iCnt
                        Next

                        .Col = 5 + i : .Row = .MaxRows : .Text = CType(iSum, String)
                    Next

                    .ReDraw = True
                End With
            Else
                spdStatistics.MaxRows = 0
            End If

            Return True
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
            Return False
        End Try
    End Function

    Private Sub sbDisplay_dept()

        Try
            Dim dt As DataTable= OCSAPP.OcsLink.SData.fnGet_DeptList

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboDept.Items.Add(dt.Rows(i).Item("deptnm").ToString + Space(200) + "|" + dt.Rows(i).Item("deptcd").ToString)
                Next
            End If

            If Me.cboDept.Items.Count > 0 Then Me.cboDept.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_part()
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List()

            Me.cboSlip.Items.Clear()

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboSlip.Items.Add("[" + dt.Rows(i).Item("partcd").ToString + "]" + " " + dt.Rows(i).Item("partnmd").ToString)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_bccls()
        Dim sFn As String = "sbDisplay_bccls"

        Try
            Dim dt As DataTable= LISAPP.COMM.cdfn.fnGet_Bccls_List()

            Me.cboBccls.Items.Clear()

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboBccls.Items.Add("[" + dt.Rows(i).Item("bcclscd").ToString + "]" + " " + dt.Rows(i).Item("bcclsnmd").ToString)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_slip()

        Try
            Dim dt As DataTable= LISAPP.COMM.cdfn.fnGet_Slip_List

            Me.cboSlip.Items.Clear()

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboSlip.Items.Add("[" + dt.Rows(i).Item("slipcd").ToString + "]" + " " + dt.Rows(i).Item("slipnmd").ToString)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_test()

        Try

            Dim sBcclsCd As String = "", sSlipCd As String = "", sTGrp As String = ""
            Dim iCol As Integer = 0

            If Me.rdoBcclsS.Checked Then
                With Me.cboBccls
                    sBcclsCd = Ctrl.Get_Code(.SelectedItem.ToString)
                End With
            End If

            If Me.rdoSlipS.Checked Or Me.rdoSlipP.Checked Then
                If Ctrl.Get_Code(Me.cboSlip) <> "" Then
                    sSlipCd = Ctrl.Get_Code(Me.cboSlip)
                End If
            End If

            If Me.rdoTGrpS.Checked Then sTGrp = Ctrl.Get_Code(Me.cboTGrp)
            If sTGrp <> "" Then sSlipCd = "" : sBcclsCd = ""

            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_test_list(sSlipCd, sTGrp, "", "", "", sBcclsCd)

            spdTest.MaxRows = 0

            If dt Is Nothing Then Return

            Dim dr As DataRow()

            dr = dt.Select("tcdgbn IN ('S', 'P', 'B') AND ordhide = '0'", "")
            dt = Fn.ChangeToDataTable(dr)

            If dt.Rows.Count > 0 Then
                With spdTest
                    .ReDraw = False

                    .MaxRows = dt.Rows.Count

                    For i As Integer = 0 To dt.Rows.Count - 1
                        For j As Integer = 0 To dt.Columns.Count - 1
                            iCol = 0
                            iCol = .GetColFromID(dt.Columns(j).ColumnName.ToUpper)

                            If iCol > 0 Then
                                .Col = iCol
                                .Row = i + 1
                                .Text = dt.Rows(i).Item(j).ToString
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

    Private Sub sbDisplay_ward()

        Try
            Dim dt As DataTable= OCSAPP.OcsLink.SData.fnGet_WardList
            Me.cboWard.Items.Clear()

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboWard.Items.Add(dt.Rows(i).Item("wardnm").ToString + Space(200) + "|" + dt.Rows(i).Item("wardno").ToString)
                Next
            End If

            If Me.cboWard.Items.Count > 0 Then Me.cboWard.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_tgrp()

        Try
            Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_TGrp_List()

            Me.cboTGrp.Items.Clear()

            If dt Is Nothing Then Return

            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Me.cboTGrp.Items.Add("[" + dt.Rows(i).Item("tgrpcd").ToString + "]" + " " + dt.Rows(i).Item("tgrpnmd").ToString)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbInitialize()

        Try
            miSelectKey = 1
            Dim sCurSysDate As String = ""

            Me.rdoDay.Checked = True
            Me.cboTimeCfg.SelectedIndex = 0
            '------------------------------
            Me.rdoOptDT1.Checked = True
            '------------------------------
            sCurSysDate = (New LISAPP.APP_DB.ServerDateTime).GetDate("-")
            Me.dtpDT1.CustomFormat = "yyyy-MM-dd" : Me.dtpDT1.Value = CType(sCurSysDate + " 00:00:00", Date)
            Me.dtpDT2.CustomFormat = "yyyy-MM-dd" : Me.dtpDT2.Value = CType(sCurSysDate + " 23:59:59", Date)
            Me.dtpTM1.Value = Me.dtpDT1.Value : Me.dtpTM1.Enabled = False
            Me.dtpTM2.Value = Me.dtpDT2.Value : Me.dtpTM2.Enabled = False
            '------------------------------
            Me.rdoIOA.Checked = True

            Me.rdoDeptA.Checked = True
            Me.pnlDept.Enabled = False
            Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            Me.rdoWardA.Checked = True
            Me.pnlWard.Enabled = False
            Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False
            '------------------------------
            Me.rdoAbNone.Checked = True
            '------------------------------
            Me.rdoSlipA.Checked = True
            Me.cboSlip.SelectedIndex = -1 : Me.cboSlip.Enabled = False
            '------------------------------
            Me.rdoBcclsA.Checked = True
            Me.cboBccls.SelectedIndex = -1 : Me.cboBccls.Enabled = False
            '------------------------------
            'Me.rdoWkGrpA.Checked = True
            'Me.cboWkGrp.SelectedIndex = -1 : Me.cboWkGrp.Enabled = False
            '------------------------------
            Me.rdoTGrpA.Checked = True
            Me.cboTGrp.SelectedIndex = -1 : Me.cboTGrp.Enabled = False
            '------------------------------
            Me.chkSameCd.Checked = False
            Me.chkSpcCd.Checked = False
            Me.chkExLab.Checked = False
            Me.chkTcls_s_p.Checked = False
            '------------------------------
            Me.rdoTestA.Checked = True
            Me.spdTest.MaxRows = 0

            Dim bAuthority As Boolean = USER_SKILL.Authority("T01", mi_Analysis_Or_Reanalysis)

            Me.btnAnalysis.Enabled = bAuthority

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub sbInitialize_spdStatistics(ByVal ra_sDMY As String(), ByVal rsSpc As String)

        Try
            With Me.spdStatistics
                .ReDraw = False

                '검사코드, 검체코드, 검사명, 검체명, Total
                .MaxCols = ra_sDMY.Length + 5

                .Col = 5 : .Col2 = .MaxCols : .Row = -1 : .Row2 = -1
                .BlockMode = True : .CellType = FPSpreadADO.CellTypeConstants.CellTypeNumber : .TypeNumberDecPlaces = 0 : .Lock = True : .BlockMode = False

                If rsSpc = "Y" Then
                    .Col = .GetColFromID("SPCCD")
                    .ColHidden = False

                    .Col = .GetColFromID("SPCNM")
                    .ColHidden = False

                    .set_ColWidth(.GetColFromID("SPCCD"), 7)
                    .set_ColWidth(.GetColFromID("SPCNM"), 12)
                Else
                    .Col = .GetColFromID("SPCCD")
                    .ColHidden = True

                    .Col = .GetColFromID("SPCNM")
                    .ColHidden = True
                End If

                For i As Integer = 0 To ra_sDMY.Length - 1
                    .Col = 5 + i : .Row = 0 : .Text = ra_sDMY(i) : .ColID = "C" + (i + 1).ToString : .set_ColWidth(.GetColFromID("C" + (i + 1).ToString), 9)
                Next

                .Col = 5 + ra_sDMY.Length : .Row = 0 : .Text = "Total" : .ColID = "CTOTAL" : .set_ColWidth(.GetColFromID("CTOTAL"), 9)

                If ra_sDMY(0).Replace("-", "").Replace(" ", "").Length > 8 Then
                    .set_RowHeight(0, 17.12)
                Else
                    .set_RowHeight(0, 12.34)
                End If

                .ReDraw = True
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            miSelectKey = 0

        End Try
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        sbInitialize()
    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents tclStatistics As System.Windows.Forms.TabControl
    Friend WithEvents tpgVar As System.Windows.Forms.TabPage
    Friend WithEvents dtpTM1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblInsuGbn As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpDT2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDT1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDT As System.Windows.Forms.Label
    Friend WithEvents pnlOptDT As System.Windows.Forms.Panel
    Friend WithEvents PnlDMYsel As System.Windows.Forms.Panel
    Friend WithEvents rdoMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDay As System.Windows.Forms.RadioButton
    Friend WithEvents lblDMYsel As System.Windows.Forms.Label
    Friend WithEvents cboTimeCfg As System.Windows.Forms.ComboBox
    Friend WithEvents lblopDT As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTM As System.Windows.Forms.Label
    Friend WithEvents lblIO As System.Windows.Forms.Label
    Friend WithEvents pnlIO As System.Windows.Forms.Panel
    Friend WithEvents pnlDept As System.Windows.Forms.Panel
    Friend WithEvents lblDept As System.Windows.Forms.Label
    Friend WithEvents lblWard As System.Windows.Forms.Label
    Friend WithEvents pnlSlip As System.Windows.Forms.Panel
    Friend WithEvents lblslip As System.Windows.Forms.Label
    Friend WithEvents grp02 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlSect As System.Windows.Forms.Panel
    Friend WithEvents lblsect As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents rdoSlipP As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSlipA As System.Windows.Forms.RadioButton
    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents rdoWardS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoWardA As System.Windows.Forms.RadioButton
    Friend WithEvents cboDept As System.Windows.Forms.ComboBox
    Friend WithEvents rdoDeptS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDeptA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOO As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOI As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIOA As System.Windows.Forms.RadioButton
    Friend WithEvents dtpTM2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdoOptDT2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOptDT1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSlipS As System.Windows.Forms.RadioButton
    Friend WithEvents cboBccls As System.Windows.Forms.ComboBox
    Friend WithEvents rdoBcclsS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBcclsA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTestS As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTestA As System.Windows.Forms.RadioButton
    Friend WithEvents spdTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlWard As System.Windows.Forms.Panel
    Friend WithEvents chkExLab As System.Windows.Forms.CheckBox
    Friend WithEvents chkSameCd As System.Windows.Forms.CheckBox
    Friend WithEvents chkSpcCd As System.Windows.Forms.CheckBox
    Friend WithEvents cboDayNight As System.Windows.Forms.ComboBox
    Friend WithEvents chkTcls_s_p As System.Windows.Forms.CheckBox
    Friend WithEvents pnlAb As System.Windows.Forms.Panel
    Friend WithEvents rdoAbWithLH As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAbWithoutLH As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAbNone As System.Windows.Forms.RadioButton
    Friend WithEvents lblAb As System.Windows.Forms.Label
    Friend WithEvents lblDayNight As System.Windows.Forms.Label
    Friend WithEvents rdoYear As System.Windows.Forms.RadioButton

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGT02))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.tclStatistics = New System.Windows.Forms.TabControl()
        Me.tpgVar = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.spdStatistics = New AxFPSpreadADO.AxfpSpread()
        Me.split1 = New System.Windows.Forms.Splitter()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkIoGbn_noC = New System.Windows.Forms.CheckBox()
        Me.cboTGrp = New System.Windows.Forms.ComboBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdoTGrpS = New System.Windows.Forms.RadioButton()
        Me.rdoTGrpA = New System.Windows.Forms.RadioButton()
        Me.lblTGrp = New System.Windows.Forms.Label()
        Me.chkTcls_b = New System.Windows.Forms.CheckBox()
        Me.chkTcls_s_p = New System.Windows.Forms.CheckBox()
        Me.lblDMYsel = New System.Windows.Forms.Label()
        Me.chkExLab = New System.Windows.Forms.CheckBox()
        Me.lblDayNight = New System.Windows.Forms.Label()
        Me.cboTimeCfg = New System.Windows.Forms.ComboBox()
        Me.chkSameCd = New System.Windows.Forms.CheckBox()
        Me.chkSpcCd = New System.Windows.Forms.CheckBox()
        Me.PnlDMYsel = New System.Windows.Forms.Panel()
        Me.rdoYear = New System.Windows.Forms.RadioButton()
        Me.rdoMonth = New System.Windows.Forms.RadioButton()
        Me.rdoDay = New System.Windows.Forms.RadioButton()
        Me.pnlAb = New System.Windows.Forms.Panel()
        Me.rdoAbWithLH = New System.Windows.Forms.RadioButton()
        Me.rdoAbWithoutLH = New System.Windows.Forms.RadioButton()
        Me.rdoAbNone = New System.Windows.Forms.RadioButton()
        Me.lblInsuGbn = New System.Windows.Forms.Label()
        Me.lblopDT = New System.Windows.Forms.Label()
        Me.lblAb = New System.Windows.Forms.Label()
        Me.pnlOptDT = New System.Windows.Forms.Panel()
        Me.rdoOptDT2 = New System.Windows.Forms.RadioButton()
        Me.rdoOptDT1 = New System.Windows.Forms.RadioButton()
        Me.cboBccls = New System.Windows.Forms.ComboBox()
        Me.lblDT = New System.Windows.Forms.Label()
        Me.pnlSect = New System.Windows.Forms.Panel()
        Me.rdoBcclsS = New System.Windows.Forms.RadioButton()
        Me.rdoBcclsA = New System.Windows.Forms.RadioButton()
        Me.dtpDT1 = New System.Windows.Forms.DateTimePicker()
        Me.lblsect = New System.Windows.Forms.Label()
        Me.cboDayNight = New System.Windows.Forms.ComboBox()
        Me.dtpDT2 = New System.Windows.Forms.DateTimePicker()
        Me.cboSlip = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlSlip = New System.Windows.Forms.Panel()
        Me.rdoSlipS = New System.Windows.Forms.RadioButton()
        Me.rdoSlipP = New System.Windows.Forms.RadioButton()
        Me.rdoSlipA = New System.Windows.Forms.RadioButton()
        Me.lblslip = New System.Windows.Forms.Label()
        Me.dtpTM1 = New System.Windows.Forms.DateTimePicker()
        Me.dtpTM2 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTM = New System.Windows.Forms.Label()
        Me.lblIO = New System.Windows.Forms.Label()
        Me.pnlIO = New System.Windows.Forms.Panel()
        Me.rdoIOC = New System.Windows.Forms.RadioButton()
        Me.rdoIOO = New System.Windows.Forms.RadioButton()
        Me.rdoIOI = New System.Windows.Forms.RadioButton()
        Me.rdoIOA = New System.Windows.Forms.RadioButton()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.pnlDept = New System.Windows.Forms.Panel()
        Me.rdoDeptS = New System.Windows.Forms.RadioButton()
        Me.rdoDeptA = New System.Windows.Forms.RadioButton()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.lblWard = New System.Windows.Forms.Label()
        Me.cboWard = New System.Windows.Forms.ComboBox()
        Me.pnlWard = New System.Windows.Forms.Panel()
        Me.rdoWardS = New System.Windows.Forms.RadioButton()
        Me.rdoWardA = New System.Windows.Forms.RadioButton()
        Me.grp02 = New System.Windows.Forms.GroupBox()
        Me.spdTest = New AxFPSpreadADO.AxfpSpread()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.rdoTestS = New System.Windows.Forms.RadioButton()
        Me.rdoTestA = New System.Windows.Forms.RadioButton()
        Me.lblTest = New System.Windows.Forms.Label()
        Me.btnSearch = New CButtonLib.CButton()
        Me.CButton1 = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnAnalysis = New CButtonLib.CButton()
        Me.tclStatistics.SuspendLayout()
        Me.tpgVar.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.PnlDMYsel.SuspendLayout()
        Me.pnlAb.SuspendLayout()
        Me.pnlOptDT.SuspendLayout()
        Me.pnlSect.SuspendLayout()
        Me.pnlSlip.SuspendLayout()
        Me.pnlIO.SuspendLayout()
        Me.pnlDept.SuspendLayout()
        Me.pnlWard.SuspendLayout()
        Me.grp02.SuspendLayout()
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel9.SuspendLayout()
        Me.SuspendLayout()
        '
        'tclStatistics
        '
        Me.tclStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tclStatistics.Controls.Add(Me.tpgVar)
        Me.tclStatistics.Location = New System.Drawing.Point(0, 0)
        Me.tclStatistics.Name = "tclStatistics"
        Me.tclStatistics.SelectedIndex = 0
        Me.tclStatistics.Size = New System.Drawing.Size(1016, 662)
        Me.tclStatistics.TabIndex = 0
        '
        'tpgVar
        '
        Me.tpgVar.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgVar.Controls.Add(Me.Panel4)
        Me.tpgVar.Controls.Add(Me.split1)
        Me.tpgVar.Controls.Add(Me.Panel3)
        Me.tpgVar.Location = New System.Drawing.Point(4, 22)
        Me.tpgVar.Name = "tpgVar"
        Me.tpgVar.Size = New System.Drawing.Size(1008, 636)
        Me.tpgVar.TabIndex = 0
        Me.tpgVar.Text = "조회조건설정"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(463, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(545, 636)
        Me.Panel4.TabIndex = 128
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.spdStatistics)
        Me.GroupBox1.Location = New System.Drawing.Point(0, -10)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(545, 643)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'spdStatistics
        '
        Me.spdStatistics.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdStatistics.DataSource = Nothing
        Me.spdStatistics.Location = New System.Drawing.Point(3, 10)
        Me.spdStatistics.Name = "spdStatistics"
        Me.spdStatistics.OcxState = CType(resources.GetObject("spdStatistics.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStatistics.Size = New System.Drawing.Size(545, 632)
        Me.spdStatistics.TabIndex = 0
        '
        'split1
        '
        Me.split1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.split1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.split1.Location = New System.Drawing.Point(458, 0)
        Me.split1.MinSize = 224
        Me.split1.Name = "split1"
        Me.split1.Size = New System.Drawing.Size(5, 636)
        Me.split1.TabIndex = 127
        Me.split1.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.Controls.Add(Me.chkIoGbn_noC)
        Me.Panel3.Controls.Add(Me.cboTGrp)
        Me.Panel3.Controls.Add(Me.Panel5)
        Me.Panel3.Controls.Add(Me.lblTGrp)
        Me.Panel3.Controls.Add(Me.chkTcls_b)
        Me.Panel3.Controls.Add(Me.chkTcls_s_p)
        Me.Panel3.Controls.Add(Me.lblDMYsel)
        Me.Panel3.Controls.Add(Me.chkExLab)
        Me.Panel3.Controls.Add(Me.lblDayNight)
        Me.Panel3.Controls.Add(Me.cboTimeCfg)
        Me.Panel3.Controls.Add(Me.chkSameCd)
        Me.Panel3.Controls.Add(Me.chkSpcCd)
        Me.Panel3.Controls.Add(Me.PnlDMYsel)
        Me.Panel3.Controls.Add(Me.pnlAb)
        Me.Panel3.Controls.Add(Me.lblInsuGbn)
        Me.Panel3.Controls.Add(Me.lblopDT)
        Me.Panel3.Controls.Add(Me.lblAb)
        Me.Panel3.Controls.Add(Me.pnlOptDT)
        Me.Panel3.Controls.Add(Me.cboBccls)
        Me.Panel3.Controls.Add(Me.lblDT)
        Me.Panel3.Controls.Add(Me.pnlSect)
        Me.Panel3.Controls.Add(Me.dtpDT1)
        Me.Panel3.Controls.Add(Me.lblsect)
        Me.Panel3.Controls.Add(Me.cboDayNight)
        Me.Panel3.Controls.Add(Me.dtpDT2)
        Me.Panel3.Controls.Add(Me.cboSlip)
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Controls.Add(Me.pnlSlip)
        Me.Panel3.Controls.Add(Me.lblslip)
        Me.Panel3.Controls.Add(Me.dtpTM1)
        Me.Panel3.Controls.Add(Me.dtpTM2)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.lblTM)
        Me.Panel3.Controls.Add(Me.lblIO)
        Me.Panel3.Controls.Add(Me.pnlIO)
        Me.Panel3.Controls.Add(Me.lblDept)
        Me.Panel3.Controls.Add(Me.pnlDept)
        Me.Panel3.Controls.Add(Me.cboDept)
        Me.Panel3.Controls.Add(Me.lblWard)
        Me.Panel3.Controls.Add(Me.cboWard)
        Me.Panel3.Controls.Add(Me.pnlWard)
        Me.Panel3.Controls.Add(Me.grp02)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(458, 636)
        Me.Panel3.TabIndex = 24
        '
        'chkIoGbn_noC
        '
        Me.chkIoGbn_noC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkIoGbn_noC.AutoSize = True
        Me.chkIoGbn_noC.Location = New System.Drawing.Point(8, 617)
        Me.chkIoGbn_noC.Name = "chkIoGbn_noC"
        Me.chkIoGbn_noC.Size = New System.Drawing.Size(72, 16)
        Me.chkIoGbn_noC.TabIndex = 153
        Me.chkIoGbn_noC.Text = "수탁제외"
        Me.chkIoGbn_noC.UseVisualStyleBackColor = True
        '
        'cboTGrp
        '
        Me.cboTGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTGrp.Location = New System.Drawing.Point(212, 239)
        Me.cboTGrp.Margin = New System.Windows.Forms.Padding(0)
        Me.cboTGrp.Name = "cboTGrp"
        Me.cboTGrp.Size = New System.Drawing.Size(237, 20)
        Me.cboTGrp.TabIndex = 127
        Me.cboTGrp.Tag = "TCDGBN_01"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Linen
        Me.Panel5.Controls.Add(Me.rdoTGrpS)
        Me.Panel5.Controls.Add(Me.rdoTGrpA)
        Me.Panel5.Location = New System.Drawing.Point(99, 238)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(112, 20)
        Me.Panel5.TabIndex = 126
        '
        'rdoTGrpS
        '
        Me.rdoTGrpS.BackColor = System.Drawing.Color.Linen
        Me.rdoTGrpS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTGrpS.ForeColor = System.Drawing.Color.Black
        Me.rdoTGrpS.Location = New System.Drawing.Point(60, 1)
        Me.rdoTGrpS.Name = "rdoTGrpS"
        Me.rdoTGrpS.Size = New System.Drawing.Size(48, 18)
        Me.rdoTGrpS.TabIndex = 13
        Me.rdoTGrpS.Text = "선택"
        Me.rdoTGrpS.UseVisualStyleBackColor = False
        '
        'rdoTGrpA
        '
        Me.rdoTGrpA.BackColor = System.Drawing.Color.Linen
        Me.rdoTGrpA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTGrpA.ForeColor = System.Drawing.Color.Black
        Me.rdoTGrpA.Location = New System.Drawing.Point(4, 1)
        Me.rdoTGrpA.Name = "rdoTGrpA"
        Me.rdoTGrpA.Size = New System.Drawing.Size(48, 18)
        Me.rdoTGrpA.TabIndex = 11
        Me.rdoTGrpA.Text = "전체"
        Me.rdoTGrpA.UseVisualStyleBackColor = False
        '
        'lblTGrp
        '
        Me.lblTGrp.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTGrp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblTGrp.Location = New System.Drawing.Point(3, 238)
        Me.lblTGrp.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTGrp.Name = "lblTGrp"
        Me.lblTGrp.Size = New System.Drawing.Size(95, 20)
        Me.lblTGrp.TabIndex = 125
        Me.lblTGrp.Text = "검사그룹"
        Me.lblTGrp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkTcls_b
        '
        Me.chkTcls_b.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkTcls_b.Location = New System.Drawing.Point(8, 576)
        Me.chkTcls_b.Name = "chkTcls_b"
        Me.chkTcls_b.Size = New System.Drawing.Size(328, 20)
        Me.chkTcls_b.TabIndex = 124
        Me.chkTcls_b.Text = "Battery 검사만 적용"
        '
        'chkTcls_s_p
        '
        Me.chkTcls_s_p.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkTcls_s_p.Location = New System.Drawing.Point(8, 595)
        Me.chkTcls_s_p.Name = "chkTcls_s_p"
        Me.chkTcls_s_p.Size = New System.Drawing.Size(328, 20)
        Me.chkTcls_s_p.TabIndex = 123
        Me.chkTcls_s_p.Text = "Single, Parent 검사만 적용"
        '
        'lblDMYsel
        '
        Me.lblDMYsel.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDMYsel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDMYsel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblDMYsel.Location = New System.Drawing.Point(3, 4)
        Me.lblDMYsel.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDMYsel.Name = "lblDMYsel"
        Me.lblDMYsel.Size = New System.Drawing.Size(95, 20)
        Me.lblDMYsel.TabIndex = 24
        Me.lblDMYsel.Text = "일별/월별구분"
        Me.lblDMYsel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkExLab
        '
        Me.chkExLab.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkExLab.Location = New System.Drawing.Point(8, 556)
        Me.chkExLab.Name = "chkExLab"
        Me.chkExLab.Size = New System.Drawing.Size(328, 20)
        Me.chkExLab.TabIndex = 122
        Me.chkExLab.Text = "위탁검사 제외(위탁검사는 통계에서 제외)"
        '
        'lblDayNight
        '
        Me.lblDayNight.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDayNight.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDayNight.ForeColor = System.Drawing.Color.White
        Me.lblDayNight.Location = New System.Drawing.Point(330, 67)
        Me.lblDayNight.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDayNight.Name = "lblDayNight"
        Me.lblDayNight.Size = New System.Drawing.Size(76, 20)
        Me.lblDayNight.TabIndex = 72
        Me.lblDayNight.Text = "주/야 설정"
        Me.lblDayNight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTimeCfg
        '
        Me.cboTimeCfg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeCfg.Items.AddRange(New Object() {"[0] 일자로만 조회", "[1] 순차시간 형식으로 조회", "[2] 시간대 형식으로 조회"})
        Me.cboTimeCfg.Location = New System.Drawing.Point(208, 25)
        Me.cboTimeCfg.Margin = New System.Windows.Forms.Padding(0)
        Me.cboTimeCfg.Name = "cboTimeCfg"
        Me.cboTimeCfg.Size = New System.Drawing.Size(242, 20)
        Me.cboTimeCfg.TabIndex = 23
        Me.cboTimeCfg.Tag = "TCDGBN_01"
        '
        'chkSameCd
        '
        Me.chkSameCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSameCd.Location = New System.Drawing.Point(8, 515)
        Me.chkSameCd.Name = "chkSameCd"
        Me.chkSameCd.Size = New System.Drawing.Size(419, 20)
        Me.chkSameCd.TabIndex = 119
        Me.chkSameCd.Text = "대표검사 적용(대표검사가 설정된 항목을 대표검사의 통계에 포함)"
        '
        'chkSpcCd
        '
        Me.chkSpcCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkSpcCd.Location = New System.Drawing.Point(8, 536)
        Me.chkSpcCd.Name = "chkSpcCd"
        Me.chkSpcCd.Size = New System.Drawing.Size(397, 20)
        Me.chkSpcCd.TabIndex = 120
        Me.chkSpcCd.Text = "검체코드 적용(검체코드 레벨까지 분리하여 통계 처리)"
        '
        'PnlDMYsel
        '
        Me.PnlDMYsel.BackColor = System.Drawing.Color.Beige
        Me.PnlDMYsel.Controls.Add(Me.rdoYear)
        Me.PnlDMYsel.Controls.Add(Me.rdoMonth)
        Me.PnlDMYsel.Controls.Add(Me.rdoDay)
        Me.PnlDMYsel.Location = New System.Drawing.Point(99, 4)
        Me.PnlDMYsel.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlDMYsel.Name = "PnlDMYsel"
        Me.PnlDMYsel.Size = New System.Drawing.Size(164, 20)
        Me.PnlDMYsel.TabIndex = 25
        '
        'rdoYear
        '
        Me.rdoYear.BackColor = System.Drawing.Color.Beige
        Me.rdoYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoYear.Location = New System.Drawing.Point(112, 1)
        Me.rdoYear.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoYear.Name = "rdoYear"
        Me.rdoYear.Size = New System.Drawing.Size(48, 18)
        Me.rdoYear.TabIndex = 13
        Me.rdoYear.Text = "연별"
        Me.rdoYear.UseVisualStyleBackColor = False
        '
        'rdoMonth
        '
        Me.rdoMonth.BackColor = System.Drawing.Color.Beige
        Me.rdoMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoMonth.Location = New System.Drawing.Point(60, 1)
        Me.rdoMonth.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoMonth.Name = "rdoMonth"
        Me.rdoMonth.Size = New System.Drawing.Size(48, 18)
        Me.rdoMonth.TabIndex = 12
        Me.rdoMonth.Text = "월별"
        Me.rdoMonth.UseVisualStyleBackColor = False
        '
        'rdoDay
        '
        Me.rdoDay.BackColor = System.Drawing.Color.Beige
        Me.rdoDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDay.Location = New System.Drawing.Point(4, 1)
        Me.rdoDay.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoDay.Name = "rdoDay"
        Me.rdoDay.Size = New System.Drawing.Size(48, 18)
        Me.rdoDay.TabIndex = 11
        Me.rdoDay.Text = "일별"
        Me.rdoDay.UseVisualStyleBackColor = False
        '
        'pnlAb
        '
        Me.pnlAb.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.pnlAb.Controls.Add(Me.rdoAbWithLH)
        Me.pnlAb.Controls.Add(Me.rdoAbWithoutLH)
        Me.pnlAb.Controls.Add(Me.rdoAbNone)
        Me.pnlAb.Location = New System.Drawing.Point(99, 175)
        Me.pnlAb.Name = "pnlAb"
        Me.pnlAb.Size = New System.Drawing.Size(228, 20)
        Me.pnlAb.TabIndex = 70
        '
        'rdoAbWithLH
        '
        Me.rdoAbWithLH.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.rdoAbWithLH.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAbWithLH.ForeColor = System.Drawing.Color.Black
        Me.rdoAbWithLH.Location = New System.Drawing.Point(60, 1)
        Me.rdoAbWithLH.Name = "rdoAbWithLH"
        Me.rdoAbWithLH.Size = New System.Drawing.Size(92, 18)
        Me.rdoAbWithLH.TabIndex = 13
        Me.rdoAbWithLH.Text = "L H A P C D"
        Me.rdoAbWithLH.UseVisualStyleBackColor = False
        '
        'rdoAbWithoutLH
        '
        Me.rdoAbWithoutLH.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.rdoAbWithoutLH.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAbWithoutLH.ForeColor = System.Drawing.Color.Black
        Me.rdoAbWithoutLH.Location = New System.Drawing.Point(158, 1)
        Me.rdoAbWithoutLH.Name = "rdoAbWithoutLH"
        Me.rdoAbWithoutLH.Size = New System.Drawing.Size(69, 18)
        Me.rdoAbWithoutLH.TabIndex = 12
        Me.rdoAbWithoutLH.Text = "A P C D"
        Me.rdoAbWithoutLH.UseVisualStyleBackColor = False
        '
        'rdoAbNone
        '
        Me.rdoAbNone.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.rdoAbNone.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAbNone.ForeColor = System.Drawing.Color.Black
        Me.rdoAbNone.Location = New System.Drawing.Point(4, 1)
        Me.rdoAbNone.Name = "rdoAbNone"
        Me.rdoAbNone.Size = New System.Drawing.Size(48, 18)
        Me.rdoAbNone.TabIndex = 11
        Me.rdoAbNone.Text = "없음"
        Me.rdoAbNone.UseVisualStyleBackColor = False
        '
        'lblInsuGbn
        '
        Me.lblInsuGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblInsuGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblInsuGbn.ForeColor = System.Drawing.Color.White
        Me.lblInsuGbn.Location = New System.Drawing.Point(99, 25)
        Me.lblInsuGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblInsuGbn.Name = "lblInsuGbn"
        Me.lblInsuGbn.Size = New System.Drawing.Size(108, 20)
        Me.lblInsuGbn.TabIndex = 31
        Me.lblInsuGbn.Text = " 일별 시간 조건"
        Me.lblInsuGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblopDT
        '
        Me.lblopDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblopDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblopDT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblopDT.Location = New System.Drawing.Point(3, 46)
        Me.lblopDT.Margin = New System.Windows.Forms.Padding(1)
        Me.lblopDT.Name = "lblopDT"
        Me.lblopDT.Size = New System.Drawing.Size(95, 20)
        Me.lblopDT.TabIndex = 22
        Me.lblopDT.Text = "기준시간 구분"
        Me.lblopDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAb
        '
        Me.lblAb.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblAb.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblAb.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblAb.Location = New System.Drawing.Point(3, 175)
        Me.lblAb.Margin = New System.Windows.Forms.Padding(1)
        Me.lblAb.Name = "lblAb"
        Me.lblAb.Size = New System.Drawing.Size(95, 20)
        Me.lblAb.TabIndex = 69
        Me.lblAb.Text = "이상자 구분"
        Me.lblAb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlOptDT
        '
        Me.pnlOptDT.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlOptDT.Controls.Add(Me.rdoOptDT2)
        Me.pnlOptDT.Controls.Add(Me.rdoOptDT1)
        Me.pnlOptDT.Location = New System.Drawing.Point(99, 46)
        Me.pnlOptDT.Margin = New System.Windows.Forms.Padding(0)
        Me.pnlOptDT.Name = "pnlOptDT"
        Me.pnlOptDT.Size = New System.Drawing.Size(160, 20)
        Me.pnlOptDT.TabIndex = 26
        '
        'rdoOptDT2
        '
        Me.rdoOptDT2.BackColor = System.Drawing.Color.AliceBlue
        Me.rdoOptDT2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOptDT2.Location = New System.Drawing.Point(84, 1)
        Me.rdoOptDT2.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoOptDT2.Name = "rdoOptDT2"
        Me.rdoOptDT2.Size = New System.Drawing.Size(72, 18)
        Me.rdoOptDT2.TabIndex = 12
        Me.rdoOptDT2.Text = "보고일시"
        Me.rdoOptDT2.UseVisualStyleBackColor = False
        '
        'rdoOptDT1
        '
        Me.rdoOptDT1.BackColor = System.Drawing.Color.AliceBlue
        Me.rdoOptDT1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOptDT1.Location = New System.Drawing.Point(4, 1)
        Me.rdoOptDT1.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoOptDT1.Name = "rdoOptDT1"
        Me.rdoOptDT1.Size = New System.Drawing.Size(72, 18)
        Me.rdoOptDT1.TabIndex = 11
        Me.rdoOptDT1.Text = "접수일시"
        Me.rdoOptDT1.UseVisualStyleBackColor = False
        '
        'cboBccls
        '
        Me.cboBccls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBccls.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBccls.Location = New System.Drawing.Point(212, 196)
        Me.cboBccls.Margin = New System.Windows.Forms.Padding(0)
        Me.cboBccls.Name = "cboBccls"
        Me.cboBccls.Size = New System.Drawing.Size(237, 20)
        Me.cboBccls.TabIndex = 60
        Me.cboBccls.Tag = "TCDGBN_01"
        '
        'lblDT
        '
        Me.lblDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDT.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblDT.Location = New System.Drawing.Point(3, 67)
        Me.lblDT.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDT.Name = "lblDT"
        Me.lblDT.Size = New System.Drawing.Size(95, 20)
        Me.lblDT.TabIndex = 27
        Me.lblDT.Text = "날짜구간 설정"
        Me.lblDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlSect
        '
        Me.pnlSect.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.pnlSect.Controls.Add(Me.rdoBcclsS)
        Me.pnlSect.Controls.Add(Me.rdoBcclsA)
        Me.pnlSect.Location = New System.Drawing.Point(99, 196)
        Me.pnlSect.Name = "pnlSect"
        Me.pnlSect.Size = New System.Drawing.Size(112, 20)
        Me.pnlSect.TabIndex = 59
        '
        'rdoBcclsS
        '
        Me.rdoBcclsS.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.rdoBcclsS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBcclsS.ForeColor = System.Drawing.Color.Black
        Me.rdoBcclsS.Location = New System.Drawing.Point(60, 1)
        Me.rdoBcclsS.Name = "rdoBcclsS"
        Me.rdoBcclsS.Size = New System.Drawing.Size(49, 18)
        Me.rdoBcclsS.TabIndex = 13
        Me.rdoBcclsS.Text = "선택"
        Me.rdoBcclsS.UseVisualStyleBackColor = False
        '
        'rdoBcclsA
        '
        Me.rdoBcclsA.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.rdoBcclsA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBcclsA.ForeColor = System.Drawing.Color.Black
        Me.rdoBcclsA.Location = New System.Drawing.Point(4, 1)
        Me.rdoBcclsA.Name = "rdoBcclsA"
        Me.rdoBcclsA.Size = New System.Drawing.Size(46, 18)
        Me.rdoBcclsA.TabIndex = 11
        Me.rdoBcclsA.Text = "전체"
        Me.rdoBcclsA.UseVisualStyleBackColor = False
        '
        'dtpDT1
        '
        Me.dtpDT1.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDT1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDT1.Location = New System.Drawing.Point(99, 67)
        Me.dtpDT1.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDT1.Name = "dtpDT1"
        Me.dtpDT1.Size = New System.Drawing.Size(106, 21)
        Me.dtpDT1.TabIndex = 28
        Me.dtpDT1.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'lblsect
        '
        Me.lblsect.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblsect.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblsect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblsect.Location = New System.Drawing.Point(3, 196)
        Me.lblsect.Margin = New System.Windows.Forms.Padding(1)
        Me.lblsect.Name = "lblsect"
        Me.lblsect.Size = New System.Drawing.Size(95, 20)
        Me.lblsect.TabIndex = 58
        Me.lblsect.Text = "바코드분류"
        Me.lblsect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboDayNight
        '
        Me.cboDayNight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDayNight.Items.AddRange(New Object() {"", "주간", "야간"})
        Me.cboDayNight.Location = New System.Drawing.Point(407, 67)
        Me.cboDayNight.Margin = New System.Windows.Forms.Padding(0)
        Me.cboDayNight.Name = "cboDayNight"
        Me.cboDayNight.Size = New System.Drawing.Size(43, 20)
        Me.cboDayNight.TabIndex = 68
        Me.cboDayNight.Tag = "TCDGBN_01"
        '
        'dtpDT2
        '
        Me.dtpDT2.CustomFormat = "yyyy-MM-dd HH"
        Me.dtpDT2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDT2.Location = New System.Drawing.Point(221, 67)
        Me.dtpDT2.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDT2.Name = "dtpDT2"
        Me.dtpDT2.Size = New System.Drawing.Size(106, 21)
        Me.dtpDT2.TabIndex = 29
        Me.dtpDT2.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSlip.Location = New System.Drawing.Point(268, 217)
        Me.cboSlip.Margin = New System.Windows.Forms.Padding(0)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(181, 20)
        Me.cboSlip.TabIndex = 56
        Me.cboSlip.Tag = "TCDGBN_01"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(205, 68)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(16, 16)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "~"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlSlip
        '
        Me.pnlSlip.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.pnlSlip.Controls.Add(Me.rdoSlipS)
        Me.pnlSlip.Controls.Add(Me.rdoSlipP)
        Me.pnlSlip.Controls.Add(Me.rdoSlipA)
        Me.pnlSlip.Location = New System.Drawing.Point(99, 217)
        Me.pnlSlip.Name = "pnlSlip"
        Me.pnlSlip.Size = New System.Drawing.Size(168, 20)
        Me.pnlSlip.TabIndex = 53
        '
        'rdoSlipS
        '
        Me.rdoSlipS.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.rdoSlipS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSlipS.ForeColor = System.Drawing.Color.Black
        Me.rdoSlipS.Location = New System.Drawing.Point(116, 1)
        Me.rdoSlipS.Name = "rdoSlipS"
        Me.rdoSlipS.Size = New System.Drawing.Size(48, 18)
        Me.rdoSlipS.TabIndex = 14
        Me.rdoSlipS.Text = "분야"
        Me.rdoSlipS.UseVisualStyleBackColor = False
        '
        'rdoSlipP
        '
        Me.rdoSlipP.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.rdoSlipP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSlipP.ForeColor = System.Drawing.Color.Black
        Me.rdoSlipP.Location = New System.Drawing.Point(60, 1)
        Me.rdoSlipP.Name = "rdoSlipP"
        Me.rdoSlipP.Size = New System.Drawing.Size(48, 18)
        Me.rdoSlipP.TabIndex = 13
        Me.rdoSlipP.Text = "부서"
        Me.rdoSlipP.UseVisualStyleBackColor = False
        '
        'rdoSlipA
        '
        Me.rdoSlipA.BackColor = System.Drawing.Color.BlanchedAlmond
        Me.rdoSlipA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoSlipA.ForeColor = System.Drawing.Color.Black
        Me.rdoSlipA.Location = New System.Drawing.Point(4, 1)
        Me.rdoSlipA.Name = "rdoSlipA"
        Me.rdoSlipA.Size = New System.Drawing.Size(48, 18)
        Me.rdoSlipA.TabIndex = 11
        Me.rdoSlipA.Text = "전체"
        Me.rdoSlipA.UseVisualStyleBackColor = False
        '
        'lblslip
        '
        Me.lblslip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblslip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblslip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblslip.Location = New System.Drawing.Point(3, 217)
        Me.lblslip.Margin = New System.Windows.Forms.Padding(1)
        Me.lblslip.Name = "lblslip"
        Me.lblslip.Size = New System.Drawing.Size(95, 20)
        Me.lblslip.TabIndex = 52
        Me.lblslip.Text = "부서/분야"
        Me.lblslip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpTM1
        '
        Me.dtpTM1.CustomFormat = "HH"
        Me.dtpTM1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTM1.Location = New System.Drawing.Point(222, 90)
        Me.dtpTM1.Name = "dtpTM1"
        Me.dtpTM1.Size = New System.Drawing.Size(45, 21)
        Me.dtpTM1.TabIndex = 32
        Me.dtpTM1.Value = New Date(2008, 1, 23, 0, 0, 0, 0)
        '
        'dtpTM2
        '
        Me.dtpTM2.CustomFormat = "HH"
        Me.dtpTM2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTM2.Location = New System.Drawing.Point(288, 90)
        Me.dtpTM2.Name = "dtpTM2"
        Me.dtpTM2.Size = New System.Drawing.Size(45, 21)
        Me.dtpTM2.TabIndex = 33
        Me.dtpTM2.Value = New Date(2008, 1, 23, 23, 59, 59, 0)
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(274, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(8, 16)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "~"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTM
        '
        Me.lblTM.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTM.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTM.ForeColor = System.Drawing.Color.White
        Me.lblTM.Location = New System.Drawing.Point(99, 90)
        Me.lblTM.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTM.Name = "lblTM"
        Me.lblTM.Size = New System.Drawing.Size(122, 20)
        Me.lblTM.TabIndex = 36
        Me.lblTM.Text = " 일별 시간대 설정"
        Me.lblTM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIO
        '
        Me.lblIO.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIO.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblIO.Location = New System.Drawing.Point(3, 112)
        Me.lblIO.Margin = New System.Windows.Forms.Padding(1)
        Me.lblIO.Name = "lblIO"
        Me.lblIO.Size = New System.Drawing.Size(95, 20)
        Me.lblIO.TabIndex = 37
        Me.lblIO.Text = "외래/입원"
        Me.lblIO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlIO
        '
        Me.pnlIO.BackColor = System.Drawing.Color.Cornsilk
        Me.pnlIO.Controls.Add(Me.rdoIOC)
        Me.pnlIO.Controls.Add(Me.rdoIOO)
        Me.pnlIO.Controls.Add(Me.rdoIOI)
        Me.pnlIO.Controls.Add(Me.rdoIOA)
        Me.pnlIO.Location = New System.Drawing.Point(99, 112)
        Me.pnlIO.Name = "pnlIO"
        Me.pnlIO.Size = New System.Drawing.Size(219, 20)
        Me.pnlIO.TabIndex = 40
        '
        'rdoIOC
        '
        Me.rdoIOC.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOC.ForeColor = System.Drawing.Color.Black
        Me.rdoIOC.Location = New System.Drawing.Point(170, 1)
        Me.rdoIOC.Name = "rdoIOC"
        Me.rdoIOC.Size = New System.Drawing.Size(48, 18)
        Me.rdoIOC.TabIndex = 14
        Me.rdoIOC.Text = "수탁"
        Me.rdoIOC.UseVisualStyleBackColor = False
        '
        'rdoIOO
        '
        Me.rdoIOO.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOO.ForeColor = System.Drawing.Color.Black
        Me.rdoIOO.Location = New System.Drawing.Point(60, 1)
        Me.rdoIOO.Name = "rdoIOO"
        Me.rdoIOO.Size = New System.Drawing.Size(48, 18)
        Me.rdoIOO.TabIndex = 13
        Me.rdoIOO.Text = "외래"
        Me.rdoIOO.UseVisualStyleBackColor = False
        '
        'rdoIOI
        '
        Me.rdoIOI.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOI.ForeColor = System.Drawing.Color.Black
        Me.rdoIOI.Location = New System.Drawing.Point(116, 1)
        Me.rdoIOI.Name = "rdoIOI"
        Me.rdoIOI.Size = New System.Drawing.Size(48, 18)
        Me.rdoIOI.TabIndex = 12
        Me.rdoIOI.Text = "입원"
        Me.rdoIOI.UseVisualStyleBackColor = False
        '
        'rdoIOA
        '
        Me.rdoIOA.BackColor = System.Drawing.Color.Cornsilk
        Me.rdoIOA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIOA.ForeColor = System.Drawing.Color.Black
        Me.rdoIOA.Location = New System.Drawing.Point(4, 1)
        Me.rdoIOA.Name = "rdoIOA"
        Me.rdoIOA.Size = New System.Drawing.Size(48, 18)
        Me.rdoIOA.TabIndex = 11
        Me.rdoIOA.Text = "전체"
        Me.rdoIOA.UseVisualStyleBackColor = False
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDept.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDept.ForeColor = System.Drawing.Color.White
        Me.lblDept.Location = New System.Drawing.Point(98, 133)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(58, 20)
        Me.lblDept.TabIndex = 41
        Me.lblDept.Text = " 진료과"
        Me.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlDept
        '
        Me.pnlDept.BackColor = System.Drawing.Color.Honeydew
        Me.pnlDept.Controls.Add(Me.rdoDeptS)
        Me.pnlDept.Controls.Add(Me.rdoDeptA)
        Me.pnlDept.Location = New System.Drawing.Point(158, 133)
        Me.pnlDept.Name = "pnlDept"
        Me.pnlDept.Size = New System.Drawing.Size(112, 20)
        Me.pnlDept.TabIndex = 42
        '
        'rdoDeptS
        '
        Me.rdoDeptS.BackColor = System.Drawing.Color.Honeydew
        Me.rdoDeptS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDeptS.ForeColor = System.Drawing.Color.Black
        Me.rdoDeptS.Location = New System.Drawing.Point(60, 1)
        Me.rdoDeptS.Name = "rdoDeptS"
        Me.rdoDeptS.Size = New System.Drawing.Size(48, 18)
        Me.rdoDeptS.TabIndex = 13
        Me.rdoDeptS.Text = "선택"
        Me.rdoDeptS.UseVisualStyleBackColor = False
        '
        'rdoDeptA
        '
        Me.rdoDeptA.BackColor = System.Drawing.Color.Honeydew
        Me.rdoDeptA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoDeptA.ForeColor = System.Drawing.Color.Black
        Me.rdoDeptA.Location = New System.Drawing.Point(4, 1)
        Me.rdoDeptA.Name = "rdoDeptA"
        Me.rdoDeptA.Size = New System.Drawing.Size(48, 18)
        Me.rdoDeptA.TabIndex = 11
        Me.rdoDeptA.Text = "전체"
        Me.rdoDeptA.UseVisualStyleBackColor = False
        '
        'cboDept
        '
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDept.Location = New System.Drawing.Point(271, 133)
        Me.cboDept.Margin = New System.Windows.Forms.Padding(0)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.Size = New System.Drawing.Size(179, 20)
        Me.cboDept.TabIndex = 43
        Me.cboDept.Tag = ""
        '
        'lblWard
        '
        Me.lblWard.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWard.ForeColor = System.Drawing.Color.White
        Me.lblWard.Location = New System.Drawing.Point(98, 154)
        Me.lblWard.Name = "lblWard"
        Me.lblWard.Size = New System.Drawing.Size(75, 20)
        Me.lblWard.TabIndex = 44
        Me.lblWard.Text = " 병동 선택"
        Me.lblWard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWard.Location = New System.Drawing.Point(290, 154)
        Me.cboWard.Margin = New System.Windows.Forms.Padding(0)
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(160, 20)
        Me.cboWard.TabIndex = 46
        Me.cboWard.Tag = "TCDGBN_01"
        '
        'pnlWard
        '
        Me.pnlWard.BackColor = System.Drawing.Color.LavenderBlush
        Me.pnlWard.Controls.Add(Me.rdoWardS)
        Me.pnlWard.Controls.Add(Me.rdoWardA)
        Me.pnlWard.Location = New System.Drawing.Point(177, 154)
        Me.pnlWard.Name = "pnlWard"
        Me.pnlWard.Size = New System.Drawing.Size(112, 20)
        Me.pnlWard.TabIndex = 45
        '
        'rdoWardS
        '
        Me.rdoWardS.BackColor = System.Drawing.Color.LavenderBlush
        Me.rdoWardS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWardS.ForeColor = System.Drawing.Color.Black
        Me.rdoWardS.Location = New System.Drawing.Point(60, 1)
        Me.rdoWardS.Name = "rdoWardS"
        Me.rdoWardS.Size = New System.Drawing.Size(48, 18)
        Me.rdoWardS.TabIndex = 13
        Me.rdoWardS.Text = "선택"
        Me.rdoWardS.UseVisualStyleBackColor = False
        '
        'rdoWardA
        '
        Me.rdoWardA.BackColor = System.Drawing.Color.LavenderBlush
        Me.rdoWardA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoWardA.ForeColor = System.Drawing.Color.Black
        Me.rdoWardA.Location = New System.Drawing.Point(4, 1)
        Me.rdoWardA.Name = "rdoWardA"
        Me.rdoWardA.Size = New System.Drawing.Size(48, 18)
        Me.rdoWardA.TabIndex = 11
        Me.rdoWardA.Text = "전체"
        Me.rdoWardA.UseVisualStyleBackColor = False
        '
        'grp02
        '
        Me.grp02.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grp02.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grp02.Controls.Add(Me.spdTest)
        Me.grp02.Controls.Add(Me.Panel9)
        Me.grp02.Controls.Add(Me.lblTest)
        Me.grp02.Location = New System.Drawing.Point(1, 253)
        Me.grp02.Name = "grp02"
        Me.grp02.Size = New System.Drawing.Size(455, 255)
        Me.grp02.TabIndex = 23
        Me.grp02.TabStop = False
        '
        'spdTest
        '
        Me.spdTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdTest.DataSource = Nothing
        Me.spdTest.Location = New System.Drawing.Point(2, 33)
        Me.spdTest.Name = "spdTest"
        Me.spdTest.OcxState = CType(resources.GetObject("spdTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTest.Size = New System.Drawing.Size(447, 218)
        Me.spdTest.TabIndex = 116
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.Thistle
        Me.Panel9.Controls.Add(Me.rdoTestS)
        Me.Panel9.Controls.Add(Me.rdoTestA)
        Me.Panel9.Location = New System.Drawing.Point(99, 11)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(112, 20)
        Me.Panel9.TabIndex = 65
        '
        'rdoTestS
        '
        Me.rdoTestS.BackColor = System.Drawing.Color.Thistle
        Me.rdoTestS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTestS.ForeColor = System.Drawing.Color.Black
        Me.rdoTestS.Location = New System.Drawing.Point(60, 1)
        Me.rdoTestS.Name = "rdoTestS"
        Me.rdoTestS.Size = New System.Drawing.Size(48, 18)
        Me.rdoTestS.TabIndex = 13
        Me.rdoTestS.Text = "선택"
        Me.rdoTestS.UseVisualStyleBackColor = False
        '
        'rdoTestA
        '
        Me.rdoTestA.BackColor = System.Drawing.Color.Thistle
        Me.rdoTestA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTestA.ForeColor = System.Drawing.Color.Black
        Me.rdoTestA.Location = New System.Drawing.Point(4, 1)
        Me.rdoTestA.Name = "rdoTestA"
        Me.rdoTestA.Size = New System.Drawing.Size(48, 18)
        Me.rdoTestA.TabIndex = 11
        Me.rdoTestA.Text = "전체"
        Me.rdoTestA.UseVisualStyleBackColor = False
        '
        'lblTest
        '
        Me.lblTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest.ForeColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.lblTest.Location = New System.Drawing.Point(3, 11)
        Me.lblTest.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(95, 20)
        Me.lblTest.TabIndex = 64
        Me.lblTest.Text = "검사 선택"
        Me.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems1
        Me.btnSearch.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSearch.Corners.All = CType(6, Short)
        Me.btnSearch.Corners.LowerLeft = CType(6, Short)
        Me.btnSearch.Corners.LowerRight = CType(6, Short)
        Me.btnSearch.Corners.UpperLeft = CType(6, Short)
        Me.btnSearch.Corners.UpperRight = CType(6, Short)
        Me.btnSearch.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSearch.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSearch.FocalPoints.CenterPtX = 0.4859813!
        Me.btnSearch.FocalPoints.CenterPtY = 0.16!
        Me.btnSearch.FocalPoints.FocusPtX = 0.0!
        Me.btnSearch.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker2
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(581, 663)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSearch.SideImage = Nothing
        Me.btnSearch.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSearch.Size = New System.Drawing.Size(107, 25)
        Me.btnSearch.TabIndex = 185
        Me.btnSearch.Text = "검사통계조회"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSearch.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'CButton1
        '
        Me.CButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton1.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.CButton1.ColorFillBlend = CBlendItems2
        Me.CButton1.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.CButton1.Corners.All = CType(6, Short)
        Me.CButton1.Corners.LowerLeft = CType(6, Short)
        Me.CButton1.Corners.LowerRight = CType(6, Short)
        Me.CButton1.Corners.UpperLeft = CType(6, Short)
        Me.CButton1.Corners.UpperRight = CType(6, Short)
        Me.CButton1.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.CButton1.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.CButton1.FocalPoints.CenterPtX = 0.5!
        Me.CButton1.FocalPoints.CenterPtY = 0.0!
        Me.CButton1.FocalPoints.FocusPtX = 0.03738318!
        Me.CButton1.FocalPoints.FocusPtY = 0.04!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.CButton1.FocusPtTracker = DesignerRectTracker4
        Me.CButton1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.CButton1.ForeColor = System.Drawing.Color.White
        Me.CButton1.Image = Nothing
        Me.CButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton1.ImageIndex = 0
        Me.CButton1.ImageSize = New System.Drawing.Size(16, 16)
        Me.CButton1.Location = New System.Drawing.Point(689, 663)
        Me.CButton1.Name = "CButton1"
        Me.CButton1.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.CButton1.SideImage = Nothing
        Me.CButton1.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CButton1.SideImageSize = New System.Drawing.Size(48, 48)
        Me.CButton1.Size = New System.Drawing.Size(107, 25)
        Me.CButton1.TabIndex = 189
        Me.CButton1.Text = "To Excel"
        Me.CButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.CButton1.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0.0!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(797, 663)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 190
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(905, 663)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(107, 25)
        Me.btnExit.TabIndex = 191
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnAnalysis
        '
        Me.btnAnalysis.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAnalysis.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnAnalysis.ColorFillBlend = CBlendItems5
        Me.btnAnalysis.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnAnalysis.Corners.All = CType(6, Short)
        Me.btnAnalysis.Corners.LowerLeft = CType(6, Short)
        Me.btnAnalysis.Corners.LowerRight = CType(6, Short)
        Me.btnAnalysis.Corners.UpperLeft = CType(6, Short)
        Me.btnAnalysis.Corners.UpperRight = CType(6, Short)
        Me.btnAnalysis.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnAnalysis.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnAnalysis.FocalPoints.CenterPtX = 0.4859813!
        Me.btnAnalysis.FocalPoints.CenterPtY = 0.16!
        Me.btnAnalysis.FocalPoints.FocusPtX = 0.0!
        Me.btnAnalysis.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAnalysis.FocusPtTracker = DesignerRectTracker10
        Me.btnAnalysis.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnAnalysis.ForeColor = System.Drawing.Color.White
        Me.btnAnalysis.Image = Nothing
        Me.btnAnalysis.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAnalysis.ImageIndex = 0
        Me.btnAnalysis.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnAnalysis.Location = New System.Drawing.Point(5, 663)
        Me.btnAnalysis.Name = "btnAnalysis"
        Me.btnAnalysis.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnAnalysis.SideImage = Nothing
        Me.btnAnalysis.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAnalysis.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnAnalysis.Size = New System.Drawing.Size(127, 25)
        Me.btnAnalysis.TabIndex = 192
        Me.btnAnalysis.Text = "통계 분석/재분석"
        Me.btnAnalysis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAnalysis.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnAnalysis.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGT02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 690)
        Me.Controls.Add(Me.btnAnalysis)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.CButton1)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.tclStatistics)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGT02"
        Me.Text = "검사통계 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tclStatistics.ResumeLayout(False)
        Me.tpgVar.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdStatistics, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.PnlDMYsel.ResumeLayout(False)
        Me.pnlAb.ResumeLayout(False)
        Me.pnlOptDT.ResumeLayout(False)
        Me.pnlSect.ResumeLayout(False)
        Me.pnlSlip.ResumeLayout(False)
        Me.pnlIO.ResumeLayout(False)
        Me.pnlDept.ResumeLayout(False)
        Me.pnlWard.ResumeLayout(False)
        Me.grp02.ResumeLayout(False)
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel9.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FGT02_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    '> Control Event
    Private Sub FGT02_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If m_fgt02_anal.mbAnalyzing Then
            e.Cancel = True

            MsgBox(m_fgt02_anal.Text + "을 진행 중이라 종료할 수 없습니다. 나중에 다시 시도하십시요!!", MsgBoxStyle.Exclamation)

            Return
        End If

        If m_fgt02_anal IsNot Nothing Then
            m_fgt02_anal.mbForceClose = True
            m_fgt02_anal.Dispose()
            m_fgt02_anal.Close()
        End If

    End Sub

    Private Sub btnAnalysis_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnalysis.Click

        Try
            '> m_fgt02_anal의 Control 값 변경
            If Me.rdoOptDT1.Checked Then
                m_fgt02_anal.lblDay.Text = "접수일자"

            ElseIf Me.rdoOptDT2.Checked Then
                m_fgt02_anal.lblDay.Text = "보고일자"

            End If

            Dim dtB As Date, dtE As Date

            If Me.rdoDay.Checked Then
                '일별
                dtB = Me.dtpDT1.Value
                dtE = Me.dtpDT2.Value

                m_fgt02_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt02_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            ElseIf Me.rdoMonth.Checked Then
                '월별
                dtB = CDate(Me.dtpDT1.Value.ToString("yyyy-MM") + "-" + "01")
                dtE = CDate(Me.dtpDT2.Value.ToString("yyyy-MM") + "-" + Date.DaysInMonth(Me.dtpDT2.Value.Year, Me.dtpDT2.Value.Month).ToString("00"))

                m_fgt02_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt02_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            ElseIf Me.rdoYear.Checked Then
                '연별
                dtB = CDate(Me.dtpDT1.Value.ToString("yyyy") + "-" + "01-01")
                dtE = CDate(Me.dtpDT2.Value.ToString("yyyy") + "-" + "12-31")

                m_fgt02_anal.dtpDayB.Value = CDate(dtB.ToString("yyyy-MM-dd"))
                m_fgt02_anal.dtpDayE.Value = CDate(dtE.ToString("yyyy-MM-dd"))

            End If

            m_fgt02_anal.fnDisplay_ResultOfAnalysis()

            m_fgt02_anal.TopLevel = True
            m_fgt02_anal.Show()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnExcel_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CButton1.Click
        Dim sBuf As String = ""

        With spdStatistics
            .ReDraw = False

            .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

            .MaxRows = .MaxRows + 1
            .InsertRows(1, 1)

            For i As Integer = 1 To .MaxCols
                .Col = i : .Row = 0 : sBuf = .Text
                .Col = i : .Row = 1 : .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText : .Text = sBuf
            Next

            If .ExportToExcel("statistics.xls", "Statistics", "") Then
                Process.Start("statistics.xls")
            End If

            .DeleteRows(1, 1)
            .MaxRows -= 1

            .ReDraw = True
        End With
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim sFn As String = "btnExit_Click"

        Try
            Me.Close()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            fnDisplayStatistics()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub cboTimeCfg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTimeCfg.SelectedIndexChanged

        If miSelectKey = 1 Then Return

        Try
            Select Case Me.cboTimeCfg.SelectedIndex
                Case 0
                    Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
                    Me.dtpDT2.CustomFormat = "yyyy-MM-dd"

                    Me.dtpTM1.Enabled = False
                    Me.dtpTM2.Enabled = False

                    Me.cboDayNight.SelectedIndex = 0
                    Me.cboDayNight.Enabled = True

                Case 1
                    Me.dtpDT1.CustomFormat = "yyyy-MM-dd HH"
                    Me.dtpDT2.CustomFormat = "yyyy-MM-dd HH"

                    Me.dtpTM1.Enabled = False
                    Me.dtpTM2.Enabled = False

                    Me.cboDayNight.SelectedIndex = 0
                    Me.cboDayNight.Enabled = False

                Case 2
                    Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
                    Me.dtpDT2.CustomFormat = "yyyy-MM-dd"

                    Me.dtpTM1.Enabled = True
                    Me.dtpTM2.Enabled = True

                    Me.cboDayNight.SelectedIndex = 0
                    Me.cboDayNight.Enabled = False

            End Select

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoDayMonthYear_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDay.CheckedChanged, rdoMonth.CheckedChanged, rdoYear.CheckedChanged

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDay.Checked Then
                '일별 체크 시
                Me.cboTimeCfg.Enabled = True

                Select Case Me.cboTimeCfg.SelectedIndex
                    Case 0
                        Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
                        Me.dtpDT2.CustomFormat = "yyyy-MM-dd"

                    Case 1
                        Me.dtpDT1.CustomFormat = "yyyy-MM-dd HH"
                        Me.dtpDT2.CustomFormat = "yyyy-MM-dd HH"

                    Case 2
                        Me.dtpDT1.CustomFormat = "yyyy-MM-dd"
                        Me.dtpDT2.CustomFormat = "yyyy-MM-dd"

                End Select

            ElseIf Me.rdoMonth.Checked Then
                '월별 체크 시
                Me.cboTimeCfg.SelectedIndex = 0
                Me.cboTimeCfg.Enabled = False

                Me.dtpDT1.CustomFormat = "yyyy-MM"
                Me.dtpDT2.CustomFormat = "yyyy-MM"

                Me.dtpTM1.Enabled = False
                Me.dtpTM2.Enabled = False

            ElseIf Me.rdoYear.Checked Then
                '연별 체크 시
                Me.cboTimeCfg.SelectedIndex = 0
                Me.cboTimeCfg.Enabled = False

                Me.dtpDT1.CustomFormat = "yyyy"
                Me.dtpDT2.CustomFormat = "yyyy"

                Me.dtpTM1.Enabled = False
                Me.dtpTM2.Enabled = False

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoDept_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoDeptA.CheckedChanged, rdoDeptS.CheckedChanged

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoDeptA.Checked Then
                '전체
                Me.cboDept.SelectedIndex = -1 : Me.cboDept.Enabled = False

            ElseIf Me.rdoDeptS.Checked Then
                '선택
                If Not Me.cboDept.Items.Count > 0 Then
                    sbDisplay_dept()
                End If

                If Me.cboDept.Items.Count = 0 Then Return

                Me.cboDept.SelectedIndex = 0 : Me.cboDept.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoIO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIOA.CheckedChanged, rdoIOO.CheckedChanged, rdoIOI.CheckedChanged, rdoIOC.CheckedChanged

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoIOA.Checked Then
                '전체
                Me.rdoDeptA.Checked = True
                Me.rdoWardA.Checked = True

                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = False

                Me.cboDept.Enabled = False
                Me.cboWard.Enabled = False

            ElseIf Me.rdoIOO.Checked Or Me.rdoIOC.Checked Then
                '외래
                Me.rdoWardA.Checked = True

                Me.pnlDept.Enabled = True
                Me.pnlWard.Enabled = False

            ElseIf Me.rdoIOI.Checked Then
                '입원
                Me.rdoDeptA.Checked = True

                Me.pnlDept.Enabled = False
                Me.pnlWard.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoBccls_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoBcclsA.CheckedChanged, rdoBcclsS.CheckedChanged

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoBcclsA.Checked Then
                '전체
                Me.cboBccls.SelectedIndex = -1 : Me.cboBccls.Enabled = False

            ElseIf Me.rdoBcclsS.Checked Then
                '계
                sbDisplay_bccls()

                If Me.cboBccls.Items.Count = 0 Then Return

                Me.cboBccls.SelectedIndex = 0 : Me.cboBccls.Enabled = True

                ' ElseIf Me.rdoSectT.Checked Then
                '검사계
                ' sbGetTSectInfo()

                If Me.cboBccls.Items.Count = 0 Then Return

                Me.cboBccls.SelectedIndex = 0 : Me.cboBccls.Enabled = True

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoSlip_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoSlipA.CheckedChanged, rdoSlipP.CheckedChanged, rdoSlipS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoSlipA.Checked Then
                '전체
                Me.cboSlip.SelectedIndex = -1 : Me.cboSlip.Enabled = False

            ElseIf Me.rdoSlipP.Checked Then
                '부서
                sbDisplay_part()

                If Me.cboSlip.Items.Count = 0 Then Return

                Me.cboSlip.SelectedIndex = 0 : Me.cboSlip.Enabled = True

            ElseIf Me.rdoSlipS.Checked Then
                '분야
                sbDisplay_slip()

                If Me.cboSlip.Items.Count = 0 Then Return

                Me.cboSlip.SelectedIndex = 0 : Me.cboSlip.Enabled = True

            End If
            'Me.cboBccls.Items.Clear()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoTest_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTestA.CheckedChanged, rdoTestS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoTestA.Checked Then
                '전체
                Me.spdTest.MaxRows = 0

            ElseIf Me.rdoTestS.Checked Then
                '선택
                sbDisplay_test()

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub rdoWard_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoWardA.CheckedChanged, rdoWardS.CheckedChanged

        If miSelectKey = 1 Then Return
        If CType(sender, RadioButton).Checked = False Then Return

        Try
            If Me.rdoWardA.Checked Then
                '전체
                Me.cboWard.SelectedIndex = -1 : Me.cboWard.Enabled = False

            ElseIf Me.rdoWardS.Checked Then
                '선택
                If Not Me.cboWard.Items.Count > 0 Then
                    sbDisplay_ward()
                End If

                If Me.cboWard.Items.Count = 0 Then Return

                Me.cboWard.SelectedIndex = 0 : Me.cboWard.Enabled = True

            End If

        Catch ex As Exception
           CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub cboTest_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged, cboBccls.SelectedIndexChanged, cboBccls.SelectedIndexChanged, cboTGrp.SelectedIndexChanged

        If miSelectKey = 1 Then Return

        Try
            Me.rdoTestA.Checked = True

            Me.spdTest.MaxRows = 0

            If CType(sender, ComboBox).Name.ToUpper = "CBOSECT" Then
                'rdoWkGrp_CheckedChanged(IIf(rdoWkGrpA.Checked, rdoWkGrpA, rdoWkGrpS), e)
            ElseIf CType(sender, ComboBox).Name.ToUpper = "CBOWKGRP" Then
                rdoTGrpA_CheckedChanged(IIf(rdoTGrpA.Checked, rdoTGrpA, rdoTGrpS), e)
            ElseIf CType(sender, ComboBox).Name.ToUpper = "CBOTGRP" Then
                rdoTest_CheckedChanged(IIf(rdoTestA.Checked, rdoTestA, rdoTestS), e)
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub spdTest_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdTest.ButtonClicked

        If miSelectKey = 1 Then Return

        Try
            Dim iChkCnt As Integer = 0

            With Me.spdTest
                For i As Integer = 1 To .MaxRows
                    .Col = 1 : .Row = i : Dim sChk As String = .Text

                    If sChk = "1" Then
                        iChkCnt += 1
                    End If
                Next

                'If iChkCnt > CType(Me.txtMaxTest.Text, Integer) Then
                '    miSelectKey = 1

                '    MsgBox("최대 선택가능한 검사 수를 초과하였습니다!!")
                '    .Col = 1 : .Row = e.row : .Text = ""
                'End If
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            miSelectKey = 0

        End Try
    End Sub

    Private Sub FGT02_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)

        End Select
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.spdStatistics.MaxRows = 0
    End Sub

    Private Sub rdoTGrpA_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoTGrpA.CheckedChanged, rdoTGrpS.CheckedChanged

        If miSelectKey = 1 Then Return

        If CType(sender, RadioButton).Checked = False Then Return

        Try
            '전체
            If Me.rdoTGrpA.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOTGRPA" Then
                Me.cboTGrp.SelectedIndex = -1 : Me.cboTGrp.Enabled = False

            ElseIf rdoTGrpS.Checked And CType(sender, RadioButton).Name.ToUpper = "RDOTGRPS" Then
                sbDisplay_tgrp()

                If Me.cboTGrp.Items.Count = 0 Then Return
                Me.cboTGrp.SelectedIndex = 0 : Me.cboTGrp.Enabled = True
            End If

        Catch ex As Exception
           CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub FGT02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)
    End Sub
End Class
