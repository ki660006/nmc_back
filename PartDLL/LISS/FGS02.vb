'>>> TurnAroundTime 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports COMMON.CommFN
Imports common.commlogin.login
Imports COMMON.SVar

Imports LISAPP.APP_DB
Imports LISAPP.APP_S.TatFn

Public Class FGS02
    Inherits System.Windows.Forms.Form

    Private Const msXmlDir As String = "\XML"
    Private msFile_Slip As String = Windows.Forms.Application.StartupPath + msXmlDir + "\FGS02_Slip.XML"
    Private msFile_Test As String = Windows.Forms.Application.StartupPath + msXmlDir + "\FGS02_TEST.XML"
    Private msFile_OverYn As String = Windows.Forms.Application.StartupPath + msXmlDir + "\FGS02_OverYN.XML"

    Private m_tooltip As New Windows.Forms.ToolTip

    'Private mPartItem As New Item
    Private mbQuery As Boolean = False
    Friend WithEvents txtSelTest As System.Windows.Forms.TextBox
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Private mbEscape As Boolean = False

    Private mbMicro As Boolean = False
    Friend WithEvents chkEditColumn As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear_Tcls As System.Windows.Forms.Button
    Protected Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPatnm As System.Windows.Forms.TextBox
    Friend WithEvents txtDeptWard As System.Windows.Forms.TextBox
    Protected Friend WithEvents lblIOGbn As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rdoBaseOrd As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBaseTst As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents rdoBaseTSect As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBaseTkDt As System.Windows.Forms.RadioButton

    '< add yjlee 
    Private msSpdForm As String = Application.StartupPath & "\SSF\FGS02_SPDLIST.SS7"
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents rdoIogbnI As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIogbnO As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents rdoIogbnA As System.Windows.Forms.RadioButton
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Protected Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnCdHelp_dept As System.Windows.Forms.Button
    Friend WithEvents chkTATCont As System.Windows.Forms.CheckBox
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents spdStList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnCmtDel As CButtonLib.CButton
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboCmt As System.Windows.Forms.ComboBox
    Friend WithEvents btnWcmt As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button

#Region " Form내부 함수 "

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim alPrint As New ArrayList

            If Me.cboPrint.SelectedIndex = 0 Then
                With Me.spdList
                    For iRow As Integer = 1 To .MaxRows
                        If iRow = 243 Then
                            Dim ss As String = ""


                        End If

                        Dim dd As Integer = .MaxRows
                        .Row = iRow
                        Dim sBuf() As String = rsTitle_Item.Split("|"c)
                        Dim alItem As New ArrayList

                        For ix As Integer = 0 To sBuf.Length - 1

                            If sBuf(ix) = "" Then Exit For

                            Dim iCol As Integer = .GetColFromID(sBuf(ix).Split("^"c)(1))

                            If iCol > 0 Then

                                Dim sTitle As String = sBuf(ix).Split("^"c)(0)
                                Dim sField As String = sBuf(ix).Split("^"c)(1)
                                Dim sWidth As String = sBuf(ix).Split("^"c)(2)

                                .Row = iRow
                                .Col = .GetColFromID(sField) : Dim sVal As String = .Text

                                If sField = "tatcont" Then sVal = Ctrl.Get_Name(sVal)

                                alItem.Add(sVal + "^" + sTitle + "^" + sWidth + "^")
                            End If
                        Next

                        Dim objPat As New FGS00_PATINFO

                        With objPat
                            .alItem = alItem
                        End With

                        alPrint.Add(objPat)
                    Next
                End With
            Else
                With Me.spdStList
                    For iRow As Integer = 1 To .MaxRows
                        .Row = iRow
                        Dim sBuf() As String = rsTitle_Item.Split("|"c)
                        Dim alItem As New ArrayList

                        For ix As Integer = 0 To sBuf.Length - 1

                            If sBuf(ix) = "" Then Exit For

                            Dim iCol As Integer = .GetColFromID(sBuf(ix).Split("^"c)(1))

                            If iCol > 0 Then

                                Dim sTitle As String = sBuf(ix).Split("^"c)(0)
                                Dim sField As String = sBuf(ix).Split("^"c)(1)
                                Dim sWidth As String = sBuf(ix).Split("^"c)(2)

                                .Row = iRow
                                .Col = .GetColFromID(sField) : Dim sVal As String = .Text

                                alItem.Add(sVal + "^" + sTitle + "^" + sWidth + "^")
                            End If
                        Next

                        Dim objPat As New FGS00_PATINFO

                        With objPat
                            .alItem = alItem
                        End With

                        alPrint.Add(objPat)
                    Next
                End With
            End If


            If alPrint.Count > 0 Then
                Dim prt As New FGS00_PRINT

                prt.mbLandscape = True  '-- false : 세로, true : 가로
                prt.msTitle = IIf(Me.cboPrint.SelectedIndex = 0, "TurnAroundTime 목록", "TAT 사유 통계").ToString
                prt.maPrtData = alPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview("FGS02")
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        If Me.cboPrint.SelectedIndex = 0 Then
            With spdList
                For ix As Integer = 2 To .MaxCols

                    .Row = 0 : .Col = ix
                    If .ColHidden = False Then
                        stu_item = New STU_PrtItemInfo

                        If .ColID = "regno" Or .ColID = "patnm" Or .ColID = "sexage" Or .ColID = "deptcd" Or .ColID = "tnmd" Or _
                           .ColID = "tkdt" Or .ColID = "fndt" Or .ColID = "tat2" Or .ColID = "tat3" Or .ColID = "tatcont" Or _
                           .ColID = "bcno" Then
                            stu_item.CHECK = "1"
                        Else
                            stu_item.CHECK = "0"
                        End If
                        stu_item.TITLE = .Text
                        stu_item.FIELD = .ColID
                        If .ColID = "tatcont" Then
                            stu_item.WIDTH = (.get_ColWidth(ix) * 10 + 50).ToString
                        Else
                            stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString
                        End If
                        alItems.Add(stu_item)
                    End If
                Next

            End With
        Else
            With spdStList
                For ix As Integer = 1 To .MaxCols

                    .Row = 0 : .Col = ix
                    If .ColHidden = False Then
                        stu_item = New STU_PrtItemInfo

                        stu_item.CHECK = "1"

                        stu_item.TITLE = .Text
                        stu_item.FIELD = .ColID
                        stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString

                        alItems.Add(stu_item)
                    End If
                Next

            End With
        End If

        Return alItems

    End Function

    ' Form초기화
    Private Sub sbFormInitialize()
        Dim objComm As New ServerDateTime

        Try
            ' 로그인정보 설정
            lblUserId.Text = USER_INFO.USRID
            lblUserNm.Text = USER_INFO.USRNM

            '-- 서버날짜로 설정
            dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
            dtpDate0.Value = dtpDate1.Value

            sbFormClear(0)

            sbSpreadColHidden(True)

            '검사분야 표시
            sbDisplay_slip()

            DS_FormDesige.sbInti(Me)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try


    End Sub

    ' 화면정리
    Private Sub sbFormClear(ByVal riPhase As Integer)

        Try
            If InStr("0", riPhase.ToString, CompareMethod.Text) > 0 Then
                spdList.MaxRows = 0
                Me.txtSelTest.Text = ""
                Me.txtSelTest.Tag = ""
                Me.txtPatnm.Text = ""
                Me.txtRegNo.Text = ""
                Me.txtDeptWard.Text = ""
                Me.txtDeptWard.Tag = ""
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try


    End Sub

    ' 칼럼 Hidden 유무
    Private Sub sbSpreadColHidden(ByVal rbFlag As Boolean)

        Try
            With spdList
                .Col = .GetColFromID("tat1_mi") : .ColHidden = rbFlag
                .Col = .GetColFromID("tat2_mi") : .ColHidden = rbFlag
                .Col = .GetColFromID("prptmi") : .ColHidden = rbFlag
                .Col = .GetColFromID("frptmi") : .ColHidden = rbFlag
                .Col = .GetColFromID("partcd") : .ColHidden = rbFlag
                .Col = .GetColFromID("ovt1") : .ColHidden = rbFlag
                .Col = .GetColFromID("ovt2") : .ColHidden = rbFlag
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub sbDisplay_slip()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List()

            Me.cboSlip.Items.Clear()

            If dt.Rows.Count < 1 Then Return

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXmlDir, msFile_Slip, "SLIP")

            If sTmp = "" Then
                Me.cboSlip.SelectedIndex = 0
            Else
                If CInt(sTmp) < cboSlip.Items.Count Then Me.cboSlip.SelectedIndex = CInt(sTmp)
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    ' 조회
    Private Sub sbQuery()

        Dim dt2 As New DataTable
        Dim strKey As String = ""
        Dim strOldKey As String = ""

        Dim intGrpNo As Integer = 0
        Dim spdBackColor As New Drawing.Color

        Dim sngTAT1_MI As Single = 0
        Dim sngTAT2_MI As Single = 0
        Dim sngPRPTMI As Single = 0
        Dim sngFRPTMI As Single = 0
        Dim sngTAT1_MI_exp_holi As Single = 0

        Dim sTestCds As String = ""

        Try
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 데이타 조회중... -> 데이타량에 따라 다소 시간이 걸리므로 잠시만 기다려 주십시오.")

            Dim sEmerYN As String = ""

            If chkEmer.Checked And chkNotEmer.Checked Then
                sEmerYN = ""
            ElseIf chkEmer.Checked Then
                sEmerYN = "Y"
            ElseIf chkNotEmer.Checked Then
                sEmerYN = "N"
            End If

            sTestCds = Ctrl.Get_Code_Tag(Me.txtSelTest)

            If sTestCds.Length > 0 Then
                sTestCds = "'" + sTestCds.Replace(",", "','") + "'"
            End If

            Dim dt_Cmt As New DataTable
            Dim objCollTkCd As New LISAPP.APP_F_COLLTKCD

            dt_Cmt = objCollTkCd.fnGet_CollTK_Cancel_ContInfo("C")
            Dim dt As DataTable = fnGet_Tat_List(sTestCds, Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), _
                                                 IIf(Me.rdoBaseTst.Checked, "", "ORDER").ToString(), _
                                                 Me.chkOVT.Checked, Ctrl.Get_Code(Me.cboSlip), sEmerYN, Me.txtRegNo.Text, Me.chkTATCont.Checked)

            Dim sSelect As String = ""

            If Me.txtPatnm.Text <> "" Then sSelect = "patnm LIKE '" + Me.txtPatnm.Text.Trim() + "%'"

            If Me.txtRegNo.Text <> "" Then sSelect += IIf(sSelect = "", "", " AND ").ToString + " regno = '" + Me.txtRegNo.Text + "'"

            If Me.rdoIogbnI.Checked Then
                sSelect += IIf(sSelect = "", "", " AND ").ToString + "iogbn IN ('I', 'D', 'E')"
                If Me.txtDeptWard.Text <> "" Then sSelect += IIf(sSelect = "", "", " AND ").ToString + "wardno IN ('" + Me.txtDeptWard.Tag.ToString.Replace(",", "','").ToString + "')"
            ElseIf Me.rdoIogbnO.Checked Then
                sSelect += IIf(sSelect = "", "", " AND ").ToString + "iogbn NOT IN ('I', 'D', 'E')"
                If Me.txtDeptWard.Text <> "" Then sSelect += IIf(sSelect = "", "", " AND ").ToString + "deptcd IN ('" + Me.txtDeptWard.Tag.ToString.Replace(",", "','").ToString + "')"
            End If

            If Me.chkTATCont.Checked Then
                sSelect += IIf(sSelect = "", "", " AND ").ToString + "TRIM(cmtcont) <> '[]'"
            End If
            '< add freety 2007/01/23 : 정렬기준 접수일시와 검체번호로 분리
            Dim sSortBy As String = ""
            Dim a_dr() As DataRow

            If Me.rdoBaseTkDt.Checked Then
                sSortBy = "tkdt, bcno, sort_slip, sort_test, testcd"
            Else
                sSortBy = "sort_slip, slipcd, tkdt, sort_test, testcd"
            End If

            a_dr = dt.Select(sSelect, sSortBy)

            dt = Fn.ChangeToDataTable(a_dr)
            '>

            If dt.Rows.Count > 0 Then
                mbQuery = True
                pnlMainBtn.Enabled = False

                Dim bldFlag As Boolean = False

                With spdList
                    .MaxRows = 0
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
                        Application.DoEvents()

                        ' 중간 취소
                        If mbEscape = True Then Exit For
                        DS_StatusBar.setTextStatusBar(" ▷▶▷ TAT 리스트 표시중... [" & (ix + 1).ToString & "/" & dt.Rows.Count.ToString & "] ->  표시 취소는 Esc Key를 눌러 주십시오.")

                        .MaxRows += 1 : .Row = .MaxRows
                        strKey = dt.Rows(ix).Item("bcno").ToString.Trim
                        If strKey <> strOldKey Then
                            If strOldKey <> "" Then Fn.DrawBorderLineTop(spdList, .MaxRows)
                            .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString.Trim
                            .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString.Trim
                            .Col = .GetColFromID("sexage") : .Text = dt.Rows(ix).Item("sa").ToString.Trim
                            .Col = .GetColFromID("deptnm") : .Text = dt.Rows(ix).Item("deptcd").ToString.Trim
                            .Col = .GetColFromID("doctornm") : .Text = dt.Rows(ix).Item("doctornm").ToString.Trim
                            .Col = .GetColFromID("wardroom") : .Text = dt.Rows(ix).Item("ws").ToString.Trim
                            .Col = .GetColFromID("spcnmd") : .Text = dt.Rows(ix).Item("spcnmd").ToString.Trim
                            .Col = .GetColFromID("bcno") : .Text = Fn.BCNO_View(strKey, True)


                            intGrpNo += 1
                            If intGrpNo Mod 2 = 1 Then
                                spdBackColor = System.Drawing.Color.White
                            Else
                                spdBackColor = System.Drawing.Color.FromArgb(255, 251, 244)
                            End If

                            strOldKey = strKey
                        End If

                        '배경색 설정
                        .Row = ix + 1 : .Row2 = ix + 1
                        .Col = 1 : .Col2 = .MaxCols
                        .BlockMode = True
                        .BackColor = spdBackColor
                        .BlockMode = False

                        .Row = .MaxRows
                        .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim

                        .Col = .GetColFromID("statgbn")
                        If dt.Rows(ix).Item("statgbn").ToString.Trim <> "" Then  '기존 If dt.Rows(ix).Item("statgbn").ToString.Trim <> "Y" Then
                            .ForeColor = System.Drawing.Color.Red : .FontBold = True
                            .Text = "Y"
                            .set_RowHeight(.Row, 12.27)
                        Else
                            .Text = ""
                        End If

                        .Col = .GetColFromID("tkdt") : .Text = dt.Rows(ix).Item("tkdt").ToString.Trim
                        .ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                        If intGrpNo Mod 2 = 1 Then
                            .BackColor = System.Drawing.Color.FromArgb(244, 244, 244)
                        Else
                            .BackColor = System.Drawing.Color.FromArgb(238, 238, 238)
                        End If

                        .Col = .GetColFromID("colldt") : .Text = dt.Rows(ix).Item("colldt").ToString.Trim
                        .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString.Trim

                        .Col = .GetColFromID("t1") : .Text = dt.Rows(ix).Item("t1").ToString.Trim
                        .Col = .GetColFromID("t2") : .Text = dt.Rows(ix).Item("t2").ToString.Trim

                        .Col = .GetColFromID("mwdt") : .Text = dt.Rows(ix).Item("mwdt").ToString.Trim
                        .ForeColor = System.Drawing.Color.FromArgb(0, 64, 0)
                        If intGrpNo Mod 2 = 1 Then
                            .BackColor = System.Drawing.Color.FromArgb(234, 255, 234)
                        Else
                            .BackColor = System.Drawing.Color.FromArgb(234, 249, 228)
                        End If

                        .Col = .GetColFromID("fndt") : .Text = dt.Rows(ix).Item("fndt").ToString.Trim
                        .ForeColor = System.Drawing.Color.FromArgb(0, 0, 94)
                        If intGrpNo Mod 2 = 1 Then
                            .BackColor = System.Drawing.Color.FromArgb(234, 234, 255)
                        Else
                            .BackColor = System.Drawing.Color.FromArgb(234, 228, 249)
                        End If

                        sngTAT1_MI = CSng(IIf(dt.Rows(ix).Item("tat1_mi").ToString.Trim = "", 0, dt.Rows(ix).Item("tat1_mi").ToString.Trim))
                        sngTAT2_MI = CSng(IIf(dt.Rows(ix).Item("tat2_mi").ToString.Trim = "", 0, dt.Rows(ix).Item("tat2_mi").ToString.Trim))
                        sngPRPTMI = CSng(IIf(dt.Rows(ix).Item("prptmi").ToString.Trim = "", 0, dt.Rows(ix).Item("prptmi").ToString.Trim))
                        sngFRPTMI = CSng(IIf(dt.Rows(ix).Item("frptmi").ToString.Trim = "", 0, dt.Rows(ix).Item("frptmi").ToString.Trim))
                        '20210210 jhs 휴일제외 tat추가
                        'sngTAT1_MI_exp_holi = CSng(IIf(dt.Rows(ix).Item("tat1_mi_exp_holi").ToString.Trim = "", 0, dt.Rows(ix).Item("tat1_mi_exp_holi").ToString.Trim))
                        '.Col = .GetColFromID("tat1_mi_exp_holi") : .Text = sngTAT1_MI_exp_holi.ToString
                        .Col = .GetColFromID("tat1_mi_exp_holi") : .Text = dt.Rows(ix).Item("tat1_mi_exp_holi").ToString.Trim
                        '-------------------------------------------------

                        .Col = .GetColFromID("tat1") : .Text = dt.Rows(ix).Item("tat1").ToString.Trim
                        '<<<20170511 TAT시간에 걸린것 이 소수점이 있을경우 오버타임으로 계산되는것 막기 위해 소수점은버림 추가 
                        If Math.Truncate(sngTAT1_MI) > sngPRPTMI And sngPRPTMI > 0 Then
                            .ForeColor = System.Drawing.Color.Red
                            .BackColor = System.Drawing.Color.FromArgb(255, 202, 202)

                            .Col = .GetColFromID("ovt1") : .Text = (Fix(sngTAT1_MI - sngPRPTMI)).ToString
                        End If

                        .Col = .GetColFromID("tat3") : .Text = dt.Rows(ix).Item("tat3").ToString.Trim
                        '<<<20170511 TAT시간에 걸린것 이 소수점이 있을경우 오버타임으로 계산되는것 막기 위해 소수점은버림 추가 
                        If Math.Truncate(sngTAT2_MI) > sngFRPTMI And sngFRPTMI > 0 Then
                            .ForeColor = System.Drawing.Color.Red
                            .BackColor = System.Drawing.Color.FromArgb(255, 202, 202)

                            .Col = .GetColFromID("ovt2") : .Text = (Fix(sngTAT2_MI - sngFRPTMI)).ToString
                        End If

                        '< yjlee 2009-03-10
                        .Col = .GetColFromID("tat2") : .Text = dt.Rows(ix).Item("tat2").ToString.Trim
                        .Col = .GetColFromID("total") : .Text = dt.Rows(ix).Item("tot").ToString.Trim

                        '< add yjlee 2009-03-27
                        'dt_Cmt
                        .Col = .GetColFromID("tatcont")
                        '.CellType = FPSpreadADO.CellTypeConstants.CellTypeComboBox
                        If dt_Cmt.Rows.Count > 0 Then
                            Dim sCmt As String = "".PadLeft(6, " "c) + Chr(9)

                            For iCnt As Integer = 0 To dt_Cmt.Rows.Count - 1

                                sCmt += "[" + dt_Cmt.Rows(iCnt).Item("cmtcd").ToString().Trim() + "]" + dt_Cmt.Rows(iCnt).Item("cmtcont").ToString().Trim() + Chr(9)
                            Next

                            .TypeComboBoxList = sCmt
                        End If
                        '.TypeComboBoxIndex = 1
                        .Text = dt.Rows(ix).Item("cmtcont").ToString.Trim

                        .Col = .GetColFromID("testcd") : .Text = dt.Rows(ix).Item("testcd").ToString.Trim
                        .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString.Trim
                        .Col = .GetColFromID("rstnm") : .Text = dt.Rows(ix).Item("rstnm").ToString.Trim

                        '> yjlee 2009-03-27  

                        .Col = .GetColFromID("bcno") : .Text = Fn.BCNO_View(strKey, True)

                        '<<< 20170412 기준 TAT 표시 추가 
                        'Dim sPrptmi As String = ""
                        'Dim sFrptmi As String = ""
                        'Dim dHours As Double = 0.0
                        'Dim dMin As Double = 0.0
                        'Dim dSec As Double = 0.0

                        'dHours = CDbl(sPrptmi) / 3600
                        'dMin = CDbl(sPrptmi) Mod 3600 / 60
                        'dSec = CDbl(sPrptmi) Mod 3600 Mod 60

                        '.Col = .GetColFromID("prptmi2") : .Text = dt.Rows(ix).Item("prptmi").ToString.Trim
                        '.Col = .GetColFromID("frptmi2") : .Text = dt.Rows(ix).Item("frptmi").ToString.Trim

                        '-- Hidden Field
                        .Col = .GetColFromID("tat1_mi") : .Text = sngTAT1_MI.ToString
                        .Col = .GetColFromID("tat2_mi") : .Text = sngTAT2_MI.ToString
                        .Col = .GetColFromID("prptmi") : .Text = sngPRPTMI.ToString
                        .Col = .GetColFromID("frptmi") : .Text = sngFRPTMI.ToString
                        .Col = .GetColFromID("partcd") : .Text = dt.Rows(ix).Item("slipcd").ToString.Trim

                    Next
                End With

                sbDisplay_St()

                Debug.WriteLine(dt2.Rows.Count)

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

    Private Sub sbDisplay_St()

        Try
            Dim a_sDMY As String() = Nothing
            Dim iDMYDiff As Integer

            iDMYDiff = CInt(DateDiff(DateInterval.Day, CDate(Me.dtpDate0.Value), CDate(Me.dtpDate1.Value)))

            ReDim a_sDMY(iDMYDiff)

            For i As Integer = 1 To iDMYDiff + 1
                a_sDMY(i - 1) = DateAdd(DateInterval.Day, i - 1, CDate(Me.dtpDate0.Value)).ToShortDateString
            Next

            With Me.spdStList
                .ReDraw = False
                .MaxRows = 0
                '코드, 사유, Total
                .MaxCols = a_sDMY.Length + 3

                For i As Integer = 0 To a_sDMY.Length - 1
                    .Row = 0
                    .Col = .GetColFromID("total") + i + 1 : .Text = a_sDMY(i) : .ColID = .Text
                Next

                Dim sEmerYN As String = ""

                If chkEmer.Checked And chkNotEmer.Checked Then
                    sEmerYN = ""
                ElseIf chkEmer.Checked Then
                    sEmerYN = "Y"
                ElseIf chkNotEmer.Checked Then
                    sEmerYN = "N"
                End If

                '<20130212 TAT소견 응급구분 ,오버타임 가져가기위해 수정
                'Dim dt As DataTable = fnGet_TatCont_St(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), Ctrl.Get_Code(cboSlip))
                Dim dt As DataTable = fnGet_TatCont_St2(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), sEmerYN, Me.chkOVT.Checked, Ctrl.Get_Code(cboSlip))
                Dim sCmtCd As String = ""
                Dim sCmtCont As String = ""

                If dt.Rows.Count < 1 Then Return

                For ix As Integer = 0 To dt.Rows.Count - 1

                    If sCmtCd <> dt.Rows(ix).Item("cmtcd").ToString Then

                        If sCmtCd <> "" Then
                            .Row = .MaxRows
                            .Col = .GetColFromID("cmtcd") : .Text = sCmtCd
                            .Col = .GetColFromID("cmtcont") : .Text = sCmtCont
                        End If

                        .MaxRows += 1
                        .Row = .MaxRows

                    End If

                    Dim iCol = .GetColFromID(dt.Rows(ix).Item("regdt").ToString)

                    If iCol > 0 Then
                        .Row = .MaxRows
                        .Col = iCol : .Text = Format(dt.Rows(ix).Item("cnt"), "#,##0").ToString
                    End If

                    sCmtCd = dt.Rows(ix).Item("cmtcd").ToString
                    sCmtCont = dt.Rows(ix).Item("cmtcont").ToString
                Next

                If sCmtCd <> "" Then
                    .Row = .MaxRows
                    .Col = .GetColFromID("cmtcd") : .Text = sCmtCd
                    .Col = .GetColFromID("cmtcont") : .Text = sCmtCont
                End If

                For iRow As Integer = 1 To .MaxRows

                    Dim lgTotal As Long = 0
                    For iCol As Integer = .GetColFromID("total") + 1 To .MaxCols
                        .Row = iRow
                        .Col = iCol : lgTotal += CType(IIf(.Text = "", 0, .Text.Replace(",", "")), Long)
                    Next

                    .Row = iRow : .Col = .GetColFromID("total") : .Text = Format(lgTotal, "#,##0").ToString
                Next

                .MaxRows += 1

                .Row = .MaxRows : .Col = .GetColFromID("cmtcont") : .Text = "합 계"

                For iCol As Integer = .GetColFromID("total") To .MaxCols

                    Dim lgTotal As Long = 0

                    For iRow As Integer = 1 To .MaxRows - 1
                        .Row = iRow
                        .Col = iCol : lgTotal += CType(IIf(.Text = "", 0, .Text.Replace(",", "")), Long)
                    Next

                    .Row = .MaxRows : .Col = iCol : .Text = Format(lgTotal, "#,##0").ToString
                Next


                .ReDraw = True
            End With
        Catch ex As Exception
            Throw (New Exception(ex.Message))
        Finally
            spdStList.ReDraw = True
        End Try

    End Sub
#End Region

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkOVT As System.Windows.Forms.CheckBox
    Friend WithEvents pnlView As System.Windows.Forms.Panel
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents pnlMainBtn As System.Windows.Forms.Panel
    'Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkEmer As System.Windows.Forms.CheckBox
    Friend WithEvents chkNotEmer As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS02))
        Dim DesignerRectTracker13 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems7 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker14 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker15 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems8 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker16 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnWcmt = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboCmt = New System.Windows.Forms.ComboBox()
        Me.chkTATCont = New System.Windows.Forms.CheckBox()
        Me.btnCdHelp_dept = New System.Windows.Forms.Button()
        Me.btnCdHelp_test = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRegNo = New System.Windows.Forms.TextBox()
        Me.rdoIogbnI = New System.Windows.Forms.RadioButton()
        Me.rdoIogbnO = New System.Windows.Forms.RadioButton()
        Me.rdoIogbnA = New System.Windows.Forms.RadioButton()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.txtDeptWard = New System.Windows.Forms.TextBox()
        Me.lblIOGbn = New System.Windows.Forms.Label()
        Me.txtPatnm = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnClear_Tcls = New System.Windows.Forms.Button()
        Me.cboSlip = New System.Windows.Forms.ComboBox()
        Me.txtSelTest = New System.Windows.Forms.TextBox()
        Me.lblTest = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkNotEmer = New System.Windows.Forms.CheckBox()
        Me.chkEmer = New System.Windows.Forms.CheckBox()
        Me.chkOVT = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker()
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdStList = New AxFPSpreadADO.AxfpSpread()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.pnlMainBtn = New System.Windows.Forms.Panel()
        Me.btnCmtDel = New CButtonLib.CButton()
        Me.cboPrint = New System.Windows.Forms.ComboBox()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnQuery = New CButtonLib.CButton()
        Me.btnPrint = New CButtonLib.CButton()
        Me.btnExcel = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.chkEditColumn = New System.Windows.Forms.CheckBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblUserId = New System.Windows.Forms.Label()
        Me.pnlView = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rdoBaseOrd = New System.Windows.Forms.RadioButton()
        Me.rdoBaseTst = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdoBaseTSect = New System.Windows.Forms.RadioButton()
        Me.rdoBaseTkDt = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdStList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMainBtn.SuspendLayout()
        Me.pnlView.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.btnWcmt)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.cboCmt)
        Me.GroupBox1.Controls.Add(Me.chkTATCont)
        Me.GroupBox1.Controls.Add(Me.btnCdHelp_dept)
        Me.GroupBox1.Controls.Add(Me.btnCdHelp_test)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtRegNo)
        Me.GroupBox1.Controls.Add(Me.rdoIogbnI)
        Me.GroupBox1.Controls.Add(Me.rdoIogbnO)
        Me.GroupBox1.Controls.Add(Me.rdoIogbnA)
        Me.GroupBox1.Controls.Add(Me.btnDel)
        Me.GroupBox1.Controls.Add(Me.txtDeptWard)
        Me.GroupBox1.Controls.Add(Me.lblIOGbn)
        Me.GroupBox1.Controls.Add(Me.txtPatnm)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnClear_Tcls)
        Me.GroupBox1.Controls.Add(Me.cboSlip)
        Me.GroupBox1.Controls.Add(Me.txtSelTest)
        Me.GroupBox1.Controls.Add(Me.lblTest)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpDate1)
        Me.GroupBox1.Controls.Add(Me.dtpDate0)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(4, -2)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(957, 89)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(537, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(527, 12)
        Me.Label9.TabIndex = 193
        Me.Label9.Text = "※일괄적용 시 체크된 항목에 대해 사유가 입력됩니다.이후 저장버튼을 누르셔야 저장됩니다."
        '
        'btnWcmt
        '
        Me.btnWcmt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWcmt.Location = New System.Drawing.Point(448, 62)
        Me.btnWcmt.Margin = New System.Windows.Forms.Padding(0)
        Me.btnWcmt.Name = "btnWcmt"
        Me.btnWcmt.Size = New System.Drawing.Size(88, 21)
        Me.btnWcmt.TabIndex = 183
        Me.btnWcmt.Text = "사유일괄적용"
        Me.btnWcmt.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(212, 62)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 21)
        Me.Label7.TabIndex = 192
        Me.Label7.Text = "TAT 사유"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCmt
        '
        Me.cboCmt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCmt.ItemHeight = 12
        Me.cboCmt.Location = New System.Drawing.Point(284, 62)
        Me.cboCmt.Margin = New System.Windows.Forms.Padding(1)
        Me.cboCmt.Name = "cboCmt"
        Me.cboCmt.Size = New System.Drawing.Size(163, 20)
        Me.cboCmt.TabIndex = 191
        '
        'chkTATCont
        '
        Me.chkTATCont.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkTATCont.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkTATCont.ForeColor = System.Drawing.Color.Black
        Me.chkTATCont.Location = New System.Drawing.Point(167, 63)
        Me.chkTATCont.Name = "chkTATCont"
        Me.chkTATCont.Size = New System.Drawing.Size(55, 19)
        Me.chkTATCont.TabIndex = 190
        Me.chkTATCont.Text = "사유"
        '
        'btnCdHelp_dept
        '
        Me.btnCdHelp_dept.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_dept.Image = CType(resources.GetObject("btnCdHelp_dept.Image"), System.Drawing.Image)
        Me.btnCdHelp_dept.Location = New System.Drawing.Point(547, 62)
        Me.btnCdHelp_dept.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_dept.Name = "btnCdHelp_dept"
        Me.btnCdHelp_dept.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_dept.TabIndex = 189
        Me.btnCdHelp_dept.UseVisualStyleBackColor = True
        Me.btnCdHelp_dept.Visible = False
        '
        'btnCdHelp_test
        '
        Me.btnCdHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_test.Image = CType(resources.GetObject("btnCdHelp_test.Image"), System.Drawing.Image)
        Me.btnCdHelp_test.Location = New System.Drawing.Point(75, 38)
        Me.btnCdHelp_test.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_test.Name = "btnCdHelp_test"
        Me.btnCdHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_test.TabIndex = 188
        Me.btnCdHelp_test.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(824, 61)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 21)
        Me.Label6.TabIndex = 187
        Me.Label6.Tag = ""
        Me.Label6.Text = "성명"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRegNo
        '
        Me.txtRegNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Location = New System.Drawing.Point(756, 61)
        Me.txtRegNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(63, 21)
        Me.txtRegNo.TabIndex = 186
        Me.txtRegNo.WordWrap = False
        '
        'rdoIogbnI
        '
        Me.rdoIogbnI.AutoSize = True
        Me.rdoIogbnI.Location = New System.Drawing.Point(114, 66)
        Me.rdoIogbnI.Name = "rdoIogbnI"
        Me.rdoIogbnI.Size = New System.Drawing.Size(47, 16)
        Me.rdoIogbnI.TabIndex = 184
        Me.rdoIogbnI.Text = "입원"
        Me.rdoIogbnI.UseVisualStyleBackColor = True
        '
        'rdoIogbnO
        '
        Me.rdoIogbnO.AutoSize = True
        Me.rdoIogbnO.Location = New System.Drawing.Point(61, 66)
        Me.rdoIogbnO.Name = "rdoIogbnO"
        Me.rdoIogbnO.Size = New System.Drawing.Size(47, 16)
        Me.rdoIogbnO.TabIndex = 183
        Me.rdoIogbnO.Text = "외래"
        Me.rdoIogbnO.UseVisualStyleBackColor = True
        '
        'rdoIogbnA
        '
        Me.rdoIogbnA.AutoSize = True
        Me.rdoIogbnA.Checked = True
        Me.rdoIogbnA.Location = New System.Drawing.Point(8, 66)
        Me.rdoIogbnA.Name = "rdoIogbnA"
        Me.rdoIogbnA.Size = New System.Drawing.Size(47, 16)
        Me.rdoIogbnA.TabIndex = 185
        Me.rdoIogbnA.TabStop = True
        Me.rdoIogbnA.Text = "전체"
        Me.rdoIogbnA.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDel.Location = New System.Drawing.Point(626, 61)
        Me.btnDel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(50, 21)
        Me.btnDel.TabIndex = 182
        Me.btnDel.Text = "clear"
        Me.btnDel.UseVisualStyleBackColor = True
        Me.btnDel.Visible = False
        '
        'txtDeptWard
        '
        Me.txtDeptWard.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDeptWard.BackColor = System.Drawing.Color.Thistle
        Me.txtDeptWard.ForeColor = System.Drawing.Color.Brown
        Me.txtDeptWard.Location = New System.Drawing.Point(573, 61)
        Me.txtDeptWard.Margin = New System.Windows.Forms.Padding(0)
        Me.txtDeptWard.Name = "txtDeptWard"
        Me.txtDeptWard.ReadOnly = True
        Me.txtDeptWard.Size = New System.Drawing.Size(52, 21)
        Me.txtDeptWard.TabIndex = 180
        Me.txtDeptWard.Visible = False
        '
        'lblIOGbn
        '
        Me.lblIOGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblIOGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIOGbn.ForeColor = System.Drawing.Color.Black
        Me.lblIOGbn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblIOGbn.Location = New System.Drawing.Point(477, 62)
        Me.lblIOGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblIOGbn.Name = "lblIOGbn"
        Me.lblIOGbn.Size = New System.Drawing.Size(69, 21)
        Me.lblIOGbn.TabIndex = 176
        Me.lblIOGbn.Tag = ""
        Me.lblIOGbn.Text = "진료과"
        Me.lblIOGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblIOGbn.Visible = False
        '
        'txtPatnm
        '
        Me.txtPatnm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPatnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatnm.Location = New System.Drawing.Point(894, 61)
        Me.txtPatnm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPatnm.Name = "txtPatnm"
        Me.txtPatnm.Size = New System.Drawing.Size(56, 21)
        Me.txtPatnm.TabIndex = 174
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(686, 61)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 21)
        Me.Label4.TabIndex = 173
        Me.Label4.Tag = ""
        Me.Label4.Text = "등록번호"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClear_Tcls
        '
        Me.btnClear_Tcls.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear_Tcls.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_Tcls.Location = New System.Drawing.Point(900, 38)
        Me.btnClear_Tcls.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_Tcls.Name = "btnClear_Tcls"
        Me.btnClear_Tcls.Size = New System.Drawing.Size(50, 21)
        Me.btnClear_Tcls.TabIndex = 172
        Me.btnClear_Tcls.Text = "clear"
        Me.btnClear_Tcls.UseVisualStyleBackColor = True
        '
        'cboSlip
        '
        Me.cboSlip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.ItemHeight = 12
        Me.cboSlip.Items.AddRange(New Object() {"검사항목별 - 작업그룹"})
        Me.cboSlip.Location = New System.Drawing.Point(355, 15)
        Me.cboSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(384, 20)
        Me.cboSlip.TabIndex = 171
        '
        'txtSelTest
        '
        Me.txtSelTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSelTest.BackColor = System.Drawing.Color.Thistle
        Me.txtSelTest.ForeColor = System.Drawing.Color.Brown
        Me.txtSelTest.Location = New System.Drawing.Point(102, 38)
        Me.txtSelTest.Margin = New System.Windows.Forms.Padding(0)
        Me.txtSelTest.Name = "txtSelTest"
        Me.txtSelTest.ReadOnly = True
        Me.txtSelTest.Size = New System.Drawing.Size(796, 21)
        Me.txtSelTest.TabIndex = 169
        '
        'lblTest
        '
        Me.lblTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest.ForeColor = System.Drawing.Color.White
        Me.lblTest.Location = New System.Drawing.Point(5, 38)
        Me.lblTest.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(69, 21)
        Me.lblTest.TabIndex = 167
        Me.lblTest.Text = "검사항목"
        Me.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel2.Controls.Add(Me.chkNotEmer)
        Me.Panel2.Controls.Add(Me.chkEmer)
        Me.Panel2.Controls.Add(Me.chkOVT)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Location = New System.Drawing.Point(752, 14)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(199, 21)
        Me.Panel2.TabIndex = 90
        '
        'chkNotEmer
        '
        Me.chkNotEmer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkNotEmer.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkNotEmer.ForeColor = System.Drawing.Color.Blue
        Me.chkNotEmer.Location = New System.Drawing.Point(96, 2)
        Me.chkNotEmer.Name = "chkNotEmer"
        Me.chkNotEmer.Size = New System.Drawing.Size(49, 19)
        Me.chkNotEmer.TabIndex = 3
        Me.chkNotEmer.Text = "일반"
        '
        'chkEmer
        '
        Me.chkEmer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkEmer.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkEmer.ForeColor = System.Drawing.Color.Blue
        Me.chkEmer.Location = New System.Drawing.Point(149, 2)
        Me.chkEmer.Name = "chkEmer"
        Me.chkEmer.Size = New System.Drawing.Size(52, 19)
        Me.chkEmer.TabIndex = 2
        Me.chkEmer.Text = "응급"
        '
        'chkOVT
        '
        Me.chkOVT.Checked = True
        Me.chkOVT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOVT.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkOVT.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkOVT.ForeColor = System.Drawing.Color.Firebrick
        Me.chkOVT.Location = New System.Drawing.Point(6, 3)
        Me.chkOVT.Name = "chkOVT"
        Me.chkOVT.Size = New System.Drawing.Size(87, 19)
        Me.chkOVT.TabIndex = 1
        Me.chkOVT.Text = "OverTime Item"
        '
        'Label8
        '
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label8.Location = New System.Drawing.Point(0, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(199, 21)
        Me.Label8.TabIndex = 0
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label13.Location = New System.Drawing.Point(284, 15)
        Me.Label13.Margin = New System.Windows.Forms.Padding(1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(70, 21)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "검사분야"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(168, 19)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "~"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(182, 15)
        Me.dtpDate1.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(90, 21)
        Me.dtpDate1.TabIndex = 1
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(75, 15)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(90, 21)
        Me.dtpDate0.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(5, 15)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(69, 21)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "접수일자"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.spdStList)
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(4, 91)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1393, 534)
        Me.Panel1.TabIndex = 1
        '
        'spdStList
        '
        Me.spdStList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdStList.DataSource = Nothing
        Me.spdStList.Location = New System.Drawing.Point(0, 391)
        Me.spdStList.Name = "spdStList"
        Me.spdStList.OcxState = CType(resources.GetObject("spdStList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStList.Size = New System.Drawing.Size(1393, 141)
        Me.spdStList.TabIndex = 1
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1391, 387)
        Me.spdList.TabIndex = 0
        '
        'pnlMainBtn
        '
        Me.pnlMainBtn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlMainBtn.Controls.Add(Me.btnCmtDel)
        Me.pnlMainBtn.Controls.Add(Me.cboPrint)
        Me.pnlMainBtn.Controls.Add(Me.btnReg)
        Me.pnlMainBtn.Controls.Add(Me.btnQuery)
        Me.pnlMainBtn.Controls.Add(Me.btnPrint)
        Me.pnlMainBtn.Controls.Add(Me.btnExcel)
        Me.pnlMainBtn.Controls.Add(Me.btnClear)
        Me.pnlMainBtn.Controls.Add(Me.btnExit)
        Me.pnlMainBtn.Controls.Add(Me.chkEditColumn)
        Me.pnlMainBtn.Controls.Add(Me.lblUserNm)
        Me.pnlMainBtn.Controls.Add(Me.lblUserId)
        Me.pnlMainBtn.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlMainBtn.Location = New System.Drawing.Point(0, 631)
        Me.pnlMainBtn.Name = "pnlMainBtn"
        Me.pnlMainBtn.Size = New System.Drawing.Size(1401, 34)
        Me.pnlMainBtn.TabIndex = 2
        '
        'btnCmtDel
        '
        Me.btnCmtDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker13.IsActive = False
        DesignerRectTracker13.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker13.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCmtDel.CenterPtTracker = DesignerRectTracker13
        CBlendItems7.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems7.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnCmtDel.ColorFillBlend = CBlendItems7
        Me.btnCmtDel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnCmtDel.Corners.All = CType(6, Short)
        Me.btnCmtDel.Corners.LowerLeft = CType(6, Short)
        Me.btnCmtDel.Corners.LowerRight = CType(6, Short)
        Me.btnCmtDel.Corners.UpperLeft = CType(6, Short)
        Me.btnCmtDel.Corners.UpperRight = CType(6, Short)
        Me.btnCmtDel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnCmtDel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnCmtDel.FocalPoints.CenterPtX = 0.5416667!
        Me.btnCmtDel.FocalPoints.CenterPtY = 0.16!
        Me.btnCmtDel.FocalPoints.FocusPtX = 0.0!
        Me.btnCmtDel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker14.IsActive = False
        DesignerRectTracker14.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker14.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCmtDel.FocusPtTracker = DesignerRectTracker14
        Me.btnCmtDel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCmtDel.ForeColor = System.Drawing.Color.White
        Me.btnCmtDel.Image = Nothing
        Me.btnCmtDel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCmtDel.ImageIndex = 0
        Me.btnCmtDel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnCmtDel.Location = New System.Drawing.Point(794, 3)
        Me.btnCmtDel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCmtDel.Name = "btnCmtDel"
        Me.btnCmtDel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnCmtDel.SideImage = Nothing
        Me.btnCmtDel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnCmtDel.Size = New System.Drawing.Size(100, 25)
        Me.btnCmtDel.TabIndex = 205
        Me.btnCmtDel.Text = "TAT사유 삭제"
        Me.btnCmtDel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnCmtDel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'cboPrint
        '
        Me.cboPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.Items.AddRange(New Object() {"위", "아래"})
        Me.cboPrint.Location = New System.Drawing.Point(640, 6)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(48, 20)
        Me.cboPrint.TabIndex = 204
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker15.IsActive = False
        DesignerRectTracker15.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker15.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker15
        CBlendItems8.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems8.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems8
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5416667!
        Me.btnReg.FocalPoints.CenterPtY = 0.16!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker16.IsActive = True
        DesignerRectTracker16.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker16.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker16
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(692, 3)
        Me.btnReg.Margin = New System.Windows.Forms.Padding(0)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(100, 25)
        Me.btnReg.TabIndex = 203
        Me.btnReg.Text = "TAT사유 저장"
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems1
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.5416667!
        Me.btnQuery.FocalPoints.CenterPtY = 0.16!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(896, 3)
        Me.btnQuery.Margin = New System.Windows.Forms.Padding(0)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(96, 25)
        Me.btnQuery.TabIndex = 202
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems2
        Me.btnPrint.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint.Corners.All = CType(6, Short)
        Me.btnPrint.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint.Corners.LowerRight = CType(6, Short)
        Me.btnPrint.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint.Corners.UpperRight = CType(6, Short)
        Me.btnPrint.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint.FocalPoints.CenterPtX = 0.5!
        Me.btnPrint.FocalPoints.CenterPtY = 0.0!
        Me.btnPrint.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker4
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(994, 3)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(96, 25)
        Me.btnPrint.TabIndex = 201
        Me.btnPrint.Text = "출력"
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems3
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker6
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(1092, 3)
        Me.btnExcel.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(100, 25)
        Me.btnExcel.TabIndex = 200
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems4
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
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker8
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1194, 3)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 199
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems5
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker10
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1296, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(97, 25)
        Me.btnExit.TabIndex = 198
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chkEditColumn
        '
        Me.chkEditColumn.AutoSize = True
        Me.chkEditColumn.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkEditColumn.Location = New System.Drawing.Point(5, 12)
        Me.chkEditColumn.Name = "chkEditColumn"
        Me.chkEditColumn.Size = New System.Drawing.Size(90, 15)
        Me.chkEditColumn.TabIndex = 89
        Me.chkEditColumn.Text = "컬럼이동모드"
        Me.chkEditColumn.UseVisualStyleBackColor = True
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(80, 8)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 6
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(8, 8)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 5
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'pnlView
        '
        Me.pnlView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlView.Controls.Add(Me.PictureBox1)
        Me.pnlView.Controls.Add(Me.Label18)
        Me.pnlView.Location = New System.Drawing.Point(961, 22)
        Me.pnlView.Name = "pnlView"
        Me.pnlView.Size = New System.Drawing.Size(437, 70)
        Me.pnlView.TabIndex = 85
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(3, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(431, 57)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label18
        '
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label18.Location = New System.Drawing.Point(0, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(437, 70)
        Me.Label18.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.Controls.Add(Me.Label3)
        Me.Panel6.Controls.Add(Me.Panel3)
        Me.Panel6.Controls.Add(Me.Label1)
        Me.Panel6.Controls.Add(Me.Panel5)
        Me.Panel6.Location = New System.Drawing.Point(964, 5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(431, 24)
        Me.Panel6.TabIndex = 97
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(193, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 22)
        Me.Label3.TabIndex = 99
        Me.Label3.Text = "정렬기준"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Thistle
        Me.Panel3.Controls.Add(Me.rdoBaseOrd)
        Me.Panel3.Controls.Add(Me.rdoBaseTst)
        Me.Panel3.Location = New System.Drawing.Point(76, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(115, 22)
        Me.Panel3.TabIndex = 96
        '
        'rdoBaseOrd
        '
        Me.rdoBaseOrd.BackColor = System.Drawing.Color.Transparent
        Me.rdoBaseOrd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBaseOrd.Location = New System.Drawing.Point(62, 2)
        Me.rdoBaseOrd.Name = "rdoBaseOrd"
        Me.rdoBaseOrd.Size = New System.Drawing.Size(47, 18)
        Me.rdoBaseOrd.TabIndex = 1
        Me.rdoBaseOrd.Text = "처방"
        Me.rdoBaseOrd.UseVisualStyleBackColor = False
        '
        'rdoBaseTst
        '
        Me.rdoBaseTst.BackColor = System.Drawing.Color.Transparent
        Me.rdoBaseTst.Checked = True
        Me.rdoBaseTst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBaseTst.Location = New System.Drawing.Point(5, 2)
        Me.rdoBaseTst.Name = "rdoBaseTst"
        Me.rdoBaseTst.Size = New System.Drawing.Size(47, 18)
        Me.rdoBaseTst.TabIndex = 0
        Me.rdoBaseTst.TabStop = True
        Me.rdoBaseTst.Text = "검사"
        Me.rdoBaseTst.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(1, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 22)
        Me.Label1.TabIndex = 97
        Me.Label1.Text = "항목기준"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Thistle
        Me.Panel5.Controls.Add(Me.rdoBaseTSect)
        Me.Panel5.Controls.Add(Me.rdoBaseTkDt)
        Me.Panel5.Location = New System.Drawing.Point(268, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(162, 22)
        Me.Panel5.TabIndex = 98
        '
        'rdoBaseTSect
        '
        Me.rdoBaseTSect.BackColor = System.Drawing.Color.Transparent
        Me.rdoBaseTSect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBaseTSect.Location = New System.Drawing.Point(83, 2)
        Me.rdoBaseTSect.Name = "rdoBaseTSect"
        Me.rdoBaseTSect.Size = New System.Drawing.Size(73, 18)
        Me.rdoBaseTSect.TabIndex = 1
        Me.rdoBaseTSect.Text = "검사분야"
        Me.rdoBaseTSect.UseVisualStyleBackColor = False
        '
        'rdoBaseTkDt
        '
        Me.rdoBaseTkDt.BackColor = System.Drawing.Color.Transparent
        Me.rdoBaseTkDt.Checked = True
        Me.rdoBaseTkDt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBaseTkDt.Location = New System.Drawing.Point(5, 2)
        Me.rdoBaseTkDt.Name = "rdoBaseTkDt"
        Me.rdoBaseTkDt.Size = New System.Drawing.Size(77, 18)
        Me.rdoBaseTkDt.TabIndex = 0
        Me.rdoBaseTkDt.TabStop = True
        Me.rdoBaseTkDt.Text = "접수일시"
        Me.rdoBaseTkDt.UseVisualStyleBackColor = False
        '
        'FGS02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1401, 665)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.pnlView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pnlMainBtn)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS02"
        Me.Text = "TurnAroundTime 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdStList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMainBtn.ResumeLayout(False)
        Me.pnlMainBtn.PerformLayout()
        Me.pnlView.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Spread 보기기/숨김 "
    Private Sub Form_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick

        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        sbSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If
    End Sub
#End Region

#Region " 메인 버튼 처리 "

    Private Sub FGS02_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try
            '< add yjlee 2009-03-30 
            spdList.AllowColMove = False
            spdList.MaxRows = 0
            spdList.SaveToFile(msSpdForm, False)
            '> add yjlee 2009-03-30 

            COMMON.CommXML.setOneElementXML(msXmlDir, msFile_OverYn, "YN", IIf(chkOVT.Checked, "T", "F").ToString())

            MdiTabControl.sbTabPageMove(Me)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
    ' Function Key정의
    Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If e.KeyCode = Keys.F4 Then
            ' 화면정리
            btnClear_Click(Nothing, Nothing)

            'ElseIf e.KeyCode = Keys.F5 Then
            '    btnQuery_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.Escape Then
            If mbQuery = False Then Me.Close()
        End If

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Try
            If mbQuery = False Then sbFormClear(0)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Me.spdList.MaxRows = 0
            Me.spdStList.MaxRows = 0

            If mbQuery = False Then sbQuery()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
        End Try

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mbQuery Then Return

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If mbQuery = False Then Me.Close()
    End Sub
#End Region



#Region " Control Event 처리 "

    Private Sub spdList_TextTipFetch(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles spdList.TextTipFetch
        Dim objSpd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

        e.multiLine = 0
        With objSpd
            .SetTextTipAppearance("굴림체", 9, False, False, &HDFFFFF&, &H800000)
            e.showTip = True

            Select Case e.col
                Case .GetColFromID("TAT1")
                    .Col = .GetColFromID("OVT1") : .Row = e.row
                    If .Text = "" Then e.showTip = False : Exit Sub
                    e.tipText = "중간보고가 " & .Text & "분 초과 되었습니다."

                Case .GetColFromID("TAT2")
                    .Col = .GetColFromID("OVT2") : .Row = e.row
                    If .Text = "" Then e.showTip = False : Exit Sub
                    e.tipText = "최종보고가 " & .Text & "분 초과 되었습니다."

                Case Else
                    e.showTip = False

            End Select

        End With
    End Sub

#End Region

    '< yjlee 2009-03-11 add 
    Private Sub FGS02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo
        DisplayInit()
        cboPrint.SelectedIndex = 0

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub DisplayInit()

        Try
            Dim dt_cmt As DataTable = New DataTable
            Dim objCollTkCd As New LISAPP.APP_F_COLLTKCD
            Display_Last()
            Me.spdList.MaxRows = 0

            dt_cmt = objCollTkCd.fnGet_CollTK_Cancel_ContInfo("C")

            If dt_cmt.Rows.Count > 0 Then
                Dim sCmt As String = "".PadLeft(6, " "c) + Chr(9)

                For iCnt As Integer = 0 To dt_cmt.Rows.Count - 1

                    'sCmt += "[" + dt_cmt.Rows(iCnt).Item("cmtcd").ToString().Trim() + "]" + dt_cmt.Rows(iCnt).Item("cmtcont").ToString().Trim() + Chr(9)
                    Me.cboCmt.Items.Add("[" + dt_cmt.Rows(iCnt).Item("cmtcd").ToString().Trim() + "]" + dt_cmt.Rows(iCnt).Item("cmtcont").ToString().Trim())
                Next


            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Public Overridable Sub Display_Last()

        Try

            Dim sTmp As String = ""

            '-- 분야
            sTmp = COMMON.CommXML.getOneElementXML(msXmlDir, msFile_Slip, "SLIP")
            If sTmp <> "" Then cboSlip.SelectedIndex = CInt(sTmp)

            '-- Over Time
            sTmp = COMMON.CommXML.getOneElementXML(msXmlDir, msFile_OverYn, "YN")
            chkOVT.Checked = CBool(IIf(sTmp = "F", False, True))

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub cboSlip_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        COMMON.CommXML.setOneElementXML(msXmlDir, msFile_Slip, "SLIP", Me.cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub chkEditColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditColumn.Click

        Me.spdList.AllowColMove = chkEditColumn.Checked

    End Sub

    Private Sub spdList_ComboSelChange(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ComboSelChangeEvent) Handles spdList.ComboSelChange
        With spdList
            Dim sTmpCont As String = ""
            If .ActiveCol = .GetColFromID("tatcont") Then
                .Col = .GetColFromID("chk")
                .Row = .ActiveRow
                .Text = "1"
            End If
        End With

    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        Try
            Dim sErrMsg As String = ""

            With spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk")

                    If .Text = "1" Then
                        Dim tat_cmt_info As New COMMON.SVar.STU_TATCmtInfo
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : Dim sTclsCd As String = .Text
                        .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
                        .Col = .GetColFromID("tatcont") : Dim sCmtCont As String = .Text

                        If LISAPP.APP_R.TatFn.fnExe_Tat_Reg(sBcNo, sTclsCd + "|", Ctrl.Get_Code(sCmtCont), Ctrl.Get_Name(sCmtCont)) = False Then
                            sErrMsg = "검체번호[" + Fn.BCNO_View(sBcNo) + "]  검사명[" + sTnmd + "] + vbcrlf"
                        End If
                    End If
                Next

                If sErrMsg <> "" Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, sErrMsg + "등록에 실패 했습니다.!!")
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "TAT 사유 등록을 성공 했습니다.!!")
                End If

            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    '< add yjlee 2009-03-30
    Private Sub btnClear_Tcls_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear_Tcls.Click

        Try
            Me.txtSelTest.Text = ""
            Me.txtSelTest.Tag = ""

            COMMON.CommXML.setOneElementXML(msXmlDir, msFile_Test, "TEST", "")
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
    '> add yjlee 2009-03-30 

    Private Sub txtName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPatnm.Click
        Me.txtPatnm.SelectionStart = 0
        Me.txtPatnm.SelectAll()
    End Sub

    Private Sub opt1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoIogbnO.CheckedChanged, rdoIogbnI.CheckedChanged

        Try
            If Me.rdoIogbnA.Checked Then
                Me.txtDeptWard.Visible = False
                Me.btnCdHelp_dept.Visible = False
                Me.lblIOGbn.Visible = False
                Me.btnDel.Visible = False
            Else
                Me.txtDeptWard.Visible = True
                Me.btnCdHelp_dept.Visible = True
                Me.lblIOGbn.Visible = True
                Me.btnDel.Visible = True
            End If

            If Me.rdoIogbnO.Checked Then
                Me.lblIOGbn.Text = "진료과"
            Else
                Me.lblIOGbn.Text = "병동"
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Me.txtDeptWard.Text = ""
        Me.txtDeptWard.Tag = ""
    End Sub

    Private Sub btnCdHelp_dept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp_dept.Click


        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            objHelp.FormText = lblIOGbn.Text

            Dim sFiled1 As String = ""
            Dim sFiled2 As String = ""
            Dim sKeyCodes As String = ""
            Dim dt As New DataTable

            If rdoIogbnO.Checked Then
                dt = OCSAPP.OcsLink.SData.fnGet_DeptList()

                sFiled1 = "deptnm"
                sFiled2 = "deptcd"

            Else
                dt = OCSAPP.OcsLink.SData.fnGet_WardList()

                sFiled1 = "wardnm"
                sFiled2 = "wardno"
            End If

            objHelp.TableNm = ""
            objHelp.Where = ""

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = sKeyCodes

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField(sFiled1, lblIOGbn.Text, 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField(sFiled2, "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnCdHelp_dept)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnCdHelp_dept.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_dept.Height + 80, dt)

            If aryList.Count > 0 Then
                Me.txtDeptWard.Text = "" : Me.txtDeptWard.Tag = ""

                For ix As Integer = 0 To aryList.Count - 1

                    If ix > 0 Then
                        Me.txtDeptWard.Text += ","
                        Me.txtDeptWard.Tag = Me.txtDeptWard.Tag.ToString + ","
                    End If
                    Me.txtDeptWard.Text += aryList.Item(ix).ToString.Split("|"c)(0)
                    Me.txtDeptWard.Tag = aryList.Item(ix).ToString.Split("|"c)(1)
                Next

            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub txtName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPatnm.GotFocus

        Me.txtPatnm.SelectionStart = 0
        Me.txtPatnm.SelectAll()

    End Sub

    Private Sub txtRegNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.Click
        Me.txtRegNo.SelectionStart = 0
        Me.txtRegNo.SelectAll()

    End Sub

    Private Sub txtRegNo_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.GotFocus
        Me.txtRegNo.SelectionStart = 0
        Me.txtRegNo.SelectAll()

    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown

        If e.KeyCode <> Keys.Enter Then Return

        Dim sRegNo As String = txtRegNo.Text.Trim

        If IsNumeric(sRegNo.Substring(0, 1)) Then
            Me.txtRegNo.Text = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
        Else
            Me.txtRegNo.Text = sRegNo.Substring(0, 1).ToUpper + sRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
        End If

    End Sub

    Private Sub btnCdHelp_test_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_test.Click

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim sSlipCd As String = Ctrl.Get_Code(Me.cboSlip)
            Dim sTestCds As String = ""

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            If txtSelTest.Tag Is Nothing Then txtSelTest.Tag = ""
            If txtSelTest.Tag.ToString <> "" Then sTestCds = "'" + txtSelTest.Tag.ToString.Replace(",", "','") + "'"

            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list(Ctrl.Get_Code(Me.cboSlip), "")
            Dim a_dr As DataRow() = dt.Select("tcdgbn IN ('P', 'B', 'S')")
            dt = Fn.ChangeToDataTable(a_dr)

            objHelp.FormText = "검사정보"
            objHelp.TableNm = ""
            objHelp.Where = ""
            objHelp.GroupBy = ""
            objHelp.OrderBy = ""
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = sTestCds

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("tnmd", "항목명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("testspc", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("tcdgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("sortl", "순서", , , , True)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtSelTest)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnCdHelp_test.Left, pntFrmXY.Y + pntCtlXY.Y + btnCdHelp_test.Height + 80, dt)

            If aryList.Count > 0 Then
                Me.txtSelTest.Text = "" : Me.txtSelTest.Tag = ""

                For ix As Integer = 0 To aryList.Count - 1
                    If ix > 0 Then
                        Me.txtSelTest.Tag = Me.txtSelTest.Tag.ToString + ", "
                        Me.txtSelTest.Text += ","
                    End If
                    '  Me.txtSelTest.Tag = Me.txtSelTest.Tag.ToString + aryList.Item(ix).ToString.Split("|"c)(1)
                    Me.txtSelTest.Tag = Me.txtSelTest.Tag.ToString + aryList.Item(ix).ToString.Split("|"c)(1)

                    Me.txtSelTest.Text += aryList.Item(ix).ToString.Split("|"c)(0)
                Next
            End If

            m_tooltip.RemoveAll()
            DP_Common.setToolTip(Me.CreateGraphics, Me.txtSelTest, Me.txtSelTest.Text, m_tooltip)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            COMMON.CommXML.setOneElementXML(msXmlDir, msFile_Test, "TEST", Me.txtSelTest.Text + "^" + Me.txtSelTest.Tag.ToString())
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try
            If mbQuery = False Then
                With spdList
                    .ReDraw = False

                    .Row = 1
                    .MaxRows += 1
                    .Action = FPSpreadADO.ActionConstants.ActionInsertRow

                    For intCol As Integer = 1 To .MaxCols
                        .Row = 0 : .Col = intCol : Dim strTmp As String = .Text
                        .Row = 1 : .Col = intCol : .Text = strTmp
                    Next

                    If spdList.MaxRows < 1 Then MsgBox("조회후 출력이 가능합니다.", MsgBoxStyle.Information, Me.Text)
                    If spdList.ExportToExcel("TAT.xls", "TAT List", "") Then Process.Start("TAT.xls")

                    .Row = 1
                    .Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                    .MaxRows -= 1

                    .ReDraw = True
                End With
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub
    '<20130213 ymg 소견삭제버튼 추가 

    Private Sub btnCmtDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCmtDel.Click

        Try
            Dim sErrMsg As String = ""

            With spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("chk")

                    If .Text = "1" Then
                        Dim tat_cmt_info As New COMMON.SVar.STU_TATCmtInfo
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text.Replace("-", "")
                        .Col = .GetColFromID("testcd") : Dim sTclsCd As String = .Text
                        .Col = .GetColFromID("tnmd") : Dim sTnmd As String = .Text
                        .Col = .GetColFromID("spccd") : Dim sSpcCd As String = .Text
                        .Col = .GetColFromID("tatcont") : Dim sCmtCont As String = .Text

                        If LISAPP.APP_R.TatFn.fnExe_Tat_CmtDel(sBcNo, sTclsCd + "|", Ctrl.Get_Code(sCmtCont), Ctrl.Get_Name(sCmtCont)) = False Then
                            sErrMsg = "검체번호[" + Fn.BCNO_View(sBcNo) + "]  검사명[" + sTnmd + "] + vbcrlf"
                        End If
                    End If
                Next

                If sErrMsg <> "" Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, sErrMsg + "삭제에 실패 했습니다.!!")
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "TAT 사유 삭제 성공 했습니다.!!")
                End If

            End With
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub


    Private Sub btnWcmt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWcmt.Click
        With spdList
            For i As Integer = 1 To .MaxRows
                .Row = i
                .Col = .GetColFromID("chk")

                If .Text = "1" Then
                    .Col = .GetColFromID("tatcont")
                    .Text = cboCmt.SelectedItem.ToString

                End If
            Next
        End With
    End Sub
End Class

