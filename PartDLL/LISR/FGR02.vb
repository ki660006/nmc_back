Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN

Imports System.Windows.Forms
Imports System.Drawing
Public Class FGR02
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : R01.dll, Class : FGR02" & vbTab

    Public mbBloodBankYN As Boolean = False
    Public mbBloodBankModify As Boolean = False
    Public msUse_SlipCd As String = ""
    Public msSearCh_mode As Integer = 0 '< 0:접수일시 조회 , 1 :결과일시 조회 <<< 20150804
    Public mbAutoQuery As Boolean
    Public mbAutoTAT As Boolean
    Public mbTATQ As Boolean = False

    Public msTitle As String
    Private Const msXMLDir As String = "\XML"
    Private msPartSlip As String = Application.StartupPath + msXMLDir & "\FGR02_SLIPINFO.XML"
    Private msTClsFileRSL As String = Application.StartupPath + msXMLDir + "\FGR02_RstSearchList.XML"

    Private mTATAlarmList As New POPUPWIN.POP_COM()

    Public WriteOnly Property BloodBankYN() As Boolean
        Set(ByVal value As Boolean)
            mbBloodBankYN = value
        End Set
    End Property

    Public Sub sbDisplay_Data(ByVal rsBcNo As String)

        AxPatInfo.BcNo = rsBcNo
        If Not AxPatInfo.fnDisplay_Data() Then
            MsgBox("접수된 검체가 없습니다!!")
            Return
        End If

        AxResult.FnDt = AxPatInfo.FnDt
        AxResult.sbDisplay_Data(rsBcNo)
        AxResult.sbFocus()
        AxResult.Focus()

    End Sub

    Private Function fnFind_RegNo(ByVal rsPatNm As String) As String
        Dim sFn As String = "fnFind_RegNo"

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim strWkGpCd As String = "", strWkGpNm As String = ""
            Dim strTclsCds As String = ""

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_byNm(rsPatNm)

            objHelp.FormText = "환자조회"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = strTclsCds

            objHelp.AddField("regno", "등록번호", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, , , "TNMD")
            objHelp.AddField("patnm", "성명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft, , , , "Y")
            objHelp.AddField("sex", "성별", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("idno", "주민번호", 12, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtSearch)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - txtSearch.Left, pntFrmXY.Y + pntCtlXY.Y + txtSearch.Height + 80, dt)

            If aryList.Count > 0 Then
                Dim sReturn As String = aryList.Item(0).ToString.Split("|"c)(0)
                txtSearch.Text = aryList.Item(0).ToString.Split("|"c)(1)
                Return sReturn
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Sub sbDisplay_part()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List(, IIf(mbBloodBankYN, "3", "0").ToString)
            
            Me.cboPartSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_slip()

        Dim sFn As String = "Sub sbDisplay_slip()"

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List(dtpTkDtS.Text.Replace("-", ""), False, mbBloodBankYN)

            Me.cboPartSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString + "] " + dt.Rows(ix).Item("slipnmd").ToString)
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbSearch(ByVal rsRstFlg As String)
        Dim sFn As String = "Sub Search()"

        Try
            AxPatInfo.sbDisplay_Init()
            AxResult.sbDisplay_Init("ALL")

            spdList.MaxRows = 0

            Dim sN As String = "", sHL As String = "", sPDC As String = "", sA As String = "", sEqFlag As String = "", sReRun As String = "", sER As String = ""

            If Me.chkN.Checked Then sN = "N"
            If Me.chkHL.Checked Then sHL = "HL"
            If Me.chkPDC.Checked Then sPDC = "PDC"
            If Me.chkA.Checked Then sA = "A"
            If Me.chkER.Checked Then sER = "ER"
            If Me.chkFlag.Checked Then sEqFlag = "CMT"
            If Me.chkReRun.Checked Then sReRun = "RERUN"
            If Me.chkNotRerun.Checked Then sReRun = "NOTRERUN"

            If Me.cboPartSlip.SelectedIndex > -1 Then

                'Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_TK(Ctrl.Get_Code(Me.cboPartSlip), Me.dtpTkDtS.Text, Me.dtpTkDtE.Text, sN, sHL, sPDC, sA, sEqFlag, sReRun, sER)
                'Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_TK(Ctrl.Get_Code(Me.cboPartSlip), Me.dtpTkDtS.Text, Me.dtpTkDtE.Text, sER)  '<<<20150805 이전
                Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_TK(Ctrl.Get_Code(Me.cboPartSlip), Me.dtpTkDtS.Text, Me.dtpTkDtE.Text, sER, msSearCh_mode) '<<<20150805 결과일자조회추가로 수정

                Dim dr() As DataRow
                Dim sSql As String = ""

                Select Case rsRstFlg
                    Case "0"
                        sSql += "rstflg_t = '00'"
                    Case "1"
                        sSql += "rstflg_t >= '01' AND rstflg_t <= '13'"
                    Case "2"
                        sSql += "rstflg_t >= '20' AND rstflg_t <= '23'"
                    Case "3"
                        sSql += "rstflg_t >= '3'"
                End Select

                If sN = "N" Then
                    sSql += IIf(sSql = "", "", " AND ").ToString + "TRIM(hl) = ''"
                ElseIf sN = "HL" Then
                    sSql += IIf(sSql = "", "", " AND ").ToString + "(hl = 'L' AND hl = 'H')"
                End If

                If sPDC <> "" Then sSql += IIf(sSql = "", "", " AND ").ToString + "(pm = 'P' OR dm = 'D' OR cm = 'C')"
                If sA <> "" Then sSql += IIf(sSql = "", "", " AND ").ToString + "am = 'A'"
                If sEqFlag <> "" Then sSql += IIf(sSql = "", "", " AND ").ToString + "TRIM(eqflag)  <> ''"

                If sReRun = "RERUN" Then
                    sSql += IIf(sSql = "", "", " AND ").ToString + "rerun > '0'"
                ElseIf sReRun = "NOTRERUN" Then
                    sSql += IIf(sSql = "", "", " AND ").ToString + "rerun = '0'"
                End If

                dr = dt.Select(sSql, "tkdt, bcno")
                dt = Fn.ChangeToDataTable(dr)


                sbDisplay_SpcList(dt)
                Me.spdList.SetActiveCell(0, 0)

                If spdList.MaxRows > 0 Then spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spdList.GetColFromID("bcno"), 1))

                If mnuRst.Checked = True Then
                    Me.AxResult.sbFocus()
                    Me.AxResult.Focus()
                Else
                    Me.spdList.Focus()
                End If

            Else
                Me.cboPartSlip.Focus()
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "검사분야를 선택해 주세요.")
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_SpcList(ByVal r_dt As DataTable)

        Me.spdList.MaxRows = r_dt.Rows.Count

        If r_dt.Rows.Count > 0 Then
            With Me.spdList
                For ix1 As Integer = 0 To r_dt.Rows.Count - 1
                    For ix2 As Integer = 1 To r_dt.Columns.Count
                        Dim iCol As Integer = .GetColFromID(r_dt.Columns(ix2 - 1).ColumnName.ToLower())
                        If iCol > 0 Then
                            .Row = ix1 + 1
                            .Col = iCol

                            .Text = r_dt.Rows(ix1).Item(ix2 - 1).ToString

                            If r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "deptcd" And _
                               (r_dt.Rows(ix1).Item(ix2 - 1).ToString.ToLower.IndexOf(PRG_CONST.DEPT_ER) Or _
                                r_dt.Rows(ix1).Item(ix2 - 1).ToString.ToLower.IndexOf("em")) >= 0 Then
                                .Row = ix1 + 1 : .Col = iCol : .ForeColor = Color.Red
                            End If

                            '<< JJH
                            'If r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "bcno" Then
                            '    Dim ERYN As String = LISAPP.COMM.RstFn.fnGet_ERYN(r_dt.Rows(ix1).Item(ix2 - 1).ToString.Replace("-", "").Replace(" ", ""))

                            '    If ERYN = "Y" Then
                            '        MsgBox("chk")
                            '    End If
                            'End If
                            '>>

                        Else
                            'If r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "statgbn" Then
                            '<< JJH   eryn  -> 자체응급 추가
                            If (r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "statgbn" Or r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "eryn") Then
                                If r_dt.Rows(ix1).Item(ix2 - 1).ToString.ToUpper = "Y" Or r_dt.Rows(ix1).Item(ix2 - 1).ToString.ToUpper <> "" Then
                                    .Row = ix1 + 1 : .Col = -1 : .ForeColor = Color.Red
                                End If
                            ElseIf r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "rmkyn" Then
                                If r_dt.Rows(ix1).Item(ix2 - 1).ToString.ToUpper = "Y" And r_dt.Rows(ix1).Item("statgbn").ToString = "" Then
                                    .Row = ix1 + 1 : .Col = -1 : .ForeColor = Color.Blue
                                End If
                            ElseIf r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "brainyn" Then
                                If r_dt.Rows(ix1).Item(ix2 - 1).ToString.ToUpper = "Y" And r_dt.Rows(ix1).Item("cancelyn").ToString.Equals("N") Then
                                    .Col = -1 : .BackColor = Color.Yellow
                                End If
                            ElseIf r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "tat" Then
                                Dim sBuf() As String = r_dt.Rows(ix1).Item("tat").ToString.Split("^"c)
                                If Val(sBuf(0)) > Val(sBuf(1)) And Val(sBuf(1)) > 0 Then
                                    .Col = 0 : .Text = "√" : .Col = -1 : .BackColor = Color.SkyBlue
                                End If
                            ElseIf r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "alarmtime" Then '2019-07-23 TAT 임박 시간 음영표시 추가
                                Dim sBuf() As String = r_dt.Rows(ix1).Item("tat").ToString.Split("^"c)
                                Dim sAlt As String = r_dt.Rows(ix1).Item("alarmtime").ToString
                                Dim srstflg As String = r_dt.Rows(ix1).Item("rstflg").ToString

                                If Val(sBuf(0)) >= Val(sAlt) And Val(sBuf(0)) <= Val(sBuf(1)) And Val(sBuf(1)) > 0 And Val(sAlt) > 0 And CDbl(srstflg) < 2 Then
                                    .Row = ix1 + 1 : .Col = -1 : .BackColor = Color.DimGray
                                End If
                            End If
                        End If
                    Next
                Next
            End With
        End If

    End Sub

    Private Sub FGR02_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.txtSearch.Text = ""
        Me.txtSearch.SelectAll()
        Me.txtSearch.Focus()
    End Sub

    Private Sub FGR02_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.PageUp
                btnUpDown_Click(btnUp, Nothing)
            Case Keys.PageDown
                btnUpDown_Click(btnDown, Nothing)
            Case Keys.F2
                If Me.lblSearch.Text = "검체번호" Then
                    btnToggle_Click(Nothing, Nothing)
                End If
                Me.txtSearch.Focus()
            Case Keys.F3
                AxResult.btnKeyPad_Click(Nothing, Nothing)

            Case Keys.F4
                btnClear_Click(Nothing, Nothing)

            Case Keys.F5
                btnSearch_Click(Nothing, Nothing)
            Case Keys.F9
                btnReg_ButtonClick(Nothing, Nothing)
            Case Keys.F11
                btnMW_ButtonClick(btnMW, New System.EventArgs)
            Case Keys.F12
                btnFN_ButtonClick(btnFN, New System.EventArgs)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select
    End Sub


    Private Sub FGR02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If msTitle <> "" Then Me.Text = msTitle

        If USER_INFO.USRID = "ACK" Then Me.btnReg_aborh.Visible = True
        If COMMON.CommLogin.LOGIN.USER_INFO.USRLVL = "S" Then btnRst_ocs.Visible = True

        '2022.08.03 JJH TAT 임박 알람 권한
        If USER_SKILL.Authority("T01", 2) Then lblTATAlarm.Visible = True

        If STU_AUTHORITY.UsrID = "ICU" Then
            btnChg_rstdt.Visible = True
            btnReg.Visible = False
        End If

        Me.dtpTkDtS.Value = Now
        Me.dtpTkDtE.Value = Now

        Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXMLDir, msPartSlip, "PARTSLIP")

        Try
            If sTmp.IndexOf("^"c) < 0 Then
                Me.cboQryGbn.SelectedIndex = 1
                sbDisplay_slip() ' 검사분야 표시 
                If sTmp = "" Then
                    Me.cboPartSlip.SelectedIndex = 0
                Else
                    If CInt(sTmp) < Me.cboPartSlip.Items.Count Then
                        Me.cboPartSlip.SelectedIndex = CInt(sTmp)
                    Else
                        If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0
                    End If
                End If
            Else
                Me.cboQryGbn.SelectedIndex = CInt(sTmp.Split("^"c)(0))
                If Me.cboQryGbn.SelectedIndex = 0 Then
                    sbDisplay_part() ' 검사부서 표시 
                Else
                    sbDisplay_slip() ' 검사분야 표시 
                End If

                If CInt(sTmp.Split("^"c)(1)) < Me.cboPartSlip.Items.Count Then
                    Me.cboPartSlip.SelectedIndex = CInt(sTmp.Split("^"c)(1))
                Else
                    If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception

        End Try

        Me.cboRstFlg.SelectedIndex = 0
        With Me.spdList
            .MaxRows = 0
            .Col = .GetColFromID("deptcd") : .Row = -1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter

            .Col = .GetColFromID("prtbcno") : .Row = -1
            .TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter
            .TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter
        End With

        Me.AxPatInfo.UsrLevel = STU_AUTHORITY.UsrID
        Me.AxPatInfo.sbDisplay_Init()

        Me.AxResult.UseBloodBank = mbBloodBankYN
        Me.AxResult.Form = Me
        Me.AxResult.ColHiddenYn = True
        Me.AxResult.BcNoAll = CType(IIf(PRG_CONST.RST_BCNO_CHECK = "1", True, False), Boolean)

        mnuRst.Checked = False
        mnuSearchList.Checked = True
        sTmp = COMMON.CommXML.getOneElementXML(msXMLDir, msTClsFileRSL, "RSL")
        Select Case sTmp
            Case "R"
                mnuRst.Checked = True
                mnuSearchList.Checked = False
        End Select


        If Me.txtSearch.Text <> "" Then
            Me.txtSearch_KeyDown(Nothing, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
        End If
        '20210719 jhs 요청으로 인해 중간보고 안보이게 수정 
        If mbBloodBankYN Then
            '    Me.btnMW.Text = "중간보고(F11)"

            Me.btnRerun.Visible = False

            '    Me.btnReg.Left = Me.btnClear.Left - Me.btnReg.Width - 2
            '    Me.btnMW.Left = Me.btnReg.Left - Me.btnMW.Width - 2
            '    Me.btnFN.Left = Me.btnMW.Left - Me.btnMW.Width - 2
            Me.btnRst_Clear.Left = Me.btnFN.Left - Me.btnRst_Clear.Width - 2
            Me.chkMW.Left = Me.btnRst_Clear.Left - Me.chkMW.Width - 2
            '    Me.btnReg.Visible = True
        End If
        '--------------------------------------

        'Me.txtSearch.Text = ""
        Me.txtSearch.Focus()
        Me.txtSearch.SelectAll()

        If msUse_SlipCd <> "" Then
            Me.btnRerun.Visible = True
            Me.btnRerun.Visible = True
        End If

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus

        Me.txtSearch.SelectionStart = 0
        Me.txtSearch.SelectAll()

    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sFn As String = "Sub txtSearch_KeyDown(Object, System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown"
        Try
            Me.AxResult.FORMID = Me.Name  ''' 정은추가  

            If e.KeyCode <> Windows.Forms.Keys.Enter Then Exit Sub

            Me.txtSearch.Text = Me.txtSearch.Text.Trim

            e.Handled = True
            If Me.lblSearch.Text = "검체번호" Then
                Dim sBCNO As String = Trim(Me.txtSearch.Text)

                Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "").Trim()

                If sBCNO = "" Then
                    MsgBox("검체번호를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If

                '< add freety 2005/09/14 : -가 포함된 검체번호 입력도 허용
                sBCNO = sBCNO.Replace("-", "")
                '>


                If Len(sBCNO) = 11 Or Len(sBCNO) = 12 Then
                    sBCNO = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(Mid(sBCNO, 1, 11))
                End If

                If sBCNO = "" Then
                    txtSearch.SelectAll()
                    Exit Sub
                End If
                If sBCNO.Length = 14 Then sBCNO += "0"
                If sBCNO.Length < 15 Then Return

                If mbBloodBankYN Then
                    If PRG_CONST.BCCLS_BloodBank <> sBCNO.Substring(8, 2) Then
                        MsgBox("접수된 검체가 없습니다!!")
                        txtSearch.SelectAll()
                        txtSearch.Focus()
                        Return
                    End If
                End If

                Me.AxPatInfo.BcNo = sBCNO
                Me.AxPatInfo.SlipCd = "" ' Ctrl.Get_Code(me.cboPartSlip)
                If Not Me.AxPatInfo.fnDisplay_Data() Then
                    MsgBox("접수된 검체가 없습니다!!")
                    Me.txtSearch.SelectAll()
                    Me.txtSearch.Focus()
                    Return
                End If

                Me.AxResult.FORMID = Me.Name  ''' 정은추가  
                Me.AxResult.Form = Me
                Me.AxResult.RegNo = AxPatInfo.RegNo
                Me.AxResult.PatName = AxPatInfo.PatNm
                Me.AxResult.SexAge = AxPatInfo.SexAge
                Me.AxResult.DeptCd = AxPatInfo.DeptName
                Me.AxResult.FnDt = AxPatInfo.FnDt
                Me.AxResult.AboRh = AxPatInfo.ABORh
                Me.AxResult.SlipCd = Ctrl.Get_Code(Me.cboPartSlip.Text)
                Me.AxResult.TgrpCds = ""
                Me.AxResult.WKgrpCd = ""
                Me.AxResult.EqCd = ""
                Me.AxResult.sbDisplay_Data(sBCNO)
                Me.AxResult.sbFocus()

                Me.txtSearch.SelectAll()
                Me.txtSearch.Focus()

                'If mnuRst.Checked = True Then
                '    Me.AxResult.sbFocus()
                '    Me.AxResult.Focus()
                'Else
                '    Me.txtSearch.SelectAll()
                '    Me.txtSearch.Focus()
                'End If

            Else
                ' 등록번호 또는 성명 입력시 처리
                Dim sRegNo As String = ""

                If Me.lblSearch.Text = "성    명" Then
                    sRegNo = fnFind_RegNo(txtSearch.Text)
                Else
                    sRegNo = Me.txtSearch.Text.Trim

                    If IsNumeric(sRegNo.Substring(0, 1)) Then
                        sRegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                    Else
                        sRegNo = sRegNo.Substring(0, 1).ToUpper + sRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                    End If

                    Me.txtSearch.Text = sRegNo
                End If

                If sRegNo = "" Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "등록번호를 입력해 주세요.")
                    Return
                End If

                Dim dt As DataTable

                If Me.chkFilter.Checked Then
                    dt = LISAPP.APP_R.RstFn.fnGet_SpcList_Reg(sRegNo, Ctrl.Get_Code(Me.cboPartSlip), Format(dtpTkDtS.Value, "yyyy-MM-dd").ToString, Format(dtpTkDtE.Value, "yyyy-MM-dd").ToString, mbBloodBankYN)
                Else
                    dt = LISAPP.APP_R.RstFn.fnGet_SpcList_Reg(sRegNo, Ctrl.Get_Code(Me.cboPartSlip), , , mbBloodBankYN)
                End If

                sbDisplay_SpcList(dt)
                spdList.SetActiveCell(0, 0)

                If dt.Rows.Count > 0 Then
                    spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spdList.GetColFromID("chk") + 1, 1))
                Else
                    MsgBox("해당하는 환자가 없습니다.", MsgBoxStyle.Information, Me.Text)
                End If

            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click

        Dim CommFn As New Fn
        'Fn.SearchToggle(lblSearch, btnToggle, enumToggle.BcnoToRegno, txtSearch)
        Fn.SearchToggle(Me.lblSearch, Me.btnToggle, enumToggle.Regno_Name_Bcno, Me.txtSearch)

        txtSearch.Text = ""
        txtSearch.Focus()
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        If e.row < 1 Then Exit Sub

        If e.col = Me.spdList.GetColFromID("chk") Then
            With Me.spdList
                .Row = e.row
                .Col = e.col : .Text = IIf(.Text = "1", "", "1").ToString
            End With
        Else
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sBcNo As String = ""
            Dim sPartSlip As String = ""

            With Me.spdList
                .Row = e.row
                .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "").Trim
                .Col = .GetColFromID("partslip") : sPartSlip = .Text.Trim
                If Me.lblSearch.Text = "검체번호" Then Me.txtSearch.Text = sBcNo
            End With

            Me.AxPatInfo.BcNo = sBcNo
            Me.AxPatInfo.SlipCd = sPartSlip
            Me.AxPatInfo.fnDisplay_Data()

            Me.AxResult.FORMID = Me.Name  ''' 정은추가  
            Me.AxResult.Form = Me
            Me.AxResult.RegNo = AxPatInfo.RegNo
            Me.AxResult.PatName = AxPatInfo.PatNm
            Me.AxResult.SexAge = AxPatInfo.SexAge
            Me.AxResult.DeptCd = AxPatInfo.DeptName
            Me.AxResult.FnDt = AxPatInfo.FnDt
            Me.AxResult.AboRh = AxPatInfo.ABORh
            Me.AxResult.SlipCd = sPartSlip
            Me.AxResult.TgrpCds = ""
            Me.AxResult.WKgrpCd = ""
            Me.AxResult.EqCd = ""
            Me.AxResult.sbDisplay_Data(sBcNo)

            If mnuRst.Checked = True Then
                Me.AxResult.sbFocus()
                Me.AxResult.Focus()
            Else
                Me.spdList.Focus()
            End If

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

    End Sub

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click

        If btnMove.Text = "◀" Then
            Me.btnMove.Left = 1
            Me.AxPatInfo.Left = 9 : Me.AxPatInfo.Width = Me.Width - btnDown.Width - 20
            Me.AxResult.Left = 9 : Me.AxResult.Width = Me.Width - 15

            Me.btnHistory.Left = 9
            Me.btnHistory.Left = 9

            Me.Refresh()

            Me.btnMove.Text = "▶"
        Else
            Me.btnMove.Left = 286
            Me.AxPatInfo.Left = 295 : Me.AxPatInfo.Width -= 286
            Me.AxResult.Left = 295 : Me.AxResult.Width -= 286

            Me.btnHistory.Left = 295
            Me.btnHistory.Left = 295

            Me.btnMove.Text = "◀"
        End If

    End Sub

    Private Sub Panel3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel3.DoubleClick

        If AxResult.ColHiddenYn Then
            AxResult.ColHiddenYn = False
        Else
            AxResult.ColHiddenYn = True
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        sbSearch(Ctrl.Get_Code(Me.cboRstFlg))

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click

        '''AxPatInfo.sbDisplay_Init()
        Me.AxPatInfo.sbDisplay_Init()
        Me.AxResult.sbDisplay_Init("ALL")

        Me.spdList.MaxRows = 0

        '<<<----SKY20071211 
        Me.txtSearch.SelectAll()
        Me.txtSearch.Focus()
        '------------>>>>>
    End Sub

    Private Sub btnReg_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click

        Dim blnRst As Boolean

        If tmrReq.Enabled = True Then
            tmrReq.Enabled = False  '접수중 자동조회 일시중지
        End If
        If tmrTAT.Enabled = True Then
            tmrTAT.Enabled = False
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        blnRst = AxResult.fnReg("1")
        If blnRst Then

            Me.AxPatInfo.sbDisplay_Init()
            Me.AxResult.sbDisplay_Init("ALL")

            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)

            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()
        End If

        If mbAutoQuery = True Then
            tmrReq.Enabled = True
        End If
        If mbAutoTAT = True Then
            tmrTAT.Enabled = True
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnMW_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMW.Click
        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If tmrReq.Enabled = True Then
            tmrReq.Enabled = False
        End If
        If tmrTAT.Enabled = True Then
            tmrTAT.Enabled = False
        End If

        blnRst = AxResult.fnReg(IIf(btnMW.Text.StartsWith("중간보고"), "22", "2").ToString) ''' 2 결과확인 
        If blnRst Then
            Me.AxPatInfo.sbDisplay_Init()
            Me.AxResult.sbDisplay_Init("ALL")

            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)

            If Me.chkMW.Checked And Me.chkMW.Visible Then Me.chkMW.Checked = False

            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()

        End If

        If mbAutoQuery = True Then
            tmrReq.Enabled = True
        End If
        If mbAutoTAT = True Then
            tmrTAT.Enabled = True
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnFN_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFN.Click
        If STU_AUTHORITY.FNReg <> "1" Then
            MsgBox("결과검증 권한이 없습니다.!!  확인하세요.")
            Return
        End If

        Dim blnRst As Boolean = False

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If tmrReq.Enabled = True Then
            tmrReq.Enabled = False
        End If
        If tmrTAT.Enabled = True Then
            tmrTAT.Enabled = False
        End If

        blnRst = AxResult.fnReg("3")
        If blnRst Then
            Me.AxPatInfo.sbDisplay_Init()
            Me.AxResult.sbDisplay_Init("ALL")

            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)

            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()
            sbDisplay_Data(Me.AxResult.BCNO) '<20150922 검증시 재조회 기능 추가 
        End If

        If mbAutoQuery = True Then
            tmrReq.Enabled = True
        End If
        If mbAutoTAT = True Then
            tmrTAT.Enabled = True
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnRst_Clear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRst_Clear.Click

        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        If tmrReq.Enabled = True Then
            tmrReq.Enabled = False
        End If
        If tmrTAT.Enabled = True Then
            tmrTAT.Enabled = False
        End If

        blnRst = AxResult.fnReg_Erase()

        If blnRst Then
            AxPatInfo.sbDisplay_Init()
            AxResult.sbDisplay_Init("ALL")

            txtSearch.SelectAll()
            txtSearch.Focus()
        End If

        If mbAutoQuery = True Then
            tmrReq.Enabled = True
        End If
        If mbAutoTAT = True Then
            tmrTAT.Enabled = True
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnBFN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBFN.Click

        If STU_AUTHORITY.FNReg <> "1" Then
            MsgBox("결과검증 권한이 없습니다.!!  확인하세요.")
            Return
        End If

        Dim strChk As String = ""
        Dim blnRst As Boolean = False

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            With Me.spdList
                AxResult.BatchMode = True
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk") : strChk = .Text

                    If strChk = "1" Then
                        Dim sBcNo As String = ""

                        .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "")

                        Me.AxPatInfo.BcNo = sBcNo
                        Me.AxPatInfo.fnDisplay_Data()

                        Me.AxResult.Form = Me
                        Me.AxResult.RegNo = AxPatInfo.RegNo
                        Me.AxResult.PatName = AxPatInfo.PatNm
                        Me.AxResult.SexAge = AxPatInfo.SexAge
                        Me.AxResult.SlipCd = Ctrl.Get_Code(cboPartSlip)
                        Me.AxResult.sbDisplay_Data(sBcNo, True)

                        Threading.Thread.Sleep(100)

                        blnRst = AxResult.fnReg("3", , , True)
                        AxPatInfo.sbDisplay_Init()
                        AxResult.sbDisplay_Init("ALL")

                        If blnRst Then
                        End If
                    End If
                Next

                AxResult.BatchMode = False
            End With

            MsgBox("완료 되었습니다.!!")

        Catch ex As Exception
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try


    End Sub

    Private Sub spdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        Select Case Convert.ToInt32(e.keyCode)
            Case Keys.PageUp
                e.keyCode = 0
            Case Keys.PageDown
                e.keyCode = 0
            Case Keys.Enter
                Dim iRow As Integer = spdList.ActiveRow
                spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(2, iRow))
        End Select
    End Sub

    Private Sub chkSel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSel.CheckedChanged

        For intRow As Integer = 0 To spdList.MaxRows
            With spdList
                .Row = intRow
                .Col = .GetColFromID("chk") : .Text = IIf(chkSel.Checked, "1", "").ToString
            End With
        Next
    End Sub

    Private Sub mnuRst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuRst.Click
        mnuRst.Checked = True
        mnuSearchList.Checked = False
        COMMON.CommXML.setOneElementXML(msXMLDir, msTClsFileRSL, "RSL", "R")
    End Sub

    Private Sub mnuSearchList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSearchList.Click
        mnuRst.Checked = False
        mnuSearchList.Checked = True
        COMMON.CommXML.setOneElementXML(msXMLDir, msTClsFileRSL, "RSL", "SL")
    End Sub

    Private Sub btnHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory.Click

        'frm = Ctrl.CheckFormObject(Me, "누적결과조회(결과)")

        Dim sRegNo As String = AxPatInfo.RegNo
        Dim sPartSlip As String = Ctrl.Get_Code(Me.cboPartSlip)

        Dim frm As Windows.Forms.Form
        frm = New LISV.FGRV14(sRegNo, "", "", sPartSlip, True)

        'frm.MdiParent = Me.MdiParent
        frm.WindowState = Windows.Forms.FormWindowState.Maximized
        frm.Text = "누적결과조회(결과)"
        frm.Activate()
        frm.ShowDialog()

    End Sub

    Private Sub btnUpDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click, btnDown.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        If spd.MaxRows = 0 Then Return

        Dim iNext As Integer = 0

        If CType(sender, Windows.Forms.Button).Name.ToLower.EndsWith("up") Then
            If spd.ActiveRow < 1 Then Return

            iNext -= 1
        Else
            If spd.ActiveRow = spd.MaxRows Then Return

            iNext += 1
        End If

        Me.spdList_ClickEvent(spd, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spd.GetColFromID("regno"), spd.ActiveRow + iNext))

        With spd
            .ReDraw = False
            .SetActiveCell(.GetColFromID("regno"), .ActiveRow + iNext)
            .Action = FPSpreadADO.ActionConstants.ActionGotoCell
            .ReDraw = True
        End With


    End Sub

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

    End Sub

    Public Sub New(ByVal rsBloodBankYN As Boolean)

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        mbBloodBankYN = rsBloodBankYN
        If rsBloodBankYN Then
            msPartSlip = Application.StartupPath + msXMLDir & "\FGR02_SLIPINFO_B.XML"
            msTClsFileRSL = Application.StartupPath + msXMLDir + "\FGR02_RstSearchList_B.XML"

            Me.Text = Me.Text + "(혈액은행)"
            '20210719 jhs 혈액은행 중간보고 없애기 
            ' Me.chkMW.Visible = False
            '---------------------------
        End If

    End Sub



    Public Sub New(ByVal rsBcNo As String, ByVal rsRegNo As String)
        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        If rsRegNo <> "" Then
            btnToggle_Click(Nothing, Nothing)
            If Me.lblSearch.Text <> "등록번호" Then btnToggle_Click(Nothing, Nothing)
            If Me.lblSearch.Text <> "등록번호" Then btnToggle_Click(Nothing, Nothing)
            If Me.lblSearch.Text <> "등록번호" Then btnToggle_Click(Nothing, Nothing)
            If Me.lblSearch.Text <> "등록번호" Then btnToggle_Click(Nothing, Nothing)

            Me.txtSearch.Text = rsRegNo
        Else
            Me.txtSearch.Text = rsBcNo
        End If
    End Sub

    Private Sub chkN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkN.Click

        If chkN.Checked Then
            chkHL.Checked = False
        End If

    End Sub

    Private Sub chkHL_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkHL.Click
        If chkHL.Checked Then
            chkN.Checked = False
        End If
    End Sub

    Private Sub chkReRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkReRun.Click
        If chkReRun.Checked Then
            If chkNotRerun.Checked Then chkNotRerun.Checked = False
        End If
    End Sub

    Private Sub chkNotReRun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNotRerun.Click
        If chkNotRerun.Checked Then
            If chkReRun.Checked Then chkReRun.Checked = False
        End If
    End Sub

    Private Sub chkMoveCol_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMoveCol.CheckedChanged

        If chkMoveCol.Checked Then
            spdList.AllowColMove = True
        Else
            spdList.AllowColMove = False
        End If

    End Sub

    Private Sub cboRstFlag_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRstFlg.SelectedIndexChanged

        If Ctrl.Get_Code(cboRstFlg) = "0" Then
            If chkNotRerun.Checked Then chkNotRerun.Checked = False
            chkNotRerun.Enabled = False
        Else
            chkNotRerun.Enabled = True
        End If

    End Sub

    Private Sub txtSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Dim sFn As String = ""

        Try
            Me.txtSearch.SelectAll()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRerun_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRerun.Click
        Dim blnRst As Boolean = False

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            blnRst = AxResult.fnReRun("Standard")

            If blnRst Then
                Me.AxPatInfo.sbDisplay_Init()
                Me.AxResult.sbDisplay_Init("ALL")

                Me.txtSearch.SelectAll()
                Me.txtSearch.Focus()
            End If
        Catch ex As Exception
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub AxResult_Call_SpRst(ByVal BcNo As String, ByVal TclsCd As String) Handles AxResult.Call_SpRst
        Try
            Dim frmChild As Windows.Forms.Form
            frmChild = New LISR.FGR08(1, TclsCd, BcNo)
            CType(frmChild, LISR.FGR08).msUse_PartCd = ""

            Me.AddOwnedForm(frmChild)
            frmChild.WindowState = FormWindowState.Normal
            frmChild.Activate()
            frmChild.Show()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnRst_ocs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRst_ocs.Click

        Try
            Dim blnRet As Boolean = (New LISAPP.APP_R.AxRstFn).fnReg_OCS(AxResult.BCNO)

            MsgBox(IIf(blnRet, "성공", "실패").ToString)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub btnReg_aborh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_aborh.Click
        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim lgDiff As Long = DateDiff(DateInterval.Day, Me.dtpTkDtS.Value, Me.dtpTkDtE.Value)


            For ix1 As Long = 0 To lgDiff
                Dim sDate As String = Format(DateAdd(DateInterval.Day, ix1 * -1, Me.dtpTkDtE.Value), "yyyyMMdd").ToString

                Dim blnRet As Boolean = (New LISAPP.APP_R.AxRstFn).fnReg_AboRh(sDate)

                If blnRet = False Then MsgBox("실패: " + sDate)
            Next
            MsgBox("완료")
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub btnChg_rstdt_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChg_rstdt.Click

        Dim frm As New FGR02_S01


        Dim strRet As String = frm.Display_Result()

        If strRet = "" Then Return
        Try
            Dim blnRet As Boolean = (New LISAPP.APP_R.AxRstFn).fnReg_Change_CollAndTkAndRst_date(AxResult.BCNO, strRet)

            If blnRet Then
                MsgBox("채혈/접수/결과일시 변경에 성공 했습니다.!!", , "결과등록")
            Else
                MsgBox("채혈/접수/결과일시 변경을 실패 했습니다.!!", , "결과등록")
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub FGR_closed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub cboPartSlip_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged

        Me.btnClear_Click(Nothing, Nothing)
        Me.txtSearch.Text = ""

        COMMON.CommXML.setOneElementXML(msXMLDir, msPartSlip, "PARTSLIP", Me.cboQryGbn.SelectedIndex.ToString + "^" + Me.cboPartSlip.SelectedIndex.ToString)

    End Sub

    Private Sub cboQryGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQryGbn.SelectedIndexChanged

        If Me.cboQryGbn.SelectedIndex = 0 Then
            sbDisplay_part()
        Else
            sbDisplay_slip()
        End If

        If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0

    End Sub


    Private Sub dtpTkDtS_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpTkDtS.CloseUp, dtpTkDtE.CloseUp
        Me.txtSearch.Text = ""
    End Sub

    Private Sub chkMW_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMW.CheckedChanged
        If Me.chkMW.Checked Then
            Me.btnMW.Text = "중간보고(F11)"
        Else
            Me.btnMW.Text = "결과확인(F11)"
        End If
    End Sub

    Private Sub AxResult_ChangedTestCd(ByVal BcNo As String, ByVal TestCd As String) Handles AxResult.ChangedTestCd
        Me.AxPatInfo.sbDisplay_rst_info(BcNo, TestCd)
    End Sub

    Private Sub btnQuery_pat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery_pat.Click

        Try
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_PatInfo_Current(Me.AxPatInfo.RegNo) '.Text.Trim())

            If dt.Rows.Count = 0 Then
                MsgBox("OCS에서 환자정보를 찾을 수 없습니다!!", MsgBoxStyle.Information)

                Return
            End If

            Dim iLeft As Integer = Ctrl.FindControlLeft(btnQuery_pat)
            Dim iTop As Integer = Ctrl.menuHeight + Ctrl.FindControlTop(btnQuery_pat) + btnQuery_pat.Height

            Dim patallinfo As New OCSAPP.FGOCS01

            With patallinfo
                .Left = iLeft
                .Top = iTop

                .gsRegNo = dt.Rows(0).Item("regno").ToString()
                .gsPatNm = dt.Rows(0).Item("patnm").ToString()
                .gsSexAge = dt.Rows(0).Item("sexage").ToString()
                .gsIdNo = dt.Rows(0).Item("idno").ToString()

                .gsOrdDt = dt.Rows(0).Item("orddt").ToString()
                .gsDeptNm = dt.Rows(0).Item("deptnm").ToString()
                .gsDoctorNm = dt.Rows(0).Item("doctornm").ToString()
                .gsWardRoom = dt.Rows(0).Item("wardroom").ToString()
                '.InWonDate = dt.Rows(0).Item("entdt").ToString + "/" + dt.Rows(0).Item("entdt_to").ToString
                .gsNowDate = Format(Now, "yyyyMMdd").ToString

                .gsTel = (dt.Rows(0).Item("tel1").ToString() + " / " + dt.Rows(0).Item("tel2").ToString()).Trim
                If .gsTel.StartsWith("/") Then .gsTel = .gsTel.Substring(1)
                If .gsTel.EndsWith("/") Then .gsTel = .gsTel.Substring(0, .gsTel.Length - 1)

                .gsAddr1 = dt.Rows(0).Item("addr1").ToString().Trim
                .gsAddr2 = dt.Rows(0).Item("addr2").ToString().Trim

                .sbDisplay_PatInfo() '환자 기본정보 출력

                .sbDisplay_SujinInfo() '환자 수진내역 출력

                .spdOrdDt.MaxRows = 0

                .spdOrdInfo.MaxRows = 0

                .ShowDialog()
            End With

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Label14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label14.Click
        If Label14.Text = "접수일자" Then
            Label14.Text = "결과일자"
            chkFilter.Text = "결과일자 조건 적용"
            msSearCh_mode = 1
        ElseIf Label14.Text = "결과일자" Then
            Label14.Text = "접수일자"
            chkFilter.Text = "접수일자 조건 적용"
            msSearCh_mode = 0
        End If
    End Sub

    Private Sub lblAutoQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAutoQuery.Click
        Dim objButton As Windows.Forms.Label = CType(sender, Windows.Forms.Label)
        Dim strTag As String = CType(objButton.Tag, String)

        Try
            If mbAutoQuery = False Then
                ' 자동조회 On 설정
                With lblAutoQuery
                    .Text = "자동조회 ON"
                    .BackColor = System.Drawing.Color.FromArgb(179, 232, 147)
                    .ForeColor = System.Drawing.Color.FromArgb(0, 64, 0)
                End With
                mbAutoQuery = True


                ' 자동조회초 조회
                If IsNumeric(txtAutoSearchSec.Text) Then
                    tmrReq.Interval = CInt(txtAutoSearchSec.Text) * 1000
                End If

                ' 자동조회 타이머 동작
                tmrReq.Enabled = True

                txtAutoSearchSec.Enabled = True
                'fnFormClear(0)

            Else
                ' 자동조회 Off 설정
                With lblAutoQuery
                    .Text = "자동조회 OFF"
                    .BackColor = System.Drawing.SystemColors.Control
                    .ForeColor = System.Drawing.SystemColors.ControlText
                End With
                mbAutoQuery = False



                ' 자동조회 타이머 동작
                tmrReq.Enabled = False


                txtAutoSearchSec.Enabled = False
            End If

            btnSearch_Click(Nothing, Nothing)

            ' 자동조회는 처음에 조회
            If mbAutoQuery = True Then btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    ' 자동조회 타이머
    Private Sub tmrReq_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrReq.Tick

        Try
            'tmrReq.Enabled = False
            Debug.WriteLine("R  :" & Now.ToLongTimeString)

            Application.DoEvents()
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub lblTATAlarm_Click(sender As Object, e As EventArgs) Handles lblTATAlarm.Click

        Try
            If mbAutoTAT = False Then
                'TAT 임박 알람 On 설정
                With lblTATAlarm
                    .Text = "TAT 임박 알람 ON"
                    .BackColor = Color.Red
                    .ForeColor = Color.White
                End With
                mbAutoTAT = True

                'TAT 임박 알람 타이머 동작
                tmrTAT.Enabled = True

                'lblTATAlarm.Enabled = True
                'fnFormClear(0)

            Else
                'TAT 임박 알람 Off 설정
                With lblTATAlarm
                    .Text = "TAT 임박 알람 OFF"
                    .BackColor = System.Drawing.Color.FromArgb(179, 232, 147)
                    .ForeColor = System.Drawing.Color.FromArgb(0, 64, 0)
                End With
                mbAutoTAT = False



                'TAT 임박 알람 타이머 동작
                tmrTAT.Enabled = False


                'lblTATAlarm.Enabled = False
            End If

            'TAT 임박 조회는 처음에 조회
            If mbAutoTAT = True Then sbTATAlarm()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub tmrTAT_Tick(sender As Object, e As EventArgs) Handles tmrTAT.Tick

        If mbTATQ = True Then Return

        sbTATAlarm()
    End Sub

    Private Sub sbTATAlarm()

        Try

            mbTATQ = True

            mTATAlarmList.sbPOPUP_UrineTATOverList()

            mbTATQ = False

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

End Class