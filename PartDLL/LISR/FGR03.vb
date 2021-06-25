'>>> 담당자별 결과저장 및 보고
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports COMMON.CommLogin
Imports COMMON.CommLogin.LOGIN
Imports AxAckResult
Imports System.IO
Imports DBORA

Public Class FGR03
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGR03.vb, Class : FGR03" & vbTab

    Private mGRstCdDTable As DataTable  ' 검사항목별 결과코드 마스터
    Private mCommentTable As DataTable ' 소견 마스터

    Public mbBloodBankYN As Boolean = False
    Private malRst As New ArrayList         ' 결과 hidden 컬럼 정보
    Private malCmt As New ArrayList         ' 소견 hidden 컬럼 정보
    Private malList As New ArrayList        ' 소견 hidden 컬럼 정보

    Private msState As String           ' 결과조회 상태
    Private msBCNO As String            ' 검체번호
    Private msSpreadNm As String    ' 결과코드 리스트 해당 스프레드
    Private Const msXmlDir As String = "\XML"
    Private msTABFile As String = Application.StartupPath + msXmlDir + "\FGR03_TABCFG.XML"
    Private msPartSlip As String = Application.StartupPath + msXmlDir + "\FGR03_SLIP.XML"
    Private msTGFile As String = Application.StartupPath + msXmlDir + "\FGR03_TGCD.XML"
    Private msWGFile As String = Application.StartupPath & msXmlDir & "\FGR03_WGCD.XML"
    Private msEQFile As String = Application.StartupPath & msXmlDir & "\FGR03_EQCD.XML"

    Private mbDisplay As Boolean = False    ' 화면 표시 여부
    Private mbFirst As Boolean = True
    Private msGbn As String

    Private ToolTip1 As New Windows.Forms.ToolTip

    Private m_dt_RstUsr As DataTable
    Friend WithEvents btnMove As System.Windows.Forms.Button
    Friend WithEvents AxResult As AxAckResult.AxRstInput


    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpWkDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblWkNo As System.Windows.Forms.Label
    Friend WithEvents txtWkNoE As System.Windows.Forms.TextBox
    Friend WithEvents txtWkNoS As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents chkJobGbn As System.Windows.Forms.CheckBox
    Friend WithEvents chkSel_List As System.Windows.Forms.CheckBox
    Friend WithEvents sfdSCd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents chkNotRerun As System.Windows.Forms.CheckBox
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkHL As System.Windows.Forms.CheckBox
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents chkA As System.Windows.Forms.CheckBox
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents chkReRun As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboRstFlg As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkPDC As System.Windows.Forms.CheckBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents chkFlag As System.Windows.Forms.CheckBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents chkN As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoveCol As System.Windows.Forms.CheckBox
    Friend WithEvents lblTNMD As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboWkGrp As System.Windows.Forms.ComboBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents chkER As System.Windows.Forms.CheckBox
    Friend WithEvents AxPatInfo As AxAckResult.AxRstPatInfo
    Friend WithEvents btnHistory As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnMW As CButtonLib.CButton
    Friend WithEvents btnFN As CButtonLib.CButton
    Friend WithEvents btnRst_Clear As CButtonLib.CButton
    Friend WithEvents btnRerun As CButtonLib.CButton
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpWkDt_e As System.Windows.Forms.DateTimePicker
    Friend WithEvents spdTGrp As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkSel_Tgrp As System.Windows.Forms.CheckBox
    Friend WithEvents btnHelp_Tcls As System.Windows.Forms.Button
    Friend WithEvents btnClear_Tcls As System.Windows.Forms.Button
    Friend WithEvents spdEq As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblEqnm As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpWLdts As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboWL As System.Windows.Forms.ComboBox
    Friend WithEvents lblTest_wl As System.Windows.Forms.Label
    Friend WithEvents btnQuery_wl As CButtonLib.CButton
    Friend WithEvents cboRstFlg_wl As System.Windows.Forms.ComboBox
    Friend WithEvents dtpWLdte As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkMW As System.Windows.Forms.CheckBox
    Friend WithEvents btnRst_ocs As System.Windows.Forms.Button
    Friend WithEvents cboQryGbn As System.Windows.Forms.ComboBox
    Friend WithEvents btnQuery_pat As System.Windows.Forms.Button
    Friend WithEvents chkConQC As System.Windows.Forms.CheckBox

    Private m_sf_dri As AxAckResultViewer.SF_Disp_RstInfo


    Public WriteOnly Property BloodBankYN() As Boolean
        Set(ByVal value As Boolean)
            mbBloodBankYN = value
        End Set
    End Property


    ' hidden 컬럼 보이기/숨기기
    Private Sub sbSpread_Row_ShowHidden(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal raValue As ArrayList)
        Dim sFn As String = "Sub sbSpread_Row_ShowHidden(AxFPSpreadADO.AxFPSpread, ArrayList)"
        Try
            With r_spd
                For iLoop As Integer = 0 To raValue.Count - 1
                    .Row = CType(raValue(iLoop), Integer)
                    If .RowHidden = True Then
                        .RowHidden = False
                    Else
                        .RowHidden = True
                    End If
                Next
            End With
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    ' hidden 컬럼 보이기/숨기기
    Private Sub sbSpread_Col_ShowHidden(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal raValue As ArrayList)
        Dim sFn As String = "Sub spreadShowHide(ByVal spdLocal As AxFPSpreadADO.AxFPSpread, ByVal alRst As ArrayList)"
        Try
            With r_spd
                For iLoop As Integer = 0 To raValue.Count - 1
                    .Col = CType(raValue(iLoop), Integer)
                    If .ColHidden = True Then
                        .ColHidden = False
                    Else
                        .ColHidden = True
                    End If
                Next
            End With
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    ' hidden 컬럼 정보 저장
    Private Sub sbSpread_Col_HiddenInfo(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal raValue As ArrayList)
        Dim sFn As String = "Sub sbSpread_HiddenInfo(AxFPSpreadADO.AxFPSpread, ArrayList)"
        Try
            With r_spd
                For iLoop As Integer = 1 To .MaxCols
                    .Col = iLoop
                    If .ColHidden = True Then
                        raValue.Add(iLoop)
                    End If
                Next
            End With
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    ' hidden 컬럼 정보 저장
    Private Sub sbSpread_Row_HiddenInfo(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal raValue As ArrayList)
        Dim sFn As String = "Sub sbSpread_Row_HiddenInfo(AxFPSpreadADO.AxFPSpread, ArrayList)"
        Try
            With r_spd
                For iLoop As Integer = 1 To .MaxRows
                    .Row = iLoop
                    If .RowHidden = True Then
                        raValue.Add(iLoop)
                    End If
                Next
            End With
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
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

    Private Sub sbDisplay_SpcListVIew(ByVal r_spd As AxFPSpreadADO.AxfpSpread, ByVal r_dt As DataTable)

        r_spd.MaxRows = r_dt.Rows.Count

        If r_dt.Rows.Count > 0 Then
            With r_spd
                For iRow As Integer = 0 To r_dt.Rows.Count - 1
                    For ix As Integer = 1 To r_dt.Columns.Count
                        Dim iCol As Integer = 0

                        iCol = .GetColFromID(r_dt.Columns(ix - 1).ColumnName.ToLower())
                        If iCol > 0 Then
                            .Row = iRow + 1
                            .Col = iCol

                            .Text = r_dt.Rows(iRow).Item(ix - 1).ToString.Trim

                            If r_dt.Columns(ix - 1).ColumnName.ToLower() = "deptcd" And _
                               (r_dt.Rows(iRow).Item(ix - 1).ToString.Trim.ToLower.IndexOf(PRG_CONST.DEPT_ER) >= 0 Or _
                                r_dt.Rows(iRow).Item(ix - 1).ToString.Trim.ToLower.IndexOf("em") >= 0) Then
                                .Row = iRow + 1 : .Col = iCol : .ForeColor = Color.Red
                            End If
                        Else
                            If r_dt.Columns(ix - 1).ColumnName.ToLower() = "rmkyn" Then
                                If r_dt.Rows(iRow).Item(ix - 1).ToString.Trim.ToUpper = "Y" And r_dt.Rows(iRow).Item("statgbn").ToString.Trim = "" Then
                                    .Row = iRow + 1 : .Col = -1 : .ForeColor = Color.Blue
                                End If
                                'ElseIf r_dt.Columns(ix - 1).ColumnName.ToLower() = "statgbn" Then ' ori
                            ElseIf (r_dt.Columns(ix - 1).ColumnName.ToLower() = "statgbn" Or r_dt.Columns(ix - 1).ColumnName.ToLower() = "eryn") Then ' JJH eryn -> 자체응급(LJ015M)
                                If r_dt.Rows(iRow).Item(ix - 1).ToString.Trim.ToUpper = "Y" Or r_dt.Rows(iRow).Item(ix - 1).ToString.Trim.ToUpper = "E" Then
                                    .Row = iRow + 1 : .Col = -1 : .ForeColor = Color.Red
                                End If
                            ElseIf r_dt.Columns(ix - 1).ColumnName.ToLower() = "tat" Then
                                Dim sBuf() As String = r_dt.Rows(iRow).Item("tat").ToString.Split("^"c)
                                If Val(sBuf(0)) > Val(sBuf(1)) And Val(sBuf(1)) > 0 Then
                                    .Col = 0 : .Text = "√" : .Col = -1 : .BackColor = Color.SkyBlue
                                End If
                            End If
                        End If
                    Next
                Next
            End With
        End If

    End Sub

    Private Sub sbDisplay_Slip()

        Dim sFn As String = "Sub sbDisplay_Slip()"
        Dim dt As DataTable

        Try
            dt = LISAPP.COMM.CdFn.fnGet_Slip_List(dtpTkdtS.Text, False, mbBloodBankYN)

            Me.cboPartSlip.Items.Clear()
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPartSlip.Items.Add("[" + dt.Rows(ix).Item("slipcd").ToString.Trim + "] " + dt.Rows(ix).Item("slipnmd").ToString.Trim)
            Next

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try
    End Sub

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

    Private Sub sbDisplay_TGrp()

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TGrp_List(Ctrl.Get_Code(Me.cboPartSlip))

        With spdTGrp
            .ReDraw = False
            .MaxRows = dt.Rows.Count

            For ix As Integer = 0 To dt.Rows.Count - 1
                .Row = ix + 1
                .Col = .GetColFromID("tgrpcd") : .Text = dt.Rows(ix).Item("tgrpcd").ToString.Trim
                .Col = .GetColFromID("tgrpnmd") : .Text = dt.Rows(ix).Item("tgrpnmd").ToString.Trim
            Next
            .ReDraw = True
        End With
    End Sub

    Private Sub sbDisplay_wl()

        Dim sFn As String = "Sub sbDisplay_wl()"

        Try
            Me.lblTest_wl.Text = "" : Me.lblTest_wl.Tag = ""

            Dim dt As DataTable = LISAPP.APP_WL.Qry.fnGet_wl_title(Ctrl.Get_Code(Me.cboPartSlip), "--", Me.dtpWLdts.Text.Replace("-", ""), Me.dtpWLdte.Text.Replace("-", ""), Ctrl.Get_Code(Me.cboRstFlg_wl))

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

    ' 검사장비별 조회
    Private Sub sbDisplay_SpcList_E()
        Dim sFn As String = "Sub sbDisplay_SpcList_E"

        Try
            Dim sN As String = "", sHL As String = "", sPDC As String = "", sA As String = "", sEqFlag As String = "", sReRun As String = "", sEr As String = ""
            Dim sRstFlg As String = Ctrl.Get_Code(cboRstFlg)

            If chkN.Checked Then sN = "N"
            If chkHL.Checked Then sHL = "HL"
            If chkPDC.Checked Then sPDC = "PDC"
            If chkA.Checked Then sA = "A"
            If chkFlag.Checked Then sEqFlag = "CMT"
            If chkReRun.Checked Then sReRun = "RERUN"
            If chkNotRerun.Checked Then sReRun = "NOTRERUN"

            If Me.lblEqnm.Text = "" Then Exit Sub

            Dim sRegNo As String = ""

            If lblSearch.Text <> "검체번호" Then
                If txtSearch.Tag Is Nothing Then txtSearch.Tag = ""
                sRegNo = txtSearch.Tag.ToString
            End If

            Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_Eq(Me.lblEqnm.Tag.ToString, Me.dtpRst.Text.Replace("-", ""), sEr, sRegNo)

            Dim dr As DataRow()

            Dim sSql As String = ""

            Select Case sRstFlg
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

            '20210406 jhs QC 연동 
            If chkConQC.Checked = True Then
                Dim rsEqcd As String = ""

                Dim dt_eqcd As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_Qceqcd(Me.lblEqnm.Tag.ToString)
                If dt_eqcd.Rows.Count > 0 And dt_eqcd.Rows(0).Item(0).ToString.Trim <> "" Then
                    rsEqcd = "'" + dt_eqcd.Rows(0).Item(0).ToString.Trim + "'"

                    Dim dt_QC As DataTable = fnGet_QC_SpcList_Eq_QC(rsEqcd, Me.dtpRst.Text.Replace("-", ""), sEr, sRegNo)
                    dt.Merge(dt_QC)
                    dr = dt.Select("", "tkdt, bcno")
                    dt = Fn.ChangeToDataTable(dr)
                Else
                    MsgBox("QC 장비코드가 조회되지 않았습니다. 관리자 에게 문의 부탁드립니다.")
                End If
            End If
            '-------------------------------

            sbDisplay_SpcListVIew(spdList_eq, dt)
            If spdList_eq.MaxRows > 0 Then spdSpcList_ClickEvent(spdList_eq, New AxFPSpreadADO._DSpreadEvents_ClickEvent(2, 1))


        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)

        End Try
    End Sub


    '--20210406 jhs  장비별 검사리스트 QC 데이터 조회 
    Public Shared Function fnGet_QC_SpcList_Eq_QC(ByVal rsEqCd As String, ByVal rsRstDt As String, _
                                            ByVal rsEr As String, Optional ByVal rsRegNo As String = "") As DataTable
        Dim sFn As String = "fnGet_QC_SpcList_Eq_QC() As DataTable"
        Try
            Dim oleDbCn As OleDb.OleDbConnection
            Dim oleDbCmd As New OleDb.OleDbCommand

            oleDbCn = CType(DBORA.ORADB.DbConnection_QC(), OleDb.OleDbConnection)

            Dim sSql As String = ""
            Dim dt As New DataTable
            Dim strWhere As String = ""

            rsRstDt = rsRstDt.Replace("-", "")

            sSql += " "
            sSql += " select    '검사장비' as QRYGBN," + vbCrLf
            sSql += "           substring(min(r.rstdt), 1, 12) PRTBCNO," + vbCrLf
            sSql += "           '---' PATNM," + vbCrLf
            'sSql += "           case when r.bcno is null then 'QC' + r.lcd +'-'+m.MNM else 'QP'+ r.lcd + '-' + m.MNM  end REGNO," + vbCrLf 'ex)QC1-물질명
            sSql += "            'QC' + r.lcd +'-'+m.MNM as REGNO," + vbCrLf 'ex)QC1-물질명
            sSql += "           '^' TAT," + vbCrLf
            '' QC 컨트롤이 끝난다음에 장비에 검체를 걸기때문에  rstdt 를 tkdt로 대체
            sSql += "           substring(min(r.rstdt), 1, 4) + '-' +substring(min(r.rstdt), 5, 2) + '-' + substring(min(r.rstdt), 7, 2) +' '+ substring(min(r.rstdt), 9, 2)+':'+substring(min(r.rstdt), 11, 2)+':'+substring(min(r.rstdt), 13, 2) TKDT," + vbCrLf
            sSql += "           case when r.bcno is null then r.rstymd else r.bcno end as BCNO ,  " + vbCrLf
            sSql += "           substring(min(r.sysdt), 9, 2)+':'+substring(min(r.sysdt), 11, 2)+':'+substring(min(r.sysdt), 13, 2) as systm," + vbCrLf
            sSql += "           substring(min(r.rstdt), 9, 2)+':'+substring(min(r.rstdt), 11, 2)+':'+substring(min(r.rstdt), 13, 2) as rsttm" + vbCrLf
            sSql += "   from (((" + vbCrLf
            sSql += "           qcrst r  with(readpast ,index([PK_QCRST]))" + vbCrLf
            sSql += "               inner join qcmst q " + vbCrLf
            sSql += "               on r.eqcd = q.eqcd and r.mcd = q.mcd and r.tcd = q.tcd and r.lcd = q.lcd and q.usdt <= r.sysdt and q.uedt > r.sysdt" + vbCrLf
            sSql += "               )" + vbCrLf
            sSql += "               inner join eqmmst m " + vbCrLf
            sSql += "               on r.eqcd = m.eqcd and r.mcd = m.mcd" + vbCrLf
            sSql += "               )" + vbCrLf
            sSql += "               inner join eqlmst l " + vbCrLf
            sSql += "               on r.eqcd = l.eqcd and r.mcd = l.mcd and r.lcd = l.lcd" + vbCrLf
            sSql += "               )" + vbCrLf
            sSql += "   where r.eqcd in (" + rsEqCd + ")" + vbCrLf
            sSql += "     and r.rstdt >= '" + rsRstDt + "000000' " + vbCrLf
            sSql += "     and r.rstdt <= '" + rsRstDt + "235959' " + vbCrLf
            sSql += "     and r.bcno is null " + vbCrLf
            sSql += "   group by r.eqcd, r.rstymd, m.MNM , r.lcd, r.bcno, r.rstseq, r.rstdt" + vbCrLf
            sSql += "   order by min(r.sysdt), r.rstymd, r.rstseq" + vbCrLf



            'er 응급에 대한 내용없어서 지워놈
            'If rsEr <> "" Then
            '    'sSql += "   AND NVL(j.statgbn, '0') <> '0'"
            '    sSql += "   AND (NVL(j.statgbn, '0') <> '0' or NVL(j15.bcno, 'N') <> 'N')" + vbCrLf
            'End If

            'regno 없어서 지워놈
            'If rsRegNo <> "" Then
            '    sSql += "   AND j.regno = :regno" + vbCrLf
            'End If


            With oleDbCmd
                .Connection = oleDbCn
                .CommandType = CommandType.Text
                .CommandText = sSql

                Dim objDAdapter As New OleDb.OleDbDataAdapter(oleDbCmd)
                objDAdapter.Fill(dt)
            End With

            Return dt

        Catch ex As Exception
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try
    End Function


    ' 검사그룹별 조회
    Private Sub sbDisplay_SpcList_T()

        Dim sTGrpCds As String = ""
        Dim bFind As Boolean = False
        Dim sN As String = "", sHL As String = "", sPDC As String = "", sA As String = "", sEqFlag As String = "", sReRun As String = "", sER As String = ""
        Dim sRstFlg As String = Ctrl.Get_Code(Me.cboRstFlg)

        If chkN.Checked Then sN = "N"
        If chkHL.Checked Then sHL = "HL"
        If chkPDC.Checked Then sPDC = "PDC"
        If chkA.Checked Then sA = "A"
        If chkFlag.Checked Then sEqFlag = "CMT"
        If chkReRun.Checked Then sReRun = "RERUN"
        If chkNotRerun.Checked Then sReRun = "NOTRERUN"
        If chkER.Checked Then sER = "ER"
        If sRstFlg = "0" And sReRun = "NOTRERUN" Then sReRun = ""

        With spdTGrp
            For iRow As Integer = 1 To .MaxRows
                .Row = iRow
                .Col = .GetColFromID("chk")
                If .Text = "1" Then
                    .Col = .GetColFromID("tgrpcd") : sTGrpCds += IIf(sTGrpCds = "", "", ",").ToString + .Text

                    bFind = True
                End If
            Next
        End With

        If bFind = False Then
            MsgBox("검사그룹을 선택하신 후 조회하세요.")
            Return
        End If

        Dim sRegNo As String = ""

        If Me.lblSearch.Text <> "검체번호" Then
            If txtSearch.Tag Is Nothing Then Me.txtSearch.Tag = ""
            sRegNo = Me.txtSearch.Tag.ToString
        End If

        Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_TGrp(sTGrpCds, Me.dtpTkdtS.Text.Replace("-", ""), Me.dtpTkDtE.Text.Replace("-", ""), sER, sRegNo)

        Dim dr As DataRow()
        Dim sSql As String = ""

        Select Case sRstFlg
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

        sbDisplay_SpcListVIew(spdList, dt)

        If spdList.MaxRows > 0 Then spdSpcList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(2, 1))

        COMMON.CommXML.setOneElementXML(msXmlDir, msTGFile, "TGCD", sTGrpCds)
    End Sub

    ' 작업번호별 조회
    Private Sub sbDisplay_SpcList_W()
        Dim sFn As String = "Sub sbDisplay_SpcList_W"

        Try
            Dim sWkYmd As String = Me.dtpWkDt.Text.Replace("-", "").PadRight(8, "0"c)
            Dim sWkYmd_e As String = IIf(chkJobGbn.Checked, "", Me.dtpWkDt_e.Text.Replace("-", "").PadRight(8, "0"c)).ToString
            Dim sWkNoS As String = Me.txtWkNoS.Text.PadLeft(4, "0"c)
            Dim sWkNoE As String = IIf(Me.txtWkNoE.Text = "", "9999", Me.txtWkNoE.Text.PadLeft(4, "0"c)).ToString

            Dim sN As String = "", sHL As String = "", sPDC As String = "", sA As String = "", sEqFlag As String = "", sReRun As String = "", sER As String = ""
            Dim sRstFlg As String = Ctrl.Get_Code(cboRstFlg)

            If chkN.Checked Then sN = "N"
            If chkHL.Checked Then sHL = "HL"
            If chkPDC.Checked Then sPDC = "PDC"
            If chkA.Checked Then sA = "A"

            If chkER.Checked Then sER = "ER"
            If chkFlag.Checked Then sEqFlag = "CMT"
            If chkReRun.Checked Then sReRun = "RERUN"
            If chkNotRerun.Checked Then sReRun = "NOTRERUN"


            If cboWkGrp.SelectedIndex > -1 Then


                Dim sTestCds As String = ""
                If Me.lblTNMD.Text <> "" Then sTestCds = Me.lblTNMD.Tag.ToString

                Dim sRegNo As String = ""

                If Me.lblSearch.Text <> "검체번호" Then
                    If txtSearch.Tag Is Nothing Then txtSearch.Tag = ""
                    sRegNo = txtSearch.Tag.ToString
                End If

                Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_WGrp(Ctrl.Get_Code(cboWkGrp), sWkYmd, sWkYmd_e, sWkNoS, sWkNoE, sER, sRegNo)

                Dim dr() As DataRow
                Dim sSql As String = ""

                Select Case sRstFlg
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

                dr = dt.Select(sSql, "workno, bcno")
                dt = Fn.ChangeToDataTable(dr)

                sbDisplay_SpcListVIew(spdList, dt)
                If spdList.MaxRows > 0 Then spdSpcList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(2, 1))

            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try

    End Sub

    ' W/L 조회
    Private Sub sbDisplay_SpcList_WL()

        Dim sN As String = "", sHL As String = "", sPDC As String = "", sA As String = "", sEqFlag As String = "", sReRun As String = "", sER As String = ""
        Dim sRstFlg As String = Ctrl.Get_Code(cboRstFlg)

        If chkN.Checked Then sN = "N"
        If chkHL.Checked Then sHL = "HL"
        If chkPDC.Checked Then sPDC = "PDC"
        If chkA.Checked Then sA = "A"
        If chkFlag.Checked Then sEqFlag = "CMT"
        If chkReRun.Checked Then sReRun = "RERUN"
        If chkNotRerun.Checked Then sReRun = "NOTRERUN"
        If chkER.Checked Then sER = "ER"
        If sRstFlg = "0" And sReRun = "NOTRERUN" Then sReRun = ""

        If Me.cboWL.SelectedIndex < 0 Then
            MsgBox("W/L 을 선택하신 후 조회하세요.")
            Return
        End If

        Dim sRegNo As String = ""

        If Me.lblSearch.Text <> "검체번호" Then
            If txtSearch.Tag Is Nothing Then Me.txtSearch.Tag = ""
            sRegNo = Me.txtSearch.Tag.ToString
        End If

        Dim sWLUid As String = Me.cboWL.Text.Split("|"c)(2)
        Dim sWLYmd As String = Me.cboWL.Text.Split("|"c)(1)
        Dim sWLTitle As String = Me.cboWL.Text.Split("|"c)(0).Trim.Replace("(" + sWLYmd + ")", "").Trim

        Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_WL(sWLUid, sWLYmd, sWLTitle, sN, sHL, sPDC, sA, sEqFlag, sReRun, sER, sRegNo)

        Dim dr As DataRow()
        Dim sSql As String = ""

        Select Case sRstFlg
            Case "0"
                sSql += "rstflg_t = '00'"
            Case "1"
                sSql += "rstflg_t >= '01' AND rstflg_t <= '13'"
            Case "2"
                sSql += "rstflg_t >= '20' AND rstflg_t <= '23'"
            Case "3"
                sSql += "rstflg_t >= '3'"
        End Select

        dr = dt.Select(sSql, "tkdt, bcno")
        dt = Fn.ChangeToDataTable(dr)

        sbDisplay_SpcListVIew(spdList, dt)

        If spdList.MaxRows > 0 Then spdSpcList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(2, 1))

    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub

    Public Sub New(ByVal rsBloodBankYN As Boolean)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        mbBloodBankYN = rsBloodBankYN
        If rsBloodBankYN Then
            msTABFile = Application.StartupPath + msXmlDir + "\FGR03_TABCFG_B.XML"
            msPartSlip = Application.StartupPath + msXmlDir + "\FGR03_SLIP_B.XML"
            msTGFile = Application.StartupPath + msXmlDir + "\FGR03_TGCD_B.XML"
            msWGFile = Application.StartupPath & msXmlDir & "\FGR03_WGCD_B.XML"
            msEQFile = Application.StartupPath & msXmlDir & "\FGR03_EQCD_B.XML"

            Me.Text = Me.Text + "(혈액은행)"
            Me.chkMW.Visible = False
        End If

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents tbcJob As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents dtpTkDtE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTkdtS As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpRst As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnBFN As System.Windows.Forms.Button
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents spdList_eq As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cboRerun As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR03))
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.spdList_eq = New AxFPSpreadADO.AxfpSpread()
        Me.btnBFN = New System.Windows.Forms.Button()
        Me.chkSel_List = New System.Windows.Forms.CheckBox()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.cboQryGbn = New System.Windows.Forms.ComboBox()
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.cboPartSlip = New System.Windows.Forms.ComboBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.tbcJob = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkSel_Tgrp = New System.Windows.Forms.CheckBox()
        Me.spdTGrp = New AxFPSpreadADO.AxfpSpread()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpTkDtE = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpTkdtS = New System.Windows.Forms.DateTimePicker()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkJobGbn = New System.Windows.Forms.CheckBox()
        Me.cboWkGrp = New System.Windows.Forms.ComboBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblWkNo = New System.Windows.Forms.Label()
        Me.txtWkNoE = New System.Windows.Forms.TextBox()
        Me.txtWkNoS = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.dtpWkDt_e = New System.Windows.Forms.DateTimePicker()
        Me.btnClear_Tcls = New System.Windows.Forms.Button()
        Me.btnHelp_Tcls = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpWkDt = New System.Windows.Forms.DateTimePicker()
        Me.lblTNMD = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.lblEqnm = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.spdEq = New AxFPSpreadADO.AxfpSpread()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.dtpRst = New System.Windows.Forms.DateTimePicker()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.btnQuery_wl = New CButtonLib.CButton()
        Me.cboRstFlg_wl = New System.Windows.Forms.ComboBox()
        Me.dtpWLdte = New System.Windows.Forms.DateTimePicker()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblTest_wl = New System.Windows.Forms.Label()
        Me.cboWL = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpWLdts = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.btnRst_ocs = New System.Windows.Forms.Button()
        Me.chkMW = New System.Windows.Forms.CheckBox()
        Me.btnRerun = New CButtonLib.CButton()
        Me.btnFN = New CButtonLib.CButton()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.cboRerun = New System.Windows.Forms.ComboBox()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnRst_Clear = New CButtonLib.CButton()
        Me.btnMW = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.AxResult = New AxAckResult.AxRstInput()
        Me.btnDown = New System.Windows.Forms.Button()
        Me.btnUp = New System.Windows.Forms.Button()
        Me.sfdSCd = New System.Windows.Forms.SaveFileDialog()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.chkER = New System.Windows.Forms.CheckBox()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.chkNotRerun = New System.Windows.Forms.CheckBox()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.chkHL = New System.Windows.Forms.CheckBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.chkA = New System.Windows.Forms.CheckBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.chkReRun = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboRstFlg = New System.Windows.Forms.ComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkPDC = New System.Windows.Forms.CheckBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.chkFlag = New System.Windows.Forms.CheckBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.chkN = New System.Windows.Forms.CheckBox()
        Me.btnQuery = New CButtonLib.CButton()
        Me.chkMoveCol = New System.Windows.Forms.CheckBox()
        Me.AxPatInfo = New AxAckResult.AxRstPatInfo()
        Me.btnHistory = New CButtonLib.CButton()
        Me.btnQuery_pat = New System.Windows.Forms.Button()
        Me.chkConQC = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdList_eq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox12.SuspendLayout()
        Me.tbcJob.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.spdTGrp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.spdEq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Controls.Add(Me.spdList_eq)
        Me.Panel1.Location = New System.Drawing.Point(4, 344)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(294, 250)
        Me.Panel1.TabIndex = 100
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(290, 246)
        Me.spdList.TabIndex = 8
        '
        'spdList_eq
        '
        Me.spdList_eq.DataSource = Nothing
        Me.spdList_eq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList_eq.Location = New System.Drawing.Point(0, 0)
        Me.spdList_eq.Name = "spdList_eq"
        Me.spdList_eq.OcxState = CType(resources.GetObject("spdList_eq.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList_eq.Size = New System.Drawing.Size(290, 246)
        Me.spdList_eq.TabIndex = 9
        '
        'btnBFN
        '
        Me.btnBFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnBFN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBFN.Location = New System.Drawing.Point(4, 571)
        Me.btnBFN.Name = "btnBFN"
        Me.btnBFN.Size = New System.Drawing.Size(294, 25)
        Me.btnBFN.TabIndex = 109
        Me.btnBFN.Text = "일괄 결과검증"
        Me.btnBFN.Visible = False
        '
        'chkSel_List
        '
        Me.chkSel_List.AutoSize = True
        Me.chkSel_List.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkSel_List.Location = New System.Drawing.Point(36, 352)
        Me.chkSel_List.Name = "chkSel_List"
        Me.chkSel_List.Size = New System.Drawing.Size(15, 14)
        Me.chkSel_List.TabIndex = 10
        Me.chkSel_List.UseVisualStyleBackColor = False
        '
        'GroupBox12
        '
        Me.GroupBox12.Controls.Add(Me.cboQryGbn)
        Me.GroupBox12.Controls.Add(Me.btnToggle)
        Me.GroupBox12.Controls.Add(Me.cboPartSlip)
        Me.GroupBox12.Controls.Add(Me.lblSearch)
        Me.GroupBox12.Controls.Add(Me.txtSearch)
        Me.GroupBox12.Controls.Add(Me.Label48)
        Me.GroupBox12.Location = New System.Drawing.Point(4, -5)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(294, 59)
        Me.GroupBox12.TabIndex = 110
        Me.GroupBox12.TabStop = False
        '
        'cboQryGbn
        '
        Me.cboQryGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboQryGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboQryGbn.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboQryGbn.Location = New System.Drawing.Point(86, 11)
        Me.cboQryGbn.Name = "cboQryGbn"
        Me.cboQryGbn.Size = New System.Drawing.Size(52, 20)
        Me.cboQryGbn.TabIndex = 123
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(242, 34)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(46, 21)
        Me.btnToggle.TabIndex = 18
        Me.btnToggle.Text = "<->"
        '
        'cboPartSlip
        '
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPartSlip.Items.AddRange(New Object() {"자동화계", "특수계1", "툭스계2"})
        Me.cboPartSlip.Location = New System.Drawing.Point(139, 11)
        Me.cboPartSlip.MaxDropDownItems = 10
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(149, 20)
        Me.cboPartSlip.TabIndex = 122
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(5, 34)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(80, 21)
        Me.lblSearch.TabIndex = 17
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSearch.Location = New System.Drawing.Point(86, 34)
        Me.txtSearch.MaxLength = 18
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(155, 21)
        Me.txtSearch.TabIndex = 16
        '
        'Label48
        '
        Me.Label48.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label48.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label48.ForeColor = System.Drawing.Color.White
        Me.Label48.Location = New System.Drawing.Point(5, 11)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(80, 21)
        Me.Label48.TabIndex = 17
        Me.Label48.Text = "검사분야"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbcJob
        '
        Me.tbcJob.Controls.Add(Me.TabPage1)
        Me.tbcJob.Controls.Add(Me.TabPage2)
        Me.tbcJob.Controls.Add(Me.TabPage3)
        Me.tbcJob.Controls.Add(Me.TabPage4)
        Me.tbcJob.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcJob.HotTrack = True
        Me.tbcJob.ItemSize = New System.Drawing.Size(72, 20)
        Me.tbcJob.Location = New System.Drawing.Point(4, 57)
        Me.tbcJob.Name = "tbcJob"
        Me.tbcJob.SelectedIndex = 0
        Me.tbcJob.Size = New System.Drawing.Size(294, 186)
        Me.tbcJob.TabIndex = 109
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.chkSel_Tgrp)
        Me.TabPage1.Controls.Add(Me.spdTGrp)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.dtpTkDtE)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.dtpTkdtS)
        Me.TabPage1.Location = New System.Drawing.Point(4, 24)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(286, 158)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "검사그룹별"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkSel_Tgrp
        '
        Me.chkSel_Tgrp.AutoSize = True
        Me.chkSel_Tgrp.Location = New System.Drawing.Point(11, 34)
        Me.chkSel_Tgrp.Name = "chkSel_Tgrp"
        Me.chkSel_Tgrp.Size = New System.Drawing.Size(15, 14)
        Me.chkSel_Tgrp.TabIndex = 18
        Me.chkSel_Tgrp.UseVisualStyleBackColor = True
        '
        'spdTGrp
        '
        Me.spdTGrp.DataSource = Nothing
        Me.spdTGrp.Location = New System.Drawing.Point(5, 29)
        Me.spdTGrp.Name = "spdTGrp"
        Me.spdTGrp.OcxState = CType(resources.GetObject("spdTGrp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTGrp.Size = New System.Drawing.Size(276, 126)
        Me.spdTGrp.TabIndex = 17
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(173, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(11, 12)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "~"
        '
        'dtpTkDtE
        '
        Me.dtpTkDtE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkDtE.Location = New System.Drawing.Point(191, 5)
        Me.dtpTkDtE.Name = "dtpTkDtE"
        Me.dtpTkDtE.Size = New System.Drawing.Size(90, 21)
        Me.dtpTkDtE.TabIndex = 15
        Me.dtpTkDtE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(5, 5)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(70, 21)
        Me.Label14.TabIndex = 14
        Me.Label14.Text = "접수일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpTkdtS
        '
        Me.dtpTkdtS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTkdtS.Location = New System.Drawing.Point(76, 5)
        Me.dtpTkdtS.Name = "dtpTkdtS"
        Me.dtpTkdtS.Size = New System.Drawing.Size(90, 21)
        Me.dtpTkdtS.TabIndex = 13
        Me.dtpTkdtS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.chkJobGbn)
        Me.TabPage2.Controls.Add(Me.cboWkGrp)
        Me.TabPage2.Controls.Add(Me.Label46)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.lblWkNo)
        Me.TabPage2.Controls.Add(Me.txtWkNoE)
        Me.TabPage2.Controls.Add(Me.txtWkNoS)
        Me.TabPage2.Controls.Add(Me.Label43)
        Me.TabPage2.Controls.Add(Me.dtpWkDt_e)
        Me.TabPage2.Controls.Add(Me.btnClear_Tcls)
        Me.TabPage2.Controls.Add(Me.btnHelp_Tcls)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.dtpWkDt)
        Me.TabPage2.Controls.Add(Me.lblTNMD)
        Me.TabPage2.Location = New System.Drawing.Point(4, 24)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(286, 158)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "작업그룹별"
        Me.TabPage2.UseVisualStyleBackColor = True
        Me.TabPage2.Visible = False
        '
        'chkJobGbn
        '
        Me.chkJobGbn.AutoSize = True
        Me.chkJobGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkJobGbn.Checked = True
        Me.chkJobGbn.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkJobGbn.Location = New System.Drawing.Point(189, 30)
        Me.chkJobGbn.Name = "chkJobGbn"
        Me.chkJobGbn.Size = New System.Drawing.Size(84, 16)
        Me.chkJobGbn.TabIndex = 95
        Me.chkJobGbn.Text = "리스트조회"
        Me.chkJobGbn.UseVisualStyleBackColor = False
        '
        'cboWkGrp
        '
        Me.cboWkGrp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWkGrp.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWkGrp.Location = New System.Drawing.Point(76, 4)
        Me.cboWkGrp.Name = "cboWkGrp"
        Me.cboWkGrp.Size = New System.Drawing.Size(205, 20)
        Me.cboWkGrp.TabIndex = 100
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label46.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label46.ForeColor = System.Drawing.Color.Black
        Me.Label46.Location = New System.Drawing.Point(5, 4)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(70, 21)
        Me.Label46.TabIndex = 99
        Me.Label46.Text = "작업그룹"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(173, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 12)
        Me.Label5.TabIndex = 137
        Me.Label5.Text = "~"
        Me.Label5.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(5, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 21)
        Me.Label1.TabIndex = 94
        Me.Label1.Text = "작업일자"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWkNo
        '
        Me.lblWkNo.AutoSize = True
        Me.lblWkNo.Location = New System.Drawing.Point(173, 56)
        Me.lblWkNo.Name = "lblWkNo"
        Me.lblWkNo.Size = New System.Drawing.Size(11, 12)
        Me.lblWkNo.TabIndex = 93
        Me.lblWkNo.Text = "~"
        '
        'txtWkNoE
        '
        Me.txtWkNoE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkNoE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkNoE.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoE.Location = New System.Drawing.Point(189, 51)
        Me.txtWkNoE.MaxLength = 4
        Me.txtWkNoE.Name = "txtWkNoE"
        Me.txtWkNoE.Size = New System.Drawing.Size(91, 21)
        Me.txtWkNoE.TabIndex = 92
        Me.txtWkNoE.Text = "9999"
        '
        'txtWkNoS
        '
        Me.txtWkNoS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWkNoS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWkNoS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWkNoS.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWkNoS.Location = New System.Drawing.Point(76, 51)
        Me.txtWkNoS.MaxLength = 4
        Me.txtWkNoS.Name = "txtWkNoS"
        Me.txtWkNoS.Size = New System.Drawing.Size(92, 21)
        Me.txtWkNoS.TabIndex = 91
        Me.txtWkNoS.Text = "0000"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label43.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.Black
        Me.Label43.Location = New System.Drawing.Point(5, 51)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(70, 21)
        Me.Label43.TabIndex = 90
        Me.Label43.Text = "작업번호"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpWkDt_e
        '
        Me.dtpWkDt_e.CustomFormat = "yyyy-MM-dd"
        Me.dtpWkDt_e.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpWkDt_e.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWkDt_e.Location = New System.Drawing.Point(189, 27)
        Me.dtpWkDt_e.Name = "dtpWkDt_e"
        Me.dtpWkDt_e.Size = New System.Drawing.Size(92, 21)
        Me.dtpWkDt_e.TabIndex = 136
        Me.dtpWkDt_e.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        Me.dtpWkDt_e.Visible = False
        '
        'btnClear_Tcls
        '
        Me.btnClear_Tcls.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear_Tcls.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_Tcls.Location = New System.Drawing.Point(34, 26)
        Me.btnClear_Tcls.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_Tcls.Name = "btnClear_Tcls"
        Me.btnClear_Tcls.Size = New System.Drawing.Size(43, 21)
        Me.btnClear_Tcls.TabIndex = 190
        Me.btnClear_Tcls.Text = "clear"
        Me.btnClear_Tcls.UseVisualStyleBackColor = True
        Me.btnClear_Tcls.Visible = False
        '
        'btnHelp_Tcls
        '
        Me.btnHelp_Tcls.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_Tcls.Image = CType(resources.GetObject("btnHelp_Tcls.Image"), System.Drawing.Image)
        Me.btnHelp_Tcls.Location = New System.Drawing.Point(5, 26)
        Me.btnHelp_Tcls.Margin = New System.Windows.Forms.Padding(0)
        Me.btnHelp_Tcls.Name = "btnHelp_Tcls"
        Me.btnHelp_Tcls.Size = New System.Drawing.Size(26, 21)
        Me.btnHelp_Tcls.TabIndex = 189
        Me.btnHelp_Tcls.UseVisualStyleBackColor = True
        Me.btnHelp_Tcls.Visible = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(5, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 21)
        Me.Label4.TabIndex = 101
        Me.Label4.Text = "검사선택"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label4.Visible = False
        '
        'dtpWkDt
        '
        Me.dtpWkDt.CustomFormat = "yyyy-MM-dd"
        Me.dtpWkDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpWkDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWkDt.Location = New System.Drawing.Point(76, 28)
        Me.dtpWkDt.Name = "dtpWkDt"
        Me.dtpWkDt.Size = New System.Drawing.Size(92, 21)
        Me.dtpWkDt.TabIndex = 89
        Me.dtpWkDt.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'lblTNMD
        '
        Me.lblTNMD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTNMD.BackColor = System.Drawing.Color.Thistle
        Me.lblTNMD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTNMD.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTNMD.ForeColor = System.Drawing.Color.Brown
        Me.lblTNMD.Location = New System.Drawing.Point(76, 4)
        Me.lblTNMD.Name = "lblTNMD"
        Me.lblTNMD.Size = New System.Drawing.Size(206, 105)
        Me.lblTNMD.TabIndex = 103
        Me.lblTNMD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblTNMD.Visible = False
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.lblEqnm)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.spdEq)
        Me.TabPage3.Controls.Add(Me.Label42)
        Me.TabPage3.Controls.Add(Me.dtpRst)
        Me.TabPage3.Location = New System.Drawing.Point(4, 24)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(286, 158)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "검사장비별"
        Me.TabPage3.UseVisualStyleBackColor = True
        Me.TabPage3.Visible = False
        '
        'lblEqnm
        '
        Me.lblEqnm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblEqnm.BackColor = System.Drawing.Color.Thistle
        Me.lblEqnm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEqnm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblEqnm.ForeColor = System.Drawing.Color.Brown
        Me.lblEqnm.Location = New System.Drawing.Point(76, 29)
        Me.lblEqnm.Name = "lblEqnm"
        Me.lblEqnm.Size = New System.Drawing.Size(206, 21)
        Me.lblEqnm.TabIndex = 104
        Me.lblEqnm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(5, 29)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 21)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "장비명"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdEq
        '
        Me.spdEq.DataSource = Nothing
        Me.spdEq.Location = New System.Drawing.Point(5, 52)
        Me.spdEq.Name = "spdEq"
        Me.spdEq.OcxState = CType(resources.GetObject("spdEq.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdEq.Size = New System.Drawing.Size(275, 102)
        Me.spdEq.TabIndex = 15
        '
        'Label42
        '
        Me.Label42.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label42.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label42.ForeColor = System.Drawing.Color.Black
        Me.Label42.Location = New System.Drawing.Point(4, 5)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(70, 21)
        Me.Label42.TabIndex = 14
        Me.Label42.Text = "결과일자"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpRst
        '
        Me.dtpRst.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpRst.Location = New System.Drawing.Point(75, 5)
        Me.dtpRst.Name = "dtpRst"
        Me.dtpRst.Size = New System.Drawing.Size(88, 21)
        Me.dtpRst.TabIndex = 13
        Me.dtpRst.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.btnQuery_wl)
        Me.TabPage4.Controls.Add(Me.cboRstFlg_wl)
        Me.TabPage4.Controls.Add(Me.dtpWLdte)
        Me.TabPage4.Controls.Add(Me.Label10)
        Me.TabPage4.Controls.Add(Me.lblTest_wl)
        Me.TabPage4.Controls.Add(Me.cboWL)
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.dtpWLdts)
        Me.TabPage4.Controls.Add(Me.Label8)
        Me.TabPage4.Controls.Add(Me.Label11)
        Me.TabPage4.Location = New System.Drawing.Point(4, 24)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(286, 158)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "W/L"
        Me.TabPage4.UseVisualStyleBackColor = True
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
        Me.btnQuery_wl.Location = New System.Drawing.Point(213, 26)
        Me.btnQuery_wl.Name = "btnQuery_wl"
        Me.btnQuery_wl.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery_wl.SideImage = Nothing
        Me.btnQuery_wl.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery_wl.Size = New System.Drawing.Size(68, 22)
        Me.btnQuery_wl.TabIndex = 143
        Me.btnQuery_wl.Text = "W/L 조회"
        Me.btnQuery_wl.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery_wl.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'cboRstFlg_wl
        '
        Me.cboRstFlg_wl.AutoCompleteCustomSource.AddRange(New String() {"[ ] 전체", "[N] 미완료", "[F] 완료"})
        Me.cboRstFlg_wl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRstFlg_wl.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboRstFlg_wl.Items.AddRange(New Object() {"[ ] 전체 ", "[N] 미완료", "[F] 완료"})
        Me.cboRstFlg_wl.Location = New System.Drawing.Point(76, 27)
        Me.cboRstFlg_wl.MaxDropDownItems = 10
        Me.cboRstFlg_wl.Name = "cboRstFlg_wl"
        Me.cboRstFlg_wl.Size = New System.Drawing.Size(133, 20)
        Me.cboRstFlg_wl.TabIndex = 141
        '
        'dtpWLdte
        '
        Me.dtpWLdte.CustomFormat = "yyyy-MM-dd"
        Me.dtpWLdte.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpWLdte.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWLdte.Location = New System.Drawing.Point(189, 4)
        Me.dtpWLdte.Name = "dtpWLdte"
        Me.dtpWLdte.Size = New System.Drawing.Size(92, 21)
        Me.dtpWLdte.TabIndex = 139
        Me.dtpWLdte.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(173, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(11, 12)
        Me.Label10.TabIndex = 138
        Me.Label10.Text = "~"
        Me.Label10.Visible = False
        '
        'lblTest_wl
        '
        Me.lblTest_wl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTest_wl.BackColor = System.Drawing.Color.Thistle
        Me.lblTest_wl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTest_wl.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest_wl.ForeColor = System.Drawing.Color.Brown
        Me.lblTest_wl.Location = New System.Drawing.Point(4, 72)
        Me.lblTest_wl.Name = "lblTest_wl"
        Me.lblTest_wl.Size = New System.Drawing.Size(277, 83)
        Me.lblTest_wl.TabIndex = 129
        '
        'cboWL
        '
        Me.cboWL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWL.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWL.Items.AddRange(New Object() {"자동화계", "특수계1", "툭스계2"})
        Me.cboWL.Location = New System.Drawing.Point(76, 49)
        Me.cboWL.MaxDropDownItems = 10
        Me.cboWL.Name = "cboWL"
        Me.cboWL.Size = New System.Drawing.Size(205, 20)
        Me.cboWL.TabIndex = 128
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(5, 49)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 21)
        Me.Label9.TabIndex = 127
        Me.Label9.Text = "W/L 제목"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpWLdts
        '
        Me.dtpWLdts.CustomFormat = "yyyy-MM-dd"
        Me.dtpWLdts.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpWLdts.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpWLdts.Location = New System.Drawing.Point(76, 4)
        Me.dtpWLdts.Name = "dtpWLdts"
        Me.dtpWLdts.Size = New System.Drawing.Size(92, 21)
        Me.dtpWLdts.TabIndex = 126
        Me.dtpWLdts.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(5, 26)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 21)
        Me.Label8.TabIndex = 125
        Me.Label8.Text = "구    분"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(5, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 21)
        Me.Label11.TabIndex = 140
        Me.Label11.Text = "W/L 일자"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel5.Controls.Add(Me.btnRst_ocs)
        Me.Panel5.Controls.Add(Me.chkMW)
        Me.Panel5.Controls.Add(Me.btnRerun)
        Me.Panel5.Controls.Add(Me.btnFN)
        Me.Panel5.Controls.Add(Me.btnReg)
        Me.Panel5.Controls.Add(Me.btnExit)
        Me.Panel5.Controls.Add(Me.cboRerun)
        Me.Panel5.Controls.Add(Me.txtID)
        Me.Panel5.Controls.Add(Me.btnRst_Clear)
        Me.Panel5.Controls.Add(Me.btnMW)
        Me.Panel5.Controls.Add(Me.btnClear)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 597)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1422, 32)
        Me.Panel5.TabIndex = 148
        '
        'btnRst_ocs
        '
        Me.btnRst_ocs.Location = New System.Drawing.Point(3, 1)
        Me.btnRst_ocs.Name = "btnRst_ocs"
        Me.btnRst_ocs.Size = New System.Drawing.Size(75, 26)
        Me.btnRst_ocs.TabIndex = 197
        Me.btnRst_ocs.TabStop = False
        Me.btnRst_ocs.Text = "OCS"
        Me.btnRst_ocs.UseVisualStyleBackColor = True
        Me.btnRst_ocs.Visible = False
        '
        'chkMW
        '
        Me.chkMW.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkMW.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkMW.Location = New System.Drawing.Point(660, 8)
        Me.chkMW.Name = "chkMW"
        Me.chkMW.Size = New System.Drawing.Size(73, 17)
        Me.chkMW.TabIndex = 196
        Me.chkMW.Text = "중간보고"
        Me.chkMW.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkMW.UseVisualStyleBackColor = False
        '
        'btnRerun
        '
        Me.btnRerun.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRerun.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnRerun.ColorFillBlend = CBlendItems2
        Me.btnRerun.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnRerun.Corners.All = CType(6, Short)
        Me.btnRerun.Corners.LowerLeft = CType(6, Short)
        Me.btnRerun.Corners.LowerRight = CType(6, Short)
        Me.btnRerun.Corners.UpperLeft = CType(6, Short)
        Me.btnRerun.Corners.UpperRight = CType(6, Short)
        Me.btnRerun.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnRerun.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnRerun.FocalPoints.CenterPtX = 0.5208333!
        Me.btnRerun.FocalPoints.CenterPtY = 0.24!
        Me.btnRerun.FocalPoints.FocusPtX = 0.0!
        Me.btnRerun.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRerun.FocusPtTracker = DesignerRectTracker4
        Me.btnRerun.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnRerun.ForeColor = System.Drawing.Color.White
        Me.btnRerun.Image = Nothing
        Me.btnRerun.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRerun.ImageIndex = 0
        Me.btnRerun.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnRerun.Location = New System.Drawing.Point(836, 3)
        Me.btnRerun.Name = "btnRerun"
        Me.btnRerun.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnRerun.SideImage = Nothing
        Me.btnRerun.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnRerun.Size = New System.Drawing.Size(96, 25)
        Me.btnRerun.TabIndex = 195
        Me.btnRerun.Text = "재검"
        Me.btnRerun.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnRerun.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnFN
        '
        Me.btnFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnFN.ColorFillBlend = CBlendItems3
        Me.btnFN.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnFN.Corners.All = CType(6, Short)
        Me.btnFN.Corners.LowerLeft = CType(6, Short)
        Me.btnFN.Corners.LowerRight = CType(6, Short)
        Me.btnFN.Corners.UpperLeft = CType(6, Short)
        Me.btnFN.Corners.UpperRight = CType(6, Short)
        Me.btnFN.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnFN.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnFN.FocalPoints.CenterPtX = 0.5!
        Me.btnFN.FocalPoints.CenterPtY = 0.08!
        Me.btnFN.FocalPoints.FocusPtX = 0.0!
        Me.btnFN.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.FocusPtTracker = DesignerRectTracker6
        Me.btnFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnFN.ForeColor = System.Drawing.Color.White
        Me.btnFN.Image = Nothing
        Me.btnFN.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFN.ImageIndex = 0
        Me.btnFN.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnFN.Location = New System.Drawing.Point(933, 3)
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
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems4
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
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker8
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(1127, 3)
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
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
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
        Me.btnExit.Location = New System.Drawing.Point(1322, 3)
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
        'cboRerun
        '
        Me.cboRerun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRerun.Items.AddRange(New Object() {"Standard", "Rerun1", "Rerun2"})
        Me.cboRerun.Location = New System.Drawing.Point(218, 8)
        Me.cboRerun.Name = "cboRerun"
        Me.cboRerun.Size = New System.Drawing.Size(84, 20)
        Me.cboRerun.TabIndex = 153
        Me.cboRerun.Visible = False
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(307, 6)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(116, 21)
        Me.txtID.TabIndex = 148
        Me.txtID.Text = "ACK"
        Me.txtID.Visible = False
        '
        'btnRst_Clear
        '
        Me.btnRst_Clear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRst_Clear.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnRst_Clear.ColorFillBlend = CBlendItems6
        Me.btnRst_Clear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnRst_Clear.Corners.All = CType(6, Short)
        Me.btnRst_Clear.Corners.LowerLeft = CType(6, Short)
        Me.btnRst_Clear.Corners.LowerRight = CType(6, Short)
        Me.btnRst_Clear.Corners.UpperLeft = CType(6, Short)
        Me.btnRst_Clear.Corners.UpperRight = CType(6, Short)
        Me.btnRst_Clear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnRst_Clear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnRst_Clear.FocalPoints.CenterPtX = 0.40625!
        Me.btnRst_Clear.FocalPoints.CenterPtY = 0.28!
        Me.btnRst_Clear.FocalPoints.FocusPtX = 0.0!
        Me.btnRst_Clear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnRst_Clear.FocusPtTracker = DesignerRectTracker12
        Me.btnRst_Clear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnRst_Clear.ForeColor = System.Drawing.Color.White
        Me.btnRst_Clear.Image = Nothing
        Me.btnRst_Clear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnRst_Clear.ImageIndex = 0
        Me.btnRst_Clear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnRst_Clear.Location = New System.Drawing.Point(739, 3)
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
        Me.btnMW.Location = New System.Drawing.Point(1030, 3)
        Me.btnMW.Name = "btnMW"
        Me.btnMW.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnMW.SideImage = Nothing
        Me.btnMW.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnMW.Size = New System.Drawing.Size(96, 25)
        Me.btnMW.TabIndex = 192
        Me.btnMW.Text = "결과확인(F11)"
        Me.btnMW.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnMW.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker15.IsActive = False
        DesignerRectTracker15.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker15.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker15
        CBlendItems8.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems8.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems8
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
        DesignerRectTracker16.IsActive = False
        DesignerRectTracker16.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker16.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker16
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1224, 3)
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
        'btnMove
        '
        Me.btnMove.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMove.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMove.Location = New System.Drawing.Point(300, 5)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(8, 591)
        Me.btnMove.TabIndex = 163
        Me.btnMove.Text = "◀"
        Me.btnMove.UseVisualStyleBackColor = False
        '
        'AxResult
        '
        Me.AxResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AxResult.BcNoAll = False
        Me.AxResult.ColHiddenYn = False
        Me.AxResult.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.AxResult.Location = New System.Drawing.Point(310, 118)
        Me.AxResult.Name = "AxResult"
        Me.AxResult.Size = New System.Drawing.Size(1111, 477)
        Me.AxResult.TabIndex = 168
        Me.AxResult.UseBloodBank = False
        Me.AxResult.UseDoctor = False
        '
        'btnDown
        '
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Font = New System.Drawing.Font("굴림", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDown.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnDown.Location = New System.Drawing.Point(1372, 59)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(49, 55)
        Me.btnDown.TabIndex = 180
        Me.btnDown.Text = "▼"
        '
        'btnUp
        '
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Font = New System.Drawing.Font("굴림", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUp.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.btnUp.Location = New System.Drawing.Point(1372, 3)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(49, 55)
        Me.btnUp.TabIndex = 179
        Me.btnUp.Text = "▲"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Panel11)
        Me.GroupBox3.Controls.Add(Me.Panel10)
        Me.GroupBox3.Controls.Add(Me.Panel8)
        Me.GroupBox3.Controls.Add(Me.Panel7)
        Me.GroupBox3.Controls.Add(Me.Panel6)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cboRstFlg)
        Me.GroupBox3.Controls.Add(Me.Panel2)
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Controls.Add(Me.Panel9)
        Me.GroupBox3.Controls.Add(Me.btnQuery)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 237)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(295, 86)
        Me.GroupBox3.TabIndex = 186
        Me.GroupBox3.TabStop = False
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel11.Controls.Add(Me.chkER)
        Me.Panel11.Location = New System.Drawing.Point(4, 59)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(67, 22)
        Me.Panel11.TabIndex = 143
        '
        'chkER
        '
        Me.chkER.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkER.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkER.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkER.ForeColor = System.Drawing.Color.Black
        Me.chkER.Location = New System.Drawing.Point(2, 1)
        Me.chkER.Name = "chkER"
        Me.chkER.Size = New System.Drawing.Size(62, 20)
        Me.chkER.TabIndex = 125
        Me.chkER.Text = "응급"
        Me.chkER.UseVisualStyleBackColor = False
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel10.Controls.Add(Me.chkNotRerun)
        Me.Panel10.Location = New System.Drawing.Point(205, 59)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(86, 22)
        Me.Panel10.TabIndex = 140
        '
        'chkNotRerun
        '
        Me.chkNotRerun.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkNotRerun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkNotRerun.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkNotRerun.ForeColor = System.Drawing.Color.Black
        Me.chkNotRerun.Location = New System.Drawing.Point(2, 1)
        Me.chkNotRerun.Name = "chkNotRerun"
        Me.chkNotRerun.Size = New System.Drawing.Size(79, 20)
        Me.chkNotRerun.TabIndex = 125
        Me.chkNotRerun.Text = "재검제외"
        Me.chkNotRerun.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel8.Controls.Add(Me.chkHL)
        Me.Panel8.Location = New System.Drawing.Point(80, 35)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(59, 22)
        Me.Panel8.TabIndex = 139
        '
        'chkHL
        '
        Me.chkHL.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkHL.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkHL.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkHL.ForeColor = System.Drawing.Color.Black
        Me.chkHL.Location = New System.Drawing.Point(2, 1)
        Me.chkHL.Name = "chkHL"
        Me.chkHL.Size = New System.Drawing.Size(55, 20)
        Me.chkHL.TabIndex = 125
        Me.chkHL.Text = "H/L"
        Me.chkHL.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel7.Controls.Add(Me.chkA)
        Me.Panel7.Location = New System.Drawing.Point(216, 35)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(75, 22)
        Me.Panel7.TabIndex = 138
        '
        'chkA
        '
        Me.chkA.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkA.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkA.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkA.ForeColor = System.Drawing.Color.Black
        Me.chkA.Location = New System.Drawing.Point(2, 1)
        Me.chkA.Name = "chkA"
        Me.chkA.Size = New System.Drawing.Size(71, 20)
        Me.chkA.TabIndex = 126
        Me.chkA.Text = "Alert"
        Me.chkA.UseVisualStyleBackColor = False
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel6.Controls.Add(Me.chkReRun)
        Me.Panel6.Location = New System.Drawing.Point(139, 59)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(66, 22)
        Me.Panel6.TabIndex = 137
        '
        'chkReRun
        '
        Me.chkReRun.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkReRun.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkReRun.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkReRun.ForeColor = System.Drawing.Color.Black
        Me.chkReRun.Location = New System.Drawing.Point(2, 1)
        Me.chkReRun.Name = "chkReRun"
        Me.chkReRun.Size = New System.Drawing.Size(62, 20)
        Me.chkReRun.TabIndex = 125
        Me.chkReRun.Text = "재검"
        Me.chkReRun.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(4, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 20)
        Me.Label3.TabIndex = 136
        Me.Label3.Text = "검사상태"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboRstFlg
        '
        Me.cboRstFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRstFlg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboRstFlg.FormattingEnabled = True
        Me.cboRstFlg.Items.AddRange(New Object() {"[A] 전체", "[3] 완료", "[2] Review", "[1] 검사", "[0] 미결과"})
        Me.cboRstFlg.Location = New System.Drawing.Point(85, 12)
        Me.cboRstFlg.Name = "cboRstFlg"
        Me.cboRstFlg.Size = New System.Drawing.Size(137, 20)
        Me.cboRstFlg.TabIndex = 135
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel2.Controls.Add(Me.chkPDC)
        Me.Panel2.Location = New System.Drawing.Point(139, 35)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(77, 22)
        Me.Panel2.TabIndex = 134
        '
        'chkPDC
        '
        Me.chkPDC.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkPDC.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkPDC.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkPDC.ForeColor = System.Drawing.Color.Black
        Me.chkPDC.Location = New System.Drawing.Point(3, 1)
        Me.chkPDC.Name = "chkPDC"
        Me.chkPDC.Size = New System.Drawing.Size(71, 20)
        Me.chkPDC.TabIndex = 126
        Me.chkPDC.Text = "P/D/C"
        Me.chkPDC.UseVisualStyleBackColor = False
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel4.Controls.Add(Me.chkFlag)
        Me.Panel4.Location = New System.Drawing.Point(71, 59)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(68, 22)
        Me.Panel4.TabIndex = 133
        '
        'chkFlag
        '
        Me.chkFlag.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkFlag.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkFlag.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkFlag.ForeColor = System.Drawing.Color.Black
        Me.chkFlag.Location = New System.Drawing.Point(2, 1)
        Me.chkFlag.Name = "chkFlag"
        Me.chkFlag.Size = New System.Drawing.Size(63, 20)
        Me.chkFlag.TabIndex = 125
        Me.chkFlag.Text = "FLAG"
        Me.chkFlag.UseVisualStyleBackColor = False
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.Panel9.Controls.Add(Me.chkN)
        Me.Panel9.Location = New System.Drawing.Point(4, 35)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(76, 22)
        Me.Panel9.TabIndex = 130
        '
        'chkN
        '
        Me.chkN.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkN.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.chkN.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkN.ForeColor = System.Drawing.Color.Black
        Me.chkN.Location = New System.Drawing.Point(2, 1)
        Me.chkN.Name = "chkN"
        Me.chkN.Size = New System.Drawing.Size(71, 20)
        Me.chkN.TabIndex = 125
        Me.chkN.Text = "Normal"
        Me.chkN.UseVisualStyleBackColor = False
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker17.IsActive = False
        DesignerRectTracker17.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker17.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker17
        CBlendItems9.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems9.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems9
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.5147059!
        Me.btnQuery.FocalPoints.CenterPtY = 0.0!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker18.IsActive = False
        DesignerRectTracker18.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker18.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker18
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(223, 11)
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
        'chkMoveCol
        '
        Me.chkMoveCol.AutoSize = True
        Me.chkMoveCol.Location = New System.Drawing.Point(194, 327)
        Me.chkMoveCol.Name = "chkMoveCol"
        Me.chkMoveCol.Size = New System.Drawing.Size(104, 16)
        Me.chkMoveCol.TabIndex = 187
        Me.chkMoveCol.Text = "컬럼 이동 모드"
        Me.chkMoveCol.UseVisualStyleBackColor = True
        '
        'AxPatInfo
        '
        Me.AxPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AxPatInfo.BcNo = ""
        Me.AxPatInfo.Location = New System.Drawing.Point(309, 1)
        Me.AxPatInfo.Name = "AxPatInfo"
        Me.AxPatInfo.RegNo = ""
        Me.AxPatInfo.Size = New System.Drawing.Size(1061, 114)
        Me.AxPatInfo.SlipCd = ""
        Me.AxPatInfo.TabIndex = 188
        '
        'btnHistory
        '
        Me.btnHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHistory.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnHistory.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker19.IsActive = False
        DesignerRectTracker19.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker19.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHistory.CenterPtTracker = DesignerRectTracker19
        CBlendItems10.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems10.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnHistory.ColorFillBlend = CBlendItems10
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
        DesignerRectTracker20.IsActive = False
        DesignerRectTracker20.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker20.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnHistory.FocusPtTracker = DesignerRectTracker20
        Me.btnHistory.Image = Nothing
        Me.btnHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnHistory.ImageIndex = 0
        Me.btnHistory.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnHistory.Location = New System.Drawing.Point(855, 117)
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
        'btnQuery_pat
        '
        Me.btnQuery_pat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuery_pat.Location = New System.Drawing.Point(1329, 118)
        Me.btnQuery_pat.Name = "btnQuery_pat"
        Me.btnQuery_pat.Size = New System.Drawing.Size(92, 22)
        Me.btnQuery_pat.TabIndex = 219
        Me.btnQuery_pat.TabStop = False
        Me.btnQuery_pat.Text = "환자진단조회"
        Me.btnQuery_pat.UseVisualStyleBackColor = True
        '
        'chkConQC
        '
        Me.chkConQC.AutoSize = True
        Me.chkConQC.Location = New System.Drawing.Point(5, 323)
        Me.chkConQC.Name = "chkConQC"
        Me.chkConQC.Size = New System.Drawing.Size(66, 16)
        Me.chkConQC.TabIndex = 220
        Me.chkConQC.Text = "QC연동"
        Me.chkConQC.UseVisualStyleBackColor = True
        '
        'FGR03
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1422, 629)
        Me.Controls.Add(Me.chkConQC)
        Me.Controls.Add(Me.btnQuery_pat)
        Me.Controls.Add(Me.tbcJob)
        Me.Controls.Add(Me.btnHistory)
        Me.Controls.Add(Me.AxResult)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.chkMoveCol)
        Me.Controls.Add(Me.chkSel_List)
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox12)
        Me.Controls.Add(Me.AxPatInfo)
        Me.Controls.Add(Me.btnBFN)
        Me.KeyPreview = True
        Me.Name = "FGR03"
        Me.Text = "담당자별 결과저장 및 보고"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdList_eq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox12.ResumeLayout(False)
        Me.GroupBox12.PerformLayout()
        Me.tbcJob.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.spdTGrp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.spdEq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggle.Click

        Dim CommFn As New Fn
        'CommFn.SearchToggle(lblSearch, btnToggle, enumToggle.BcnoToRegno, txtSearch)
        Fn.SearchToggle(Me.lblSearch, Me.btnToggle, enumToggle.Regno_Name_Bcno, Me.txtSearch)

        txtSearch.Text = ""
        txtSearch.Focus()

    End Sub


    ' 화면 초기화
    Private Sub fnSpread_Init(ByVal sState As String)
        Dim sFn As String = "Sub fnInitSpread()"
        Try
            If sState = "텍스트박스" Then
                With spdList
                    .MaxRows = 0
                End With

                With spdList_eq
                    .MaxRows = 0
                End With
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)

        End Try
    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        txtSearch.SelectionStart = 0
        txtSearch.SelectAll()

    End Sub

    ' 검체번호 입력 처리
    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sFn As String = "Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown"
        Try

            Me.txtSearch.Text = Me.txtSearch.Text.Trim

            Me.AxResult.FORMID = Me.Name
            If e.KeyCode = Windows.Forms.Keys.Enter Then
                Dim sTestCds As String = ""
                Dim sTGrpCds As String = ""
                Dim sWGrpCd As String = ""
                Dim sEqCd As String = ""

                Select Case tbcJob.SelectedTab.Text
                    Case "검사그룹별"
                        With spdTGrp
                            For iRow As Integer = 1 To .MaxRows
                                .Row = iRow
                                .Col = .GetColFromID("chk")
                                If .Text = "1" Then
                                    .Col = .GetColFromID("tgrpcd") : sTestCds += .Text
                                End If
                            Next

                            If sTGrpCds <> "" Then sTGrpCds = "'" + sTGrpCds.Substring(0, sTGrpCds.Length - 1).Replace(",", "', '") + "'"
                        End With
                    Case "작업그룹별"
                        If lblTNMD.Text <> "" Then sTestCds = "'" + lblTNMD.Tag.ToString.Replace(",", "','") + "'"

                        If sTestCds = "" Then sWGrpCd = Ctrl.Get_Code(cboWkGrp)

                    Case "검사장비별"
                        sEqCd = Ctrl.Get_Code(Me.lblEqnm)
                End Select

                msState = "텍스트박스"
                If lblSearch.Text = "검체번호" Then
                    Dim sBCNO As String = Trim(Me.txtSearch.Text.Replace("-", ""))

                    If sBCNO = "" Then
                        MsgBox("검체번호를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                        Exit Sub
                    End If
                    Me.txtSearch.Text = txtSearch.Text.Replace("-", "").Trim()

                    '< add freety 2005/09/14 : -가 포함된 검체번호 입력도 허용
                    sBCNO = sBCNO.Replace("-", "")
                    '>

                    If Len(sBCNO) = 11 Or Len(sBCNO) = 12 Then
                        sBCNO = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(Mid(sBCNO, 1, 11))
                    End If

                    If sBCNO.Length = 14 Then sBCNO += "0"
                    If sBCNO.Length < 15 Then
                        Me.txtSearch.SelectAll()
                        Return
                    End If

                    Me.AxPatInfo.BcNo = sBCNO

                    If Not AxPatInfo.fnDisplay_Data() Then
                        MsgBox("접수된 검체가 없습니다!!")
                        Return
                    End If

                    Me.AxResult.RegNo = AxPatInfo.RegNo
                    Me.AxResult.PatName = AxPatInfo.PatNm
                    Me.AxResult.SexAge = AxPatInfo.SexAge
                    Me.AxResult.DeptCd = AxPatInfo.DeptName
                    Me.AxResult.FnDt = AxPatInfo.FnDt
                    Me.AxResult.TestCds = sTestCds
                    Me.AxResult.TgrpCds = sTGrpCds
                    Me.AxResult.WKgrpCd = sWGrpCd
                    Me.AxResult.EqCd = sEqCd

                    Me.AxResult.sbDisplay_Data(sBCNO)
                    Me.AxResult.sbFocus()
                    Me.AxResult.Focus()

                    AxPatInfo.BcNo = sBCNO


                    txtSearch.SelectAll()
                    txtSearch.Focus()

                Else
                    ' 등록번호 또는 성명 입력시 처리
                    Dim sRegNo As String

                    If lblSearch.Text = "성    명" Then
                        sRegNo = fnFind_RegNo(txtSearch.Text)
                    Else
                        sRegNo = txtSearch.Text.Trim

                        If IsNumeric(sRegNo.Substring(0, 1)) Then
                            sRegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                        Else
                            sRegNo = sRegNo.Substring(0, 1).ToUpper + sRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                        End If

                        Me.txtSearch.Text = sRegNo
                    End If

                    Me.txtSearch.Tag = sRegNo
                    If sRegNo = "" Then
                        MsgBox("등록번호를 입력해 주세요.", MsgBoxStyle.Information, Me.Text)
                        Return
                    End If

                    btnQuery_Click(Nothing, Nothing)
                    Me.txtSearch.Text = ""
                End If
            End If
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_WGrp()
        Dim sFn As String = "Sub sbDisplay_WkGrpNm()"
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_WKGrp_List(Ctrl.Get_Code(Me.cboPartSlip))

            Me.cboWkGrp.Items.Clear()

            For ix As Integer = 0 To dt.Rows.Count - 1
                Dim sTmp As String = "[" + dt.Rows(ix).Item("wkgrpcd").ToString.Trim + "] " + dt.Rows(ix).Item("wkgrpnmd").ToString.Trim + Space(200) + "|" + dt.Rows(ix).Item("wkgrpgbn").ToString.Trim
                Me.cboWkGrp.Items.Add(sTmp)
            Next
        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Eq()

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Eq_List(Ctrl.Get_Code(Me.cboPartSlip))

        With Me.spdEq
            .MaxRows = 0

            .ReDraw = False
            .MaxRows = dt.Rows.Count

            For iRow As Integer = 1 To dt.Rows.Count

                For ix As Integer = 1 To dt.Columns.Count
                    Dim iCol As Integer = .GetColFromID(dt.Columns(ix - 1).ColumnName.ToLower())

                    If iCol > 0 Then
                        .Col = iCol
                        .Row = iRow

                        Select Case iCol
                            Case .GetColFromID("eqnm")
                                .Text = "[" + dt.Rows(iRow - 1).Item("eqcd").ToString.Trim + "] " + dt.Rows(iRow - 1).Item(ix - 1).ToString().Trim
                            Case Else
                                .Text = dt.Rows(iRow - 1).Item(ix - 1).ToString().Trim
                        End Select

                    End If
                Next

            Next

            .ReDraw = True

        End With
    End Sub

    Private Sub FGR03_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
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
                btnQuery_Click(Nothing, Nothing)

            Case Keys.F9
                btnReg_ButtonClick(btnMW, New System.EventArgs)
            Case Keys.F11
                btnMW_ButtonClick(btnMW, New System.EventArgs)
            Case Keys.F12
                btnFN_ButtonClick(btnFN, New System.EventArgs)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    ' 초기작업
    Private Sub FGR03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sFn As String = "Sub FGR03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load"
        Try
            DS_FormDesige.sbInti(Me)

            STU_AUTHORITY.UsrID = USER_INFO.USRID

            cboRstFlg.SelectedIndex = 0

            spdTGrp.MaxRows = 0
            spdEq.MaxRows = 0

            sbSpread_Col_HiddenInfo(spdList, malList)

            Me.dtpWkDt.Value = Now
            Me.dtpWkDt_e.Value = Now

            Me.dtpTkdtS.Value = Now
            Me.dtpTkDtE.Value = Now

            Me.dtpRst.Value = Now
            Me.dtpWLdts.Value = Now
            Me.dtpWLdte.Value = Now

            sbDisplay_Slip()    '-- 검사분야 표시  
            sbDisplay_TGrp()    '-- 검사그룹
            sbDisplay_Eq()      '-- 검사장비

            Dim sTmp As String = COMMON.CommXML.getOneElementXML(msXmlDir, msPartSlip, "PARTSLIP")
            Try
                If sTmp.IndexOf("^"c) < 0 Then
                    Me.cboQryGbn.SelectedIndex = 1
                    sbDisplay_Slip() ' 검사분야 표시 
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
                        sbDisplay_Slip() ' 검사분야 표시 
                    End If

                    If CInt(sTmp.Split("^"c)(1)) < Me.cboPartSlip.Items.Count Then
                        Me.cboPartSlip.SelectedIndex = CInt(sTmp.Split("^"c)(1))
                    Else
                        If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception

            End Try

            sTmp = COMMON.CommXML.getOneElementXML(msXmlDir, msWGFile, "WKGRP")
            If sTmp <> "" Then
                If Val(sTmp) < Me.cboWkGrp.Items.Count Then Me.cboWkGrp.SelectedIndex = CInt(IIf(sTmp = "", 0, sTmp))
            End If

            sbSet_TGRPCD()
            sbSet_EQCD()
            sbSet_Tab()    ' 담당자별 결과저장 TAB 설정 

            AxPatInfo.UsrLevel = STU_AUTHORITY.UsrID
            AxPatInfo.sbDisplay_Init()

            AxResult.UseBloodBank = mbBloodBankYN
            AxResult.Form = Me
            AxResult.ColHiddenYn = True
            AxResult.UseDoctor = False
            AxResult.sbDisplay_Init("ALL")

            Dim load_casegbn As String = ""

            spdList.MaxRows = 0
            spdList_eq.MaxRows = 0

            '< yjlee
            Me.txtSearch.Focus()
            '> 

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            MsgBox(msFile & sFn & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub sbSet_EQCD()
        Dim sEqCd As String = COMMON.CommXML.getOneElementXML(msXmlDir, msEQFile, "EQCD")

        With spdEq
            For ix As Integer = 1 To .MaxRows
                .Row = ix
                .Col = .GetColFromID("eqcd")
                If .Text = sEqCd Then
                    .Row = ix
                    .Col = .GetColFromID("eqcd") : Me.lblEqnm.Tag = .Text
                    .Col = .GetColFromID("eqnm") : Me.lblEqnm.Text = .Text
                    Return
                End If
            Next
        End With
    End Sub

    Private Sub sbSet_TGRPCD()

        Dim sBuf() As String
        sBuf = COMMON.CommXML.getOneElementXML(msXmlDir, msTGFile, "TGCD").Split(","c)
        For ix As Integer = 0 To UBound(sBuf) - 1
            With spdTGrp
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("tgrpcd")
                    If .Text = sBuf(ix) Then
                        .Row = iRow
                        .Col = .GetColFromID("chk") : .Text = "1"
                    End If
                Next
            End With
        Next

    End Sub

    ' 선택된 탭 정보 표시
    Private Sub sbSet_Tab()
        ' 담당계 정보 가져오기

        With spdList
            .BringToFront()
        End With

        Select Case COMMON.CommXML.getOneElementXML(msXmlDir, msTABFile, "TAB")
            Case "검사그룹별"
                With spdList
                    .Row = 0
                    .Col = .GetColFromID("bcno") : .ColHidden = False : .set_ColWidth(.GetColFromID("bcno"), 12.5)
                    .Col = .GetColFromID("workno") : .ColHidden = True
                End With
            Case "작업그룹별"
                With spdList
                    .Row = 0
                    .Col = .GetColFromID("workno") : .ColHidden = False : .set_ColWidth(.GetColFromID("workno"), 11.5)
                    .Col = .GetColFromID("bcno") : .ColHidden = True
                End With

                tbcJob.SelectedTab = TabPage2
            Case "검사장비별"
                tbcJob.SelectedTab = TabPage3
        End Select

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        AxPatInfo.sbDisplay_Init()
        AxResult.sbDisplay_Init("ALL")

        spdList.MaxRows = 0
        spdList_eq.MaxRows = 0

    End Sub

    Private Sub spdSpcList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent, spdList_eq.ClickEvent

        If e.row < 1 Then Exit Sub

        Dim spd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

        If e.col = spd.GetColFromID("chk") Then
            With spd
                .Row = e.row
                .Col = e.col : .Text = IIf(.Text = "1", "", "1").ToString
            End With
        Else
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim sBcNo As String = ""
            Dim sPartSlip As String = ""

            With spd
                .Row = e.row
                .Col = .GetColFromID("bcno") : sBcNo = .Text.Replace("-", "").Trim
                .Col = .GetColFromID("partslip") : sPartSlip = .Text

                If Me.lblSearch.Text = "검체번호" Then Me.txtSearch.Text = sBcNo

            End With

            '20210406 jhs QC 데이터 제거
            If sBcNo.Length = 8 Then
                Return
            End If
            '-------------------------

            Me.AxPatInfo.BcNo = sBcNo
            Me.AxPatInfo.SlipCd = IIf(Me.tbcJob.SelectedTab.Text = "작업그룹별", Ctrl.Get_Code(cboPartSlip), "").ToString
            Me.AxPatInfo.fnDisplay_Data()

            Me.AxResult.FORMID = Me.Name  ''' 정은추가  
            Me.AxResult.Form = Me
            Me.AxResult.RegNo = AxPatInfo.RegNo
            Me.AxResult.PatName = AxPatInfo.PatNm
            Me.AxResult.SexAge = AxPatInfo.SexAge
            Me.AxResult.DeptCd = AxPatInfo.DeptName
            Me.AxResult.FnDt = AxPatInfo.FnDt
            Me.AxResult.AboRh = AxPatInfo.ABORh

            Select Case Me.tbcJob.SelectedTab.Text
                Case "검사그룹별"

                    Dim sTGrpCds As String = ""

                    With spdTGrp
                        For ix As Integer = 1 To .MaxRows
                            .Row = ix
                            .Col = .GetColFromID("chk") : Dim sChk As String = .Text
                            .Col = .GetColFromID("tgrpcd") : Dim sTgrpCd As String = .Text

                            If sChk = "1" Then
                                sTGrpCds += IIf(sTGrpCds = "", "", ",").ToString + sTgrpCd
                            End If
                        Next
                    End With

                    Me.AxResult.TestCds = ""
                    Me.AxResult.SlipCd = ""
                    Me.AxResult.WKgrpCd = ""
                    Me.AxResult.TgrpCds = sTGrpCds
                    Me.AxResult.EqCd = ""

                Case "작업그룹별"
                    Me.AxResult.TestCds = ""
                    Me.AxResult.SlipCd = sPartSlip
                    Me.AxResult.WKgrpCd = Ctrl.Get_Code(cboWkGrp)

                    Me.AxResult.TgrpCds = ""
                    Me.AxResult.EqCd = ""

                Case "검사장비별"
                    Me.AxResult.TestCds = ""
                    Me.AxResult.SlipCd = ""
                    Me.AxResult.TgrpCds = ""
                    Me.AxResult.WKgrpCd = Ctrl.Get_Code(cboWkGrp)
                    Me.AxResult.EqCd = Ctrl.Get_Code(Me.lblEqnm.Text)

                Case "W/L"
                    If Me.lblTest_wl.Text <> "" Then
                        Me.AxResult.SlipCd = sPartSlip
                        Me.AxResult.WKgrpCd = ""
                        Me.AxResult.TgrpCds = ""
                        Me.AxResult.EqCd = ""
                        Me.AxResult.TestCds = Me.lblTest_wl.Tag.ToString.Split("^"c)(0).Replace("|", ",")
                    End If
            End Select

            Me.AxResult.sbDisplay_Data(sBcNo)
            Me.AxResult.sbFocus()

            spd.Focus()

            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

    End Sub

    Private Sub spdTGroup_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdList.KeyDownEvent
        Dim spdLocal As AxFPSpreadADO.AxfpSpread
        spdLocal = CType(sender, AxFPSpreadADO.AxfpSpread)

        Select Case e.keyCode
            Case 123
#If DEBUG Then
                Select Case spdLocal.Name
                    Case "spdTGroup"

                    Case "spdSpcList"
                        sbSpread_Col_ShowHidden(spdLocal, malList)

                    Case "spdEq"
                End Select

#End If
        End Select
    End Sub


    Private Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPartSlip.SelectedIndexChanged

        sbDisplay_wl()
        sbDisplay_WGrp()
        sbDisplay_TGrp()
        sbDisplay_Eq()

        Me.btnClear_Click(Nothing, Nothing)
        Me.txtSearch.Text = ""
        Me.lblEqnm.Text = ""

        COMMON.CommXML.setOneElementXML(msXmlDir, msPartSlip, "PARTSLIP", Me.cboQryGbn.SelectedIndex.ToString + "^" + Me.cboPartSlip.SelectedIndex.ToString)

    End Sub


    Private Sub spdEq_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdEq.LeaveCell
        With spdEq
            If e.newRow > 0 Then
                spdEq_ClickEvent(spdEq, New AxFPSpreadADO._DSpreadEvents_ClickEvent(e.newCol, e.newRow))
            End If
        End With
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcJob.SelectedIndexChanged

        If Me.tbcJob.SelectedTab.Text = "" Then
            Exit Sub
        End If

        AxPatInfo.sbDisplay_Init()
        AxResult.sbDisplay_Init("ALL")
        spdList.MaxRows = 0
        spdList_eq.MaxRows = 0

        AxResult.Refresh()

        Select Case Me.tbcJob.SelectedTab.Text
            Case "검사그룹별"
                With spdList
                    .BringToFront()

                    .Row = 0
                    .Col = .GetColFromID("bcno") : .ColHidden = False : .set_ColWidth(.GetColFromID("bcno"), 12.5)
                    .Col = .GetColFromID("workno") : .ColHidden = True
                    .Row = 0
                    .Col = .GetColFromID("chk") : .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                End With
            Case "작업그룹별", "W/L"
                With spdList
                    .BringToFront()

                    .Row = 0
                    .Col = .GetColFromID("bcno") : .ColHidden = True
                    .Col = .GetColFromID("workno") : .ColHidden = False : .set_ColWidth(.GetColFromID("workno"), 4)
                    .Row = 0
                    .Col = .GetColFromID("workno") : .Action = FPSpreadADO.ActionConstants.ActionActiveCell

                End With
            Case "검사장비별"
                With spdList_eq
                    .BringToFront()
                End With

        End Select

        COMMON.CommXML.setOneElementXML(msXmlDir, msTABFile, "TAB", tbcJob.SelectedTab.Text)

    End Sub

    Private Sub spdEq_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdEq.ClickEvent

        With spdEq
            If e.row > 0 Then
                .Row = e.row
                .Col = .GetColFromID("eqnm") : Me.lblEqnm.Text = .Text
                .Col = .GetColFromID("eqcd") : Me.lblEqnm.Tag = .Text
            End If
        End With

        COMMON.CommXML.setOneElementXML(msXmlDir, msEQFile, "EQCD", Me.lblEqnm.Tag.ToString)

    End Sub

    Private Sub btnMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMove.Click
        If btnMove.Text = "◀" Then
            Me.btnMove.Left = 1
            Me.AxPatInfo.Left = 9 : Me.AxPatInfo.Width = Me.Width - btnDown.Width - 20
            Me.AxResult.Left = 9 : Me.AxResult.Width = Me.Width - 15

            Me.btnHistory.Left = 9

            Me.chkMoveCol.Visible = False
            Me.chkSel_List.Visible = False

            Me.btnMove.Text = "▶"
        Else
            Me.btnMove.Left = 300
            Me.AxPatInfo.Left = 309 : Me.AxPatInfo.Width -= 300
            Me.AxResult.Left = 309 : Me.AxResult.Width -= 300

            Me.btnHistory.Left = 309

            Me.chkMoveCol.Visible = True
            Me.chkSel_List.Visible = True

            Me.btnMove.Text = "◀"
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click

        Dim sFn As String = "Handles btnQuery.Click"

        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        AxPatInfo.sbDisplay_Init()
        AxResult.sbDisplay_Init("ALL")

        Me.spdList.MaxRows = 0
        Me.spdList_eq.MaxRows = 0

        Try
            Select Case Me.tbcJob.SelectedTab.Text
                Case "검사그룹별"
                    sbDisplay_SpcList_T()

                Case "작업그룹별"
                    sbDisplay_SpcList_W()

                Case "검사장비별"
                    sbDisplay_SpcList_E()

                Case Else
                    sbDisplay_SpcList_WL()

            End Select

            Me.txtSearch.Text = "" : Me.txtSearch.Tag = ""

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf & ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default
        End Try
    End Sub

    Private Sub btnReg_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnRst = AxResult.fnReg("1")
        If blnRst Then
            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)

            AxPatInfo.sbDisplay_Init()
            AxResult.sbDisplay_Init("ALL")
            If Me.tbcJob.SelectedTab.Text = "작업그룹별" Then
                Me.txtWkNoS.Focus()
            Else
                Me.txtSearch.Focus()
            End If
        End If

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub btnMW_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMW.Click
        Dim blnRst As Boolean

        Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        blnRst = AxResult.fnReg(IIf(Me.btnMW.Text.StartsWith("중간보고"), "22", "2").ToString)
        If blnRst Then
            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)

            If Me.chkMW.Checked And Me.chkMW.Visible Then Me.chkMW.Checked = False
            '
            AxPatInfo.sbDisplay_Init()
            AxResult.sbDisplay_Init("ALL")

            If Me.tbcJob.SelectedTab.Text = "작업그룹별" Then
                Me.txtWkNoS.Focus()
            Else
                Me.txtSearch.Focus()
            End If

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

        blnRst = AxResult.fnReg("3")
        If blnRst Then
            'MsgBox("정상적으로 완료되었습니다.!!", MsgBoxStyle.Information)


            AxPatInfo.sbDisplay_Init()
            AxResult.sbDisplay_Init("ALL")
            If Me.tbcJob.SelectedTab.Text = "작업그룹별" Then
                txtWkNoS.Focus()
            Else
                txtSearch.Focus()
            End If
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

        Dim objSpd As AxFPSpreadADO.AxfpSpread

        Dim strTclsCds As String = ""
        Dim strEqCd As String = ""

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Select Case tbcJob.SelectedTab.Text
                Case "검사그룹별"

                    With spdTGrp
                        For intRow As Integer = 1 To .MaxRows
                            .Row = intRow
                            .Col = .GetColFromID("chk")
                            If .Text = "1" Then
                                .Col = .GetColFromID("tclscd") : strTclsCds += .Text
                            End If
                        Next

                        If strTclsCds <> "" Then strTclsCds = "'" + strTclsCds.Substring(0, strTclsCds.Length - 1).Replace(",", "', '") + "'"
                    End With
                Case "검사장비별"
                    With spdEq
                        For iRow As Integer = 1 To .MaxRows
                            .Row = iRow
                            .Col = .GetColFromID("chk")
                            If .Text = "1" Then
                                .Col = .GetColFromID("eqcd") : strEqCd = .Text
                            End If
                        Next
                    End With

            End Select

            If spdList.Visible Then
                objSpd = spdList
            Else
                objSpd = spdList_eq
            End If
            With objSpd
                AxResult.BatchMode = True
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("chk") : strChk = .Text

                    If strChk = "1" Then
                        Dim strBcNo As String = ""
                        Dim strRegNo As String = ""
                        Dim strPatNm As String = ""
                        Dim strSexAge As String = ""

                        .Col = .GetColFromID("bcno") : strBcNo = .Text.Replace("-", "")

                        AxPatInfo.BcNo = strBcNo
                        AxPatInfo.fnDisplay_Data()

                        AxResult.RegNo = AxPatInfo.RegNo
                        AxResult.PatName = AxPatInfo.PatNm
                        AxResult.SexAge = AxPatInfo.SexAge
                        AxResult.DeptCd = AxPatInfo.DeptName
                        AxResult.FnDt = AxPatInfo.FnDt

                        AxResult.TestCds = strTclsCds
                        AxResult.EqCd = strEqCd
                        AxResult.sbDisplay_Data(strBcNo, True)

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
            'btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception

        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub btnRerun_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRerun.Click

        Dim blnRst As Boolean = False

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            blnRst = AxResult.fnReRun(IIf(cboRerun.Text = "", "Standard", cboRerun.Text).ToString)

            If blnRst Then
                AxPatInfo.sbDisplay_Init()
                AxResult.sbDisplay_Init("ALL")

                txtSearch.SelectAll()
                txtSearch.Focus()
            End If
        Catch ex As Exception
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub btnRst_Clear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRst_Clear.Click

        Dim blnRst As Boolean = False

        Try
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            blnRst = AxResult.fnReg_Erase()

            If blnRst Then
                AxPatInfo.sbDisplay_Init()
                AxResult.sbDisplay_Init("ALL")

                txtSearch.SelectAll()
                txtSearch.Focus()
            End If
        Catch ex As Exception
        Finally
            Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End Try

    End Sub

    Private Sub btnUpDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click, btnDown.Click
        Dim spd As AxFPSpreadADO.AxfpSpread


        If tbcJob.SelectedTab.Text = "검사장비별" Then
            spd = Me.spdList_eq
        Else
            spd = Me.spdList
        End If
        If spd.MaxRows = 0 Then Return


        Dim iNext As Integer = 0

        If CType(sender, Windows.Forms.Button).Name.ToLower.EndsWith("up") Then
            If spd.ActiveRow < 1 Then Return

            iNext -= 1
        Else
            If spd.ActiveRow = spd.MaxRows Then Return

            iNext += 1
        End If

        Me.spdSpcList_ClickEvent(spd, New AxFPSpreadADO._DSpreadEvents_ClickEvent(1, spd.ActiveRow + iNext))

        With spd
            .ReDraw = False
            .SetActiveCell(1, .ActiveRow + iNext)
            '       .Action = FPSpreadADO.ActionConstants.ActionGotoCell
            .ReDraw = True
        End With
    End Sub

    Private Sub chkJobGbn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkJobGbn.CheckedChanged

        If chkJobGbn.Checked Then
            lblWkNo.Visible = True
            txtWkNoE.Visible = True
        Else
            lblWkNo.Visible = False
            txtWkNoE.Visible = False
        End If
    End Sub

    Private Sub txtWkNoS_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWkNoS.GotFocus, txtWkNoE.GotFocus
        txtWkNoS.SelectionStart = 0
        txtWkNoS.SelectAll()
    End Sub

    Private Sub txtWkNoS_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWkNoS.KeyDown

        If e.KeyCode <> Keys.Enter Then Return
        If chkJobGbn.Checked Then Return
        If cboWkGrp.SelectedIndex < 0 Then Return

        Me.txtWkNoS.Text = Me.txtWkNoS.Text.PadLeft(4, "0"c)

        Dim sWkDtS As String = Me.dtpWkDt.Text.Replace("-", "").PadRight(8, "0"c)
        Dim sWkNo As String = Me.txtWkNoS.Text

        Dim dt As DataTable = LISAPP.APP_R.RstFn.fnGet_SpcList_WGrp(Ctrl.Get_Code(cboWkGrp), sWkDtS, "", sWkNo, sWkNo, "")

        If dt.Rows.Count > 0 Then
            With spdList
                Dim iRow As Integer = .SearchCol(.GetColFromID("workno"), 0, .MaxRows, dt.Rows(0).Item("workno").ToString, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                If iRow > 0 Then
                    spdSpcList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(iRow, 1))
                Else
                    If dt.Rows.Count > 0 Then
                        .MaxRows += 1
                        For ix As Integer = 0 To dt.Columns.Count - 1
                            Dim intCol As Integer = .GetColFromID(dt.Columns(ix).ColumnName.ToLower())

                            If intCol > 0 Then
                                .Row = .MaxRows
                                .Col = intCol : .Text = dt.Rows(0).Item(ix).ToString.Trim
                            End If
                        Next
                        spdSpcList_ClickEvent(spdList, New AxFPSpreadADO._DSpreadEvents_ClickEvent(.MaxRows, 1))
                    End If
                End If

            End With

            If txtWkNoE.Visible = False Then
                txtWkNoS.Focus()
            End If
        Else

            AxPatInfo.sbDisplay_Init()
            AxResult.sbDisplay_Init("ALL")

            MsgBox("해당 자료가 없습니다.!!", MsgBoxStyle.Information)
            txtWkNoS.Focus()
        End If

        txtWkNoS.Text = ""
    End Sub


    Private Sub chkSel_List_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSel_List.CheckedChanged

        Dim objSpd As AxFPSpreadADO.AxfpSpread = Nothing

        Select Case Me.tbcJob.SelectedTab.Text
            Case "검사장비별"
                objSpd = spdList_eq
            Case Else
                objSpd = spdList
        End Select

        With objSpd
            For intIdx As Integer = 1 To .MaxRows
                .Row = intIdx
                .Col = .GetColFromID("chk")
                .Text = IIf(chkSel_List.Checked, "1", "").ToString
            Next
        End With

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

    Private Sub Panel5_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel5.DoubleClick
        If AxResult.ColHiddenYn Then
            AxResult.ColHiddenYn = False
        Else
            AxResult.ColHiddenYn = True
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

    Private Sub chkMoveCol_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMoveCol.CheckedChanged
        If chkMoveCol.Checked Then
            spdList.AllowColMove = True
            spdList_eq.AllowColMove = True
        Else
            spdList.AllowColMove = False
            spdList_eq.AllowColMove = False
        End If
    End Sub

    Private Sub txtSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Dim sFn As String = ""

        Try
            Me.txtSearch.SelectAll()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FGR_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub cboWkGrp_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWkGrp.SelectedIndexChanged

        Dim sWkGbn As String = Me.cboWkGrp.Text.Split("|"c)(1)

        Select Case sWkGbn
            Case "1" : Me.dtpWkDt.CustomFormat = "yyyy-MM-dd"
            Case "2" : Me.dtpWkDt.CustomFormat = "yyyy-MM"
            Case "3" : Me.dtpWkDt.CustomFormat = "yyyy"
        End Select

        COMMON.CommXML.setOneElementXML(msXmlDir, msWGFile, "WKGRP", cboWkGrp.SelectedIndex.ToString)

    End Sub

    Private Sub cboPartSlip_wl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

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

    Private Sub AxResult_Call_SpRst(ByVal BcNo As String, ByVal TestCd As String) Handles AxResult.Call_SpRst
        Try
            Dim frmChild As Windows.Forms.Form
            frmChild = New LISR.FGR08(1, TestCd, BcNo)
            CType(frmChild, LISR.FGR08).msUse_PartCd = ""

            Me.AddOwnedForm(frmChild)
            frmChild.WindowState = FormWindowState.Normal
            frmChild.Activate()
            frmChild.Show()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub dtpTkDtE_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpTkDtE.CloseUp, dtpTkdtS.CloseUp, dtpWkDt_e.CloseUp, dtpWkDt.CloseUp, dtpWLdte.CloseUp, dtpWLdts.CloseUp
        Me.txtSearch.Text = ""
    End Sub

    Private Sub chkMW_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMW.CheckedChanged
        If Me.chkMW.Checked Then
            Me.btnMW.Text = "중간보고(F11)"
        Else
            Me.btnMW.Text = "결과확인(F11)"
        End If
    End Sub

    Private Sub btnRst_ocs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRst_ocs.Click

        Try
            Dim blnRet As Boolean = (New LISAPP.APP_R.AxRstFn).fnReg_OCS(AxResult.BCNO)

            MsgBox(IIf(blnRet, "성공", "실패").ToString)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub cboQryGbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboQryGbn.SelectedIndexChanged
        If Me.cboQryGbn.SelectedIndex = 0 Then
            sbDisplay_part()
        Else
            sbDisplay_Slip()
        End If

        If Me.cboPartSlip.Items.Count > 0 Then Me.cboPartSlip.SelectedIndex = 0

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
End Class


