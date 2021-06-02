'>>> 검체별 결과저장 및 보고 (M)

Imports COMMON.CommFN
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar
Imports COMMON.CommConst

Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing

Public Class FGM01
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGM01.vb, Class : FGM01" & vbTab

    Private msTCLSDir As String = "\XML"
    Private msTABFile As String = System.Windows.Forms.Application.StartupPath & msTCLSDir & "\FGM01_TABCFG.XML"
    Private msSLIPFile As String = System.Windows.Forms.Application.StartupPath & msTCLSDir & "\FGM01_SLIP.XML"
    Private msWGFile As String = System.Windows.Forms.Application.StartupPath & msTCLSDir & "\FGM01_WGCD.XML"
    'Private msTGFile As String = System.Windows.Forms.Application.StartupPath & msTCLSDir & "\FGM01_TGCD.XML"

    Public msSearCh_mode As Integer = 0 '< 0:접수일시 조회 , 1 :결과일시 조회 <<< 20150804

    Private miProcessing As Integer = 0

    Private msSEP_Title As String = " - "

    Friend WithEvents lblTclsCd As System.Windows.Forms.Label
    Friend WithEvents grpWkqry As System.Windows.Forms.GroupBox
    Friend WithEvents btnWLDelete As System.Windows.Forms.Button
    Friend WithEvents btnWLUpdate As System.Windows.Forms.Button
    Friend WithEvents btnWLRead As System.Windows.Forms.Button
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents dtpTkE As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpTkS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboRstFlg As System.Windows.Forms.ComboBox
    Friend WithEvents btnRst_Ocs As System.Windows.Forms.Button
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents AxPatInfo As AxAckResult.AxRstPatInfo
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnMW As CButtonLib.CButton
    Friend WithEvents btnRst_Clear As CButtonLib.CButton
    Friend WithEvents btnFN As CButtonLib.CButton
    Friend WithEvents btnHistory As CButtonLib.CButton
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents spdTgrp As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblTgrp As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cboWL As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpWLdts As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblTest_wl As System.Windows.Forms.Label
    Friend WithEvents btnQuery_wl As CButtonLib.CButton
    Friend WithEvents cboRstFlg_wl As System.Windows.Forms.ComboBox
    Friend WithEvents dtpWLdte As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnReg_err As System.Windows.Forms.Button
    Friend WithEvents btnQuery_pat As System.Windows.Forms.Button
    Friend WithEvents cmuRstList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRst As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSearchList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BottomToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents TopToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents RightToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents LeftToolStripPanel As System.Windows.Forms.ToolStripPanel
    Friend WithEvents ContentPanel As System.Windows.Forms.ToolStripContentPanel
    Friend WithEvents btnAddmic As CButtonLib.CButton
    Friend WithEvents axResult As AxAckResult.AxRstInput_m

    Public Sub sbDisplay_Data(ByVal rsBcNo As String)
        '직접 입력 시에는 다시 조회 가능하도록 처리
        Me.txtBcNo.AccessibleName = ""
        sbDisplay_BcNo(rsBcNo)
    End Sub

    Private Sub sbDisplay_BcNo(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_BcNo"

        Try
            '이전 BcNo와 같으면 Return
            If Me.txtBcNo.AccessibleName = rsBcNo Then Return

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Me.AxPatInfo.BcNo = rsBcNo
            Me.AxPatInfo.SlipCd = "" ' Ctrl.Get_Code(Me.cboSlip)
            Me.AxPatInfo.fnDisplay_Data()

            Me.axResult.Form = Me
            Me.axResult.RegNo = AxPatInfo.RegNo
            Me.axResult.PatName = AxPatInfo.PatNm
            Me.axResult.SexAge = AxPatInfo.SexAge
            Me.axResult.DeptCd = AxPatInfo.DeptName
            Me.axResult.TkDt = AxPatInfo.TkDt.Replace("-", "").Replace(":", "")
            Me.axResult.FnDt = AxPatInfo.FnDt.Replace("-", "").Replace(":", "")
            Me.axResult.PartSlip = Ctrl.Get_Code(cboSlip)
            Me.axResult.EqCd = ""
            Me.axResult.WKgrpCd = ""
            Me.axResult.TgrpCds = ""
            Me.axResult.TestCds = ""

            Select Case Me.tbcOpt.SelectedTab.Text
                Case "검사분야별"
                    If Ctrl.Get_Code(Me.lblTgrp) <> "" Then
                        Me.axResult.PartSlip = ""
                        Me.axResult.TgrpCds = Ctrl.Get_Code(Me.lblTgrp)
                    End If

                Case "작업번호별"
                    Me.axResult.WKgrpCd = Ctrl.Get_Code(cboWkGrp)
                Case "W/L"
                    If Me.lblTest_wl.Text <> "" Then
                        Me.axResult.TestCds = Me.lblTest_wl.Tag.ToString.Split("^"c)(0).Replace("|", ",")
                    End If
            End Select

            Me.axResult.QueryMOde = True
            If Me.axResult.sbDisplay_Data(rsBcNo) Then
                Me.axResult.sbFocus()
                Me.axResult.Focus()
            Else
                '< 20121211 접수내역업는 검체 상태값 확인

                Dim dt As DataTable = fnSpc_info(rsBcNo)
                Dim sSpcflg As String
                Dim sWrYn As String


                If dt.Rows.Count > 0 Then

                    sSpcflg = dt.Rows(0).Item("spcflg").ToString().Trim()
                    sWrYn = dt.Rows(0).Item("WrYn").ToString().Trim()

                    If sSpcflg = "R" Then
                        If sWrYn = "0" Then
                            MsgBox("Reject 검체입니다.")
                        Else
                            MsgBox("부적합등록된 검체입니다.")
                        End If

                    ElseIf sSpcflg = "3" Or sSpcflg = "2" Or sSpcflg = "1" Then
                        MsgBox("라벨/채혈 후 접수되지 않은 검체입니다.")
                    Else
                        MsgBox("접수된 검체가 없습니다!!")
                    End If

                Else
                    MsgBox("접수된 검체가 없습니다!!")
                End If

                Me.txtSearch.SelectAll()
                Me.txtSearch.Focus()
                Return

            End If
            Me.axResult.QueryMOde = False

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

            '조회된 내역 존재 --> 이전 BcNo로 남김, miDelCnt 초기화
            If Me.txtBcNo.Text <> "" And Me.txtBcNo.AccessibleName <> rsBcNo Then
                Me.txtBcNo.AccessibleName = rsBcNo
            End If

        End Try
    End Sub
    Public Function fnSpc_info(ByVal rsbcno As String) As DataTable
        Dim sFn As String = "fnSpc_info"

        Try
            Dim dt As DataTable = LISAPP.APP_R.UnifitFn.fnGet_SpcInfo(rsbcno)

            Return dt

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Function


    Private Sub sbDisplay_BcNo_PatInfo(ByVal rsBcNo As String)
        Dim sFn As String = "sbDisplay_BcNo_PatInfo"

        Try
            AxPatInfo.sbDisplay_Init()

            AxPatInfo.BcNo = rsBcNo
            AxPatInfo.fnDisplay_Data()

            Me.txtBcNo.Text = rsBcNo

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Clear()
        Dim sFn As String = "sbDisplay_Clear"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            '이전 검체번호 저장 초기화
            Me.txtBcNo.AccessibleName = ""

            Me.lblTclsCd.Text = ""

            sbDisplayInit_PatInfo()

            Me.AxPatInfo.sbDisplay_Init()
            Me.axResult.sbDisplay_Init("ALL")

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Last()
        Dim sFn As String = "sbDisplay_Last"

        Try
            'Last Tab
            Dim strTmp As String = ""

            strTmp = COMMON.CommXML.getOneElementXML(msTCLSDir, msTABFile, "TAB")
            If IsNumeric(strTmp) Then
                Me.tbcOpt.SelectedIndex = Convert.ToInt16(strTmp)
            End If

            strTmp = COMMON.CommXML.getOneElementXML(msTCLSDir, msSLIPFile, "SLIP")
            If IsNumeric(strTmp) Then
                Me.cboSlip.SelectedIndex = Convert.ToInt16(strTmp)
            Else
                Me.cboSlip.SelectedIndex = 0
            End If

            strTmp = COMMON.CommXML.getOneElementXML(msTCLSDir, msWGFile, "WKGRP")

            If IsNumeric(strTmp) Then
                If Convert.ToInt16(strTmp) < cboWkGrp.Items.Count Then
                    Me.cboWkGrp.SelectedIndex = Convert.ToInt16(strTmp)
                End If
            Else
                If Me.cboWkGrp.Items.Count > 0 Then Me.cboWkGrp.SelectedIndex = 0
            End If

            ''Last TGrpCd
            'strTmp = COMMON.CommXML.getOneElementXML(msTCLSDir, msTGFile, "TGRPCD")

            'If strTmp <> "" Then
            '    Me.lblTgrp.Text = strTmp
            'End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Search(ByVal rsRstFlg As String)
        Dim sFn As String = "sbDisplay_Search"

        Dim dt As New DataTable

        Try
            Me.AxPatInfo.sbDisplay_Init()
            Me.axResult.sbDisplay_Init("ALL")

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            Select Case Me.tbcOpt.SelectedTab.Text
                Case "검사분야별"

                    'dt = LISAPP.APP_M.CommFn.fnGet_SpcList_tk(Ctrl.Get_Code(cboSlip), Ctrl.Get_Code(Me.lblTgrp), Me.dtpTkS.Text.Replace("-", ""), Me.dtpTkE.Text.Replace("-", ""), rsRstFlg)
                    dt = LISAPP.APP_M.CommFn.fnGet_SpcList_tk(Ctrl.Get_Code(cboSlip), Ctrl.Get_Code(Me.lblTgrp), Me.dtpTkS.Text.Replace("-", ""), Me.dtpTkE.Text.Replace("-", ""), rsRstFlg, msSearCh_mode)

                    Dim aa As Integer = dt.Rows.Count
                Case "작업번호별"
                    Dim sWGrpCd As String = Ctrl.Get_Code(Me.cboWkGrp)

                    If sWGrpCd.Length < 2 Then
                        MsgBox("작업그룹 코드가 없습니다. 확인하여 주십시요!!")
                        Return
                    End If

                    Dim sWkYmd As String = Me.dtpWkDt.Text.Replace("-", "").PadRight(8, "0"c)
                    Dim sWkNoS As String = ""
                    Dim sWkNoE As String = ""

                    If Me.txtWkNoS.Text = "" Then
                        sWkNoS = "0000"
                    Else
                        If IsNumeric(Me.txtWkNoS.Text) Then
                            sWkNoS = Me.txtWkNoS.Text.PadLeft(4, "0"c)
                        Else
                            MsgBox("작업번호에 숫자를 입력하여 주십시요!!")
                            Return
                        End If
                    End If

                    If Me.txtWkNoE.Text = "" Then
                        sWkNoE = "9999"
                    Else
                        If IsNumeric(Me.txtWkNoE.Text) Then
                            sWkNoE = Me.txtWkNoE.Text.PadLeft(4, "0"c)
                        Else
                            MsgBox("작업번호에 숫자를 입력하여 주십시요!!")
                            Return
                        End If
                    End If

                    dt = LISAPP.APP_M.CommFn.fnGet_SpcList_wgrp(sWkYmd, Ctrl.Get_Code(cboWkGrp), sWkNoS, sWkNoE, rsRstFlg)

                Case "W/L"
                    Dim sWLUid As String = Me.cboWL.Text.Split("|"c)(2)
                    Dim sWLYmd As String = Me.cboWL.Text.Split("|"c)(1)
                    Dim sWLTitle As String = Me.cboWL.Text.Split("|"c)(0).Trim.Replace("(" + sWLYmd + ")", "")

                    dt = LISAPP.APP_M.CommFn.fnGet_SpcList_wl(sWLUid, sWLYmd, sWLTitle, rsRstFlg)

                    If dt.Rows.Count = 0 Then
                        MsgBox("조회할 자료가 없습니다.")
                    End If

                Case Else

            End Select

            Ctrl.DisplayAfterSelect(spd, dt)
            sbDisplay_SpcList(dt) '20130910 정선영 추가, reamark 색 표시
            sbDisplay_Search_Color(rsRstFlg, spdList)
            If spd.MaxRows > 0 Then spdList_ClickEvent(spd, New AxFPSpreadADO._DSpreadEvents_ClickEvent(2, 1))

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub
    '<20130910 정선영 추가, remark 있을 경우 색 표시
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
                        Else
                            If r_dt.Columns(ix2 - 1).ColumnName.ToLower() = "rmkyn" Then
                                If r_dt.Rows(ix1).Item(ix2 - 1).ToString.ToUpper = "Y" Then
                                    .Row = ix1 + 1 : .Col = -1 : .ForeColor = Color.Blue
                                End If
                            End If
                        End If
                    Next
                Next
            End With
        End If
    End Sub
    '>
    Private Sub sbDisplay_Search_Color(ByVal rsOpt As String, ByVal r_spd As AxFPSpreadADO.AxfpSpread)
        Dim sFn As String = "sbDisplay_Search_Color"

        '전체인 경우에만 완/미완 색상
        'If Not rsOpt.Substring(0, 1) = "A" Then
        '    Return
        'End If

        'Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            With r_spd
                .ReDraw = False

                For i As Integer = 1 To .MaxRows

                    '<< JJH 검사상태=전체 일때만 타도록 (완/미완 색표시)
                    If rsOpt.Substring(0, 1) = "A" Then
                        Dim sRstFlg As String = Ctrl.Get_Code(r_spd, "rstflg", i)

                        '미완 --> BackColor 변경
                        If sRstFlg = "N" Then
                            .Col = 1 : .Col2 = .MaxCols
                            .Row = i : .Row2 = i
                            .BlockMode = True : .BackColor = Ctrl.color_LightRed : .BlockMode = False
                        End If
                    End If


                    '<< JJH Field, MTB, NTM Color 설정   function  -->  fn_get_afbculture_color
                    'If Ctrl.Get_Code(cboSlip) = "M2" Then
                    Dim sColor As String = Ctrl.Get_Code(r_spd, "color", i)

                    Select Case sColor

                        Case "Field"

                            .Col = 1 : .Col2 = .MaxCols
                            .Row = i : .Row2 = i
                            .BlockMode = True : .BackColor = Color.Yellow : .BlockMode = False

                        Case "MTB"

                            .Col = 1 : .Col2 = .MaxCols
                            .Row = i : .Row2 = i
                            .BlockMode = True : .BackColor = Color.Orange : .BlockMode = False

                        Case "NTM"

                            .Col = 1 : .Col2 = .MaxCols
                            .Row = i : .Row2 = i
                            .BlockMode = True : .BackColor = Color.SkyBlue : .BlockMode = False

                    End Select
                    'End If
                    '>>

                Next
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            r_spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplay_Slip()
        Dim sFn As String = "sbDisplay_Slip"
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Slip_List(Me.dtpTkS.Value.ToShortDateString.Replace("-", ""), , , True)
            Me.cboSlip.Items.Clear()

            'Me.cboSlip.Items.Add("[  ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString().Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString().Trim)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_WkGrp()
        Dim sFn As String = "sbDisplay_WkGrp"

        Dim cbo As System.Windows.Forms.ComboBox = Me.cboWkGrp

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_WKGrp_List(Ctrl.Get_Code(cboSlip))
            Me.cboWkGrp.Items.Clear()

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboWkGrp.Items.Add("[" + dt.Rows(ix).Item("wkgrpcd").ToString().Trim + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString().Trim + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString.Trim)
            Next

            If dt.Rows.Count > 0 Then
                Me.cboWkGrp.Text = cboWkGrp.Items(0).ToString
            Else
                Me.cboWkGrp.Text = ""
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_TGrp()
        Dim sFn As String = "sbDisplay_TGrp"

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List(, True)

            With spdTgrp
                .MaxRows = dt.Rows.Count + 1

                .Row = 1
                .Col = .GetColFromID("tgrpcd") : .Text = "[  ] 전체"

                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 2
                    .Col = .GetColFromID("tgrpcd") : .Text = "[" + dt.Rows(ix).Item("tgrpcd").ToString().Trim + "] " + dt.Rows(ix).Item("tgrpnmd").ToString().Trim
                Next
            End With


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_wl()

        Dim sFn As String = "Sub sbDisplay_wl()"

        Try

            Dim dt As DataTable = LISAPP.APP_WL.Qry.fnGet_wl_title(Ctrl.Get_Code(Me.cboSlip), "--", Me.dtpWLdts.Text.Replace("-", ""), Me.dtpWLdte.Text.Replace("-", ""), Ctrl.Get_Code(Me.cboRstFlg_wl))

            Me.cboWL.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sTmp As String = ""
                sTmp += dt.Rows(ix).Item("wltitle").ToString.Trim + "(" + dt.Rows(ix).Item("wlymd").ToString.Trim + ")" + Space(200) + "|"
                sTmp += dt.Rows(ix).Item("wlymd").ToString.Trim + "|"
                sTmp += dt.Rows(ix).Item("wluid").ToString.Trim + "|"
                sTmp += dt.Rows(ix).Item("wltype").ToString.Trim + "|"

                Me.cboWL.Items.Add(sTmp)
            Next

            If Me.cboWL.Items.Count > 0 Then Me.cboWL.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_Test_wl(ByVal rsWLUId As String, ByVal rsWLYmd As String, ByVal rsWLTitle As String)

        Try
            Dim sTestCds As String = "", sTestNmds As String = ""
            Dim dt As DataTable = LISAPP.APP_WL.Qry.fnGet_wl_testspc(rsWLUId, rsWLYmd, rsWLTitle)

            If dt.Rows.Count < 1 Then Return

            Me.lblTest_wl.Text = "" : Me.lblTest_wl.Tag = ""

            For ix As Integer = 0 To dt.Rows.Count - 1

                Dim sTestCd As String = dt.Rows(ix).Item("testspc").ToString.Trim
                Dim sTnmd As String = dt.Rows(ix).Item("tnmd").ToString.Trim

                If ix > 0 Then
                    sTestCds += "|" : sTestNmds += "|"
                End If

                sTestCds += sTestCd : sTestNmds += sTnmd

            Next

            Me.lblTest_wl.Text = sTestNmds.Replace("|", ",")
            Me.lblTest_wl.Tag = sTestCds + "^" + sTestNmds

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim dtNow As Date = New LISAPP.APP_DB.ServerDateTime().GetDateTime

            Me.txtSearch.Text = ""
            Me.dtpTkE.Value = dtNow
            Me.dtpTkS.Value = Convert.ToDateTime(dtNow.Year.ToString() + "-" + dtNow.Month.ToString() + "-" + "01")
            Me.dtpWkDt.Value = Me.dtpTkS.Value
            Me.dtpWLdts.Value = dtNow
            Me.dtpWLdte.Value = dtNow
            Me.txtWkNoE.Text = ""
            Me.txtWkNoS.Text = ""

            sbDisplayInit_spd()
            sbDisplayInit_PatInfo()

            sbDisplay_Slip()
            sbDisplay_WkGrp()
            sbDisplay_TGrp()

            sbDisplay_Last()
            Me.cboRstFlg.SelectedIndex = 0  ''' 결과상태 전체로 셋팅 

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub sbTabPage_Init()
        Dim sFn As String = "sbTabPage_Init"

        Try
            Me.tbcOpt.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub


    Private Sub sbDisplayInit_PatInfo()
        Dim sFn As String = "sbDisplayInit_PatInfo"

        Try
            Me.AxPatInfo.sbDisplay_Init()
            Me.txtBcNo.Text = ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_spd()
        Dim sFn As String = "sbDisplayInit_spd"

        Try
            Me.spdList.MaxRows = 0

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub


#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub

    Public Sub New(ByVal rsBcNo As String)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        Me.txtSearch.Text = rsBcNo
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
    Friend WithEvents pnlLeft As System.Windows.Forms.Panel
    Friend WithEvents grpRstflg As System.Windows.Forms.GroupBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents tbcOpt As System.Windows.Forms.TabControl
    Friend WithEvents pnlFill As System.Windows.Forms.Panel
    Friend WithEvents tpgInfoBc As System.Windows.Forms.TabPage
    Friend WithEvents tpgInfoWk As System.Windows.Forms.TabPage
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnMove As System.Windows.Forms.Button
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dtpWkDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboWkGrp As System.Windows.Forms.ComboBox
    Friend WithEvents txtWkNoE As System.Windows.Forms.TextBox
    Friend WithEvents txtWkNoS As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGM01))
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
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker13 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems7 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker14 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker15 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems8 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker16 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker17 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems9 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker18 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker19 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems10 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker20 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.pnlLeft = New System.Windows.Forms.Panel()
        Me.cmuRstList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRst = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSearchList = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.tbcOpt = New System.Windows.Forms.TabControl()
        Me.tpgInfoBc = New System.Windows.Forms.TabPage()
        Me.lblTgrp = New System.Windows.Forms.Label()
        Me.spdTgrp = New AxFPSpreadADO.AxfpSpread()
        Me.dtpTkE = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpTkS = New System.Windows.Forms.DateTimePicker()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.tpgInfoWk = New System.Windows.Forms.TabPage()
        Me.txtWkNoE = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtWkNoS = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.dtpWkDt = New System.Windows.Forms.DateTimePicker()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.cboWkGrp = New System.Windows.Forms.ComboBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnQuery_wl = New CButtonLib.CButton()
        Me.cboRstFlg_wl = New System.Windows.Forms.ComboBox()
        Me.dtpWLdte = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTest_wl = New System.Windows.Forms.Label()
        Me.cboWL = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpWLdts = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.grpRstflg = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboRstFlg = New System.Windows.Forms.ComboBox()
        Me.btnQuery = New CButtonLib.CButton()
        Me.grpWkqry = New System.Windows.Forms.GroupBox()
        Me.btnWLDelete = New System.Windows.Forms.Button()
        Me.btnWLUpdate = New System.Windows.Forms.Button()
        Me.btnWLRead = New System.Windows.Forms.Button()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.cboSlip = New System.Windows.Forms.ComboBox()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnRst_Ocs = New System.Windows.Forms.Button()
        Me.pnlFill = New System.Windows.Forms.Panel()
        Me.btnAddmic = New CButtonLib.CButton()
        Me.btnQuery_pat = New System.Windows.Forms.Button()
        Me.btnHistory = New CButtonLib.CButton()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.txtBcNo = New System.Windows.Forms.TextBox()
        Me.axResult = New AxAckResult.AxRstInput_m()
        Me.AxPatInfo = New AxAckResult.AxRstPatInfo()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnReg_err = New System.Windows.Forms.Button()
        Me.lblTclsCd = New System.Windows.Forms.Label()
        Me.btnRst_Clear = New CButtonLib.CButton()
        Me.btnFN = New CButtonLib.CButton()
        Me.btnMW = New CButtonLib.CButton()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.BottomToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.TopToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.RightToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.LeftToolStripPanel = New System.Windows.Forms.ToolStripPanel()
        Me.ContentPanel = New System.Windows.Forms.ToolStripContentPanel()
        Me.pnlLeft.SuspendLayout()
        Me.cmuRstList.SuspendLayout()
        Me.tbcOpt.SuspendLayout()
        Me.tpgInfoBc.SuspendLayout()
        CType(Me.spdTgrp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpgInfoWk.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.grpRstflg.SuspendLayout()
        Me.grpWkqry.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFill.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlLeft
        '
        Me.pnlLeft.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlLeft.ContextMenuStrip = Me.cmuRstList
        Me.pnlLeft.Controls.Add(Me.btnToggle)
        Me.pnlLeft.Controls.Add(Me.tbcOpt)
        Me.pnlLeft.Controls.Add(Me.grpRstflg)
        Me.pnlLeft.Controls.Add(Me.grpWkqry)
        Me.pnlLeft.Controls.Add(Me.lblSearch)
        Me.pnlLeft.Controls.Add(Me.cboSlip)
        Me.pnlLeft.Controls.Add(Me.spdList)
        Me.pnlLeft.Controls.Add(Me.Label39)
        Me.pnlLeft.Controls.Add(Me.txtSearch)
        Me.pnlLeft.Location = New System.Drawing.Point(0, 0)
        Me.pnlLeft.Name = "pnlLeft"
        Me.pnlLeft.Size = New System.Drawing.Size(280, 598)
        Me.pnlLeft.TabIndex = 0
        '
        'cmuRstList
        '
        Me.cmuRstList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRst, Me.mnuSearchList})
        Me.cmuRstList.Name = "ContextMenuStrip1"
        Me.cmuRstList.Size = New System.Drawing.Size(163, 48)
        '
        'mnuRst
        '
        Me.mnuRst.Checked = True
        Me.mnuRst.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuRst.Name = "mnuRst"
        Me.mnuRst.Size = New System.Drawing.Size(162, 22)
        Me.mnuRst.Text = "결과입력 위치"
        '
        'mnuSearchList
        '
        Me.mnuSearchList.Name = "mnuSearchList"
        Me.mnuSearchList.Size = New System.Drawing.Size(162, 22)
        Me.mnuSearchList.Text = "조회리스트 위치"
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(229, 27)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(46, 21)
        Me.btnToggle.TabIndex = 18
        Me.btnToggle.Text = "<->"
        '
        'tbcOpt
        '
        Me.tbcOpt.Controls.Add(Me.tpgInfoBc)
        Me.tbcOpt.Controls.Add(Me.tpgInfoWk)
        Me.tbcOpt.Controls.Add(Me.TabPage1)
        Me.tbcOpt.HotTrack = True
        Me.tbcOpt.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.tbcOpt.ItemSize = New System.Drawing.Size(69, 20)
        Me.tbcOpt.Location = New System.Drawing.Point(2, 50)
        Me.tbcOpt.Margin = New System.Windows.Forms.Padding(0)
        Me.tbcOpt.Name = "tbcOpt"
        Me.tbcOpt.SelectedIndex = 0
        Me.tbcOpt.Size = New System.Drawing.Size(274, 131)
        Me.tbcOpt.TabIndex = 0
        '
        'tpgInfoBc
        '
        Me.tpgInfoBc.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgInfoBc.Controls.Add(Me.lblTgrp)
        Me.tpgInfoBc.Controls.Add(Me.spdTgrp)
        Me.tpgInfoBc.Controls.Add(Me.dtpTkE)
        Me.tpgInfoBc.Controls.Add(Me.Label14)
        Me.tpgInfoBc.Controls.Add(Me.dtpTkS)
        Me.tpgInfoBc.Controls.Add(Me.Label32)
        Me.tpgInfoBc.Location = New System.Drawing.Point(4, 24)
        Me.tpgInfoBc.Margin = New System.Windows.Forms.Padding(0)
        Me.tpgInfoBc.Name = "tpgInfoBc"
        Me.tpgInfoBc.Size = New System.Drawing.Size(266, 103)
        Me.tpgInfoBc.TabIndex = 0
        Me.tpgInfoBc.Text = "검사분야별"
        Me.tpgInfoBc.UseVisualStyleBackColor = True
        '
        'lblTgrp
        '
        Me.lblTgrp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTgrp.BackColor = System.Drawing.Color.Thistle
        Me.lblTgrp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTgrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTgrp.ForeColor = System.Drawing.Color.Brown
        Me.lblTgrp.Location = New System.Drawing.Point(5, 28)
        Me.lblTgrp.Name = "lblTgrp"
        Me.lblTgrp.Size = New System.Drawing.Size(33, 21)
        Me.lblTgrp.TabIndex = 105
        Me.lblTgrp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdTgrp
        '
        Me.spdTgrp.DataSource = Nothing
        Me.spdTgrp.Location = New System.Drawing.Point(4, 28)
        Me.spdTgrp.Name = "spdTgrp"
        Me.spdTgrp.OcxState = CType(resources.GetObject("spdTgrp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTgrp.Size = New System.Drawing.Size(259, 73)
        Me.spdTgrp.TabIndex = 24
        '
        'dtpTkE
        '
        Me.dtpTkE.CustomFormat = "yyyy-MM"
        Me.dtpTkE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkE.Location = New System.Drawing.Point(177, 4)
        Me.dtpTkE.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpTkE.Name = "dtpTkE"
        Me.dtpTkE.Size = New System.Drawing.Size(86, 21)
        Me.dtpTkE.TabIndex = 22
        Me.dtpTkE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(3, 4)
        Me.Label14.Margin = New System.Windows.Forms.Padding(0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 21)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "접수일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpTkS
        '
        Me.dtpTkS.CustomFormat = "yyyy-MM"
        Me.dtpTkS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkS.Location = New System.Drawing.Point(76, 4)
        Me.dtpTkS.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpTkS.Name = "dtpTkS"
        Me.dtpTkS.Size = New System.Drawing.Size(86, 21)
        Me.dtpTkS.TabIndex = 20
        Me.dtpTkS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(164, 8)
        Me.Label32.Margin = New System.Windows.Forms.Padding(0)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(14, 12)
        Me.Label32.TabIndex = 23
        Me.Label32.Text = "~"
        '
        'tpgInfoWk
        '
        Me.tpgInfoWk.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgInfoWk.Controls.Add(Me.txtWkNoE)
        Me.tpgInfoWk.Controls.Add(Me.Label43)
        Me.tpgInfoWk.Controls.Add(Me.txtWkNoS)
        Me.tpgInfoWk.Controls.Add(Me.Label45)
        Me.tpgInfoWk.Controls.Add(Me.dtpWkDt)
        Me.tpgInfoWk.Controls.Add(Me.Label44)
        Me.tpgInfoWk.Controls.Add(Me.cboWkGrp)
        Me.tpgInfoWk.Controls.Add(Me.Label46)
        Me.tpgInfoWk.Location = New System.Drawing.Point(4, 24)
        Me.tpgInfoWk.Margin = New System.Windows.Forms.Padding(0)
        Me.tpgInfoWk.Name = "tpgInfoWk"
        Me.tpgInfoWk.Size = New System.Drawing.Size(266, 103)
        Me.tpgInfoWk.TabIndex = 1
        Me.tpgInfoWk.Text = "작업번호별"
        Me.tpgInfoWk.UseVisualStyleBackColor = True
        Me.tpgInfoWk.Visible = False
        '
        'txtWkNoE
        '
        Me.txtWkNoE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoE.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoE.Location = New System.Drawing.Point(179, 68)
        Me.txtWkNoE.MaxLength = 4
        Me.txtWkNoE.Name = "txtWkNoE"
        Me.txtWkNoE.Size = New System.Drawing.Size(83, 21)
        Me.txtWkNoE.TabIndex = 17
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label43.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.White
        Me.Label43.Location = New System.Drawing.Point(3, 40)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(72, 22)
        Me.Label43.TabIndex = 14
        Me.Label43.Text = "작업일자"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWkNoS
        '
        Me.txtWkNoS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoS.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoS.Location = New System.Drawing.Point(76, 68)
        Me.txtWkNoS.MaxLength = 4
        Me.txtWkNoS.Name = "txtWkNoS"
        Me.txtWkNoS.Size = New System.Drawing.Size(84, 21)
        Me.txtWkNoS.TabIndex = 16
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label45.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.White
        Me.Label45.Location = New System.Drawing.Point(3, 68)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(72, 22)
        Me.Label45.TabIndex = 15
        Me.Label45.Text = "작업번호"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpWkDt
        '
        Me.dtpWkDt.CustomFormat = "yyyy-MM"
        Me.dtpWkDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWkDt.Location = New System.Drawing.Point(76, 41)
        Me.dtpWkDt.Name = "dtpWkDt"
        Me.dtpWkDt.Size = New System.Drawing.Size(85, 21)
        Me.dtpWkDt.TabIndex = 13
        Me.dtpWkDt.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(163, 73)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(14, 12)
        Me.Label44.TabIndex = 18
        Me.Label44.Text = "~"
        '
        'cboWkGrp
        '
        Me.cboWkGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWkGrp.Location = New System.Drawing.Point(76, 12)
        Me.cboWkGrp.Name = "cboWkGrp"
        Me.cboWkGrp.Size = New System.Drawing.Size(185, 20)
        Me.cboWkGrp.TabIndex = 86
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label46.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.White
        Me.Label46.Location = New System.Drawing.Point(3, 12)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(72, 22)
        Me.Label46.TabIndex = 85
        Me.Label46.Text = "작업그룹"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnQuery_wl)
        Me.TabPage1.Controls.Add(Me.cboRstFlg_wl)
        Me.TabPage1.Controls.Add(Me.dtpWLdte)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.lblTest_wl)
        Me.TabPage1.Controls.Add(Me.cboWL)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.dtpWLdts)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(266, 103)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "W/L"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnQuery_wl
        '
        Me.btnQuery_wl.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery_wl.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery_wl.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery_wl.ColorFillBlend = CBlendItems1
        Me.btnQuery_wl.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery_wl.Corners.All = CType(6, Short)
        Me.btnQuery_wl.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery_wl.Corners.LowerRight = CType(6, Short)
        Me.btnQuery_wl.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery_wl.Corners.UpperRight = CType(6, Short)
        Me.btnQuery_wl.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery_wl.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery_wl.FocalPoints.CenterPtX = 0.5147059!
        Me.btnQuery_wl.FocalPoints.CenterPtY = 0.0!
        Me.btnQuery_wl.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery_wl.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery_wl.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery_wl.Image = Nothing
        Me.btnQuery_wl.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery_wl.ImageIndex = 0
        Me.btnQuery_wl.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery_wl.Location = New System.Drawing.Point(195, 27)
        Me.btnQuery_wl.Name = "btnQuery_wl"
        Me.btnQuery_wl.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery_wl.SideImage = Nothing
        Me.btnQuery_wl.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery_wl.Size = New System.Drawing.Size(68, 22)
        Me.btnQuery_wl.TabIndex = 148
        Me.btnQuery_wl.Text = "조회"
        Me.btnQuery_wl.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery_wl.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'cboRstFlg_wl
        '
        Me.cboRstFlg_wl.AutoCompleteCustomSource.AddRange(New String() {"[ ] 전체", "[N] 미완료", "[F] 완료"})
        Me.cboRstFlg_wl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRstFlg_wl.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboRstFlg_wl.Items.AddRange(New Object() {"[ ] 전체 ", "[N] 미완료", "[F] 완료"})
        Me.cboRstFlg_wl.Location = New System.Drawing.Point(75, 28)
        Me.cboRstFlg_wl.MaxDropDownItems = 10
        Me.cboRstFlg_wl.Name = "cboRstFlg_wl"
        Me.cboRstFlg_wl.Size = New System.Drawing.Size(117, 20)
        Me.cboRstFlg_wl.TabIndex = 147
        '
        'dtpWLdte
        '
        Me.dtpWLdte.CustomFormat = "yyyy-MM-dd"
        Me.dtpWLdte.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpWLdte.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWLdte.Location = New System.Drawing.Point(178, 5)
        Me.dtpWLdte.Name = "dtpWLdte"
        Me.dtpWLdte.Size = New System.Drawing.Size(86, 21)
        Me.dtpWLdte.TabIndex = 146
        Me.dtpWLdte.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(162, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(14, 12)
        Me.Label10.TabIndex = 145
        Me.Label10.Text = "~"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 21)
        Me.Label1.TabIndex = 144
        Me.Label1.Text = "구    분"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTest_wl
        '
        Me.lblTest_wl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTest_wl.BackColor = System.Drawing.Color.Thistle
        Me.lblTest_wl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTest_wl.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest_wl.ForeColor = System.Drawing.Color.Brown
        Me.lblTest_wl.Location = New System.Drawing.Point(4, 71)
        Me.lblTest_wl.Name = "lblTest_wl"
        Me.lblTest_wl.Size = New System.Drawing.Size(259, 29)
        Me.lblTest_wl.TabIndex = 133
        '
        'cboWL
        '
        Me.cboWL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWL.Items.AddRange(New Object() {"자동화계", "특수계1", "툭스계2"})
        Me.cboWL.Location = New System.Drawing.Point(75, 49)
        Me.cboWL.MaxDropDownItems = 10
        Me.cboWL.Name = "cboWL"
        Me.cboWL.Size = New System.Drawing.Size(186, 20)
        Me.cboWL.TabIndex = 132
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(4, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 21)
        Me.Label9.TabIndex = 131
        Me.Label9.Text = "W/L 제목"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpWLdts
        '
        Me.dtpWLdts.CustomFormat = "yyyy-MM-dd"
        Me.dtpWLdts.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpWLdts.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWLdts.Location = New System.Drawing.Point(75, 5)
        Me.dtpWLdts.Name = "dtpWLdts"
        Me.dtpWLdts.Size = New System.Drawing.Size(86, 21)
        Me.dtpWLdts.TabIndex = 130
        Me.dtpWLdts.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(4, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 21)
        Me.Label8.TabIndex = 129
        Me.Label8.Text = "W/L 일자"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpRstflg
        '
        Me.grpRstflg.Controls.Add(Me.Label3)
        Me.grpRstflg.Controls.Add(Me.cboRstFlg)
        Me.grpRstflg.Controls.Add(Me.btnQuery)
        Me.grpRstflg.Location = New System.Drawing.Point(3, 174)
        Me.grpRstflg.Name = "grpRstflg"
        Me.grpRstflg.Size = New System.Drawing.Size(273, 40)
        Me.grpRstflg.TabIndex = 1
        Me.grpRstflg.TabStop = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(5, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 20)
        Me.Label3.TabIndex = 139
        Me.Label3.Text = "검사상태"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboRstFlg
        '
        Me.cboRstFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRstFlg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboRstFlg.FormattingEnabled = True
        Me.cboRstFlg.Items.AddRange(New Object() {"[A] 전체", "[4] 미최종보고", "[3] 최종보고", "[2] 중간보고", "[1] 예비결과", "[0] 미결과"})
        Me.cboRstFlg.Location = New System.Drawing.Point(79, 14)
        Me.cboRstFlg.Name = "cboRstFlg"
        Me.cboRstFlg.Size = New System.Drawing.Size(112, 20)
        Me.cboRstFlg.TabIndex = 138
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems2.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems2
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 1.0!
        Me.btnQuery.FocalPoints.CenterPtY = 0.8181818!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker4
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(198, 14)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(68, 22)
        Me.btnQuery.TabIndex = 142
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'grpWkqry
        '
        Me.grpWkqry.Controls.Add(Me.btnWLDelete)
        Me.grpWkqry.Controls.Add(Me.btnWLUpdate)
        Me.grpWkqry.Controls.Add(Me.btnWLRead)
        Me.grpWkqry.Location = New System.Drawing.Point(3, 174)
        Me.grpWkqry.Name = "grpWkqry"
        Me.grpWkqry.Size = New System.Drawing.Size(273, 40)
        Me.grpWkqry.TabIndex = 6
        Me.grpWkqry.TabStop = False
        '
        'btnWLDelete
        '
        Me.btnWLDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWLDelete.Location = New System.Drawing.Point(192, 12)
        Me.btnWLDelete.Name = "btnWLDelete"
        Me.btnWLDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnWLDelete.TabIndex = 123
        Me.btnWLDelete.Text = "WL 삭제"
        '
        'btnWLUpdate
        '
        Me.btnWLUpdate.Enabled = False
        Me.btnWLUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWLUpdate.Location = New System.Drawing.Point(104, 12)
        Me.btnWLUpdate.Name = "btnWLUpdate"
        Me.btnWLUpdate.Size = New System.Drawing.Size(60, 22)
        Me.btnWLUpdate.TabIndex = 122
        Me.btnWLUpdate.Text = "WL 수정"
        '
        'btnWLRead
        '
        Me.btnWLRead.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWLRead.Location = New System.Drawing.Point(20, 12)
        Me.btnWLRead.Name = "btnWLRead"
        Me.btnWLRead.Size = New System.Drawing.Size(60, 22)
        Me.btnWLRead.TabIndex = 121
        Me.btnWLRead.Text = "WL 조회"
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(5, 27)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(80, 21)
        Me.lblSearch.TabIndex = 17
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Location = New System.Drawing.Point(86, 5)
        Me.cboSlip.Margin = New System.Windows.Forms.Padding(0)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(188, 20)
        Me.cboSlip.TabIndex = 89
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(3, 216)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(275, 378)
        Me.spdList.TabIndex = 2
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label39.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.White
        Me.Label39.Location = New System.Drawing.Point(5, 5)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(80, 21)
        Me.Label39.TabIndex = 88
        Me.Label39.Text = "검사분야"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSearch.Location = New System.Drawing.Point(86, 27)
        Me.txtSearch.MaxLength = 18
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(142, 21)
        Me.txtSearch.TabIndex = 16
        '
        'btnRst_Ocs
        '
        Me.btnRst_Ocs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRst_Ocs.Location = New System.Drawing.Point(2, 3)
        Me.btnRst_Ocs.Margin = New System.Windows.Forms.Padding(1)
        Me.btnRst_Ocs.Name = "btnRst_Ocs"
        Me.btnRst_Ocs.Size = New System.Drawing.Size(75, 26)
        Me.btnRst_Ocs.TabIndex = 96
        Me.btnRst_Ocs.Text = "OCS"
        Me.btnRst_Ocs.UseVisualStyleBackColor = True
        Me.btnRst_Ocs.Visible = False
        '
        'pnlFill
        '
        Me.pnlFill.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFill.Controls.Add(Me.btnAddmic)
        Me.pnlFill.Controls.Add(Me.btnQuery_pat)
        Me.pnlFill.Controls.Add(Me.btnHistory)
        Me.pnlFill.Controls.Add(Me.btnDown)
        Me.pnlFill.Controls.Add(Me.btnUp)
        Me.pnlFill.Controls.Add(Me.txtBcNo)
        Me.pnlFill.Controls.Add(Me.axResult)
        Me.pnlFill.Controls.Add(Me.AxPatInfo)
        Me.pnlFill.Location = New System.Drawing.Point(288, 0)
        Me.pnlFill.Name = "pnlFill"
        Me.pnlFill.Size = New System.Drawing.Size(969, 598)
        Me.pnlFill.TabIndex = 0
        '
        'btnAddmic
        '
        Me.btnAddmic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddmic.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAddmic.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAddmic.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems3.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnAddmic.ColorFillBlend = CBlendItems3
        Me.btnAddmic.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnAddmic.Corners.All = CType(6, Short)
        Me.btnAddmic.Corners.LowerLeft = CType(6, Short)
        Me.btnAddmic.Corners.LowerRight = CType(6, Short)
        Me.btnAddmic.Corners.UpperLeft = CType(6, Short)
        Me.btnAddmic.Corners.UpperRight = CType(6, Short)
        Me.btnAddmic.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnAddmic.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnAddmic.FocalPoints.CenterPtX = 1.0!
        Me.btnAddmic.FocalPoints.CenterPtY = 0.7272727!
        Me.btnAddmic.FocalPoints.FocusPtX = 0.0!
        Me.btnAddmic.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnAddmic.FocusPtTracker = DesignerRectTracker6
        Me.btnAddmic.Image = Nothing
        Me.btnAddmic.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnAddmic.ImageIndex = 0
        Me.btnAddmic.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnAddmic.Location = New System.Drawing.Point(247, 118)
        Me.btnAddmic.Name = "btnAddmic"
        Me.btnAddmic.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnAddmic.SideImage = Nothing
        Me.btnAddmic.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnAddmic.Size = New System.Drawing.Size(76, 22)
        Me.btnAddmic.TabIndex = 221
        Me.btnAddmic.Text = "추가처방"
        Me.btnAddmic.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnAddmic.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery_pat
        '
        Me.btnQuery_pat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuery_pat.Location = New System.Drawing.Point(875, 117)
        Me.btnQuery_pat.Name = "btnQuery_pat"
        Me.btnQuery_pat.Size = New System.Drawing.Size(93, 22)
        Me.btnQuery_pat.TabIndex = 220
        Me.btnQuery_pat.TabStop = False
        Me.btnQuery_pat.Text = "환자진단조회"
        Me.btnQuery_pat.UseVisualStyleBackColor = True
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnHistory.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHistory.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems4.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnHistory.ColorFillBlend = CBlendItems4
        Me.btnHistory.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnHistory.Corners.All = CType(6, Short)
        Me.btnHistory.Corners.LowerLeft = CType(6, Short)
        Me.btnHistory.Corners.LowerRight = CType(6, Short)
        Me.btnHistory.Corners.UpperLeft = CType(6, Short)
        Me.btnHistory.Corners.UpperRight = CType(6, Short)
        Me.btnHistory.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnHistory.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnHistory.FocalPoints.CenterPtX = 1.0!
        Me.btnHistory.FocalPoints.CenterPtY = 0.7272727!
        Me.btnHistory.FocalPoints.FocusPtX = 0.0!
        Me.btnHistory.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHistory.FocusPtTracker = DesignerRectTracker8
        Me.btnHistory.Image = Nothing
        Me.btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnHistory.ImageIndex = 0
        Me.btnHistory.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnHistory.Location = New System.Drawing.Point(401, 117)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnHistory.SideImage = Nothing
        Me.btnHistory.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnHistory.Size = New System.Drawing.Size(91, 23)
        Me.btnHistory.TabIndex = 195
        Me.btnHistory.Text = "누적결과조회"
        Me.btnHistory.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnHistory.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnDown
        '
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Font = New System.Drawing.Font("굴림", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDown.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnDown.Location = New System.Drawing.Point(921, 60)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(45, 52)
        Me.btnDown.TabIndex = 174
        Me.btnDown.Text = "▼"
        '
        'btnUp
        '
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("굴림", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUp.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnUp.Location = New System.Drawing.Point(921, 4)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(45, 52)
        Me.btnUp.TabIndex = 173
        Me.btnUp.Text = "▲"
        '
        'txtBcNo
        '
        Me.txtBcNo.BackColor = System.Drawing.Color.White
        Me.txtBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcNo.Location = New System.Drawing.Point(126, 115)
        Me.txtBcNo.MaxLength = 18
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(117, 21)
        Me.txtBcNo.TabIndex = 172
        Me.txtBcNo.Text = "20050301-M0-0001-0"
        Me.txtBcNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtBcNo.Visible = False
        '
        'axResult
        '
        Me.axResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axResult.BcNoAll = False
        Me.axResult.ColHiddenYn = False
        Me.axResult.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axResult.Location = New System.Drawing.Point(3, 118)
        Me.axResult.Name = "axResult"
        Me.axResult.Size = New System.Drawing.Size(965, 477)
        Me.axResult.TabIndex = 176
        '
        'AxPatInfo
        '
        Me.AxPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AxPatInfo.BcNo = ""
        Me.AxPatInfo.Location = New System.Drawing.Point(2, 1)
        Me.AxPatInfo.Name = "AxPatInfo"
        Me.AxPatInfo.RegNo = ""
        Me.AxPatInfo.Size = New System.Drawing.Size(918, 114)
        Me.AxPatInfo.SlipCd = ""
        Me.AxPatInfo.TabIndex = 177
        '
        'Panel1
        '
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnReg_err)
        Me.Panel1.Controls.Add(Me.btnRst_Ocs)
        Me.Panel1.Controls.Add(Me.lblTclsCd)
        Me.Panel1.Controls.Add(Me.btnRst_Clear)
        Me.Panel1.Controls.Add(Me.btnFN)
        Me.Panel1.Controls.Add(Me.btnMW)
        Me.Panel1.Controls.Add(Me.btnReg)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 597)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1259, 32)
        Me.Panel1.TabIndex = 3
        '
        'btnReg_err
        '
        Me.btnReg_err.Location = New System.Drawing.Point(76, 3)
        Me.btnReg_err.Name = "btnReg_err"
        Me.btnReg_err.Size = New System.Drawing.Size(75, 26)
        Me.btnReg_err.TabIndex = 195
        Me.btnReg_err.TabStop = False
        Me.btnReg_err.Text = "오류"
        Me.btnReg_err.UseVisualStyleBackColor = True
        Me.btnReg_err.Visible = False
        '
        'lblTclsCd
        '
        Me.lblTclsCd.AutoSize = True
        Me.lblTclsCd.Location = New System.Drawing.Point(319, 10)
        Me.lblTclsCd.Name = "lblTclsCd"
        Me.lblTclsCd.Size = New System.Drawing.Size(0, 12)
        Me.lblTclsCd.TabIndex = 109
        Me.lblTclsCd.Visible = False
        '
        'btnRst_Clear
        '
        Me.btnRst_Clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRst_Clear.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnRst_Clear.ColorFillBlend = CBlendItems5
        Me.btnRst_Clear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnRst_Clear.Corners.All = CType(6, Short)
        Me.btnRst_Clear.Corners.LowerLeft = CType(6, Short)
        Me.btnRst_Clear.Corners.LowerRight = CType(6, Short)
        Me.btnRst_Clear.Corners.UpperLeft = CType(6, Short)
        Me.btnRst_Clear.Corners.UpperRight = CType(6, Short)
        Me.btnRst_Clear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnRst_Clear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnRst_Clear.FocalPoints.CenterPtX = 0.5!
        Me.btnRst_Clear.FocalPoints.CenterPtY = 0.0!
        Me.btnRst_Clear.FocalPoints.FocusPtX = 0.0!
        Me.btnRst_Clear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRst_Clear.FocusPtTracker = DesignerRectTracker10
        Me.btnRst_Clear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnRst_Clear.ForeColor = System.Drawing.Color.White
        Me.btnRst_Clear.Image = Nothing
        Me.btnRst_Clear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRst_Clear.ImageIndex = 0
        Me.btnRst_Clear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnRst_Clear.Location = New System.Drawing.Point(670, 3)
        Me.btnRst_Clear.Name = "btnRst_Clear"
        Me.btnRst_Clear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnRst_Clear.SideImage = Nothing
        Me.btnRst_Clear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnRst_Clear.Size = New System.Drawing.Size(96, 25)
        Me.btnRst_Clear.TabIndex = 194
        Me.btnRst_Clear.Text = "결과소거"
        Me.btnRst_Clear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnRst_Clear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnFN
        '
        Me.btnFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnFN.ColorFillBlend = CBlendItems6
        Me.btnFN.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnFN.Corners.All = CType(6, Short)
        Me.btnFN.Corners.LowerLeft = CType(6, Short)
        Me.btnFN.Corners.LowerRight = CType(6, Short)
        Me.btnFN.Corners.UpperLeft = CType(6, Short)
        Me.btnFN.Corners.UpperRight = CType(6, Short)
        Me.btnFN.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnFN.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnFN.FocalPoints.CenterPtX = 0.5!
        Me.btnFN.FocalPoints.CenterPtY = 0.0!
        Me.btnFN.FocalPoints.FocusPtX = 0.0!
        Me.btnFN.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.FocusPtTracker = DesignerRectTracker12
        Me.btnFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnFN.ForeColor = System.Drawing.Color.White
        Me.btnFN.Image = Nothing
        Me.btnFN.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFN.ImageIndex = 0
        Me.btnFN.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnFN.Location = New System.Drawing.Point(767, 3)
        Me.btnFN.Name = "btnFN"
        Me.btnFN.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnFN.SideImage = Nothing
        Me.btnFN.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnFN.Size = New System.Drawing.Size(96, 25)
        Me.btnFN.TabIndex = 193
        Me.btnFN.Text = "결과검증(F12)"
        Me.btnFN.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnFN.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnMW
        '
        Me.btnMW.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker13.IsActive = False
        DesignerRectTracker13.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker13.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnMW.CenterPtTracker = DesignerRectTracker13
        CBlendItems7.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems7.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnMW.ColorFillBlend = CBlendItems7
        Me.btnMW.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnMW.Corners.All = CType(6, Short)
        Me.btnMW.Corners.LowerLeft = CType(6, Short)
        Me.btnMW.Corners.LowerRight = CType(6, Short)
        Me.btnMW.Corners.UpperLeft = CType(6, Short)
        Me.btnMW.Corners.UpperRight = CType(6, Short)
        Me.btnMW.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnMW.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnMW.FocalPoints.CenterPtX = 0.5!
        Me.btnMW.FocalPoints.CenterPtY = 0.0!
        Me.btnMW.FocalPoints.FocusPtX = 0.0!
        Me.btnMW.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker14.IsActive = False
        DesignerRectTracker14.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker14.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnMW.FocusPtTracker = DesignerRectTracker14
        Me.btnMW.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnMW.ForeColor = System.Drawing.Color.White
        Me.btnMW.Image = Nothing
        Me.btnMW.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnMW.ImageIndex = 0
        Me.btnMW.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnMW.Location = New System.Drawing.Point(864, 3)
        Me.btnMW.Name = "btnMW"
        Me.btnMW.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnMW.SideImage = Nothing
        Me.btnMW.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnMW.Size = New System.Drawing.Size(96, 25)
        Me.btnMW.TabIndex = 192
        Me.btnMW.Text = "중간보고(F11)"
        Me.btnMW.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnMW.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker15.IsActive = False
        DesignerRectTracker15.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker15.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker15
        CBlendItems8.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems8.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems8
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5!
        Me.btnReg.FocalPoints.CenterPtY = 0.0!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker16.IsActive = False
        DesignerRectTracker16.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker16.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker16
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(961, 3)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(96, 25)
        Me.btnReg.TabIndex = 191
        Me.btnReg.Text = "결과저장(F9)"
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker17.IsActive = False
        DesignerRectTracker17.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker17.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker17
        CBlendItems9.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems9.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems9
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4123711!
        Me.btnClear.FocalPoints.CenterPtY = 0.24!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker18.IsActive = False
        DesignerRectTracker18.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker18.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker18
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1058, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(97, 25)
        Me.btnClear.TabIndex = 190
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker19.IsActive = False
        DesignerRectTracker19.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker19.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker19
        CBlendItems10.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems10.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems10
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
        DesignerRectTracker20.IsActive = False
        DesignerRectTracker20.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker20.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker20
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1156, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(93, 25)
        Me.btnExit.TabIndex = 189
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnMove
        '
        Me.btnMove.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMove.BackColor = System.Drawing.Color.Lavender
        Me.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMove.Location = New System.Drawing.Point(281, 262)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(8, 74)
        Me.btnMove.TabIndex = 3
        Me.btnMove.Text = "◀"
        Me.btnMove.UseVisualStyleBackColor = False
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.Color.Transparent
        Me.imgList.Images.SetKeyName(0, "")
        '
        'BottomToolStripPanel
        '
        Me.BottomToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.BottomToolStripPanel.Name = "BottomToolStripPanel"
        Me.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.BottomToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.BottomToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'TopToolStripPanel
        '
        Me.TopToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.TopToolStripPanel.Name = "TopToolStripPanel"
        Me.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.TopToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.TopToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'RightToolStripPanel
        '
        Me.RightToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.RightToolStripPanel.Name = "RightToolStripPanel"
        Me.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.RightToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.RightToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'LeftToolStripPanel
        '
        Me.LeftToolStripPanel.Location = New System.Drawing.Point(0, 0)
        Me.LeftToolStripPanel.Name = "LeftToolStripPanel"
        Me.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        Me.LeftToolStripPanel.RowMargin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.LeftToolStripPanel.Size = New System.Drawing.Size(0, 0)
        '
        'ContentPanel
        '
        Me.ContentPanel.Size = New System.Drawing.Size(150, 175)
        '
        'FGM01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1259, 629)
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.pnlFill)
        Me.Controls.Add(Me.pnlLeft)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FGM01"
        Me.Text = "분야별 결과저장 및 보고 (M)"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlLeft.ResumeLayout(False)
        Me.pnlLeft.PerformLayout()
        Me.cmuRstList.ResumeLayout(False)
        Me.tbcOpt.ResumeLayout(False)
        Me.tpgInfoBc.ResumeLayout(False)
        Me.tpgInfoBc.PerformLayout()
        CType(Me.spdTgrp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpgInfoWk.ResumeLayout(False)
        Me.tpgInfoWk.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.grpRstflg.ResumeLayout(False)
        Me.grpWkqry.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFill.ResumeLayout(False)
        Me.pnlFill.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FGM01_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sFn As String = "FGM01_Load"

        Try
            DS_FormDesige.sbInti(Me)

            Dim sBcNo As String = Me.txtSearch.Text

#If DEBUG Then
            btnReg_err.Visible = True
#End If
            If USER_INFO.USRLVL = "S" Then btnRst_Ocs.Visible = True

            AxPatInfo.UsrLevel = STU_AUTHORITY.UsrID
            AxPatInfo.sbDisplay_Init()

            axResult.Form = Me
            axResult.ColHiddenYn = True
            axResult.sbDisplay_Init("ALL")
            Me.axResult.BcNoAll = CType(IIf(PRG_CONST.RST_BCNO_CHECK = "1", True, False), Boolean)

            sbDisplayInit()

            If sBcNo <> "" Then
                Me.txtSearch.Text = sBcNo
                Me.txtSearch_KeyDown(Nothing, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
            End If

            '< 20121011 로드시 그리드포커스 셋팅

            If mnuRst.Checked = True Then
                Me.axResult.sbFocus()
                Me.axResult.Focus()
            Else
                Me.spdList.Focus()
            End If


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Clear()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click
        If Me.btnMove.Text = "◀" Then
            Me.btnMove.Left = Me.pnlLeft.Left

            Me.pnlLeft.Visible = False
            Me.pnlFill.Left = Me.btnMove.Left + Me.btnMove.Width + 2
            Me.pnlFill.Width += Me.pnlLeft.Width

            Me.Refresh()

            Me.btnMove.Text = "▶"
        Else
            Me.btnMove.Left = 281

            Me.pnlFill.Left = 288
            Me.pnlFill.Width -= Me.pnlLeft.Width

            Me.pnlLeft.Visible = True

            Me.btnMove.Text = "◀"
        End If
    End Sub

    Private Sub sbSearch_Data()

        If Me.lblSearch.Text = "검체번호" Then
            Dim sBcNo As String = Me.txtSearch.Text.Trim.Replace("-", "")

            If sBcNo = "" Then
                MsgBox("검체번호를 입력해 주십시요!!")
                Return
            End If

            '검체번호               : 14 Or 15
            '검체번호바코드(일반)   : 11 Or 12
            '작업번호바코드(미생물) : 10
            Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "").Trim()

            If sBcNo.Length = 14 Then sBcNo = sBcNo + "0"

            If sBcNo.Length = 12 Or sBcNo.Length = 11 Then
                sBcNo = LISAPP.COMM.BcnoFn.fnFind_BcNo(sBcNo)
            End If

            If Not sBcNo.Length = 15 Then
                MsgBox("검체번호에 오류가 발견되었습니다. 확인하여 주십시요!!")

                Me.txtSearch.SelectAll()

                Return
            End If

            '직접 입력 시에는 다시 조회 가능하도록 처리
            Me.txtBcNo.AccessibleName = ""

            sbDisplay_BcNo(sBcNo)

            '조회 후 화면 처리
            If Me.txtBcNo.Text = "" Then
                Me.txtSearch.SelectAll()
                'Else
                '    Me.txtSearch.Text = ""
            End If


        Else
            '등록번호 입력시 처리
            Dim sRegNo As String = Me.txtSearch.Text.Trim()

            If sRegNo = "" Then
                MsgBox("등록번호를 입력해 주십시요!!")
                Return
            End If

            If IsNumeric(sRegNo.Substring(0, 1)) Then
                sRegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                sRegNo = sRegNo.Substring(0, 1).ToUpper + sRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            End If

            Dim dt As New DataTable
            If Me.tbcOpt.SelectedTab.Text = "작업번호별" Then
                dt = LISAPP.APP_M.CommFn.fnGet_SpcList_RegNo(True, Ctrl.Get_Code(cboSlip), "", Ctrl.Get_Code(cboWkGrp), sRegNo)
            Else
                dt = LISAPP.APP_M.CommFn.fnGet_SpcList_RegNo(True, "", Ctrl.Get_Code(Me.lblTgrp), "", sRegNo)
            End If

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            '접수일시에 Desc 정렬 표시
            spd.set_ColUserSortIndicator(spd.GetColFromID("tkdt"), FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorDescending)

            Ctrl.DisplayAfterSelect(spd, dt)

            '조회 후 화면 처리
            If spd.MaxRows = 0 Then
                MsgBox("해당하는 환자가 없습니다!!")
                Me.txtSearch.SelectAll()
                'Else
                '    Me.txtSearch.Text = ""
            End If
        End If
    End Sub

    Private Sub btnFN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFN.Click

        If STU_AUTHORITY.FNReg <> "1" Then
            MsgBox("결과검증 권한이 없습니다.!!  확인하세요.")
            Return
        End If

        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnRst = axResult.fnReg("3")
        If blnRst Then

            'Me.AxPatInfo.sbDisplay_Init()
            'Me.axResult.sbDisplay_Init("ALL")
            Me.axResult.QueryMOde = True
            Me.axResult.sbDisplay_Data(Me.axResult.BCNO)
            Me.txtSearch.Focus()
            Me.axResult.QueryMOde = False

            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)
        End If
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnMW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMW.Click
        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnRst = axResult.fnReg("22")
        If blnRst Then

            'Me.AxPatInfo.sbDisplay_Init()
            'Me.axResult.sbDisplay_Init("ALL")
            Me.axResult.QueryMOde = True
            Me.axResult.sbDisplay_Data(Me.axResult.BCNO)
            Me.txtSearch.Focus()
            Me.axResult.QueryMOde = False
            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)
        End If
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub btnReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnRst = axResult.fnReg("1")
        If blnRst Then

            'Me.AxPatInfo.sbDisplay_Init()
            'Me.axResult.sbDisplay_Init("ALL")
            Me.axResult.QueryMOde = True
            Me.axResult.sbDisplay_Data(Me.axResult.BCNO)
            Me.txtSearch.Focus()
            Me.axResult.QueryMOde = False
            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)
        End If
        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn
        Fn.SearchToggle(Me.lblSearch, Me.btnToggle, enumToggle.BcnoToRegno, Me.txtSearch)
        Me.txtSearch.Focus()
    End Sub

    Private Sub spdList_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        Select Case Convert.ToInt32(e.keyCode)
            Case Keys.PageDown, Keys.PageUp
                e.keyCode = 0
            Case Keys.Enter  '< 20121011 죄측그리드 엔터시 내용조회
                Dim iRow As Integer = spdList.ActiveRow
                spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(2, iRow))

        End Select
    End Sub

    Private Sub spd_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs)  ', spdList.Resize
        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

            With spd
                .ReDraw = False
                .Hide()
                .Show()
                .ReDraw = True
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tbcOpt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcOpt.SelectedIndexChanged
        If Me.tbcOpt.SelectedTab.Text = "" Then Return

        sbDisplayInit_spd()

        COMMON.CommXML.setOneElementXML(msTCLSDir, msTABFile, "TAB", Me.tbcOpt.SelectedIndex.ToString)
    End Sub


    Private Sub txtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Me.txtSearch.SelectionStart = 0
        Me.txtSearch.SelectAll()
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        sbSearch_Data()
        Me.txtSearch.Focus()

    End Sub

    Private Sub btnRst_Clear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRst_Clear.Click

        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnRst = axResult.fnReg_Erase()
        If blnRst Then
            AxPatInfo.sbDisplay_Init()
            axResult.sbDisplay_Init("ALL")
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If e.col < 1 Then Return
        If e.row < 1 Then Return

        With spdList
            .Row = e.row
            .Col = e.col
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell
        End With
        Dim sBcNo As String = Ctrl.Get_Code(Me.spdList, "bcno", e.row).Replace("-", "")

        If sBcNo.Length = 15 Then sbDisplay_BcNo(sBcNo)

        '< 20121011 왼쪽 그리드 조회시 포커스 

        If mnuRst.Checked = True Then
            Me.axResult.sbFocus()
            Me.axResult.Focus()
        Else
            Me.spdList.Focus()
        End If


    End Sub

    Private Sub spdList_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdList.LeaveCell
        'If e.newCol < 1 Then Return
        'If e.newRow < 1 Then Return
        'If e.row = e.newRow Then Return

        'Dim sBcNo As String = Ctrl.Get_Code(Me.spdList, "bcno", e.newRow)

        'If sBcNo = axResult.BCNO.Replace("-", "") Then Return

        'spdList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(e.newCol, e.newRow))

    End Sub

    Private Sub FGM06_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

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

            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnSearch_Click(Nothing, Nothing)

            Case Keys.F9
                btnReg_Click(btnReg, New System.EventArgs)
            Case Keys.F11
                btnMW_Click(btnMW, New System.EventArgs)
            Case Keys.F12
                btnFN_Click(btnFN, New System.EventArgs)
            Case Keys.Escape
                If axResult.pnlCode.Visible Then axResult.pnlCode.Visible = False : Return

                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        If cboWkGrp.SelectedIndex < 0 Then Exit Sub

        Dim strWkNoGbn As String = cboWkGrp.Text.Split("|"c)(1)

        Select Case strWkNoGbn
            Case "2"
                dtpWkDt.CustomFormat = "yyyy-MM"
            Case "3"
                dtpWkDt.CustomFormat = "yyyy"
            Case Else
                dtpWkDt.CustomFormat = "yyyy-MM-dd"
        End Select

        COMMON.CommXML.setOneElementXML(msTCLSDir, msWGFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)


    End Sub

    Private Sub sbDisplay_List(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable)
        Dim sFn As String = "sbDisplay_List(AxFPSpreadADO,DataTable)"

        Try
            With r_spd
                If r_dt Is Nothing Then
                    .MaxRows = 0

                    Return
                End If

                .MaxRows = 0

                .ReDraw = False

                .MaxRows = r_dt.Rows.Count

                For i As Integer = 1 To r_dt.Rows.Count
                    For j As Integer = 1 To r_dt.Columns.Count
                        Dim iCol As Integer = .GetColFromID(r_dt.Columns(j - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i
                            If r_dt.Columns(j - 1).ColumnName.ToLower() = "r" Then
                                If r_dt.Rows(i - 1).Item(j - 1).ToString() = "3" Then
                                    .Text = FixedVariable.gsRstFlagF
                                    .ForeColor = FixedVariable.g_color_FN
                                ElseIf r_dt.Rows(i - 1).Item(j - 1).ToString() = "2" Then
                                    .Text = FixedVariable.gsRstFlagM
                                ElseIf r_dt.Rows(i - 1).Item(j - 1).ToString() = "1" Then
                                    .Text = FixedVariable.gsRstFlagR
                                End If
                            Else
                                .Text = r_dt.Rows(i - 1).Item(j - 1).ToString()
                            End If
                            .CellTag = r_dt.Rows(i - 1).Item(j - 1).ToString()
                        End If
                    Next
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub



    Public Overridable Sub sbDisplayInit_spdList()
        Dim sFn As String = "Sub DisplayInit_spdList"

        '> Form Load 후 [기존 리스트에 추가] 체크 시 Skip
        'If mbLoaded And Me.chkAddList.Checked Then Return

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            With spd
                .Col = .GetColFromID("workno0")
                .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .Col = .GetColFromID("bcno0")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("regno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("patnm")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("sexage")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("deptcd")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("wardno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("srcd")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("doctornm")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("orddt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("tkdt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("itemcnt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("spccd")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("spcnmd")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .MaxRows = 0
            End With

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

        Finally
            'Me.txtRowKey.Text = ""

        End Try
    End Sub

    Private Sub btnHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistory.Click

        Dim sRegNo As String = AxPatInfo.RegNo
        Dim sPartSlip As String = Ctrl.Get_Code(Me.cboSlip)

        Dim frm As Windows.Forms.Form
        frm = New LISV.FGRV14(sRegNo, "", "", sPartSlip, True)

        'frm.MdiParent = Me.MdiParent
        frm.WindowState = Windows.Forms.FormWindowState.Maximized
        frm.Text = "누적결과조회(결과)"
        frm.Activate()
        frm.ShowDialog()

    End Sub

    Private Sub txtBcNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBcNo.Click
        Me.txtBcNo.SelectAll()
    End Sub

    Private Sub txtBcNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBcNo.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If Not Me.lblSearch.Text = "검체번호" Then btnToggle_Click(Me.btnToggle, Nothing)

            Me.txtSearch.Text = Me.txtBcNo.Text

            sbSearch_Data()

            Me.txtBcNo.SelectAll()
            Me.txtBcNo.Focus()
        End If
    End Sub

    Private Sub btnUpDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click, btnDown.Click
        Dim spd As AxFPSpreadADO.AxfpSpread = Nothing

        spd = Me.spdList

        If spd.MaxRows = 0 Then Return

        Dim iNext As Integer = 0

        If CType(sender, Windows.Forms.Button).Name.ToLower.EndsWith("up") Then
            If spd.ActiveRow < 1 Then Return

            iNext -= 1
        Else
            If spd.ActiveRow = spd.MaxRows Then Return

            iNext += 1
        End If

        Me.spdList_ClickEvent(spd, New AxFPSpreadADO._DSpreadEvents_ClickEvent(spd.GetColFromID("workno"), spd.ActiveRow + iNext))

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        If Me.cboRstFlg.SelectedIndex < 0 Then Me.cboRstFlg.SelectedIndex = 0

        Me.spdList.MaxRows = 0
        btnClear_Click(Nothing, Nothing)
        sbDisplay_Search(Ctrl.Get_Code(cboRstFlg).Trim)

        If Me.spdList.MaxRows = 0 Then
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회자료가 없습니다.!!")
        End If

    End Sub

    Private Sub cboTSect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSlip.SelectedIndexChanged
        sbDisplay_WkGrp()
        sbDisplay_wl()

        COMMON.CommXML.setOneElementXML(msTCLSDir, msSLIPFile, "SLIP", cboSlip.SelectedIndex.ToString)
    End Sub

    Private Sub Panel1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.DoubleClick
        If axResult.ColHiddenYn Then
            Me.axResult.ColHiddenYn = False
            'Me.lblTgrp.Visible = True
        Else
            Me.axResult.ColHiddenYn = True
            'Me.lblTgrp.Visible = False
        End If
    End Sub

    Private Sub btnRst_Ocs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRst_Ocs.Click

        Try
            Dim blnRet As Boolean = (New LISAPP.APP_R.AxRstFn).fnReg_OCS(axResult.BCNO)
            MsgBox(IIf(blnRet, "성공", "실패").ToString)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FGR_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub spdTgrp_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdTgrp.ClickEvent

        With spdTgrp
            .Row = e.row
            .Col = .GetColFromID("tgrpcd") : Me.lblTgrp.Text = .Text
            .Action = FPSpreadADO.ActionConstants.ActionActiveCell
        End With

        'COMMON.CommXML.setOneElementXML(msTCLSDir, msTGFile, "TGRPCD", Me.lblTgrp.Text)
    End Sub

    Private Sub cboWL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWL.SelectedIndexChanged

        Me.lblTest_wl.Text = "" : Me.lblTest_wl.Tag = ""

        If Me.cboWL.SelectedIndex < 0 Or Me.cboWL.Text = "" Then Return

        Dim sBuf() As String = Me.cboWL.Text.Split("|"c)

        If sBuf.Length > 3 Then
            Dim sWLYmd As String = sBuf(1)
            Dim sWLUId As String = sBuf(2)
            Dim sWLtitle As String = sBuf(0).Replace("(" + sWLYmd + ")", "")

            Me.dtpWLdts.Value = CDate(sWLYmd.Insert(4, "-").Insert(7, "-"))

            sbDisplay_Test_wl(sWLUId, sWLYmd, sWLtitle)
        End If
    End Sub

    Private Sub btnQuery_wl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery_wl.Click
        sbDisplay_wl()
    End Sub

    Private Sub btnReg_err_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg_err.Click
        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            For ix As Integer = 1 To 3
                Dim blnRet As Boolean = (New LISAPP.APP_R.AxRstFn).fnReg_err_m()
            Next

            MsgBox("완료")
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub spdTgrp_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdTgrp.LostFocus

        With Me.spdTgrp
            .Row = .ActiveRow
            .Col = .GetColFromID("tgrpcd") : Me.lblTgrp.Text = .Text
        End With

    End Sub

    Private Sub axResult_ChangedTestCd(ByVal BcNo As String, ByVal TestCd As String) Handles axResult.ChangedTestCd
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

    Private Sub mnuRst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRst.Click
        mnuRst.Checked = True
        mnuSearchList.Checked = False
        Me.axResult.sbFocus()
        Me.axResult.Focus()
    End Sub

    Private Sub mnuSearchList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSearchList.Click
        mnuRst.Checked = False
        mnuSearchList.Checked = True
        Me.spdList.Focus()
    End Sub

    Private Sub btnAddmic_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddmic.Click

        Dim sFn As String = "sbGv_Mic_hit"

        If Me.AxPatInfo.EntDt.Trim = "" Then
            MsgBox("추가처방을 낼 수 없습니다.환자정보를 확인해주세요")

        Else

            If LISAPP.APP_G.CommFn.fnGet_ENT_OUT_YN(Me.AxPatInfo.RegNo) = "Y" Then

                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "퇴원한 환자 입니다.!!")
                Return
            End If

            Dim objHelp As New LISM.FGM01_S01

            objHelp.FormText = "결과코드"

            Dim sGetDrid As String = objHelp.Display_Form()


            If sGetDrid.Length > 0 Then

                Dim al_sucs As New ArrayList
                Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

                Try
                    Dim sDeptInf As String = LISAPP.APP_G.CommFn.fnGet_Usr_Dept_info(sGetDrid)

                    Dim stu As New COMMON.SVar.STU_GVINFO

                    stu.REGNO = Me.AxPatInfo.RegNo

                    '<20150831 추가처방 오류 
                    If sGetDrid.Split("^"c)(1) = "MIC/Disk" Then
                        stu.ORDCD2 = PRG_CONST.TEST_MICRO_ORDCD2.Split("/"c)(0)     'DISK  참고 테이블 LF000M
                        stu.SUGACD2 = PRG_CONST.TEST_MICRO_ORDCD2.Split("/"c)(1)
                        stu.ORDCD = PRG_CONST.TEST_MICRO_ORDCD.Split("/"c)(0)     'MIC  참고 테이블 LF000M
                        stu.SUGACD = PRG_CONST.TEST_MICRO_ORDCD.Split("/"c)(1)

                    ElseIf sGetDrid.Split("^"c)(1) = "MIC/" Then
                        stu.ORDCD = PRG_CONST.TEST_MICRO_ORDCD.Split("/"c)(0)     'MIC  참고 테이블 LF000M
                        stu.SUGACD = PRG_CONST.TEST_MICRO_ORDCD.Split("/"c)(1)

                    ElseIf sGetDrid.Split("^"c)(1) = "/Disk" Then

                        stu.ORDCD2 = PRG_CONST.TEST_MICRO_ORDCD2.Split("/"c)(0)     'DISK  참고 테이블 LF000M
                        stu.SUGACD2 = PRG_CONST.TEST_MICRO_ORDCD2.Split("/"c)(1)
                    End If


                    'If PRG_CONST.TEST_MICRO_ORDCD2.Split("/"c)(0) = "LEB4061" Then

                    'ElseIf PRG_CONST.TEST_MICRO_ORDCD.Split("/"c)(0) = "LEB4062" Then

                    'End If

                    'stu.ORDCD = PRG_CONST.TEST_MICRO_ORDCD.Split("/"c)(0)
                    'stu.SUGACD = PRG_CONST.TEST_MICRO_ORDCD.Split("/"c)(1)


                    If sDeptInf.IndexOf("/") >= 0 Then
                        stu.DEPTCD_USR = sDeptInf.Split("/"c)(0)
                        stu.DEPTNM_USR = sDeptInf.Split("/"c)(1)
                    Else
                        stu.DEPTCD_USR = ""
                        stu.DEPTNM_USR = ""
                    End If

                    If sGetDrid.IndexOf("/") >= 0 Then
                        stu.ORDDRID = sGetDrid.Split("/"c)(0)
                        stu.ORDDRNM = sGetDrid.Split("/"c)(1)
                    Else
                        stu.ORDDRID = "-"
                        stu.ORDDRNM = "-"
                    End If

                    stu.SPCCD = "-"
                    stu.STATUS = "I,M"

                    Dim sRet As String = (New WEBSERVER.CGWEB_G).ExecuteDo(stu)

                    If sRet.StartsWith("00") Then
                        '성공
                        MsgBox("처방이 성공적으로 이루어졌습니다.")
                    Else
                        '실패
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, sRet.Substring(2))
                        Return
                    End If


                Catch ex As Exception
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

                Finally

                    For i As Integer = al_sucs.Count To 1 Step -1
                        spd.DeleteRows(Convert.ToInt32(al_sucs(i - 1)), 1)
                        spd.MaxRows -= 1
                    Next


                End Try
            End If
        End If
    End Sub
    '<<<20150806 결과일자로 조회 되도록 수정 추가 
    Private Sub Label14_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label14.Click

        If Label14.Text = "접수일자" Then
            Label14.Text = "결과일자"

            msSearCh_mode = 1
        ElseIf Label14.Text = "결과일자" Then
            Label14.Text = "접수일자"

            msSearCh_mode = 0
        End If
    End Sub

End Class

